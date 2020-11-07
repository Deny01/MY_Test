using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Transactions;
using HappyYF.Infrastructure.Repositories;
using DataGridFilter;

namespace HappyYF.YuanXin.WorkSet
{
    public partial class LY_DayGet_Plan_New  : Form
    {
         string formState = "View";

         public LY_DayGet_Plan_New()
        {
            InitializeComponent();
        }

        private void ly_material_plan_mainBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.ly_material_plan_mainBindingSource.EndEdit();
            this.ly_material_plan_mainTableAdapter.Update(this.lYPlanMange.ly_material_plan_main);

            SetFormState("View");

        }

        private void LY_Material_Plan_Load(object sender, EventArgs e)
        {
            this.ly_material_plan_mainTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            this.lY_dayget_material_sel_newTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            this.ly_plan_getmaterialPlanTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;

            if (SQLDatabase.CheckHaveRight(SQLDatabase.NowUserID, "赠品分配"))
            {
                this.gIFTCheckBox.Visible = true;
            }
            else
            {

                this.gIFTCheckBox.Visible = false;
            }

            if (SQLDatabase.CheckHaveRight(SQLDatabase.NowUserID, "物料报废"))
            {
                this.报废CheckBox.Visible = true;
            }
            else
            {

                this.报废CheckBox.Visible = false;
            }


            if ("0003" == SQLDatabase.nowUserDepartmentBig() || "000" == SQLDatabase.NowUserID || "999" == SQLDatabase.NowUserID || "998" == SQLDatabase.NowUserID || "66" == SQLDatabase.NowUserID)
            {
                this.客户名称TextBox.Visible = true;
                客户名称Label.Visible = true;
                this.客户编码TextBox.Visible = true;
                this.营业员码TextBox.Visible = true;
                营业员Label.Visible = true;
                this.营业员TextBox.Visible = true;
                this.button8.Visible = true;

            }
            else
            {

                this.客户名称TextBox.Visible = false ;
                客户名称Label.Visible = false ;
                this.客户编码TextBox.Visible = false ;
                this.营业员码TextBox.Visible = false ;
                营业员Label.Visible = false ;
                this.营业员TextBox.Visible = false ;
                this.button8.Visible = false ;
            }


            if (!SQLDatabase.CheckHaveRight(SQLDatabase.NowUserID, "计划领料审批"))
            {
                this.ly_material_plan_mainBindingSource.Filter = "制定人='" + SQLDatabase.nowUserName()+"'";
                //this.完成CheckBox.Visible = false;
                
            }
            else
            {
                if ("000" == SQLDatabase.NowUserID ||  "998" == SQLDatabase.NowUserID || "66" == SQLDatabase.NowUserID)
                {
                    this.ly_material_plan_mainBindingSource.Filter = "";
                }
                else if ("999" == SQLDatabase.NowUserID )
                {
                    this.ly_material_plan_mainBindingSource.Filter = "部门='0003'";
                }
                else if ("906" == SQLDatabase.NowUserID)
                {
                    this.ly_material_plan_mainBindingSource.Filter = "部门='0006'";
                }
                else
                {
                    this.ly_material_plan_mainBindingSource.Filter = "部门='" + SQLDatabase.nowUserDepartmentBig() + "'";
                }
                //this.完成CheckBox.Visible = true ;
            }

            this.ly_material_plan_mainTableAdapter.Fill(this.lYPlanMange.ly_material_plan_main,"LLJH");

            SetFormState("View");

        }

        private void SetFormState(string state)
        {
            // view edit add save cancle

            //return;

            if ("View" == state)
            {
                this.formState = "View";

                this.制定日期DateTimePicker.Enabled = false;

                this.说明TextBox.ReadOnly = true;
                this.启用CheckBox.Enabled  = false ;
                this.完成CheckBox.Enabled  = false ;
              



                this.bindingNavigatorMoveFirstItem.Enabled = true;
                this.bindingNavigatorMoveLastItem.Enabled = true;
                this.bindingNavigatorMoveNextItem.Enabled = true;
                this.bindingNavigatorMovePreviousItem.Enabled = true;
                this.bindingNavigatorPositionItem.Enabled = true;




                toolStripButton2.Enabled = true;
                bindingNavigatorDeleteItem.Enabled = true;
                bindingNavigatorAddNewItem.Enabled = true;
                ly_material_plan_mainBindingNavigatorSaveItem.Enabled = false;

                //yX_clientBindingNavigatorSaveItem.Enabled = true ;

                this .gIFTCheckBox.Enabled = false;
                ly_material_plan_mainDataGridView.Enabled = true;

                button8.Enabled = false;


            }
            else
            {
                this.formState = "Edit";

                this.制定日期DateTimePicker.Enabled = true ;

                this.说明TextBox.ReadOnly = false ;

                //if (!SQLDatabase.CheckHaveRight(SQLDatabase.NowUserID, "计划领料审批"))
                //{
                    //this.启用CheckBox.Enabled = false ;
                    //this.完成CheckBox.Enabled = false ;
                //}
                //else
                //{
                //    this.启用CheckBox.Enabled = true;
                //    this.完成CheckBox.Enabled = true;
                //}




                this.bindingNavigatorMoveFirstItem.Enabled = false ;
                this.bindingNavigatorMoveLastItem.Enabled = false ;
                this.bindingNavigatorMoveNextItem.Enabled = false ;
                this.bindingNavigatorMovePreviousItem.Enabled = false ;
                this.bindingNavigatorPositionItem.Enabled = false ;




                toolStripButton2.Enabled = false ;
                bindingNavigatorDeleteItem.Enabled = false ;
                bindingNavigatorAddNewItem.Enabled = false ;
                ly_material_plan_mainBindingNavigatorSaveItem.Enabled = true ;

                //yX_clientBindingNavigatorSaveItem.Enabled = true ;
                this.gIFTCheckBox.Enabled = true ;

                ly_material_plan_mainDataGridView.Enabled = false ;

                button8.Enabled = true;
            }


        }

        private string GetMaxPlanCode()
        {
            SqlConnection sqlConnection1 = new SqlConnection(SQLDatabase.Connectstring);
            SqlCommand cmd = new SqlCommand();

            string MaxPlanCode = "";

            cmd.Parameters.Add("@Plan_mode", SqlDbType.VarChar);
            cmd.Parameters["@Plan_mode"].Value = "LLJH";


            cmd.CommandText = "LY_GetMaxPlanCode";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = sqlConnection1;

            sqlConnection1.Open();
            MaxPlanCode = cmd.ExecuteScalar().ToString();
            sqlConnection1.Close();



            return MaxPlanCode;
        }

        private void bindingNavigatorAddNewItem_Click(object sender, EventArgs e)
        {
            string message = "增加物料计划吗？";
            string caption = "提示...";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;

            DialogResult result;



            result = MessageBox.Show(message, caption, buttons,
            MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

            if (result == DialogResult.Yes)
            {
                this.ly_material_plan_mainBindingSource.AddNew();
                this.ly_material_plan_mainDataGridView.CurrentRow.Cells["计划编号"].Value = GetMaxPlanCode();
                this.ly_material_plan_mainDataGridView.CurrentRow.Cells["制定日期"].Value =DateTime .Now ;
                this.ly_material_plan_mainDataGridView.CurrentRow.Cells["制定人"].Value = SQLDatabase.nowUserName();
                this.ly_material_plan_mainDataGridView.CurrentRow.Cells["部门码"].Value = SQLDatabase.nowUserDepartment();
                this.ly_material_plan_mainBindingSource.EndEdit();

                this.Validate();
                this.ly_material_plan_mainBindingSource.EndEdit();

             

                    this.ly_material_plan_mainTableAdapter.Update(this.lYPlanMange.ly_material_plan_main);

              

                SetFormState("Edit");
                this.制定日期DateTimePicker.Focus();

                //DataRowView nowCard = (DataRowView)this.yX_clientBindingSource.Current;

                //   nowCard["Card_number"].; nowCard.

            }
        }

        

        private void bindingNavigatorDeleteItem_Click(object sender, EventArgs e)
        {
            if (null == this.ly_material_plan_mainDataGridView.CurrentRow) return;


            if (Check_ifApproved(this.ly_material_plan_mainDataGridView.CurrentRow.Cells["计划编号"].Value.ToString()))
            {

                MessageBox.Show("计划已经批准,不能改动...", "注意");
                return;
            }

            if (this.ly_plan_getmaterialDataGridView.RowCount > 0)
            {
                MessageBox.Show("计划已有物料记录，不能删除(实需删除，请先删除该计划的物料记录)", "注意");
                return;

            }

            if ("True" == this.ly_material_plan_mainDataGridView.CurrentRow.Cells["启用"].Value.ToString())
            {
                MessageBox.Show("领料计划已经审批,不能删除...", "注意");
                return;
            }

            string nowPlanNumber = this.ly_material_plan_mainDataGridView.CurrentRow.Cells["计划编号"].Value.ToString();


            string message1 = "当前(计划：" + nowPlanNumber + ")将被删除，继续吗？";
            string caption1 = "提示...";
            MessageBoxButtons buttons1 = MessageBoxButtons.YesNo;

            DialogResult result1;



            result1 = MessageBox.Show(message1, caption1, buttons1,
            MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

            if (result1 == DialogResult.Yes)
            {

                string delstr = " delete ly_material_plan_main  where material_plan_num = '" + nowPlanNumber + "'";

           
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


                    this.ly_material_plan_mainTableAdapter.Fill(this.lYPlanMange.ly_material_plan_main, "LLJH");
                }


            }
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            if (null == ly_material_plan_mainDataGridView.CurrentRow) return;

            if (Check_ifApproved(this.ly_material_plan_mainDataGridView.CurrentRow.Cells["计划编号"].Value.ToString()))
            {

                MessageBox.Show("计划已经批准,不能改动...", "注意");
                return;
            }

            SetFormState("Edit");
        }

       
        private void toolStripTextBox2_KeyUp(object sender, KeyEventArgs e)
        {
            string dFilter = "";

            //for (int i = 0; i < this.hT_Vehicle_collectDataGridView.ColumnCount; i++)
            for (int i = 1; i < 10; i++)
            {
                string tempColumnName = this.lY_dayget_material_selDataGridView.Columns[i].DataPropertyName;

                if (i != 9)
                    dFilter = dFilter + tempColumnName + " like  '*" + this.toolStripTextBox2.Text + "*' or ";
                else
                    dFilter = dFilter + tempColumnName + " like  '*" + this.toolStripTextBox2.Text + "*' ";

            }

            if (this.toolStripTextBox2.Text.Replace(" ", "").Length > 0)

                this.lY_dayget_material_sel_newBindingSource.Filter = dFilter;
            else
                this.lY_dayget_material_sel_newBindingSource.Filter = " ";
        }

        private void toolStripTextBox2_Enter(object sender, EventArgs e)
        {
            toolStripTextBox2.Text = "";

            this.lY_dayget_material_sel_newBindingSource.Filter = "";
        }

        private void ly_material_plan_mainDataGridView_SelectionChanged(object sender, EventArgs e)
        {
            if (this.formState == "View")
            {
                if (null != this.ly_material_plan_mainDataGridView.CurrentRow)
                {
                    int parentId = int.Parse(this.ly_material_plan_mainDataGridView.CurrentRow.Cells["id"].Value.ToString());
                    string planNum = this.ly_material_plan_mainDataGridView.CurrentRow.Cells["计划编号"].Value.ToString();

                    
                    this.lY_dayget_material_sel_newTableAdapter.Fill(this.lYPlanMange.LY_dayget_material_sel_new, parentId,SQLDatabase.nowUserDepartment());



                    this.ly_plan_getmaterialPlanTableAdapter.Fill(this.lYPlanMange.ly_plan_getmaterialPlan, planNum);


                    this.groupBox3.Text = planNum +":物料列表";

                   
                }
            }
            else
            {
                // this.yX_taocan_mainBindingSource.Position = this.nowRow;
            }
        }

        private void ly_inma0010_planselDataGridView_DoubleClick(object sender, EventArgs e)
        {
            if (null == this.ly_material_plan_mainDataGridView.CurrentRow) return;
            if (null == lY_dayget_material_selDataGridView.CurrentRow) return;

            if (Check_ifApproved(this.ly_material_plan_mainDataGridView.CurrentRow.Cells["计划编号"].Value.ToString()))
            {

                MessageBox.Show("计划已经批准,不能改动...", "注意");
                return;
            }

            //string componentNum = this.lY_dayget_material_selDataGridView.CurrentRow.Cells["物资编号"].Value.ToString();
            //int parentId = int.Parse(this.ly_material_plan_mainDataGridView.CurrentRow.Cells["id"].Value.ToString());


            CountPlanStru();
            

            
        }

        private void 删除子件ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (null == this.ly_plan_getmaterialDataGridView.CurrentRow) return;

            if (Check_ifApproved(this.ly_material_plan_mainDataGridView.CurrentRow.Cells["计划编号"].Value.ToString()))
            {

                MessageBox.Show("计划已经批准,不能改动...", "注意");
                return;
            }
            if ("True" == this.ly_material_plan_mainDataGridView.CurrentRow.Cells["启用"].Value.ToString())
            {
                MessageBox.Show("领料计划已经审批,不能删除条目...", "注意");
                return;
            }


            int nowId = int.Parse(this.ly_plan_getmaterialDataGridView.CurrentRow.Cells["id1"].Value.ToString());
            int parentId = int.Parse(this.ly_material_plan_mainDataGridView.CurrentRow.Cells["id"].Value.ToString());
            string componentNum = this.ly_plan_getmaterialDataGridView.CurrentRow.Cells["物料编号"].Value.ToString();
            decimal haveget = 0;
            if (string.IsNullOrEmpty(this.ly_plan_getmaterialDataGridView.CurrentRow.Cells["已领数量"].Value.ToString()))
            {
                haveget = decimal.Parse(this.ly_plan_getmaterialDataGridView.CurrentRow.Cells["已领数量"].Value.ToString());
            }

            if (0 < haveget)
            {
                MessageBox.Show("已有领料记录,不能删除条目...", "注意");
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

                string delstr = " delete ly_plan_getmaterial  where id = " + nowId + "";





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
                    this.lY_dayget_material_sel_newTableAdapter.Fill(this.lYPlanMange.LY_dayget_material_sel_new, parentId, SQLDatabase.nowUserDepartment());
                    string planNum = this.ly_material_plan_mainDataGridView.CurrentRow.Cells["计划编号"].Value.ToString();
                    this.ly_plan_getmaterialPlanTableAdapter.Fill(this.lYPlanMange.ly_plan_getmaterialPlan, planNum);


                    this.lY_dayget_material_sel_newBindingSource.Position = this.lY_dayget_material_sel_newBindingSource.Find("物资编号", componentNum);

                    //CountPlanStru();
                }


            }
        }

        private void SaveChanged()
        {
            ///////////////////////////

            this.ly_plan_getmaterialDataGridView.EndEdit();


            this.ly_plan_getmaterialPlanBindingSource.EndEdit();



            this.ly_plan_getmaterialPlanTableAdapter.Update(this.lYPlanMange.ly_plan_getmaterialPlan);



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

        private void ly_material_plan_detailDataGridView_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {


            if (Check_ifApproved(this.ly_material_plan_mainDataGridView.CurrentRow.Cells["计划编号"].Value.ToString()))
            {

                MessageBox.Show("计划已经批准,不能改动...", "注意");
                return;
            }


            if ("True" == this.ly_material_plan_mainDataGridView.CurrentRow.Cells["启用"].Value.ToString())
            {
                MessageBox.Show("领料计划已经审批,不能修改条目...", "注意");
                return;
            }

            DataGridView dgv = sender as DataGridView;




            if ("领料数量" == dgv.CurrentCell.OwningColumn.Name)
            {

                ChangeValue queryForm = new ChangeValue();

                queryForm.OldValue = dgv.CurrentCell.Value.ToString();
                queryForm.NewValue = "";
                queryForm.ChangeMode = "value";
                queryForm.ShowDialog();




                if (queryForm.NewValue != "")
                {
                    //if (decimal.Parse(queryForm.NewValue) <= 0)
                    //{
                    //    MessageBox.Show("领料数量不能为负数...", "注意");
                    //    return;
                    //}
                    dgv.CurrentRow.Cells["领料数量"].Value = queryForm.NewValue;
                    //dgv.CurrentRow.Cells["discount_money"].Value = DBNull.Value;

                    //dgv.CurrentRow.Cells["approve_flag"].Value = "False";
                    SaveChanged();

                 
                   // CountPlanStru();

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

            if ("新旧" == dgv.CurrentCell.OwningColumn.Name)
            {

                

                    //////////////////////////

                string sel = "SELECT oldnew as 新旧 FROM ly_finishproduct_newold  ";
                QueryForm queryForm = new QueryForm();


                queryForm.Sel = sel;
                queryForm.Constr = SQLDatabase.Connectstring;

                //Set the Column Collection to the filter Table
                //queryForm.SetSourceColumns(this.billMainDataSet.BalanceBill.Columns);

                queryForm.ShowDialog();

                 
                ///////////////////////


                if (queryForm.Result != "")
                {
                    dgv.CurrentRow.Cells["新旧"].Value = queryForm.Result;
                    //dgv.CurrentRow.Cells["discount_money"].Value = DBNull.Value;

                    //dgv.CurrentRow.Cells["approve_flag"].Value = "False";
                    SaveChanged();


                    // CountPlanStru();

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







            if ("部门名称" == dgv.CurrentCell.OwningColumn.Name)
            {
                //ChangeValue queryForm = new ChangeValue();

                //queryForm.OldValue = dgv.CurrentCell.Value.ToString();
                //queryForm.NewValue = "";
                //queryForm.ShowDialog();




                //if (queryForm.NewValue != "")
                //{
                //    dgv.CurrentRow.Cells["部门名称"].Value = queryForm.NewValue;
                //    //dgv.CurrentRow.Cells["discount_money"].Value = DBNull.Value;

                //    //dgv.CurrentRow.Cells["approve_flag"].Value = "False";
                //    SaveChanged();

                //}
                //else
                //{


                //}
                //return;

                //////////////////
                return;
                string sel = "SELECT a.prodname as 编码,a.prodcode as 名称 FROM ly_prod_dept a ";


                QueryForm queryForm = new QueryForm();


                queryForm.Sel = sel;
                queryForm.Constr = SQLDatabase.Connectstring;



                queryForm.ShowDialog();


                

                dgv.CurrentRow.Cells["部门名称"].Value = queryForm.Result; 
                dgv.CurrentRow.Cells["领料部门"].Value = queryForm.Result1 ;
                SaveChanged();



                return;
            }
        }

        private void 计划物料计算ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (null == this.ly_material_plan_mainDataGridView.CurrentRow) return;

            if (this.formState != "View") return ;
            
                
                    int parentId = int.Parse(this.ly_material_plan_mainDataGridView.CurrentRow.Cells["id"].Value.ToString());
                    string planNum = this.ly_material_plan_mainDataGridView.CurrentRow.Cells["计划编号"].Value.ToString();

                  
                
           

            //////////////////////////////////
            
            string message = "计算物料计划物料需求吗？";
            string caption = "提示...";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;

            DialogResult result;



            result = MessageBox.Show(message, caption, buttons,
            MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

            if (result == DialogResult.Yes)
            {
                CountPlanStru();


                   



              

            }
        }

        private void CountPlanStru()
        {
            if (null == this.ly_material_plan_mainDataGridView.CurrentRow) return;

            if ("True" == this.ly_material_plan_mainDataGridView.CurrentRow.Cells["启用"].Value.ToString())
            {
                MessageBox.Show("领料计划已经审批,不能增加条目...", "注意");
                return;
            }
           
           
            string planNum = this.ly_material_plan_mainDataGridView.CurrentRow.Cells["计划编号"].Value.ToString();

            string componentNum = this.lY_dayget_material_selDataGridView.CurrentRow.Cells["物资编号"].Value.ToString();
            int parentId = int.Parse(this.ly_material_plan_mainDataGridView.CurrentRow.Cells["id"].Value.ToString());
            string prod_dept = SQLDatabase.nowUserDepartmentBig();
            
            frmWaiting.Show(this);
            
            SqlConnection sqlConnection1 = new SqlConnection(SQLDatabase.Connectstring);
            SqlCommand cmd = new SqlCommand();



            cmd.Parameters.Add("@planId", SqlDbType.Int);
            cmd.Parameters["@planId"].Value = parentId;

            cmd.Parameters.Add("@prod_dept", SqlDbType.VarChar );
            cmd.Parameters["@prod_dept"].Value = prod_dept;

            cmd.Parameters.Add("@itemno", SqlDbType.VarChar );
            cmd.Parameters["@itemno"].Value = componentNum;


            cmd.CommandText = "LY_Dayget_input";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = sqlConnection1;

            sqlConnection1.Open();
            cmd.ExecuteNonQuery();
            sqlConnection1.Close();



            this.lY_dayget_material_sel_newTableAdapter.Fill(this.lYPlanMange.LY_dayget_material_sel_new, parentId, SQLDatabase.nowUserDepartment());
            this.ly_plan_getmaterialPlanTableAdapter.Fill(this.lYPlanMange.ly_plan_getmaterialPlan,planNum );

            frmWaiting.Hide(this);
        }

       

        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            //if (null == this.ly_store_planitemcountDataGridView1.CurrentRow) return;



            ////string balanceFlag = this.xD_Sel_SellBalanceDataGridView.CurrentRow.Cells["结算"].Value.ToString();



            //frmWaiting.Show(this);


            //BaseReportView queryForm = new BaseReportView();

            //queryForm.Text = "中原精密生产计划欠料表";

            //queryForm.Printdata = this.lYPlanMange;

            //queryForm.PrintCrystalReport = new LY_ProductReport_Owe();

            ////string nowCardNumber = this.xD_Sel_SellBalanceDataGridView.CurrentRow.Cells["编号"].Value.ToString();

            ////string selectFormula;
            ////selectFormula = "{XD_Sel_SellBalance.编号}   =   '" + nowCardNumber + "'";
            ////queryForm.CrystalReportViewer1.SelectionFormula = selectFormula;

            ////if (this.radioButton2.Checked)
            ////{
            ////    string selectFormula;
            ////    selectFormula = "{XD_Sel_SellBalance.Card_number}   =   '" + nowCardNumber + "'";
            ////    queryForm.PrintCrystalReport.DataDefinition.RecordSelectionFormula = selectFormula;
            ////}

            //string selectFormula;
            ////selectFormula = "{ly_store_planitemcount.status}  =   '原料' and {ly_store_planitemcount.owemoney} >0 ";
            //selectFormula = "{ly_store_planitemcount.状态}  =   '原料'  and {ly_store_planitemcount.欠料金额}>0 ";
            //queryForm.PrintCrystalReport.DataDefinition.RecordSelectionFormula = selectFormula;

            ////((CrystalDecisions.CrystalReports.Engine.TextObject)(queryForm.PrintCrystalReport.DataDefinition.GroupNameFields.GroupHeaderSection1.ReportObjects["Text24"])).Text = "潼关中金冶炼有限责任公司原料结算单";


            ////queryForm.PrintCrystalReport = new XD_SellBalance_All();

            ////string nowCardNumber = this.xD_Sel_SellBalanceDataGridView.CurrentRow.Cells["编号"].Value.ToString();

            //frmWaiting.Hide(this);

            //queryForm.ShowDialog();
        }

        private void toolStripButton7_Click(object sender, EventArgs e)
        {
            if (null == this.ly_plan_getmaterialDataGridView.CurrentRow) return;

            int parentId = int.Parse(this.ly_material_plan_mainDataGridView.CurrentRow.Cells["id"].Value.ToString());





            frmWaiting.Show(this);

            //this.ly_plan_getmaterialTableAdapter.Fill(this.lYPlanMange.ly_plan_getmaterial, parentId);

            BaseReportView queryForm = new BaseReportView();

            queryForm.Text = "中原精密计划领料单";

            queryForm.Printdata = this.lYPlanMange;

            queryForm.PrintCrystalReport = new LY_GetMaterial_Dayget();


            //string selectFormula;

            //selectFormula = "{ly_store_planitemcount.状态}  =   '原料'  and {ly_store_planitemcount.欠料金额}>0 ";
            //queryForm.PrintCrystalReport.DataDefinition.RecordSelectionFormula = selectFormula;

            frmWaiting.Hide(this);

            queryForm.ShowDialog();
        }

        //private void toolStripButton8_Click(object sender, EventArgs e)
        //{
        //    ExportDataGridviewTOExcell.ExportDataGridview(this.ly_material_plan_explodeDataGridView, true);
        //}

        private void toolStripButton9_Click(object sender, EventArgs e)
        {
            ExportDataGridviewTOExcell.ExportDataGridview(this.ly_plan_getmaterialDataGridView, true);
        }

        private void lY_dayget_material_selDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button8_Click(object sender, EventArgs e)
        {

            if (Check_ifApproved(this.ly_material_plan_mainDataGridView.CurrentRow.Cells["计划编号"].Value.ToString()))
            {

                MessageBox.Show("计划已经批准,不能改动...", "注意");
                return;
            }

            string sel ;
             if ("000301" != SQLDatabase.nowUserDepartment())
             {

                 sel = "SELECT distinct a.salesclient_name as 客户,a.salesclient_code as 编码,a.salesperson_code as 营业员代码,b.yhmc as 营业员,a.salesclient_py as py,a.salesclient_jp as jp FROM ly_sales_client a left join T_users AS b ON a.salesperson_code = b.yhbm order by a.salesclient_code";
            
             }
             else
             {

                 sel = "SELECT distinct a.salesclient_name as 客户,a.salesclient_code as 编码,a.salesperson_code as 营业员代码,b.yhmc as 营业员,a.salesclient_py as py,a.salesclient_jp as jp FROM ly_sales_client a left join T_users AS b ON a.salesperson_code = b.yhbm where salesperson_code='"+ SQLDatabase .NowUserID +"' order by a.salesclient_code";
            
             }

            if (SQLDatabase.CheckHaveRight(SQLDatabase.NowUserID, "营业区域信息"))
            {

                sel = "SELECT distinct a.salesclient_name as 客户,a.salesclient_code as 编码,a.salesperson_code as 营业员代码,b.yhmc as 营业员,a.salesclient_py as py,a.salesclient_jp as jp FROM ly_sales_client a left join T_users AS b ON a.salesperson_code = b.yhbm where salesregion_code='" + SQLDatabase.nowSalesregioncode() + "' order by a.salesclient_code";


            }
            QueryForm queryForm = new QueryForm();


            queryForm.Sel = sel;
            queryForm.Constr = SQLDatabase.Connectstring;
            queryForm.Nodiscol = 4;
            //Set the Column Collection to the filter Table
            //queryForm.SetSourceColumns(this.billMainDataSet.BalanceBill.Columns);

            queryForm.ShowDialog();

            this.客户名称TextBox.Text = queryForm.Result;
            this.客户编码TextBox.Text = queryForm.Result1;
            this.营业员码TextBox.Text = queryForm.Result2;
            this.营业员TextBox.Text = queryForm.Result3;
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

            //if (this.ly_sales_contract_mainDataGridView.CurrentRow.Cells["录入人c"].Value.ToString() != SQLDatabase.nowUserName())
            //{

            //    MessageBox.Show("请" + this.ly_sales_contract_mainDataGridView.CurrentRow.Cells["录入人c"].Value.ToString() + "修改...", "注意");

            //    return;
            //}


            if ("启用" == dgv.CurrentCell.OwningColumn.Name)
            {

                //if ("True" == dgv.CurrentRow.Cells["批准"].Value.ToString())
                //{
                //    MessageBox.Show("合同文本已经提交,不能修改交付日期...", "注意");

                //    return;
                //}

                //ChangeValue queryForm = new ChangeValue();

                //queryForm.OldValue = dgv.CurrentCell.Value.ToString();
                //queryForm.NewValue = "";
                //queryForm.ChangeMode = "datetime";
                //queryForm.ShowDialog();


                //if (queryForm.NewValue != "")
                //{
                //    dgv.CurrentRow.Cells["合同文本交付"].Value = queryForm.NewValue;

                //}
                //else
                //{

                //    dgv.CurrentRow.Cells["合同文本交付"].Value = DBNull.Value;

                //}

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

       

        //private void fillToolStripButton_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        this.lY_dayget_material_sel_newTableAdapter.Fill(this.lYPlanMange.LY_dayget_material_sel_new, new System.Nullable<int>(((int)(System.Convert.ChangeType(plan_idToolStripTextBox.Text, typeof(int))))), employe_deptToolStripTextBox.Text);
        //    }
        //    catch (System.Exception ex)
        //    {
        //        System.Windows.Forms.MessageBox.Show(ex.Message);
        //    }

        //}


        //{
        //    try
        //    {
        //        this.ly_plan_getmaterialTableAdapter.Fill(this.lYPlanMange.ly_plan_getmaterial, new System.Nullable<int>(((int)(System.Convert.ChangeType(plan_idToolStripTextBox.Text, typeof(int))))));
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
        //        this.ly_material_plan_explodeTableAdapter.Fill(this.lYPlanMange.ly_material_plan_explode, new System.Nullable<int>(((int)(System.Convert.ChangeType(plan_idToolStripTextBox.Text, typeof(int))))));
        //    }
        //    catch (System.Exception ex)
        //    {
        //        System.Windows.Forms.MessageBox.Show(ex.Message);
        //    }

        //}






    }
}
