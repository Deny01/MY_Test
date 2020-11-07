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
    public partial class LY_MaterialGetmethod_Set : Form
    {
        public LY_MaterialGetmethod_Set()
        {
            InitializeComponent();
        }

      

        private void LY_MaterialSort_Set_Load(object sender, EventArgs e)
        {
            

            this.ly_material_getmethodTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            this.ly_material_getmethodTableAdapter.Fill(this.lYMaterialMange.ly_material_getmethod);
        }

        private void ly_material_getmethodBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.ly_material_getmethodBindingSource.EndEdit();
            this.ly_material_getmethodTableAdapter.Update(this.lYMaterialMange.ly_material_getmethod);
        }
    }
}
