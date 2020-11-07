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
    public partial class LY_Manufacturing_procedure_Manage : Form
    {
        public LY_Manufacturing_procedure_Manage()
        {
            InitializeComponent();
        }

        private void ly_manufacturing_procedureBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.ly_manufacturing_procedureBindingSource.EndEdit();
            this.ly_manufacturing_procedureTableAdapter.Update (this.lYMaterielRequirements.ly_manufacturing_procedure);

        }

        private void LY_Manufacturing_procedure_Manage_Load(object sender, EventArgs e)
        {
            this.ly_manufacturing_procedureTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
           
            this.ly_manufacturing_procedureTableAdapter.Fill(this.lYMaterielRequirements.ly_manufacturing_procedure);

        }
    }
}
