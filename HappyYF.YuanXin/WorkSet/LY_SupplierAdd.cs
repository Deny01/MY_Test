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
    public partial class LY_SupplierAdd : Form
    {
        public string runmode;
        public string sortmode;
        public string supplier_code;
        private string sortcode;

        public string Sortcode
        {
            get { return sortcode; }
            set { sortcode = value; }
        }

        public LY_SupplierAdd()
        {
            InitializeComponent();
        }

        private void LY_MaterialAdd_Load(object sender, EventArgs e)
        {
            this.ly_supplier_listTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;

            this.期初应付TextBox.ReadOnly = true ;
            this.期初应付TextBox.Enabled = false;


            if ("增加" == runmode)
            {

                if ("CG" == sortmode)
                {
                    this.Text = "新增采购供应商信息";
                    //this.sortcode = "3";
                }

                if ("WX" == sortmode)
                {
                    this.Text = "新增外协供应商信息";
                    //this.sortcode = "2";
                }
                if ("WT" == sortmode)
                {
                    this.Text = "新增机加委托商信息";
                    //this.sortcode = "4";
                }

                if ("NP" == sortmode)
                {
                    this.Text = "非采购供应商管理";
                    
                }

                this.ly_supplier_listBindingSource.AddNew();

                this.supplier_code = GetSupplierCode(sortmode);
                this.编码TextBox.Text = this.supplier_code;
                this.sort_codeTextBox.Text = this.sortcode;
                this.录入人TextBox.Text = SQLDatabase.nowUserName();

                 
            }
            else
            {
                if (SQLDatabase.CheckHaveRight(SQLDatabase.NowUserID, "供应商财务编码修改"))
                {
                    //MessageBox.Show("无应付计划增加权限...", "注意");
                    this.财务编码TextBox.ReadOnly = false;

                   

                }
                if ("CG" == sortmode)
                {
                    this.Text = "修改采购供应商信息";
                    //this.sortcode = "3";
                }

                if ("WX" == sortmode)
                {
                    this.Text = "修改外协供应商信息";
                    //this.sortcode = "2";
                }
                if ("WT" == sortmode)
                {
                    this.Text = "修改机加委托商信息";
                    //this.sortcode = "4";
                }
                this.ly_supplier_listTableAdapter.Fill(this.lYMaterielRequirements.ly_supplier_list, sortcode);
                this.ly_supplier_listBindingSource.Filter = "编码='" + supplier_code + "'";

                this.Text = "修改供应商信息";
               
              
            
            }

       
        }

        private string GetSupplierCode( string sortmode)
        {
           

            SqlConnection sqlConnection1 = new SqlConnection(SQLDatabase.Connectstring);
            SqlCommand cmd = new SqlCommand();

            string suppliercode = "";

            cmd.Parameters.Add("@Supplier_mode", SqlDbType.VarChar);
            cmd.Parameters["@Supplier_mode"].Value = sortmode;

            //cmd.Parameters.Add("@loanId", SqlDbType.Int);
            //cmd.Parameters["@loanId"].Value = loanId;

            //cmd.Parameters.Add("@item_name", SqlDbType.VarChar);
            //cmd.Parameters["@item_name"].Value = item_name;


            cmd.CommandText = "LY_GetMaxSupplierCode";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = sqlConnection1;

            sqlConnection1.Open();
            suppliercode = cmd.ExecuteScalar().ToString();
            sqlConnection1.Close();



            return suppliercode;


        
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

            this.ly_supplier_listBindingSource.EndEdit();

            try
            {
                this.ly_supplier_listTableAdapter.Update(this .lYMaterielRequirements.ly_supplier_list );
                this.supplier_code  = this.编码TextBox.Text;
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
