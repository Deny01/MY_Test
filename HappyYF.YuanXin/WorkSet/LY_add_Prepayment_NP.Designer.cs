namespace HappyYF.YuanXin.WorkSet
{
    partial class LY_add_Prepayment_NP
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
            System.Windows.Forms.Label label1;
            System.Windows.Forms.Label label2;
            System.Windows.Forms.Label label8;
            System.Windows.Forms.Label label12;
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.invoice_code = new System.Windows.Forms.TextBox();
            this.invoice_money = new System.Windows.Forms.TextBox();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.supplier_financial_code = new System.Windows.Forms.TextBox();
            label1 = new System.Windows.Forms.Label();
            label2 = new System.Windows.Forms.Label();
            label8 = new System.Windows.Forms.Label();
            label12 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(48, 80);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(59, 12);
            label1.TabIndex = 10;
            label1.Text = "预付金额:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new System.Drawing.Point(48, 131);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(35, 12);
            label2.TabIndex = 11;
            label2.Text = "备注:";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new System.Drawing.Point(48, 50);
            label8.Name = "label8";
            label8.Size = new System.Drawing.Size(59, 12);
            label8.TabIndex = 52;
            label8.Text = "支付日期:";
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Location = new System.Drawing.Point(48, 106);
            label12.Name = "label12";
            label12.Size = new System.Drawing.Size(47, 12);
            label12.TabIndex = 58;
            label12.Text = "支付人:";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(135, 199);
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
            this.button2.Location = new System.Drawing.Point(250, 199);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(63, 26);
            this.button2.TabIndex = 7;
            this.button2.Text = "取消";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // invoice_code
            // 
            this.invoice_code.BackColor = System.Drawing.Color.White;
            this.invoice_code.ForeColor = System.Drawing.Color.Black;
            this.invoice_code.Location = new System.Drawing.Point(113, 127);
            this.invoice_code.Multiline = true;
            this.invoice_code.Name = "invoice_code";
            this.invoice_code.Size = new System.Drawing.Size(200, 57);
            this.invoice_code.TabIndex = 2;
            // 
            // invoice_money
            // 
            this.invoice_money.BackColor = System.Drawing.Color.White;
            this.invoice_money.ForeColor = System.Drawing.Color.Black;
            this.invoice_money.Location = new System.Drawing.Point(113, 73);
            this.invoice_money.Name = "invoice_money";
            this.invoice_money.Size = new System.Drawing.Size(200, 21);
            this.invoice_money.TabIndex = 3;
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.CustomFormat = "yyyy-MM-dd";
            this.dateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePicker1.Location = new System.Drawing.Point(113, 46);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(92, 21);
            this.dateTimePicker1.TabIndex = 51;
            // 
            // supplier_financial_code
            // 
            this.supplier_financial_code.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.supplier_financial_code.ForeColor = System.Drawing.Color.White;
            this.supplier_financial_code.Location = new System.Drawing.Point(113, 100);
            this.supplier_financial_code.Name = "supplier_financial_code";
            this.supplier_financial_code.ReadOnly = true;
            this.supplier_financial_code.Size = new System.Drawing.Size(200, 21);
            this.supplier_financial_code.TabIndex = 57;
            // 
            // LY_add_Prepayment_NP
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(382, 271);
            this.Controls.Add(label12);
            this.Controls.Add(this.supplier_financial_code);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.button2);
            this.Controls.Add(label8);
            this.Controls.Add(this.dateTimePicker1);
            this.Controls.Add(this.invoice_code);
            this.Controls.Add(label1);
            this.Controls.Add(this.invoice_money);
            this.Controls.Add(label2);
            this.Name = "LY_add_Prepayment_NP";
            this.Text = "非生产采购预付增加";
            this.Load += new System.EventHandler(this.LY_add_Prepayment_NP_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox invoice_code;
        private System.Windows.Forms.TextBox invoice_money;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.TextBox supplier_financial_code;
    }
}