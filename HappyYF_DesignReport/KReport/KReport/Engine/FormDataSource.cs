namespace KReport.Engine
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class FormDataSource : Form
    {
        private Button btnOk;
        private IContainer components = null;
        private static FormDataSource form;
        private ListBox lsbDataSources;
        private Report report;

        private FormDataSource()
        {
            this.InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (this.lsbDataSources.SelectedItem != null)
            {
                this.report.MasterDataSource = this.lsbDataSources.SelectedItem.ToString();
            }
            base.Close();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void FormDataSource_Load(object sender, EventArgs e)
        {
            this.lsbDataSources.Items.Clear();
            foreach (IReportData data in this.report.DataSources)
            {
                this.lsbDataSources.Items.Add(data.DataSourceName);
            }
            this.lsbDataSources.SelectedItem = this.report.MasterDataSource;
        }

        private void InitializeComponent()
        {
            this.btnOk = new Button();
            this.lsbDataSources = new ListBox();
            base.SuspendLayout();
            this.btnOk.Location = new Point(0x3f, 0x8d);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new Size(0x4b, 0x17);
            this.btnOk.TabIndex = 1;
            this.btnOk.Text = "Ok";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new EventHandler(this.button1_Click);
            this.lsbDataSources.FormattingEnabled = true;
            this.lsbDataSources.Location = new Point(3, 1);
            this.lsbDataSources.Name = "lsbDataSources";
            this.lsbDataSources.Size = new Size(0xc9, 0x86);
            this.lsbDataSources.TabIndex = 2;
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0xcf, 0xa8);
            base.Controls.Add(this.lsbDataSources);
            base.Controls.Add(this.btnOk);
            base.FormBorderStyle = FormBorderStyle.FixedDialog;
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "FormDataSource";
            base.StartPosition = FormStartPosition.CenterParent;
            this.Text = "Condutor de Dados Principal";
            base.Load += new EventHandler(this.FormDataSource_Load);
            base.ResumeLayout(false);
        }

        public static DialogResult ShowDialog(Report report)
        {
            if (form == null)
            {
                form = new FormDataSource();
            }
            form.report = report;
            return form.ShowDialog();
        }
    }
}

