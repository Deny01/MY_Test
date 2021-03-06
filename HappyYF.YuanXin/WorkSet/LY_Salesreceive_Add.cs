﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HappyYF.Infrastructure.Repositories;
using HappyYF.YuanXin.Data;
using DataGridFilter;
using Project_Manager.AppServices;

namespace HappyYF.YuanXin.WorkSet
{
    public partial class LY_Salesreceive_Add : Form
    {
        public string runmode;

        public string receive_code;
        public string salesclientcode;


        public LY_Salesreceive_Add()
        {
            InitializeComponent();
        }

        private void SaveData()
        {



            this.ly_sales_receive_singleBindingSource.EndEdit();
            this.ly_sales_receive_singleTableAdapter.Update(this.lYSalseMange2.ly_sales_receive_single);
            //this.ly_sales_contract_mainSingleTableAdapter.Fill(this.lYSalseMange.ly_sales_contract_mainSingle, this.contractinner_code);

          
        
        }

        private void LY_MaterialAdd_Load(object sender, EventArgs e)
        {
           
           
           

            this.ly_express_companyTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            this.ly_express_companyTableAdapter.Fill(this.lYSalseMange.ly_express_company);

            this.ly_sales_clientTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            this.ly_sales_clientTableAdapter.Fill(this.lYSalseMange.ly_sales_client);





            this.ly_sales_receive_singleTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            //this.ly_sales_receive_singleTableAdapter.Fill(this.lYSalseMange2.ly_sales_receive_single);
          
           
            
            if ("增加" == runmode)
            {

              
                    this.Text = "新增收件业务";




                    this.ly_sales_receive_singleBindingSource.AddNew();



                    this.receive_code = GetReceive_Code();
                    this.合同编码TextBox.Text = this.receive_code;
                    this.维修部门TextBox.Text = "公司维修";

                    //this.客户编码ComboBox.SelectedValue  = this.salesclientcode;

                    this.收件日期DateTimePicker.Value = SQLDatabase.GetNowdate();

                    this.录入日期DateTimePicker.Value = SQLDatabase.GetNowdate();

                    this.指定交货日期DateTimePicker.Value = SQLDatabase.GetNowdate().AddDays(5);
                    this.录入人TextBox1.Text = SQLDatabase.nowUserName();
                   // SaveData();
                 

                   // this.ly_sales_contract_terms_forcontractTableAdapter.Fill(this.lYSalseMange.ly_sales_contract_terms_forcontract, this.contractinner_code);


                   // this.operatorTextBox.Text = SQLDatabase.nowUserName();

                 
            }
            else if ("复制" == runmode)
            {

                this.Text = "复制营业业务";




                this.ly_sales_receive_singleBindingSource.AddNew();



                //this.receive_code   = GetReceive_Code();
                //this.合同编码TextBox.Text = this.receive_code;

                //this.客户编码ComboBox.SelectedValue  = this.salesclientcode;
                this.录入日期DateTimePicker.Value = SQLDatabase.GetNowdate();
                this.录入人TextBox1.Text = SQLDatabase.nowUserName();
            }
            else
            {



                this.ly_sales_receive_singleTableAdapter.Fill(this.lYSalseMange2.ly_sales_receive_single, this.receive_code);
                
                this.录入人TextBox1.Text = SQLDatabase.nowUserName();
           

                this.Text = "修改收件业务";



            }

       
        }

        private string GetReceive_Code()
        {
           

            SqlConnection sqlConnection1 = new SqlConnection(SQLDatabase.Connectstring);
            SqlCommand cmd = new SqlCommand();

            string receiveCode = "";

            //cmd.Parameters.Add("@Client_mode", SqlDbType.VarChar);
            //cmd.Parameters["@Client_mode"].Value = "YY";

            //cmd.Parameters.Add("@loanId", SqlDbType.Int);
            //cmd.Parameters["@loanId"].Value = loanId;

            //cmd.Parameters.Add("@item_name", SqlDbType.VarChar);
            //cmd.Parameters["@item_name"].Value = item_name;


            cmd.CommandText = "LY_GetMax_ReceiveCode";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = sqlConnection1;

            sqlConnection1.Open();
            receiveCode = cmd.ExecuteScalar().ToString();
            sqlConnection1.Close();



            return receiveCode;


        
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

                //this.contractinner_code = GetContractinner_Code();
                //this.录入日期DateTimePicker.Value = SQLDatabase.GetNowdate();

                this.receive_code = GetReceive_Code();
                this.合同编码TextBox.Text = this.receive_code;
            }


            if (null == this.客户编码ComboBox.SelectedValue)
            {
                MessageBox.Show("必须选择客户...", "注意");
                return;
            }

            if (string.IsNullOrEmpty(this.合同编码TextBox.Text ))
            {
                MessageBox.Show("必须输入合同编号...", "注意");
                return;
            }

            if (string.IsNullOrEmpty(this.维修部门TextBox.Text))
            {
                MessageBox.Show("必须输入维修部门...", "注意");
                return;
            }

            //维修部门TextBox



            this.ly_sales_receive_singleBindingSource.EndEdit();

            try
            {



                //if (string.IsNullOrEmpty(this.salesperson_codeComboBox.Text))
                //{
                //    MessageBox.Show("必须选择营业员...", "注意");
                //    return;ly_sales_receive_singleBindingSource

                //}



                this.Validate();
                this.ly_sales_receive_singleTableAdapter.Update(this.lYSalseMange2.ly_sales_receive_single);
                //this.salesperson_code = this.salesperson_codeComboBox.SelectedValue.ToString() + ":" + this.salesperson_codeComboBox.Text;

              
                this.salesclientcode = this.客户编码ComboBox.SelectedValue.ToString ();
                this.receive_code  = this.合同编码TextBox.Text;

                //this.ly_sales_contract_terms_forcontractBindingSource.EndEdit();
                //this.ly_sales_contract_terms_forcontractTableAdapter.Update(this.lYSalseMange.ly_sales_contract_terms_forcontract);
               


                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (SqlException sqle)
            {

                MessageBox.Show("业务编号重复...", "注意");
                //MessageBox.Show(sqle.Message, "注意");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button8_Click(object sender, EventArgs e)
        {
            LY_Salesclient_Sel queryForm = new LY_Salesclient_Sel();

            //queryForm.salesclient_code = "";
            //queryForm.runmode = "增加";


            queryForm.StartPosition = FormStartPosition.CenterParent;
            queryForm.ShowDialog();

            if (queryForm.DialogResult != DialogResult.Cancel)
            {

                this.客户编码ComboBox.SelectedValue = queryForm.sales_Clientcode;
                

            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string sel = "SELECT  repair_sector_name as 维修部门 FROM ly_sales_repair_sectors order by repair_sector_code";


            QueryForm queryForm = new QueryForm();


            queryForm.Sel = sel;
            queryForm.Constr = SQLDatabase.Connectstring;

            //Set the Column Collection to the filter Table
            //queryForm.SetSourceColumns(this.billMainDataSet.BalanceBill.Columns);

            queryForm.ShowDialog();

            this.维修部门TextBox.Text = queryForm.Result;
        }

        private void 维修部门Label_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            LY_Sales_RepairSet queryForm = new LY_Sales_RepairSet();


            queryForm.StartPosition = FormStartPosition.CenterParent;
            queryForm.ShowDialog();



           
        }

        private void 维修部门Label_Click(object sender, EventArgs e)
        {

        }



        //private void fillToolStripButton_Click_1(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        this.ly_sales_business_singleTableAdapter.Fill(this.lYSalseMange.ly_sales_business_single, business_codeToolStripTextBox.Text);
        //    }
        //    catch (System.Exception ex)
        //    {
        //        System.Windows.Forms.MessageBox.Show(ex.Message);
        //    }

        //}

        //private void 类别Label_MouseDoubleClick(object sender, MouseEventArgs e)
        //{
        //    if (SQLDatabase.CheckHaveRight(SQLDatabase.NowUserID, "营业综合信息"))
        //    {


        //        LY_Sales_ClassInfo queryForm = new LY_Sales_ClassInfo();

        //        // queryForm.Cardnumber = this.xD_Sel_SellBalanceDataGridView.CurrentRow.Cells["编号"].Value.ToString();
        //        queryForm.StartPosition = FormStartPosition.CenterParent;
        //        queryForm.ShowDialog();

        //        this.ly_sales_contract_classTableAdapter.Fill(this.lYSalseMange.ly_sales_contract_class);
        //    }
        //}

        //private void 属性Label_MouseDoubleClick(object sender, MouseEventArgs e)
        //{
        //    if (SQLDatabase.CheckHaveRight(SQLDatabase.NowUserID, "营业综合信息"))
        //    {




        //        LY_SalesStyle_Info queryForm = new LY_SalesStyle_Info();

        //        // queryForm.Cardnumber = this.xD_Sel_SellBalanceDataGridView.CurrentRow.Cells["编号"].Value.ToString();
        //        queryForm.StartPosition = FormStartPosition.CenterParent;
        //        queryForm.ShowDialog();

        //        this.ly_sales_contract_styleTableAdapter.Fill(this.lYSalseMange.ly_sales_contract_style);
        //    }
        //}

        //private void 公司编码Label_MouseDoubleClick(object sender, MouseEventArgs e)
        //{
        //    if (SQLDatabase.CheckHaveRight(SQLDatabase.NowUserID, "营业综合信息"))
        //    {




        //        LY_Company_Info queryForm = new LY_Company_Info();

        //        // queryForm.Cardnumber = this.xD_Sel_SellBalanceDataGridView.CurrentRow.Cells["编号"].Value.ToString();
        //        queryForm.StartPosition = FormStartPosition.CenterParent;
        //        queryForm.ShowDialog();

        //        this.ly_company_informationTableAdapter.Fill(this.lYSalseMange.ly_company_information);
        //    }
        //}

        //private void 属性ComboBox_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    SaveData();
        //    this.属性ComboBox.Focus();
        //}






        //private void 名称TextBox_Enter(object sender, EventArgs e)
        //{
        //    AppSet appSet = AppSet.Load();
        //    //InputLanguage.CurrentInputLanguage = InputLanguage.InstalledInputLanguages[2];
        //    if (0 < appSet.KeyboardInputIndex)
        //        InputLanguage.CurrentInputLanguage = InputLanguage.InstalledInputLanguages[appSet.KeyboardInputIndex];
        //}

        //private void 名称TextBox_Leave(object sender, EventArgs e)
        //{
        //    InputLanguage.CurrentInputLanguage = InputLanguage.DefaultInputLanguage;
        //}

        //private void ly_sales_clientBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        //{
        //    //this.Validate();
        //    //this.ly_sales_clientBindingSource.EndEdit();
        //    //this.tableAdapterManager.UpdateAll(this.lYSalseMange);

        //}

        //private void ly_sales_contract_mainSingleBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        //{
        //    //this.Validate();
        //    //this.ly_sales_contract_mainSingleBindingSource.EndEdit();
        //    //this.tableAdapterManager.UpdateAll(this.lYSalseMange);

        //}



        //private void ly_sales_contract_mainSingleBindingNavigatorSaveItem_Click_1(object sender, EventArgs e)
        //{
        //    this.Validate();
        //    this.ly_sales_contract_mainSingleBindingSource.EndEdit();
        //    this.tableAdapterManager.UpdateAll(this.lYSalseMange);

        //}




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
