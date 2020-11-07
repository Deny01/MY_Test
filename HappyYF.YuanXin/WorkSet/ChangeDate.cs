using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace HappyYF.YuanXin.WorkSet
{
    public partial class ChangeDate : Form
    {
        private string oldValue;

        public string OldValue
        {
            get { return oldValue; }
            set { oldValue = value; }
        }
        private string newValue;

        public string NewValue
        {
            get { return newValue; }
            set { newValue = value; }
        }
        
        public ChangeDate()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!(Regex.IsMatch(this.textBox2.Text, @"(([0-9]{3}[1-9]|[0-9]{2}[1-9][0-9]{1}|[0-9]{1}[1-9][0-9]{2}|[1-9][0-9]{3})-(((0[13578]|1[02])-(0[1-9]|[12]
[0-9]|3[01]))|((0[469]|11)-(0[1-9]|[12][0-9]|30))|(02-(0[1-9]|[1][0-9]|2[0-8]))))|((([0-9]{2})(0[48]|[2468][048]|[13579]
[26])|((0[48]|[2468][048]|[3579][26])00))-02-29)"))) // ^\+?[1-9][0-9]*$
            {
                MessageBox.Show("日期格式错误，请重新输入");
                this.textBox2.Focus();
                return;

            }

            NewValue = this.textBox2.Text;

            this.Close();
        }

        private void ChangeDate_Load(object sender, EventArgs e)
        {
            this.textBox1.Text = OldValue;
        }
    }
}
