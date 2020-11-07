using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HappyYF.Infrastructure.Repositories;
using DataGridFilter;

namespace HappyYF.YuanXin.WorkSet
{
    public partial class LY_Salesclient_Sel : Form
    {
        public string sales_Clientcode;
        
        public LY_Salesclient_Sel()
        {
            InitializeComponent();
        }

        private void ly_sales_clientBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.ly_sales_clientBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.lYSalseMange);

        }

        private void LY_Salesclient_Sel_Load(object sender, EventArgs e)
        {
           
            
            this.ly_sales_clientTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            this.ly_sales_clientTableAdapter.Fill(this.lYSalseMange.ly_sales_client);

            string clifilter = "";



            if (SQLDatabase.CheckHaveRight(SQLDatabase.NowUserID, "营业综合信息"))
            {
                clifilter = "in_use=0";
            }
            else
            {
                clifilter = "salesperson_code='" + SQLDatabase.NowUserID + "' and in_use=0"; 
            
            }

            this.ly_sales_clientBindingSource.Filter = clifilter;

        }

        private void ly_sales_clientDataGridView_DoubleClick(object sender, EventArgs e)
        {
            if (null == this.ly_sales_clientDataGridView.CurrentRow)
            {
                return;
           
            }
            if (this.ly_sales_clientDataGridView.CurrentRow.Cells["停用"].Value.ToString() == "True")
            {
                MessageBox.Show("已经停用", "提示！");
                return;
            }

            this.sales_Clientcode = this.ly_sales_clientDataGridView.CurrentRow.Cells["客户编码"].Value.ToString();
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void toolStripTextBox1_KeyUp(object sender, KeyEventArgs e)
        {
            string filterString;
            string clifilter = "";

            if (SQLDatabase.CheckHaveRight(SQLDatabase.NowUserID, "营业综合信息"))
            {
                clifilter = "";
            }
            else
            {
                clifilter = "salesperson_code='" + SQLDatabase.NowUserID + "'";

            }
            if ("" == clifilter)
            {
                filterString = GetDataGridviewMultiFilterString.DGVMultiFilterString(this.ly_sales_clientDataGridView, this.toolStripTextBox1.Text);

            }
            else
            {

                filterString = clifilter + " and (" + GetDataGridviewMultiFilterString.DGVMultiFilterString(this.ly_sales_clientDataGridView, this.toolStripTextBox1.Text) + ")";
            }

            //if (null == filterString)
            //    filterString = this.treeView1.SelectedNode.Tag.ToString();

            this.ly_sales_clientBindingSource.Filter = filterString;
        }

        private void toolStripTextBox1_Enter(object sender, EventArgs e)
        {
            toolStripTextBox1.Text = "";

            string clifilter = "";

            if (SQLDatabase.CheckHaveRight(SQLDatabase.NowUserID, "营业综合信息"))
            {
                clifilter = "";
            }
            else
            {
                clifilter = "salesperson_code='" + SQLDatabase.NowUserID + "'";

            }

            this.ly_sales_clientBindingSource.Filter = clifilter;
        }
    }
}
