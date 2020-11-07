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
    public partial class LY_SalesBorrowStyle_Info : Form
    {
        public LY_SalesBorrowStyle_Info()
        {
            InitializeComponent();
        }

        private void ly_salesregionBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.ly_sales_borrow_styleBindingSource.EndEdit();
            this.ly_sales_borrow_styleTableAdapter.Update(this.lYSalseMange.ly_sales_borrow_style);

        }

        private void LY_Salesregion_Mange_Load(object sender, EventArgs e)
        {
            
            this.ly_sales_borrow_styleTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            this.ly_sales_borrow_styleTableAdapter.Fill(this.lYSalseMange.ly_sales_borrow_style);
            
          


        }

        private void LY_Salesregion_Mange_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Validate();
            this.ly_sales_borrow_styleBindingSource.EndEdit();
            this.ly_sales_borrow_styleTableAdapter.Update(this.lYSalseMange.ly_sales_borrow_style);
        }

       
    }
}
