namespace KReport.Controls
{
    using KReport.Engine;
    using System;
    using System.Drawing;
    using System.Windows.Forms;

    public class ControlBox
    {
        private Cursor[] arrArrow = new Cursor[] { Cursors.SizeNWSE, Cursors.SizeNS, Cursors.SizeNESW, Cursors.SizeWE, Cursors.SizeNWSE, Cursors.SizeNS, Cursors.SizeNESW, Cursors.SizeWE };
        private Color BOX_COLOR = Color.Black;
        private const int BOX_SIZE = 5;
        private bool dragging;
        private Label[] lbl = new Label[8];
        private Control m_control;
        private const int MIN_SIZE = 5;
        private Cursor oldCursor;
        private int starth;
        private int startl;
        private int startt;
        private int startw;
        private int startx;
        private int starty;

        public event MouseEventHandler SizeChanging;
        public event MouseEventHandler SizeChanged;

        public ControlBox()
        {
            for (int i = 0; i < 8; i++)
            {
                this.lbl[i] = new Label();
                this.lbl[i].TabIndex = i;
                this.lbl[i].FlatStyle = FlatStyle.Flat;
                this.lbl[i].BorderStyle = BorderStyle.FixedSingle;
                this.lbl[i].BackColor = this.BOX_COLOR;
                this.lbl[i].Cursor = this.arrArrow[i];
                this.lbl[i].Text = "";
                this.lbl[i].BringToFront();
                this.lbl[i].AutoSize = false;
                this.lbl[i].Size = new Size(5, 5);
                this.lbl[i].MouseDown += new MouseEventHandler(this.lbl_MouseDown);
                this.lbl[i].MouseMove += new MouseEventHandler(this.lbl_MouseMove);
                this.lbl[i].MouseUp += new MouseEventHandler(this.lbl_MouseUp);
                this.lbl[i].Visible = false;
            }
        }
        private void ctl_LocationChanged(object sender, System.EventArgs e)
        {
            this.MoveHandles();
        }

        private void ctl_MouseDown(object sender, MouseEventArgs e)
        {
            this.dragging = true;
            this.startx = e.X;
            this.starty = e.Y;
            this.HideHandles();

            if (SizeChanging != null)
            {
                SizeChanging(sender, e);
            }
        }

        private void ctl_MouseUp(object sender, MouseEventArgs e)
        {
            if (this.dragging)
            {
                this.dragging = false;
                this.MoveHandles();
                this.ShowHandles();

                if (SizeChanged != null)
                {
                    SizeChanged(sender, e);
                }
            }

           
        }

        public void HideHandles()
        {
            for (int i = 0; i < 8; i++)
            {
                this.lbl[i].Visible = false;
            }

            //for (int i = 0; i < 8; i++)
            //{
            //    this.m_control.Parent.Controls.Add(this.lbl[i]);
            //    this.lbl[i].BringToFront();
            //    if (this.m_control.Parent.Controls.Contains(this.lbl[i]))
            //    {
            //        this.m_control.Parent.Controls.Remove(this.lbl[i]);
            //    }

            //}
        }

        private void lbl_MouseDown(object sender, MouseEventArgs e)
        {
            this.m_control.SizeChanged -= new EventHandler(this.ctl_LocationChanged);
            this.m_control.LocationChanged -= new EventHandler(this.ctl_LocationChanged);
            this.dragging = true;
            this.startl = this.m_control.Left;
            this.startt = this.m_control.Top;
            this.startw = this.m_control.Width;
            this.starth = this.m_control.Height;
            this.HideHandles();

            if (SizeChanging != null)
            {
                SizeChanging(sender, e);
            }
            
        }

        private void lbl_MouseMove(object sender, MouseEventArgs e)
        {
            int left = this.m_control.Left;
            int width = this.m_control.Width;
            int top = this.m_control.Top;
            int height = this.m_control.Height;
            if (this.dragging)
            {
                switch (((Label) sender).TabIndex)
                {
                    case 0:
                        left = ((this.startl + e.X) < ((this.startl + this.startw) - 5)) ? (this.startl + e.X) : ((this.startl + this.startw) - 5);
                        top = ((this.startt + e.Y) < ((this.startt + this.starth) - 5)) ? (this.startt + e.Y) : ((this.startt + this.starth) - 5);
                        width = (this.startl + this.startw) - this.m_control.Left;
                        height = (this.startt + this.starth) - this.m_control.Top;
                        break;

                    case 1:
                        top = ((this.startt + e.Y) < ((this.startt + this.starth) - 5)) ? (this.startt + e.Y) : ((this.startt + this.starth) - 5);
                        height = (this.startt + this.starth) - this.m_control.Top;
                        break;

                    case 2:
                        width = ((this.startw + e.X) > 5) ? (this.startw + e.X) : 5;
                        top = ((this.startt + e.Y) < ((this.startt + this.starth) - 5)) ? (this.startt + e.Y) : ((this.startt + this.starth) - 5);
                        height = (this.startt + this.starth) - this.m_control.Top;
                        break;

                    case 3:
                        width = ((this.startw + e.X) > 5) ? (this.startw + e.X) : 5;
                        break;

                    case 4:
                        width = ((this.startw + e.X) > 5) ? (this.startw + e.X) : 5;
                        height = ((this.starth + e.Y) > 5) ? (this.starth + e.Y) : 5;
                        break;

                    case 5:
                        height = ((this.starth + e.Y) > 5) ? (this.starth + e.Y) : 5;
                        break;

                    case 6:
                        left = ((this.startl + e.X) < ((this.startl + this.startw) - 5)) ? (this.startl + e.X) : ((this.startl + this.startw) - 5);
                        width = (this.startl + this.startw) - this.m_control.Left;
                        height = ((this.starth + e.Y) > 5) ? (this.starth + e.Y) : 5;
                        break;

                    case 7:
                        left = ((this.startl + e.X) < ((this.startl + this.startw) - 5)) ? (this.startl + e.X) : ((this.startl + this.startw) - 5);
                        width = (this.startl + this.startw) - this.m_control.Left;
                        break;
                }
                left = (left < 0) ? 0 : left;
                top = (top < 0) ? 0 : top;
                ((CustomControl) this.m_control).resizing = true;
                this.m_control.SetBounds(left, top, width, height);
            }
        }

        private void lbl_MouseUp(object sender, MouseEventArgs e)
        {
            this.m_control.SizeChanged += new EventHandler(this.ctl_LocationChanged);
            this.m_control.LocationChanged += new EventHandler(this.ctl_LocationChanged);
            this.dragging = false;
            this.MoveHandles();
            this.ShowHandles();
            ((CustomControl) this.m_control).resizing = false;
            ((CustomControl) this.m_control).Invalidate();

            if (SizeChanged != null)
            {
                SizeChanged(sender, e);
            }
        }

        private void MoveHandles()
        {
            int num = this.m_control.Left - 5;
            int num2 = this.m_control.Top - 5;
            int num3 = this.m_control.Width + 5;
            int num4 = this.m_control.Height + 5;
            int num5 = 2;
            int[] numArray = new int[] { num + num5, num + (num3 / 2), (num + num3) - num5, (num + num3) - num5, (num + num3) - num5, num + (num3 / 2), num + num5, num + num5 };
            int[] numArray2 = new int[] { num2 + num5, num2 + num5, num2 + num5, num2 + (num4 / 2), (num2 + num4) - num5, (num2 + num4) - num5, (num2 + num4) - num5, num2 + (num4 / 2) };
            for (int i = 0; i < 8; i++)
            {
                this.lbl[i].SetBounds(numArray[i], numArray2[i], 5, 5);
            }
        }

        public void Remove()
        {
            this.HideHandles();
            this.m_control.Cursor = this.oldCursor;
        }

        public void SelectControl(object sender, EventArgs e)
        {
            if (sender != null)
            {
                if (this.m_control != null)
                {
                    this.m_control.Cursor = this.oldCursor;
                    this.m_control.MouseDown -= new MouseEventHandler(this.ctl_MouseDown);
                    this.m_control.MouseUp -= new MouseEventHandler(this.ctl_MouseUp);
                    this.m_control.LocationChanged -= new EventHandler(this.ctl_LocationChanged);
                    this.m_control.SizeChanged  -= new EventHandler(this.ctl_LocationChanged);
                    this.m_control = null;
                }
                this.m_control = (Control) sender;
                this.m_control.MouseDown += new MouseEventHandler(this.ctl_MouseDown);
                this.m_control.MouseUp += new MouseEventHandler(this.ctl_MouseUp);
                this.m_control.LocationChanged += new EventHandler(this.ctl_LocationChanged);
                this.m_control.SizeChanged += new EventHandler(this.ctl_LocationChanged);
                for (int i = 0; i < 8; i++)
                {
                    this.m_control.Parent.Controls.Add(this.lbl[i]);
                    this.lbl[i].BringToFront();
                }
                this.MoveHandles();
                this.ShowHandles();
                this.oldCursor = this.m_control.Cursor;
                this.m_control.Cursor = Cursors.SizeAll;
            }
        }

        private   void ShowHandles()
        {
            if (this.m_control != null)
            {
                for (int i = 0; i < 8; i++)
                {
                    this.lbl[i].Visible = true;
                }
            }
        }
        public void ShowHandlesDeny( Control now_control)
        {
            this.m_control = now_control;
            this.MoveHandles();
            
            if (this.m_control != null)
            {

                //for (int i = 0; i < 8; i++)
                //{
                //    this.m_control.Parent.Controls.Add(this.lbl[i]);
                //    this.lbl[i].BringToFront();
                //}
                for (int i = 0; i < 8; i++)
                {
                    this.lbl[i].BackColor = Color.Black;
                    this.lbl[i].Visible = true;
                    this.lbl[i].BringToFront();
                }
            }
        }

        public void WireControl(Control ctl)
        {
            ctl.Click += new EventHandler(this.SelectControl);
        }
    }
}

