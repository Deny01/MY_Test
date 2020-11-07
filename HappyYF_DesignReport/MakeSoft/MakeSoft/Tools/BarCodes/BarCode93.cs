namespace MakeSoft.Tools.BarCodes
{
    using MakeSoft.Tools;
    using System;
    using System.Drawing;

    public class BarCode93 : BarCode
    {
        public BarCode93()
        {
            base.code = "1234567890";
            base.Font = new Font("Arial", 10f);
            this.BarCodeWidth = 300f;
            this.BarCodeHeight = 60f;
            base.ShowCode = true;
        }

        private void ASCIItoCode93Sequence(long nASCIINumber, ref int nFirstNumber, ref int nSecondNumber)
        {
            long num = nASCIINumber;
            if ((num <= 0x7fL) && (num >= 0L))
            {
                switch (((int) num))
                {
                    case 0:
                        nFirstNumber = 0x2c;
                        nSecondNumber = 30;
                        break;

                    case 1:
                        nFirstNumber = 0x2b;
                        nSecondNumber = 10;
                        break;

                    case 2:
                        nFirstNumber = 0x2b;
                        nSecondNumber = 11;
                        break;

                    case 3:
                        nFirstNumber = 0x2b;
                        nSecondNumber = 12;
                        break;

                    case 4:
                        nFirstNumber = 0x2b;
                        nSecondNumber = 13;
                        break;

                    case 5:
                        nFirstNumber = 0x2b;
                        nSecondNumber = 14;
                        break;

                    case 6:
                        nFirstNumber = 0x2b;
                        nSecondNumber = 15;
                        break;

                    case 7:
                        nFirstNumber = 0x2b;
                        nSecondNumber = 0x10;
                        break;

                    case 8:
                        nFirstNumber = 0x2b;
                        nSecondNumber = 0x11;
                        break;

                    case 9:
                        nFirstNumber = 0x2b;
                        nSecondNumber = 0x12;
                        break;

                    case 10:
                        nFirstNumber = 0x2b;
                        nSecondNumber = 0x13;
                        break;

                    case 11:
                        nFirstNumber = 0x2b;
                        nSecondNumber = 20;
                        break;

                    case 12:
                        nFirstNumber = 0x2b;
                        nSecondNumber = 0x15;
                        break;

                    case 13:
                        nFirstNumber = 0x2b;
                        nSecondNumber = 0x16;
                        break;

                    case 14:
                        nFirstNumber = 0x2b;
                        nSecondNumber = 0x17;
                        break;

                    case 15:
                        nFirstNumber = 0x2b;
                        nSecondNumber = 0x18;
                        break;

                    case 0x10:
                        nFirstNumber = 0x2b;
                        nSecondNumber = 0x19;
                        break;

                    case 0x11:
                        nFirstNumber = 0x2b;
                        nSecondNumber = 0x1a;
                        break;

                    case 0x12:
                        nFirstNumber = 0x2b;
                        nSecondNumber = 0x1b;
                        break;

                    case 0x13:
                        nFirstNumber = 0x2b;
                        nSecondNumber = 0x1c;
                        break;

                    case 20:
                        nFirstNumber = 0x2b;
                        nSecondNumber = 0x1d;
                        break;

                    case 0x15:
                        nFirstNumber = 0x2b;
                        nSecondNumber = 30;
                        break;

                    case 0x16:
                        nFirstNumber = 0x2b;
                        nSecondNumber = 0x1f;
                        break;

                    case 0x17:
                        nFirstNumber = 0x2b;
                        nSecondNumber = 0x20;
                        break;

                    case 0x18:
                        nFirstNumber = 0x2b;
                        nSecondNumber = 0x21;
                        break;

                    case 0x19:
                        nFirstNumber = 0x2b;
                        nSecondNumber = 0x22;
                        break;

                    case 0x1a:
                        nFirstNumber = 0x2b;
                        nSecondNumber = 0x23;
                        break;

                    case 0x1b:
                        nFirstNumber = 0x2c;
                        nSecondNumber = 10;
                        break;

                    case 0x1c:
                        nFirstNumber = 0x2c;
                        nSecondNumber = 11;
                        break;

                    case 0x1d:
                        nFirstNumber = 0x2c;
                        nSecondNumber = 12;
                        break;

                    case 30:
                        nFirstNumber = 0x2c;
                        nSecondNumber = 13;
                        break;

                    case 0x1f:
                        nFirstNumber = 0x2c;
                        nSecondNumber = 14;
                        break;

                    case 0x20:
                        nFirstNumber = 0x26;
                        nSecondNumber = -1;
                        break;

                    case 0x21:
                        nFirstNumber = 0x2d;
                        nSecondNumber = 10;
                        break;

                    case 0x22:
                        nFirstNumber = 0x2d;
                        nSecondNumber = 11;
                        break;

                    case 0x23:
                        nFirstNumber = 0x2d;
                        nSecondNumber = 12;
                        break;

                    case 0x24:
                        nFirstNumber = 0x27;
                        nSecondNumber = -1;
                        break;

                    case 0x25:
                        nFirstNumber = 0x2a;
                        nSecondNumber = -1;
                        break;

                    case 0x26:
                        nFirstNumber = 0x2d;
                        nSecondNumber = 15;
                        break;

                    case 0x27:
                        nFirstNumber = 0x2d;
                        nSecondNumber = 0x10;
                        break;

                    case 40:
                        nFirstNumber = 0x2d;
                        nSecondNumber = 0x11;
                        break;

                    case 0x29:
                        nFirstNumber = 0x2d;
                        nSecondNumber = 0x12;
                        break;

                    case 0x2a:
                        nFirstNumber = 0x2d;
                        nSecondNumber = 0x13;
                        break;

                    case 0x2b:
                        nFirstNumber = 0x29;
                        nSecondNumber = -1;
                        break;

                    case 0x2c:
                        nFirstNumber = 0x2d;
                        nSecondNumber = 0x15;
                        break;

                    case 0x2d:
                        nFirstNumber = 0x24;
                        nSecondNumber = -1;
                        break;

                    case 0x2e:
                        nFirstNumber = 0x25;
                        nSecondNumber = -1;
                        break;

                    case 0x2f:
                        nFirstNumber = 40;
                        nSecondNumber = -1;
                        break;

                    case 0x30:
                        nFirstNumber = 0;
                        nSecondNumber = -1;
                        break;

                    case 0x31:
                        nFirstNumber = 1;
                        nSecondNumber = -1;
                        break;

                    case 50:
                        nFirstNumber = 2;
                        nSecondNumber = -1;
                        break;

                    case 0x33:
                        nFirstNumber = 3;
                        nSecondNumber = -1;
                        break;

                    case 0x34:
                        nFirstNumber = 4;
                        nSecondNumber = -1;
                        break;

                    case 0x35:
                        nFirstNumber = 5;
                        nSecondNumber = -1;
                        break;

                    case 0x36:
                        nFirstNumber = 6;
                        nSecondNumber = -1;
                        break;

                    case 0x37:
                        nFirstNumber = 7;
                        nSecondNumber = -1;
                        break;

                    case 0x38:
                        nFirstNumber = 8;
                        nSecondNumber = -1;
                        break;

                    case 0x39:
                        nFirstNumber = 9;
                        nSecondNumber = -1;
                        break;

                    case 0x3a:
                        nFirstNumber = 0x2d;
                        nSecondNumber = 0x23;
                        break;

                    case 0x3b:
                        nFirstNumber = 0x2c;
                        nSecondNumber = 15;
                        break;

                    case 60:
                        nFirstNumber = 0x2c;
                        nSecondNumber = 0x10;
                        break;

                    case 0x3d:
                        nFirstNumber = 0x2c;
                        nSecondNumber = 0x11;
                        break;

                    case 0x3e:
                        nFirstNumber = 0x2c;
                        nSecondNumber = 0x12;
                        break;

                    case 0x3f:
                        nFirstNumber = 0x2c;
                        nSecondNumber = 0x13;
                        break;

                    case 0x40:
                        nFirstNumber = 0x2c;
                        nSecondNumber = 0x1f;
                        break;

                    case 0x41:
                        nFirstNumber = 10;
                        nSecondNumber = -1;
                        break;

                    case 0x42:
                        nFirstNumber = 11;
                        nSecondNumber = -1;
                        break;

                    case 0x43:
                        nFirstNumber = 12;
                        nSecondNumber = -1;
                        break;

                    case 0x44:
                        nFirstNumber = 13;
                        nSecondNumber = -1;
                        break;

                    case 0x45:
                        nFirstNumber = 14;
                        nSecondNumber = -1;
                        break;

                    case 70:
                        nFirstNumber = 15;
                        nSecondNumber = -1;
                        break;

                    case 0x47:
                        nFirstNumber = 0x10;
                        nSecondNumber = -1;
                        break;

                    case 0x48:
                        nFirstNumber = 0x11;
                        nSecondNumber = -1;
                        break;

                    case 0x49:
                        nFirstNumber = 0x12;
                        nSecondNumber = -1;
                        break;

                    case 0x4a:
                        nFirstNumber = 0x13;
                        nSecondNumber = -1;
                        break;

                    case 0x4b:
                        nFirstNumber = 20;
                        nSecondNumber = -1;
                        break;

                    case 0x4c:
                        nFirstNumber = 0x15;
                        nSecondNumber = -1;
                        break;

                    case 0x4d:
                        nFirstNumber = 0x16;
                        nSecondNumber = -1;
                        break;

                    case 0x4e:
                        nFirstNumber = 0x17;
                        nSecondNumber = -1;
                        break;

                    case 0x4f:
                        nFirstNumber = 0x18;
                        nSecondNumber = -1;
                        break;

                    case 80:
                        nFirstNumber = 0x19;
                        nSecondNumber = -1;
                        break;

                    case 0x51:
                        nFirstNumber = 0x1a;
                        nSecondNumber = -1;
                        break;

                    case 0x52:
                        nFirstNumber = 0x1b;
                        nSecondNumber = -1;
                        break;

                    case 0x53:
                        nFirstNumber = 0x1c;
                        nSecondNumber = -1;
                        break;

                    case 0x54:
                        nFirstNumber = 0x1d;
                        nSecondNumber = -1;
                        break;

                    case 0x55:
                        nFirstNumber = 30;
                        nSecondNumber = -1;
                        break;

                    case 0x56:
                        nFirstNumber = 0x1f;
                        nSecondNumber = -1;
                        break;

                    case 0x57:
                        nFirstNumber = 0x20;
                        nSecondNumber = -1;
                        break;

                    case 0x58:
                        nFirstNumber = 0x21;
                        nSecondNumber = -1;
                        break;

                    case 0x59:
                        nFirstNumber = 0x22;
                        nSecondNumber = -1;
                        break;

                    case 90:
                        nFirstNumber = 0x23;
                        nSecondNumber = -1;
                        break;

                    case 0x5b:
                        nFirstNumber = 0x2c;
                        nSecondNumber = 20;
                        break;

                    case 0x5c:
                        nFirstNumber = 0x2c;
                        nSecondNumber = 0x15;
                        break;

                    case 0x5d:
                        nFirstNumber = 0x2c;
                        nSecondNumber = 0x16;
                        break;

                    case 0x5e:
                        nFirstNumber = 0x2c;
                        nSecondNumber = 0x17;
                        break;

                    case 0x5f:
                        nFirstNumber = 0x2c;
                        nSecondNumber = 0x18;
                        break;

                    case 0x60:
                        nFirstNumber = 0x2c;
                        nSecondNumber = 0x20;
                        break;

                    case 0x61:
                        nFirstNumber = 0x2e;
                        nSecondNumber = 10;
                        break;

                    case 0x62:
                        nFirstNumber = 0x2e;
                        nSecondNumber = 11;
                        break;

                    case 0x63:
                        nFirstNumber = 0x2e;
                        nSecondNumber = 12;
                        break;

                    case 100:
                        nFirstNumber = 0x2e;
                        nSecondNumber = 13;
                        break;

                    case 0x65:
                        nFirstNumber = 0x2e;
                        nSecondNumber = 14;
                        break;

                    case 0x66:
                        nFirstNumber = 0x2e;
                        nSecondNumber = 15;
                        break;

                    case 0x67:
                        nFirstNumber = 0x2e;
                        nSecondNumber = 0x10;
                        break;

                    case 0x68:
                        nFirstNumber = 0x2e;
                        nSecondNumber = 0x11;
                        break;

                    case 0x69:
                        nFirstNumber = 0x2e;
                        nSecondNumber = 0x12;
                        break;

                    case 0x6a:
                        nFirstNumber = 0x2e;
                        nSecondNumber = 0x13;
                        break;

                    case 0x6b:
                        nFirstNumber = 0x2e;
                        nSecondNumber = 20;
                        break;

                    case 0x6c:
                        nFirstNumber = 0x2e;
                        nSecondNumber = 0x15;
                        break;

                    case 0x6d:
                        nFirstNumber = 0x2e;
                        nSecondNumber = 0x16;
                        break;

                    case 110:
                        nFirstNumber = 0x2e;
                        nSecondNumber = 0x17;
                        break;

                    case 0x6f:
                        nFirstNumber = 0x2e;
                        nSecondNumber = 0x18;
                        break;

                    case 0x70:
                        nFirstNumber = 0x2e;
                        nSecondNumber = 0x19;
                        break;

                    case 0x71:
                        nFirstNumber = 0x2e;
                        nSecondNumber = 0x1a;
                        break;

                    case 0x72:
                        nFirstNumber = 0x2e;
                        nSecondNumber = 0x1b;
                        break;

                    case 0x73:
                        nFirstNumber = 0x2e;
                        nSecondNumber = 0x1c;
                        break;

                    case 0x74:
                        nFirstNumber = 0x2e;
                        nSecondNumber = 0x1d;
                        break;

                    case 0x75:
                        nFirstNumber = 0x2e;
                        nSecondNumber = 30;
                        break;

                    case 0x76:
                        nFirstNumber = 0x2e;
                        nSecondNumber = 0x1f;
                        break;

                    case 0x77:
                        nFirstNumber = 0x2e;
                        nSecondNumber = 0x20;
                        break;

                    case 120:
                        nFirstNumber = 0x2e;
                        nSecondNumber = 0x21;
                        break;

                    case 0x79:
                        nFirstNumber = 0x2e;
                        nSecondNumber = 0x22;
                        break;

                    case 0x7a:
                        nFirstNumber = 0x2e;
                        nSecondNumber = 0x23;
                        break;

                    case 0x7b:
                        nFirstNumber = 0x2c;
                        nSecondNumber = 0x19;
                        break;

                    case 0x7c:
                        nFirstNumber = 0x2c;
                        nSecondNumber = 0x1a;
                        break;

                    case 0x7d:
                        nFirstNumber = 0x2c;
                        nSecondNumber = 0x1b;
                        break;

                    case 0x7e:
                        nFirstNumber = 0x2c;
                        nSecondNumber = 0x1c;
                        break;

                    case 0x7f:
                        nFirstNumber = 0x2c;
                        nSecondNumber = 0x1d;
                        break;
                }
            }
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
                SizeF ef = g.MeasureString(this.Code, base.Font);
                base.internalbarcodeheight = 0f;
                if (base.ShowCode)
                {
                    base.internalbarcodeheight = ((int) this.BarCodeHeight) - ((int) ef.Height);
                }
                else
                {
                    base.internalbarcodeheight = (int) this.BarCodeHeight;
                }
                int nFirstNumber = 0;
                int nSecondNumber = 0;
                base.cursor = x;
                this.DrawPattern(g, base.cursor, y, this.RetrievePattern(0x2fL));
                for (int i = 0; i < this.Code.Length; i++)
                {
                    char ch = this.Code[i];
                    this.ASCIItoCode93Sequence(Convert.ToInt64(ch.ToString()), ref nFirstNumber, ref nSecondNumber);
                    this.DrawPattern(g, base.cursor, y, this.RetrievePattern((long) nFirstNumber));
                    if (nSecondNumber != -1)
                    {
                        this.DrawPattern(g, base.cursor, y, this.RetrievePattern((long) nSecondNumber));
                    }
                }
                this.DrawPattern(g, base.cursor, y, this.RetrievePattern(0x30L));
                base.barcodewidth = base.cursor - x;
                if (base.ShowCode)
                {
                    x += (((int) base.barcodewidth) - ((int) ef.Width)) / 2;
                    g.DrawString(this.Code, base.Font, Brushes.Black, (float) x, y + base.internalbarcodeheight);
                }
            }
        }

        public override void DrawCode(Graphics g, float x, float y)
        {
            this.DrawCode(g, (int) x, (int) y);
        }

        private void DrawPattern(Graphics g, int x, int y, string csCharPattern)
        {
            int pixel = x;
            int num3 = y;
            for (int i = 0; i < csCharPattern.Length; i++)
            {
                int num4 = (int) FunctionsGraphics.ConvertPixelToDisplay(1);
                for (pixel = x; pixel < (x + num4); pixel++)
                {
                    if (csCharPattern[i] == 'b')
                    {
                        g.FillRectangle(Brushes.Black, (float) ((int) FunctionsGraphics.ConvertPixelToDisplay(pixel)), (float) num3, (float) num4, base.internalbarcodeheight);
                    }
                    else
                    {
                        g.FillRectangle(Brushes.White, (float) ((int) FunctionsGraphics.ConvertPixelToDisplay(pixel)), (float) num3, (float) num4, base.internalbarcodeheight);
                    }
                }
                x += num4;
                base.cursor += num4;
            }
        }

        private string RetrievePattern(long c)
        {
            string str = string.Empty;
            long num = c;
            if ((num <= 0x30L) && (num >= 0L))
            {
                switch (((int) num))
                {
                    case 0:
                        return "bsssbsbss";

                    case 1:
                        return "bsbssbsss";

                    case 2:
                        return "bsbsssbss";

                    case 3:
                        return "bsbssssbs";

                    case 4:
                        return "bssbsbsss";

                    case 5:
                        return "bssbssbss";

                    case 6:
                        return "bssbsssbs";

                    case 7:
                        return "bsbsbssss";

                    case 8:
                        return "bsssbssbs";

                    case 9:
                        return "bssssbsbs";

                    case 10:
                        return "bbsbsbsss";

                    case 11:
                        return "bbsbssbss";

                    case 12:
                        return "bbsbsssbs";

                    case 13:
                        return "bbssbsbss";

                    case 14:
                        return "bbssbssbs";

                    case 15:
                        return "bbsssbsbs";

                    case 0x10:
                        return "bsbbsbsss";

                    case 0x11:
                        return "bsbbssbss";

                    case 0x12:
                        return "bsbbsssbs";

                    case 0x13:
                        return "bssbbsbss";

                    case 20:
                        return "bsssbbsbs";

                    case 0x15:
                        return "bsbsbbsss";

                    case 0x16:
                        return "bsbssbbss";

                    case 0x17:
                        return "bsbsssbbs";

                    case 0x18:
                        return "bssbsbbss";

                    case 0x19:
                        return "bsssbsbbs";

                    case 0x1a:
                        return "bbsbbsbss";

                    case 0x1b:
                        return "bbsbbssbs";

                    case 0x1c:
                        return "bbsbsbbss";

                    case 0x1d:
                        return "bbsbssbbs";

                    case 30:
                        return "bbssbsbbs";

                    case 0x1f:
                        return "bbssbbsbs";

                    case 0x20:
                        return "bsbbsbbss";

                    case 0x21:
                        return "bsbbssbbs";

                    case 0x22:
                        return "bssbbsbbs";

                    case 0x23:
                        return "bssbbbsbs";

                    case 0x24:
                        return "bssbsbbbs";

                    case 0x25:
                        return "bbbsbsbss";

                    case 0x26:
                        return "bbbsbssbs";

                    case 0x27:
                        return "bbbssbsbs";

                    case 40:
                        return "bsbbsbbbs";

                    case 0x29:
                        return "bsbbbsbbs";

                    case 0x2a:
                        return "bbsbsbbbs";

                    case 0x2b:
                        return "bssbssbbs";

                    case 0x2c:
                        return "bbbsbbsbs";

                    case 0x2d:
                        return "bbbsbsbbs";

                    case 0x2e:
                        return "bssbbssbs";

                    case 0x2f:
                        return "bsbsbbbbs";

                    case 0x30:
                        return "bsbsbbbbsb";
                }
            }
            return str;
        }
    }
}

