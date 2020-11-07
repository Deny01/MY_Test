using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Data.SqlClient;

using System.Transactions;

using HappyYF.Infrastructure.Repositories;
using DataGridFilter;

namespace HappyYF.YuanXin.WorkSet
{
    public partial class LY_Payable_Manage : Form
    {

        private int selectionIdx = 0;
        string formState = "View";
        public LY_Payable_Manage()
        {
            InitializeComponent(); 
            //this.LY_payable_mainTableAdapter.
            this.ly_payable_planTableAdapter.CommandTimeout = 0;
            this.ly_payable_itemsTableAdapter.CommandTimeout = 0;
            this.lY_payable_periodTableAdapter.CommandTimeout = 0;
            this.lY_payable_mainTableAdapter.CommandTimeout = 0;


        }

       
        private void LY_Payable_Manage_Load(object sender, EventArgs e)
        {
            this.ly_Prepayment_NPTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            this.ly_actualpayment_NPTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            this.ly_payable_NPTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;

            this.lY_payable_detail_standard_SumTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            
            this.lY_payable_PrepayHaveinvoiceNoPlanTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            
            this.lY_payable_PrepayNoincoiceTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            this.lY_payable_PrepayNoincoicePlanTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;

            this.lY_payable_detail_allTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            this.lY_payable_detail_all_planTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;

            this.ly_supplier_listTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            

            this.lY_payable_periodTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            this.lY_payable_period_detailTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;

            this.ly_payable_planTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            this.ly_payable_itemsTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            this.ly_payable_item_detail1TableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            this.ly_payable_item_bindTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;

            //this.ly_payable_planTableAdapter.Fill(this.lYFinancialMange.ly_payable_plan);

            if ("0002" == SQLDatabase.nowUserDepartmentBig())
                {

                this.ly_payable_planBindingSource.Filter = "部门批准=True";
                this.ly_PrepaymentBindingSource.Filter = "审定=True";
                
            }

            this.dateTimePicker1.Text = SQLDatabase.GetNowdate().Date.ToString();

            //this.dateTimePicker2.MinDate = DateTime.Parse("2019-05-01");
            this.dateTimePicker2.Text = DateTime.Now.AddDays(1 - DateTime.Now.Day).Date.ToString();
            this.dateTimePicker3.Text = SQLDatabase.GetNowdate().Date.ToString();

            this.dateTimePicker4.Text = SQLDatabase.GetNowdate().AddMonths(-2).Date.ToString();
            this.dateTimePicker5.Text = SQLDatabase.GetNowdate().AddMonths(-1).Date.ToString();

            this.lY_payable_contractTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            this.ly_PrepaymentTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;

            this.lY_payable_detailTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            this.lY_payable_mainTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;

            //this.lY_payable_mainTableAdapter.Fill(this.lYFinancialMange.LY_payable_main, this.dateTimePicker1.Value);
            //AddSummationRow_New(lY_payable_mainBindingSource, lY_payable_mainDataGridView);

            /////////////////////////////////////
            
            DataGridViewColumnSelector cs = new DataGridViewColumnSelector(lY_payable_periodDataGridView, this.Text);
            cs.MaxHeight = 180;
            cs.Width = 800;

            cs.Set_dgvColumns();
            ////////////////////////////////

            SetFormState("View");

        }

        private void lY_payable_mainDataGridView_SelectionChanged(object sender, EventArgs e)
        {
            Mainchanged();
            //this.lY_payable_detailTableAdapter.Fill(this.lYFinancialMange.LY_payable_detail, this.dateTimePicker1.Value, nowsupplierCode);

        }

        private void Mainchanged()
        {
            if (null == lY_payable_mainDataGridView.CurrentRow)
            {
                //this.ly_sales_contract_detailTableAdapter.Fill(this.lYSalseMange.ly_sales_contract_detail, "", 0);
                this.lY_payable_contractTableAdapter.Fill(this.lYFinancialMange.LY_payable_contract, this.dateTimePicker1.Value, "sss");
                return;
            }


            string nowsupplierCode = lY_payable_mainDataGridView.CurrentRow.Cells["supplier_code"].Value.ToString();
            this.lY_payable_contractTableAdapter.Fill(this.lYFinancialMange.LY_payable_contract, this.dateTimePicker1.Value, nowsupplierCode);
        }

        protected bool IsInteger(object o)
        {
            if (o is Int64)
                return true;
            if (o is Int32)
                return true;
            if (o is Int16)
                return true;
            return false;
        }
        protected bool IsDecimal(object o)
        {
            if (o is Decimal)
                return true;
            if (o is Single)
                return true;
            if (o is Double)
                return true;
            return false;
        }

        private void AddSummationRow_New(BindingSource bs, DataGridView dgv)
        {
            //InitializeApp();
            //return;

            DataRow sumdr = (((DataSet)bs.DataSource).Tables[bs.DataMember]).NewRow();

            if (-1 != bs.Find("supplier_code", "_合计"))
            {
                bs.RemoveAt(bs.Find("supplier_code", "_合计"));
            }

            foreach (DataGridViewRow dgvRow in dgv.Rows)
            {
                foreach (DataGridViewCell dgvCell in dgvRow.Cells)
                {
                    //foreach (DataGridViewColumn dgvColumn in dgv.Columns)
                    //{
                    if (dgvCell.Value != null && !(dgvCell.Value is DBNull))
                    {
                        if (IsInteger(dgvCell.Value))
                        {
                            if ("年份" != dgvCell.OwningColumn.HeaderText && "月份" != dgvCell.OwningColumn.HeaderText)
                            {
                                if (sumdr[dgvCell.OwningColumn.DataPropertyName] == null || (sumdr[dgvCell.OwningColumn.DataPropertyName] is DBNull))
                                    sumdr[dgvCell.OwningColumn.DataPropertyName] = 0;


                                sumdr[dgvCell.OwningColumn.DataPropertyName] = Convert.ToInt64(sumdr[dgvCell.OwningColumn.DataPropertyName]) + Convert.ToInt64(dgvCell.Value);
                            }
                        }
                        else if (IsDecimal(dgvCell.Value))
                        {
                            if ("年份" != dgvCell.OwningColumn.HeaderText && "月份" != dgvCell.OwningColumn.HeaderText)
                            {
                                if (sumdr[dgvCell.OwningColumn.DataPropertyName] == null || (sumdr[dgvCell.OwningColumn.DataPropertyName] is DBNull))
                                    sumdr[dgvCell.OwningColumn.DataPropertyName] = 0;
                                //dgvCell .OwningColumn.Name  dgvCell.ColumnIndex

                                sumdr[dgvCell.OwningColumn.DataPropertyName] = Convert.ToDecimal(sumdr[dgvCell.OwningColumn.DataPropertyName]) + Convert.ToDecimal(dgvCell.Value);
                            }
                        }

                        //sumBox.Text = string.Format("{0}", sumBox.Tag);
                        //sumBox.Invalidate();

                    }
                    //}
                }

            }

            //sumdr["supplier_code"] = "02";
            sumdr["supplier_code"] = "_合计";
            //sumdr["id"] = -999;
            //sumdr["税务"] = "";
            ((DataSet)bs.DataSource).Tables[bs.DataMember].Rows.Add(sumdr);
            bs.ResetBindings(true);

        }
        private void AddSummationRow_New2(BindingSource bs, DataGridView dgv)
        { 

            DataRow sumdr = (((DataSet)bs.DataSource).Tables[bs.DataMember]).NewRow();

            if (-1 != bs.Find("供应商代码", "_Amount"))
            {
                bs.RemoveAt(bs.Find("供应商代码", "_Amount"));
            }

            foreach (DataGridViewRow dgvRow in dgv.Rows)
            {
                foreach (DataGridViewCell dgvCell in dgvRow.Cells)
                { 
                    if (dgvCell.Value != null && !(dgvCell.Value is DBNull))
                    {
                        if (IsInteger(dgvCell.Value))
                        {
                            if ("年份" != dgvCell.OwningColumn.HeaderText && "月份" != dgvCell.OwningColumn.HeaderText)
                            {
                                if (sumdr[dgvCell.OwningColumn.DataPropertyName] == null || (sumdr[dgvCell.OwningColumn.DataPropertyName] is DBNull))
                                    sumdr[dgvCell.OwningColumn.DataPropertyName] = 0;


                                sumdr[dgvCell.OwningColumn.DataPropertyName] = Convert.ToInt64(sumdr[dgvCell.OwningColumn.DataPropertyName]) + Convert.ToInt64(dgvCell.Value);
                            }
                        }
                        else if (IsDecimal(dgvCell.Value))
                        {
                            if ("年份" != dgvCell.OwningColumn.HeaderText && "月份" != dgvCell.OwningColumn.HeaderText)
                            {
                                if (sumdr[dgvCell.OwningColumn.DataPropertyName] == null || (sumdr[dgvCell.OwningColumn.DataPropertyName] is DBNull))
                                    sumdr[dgvCell.OwningColumn.DataPropertyName] = 0; 
                                sumdr[dgvCell.OwningColumn.DataPropertyName] = Convert.ToDecimal(sumdr[dgvCell.OwningColumn.DataPropertyName]) + Convert.ToDecimal(dgvCell.Value);
                            }
                        }
                         

                    } 
                }

            }

           
            sumdr["供应商代码"] = "_Amount"; 
            ((DataSet)bs.DataSource).Tables[bs.DataMember].Rows.Add(sumdr);
            bs.ResetBindings(true);

        }
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            NewFrm.Show(this);

            

            this.lY_payable_mainDataGridView.SelectionChanged -= this.lY_payable_mainDataGridView_SelectionChanged;
            this.lY_payable_mainTableAdapter.Fill(this.lYFinancialMange.LY_payable_main, this.dateTimePicker1.Value);
            AddSummationRow_New(lY_payable_mainBindingSource, lY_payable_mainDataGridView);
            this.lY_payable_mainDataGridView.SelectionChanged += this.lY_payable_mainDataGridView_SelectionChanged;

            Mainchanged();

            NewFrm.Hide(this);
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            string nowdatestyle;

            if (this.radioButton1.Checked)
            {
                nowdatestyle = "申请";
            }
            else
            {
                nowdatestyle = "付款";
            }

            this.ly_PrepaymentTableAdapter.Fill(this.lYFinancialMange.ly_Prepayment, this.dateTimePicker2.Value, this.dateTimePicker3.Value.AddDays(1), nowdatestyle);
        }

        private void lY_payable_contractDataGridView_SelectionChanged(object sender, EventArgs e)
        {
            if (null == lY_payable_contractDataGridView.CurrentRow)
            {
                this.lY_payable_detailTableAdapter.Fill(this.lYFinancialMange.LY_payable_detail, this.dateTimePicker1.Value, "sss");

                //this.lY_payable_contractTableAdapter.Fill(this.lYFinancialMange.LY_payable_contract, this.dateTimePicker1.Value, "sss");
                return;
            }


            string nowcontractCode = lY_payable_contractDataGridView.CurrentRow.Cells["合同编码c"].Value.ToString();
            //this.lY_payable_contractTableAdapter.Fill(this.lYFinancialMange.LY_payable_contract, this.dateTimePicker1.Value, nowsupplierCode);
            this.lY_payable_detailTableAdapter.Fill(this.lYFinancialMange.LY_payable_detail, this.dateTimePicker1.Value, nowcontractCode);

        }

        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }

        private void SetFormState(string state)
        {
            // view edit add save cancle

            //return;

            if ("View" == state)
            {
                this.formState = "View";

                this.计划日期DateTimePicker.Enabled = false;

                this.计划说明TextBox.ReadOnly = true;
                this.部门批准CheckBox.Enabled = false;
                this.公司批准CheckBox.Enabled = false;
                //启用备料CheckBox.Enabled = false;
                //备料计算CheckBox.Enabled = false;
                //年份TextBox.ReadOnly = true;
                //月份ComboBox.Enabled = false;


                this.toolStripButton3.Enabled = true;
                this.toolStripButton6.Enabled = true;
                this.toolStripButton4.Enabled = true;
                this.toolStripButton5.Enabled = true;
                this.toolStripTextBox1.Enabled = true;




                toolStripButton7.Enabled = true;
                bindingNavigatorDeleteItem.Enabled = true;
                bindingNavigatorAddNewItem.Enabled = true;
                ly_material_plan_mainBindingNavigatorSaveItem.Enabled = false;

                //yX_clientBindingNavigatorSaveItem.Enabled = true ;


                ly_payable_planDataGridView.Enabled = true;
                //this.button1.Enabled = true;


            }
            else
            {
                this.formState = "Edit";

                this.计划日期DateTimePicker.Enabled = true;

                this.计划说明TextBox.ReadOnly = false;
                this.部门批准CheckBox.Enabled = false;
                this.公司批准CheckBox.Enabled = false;
                //启用备料CheckBox.Enabled = true;
                //备料计算CheckBox.Enabled = true;

                //年份TextBox.ReadOnly = false;
                //月份ComboBox.Enabled = true;


                this.toolStripButton3.Enabled = false;
                this.toolStripButton6.Enabled = false;
                this.toolStripButton4.Enabled = false;
                this.toolStripButton5.Enabled = false;
                this.toolStripTextBox1.Enabled = false;




                toolStripButton7.Enabled = false;
                bindingNavigatorDeleteItem.Enabled = false;
                bindingNavigatorAddNewItem.Enabled = false;
                ly_material_plan_mainBindingNavigatorSaveItem.Enabled = true;

                


                ly_payable_planDataGridView.Enabled = false;



            }


        }

        private string GetMaxPlanCode()
        {
            SqlConnection sqlConnection1 = new SqlConnection(SQLDatabase.Connectstring);
            SqlCommand cmd = new SqlCommand();

            string MaxPlanCode = "";

            cmd.Parameters.Add("@Plan_mode", SqlDbType.VarChar);
            cmd.Parameters["@Plan_mode"].Value = "FKJH";


            cmd.CommandText = "LY_GetMaxPayablePlanCode";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = sqlConnection1;

            sqlConnection1.Open();
            MaxPlanCode = cmd.ExecuteScalar().ToString();
            sqlConnection1.Close();

            ////  aaaaaaaaaaaaaa  

            return MaxPlanCode;
        }

        private void bindingNavigatorAddNewItem_Click(object sender, EventArgs e)
        {
            if (!SQLDatabase.CheckHaveRight(SQLDatabase.NowUserID, "应付计划增加"))
            {
                MessageBox.Show("无应付计划增加权限...", "注意");

                return;

            }

            if (!Check_ifApproved("canadd"))
            {

                MessageBox.Show("存在未经付款确认的计划,不能增加新计划...", "注意");
                return;
            }

            string message = "增加付款计划吗？";
            string caption = "提示...";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;

            DialogResult result;



            result = MessageBox.Show(message, caption, buttons,
            MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

            if (result == DialogResult.Yes)
            {
                this.ly_payable_planBindingSource.AddNew();
                this.ly_payable_planDataGridView.CurrentRow.Cells["计划编码"].Value = GetMaxPlanCode();
                this.ly_payable_planDataGridView.CurrentRow.Cells["计划日期"].Value = SQLDatabase.GetNowdate();
                this.ly_payable_planDataGridView.CurrentRow.Cells["制定人"].Value = SQLDatabase.nowUserName();
                this.ly_payable_planDataGridView.EndEdit();

                this.Validate();
                this.ly_payable_planBindingSource.EndEdit();



                this.ly_payable_planTableAdapter.Update(this.lYFinancialMange.ly_payable_plan);



                SetFormState("Edit");
                this.计划日期DateTimePicker.Focus();

                //DataRowView nowCard = (DataRowView)this.yX_clientBindingSource.Current;

                //   nowCard["Card_number"].; nowCard.

            }
        }

        private Boolean Check_ifApproved(string nowDepartment)
        {
            if (this.ly_payable_planDataGridView.CurrentRow == null) return true;
            string planNum = this.ly_payable_planDataGridView.CurrentRow.Cells["计划编码"].Value.ToString();

            SqlConnection sqlConnection1 = new SqlConnection(SQLDatabase.Connectstring);
            SqlCommand cmd = new SqlCommand();

            Boolean Approve_flag = true;

            cmd.Parameters.Add("@NowPlannum", SqlDbType.VarChar);
            cmd.Parameters["@NowPlannum"].Value = planNum;

            cmd.Parameters.Add("@Nowdepartment", SqlDbType.VarChar);
            cmd.Parameters["@Nowdepartment"].Value = nowDepartment;


            cmd.CommandText = "LY_GetPayablePlan_Approve";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = sqlConnection1;

            sqlConnection1.Open();
            Approve_flag = Boolean.Parse(cmd.ExecuteScalar().ToString());
            sqlConnection1.Close();



            return Approve_flag;

        }

        private void bindingNavigatorDeleteItem_Click(object sender, EventArgs e)
        {
            if (null == this.ly_payable_planDataGridView.CurrentRow) return;

            string planNum = this.ly_payable_planDataGridView.CurrentRow.Cells["计划编码"].Value.ToString();

            if (Check_ifApproved("department"))
            {

                MessageBox.Show("计划已经批准,不能删除...", "注意");
                return;
            }

            if (this.ly_payable_planDataGridView.CurrentRow.Cells["制定人"].Value.ToString() != SQLDatabase.nowUserName())
            {

                MessageBox.Show("请" + this.ly_payable_planDataGridView.CurrentRow.Cells["制定人"].Value.ToString() + "删除...", "注意");

                return;
            }

            //if (this.ly_material_plan_detailDataGridView.RowCount > 0)
            //{
            //    MessageBox.Show("计划已有物料记录，不能删除(实需删除，请先删除该计划的物料记录)", "注意");
            //    return;

            //}



            string message1 = "当前(计划：" + planNum + ")将被删除，继续吗？";
            string caption1 = "提示...";
            MessageBoxButtons buttons1 = MessageBoxButtons.YesNo;

            DialogResult result1;



            result1 = MessageBox.Show(message1, caption1, buttons1,
            MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

            if (result1 == DialogResult.Yes)
            {

                string delstr = " delete ly_payable_plan  where payable_plan_num = '" + planNum + "'";


                SqlConnection sqlConnection1 = new SqlConnection(SQLDatabase.Connectstring);
                SqlCommand cmd = new SqlCommand();

                cmd.CommandText = delstr;
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


                    this.ly_payable_planTableAdapter.Fill(this.lYFinancialMange.ly_payable_plan);
                }


            }
        }

        private void toolStripButton7_Click(object sender, EventArgs e)
        {
            if (null == this.ly_payable_planDataGridView.CurrentRow) return;

            string planNum = this.ly_payable_planDataGridView.CurrentRow.Cells["计划编码"].Value.ToString();

            if (Check_ifApproved("department"))
            {

                MessageBox.Show("计划已经批准,不能修改数据...", "注意");
                return;
            }

            if (this.ly_payable_planDataGridView.CurrentRow.Cells["制定人"].Value.ToString() != SQLDatabase.nowUserName())
            {

                MessageBox.Show("请" + this.ly_payable_planDataGridView.CurrentRow.Cells["制定人"].Value.ToString() + "修改...", "注意");

                return;
            }

            SetFormState("Edit");
        }

        private void ly_material_plan_mainBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.ly_payable_planBindingSource.EndEdit();
            this.ly_payable_planTableAdapter.Update(this.lYFinancialMange.ly_payable_plan);

            SetFormState("View");
        }

        private void CountPlanStru(string planNum)
        {




            NewFrm.Show(this);

            SqlConnection sqlConnection1 = new SqlConnection(SQLDatabase.Connectstring);
            SqlCommand cmd = new SqlCommand();



            cmd.Parameters.Add("@nowplan", SqlDbType.VarChar);
            cmd.Parameters["@nowplan"].Value = planNum;

            cmd.Parameters.Add("@markdate", SqlDbType.DateTime);
            //cmd.Parameters["@markdate"].Value = this.dateTimePicker1.Value;
            cmd.Parameters["@markdate"].Value = this.计划日期DateTimePicker.Value;

            cmd.CommandText = "LY_payable_mainplan";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = sqlConnection1;
            cmd.CommandTimeout = 0;

            sqlConnection1.Open();
            cmd.ExecuteNonQuery();
            sqlConnection1.Close();

            this.ly_payable_itemsTableAdapter.Fill(this.lYFinancialMange.ly_payable_items, planNum);

            NewFrm.Hide(this);
        }

        private void 付款计划自动生成ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (null == this.ly_payable_planDataGridView.CurrentRow) return;

            string planNum = this.ly_payable_planDataGridView.CurrentRow.Cells["计划编码"].Value.ToString();

            if (Check_ifApproved("department"))
            {

                MessageBox.Show("计划已经批准,不能修改数据...", "注意");
                return;
            }

            if (this.formState != "View") return;





            //////////////////////////////////

            string message = "计算物料付款计划吗？";
            string caption = "提示...";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;

            DialogResult result;



            result = MessageBox.Show(message, caption, buttons,
            MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

            if (result == DialogResult.Yes)
            {


                CountPlanStru(planNum);



            }
        }

        //private void fillToolStripButton_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        this.ly_payable_itemsTableAdapter.Fill(this.lYFinancialMange.ly_payable_items, payable_plan_numToolStripTextBox.Text);
        //    }
        //    catch (System.Exception ex)
        //    {
        //        System.Windows.Forms.MessageBox.Show(ex.Message);
        //    }

        //}

        //private void fillToolStripButton1_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        this.ly_payable_item_detailTableAdapter.Fill(this.lYFinancialMange.ly_payable_item_detail, supplier_codeToolStripTextBox.Text);
        //    }
        //    catch (System.Exception ex)
        //    {
        //        System.Windows.Forms.MessageBox.Show(ex.Message);
        //    }

        //}

        private void ly_payable_planDataGridView_SelectionChanged(object sender, EventArgs e)
        {

            //if (null == this.ly_payable_planDataGridView.CurrentRow) return;

            //string planNum = this.ly_payable_planDataGridView.CurrentRow.Cells["计划编码"].Value.ToString();

            //this.ly_payable_itemsTableAdapter.Fill(this.lYFinancialMange.ly_payable_items, planNum);

            //AddSummationRow_New2(ly_payable_itemsBindingSource, ly_payable_itemsDataGridView);
        }

        private void ly_payable_itemsDataGridView_SelectionChanged(object sender, EventArgs e)
        {
            RefreshDetail();
        }

        private void RefreshDetail()
        {
            if (null == this.ly_payable_itemsDataGridView.CurrentRow) return;

            string supplierNum = this.ly_payable_itemsDataGridView.CurrentRow.Cells["供应商码"].Value.ToString();
            string planNum = this.ly_payable_itemsDataGridView.CurrentRow.Cells["计划编码s"].Value.ToString();

          

            //this.ly_payable_itemsTableAdapter.Fill(this.lYFinancialMange.ly_payable_items, planNum);
            //this.ly_payable_itemsBindingSource.Position = this.ly_payable_itemsBindingSource.Find("供应商代码", supplierNum);

            this.ly_payable_item_detail1TableAdapter.Fill(this.lYFinancialMange.ly_payable_item_detail1, planNum, supplierNum);

           
        }

        //private void toolStripLabel2_Click(object sender, EventArgs e)
        //{
        //    if ("隐藏计划界面" == toolStripButton2.Text)
        //    {
        //        toolStripButton2.Text = "显示计划界面";
        //        this.splitContainer2.Panel1Collapsed = true;
        //    }
        //    else
        //    {
        //        toolStripButton2.Text = "隐藏计划界面";
        //        this.splitContainer2.Panel1Collapsed = false;
        //    }
        //}

        private void toolStripButton8_Click(object sender, EventArgs e)
        {
            if ("隐藏计划界面" == toolStripButton8.Text)
            {
                toolStripButton8.Text = "显示计划界面";
                this.splitContainer2.Panel1Collapsed = true;
            }
            else
            {
                toolStripButton8.Text = "隐藏计划界面";
                this.splitContainer2.Panel1Collapsed = false;
            }
        }

        private void ly_PrepaymentDataGridView_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            DataGridView dgv = sender as DataGridView;

            if (null == dgv.CurrentRow) return;


            //if (this.ly_sales_contract_mainDataGridView.CurrentRow.Cells["录入人c"].Value.ToString() != SQLDatabase.nowUserName())
            //{

            //    MessageBox.Show("请" + this.ly_sales_contract_mainDataGridView.CurrentRow.Cells["录入人c"].Value.ToString() + "修改...", "注意");

            //    return;
            //}


            if ("付款" == dgv.CurrentCell.OwningColumn.Name)
            {

                if (!SQLDatabase.CheckHaveRight(SQLDatabase.NowUserID, "采购预付付款确认"))
                {
                    MessageBox.Show("无采购预付付款确认权限...", "注意");

                    return;

                }

                if ("True" == dgv.CurrentRow.Cells["审批1"].Value.ToString() && "True" == dgv.CurrentRow.Cells["审定1"].Value.ToString())
                {
                    if ("True" == dgv.CurrentRow.Cells["付款"].Value.ToString())
                    {
                        if (SQLDatabase.CheckHaveRight(SQLDatabase.NowUserID, "采购预付付款取消"))
                        {
                            dgv.CurrentRow.Cells["付款"].Value = "False";

                            dgv.CurrentRow.Cells["付款人"].Value = DBNull.Value;
                            dgv.CurrentRow.Cells["付款日期"].Value = DBNull.Value;
                        }
                        else
                        {

                            MessageBox.Show("无采购预付付款取消权限...", "注意");

                            return;
                        }



                    }
                    else
                    {

                        dgv.CurrentRow.Cells["付款"].Value = "True";

                        dgv.CurrentRow.Cells["付款人"].Value = SQLDatabase.nowUserName();
                        dgv.CurrentRow.Cells["付款日期"].Value = SQLDatabase.GetNowdate();

                    }


                    this.ly_PrepaymentDataGridView.EndEdit();
                    this.ly_PrepaymentBindingSource.EndEdit();

                    this.ly_PrepaymentTableAdapter.Update(this.lYFinancialMange.ly_Prepayment);



                    return;

                }
                else
                {
                    MessageBox.Show("必须审批和审定...", "注意");

                    return;
                }

              

            }
        }




        private void ly_payable_planDataGridView_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            DataGridView dgv = sender as DataGridView;

            if (null == dgv.CurrentRow) return;



            //if (this.ly_sales_contract_mainDataGridView.CurrentRow.Cells["录入人c"].Value.ToString() != SQLDatabase.nowUserName())
            //{

            //    MessageBox.Show("请" + this.ly_sales_contract_mainDataGridView.CurrentRow.Cells["录入人c"].Value.ToString() + "修改...", "注意");

            //    return;
            //}


            if ("部门批准" == dgv.CurrentCell.OwningColumn.Name)
            {

                if (!SQLDatabase.CheckHaveRight(SQLDatabase.NowUserID, "采购付款计划批准"))
                {
                    MessageBox.Show("无采购付款计划批准权限...", "注意");

                    return;

                }

                if ("True" == dgv.CurrentRow.Cells["公司批准"].Value.ToString())
                {
                    MessageBox.Show("已经公司批准，不能修改部门批准...", "注意");

                }

                if (Check_ifApproved("company"))
                {

                    MessageBox.Show("计划公司批准,不能修改数据...", "注意");
                    return;
                }

                if (Check_ifApproved("fin"))
                {

                    MessageBox.Show("计划已经确认付款,不能修改数据...", "注意");
                    return;
                }

                if ("True" == dgv.CurrentRow.Cells["部门批准"].Value.ToString())
                {
                    dgv.CurrentRow.Cells["部门批准"].Value = "False";

                    dgv.CurrentRow.Cells["部门批准人"].Value = DBNull.Value;
                    dgv.CurrentRow.Cells["部门批准日期"].Value = DBNull.Value;

                }
                else
                {

                    dgv.CurrentRow.Cells["部门批准"].Value = "True";

                    dgv.CurrentRow.Cells["部门批准人"].Value = SQLDatabase.nowUserName();
                    dgv.CurrentRow.Cells["部门批准日期"].Value = SQLDatabase.GetNowdate();

                }


                this.ly_payable_planDataGridView.EndEdit();
                this.ly_payable_planBindingSource.EndEdit();

                this.ly_payable_planTableAdapter.Update(this.lYFinancialMange.ly_payable_plan);



                return;

            }

            if ("公司批准" == dgv.CurrentCell.OwningColumn.Name)
            {

                if (!SQLDatabase.CheckHaveRight(SQLDatabase.NowUserID, "采购付款计划审定"))
                {
                    MessageBox.Show("无采购付款计划审定权限...", "注意");

                    return;

                }

                if (!Check_ifApproved("department"))
                {

                    MessageBox.Show("计划未经部门批准,不能修改数据...", "注意");
                    return;
                }

                if (Check_ifApproved("fin"))
                {

                    MessageBox.Show("计划已经付款,不能修改数据...", "注意");
                    return;
                }

                if ("True" == dgv.CurrentRow.Cells["公司批准"].Value.ToString())
                {
                    dgv.CurrentRow.Cells["公司批准"].Value = "False";

                    dgv.CurrentRow.Cells["公司批准人"].Value = DBNull.Value;
                    dgv.CurrentRow.Cells["公司批准日期"].Value = DBNull.Value;

                }
                else
                {

                    dgv.CurrentRow.Cells["公司批准"].Value = "True";

                    dgv.CurrentRow.Cells["公司批准人"].Value = SQLDatabase.nowUserName();
                    dgv.CurrentRow.Cells["公司批准日期"].Value = SQLDatabase.GetNowdate();

                }


                this.ly_payable_planDataGridView.EndEdit();
                this.ly_payable_planBindingSource.EndEdit();

                this.ly_payable_planTableAdapter.Update(this.lYFinancialMange.ly_payable_plan);



                return;

            }

            if ("付款确认" == dgv.CurrentCell.OwningColumn.Name)
            {

                if (!SQLDatabase.CheckHaveRight(SQLDatabase.NowUserID, "采购付款计划确认"))
                {
                    MessageBox.Show("无采购付款计划确认权限...", "注意");
                    return;

                }

                if (!Check_ifApproved("company"))
                {

                    MessageBox.Show("计划未经公司批准,不能付款确认...", "注意");
                    return;
                }

                if ("True" == dgv.CurrentRow.Cells["付款确认"].Value.ToString())
                {
                    

                    if (SQLDatabase.CheckHaveRight(SQLDatabase.NowUserID, "采购预付付款取消"))
                    {
                        dgv.CurrentRow.Cells["付款确认"].Value = "False";

                        dgv.CurrentRow.Cells["付款确认人"].Value = DBNull.Value;
                        dgv.CurrentRow.Cells["付款确认日期"].Value = DBNull.Value;
                    }
                    else
                    {

                        MessageBox.Show("无采购付款确认取消权限...", "注意");

                        return;
                    }

                }
                else
                {

                    dgv.CurrentRow.Cells["付款确认"].Value = "True";

                    dgv.CurrentRow.Cells["付款确认人"].Value = SQLDatabase.nowUserName();
                    dgv.CurrentRow.Cells["付款确认日期"].Value = SQLDatabase.GetNowdate();

                }


                this.ly_payable_planDataGridView.EndEdit();
                this.ly_payable_planBindingSource.EndEdit();

                this.ly_payable_planTableAdapter.Update(this.lYFinancialMange.ly_payable_plan);



                return;

            }
        }

        //private void fillToolStripButton_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        this.ly_payable_item_bindTableAdapter.Fill(this.lYFinancialMange.ly_payable_item_bind, payable_plan_numToolStripTextBox.Text, supplier_codeToolStripTextBox.Text, contract_codeToolStripTextBox.Text);
        //    }
        //    catch (System.Exception ex)
        //    {
        //        System.Windows.Forms.MessageBox.Show(ex.Message);
        //    }

        //}

        private void ly_payable_item_detailDataGridView_SelectionChanged(object sender, EventArgs e)
        {
            if (null == ly_payable_item_detailDataGridView.CurrentRow)
            {
                this.ly_payable_item_bindTableAdapter.Fill(this.lYFinancialMange.ly_payable_item_bind, "aaa", "aaa", "aaa");
                //this.lY_payable_contractTableAdapter.Fill(this.lYFinancialMange.LY_payable_contract, this.dateTimePicker1.Value, "sss");
                return;
            }

            string nowplanCode = ly_payable_item_detailDataGridView.CurrentRow.Cells["计划编码D"].Value.ToString();
            string nowsupptlerCode = ly_payable_item_detailDataGridView.CurrentRow.Cells["supplier_financial_code"].Value.ToString();
            string nowcontractCode = ly_payable_item_detailDataGridView.CurrentRow.Cells["合同编码D"].Value.ToString();

            this.ly_payable_item_bindTableAdapter.Fill(this.lYFinancialMange.ly_payable_item_bind, nowplanCode, nowcontractCode, nowsupptlerCode);
        }

        private void 增加供应商ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddSupplierSingle();
        }

        private void AddSupplierSingle()
        {
            if (null == this.ly_payable_planDataGridView.CurrentRow) return;

            if (!SQLDatabase.CheckHaveRight(SQLDatabase.NowUserID, "应付计划增加"))
            {
                MessageBox.Show("无应付计划增加权限...", "注意");

                return;

            }

            string planNum = this.ly_payable_planDataGridView.CurrentRow.Cells["计划编码"].Value.ToString();

            if (Check_ifApproved("department"))
            {

                MessageBox.Show("计划已经批准,不能修改数据...", "注意");
                return;
            }

            string sel = " select supplier_code as 供应商码,supplier_name as 供应商,bind_money as 发票金额,prepay as 预付金额,paymoney as 已付金额,notpay as 未付金额 FROM ly_payable_supplier_forplan_view where supplier_code not in  ( select supplier_code from ly_payable_items where payable_plan_num ='" + planNum + "') ";

            QueryForm queryForm = new QueryForm();


            queryForm.Sel = sel;
            queryForm.Constr = SQLDatabase.Connectstring;

            //Set the Column Collection to the filter Table
            //queryForm.SetSourceColumns(this.billMainDataSet.BalanceBill.Columns);

            queryForm.ShowDialog();

            if (string.IsNullOrEmpty(queryForm.Result))
            {

                return;
            }
            //NewFrm.Show(this); 2018-04-03


            NewFrm.Show(this);

            SqlConnection sqlConnection1 = new SqlConnection(SQLDatabase.Connectstring);
            SqlCommand cmd = new SqlCommand();



            cmd.Parameters.Add("@nowplan", SqlDbType.VarChar);
            cmd.Parameters["@nowplan"].Value = planNum;

            cmd.Parameters.Add("@nowsupplier", SqlDbType.VarChar);
            cmd.Parameters["@nowsupplier"].Value = queryForm.Result;


            cmd.Parameters.Add("@markdate", SqlDbType.DateTime);
            //cmd.Parameters["@markdate"].Value = this.dateTimePicker1.Value;
            cmd.Parameters["@markdate"].Value = this.计划日期DateTimePicker.Value;


            cmd.CommandText = "LY_payable_AddSupplierSingle";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = sqlConnection1;
            cmd.CommandTimeout = 0;

            sqlConnection1.Open();
            cmd.ExecuteNonQuery();
            sqlConnection1.Close();


            this.ly_payable_itemsTableAdapter.Fill(this.lYFinancialMange.ly_payable_items, planNum);



            //ly_material_plan_explodeTableAdapter.Fill(this.lYPlanMange.ly_material_plan_explode, parentId);
            //this.ly_store_planitemcountTableAdapter.Fill(this.lYPlanMange.ly_store_planitemcount, parentId);
            //this.ly_plan_getmaterial_departmentTableAdapter.Fill(this.lYPlanMange.ly_plan_getmaterial_department, planNum);

            NewFrm.Hide(this);

            AddSummationRow_New2(ly_payable_itemsBindingSource, ly_payable_itemsDataGridView);
        }

        private void 删除供应商ToolStripMenuItem_Click(object sender, EventArgs e)
        {

            DelSupplierSingle();
        }

        private void DelSupplierSingle()
        {
            //if (null == this.ly_payable_planDataGridView.CurrentRow) return;

            if (!SQLDatabase.CheckHaveRight(SQLDatabase.NowUserID, "应付计划增加")
                && !ly_payable_itemsDataGridView.CurrentRow.Cells["buyer_now"].Value.ToString().Contains(SQLDatabase.nowUserName())
                )
            {
                MessageBox.Show("无应付计划增加权限...", "注意");

                return;

            }

            if (Check_ifApproved("department"))
            {

                MessageBox.Show("计划已经批准,不能修改数据...", "注意");
                return;
            }


            string message = "确定删除当前记录吗？";
            string caption = "提示...";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result;



            result = MessageBox.Show(message, caption, buttons,
            MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

            if (result == DialogResult.Yes)
            {


                foreach (DataGridViewRow dgr in ly_payable_itemsDataGridView.Rows)
                {
                    if (true == dgr.Selected
                        &&
                        (SQLDatabase.CheckHaveRight(SQLDatabase.NowUserID, "应付计划增加")
                          || dgr.Cells["buyer_now"].Value.ToString().Contains(SQLDatabase.nowUserName())
                         )
                       )


                    {
                        string id = dgr.Cells["id_plan"].Value.ToString();
                        using (SqlConnection con = new SqlConnection(SQLDatabase.Connectstring))
                        {
                            string sql = "DELETE FROM ly_payable_items WHERE id =" + id;
                            using (SqlCommand cmd = new SqlCommand(sql, con))
                            {
                                con.Open();
                                cmd.ExecuteNonQuery();
                            }
                        }
                    }
                }

                if (null == this.ly_payable_planDataGridView.CurrentRow) return;

                string planNum = this.ly_payable_planDataGridView.CurrentRow.Cells["计划编码"].Value.ToString();

                this.ly_payable_itemsTableAdapter.Fill(this.lYFinancialMange.ly_payable_items, planNum);
                AddSummationRow_New2(ly_payable_itemsBindingSource, ly_payable_itemsDataGridView);
            }
        }

    
        private void SaveChanged()
        {

            this.ly_payable_itemsBindingSource.EndEdit();
            try
            {
                this.ly_payable_itemsTableAdapter.Update(this.lYFinancialMange.ly_payable_items);
            }
            catch (SqlException sqle)
            {


                MessageBox.Show(sqle.Message);
            }
            finally
            {

                RefreshDetail();
            }

            if (null == this.ly_payable_itemsDataGridView.CurrentRow) return;

            string supplierNum = this.ly_payable_itemsDataGridView.CurrentRow.Cells["供应商码"].Value.ToString();
            string planNum = this.ly_payable_itemsDataGridView.CurrentRow.Cells["计划编码s"].Value.ToString();



            this.ly_payable_itemsTableAdapter.Fill(this.lYFinancialMange.ly_payable_items, planNum);
            this.ly_payable_itemsBindingSource.Position = this.ly_payable_itemsBindingSource.Find("供应商代码", supplierNum);
            AddSummationRow_New2(ly_payable_itemsBindingSource, ly_payable_itemsDataGridView);
            // this.ly_payable_item_detailTableAdapter.Fill(this.lYFinancialMange.ly_payable_item_detail, supplierNum, planNum);
        }

        private void ly_payable_itemsDataGridView_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (null == this.ly_payable_planDataGridView.CurrentRow) return;

            string planNum = this.ly_payable_planDataGridView.CurrentRow.Cells["计划编码"].Value.ToString();

            DataGridView dgv = sender as DataGridView;


            //if (!SQLDatabase.CheckHaveRight(SQLDatabase.NowUserID, "采购付款计划批准"))
            //{
            //    MessageBox.Show("无采购付款计划批准权限...", "注意");

            //    return;

            //}

            decimal noPayInvoice;
            if (!string.IsNullOrEmpty(dgv.CurrentRow.Cells["开票未付"].Value.ToString()))
            {
                noPayInvoice = decimal.Parse(dgv.CurrentRow.Cells["开票未付"].Value.ToString());
            }
            else
            {
                noPayInvoice = 0;
            }

            decimal noPayPeriod;
            if (!string.IsNullOrEmpty(dgv.CurrentRow.Cells["到期未付"].Value.ToString()))
            {
                noPayPeriod = decimal.Parse(dgv.CurrentRow.Cells["到期未付"].Value.ToString());
            }
            else
            {
                noPayPeriod = 0;
            }



            decimal preparePay = 0;


            if (SQLDatabase.CheckHaveRight(SQLDatabase.NowUserID, "应付计划制定"))
            {


                if ("计划付款" == dgv.CurrentCell.OwningColumn.Name)
                {

                    if (Check_ifApproved("department"))
                    {

                        MessageBox.Show("计划已经批准,不能修改数据...", "注意");
                        return;
                    }

                    if (!SQLDatabase.CheckHaveRight(SQLDatabase.NowUserID, "采购付款计划批准"))
                    {

                        if (ly_payable_item_detail1BindingSource.Count > 1)
                        {
                            if (0 > this.ly_payable_item_detail1BindingSource.Find("采购员", SQLDatabase.nowUserName()))
                            {

                                MessageBox.Show("采购员供应商不匹配,操作取消...", "注意");
                                return;
                            }
                        }

                        if (ly_payable_item_detail1BindingSource.Count == 1)
                        {
                            if (0 > this.ly_payable_item_detail1BindingSource.Find("采购员", "期初"))
                            {

                                if (0 > this.ly_payable_item_detail1BindingSource.Find("采购员", SQLDatabase.nowUserName()))
                                {

                                    MessageBox.Show("采购员供应商不匹配,操作取消...", "注意");
                                    return;
                                }
                            }
                        }
                    }





                    ChangeValue queryForm = new ChangeValue();

                    queryForm.OldValue = dgv.CurrentCell.Value.ToString();
                    queryForm.NewValue = "";
                    queryForm.ChangeMode = "value";
                    queryForm.ShowDialog();




                    if (queryForm.NewValue != "")
                    {
                        preparePay = decimal.Parse(queryForm.NewValue);


                        if (SQLDatabase.CheckHaveRight(SQLDatabase.NowUserID, "采购付款计划批准"))
                        {
                            if (preparePay > decimal.Round(noPayInvoice,2))
                            {

                                MessageBox.Show("付款申请超过开票未付,操作取消...", "注意");

                                return;
                            }

                        }
                        else
                        {

                            if (preparePay > noPayPeriod)
                            {

                                MessageBox.Show("付款申请超过到期未付,操作取消...", "注意");

                                return;
                            }
                        }



                        if (queryForm.NewValue != queryForm.OldValue)
                        {
                            dgv.CurrentRow.Cells["计划付款"].Value = queryForm.NewValue;
                            dgv.CurrentRow.Cells["财务实付"].Value = queryForm.NewValue;
                            dgv.CurrentRow.Cells["承兑计划"].Value = DBNull.Value;
                            dgv.CurrentRow.Cells["转账计划"].Value = DBNull.Value;
                            dgv.CurrentRow.Cells["实付承兑"].Value = DBNull.Value;
                            dgv.CurrentRow.Cells["实付转账"].Value = DBNull.Value;

                            SaveChanged();

                        }



                    }
                    else
                    {
                        //dgv.CurrentRow.Cells["计划付款"].Value = DBNull.Value;
                        //dgv.CurrentRow.Cells["财务实付"].Value = DBNull.Value;
                        //dgv.CurrentRow.Cells["承兑计划"].Value = DBNull.Value;
                        //dgv.CurrentRow.Cells["转账计划"].Value = DBNull.Value;
                        //dgv.CurrentRow.Cells["实付承兑"].Value = DBNull.Value;
                        //dgv.CurrentRow.Cells["实付转账"].Value = DBNull.Value;


                        //SaveChanged();

                    }
                    return;

                }

                /////////////////////////////////////////////////////

                if ("承兑计划" == dgv.CurrentCell.OwningColumn.Name)
                {
                    if (Check_ifApproved("department"))
                    {

                        MessageBox.Show("计划已经批准,不能修改数据...", "注意");
                        return;
                    }


                    ChangeValue queryForm = new ChangeValue();

                    queryForm.OldValue = dgv.CurrentCell.Value.ToString();
                    queryForm.NewValue = "";
                    queryForm.ChangeMode = "value";
                    queryForm.ShowDialog();

                    decimal planpay = 0;
                    decimal billpay = 0;

                    if (!string.IsNullOrEmpty(dgv.CurrentRow.Cells["计划付款"].Value.ToString()))
                    {
                        planpay = decimal.Round(decimal.Parse(dgv.CurrentRow.Cells["计划付款"].Value.ToString()),2);
                        
                    }


                    if (queryForm.NewValue != "")
                    {
                        if (queryForm.NewValue != queryForm.OldValue)
                        {
                            billpay = decimal.Parse(queryForm.NewValue);

                            if (billpay > planpay)
                            {

                                MessageBox.Show("承兑金额大于计划金额,操作取消...", "注意");
                                return;
                            }

                            //dgv.CurrentRow.Cells["计划付款"].Value = queryForm.NewValue;
                            //dgv.CurrentRow.Cells["财务实付"].Value = queryForm.NewValue;
                            dgv.CurrentRow.Cells["承兑计划"].Value = queryForm.NewValue;
                            dgv.CurrentRow.Cells["转账计划"].Value = planpay - billpay;
                            dgv.CurrentRow.Cells["实付承兑"].Value = queryForm.NewValue;
                            dgv.CurrentRow.Cells["实付转账"].Value = planpay - billpay;

                            SaveChanged();

                        }



                    }
                    else
                    {
                        //dgv.CurrentRow.Cells["计划付款"].Value = DBNull.Value;
                        //dgv.CurrentRow.Cells["财务实付"].Value = DBNull.Value;
                        //dgv.CurrentRow.Cells["承兑计划"].Value = DBNull.Value;
                        //dgv.CurrentRow.Cells["转账计划"].Value = DBNull.Value;
                        //dgv.CurrentRow.Cells["实付承兑"].Value = DBNull.Value;
                        //dgv.CurrentRow.Cells["实付转账"].Value = DBNull.Value;


                        SaveChanged();

                    }
                    return;

                }
            }

            /////////////////////////////////////////////////////

            if (SQLDatabase.CheckHaveRight(SQLDatabase.NowUserID, "财务付款权限"))
            {
                //if (Check_ifApproved("company"))
                //{

                //    MessageBox.Show("计划已经公司批准,不能修改数据...", "注意");
                //    return;
                //}

                if (Check_ifApproved("fin"))
                {

                    MessageBox.Show("计划已经付款确认,不能修改数据...", "注意");
                    return;
                }
                if (!Check_ifApproved("department"))
                {

                    MessageBox.Show("计划未经部门批准,不能修改财务数据...", "注意");
                    return;
                }

                if ("实付承兑" == dgv.CurrentCell.OwningColumn.Name)
                {

                    ChangeValue queryForm = new ChangeValue();

                    queryForm.OldValue = dgv.CurrentCell.Value.ToString();
                    queryForm.NewValue = "";
                    queryForm.ChangeMode = "value";
                    queryForm.ShowDialog();

                    decimal planpay = 0;
                    decimal billpay = 0;

                    if (!string.IsNullOrEmpty(dgv.CurrentRow.Cells["财务实付"].Value.ToString()))
                    {
                        planpay = decimal.Parse(dgv.CurrentRow.Cells["财务实付"].Value.ToString());

                    }


                    if (queryForm.NewValue != "")
                    {
                        if (queryForm.NewValue != queryForm.OldValue)
                        {
                            billpay = decimal.Parse(queryForm.NewValue);

                            if (billpay > planpay)
                            {

                                MessageBox.Show("实付承兑大于计划金额,操作取消...", "注意");
                                return;
                            }

                            //dgv.CurrentRow.Cells["计划付款"].Value = queryForm.NewValue;
                            //dgv.CurrentRow.Cells["财务实付"].Value = queryForm.NewValue;
                            //dgv.CurrentRow.Cells["承兑计划"].Value = queryForm.NewValue;
                            //dgv.CurrentRow.Cells["转账计划"].Value = planpay - billpay;
                            dgv.CurrentRow.Cells["实付承兑"].Value = queryForm.NewValue;
                            dgv.CurrentRow.Cells["实付转账"].Value = planpay - billpay;

                            SaveChanged();

                        }



                    }
                    else
                    {
                        //dgv.CurrentRow.Cells["计划付款"].Value = DBNull.Value;
                        //dgv.CurrentRow.Cells["财务实付"].Value = DBNull.Value;
                        //dgv.CurrentRow.Cells["承兑计划"].Value = DBNull.Value;
                        //dgv.CurrentRow.Cells["转账计划"].Value = DBNull.Value;
                        dgv.CurrentRow.Cells["实付承兑"].Value = DBNull.Value;
                        dgv.CurrentRow.Cells["实付转账"].Value = DBNull.Value;


                        SaveChanged();

                    }
                    return;

                }

                if ("财务实付" == dgv.CurrentCell.OwningColumn.Name)
                {

                    ChangeValue queryForm = new ChangeValue();

                    queryForm.OldValue = dgv.CurrentCell.Value.ToString();
                    queryForm.NewValue = "";
                    queryForm.ChangeMode = "value";
                    queryForm.ShowDialog();

                    decimal planpay = 0;
                    decimal billpay = 0;

                    //if (!string.IsNullOrEmpty(dgv.CurrentRow.Cells["计划付款"].Value.ToString()))
                    //{
                    //    planpay = decimal.Parse(dgv.CurrentRow.Cells["计划付款"].Value.ToString());

                    //}


                    if (queryForm.NewValue != "")
                    {
                        if (queryForm.NewValue != queryForm.OldValue)
                        {
                            //billpay = decimal.Parse(queryForm.NewValue);

                            //if (billpay > planpay)
                            //{

                            //    MessageBox.Show("实付承兑大于计划金额,操作取消...", "注意");
                            //    return;
                            //}

                            //dgv.CurrentRow.Cells["计划付款"].Value = queryForm.NewValue;
                            dgv.CurrentRow.Cells["财务实付"].Value = queryForm.NewValue;
                            //dgv.CurrentRow.Cells["承兑计划"].Value = queryForm.NewValue;
                            //dgv.CurrentRow.Cells["转账计划"].Value = planpay - billpay;
                            dgv.CurrentRow.Cells["实付承兑"].Value = DBNull.Value;
                            dgv.CurrentRow.Cells["实付转账"].Value = DBNull.Value;

                            SaveChanged();

                        }



                    }
                    else
                    {
                        //dgv.CurrentRow.Cells["计划付款"].Value = DBNull.Value;
                        dgv.CurrentRow.Cells["财务实付"].Value = DBNull.Value;
                        //dgv.CurrentRow.Cells["承兑计划"].Value = DBNull.Value;
                        //dgv.CurrentRow.Cells["转账计划"].Value = DBNull.Value;
                        dgv.CurrentRow.Cells["实付承兑"].Value = DBNull.Value;
                        dgv.CurrentRow.Cells["实付转账"].Value = DBNull.Value;


                        SaveChanged();

                    }
                    return;

                }
            }




            /////////////////////////////////////////////////////////////

            //if ("备注" == dgv.CurrentCell.OwningColumn.Name)
            //{
            //    ChangeValue queryForm = new ChangeValue();

            //    queryForm.OldValue = dgv.CurrentCell.Value.ToString();
            //    queryForm.NewValue = "";
            //    queryForm.ShowDialog();




            //    if (queryForm.NewValue != "")
            //    {
            //        dgv.CurrentRow.Cells["备注"].Value = queryForm.NewValue;
            //        SaveChanged();

            //    }
            //    else
            //    {


            //    }
            //    return;
            //}
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //NewFrm.Show(this);
            //Thread.Sleep(100);
            //this .ly_payable_itemsDataGridView.SelectionChanged -=
            this.ly_payable_planTableAdapter.Fill(this.lYFinancialMange.ly_payable_plan);
            //AddSummationRow_New2(ly_payable_itemsBindingSource, ly_payable_itemsDataGridView);

            //this.ly_payable_itemsDataGridView.SelectionChanged +=
            //NewFrm.Hide(this)   ;
        }

        private void ly_payable_item_detailDataGridView_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex >= 0)
                selectionIdx = e.RowIndex;
        }

        private void ly_payable_item_detailDataGridView_CellMouseMove(object sender, DataGridViewCellMouseEventArgs e)
        {
            DataGridView dgv = sender as DataGridView;

            if ((e.Clicks < 2) && (e.Button == MouseButtons.Left))
            {
                if ((e.ColumnIndex == -1) && (e.RowIndex > -1))
                    dgv.DoDragDrop(dgv.Rows[e.RowIndex], DragDropEffects.Move);
            }
        }
        private int GetRowFromPoint(DataGridView dgv, int x, int y)
        {
            for (int i = 0; i < dgv.RowCount; i++)
            {
                Rectangle rec = dgv.GetRowDisplayRectangle(i, false);

                if (dgv.RectangleToScreen(rec).Contains(x, y))
                    return i;
            }

            return -1;
        }
        private void ly_payable_item_detailDataGridView_DragDrop(object sender, DragEventArgs e)
        {
            DataGridView dgv = sender as DataGridView;



            int idx = GetRowFromPoint(dgv, e.X, e.Y);
            if (idx < 0) return;
            //index2 = idx;
            if (e.Data.GetDataPresent(typeof(DataGridViewRow)))
            {

                DataGridViewRow row = (DataGridViewRow)e.Data.GetData(typeof(DataGridViewRow));

                int tempOrder = row.Index;
                // this.gqis.Ins_Incontrol(idx, row.Cells[0].Value.ToString());



                //dgv.Rows[idx].Cells["顺序"].Value = tempOrder;
                //dgv.Rows[idx].Cells["顺序"].Value = tempOrder;

                if (idx > row.Index)
                {
                    foreach (DataGridViewRow dgvr in dgv.Rows)
                    {

                        if (dgvr.Index > row.Index && dgvr.Index <= idx)
                        {
                            dgvr.Cells["付款顺序"].Value = dgvr.Index;

                        }
                    }
                }
                if (idx < row.Index)
                {
                    foreach (DataGridViewRow dgvr in dgv.Rows)
                    {

                        if (dgvr.Index >= idx && dgvr.Index < row.Index)
                        {
                            dgvr.Cells["付款顺序"].Value = dgvr.Index + 2;

                        }
                    }
                }


                row.Cells["付款顺序"].Value = idx + 1;
                // dgv.Rows[idx].Cells["顺序"].Value = row.Index + 1;

                SaveDetailItem();



                dgv.Rows[idx].Selected = true;
                dgv.CurrentCell = dgv.Rows[idx].Cells["付款顺序"];


                //selectionIdx = idx;
            }
        }

        private void SaveDetailItem()
        {
            this.ly_payable_item_detail1BindingSource.EndEdit();
            this.ly_payable_item_detail1TableAdapter.Update(this.lYFinancialMange.ly_payable_item_detail1);

            int nowdetailId;
            if (null != ly_payable_item_detailDataGridView.CurrentRow)
            {
                nowdetailId = int.Parse(ly_payable_item_detailDataGridView.CurrentRow.Cells["id_detail"].Value.ToString());
            }
            else
            {
                nowdetailId = 0;
            }


            //int nowcontractId;
            //if (null != ly_sales_contract_mainDataGridView.CurrentRow)
            //{
            //    nowcontractId = int.Parse(ly_sales_contract_mainDataGridView.CurrentRow.Cells["id_main"].Value.ToString());
            //}
            //else
            //{
            //    nowcontractId = 0;
            //}



            //this.ly_sales_contract_mainDataGridView.EndEdit();
            //this.ly_sales_contract_main_forbusinessBindingSource.EndEdit();
            //this.ly_sales_contract_main_forbusinessTableAdapter.Update(this.lYSalseMange.ly_sales_contract_main_forbusiness);

            //this.ly_sales_contract_main_forbusinessTableAdapter.Fill(this.lYSalseMange.ly_sales_contract_main_forbusiness, this.nowcontractCode);
            //this.ly_sales_contract_main_forbusinessBindingSource.Position = this.ly_sales_contract_main_forbusinessBindingSource.Find("id", nowcontractId);

            //this.ly_sales_contract_main1DataGridView.SelectionChanged -= ly_sales_contract_main1DataGridView_SelectionChanged;
            //this.ly_sales_contract_main1TableAdapter.Fill(this.lYSalseMange.ly_sales_contract_main1, this.nowusercode, this.nowfillstragecode, this.dateTimePicker1.Value, this.dateTimePicker2.Value);
            //this.ly_sales_contract_main1DataGridView.SelectionChanged += ly_sales_contract_main1DataGridView_SelectionChanged;

            //this.ly_sales_contract_detailTableAdapter.Fill(this.lYSalseMange.ly_sales_contract_detail, nowinnerCode);

            RefreshDetail();

            this.ly_payable_item_detail1BindingSource.Position = this.ly_payable_item_detail1BindingSource.Find("id", nowdetailId);

            //this.ly_sales_contract_detailDataGridView.CurrentRow.Cells["单件折扣"].Value = this.ly_sales_contract_detailDataGridView.CurrentRow.Cells["单件折扣"].Value;
            //this.ly_sales_contract_detailDataGridView.CurrentRow.Cells["单价"].Value = this.ly_sales_contract_detailDataGridView.CurrentRow.Cells["单价"].Value;

            foreach (DataGridViewRow dgr in ly_payable_item_detailDataGridView.Rows)
            {
                dgr.Cells["付款顺序"].Value = dgr.Index + 1;

            }

            this.ly_payable_item_detailDataGridView.EndEdit();
            this.ly_payable_item_detail1BindingSource.EndEdit();
            this.ly_payable_item_detail1TableAdapter.Update(this.lYFinancialMange.ly_payable_item_detail1);
        }

        private void ly_payable_item_detailDataGridView_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;
        }

        private void bindingNavigatorAddNewItem1_Click(object sender, EventArgs e)
        {
            AddSupplierSingle();
        }

        private void bindingNavigatorDeleteItem1_Click(object sender, EventArgs e)
        {
            DelSupplierSingle();
        }

        private void 打印PToolStripButton_Click(object sender, EventArgs e)
        {
            if (ly_payable_itemsDataGridView.Rows.Count<=0) return;

            BaseReportView queryForm = new BaseReportView();
            queryForm.Text = "中原精密付款计划";
            queryForm.Printdata = this.lYFinancialMange;
            queryForm.PrintCrystalReport = new PrintPayPlan();
            queryForm.ShowDialog();
        }

        private void toolStripTextBox2_Enter(object sender, EventArgs e)
        {
            toolStripTextBox2.Text = "";
             
            this.ly_PrepaymentBindingSource.Filter = "审定=True";

            
        }

        private void toolStripTextBox2_KeyUp(object sender, KeyEventArgs e)
        {
            string filterString;


            filterString = GetDataGridviewMultiFilterString.DGVMultiFilterString(this.ly_PrepaymentDataGridView, this.toolStripTextBox2.Text);


            this.ly_PrepaymentBindingSource.Filter = "(" + filterString + ") and 审定 = True";
        }

        private void ly_payable_item_detailDataGridView_DragLeave(object sender, EventArgs e)
        {

        }

        private void toolStripTextBox3_KeyUp(object sender, KeyEventArgs e)
        {
            string filterString;


            filterString = GetDataGridviewMultiFilterString.DGVMultiFilterString(this.lY_payable_mainDataGridView, this.toolStripTextBox3.Text);


            this.lY_payable_mainBindingSource.Filter = "(" + filterString + ")  or supplier_code='_合计'";

            AddSummationRow_New(lY_payable_mainBindingSource, lY_payable_mainDataGridView);



            //this.ly_sales_contract_standard_ReportBindingSource.Filter = "(" + filterString + ") or 清单号='合计'";
            //AddSummationRow_New(ly_sales_contract_standard_ReportBindingSource, ly_sales_contract_standard_ReportDataGridView);
        }

        private void toolStripTextBox3_Enter(object sender, EventArgs e)
        {
            toolStripTextBox3.Text = "";

            this.lY_payable_mainBindingSource.Filter = "";

            AddSummationRow_New(lY_payable_mainBindingSource, lY_payable_mainDataGridView);
           
        }

        private void toolStripButton13_Click(object sender, EventArgs e)
        {
            NewFrm.Show(this);
            Thread.Sleep(100);

            this.lY_payable_periodTableAdapter.Fill(this.lYFinancialMange.LY_payable_period, this.dateTimePicker4.Value, this.dateTimePicker5.Value.AddDays(1));
            AddSummationRow_Period(lY_payable_periodBindingSource, lY_payable_periodDataGridView);

            NewFrm.Hide(this);
        }
        private void AddSummationRow_Period(BindingSource bs, DataGridView dgv)
        {
            //InitializeApp();
            //return;

            DataRow sumdr = (((DataSet)bs.DataSource).Tables[bs.DataMember]).NewRow();

           

            if (-1 != bs.Find("编码", "合计"))
            {
                bs.RemoveAt(bs.Find("编码", "合计"));
            }

            foreach (DataGridViewRow dgvRow in dgv.Rows)
            {
                foreach (DataGridViewCell dgvCell in dgvRow.Cells)
                {
                    //foreach (DataGridViewColumn dgvColumn in dgv.Columns)
                    //{
                    if (dgvCell.Value != null && !(dgvCell.Value is DBNull))
                    {
                        if (IsInteger(dgvCell.Value))
                        {
                            if ("年份" != dgvCell.OwningColumn.HeaderText && "月份" != dgvCell.OwningColumn.HeaderText)
                            {
                                if (sumdr[dgvCell.OwningColumn.DataPropertyName] == null || (sumdr[dgvCell.OwningColumn.DataPropertyName] is DBNull))
                                    sumdr[dgvCell.OwningColumn.DataPropertyName] = 0;


                                sumdr[dgvCell.OwningColumn.DataPropertyName] = Convert.ToInt64(sumdr[dgvCell.OwningColumn.DataPropertyName]) + Convert.ToInt64(dgvCell.Value);
                            }
                        }
                        else if (IsDecimal(dgvCell.Value))
                        {
                            if ("年份" != dgvCell.OwningColumn.HeaderText && "月份" != dgvCell.OwningColumn.HeaderText)
                            {
                                if (sumdr[dgvCell.OwningColumn.DataPropertyName] == null || (sumdr[dgvCell.OwningColumn.DataPropertyName] is DBNull))
                                    sumdr[dgvCell.OwningColumn.DataPropertyName] = 0;
                                //dgvCell .OwningColumn.Name  dgvCell.ColumnIndex

                                sumdr[dgvCell.OwningColumn.DataPropertyName] = Convert.ToDecimal(sumdr[dgvCell.OwningColumn.DataPropertyName]) + Convert.ToDecimal(dgvCell.Value);
                            }
                        }

                        //sumBox.Text = string.Format("{0}", sumBox.Tag);
                        //sumBox.Invalidate();

                    }
                    //}
                }

            }

            //sumdr["sumorder"] = "02";
            sumdr["编码"] = "合计";
            //sumdr["客户"] = "";
            //sumdr["税务"] = "";
            ((DataSet)bs.DataSource).Tables[bs.DataMember].Rows.Add(sumdr);
            bs.ResetBindings(true);

        }

        private void lY_payable_periodDataGridView_SelectionChanged(object sender, EventArgs e)
        {
            if (null == lY_payable_periodDataGridView.CurrentRow)
            {
                //this.ly_sales_contract_detailTableAdapter.Fill(this.lYSalseMange.ly_sales_contract_detail, "", 0);
                this.lY_payable_period_detailTableAdapter.Fill(this.lYFinancialMange.LY_payable_period_detail,  "sss", this.dateTimePicker4.Value, this.dateTimePicker5.Value.AddDays(1));
                return;
            }


            string nowsupplierCode = lY_payable_periodDataGridView.CurrentRow.Cells["编码"].Value.ToString();
            this.lY_payable_period_detailTableAdapter.Fill(this.lYFinancialMange.LY_payable_period_detail, nowsupplierCode, this.dateTimePicker4.Value, this.dateTimePicker5.Value.AddDays(1));
        }

        private void toolStripTextBox5_KeyUp(object sender, KeyEventArgs e)
        {
            string filterString;


            filterString = GetDataGridviewMultiFilterString.DGVMultiFilterString(this.lY_payable_periodDataGridView, this.toolStripTextBox5.Text);

            this.lY_payable_periodBindingSource.Filter = "(" + filterString + ")  or 编码='合计'";

            AddSummationRow_Period(lY_payable_periodBindingSource, lY_payable_periodDataGridView);
            

            //AddSummationRow_New(lY_payable_mainBindingSource, lY_payable_mainDataGridView);

        }

        private void toolStripTextBox5_Enter(object sender, EventArgs e)
        {
            toolStripTextBox5.Text = "";
            this.lY_payable_periodBindingSource.Filter = "";
            AddSummationRow_Period(lY_payable_periodBindingSource, lY_payable_periodDataGridView);
        }

        private void ly_payable_planDataGridView_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (null == this.ly_payable_planDataGridView.CurrentRow) return;



            //string planNum = this.ly_payable_planDataGridView.CurrentRow.Cells["计划编码"].Value.ToString();
            string planNum = this.ly_payable_planDataGridView.Rows[e.RowIndex].Cells["计划编码"].Value.ToString();

            //NewFrm.Show(this);
            //Thread.Sleep(100);


            this.ly_payable_itemsTableAdapter.Fill(this.lYFinancialMange.ly_payable_items, planNum);

            AddSummationRow_New2(ly_payable_itemsBindingSource, ly_payable_itemsDataGridView);

            //NewFrm.Hide(this);
        }

       

        private void toolStripButton26_Click(object sender, EventArgs e)
        {
            this.ly_supplier_listTableAdapter.Fill(this.lYMaterielRequirements.ly_supplier_list, "5");
        }

        private void toolStripTextBox7_KeyUp(object sender, KeyEventArgs e)
        {
            string filterString = GetDataGridviewMultiFilterString.DGVMultiFilterString(this.ly_supplier_listDataGridView, this.toolStripTextBox7.Text);


            if (null == filterString)
                filterString = "";


            this.ly_supplier_listBindingSource.Filter = filterString;
        }

        private void toolStripTextBox7_Enter(object sender, EventArgs e)
        {
            toolStripTextBox7.Text = "";

            this.ly_supplier_listBindingSource.Filter = "";
        }

        private void toolStripButton25_Click(object sender, EventArgs e)
        {
            LY_SupplierMange queryForm = new LY_SupplierMange();
            queryForm.WindowState = FormWindowState.Maximized;

            queryForm.Sortmode = "NP"; // "CG";

            queryForm.ShowDialog();

            if (queryForm.DialogResult != DialogResult.Cancel)
            {
                this.ly_supplier_listTableAdapter.Fill(this.lYMaterielRequirements.ly_supplier_list, "5");
                this.ly_supplier_listBindingSource.Position = this.ly_supplier_listBindingSource.Find("编码", queryForm.Nowsupplier_code);


            }
        }

        private void ly_supplier_listDataGridView_SelectionChanged(object sender, EventArgs e)
        {
            if (null == ly_supplier_listDataGridView.CurrentRow) return;
            string s = this.ly_supplier_listDataGridView.CurrentRow.Cells["编码NP"].Value.ToString();

            string nowsuppilercode = s;

            this.ly_Prepayment_NPTableAdapter.Fill(this.lYFinancialMange.ly_Prepayment_NP, nowsuppilercode);
            this.ly_actualpayment_NPTableAdapter.Fill(this.lYFinancialMange.ly_actualpayment_NP, nowsuppilercode);
            this.ly_payable_NPTableAdapter.Fill(this.lYFinancialMange.ly_payable_NP, nowsuppilercode);
        }

        private void toolStripButton32_Click(object sender, EventArgs e)
        {
            if (null == ly_supplier_listDataGridView.CurrentRow) return;
            string s = this.ly_supplier_listDataGridView.CurrentRow.Cells["编码NP"].Value.ToString();

            LY_add_Prepayment_NP queryForm = new LY_add_Prepayment_NP();

            queryForm.nowsuppilercode = s;
            queryForm.StartPosition = FormStartPosition.CenterParent;
            queryForm.ShowDialog();


            if (queryForm.DialogResult != DialogResult.Cancel)
            {
                this.ly_Prepayment_NPTableAdapter.Fill(this.lYFinancialMange.ly_Prepayment_NP, s);
                // this.lyinvoiceBindingSource.Position = this.lyinvoiceBindingSource.Find("invoice_code", queryForm.invoice_code_add);

            }
        }

        private void toolStripButton33_Click(object sender, EventArgs e)
        {
            if (ly_Prepayment_NPDataGridView.CurrentRow == null)
                return;
            if (!SQLDatabase.CheckHaveRight(SQLDatabase.NowUserID, "非采购供应商账款删除"))
            {
                MessageBox.Show("无删除权限", "注意");
                return;
            }
            else
            {
                string salespeople = this.ly_Prepayment_NPDataGridView.CurrentRow.Cells["支付人"].Value.ToString();
                if (!string.IsNullOrEmpty(salespeople))
                {
                    if (salespeople != SQLDatabase.nowUserName())
                    {
                        MessageBox.Show("请支付人:" + salespeople + "删除", "注意");
                        return;
                    }
                }
            }
            //if (ly_fpDataGridView.CurrentRow.Cells["lock_flag"].Value.ToString() == "True")
            //{
            //    MessageBox.Show("已经锁定无法操作...", "注意");
            //    return;
            //}

            //if (ly_invoice_detailDataGridView.Rows.Count > 0)
            //{
            //    MessageBox.Show("该发票已经绑定入库单...", "注意");
            //    return;
            //}
            string caption = "提示...";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;

            DialogResult result;

            string message = "确定要删除吗";

            result = MessageBox.Show(message, caption, buttons,
            MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

            if (result == DialogResult.Yes)
            {
                this.ly_Prepayment_NPBindingSource.RemoveCurrent();
                this.ly_Prepayment_NPTableAdapter.Update(this.lYFinancialMange.ly_Prepayment_NP);

            }
            else
            {
                return;
            }
        }

        private void toolStripButton39_Click(object sender, EventArgs e)
        {
            if (null == ly_supplier_listDataGridView.CurrentRow) return;
            string s = this.ly_supplier_listDataGridView.CurrentRow.Cells["编码NP"].Value.ToString();

            LY_add_actualpayment_NP queryForm = new LY_add_actualpayment_NP();

            queryForm.nowsuppilercode = s;
            queryForm.StartPosition = FormStartPosition.CenterParent;
            queryForm.ShowDialog();


            if (queryForm.DialogResult != DialogResult.Cancel)
            {
                this.ly_actualpayment_NPTableAdapter.Fill(this.lYFinancialMange.ly_actualpayment_NP, s);
                // this.lyinvoiceBindingSource.Position = this.lyinvoiceBindingSource.Find("invoice_code", queryForm.invoice_code_add);

            }
        }

        private void toolStripButton40_Click(object sender, EventArgs e)
        {
            if (ly_actualpayment_NPDataGridView.CurrentRow == null)
                return;
            if (!SQLDatabase.CheckHaveRight(SQLDatabase.NowUserID, "非采购供应商账款删除"))
            {
                MessageBox.Show("无删除权限", "注意");
                return;
            }
            else
            {
                string salespeople = this.ly_actualpayment_NPDataGridView.CurrentRow.Cells["支付人ac"].Value.ToString();
                if (!string.IsNullOrEmpty(salespeople))
                {
                    if (salespeople != SQLDatabase.nowUserName())
                    {
                        MessageBox.Show("请支付人:" + salespeople + "删除", "注意");
                        return;
                    }
                }
            }
            //if (ly_fpDataGridView.CurrentRow.Cells["lock_flag"].Value.ToString() == "True")
            //{
            //    MessageBox.Show("已经锁定无法操作...", "注意");
            //    return;
            //}

            //if (ly_invoice_detailDataGridView.Rows.Count > 0)
            //{
            //    MessageBox.Show("该发票已经绑定入库单...", "注意");
            //    return;
            //}
            string caption = "提示...";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;

            DialogResult result;

            string message = "确定要删除吗";

            result = MessageBox.Show(message, caption, buttons,
            MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

            if (result == DialogResult.Yes)
            {
                this.ly_actualpayment_NPBindingSource.RemoveCurrent();
                this.ly_actualpayment_NPTableAdapter.Update(this.lYFinancialMange.ly_actualpayment_NP);

            }
            else
            {
                return;
            }
        }

        private void toolStripButton46_Click(object sender, EventArgs e)
        {
            if (null == ly_supplier_listDataGridView.CurrentRow) return;
            string s = this.ly_supplier_listDataGridView.CurrentRow.Cells["编码NP"].Value.ToString();

            LY_add_payable_NP queryForm = new LY_add_payable_NP();

            queryForm.nowsuppilercode = s;
            queryForm.StartPosition = FormStartPosition.CenterParent;
            queryForm.ShowDialog();


            if (queryForm.DialogResult != DialogResult.Cancel)
            {
                this.ly_payable_NPTableAdapter.Fill(this.lYFinancialMange.ly_payable_NP, s);
                // this.lyinvoiceBindingSource.Position = this.lyinvoiceBindingSource.Find("invoice_code", queryForm.invoice_code_add);

            }
        }

        private void toolStripButton47_Click(object sender, EventArgs e)
        {
            if (ly_payable_NPDataGridView.CurrentRow == null)
                return;
            if (!SQLDatabase.CheckHaveRight(SQLDatabase.NowUserID, "非采购供应商账款删除"))
            {
                MessageBox.Show("无删除权限", "注意");
                return;
            }
            else
            {
                string salespeople = this.ly_payable_NPDataGridView.CurrentRow.Cells["录入人"].Value.ToString();
                if (!string.IsNullOrEmpty(salespeople))
                {
                    if (salespeople != SQLDatabase.nowUserName())
                    {
                        MessageBox.Show("请录入人:" + salespeople + "删除", "注意");
                        return;
                    }
                }
            }
            //if (ly_fpDataGridView.CurrentRow.Cells["lock_flag"].Value.ToString() == "True")
            //{
            //    MessageBox.Show("已经锁定无法操作...", "注意");
            //    return;
            //}

            //if (ly_invoice_detailDataGridView.Rows.Count > 0)
            //{
            //    MessageBox.Show("该发票已经绑定入库单...", "注意");
            //    return;
            //}
            string caption = "提示...";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;

            DialogResult result;

            string message = "确定要删除吗";

            result = MessageBox.Show(message, caption, buttons,
            MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

            if (result == DialogResult.Yes)
            {
                this.ly_payable_NPBindingSource.RemoveCurrent();
                this.ly_payable_NPTableAdapter.Update(this.lYFinancialMange.ly_payable_NP);

            }
            else
            {
                return;
            }
        }

        private void toolStripButton52_Click(object sender, EventArgs e)
        {


            this.lY_payable_detail_allTableAdapter.Fill(this.lYFinancialMange.LY_payable_detail_all, this.dateTimePicker1.Value);
            AddSummationRow_New3(lY_payable_detail_allBindingSource, lY_payable_detail_allDataGridView);
            //if (null == this.lY_payable_mainDataGridView.CurrentRow) return;

            //string nowsupplierCode;
            //string noename;

            //NewFrm.Show(this);
            //foreach (DataGridViewRow dgr in lY_payable_mainDataGridView.Rows)
            //{
            //    nowsupplierCode = dgr.Cells["supplier_code"].Value.ToString();
            //    noename = dgr.Cells["supplier_name"].Value.ToString();



            //    //this.toolStripLabel3.Text = plannum;
            //    //this.toolStripLabel3.Invalidate();

            //    NewFrm.Notify(this, "正在处理:  (" + nowsupplierCode + ")" + noename + "   发票付款");



            //    Countinvoicemoney(nowsupplierCode, this.dateTimePicker1.Value);

            //}

            //NewFrm.Hide(this);

            //this.ly_inma0010cpTableAdapter.Fill(this.lYMaterialMange.ly_inma0010cp);



        }

        private static void Countinvoicemoney(string nowsupplierCode,DateTime markdate)
        {

           


            SqlConnection sqlConnection1 = new SqlConnection(SQLDatabase.Connectstring);
            SqlCommand cmd = new SqlCommand();

            cmd.Parameters.Add("@markdate", SqlDbType.DateTime);
            cmd.Parameters["@markdate"].Value = markdate ;



            cmd.Parameters.Add("@supplier_code", SqlDbType.VarChar);
            cmd.Parameters["@supplier_code"].Value = nowsupplierCode;


            cmd.CommandText = "LY_payable_payInvoice";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandTimeout = 0;
            cmd.Connection = sqlConnection1;

            sqlConnection1.Open();
            cmd.ExecuteNonQuery();
            sqlConnection1.Close();


           
        }

        //private void fillToolStripButton_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        this.lY_payable_detail_allTableAdapter.Fill(this.lYFinancialMange.LY_payable_detail_all, new System.Nullable<System.DateTime>(((System.DateTime)(System.Convert.ChangeType(markdateToolStripTextBox.Text, typeof(System.DateTime))))));
        //    }
        //    catch (System.Exception ex)
        //    {
        //        System.Windows.Forms.MessageBox.Show(ex.Message);
        //    }

        //}

        //private void fillToolStripButton1_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        this.lY_payable_PrepayNoincoiceTableAdapter.Fill(this.lYFinancialMange.LY_payable_PrepayNoincoice, new System.Nullable<System.DateTime>(((System.DateTime)(System.Convert.ChangeType(markdateToolStripTextBox1.Text, typeof(System.DateTime))))));
        //    }
        //    catch (System.Exception ex)
        //    {
        //        System.Windows.Forms.MessageBox.Show(ex.Message);
        //    }

        //}

        private static void CountinvoicemoneyTotal( DateTime markdate)
        {




            SqlConnection sqlConnection1 = new SqlConnection(SQLDatabase.Connectstring);
            SqlCommand cmd = new SqlCommand();

            cmd.Parameters.Add("@markdate", SqlDbType.DateTime);
            cmd.Parameters["@markdate"].Value = markdate;



            //cmd.Parameters.Add("@supplier_code", SqlDbType.VarChar);
            //cmd.Parameters["@supplier_code"].Value = nowsupplierCode;


            cmd.CommandText = "LY_payable_payInvoice_total";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandTimeout = 0;
            cmd.Connection = sqlConnection1;

            sqlConnection1.Open();
            cmd.ExecuteNonQuery();
            sqlConnection1.Close();



        }

        private void toolStripButton53_Click(object sender, EventArgs e)
        {
            if (null == this.lY_payable_mainDataGridView.CurrentRow) return;
            if (SQLDatabase.NowUserID != "000")
            {
                MessageBox.Show("暂时无权限，请联系公司IT", "注意");
                return; 
            }

            string nowsupplierCode;
            string noename;

            /////////////////////

            //SqlConnection sqlConnection1 = new SqlConnection(SQLDatabase.Connectstring);
            //SqlCommand cmd = new SqlCommand();

            ////cmd.Parameters.Add("@markdate", SqlDbType.DateTime);
            ////cmd.Parameters["@markdate"].Value = markdate;



            ////cmd.Parameters.Add("@supplier_code", SqlDbType.VarChar);
            ////cmd.Parameters["@supplier_code"].Value = nowsupplierCode;


            //cmd.CommandText = " delete ly_invoice_paymente ";
            //cmd.CommandType = CommandType.Text;
            //cmd.CommandTimeout = 0;
            //cmd.Connection = sqlConnection1;

            //sqlConnection1.Open();
            //cmd.ExecuteNonQuery();
            //sqlConnection1.Close();


            ///////////////////////

            NewFrm.Show(this);


            //foreach (DataGridViewRow dgr in lY_payable_mainDataGridView.Rows)
            //{
            //    nowsupplierCode = dgr.Cells["supplier_code"].Value.ToString();
            //    noename = dgr.Cells["supplier_name"].Value.ToString();



            //    //this.toolStripLabel3.Text = plannum;
            //    //this.toolStripLabel3.Invalidate();

            //    NewFrm.Notify(this, "正在处理:  (" + nowsupplierCode + ")" + noename + "   发票付款");



            //    Countinvoicemoney(nowsupplierCode, this.dateTimePicker1.Value.AddDays(1));

            //}


            CountinvoicemoneyTotal( this.dateTimePicker1.Value.AddDays(1));

            NewFrm.Hide(this);

            this.lY_payable_detail_allTableAdapter.Fill(this.lYFinancialMange.LY_payable_detail_all, this.dateTimePicker1.Value);
        }

        private void toolStripButton59_Click(object sender, EventArgs e)
        {
            this.lY_payable_PrepayNoincoiceTableAdapter.Fill(this.lYFinancialMange.LY_payable_PrepayNoincoice, this.dateTimePicker1.Value);
            AddSummationRow_New5(lY_payable_PrepayNoincoiceBindingSource, lY_payable_PrepayNoincoiceDataGridView);

        }


        private void AddSummationRow_New3(BindingSource bs, DataGridView dgv)
        {

            DataRow sumdr = (((DataSet)bs.DataSource).Tables[bs.DataMember]).NewRow();

            if (-1 != bs.Find("供应商编码", "合计"))
            {
                bs.RemoveAt(bs.Find("供应商编码", "合计"));
            }

            foreach (DataGridViewRow dgvRow in dgv.Rows)
            {
                foreach (DataGridViewCell dgvCell in dgvRow.Cells)
                {
                    if (dgvCell.Value != null && !(dgvCell.Value is DBNull))
                    {
                        if (IsInteger(dgvCell.Value))
                        {
                            if ("年份" != dgvCell.OwningColumn.HeaderText && "月份" != dgvCell.OwningColumn.HeaderText)
                            {
                                if (sumdr[dgvCell.OwningColumn.DataPropertyName] == null || (sumdr[dgvCell.OwningColumn.DataPropertyName] is DBNull))
                                    sumdr[dgvCell.OwningColumn.DataPropertyName] = 0;


                                sumdr[dgvCell.OwningColumn.DataPropertyName] = Convert.ToInt64(sumdr[dgvCell.OwningColumn.DataPropertyName]) + Convert.ToInt64(dgvCell.Value);
                            }
                        }
                        else if (IsDecimal(dgvCell.Value))
                        {
                            if ("年份" != dgvCell.OwningColumn.HeaderText && "月份" != dgvCell.OwningColumn.HeaderText)
                            {
                                if (sumdr[dgvCell.OwningColumn.DataPropertyName] == null || (sumdr[dgvCell.OwningColumn.DataPropertyName] is DBNull))
                                    sumdr[dgvCell.OwningColumn.DataPropertyName] = 0;
                                sumdr[dgvCell.OwningColumn.DataPropertyName] = Convert.ToDecimal(sumdr[dgvCell.OwningColumn.DataPropertyName]) + Convert.ToDecimal(dgvCell.Value);
                            }
                        }


                    }
                }

            }


            sumdr["供应商编码"] = "合计";
            ((DataSet)bs.DataSource).Tables[bs.DataMember].Rows.Add(sumdr);
            bs.ResetBindings(true);

        }

        private void AddSummationRow_New5(BindingSource bs, DataGridView dgv)
        {

            DataRow sumdr = (((DataSet)bs.DataSource).Tables[bs.DataMember]).NewRow();

            if (-1 != bs.Find("供应商编码", "合计"))
            {
                bs.RemoveAt(bs.Find("供应商编码", "合计"));
            }

            foreach (DataGridViewRow dgvRow in dgv.Rows)
            {
                foreach (DataGridViewCell dgvCell in dgvRow.Cells)
                {
                    if (dgvCell.Value != null && !(dgvCell.Value is DBNull))
                    {
                        if (IsInteger(dgvCell.Value))
                        {
                            if ("年份" != dgvCell.OwningColumn.HeaderText && "月份" != dgvCell.OwningColumn.HeaderText)
                            {
                                if (sumdr[dgvCell.OwningColumn.DataPropertyName] == null || (sumdr[dgvCell.OwningColumn.DataPropertyName] is DBNull))
                                    sumdr[dgvCell.OwningColumn.DataPropertyName] = 0;


                                sumdr[dgvCell.OwningColumn.DataPropertyName] = Convert.ToInt64(sumdr[dgvCell.OwningColumn.DataPropertyName]) + Convert.ToInt64(dgvCell.Value);
                            }
                        }
                        else if (IsDecimal(dgvCell.Value))
                        {
                            if ("年份" != dgvCell.OwningColumn.HeaderText && "月份" != dgvCell.OwningColumn.HeaderText)
                            {
                                if (sumdr[dgvCell.OwningColumn.DataPropertyName] == null || (sumdr[dgvCell.OwningColumn.DataPropertyName] is DBNull))
                                    sumdr[dgvCell.OwningColumn.DataPropertyName] = 0;
                                sumdr[dgvCell.OwningColumn.DataPropertyName] = Convert.ToDecimal(sumdr[dgvCell.OwningColumn.DataPropertyName]) + Convert.ToDecimal(dgvCell.Value);
                            }
                        }


                    }
                }

            }


            sumdr["供应商编码"] = "合计";
            ((DataSet)bs.DataSource).Tables[bs.DataMember].Rows.Add(sumdr);
            bs.ResetBindings(true);

        }

        private void toolStripButton24_Click(object sender, EventArgs e)
        {

        }

        private void toolStripButton54_Click(object sender, EventArgs e)
        {
            ExportDataGridviewTOExcellA.ExportDataGridview(this.lY_payable_mainDataGridView, true);
        }

        private void toolStripButton61_Click(object sender, EventArgs e)
        {
            ExportDataGridviewTOExcellA.ExportDataGridview(this.lY_payable_detail_allDataGridView, true);
        }

        private void toolStripButton62_Click(object sender, EventArgs e)
        {
            ExportDataGridviewTOExcellA.ExportDataGridview(this.lY_payable_PrepayNoincoiceDataGridView, true);
        }

        private void toolStripButton60_Click(object sender, EventArgs e)
        {
            ExportDataGridviewTOExcellA.ExportDataGridview(this.lY_payable_periodDataGridView, true);
        }

        private void toolStripButton69_Click(object sender, EventArgs e)
        {
            this.lY_payable_detail_all_planTableAdapter.Fill(this.lYFinancialMange.LY_payable_detail_all_plan, this.dateTimePicker1.Value.AddDays(1));
            AddSummationRow_New6(lY_payable_detail_all_planBindingSource, lY_payable_detail_all_planDataGridView);
        }

        private void AddSummationRow_New6(BindingSource bs, DataGridView dgv)
        {

            DataRow sumdr = (((DataSet)bs.DataSource).Tables[bs.DataMember]).NewRow();

            if (-1 != bs.Find("供应商编码", "合计"))
            {
                bs.RemoveAt(bs.Find("供应商编码", "合计"));
            }

            foreach (DataGridViewRow dgvRow in dgv.Rows)
            {
                foreach (DataGridViewCell dgvCell in dgvRow.Cells)
                {
                    if (dgvCell.Value != null && !(dgvCell.Value is DBNull))
                    {
                        if (IsInteger(dgvCell.Value))
                        {
                            if ("年份" != dgvCell.OwningColumn.HeaderText && "月份" != dgvCell.OwningColumn.HeaderText)
                            {
                                if (sumdr[dgvCell.OwningColumn.DataPropertyName] == null || (sumdr[dgvCell.OwningColumn.DataPropertyName] is DBNull))
                                    sumdr[dgvCell.OwningColumn.DataPropertyName] = 0;


                                sumdr[dgvCell.OwningColumn.DataPropertyName] = Convert.ToInt64(sumdr[dgvCell.OwningColumn.DataPropertyName]) + Convert.ToInt64(dgvCell.Value);
                            }
                        }
                        else if (IsDecimal(dgvCell.Value))
                        {
                            if ("年份" != dgvCell.OwningColumn.HeaderText && "月份" != dgvCell.OwningColumn.HeaderText)
                            {
                                if (sumdr[dgvCell.OwningColumn.DataPropertyName] == null || (sumdr[dgvCell.OwningColumn.DataPropertyName] is DBNull))
                                    sumdr[dgvCell.OwningColumn.DataPropertyName] = 0;
                                sumdr[dgvCell.OwningColumn.DataPropertyName] = Convert.ToDecimal(sumdr[dgvCell.OwningColumn.DataPropertyName]) + Convert.ToDecimal(dgvCell.Value);
                            }
                        }


                    }
                }

            }


            sumdr["供应商编码"] = "合计";
            ((DataSet)bs.DataSource).Tables[bs.DataMember].Rows.Add(sumdr);
            bs.ResetBindings(true);

        }

        //private void AddSummationRow_New7(BindingSource bs, DataGridView dgv)
        //{

        //    DataRow sumdr = (((DataSet)bs.DataSource).Tables[bs.DataMember]).NewRow();

        //    if (-1 != bs.Find("供应商编码", "合计"))
        //    {
        //        bs.RemoveAt(bs.Find("供应商编码", "合计"));
        //    }

        //    foreach (DataGridViewRow dgvRow in dgv.Rows)
        //    {
        //        foreach (DataGridViewCell dgvCell in dgvRow.Cells)
        //        {
        //            if (dgvCell.Value != null && !(dgvCell.Value is DBNull))
        //            {
        //                if (IsInteger(dgvCell.Value))
        //                {
        //                    if ("年份" != dgvCell.OwningColumn.HeaderText && "月份" != dgvCell.OwningColumn.HeaderText)
        //                    {
        //                        if (sumdr[dgvCell.OwningColumn.DataPropertyName] == null || (sumdr[dgvCell.OwningColumn.DataPropertyName] is DBNull))
        //                            sumdr[dgvCell.OwningColumn.DataPropertyName] = 0;


        //                        sumdr[dgvCell.OwningColumn.DataPropertyName] = Convert.ToInt64(sumdr[dgvCell.OwningColumn.DataPropertyName]) + Convert.ToInt64(dgvCell.Value);
        //                    }
        //                }
        //                else if (IsDecimal(dgvCell.Value))
        //                {
        //                    if ("年份" != dgvCell.OwningColumn.HeaderText && "月份" != dgvCell.OwningColumn.HeaderText)
        //                    {
        //                        if (sumdr[dgvCell.OwningColumn.DataPropertyName] == null || (sumdr[dgvCell.OwningColumn.DataPropertyName] is DBNull))
        //                            sumdr[dgvCell.OwningColumn.DataPropertyName] = 0;
        //                        sumdr[dgvCell.OwningColumn.DataPropertyName] = Convert.ToDecimal(sumdr[dgvCell.OwningColumn.DataPropertyName]) + Convert.ToDecimal(dgvCell.Value);
        //                    }
        //                }


        //            }
        //        }

        //    }


        //    sumdr["供应商编码"] = "合计";
        //    ((DataSet)bs.DataSource).Tables[bs.DataMember].Rows.Add(sumdr);
        //    bs.ResetBindings(true);

        //}

        private void toolStripButton63_Click(object sender, EventArgs e)
        {
            if (SQLDatabase.NowUserID != "000")
            {
                MessageBox.Show("暂时无权限，请联系公司IT", "注意");
                return;
            }

            NewFrm.Show(this);

            CountinvoicemoneyPlan();

            NewFrm.Hide(this);
        }

        private static void CountinvoicemoneyPlan()
        {




            SqlConnection sqlConnection1 = new SqlConnection(SQLDatabase.Connectstring);
            SqlCommand cmd = new SqlCommand();

            //cmd.Parameters.Add("@markdate", SqlDbType.DateTime);
            //cmd.Parameters["@markdate"].Value = markdate;



            //cmd.Parameters.Add("@supplier_code", SqlDbType.VarChar);
            //cmd.Parameters["@supplier_code"].Value = nowsupplierCode;


            cmd.CommandText = "LY_payable_payInvoice_All";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandTimeout = 0;
            cmd.Connection = sqlConnection1;

            sqlConnection1.Open();
            cmd.ExecuteNonQuery();
            sqlConnection1.Close();



        }

        private void AddSummationRow_New7(BindingSource bs, DataGridView dgv)
        {

            DataRow sumdr = (((DataSet)bs.DataSource).Tables[bs.DataMember]).NewRow();

            if (-1 != bs.Find("供应商编码", "合计"))
            {
                bs.RemoveAt(bs.Find("供应商编码", "合计"));
            }

            foreach (DataGridViewRow dgvRow in dgv.Rows)
            {
                foreach (DataGridViewCell dgvCell in dgvRow.Cells)
                {
                    if (dgvCell.Value != null && !(dgvCell.Value is DBNull))
                    {
                        if (IsInteger(dgvCell.Value))
                        {
                            if ("年份" != dgvCell.OwningColumn.HeaderText && "月份" != dgvCell.OwningColumn.HeaderText)
                            {
                                if (sumdr[dgvCell.OwningColumn.DataPropertyName] == null || (sumdr[dgvCell.OwningColumn.DataPropertyName] is DBNull))
                                    sumdr[dgvCell.OwningColumn.DataPropertyName] = 0;


                                sumdr[dgvCell.OwningColumn.DataPropertyName] = Convert.ToInt64(sumdr[dgvCell.OwningColumn.DataPropertyName]) + Convert.ToInt64(dgvCell.Value);
                            }
                        }
                        else if (IsDecimal(dgvCell.Value))
                        {
                            if ("年份" != dgvCell.OwningColumn.HeaderText && "月份" != dgvCell.OwningColumn.HeaderText)
                            {
                                if (sumdr[dgvCell.OwningColumn.DataPropertyName] == null || (sumdr[dgvCell.OwningColumn.DataPropertyName] is DBNull))
                                    sumdr[dgvCell.OwningColumn.DataPropertyName] = 0;
                                sumdr[dgvCell.OwningColumn.DataPropertyName] = Convert.ToDecimal(sumdr[dgvCell.OwningColumn.DataPropertyName]) + Convert.ToDecimal(dgvCell.Value);
                            }
                        }


                    }
                }

            }


            sumdr["供应商编码"] = "合计";
            ((DataSet)bs.DataSource).Tables[bs.DataMember].Rows.Add(sumdr);
            bs.ResetBindings(true);

        }

        private void toolStripButton75_Click(object sender, EventArgs e)
        {
            this.lY_payable_PrepayNoincoicePlanTableAdapter.Fill(this.lYFinancialMange.LY_payable_PrepayNoincoicePlan, this.dateTimePicker1.Value.AddDays(1));
            AddSummationRow_New7(lY_payable_PrepayNoincoicePlanBindingSource, lY_payable_PrepayNoincoicePlanDataGridView);
        }


        private void AddSummationRow_New8(BindingSource bs, DataGridView dgv)
        {

            DataRow sumdr = (((DataSet)bs.DataSource).Tables[bs.DataMember]).NewRow();

            if (-1 != bs.Find("供应商编码", "合计"))
            {
                bs.RemoveAt(bs.Find("供应商编码", "合计"));
            }

            foreach (DataGridViewRow dgvRow in dgv.Rows)
            {
                foreach (DataGridViewCell dgvCell in dgvRow.Cells)
                {
                    if (dgvCell.Value != null && !(dgvCell.Value is DBNull))
                    {
                        if (IsInteger(dgvCell.Value))
                        {
                            if ("年份" != dgvCell.OwningColumn.HeaderText && "月份" != dgvCell.OwningColumn.HeaderText)
                            {
                                if (sumdr[dgvCell.OwningColumn.DataPropertyName] == null || (sumdr[dgvCell.OwningColumn.DataPropertyName] is DBNull))
                                    sumdr[dgvCell.OwningColumn.DataPropertyName] = 0;


                                sumdr[dgvCell.OwningColumn.DataPropertyName] = Convert.ToInt64(sumdr[dgvCell.OwningColumn.DataPropertyName]) + Convert.ToInt64(dgvCell.Value);
                            }
                        }
                        else if (IsDecimal(dgvCell.Value))
                        {
                            if ("年份" != dgvCell.OwningColumn.HeaderText && "月份" != dgvCell.OwningColumn.HeaderText)
                            {
                                if (sumdr[dgvCell.OwningColumn.DataPropertyName] == null || (sumdr[dgvCell.OwningColumn.DataPropertyName] is DBNull))
                                    sumdr[dgvCell.OwningColumn.DataPropertyName] = 0;
                                sumdr[dgvCell.OwningColumn.DataPropertyName] = Convert.ToDecimal(sumdr[dgvCell.OwningColumn.DataPropertyName]) + Convert.ToDecimal(dgvCell.Value);
                            }
                        }


                    }
                }

            }


            sumdr["供应商编码"] = "合计";
            ((DataSet)bs.DataSource).Tables[bs.DataMember].Rows.Add(sumdr);
            bs.ResetBindings(true);

        }

        private void toolStripButton81_Click(object sender, EventArgs e)
        {

            this.lY_payable_PrepayHaveinvoiceNoPlanTableAdapter.Fill(this.lYFinancialMange.LY_payable_PrepayHaveinvoiceNoPlan, this.dateTimePicker1.Value.AddDays(1));

            AddSummationRow_New8(lY_payable_PrepayHaveinvoiceNoPlanBindingSource, lY_payable_PrepayHaveinvoiceNoPlanDataGridView);

        }

        private void toolStripButton68_Click(object sender, EventArgs e)
        {
            ExportDataGridviewTOExcellA.ExportDataGridview(this.lY_payable_detail_all_planDataGridView, true);
        }

        private void toolStripButton74_Click(object sender, EventArgs e)
        {
            
                ExportDataGridviewTOExcellA.ExportDataGridview(this.lY_payable_PrepayNoincoicePlanDataGridView, true);
        }

        private void toolStripButton80_Click(object sender, EventArgs e)
        {
            
                ExportDataGridviewTOExcellA.ExportDataGridview(this.lY_payable_PrepayHaveinvoiceNoPlanDataGridView, true);
        }

        private void 计划编码TextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void lY_payable_mainDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        //private void fillToolStripButton_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        this.lY_payable_detail_standard_SumTableAdapter.Fill(this.lYFinancialMange.LY_payable_detail_standard_Sum, new System.Nullable<System.DateTime>(((System.DateTime)(System.Convert.ChangeType(markdateToolStripTextBox.Text, typeof(System.DateTime))))));
        //    }
        //    catch (System.Exception ex)
        //    {
        //        System.Windows.Forms.MessageBox.Show(ex.Message);
        //    }

        //}

        private void toolStripButton87_Click(object sender, EventArgs e)
        {
            this.lY_payable_detail_standard_SumTableAdapter.Fill(this.lYFinancialMange.LY_payable_detail_standard_Sum, this.dateTimePicker1.Value);
            AddSummationRow_AccountAge(lY_payable_detail_standard_SumBindingSource, lY_payable_detail_standard_SumDataGridView);
        }

        private void AddSummationRow_AccountAge(BindingSource bs, DataGridView dgv)
        {

            DataRow sumdr = (((DataSet)bs.DataSource).Tables[bs.DataMember]).NewRow();

            if (-1 != bs.Find("供应商编号", "合计"))
            {
                bs.RemoveAt(bs.Find("供应商编号", "合计"));
            }

            foreach (DataGridViewRow dgvRow in dgv.Rows)
            {
                foreach (DataGridViewCell dgvCell in dgvRow.Cells)
                {
                    if (dgvCell.Value != null && !(dgvCell.Value is DBNull))
                    {
                        if (IsInteger(dgvCell.Value))
                        {
                            if ("年份" != dgvCell.OwningColumn.HeaderText && "月份" != dgvCell.OwningColumn.HeaderText)
                            {
                                if (sumdr[dgvCell.OwningColumn.DataPropertyName] == null || (sumdr[dgvCell.OwningColumn.DataPropertyName] is DBNull))
                                    sumdr[dgvCell.OwningColumn.DataPropertyName] = 0;


                                sumdr[dgvCell.OwningColumn.DataPropertyName] = Convert.ToInt64(sumdr[dgvCell.OwningColumn.DataPropertyName]) + Convert.ToInt64(dgvCell.Value);
                            }
                        }
                        else if (IsDecimal(dgvCell.Value))
                        {
                            if ("年份" != dgvCell.OwningColumn.HeaderText && "月份" != dgvCell.OwningColumn.HeaderText)
                            {
                                if (sumdr[dgvCell.OwningColumn.DataPropertyName] == null || (sumdr[dgvCell.OwningColumn.DataPropertyName] is DBNull))
                                    sumdr[dgvCell.OwningColumn.DataPropertyName] = 0;
                                sumdr[dgvCell.OwningColumn.DataPropertyName] = Convert.ToDecimal(sumdr[dgvCell.OwningColumn.DataPropertyName]) + Convert.ToDecimal(dgvCell.Value);
                            }
                        }


                    }
                }

            }

            //////////
            sumdr["供应商编号"] = "合计";
            ((DataSet)bs.DataSource).Tables[bs.DataMember].Rows.Add(sumdr);
            bs.ResetBindings(true);

        }

        //private void fillToolStripButton_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        this.lY_payable_PrepayHaveinvoiceNoPlanTableAdapter.Fill(this.lYFinancialMange.LY_payable_PrepayHaveinvoiceNoPlan, new System.Nullable<System.DateTime>(((System.DateTime)(System.Convert.ChangeType(markdateToolStripTextBox.Text, typeof(System.DateTime))))));
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
        //        this.lY_payable_PrepayNoincoicePlanTableAdapter.Fill(this.lYFinancialMange.LY_payable_PrepayNoincoicePlan, new System.Nullable<System.DateTime>(((System.DateTime)(System.Convert.ChangeType(markdateToolStripTextBox.Text, typeof(System.DateTime))))));
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
        //        this.lY_payable_detail_all_planTableAdapter.Fill(this.lYFinancialMange.LY_payable_detail_all_plan, new System.Nullable<System.DateTime>(((System.DateTime)(System.Convert.ChangeType(markdateToolStripTextBox.Text, typeof(System.DateTime))))));
        //    }
        //    catch (System.Exception ex)
        //    {
        //        System.Windows.Forms.MessageBox.Show(ex.Message);
        //    }

        //}



        //    private void fillToolStripButton_Click(object sender, EventArgs e)
        //    {
        //        try
        //        {
        //            this.lY_payable_periodTableAdapter.Fill(this.lYFinancialMange.LY_payable_period, new System.Nullable<System.DateTime>(((System.DateTime)(System.Convert.ChangeType(begindateToolStripTextBox.Text, typeof(System.DateTime))))), new System.Nullable<System.DateTime>(((System.DateTime)(System.Convert.ChangeType(enddateToolStripTextBox.Text, typeof(System.DateTime))))));
        //        }
        //        catch (System.Exception ex)
        //        {
        //            System.Windows.Forms.MessageBox.Show(ex.Message);
        //        }

        //    }

        //    private void fillToolStripButton_Click_1(object sender, EventArgs e)
        //    {
        //        try
        //        {
        //            this.lY_payable_period_detailTableAdapter.Fill(this.lYFinancialMange.LY_payable_period_detail, supplier_codeToolStripTextBox.Text, new System.Nullable<System.DateTime>(((System.DateTime)(System.Convert.ChangeType(begindateToolStripTextBox.Text, typeof(System.DateTime))))), new System.Nullable<System.DateTime>(((System.DateTime)(System.Convert.ChangeType(enddateToolStripTextBox.Text, typeof(System.DateTime))))));
        //        }
        //        catch (System.Exception ex)
        //        {
        //            System.Windows.Forms.MessageBox.Show(ex.Message);
        //        }

        //    }
    }
}

