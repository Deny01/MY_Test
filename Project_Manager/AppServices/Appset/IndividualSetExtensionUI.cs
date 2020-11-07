using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Project_Manager.UI;

namespace Project_Manager.AppServices
{
    class IndividualSetExtensionUI : IUIExtension
    {
        #region IUIExtension Members

        public System.Windows.Forms.Control[] CreateSettingsView()
        {
            //return new Control[] { new IndividualSetExtensionUI_Style(),new IndividualSetExten_KeyboardInputIndex() };
            return new Control[] {  new IndividualSetExten_KeyboardInputIndex() };
        }

        public void PersistSettings(System.Windows.Forms.Control[] settingsView)
        {
            //IndividualSetExtensionUI_Style individualSetExtensionUI_Style = (IndividualSetExtensionUI_Style)settingsView[0];
            //individualSetExtensionUI_Style.Save();

            IndividualSetExten_KeyboardInputIndex individualSetExten_KeyboardInputIndex = (IndividualSetExten_KeyboardInputIndex)settingsView[0];
            individualSetExten_KeyboardInputIndex.Save();


        }

        #endregion

    }
}
