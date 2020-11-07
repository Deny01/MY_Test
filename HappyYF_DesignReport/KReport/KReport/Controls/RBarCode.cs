namespace KReport.Controls
{
    using KReport.Engine;
    using MakeSoft.Tools.BarCodes;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    internal class RBarCode : CustomControl
    {
        protected BarCode barcode = null;
        protected MakeSoft.Tools.BarCodes.BarCodeType barcodetype = MakeSoft.Tools.BarCodes.BarCodeType.Ean13;
        protected string code;

        public RBarCode()
        {
            base.Height = 50;
            base.Width = 100;
            base.controlType = ControlType.ControlBarCode;
            this.CreateBarCode();
            this.Code = this.barcode.Code;
        }

        protected void CreateBarCode()
        {
            this.barcode = BarCode.Instance(this.BarCodeType);
        }

        protected void DrawBarCode(Graphics g, int x, int y)
        {
            this.barcode.Code = this.Code;
            this.barcode.DrawCode(g, x, y);
        }

        public override void DrawCommand(Graphics g, int offset)
        {
            base.ApplyEscala(g);
            int y = ((int) Utils.ConvertPixelToDisplay(base.垂直位置)) + offset;
            int esquerda = base.esquerda;
            this.DrawBarCode(g, esquerda, y);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.ApplyEscala(e.Graphics);
            this.DrawBarCode(e.Graphics, 0, 0);
        }

        [Category("Custom")]
        public MakeSoft.Tools.BarCodes.BarCodeType BarCodeType
        {
            get
            {
                return this.barcodetype;
            }
            set
            {
                this.barcodetype = value;
                this.CreateBarCode();
            }
        }

        [Category("Custom")]
        public string Code
        {
            get
            {
                return this.code;
            }
            set
            {
                this.code = value;
                base.Invalidate();
            }
        }

        [Category("Custom")]
        public bool ShowCode
        {
            get
            {
                return this.barcode.ShowCode;
            }
            set
            {
                this.barcode.ShowCode = value;
                base.Invalidate();
            }
        }
    }
}

