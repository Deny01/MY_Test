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
    public partial class LY_SalesDiscount_Mange : Form
    {
        public LY_SalesDiscount_Mange()
        {
            InitializeComponent();
        }

        private void ly_salesregionBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.ly_salesdiscountBindingSource.EndEdit();
            this.ly_salesdiscountTableAdapter.Update(this.lYSalseMange.ly_salesdiscount);

        }

        private void LY_Salesregion_Mange_Load(object sender, EventArgs e)
        {
            this.ly_salesdiscountTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;      
            this.ly_salesdiscountTableAdapter.Fill(this.lYSalseMange.ly_salesdiscount);
            
        }

        private void LY_Salesregion_Mange_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Validate();
            this.ly_salesdiscountBindingSource.EndEdit();
            this.ly_salesdiscountTableAdapter.Update(this.lYSalseMange.ly_salesdiscount);

        }
    }
}
