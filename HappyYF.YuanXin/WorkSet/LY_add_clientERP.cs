using System;
using System.Windows.Forms;
using HappyYF.Infrastructure.Repositories;
using System.Text.RegularExpressions;
using DataGridFilter;
using System.Data;
using System.Data.SqlClient;
namespace HappyYF.YuanXin.WorkSet
{
    public partial class LY_add_clientERP : Form
    {

        public string client_code_add;
        public string client_code_id;
        public DateTime begintime;
        public DateTime endtime;
        public LY_add_clientERP()
        {
            InitializeComponent();
        }




        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(client_code_add) || string.IsNullOrEmpty(client_code_id))
            {
                return;
            }
            if (string.IsNullOrEmpty(client_name.Text))
            {
                MessageBox.Show("客户编码不能为空，请重新输入");
                this.client_name.Focus();
                return;
            }
            if (!(Regex.IsMatch(this.invoice_money.Text, @"^([-]{1}[0-9]+(.[0-9]{2})?$)|([0-9]+(.[0-9]{2})?$)"))) // ^\+?[1-9][0-9]*$
            {
                MessageBox.Show("金额格式错误，请重新输入");
                this.invoice_money.Focus();
                return;
            }
            if (!(Regex.IsMatch(this.textBox2.Text, @"^([-]{1}[0-9]+(.[0-9]{2})?$)|([0-9]+(.[0-9]{2})?$)"))) // ^\+?[1-9][0-9]*$
            {
                MessageBox.Show("金额格式错误，请重新输入");
                this.textBox2.Focus();
                return;
            }
            if (string.IsNullOrEmpty(dateTimePicker1.Text))
            {
                MessageBox.Show("期初日期格式错误，请重新输入");
                this.dateTimePicker1.Focus();
                return;
            }
            if (decimal.Parse(this.invoice_money.Text) > 0 && decimal.Parse(this.textBox2.Text) > 0)
            {
                MessageBox.Show("预收和应收不能同时存在");
                
                return;
            }

            SqlConnection myConn = new SqlConnection(SQLDatabase.Connectstring);
            myConn.Open();
            SqlCommand myComm = new SqlCommand();
            try
            {
                myComm.Connection = myConn;
                myComm.CommandText = @"update ly_sales_client   set start_yingshou_money = " + textBox2.Text + ",  strart_yushou_money = " + invoice_money.Text + ", start_time = '"
                         + dateTimePicker1.Value + "' where (id = " + client_code_id + ") ";

                myComm.ExecuteNonQuery();

            }
            catch (Exception err)
            {
                throw new ApplicationException("事务操作出错，系统信息：" + err.Message);
            }
            finally
            {
                myConn.Close();
            }

 

            SqlConnection myConnN = new SqlConnection(SQLDatabase.Connectstring);
            myConnN.Open();
            SqlCommand myCommN = new SqlCommand();
            try
            {
                myCommN.Connection = myConnN;

                myCommN.CommandText = @" insert into  [t_financeReceivables]
                                              ([client_code]  ,[start_yingshou]
                                              ,[start_yushou],[pay_money]
                                              ,[pay_money_time],[pay_moeny_peo] 
                                              ,[order_start_time]  ,[now_yingshou]
                                              ,[now_yingshou_end] ,[now_yushou_end] ,[remark]           
                                              )
                                values
                                              ('" + client_code_add + "'," + textBox2.Text + "," + invoice_money.Text + ",0,'" +
                                              dateTimePicker1.Value + "','期初','" + dateTimePicker1.Value + "',0,0,0,'期初生成')";


                myCommN.ExecuteNonQuery();
                this.DialogResult = DialogResult.OK;
                this.Close();

            }
            catch (Exception err)
            {
                throw new ApplicationException("事务操作出错，系统信息：" + err.Message);
            }
            finally
            {
                myConnN.Close();
            }


        }

        private void LY_add_paymoeny_Load(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(client_code_add) || string.IsNullOrEmpty(client_code_id))
            {
                return;
            }
            dateTimePicker1.Value = DateTime.Parse("2020-01-01");
            client_name.Text = client_code_add;
           
        }

 

   
    }
}
