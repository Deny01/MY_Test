namespace KReport.Engine
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Drawing.Imaging;
    using System.IO;
    using System.Windows.Forms;

    internal class ControlScreen : UserControl
    {
        private int columns;
        private IContainer components = null;
        private ReportDeviceScreen device;
        private Panel panel1;
        private Panel panel2;
        private PictureBox pictureBoxPage;
        private Panel pnlbackground;
        private int rows;
        private int startpage;
        private double zoom;

        public ControlScreen()
        {
            this.InitializeComponent();
            this.rows = 1;
            this.columns = 1;
            this.startpage = 1;
            this.zoom = 1.0;
        }

        public void AlingImage()
        {
            int num = 40;
            int num2 = 40;
            if (this.pnlbackground.Width > this.pictureBoxPage.Width)
            {
                num = (this.pnlbackground.Width / 2) - (this.pictureBoxPage.Width / 2);
            }
            if (this.pnlbackground.Height > this.pictureBoxPage.Height)
            {
                num2 = (this.pnlbackground.Height / 2) - (this.pictureBoxPage.Height / 2);
            }
            this.pictureBoxPage.Left = num;
            this.pictureBoxPage.Top = num2;
        }

        public static Bitmap ConvertBitmap(Bitmap inputBmp, ImageFormat destFormat)
        {
            if (inputBmp.RawFormat.Equals(destFormat))
            {
                return (Bitmap) inputBmp.Clone();
            }
            Stream stream = new MemoryStream();
            inputBmp.Save(stream, destFormat);
            return new Bitmap(stream);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        public Bitmap GetImageZoon(Bitmap source, float scale)
        {
            Bitmap image = new Bitmap((int) (source.Size.Width * scale), (int) (source.Size.Height * scale), PixelFormat.Format24bppRgb);
            Graphics graphics = Graphics.FromImage(image);
            graphics.Clear(Color.White);
            graphics.ScaleTransform(scale, scale);
            graphics.DrawImage(source, 0, 0, image.Width, image.Height);
            graphics.Dispose();
            return image;
        }

        private void InitializeComponent()
        {
            this.pnlbackground = new Panel();
            this.pictureBoxPage = new PictureBox();
            this.panel2 = new Panel();
            this.panel1 = new Panel();
            this.pnlbackground.SuspendLayout();
            ((ISupportInitialize) this.pictureBoxPage).BeginInit();
            base.SuspendLayout();
            this.pnlbackground.AutoScroll = true;
            this.pnlbackground.BackColor = SystemColors.ControlDark;
            this.pnlbackground.Controls.Add(this.pictureBoxPage);
            this.pnlbackground.Controls.Add(this.panel2);
            this.pnlbackground.Controls.Add(this.panel1);
            this.pnlbackground.Dock = DockStyle.Fill;
            this.pnlbackground.Location = new Point(0, 0);
            this.pnlbackground.Name = "pnlbackground";
            this.pnlbackground.Size = new Size(0x1c0, 390);
            this.pnlbackground.TabIndex = 0;
            this.pictureBoxPage.Location = new Point(0x36, 0x34);
            this.pictureBoxPage.Name = "pictureBoxPage";
            this.pictureBoxPage.Size = new Size(340, 0xf4);
            this.pictureBoxPage.TabIndex = 5;
            this.pictureBoxPage.TabStop = false;
            this.panel2.Dock = DockStyle.Bottom;
            this.panel2.Location = new Point(0, 0x155);
            this.panel2.Name = "panel2";
            this.panel2.Size = new Size(0x1c0, 0x31);
            this.panel2.TabIndex = 1;
            this.panel1.Dock = DockStyle.Top;
            this.panel1.Location = new Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new Size(0x1c0, 0x2f);
            this.panel1.TabIndex = 0;
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.Controls.Add(this.pnlbackground);
            base.Name = "ControlScreen";
            base.Size = new Size(0x1c0, 390);
            this.pnlbackground.ResumeLayout(false);
            ((ISupportInitialize) this.pictureBoxPage).EndInit();
            base.ResumeLayout(false);
        }

        private void ProcessImage()
        {
            if (this.DeviceScreen != null)
            {
                this.pictureBoxPage.SizeMode = PictureBoxSizeMode.Normal;
                this.pictureBoxPage.Image = null;
                Bitmap inputBmp = (Bitmap) this.DeviceScreen.Pages[this.startpage];
                Bitmap bitmap2 = ScaleBitmap(inputBmp, this.zoom, this.zoom);
                this.pictureBoxPage.Width = bitmap2.Width;
                this.pictureBoxPage.Height = bitmap2.Height;
                this.AlingImage();
                this.pictureBoxPage.Image = bitmap2;
            }
        }

        public static Bitmap ScaleBitmap(Bitmap inputBmp, double xScaleFactor, double yScaleFactor)
        {
            Bitmap image = new Bitmap((int) (inputBmp.Size.Width * xScaleFactor), (int) (inputBmp.Size.Height * yScaleFactor), PixelFormat.Format48bppRgb);
            Graphics graphics = Graphics.FromImage(image);
            graphics.PageScale = 1f;
            graphics.PageUnit = GraphicsUnit.Pixel;
            graphics.Clear(Color.White);
            graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
            graphics.ScaleTransform((float) xScaleFactor, (float) yScaleFactor);
            Rectangle destRect = new Rectangle(0, 0, inputBmp.Size.Width, inputBmp.Size.Height);
            graphics.DrawImage(inputBmp, destRect, destRect, GraphicsUnit.Pixel);
            graphics.Dispose();
            return ConvertBitmap(image, inputBmp.RawFormat);
        }

        public void ShowScreen()
        {
            this.ProcessImage();
        }

        public int Columns
        {
            get
            {
                return this.columns;
            }
            set
            {
                this.columns = value;
            }
        }

        public ReportDeviceScreen DeviceScreen
        {
            get
            {
                return this.device;
            }
            set
            {
                this.device = value;
            }
        }

        public int Rows
        {
            get
            {
                return this.rows;
            }
            set
            {
                this.rows = value;
            }
        }

        public int StartPage
        {
            get
            {
                return this.startpage;
            }
            set
            {
                this.startpage = value;
            }
        }

        public double Zoom
        {
            get
            {
                return this.zoom;
            }
            set
            {
                if (value > 0.0)
                {
                    this.zoom = value;
                    this.ProcessImage();
                }
            }
        }
    }
}

