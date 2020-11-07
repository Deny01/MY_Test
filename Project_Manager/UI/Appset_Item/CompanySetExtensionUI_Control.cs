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
    public partial class CompanySetExtensionUI_Control : UserControl
    {
        AppSet appSet;
        public CompanySetExtensionUI_Control()
        {
            InitializeComponent();

            Text = "基本参数";
             appSet = AppSet.Load();
            this.companyNametextBox.Text = AppSet.companyName;
            this.companySimpleNametextBox .Text = AppSet.companySimpleName;
            this.countdaycomboBox.Text  = AppSet.countday;
            this.getdayaccrualcomboBox .Text = AppSet.getdayaccrual ;

            this.use_postponed_checkBox.Checked = AppSet.use_postponed;
            this.auto_postponed_checkBox.Checked = AppSet.auto_postponed;

            this.dateTimePicker1.Value = AppSet.countlineDate;
            
        }

        private void CompanySetExtensionUI_Control_Load(object sender, EventArgs e)
        {
            //AppSet appSet = AppSet.Load();
            
            //this.textBox1.Text = appSet .CompanyName;
        }

        public void SaveComanyName()
        {
            AppSet appSet = AppSet.Load();

            AppSet.companyName = this.companyNametextBox.Text;
            AppSet.companySimpleName  = this.companySimpleNametextBox .Text;
            AppSet.countday = this.countdaycomboBox .Text;
            AppSet.getdayaccrual  = this.getdayaccrualcomboBox .Text;

            AppSet.use_postponed = this.use_postponed_checkBox.Checked;
            AppSet.auto_postponed = this.auto_postponed_checkBox.Checked;

            AppSet.countlineDate = this.dateTimePicker1.Value.Date;

            appSet.Save();
        }

        private void textBox1_Enter(object sender, EventArgs e)
        {
            AppSet appSet = AppSet.Load();
            //InputLanguage.CurrentInputLanguage = InputLanguage.InstalledInputLanguages[2];
            if ( 0 < appSet.KeyboardInputIndex)
            InputLanguage.CurrentInputLanguage = InputLanguage.InstalledInputLanguages[appSet.KeyboardInputIndex];
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            InputLanguage.CurrentInputLanguage = InputLanguage.DefaultInputLanguage ;
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

       
       
    }
}
