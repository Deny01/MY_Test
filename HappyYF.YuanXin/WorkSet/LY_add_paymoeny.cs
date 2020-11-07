using System;
using System.Windows.Forms;
using HappyYF.Infrastructure.Repositories;
using System.Text.RegularExpressions;
using DataGridFilter;
using System.Data;
using System.Data.SqlClient;
namespace HappyYF.YuanXin.WorkSet
{
    public partial class LY_add_paymoeny : Form
    {
        public string client_code_add;
        public string client_name_add;
 
  
        public LY_add_paymoeny()
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

           
            DataTable dt = null;
            string sql = @"select [id],[client_code],[start_yingshou],[start_yushou],[pay_money],[pay_money_time] , [pay_moeny_peo],[sys_time],[order_start_time],[now_yingshou],[now_yingshou_end] ,[now_yushou_end],
                          isnull(now_yingshou_change,0) as now_yingshou_change  from [t_financeReceivables] 
                          where client_code='" + client_code_add + "' order by id asc";
            using (SqlConnection connection = new SqlConnection(SQLDatabase.Connectstring))
            {

                SqlDataAdapter adapter = new SqlDataAdapter(sql, connection);
                DataSet ds = new DataSet();
                adapter.Fill(ds);
                dt = ds.Tables[0];
            }

            decimal daokuan = decimal.Parse(this.invoice_money.Text);
 

          
            if (DateTime.Parse(dt.Rows[dt.Rows.Count - 1]["pay_money_time"].ToString()) > dateTimePicker1.Value)
            {
                MessageBox.Show("该次到款时间不能小于上次到款时间，请重新输入");
                this.dateTimePicker1.Focus();
                return;
            }
    
          

            string sqlInsert = @"INSERT INTO  [t_financeReceivables]
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
                                              ('" + client_code_add + "',0,0," + daokuan + ",'" +
                                              dateTimePicker1.Value + "','" + SQLDatabase.nowUserName() +
                                              "','" + dateTimePicker1.Value + "',0,0,0,'" + textBox1.Text + "')";
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
       
        } 

    }
}
