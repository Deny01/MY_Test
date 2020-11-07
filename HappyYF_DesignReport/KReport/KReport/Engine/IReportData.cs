namespace KReport.Engine
{
    using System;

    public interface IReportData
    {
        void DataSourceFirstRecord();
        void DataSourceNextRecord();
        void DataSourcePriorRecord();
        bool EndFile();
        object GetFieldValue(string fieldName);
        object GetFieldValue(string fieldName, string tableName);
        void OpenDataSouce();

        string DataSourceName { get; }

        ReportDataFieldInfo[] Fields { get; }
    }
}

