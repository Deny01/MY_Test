using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HappyYF.Infrastructure.Repositories;
using System.Transactions;
using DataGridFilter;


namespace HappyYF.YuanXin.WorkSet
{
    public partial class t_fentan : Form
    {
        List<string> itemlist = new List<string>();

        public t_fentan()
        {
            InitializeComponent();
        }
 

        private void LY_MaterialBom_Load(object sender, EventArgs e)
        {
            // TODO: 这行代码将数据加载到表“lYMaterialMange.t_fentan”中。您可以根据需要移动或删除它。
            this.t_fentanTableAdapter.Fill(this.lYMaterialMange.t_fentan);
            // TODO: 这行代码将数据加载到表“lYMaterialMange.ly_sales_client”中。您可以根据需要移动或删除它。

            this.t_fentanTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            // TODO: 这行代码将数据加载到表“lYMaterialMange.t_financial_type”中。您可以根据需要移动或删除它。
            this.t_fentanTableAdapter.Fill(this.lYMaterialMange.t_fentan);



        }

        private void toolStripTextBox1_Enter(object sender, EventArgs e)
        {
            toolStripTextBox1.Text = "";

            this.tfentanBindingSource.Filter = "";

      
        }
        
        private void toolStripTextBox1_KeyUp(object sender, KeyEventArgs e)
        {
            string filterString;


            filterString = GetDataGridviewMultiFilterString.DGVMultiFilterString(this.ly_inma0010DataGridView, this.toolStripTextBox1.Text);
            this.tfentanBindingSource.Filter = "(" + filterString + ")";



        }

       
 

       

      
 

       
        private void toolStripButton16_Click(object sender, EventArgs e)
        {
            ExportDataGridviewTOExcellA.ExportDataGridview(this.ly_inma0010DataGridView, true);
        }

        private void ly_inma0010DataGridView_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            DataGridView dgv = sender as DataGridView;
            if (!SQLDatabase.CheckHaveRight(SQLDatabase.NowUserID, "财务分摊锁定"))
            {
                MessageBox.Show("无权限！"); return;
            }
            if ("lockFlag" == dgv.CurrentCell.OwningColumn.Name)
            {


                if ("True" == dgv.CurrentRow.Cells["lockFlag"].Value.ToString())
                {
                    dgv.CurrentRow.Cells["lockFlag"].Value = "False";
                    dgv.CurrentRow.Cells["lockpeo"].Value = DBNull.Value;
                    dgv.CurrentRow.Cells["locktime"].Value = DBNull.Value;

                }
                else
                {

                    dgv.CurrentRow.Cells["lockFlag"].Value = "True";
                    dgv.CurrentRow.Cells["lockpeo"].Value = SQLDatabase.nowUserName();
                    dgv.CurrentRow.Cells["locktime"].Value = DateTime.Now;
                }



                this.ly_inma0010DataGridView.EndEdit();

                this.Validate();
                this.tfentanBindingSource.EndEdit();

                this.t_fentanTableAdapter.Update(this.lYMaterialMange.t_fentan);
                return;

            }
        }
    }
}
