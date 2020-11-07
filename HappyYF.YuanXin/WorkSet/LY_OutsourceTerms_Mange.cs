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
    public partial class LY_OutsourceTerms_Mange : Form
    {
        public LY_OutsourceTerms_Mange()
        {
            InitializeComponent();
        }

       

        private void LY_Salesperson_Mange_Load(object sender, EventArgs e)
        {


            this.ly_sales_contract_terms_outsourceTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            this.ly_sales_contract_terms_outsourceTableAdapter.Fill(this.lYMaterielRequirements.ly_sales_contract_terms_outsource);
            
        }

        private void LY_Salesperson_Mange_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Validate();
            this.ly_sales_contract_terms_outsourceBindingSource.EndEdit();
            this.ly_sales_contract_terms_outsourceTableAdapter.Update(this.lYMaterielRequirements.ly_sales_contract_terms_outsource);
        }

       

        private void ly_sales_contract_termsBindingNavigatorSaveItem_Click_1(object sender, EventArgs e)
        {
            this.Validate();
            this.ly_sales_contract_terms_outsourceBindingSource.EndEdit();
            this.ly_sales_contract_terms_outsourceTableAdapter.Update(this.lYMaterielRequirements.ly_sales_contract_terms_outsource);

        }

        private void ly_sales_contract_termsDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
