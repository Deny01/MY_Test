using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using HappyYF.Infrastructure.Repositories;

namespace HappyYF.YuanXin.WorkSet
{
    public partial class LY_Salescontract_Return : Form
    {
        private string nowclient_code;

        public string Nowclient_code
        {
            get { return nowclient_code; }
            set { nowclient_code = value; }
        }

        private string nowbill_code;

        public string Nowbill_code
        {
            get { return nowbill_code; }
            set { nowbill_code = value; }
        }

        public LY_Salescontract_Return()
        {
            InitializeComponent();
        }



        private void LY_Salescontract_Return_Load(object sender, EventArgs e)
        {
            this.ly_sales_receive_returnSumTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            this.ly_sales_receive_returnBindTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;

            this.ly_sales_receive_returnSumTableAdapter.Fill(this.lYSalseMange2.ly_sales_receive_returnSum, this.nowclient_code);
            this.ly_sales_receive_returnBindTableAdapter.Fill(this.lYSalseMange2.ly_sales_receive_returnBind, this.nowclient_code);



        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if (null == this.ly_sales_receive_returnSumDataGridView.CurrentRow)
            {
                return;
            }

            string message = "确定导出数据吗？";
            string caption = "提示...";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result;



            result = MessageBox.Show(message, caption, buttons,
            MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

            if (result == DialogResult.Yes)
            {
                //NewFrm.Show(this);


                MakeSalesContract(nowclient_code, nowbill_code);
                this.DialogResult = DialogResult.OK;
                this.Close();

                //this.ly_sales_contract_main_forbusinessTableAdapter.Fill(this.lYSalseMange.ly_sales_contract_main_forbusiness, this.nowcontractCode);
                //this.tabControl1.SelectedIndex = 2;

                //NewFrm.Hide(this);

            }


        }

        private void MakeSalesContract(string clientcode, string billcode)
        {




            SqlConnection sqlConnection1 = new SqlConnection(SQLDatabase.Connectstring);
            SqlCommand cmd = new SqlCommand();



            cmd.Parameters.Add("@salesclient_code", SqlDbType.VarChar);
            cmd.Parameters["@salesclient_code"].Value = clientcode;

            cmd.Parameters.Add("@contract_inner_code", SqlDbType.VarChar);
            cmd.Parameters["@contract_inner_code"].Value = billcode;

            //cmd.Parameters.Add("@warehousename", SqlDbType.VarChar);
            //cmd.Parameters["@warehousename"].Value = comboBox2.SelectedValue;

            //string outNum = GetMaxOutNum();
            //cmd.Parameters.Add("@out_number", SqlDbType.VarChar);
            //cmd.Parameters["@out_number"].Value = outNum;

            //cmd.Parameters.Add("@inputman", SqlDbType.VarChar);
            //cmd.Parameters["@inputman"].Value = SQLDatabase.nowUserName();

            //cmd.Parameters.Add("@innercode", SqlDbType.VarChar);
            //cmd.Parameters["@innercode"].Value = innerCode;









            cmd.CommandText = "LY_return_to_contract";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = sqlConnection1;



            sqlConnection1.Open();

            try
            {
                NewFrm.Show(this);

                Thread.Sleep(100);
               
                cmd.ExecuteNonQuery();
                NewFrm.Hide(this);
                MessageBox.Show("导出成功,请补充合同相关数据", "注意");
            }
            catch (SqlException sqle)
            {
                MessageBox.Show("导出失败,请检验相关数据", "注意");
                NewFrm.Hide(this);
            }
            finally
            {
                sqlConnection1.Close();

            }







        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void ly_sales_receive_returnBindDataGridView_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            /////////////////////////////////////////////////////////

            DataGridView dgv = sender as DataGridView;
            if (null == dgv.CurrentRow) return;

            if ("check_flag" == dgv.CurrentCell.OwningColumn.Name)
            {
                int nowId = int.Parse(dgv.CurrentRow.Cells["return_id"].Value.ToString());
                string updstr;
                if (nowId > 0)
                {

                    if ("True" == dgv.CurrentRow.Cells["check_flag"].Value.ToString())
                    {
                        updstr = " update ly_sales_receive_itemDetail  " +
                           "  set check_flag=0  where  id=" + nowId;
                    }
                    else
                    {
                        updstr = " update ly_sales_receive_itemDetail  " +
                          "  set check_flag=1 where  id=" + nowId;
                    }

                }
                else
                {

                    if ("True" == dgv.CurrentRow.Cells["check_flag"].Value.ToString())
                    {
                        updstr = " update ly_sales_receive_itemDetail_child  " +
                           "  set check_flag=0  where  id=" + (0 - nowId);
                    }
                    else
                    {
                        updstr = " update ly_sales_receive_itemDetail_child  " +
                          "  set check_flag=1 where  id=" + (0 - nowId);
                    }


                }

                SqlConnection sqlConnection1 = new SqlConnection(SQLDatabase.Connectstring);
                SqlCommand cmd = new SqlCommand();

                cmd.CommandText = updstr;
                cmd.CommandType = CommandType.Text;
                cmd.Connection = sqlConnection1;

               

              

                    sqlConnection1.Open();
                    try
                    {

                        cmd.ExecuteNonQuery();



                       



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

            this.ly_sales_receive_returnSumTableAdapter.Fill(this.lYSalseMange2.ly_sales_receive_returnSum, this.nowclient_code);
            this.ly_sales_receive_returnBindTableAdapter.Fill(this.lYSalseMange2.ly_sales_receive_returnBind, this.nowclient_code);



            return;

        }








    }
}
