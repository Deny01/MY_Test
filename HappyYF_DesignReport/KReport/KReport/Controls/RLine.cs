namespace KReport.Controls
{
    using KReport.Engine;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Windows.Forms;

    internal class RLine : CustomControl
    {
        private LineAlignment lineAlignment = LineAlignment.靠顶;
        private float lineLargura;
        private LineStyle lineStyle;

        public RLine()
        {
            base.controlType = ControlType.ControlLine;
            this.ForeColor = Color.Black;
            this.BackColor = Color.White;
            base.Width = 200;
            base.Height = 10;
            this.线段类型 = LineStyle.虚线;
            this.线宽 = 1f;
        }

        public override void DrawCommand(Graphics g, int offset)
        {
            base.ApplyEscala(g);
            int num = offset + ((int)Utils.ConvertPixelToDisplay(base.垂直位置));
            int esquerda = 0;
            int num3 = 0;
            int direita = 0;
            int num5 = 0;
            int num6 = offset + ((int)Utils.ConvertPixelToDisplay(base.垂直位置));
            int num7 = num6 + base.altura;
            switch (this.对齐方式)
            {
                case LineAlignment.靠顶:
                    esquerda = base.esquerda;
                    num3 = num6;
                    direita = base.direita;
                    num5 = num;
                    break;

                case LineAlignment.靠底:
                    esquerda = base.esquerda;
                    num3 = num7 - 1;
                    direita = base.direita;
                    num5 = num7 - 1;
                    break;

                case LineAlignment.靠左:
                    esquerda = base.esquerda;
                    num3 = num6;
                    direita = base.esquerda;
                    num5 = num7;
                    break;

                case LineAlignment.靠右:
                    esquerda = base.direita - 1;
                    num3 = num6;
                    direita = base.direita - 1;
                    num5 = num7;
                    break;
            }
            this.DrawLine(g, esquerda, num3, direita, num5);
        }

        private void DrawLine(Graphics g)
        {
            int num = 0;
            int num2 = 0;
            int width = 0;
            int altura = 0;
            switch (this.对齐方式)
            {
                case LineAlignment.靠顶:
                    num = 0;
                    num2 = 1;
                    width = base.Width;
                    altura = 1;
                    break;

                case LineAlignment.靠底:
                    num = 0;
                    num2 = base.altura - 1;
                    width = base.largura;
                    altura = base.altura - 1;
                    break;

                case LineAlignment.靠左:
                    num = 1;
                    num2 = 0;
                    width = 1;
                    altura = base.altura;
                    break;

                case LineAlignment.靠右:
                    num = base.largura - 1;
                    num2 = 0;
                    width = base.largura - 1;
                    altura = base.altura;
                    break;
            }
            this.DrawLine(g, num, num2, width, altura);
        }

        private void DrawLine(Graphics g, int x1, int y1, int x2, int y2)
        {
            Pen pen = new Pen(this.ForeColor, this.线宽);
            if (this.线段类型 == LineStyle.虚线)
            {
                pen.DashStyle = DashStyle.Dot;
                pen.DashCap = DashCap.Flat;
            }
            else
            {
                pen.DashStyle = DashStyle.Solid;
            }
            g.DrawLine(pen, x1, y1, x2, y2);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.FillRectangle(Brushes.Transparent, 0, 0, base.ClientRectangle.Width, base.ClientRectangle.Height);
            base.OnPaint(e);
            base.ApplyEscala(e.Graphics);
            this.DrawLine(e.Graphics);
        }

        //[Category("Custom")]
        [Description("线段排列方式"), Category("线段"), ReportElementAttribute("报表元素")]
        public LineAlignment 对齐方式
        {
            get
            {
                return this.lineAlignment;
            }
            set
            {
                this.lineAlignment = value;
                base.Invalidate();
            }
        }

        //[Category("Custom")]
        [Description("线段类型"), Category("线段"), ReportElementAttribute("报表元素")]
        public LineStyle 线段类型
        {
            get
            {
                return this.lineStyle;
            }
            set
            {
                this.lineStyle = value;
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
            }
        }

        //[Category("Custom")]
        [Description("设置线宽"), Category("外观"), ReportElementAttribute("报表元素")]
        public float 线宽
        {
            get
            {
                return this.lineLargura;
            }
            set
            {
                if (value >= 0.5f)
                {
                    this.lineLargura = value;
                }
                base.Invalidate();
            }
        }
    }
}

