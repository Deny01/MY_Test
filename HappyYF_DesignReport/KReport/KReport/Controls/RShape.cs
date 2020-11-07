namespace KReport.Controls
{
    using KReport.Engine;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    internal class RShape : CustomControl
    {
        private Color borderColor;
        private int borderWidth;
        private TypeShape typeShape;

        public RShape()
        {
            base.controlType = ControlType.ControlShape;
            this.typeShape = TypeShape.矩形;
            this.borderWidth = 0;
            this.BackColor = Color.White;
            base.Width = 200;
            base.Height = 150;
        }

        public override void DrawCommand(Graphics g, int offset)
        {
            base.ApplyEscala(g);
            int y = offset + ((int) Utils.ConvertPixelToDisplay(base.垂直位置 ));
            Rectangle region = new Rectangle(base.esquerda, y, base.largura, base.altura);
            this.DrawShape(g, region);
        }

        private void DrawShape(Graphics g, Rectangle region)
        {
            if (this.typeShape == TypeShape.矩形)
            {
                g.FillRectangle(new SolidBrush(this.BackColor), region.Left, region.Top, region.Width, region.Height);
                if (this.边界线宽 > 0)
                {
                    g.DrawRectangle(new Pen(this.边界颜色, (float) this.边界线宽), region.Left, region.Top, region.Width - 2, region.Height - 2);
                }
            }
            if (this.typeShape == TypeShape.椭圆)
            {
                g.FillEllipse(new SolidBrush(this.BackColor), region.Left, region.Top, region.Width, region.Height);
                if (this.边界线宽 > 0)
                {
                    g.DrawEllipse(new Pen(this.边界颜色, (float) this.边界线宽), region.Left, region.Top, region.Width - 2, region.Height - 2);
                }
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Rectangle region = new Rectangle(1, 1, base.Width, base.Height);
            this.DrawShape(e.Graphics, region);
        }

        protected override void OnResize(EventArgs e)
        {
            Graphics g = base.CreateGraphics();
            g.Clear(this.BackColor);
            Rectangle region = new Rectangle(1, 1, base.Width, base.Right);
            this.DrawShape(g, region);
            g.Dispose();
        }

       // [Category("Custom")]
        [Description("设置背景色"), Category("外观"), ReportElementAttribute("报表元素")]
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
            }
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
        //public int BorderWidth
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
        [Description("图形形状"), Category("外观"), ReportElementAttribute("报表元素")]
        //public TypeShape ShapeType
        public TypeShape 形状
        {
            get
            {
                return this.typeShape;
            }
            set
            {
                this.typeShape = value;
                base.Invalidate();
            }
        }

        public enum TypeShape
        {
            //Retangle,
            //Cicle,
            //Elipse
            矩形,
            圆形,
            椭圆
        }
    }
}

