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
    public partial class LY_MaterialCategorySet : Form
    {
        public LY_MaterialCategorySet()
        {
            InitializeComponent();
        }

        private void ly_materialcategoryBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.ly_materialcategoryBindingSource.EndEdit();
            this.ly_materialcategoryTableAdapter.Update(this.lYMaterialMange.ly_materialcategory);

        }

        private void LY_MaterialCategorySet_Load(object sender, EventArgs e)
        {
            this.ly_materialcategoryTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            this.ly_materialcategoryTableAdapter.Fill(this.lYMaterialMange.ly_materialcategory);

        }
    }
}
