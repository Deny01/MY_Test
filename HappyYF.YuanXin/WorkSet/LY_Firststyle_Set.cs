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
    public partial class LY_Firststyle_Set : Form
    {
        public LY_Firststyle_Set()
        {
            InitializeComponent();
        }

        private void ly_materrial_sortBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.ly_firststyle_setBindingSource.EndEdit();
            this.ly_firststyle_setTableAdapter.Update(this.lYMaterialMange.ly_firststyle_set );

        }

        private void LY_MaterialSort_Set_Load(object sender, EventArgs e)
        {
            this.ly_firststyle_setTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;    
            this.ly_firststyle_setTableAdapter.Fill(this.lYMaterialMange.ly_firststyle_set);
         
           

        }
    }
}
