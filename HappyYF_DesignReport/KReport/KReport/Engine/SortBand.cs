namespace KReport.Engine
{
    using System;
    using System.Collections;

    internal class SortBand : IComparer
    {
        public int Compare(object x, object y)
        {
            int num = ((BandBase) x).BandType.CompareTo(((BandBase) y).BandType);
            if (num == 0)
            {
                if (((BandBase) x).BandType == BandType.BandGroupHeader)
                {
                    if (((BandBase) x).Index == ((BandBase) y).Index)
                    {
                        return 0;
                    }
                    if (((BandBase) x).Index < ((BandBase) y).Index)
                    {
                        return -11;
                    }
                    return 1;
                }
                if (((BandBase) x).BandType == BandType.BandGroupFooder)
                {
                    if (((BandBase) x).Index == ((BandBase) y).Index)
                    {
                        return 0;
                    }
                    if (((BandBase) x).Index < ((BandBase) y).Index)
                    {
                        return 1;
                    }
                    return -1;
                }
            }
            return num;
        }
    }
}

