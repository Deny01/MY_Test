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
    public partial class LY_MaterialDept : Form
    {
        public LY_MaterialDept()
        {
            InitializeComponent();
        }

        private void ly_prod_deptBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.ly_prod_deptBindingSource.EndEdit();
            this.ly_prod_deptTableAdapter.Update(this.lYMaterialMange.ly_prod_dept);

        }

        private void LY_MaterialDept_Load(object sender, EventArgs e)
        {
            this.ly_prod_deptTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            this.ly_prod_deptTableAdapter.Fill(this.lYMaterialMange.ly_prod_dept);

        }
    }
}
