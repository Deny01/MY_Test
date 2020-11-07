using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Data.SqlClient;

using System.Transactions;
using HappyYF.Infrastructure.Repositories;
using DataGridFilter;
using System.Threading;

namespace HappyYF.YuanXin.WorkSet
{
    public partial class LY_Newitems_LLL : Form
    {
        public LY_Newitems_LLL()
        {
            InitializeComponent();
        }

        private void ly_new_LLLBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.ly_new_LLLBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.lYFinancialMange);

        }

        private void LY_Newitems_LLL_Load(object sender, EventArgs e)
        {

            this.ly_new_detail_LLLTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            this.ly_new_LLLTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;

            this.ly_new_LLLTableAdapter.Fill(this.lYFinancialMange.ly_new_LLL);

        }

        private void CountNewItemLLL()
        {
            //if (null == this.ly_material_plan_mainDataGridView.CurrentRow) return;

            //int parentId = int.Parse(this.ly_material_plan_mainDataGridView.CurrentRow.Cells["id"].Value.ToString());
            //string planNum = this.ly_material_plan_mainDataGridView.CurrentRow.Cells["计划编号"].Value.ToString();

            NewFrm.Show(this);
            Thread.Sleep(500);

            //2018 -04-03

            SqlConnection sqlConnection1 = new SqlConnection(SQLDatabase.Connectstring);
            SqlCommand cmd = new SqlCommand();



            //cmd.Parameters.Add("@planId", SqlDbType.Int);
            //cmd.Parameters["@planId"].Value = parentId;


            cmd.CommandText = "LY_NewitemsLLL";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = sqlConnection1;
            cmd.CommandTimeout = 0;

            sqlConnection1.Open();
            cmd.ExecuteNonQuery();
            sqlConnection1.Close();


            //ly_material_plan_explodeTableAdapter.Fill(this.lYPlanMange.ly_material_plan_explode, parentId);
            //this.ly_store_planitemcountTableAdapter.Fill(this.lYPlanMange.ly_store_planitemcount, parentId);
            //this.ly_plan_getmaterial_departmentTableAdapter.Fill(this.lYPlanMange.ly_plan_getmaterial_department , planNum);

            NewFrm.Hide(this);
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {


            CountNewItemLLL();
            this.ly_new_LLLTableAdapter.Fill(this.lYFinancialMange.ly_new_LLL);
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            ExportDataGridviewTOExcell.ExportDataGridview(this.ly_new_LLLDataGridView, true);
        }

        //private void fillToolStripButton_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        this.ly_new_detail_LLLTableAdapter.Fill(this.lYFinancialMange.ly_new_detail_LLL, itemno_mainToolStripTextBox.Text);
        //    }
        //    catch (System.Exception ex)
        //    {
        //        System.Windows.Forms.MessageBox.Show(ex.Message);
        //    }

        //}

        private void ly_new_LLLDataGridView_SelectionChanged(object sender, EventArgs e)
        {
            if (null == ly_new_LLLDataGridView.CurrentRow)
            {
                this.ly_new_detail_LLLTableAdapter.Fill(this.lYFinancialMange.ly_new_detail_LLL, "asddsa");
                return;
            }

          
           string nowitemCode = ly_new_LLLDataGridView.CurrentRow.Cells["itemno"].Value.ToString();

            this.ly_new_detail_LLLTableAdapter.Fill(this.lYFinancialMange.ly_new_detail_LLL, nowitemCode);


        }

        private void toolStripTextBox5_KeyUp(object sender, KeyEventArgs e)
        {
            string filterString;


            filterString = GetDataGridviewMultiFilterString.DGVMultiFilterString(this.ly_new_LLLDataGridView, this.toolStripTextBox5.Text);


            this.ly_new_LLLBindingSource.Filter = filterString;
        }

        private void toolStripTextBox5_Enter(object sender, EventArgs e)
        {
            toolStripTextBox5.Text = "";

            this.ly_new_LLLBindingSource.Filter = "";
        }
    }
}
