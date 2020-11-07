namespace KReport.Engine
{
    using System;

    public abstract class ReportData : IReportData
    {
        protected ReportData()
        {
        }

        public virtual void DataSourceFirstRecord()
        {
        }

        public virtual void DataSourceNextRecord()
        {
        }

        public virtual void DataSourcePriorRecord()
        {
        }

        public virtual bool EndFile()
        {
            return true;
        }

        public virtual object GetFieldValue(string fieldName)
        {
            return null;
        }

        public virtual object GetFieldValue(string fieldName, string tableName)
        {
            return null;
        }

        public virtual void OpenDataSouce()
        {
        }

        public virtual string DataSourceName
        {
            get
            {
                return string.Empty;
            }
        }

        public virtual ReportDataFieldInfo[] Fields
        {
            get
            {
                return null;
            }
        }
    }
}

