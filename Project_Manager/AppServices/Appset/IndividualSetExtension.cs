using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Project_Manager.AppServices
{
    class IndividualSetExtension : IExtension
    {
        public string Name
        {
            get { return "个人"; }
        }

        public IUIExtension UIExtension
        {
            get { return new IndividualSetExtensionUI(); }
        }
    }
}
