namespace GithubDownload
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            buttonStart = new Button();
            labelMsg = new Label();
            labelAllNum = new Label();
            label3 = new Label();
            label5 = new Label();
            textBoxLocalDir = new TextBox();
            label9 = new Label();
            label1 = new Label();
            textBoxDownloadTaskCount = new TextBox();
            buttonStop = new Button();
            label10 = new Label();
            label11 = new Label();
            labelSuccessNum = new Label();
            labelErrorNum = new Label();
            textBoxOwnerName = new TextBox();
            textBoxProjectName = new TextBox();
            textBoxBranchName = new TextBox();
            textBoxGithubDir = new TextBox();
            label2 = new Label();
            labelAllSize = new Label();
            buttonRetry = new Button();
            label12 = new Label();
            label13 = new Label();
            label14 = new Label();
            label15 = new Label();
            groupBox1 = new GroupBox();
            label4 = new Label();
            label6 = new Label();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // buttonStart
            // 
            buttonStart.Location = new Point(137, 116);
            buttonStart.Name = "buttonStart";
            buttonStart.Size = new Size(177, 43);
            buttonStart.TabIndex = 0;
            buttonStart.Text = "开始下载";
            buttonStart.UseVisualStyleBackColor = true;
            buttonStart.Click += buttonStart_Click;
            // 
            // labelMsg
            // 
            labelMsg.AutoSize = true;
            labelMsg.Location = new Point(82, 307);
            labelMsg.Name = "labelMsg";
            labelMsg.Size = new Size(26, 17);
            labelMsg.TabIndex = 1;
            labelMsg.Text = "xxx";
            // 
            // labelAllNum
            // 
            labelAllNum.AutoSize = true;
            labelAllNum.Location = new Point(231, 16);
            labelAllNum.Name = "labelAllNum";
            labelAllNum.Size = new Size(15, 17);
            labelAllNum.TabIndex = 2;
            labelAllNum.Text = "0";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(158, 16);
            label3.Name = "label3";
            label3.Size = new Size(67, 17);
            label3.TabIndex = 3;
            label3.Text = "文件总数 : ";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(15, 307);
            label5.Name = "label5";
            label5.Size = new Size(39, 17);
            label5.TabIndex = 6;
            label5.Text = "消息 :";
            // 
            // textBoxLocalDir
            // 
            textBoxLocalDir.Font = new Font("Microsoft YaHei UI", 15F);
            textBoxLocalDir.Location = new Point(161, 254);
            textBoxLocalDir.Name = "textBoxLocalDir";
            textBoxLocalDir.Size = new Size(762, 33);
            textBoxLocalDir.TabIndex = 10;
            textBoxLocalDir.Text = "E:\\Desktop\\Test\\";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(16, 262);
            label9.Name = "label9";
            label9.Size = new Size(139, 17);
            label9.TabIndex = 11;
            label9.Text = "保存到本地文件夹路径 : ";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(744, 16);
            label1.Name = "label1";
            label1.Size = new Size(55, 17);
            label1.TabIndex = 13;
            label1.Text = "并发数 : ";
            // 
            // textBoxDownloadTaskCount
            // 
            textBoxDownloadTaskCount.Location = new Point(817, 13);
            textBoxDownloadTaskCount.Name = "textBoxDownloadTaskCount";
            textBoxDownloadTaskCount.Size = new Size(100, 23);
            textBoxDownloadTaskCount.TabIndex = 14;
            textBoxDownloadTaskCount.Text = "5";
            // 
            // buttonStop
            // 
            buttonStop.Location = new Point(613, 118);
            buttonStop.Name = "buttonStop";
            buttonStop.Size = new Size(158, 38);
            buttonStop.TabIndex = 15;
            buttonStop.Text = "停止下载";
            buttonStop.UseVisualStyleBackColor = true;
            buttonStop.Click += buttonStop_Click;
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new Point(320, 16);
            label10.Name = "label10";
            label10.Size = new Size(55, 17);
            label10.TabIndex = 16;
            label10.Text = "成功数 : ";
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Location = new Point(483, 16);
            label11.Name = "label11";
            label11.Size = new Size(55, 17);
            label11.TabIndex = 17;
            label11.Text = "失败数 : ";
            // 
            // labelSuccessNum
            // 
            labelSuccessNum.AutoSize = true;
            labelSuccessNum.Location = new Point(400, 16);
            labelSuccessNum.Name = "labelSuccessNum";
            labelSuccessNum.Size = new Size(15, 17);
            labelSuccessNum.TabIndex = 18;
            labelSuccessNum.Text = "0";
            // 
            // labelErrorNum
            // 
            labelErrorNum.AutoSize = true;
            labelErrorNum.Location = new Point(562, 16);
            labelErrorNum.Name = "labelErrorNum";
            labelErrorNum.Size = new Size(15, 17);
            labelErrorNum.TabIndex = 19;
            labelErrorNum.Text = "0";
            // 
            // textBoxOwnerName
            // 
            textBoxOwnerName.Font = new Font("Microsoft YaHei UI", 15F);
            textBoxOwnerName.Location = new Point(20, 57);
            textBoxOwnerName.Name = "textBoxOwnerName";
            textBoxOwnerName.Size = new Size(177, 33);
            textBoxOwnerName.TabIndex = 21;
            // 
            // textBoxProjectName
            // 
            textBoxProjectName.Font = new Font("Microsoft YaHei UI", 15F);
            textBoxProjectName.Location = new Point(203, 57);
            textBoxProjectName.Name = "textBoxProjectName";
            textBoxProjectName.Size = new Size(186, 33);
            textBoxProjectName.TabIndex = 22;
            // 
            // textBoxBranchName
            // 
            textBoxBranchName.Font = new Font("Microsoft YaHei UI", 15F);
            textBoxBranchName.Location = new Point(395, 57);
            textBoxBranchName.Name = "textBoxBranchName";
            textBoxBranchName.Size = new Size(212, 33);
            textBoxBranchName.TabIndex = 23;
            // 
            // textBoxGithubDir
            // 
            textBoxGithubDir.Font = new Font("Microsoft YaHei UI", 15F);
            textBoxGithubDir.Location = new Point(613, 57);
            textBoxGithubDir.Name = "textBoxGithubDir";
            textBoxGithubDir.Size = new Size(287, 33);
            textBoxGithubDir.TabIndex = 24;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(17, 16);
            label2.Name = "label2";
            label2.Size = new Size(55, 17);
            label2.TabIndex = 25;
            label2.Text = "总大小 : ";
            // 
            // labelAllSize
            // 
            labelAllSize.AutoSize = true;
            labelAllSize.Location = new Point(82, 16);
            labelAllSize.Name = "labelAllSize";
            labelAllSize.Size = new Size(31, 17);
            labelAllSize.TabIndex = 26;
            labelAllSize.Text = "0 M";
            // 
            // buttonRetry
            // 
            buttonRetry.Location = new Point(364, 117);
            buttonRetry.Name = "buttonRetry";
            buttonRetry.Size = new Size(179, 40);
            buttonRetry.TabIndex = 27;
            buttonRetry.Text = "仅下载失败的";
            buttonRetry.UseVisualStyleBackColor = true;
            buttonRetry.Click += buttonRetry_Click;
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Location = new Point(55, 28);
            label12.Name = "label12";
            label12.Size = new Size(32, 17);
            label12.TabIndex = 28;
            label12.Text = "作者";
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.Location = new Point(255, 28);
            label13.Name = "label13";
            label13.Size = new Size(32, 17);
            label13.TabIndex = 29;
            label13.Text = "仓库";
            // 
            // label14
            // 
            label14.AutoSize = true;
            label14.Location = new Point(494, 28);
            label14.Name = "label14";
            label14.Size = new Size(64, 17);
            label14.TabIndex = 30;
            label14.Text = "分支 / tag";
            // 
            // label15
            // 
            label15.AutoSize = true;
            label15.Location = new Point(702, 28);
            label15.Name = "label15";
            label15.Size = new Size(56, 17);
            label15.TabIndex = 31;
            label15.Text = "相对路径";
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(label14);
            groupBox1.Controls.Add(label15);
            groupBox1.Controls.Add(buttonStart);
            groupBox1.Controls.Add(buttonStop);
            groupBox1.Controls.Add(label13);
            groupBox1.Controls.Add(textBoxOwnerName);
            groupBox1.Controls.Add(label12);
            groupBox1.Controls.Add(textBoxProjectName);
            groupBox1.Controls.Add(buttonRetry);
            groupBox1.Controls.Add(textBoxBranchName);
            groupBox1.Controls.Add(textBoxGithubDir);
            groupBox1.Location = new Point(17, 55);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(906, 179);
            groupBox1.TabIndex = 32;
            groupBox1.TabStop = false;
            groupBox1.Text = "Github 信息";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.ForeColor = Color.Red;
            label4.Location = new Point(494, 39);
            label4.Name = "label4";
            label4.Size = new Size(436, 17);
            label4.TabIndex = 33;
            label4.Text = "并发数越高, 完成的越快, 当心触发 Github 403 速率限制, 一般的使用强度没啥事";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(529, 351);
            label6.Name = "label6";
            label6.Size = new Size(389, 119);
            label6.TabIndex = 34;
            label6.Text = "例子:\r\nhttps://github.com/dotnet/webapi/tree/master/docs/zh-Hans\r\n解析:\r\ndotnet 作者,\r\nwebapi 仓库\r\nmaster 分支 / tag\r\ndocs/zh-Hans 路径 ( 可以在github页面中一键复制 ) (不填是整个仓库)";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(942, 479);
            Controls.Add(label6);
            Controls.Add(label4);
            Controls.Add(groupBox1);
            Controls.Add(labelAllSize);
            Controls.Add(label2);
            Controls.Add(labelErrorNum);
            Controls.Add(labelSuccessNum);
            Controls.Add(label11);
            Controls.Add(label10);
            Controls.Add(textBoxDownloadTaskCount);
            Controls.Add(label1);
            Controls.Add(label9);
            Controls.Add(textBoxLocalDir);
            Controls.Add(label5);
            Controls.Add(label3);
            Controls.Add(labelAllNum);
            Controls.Add(labelMsg);
            Name = "Form1";
            Text = "Github 文件夹/文件 下载";
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button buttonStart;
        private Label labelMsg;
        private Label labelAllNum;
        private Label label3;
        private Label label5;
        private TextBox textBoxLocalDir;
        private Label label9;
        private Label label1;
        private TextBox textBoxDownloadTaskCount;
        private Button buttonStop;
        private Label label10;
        private Label label11;
        private Label labelSuccessNum;
        private Label labelErrorNum;
        private Button buttonContinue;
        private TextBox textBoxOwnerName;
        private TextBox textBoxProjectName;
        private TextBox textBoxBranchName;
        private TextBox textBoxGithubDir;
        private Label label2;
        private Label labelAllSize;
        private Button buttonRetry;
        private Label label12;
        private Label label13;
        private Label label14;
        private Label label15;
        private GroupBox groupBox1;
        private Label label4;
        private Label label6;
    }
}
