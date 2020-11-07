using System;
using System.Windows.Forms;
using HappyYF.Infrastructure.Repositories;
using System.Text.RegularExpressions;
using DataGridFilter;
using System.Data;
using System.Data.SqlClient;
namespace HappyYF.YuanXin.WorkSet
{
    public partial class LY_add_invoice : Form
    {
        public string invoice_code_add;
        public LY_add_invoice()
        {
            InitializeComponent();
        }




        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(client_code.Text))
            {
                MessageBox.Show("客户不能为空！"); return;
            }
            if (string.IsNullOrEmpty(supplier_financial_code.Text))
            {
                MessageBox.Show("结算编码不能为空！"); return;
            }
            if (!(Regex.IsMatch(this.invoice_rate.Text, @"^([-]{1}[0-9]+(.[0-9]{2})?$)|([0-9]+(.[0-9]{2})?$)"))) // ^\+?[1-9][0-9]*$
            {
                MessageBox.Show("数字格式错误，请重新输入");
                this.invoice_rate.Focus();
                return;
            }
            if (string.IsNullOrEmpty(invoice_code.Text))
            {
                MessageBox.Show("发票号不能为空！"); invoice_code.Focus(); return;
            }
            if (!(Regex.IsMatch(this.invoice_money.Text, @"^([-]{1}[0-9]+(.[0-9]{2})?$)|([0-9]+(.[0-9]{2})?$)"))) // ^\+?[1-9][0-9]*$
            {
                MessageBox.Show("数字格式错误，请重新输入");
                this.invoice_money.Focus();
                return;
            }
            if (string.IsNullOrEmpty(dateTimePicker1.Text))
            {
                MessageBox.Show("开票日期格式错误，请重新输入");
                this.dateTimePicker1.Focus();
                return;
            }
            DataTable dt = null;
            string check = "select count(1) from ly_invoice where invoice_code='" + invoice_code.Text + "'";
            using (SqlConnection connection = new SqlConnection(SQLDatabase.Connectstring))
            {

                SqlDataAdapter adapter = new SqlDataAdapter(check, connection);
                DataSet ds = new DataSet();
                adapter.Fill(ds);
                dt = ds.Tables[0];
            }
            if (dt.Rows[0][0].ToString() == "0")
            {
                string rs = radioButton1.Checked ? "专票" : "普票";
                string sql = @"insert into ly_invoice (client_code,client_name,invoice_code,invoice_date ,invoice_money,invoice_rate,sava_people,save_time ,invoice_type,supplier_financial_code)
                               values ('" + client_code.Text + "' ,'" + client_name.Text + "' ,'" + invoice_code.Text + "' ,'" + dateTimePicker1.Text + "','" +
                               invoice_money.Text + "','" + invoice_rate.Text + "','" + SQLDatabase.nowUserName() + "','" + SQLDatabase.GetNowtime() + "', '" + rs + "', '" + supplier_financial_code.Text + "')";
                using (SqlConnection con = new SqlConnection(SQLDatabase.Connectstring))
                {

                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {

                        con.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
                invoice_code_add = invoice_code.Text;
                this.DialogResult = DialogResult.OK;
                this.Close();

            }
            else
            {
                MessageBox.Show("发票号已经存在，请重新输入"); return;
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            string sel = "select  supplier_code as 客户编码,supplier_name  客户名称,supplier_financial_code as 结算编码  from ly_supplier_list ";
            QueryForm queryForm = new QueryForm();
            queryForm.Sel = sel;
            queryForm.Constr = SQLDatabase.Connectstring;
            queryForm.ShowDialog();

            if (queryForm.Result != "")
            {
                client_code.Text = queryForm.Result;
                client_name.Text = queryForm.Result1;
                supplier_financial_code.Text = queryForm.Result2;
            }
        }

 











    }
}
