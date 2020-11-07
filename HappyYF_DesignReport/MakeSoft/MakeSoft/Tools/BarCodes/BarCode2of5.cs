namespace MakeSoft.Tools.BarCodes
{
    using MakeSoft.Tools;
    using System;
    using System.Drawing;

    public class BarCode2of5 : BarCode
    {
        public BarCode2of5()
        {
            base.code = "1234567890";
            base.Font = new Font("Arial", 10f);
            this.BarCodeWidth = 300f;
            this.BarCodeHeight = 60f;
            base.ShowCode = true;
        }

        private void AjusteSizeBarCode()
        {
        }

        public override bool CheckBarCode()
        {
            if (!base.CheckBarCode())
            {
                return false;
            }
            return ((this.Code.Length % 2) == 0);
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
                base.cursor = x;
                this.DrawPattern(g, base.cursor, y, "nnnn");
                for (int i = 0; i < this.Code.Length; i += 2)
                {
                    char ch = this.Code[i];
                    int nTwoDigitNumber = Convert.ToInt16(ch.ToString()) * 10;
                    ch = this.Code[i + 1];
                    nTwoDigitNumber += Convert.ToInt16(ch.ToString());
                    this.DrawPattern(g, base.cursor, y, this.RetrievePattern(nTwoDigitNumber));
                }
                this.DrawPattern(g, base.cursor, y, "wnn");
                base.barcodewidth = base.cursor - x;
                if (base.ShowCode)
                {
                    x += (((int) base.barcodewidth) - ((int) ef.Width)) / 2;
                    g.DrawString(this.Code, base.Font, Brushes.Black, (float) x, y + base.internalbarcodeheight);
                }
            }
        }

        private void DrawPattern(Graphics g, int x, int y, string csCharPattern)
        {
            int pixel = x;
            int num3 = y;
            for (int i = 0; i < csCharPattern.Length; i++)
            {
                int num4;
                if (csCharPattern[i] == 'n')
                {
                    num4 = (int) FunctionsGraphics.ConvertPixelToDisplay(1);
                }
                else
                {
                    num4 = (int) FunctionsGraphics.ConvertPixelToDisplay(3);
                }
                for (pixel = x; pixel < (x + num4); pixel++)
                {
                    if ((i % 2) == 0)
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

        private string RetrievePattern(int nTwoDigitNumber)
        {
            string str = string.Empty;
            switch (nTwoDigitNumber)
            {
                case 0:
                    return "nnnnwwwwnn";

                case 1:
                    return "nwnnwnwnnw";

                case 2:
                    return "nnnwwnwnnw";

                case 3:
                    return "nwnwwnwnnn";

                case 4:
                    return "nnnnwwwnnw";

                case 5:
                    return "nwnnwwwnnn";

                case 6:
                    return "nnnwwwwnnn";

                case 7:
                    return "nnnnwnwwnw";

                case 8:
                    return "nwnnwnwwnn";

                case 9:
                    return "nnnwwnwwnn";

                case 10:
                    return "wnnnnwnwwn";

                case 11:
                    return "wwnnnnnnww";

                case 12:
                    return "wnnwnnnnww";

                case 13:
                    return "wwnwnnnnwn";

                case 14:
                    return "wnnnnwnnww";

                case 15:
                    return "wwnnnwnnwn";

                case 0x10:
                    return "wnnwnwnnwn";

                case 0x11:
                    return "wnnnnnnwww";

                case 0x12:
                    return "wwnnnnnwwn";

                case 0x13:
                    return "wnnwnnnwwn";

                case 20:
                    return "nnwnnwnwwn";

                case 0x15:
                    return "nwwnnnnnww";

                case 0x16:
                    return "nnwwnnnnww";

                case 0x17:
                    return "nwwwnnnnwn";

                case 0x18:
                    return "nnwnnwnnww";

                case 0x19:
                    return "nwwnnwnnwn";

                case 0x1a:
                    return "nnwwnwnnwn";

                case 0x1b:
                    return "nnwnnnnwww";

                case 0x1c:
                    return "nwwnnnnwwn";

                case 0x1d:
                    return "nnwwnnnwwn";

                case 30:
                    return "wnwnnwnwnn";

                case 0x1f:
                    return "wwwnnnnnnw";

                case 0x20:
                    return "wnwwnnnnnw";

                case 0x21:
                    return "wwwwnnnnnn";

                case 0x22:
                    return "wnwnnwnnnw";

                case 0x23:
                    return "wwwnnwnnnn";

                case 0x24:
                    return "wnwwnwnnnn";

                case 0x25:
                    return "wnwnnnnwnw";

                case 0x26:
                    return "wwwnnnnwnn";

                case 0x27:
                    return "wnwwnnnwnn";

                case 40:
                    return "nnnnwwnwwn";

                case 0x29:
                    return "nwnnwnnnww";

                case 0x2a:
                    return "nnnwwnnnww";

                case 0x2b:
                    return "nwnwwnnnwn";

                case 0x2c:
                    return "nnnnwwnnww";

                case 0x2d:
                    return "nwnnwwnnwn";

                case 0x2e:
                    return "nnnwwwnnwn";

                case 0x2f:
                    return "nnnnwnnwww";

                case 0x30:
                    return "nwnnwnnwwn";

                case 0x31:
                    return "nnnwwnnwwn";

                case 50:
                    return "wnnnwwnwnn";

                case 0x33:
                    return "wwnnwnnnnw";

                case 0x34:
                    return "wnnwwnnnnw";

                case 0x35:
                    return "wwnwwnnnnn";

                case 0x36:
                    return "wnnnwwnnnw";

                case 0x37:
                    return "wwnnwwnnnn";

                case 0x38:
                    return "wnnwwwnnnn";

                case 0x39:
                    return "wnnnwnnwnw";

                case 0x3a:
                    return "wwnnwnnwnn";

                case 0x3b:
                    return "wnnwwnnwnn";

                case 60:
                    return "nnwnwwnwnn";

                case 0x3d:
                    return "nwwnwnnnnw";

                case 0x3e:
                    return "nnwwwnnnnw";

                case 0x3f:
                    return "nwwwwnnnnn";

                case 0x40:
                    return "nnwnwwnnnw";

                case 0x41:
                    return "nwwnwwnnnn";

                case 0x42:
                    return "nnwwwwnnnn";

                case 0x43:
                    return "nnwnwnnwnw";

                case 0x44:
                    return "nwwnwnnwnn";

                case 0x45:
                    return "nnwwwnnwnn";

                case 70:
                    return "nnnnnwwwwn";

                case 0x47:
                    return "nwnnnnwnww";

                case 0x48:
                    return "nnnwnnwnww";

                case 0x49:
                    return "nwnwnnwnwn";

                case 0x4a:
                    return "nnnnnwwnww";

                case 0x4b:
                    return "nwnnnwwnwn";

                case 0x4c:
                    return "nnnwnwwnwn";

                case 0x4d:
                    return "nnnnnnwwww";

                case 0x4e:
                    return "nwnnnnwwwn";

                case 0x4f:
                    return "nnnwnnwwwn";

                case 80:
                    return "wnnnnwwwnn";

                case 0x51:
                    return "wwnnnnwnnw";

                case 0x52:
                    return "wnnwnnwnnw";

                case 0x53:
                    return "wwnwnnwnnn";

                case 0x54:
                    return "wnnnnwwnnw";

                case 0x55:
                    return "wwnnnwwnnn";

                case 0x56:
                    return "wnnwnwwnnn";

                case 0x57:
                    return "wnnnnnwwnw";

                case 0x58:
                    return "wwnnnnwwnn";

                case 0x59:
                    return "wnnwnnwwnn";

                case 90:
                    return "nnwnnwwwnn";

                case 0x5b:
                    return "nwwnnnwnnw";

                case 0x5c:
                    return "nnwwnnwnnw";

                case 0x5d:
                    return "nwwwnnwnnn";

                case 0x5e:
                    return "nnwnnwwnnw";

                case 0x5f:
                    return "nwwnnwwnnn";

                case 0x60:
                    return "nnwwnwwnnn";

                case 0x61:
                    return "nnwnnnwwnw";

                case 0x62:
                    return "nwwnnnwwnn";

                case 0x63:
                    return "nnwwnnwwnn";
            }
            return str;
        }
    }
}

