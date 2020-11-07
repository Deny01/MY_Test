namespace KReport.Engine
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    internal class ControlPrintView : UserControl
    {
        private IContainer components = null;
        private Panel panel1;
        private Panel panel2;
        public PictureBox pictureBox1;
        private Panel pnlbackground;

        public ControlPrintView()
        {
            this.InitializeComponent();
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
            this.pnlbackground = new Panel();
            this.panel1 = new Panel();
            this.panel2 = new Panel();
            this.pictureBox1 = new PictureBox();
            this.pnlbackground.SuspendLayout();
            ((ISupportInitialize) this.pictureBox1).BeginInit();
            base.SuspendLayout();
            this.pnlbackground.AutoScroll = true;
            this.pnlbackground.Controls.Add(this.pictureBox1);
            this.pnlbackground.Controls.Add(this.panel2);
            this.pnlbackground.Controls.Add(this.panel1);
            this.pnlbackground.Dock = DockStyle.Fill;
            this.pnlbackground.Location = new Point(0, 0);
            this.pnlbackground.Name = "pnlbackground";
            this.pnlbackground.Size = new Size(570, 0x133);
            this.pnlbackground.TabIndex = 2;
            this.panel1.Dock = DockStyle.Bottom;
            this.panel1.Location = new Point(0, 0x114);
            this.panel1.Name = "panel1";
            this.panel1.Size = new Size(570, 0x1f);
            this.panel1.TabIndex = 3;
            this.panel2.Dock = DockStyle.Top;
            this.panel2.Location = new Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new Size(570, 0x1f);
            this.panel2.TabIndex = 4;
            this.pictureBox1.Location = new Point(0x1f, 0x25);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new Size(0x20b, 0xe9);
            this.pictureBox1.TabIndex = 5;
            this.pictureBox1.TabStop = false;
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.AutoScaleMode = AutoScaleMode.Font;
            this.BackColor = SystemColors.AppWorkspace;
            base.Controls.Add(this.pnlbackground);
            base.Name = "ControlPrintView";
            base.Size = new Size(570, 0x133);
            this.pnlbackground.ResumeLayout(false);
            ((ISupportInitialize) this.pictureBox1).EndInit();
            base.ResumeLayout(false);
        }
    }
}

