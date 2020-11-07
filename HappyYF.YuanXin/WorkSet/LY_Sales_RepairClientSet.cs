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
    public partial class LY_Sales_RepairClientSet : Form
    {
        public LY_Sales_RepairClientSet()
        {
            InitializeComponent();
        }

        private void ly_sales_repair_sectorsBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.ly_sales_client_RepairBindingSource.EndEdit();
            this.ly_sales_client_RepairTableAdapter.Update(this.lYSalseRepair.ly_sales_client_Repair);
           
        }

        private void LY_Sales_RepairSet_Load(object sender, EventArgs e)
        {
            this.ly_sales_client_RepairTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            this.ly_sales_client_RepairTableAdapter.Fill(this.lYSalseRepair.ly_sales_client_Repair);
           

        }

        private void ly_sales_client_RepairDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
