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
    public partial class LY_Marposs_StyleSet : Form
    {
        public LY_Marposs_StyleSet()
        {
            InitializeComponent();
        }

        private void ly_sales_repair_sectorsBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.ly_material_marposs_styleBindingSource.EndEdit();
            this.ly_material_marposs_styleTableAdapter.Update(this.lYSalseRepair.ly_material_marposs_style);

        }

        private void LY_Sales_RepairSet_Load(object sender, EventArgs e)
        {
            // TODO: 这行代码将数据加载到表“lYSalseRepair.ly_material_marposs_style”中。您可以根据需要移动或移除它。

            this.ly_material_marposs_styleTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            this.ly_material_marposs_styleTableAdapter.Fill(this.lYSalseRepair.ly_material_marposs_style);

        }
    }
}
