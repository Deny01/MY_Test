namespace KReport.Engine
{
    using KReport.Properties;
    using MakeSoft.Tools;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Drawing.Printing;
    using System.Windows.Forms;

    internal class FormPageSetting : Form
    {
        private Button btnCancela;
        private Button btnOk;
        private ComboBox comboPageSize;
        private IContainer components = null;
        private NumericUpDown edtAltura;
        private NumericUpDown edtBottom;
        private NumericUpDown edtLargura;
        private NumericUpDown edtLeft;
        private NumericUpDown edtRigth;
        private NumericUpDown edtTop;
        private static FormPageSetting formPage = null;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private Label label7;
        private RPage pageReport = null;
        private PictureBox pictureBox1;
        private RadioButton rdbPaisagem;
        private RadioButton rdbRetrato;

        private FormPageSetting()
        {

            //int[] bits = new int[4];
            
           iniNew();
           this.InitializeComponent();
           
        }

        public  void iniNew()
        {
            this.comboPageSize = new ComboBox();
            this.label1 = new Label();
            this.groupBox1 = new GroupBox();
            this.edtLeft = new NumericUpDown();
            this.edtRigth = new NumericUpDown();
            this.edtBottom = new NumericUpDown();
            this.edtTop = new NumericUpDown();
            this.label4 = new Label();
            this.label5 = new Label();
            this.label3 = new Label();
            this.label2 = new Label();
            this.label6 = new Label();
            this.label7 = new Label();
            this.btnOk = new Button();
            this.btnCancela = new Button();
            this.groupBox2 = new GroupBox();
            this.rdbPaisagem = new RadioButton();
            this.rdbRetrato = new RadioButton();
            this.edtAltura = new NumericUpDown();
            this.edtLargura = new NumericUpDown();
            this.pictureBox1 = new PictureBox();
            this.groupBox1.SuspendLayout();
            this.edtLeft.BeginInit();
            this.edtRigth.BeginInit();
            this.edtBottom.BeginInit();
            this.edtTop.BeginInit();
            this.groupBox2.SuspendLayout();
            this.edtAltura.BeginInit();
            this.edtLargura.BeginInit();
            ((ISupportInitialize)this.pictureBox1).BeginInit();
            base.SuspendLayout();
            this.comboPageSize.DropDownStyle = ComboBoxStyle.DropDownList;
            this.comboPageSize.FormattingEnabled = true;
            this.comboPageSize.Location = new Point(15, 0x19);
            this.comboPageSize.Name = "comboPageSize";
            this.comboPageSize.Size = new Size(0xa3, 0x15);
            this.comboPageSize.TabIndex = 1;
            this.comboPageSize.SelectedIndexChanged += new EventHandler(this.comboPageSize_SelectedIndexChanged);
            this.label1.AutoSize = true;
            this.label1.Location = new Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x61, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "纸张尺寸";
            this.groupBox1.Controls.Add(this.edtLeft);
            this.groupBox1.Controls.Add(this.edtRigth);
            this.groupBox1.Controls.Add(this.edtBottom);
            this.groupBox1.Controls.Add(this.edtTop);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new Point(0xbb, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(0xd5, 0x98);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "边界";
            this.edtLeft.Location = new Point(0x53, 100);
            int[] bits = new int[4];
            bits[0] = 0x7d0;
            this.edtLeft.Maximum = new decimal(bits);
            this.edtLeft.Name = "edtLeft";
            this.edtLeft.Size = new Size(0x4f, 20);
            this.edtLeft.TabIndex = 15;
            bits = new int[4];
            bits[0] = 3;
            bits[3] = 0x10000;
            this.edtLeft.Value = new decimal(bits);
            this.edtRigth.Location = new Point(0x53, 0x4a);
            bits = new int[4];
            bits[0] = 0x7d0;
            this.edtRigth.Maximum = new decimal(bits);
            this.edtRigth.Name = "edtRigth";
            this.edtRigth.Size = new Size(0x4f, 20);
            this.edtRigth.TabIndex = 14;
            bits = new int[4];
            bits[0] = 3;
            bits[3] = 0x10000;
            this.edtRigth.Value = new decimal(bits);
            this.edtBottom.Location = new Point(0x53, 0x2e);
            bits = new int[4];
            bits[0] = 0x7d0;
            this.edtBottom.Maximum = new decimal(bits);
            this.edtBottom.Name = "edtBottom";
            this.edtBottom.Size = new Size(0x4f, 20);
            this.edtBottom.TabIndex = 13;
            bits = new int[4];
            bits[0] = 3;
            bits[3] = 0x10000;
            this.edtBottom.Value = new decimal(bits);
            this.edtTop.Location = new Point(0x53, 20);
            bits = new int[4];
            bits[0] = 0x7d0;
            this.edtTop.Maximum = new decimal(bits);
            this.edtTop.Name = "edtTop";
            this.edtTop.Size = new Size(0x4f, 20);
            this.edtTop.TabIndex = 12;
            bits = new int[4];
            bits[0] = 3;
            bits[3] = 0x10000;
            this.edtTop.Value = new decimal(bits);
            this.label4.AutoSize = true;
            this.label4.Location = new Point(0x19, 0x30);
            this.label4.Name = "label4";
            this.label4.Size = new Size(0x29, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "底部";
            this.label5.AutoSize = true;
            this.label5.Location = new Point(0x19, 0x16);
            this.label5.Name = "label5";
            this.label5.Size = new Size(0x2e, 13);
            this.label5.TabIndex = 7;
            this.label5.Text = "顶部";
            this.label3.AutoSize = true;
            this.label3.Location = new Point(0x19, 100);
            this.label3.Name = "label3";
            this.label3.Size = new Size(0x34, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "右边";
            this.label2.AutoSize = true;
            this.label2.Location = new Point(0x19, 0x4a);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x25, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "左边";
            this.label6.AutoSize = true;
            this.label6.Location = new Point(12, 0x4f);
            this.label6.Name = "label6";
            this.label6.Size = new Size(0x2b, 13);
            this.label6.TabIndex = 13;
            this.label6.Text = "宽度";
            this.label7.AutoSize = true;
            this.label7.Location = new Point(12, 0x38);
            this.label7.Name = "label7";
            this.label7.Size = new Size(0x22, 13);
            this.label7.TabIndex = 11;
            this.label7.Text = "高度";
            this.btnOk.Location = new Point(0x6f, 180);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new Size(0x55, 0x1b);
            this.btnOk.TabIndex = 0x10;
            this.btnOk.Text = "确  定";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new EventHandler(this.btnOk_Click);
            this.btnCancela.Location = new Point(0xd5, 180);
            this.btnCancela.Name = "btnCancela";
            this.btnCancela.Size = new Size(0x55, 0x1b);
            this.btnCancela.TabIndex = 0x11;
            this.btnCancela.Text = "取  消";
            this.btnCancela.UseVisualStyleBackColor = true;
            this.btnCancela.Click += new EventHandler(this.btnCancela_Click);
            this.groupBox2.Controls.Add(this.pictureBox1);
            this.groupBox2.Controls.Add(this.rdbPaisagem);
            this.groupBox2.Controls.Add(this.rdbRetrato);
            this.groupBox2.Location = new Point(6, 0x68);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new Size(0xaf, 60);
            this.groupBox2.TabIndex = 0x12;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "方向";
            this.rdbPaisagem.AutoSize = true;
            this.rdbPaisagem.Location = new Point(0x12, 0x25);
            this.rdbPaisagem.Name = "rdbPaisagem";
            this.rdbPaisagem.Size = new Size(0x47, 0x11);
            this.rdbPaisagem.TabIndex = 1;
            this.rdbPaisagem.Text = "横向";
            this.rdbPaisagem.UseVisualStyleBackColor = true;
            this.rdbRetrato.AutoSize = true;
            this.rdbRetrato.Checked = true;
            this.rdbRetrato.Location = new Point(0x12, 14);
            this.rdbRetrato.Name = "rdbRetrato";
            this.rdbRetrato.Size = new Size(60, 0x11);
            this.rdbRetrato.TabIndex = 0;
            this.rdbRetrato.TabStop = true;
            this.rdbRetrato.Text = "纵向";
            this.rdbRetrato.UseVisualStyleBackColor = true;
            this.edtAltura.Location = new Point(0x63, 0x34);
            bits = new int[4];
            bits[0] = 0x2710;
            this.edtAltura.Maximum = new decimal(bits);
            this.edtAltura.Name = "edtAltura";
            this.edtAltura.Size = new Size(0x4f, 20);
            this.edtAltura.TabIndex = 11;
            bits = new int[4];
            bits[0] = 3;
            bits[3] = 0x10000;
            this.edtAltura.Value = new decimal(bits);
            this.edtLargura.Location = new Point(0x63, 0x4f);
            bits = new int[4];
            bits[0] = 0x2710;
            this.edtLargura.Maximum = new decimal(bits);
            this.edtLargura.Name = "edtLargura";
            this.edtLargura.Size = new Size(0x4f, 20);
            this.edtLargura.TabIndex = 0x13;
            bits = new int[4];
            bits[0] = 3;
            bits[3] = 0x10000;
            this.edtLargura.Value = new decimal(bits);
            this.pictureBox1.Image = Resources.Ladscape;
            this.pictureBox1.Location = new Point(0x69, 0x13);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new Size(0x29, 0x23);
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x195, 0xde);
            base.Controls.Add(this.edtLargura);
            base.Controls.Add(this.edtAltura);
            base.Controls.Add(this.groupBox2);
            base.Controls.Add(this.btnCancela);
            base.Controls.Add(this.btnOk);
            base.Controls.Add(this.label6);
            base.Controls.Add(this.label7);
            base.Controls.Add(this.groupBox1);
            base.Controls.Add(this.label1);
            base.Controls.Add(this.comboPageSize);
            base.FormBorderStyle = FormBorderStyle.FixedDialog;
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "FormPageSetting";
            base.StartPosition = FormStartPosition.CenterParent;
            this.Text = "打印设定";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.edtLeft.EndInit();
            this.edtRigth.EndInit();
            this.edtBottom.EndInit();
            this.edtTop.EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.edtAltura.EndInit();
            this.edtLargura.EndInit();
            ((ISupportInitialize)this.pictureBox1).EndInit();
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        private void btnCancela_Click(object sender, EventArgs e)
        {
            base.Close();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            this.SetPageReport();
            base.Close();
        }

        private void comboPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            PaperSize pageSize = this.GetPageSize(this.comboPageSize.SelectedItem.ToString());
            this.edtAltura.Value = FunctionsGraphics.ConvertDisplayToMilimetro(pageSize.Height);
            this.edtLargura.Value = FunctionsGraphics.ConvertDisplayToMilimetro(pageSize.Width);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void FillComboPageSize()
        {
            PrinterSettings settings = new PrinterSettings();
            this.comboPageSize.Items.Clear();
            foreach (PaperSize size in settings.PaperSizes)
            {
                this.comboPageSize.Items.Add(size.PaperName);
            }
        }

        private PaperSize GetPageSize(string kind)
        {
            PrinterSettings settings = new PrinterSettings();
            foreach (PaperSize size in settings.PaperSizes)
            {
                if (size.PaperName.Equals(kind))
                {
                    return size;
                }
            }
            return null;
        }

        private void Iniciatize()
        {
            for (int i = 0; i <= (this.comboPageSize.Items.Count - 1); i++)
            {
                if (this.comboPageSize.Items[i].ToString().Equals(this.pageReport.PaperName))
                {
                    this.comboPageSize.SelectedIndex = i;
                    break;
                }
            }
            this.edtBottom.Value = this.pageReport.MarginBottom;
            this.edtLeft.Value = this.pageReport.MarginLeft;
            this.edtRigth.Value = this.pageReport.MarginRigth;
            this.edtTop.Value = this.pageReport.MarginTop;
            this.edtLargura.Value = (int)FunctionsGraphics.ConvertPixelToMilimetro(this.pageReport.PageWidth);
            this.edtAltura.Value = (int)FunctionsGraphics.ConvertPixelToMilimetro(this.pageReport.PageHeight);
            this.rdbPaisagem.Checked = this.pageReport.LandScape;
            this.rdbRetrato.Checked = !this.pageReport.LandScape;
        }

        private void InitializeComponent()
        {
          // iniNew();

        }

        private void SetPageReport()
        {
            this.pageReport.PaperName = this.comboPageSize.SelectedItem.ToString();
            this.pageReport.MarginBottom = Convert.ToInt16(this.edtBottom.Value);
            this.pageReport.MarginLeft = Convert.ToInt16(this.edtLeft.Value);
            this.pageReport.MarginRigth = Convert.ToInt16(this.edtRigth.Value);
            this.pageReport.MarginTop = Convert.ToInt16(this.edtTop.Value);
            this.pageReport.LandScape = this.rdbPaisagem.Checked;
            this.pageReport.PageHeight = FunctionsGraphics.ConvertMilimetroToPixel((int)this.edtAltura.Value);
            this.pageReport.PageWidth = FunctionsGraphics.ConvertMilimetroToPixel((int)this.edtLargura.Value);
        }

        public static DialogResult ShowDialog(RPage page)
        {
            formPage = new FormPageSetting();
            formPage.PageReport = page;
            formPage.FillComboPageSize();
            formPage.Iniciatize();
            return formPage.ShowDialog();
        }

        public RPage PageReport
        {
            get
            {
                return this.pageReport;
            }
            set
            {
                this.pageReport = value;
            }
        }
    }
}

