using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient ;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DataGridFilter;
using HappyYF.Infrastructure.Repositories;
using HappyYF.YuanXin.Data;

namespace HappyYF.YuanXin.WorkSet
{
    public partial class LY_StoreInAnalysis_FinNew : Form
    {

        public LY_StoreInAnalysis_FinNew()
        {
            InitializeComponent();
        }


        private void toolStripButton3_Click(object sender, EventArgs e)
        {
          
        }





        public bool ExportDataGridview(DataGridView gridView, bool isShowExcle)
        {
            if (gridView.Rows.Count == 0)
                return false;
            //建立Excel对象
            Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();
            excel.Application.Workbooks.Add(true);
            excel.Visible = isShowExcle;
            //生成字段名称
            for (int i = 0; i < gridView.ColumnCount; i++)
            {
                excel.Cells[1, i + 1] = gridView.Columns[i].HeaderText;
            }
            //填充数据
            for (int i = 0; i <= gridView.RowCount - 1; i++)
            {
                for (int j = 0; j < gridView.ColumnCount; j++)
                {
                    if (gridView[j, i].ValueType == typeof(string))
                    {
                        excel.Cells[i + 2, j + 1] = "'" + gridView[j, i].Value.ToString();
                    }
                    else
                    {
                        excel.Cells[i + 2, j + 1] = gridView[j, i].Value.ToString();
                    }
                }
            }
            return true;
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            ExportDataGridview(dataGridView1, true);
        }







        private void AddSummationRow(BindingSource bs, DataGridView dg)
        {
            if (null == dg.CurrentRow) return;

            decimal sum_qty = 0;
            decimal sum_qty_ylj = 0;
            decimal sum_qty_wxf = 0;
            decimal sum_qty_wxjgf = 0;
            decimal sum_qty_rgf = 0;
            decimal sum_qty_cbf = 0;

            decimal sum_qty_zjcl = 0;
            decimal sum_qty_zcb = 0;
            foreach (DataGridViewRow dr in dg.Rows)
            {
                if (System.DBNull.Value == dr.Cells["数量"].Value)
                    sum_qty = sum_qty + 0;
                else
                    sum_qty = sum_qty + decimal.Parse(dr.Cells["数量"].Value.ToString());

                if (System.DBNull.Value == dr.Cells["原料价"].Value)
                    sum_qty_ylj = sum_qty_ylj + 0;
                else
                    sum_qty_ylj = sum_qty_ylj + decimal.Parse(dr.Cells["原料价"].Value.ToString());

                if (System.DBNull.Value == dr.Cells["外协费"].Value)
                    sum_qty_wxf = sum_qty_wxf + 0;
                else
                    sum_qty_wxf = sum_qty_wxf + decimal.Parse(dr.Cells["外协费"].Value.ToString());

                if (System.DBNull.Value == dr.Cells["外协加工费"].Value)
                    sum_qty_wxjgf = sum_qty_wxjgf + 0;
                else
                    sum_qty_wxjgf = sum_qty_wxjgf + decimal.Parse(dr.Cells["外协加工费"].Value.ToString());


                if (System.DBNull.Value == dr.Cells["FartificialTotal"].Value)
                    sum_qty_rgf = sum_qty_rgf + 0;
                else
                    sum_qty_rgf = sum_qty_rgf + decimal.Parse(dr.Cells["FartificialTotal"].Value.ToString());

                if (System.DBNull.Value == dr.Cells["FcostBTotal"].Value)
                    sum_qty_cbf = sum_qty_cbf + 0;
                else
                    sum_qty_cbf = sum_qty_cbf + decimal.Parse(dr.Cells["FcostBTotal"].Value.ToString());


                if (System.DBNull.Value == dr.Cells["yclFy"].Value)
                    sum_qty_zjcl = sum_qty_zjcl + 0;
                else
                    sum_qty_zjcl = sum_qty_zjcl + decimal.Parse(dr.Cells["yclFy"].Value.ToString());

                if (System.DBNull.Value == dr.Cells["zFy"].Value)
                    sum_qty_zcb = sum_qty_zcb + 0;
                else
                    sum_qty_zcb = sum_qty_zcb + decimal.Parse(dr.Cells["zFy"].Value.ToString());
            }
            bs.AddNew();


            dg.CurrentRow.Cells["数量"].Value = sum_qty;
            dg.CurrentRow.Cells["原料价"].Value = sum_qty_ylj;
            dg.CurrentRow.Cells["外协费"].Value = sum_qty_wxf;
            dg.CurrentRow.Cells["外协加工费"].Value = sum_qty_wxjgf;
            dg.CurrentRow.Cells["FartificialTotal"].Value = sum_qty_rgf;
            dg.CurrentRow.Cells["FcostBTotal"].Value = sum_qty_cbf;
            dg.CurrentRow.Cells["yclFy"].Value = sum_qty_zjcl;
            dg.CurrentRow.Cells["zFy"].Value = sum_qty_zcb;
            bs.EndEdit();

            bs.Position = 0;




        }



        private void button12_Click(object sender, EventArgs e)
        {

            string sel = "SELECT distinct  categoryCw as 财务组别 FROM ly_store_in_View_Fin";


            QueryForm queryForm = new QueryForm();


            queryForm.Sel = sel;
            queryForm.Constr = SQLDatabase.Connectstring;

            queryForm.ShowDialog();

            this.textBox17.Text = queryForm.Result;
        }

        private void LY_StoreInAnalysis_FinNew_Load(object sender, EventArgs e)
        {
            this.get_store_inTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            this.dateTimePicker1.Text = (DateTime.Today.AddDays(26 - DateTime.Today.Day)).AddMonths(-1).Date.ToString();
            this.dateTimePicker2.Text = DateTime.Today.AddDays(26 - DateTime.Today.Day).Date.ToString();
       
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox17.Text == "")
            {
                MessageBox.Show("请选择财务组别！"); return;
            }
            if (textBox17.Text.Trim() != "成品")
            {
                MessageBox.Show("请选择成品财务组别！"); return;
            }
            DateTime dtstart = this.dateTimePicker1.Value.Date;
            DateTime dtend = this.dateTimePicker2.Value.Date.AddDays(1);
            if (dtend < dtstart)
            {
                MessageBox.Show("结束时间必须大于开始时间！"); return;
            }
  
            if (textBox1.Text == "" || textBox2.Text == "")
            {
                MessageBox.Show("分摊费用不能为空！");return;
            }

            decimal m = 0;
            decimal n = 0;
            try
            {
                m = decimal.Parse(textBox1.Text);
                n = decimal.Parse(textBox2.Text);
            }

            catch (Exception ex)
            {
                MessageBox.Show("金额格式错误！"); return;
            }

            //这一块需要修改



            //这一块进行重复判断
            string sqlquery = "select top 1 isnull(lock,0) ,end_time from t_fentan order by id desc";
            DataTable dtmax = null;
            using (SqlConnection connection = new SqlConnection(SQLDatabase.Connectstring))
            {
                SqlDataAdapter adapter = new SqlDataAdapter(sqlquery, connection);
                DataSet ds = new DataSet();
                adapter.Fill(ds);
                dtmax = ds.Tables[0];
            }

            if (dtmax.Rows.Count == 1)
            {
                if (DateTime.Parse(dtmax.Rows[0][1].ToString()) >= dtstart)
                {
                    if (dtmax.Rows[0][0].ToString() == "True")
                    {

                        MessageBox.Show("时间段有分摊,并被锁定，无法再次分摊！"); return;

                    }
                }
                else
                {
                    DateTime dtstart2 = new DateTime(dtstart.Year, dtstart.Month, dtstart.Day, 0, 0, 1);
                    DateTime dtend2 = new DateTime(dtend.Year, dtend.Month, dtend.Day-1, 23, 59, 59);
                    string sqlUp = @"insert into t_fentan (start_time,end_time ,people,fartificial,fcost)  values ('" + dtstart2
                                   + "','" + dtend2 + "','" + SQLDatabase.nowUserName() + "'," + m + "," + n + ")";
                    using (SqlConnection con = new SqlConnection(SQLDatabase.Connectstring))
                    {

                        using (SqlCommand cmd = new SqlCommand(sqlUp, con))
                        {

                            con.Open();
                            cmd.ExecuteNonQuery();
                        }
                    }
                }
            }
            else
            {
                DateTime dtstart2 = new DateTime(dtstart.Year, dtstart.Month, dtstart.Day, 0, 0, 1);
                DateTime dtend2 = new DateTime(dtend.Year, dtend.Month, dtend.Day - 1, 23, 59, 59);
                string sqlUp = @"insert into t_fentan (start_time,end_time ,people,fartificial,fcost)  values ('" + dtstart2
                               + "','" + dtend2 + "','" + SQLDatabase.nowUserName() + "'," + m + "," + n + ")";
                using (SqlConnection con = new SqlConnection(SQLDatabase.Connectstring))
                {

                    using (SqlCommand cmd = new SqlCommand(sqlUp, con))
                    {

                        con.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            string message = "确定时间段内费用进行平摊？";
            string caption = "提示...";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;

            DialogResult result  = MessageBox.Show(message, caption, buttons,
            MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

            if (result == DialogResult.Yes)
            {
                NewFrm.Show(this);
                string sql = @"select a.id,a.wzbh,ISNULL(a.qty,0)as qty,isnull(b.T6_factor,0) as T6_factor
                               from ly_store_in_View_Fin as a left join ly_assembly_time b on a.wzbh = b.Item_Code
                               where(a.input_date >= '"+ dtstart + "' and a.input_date < '"+dtend+"')  and (a.categoryCw = '"+ textBox17.Text 
                               + "')  and   isnull(a.in_style,'未录') <>'采购入库'  and   isnull(a.in_style,'未录') <>'盘点' and isnull(a.in_style,'未录') <>'改型号入库' ";

                DataTable dt = null; 
                using (SqlConnection connection = new SqlConnection(SQLDatabase.Connectstring))
                { 
                    SqlDataAdapter adapter = new SqlDataAdapter(sql, connection);
                    DataSet ds = new DataSet();
                    adapter.Fill(ds);
                    dt = ds.Tables[0];
                }
                decimal total = 0;//总系数分母
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    total += (   decimal.Parse(dt.Rows[i]["qty"].ToString()) * decimal.Parse(dt.Rows[i]["T6_factor"].ToString())  );

                }
                DateTime dtt = DateTime.Now;
                decimal rengong = 0;
                decimal cost = 0;
              
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    
                    rengong = (decimal.Parse(dt.Rows[i]["qty"].ToString()) * decimal.Parse(dt.Rows[i]["T6_factor"].ToString())) / total * m;
                    cost = (decimal.Parse(dt.Rows[i]["qty"].ToString()) * decimal.Parse(dt.Rows[i]["T6_factor"].ToString())) / total * n;

                    string sqlUp = "update ly_store_in set Fartificial="+ rengong + ",FartificialTime='"+ dtt + "',FcostB="+cost+ ",FcostTime= '" + dtt + "'  where id=" + dt.Rows[i]["id"].ToString();
                 
                    using (SqlConnection con = new SqlConnection(SQLDatabase.Connectstring))
                    {

                        using (SqlCommand cmd = new SqlCommand(sqlUp, con))
                        {

                            con.Open();
                            cmd.ExecuteNonQuery();
                        }
                    }
                }
                NewFrm.Hide(this);
               
                this.get_store_inTableAdapter.Fill(this.lYStoreMange.get_store_in, dtstart, dtend, textBox17.Text);
                textBox1.Text = "";
                textBox2.Text = "";


                

                AddSummationRow(getstoreinBindingSource, dataGridView1);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox17.Text == "")
            {
                MessageBox.Show("请选择财务组别！"); return;
            }
            DateTime dtstart = this.dateTimePicker1.Value.Date;
            DateTime dtend = this.dateTimePicker2.Value.Date.AddDays(1);
            if (dtend < dtstart)
            {
                MessageBox.Show("结束时间必须大于开始时间！"); return;
            }
            //dtstart = new DateTime(dtstart.Year, dtstart.Month, dtstart.Day, 0, 0, 1);
            //dtend = new DateTime(dtend.Year, dtend.Month, dtend.Day, 23, 59, 59);


            this.get_store_inTableAdapter.Fill(this.lYStoreMange.get_store_in, dtstart, dtend, textBox17.Text);


            AddSummationRow(getstoreinBindingSource, dataGridView1);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            t_fentan queryForm = new t_fentan();

          

            queryForm.StartPosition = FormStartPosition.CenterParent;
            queryForm.ShowDialog();

            if (queryForm.DialogResult != DialogResult.Cancel)
            {
                
            }

        }
    }
}
