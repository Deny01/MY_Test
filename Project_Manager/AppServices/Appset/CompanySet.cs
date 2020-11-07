using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Project_Manager.AppServices
{
    class CompanySetExtension : IExtension
    {
        public string Name
        {
            get { return "公司"; }
        }

        public IUIExtension UIExtension
        {
            get { return new CompanySetExtensionUI(); }
        }
    }
}
