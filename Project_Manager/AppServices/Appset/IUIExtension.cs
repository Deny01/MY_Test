using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace Project_Manager.AppServices
{
    public interface IUIExtension
    {
        Control[] CreateSettingsView();

        void PersistSettings(Control[] settingsView);
    }
}
