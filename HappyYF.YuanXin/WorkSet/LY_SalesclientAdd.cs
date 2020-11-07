using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HappyYF.Infrastructure.Repositories;
using Project_Manager.AppServices;

namespace HappyYF.YuanXin.WorkSet
{
    public partial class LY_SalesclientAdd : Form
    {
        public string runmode;
        
        public string salesclient_code;
        public string salesperson_code;
        public string client_name;

        public LY_SalesclientAdd()
        {
            InitializeComponent();
        }

        private void LY_MaterialAdd_Load(object sender, EventArgs e)
        {
            this.ly_salesregionTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            this.ly_salesregionTableAdapter.Fill(this.lYSalseMange.ly_salesregion);
            this.ly_sales_gift_styleTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            this.ly_sales_gift_styleTableAdapter.Fill(this.lYSalseMange2.ly_sales_gift_style);
            this.ly_sales_contract_styleTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            this.ly_sales_contract_styleTableAdapter.Fill(this.lYSalseMange.ly_sales_contract_style);
            this.ly_salesregion_secondTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            this.ly_salesregion_secondTableAdapter.Fill(this.lYSalseMange.ly_salesregion_second);
            this.ly_salespersonTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            this.ly_salespersonTableAdapter.Fill(this.lYSalseMange.ly_salesperson);

            this.ly_sales_clientTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
         
            if (SQLDatabase.CheckHaveRight(SQLDatabase.NowUserID, "营业综合信息"))
            {
                this.lysalespersonBindingSource.Filter = "";
            }
            else
            {
                this.lysalespersonBindingSource.Filter = "编码='" + SQLDatabase.NowUserID + "'";
            }

        
            this.ly_sales_clientTableAdapter.Fill(this.lYSalseMange.ly_sales_client);
           
          

           
            
            if ("增加" == runmode)
            {

              
                    this.Text = "新增客户信息";




                    this.ly_sales_clientBindingSource.AddNew();

                    this.salesclient_code = GetSalesclientCode();

                    this.客户编码TextBox.Text = this.salesclient_code;

                this.operatorTextBox.Text = SQLDatabase.nowUserName();

                this.sys_dateDateTimePicker.Value = SQLDatabase.GetNowdate();

                 
            }
            else
            {
                 

                this.ly_sales_clientTableAdapter.Fill(this.lYSalseMange.ly_sales_client);
                this.ly_sales_clientBindingSource.Filter = "客户编码='" + salesclient_code + "'";

                this.Text = "修改客户信息";
               
              
            
            }

       
        }

        private string GetSalesclientCode()
        {
           

            SqlConnection sqlConnection1 = new SqlConnection(SQLDatabase.Connectstring);
            SqlCommand cmd = new SqlCommand();

            string Salesclientcode = "";

            cmd.Parameters.Add("@Client_mode", SqlDbType.VarChar);
            cmd.Parameters["@Client_mode"].Value = "YY";

            //cmd.Parameters.Add("@loanId", SqlDbType.Int);
            //cmd.Parameters["@loanId"].Value = loanId;

            //cmd.Parameters.Add("@item_name", SqlDbType.VarChar);
            //cmd.Parameters["@item_name"].Value = item_name;


            cmd.CommandText = "LY_GetMax_SalesclientCode";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = sqlConnection1;

            sqlConnection1.Open();
            Salesclientcode = cmd.ExecuteScalar().ToString();
            sqlConnection1.Close();



            return Salesclientcode;


        
        }

        //private void decimalTextBox_Validating(object sender, CancelEventArgs e)
        //{
        //    TextBox tb = sender as TextBox;
        //    tb.Text = tb.Text.Replace(" ", "");
        //    tb.DataBindings[0].Parse += new ConvertEventHandler(bind_Parse);
        //}

        //void bind_Parse(object sender, ConvertEventArgs e)//控件值更改时发生
        //{
        //    if (e.Value.ToString() == "")
        //        e.Value = DBNull.Value;
        //}

        private void button1_Click(object sender, EventArgs e)
        {

            this.ly_sales_clientBindingSource.EndEdit();

            try
            {
                if (string.IsNullOrEmpty(this.客户名称TextBox.Text))
                {
                    {
                        MessageBox.Show("必须输入客户名称...", "注意");
                        return;

                    }
                }
                if (string.IsNullOrEmpty(this.salesclient_en_nameTextBox.Text))
                {
                    {
                        MessageBox.Show("必须输入客户英文名称...", "注意");
                        return;

                    }
                }


                if (in_useCheckBox.Checked)

                {
                    DataTable dt_client = null;
                    string sql_client = @"select start_time from ly_sales_client  where salesclient_code='" + salesclient_code + "'";
                    using (SqlConnection connection = new SqlConnection(SQLDatabase.Connectstring))
                    {

                        SqlDataAdapter adapter = new SqlDataAdapter(sql_client, connection);
                        DataSet ds = new DataSet();
                        adapter.Fill(ds);
                        dt_client = ds.Tables[0];
                    }
                    if (dt_client.Rows.Count > 0)
                    {
                        if (!string.IsNullOrEmpty(dt_client.Rows[0][0].ToString()))
                        {
                            MessageBox.Show("该客户有应收应付账款...", "注意");
                            return;
                        }
                    }
                }
             
                if (!string.IsNullOrEmpty(client_name))
                {
                    if (客户名称TextBox.Text != client_name)
                    {
                        if (!SQLDatabase.CheckHaveRight(SQLDatabase.NowUserID, "设置客户期初应收"))
                        {
                            MessageBox.Show("该客户有应收应付账款...", "注意");
                            return;
                        }
                    }
                }
                if (string.IsNullOrEmpty(this.salesperson_codeComboBox.Text))
                {
                    MessageBox.Show("必须选择营业员...", "注意");
                    return;
                
                }
                if (string.IsNullOrEmpty(this.comboBox1.Text))
                {
                    MessageBox.Show("必须选择区域二...", "注意");
                    return;

                }
                this.ly_sales_clientTableAdapter.Update(this.lYSalseMange.ly_sales_client);
                this.salesperson_code = this.salesperson_codeComboBox.SelectedValue.ToString() + ":" + this.salesperson_codeComboBox.Text;
                this.salesclient_code = this.客户编码TextBox.Text;
               
               
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (SqlException sqle)
            {
                
                //MessageBox.Show("物资编号重复...", "注意");
                MessageBox.Show(sqle.Message , "注意");
            }
        }

        private void 名称TextBox_Enter(object sender, EventArgs e)
        {
            AppSet appSet = AppSet.Load();
            //InputLanguage.CurrentInputLanguage = InputLanguage.InstalledInputLanguages[2];
            if (0 < appSet.KeyboardInputIndex)
                InputLanguage.CurrentInputLanguage = InputLanguage.InstalledInputLanguages[appSet.KeyboardInputIndex];
        }

        private void 名称TextBox_Leave(object sender, EventArgs e)
        {
            InputLanguage.CurrentInputLanguage = InputLanguage.DefaultInputLanguage;
        }

        private void ly_sales_clientBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.ly_sales_clientBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.lYSalseMange);

        }

        private void salesregionSec_codeLabel_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            LY_SalesregionSec__Mange queryForm = new LY_SalesregionSec__Mange();


            queryForm.StartPosition = FormStartPosition.CenterParent;
            queryForm.ShowDialog();



            this.ly_salesregion_secondTableAdapter.Fill(this.lYSalseMange.ly_salesregion_second);
        }

        private void 行业编码Label_Click(object sender, EventArgs e)
        {

        }

        private void 行业编码TextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void 行业编码Label_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (SQLDatabase.CheckHaveRight(SQLDatabase.NowUserID, "营业综合信息"))
            {
                 
                LY_SalesStyle_Info queryForm = new LY_SalesStyle_Info();

                // queryForm.Cardnumber = this.xD_Sel_SellBalanceDataGridView.CurrentRow.Cells["编号"].Value.ToString();
                queryForm.StartPosition = FormStartPosition.CenterParent;
                queryForm.ShowDialog();

                this.ly_sales_contract_styleTableAdapter.Fill(this.lYSalseMange.ly_sales_contract_style);
            }
        }



        //private void 备注TextBox_KeyPress(object sender, KeyPressEventArgs e)
        //{
        //    if (e.KeyChar.ToString() == "\r")
        //    {
        //        e.Handled = true;
        //        this.物资编号TextBox.Focus();

        //    }
        //}




        //private void fir_styleLabel_MouseDoubleClick(object sender, MouseEventArgs e)
        //{
        //    LY_Firststyle_Set queryForm = new LY_Firststyle_Set();


        //    queryForm.StartPosition = FormStartPosition.CenterParent;
        //    queryForm.ShowDialog();



        //    this.ly_firststyle_setTableAdapter.Fill(this.lYMaterialMange.ly_firststyle_set);
        //}

        //private void sec_styleLabel_MouseDoubleClick(object sender, MouseEventArgs e)
        //{
        //    LY_Secondstyle_Set queryForm = new LY_Secondstyle_Set();

        //    string firstyle_Code = this.fir_styleComboBox.SelectedValue.ToString();

        //    queryForm.firstyle_Code = firstyle_Code;

        //    queryForm.StartPosition = FormStartPosition.CenterParent;
        //    queryForm.ShowDialog();



        //    this.ly_secondstyle_setTableAdapter.Fill(this.lYMaterialMange.ly_secondstyle_set);
        //    this.lysecondstylesetBindingSource.Filter = "firststyleCode ='" + firstyle_Code+"'";
        //}

        //private void fir_styleComboBox_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    string firstyle_Code = this.fir_styleComboBox.SelectedValue.ToString();
        //    this.lysecondstylesetBindingSource.Filter = "firststyleCode ='" + firstyle_Code + "'";
        //}










    }
}
