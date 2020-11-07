namespace KReport.Controls
{
    using KReport.Engine;
    using System;
    using System.ComponentModel;
    using System.Drawing;

    internal class RDBBarCode : RBarCode, IRDBControl
    {
        private string datasource = string.Empty;
        private string fieldName = string.Empty;
        protected IReportData reportData;

        public RDBBarCode()
        {
            base.controlType = ControlType.ControlDBBarCode;
            base.CreateBarCode();
            base.Code = base.barcode.Code;
        }

        public override void DrawCommand(Graphics g, int offset)
        {
            if (this.reportData != null)
            {
                base.code = this.reportData.GetFieldValue(this.数据字段, this.数据源).ToString();
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
    }
}

