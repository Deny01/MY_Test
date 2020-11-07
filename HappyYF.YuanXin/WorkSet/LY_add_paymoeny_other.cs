using System;
using System.Windows.Forms;
using HappyYF.Infrastructure.Repositories;
using System.Text.RegularExpressions;
using DataGridFilter;
using System.Data;
using System.Data.SqlClient;
namespace HappyYF.YuanXin.WorkSet
{
    public partial class LY_add_paymoeny_other : Form
    {
        public string client_code_add;
        public string client_name_add;

  
        public LY_add_paymoeny_other()
        {
            InitializeComponent();
        }




        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(client_code_add))
            {
                return;
            }

            if (!(Regex.IsMatch(this.invoice_money.Text, @"^([-]{1}[0-9]+(.[0-9]{2})?$)|([0-9]+(.[0-9]{2})?$)"))) // ^\+?[1-9][0-9]*$
            {
                MessageBox.Show("到款金额格式错误，请重新输入");
                this.invoice_money.Focus();
                return;
            }
            if (string.IsNullOrEmpty(dateTimePicker1.Text))
            {
                MessageBox.Show("到款日期格式错误，请重新输入");
                this.dateTimePicker1.Focus();
                return;
            }
            if (string.IsNullOrEmpty(dateTimePicker2.Text))
            {
                MessageBox.Show("期初日期格式错误，请重新输入");
                     this.dateTimePicker2.Focus();
                return;

               
            }
            DataTable dt = null;
            string sql = @"select [id],[client_code],[start_yingshou],[start_yushou],[pay_money],[pay_money_time]  ,[pay_moeny_peo],[sys_time],[order_start_time],[now_yingshou],[now_yingshou_end] ,[now_yushou_end],isnull(now_yingshou_change,0) as now_yingshou_change
                           from[t_financeReceivables_other] where client_code='" + client_code_add + "' order by id asc";
            using (SqlConnection connection = new SqlConnection(SQLDatabase.Connectstring))
            {

                SqlDataAdapter adapter = new SqlDataAdapter(sql, connection);
                DataSet ds = new DataSet();
                adapter.Fill(ds);
                dt = ds.Tables[0];
            }

            decimal daokuan = decimal.Parse(this.invoice_money.Text);
            // decimal yingshou = GetOrderByBussCodeSUM(client_code_add, dateTimePicker2.Value); //本期应收
            decimal yingshou = 0 ;

            decimal chushiyingshou = 0;
            decimal chushiyushou = 0;
            if (dt.Rows.Count <= 0)
            {
                //第一次 插入数据
                DataTable dt_client = null;
                string sql_client = @"select isnull(start_yingshou_money,0),isnull(strart_yushou_money,0) from ly_sales_client_other where salesclient_code='" + client_code_add + "'";
                using (SqlConnection connection = new SqlConnection(SQLDatabase.Connectstring))
                {

                    SqlDataAdapter adapter = new SqlDataAdapter(sql_client, connection);
                    DataSet ds = new DataSet();
                    adapter.Fill(ds);
                    dt_client = ds.Tables[0];
                }
                chushiyingshou = decimal.Parse(dt_client.Rows[0][0].ToString());
                chushiyushou = decimal.Parse(dt_client.Rows[0][1].ToString());

            }
            else
            {
                decimal a = 0;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    a += decimal.Parse(dt.Rows[i]["now_yingshou"].ToString());

                }
                //for (int i = 0; i < dt.Rows.Count; i++)
                //{
                //    a += decimal.Parse(dt.Rows[i]["now_yingshou_change"].ToString());

                //}
                if (DateTime.Parse(dt.Rows[dt.Rows.Count - 1]["pay_money_time"].ToString()) > dateTimePicker1.Value)
                {
                    MessageBox.Show("该次到款时间不能小于上次到款时间，请重新输入");
                    this.dateTimePicker1.Focus();
                    return;
                }



                yingshou = (yingshou - a);  //本期应收-往期应收=真实的本次应收

                chushiyingshou = decimal.Parse(dt.Rows[dt.Rows.Count - 1]["now_yingshou_end"].ToString()); //最近一次的应收
                chushiyushou = decimal.Parse(dt.Rows[dt.Rows.Count - 1]["now_yushou_end"].ToString());//最近一次的预收
            }
            decimal yue = yingshou + chushiyingshou - chushiyushou - daokuan; //本次应收+初次应收-初次预收-到款 =当前余额


            decimal yingshoujieyu = 0;
            decimal yushoujieyu = 0;
            if (yue >= 0)
            {
                yingshoujieyu = yue;
            }
            else
            {
                yushoujieyu = 0-yue;

            }

            string sqlInsert = @"INSERT INTO  [t_financeReceivables_other]
                                              ([client_code]
                                              ,[start_yingshou]
                                              ,[start_yushou]
                                              ,[pay_money]
                                              ,[pay_money_time]
                                              ,[pay_moeny_peo] 
                                              ,[order_start_time] 
                                              ,[now_yingshou]
                                              ,[now_yingshou_end]
                                              ,[now_yushou_end]
                                              ,[remark]           
                                              )
                                VALUES
                                              ('" + client_code_add + "'," + chushiyingshou + "," + chushiyushou + "," + daokuan + ",'" +
                                              dateTimePicker1.Value + "','" + SQLDatabase.nowUserName() +
                                              "','" + dateTimePicker2.Value + "'," + yingshou + "," + yingshoujieyu + "," + yushoujieyu + ",'"+ textBox1.Text+ "')";
            using (SqlConnection con = new SqlConnection(SQLDatabase.Connectstring))
            {

                using (SqlCommand cmd = new SqlCommand(sqlInsert, con))
                {

                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            client_code_add = client_code.Text;
            this.DialogResult = DialogResult.OK;
            this.Close();

        }
 

        private void LY_add_paymoeny_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(client_code_add))
            {
                client_code.Text = client_code_add;
            }
            if (!string.IsNullOrEmpty(client_name_add))
            {

                client_name.Text = client_name_add;
            }
            DataTable dt = null;
            string sql = @"select [id] ,[order_start_time] from[t_financeReceivables_other] where client_code='" + client_code_add + "'";
            using (SqlConnection connection = new SqlConnection(SQLDatabase.Connectstring))
            {
                SqlDataAdapter adapter = new SqlDataAdapter(sql, connection);
                DataSet ds = new DataSet();
                adapter.Fill(ds);
                dt = ds.Tables[0];
            }
            if (dt.Rows.Count > 0)
            {
               
                dateTimePicker2.Text =DateTime.Parse( dt.Rows[0]["order_start_time"].ToString()).Date.ToString();
                dateTimePicker2.Enabled = false;
           
            }
            else
            {
                dateTimePicker2.Text = DateTime.Parse("2019-06-01").Date.ToString();


                dateTimePicker2.Enabled = false;

            }
        }



        /// <summary>
        /// 得到当前发票节点的所有应收金额
        /// </summary>
        /// <returns></returns>
        private decimal GetOrderByBussCodeSUM(string busCode,DateTime dt)
        {
            SqlConnection sqlConnection1 = new SqlConnection(SQLDatabase.Connectstring);
            SqlCommand cmd = new SqlCommand();

            decimal MaxProductionorder =0;

            cmd.Parameters.Add("@BussCode", SqlDbType.VarChar);
            cmd.Parameters["@BussCode"].Value = busCode;
            cmd.Parameters.Add("@begindate", SqlDbType.DateTime);
            cmd.Parameters["@begindate"].Value = dt;

            cmd.CommandText = "GetOrderByBussCodeSUM";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = sqlConnection1;

            sqlConnection1.Open();
            MaxProductionorder =decimal.Parse( cmd.ExecuteScalar().ToString());
            sqlConnection1.Close();



            return MaxProductionorder;
        }

    }
}
