namespace KReport.Controls
{
    using KReport.Engine;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    internal class RImage : CustomControl
    {
        private Color borderColor;
        private int borderWidth;
        private bool doStretch = false;
        private string fileName = string.Empty;
        protected Bitmap image = null;

        public RImage()
        {
            this.image = null;
            base.controlType = ControlType.ControlImage;
            this.ForeColor = Color.Black;
            this.BackColor = Color.White;
            this.边界颜色 = Color.Black;
            this.边界线宽 = 1;
            base.Height = 150;
            base.Width = 150;
        }

        public override void DrawCommand(Graphics g, int offset)
        {
            base.ApplyEscala(g);
            int y = offset + base.垂直位置;
            Rectangle region = new Rectangle(base.esquerda, y, base.largura - 2, base.altura - 2);
            this.DrawImage(g, region, false);
        }

        protected void DrawImage(Graphics g, Rectangle region, bool isdesigner)
        {
            this.BackColor = Color.White;
            SizeF ef = g.MeasureString("Image", new Font("Arial", 8f));
            if (this.image != null)
            {
                if (this.Stretch)
                {
                    g.DrawImage(this.image, region, 0, 0, this.image.Width, this.image.Height, GraphicsUnit.Pixel);
                }
                else
                {
                    g.DrawImage(this.image, region, 0, 0, region.Width, region.Height, GraphicsUnit.Pixel);
                }
            }
            else if (isdesigner)
            {
                int num = (region.Width / 2) - (Convert.ToInt16(ef.Width) / 2);
                int num2 = (region.Height / 2) - (Convert.ToInt16(ef.Height) / 2);
                g.DrawString(base.Name, new Font("Arial", 8f), Brushes.Black, (float) (num + region.Left), (float) num2, StringFormat.GenericDefault);
            }
            g.DrawRectangle(new Pen(this.边界颜色, (float) this.borderWidth), region);
        }

        protected void LoadImage()
        {
            try
            {
                this.image = new Bitmap(this.fileName);
            }
            catch (Exception)
            {
                this.image = null;
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.ApplyEscala(e.Graphics);
            base.OnPaint(e);
            Rectangle region = new Rectangle(1, 1, base.largura - 2, base.altura - 2);
            e.Graphics.Clear(this.BackColor);
            this.DrawImage(e.Graphics, region, true);
        }

        protected override void OnResize(EventArgs e)
        {
            Graphics g = base.CreateGraphics();
            g.Clear(this.BackColor);
            Rectangle region = new Rectangle(1, 1, base.largura - 2, base.Height - 2);
            this.DrawImage(g, region, true);
            g.Dispose();
        }

        //[Category("Custom")]
        [Description("设置边界颜色"), Category("外观"), ReportElementAttribute("报表元素")]
        //public Color BorderColor
        public Color 边界颜色
        {
            get
            {
                return this.borderColor;
            }
            set
            {
                this.borderColor = value;
                base.Invalidate();
            }
        }

       // [Category("Custom")]
        [Description("设置边界线宽"), Category("外观"), ReportElementAttribute("报表元素")]
       // public int BorderWidth
        public int 边界线宽
        {
            get
            {
                return this.borderWidth;
            }
            set
            {
                this.borderWidth = value;
                base.Invalidate();
            }
        }

        //[Category("Custom")]
        [Description("设置图形文件"), Category("图片"), ReportElementAttribute("报表元素")]
        public string 文件
        {
            get
            {
                return this.fileName;
            }
            set
            {
                this.fileName = value;
                this.LoadImage();
            }
        }

        //[Category("Custom")]
        [Description("设置图形"), Category("图片"), ReportElementAttribute("报表元素")]
        public Bitmap 图片
        {
            get
            {
                return this.image;
            }
            set
            {
                this.image = value;
                base.Invalidate();
            }
        }

        //[Category("Custom")]
        [Description("设置图形填充方式"), Category("图片"), ReportElementAttribute("报表元素")]
        public bool Stretch
        {
            get
            {
                return this.doStretch;
            }
            set
            {
                this.doStretch = value;
                base.Invalidate();
            }
        }
    }
}

