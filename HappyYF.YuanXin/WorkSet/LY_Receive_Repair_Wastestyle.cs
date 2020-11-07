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
    public partial class LY_Receive_Repair_Wastestyle : Form
    {
        public LY_Receive_Repair_Wastestyle()
        {
            InitializeComponent();
        }

        private void ly_store_out_stylesetBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.ly_receive_repair_wastestyleBindingSource.EndEdit();
            this.ly_receive_repair_wastestyleTableAdapter.Update(this.lYSalseRepair.ly_receive_repair_wastestyle);

        }

        private void LY_StoreoutSet_Load(object sender, EventArgs e)
        {


            this.ly_receive_repair_wastestyleTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
           
            this.ly_receive_repair_wastestyleTableAdapter.Fill(this.lYSalseRepair.ly_receive_repair_wastestyle);

        }
    }
}
