using System.Collections.Concurrent;
using System.Net.Http.Json;
using System.Text.Json.Serialization;
using System.Web;

namespace GithubDownload;

public partial class Form1 : Form
{
    public Form1()
    {
        InitializeComponent();
    }

    // ʹ��ԭ����������
    public int _errorNum = 0;

    public int _successNum = 0;

    public int _allSize = 0;

    // �̰߳�ȫ�� List
    public ConcurrentBag<GithubReposContentsModel> _fileList { get; set; }

    public string _ownerName { get; set; }

    public string _projectName { get; set; }

    public string _branchName { get; set; }

    public string _dirPath { get; set; }

    // ���̵߳�ȡ��token
    public CancellationTokenSource cancellation = new CancellationTokenSource();

    private void buttonStart_Click(object sender, EventArgs e)
    {
        Run();
    }

    public async void Run()
    {
        try
        {
            _fileList = new();

            cancellation.TryReset();

            _successNum = 0;
            _errorNum = 0;
            _allSize = 0;

            ShowMsg("��ʼ��");

            _ownerName = HttpUtility.UrlDecode(textBoxOwnerName.Text);
            _projectName = HttpUtility.UrlDecode(textBoxProjectName.Text);
            _branchName = HttpUtility.UrlDecode(textBoxBranchName.Text);
            _dirPath = HttpUtility.UrlDecode(textBoxGithubDir.Text);

            HttpClient client = CreateHttpClient();

            ShowMsg("��ȡ�ļ���Ϣ");

            await FetchFileInfoAsync(_dirPath, cancellation.Token);

            ShowMsg("�����ļ���");

            await DownloadFileAsync();
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }
    }

    /// <summary>
    /// ��ȡ�ļ���Ϣ (�ݹ�)
    /// </summary>
    /// <param name="path"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task FetchFileInfoAsync(string path, CancellationToken cancellationToken)
    {
        ParallelOptions parallelOptions = new ParallelOptions
        {
            MaxDegreeOfParallelism = Convert.ToInt32(textBoxDownloadTaskCount.Text), // ��������
            CancellationToken = cancellation.Token, // ��������ȡ�� task �� token
        };

        var client = CreateHttpClient();

        var contentsList =
               await client.GetFromJsonAsync<List<GithubReposContentsModel>>(
                   CreateGithubContentsUrl(path), cancellationToken
                   );

        if (contentsList is null)
        {
            return;
        }

        await Parallel.ForEachAsync(contentsList, parallelOptions, async (item, cancelToken) =>
        {
            if (item.type == GithubReposContentsTypeEnum.dir)
            {
                await FetchFileInfoAsync(path + "/" + item.name, cancelToken);
            }
            else if (item.type == GithubReposContentsTypeEnum.file)
            {
                item.name = path + "/" + item.name;

                _fileList.Add(item);

                Interlocked.Add(ref _allSize, item.size);

                labelAllNum.Invoke(() =>
                {
                    labelAllNum.Text = _fileList.Count + "";

                    labelAllSize.Text = HumanReadableFilesize(Convert.ToDouble(_allSize));
                });
            }
        });
    }

    /// <summary>
    /// �ļ���С ��λת��
    /// </summary>
    /// <param name="size">�ֽ�ֵ (B)</param>
    /// <returns></returns>
    public string HumanReadableFilesize(double size)
    {
        string[] units = new string[] { "B", "KB", "MB", "GB", "TB", "PB" };

        double mod = 1024.0;

        int i = 0;

        while (size >= mod)
        {
            size /= mod;

            i++;
        }

        return Math.Round(size) + units[i];
    }

    /// <summary>
    /// ƴ�Ӳ�ѯ��ַ( ��ѯĿ¼�е��ļ������ļ���Ϣ )
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    public string CreateGithubContentsUrl(string path)
    {
        return $"https://api.github.com/repos/{_ownerName}/{_projectName}/contents/{path}?ref={_branchName}";
    }

    /// <summary>
    /// �����ļ�
    /// </summary>
    /// <returns></returns>
    public async Task DownloadFileAsync()
    {
        ShowMsg("��ʼ�����");

        labelAllNum.Text = _fileList.Count + "";

        ParallelOptions parallelOptions = new ParallelOptions
        {
            MaxDegreeOfParallelism = Convert.ToInt32(textBoxDownloadTaskCount.Text), // ��������
            CancellationToken = cancellation.Token, // ��������ȡ�� task �� token
        };

        await Parallel.ForEachAsync(_fileList, parallelOptions, async (item, cancelToken) =>
          {
              try
              {
                  if (item.status != "success")
                  {
                      var client = CreateHttpClient();

                      // �����ļ�, �õ� byte[]
                      var fileByteData = await client.GetByteArrayAsync(item.download_url, cancelToken);

                      // ����Ŀ¼ + �ֿ��� + �ļ���ַ
                      var filePath = Path.Join(textBoxLocalDir.Text.Trim(), _projectName, item.name.Replace("/", "\\"));

                      // �ݹ鴴���ļ���
                      CreateDir(filePath);

                      // д �ļ� �� Ӳ��
                      System.IO.File.WriteAllBytes(filePath, fileByteData);

                      item.status = "success";
                      // �ɹ��� +1
                      Interlocked.Increment(ref _successNum);

                      labelSuccessNum.Invoke(() =>
                      {
                          labelSuccessNum.Text = _successNum + "";
                      });
                  }
              }
              catch (Exception e)
              {
                  // ������ +1
                  Interlocked.Decrement(ref _errorNum);

                  item.status = e.Message;

                  labelErrorNum.Invoke(() =>
                  {
                      labelErrorNum.Text = _errorNum + "";
                  });
              }
          });

        ShowMsg("���");
    }

    /// <summary>
    /// ����Ŀ¼
    /// </summary>
    /// <param name="fullPath"></param>
    private void CreateDir(string fullPath)
    {
        if (System.IO.File.Exists(fullPath))
        {
            return;
        }
        else //��������ʼ����
        {
            string dirpath = fullPath[..fullPath.LastIndexOf('\\')];

            string[] pathes = dirpath.Split('\\');

            if (pathes.Length > 1)
            {
                string path = pathes[0];

                for (int i = 1; i < pathes.Length; i++)
                {
                    path += "\\" + pathes[i];

                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                }
            }
        }
    }

    /// <summary>
    /// ���� HttpClient ( �����UA )
    /// </summary>
    /// <returns></returns>
    public HttpClient CreateHttpClient()
    {
        var client = new HttpClient();

        client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/117.0.0.0 Safari/537.36 Edg/117.0.2045.43");

        return client;
    }

    public void ShowMsg(string data)
    {
        this.labelMsg.Text = data;
    }

    // ֹͣ
    private void buttonStop_Click(object sender, EventArgs e)
    {
        cancellation.CancelAsync();
    }

    // ����
    private void buttonRetry_Click(object sender, EventArgs e)
    {
        _errorNum = 0;

        labelErrorNum.Text = "0";

        _ = DownloadFileAsync();
    }
}

public class GithubReposContentsModel
{
    /// <summary>
    /// �ļ�(��)����
    /// </summary>
    public string name { get; set; }

    /// <summary>
    /// ���� (dir/file)
    /// </summary>
    [JsonConverter(typeof(JsonStringEnumConverter))] // ö���ַ���
    public GithubReposContentsTypeEnum type { get; set; }

    /// <summary>
    /// ��Ŀ¼���� (�ļ�(��) GithubReposContentsModel)
    /// </summary>
    public string url { get; set; }

    /// <summary>
    /// �ļ���С (�ļ���Ϊ 0)
    /// </summary>
    public int size { get; set; }

    /// <summary>
    /// �ļ��������� (�ļ��� Ϊ null)
    /// </summary>
    public string download_url { get; set; }

    [JsonIgnore]
    public string status { get; set; }
}

public enum GithubReposContentsTypeEnum
{
    dir,
    file
}