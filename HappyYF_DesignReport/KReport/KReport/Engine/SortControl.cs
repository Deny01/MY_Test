namespace KReport.Engine
{
    using System;
    using System.Collections;

    internal class SortControl : IComparer
    {
        public int Compare(object x, object y)
        {
            return ((CustomControl) x).Index.CompareTo(((CustomControl) y).Index);
        }
    }
}

