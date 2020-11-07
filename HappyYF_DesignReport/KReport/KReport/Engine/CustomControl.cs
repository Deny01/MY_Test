namespace KReport.Engine
{
    using KReport.Controls;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Reflection;
    using System.Windows.Forms;

    public class CustomControl : ControlBase, ICloneable
    {
        private BandBase _band = null;
        protected int altura;
        protected ControlType controlType;
        protected int direita;
        protected int esquerda;
        private int index = 1;
        protected int largura;
        protected int posX;
        protected int posY;
        public bool resizing = false;
        private int xRelative_;
        private int yRelative_;
       

        protected void ApplyEscala(Graphics g)
        {
            switch (g.PageUnit)
            {
                case GraphicsUnit.Display:
                    this.posX = (int) Utils.ConvertPixelToDisplay(base.Left);
                    this.esquerda = (int)Utils.ConvertPixelToDisplay(base.Left);
                    this.direita = (int) Utils.ConvertPixelToDisplay(base.Right);
                    this.largura = (int) Utils.ConvertPixelToDisplay(base.Width);
                    this.altura = (int) Utils.ConvertPixelToDisplay(base.Height);
                    break;

                case GraphicsUnit.Pixel:
                    this.esquerda = base.Left;
                    this.direita = base.Right;
                    this.largura = base.Width;
                    this.altura = base.Height;
                    break;
            }
        }

        public virtual object Clone()
        {
            CustomControl control = (CustomControl) Activator.CreateInstance(base.GetType());
            foreach (MemberInfo info4 in base.GetType().GetMembers(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance))
            {
                if (info4.MemberType == MemberTypes.Property)
                {
                    PropertyInfo info = info4 as PropertyInfo;
                    if ((((((info.PropertyType == typeof(long)) || (info.PropertyType == typeof(int))) || ((info.PropertyType == typeof(short)) || (info.PropertyType == typeof(int)))) || (((info.PropertyType == typeof(string)) || (info.PropertyType == typeof(double))) || ((info.PropertyType == typeof(bool)) || (info.PropertyType == typeof(DateTime))))) || ((((info.PropertyType == typeof(Font)) || (info.PropertyType == typeof(Color))) || ((info.PropertyType == typeof(TextAlignment)) || (info.PropertyType == typeof(LineAlignment)))) || (info.PropertyType == typeof(TypeCalculated)))) || (info.PropertyType == typeof(RSystemType)))
                    {
                        MemberInfo[] member = control.GetType().GetMember(info.Name, BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
                        if (member.Length != 0)
                        {
                            MemberInfo info3 = member[0];
                            PropertyInfo info2 = info3 as PropertyInfo;
                            if ((info2 != null) && info2.CanWrite)
                            {
                                info2.SetValue(control, info.GetValue(this, null), null);
                            }
                        }
                    }
                }
            }
            return control;
        }

        public void Draw(ReportDevice device)
        {
            device.DrawControl(this);
        }

        public virtual void Draw(Graphics g)
        {
        }

        public virtual void DrawCommand(Graphics g, int offset)
        {
        }

        public static CustomControl FactoryControl(ControlType controlType)
        {
            switch (controlType)
            {
                case ControlType.ControlText:
                    return new RLabel();

                case ControlType.ControlLine:
                    return new RLine();

                case ControlType.ControlShape:
                    return new RShape();

                case ControlType.ControlImage:
                    return new RImage();

                case ControlType.ControlMemo:
                    return new RMemo();

                case ControlType.ControlBarCode:
                    return new RBarCode();

                case ControlType.ControlDBText:
                    return new RDBLabel();

                case ControlType.ControlDBCalc:
                    return new RDBCalculated();

                case ControlType.ControlSystem:
                    return new RSystem();

                case ControlType.ControlChart:
                    return new RChart();

                case ControlType.ControlDBBarCode:
                    return new RDBBarCode();

                case ControlType.ControlDBImage:
                    return new RDBImage();

                case ControlType.ControlDBChart:
                    return new RDBChart();
            }
            throw new Exception("Controle desconhecido");
        }

        public void Load()
        {
            if (base.Name.Equals(string.Empty))
            {
                this.Band.Report.SetUniqueName(this);
            }
        }

        protected override void OnMove(EventArgs e)
        {
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
        }

        public BandBase Band
        {
            get
            {
                return this._band;
            }
            set
            {
                if (this._band != null)
                {
                    this._band.Controls.Remove(this);
                }
                this._band = value;
                if (this._band != null)
                {
                    this._band.AddControl(this);
                }
            }
        }

        public int Index
        {
            get
            {
                return this.index;
            }
            set
            {
                this.index = value;
            }
        }

        public ControlType Type
        {
            get
            {
                return this.controlType;
            }
        }

        //[ Category("Position")]
        [Description("水平位置"), Category("位置"), ReportElementAttribute("报表元素")]
        //public int xRelative
        public int 水平位置
        {
            get
            {
                return this.xRelative_;
            }
            set
            {
                this.xRelative_ = value;

                this.Left = value;
            }
        }

        //[Category("Position")]
        [Description("垂直位置"), Category("位置"), ReportElementAttribute("报表元素")]
        //public int yRelative
        public int 垂直位置
        {
            get
            {
                return this.yRelative_;
            }
            set
            {
                this.yRelative_ = value;
                if (null != this.Band)
                    this.Location = new Point(this.Location.X , this.Band.Top + value);
            }
        }

        [Description("宽度"), Category("宽高"), ReportElementAttribute("报表元素")]
        //public int yRelative
        public int 宽度
        {
            get
            {
                return base .Width ;
            }
            set
            {
                base.Width = value;
            }
        }
        [Description("高度"), Category("宽高"), ReportElementAttribute("报表元素")]
        //public int yRelative
        public int 高度
        {
            get
            {
                return base .Height;
            }
            set
            {
                base.Height = value;
            }
        }
    }
}

