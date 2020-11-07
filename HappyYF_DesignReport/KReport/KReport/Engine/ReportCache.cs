namespace KReport.Engine
{
    using System;
    using System.Collections;
    using System.Drawing;

    public class ReportCache
    {
        private int index = 0;
        private Hashtable pages = new Hashtable();

        public ReportCache()
        {
            this.index = 1;
        }

        public void AddPage(Bitmap page)
        {
            this.pages.Add(this.index, page);
            this.index++;
        }

        public Bitmap Page(int index)
        {
            return (Bitmap) this.pages[index];
        }

        public Hashtable Pages()
        {
            return this.pages;
        }

        public void ResetCache()
        {
            this.pages.Clear();
            this.index = 1;
        }
    }
}

