using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Project_Manager.UI;

namespace Project_Manager.AppServices
{
    class CompanySetExtensionUI : IUIExtension
    {
        #region IUIExtension Members

        public System.Windows.Forms.Control[] CreateSettingsView()
        {
            return new Control[] { new CompanySetExtensionUI_Control() };
        }

        public void PersistSettings(System.Windows.Forms.Control[] settingsView)
        {
            CompanySetExtensionUI_Control companyNameControl = (CompanySetExtensionUI_Control)settingsView[0];
            companyNameControl.SaveComanyName();

           
        }

        #endregion

    }
}
