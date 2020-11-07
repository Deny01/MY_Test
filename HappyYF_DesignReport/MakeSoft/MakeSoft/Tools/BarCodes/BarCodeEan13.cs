namespace MakeSoft.Tools.BarCodes
{
    using MakeSoft.Tools;
    using System;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Text;

    public class BarCodeEan13 : BarCode
    {
        private string[] _aEvenLeft;
        private string[] _aOddLeft;
        private string[] _aRight;
        private float _fFontSize;
        private float _fMaximumAllowableScale;
        private float _fMinimumAllowableScale;
        private float _fScale;
        private string _sChecksumDigit;
        private string _sLeadTail;
        private string _sName;
        private string _sQuiteZone;
        private string _sSeparator;

        public BarCodeEan13()
        {
            this._sName = "EAN13";
            this._fMinimumAllowableScale = 0.8f;
            this._fMaximumAllowableScale = 2f;
            this._fFontSize = 8f;
            this._fScale = 1f;
            this._aOddLeft = new string[] { "0001101", "0011001", "0010011", "0111101", "0100011", "0110001", "0101111", "0111011", "0110111", "0001011" };
            this._aEvenLeft = new string[] { "0100111", "0110011", "0011011", "0100001", "0011101", "0111001", "0000101", "0010001", "0001001", "0010111" };
            this._aRight = new string[] { "1110010", "1100110", "1101100", "1000010", "1011100", "1001110", "1010000", "1000100", "1001000", "1110100" };
            this._sQuiteZone = "000000000";
            this._sLeadTail = "101";
            this._sSeparator = "01010";
            this.Initialize();
        }

        public BarCodeEan13(string code)
        {
            this._sName = "EAN13";
            this._fMinimumAllowableScale = 0.8f;
            this._fMaximumAllowableScale = 2f;
            this._fFontSize = 8f;
            this._fScale = 1f;
            this._aOddLeft = new string[] { "0001101", "0011001", "0010011", "0111101", "0100011", "0110001", "0101111", "0111011", "0110111", "0001011" };
            this._aEvenLeft = new string[] { "0100111", "0110011", "0011011", "0100001", "0011101", "0111001", "0000101", "0010001", "0001001", "0010111" };
            this._aRight = new string[] { "1110010", "1100110", "1101100", "1000010", "1011100", "1001110", "1010000", "1000100", "1001000", "1110100" };
            this._sQuiteZone = "000000000";
            this._sLeadTail = "101";
            this._sSeparator = "01010";
            this.Initialize();
            base.code = code;
        }

        public void CalculateChecksumDigit()
        {
            string str = this.Code.Substring(0, 12);
            int num = 0;
            int num2 = 0;
            for (int i = str.Length; i >= 1; i--)
            {
                num2 = Convert.ToInt32(str.Substring(i - 1, 1));
                if ((i % 2) == 0)
                {
                    num += num2 * 3;
                }
                else
                {
                    num += num2;
                }
            }
            this._sChecksumDigit = ((10 - (num % 10)) % 10).ToString();
        }

        public override bool CheckBarCode()
        {
            if (!base.CheckBarCode())
            {
                return false;
            }
            if (this.Code.Length != 13)
            {
                return false;
            }
            char[] chArray = this.Code.ToCharArray();
            foreach (char ch in chArray)
            {
                if (!char.IsDigit(ch))
                {
                    return false;
                }
            }
            return this.CheckSumDigit();
        }

        public bool CheckSumDigit()
        {
            this.CalculateChecksumDigit();
            return this.Code.Substring(12, 1).Equals(this._sChecksumDigit);
        }

        private string ConvertLeftPattern(string sLeft)
        {
            switch (sLeft.Substring(0, 1))
            {
                case "0":
                    return this.CountryCode0(sLeft.Substring(1));

                case "1":
                    return this.CountryCode1(sLeft.Substring(1));

                case "2":
                    return this.CountryCode2(sLeft.Substring(1));

                case "3":
                    return this.CountryCode3(sLeft.Substring(1));

                case "4":
                    return this.CountryCode4(sLeft.Substring(1));

                case "5":
                    return this.CountryCode5(sLeft.Substring(1));

                case "6":
                    return this.CountryCode6(sLeft.Substring(1));

                case "7":
                    return this.CountryCode7(sLeft.Substring(1));

                case "8":
                    return this.CountryCode8(sLeft.Substring(1));

                case "9":
                    return this.CountryCode9(sLeft.Substring(1));
            }
            return "";
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

        private string CountryCode0(string sLeft)
        {
            return this.ConvertToDigitPatterns(sLeft, this._aOddLeft);
        }

        private string CountryCode1(string sLeft)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(this.ConvertToDigitPatterns(sLeft.Substring(0, 1), this._aOddLeft));
            builder.Append(this.ConvertToDigitPatterns(sLeft.Substring(1, 1), this._aOddLeft));
            builder.Append(this.ConvertToDigitPatterns(sLeft.Substring(2, 1), this._aEvenLeft));
            builder.Append(this.ConvertToDigitPatterns(sLeft.Substring(3, 1), this._aOddLeft));
            builder.Append(this.ConvertToDigitPatterns(sLeft.Substring(4, 1), this._aEvenLeft));
            builder.Append(this.ConvertToDigitPatterns(sLeft.Substring(5, 1), this._aEvenLeft));
            return builder.ToString();
        }

        private string CountryCode2(string sLeft)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(this.ConvertToDigitPatterns(sLeft.Substring(0, 1), this._aOddLeft));
            builder.Append(this.ConvertToDigitPatterns(sLeft.Substring(1, 1), this._aOddLeft));
            builder.Append(this.ConvertToDigitPatterns(sLeft.Substring(2, 1), this._aEvenLeft));
            builder.Append(this.ConvertToDigitPatterns(sLeft.Substring(3, 1), this._aEvenLeft));
            builder.Append(this.ConvertToDigitPatterns(sLeft.Substring(4, 1), this._aOddLeft));
            builder.Append(this.ConvertToDigitPatterns(sLeft.Substring(5, 1), this._aEvenLeft));
            return builder.ToString();
        }

        private string CountryCode3(string sLeft)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(this.ConvertToDigitPatterns(sLeft.Substring(0, 1), this._aOddLeft));
            builder.Append(this.ConvertToDigitPatterns(sLeft.Substring(1, 1), this._aOddLeft));
            builder.Append(this.ConvertToDigitPatterns(sLeft.Substring(2, 1), this._aEvenLeft));
            builder.Append(this.ConvertToDigitPatterns(sLeft.Substring(3, 1), this._aEvenLeft));
            builder.Append(this.ConvertToDigitPatterns(sLeft.Substring(4, 1), this._aEvenLeft));
            builder.Append(this.ConvertToDigitPatterns(sLeft.Substring(5, 1), this._aOddLeft));
            return builder.ToString();
        }

        private string CountryCode4(string sLeft)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(this.ConvertToDigitPatterns(sLeft.Substring(0, 1), this._aOddLeft));
            builder.Append(this.ConvertToDigitPatterns(sLeft.Substring(1, 1), this._aEvenLeft));
            builder.Append(this.ConvertToDigitPatterns(sLeft.Substring(2, 1), this._aOddLeft));
            builder.Append(this.ConvertToDigitPatterns(sLeft.Substring(3, 1), this._aOddLeft));
            builder.Append(this.ConvertToDigitPatterns(sLeft.Substring(4, 1), this._aEvenLeft));
            builder.Append(this.ConvertToDigitPatterns(sLeft.Substring(5, 1), this._aEvenLeft));
            return builder.ToString();
        }

        private string CountryCode5(string sLeft)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(this.ConvertToDigitPatterns(sLeft.Substring(0, 1), this._aOddLeft));
            builder.Append(this.ConvertToDigitPatterns(sLeft.Substring(1, 1), this._aEvenLeft));
            builder.Append(this.ConvertToDigitPatterns(sLeft.Substring(2, 1), this._aEvenLeft));
            builder.Append(this.ConvertToDigitPatterns(sLeft.Substring(3, 1), this._aOddLeft));
            builder.Append(this.ConvertToDigitPatterns(sLeft.Substring(4, 1), this._aOddLeft));
            builder.Append(this.ConvertToDigitPatterns(sLeft.Substring(5, 1), this._aEvenLeft));
            return builder.ToString();
        }

        private string CountryCode6(string sLeft)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(this.ConvertToDigitPatterns(sLeft.Substring(0, 1), this._aOddLeft));
            builder.Append(this.ConvertToDigitPatterns(sLeft.Substring(1, 1), this._aEvenLeft));
            builder.Append(this.ConvertToDigitPatterns(sLeft.Substring(2, 1), this._aEvenLeft));
            builder.Append(this.ConvertToDigitPatterns(sLeft.Substring(3, 1), this._aEvenLeft));
            builder.Append(this.ConvertToDigitPatterns(sLeft.Substring(4, 1), this._aOddLeft));
            builder.Append(this.ConvertToDigitPatterns(sLeft.Substring(5, 1), this._aOddLeft));
            return builder.ToString();
        }

        private string CountryCode7(string sLeft)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(this.ConvertToDigitPatterns(sLeft.Substring(0, 1), this._aOddLeft));
            builder.Append(this.ConvertToDigitPatterns(sLeft.Substring(1, 1), this._aEvenLeft));
            builder.Append(this.ConvertToDigitPatterns(sLeft.Substring(2, 1), this._aOddLeft));
            builder.Append(this.ConvertToDigitPatterns(sLeft.Substring(3, 1), this._aEvenLeft));
            builder.Append(this.ConvertToDigitPatterns(sLeft.Substring(4, 1), this._aOddLeft));
            builder.Append(this.ConvertToDigitPatterns(sLeft.Substring(5, 1), this._aEvenLeft));
            return builder.ToString();
        }

        private string CountryCode8(string sLeft)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(this.ConvertToDigitPatterns(sLeft.Substring(0, 1), this._aOddLeft));
            builder.Append(this.ConvertToDigitPatterns(sLeft.Substring(1, 1), this._aEvenLeft));
            builder.Append(this.ConvertToDigitPatterns(sLeft.Substring(2, 1), this._aOddLeft));
            builder.Append(this.ConvertToDigitPatterns(sLeft.Substring(3, 1), this._aEvenLeft));
            builder.Append(this.ConvertToDigitPatterns(sLeft.Substring(4, 1), this._aEvenLeft));
            builder.Append(this.ConvertToDigitPatterns(sLeft.Substring(5, 1), this._aOddLeft));
            return builder.ToString();
        }

        private string CountryCode9(string sLeft)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(this.ConvertToDigitPatterns(sLeft.Substring(0, 1), this._aOddLeft));
            builder.Append(this.ConvertToDigitPatterns(sLeft.Substring(1, 1), this._aEvenLeft));
            builder.Append(this.ConvertToDigitPatterns(sLeft.Substring(2, 1), this._aEvenLeft));
            builder.Append(this.ConvertToDigitPatterns(sLeft.Substring(3, 1), this._aOddLeft));
            builder.Append(this.ConvertToDigitPatterns(sLeft.Substring(4, 1), this._aEvenLeft));
            builder.Append(this.ConvertToDigitPatterns(sLeft.Substring(5, 1), this._aOddLeft));
            return builder.ToString();
        }

        public Bitmap CreateBitmap()
        {
            float num = (this.BarCodeWidth * this.Scale) * 100f;
            float num2 = (this.BarCodeHeight * this.Scale) * 100f;
            Bitmap image = new Bitmap((int) num, (int) num2);
            Graphics g = Graphics.FromImage(image);
            this.DrawCode(g, new Point(0, 0));
            g.Dispose();
            return image;
        }

        public override void DrawCode(Graphics g)
        {
            this.DrawCode(g, 0, 0);
        }

        public override void DrawCode(Graphics g, Point pt)
        {
            if (!this.CheckBarCode())
            {
                base.DrawStringErro(g, pt.X, pt.Y);
            }
            else
            {
                pt = new Point(FunctionsGraphics.ConvertDisplayToMilimetro(pt.X), FunctionsGraphics.ConvertDisplayToMilimetro(pt.Y));
                float num = this.BarCodeWidth * this.Scale;
                float height = this.BarCodeHeight * this.Scale;
                float width = num / 113f;
                GraphicsState gstate = g.Save();
                g.PageUnit = GraphicsUnit.Millimeter;
                g.PageScale = 1f;
                SolidBrush brush = new SolidBrush(Color.Black);
                float x = pt.X;
                StringBuilder builder = new StringBuilder();
                float num5 = pt.X;
                float y = pt.Y;
                float num7 = 0f;
                Font font = new Font("Arial", this._fFontSize * this.Scale);
                string code = this.Code;
                string str2 = "";
                str2 = this.ConvertLeftPattern(code.Substring(0, 7));
                builder.AppendFormat("{0}{1}{2}{3}{4}{1}{0}", new object[] { this._sQuiteZone, this._sLeadTail, str2, this._sSeparator, this.ConvertToDigitPatterns(code.Substring(7), this._aRight) });
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
                        if (((i > 12) && (i < 0x37)) || ((i > 0x39) && (i < 0x65)))
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
                x = num5 - g.MeasureString(this.Code.Substring(0, 1), font).Width;
                float num10 = y + (height - num8);
                g.DrawString(code.Substring(0, 1), font, brush, new PointF(x, num10));
                x += (g.MeasureString(code.Substring(0, 1), font).Width + (43f * width)) - g.MeasureString(code.Substring(1, 6), font).Width;
                g.DrawString(code.Substring(1, 6), font, brush, new PointF(x, num10));
                x += g.MeasureString(code.Substring(1, 6), font).Width + (11f * width);
                g.DrawString(code.Substring(7), font, brush, new PointF(x, num10));
                g.Restore(gstate);
            }
        }

        public override void DrawCode(Graphics g, int x, int y)
        {
            this.DrawCode(g, new Point(x, y));
        }

        public override void DrawCode(Graphics g, float x, float y)
        {
            this.DrawCode(g, (int) x, (int) y);
        }

        private void Initialize()
        {
            base.barcodewidth = 37.29f;
            base.barcodeheight = 25.93f;
            this.Scale = 1f;
            base.Font = new Font("Arial", 10f);
            base.code = "789100025260";
            this.CalculateChecksumDigit();
            base.code = base.code + this._sChecksumDigit;
        }

        public float FontSize
        {
            get
            {
                return this._fFontSize;
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
                    throw new Exception("O valor da escala esta forma da faixa minima e maxima.  O valor deve esta entre " + this._fMinimumAllowableScale.ToString() + " e " + this._fMaximumAllowableScale.ToString());
                }
                this._fScale = value;
            }
        }
    }
}

