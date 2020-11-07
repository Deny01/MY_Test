namespace Project_Manager.UI
{
    partial class CompanySetExtensionUI_Control
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.companyNametextBox = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.button1 = new System.Windows.Forms.Button();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.label7 = new System.Windows.Forms.Label();
            this.auto_postponed_checkBox = new System.Windows.Forms.CheckBox();
            this.use_postponed_checkBox = new System.Windows.Forms.CheckBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.getdayaccrualcomboBox = new System.Windows.Forms.ComboBox();
            this.countdaycomboBox = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.companySimpleNametextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.shapeContainer1 = new Microsoft.VisualBasic.PowerPacks.ShapeContainer();
            this.lineShape4 = new Microsoft.VisualBasic.PowerPacks.LineShape();
            this.lineShape3 = new Microsoft.VisualBasic.PowerPacks.LineShape();
            this.lineShape2 = new Microsoft.VisualBasic.PowerPacks.LineShape();
            this.lineShape1 = new Microsoft.VisualBasic.PowerPacks.LineShape();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // companyNametextBox
            // 
            this.companyNametextBox.Location = new System.Drawing.Point(153, 41);
            this.companyNametextBox.Name = "companyNametextBox";
            this.companyNametextBox.Size = new System.Drawing.Size(191, 21);
            this.companyNametextBox.TabIndex = 1;
            this.companyNametextBox.Leave += new System.EventHandler(this.textBox1_Leave);
            this.companyNametextBox.Enter += new System.EventHandler(this.textBox1_Enter);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.dateTimePicker1);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.auto_postponed_checkBox);
            this.groupBox1.Controls.Add(this.use_postponed_checkBox);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.getdayaccrualcomboBox);
            this.groupBox1.Controls.Add(this.countdaycomboBox);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.companySimpleNametextBox);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.companyNametextBox);
            this.groupBox1.Controls.Add(this.shapeContainer1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(421, 320);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "系统基本参数设置";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(293, 254);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 16;
            this.button1.Text = "计算";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.CustomFormat = "yyyy-MM-dd";
            this.dateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePicker1.Location = new System.Drawing.Point(153, 256);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(121, 21);
            this.dateTimePicker1.TabIndex = 15;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(34, 260);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(83, 12);
            this.label7.TabIndex = 14;
            this.label7.Text = "计算界限日期:";
            // 
            // auto_postponed_checkBox
            // 
            this.auto_postponed_checkBox.AutoSize = true;
            this.auto_postponed_checkBox.Location = new System.Drawing.Point(153, 213);
            this.auto_postponed_checkBox.Name = "auto_postponed_checkBox";
            this.auto_postponed_checkBox.Size = new System.Drawing.Size(15, 14);
            this.auto_postponed_checkBox.TabIndex = 12;
            this.auto_postponed_checkBox.UseVisualStyleBackColor = true;
            this.auto_postponed_checkBox.Visible = false;
            // 
            // use_postponed_checkBox
            // 
            this.use_postponed_checkBox.AutoSize = true;
            this.use_postponed_checkBox.Location = new System.Drawing.Point(153, 190);
            this.use_postponed_checkBox.Name = "use_postponed_checkBox";
            this.use_postponed_checkBox.Size = new System.Drawing.Size(15, 14);
            this.use_postponed_checkBox.TabIndex = 11;
            this.use_postponed_checkBox.UseVisualStyleBackColor = true;
            this.use_postponed_checkBox.Visible = false;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(34, 213);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(107, 12);
            this.label6.TabIndex = 10;
            this.label6.Text = "合同自延自动计算:";
            this.label6.Visible = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(34, 190);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(107, 12);
            this.label5.TabIndex = 9;
            this.label5.Text = "启用合同自延功能:";
            this.label5.Visible = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(34, 139);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(83, 12);
            this.label4.TabIndex = 8;
            this.label4.Text = "日息计算方式:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(34, 113);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(47, 12);
            this.label3.TabIndex = 7;
            this.label3.Text = "结算日:";
            // 
            // getdayaccrualcomboBox
            // 
            this.getdayaccrualcomboBox.FormattingEnabled = true;
            this.getdayaccrualcomboBox.Items.AddRange(new object[] {
            "自然日历",
            "银行标准"});
            this.getdayaccrualcomboBox.Location = new System.Drawing.Point(153, 136);
            this.getdayaccrualcomboBox.Name = "getdayaccrualcomboBox";
            this.getdayaccrualcomboBox.Size = new System.Drawing.Size(121, 20);
            this.getdayaccrualcomboBox.TabIndex = 6;
            // 
            // countdaycomboBox
            // 
            this.countdaycomboBox.FormattingEnabled = true;
            this.countdaycomboBox.Items.AddRange(new object[] {
            "01",
            "02",
            "03",
            "04",
            "05",
            "06",
            "07",
            "08",
            "09",
            "10",
            "11",
            "12",
            "13",
            "14",
            "15",
            "16",
            "17",
            "18",
            "19",
            "20",
            "21",
            "22",
            "23",
            "24",
            "25",
            "26",
            "27",
            "28",
            "月底"});
            this.countdaycomboBox.Location = new System.Drawing.Point(153, 110);
            this.countdaycomboBox.Name = "countdaycomboBox";
            this.countdaycomboBox.Size = new System.Drawing.Size(121, 20);
            this.countdaycomboBox.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(34, 68);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 12);
            this.label2.TabIndex = 4;
            this.label2.Text = "公司简称:";
            // 
            // companySimpleNametextBox
            // 
            this.companySimpleNametextBox.Location = new System.Drawing.Point(153, 68);
            this.companySimpleNametextBox.Name = "companySimpleNametextBox";
            this.companySimpleNametextBox.Size = new System.Drawing.Size(191, 21);
            this.companySimpleNametextBox.TabIndex = 3;
            this.companySimpleNametextBox.Leave += new System.EventHandler(this.textBox1_Leave);
            this.companySimpleNametextBox.Enter += new System.EventHandler(this.textBox1_Enter);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(34, 41);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "公司名称:";
            // 
            // shapeContainer1
            // 
            this.shapeContainer1.Location = new System.Drawing.Point(3, 17);
            this.shapeContainer1.Margin = new System.Windows.Forms.Padding(0);
            this.shapeContainer1.Name = "shapeContainer1";
            this.shapeContainer1.Shapes.AddRange(new Microsoft.VisualBasic.PowerPacks.Shape[] {
            this.lineShape4,
            this.lineShape3,
            this.lineShape2,
            this.lineShape1});
            this.shapeContainer1.Size = new System.Drawing.Size(415, 300);
            this.shapeContainer1.TabIndex = 13;
            this.shapeContainer1.TabStop = false;
            // 
            // lineShape4
            // 
            this.lineShape4.BorderColor = System.Drawing.Color.Teal;
            this.lineShape4.Name = "lineShape4";
            this.lineShape4.X1 = 27;
            this.lineShape4.X2 = 391;
            this.lineShape4.Y1 = 272;
            this.lineShape4.Y2 = 272;
            // 
            // lineShape3
            // 
            this.lineShape3.BorderColor = System.Drawing.Color.Teal;
            this.lineShape3.Name = "lineShape3";
            this.lineShape3.X1 = 27;
            this.lineShape3.X2 = 391;
            this.lineShape3.Y1 = 219;
            this.lineShape3.Y2 = 219;
            // 
            // lineShape2
            // 
            this.lineShape2.BorderColor = System.Drawing.Color.Teal;
            this.lineShape2.Name = "lineShape2";
            this.lineShape2.X1 = 27;
            this.lineShape2.X2 = 391;
            this.lineShape2.Y1 = 148;
            this.lineShape2.Y2 = 148;
            // 
            // lineShape1
            // 
            this.lineShape1.BorderColor = System.Drawing.Color.Teal;
            this.lineShape1.Name = "lineShape1";
            this.lineShape1.X1 = 27;
            this.lineShape1.X2 = 391;
            this.lineShape1.Y1 = 81;
            this.lineShape1.Y2 = 81;
            // 
            // CompanySetExtensionUI_Control
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Name = "CompanySetExtensionUI_Control";
            this.Size = new System.Drawing.Size(421, 320);
            this.Load += new System.EventHandler(this.CompanySetExtensionUI_Control_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox companyNametextBox;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox getdayaccrualcomboBox;
        private System.Windows.Forms.ComboBox countdaycomboBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox companySimpleNametextBox;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckBox auto_postponed_checkBox;
        private System.Windows.Forms.CheckBox use_postponed_checkBox;
        private Microsoft.VisualBasic.PowerPacks.ShapeContainer shapeContainer1;
        private Microsoft.VisualBasic.PowerPacks.LineShape lineShape1;
        private Microsoft.VisualBasic.PowerPacks.LineShape lineShape3;
        private Microsoft.VisualBasic.PowerPacks.LineShape lineShape2;
        private System.Windows.Forms.Label label7;
        private Microsoft.VisualBasic.PowerPacks.LineShape lineShape4;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.Button button1;
    }
}
