using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HappyYF.Infrastructure.Repositories;
using System.Data.SqlClient;

namespace HappyYF.YuanXin.WorkSet
{
    public partial class LY_ReceivableRep : Form
    {
        public LY_ReceivableRep()
        {
            InitializeComponent();
            
        }



       

        private void LY_Quality_Control_PurchaseRep_Load(object sender, EventArgs e)
        {
            this.dateTimePicker1.MinDate = DateTime.Parse("2019-06-01");
            this.dateTimePicker3.MinDate = DateTime.Parse("2019-06-01");
            DateTime dtnew= DateTime.Now.AddDays(1 - DateTime.Now.Day).Date;
            string st = dtnew.ToString();
            string et = dtnew.AddMonths(1).AddDays(-1).ToString();
            this.dateTimePicker1.Text = st;
            this.dateTimePicker2.Text = et;
            this.dateTimePicker3.Text = st;
            this.dateTimePicker4.Text = et;

         
            this.ly_sales_clientreceivablesRepOtherTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
        }
 
      
       

        private void button2_Click(object sender, EventArgs e)
        {
 
            this.ly_sales_clientreceivablesRepOtherTableAdapter.Fill(this.lYSalseMange.ly_sales_clientreceivablesRepOther, DateTime.Parse(this.dateTimePicker3.Text).Date, DateTime.Parse(this.dateTimePicker4.Text).Date.AddDays(1));
            AddSummationRow_New1(lysalesclientreceivablesRepOtherBindingSource, dataGridView1);
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            ExportDataGridviewTOExcellA.ExportDataGridview(this.dataGridView1, true);
        }

        private void toolStripTextBox2_Enter(object sender, EventArgs e)
        {

            toolStripTextBox2.Text = "";

            this.lysalesclientreceivablesRepOtherBindingSource.Filter = "";
        }

        private void toolStripTextBox2_KeyUp(object sender, KeyEventArgs e)
        {
            //if (this.dataGridView1.Rows.Count <= 0)
            //    return;
            string filterString;

            filterString = GetDataGridviewMultiFilterString.DGVMultiFilterString(this.dataGridView1, this.toolStripTextBox2.Text);

            this.lysalesclientreceivablesRepOtherBindingSource.Filter = filterString;
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
             NewFrm.Show(this);
            ly_purchase_contract_inspectionRepDataGridView.DataSource = null;
            string userId = SQLDatabase.NowUserID;
            string sql = "";
            if (SQLDatabase.CheckHaveRight(userId, "设置客户期初应收"))
            {
                sql = @"SELECT a.id,  a.salesclient_code, a.salesclient_name,  b.yhmc                
                         FROM ly_sales_client AS a LEFT OUTER JOIN
                         T_users AS b ON a.salesperson_code = b.yhbm 
                         where isnull(a.in_use,0)=0
                         and a.start_time is not null
                         and a.salesclient_code<>'YY0001247'";
            }
            else if (SQLDatabase.CheckHaveRight(userId, "查看客户期初应收"))
            {
                sql = @"SELECT a.id,  a.salesclient_code, a.salesclient_name,  b.yhmc                
                         FROM ly_sales_client AS a LEFT OUTER JOIN
                         T_users AS b ON a.salesperson_code = b.yhbm 
                         where isnull(a.in_use,0)=0
                         and a.start_time is not null
                         and a.salesclient_code<>'YY0001247'";
            }
            else
            {
                string salespeople = SQLDatabase.nowUserName();

                //sql = @"SELECT a.id, a.salesclient_code, a.salesclient_name,  b.yhmc                
                //         FROM ly_sales_client AS a LEFT OUTER JOIN
                //         T_users AS b ON a.salesperson_code = b.yhbm 
                //         where isnull(a.in_use,0)=0
                //         and a.start_time is not null and b.yhmc='" + salespeople + "'  and a.salesclient_code<>'YY0001247'";
                sql = @" SELECT a.id, a.salesclient_code, a.salesclient_name,  b.yhmc  ,b.yhbm              
                         FROM ly_sales_client AS a LEFT OUTER JOIN
                         T_users AS b ON a.salesperson_code = b.yhbm 
                         where isnull(a.in_use,0)=0
                         and a.start_time is not null  and a.salesclient_code<>'YY0001247'
                         and (b.yhmc='"+ salespeople + "'  or salesregion_code in(select salesregion_code from ly_salesregion where  salesregion_leader='"+ userId + "'))";


                // lysalesclientreceivablesNewsBindingSource.Filter = "";

            }


            DataTable dt = new DataTable("mytable");
            //DataColumn dc0 = new DataColumn("id", Type.GetType("System.Int32"));
            DataColumn dc1 = new DataColumn("客户编码", Type.GetType("System.String"));
            DataColumn dc2 = new DataColumn("客户名称", Type.GetType("System.String"));
            DataColumn dc3 = new DataColumn("营业员", Type.GetType("System.String"));
            DataColumn dc4 = new DataColumn("期初应收", Type.GetType("System.Decimal"));
            DataColumn dc5 = new DataColumn("期初预收", Type.GetType("System.Decimal"));
            DataColumn dc6 = new DataColumn("到款合计", Type.GetType("System.Decimal"));
            DataColumn dc7 = new DataColumn("开票合计", Type.GetType("System.Decimal"));
            DataColumn dc8 = new DataColumn("应收结余", Type.GetType("System.Decimal"));
            DataColumn dc9 = new DataColumn("预收结余", Type.GetType("System.Decimal"));
           // dt.Columns.Add(dc0);
            dt.Columns.Add(dc1);
            dt.Columns.Add(dc2);
            dt.Columns.Add(dc3);
            dt.Columns.Add(dc4);
            dt.Columns.Add(dc5);
            dt.Columns.Add(dc6);
            dt.Columns.Add(dc7);
            dt.Columns.Add(dc8);
            dt.Columns.Add(dc9);


    

            DataTable dt_client = null;
            string sql_client = sql;
            using (SqlConnection connection = new SqlConnection(SQLDatabase.Connectstring))
            {
       
                SqlDataAdapter adapter = new SqlDataAdapter(sql_client, connection);
                DataSet ds = new DataSet();
                adapter.Fill(ds);
                dt_client = ds.Tables[0];
            }
            for (int i = 0; i < dt_client.Rows.Count; i++)
            {
                DataRow dr = dt.NewRow();
                //dr["id"] = int.Parse(dt_client.Rows[i]["id"].ToString());
                dr["客户编码"] = dt_client.Rows[i]["salesclient_code"].ToString();
                dr["客户名称"] = dt_client.Rows[i]["salesclient_name"].ToString();
                dr["营业员"] = dt_client.Rows[i]["yhmc"].ToString();

                string sql_c = @"select  start_yingshou  ,start_yushou   ,end_yingshou  ,
                               end_yushou ,kaipiao ,fukuan from  dbo.f_GetMoneyNew('"+ dt_client.Rows[i]["salesclient_code"].ToString() + "'," +
                    "'" + DateTime.Parse(this.dateTimePicker1.Text).Date + "','"+ DateTime.Parse(this.dateTimePicker2.Text).Date.AddDays(1) + "')"; 
                DataTable dt_client_c= null; 
                using (SqlConnection connection = new SqlConnection(SQLDatabase.Connectstring))
                {
  
                    SqlDataAdapter adapter = new SqlDataAdapter(sql_c, connection);
                    DataSet ds = new DataSet();
                    adapter.Fill(ds);
                    dt_client_c = ds.Tables[0];
                }
                dr["期初应收"] =decimal.Parse( dt_client_c.Rows[0]["start_yingshou"].ToString());
                dr["期初预收"] = decimal.Parse(dt_client_c.Rows[0]["start_yushou"].ToString());
                dr["到款合计"] = decimal.Parse(dt_client_c.Rows[0]["fukuan"].ToString());
                dr["开票合计"] = decimal.Parse(dt_client_c.Rows[0]["kaipiao"].ToString());
                dr["应收结余"] = decimal.Parse(dt_client_c.Rows[0]["end_yingshou"].ToString());
                dr["预收结余"] = decimal.Parse(dt_client_c.Rows[0]["end_yushou"].ToString());

                dt.Rows.Add(dr);

            }
            decimal a = 0;
            decimal b = 0;
            decimal c = 0;
            decimal d = 0;
            decimal f = 0;
            decimal g = 0;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                a += decimal.Parse(dt.Rows[i]["期初应收"].ToString());
                b += decimal.Parse(dt.Rows[i]["期初预收"].ToString());
                c += decimal.Parse(dt.Rows[i]["到款合计"].ToString());
                d += decimal.Parse(dt.Rows[i]["开票合计"].ToString());
                f += decimal.Parse(dt.Rows[i]["应收结余"].ToString());
                g += decimal.Parse(dt.Rows[i]["预收结余"].ToString());
            }
            DataRow dr_z = dt.NewRow();

            dr_z["客户编码"] = "_总计";
            dr_z["客户名称"] = null;
            dr_z["营业员"] = null;
            dr_z["期初应收"] = a;
            dr_z["期初预收"] = b;
            dr_z["到款合计"] = c;
            dr_z["开票合计"] = d;
            dr_z["应收结余"] = f;
            dr_z["预收结余"] = g;
            dt.Rows.Add(dr_z);


            bindingSource1.DataSource = dt;


            ly_purchase_contract_inspectionRepDataGridView.DataSource = bindingSource1;


      

            NewFrm.Hide(this);
        }



        private void toolStripTextBox1_Enter_1(object sender, EventArgs e)
        {

            toolStripTextBox1.Text = "";

            this.bindingSource1.Filter = "";

            //DataGridView dgvSp = ly_purchase_contract_inspectionRepDataGridView;
            // dgvSp.ClearSelection();
            // toolStripTextBox1.Text = "";
        }

        private void toolStripTextBox1_KeyUp_1(object sender, KeyEventArgs e)
        {
            //if (this.ly_purchase_contract_inspectionRepDataGridView.Rows.Count <= 0)
            //    return;
            string filterString;

            filterString = GetDataGridviewMultiFilterString.DGVMultiFilterString(this.ly_purchase_contract_inspectionRepDataGridView, this.toolStripTextBox1.Text);

            this.bindingSource1.Filter = filterString;



            //string InputStr = toolStripTextBox1.Text;
            //DataGridView dgvSp = ly_purchase_contract_inspectionRepDataGridView;
            //int rowNum = -1;
            //foreach (DataGridViewRow dvr in dgvSp.Rows)
            //{
            //    if (dvr.Cells[1].Value.ToString().Contains(InputStr))
            //    {
            //        //dgvSp.ClearSelection();
            //        //dvr.Selected = true;
            //        //dgvSp.CurrentCell = dvr.Cells[1];
            //        rowNum = dvr.Index; 
            //        break;
            //    }
            //}
            //if (rowNum == -1)
            //    return;
            //DataGridViewRow row = dgvSp.Rows[rowNum];
            //dgvSp.Rows.RemoveAt(rowNum);
            //dgvSp.Rows.Insert(0, row);
            //dgvSp.Rows[0].Cells[1].Selected = true;
        }
        private void AddSummationRow_New1(BindingSource bs, DataGridView dgv)
        {

            DataRow sumdr = (((DataSet)bs.DataSource).Tables[bs.DataMember]).NewRow();

            if (-1 != bs.Find("salesclient_code", "_合计"))
            {
                bs.RemoveAt(bs.Find("salesclient_code", "_合计"));
            }

            foreach (DataGridViewRow dgvRow in dgv.Rows)
            {
                foreach (DataGridViewCell dgvCell in dgvRow.Cells)
                {
                    if (dgvCell.Value != null && !(dgvCell.Value is DBNull))
                    {
                        //if (IsInteger(dgvCell.Value))
                        //{
                        //    if ("年份" != dgvCell.OwningColumn.HeaderText && "月份" != dgvCell.OwningColumn.HeaderText && "税率" != dgvCell.OwningColumn.HeaderText)
                        //    {
                        //        if (sumdr[dgvCell.OwningColumn.DataPropertyName] == null || (sumdr[dgvCell.OwningColumn.DataPropertyName] is DBNull))
                        //            sumdr[dgvCell.OwningColumn.DataPropertyName] = 0;


                        //        sumdr[dgvCell.OwningColumn.DataPropertyName] = Convert.ToInt64(sumdr[dgvCell.OwningColumn.DataPropertyName]) + Convert.ToInt64(dgvCell.Value);
                        //    }
                        //}
                        if (IsDecimal(dgvCell.Value))
                        {
                            if (sumdr[dgvCell.OwningColumn.DataPropertyName] == null || (sumdr[dgvCell.OwningColumn.DataPropertyName] is DBNull))
                                sumdr[dgvCell.OwningColumn.DataPropertyName] = 0;
                            sumdr[dgvCell.OwningColumn.DataPropertyName] = Convert.ToDecimal(sumdr[dgvCell.OwningColumn.DataPropertyName]) + Convert.ToDecimal(dgvCell.Value);
                        }

                    }
                }

            }


            sumdr["salesclient_code"] = "_合计";
            ((DataSet)bs.DataSource).Tables[bs.DataMember].Rows.Add(sumdr);
            bs.ResetBindings(true);

        }

   
        protected bool IsInteger(object o)
        {
            if (o is Int64)
                return true;
            if (o is Int32)
                return true;
            if (o is Int16)
                return true;
            return false;
        }
        protected bool IsDecimal(object o)
        {
            if (o is Decimal)
                return true;
            if (o is Single)
                return true;
            if (o is Double)
                return true;
            return false;
        }

        private void toolStripButton10_Click_1(object sender, EventArgs e)
        {
            ExportDataGridviewTOExcellA.ExportDataGridview(this.ly_purchase_contract_inspectionRepDataGridView, true);
        }
    }
}
