namespace KReport.Engine
{
    using KReport.Controls;
    using System;
    using System.Collections;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Drawing.Printing;
    using System.Runtime.CompilerServices;

    public class ReportDeviceScreen : ReportDevice
    {
        private int altura;
        private int direita;
        private int esquerda;
        private int largura;
        private Graphics page;
        private int posX;
        private int posY;

        public event DeviceEndPrintEventHandler EndPrint;

        public event DevicePageGenerateEventHandler PageGenerate;

        public event DeviceStartPrintEventHandler StartPrint;

        public ReportDeviceScreen()
        {
            base.DefaultPageSettings = new PageSettings();
            base.cache = new ReportCache();
        }

        protected void ApplyEscala(CustomControl control, GraphicsUnit pageunit)
        {
            switch (pageunit)
            {
                case GraphicsUnit.Display:
                    this.posX = (int) Utils.ConvertPixelToDisplay(control.Left);
                    this.esquerda = (int) Utils.ConvertPixelToDisplay(control.Left);
                    this.direita = (int) Utils.ConvertPixelToDisplay(control.Right);
                    this.largura = (int) Utils.ConvertPixelToDisplay(control.Width);
                    this.altura = (int) Utils.ConvertPixelToDisplay(control.Height);
                    break;

                case GraphicsUnit.Pixel:
                    this.esquerda = control.Left;
                    this.direita = control.Right;
                    this.largura = control.Width;
                    this.altura = control.Height;
                    break;
            }
        }

        public override int ConvertPixelToDevice(int value)
        {
            return (int) Utils.ConvertPixelToDisplay(value);
        }

        public void DrawControl(CustomControl control, int offset)
        {
            if (control.Type == ControlType.ControlLine)
            {
                this.DrawLine((RLine) control, offset);
            }
        }

        private void DrawLine(RLine control, int offset)
        {
            int num = offset + ((int)Utils.ConvertPixelToDisplay(control.垂直位置));
            int esquerda = 0;
            int num3 = 0;
            int direita = 0;
            int num5 = 0;
            int num6 = offset + ((int)Utils.ConvertPixelToDisplay(control.垂直位置));
            int num7 = num6 + this.altura;
            switch (control.对齐方式)
            {
                case LineAlignment.靠顶:
                    esquerda = this.esquerda;
                    num3 = num6;
                    direita = this.direita;
                    num5 = num;
                    break;

                case LineAlignment.靠底:
                    esquerda = this.esquerda;
                    num3 = num7 - 1;
                    direita = this.direita;
                    num5 = num7 - 1;
                    break;

                case LineAlignment.靠左:
                    esquerda = this.esquerda;
                    num3 = num6;
                    direita = this.esquerda;
                    num5 = num7;
                    break;

                case LineAlignment.靠右:
                    esquerda = this.direita - 1;
                    num3 = num6;
                    direita = this.direita - 1;
                    num5 = num7;
                    break;
            }
            this.DrawLine(this.page, control, esquerda, num3, direita, num5);
        }

        private void DrawLine(Graphics g, RLine control, int x1, int y1, int x2, int y2)
        {
            Pen pen = new Pen(control.ForeColor, control.线宽);
            if (control.线段类型 == LineStyle.虚线)
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

        public Bitmap ImagePage()
        {
            return new Bitmap(base.DefaultPageSettings.PaperSize.Width, base.DefaultPageSettings.PaperSize.Height);
        }

        public Graphics newPage()
        {
            Bitmap page = this.ImagePage();
            base.cache.AddPage(page);
            Graphics graphics = Graphics.FromImage(page);
            graphics.PageUnit = GraphicsUnit.Display;
            graphics.PageScale = 1f;
            graphics.Clear(Color.White);
            return graphics;
        }

        public override void Show(Report report)
        {
            FormPreviewScreen.Show(report);
        }

        public override void Start()
        {
            if (this.StartPrint != null)
            {
                this.StartPrint();
            }
            while (base.hasmorepage)
            {
                if (this.PageGenerate != null)
                {
                    this.page = this.newPage();
                    this.PageGenerate(this.page);
                }
            }
            if (this.EndPrint != null)
            {
                this.EndPrint();
            }
        }

        public Hashtable Pages
        {
            get
            {
                return base.cache.Pages();
            }
        }
    }
}

