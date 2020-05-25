using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using StringMatching;

namespace WordFA
{
    public partial class FrmMain : Form
    {
        private delegate void SetTextCallBack(string str);

        public FrmMain()
        {
            InitializeComponent();
        }

        private void btnfile_Click(object sender, EventArgs e)
        {
            string fileName = GetFileName();
            if (string.IsNullOrEmpty(fileName) == false)
                txtFile.Text = fileName;
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            string fileName = Application.StartupPath + @"\user.txt";
            if (File.Exists(fileName) == true)
                txtdirc.Text = fileName;

            lblInfor.Text = string.Empty;
        }

        private string GetFileName()
        {
            string fileName = string.Empty;
            OpenFileDialog fid = new OpenFileDialog {Filter = @"*.txt|*.txt"};
            if (fid.ShowDialog() == DialogResult.OK)
            {
                fileName = fid.FileName;
            }
            return fileName;
        }

        private void btnDire_Click(object sender, EventArgs e)
        {
            string fileName = GetFileName();
            if (string.IsNullOrEmpty(fileName) == false)
                txtdirc.Text = fileName;
        }

        private void RunPath()
        {
            SubstrStatic subStat = new SubstrStatic();
            string str = @"系统工具开始统计....";
            str += "\n\n" + @"系统所用匹配方法为：" + subStat.MatchingClassName;
            str += "\n\n" + @"系统所用排序方法为：" + subStat.SortClassName;
            SetText(str);
            HiperfTimer ht = new HiperfTimer();
            ht.Start();
            string path = txtPath.Text.Trim();
            string dirc = txtdirc.Text.Trim();

            str += "\n\n" + @"系统工具正在读取文件夹中的文件...";
            SetText(str);
            subStat.SourceFromPath(path);
            str += "\n\n" + @"系统工具正在统计词频...";
            SetText(str);
            subStat.SubStringFromFile(dirc);
            List<Node> lst = subStat.Run();
            str += "\n\n" + @"系统工具正在写入文件...";
            SetText(str);
            subStat.Write(lst);
            ht.Stop();
            string cost = Math.Round(ht.Duration_second, 4).ToString(CultureInfo.InvariantCulture);
            str += "\n\n" + @"系统工具所用时间为：" + cost + "秒";
            SetText(str);
        }

        private void SetText(string text)
        {
            if (lblInfor.InvokeRequired)
            {
                SetTextCallBack d = new SetTextCallBack(SetText);
                Invoke(d, new object[] {text});
            }
            else
            {
                lblInfor.Text = text;
            }
        }

        private void btnstatic_Click(object sender, EventArgs e)
        {
            string file = txtFile.Text.Trim();
            string path = txtPath.Text.Trim();
            string dirc = txtdirc.Text.Trim();
            lblInfor.Text = string.Empty;

            if (string.IsNullOrEmpty(dirc) == true)
            {
                MessageBox.Show(@"未选择待字典文件.", @"系统提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            SubstrStatic subStat = new SubstrStatic();
            if (rdoSigle.Checked == true) //单个文件统计
            {
                if (string.IsNullOrEmpty(file) == true)
                {
                    MessageBox.Show(@"未选择待分析文件.", @"系统提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                subStat.SourceFromFile(file);
                subStat.SubStringFromFile(dirc);
                List<Node> lst = subStat.Run();
                subStat.Write(lst);
            }
            else //多个文件统计
            {
                if (string.IsNullOrEmpty(path) == true)
                {
                    MessageBox.Show(@"未选择待分析路径.", @"系统提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                Thread thread = new Thread(RunPath);
                thread.IsBackground = true;
                thread.Start();
            }
        }

        private void btnPath_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            fbd.SelectedPath = AppDomain.CurrentDomain.BaseDirectory;

            if (fbd.ShowDialog() == DialogResult.OK)
            {
                DirectoryInfo aa = new DirectoryInfo(fbd.SelectedPath);
                FileInfo[] finfo = aa.GetFiles("*.txt");
                if (finfo.Length == 0)
                {
                    MessageBox.Show(@"该目录下不存在txt文件.", @"系统提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                txtPath.Text = fbd.SelectedPath;
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
