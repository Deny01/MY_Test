namespace KReport.Controls
{
    using System;
    using System.Drawing;
    using System.Windows.Forms;

    internal class Ruler : ControlBase
    {
        private double DPIX;
        private double DPIY;
        private Units escala;
        private double incremento;
        private double labelunidade;
        private RulerOrientation orientation;
        private int posend;
        private int posstart;
        private int tick;
        private double tickfator;
        private int tickLen;
        private int tickLenFull;
        private int tickporunidade;

        private Ruler()
        {
            this.posstart = 0;
            this.posend = 0;
            this.Init();
        }

        public Ruler(RulerOrientation orientation)
        {
            this.posstart = 0;
            this.posend = 0;
            this.Init();
            this.orientation = orientation;
        }

        private void DrawLabel(PaintEventArgs e)
        {
            if (this.orientation == RulerOrientation.Vertical)
            {
                e.Graphics.DrawString(Convert.ToString(Math.Round((double) (this.tick * this.tickfator))), this.Font, Brushes.Black, (float) base.Left, (float) this.posstart);
            }
            else
            {
                e.Graphics.DrawString(Convert.ToString(Math.Round((double) (this.tick * this.tickfator))), this.Font, Brushes.Black, (float) this.posstart, (float) (base.Height / 3));
            }
        }

        private void DrawTick(PaintEventArgs e)
        {
            if (this.orientation == RulerOrientation.Vertical)
            {
                e.Graphics.DrawLine(new Pen(Brushes.Black, 0.1f), 0, this.posstart, this.tickLen, this.posstart);
            }
            else
            {
                e.Graphics.DrawLine(new Pen(Brushes.Black, 0.1f), this.posstart, 0, this.posstart, this.tickLen);
            }
        }

        private void Init()
        {
            this.orientation = RulerOrientation.Horizontal;
            this.escala = Units.Centimetro;
            this.tickporunidade = 0;
            this.tick = 0;
            this.tickfator = 0.0;
            this.Font = new Font("Small Fonts", 7f);
            this.InitializeDevive();
            this.InternalSetUnits();
        }

        private void InitializeDevive()
        {
            Graphics graphics = base.CreateGraphics();
            graphics.PageUnit = GraphicsUnit.Pixel;
            this.DPIX = graphics.DpiX;
            this.DPIY = graphics.DpiY;
            graphics.Dispose();
        }

        private void InternalSetUnits()
        {
            switch (this.Escala)
            {
                case Units.Inch:
                    this.labelunidade = 1.0;
                    this.tickporunidade = 8;
                    this.incremento = this.DPIX / ((double) this.tickporunidade);
                    this.tickfator = this.labelunidade / ((double) this.tickporunidade);
                    break;

                case Units.Milimetro:
                    this.labelunidade = 10.0;
                    this.tickporunidade = 4;
                    this.incremento = (this.DPIX / 2.54) / ((double) this.tickporunidade);
                    this.tickfator = this.labelunidade / ((double) this.tickporunidade);
                    break;

                case Units.Centimetro:
                    this.labelunidade = 1.0;
                    this.tickporunidade = 4;
                    this.incremento = (this.DPIX / 2.54) / ((double) this.tickporunidade);
                    this.tickfator = this.labelunidade / ((double) this.tickporunidade);
                    break;

                case Units.Pixel:
                    this.labelunidade = this.DPIX;
                    this.tickporunidade = Convert.ToInt16(Math.Round((double) (this.DPIX / 10.0)));
                    this.incremento = this.DPIX / ((double) this.tickporunidade);
                    this.tickfator = this.labelunidade / ((double) this.tickporunidade);
                    break;
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            this.BackColor = SystemColors.Window;
            e.Graphics.PageUnit = GraphicsUnit.Pixel;
            if (this.orientation == RulerOrientation.Vertical)
            {
                this.posstart = 0;
                this.posend = base.Height;
                this.tickLenFull = base.Right;
            }
            else
            {
                this.posstart = 0;
                this.posend = base.Width;
                this.tickLenFull = base.Height;
            }
            this.tick = 0;
            double posstart = this.posstart;
            while (this.posstart <= this.posend)
            {
                if ((this.tick % this.tickporunidade) == 0)
                {
                    this.tickLen = this.tickLenFull;
                }
                else if ((this.tick % (this.tickporunidade / 2)) == 0)
                {
                    this.tickLen = Convert.ToInt16(Math.Truncate((double) (((double) this.tickLenFull) / 2.0)));
                }
                else
                {
                    this.tickLen = Convert.ToInt16(Math.Truncate((double) (((double) this.tickLenFull) / 4.0)));
                }
                if (this.tick > 0)
                {
                    this.DrawTick(e);
                }
                if ((this.tick % this.tickporunidade) == 0)
                {
                    this.DrawLabel(e);
                }
                posstart += this.incremento;
                this.posstart = Convert.ToInt16(Math.Truncate(posstart));
                this.tick++;
            }
        }

        public void SetUnits()
        {
            this.InternalSetUnits();
            base.Invalidate();
        }

        public Units Escala
        {
            get
            {
                return this.escala;
            }
            set
            {
                this.escala = value;
                this.SetUnits();
            }
        }

        public RulerOrientation Orientation
        {
            get
            {
                return this.orientation;
            }
        }
    }
}

