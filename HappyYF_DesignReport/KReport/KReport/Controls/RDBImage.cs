namespace KReport.Controls
{
    using KReport.Engine;
    using System;
    using System.ComponentModel;
    using System.Drawing;

    internal class RDBImage : RImage, IRDBControl
    {
        private string datasource;
        private string fieldName;
        protected IReportData reportData;

        public RDBImage()
        {
            base.image = null;
            base.controlType = ControlType.ControlDBImage;
            this.ForeColor = Color.Black;
            this.BackColor = Color.White;
            base.边界颜色 = Color.Black;
            base.边界线宽 = 1;
            base.Height = 150;
            base.Width = 150;
        }

        public override void DrawCommand(Graphics g, int offset)
        {
            if (this.reportData != null)
            {
                object fieldValue = this.reportData.GetFieldValue(this.数据字段, this.数据源);
                base.image = (Bitmap) fieldValue;
                base.DrawCommand(g, offset);
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
        [Description("数据源图片字段"), Category("数据字段"), ReportElementAttribute("报表元素")]
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
    }
}

