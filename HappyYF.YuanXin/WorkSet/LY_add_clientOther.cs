using System;
using System.Windows.Forms;
using HappyYF.Infrastructure.Repositories;
using System.Text.RegularExpressions;
using DataGridFilter;
using System.Data;
using System.Data.SqlClient;
namespace HappyYF.YuanXin.WorkSet
{
    public partial class LY_add_clientOther : Form
    {
       
  
        public LY_add_clientOther()
        {
            InitializeComponent();
        }




        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(client_name.Text))
            {
                MessageBox.Show("名称不能为空，请重新输入");
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
                MessageBox.Show("到款日期格式错误，请重新输入");
                this.dateTimePicker1.Focus();
                return;
            }
            DataTable dt = null;
            string sql = @"select  id  from [ly_sales_client_other] where salesclient_name='" + client_name.Text + "' order by id asc";
            using (SqlConnection connection = new SqlConnection(SQLDatabase.Connectstring))
            {

                SqlDataAdapter adapter = new SqlDataAdapter(sql, connection);
                DataSet ds = new DataSet();
                adapter.Fill(ds);
                dt = ds.Tables[0];
            }
            if(dt.Rows.Count>0)
            {
                MessageBox.Show("名字重复"); 
                return;
            }

            string sqlInsert = @"INSERT INTO [ly_sales_client_other]
                                ([salesclient_code]
                                ,[salesclient_name]
                                ,[start_yingshou_money]
                                ,[strart_yushou_money]
                                ,[start_time])
                                 VALUES
           ( '"+ GetSalesclientCode() + "'  ,'"+client_name.Text+"',"+ textBox2.Text+","+ invoice_money.Text+",'"+ dateTimePicker1.Value + "')"; 
            using (SqlConnection con = new SqlConnection(SQLDatabase.Connectstring))
            {

                using (SqlCommand cmd = new SqlCommand(sqlInsert, con))
                {

                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            this.DialogResult = DialogResult.OK;
            this.Close();

        }
        private string GetSalesclientCode()
        {


            SqlConnection sqlConnection1 = new SqlConnection(SQLDatabase.Connectstring);
            SqlCommand cmd = new SqlCommand();

            string Salesclientcode = "";

            cmd.Parameters.Add("@Client_mode", SqlDbType.VarChar);
            cmd.Parameters["@Client_mode"].Value = "GR";

           
            cmd.CommandText = "LY_GetMax_SalesclientCodeOther";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = sqlConnection1;

            sqlConnection1.Open();
            Salesclientcode = cmd.ExecuteScalar().ToString();
            sqlConnection1.Close();



            return Salesclientcode;



        }

        private void LY_add_paymoeny_Load(object sender, EventArgs e)
        {
            this.dateTimePicker1.Value = DateTime.Parse("2020-01-01");
        }



   
    }
}
