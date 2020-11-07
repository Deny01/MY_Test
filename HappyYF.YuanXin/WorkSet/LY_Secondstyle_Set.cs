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
    public partial class LY_Secondstyle_Set : Form
    {
        public string firstyle_Code;
        
        public LY_Secondstyle_Set()
        {
            InitializeComponent();
        }

        private void ly_materrial_sortBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();

            foreach (DataGridViewRow dgr in ly_secondstyle_setDataGridView.Rows)
            {
                dgr.Cells["firststyleCode"].Value  = firstyle_Code;

            }
            this.ly_secondstyle_setBindingSource.EndEdit();
            this.ly_secondstyle_setTableAdapter.Update(this.lYMaterialMange.ly_secondstyle_set);

        }

        private void LY_MaterialSort_Set_Load(object sender, EventArgs e)
        {
            this.ly_secondstyle_setTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;    
            this.ly_secondstyle_setTableAdapter.Fill(this.lYMaterialMange.ly_secondstyle_set);
            this.ly_secondstyle_setBindingSource.Filter = "firststyleCode ='" + firstyle_Code + "'";
            
            

        }

        private void bindingNavigatorAddNewItem_Click(object sender, EventArgs e)
        {
            this.ly_secondstyle_setBindingSource.AddNew();

            ly_secondstyle_setDataGridView.CurrentRow.Cells["firststyleCode"].Value = firstyle_Code;
            this.ly_secondstyle_setBindingSource.EndEdit();
            this.ly_secondstyle_setTableAdapter.Update(this.lYMaterialMange.ly_secondstyle_set);
        }

        
    }
}
