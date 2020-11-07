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
    public partial class t_cilent_name : Form
    {
        List<string> itemlist = new List<string>();

        public t_cilent_name()
        {
            InitializeComponent();
        }
 

        private void LY_MaterialBom_Load(object sender, EventArgs e)
        {
            // TODO: 这行代码将数据加载到表“lYMaterialMange.ly_sales_client”中。您可以根据需要移动或删除它。
   
            this.ly_sales_clientTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            // TODO: 这行代码将数据加载到表“lYMaterialMange.t_financial_type”中。您可以根据需要移动或删除它。
            this.ly_sales_clientTableAdapter.Fill(this.lYMaterialMange.ly_sales_client);



        }

        private void toolStripTextBox1_Enter(object sender, EventArgs e)
        {
            toolStripTextBox1.Text = "";

            this.lysalesclientBindingSource.Filter = "";

      
        }
        
        private void toolStripTextBox1_KeyUp(object sender, KeyEventArgs e)
        {
            string filterString;


            filterString = GetDataGridviewMultiFilterString.DGVMultiFilterString(this.ly_inma0010DataGridView, this.toolStripTextBox1.Text);
            this.lysalesclientBindingSource.Filter = "(" + filterString + ")";



        }

       
 

       

      
 

       
        private void toolStripButton16_Click(object sender, EventArgs e)
        {
            ExportDataGridviewTOExcellA.ExportDataGridview(this.ly_inma0010DataGridView, true);
        }

        private void ly_inma0010DataGridView_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            DataGridView dgv = sender as DataGridView;
            if ("en_name" == dgv.CurrentCell.OwningColumn.Name)
            {

                ChangeValue queryForm = new ChangeValue();

                queryForm.OldValue = dgv.CurrentCell.Value.ToString();
                queryForm.NewValue = "";
                queryForm.ChangeMode = "longstring";
                queryForm.ShowDialog();




                if (!string.IsNullOrEmpty(queryForm.NewValue))
                {
                    dgv.CurrentRow.Cells["en_name"].Value = queryForm.NewValue;
                    this.ly_inma0010DataGridView.EndEdit();

                    this.Validate();
                    this.lysalesclientBindingSource.EndEdit();

                    this.ly_sales_clientTableAdapter.Update(this.lYMaterialMange.ly_sales_client);
                    return;
                }
            }
        }
    }
}
