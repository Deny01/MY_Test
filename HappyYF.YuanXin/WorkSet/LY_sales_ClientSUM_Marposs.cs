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
    public partial class LY_sales_ClientSUM_Marposs : Form
    {
        public LY_sales_ClientSUM_Marposs()
        {
            InitializeComponent();
        }

        List<string> itemlist = new List<string>();

        private void LY_sales_ClientSUM_Marposs_Load(object sender, EventArgs e)
        {
            this.dateTimePicker9.Text = SQLDatabase.GetNowdate().Date.Year.ToString() + "-01" + "-01";
            this.dateTimePicker10.Text = SQLDatabase.GetNowdate().AddDays(0).Date.ToString();

            this.ly_salesAndrepair_MaopossReportTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
        }

        private void toolStripButton38_Click(object sender, EventArgs e)
        {
            this.ly_salesAndrepair_MaopossReportTableAdapter.Fill(this.lYSalseMange2.ly_salesAndrepair_MaopossReport,  this.dateTimePicker9.Value, this.dateTimePicker10.Value.AddDays(1));
            
        }

        private void toolStripButton37_Click(object sender, EventArgs e)
        {
            ExportDataGridviewTOExcell.ExportDataGridview(this.ly_salesAndrepair_MaopossReportDataGridView, true);
        }

        private void toolStripTextBox9_KeyUp(object sender, KeyEventArgs e)
        {
            string filterString;


            filterString = GetDataGridviewMultiFilterString.DGVMultiFilterString(this.ly_salesAndrepair_MaopossReportDataGridView, this.toolStripTextBox9.Text);


            this.ly_salesAndrepair_MaopossReportBindingSource.Filter = filterString;
        }

        private void toolStripTextBox9_Enter(object sender, EventArgs e)
        {
            this.ly_salesAndrepair_MaopossReportBindingSource.Filter = "";
        }

        private void 批量指定所属行业ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataGridView dgv = this.ly_salesAndrepair_MaopossReportDataGridView;


            if (null == dgv.CurrentRow) return;



            string sel = "SELECT  style_code as 代码,style_name as 名称 FROM ly_sales_contract_style     order by style_code";


            QueryForm queryForm = new QueryForm();


            queryForm.Sel = sel;
            queryForm.Constr = SQLDatabase.Connectstring;

            //Set the Column Collection to the filter Table
            //queryForm.SetSourceColumns(this.billMainDataSet.BalanceBill.Columns);

            queryForm.ShowDialog();

            string clientno = "";
            //int nowid = -1;
            this.itemlist.Clear();

            foreach (DataGridViewRow dgr in dgv.Rows)
            {
                if (true == dgr.Selected)
                {

                    dgr.Cells["行业编码"].Value = queryForm.Result;
                    dgr.Cells["所属行业"].Value = queryForm.Result1;

                    clientno = dgr.Cells["编码"].Value.ToString();

                    this.itemlist.Add(clientno);




                    UpdateClientCategory(clientno,   queryForm.Result);

                }
            }

            //dgv.EndEdit();

            //this.ly_sales_clientBindingSource.EndEdit();

            //this.ly_sales_clientTableAdapter.Update(this.lYSalseMange.ly_sales_client);

            //int parentId = int.Parse(this.ly_material_plan_mainDataGridView.CurrentRow.Cells["id"].Value.ToString());
            //string planNum = this.ly_material_plan_mainDataGridView.CurrentRow.Cells["计划编号"].Value.ToString();

            //int nowindex = 0;

            //if (3 == this.tabControl1.SelectedIndex)
            //{
            //    this.lY_MaterielRequirementsTableAdapter.Fill(this.lYPlanMange.LY_MaterielRequirements, parentId, "外协", "OWE");


            //    foreach (int nowitem in itemlist)
            //    {
            //        nowindex = this.lY_MaterielRequirementsBindingSource.Find("id", nowitem);
            //        dgv.Rows[nowindex].Selected = true;
            //    }

            //}
            //if (4 == this.tabControl1.SelectedIndex)
            //{
            //    this.lY_MaterielRequirementsTableAdapter.Fill(this.lYPlanMange.LY_MaterielRequirements, parentId, "外购", "OWE");

            //    foreach (int nowitem in itemlist)
            //    {
            //        nowindex = this.lY_MaterielRequirementsBindingSource.Find("id", nowitem);
            //        dgv.Rows[nowindex].Selected = true;
            //    }
            //}
            //if (5 == this.tabControl1.SelectedIndex)
            //{
            //    this.lY_MaterielRequirementsTableAdapter.Fill(this.lYPlanMange.LY_MaterielRequirements, parentId, "机加", "OWE");

            //    foreach (int nowitem in itemlist)
            //    {
            //        nowindex = this.lY_MaterielRequirementsBindingSource.Find("id", nowitem);
            //        dgv.Rows[nowindex].Selected = true;
            //    }

            //}
        }

        private void UpdateClientCategory(string nowclientno,  string nowstyle)
        {
           

            SqlConnection sqlConnection1 = new SqlConnection(SQLDatabase.Connectstring);
            SqlCommand cmd = new SqlCommand();


            cmd.Parameters.Add("@nowclientno", SqlDbType.VarChar);
            cmd.Parameters["@nowclientno"].Value = nowclientno;

            //cmd.Parameters.Add("@nowid", SqlDbType.Int);
            //cmd.Parameters["@nowid"].Value = nowid;

            cmd.Parameters.Add("@nowstyle", SqlDbType.VarChar);
            cmd.Parameters["@nowstyle"].Value = nowstyle;

            //cmd.Parameters.Add("@newvalue", SqlDbType.VarChar);
            //cmd.Parameters["@newvalue"].Value = nowvalue;


            cmd.CommandText = "LY_UpdateClientCategory";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = sqlConnection1;

            sqlConnection1.Open();
            cmd.ExecuteNonQuery();
            sqlConnection1.Close();




        }

        private void UpdateClientOEM(string nowclientno, string nowstyle)
        {


            SqlConnection sqlConnection1 = new SqlConnection(SQLDatabase.Connectstring);
            SqlCommand cmd = new SqlCommand();


            cmd.Parameters.Add("@nowclientno", SqlDbType.VarChar);
            cmd.Parameters["@nowclientno"].Value = nowclientno;

            //cmd.Parameters.Add("@nowid", SqlDbType.Int);
            //cmd.Parameters["@nowid"].Value = nowid;

            cmd.Parameters.Add("@nowstyle", SqlDbType.VarChar);
            cmd.Parameters["@nowstyle"].Value = nowstyle;

            //cmd.Parameters.Add("@newvalue", SqlDbType.VarChar);
            //cmd.Parameters["@newvalue"].Value = nowvalue;


            cmd.CommandText = "LY_UpdateClientOEM";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = sqlConnection1;

            sqlConnection1.Open();
            cmd.ExecuteNonQuery();
            sqlConnection1.Close();




        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if (null == this.ly_salesAndrepair_MaopossReportDataGridView.CurrentRow) return;

            FilterForm filterForm = new FilterForm();




            //SumQueryDataSet qds;
            //qds = new SumQueryDataSet();

            List<string> ls = new List<string>();
            ls.Add("id");


            filterForm.SetSourceColumns(this.lYSalseMange2.ly_salesAndrepair_MaopossReport.Columns, ls);

            filterForm.ShowDialog();

            this.ly_salesAndrepair_MaopossReportBindingSource.Filter = filterForm.GetFilterString();
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            if (null == this.ly_salesAndrepair_MaopossReportDataGridView.CurrentRow) return;
            SortForm DataSort = new SortForm();

            List<string> ls = new List<string>();
            ls.Add("id");


            DataSort.SetSortColumns(this.lYSalseMange2.ly_salesAndrepair_MaopossReport.Columns, ls);
            DataSort.ShowDialog();
            this.ly_salesAndrepair_MaopossReportBindingSource.Sort = DataSort.GetSortString();
        }

        private void 批量指定OEMToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataGridView dgv = this.ly_salesAndrepair_MaopossReportDataGridView;


            if (null == dgv.CurrentRow) return;

            string clientno = "";
            //int nowid = -1;
            this.itemlist.Clear();


            

            foreach (DataGridViewRow dgr in dgv.Rows)
            {
                if (true == dgr.Selected)
                {

                    dgr.Cells["OEM"].Value = "YES";
                    
                    clientno = dgr.Cells["编码"].Value.ToString();

                    this.itemlist.Add(clientno);




                    UpdateClientOEM(clientno, "YES");

                }
            }
        }

        private void 批量取消OEMToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataGridView dgv = this.ly_salesAndrepair_MaopossReportDataGridView;


            if (null == dgv.CurrentRow) return;


            string clientno = "";
            //int nowid = -1;
            this.itemlist.Clear();



            foreach (DataGridViewRow dgr in dgv.Rows)
            {
                if (true == dgr.Selected)
                {

                    dgr.Cells["OEM"].Value = "NO";

                    clientno = dgr.Cells["编码"].Value.ToString();

                    this.itemlist.Add(clientno);




                    UpdateClientOEM(clientno, "NO");

                }
            }
        }

       
    }
}
