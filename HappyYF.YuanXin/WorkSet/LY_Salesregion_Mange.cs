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
    public partial class LY_Salesregion_Mange : Form
    {
        public LY_Salesregion_Mange()
        {
            InitializeComponent();
        }

        private void ly_salesregionBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.ly_salesregionBindingSource.EndEdit();
            this.ly_salesregionTableAdapter.Update(this.lYSalseMange.ly_salesregion);

        }

        private void LY_Salesregion_Mange_Load(object sender, EventArgs e)
        {
            this.ly_salespersonTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;      
            this.ly_salespersonTableAdapter.Fill(this.lYSalseMange.ly_salesperson);

            this.ly_salesregionTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;     
            this.ly_salesregionTableAdapter.Fill(this.lYSalseMange.ly_salesregion);

        }

        private void LY_Salesregion_Mange_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Validate();
            this.ly_salesregionBindingSource.EndEdit();
            this.ly_salesregionTableAdapter.Update(this.lYSalseMange.ly_salesregion);

        }
    }
}
