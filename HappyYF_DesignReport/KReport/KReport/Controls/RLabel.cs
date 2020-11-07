namespace KReport.Controls
{
    using KReport.Engine;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    internal class RLabel : CustomControl, ICloneable
    {
        private TextAlignment textAlignment = TextAlignment.Center;

        public RLabel()
        {
            base.controlType = ControlType.ControlText;
            this.BackColor = Color.White;
            base.Height = 10;
            base.Width = 30;
        }

        protected Point AligimentText(Graphics g)
        {
            SizeF ef = g.MeasureString(this.Text, this.Font);
            int x = 0;
            int y = 0;
            switch (this.textAlignment)
            {
                case TextAlignment.Left:
                    x = 0;
                    y = 0;
                    break;

                case TextAlignment.Center:
                    if (base.largura > ef.Width)
                    {
                        x = (base.largura / 2) - (Convert.ToInt16(ef.Width) / 2);
                    }
                    if (base.Height > ef.Height)
                    {
                        y = (base.altura / 2) - (Convert.ToInt16(ef.Height) / 2);
                    }
                    break;

                case TextAlignment.Right:
                    x = base.largura - Convert.ToInt16(ef.Width);
                    y = 0;
                    break;

                default:
                    x = 0;
                    y = 0;
                    break;
            }
            return new Point(x, y);
        }

        public override void DrawCommand(Graphics g, int offset)
        {
            base.ApplyEscala(g);
            int x = this.AligimentText(g).X + base.esquerda;
            int y = ((int) Utils.ConvertPixelToDisplay(base.垂直位置)) + offset;
            this.DrawString(g, x, y);
        }

        protected void DrawRectangle(Graphics g)
        {
            if (base.largura > 0)
            {
                g.DrawRectangle(new Pen(this.ForeColor, 2f), new Rectangle(0, 0, base.ClientRectangle.Width, base.ClientRectangle.Height));
            }
        }

        protected void DrawString(Graphics g)
        {
            Point point = this.AligimentText(g);
            this.DrawString(g, point.X, point.Y);
        }

        protected void DrawString(Graphics g, int x, int y)
        {
            g.DrawString(this.Text, this.Font, new SolidBrush(this.ForeColor), (float) x, (float) y);
        }

        protected void FillRectangle(Graphics g)
        {
            g.FillRectangle(Brushes.Transparent, 0, 0, base.ClientRectangle.Width, base.ClientRectangle.Height);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.ApplyEscala(e.Graphics);
            this.FillRectangle(e.Graphics);
            this.DrawRectangle(e.Graphics);
            this.DrawString(e.Graphics);
        }

        protected override void OnResize(EventArgs e)
        {
            Graphics g = base.CreateGraphics();
            g.Clear(this.BackColor);
            this.FillRectangle(g);
            this.DrawRectangle(g);
            this.DrawString(g);
            g.Dispose();
        }

        //[Category("Custom")]
        [Description("文字排列方式"), Category("文字"), ReportElementAttribute("报表元素")]
        public TextAlignment 对齐方式
        {
            get
            {
                return this.textAlignment;
            }
            set
            {
                this.textAlignment = value;
                base.Invalidate();
            }
        }

        //[Category("Custom")]
        [Description("设置背景色"), Category("外观"), DefaultValue("255,255,255"), ReportElementAttribute("报表元素")]
        //public override Color BackColor
        public  Color 背景色
        {
            get
            {
                return base.BackColor;
            }
            set
            {
                base.BackColor = value;
                base.Invalidate();
            }
        }

        //[Category("Custom")]
        [Description("设置字体"), Category("文字"), ReportElementAttribute("报表元素")]
       // public override System.Drawing.Font 字体
        public  System.Drawing.Font 字体
        {
            get
            {
                return base.Font;
            }
            set
            {
                base.Font = value;
                base.Invalidate();
            }
        }

        //[Category("Custom")]
        [Description("设置前景色"), Category("外观"), ReportElementAttribute("报表元素")]
        //public override Color ForeColor
        public  Color 前景色
        {
            get
            {
                return base.ForeColor;
            }
            set
            {
                base.ForeColor = value;
                base.Invalidate();
            }
        }

        //[Category("Custom")]
        [Description("设置文字"), Category("文字"), ReportElementAttribute("报表元素")]
        //public override string Text
        public  string 文本  
        {
            get
            {
                return base.Text;
            }
            set
            {
                base.Text = value;
                base.Invalidate();
            }
        }

        ////[ Category("Position")]
        //[Description("水平位置"), Category("位置")]
        //public int xRelative
        //{
        //    get
        //    {
        //        return base.xRelative;
        //    }
        //    set
        //    {
        //        base.xRelative = value;
        //    }
        //}

        ////[Category("Position")]
        //[Description("垂直位置"), Category("位置")]
        //public int yRelative
        //{
        //    get
        //    {
        //        return base.yRelative;
        //    }
        //    set
        //    {
        //        base.yRelative = value;
        //    }
        //}
    }
}

