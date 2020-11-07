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
    public partial class LY_Salesborror_Add : Form
    {
        public string runmode;

        public string borrowcode;
        public string salesclientcode;
        public string contract_code;
        public string borrowstyle;


        public LY_Salesborror_Add()
        {
            InitializeComponent();
        }

        private void SaveData()
        {



            this.ly_sales_borrow_singleBindingSource.EndEdit();
            this.ly_sales_borrow_singleTableAdapter.Update(this.lYSalseMange.ly_sales_borrow_single);
            
            this.ly_sales_borrow_singleTableAdapter.Fill(this.lYSalseMange.ly_sales_borrow_single, this .borrowcode );

           
        
        }

        private void LY_MaterialAdd_Load(object sender, EventArgs e)
        {
            this.ly_sales_borrow_styleTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            this.ly_sales_borrow_styleTableAdapter.Fill(this.lYSalseMange.ly_sales_borrow_style);

            this.ly_sales_clientTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            this.ly_sales_clientTableAdapter.Fill(this.lYSalseMange.ly_sales_client);


           



            this.ly_sales_borrow_singleTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            

         
            
            if ("增加" == runmode)
            {

              
                    this.Text = "新增借用单";



                    this.ly_sales_borrow_singleBindingSource.AddNew();

                    this.借用日期DateTimePicker.Value = SQLDatabase.GetNowdate();

                    this.开始日期DateTimePicker.Value = SQLDatabase.GetNowdate();
                    this.返还日期DateTimePicker.Value = SQLDatabase.GetNowdate().AddMonths(3);

                    this.录入日期DateTimePicker1.Value = SQLDatabase.GetNowdate();

                    this.borrowcode  = GetContractborrow_Code();
                    this.内部编码TextBox.Text = this.borrowcode;
                    this.合同编码TextBox.Text = this.contract_code;

                    this.客户编码ComboBox.SelectedValue  = this.salesclientcode;
                    //this.录入人TextBox1.Text = SQLDatabase.nowUserName();

                    this.借用类别ComboBox.SelectedValue = this.borrowstyle;
                    this.借用目的TextBox.Text = "替换";
                    //SaveData();
                 

                    //this.ly_sales_contract_terms_forcontractTableAdapter.Fill(this.lYSalseMange.ly_sales_contract_terms_forcontract, this.contractinner_code);


                   // this.operatorTextBox.Text = SQLDatabase.nowUserName();

                 
            }
            else
            {




                this.ly_sales_borrow_singleTableAdapter.Fill(this.lYSalseMange.ly_sales_borrow_single, this.borrowcode);
              
                //this.录入人TextBox1.Text = SQLDatabase.nowUserName();
              
              
                this.Text = "修改借用单信息";
               
              
            
            }

       
        }

        private string GetContractborrow_Code()
        {
           

            SqlConnection sqlConnection1 = new SqlConnection(SQLDatabase.Connectstring);
            SqlCommand cmd = new SqlCommand();

            string contractborrowCode = "";

            //cmd.Parameters.Add("@Client_mode", SqlDbType.VarChar);
            //cmd.Parameters["@Client_mode"].Value = "YY";

            //cmd.Parameters.Add("@loanId", SqlDbType.Int);
            //cmd.Parameters["@loanId"].Value = loanId;

            //cmd.Parameters.Add("@item_name", SqlDbType.VarChar);
            //cmd.Parameters["@item_name"].Value = item_name;


            cmd.CommandText = "LY_GetMax_ContractborrowCode";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = sqlConnection1;

            sqlConnection1.Open();
            contractborrowCode = cmd.ExecuteScalar().ToString();
            sqlConnection1.Close();



            return contractborrowCode;


        
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

            if ("增加" == runmode)
            {
                this.borrowcode = GetContractborrow_Code();
                this.内部编码TextBox.Text = this.borrowcode;
            }

            if ("01" == this.borrowstyle)
            {
                if (string.IsNullOrEmpty(this.合同编码TextBox.Text))
                {
                    MessageBox.Show("必须输入业务编号...", "注意");
                    return;
                }

                if (this.返还日期DateTimePicker.Value <= this .开始日期DateTimePicker.Value)
                {
                    MessageBox.Show("返还日期不能小于开始日期...", "注意");
                    return;
                }
            }


            //---------------------------------------------------------------------
            string  clcode = this.客户编码ComboBox.SelectedValue.ToString();
            string sql = "select in_use from ly_sales_client where salesclient_code='"+ clcode + "'";
            DataTable dt = null;
            using (SqlConnection connection = new SqlConnection(SQLDatabase.Connectstring))
            {

                SqlDataAdapter adapter = new SqlDataAdapter(sql, connection);
                DataSet ds = new DataSet();
                adapter.Fill(ds);
                dt = ds.Tables[0];
            }
            if (dt.Rows[0][0].ToString() == "1")
            {
                MessageBox.Show("已经停用...", "注意");
                return;
            }
            //---------------------------------------------------------------------


            this.ly_sales_borrow_singleBindingSource.EndEdit();

            try
            {



                //if (string.IsNullOrEmpty(this.salesperson_codeComboBox.Text))
                //{
                //    MessageBox.Show("必须选择营业员...", "注意");
                //    return;

                //}

                this.Validate();
                this.ly_sales_borrow_singleTableAdapter.Update(this.lYSalseMange.ly_sales_borrow_single);
                //this.salesperson_code = this.salesperson_codeComboBox.SelectedValue.ToString() + ":" + this.salesperson_codeComboBox.Text;
                this.salesclientcode = this.客户编码ComboBox.SelectedValue .ToString ();
                this.borrowcode = this.内部编码TextBox.Text;



                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (SqlException sqle)
            {

                MessageBox.Show("借用单号重复...", "注意");
               // MessageBox.Show(sqle.Message, "注意");
            }
        }

      
      

    

      

        private void 借用类别Label_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (SQLDatabase.CheckHaveRight(SQLDatabase.NowUserID, "营业综合信息"))
            {




                LY_SalesBorrowStyle_Info queryForm = new LY_SalesBorrowStyle_Info();

                // queryForm.Cardnumber = this.xD_Sel_SellBalanceDataGridView.CurrentRow.Cells["编号"].Value.ToString();
                queryForm.StartPosition = FormStartPosition.CenterParent;
                queryForm.ShowDialog();

                this.ly_sales_borrow_styleTableAdapter.Fill(this.lYSalseMange.ly_sales_borrow_style);
            }
        }

       

       
     

     

       
       

       
     

       
    }
}
