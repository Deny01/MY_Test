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

namespace HappyYF.YuanXin.WorkSet
{
    public partial class LY_MaterialAdd_BaseMatiral : Form
    {
        public string runmode;
        public string statemode;
        public string material_code;

        public LY_MaterialAdd_BaseMatiral()
        {
            InitializeComponent();
        }

        private void LY_MaterialAdd_Load(object sender, EventArgs e)
        {
           
            
            this.ly_secondstyle_setTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            this.ly_secondstyle_setTableAdapter.Fill(this.lYMaterialMange.ly_secondstyle_set);

            this.ly_firststyle_setTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            this.ly_firststyle_setTableAdapter.Fill(this.lYMaterialMange.ly_firststyle_set);

            this.ly_warehouseTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            this.ly_warehouseTableAdapter.Fill(this.lYMaterialMange.ly_warehouse);
           
            this.ly_prod_deptTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            this.ly_prod_deptTableAdapter.Fill(this.lYMaterialMange.ly_prod_dept);
            this.ly_materrial_sortTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            this.ly_materrial_sortTableAdapter.Fill(this.lYMaterialMange.ly_materrial_sort);

            this.ly_unitsetTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            this.ly_unitsetTableAdapter.Fill(this.lYMaterialMange.ly_unitset);

            this.ly_unitset1TableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            this.ly_unitset1TableAdapter.Fill(this.lYMaterialMange.ly_unitset1);

            this.ly_materialstatusTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            this.ly_materialstatusTableAdapter.Fill(this.lYMaterialMange.ly_materialstatus);
            this.ly_inma0010addTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;


           
            
            if ("增加" == runmode)
            {
                this.Text = "新增物资信息";

               

                if ("原料" == statemode)
                {
                    this.lymaterialstatusBindingSource.Filter = "status_name='原料' or status_name='基料'";
                    this.lywarehouseBindingSource.Filter = "warehousename<>'成品'";
                    this.ly_inma0010addBindingSource.AddNew();
                }
                else
                {
                    this.lymaterialstatusBindingSource.Filter = "status_name<>'原料' and status_name<>'基料'";
                    this.lywarehouseBindingSource.Filter = "warehousename='成品'";
                    this.ly_inma0010addBindingSource.AddNew();
                  
                        this.物资编号TextBox.Text = GetProuductCode();
                        this.物资编号TextBox.ReadOnly = true;
                        this.groupBox2.Visible = true;

                   
                }
                

             
            }
            else
            {
                this.Text = "修改物资信息";
               
                //this.物资编号TextBox.ReadOnly = true;
                this.物资编号TextBox.ReadOnly = false;

                if ("原料" == statemode)
                {
                    this.lymaterialstatusBindingSource.Filter = "status_name='原料' or status_name='基料'";
                    this.lywarehouseBindingSource.Filter = "warehousename<>'成品'";
                }
                else
                {
                    this.lymaterialstatusBindingSource.Filter = "status_name<>'原料' and status_name<>'基料'";
                    this.lywarehouseBindingSource.Filter = "warehousename='成品'";
                    this.groupBox2.Visible = true;
                }

                this.ly_inma0010addTableAdapter.Fill(this.lYMaterialMange.ly_inma0010add, material_code);

            }

            //if ("原料" == statemode)
            //{
            //    this.lymaterialstatusBindingSource.Filter = "status_name='原料'";
            //}
            //else
            //{
            //    this.lymaterialstatusBindingSource.Filter = "status_name<>'原料'";
            //    if ("增加" == runmode)
            //    {
            //        this.物资编号TextBox.Text  = GetProuductCode();
            //        this.物资编号TextBox.ReadOnly = true;

            //    }
            //}
        }

        private string GetProuductCode()
        {
           

            SqlConnection sqlConnection1 = new SqlConnection(SQLDatabase.Connectstring);
            SqlCommand cmd = new SqlCommand();

            string pdcode = "";

            //cmd.Parameters.Add("@datestyle", SqlDbType.VarChar );
            //cmd.Parameters["@datestyle"].Value = datestyle;

            //cmd.Parameters.Add("@loanId", SqlDbType.Int);
            //cmd.Parameters["@loanId"].Value = loanId;

            //cmd.Parameters.Add("@item_name", SqlDbType.VarChar);
            //cmd.Parameters["@item_name"].Value = item_name;


            cmd.CommandText = "LY_GetProduct_Number";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = sqlConnection1;

            sqlConnection1.Open();
            pdcode = cmd.ExecuteScalar().ToString();
            sqlConnection1.Close();



            return pdcode;


        
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
            this.物资编号TextBox.Text = this.物资编号TextBox.Text.Replace(" ", "");

            if (string.IsNullOrEmpty(this.物资编号TextBox.Text))
            {
                MessageBox.Show("物资编号不能为空...", "注意");
                return;
            }
            if (8 != this.物资编号TextBox.Text.Length)
            {
                MessageBox.Show("物资编号应为8位...", "注意");
                return;
            }

            if (string.IsNullOrEmpty(this.状态ComboBox.Text))
            {
                MessageBox.Show("物资状态不能为空...", "注意");
                return;
            }

            this.ly_inma0010addBindingSource.EndEdit();

            try
            {
                this.ly_inma0010addTableAdapter.Update(this.lYMaterialMange.ly_inma0010add);
                this.material_code = this.物资编号TextBox.Text;
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (SqlException sqle)
            {
                
                //MessageBox.Show("物资编号重复...", "注意");
                MessageBox.Show(sqle.Message , "注意");
            }
        }

        private void 状态Label_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            LY_MaterialStatusSet queryForm = new LY_MaterialStatusSet();


            queryForm.StartPosition = FormStartPosition.CenterParent;
            queryForm.ShowDialog();


           
            this.ly_materialstatusTableAdapter.Fill(this.lYMaterialMange.ly_materialstatus);
        }

        private void 单位Label_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            LY_MaterialUnitSet queryForm = new LY_MaterialUnitSet();


            queryForm.StartPosition = FormStartPosition.CenterParent;
            queryForm.ShowDialog();


            this.ly_unitsetTableAdapter.Fill(this.lYMaterialMange.ly_unitset);
           
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
            if (e.KeyChar.ToString() == "\r")
            {
                e.Handled = true;
                this.状态ComboBox.Focus();

            }
        }

        private void 状态ComboBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar.ToString() == "\r")
            {
                e.Handled = true;
                this.库存位置TextBox.Focus();

            }

        }

        private void 库存位置TextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar.ToString() == "\r")
            {
                e.Handled = true;
                this.中方型号TextBox.Focus();

            }

        }

        private void 中方型号TextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar.ToString() == "\r")
            {
                e.Handled = true;
                this.日方型号TextBox.Focus();

            }

        }

        private void 日方型号TextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar.ToString() == "\r")
            {
                e.Handled = true;
                this.规格TextBox.Focus();

            }

        }

        private void 规格TextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar.ToString() == "\r")
            {
                e.Handled = true;
                this.单位ComboBox.Focus();

            }

        }

        private void 单位ComboBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar.ToString() == "\r")
            {
                e.Handled = true;
                this.库存单价TextBox.Focus();

            }

        }

        private void 库存单价TextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar.ToString() == "\r")
            {
                e.Handled = true;
                this.库存底线TextBox.Focus();

            }
        }

        private void 库存底线TextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar.ToString() == "\r")
            {
                e.Handled = true;
                this.推荐采购TextBox.Focus();

            }
        }

        private void 推荐采购TextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar.ToString() == "\r")
            {
                e.Handled = true;
                this.种类ComboBox.Focus();

            }
        }

        private void 种类TextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar.ToString() == "\r")
            {
                e.Handled = true;
                this.生产部门ComboBox.Focus();

            }
        }

        private void 生产部门ComboBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar.ToString() == "\r")
            {
                e.Handled = true;
                this.出库CheckBox.Focus();

            }
        }

        private void 出库CheckBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar.ToString() == "\r")
            {
                e.Handled = true;
                this.备注TextBox.Focus();

            }
        }

        private void 备注TextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar.ToString() == "\r")
            {
                e.Handled = true;
                this.物资编号TextBox.Focus();

            }
        }

        private void 种类Label_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            LY_MaterialSort_Set queryForm = new LY_MaterialSort_Set();


            queryForm.StartPosition = FormStartPosition.CenterParent;
            queryForm.ShowDialog();



            this.ly_materrial_sortTableAdapter.Fill(this.lYMaterialMange.ly_materrial_sort);
        }

        private void 生产部门Label_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            LY_MaterialDept queryForm = new LY_MaterialDept();


            queryForm.StartPosition = FormStartPosition.CenterParent;
            queryForm.ShowDialog();



            this.ly_prod_deptTableAdapter.Fill(this.lYMaterialMange.ly_prod_dept);
        }

        private void 仓库Label_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            LY_WarehouseSet queryForm = new LY_WarehouseSet();


            queryForm.StartPosition = FormStartPosition.CenterParent;
            queryForm.ShowDialog();



            this.ly_warehouseTableAdapter.Fill(this.lYMaterialMange.ly_warehouse);
        }

        private void fir_styleLabel_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            LY_Firststyle_Set queryForm = new LY_Firststyle_Set();


            queryForm.StartPosition = FormStartPosition.CenterParent;
            queryForm.ShowDialog();



            this.ly_firststyle_setTableAdapter.Fill(this.lYMaterialMange.ly_firststyle_set);
        }

        private void sec_styleLabel_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            LY_Secondstyle_Set queryForm = new LY_Secondstyle_Set();

            string firstyle_Code = this.fir_styleComboBox.SelectedValue.ToString();

            queryForm.firstyle_Code = firstyle_Code;

            queryForm.StartPosition = FormStartPosition.CenterParent;
            queryForm.ShowDialog();



            this.ly_secondstyle_setTableAdapter.Fill(this.lYMaterialMange.ly_secondstyle_set);
            this.lysecondstylesetBindingSource.Filter = "firststyleCode ='" + firstyle_Code+"'";
        }

        private void fir_styleComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            string firstyle_Code = this.fir_styleComboBox.SelectedValue.ToString();
            this.lysecondstylesetBindingSource.Filter = "firststyleCode ='" + firstyle_Code + "'";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.ly_inma0010addBindingSource.EndEdit();
            try
            {
                this.ly_inma0010addTableAdapter.Update(this.lYMaterialMange.ly_inma0010add);
                this.material_code = this.物资编号TextBox.Text;
                this.ly_inma0010addTableAdapter.Fill(this.lYMaterialMange.ly_inma0010add, material_code);
               
            }
            catch (SqlException sqle)
            {

                //MessageBox.Show("物资编号重复...", "注意");
                MessageBox.Show(sqle.Message, "注意");
            }
        }

       
       

       
     

       
    }
}
