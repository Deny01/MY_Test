namespace KReport.Controls
{
    using KReport.Engine;
    using System;

    public interface IRDBControl
    {
        //string DataSource { get; set; }
        string 数据源 { get; set; }

        IReportData DataSourceLink { get; set; }

        //string FieldName { get; set; }
        string 数据字段 { get; set; }
    }
}

