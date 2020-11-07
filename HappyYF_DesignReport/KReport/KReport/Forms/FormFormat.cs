namespace KReport.Forms
{
    using KReport.Engine;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    internal class FormFormat : Form
    {
        private static CustomControl _control;
        private Button btnCancel;
        private Button btnOk;
        private IContainer components = null;
        private ListBox listBoxformat;
        private TextBox textBox1;

        private FormFormat()
        {
            this.InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void FillFormatedList()
        {
            switch (_control.Type)
            {
            }
        }

        private void FormatDate()
        {
            this.listBoxformat.Items.Clear();
            this.listBoxformat.Items.Add("03/09/03         dd/mm/yy");
            this.listBoxformat.Items.Add("03/09/2003       dd/mm/yyyy");
            this.listBoxformat.Items.Add("03/Setembro/2003 dd/mmm/yyyy");
        }

        private void InitializeComponent()
        {
            this.listBoxformat = new System.Windows.Forms.ListBox();
            this.btnOk = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // listBoxformat
            // 
            this.listBoxformat.ItemHeight = 12;
            this.listBoxformat.Items.AddRange(new object[] {
            "adfasfsafsadf",
            "asdfasdf",
            "asdfadfasd",
            "fasf",
            "dsaf",
            "dsaf",
            "dsaf",
            "dsaf",
            "dsaf",
            "asd",
            "fasd",
            "fasdfdsaf",
            "saf",
            "asd",
            "fasdf"});
            this.listBoxformat.Location = new System.Drawing.Point(3, 29);
            this.listBoxformat.Name = "listBoxformat";
            this.listBoxformat.Size = new System.Drawing.Size(247, 172);
            this.listBoxformat.TabIndex = 0;
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(88, 208);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 1;
            this.btnOk.Text = "确定";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.button1_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(3, 3);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(247, 21);
            this.textBox1.TabIndex = 2;
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(169, 208);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // FormFormat
            // 
            this.ClientSize = new System.Drawing.Size(256, 234);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.listBoxformat);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormFormat";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "格式化串";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        public static DialogResult ShowDialog(CustomControl value)
        {
            _control = value;
            return new FormFormat().ShowDialog();
        }
    }
}

