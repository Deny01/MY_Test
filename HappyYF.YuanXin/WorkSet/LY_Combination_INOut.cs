using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Transactions;
using HappyYF.Infrastructure.Repositories;
using System.Threading;
using DataGridFilter;

namespace HappyYF.YuanXin.WorkSet
{
    public partial class LY_Combination_INOut : Form
    {
         string formState = "View";

         public LY_Combination_INOut()
        {
            InitializeComponent();
        }

       

        private void LY_Material_Plan_Load(object sender, EventArgs e)
        {

           
            this.ly_plan_getmaterialCombinationOutTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            this.ly_inma0010_planselAllTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            this.ly_material_plan_detailTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            this.ly_material_plan_mainTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;

            this.ly_store_out_combinationTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            this.ly_store_out_combination_detailTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;

            this.ly_store_innum_combinationTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;

            this.ly_store_innum_combination_detailTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;

            this.ly_material_plan_mainTableAdapter.Fill(this.lYPlanMange.ly_material_plan_main,"GXJH");
            //if (null == this.ly_store_outnumDataGridView.CurrentRow)
            //{
            //    this.ly_store_outTableAdapter.Fill(this.lYStoreMange.ly_store_out, "asd", SQLDatabase.nowUserName());

            //}

            SetFormState("View");

        }

        private void SetFormState(string state)
        {

            if ("View" == state)
            {
                this.formState = "View";

                this.制定日期DateTimePicker.Enabled = false;

                this.说明TextBox.ReadOnly = true;
                this.启用CheckBox.Enabled  = false ;  

                this.bindingNavigatorMoveFirstItem.Enabled = true;
                this.bindingNavigatorMoveLastItem.Enabled = true;
                this.bindingNavigatorMoveNextItem.Enabled = true;
                this.bindingNavigatorMovePreviousItem.Enabled = true;
                this.bindingNavigatorPositionItem.Enabled = true;

                this.bindingNavigator6.Enabled = true;
                this .bindingNavigatorAddNewItem.Enabled = true;
                this.ly_material_plan_mainBindingNavigatorSaveItem.Enabled = false;


                ly_material_plan_mainDataGridView.Enabled = true;
                
            }
            else
            {
                this.formState = "Edit";

                this.制定日期DateTimePicker.Enabled = true ;

                this.说明TextBox.ReadOnly = false ;
                this.bindingNavigatorMoveFirstItem.Enabled = false ;
                this.bindingNavigatorMoveLastItem.Enabled = false ;
                this.bindingNavigatorMoveNextItem.Enabled = false ;
                this.bindingNavigatorMovePreviousItem.Enabled = false ;
                this.bindingNavigatorPositionItem.Enabled = false ;
 
                ly_material_plan_mainDataGridView.Enabled = false ;

                this.bindingNavigator6.Enabled = false;
                this.bindingNavigatorAddNewItem.Enabled = false;
                this.ly_material_plan_mainBindingNavigatorSaveItem.Enabled = true ;


            }


        }





       
      

       
        private Boolean Check_ifApproved(string noPlanNum)
        {
            SqlConnection sqlConnection1 = new SqlConnection(SQLDatabase.Connectstring);
            SqlCommand cmd = new SqlCommand();

            Boolean Approve_flag = true;

            cmd.Parameters.Add("@NowPlannum", SqlDbType.VarChar);
            cmd.Parameters["@NowPlannum"].Value = noPlanNum;


            cmd.CommandText = "LY_GetPlan_ApproveApplyFor";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = sqlConnection1;

            sqlConnection1.Open();
            Approve_flag = Boolean.Parse(cmd.ExecuteScalar().ToString());
            sqlConnection1.Close();
            return Approve_flag;

        }

        private Boolean Check_ifOutstore(string noPlanNum)
        {
            SqlConnection sqlConnection1 = new SqlConnection(SQLDatabase.Connectstring);
            SqlCommand cmd = new SqlCommand();

            Boolean Approve_flag = true;

            cmd.Parameters.Add("@NowPlannum", SqlDbType.VarChar);
            cmd.Parameters["@NowPlannum"].Value = noPlanNum;


            cmd.CommandText = "LY_GetPlan_ifOutstore";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = sqlConnection1;

            sqlConnection1.Open();
            Approve_flag = Boolean.Parse(cmd.ExecuteScalar().ToString());
            sqlConnection1.Close();
            return Approve_flag;

        }




        private string GetMaxOutNum()
        {
            SqlConnection sqlConnection1 = new SqlConnection(SQLDatabase.Connectstring);
            SqlCommand cmd = new SqlCommand();

            string MaxPlanCode = ""; 
            cmd.CommandText = "LY_Get_OutNumber";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = sqlConnection1;

            sqlConnection1.Open();
            MaxPlanCode = cmd.ExecuteScalar().ToString();
            sqlConnection1.Close();



            return MaxPlanCode;
        }



       
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            //if (null == this.ly_plan_getmaterialDataGridView.CurrentRow) return;
            //if (null == dataGridView1.CurrentRow) return;

            if (null == this.ly_material_plan_mainDataGridView.CurrentRow) return;

            if ("False" == ly_material_plan_mainDataGridView.CurrentRow.Cells["启用"].Value.ToString() || "" == ly_material_plan_mainDataGridView.CurrentRow.Cells["启用"].Value.ToString().Trim())
            {

                MessageBox.Show("未批准！", "注意");
                return;
            }

            string planNum = this.ly_material_plan_mainDataGridView.CurrentRow.Cells["计划编号"].Value.ToString();
            string dptId = this.ly_material_plan_mainDataGridView.CurrentRow.Cells["部门码"].Value.ToString();


            string message = "确定领料出库吗?";
            string caption = "提示...";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result;

            result = MessageBox.Show(message, caption, buttons,
            MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

            if (result == DialogResult.Yes)
            {

                NewFrm.Show(this);

                SqlConnection sqlConnection1 = new SqlConnection(SQLDatabase.Connectstring);
                SqlCommand cmd = new SqlCommand(); 

                cmd.Parameters.Add("@plan_num", SqlDbType.VarChar);
                cmd.Parameters["@plan_num"].Value = planNum; 
                cmd.Parameters.Add("@prod_dept", SqlDbType.VarChar);
                cmd.Parameters["@prod_dept"].Value = dptId.ToString(); 
                string outNum = GetMaxOutNum();
                cmd.Parameters.Add("@out_number", SqlDbType.VarChar);
                cmd.Parameters["@out_number"].Value = outNum; 
                cmd.Parameters.Add("@faliaoren", SqlDbType.VarChar);
                cmd.Parameters["@faliaoren"].Value = SQLDatabase.nowUserName(); 
                cmd.Parameters.Add("@nowoutstyle", SqlDbType.VarChar);
                cmd.Parameters["@nowoutstyle"].Value = "改型号领料";

                

                 

                cmd.CommandText = "LY_store_out_Gxh";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = sqlConnection1; 
                cmd.CommandTimeout = 0; 
                sqlConnection1.Open(); 
                try
                { 
                    cmd.ExecuteNonQuery();  
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


        }

       

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void ly_material_plan_mainDataGridView_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (this.formState == "View")
            {

                if (null != this.ly_material_plan_mainDataGridView.CurrentRow)
                {
                    int parentId = int.Parse(this.ly_material_plan_mainDataGridView.Rows[e.RowIndex].Cells["id"].Value.ToString());
                    string planNum = this.ly_material_plan_mainDataGridView.Rows[e.RowIndex].Cells["计划编号"].Value.ToString();

                    //if (this.tabControl2.SelectedIndex == 0)
                    //{
                        this.ly_material_plan_detailTableAdapter.Fill(this.lYPlanMange.ly_material_plan_detail, parentId);
                        this.ly_inma0010_planselAllTableAdapter.Fill(this.lYPlanMange.ly_inma0010_planselAll, parentId);

                    this.ly_store_out_combinationTableAdapter.Fill(this.lYStoreMange.ly_store_out_combination, planNum, SQLDatabase.NowUserID);
                    this.ly_store_innum_combinationTableAdapter.Fill(this.lYStoreMange.ly_store_innum_combination, planNum, SQLDatabase.NowUserID);
                    //  this.ly_plan_getmaterialCombinationOutTableAdapter.Fill(this.lYPlanMange.ly_plan_getmaterialCombinationOut, planNum);

                    //}
                    //else if (this.tabControl2.SelectedIndex == 1)
                    //{
                    //    if (this.tabControl1.SelectedIndex == 0)
                    //    {
                    //        ly_material_plan_explodeTableAdapter.Fill(this.lYPlanMange.ly_material_plan_explode, parentId);
                    //    }
                    //    else if (this.tabControl1.SelectedIndex == 1)
                    //    {

                    //        this.ly_plan_getmaterial_departmentTableAdapter.Fill(this.lYPlanMange.ly_plan_getmaterial_department, planNum);
                    //    }
                    //    else if (this.tabControl1.SelectedIndex == 2)
                    //    {
                    //        // ly_material_plan_mainDataGridView.RowEnter -= this.ly_material_plan_mainDataGridView_RowEnter;

                    //        NewFrm.Show(this.ParentForm);
                    //        Thread.Sleep(500);
                    //        this.ly_store_planitemcountTableAdapter.Fill(this.lYPlanMange.ly_store_planitemcount, parentId);
                    //        NewFrm.Hide(this.ParentForm);
                    //        // ly_material_plan_mainDataGridView.RowEnter += this.ly_material_plan_mainDataGridView_RowEnter;


                    //    }
                    //    else if (this.tabControl1.SelectedIndex == 3)
                    //    {
                    //        this.lY_MaterielRequirementsTableAdapter.Fill(this.lYPlanMange.LY_MaterielRequirements, parentId, "外协", "OWE");
                    //    }
                    //    else if (this.tabControl1.SelectedIndex == 4)
                    //    {
                    //        this.lY_MaterielRequirementsTableAdapter.Fill(this.lYPlanMange.LY_MaterielRequirements, parentId, "外购", "OWE");
                    //    }
                    //    else if (this.tabControl1.SelectedIndex == 5)
                    //    {
                    //        this.lY_MaterielRequirementsTableAdapter.Fill(this.lYPlanMange.LY_MaterielRequirements, parentId, "机加", "OWE");
                    //    }



                    //}


                    this.groupBox3.Text = planNum + ":物料列表";


                }
            }
            else
            {
                // this.yX_taocan_mainBindingSource.Position = this.nowRow;
            }

            //this.Activate();
        }

        private void ly_inma0010_planselDataGridView_DoubleClick(object sender, EventArgs e)
        {
            if (null == this.ly_material_plan_mainDataGridView.CurrentRow) return;
            if (null == ly_inma0010_planselDataGridView.CurrentRow) return;


            //if (ly_material_plan_detailDataGridView.Rows.Count > 0)
            //{

            //    MessageBox.Show("已经有改型号物料存在...", "注意");
            //    return;
            //}

            string planNum = this.ly_material_plan_mainDataGridView.CurrentRow.Cells["计划编号"].Value.ToString();

            if (Check_ifApproved(planNum))
            {

                MessageBox.Show("计划已经批准,不能修改数据...", "注意");
                return;
            }

            //string isAlertplan = this.ly_material_plan_mainDataGridView.CurrentRow.Cells["is_alertPlan"].Value.ToString();

            //if ("True" == isAlertplan)
            //{
            //    MessageBox.Show("警戒计划不用增加成品,直接结算需求即可", "提示");
            //    return;

            //}

            string componentNum = this.ly_inma0010_planselDataGridView.CurrentRow.Cells["物资编号"].Value.ToString();
            int planId = int.Parse(this.ly_material_plan_mainDataGridView.CurrentRow.Cells["id"].Value.ToString());

            //
            //if (decimal.TryParse(this.ly_inma0010_planselDataGridView.CurrentRow.Cells["配套数"].Value.ToString(), out nowabsqty))
            //{

            //}
            //else
            //{
            //    nowabsqty = 0;
            //}

            decimal nowabsqty = 0;
            ChangeValue queryForm = new ChangeValue();
            queryForm.Text = "改型号数量";
            queryForm.ChangeMode = "value";
            queryForm.ShowDialog();

            if (queryForm.NewValue != "")
            {
                try
                {
                    nowabsqty = int.Parse(queryForm.NewValue);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("数量只能为正数...", "注意");
                    return;
                }
                finally { }

            }
            else
            {

                MessageBox.Show("数量不能为空...", "注意");
                return;
            }






            InserPlanDetail(componentNum, planId, nowabsqty);

            CountPlanStru(componentNum, planId, nowabsqty);

         

        }

        private void CountPlanStru(string componentNum, int planId, decimal nowabsqty)
        {

           

            ////////////////////////////////////




            string planNum = this.ly_material_plan_mainDataGridView.CurrentRow.Cells["计划编号"].Value.ToString();

            //string componentNum = this.lY_dayget_material_selDataGridView.CurrentRow.Cells["物资编号"].Value.ToString();
            //int parentId = int.Parse(this.ly_material_plan_mainDataGridView.CurrentRow.Cells["id"].Value.ToString());
            string prod_dept = SQLDatabase.nowUserDepartmentBig();

            //NewFrm.Show(this);
            //Thread.Sleep(500);

            SqlConnection sqlConnection1 = new SqlConnection(SQLDatabase.Connectstring);
            SqlCommand cmd = new SqlCommand();

            cmd.Parameters.Add("@number", SqlDbType.Int);
            cmd.Parameters["@number"].Value = nowabsqty;

            cmd.Parameters.Add("@planId", SqlDbType.Int);
            cmd.Parameters["@planId"].Value = planId;

            cmd.Parameters.Add("@prod_dept", SqlDbType.VarChar);
            cmd.Parameters["@prod_dept"].Value = prod_dept;

            cmd.Parameters.Add("@parentitemno", SqlDbType.VarChar);
            cmd.Parameters["@parentitemno"].Value = componentNum;


            cmd.CommandText = "LY_Combination_request";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = sqlConnection1;

            sqlConnection1.Open();
            cmd.ExecuteNonQuery();
            sqlConnection1.Close();


            this.ly_material_plan_detailTableAdapter.Fill(this.lYPlanMange.ly_material_plan_detail, planId);
            this.ly_inma0010_planselAllTableAdapter.Fill(this.lYPlanMange.ly_inma0010_planselAll, planId);
           // this.ly_plan_getmaterialCombinationOutTableAdapter.Fill(this.lYPlanMange.ly_plan_getmaterialCombinationOut, planNum);


            this.ly_material_plan_detailBindingSource.Position = this.ly_material_plan_detailBindingSource.Find("编号", componentNum);

            //NewFrm.Hide(this);
        }
        private static void InserPlanDetail(string componentNum, int planId, decimal nowabsqty)
        {
            string insStr = " INSERT INTO ly_material_plan_detail  " +
           "( plan_id,wzbh,plan_count,newold) " +
           " values ('" + planId + "','" + componentNum + "'," + nowabsqty + ",'新')";


            using (TransactionScope scope = new TransactionScope())
            {

                SqlConnection sqlConnection1 = new SqlConnection(SQLDatabase.Connectstring);
                SqlCommand cmd = new SqlCommand();

                cmd.CommandText = insStr;
                cmd.CommandType = CommandType.Text;
                cmd.Connection = sqlConnection1;
                cmd.CommandTimeout = 0;


                sqlConnection1.Open();
                cmd.ExecuteNonQuery();

                sqlConnection1.Close();


                scope.Complete();
            }
        }

        private void toolStripTextBox2_KeyUp(object sender, KeyEventArgs e)
        {
            string filterString;


            filterString = GetDataGridviewMultiFilterString.DGVMultiFilterString(this.ly_inma0010_planselDataGridView, this.toolStripTextBox2.Text);


            this.ly_inma0010_planselAllBindingSource.Filter = filterString;
        }

        private void toolStripTextBox2_Enter(object sender, EventArgs e)
        {
            toolStripTextBox2.Text = "";

            this.ly_inma0010_planselAllBindingSource.Filter = "";
        }

        //private void fillToolStripButton_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        this.ly_plan_getmaterialCombinationOutTableAdapter.Fill(this.lYPlanMange.ly_plan_getmaterialCombinationOut, plan_numToolStripTextBox.Text);
        //    }
        //    catch (System.Exception ex)
        //    {
        //        System.Windows.Forms.MessageBox.Show(ex.Message);
        //    }

        //}

        private void bindingNavigator1_RefreshItems(object sender, EventArgs e)
        {

        }

        private void ly_material_plan_mainDataGridView_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            DataGridView dgv = sender as DataGridView;

            if (null == dgv.CurrentRow) return;

            if (!SQLDatabase.CheckHaveRight(SQLDatabase.NowUserID, "计划领料审批"))
            {
                MessageBox.Show("无批准权限...", "注意");

                return;

            }


            if (Check_ifOutstore(dgv.CurrentRow.Cells["计划编号"].Value.ToString()))
            {

                MessageBox.Show("计划已经有出库记录,不能改动...", "注意");
                return;
            }



            if ("启用" == dgv.CurrentCell.OwningColumn.Name)
            {

                if ("True" == dgv.CurrentRow.Cells["启用"].Value.ToString())
                {
                    dgv.CurrentRow.Cells["启用"].Value = "False";

                    dgv.CurrentRow.Cells["批准人"].Value = DBNull.Value;
                    dgv.CurrentRow.Cells["批准日期"].Value = DBNull.Value;

                }
                else
                {

                    dgv.CurrentRow.Cells["启用"].Value = "True";

                    dgv.CurrentRow.Cells["批准人"].Value = SQLDatabase.nowUserName();
                    dgv.CurrentRow.Cells["批准日期"].Value = SQLDatabase.GetNowdate();

                }


                this.ly_material_plan_mainDataGridView.EndEdit();
                this.ly_material_plan_mainBindingSource.EndEdit();

                this.ly_material_plan_mainTableAdapter.Update(this.lYPlanMange.ly_material_plan_main);

                return;

            }
        }

        private void 删除子件ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (null == this.ly_material_plan_detailDataGridView.CurrentRow) return;




            int nowId = int.Parse(this.ly_material_plan_detailDataGridView.CurrentRow.Cells["id1"].Value.ToString());
            int parentId = int.Parse(this.ly_material_plan_mainDataGridView.CurrentRow.Cells["id"].Value.ToString());
            string componentNum = this.ly_material_plan_detailDataGridView.CurrentRow.Cells["物料编号"].Value.ToString();

            string planNum = this.ly_material_plan_mainDataGridView.CurrentRow.Cells["计划编号"].Value.ToString();

           
            if (Check_ifApproved(planNum))
            {

                MessageBox.Show("计划已经批准,不能改动...", "注意");
                return;
            }


            string message1 = "当前(物料：" + componentNum + ")将被删除，继续吗？";
            string caption1 = "提示...";
            MessageBoxButtons buttons1 = MessageBoxButtons.YesNo;

            DialogResult result1;



            result1 = MessageBox.Show(message1, caption1, buttons1,
            MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

            if (result1 == DialogResult.Yes)
            {

                string delstr = " delete ly_material_plan_detail  where id = " + nowId + "";





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

                    this.ly_material_plan_detailTableAdapter.Fill(this.lYPlanMange.ly_material_plan_detail, parentId);
                    this.ly_inma0010_planselAllTableAdapter.Fill(this.lYPlanMange.ly_inma0010_planselAll, parentId);
                  //  this.ly_plan_getmaterialCombinationOutTableAdapter.Fill(this.lYPlanMange.ly_plan_getmaterialCombinationOut, planNum);


                    this.ly_material_plan_detailBindingSource.Position = this.ly_material_plan_detailBindingSource.Find("编号", componentNum);


                  

                    this.ly_inma0010_planselAllBindingSource.Position = this.ly_inma0010_planselAllBindingSource.Find("物资编号", componentNum);

                    // CountPlanStru();
                }


            }
        }

        private void 计划物料计算ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (null == this.ly_material_plan_mainDataGridView.CurrentRow) return;

            string planNum = this.ly_material_plan_mainDataGridView.CurrentRow.Cells["计划编号"].Value.ToString();

            if (Check_ifApproved(planNum))
            {

                MessageBox.Show("计划已经批准,不能修改数据...", "注意");
                return;
            }

           // if (this.formState != "View") return;


           


            //////////////////////////////////

            string message = "计算物料计划物料需求吗？";
            string caption = "提示...";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;

            DialogResult result;



            result = MessageBox.Show(message, caption, buttons,
            MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

            if (result == DialogResult.Yes)
            {

              
                    CountPlanAll();
             
            }
        }
        private void CountPlanAll()
        {
            if (null == this.ly_material_plan_mainDataGridView.CurrentRow) return;

            int planId = int.Parse(this.ly_material_plan_mainDataGridView.CurrentRow.Cells["id"].Value.ToString());
            string planNum = this.ly_material_plan_mainDataGridView.CurrentRow.Cells["计划编号"].Value.ToString();

            NewFrm.Show(this);
            Thread.Sleep(500);

            if (null == this.ly_material_plan_detailDataGridView.CurrentRow) return;

            string nowcomponentNum;
            decimal nowabsqty ;


            foreach (DataGridViewRow dgr in ly_material_plan_detailDataGridView.Rows)
            {
                nowcomponentNum = dgr.Cells["物料编号"].Value.ToString();
                nowabsqty = 0;
                decimal.TryParse ( dgr.Cells["数量"].Value.ToString(),out nowabsqty);

                //this.toolStripLabel3.Text = plannum;
                //this.toolStripLabel3.Invalidate();

                NewFrm.Notify(this, "正在计算:  (" + nowcomponentNum + ")" +  "   成本");



                CountPlanStru(nowcomponentNum, planId, nowabsqty);

            }

            NewFrm.Hide(this);
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (null == this.ly_material_plan_detailDataGridView.CurrentRow) return;




            int nowId = int.Parse(this.ly_material_plan_detailDataGridView.CurrentRow.Cells["id1"].Value.ToString());
            int parentId = int.Parse(this.ly_material_plan_mainDataGridView.CurrentRow.Cells["id"].Value.ToString());

            string componentNum = this.ly_plan_getmaterialCombinationOutDataGridView.CurrentRow.Cells["物料编号out"].Value.ToString();

            string planNum = this.ly_material_plan_mainDataGridView.CurrentRow.Cells["计划编号"].Value.ToString();


            if (Check_ifApproved(planNum))
            {

                MessageBox.Show("计划已经批准,不能改动...", "注意");
                return;
            }


            string message1 = "当前(物料：" + componentNum + ")将被删除，继续吗？";
            string caption1 = "提示...";
            MessageBoxButtons buttons1 = MessageBoxButtons.YesNo;

            DialogResult result1;



            result1 = MessageBox.Show(message1, caption1, buttons1,
            MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

            if (result1 == DialogResult.Yes)
            {

                this.ly_plan_getmaterialCombinationOutBindingSource.RemoveCurrent();


                ly_plan_getmaterialCombinationOutDataGridView.EndEdit();
                ly_plan_getmaterialCombinationOutBindingSource.EndEdit();



                this.ly_plan_getmaterialCombinationOutTableAdapter.Update(this.lYPlanMange.ly_plan_getmaterialCombinationOut);



               // this.ly_plan_getmaterialCombinationOutTableAdapter.Fill(this.lYPlanMange.ly_plan_getmaterialCombinationOut, planNum);


                  


            }
        }

        private void ly_material_plan_detailDataGridView_SelectionChanged(object sender, EventArgs e)
        {
            if (null == this.ly_material_plan_detailDataGridView.CurrentRow)
            {
                this.ly_plan_getmaterialCombinationOutTableAdapter.Fill(this.lYPlanMange.ly_plan_getmaterialCombinationOut, "dsaasd", "asddsa");
                return;
            }

            //string planNum = this.ly_material_plan_mainDataGridView.CurrentRow.Cells["计划编号"].Value.ToString();
            //string parentCode= this.ly_material_plan_detailDataGridView.CurrentRow.Cells["物料编号"].Value.ToString();
            //////if (this.tabControl2.SelectedIndex == 0)
            //////{Rows[e.RowIndex]
            ////this.ly_material_plan_detailTableAdapter.Fill(this.lYPlanMange.ly_material_plan_detail, parentId);
            ////this.ly_inma0010_planselAllTableAdapter.Fill(this.lYPlanMange.ly_inma0010_planselAll, parentId);
            //this.ly_plan_getmaterialCombinationOutTableAdapter.Fill(this.lYPlanMange.ly_plan_getmaterialCombinationOut, planNum, parentCode);

        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            if (null == this.ly_material_plan_mainDataGridView.CurrentRow) return;
            if (null == ly_inma0010_planselDataGridView.CurrentRow) return;
            if (null == this.ly_material_plan_detailDataGridView.CurrentRow)
            {
                
                return;
            }


            //int parentId = int.Parse(this.ly_material_plan_mainDataGridView.CurrentRow.Cells["id"].Value.ToString());

            //string componentNum = this.ly_plan_getmaterialCombinationOutDataGridView.CurrentRow.Cells["物料编号out"].Value.ToString();
            int planId = int.Parse(this.ly_material_plan_mainDataGridView.CurrentRow.Cells["id"].Value.ToString());
            string planNum = this.ly_material_plan_mainDataGridView.CurrentRow.Cells["计划编号"].Value.ToString();
            string parentCode = this.ly_material_plan_detailDataGridView.CurrentRow.Cells["物料编号"].Value.ToString();

            if (Check_ifApproved(planNum))
            {

                MessageBox.Show("计划已经批准,不能改动...", "注意");
                return;
            }

            string message1 = "增加用于当前(物料：" + parentCode + ")的子件出库，继续吗？";
            string caption1 = "提示...";
            MessageBoxButtons buttons1 = MessageBoxButtons.YesNo;

            DialogResult result1;



            result1 = MessageBox.Show(message1, caption1, buttons1,
            MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

            if (result1 == DialogResult.Yes)
            {




                decimal nowabsqty = 0;
                ChangeValue queryForm = new ChangeValue();
                queryForm.Text = "手动增加出库数量";
                queryForm.ChangeMode = "value";
                queryForm.ShowDialog();

                if (queryForm.NewValue != "")
                {
                    try
                    {
                        nowabsqty = int.Parse(queryForm.NewValue);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("数量只能为正数...", "注意");
                        return;
                    }
                    finally { }

                }
                else
                {

                    MessageBox.Show("数量不能为空...", "注意");
                    return;
                }




                string componentNum = this.ly_inma0010_planselDataGridView.CurrentRow.Cells["物资编号"].Value.ToString();

                string componentName = this.ly_inma0010_planselDataGridView.CurrentRow.Cells["名称sel"].Value.ToString();
                string componentUnit = this.ly_inma0010_planselDataGridView.CurrentRow.Cells["单位sel"].Value.ToString();



                InserOutDetail(planId, planNum, parentCode, componentNum, nowabsqty, componentName, componentUnit);

                this.ly_plan_getmaterialCombinationOutTableAdapter.Fill(this.lYPlanMange.ly_plan_getmaterialCombinationOut, planNum, parentCode);

            }

        }

        private static void InserOutDetail(int planId,string planNum, 
            string parentCode,string nowcomponantCode, decimal nowabsqty,
            string nowitem,string nowunit)
        {

            

            string insStr = " INSERT INTO ly_plan_getmaterial  " +
           "([plan_id],[material_plan_num],[absqty],[newold], [parentno]," +
           " [itemno],[itemname],[unit], [fromwhere]) " +
          
           " values (" + planId + ",'" + planNum + "'," + nowabsqty + ",'新','" + parentCode + "','" +
               nowcomponantCode + "','" + nowitem + "','" + nowunit + "','"+SQLDatabase.nowUserName()



          +  "增加')";


            using (TransactionScope scope = new TransactionScope())
            {

                SqlConnection sqlConnection1 = new SqlConnection(SQLDatabase.Connectstring);
                SqlCommand cmd = new SqlCommand();

                cmd.CommandText = insStr;
                cmd.CommandType = CommandType.Text;
                cmd.Connection = sqlConnection1;
                cmd.CommandTimeout = 0;


                sqlConnection1.Open();
                cmd.ExecuteNonQuery();

                sqlConnection1.Close();


                scope.Complete();
            }
        }

        //private void fillToolStripButton_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        this.ly_store_outnumTableAdapter.Fill(this.lYStoreMange.ly_store_outnum, plan_numToolStripTextBox.Text, yonghu_codeToolStripTextBox.Text);
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
        //        this.ly_store_outTableAdapter.Fill(this.lYStoreMange.ly_store_out, out_numberToolStripTextBox.Text, yonghu_nameToolStripTextBox.Text);
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
        //        this.ly_store_out_combinationTableAdapter.Fill(this.lYStoreMange.ly_store_out_combination, plan_numToolStripTextBox.Text, yonghu_codeToolStripTextBox.Text);
        //    }
        //    catch (System.Exception ex)
        //    {
        //        System.Windows.Forms.MessageBox.Show(ex.Message);
        //    }

        //}

        private void ly_store_out_combinationDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
                    }

        private void ly_store_outnumDataGridView_SelectionChanged(object sender, EventArgs e)
        {
            
                if (null == ly_store_outnumDataGridView.CurrentRow)

          
            {

                this.ly_store_innum_combination_detailTableAdapter.Fill(this.lYStoreMange.ly_store_innum_combination_detail, "", SQLDatabase.NowUserID);
                this.ly_store_out_combination_detailTableAdapter.Fill(this.lYStoreMange.ly_store_out_combination_detail, "",SQLDatabase.nowUserName());
                return;
            }




            string outNum = this.ly_store_outnumDataGridView.CurrentRow.Cells["out_number"].Value.ToString();


                    this.ly_store_out_combination_detailTableAdapter.Fill(this.lYStoreMange.ly_store_out_combination_detail, outNum, SQLDatabase.nowUserName());

                    //string deptcode = this.ly_store_outnumDataGridView.CurrentRow.Cells["out_deptcode"].Value.ToString();
                    //string warehouse = this.ly_store_outnumDataGridView.CurrentRow.Cells["warehouse"].Value.ToString();

                    //if (!string.IsNullOrEmpty(deptcode))
                    //{
                    //    if ("全部" == warehouse)
                    //    {

                    //        this.ly_plan_getmaterial_departmentBindingSource.Filter = "领料部门='" + deptcode + "'";
                    //        this.comboBox1.SelectedValue = deptcode;
                    //        this.comboBox2.SelectedValue = warehouse;
                    //    }
                    //    else
                    //    {
                    //        this.ly_plan_getmaterial_departmentBindingSource.Filter = "领料部门='" + deptcode + "' and 仓库='" + warehouse + "'";
                    //        this.comboBox1.SelectedValue = deptcode;
                    //        this.comboBox2.SelectedValue = warehouse;
                    //    }

            ///////

                    //}

            
        }

        private void ly_store_innum_combinationDataGridView_SelectionChanged(object sender, EventArgs e)
        {
            if (null == this.ly_store_innum_combinationDataGridView.CurrentRow)
            {

                this.ly_store_innum_combination_detailTableAdapter.Fill(this.lYStoreMange.ly_store_innum_combination_detail, "", SQLDatabase.NowUserID);

                return;
            }


            string nowInNum = this.ly_store_innum_combinationDataGridView.CurrentRow.Cells["入库单号"].Value.ToString();
            this.ly_store_innum_combination_detailTableAdapter.Fill(this.lYStoreMange.ly_store_innum_combination_detail, nowInNum, SQLDatabase.NowUserID);
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            //string message = "确定改型号出入库吗?";
            //string caption = "提示...";
            //MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            //DialogResult result;



            //result = MessageBox.Show(message, caption, buttons,
            //MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

            //if (result == DialogResult.Yes)
            //{
            //    string planNum = this.ly_material_plan_mainDataGridView.CurrentRow.Cells["计划编号"].Value.ToString();
            //    CountStoreInOutAuto(planNum);
            //}


        }
        private void CountStoreInOutAuto(string plannum)
        {

            SqlConnection sqlConnection1 = new SqlConnection(SQLDatabase.Connectstring);
            SqlCommand cmd = new SqlCommand();



            cmd.Parameters.Add("@plan_num", SqlDbType.VarChar);
            cmd.Parameters["@plan_num"].Value = plannum;

            

            cmd.Parameters.Add("@inputman", SqlDbType.VarChar);
            cmd.Parameters["@inputman"].Value = SQLDatabase.nowUserName();

          

            cmd.CommandText = "LY_combination_InOut_store";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = sqlConnection1;
            cmd.CommandTimeout = 0;


            sqlConnection1.Open();

            try
            {
                NewFrm.Show(this);
                cmd.ExecuteNonQuery();
                NewFrm.Hide(this);
                MessageBox.Show("完成", "注意");
            }
            catch (SqlException sqle)
            {
                
                NewFrm.Hide(this);
                MessageBox.Show("失败", "注意");
            }
            finally
            {
                sqlConnection1.Close();

            }
        }

        private void bindingNavigatorAddNewItem_Click(object sender, EventArgs e)
        {
            string message = "增加改型号计划吗？";
            string caption = "提示...";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;

            DialogResult result;



            result = MessageBox.Show(message, caption, buttons,
            MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

            if (result == DialogResult.Yes)
            {
                this.ly_material_plan_mainBindingSource.AddNew();
                this.ly_material_plan_mainDataGridView.CurrentRow.Cells["计划编号"].Value = GetMaxPlanCode();
                this.ly_material_plan_mainDataGridView.CurrentRow.Cells["制定日期"].Value = DateTime.Now;
                this.ly_material_plan_mainDataGridView.CurrentRow.Cells["制定人"].Value = SQLDatabase.nowUserName();
                this.ly_material_plan_mainDataGridView.CurrentRow.Cells["部门码"].Value = SQLDatabase.nowUserDepartment();
                this.ly_material_plan_mainBindingSource.EndEdit();

                this.Validate();
                this.ly_material_plan_mainBindingSource.EndEdit();
                this.ly_material_plan_mainTableAdapter.Update(this.lYPlanMange.ly_material_plan_main);

                SetFormState("Edit");
                this.制定日期DateTimePicker.Focus();

            }
        }
        private string GetMaxPlanCode()
        {
            SqlConnection sqlConnection1 = new SqlConnection(SQLDatabase.Connectstring);
            SqlCommand cmd = new SqlCommand();

            string MaxPlanCode = "";

            cmd.Parameters.Add("@Plan_mode", SqlDbType.VarChar);
            cmd.Parameters["@Plan_mode"].Value = "GXJH";


            cmd.CommandText = "LY_GetMaxPlanCode";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = sqlConnection1;

            sqlConnection1.Open();
            MaxPlanCode = cmd.ExecuteScalar().ToString();
            sqlConnection1.Close();



            return MaxPlanCode;
        }

        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            if (null == ly_material_plan_mainDataGridView.CurrentRow) return;

            if (Check_ifApproved(this.ly_material_plan_mainDataGridView.CurrentRow.Cells["计划编号"].Value.ToString()))
            {

                MessageBox.Show("计划已经批准,不能改动...", "注意");
                return;
            }

            SetFormState("Edit");
        }

        private void ly_material_plan_mainBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.ly_material_plan_mainBindingSource.EndEdit();
            this.ly_material_plan_mainTableAdapter.Update(this.lYPlanMange.ly_material_plan_main);

            SetFormState("View");
        }

        private void toolStripButton7_Click(object sender, EventArgs e)
        {

            if (!Check_ifApproved(this.ly_material_plan_mainDataGridView.CurrentRow.Cells["计划编号"].Value.ToString()))
            {

                MessageBox.Show("计划未经批准,不能出入库...", "注意");
                return;
            }

            string message = "确定改型号出入库吗?";
            string caption = "提示...";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result;



            result = MessageBox.Show(message, caption, buttons,
            MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

            if (result == DialogResult.Yes)
            {
                string planNum = this.ly_material_plan_mainDataGridView.CurrentRow.Cells["计划编号"].Value.ToString();
                int parentId = int.Parse(this.ly_material_plan_mainDataGridView.CurrentRow.Cells["id"].Value.ToString());
                CountStoreInOutAuto(planNum);




                //this.ly_material_plan_detailTableAdapter.Fill(this.lYPlanMange.ly_material_plan_detail, parentId);
                //this.ly_inma0010_planselAllTableAdapter.Fill(this.lYPlanMange.ly_inma0010_planselAll, parentId);
                NewFrm.Show(this);


                this.ly_material_plan_detailTableAdapter.Fill(this.lYPlanMange.ly_material_plan_detail, parentId);
                this.ly_store_out_combinationTableAdapter.Fill(this.lYStoreMange.ly_store_out_combination, planNum, SQLDatabase.NowUserID);
                this.ly_store_innum_combinationTableAdapter.Fill(this.lYStoreMange.ly_store_innum_combination, planNum, SQLDatabase.NowUserID);

                NewFrm.Hide(this);

            }
        }

        private void ly_material_plan_detailDataGridView_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (Check_ifApproved(this.ly_material_plan_mainDataGridView.CurrentRow.Cells["计划编号"].Value.ToString()))
            {

                MessageBox.Show("计划已经批准,不能改动...", "注意");
                return;
            }


            //if ("True" == this.ly_material_plan_mainDataGridView.CurrentRow.Cells["启用"].Value.ToString())
            //{
            //    MessageBox.Show("领料计划已经审批,不能修改条目...", "注意");
            //    return;
            //}

            DataGridView dgv = sender as DataGridView;




            if ("old_count" == dgv.CurrentCell.OwningColumn.Name)
            {
                
                ChangeValue queryForm = new ChangeValue();

                queryForm.OldValue = dgv.CurrentCell.Value.ToString();
                queryForm.NewValue = "";
                queryForm.ChangeMode = "value";
                queryForm.ShowDialog();

                if (queryForm.NewValue != "")
                {
                    dgv.CurrentRow.Cells["old_count"].Value = queryForm.NewValue;

                    this.ly_material_plan_detailDataGridView.EndEdit();
                    this.ly_material_plan_detailBindingSource.EndEdit();
                    this.ly_material_plan_detailTableAdapter.Update(this.lYPlanMange.ly_material_plan_detail);

                     
                }
                return;

            }


            //if ("新旧" == dgv.CurrentCell.OwningColumn.Name)
            //{
            //    string sel = "SELECT oldnew as 新旧 FROM ly_finishproduct_newold  ";
            //    QueryForm queryForm = new QueryForm();
            //    queryForm.Sel = sel;
            //    queryForm.Constr = SQLDatabase.Connectstring;
            //    queryForm.ShowDialog();

            //    if (queryForm.Result != "")
            //    {
            //        dgv.CurrentRow.Cells["新旧"].Value = queryForm.Result;
            //        SaveChanged();

            //    }
            //    return;

            //}
        }

        private void ly_plan_getmaterialCombinationOutDataGridView_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            
                 if (Check_ifApproved(this.ly_material_plan_mainDataGridView.CurrentRow.Cells["计划编号"].Value.ToString()))
            {

                MessageBox.Show("计划已经批准,不能改动...", "注意");
                return;
            }


            //if ("True" == this.ly_material_plan_mainDataGridView.CurrentRow.Cells["启用"].Value.ToString())
            //{
            //    MessageBox.Show("领料计划已经审批,不能修改条目...", "注意");
            //    return;
            //}

            DataGridView dgv = sender as DataGridView;




            if ("old_count_out" == dgv.CurrentCell.OwningColumn.Name)
            {

                ChangeValue queryForm = new ChangeValue();

                queryForm.OldValue = dgv.CurrentCell.Value.ToString();
                queryForm.NewValue = "";
                queryForm.ChangeMode = "value";
                queryForm.ShowDialog();

                if (queryForm.NewValue != "")
                {
                    dgv.CurrentRow.Cells["old_count_out"].Value = queryForm.NewValue;

                    this.ly_plan_getmaterialCombinationOutDataGridView.EndEdit();
                    this.ly_plan_getmaterialCombinationOutBindingSource.EndEdit();
                    this.ly_plan_getmaterialCombinationOutTableAdapter.Update(this.lYPlanMange.ly_plan_getmaterialCombinationOut);


                }
                return;

            }
        }

        private void toolStripButton2_Click_1(object sender, EventArgs e)
        {
            this.ly_material_plan_mainTableAdapter.Fill(this.lYPlanMange.ly_material_plan_main, "GXJH");
        }

        private void toolStripButton8_Click(object sender, EventArgs e)
        {
            if (null == this.ly_store_innum_combinationDataGridView.CurrentRow) return;

            string nowoperptar = this.ly_store_innum_combinationDataGridView.CurrentRow.Cells["收料人"].Value.ToString();

            if (nowoperptar != SQLDatabase.nowUserName())
            {
                MessageBox.Show("请操作人:" + nowoperptar + "删除", "注意");
                return;
            }

            if ("True" == ly_store_innum_combinationDataGridView.CurrentRow.Cells["签证"].Value.ToString())
            {
                MessageBox.Show("已经签证,不能删除入库单...");

                return;

            }

            string innumber = ly_store_innum_combinationDataGridView.CurrentRow.Cells["入库单号"].Value.ToString();
            //string storename = ly_store_innum_combinationDataGridView.CurrentRow.Cells["仓库"].Value.ToString();

            string message = "删除当前入库单:" + innumber + "吗？";
            string caption = "提示...";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;

            DialogResult result;



            result = MessageBox.Show(message, caption, buttons,
            MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

            if (result == DialogResult.Yes)
            {

                string delstr = " delete ly_store_in  from ly_store_in  where ly_store_in.in_number = '" + innumber + "' ";


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
                }

                string planNum = this.ly_material_plan_mainDataGridView.CurrentRow.Cells["计划编号"].Value.ToString();
                int parentId = int.Parse(this.ly_material_plan_mainDataGridView.CurrentRow.Cells["id"].Value.ToString());
                //CountStoreInOutAuto(planNum);




                this.ly_material_plan_detailTableAdapter.Fill(this.lYPlanMange.ly_material_plan_detail, parentId);
                //this.ly_inma0010_planselAllTableAdapter.Fill(this.lYPlanMange.ly_inma0010_planselAll, parentId);

                this.ly_store_out_combinationTableAdapter.Fill(this.lYStoreMange.ly_store_out_combination, planNum, SQLDatabase.NowUserID);
                this.ly_store_innum_combinationTableAdapter.Fill(this.lYStoreMange.ly_store_innum_combination, planNum, SQLDatabase.NowUserID);



            }
        }

        private void 打印PToolStripButton_Click(object sender, EventArgs e)
        {
            if (null == this.ly_store_outDataGridView.CurrentRow) return;

            //int parentId = int.Parse(this.ly_material_plan_mainDataGridView.CurrentRow.Cells["id"].Value.ToString());


            string nowDepartment = this.ly_store_outDataGridView.CurrentRow.Cells["DepartmentName"].Value.ToString();



            frmWaiting.Show(this);

            //this.ly_plan_getmaterialTableAdapter.Fill(this.lYPlanMange.ly_plan_getmaterial, parentId);

            BaseReportView queryForm = new BaseReportView();

            queryForm.Text = "中原精密生产领料单";

            queryForm.Printdata = this.lYStoreMange;

            if ("营业部" != nowDepartment)
            {
                queryForm.PrintCrystalReport = new LY_Lingliaodan_combination();
            }
            else
            {
                queryForm.PrintCrystalReport = new LY_LingliaodanYingye();
            }


            //string selectFormula;

            //selectFormula = "{ly_store_planitemcount.状态}  =   '原料'  and {ly_store_planitemcount.欠料金额}>0 ";
            //queryForm.PrintCrystalReport.DataDefinition.RecordSelectionFormula = selectFormula;

            frmWaiting.Hide(this);

            queryForm.ShowDialog();
        }

        private void toolStripButton8_Click_1(object sender, EventArgs e)
        {
            if (null == this.ly_store_innum_combinationDataGridView.CurrentRow) return;

            BaseReportView queryForm = new BaseReportView();
            queryForm.Text = "中原精密电装改制入库单";
            queryForm.Printdata = this.lYStoreMange;
            queryForm.PrintCrystalReport = new LY_ProductRukudan_Combination();
            queryForm.ShowDialog();
        }

        private void ly_material_plan_detailDataGridView_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            //if (null == this.ly_material_plan_detailDataGridView.CurrentRow)
            //{
            //    this.ly_plan_getmaterialCombinationOutTableAdapter.Fill(this.lYPlanMange.ly_plan_getmaterialCombinationOut, "dsaasd", "asddsa");
            //    return;
            //}

            string planNum = this.ly_material_plan_mainDataGridView.CurrentRow.Cells["计划编号"].Value.ToString();
            string parentCode = this.ly_material_plan_detailDataGridView.Rows[e.RowIndex].Cells["物料编号"].Value.ToString();
            ////if (this.tabControl2.SelectedIndex == 0)
            ////{Rows[e.RowIndex]
            //this.ly_material_plan_detailTableAdapter.Fill(this.lYPlanMange.ly_material_plan_detail, parentId);
            //this.ly_inma0010_planselAllTableAdapter.Fill(this.lYPlanMange.ly_inma0010_planselAll, parentId);
            this.ly_plan_getmaterialCombinationOutTableAdapter.Fill(this.lYPlanMange.ly_plan_getmaterialCombinationOut, planNum, parentCode);

        }

        //private void fillToolStripButton_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        this.ly_store_innum_combination_detailTableAdapter.Fill(this.lYStoreMange.ly_store_innum_combination_detail, in_numberToolStripTextBox.Text, yonghu_codeToolStripTextBox.Text);
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
        //        this.ly_store_in_innumTableAdapter.Fill(this.lYStoreMange.ly_store_in_innum, in_numberToolStripTextBox1.Text, yonghu_codeToolStripTextBox1.Text);
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
        //        this.ly_store_in_innumTableAdapter.Fill(this.lYStoreMange.ly_store_in_innum, in_numberToolStripTextBox.Text, yonghu_codeToolStripTextBox.Text);
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
        //        this.ly_store_innum_combinationTableAdapter.Fill(this.lYStoreMange.ly_store_innum_combination, bill_codeToolStripTextBox.Text, yonghu_codeToolStripTextBox.Text);
        //    }
        //    catch (System.Exception ex)
        //    {
        //        System.Windows.Forms.MessageBox.Show(ex.Message);
        //    }

        //}
    }
}
