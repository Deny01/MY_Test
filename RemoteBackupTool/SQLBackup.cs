using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HappyYF.Infrastructure.Repositories;

namespace RemoteBackupTool
{
	public partial class SQLBackup : Form
	{
		protected SQLLocalBackup _backupClass; // our backup class located in SQLLocalBackup.cs

        private string serverName;

        public string ServerName
        {
            get { return serverName; }
            set { serverName = value; }
        }
        private string dataBaseName;

        public string DataBaseName
        {
            get { return dataBaseName; }
            set { dataBaseName = value; }
        }
        private string userName;

        public string UserName
        {
            get { return userName; }
            set { userName = value; }
        }
        private string passWord;

        public string PassWord
        {
            get { return passWord; }
            set { passWord = value; }
        }

        public SQLBackup()
		{
			InitializeComponent();
		}

		private void button1_Click(object sender, EventArgs e)
		{
			Close ();
		}

		private void btnDoBackup_Click(object sender, EventArgs e)
		{
            if (1 > this.tbDirOnLocalSide.Text.Length)
            {
                MessageBox.Show("请选择保存目录......");

                return;

            
            
            }
            
            antiFreezer.RunWorkerAsync();
		}

		private void antiFreezer_DoWork(object sender, DoWorkEventArgs e)
		{
			_backupClass = new SQLLocalBackup(tbServerAddress.Text, tbUser.Text, tbPass.Text, tbDBaseName.Text);

            if (radioButton1.Checked )
                 _backupClass.DoLocalBackup(tbTempDirOnServerSide.Text, tbDirOnLocalSide.Text, "Local");
            else
                _backupClass.DoLocalBackup(tbTempDirOnServerSide.Text, tbDirOnLocalSide.Text, "Remote");
		}

		private void antiFreezer_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			MessageBox.Show("备份完成......");
		}

        private void SQLBackup_Load(object sender, EventArgs e)
        {
            this.tbServerAddress.Text = this.serverName;
            this.tbDBaseName.Text = this.dataBaseName ;
            this.tbUser.Text = this.userName ;
            this.tbPass.Text = this.passWord ;

            this.tbTempDirOnServerSide.Text = "c:\\";
            //this.tbDirOnLocalSide.Text = System.Environment.CurrentDirectory;
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            folderdialog aa = new folderdialog();
            
            
            aa.displaydialog();

            this.tbDirOnLocalSide.Text = aa.path;
            
            //MessageBox.Show (aa.path);  

        }
	}
}
