using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.IO;
using System.Diagnostics;
using runbat.Properties;
using System.Threading;
using System.Net;

namespace runbat
{
    public partial class Form1 : Form
    {
        WebClient wc;
        Process prcFFMPEG = new Process();
        List<string> titles = new List<string>();
        Form dlg;
        int nProcessingCount;
        int nProcessingMaxCount;

        enum ProcState
        {
            PROC_STATE_READY,
            PROC_STATE_ENCORDING,
        }

        public Form1()
        {
            InitializeComponent();
            StateChange(ProcState.PROC_STATE_READY);
            txtSource.Text = Application.StartupPath;
        }

        private void StateChange(ProcState state)
        {
            switch(state)
            {
                case ProcState.PROC_STATE_READY:
                    lbProcIndex.Text = "대 기 중";
                    break;
                case ProcState.PROC_STATE_ENCORDING:
                    lbProcIndex.Text = string.Format("( {0} / {1} ) 진행 중 ...", nProcessingCount+1, nProcessingMaxCount);
                    break;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Settings.Default.SourceDir) == false)
            {
                fbdSource.SelectedPath = Settings.Default.SourceDir;
            }

            if (fbdSource.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                txtSource.Text = fbdSource.SelectedPath;
                Settings.Default.SourceDir = txtSource.Text;
                Settings.Default.Save();
            }
        }

        private void Start_Click(object sender, EventArgs e)
        {
            this.btnStart.Enabled = false;
            nProcessingCount = 0;
            this.progressBar1.Value = 0;
            titles.Clear();

            if (wc == null)
                wc = new WebClient();

            string[] split_string = textBox2.Text.Split('\n');
            
            foreach (string titleUrl in split_string)
            {
                if (titleUrl.Contains("http") == false)
                    continue;

                string[] chk = titleUrl.Split('|');
                titles.Add(checkExpression(chk[0]));

                DownloadFile(nProcessingCount++, chk[1]);
            }

            nProcessingCount = 0;
            nProcessingMaxCount = titles.Count;
            ThreadPool.QueueUserWorkItem((object state) =>
            {
                ConvertFile(txtSource.Text + "/", titles[0], Application.StartupPath + "/" + nProcessingCount.ToString(), nProcessingCount);
                nProcessingCount++;
            });
        }

        private string checkExpression(string arg)
        {
            string result = arg;

            if (result.Contains('\\'))
                result = result.Replace('\\', '.');
            if (result.Contains('/'))
                result = result.Replace('/', '.');
            if (result.Contains(':'))
                result = result.Replace(':', '.');
            if (result.Contains('*'))
                result = result.Replace('*', '.');
            if (result.Contains('?'))
                result = result.Replace('?', '.');
            if (result.Contains('"'))
                result = result.Replace('"', '.');
            if (result.Contains('<'))
                result = result.Replace('<', '.');
            if (result.Contains('>'))
                result = result.Replace('>', '.');
            if (result.Contains('|'))
                result = result.Replace('|', '.');

            return result;
        }

        private void DownloadFile(int index, string url)
        {
            byte[] streamData = wc.DownloadData(url);
            string strData = Encoding.Default.GetString(streamData);

            string[] chkString = url.Split('?');
            chkString[0] = chkString[0].Substring(0, chkString[0].Length - 5);
            string[] subChk = chkString[0].Split('/');


            chkString[1] = ".ts?" + chkString[1];

            strData = strData.Replace(subChk[subChk.Length - 1], chkString[0]);
            strData = strData.Replace(".ts", chkString[1]);

            System.IO.File.WriteAllText(Application.StartupPath + "/" + index.ToString() + ".m3u8", strData, Encoding.Default);
        }

        private void Exit_Click(object sender, EventArgs e)
        {
            Process[] procs = Process.GetProcessesByName("ffmpeg");
            if (procs.Length > 0)
            {
                foreach (Process proc in Process.GetProcessesByName("ffmpeg"))
                    proc.Kill();
            }

            Application.Exit();
        }

        private void ConvertFile(string filePath, string fileName, string srcFile, int srcIndex)
        {
            this.BeginInvoke(new MethodInvoker(() =>
            {
                StateChange(ProcState.PROC_STATE_ENCORDING);
            }));

            System.IO.FileInfo checkFile = new System.IO.FileInfo(filePath + fileName + ".ts");
            if (checkFile.Exists)
                System.IO.File.Delete(filePath + fileName + ".ts");

            try
            {
                string dstFile = filePath + fileName;
                string strFFMPEGOut;
                ProcessStartInfo psiProcInfo = new ProcessStartInfo();
                TimeSpan estimatedTime = TimeSpan.MaxValue;

                StreamReader srFFMPEG;
                string strFFMPEGCmd = " -c copy \"" + dstFile + ".ts" + "\" " + "-protocol_whitelist \"file,http,https,tcp,tls\" -i " + "\"" + srcFile + ".m3u8" + "\"";

                psiProcInfo.FileName = Application.StartupPath + ((IntPtr.Size == 8) ? "\\x64" : "\\x86") + "\\ffmpeg.exe";
                psiProcInfo.Arguments = strFFMPEGCmd;
                psiProcInfo.UseShellExecute = false;
                psiProcInfo.WindowStyle = ProcessWindowStyle.Hidden;
                psiProcInfo.RedirectStandardError = true;
                psiProcInfo.RedirectStandardOutput = true;
                psiProcInfo.CreateNoWindow = true;

                prcFFMPEG.StartInfo = psiProcInfo;

                prcFFMPEG.Start();

                srFFMPEG = prcFFMPEG.StandardError;

                do
                {
                    strFFMPEGOut = srFFMPEG.ReadLine();

                    string duration = "Duration";
                    if (strFFMPEGOut != null)
                    {
                        if (strFFMPEGOut.TrimStart().IndexOf(duration) == 0)
                        {
                            try
                            {
                                string text = strFFMPEGOut.TrimStart().Substring(duration.Length + 2);
                                int pos = text.IndexOf(",");
                                string estimated = text.Substring(0, pos);

                                estimatedTime = TimeSpan.Parse(estimated);
                            }
                            catch
                            {
                            }
                        }

                        if (estimatedTime != TimeSpan.MaxValue)
                        {
                            // 예측 시간이 나왔으면.
                            string time = "time=";
                            int startPos = strFFMPEGOut.IndexOf(time);
                            if (startPos != -1)
                            {
                                string text = strFFMPEGOut.Substring(startPos + time.Length);
                                int pos = text.IndexOf(" ");
                                string current = text.Substring(0, pos);

                                TimeSpan currentTime = TimeSpan.Parse(current);

                                int progresss = (int)(currentTime.TotalMilliseconds * 100 / estimatedTime.TotalMilliseconds);
                                this.BeginInvoke(new MethodInvoker(() =>
                                {
                                    this.progressBar1.Value = progresss;
                                }));
                            }
                        }
                    }
                } while (prcFFMPEG.HasExited == false);

            }
            finally
            {
                this.BeginInvoke(new MethodInvoker(() =>
                {
                    // 현재 처리한 타이틀 제거
                    if (titles.Count > 0)
                        titles.RemoveAt(0);

                    // m3u8 파일 삭제
                    System.IO.FileInfo deleteSrc = new System.IO.FileInfo(Application.StartupPath + "/" + srcIndex.ToString() + ".m3u8");
                    if (deleteSrc.Exists)
                        System.IO.File.Delete(Application.StartupPath + "/" + (nProcessingCount - 1).ToString() + ".m3u8");

                    // 남은 타이틀이 없으면 버튼 활성화, 프로세스 처리 완전 종료
                    if (titles.Count == 0)
                    {
                        this.btnStart.Enabled = true;
                        this.BeginInvoke(new MethodInvoker(() =>
                        {
                            StateChange(ProcState.PROC_STATE_READY);
                        }));
                    }
                    else
                    {
                        //남은게 있으면 recursion
                        ThreadPool.QueueUserWorkItem((object state) =>
                        {
                            ConvertFile(filePath, titles[0], Application.StartupPath + "/" + nProcessingCount.ToString(), nProcessingCount);
                            nProcessingCount++;
                        });
                    }
                }));
            }
        }

        private void btnAttention_Click(object sender, EventArgs e)
        {
            if (dlg == null)
            {
                dlg = new Form();
                dlg.Text = "주 의 사 항";
                dlg.StartPosition = FormStartPosition.CenterParent;

                // 닫기버튼 추가
                Button btnExit = new Button();
                btnExit.Text = "닫 기";
                btnExit.Click += new EventHandler((object obj, EventArgs eArgs) =>
                {
                    dlg.Hide();
                });
                btnExit.Dock = DockStyle.Bottom;
                dlg.Controls.Add(btnExit);

                // 약관 라벨 추가
                Label lbAttention = new Label();
                System.IO.FileInfo checkAttention = new System.IO.FileInfo(Application.StartupPath + "/attention.txt");
                if(checkAttention.Exists)
                    lbAttention.Text = System.IO.File.ReadAllText(Application.StartupPath + "/attention.txt");

                dlg.Controls.Add(lbAttention);
                lbAttention.Dock = DockStyle.Fill;
            }

            dlg.ShowDialog();
        }
    }
}
