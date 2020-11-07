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
    public partial class IndividualSetExten_KeyboardInputIndex : UserControl
    {
        public IndividualSetExten_KeyboardInputIndex()
        {
            InitializeComponent();

            Text = "中文输入法";

            InputLanguageCollection ilc = InputLanguage.InstalledInputLanguages;
            foreach (InputLanguage il in ilc)
            {
                comboBox1.Items.Add(il.LayoutName);
            }

            AppSet appSet = AppSet.Load();

            this.comboBox1.SelectedIndex = appSet.KeyboardInputIndex;
        }

        public void Save()
        {
            AppSet appSet = AppSet.Load();

            appSet.KeyboardInputIndex = this.comboBox1.SelectedIndex;

            appSet.Save();
        }

        private void IndividualSetExten_KeyboardInputIndex_Load(object sender, EventArgs e)
        {

            //InputLanguageCollection ilc = InputLanguage.InstalledInputLanguages;
            //foreach (InputLanguage il in ilc)
            //{
            //    comboBox1.Items.Add(il.LayoutName);
            //}
            
            //AppSet appSet = AppSet.Load();

            //this.comboBox1.SelectedIndex  = appSet.KeyboardInputIndex;
        }
    }
}
