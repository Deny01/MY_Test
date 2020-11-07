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
    public partial class LY_Salescontract_GroupDetailAdd : Form
    {
        public string runmode;

        public string nowsales_groupcode;
        public string now_plannum;

        public string nowclient_receiver;
        public string nowsalesclient_phone;
        public string nowpost_code;
        public string nowsalesclient_address;

        public LY_Salescontract_GroupDetailAdd()
        {
            InitializeComponent();
        }

        private void SaveData()
        {



            this.ly_sales_groupSingleBindingSource.EndEdit();
            this.ly_sales_groupSingleTableAdapter.Update(this.lYSalseMange.ly_sales_groupSingle);
            this.ly_sales_groupSingleTableAdapter.Fill(this.lYSalseMange.ly_sales_groupSingle, now_plannum, nowsales_groupcode);

          
        
        }

        private void LY_MaterialAdd_Load(object sender, EventArgs e)
        {


            this.ly_sales_groupSingleTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
           
          
            
            if ("增加" == runmode)
            {


                this.Text = "增加依赖书配套信息";




                this.ly_sales_groupSingleBindingSource.AddNew();

                    //this.制定日期DateTimePicker.Value = SQLDatabase.GetNowdate();

                    this.nowsales_groupcode = GetMaxGroupCode();

                    this.配套编码TextBox.Text = this.nowsales_groupcode;
                    this.依赖书号TextBox.Text = this.now_plannum;


                    this.收件人TextBox.Text = this.nowclient_receiver;
                    this.电话TextBox.Text = this.nowsalesclient_phone;
                    this.邮编TextBox.Text = this.nowpost_code;
                    this.地址TextBox.Text = this.nowsalesclient_address;

                    //this.录入人TextBox1.Text = SQLDatabase.nowUserName();

                    //SaveData();


                    //this.ly_material_plan_mainSingleTableAdapter.Fill(this.lYSalseMange.ly_material_plan_mainSingle, now_plannum);


                   // this.operatorTextBox.Text = SQLDatabase.nowUserName();

                 
            }
            else
            {



                this.ly_sales_groupSingleTableAdapter.Fill(this.lYSalseMange.ly_sales_groupSingle, now_plannum, nowsales_groupcode);



                this.Text = "修改依赖书配套信息";
               
              
            
            }

       
        }

        private string GetMaxGroupCode()
        {
            SqlConnection sqlConnection1 = new SqlConnection(SQLDatabase.Connectstring);
            SqlCommand cmd = new SqlCommand();

            string MaxPlanCode = "";

            cmd.Parameters.Add("@Plan_num", SqlDbType.VarChar);
            cmd.Parameters["@Plan_num"].Value = this .now_plannum;


            cmd.CommandText = "LY_Get_SalesgroupCode";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = sqlConnection1;

            sqlConnection1.Open();
            MaxPlanCode = cmd.ExecuteScalar().ToString();
            sqlConnection1.Close();



            return MaxPlanCode;
        }

        private void decimalTextBox_Validating(object sender, CancelEventArgs e)
        {
            TextBox tb = sender as TextBox;
            tb.Text = tb.Text.Replace(" ", "");
            tb.DataBindings[0].Parse += new ConvertEventHandler(bind_Parse);
        }

        void bind_Parse(object sender, ConvertEventArgs e)//控件值更改时发生
        {
            if (e.Value.ToString() == "")
                e.Value = DBNull.Value;
        }

        private void button1_Click(object sender, EventArgs e)
        {

           
            //this.ly_sales_groupSingleBindingSource.EndEdit();
            //this.ly_sales_groupSingleTableAdapter.Update(this.lYSalseMange.ly_material_plan_mainSingle);
            //this.ly_sales_groupSingleTableAdapter.Fill(this.lYSalseMange.ly_sales_groupSingle, now_plannum, nowsales_groupcode);

          

            try
            {



                if (string.IsNullOrEmpty(this.数量TextBox.Text))
                {
                    MessageBox.Show("必须输入数量", "注意");
                    return;

                }

                this.Validate();
                SaveData();
              

               


                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (SqlException sqle)
            {

                //MessageBox.Show("物资编号重复...", "注意");
                MessageBox.Show(sqle.Message, "注意");
            }
        }

       

        //private void fillToolStripButton_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        this.ly_sales_groupSingleTableAdapter.Fill(this.lYSalseMange.ly_sales_groupSingle, material_plan_numToolStripTextBox.Text, sales_group_codeToolStripTextBox.Text);
        //    }
        //    catch (System.Exception ex)
        //    {
        //        System.Windows.Forms.MessageBox.Show(ex.Message);
        //    }

        //}

       
       

     
      
       

     

       

        //private void fillToolStripButton_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        this.ly_material_plan_mainSingleTableAdapter.Fill(this.lYSalseMange.ly_material_plan_mainSingle, material_plan_numToolStripTextBox.Text);
        //    }
        //    catch (System.Exception ex)
        //    {
        //        System.Windows.Forms.MessageBox.Show(ex.Message);
        //    }

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
