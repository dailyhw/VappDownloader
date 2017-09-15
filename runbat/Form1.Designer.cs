namespace runbat
{
    partial class Form1
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.fbdSource = new System.Windows.Forms.FolderBrowserDialog();
            this.button1 = new System.Windows.Forms.Button();
            this.txtSource = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.Link = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.Exit = new System.Windows.Forms.Button();
            this.btnStart = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.btnAttention = new System.Windows.Forms.Button();
            this.lbProcIndex = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(545, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "...";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // txtSource
            // 
            this.txtSource.Location = new System.Drawing.Point(133, 12);
            this.txtSource.Name = "txtSource";
            this.txtSource.Size = new System.Drawing.Size(406, 21);
            this.txtSource.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "Output Folder";
            // 
            // Link
            // 
            this.Link.AutoSize = true;
            this.Link.Location = new System.Drawing.Point(12, 51);
            this.Link.Name = "Link";
            this.Link.Size = new System.Drawing.Size(70, 12);
            this.Link.TabIndex = 3;
            this.Link.Text = "M3U8 Links";
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(14, 75);
            this.textBox2.Multiline = true;
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(606, 200);
            this.textBox2.TabIndex = 4;
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(14, 281);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(606, 23);
            this.progressBar1.TabIndex = 5;
            // 
            // Exit
            // 
            this.Exit.Location = new System.Drawing.Point(545, 310);
            this.Exit.Name = "Exit";
            this.Exit.Size = new System.Drawing.Size(75, 23);
            this.Exit.TabIndex = 6;
            this.Exit.Text = "Exit";
            this.Exit.UseVisualStyleBackColor = true;
            this.Exit.Click += new System.EventHandler(this.Exit_Click);
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(14, 310);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(75, 23);
            this.btnStart.TabIndex = 7;
            this.btnStart.Text = "Start";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.Start_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(472, 51);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(148, 12);
            this.label2.TabIndex = 8;
            this.label2.Text = "Create by 자두같이생긴애";
            // 
            // btnAttention
            // 
            this.btnAttention.Location = new System.Drawing.Point(402, 46);
            this.btnAttention.Name = "btnAttention";
            this.btnAttention.Size = new System.Drawing.Size(64, 23);
            this.btnAttention.TabIndex = 9;
            this.btnAttention.Text = "주의사항";
            this.btnAttention.UseVisualStyleBackColor = true;
            this.btnAttention.Click += new System.EventHandler(this.btnAttention_Click);
            // 
            // lbProcIndex
            // 
            this.lbProcIndex.AutoSize = true;
            this.lbProcIndex.Location = new System.Drawing.Point(252, 310);
            this.lbProcIndex.Name = "lbProcIndex";
            this.lbProcIndex.Size = new System.Drawing.Size(111, 12);
            this.lbProcIndex.TabIndex = 10;
            this.lbProcIndex.Text = "( 1 / 10 ) 진행 중...";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(632, 341);
            this.Controls.Add(this.lbProcIndex);
            this.Controls.Add(this.btnAttention);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.Exit);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.Link);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtSource);
            this.Controls.Add(this.button1);
            this.Name = "Form1";
            this.Text = "Lovelyz Vapp Downloader";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.FolderBrowserDialog fbdSource;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox txtSource;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label Link;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Button Exit;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnAttention;
        private System.Windows.Forms.Label lbProcIndex;
    }
}

