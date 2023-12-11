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

    // 使用原子增减变量
    public int _errorNum = 0;

    public int _successNum = 0;

    public int _allSize = 0;

    // 线程安全的 List
    public ConcurrentBag<GithubReposContentsModel> _fileList { get; set; }

    public string _ownerName { get; set; }

    public string _projectName { get; set; }

    public string _branchName { get; set; }

    public string _dirPath { get; set; }

    // 多线程的取消token
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

            ShowMsg("开始了");

            _ownerName = HttpUtility.UrlDecode(textBoxOwnerName.Text);
            _projectName = HttpUtility.UrlDecode(textBoxProjectName.Text);
            _branchName = HttpUtility.UrlDecode(textBoxBranchName.Text);
            _dirPath = HttpUtility.UrlDecode(textBoxGithubDir.Text);

            HttpClient client = CreateHttpClient();

            ShowMsg("获取文件信息");

            await FetchFileInfoAsync(_dirPath, cancellation.Token);

            ShowMsg("下载文件中");

            await DownloadFileAsync();
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }
    }

    /// <summary>
    /// 获取文件信息 (递归)
    /// </summary>
    /// <param name="path"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task FetchFileInfoAsync(string path, CancellationToken cancellationToken)
    {
        ParallelOptions parallelOptions = new ParallelOptions
        {
            MaxDegreeOfParallelism = Convert.ToInt32(textBoxDownloadTaskCount.Text), // 并发数量
            CancellationToken = cancellation.Token, // 可以主动取消 task 的 token
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
    /// 文件大小 单位转换
    /// </summary>
    /// <param name="size">字节值 (B)</param>
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
    /// 拼接查询地址( 查询目录中的文件夹与文件信息 )
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    public string CreateGithubContentsUrl(string path)
    {
        return $"https://api.github.com/repos/{_ownerName}/{_projectName}/contents/{path}?ref={_branchName}";
    }

    /// <summary>
    /// 下载文件
    /// </summary>
    /// <returns></returns>
    public async Task DownloadFileAsync()
    {
        ShowMsg("开始下载喽");

        labelAllNum.Text = _fileList.Count + "";

        ParallelOptions parallelOptions = new ParallelOptions
        {
            MaxDegreeOfParallelism = Convert.ToInt32(textBoxDownloadTaskCount.Text), // 并发数量
            CancellationToken = cancellation.Token, // 可以主动取消 task 的 token
        };

        await Parallel.ForEachAsync(_fileList, parallelOptions, async (item, cancelToken) =>
          {
              try
              {
                  if (item.status != "success")
                  {
                      var client = CreateHttpClient();

                      // 下载文件, 得到 byte[]
                      var fileByteData = await client.GetByteArrayAsync(item.download_url, cancelToken);

                      // 本地目录 + 仓库名 + 文件地址
                      var filePath = Path.Join(textBoxLocalDir.Text.Trim(), _projectName, item.name.Replace("/", "\\"));

                      // 递归创建文件夹
                      CreateDir(filePath);

                      // 写 文件 到 硬盘
                      System.IO.File.WriteAllBytes(filePath, fileByteData);

                      item.status = "success";
                      // 成功数 +1
                      Interlocked.Increment(ref _successNum);

                      labelSuccessNum.Invoke(() =>
                      {
                          labelSuccessNum.Text = _successNum + "";
                      });
                  }
              }
              catch (Exception e)
              {
                  // 错误数 +1
                  Interlocked.Decrement(ref _errorNum);

                  item.status = e.Message;

                  labelErrorNum.Invoke(() =>
                  {
                      labelErrorNum.Text = _errorNum + "";
                  });
              }
          });

        ShowMsg("完成");
    }

    /// <summary>
    /// 创建目录
    /// </summary>
    /// <param name="fullPath"></param>
    private void CreateDir(string fullPath)
    {
        if (System.IO.File.Exists(fullPath))
        {
            return;
        }
        else //不存在则开始创建
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
    /// 创建 HttpClient ( 添加上UA )
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

    // 停止
    private void buttonStop_Click(object sender, EventArgs e)
    {
        cancellation.CancelAsync();
    }

    // 重试
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
    /// 文件(夹)名称
    /// </summary>
    public string name { get; set; }

    /// <summary>
    /// 类型 (dir/file)
    /// </summary>
    [JsonConverter(typeof(JsonStringEnumConverter))] // 枚举字符串
    public GithubReposContentsTypeEnum type { get; set; }

    /// <summary>
    /// 子目录内容 (文件(夹) GithubReposContentsModel)
    /// </summary>
    public string url { get; set; }

    /// <summary>
    /// 文件大小 (文件夹为 0)
    /// </summary>
    public int size { get; set; }

    /// <summary>
    /// 文件下载链接 (文件夹 为 null)
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