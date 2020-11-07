using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Management;
using TGZJ.Base;


namespace TGZJ
{
    public partial class RegisterCode : Form
    {
        public RegisterCode()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Simple3Des jm = new Simple3Des("deny01.12345678901234567890zb");

            System.Management.ManagementClass mcpu = new ManagementClass("win32_processor");
            ManagementObjectCollection moc = mcpu.GetInstances();

            string scpuCode = "";

            foreach (ManagementObject mo in moc)
            {
                //MessageBox.Show(mo["processorid"].ToString());
                scpuCode = scpuCode + mo["processorid"].ToString();

            }


            if (jm.EncryptData (scpuCode) == this.textBox1.Text)
            {
                Mydataconn mc = Mydataconn.Load();

                if (null == mc)
                {
                    mc = new Mydataconn();
                }

                mc.RegisterCode = this.textBox1.Text;

                mc.Save();

                this.DialogResult = DialogResult.OK;
            }
            else
            {
                MessageBox.Show("注册码错误，请重新输入...");
            
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            System.Management.ManagementClass mc = new ManagementClass("win32_processor");
            ManagementObjectCollection moc = mc.GetInstances();

            this.textBox1.Text = "";
            string cpuId="";
            //this.textBox2.Text = "";

            foreach (ManagementObject mo in moc)
            {
                //MessageBox.Show(mo["processorid"].ToString());
                cpuId = cpuId + mo["processorid"].ToString();

            }

            Simple3Des jm = new Simple3Des(this.textBox2.Text);

            this.textBox1.Text = jm.EncryptData(cpuId);
            this.textBox3.Text = cpuId;
            
            
        }
    }
}
