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
    public partial class LY_MaterialUnitSet : Form
    {
        public LY_MaterialUnitSet()
        {
            InitializeComponent();
        }

        private void ly_unitsetBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.ly_unitsetBindingSource.EndEdit();
            this.ly_unitsetTableAdapter.Update(this.lYMaterialMange.ly_unitset);

        }

        private void LY_MaterialUnitSet_Load(object sender, EventArgs e)
        {
            this.ly_unitsetTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            this.ly_unitsetTableAdapter.Fill(this.lYMaterialMange.ly_unitset);

        }
    }
}
