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
    public partial class ChangeValue : Form
         

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

        private string changeMode = "string";

        public string ChangeMode
        {
            get { return changeMode; }
            set { changeMode = value; }
        }
        
        
        public ChangeValue()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (this.changeMode != "datetime")
            {
                if (this.changeMode != "string" && this.changeMode != "longstring")
                {
                    if (!(Regex.IsMatch(this.textBox2.Text, @"^([-]{1}[0-9]+(.[0-9]{3})?$)|([0-9]+(.[0-9]{3})?$)"))) // ^\+?[1-9][0-9]*$
                    {
                        MessageBox.Show("数字格式错误，请重新输入");
                        this.textBox2.Focus();
                        return;

                    }
                }

                NewValue = this.textBox2.Text;
            }
            else
            {
                NewValue = this.dateTimePicker1.Text;
            }

            this.Close();
        }
        public void setInFocus()
        {
            this.textBox2.Focus();
        }
        private void ChangeValue_Load(object sender, EventArgs e)
        {
            this.Activate();
            
            this.textBox1.Text = OldValue;
            this.textBox2.Text = NewValue;
            this.textBox2.Focus();
            if (this.changeMode == "datetime")
            {
               
                this.textBox2.Visible = false;
                this.dateTimePicker1.Visible = true;
                this.dateTimePicker1.CustomFormat = "yyyy-MM-dd HH:mm:ss";
                this.dateTimePicker1.Text  = OldValue;
            
            }

            if (this.changeMode == "longstring")
            {
                this.textBox1.Visible = false ;
                this.textBox2.Visible = true ;
                this.label1.Visible = false;
                this.label2.Visible = false;
                this.dateTimePicker1.Visible = false ;

                this.Height =280;

                this.textBox2.Location = new Point(10, 10);
                
                this.textBox2.Width  = 390;
                this.textBox2.Height = 181;

                this.textBox2.Multiline = true;

                this .button1 .Location = new Point (300,200);

                this.textBox2.KeyPress -= textBox2_KeyPress;

                this.textBox2.Text = OldValue;



            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar.ToString() == "\r")
            {
                e.Handled = true;
                if (this.changeMode != "string")
                {
                    if (!(Regex.IsMatch(this.textBox2.Text, @"^([-]{1}[0-9]+(.[0-9]{2})?$)|([0-9]+(.[0-9]{2})?$)"))) // ^\+?[1-9][0-9]*$
                    {
                        MessageBox.Show("数字格式错误，请重新输入");
                        this.textBox2.Focus();
                        return;

                    }
                }

                NewValue = this.textBox2.Text;

                this.Close();
            }
        }

        private void ChangeValue_Activated(object sender, EventArgs e)
        {
            this.textBox2.Focus();
        }

        //private void ChangeValue_FormClosed(object sender, FormClosedEventArgs e)
        //{
        //    //NewValue ="";
        //}
    }
}
