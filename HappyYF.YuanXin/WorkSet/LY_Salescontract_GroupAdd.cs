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
using Project_Manager.AppServices;

namespace HappyYF.YuanXin.WorkSet
{
    public partial class LY_Salescontract_GroupAdd : Form
    {
        public string runmode;

        public string contractinner_code;
        public string now_plannum;
        public string nowcontractCode;


        public LY_Salescontract_GroupAdd()
        {
            InitializeComponent();
        }

        private void SaveData()
        {

            

            this.ly_material_plan_mainSingleBindingSource.EndEdit();
            this.ly_material_plan_mainSingleTableAdapter.Update(this.lYSalseMange.ly_material_plan_mainSingle);
            this.ly_material_plan_mainSingleTableAdapter.Fill(this.lYSalseMange.ly_material_plan_mainSingle, now_plannum);

          
        
        }

        private void LY_MaterialAdd_Load(object sender, EventArgs e)
        {
           
            
            this.ly_material_plan_mainSingleTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
           
          
            
            if ("增加" == runmode)
            {

              
                    this.Text = "新增依赖书";




                    this.ly_material_plan_mainSingleBindingSource.AddNew();

                    this.制定日期DateTimePicker.Value = SQLDatabase.GetNowdate();
                    this.发货日期DateTimePicker.Value = SQLDatabase.GetNowdate();

                    this.now_plannum = GetMaxPlanCode();
                    this.计划编号TextBox.Text = this.now_plannum;


                    this.内部编码TextBox.Text = this.nowcontractCode;

                    this.合同编号TextBox.Text = this.nowcontractCode;

                    this.录入人TextBox1.Text = SQLDatabase.nowUserName();

                    this.提交CheckBox.Checked = false;
                    this.提交CheckBox.Checked = true;
                    

                    //SaveData();


                    //this.ly_material_plan_mainSingleTableAdapter.Fill(this.lYSalseMange.ly_material_plan_mainSingle, now_plannum);


                   // this.operatorTextBox.Text = SQLDatabase.nowUserName();

                 
            }
            else
            {



                this.ly_material_plan_mainSingleTableAdapter.Fill(this.lYSalseMange.ly_material_plan_mainSingle, now_plannum);

                

                this.Text = "修改依赖书";
               
              
            
            }

       
        }

        private string GetMaxPlanCode()
        {
            SqlConnection sqlConnection1 = new SqlConnection(SQLDatabase.Connectstring);
            SqlCommand cmd = new SqlCommand();

            string MaxPlanCode = "";

            cmd.Parameters.Add("@Plan_mode", SqlDbType.VarChar);
            cmd.Parameters["@Plan_mode"].Value = "LSPT";


            cmd.CommandText = "LY_GetMaxPlanCode";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = sqlConnection1;

            sqlConnection1.Open();
            MaxPlanCode = cmd.ExecuteScalar().ToString();
            sqlConnection1.Close();



            return MaxPlanCode;
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

            this.ly_material_plan_mainSingleBindingSource.EndEdit();

            try
            {



                //if (string.IsNullOrEmpty(this.salesperson_codeComboBox.Text))
                //{
                //    MessageBox.Show("必须选择营业员...", "注意");
                //    return;

                //}

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