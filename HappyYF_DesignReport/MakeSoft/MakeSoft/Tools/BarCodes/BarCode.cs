namespace MakeSoft.Tools.BarCodes
{
    using System;
    using System.Drawing;
    using System.Drawing.Imaging;

    public abstract class BarCode
    {
        private System.Drawing.Font _font;
        protected float barcodeheight;
        protected float barcodewidth;
        protected string code;
        protected int cursor;
        protected float internalbarcodeheight;
        protected bool showcode;

        protected BarCode()
        {
        }

        public virtual bool CheckBarCode()
        {
            return !string.IsNullOrEmpty(this.code);
        }

        public virtual void DrawCode(Graphics g)
        {
        }

        public virtual void DrawCode(Graphics g, Point pt)
        {
        }

        public virtual void DrawCode(Graphics g, int x, int y)
        {
        }

        public virtual void DrawCode(Graphics g, float x, float y)
        {
        }

        protected void DrawStringErro(Graphics g, int x, int y)
        {
            g.DrawString("Codigo de barras inv\x00e1lido!", this.Font, Brushes.Red, (float) (x + 10), (float) (y + 10));
        }

        public virtual Image GetImage()
        {
            return null;
        }

        public static BarCode Instance(BarCodeType type)
        {
            switch (type)
            {
                case BarCodeType.Ean13:
                    return new BarCodeEan13();

                case BarCodeType.Code128:
                    return new BarCode128();

                case BarCodeType.Code39:
                    return new BarCode39();

                case BarCodeType.Code2of5:
                    return new BarCode2of5();

                case BarCodeType.Code2of7:
                    return new BarCode2of5();

                case BarCodeType.Code93:
                    return new BarCode93();

                case BarCodeType.Ean8:
                    return new BarCodeEan13();

                case BarCodeType.UPC_A:
                    return new BarCodeUPCA();

                case BarCodeType.UPC_E:
                    return new BarCodeUPCE();

                case BarCodeType.ISBN:
                    return new BarCodeISBN();
            }
            return new BarCode2of5();
        }

        public virtual void SaveImage(string file)
        {
            Bitmap image = new Bitmap((int) this.BarCodeWidth, (int) this.BarCodeHeight, PixelFormat.Format32bppArgb);
            Graphics g = Graphics.FromImage(image);
            g.FillRectangle(Brushes.White, 0f, 0f, this.BarCodeWidth, this.BarCodeHeight);
            this.DrawCode(g);
            image.Save(file);
        }

        public virtual float BarCodeHeight
        {
            get
            {
                return this.barcodeheight;
            }
            set
            {
                this.barcodeheight = value;
            }
        }

        public virtual float BarCodeWidth
        {
            get
            {
                return this.barcodewidth;
            }
            set
            {
                this.barcodewidth = value;
            }
        }

        public virtual string Code
        {
            get
            {
                return this.code;
            }
            set
            {
                this.code = value;
            }
        }

        public System.Drawing.Font Font
        {
            get
            {
                return this._font;
            }
            set
            {
                this._font = value;
            }
        }

        public bool ShowCode
        {
            get
            {
                return this.showcode;
            }
            set
            {
                this.showcode = value;
            }
        }
    }
}

