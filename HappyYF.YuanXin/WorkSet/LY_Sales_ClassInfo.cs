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
    public partial class LY_Sales_ClassInfo : Form
    {
        public LY_Sales_ClassInfo()
        {
            InitializeComponent();
        }

        private void ly_salesregionBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.ly_sales_contract_classBindingSource.EndEdit();
            this.ly_sales_contract_classTableAdapter.Update(this.lYSalseMange.ly_sales_contract_class);

        }

        private void LY_Salesregion_Mange_Load(object sender, EventArgs e)
        {
            this.ly_sales_contract_classTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;    
            this.ly_sales_contract_classTableAdapter.Fill(this.lYSalseMange.ly_sales_contract_class);
          
           
            


        }

        private void LY_Salesregion_Mange_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Validate();
            this.ly_sales_contract_classBindingSource.EndEdit();
            this.ly_sales_contract_classTableAdapter.Update(this.lYSalseMange.ly_sales_contract_class);
        }

       
    }
}
