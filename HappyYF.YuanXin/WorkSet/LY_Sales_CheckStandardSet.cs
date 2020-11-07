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
    public partial class LY_Sales_CheckStandardSet : Form
    {

        public  int  nowparentId;
        
        public LY_Sales_CheckStandardSet()
        {
            InitializeComponent();
        }

        private void ly_sales_matingBomBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.ly_sales_matingBomBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.lYSalseMange2);

        }

        private void LY_Sales_CheckStandardSet_Load(object sender, EventArgs e)
        {
            this.ly_sales_matingBomTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            this.ly_sales_matingBomTableAdapter.Fill(this.lYSalseMange2.ly_sales_matingBom, nowparentId);
        }

        
    }
}
