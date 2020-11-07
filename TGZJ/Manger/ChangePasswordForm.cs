using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TGZJ.Base;


namespace TGZJ.Manger
{
    public partial class ChangePasswordForm : Form
    {

        Now_Client nowClient;

        public ChangePasswordForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (this.textBox1.Text != nowClient.Pwd )
            {
                MessageBox.Show( "原密码错误" );
                return;
            
            }

            if (this.textBox2.Text.Replace (" ","").Length < 1)
            {
                MessageBox.Show("新密码不能空");
                return;

            }

            if (this.textBox2.Text != this.textBox3.Text)
            {
                MessageBox.Show("新密码和确认密码不一致");
                return;

            }

            nowClient.Pwd = this.textBox2.Text;

            if (nowClient.Update_client())
            
                this.label7.Text = "密码修改成功！";
            
            else
                this.label7.Text = "密码修改失败，请联系管理员";
        }

        private void ChangePasswordForm_Load(object sender, EventArgs e)
        {
            nowClient = new Now_Client(Program.nowUser.Name );

            this.label2.Text = nowClient.Yhbm ;
            this.label3.Text = nowClient.Yhmc ;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
                this.button1.Enabled = true;
            else
                this.button1.Enabled = false;
        }
    }
}
