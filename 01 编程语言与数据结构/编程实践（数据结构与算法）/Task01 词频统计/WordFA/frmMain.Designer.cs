namespace WordFA
{
    partial class FrmMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMain));
            this.rdoSigle = new System.Windows.Forms.RadioButton();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblInfor = new System.Windows.Forms.Label();
            this.btnstatic = new System.Windows.Forms.Button();
            this.btnDire = new System.Windows.Forms.Button();
            this.btnPath = new System.Windows.Forms.Button();
            this.btnfile = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnClose = new System.Windows.Forms.Button();
            this.txtdirc = new System.Windows.Forms.Label();
            this.txtPath = new System.Windows.Forms.Label();
            this.txtFile = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // rdoSigle
            // 
            this.rdoSigle.AutoSize = true;
            this.rdoSigle.Checked = true;
            this.rdoSigle.Location = new System.Drawing.Point(17, 20);
            this.rdoSigle.Name = "rdoSigle";
            this.rdoSigle.Size = new System.Drawing.Size(83, 16);
            this.rdoSigle.TabIndex = 13;
            this.rdoSigle.TabStop = true;
            this.rdoSigle.Text = "单文件统计";
            this.rdoSigle.UseVisualStyleBackColor = true;
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Location = new System.Drawing.Point(17, 42);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(83, 16);
            this.radioButton2.TabIndex = 14;
            this.radioButton2.Text = "多文件统计";
            this.radioButton2.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rdoSigle);
            this.groupBox1.Controls.Add(this.radioButton2);
            this.groupBox1.Location = new System.Drawing.Point(446, 180);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(161, 70);
            this.groupBox1.TabIndex = 15;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "文件选项";
            // 
            // lblInfor
            // 
            this.lblInfor.AutoSize = true;
            this.lblInfor.BackColor = System.Drawing.Color.Transparent;
            this.lblInfor.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblInfor.ForeColor = System.Drawing.Color.Red;
            this.lblInfor.Location = new System.Drawing.Point(6, 28);
            this.lblInfor.Name = "lblInfor";
            this.lblInfor.Size = new System.Drawing.Size(51, 19);
            this.lblInfor.TabIndex = 17;
            this.lblInfor.Text = "label1";
            // 
            // btnstatic
            // 
            this.btnstatic.Location = new System.Drawing.Point(446, 271);
            this.btnstatic.Name = "btnstatic";
            this.btnstatic.Size = new System.Drawing.Size(90, 23);
            this.btnstatic.TabIndex = 18;
            this.btnstatic.Text = "开始统计(&S)";
            this.btnstatic.UseVisualStyleBackColor = true;
            this.btnstatic.Click += new System.EventHandler(this.btnstatic_Click);
            // 
            // btnDire
            // 
            this.btnDire.Location = new System.Drawing.Point(567, 137);
            this.btnDire.Name = "btnDire";
            this.btnDire.Size = new System.Drawing.Size(40, 23);
            this.btnDire.TabIndex = 20;
            this.btnDire.Text = "...";
            this.btnDire.UseVisualStyleBackColor = true;
            this.btnDire.Click += new System.EventHandler(this.btnDire_Click);
            // 
            // btnPath
            // 
            this.btnPath.Location = new System.Drawing.Point(567, 83);
            this.btnPath.Name = "btnPath";
            this.btnPath.Size = new System.Drawing.Size(40, 23);
            this.btnPath.TabIndex = 21;
            this.btnPath.Text = "...";
            this.btnPath.UseVisualStyleBackColor = true;
            this.btnPath.Click += new System.EventHandler(this.btnPath_Click);
            // 
            // btnfile
            // 
            this.btnfile.Location = new System.Drawing.Point(567, 36);
            this.btnfile.Name = "btnfile";
            this.btnfile.Size = new System.Drawing.Size(40, 23);
            this.btnfile.TabIndex = 22;
            this.btnfile.Text = "...";
            this.btnfile.UseVisualStyleBackColor = true;
            this.btnfile.Click += new System.EventHandler(this.btnfile_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 12);
            this.label1.TabIndex = 23;
            this.label1.Text = "待分析文件：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 68);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 24;
            this.label2.Text = "文件路径：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(10, 121);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 25;
            this.label3.Text = "字典文件：";
            // 
            // groupBox2
            // 
            this.groupBox2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.groupBox2.Controls.Add(this.lblInfor);
            this.groupBox2.Location = new System.Drawing.Point(12, 180);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(416, 365);
            this.groupBox2.TabIndex = 16;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "统计结果";
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(446, 309);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(90, 23);
            this.btnClose.TabIndex = 26;
            this.btnClose.Text = "退出软件(&Q)";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // txtdirc
            // 
            this.txtdirc.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtdirc.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.txtdirc.Location = new System.Drawing.Point(10, 137);
            this.txtdirc.Name = "txtdirc";
            this.txtdirc.Size = new System.Drawing.Size(549, 21);
            this.txtdirc.TabIndex = 18;
            this.txtdirc.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtPath
            // 
            this.txtPath.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPath.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.txtPath.Location = new System.Drawing.Point(10, 83);
            this.txtPath.Name = "txtPath";
            this.txtPath.Size = new System.Drawing.Size(549, 21);
            this.txtPath.TabIndex = 27;
            this.txtPath.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtFile
            // 
            this.txtFile.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtFile.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.txtFile.Location = new System.Drawing.Point(10, 36);
            this.txtFile.Name = "txtFile";
            this.txtFile.Size = new System.Drawing.Size(549, 21);
            this.txtFile.TabIndex = 28;
            this.txtFile.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(658, 557);
            this.Controls.Add(this.txtFile);
            this.Controls.Add(this.txtPath);
            this.Controls.Add(this.txtdirc);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnfile);
            this.Controls.Add(this.btnPath);
            this.Controls.Add(this.btnDire);
            this.Controls.Add(this.btnstatic);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "词频统计小工具";
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.RadioButton rdoSigle;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lblInfor;
        private System.Windows.Forms.Button btnstatic;
        private System.Windows.Forms.Button btnDire;
        private System.Windows.Forms.Button btnPath;
        private System.Windows.Forms.Button btnfile;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Label txtdirc;
        private System.Windows.Forms.Label txtPath;
        private System.Windows.Forms.Label txtFile;
    }
}

