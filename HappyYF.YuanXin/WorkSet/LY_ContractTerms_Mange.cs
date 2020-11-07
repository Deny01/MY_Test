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
    public partial class LY_ContractTerms_Mange : Form
    {
        public LY_ContractTerms_Mange()
        {
            InitializeComponent();
        }

       

        private void LY_Salesperson_Mange_Load(object sender, EventArgs e)
        {
            this.ly_sales_contract_termsTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;    
            this.ly_sales_contract_termsTableAdapter.Fill(this.lYSalseMange.ly_sales_contract_terms);
            
        }

        private void LY_Salesperson_Mange_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Validate();
            this.ly_sales_contract_termsBindingSource.EndEdit();
            this.ly_sales_contract_termsTableAdapter.Update(this.lYSalseMange.ly_sales_contract_terms);
        }

       

        private void ly_sales_contract_termsBindingNavigatorSaveItem_Click_1(object sender, EventArgs e)
        {
            this.Validate();
            this.ly_sales_contract_termsBindingSource.EndEdit();
            this.ly_sales_contract_termsTableAdapter.Update(this.lYSalseMange.ly_sales_contract_terms);

        }
    }
}
