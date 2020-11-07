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
    public partial class LY_MaterialSort_Set : Form
    {
        public LY_MaterialSort_Set()
        {
            InitializeComponent();
        }

        private void ly_materrial_sortBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.ly_materrial_sortBindingSource.EndEdit();
            this.ly_materrial_sortTableAdapter.Update(this.lYMaterialMange.ly_materrial_sort);

        }

        private void LY_MaterialSort_Set_Load(object sender, EventArgs e)
        {
            this.ly_materrial_sortTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;    
            this.ly_materrial_sortTableAdapter.Fill(this.lYMaterialMange.ly_materrial_sort);

        }
    }
}
