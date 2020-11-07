namespace KReport.Engine
{
    using KReport.Designer;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class FormPreview : Form
    {
        private IContainer components = null;
        private ControlViewer controlViewer1;
        private static FormPreview form = null;

        private FormPreview()
        {
            
            this.InitializeComponent();

            this.controlViewer1 = new ControlViewer();
            base.SuspendLayout();
            this.controlViewer1.Dock = DockStyle.Fill;
            this.controlViewer1.Location = new Point(0, 0);
            this.controlViewer1.Name = "controlViewer1";
            this.controlViewer1.Size = new Size(0x282, 0x177);
            this.controlViewer1.TabIndex = 0;
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x282, 0x177);
            base.Controls.Add(this.controlViewer1);
            //base.MaximizeBox = false;
            //base.MinimizeBox = false;
            base.Name = "FormView";
            base.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "报表打印";
            base.ResumeLayout(false);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // FormPreview
            // 
            this.ClientSize = new System.Drawing.Size(292, 273);
            this.Name = "FormPreview";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.ResumeLayout(false);

        }

        public static DialogResult Show(Report report)
        {
            form = new FormPreview();
            form.controlViewer1.ShowReport(report);
            return form.ShowDialog();
        }
    }
}

