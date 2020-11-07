namespace KReport.Engine
{
    using System;
    using System.Collections;
    using System.Reflection;

    public class ReportDataCollection : IList, ICollection, IEnumerable
    {
        private ArrayList dataSourceList = new ArrayList();

        public int Add(object value)
        {
            return this.dataSourceList.Add(value);
        }

        public void AddRange(IReportData[] bands)
        {
            this.dataSourceList.AddRange(bands);
        }

        public void Clear()
        {
            this.dataSourceList.Clear();
        }

        public bool Contains(object value)
        {
            return this.dataSourceList.Contains(value);
        }

        public void CopyTo(Array array, int index)
        {
            this.dataSourceList.CopyTo(array, index);
        }

        public IEnumerator GetEnumerator()
        {
            return this.dataSourceList.GetEnumerator();
        }

        public IReportData GetSource(string sourceName)
        {
            foreach (IReportData data in this.dataSourceList)
            {
                if (data.DataSourceName.Equals(sourceName))
                {
                    return data;
                }
            }
            return null;
        }

        public int IndexOf(object value)
        {
            return this.dataSourceList.IndexOf(value);
        }

        public void Insert(int index, object value)
        {
            this.dataSourceList.Insert(index, value);
        }

        public void Remove(object value)
        {
            this.dataSourceList.Remove(value);
        }

        public void RemoveAt(int index)
        {
            this.dataSourceList.RemoveAt(index);
        }

        public void Sort()
        {
            this.dataSourceList.Sort();
        }

        public int Count
        {
            get
            {
                return this.dataSourceList.Count;
            }
        }

        public bool IsFixedSize
        {
            get
            {
                return this.dataSourceList.IsFixedSize;
            }
        }

        public bool IsReadOnly
        {
            get
            {
                return this.dataSourceList.IsReadOnly;
            }
        }

        public bool IsSynchronized
        {
            get
            {
                return this.dataSourceList.IsSynchronized;
            }
        }

        public object this[int index]
        {
            get
            {
                return this.dataSourceList[index];
            }
            set
            {
                this.dataSourceList[index] = value;
            }
        }

        public object SyncRoot
        {
            get
            {
                return this.dataSourceList.SyncRoot;
            }
        }
    }
}

