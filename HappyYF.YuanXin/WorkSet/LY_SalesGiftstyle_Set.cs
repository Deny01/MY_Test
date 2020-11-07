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
    public partial class LY_SalesGiftstyle_Set : Form
    {
        public LY_SalesGiftstyle_Set()
        {
            InitializeComponent();
        }

        private void ly_sales_gift_styleBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.ly_sales_gift_styleBindingSource.EndEdit();
            this.ly_sales_gift_styleTableAdapter.Update(this.lYSalseMange2.ly_sales_gift_style);


        }

        private void LY_SalesGiftstyle_Set_Load(object sender, EventArgs e)
        {
            this.ly_sales_gift_styleTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;    
            this.ly_sales_gift_styleTableAdapter.Fill(this.lYSalseMange2.ly_sales_gift_style);

        }
    }
}
