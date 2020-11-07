using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Transactions;
using HappyYF.Infrastructure.Repositories;
using DataGridFilter;

 

namespace HappyYF.YuanXin.WorkSet
{
    public partial class LY_SalesStyle_Info : Form
    {
        public LY_SalesStyle_Info()
        {
            InitializeComponent();
        }

        private void ly_salesregionBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.ly_sales_contract_styleBindingSource.EndEdit();

            if (ly_sales_contract_styleDataGridView.CurrentRow == null)
            {
                MessageBox.Show("请选择要保存的行");return;
            }
            string sql = "select id from  ly_sales_contract_style where style_name='"+ ly_sales_contract_styleDataGridView.CurrentRow.Cells["属性名称"].Value.ToString()+ "'";
            DataTable dt = null;
            using (SqlConnection connection = new SqlConnection(SQLDatabase.Connectstring))
            {

                SqlDataAdapter adapter = new SqlDataAdapter(sql, connection);
                DataSet ds = new DataSet();
                adapter.Fill(ds);
                dt = ds.Tables[0];
            }
            if (dt.Rows.Count > 0)
            {
                MessageBox.Show("该属性名称已经存在"); return;
            }

            this.Validate();  
            this.ly_sales_contract_styleBindingSource.EndEdit();
            this.ly_sales_contract_styleTableAdapter.Update(this.lYSalseMange.ly_sales_contract_style);

        }

        private void LY_Salesregion_Mange_Load(object sender, EventArgs e)
        {
            this.ly_sales_contract_styleTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;    
            this.ly_sales_contract_styleTableAdapter.Fill(this.lYSalseMange.ly_sales_contract_style);
            
          


        }

        private void LY_Salesregion_Mange_FormClosing(object sender, FormClosingEventArgs e)
        {
            //this.Validate();
            //this.ly_sales_contract_styleBindingSource.EndEdit();
            //this.ly_sales_contract_styleTableAdapter.Update(this.lYSalseMange.ly_sales_contract_style);
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if (ly_sales_contract_styleDataGridView.CurrentRow == null)
            {
                MessageBox.Show("请选择要删除的行"); return;
            }
            string sql = "delete  from  ly_sales_contract_style where id= " + ly_sales_contract_styleDataGridView.CurrentRow.Cells["id"].Value.ToString() + " ";

            SqlConnection sqlConnection1 = new SqlConnection(SQLDatabase.Connectstring);
            SqlCommand cmd = new SqlCommand();

            cmd.CommandText = sql;
            cmd.CommandType = CommandType.Text;
            cmd.Connection = sqlConnection1;

            int temp = 0;

            using (TransactionScope scope = new TransactionScope())
            {

                sqlConnection1.Open();
                try
                {

                    cmd.ExecuteNonQuery();
                    scope.Complete();
                    temp = 1;


                }
                catch (SqlException sqle)
                {


                    MessageBox.Show(sqle.Message.Split('*')[0]);
                }


                finally
                {
                    sqlConnection1.Close();


                }
            }
            if (1 == temp)
            {

                this.ly_sales_contract_styleTableAdapter.Fill(this.lYSalseMange.ly_sales_contract_style);

            }
            else
            {
                MessageBox.Show("删除失败"); return;
            }
        }
    }
}
