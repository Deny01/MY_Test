using System;
using System.Windows.Forms;
using HappyYF.Infrastructure.Repositories;
using System.Text.RegularExpressions;
using DataGridFilter;
using System.Data;
using System.Data.SqlClient;
namespace HappyYF.YuanXin.WorkSet
{
    public partial class LY_add_actualpayment_NP : Form
    {
        public string nowsuppilercode;
        public LY_add_actualpayment_NP()
        {
            InitializeComponent(); 
        }




        private void button1_Click(object sender, EventArgs e)
        {
            //if (string.IsNullOrEmpty(client_code.Text))
            //{
            //    MessageBox.Show("客户不能为空！"); return;
            //}
            //if (string.IsNullOrEmpty(supplier_financial_code.Text))
            //{
            //    MessageBox.Show("结算编码不能为空！"); return;
            //}
           
           
            if (!(Regex.IsMatch(this.invoice_money.Text, @"^([-]{1}[0-9]+(.[0-9]{2})?$)|([0-9]+(.[0-9]{2})?$)"))) // ^\+?[1-9][0-9]*$
            {
                MessageBox.Show("数字格式错误，请重新输入");
                this.invoice_money.Focus();
                return;
            }
            if (string.IsNullOrEmpty(dateTimePicker1.Text))
            {
                MessageBox.Show("支付日期格式错误，请重新输入");
                this.dateTimePicker1.Focus();
                return;
            }
           
           
               
                string sql = @"insert into ly_actualpayment_NP (supplier_code,pay_date,prepaymoney,pay_people ,remark)
                               values ('" + this.nowsuppilercode + "' ,'" + dateTimePicker1.Text + "' ,'" + invoice_money.Text + "' ,'" + supplier_financial_code.Text + "','" + invoice_code.Text + "')";
            //invoice_money.Text + "','" + invoice_rate.Text + "','" + SQLDatabase.nowUserName() + "','" + SQLDatabase.GetNowtime() + "', '" + rs + "', '" + supplier_financial_code.Text + "')";
                using (SqlConnection con = new SqlConnection(SQLDatabase.Connectstring))
                {

                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {

                        con.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
                //invoice_code_add = invoice_code.Text;
                this.DialogResult = DialogResult.OK;
                this.Close();

          

        }

        private void LY_add_Prepayment_NP_Load(object sender, EventArgs e)
        {
            this.supplier_financial_code.Text = SQLDatabase.nowUserName();
        }
    }
}
