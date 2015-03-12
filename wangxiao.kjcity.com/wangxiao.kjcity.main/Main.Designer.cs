namespace wangxiao.kjcity.main
{
    partial class Main
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.labelMsg = new System.Windows.Forms.Label();
            this.btmOutLogin = new System.Windows.Forms.Button();
            this.btnMove = new System.Windows.Forms.Button();
            this.btnPlayer = new System.Windows.Forms.Button();
            this.labmsg = new System.Windows.Forms.Label();
            this.btnChapter = new System.Windows.Forms.Button();
            this.btnLogin = new System.Windows.Forms.Button();
            this.listBoxLeft = new System.Windows.Forms.ListBox();
            this.listBoxRight = new System.Windows.Forms.ListBox();
            this.btnExcute = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnExcute);
            this.groupBox1.Controls.Add(this.labelMsg);
            this.groupBox1.Controls.Add(this.btmOutLogin);
            this.groupBox1.Controls.Add(this.btnMove);
            this.groupBox1.Controls.Add(this.btnPlayer);
            this.groupBox1.Controls.Add(this.labmsg);
            this.groupBox1.Controls.Add(this.btnChapter);
            this.groupBox1.Controls.Add(this.btnLogin);
            this.groupBox1.Location = new System.Drawing.Point(26, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(903, 82);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "相关操作";
            // 
            // labelMsg
            // 
            this.labelMsg.AutoSize = true;
            this.labelMsg.ForeColor = System.Drawing.Color.Red;
            this.labelMsg.Location = new System.Drawing.Point(37, 63);
            this.labelMsg.Name = "labelMsg";
            this.labelMsg.Size = new System.Drawing.Size(0, 12);
            this.labelMsg.TabIndex = 7;
            // 
            // btmOutLogin
            // 
            this.btmOutLogin.Location = new System.Drawing.Point(805, 24);
            this.btmOutLogin.Name = "btmOutLogin";
            this.btmOutLogin.Size = new System.Drawing.Size(75, 33);
            this.btmOutLogin.TabIndex = 6;
            this.btmOutLogin.Text = "退出登录";
            this.btmOutLogin.UseVisualStyleBackColor = true;
            this.btmOutLogin.Click += new System.EventHandler(this.btmOutLogin_Click);
            // 
            // btnMove
            // 
            this.btnMove.Location = new System.Drawing.Point(380, 24);
            this.btnMove.Name = "btnMove";
            this.btnMove.Size = new System.Drawing.Size(75, 33);
            this.btnMove.TabIndex = 5;
            this.btnMove.Text = "Move视频";
            this.btnMove.UseVisualStyleBackColor = true;
            this.btnMove.Click += new System.EventHandler(this.btnMove_Click);
            // 
            // btnPlayer
            // 
            this.btnPlayer.Location = new System.Drawing.Point(259, 24);
            this.btnPlayer.Name = "btnPlayer";
            this.btnPlayer.Size = new System.Drawing.Size(75, 33);
            this.btnPlayer.TabIndex = 4;
            this.btnPlayer.Text = "播放课时";
            this.btnPlayer.UseVisualStyleBackColor = true;
            this.btnPlayer.Click += new System.EventHandler(this.btnPlayer_Click);
            // 
            // labmsg
            // 
            this.labmsg.AutoSize = true;
            this.labmsg.Location = new System.Drawing.Point(37, 64);
            this.labmsg.Name = "labmsg";
            this.labmsg.Size = new System.Drawing.Size(29, 12);
            this.labmsg.TabIndex = 3;
            this.labmsg.Text = "    ";
            // 
            // btnChapter
            // 
            this.btnChapter.Location = new System.Drawing.Point(141, 24);
            this.btnChapter.Name = "btnChapter";
            this.btnChapter.Size = new System.Drawing.Size(75, 33);
            this.btnChapter.TabIndex = 2;
            this.btnChapter.Text = "获取章节";
            this.btnChapter.UseVisualStyleBackColor = true;
            this.btnChapter.Click += new System.EventHandler(this.btnChapter_Click);
            // 
            // btnLogin
            // 
            this.btnLogin.Location = new System.Drawing.Point(35, 24);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(75, 33);
            this.btnLogin.TabIndex = 0;
            this.btnLogin.Text = "登 录";
            this.btnLogin.UseVisualStyleBackColor = true;
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            // 
            // listBoxLeft
            // 
            this.listBoxLeft.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.listBoxLeft.FormattingEnabled = true;
            this.listBoxLeft.Location = new System.Drawing.Point(26, 109);
            this.listBoxLeft.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.listBoxLeft.Name = "listBoxLeft";
            this.listBoxLeft.Size = new System.Drawing.Size(249, 381);
            this.listBoxLeft.TabIndex = 1;
            // 
            // listBoxRight
            // 
            this.listBoxRight.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.listBoxRight.FormattingEnabled = true;
            this.listBoxRight.Location = new System.Drawing.Point(281, 109);
            this.listBoxRight.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.listBoxRight.Name = "listBoxRight";
            this.listBoxRight.ScrollAlwaysVisible = true;
            this.listBoxRight.Size = new System.Drawing.Size(648, 381);
            this.listBoxRight.TabIndex = 2;
            // 
            // btnExcute
            // 
            this.btnExcute.Location = new System.Drawing.Point(494, 24);
            this.btnExcute.Name = "btnExcute";
            this.btnExcute.Size = new System.Drawing.Size(75, 33);
            this.btnExcute.TabIndex = 8;
            this.btnExcute.Text = "开始执行";
            this.btnExcute.UseVisualStyleBackColor = true;
            this.btnExcute.Click += new System.EventHandler(this.btnExcute_Click);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(955, 501);
            this.Controls.Add(this.listBoxRight);
            this.Controls.Add(this.listBoxLeft);
            this.Controls.Add(this.groupBox1);
            this.MaximizeBox = false;
            this.Name = "Main";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "恒企网校课程下载";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnLogin;
        private System.Windows.Forms.ListBox listBoxLeft;
        private System.Windows.Forms.ListBox listBoxRight;
        private System.Windows.Forms.Button btnChapter;
        private System.Windows.Forms.Label labmsg;
        private System.Windows.Forms.Button btnPlayer;
        private System.Windows.Forms.Button btnMove;
        private System.Windows.Forms.Button btmOutLogin;
        private System.Windows.Forms.Label labelMsg;
        private System.Windows.Forms.Button btnExcute;
    }
}

