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
using System.Threading;

using HappyYF.Infrastructure.Repositories;
using DataGridFilter;

namespace HappyYF.YuanXin.WorkSet
{
    public partial class LY_WIP : Form
    {
        public LY_WIP()
        {
            InitializeComponent();
            this.ly_wip_viewTableAdapter.CommandTimeout = 0;
        }

        private void LY_WIP_Load(object sender, EventArgs e)
        {
            ly_wip_plan_viewTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            ly_wip_viewTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            ly_wip_detail_viewTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;





            this.dateTimePicker7.Text = SQLDatabase.GetNowdate().Date.Year.ToString() + "-01" + "-01";
            this.dateTimePicker8.Text = SQLDatabase.GetNowdate().AddDays(0).Date.ToString();
            this.ly_wip_viewTableAdapter.Fill(this.lYStoreMange.ly_wip_view, dateTimePicker7.Value, dateTimePicker8.Value.AddDays (1));
      
        }

        private void toolStripButton31_Click(object sender, EventArgs e)
        {
            NewFrm.Show(this );
            Thread.Sleep(500);
            this.ly_wip_viewTableAdapter.Fill(this.lYStoreMange.ly_wip_view, dateTimePicker7.Value, dateTimePicker8.Value.AddDays(1));
            NewFrm.Hide(this);
        }

        private void ly_wip_viewDataGridView_SelectionChanged(object sender, EventArgs e)
        {
            if (ly_wip_viewDataGridView.CurrentRow == null)
                return;
            DateTime begindate;

            if (dateTimePicker7.Value >= DateTime.Parse("2018-01-01"))
            {

                begindate = dateTimePicker7.Value;
            }
            else
            {

                begindate = DateTime.Parse("2018-01-01");
            }

            this.ly_wip_plan_viewTableAdapter.Fill(this.lYStoreMange.ly_wip_plan_view, begindate, dateTimePicker8.Value.AddDays(1), ly_wip_viewDataGridView.CurrentRow.Cells["query_type_main"].Value.ToString());
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (ly_wip_plan_viewDataGridView.CurrentRow == null)
                return;
            //String sqlId = "";
            //if (dataGridView1.CurrentRow.Cells["query_type"].Value.ToString() == "Assembly")
            //{
            //    sqlId = "select * from ly_wip_Product_view where material_plan_num='" + dataGridView1.CurrentRow.Cells["material_plan_num_det"].Value.ToString() + "' ";
            //}
            //else if (dataGridView1.CurrentRow.Cells["query_type"].Value.ToString() == "WorkShop")
            //{
            //    sqlId = "select * from ly_wip_machine_view where material_plan_num='" + dataGridView1.CurrentRow.Cells["material_plan_num_det"].Value.ToString() + "' ";

            //}
            //else
            //{
            //    sqlId = "select * from ly_wip_restructruing_view where material_plan_num='" + dataGridView1.CurrentRow.Cells["material_plan_num_det"].Value.ToString() + "' ";

            //}
            //String strConn = SQLDatabase.Connectstring;
            //SqlConnection conn = new SqlConnection(strConn);

            //conn.Open();
            //SqlCommand cmd = new SqlCommand(sqlId, conn);
            //SqlDataAdapter da = new SqlDataAdapter(cmd);
            //DataSet ds = new DataSet();
            //da.Fill(ds);
            //dataGridView2.DataSource = ds.Tables[0];
            //conn.Close();

            ly_wip_detail_viewTableAdapter.Fill(this.lYStoreMange.ly_wip_detail_view, ly_wip_plan_viewDataGridView.CurrentRow.Cells["material_plan_num_det"].Value.ToString());

        }

        private void toolStripButton27_Click(object sender, EventArgs e)
        {
            ExportDataGridviewTOExcellA.ExportDataGridview(this.ly_wip_viewDataGridView, true);
        }

        private void dataGridView1_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (null == ly_wip_plan_viewDataGridView.CurrentRow) return;
          
            string nowplannum = this.ly_wip_plan_viewDataGridView.CurrentRow.Cells["material_plan_num_det"].Value.ToString();

           
            //string nowxh = this.ly_inma0010DataGridView.CurrentRow.Cells["id"].Value.ToString();
            //int loanid = int.Parse(this.xD_Client_LoanDataGridView.CurrentRow.Cells["Id_loan"].Value.ToString());

            ly_wip_planInOut_detail queryForm = new ly_wip_planInOut_detail();

            //queryForm.statemode = "原料";
            //queryForm.runmode = "修改";
           
            queryForm.Nowplannum = nowplannum;
            queryForm.StartPosition = FormStartPosition.CenterParent;
            queryForm.WindowState = FormWindowState.Maximized;
            queryForm.ShowDialog();
        }
    }
}
