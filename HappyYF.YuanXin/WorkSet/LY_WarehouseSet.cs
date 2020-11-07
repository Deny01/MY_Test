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
    public partial class LY_WarehouseSet : Form
    {
        public LY_WarehouseSet()
        {
            InitializeComponent();
        }

        private void ly_warehouseBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.ly_warehouseBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.lYMaterialMange);

        }

        private void LY_WarehouseSet_Load(object sender, EventArgs e)
        {
            this.ly_warehouseTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            this.ly_warehouseTableAdapter.Fill(this.lYMaterialMange.ly_warehouse);

        }
    }
}
