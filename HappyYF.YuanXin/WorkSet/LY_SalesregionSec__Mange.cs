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
    public partial class LY_SalesregionSec__Mange : Form
    {
        public LY_SalesregionSec__Mange()
        {
            InitializeComponent();    

        }

       

        private void LY_Salesregion_Mange_Load(object sender, EventArgs e)
        {
            this.ly_salesregion_secondTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;     
            this.ly_salesregion_secondTableAdapter.Fill(this.lYSalseMange.ly_salesregion_second);
            
           

           
        }

        private void LY_Salesregion_Mange_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Validate();
            this.ly_salesregion_secondBindingSource.EndEdit();
            this.ly_salesregion_secondTableAdapter.Update(this.lYSalseMange.ly_salesregion_second);

        }

        private void ly_express_companyBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.ly_salesregion_secondBindingSource.EndEdit();
            this.ly_salesregion_secondTableAdapter.Update(this.lYSalseMange.ly_salesregion_second);

        }
    }
}
