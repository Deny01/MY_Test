namespace KReport.Controls
{
    using KReport.Engine;
    using System;
    using System.ComponentModel;
    using System.Drawing;

    internal class RDBLabel : RLabel, IRDBControl
    {
        private string datasource = string.Empty;
        private string fieldName = string.Empty;
        private string formaString = string.Empty;
        private bool displayZeroValue = false;
        protected IReportData reportData;

        public RDBLabel()
        {
            base.controlType = ControlType.ControlDBText;
            this.BackColor = SystemColors.Window;
            base.Height = 10;
            base.Width = 30;
        }

        public override void DrawCommand(Graphics g, int offset)
        {
            if (this.reportData != null)
            {
                object fieldValue = this.reportData.GetFieldValue(this.数据字段, this.数据源);
                this.FormatText(fieldValue);
                base.DrawCommand(g, offset);
            }
        }

        protected void FormatText(object dataLink)
        {
            if (dataLink != null)
            {
                if (!string.IsNullOrEmpty(this.formaString))
                {
                    string str = dataLink.GetType().ToString();
                    if (dataLink.GetType() == typeof(short))
                    {
                        this.Text = string.Format(this.格式化显示, Convert.ToInt16(dataLink));
                        if (0 == short.Parse(this.Text) && false == displayZeroValue)
                        {
                            this.Text = "";
                        }
                    }
                    else if (dataLink.GetType() == typeof(int))
                    {
                        this.Text = string.Format(this.格式化显示, Convert.ToInt32(dataLink));
                        if (0 == int.Parse(this.Text) && false == displayZeroValue)
                        {
                            this.Text = "";
                        }
                    }
                    else if (dataLink.GetType() == typeof(long))
                    {
                        this.Text = string.Format(this.格式化显示, Convert.ToInt64(dataLink));
                        if (0 == long.Parse(this.Text) && false == displayZeroValue)
                        {
                            this.Text = "";
                        }
                    }
                    else if (dataLink.GetType() == typeof(double))
                    {
                        this.Text = string.Format(this.格式化显示, Convert.ToDouble(dataLink));
                        if (0 == double.Parse(this.Text) && false == displayZeroValue)
                        {
                            this.Text = "";
                        }
                    }
                    else if (dataLink.GetType() == typeof(DateTime))
                    {
                        this.Text = Convert.ToDateTime(dataLink).ToString(this.格式化显示);
                        if (null == dataLink)
                        {
                            this.Text = "";
                        }
                    }
                    else if (dataLink.GetType() == typeof(decimal))
                    {
                        this.Text = string.Format(this.格式化显示, Convert.ToDecimal(dataLink));
                        if (0 == decimal.Parse(this.Text) && false == displayZeroValue)
                        {
                            this.Text = "";
                        }
                    }
                }
                else
                {
                    this.Text = dataLink.ToString();
                }



            }
            else
            {
                this.Text = "";
            }

            if ("" == dataLink.ToString())
            {

                this.Text = "";
            }
        }

        //[Category("Custom")]
        [Description("使标签显示数据源"), Category("数据字段"), ReportElementAttribute("报表元素")]
        //public string DataSource
        public string 数据源
        {
            get
            {
                return this.datasource;
            }
            set
            {
                this.datasource = value;
            }
        }

        public IReportData DataSourceLink
        {
            get
            {
                return this.reportData;
            }
            set
            {
                this.reportData = value;
            }
        }

        //[Category("Custom")]
        [Description("使标签显示数据字段"), Category("数据字段"), ReportElementAttribute("报表元素")]
        //public string FieldName
        public string 数据字段
        {
            get
            {
                return this.fieldName;
            }
            set
            {
                this.fieldName = value;
            }
        }

        //[Category("Custom")]  
        [Description("格式化数据"), Category("数据字段"), ReportElementAttribute("报表元素")]
        //public string FormatString
        public string 格式化显示
        {
            get
            {
                return this.formaString;
            }
            set
            {
                this.formaString = value;
            }
        }

        [Description("显示 0 值"), Category("数据字段"), ReportElementAttribute("报表元素")]
        //public string FormatString
        public DisplayZero 显示0值
        {
            get
            {
                if (true  == this.displayZeroValue)
                    return DisplayZero.显示;
                else
                    return DisplayZero.隐藏;
            }
            set
            {
                if ( value == DisplayZero .显示 )
                    this.displayZeroValue = true ;
                else
                    this.displayZeroValue = false ;
            }
        }

        private class FormatStringDefault : StringConverter
        {
            private string[] lista = new string[] { "Fernando", "Lindo" };

            public override TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
            {
                return new TypeConverter.StandardValuesCollection(this.lista);
            }

            public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
            {
                return true;
            }
        }
    }
}

