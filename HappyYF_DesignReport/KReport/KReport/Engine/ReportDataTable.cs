namespace KReport.Engine
{
    using System;
    using System.Collections;
    using System.Data;

    public class ReportDataTable : IReportData
    {
        private bool endfile;
        private ArrayList fields;
        private int posFile;
        private DataTable table;

        public ReportDataTable()
        {
            this.table = new DataTable();
            this.OpenDataSouce();
            this.endfile = true;
            this.PrepareFields();
        }

        public ReportDataTable(DataTable table, string sourceName)
        {
            if (string.IsNullOrEmpty(sourceName))
            {
                sourceName = table.TableName;
            }
            this.table = table;
            this.table.TableName = sourceName;
            this.OpenDataSouce();
            this.endfile = false;
            this.PrepareFields();
        }

        public void DataSourceFirstRecord()
        {
            this.posFile = 0;
        }

        public void DataSourceNextRecord()
        {
            this.posFile++;
            if (this.posFile == this.table.Rows.Count)
            {
                this.endfile = true;
                this.posFile--;
            }
        }

        public void DataSourcePriorRecord()
        {
            this.posFile--;
        }

        public bool EndFile()
        {
            return this.endfile;
        }

        public object GetFieldValue(string fieldName)
        {
            if (this.table.Columns.Contains(fieldName))
            {
                return this.table.Rows[this.posFile][fieldName];
            }
            return null;
        }

        public object GetFieldValue(string fieldName, string tableName)
        {
            if (!tableName.Trim().Equals(string.Empty))
            {
                if (this.table.Columns.Contains(fieldName))
                {
                    return this.table.Rows[this.posFile][fieldName];
                }
                return null;
            }
            return null;
        }

        public void OpenDataSouce()
        {
            this.posFile = 0;
            this.endfile = this.posFile == this.table.Rows.Count;
        }

        private void PrepareFields()
        {
            this.fields = new ArrayList();
            foreach (DataColumn column in this.table.Columns)
            {
                this.fields.Add(new ReportDataFieldInfo(column.ColumnName, column.DataType));
            }
        }

        public string DataSourceName
        {
            get
            {
                return this.table.TableName;
            }
        }

        public ReportDataFieldInfo[] Fields
        {
            get
            {
                ReportDataFieldInfo[] array = new ReportDataFieldInfo[this.fields.Count];
                this.fields.CopyTo(array);
                return array;
            }
        }
    }
}

