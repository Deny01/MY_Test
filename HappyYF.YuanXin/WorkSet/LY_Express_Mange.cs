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
    public partial class LY_Express_Mange : Form
    {
        public LY_Express_Mange()
        {
            InitializeComponent();    

        }

       

        private void LY_Salesregion_Mange_Load(object sender, EventArgs e)
        {
            this.ly_express_companyTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;      
            this.ly_express_companyTableAdapter.Fill(this.lYSalseMange.ly_express_company);

           
        }

        private void LY_Salesregion_Mange_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Validate();
            this.ly_express_companyBindingSource.EndEdit();
            this.ly_express_companyTableAdapter.Update(this.lYSalseMange.ly_express_company);

        }

        private void ly_express_companyBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.ly_express_companyBindingSource.EndEdit();
            this.ly_express_companyTableAdapter.Update(this.lYSalseMange.ly_express_company);

        }
    }
}
