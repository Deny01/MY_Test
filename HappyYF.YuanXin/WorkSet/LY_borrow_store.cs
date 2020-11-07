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
using System.Data.SqlClient;
namespace HappyYF.YuanXin.WorkSet
{
    public partial class LY_borrow_store : Form
    {
    

        public LY_borrow_store()
        {
            InitializeComponent();
        }

        private void LY_Machine_Process_Load(object sender, EventArgs e)
        {
            // TODO: 这行代码将数据加载到表“lYMaterielRequirements.ly_borrow_store”中。您可以根据需要移动或删除它。
          

            this.bom_material_for_machine_base_NewTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            this.bom_material_for_machine_base_NewTableAdapter.Fill(this.lYMaterielRequirements.bom_material_for_machine_base_New);


            this.ly_borrow_storeTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;

            this.ly_borrow_storeTableAdapter.Fill(this.lYMaterielRequirements.ly_borrow_store);



        }

        private void toolStripTextBox1_Enter(object sender, EventArgs e)
        {
            toolStripTextBox1.Text = "";

            this.bom_material_for_machine_base_NewBindingSource.Filter = "";//bom_material_sel_replace
        }

        private void toolStripTextBox1_KeyUp(object sender, KeyEventArgs e)
        {
            string filterString = GetDataGridviewMultiFilterString.DGVMultiFilterString(this.bom_material_for_machine_baseDataGridView, this.toolStripTextBox1.Text);



            if (null == filterString)
                filterString = "";

            this.bom_material_for_machine_base_NewBindingSource.Filter = filterString;
        }

        private void ly_inma0010_sortDataGridView_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView dgv = sender as DataGridView;

            if (null == dgv.CurrentRow) return;
 
        }



        private void 删除供应商ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (null == this.ly_borrow_store_Grid.CurrentRow) return;


            if ("True" == ly_borrow_store_Grid.CurrentRow.Cells["borrrow_falg"].Value.ToString())
            {
                MessageBox.Show("已经被借用，无法删除！", "注意");
                return;

            }


            string message1 = "当前记录将被删除，继续吗？";
            string caption1 = "提示...";
            MessageBoxButtons buttons1 = MessageBoxButtons.YesNo;

            DialogResult result;



            result = MessageBox.Show(message1, caption1, buttons1,
            MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

            if (result == DialogResult.Yes)
            {

                this.lyborrowstoreBindingSource.RemoveCurrent();

                SaveChanged();
            }
        }

        private void SaveChanged()
        {

            this.ly_borrow_store_Grid.EndEdit();
            this.lyborrowstoreBindingSource.EndEdit();

            this.ly_borrow_storeTableAdapter.Update(this.lYMaterielRequirements.ly_borrow_store);


            this.ly_borrow_storeTableAdapter.Fill(this.lYMaterielRequirements.ly_borrow_store);

        }

      
      

       

      

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
             
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        { 
        }
 

      

    
 

        private void bom_material_for_machine_baseDataGridView_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (null == this.bom_material_for_machine_baseDataGridView.CurrentRow) return;



            string itemno = this.bom_material_for_machine_baseDataGridView.CurrentRow.Cells["物资编号"].Value.ToString();

            ChangeValue queryForm = new ChangeValue();
            queryForm.Text = "请输入机器号";
            queryForm.ChangeMode = "longstring"; 
            queryForm.NewValue = "";
            queryForm.ShowDialog();
            if (queryForm.NewValue== "")
            {
                MessageBox.Show("机器号为空，不可添加！"); return;
            }
            string sql = "select id from ly_borrow_store where machine_num='"+ queryForm.NewValue + "'";
            DataTable dt = null;
            using (SqlConnection connection = new SqlConnection(SQLDatabase.Connectstring))
            {

                SqlDataAdapter adapter = new SqlDataAdapter(sql, connection);
                DataSet ds = new DataSet();
                adapter.Fill(ds);
                dt = ds.Tables[0];
            }
            if(dt.Rows.Count>0)
            {
                MessageBox.Show("该机器号在库中已经存在，不可添加！"); return;
            }
            this.lyborrowstoreBindingSource.AddNew();

            this.ly_borrow_store_Grid.CurrentRow.Cells["wzbh"].Value = itemno;
            this.ly_borrow_store_Grid.CurrentRow.Cells["machine_num"].Value = queryForm.NewValue;
            SaveChanged();
        }

        

        private void toolStripTextBox2_KeyUp(object sender, KeyEventArgs e)
        {
            string filterString = GetDataGridviewMultiFilterString.DGVMultiFilterString(this.ly_borrow_store_Grid, this.toolStripTextBox2.Text);



            if (null == filterString)
                filterString = "";

            this.lyborrowstoreBindingSource.Filter = filterString;
        }

        private void toolStripTextBox2_Enter_1(object sender, EventArgs e)
        {
            toolStripTextBox2.Text = "";

            this.lyborrowstoreBindingSource.Filter = "";//bom_material_sel_replace
        }
    }
}
