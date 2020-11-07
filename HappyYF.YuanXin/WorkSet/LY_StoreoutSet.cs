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
    public partial class LY_StoreoutSet : Form
    {
        public LY_StoreoutSet()
        {
            InitializeComponent();
        }

        private void ly_store_out_stylesetBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.ly_store_out_stylesetBindingSource.EndEdit();
            this.ly_store_out_stylesetTableAdapter.Update(this.lYStoreMange.ly_store_out_styleset);

        }

        private void LY_StoreoutSet_Load(object sender, EventArgs e)
        {
            this.ly_store_out_stylesetTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            this.ly_store_out_stylesetTableAdapter.Fill(this.lYStoreMange.ly_store_out_styleset);

        }
    }
}
