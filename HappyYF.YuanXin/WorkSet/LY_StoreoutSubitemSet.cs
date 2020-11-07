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
    public partial class LY_StoreoutSubitemSet : Form
    {
        public LY_StoreoutSubitemSet()
        {
            InitializeComponent();
        }

        private void ly_store_out_stylesetBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.ly_store_out_substylesetBindingSource.EndEdit();
            this.ly_store_out_substylesetTableAdapter.Update(this.lYStoreMange.ly_store_out_substyleset);

        }

        private void LY_StoreoutSet_Load(object sender, EventArgs e)
        {
            // TODO: 这行代码将数据加载到表“lYStoreMange.ly_store_out_substyleset”中。您可以根据需要移动或移除它。
           
            this.ly_store_out_substylesetTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            this.ly_store_out_substylesetTableAdapter.Fill(this.lYStoreMange.ly_store_out_substyleset);
        }
    }
}
