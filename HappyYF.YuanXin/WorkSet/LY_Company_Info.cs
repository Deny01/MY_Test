using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HappyYF.Infrastructure.Repositories;

namespace HappyYF.YuanXin.WorkSet
{
    public partial class LY_Company_Info : Form
    {
        public LY_Company_Info()
        {
            InitializeComponent();
        }

        private void ly_salesregionBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.ly_company_informationBindingSource.EndEdit();
            this.ly_company_informationTableAdapter.Update(this.lYSalseMange.ly_company_information);

        }

        private void LY_Salesregion_Mange_Load(object sender, EventArgs e)
        {
            this.ly_company_informationTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;    
            this.ly_company_informationTableAdapter.Fill(this.lYSalseMange.ly_company_information);
            


        }

        private void LY_Salesregion_Mange_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Validate();
            this.ly_company_informationBindingSource.EndEdit();
            this.ly_company_informationTableAdapter.Update(this.lYSalseMange.ly_company_information);
        }

       
    }
}
