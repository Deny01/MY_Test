namespace HappyYF.YuanXin.WorkSet
{
    partial class LY_pay_money
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
            System.Windows.Forms.Label label2;
            System.Windows.Forms.Label label3;
            System.Windows.Forms.Label label5;
            System.Windows.Forms.Label label1;
            System.Windows.Forms.Label label4;
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.txt_remark = new System.Windows.Forms.TextBox();
            this.contract_pay_money = new System.Windows.Forms.TextBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.contract_code = new System.Windows.Forms.TextBox();
            label2 = new System.Windows.Forms.Label();
            label3 = new System.Windows.Forms.Label();
            label5 = new System.Windows.Forms.Label();
            label1 = new System.Windows.Forms.Label();
            label4 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new System.Drawing.Point(88, 60);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(59, 12);
            label2.TabIndex = 11;
            label2.Text = "申请金额:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new System.Drawing.Point(107, 105);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(35, 12);
            label3.TabIndex = 12;
            label3.Text = "备注:";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.ForeColor = System.Drawing.Color.Red;
            label5.Location = new System.Drawing.Point(71, 57);
            label5.Name = "label5";
            label5.Size = new System.Drawing.Size(11, 12);
            label5.TabIndex = 16;
            label5.Text = "*";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.ForeColor = System.Drawing.Color.Red;
            label1.Location = new System.Drawing.Point(71, 19);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(11, 12);
            label1.TabIndex = 19;
            label1.Text = "*";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new System.Drawing.Point(88, 22);
            label4.Name = "label4";
            label4.Size = new System.Drawing.Size(59, 12);
            label4.TabIndex = 18;
            label4.Text = "合同编号:";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(248, 22);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(84, 25);
            this.button1.TabIndex = 6;
            this.button1.Text = "确定";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button2.Location = new System.Drawing.Point(341, 22);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(84, 25);
            this.button2.TabIndex = 7;
            this.button2.Text = "取消";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // txt_remark
            // 
            this.txt_remark.Location = new System.Drawing.Point(152, 102);
            this.txt_remark.Name = "txt_remark";
            this.txt_remark.Size = new System.Drawing.Size(200, 21);
            this.txt_remark.TabIndex = 4;
            // 
            // contract_pay_money
            // 
            this.contract_pay_money.BackColor = System.Drawing.Color.White;
            this.contract_pay_money.ForeColor = System.Drawing.SystemColors.WindowText;
            this.contract_pay_money.Location = new System.Drawing.Point(153, 57);
            this.contract_pay_money.Name = "contract_pay_money";
            this.contract_pay_money.Size = new System.Drawing.Size(200, 21);
            this.contract_pay_money.TabIndex = 3;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(label1);
            this.splitContainer1.Panel1.Controls.Add(label4);
            this.splitContainer1.Panel1.Controls.Add(this.contract_code);
            this.splitContainer1.Panel1.Controls.Add(label5);
            this.splitContainer1.Panel1.Controls.Add(this.txt_remark);
            this.splitContainer1.Panel1.Controls.Add(label2);
            this.splitContainer1.Panel1.Controls.Add(label3);
            this.splitContainer1.Panel1.Controls.Add(this.contract_pay_money);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.button2);
            this.splitContainer1.Panel2.Controls.Add(this.button1);
            this.splitContainer1.Size = new System.Drawing.Size(454, 217);
            this.splitContainer1.SplitterDistance = 154;
            this.splitContainer1.TabIndex = 53;
            // 
            // contract_code
            // 
            this.contract_code.BackColor = System.Drawing.Color.White;
            this.contract_code.ForeColor = System.Drawing.SystemColors.WindowText;
            this.contract_code.Location = new System.Drawing.Point(153, 19);
            this.contract_code.Name = "contract_code";
            this.contract_code.ReadOnly = true;
            this.contract_code.Size = new System.Drawing.Size(200, 21);
            this.contract_code.TabIndex = 17;
            // 
            // LY_pay_money
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(454, 217);
            this.Controls.Add(this.splitContainer1);
            this.Name = "LY_pay_money";
            this.Text = "申请预付";
            this.Load += new System.EventHandler(this.LY_pay_money_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox contract_pay_money;
        private System.Windows.Forms.TextBox txt_remark;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TextBox contract_code;
    }
}