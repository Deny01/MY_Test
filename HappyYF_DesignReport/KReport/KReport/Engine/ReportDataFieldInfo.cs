namespace KReport.Engine
{
    using System;

    public class ReportDataFieldInfo
    {
        private string fieldname;
        private Type fieldtype;

        public ReportDataFieldInfo(string fieldname, Type fieldtype)
        {
            this.fieldname = fieldname;
            this.fieldtype = fieldtype;
        }

        public string FieldName
        {
            get
            {
                return this.fieldname;
            }
        }

        public Type FieldType
        {
            get
            {
                return this.fieldtype;
            }
        }
    }
}

