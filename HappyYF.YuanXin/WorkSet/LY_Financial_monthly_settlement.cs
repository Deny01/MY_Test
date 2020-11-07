using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Text.RegularExpressions;
using System.Threading;
using System.Data.SqlClient;

using System.Transactions;

using HappyYF.Infrastructure.Repositories;
using DataGridFilter;

namespace HappyYF.YuanXin.WorkSet
{
    public partial class LY_Financial_monthly_settlement : Form
    {

        private DataSet ds3;
        BindingSource bindingSource3;

        string formState = "View";
        public LY_Financial_monthly_settlement()
        {
            InitializeComponent();

            this.ly_get_wip_finTableAdapter.CommandTimeout = 0;
            this.lY_Inventory_query_settlementTableAdapter.CommandTimeout = 0;
            this.ly_sales_cost_Detail_BOMTableAdapter.CommandTimeout = 0;
            this.ly_get_wip_detail_finTableAdapter.CommandTimeout = 0;
            this.ly_get_wip_storeinTableAdapter.CommandTimeout = 0;
            this.ly_get_wip_storeoutTableAdapter.CommandTimeout = 0;

            this.ly_store_inout_wip_periodTableAdapter.CommandTimeout = 0;
            this.ly_get_wip_storeout_periodTableAdapter.CommandTimeout = 0;
            this.ly_get_wip_storein_periodTableAdapter.CommandTimeout = 0;
            this.ly_get_wip_material_periodTableAdapter.CommandTimeout = 0;
            this.get_SalesPrice_ByApprove_Fin_month_detailTableAdapter.CommandTimeout = 0;

            this.f_Item_dynamicpriceBTableAdapter.CommandTimeout = 0;
            this.ly_Settlement_In_FinishGood_SumTableAdapter.CommandTimeout = 0;

            

        }

        private void ly_financial_monthly_settlement_mainBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            //this.ly_financial_monthly_settlement_mainBindingSource.EndEdit();
            //this.tableAdapterManager.UpdateAll(this.lYFinancialMange);

            //this.Validate();


            if (人工费用TextBox.Text == "" || 业务费用TextBox.Text == "")
            {

                MessageBox.Show("分摊费用不能为空！"); return;
            }

            if (!(Regex.IsMatch(this.人工费用TextBox.Text, @"^([-]{1}[0-9]+(.[0-9]{3})?$)|([0-9]+(.[0-9]{3})?$)"))) // ^\+?[1-9][0-9]*$
            {
                MessageBox.Show("人工费用数字格式错误，请重新输入");
                this.人工费用TextBox.Focus();
                return;

            }

            if (!(Regex.IsMatch(this.业务费用TextBox.Text, @"^([-]{1}[0-9]+(.[0-9]{3})?$)|([0-9]+(.[0-9]{3})?$)"))) // ^\+?[1-9][0-9]*$
            {
                MessageBox.Show("业务费用数字格式错误，请重新输入");
                this.业务费用TextBox.Focus();
                return;

            }

            this.ly_financial_monthly_settlement_mainBindingSource.EndEdit();
            this.ly_financial_monthly_settlement_mainTableAdapter.Update(this.lYFinancialMange.ly_financial_monthly_settlement_main);

            SetFormState("View");

        }

        private void LY_Financial_monthly_settlement_Load(object sender, EventArgs e)
        {
            this.dateTimePicker5.Text = "2020" + "-01" + "-01";

            this.dateTimePicker6.Text = SQLDatabase.GetNowdate().AddDays(0).Date.ToString();


            this.dateTimePicker10.Text = SQLDatabase.GetNowdate().Date.Year.ToString() + "-01" + "-01";
            this.dateTimePicker11.Text = SQLDatabase.GetNowdate().AddDays(0).Date.ToString();

            this.dateTimePicker15.Text = DateTime.Today.AddMonths(-1).Date.ToString();
            this.dateTimePicker16.Text = DateTime.Today.AddDays(0).Date.ToString();

            //this.dateTimePicker19.Text = SQLDatabase.GetNowdate().Date.Year.ToString() + "-01" + "-01";
            this.dateTimePicker19.Text = "2018-01-01";
            this.dateTimePicker20.Text = SQLDatabase.GetNowdate().AddDays(0).Date.ToString();

            this.dateTimePicker68.Text = SQLDatabase.GetNowdate().Date.Year.ToString() + "-01" + "-01";
            this.dateTimePicker69.Text = SQLDatabase.GetNowdate().AddDays(0).Date.ToString();

            this.dateTimePicker21.Text = "2018-01-01";
            this.dateTimePicker22.Text = SQLDatabase.GetNowdate().AddDays(0).Date.ToString();

            this.ly_store_in_aettlementTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            this.ly_store_out_aettlementTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;

           

            this.ly_store_inout_wip_periodTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            this.ly_get_wip_storeout_periodTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            this.ly_get_wip_storein_periodTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;

            this.ly_get_wip_material_periodTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;

            this.f_Item_dynamicpriceBTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;

            this.ly_get_wip_storeinTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            this.ly_get_wip_storeoutTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;


            this.ly_get_wip_detail_finTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;

            this.ly_financial_monthly_settlement_mainTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            this.ly_Settlement_InOutListTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            this.ly_Real_InOutListTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;

            this.ly_sales_cost_Detail_BOMTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            

            this.ly_get_wip_finTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
         

            this.get_SalesPrice_ByApprove_Fin_month_detailTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
           
           this.f_Item_dynamicpriceB_pacTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;

            this.lY_Inventory_query_settlementTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;

            ly_dni_monthTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;

            ly_dni_outTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            ly_store_out_settlementViewTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            ly_store_in_View_FinTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            lY_payable_detail_standard_SumTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            //this.ly_Settlement_InOutListTableAdapter.Fill(this.lYFinancialMange.ly_Settlement_InOutList, monthly_settlement_numToolStripTextBox.Text);  
            this.ly_financial_monthly_settlement_mainTableAdapter.Fill(this.lYFinancialMange.ly_financial_monthly_settlement_main);

            this.dateTimePicker8.MinDate = DateTime.Parse("2019-04-30");
            this.dateTimePicker8.Text = new DateTime(DateTime.Now.Year, 1, 1).ToString("yyyy-MM-dd");
            this.dateTimePicker9.Text = DateTime.Today.AddDays(1).Date.ToString();





            this.lY_Invoice_storeInAllTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;



            SetFormState("View");
        }

        private void SetFormState(string state)
        {
            // view edit add save cancle

            //return;

            if ("View" == state)
            {
                this.formState = "View";

                //this.计划日期DateTimePicker.Enabled = false;

                this.月结说明TextBox.ReadOnly = true;
                this.人工费用TextBox.ReadOnly = true;
                this.业务费用TextBox.ReadOnly = true;
                //this.部门批准CheckBox.Enabled = false;
                //this.公司批准CheckBox.Enabled = false;


                this.bindingNavigatorMoveFirstItem.Enabled = true;
                this.bindingNavigatorMovePreviousItem.Enabled = true;
                this.bindingNavigatorMoveNextItem.Enabled = true;
                this.bindingNavigatorMoveLastItem.Enabled = true;


                this.bindingNavigatorPositionItem.Enabled = true;




                toolStripButton7.Enabled = true;
                bindingNavigatorDeleteItem.Enabled = true;
                bindingNavigatorAddNewItem.Enabled = true;
                ly_financial_monthly_settlement_mainBindingNavigatorSaveItem.Enabled = false;

                //yX_clientBindingNavigatorSaveItem.Enabled = true ;


                ly_financial_monthly_settlement_mainDataGridView.Enabled = true;
                //this.button1.Enabled = true;


            }
            else
            {
                this.formState = "Edit";

                //this.计划日期DateTimePicker.Enabled = true;

                this.月结说明TextBox.ReadOnly = false;
                this.人工费用TextBox.ReadOnly = false;
                this.业务费用TextBox.ReadOnly = false;
                //this.部门批准CheckBox.Enabled = false;
                //this.公司批准CheckBox.Enabled = false;
                //启用备料CheckBox.Enabled = true;
                //备料计算CheckBox.Enabled = true;

                //年份TextBox.ReadOnly = false;
                //月份ComboBox.Enabled = true;


                this.bindingNavigatorMoveFirstItem.Enabled = false;
                this.bindingNavigatorMovePreviousItem.Enabled = false;
                this.bindingNavigatorMoveNextItem.Enabled = false;
                this.bindingNavigatorMoveLastItem.Enabled = false;
                this.bindingNavigatorPositionItem.Enabled = false;




                toolStripButton7.Enabled = false;
                bindingNavigatorDeleteItem.Enabled = false;
                bindingNavigatorAddNewItem.Enabled = false;
                ly_financial_monthly_settlement_mainBindingNavigatorSaveItem.Enabled = true;




                ly_financial_monthly_settlement_mainDataGridView.Enabled = false;



            }


        }

        private Boolean Check_ifApproved(string nowDepartment)
        {
            if (this.ly_financial_monthly_settlement_mainDataGridView.CurrentRow == null) return true;
            string planNum = this.ly_financial_monthly_settlement_mainDataGridView.CurrentRow.Cells["月结编码"].Value.ToString();

            SqlConnection sqlConnection1 = new SqlConnection(SQLDatabase.Connectstring);
            SqlCommand cmd = new SqlCommand();

            Boolean Approve_flag = true;

            cmd.Parameters.Add("@NowSettlement", SqlDbType.VarChar);
            cmd.Parameters["@NowSettlement"].Value = planNum;

            cmd.Parameters.Add("@Nowdepartment", SqlDbType.VarChar);
            cmd.Parameters["@Nowdepartment"].Value = nowDepartment;


            cmd.CommandText = "LY_GetMonthly_settlement_Approve";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = sqlConnection1;

            sqlConnection1.Open();
            Approve_flag = Boolean.Parse(cmd.ExecuteScalar().ToString());
            sqlConnection1.Close();



            return Approve_flag;

        }

        private string GetMaxSettlementCode()
        {
            SqlConnection sqlConnection1 = new SqlConnection(SQLDatabase.Connectstring);
            SqlCommand cmd = new SqlCommand();

            string MaxPlanCode = "";

            cmd.Parameters.Add("@Plan_mode", SqlDbType.VarChar);
            cmd.Parameters["@Plan_mode"].Value = "FMSC";


            cmd.CommandText = "LY_GetMaxSettlementCode";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = sqlConnection1;

            sqlConnection1.Open();
            MaxPlanCode = cmd.ExecuteScalar().ToString();
            sqlConnection1.Close();



            return MaxPlanCode;
        }

        private void bindingNavigatorAddNewItem_Click(object sender, EventArgs e)
        {
            if (!SQLDatabase.CheckHaveRight(SQLDatabase.NowUserID, "月结增加"))
            {
                MessageBox.Show("无月结增加权限...", "注意");

                return;

            }

            if (!Check_ifApproved("canadd"))
            {

                MessageBox.Show("存在未经付款确认的月结记录,不能增加新月结...", "注意");
                return;
            }

            string message = "增加月结吗？";
            string caption = "提示...";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;

            DialogResult result;



            result = MessageBox.Show(message, caption, buttons,
            MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

            if (result == DialogResult.Yes)
            {


                this.ly_financial_monthly_settlement_mainBindingSource.AddNew();
                this.ly_financial_monthly_settlement_mainDataGridView.CurrentRow.Cells["月结编码"].Value = GetMaxSettlementCode();
                //this.ly_financial_monthly_settlement_mainDataGridView.CurrentRow.Cells["计划日期"].Value = SQLDatabase.GetNowdate();
                this.ly_financial_monthly_settlement_mainDataGridView.CurrentRow.Cells["结算人"].Value = SQLDatabase.nowUserName();
                this.ly_financial_monthly_settlement_mainDataGridView.EndEdit();

                this.Validate();
                this.ly_financial_monthly_settlement_mainBindingSource.EndEdit();



                this.ly_financial_monthly_settlement_mainTableAdapter.Update(this.lYFinancialMange.ly_financial_monthly_settlement_main);



                SetFormState("Edit");

                //this.计划日期DateTimePicker.Focus();

                //DataRowView nowCard = (DataRowView)this.yX_clientBindingSource.Current;

                //   nowCard["Card_number"].; nowCard.

            }
        }

        private void bindingNavigatorDeleteItem_Click(object sender, EventArgs e)
        {
            if (null == this.ly_financial_monthly_settlement_mainDataGridView.CurrentRow) return;

            //if (!SQLDatabase.CheckHaveRight(SQLDatabase.NowUserID, "月结确认"))
            //{
            //    MessageBox.Show("无月结确认权限...", "注意");
            //    return;

            //}

            string planNum = this.ly_financial_monthly_settlement_mainDataGridView.CurrentRow.Cells["月结编码"].Value.ToString();

            if (Check_ifApproved("department"))
            {

                MessageBox.Show("月结已经批准,不能删除...", "注意");
                return;
            }

            if (this.ly_financial_monthly_settlement_mainDataGridView.CurrentRow.Cells["结算人"].Value.ToString() != SQLDatabase.nowUserName())
            {

                MessageBox.Show("请" + this.ly_financial_monthly_settlement_mainDataGridView.CurrentRow.Cells["结算人"].Value.ToString() + "删除...", "注意");

                return;
            }

            //if (this.ly_material_plan_detailDataGridView.RowCount > 0)
            //{
            //    MessageBox.Show("计划已有物料记录，不能删除(实需删除，请先删除该计划的物料记录)", "注意");
            //    return;

            //}



            string message1 = "当前(月结：" + planNum + ")将被删除，继续吗？";
            string caption1 = "提示...";
            MessageBoxButtons buttons1 = MessageBoxButtons.YesNo;

            DialogResult result1;



            result1 = MessageBox.Show(message1, caption1, buttons1,
            MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

            if (result1 == DialogResult.Yes)
            {

                string delstr = " delete ly_financial_monthly_settlement_main  where monthly_settlement_num = '" + planNum + "'";


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



                    this.ly_financial_monthly_settlement_mainTableAdapter.Fill(this.lYFinancialMange.ly_financial_monthly_settlement_main);
                }


            }
        }

        private void toolStripButton7_Click(object sender, EventArgs e)
        {
            if (null == this.ly_financial_monthly_settlement_mainDataGridView.CurrentRow) return;

            string planNum = this.ly_financial_monthly_settlement_mainDataGridView.CurrentRow.Cells["月结编码"].Value.ToString();

            if (Check_ifApproved("department"))
            {

                MessageBox.Show("月结已经批准,不能修改数据...", "注意");
                return;
            }

            if (this.ly_financial_monthly_settlement_mainDataGridView.CurrentRow.Cells["结算人"].Value.ToString() != SQLDatabase.nowUserName())
            {

                MessageBox.Show("请" + this.ly_financial_monthly_settlement_mainDataGridView.CurrentRow.Cells["结算人"].Value.ToString() + "修改...", "注意");

                return;
            }

            SetFormState("Edit");
        }

        private void ly_financial_monthly_settlement_mainDataGridView_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            DataGridView dgv = sender as DataGridView;

            if (null == dgv.CurrentRow) return;



            if ("月结确认" == dgv.CurrentCell.OwningColumn.Name)
            {

                if (!SQLDatabase.CheckHaveRight(SQLDatabase.NowUserID, "财务月结确认"))
                {
                    MessageBox.Show("无月结确认权限...", "注意");
                    return;

                }

                //if (!Check_ifApproved("company"))
                //{

                //    MessageBox.Show("计划未经公司批准,不能付款确认...", "注意");
                //    return;
                //}

                if ("True" == dgv.CurrentRow.Cells["月结确认"].Value.ToString())
                {


                    //if (SQLDatabase.CheckHaveRight(SQLDatabase.NowUserID, "采购预付付款取消"))
                    //{
                    dgv.CurrentRow.Cells["月结确认"].Value = "False";

                    dgv.CurrentRow.Cells["确认人"].Value = DBNull.Value;
                    dgv.CurrentRow.Cells["确认日期"].Value = DBNull.Value;
                    //}
                    //else
                    //{

                    //    MessageBox.Show("无采购付款确认取消权限...", "注意");

                    //    return;
                    //}

                }
                else
                {

                    dgv.CurrentRow.Cells["月结确认"].Value = "True";

                    dgv.CurrentRow.Cells["确认人"].Value = SQLDatabase.nowUserName();
                    dgv.CurrentRow.Cells["确认日期"].Value = SQLDatabase.GetNowdate();

                }


                this.ly_financial_monthly_settlement_mainDataGridView.EndEdit();
                this.ly_financial_monthly_settlement_mainBindingSource.EndEdit();

                this.ly_financial_monthly_settlement_mainTableAdapter.Update(this.lYFinancialMange.ly_financial_monthly_settlement_main);



                return;

            }
        }

        private void 月结数据计算ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (null == this.ly_financial_monthly_settlement_mainDataGridView.CurrentRow) return;

            string nowsettlementnum = this.ly_financial_monthly_settlement_mainDataGridView.CurrentRow.Cells["月结编码"].Value.ToString();

            if (Check_ifApproved("department"))
            {

                MessageBox.Show("月结数据已经确认,不能计算数据...", "注意");
                return;
            }

            if (this.formState != "View") return;




            //////////////////////////////////

            string message = "计算月结数据吗？";
            string caption = "提示...";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;

            DialogResult result;



            result = MessageBox.Show(message, caption, buttons,
            MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

            if (result == DialogResult.Yes)
            {

                CalculateSettlement(nowsettlementnum);


            }
        }
        ////////////
        private void CalculateSettlement(string nowsettlementnum)
        {
            //if (null == this.ly_material_plan_mainDataGridView.CurrentRow) return;

            //int parentId = int.Parse(this.ly_material_plan_mainDataGridView.CurrentRow.Cells["id"].Value.ToString());
            //string planNum = this.ly_material_plan_mainDataGridView.CurrentRow.Cells["计划编号"].Value.ToString();

            NewFrm.Show(this);
            Thread.Sleep(500);

            //2018 -04-03

            SqlConnection sqlConnection1 = new SqlConnection(SQLDatabase.Connectstring);
            SqlCommand cmd = new SqlCommand();



            cmd.Parameters.Add("@settlement", SqlDbType.VarChar);
            cmd.Parameters["@settlement"].Value = nowsettlementnum;


            cmd.CommandText = "LY_Monthly_settlement";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = sqlConnection1;
            cmd.CommandTimeout = 0;

            sqlConnection1.Open();
            cmd.ExecuteNonQuery();
            sqlConnection1.Close();


            this.ly_financial_monthly_settlement_mainTableAdapter.Fill(this.lYFinancialMange.ly_financial_monthly_settlement_main);
            //ly_material_plan_explodeTableAdapter.Fill(this.lYPlanMange.ly_material_plan_explode, parentId);
            //this.ly_store_planitemcountTableAdapter.Fill(this.lYPlanMange.ly_store_planitemcount, parentId);
            //this.ly_plan_getmaterial_departmentTableAdapter.Fill(this.lYPlanMange.ly_plan_getmaterial_department , planNum);

            NewFrm.Hide(this);
        }

        private void ly_financial_monthly_settlement_mainDataGridView_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (null == this.ly_financial_monthly_settlement_mainDataGridView.CurrentRow) return;



            string settlementNum = this.ly_financial_monthly_settlement_mainDataGridView.Rows[e.RowIndex].Cells["月结编码"].Value.ToString();

            if (string.IsNullOrEmpty(settlementNum))
            {

                return;
            }

            NewFrm.Show(this.ParentForm);
            Thread.Sleep(100);

            this.ly_Settlement_InOutListTableAdapter.Fill(this.lYFinancialMange.ly_Settlement_InOutList, settlementNum);

            AddSummationRow_New(ly_Settlement_InOutListBindingSource, ly_Settlement_InOutListDataGridView);

            this.ly_Real_InOutListTableAdapter.Fill(this.lYFinancialMange.ly_Real_InOutList, DateTime.Parse(this.ly_financial_monthly_settlement_mainDataGridView.Rows[e.RowIndex].Cells["开始日期"].Value.ToString()), DateTime.Parse(this.ly_financial_monthly_settlement_mainDataGridView.Rows[e.RowIndex].Cells["结束日期"].Value.ToString()).AddDays(1));
            AddSummationRow_New(ly_Real_InOutListBindingSource, ly_Real_InOutListDataGridView);



            //this.dateTimePicker1.Text = DateTime.Parse(this.ly_financial_monthly_settlement_mainDataGridView.Rows[e.RowIndex].Cells["开始日期"].Value.ToString()).ToString();
            //this.dateTimePicker2.Text = DateTime.Parse(this.ly_financial_monthly_settlement_mainDataGridView.Rows[e.RowIndex].Cells["结束日期"].Value.ToString()).ToString();
            //this.dateTimePicker3.Text = DateTime.Parse(this.ly_financial_monthly_settlement_mainDataGridView.Rows[e.RowIndex].Cells["开始日期"].Value.ToString()).ToString();
            //this.dateTimePicker4.Text = DateTime.Parse(this.ly_financial_monthly_settlement_mainDataGridView.Rows[e.RowIndex].Cells["结束日期"].Value.ToString()).ToString();

            //this.dateTimePicker7.Text = DateTime.Parse(this.ly_financial_monthly_settlement_mainDataGridView.Rows[e.RowIndex].Cells["结束日期"].Value.ToString()).ToString();

            NewFrm.Hide(this.ParentForm);
        }


        private void AddSummationRow_New(BindingSource bs, DataGridView dgv)
        {

            DataRow sumdr = (((DataSet)bs.DataSource).Tables[bs.DataMember]).NewRow();

            if (-1 != bs.Find("物料编号", "_Amount"))
            {
                bs.RemoveAt(bs.Find("物料编号", "_Amount"));
            }

            foreach (DataGridViewRow dgvRow in dgv.Rows)
            {
                foreach (DataGridViewCell dgvCell in dgvRow.Cells)
                {
                    if (dgvCell.Value != null && !(dgvCell.Value is DBNull))
                    {
                        //if (IsInteger(dgvCell.Value))
                        //{
                        //    if ("年份" != dgvCell.OwningColumn.HeaderText && "月份" != dgvCell.OwningColumn.HeaderText)
                        //    {
                        //        if (sumdr[dgvCell.OwningColumn.DataPropertyName] == null || (sumdr[dgvCell.OwningColumn.DataPropertyName] is DBNull))
                        //            sumdr[dgvCell.OwningColumn.DataPropertyName] = 0;


                        //        sumdr[dgvCell.OwningColumn.DataPropertyName] = Convert.ToInt64(sumdr[dgvCell.OwningColumn.DataPropertyName]) + Convert.ToInt64(dgvCell.Value);
                        //    }
                        //}
                        //else if (IsDecimal(dgvCell.Value))
                        //{
                        if ("财务人工金额入库" == dgvCell.OwningColumn.HeaderText || "财务业务金额入库" == dgvCell.OwningColumn.HeaderText

                             || "财务人工金额出库" == dgvCell.OwningColumn.HeaderText || "财务业务金额出库" == dgvCell.OwningColumn.HeaderText
                             || "金额" == dgvCell.OwningColumn.HeaderText)
                        {
                            if (sumdr[dgvCell.OwningColumn.DataPropertyName] == null || (sumdr[dgvCell.OwningColumn.DataPropertyName] is DBNull))
                                sumdr[dgvCell.OwningColumn.DataPropertyName] = 0;
                            sumdr[dgvCell.OwningColumn.DataPropertyName] = Convert.ToDecimal(sumdr[dgvCell.OwningColumn.DataPropertyName]) + Convert.ToDecimal(dgvCell.Value);
                        }
                        //}


                    }
                }

            }

            // here
            sumdr["物料编号"] = "_Amount";
            ((DataSet)bs.DataSource).Tables[bs.DataMember].Rows.Add(sumdr);
            bs.ResetBindings(true);

        }

        private void toolStripTextBox2_KeyUp(object sender, KeyEventArgs e)
        {
            string filterString;


            filterString = GetDataGridviewMultiFilterString.DGVMultiFilterString(this.ly_Settlement_InOutListDataGridView, this.toolStripTextBox2.Text);


            this.ly_Settlement_InOutListBindingSource.Filter = "(" + filterString + ") or 物料编号='_Amount'";
            this.ly_Real_InOutListBindingSource.Filter = "(" + filterString + ") or 物料编号='_Amount'";

            AddSummationRow_New(ly_Settlement_InOutListBindingSource, ly_Settlement_InOutListDataGridView);
            AddSummationRow_New(ly_Real_InOutListBindingSource, ly_Real_InOutListDataGridView);
        }

        private void toolStripTextBox2_Enter(object sender, EventArgs e)
        {
            toolStripTextBox2.Text = "";

            this.ly_Settlement_InOutListBindingSource.Filter = "";
            this.ly_Real_InOutListBindingSource.Filter = "";

            AddSummationRow_New(ly_Settlement_InOutListBindingSource, ly_Settlement_InOutListDataGridView);
            AddSummationRow_New(ly_Real_InOutListBindingSource, ly_Real_InOutListDataGridView);

        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            ExportDataGridviewTOExcell.ExportDataGridview(this.ly_Settlement_InOutListDataGridView, true);
        }

        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            if (null == this.ly_Settlement_InOutListDataGridView.CurrentRow) return;

            FilterForm filterForm = new FilterForm();

            //SumQueryDataSet qds;
            //qds = new SumQueryDataSet();

            List<string> ls = new List<string>();
            ls.Add("id");


            filterForm.SetSourceColumns(this.lYFinancialMange.ly_Settlement_InOutList.Columns, ls);

            filterForm.ShowDialog();

            string filterString = filterForm.GetFilterString();
            if (!string.IsNullOrEmpty(filterString))
            {

                this.ly_Settlement_InOutListBindingSource.Filter = "(" + filterString + ") or 物料编号='_Amount'";
                this.ly_Real_InOutListBindingSource.Filter = "(" + filterString + ") or 物料编号='_Amount'";
            }
            AddSummationRow_New(ly_Settlement_InOutListBindingSource, ly_Settlement_InOutListDataGridView);
            // this.ly_Real_InOutListTableAdapter.Fill(this.lYFinancialMange.ly_Real_InOutList, this.开始日期DateTimePicker.Value, this.结束日期DateTimePicker.Value.AddDays(1));
            AddSummationRow_New(ly_Real_InOutListBindingSource, ly_Real_InOutListDataGridView);
        }

        private void toolStripButton8_Click(object sender, EventArgs e)
        {
            if (null == this.ly_Settlement_InOutListDataGridView.CurrentRow) return;
            SortForm DataSort = new SortForm();

            List<string> ls = new List<string>();
            ls.Add("id");


            DataSort.SetSortColumns(this.lYFinancialMange.ly_Settlement_InOutList.Columns, ls);
            DataSort.ShowDialog();

            //this.ly_Settlement_InOutListBindingSource.Sort = " sumorder asc," + DataSort.GetSortString();

            this.ly_Settlement_InOutListBindingSource.Sort = DataSort.GetSortString();
            this.ly_Real_InOutListBindingSource.Sort = DataSort.GetSortString();

            AddSummationRow_New(ly_Settlement_InOutListBindingSource, ly_Settlement_InOutListDataGridView);
            AddSummationRow_New(ly_Real_InOutListBindingSource, ly_Real_InOutListDataGridView);
        }

        private void toolStripButton9_Click(object sender, EventArgs e)
        {
            if (null == this.ly_financial_monthly_settlement_mainDataGridView.CurrentRow) return;



            string settlementNum = this.ly_financial_monthly_settlement_mainDataGridView.CurrentRow.Cells["月结编码"].Value.ToString();

            NewFrm.Show(this);
            Thread.Sleep(100);

            this.ly_Settlement_InOutListTableAdapter.Fill(this.lYFinancialMange.ly_Settlement_InOutList, settlementNum);

            AddSummationRow_New(ly_Settlement_InOutListBindingSource, ly_Settlement_InOutListDataGridView);

            this.ly_Real_InOutListTableAdapter.Fill(this.lYFinancialMange.ly_Real_InOutList, this.开始日期DateTimePicker.Value.Date , this.结束日期DateTimePicker.Value.Date.AddDays(1));
            AddSummationRow_New(ly_Real_InOutListBindingSource, ly_Real_InOutListDataGridView);

            NewFrm.Hide(this);
        }

        private void ly_Settlement_InOutListDataGridView_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            DataGridView dgv = sender as DataGridView;
            SetRowBackgroundInOut(dgv);
        }

        private void SetRowBackgroundInOut(DataGridView dgv)
        {
            foreach (DataGridViewRow dgr in dgv.Rows)
            {
                //if ("入库" == dgr.Cells["出入"].Value.ToString())
                if ("入库" == dgr.Cells[0].Value.ToString() || "入库" == dgr.Cells[1].Value.ToString())
                {
                    dgr.DefaultCellStyle.BackColor = Color.Teal;
                    dgr.DefaultCellStyle.ForeColor = Color.White;
                }
                else
                {
                    dgr.DefaultCellStyle.BackColor = Color.White;
                    dgr.DefaultCellStyle.ForeColor = Color.Black;
                }




            }


        }

        private void toolStripButton14_Click(object sender, EventArgs e)
        {
            ExportDataGridviewTOExcell.ExportDataGridview(this.ly_Real_InOutListDataGridView, true);
        }

        private void toolStripButton28_Click(object sender, EventArgs e)
        {
            NewFrm.Show(this);
            this.ly_inma0010DataGridView.SelectionChanged -= ly_inma0010DataGridView_SelectionChanged;

            this.lY_Inventory_query_settlementTableAdapter.Fill(this.lYStoreMange.LY_Inventory_query_settlement, SQLDatabase.NowUserID, this.dateTimePicker5.Value.Date , this.dateTimePicker6.Value.Date.AddDays(1));

            CountMoney(lY_Inventory_query_settlementBindingSource, ly_inma0010DataGridView);

            this.ly_inma0010DataGridView.SelectionChanged += ly_inma0010DataGridView_SelectionChanged;

            //this.ly_sales_standard_SumTableAdapter.Fill(this.lYSalseRepair.ly_sales_standard_Sum, this.nowusercode, this.nowfillstragecode, this.dateTimePicker5.Value, this.dateTimePicker6.Value.AddDays(1));


            NewFrm.Hide(this);
        }

        private void CountMoney(BindingSource bs, DataGridView dg)
        {
            int haveHere = bs.Find("仓库", "总计");

            if (haveHere > -1)
            {
                bs.RemoveAt(haveHere);
            }

            if (null == dg.CurrentRow) return;


            decimal begin_qty = 0;
            decimal begin_money = 0;

            decimal in_qty = 0;
            decimal in_money = 0;
            decimal out_qty = 0;
            decimal out_money = 0;

            decimal end_qty = 0;
            decimal end_money = 0;



            foreach (DataGridViewRow dr in dg.Rows)
            {
                //if (System.DBNull.Value == dr.Cells["数量"].Value)
                //    sum_qty = sum_qty + 0;
                //else
                //    sum_qty = sum_qty + decimal.Parse(dr.Cells["数量"].Value.ToString());

                if ("总计" == dr.Cells["仓库"].Value.ToString())
                {
                    dg.Rows.Remove(dr);

                }
                else
                {

                    if (System.DBNull.Value == dr.Cells["期初库存"].Value)
                        begin_qty = begin_qty + 0;
                    else
                        begin_qty = begin_qty + decimal.Parse(dr.Cells["期初库存"].Value.ToString());

                    if (System.DBNull.Value == dr.Cells["期初金额0"].Value)
                        begin_money = begin_money + 0;
                    else
                        begin_money = begin_money + decimal.Parse(dr.Cells["期初金额0"].Value.ToString());

                    if (System.DBNull.Value == dr.Cells["入库数量"].Value)
                        in_qty = in_qty + 0;
                    else
                        in_qty = in_qty + decimal.Parse(dr.Cells["入库数量"].Value.ToString());

                    if (System.DBNull.Value == dr.Cells["期间入库金额"].Value)
                        in_money = in_money + 0;
                    else
                        in_money = in_money + decimal.Parse(dr.Cells["期间入库金额"].Value.ToString());

                    if (System.DBNull.Value == dr.Cells["出库数量"].Value)
                        out_qty = out_qty + 0;
                    else
                        out_qty = out_qty + decimal.Parse(dr.Cells["出库数量"].Value.ToString());

                    if (System.DBNull.Value == dr.Cells["期间出库金额"].Value)
                        out_money = out_money + 0;
                    else
                        out_money = out_money + decimal.Parse(dr.Cells["期间出库金额"].Value.ToString());


                    if (System.DBNull.Value == dr.Cells["期末库存"].Value)
                        end_qty = end_qty + 0;
                    else
                        end_qty = end_qty + decimal.Parse(dr.Cells["期末库存"].Value.ToString());

                    if (System.DBNull.Value == dr.Cells["期末金额"].Value)
                        end_money = end_money + 0;
                    else
                        end_money = end_money + decimal.Parse(dr.Cells["期末金额"].Value.ToString());


                }

                //if (System.DBNull.Value == dr.Cells["采购金额"].Value)
                //    sum_buymoney = sum_buymoney + 0;
                //else
                //    sum_buymoney = sum_buymoney + decimal.Parse(dr.Cells["采购金额"].Value.ToString());


                //if (System.DBNull.Value == dr.Cells["利息"].Value)
                //    sum_accrual = sum_accrual + 0;
                //else
                //    sum_accrual = sum_accrual + decimal.Parse(dr.Cells["利息"].Value.ToString());

                //if (System.DBNull.Value == dr.Cells["还息"].Value)
                //    sum_compensate_accrual = sum_compensate_accrual + 0;
                //else
                //    sum_compensate_accrual = sum_compensate_accrual + decimal.Parse(dr.Cells["还息"].Value.ToString());

                //if (System.DBNull.Value == dr.Cells["欠息"].Value)
                //    sum_left_accrual = sum_left_accrual + 0;
                //else
                //    sum_left_accrual = sum_left_accrual + decimal.Parse(dr.Cells["欠息"].Value.ToString());



            }
            bs.AddNew();


            dg.CurrentRow.Cells["仓库"].Value = "总计";
            dg.CurrentRow.Cells["期初库存"].Value = begin_qty;
            dg.CurrentRow.Cells["期初金额0"].Value = begin_money;
            dg.CurrentRow.Cells["入库数量"].Value = in_qty;
            dg.CurrentRow.Cells["期间入库金额"].Value = in_money;
            dg.CurrentRow.Cells["出库数量"].Value = out_qty;
            dg.CurrentRow.Cells["期间出库金额"].Value = out_money;
            dg.CurrentRow.Cells["期末库存"].Value = end_qty;
            dg.CurrentRow.Cells["期末金额"].Value = end_money;
            dg.CurrentRow.Cells["物资编号"].Value = "___";


            ////////


            bs.EndEdit();

            bs.Position = 0;
        }

        private void ly_inma0010DataGridView_SelectionChanged(object sender, EventArgs e)
        {
           

            //if (null == this.ly_inma0010DataGridView.CurrentRow) return;



            //string nowitemno = ly_inma0010DataGridView.CurrentRow.Cells["物资编号"].Value.ToString();

          

            //NewFrm.Show(this.ParentForm);
            //Thread.Sleep(100);

            //this.ly_store_in_aettlementTableAdapter.Fill(this.genQuey.ly_store_in_aettlement, nowitemno, this.dateTimePicker6.Value.Date.AddDays(1));
            //this.ly_store_out_aettlementTableAdapter.Fill(this.genQuey.ly_store_out_aettlement, nowitemno, this.dateTimePicker6.Value.Date.AddDays(1));

            ////this.ly_Settlement_InOutListTableAdapter.Fill(this.lYFinancialMange.ly_Settlement_InOutList, settlementNum);

            ////AddSummationRow_New(ly_Settlement_InOutListBindingSource, ly_Settlement_InOutListDataGridView);

            ////this.ly_Real_InOutListTableAdapter.Fill(this.lYFinancialMange.ly_Real_InOutList, DateTime.Parse(this.ly_financial_monthly_settlement_mainDataGridView.Rows[e.RowIndex].Cells["开始日期"].Value.ToString()), DateTime.Parse(this.ly_financial_monthly_settlement_mainDataGridView.Rows[e.RowIndex].Cells["结束日期"].Value.ToString()).AddDays(1));
            ////AddSummationRow_New(ly_Real_InOutListBindingSource, ly_Real_InOutListDataGridView);


            //NewFrm.Hide(this.ParentForm);
        }

        private void toolStripButton19_Click(object sender, EventArgs e)
        {
            if (null == ly_inma0010DataGridView.CurrentRow) return;

            string s = this.ly_inma0010DataGridView.CurrentRow.Cells["物资编号"].Value.ToString();


            //this.f_Item_dynamicpriceBTableAdapter.Fill(this.lYStoreMange.f_Item_dynamicpriceB, s);
            this.f_Item_dynamicpriceB_pacTableAdapter.Fill(this.lYStoreMange.f_Item_dynamicpriceB_pac, s);
        }

        private void toolStripTextBox5_KeyUp(object sender, KeyEventArgs e)
        {
            string filterString;


            filterString = GetDataGridviewMultiFilterString.DGVMultiFilterString(this.ly_inma0010DataGridView, this.toolStripTextBox5.Text);


            //this.ly_sales_standard_Report_zhongchengBindingSource.Filter = "(" + filterString + ") or 清单号='合计'";

            this.lY_Inventory_query_settlementBindingSource.Filter = "(" + filterString + ") or 仓库='总计'";

            CountMoney(lY_Inventory_query_settlementBindingSource, ly_inma0010DataGridView);
        }

        private void toolStripTextBox5_Enter(object sender, EventArgs e)
        {
            toolStripTextBox5.Text = "";

            this.lY_Inventory_query_settlementBindingSource.Filter = " ";

            CountMoney(lY_Inventory_query_settlementBindingSource, ly_inma0010DataGridView);
        }

        private void dataGridView1_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            DataGridView dgv = sender as DataGridView;
            SetRowBackgroundInOut(dgv);
        }

        private void toolStripButton25_Click(object sender, EventArgs e)
        {
            FilterForm filterForm = new FilterForm();



            List<string> ls = new List<string>();
            ls.Add("id");


            filterForm.SetSourceColumns(ly_inma0010DataGridView.Columns, ls);

            filterForm.ShowDialog();

            string nowfilter = filterForm.GetFilterString();
            if (string.IsNullOrEmpty(nowfilter))
            {
                this.lY_Inventory_query_settlementBindingSource.Filter = nowfilter;
            }
            else
            {
                this.lY_Inventory_query_settlementBindingSource.Filter = "(" + nowfilter + ")" + " or 仓库='总计'";
            }

            //this.ly_inma0010BindingSource.Filter = filterForm.GetFilterString() ;

            CountMoney(lY_Inventory_query_settlementBindingSource, ly_inma0010DataGridView);
        }

        private void toolStripButton26_Click(object sender, EventArgs e)
        {
            if (null == this.ly_inma0010DataGridView.CurrentRow) return;
            SortForm DataSort = new SortForm();

            List<string> ls = new List<string>();
            ls.Add("id");


            DataSort.SetSortColumns(ly_inma0010DataGridView.Columns, ls);
            DataSort.ShowDialog();
            this.lY_Inventory_query_settlementBindingSource.Sort = DataSort.GetSortString();
        }

        private void toolStripButton27_Click(object sender, EventArgs e)
        {
            ExportDataGridviewTOExcell.ExportDataGridview(this.ly_inma0010DataGridView, true);
        }

        private void toolStripButton35_Click(object sender, EventArgs e)
        {
            if (null == this.ly_financial_monthly_settlement_mainDataGridView.CurrentRow) return;


            NewFrm.Show(this);
            Thread.Sleep(100);
            string settlementNum = this.ly_financial_monthly_settlement_mainDataGridView.CurrentRow.Cells["月结编码"].Value.ToString();

            this.ly_dni_monthTableAdapter.Fill(this.lYStoreMange.ly_dni_month, this.开始日期DateTimePicker.Value.Date, this.结束日期DateTimePicker.Value.Date.AddDays(1));

            this.ly_dni_outTableAdapter.Fill(this.lYStoreMange.ly_dni_out, this.开始日期DateTimePicker.Value.Date, this.结束日期DateTimePicker.Value.Date.AddDays(1));






            //this.ly_Real_InOutListTableAdapter.Fill(this.lYFinancialMange.ly_Real_InOutList, this.开始日期DateTimePicker.Value, this.结束日期DateTimePicker.Value.AddDays(1));
            //AddSummationRow_New(ly_Real_InOutListBindingSource, ly_Real_InOutListDataGridView);

            NewFrm.Hide(this);
        }

        private void toolStripButton32_Click(object sender, EventArgs e)
        {
            ExportDataGridviewTOExcell.ExportDataGridview(this.dataGridView2, true);
        }

        private void toolStripButton38_Click(object sender, EventArgs e)
        {
            ExportDataGridviewTOExcell.ExportDataGridview(this.dataGridView3, true);
        }

        private void toolStripTextBox7_KeyUp(object sender, KeyEventArgs e)
        {
            string filterString;


            filterString = GetDataGridviewMultiFilterString.DGVMultiFilterString(this.dataGridView2, this.toolStripTextBox7.Text);


            this.lydnimonthBindingSource.Filter = "(" + filterString + ")";



        }

        private void toolStripTextBox7_Enter(object sender, EventArgs e)
        {
            toolStripTextBox7.Text = "";

            this.lydnimonthBindingSource.Filter = "";


        }
        private void toolStripTextBox9_KeyUp(object sender, KeyEventArgs e)
        {
            string filterString;


            filterString = GetDataGridviewMultiFilterString.DGVMultiFilterString(this.dataGridView3, this.toolStripTextBox9.Text);


            this.lydnioutBindingSource.Filter = "(" + filterString + ")";



        }

        private void toolStripTextBox9_Enter(object sender, EventArgs e)
        {
            toolStripTextBox9.Text = "";
            this.lydnioutBindingSource.Filter = "";

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton rdb = sender as RadioButton;

            string filterString = "";

            if ("全部" == rdb.Text)
            {
                filterString = "";
            }
            if ("入库" == rdb.Text)
            {
                filterString = "出入='入库' or 物料编号='_Amount'";
            }
            if ("出库" == rdb.Text)
            {
                filterString = "出入='出库' or 物料编号='_Amount'";
            }


            this.ly_Settlement_InOutListBindingSource.Filter = filterString;
            this.ly_Real_InOutListBindingSource.Filter = filterString;

            AddSummationRow_New(ly_Settlement_InOutListBindingSource, ly_Settlement_InOutListDataGridView);
            AddSummationRow_New(ly_Real_InOutListBindingSource, ly_Real_InOutListDataGridView);



        }

        private void button1_Click(object sender, EventArgs e)
        {
            NewFrm.Show(this);
            this.ly_store_out_settlementViewTableAdapter.Fill(this.genQuey.ly_store_out_settlementView,DateTime.Parse( dateTimePicker1.Text), DateTime.Parse(dateTimePicker2.Text).AddDays(1));
            Store_out_settlement_Sum(lystoreoutsettlementViewBindingSource, out_dgv);

            NewFrm.Hide(this);
        }

        private void Store_out_settlement_Sum(BindingSource bs, DataGridView dgv)
        {
            //InitializeApp();
            //return;

            DataRow sumdr = (((DataSet)bs.DataSource).Tables[bs.DataMember]).NewRow();

            if (-1 != bs.Find("计划编码", "合计"))
            {
                bs.RemoveAt(bs.Find("计划编码", "合计"));
            }

            foreach (DataGridViewRow dgvRow in dgv.Rows)
            {
                foreach (DataGridViewCell dgvCell in dgvRow.Cells)
                {
                    //foreach (DataGridViewColumn dgvColumn in dgv.Columns)
                    //{
                    if (dgvCell.Value != null && !(dgvCell.Value is DBNull))
                    {


                        if ("出库金额" == dgvCell.OwningColumn.HeaderText
                            //|| "WIP_5" == dgvCell.OwningColumn.HeaderText
                            //|| "WIP_3" == dgvCell.OwningColumn.HeaderText

                            )




                        {


                            if (sumdr[dgvCell.OwningColumn.DataPropertyName] == null || (sumdr[dgvCell.OwningColumn.DataPropertyName] is DBNull))
                                sumdr[dgvCell.OwningColumn.DataPropertyName] = 0;
                            //dgvCell .OwningColumn.Name  dgvCell.ColumnIndex

                            sumdr[dgvCell.OwningColumn.DataPropertyName] = Convert.ToDecimal(sumdr[dgvCell.OwningColumn.DataPropertyName]) + Convert.ToDecimal(dgvCell.Value);

                        }


                        //if (IsInteger(dgvCell.Value))
                        //{
                        //    if ("年份" != dgvCell.OwningColumn.HeaderText && "月份" != dgvCell.OwningColumn.HeaderText)
                        //    {
                        //        if (sumdr[dgvCell.OwningColumn.DataPropertyName] == null || (sumdr[dgvCell.OwningColumn.DataPropertyName] is DBNull))
                        //            sumdr[dgvCell.OwningColumn.DataPropertyName] = 0;


                        //        sumdr[dgvCell.OwningColumn.DataPropertyName] = Convert.ToInt64(sumdr[dgvCell.OwningColumn.DataPropertyName]) + Convert.ToInt64(dgvCell.Value);
                        //    }
                        //}
                        //else if (IsDecimal(dgvCell.Value))
                        //{
                        //    if ("年份" != dgvCell.OwningColumn.HeaderText && "月份" != dgvCell.OwningColumn.HeaderText)
                        //    {
                        //        if (sumdr[dgvCell.OwningColumn.DataPropertyName] == null || (sumdr[dgvCell.OwningColumn.DataPropertyName] is DBNull))
                        //            sumdr[dgvCell.OwningColumn.DataPropertyName] = 0;
                        //        //dgvCell .OwningColumn.Name  dgvCell.ColumnIndex

                        //        sumdr[dgvCell.OwningColumn.DataPropertyName] = Convert.ToDecimal(sumdr[dgvCell.OwningColumn.DataPropertyName]) + Convert.ToDecimal(dgvCell.Value);
                        //    }
                        //}

                        //////sumBox.Text = string.Format("{0}", sumBox.Tag);
                        //////sumBox.Invalidate();

                    }
                    //}
                }

            }

            //sumdr["sumorder"] = "02";
            sumdr["计划编码"] = "合计";
            //sumdr["名称"] = "";

            //sumdr["客户"] = "";
            //sumdr["税务"] = "";
            ((DataSet)bs.DataSource).Tables[bs.DataMember].Rows.Add(sumdr);
            bs.ResetBindings(true);

        }

        private void toolStripButton43_Click(object sender, EventArgs e)
        {
            ExportDataGridviewTOExcell.ExportDataGridview(this.out_dgv, true);
        }

        private void toolStripButton48_Click(object sender, EventArgs e)
        {
            ExportDataGridviewTOExcell.ExportDataGridview(this.in_dgv, true);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            NewFrm.Show(this);
            this.ly_store_in_View_FinTableAdapter.Fill(this.genQuey.ly_store_in_View_Fin,DateTime.Parse( dateTimePicker3.Text), DateTime.Parse(dateTimePicker4.Text).AddDays(1));
            Store_in_settlement_Sum(lystoreinViewFinBindingSource, in_dgv);
            NewFrm.Hide(this);
        }

        private void Store_in_settlement_Sum(BindingSource bs, DataGridView dgv)
        {
            //InitializeApp();
            //return;

            DataRow sumdr = (((DataSet)bs.DataSource).Tables[bs.DataMember]).NewRow();

            if (-1 != bs.Find("计划编码", "合计"))
            {
                bs.RemoveAt(bs.Find("计划编码", "合计"));
            }

            foreach (DataGridViewRow dgvRow in dgv.Rows)
            {
                foreach (DataGridViewCell dgvCell in dgvRow.Cells)
                {
                    //foreach (DataGridViewColumn dgvColumn in dgv.Columns)
                    //{
                    if (dgvCell.Value != null && !(dgvCell.Value is DBNull))
                    {


                        if ("入库金额" == dgvCell.OwningColumn.HeaderText
                            //|| "WIP_5" == dgvCell.OwningColumn.HeaderText
                            //|| "WIP_3" == dgvCell.OwningColumn.HeaderText

                            )




                        {


                            if (sumdr[dgvCell.OwningColumn.DataPropertyName] == null || (sumdr[dgvCell.OwningColumn.DataPropertyName] is DBNull))
                                sumdr[dgvCell.OwningColumn.DataPropertyName] = 0;
                            //dgvCell .OwningColumn.Name  dgvCell.ColumnIndex

                            sumdr[dgvCell.OwningColumn.DataPropertyName] = Convert.ToDecimal(sumdr[dgvCell.OwningColumn.DataPropertyName]) + Convert.ToDecimal(dgvCell.Value);

                        }


                        //if (IsInteger(dgvCell.Value))
                        //{
                        //    if ("年份" != dgvCell.OwningColumn.HeaderText && "月份" != dgvCell.OwningColumn.HeaderText)
                        //    {
                        //        if (sumdr[dgvCell.OwningColumn.DataPropertyName] == null || (sumdr[dgvCell.OwningColumn.DataPropertyName] is DBNull))
                        //            sumdr[dgvCell.OwningColumn.DataPropertyName] = 0;


                        //        sumdr[dgvCell.OwningColumn.DataPropertyName] = Convert.ToInt64(sumdr[dgvCell.OwningColumn.DataPropertyName]) + Convert.ToInt64(dgvCell.Value);
                        //    }
                        //}
                        //else if (IsDecimal(dgvCell.Value))
                        //{
                        //    if ("年份" != dgvCell.OwningColumn.HeaderText && "月份" != dgvCell.OwningColumn.HeaderText)
                        //    {
                        //        if (sumdr[dgvCell.OwningColumn.DataPropertyName] == null || (sumdr[dgvCell.OwningColumn.DataPropertyName] is DBNull))
                        //            sumdr[dgvCell.OwningColumn.DataPropertyName] = 0;
                        //        //dgvCell .OwningColumn.Name  dgvCell.ColumnIndex

                        //        sumdr[dgvCell.OwningColumn.DataPropertyName] = Convert.ToDecimal(sumdr[dgvCell.OwningColumn.DataPropertyName]) + Convert.ToDecimal(dgvCell.Value);
                        //    }
                        //}

                        //////sumBox.Text = string.Format("{0}", sumBox.Tag);
                        //////sumBox.Invalidate();

                    }
                    //}
                }

            }

            //sumdr["sumorder"] = "02";
            sumdr["计划编码"] = "合计";
            //sumdr["名称"] = "";

            //sumdr["客户"] = "";
            //sumdr["税务"] = "";
            ((DataSet)bs.DataSource).Tables[bs.DataMember].Rows.Add(sumdr);
            bs.ResetBindings(true);

        }

        private void toolStripButton56_Click(object sender, EventArgs e)
        {
            NewFrm.Show(this);
            this.lY_payable_detail_standard_SumTableAdapter.Fill(this.lYFinancialMange.LY_payable_detail_standard_Sum, this.dateTimePicker7.Value.Date);
            AddSummationRow_AccountAge(lYpayabledetailstandardSumBindingSource, dataGridView4);

            NewFrm.Hide(this);
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
        private void toolStripButton55_Click(object sender, EventArgs e)
        {
            ExportDataGridviewTOExcell.ExportDataGridview(this.dataGridView4, true);
        }

        private void toolStripButton61_Click(object sender, EventArgs e)
        {
            ExportDataGridviewTOExcell.ExportDataGridview(this.dataGridView5, true);
        }

        private void toolStripButton62_Click(object sender, EventArgs e)
        {

            NewFrm.Show(this);
            this.lY_Invoice_storeInAllTableAdapter.Fill(this.lYQualityInspector.LY_Invoice_storeInAll, DateTime.Parse(this.dateTimePicker8.Text).Date, DateTime.Parse(this.dateTimePicker9.Text).Date.AddDays(1));

            AddSummationRow_New2(lYInvoicestoreInAllBindingSource, dataGridView5);

            NewFrm.Hide(this);
        }
        private void AddSummationRow_New2(BindingSource bs, DataGridView dgv)
        {

            DataRow sumdr = (((DataSet)bs.DataSource).Tables[bs.DataMember]).NewRow();

            if (-1 != bs.Find("supplier_code", "_合计"))
            {
                bs.RemoveAt(bs.Find("supplier_code", "_合计"));
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

            sumdr["supplier_code"] = "_合计";

            ((DataSet)bs.DataSource).Tables[bs.DataMember].Rows.Add(sumdr);
            bs.ResetBindings(true);

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

        private void toolStripButton68_Click(object sender, EventArgs e)
        {
            NewFrm.Show(this);

            TableForSumWeek("FromCal");

            NewFrm.Hide(this);
        }

        private void TableForSumWeek(string nowstyle)
        {


            SqlDataAdapter myAdapter1 = new SqlDataAdapter();

            myAdapter1.SelectCommand = new SqlCommand();

            myAdapter1.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter1.SelectCommand.CommandText = "Get_SalesPrice_ByApprove_Fin_month";
            myAdapter1.SelectCommand.Connection = new SqlConnection(SQLDatabase.Connectstring);
            myAdapter1.SelectCommand.CommandTimeout = 0;

            //myAdapter1.SelectCommand.Parameters.Add("@salesperson_code", SqlDbType.VarChar);
            //myAdapter1.SelectCommand.Parameters["@salesperson_code"].Value = this.nowusercode;

            //myAdapter1.SelectCommand.Parameters.Add("@selcode", SqlDbType.VarChar);
            //myAdapter1.SelectCommand.Parameters["@selcode"].Value = this.nowfillstragecode;

            myAdapter1.SelectCommand.Parameters.Add("@begindate", SqlDbType.DateTime);
            myAdapter1.SelectCommand.Parameters["@begindate"].Value = this.dateTimePicker10.Value.Date;

            myAdapter1.SelectCommand.Parameters.Add("@enddate", SqlDbType.DateTime);
            myAdapter1.SelectCommand.Parameters["@enddate"].Value = this.dateTimePicker11.Value.Date.AddDays(1);

            myAdapter1.SelectCommand.Parameters.Add("@getstyle", SqlDbType.VarChar);
            myAdapter1.SelectCommand.Parameters["@getstyle"].Value = nowstyle;









            if (null != ds3)
                ds3.Dispose();

            ds3 = new DataSet();

            myAdapter1.Fill(ds3);


            myAdapter1.Dispose();

            //this.dataGridView1.DataSource = allData;


            this.dataGridView6.Columns.Clear();

            this.dataGridView6.AutoGenerateColumns = true;
            dataGridView6.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCellsExceptHeaders;

            //BindingSource bindingSource1 = new BindingSource();

            if (null != bindingSource3)
                this.bindingSource3.Dispose();

            this.bindingSource3 = new BindingSource();


            bindingSource3.DataSource = ds3.Tables[0];
            dataGridView6.DataSource = bindingSource3;

            this.bindingNavigator4.BindingSource = bindingSource3;

            bindingSource3.ResetBindings(true);


            SetDGV(this.dataGridView6);
        }
        private void SetDGV(DataGridView nowDGV)
        {
            //nowDGV.Columns[0].Visible = false;

            for (int i = 0; i < nowDGV.Columns.Count; i++)
            {
                nowDGV.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;

                if (i < 1)
                {

                    nowDGV.Columns[i].Frozen = true;
                }

                //nowDGV.Columns[i].ValueType System .Type .nowDGV.Columns[5].DefaultCellStyle.Alignment

                string weekperiod = "---";

                if ("Decimal" == nowDGV.Columns[i].ValueType.Name || "Int32" == nowDGV.Columns[i].ValueType.Name)
                {
                    nowDGV.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    nowDGV.Columns[i].DefaultCellStyle.Format = "N0";

                    if ("SUM" != nowDGV.Columns[i].Name)
                    {
                       //  GetWeekPeriod(nowDGV.Columns[i].Name, out weekperiod);

                        nowDGV.Columns[i].ToolTipText = "";
                    }
                    else
                    {

                        nowDGV.Columns[i].ToolTipText = this.dateTimePicker7.Value.Date.ToString("yyyy-MM-dd") + ":" + this.dateTimePicker8.Value.Date.ToString("yyyy-MM-dd");
                    }

                }

                //if (nowDGV.Columns[i].HeaderText.Contains("总计") || nowDGV.Columns[i].HeaderText.Contains("增长率"))
                //{
                //    //nowDGV.Columns[i].DefaultCellStyle.BackColor = Color.SkyBlue;
                //    nowDGV.Columns[i].DefaultCellStyle.ForeColor = Color.Red;


                //}

                //if (nowDGV.Columns[i].HeaderText.Contains("总计") || nowDGV.Columns[i].HeaderText.Contains("合计"))
                if (nowDGV.Columns[i].HeaderText.Contains("SUM"))
                {
                    nowDGV.Columns[i].DefaultCellStyle.BackColor = Color.Teal;
                    nowDGV.Columns[i].DefaultCellStyle.ForeColor = Color.Navy;
                }



            }
        }

        //private void fillToolStripButton_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        this.get_SalesPrice_ByApprove_Fin_month_detailTableAdapter.Fill(this.lYFinancialMange.Get_SalesPrice_ByApprove_Fin_month_detail, new System.Nullable<System.DateTime>(((System.DateTime)(System.Convert.ChangeType(enddateToolStripTextBox.Text, typeof(System.DateTime))))));
        //    }
        //    catch (System.Exception ex)
        //    {
        //        System.Windows.Forms.MessageBox.Show(ex.Message);
        //    }

        //}

        private void toolStripButton74_Click(object sender, EventArgs e)
        {
            NewFrm.Show(this );

            this.get_SalesPrice_ByApprove_Fin_month_detailTableAdapter.Fill(this.lYFinancialMange.Get_SalesPrice_ByApprove_Fin_month_detail, this.dateTimePicker11.Value.Date.AddDays(1));
      


        AddSummationRow_DashboardDetail(get_SalesPrice_ByApprove_Fin_month_detailBindingSource, get_SalesPrice_ByApprove_Fin_month_detailDataGridView);

        NewFrm.Hide(this);
        }
        private void AddSummationRow_DashboardDetail(BindingSource bs, DataGridView dgv)
        {

            DataRow sumdr = (((DataSet)bs.DataSource).Tables[bs.DataMember]).NewRow();

            if (-1 != bs.Find("物料编码", "总计"))
            {
                bs.RemoveAt(bs.Find("物料编码", "总计"));
            }

            foreach (DataGridViewRow dgvRow in dgv.Rows)
            {
                foreach (DataGridViewCell dgvCell in dgvRow.Cells)
                {
                    if (dgvCell.Value != null && !(dgvCell.Value is DBNull))
                    {


                        
                              if ("库存金额" == dgvCell.OwningColumn.HeaderText )
                        {
                            if (sumdr[dgvCell.OwningColumn.DataPropertyName] == null || (sumdr[dgvCell.OwningColumn.DataPropertyName] is DBNull))
                                sumdr[dgvCell.OwningColumn.DataPropertyName] = 0;


                            sumdr[dgvCell.OwningColumn.DataPropertyName] = Convert.ToDecimal(sumdr[dgvCell.OwningColumn.DataPropertyName]) + Convert.ToDecimal(dgvCell.Value);
                        }

                        //if (IsInteger(dgvCell.Value))
                        //{
                        //    if ("年份" != dgvCell.OwningColumn.HeaderText && "月份" != dgvCell.OwningColumn.HeaderText)
                        //    {
                        //        if (sumdr[dgvCell.OwningColumn.DataPropertyName] == null || (sumdr[dgvCell.OwningColumn.DataPropertyName] is DBNull))
                        //            sumdr[dgvCell.OwningColumn.DataPropertyName] = 0;


                        //        sumdr[dgvCell.OwningColumn.DataPropertyName] = Convert.ToInt64(sumdr[dgvCell.OwningColumn.DataPropertyName]) + Convert.ToInt64(dgvCell.Value);
                        //    }
                        //}
                        //else if (IsDecimal(dgvCell.Value))
                        //{
                        //    if ("年份" != dgvCell.OwningColumn.HeaderText && "月份" != dgvCell.OwningColumn.HeaderText)
                        //    {
                        //        if (sumdr[dgvCell.OwningColumn.DataPropertyName] == null || (sumdr[dgvCell.OwningColumn.DataPropertyName] is DBNull))
                        //            sumdr[dgvCell.OwningColumn.DataPropertyName] = 0;
                        //        sumdr[dgvCell.OwningColumn.DataPropertyName] = Convert.ToDecimal(sumdr[dgvCell.OwningColumn.DataPropertyName]) + Convert.ToDecimal(dgvCell.Value);
                        //    }
                        //}


                    }
                }

            }

            //////////
            sumdr["物料编码"] = "总计";
            ((DataSet)bs.DataSource).Tables[bs.DataMember].Rows.Add(sumdr);
            bs.ResetBindings(true);

        }

        //private void fillToolStripButton_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        this.ly_wip_viewTableAdapter.Fill(this.lYStoreMange.ly_wip_view, new System.Nullable<System.DateTime>(((System.DateTime)(System.Convert.ChangeType(begindateToolStripTextBox.Text, typeof(System.DateTime))))), new System.Nullable<System.DateTime>(((System.DateTime)(System.Convert.ChangeType(enddateToolStripTextBox.Text, typeof(System.DateTime))))));
        //    }
        //    catch (System.Exception ex)
        //    {
        //        System.Windows.Forms.MessageBox.Show(ex.Message);
        //    }

        //}

        private void bindingNavigator11_RefreshItems(object sender, EventArgs e)
        {

        }

        private void toolStripButton80_Click(object sender, EventArgs e)
        {

            NewFrm.Show(this);
            Thread.Sleep(500);
            this.ly_get_wip_finTableAdapter.Fill(this.lYStoreMange.ly_get_wip_fin, dateTimePicker19.Value.Date, dateTimePicker20.Value.Date.AddDays(1));
            NewFrm.Hide(this);
        }

        ////private void fillToolStripButton_Click(object sender, EventArgs e)
        ////{
        ////    try
        ////    {
        ////        this.ly_sales_cost_Detail_BOMTableAdapter.Fill(this.lYSalseMange2.ly_sales_cost_Detail_BOM, new System.Nullable<System.DateTime>(((System.DateTime)(System.Convert.ChangeType(begindateToolStripTextBox.Text, typeof(System.DateTime))))), new System.Nullable<System.DateTime>(((System.DateTime)(System.Convert.ChangeType(enddateToolStripTextBox.Text, typeof(System.DateTime))))), timetypeToolStripTextBox.Text);
        ////    }
        ////    catch (System.Exception ex)
        ////    {
        ////        System.Windows.Forms.MessageBox.Show(ex.Message);
        ////    }

        ////}

        private void dateTimePicker13_ValueChanged(object sender, EventArgs e)
        {

        }

        private void toolStripButton95_Click(object sender, EventArgs e)
        {
            NewFrm.Show(this);
            Thread.Sleep(100);

          
                //this.ly_sales_cost_ReportTableAdapter.Fill(this.lYSalseMange2.ly_sales_cost_Report, this.nowusercode, this.nowfillstragecode, this.dateTimePicker3.Value, this.dateTimePicker4.Value.AddDays(1), "invoice");
                this.ly_sales_cost_Detail_BOMTableAdapter.Fill(this.lYSalseMange2.ly_sales_cost_Detail_BOM, this.dateTimePicker15.Value.Date, this.dateTimePicker16.Value.Date.AddDays(1), "invoice");
          

            AddSummationRow_Detail_BOM(ly_sales_cost_Detail_BOMBindingSource, ly_sales_cost_Detail_BOMDataGridView);

            NewFrm.Hide(this);
        }
        private void AddSummationRow_Detail_BOM(BindingSource bs, DataGridView dgv)
        {
            //InitializeApp();
            //return;

            DataRow sumdr = (((DataSet)bs.DataSource).Tables[bs.DataMember]).NewRow();

            if (-1 != bs.Find("大类", "合计"))
            {
                bs.RemoveAt(bs.Find("大类", "合计"));
            }

            foreach (DataGridViewRow dgvRow in dgv.Rows)
            {
                foreach (DataGridViewCell dgvCell in dgvRow.Cells)
                {
                    //foreach (DataGridViewColumn dgvColumn in dgv.Columns)
                    //{
                    if (dgvCell.Value != null && !(dgvCell.Value is DBNull))
                    {


                        if ("BOM金额" == dgvCell.OwningColumn.HeaderText
                            || "成本A_总" == dgvCell.OwningColumn.HeaderText
                            || "成本B_总" == dgvCell.OwningColumn.HeaderText
                            || "成本C_总" == dgvCell.OwningColumn.HeaderText
                            || "成本A_自" == dgvCell.OwningColumn.HeaderText
                            || "成本B_自" == dgvCell.OwningColumn.HeaderText
                            || "成本C_自" == dgvCell.OwningColumn.HeaderText
                            || "销售金额" == dgvCell.OwningColumn.HeaderText
                            || "销售金额_NOVAT" == dgvCell.OwningColumn.HeaderText

                            || "纯原料BOM_金额" == dgvCell.OwningColumn.HeaderText
                            || "原料BOM_金额" == dgvCell.OwningColumn.HeaderText
                            || "外协BOM_金额" == dgvCell.OwningColumn.HeaderText
                            || "机外BOM_金额" == dgvCell.OwningColumn.HeaderText
                            || "人工BOM_金额" == dgvCell.OwningColumn.HeaderText
                            || "装配BOM_金额" == dgvCell.OwningColumn.HeaderText


                            || "纯原料B_金额" == dgvCell.OwningColumn.HeaderText
                            || "原料B_金额" == dgvCell.OwningColumn.HeaderText
                            || "外协B_金额" == dgvCell.OwningColumn.HeaderText
                            || "机外B_金额" == dgvCell.OwningColumn.HeaderText
                            || "人工B_金额" == dgvCell.OwningColumn.HeaderText
                            || "装配B_金额" == dgvCell.OwningColumn.HeaderText

                            || "销售成本总金额" == dgvCell.OwningColumn.HeaderText
                            )




                        {


                            if (sumdr[dgvCell.OwningColumn.DataPropertyName] == null || (sumdr[dgvCell.OwningColumn.DataPropertyName] is DBNull))
                                sumdr[dgvCell.OwningColumn.DataPropertyName] = 0;
                            //dgvCell .OwningColumn.Name  dgvCell.ColumnIndex

                            sumdr[dgvCell.OwningColumn.DataPropertyName] = Convert.ToDecimal(sumdr[dgvCell.OwningColumn.DataPropertyName]) + Convert.ToDecimal(dgvCell.Value);

                        }


                        //if (IsInteger(dgvCell.Value))
                        //{
                        //    if ("年份" != dgvCell.OwningColumn.HeaderText && "月份" != dgvCell.OwningColumn.HeaderText)
                        //    {
                        //        if (sumdr[dgvCell.OwningColumn.DataPropertyName] == null || (sumdr[dgvCell.OwningColumn.DataPropertyName] is DBNull))
                        //            sumdr[dgvCell.OwningColumn.DataPropertyName] = 0;


                        //        sumdr[dgvCell.OwningColumn.DataPropertyName] = Convert.ToInt64(sumdr[dgvCell.OwningColumn.DataPropertyName]) + Convert.ToInt64(dgvCell.Value);
                        //    }
                        //}
                        //else if (IsDecimal(dgvCell.Value))
                        //{
                        //    if ("年份" != dgvCell.OwningColumn.HeaderText && "月份" != dgvCell.OwningColumn.HeaderText)
                        //    {
                        //        if (sumdr[dgvCell.OwningColumn.DataPropertyName] == null || (sumdr[dgvCell.OwningColumn.DataPropertyName] is DBNull))
                        //            sumdr[dgvCell.OwningColumn.DataPropertyName] = 0;
                        //        //dgvCell .OwningColumn.Name  dgvCell.ColumnIndex

                        //        sumdr[dgvCell.OwningColumn.DataPropertyName] = Convert.ToDecimal(sumdr[dgvCell.OwningColumn.DataPropertyName]) + Convert.ToDecimal(dgvCell.Value);
                        //    }
                        //}

                        ////sumBox.Text = string.Format("{0}", sumBox.Tag);
                        ////sumBox.Invalidate();

                    }
                    //}
                }

            }

            //sumdr["sumorder"] = "02";
            sumdr["大类"] = "合计";
            sumdr["物料比例BOM"] = 100 * Convert.ToDecimal(sumdr["纯原料BOM_金额"]) / Convert.ToDecimal(sumdr["BOM金额"]);
            sumdr["物料比例B"] = 100 * Convert.ToDecimal(sumdr["纯原料B_金额"]) / Convert.ToDecimal(sumdr["成本B_总"]);
            //sumdr["客户"] = "";
            //sumdr["税务"] = "";
            ((DataSet)bs.DataSource).Tables[bs.DataMember].Rows.Add(sumdr);
            bs.ResetBindings(true);

        }

        private void toolStripButton90_Click(object sender, EventArgs e)
        {
            if (null == this.ly_sales_cost_Detail_BOMDataGridView.CurrentRow) return;

            FilterForm filterForm = new FilterForm();


            List<string> ls = new List<string>();
            ls.Add("id");


            filterForm.SetSourceColumns(this.lYSalseMange2.ly_sales_cost_Detail_BOM.Columns, ls);

            filterForm.ShowDialog();

            string filterstr = filterForm.GetFilterString();
            if (!string.IsNullOrEmpty(filterstr))
            {

                //this.lysalescostReportBindingSource.Filter = "(" + filterstr + ") or 清单号='合计'";

                // this.ly_sales_cost_Detail_ReportBindingSource.Filter = filterstr;

                this.ly_sales_cost_Detail_BOMBindingSource.Filter = "(" + filterstr + ") ";
                //AddSummationRow_Detail_New(ly_sales_cost_Detail_ReportBindingSource, ly_sales_cost_Detail_ReportDataGridView);
            }
        }

        private void toolStripButton89_Click(object sender, EventArgs e)
        {
            ExportDataGridviewTOExcell.ExportDataGridview(this.ly_sales_cost_Detail_BOMDataGridView, true);
        }

        private void toolStripButton92_Click(object sender, EventArgs e)
        {

            NewFrm.Show(this);
            Thread.Sleep(500);
            this.ly_get_wip_detail_finTableAdapter.Fill(this.lYStoreMange.ly_get_wip_detail_fin, dateTimePicker19.Value.Date, dateTimePicker20.Value.Date.AddDays(1));
            NewFrm.Hide(this);
        }

       
        private void toolStripButton105_Click(object sender, EventArgs e)
        {
            

                 NewFrm.Show(this);
            Thread.Sleep(500);
            this.ly_get_wip_storeoutTableAdapter.Fill(this.lYStoreMange.ly_get_wip_storeout, dateTimePicker19.Value.Date, dateTimePicker20.Value.Date.AddDays(1));
            AddSummationRow_wip_storeout(ly_get_wip_storeoutBindingSource, ly_get_wip_storeoutDataGridView);
            NewFrm.Hide(this);
        }

       

      
        private void AddSummationRow_wip_storeout(BindingSource bs, DataGridView dgv)
        {
            //InitializeApp();
            //return;

            DataRow sumdr = (((DataSet)bs.DataSource).Tables[bs.DataMember]).NewRow();

            if (-1 != bs.Find("类别", "合计"))
            {
                bs.RemoveAt(bs.Find("类别", "合计"));
            }

            foreach (DataGridViewRow dgvRow in dgv.Rows)
            {
                foreach (DataGridViewCell dgvCell in dgvRow.Cells)
                {
                    //foreach (DataGridViewColumn dgvColumn in dgv.Columns)
                    //{
                    if (dgvCell.Value != null && !(dgvCell.Value is DBNull))
                    {


                        if ("WIP_7" == dgvCell.OwningColumn.HeaderText
                            || "WIP_5" == dgvCell.OwningColumn.HeaderText
                            || "WIP_3" == dgvCell.OwningColumn.HeaderText

                            )




                        {


                            if (sumdr[dgvCell.OwningColumn.DataPropertyName] == null || (sumdr[dgvCell.OwningColumn.DataPropertyName] is DBNull))
                                sumdr[dgvCell.OwningColumn.DataPropertyName] = 0;
                            //dgvCell .OwningColumn.Name  dgvCell.ColumnIndex

                            sumdr[dgvCell.OwningColumn.DataPropertyName] = Convert.ToDecimal(sumdr[dgvCell.OwningColumn.DataPropertyName]) + Convert.ToDecimal(dgvCell.Value);

                        }


                        //if (IsInteger(dgvCell.Value))
                        //{
                        //    if ("年份" != dgvCell.OwningColumn.HeaderText && "月份" != dgvCell.OwningColumn.HeaderText)
                        //    {
                        //        if (sumdr[dgvCell.OwningColumn.DataPropertyName] == null || (sumdr[dgvCell.OwningColumn.DataPropertyName] is DBNull))
                        //            sumdr[dgvCell.OwningColumn.DataPropertyName] = 0;


                        //        sumdr[dgvCell.OwningColumn.DataPropertyName] = Convert.ToInt64(sumdr[dgvCell.OwningColumn.DataPropertyName]) + Convert.ToInt64(dgvCell.Value);
                        //    }
                        //}
                        //else if (IsDecimal(dgvCell.Value))
                        //{
                        //    if ("年份" != dgvCell.OwningColumn.HeaderText && "月份" != dgvCell.OwningColumn.HeaderText)
                        //    {
                        //        if (sumdr[dgvCell.OwningColumn.DataPropertyName] == null || (sumdr[dgvCell.OwningColumn.DataPropertyName] is DBNull))
                        //            sumdr[dgvCell.OwningColumn.DataPropertyName] = 0;
                        //        //dgvCell .OwningColumn.Name  dgvCell.ColumnIndex

                        //        sumdr[dgvCell.OwningColumn.DataPropertyName] = Convert.ToDecimal(sumdr[dgvCell.OwningColumn.DataPropertyName]) + Convert.ToDecimal(dgvCell.Value);
                        //    }
                        //}

                        ////sumBox.Text = string.Format("{0}", sumBox.Tag);
                        ////sumBox.Invalidate();

                    }
                    //}
                }

            }

            //sumdr["sumorder"] = "02";
            sumdr["类别"] = "合计";
            
            //sumdr["客户"] = "";
            //sumdr["税务"] = "";
            ((DataSet)bs.DataSource).Tables[bs.DataMember].Rows.Add(sumdr);
            bs.ResetBindings(true);

        }

        private void toolStripButton99_Click(object sender, EventArgs e)
        {

            NewFrm.Show(this);
            Thread.Sleep(500);
            this.ly_get_wip_storeinTableAdapter.Fill(this.lYStoreMange.ly_get_wip_storein, dateTimePicker19.Value.Date, dateTimePicker20.Value.Date.AddDays(1));
            AddSummationRow_wip_storeout(ly_get_wip_storeinBindingSource, ly_get_wip_storeinDataGridView);
            NewFrm.Hide(this);
        }

        private void ly_get_wip_finDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void toolStripButton111_Click(object sender, EventArgs e)
        {
            NewFrm.Show(this);
            Thread.Sleep(500);
            this.ly_store_inout_wip_periodTableAdapter.Fill(this.lYFinancialMange.ly_store_inout_wip_period, dateTimePicker68.Value.Date, dateTimePicker69.Value.Date.AddDays(1));
            AddSummationRow_wip_inout_period(ly_store_inout_wip_periodBindingSource, ly_store_inout_wip_periodDataGridView);
            NewFrm.Hide(this);
        }
        private void AddSummationRow_wip_inout_period(BindingSource bs, DataGridView dgv)
        {
            //InitializeApp();
            //return;

            DataRow sumdr = (((DataSet)bs.DataSource).Tables[bs.DataMember]).NewRow();

            if (-1 != bs.Find("类别", "合计"))
            {
                bs.RemoveAt(bs.Find("类别", "合计"));
            }

            foreach (DataGridViewRow dgvRow in dgv.Rows)
            {
                foreach (DataGridViewCell dgvCell in dgvRow.Cells)
                {
                    //foreach (DataGridViewColumn dgvColumn in dgv.Columns)
                    //{
                    if (dgvCell.Value != null && !(dgvCell.Value is DBNull))
                    {


                        //if ("WIP_7" == dgvCell.OwningColumn.HeaderText
                        //    || "WIP_5" == dgvCell.OwningColumn.HeaderText
                        //    || "WIP_3" == dgvCell.OwningColumn.HeaderText

                        //    )




                        //{


                        //    if (sumdr[dgvCell.OwningColumn.DataPropertyName] == null || (sumdr[dgvCell.OwningColumn.DataPropertyName] is DBNull))
                        //        sumdr[dgvCell.OwningColumn.DataPropertyName] = 0;
                        //    //dgvCell .OwningColumn.Name  dgvCell.ColumnIndex

                        //    sumdr[dgvCell.OwningColumn.DataPropertyName] = Convert.ToDecimal(sumdr[dgvCell.OwningColumn.DataPropertyName]) + Convert.ToDecimal(dgvCell.Value);

                        //}


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

                        ////sumBox.Text = string.Format("{0}", sumBox.Tag);
                        ////sumBox.Invalidate();

                    }
                    //}
                }

            }

            //sumdr["sumorder"] = "02";
            sumdr["类别"] = "合计";
            sumdr["名称"] = "";

            //sumdr["客户"] = "";
            //sumdr["税务"] = "";
            ((DataSet)bs.DataSource).Tables[bs.DataMember].Rows.Add(sumdr);
            bs.ResetBindings(true);

        }

        private void toolStripButton117_Click(object sender, EventArgs e)
        {
            NewFrm.Show(this);
            Thread.Sleep(500);
            this.ly_get_wip_storeout_periodTableAdapter.Fill(this.lYFinancialMange.ly_get_wip_storeout_period, dateTimePicker68.Value.Date, dateTimePicker69.Value.Date.AddDays(1));
            AddSummationRow_wip_out_period(ly_get_wip_storeout_periodBindingSource, ly_get_wip_storeout_periodDataGridView);
            NewFrm.Hide(this);
        }
        private void AddSummationRow_wip_out_period(BindingSource bs, DataGridView dgv)
        {
            //InitializeApp();
            //return;

            DataRow sumdr = (((DataSet)bs.DataSource).Tables[bs.DataMember]).NewRow();

            if (-1 != bs.Find("类别", "合计"))
            {
                bs.RemoveAt(bs.Find("类别", "合计"));
            }

            foreach (DataGridViewRow dgvRow in dgv.Rows)
            {
                foreach (DataGridViewCell dgvCell in dgvRow.Cells)
                {
                    //foreach (DataGridViewColumn dgvColumn in dgv.Columns)
                    //{
                    if (dgvCell.Value != null && !(dgvCell.Value is DBNull))
                    {


                        if ("出库金额" == dgvCell.OwningColumn.HeaderText
                            || "WIP_5" == dgvCell.OwningColumn.HeaderText
                            || "WIP_3" == dgvCell.OwningColumn.HeaderText

                            )




                        {


                            if (sumdr[dgvCell.OwningColumn.DataPropertyName] == null || (sumdr[dgvCell.OwningColumn.DataPropertyName] is DBNull))
                                sumdr[dgvCell.OwningColumn.DataPropertyName] = 0;
                            //dgvCell .OwningColumn.Name  dgvCell.ColumnIndex

                            sumdr[dgvCell.OwningColumn.DataPropertyName] = Convert.ToDecimal(sumdr[dgvCell.OwningColumn.DataPropertyName]) + Convert.ToDecimal(dgvCell.Value);

                        }


                        //if (IsInteger(dgvCell.Value))
                        //{
                        //    if ("年份" != dgvCell.OwningColumn.HeaderText && "月份" != dgvCell.OwningColumn.HeaderText)
                        //    {
                        //        if (sumdr[dgvCell.OwningColumn.DataPropertyName] == null || (sumdr[dgvCell.OwningColumn.DataPropertyName] is DBNull))
                        //            sumdr[dgvCell.OwningColumn.DataPropertyName] = 0;


                        //        sumdr[dgvCell.OwningColumn.DataPropertyName] = Convert.ToInt64(sumdr[dgvCell.OwningColumn.DataPropertyName]) + Convert.ToInt64(dgvCell.Value);
                        //    }
                        //}
                        //else if (IsDecimal(dgvCell.Value))
                        //{
                        //    if ("年份" != dgvCell.OwningColumn.HeaderText && "月份" != dgvCell.OwningColumn.HeaderText)
                        //    {
                        //        if (sumdr[dgvCell.OwningColumn.DataPropertyName] == null || (sumdr[dgvCell.OwningColumn.DataPropertyName] is DBNull))
                        //            sumdr[dgvCell.OwningColumn.DataPropertyName] = 0;
                        //        //dgvCell .OwningColumn.Name  dgvCell.ColumnIndex

                        //        sumdr[dgvCell.OwningColumn.DataPropertyName] = Convert.ToDecimal(sumdr[dgvCell.OwningColumn.DataPropertyName]) + Convert.ToDecimal(dgvCell.Value);
                        //    }
                        //}

                        //////sumBox.Text = string.Format("{0}", sumBox.Tag);
                        //////sumBox.Invalidate();

                    }
                    //}
                }

            }

            //sumdr["sumorder"] = "02";
            sumdr["类别"] = "合计";

            //sumdr["客户"] = "";
            //sumdr["税务"] = "";
            ((DataSet)bs.DataSource).Tables[bs.DataMember].Rows.Add(sumdr);
            bs.ResetBindings(true);

        }
        private void toolStripButton123_Click(object sender, EventArgs e)
        {
            NewFrm.Show(this);
            Thread.Sleep(500);
            this.ly_get_wip_storein_periodTableAdapter.Fill(this.lYFinancialMange.ly_get_wip_storein_period, dateTimePicker68.Value.Date, dateTimePicker69.Value.Date.AddDays(1));
            AddSummationRow_wip_in_period(ly_get_wip_storein_periodBindingSource, ly_get_wip_storein_periodDataGridView);
            NewFrm.Hide(this);
        }
        private void AddSummationRow_wip_in_period(BindingSource bs, DataGridView dgv)
        {
            //InitializeApp();
            //return;

            DataRow sumdr = (((DataSet)bs.DataSource).Tables[bs.DataMember]).NewRow();

            if (-1 != bs.Find("类别", "合计"))
            {
                bs.RemoveAt(bs.Find("类别", "合计"));
            }

            foreach (DataGridViewRow dgvRow in dgv.Rows)
            {
                foreach (DataGridViewCell dgvCell in dgvRow.Cells)
                {
                    //foreach (DataGridViewColumn dgvColumn in dgv.Columns)
                    //{
                    if (dgvCell.Value != null && !(dgvCell.Value is DBNull))
                    {


                        if ("入库金额" == dgvCell.OwningColumn.HeaderText
                            || "WIP_5" == dgvCell.OwningColumn.HeaderText
                            || "WIP_3" == dgvCell.OwningColumn.HeaderText

                            )




                        {


                            if (sumdr[dgvCell.OwningColumn.DataPropertyName] == null || (sumdr[dgvCell.OwningColumn.DataPropertyName] is DBNull))
                                sumdr[dgvCell.OwningColumn.DataPropertyName] = 0;
                            //dgvCell .OwningColumn.Name  dgvCell.ColumnIndex

                            sumdr[dgvCell.OwningColumn.DataPropertyName] = Convert.ToDecimal(sumdr[dgvCell.OwningColumn.DataPropertyName]) + Convert.ToDecimal(dgvCell.Value);

                        }


                        //if (IsInteger(dgvCell.Value))
                        //{
                        //    if ("年份" != dgvCell.OwningColumn.HeaderText && "月份" != dgvCell.OwningColumn.HeaderText)
                        //    {
                        //        if (sumdr[dgvCell.OwningColumn.DataPropertyName] == null || (sumdr[dgvCell.OwningColumn.DataPropertyName] is DBNull))
                        //            sumdr[dgvCell.OwningColumn.DataPropertyName] = 0;


                        //        sumdr[dgvCell.OwningColumn.DataPropertyName] = Convert.ToInt64(sumdr[dgvCell.OwningColumn.DataPropertyName]) + Convert.ToInt64(dgvCell.Value);
                        //    }
                        //}
                        //else if (IsDecimal(dgvCell.Value))
                        //{
                        //    if ("年份" != dgvCell.OwningColumn.HeaderText && "月份" != dgvCell.OwningColumn.HeaderText)
                        //    {
                        //        if (sumdr[dgvCell.OwningColumn.DataPropertyName] == null || (sumdr[dgvCell.OwningColumn.DataPropertyName] is DBNull))
                        //            sumdr[dgvCell.OwningColumn.DataPropertyName] = 0;
                        //        //dgvCell .OwningColumn.Name  dgvCell.ColumnIndex

                        //        sumdr[dgvCell.OwningColumn.DataPropertyName] = Convert.ToDecimal(sumdr[dgvCell.OwningColumn.DataPropertyName]) + Convert.ToDecimal(dgvCell.Value);
                        //    }
                        //}

                        //////sumBox.Text = string.Format("{0}", sumBox.Tag);
                        //////sumBox.Invalidate();

                    }
                    //}
                }

            }

            //sumdr["sumorder"] = "02";
            sumdr["类别"] = "合计";

            //sumdr["客户"] = "";
            //sumdr["税务"] = "";
            ((DataSet)bs.DataSource).Tables[bs.DataMember].Rows.Add(sumdr);
            bs.ResetBindings(true);

        }

        private void label19_Click(object sender, EventArgs e)
        {

        }

        private void dateTimePicker15_ValueChanged(object sender, EventArgs e)
        {

        }

        private void label18_Click(object sender, EventArgs e)
        {

        }

        private void toolStripButton129_Click(object sender, EventArgs e)
        {
            NewFrm.Show(this);
            Thread.Sleep(500);
            this.ly_get_wip_material_periodTableAdapter.Fill(this.lYFinancialMange.ly_get_wip_material_period, dateTimePicker21.Value.Date, dateTimePicker22.Value.Date.AddDays(1));
            AddSummationRow_wip_material_period(ly_get_wip_material_periodBindingSource, ly_get_wip_material_periodDataGridView);
            NewFrm.Hide(this);
        }
        private void AddSummationRow_wip_material_period(BindingSource bs, DataGridView dgv)
        {
            //InitializeApp();
            //return;

            DataRow sumdr = (((DataSet)bs.DataSource).Tables[bs.DataMember]).NewRow();

            if (-1 != bs.Find("类别", "合计"))
            {
                bs.RemoveAt(bs.Find("类别", "合计"));
            }

            foreach (DataGridViewRow dgvRow in dgv.Rows)
            {
                foreach (DataGridViewCell dgvCell in dgvRow.Cells)
                {
                    //foreach (DataGridViewColumn dgvColumn in dgv.Columns)
                    //{
                    if (dgvCell.Value != null && !(dgvCell.Value is DBNull))
                    {


                        if ("入库金额" == dgvCell.OwningColumn.HeaderText
                            || "WIP_nat" == dgvCell.OwningColumn.HeaderText
                            || "WIP_real" == dgvCell.OwningColumn.HeaderText

                            )




                        {


                            if (sumdr[dgvCell.OwningColumn.DataPropertyName] == null || (sumdr[dgvCell.OwningColumn.DataPropertyName] is DBNull))
                                sumdr[dgvCell.OwningColumn.DataPropertyName] = 0;
                            //dgvCell .OwningColumn.Name  dgvCell.ColumnIndex

                            sumdr[dgvCell.OwningColumn.DataPropertyName] = Convert.ToDecimal(sumdr[dgvCell.OwningColumn.DataPropertyName]) + Convert.ToDecimal(dgvCell.Value);

                        }


                        //if (IsInteger(dgvCell.Value))
                        //{
                        //    if ("年份" != dgvCell.OwningColumn.HeaderText && "月份" != dgvCell.OwningColumn.HeaderText)
                        //    {
                        //        if (sumdr[dgvCell.OwningColumn.DataPropertyName] == null || (sumdr[dgvCell.OwningColumn.DataPropertyName] is DBNull))
                        //            sumdr[dgvCell.OwningColumn.DataPropertyName] = 0;


                        //        sumdr[dgvCell.OwningColumn.DataPropertyName] = Convert.ToInt64(sumdr[dgvCell.OwningColumn.DataPropertyName]) + Convert.ToInt64(dgvCell.Value);
                        //    }
                        //}
                        //else if (IsDecimal(dgvCell.Value))
                        //{
                        //    if ("年份" != dgvCell.OwningColumn.HeaderText && "月份" != dgvCell.OwningColumn.HeaderText)
                        //    {
                        //        if (sumdr[dgvCell.OwningColumn.DataPropertyName] == null || (sumdr[dgvCell.OwningColumn.DataPropertyName] is DBNull))
                        //            sumdr[dgvCell.OwningColumn.DataPropertyName] = 0;
                        //        //dgvCell .OwningColumn.Name  dgvCell.ColumnIndex

                        //        sumdr[dgvCell.OwningColumn.DataPropertyName] = Convert.ToDecimal(sumdr[dgvCell.OwningColumn.DataPropertyName]) + Convert.ToDecimal(dgvCell.Value);
                        //    }
                        //}

                        //////sumBox.Text = string.Format("{0}", sumBox.Tag);
                        //////sumBox.Invalidate();

                    }
                    //}
                }

            }

            //sumdr["sumorder"] = "02";
            sumdr["类别"] = "合计";

            //sumdr["客户"] = "";
            //sumdr["税务"] = "";
            ((DataSet)bs.DataSource).Tables[bs.DataMember].Rows.Add(sumdr);
            bs.ResetBindings(true);

        }

       
       

        private void toolStripButton128_Click(object sender, EventArgs e)
        {
            ExportDataGridviewTOExcell.ExportDataGridview(this.ly_get_wip_material_periodDataGridView, true);
        }

        //private void fillToolStripButton_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        this.f_Item_dynamicpriceBTableAdapter.Fill(this.lYStoreMange.f_Item_dynamicpriceB, itemnoToolStripTextBox.Text);

        //    }
        //    catch (System.Exception ex)
        //    {
        //        System.Windows.Forms.MessageBox.Show(ex.Message);
        //    }

        //}

        private void toolStripButton134_Click(object sender, EventArgs e)
        {
           
            if (null == ly_inma0010DataGridView.CurrentRow) return;

            string s = this.ly_inma0010DataGridView.CurrentRow.Cells["物资编号"].Value.ToString();

            this.f_Item_dynamicpriceBTableAdapter.Fill(this.lYStoreMange.f_Item_dynamicpriceB, s);

        }

        private void dataGridView7_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            DataGridView dgv = sender as DataGridView;
            SetRowBackgroundInOut(dgv);
        }

        private void 查看入库价格来源ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //DataGridView dgv = sender as DataGridView;

            if (dataGridView1.CurrentRow == null) return;
            if ("出库" == dataGridView1.CurrentRow.Cells["出入B"].Value.ToString()) return;

            string wzbh = dataGridView1.CurrentRow.Cells["wzbhB"].Value.ToString();
            string djbh = dataGridView1.CurrentRow.Cells["原始单据B"].Value.ToString();
            string machine_Code = dataGridView1.CurrentRow.Cells["machine_num"].Value.ToString();
            string storeInId = dataGridView1.CurrentRow.Cells["origin_id"].Value.ToString();

            if (string.IsNullOrEmpty(djbh)) return;

            if (djbh.Length < 2) return;

            string Rs = djbh.Substring(0, 2);

            switch (Rs)
            {
                case "CG":

                    LY_GetPurchasePrice queryForm = new LY_GetPurchasePrice();

                    queryForm.InStr = djbh;
                    queryForm.Code = wzbh;

                    queryForm.StartPosition = FormStartPosition.CenterParent;

                    queryForm.ShowDialog();

                    break;




                case "GD":

                    LY_GetRestructuringPrice queryFormDG = new LY_GetRestructuringPrice();

                    queryFormDG.InStr = djbh;
                    queryFormDG.Code = machine_Code;

                    queryFormDG.StartPosition = FormStartPosition.CenterParent;

                    queryFormDG.ShowDialog();

                    break;

                case "GQ":

                    LY_GetRestructuringPrice queryFormQG = new LY_GetRestructuringPrice();

                    queryFormQG.InStr = djbh;
                    queryFormQG.Code = machine_Code;

                    queryFormQG.StartPosition = FormStartPosition.CenterParent;

                    queryFormQG.ShowDialog();

                    break;

                case "DZ":

                    LY_GetQzDzPrice queryFormDZ = new LY_GetQzDzPrice();

                    queryFormDZ.InStr = djbh;
                    queryFormDZ.Code = machine_Code;

                    queryFormDZ.StartPosition = FormStartPosition.CenterParent;
                    queryFormDZ.ShowDialog();

                    break;

                case "QZ":

                    LY_GetQzDzPrice queryFormQZ = new LY_GetQzDzPrice();

                    queryFormQZ.InStr = djbh;
                    queryFormQZ.Code = machine_Code;

                    queryFormQZ.StartPosition = FormStartPosition.CenterParent;

                    queryFormQZ.ShowDialog();

                    break;

                case "ZJ":

                    LY_GetMachinePrice queryFormZJ = new LY_GetMachinePrice();

                    queryFormZJ.InStrId = storeInId;
                    queryFormZJ.StartPosition = FormStartPosition.CenterParent;

                    queryFormZJ.ShowDialog();

                    break;

                case "JY":

                    LY_OutSourcePrice queryFormJY = new LY_OutSourcePrice();

                    queryFormJY.InStrId = storeInId;

                    queryFormJY.StartPosition = FormStartPosition.CenterParent;
                    queryFormJY.ShowDialog();

                    break;


                default:
                    //Console.WriteLine("Default case");
                    break;
            }
        }

        //private void fillToolStripButton_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        this.ly_Settlement_In_FinishGood_SumTableAdapter.Fill(this.lYFinancialMange.ly_Settlement_In_FinishGood_Sum, monthly_settlement_numToolStripTextBox.Text);
        //    }
        //    catch (System.Exception ex)
        //    {
        //        System.Windows.Forms.MessageBox.Show(ex.Message);
        //    }

        //}

        private void toolStripButton142_Click(object sender, EventArgs e)
        {

            if (null == this.ly_financial_monthly_settlement_mainDataGridView.CurrentRow) return;



            string settlementNum = this.ly_financial_monthly_settlement_mainDataGridView.CurrentRow.Cells["月结编码"].Value.ToString();

            this.ly_Settlement_In_FinishGood_SumTableAdapter.Fill(this.lYFinancialMange.ly_Settlement_In_FinishGood_Sum, settlementNum);
        }

        private void toolStripButton139_Click(object sender, EventArgs e)
        {
            ExportDataGridviewTOExcell.ExportDataGridview(this.ly_Settlement_In_FinishGood_SumDataGridView, true);
        }

        private void toolStripButton147_Click(object sender, EventArgs e)
        {
            if (null == this.ly_inma0010DataGridView.CurrentRow) return;



            string nowitemno = ly_inma0010DataGridView.CurrentRow.Cells["物资编号"].Value.ToString();



            NewFrm.Show(this.ParentForm);
            Thread.Sleep(100);

            this.ly_store_in_aettlementTableAdapter.Fill(this.genQuey.ly_store_in_aettlement, nowitemno, this.dateTimePicker6.Value.Date.AddDays(1));
            this.ly_store_out_aettlementTableAdapter.Fill(this.genQuey.ly_store_out_aettlement, nowitemno, this.dateTimePicker6.Value.Date.AddDays(1));

            

            NewFrm.Hide(this.ParentForm);
        }

        private void toolStripButton116_Click(object sender, EventArgs e)
        {
            ExportDataGridviewTOExcell.ExportDataGridview(this.ly_get_wip_storeout_periodDataGridView, true);
        }

        private void toolStripButton122_Click(object sender, EventArgs e)
        {
            
                ExportDataGridviewTOExcell.ExportDataGridview(this.ly_get_wip_storein_periodDataGridView, true);
        }

        private void toolStripButton110_Click(object sender, EventArgs e)
        {
            
                ExportDataGridviewTOExcell.ExportDataGridview(this.ly_store_inout_wip_periodDataGridView, true);
        }

        private void toolStripButton67_Click(object sender, EventArgs e)
        {
            
                ExportDataGridviewTOExcell.ExportDataGridview(this.dataGridView6, true);
        }

        private void toolStripButton73_Click(object sender, EventArgs e)
        {
            
                  ExportDataGridviewTOExcell.ExportDataGridview(this.get_SalesPrice_ByApprove_Fin_month_detailDataGridView, true);
        }

        //private void fillToolStripButton_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        this.ly_store_in_aettlementTableAdapter.Fill(this.genQuey.ly_store_in_aettlement, wzbhToolStripTextBox.Text, new System.Nullable<System.DateTime>(((System.DateTime)(System.Convert.ChangeType(enddateToolStripTextBox.Text, typeof(System.DateTime))))));

        //    }
        //    catch (System.Exception ex)
        //    {
        //        System.Windows.Forms.MessageBox.Show(ex.Message);
        //    }

        //}

        //private void fillToolStripButton_Click_1(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        this.ly_store_in_aettlementTableAdapter.Fill(this.genQuey.ly_store_in_aettlement,
        //        this.ly_store_out_aettlementTableAdapter.Fill(this.genQuey.ly_store_out_aettlement, wzbhToolStripTextBox.Text, new System.Nullable<System.DateTime>(((System.DateTime)(System.Convert.ChangeType(out_dateToolStripTextBox.Text, typeof(System.DateTime))))));
        //    }
        //    catch (System.Exception ex)
        //    {
        //        System.Windows.Forms.MessageBox.Show(ex.Message);
        //    }

        //}


        //private void fillToolStripButton_Click_1(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        this.ly_get_wip_material_periodTableAdapter.Fill(this.lYFinancialMange.ly_get_wip_material_period, new System.Nullable<System.DateTime>(((System.DateTime)(System.Convert.ChangeType(begindateToolStripTextBox.Text, typeof(System.DateTime))))), new System.Nullable<System.DateTime>(((System.DateTime)(System.Convert.ChangeType(enddateToolStripTextBox.Text, typeof(System.DateTime))))));
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
        //        this.ly_store_inout_wip_periodTableAdapter.Fill(this.lYFinancialMange.ly_store_inout_wip_period, new System.Nullable<System.DateTime>(((System.DateTime)(System.Convert.ChangeType(begindateToolStripTextBox.Text, typeof(System.DateTime))))), new System.Nullable<System.DateTime>(((System.DateTime)(System.Convert.ChangeType(enddateToolStripTextBox.Text, typeof(System.DateTime))))));
        //    }
        //    catch (System.Exception ex)
        //    {
        //        System.Windows.Forms.MessageBox.Show(ex.Message);
        //    }

        //}

        //private void fillToolStripButton_Click_1(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        this.ly_get_wip_storeout_periodTableAdapter.Fill(this.lYFinancialMange.ly_get_wip_storeout_period, new System.Nullable<System.DateTime>(((System.DateTime)(System.Convert.ChangeType(begindateToolStripTextBox.Text, typeof(System.DateTime))))), new System.Nullable<System.DateTime>(((System.DateTime)(System.Convert.ChangeType(enddateToolStripTextBox.Text, typeof(System.DateTime))))));
        //    }
        //    catch (System.Exception ex)
        //    {
        //        System.Windows.Forms.MessageBox.Show(ex.Message);
        //    }

        //}

        //private void fillToolStripButton_Click_2(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        this.ly_get_wip_storein_periodTableAdapter.Fill(this.lYFinancialMange.ly_get_wip_storein_period, new System.Nullable<System.DateTime>(((System.DateTime)(System.Convert.ChangeType(begindateToolStripTextBox.Text, typeof(System.DateTime))))), new System.Nullable<System.DateTime>(((System.DateTime)(System.Convert.ChangeType(enddateToolStripTextBox.Text, typeof(System.DateTime))))));
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
        //        this.ly_get_wip_detail_finTableAdapter.Fill(this.lYStoreMange.ly_get_wip_detail_fin, new System.Nullable<System.DateTime>(((System.DateTime)(System.Convert.ChangeType(begindateToolStripTextBox.Text, typeof(System.DateTime))))), new System.Nullable<System.DateTime>(((System.DateTime)(System.Convert.ChangeType(enddateToolStripTextBox.Text, typeof(System.DateTime))))));
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
        //        this.f_Item_dynamicpriceB_pacTableAdapter.Fill(this.lYStoreMange.f_Item_dynamicpriceB_pac, itemnoToolStripTextBox.Text);
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
        //        this.ly_get_wip_finTableAdapter.Fill(this.lYStoreMange.ly_get_wip_fin, new System.Nullable<System.DateTime>(((System.DateTime)(System.Convert.ChangeType(begindateToolStripTextBox.Text, typeof(System.DateTime))))), new System.Nullable<System.DateTime>(((System.DateTime)(System.Convert.ChangeType(enddateToolStripTextBox.Text, typeof(System.DateTime))))));
        //    }
        //    catch (System.Exception ex)
        //    {
        //        System.Windows.Forms.MessageBox.Show(ex.Message);
        //    }

        //}
    }
}
