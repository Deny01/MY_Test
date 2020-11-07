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
    public partial class LY_Quality_Control_All_TEC : Form
    {

        string formState = "View";
        Point pt = new Point();

        string nowproductionorderNum = "";
        string now_order_id = "";
        string orderfinished = "False";


        public LY_Quality_Control_All_TEC()
        {
            InitializeComponent();
        }

        private void LY_Machine_Load(object sender, EventArgs e)
        {

            toolStripButton4.Text = "显示计划界面";
            this.splitContainer1.Panel1Collapsed = true;


            //this.tabControl1.SelectedIndex = 0;
            //this.tabControl1.TabPages[0].Show();
            //this.tabControl1.TabPages[1].Hide();

            this.tabPage1.Parent = this.tabControl1;
            this.tabPage2.Parent = null;
            this.tabControl1.SelectedTab = this.tabPage1;

            this.toolStripTextBox2.Visible = true;
            this.label1.Visible = true;
            this.label2.Visible = true;
            this.dateTimePicker1.Visible = true;
            this.dateTimePicker2.Visible = true;
            this.button1.Visible = true;



            this.bindingNavigator1.BindingSource = lY_productionorder_listBindingSource;

            this.dateTimePicker1.Text = DateTime.Today.AddMonths(-6).Date.ToString();
            this.dateTimePicker2.Text = DateTime.Today.AddDays(1).Date.ToString();
            //////////////////////////////////////////////////////////////////////////////////////////
            
            this.ly_production_order_inspectionAll_MainTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            this.ly_machinepart_process_work_allTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
           
            this.lY_productionorder_qualityTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
          
            this.ly_production_order_inspectionAllTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;

            this.lY_productionorder_listTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            this.lY_productionorder_listTableAdapter.Fill(this.lYQualityInspector.LY_productionorder_list, DateTime.Parse(this.dateTimePicker1.Text).Date, DateTime.Parse(this.dateTimePicker2.Text).Date);
          

            
          

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
                if (this.tabPage2 == this.tabControl1.SelectedTab)
                {
                    if (null != this.ly_material_plan_mainDataGridView.CurrentRow)
                    {
                        int parentId = int.Parse(this.ly_material_plan_mainDataGridView.Rows[e.RowIndex].Cells["parentid"].Value.ToString());
                        string planNum = this.ly_material_plan_mainDataGridView.Rows[e.RowIndex].Cells["计划编号"].Value.ToString();

                        //NewFrm.Show(this.ParentForm);

                        this.lY_productionorder_qualityTableAdapter.Fill(this.lYQualityInspector.LY_productionorder_quality, parentId);
                        //NewFrm.Hide(this.ParentForm);
                    }
                }
            }

                  
                 
        }

    
      
      


       

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            if ("隐藏计划界面" == toolStripButton4.Text)
            {
                toolStripButton4.Text = "显示计划界面";
                this.splitContainer1.Panel1Collapsed = true;

                //this.tabControl1.SelectedIndex = 0;

                //this.tabControl1.TabPages[0].Show();
                //this.tabControl1.TabPages[1].Hide();
                this.tabPage1.Parent = this.tabControl1;
                this.tabPage2.Parent = null;
                this.tabControl1.SelectedTab = this.tabPage1;

                this.toolStripTextBox2.Visible = true;
                this.label1.Visible = true;
                this.label2.Visible = true;
                this.dateTimePicker1.Visible = true;
                this.dateTimePicker2.Visible = true;
                this.button1.Visible = true;

                this.bindingNavigator1.BindingSource = lY_productionorder_listBindingSource;



            }
            else
            {
                toolStripButton4.Text = "隐藏计划界面";
                this.splitContainer1.Panel1Collapsed = false;




                //this.tabControl1.TabPages[0].Hide();
                //this.tabControl1.TabPages[1].Show();

                this.tabPage1.Parent = null;
                this.tabPage2.Parent = this.tabControl1;
                this.tabControl1.SelectedTab = this.tabPage2;

                this.toolStripTextBox2.Visible = false;
                this.label1.Visible = false;
                this.label2.Visible = false;
                this.dateTimePicker1.Visible = false;
                this.dateTimePicker2.Visible = false;
                this.button1.Visible = false;

                this.bindingNavigator1.BindingSource = lY_productionorder_qualityBindingSource;
            }
        }

     

        private void ly_production_orderDataGridView_SelectionChanged(object sender, EventArgs e)
        {
            if (this.formState == "View")
            {

                if (null != this.ly_production_orderDataGridView.CurrentRow)
                {

                    string nowitem = this.ly_production_orderDataGridView.CurrentRow.Cells["物料编码"].Value.ToString();
                    nowproductionorderNum = this.ly_production_orderDataGridView.CurrentRow.Cells["跟单编号"].Value.ToString();
                    now_order_id = this.ly_production_orderDataGridView.CurrentRow.Cells["orderId"].Value.ToString();
                    orderfinished = this.ly_production_orderDataGridView.CurrentRow.Cells["完成"].Value.ToString();

                    this.ly_machinepart_process_work_allTableAdapter.Fill(this.lYMaterielRequirements.ly_machinepart_process_work_all, nowitem, nowproductionorderNum);

                  

                    this.ly_production_order_inspectionAll_MainTableAdapter.Fill(this.lYQualityInspector.ly_production_order_inspectionAll_Main, nowproductionorderNum);

                   //set_processOrder_Num();


                }
                else
                {
                    this.ly_machinepart_process_work_allTableAdapter.Fill(this.lYMaterielRequirements.ly_machinepart_process_work_all, "", "");
                 
                    this.ly_production_order_inspectionAll_MainTableAdapter.Fill(this.lYQualityInspector.ly_production_order_inspectionAll_Main, "");
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
            //if (null != this.ly_production_orderDataGridView.CurrentRow && null != this.ly_machinepart_process_workDataGridView.CurrentRow)
            //{

            //    int nowOrder;
            //    string nowproductionorderNum = this.ly_production_orderDataGridView.CurrentRow.Cells["跟单编号"].Value.ToString();

            //    if ("" != this.ly_machinepart_process_workDataGridView.CurrentRow.Cells["工序编号"].Value.ToString())
            //    {
            //        nowOrder = int.Parse(this.ly_machinepart_process_workDataGridView.CurrentRow.Cells["工序编号"].Value.ToString());
            //    }
            //    else
            //    {
            //        nowOrder = 0;
            //    }



            //    this.ly_production_order_detailTableAdapter.Fill(this.lYMaterielRequirements.ly_production_order_detail, nowproductionorderNum, nowOrder);


            //}
            //else
            //{
            //    this.ly_production_order_detailTableAdapter.Fill(this.lYMaterielRequirements.ly_production_order_detail, "", 0);
            //}
        }

        private void ly_production_order_detailDataGridView_SelectionChanged(object sender, EventArgs e)
        {
            //if (null == this.ly_production_order_detailDataGridView.CurrentRow)
            //{
            //    this.ly_production_order_inspectionTableAdapter.Fill(this.lYQualityInspector.ly_production_order_inspection, -1);
            //    return;
            //}

            //int detailId = int.Parse(this.ly_production_order_detailDataGridView.CurrentRow.Cells["orderdetail_id"].Value.ToString());

            //this.ly_production_order_inspectionTableAdapter.Fill(this.lYQualityInspector.ly_production_order_inspection, detailId);
        }

        private bool CheckFinished()
        {

            if ("True" == this.orderfinished)
            {
                MessageBox.Show("跟单已经完成,操作取消", "注意");
                return true;

            }
            else
            {
                return false;
            }
            //if ("True" == this.ly_production_orderDataGridView.CurrentRow.Cells["完成"].Value.ToString())
            //{
            //    MessageBox.Show("跟单已经完成,操作取消", "注意");
            //    return true;

            //}
            //else
            //{
            //    return false;
            //}
        }

        private void bindingNavigatorAddNewItem_Click(object sender, EventArgs e)
        {
            if (CheckFinished()) return;
            
            if (null == ly_production_orderDataGridView.CurrentRow) return;

            if (null == ly_machinepart_process_workDataGridView.CurrentRow) return;

           // string nowproductionorder = this.ly_production_orderDataGridView.CurrentRow.Cells["跟单编号"].Value.ToString();

            // if ("下料" == nowordername) return;  ly_machinepart_process_workDataGridView

            string message = "增加总检记录吗？";
            string caption = "提示...";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;

            DialogResult result;



            result = MessageBox.Show(message, caption, buttons,
            MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

            if (result == DialogResult.Yes)
            {


                decimal inspect_count;
                decimal qualified_count;
                decimal canuse_count;
                decimal all_work_hours;


                if (!string.IsNullOrEmpty(this.ly_machinepart_process_workDataGridView.Rows[this.ly_machinepart_process_workDataGridView.RowCount - 1].Cells["单件累加"].Value.ToString()))
                {
                    all_work_hours = decimal.Parse(this.ly_machinepart_process_workDataGridView.Rows[this.ly_machinepart_process_workDataGridView.RowCount - 1].Cells["单件累加"].Value.ToString());
                }
                else
                {
                    all_work_hours = 0;
                }


                if (!string.IsNullOrEmpty(this.ly_machinepart_process_workDataGridView.Rows[this.ly_machinepart_process_workDataGridView.RowCount - 1].Cells["本序合格"].Value.ToString()))
                {
                    qualified_count = decimal.Parse(this.ly_machinepart_process_workDataGridView.Rows[this.ly_machinepart_process_workDataGridView.RowCount - 1].Cells["本序合格"].Value.ToString());
                }
                else
                {
                    qualified_count = 0;
                }

                if (!string.IsNullOrEmpty(this.ly_machinepart_process_workDataGridView.Rows[this.ly_machinepart_process_workDataGridView.RowCount - 1].Cells["本序可用"].Value.ToString()))
                {
                    canuse_count = decimal.Parse(this.ly_machinepart_process_workDataGridView.Rows[this.ly_machinepart_process_workDataGridView.RowCount - 1].Cells["本序可用"].Value.ToString());
                }
                else
                {
                    canuse_count = 0;
                }

                inspect_count = qualified_count + canuse_count;

                //if (!string.IsNullOrEmpty(this.ly_production_order_detailDataGridView.CurrentRow.Cells["合格1"].Value.ToString()))
                //{
                //    qualified_count = decimal.Parse(this.ly_production_order_detailDataGridView.CurrentRow.Cells["合格1"].Value.ToString());
                //}
                //else
                //{
                //    qualified_count = 0;
                //}

                //if (!string.IsNullOrEmpty(this.ly_production_order_detailDataGridView.CurrentRow.Cells["废品1"].Value.ToString()))
                //{
                //    waste_count = decimal.Parse(this.ly_production_order_detailDataGridView.CurrentRow.Cells["废品1"].Value.ToString());
                //}
                //else
                //{
                //    waste_count = 0;
                //}

                decimal send_inspect_count = 0;

                if (null != this.ly_production_order_inspectionAll_MainDataGridView.CurrentRow)
                {

                    foreach (DataGridViewRow dgr in ly_production_order_inspectionAll_MainDataGridView.Rows)
                    {

                        if (string.IsNullOrEmpty(dgr.Cells["送检"].Value.ToString())) continue;
                        send_inspect_count = send_inspect_count + decimal.Parse(dgr.Cells["送检"].Value.ToString());



                    }
                }

                if (1 > inspect_count)
                {

                    MessageBox.Show("末序无序检记录,不能增加总检记录", "注意");

                    return;
                }

                if (send_inspect_count >= inspect_count)
                {
                    MessageBox.Show("末序数量已经全部送交总检,不能增加总检记录", "注意");

                    return;
                }
                else
                {

                    this.ly_production_order_inspectionAll_MainBindingSource.AddNew();

                    this.ly_production_order_inspectionAll_MainDataGridView.CurrentRow.Cells["总检单号"].Value = GetMaxInspectionAll();


                    this.ly_production_order_inspectionAll_MainDataGridView.CurrentRow.Cells["跟单编号1"].Value = nowproductionorderNum;

                    this.ly_production_order_inspectionAll_MainDataGridView.CurrentRow.Cells["检验日期"].Value = SQLDatabase.GetNowdate().ToString(); ;

                    this.ly_production_order_inspectionAll_MainDataGridView.CurrentRow.Cells["质检员"].Value = SQLDatabase.nowUserName();


                    this.ly_production_order_inspectionAll_MainDataGridView.CurrentRow.Cells["送检"].Value = inspect_count - send_inspect_count;

                    this.ly_production_order_inspectionAll_MainDataGridView.CurrentRow.Cells["单件工时"].Value = all_work_hours;


                }






                SaveChanged();




            }
        }
        private string GetMaxInspectionAll()
        {
           

            SqlConnection sqlConnection1 = new SqlConnection(SQLDatabase.Connectstring);
            SqlCommand cmd = new SqlCommand();

            string MaxInspectionNum = "";

            cmd.Parameters.Add("@Production_mode", SqlDbType.VarChar);
            cmd.Parameters["@Production_mode"].Value = "ZJJG";


            cmd.CommandText = "LY_GetMax_InspectionAllNum";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = sqlConnection1;

            sqlConnection1.Open();
            MaxInspectionNum = cmd.ExecuteScalar().ToString();
            sqlConnection1.Close();



            return MaxInspectionNum;
        }

        private void SaveChanged()
        {

            this.ly_production_order_inspectionAll_MainDataGridView.EndEdit();

            this.Validate();
            this.ly_production_order_inspectionAll_MainBindingSource.EndEdit();

            this.ly_production_order_inspectionAll_MainTableAdapter.Update(this.lYQualityInspector.ly_production_order_inspectionAll_Main);


            string nowproductionorderAllNum="";

            if (null != this.ly_production_order_inspectionAll_MainDataGridView.CurrentRow)
            {
                nowproductionorderAllNum = this.ly_production_order_inspectionAll_MainDataGridView.CurrentRow.Cells["总检单号"].Value.ToString();
            }

            this.ly_production_order_inspectionAllTableAdapter.Fill(this.lYQualityInspector.ly_production_order_inspectionAll, nowproductionorderAllNum);

          

           
        }

        private void ly_production_order_inspectionDataGridView_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {

            if (CheckFinished()) return;
            DataGridView dgv = sender as DataGridView;


            //string nowordername = this.ly_machinepart_process_workDataGridView.CurrentRow.Cells["工序名称"].Value.ToString();

            //if ("下料" == nowordername) return;

//            if ("送检" == dgv.CurrentCell.OwningColumn.Name)
//            {

//                ChangeValue queryForm = new ChangeValue();

//                queryForm.OldValue = dgv.CurrentCell.Value.ToString();
//                queryForm.NewValue = "";
//                queryForm.ChangeMode = "value";
//                queryForm.ShowDialog();




//                if (queryForm.NewValue != "")
//                {
//                    dgv.CurrentRow.Cells["送检"].Value = queryForm.NewValue;

//                   /////////////////////////////////////////////////////////////////////////////////////////////

//                    decimal inspect_count;
//                    decimal qualified_count;
//                    decimal canuse_count;

//                    if (!string.IsNullOrEmpty(this.ly_machinepart_process_workDataGridView.Rows[this.ly_machinepart_process_workDataGridView.RowCount - 1].Cells["本序合格"].Value.ToString()))
//                    {
//                        qualified_count = decimal.Parse(this.ly_machinepart_process_workDataGridView.Rows[this.ly_machinepart_process_workDataGridView.RowCount - 1].Cells["本序合格"].Value.ToString());
//                    }
//                    else
//                    {
//                        qualified_count = 0;
//                    }

//                    if (!string.IsNullOrEmpty(this.ly_machinepart_process_workDataGridView.Rows[this.ly_machinepart_process_workDataGridView.RowCount - 1].Cells["本序可用"].Value.ToString()))
//                    {
//                        canuse_count = decimal.Parse(this.ly_machinepart_process_workDataGridView.Rows[this.ly_machinepart_process_workDataGridView.RowCount - 1].Cells["本序可用"].Value.ToString());
//                    }
//                    else
//                    {
//                        canuse_count = 0;
//                    }

//                    inspect_count = qualified_count + canuse_count;

//                    //if (!string.IsNullOrEmpty(this.ly_production_order_detailDataGridView.CurrentRow.Cells["合格1"].Value.ToString()))
//                    //{
//                    //    qualified_count = decimal.Parse(this.ly_production_order_detailDataGridView.CurrentRow.Cells["合格1"].Value.ToString());
//                    //}
//                    //else
//                    //{
//                    //    qualified_count = 0;
//                    //}

//                    //if (!string.IsNullOrEmpty(this.ly_production_order_detailDataGridView.CurrentRow.Cells["废品1"].Value.ToString()))
//                    //{
//                    //    waste_count = decimal.Parse(this.ly_production_order_detailDataGridView.CurrentRow.Cells["废品1"].Value.ToString());
//                    //}
//                    //else
//                    //{
//                    //    waste_count = 0;
//                    //}

//                    decimal send_inspect_count = 0;

//                    if (null != this.ly_production_order_inspectionAll_MainDataGridView.CurrentRow)
//                    {

//                        foreach (DataGridViewRow dgr in ly_production_order_inspectionAll_MainDataGridView.Rows)
//                        {

//                            if (string.IsNullOrEmpty(dgr.Cells["送检"].Value.ToString())) continue;
//                            send_inspect_count = send_inspect_count + decimal.Parse(dgr.Cells["送检"].Value.ToString());



//                        }
//                    }

//                    if (1 > inspect_count)
//                    {

//                        MessageBox.Show("末序无序检记录,不能增加总检记录", "注意");

//                        return;
//                    }

//                    if (send_inspect_count >= inspect_count)
//                    {
//                        //dgv.CurrentRow.Cells["送检"].Value = queryForm.OldValue;

//                        if (string.IsNullOrEmpty(queryForm.OldValue))
//                        {
//                            dgv.CurrentRow.Cells["送检"].Value = DBNull.Value;
//                        }
//                        else
//                        {
//                            dgv.CurrentRow.Cells["送检"].Value = queryForm.OldValue;
//                        }
//                        MessageBox.Show("末序数量已经全部送交总检,不能增加总检记录", "注意");

//                        return;
//                    }
                  

//                    ////////////////////////////////////////////////////////////////////////////////////////////

//                    if (!CheckInput(dgv))
//                    {
//                        if (string.IsNullOrEmpty(queryForm.OldValue))
//                        {
//                            dgv.CurrentRow.Cells["送检"].Value = DBNull.Value;
//                        }
//                        else
//                        {
//                            dgv.CurrentRow.Cells["送检"].Value = queryForm.OldValue;
//                        }
//                        return;
//                    }
//                    //dgv.CurrentRow.Cells["discount_money"].Value = DBNull.Value;

//                    //dgv.CurrentRow.Cells["approve_flag"].Value = "False";
//                    SaveChanged();


//                    //CountPlanStru();

//                }
//                else
//                {
//                    //hT_Manage_ItemDataGridView.CurrentRow.Cells["apply_money"].Value = queryForm.NewValue;
//                    //dgv.CurrentRow.Cells["discount_money"].Value = DBNull.Value;
//                    //dgv.CurrentRow.Cells["apply_money"].Value = DBNull.Value;
//                    //dgv.CurrentRow.Cells["approve_flag"].Value = "False";
//                    //SaveChanged();

//                }
//                return;

//            }


//            ///////////////////////////////////////////////////////
//            if ("合格" == dgv.CurrentCell.OwningColumn.Name)
//            {

//                ChangeValue queryForm = new ChangeValue();

//                queryForm.OldValue = dgv.CurrentCell.Value.ToString();
//                queryForm.NewValue = "";
//                queryForm.ChangeMode = "value";
//                queryForm.ShowDialog();




//                if (queryForm.NewValue != "")
//                {
//                    dgv.CurrentRow.Cells["合格"].Value = queryForm.NewValue;


//                    if (!CheckInput(dgv))
//                    {
//                        if (string.IsNullOrEmpty(queryForm.OldValue))
//                        {
//                            dgv.CurrentRow.Cells["合格"].Value = DBNull.Value;
//                        }
//                        else
//                        {
//                            dgv.CurrentRow.Cells["合格"].Value = queryForm.OldValue;
//                        }
//                        return;
//                    }

                   

//                    SaveChanged();


                  

//                }
//                else
//                {
//                    //hT_Manage_ItemDataGridView.CurrentRow.Cells["apply_money"].Value = queryForm.NewValue;
//                    //dgv.CurrentRow.Cells["discount_money"].Value = DBNull.Value;
//                    //dgv.CurrentRow.Cells["apply_money"].Value = DBNull.Value;
//                    //dgv.CurrentRow.Cells["approve_flag"].Value = "False";
//                    //SaveChanged();

//                }
//                return;

//            }

/////////////////////////////////////////////////////////////////////////////////
          

//            if ("检验日期" == dgv.CurrentCell.OwningColumn.Name)
//            {

//                ChangeValue queryForm = new ChangeValue();

//                queryForm.OldValue = dgv.CurrentCell.Value.ToString();
//                queryForm.NewValue = "";
//                queryForm.ChangeMode = "datetime";
//                queryForm.ShowDialog();




//                if (queryForm.NewValue != "")
//                {
//                    dgv.CurrentRow.Cells["检验日期"].Value = queryForm.NewValue;
//                    //dgv.CurrentRow.Cells["discount_money"].Value = DBNull.Value;

//                    //dgv.CurrentRow.Cells["approve_flag"].Value = "False";
//                    SaveChanged();


//                    //CountPlanStru();

//                }
//                else
//                {
                   

//                }
//                return;
                
                
//                /////////////////////////////////////

//                //DatePicker queryForm = new DatePicker();
//                //queryForm.Pt = pt;

//                //if (null != (dgv.CurrentCell.Value))
//                //    queryForm.NowDate = dgv.CurrentCell.Value.ToString();

//                //queryForm.ShowDialog();



//                //if (null != queryForm.NowDate)
//                //{

//                //    dgv.CurrentRow.Cells["检验日期"].Value = queryForm.NowDate;
//                //    SaveChanged();

//                //}
//                //return;
//            }









//            ///////////////////////////////////////////////////////

//            if ("检测说明" == dgv.CurrentCell.OwningColumn.Name)
//            {

//                ChangeValue queryForm = new ChangeValue();

//                queryForm.OldValue = dgv.CurrentCell.Value.ToString();
//                queryForm.NewValue = "";
//                queryForm.ChangeMode = "longstring";
//                queryForm.ShowDialog();




//                if (queryForm.NewValue != "")
//                {
//                    dgv.CurrentRow.Cells["检测说明"].Value = queryForm.NewValue;
//                    //dgv.CurrentRow.Cells["discount_money"].Value = DBNull.Value;

//                    //dgv.CurrentRow.Cells["approve_flag"].Value = "False";
//                    SaveChanged();


//                    //CountPlanStru();

//                }
//                else
//                {

//                }
//                return;

//            }

            ///////////////////////////////////////////////////////
            if ("可用" == dgv.CurrentCell.OwningColumn.Name)
            {

                ChangeValue queryForm = new ChangeValue();

                queryForm.OldValue = dgv.CurrentCell.Value.ToString();
                queryForm.NewValue = "";
                queryForm.ChangeMode = "value";
                queryForm.ShowDialog();




                if (queryForm.NewValue != "")
                {
                    dgv.CurrentRow.Cells["可用"].Value = queryForm.NewValue;


                    if (!CheckCanuse(dgv))
                    {
                        if (string.IsNullOrEmpty(queryForm.OldValue))
                        {
                            dgv.CurrentRow.Cells["可用"].Value = DBNull.Value;
                        }
                        else
                        {
                            dgv.CurrentRow.Cells["可用"].Value = queryForm.OldValue;
                        }
                        return;
                    }

                    dgv.CurrentRow.Cells["技术审查日期"].Value = SQLDatabase.GetNowdate().ToString();

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

            ///////////////////////////////////////////////////////
            if ("返修" == dgv.CurrentCell.OwningColumn.Name)
            {

                ChangeValue queryForm = new ChangeValue();

                queryForm.OldValue = dgv.CurrentCell.Value.ToString();
                queryForm.NewValue = "";
                queryForm.ChangeMode = "value";
                queryForm.ShowDialog();




                if (queryForm.NewValue != "")
                {
                    dgv.CurrentRow.Cells["返修"].Value = queryForm.NewValue;


                    if (!Checkremake(dgv))
                    {
                        if (string.IsNullOrEmpty(queryForm.OldValue))
                        {
                            dgv.CurrentRow.Cells["返修"].Value = DBNull.Value;
                        }
                        else
                        {
                            dgv.CurrentRow.Cells["返修"].Value = queryForm.OldValue;
                        }
                        return;
                    }

                    dgv.CurrentRow.Cells["技术审查日期"].Value = SQLDatabase.GetNowdate().ToString();

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

            /////////////////////////////

            if ("技术审查意见" == dgv.CurrentCell.OwningColumn.Name)
            {

                ChangeValue queryForm = new ChangeValue();

                queryForm.OldValue = dgv.CurrentCell.Value.ToString();
                queryForm.NewValue = "";
                queryForm.ChangeMode = "longstring";
                queryForm.ShowDialog();




                if (queryForm.NewValue != "")
                {
                    dgv.CurrentRow.Cells["技术审查意见"].Value = queryForm.NewValue;
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
            ///////////////////////////////////////////////////////////////////////////////


            if ("技术审查日期" == dgv.CurrentCell.OwningColumn.Name)
            {

                ChangeValue queryForm = new ChangeValue();

                queryForm.OldValue = dgv.CurrentCell.Value.ToString();
                queryForm.NewValue = "";
                queryForm.ChangeMode = "datetime";
                queryForm.ShowDialog();




                if (queryForm.NewValue != "")
                {
                    dgv.CurrentRow.Cells["技术审查日期"].Value = queryForm.NewValue;
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


            /////////////////////////////////////
        }

        private bool  CheckInput(DataGridView dgv)
        {
            decimal  qualified_count;
            decimal inspect_count;
            decimal canuse_count;
            decimal remake_count;


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

            if (!string.IsNullOrEmpty(dgv.CurrentRow.Cells["返修"].Value.ToString()))
            {
                remake_count = decimal.Parse(dgv.CurrentRow.Cells["返修"].Value.ToString());
            }
            else
            {
                remake_count = 0;
            }

            if ((qualified_count + canuse_count) > inspect_count)
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

            if (!string.IsNullOrEmpty(dgv.CurrentRow.Cells["返修"].Value.ToString()))
            {
                remake_count = decimal.Parse(dgv.CurrentRow.Cells["返修"].Value.ToString());
            }
            else
            {
                remake_count = 0;
            }

            if (canuse_count > (inspect_count - (qualified_count + remake_count)))
            {
                MessageBox.Show("回用数量不能大于废品数量", "注意");

                return false;
            }
            else
            {
                dgv.CurrentRow.Cells["废品"].Value = inspect_count - (qualified_count + canuse_count + remake_count);
                dgv.CurrentRow.Cells["技术审查人"].Value =SQLDatabase.nowUserName();
                dgv.CurrentRow.Cells["技术审查日期"].Value = SQLDatabase.GetNowdate().ToString();

                
                return true;
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

            if (!string.IsNullOrEmpty(dgv.CurrentRow.Cells["返修"].Value.ToString()))
            {
                remake_count = decimal.Parse(dgv.CurrentRow.Cells["返修"].Value.ToString());
            }
            else
            {
                remake_count = 0;
            }

            if (remake_count > (inspect_count - qualified_count - canuse_count))
            {
                MessageBox.Show("返修合格数量不能大于废品数量", "注意");

                return false;
            }
            else
            {
                dgv.CurrentRow.Cells["废品"].Value = inspect_count - (qualified_count + canuse_count + remake_count);
                dgv.CurrentRow.Cells["技术审查人"].Value = SQLDatabase.nowUserName();
                dgv.CurrentRow.Cells["技术审查日期"].Value = SQLDatabase.GetNowdate().ToString();


                return true;
            }
        }

        private void bindingNavigatorDeleteItem_Click(object sender, EventArgs e)
        {
            if (null == this.ly_production_order_inspectionAll_MainDataGridView.CurrentRow) return;

            if (CheckFinished()) return;

            decimal inStoreNum;
            int   mainId = int.Parse(this.ly_production_order_inspectionAll_MainDataGridView.CurrentRow.Cells["mainid"].Value.ToString());
           
                       
            //string nowproductionorderNum = this.ly_production_orderDataGridView.CurrentRow.Cells["跟单编号"].Value.ToString();
          
            this.ly_production_order_inspectionAll_MainTableAdapter.Fill(this.lYQualityInspector.ly_production_order_inspectionAll_Main, nowproductionorderNum);
            this.ly_production_order_inspectionAll_MainBindingSource.Position = this.ly_production_order_inspectionAll_MainBindingSource.Find("id", mainId);

            if (string.IsNullOrEmpty(this.ly_production_order_inspectionAll_MainDataGridView.CurrentRow.Cells["入库"].Value.ToString()))
            {
                inStoreNum =0;
            }
            else
            {
                inStoreNum = decimal.Parse(this.ly_production_order_inspectionAll_MainDataGridView.CurrentRow.Cells["入库"].Value.ToString());
            
            }

            if (0 < inStoreNum)
            {
                MessageBox.Show("已有入库记录，不能删除(实需删除，请先删除该总检单号的入库记录)", "注意");
                return;

            }

            string inspector = this.ly_production_order_inspectionAll_MainDataGridView.CurrentRow.Cells["质检员"].Value.ToString();

            if (inspector != SQLDatabase.nowUserName())
            {
                MessageBox.Show("请质检员:" + inspector + "删除", "注意");
                return;
            }

          

            string message1 = "当前总检记录将被删除，继续吗？";
            string caption1 = "提示...";
            MessageBoxButtons buttons1 = MessageBoxButtons.YesNo;

            DialogResult result1;



            result1 = MessageBox.Show(message1, caption1, buttons1,
            MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

            if (result1 == DialogResult.Yes)
            {

                this.ly_production_order_inspectionAll_MainBindingSource.RemoveCurrent();
             


                SaveChanged();




            }
        }

        private void ly_production_order_inspectionAll_MainDataGridView_SelectionChanged(object sender, EventArgs e)
        {
            if (null != this.ly_production_order_inspectionAll_MainDataGridView.CurrentRow)
            {


                string nowproductionorderAllNum = this.ly_production_order_inspectionAll_MainDataGridView.CurrentRow.Cells["总检单号"].Value.ToString();

                this.ly_production_order_inspectionAllTableAdapter.Fill(this.lYQualityInspector.ly_production_order_inspectionAll, nowproductionorderAllNum);

                //set_processOrder_Num();


            }
            else
            {
                this.ly_production_order_inspectionAllTableAdapter.Fill(this.lYQualityInspector.ly_production_order_inspectionAll, "");
            }
        }

        private void ly_production_order_inspectionAllDataGridView_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            DataGridView dgv = sender as DataGridView;

            if (!SQLDatabase.CheckHaveRight(SQLDatabase.NowUserID, "机加总检判定"))
            {
                MessageBox.Show("无机加总检判定权限,操作取消...", "注意");
                return;

            }

            if (CheckFinished()) return;
          


            if ("判定日期" == dgv.CurrentCell.OwningColumn.Name)
            {
               

                ChangeValue queryForm = new ChangeValue();

                queryForm.OldValue = dgv.CurrentCell.Value.ToString();
                queryForm.NewValue = "";
                queryForm.ChangeMode = "datetime";
                queryForm.ShowDialog();




                if (queryForm.NewValue != "")
                {
                    dgv.CurrentRow.Cells["判定日期"].Value = queryForm.NewValue;
                    //dgv.CurrentRow.Cells["discount_money"].Value = DBNull.Value;

                    //dgv.CurrentRow.Cells["approve_flag"].Value = "False";
                    SaveJudged();


                    //CountPlanStru();

                }
                else
                {


                }
                return;


            
            }









            ///////////////////////////////////////////////////////

            if ("判定说明" == dgv.CurrentCell.OwningColumn.Name)
            {

               

                ChangeValue queryForm = new ChangeValue();

                queryForm.OldValue = dgv.CurrentCell.Value.ToString();
                queryForm.NewValue = "";
                queryForm.ChangeMode = "longstring";
                queryForm.ShowDialog();




                if (queryForm.NewValue != "")
                {
                    dgv.CurrentRow.Cells["判定说明"].Value = queryForm.NewValue;
                    //dgv.CurrentRow.Cells["discount_money"].Value = DBNull.Value;

                    //dgv.CurrentRow.Cells["approve_flag"].Value = "False";
                    SaveJudged();


                    //CountPlanStru();

                }
                else
                {

                }
                return;

            }

            ///////////////////////////////////////////////////////
            if ("可用数量" == dgv.CurrentCell.OwningColumn.Name)
            {

               
                
                ChangeValue queryForm = new ChangeValue();

                queryForm.OldValue = dgv.CurrentCell.Value.ToString();
                queryForm.NewValue = "";
                queryForm.ChangeMode = "value";
                queryForm.ShowDialog();




                if (queryForm.NewValue != "")
                {
                    dgv.CurrentRow.Cells["可用数量"].Value = queryForm.NewValue;


                    if (!CheckJudged(dgv))
                    {


                        if (string.IsNullOrEmpty(queryForm.OldValue))
                        {
                            dgv.CurrentRow.Cells["可用数量"].Value = DBNull.Value;
                        }
                        else
                        {
                            dgv.CurrentRow.Cells["可用数量"].Value = queryForm.OldValue;
                        }
                        return;
                    }

                    dgv.CurrentRow.Cells["判定日期"].Value = SQLDatabase.GetNowdate().ToString();
                    dgv.CurrentRow.Cells["判定人"].Value = SQLDatabase.nowUserName();

                    SaveJudged();


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
            if ("废品数量" == dgv.CurrentCell.OwningColumn.Name)
            {
               
                
                
                
                ChangeValue queryForm = new ChangeValue();

                queryForm.OldValue = dgv.CurrentCell.Value.ToString();
                queryForm.NewValue = "";
                queryForm.ChangeMode = "value";
                queryForm.ShowDialog();




                if (queryForm.NewValue != "")
                {
                    dgv.CurrentRow.Cells["废品数量"].Value = queryForm.NewValue;


                    if (!CheckJudged(dgv))
                    {
                        if (string.IsNullOrEmpty(queryForm.OldValue))
                        {
                            dgv.CurrentRow.Cells["废品数量"].Value = DBNull.Value;
                        }
                        else
                        {
                            dgv.CurrentRow.Cells["废品数量"].Value = queryForm.OldValue;
                        }
                        return;
                    }

                    dgv.CurrentRow.Cells["判定日期"].Value = SQLDatabase.GetNowdate().ToString();
                    dgv.CurrentRow.Cells["判定人"].Value = SQLDatabase.nowUserName();

                    SaveJudged();


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
            if ("返修合格" == dgv.CurrentCell.OwningColumn.Name)
            {




                ChangeValue queryForm = new ChangeValue();

                queryForm.OldValue = dgv.CurrentCell.Value.ToString();
                queryForm.NewValue = "";
                queryForm.ChangeMode = "value";
                queryForm.ShowDialog();




                if (queryForm.NewValue != "")
                {
                    dgv.CurrentRow.Cells["返修合格"].Value = queryForm.NewValue;


                    if (!CheckJudged(dgv))
                    {
                        if (string.IsNullOrEmpty(queryForm.OldValue))
                        {
                            dgv.CurrentRow.Cells["返修合格"].Value = DBNull.Value;
                        }
                        else
                        {
                            dgv.CurrentRow.Cells["返修合格"].Value = queryForm.OldValue;
                        }
                        return;
                    }

                    dgv.CurrentRow.Cells["判定日期"].Value = SQLDatabase.GetNowdate().ToString();
                    dgv.CurrentRow.Cells["判定人"].Value = SQLDatabase.nowUserName();

                    SaveJudged();

                    nowproductionorderNum = this.lY_productionorder_listDataGridView.CurrentRow.Cells["跟单编号2"].Value.ToString();

                    this.ly_production_order_inspectionAll_MainDataGridView.SelectionChanged -= this.ly_production_order_inspectionAll_MainDataGridView_SelectionChanged;
                    this.ly_production_order_inspectionAll_MainTableAdapter.Fill(this.lYQualityInspector.ly_production_order_inspectionAll_Main, nowproductionorderNum);
                    this.ly_production_order_inspectionAll_MainDataGridView.SelectionChanged += this.ly_production_order_inspectionAll_MainDataGridView_SelectionChanged;




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

           
        }

        private void SaveJudged()
        {

            this.ly_production_order_inspectionAllDataGridView.EndEdit();

            this.Validate();
            this.ly_production_order_inspectionAllBindingSource.EndEdit();

            this.ly_production_order_inspectionAllTableAdapter.Update(this.lYQualityInspector.ly_production_order_inspectionAll);


            string nowproductionorderAllNum = "";

            if (null != this.ly_production_order_inspectionAll_MainDataGridView.CurrentRow)
            {
                nowproductionorderAllNum = this.ly_production_order_inspectionAll_MainDataGridView.CurrentRow.Cells["总检单号"].Value.ToString();
            }

            this.ly_production_order_inspectionAllTableAdapter.Fill(this.lYQualityInspector.ly_production_order_inspectionAll, nowproductionorderAllNum);




        }

        private bool CheckJudged(DataGridView dgv)
        {


            //return true;

            decimal std_canuse_count;
            decimal std_waste_count;



            if (!string.IsNullOrEmpty(ly_production_order_inspectionAll_MainDataGridView.CurrentRow.Cells["可用"].Value.ToString()))
            {
                std_canuse_count = decimal.Parse(ly_production_order_inspectionAll_MainDataGridView.CurrentRow.Cells["可用"].Value.ToString());
            }
            else
            {
                std_canuse_count = 0;
            }

            if (!string.IsNullOrEmpty(ly_production_order_inspectionAll_MainDataGridView.CurrentRow.Cells["废品"].Value.ToString()))
            {
                std_waste_count = decimal.Parse(ly_production_order_inspectionAll_MainDataGridView.CurrentRow.Cells["废品"].Value.ToString());
            }
            else
            {
                std_waste_count = 0;
            }



            decimal waste_count = 0;
            decimal canuse_count = 0;


           

            if (null != dgv.CurrentRow)
            {

                foreach (DataGridViewRow dgr in dgv.Rows)
                {

                    if (string.IsNullOrEmpty(dgr.Cells["废品数量"].Value.ToString())) continue;
                    waste_count = waste_count + decimal.Parse(dgr.Cells["废品数量"].Value.ToString());



                }
            }

            if (null != dgv.CurrentRow)
            {

                foreach (DataGridViewRow dgr in dgv.Rows)
                {

                    if (string.IsNullOrEmpty(dgr.Cells["可用数量"].Value.ToString())) continue;
                    canuse_count = canuse_count + decimal.Parse(dgr.Cells["可用数量"].Value.ToString());



                }
            }


            decimal remake_count = 0;
            decimal have_remake_count = 0;

            if (!string.IsNullOrEmpty(dgv.CurrentRow.Cells["返修合格"].Value.ToString()))
            {
                remake_count = decimal.Parse(dgv.CurrentRow.Cells["返修合格"].Value.ToString());
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

           ///////////////////////////////////////////////////////////



            //if (waste_count > std_waste_count )
            //{
            //    MessageBox.Show("废品判定数量大于总检废品数量,请检验输入数据...", "注意");

            //    return false;
            //}

            //if (waste_count > std_waste_count || canuse_count > std_canuse_count)
            //{
            //    MessageBox.Show("回用判定数量大于总检回用数量,请检验输入数据...", "注意");

            //    return false;
            //}
           

            return true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.lY_productionorder_listTableAdapter.Fill(this.lYQualityInspector.LY_productionorder_list, DateTime.Parse(this.dateTimePicker1.Text).Date, DateTime.Parse(this.dateTimePicker2.Text).Date);
        }


        private void toolStripTextBox2_Enter(object sender, EventArgs e)
        {
            toolStripTextBox2.Text = "";

            this.lY_productionorder_listBindingSource.Filter = "";
        }

        private void toolStripTextBox2_KeyUp(object sender, KeyEventArgs e)
        {
            string dFilter = "";

            dFilter = GetDataGridviewMultiFilterString.DGVMultiFilterString(this.lY_productionorder_listDataGridView, this.toolStripTextBox2.Text);

            if (null == dFilter)
                dFilter = "";

            this.lY_productionorder_listBindingSource.Filter = dFilter;
        }

        private void lY_productionorder_listDataGridView_SelectionChanged(object sender, EventArgs e)
        {
            if (null != this.lY_productionorder_listDataGridView.CurrentRow)
            {

                int nowplanid = int.Parse(this.lY_productionorder_listDataGridView.CurrentRow.Cells["plan_id"].Value.ToString());

                this.ly_material_plan_mainDataGridView.RowEnter -= ly_material_plan_mainDataGridView_RowEnter;
                this.ly_material_plan_mainBindingSource.Position = this.ly_material_plan_mainBindingSource.Find("id", nowplanid);
                this.ly_material_plan_mainDataGridView.RowEnter += ly_material_plan_mainDataGridView_RowEnter;


                string nowitemno = this.lY_productionorder_listDataGridView.CurrentRow.Cells["物料编码2"].Value.ToString();

                nowproductionorderNum = this.lY_productionorder_listDataGridView.CurrentRow.Cells["跟单编号2"].Value.ToString();
                now_order_id = this.lY_productionorder_listDataGridView.CurrentRow.Cells["id2"].Value.ToString();
                orderfinished = this.lY_productionorder_listDataGridView.CurrentRow.Cells["完成2"].Value.ToString();

                this.ly_machinepart_process_work_allTableAdapter.Fill(this.lYMaterielRequirements.ly_machinepart_process_work_all, nowitemno, nowproductionorderNum);



                this.ly_production_order_inspectionAll_MainTableAdapter.Fill(this.lYQualityInspector.ly_production_order_inspectionAll_Main, nowproductionorderNum);

                //set_processOrder_Num();


            }
            else
            {
                this.ly_machinepart_process_work_allTableAdapter.Fill(this.lYMaterielRequirements.ly_machinepart_process_work_all, "", "");

                this.ly_production_order_inspectionAll_MainTableAdapter.Fill(this.lYQualityInspector.ly_production_order_inspectionAll_Main, "");
            }
         

              
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            TabControl tc = sender as TabControl;

            if (this.tabPage1 == tc.SelectedTab)
            {


                if (null != this.lY_productionorder_listDataGridView.CurrentRow)
                {

                    int nowplanid = int.Parse(this.lY_productionorder_listDataGridView.CurrentRow.Cells["plan_id"].Value.ToString());

                    this.ly_material_plan_mainDataGridView.RowEnter -= ly_material_plan_mainDataGridView_RowEnter;
                    this.ly_material_plan_mainBindingSource.Position = this.ly_material_plan_mainBindingSource.Find("id", nowplanid);
                    this.ly_material_plan_mainDataGridView.RowEnter += ly_material_plan_mainDataGridView_RowEnter;


                    string nowitemno = this.lY_productionorder_listDataGridView.CurrentRow.Cells["物料编码2"].Value.ToString();

                    nowproductionorderNum = this.lY_productionorder_listDataGridView.CurrentRow.Cells["跟单编号2"].Value.ToString();
                    now_order_id = this.lY_productionorder_listDataGridView.CurrentRow.Cells["id2"].Value.ToString();
                    orderfinished = this.lY_productionorder_listDataGridView.CurrentRow.Cells["完成2"].Value.ToString();

                    this.ly_machinepart_process_work_allTableAdapter.Fill(this.lYMaterielRequirements.ly_machinepart_process_work_all, nowitemno, nowproductionorderNum);



                    this.ly_production_order_inspectionAll_MainTableAdapter.Fill(this.lYQualityInspector.ly_production_order_inspectionAll_Main, nowproductionorderNum);

                    //set_processOrder_Num();


                }
                else
                {
                    this.ly_machinepart_process_work_allTableAdapter.Fill(this.lYMaterielRequirements.ly_machinepart_process_work_all, "", "");

                    this.ly_production_order_inspectionAll_MainTableAdapter.Fill(this.lYQualityInspector.ly_production_order_inspectionAll_Main, "");
                }

            }

            if (this.tabPage2 == tc.SelectedTab)
            {
                if (null != this.ly_production_orderDataGridView.CurrentRow)
                {

                    string nowitem = this.ly_production_orderDataGridView.CurrentRow.Cells["物料编码"].Value.ToString();
                    nowproductionorderNum = this.ly_production_orderDataGridView.CurrentRow.Cells["跟单编号"].Value.ToString();
                    now_order_id = this.ly_production_orderDataGridView.CurrentRow.Cells["orderId"].Value.ToString();
                    orderfinished = this.ly_production_orderDataGridView.CurrentRow.Cells["完成"].Value.ToString();

                    this.ly_machinepart_process_work_allTableAdapter.Fill(this.lYMaterielRequirements.ly_machinepart_process_work_all, nowitem, nowproductionorderNum);



                    this.ly_production_order_inspectionAll_MainTableAdapter.Fill(this.lYQualityInspector.ly_production_order_inspectionAll_Main, nowproductionorderNum);

                    //set_processOrder_Num();


                }
                else
                {
                    this.ly_machinepart_process_work_allTableAdapter.Fill(this.lYMaterielRequirements.ly_machinepart_process_work_all, "", "");

                    this.ly_production_order_inspectionAll_MainTableAdapter.Fill(this.lYQualityInspector.ly_production_order_inspectionAll_Main, "");
                }
            }
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (null == this.ly_production_order_inspectionAllDataGridView.CurrentRow) return;

            LY_Machine_RemakeMange queryForm = new LY_Machine_RemakeMange();

            //decimal waste_count;


            //if (!string.IsNullOrEmpty(ly_production_order_inspectionDataGridView.CurrentRow.Cells["废品"].Value.ToString()))
            //{
            //    waste_count = decimal.Parse(ly_production_order_inspectionDataGridView.CurrentRow.Cells["废品"].Value.ToString());
            //}
            //else
            //{
            //    waste_count = 0;
            //}

            //decimal qualified_count;
            //decimal inspect_count;



            //if (!string.IsNullOrEmpty(ly_production_order_inspectionAllDataGridView.CurrentRow.Cells["送检"].Value.ToString()))
            //{
            //    inspect_count = decimal.Parse(ly_production_order_inspectionAllDataGridView.CurrentRow.Cells["送检"].Value.ToString());
            //}
            //else
            //{
            //    inspect_count = 0;
            //}

            //if (!string.IsNullOrEmpty(ly_production_order_inspectionDataGridView.CurrentRow.Cells["合格"].Value.ToString()))
            //{
            //    qualified_count = decimal.Parse(ly_production_order_inspectionDataGridView.CurrentRow.Cells["合格"].Value.ToString());
            //}
            //else
            //{
            //    qualified_count = 0;
            //}



            decimal waste_count;
            decimal canuse_count;


            if (!string.IsNullOrEmpty(ly_production_order_inspectionAllDataGridView.CurrentRow.Cells["废品数量"].Value.ToString()))
            {
                waste_count = decimal.Parse(ly_production_order_inspectionAllDataGridView.CurrentRow.Cells["废品数量"].Value.ToString());
            }
            else
            {
                waste_count = 0;
            }

            //if (!string.IsNullOrEmpty(ly_production_order_inspectionDataGridView.CurrentRow.Cells["可用"].Value.ToString()))
            //{
            //    canuse_count = decimal.Parse(ly_production_order_inspectionDataGridView.CurrentRow.Cells["可用"].Value.ToString());
            //}
            //else
            //{
            //    canuse_count = 0;
            //}

            decimal have_remake_count;
            if (!string.IsNullOrEmpty(ly_production_order_inspectionAllDataGridView.CurrentRow.Cells["返修已排"].Value.ToString()))
            {
                have_remake_count = decimal.Parse(ly_production_order_inspectionAllDataGridView.CurrentRow.Cells["返修已排"].Value.ToString());
            }
            else
            {
                have_remake_count = 0;
            }

            //decimal remake_count;
            //if (!string.IsNullOrEmpty(ly_production_order_inspectionDataGridView.CurrentRow.Cells["返修合格"].Value.ToString()))
            //{
            //    remake_count = decimal.Parse(ly_production_order_inspectionDataGridView.CurrentRow.Cells["返修合格"].Value.ToString());
            //}
            //else
            //{
            //    remake_count = 0;
            //}

            int nowinspectionId;

            if (!string.IsNullOrEmpty(ly_production_order_inspectionAllDataGridView.CurrentRow.Cells["inspectid"].Value.ToString()))
            {
                nowinspectionId = int.Parse(ly_production_order_inspectionAllDataGridView.CurrentRow.Cells["inspectid"].Value.ToString());
            }
            else
            {
                nowinspectionId = 0;
            }

            string nowinspectionNum;

            if (!string.IsNullOrEmpty(ly_production_order_inspectionAllDataGridView.CurrentRow.Cells["总检单号1"].Value.ToString()))
            {
                nowinspectionNum = ly_production_order_inspectionAllDataGridView.CurrentRow.Cells["总检单号1"].Value.ToString();
            }
            else
            {
                nowinspectionNum = "";
            }

            //if (0 >= waste_count)
            //{
            //    MessageBox.Show("本序无废品,无须增加返修单...", "注意");
            //    return;


            //}

            nowproductionorderNum = this.lY_productionorder_listDataGridView.CurrentRow.Cells["跟单编号2"].Value.ToString();
            string now_order_name = this.ly_production_order_inspectionAllDataGridView.CurrentRow.Cells["序名"].Value.ToString();
            string now_worker = this.ly_production_order_inspectionAllDataGridView.CurrentRow.Cells["姓名"].Value.ToString();
            int now_parent_process_order =int .Parse ( this.ly_production_order_inspectionAllDataGridView.CurrentRow.Cells["工序"].Value.ToString());

            queryForm.Runmode = "管理";
            queryForm.Remake_style = "总检";
            queryForm.Now_inspectid = nowinspectionId;
            queryForm.Nowinspectcode = nowinspectionNum;
            queryForm.Parent_process_order = now_parent_process_order;

            queryForm.Nowproductorder = nowproductionorderNum;
            queryForm.Nowordername = now_order_name;
            queryForm.Nowworker = now_worker;

            queryForm.Allcan_remake_count = waste_count;
            queryForm.Have_remake_count = have_remake_count;

            queryForm.StartPosition = FormStartPosition.CenterParent;
            queryForm.ShowDialog();



            string nowproductionorderAllNum = this.ly_production_order_inspectionAll_MainDataGridView.CurrentRow.Cells["总检单号"].Value.ToString();

            this.ly_production_order_inspectionAllTableAdapter.Fill(this.lYQualityInspector.ly_production_order_inspectionAll, nowproductionorderAllNum);
        }

       
        

     
      
    }
}
