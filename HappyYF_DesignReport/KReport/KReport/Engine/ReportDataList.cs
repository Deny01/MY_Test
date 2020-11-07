namespace KReport.Engine
{
    using System;
    using System.Collections;
    using System.Reflection;

    public class ReportDataList : IReportData
    {
        private int posFile;
        private PropertyInfo[] properties;
        private IList sourceList = null;
        private string sourcename;

        public ReportDataList(ArrayList source, string sourcename)
        {
            this.sourceList = source;
            this.sourcename = sourcename;
            this.PrepareFields();
        }

        public void DataSourceFirstRecord()
        {
            this.posFile = 0;
        }

        public void DataSourceNextRecord()
        {
            if (!this.EndFile())
            {
                this.posFile++;
            }
        }

        public void DataSourcePriorRecord()
        {
            this.posFile--;
        }

        public bool EndFile()
        {
            return (this.posFile == this.sourceList.Count);
        }

        private PropertyInfo FindProperty(string fieldname)
        {
            foreach (PropertyInfo info in this.properties)
            {
                if (info.Name.ToUpper().Equals(fieldname.ToUpper()))
                {
                    return info;
                }
            }
            return null;
        }

        public object GetFieldValue(string fieldName)
        {
            return this.GetFieldValue(fieldName, "");
        }

        public object GetFieldValue(string fieldName, string tableName)
        {
            PropertyInfo info = this.FindProperty(fieldName);
            if (info != null)
            {
                return info.GetValue(this.sourceList[this.posFile], null);
            }
            return null;
        }

        public void OpenDataSouce()
        {
            this.posFile = 0;
        }

        private void PrepareFields()
        {
            object obj2 = this.sourceList[0];
            this.properties = obj2.GetType().GetProperties();
        }

        public string DataSourceName
        {
            get
            {
                return this.sourcename;
            }
        }

        public ReportDataFieldInfo[] Fields
        {
            get
            {
                ReportDataFieldInfo[] array = new ReportDataFieldInfo[this.properties.Length];
                ArrayList list = new ArrayList();
                for (int i = 0; i < this.properties.Length; i++)
                {
                    list.Add(new ReportDataFieldInfo(this.properties[i].Name, this.properties[i].PropertyType));
                }
                list.CopyTo(array);
                return array;
            }
        }
    }
}

