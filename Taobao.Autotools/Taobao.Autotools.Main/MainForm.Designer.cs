namespace Taobao.Autotools.Main
{
    partial class MainForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.tb_Url = new System.Windows.Forms.TextBox();
            this.tb_Search = new System.Windows.Forms.Button();
            this.tb_Content = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(44, 37);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "宝贝地址：";
            // 
            // tb_Url
            // 
            this.tb_Url.Location = new System.Drawing.Point(115, 34);
            this.tb_Url.Name = "tb_Url";
            this.tb_Url.Size = new System.Drawing.Size(468, 21);
            this.tb_Url.TabIndex = 1;
            // 
            // tb_Search
            // 
            this.tb_Search.Location = new System.Drawing.Point(611, 23);
            this.tb_Search.Name = "tb_Search";
            this.tb_Search.Size = new System.Drawing.Size(92, 40);
            this.tb_Search.TabIndex = 2;
            this.tb_Search.Text = "获取商品";
            this.tb_Search.UseVisualStyleBackColor = true;
            this.tb_Search.Click += new System.EventHandler(this.tb_Search_Click);
            // 
            // tb_Content
            // 
            this.tb_Content.Location = new System.Drawing.Point(115, 81);
            this.tb_Content.Multiline = true;
            this.tb_Content.Name = "tb_Content";
            this.tb_Content.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tb_Content.Size = new System.Drawing.Size(588, 338);
            this.tb_Content.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(44, 168);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 4;
            this.label2.Text = "商品参数：";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(741, 439);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tb_Content);
            this.Controls.Add(this.tb_Search);
            this.Controls.Add(this.tb_Url);
            this.Controls.Add(this.label1);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "店铺宝贝发布工具";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tb_Url;
        private System.Windows.Forms.Button tb_Search;
        private System.Windows.Forms.TextBox tb_Content;
        private System.Windows.Forms.Label label2;
    }
}

