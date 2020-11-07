namespace MakeSoft.Tools.BarCodes
{
    using MakeSoft.Tools;
    using System;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Text;

    public class BarCodeUPCE : BarCode
    {
        private string[] _aLeft = new string[] { "0001101", "0011001", "0010011", "0111101", "0100011", "0110001", "0101111", "0111011", "0110111", "0001011" };
        private string[] _aRight = new string[] { "1110010", "1100110", "1101100", "1000010", "1011100", "1001110", "1010000", "1000100", "1001000", "1110100" };
        private float _fFontSize = 8f;
        private float _fHeight = 1.02f;
        private float _fMaximumAllowableScale = 2f;
        private float _fMinimumAllowableScale = 0.8f;
        private float _fScale = 1f;
        private float _fWidth = 1.469f;
        private string _sChecksumDigit;
        private string _sLeadTail = "101";
        private string _sManufacturerCode;
        private string _sName = "UPC-A";
        private string _sProductCode;
        private string _sProductType = "0";
        private string _sQuiteZone = "0000000000";
        private string _sSeparator = "01010";

        public BarCodeUPCE()
        {
            this.Code = "012345678909";
            base.barcodeheight = 50f;
            base.barcodewidth = 100f;
        }

        public void CalculateChecksumDigit()
        {
            string code = this.Code;
            int num = 0;
            int num2 = 0;
            for (int i = 1; i <= code.Length; i++)
            {
                num2 = Convert.ToInt32(code.Substring(i - 1, 1));
                if ((i % 2) == 0)
                {
                    num += num2;
                }
                else
                {
                    num += num2 * 3;
                }
            }
            this._sChecksumDigit = ((10 - (num % 10)) % 10).ToString();
        }

        private string ConvertToDigitPatterns(string inputNumber, string[] patterns)
        {
            StringBuilder builder = new StringBuilder();
            int index = 0;
            for (int i = 0; i < inputNumber.Length; i++)
            {
                index = Convert.ToInt32(inputNumber.Substring(i, 1));
                builder.Append(patterns[index]);
            }
            return builder.ToString();
        }

        public Bitmap CreateBitmap()
        {
            float num = (this.Width * this.Scale) * 100f;
            float num2 = (this.Height * this.Scale) * 100f;
            Bitmap image = new Bitmap((int) num, (int) num2);
            Graphics g = Graphics.FromImage(image);
            this.DrawUpcaBarcode(g, new Point(0, 0));
            g.Dispose();
            return image;
        }

        public void DrawUpcaBarcode(Graphics g, Point pt)
        {
            this._sProductType = this.Code.Substring(0, 1);
            this._sManufacturerCode = this.Code.Substring(1, 5);
            this._sProductCode = this.Code.Substring(6, 5);
            this._sChecksumDigit = this.Code.Substring(11, 1);
            pt = new Point((int) FunctionsGraphics.ConvertPointToInch((float) pt.X), (int) FunctionsGraphics.ConvertPointToInch((float) pt.Y));
            float num = this.Width * this.Scale;
            float height = this.Height * this.Scale;
            float width = num / 113f;
            GraphicsState gstate = g.Save();
            g.PageUnit = GraphicsUnit.Inch;
            g.PageScale = 1f;
            SolidBrush brush = new SolidBrush(Color.Black);
            float x = pt.X;
            StringBuilder builder = new StringBuilder();
            float num5 = pt.X;
            float y = pt.Y;
            float num7 = 0f;
            Font font = new Font("Arial", this._fFontSize * this.Scale);
            this.CalculateChecksumDigit();
            builder.AppendFormat("{0}{1}{2}{3}{4}{5}{6}{1}{0}", new object[] { this._sQuiteZone, this._sLeadTail, this.ConvertToDigitPatterns(this._sProductType, this._aLeft), this.ConvertToDigitPatterns(this._sManufacturerCode, this._aLeft), this._sSeparator, this.ConvertToDigitPatterns(this._sProductCode, this._aRight), this.ConvertToDigitPatterns(this._sChecksumDigit, this._aRight) });
            string text = builder.ToString();
            float num8 = g.MeasureString(text, font).Height;
            for (int i = 0; i < builder.Length; i++)
            {
                if (text.Substring(i, 1) == "1")
                {
                    if (num5 == pt.X)
                    {
                        num5 = x;
                    }
                    if (((i > 0x13) && (i < 0x38)) || ((i > 0x3b) && (i < 0x5f)))
                    {
                        g.FillRectangle(brush, x, y, width, height - num8);
                    }
                    else
                    {
                        g.FillRectangle(brush, x, y, width, height);
                    }
                }
                x += width;
                num7 = x;
            }
            x = num5 - g.MeasureString(this._sProductType, font).Width;
            float num10 = y + (height - num8);
            g.DrawString(this._sProductType, font, brush, new PointF(x, num10));
            x += (g.MeasureString(this._sProductType, font).Width + (45f * width)) - g.MeasureString(this._sManufacturerCode, font).Width;
            g.DrawString(this._sManufacturerCode, font, brush, new PointF(x, num10));
            x += g.MeasureString(this._sManufacturerCode, font).Width + (5f * width);
            g.DrawString(this._sProductCode, font, brush, new PointF(x, num10));
            x += 46f * width;
            g.DrawString(this._sChecksumDigit, font, brush, new PointF(x, num10));
            g.Restore(gstate);
        }

        public float FontSize
        {
            get
            {
                return this._fFontSize;
            }
        }

        public float Height
        {
            get
            {
                return this._fHeight;
            }
        }

        public float MaximumAllowableScale
        {
            get
            {
                return this._fMaximumAllowableScale;
            }
        }

        public float MinimumAllowableScale
        {
            get
            {
                return this._fMinimumAllowableScale;
            }
        }

        public string Name
        {
            get
            {
                return this._sName;
            }
        }

        public float Scale
        {
            get
            {
                return this._fScale;
            }
            set
            {
                if ((value < this._fMinimumAllowableScale) || (value > this._fMaximumAllowableScale))
                {
                    throw new Exception("Valor da escala esta fora da faixa.  O valor deve esta entre  " + this._fMinimumAllowableScale.ToString() + " e " + this._fMaximumAllowableScale.ToString());
                }
                this._fScale = value;
            }
        }

        public float Width
        {
            get
            {
                return this._fWidth;
            }
        }
    }
}

