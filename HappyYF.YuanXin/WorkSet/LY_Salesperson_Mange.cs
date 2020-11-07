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
    public partial class LY_Salesperson_Mange : Form
    {
        public LY_Salesperson_Mange()
        {
            InitializeComponent();
        }

        private void ly_salespersonBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.ly_salespersonBindingSource.EndEdit();
            this.ly_salespersonTableAdapter.Update(this.lYSalseMange.ly_salesperson);

        }

        private void LY_Salesperson_Mange_Load(object sender, EventArgs e)
        {
            this.ly_salesdiscountTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;  
            this.ly_salesdiscountTableAdapter.Fill(this.lYSalseMange.ly_salesdiscount);
            this.ly_salesregionTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;      
            this.ly_salesregionTableAdapter.Fill(this.lYSalseMange.ly_salesregion);
            this.ly_salespersonTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;      
            this.ly_salespersonTableAdapter.Fill(this.lYSalseMange.ly_salesperson);

        }

        private void LY_Salesperson_Mange_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Validate();
            this.ly_salespersonBindingSource.EndEdit();
            this.ly_salespersonTableAdapter.Update(this.lYSalseMange.ly_salesperson);
        }
    }
}
