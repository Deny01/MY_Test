namespace MakeSoft.Tools.BarCodes
{
    using MakeSoft.Tools;
    using System;
    using System.Drawing;

    public class BarCode39 : MakeSoft.Tools.BarCodes.BarCode
    {
        private AlignType align = AlignType.Center;
        private string alphabet39 = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ-. $/+%*";
        private string[] coded39Char = new string[] { 
            "000110100", "100100001", "001100001", "101100000", "000110001", "100110000", "001110000", "000100101", "100100100", "001100100", "100001001", "001001001", "101001000", "000011001", "100011000", "001011000", 
            "000001101", "100001100", "001001100", "000011100", "100000011", "001000011", "101000010", "000010011", "100010010", "001010010", "000000111", "100000110", "001000110", "000010110", "110000001", "011000001", 
            "111000000", "010010001", "110010000", "011010000", "010000101", "110000100", "011000100", "010101000", "010100010", "010001010", "000101010", "010010100"
         };
        private Font footerFont = new Font("Courier", 8f);
        private Font headerFont = new Font("Courier", 8f);
        private string headerText = "BarCode Header";
        private bool showHeader;
        private BarCodeWeight weight = BarCodeWeight.Small;

        public BarCode39()
        {
            this.Initilize();
        }

        public override bool CheckBarCode()
        {
            if (!base.CheckBarCode())
            {
                return false;
            }
            for (int i = 0; i < base.code.Length; i++)
            {
                if ((this.alphabet39.IndexOf(base.code[i]) == -1) || (base.code[i] == '*'))
                {
                    return false;
                }
            }
            return true;
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
                int num2;
                string str = "0";
                string str2 = '*' + base.code.ToUpper() + '*';
                int length = str2.Length;
                string str3 = "";
                for (num2 = 0; num2 < length; num2++)
                {
                    if (num2 > 0)
                    {
                        str3 = str3 + str;
                    }
                    str3 = str3 + this.coded39Char[this.alphabet39.IndexOf(str2[num2])];
                }
                int num3 = str3.Length;
                int num4 = 0;
                double num5 = 3.0;
                for (num2 = 0; num2 < num3; num2++)
                {
                    if (str3[num2] == '1')
                    {
                        num4 += (int) (num5 * ((double) this.weight));
                    }
                    else
                    {
                        num4 += (int)this.weight;
                    }
                }
                int width = 0;
                int num7 = 0;
                SizeF ef = g.MeasureString(this.headerText, this.headerFont);
                SizeF ef2 = g.MeasureString(base.code, this.footerFont);
                int num8 = x;
                int num9 = x;
                num8 = (num4 - ((int) ef.Width)) / 2;
                num9 += (num4 - ((int) ef2.Width)) / 2;
                if (this.showHeader)
                {
                    num7 = ((int) ef.Height) + y;
                    g.DrawString(this.headerText, this.headerFont, Brushes.Black, (float) num8, (float) y);
                }
                else
                {
                    num7 = y;
                }
                int height = 0;
                if (base.ShowCode)
                {
                    height = ((int) this.BarCodeHeight) - ((int) ef2.Height);
                }
                else
                {
                    height = (int) this.BarCodeHeight;
                }
                for (num2 = 0; num2 < num3; num2++)
                {
                    if (g.PageUnit == GraphicsUnit.Display)
                    {
                        if (str3[num2] == '1')
                        {
                            width = (int) (FunctionsGraphics.ConvertPixelToDisplay((int) num5) * FunctionsGraphics.ConvertPixelToDisplay((int) this.weight));
                        }
                        else
                        {
                            width = (int) FunctionsGraphics.ConvertPixelToDisplay((int) this.weight);
                        }
                    }
                    else if (str3[num2] == '1')
                    {
                        width = (int) (num5 * ((double) this.weight));
                    }
                    else
                    {
                        width = (int) this.weight;
                    }
                    g.FillRectangle(((num2 % 2) == 0) ? Brushes.Black : Brushes.White, x, num7, width, height);
                    x += width;
                }
                num7 += height;
                if (base.ShowCode)
                {
                    g.DrawString(base.code, this.footerFont, Brushes.Black, (float) num9, (float) num7);
                    num7 += (int) ef2.Height;
                }
            }
        }

        public override void DrawCode(Graphics g, float x, float y)
        {
            this.DrawCode(g, (int) x, (int) y);
        }

        private void Initilize()
        {
            base.Font = new Font("Arial", 10f);
            this.Code = "1234567890";
            this.BarCodeWidth = 100f;
            this.BarCodeHeight = 70f;
            base.showcode = true;
        }

        private void Invalidate()
        {
        }

        public string BarCode
        {
            get
            {
                return base.code;
            }
            set
            {
                base.code = value.ToUpper();
            }
        }

        public Font FooterFont
        {
            get
            {
                return this.footerFont;
            }
            set
            {
                this.footerFont = value;
            }
        }

        public Font HeaderFont
        {
            get
            {
                return this.headerFont;
            }
            set
            {
                this.headerFont = value;
            }
        }

        public string HeaderText
        {
            get
            {
                return this.headerText;
            }
            set
            {
                this.headerText = value;
            }
        }

        public bool ShowHeader
        {
            get
            {
                return this.showHeader;
            }
            set
            {
                this.showHeader = value;
            }
        }

        public AlignType VertAlign
        {
            get
            {
                return this.align;
            }
            set
            {
                this.align = value;
            }
        }

        public BarCodeWeight Weight
        {
            get
            {
                return this.weight;
            }
            set
            {
                this.weight = value;
            }
        }

        public enum AlignType
        {
            Left,
            Center,
            Right
        }

        public enum BarCodeWeight
        {
            Large = 3,
            Medium = 2,
            Small = 1
        }
    }
}

