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
    public partial class LY_SalesMating_Mange : Form
    {
        public LY_SalesMating_Mange()
        {
            InitializeComponent();
        }

        private void ly_sales_mating_setBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.ly_sales_mating_setBindingSource.EndEdit();
            this.ly_sales_mating_setTableAdapter.Update(this.lYSalseMange2.ly_sales_mating_set);

        }

        private void LY_SalesMating_Mange_Load(object sender, EventArgs e)
        {
            this.ly_sales_mating_setTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            this.ly_sales_mating_setTableAdapter.Fill(this.lYSalseMange2.ly_sales_mating_set);

        }

        private void LY_SalesMating_Mange_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Validate();
            this.ly_sales_mating_setBindingSource.EndEdit();
            this.ly_sales_mating_setTableAdapter.Update(this.lYSalseMange2.ly_sales_mating_set);

        }
    }
}
