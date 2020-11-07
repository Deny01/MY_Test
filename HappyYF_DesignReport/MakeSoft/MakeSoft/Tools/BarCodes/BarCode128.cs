namespace MakeSoft.Tools.BarCodes
{
    using System;
    using System.Drawing;

    public class BarCode128 : BarCode
    {
        private int _barWeight;
        private static readonly int[,] cPatterns = new int[,] { 
            { 2, 1, 2, 2, 2, 2, 0, 0 }, { 2, 2, 2, 1, 2, 2, 0, 0 }, { 2, 2, 2, 2, 2, 1, 0, 0 }, { 1, 2, 1, 2, 2, 3, 0, 0 }, { 1, 2, 1, 3, 2, 2, 0, 0 }, { 1, 3, 1, 2, 2, 2, 0, 0 }, { 1, 2, 2, 2, 1, 3, 0, 0 }, { 1, 2, 2, 3, 1, 2, 0, 0 }, { 1, 3, 2, 2, 1, 2, 0, 0 }, { 2, 2, 1, 2, 1, 3, 0, 0 }, { 2, 2, 1, 3, 1, 2, 0, 0 }, { 2, 3, 1, 2, 1, 2, 0, 0 }, { 1, 1, 2, 2, 3, 2, 0, 0 }, { 1, 2, 2, 1, 3, 2, 0, 0 }, { 1, 2, 2, 2, 3, 1, 0, 0 }, { 1, 1, 3, 2, 2, 2, 0, 0 }, 
            { 1, 2, 3, 1, 2, 2, 0, 0 }, { 1, 2, 3, 2, 2, 1, 0, 0 }, { 2, 2, 3, 2, 1, 1, 0, 0 }, { 2, 2, 1, 1, 3, 2, 0, 0 }, { 2, 2, 1, 2, 3, 1, 0, 0 }, { 2, 1, 3, 2, 1, 2, 0, 0 }, { 2, 2, 3, 1, 1, 2, 0, 0 }, { 3, 1, 2, 1, 3, 1, 0, 0 }, { 3, 1, 1, 2, 2, 2, 0, 0 }, { 3, 2, 1, 1, 2, 2, 0, 0 }, { 3, 2, 1, 2, 2, 1, 0, 0 }, { 3, 1, 2, 2, 1, 2, 0, 0 }, { 3, 2, 2, 1, 1, 2, 0, 0 }, { 3, 2, 2, 2, 1, 1, 0, 0 }, { 2, 1, 2, 1, 2, 3, 0, 0 }, { 2, 1, 2, 3, 2, 1, 0, 0 }, 
            { 2, 3, 2, 1, 2, 1, 0, 0 }, { 1, 1, 1, 3, 2, 3, 0, 0 }, { 1, 3, 1, 1, 2, 3, 0, 0 }, { 1, 3, 1, 3, 2, 1, 0, 0 }, { 1, 1, 2, 3, 1, 3, 0, 0 }, { 1, 3, 2, 1, 1, 3, 0, 0 }, { 1, 3, 2, 3, 1, 1, 0, 0 }, { 2, 1, 1, 3, 1, 3, 0, 0 }, { 2, 3, 1, 1, 1, 3, 0, 0 }, { 2, 3, 1, 3, 1, 1, 0, 0 }, { 1, 1, 2, 1, 3, 3, 0, 0 }, { 1, 1, 2, 3, 3, 1, 0, 0 }, { 1, 3, 2, 1, 3, 1, 0, 0 }, { 1, 1, 3, 1, 2, 3, 0, 0 }, { 1, 1, 3, 3, 2, 1, 0, 0 }, { 1, 3, 3, 1, 2, 1, 0, 0 }, 
            { 3, 1, 3, 1, 2, 1, 0, 0 }, { 2, 1, 1, 3, 3, 1, 0, 0 }, { 2, 3, 1, 1, 3, 1, 0, 0 }, { 2, 1, 3, 1, 1, 3, 0, 0 }, { 2, 1, 3, 3, 1, 1, 0, 0 }, { 2, 1, 3, 1, 3, 1, 0, 0 }, { 3, 1, 1, 1, 2, 3, 0, 0 }, { 3, 1, 1, 3, 2, 1, 0, 0 }, { 3, 3, 1, 1, 2, 1, 0, 0 }, { 3, 1, 2, 1, 1, 3, 0, 0 }, { 3, 1, 2, 3, 1, 1, 0, 0 }, { 3, 3, 2, 1, 1, 1, 0, 0 }, { 3, 1, 4, 1, 1, 1, 0, 0 }, { 2, 2, 1, 4, 1, 1, 0, 0 }, { 4, 3, 1, 1, 1, 1, 0, 0 }, { 1, 1, 1, 2, 2, 4, 0, 0 }, 
            { 1, 1, 1, 4, 2, 2, 0, 0 }, { 1, 2, 1, 1, 2, 4, 0, 0 }, { 1, 2, 1, 4, 2, 1, 0, 0 }, { 1, 4, 1, 1, 2, 2, 0, 0 }, { 1, 4, 1, 2, 2, 1, 0, 0 }, { 1, 1, 2, 2, 1, 4, 0, 0 }, { 1, 1, 2, 4, 1, 2, 0, 0 }, { 1, 2, 2, 1, 1, 4, 0, 0 }, { 1, 2, 2, 4, 1, 1, 0, 0 }, { 1, 4, 2, 1, 1, 2, 0, 0 }, { 1, 4, 2, 2, 1, 1, 0, 0 }, { 2, 4, 1, 2, 1, 1, 0, 0 }, { 2, 2, 1, 1, 1, 4, 0, 0 }, { 4, 1, 3, 1, 1, 1, 0, 0 }, { 2, 4, 1, 1, 1, 2, 0, 0 }, { 1, 3, 4, 1, 1, 1, 0, 0 }, 
            { 1, 1, 1, 2, 4, 2, 0, 0 }, { 1, 2, 1, 1, 4, 2, 0, 0 }, { 1, 2, 1, 2, 4, 1, 0, 0 }, { 1, 1, 4, 2, 1, 2, 0, 0 }, { 1, 2, 4, 1, 1, 2, 0, 0 }, { 1, 2, 4, 2, 1, 1, 0, 0 }, { 4, 1, 1, 2, 1, 2, 0, 0 }, { 4, 2, 1, 1, 1, 2, 0, 0 }, { 4, 2, 1, 2, 1, 1, 0, 0 }, { 2, 1, 2, 1, 4, 1, 0, 0 }, { 2, 1, 4, 1, 2, 1, 0, 0 }, { 4, 1, 2, 1, 2, 1, 0, 0 }, { 1, 1, 1, 1, 4, 3, 0, 0 }, { 1, 1, 1, 3, 4, 1, 0, 0 }, { 1, 3, 1, 1, 4, 1, 0, 0 }, { 1, 1, 4, 1, 1, 3, 0, 0 }, 
            { 1, 1, 4, 3, 1, 1, 0, 0 }, { 4, 1, 1, 1, 1, 3, 0, 0 }, { 4, 1, 1, 3, 1, 1, 0, 0 }, { 1, 1, 3, 1, 4, 1, 0, 0 }, { 1, 1, 4, 1, 3, 1, 0, 0 }, { 3, 1, 1, 1, 4, 1, 0, 0 }, { 4, 1, 1, 1, 3, 1, 0, 0 }, { 2, 1, 1, 4, 1, 2, 0, 0 }, { 2, 1, 1, 2, 1, 4, 0, 0 }, { 2, 1, 1, 2, 3, 2, 0, 0 }, { 2, 3, 3, 1, 1, 1, 2, 0 }
         };
        private const int cQuietWidth = 10;

        public BarCode128()
        {
            base.code = "123456789";
            base.Font = new Font("Arial", 10f);
            base.barcodeheight = 50f;
            this.BarCodeWidth = 200f;
            base.ShowCode = true;
            this.BarWeight = 2;
        }

        public override void DrawCode(Graphics g)
        {
            this.DrawCode(g, 0, 0);
        }

        public override void DrawCode(Graphics g, Point pt)
        {
            this.DrawCode(g, pt.X, pt.Y);
        }

        public override void DrawCode(Graphics g, int x, int y)
        {
            if (!this.CheckBarCode())
            {
                base.DrawStringErro(g, x, y);
            }
            else
            {
                this.PrepareCode();
                int[] codes = this.content.Codes;
                g.FillRectangle(Brushes.White, (float) x, (float) y, this.BarCodeWidth, this.BarCodeHeight);
                SizeF ef = g.MeasureString(this.Code, base.Font);
                if (base.ShowCode)
                {
                    base.internalbarcodeheight = ((int) this.BarCodeHeight) - ((int) ef.Height);
                }
                else
                {
                    base.internalbarcodeheight = (int) this.BarCodeHeight;
                }
                int num = x;
                for (int i = 0; i < codes.Length; i++)
                {
                    int num3 = codes[i];
                    for (int j = 0; j < 8; j += 2)
                    {
                        int num5 = cPatterns[num3, j] * this.BarWeight;
                        int num6 = cPatterns[num3, j + 1] * this.BarWeight;
                        if (num5 > 0)
                        {
                            g.FillRectangle(Brushes.Black, (float) num, (float) y, (float) num5, base.internalbarcodeheight);
                        }
                        num += num5 + num6;
                    }
                }
                if (base.ShowCode)
                {
                    x += ((num - x) - ((int) ef.Width)) / 2;
                    g.DrawString(this.Code, base.Font, Brushes.Black, (float) x, y + base.internalbarcodeheight);
                }
            }
        }

        public override void DrawCode(Graphics g, float x, float y)
        {
            this.DrawCode(g, (int) x, (int) y);
        }

        public override Image GetImage()
        {
            this.PrepareCode();
            Image image = new Bitmap((int) this.BarCodeWidth, (int) this.BarCodeHeight);
            using (Graphics graphics = Graphics.FromImage(image))
            {
                graphics.FillRectangle(Brushes.White, 0f, 0f, this.BarCodeWidth, this.BarCodeHeight);
                this.DrawCode(graphics, 0, 0);
            }
            return image;
        }

        private void PrepareCode()
        {
            int num = (((this.content.Codes.Length - 3) * 11) + 0x23) * this.BarWeight;
            int num2 = Convert.ToInt32(Math.Ceiling((double) (Convert.ToSingle(num) * 0.15f)));
            this.BarCodeWidth = num;
            this.BarCodeHeight = num2;
        }

        public int BarWeight
        {
            get
            {
                return this._barWeight;
            }
            set
            {
                this._barWeight = value;
            }
        }

        private Code128Content content
        {
            get
            {
                return new Code128Content(this.Code);
            }
        }
    }
}

