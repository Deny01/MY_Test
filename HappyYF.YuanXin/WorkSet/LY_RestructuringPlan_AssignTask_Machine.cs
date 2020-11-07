using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using DataGridFilter;
using HappyYF.Infrastructure.Repositories;

namespace HappyYF.YuanXin.WorkSet
{
    public partial class LY_RestructuringPlan_AssignTask_Machine : Form
    {

        string formState = "View";
        Point pt = new Point();

        public LY_RestructuringPlan_AssignTask_Machine()
        {
            InitializeComponent();
        }

        private void LY_Machine_Load(object sender, EventArgs e)
        {
           

            toolStripButton4.Text = "显示计划界面";
            this.splitContainer1.Panel1Collapsed = false;
 

            this.lY_restructuring_task_selTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            this.ly_restructuring_plan_detailTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;

            this.ly_restructuring_plan_mainTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;

            this.ly_Restructuring_requestTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            this.ly_Restructuring_returnTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            this.ly_restructuring_task_inspectionTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
           



            this.ly_restructuring_plan_mainTableAdapter.Fill(this.lYProductMange.ly_restructuring_plan_main , "GZJH");

 

        }





        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            if (ly_material_plan_mainDataGridView.CurrentRow == null) return;
            string planNum = this.ly_material_plan_mainDataGridView.CurrentRow.Cells["id"].Value.ToString();


            this.ly_restructuring_plan_detailTableAdapter.Fill(this.lYProductMange.ly_restructuring_plan_detail, int.Parse(planNum), "01");
        }

        private string GetMaxProductionorder()
        {
            SqlConnection sqlConnection1 = new SqlConnection(SQLDatabase.Connectstring);
            SqlCommand cmd = new SqlCommand();

            string MaxProductionorder = "";

            cmd.Parameters.Add("@Production_mode", SqlDbType.VarChar);
            cmd.Parameters["@Production_mode"].Value = "GQ";


            cmd.CommandText = "LY_GetMax_Restructuringtask";
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
            if (null == ly_material_plan_mainDataGridView.CurrentRow) return;

            if ("True" != ly_material_plan_mainDataGridView.CurrentRow.Cells["plan_approve_two"].Value.ToString())
            {
                MessageBox.Show("请联系库管员进行发料审批,操作取消...", "注意");
                return;
            }
            if ("True" != ly_material_plan_mainDataGridView.CurrentRow.Cells["批准2"].Value.ToString())
            {
                MessageBox.Show("没有审批,操作取消...", "注意");
                return;
            }



            string message = "增加改制生产任务单吗？";
            string caption = "提示...";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;

            DialogResult result;



            result = MessageBox.Show(message, caption, buttons,
            MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

            if (result == DialogResult.Yes)
            { 

                int noworderValue = 0;

                int nowplanValue = int.Parse(this.ly_material_plan_detailDataGridView.CurrentRow.Cells["改制数量"].Value.ToString(), System.Globalization.NumberStyles.Number);


                foreach (DataGridViewRow dgr in ly_production_orderDataGridView.Rows)
                {

                    if (string.IsNullOrEmpty(dgr.Cells["加工数量"].Value.ToString())) continue;
                    noworderValue = noworderValue + int.Parse(dgr.Cells["加工数量"].Value.ToString(), System.Globalization.NumberStyles.Number); 
                }

                if (noworderValue >= nowplanValue)
                {
   
                    MessageBox.Show("任务安排不能超过计划数量,操作取消...", "注意");
                    return;

                }
                 

                string nowcount = this.ly_material_plan_detailDataGridView.CurrentRow.Cells["未排数量_new"].Value.ToString();

                if (0 >= decimal.Parse(nowcount))
                {
                    MessageBox.Show("未排数量为0！", "注意");
                    return;

                }

                this.lY_restructuring_task_selBindingSource.AddNew();

                string nowtask = GetMaxProductionorder();//任务单号
                this.ly_production_orderDataGridView.CurrentRow.Cells["任务单号"].Value = nowtask;

                this.ly_production_orderDataGridView.CurrentRow.Cells["下单日期"].Value = SQLDatabase.GetNowdate().ToString(); ;

                this.ly_production_orderDataGridView.CurrentRow.Cells["调度"].Value = SQLDatabase.nowUserName();

                int parentId = int.Parse(this.ly_material_plan_detailDataGridView.CurrentRow.Cells["改制主计划号"].Value.ToString());
                this.ly_production_orderDataGridView.CurrentRow.Cells["plan_id_order"].Value = parentId;


                int restructuring_id = int.Parse(this.ly_material_plan_detailDataGridView.CurrentRow.Cells["改制子计划号"].Value.ToString());
                this.ly_production_orderDataGridView.CurrentRow.Cells["改制编号"].Value = restructuring_id;

                string nowitem = this.ly_material_plan_detailDataGridView.CurrentRow.Cells["改后编码"].Value.ToString();
                this.ly_production_orderDataGridView.CurrentRow.Cells["物料编码"].Value = nowitem;

               
                this.ly_production_orderDataGridView.CurrentRow.Cells["加工数量"].Value = nowcount;
 
                SaveChanged(restructuring_id);
                
                this.lY_restructuring_task_selTableAdapter.Fill(this.lYProductMange.LY_restructuring_task_sel, restructuring_id, nowitem);

                this.lY_restructuring_task_selBindingSource.Position = this.lY_restructuring_task_selBindingSource.Find("跟单编号", nowtask);
 

            }
        }


        private void bindingNavigatorDeleteItem_Click(object sender, EventArgs e)
        {
            if (null == this.ly_production_orderDataGridView.CurrentRow) return;


            //---判断入库
            //---判断质检


            string nowproductionorder = this.ly_production_orderDataGridView.CurrentRow.Cells["任务单号"].Value.ToString();
            int taskParent = int.Parse(ly_material_plan_detailDataGridView.CurrentRow.Cells["改制子计划号"].Value.ToString()); // 改制计划下的某一台  =  parent_id  =  restructuring_id



         
            using (SqlConnection con = new SqlConnection(SQLDatabase.Connectstring))
            {
                string sql = "select COUNT(1) from  ly_store_out where  pruductionTaskInspection_num='" + nowproductionorder + "'";
                using (SqlCommand cmd = new SqlCommand(sql, con))
                {

                    con.Open();
                    int k=Convert.ToInt32(cmd.ExecuteScalar());
                    if (k > 0)
                    {
                        MessageBox.Show("已有出入库记录不可删除", "注意");
                        return;
                    }
                }
            }

            using (SqlConnection con = new SqlConnection(SQLDatabase.Connectstring))
            {
                string sql = "select COUNT(1) from  ly_store_in where  pruductionTaskInspection_num='" + nowproductionorder + "'";
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





            if (ly_restructuring_task_inspectionDataGridView.Rows.Count > 0)
            {
 
                foreach (DataGridViewRow dgr in ly_restructuring_task_inspectionDataGridView.Rows)
                {

                    if ("True" == dgr.Cells["合格"].Value.ToString())
                    {
                        MessageBox.Show("已经质检合格，不能删除(实需删除，请先清除质检合格标记)", "注意");
                        return;

                    }
                    if ("True" == dgr.Cells["出库"].Value.ToString())
                    {
                        MessageBox.Show("已经领料，不能删除(实需删除，请先清除领料标记)", "注意");
                        return;

                    }

                    if ("True" == dgr.Cells["入库"].Value.ToString())
                    {
                        MessageBox.Show("已经入库，不能删除(实需删除，请先删除该质检单号的入库记录)", "注意");
                        return;

                    }

                }
            }

                string diaodu = this.ly_production_orderDataGridView.CurrentRow.Cells["调度"].Value.ToString();

            if (diaodu != SQLDatabase.nowUserName())
            {
                MessageBox.Show("请安排人:" + diaodu + "删除", "注意");
                return;
            }


            string message1 = "当前(任务单：" + nowproductionorder + ")将被删除，继续吗？";
            string caption1 = "提示...";
            MessageBoxButtons buttons1 = MessageBoxButtons.YesNo;

            DialogResult result1;

            result1 = MessageBox.Show(message1, caption1, buttons1,
            MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

            if (result1 == DialogResult.Yes)
            {

                this.lY_restructuring_task_selBindingSource.RemoveCurrent();
                //删除领料
                using (SqlConnection con = new SqlConnection(SQLDatabase.Connectstring))
                {
                    string sql = "delete from ly_Restructuring_request where pruductionOrder_num='" + nowproductionorder + "' and parent_id=" + taskParent;
                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {

                        con.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
                //删除退料
                using (SqlConnection con = new SqlConnection(SQLDatabase.Connectstring))
                {
                    string sql = "delete from ly_Restructuring_return where pruductionOrder_num='" + nowproductionorder + "' and parent_id=" + taskParent;
                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {

                        con.Open();
                        cmd.ExecuteNonQuery();
                    }
                }

                //删除质检
                using (SqlConnection con = new SqlConnection(SQLDatabase.Connectstring))
                {
                    string sql = "delete from ly_restructuring_task_inspection where  pruductionOrder_num='"+ nowproductionorder + "'";
                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {

                        con.Open();
                        cmd.ExecuteNonQuery();
                    }
                }



                SaveChanged(taskParent); 
    
            }
        }


        private void SaveChanged(int gzbh)
        {

            this.ly_production_orderDataGridView.EndEdit();

            this.Validate();
            this.lY_restructuring_task_selBindingSource.EndEdit();
            this.lY_restructuring_task_selTableAdapter.Update(this.lYProductMange.LY_restructuring_task_sel);
            //-----------------------------------

            if (null != this.ly_material_plan_mainDataGridView.CurrentRow)
            {
                int parentId = int.Parse(this.ly_material_plan_mainDataGridView.CurrentRow.Cells["id"].Value.ToString());//计划编号 
                this.ly_restructuring_plan_detailTableAdapter.Fill(this.lYProductMange.ly_restructuring_plan_detail, parentId, "01");


                this.ly_restructuring_plan_detailBindingSource.Position = this.ly_restructuring_plan_detailBindingSource.Find("id", gzbh);
            }
            if (null != this.ly_material_plan_detailDataGridView.CurrentRow)
            {
                int parentId = int.Parse(this.ly_material_plan_detailDataGridView.CurrentRow.Cells["改制子计划号"].Value.ToString());
                string nowitem = this.ly_material_plan_detailDataGridView.CurrentRow.Cells["改后编码"].Value.ToString();
                this.lY_restructuring_task_selTableAdapter.Fill(this.lYProductMange.LY_restructuring_task_sel, parentId, nowitem);
            }
            if (null != this.ly_production_orderDataGridView.CurrentRow)
            {
                string taskMumber = ly_production_orderDataGridView.CurrentRow.Cells["任务单号"].Value.ToString();

                int taskParent = int.Parse(ly_production_orderDataGridView.CurrentRow.Cells["改制编号"].Value.ToString()); // 改制计划下的某一台  =  parent_id  =  restructuring_id

                this.ly_Restructuring_requestTableAdapter.Fill(this.lYProductMange.ly_Restructuring_request, taskParent, taskMumber);
                this.ly_Restructuring_returnTableAdapter.Fill(this.lYProductMange.ly_Restructuring_return, taskParent, taskMumber);
                this.ly_restructuring_task_inspectionTableAdapter.Fill(this.lYProductMange.ly_restructuring_task_inspection, taskMumber);

            }
            else
            {
                this.ly_restructuring_task_inspectionTableAdapter.Fill(this.lYProductMange.ly_restructuring_task_inspection, "123");
                this.ly_Restructuring_requestTableAdapter.Fill(this.lYProductMange.ly_Restructuring_request, -1, "123");
                this.ly_Restructuring_returnTableAdapter.Fill(this.lYProductMange.ly_Restructuring_return, -1, "123");
                this.ly_restructuring_task_inspectionTableAdapter.Fill(this.lYProductMange.ly_restructuring_task_inspection, "");

            }
        }
 

        protected void InsertRequset(DataGridView dgv)
        {
            //抄写改制标准领料与标准退料

            string taskMumber = dgv.CurrentRow.Cells["任务单号"].Value.ToString();
            string taskNumber = dgv.CurrentRow.Cells["加工数量"].Value.ToString();
            string taskParent = ly_material_plan_detailDataGridView.CurrentRow.Cells["改制子计划号"].Value.ToString(); // 改制计划号
 
            string wzbh_old= ly_material_plan_detailDataGridView.CurrentRow.Cells["改前编码"].Value.ToString(); //  



            SqlConnection myConn = new SqlConnection(SQLDatabase.Connectstring);
            myConn.Open();
            SqlCommand myComm = new SqlCommand();
            SqlTransaction myTran;
            myTran = myConn.BeginTransaction();
            try
            {
                myComm.Connection = myConn;
                myComm.Transaction = myTran;
                myComm.CommandText = "delete from ly_Restructuring_request where pruductionOrder_num='" + taskMumber + "' and parent_id=" + taskParent;
                myComm.ExecuteNonQuery();

                string sqlIn = @"insert into ly_Restructuring_request (parent_id,pruductionOrder_num,itemno,request_qty ,sys_date,main_material_flag) 
                            ( select restructuring_id,'" + taskMumber + "',itemno,request_qty*" + taskNumber + ",getdate(),isnull(main_material_flag,0) from  ly_Restructuring_request_standard" +
                    " where restructuring_id=" + taskParent + ")";

                myComm.CommandText = sqlIn;
                myComm.ExecuteNonQuery();



                //string sqlInOld = @"insert into ly_Restructuring_request (parent_id,pruductionOrder_num,itemno,request_qty ,sys_date) values
                //            ( "+ taskParent + ",'" + taskMumber + "','"+ wzbh_old + "', " + taskNumber + ",getdate())";

                //myComm.CommandText = sqlInOld;
                //myComm.ExecuteNonQuery();

                myComm.CommandText = "delete from ly_restructuring_task_inspection where  pruductionOrder_num='"+ taskMumber + "'";
                myComm.ExecuteNonQuery();

                int k = (int)(decimal.Parse(taskNumber));
                for (int i = 0; i < k; i++)
                {

                    string sqlInspection = @"insert into ly_restructuring_task_inspection(pruductionOrder_num,subpeople,subdate,restructuring_id,out_flag,newold,Assembly_Time,Assembly_price)values('"
                        + taskMumber + "','"+ SQLDatabase.nowUserName()+ "',getdate(),"+ taskParent + ",0,'旧',(select ISNULL(Assembly_Time,null) from dbo.ly_Restructuring_Bom_main  where dis_itemno=(select wzbh from ly_material_plan_detail where id="+ taskParent 
                        + ") and origin_itemno = (select origin_itemno from ly_material_plan_detail where id = "+taskParent+ ")),  (select ISNULL(Assembly_price,0) from dbo.ly_Restructuring_Bom_main  where dis_itemno=(select wzbh from ly_material_plan_detail where id=" + taskParent
                        + ") and origin_itemno = (select origin_itemno from ly_material_plan_detail where id = " + taskParent + ")) )";

                    string s = sqlInspection;
                    myComm.CommandText = sqlInspection;
                    myComm.ExecuteNonQuery();
                }

                myComm.CommandText = "delete from ly_Restructuring_return where pruductionOrder_num='" + taskMumber + "' and parent_id=" + taskParent;
                myComm.ExecuteNonQuery();

                string sqlInRe = @"insert into ly_Restructuring_return (parent_id,pruductionOrder_num,itemno,return_qty, sys_date) 
                            ( select restructuring_id,'" + taskMumber + "',itemno,return_qty*" + taskNumber + " ,getdate() from  ly_Restructuring_return_standard" +
                    " where restructuring_id='" + taskParent + "')";

                myComm.CommandText = sqlInRe;
                myComm.ExecuteNonQuery();

                myTran.Commit();
            }
            catch (SqlException err)
            {
                MessageBox.Show(err.Message);
                myTran.Rollback();
            }
            finally
            {
                myConn.Close();
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

             

            if (ly_restructuring_task_inspectionDataGridView.Rows.Count > 0)
            {

                foreach (DataGridViewRow dgr in ly_restructuring_task_inspectionDataGridView.Rows)
                {

                    if ("True" == dgr.Cells["合格"].Value.ToString())
                    {
                        MessageBox.Show("已经质检合格，不能删除(实需删除，请先清除质检合格标记)", "注意");
                        return;

                    }
                    if ("True" == dgr.Cells["出库"].Value.ToString())
                    {
                        MessageBox.Show("已经领料，不能删除(实需删除，请先清除领料标记)", "注意");
                        return;

                    }

                    if ("True" == dgr.Cells["入库"].Value.ToString())
                    {
                        MessageBox.Show("已经入库，不能删除(实需删除，请先删除该质检单号的入库记录)", "注意");
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

                    int nowplanValue = int.Parse(this.ly_material_plan_detailDataGridView.CurrentRow.Cells["改制数量"].Value.ToString(), System.Globalization.NumberStyles.Number);


                    foreach (DataGridViewRow dgr in dgv.Rows)
                    {

                        if (string.IsNullOrEmpty(dgr.Cells["加工数量"].Value.ToString())) continue;
                        noworderValue = noworderValue + int.Parse(dgr.Cells["加工数量"].Value.ToString(), System.Globalization.NumberStyles.Number);

                    }

                    if (noworderValue > nowplanValue)
                    {
                        dgv.CurrentRow.Cells["加工数量"].Value = queryForm.OldValue;
                        MessageBox.Show("任务安排不能超过计划数量,操作取消...", "注意");
                        return;

                    }
                    dgv.CurrentRow.Cells["加工数量"].Value = queryForm.NewValue;
                    dgv.CurrentRow.Cells["未入库数"].Value = queryForm.NewValue;
                    InsertRequset(dgv);
                    SaveChanged(int.Parse(dgv.CurrentRow.Cells["改制编号"].Value.ToString()));

                }
                else
                { 
                }
                return;

            }

            if ("工号" == dgv.CurrentCell.OwningColumn.Name || "工人" == dgv.CurrentCell.OwningColumn.Name)
            {


                string sel;


                sel = "SELECT  work_code as 工号, worker_name as 姓名 FROM ly_worker_list where prodcode='01'";
                QueryForm queryForm = new QueryForm();


                queryForm.Sel = sel;
                queryForm.Constr = SQLDatabase.Connectstring;

                queryForm.ShowDialog();


                if (queryForm.Result != "")
                {
                    dgv.CurrentRow.Cells["工号"].Value = queryForm.Result;
                    dgv.CurrentRow.Cells["工人"].Value = queryForm.Result1;

                    InsertRequset(dgv);

                    SaveChanged(int.Parse(dgv.CurrentRow.Cells["改制编号"].Value.ToString()));

                }
                else
                {
                    dgv.CurrentRow.Cells["工号"].Value = DBNull.Value;
                    dgv.CurrentRow.Cells["工人"].Value = DBNull.Value;

                    SaveChanged(int.Parse(dgv.CurrentRow.Cells["改制编号"].Value.ToString()));
                }
                return;

            }


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
                    SaveChanged(int.Parse(dgv.CurrentRow.Cells["改制编号"].Value.ToString()));
                }
                return;
            }


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
                    SaveChanged(int.Parse(dgv.CurrentRow.Cells["改制编号"].Value.ToString()));
                }
                return;
            }



            if ("任务单备注" == dgv.CurrentCell.OwningColumn.Name)
            {

                ChangeValue queryForm = new ChangeValue();

                queryForm.OldValue = dgv.CurrentCell.Value.ToString();
                queryForm.NewValue = "";

                queryForm.ShowDialog(); 
                if (queryForm.NewValue != "")
                {
                    dgv.CurrentRow.Cells["任务单备注"].Value = queryForm.NewValue;
                    SaveChanged(int.Parse(dgv.CurrentRow.Cells["改制编号"].Value.ToString()));
                }
                else
                {

                }
                return;

            }


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

                SaveChanged(int.Parse(dgv.CurrentRow.Cells["改制编号"].Value.ToString()));
                return;
            }
          
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

 

        private void toolStripButton8_Click(object sender, EventArgs e)
        {
            //ExportDataGridviewTOExcell.ExportDataGridview(this.lY_MaterielRequirementsDataGridView, true);
        }

        
 
       
        
    
        private void toolStripTextBox1_KeyUp(object sender, KeyEventArgs e)
        {
            string filterString = GetDataGridviewMultiFilterString.DGVMultiFilterString(this.ly_material_plan_detailDataGridView, this.toolStripTextBox1.Text);



            if (null == filterString)
                filterString = "";

            this.ly_restructuring_plan_detailBindingSource.Filter = filterString;
        }

        private void toolStripTextBox1_Enter(object sender, EventArgs e)
        {
            toolStripTextBox1.Text = "";

            this.ly_restructuring_plan_detailBindingSource.Filter = "";
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

  
 
       

        private void toolStripButton15_Click(object sender, EventArgs e)
        {

            if (null == this.ly_production_orderDataGridView.CurrentRow) return;



            BaseReportView queryForm = new BaseReportView();

            queryForm.Text = "钳装改制生产任务领料单";

            queryForm.Printdata = this.lYProductMange;

            queryForm.PrintCrystalReport = new LY_ProductTaskReport();

            queryForm.ShowDialog();
        }

        private void toolStripButton1_Click_1(object sender, EventArgs e)
        {
            this.ly_restructuring_plan_mainTableAdapter.Fill(this.lYProductMange.ly_restructuring_plan_main, "GZJH");
        }
 
        private void ly_production_orderDataGridView_SelectionChanged(object sender, EventArgs e)
        {

            if (null != this.ly_production_orderDataGridView.CurrentRow)
            {
                if (ly_production_orderDataGridView.CurrentRow.Cells["任务单号"].Value == null)
                {
                    this.ly_Restructuring_requestTableAdapter.Fill(this.lYProductMange.ly_Restructuring_request, -1, "123");
                    this.ly_Restructuring_returnTableAdapter.Fill(this.lYProductMange.ly_Restructuring_return, -1, "123");


                    this.ly_restructuring_task_inspectionTableAdapter.Fill(this.lYProductMange.ly_restructuring_task_inspection, "123");
                }
                else
                {

                    string taskMumber = ly_production_orderDataGridView.CurrentRow.Cells["任务单号"].Value.ToString();

                    string taskParentStr = ly_production_orderDataGridView.CurrentRow.Cells["改制编号"].Value.ToString();

                    if (taskMumber == "" || taskParentStr == "")
                    { }
                    else
                    {
                        int taskParent = int.Parse(taskParentStr); // 改制计划下的某一台  =  parent_id  =  restructuring_id

                        this.ly_Restructuring_requestTableAdapter.Fill(this.lYProductMange.ly_Restructuring_request, taskParent, taskMumber);
                        this.ly_Restructuring_returnTableAdapter.Fill(this.lYProductMange.ly_Restructuring_return, taskParent, taskMumber);
                        this.ly_restructuring_task_inspectionTableAdapter.Fill(this.lYProductMange.ly_restructuring_task_inspection, taskMumber);
                    }
                }
            }
        }

        private void ly_material_plan_detailDataGridView_SelectionChanged(object sender, EventArgs e)
        {
            if (null != this.ly_material_plan_detailDataGridView.CurrentRow)
            {
                int parentId = int.Parse(this.ly_material_plan_detailDataGridView.CurrentRow.Cells["改制子计划号"].Value.ToString());
                string nowitem = this.ly_material_plan_detailDataGridView.CurrentRow.Cells["改后编码"].Value.ToString();
                this.lY_restructuring_task_selTableAdapter.Fill(this.lYProductMange.LY_restructuring_task_sel, parentId, nowitem);
            }
        }

        private void ly_material_plan_mainDataGridView_SelectionChanged(object sender, EventArgs e)
        {
            if (this.formState == "View")
            {

                if (null != this.ly_material_plan_mainDataGridView.CurrentRow)
                {
                    int parentId = int.Parse(this.ly_material_plan_mainDataGridView.CurrentRow.Cells["id"].Value.ToString());//计划编号

                    计划编号TextBox.Text = this.ly_material_plan_mainDataGridView.CurrentRow.Cells["计划编号"].Value.ToString();
                    说明TextBox.Text = this.ly_material_plan_mainDataGridView.CurrentRow.Cells["说明DataGridViewTextBoxColumn"].Value.ToString();
                    制定日期DateTimePicker.Text = this.ly_material_plan_mainDataGridView.CurrentRow.Cells["制定日期DataGridViewTextBoxColumn"].Value.ToString();
                    制定人TextBox.Text = this.ly_material_plan_mainDataGridView.CurrentRow.Cells["制定人DataGridViewTextBoxColumn"].Value.ToString();

                    this.ly_restructuring_plan_detailTableAdapter.Fill(this.lYProductMange.ly_restructuring_plan_detail, parentId, "01");

                    this.lY_restructuring_task_selTableAdapter.Fill(this.lYProductMange.LY_restructuring_task_sel, -1, "123");

                    this.ly_Restructuring_requestTableAdapter.Fill(this.lYProductMange.ly_Restructuring_request, -1, "123");
                    this.ly_Restructuring_returnTableAdapter.Fill(this.lYProductMange.ly_Restructuring_return, -1, "123");
                    this.ly_restructuring_task_inspectionTableAdapter.Fill(this.lYProductMange.ly_restructuring_task_inspection, "123");
                }

                else

                {
                   

                    计划编号TextBox.Text ="";
                    说明TextBox.Text = "";
                    制定日期DateTimePicker.Text = "";
                    制定人TextBox.Text = "";

                    this.ly_restructuring_plan_detailTableAdapter.Fill(this.lYProductMange.ly_restructuring_plan_detail, -1, "01");

                    this.lY_restructuring_task_selTableAdapter.Fill(this.lYProductMange.LY_restructuring_task_sel, -1, "123");

                    this.ly_Restructuring_requestTableAdapter.Fill(this.lYProductMange.ly_Restructuring_request, -1, "123");
                    this.ly_Restructuring_returnTableAdapter.Fill(this.lYProductMange.ly_Restructuring_return, -1, "123");
                    this.ly_restructuring_task_inspectionTableAdapter.Fill(this.lYProductMange.ly_restructuring_task_inspection, "123");

                }
            }
        }
    }
}
