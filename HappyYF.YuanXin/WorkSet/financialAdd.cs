using DataGridFilter;
using HappyYF.Infrastructure.Repositories;
using System;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace HappyYF.YuanXin.WorkSet
{
    public partial class financialAdd : Form
    {
        public string runmode;
        public string type_code;

        public financialAdd()
        {
            InitializeComponent();
        }

        private void LY_MaterialAdd_Load(object sender, EventArgs e)
        {
            if ("增加" == runmode)
            {
                this.Text = "新增信息";
            }
            else
            {
                this.Text = "修改信息";


                string sql = @"select * from  t_financial_type where  id_code='" + type_code + "' ";

                DataTable dt = null;
                using (SqlConnection connection = new SqlConnection(SQLDatabase.Connectstring))
                {

                    SqlDataAdapter adapter = new SqlDataAdapter(sql, connection);
                    DataSet ds = new DataSet();
                    adapter.Fill(ds);
                    dt = ds.Tables[0];
                }

                if (dt.Rows.Count == 1)
                {
                    this.物资编号TextBox.Text = dt.Rows[0]["id_code"].ToString();
                    this.名称TextBox.Text = dt.Rows[0]["name"].ToString();
                    this.textBox2.Text=dt.Rows[0]["en_name"].ToString();
                    this.textBox1.Text = dt.Rows[0]["remark"].ToString();

                }


                this.物资编号TextBox.ReadOnly = true;
            }
        }




        private void button1_Click(object sender, EventArgs e)
        {
            this.物资编号TextBox.Text = this.物资编号TextBox.Text.Replace(" ", "");

            if (string.IsNullOrEmpty(this.物资编号TextBox.Text))
            {
                MessageBox.Show("编号不能为空...", "注意");
                return;
            }


            if (string.IsNullOrEmpty(this.名称TextBox.Text))
            {
                MessageBox.Show("中文名称不能为空...", "注意");
                return;
            }
            if (string.IsNullOrEmpty(this.textBox2.Text))
            {
                MessageBox.Show("英文名称不能为空...", "注意");
                return;
            }

            if ("增加" == runmode)
            {

                using (SqlConnection con = new SqlConnection(SQLDatabase.Connectstring))
                {
                    string sql = "select count(1) from  t_financial_type where  id_code='" + this.物资编号TextBox.Text + "'";
                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {

                        con.Open();
                        int k = Convert.ToInt32(cmd.ExecuteScalar());
                        if (k > 0)
                        {
                            MessageBox.Show("已有该编号", "注意");
                            return;
                        }
                    }
                }
                try
                {
                    using (SqlConnection con = new SqlConnection(SQLDatabase.Connectstring))
                    {
                        string sqlIn = @"insert into  t_financial_type (id_code,name,en_name,remark) values('" + this.物资编号TextBox.Text + "', '" + this.名称TextBox.Text + "', '" + this.textBox2.Text + "', '" + textBox1.Text + "')";

                        using (SqlCommand cmd = new SqlCommand(sqlIn, con))
                        {

                            con.Open();
                            int k = cmd.ExecuteNonQuery();
                            if (k > 0)
                            {
                                this.DialogResult = DialogResult.OK;
                                this.Close();
                            }
                        }
                    }
                }
                catch (SqlException sqle)
                {
                    MessageBox.Show(sqle.Message, "注意");
                }

            }


            else
            {
                if (string.IsNullOrEmpty(type_code)) return;
                string sqlUp = @"update  t_financial_type set  name = '" + this.名称TextBox.Text + "' , en_name = '" + this.textBox2.Text + "',remark = '" + this.textBox1.Text + "' where id_code ='" + type_code + "' ";
                using (SqlConnection con = new SqlConnection(SQLDatabase.Connectstring))
                {
                    using (SqlCommand cmd = new SqlCommand(sqlUp, con))
                    {

                        con.Open();
                        int k = cmd.ExecuteNonQuery();
                        if (k > 0)
                        {
                            this.DialogResult = DialogResult.OK;
                            this.Close();
                        }
                    }
                }
            }
        }
 

        private void 物资编号TextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar.ToString() == "\r")
            {
                e.Handled = true;
                this.名称TextBox.Focus();

            }
        }

        private void 名称TextBox_KeyPress(object sender, KeyPressEventArgs e)
        {

        }




    }
}