namespace HappyYF.YuanXin.WorkSet
{
    partial class LY_add_paymoeny
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
            System.Windows.Forms.Label 客户编码Label;
            System.Windows.Forms.Label label1;
            System.Windows.Forms.Label label4;
            System.Windows.Forms.Label label6;
            System.Windows.Forms.Label label8;
            System.Windows.Forms.Label label3;
            System.Windows.Forms.Label label10;
            System.Windows.Forms.Label label11;
            System.Windows.Forms.Label label12;
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.client_code = new System.Windows.Forms.TextBox();
            this.invoice_money = new System.Windows.Forms.TextBox();
            this.client_name = new System.Windows.Forms.TextBox();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.textBox1 = new System.Windows.Forms.TextBox();
            客户编码Label = new System.Windows.Forms.Label();
            label1 = new System.Windows.Forms.Label();
            label4 = new System.Windows.Forms.Label();
            label6 = new System.Windows.Forms.Label();
            label8 = new System.Windows.Forms.Label();
            label3 = new System.Windows.Forms.Label();
            label10 = new System.Windows.Forms.Label();
            label11 = new System.Windows.Forms.Label();
            label12 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // 客户编码Label
            // 
            客户编码Label.AutoSize = true;
            客户编码Label.Location = new System.Drawing.Point(94, 51);
            客户编码Label.Name = "客户编码Label";
            客户编码Label.Size = new System.Drawing.Size(59, 12);
            客户编码Label.TabIndex = 9;
            客户编码Label.Text = "客户编码:";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(94, 134);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(59, 12);
            label1.TabIndex = 10;
            label1.Text = "到款金额:";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.ForeColor = System.Drawing.Color.Red;
            label4.Location = new System.Drawing.Point(77, 51);
            label4.Name = "label4";
            label4.Size = new System.Drawing.Size(11, 12);
            label4.TabIndex = 15;
            label4.Text = "*";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new System.Drawing.Point(94, 84);
            label6.Name = "label6";
            label6.Size = new System.Drawing.Size(59, 12);
            label6.TabIndex = 17;
            label6.Text = "客户名称:";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new System.Drawing.Point(94, 177);
            label8.Name = "label8";
            label8.Size = new System.Drawing.Size(59, 12);
            label8.TabIndex = 52;
            label8.Text = "到款日期:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.ForeColor = System.Drawing.Color.Red;
            label3.Location = new System.Drawing.Point(77, 84);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(11, 12);
            label3.TabIndex = 53;
            label3.Text = "*";
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.ForeColor = System.Drawing.Color.Red;
            label10.Location = new System.Drawing.Point(77, 134);
            label10.Name = "label10";
            label10.Size = new System.Drawing.Size(11, 12);
            label10.TabIndex = 55;
            label10.Text = "*";
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.ForeColor = System.Drawing.Color.Red;
            label11.Location = new System.Drawing.Point(77, 178);
            label11.Name = "label11";
            label11.Size = new System.Drawing.Size(11, 12);
            label11.TabIndex = 56;
            label11.Text = "*";
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Location = new System.Drawing.Point(94, 236);
            label12.Name = "label12";
            label12.Size = new System.Drawing.Size(35, 12);
            label12.TabIndex = 62;
            label12.Text = "备注:";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(140, 293);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(94, 26);
            this.button1.TabIndex = 6;
            this.button1.Text = "确定";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button2.Location = new System.Drawing.Point(296, 293);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(63, 26);
            this.button2.TabIndex = 7;
            this.button2.Text = "取消";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // client_code
            // 
            this.client_code.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.client_code.ForeColor = System.Drawing.Color.White;
            this.client_code.Location = new System.Drawing.Point(159, 48);
            this.client_code.Name = "client_code";
            this.client_code.ReadOnly = true;
            this.client_code.Size = new System.Drawing.Size(200, 21);
            this.client_code.TabIndex = 1;
            // 
            // invoice_money
            // 
            this.invoice_money.BackColor = System.Drawing.Color.White;
            this.invoice_money.ForeColor = System.Drawing.Color.Black;
            this.invoice_money.Location = new System.Drawing.Point(159, 127);
            this.invoice_money.Name = "invoice_money";
            this.invoice_money.Size = new System.Drawing.Size(200, 21);
            this.invoice_money.TabIndex = 3;
            // 
            // client_name
            // 
            this.client_name.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.client_name.ForeColor = System.Drawing.Color.White;
            this.client_name.Location = new System.Drawing.Point(159, 81);
            this.client_name.Name = "client_name";
            this.client_name.ReadOnly = true;
            this.client_name.Size = new System.Drawing.Size(200, 21);
            this.client_name.TabIndex = 18;
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            this.dateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePicker1.Location = new System.Drawing.Point(159, 173);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(200, 21);
            this.dateTimePicker1.TabIndex = 51;
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.Color.White;
            this.textBox1.ForeColor = System.Drawing.Color.Black;
            this.textBox1.Location = new System.Drawing.Point(159, 213);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(200, 51);
            this.textBox1.TabIndex = 61;
            // 
            // LY_add_paymoeny
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(464, 356);
            this.Controls.Add(label12);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.button2);
            this.Controls.Add(label11);
            this.Controls.Add(label10);
            this.Controls.Add(客户编码Label);
            this.Controls.Add(this.client_code);
            this.Controls.Add(label3);
            this.Controls.Add(label8);
            this.Controls.Add(this.dateTimePicker1);
            this.Controls.Add(label1);
            this.Controls.Add(this.invoice_money);
            this.Controls.Add(this.client_name);
            this.Controls.Add(label6);
            this.Controls.Add(label4);
            this.Name = "LY_add_paymoeny";
            this.Text = "增加到款信息";
            this.Load += new System.EventHandler(this.LY_add_paymoeny_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox client_code;
        private System.Windows.Forms.TextBox invoice_money;
        private System.Windows.Forms.TextBox client_name;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.TextBox textBox1;
    }
}