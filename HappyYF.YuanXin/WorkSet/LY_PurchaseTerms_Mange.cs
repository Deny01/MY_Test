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
    public partial class LY_PurchaseTerms_Mange : Form
    {
        public LY_PurchaseTerms_Mange()
        {
            InitializeComponent();
        }

       

        private void LY_Salesperson_Mange_Load(object sender, EventArgs e)
        {




            this.ly_sales_contract_terms_purchaseTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            this.ly_sales_contract_terms_purchaseTableAdapter.Fill(this.lYMaterielRequirements.ly_sales_contract_terms_purchase);
            
        }

        private void LY_Salesperson_Mange_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Validate();
            this.ly_sales_contract_terms_purchaseBindingSource.EndEdit();
            this.ly_sales_contract_terms_purchaseTableAdapter.Update(this.lYMaterielRequirements.ly_sales_contract_terms_purchase);
        }

       

        private void ly_sales_contract_termsBindingNavigatorSaveItem_Click_1(object sender, EventArgs e)
        {
            this.Validate();
            this.ly_sales_contract_terms_purchaseBindingSource.EndEdit();
            this.ly_sales_contract_terms_purchaseTableAdapter.Update(this.lYMaterielRequirements.ly_sales_contract_terms_purchase);

        }

        private void ly_sales_contract_termsDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

      
    }
}
