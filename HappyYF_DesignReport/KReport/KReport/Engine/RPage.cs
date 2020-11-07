namespace KReport.Engine
{
    using KReport.Controls;
    using System;
    using System.Drawing;
    using System.Windows.Forms;

    internal class RPage : Control
    {
        private double DPIX;
        private double DPIY;
        private bool landScape;
        private int marginBottom;
        private int marginLeft;
        private int marginRigth;
        private int marginTop;
        private int pageHeight;
        private int pageWidth;
        private string papername;
        private Units unidadeMedida;

        public RPage()
        {
            Graphics graphics = base.CreateGraphics();
            graphics.PageUnit = GraphicsUnit.Pixel;
            this.DPIX = graphics.DpiX;
            this.DPIY = graphics.DpiY;
            graphics.Dispose();
            this.unidadeMedida = Units.Centimetro;
            this.papername = "A4";
            this.pageHeight = Convert.ToInt16((double) (29.7 * (this.DPIY / 2.54)));
            this.pageWidth = Convert.ToInt16((double) (21.0 * (this.DPIX / 2.54)));
            this.marginBottom = Convert.ToInt16((double) (1.0 * (this.DPIX / 2.54)));
            this.marginTop = Convert.ToInt16((double) (1.0 * (this.DPIX / 2.54)));
            this.marginLeft = Convert.ToInt16((double) (1.0 * (this.DPIX / 2.54)));
            this.marginRigth = Convert.ToInt16((double) (1.0 * (this.DPIX / 2.54)));
            this.landScape = false;
        }

        public int AreaPrintedHeigth
        {
            get
            {
                return (this.PageHeight - (this.MarginBottom + this.MarginTop));
            }
        }

        public int AreaPrintedWidth
        {
            get
            {
                return (this.PageWidth - (this.MarginLeft + this.MarginRigth));
            }
        }

        public Units Escala
        {
            get
            {
                return this.unidadeMedida;
            }
            set
            {
                this.unidadeMedida = value;
            }
        }

        public bool LandScape
        {
            get
            {
                return this.landScape;
            }
            set
            {
                this.landScape = value;
            }
        }

        public int MarginBottom
        {
            get
            {
                return this.marginBottom;
            }
            set
            {
                this.marginBottom = value;
            }
        }

        public int MarginLeft
        {
            get
            {
                return this.marginLeft;
            }
            set
            {
                this.marginLeft = value;
            }
        }

        public int MarginRigth
        {
            get
            {
                return this.marginRigth;
            }
            set
            {
                this.marginRigth = value;
            }
        }

        public int MarginTop
        {
            get
            {
                return this.marginTop;
            }
            set
            {
                this.marginTop = value;
            }
        }

        public int PageHeight
        {
            get
            {
                return this.pageHeight;
            }
            set
            {
                this.pageHeight = value;
            }
        }

        public int PageWidth
        {
            get
            {
                return this.pageWidth;
            }
            set
            {
                this.pageWidth = value;
            }
        }

        public string PaperName
        {
            get
            {
                return this.papername;
            }
            set
            {
                this.papername = value;
            }
        }
    }
}

