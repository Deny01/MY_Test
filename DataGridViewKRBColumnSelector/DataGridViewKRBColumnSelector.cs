using System;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;
using System.ComponentModel;
using System.Drawing.Design;
using System.Drawing.Drawing2D;

namespace CustomDataGridViewKRBColumnSelector
{
    /// <summary>
    /// Add column show/hide capability to a DataGridView.When user right-clicks the any column's header a popup menu is shown.
    /// </summary>
    [ToolboxBitmap(typeof(DataGridViewKRBColumnSelector), "Icon.DataGridViewKRBColumnSelector.ico")]
    public partial class DataGridViewKRBColumnSelector : Component
    {
        #region Instance Members

        private int maxColumn = 10;                                 // Initializer
        private bool dropShadow = false;                            // Initializer
        private Color backColor = Color.Gainsboro;                  // Initializer
        private Color borderColor = Color.DarkGray;                 // Initializer
        private DataGridView gridView = null;                       // Initializer
        private StringAlignment alignment = StringAlignment.Near;   // Initializer
        private Font font = new Font("Tahoma", 8);

        private ItemMenu menuItem;
        private GradientBar barGradient;
        private CheckBoxArea areaCheckBox;
        private CheckBoxHoverArea hoverAreaCheckBox;
        private ButtonExit exitButton;

        private ColumnSelector dropDownControl = null;  // Initializer
        private ToolStripDropDown popup = null;         // Initializer

        #endregion

        #region Constructors

        public DataGridViewKRBColumnSelector()
        {
            InitializeComponent();
            CreateSelector();
        }

        public DataGridViewKRBColumnSelector(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
            CreateSelector();
        }

        #endregion

        #region Property

        /// <summary>
        /// Determines how much column is included in the selector.
        /// </summary>
        [Description("Determines how much column is included in the selector")]
        [DefaultValue(10)]
        [Browsable(true)]
        public int MaxColumn
        {
            get { return maxColumn; }
            set 
            {
                if (!value.Equals(maxColumn))
                    maxColumn = value;
            }
        }

        /// <summary>
        /// Determines whether the system drop shadow is visible or not.
        /// </summary>
        [Description("Determines whether the system drop shadow is visible or not")]
        [DefaultValue(false)]
        [Browsable(true)]
        public bool DropShadow
        {
            get { return dropShadow; }
            set 
            {
                if (!value.Equals(dropShadow))
                    dropShadow = value;
            }
        }

        /// <summary>
        /// Gets or sets the back color of the column selector.
        /// </summary>
        [Description("Gets or sets the back color of the column selector")]
        [DefaultValue(typeof(Color), "Gainsboro")]
        [Browsable(true)]
        public Color BackColor
        {
            get { return backColor; }
            set 
            {
                if (!value.Equals(backColor))
                    backColor = value;
            }
        }

        /// <summary>
        /// Gets or sets the border color of the column selector.
        /// </summary>
        [Description("Gets or sets the border color of the column selector")]
        [DefaultValue(typeof(Color), "DarkGray")]
        [Browsable(true)]
        public Color BorderColor
        {
            get { return borderColor; }
            set
            {
                if (!value.Equals(borderColor))
                    borderColor = value;
            }
        }
        
        /// <summary>
        /// Gets or sets the DataGridView to which the DataGridViewKRBColumnSelector is attached.
        /// </summary>
        [Description("Gets or sets the DataGridView to which the DataGridViewKRBColumnSelector is attached")]
        [Browsable(true)]
        public DataGridView GridView
        {
            get { return gridView; }
            set 
            {
                if (value == null)
                {
                    if (gridView != null)
                        gridView.CellMouseClick -= new DataGridViewCellMouseEventHandler(gridView_CellMouseClick);

                    gridView = value;
                }
                else
                {
                    if (!value.Equals(gridView))
                    {
                        if (gridView != null)
                            gridView.CellMouseClick -= new DataGridViewCellMouseEventHandler(gridView_CellMouseClick);

                        gridView = value;
                        gridView.CellMouseClick += new DataGridViewCellMouseEventHandler(gridView_CellMouseClick);
                    }
                }
            }
        }

        /// <summary>
        /// Gets or sets the positioning characteristics of the text in a text rectangle.
        /// </summary>
        [Description("Gets or sets the positioning characteristics of the text in a text rectangle")]
        [DefaultValue(typeof(StringAlignment), "Near")]
        [Browsable(true)]
        public StringAlignment Alignment
        {
            get { return alignment; }
            set 
            {
                if (!value.Equals(alignment))
                    alignment = value;
            }
        }

        /// <summary>
        /// Gets or sets the Font type of the column selector.
        /// </summary>
        [Description("The font used to display text in the column selector")]
        [DefaultValue(typeof(Font), "Tahoma, 8pt")]
        [Browsable(true)]
        public Font Font
        {
            get { return font; }
            set 
            {
                if (!value.Equals(font))
                    font = value;
            }
        }

        /// <summary>
        /// You can change the appearance of the MenuItem area from here.
        /// </summary>
        [Description("You can change the appearance of the MenuItem area from here")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [Browsable(true)]
        [Category("MenuItemAppearance")]
        public ItemMenu MenuItem
        {
            get { return menuItem; }
            set 
            { 
                if(!value.Equals(menuItem))
                    menuItem = value; ;
            }
        }

        /// <summary>
        /// You can change the appearance of the MenuBar area from here.
        /// </summary>
        [Description("You can change the appearance of the MenuBar area from here")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [Browsable(true)]
        [Category("BarAppearance")]
        public GradientBar BarGradient
        {
            get { return barGradient; }
            set 
            {
                if (!value.Equals(barGradient))
                    barGradient = value;
            }
        }

        /// <summary>
        /// You can change the appearance of the checkbox area from here.
        /// </summary>
        [Description("You can change the appearance of the checkbox area from here")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [Browsable(true)]
        [Category("CheckBoxAppearance")]
        public CheckBoxArea AreaCheckBox
        {
            get { return areaCheckBox; }
            set 
            {
                if (!value.Equals(areaCheckBox))
                    areaCheckBox = value;
            }
        }

        /// <summary>
        /// You can change the appearance of the checkbox hover area from here.
        /// </summary>
        [Description("You can change the appearance of the checkbox hover area from here")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [Browsable(true)]
        [Category("CheckBoxAppearance")]
        public CheckBoxHoverArea HoverAreaCheckBox
        {
            get { return hoverAreaCheckBox; }
            set 
            {
                if (!value.Equals(hoverAreaCheckBox))
                    hoverAreaCheckBox = value;
            }
        }

        /// <summary>
        /// You can change the appearance of the exit button from here.
        /// </summary>
        [Description("You can change the appearance of the exit button from here")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [Browsable(true)]
        [Category("ExitButtonAppearance")]
        public ButtonExit ExitButton
        {
            get { return exitButton; }
            set 
            {
                if (!value.Equals(exitButton))
                    exitButton = value;
            }
        }

        #endregion

        #region Helper Methods

        private void CreateSelector()
        {
            menuItem = new ItemMenu();
            barGradient = new GradientBar();
            areaCheckBox = new CheckBoxArea();
            hoverAreaCheckBox = new CheckBoxHoverArea();
            exitButton = new ButtonExit();

            popup = new ToolStripDropDown();
            popup.Padding = Padding.Empty;
            popup.AutoClose = true;
            popup.Closed += new ToolStripDropDownClosedEventHandler(popup_Closed);
        }

        private void popup_Closed(object sender, ToolStripDropDownClosedEventArgs e)
        {
            // Clear popup collection.
            popup.Items.Clear();
            // Detach column selector events.
            dropDownControl.Closed -= new EventHandler(dropDownControl_Closed);
            dropDownControl.MenuItemChanged -= new ColumnSelector.MenuItemEventHandler(dropDownControl_MenuItemChanged);
            // Dispose custom control.
            dropDownControl.Dispose();
        }

        private void dropDownControl_Closed(object sender, EventArgs e)
        {
            popup.Close(ToolStripDropDownCloseReason.ItemClicked);
        }
        
        private void dropDownControl_MenuItemChanged(object sender, MenuItemEventArgs ea)
        {
            DataGridViewColumn column = this.gridView.Columns[ea.ColumnIndex];
            column.Visible = !column.Visible;
        }

        private void gridView_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            //if (e.Button == MouseButtons.Right && e.RowIndex == -1)
            if (e.Button == MouseButtons.Right && e.RowIndex == -1 && e.ColumnIndex  != -1)
            {
                dropDownControl = new ColumnSelector(this, maxColumn);
                dropDownControl.Closed += new EventHandler(dropDownControl_Closed);
                dropDownControl.MenuItemChanged += new ColumnSelector.MenuItemEventHandler(dropDownControl_MenuItemChanged);

                ToolStripControlHost controlHost = new ToolStripControlHost(dropDownControl);
                controlHost.Margin = Padding.Empty;
                controlHost.Padding = Padding.Empty;
                controlHost.AutoSize = false;

                // Show column selector.
                popup.Items.Add(controlHost);
                popup.DropShadowEnabled = dropShadow;
                popup.Show(Cursor.Position, ToolStripDropDownDirection.Default);
            }
        }

        #endregion

        [Editor(typeof(CheckBoxAreaEditor), typeof(UITypeEditor))]
        [TypeConverter(typeof(CheckBoxAreaConverter))]
        public class CheckBoxArea : IDisposable
        {
            #region Event

            /// <summary>
            /// Occurs when the properties changed.
            /// </summary>
            public event EventHandler GradientChanged;

            #endregion

            #region Instance Members

            private Color colorStart = Color.WhiteSmoke;
            private Color colorEnd = Color.LightSteelBlue;
            private Color tickColor = Color.Maroon;
            private Color borderColor = Color.Blue;
            private LinearGradientMode gradientStyle = LinearGradientMode.Vertical;

            #endregion

            #region Constructor

            public CheckBoxArea() { }

            public CheckBoxArea(Color first, Color second, Color tickColor, Color borderColor, LinearGradientMode gradientStyle)
            {
                this.colorStart = first;
                this.colorEnd = second;
                this.tickColor = tickColor;
                this.borderColor = borderColor;
                this.gradientStyle = gradientStyle;
            }

            #endregion

            #region Property

            /// <summary>
            /// Gets or Sets the first checkbox gradient color.
            /// </summary>
            [Description("Gets or Sets the first checkbox gradient color")]
            [RefreshProperties(RefreshProperties.Repaint)]
            [NotifyParentProperty(true)]
            [DefaultValue(typeof(Color), "WhiteSmoke")]
            [Browsable(true)]
            public Color ColorStart
            {
                get { return colorStart; }
                set
                {
                    if (!value.Equals(colorStart))
                    {
                        colorStart = value;
                        OnGradientChanged(EventArgs.Empty);
                    }
                }
            }

            /// <summary>
            /// Gets or Sets the second checkbox gradient color.
            /// </summary>
            [Description("Gets or Sets the second checkbox gradient color")]
            [RefreshProperties(RefreshProperties.Repaint)]
            [NotifyParentProperty(true)]
            [DefaultValue(typeof(Color), "LightSteelBlue")]
            [Browsable(true)]
            public Color ColorEnd
            {
                get { return colorEnd; }
                set
                {
                    if (!value.Equals(colorEnd))
                    {
                        colorEnd = value;
                        OnGradientChanged(EventArgs.Empty);
                    }
                }
            }

            /// <summary>
            /// Gets or Sets the checkbox tick color.
            /// </summary>
            [Description("Gets or Sets the checkbox tick color")]
            [RefreshProperties(RefreshProperties.Repaint)]
            [NotifyParentProperty(true)]
            [DefaultValue(typeof(Color), "Maroon")]
            [Browsable(true)]
            public Color TickColor
            {
                get { return tickColor; }
                set
                {
                    if (!value.Equals(tickColor))
                    {
                        tickColor = value;
                        OnGradientChanged(EventArgs.Empty);
                    }
                }
            }

            /// <summary>
            /// Gets or Sets the checkbox area border color.
            /// </summary>
            [Description("Gets or Sets the checkbox area border color")]
            [RefreshProperties(RefreshProperties.Repaint)]
            [NotifyParentProperty(true)]
            [DefaultValue(typeof(Color), "Blue")]
            [Browsable(true)]
            public Color BorderColor
            {
                get { return borderColor; }
                set 
                {
                    if (!value.Equals(borderColor))
                    {
                        borderColor = value;
                        OnGradientChanged(EventArgs.Empty);
                    }
                }
            }

            /// <summary>
            /// Gets or Sets the checkbox area gradient style.
            /// </summary>
            [Description("Gets or Sets the checkbox area gradient style")]
            [RefreshProperties(RefreshProperties.Repaint)]
            [NotifyParentProperty(true)]
            [DefaultValue(typeof(LinearGradientMode), "Vertical")]
            [Browsable(true)]
            public LinearGradientMode GradientStyle
            {
                get { return gradientStyle; }
                set
                {
                    if (!value.Equals(gradientStyle))
                    {
                        gradientStyle = value;
                        OnGradientChanged(EventArgs.Empty);
                    }
                }
            }

            #endregion

            #region Helper Methods

            private void OnGradientChanged(EventArgs e)
            {
                if (GradientChanged != null)
                    GradientChanged(this, e);
            }

            #endregion

            #region IDisposable Members

            public void Dispose()
            {
                GC.SuppressFinalize(this);
            }

            #endregion
        }

        class CheckBoxAreaConverter : ExpandableObjectConverter
        {
            #region Override Methods

            //All the CanConvertTo() method needs to is check that the target type is a string.
            public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
            {
                if (destinationType == typeof(string))
                    return true;
                else
                    return base.CanConvertTo(context, destinationType);
            }

            //ConvertTo() simply checks that it can indeed convert to the desired type.
            public override object ConvertTo(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, Type destinationType)
            {
                if (destinationType == typeof(string))
                    return ToString(value);
                else
                    return base.ConvertTo(context, culture, value, destinationType);
            }

            /* The exact same process occurs in reverse when converting a CheckBoxArea object to a string.
            First the Properties window calls CanConvertFrom(). If it returns true, the next step is to call
            the ConvertFrom() method. */
            public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
            {
                if (sourceType == typeof(string))
                    return true;
                else
                    return base.CanConvertFrom(context, sourceType);
            }

            public override object ConvertFrom(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value)
            {
                if (value is string)
                    return FromString(value);
                else
                    return base.ConvertFrom(context, culture, value);
            }

            #endregion

            #region Helper Methods

            private string ToString(object value)
            {
                CheckBoxArea boxArea = value as CheckBoxArea;    // Gelen object tipimizi CheckBoxArea tipine dönüştürüyoruz ve ayıklama işlemine başlıyoruz.
                ColorConverter converter = new ColorConverter();
                return String.Format("{0}, {1}, {2}, {3}, {4}",
                    converter.ConvertToString(boxArea.ColorStart), converter.ConvertToString(boxArea.ColorEnd), converter.ConvertToString(boxArea.TickColor), converter.ConvertToString(boxArea.BorderColor), boxArea.GradientStyle);
            }

            private CheckBoxArea FromString(object value)
            {
                string[] result = ((string)value).Split(',');
                if (result.Length != 5)
                    throw new ArgumentException("Could not convert to value");

                try
                {
                    CheckBoxArea boxArea = new CheckBoxArea();

                    // Retrieve the colors
                    ColorConverter converter = new ColorConverter();
                    boxArea.ColorStart = (Color)converter.ConvertFromString(result[0]);
                    boxArea.ColorEnd = (Color)converter.ConvertFromString(result[1]);
                    boxArea.TickColor = (Color)converter.ConvertFromString(result[2]);
                    boxArea.BorderColor = (Color)converter.ConvertFromString(result[3]);
                    boxArea.GradientStyle = (LinearGradientMode)Enum.Parse(typeof(LinearGradientMode), result[4], true);

                    return boxArea;
                }
                catch (Exception)
                {
                    throw new ArgumentException("Could not convert to value");
                }
            }

            #endregion
        }

        class CheckBoxAreaEditor : UITypeEditor
        {
            #region Override Methods

            public override bool GetPaintValueSupported(ITypeDescriptorContext context)
            {
                return true;
            }

            public override void PaintValue(PaintValueEventArgs e)
            {
                CheckBoxArea boxArea = e.Value as CheckBoxArea;
                using (LinearGradientBrush brush = new LinearGradientBrush(e.Bounds, boxArea.ColorStart, boxArea.ColorEnd, boxArea.GradientStyle))
                {
                    e.Graphics.FillRectangle(brush, e.Bounds);
                }
            }

            #endregion
        }

        [Editor(typeof(CheckBoxHoverAreaEditor), typeof(UITypeEditor))]
        [TypeConverter(typeof(CheckBoxHoverAreaConverter))]
        public class CheckBoxHoverArea : IDisposable
        {
            #region Event

            /// <summary>
            /// Occurs when the properties changed.
            /// </summary>
            public event EventHandler GradientChanged;

            #endregion

            #region Instance Members

            private Color colorStart = Color.WhiteSmoke;
            private Color colorEnd = Color.DeepSkyBlue;
            private Color tickColor = Color.Maroon;
            private Color borderColor = Color.Blue;
            private LinearGradientMode gradientStyle = LinearGradientMode.ForwardDiagonal;

            #endregion

            #region Constructor

            public CheckBoxHoverArea() { }

            public CheckBoxHoverArea(Color first, Color second, Color tickColor, Color borderColor, LinearGradientMode gradientStyle)
            {
                this.colorStart = first;
                this.colorEnd = second;
                this.tickColor = tickColor;
                this.borderColor = borderColor;
                this.gradientStyle = gradientStyle;
            }

            #endregion

            #region Property

            /// <summary>
            /// Gets or Sets the first checkbox hover gradient color.
            /// </summary>
            [Description("Gets or Sets the first checkbox hover gradient color")]
            [RefreshProperties(RefreshProperties.Repaint)]
            [NotifyParentProperty(true)]
            [DefaultValue(typeof(Color), "WhiteSmoke")]
            [Browsable(true)]
            public Color ColorStart
            {
                get { return colorStart; }
                set
                {
                    if (!value.Equals(colorStart))
                    {
                        colorStart = value;
                        OnGradientChanged(EventArgs.Empty);
                    }
                }
            }

            /// <summary>
            /// Gets or Sets the second checkbox hover gradient color.
            /// </summary>
            [Description("Gets or Sets the second checkbox hover gradient color")]
            [RefreshProperties(RefreshProperties.Repaint)]
            [NotifyParentProperty(true)]
            [DefaultValue(typeof(Color), "DeepSkyBlue")]
            [Browsable(true)]
            public Color ColorEnd
            {
                get { return colorEnd; }
                set
                {
                    if (!value.Equals(colorEnd))
                    {
                        colorEnd = value;
                        OnGradientChanged(EventArgs.Empty);
                    }
                }
            }

            /// <summary>
            /// Gets or Sets the checkbox hover tick color.
            /// </summary>
            [Description("Gets or Sets the checkbox hover tick color")]
            [RefreshProperties(RefreshProperties.Repaint)]
            [NotifyParentProperty(true)]
            [DefaultValue(typeof(Color), "Maroon")]
            [Browsable(true)]
            public Color TickColor
            {
                get { return tickColor; }
                set
                {
                    if (!value.Equals(tickColor))
                    {
                        tickColor = value;
                        OnGradientChanged(EventArgs.Empty);
                    }
                }
            }

            /// <summary>
            /// Gets or Sets the checkbox hover area border color.
            /// </summary>
            [Description("Gets or Sets the checkbox hover area border color")]
            [RefreshProperties(RefreshProperties.Repaint)]
            [NotifyParentProperty(true)]
            [DefaultValue(typeof(Color), "Blue")]
            [Browsable(true)]
            public Color BorderColor
            {
                get { return borderColor; }
                set
                {
                    if (!value.Equals(borderColor))
                    {
                        borderColor = value;
                        OnGradientChanged(EventArgs.Empty);
                    }
                }
            }

            /// <summary>
            /// Gets or Sets the checkbox hover area gradient style.
            /// </summary>
            [Description("Gets or Sets the checkbox hover area gradient style")]
            [RefreshProperties(RefreshProperties.Repaint)]
            [NotifyParentProperty(true)]
            [DefaultValue(typeof(LinearGradientMode), "ForwardDiagonal")]
            [Browsable(true)]
            public LinearGradientMode GradientStyle
            {
                get { return gradientStyle; }
                set
                {
                    if (!value.Equals(gradientStyle))
                    {
                        gradientStyle = value;
                        OnGradientChanged(EventArgs.Empty);
                    }
                }
            }

            #endregion

            #region Helper Methods

            private void OnGradientChanged(EventArgs e)
            {
                if (GradientChanged != null)
                    GradientChanged(this, e);
            }

            #endregion

            #region IDisposable Members

            public void Dispose()
            {
                GC.SuppressFinalize(this);
            }

            #endregion
        }

        class CheckBoxHoverAreaConverter : ExpandableObjectConverter
        {
            #region Override Methods

            //All the CanConvertTo() method needs to is check that the target type is a string.
            public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
            {
                if (destinationType == typeof(string))
                    return true;
                else
                    return base.CanConvertTo(context, destinationType);
            }

            //ConvertTo() simply checks that it can indeed convert to the desired type.
            public override object ConvertTo(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, Type destinationType)
            {
                if (destinationType == typeof(string))
                    return ToString(value);
                else
                    return base.ConvertTo(context, culture, value, destinationType);
            }

            /* The exact same process occurs in reverse when converting a CheckBoxHoverArea object to a string.
            First the Properties window calls CanConvertFrom(). If it returns true, the next step is to call
            the ConvertFrom() method. */
            public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
            {
                if (sourceType == typeof(string))
                    return true;
                else
                    return base.CanConvertFrom(context, sourceType);
            }

            public override object ConvertFrom(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value)
            {
                if (value is string)
                    return FromString(value);
                else
                    return base.ConvertFrom(context, culture, value);
            }

            #endregion

            #region Helper Methods

            private string ToString(object value)
            {
                CheckBoxHoverArea boxHoverArea = value as CheckBoxHoverArea;    // Gelen object tipimizi CheckBoxHoverArea tipine dönüştürüyoruz ve ayıklama işlemine başlıyoruz.
                ColorConverter converter = new ColorConverter();
                return String.Format("{0}, {1}, {2}, {3}, {4}",
                    converter.ConvertToString(boxHoverArea.ColorStart), converter.ConvertToString(boxHoverArea.ColorEnd),
                    converter.ConvertToString(boxHoverArea.TickColor), converter.ConvertToString(boxHoverArea.BorderColor), boxHoverArea.GradientStyle);
            }

            private CheckBoxHoverArea FromString(object value)
            {
                string[] result = ((string)value).Split(',');
                if (result.Length != 5)
                    throw new ArgumentException("Could not convert to value");

                try
                {
                    CheckBoxHoverArea boxHoverArea = new CheckBoxHoverArea();

                    // Retrieve the colors
                    ColorConverter converter = new ColorConverter();
                    boxHoverArea.ColorStart = (Color)converter.ConvertFromString(result[0]);
                    boxHoverArea.ColorEnd = (Color)converter.ConvertFromString(result[1]);
                    boxHoverArea.TickColor = (Color)converter.ConvertFromString(result[2]);
                    boxHoverArea.BorderColor = (Color)converter.ConvertFromString(result[3]);
                    boxHoverArea.GradientStyle = (LinearGradientMode)Enum.Parse(typeof(LinearGradientMode), result[4], true);

                    return boxHoverArea;
                }
                catch (Exception)
                {
                    throw new ArgumentException("Could not convert to value");
                }
            }

            #endregion
        }

        class CheckBoxHoverAreaEditor : UITypeEditor
        {
            #region Override Methods

            public override bool GetPaintValueSupported(ITypeDescriptorContext context)
            {
                return true;
            }

            public override void PaintValue(PaintValueEventArgs e)
            {
                CheckBoxHoverArea boxHoverArea = e.Value as CheckBoxHoverArea;
                using (LinearGradientBrush brush = new LinearGradientBrush(e.Bounds, boxHoverArea.ColorStart, boxHoverArea.ColorEnd, boxHoverArea.GradientStyle))
                {
                    e.Graphics.FillRectangle(brush, e.Bounds);
                }
            }

            #endregion
        }

        [Editor(typeof(ItemMenuEditor), typeof(UITypeEditor))]
        [TypeConverter(typeof(ItemMenuConverter))]
        public class ItemMenu : IDisposable
        {
            #region Event

            /// <summary>
            /// Occurs when the properties changed.
            /// </summary>
            public event EventHandler GradientChanged;

            #endregion

            #region Instance Members

            private Color colorStart = Color.WhiteSmoke;
            private Color colorEnd = Color.SandyBrown;
            private Color itemForeColor = Color.Black;
            private Color itemHoverForeColor = Color.Maroon;
            private Color itemBorderColor = Color.Gray;
            private LinearGradientMode gradientStyle = LinearGradientMode.BackwardDiagonal;

            #endregion

            #region Constructor

            public ItemMenu() { }

            public ItemMenu(Color first, Color second, Color itemForeColor, Color itemHoverForeColor,
                Color itemBorderColor, LinearGradientMode gradientStyle)
            {
                this.colorStart = first;
                this.colorEnd = second;
                this.itemForeColor = itemForeColor;
                this.itemHoverForeColor = itemHoverForeColor;
                this.itemBorderColor = itemBorderColor;
                this.gradientStyle = gradientStyle;
            }

            #endregion

            #region Property

            /// <summary>
            /// Gets or Sets the first MenuItem gradient color.
            /// </summary>
            [Description("Gets or Sets the first MenuItem gradient color")]
            [RefreshProperties(RefreshProperties.Repaint)]
            [NotifyParentProperty(true)]
            [DefaultValue(typeof(Color), "WhiteSmoke")]
            [Browsable(true)]
            public Color ColorStart
            {
                get { return colorStart; }
                set
                {
                    if (!value.Equals(colorStart))
                    {
                        colorStart = value;
                        OnGradientChanged(EventArgs.Empty);
                    }
                }
            }

            /// <summary>
            /// Gets or Sets the second MenuItem gradient color.
            /// </summary>
            [Description("Gets or Sets the second MenuItem gradient color")]
            [RefreshProperties(RefreshProperties.Repaint)]
            [NotifyParentProperty(true)]
            [DefaultValue(typeof(Color), "SandyBrown")]
            [Browsable(true)]
            public Color ColorEnd
            {
                get { return colorEnd; }
                set
                {
                    if (!value.Equals(colorEnd))
                    {
                        colorEnd = value;
                        OnGradientChanged(EventArgs.Empty);
                    }
                }
            }

            /// <summary>
            /// Gets or Sets the MenuItem fore color.
            /// </summary>
            [Description("Gets or Sets the MenuItem fore color")]
            [RefreshProperties(RefreshProperties.Repaint)]
            [NotifyParentProperty(true)]
            [DefaultValue(typeof(Color), "Black")]
            [Browsable(true)]
            public Color ItemForeColor
            {
                get { return itemForeColor; }
                set
                {
                    if (!value.Equals(itemForeColor))
                    {
                        itemForeColor = value;
                        OnGradientChanged(EventArgs.Empty);
                    }
                }
            }

            /// <summary>
            /// Gets or Sets the MenuItem hover fore color.
            /// </summary>
            [Description("Gets or Sets the MenuItem hover fore color")]
            [RefreshProperties(RefreshProperties.Repaint)]
            [NotifyParentProperty(true)]
            [DefaultValue(typeof(Color), "Maroon")]
            [Browsable(true)]
            public Color ItemHoverForeColor
            {
                get { return itemHoverForeColor; }
                set 
                {
                    if (!value.Equals(itemHoverForeColor))
                    {
                        itemHoverForeColor = value;
                        OnGradientChanged(EventArgs.Empty);
                    }
                }
            }

            /// <summary>
            /// Gets or Sets the MenuItem area border color.
            /// </summary>
            [Description("Gets or Sets the MenuItem area border color")]
            [RefreshProperties(RefreshProperties.Repaint)]
            [NotifyParentProperty(true)]
            [DefaultValue(typeof(Color), "Gray")]
            [Browsable(true)]
            public Color ItemBorderColor
            {
                get { return itemBorderColor; }
                set
                {
                    if (!value.Equals(itemBorderColor))
                    {
                        itemBorderColor = value;
                        OnGradientChanged(EventArgs.Empty);
                    }
                }
            }

            /// <summary>
            /// Gets or Sets the MenuItem area gradient style.
            /// </summary>
            [Description("Gets or Sets the MenuItem area gradient style")]
            [RefreshProperties(RefreshProperties.Repaint)]
            [NotifyParentProperty(true)]
            [DefaultValue(typeof(LinearGradientMode), "BackwardDiagonal")]
            [Browsable(true)]
            public LinearGradientMode GradientStyle
            {
                get { return gradientStyle; }
                set
                {
                    if (!value.Equals(gradientStyle))
                    {
                        gradientStyle = value;
                        OnGradientChanged(EventArgs.Empty);
                    }
                }
            }

            #endregion

            #region Helper Methods

            private void OnGradientChanged(EventArgs e)
            {
                if (GradientChanged != null)
                    GradientChanged(this, e);
            }

            #endregion

            #region IDisposable Members

            public void Dispose()
            {
                GC.SuppressFinalize(this);
            }

            #endregion
        }

        class ItemMenuConverter : ExpandableObjectConverter
        {
            #region Override Methods

            //All the CanConvertTo() method needs to is check that the target type is a string.
            public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
            {
                if (destinationType == typeof(string))
                    return true;
                else
                    return base.CanConvertTo(context, destinationType);
            }

            //ConvertTo() simply checks that it can indeed convert to the desired type.
            public override object ConvertTo(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, Type destinationType)
            {
                if (destinationType == typeof(string))
                    return ToString(value);
                else
                    return base.ConvertTo(context, culture, value, destinationType);
            }

            /* The exact same process occurs in reverse when converting a ItemMenu object to a string.
            First the Properties window calls CanConvertFrom(). If it returns true, the next step is to call
            the ConvertFrom() method. */
            public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
            {
                if (sourceType == typeof(string))
                    return true;
                else
                    return base.CanConvertFrom(context, sourceType);
            }

            public override object ConvertFrom(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value)
            {
                if (value is string)
                    return FromString(value);
                else
                    return base.ConvertFrom(context, culture, value);
            }

            #endregion

            #region Helper Methods

            private string ToString(object value)
            {
                ItemMenu item = value as ItemMenu;    // Gelen object tipimizi ItemMenu tipine dönüştürüyoruz ve ayıklama işlemine başlıyoruz.
                ColorConverter converter = new ColorConverter();
                return String.Format("{0}, {1}, {2}, {3}, {4}, {5}",
                    converter.ConvertToString(item.ColorStart), converter.ConvertToString(item.ColorEnd), converter.ConvertToString(item.ItemForeColor),
                    converter.ConvertToString(item.ItemHoverForeColor), converter.ConvertToString(item.ItemBorderColor), item.GradientStyle);
            }

            private ItemMenu FromString(object value)
            {
                string[] result = ((string)value).Split(',');
                if (result.Length != 6)
                    throw new ArgumentException("Could not convert to value");

                try
                {
                    ItemMenu item = new ItemMenu();

                    // Retrieve the colors
                    ColorConverter converter = new ColorConverter();
                    item.ColorStart = (Color)converter.ConvertFromString(result[0]);
                    item.ColorEnd = (Color)converter.ConvertFromString(result[1]);
                    item.ItemForeColor = (Color)converter.ConvertFromString(result[2]);
                    item.ItemHoverForeColor = (Color)converter.ConvertFromString(result[3]);
                    item.ItemBorderColor = (Color)converter.ConvertFromString(result[4]);
                    item.GradientStyle = (LinearGradientMode)Enum.Parse(typeof(LinearGradientMode), result[5], true);

                    return item;
                }
                catch (Exception)
                {
                    throw new ArgumentException("Could not convert to value");
                }
            }

            #endregion
        }

        class ItemMenuEditor : UITypeEditor
        {
            #region Override Methods

            public override bool GetPaintValueSupported(ITypeDescriptorContext context)
            {
                return true;
            }

            public override void PaintValue(PaintValueEventArgs e)
            {
                ItemMenu item = e.Value as ItemMenu;
                using (LinearGradientBrush brush = new LinearGradientBrush(e.Bounds, item.ColorStart, item.ColorEnd, item.GradientStyle))
                {
                    e.Graphics.FillRectangle(brush, e.Bounds);
                }
            }

            #endregion
        }
        
        [Editor(typeof(GradientBarEditor), typeof(UITypeEditor))]
        [TypeConverter(typeof(GradientBarConverter))]
        public class GradientBar : IDisposable
        {
            #region Event

            /// <summary>
            /// Occurs when the properties changed.
            /// </summary>
            public event EventHandler GradientChanged;

            #endregion

            #region Instance Members

            private Color colorStart = Color.WhiteSmoke;
            private Color colorEnd = Color.Gainsboro;
            private LinearGradientMode gradientStyle = LinearGradientMode.Vertical;

            #endregion

            #region Constructor

            public GradientBar() { }

            public GradientBar(Color first, Color second, LinearGradientMode gradientStyle)
            {
                this.colorStart = first;
                this.colorEnd = second;
                this.gradientStyle = gradientStyle;
            }

            #endregion

            #region Property

            /// <summary>
            /// Gets or Sets the first gradient bar color.
            /// </summary>
            [Description("Gets or Sets the first gradient bar color")]
            [RefreshProperties(RefreshProperties.Repaint)]
            [NotifyParentProperty(true)]
            [DefaultValue(typeof(Color), "WhiteSmoke")]
            [Browsable(true)]
            public Color ColorStart
            {
                get { return colorStart; }
                set
                {
                    if (!value.Equals(colorStart))
                    {
                        colorStart = value;
                        OnGradientChanged(EventArgs.Empty);
                    }
                }
            }

            /// <summary>
            /// Gets or Sets the second gradient bar color.
            /// </summary>
            [Description("Gets or Sets the second gradient bar color")]
            [RefreshProperties(RefreshProperties.Repaint)]
            [NotifyParentProperty(true)]
            [DefaultValue(typeof(Color), "Gainsboro")]
            [Browsable(true)]
            public Color ColorEnd
            {
                get { return colorEnd; }
                set
                {
                    if (!value.Equals(colorEnd))
                    {
                        colorEnd = value;
                        OnGradientChanged(EventArgs.Empty);
                    }
                }
            }

            /// <summary>
            /// Gets or Sets the bar gradient style.
            /// </summary>
            [Description("Gets or Sets the bar gradient style")]
            [RefreshProperties(RefreshProperties.Repaint)]
            [NotifyParentProperty(true)]
            [DefaultValue(typeof(LinearGradientMode), "Vertical")]
            [Browsable(true)]
            public LinearGradientMode GradientStyle
            {
                get { return gradientStyle; }
                set
                {
                    if (!value.Equals(gradientStyle))
                    {
                        gradientStyle = value;
                        OnGradientChanged(EventArgs.Empty);
                    }
                }
            }

            #endregion

            #region Helper Methods

            private void OnGradientChanged(EventArgs e)
            {
                if (GradientChanged != null)
                    GradientChanged(this, e);
            }

            #endregion

            #region IDisposable Members

            public void Dispose()
            {
                GC.SuppressFinalize(this);
            }

            #endregion
        }

        class GradientBarConverter : ExpandableObjectConverter
        {
            #region Override Methods

            //All the CanConvertTo() method needs to is check that the target type is a string.
            public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
            {
                if (destinationType == typeof(string))
                    return true;
                else
                    return base.CanConvertTo(context, destinationType);
            }

            //ConvertTo() simply checks that it can indeed convert to the desired type.
            public override object ConvertTo(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, Type destinationType)
            {
                if (destinationType == typeof(string))
                    return ToString(value);
                else
                    return base.ConvertTo(context, culture, value, destinationType);
            }

            /* The exact same process occurs in reverse when converting a GradientBar object to a string.
            First the Properties window calls CanConvertFrom(). If it returns true, the next step is to call
            the ConvertFrom() method. */
            public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
            {
                if (sourceType == typeof(string))
                    return true;
                else
                    return base.CanConvertFrom(context, sourceType);
            }

            public override object ConvertFrom(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value)
            {
                if (value is string)
                    return FromString(value);
                else
                    return base.ConvertFrom(context, culture, value);
            }

            #endregion

            #region Helper Methods

            private string ToString(object value)
            {
                GradientBar gradient = value as GradientBar;    // Gelen object tipimizi GradientBar tipine dönüştürüyoruz ve ayıklama işlemine başlıyoruz.
                ColorConverter converter = new ColorConverter();
                return String.Format("{0}, {1}, {2}",
                    converter.ConvertToString(gradient.ColorStart), converter.ConvertToString(gradient.ColorEnd), gradient.GradientStyle);
            }

            private GradientBar FromString(object value)
            {
                string[] result = ((string)value).Split(',');
                if (result.Length != 3)
                    throw new ArgumentException("Could not convert to value");

                try
                {
                    GradientBar gradient = new GradientBar();

                    // Retrieve the colors
                    ColorConverter converter = new ColorConverter();
                    gradient.ColorStart = (Color)converter.ConvertFromString(result[0]);
                    gradient.ColorEnd = (Color)converter.ConvertFromString(result[1]);
                    gradient.GradientStyle = (LinearGradientMode)Enum.Parse(typeof(LinearGradientMode), result[2], true);

                    return gradient;
                }
                catch (Exception)
                {
                    throw new ArgumentException("Could not convert to value");
                }
            }

            #endregion
        }

        class GradientBarEditor : UITypeEditor
        {
            #region Override Methods

            public override bool GetPaintValueSupported(ITypeDescriptorContext context)
            {
                return true;
            }

            public override void PaintValue(PaintValueEventArgs e)
            {
                GradientBar gradient = e.Value as GradientBar;
                using (LinearGradientBrush brush = new LinearGradientBrush(e.Bounds, gradient.ColorStart, gradient.ColorEnd, gradient.GradientStyle))
                {
                    e.Graphics.FillRectangle(brush, e.Bounds);
                }
            }

            #endregion
        }

        [TypeConverter(typeof(ButtonExitConverter))]
        public class ButtonExit : IDisposable
        {
            #region Event

            /// <summary>
            /// Occurs when the properties changed.
            /// </summary>
            public event EventHandler ExitButtonChanged;

            #endregion

            #region Instance Members

            private Color backColor = Color.WhiteSmoke;
            private Color hoverBackColor = Color.Red;
            private Color foreColor = Color.Black;
            private Color hoverForeColor = Color.White;
            private Border3DStyle buttonBorderStyle = Border3DStyle.Etched;

            #endregion

            #region Constructor

            public ButtonExit() { }

            public ButtonExit(Color backColor, Color hoverBackColor, Color foreColor, Color hoverForeColor,
                Border3DStyle buttonBorderStyle)
            {
                this.backColor = backColor;
                this.hoverBackColor = hoverBackColor;
                this.foreColor = foreColor;
                this.hoverForeColor = hoverForeColor;
                this.buttonBorderStyle = buttonBorderStyle;
            }

            #endregion

            #region Property

            /// <summary>
            /// Gets or Sets the back color of the exit button.
            /// </summary>
            [Description("Gets or Sets the back color of the exit button")]
            [RefreshProperties(RefreshProperties.Repaint)]
            [NotifyParentProperty(true)]
            [DefaultValue(typeof(Color), "WhiteSmoke")]
            [Browsable(true)]
            public Color BackColor
            {
                get { return backColor; }
                set
                {
                    if (!value.Equals(backColor))
                    {
                        backColor = value;
                        OnExitButtonChanged(EventArgs.Empty);
                    }
                }
            }

            /// <summary>
            /// Gets or Sets the hover back color of the exit button.
            /// </summary>
            [Description("Gets or Sets the hover back color of the exit button")]
            [RefreshProperties(RefreshProperties.Repaint)]
            [NotifyParentProperty(true)]
            [DefaultValue(typeof(Color), "Red")]
            [Browsable(true)]
            public Color HoverBackColor
            {
                get { return hoverBackColor; }
                set
                {
                    if (!value.Equals(hoverBackColor))
                    {
                        hoverBackColor = value;
                        OnExitButtonChanged(EventArgs.Empty);
                    }
                }
            }

            /// <summary>
            /// Gets or Sets the fore color of the exit button.
            /// </summary>
            [Description("Gets or Sets the fore color of the exit button")]
            [RefreshProperties(RefreshProperties.Repaint)]
            [NotifyParentProperty(true)]
            [DefaultValue(typeof(Color), "Black")]
            [Browsable(true)]
            public Color ForeColor
            {
                get { return foreColor; }
                set
                {
                    if (!value.Equals(foreColor))
                    {
                        foreColor = value;
                        OnExitButtonChanged(EventArgs.Empty);
                    }
                }
            }

            /// <summary>
            /// Gets or Sets the hover fore color of the exit button.
            /// </summary>
            [Description("Gets or Sets the hover fore color of the exit button")]
            [RefreshProperties(RefreshProperties.Repaint)]
            [NotifyParentProperty(true)]
            [DefaultValue(typeof(Color), "White")]
            [Browsable(true)]
            public Color HoverForeColor
            {
                get { return hoverForeColor; }
                set
                {
                    if (!value.Equals(hoverForeColor))
                    {
                        hoverForeColor = value;
                        OnExitButtonChanged(EventArgs.Empty);
                    }
                }
            }

            /// <summary>
            /// Gets or Sets the exit button border style.
            /// </summary>
            [Description("Gets or Sets the exit button border style")]
            [RefreshProperties(RefreshProperties.Repaint)]
            [NotifyParentProperty(true)]
            [DefaultValue(typeof(Border3DStyle), "Etched")]
            [Browsable(true)]
            public Border3DStyle ButtonBorderStyle
            {
                get { return buttonBorderStyle; }
                set 
                {
                    if (!value.Equals(buttonBorderStyle))
                    {
                        buttonBorderStyle = value;
                        OnExitButtonChanged(EventArgs.Empty);
                    }
                }
            }

            #endregion

            #region Helper Methods

            private void OnExitButtonChanged(EventArgs e)
            {
                if (ExitButtonChanged != null)
                    ExitButtonChanged(this, e);
            }

            #endregion

            #region IDisposable Members

            public void Dispose()
            {
                GC.SuppressFinalize(this);
            }

            #endregion
        }

        class ButtonExitConverter : ExpandableObjectConverter
        {
            #region Override Methods

            //All the CanConvertTo() method needs to is check that the target type is a string.
            public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
            {
                if (destinationType == typeof(string))
                    return true;
                else
                    return base.CanConvertTo(context, destinationType);
            }

            //ConvertTo() simply checks that it can indeed convert to the desired type.
            public override object ConvertTo(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, Type destinationType)
            {
                if (destinationType == typeof(string))
                    return ToString(value);
                else
                    return base.ConvertTo(context, culture, value, destinationType);
            }

            /* The exact same process occurs in reverse when converting a ButtonExit object to a string.
            First the Properties window calls CanConvertFrom(). If it returns true, the next step is to call
            the ConvertFrom() method. */
            public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
            {
                if (sourceType == typeof(string))
                    return true;
                else
                    return base.CanConvertFrom(context, sourceType);
            }

            public override object ConvertFrom(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value)
            {
                if (value is string)
                    return FromString(value);
                else
                    return base.ConvertFrom(context, culture, value);
            }

            #endregion

            #region Helper Methods

            private string ToString(object value)
            {
                ButtonExit exit = value as ButtonExit;    // Gelen object tipimizi ButtonExit tipine dönüştürüyoruz ve ayıklama işlemine başlıyoruz.
                ColorConverter converter = new ColorConverter();
                return String.Format("{0}, {1}, {2}, {3}, {4}",
                    converter.ConvertToString(exit.BackColor), converter.ConvertToString(exit.HoverBackColor), converter.ConvertToString(exit.ForeColor),
                    converter.ConvertToString(exit.HoverForeColor), exit.ButtonBorderStyle);
            }

            private ButtonExit FromString(object value)
            {
                string[] result = ((string)value).Split(',');
                if (result.Length != 5)
                    throw new ArgumentException("Could not convert to value");

                try
                {
                    ButtonExit exit = new ButtonExit();

                    // Retrieve the colors
                    ColorConverter converter = new ColorConverter();
                    exit.BackColor = (Color)converter.ConvertFromString(result[0]);
                    exit.HoverBackColor = (Color)converter.ConvertFromString(result[1]);
                    exit.ForeColor = (Color)converter.ConvertFromString(result[2]);
                    exit.HoverForeColor = (Color)converter.ConvertFromString(result[3]);
                    exit.ButtonBorderStyle = (Border3DStyle)Enum.Parse(typeof(Border3DStyle), result[4], true);

                    return exit;
                }
                catch (Exception)
                {
                    throw new ArgumentException("Could not convert to value");
                }
            }

            #endregion
        }
    }
}