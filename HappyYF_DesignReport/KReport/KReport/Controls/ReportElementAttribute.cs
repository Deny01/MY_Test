using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KReport.Controls
{
    class ReportElementAttribute : Attribute
    {
        string names;
        public ReportElementAttribute(string names)
        {
            this.Names = names;
        }
        public string Names
        {
            get { return names; }
            set { names = value; }
        }

    }
}
