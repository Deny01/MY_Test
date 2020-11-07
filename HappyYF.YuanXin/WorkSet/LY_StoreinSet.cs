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
    public partial class LY_StoreinSet : Form
    {
        public LY_StoreinSet()
        {
            InitializeComponent();
        }

        private void ly_store_in_stylesetBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.ly_store_in_stylesetBindingSource.EndEdit();
            this.ly_store_in_stylesetTableAdapter.Update(this.lYStoreMange.ly_store_in_styleset );

        }

        private void LY_StoreinSet_Load(object sender, EventArgs e)
        {
            this.ly_store_in_stylesetTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            this.ly_store_in_stylesetTableAdapter.Fill(this.lYStoreMange.ly_store_in_styleset);

        }
    }
}
