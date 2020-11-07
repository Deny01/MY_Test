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
    public partial class LY_Drawing_levelSet : Form
    {
        public LY_Drawing_levelSet()
        {
            InitializeComponent();
        }

        private void LY_Drawing_levelSet_Load(object sender, EventArgs e)
        {
            this.ly_drawing_levelSetTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            this.ly_drawing_levelSetTableAdapter.Fill(this.lYMaterialMange.ly_drawing_levelSet);
           
           
        }

        private void ly_drawing_levelBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.ly_drawing_levelSetBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.lYMaterialMange);

        }

     

     
    }
}
