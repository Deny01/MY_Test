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
    public partial class LY_MaterialStatusSet : Form
    {
        public LY_MaterialStatusSet()
        {
            InitializeComponent();
        }

        private void ly_materialstatusBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.ly_materialstatusBindingSource.EndEdit();
            this.ly_materialstatusTableAdapter.Update(this.lYMaterialMange.ly_materialstatus);

        }

        private void LY_MaterialStatusSet_Load(object sender, EventArgs e)
        {
            this.ly_materialstatusTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            this.ly_materialstatusTableAdapter.Fill(this.lYMaterialMange.ly_materialstatus);

        }
    }
}
