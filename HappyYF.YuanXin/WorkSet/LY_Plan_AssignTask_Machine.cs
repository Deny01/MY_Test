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
using System.Threading;
using DataGridFilter;
using HappyYF.Infrastructure.Repositories;

namespace HappyYF.YuanXin.WorkSet
{
    public partial class LY_Plan_AssignTask_Machine : Form
    {

        string formState = "View";
        Point pt = new Point();

        public LY_Plan_AssignTask_Machine()
        {
            InitializeComponent();

            //this.ly_material_plan_mainTableAdapter1.CommandTimeout = 0;
        }

        private void LY_Machine_Load(object sender, EventArgs e)
        {
            this.notifyIcon1.Visible = false;

            
            //定时函数
            //this.timer1.Interval = 5000;
            //this.timer1.Start();

            toolStripButton4.Text = "隐藏计划界面";
            //this.splitContainer1.Panel1Collapsed = true;

            this.lY_productiontask_allplanTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;

            this.ly_production_job_materialAddTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            this.ly_Bom_expendTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;

       



            this.ly_material_plan_explodeTaskTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            this.ly_production_task_inspectionTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;

            this.lY_productiontask_selTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            this.ly_material_plan_detailTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            this.ly_material_plan_explodeTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            this.lY_TaskExplode_sumTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;

            //this.ly_material_plan_detailBindingSource.Filter = "oper_dept='01'";

            this.ly_material_plan_mainTableAdapter1.Connection.ConnectionString = SQLDatabase.Connectstring;
 
        
            this.ly_material_plan_mainBindingSource1.Filter = "unfinished_count>0 and oper_dept='01'";



           
            
            

            NewFrm.Show(this.ParentForm);
            Thread.Sleep(100);

            this.ly_material_plan_mainTableAdapter1.Fill(this.lYProductMange.ly_material_plan_main, "SCJH");
            NewFrm.Hide(this.ParentForm);

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
            


            this.ly_material_plan_mainBindingSource1.Filter = " oper_dept='01'  ";
            this.ly_material_plan_mainTableAdapter1.Fill(this.lYProductMange.ly_material_plan_main, "SCJH");

            this.toolStripButton9.Visible = true;
            this.toolStripButton1.Visible = false;

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



                    //计划编号TextBox.Text= this.ly_material_plan_mainDataGridView.Rows[e.RowIndex].Cells["计划编号"].Value.ToString();

                     
                    this.lY_productiontask_allplanTableAdapter.Fill(this.lYProductMange.LY_productiontask_allplan, parentId, "01");
           
                    this.ly_material_plan_detailTableAdapter.Fill(this.lYProductMange.ly_material_plan_detail, parentId,"01");

                    if (null == this.ly_material_plan_detailDataGridView.CurrentRow)
                    {
                        this.lY_productiontask_selTableAdapter.Fill(this.lYProductMange.LY_productiontask_sel, 0, "123");
                    }

                    //NewFrm.Show(this.ParentForm);
                    //this.lY_MaterielRequirementsExecuteTableAdapter.Fill(this.lYMaterielRequirements.LY_MaterielRequirementsExecute , parentId, "机加");
                    //NewFrm.Hide(this.ParentForm);
                }



             
            }

                  
                 
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            int parentId = int.Parse(this.ly_material_plan_mainDataGridView.CurrentRow.Cells["parentid"].Value.ToString());
            string planNum = this.ly_material_plan_mainDataGridView.CurrentRow.Cells["计划编号"].Value.ToString();


            this.ly_material_plan_detailTableAdapter.Fill(this.lYProductMange.ly_material_plan_detail, parentId, "01");
        }

        private string GetMaxProductionorder()
        {
            SqlConnection sqlConnection1 = new SqlConnection(SQLDatabase.Connectstring);
            SqlCommand cmd = new SqlCommand();

            string MaxProductionorder = "";

            cmd.Parameters.Add("@Production_mode", SqlDbType.VarChar);
            cmd.Parameters["@Production_mode"].Value = "QZ";


            cmd.CommandText = "LY_GetMax_Productiontask";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = sqlConnection1;

            sqlConnection1.Open();
            MaxProductionorder = cmd.ExecuteScalar().ToString();
            sqlConnection1.Close();



            return MaxProductionorder;
        }

       

        private void bindingNavigatorAddNewItem_Click(object sender, EventArgs e)
        {
            if (null == ly_material_plan_detailDataGridView.CurrentRow) return;

            if(null == ly_material_plan_mainDataGridView.CurrentRow) return;

            string message = "增加生产任务单吗？";
            string caption = "提示...";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;

            DialogResult result;

            //DataGridView dgv = sender as DataGridView;
            DataGridView dgv = ly_production_orderDataGridView;
            result = MessageBox.Show(message, caption, buttons,
            MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

            if (result == DialogResult.Yes)
            {
                string jhbh = ly_material_plan_mainDataGridView.CurrentRow.Cells["计划编号"].Value.ToString(); 

                using (SqlConnection con = new SqlConnection(SQLDatabase.Connectstring))
                {
                    string sql = "select count(*) from ly_store_out where bill_code='"+ jhbh + "'";
                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {

                        con.Open();
                        int k = Convert.ToInt32(cmd.ExecuteScalar());
                        if (k == 0)
                        {
                            MessageBox.Show("该计划还没有领料无法分配任务", "注意");
                            return;
                        }
                    }
                }



                int noworderValue = 0;

                int nowplanValue = int.Parse(this.ly_material_plan_detailDataGridView.CurrentRow.Cells["数量"].Value.ToString(), System.Globalization.NumberStyles.Number);


                foreach (DataGridViewRow dgr in dgv.Rows)
                {

                    if (string.IsNullOrEmpty(dgr.Cells["加工数量"].Value.ToString())) continue;
                    noworderValue = noworderValue + int.Parse(dgr.Cells["加工数量"].Value.ToString(), System.Globalization.NumberStyles.Number);



                }

                if (noworderValue >=nowplanValue)
                {
   
                    MessageBox.Show("任务安排不能超过计划数量,操作取消...", "注意");
                    return;

                }






                this.lY_productiontask_selBindingSource.AddNew();

                string nowtask = GetMaxProductionorder();
                this.ly_production_orderDataGridView.CurrentRow.Cells["任务单号"].Value = nowtask;

                this.ly_production_orderDataGridView.CurrentRow.Cells["下单日期"].Value = SQLDatabase.GetNowdate().ToString(); ;

                this.ly_production_orderDataGridView.CurrentRow.Cells["调度"].Value = SQLDatabase.nowUserName();

                int parentId = int.Parse(this.ly_material_plan_mainDataGridView.CurrentRow.Cells["parentid"].Value.ToString());
                this.ly_production_orderDataGridView.CurrentRow.Cells["plan_id_order"].Value = parentId;

                string nowitem = this.ly_material_plan_detailDataGridView.CurrentRow.Cells["编号d"].Value.ToString();
                this.ly_production_orderDataGridView.CurrentRow.Cells["物料编码"].Value = nowitem;

                string nowcount = this.ly_material_plan_detailDataGridView.CurrentRow.Cells["未排数量"].Value.ToString();
                this.ly_production_orderDataGridView.CurrentRow.Cells["加工数量"].Value = nowcount;

                //string financialunit_price = this.ly_material_plan_detailDataGridView.CurrentRow.Cells["financial_unit_price"].Value.ToString();
                //this.ly_production_orderDataGridView.CurrentRow.Cells["物料单价"].Value = financialunit_price;

                SaveChanged();




                //NewFrm.Show(this.ParentForm);
                this.lY_productiontask_selTableAdapter.Fill(this.lYProductMange.LY_productiontask_sel, parentId, nowitem);

                this.lY_productiontask_selBindingSource.Position = this.lY_productiontask_selBindingSource.Find("跟单编号", nowtask);

              

            }
        }

      
        private void bindingNavigatorDeleteItem_Click(object sender, EventArgs e)
        {
            if (null == this.ly_production_orderDataGridView.CurrentRow) return;
 

            string taskMumber = ly_production_orderDataGridView.CurrentRow.Cells["任务单号"].Value.ToString();



            using (SqlConnection con = new SqlConnection(SQLDatabase.Connectstring))
            {
                string sql = "select COUNT(1) from  ly_store_out where  pruductionTaskInspection_num='" + taskMumber + "'";
                using (SqlCommand cmd = new SqlCommand(sql, con))
                {

                    con.Open();
                    int k = Convert.ToInt32(cmd.ExecuteScalar());
                    if (k > 0)
                    {
                        MessageBox.Show("已有出入库记录不可删除", "注意");
                        return;
                    }
                }
            }

            using (SqlConnection con = new SqlConnection(SQLDatabase.Connectstring))
            {
                string sql = "select COUNT(1) from  ly_store_in where  pruductionTaskInspection_num='" + taskMumber + "'";
                using (SqlCommand cmd = new SqlCommand(sql, con))
                {

                    con.Open();
                    int k = Convert.ToInt32(cmd.ExecuteScalar());
                    if (k > 0)
                    {
                        MessageBox.Show("已有出入库记录不可删除", "注意");
                        return;
                    }
                }
            }




            if (decimal.Parse(this.ly_production_orderDataGridView.CurrentRow.Cells["入库数量"].Value.ToString()) > 0)
            {

                MessageBox.Show("改任务单已有入库，不可删除！", "注意");
                return;
            }

            string diaodu = this.ly_production_orderDataGridView.CurrentRow.Cells["调度"].Value.ToString();

            if (diaodu != SQLDatabase.nowUserName())
            {
                MessageBox.Show("请安排人:" + diaodu + "删除", "注意");
                return;
            }

            //if (ly_production_order_detailDataGridView.RowCount > 0)
            //{
            //    MessageBox.Show("跟单已有工序安排,删除所有工序安排后才能删除跟单...", "注意");
            //    return;

            //}

            string nowproductionorder = this.ly_production_orderDataGridView.CurrentRow.Cells["任务单号"].Value.ToString();


            string message1 = "当前(任务单：" + nowproductionorder + ")将被删除，继续吗？";
            string caption1 = "提示...";
            MessageBoxButtons buttons1 = MessageBoxButtons.YesNo;

            DialogResult result1;



            result1 = MessageBox.Show(message1, caption1, buttons1,
            MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

            if (result1 == DialogResult.Yes)
            {

                this.lY_productiontask_selBindingSource.RemoveCurrent();


                SaveChanged();




            }
        }

        private void SaveChanged()
        {

            this.ly_production_orderDataGridView.EndEdit();

            this.Validate();
            this.lY_productiontask_selBindingSource.EndEdit();

            this.lY_productiontask_selTableAdapter.Update(this.lYProductMange.LY_productiontask_sel);

           

            int parentId = int.Parse(this.ly_material_plan_mainDataGridView.CurrentRow.Cells["parentid"].Value.ToString());
            string nowitem = this.ly_material_plan_detailDataGridView.CurrentRow.Cells["编号d"].Value.ToString();


            int idd = int.Parse(this.ly_material_plan_detailDataGridView.CurrentRow.Cells["id_d"].Value.ToString());
            //this.ly_material_plan_detailDataGridView.RowEnter -= this.ly_material_plan_detailDataGridView_RowEnter;
            this.ly_material_plan_detailTableAdapter.Fill(this.lYProductMange.ly_material_plan_detail, parentId, "01");

            this.ly_material_plan_detailBindingSource.Position = this.ly_material_plan_detailBindingSource.Find("id", idd);
            //this.ly_material_plan_detailDataGridView.RowEnter += this.ly_material_plan_detailDataGridView_RowEnter;



            if (null != this.ly_production_orderDataGridView.CurrentRow)
            {

               


                string nowpruductionOrder_num = this.ly_production_orderDataGridView.CurrentRow.Cells["任务单号"].Value.ToString();



                this.ly_material_plan_explodeTaskTableAdapter.Fill(this.lYPlanMange.ly_material_plan_explodeTask, nowpruductionOrder_num);






            }
        }

        private void ly_production_orderDataGridView_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (null == this.ly_production_orderDataGridView.CurrentRow) return;
            
            DataGridView dgv = sender as DataGridView;


            string taskMumber = ly_production_orderDataGridView.CurrentRow.Cells["任务单号"].Value.ToString();



            using (SqlConnection con = new SqlConnection(SQLDatabase.Connectstring))
            {
                string sql = "select COUNT(1) from  ly_store_out where  pruductionTaskInspection_num='" + taskMumber + "'";
                using (SqlCommand cmd = new SqlCommand(sql, con))
                {

                    con.Open();
                    int k = Convert.ToInt32(cmd.ExecuteScalar());
                    if (k > 0)
                    {
                        MessageBox.Show("已有出入库记录不可删除", "注意");
                        return;
                    }
                }
            }

            using (SqlConnection con = new SqlConnection(SQLDatabase.Connectstring))
            {
                string sql = "select COUNT(1) from  ly_store_in where  pruductionTaskInspection_num='" + taskMumber + "'";
                using (SqlCommand cmd = new SqlCommand(sql, con))
                {

                    con.Open();
                    int k = Convert.ToInt32(cmd.ExecuteScalar());
                    if (k > 0)
                    {
                        MessageBox.Show("已有出入库记录不可删除", "注意");
                        return;
                    }
                }
            }



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

                    int nowplanValue = int.Parse(this.ly_material_plan_detailDataGridView.CurrentRow.Cells["数量"].Value.ToString(), System.Globalization.NumberStyles.Number);


                    foreach (DataGridViewRow dgr in dgv.Rows)
                    {

                        if (string.IsNullOrEmpty(dgr.Cells["加工数量"].Value.ToString())) continue;
                        noworderValue = noworderValue + int.Parse(dgr.Cells["加工数量"].Value.ToString(), System.Globalization.NumberStyles.Number);



                    }

                    if (noworderValue > nowplanValue )
                    {
                        dgv.CurrentRow.Cells["加工数量"].Value = queryForm.OldValue; 
                        MessageBox.Show("任务安排不能超过计划数量,操作取消...", "注意");
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

            if ("工号" == dgv.CurrentCell.OwningColumn.Name || "工人" == dgv.CurrentCell.OwningColumn.Name)
            {



                //if (!checkqualityRec() && "系统管理员" != SQLDatabase.nowUserName())
                //{

                //    MessageBox.Show("任务已有检验记录,不能修改, 操作取消", "注意");

                //    return;

                //}

                //string outflag = this.ly_production_order_detailDataGridView.CurrentRow.Cells["外协"].Value.ToString();
                string sel;

                //if ("True" == outflag)
                //{

                //    sel = "SELECT  supplier_code as 工号, supplier_name as 姓名 FROM ly_supplier_list where sort_code='4'";
                //}
                //else
                //{

                //    sel = "SELECT  work_code as 工号, worker_name as 姓名 FROM ly_worker_list";
                //}

                sel = "SELECT  work_code as 工号, worker_name as 姓名 FROM ly_worker_list where prodcode='01'";
                QueryForm queryForm = new QueryForm();


                queryForm.Sel = sel;
                queryForm.Constr = SQLDatabase.Connectstring;

                //Set the Column Collection to the filter Table
                //queryForm.SetSourceColumns(this.billMainDataSet.BalanceBill.Columns);

                queryForm.ShowDialog();


                if (queryForm.Result != "")
                {
                    dgv.CurrentRow.Cells["工号"].Value = queryForm.Result;
                    dgv.CurrentRow.Cells["工人"].Value = queryForm.Result1;
                    //dgv.CurrentRow.Cells["discount_money"].Value = DBNull.Value;

                    //dgv.CurrentRow.Cells["approve_flag"].Value = "False";
                    SaveChanged(); 


                    //CountPlanStru();

                }
                else
                {
                    dgv.CurrentRow.Cells["工号"].Value = DBNull.Value;
                    dgv.CurrentRow.Cells["工人"].Value = DBNull.Value;
                    //dgv.CurrentRow.Cells["discount_money"].Value = DBNull.Value;

                    //dgv.CurrentRow.Cells["approve_flag"].Value = "False";
                    SaveChanged(); 

                }
                return;

            }


            ///////////////////////////////////////////////////////


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

            if ("完成日期" == dgv.CurrentCell.OwningColumn.Name)
            {


                DatePicker queryForm = new DatePicker();
                queryForm.Pt = pt;

                if (null != (dgv.CurrentCell.Value))
                    queryForm.NowDate = dgv.CurrentCell.Value.ToString();
                else
                    queryForm.NowDate = SQLDatabase.GetNowdate().Date.ToString();

                queryForm.ShowDialog();



                if (null != queryForm.NowDate)
                {

                    dgv.CurrentRow.Cells["完成日期"].Value = queryForm.NowDate;
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
                this.splitContainer1.Panel1Collapsed = false ;
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
            

                if (null != this.ly_production_orderDataGridView.CurrentRow)
                {

                    //int parentId = int.Parse(this.ly_material_plan_mainDataGridView.CurrentRow.Cells["parentid"].Value.ToString());
                   
                    //string nowparentitem = this.ly_production_orderDataGridView.CurrentRow.Cells["物料编码"].Value.ToString();

                    //decimal  nowTaskcount;
                    //decimal  plancount;


                    //if (!string.IsNullOrEmpty(this.ly_production_orderDataGridView.CurrentRow.Cells["加工数量"].Value.ToString()))
                    //{
                    //    nowTaskcount =decimal.Parse ( this.ly_production_orderDataGridView.CurrentRow.Cells["加工数量"].Value.ToString());
                    //}
                    //else
                    //{
                    //    nowTaskcount = 0;
                    //}

                    //if (!string.IsNullOrEmpty(this.ly_material_plan_detailDataGridView.CurrentRow.Cells["数量"].Value.ToString()))
                    //{
                    //    plancount = decimal.Parse (this.ly_material_plan_detailDataGridView.CurrentRow.Cells["数量"].Value.ToString());
                    //}
                    //else
                    //{
                    //    plancount = 1;
                    //}


                    string nowpruductionOrder_num = this.ly_production_orderDataGridView.CurrentRow.Cells["任务单号"].Value.ToString();
                    string nowitemno = this.ly_production_orderDataGridView.CurrentRow.Cells["物料编码"].Value.ToString();



                    this.ly_production_task_inspectionTableAdapter.Fill(this.lYProductMange.ly_production_task_inspection, nowpruductionOrder_num);

                    this.ly_material_plan_explodeTaskTableAdapter.Fill(this.lYPlanMange.ly_material_plan_explodeTask, nowpruductionOrder_num);

                    this.lY_TaskExplode_sumTableAdapter.Fill(this.lYProductMange.LY_TaskExplode_sum, nowpruductionOrder_num);


                //2018-06-01 方新注释
                // this.ly_Bom_expendTableAdapter.Fill(this.lYMaterialMange.ly_Bom_expend, nowitemno);
                // this.ly_production_job_materialAddTableAdapter.Fill(this.lYProductMange.ly_production_job_materialAdd, nowpruductionOrder_num);






            }
            else
                {
                    this.ly_production_task_inspectionTableAdapter.Fill(this.lYProductMange.ly_production_task_inspection, "");

                    this.ly_material_plan_explodeTaskTableAdapter.Fill(this.lYPlanMange.ly_material_plan_explodeTask, "");
                    this.lY_TaskExplode_sumTableAdapter.Fill(this.lYProductMange.LY_TaskExplode_sum, "");



                //2018-06-01 方新注释
                //this.ly_Bom_expendTableAdapter.Fill(this.lYMaterialMange.ly_Bom_expend, "");
               // this.ly_production_job_materialAddTableAdapter.Fill(this.lYProductMange.ly_production_job_materialAdd, "");



            }

        }

       

       

       

        private void SaveTask()
        {

            //this.ly_production_order_detailDataGridView.EndEdit();

            //this.Validate();
            //this.ly_production_task_detail_selBindingSource.EndEdit();

            //this.ly_production_task_detail_selTableAdapter.Update(this.lYProductMange.ly_production_task_detail_sel);
            //////////////////////////////////////////////////

          
          
        }

        //private bool checkqualityRec()
        //{
        //      //decimal qualitied_count = 0;
        //      //decimal waste_count = 0;



        //      //if (!string.IsNullOrEmpty(this.ly_production_order_detailDataGridView.CurrentRow.Cells["合格1"].Value.ToString()))
        //      //{
        //      //    qualitied_count = decimal.Parse(this.ly_production_order_detailDataGridView.CurrentRow.Cells["合格1"].Value.ToString());
        //      //}
        //      //else
        //      //{
        //      //    qualitied_count = 0;
        //      //}

        //      //if (!string.IsNullOrEmpty(this.ly_production_order_detailDataGridView.CurrentRow.Cells["废品1"].Value.ToString()))
        //      //{
        //      //    waste_count = decimal.Parse(this.ly_production_order_detailDataGridView.CurrentRow.Cells["废品1"].Value.ToString());
        //      //}
        //      //else
        //      //{
        //      //    waste_count = 0;
        //      //}


        //      //if ((qualitied_count + waste_count) > 0)
        //      //{
        //      //    string nowordername = this.ly_machinepart_process_workDataGridView.CurrentRow.Cells["工序名称"].Value.ToString();

        //      //    if ("下料" == nowordername)
        //      //    {
        //      //        return false ;
        //      //    }
        //      //    else
        //      //    {
        //      //        return false;
        //      //    }
                 
        //      //}
        //      //else
        //      //{

        //      //    return true ;
        //      //}
        
        //}

        private void ly_production_order_detailDataGridView_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {

        }

       

      

        private void toolStripButton8_Click(object sender, EventArgs e)
        {
            //ExportDataGridviewTOExcell.ExportDataGridview(this.lY_MaterielRequirementsDataGridView, true);
        }

        

        private void LY_Machine_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.timer1.Stop();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.notifyIcon1.Visible = true ;
            this.notifyIcon1.ShowBalloonTip(2000, "提示", "有新加工任务,请查看...", ToolTipIcon.Info);
        }

        private void notifyIcon1_BalloonTipClosed(object sender, EventArgs e)
        {
            this.notifyIcon1.Visible = false;
        }

       
       

     

       

        private void toolStripButton12_Click(object sender, EventArgs e)
        {
            //LY_Production_order_Mange queryForm = new LY_Production_order_Mange();

          

            //queryForm.OwnerForm  = this ;
            ////queryForm.runmode = "增加";
            ////queryForm.statemode = "原料";

            //queryForm.StartPosition = FormStartPosition.CenterParent;
            //queryForm.ShowDialog( this);
           
            ////queryForm.ShowDialog();


            //if (queryForm.DialogResult != DialogResult.Cancel)
            //{
            //    //this.ly_inma0010ylTableAdapter.Fill(this.lYMaterialMange.ly_inma0010yl);
            //    //this.ly_inma0010ylBindingSource.Position = this.ly_inma0010ylBindingSource.Find("物资编号", queryForm.material_code);
            //}

            ///////////////////////////////////////////
        }

      ////////////////////////////////////////////////////

        public void find_NowProduc(int nowplanid, string nowitemno, string nowproduction_order)
        {
            this.ly_material_plan_mainBindingSource1.Position = this.ly_material_plan_mainBindingSource1.Find("id", nowplanid);



           
            this.ly_material_plan_detailTableAdapter.Fill(this.lYProductMange.ly_material_plan_detail, nowplanid, "01");

            this.ly_material_plan_detailBindingSource.Position = this.ly_material_plan_detailBindingSource.Find("编号d", nowitemno);

            this.lY_productiontask_selTableAdapter.Fill(this.lYProductMange.LY_productiontask_sel, nowplanid, nowitemno);

            this.lY_productiontask_selBindingSource.Position = this.lY_productiontask_selBindingSource.Find("跟单编号", nowproduction_order);


        
        }
       
       //////////////////////////////////////

        public void find_NowProduc(int nowplanid, string nowitemno, string nowproduction_order, int nowprocess_order)
        {
           
            find_NowProduc( nowplanid,  nowitemno,  nowproduction_order);

            //this.ly_machinepart_process_workBindingSource.Position = this.ly_machinepart_process_workBindingSource.Find("工序编号", nowprocess_order);



        }

        private void toolStripTextBox1_KeyUp(object sender, KeyEventArgs e)
        {
            string filterString = GetDataGridviewMultiFilterString.DGVMultiFilterString(this.ly_material_plan_detailDataGridView, this.toolStripTextBox1.Text);



            if (null == filterString)
                filterString = "";

            this.ly_material_plan_detailBindingSource.Filter = filterString;
        }

        private void toolStripTextBox1_Enter(object sender, EventArgs e)
        {
            toolStripTextBox1.Text = "";

            this.ly_material_plan_detailBindingSource.Filter = "";
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
                dgv.Columns["out_count"].Visible = true ;
                dgv.Columns["tec_qty"].Visible = true ;
            }

            if ("板料" == nowgeometry)
            {
                dgv.Columns["diameter"].Visible = false;
                dgv.Columns["width"].Visible = true;
                dgv.Columns["length"].Visible = true;
                dgv.Columns["height"].Visible = true;
                dgv.Columns["input_count"].Visible = true;
                dgv.Columns["specific_weight"].Visible = true;
                dgv.Columns["out_count"].Visible = true ;
                dgv.Columns["tec_qty"].Visible = true ;
            }
        }

        private void ly_production_order_materialrequisitionDataGridView_SelectionChanged(object sender, EventArgs e)
        {
            SetDisplayColumn(sender as DataGridView);
        }

        private void ly_material_plan_detailDataGridView_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (this.formState == "View")
            {

                if (null != this.ly_material_plan_detailDataGridView.CurrentRow)
                {
                    int parentId = int.Parse(this.ly_material_plan_mainDataGridView.CurrentRow.Cells["parentid"].Value.ToString());
                    string nowitem = this.ly_material_plan_detailDataGridView.Rows[e.RowIndex].Cells["编号d"].Value.ToString();

                    //NewFrm.Show(this.ParentForm);
                    this.lY_productiontask_selTableAdapter.Fill(this.lYProductMange.LY_productiontask_sel, parentId, nowitem);
                    //this.ly_production_orderTableAdapter.Fill(this.lYMaterielRequirements.ly_production_order, parentId, nowitem);
                    ////this.ly_machinepart_processTableAdapter.Fill(this.lYMaterielRequirements.ly_machinepart_process, nowitem);
                    //this.order_instore_detailTableAdapter.Fill(this.lYMaterielRequirements.order_instore_detail, parentId, nowitem);

                    //NewFrm.Hide(this.ParentForm);
                }
            }
        }

        private void 打印PToolStripButton_Click(object sender, EventArgs e)
        {
            if (null == this.ly_production_orderDataGridView.CurrentRow) return;



            //string balanceFlag = this.xD_Sel_SellBalanceDataGridView.CurrentRow.Cells["结算"].Value.ToString();



            NewFrm.Show(this);


            BaseReportView queryForm = new BaseReportView();

            queryForm.Text = "钳装生产任务领料单";

            queryForm.Printdata = this.lYProductMange;

            queryForm.PrintCrystalReport = new LY_ProductTaskReport();
 

            NewFrm.Hide(this);

            queryForm.ShowDialog();
        }


        private void ly_material_plan_detailDataGridView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            DataGridView dgv = sender as DataGridView;
            if (e.RowIndex > -1)
            {
                if ((dgv.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.GetType() == typeof(decimal)
                      || dgv.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.GetType() == typeof(double)
                      || dgv.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.GetType() == typeof(int)))
                {
                    if (Convert.ToDecimal(dgv.Rows[e.RowIndex].Cells[e.ColumnIndex].Value) == 0)
                    {
                        e.Value = System.DBNull.Value;
                    }
                }
            }       
        }

        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            int parentId = int.Parse(this.ly_material_plan_mainDataGridView.CurrentRow.Cells["parentid"].Value.ToString());
            string planNum = this.ly_material_plan_mainDataGridView.CurrentRow.Cells["计划编号"].Value.ToString();


            this.lY_productiontask_allplanTableAdapter.Fill(this.lYProductMange.LY_productiontask_allplan, parentId, "01");
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            if (null == this.dataGridView1.CurrentRow) return;



            //string balanceFlag = this.xD_Sel_SellBalanceDataGridView.CurrentRow.Cells["结算"].Value.ToString();



            NewFrm.Show(this);


            BaseReportView queryForm = new BaseReportView();

            queryForm.Text = "钳装生产任务单";

            queryForm.Printdata = this.lYProductMange;

            queryForm.PrintCrystalReport = new LY_ProductTaskAllReport();

            //string nowCardNumber = this.xD_Sel_SellBalanceDataGridView.CurrentRow.Cells["编号"].Value.ToString();

            //string selectFormula;
            //selectFormula = "{XD_Sel_SellBalance.编号}   =   '" + nowCardNumber + "'";
            //queryForm.CrystalReportViewer1.SelectionFormula = selectFormula;

            //if (this.radioButton2.Checked)
            //{
            //    string selectFormula;
            //    selectFormula = "{XD_Sel_SellBalance.Card_number}   =   '" + nowCardNumber + "'";
            //    queryForm.PrintCrystalReport.DataDefinition.RecordSelectionFormula = selectFormula;
            //}




            //queryForm.PrintCrystalReport = new XD_SellBalance_All();

            //string nowCardNumber = this.xD_Sel_SellBalanceDataGridView.CurrentRow.Cells["编号"].Value.ToString();

            NewFrm.Hide(this);

            queryForm.ShowDialog();
        }

        private void toolStripButton9_Click(object sender, EventArgs e)
        {
 

            this.ly_material_plan_mainBindingSource1.Filter = " unfinished_count>0 and oper_dept='01'  ";

            this.toolStripButton1.Visible = true;
            this.toolStripButton9.Visible = false;

            SetFormState("View");
        }

        private void ly_material_plan_detailDataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (ly_material_plan_detailDataGridView.CurrentRow == null)
            {
                return;
            }
            try
            {
                string plan_count = ly_material_plan_detailDataGridView.CurrentRow.Cells["数量"].Value.ToString();
                string done_count = ly_material_plan_detailDataGridView.CurrentRow.Cells["完成数量"].Value.ToString();
                if (!SQLDatabase.CheckHaveRight(SQLDatabase.NowUserID, "装配工单关闭"))
                {
                    MessageBox.Show("无装配工单关闭权限！"); return;
                }

                DataGridView dgv = sender as DataGridView;
                if ("close_count" == dgv.CurrentCell.OwningColumn.Name)
                {
                    ChangeValue queryForm = new ChangeValue();
                    queryForm.OldValue = dgv.CurrentCell.Value.ToString();
                    queryForm.NewValue = "";
                    queryForm.ChangeMode = "value";
                    queryForm.ShowDialog();
                    if (queryForm.NewValue != "")
                    {
                        if (queryForm.NewValue.Trim() == "0")
                        {


                            decimal count = 0;
                            if (string.IsNullOrEmpty(dgv.CurrentCell.Value.ToString()))
                            {
                                count = 0;
                            }
                            else
                            {
                                count = decimal.Parse(dgv.CurrentCell.Value.ToString());
                            }

                            dgv.CurrentRow.Cells["close_count"].Value = 0;
                            dgv.CurrentRow.Cells["close_peo"].Value = SQLDatabase.nowUserName();
                            dgv.CurrentRow.Cells["数量"].Value = decimal.Parse(plan_count) + count;
                            dgv.CurrentRow.Cells["close_time"].Value = DateTime.Now;
                        }
                        else
                        {
                            decimal count = decimal.Parse(queryForm.NewValue);

                            if ((decimal.Parse(plan_count) - decimal.Parse(done_count)) < count)
                            {
                                MessageBox.Show("关闭数量超过未完成数量！"); return;
                            }
                            dgv.CurrentRow.Cells["close_count"].Value = count;
                            dgv.CurrentRow.Cells["close_peo"].Value = SQLDatabase.nowUserName();
                            dgv.CurrentRow.Cells["数量"].Value = decimal.Parse(plan_count) - count;
                            dgv.CurrentRow.Cells["close_time"].Value = DateTime.Now;

                        }
                    }



                    this.ly_material_plan_detailDataGridView.EndEdit();
                    this.ly_material_plan_detailBindingSource.EndEdit();
                    this.ly_material_plan_detailTableAdapter.Update(this.lYProductMange.ly_material_plan_detail);
                    if (null != this.ly_material_plan_mainDataGridView.CurrentRow)
                    {
                        int parentId = int.Parse(this.ly_material_plan_mainDataGridView.CurrentRow.Cells["parentid"].Value.ToString());

                        this.ly_material_plan_detailTableAdapter.Fill(this.lYProductMange.ly_material_plan_detail, parentId, "01");

                    }
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString()); return;
            }

        }
    }
}
