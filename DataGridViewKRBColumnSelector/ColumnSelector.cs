using System;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;
using System.Drawing.Drawing2D;
using System.Collections.Generic;

namespace CustomDataGridViewKRBColumnSelector
{
    internal class ColumnSelector : Control
    {
        #region Event - Delegate

        /// <summary>
        /// Represents an event which occurs when the menu item checked changes.
        /// </summary>
        /// <param name="sender">Event sender class</param>
        /// <param name="ea">Menu item information args</param>
        internal delegate void MenuItemEventHandler(object sender, MenuItemEventArgs ea);
        
        /// <summary>
        /// Occurs when the selector menu is closed by the user.
        /// </summary>
        internal event EventHandler Closed;
        
        /// <summary>
        /// Occurs when the menu item checked state changed.
        /// </summary>
        internal event MenuItemEventHandler MenuItemChanged;

        #endregion

        #region Symbolic Constants

        private const int MAXCOLUMNS = 10;

        #endregion

        #region Instance Members

        private int hotIndex = -2;
        private int barGradientWidth = 0;
        private Bitmap baseBitmap = null;
        private Graphics graphOne = null;
        private Rectangle exitRectangle = Rectangle.Empty;
        private List<MenuItem> items = new List<MenuItem>();
        private DataGridViewKRBColumnSelector selector = null;

        #endregion

        #region Constructor

        public ColumnSelector()
        {
            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.UserPaint |
                ControlStyles.UserMouse | ControlStyles.FixedWidth | ControlStyles.FixedHeight, true);
        }

        public ColumnSelector(DataGridViewKRBColumnSelector selector)
            : this()
        {
            this.selector = selector;
            this.Font = this.selector.Font;
            DataGridView dataGridView = this.selector.GridView;

            if (dataGridView == null)
                throw new ArgumentNullException("DataGridView cannot be null.");
            else
            {
                for (int i = 0; i < dataGridView.Columns.Count && i <= MAXCOLUMNS - 1; i++)
                {
                    DataGridViewColumn currentColumn = dataGridView.Columns[i];
                    items.Add(new MenuItem(currentColumn.HeaderText, currentColumn.Visible));
                    
                }

                CreatesRectangles();
            }
        }

        public ColumnSelector(DataGridViewKRBColumnSelector selector, int maxColumns)
            : this()
        {
            this.selector = selector;
            this.Font = this.selector.Font;
            DataGridView dataGridView = this.selector.GridView;

            if (dataGridView == null)
                throw new ArgumentNullException("DataGridView cannot be null.");
            else
            {
                for (int i = 0; i < dataGridView.Columns.Count && i <= maxColumns - 1; i++)
                {
                    DataGridViewColumn currentColumn = dataGridView.Columns[i];
                    items.Add(new MenuItem(currentColumn.HeaderText, currentColumn.Visible));
                }

                CreatesRectangles();
            }
        }

        #endregion

        #region Helper Methods

        private int HitTest(int x, int y)
        {
            // Nothing.
            int result = -2;

            foreach (MenuItem item in items)
            {
                if (item.ItemRectangle.Contains(x, y))
                {
                    result = items.IndexOf(item);
                    break;
                }
            }

            return result;
        }
        
        /// <summary>
        /// Trigger a menu closed event.
        /// </summary>
        private void OnClosed()
        {
            if (Closed != null)
                Closed(this, EventArgs.Empty);
        }

        private void CreatesRectangles()
        {
            Graphics g = this.CreateGraphics();

            SizeF textMaxSize = new SizeF();
            foreach (MenuItem item in items)
            {
                SizeF currentSize = g.MeasureString(item.ItemText, this.Font, Int32.MaxValue, StringFormat.GenericTypographic);
                textMaxSize = textMaxSize.Width >= currentSize.Width ? textMaxSize : currentSize;
            }

            barGradientWidth = 10 + (int)textMaxSize.Height;

            this.Size = new Size()
            {
                Width = 22 + barGradientWidth + (int)textMaxSize.Width,
                Height = 7 + ((items.Count + 1) * ((int)textMaxSize.Height + 6)) + (items.Count - 1)
            };

            Rectangle clientRectangle = this.ClientRectangle;
            clientRectangle.Offset(2, 2);
            clientRectangle.Width -= 4;
            clientRectangle.Height = (int)textMaxSize.Height + 6;

            for (int i = 0; i < items.Count; i++)
            {
                MenuItem item = items[i];

                if (i == 0)
                {
                    item.ItemRectangle = clientRectangle;
                }
                else
                {
                    // Object Initializer
                    item.ItemRectangle = new Rectangle()
                    {
                        X = clientRectangle.X,
                        Y = items[i - 1].ItemRectangle.Y + clientRectangle.Height + 1,
                        Width = clientRectangle.Width,
                        Height = clientRectangle.Height
                    };

                    if (i == items.Count - 1)
                    {
                        exitRectangle = new Rectangle()
                        {
                            X = clientRectangle.X,
                            Y = item.ItemRectangle.Y + clientRectangle.Height + 2,
                            Width = clientRectangle.Width,
                            Height = clientRectangle.Height
                        };
                    }
                }
            }
        }

        /// <summary>
        /// Trigger a menu item changed event.
        /// </summary>
        /// <param name="sender">Event sender class</param>
        /// <param name="ea">Menu item information args</param>
        private void OnMenuItemChanged(object sender, MenuItemEventArgs ea)
        {
            if (MenuItemChanged != null)
                MenuItemChanged(sender, ea);
        }

        #endregion

        #region Override Methods

        protected override void OnPaint(PaintEventArgs pe)
        {
            if (baseBitmap == null)
            {
                baseBitmap = new Bitmap(this.Width, this.Height);
                graphOne = Graphics.FromImage(baseBitmap);

                DrawBarGradient(graphOne);
                DrawBackground(graphOne);
                DrawBorder(graphOne);
            }

            using (Bitmap overlay = new Bitmap(baseBitmap))
            using (Graphics gr = Graphics.FromImage(overlay))
            {
                DrawMenuItems(gr);
                pe.Graphics.DrawImage(overlay, this.ClientRectangle, 0, 0, overlay.Width, overlay.Height, GraphicsUnit.Pixel);
            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (exitRectangle.Contains(e.Location))
            {
                if (hotIndex != -1)
                {
                    hotIndex = -1;

                    // RePaint column selector.
                    this.Invalidate();
                }
            }
            else
            {
                int index = HitTest(e.X, e.Y);
                if (index != hotIndex)
                {
                    hotIndex = index;

                    // RePaint column selector.
                    this.Invalidate();
                }
            }
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                switch (hotIndex)
                {
                    case -2:
                        break;
                    case -1:
                        OnClosed();  // Trigger a menu closed event.
                        break;
                    default:
                        // Trigger a menu item changed event.
                        OnMenuItemChanged(this, new MenuItemEventArgs(hotIndex));

                        // Update menu item.
                        MenuItem item = items[hotIndex];
                        item.IsChecked = !item.IsChecked;

                        // RePaint column selector.
                        this.Invalidate();
                        break;
                }
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (graphOne != null)
                    graphOne.Dispose();

                if (baseBitmap != null)
                    baseBitmap.Dispose();

                foreach (MenuItem item in items)
                    item.Dispose();
            }

            base.Dispose(disposing);
        }
        
        #endregion

        #region Virtual Methods

        protected virtual void DrawBarGradient(Graphics g)
        {
            Rectangle barGradientRct = this.ClientRectangle;
            barGradientRct.Inflate(-1, -1);
            barGradientRct.Width = barGradientWidth;

            CustomDataGridViewKRBColumnSelector.DataGridViewKRBColumnSelector.GradientBar gradient = this.selector.BarGradient;
            using (LinearGradientBrush brush = new LinearGradientBrush(barGradientRct, gradient.ColorStart, gradient.ColorEnd, gradient.GradientStyle))
            {
                g.FillRectangle(brush, barGradientRct);
            }
        }

        protected virtual void DrawBackground(Graphics g)
        {
            using (SolidBrush brush = new SolidBrush(this.selector.BackColor))
            {
                g.FillRectangle(brush, barGradientWidth + 1, 1, (this.Width - 1) - (barGradientWidth + 1), this.Height - 2);
            }
        }

        protected virtual void DrawBorder(Graphics g)
        {
            using (Pen pen = new Pen(this.selector.BorderColor))
            {
                g.DrawRectangle(pen, 0, 0, this.Width - 1, this.Height - 1);
            }
        }

        protected virtual void DrawMenuItems(Graphics g)
        {
            // The text doesn't wrap but instead disappears under the right edge of the menu item rectangle.
            StringFormat sf = new StringFormat(StringFormatFlags.NoWrap);
            sf.Alignment = this.selector.Alignment;     // Horizontal Alignment
            sf.LineAlignment = StringAlignment.Center;  // Vertical Alignment

            for (int i = 0; i < items.Count; i++)
            {
                MenuItem currentItem = items[i];
                Rectangle rct = currentItem.ItemRectangle;

                // Object Initializer
                Rectangle textRct = new Rectangle()
                {
                    X = barGradientWidth + 5,
                    Y = rct.Y,
                    Width = this.Width - (barGradientWidth + 4),
                    Height = rct.Height
                };

                if (i == hotIndex)
                {
                    using (Pen pen = new Pen(this.selector.MenuItem.ItemBorderColor))
                    {
                        g.DrawRectangle(pen, rct.X, rct.Y, rct.Width - 1, rct.Height - 1);
                    }

                    rct.Inflate(-1, -1);
                    using (LinearGradientBrush brush = new LinearGradientBrush(rct, this.selector.MenuItem.ColorStart,
                        this.selector.MenuItem.ColorEnd, this.selector.MenuItem.GradientStyle))
                    {
                        g.FillRectangle(brush, rct);
                    }

                    // Draw Checkbox border and gradient area if current menu item is checked.
                    if (currentItem.IsChecked)
                    {
                        rct.Inflate(-1, -1);
                        rct.Width = barGradientWidth - 8;
                        using (Pen pen = new Pen(this.selector.HoverAreaCheckBox.BorderColor))
                        {
                            g.DrawRectangle(pen, rct.X, rct.Y, rct.Width - 1, rct.Height - 1);
                        }

                        rct.Inflate(-1, -1);
                        using (LinearGradientBrush brush = new LinearGradientBrush(rct, this.selector.HoverAreaCheckBox.ColorStart,
                            this.selector.HoverAreaCheckBox.ColorEnd, this.selector.HoverAreaCheckBox.GradientStyle))
                        {
                            g.FillRectangle(brush, rct);
                            rct.Inflate(1, 1);
                            rct.X += 1;
                            ControlPaint.DrawMenuGlyph(g, rct, MenuGlyph.Checkmark, this.selector.HoverAreaCheckBox.TickColor, Color.Transparent);
                        }
                    }

                    // Draw MenuItem Text.
                    using (SolidBrush brush = new SolidBrush(this.selector.MenuItem.ItemHoverForeColor))
                    {
                        g.DrawString(currentItem.ItemText, this.Font, brush, textRct, sf);
                    }
                }
                else
                {
                    // Draw Checkbox border and gradient area if current menu item is checked.
                    if (currentItem.IsChecked)
                    {
                        rct.Inflate(-2, -2);
                        rct.Width = barGradientWidth - 8;
                        using (Pen pen = new Pen(this.selector.AreaCheckBox.BorderColor))
                        {
                            g.DrawRectangle(pen, rct.X, rct.Y, rct.Width - 1, rct.Height - 1);
                        }

                        rct.Inflate(-1, -1);
                        using (LinearGradientBrush brush = new LinearGradientBrush(rct, this.selector.AreaCheckBox.ColorStart,
                            this.selector.AreaCheckBox.ColorEnd, this.selector.AreaCheckBox.GradientStyle))
                        {
                            g.FillRectangle(brush, rct);
                            rct.Inflate(1, 1);
                            rct.X += 1;
                            ControlPaint.DrawMenuGlyph(g, rct, MenuGlyph.Checkmark, this.selector.AreaCheckBox.TickColor, Color.Transparent);
                        }
                    }

                    // Draw MenuItem Text.
                    using (SolidBrush brush = new SolidBrush(this.selector.MenuItem.ItemForeColor))
                    {
                        g.DrawString(currentItem.ItemText, this.Font, brush, textRct, sf);
                    }
                }
            }

            // Draw exit button border, fill and it's string.
            CustomDataGridViewKRBColumnSelector.DataGridViewKRBColumnSelector.ButtonExit exitButton = this.selector.ExitButton;
            Rectangle exitRct = exitRectangle;
            ControlPaint.DrawBorder3D(g, exitRct, exitButton.ButtonBorderStyle, Border3DSide.All);
            Size size3d = SystemInformation.Border3DSize;
            exitRct.Inflate(-size3d.Width, -size3d.Height);
            using (SolidBrush brush = new SolidBrush(hotIndex == -1 ? exitButton.HoverBackColor : exitButton.BackColor))
            {
                g.FillRectangle(brush, exitRct);
                brush.Color = hotIndex == -1 ? exitButton.HoverForeColor : exitButton.ForeColor;
                //g.DrawString("Exit", this.Font, brush, exitRct, sf);
                g.DrawString("退出", this.Font, brush, exitRct, sf);
            }
        }

        #endregion

        #region MenuItemClass

        private class MenuItem : IDisposable
        {
            #region Instance Members

            private bool isChecked = false;                     // Initializer
            private string itemText = null;                     // Initializer
            private Rectangle itemRectangle = Rectangle.Empty;  // Initializer

            #endregion

            #region Constructor

            public MenuItem(string itemText)
            {
                this.itemText = itemText;
            }

            public MenuItem(string itemText, bool isChecked)
            {
                this.itemText = itemText;
                this.isChecked = isChecked;
            }

            #endregion

            #region Property

            /// <summary>
            /// Determines whether the menu item(or column visibility) is checked or not.
            /// </summary>
            public bool IsChecked
            {
                get { return isChecked; }
                set
                {
                    if (!value.Equals(isChecked))
                        isChecked = value;
                }
            }

            /// <summary>
            /// Displays the current datagridview column name.
            /// </summary>
            public string ItemText
            {
                get { return itemText; }
                set
                {
                    if (!value.Equals(itemText))
                        itemText = value;
                }
            }

            /// <summary>
            /// Gets or Sets the current menu item rectangle.
            /// </summary>
            public Rectangle ItemRectangle
            {
                get { return itemRectangle; }
                set
                {
                    if (!value.Equals(itemRectangle))
                        itemRectangle = value;
                }
            }

            #endregion

            #region IDisposable Members

            /// <summary>
            /// Dispose this class.
            /// </summary>
            public void Dispose()
            {
                GC.SuppressFinalize(this);
            }

            #endregion
        }

        #endregion
    }

    internal class MenuItemEventArgs : EventArgs
    {
        #region Instance Members

        private int columnIndex = -1;

        #endregion

        #region Constructor

        public MenuItemEventArgs(int columnIndex)
        {
            this.columnIndex = columnIndex;
        }

        #endregion

        #region Property

        public int ColumnIndex
        {
            get { return columnIndex; }
        }

        #endregion
    }
}