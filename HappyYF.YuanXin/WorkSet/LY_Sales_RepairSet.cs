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
    public partial class LY_Sales_RepairSet : Form
    {
        public LY_Sales_RepairSet()
        {
            InitializeComponent();
        }

        private void ly_sales_repair_sectorsBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.ly_sales_repair_sectorsBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.lYSalseMange2);

        }

        private void LY_Sales_RepairSet_Load(object sender, EventArgs e)
        {
            this.ly_sales_repair_sectorsTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            this.ly_sales_repair_sectorsTableAdapter.Fill(this.lYSalseMange2.ly_sales_repair_sectors);

        }
    }
}
