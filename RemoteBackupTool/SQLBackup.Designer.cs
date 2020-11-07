namespace RemoteBackupTool
{
    partial class SQLBackup
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
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
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SQLBackup));
            this.afpLabelProvider1 = new AfpComponents.AfpLabelProvider();
            this.tbServerAddress = new System.Windows.Forms.TextBox();
            this.btnCloseApplication = new System.Windows.Forms.Button();
            this.btnDoBackup = new System.Windows.Forms.Button();
            this.tbDBaseName = new System.Windows.Forms.TextBox();
            this.tbTempDirOnServerSide = new System.Windows.Forms.TextBox();
            this.tbDirOnLocalSide = new System.Windows.Forms.TextBox();
            this.tbUser = new System.Windows.Forms.TextBox();
            this.tbPass = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.antiFreezer = new System.ComponentModel.BackgroundWorker();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tbServerAddress
            // 
            this.afpLabelProvider1.SetAfpLabel(this.tbServerAddress, "SQLServer 服务器:");
            this.tbServerAddress.Location = new System.Drawing.Point(205, 35);
            this.tbServerAddress.Name = "tbServerAddress";
            this.tbServerAddress.ReadOnly = true;
            this.tbServerAddress.Size = new System.Drawing.Size(157, 21);
            this.tbServerAddress.TabIndex = 0;
            // 
            // btnCloseApplication
            // 
            this.afpLabelProvider1.SetAfpLabel(this.btnCloseApplication, "");
            this.btnCloseApplication.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCloseApplication.Location = new System.Drawing.Point(368, 197);
            this.btnCloseApplication.Name = "btnCloseApplication";
            this.btnCloseApplication.Size = new System.Drawing.Size(89, 21);
            this.btnCloseApplication.TabIndex = 7;
            this.btnCloseApplication.Text = "退  出";
            this.btnCloseApplication.UseVisualStyleBackColor = true;
            this.btnCloseApplication.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnDoBackup
            // 
            this.afpLabelProvider1.SetAfpLabel(this.btnDoBackup, "");
            this.btnDoBackup.Location = new System.Drawing.Point(273, 197);
            this.btnDoBackup.Name = "btnDoBackup";
            this.btnDoBackup.Size = new System.Drawing.Size(89, 21);
            this.btnDoBackup.TabIndex = 6;
            this.btnDoBackup.Text = "开始备份";
            this.btnDoBackup.UseVisualStyleBackColor = true;
            this.btnDoBackup.Click += new System.EventHandler(this.btnDoBackup_Click);
            // 
            // tbDBaseName
            // 
            this.afpLabelProvider1.SetAfpLabel(this.tbDBaseName, "数据库名称:");
            this.tbDBaseName.Location = new System.Drawing.Point(205, 59);
            this.tbDBaseName.Name = "tbDBaseName";
            this.tbDBaseName.ReadOnly = true;
            this.tbDBaseName.Size = new System.Drawing.Size(157, 21);
            this.tbDBaseName.TabIndex = 1;
            // 
            // tbTempDirOnServerSide
            // 
            this.afpLabelProvider1.SetAfpLabel(this.tbTempDirOnServerSide, "备份原始目录:");
            this.tbTempDirOnServerSide.Location = new System.Drawing.Point(205, 131);
            this.tbTempDirOnServerSide.Name = "tbTempDirOnServerSide";
            this.tbTempDirOnServerSide.ReadOnly = true;
            this.tbTempDirOnServerSide.Size = new System.Drawing.Size(157, 21);
            this.tbTempDirOnServerSide.TabIndex = 4;
            // 
            // tbDirOnLocalSide
            // 
            this.afpLabelProvider1.SetAfpLabel(this.tbDirOnLocalSide, "备份目录:");
            this.tbDirOnLocalSide.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.tbDirOnLocalSide.Location = new System.Drawing.Point(205, 155);
            this.tbDirOnLocalSide.Name = "tbDirOnLocalSide";
            this.tbDirOnLocalSide.ReadOnly = true;
            this.tbDirOnLocalSide.Size = new System.Drawing.Size(157, 21);
            this.tbDirOnLocalSide.TabIndex = 5;
            // 
            // tbUser
            // 
            this.afpLabelProvider1.SetAfpLabel(this.tbUser, "SQL 用户:");
            this.tbUser.Location = new System.Drawing.Point(205, 83);
            this.tbUser.Name = "tbUser";
            this.tbUser.ReadOnly = true;
            this.tbUser.Size = new System.Drawing.Size(157, 21);
            this.tbUser.TabIndex = 2;
            // 
            // tbPass
            // 
            this.afpLabelProvider1.SetAfpLabel(this.tbPass, "SQL 用户口令:");
            this.tbPass.Location = new System.Drawing.Point(205, 107);
            this.tbPass.Name = "tbPass";
            this.tbPass.PasswordChar = '*';
            this.tbPass.ReadOnly = true;
            this.tbPass.Size = new System.Drawing.Size(157, 21);
            this.tbPass.TabIndex = 3;
            // 
            // button1
            // 
            this.afpLabelProvider1.SetAfpLabel(this.button1, "");
            this.button1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button1.BackgroundImage")));
            this.button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button1.Location = new System.Drawing.Point(368, 155);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(23, 21);
            this.button1.TabIndex = 10;
            this.button1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // radioButton1
            // 
            this.afpLabelProvider1.SetAfpLabel(this.radioButton1, "");
            this.radioButton1.AutoSize = true;
            this.radioButton1.Checked = true;
            this.radioButton1.Location = new System.Drawing.Point(6, 11);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(47, 16);
            this.radioButton1.TabIndex = 11;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "本地";
            this.radioButton1.UseVisualStyleBackColor = true;
            // 
            // radioButton2
            // 
            this.afpLabelProvider1.SetAfpLabel(this.radioButton2, "");
            this.radioButton2.AutoSize = true;
            this.radioButton2.Location = new System.Drawing.Point(60, 11);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(47, 16);
            this.radioButton2.TabIndex = 12;
            this.radioButton2.Text = "远程";
            this.radioButton2.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.afpLabelProvider1.SetAfpLabel(this.groupBox1, "");
            this.groupBox1.Controls.Add(this.radioButton1);
            this.groupBox1.Controls.Add(this.radioButton2);
            this.groupBox1.Location = new System.Drawing.Point(86, 191);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(115, 32);
            this.groupBox1.TabIndex = 13;
            this.groupBox1.TabStop = false;
            // 
            // antiFreezer
            // 
            this.antiFreezer.WorkerReportsProgress = true;
            this.antiFreezer.DoWork += new System.ComponentModel.DoWorkEventHandler(this.antiFreezer_DoWork);
            this.antiFreezer.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.antiFreezer_RunWorkerCompleted);
            // 
            // SQLBackup
            // 
            this.afpLabelProvider1.SetAfpLabel(this, "");
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(520, 252);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnDoBackup);
            this.Controls.Add(this.tbPass);
            this.Controls.Add(this.tbUser);
            this.Controls.Add(this.tbDirOnLocalSide);
            this.Controls.Add(this.tbTempDirOnServerSide);
            this.Controls.Add(this.tbDBaseName);
            this.Controls.Add(this.btnCloseApplication);
            this.Controls.Add(this.tbServerAddress);
            this.Name = "SQLBackup";
            this.Text = "数据备份";
            this.Load += new System.EventHandler(this.SQLBackup_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private AfpComponents.AfpLabelProvider afpLabelProvider1;
		private System.Windows.Forms.TextBox tbServerAddress;
		private System.Windows.Forms.Button btnCloseApplication;
		private System.Windows.Forms.Button btnDoBackup;
		private System.Windows.Forms.TextBox tbDBaseName;
		private System.Windows.Forms.TextBox tbTempDirOnServerSide;
		private System.Windows.Forms.TextBox tbDirOnLocalSide;
		private System.Windows.Forms.TextBox tbUser;
		private System.Windows.Forms.TextBox tbPass;
		private System.ComponentModel.BackgroundWorker antiFreezer;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.GroupBox groupBox1;
	}
}

