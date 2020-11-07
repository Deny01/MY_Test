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

namespace HappyYF.YuanXin.WorkSet
{

    
    public partial class LY_Runing_DashBoard : Form
    {

        private DataSet ds3;
        BindingSource bindingSource3;
        public LY_Runing_DashBoard()
        {
            InitializeComponent();
        }

        private void TableForSumWeek(string nowstyle)
        {


            SqlDataAdapter myAdapter1 = new SqlDataAdapter();

            myAdapter1.SelectCommand = new SqlCommand();

            myAdapter1.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter1.SelectCommand.CommandText = "Get_SalesPrice_ByApprove";
            myAdapter1.SelectCommand.Connection = new SqlConnection(SQLDatabase.Connectstring);
            myAdapter1.SelectCommand.CommandTimeout = 0;

            //myAdapter1.SelectCommand.Parameters.Add("@salesperson_code", SqlDbType.VarChar);
            //myAdapter1.SelectCommand.Parameters["@salesperson_code"].Value = this.nowusercode;

            //myAdapter1.SelectCommand.Parameters.Add("@selcode", SqlDbType.VarChar);
            //myAdapter1.SelectCommand.Parameters["@selcode"].Value = this.nowfillstragecode;

            myAdapter1.SelectCommand.Parameters.Add("@begindate", SqlDbType.DateTime);
            myAdapter1.SelectCommand.Parameters["@begindate"].Value = this.dateTimePicker7.Value;

            myAdapter1.SelectCommand.Parameters.Add("@enddate", SqlDbType.DateTime);
            myAdapter1.SelectCommand.Parameters["@enddate"].Value = this.dateTimePicker8.Value.AddDays(1);

            myAdapter1.SelectCommand.Parameters.Add("@getstyle", SqlDbType.VarChar);
            myAdapter1.SelectCommand.Parameters["@getstyle"].Value = nowstyle;









            if (null != ds3)
                ds3.Dispose();

            ds3 = new DataSet();

            myAdapter1.Fill(ds3);


            myAdapter1.Dispose();

            //this.dataGridView1.DataSource = allData;


            this.dataGridView1.Columns.Clear();

            this.dataGridView1.AutoGenerateColumns = true;
            dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCellsExceptHeaders;

            //BindingSource bindingSource1 = new BindingSource();

            if (null != bindingSource3)
                this.bindingSource3.Dispose();

            this.bindingSource3 = new BindingSource();


            bindingSource3.DataSource = ds3.Tables[0];
            dataGridView1.DataSource = bindingSource3;

            this.bindingNavigator4.BindingSource = bindingSource3;

            bindingSource3.ResetBindings(true);


            SetDGV(this.dataGridView1);
        }
        private void SetDGV(DataGridView nowDGV)
        {
            //nowDGV.Columns[0].Visible = false;

            for (int i = 0; i < nowDGV.Columns.Count; i++)
            {
                nowDGV.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;

                if (i < 1)
                {

                    nowDGV.Columns[i].Frozen = true;
                }

                //nowDGV.Columns[i].ValueType System .Type .nowDGV.Columns[5].DefaultCellStyle.Alignment

                string weekperiod = "---";

                if ("Decimal" == nowDGV.Columns[i].ValueType.Name || "Int32" == nowDGV.Columns[i].ValueType.Name)
                {
                    nowDGV.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    nowDGV.Columns[i].DefaultCellStyle.Format = "N0";

                    if ("SUM" != nowDGV.Columns[i].Name)
                    {
                        GetWeekPeriod(nowDGV.Columns[i].Name, out weekperiod);

                        nowDGV.Columns[i].ToolTipText = weekperiod;
                    }
                    else
                    {

                        nowDGV.Columns[i].ToolTipText = this.dateTimePicker7.Value.Date.ToString("yyyy-MM-dd") + ":" + this.dateTimePicker8.Value.Date.ToString("yyyy-MM-dd");
                    }

                }

                //if (nowDGV.Columns[i].HeaderText.Contains("总计") || nowDGV.Columns[i].HeaderText.Contains("增长率"))
                //{
                //    //nowDGV.Columns[i].DefaultCellStyle.BackColor = Color.SkyBlue;
                //    nowDGV.Columns[i].DefaultCellStyle.ForeColor = Color.Red;


                //}

                //if (nowDGV.Columns[i].HeaderText.Contains("总计") || nowDGV.Columns[i].HeaderText.Contains("合计"))
                if (nowDGV.Columns[i].HeaderText.Contains("SUM"))
                {
                    nowDGV.Columns[i].DefaultCellStyle.BackColor = Color.Teal;
                    nowDGV.Columns[i].DefaultCellStyle.ForeColor = Color.Navy;
                }



            }

            //string nowreigon;

            //if (nowDGV.RowCount >= 2)
            //{
            // clear all 
            //    nowreigon = nowDGV.Rows[0].Cells["区域"].Value.ToString();
            //    for (int i = 1; i < nowDGV.RowCount; i++)
            //    {


            //        if (nowDGV.Rows[i].Cells["区域"].Value.ToString() == nowreigon)
            //        {

            //            for (int j = 0; j < nowDGV.Columns.Count; j++)
            //            {
            //                if (nowDGV.Columns[j].HeaderText.Contains("区域"))
            //                {


            //                    nowDGV.Rows[i].Cells[j].Value = DBNull.Value;


            //                }
            //            }
            //        }
            //        else
            //        {
            //            nowreigon = nowDGV.Rows[i].Cells["区域"].Value.ToString();
            //        }



            //    }
            //}

        }

        private void GetWeekPeriod(string nowweek, out string nowweekperiod)
        {
            if ("SUM" == nowweek)
            {
                nowweekperiod = "SUM";
                return;


            }

            SqlConnection sqlConnection1 = new SqlConnection(SQLDatabase.Connectstring);
            SqlCommand cmd = new SqlCommand();



            cmd.Parameters.Add("@nowweek", SqlDbType.VarChar);
            cmd.Parameters["@nowweek"].Value = nowweek;


            cmd.Parameters.Add("@weekperiod", SqlDbType.VarChar);
            cmd.Parameters["@weekperiod"].Direction = ParameterDirection.Output;
            cmd.Parameters["@weekperiod"].Size = 30;








            cmd.CommandText = "LY_GetWeek_Period";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = sqlConnection1;

            sqlConnection1.Open();
            cmd.ExecuteNonQuery();
            sqlConnection1.Close();

            nowweekperiod = cmd.Parameters["@weekperiod"].Value.ToString();


        }
        private void toolStripButton31_Click(object sender, EventArgs e)
        {
            NewFrm.Show(this);

            TableForSumWeek("FromTable");

            NewFrm.Hide(this);
        }

        private void LY_Runing_DashBoard_Load(object sender, EventArgs e)
        {
            this.dateTimePicker7.Text = SQLDatabase.GetNowdate().Date.Year.ToString() + "-01" + "-01";
            this.dateTimePicker8.Text = SQLDatabase.GetNowdate().AddDays(0).Date.ToString();

            this.ly_dashboard_storeout_accumlationTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            this.ly_dashboard_inspectionout_accumlationTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;




        }

        private void toolStripButton27_Click(object sender, EventArgs e)
        {
            ExportDataGridviewTOExcellA.ExportDataGridview(this.dataGridView1, true);
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            this.ly_dashboard_storeout_accumlationTableAdapter.Fill(this.lYSalseMange2.ly_dashboard_storeout_accumlation, this.dateTimePicker7.Value, this.dateTimePicker8.Value.AddDays(1));
            AddSummationRow_New(ly_dashboard_storeout_accumlationBindingSource, "asmoney", ly_dashboard_storeout_accumlationDataGridView, "asmoney");

        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            this.ly_dashboard_inspectionout_accumlationTableAdapter.Fill(this.lYSalseMange2.ly_dashboard_inspectionout_accumlation, this.dateTimePicker7.Value, this.dateTimePicker8.Value.AddDays(1));
            AddSummationRow_New(ly_dashboard_inspectionout_accumlationBindingSource, "asmoney", ly_dashboard_inspectionout_accumlationDataGridView, "asmoney1");

        }

        private void AddSummationRow_New(BindingSource bs,string bsitem, DataGridView dgv,string dgvitem)
        {
            //InitializeApp();
            //return;

            DataRow sumdr = (((DataSet)bs.DataSource).Tables[bs.DataMember]).NewRow();

            sumdr[bsitem] = 0;

            if (-1 != bs.Find("contract_inner_code", "合计"))
            {
                bs.RemoveAt(bs.Find("contract_inner_code", "合计"));
            }

            foreach (DataGridViewRow dgvRow in dgv.Rows)
            {

                if (dgvRow.Cells[dgvitem].Value  == null || (dgvRow.Cells[dgvitem].Value  is DBNull))
                    dgvRow.Cells[dgvitem].Value  = 0;


                sumdr[bsitem] = Convert.ToInt64(sumdr[bsitem]) + Convert.ToInt64(dgvRow.Cells[dgvitem].Value);


                

            }

            
            sumdr["contract_inner_code"] = "合计";
            sumdr["xhc"] = "";
            //sumdr["税务"] = "";
            ((DataSet)bs.DataSource).Tables[bs.DataMember].Rows.Add(sumdr);
            bs.ResetBindings(true);

        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            NewFrm.Show(this);

            TableForSumWeek("FromCal");

            NewFrm.Hide(this);

        }
    }
}
