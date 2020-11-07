namespace KReport.Engine
{
    using System;
    using System.Collections;
    using System.Reflection;

    public class BandCollection : IList, ICollection, IEnumerable
    {
        private ArrayList bandlist = new ArrayList();

        public int Add(object value)
        {
            return this.bandlist.Add(value);
        }

        public void AddRange(BandBase[] bands)
        {
            this.bandlist.AddRange(bands);
        }

        public void Clear()
        {
            this.bandlist.Clear();
        }

        public bool Contains(object value)
        {
            return this.bandlist.Contains(value);
        }

        public void CopyTo(Array array, int index)
        {
            this.bandlist.CopyTo(array, index);
        }

        public BandBase GetBandByType(BandType bandtype)
        {
            return null;
        }

        public IEnumerator GetEnumerator()
        {
            return this.bandlist.GetEnumerator();
        }

        public int IndexOf(object value)
        {
            return this.bandlist.IndexOf(value);
        }

        public void Insert(int index, object value)
        {
            this.bandlist.Insert(index, value);
        }

        public void Remove(object value)
        {
            this.bandlist.Remove(value);
        }

        public void RemoveAt(int index)
        {
            this.bandlist.RemoveAt(index);
        }

        public void Sort()
        {
            this.bandlist.Sort(new SortBand());
        }

        public int Count
        {
            get
            {
                return this.bandlist.Count;
            }
        }

        public bool IsFixedSize
        {
            get
            {
                return this.bandlist.IsFixedSize;
            }
        }

        public bool IsReadOnly
        {
            get
            {
                return this.bandlist.IsReadOnly;
            }
        }

        public bool IsSynchronized
        {
            get
            {
                return this.bandlist.IsSynchronized;
            }
        }

        public object this[int index]
        {
            get
            {
                return this.bandlist[index];
            }
            set
            {
                this.bandlist[index] = value;
            }
        }

        public object SyncRoot
        {
            get
            {
                return this.bandlist.SyncRoot;
            }
        }
    }
}

