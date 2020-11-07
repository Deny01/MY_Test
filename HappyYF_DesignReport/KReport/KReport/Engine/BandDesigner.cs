namespace KReport.Engine
{
    using System;
    using System.Drawing;
    using System.Windows.Forms;

    //internal class BandDesigner : Control
    public  class BandDesigner : Control
    {
        public BandDesigner()
        {
            base.Height = 0x10;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            this.BackColor = SystemColors.Control;
            e.Graphics.DrawString("^ " + this.Text, new Font("Arial", 8f), Brushes.Black, 0f, 0f, StringFormat.GenericDefault);
        }
    }
}

