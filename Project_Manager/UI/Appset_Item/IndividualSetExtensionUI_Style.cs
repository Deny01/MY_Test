using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Project_Manager.AppServices;

namespace Project_Manager.UI
{
    public partial class IndividualSetExtensionUI_Style : UserControl
    {
        public IndividualSetExtensionUI_Style()
        {
            InitializeComponent();

            Text = "界面风格";
        }

        public void Save()
        {
            AppSet appSet = AppSet.Load();

            appSet.AppearanceStyle = this.comboBox1.Text;

            appSet.Save();
        }

        private void IndividualSetExtensionUI_Style_Load(object sender, EventArgs e)
        {
            AppSet appSet = AppSet.Load();

            this.comboBox1 .Text= appSet.AppearanceStyle;
        }
    }
}
