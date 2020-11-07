using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Transactions;
using System.Windows.Forms;
using DataGridFilter;
using HappyYF.Infrastructure.Repositories;

namespace HappyYF.YuanXin.WorkSet
{
    public partial class LY_Machine : Form
    {

        string formState = "View";
        Point pt = new Point();

        public LY_Machine()
        {
            InitializeComponent();
        }

        private void LY_Machine_Load(object sender, EventArgs e)
        {
            this.notifyIcon1.Visible = false;


            //定时函数
            //this.timer1.Interval = 5000;
            //this.timer1.Start();

            toolStripButton4.Text = "显示计划界面";
            this.splitContainer1.Panel1Collapsed = true;

            //this.order_instore_detailTableAdapter.Fill(this.lYMaterielRequirements.order_instore_detail, 
            this.order_instore_detailTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;

            this.ly_machinepart_process_workTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            this.ly_production_orderTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            this.ly_production_order_detailTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;

            this.ly_production_order_detail1TableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            this.ly_production_order_materialrequisitionTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            this.ly_production_order_inspectionTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;


            this.ly_material_plan_mainTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            this.ly_material_plan_mainTableAdapter.Fill(this.lYMaterielRequirements.ly_material_plan_main, "SCJH");

            this.lY_MaterielRequirementsExecuteTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;

            SetFormState("View");

        }

        private void SetFormState(string state)
        {
            // view edit add save cancle

            //return;

            if ("View" == state)
            {
                this.formState = "View";





                this.bindingNavigatorMoveFirstItem.Enabled = true;
                this.bindingNavigatorMoveLastItem.Enabled = true;
                this.bindingNavigatorMoveNextItem.Enabled = true;
                this.bindingNavigatorMovePreviousItem.Enabled = true;
                this.bindingNavigatorPositionItem.Enabled = true;





            }
            else
            {




                this.bindingNavigatorMoveFirstItem.Enabled = false;
                this.bindingNavigatorMoveLastItem.Enabled = false;
                this.bindingNavigatorMoveNextItem.Enabled = false;
                this.bindingNavigatorMovePreviousItem.Enabled = false;
                this.bindingNavigatorPositionItem.Enabled = false;




            }


        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            this.ly_material_plan_mainTableAdapter.Fill(this.lYMaterielRequirements.ly_material_plan_main, "SCJH");

            SetFormState("View");
        }



        private void ly_material_plan_mainDataGridView_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (this.formState == "View")
            {

                if (null != this.ly_material_plan_mainDataGridView.CurrentRow)
                {
                    int parentId = int.Parse(this.ly_material_plan_mainDataGridView.Rows[e.RowIndex].Cells["parentid"].Value.ToString());
                    string planNum = this.ly_material_plan_mainDataGridView.Rows[e.RowIndex].Cells["计划编号"].Value.ToString();

                    //NewFrm.Show(this.ParentForm);
                    this.lY_MaterielRequirementsExecuteTableAdapter.Fill(this.lYMaterielRequirements.LY_MaterielRequirementsExecute, parentId, "机加");
                    //NewFrm.Hide(this.ParentForm);
                }
            }



        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            int parentId = int.Parse(this.ly_material_plan_mainDataGridView.CurrentRow.Cells["parentid"].Value.ToString());

            //NewFrm.Show(this.ParentForm);
            this.lY_MaterielRequirementsExecuteTableAdapter.Fill(this.lYMaterielRequirements.LY_MaterielRequirementsExecute, parentId, "机加");
            //NewFrm.Hide(this.ParentForm);
        }

        private string GetMaxProductionorder()
        {
            SqlConnection sqlConnection1 = new SqlConnection(SQLDatabase.Connectstring);
            SqlCommand cmd = new SqlCommand();

            string MaxProductionorder = "";

            cmd.Parameters.Add("@Production_mode", SqlDbType.VarChar);
            cmd.Parameters["@Production_mode"].Value = "JG";


            cmd.CommandText = "LY_GetMax_Productionorder";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = sqlConnection1;

            sqlConnection1.Open();
            MaxProductionorder = cmd.ExecuteScalar().ToString();
            sqlConnection1.Close();



            return MaxProductionorder;
        }



        private void bindingNavigatorAddNewItem_Click(object sender, EventArgs e)
        {
            if (null == lY_MaterielRequirementsDataGridView.CurrentRow) return;

            string message = "增加加工跟单吗？";
            string caption = "提示...";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;

            DialogResult result;



            result = MessageBox.Show(message, caption, buttons,
            MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

            if (result == DialogResult.Yes)
            {
                this.ly_production_orderBindingSource.AddNew();
                this.ly_production_orderDataGridView.CurrentRow.Cells["跟单编号"].Value = GetMaxProductionorder();

                this.ly_production_orderDataGridView.CurrentRow.Cells["下单日期"].Value = SQLDatabase.GetNowdate().ToString();  

                this.ly_production_orderDataGridView.CurrentRow.Cells["调度"].Value = SQLDatabase.nowUserName();

                int parentId = int.Parse(this.ly_material_plan_mainDataGridView.CurrentRow.Cells["parentid"].Value.ToString());
                this.ly_production_orderDataGridView.CurrentRow.Cells["plan_id_order"].Value = parentId;

                string nowitem = this.lY_MaterielRequirementsDataGridView.CurrentRow.Cells["物料编码1"].Value.ToString();
                this.ly_production_orderDataGridView.CurrentRow.Cells["物料编码"].Value = nowitem;

                string nowcount = this.lY_MaterielRequirementsDataGridView.CurrentRow.Cells["未排跟单"].Value.ToString();
                this.ly_production_orderDataGridView.CurrentRow.Cells["加工数量"].Value = nowcount;

                string financialunit_price = this.lY_MaterielRequirementsDataGridView.CurrentRow.Cells["financial_unit_price"].Value.ToString();
                this.ly_production_orderDataGridView.CurrentRow.Cells["物料单价"].Value = financialunit_price;

                this.ly_production_orderDataGridView.CurrentRow.Cells["审批2"].Value = "True";
                this.ly_production_orderDataGridView.CurrentRow.Cells["审批人2"].Value = "系统默认";
                this.ly_production_orderDataGridView.CurrentRow.Cells["审批日期2"].Value = SQLDatabase.GetNowdate().ToString();

                SaveChanged();

                //this.ly_production_orderDataGridView.EndEdit();

                //this.Validate();
                //this.ly_production_orderBindingSource.EndEdit();



                //this.ly_production_orderTableAdapter.Update(this.lYMaterielRequirements.ly_production_order);

                //this.ly_production_orderTableAdapter.Fill(this.lYMaterielRequirements.ly_production_order, parentId, nowitem);

                //this.lY_MaterielRequirementsExecuteTableAdapter.Fill(this.lYMaterielRequirements.LY_MaterielRequirementsExecute, parentId, "机加");

                //SetFormState("Edit");

                //this.制定日期DateTimePicker.Focus();



            }
        }

        private void lY_MaterielRequirementsDataGridView_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (this.formState == "View")
            {

                if (null != this.lY_MaterielRequirementsDataGridView.CurrentRow)
                {
                    int parentId = int.Parse(this.ly_material_plan_mainDataGridView.CurrentRow.Cells["parentid"].Value.ToString());
                    string nowitem = this.lY_MaterielRequirementsDataGridView.Rows[e.RowIndex].Cells["物料编码1"].Value.ToString();

                    //NewFrm.Show(this.ParentForm);
                    this.ly_production_orderTableAdapter.Fill(this.lYMaterielRequirements.ly_production_order, parentId, nowitem);
                    //this.ly_machinepart_processTableAdapter.Fill(this.lYMaterielRequirements.ly_machinepart_process, nowitem);
                    this.order_instore_detailTableAdapter.Fill(this.lYMaterielRequirements.order_instore_detail, parentId, nowitem);

                    //NewFrm.Hide(this.ParentForm);
                }
            }

        }

        private void bindingNavigatorDeleteItem_Click(object sender, EventArgs e)
        {
            if (null == this.ly_production_orderDataGridView.CurrentRow) return;

            //if (this.ly_material_plan_detailDataGridView.RowCount > 0)
            //{
            //    MessageBox.Show("计划已有物料记录，不能删除(实需删除，请先删除该计划的物料记录)", "注意");
            //    return;

            //}

            string diaodu = this.ly_production_orderDataGridView.CurrentRow.Cells["调度"].Value.ToString();

            if (diaodu != SQLDatabase.nowUserName())
            {
                MessageBox.Show("请调度:" + diaodu + "删除", "注意");
                return;
            }

            //2018-4-4 
            if (this.ly_production_order_materialrequisitionDataGridView.RowCount > 0)
            {

                decimal hadget = 0;
                foreach (DataGridViewRow dgr in ly_production_order_materialrequisitionDataGridView.Rows)
                {

                    if (string.IsNullOrEmpty(dgr.Cells["已领5"].Value.ToString())) continue;
                    hadget = hadget + decimal.Parse(dgr.Cells["已领5"].Value.ToString());
                }
                if (0 < hadget)
                {

                    MessageBox.Show("跟单已有领料记录，不能删除(实需删除，请先删除该跟单的领料记录)", "注意");
                    return;
                }
            }


            if (ly_production_order_detailDataGridView.RowCount > 0)
            {
                MessageBox.Show("跟单已有工序安排,删除所有工序安排后才能删除跟单...", "注意");
                return;

            }

            string nowproductionorder = this.ly_production_orderDataGridView.CurrentRow.Cells["跟单编号"].Value.ToString();


            string message1 = "当前(跟单：" + nowproductionorder + ")将被删除，继续吗？";
            string caption1 = "提示...";
            MessageBoxButtons buttons1 = MessageBoxButtons.YesNo;

            DialogResult result1; 
            result1 = MessageBox.Show(message1, caption1, buttons1,
            MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

            if (result1 == DialogResult.Yes)
            {

                this.ly_production_orderBindingSource.RemoveCurrent(); 

                SaveChanged(); 

            }
        }

        private void SaveChanged()
        {

            this.ly_production_orderDataGridView.EndEdit();

            this.Validate();
            this.ly_production_orderBindingSource.EndEdit();

            try
            {
                this.ly_production_orderTableAdapter.Update(this.lYMaterielRequirements.ly_production_order);
            }
            catch(SqlException  sqle)
            {
                MessageBox.Show(sqle .Message ,"注意");

                return;
            }

            int parentId = int.Parse(this.ly_material_plan_mainDataGridView.CurrentRow.Cells["parentid"].Value.ToString());
            string nowitem = this.lY_MaterielRequirementsDataGridView.CurrentRow.Cells["物料编码1"].Value.ToString();

            this.ly_production_orderTableAdapter.Fill(this.lYMaterielRequirements.ly_production_order, parentId, nowitem);

            string now_require_id = this.lY_MaterielRequirementsDataGridView.CurrentRow.Cells["require_id"].Value.ToString();

            this.lY_MaterielRequirementsExecuteTableAdapter.Fill(this.lYMaterielRequirements.LY_MaterielRequirementsExecute, parentId, "机加");

            this.lY_MaterielRequirementsExecuteBindingSource.Position = this.lY_MaterielRequirementsExecuteBindingSource.Find("id", now_require_id);
        }

        private void ly_production_orderDataGridView_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (null == this.ly_production_orderDataGridView.CurrentRow) return;

            DataGridView dgv = sender as DataGridView;




            if ("加工数量" == dgv.CurrentCell.OwningColumn.Name)
            {

                ChangeValue queryForm = new ChangeValue();

                queryForm.OldValue = dgv.CurrentCell.Value.ToString();
                queryForm.NewValue = "";
                queryForm.ChangeMode = "value";
                queryForm.ShowDialog();




                if (queryForm.NewValue != "")
                {


                    dgv.CurrentRow.Cells["加工数量"].Value = queryForm.NewValue;

                    int noworderValue = 0;

                    int nowplanValue = int.Parse(this.lY_MaterielRequirementsDataGridView.CurrentRow.Cells["需求数量"].Value.ToString(), System.Globalization.NumberStyles.Number);


                    foreach (DataGridViewRow dgr in dgv.Rows)
                    {

                        if (string.IsNullOrEmpty(dgr.Cells["加工数量"].Value.ToString())) continue;
                        noworderValue = noworderValue + int.Parse(dgr.Cells["加工数量"].Value.ToString(), System.Globalization.NumberStyles.Number);



                    }

                    if (noworderValue > nowplanValue * 1)
                    {
                        dgv.CurrentRow.Cells["加工数量"].Value = queryForm.OldValue;
                        MessageBox.Show("跟单安排不能超过计划数量的 120%,操作取消...", "注意");
                        return;

                    }


                    dgv.CurrentRow.Cells["加工数量"].Value = queryForm.NewValue;
                    //dgv.CurrentRow.Cells["discount_money"].Value = DBNull.Value;

                    //dgv.CurrentRow.Cells["approve_flag"].Value = "False";
                    SaveChanged();


                    //CountPlanStru();

                }
                else
                {
                    //hT_Manage_ItemDataGridView.CurrentRow.Cells["apply_money"].Value = queryForm.NewValue;
                    //dgv.CurrentRow.Cells["discount_money"].Value = DBNull.Value;
                    //dgv.CurrentRow.Cells["apply_money"].Value = DBNull.Value;
                    //dgv.CurrentRow.Cells["approve_flag"].Value = "False";
                    //SaveChanged();

                }
                return;

            }


            ///////////////////////////////////////////////////////

            if ("下单日期" == dgv.CurrentCell.OwningColumn.Name)
            {


                DatePicker queryForm = new DatePicker();
                queryForm.Pt = pt;

                if (null != (dgv.CurrentCell.Value))
                    queryForm.NowDate = dgv.CurrentCell.Value.ToString();

                queryForm.ShowDialog();



                if (null != queryForm.NowDate)
                {

                    dgv.CurrentRow.Cells["下单日期"].Value = queryForm.NowDate;
                    SaveChanged();

                }
                return;
            }









            ///////////////////////////////////////////////////////

            if ("备注" == dgv.CurrentCell.OwningColumn.Name)
            {

                ChangeValue queryForm = new ChangeValue();

                queryForm.OldValue = dgv.CurrentCell.Value.ToString();
                queryForm.NewValue = "";
                //queryForm.ChangeMode = "value";
                queryForm.ShowDialog();




                if (queryForm.NewValue != "")
                {
                    dgv.CurrentRow.Cells["备注"].Value = queryForm.NewValue;
                    //dgv.CurrentRow.Cells["discount_money"].Value = DBNull.Value;

                    //dgv.CurrentRow.Cells["approve_flag"].Value = "False";
                    SaveChanged();


                    //CountPlanStru();

                }
                else
                {

                }
                return;

            }

            /////////////////////////////

            if ("完成" == dgv.CurrentCell.OwningColumn.Name)
            {
                if (!SQLDatabase.CheckHaveRight(SQLDatabase.NowUserID, "跟单完成设置"))
                {
                    MessageBox.Show("无跟单完成设置权限,操作取消...", "注意");
                    return;

                }

                if ("True" == dgv.CurrentRow.Cells["完成"].Value.ToString())
                {
                    dgv.CurrentRow.Cells["完成"].Value = "False";

                }
                else
                {

                    dgv.CurrentRow.Cells["完成"].Value = "True";
                }



                SaveChanged();





                return;
            }


            ///////////////////////////////////////////////////////


        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            if ("隐藏计划界面" == toolStripButton4.Text)
            {
                toolStripButton4.Text = "显示计划界面";
                this.splitContainer1.Panel1Collapsed = true;
            }
            else
            {
                toolStripButton4.Text = "隐藏计划界面";
                this.splitContainer1.Panel1Collapsed = false;
            }
        }

        private void ly_production_orderDataGridView_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            //if (this.formState == "View")
            //{

            //    if (null != this.ly_production_orderDataGridView.CurrentRow)
            //    {

            //        string nowitem = this.ly_production_orderDataGridView.Rows[e.RowIndex].Cells["物料编码"].Value.ToString();
            //        string nowproductionorderNum = this.ly_production_orderDataGridView.Rows[e.RowIndex].Cells["跟单编号"].Value.ToString();

            //        this.ly_machinepart_process_workTableAdapter.Fill(this.lYMaterielRequirements.ly_machinepart_process_work, nowitem, nowproductionorderNum);


            //    }
            //}
        }

        private void ly_production_orderDataGridView_SelectionChanged(object sender, EventArgs e)
        {
            if (this.formState == "View")
            {

                if (null != this.ly_production_orderDataGridView.CurrentRow)
                {

                    string nowitem = this.ly_production_orderDataGridView.CurrentRow.Cells["物料编码"].Value.ToString();
                    string nowproductionorderNum = this.ly_production_orderDataGridView.CurrentRow.Cells["跟单编号"].Value.ToString();

                    this.ly_machinepart_process_workTableAdapter.Fill(this.lYMaterielRequirements.ly_machinepart_process_work, nowitem, nowproductionorderNum);

                    this.ly_production_order_detail1TableAdapter.Fill(this.lYMaterielRequirements.ly_production_order_detail1, nowproductionorderNum, 0);

                    this.ly_production_order_materialrequisitionTableAdapter.Fill(this.lYMaterielRequirements.ly_production_order_materialrequisition, nowproductionorderNum);
                    //set_processOrder_Num();


                }
                else
                {
                    this.ly_machinepart_process_workTableAdapter.Fill(this.lYMaterielRequirements.ly_machinepart_process_work, "", "");

                    this.ly_production_order_detail1TableAdapter.Fill(this.lYMaterielRequirements.ly_production_order_detail1, "", 0);
                    this.ly_production_order_materialrequisitionTableAdapter.Fill(this.lYMaterielRequirements.ly_production_order_materialrequisition, "");

                }
            }
        }

        private void set_processOrder_Num()
        {
            int processOrder;



            if (null == this.ly_production_orderDataGridView.CurrentRow) return;

            foreach (DataGridViewRow dgr in ly_machinepart_process_workDataGridView.Rows)
            {

                if ("" == dgr.Cells["工序编号"].Value.ToString()) continue;
                processOrder = int.Parse(dgr.Cells["工序编号"].Value.ToString());


                if (1 == processOrder)
                {
                    dgr.Cells["本序数量"].Value = dgr.Cells["跟单数量"].Value;
                    //跟单数量
                }
                else
                {


                    dgr.Cells["本序数量"].Value = ly_machinepart_process_workDataGridView.Rows[processOrder - 1].Cells["本序合格"].Value;

                }

            }

            //for (int i = 1; i <= 5; i++)


        }

        private void ly_machinepart_process_workDataGridView_SelectionChanged(object sender, EventArgs e)
        {
            if (null != this.ly_production_orderDataGridView.CurrentRow && null != this.ly_machinepart_process_workDataGridView.CurrentRow)
            {

                int nowOrder;
                string nowproductionorderNum = this.ly_production_orderDataGridView.CurrentRow.Cells["跟单编号"].Value.ToString();

                if ("" != this.ly_machinepart_process_workDataGridView.CurrentRow.Cells["工序编号"].Value.ToString())
                {
                    nowOrder = int.Parse(this.ly_machinepart_process_workDataGridView.CurrentRow.Cells["工序编号"].Value.ToString());
                }
                else
                {
                    nowOrder = 0;
                }



                this.ly_production_order_detailTableAdapter.Fill(this.lYMaterielRequirements.ly_production_order_detail, nowproductionorderNum, nowOrder);


            }
            else
            {
                this.ly_production_order_detailTableAdapter.Fill(this.lYMaterielRequirements.ly_production_order_detail, "", 0);
            }
        }

        private void 任务分发ToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (ly_production_orderDataGridView.CurrentRow==null)
            {
                return;

            }
            if ("False"==this.ly_production_orderDataGridView.CurrentRow.Cells["审批2"].Value.ToString())
            {
                MessageBox.Show("请联系领导审批...", "注意");
                return;
            }

            string nowproductionorder = this.ly_production_orderDataGridView.CurrentRow.Cells["跟单编号"].Value.ToString();
            string noworder = this.ly_machinepart_process_workDataGridView.CurrentRow.Cells["工序名称"].Value.ToString();




            string message1 = "确认分配(跟单：" + nowproductionorder + " 工序:" + noworder + ")任务吗?";
            string caption1 = "提示...";
            MessageBoxButtons buttons1 = MessageBoxButtons.YesNo;

            DialogResult result1;



            result1 = MessageBox.Show(message1, caption1, buttons1,
            MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

            if (result1 == DialogResult.Yes)
            {

                ///////////////////////////////////////////////////////////////// 

                string sel = "SELECT  work_code as 工号, worker_name as 姓名 FROM ly_worker_list where prodcode='04'";


                QueryForm queryForm = new QueryForm();


                queryForm.Sel = sel;
                queryForm.Constr = SQLDatabase.Connectstring;



                queryForm.ShowDialog();



                if (string.IsNullOrEmpty(queryForm.Result))
                {
                    MessageBox.Show("必须选择人员,才能安排跟单工序加工...", "注意");
                    return;
                }


                /////////////////////////////////////////////////////////////////

                decimal up_order_count;
                decimal order_count;

                decimal up_quality;
                decimal up_canuse;
                decimal up_remake;

                int nowrowIndex = this.ly_machinepart_process_workDataGridView.CurrentRow.Index;
                string nowordername = this.ly_machinepart_process_workDataGridView.CurrentRow.Cells["工序名称"].Value.ToString();

                int nowordernum = int.Parse(this.ly_machinepart_process_workDataGridView.CurrentRow.Cells["工序编号"].Value.ToString());


                if (1 != nowordernum)
                //if ("下料" != nowordername)
                {
                    if (!string.IsNullOrEmpty(this.ly_machinepart_process_workDataGridView.Rows[nowrowIndex - 1].Cells["本序合格"].Value.ToString()))
                    {


                        up_quality = decimal.Parse(this.ly_machinepart_process_workDataGridView.Rows[nowrowIndex - 1].Cells["本序合格"].Value.ToString());
                    }
                    else
                    {

                        up_quality = 0;
                    }

                    if (!string.IsNullOrEmpty(this.ly_machinepart_process_workDataGridView.Rows[nowrowIndex - 1].Cells["本序可用数量"].Value.ToString()))
                    {


                        up_canuse = decimal.Parse(this.ly_machinepart_process_workDataGridView.Rows[nowrowIndex - 1].Cells["本序可用数量"].Value.ToString());
                    }
                    else
                    {

                        up_canuse = 0;
                    }

                    ////////////////////////

                    if (!string.IsNullOrEmpty(this.ly_machinepart_process_workDataGridView.Rows[nowrowIndex - 1].Cells["本序返修合格"].Value.ToString()))
                    {


                        up_remake = decimal.Parse(this.ly_machinepart_process_workDataGridView.Rows[nowrowIndex - 1].Cells["本序返修合格"].Value.ToString());
                    }
                    else
                    {

                        up_remake = 0;
                    }


                    up_order_count = up_quality + up_canuse + up_remake;


                    //if (!string.IsNullOrEmpty(this.ly_machinepart_process_workDataGridView.Rows[nowrowIndex - 1].Cells["本序数量"].Value.ToString()))
                    //{


                    //    up_order_count = decimal.Parse(this.ly_machinepart_process_workDataGridView.Rows[nowrowIndex - 1].Cells["本序数量"].Value.ToString());
                    //}
                    //else
                    //{

                    //    up_order_count = 0;
                    //}
                }
                else
                {
                    if (!string.IsNullOrEmpty(this.ly_machinepart_process_workDataGridView.CurrentRow.Cells["跟单数量"].Value.ToString()))
                    {
                        up_order_count = decimal.Parse(this.ly_machinepart_process_workDataGridView.CurrentRow.Cells["跟单数量"].Value.ToString());
                    }
                    else
                    {
                        up_order_count = 0;
                    }

                }


                if (!string.IsNullOrEmpty(this.ly_machinepart_process_workDataGridView.CurrentRow.Cells["本序数量"].Value.ToString()))
                {
                    order_count = decimal.Parse(this.ly_machinepart_process_workDataGridView.CurrentRow.Cells["本序数量"].Value.ToString());
                }
                else
                {
                    order_count = 0;
                }



                decimal arrange_count = 0;

                if (null != this.ly_production_orderDataGridView.CurrentRow)
                {

                    foreach (DataGridViewRow dgr in ly_production_order_detailDataGridView.Rows)
                    {

                        if (string.IsNullOrEmpty(dgr.Cells["数量1"].Value.ToString())) continue;
                        arrange_count = arrange_count + decimal.Parse(dgr.Cells["数量1"].Value.ToString());



                    }
                }



                if (arrange_count >= up_order_count)
                {
                    MessageBox.Show("跟单数量已经全部安排,不能增加跟单任务", "注意");

                    return;
                }
                else
                {



                    ///////////////////////////////////////////////////////////////////








                    this.ly_production_order_detailBindingSource.AddNew();

                    this.ly_production_order_detailDataGridView.CurrentRow.Cells["跟单编号1"].Value = this.ly_production_orderDataGridView.CurrentRow.Cells["跟单编号"].Value.ToString();

                    this.ly_production_order_detailDataGridView.CurrentRow.Cells["工序1"].Value = this.ly_machinepart_process_workDataGridView.CurrentRow.Cells["工序编号"].Value;

                    this.ly_production_order_detailDataGridView.CurrentRow.Cells["sequence_number1"].Value = this.ly_machinepart_process_workDataGridView.CurrentRow.Cells["sequence_number"].Value;


                    this.ly_production_order_detailDataGridView.CurrentRow.Cells["pruductionOrder_count1"].Value = this.ly_machinepart_process_workDataGridView.CurrentRow.Cells["跟单数量"].Value.ToString();

                    this.ly_production_order_detailDataGridView.CurrentRow.Cells["准终1"].Value = this.ly_machinepart_process_workDataGridView.CurrentRow.Cells["准终"].Value;

                    this.ly_production_order_detailDataGridView.CurrentRow.Cells["实际准终1"].Value = this.ly_machinepart_process_workDataGridView.CurrentRow.Cells["准终"].Value;

                    this.ly_production_order_detailDataGridView.CurrentRow.Cells["单件工时1"].Value = this.ly_machinepart_process_workDataGridView.CurrentRow.Cells["单件工时"].Value;
                    this.ly_production_order_detailDataGridView.CurrentRow.Cells["实际单件工时1"].Value = this.ly_machinepart_process_workDataGridView.CurrentRow.Cells["单件工时"].Value;

                    this.ly_production_order_detailDataGridView.CurrentRow.Cells["准终累加1"].Value = this.ly_machinepart_process_workDataGridView.CurrentRow.Cells["准终累加"].Value;
                    this.ly_production_order_detailDataGridView.CurrentRow.Cells["单件工时累加1"].Value = this.ly_machinepart_process_workDataGridView.CurrentRow.Cells["工时累加"].Value;

                    this.ly_production_order_detailDataGridView.CurrentRow.Cells["工时单价1"].Value = this.ly_machinepart_process_workDataGridView.CurrentRow.Cells["工时单价"].Value;
                    this.ly_production_order_detailDataGridView.CurrentRow.Cells["材料单价1"].Value = this.ly_production_orderDataGridView.CurrentRow.Cells["物料单价"].Value;


                    this.ly_production_order_detailDataGridView.CurrentRow.Cells["数量1"].Value = up_order_count - arrange_count;

                    this.ly_production_order_detailDataGridView.CurrentRow.Cells["调度1"].Value = SQLDatabase.nowUserName();

                    this.ly_production_order_detailDataGridView.CurrentRow.Cells["排序日期"].Value = SQLDatabase.GetNowdate();
                    this.ly_production_order_detailDataGridView.CurrentRow.Cells["外协"].Value = "False";

                    this.ly_production_order_detailDataGridView.CurrentRow.Cells["工号"].Value = queryForm.Result;
                    this.ly_production_order_detailDataGridView.CurrentRow.Cells["姓名1"].Value = queryForm.Result1;

                    SaveTask();
                }

            }
        }

        private void SaveTask()
        {

            this.ly_production_order_detailDataGridView.EndEdit();

            this.Validate();
            this.ly_production_order_detailBindingSource.EndEdit();
            try
            {
                this.ly_production_order_detailTableAdapter.Update(this.lYMaterielRequirements.ly_production_order_detail);
            }
            catch (SqlException sqle)
            {
                MessageBox.Show(sqle .Message, "注意");
                return;
            }
            ////////////////////////////////////////////////

            if (null != this.ly_production_orderDataGridView.CurrentRow && null != this.ly_machinepart_process_workDataGridView.CurrentRow)
            {
                int detail_Id;

                if (null != this.ly_production_order_detailDataGridView.CurrentRow)
                {
                    detail_Id = int.Parse(this.ly_production_order_detailDataGridView.CurrentRow.Cells["detailId"].Value.ToString());
                }
                else
                {
                    detail_Id = 0;
                }


                int nowOrder;
                string nowproductionorderNum = this.ly_production_orderDataGridView.CurrentRow.Cells["跟单编号"].Value.ToString();

                if ("" != this.ly_machinepart_process_workDataGridView.CurrentRow.Cells["工序编号"].Value.ToString())
                {
                    nowOrder = int.Parse(this.ly_machinepart_process_workDataGridView.CurrentRow.Cells["工序编号"].Value.ToString());
                }
                else
                {
                    nowOrder = 0;
                }
                ////////////////////////////////////////////////////////////


                string nowitem = this.ly_production_orderDataGridView.CurrentRow.Cells["物料编码"].Value.ToString();


                this.ly_machinepart_process_workTableAdapter.Fill(this.lYMaterielRequirements.ly_machinepart_process_work, nowitem, nowproductionorderNum);

                this.ly_machinepart_process_workBindingSource.Position = this.ly_machinepart_process_workBindingSource.Find("工序编号", nowOrder);
                this.ly_production_order_detailBindingSource.Position = this.ly_production_order_detailBindingSource.Find("id", detail_Id);



                //this.ly_production_order_detailTableAdapter.Fill(this.lYMaterielRequirements.ly_production_order_detail, nowproductionorderNum, nowOrder);
                /////////////////////////////////////////////////////

            }
            else
            {
                this.ly_production_order_detailTableAdapter.Fill(this.lYMaterielRequirements.ly_production_order_detail, "", 0);
            }

        }

        private bool checkqualityRec()
        {
            decimal qualitied_count = 0;
            decimal waste_count = 0;



            if (!string.IsNullOrEmpty(this.ly_production_order_detailDataGridView.CurrentRow.Cells["合格1"].Value.ToString()))
            {
                qualitied_count = decimal.Parse(this.ly_production_order_detailDataGridView.CurrentRow.Cells["合格1"].Value.ToString());
            }
            else
            {
                qualitied_count = 0;
            }

            if (!string.IsNullOrEmpty(this.ly_production_order_detailDataGridView.CurrentRow.Cells["废品1"].Value.ToString()))
            {
                waste_count = decimal.Parse(this.ly_production_order_detailDataGridView.CurrentRow.Cells["废品1"].Value.ToString());
            }
            else
            {
                waste_count = 0;
            }


            if ((qualitied_count + waste_count) > 0)
            {
                string nowordername = this.ly_machinepart_process_workDataGridView.CurrentRow.Cells["工序名称"].Value.ToString();

                if ("下料" == nowordername)
                {
                    return false;
                }
                else
                {
                    return false;
                }

            }
            else
            {

                return true;
            }

        }

        private bool checkpriceRec()
        {
            decimal qualitied_count = 0;
            decimal waste_count = 0;



            if (!string.IsNullOrEmpty(this.ly_production_order_detailDataGridView.CurrentRow.Cells["合格1"].Value.ToString()))
            {
                qualitied_count = decimal.Parse(this.ly_production_order_detailDataGridView.CurrentRow.Cells["合格1"].Value.ToString());
            }
            else
            {
                qualitied_count = 0;
            }

            if (!string.IsNullOrEmpty(this.ly_production_order_detailDataGridView.CurrentRow.Cells["废品1"].Value.ToString()))
            {
                waste_count = decimal.Parse(this.ly_production_order_detailDataGridView.CurrentRow.Cells["废品1"].Value.ToString());
            }
            else
            {
                waste_count = 0;
            }


            if ((qualitied_count + waste_count) > 0)
            {
                string nowordername = this.ly_machinepart_process_workDataGridView.CurrentRow.Cells["工序名称"].Value.ToString();

                if ("下料" == nowordername)
                {
                    return false;
                }
                else
                {
                    return false;
                }

            }
            else
            {

                return true;
            }

        }

        private void ly_production_order_detailDataGridView_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            DataGridView dgv = sender as DataGridView;

            if (null == dgv.CurrentCell) return;

            if ("外协单价" == dgv.CurrentCell.OwningColumn.Name)
            {
              
                if ("True" == dgv.CurrentRow.Cells["审批"].Value.ToString())
                   
                {

                    MessageBox.Show("外协单价已经审批,不能修改, 操作取消", "注意");

                    return;

                }


                ChangeValue queryForm = new ChangeValue();

                queryForm.OldValue = dgv.CurrentCell.Value.ToString();
                queryForm.NewValue = "";
                queryForm.ChangeMode = "value";
                queryForm.ShowDialog();




                if (queryForm.NewValue != "")
                {
                    dgv.CurrentRow.Cells["外协单价"].Value = queryForm.NewValue;

                    SaveTask();



                }
                else
                {
                    dgv.CurrentRow.Cells["外协单价"].Value = DBNull.Value; ;

                    SaveTask();
                }
                return;

            }

            ///////////////////////////////////////////////////////

            if (!checkqualityRec() && "系统管理员" != SQLDatabase.nowUserName())
            {

                MessageBox.Show("任务已有检验记录,不能修改, 操作取消", "注意");

                return;

            }


            if ("数量1" == dgv.CurrentCell.OwningColumn.Name)
            {


                if (!checkqualityRec() && "系统管理员" != SQLDatabase.nowUserName())
                {

                    MessageBox.Show("任务已有检验记录,不能修改, 操作取消", "注意");

                    return;

                }


                ChangeValue queryForm = new ChangeValue();

                queryForm.OldValue = dgv.CurrentCell.Value.ToString();
                queryForm.NewValue = "";
                queryForm.ChangeMode = "value";
                queryForm.ShowDialog();




                if (queryForm.NewValue != "")
                {
                    dgv.CurrentRow.Cells["数量1"].Value = queryForm.NewValue;
                    ////////////////////////////////////////////////////////////////////////



                    ///////////////////////////////////////////////////////////////// 

                    decimal up_order_count;


                    decimal up_quality;
                    decimal up_canuse;
                    decimal up_remake;

                    int nowrowIndex = this.ly_machinepart_process_workDataGridView.CurrentRow.Index;
                    string nowordername = this.ly_machinepart_process_workDataGridView.CurrentRow.Cells["工序名称"].Value.ToString();

                    int nowordernum = int.Parse(this.ly_machinepart_process_workDataGridView.CurrentRow.Cells["工序编号"].Value.ToString());


                    if (1 != nowordernum)

                    //if ("下料" != nowordername)
                    {
                        if (!string.IsNullOrEmpty(this.ly_machinepart_process_workDataGridView.Rows[nowrowIndex - 1].Cells["本序合格"].Value.ToString()))
                        {


                            up_quality = decimal.Parse(this.ly_machinepart_process_workDataGridView.Rows[nowrowIndex - 1].Cells["本序合格"].Value.ToString());
                        }
                        else
                        {

                            up_quality = 0;
                        }

                        if (!string.IsNullOrEmpty(this.ly_machinepart_process_workDataGridView.Rows[nowrowIndex - 1].Cells["本序可用数量"].Value.ToString()))
                        {


                            up_canuse = decimal.Parse(this.ly_machinepart_process_workDataGridView.Rows[nowrowIndex - 1].Cells["本序可用数量"].Value.ToString());
                        }
                        else
                        {

                            up_canuse = 0;
                        }

                        ////////////////////////

                        if (!string.IsNullOrEmpty(this.ly_machinepart_process_workDataGridView.Rows[nowrowIndex - 1].Cells["本序返修合格"].Value.ToString()))
                        {


                            up_remake = decimal.Parse(this.ly_machinepart_process_workDataGridView.Rows[nowrowIndex - 1].Cells["本序返修合格"].Value.ToString());
                        }
                        else
                        {

                            up_remake = 0;
                        }


                        up_order_count = up_quality + up_canuse + up_remake;





                        //if (!string.IsNullOrEmpty(this.ly_machinepart_process_workDataGridView.Rows[nowrowIndex - 1].Cells["本序数量"].Value.ToString()))
                        //{


                        //    up_order_count = decimal.Parse(this.ly_machinepart_process_workDataGridView.Rows[nowrowIndex - 1].Cells["本序数量"].Value.ToString());
                        //}
                        //else
                        //{

                        //    up_order_count = 0;
                        //}
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(this.ly_machinepart_process_workDataGridView.CurrentRow.Cells["跟单数量"].Value.ToString()))
                        {
                            up_order_count = decimal.Parse(this.ly_machinepart_process_workDataGridView.CurrentRow.Cells["跟单数量"].Value.ToString());
                        }
                        else
                        {
                            up_order_count = 0;
                        }

                    }


                    //if (!string.IsNullOrEmpty(this.ly_machinepart_process_workDataGridView.CurrentRow.Cells["本序数量"].Value.ToString()))
                    //{
                    //    order_count = decimal.Parse(this.ly_machinepart_process_workDataGridView.CurrentRow.Cells["本序数量"].Value.ToString());
                    //}
                    //else
                    //{
                    //    order_count = 0;
                    //}

                    //////////////////////////////////////////////////////////

                    decimal order_count;


                    if (!string.IsNullOrEmpty(this.ly_machinepart_process_workDataGridView.CurrentRow.Cells["跟单数量"].Value.ToString()))
                    {
                        order_count = decimal.Parse(this.ly_machinepart_process_workDataGridView.CurrentRow.Cells["跟单数量"].Value.ToString());
                    }
                    else
                    {
                        order_count = 0;
                    }



                    decimal arrange_count = 0;

                    if (null != this.ly_production_orderDataGridView.CurrentRow)
                    {

                        foreach (DataGridViewRow dgr in ly_production_order_detailDataGridView.Rows)
                        {

                            if (string.IsNullOrEmpty(dgr.Cells["数量1"].Value.ToString())) continue;
                            arrange_count = arrange_count + decimal.Parse(dgr.Cells["数量1"].Value.ToString());

                        }
                    }


                    if (arrange_count > up_order_count)
                    {

                        dgv.CurrentRow.Cells["数量1"].Value = queryForm.OldValue;

                        MessageBox.Show("任务安排不能超过上序可用数量, 操作取消", "注意");

                        return;

                    }

                    //////////////////////////////////////////////////////////////////////////
                    SaveTask();


                    //CountPlanStru();

                }
                else
                {
                    //hT_Manage_ItemDataGridView.CurrentRow.Cells["apply_money"].Value = queryForm.NewValue;
                    //dgv.CurrentRow.Cells["discount_money"].Value = DBNull.Value;
                    //dgv.CurrentRow.Cells["apply_money"].Value = DBNull.Value;
                    //dgv.CurrentRow.Cells["approve_flag"].Value = "False";
                    //SaveChanged();

                }
                return;

            }


            ///////////////////////////////////////////////////////

            if ("合格1" == dgv.CurrentCell.OwningColumn.Name)
            {


                return;
                string nowordername = this.ly_machinepart_process_workDataGridView.CurrentRow.Cells["工序名称"].Value.ToString();

                if ("下料" != nowordername) return;


                ChangeValue queryForm = new ChangeValue();

                queryForm.OldValue = dgv.CurrentCell.Value.ToString();
                queryForm.NewValue = "";
                queryForm.ChangeMode = "value";
                queryForm.ShowDialog();

                if (queryForm.NewValue != "")
                {
                    dgv.CurrentRow.Cells["合格1"].Value = queryForm.NewValue;

                    //////////////////////////////////////////////////////////////////////////////

                    int detail_Id = int.Parse(dgv.CurrentRow.Cells["detailId"].Value.ToString());

                    //string insstr = " delete ly_material_plan_main  where material_plan_num = '" + nowPlanNumber + "'";

                    string updstr = " update ly_production_order_inspection  " +
                            "  set qualified_count=  " + queryForm.NewValue + " , waste_count=inspect_count-" + queryForm.NewValue
                            + " where  detail_id=" + detail_Id.ToString();


                    SqlConnection sqlConnection1 = new SqlConnection(SQLDatabase.Connectstring);
                    SqlCommand cmd = new SqlCommand();

                    cmd.CommandText = updstr;
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

                        SaveTask();

                    }

                    //////////////////////////////////////////////////////////////////////////////////////



                }
                else
                {


                }
                return;

            }


            ///////////////////////////////////////////////////////

            if ("工号" == dgv.CurrentCell.OwningColumn.Name || "姓名1" == dgv.CurrentCell.OwningColumn.Name)
            {



                if (!checkqualityRec() && "系统管理员" != SQLDatabase.nowUserName())
                {

                    MessageBox.Show("任务已有检验记录,不能修改, 操作取消", "注意");

                    return;

                }

                string outflag = this.ly_production_order_detailDataGridView.CurrentRow.Cells["外协"].Value.ToString();
                string sel;

                if ("True" == outflag)
                {

                    sel = "SELECT  supplier_code as 工号, supplier_name as 姓名 FROM ly_supplier_list where sort_code='4'";
                }
                else
                {

                    sel = "SELECT  work_code as 工号, worker_name as 姓名 FROM ly_worker_list where prodcode='04'";
                }


                QueryForm queryForm = new QueryForm();


                queryForm.Sel = sel;
                queryForm.Constr = SQLDatabase.Connectstring;

                //Set the Column Collection to the filter Table
                //queryForm.SetSourceColumns(this.billMainDataSet.BalanceBill.Columns);

                queryForm.ShowDialog();


                if (queryForm.Result != "")
                {
                    dgv.CurrentRow.Cells["工号"].Value = queryForm.Result;
                    //dgv.CurrentRow.Cells["discount_money"].Value = DBNull.Value;

                    //dgv.CurrentRow.Cells["approve_flag"].Value = "False";
                    SaveTask();


                    //CountPlanStru();

                }
                else
                {
                    //hT_Manage_ItemDataGridView.CurrentRow.Cells["apply_money"].Value = queryForm.NewValue;
                    //dgv.CurrentRow.Cells["discount_money"].Value = DBNull.Value;
                    //dgv.CurrentRow.Cells["apply_money"].Value = DBNull.Value;
                    //dgv.CurrentRow.Cells["approve_flag"].Value = "False";
                    //SaveChanged();

                }
                return;

            }


            ///////////////////////////////////////////////////////

            if ("实际准终1" == dgv.CurrentCell.OwningColumn.Name)
            {

                ChangeValue queryForm = new ChangeValue();

                queryForm.OldValue = dgv.CurrentCell.Value.ToString();
                queryForm.NewValue = "";
                queryForm.ChangeMode = "value";
                queryForm.ShowDialog();




                if (queryForm.NewValue != "")
                {
                    dgv.CurrentRow.Cells["实际准终1"].Value = queryForm.NewValue;
                    //dgv.CurrentRow.Cells["discount_money"].Value = DBNull.Value;

                    //dgv.CurrentRow.Cells["approve_flag"].Value = "False";
                    SaveTask();


                    //CountPlanStru();

                }
                else
                {
                    //hT_Manage_ItemDataGridView.CurrentRow.Cells["apply_money"].Value = queryForm.NewValue;
                    //dgv.CurrentRow.Cells["discount_money"].Value = DBNull.Value;
                    //dgv.CurrentRow.Cells["apply_money"].Value = DBNull.Value;
                    //dgv.CurrentRow.Cells["approve_flag"].Value = "False";
                    //SaveChanged();

                }
                return;

            }


            ///////////////////////////////////////////////////////

            if ("实际加工工时1" == dgv.CurrentCell.OwningColumn.Name)
            {

                ChangeValue queryForm = new ChangeValue();

                queryForm.OldValue = dgv.CurrentCell.Value.ToString();
                queryForm.NewValue = "";
                queryForm.ChangeMode = "value";
                queryForm.ShowDialog();




                if (queryForm.NewValue != "")
                {
                    dgv.CurrentRow.Cells["实际加工工时1"].Value = queryForm.NewValue;
                    //dgv.CurrentRow.Cells["discount_money"].Value = DBNull.Value;

                    //dgv.CurrentRow.Cells["approve_flag"].Value = "False";
                    SaveTask();


                    //CountPlanStru();

                }
                else
                {
                    //hT_Manage_ItemDataGridView.CurrentRow.Cells["apply_money"].Value = queryForm.NewValue;
                    //dgv.CurrentRow.Cells["discount_money"].Value = DBNull.Value;
                    //dgv.CurrentRow.Cells["apply_money"].Value = DBNull.Value;
                    //dgv.CurrentRow.Cells["approve_flag"].Value = "False";
                    //SaveChanged();

                }
                return;

            }
            ///
            /// 

            if ("外协单价" == dgv.CurrentCell.OwningColumn.Name)
            {

                ChangeValue queryForm = new ChangeValue();

                queryForm.OldValue = dgv.CurrentCell.Value.ToString();
                queryForm.NewValue = "";
                queryForm.ChangeMode = "value";
                queryForm.ShowDialog();




                if (queryForm.NewValue != "")
                {
                    dgv.CurrentRow.Cells["外协单价"].Value = queryForm.NewValue;
                    //dgv.CurrentRow.Cells["discount_money"].Value = DBNull.Value;

                    //dgv.CurrentRow.Cells["approve_flag"].Value = "False";
                    SaveTask();


                    //CountPlanStru();

                }
                else
                {
                    //hT_Manage_ItemDataGridView.CurrentRow.Cells["apply_money"].Value = queryForm.NewValue;
                    //dgv.CurrentRow.Cells["discount_money"].Value = DBNull.Value;
                    //dgv.CurrentRow.Cells["apply_money"].Value = DBNull.Value;
                    //dgv.CurrentRow.Cells["approve_flag"].Value = "False";
                    //SaveChanged();

                }
                return;

            }
            ///////////////////////////////////////////////////////

            if ("实际可用工时1" == dgv.CurrentCell.OwningColumn.Name)
            {

                ChangeValue queryForm = new ChangeValue();

                queryForm.OldValue = dgv.CurrentCell.Value.ToString();
                queryForm.NewValue = "";
                queryForm.ChangeMode = "value";
                queryForm.ShowDialog();




                if (queryForm.NewValue != "")
                {
                    dgv.CurrentRow.Cells["实际可用工时1"].Value = queryForm.NewValue;
                    //dgv.CurrentRow.Cells["discount_money"].Value = DBNull.Value;

                    //dgv.CurrentRow.Cells["approve_flag"].Value = "False";
                    SaveTask();


                    //CountPlanStru();

                }
                else
                {
                    //hT_Manage_ItemDataGridView.CurrentRow.Cells["apply_money"].Value = queryForm.NewValue;
                    //dgv.CurrentRow.Cells["discount_money"].Value = DBNull.Value;
                    //dgv.CurrentRow.Cells["apply_money"].Value = DBNull.Value;
                    //dgv.CurrentRow.Cells["approve_flag"].Value = "False";
                    //SaveChanged();

                }
                return;

            }

            //if ("外协单价" == dgv.CurrentCell.OwningColumn.Name)
            //{

            //    ChangeValue queryForm = new ChangeValue();

            //    queryForm.OldValue = dgv.CurrentCell.Value.ToString();
            //    queryForm.NewValue = "";
            //    queryForm.ChangeMode = "value";
            //    queryForm.ShowDialog();




            //    if (queryForm.NewValue != "")
            //    {
            //        dgv.CurrentRow.Cells["外协单价"].Value = queryForm.NewValue;

            //        SaveTask();



            //    }
            //    else
            //    {

            //    }
            //    return;

            //}

            /////////////////////////////////////////////////////////

            if ("实际废品工时1" == dgv.CurrentCell.OwningColumn.Name)
            {

                ChangeValue queryForm = new ChangeValue();

                queryForm.OldValue = dgv.CurrentCell.Value.ToString();
                queryForm.NewValue = "";
                queryForm.ChangeMode = "value";
                queryForm.ShowDialog();




                if (queryForm.NewValue != "")
                {
                    dgv.CurrentRow.Cells["实际废品工时1"].Value = queryForm.NewValue;
                    //dgv.CurrentRow.Cells["discount_money"].Value = DBNull.Value;

                    //dgv.CurrentRow.Cells["approve_flag"].Value = "False";
                    SaveTask();


                    //CountPlanStru();

                }
                else
                {
                    //hT_Manage_ItemDataGridView.CurrentRow.Cells["apply_money"].Value = queryForm.NewValue;
                    //dgv.CurrentRow.Cells["discount_money"].Value = DBNull.Value;
                    //dgv.CurrentRow.Cells["apply_money"].Value = DBNull.Value;
                    //dgv.CurrentRow.Cells["approve_flag"].Value = "False";
                    //SaveChanged();

                }
                return;

            }

            ///////////////////////////////////////////////////////

            if ("备注1" == dgv.CurrentCell.OwningColumn.Name)
            {

                ChangeValue queryForm = new ChangeValue();

                queryForm.OldValue = dgv.CurrentCell.Value.ToString();
                queryForm.NewValue = "";
                queryForm.ChangeMode = "longstring";
                queryForm.ShowDialog();




                if (queryForm.NewValue != "")
                {
                    dgv.CurrentRow.Cells["备注1"].Value = queryForm.NewValue;
                    //dgv.CurrentRow.Cells["discount_money"].Value = DBNull.Value;

                    //dgv.CurrentRow.Cells["approve_flag"].Value = "False";
                    SaveTask();


                    //CountPlanStru();

                }
                else
                {

                }
                return;

            }

            /////////////////////////////

            ///////////////////////////////////////////////////////

            if ("排序日期" == dgv.CurrentCell.OwningColumn.Name)
            {

                ChangeValue queryForm = new ChangeValue();

                queryForm.OldValue = dgv.CurrentCell.Value.ToString();
                queryForm.NewValue = "";
                queryForm.ChangeMode = "datetime";
                queryForm.ShowDialog();




                if (queryForm.NewValue != "")
                {
                    dgv.CurrentRow.Cells["排序日期"].Value = queryForm.NewValue;
                    //dgv.CurrentRow.Cells["discount_money"].Value = DBNull.Value;

                    //dgv.CurrentRow.Cells["approve_flag"].Value = "False";
                    SaveTask();


                    //CountPlanStru();

                }
                else
                {

                }
                return;

            }

            /////////////////////////////
        }



        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            if (null == this.ly_production_order_detailDataGridView.CurrentRow) return;


            if (null != this.ly_production_orderDataGridView.CurrentRow)
            {

                string nowitem = this.ly_production_orderDataGridView.CurrentRow.Cells["物料编码"].Value.ToString();
                string nowproductionorderNum = this.ly_production_orderDataGridView.CurrentRow.Cells["跟单编号"].Value.ToString();


                this.ly_production_order_materialrequisitionTableAdapter.Fill(this.lYMaterielRequirements.ly_production_order_materialrequisition, nowproductionorderNum);
                //set_processOrder_Num();
            }


            //--------2018-4-4 机加工序出现调整情况，需可删除
            //if (this.ly_production_order_materialrequisitionDataGridView.RowCount > 0)
            //{

            //    decimal hadget = 0;
            //    foreach (DataGridViewRow dgr in ly_production_order_materialrequisitionDataGridView.Rows)
            //    {

            //        if (string.IsNullOrEmpty(dgr.Cells["已领5"].Value.ToString())) continue;
            //        hadget = hadget + decimal.Parse(dgr.Cells["已领5"].Value.ToString());
            //    }
            //    if (0 < hadget)
            //    {

            //        MessageBox.Show("跟单已有领料记录，不能删除(实需删除，请先删除该跟单的领料记录)", "注意");
            //        return;
            //    } 
            //}


            if (!checkqualityRec())
            {

                MessageBox.Show("任务已有检验记录,不能修改, 操作取消", "注意");

                return;

            }

            string diaodu = this.ly_production_order_detailDataGridView.CurrentRow.Cells["调度1"].Value.ToString();

            if (diaodu != SQLDatabase.nowUserName())
            {
                MessageBox.Show("请调度:" + diaodu + "删除", "注意");
                return;
            }


            string noworder = this.ly_machinepart_process_workDataGridView.CurrentRow.Cells["工序名称"].Value.ToString();
            string nowName = this.ly_production_order_detailDataGridView.CurrentRow.Cells["姓名1"].Value.ToString();



            string message1 = "删除" + nowName + " (工序：" + noworder + " )任务安排，继续吗？";
            string caption1 = "提示...";
            MessageBoxButtons buttons1 = MessageBoxButtons.YesNo;

            DialogResult result1;



            result1 = MessageBox.Show(message1, caption1, buttons1,
            MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

            if (result1 == DialogResult.Yes)
            {

                this.ly_production_order_detailBindingSource.RemoveCurrent();
                SaveTask();
            }
        }

        private void toolStripButton8_Click(object sender, EventArgs e)
        {
            ExportDataGridviewTOExcell.ExportDataGridview(this.lY_MaterielRequirementsDataGridView, true);
        }

        private void toolStripButton7_Click(object sender, EventArgs e)
        {
            string nowproductionorder = this.ly_production_orderDataGridView.CurrentRow.Cells["跟单编号"].Value.ToString();
            string noworder = this.ly_machinepart_process_workDataGridView.CurrentRow.Cells["工序名称"].Value.ToString();




            string message1 = "确认分配(跟单：" + nowproductionorder + " 工序:" + noworder + ")外协任务吗?";
            string caption1 = "提示...";
            MessageBoxButtons buttons1 = MessageBoxButtons.YesNo;

            DialogResult result1;



            result1 = MessageBox.Show(message1, caption1, buttons1,
            MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

            if (result1 == DialogResult.Yes)
            {

                string sel = "SELECT  supplier_code as 工号, supplier_name as 姓名 FROM ly_supplier_list where sort_code='4' "; 
                QueryForm queryForm = new QueryForm(); 
                queryForm.Sel = sel;
                queryForm.Constr = SQLDatabase.Connectstring;



                queryForm.ShowDialog();



                if (string.IsNullOrEmpty(queryForm.Result))
                {
                    MessageBox.Show("必须选择委托加工商,才能安排跟单外协...", "注意");
                    return;
                }


                /////////////////////////////////////////////////////////////////

                ///////////////////////////////////////////////////////////////// 

                decimal up_order_count;
                decimal order_count;

                decimal up_quality;
                decimal up_canuse;
                decimal up_remake;

                int nowrowIndex = this.ly_machinepart_process_workDataGridView.CurrentRow.Index;
                string nowordername = this.ly_machinepart_process_workDataGridView.CurrentRow.Cells["工序名称"].Value.ToString();

                int nowordernum = int.Parse(this.ly_machinepart_process_workDataGridView.CurrentRow.Cells["工序编号"].Value.ToString());

                if (1 != nowordernum)
                {
                    if (!string.IsNullOrEmpty(this.ly_machinepart_process_workDataGridView.Rows[nowrowIndex - 1].Cells["本序合格"].Value.ToString()))
                    {


                        up_quality = decimal.Parse(this.ly_machinepart_process_workDataGridView.Rows[nowrowIndex - 1].Cells["本序合格"].Value.ToString());
                    }
                    else
                    {

                        up_quality = 0;
                    }

                    if (!string.IsNullOrEmpty(this.ly_machinepart_process_workDataGridView.Rows[nowrowIndex - 1].Cells["本序可用数量"].Value.ToString()))
                    {


                        up_canuse = decimal.Parse(this.ly_machinepart_process_workDataGridView.Rows[nowrowIndex - 1].Cells["本序可用数量"].Value.ToString());
                    }
                    else
                    {

                        up_canuse = 0;
                    }

                    ////////////////////////

                    if (!string.IsNullOrEmpty(this.ly_machinepart_process_workDataGridView.Rows[nowrowIndex - 1].Cells["本序返修合格"].Value.ToString()))
                    {


                        up_remake = decimal.Parse(this.ly_machinepart_process_workDataGridView.Rows[nowrowIndex - 1].Cells["本序返修合格"].Value.ToString());
                    }
                    else
                    {

                        up_remake = 0;
                    }


                    up_order_count = up_quality + up_canuse + up_remake;


                    //if (!string.IsNullOrEmpty(this.ly_machinepart_process_workDataGridView.Rows[nowrowIndex - 1].Cells["本序数量"].Value.ToString()))
                    //{


                    //    up_order_count = decimal.Parse(this.ly_machinepart_process_workDataGridView.Rows[nowrowIndex - 1].Cells["本序数量"].Value.ToString());
                    //}
                    //else
                    //{

                    //    up_order_count = 0;
                    //}
                }
                else
                {
                    if (!string.IsNullOrEmpty(this.ly_machinepart_process_workDataGridView.CurrentRow.Cells["跟单数量"].Value.ToString()))
                    {
                        up_order_count = decimal.Parse(this.ly_machinepart_process_workDataGridView.CurrentRow.Cells["跟单数量"].Value.ToString());
                    }
                    else
                    {
                        up_order_count = 0;
                    }

                }


                if (!string.IsNullOrEmpty(this.ly_machinepart_process_workDataGridView.CurrentRow.Cells["本序数量"].Value.ToString()))
                {
                    order_count = decimal.Parse(this.ly_machinepart_process_workDataGridView.CurrentRow.Cells["本序数量"].Value.ToString());
                }
                else
                {
                    order_count = 0;
                }



                decimal arrange_count = 0;

                if (null != this.ly_production_orderDataGridView.CurrentRow)
                {

                    foreach (DataGridViewRow dgr in ly_production_order_detailDataGridView.Rows)
                    {

                        if (string.IsNullOrEmpty(dgr.Cells["数量1"].Value.ToString())) continue;
                        arrange_count = arrange_count + decimal.Parse(dgr.Cells["数量1"].Value.ToString());



                    }
                }



                if (arrange_count >= up_order_count)
                {
                    MessageBox.Show("跟单数量已经全部安排,不能增加跟单任务", "注意");

                    return;
                }
                else
                {
                    

                    this.ly_production_order_detailBindingSource.AddNew();


                    //---------------------带出最近的一次外协单价


                    string strsql = SQLDatabase.Connectstring;
                    string sql = "ly_outmachine_contract_getPrice";
                    SqlConnection conStr = new SqlConnection(strsql);
                    SqlCommand comStr = new SqlCommand(sql, conStr);
                    comStr.CommandType = CommandType.StoredProcedure;

                    comStr.Parameters.Add("@workcode", SqlDbType.Text).Value = queryForm.Result;
                    comStr.Parameters.Add("@itemno", SqlDbType.Text).Value = lY_MaterielRequirementsDataGridView.CurrentRow.Cells["物料编码1"].Value.ToString();
                    comStr.Parameters.Add("@processCode", SqlDbType.Int).Value = int.Parse(this.ly_machinepart_process_workDataGridView.CurrentRow.Cells["工序编号"].Value.ToString());
                    conStr.Open();
                    SqlDataAdapter SqlDataAdapter1 = new SqlDataAdapter(comStr);
                    DataTable DT = new DataTable();
                    SqlDataAdapter1.Fill(DT);
                    if (DT.Rows.Count >= 1 && decimal.Parse(DT.Rows[0]["contract_price"].ToString()) > 0)
                    {

                        this.ly_production_order_detailDataGridView.CurrentRow.Cells["外协单价"].Value = DT.Rows[0]["contract_price"].ToString();
                    }
                    else
                    {
                        ChangeValue queryForm2 = new ChangeValue();

                        queryForm2.Text = "请输入外协单价";
                        queryForm2.NewValue = "";
                        queryForm2.ChangeMode = "value";
                        queryForm2.ShowDialog();
                        if (queryForm2.NewValue != "")
                        {
                            this.ly_production_order_detailDataGridView.CurrentRow.Cells["外协单价"].Value = queryForm2.NewValue;
                        }
                        else
                        {
                            MessageBox.Show("外协单价必须输入", "注意");

                            return;

                        }

                    }
                    conStr.Close();//关闭连接  



                    //-----------------------



                    this.ly_production_order_detailDataGridView.CurrentRow.Cells["跟单编号1"].Value = this.ly_production_orderDataGridView.CurrentRow.Cells["跟单编号"].Value.ToString();

                    this.ly_production_order_detailDataGridView.CurrentRow.Cells["工序1"].Value = this.ly_machinepart_process_workDataGridView.CurrentRow.Cells["工序编号"].Value;

                    this.ly_production_order_detailDataGridView.CurrentRow.Cells["sequence_number1"].Value = this.ly_machinepart_process_workDataGridView.CurrentRow.Cells["sequence_number"].Value;


                    this.ly_production_order_detailDataGridView.CurrentRow.Cells["pruductionOrder_count1"].Value = this.ly_machinepart_process_workDataGridView.CurrentRow.Cells["跟单数量"].Value.ToString();

                    this.ly_production_order_detailDataGridView.CurrentRow.Cells["准终1"].Value = this.ly_machinepart_process_workDataGridView.CurrentRow.Cells["准终"].Value;

                    this.ly_production_order_detailDataGridView.CurrentRow.Cells["实际准终1"].Value = this.ly_machinepart_process_workDataGridView.CurrentRow.Cells["准终"].Value;

                    this.ly_production_order_detailDataGridView.CurrentRow.Cells["单件工时1"].Value = this.ly_machinepart_process_workDataGridView.CurrentRow.Cells["单件工时"].Value;
                    this.ly_production_order_detailDataGridView.CurrentRow.Cells["实际单件工时1"].Value = this.ly_machinepart_process_workDataGridView.CurrentRow.Cells["单件工时"].Value;

                    this.ly_production_order_detailDataGridView.CurrentRow.Cells["准终累加1"].Value = this.ly_machinepart_process_workDataGridView.CurrentRow.Cells["准终累加"].Value;
                    this.ly_production_order_detailDataGridView.CurrentRow.Cells["单件工时累加1"].Value = this.ly_machinepart_process_workDataGridView.CurrentRow.Cells["工时累加"].Value;

                    this.ly_production_order_detailDataGridView.CurrentRow.Cells["工时单价1"].Value = this.ly_machinepart_process_workDataGridView.CurrentRow.Cells["工时单价"].Value;
                    this.ly_production_order_detailDataGridView.CurrentRow.Cells["材料单价1"].Value = this.ly_production_orderDataGridView.CurrentRow.Cells["物料单价"].Value;


                    this.ly_production_order_detailDataGridView.CurrentRow.Cells["数量1"].Value = up_order_count - arrange_count;

                    this.ly_production_order_detailDataGridView.CurrentRow.Cells["调度1"].Value = SQLDatabase.nowUserName();

                    this.ly_production_order_detailDataGridView.CurrentRow.Cells["排序日期"].Value = SQLDatabase.GetNowdate();
                    this.ly_production_order_detailDataGridView.CurrentRow.Cells["外协"].Value = "True";
                    this.ly_production_order_detailDataGridView.CurrentRow.Cells["工号"].Value = queryForm.Result;
                    this.ly_production_order_detailDataGridView.CurrentRow.Cells["姓名1"].Value = queryForm.Result1;


                   



                    SaveTask();
                }

            }
        }

        private void LY_Machine_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.timer1.Stop();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.notifyIcon1.Visible = true;
            this.notifyIcon1.ShowBalloonTip(2000, "提示", "有新加工任务,请查看...", ToolTipIcon.Info);
        }

        private void notifyIcon1_BalloonTipClosed(object sender, EventArgs e)
        {
            this.notifyIcon1.Visible = false;
        }

        private void toolStripButton9_Click(object sender, EventArgs e)
        {


            /////////////////////////////////////////////



            ////int ifxialiao = this.ly_outsource_order_detail_selBindingSource.Find("序名", "下料");
            ////int nowdetailId;

            ////if (0 > ifxialiao)
            ////{
            ////    MessageBox.Show("无下料工艺,不能追加领料...", "注意");
            ////    return;
            ////}
            ////else
            ////{
            ////    this.ly_outsource_order_detail_selBindingSource.Position = ifxialiao;

            ////    nowdetailId = int.Parse(this.ly_outsource_order_detail_selDataGridView.Rows[ifxialiao].Cells["id_order"].Value.ToString());

            ////}

            //DataGridView dgv = ly_production_orderDataGridView;


            //string nowparentitemno = dgv.CurrentRow.Cells["物料编号7"].Value.ToString();
            //string nowcontractcode = dgv.CurrentRow.Cells["合同编号7"].Value.ToString();

            //string nowbuyer = this.ly_purchase_contract_mainDataGridView.CurrentRow.Cells["采购员"].Value.ToString();
            //string nowmaterialname = dgv.CurrentRow.Cells["物料名称7"].Value.ToString();

            //string message1 = "确认追加(合同：" + nowcontractcode + "  " + nowmaterialname + "(" + nowparentitemno + ") " + nowbuyer + " )原材料领料吗?";
            //string caption1 = "提示...";
            //MessageBoxButtons buttons1 = MessageBoxButtons.YesNo;

            //DialogResult result1;



            //result1 = MessageBox.Show(message1, caption1, buttons1,
            //MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

            //if (result1 == DialogResult.Yes)
            //{


            //    string nowrequisition_style = "WXLL";


            //    string nowmaterialrequisition_num = GetMaxMaterialrequisitioncode(nowcontractcode, nowparentitemno);


            //    string nowitemno;

            //    ///////////////////////////////////////////////////////





            //    string sel = " SELECT   a.wzbh as 编号,a.mch as 名称,a.jph as 库位,a.xhc as 中方型号,a.xhc as 日方型号,a.gg as 规格,a.mch_jp as 简拼,a.mch_py as 拼音,b.sortname as类别 FROM ly_inma0010 a left join ly_materrial_sort b on a.sort1=b.sortcode  where a.status='原料' or a.status='基料' ";



            //    QueryForm queryForm = new QueryForm();


            //    queryForm.Sel = sel;
            //    queryForm.Constr = SQLDatabase.Connectstring;

            //    //Set the Column Collection to the filter Table
            //    //queryForm.SetSourceColumns(this.billMainDataSet.BalanceBill.Columns);

            //    queryForm.ShowDialog();


            //    if (!string.IsNullOrEmpty(queryForm.Result))
            //    {
            //        nowitemno = queryForm.Result;
            //        //dgv.CurrentRow.Cells["discount_money"].Value = DBNull.Value;



            //    }
            //    else
            //    {
            //        return;

            //    }



            //    ///////////////////////////////////////////////////////



            //    this.ly_outsource_order_materialrequisitionBindingSource.AddNew();



            //    this.ly_outsource_order_materialrequisitionDataGridView.CurrentRow.Cells["detail_id"].Value = nowdetailId;
            //    this.ly_outsource_order_materialrequisitionDataGridView.CurrentRow.Cells["追加"].Value = "True";
            //    this.ly_outsource_order_materialrequisitionDataGridView.CurrentRow.Cells["类别"].Value = nowrequisition_style;

            //    this.ly_outsource_order_materialrequisitionDataGridView.CurrentRow.Cells["领料单号"].Value = nowmaterialrequisition_num;
            //    this.ly_outsource_order_materialrequisitionDataGridView.CurrentRow.Cells["父件编码"].Value = nowparentitemno;
            //    this.ly_outsource_order_materialrequisitionDataGridView.CurrentRow.Cells["合同编号8"].Value = nowcontractcode;
            //    this.ly_outsource_order_materialrequisitionDataGridView.CurrentRow.Cells["物料编号8"].Value = nowitemno;
            //    this.ly_outsource_order_materialrequisitionDataGridView.CurrentRow.Cells["姓名"].Value = nowbuyer;
            //    this.ly_outsource_order_materialrequisitionDataGridView.CurrentRow.Cells["下单人"].Value = nowbuyer;
            //    this.ly_outsource_order_materialrequisitionDataGridView.CurrentRow.Cells["安排日期"].Value = SQLDatabase.GetNowdate();

            //    this.ly_outsource_order_materialrequisitionDataGridView.EndEdit();
            //    this.ly_outsource_order_materialrequisitionBindingSource.EndEdit();

            //    this.ly_outsource_order_materialrequisitionTableAdapter.Update(this.lYOutsourceData.ly_outsource_order_materialrequisition);
            //    this.ly_outsource_order_materialrequisitionTableAdapter.Fill(this.lYOutsourceData.ly_outsource_order_materialrequisition, nowcontractcode);

            //    this.ly_outsource_order_materialrequisitionBindingSource.Position = this.ly_outsource_order_materialrequisitionBindingSource.Count - 1;
            //}

            ///////////////////////////////////////////////

            //if ("True" == this.ly_production_order_materialrequisitionDataGridView.CurrentRow.Cells["追加5"].Value.ToString())
            //{

            //    MessageBox.Show("追加领料单不能继续追加领料,可以在原领料单上继续追加领料的操作...", "注意");
            //    return;
            //}

            string nowproductionorder;
            string nowworker;
            string nowmaterialcode;
            string nowmaterialname;


            if (null == this.ly_production_order_materialrequisitionDataGridView.CurrentRow)
            {
                nowproductionorder = this.ly_production_orderDataGridView.CurrentRow.Cells["跟单编号"].Value.ToString();

                string sel = "SELECT  work_code as 工号, worker_name as 姓名 FROM ly_worker_list where prodcode='04'";


                QueryForm queryForm = new QueryForm();


                queryForm.Sel = sel;
                queryForm.Constr = SQLDatabase.Connectstring;



                queryForm.ShowDialog();



                //(queryForm.Result))
                    nowworker = queryForm.Result1;
                //this.ly_production_order_detailDataGridView.CurrentRow.Cells["工号"].Value = queryForm.Result;
                //this.ly_production_order_detailDataGridView.CurrentRow.Cells["姓名1"].Value = queryForm.Result1;
                //nowmaterialcode = this.ly_production_order_materialrequisitionDataGridView.CurrentRow.Cells["物料编号5"].Value.ToString();
                //nowmaterialname = this.ly_production_order_materialrequisitionDataGridView.CurrentRow.Cells["物料名称5"].Value.ToString();


                //return;

            }
            else
            {
                nowproductionorder = this.ly_production_order_materialrequisitionDataGridView.CurrentRow.Cells["跟单编号5"].Value.ToString();
                nowworker = this.ly_production_order_materialrequisitionDataGridView.CurrentRow.Cells["姓名5"].Value.ToString();
                nowmaterialcode = this.ly_production_order_materialrequisitionDataGridView.CurrentRow.Cells["物料编号5"].Value.ToString();
                nowmaterialname = this.ly_production_order_materialrequisitionDataGridView.CurrentRow.Cells["物料名称5"].Value.ToString();


            }
            string message1 = "确认追加(跟单：" + nowproductionorder + "  " + nowworker + " )领料吗?";
            string caption1 = "提示...";
            MessageBoxButtons buttons1 = MessageBoxButtons.YesNo;

            DialogResult result1;



            result1 = MessageBox.Show(message1, caption1, buttons1,
            MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

            if (result1 == DialogResult.Yes)
            {
                //string nowrequisition_style = "WXLL";


                //string nowmaterialrequisition_num = GetMaxMaterialrequisitioncode(nowcontractcode, nowparentitemno);


                string nowitemcode;

                ///////////////////////////////////////////////////////


                string sel = " SELECT   a.wzbh as 编号,a.mch as 名称,a.jph as 库位,a.xhc as 中方型号,a.xhc as 日方型号,a.gg as 规格,a.mch_jp as 简拼,a.mch_py as 拼音,b.sortname as类别 FROM ly_inma0010 a left join ly_materrial_sort b on a.sort1=b.sortcode  where a.status='原料' or a.status='基料' ";


                QueryForm queryForm = new QueryForm();


                queryForm.Sel = sel;
                queryForm.Constr = SQLDatabase.Connectstring;

                //Set the Column Collection to the filter Table
                //queryForm.SetSourceColumns(this.billMainDataSet.BalanceBill.Columns);

                queryForm.ShowDialog();


                if (!string.IsNullOrEmpty(queryForm.Result))
                {
                    nowitemcode = queryForm.Result;
                    //dgv.CurrentRow.Cells["discount_money"].Value = DBNull.Value;



                }
                else
                {
                    return;

                }



                //    ///////////////////////////////////////////////////////

                string nowdetailId;
                string nowrequisition_style ;
                string nowproductionorderNum;
                // string nowitemcode = this.ly_production_order_materialrequisitionDataGridView.CurrentRow.Cells["物料编号5"].Value.ToString();

                string replacedmaterialrequisition_num;

                if (null == this.ly_production_order_materialrequisitionDataGridView.CurrentRow)
                {
                    nowdetailId = "-9";
                    nowrequisition_style ="JGLL";
                    nowproductionorderNum = nowproductionorder;
                    // string nowitemcode = this.ly_production_order_materialrequisitionDataGridView.CurrentRow.Cells["物料编号5"].Value.ToString();

                    replacedmaterialrequisition_num = "";
                }
                else
                {
                     nowdetailId = this.ly_production_order_materialrequisitionDataGridView.CurrentRow.Cells["detail_id5"].Value.ToString();
                     nowrequisition_style = this.ly_production_order_materialrequisitionDataGridView.CurrentRow.Cells["领料类别5"].Value.ToString();
                     nowproductionorderNum = this.ly_production_order_materialrequisitionDataGridView.CurrentRow.Cells["跟单编号5"].Value.ToString();
                    // string nowitemcode = this.ly_production_order_materialrequisitionDataGridView.CurrentRow.Cells["物料编号5"].Value.ToString();

                     replacedmaterialrequisition_num = this.ly_production_order_materialrequisitionDataGridView.CurrentRow.Cells["领料单号5"].Value.ToString();
                    //string replacedoriginalqty = this.ly_production_order_materialrequisitionDataGridView.CurrentRow.Cells["原单数量5"].Value.ToString();
                }

                this.ly_production_order_materialrequisitionBindingSource.AddNew();



                this.ly_production_order_materialrequisitionDataGridView.CurrentRow.Cells["detail_id5"].Value = nowdetailId;

                this.ly_production_order_materialrequisitionDataGridView.CurrentRow.Cells["领料类别5"].Value = nowrequisition_style;

                this.ly_production_order_materialrequisitionDataGridView.CurrentRow.Cells["跟单编号5"].Value = nowproductionorderNum;


                //this.ly_production_order_materialrequisitionDataGridView.CurrentRow.Cells["领料单号"].Value = this.ly_production_order_materialrequisitionDataGridView.CurrentRow.Cells["跟单数量"].Value.ToString();

                this.ly_production_order_materialrequisitionDataGridView.CurrentRow.Cells["物料编号5"].Value = nowitemcode;

                this.ly_production_order_materialrequisitionDataGridView.CurrentRow.Cells["追加5"].Value = "True";
                this.ly_production_order_materialrequisitionDataGridView.CurrentRow.Cells["追加类别5"].Value = "普通";

                this.ly_production_order_materialrequisitionDataGridView.CurrentRow.Cells["原单编号5"].Value = replacedmaterialrequisition_num;
                //this.ly_production_order_materialrequisitionDataGridView.CurrentRow.Cells["原单数量5"].Value = replacedoriginalqty;

                this.ly_production_order_materialrequisitionDataGridView.EndEdit();
                this.ly_production_order_materialrequisitionBindingSource.EndEdit();

                try
                {

                    this.ly_production_order_materialrequisitionTableAdapter.Update(this.lYMaterielRequirements.ly_production_order_materialrequisition);
                }
                catch (SqlException sqe)
                {
                    this.ly_production_order_materialrequisitionBindingSource.RemoveCurrent();
                    MessageBox.Show(sqe.Message , "注意");
                    return;
                }
                    this.ly_production_order_materialrequisitionTableAdapter.Fill(this.lYMaterielRequirements.ly_production_order_materialrequisition, nowproductionorderNum);

                this.ly_production_order_materialrequisitionBindingSource.Position = this.ly_production_order_materialrequisitionBindingSource.Count - 1;
            }

        }

        private void toolStripButton11_Click(object sender, EventArgs e)
        {

            if (null == ly_production_order_materialrequisitionDataGridView.CurrentRow) return;

            if ("True" == this.ly_production_order_materialrequisitionDataGridView.CurrentRow.Cells["追加5"].Value.ToString())
            {

                MessageBox.Show("追加领料单不能继续追加领料,可以在原领料单上继续追加领料的操作...", "注意");
                return;
            }

            string nowproductionorder = this.ly_production_order_materialrequisitionDataGridView.CurrentRow.Cells["跟单编号5"].Value.ToString();
            string nowworker = this.ly_production_order_materialrequisitionDataGridView.CurrentRow.Cells["姓名5"].Value.ToString();
            string nowmaterialcode = this.ly_production_order_materialrequisitionDataGridView.CurrentRow.Cells["物料编号5"].Value.ToString();
            string nowmaterialname = this.ly_production_order_materialrequisitionDataGridView.CurrentRow.Cells["物料名称5"].Value.ToString();

            //string replacedmaterialrequisition_num = this.ly_production_order_materialrequisitionDataGridView.CurrentRow.Cells["原单编号5"].Value.ToString();
            //string replacedoriginalqty = this.ly_production_order_materialrequisitionDataGridView.CurrentRow.Cells["原单数量5"].Value.ToString();

            string message1 = "确认追加(跟单：" + nowproductionorder + "  " + nowmaterialname + "(" + nowmaterialcode + ") " + nowworker + " )代料领料吗?";
            string caption1 = "提示...";
            MessageBoxButtons buttons1 = MessageBoxButtons.YesNo;

            DialogResult result1;



            result1 = MessageBox.Show(message1, caption1, buttons1,
            MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

            if (result1 == DialogResult.Yes)
            {
                string nowdetailId = this.ly_production_order_materialrequisitionDataGridView.CurrentRow.Cells["detail_id5"].Value.ToString();
                string nowrequisition_style = this.ly_production_order_materialrequisitionDataGridView.CurrentRow.Cells["领料类别5"].Value.ToString();
                string nowproductionorderNum = this.ly_production_order_materialrequisitionDataGridView.CurrentRow.Cells["跟单编号5"].Value.ToString();
                string nowitemcode = this.ly_production_order_materialrequisitionDataGridView.CurrentRow.Cells["物料编号5"].Value.ToString();
                //string nowrequisition_style = this.ly_production_order_materialrequisitionDataGridView.CurrentRow.Cells["追加"].Value.ToString();

                string replacedmaterialrequisition_num = this.ly_production_order_materialrequisitionDataGridView.CurrentRow.Cells["领料单号5"].Value.ToString();
                //this.ly_production_order_materialrequisitionDataGridView.CurrentRow.Cells["原单数量5"].Value = replacedoriginalqty;


                string replaceitemno;

                ///////////////////////////////////////////////////////




                string sel;



                sel = "SELECT   a.replace_item as 编号, c.mch as 名称,c.dw AS 单位, a.replace_ratio as 代换比例, c.jph AS 库位, c.xhc AS 型号, c.gg AS 规格,  c.geometry as 形状, c.specific_weight as 比重, c.diameter as 直径, c.length as 长度, c.width as 宽度 ,c.length as 高度 FROM ly_material_replacelist a left join ly_inma0010 c on a.replace_item=c.wzbh where a.itemno='" + nowitemcode + "'";


                QueryForm queryForm = new QueryForm();


                queryForm.Sel = sel;
                queryForm.Constr = SQLDatabase.Connectstring;

                //Set the Column Collection to the filter Table
                //queryForm.SetSourceColumns(this.billMainDataSet.BalanceBill.Columns);

                queryForm.ShowDialog();


                if (!string.IsNullOrEmpty(queryForm.Result))
                {
                    replaceitemno = queryForm.Result;
                    //dgv.CurrentRow.Cells["discount_money"].Value = DBNull.Value;



                }
                else
                {
                    return;

                }



                ///////////////////////////////////////////////////////


                this.ly_production_order_materialrequisitionBindingSource.AddNew();



                this.ly_production_order_materialrequisitionDataGridView.CurrentRow.Cells["detail_id5"].Value = nowdetailId;

                this.ly_production_order_materialrequisitionDataGridView.CurrentRow.Cells["领料类别5"].Value = nowrequisition_style;

                this.ly_production_order_materialrequisitionDataGridView.CurrentRow.Cells["跟单编号5"].Value = nowproductionorderNum;


                //this.ly_production_order_materialrequisitionDataGridView.CurrentRow.Cells["领料单号"].Value = this.ly_production_order_materialrequisitionDataGridView.CurrentRow.Cells["跟单数量"].Value.ToString();

                this.ly_production_order_materialrequisitionDataGridView.CurrentRow.Cells["物料编号5"].Value = replaceitemno;

                this.ly_production_order_materialrequisitionDataGridView.CurrentRow.Cells["追加5"].Value = "True";
                this.ly_production_order_materialrequisitionDataGridView.CurrentRow.Cells["追加类别5"].Value = "代料";

                this.ly_production_order_materialrequisitionDataGridView.CurrentRow.Cells["原单编号5"].Value = replacedmaterialrequisition_num;
                //this.ly_production_order_materialrequisitionDataGridView.CurrentRow.Cells["原单数量5"].Value = replacedoriginalqty;


                this.ly_production_order_materialrequisitionDataGridView.EndEdit();
                this.ly_production_order_materialrequisitionBindingSource.EndEdit();

                this.ly_production_order_materialrequisitionTableAdapter.Update(this.lYMaterielRequirements.ly_production_order_materialrequisition);
                this.ly_production_order_materialrequisitionTableAdapter.Fill(this.lYMaterielRequirements.ly_production_order_materialrequisition, nowproductionorderNum);

                this.ly_production_order_materialrequisitionBindingSource.Position = this.ly_production_order_materialrequisitionBindingSource.Count - 1;
            }
        }

        private void toolStripButton10_Click(object sender, EventArgs e)
        {
            if (null == ly_production_order_materialrequisitionDataGridView.CurrentRow) return;

            string diaodu = this.ly_production_order_materialrequisitionDataGridView.CurrentRow.Cells["调度5"].Value.ToString();

            //if (diaodu != SQLDatabase.nowUserName())
            //{
            //    MessageBox.Show("请调度:" + diaodu + "删除", "注意");
            //    return;
            //}



            if ("True" != this.ly_production_order_materialrequisitionDataGridView.CurrentRow.Cells["追加5"].Value.ToString())
            {

                MessageBox.Show("只有追加领料单才能删除...", "注意");
                return;
            }

            decimal nowgetqty;



            if ("" != this.ly_production_order_materialrequisitionDataGridView.CurrentRow.Cells["已领5"].Value.ToString())
            {
                nowgetqty = decimal.Parse(this.ly_production_order_materialrequisitionDataGridView.CurrentRow.Cells["已领5"].Value.ToString());
            }
            else
            {
                nowgetqty = 0;
            }

            if (nowgetqty > 0)
            {
                MessageBox.Show("已有领料记录,删除领料记录后才能删除领料单...", "注意");
                return;

            }

            string nowmaterialrequisitionnum = this.ly_production_order_materialrequisitionDataGridView.CurrentRow.Cells["领料单号5"].Value.ToString();

            string message1 = "当前(领料单：" + nowmaterialrequisitionnum + ")将被删除，继续吗？";
            string caption1 = "提示...";
            MessageBoxButtons buttons1 = MessageBoxButtons.YesNo;

            DialogResult result1;



            result1 = MessageBox.Show(message1, caption1, buttons1,
            MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

            if (result1 == DialogResult.Yes)
            {

                this.ly_production_order_materialrequisitionBindingSource.RemoveCurrent();


                this.ly_production_order_materialrequisitionDataGridView.EndEdit();
                this.ly_production_order_materialrequisitionBindingSource.EndEdit();

                this.ly_production_order_materialrequisitionTableAdapter.Update(this.lYMaterielRequirements.ly_production_order_materialrequisition);




            }
        }

        private void ly_production_order_materialrequisitionDataGridView_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (null == this.ly_production_order_materialrequisitionDataGridView.CurrentRow) return;

            DataGridView dgv = sender as DataGridView;

            string diaodu = this.ly_production_order_materialrequisitionDataGridView.CurrentRow.Cells["调度5"].Value.ToString();

            //if ( !SQLDatabase.CheckHaveRight(SQLDatabase.NowUserID,"追加领料批准" ))
            //{
            //    MessageBox.Show("无追加领料批准权限", "注意");
            //    return;
            //}

            string nowgeometry;



            if (!string.IsNullOrEmpty(dgv.CurrentRow.Cells["geometry"].Value.ToString()))
            {
                nowgeometry = dgv.CurrentRow.Cells["geometry"].Value.ToString();
            }
            else
            {
                nowgeometry = "ELSE";
            }


            //if ("True" != this.ly_production_order_materialrequisitionDataGridView.CurrentRow.Cells["追加5"].Value.ToString() && "ELSE" == nowgeometry)
            //{

            //    MessageBox.Show("只有追加领料单才能修改...", "注意");
            //    return;
            //}

            decimal nowgetqty;

            if ("" != this.ly_production_order_materialrequisitionDataGridView.CurrentRow.Cells["已领5"].Value.ToString())
            {
                nowgetqty = decimal.Parse(this.ly_production_order_materialrequisitionDataGridView.CurrentRow.Cells["已领5"].Value.ToString());
            }
            else
            {
                nowgetqty = 0;
            }

            if (nowgetqty > 0)
            {
                MessageBox.Show("已有领料记录,删除领料记录后才能修改领料单...", "注意");
                return;

            }

            //DataGridView dgv = sender as DataGridView;

            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            // if ("数量5" == dgv.CurrentCell.OwningColumn.Name && dgv.CurrentRow.Cells["追加类别5"].Value.ToString() == "普通")
            if ("数量5" == dgv.CurrentCell.OwningColumn.Name && dgv.CurrentRow.Cells["追加类别5"].Value.ToString() != "代料")

            {

                ChangeValue queryForm = new ChangeValue();

                queryForm.OldValue = dgv.CurrentCell.Value.ToString();
                queryForm.NewValue = "";
                queryForm.ChangeMode = "value";
                queryForm.ShowDialog();

                if (queryForm.NewValue != "")
                {
                    dgv.CurrentRow.Cells["数量5"].Value = queryForm.NewValue;
                    //dgv.CurrentRow.Cells["discount_money"].Value = DBNull.Value;

                    //dgv.CurrentRow.Cells["approve_flag"].Value = "False";
                    this.ly_production_order_materialrequisitionDataGridView.EndEdit();
                    this.ly_production_order_materialrequisitionBindingSource.EndEdit();

                    this.ly_production_order_materialrequisitionTableAdapter.Update(this.lYMaterielRequirements.ly_production_order_materialrequisition);


                    //CountPlanStru();

                }
                else
                {
                    MessageBox.Show("代料领料请在原单数量里输入被代料数量,系统会自动按比例计算代料数量...", "注意");

                    //hT_Manage_ItemDataGridView.CurrentRow.Cells["apply_money"].Value = queryForm.NewValue;
                    //dgv.CurrentRow.Cells["discount_money"].Value = DBNull.Value;
                    //dgv.CurrentRow.Cells["apply_money"].Value = DBNull.Value;
                    //dgv.CurrentRow.Cells["approve_flag"].Value = "False";
                    //SaveChanged();

                }
                return;

            }

            if ("数量5" == dgv.CurrentCell.OwningColumn.Name && dgv.CurrentRow.Cells["追加类别5"].Value.ToString() == "代料")
            {

                MessageBox.Show("代料领料请在原单数量里输入被代料数量,系统会自动按比例计算代料数量...", "注意");
                return;
            }

            ////////////////////////////////////////////

            if ("原单数量5" == dgv.CurrentCell.OwningColumn.Name && dgv.CurrentRow.Cells["追加类别5"].Value.ToString() == "代料")
            {

                ChangeValue queryForm = new ChangeValue();

                queryForm.OldValue = dgv.CurrentCell.Value.ToString();
                queryForm.NewValue = "";
                queryForm.ChangeMode = "value";
                queryForm.ShowDialog();

                if (queryForm.NewValue != "")
                {
                    dgv.CurrentRow.Cells["原单数量5"].Value = queryForm.NewValue;
                    //dgv.CurrentRow.Cells["discount_money"].Value = DBNull.Value;

                    //dgv.CurrentRow.Cells["approve_flag"].Value = "False";
                    this.ly_production_order_materialrequisitionDataGridView.EndEdit();
                    this.ly_production_order_materialrequisitionBindingSource.EndEdit();

                    this.ly_production_order_materialrequisitionTableAdapter.Update(this.lYMaterielRequirements.ly_production_order_materialrequisition);


                    //CountPlanStru();

                }
                else
                {
                    //hT_Manage_ItemDataGridView.CurrentRow.Cells["apply_money"].Value = queryForm.NewValue;
                    //dgv.CurrentRow.Cells["discount_money"].Value = DBNull.Value;
                    //dgv.CurrentRow.Cells["apply_money"].Value = DBNull.Value;
                    //dgv.CurrentRow.Cells["approve_flag"].Value = "False";
                    //SaveChanged();

                }
                return;

            }


            ///////////////////////////////////////////////////////


            ///////////////////////////////////////////////////////

            if ("安排日期5" == dgv.CurrentCell.OwningColumn.Name)
            {


                ChangeValue queryForm = new ChangeValue();

                queryForm.OldValue = dgv.CurrentCell.Value.ToString();
                queryForm.NewValue = "";
                queryForm.ChangeMode = "datetime";
                queryForm.ShowDialog();




                if (queryForm.NewValue != "")
                {
                    dgv.CurrentRow.Cells["安排日期5"].Value = queryForm.NewValue;
                    //dgv.CurrentRow.Cells["discount_money"].Value = DBNull.Value;

                    //dgv.CurrentRow.Cells["approve_flag"].Value = "False";
                    this.ly_production_order_materialrequisitionDataGridView.EndEdit();
                    this.ly_production_order_materialrequisitionBindingSource.EndEdit();

                    this.ly_production_order_materialrequisitionTableAdapter.Update(this.lYMaterielRequirements.ly_production_order_materialrequisition);




                    //CountPlanStru();

                }
                else
                {


                }
                return;
            }



            ///////////////////////////////////////////////////////

            if ("备注5" == dgv.CurrentCell.OwningColumn.Name)
            {

                ChangeValue queryForm = new ChangeValue();

                queryForm.OldValue = dgv.CurrentCell.Value.ToString();
                queryForm.NewValue = "";
                queryForm.ChangeMode = "longstring";
                queryForm.ShowDialog();




                if (queryForm.NewValue != "")
                {
                    dgv.CurrentRow.Cells["备注5"].Value = queryForm.NewValue;
                    //dgv.CurrentRow.Cells["discount_money"].Value = DBNull.Value;

                    //dgv.CurrentRow.Cells["approve_flag"].Value = "False";
                    this.ly_production_order_materialrequisitionDataGridView.EndEdit();
                    this.ly_production_order_materialrequisitionBindingSource.EndEdit();

                    this.ly_production_order_materialrequisitionTableAdapter.Update(this.lYMaterielRequirements.ly_production_order_materialrequisition);




                    //CountPlanStru();

                }
                else
                {

                }
                return;

            }

            ///////////////////////////////////////////////////////

            if ("批准意见5" == dgv.CurrentCell.OwningColumn.Name)
            {

                ChangeValue queryForm = new ChangeValue();

                queryForm.OldValue = dgv.CurrentCell.Value.ToString();
                queryForm.NewValue = "";
                queryForm.ChangeMode = "longstring";
                queryForm.ShowDialog();




                if (queryForm.NewValue != "")
                {



                    if (!SQLDatabase.CheckHaveRight(SQLDatabase.NowUserID, "追加领料批准"))
                    {
                        MessageBox.Show("无追加领料批准权限,操作取消...", "注意");
                        return;

                    }
                    dgv.CurrentRow.Cells["批准意见5"].Value = queryForm.NewValue;
                    //dgv.CurrentRow.Cells["discount_money"].Value = DBNull.Value;

                    //dgv.CurrentRow.Cells["approve_flag"].Value = "False";
                    this.ly_production_order_materialrequisitionDataGridView.EndEdit();
                    this.ly_production_order_materialrequisitionBindingSource.EndEdit();

                    this.ly_production_order_materialrequisitionTableAdapter.Update(this.lYMaterielRequirements.ly_production_order_materialrequisition);




                    //CountPlanStru();

                }
                else
                {

                }
                return;

            }

            /////////////////////////////

            if ("批准5" == dgv.CurrentCell.OwningColumn.Name)
            {
                if (!SQLDatabase.CheckHaveRight(SQLDatabase.NowUserID, "追加领料批准"))
                {
                    MessageBox.Show("无追加领料批准权限,操作取消...", "注意");
                    return;

                }

                if ("True" == dgv.CurrentRow.Cells["批准5"].Value.ToString())
                {
                    dgv.CurrentRow.Cells["批准5"].Value = "False";
                    dgv.CurrentRow.Cells["批准人5"].Value = DBNull.Value;

                }
                else
                {

                    dgv.CurrentRow.Cells["批准5"].Value = "True";
                    dgv.CurrentRow.Cells["批准人5"].Value = SQLDatabase.nowUserName();
                }



                this.ly_production_order_materialrequisitionDataGridView.EndEdit();
                this.ly_production_order_materialrequisitionBindingSource.EndEdit();

                this.ly_production_order_materialrequisitionTableAdapter.Update(this.lYMaterielRequirements.ly_production_order_materialrequisition);



                return;
            }


            ///////////////////////////////////////////////////////

            if ("ELSE" != nowgeometry)
            {
                ///////////////////////////////////////////////////

                //if ("diameter" == dgv.CurrentCell.OwningColumn.Name)
                //{

                //    if ("ELSE" == nowgeometry) return;
                //    if ("棒料" != nowgeometry) return;



                //    ChangeValue queryForm = new ChangeValue();

                //    queryForm.OldValue = dgv.CurrentCell.Value.ToString();
                //    queryForm.NewValue = "";
                //    queryForm.ChangeMode = "value";
                //    queryForm.ShowDialog();




                //    if (queryForm.NewValue != "")
                //    {
                //        dgv.CurrentRow.Cells["diameter"].Value = queryForm.NewValue;
                //        //dgv.CurrentRow.Cells["discount_money"].Value = DBNull.Value;

                //        //dgv.CurrentRow.Cells["approve_flag"].Value = "False";


                //        SetUseNum(nowgeometry);



                //        this.ly_production_order_materialrequisitionDataGridView.EndEdit();
                //        this.ly_production_order_materialrequisitionBindingSource.EndEdit();

                //        this.ly_production_order_materialrequisitionTableAdapter.Update(this.lYMaterielRequirements.ly_production_order_materialrequisition);





                //    }
                //    else
                //    {
                //        //hT_Manage_ItemDataGridView.CurrentRow.Cells["apply_money"].Value = queryForm.NewValue;
                //        //dgv.CurrentRow.Cells["discount_money"].Value = DBNull.Value;
                //        //dgv.CurrentRow.Cells["apply_money"].Value = DBNull.Value;
                //        //dgv.CurrentRow.Cells["approve_flag"].Value = "False";
                //        //SaveChanged();

                //    }
                //    return;
                //}



                ///////////////////////////////////////////////////////

                ///////////////////////////////////////////////////

                if ("length" == dgv.CurrentCell.OwningColumn.Name)
                {

                    if ("ELSE" == nowgeometry) return;
                    //if ("圆柱体" != nowgeometry) return;

                    ChangeValue queryForm = new ChangeValue();

                    queryForm.OldValue = dgv.CurrentCell.Value.ToString();
                    queryForm.NewValue = "";
                    queryForm.ChangeMode = "value";
                    queryForm.ShowDialog();




                    if (queryForm.NewValue != "")
                    {
                        dgv.CurrentRow.Cells["length"].Value = queryForm.NewValue;
                        //dgv.CurrentRow.Cells["discount_money"].Value = DBNull.Value;
                        SetUseNum(nowgeometry);
                        //dgv.CurrentRow.Cells["approve_flag"].Value = "False";
                        this.ly_production_order_materialrequisitionDataGridView.EndEdit();
                        this.ly_production_order_materialrequisitionBindingSource.EndEdit();

                        this.ly_production_order_materialrequisitionTableAdapter.Update(this.lYMaterielRequirements.ly_production_order_materialrequisition);


                    }
                    else
                    {
                        //hT_Manage_ItemDataGridView.CurrentRow.Cells["apply_money"].Value = queryForm.NewValue;
                        //dgv.CurrentRow.Cells["discount_money"].Value = DBNull.Value;
                        //dgv.CurrentRow.Cells["apply_money"].Value = DBNull.Value;
                        //dgv.CurrentRow.Cells["approve_flag"].Value = "False";
                        //SaveChanged();

                    }
                    return;
                }



                /////////////////////////////////////////////////////
                /////////////////////////////////////////////////

                if ("width" == dgv.CurrentCell.OwningColumn.Name)
                {
                    if ("ELSE" == nowgeometry) return;
                    if ("板料" != nowgeometry) return;

                    ChangeValue queryForm = new ChangeValue();

                    queryForm.OldValue = dgv.CurrentCell.Value.ToString();
                    queryForm.NewValue = "";
                    queryForm.ChangeMode = "value";
                    queryForm.ShowDialog();




                    if (queryForm.NewValue != "")
                    {
                        dgv.CurrentRow.Cells["width"].Value = queryForm.NewValue;
                        //dgv.CurrentRow.Cells["discount_money"].Value = DBNull.Value;
                        SetUseNum(nowgeometry);
                        //dgv.CurrentRow.Cells["approve_flag"].Value = "False";
                        this.ly_production_order_materialrequisitionDataGridView.EndEdit();
                        this.ly_production_order_materialrequisitionBindingSource.EndEdit();

                        this.ly_production_order_materialrequisitionTableAdapter.Update(this.lYMaterielRequirements.ly_production_order_materialrequisition);


                    }
                    else
                    {
                        //hT_Manage_ItemDataGridView.CurrentRow.Cells["apply_money"].Value = queryForm.NewValue;
                        //dgv.CurrentRow.Cells["discount_money"].Value = DBNull.Value;
                        //dgv.CurrentRow.Cells["apply_money"].Value = DBNull.Value;
                        //dgv.CurrentRow.Cells["approve_flag"].Value = "False";
                        //SaveChanged();

                    }
                    return;
                }



                /////////////////////////////////////////////////////
                /////////////////////////////////////////////////

                if ("height" == dgv.CurrentCell.OwningColumn.Name)
                {

                    if ("ELSE" == nowgeometry) return;
                    if ("板料" != nowgeometry) return;
                    if ("True" != this.ly_production_order_materialrequisitionDataGridView.CurrentRow.Cells["追加5"].Value.ToString()) return;

                    ChangeValue queryForm = new ChangeValue();

                    queryForm.OldValue = dgv.CurrentCell.Value.ToString();
                    queryForm.NewValue = "";
                    queryForm.ChangeMode = "value";
                    queryForm.ShowDialog();


                    if (queryForm.NewValue != "")
                    {
                        dgv.CurrentRow.Cells["height"].Value = queryForm.NewValue;
                        //dgv.CurrentRow.Cells["discount_money"].Value = DBNull.Value;
                        SetUseNum(nowgeometry);
                        //dgv.CurrentRow.Cells["approve_flag"].Value = "False";
                        this.ly_production_order_materialrequisitionDataGridView.EndEdit();
                        this.ly_production_order_materialrequisitionBindingSource.EndEdit();

                        this.ly_production_order_materialrequisitionTableAdapter.Update(this.lYMaterielRequirements.ly_production_order_materialrequisition);


                    }
                    else
                    {
                        //hT_Manage_ItemDataGridView.CurrentRow.Cells["apply_money"].Value = queryForm.NewValue;
                        //dgv.CurrentRow.Cells["discount_money"].Value = DBNull.Value;
                        //dgv.CurrentRow.Cells["apply_money"].Value = DBNull.Value;
                        //dgv.CurrentRow.Cells["approve_flag"].Value = "False";
                        //SaveChanged();

                    }
                    return;
                }

                /////////////////////////////////////////////////

                if ("diameter" == dgv.CurrentCell.OwningColumn.Name)
                {

                    if ("ELSE" == nowgeometry) return;
                    if ("棒料" != nowgeometry) return;
                    if ("True" != this.ly_production_order_materialrequisitionDataGridView.CurrentRow.Cells["追加5"].Value.ToString()) return;

                    ChangeValue queryForm = new ChangeValue();

                    queryForm.OldValue = dgv.CurrentCell.Value.ToString();
                    queryForm.NewValue = "";
                    queryForm.ChangeMode = "value";
                    queryForm.ShowDialog();




                    if (queryForm.NewValue != "")
                    {
                        dgv.CurrentRow.Cells["diameter"].Value = queryForm.NewValue;
                        //dgv.CurrentRow.Cells["discount_money"].Value = DBNull.Value;
                        SetUseNum(nowgeometry);
                        //dgv.CurrentRow.Cells["approve_flag"].Value = "False";
                        this.ly_production_order_materialrequisitionDataGridView.EndEdit();
                        this.ly_production_order_materialrequisitionBindingSource.EndEdit();

                        this.ly_production_order_materialrequisitionTableAdapter.Update(this.lYMaterielRequirements.ly_production_order_materialrequisition);



                    }
                    else
                    {
                        //hT_Manage_ItemDataGridView.CurrentRow.Cells["apply_money"].Value = queryForm.NewValue;
                        //dgv.CurrentRow.Cells["discount_money"].Value = DBNull.Value;
                        //dgv.CurrentRow.Cells["apply_money"].Value = DBNull.Value;
                        //dgv.CurrentRow.Cells["approve_flag"].Value = "False";
                        //SaveChanged();

                    }
                    return;
                }


                /////////////////////////////////////////////////////

                /////////////////////////////////////////////////

                if ("out_count" == dgv.CurrentCell.OwningColumn.Name)
                {

                    if ("ELSE" == nowgeometry) return;


                    ChangeValue queryForm = new ChangeValue();

                    queryForm.OldValue = dgv.CurrentCell.Value.ToString();
                    queryForm.NewValue = "";
                    queryForm.ChangeMode = "value";
                    queryForm.ShowDialog();




                    if (queryForm.NewValue != "")
                    {
                        dgv.CurrentRow.Cells["out_count"].Value = queryForm.NewValue;
                        //dgv.CurrentRow.Cells["discount_money"].Value = DBNull.Value;
                        SetUseNum(nowgeometry);
                        //dgv.CurrentRow.Cells["approve_flag"].Value = "False";
                        this.ly_production_order_materialrequisitionDataGridView.EndEdit();
                        this.ly_production_order_materialrequisitionBindingSource.EndEdit();

                        this.ly_production_order_materialrequisitionTableAdapter.Update(this.lYMaterielRequirements.ly_production_order_materialrequisition);



                    }
                    else
                    {
                        //hT_Manage_ItemDataGridView.CurrentRow.Cells["apply_money"].Value = queryForm.NewValue;
                        //dgv.CurrentRow.Cells["discount_money"].Value = DBNull.Value;
                        //dgv.CurrentRow.Cells["apply_money"].Value = DBNull.Value;
                        //dgv.CurrentRow.Cells["approve_flag"].Value = "False";
                        //SaveChanged();

                    }
                    return;
                }



                /////////////////////////////////////////////////////

            }

            /////////////////////////////////////////////////////////
        }

        private void SetUseNum(string nowgeometry)
        {

            decimal nowspecific_weight;
            decimal nowdiameter;
            decimal nowlength;
            decimal nowwidth;
            decimal nowheight;
            decimal nowoutcount;
            decimal qtysetwaste;



            if (!string.IsNullOrEmpty(this.ly_production_order_materialrequisitionDataGridView.CurrentRow.Cells["diameter"].Value.ToString()))
            {
                nowdiameter = decimal.Parse(this.ly_production_order_materialrequisitionDataGridView.CurrentRow.Cells["diameter"].Value.ToString());
            }
            else
            {
                nowdiameter = 0;
            }

            if (!string.IsNullOrEmpty(this.ly_production_order_materialrequisitionDataGridView.CurrentRow.Cells["specific_weight"].Value.ToString()))
            {
                nowspecific_weight = decimal.Parse(this.ly_production_order_materialrequisitionDataGridView.CurrentRow.Cells["specific_weight"].Value.ToString());
            }
            else
            {
                nowspecific_weight = 0;
            }

            if (!string.IsNullOrEmpty(this.ly_production_order_materialrequisitionDataGridView.CurrentRow.Cells["length"].Value.ToString()))
            {
                nowlength = decimal.Parse(this.ly_production_order_materialrequisitionDataGridView.CurrentRow.Cells["length"].Value.ToString());
            }
            else
            {
                nowlength = 0;
            }

            if (!string.IsNullOrEmpty(this.ly_production_order_materialrequisitionDataGridView.CurrentRow.Cells["width"].Value.ToString()))
            {
                nowwidth = decimal.Parse(this.ly_production_order_materialrequisitionDataGridView.CurrentRow.Cells["width"].Value.ToString());
            }
            else
            {
                nowwidth = 0;
            }

            if (!string.IsNullOrEmpty(this.ly_production_order_materialrequisitionDataGridView.CurrentRow.Cells["height"].Value.ToString()))
            {
                nowheight = decimal.Parse(this.ly_production_order_materialrequisitionDataGridView.CurrentRow.Cells["height"].Value.ToString());
            }
            else
            {
                nowheight = 0;
            }

            if (!string.IsNullOrEmpty(this.ly_production_order_materialrequisitionDataGridView.CurrentRow.Cells["out_count"].Value.ToString()))
            {
                nowoutcount = decimal.Parse(this.ly_production_order_materialrequisitionDataGridView.CurrentRow.Cells["out_count"].Value.ToString());
            }
            else
            {
                nowoutcount = -1;
            }

            if (!string.IsNullOrEmpty(this.ly_production_order_materialrequisitionDataGridView.CurrentRow.Cells["qty_set_waste"].Value.ToString()))
            {
                qtysetwaste = 100 + decimal.Parse(this.ly_production_order_materialrequisitionDataGridView.CurrentRow.Cells["qty_set_waste"].Value.ToString());
            }
            else
            {
                qtysetwaste = 100;
            }


            if ("棒料" == nowgeometry)
            {
                this.ly_production_order_materialrequisitionDataGridView.CurrentRow.Cells["数量5"].Value = decimal.Parse("3.1415927") * nowdiameter * nowdiameter * nowlength / 4 * nowspecific_weight / 1000 / 1000 * nowoutcount * qtysetwaste / 100;
                //this.ly_production_order_materialrequisitionDataGridView.CurrentRow.Cells["数量5"].Value = decimal.Parse("3.1415927") * nowdiameter * nowdiameter * nowlength / 4 * nowspecific_weight / 1000 / 1000 * nowoutcount * 100 / 100;

            }

            if ("板料" == nowgeometry)
            {
                this.ly_production_order_materialrequisitionDataGridView.CurrentRow.Cells["数量5"].Value = nowlength * nowwidth * nowheight * nowspecific_weight / 1000 / 1000 * nowoutcount * qtysetwaste / 100;
                //this.ly_production_order_materialrequisitionDataGridView.CurrentRow.Cells["数量5"].Value = nowlength * nowwidth * nowheight * nowspecific_weight / 1000 / 1000 * nowoutcount * 100 / 100;

            }
        }

        private void toolStripButton12_Click(object sender, EventArgs e)
        {
            LY_Production_order_Mange queryForm = new LY_Production_order_Mange();



            queryForm.OwnerForm = this;
            //queryForm.runmode = "增加";
            //queryForm.statemode = "原料";

            queryForm.StartPosition = FormStartPosition.CenterParent;
            queryForm.ShowDialog(this);

            //queryForm.ShowDialog();


            if (queryForm.DialogResult != DialogResult.Cancel)
            {
                //this.ly_inma0010ylTableAdapter.Fill(this.lYMaterialMange.ly_inma0010yl);
                //this.ly_inma0010ylBindingSource.Position = this.ly_inma0010ylBindingSource.Find("物资编号", queryForm.material_code);
            }

 
        }

      

        public void find_NowProduc(int nowplanid, string nowitemno, string nowproduction_order)
        {
            this.ly_material_plan_mainBindingSource.Position = this.ly_material_plan_mainBindingSource.Find("id", nowplanid);



            this.lY_MaterielRequirementsExecuteTableAdapter.Fill(this.lYMaterielRequirements.LY_MaterielRequirementsExecute, nowplanid, "机加");
            this.lY_MaterielRequirementsExecuteBindingSource.Position = this.lY_MaterielRequirementsExecuteBindingSource.Find("物料编码", nowitemno);

            this.ly_production_orderTableAdapter.Fill(this.lYMaterielRequirements.ly_production_order, nowplanid, nowitemno);

            this.ly_production_orderBindingSource.Position = this.ly_production_orderBindingSource.Find("跟单编号", nowproduction_order);



        }

        //////////////////////////////////////

        public void find_NowProduc(int nowplanid, string nowitemno, string nowproduction_order, int nowprocess_order)
        {

            find_NowProduc(nowplanid, nowitemno, nowproduction_order);

            this.ly_machinepart_process_workBindingSource.Position = this.ly_machinepart_process_workBindingSource.Find("工序编号", nowprocess_order);


        }

        private void toolStripTextBox1_KeyUp(object sender, KeyEventArgs e)
        {
            string filterString = GetDataGridviewMultiFilterString.DGVMultiFilterString(this.lY_MaterielRequirementsDataGridView, this.toolStripTextBox1.Text);



            if (null == filterString)
                filterString = "";

            this.lY_MaterielRequirementsExecuteBindingSource.Filter = filterString;
        }

        private void toolStripTextBox1_Enter(object sender, EventArgs e)
        {
            toolStripTextBox1.Text = "";

            this.lY_MaterielRequirementsExecuteBindingSource.Filter = "";
        }

        private void SetDisplayColumn(DataGridView sender)
        {
            DataGridView dgv = sender;

            if (null == dgv.CurrentRow) return;

            string nowgeometry;

            if (!string.IsNullOrEmpty(dgv.CurrentRow.Cells["geometry"].Value.ToString()))
            {
                nowgeometry = dgv.CurrentRow.Cells["geometry"].Value.ToString();
            }
            else
            {
                nowgeometry = "ELSE";
            }

            if ("ELSE" == nowgeometry)
            {
                dgv.Columns["diameter"].Visible = false;
                dgv.Columns["width"].Visible = false;
                dgv.Columns["length"].Visible = false;
                dgv.Columns["height"].Visible = false;
                dgv.Columns["input_count"].Visible = false;
                dgv.Columns["specific_weight"].Visible = false;
                dgv.Columns["out_count"].Visible = false;
                dgv.Columns["tec_qty"].Visible = false;


            }

            if ("棒料" == nowgeometry)
            {
                dgv.Columns["diameter"].Visible = true;
                dgv.Columns["width"].Visible = false;
                dgv.Columns["length"].Visible = true;
                dgv.Columns["height"].Visible = false;
                dgv.Columns["input_count"].Visible = true;
                dgv.Columns["specific_weight"].Visible = true;
                dgv.Columns["out_count"].Visible = true;
                dgv.Columns["tec_qty"].Visible = true;
            }

            if ("板料" == nowgeometry)
            {
                dgv.Columns["diameter"].Visible = false;
                dgv.Columns["width"].Visible = true;
                dgv.Columns["length"].Visible = true;
                dgv.Columns["height"].Visible = true;
                dgv.Columns["input_count"].Visible = true;
                dgv.Columns["specific_weight"].Visible = true;
                dgv.Columns["out_count"].Visible = true;
                dgv.Columns["tec_qty"].Visible = true;
            }
        }

        private void ly_production_order_materialrequisitionDataGridView_SelectionChanged(object sender, EventArgs e)
        {
            SetDisplayColumn(sender as DataGridView);
        }

        private void tabControl2_SelectedIndexChanged(object sender, EventArgs e)
        {
            TabControl tbc = sender as TabControl;

            if (1 != tbc.SelectedIndex) return;
            if (null != this.ly_production_orderDataGridView.CurrentRow)
            {

                string nowitem = this.ly_production_orderDataGridView.CurrentRow.Cells["物料编码"].Value.ToString();
                string nowproductionorderNum = this.ly_production_orderDataGridView.CurrentRow.Cells["跟单编号"].Value.ToString();


                this.ly_production_order_materialrequisitionTableAdapter.Fill(this.lYMaterielRequirements.ly_production_order_materialrequisition, nowproductionorderNum);
                //set_processOrder_Num();


            }
            else
            {
                //this.ly_machinepart_process_workTableAdapter.Fill(this.lYMaterielRequirements.ly_machinepart_process_work, "", "");

                //this.ly_production_order_detail1TableAdapter.Fill(this.lYMaterielRequirements.ly_production_order_detail1, "", 0);
                //this.ly_production_order_materialrequisitionTableAdapter.Fill(this.lYMaterielRequirements.ly_production_order_materialrequisition, "");

            }
        }

        private void toolStripButton18_Click(object sender, EventArgs e)
        {
            if (this.ly_machinepart_process_workDataGridView.Rows.Count <= 0)
            {
                MessageBox.Show("无工序...", "注意");
                return;

            }
            if (this.ly_production_order_materialrequisitionDataGridView.Rows.Count <= 0)
            {
                BaseReportView queryForm = new BaseReportView();

                queryForm.Printdata = this.lYMaterielRequirements;
                queryForm.PrintCrystalReport = new LY_task_rep2();

                queryForm.ShowDialog();

            }
            else
            {
                BaseReportView queryForm = new BaseReportView();

                queryForm.Printdata = this.lYMaterielRequirements;
                queryForm.PrintCrystalReport = new ly_task_rep();

                queryForm.ShowDialog();
            }
        }

        private void ly_production_order_detailDataGridView_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
          
        }

        private void ly_production_order_detailDataGridView_SelectionChanged(object sender, EventArgs e)
        {
            if (null == this.ly_production_order_detailDataGridView.CurrentRow)
            {
                this.ly_production_order_inspectionTableAdapter.Fill(this.lYQualityInspector.ly_production_order_inspection, -1);
                return;
            }

            int detailId = int.Parse(this.ly_production_order_detailDataGridView.CurrentRow.Cells["detailId"].Value.ToString());

            this.ly_production_order_inspectionTableAdapter.Fill(this.lYQualityInspector.ly_production_order_inspection, detailId);
        }

        private void toolStripButton22_Click(object sender, EventArgs e)
        {
            if (null == ly_production_order_detailDataGridView.CurrentRow) return;
            string nowproductionorderNum = this.ly_production_orderDataGridView.CurrentRow.Cells["跟单编号"].Value.ToString();
            if (ly_production_order_detailDataGridView.CurrentRow.Cells["外协"].Value.ToString() == "True")
            {
                
                    MessageBox.Show("此序为外协，不能自检...", "注意");
                    return;
               
            }

            if (ly_production_order_detailDataGridView.CurrentRow.Cells["工号"].Value.ToString() == "019")
            {

                MessageBox.Show("此序工号为019，不能自检...", "注意");
                return;

            }

         


            string nowordername = this.ly_machinepart_process_workDataGridView.CurrentRow.Cells["工序名称"].Value.ToString(); 

            string message = "增加检验记录吗？";
            string caption = "提示...";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;

            DialogResult result;



            result = MessageBox.Show(message, caption, buttons,
            MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

            if (result == DialogResult.Yes)
            {


                decimal inspect_count;

                if (!string.IsNullOrEmpty(this.ly_production_order_detailDataGridView.CurrentRow.Cells["数量1"].Value.ToString()))
                {
                    inspect_count = decimal.Parse(this.ly_production_order_detailDataGridView.CurrentRow.Cells["数量1"].Value.ToString());
                }
                else
                {
                    inspect_count = 0;
                }


                decimal send_inspect_count = 0;

                if (!string.IsNullOrEmpty(nowproductionorderNum))
                {

                    foreach (DataGridViewRow dgr in ly_production_order_inspectionDataGridView.Rows)
                    {

                        if (string.IsNullOrEmpty(dgr.Cells["送检"].Value.ToString())) continue;
                        send_inspect_count = send_inspect_count + decimal.Parse(dgr.Cells["送检"].Value.ToString());



                    }
                }



                if (send_inspect_count >= inspect_count)
                {
                    MessageBox.Show("加工数量已经全部送交检验,不能增加检测记录", "注意");

                    return;
                }
                else
                {

                    this.ly_production_order_inspectionBindingSource.AddNew();

                    this.ly_production_order_inspectionDataGridView.CurrentRow.Cells["质检单号"].Value = GetMaxInspection();

                    int detailId = int.Parse(this.ly_production_order_detailDataGridView.CurrentRow.Cells["detailId"].Value.ToString());
                    this.ly_production_order_inspectionDataGridView.CurrentRow.Cells["detail_id_in"].Value = detailId;
                    this.ly_production_order_inspectionDataGridView.CurrentRow.Cells["检验日期"].Value = SQLDatabase.GetNowdate().ToString(); 
                    this.ly_production_order_inspectionDataGridView.CurrentRow.Cells["检查员"].Value = SQLDatabase.nowUserName();
                    this.ly_production_order_inspectionDataGridView.CurrentRow.Cells["送检"].Value = inspect_count - send_inspect_count;
                    this.ly_production_order_inspectionDataGridView.CurrentRow.Cells["收料数量"].Value = inspect_count - send_inspect_count;

                }
                 

                SaveChangedZJ(); 

            }
        }
        private void SaveChangedZJ()
        {

            this.ly_production_order_inspectionDataGridView.EndEdit();

            this.Validate();
            this.ly_production_order_inspectionBindingSource.EndEdit();

            try
            {
                this.ly_production_order_inspectionTableAdapter.Update(this.lYQualityInspector.ly_production_order_inspection);
            }
            catch (SqlException sqle)
            {
                MessageBox.Show(sqle.Message, "注意");
            }
            int detailId = int.Parse(this.ly_production_order_detailDataGridView.CurrentRow.Cells["detailId"].Value.ToString());
            int orderNum = int.Parse(this.ly_machinepart_process_workDataGridView.CurrentRow.Cells["工序编号"].Value.ToString());

            if (null != this.ly_production_orderDataGridView.CurrentRow)
            {

                string nowitem = this.ly_production_orderDataGridView.CurrentRow.Cells["物料编码"].Value.ToString();
                string nowproductionorderNum = this.ly_production_orderDataGridView.CurrentRow.Cells["跟单编号"].Value.ToString(); 
                this.ly_machinepart_process_workTableAdapter.Fill(this.lYMaterielRequirements.ly_machinepart_process_work, nowitem, nowproductionorderNum); 
                this.ly_production_order_detail1TableAdapter.Fill(this.lYMaterielRequirements.ly_production_order_detail1, nowproductionorderNum, 0); 
                this.ly_production_order_materialrequisitionTableAdapter.Fill(this.lYMaterielRequirements.ly_production_order_materialrequisition, nowproductionorderNum);
        
            }
            else
            {
                this.ly_machinepart_process_workTableAdapter.Fill(this.lYMaterielRequirements.ly_machinepart_process_work, "", "");
                this.ly_production_order_detail1TableAdapter.Fill(this.lYMaterielRequirements.ly_production_order_detail1, "", 0);
                this.ly_production_order_materialrequisitionTableAdapter.Fill(this.lYMaterielRequirements.ly_production_order_materialrequisition, "");

            } 
            this.ly_machinepart_process_workBindingSource.Position = this.ly_machinepart_process_workBindingSource.Find("工序编号", orderNum);
            this.ly_production_order_detailBindingSource.Position = this.ly_production_order_detailBindingSource.Find("id", detailId);


        }



        private string GetMaxInspection()
        { 
            SqlConnection sqlConnection1 = new SqlConnection(SQLDatabase.Connectstring);
            SqlCommand cmd = new SqlCommand(); 
            string MaxInspectionNum = ""; 
            cmd.Parameters.Add("@Production_mode", SqlDbType.VarChar);
            cmd.Parameters["@Production_mode"].Value = "JYJG"; 

            cmd.CommandText = "LY_GetMax_InspectionNum";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = sqlConnection1;

            sqlConnection1.Open();
            MaxInspectionNum = cmd.ExecuteScalar().ToString();
            sqlConnection1.Close(); 

            return MaxInspectionNum;
        }

        private void toolStripButton23_Click(object sender, EventArgs e)
        {
            if (null == this.ly_production_order_inspectionDataGridView.CurrentRow || ly_machinepart_process_workDataGridView.CurrentRow==null) return; 

            string nowordername = this.ly_machinepart_process_workDataGridView.CurrentRow.Cells["工序名称"].Value.ToString();  
            string inspector = this.ly_production_order_inspectionDataGridView.CurrentRow.Cells["检查员"].Value.ToString();

            if (inspector != SQLDatabase.nowUserName())
            {
                MessageBox.Show("请检查员:" + inspector + "删除", "注意");
                return;
            }

            int m = int.Parse(ly_machinepart_process_workDataGridView.CurrentRow.Cells["工序编号"].Value.ToString());//当前工序编号
            int n = 0;
            for (int i = 0; i < ly_machinepart_process_workDataGridView.Rows.Count; i++)
            {
                if (int.Parse(ly_machinepart_process_workDataGridView.Rows[i].Cells["工序编号"].Value.ToString()) > m)//当前工序后续工序流程
                {
                    if (int.Parse(ly_machinepart_process_workDataGridView.Rows[i].Cells["本序合格"].Value.ToString()) > 0)
                    {
                        n++;
                        break;
                    }
                }
            }
            if (n > 0)
            {
                MessageBox.Show("已经有下一序合格数,无法删除", "注意");
                return;
            }

            string message1 = "当前检验记录将被删除，继续吗？";
            string caption1 = "提示...";
            MessageBoxButtons buttons1 = MessageBoxButtons.YesNo; 
            DialogResult result1; 
            result1 = MessageBox.Show(message1, caption1, buttons1,
            MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

            if (result1 == DialogResult.Yes)
            { 
                this.ly_production_order_inspectionBindingSource.RemoveCurrent();
                SaveChangedZJ(); 
            }
        }


        private bool CheckInputRec(DataGridView dgv)
        {
            decimal qualified_count;
            decimal inspect_count;
            decimal canuse_count;
            decimal remake_count;

            if (!string.IsNullOrEmpty(dgv.CurrentRow.Cells["返修合格_1"].Value.ToString()))
            {
                remake_count = decimal.Parse(dgv.CurrentRow.Cells["返修合格_1"].Value.ToString());
            }
            else
            {
                remake_count = 0;
            }


            if (!string.IsNullOrEmpty(dgv.CurrentRow.Cells["收料数量"].Value.ToString()))
            {
                inspect_count = decimal.Parse(dgv.CurrentRow.Cells["收料数量"].Value.ToString());
            }
            else
            {
                inspect_count = 0;
            }

            if (!string.IsNullOrEmpty(dgv.CurrentRow.Cells["合格"].Value.ToString()))
            {
                qualified_count = decimal.Parse(dgv.CurrentRow.Cells["合格"].Value.ToString());
            }
            else
            {
                qualified_count = 0;
            }

            if (!string.IsNullOrEmpty(dgv.CurrentRow.Cells["可用"].Value.ToString()))
            {
                canuse_count = decimal.Parse(dgv.CurrentRow.Cells["可用"].Value.ToString());
            }
            else
            {
                canuse_count = 0;
            }

            if ((qualified_count + canuse_count + remake_count) > inspect_count)
            {
                MessageBox.Show("合格数量不能大于收料数量", "注意");

                return false;
            }
            else
            {
                dgv.CurrentRow.Cells["废品"].Value = inspect_count - (qualified_count + canuse_count + remake_count);
                return true;
            }
        }
        private bool CheckInput(DataGridView dgv)
        {
            decimal qualified_count;
            decimal inspect_count;
            decimal canuse_count;
            decimal remake_count;

            if (!string.IsNullOrEmpty(dgv.CurrentRow.Cells["返修合格_1"].Value.ToString()))
            {
                remake_count = decimal.Parse(dgv.CurrentRow.Cells["返修合格_1"].Value.ToString());
            }
            else
            {
                remake_count = 0;
            }


            if (!string.IsNullOrEmpty(dgv.CurrentRow.Cells["送检"].Value.ToString()))
            {
                inspect_count = decimal.Parse(dgv.CurrentRow.Cells["送检"].Value.ToString());
            }
            else
            {
                inspect_count = 0;
            }

            if (!string.IsNullOrEmpty(dgv.CurrentRow.Cells["合格"].Value.ToString()))
            {
                qualified_count = decimal.Parse(dgv.CurrentRow.Cells["合格"].Value.ToString());
            }
            else
            {
                qualified_count = 0;
            }

            if (!string.IsNullOrEmpty(dgv.CurrentRow.Cells["可用"].Value.ToString()))
            {
                canuse_count = decimal.Parse(dgv.CurrentRow.Cells["可用"].Value.ToString());
            }
            else
            {
                canuse_count = 0;
            }

            if ((qualified_count + canuse_count + remake_count) > inspect_count)
            {
                MessageBox.Show("合格数量不能大于送检数量", "注意");

                return false;
            }
            else
            {
                dgv.CurrentRow.Cells["废品"].Value = inspect_count - (qualified_count + canuse_count + remake_count);
                return true;
            }
        }

        private bool CheckCanuse(DataGridView dgv)
        {
            decimal qualified_count;
            decimal inspect_count;

            decimal remake_count;

            if (!string.IsNullOrEmpty(dgv.CurrentRow.Cells["返修合格_1"].Value.ToString()))
            {
                remake_count = decimal.Parse(dgv.CurrentRow.Cells["返修合格_1"].Value.ToString());
            }
            else
            {
                remake_count = 0;
            }

            if (!string.IsNullOrEmpty(dgv.CurrentRow.Cells["送检"].Value.ToString()))
            {
                inspect_count = decimal.Parse(dgv.CurrentRow.Cells["送检"].Value.ToString());
            }
            else
            {
                inspect_count = 0;
            }

            if (!string.IsNullOrEmpty(dgv.CurrentRow.Cells["合格"].Value.ToString()))
            {
                qualified_count = decimal.Parse(dgv.CurrentRow.Cells["合格"].Value.ToString());
            }
            else
            {
                qualified_count = 0;
            }



            decimal waste_count;
            decimal canuse_count;


            if (!string.IsNullOrEmpty(dgv.CurrentRow.Cells["废品"].Value.ToString()))
            {
                waste_count = decimal.Parse(dgv.CurrentRow.Cells["废品"].Value.ToString());
            }
            else
            {
                waste_count = 0;
            }

            if (!string.IsNullOrEmpty(dgv.CurrentRow.Cells["可用"].Value.ToString()))
            {
                canuse_count = decimal.Parse(dgv.CurrentRow.Cells["可用"].Value.ToString());
            }
            else
            {
                canuse_count = 0;
            }

            if (canuse_count > (inspect_count - qualified_count))
            {
                MessageBox.Show("回用数量不能大于废品数量", "注意");

                return false;
            }
            else
            {
                dgv.CurrentRow.Cells["废品"].Value = inspect_count - (qualified_count + canuse_count + remake_count);
                dgv.CurrentRow.Cells["技术审查"].Value = SQLDatabase.nowUserName();
                dgv.CurrentRow.Cells["审查日期"].Value = SQLDatabase.GetNowdate().ToString();


                return true;
            }
        }

        private void ly_production_order_inspectionDataGridView_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {



            DataGridView dgv = sender as DataGridView;

            if (!string.IsNullOrEmpty(dgv.CurrentRow.Cells["结账合同"].Value.ToString()))
            {
                MessageBox.Show("已经外协结账,不能修改数据", "注意");
                return;
            }
            string inspector = this.ly_production_order_inspectionDataGridView.CurrentRow.Cells["检查员"].Value.ToString();

            if (inspector != SQLDatabase.nowUserName())
            {
                MessageBox.Show("请检查员:" + inspector + "修改", "注意");
                return;
            }
            string nowordername = this.ly_machinepart_process_workDataGridView.CurrentRow.Cells["工序名称"].Value.ToString();

            if ("收料数量" == dgv.CurrentCell.OwningColumn.Name)
            {

                ChangeValue queryForm = new ChangeValue();

                queryForm.OldValue = dgv.CurrentCell.Value.ToString();
                queryForm.NewValue = "";
                queryForm.ChangeMode = "value";
                queryForm.ShowDialog();

                if (queryForm.NewValue != "")
                {
                    dgv.CurrentRow.Cells["收料数量"].Value = queryForm.NewValue;
                    decimal inspect_count;
                    if (!string.IsNullOrEmpty(this.ly_production_order_detailDataGridView.CurrentRow.Cells["数量1"].Value.ToString()))
                    {
                        inspect_count = decimal.Parse(this.ly_production_order_detailDataGridView.CurrentRow.Cells["数量1"].Value.ToString());
                    }
                    else
                    {
                        inspect_count = 0;
                    }

                    decimal send_inspect_count = 0;

                    if (null != this.ly_production_orderDataGridView.CurrentRow)
                    {

                        foreach (DataGridViewRow dgr in ly_production_order_inspectionDataGridView.Rows)
                        {

                            if (string.IsNullOrEmpty(dgr.Cells["收料数量"].Value.ToString())) continue;
                            send_inspect_count = send_inspect_count + decimal.Parse(dgr.Cells["收料数量"].Value.ToString());
                        }
                    }

                    if (!CheckInputRec(dgv))
                    {
                        dgv.CurrentRow.Cells["收料数量"].Value = queryForm.OldValue;
                        return;
                    }
                    SaveChangedZJ();

                }
                else
                {


                }
                return;
            }


            if ("送检" == dgv.CurrentCell.OwningColumn.Name)
            {

                ChangeValue queryForm = new ChangeValue();

                queryForm.OldValue = dgv.CurrentCell.Value.ToString();
                queryForm.NewValue = "";
                queryForm.ChangeMode = "value";
                queryForm.ShowDialog();

                if (queryForm.NewValue != "")
                {
                    dgv.CurrentRow.Cells["送检"].Value = queryForm.NewValue;

                    decimal inspect_count;

                    if (!string.IsNullOrEmpty(this.ly_production_order_detailDataGridView.CurrentRow.Cells["数量1"].Value.ToString()))
                    {
                        inspect_count = decimal.Parse(this.ly_production_order_detailDataGridView.CurrentRow.Cells["数量1"].Value.ToString());
                    }
                    else
                    {
                        inspect_count = 0;
                    }

                    decimal send_inspect_count = 0;

                    if (null != this.ly_production_orderDataGridView.CurrentRow)
                    {

                        foreach (DataGridViewRow dgr in ly_production_order_inspectionDataGridView.Rows)
                        {

                            if (string.IsNullOrEmpty(dgr.Cells["送检"].Value.ToString())) continue;
                            send_inspect_count = send_inspect_count + decimal.Parse(dgr.Cells["送检"].Value.ToString());



                        }
                    }
                    if (!CheckInput(dgv))
                    {
                        dgv.CurrentRow.Cells["送检"].Value = queryForm.OldValue;
                        return;
                    }
                    SaveChangedZJ();
                }
                else
                {


                }
                return;

            }


            if ("合格" == dgv.CurrentCell.OwningColumn.Name)
            {

                ChangeValue queryForm = new ChangeValue();

                queryForm.OldValue = dgv.CurrentCell.Value.ToString();
                queryForm.NewValue = "";
                queryForm.ChangeMode = "value";
                queryForm.ShowDialog();

                if (queryForm.NewValue != "")
                {
                    dgv.CurrentRow.Cells["合格"].Value = queryForm.NewValue;


                    if (!CheckInput(dgv))
                    {
                        dgv.CurrentRow.Cells["合格"].Value = queryForm.OldValue;
                        return;
                    }



                    SaveChangedZJ();

                }
                else
                {

                }
                return;
            }




            if ("检验日期" == dgv.CurrentCell.OwningColumn.Name)
            {

                ChangeValue queryForm = new ChangeValue();

                queryForm.OldValue = dgv.CurrentCell.Value.ToString();
                queryForm.NewValue = "";
                queryForm.ChangeMode = "datetime";
                queryForm.ShowDialog();




                if (queryForm.NewValue != "")
                {
                    dgv.CurrentRow.Cells["检验日期"].Value = queryForm.NewValue;
                    SaveChangedZJ();

                }
                else
                {
                }
                return;

            }

            if ("检测说明" == dgv.CurrentCell.OwningColumn.Name)
            {

                ChangeValue queryForm = new ChangeValue();

                queryForm.OldValue = dgv.CurrentCell.Value.ToString();
                queryForm.NewValue = "";
                queryForm.ChangeMode = "longstring";
                queryForm.ShowDialog();




                if (queryForm.NewValue != "")
                {
                    dgv.CurrentRow.Cells["检测说明"].Value = queryForm.NewValue;
                    SaveChangedZJ();

                }
                else
                {

                }
                return;
            }
            if ("返修合格_1" == dgv.CurrentCell.OwningColumn.Name)
            {

                ChangeValue queryForm = new ChangeValue();

                queryForm.OldValue = dgv.CurrentCell.Value.ToString();
                queryForm.NewValue = "";
                queryForm.ChangeMode = "value";
                queryForm.ShowDialog();

                if (queryForm.NewValue != "")
                {


                    if (string.IsNullOrEmpty(queryForm.NewValue))
                    {
                        dgv.CurrentRow.Cells["返修合格_1"].Value = DBNull.Value;
                    }
                    else
                    {
                        if (decimal.Parse(queryForm.NewValue) >= 0)
                        {

                            dgv.CurrentRow.Cells["返修合格_1"].Value = queryForm.NewValue;
                        }
                    }




                    if (!Checkremake(dgv))
                    {
                        if (string.IsNullOrEmpty(queryForm.OldValue))
                        {
                            dgv.CurrentRow.Cells["返修合格_1"].Value = DBNull.Value;
                        }
                        else
                        {
                            dgv.CurrentRow.Cells["返修合格_1"].Value = queryForm.OldValue;
                        }

                        return;
                    }

                    SaveChangedZJ();

                }
                else
                {


                }
                return;
            }
        }



        private bool Checkremake(DataGridView dgv)
        {
            decimal qualified_count;
            decimal inspect_count;



            if (!string.IsNullOrEmpty(dgv.CurrentRow.Cells["送检"].Value.ToString()))
            {
                inspect_count = decimal.Parse(dgv.CurrentRow.Cells["送检"].Value.ToString());
            }
            else
            {
                inspect_count = 0;
            }

            if (!string.IsNullOrEmpty(dgv.CurrentRow.Cells["合格"].Value.ToString()))
            {
                qualified_count = decimal.Parse(dgv.CurrentRow.Cells["合格"].Value.ToString());
            }
            else
            {
                qualified_count = 0;
            }



            decimal waste_count;
            decimal canuse_count;
            decimal remake_count;
            decimal have_remake_count;


            if (!string.IsNullOrEmpty(dgv.CurrentRow.Cells["废品"].Value.ToString()))
            {
                waste_count = decimal.Parse(dgv.CurrentRow.Cells["废品"].Value.ToString());
            }
            else
            {
                waste_count = 0;
            }

            if (!string.IsNullOrEmpty(dgv.CurrentRow.Cells["可用"].Value.ToString()))
            {
                canuse_count = decimal.Parse(dgv.CurrentRow.Cells["可用"].Value.ToString());
            }
            else
            {
                canuse_count = 0;
            }

            if (!string.IsNullOrEmpty(dgv.CurrentRow.Cells["返修合格_1"].Value.ToString()))
            {
                remake_count = decimal.Parse(dgv.CurrentRow.Cells["返修合格_1"].Value.ToString());
            }
            else
            {
                remake_count = 0;
            }

            if (!string.IsNullOrEmpty(dgv.CurrentRow.Cells["返修已排"].Value.ToString()))
            {
                have_remake_count = decimal.Parse(dgv.CurrentRow.Cells["返修已排"].Value.ToString());
            }
            else
            {
                have_remake_count = 0;
            }



            if (remake_count > have_remake_count)
            {
                MessageBox.Show("返修合格数量不能大于返修已排数量", "注意");

                return false;
            }
            else
            {
                dgv.CurrentRow.Cells["废品"].Value = inspect_count - (qualified_count + canuse_count + remake_count);
                dgv.CurrentRow.Cells["技术审查"].Value = SQLDatabase.nowUserName();
                dgv.CurrentRow.Cells["审查日期"].Value = SQLDatabase.GetNowdate().ToString();


                return true;
            }
        }














    }
}
