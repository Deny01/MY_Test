using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Transactions;
using HappyYF.Infrastructure.Repositories;
using DataGridFilter;

namespace HappyYF.YuanXin.WorkSet
{
    public partial class LY_Machine_Foroutsource : Form
    {

        string formState = "View";
        Point pt = new Point();
        string nowcontractNum = "";
        string now_order_id ="";
        string   orderfinished = "False";
        string nowaddcode = "";
        public LY_Machine_Foroutsource()
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



            this.bindingNavigator1.BindingSource = ly_outsource_contract_detailMachinelistBindingSource;
            //////////////////////////////////////////////////////////////////////////////////////////


            this.dateTimePicker1.Text = DateTime.Today.AddMonths(-3).Date.ToString();
            this.dateTimePicker2.Text = DateTime.Today.AddDays(1).Date.ToString();
            
            this.ly_outsourcepart_process_workTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            this.ly_outsource_order_detail_selTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            this.ly_outsource_order_inspectionTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            this.ly_outsource_contract_detailQCTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;



            this.ly_outsource_contract_detailMachinelistTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            this.ly_outsource_contract_detailMachinelistTableAdapter.Fill(this.lYOutsourceData.ly_outsource_contract_detailMachinelist, DateTime.Parse(this.dateTimePicker1.Text).Date, DateTime.Parse(this.dateTimePicker2.Text).Date);
          

            this.ly_material_plan_mainTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;

            this.ly_material_plan_mainDataGridView.RowEnter -= ly_material_plan_mainDataGridView_RowEnter;
            this.ly_material_plan_mainTableAdapter.Fill(this.lYMaterielRequirements.ly_material_plan_main, "SCJH");
            this.ly_material_plan_mainDataGridView.RowEnter += ly_material_plan_mainDataGridView_RowEnter;

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
                if (this .tabPage2  == this.tabControl1.SelectedTab )
                {
                    if (null != this.ly_material_plan_mainDataGridView.CurrentRow)
                    {
                        int parentId = int.Parse(this.ly_material_plan_mainDataGridView.Rows[e.RowIndex].Cells["parentid"].Value.ToString());
                        string planNum = this.ly_material_plan_mainDataGridView.Rows[e.RowIndex].Cells["计划编号"].Value.ToString();

                        //NewFrm.Show(this.ParentForm);

                        this.ly_outsource_contract_detailQCTableAdapter.Fill(this.lYOutsourceData.ly_outsource_contract_detailQC, planNum);
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
                this.label1 . Visible = true;
                this.label2 . Visible = true;
                this.dateTimePicker1.Visible = true;
                this.dateTimePicker2.Visible = true;
                this.button1.Visible = true;

                this.bindingNavigator1.BindingSource = ly_outsource_contract_detailMachinelistBindingSource;


                
            }
            else
            {
                toolStripButton4.Text = "隐藏计划界面";
                this.splitContainer1.Panel1Collapsed = false ;

                


                //this.tabControl1.TabPages[0].Hide();
                //this.tabControl1.TabPages[1].Show();

                this.tabPage1.Parent = null ;
                this.tabPage2.Parent = this.tabControl1;
                this.tabControl1.SelectedTab = this.tabPage2;

                this.toolStripTextBox2.Visible = false ;
                this.label1 . Visible = false;
                this.label2 . Visible = false;
                this.dateTimePicker1.Visible = false;
                this.dateTimePicker2.Visible = false;
                this.button1.Visible = false;

                this.bindingNavigator1.BindingSource = ly_outsource_contract_detailQCBindingSource;
            }
        }

     

        private void ly_production_orderDataGridView_SelectionChanged(object sender, EventArgs e)
        {
            if (this.formState == "View")
            {

                if (null != this.ly_outsource_contract_detailQCDataGridView.CurrentRow)
                {

                    string nowitem = this.ly_outsource_contract_detailQCDataGridView.CurrentRow.Cells["物料编号"].Value.ToString();
                    nowcontractNum = this.ly_outsource_contract_detailQCDataGridView.CurrentRow.Cells["合同编号"].Value.ToString();
                    now_order_id = this.ly_outsource_contract_detailQCDataGridView.CurrentRow.Cells["orderId"].Value.ToString();
                    nowaddcode = this.ly_outsource_contract_detailQCDataGridView.CurrentRow.Cells["加工码"].Value.ToString();
                    int id2 = int .Parse ( this.ly_outsource_contract_detailQCDataGridView.CurrentRow.Cells["id20"].Value.ToString());
                    this.ly_outsourcepart_process_workTableAdapter.Fill(this.lYOutsourceData.ly_outsourcepart_process_work, nowitem, nowcontractNum, id2, nowaddcode);

                   //set_processOrder_Num();


                }
                else
                {
                    this.ly_outsourcepart_process_workTableAdapter.Fill(this.lYOutsourceData.ly_outsourcepart_process_work, "", "",0,"");
                }
            }
        }
        private void set_processOrder_Num()
        {
            int processOrder;



            if (null == this.ly_outsource_contract_detailQCDataGridView.CurrentRow) return;

            foreach (DataGridViewRow dgr in ly_outsourcepart_process_workDataGridView.Rows)
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


                    dgr.Cells["本序数量"].Value = ly_outsourcepart_process_workDataGridView.Rows[processOrder - 1].Cells["本序合格"].Value;

                }

            }

            //for (int i = 1; i <= 5; i++)


        }

        private void ly_machinepart_process_workDataGridView_SelectionChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(nowcontractNum) && null != this.ly_outsourcepart_process_workDataGridView.CurrentRow)
            {

                int nowOrder;
                string nowitemno = this.ly_outsourcepart_process_workDataGridView.CurrentRow.Cells["物料编号3"].Value.ToString();

                if ("" != this.ly_outsourcepart_process_workDataGridView.CurrentRow.Cells["工序编号"].Value.ToString())
                {
                    nowOrder = int.Parse(this.ly_outsourcepart_process_workDataGridView.CurrentRow.Cells["工序编号"].Value.ToString());
                }
                else
                {
                    nowOrder = 0;
                }




                this.ly_outsource_order_detail_selTableAdapter.Fill(this.lYOutsourceData.ly_outsource_order_detail_sel, nowcontractNum, nowitemno, nowOrder,nowaddcode );

                if (null == this.ly_production_order_detailDataGridView.CurrentRow)
                {

                    this.ly_outsource_order_inspectionTableAdapter.Fill(this.lYOutsourceData.ly_outsource_order_inspection, -1);
                    return;
                }


            }
            else
            {
                //this.ly_outsource_order_detail_selTableAdapter.Fill(this.lYOutsourceData.ly_outsource_order_detail_sel, "", "", 0,"");
            }
        }

        private void ly_production_order_detailDataGridView_SelectionChanged(object sender, EventArgs e)
        {
            if (null == this.ly_production_order_detailDataGridView.CurrentRow)
            {
                
                //this.ly_outsource_order_inspectionTableAdapter.Fill(this.lYOutsourceData.ly_outsource_order_inspection, -1);
                return;
            }

            int detailId = int.Parse(this.ly_production_order_detailDataGridView.CurrentRow.Cells["orderdetail_id"].Value.ToString());

            this.ly_outsource_order_inspectionTableAdapter.Fill(this.lYOutsourceData.ly_outsource_order_inspection, detailId);
        }



        private void bindingNavigatorAddNewItem_Click(object sender, EventArgs e)
        {
            if (null == this.ly_outsource_contract_detailQClistDataGridView.CurrentRow) return;
            
            string now_contractcode = this.ly_outsource_contract_detailQClistDataGridView.CurrentRow.Cells["合同编号0"].Value.ToString();
            string nowitemno = this.ly_outsource_contract_detailQClistDataGridView.CurrentRow.Cells["物料编号0"].Value.ToString();
            string nowaddcode = this.ly_outsource_contract_detailQClistDataGridView.CurrentRow.Cells["加工码0"].Value.ToString();
            decimal nowcontract_count =decimal .Parse ( this.ly_outsource_contract_detailQClistDataGridView.CurrentRow.Cells["采购数量0"].Value.ToString());

            int nowprocess_order = int.Parse (this.ly_outsourcepart_process_workDataGridView.CurrentRow.Cells["工序编号"].Value.ToString());
            int nowsequence_number =int .Parse ( this.ly_outsourcepart_process_workDataGridView.CurrentRow.Cells["sequence_number"].Value.ToString());

            string noworder = this.ly_outsourcepart_process_workDataGridView.CurrentRow.Cells["工序名称"].Value.ToString();




            string message1 = "确认分配(合同：" + now_contractcode + " 工序:" + noworder + ")任务吗?";
            string caption1 = "提示...";
            MessageBoxButtons buttons1 = MessageBoxButtons.YesNo;

            DialogResult result1;



            result1 = MessageBox.Show(message1, caption1, buttons1,
            MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

            if (result1 == DialogResult.Yes)
            {

                ///////////////////////////////////////////////////////////////// 

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

                int nowrowIndex = this.ly_outsourcepart_process_workDataGridView.CurrentRow.Index;
                string nowordername = this.ly_outsourcepart_process_workDataGridView.CurrentRow.Cells["工序名称"].Value.ToString();

                int nowordernum = int.Parse(this.ly_outsourcepart_process_workDataGridView.CurrentRow.Cells["工序编号"].Value.ToString());


                if (1 != nowordernum)
                //if ("下料" != nowordername)
                {
                    if (!string.IsNullOrEmpty(this.ly_outsourcepart_process_workDataGridView.Rows[nowrowIndex - 1].Cells["本序合格"].Value.ToString()))
                    {


                        up_quality = decimal.Parse(this.ly_outsourcepart_process_workDataGridView.Rows[nowrowIndex - 1].Cells["本序合格"].Value.ToString());
                    }
                    else
                    {

                        up_quality = 0;
                    }

                    if (!string.IsNullOrEmpty(this.ly_outsourcepart_process_workDataGridView.Rows[nowrowIndex - 1].Cells["本序可用"].Value.ToString()))
                    {


                        up_canuse = decimal.Parse(this.ly_outsourcepart_process_workDataGridView.Rows[nowrowIndex - 1].Cells["本序可用"].Value.ToString());
                    }
                    else
                    {

                        up_canuse = 0;
                    }

                    if (!string.IsNullOrEmpty(this.ly_outsourcepart_process_workDataGridView.Rows[nowrowIndex - 1].Cells["返修合格"].Value.ToString()))
                    {


                        up_remake = decimal.Parse(this.ly_outsourcepart_process_workDataGridView.Rows[nowrowIndex - 1].Cells["返修合格"].Value.ToString());
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
                    if (!string.IsNullOrEmpty(this.ly_outsourcepart_process_workDataGridView.CurrentRow.Cells["跟单数量"].Value.ToString()))
                    {
                        up_order_count = decimal.Parse(this.ly_outsourcepart_process_workDataGridView.CurrentRow.Cells["跟单数量"].Value.ToString());
                    }
                    else
                    {
                        up_order_count = 0;
                    }

                }


                if (!string.IsNullOrEmpty(this.ly_outsourcepart_process_workDataGridView.CurrentRow.Cells["本序数量"].Value.ToString()))
                {
                    order_count = decimal.Parse(this.ly_outsourcepart_process_workDataGridView.CurrentRow.Cells["本序数量"].Value.ToString());
                }
                else
                {
                    order_count = 0;
                }



                decimal arrange_count = 0;

                if (null != this.ly_outsource_contract_detailQClistDataGridView.CurrentRow)
                {               

                    foreach (DataGridViewRow dgr in ly_production_order_detailDataGridView.Rows)
                    {                               

                        if (string.IsNullOrEmpty(dgr.Cells["数量3"].Value.ToString())) continue;
                        arrange_count = arrange_count + decimal.Parse(dgr.Cells["数量3"].Value.ToString());



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

                   

                   







                    this.ly_outsource_order_detail_selBindingSource.AddNew();

                    this.ly_production_order_detailDataGridView.CurrentRow.Cells["合同编号3"].Value = now_contractcode;
                    this.ly_production_order_detailDataGridView.CurrentRow.Cells["物料编号33"].Value = nowitemno;
                    this.ly_production_order_detailDataGridView.CurrentRow.Cells["加工码3"].Value = nowaddcode;
                    this.ly_production_order_detailDataGridView.CurrentRow.Cells["合同数量3"].Value = nowcontract_count;

                    this.ly_production_order_detailDataGridView.CurrentRow.Cells["工序3"].Value = nowprocess_order;
                    this.ly_production_order_detailDataGridView.CurrentRow.Cells["sequence_number3"].Value = nowsequence_number;




                    this.ly_production_order_detailDataGridView.CurrentRow.Cells["准终3"].Value = this.ly_outsourcepart_process_workDataGridView.CurrentRow.Cells["准终时间"].Value;
                    this.ly_production_order_detailDataGridView.CurrentRow.Cells["实际准终3"].Value = this.ly_outsourcepart_process_workDataGridView.CurrentRow.Cells["准终时间"].Value;

                    this.ly_production_order_detailDataGridView.CurrentRow.Cells["单件工时3"].Value = this.ly_outsourcepart_process_workDataGridView.CurrentRow.Cells["单件工时"].Value;
                    this.ly_production_order_detailDataGridView.CurrentRow.Cells["实际单件工时3"].Value = this.ly_outsourcepart_process_workDataGridView.CurrentRow.Cells["单件工时"].Value;

                    this.ly_production_order_detailDataGridView.CurrentRow.Cells["准终累加3"].Value = this.ly_outsourcepart_process_workDataGridView.CurrentRow.Cells["准终累加"].Value;
                    this.ly_production_order_detailDataGridView.CurrentRow.Cells["单件工时累加3"].Value = this.ly_outsourcepart_process_workDataGridView.CurrentRow.Cells["单件累加"].Value;
                                                                                  
                    this.ly_production_order_detailDataGridView.CurrentRow.Cells["工时单价3"].Value = this.ly_outsourcepart_process_workDataGridView.CurrentRow.Cells["工时单价"].Value;
                    this.ly_production_order_detailDataGridView.CurrentRow.Cells["材料单价3"].Value = this.ly_outsourcepart_process_workDataGridView.CurrentRow.Cells["物料单价"].Value;

                    this.ly_production_order_detailDataGridView.CurrentRow.Cells["id_order"].Value = 0;


                    this.ly_production_order_detailDataGridView.CurrentRow.Cells["数量3"].Value = up_order_count - arrange_count;

                    this.ly_production_order_detailDataGridView.CurrentRow.Cells["调度3"].Value = SQLDatabase.nowUserName();

                    this.ly_production_order_detailDataGridView.CurrentRow.Cells["排序日期3"].Value = SQLDatabase.GetNowdate();
                    this.ly_production_order_detailDataGridView.CurrentRow.Cells["外协3"].Value = "False";

                    this.ly_production_order_detailDataGridView.CurrentRow.Cells["工号3"].Value = queryForm.Result;
                    this.ly_production_order_detailDataGridView.CurrentRow.Cells["姓名3"].Value = queryForm.Result1;



                    SaveTask();
                }

            }
        }

        private void SaveTask()
        {

            this.ly_production_order_detailDataGridView.EndEdit();

            this.Validate();
            this.ly_outsource_order_detail_selBindingSource.EndEdit();

            this.ly_outsource_order_detail_selTableAdapter.Update(this.lYOutsourceData .ly_outsource_order_detail_sel);
            ////////////////////////////////////////////////

            if (null != this.ly_outsource_contract_detailQClistDataGridView.CurrentRow && null != this.ly_outsourcepart_process_workDataGridView.CurrentRow)
            {
                int detail_Id;

                if (null != this.ly_production_order_detailDataGridView.CurrentRow)
                {
                    detail_Id = int.Parse(this.ly_production_order_detailDataGridView.CurrentRow.Cells["orderdetail_id"].Value.ToString());
                }
                else
                {
                    detail_Id = 0;
                }


                int nowOrder;


                if ("" != this.ly_outsourcepart_process_workDataGridView.CurrentRow.Cells["工序编号"].Value.ToString())
                {
                    nowOrder = int.Parse(this.ly_outsourcepart_process_workDataGridView.CurrentRow.Cells["工序编号"].Value.ToString());
                }
                else
                {
                    nowOrder = 0;
                }
                ////////////////////////////////////////////////////////////

                

                string nowplannum = this.ly_outsource_contract_detailQClistDataGridView.CurrentRow.Cells["计划编号0"].Value.ToString();

                string nowitem = this.ly_outsource_contract_detailQClistDataGridView.CurrentRow.Cells["物料编号0"].Value.ToString();
                nowcontractNum = this.ly_outsource_contract_detailQClistDataGridView.CurrentRow.Cells["合同编号0"].Value.ToString();
                now_order_id = this.ly_outsource_contract_detailQClistDataGridView.CurrentRow.Cells["orderid0"].Value.ToString();
                nowaddcode = this.ly_outsource_contract_detailQClistDataGridView.CurrentRow.Cells["加工码0"].Value.ToString();
                int id2 = int.Parse(this.ly_outsource_contract_detailQClistDataGridView.CurrentRow.Cells["id200"].Value.ToString());
                this.ly_outsourcepart_process_workTableAdapter.Fill(this.lYOutsourceData.ly_outsourcepart_process_work, nowitem, nowcontractNum, id2, nowaddcode);

                ////////////////////////////////////////////////////////////



                this.ly_outsourcepart_process_workBindingSource.Position = this.ly_outsourcepart_process_workBindingSource.Find("工序编号", nowOrder);
                this.ly_outsource_order_detail_selBindingSource.Position = this.ly_outsource_order_detail_selBindingSource.Find("id", detail_Id);



                //this.ly_production_order_detailTableAdapter.Fill(this.lYMaterielRequirements.ly_production_order_detail, nowproductionorderNum, nowOrder);
                /////////////////////////////////////////////////////

            }
            //else
            //{
            //    this.ly_production_order_detailTableAdapter.Fill(this.lYMaterielRequirements.ly_production_order_detail, "", 0);
            //}

        }

        private bool  CheckFinished()
        {

            if ("True" == this.orderfinished  )
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
        private string GetMaxInspection()
        {
           

            SqlConnection sqlConnection1 = new SqlConnection(SQLDatabase.Connectstring);
            SqlCommand cmd = new SqlCommand();

            string MaxInspectionNum = "";

            cmd.Parameters.Add("@Production_mode", SqlDbType.VarChar);
            cmd.Parameters["@Production_mode"].Value = "JYWX";


            cmd.CommandText = "LY_GetMax_InspectionNum_Outsource";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = sqlConnection1;

            sqlConnection1.Open();
            MaxInspectionNum = cmd.ExecuteScalar().ToString();
            sqlConnection1.Close();



            return MaxInspectionNum;
        }

        private void SaveChanged()
        {

            this.ly_production_order_inspectionDataGridView.EndEdit();

            this.Validate();
            this.ly_outsource_order_inspectionBindingSource.EndEdit();

            this.ly_outsource_order_inspectionTableAdapter.Update(this.lYOutsourceData .ly_outsource_order_inspection);

            int detailId = int.Parse(this.ly_production_order_detailDataGridView.CurrentRow.Cells["orderdetail_id"].Value.ToString());
            int orderNum = int.Parse(this.ly_outsourcepart_process_workDataGridView.CurrentRow.Cells["工序编号"].Value.ToString());

            if (this .tabPage2  == this.tabControl1.SelectedTab )
            {
                string nowplannum = this.ly_outsource_contract_detailQClistDataGridView.CurrentRow.Cells["计划编号0"].Value.ToString();
                //string now_order_id = this.ly_production_orderDataGridView.CurrentRow.Cells["orderId"].Value.ToString();
                this.ly_outsource_contract_detailQCDataGridView.SelectionChanged -= ly_production_orderDataGridView_SelectionChanged;
                this.ly_outsource_contract_detailQCTableAdapter.Fill(this.lYOutsourceData.ly_outsource_contract_detailQC, nowplannum);
                this.ly_outsource_contract_detailQCDataGridView.SelectionChanged += ly_production_orderDataGridView_SelectionChanged;

                this.ly_outsource_contract_detailQCBindingSource.Position = this.ly_outsource_contract_detailQCBindingSource.Find("id", now_order_id);
                this.ly_outsourcepart_process_workBindingSource.Position = this.ly_outsourcepart_process_workBindingSource.Find("工序编号", orderNum);
                this.ly_outsource_order_detail_selBindingSource.Position = this.ly_outsource_order_detail_selBindingSource.Find("id", detailId);
            }

            if (this .tabPage1  == this.tabControl1.SelectedTab )
            {

                this.ly_outsource_contract_detailQClistDataGridView.SelectionChanged -= this.lY_productionorder_listDataGridView_SelectionChanged;
                this.ly_outsource_contract_detailMachinelistTableAdapter.Fill(this.lYOutsourceData.ly_outsource_contract_detailMachinelist , DateTime.Parse(this.dateTimePicker1.Text).Date, DateTime.Parse(this.dateTimePicker2.Text).Date);
                this.ly_outsource_contract_detailQClistDataGridView.SelectionChanged += this.lY_productionorder_listDataGridView_SelectionChanged;

                this.ly_outsource_contract_detailMachinelistBindingSource.Position = this.ly_outsource_contract_detailMachinelistBindingSource.Find("id", now_order_id);
                this.ly_outsourcepart_process_workBindingSource.Position = this.ly_outsourcepart_process_workBindingSource.Find("工序编号", orderNum);
                this.ly_outsource_order_detail_selBindingSource.Position = this.ly_outsource_order_detail_selBindingSource.Find("id", detailId);

            }


          

           
        }

        private void ly_production_order_inspectionDataGridView_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {

            if (CheckFinished()) return;
            
            DataGridView dgv = sender as DataGridView;


            string nowordername = this.ly_outsourcepart_process_workDataGridView.CurrentRow.Cells["工序名称"].Value.ToString();

            //if ("下料" == nowordername) return;

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

                   /////////////////////////////////////////////////////////////////////////////////////////////

                    decimal inspect_count;
                    //decimal qualified_count;
                    //decimal waste_count;

                    if (!string.IsNullOrEmpty(this.ly_production_order_detailDataGridView.CurrentRow.Cells["数量"].Value.ToString()))
                    {
                        inspect_count = decimal.Parse(this.ly_production_order_detailDataGridView.CurrentRow.Cells["数量"].Value.ToString());
                    }
                    else
                    {
                        inspect_count = 0;
                    }

                    decimal send_inspect_count = 0;

                    if (null != this.ly_outsource_contract_detailQCDataGridView.CurrentRow)
                    {

                        foreach (DataGridViewRow dgr in ly_production_order_inspectionDataGridView.Rows)
                        {

                            if (string.IsNullOrEmpty(dgr.Cells["送检"].Value.ToString())) continue;
                            send_inspect_count = send_inspect_count + decimal.Parse(dgr.Cells["送检"].Value.ToString());



                        }
                    }

                    if (send_inspect_count > inspect_count)
                    {

                        dgv.CurrentRow.Cells["送检"].Value = queryForm.OldValue;
                        
                        MessageBox.Show("送交检验数量不能大于安排的加工数量, 操作取消", "注意");

                        return;

                    }

                    //////////////////////////////////////////////////////////////////////////////////////////////

                    if (!CheckInput(dgv))
                    {
                        dgv.CurrentRow.Cells["送检"].Value = queryForm.OldValue ;
                        return;
                    }
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
                    //dgv.CurrentRow.Cells["discount_money"].Value = DBNull.Value;

                    //dgv.CurrentRow.Cells["approve_flag"].Value = "False";
                    SaveChanged();


                    //CountPlanStru();

                }
                else
                {
                   

                }
                return;
                
                
                /////////////////////////////////////

                //DatePicker queryForm = new DatePicker();
                //queryForm.Pt = pt;

                //if (null != (dgv.CurrentCell.Value))
                //    queryForm.NowDate = dgv.CurrentCell.Value.ToString();

                //queryForm.ShowDialog();



                //if (null != queryForm.NowDate)
                //{

                //    dgv.CurrentRow.Cells["检验日期"].Value = queryForm.NowDate;
                //    SaveChanged();

                //}
                //return;
            }









            ///////////////////////////////////////////////////////

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

            ///////////////////////////////////////////////////////////
            //if ("可用" == dgv.CurrentCell.OwningColumn.Name)
            //{

            //    ChangeValue queryForm = new ChangeValue();

            //    queryForm.OldValue = dgv.CurrentCell.Value.ToString();
            //    queryForm.NewValue = "";
            //    queryForm.ChangeMode = "value";
            //    queryForm.ShowDialog();




            //    if (queryForm.NewValue != "")
            //    {


            //        if (string.IsNullOrEmpty(queryForm.NewValue))
            //        {
            //            dgv.CurrentRow.Cells["可用"].Value = DBNull.Value;
            //        }
            //        else
            //        {
            //            if (decimal.Parse(queryForm.NewValue) > 0)
            //            {

            //                dgv.CurrentRow.Cells["可用"].Value = queryForm.NewValue;
            //            }
            //        }




            //        if (!CheckCanuse(dgv))
            //        {
            //            if (string.IsNullOrEmpty(queryForm.OldValue))
            //            {
            //                dgv.CurrentRow.Cells["可用"].Value = DBNull.Value;
            //            }
            //            else
            //            {
            //                dgv.CurrentRow.Cells["可用"].Value = queryForm.OldValue;
            //            }

            //            return;
            //        }



            //        SaveChanged();


            //        //CountPlanStru();

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


            /////////////////////////////////////////////////////////

            ///////////////////////////////

            //if ("审查意见" == dgv.CurrentCell.OwningColumn.Name)
            //{

            //    ChangeValue queryForm = new ChangeValue();

            //    queryForm.OldValue = dgv.CurrentCell.Value.ToString();
            //    queryForm.NewValue = "";
            //    queryForm.ChangeMode = "longstring";
            //    queryForm.ShowDialog();




            //    if (queryForm.NewValue != "")
            //    {
            //        dgv.CurrentRow.Cells["审查意见"].Value = queryForm.NewValue;
            //        //dgv.CurrentRow.Cells["discount_money"].Value = DBNull.Value;

            //        //dgv.CurrentRow.Cells["approve_flag"].Value = "False";
            //        SaveChanged();


            //        //CountPlanStru();

            //    }
            //    else
            //    {

            //    }
            //    return;

            //}

            ///////////////////////////////
        }

        private bool  CheckInput(DataGridView dgv)
        {
            decimal  qualified_count;
            decimal inspect_count;
            decimal canuse_count;


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

            if ((qualified_count + canuse_count) > inspect_count)
            {
                MessageBox.Show("合格数量不能大于送检数量", "注意");

                return false;
            }
            else
            {
                dgv.CurrentRow.Cells["废品"].Value = inspect_count - ( qualified_count + canuse_count);
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
                MessageBox.Show("可用数量不能大于废品数量", "注意");

                return false;
            }
            else
            {
                dgv.CurrentRow.Cells["废品"].Value = inspect_count - (qualified_count + canuse_count);
                dgv.CurrentRow.Cells["技术审查"].Value =SQLDatabase.nowUserName();
                dgv.CurrentRow.Cells["审查日期"].Value = SQLDatabase.GetNowdate().ToString();

                
                return true;
            }
        }

        private void bindingNavigatorDeleteItem_Click(object sender, EventArgs e)
        {
            if (null == this.ly_production_order_detailDataGridView.CurrentRow) return;

            string noworder = this.ly_outsourcepart_process_workDataGridView.CurrentRow.Cells["工序名称"].Value.ToString();

            if ("下料" == noworder || "外协" == noworder)
            {
                MessageBox.Show("不属于机加工序，不能删除(实需删除，请先联系技术负责人员)", "注意");
                return;
            }
            //if (this.ly_material_plan_detailDataGridView.RowCount > 0)
            //{
            //    MessageBox.Show("计划已有物料记录，不能删除(实需删除，请先删除该计划的物料记录)", "注意");
            //    return;

            //}

            if (!checkqualityRec())
            {

                MessageBox.Show("任务已有检验记录,不能修改, 操作取消", "注意");

                return;

            }

            string diaodu = this.ly_production_order_detailDataGridView.CurrentRow.Cells["调度3"].Value.ToString();

            if (diaodu != SQLDatabase.nowUserName())
            {
                MessageBox.Show("请调度:" + diaodu + "删除", "注意");
                return;
            }

                       
            string nowName = this.ly_production_order_detailDataGridView.CurrentRow.Cells["姓名3"].Value.ToString();
            
            string message1 = "删除" + nowName + " (工序：" + noworder + " )任务安排，继续吗？";
            string caption1 = "提示...";
            MessageBoxButtons buttons1 = MessageBoxButtons.YesNo;

            DialogResult result1;



            result1 = MessageBox.Show(message1, caption1, buttons1,
            MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

            if (result1 == DialogResult.Yes)
            {

                this.ly_outsource_order_detail_selBindingSource.RemoveCurrent();


                SaveTask();


            }
        }
              

        private void lY_productionorder_listDataGridView_SelectionChanged(object sender, EventArgs e)
        {
            if (null == this.ly_outsource_contract_detailQClistDataGridView.CurrentRow)
            {

                this.ly_outsourcepart_process_workTableAdapter.Fill(this.lYOutsourceData.ly_outsourcepart_process_work, "", "", 0, "");
                return;
            }

            string  nowplannum = this.ly_outsource_contract_detailQClistDataGridView.CurrentRow.Cells["计划编号0"].Value.ToString();

            string nowitem = this.ly_outsource_contract_detailQClistDataGridView.CurrentRow.Cells["物料编号0"].Value.ToString();
            nowcontractNum = this.ly_outsource_contract_detailQClistDataGridView.CurrentRow.Cells["合同编号0"].Value.ToString();
            now_order_id = this.ly_outsource_contract_detailQClistDataGridView.CurrentRow.Cells["orderid0"].Value.ToString();
            nowaddcode = this.ly_outsource_contract_detailQClistDataGridView.CurrentRow.Cells["加工码0"].Value.ToString();
            int id2 = int.Parse(this.ly_outsource_contract_detailQClistDataGridView.CurrentRow.Cells["id200"].Value.ToString());
            this.ly_outsourcepart_process_workTableAdapter.Fill(this.lYOutsourceData.ly_outsourcepart_process_work, nowitem, nowcontractNum, id2,nowaddcode );

           
            this.ly_material_plan_mainDataGridView.RowEnter -= ly_material_plan_mainDataGridView_RowEnter;
            this.ly_material_plan_mainBindingSource.Position = this.ly_material_plan_mainBindingSource.Find("计划编号", nowplannum);
            this.ly_material_plan_mainDataGridView.RowEnter += ly_material_plan_mainDataGridView_RowEnter;

     
           
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            TabControl tc = sender as TabControl;

            if (this .tabPage1  == tc.SelectedTab )
            {


                if (null == this.ly_outsource_contract_detailQClistDataGridView.CurrentRow)
                {

                    this.ly_outsourcepart_process_workTableAdapter.Fill(this.lYOutsourceData .ly_outsourcepart_process_work, "", "",0,"");
                    return;
                }

                string nowplannum = this.ly_outsource_contract_detailQClistDataGridView.CurrentRow.Cells["计划编号0"].Value.ToString();

                string nowitem = this.ly_outsource_contract_detailQClistDataGridView.CurrentRow.Cells["物料编号0"].Value.ToString();
                nowcontractNum = this.ly_outsource_contract_detailQClistDataGridView.CurrentRow.Cells["合同编号0"].Value.ToString();
                now_order_id = this.ly_outsource_contract_detailQClistDataGridView.CurrentRow.Cells["orderid0"].Value.ToString();
                nowaddcode = this.ly_outsource_contract_detailQClistDataGridView.CurrentRow.Cells["加工码0"].Value.ToString();
                int id2 = int.Parse(this.ly_outsource_contract_detailQClistDataGridView.CurrentRow.Cells["id200"].Value.ToString());
                this.ly_outsourcepart_process_workTableAdapter.Fill(this.lYOutsourceData.ly_outsourcepart_process_work, nowitem, nowcontractNum, id2, nowaddcode);

                this.ly_material_plan_mainDataGridView.RowEnter -= ly_material_plan_mainDataGridView_RowEnter;
                this.ly_material_plan_mainBindingSource.Position = this.ly_material_plan_mainBindingSource.Find("计划编号", nowplannum);
                this.ly_material_plan_mainDataGridView.RowEnter += ly_material_plan_mainDataGridView_RowEnter;

               //this.ly_outsourcepart_process_workTableAdapter.Fill(this.lYOutsourceData .ly_outsourcepart_process_work, nowitemno, nowproductionorderNum,1);
            }

            if (this .tabPage2  == tc.SelectedTab )
            {
                if (null != this.ly_outsource_contract_detailQCDataGridView.CurrentRow)
                {

                    string nowitem = this.ly_outsource_contract_detailQCDataGridView.CurrentRow.Cells["物料编码"].Value.ToString();
                    nowcontractNum = this.ly_outsource_contract_detailQCDataGridView.CurrentRow.Cells["跟单编号"].Value.ToString();
                    now_order_id = this.ly_outsource_contract_detailQCDataGridView.CurrentRow.Cells["orderId"].Value.ToString();
                    nowaddcode = this.ly_outsource_contract_detailQCDataGridView.CurrentRow.Cells["加工码"].Value.ToString();
                    int id2 = int.Parse(this.ly_outsource_contract_detailQClistDataGridView.CurrentRow.Cells["id20"].Value.ToString());
                    this.ly_outsourcepart_process_workTableAdapter.Fill(this.lYOutsourceData.ly_outsourcepart_process_work, nowitem, nowcontractNum, id2,nowaddcode );
                    //set_processOrder_Num();


                }
                else
                {
                    this.ly_outsourcepart_process_workTableAdapter.Fill(this.lYOutsourceData .ly_outsourcepart_process_work, "", "",0,"");
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ly_outsource_contract_detailQClistDataGridView.SelectionChanged -= lY_productionorder_listDataGridView_SelectionChanged;
            this.ly_outsource_contract_detailMachinelistTableAdapter.Fill(this.lYOutsourceData.ly_outsource_contract_detailMachinelist , DateTime.Parse(this.dateTimePicker1.Text).Date, DateTime.Parse(this.dateTimePicker2.Text).Date);
            ly_outsource_contract_detailQClistDataGridView.SelectionChanged += lY_productionorder_listDataGridView_SelectionChanged;
        }


        private void toolStripTextBox2_Enter(object sender, EventArgs e)
        {
            toolStripTextBox2.Text = "";

            this.ly_outsource_contract_detailMachinelistBindingSource.Filter = "";
        }

        private void toolStripTextBox2_KeyUp(object sender, KeyEventArgs e)
        {
            string dFilter = "";

            dFilter = GetDataGridviewMultiFilterString.DGVMultiFilterString(this.ly_outsource_contract_detailQClistDataGridView, this.toolStripTextBox2.Text);

            if (null == dFilter)
                dFilter = "";

            this.ly_outsource_contract_detailMachinelistBindingSource.Filter = dFilter;
        }
        private bool checkqualityRec()
        {
            decimal qualitied_count = 0;
            decimal waste_count = 0;

            
            if (!string.IsNullOrEmpty(this.ly_production_order_detailDataGridView.CurrentRow.Cells["合格3"].Value.ToString()))
            {
                qualitied_count = decimal.Parse(this.ly_production_order_detailDataGridView.CurrentRow.Cells["合格3"].Value.ToString());
            }
            else
            {
                qualitied_count = 0;
            }

            if (!string.IsNullOrEmpty(this.ly_production_order_detailDataGridView.CurrentRow.Cells["废品3"].Value.ToString()))
            {
                waste_count = decimal.Parse(this.ly_production_order_detailDataGridView.CurrentRow.Cells["废品3"].Value.ToString());
            }
            else
            {
                waste_count = 0;
            }


            if ((qualitied_count + waste_count) > 0)
            {
                string nowordername = this.ly_outsourcepart_process_workDataGridView.CurrentRow.Cells["工序名称"].Value.ToString();

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

            string noworder = this.ly_outsourcepart_process_workDataGridView.CurrentRow.Cells["工序名称"].Value.ToString();

            if ("下料" == noworder || "外协" == noworder)
            {
                MessageBox.Show("不属于机加工序，不能修改(实需修改，请先联系技术负责人员)", "注意");
                return;
            }

            if ("外协单价" == dgv.CurrentCell.OwningColumn.Name)
            {


                if ("True" == dgv.CurrentRow.Cells["approve"].Value.ToString())

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
                    //dgv.CurrentRow.Cells["discount_money"].Value = DBNull.Value;

                    //dgv.CurrentRow.Cells["approve_flag"].Value = "False";
                    SaveTask();


                    //CountPlanStru();

                }
                else
                {
                    dgv.CurrentRow.Cells["外协单价"].Value = DBNull.Value; ;

                    SaveTask();
                    //hT_Manage_ItemDataGridView.CurrentRow.Cells["apply_money"].Value = queryForm.NewValue;
                    //dgv.CurrentRow.Cells["discount_money"].Value = DBNull.Value;
                    //dgv.CurrentRow.Cells["apply_money"].Value = DBNull.Value;
                    //dgv.CurrentRow.Cells["approve_flag"].Value = "False";
                    //SaveChanged();

                }
                return;

            }

            ///////////////////////////////////////////////////////

            if (!checkqualityRec() && "系统管理员" != SQLDatabase.nowUserName())
            {

                MessageBox.Show("任务已有检验记录,不能修改, 操作取消", "注意");

                return;

            }


            if ("数量3" == dgv.CurrentCell.OwningColumn.Name)
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
                    dgv.CurrentRow.Cells["数量3"].Value = queryForm.NewValue;
                    ////////////////////////////////////////////////////////////////////////



                    ///////////////////////////////////////////////////////////////// 

                    decimal up_order_count;


                    decimal up_quality;
                    decimal up_canuse;
                    int nowordernum = int.Parse(this.ly_outsourcepart_process_workDataGridView.CurrentRow.Cells["工序编号"].Value.ToString());
                    int nowrowIndex = this.ly_outsourcepart_process_workDataGridView.CurrentRow.Index;
                    string nowordername = this.ly_outsourcepart_process_workDataGridView.CurrentRow.Cells["工序名称"].Value.ToString();

                    decimal up_remake;


                    if (1 != nowordernum)
                    //if ("下料" != nowordername)
                    {
                        if (!string.IsNullOrEmpty(this.ly_outsourcepart_process_workDataGridView.Rows[nowrowIndex - 1].Cells["本序合格"].Value.ToString()))
                        {


                            up_quality = decimal.Parse(this.ly_outsourcepart_process_workDataGridView.Rows[nowrowIndex - 1].Cells["本序合格"].Value.ToString());
                        }
                        else
                        {

                            up_quality = 0;
                        }

                        if (!string.IsNullOrEmpty(this.ly_outsourcepart_process_workDataGridView.Rows[nowrowIndex - 1].Cells["本序可用"].Value.ToString()))
                        {
                            up_canuse = decimal.Parse(this.ly_outsourcepart_process_workDataGridView.Rows[nowrowIndex - 1].Cells["本序可用"].Value.ToString());
                        }
                        else
                        {

                            up_canuse = 0;
                        }

                        if (!string.IsNullOrEmpty(this.ly_outsourcepart_process_workDataGridView.Rows[nowrowIndex - 1].Cells["返修合格"].Value.ToString()))
                        {

                            up_remake = decimal.Parse(this.ly_outsourcepart_process_workDataGridView.Rows[nowrowIndex - 1].Cells["返修合格"].Value.ToString());
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
                        if (!string.IsNullOrEmpty(this.ly_outsourcepart_process_workDataGridView.CurrentRow.Cells["跟单数量"].Value.ToString()))
                        {
                            up_order_count = decimal.Parse(this.ly_outsourcepart_process_workDataGridView.CurrentRow.Cells["跟单数量"].Value.ToString());
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


                    if (!string.IsNullOrEmpty(this.ly_outsourcepart_process_workDataGridView.CurrentRow.Cells["跟单数量"].Value.ToString()))
                    {
                        order_count = decimal.Parse(this.ly_outsourcepart_process_workDataGridView.CurrentRow.Cells["跟单数量"].Value.ToString());
                    }
                    else
                    {
                        order_count = 0;
                    }



                    decimal arrange_count = 0;

                    if (null != this.ly_outsource_contract_detailQClistDataGridView.CurrentRow)
                    {

                        foreach (DataGridViewRow dgr in ly_production_order_detailDataGridView.Rows)
                        {

                            if (string.IsNullOrEmpty(dgr.Cells["数量3"].Value.ToString())) continue;
                            arrange_count = arrange_count + decimal.Parse(dgr.Cells["数量3"].Value.ToString());

                            
                        }
                    }


                    if (arrange_count > up_order_count)
                    {

                        dgv.CurrentRow.Cells["数量3"].Value = queryForm.OldValue;

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

            if ("合格3" == dgv.CurrentCell.OwningColumn.Name)
            {


                return;
                string nowordername = this.ly_outsourcepart_process_workDataGridView.CurrentRow.Cells["工序名称"].Value.ToString();

                if ("下料" != nowordername) return;


                ChangeValue queryForm = new ChangeValue();

                queryForm.OldValue = dgv.CurrentCell.Value.ToString();
                queryForm.NewValue = "";
                queryForm.ChangeMode = "value";
                queryForm.ShowDialog();




                if (queryForm.NewValue != "")
                {
                    dgv.CurrentRow.Cells["合格3"].Value = queryForm.NewValue;

                    //////////////////////////////////////////////////////////////////////////////

                    int detail_Id = int.Parse(dgv.CurrentRow.Cells["orderdetail_id"].Value.ToString());



                    //string insstr = " delete ly_material_plan_main  where material_plan_num = '" + nowPlanNumber + "'";

                    string updstr = " update ly_outsource_order_inspection  " +
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

            if ("工号3" == dgv.CurrentCell.OwningColumn.Name || "姓名3" == dgv.CurrentCell.OwningColumn.Name)
            {



                if (!checkqualityRec() && "系统管理员" != SQLDatabase.nowUserName())
                {

                    MessageBox.Show("任务已有检验记录,不能修改, 操作取消", "注意");

                    return;

                }

                string outflag = this.ly_production_order_detailDataGridView.CurrentRow.Cells["外协3"].Value.ToString();
                string sel;

                if ("True" == outflag)
                {

                    sel = "SELECT  supplier_code as 工号, supplier_name as 姓名 FROM ly_supplier_list where sort_code='4'";
                }
                else
                {

                    sel = "SELECT  work_code as 工号, worker_name as 姓名 FROM ly_worker_list";
                }


                QueryForm queryForm = new QueryForm();


                queryForm.Sel = sel;
                queryForm.Constr = SQLDatabase.Connectstring;

                //Set the Column Collection to the filter Table
                //queryForm.SetSourceColumns(this.billMainDataSet.BalanceBill.Columns);

                queryForm.ShowDialog();


                if (queryForm.Result != "")
                {
                    dgv.CurrentRow.Cells["工号3"].Value = queryForm.Result;
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

            if ("实际准终3" == dgv.CurrentCell.OwningColumn.Name)
            {

                ChangeValue queryForm = new ChangeValue();

                queryForm.OldValue = dgv.CurrentCell.Value.ToString();
                queryForm.NewValue = "";
                queryForm.ChangeMode = "value";
                queryForm.ShowDialog();




                if (queryForm.NewValue != "")
                {
                    dgv.CurrentRow.Cells["实际准终3"].Value = queryForm.NewValue;
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

            if ("实际加工工时3" == dgv.CurrentCell.OwningColumn.Name)
            {

                ChangeValue queryForm = new ChangeValue();

                queryForm.OldValue = dgv.CurrentCell.Value.ToString();
                queryForm.NewValue = "";
                queryForm.ChangeMode = "value";
                queryForm.ShowDialog();




                if (queryForm.NewValue != "")
                {
                    dgv.CurrentRow.Cells["实际加工工时3"].Value = queryForm.NewValue;
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
            //        //dgv.CurrentRow.Cells["discount_money"].Value = DBNull.Value;

            //        //dgv.CurrentRow.Cells["approve_flag"].Value = "False";
            //        SaveTask();


            //        //CountPlanStru();

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

            /////////////////////////////////////////////////////////
            ///////////////////////////////////////////////////////

            if ("实际可用工时3" == dgv.CurrentCell.OwningColumn.Name)
            {

                ChangeValue queryForm = new ChangeValue();

                queryForm.OldValue = dgv.CurrentCell.Value.ToString();
                queryForm.NewValue = "";
                queryForm.ChangeMode = "value";
                queryForm.ShowDialog();




                if (queryForm.NewValue != "")
                {
                    dgv.CurrentRow.Cells["实际可用工时3"].Value = queryForm.NewValue;
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

            if ("实际废品工时3" == dgv.CurrentCell.OwningColumn.Name)
            {

                ChangeValue queryForm = new ChangeValue();

                queryForm.OldValue = dgv.CurrentCell.Value.ToString();
                queryForm.NewValue = "";
                queryForm.ChangeMode = "value";
                queryForm.ShowDialog();




                if (queryForm.NewValue != "")
                {
                    dgv.CurrentRow.Cells["实际废品工时3"].Value = queryForm.NewValue;
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

            if ("备注3" == dgv.CurrentCell.OwningColumn.Name)
            {

                ChangeValue queryForm = new ChangeValue();

                queryForm.OldValue = dgv.CurrentCell.Value.ToString();
                queryForm.NewValue = "";
                queryForm.ChangeMode = "longstring";
                queryForm.ShowDialog();




                if (queryForm.NewValue != "")
                {
                    dgv.CurrentRow.Cells["备注3"].Value = queryForm.NewValue;
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

            if ("排序日期3" == dgv.CurrentCell.OwningColumn.Name)
            {

                ChangeValue queryForm = new ChangeValue();

                queryForm.OldValue = dgv.CurrentCell.Value.ToString();
                queryForm.NewValue = "";
                queryForm.ChangeMode = "datetime";
                queryForm.ShowDialog();




                if (queryForm.NewValue != "")
                {
                    dgv.CurrentRow.Cells["排序日期3"].Value = queryForm.NewValue;
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

        private void ly_material_plan_mainDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void toolStripButton7_Click(object sender, EventArgs e)
        {
            if (null == this.ly_outsource_contract_detailQClistDataGridView.CurrentRow) return;

            string now_contractcode = this.ly_outsource_contract_detailQClistDataGridView.CurrentRow.Cells["合同编号0"].Value.ToString();
            string nowitemno = this.ly_outsource_contract_detailQClistDataGridView.CurrentRow.Cells["物料编号0"].Value.ToString();
            string nowaddcode = this.ly_outsource_contract_detailQClistDataGridView.CurrentRow.Cells["加工码0"].Value.ToString();
            decimal nowcontract_count = decimal.Parse(this.ly_outsource_contract_detailQClistDataGridView.CurrentRow.Cells["采购数量0"].Value.ToString());

            int nowprocess_order = int.Parse(this.ly_outsourcepart_process_workDataGridView.CurrentRow.Cells["工序编号"].Value.ToString());
            int nowsequence_number = int.Parse(this.ly_outsourcepart_process_workDataGridView.CurrentRow.Cells["sequence_number"].Value.ToString());

            string noworder = this.ly_outsourcepart_process_workDataGridView.CurrentRow.Cells["工序名称"].Value.ToString();

            
            string message1 = "确认分配(合同：" + now_contractcode + " 工序:" + noworder + ")任务吗?";
            string caption1 = "提示...";
            MessageBoxButtons buttons1 = MessageBoxButtons.YesNo;

            DialogResult result1;



            result1 = MessageBox.Show(message1, caption1, buttons1,
            MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

            if (result1 == DialogResult.Yes)



            {

                string sel = "SELECT  supplier_code as 工号, supplier_name as 姓名 FROM ly_supplier_list where sort_code='4'";


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

                decimal up_order_count;
                decimal order_count;

                decimal up_quality;
                decimal up_canuse;
                decimal up_remake;

                int nowrowIndex = this.ly_outsourcepart_process_workDataGridView.CurrentRow.Index;
                string nowordername = this.ly_outsourcepart_process_workDataGridView.CurrentRow.Cells["工序名称"].Value.ToString();

                int nowordernum = int.Parse(this.ly_outsourcepart_process_workDataGridView.CurrentRow.Cells["工序编号"].Value.ToString());


                if (1 != nowordernum)
                //if ("下料" != nowordername)
                {
                    if (!string.IsNullOrEmpty(this.ly_outsourcepart_process_workDataGridView.Rows[nowrowIndex - 1].Cells["本序合格"].Value.ToString()))
                    {


                        up_quality = decimal.Parse(this.ly_outsourcepart_process_workDataGridView.Rows[nowrowIndex - 1].Cells["本序合格"].Value.ToString());
                    }
                    else
                    {

                        up_quality = 0;
                    }

                    if (!string.IsNullOrEmpty(this.ly_outsourcepart_process_workDataGridView.Rows[nowrowIndex - 1].Cells["本序可用"].Value.ToString()))
                    {


                        up_canuse = decimal.Parse(this.ly_outsourcepart_process_workDataGridView.Rows[nowrowIndex - 1].Cells["本序可用"].Value.ToString());
                    }
                    else
                    {

                        up_canuse = 0;
                    }

                    if (!string.IsNullOrEmpty(this.ly_outsourcepart_process_workDataGridView.Rows[nowrowIndex - 1].Cells["返修合格"].Value.ToString()))
                    {


                        up_remake = decimal.Parse(this.ly_outsourcepart_process_workDataGridView.Rows[nowrowIndex - 1].Cells["返修合格"].Value.ToString());
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
                    if (!string.IsNullOrEmpty(this.ly_outsourcepart_process_workDataGridView.CurrentRow.Cells["跟单数量"].Value.ToString()))
                    {
                        up_order_count = decimal.Parse(this.ly_outsourcepart_process_workDataGridView.CurrentRow.Cells["跟单数量"].Value.ToString());
                    }
                    else
                    {
                        up_order_count = 0;
                    }

                }


                if (!string.IsNullOrEmpty(this.ly_outsourcepart_process_workDataGridView.CurrentRow.Cells["本序数量"].Value.ToString()))
                {
                    order_count = decimal.Parse(this.ly_outsourcepart_process_workDataGridView.CurrentRow.Cells["本序数量"].Value.ToString());
                }
                else
                {
                    order_count = 0;
                }


                decimal arrange_count = 0;

                if (null != this.ly_outsource_contract_detailQClistDataGridView.CurrentRow)
                {

                    foreach (DataGridViewRow dgr in ly_production_order_detailDataGridView.Rows)
                    {

                        if (string.IsNullOrEmpty(dgr.Cells["数量3"].Value.ToString())) continue;
                        arrange_count = arrange_count + decimal.Parse(dgr.Cells["数量3"].Value.ToString());



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


                    this.ly_outsource_order_detail_selBindingSource.AddNew();

                    //---------------------带出最近的一次外协单价


                    string strsql = SQLDatabase.Connectstring;
                    string sql = "ly_outmachine_contract_getPrice";
                    SqlConnection conStr = new SqlConnection(strsql);
                    SqlCommand comStr = new SqlCommand(sql, conStr);
                    comStr.CommandType = CommandType.StoredProcedure;

                    comStr.Parameters.Add("@workcode", SqlDbType.Text).Value = queryForm.Result;
                    comStr.Parameters.Add("@itemno", SqlDbType.Text).Value = ly_outsource_contract_detailQClistDataGridView.CurrentRow.Cells["物料编号0"].Value.ToString();
                    comStr.Parameters.Add("@processCode", SqlDbType.Int).Value = int.Parse(ly_outsourcepart_process_workDataGridView.CurrentRow.Cells["工序编号"].Value.ToString());
                    conStr.Open(); 
                     SqlDataAdapter SqlDataAdapter1 = new SqlDataAdapter(comStr);
                    DataTable DT = new DataTable();
                    SqlDataAdapter1.Fill(DT);
                    if (DT.Rows.Count >= 1 && decimal .Parse (DT.Rows[0]["contract_price"].ToString())>0)
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








                    this.ly_production_order_detailDataGridView.CurrentRow.Cells["合同编号3"].Value = now_contractcode;
                    this.ly_production_order_detailDataGridView.CurrentRow.Cells["物料编号33"].Value = nowitemno;
                    this.ly_production_order_detailDataGridView.CurrentRow.Cells["加工码3"].Value = nowaddcode;
                    this.ly_production_order_detailDataGridView.CurrentRow.Cells["合同数量3"].Value = nowcontract_count;

                    this.ly_production_order_detailDataGridView.CurrentRow.Cells["工序3"].Value = nowprocess_order;
                    this.ly_production_order_detailDataGridView.CurrentRow.Cells["sequence_number3"].Value = nowsequence_number;




                    this.ly_production_order_detailDataGridView.CurrentRow.Cells["准终3"].Value = this.ly_outsourcepart_process_workDataGridView.CurrentRow.Cells["准终时间"].Value;
                    this.ly_production_order_detailDataGridView.CurrentRow.Cells["实际准终3"].Value = this.ly_outsourcepart_process_workDataGridView.CurrentRow.Cells["准终时间"].Value;

                    this.ly_production_order_detailDataGridView.CurrentRow.Cells["单件工时3"].Value = this.ly_outsourcepart_process_workDataGridView.CurrentRow.Cells["单件工时"].Value;
                    this.ly_production_order_detailDataGridView.CurrentRow.Cells["实际单件工时3"].Value = this.ly_outsourcepart_process_workDataGridView.CurrentRow.Cells["单件工时"].Value;

                    this.ly_production_order_detailDataGridView.CurrentRow.Cells["准终累加3"].Value = this.ly_outsourcepart_process_workDataGridView.CurrentRow.Cells["准终累加"].Value;
                    this.ly_production_order_detailDataGridView.CurrentRow.Cells["单件工时累加3"].Value = this.ly_outsourcepart_process_workDataGridView.CurrentRow.Cells["单件累加"].Value;

                    this.ly_production_order_detailDataGridView.CurrentRow.Cells["工时单价3"].Value = this.ly_outsourcepart_process_workDataGridView.CurrentRow.Cells["工时单价"].Value;
                    this.ly_production_order_detailDataGridView.CurrentRow.Cells["材料单价3"].Value = this.ly_outsourcepart_process_workDataGridView.CurrentRow.Cells["物料单价"].Value;

                    this.ly_production_order_detailDataGridView.CurrentRow.Cells["id_order"].Value = 0;


                    this.ly_production_order_detailDataGridView.CurrentRow.Cells["数量3"].Value = up_order_count - arrange_count;

                    this.ly_production_order_detailDataGridView.CurrentRow.Cells["调度3"].Value = SQLDatabase.nowUserName();

                    this.ly_production_order_detailDataGridView.CurrentRow.Cells["排序日期3"].Value = SQLDatabase.GetNowdate();
                    this.ly_production_order_detailDataGridView.CurrentRow.Cells["外协3"].Value = "True";
                    this.ly_production_order_detailDataGridView.CurrentRow.Cells["工号3"].Value = queryForm.Result;
                    this.ly_production_order_detailDataGridView.CurrentRow.Cells["姓名3"].Value = queryForm.Result1;



                    SaveTask();
                }

            }
        }

       

      

     /////////////////////////////////////////////////
      
    }
}
