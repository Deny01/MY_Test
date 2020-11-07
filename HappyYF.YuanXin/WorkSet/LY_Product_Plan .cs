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
using KReport;
using System.Threading;
using DataGridFilter;



namespace HappyYF.YuanXin.WorkSet
{
    public partial class LY_Product_Plan  : Form
    {
         string formState = "View";
         int usetime = 1;
         public LY_Product_Plan()
        {
            InitializeComponent();
        }

         List<int> itemlist = new List<int>();

        private void ly_material_plan_mainBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            if (null == ly_material_plan_mainDataGridView.CurrentRow) return;

            string id = this.ly_material_plan_mainDataGridView.CurrentRow.Cells["id"].Value.ToString();

            this.Validate();
            this.ly_material_plan_mainBindingSource.EndEdit();
            this.ly_material_plan_mainTableAdapter.Update(this.lYPlanMange.ly_material_plan_main);


            this.ly_material_plan_mainTableAdapter.Fill(this.lYPlanMange.ly_material_plan_main, "SCJH");
            ly_material_plan_mainBindingSource.Position = ly_material_plan_mainBindingSource.Find("id", id);
            SetFormState("View");

        }

        private void LY_Material_Plan_Load(object sender, EventArgs e)
        {
            this.lY_MaterielRequirementspurDataGridView.Columns["idpur"].Visible = false;
            this.lY_MaterielRequirementsMashineDataGridView.Columns[0].Visible = false;

            this.ly_plan_getmaterial_departmentTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            this.ly_store_planitemcountTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            this.ly_store_planitemcountTableAdapter.CommandTimeout = 0;
            this.ly_material_plan_detailTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            this.ly_inma0010_planselTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            this.ly_material_plan_explodeTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;

            this.lY_MaterielRequirementsTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;

            this.ly_material_plan_mainTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            this.ly_material_plan_mainTableAdapter.Fill(this.lYPlanMange.ly_material_plan_main,"SCJH");



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
                启用备料CheckBox.Enabled = false;
                备料计算CheckBox.Enabled = false;
                年份TextBox.ReadOnly = true;
                月份ComboBox.Enabled = false;


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


                ly_material_plan_mainDataGridView.Enabled = true;
                this.button1.Enabled = true;


            }
            else
            {
                this.formState = "Edit";

                this.制定日期DateTimePicker.Enabled = true ;

                this.说明TextBox.ReadOnly = false ;
                this.启用CheckBox.Enabled  = true ;
                this.完成CheckBox.Enabled  = true;
                启用备料CheckBox.Enabled = true ;
                备料计算CheckBox.Enabled = true ;

                年份TextBox.ReadOnly = false ;
                月份ComboBox.Enabled = true ;


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


                ly_material_plan_mainDataGridView.Enabled = false ;
                this.button1.Enabled = false ;


            }


        }

        private string GetMaxPlanCode()
        {
            SqlConnection sqlConnection1 = new SqlConnection(SQLDatabase.Connectstring);
            SqlCommand cmd = new SqlCommand();

            string MaxPlanCode = "";

            cmd.Parameters.Add("@Plan_mode", SqlDbType.VarChar);
            cmd.Parameters["@Plan_mode"].Value = "SCJH";


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
                this.ly_material_plan_mainDataGridView.CurrentRow.Cells["制定日期"].Value = SQLDatabase.GetNowdate();
                this.ly_material_plan_mainDataGridView.CurrentRow.Cells["制定人"].Value = SQLDatabase.nowUserName();
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

            string planNum = this.ly_material_plan_mainDataGridView.CurrentRow.Cells["计划编号"].Value.ToString();

            if (Check_ifApproved(planNum))
            {

                MessageBox.Show("计划已经批准,不能修改数据...", "注意");
                return;
            }

            if (this.ly_material_plan_detailDataGridView.RowCount > 0)
            {
                MessageBox.Show("计划已有物料记录，不能删除(实需删除，请先删除该计划的物料记录)", "注意");
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


                    this.ly_material_plan_mainTableAdapter.Fill(this.lYPlanMange.ly_material_plan_main, "SCJH");
                }


            }
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            if (null == ly_material_plan_mainDataGridView.CurrentRow) return;

             string planNum = this.ly_material_plan_mainDataGridView.CurrentRow.Cells["计划编号"].Value.ToString();

             if (Check_ifApproved(planNum))
             {

                 MessageBox.Show("计划已经批准,不能修改数据...", "注意");
                 return;
             }

            SetFormState("Edit");
        }

       
        private void toolStripTextBox2_KeyUp(object sender, KeyEventArgs e)
        {
            //string dFilter = "";

            ////for (int i = 0; i < this.hT_Vehicle_collectDataGridView.ColumnCount; i++)
            //for (int i = 1; i < 11; i++)
            //{
            //    string tempColumnName = this.ly_inma0010_planselDataGridView.Columns[i].DataPropertyName;


            //    if (i != 10)
            //    {
            //        if (i!=3)
                    
            //            dFilter = dFilter + tempColumnName + " like  '*" + this.toolStripTextBox2.Text + "*' or ";
            //    }
            //    else
            //        dFilter = dFilter + tempColumnName + " like  '*" + this.toolStripTextBox2.Text + "*' ";

            //}

            //if (this.toolStripTextBox2.Text.Replace(" ", "").Length > 0)

            //    this.ly_inma0010_planselBindingSource.Filter = dFilter;
            //else
            //    this.ly_inma0010_planselBindingSource.Filter = " ";

            string filterString;


            filterString = GetDataGridviewMultiFilterString.DGVMultiFilterString(this.ly_inma0010_planselDataGridView, this.toolStripTextBox2.Text);


            this.ly_inma0010_planselBindingSource.Filter = filterString;
        }

        private void toolStripTextBox2_Enter(object sender, EventArgs e)
        {
            toolStripTextBox2.Text = "";

            this.ly_inma0010_planselBindingSource.Filter = "";
        }

        private void ly_material_plan_mainDataGridView_SelectionChanged(object sender, EventArgs e)
        {
            //if (this.formState == "View")
            //{
            //    if (null != this.ly_material_plan_mainDataGridView.CurrentRow)
            //    {
            //        int parentId = int.Parse(this.ly_material_plan_mainDataGridView.CurrentRow.Cells["id"].Value.ToString());
            //        string planNum = this.ly_material_plan_mainDataGridView.CurrentRow.Cells["计划编号"].Value.ToString();

            //        this.ly_material_plan_detailTableAdapter.Fill(this.lYPlanMange.ly_material_plan_detail, parentId);
            //        this.ly_inma0010_planselTableAdapter .Fill(this.lYPlanMange.ly_inma0010_plansel, parentId);
            //        ly_material_plan_explodeTableAdapter.Fill(this.lYPlanMange.ly_material_plan_explode, parentId);
            //        this.ly_store_planitemcountTableAdapter.Fill(this.lYPlanMange.ly_store_planitemcount, parentId);
            //        this.ly_plan_getmaterial_departmentTableAdapter.Fill(this.lYPlanMange.ly_plan_getmaterial_department, planNum);


            //        this.groupBox3.Text = planNum +":物料列表";

                   
            //    }
            //}
            //else
            //{
            //    // this.yX_taocan_mainBindingSource.Position = this.nowRow;
            //}
        }

        private void ly_inma0010_planselDataGridView_DoubleClick(object sender, EventArgs e)
        {
            if (null == this.ly_material_plan_mainDataGridView.CurrentRow) return;
            if (null == ly_inma0010_planselDataGridView.CurrentRow) return;

            string planNum = this.ly_material_plan_mainDataGridView.CurrentRow.Cells["计划编号"].Value.ToString();

            if (Check_ifApproved(planNum))
            {

                MessageBox.Show("计划已经批准,不能修改数据...", "注意");
                return;
            }

            string isAlertplan = this.ly_material_plan_mainDataGridView.CurrentRow.Cells["is_alertPlan"].Value.ToString();

            if ("True" == isAlertplan)
            {
                MessageBox.Show("警戒计划不用增加成品,直接结算需求即可", "提示");
                return ;
            
            }

            string componentNum = this.ly_inma0010_planselDataGridView.CurrentRow.Cells["物资编号"].Value.ToString();
            int parentId = int.Parse(this.ly_material_plan_mainDataGridView.CurrentRow.Cells["id"].Value.ToString());

            decimal  nowabsqty = 0;
            if (decimal.TryParse(this.ly_inma0010_planselDataGridView.CurrentRow.Cells["配套数"].Value.ToString(), out nowabsqty))
            {

            }
            else
            {
                nowabsqty = 0;
            }








            InserPlanDetail(componentNum, parentId, nowabsqty);

            //CountPlanStru();

            this.ly_material_plan_detailTableAdapter.Fill(this.lYPlanMange.ly_material_plan_detail, parentId);
            this.ly_inma0010_planselTableAdapter.Fill(this.lYPlanMange.ly_inma0010_plansel, parentId);

            this.ly_material_plan_detailBindingSource.Position = this.ly_material_plan_detailBindingSource.Find("编号", componentNum);

            
        }

        private static void InserPlanDetail(string componentNum, int parentId, decimal nowabsqty)
        {
            string insStr = " INSERT INTO ly_material_plan_detail  " +
           "( plan_id,wzbh,plan_count) " +
           " values ('" + parentId + "','" + componentNum + "'," + nowabsqty + ")";


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

        private void 删除子件ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (null == this.ly_material_plan_detailDataGridView.CurrentRow) return;


            int nowId = int.Parse(this.ly_material_plan_detailDataGridView.CurrentRow.Cells["id1"].Value.ToString());
            int parentId = int.Parse(this.ly_material_plan_mainDataGridView.CurrentRow.Cells["id"].Value.ToString());
            string componentNum = this.ly_material_plan_detailDataGridView.CurrentRow.Cells["物料编号"].Value.ToString();


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
                    this.ly_inma0010_planselTableAdapter.Fill(this.lYPlanMange.ly_inma0010_plansel, parentId);

                    this.ly_inma0010_planselBindingSource.Position = this.ly_inma0010_planselBindingSource.Find("物资编号", componentNum);

                    //CountPlanStru();
                }


            }
        }

        private void SaveChanged()
        {
            ///////////////////////////

            this.ly_material_plan_detailDataGridView.EndEdit();


            this.ly_material_plan_detailBindingSource.EndEdit();



            this.ly_material_plan_detailTableAdapter.Update(this.lYPlanMange.ly_material_plan_detail);



        }


        private void ly_material_plan_detailDataGridView_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {

            string planNum = this.ly_material_plan_mainDataGridView.CurrentRow.Cells["计划编号"].Value.ToString();

            if (Check_ifApproved(planNum))
            {

                MessageBox.Show("计划已经批准,不能修改数据...", "注意");
                return;
            }
            
            DataGridView dgv = sender as DataGridView;




            if ("数量" == dgv.CurrentCell.OwningColumn.Name)
            {

                ChangeValue queryForm = new ChangeValue();

                queryForm.OldValue = dgv.CurrentCell.Value.ToString();
                queryForm.NewValue = "";
                queryForm.ChangeMode = "value";
                queryForm.ShowDialog();




                if (queryForm.NewValue != "")
                {
                    dgv.CurrentRow.Cells["数量"].Value = queryForm.NewValue;
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


            /////////////////////////////////////////////////////


            //if ("种类" == dgv.CurrentCell.OwningColumn.Name)
            //{
            //    ChangeValue queryForm = new ChangeValue();

            //    queryForm.OldValue = dgv.CurrentCell.Value.ToString();
            //    queryForm.NewValue = "";
            //    queryForm.ShowDialog();




            //    if (queryForm.NewValue != "")
            //    {
            //        dgv.CurrentRow.Cells["种类"].Value = queryForm.NewValue;
            //        //dgv.CurrentRow.Cells["discount_money"].Value = DBNull.Value;

            //        //dgv.CurrentRow.Cells["approve_flag"].Value = "False";
            //        SaveChanged();

            //    }
            //    else
            //    {


            //    }
            //    return;
            //}


            if ("种类" == dgv.CurrentCell.OwningColumn.Name)
            {
                //


                if (SQLDatabase.CheckHaveRight(SQLDatabase.NowUserID, "物料来源设置"))
                {

                }
                else
                {
                    MessageBox.Show("无物料种类设置权限", "注意");
                    return;
                }




                string s = dgv.CurrentRow.Cells["物料编号"].Value.ToString();

                string sel = "SELECT distinct sortcode as 代码,sortname as 种类 FROM ly_materrial_sort  order by sortcode";


                QueryForm queryForm = new QueryForm();


                queryForm.Sel = sel;
                queryForm.Constr = SQLDatabase.Connectstring; 

                queryForm.ShowDialog();




                if (queryForm.Result != "")
                {
                    dgv.CurrentRow.Cells["种类"].Value = queryForm.Result1;
                    //dgv.CurrentRow.Cells["discount_money"].Value = DBNull.Value;

                    //dgv.CurrentRow.Cells["approve_flag"].Value = "False";
                    SaveDetailsort(s, queryForm.Result);


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
          
            ///////////////////////////////////////////////////////////

            if ("备注" == dgv.CurrentCell.OwningColumn.Name)
            {
                ChangeValue queryForm = new ChangeValue();

                queryForm.OldValue = dgv.CurrentCell.Value.ToString();
                queryForm.NewValue = "";
                queryForm.ShowDialog();




                if (queryForm.NewValue != "")
                {
                    dgv.CurrentRow.Cells["备注"].Value = queryForm.NewValue; 
                    SaveChanged();

                }
                else
                {
                   

                }
                return;
            }
        }

        private void SaveDetailsort(string nowitemno, string nowsortvalue)
        {

            ///////////////////

            string updstr = " update ly_inma0010  " +
                           "  set sort1=  '" + nowsortvalue
                           + "' where  wzbh='" + nowitemno + "'";


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




            ////////////////////////////

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

            if (this.formState != "View") return ;
            
                
                    //int parentId = int.Parse(this.ly_material_plan_mainDataGridView.CurrentRow.Cells["id"].Value.ToString());
                    //string planNum = this.ly_material_plan_mainDataGridView.CurrentRow.Cells["计划编号"].Value.ToString();

                    string isAlertplan = this.ly_material_plan_mainDataGridView.CurrentRow.Cells["is_alertPlan"].Value.ToString();
                
           

            //////////////////////////////////
            
            string message = "计算物料计划物料需求吗？";
            string caption = "提示...";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;

            DialogResult result;



            result = MessageBox.Show(message, caption, buttons,
            MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

            if (result == DialogResult.Yes)
            {

                if ("True" == isAlertplan)
                {
                    CountAlertPlanStru();
                }
                else
                {
                    CountPlanStru();
                }


                   



              

            }
        }

        private void CountPlanStru()
        {
            if (null == this.ly_material_plan_mainDataGridView.CurrentRow) return;

            int parentId = int.Parse(this.ly_material_plan_mainDataGridView.CurrentRow.Cells["id"].Value.ToString());
            string planNum = this.ly_material_plan_mainDataGridView.CurrentRow.Cells["计划编号"].Value.ToString();

            NewFrm.Show(this);
            Thread.Sleep(500);

            //2018 -04-03
            
            SqlConnection sqlConnection1 = new SqlConnection(SQLDatabase.Connectstring);
            SqlCommand cmd = new SqlCommand();



            cmd.Parameters.Add("@planId", SqlDbType.Int);
            cmd.Parameters["@planId"].Value = parentId;


            cmd.CommandText = "LY_PlanExplode";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = sqlConnection1;
            cmd.CommandTimeout = 0;

            sqlConnection1.Open();
            cmd.ExecuteNonQuery();
            sqlConnection1.Close();


            //ly_material_plan_explodeTableAdapter.Fill(this.lYPlanMange.ly_material_plan_explode, parentId);
            //this.ly_store_planitemcountTableAdapter.Fill(this.lYPlanMange.ly_store_planitemcount, parentId);
            //this.ly_plan_getmaterial_departmentTableAdapter.Fill(this.lYPlanMange.ly_plan_getmaterial_department, planNum);

            NewFrm.Hide(this);
        }

        private void CountAlertPlanStru()
        {
            if (null == this.ly_material_plan_mainDataGridView.CurrentRow) return;

            int parentId = int.Parse(this.ly_material_plan_mainDataGridView.CurrentRow.Cells["id"].Value.ToString());
            string planNum = this.ly_material_plan_mainDataGridView.CurrentRow.Cells["计划编号"].Value.ToString();

            NewFrm.Show(this);
            Thread.Sleep(500);

            SqlConnection sqlConnection1 = new SqlConnection(SQLDatabase.Connectstring);
            SqlCommand cmd = new SqlCommand();



            cmd.Parameters.Add("@planId", SqlDbType.Int);
            cmd.Parameters["@planId"].Value = parentId;


            cmd.CommandText = "LY_CountAlert_Plan";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = sqlConnection1;
            cmd.CommandTimeout = 0;

            sqlConnection1.Open();
            cmd.ExecuteNonQuery();
            sqlConnection1.Close();


            //ly_material_plan_explodeTableAdapter.Fill(this.lYPlanMange.ly_material_plan_explode, parentId);
            //this.ly_store_planitemcountTableAdapter.Fill(this.lYPlanMange.ly_store_planitemcount, parentId);
            //this.ly_plan_getmaterial_departmentTableAdapter.Fill(this.lYPlanMange.ly_plan_getmaterial_department, planNum);

            NewFrm.Hide(this);
        }

        private void CountAlertPlanStruSingle( string itemnum)
        {
            if (null == this.ly_material_plan_mainDataGridView.CurrentRow) return;
            if (string.IsNullOrEmpty(itemnum)) return;

            int parentId = int.Parse(this.ly_material_plan_mainDataGridView.CurrentRow.Cells["id"].Value.ToString());
            string planNum = this.ly_material_plan_mainDataGridView.CurrentRow.Cells["计划编号"].Value.ToString();

            //NewFrm.Show(this); 2018-04-03

            SqlConnection sqlConnection1 = new SqlConnection(SQLDatabase.Connectstring);
            SqlCommand cmd = new SqlCommand();



            cmd.Parameters.Add("@planId", SqlDbType.Int);
            cmd.Parameters["@planId"].Value = parentId;

            cmd.Parameters.Add("@itemNum", SqlDbType.VarChar);
            cmd.Parameters["@itemNum"].Value = itemnum;


            cmd.CommandText = "LY_CountAlert_Plan_single";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = sqlConnection1;
            cmd.CommandTimeout = 0;

            sqlConnection1.Open();
            cmd.ExecuteNonQuery();
            sqlConnection1.Close();

            

            //ly_material_plan_explodeTableAdapter.Fill(this.lYPlanMange.ly_material_plan_explode, parentId);
            //this.ly_store_planitemcountTableAdapter.Fill(this.lYPlanMange.ly_store_planitemcount, parentId);
            //this.ly_plan_getmaterial_departmentTableAdapter.Fill(this.lYPlanMange.ly_plan_getmaterial_department, planNum);

            //NewFrm.Hide(this);2018-04-03
        }

        private void 打印PToolStripButton_Click(object sender, EventArgs e)
        {
            if (null == this.ly_store_planitemcountDataGridView1.CurrentRow) return;



            //string balanceFlag = this.xD_Sel_SellBalanceDataGridView.CurrentRow.Cells["结算"].Value.ToString();



            //NewFrm.Show(this); 2018-04-03


            BaseReportView queryForm = new BaseReportView();

            queryForm.Text = "中原精密生产计划需求表";

            queryForm.Printdata = this.lYPlanMange;

            queryForm.PrintCrystalReport = new LY_ProductReport();

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

            //NewFrm.Hide(this);2018-04-03

            queryForm.ShowDialog();
        }

        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            if (null == this.ly_store_planitemcountDataGridView1.CurrentRow) return;



            //string balanceFlag = this.xD_Sel_SellBalanceDataGridView.CurrentRow.Cells["结算"].Value.ToString();



            //NewFrm.Show(this); 2018-04-03


            BaseReportView queryForm = new BaseReportView();

            queryForm.Text = "中原精密生产计划欠料表";

            queryForm.Printdata = this.lYPlanMange;

            queryForm.PrintCrystalReport = new LY_ProductReport_Owe();

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

            string selectFormula;
            //selectFormula = "{ly_store_planitemcount.status}  =   '原料' and {ly_store_planitemcount.owemoney} >0 ";
            //selectFormula = "{ly_store_planitemcount.状态}  =   '原料'  and {ly_store_planitemcount.计划欠料金额}>0 ";

            selectFormula = " {ly_store_planitemcount.计划欠料}>0 ";
            queryForm.PrintCrystalReport.DataDefinition.RecordSelectionFormula = selectFormula;

            //((CrystalDecisions.CrystalReports.Engine.TextObject)(queryForm.PrintCrystalReport.DataDefinition.GroupNameFields.GroupHeaderSection1.ReportObjects["Text24"])).Text = "潼关中金冶炼有限责任公司原料结算单";


            //queryForm.PrintCrystalReport = new XD_SellBalance_All();

            //string nowCardNumber = this.xD_Sel_SellBalanceDataGridView.CurrentRow.Cells["编号"].Value.ToString();

            //NewFrm.Hide(this);2018-04-03

            queryForm.ShowDialog();
        }

        private void toolStripButton7_Click(object sender, EventArgs e)
        {
            if (null == this.ly_material_plan_explodeDataGridView.CurrentRow) return;

            int parentId = int.Parse(this.ly_material_plan_mainDataGridView.CurrentRow.Cells["id"].Value.ToString());





            //NewFrm.Show(this); 2018-04-03

            //this.ly_plan_getmaterialTableAdapter.Fill(this.lYPlanMange.ly_plan_getmaterial, parentId);

            BaseReportView queryForm = new BaseReportView();

            queryForm.Text = "中原精密生产领料计划";

            queryForm.Printdata = this.lYPlanMange;

            queryForm.PrintCrystalReport = new LY_GetMaterial();


            //string selectFormula;

            //selectFormula = "{ly_store_planitemcount.状态}  =   '原料'  and {ly_store_planitemcount.欠料金额}>0 ";
            //queryForm.PrintCrystalReport.DataDefinition.RecordSelectionFormula = selectFormula;

            //NewFrm.Hide(this);2018-04-03

            queryForm.ShowDialog();
        }

        private void toolStripButton8_Click(object sender, EventArgs e)
        {
            ExportDataGridviewTOExcell.ExportDataGridview(this.ly_material_plan_explodeDataGridView, true);
        }

        private void toolStripButton9_Click(object sender, EventArgs e)
        {
            ExportDataGridviewTOExcell.ExportDataGridview(this.ly_plan_getmaterialDataGridView, true);
        }

        private void toolStripButton11_Click(object sender, EventArgs e)
        {
            if (null == ly_plan_getmaterialDataGridView.CurrentRow)
            {
                return;
            }



            HappyReport hr = new HappyReport();

            DataTable repdDT = this.lYPlanMange.ly_plan_getmaterial_department.Copy();


            hr.ShowDesigner(repdDT, "生产领料计划打印", true);
        }

        private void toolStripButton7_Click_1(object sender, EventArgs e)
        {

            if (null == ly_plan_getmaterialDataGridView.CurrentRow)
            {
                return;
            }



            HappyReport hr = new HappyReport();

            DataTable repdDT = this.lYPlanMange.ly_plan_getmaterial_department.Copy();


            hr.Show(repdDT, "生产领料计划打印");
            
           
          
        }

        private void ly_inma0010_planselDataGridView_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (true == ly_inma0010_planselDataGridView.ReadOnly) return;
            if ("配套数" == ly_inma0010_planselDataGridView.CurrentCell.OwningColumn.DataPropertyName)
            {
                //SetlastColumn();
                ly_inma0010_planselDataGridView.CurrentCell.Style.BackColor = Color.White ;
                ly_inma0010_planselDataGridView.CurrentCell.Style.ForeColor = Color.Black  ;

              
            }
        }

        private void ly_inma0010_planselDataGridView_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
            if (true == ly_inma0010_planselDataGridView.ReadOnly) return;
            if ("配套数" == ly_inma0010_planselDataGridView.CurrentCell.OwningColumn.DataPropertyName)
            {





                ly_inma0010_planselDataGridView.CurrentCell.Style.BackColor = Color.SpringGreen;
                ly_inma0010_planselDataGridView.CurrentCell.Style.ForeColor = Color.Black;
            }
        }

        private void toolStripButton12_Click(object sender, EventArgs e)
        {
            if (null == this.ly_material_plan_mainDataGridView.CurrentRow) return;
            if (null == ly_inma0010_planselDataGridView.CurrentRow) return;

            string planNum = this.ly_material_plan_mainDataGridView.CurrentRow.Cells["计划编号"].Value.ToString();

            if (Check_ifApproved(planNum))
            {

                MessageBox.Show("计划已经批准,不能修改数据...", "注意");
                return;
            }

            string isAlertplan = this.ly_material_plan_mainDataGridView.CurrentRow.Cells["is_alertPlan"].Value.ToString();

            if ("True" == isAlertplan)
            {
                MessageBox.Show("警戒计划不用增加成品,直接结算需求即可", "提示");
                return;

            }

            this.toolStripButton12.Enabled = false;
            //string componentNum = this.ly_inma0010_planselDataGridView.CurrentRow.Cells["物资编号"].Value.ToString();
            int parentId = int.Parse(this.ly_material_plan_mainDataGridView.CurrentRow.Cells["id"].Value.ToString());


            ly_inma0010_planselDataGridView.EndEdit();





            //frmWaiting.Show(this);

            string componentNum;
            decimal nowabsqty;
            //decimal nowtyl;
            foreach (DataGridViewRow dgv in ly_inma0010_planselDataGridView.Rows)
            {
                nowabsqty = 0;
                if (decimal.TryParse(dgv.Cells["配套数"].Value.ToString(), out nowabsqty))
                {
                    //if (0 < nowabsqty)
                    //{
                    //    if (decimal.TryParse(dgv.Cells["台用量"].Value.ToString(), out nowtyl))
                    //    {

                    //    }
                    //    else
                    //    {

                    //        nowtyl = 0;


                    //    }
                    componentNum = dgv.Cells["物资编号"].Value.ToString();
                    if (0 < nowabsqty)
                    {
                        InserPlanDetail(componentNum, parentId, nowabsqty);
                    }


                    //}

                }


            }

            CountPlanStru();
            this.ly_material_plan_detailTableAdapter.Fill(this.lYPlanMange.ly_material_plan_detail, parentId);
            this.ly_inma0010_planselTableAdapter.Fill(this.lYPlanMange.ly_inma0010_plansel, parentId);

            this.toolStripButton12.Enabled = true ;

            //this.ly_inma0010_planselTableAdapter.Fill(this.lYPlanMange.ly_inma0010_plansel ,0);
            //string planNum = this.ly_material_plan_mainDataGridView.CurrentRow.Cells["计划编号"].Value.ToString();
            //this.ly_plan_getmaterialTableAdapter.Fill(this.lYPlanMange.ly_plan_getmaterial, planNum);
            //frmWaiting.Hide(this);
        }

        private void 说明TextBox_TextChanged(object sender, EventArgs e)
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

                        if (this.tabControl2.SelectedIndex == 0)
                        {
                            this.ly_material_plan_detailTableAdapter.Fill(this.lYPlanMange.ly_material_plan_detail, parentId);
                            this.ly_inma0010_planselTableAdapter.Fill(this.lYPlanMange.ly_inma0010_plansel, parentId);
                        }
                        else if (this.tabControl2.SelectedIndex == 1)
                        {
                            if (this.tabControl1.SelectedIndex == 0)
                            {
                                ly_material_plan_explodeTableAdapter.Fill(this.lYPlanMange.ly_material_plan_explode, parentId);
                            }
                            else if (this.tabControl1.SelectedIndex == 1)
                            {

                                this.ly_plan_getmaterial_departmentTableAdapter.Fill(this.lYPlanMange.ly_plan_getmaterial_department, planNum);
                            }
                            else if (this.tabControl1.SelectedIndex == 2)
                            {
                                // ly_material_plan_mainDataGridView.RowEnter -= this.ly_material_plan_mainDataGridView_RowEnter;

                                NewFrm.Show(this.ParentForm);
                                Thread.Sleep(500);
                                this.ly_store_planitemcountTableAdapter.Fill(this.lYPlanMange.ly_store_planitemcount, parentId);
                                NewFrm.Hide(this.ParentForm);
                                // ly_material_plan_mainDataGridView.RowEnter += this.ly_material_plan_mainDataGridView_RowEnter;


                            }
                            else if (this.tabControl1.SelectedIndex == 3)
                            {
                                this.lY_MaterielRequirementsTableAdapter.Fill(this.lYPlanMange.LY_MaterielRequirements, parentId, "外协","OWE");
                            }
                            else if (this.tabControl1.SelectedIndex == 4)
                            {
                                this.lY_MaterielRequirementsTableAdapter.Fill(this.lYPlanMange.LY_MaterielRequirements, parentId, "外购","OWE");
                            }
                            else if (this.tabControl1.SelectedIndex == 5)
                            {
                                this.lY_MaterielRequirementsTableAdapter.Fill(this.lYPlanMange.LY_MaterielRequirements, parentId, "机加","OWE");
                            }



                        }


                        this.groupBox3.Text = planNum + ":物料列表";


                    }
                }
                else
                {
                    // this.yX_taocan_mainBindingSource.Position = this.nowRow;
                }

                //this.Activate();
          
        }

        private void toolStripButton13_Click(object sender, EventArgs e)
        {
            if (null == this.ly_material_plan_mainDataGridView.CurrentRow) return;

            if (this.formState != "View") return;


            //int parentId = int.Parse(this.ly_material_plan_mainDataGridView.CurrentRow.Cells["id"].Value.ToString());
            //string planNum = this.ly_material_plan_mainDataGridView.CurrentRow.Cells["计划编号"].Value.ToString();

            string   nogetNum;
            string plannum;
            string inuse;

            //this.toolStripLabel3.
            //NewFrm.Show(this); 2018-04-03

            foreach (DataGridViewRow dgr in ly_material_plan_mainDataGridView.Rows)
            {
                plannum = dgr.Cells["计划编号"].Value.ToString();

                inuse = dgr.Cells["启用"].Value.ToString();

                //this.toolStripLabel3.Text = plannum;
                //this.toolStripLabel3.Invalidate();

                NewFrm.Notify(this, "正在检查计划:" + plannum + "未领物料");

                nogetNum = checknogetNum(plannum);

              //if ("SCJH0000233"==plannum)
              //{

              //    int ss = 0;
              //}

                if ("yes" == nogetNum && "True" == inuse)
                       
                        dgr.DefaultCellStyle.BackColor = Color.Cyan;
                    else
                        dgr.DefaultCellStyle.BackColor = Color.White;


                  


            }

            //NewFrm.Hide(this);2018-04-03

        }

        private string   checknogetNum(string nowPlan)
        {
           


            string  nogetnum="no";
            object oob;
           

            SqlConnection sqlConnection1 = new SqlConnection(SQLDatabase.Connectstring);
            SqlCommand cmd = new SqlCommand();



            cmd.Parameters.Add("@Plan_number", SqlDbType.VarChar);
            cmd.Parameters["@Plan_number"].Value = nowPlan;


            cmd.CommandText = "LY_GetPlanNoget";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = sqlConnection1;
            cmd.CommandTimeout = 0;

            //sqlConnection1.Open();
            //cmd.ExecuteNonQuery();
            //sqlConnection1.Close();

            sqlConnection1.Open();
            oob = cmd.ExecuteScalar();
            //nogetnum = cmd.ExecuteScalar().ToString();

            if( null !=oob )
            {
               nogetnum = oob.ToString();
            }
            
            sqlConnection1.Close();



            return nogetnum;



            
        
        }

        private void tabControl2_Selected(object sender, TabControlEventArgs e)
        {
            if (null == this.ly_material_plan_mainDataGridView.CurrentRow) return;
               
                    int parentId = int.Parse(this.ly_material_plan_mainDataGridView.CurrentRow .Cells["id"].Value.ToString());
                    string planNum = this.ly_material_plan_mainDataGridView.CurrentRow .Cells["计划编号"].Value.ToString();

            if (e.TabPageIndex == 0)
            {
                this.ly_material_plan_detailTableAdapter.Fill(this.lYPlanMange.ly_material_plan_detail, parentId);
                this.ly_inma0010_planselTableAdapter.Fill(this.lYPlanMange.ly_inma0010_plansel, parentId);
            }

           // MessageBox.Show(planNum);

        }

        private void tabControl1_Selected(object sender, TabControlEventArgs e)
        {
            if (null == this.ly_material_plan_mainDataGridView.CurrentRow) return;

            int parentId = int.Parse(this.ly_material_plan_mainDataGridView.CurrentRow .Cells["id"].Value.ToString());
            string planNum = this.ly_material_plan_mainDataGridView.CurrentRow .Cells["计划编号"].Value.ToString();
            
            if (e.TabPageIndex == 0)
            {
                ly_material_plan_explodeTableAdapter.Fill(this.lYPlanMange.ly_material_plan_explode, parentId);
            }
            else if (e.TabPageIndex == 1)
            {

                this.ly_plan_getmaterial_departmentTableAdapter.Fill(this.lYPlanMange.ly_plan_getmaterial_department, planNum);
            }
            else if (e.TabPageIndex == 2)
            {
                //NewFrm.Show(this); 2018-04-03
                this.ly_store_planitemcountTableAdapter.Fill(this.lYPlanMange.ly_store_planitemcount, parentId);

                //NewFrm.Hide(this);2018-04-03

               
              
                
            }
            else if (e.TabPageIndex == 3)
            {
                this.lY_MaterielRequirementsTableAdapter.Fill(this.lYPlanMange.LY_MaterielRequirements, parentId, "外协","OWE");

                foreach (DataGridViewColumn dvc in lY_MaterielRequirementsDataGridView.Columns)
                {
                    if ("采购未到" == dvc.DataPropertyName
                     || "外协未到" == dvc.DataPropertyName
                     || "机加在制" == dvc.DataPropertyName
                      || "缓急" == dvc.DataPropertyName)
                    {
                        dvc.ReadOnly = true ;
                        //dvc.DefaultCellStyle.BackColor = Color.Teal;
                        //dvc.DefaultCellStyle.ForeColor = Color.White;
                    }
                    else if ("欠料" == dvc.DataPropertyName)
                    {
                        dvc.ReadOnly = true ;
                        //dvc.DefaultCellStyle.BackColor = Color.White;
                        //dvc.DefaultCellStyle.ForeColor = Color.Red;
                    }
                   
                }
            }
            else if (e.TabPageIndex == 4)
            {
                this.lY_MaterielRequirementsTableAdapter.Fill(this.lYPlanMange.LY_MaterielRequirements, parentId, "外购","OWE");
                foreach (DataGridViewColumn dvc in lY_MaterielRequirementspurDataGridView.Columns)
                {
                    if ("采购未到" == dvc.DataPropertyName
                     || "外协未到" == dvc.DataPropertyName
                     || "机加在制" == dvc.DataPropertyName
                      || "缓急" == dvc.DataPropertyName)
                    {
                        dvc.ReadOnly = true;
                        //dvc.DefaultCellStyle.BackColor = Color.Teal;
                        //dvc.DefaultCellStyle.ForeColor = Color.White;
                    }
                    else if ("欠料" == dvc.DataPropertyName)
                    {
                        dvc.ReadOnly = true;
                        //dvc.DefaultCellStyle.BackColor = Color.White;
                        //dvc.DefaultCellStyle.ForeColor = Color.Red;
                    }

                }

                lY_MaterielRequirementspurDataGridView.Columns["idpur"].Visible = false;
            }
            else if (e.TabPageIndex == 5)
            {
                this.lY_MaterielRequirementsTableAdapter.Fill(this.lYPlanMange.LY_MaterielRequirements, parentId, "机加","OWE");
                foreach (DataGridViewColumn dvc in lY_MaterielRequirementsMashineDataGridView.Columns)
                {
                    if ("采购未到" == dvc.DataPropertyName
                     || "外协未到" == dvc.DataPropertyName
                     || "机加在制" == dvc.DataPropertyName
                       || "缓急" == dvc.DataPropertyName)
                    {
                        dvc.ReadOnly = true;
                        //dvc.DefaultCellStyle.BackColor = Color.Teal;
                        //dvc.DefaultCellStyle.ForeColor = Color.White;
                    }
                    else if ("欠料" == dvc.DataPropertyName)
                    {
                        dvc.ReadOnly = true;
                        //dvc.DefaultCellStyle.BackColor = Color.White;
                        //dvc.DefaultCellStyle.ForeColor = Color.Red;
                    }

                }
                lY_MaterielRequirementsMashineDataGridView.Columns[0].Visible = false;
            }



            

            //this.lY_MaterielRequirementsTableAdapter.Fill(this.lYPlanMange.LY_MaterielRequirements, parentId,"外协");
        }

        private void ly_plan_getmaterialDataGridView_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {

            decimal notget = 0;
            
            foreach (DataGridViewRow dgr in ly_plan_getmaterialDataGridView.Rows)
            {

                if (string.IsNullOrEmpty(dgr.Cells["未领数量"].Value.ToString())) continue;
                notget = decimal .Parse ( dgr.Cells["未领数量"].Value.ToString());

                if (0 < notget)
                {

                    //dgr.DefaultCellStyle.BackColor = Color.Red;
                    dgr.Cells["未领数量"].Style.BackColor = Color.Red;
                    dgr.Cells["未领数量"].Style.ForeColor = Color.White;
                    
                }
                else
                {
                    //dgr.DefaultCellStyle.BackColor = Color.White;
                    dgr.Cells["未领数量"].Style.BackColor = Color.White  ;
                    dgr.Cells["未领数量"].Style.ForeColor = Color.Black ;
                }

            }
        }

        private void 计划复制ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (null == this.ly_material_plan_mainDataGridView.CurrentRow) return;

            string isAlertplan = this.ly_material_plan_mainDataGridView.CurrentRow.Cells["is_alertPlan"].Value.ToString();

            //if ("True" == isAlertplan)
            //{
            //    MessageBox.Show("警戒计划不用复制,直接添加警戒计划就可以了", "提示");
            //    return;

            //}


            string  parentId = this.ly_material_plan_mainDataGridView.CurrentRow.Cells["id"].Value.ToString();
            string nowPlanNumber = this.ly_material_plan_mainDataGridView.CurrentRow.Cells["计划编号"].Value.ToString();
            string newPlanNumber;

            string message1 = "复制前(计划：" + nowPlanNumber + " ，继续吗？";
            string caption1 = "提示...";
            MessageBoxButtons buttons1 = MessageBoxButtons.YesNo;

            DialogResult result1;



            result1 = MessageBox.Show(message1, caption1, buttons1,
            MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

            if (result1 == DialogResult.Yes)
            {
                newPlanNumber = GetMaxPlanCode(); 

                this.ly_material_plan_mainBindingSource.AddNew();
                this.ly_material_plan_mainDataGridView.CurrentRow.Cells["计划编号"].Value = newPlanNumber;
                this.ly_material_plan_mainDataGridView.CurrentRow.Cells["制定日期"].Value = SQLDatabase.GetNowdate ();
                this.ly_material_plan_mainDataGridView.CurrentRow.Cells["制定人"].Value = SQLDatabase.nowUserName();
                this.ly_material_plan_mainBindingSource.EndEdit();

                this.Validate();
                this.ly_material_plan_mainBindingSource.EndEdit();



                this.ly_material_plan_mainTableAdapter.Update(this.lYPlanMange.ly_material_plan_main);



                SetFormState("Edit");
                //this.制定日期DateTimePicker.Focus();
                
                
                //string insstr = " delete ly_material_plan_main  where material_plan_num = '" + nowPlanNumber + "'";

                string insstr = "";

                if ("True" == isAlertplan)
                {
                    //MessageBox.Show("警戒计划不用复制,直接添加警戒计划就可以了", "提示");
                    //return;

                    insstr = "INSERT INTO [HappyYF_ly].[dbo].[LY_MaterielRequirements] ([plan_id] ,[plan_num] ,[plan_date] ,[plan_remark] ,[status] ,[itemno],[itemname] "
                        + "   ,[dw] ,[warehouse] ,[jph] ,[xhc],[xhj] ,[gg] ,[instock_num],[all_plan_count]  ,[all_plan_outcount] ,[all_plan_noget] ,[plancount] ,[purchasing_num]"
                        +    ",[outsourceing_num],[machining_num] ,[storecount] ,[owecount] ,[financial_unit_price] ,[purchase_price] ,[sortcode] ,[sortname] ,[operator] ,[priority] ,[bott])"
                    + " SELECT  ( select id from ly_material_plan_main where material_plan_num='" + newPlanNumber + "' ) as plan_id ,'" + newPlanNumber + "','" + SQLDatabase.GetNowdate().ToString() + "',[plan_remark] ,[status]"
                    + " ,[itemno] ,[itemname],[dw] ,[warehouse],[jph],[xhc] ,[xhj] ,[gg]  ,[instock_num] ,[all_plan_count] ,[all_plan_outcount] ,[all_plan_noget] ,[plancount] ,[purchasing_num]"
                    + " ,[outsourceing_num] ,[machining_num] ,[storecount] ,[owecount] ,[financial_unit_price] ,[purchase_price] ,[sortcode] ,[sortname],[operator] ,[priority] ,[bott] "
                    + " FROM [HappyYF_ly].[dbo].[LY_MaterielRequirements] "
                    + " where plan_id="+parentId;

                }
                else
                {

                     insstr = " INSERT INTO ly_material_plan_detail ([plan_id] ,[wzbh] ,[plan_count] ) " +
                            " select a.plan_id,a.wzbh,a.plan_count    from  " +
                            " (select ( select id from ly_material_plan_main where material_plan_num='" + newPlanNumber + "' ) as plan_id,wzbh,plan_count " +
                            " from ly_material_plan_detail  where plan_id =" + parentId + ")a ";
                }


                SqlConnection sqlConnection1 = new SqlConnection(SQLDatabase.Connectstring);
                SqlCommand cmd = new SqlCommand();

                cmd.CommandText = insstr;
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

                        //for this session   
                    }
                    catch (SqlException sqle)
                    {

                        MessageBox.Show(sqle.Message.Split('*')[0]);
                    }


                    finally
                    {
                        sqlConnection1.Close();
                        SetFormState("View");

                    }
                }
                if (1 == temp)
                {


                    this.ly_material_plan_mainTableAdapter.Fill(this.lYPlanMange.ly_material_plan_main, "SCJH");
                    this.ly_material_plan_mainBindingSource.Position = this.ly_material_plan_mainBindingSource.Find("计划编号", newPlanNumber);

                   
                }


            }
        }


        ////////////////////////////////////////////

       //private NewfrmWaiting  myProcessBar = null;
     
        //private delegate void IncreaseHandle(int nValue);
        //private IncreaseHandle myIncrease = null;

        ///　<　summary>　　///　Open　process　bar　window　　///　<　/summary>　　
        //private void ShowProcessBar()
        //{
        //    myProcessBar = new NewfrmWaiting();//　Init　increase　event　　
        //    myIncrease = new IncreaseHandle(myProcessBar.Increase);
        //    myProcessBar.ShowDialog();
        //    myProcessBar = null;
        //}

        //////////////////////////////////////

        //private void ThreadFun()
        //{
        //    MethodInvoker mi = new MethodInvoker(ShowProcessBar);
        //    this.BeginInvoke(mi);
        //    Thread.Sleep(1000);//Sleep　a　while　to　show　window　　
        //    bool blnIncreased = false;
        //    object objReturn = null;
        //    do
        //    {
        //        Thread.Sleep(5);
        //        objReturn = this.Invoke(this.myIncrease, new object[] { 2 });
        //        blnIncreased = (bool)objReturn;
        //    } while (blnIncreased);
        //}
     
        /////////////////////////////////// 
        private void toolStripButton15_Click(object sender, EventArgs e)
        {
            if (null == this.ly_material_plan_mainDataGridView.CurrentRow) return;

            if (this.formState != "View") return;

            string isAlertplan = this.ly_material_plan_mainDataGridView.CurrentRow.Cells["is_alertPlan"].Value.ToString();

            if ("True" == isAlertplan) return;

            int parentId = int.Parse(this.ly_material_plan_mainDataGridView.CurrentRow.Cells["id"].Value.ToString());
            string planNum = this.ly_material_plan_mainDataGridView.CurrentRow.Cells["计划编号"].Value.ToString();

           



            //////////////////////////////////

            string message = "生成物料需求吗？";
            string caption = "提示...";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;

            DialogResult result;



            result = MessageBox.Show(message, caption, buttons,
            MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

            if (result == DialogResult.Yes)
            {
               // this.Invalidate();
              //  this.timer1.Interval = 1000;

              //  myIncrease = new IncreaseHandle(frmWaiting.Increase);

               
                NewFrm.Show(this);
                Thread.Sleep(500);
                //this.UseWaitCursor = true;
                //this.Enabled = false;
                //Thread thdSub = new Thread(new ThreadStart(ThreadFun));
                //thdSub.IsBackground = true;
                //thdSub.Start();　

                //int parentId = int.Parse(this.ly_material_plan_mainDataGridView.CurrentRow.Cells["id"].Value.ToString());
                //string planNum = this.ly_material_plan_mainDataGridView.CurrentRow.Cells["计划编号"].Value.ToString();

                // frmWaiting.Show(this);

                //timer1.Start();
                //myProcessBar = new NewfrmWaiting();//　Init　increase　event　　

                // myProcessBar.ShowDialog();




                SqlConnection sqlConnection1 = new SqlConnection(SQLDatabase.Connectstring);
                SqlCommand cmd = new SqlCommand();



                cmd.Parameters.Add("@planId", SqlDbType.Int);
                cmd.Parameters["@planId"].Value = parentId;

                cmd.Parameters.Add("@operator", SqlDbType.VarChar);
                cmd.Parameters["@operator"].Value = SQLDatabase .nowUserName();

                cmd.Parameters.Add("@isalert", SqlDbType.VarChar);
                cmd.Parameters["@isalert"].Value ="yes";


                cmd.CommandText = "LY_MaterielRequirementsPlan";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = sqlConnection1;
                cmd.CommandTimeout = 0;

                sqlConnection1.Open();
                cmd.ExecuteNonQuery();
                sqlConnection1.Close();


                ly_material_plan_explodeTableAdapter.Fill(this.lYPlanMange.ly_material_plan_explode, parentId);
                this.ly_store_planitemcountTableAdapter.Fill(this.lYPlanMange.ly_store_planitemcount, parentId);
                this.ly_plan_getmaterial_departmentTableAdapter.Fill(this.lYPlanMange.ly_plan_getmaterial_department, planNum);

                NewFrm.Hide(this);

                //this.UseWaitCursor = false ;
                //this.Enabled = true;
                //thdSub.Abort ();　
                //usetime = 0;
                //timer1.Stop();
               // frmWaiting.Hide(this);
               // myProcessBar = null;






            }
        }

        private void toolStripButton34_Click(object sender, EventArgs e)
        {
            string planNum = this.ly_material_plan_mainDataGridView.CurrentRow.Cells["计划编号"].Value.ToString();

            if (Check_ifApproved(planNum))
            {

                MessageBox.Show("计划已经批准,不能修改数据...", "注意");
                return;
            }

            /////////////////////////////

            if (null == this.lY_MaterielRequirementsDataGridView.CurrentRow) return;

            //---------------------------
            //if (!string.IsNullOrEmpty(lY_MaterielRequirementsDataGridView.CurrentRow.Cells["已排跟单wx"].Value.ToString()))
            //{
            //    if (decimal.Parse(lY_MaterielRequirementsDataGridView.CurrentRow.Cells["已排跟单wx"].Value.ToString()) > 0)
            //    {
            //        MessageBox.Show("已排跟单,不能修改数据...", "注意");
            //        return;
            //    }
            //}
            //---------------------------


            string nowoperptar = this.lY_MaterielRequirementsDataGridView.CurrentRow.Cells["计划员"].Value.ToString();

            if (nowoperptar != SQLDatabase.nowUserName())
            {
                MessageBox.Show("请计划员:" + nowoperptar + "修改", "注意");
                return;
            }

            //if ("True" == ly_store_innumDataGridView.CurrentRow.Cells["签证"].Value.ToString())
            //{
            //    MessageBox.Show("已经签证,不能修改入库数据...");

            //    return;

            //}


            //string ifcheck_Id = this.ly_store_innumDataGridView.CurrentRow.Cells["签证"].Value.ToString();

            //if (!string.IsNullOrEmpty(ifcheck_Id))
            //{
            //    MessageBox.Show("盘点数据不能在这里修改", "注意");
            //    return;
            //}

            //this.nowInNum = ly_store_innumDataGridView.CurrentRow.Cells["入库单号"].Value.ToString();
            //this.nowdate = ly_store_innumDataGridView.CurrentRow.Cells["入库日期1"].Value.ToString();


            //this.lY_MaterielRequirementsDataGridView.ReadOnly = false;
            this.toolStripButton34.Enabled = false;
            this.保存SToolStripButton.Enabled = true;
            foreach (DataGridViewColumn dvc in lY_MaterielRequirementsDataGridView.Columns)
            {
                
                if ("采购未到" == dvc.DataPropertyName
                     || "外协未到" == dvc.DataPropertyName
                     || "机加在制" == dvc.DataPropertyName
                      || "缓急" == dvc.DataPropertyName)
                {
                    dvc.ReadOnly = false;
                    //dvc.DefaultCellStyle.BackColor = Color.Teal;
                    //dvc.DefaultCellStyle.ForeColor = Color.White;
                }
                else if ("欠料" == dvc.DataPropertyName || "采购数量" == dvc.DataPropertyName)
                {
                    dvc.ReadOnly = false;
                    //dvc.DefaultCellStyle.BackColor = Color.White ;
                    //dvc.DefaultCellStyle.ForeColor = Color.Red ;
                }

            }
        }

        private void 保存SToolStripButton_Click(object sender, EventArgs e)
        {
            this.lY_MaterielRequirementsDataGridView.EndEdit();
            this.lY_MaterielRequirementsBindingSource.EndEdit();

            try
            {
                this.lY_MaterielRequirementsTableAdapter.Update(this.lYPlanMange.LY_MaterielRequirements);
            }
            catch (SqlException sqle)
            {


                MessageBox.Show(sqle.Message);
            }

            foreach (DataGridViewColumn dvc in lY_MaterielRequirementsDataGridView.Columns)
            {

                if ("采购未到" == dvc.DataPropertyName
                     || "外协未到" == dvc.DataPropertyName
                     || "机加在制" == dvc.DataPropertyName
                     || "缓急" == dvc.DataPropertyName)
                {
                    dvc.ReadOnly = true ;
                    //dvc.DefaultCellStyle.BackColor = Color.Teal;
                    //dvc.DefaultCellStyle.ForeColor = Color.White;
                }
                else if ("欠料" == dvc.DataPropertyName || "采购数量" == dvc.DataPropertyName)
                {
                    dvc.ReadOnly = true ;
                    //dvc.DefaultCellStyle.BackColor = Color.White;
                    //dvc.DefaultCellStyle.ForeColor = Color.Red;
                }

            }
            
            this.toolStripButton34.Enabled = true;
            this.保存SToolStripButton.Enabled = false;


        }

        private void lY_MaterielRequirementsDataGridView_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {

            DataGridView dgvc = sender as DataGridView;
            string temp = dgvc[e.ColumnIndex, e.RowIndex].EditedFormattedValue.ToString().Replace(" ", "");

            if (temp == "")
            {
                dgvc[e.ColumnIndex, e.RowIndex].Value = DBNull.Value;
                e.Cancel = false;
            }
            else
            {
                
                MessageBox.Show("数据格式错误...");
            }

         

        }

        private void 新建NToolStripButton_Click(object sender, EventArgs e)
        {
            string planNum = this.ly_material_plan_mainDataGridView.CurrentRow.Cells["计划编号"].Value.ToString();

            if (Check_ifApproved(planNum))
            {

                MessageBox.Show("计划已经批准,不能修改数据...", "注意");
                return;
            }
            
            
            string message1 = "重新按照手工输入值计算欠件,继续吗？";
            string caption1 = "提示...";
            MessageBoxButtons buttons1 = MessageBoxButtons.YesNo;

            DialogResult result1;



            result1 = MessageBox.Show(message1, caption1, buttons1,
            MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

            if (result1 == DialogResult.Yes)
            {

                this.lY_MaterielRequirementsDataGridView.EndEdit();
                //this.lY_MaterielRequirementspurDataGridView.EndEdit();
                //this.lY_MaterielRequirementsMashineDataGridView.EndEdit();
                this.lY_MaterielRequirementsBindingSource.EndEdit();
                this.lY_MaterielRequirementsTableAdapter.Update(this.lYPlanMange.LY_MaterielRequirements);


                int parentId = int.Parse(this.ly_material_plan_mainDataGridView.CurrentRow.Cells["id"].Value.ToString());



                //string insstr = " delete ly_material_plan_main  where material_plan_num = '" + nowPlanNumber + "'";

                //string updstr = " update LY_MaterielRequirements  " +
                //        "  set owecount= (isnull(plancount,0) + isnull(all_plan_noget,0))  - (isnull(storecount,0) + isnull(purchasing_num,0) + isnull(outsourceing_num,0) + isnull(machining_num,0)) "
                //        + " where  plan_id=" + parentId.ToString();

                SqlConnection sqlConnection1 = new SqlConnection(SQLDatabase.Connectstring);
                SqlCommand cmd = new SqlCommand();

                //string updstr = " update LY_MaterielRequirements set storecount=dbo.f_LY_Storecount(itemno) FROM LY_MaterielRequirements WHERE (plan_id =" + parentId.ToString() + " ) AND (sortname ='外协') ";

                //cmd.CommandText = updstr;
                //cmd.CommandType = CommandType.Text;
                //cmd.Connection = sqlConnection1;

                //int temp = 0;

                //using (TransactionScope scope = new TransactionScope())
                //{

                //    sqlConnection1.Open();
                //    try
                //    {

                //        cmd.ExecuteNonQuery();



                //        scope.Complete();
                //        temp = 1;


                //    }
                //    catch (SqlException sqle)
                //    {


                //        MessageBox.Show(sqle.Message.Split('*')[0]);
                //    }


                //    finally
                //    {
                //        sqlConnection1.Close();


                //    }
                //}
                //if (1 == temp)
                //{

                //    //int parentId = int.Parse(this.ly_material_plan_mainDataGridView.CurrentRow.Cells["id"].Value.ToString());
                //    this.lY_MaterielRequirementsTableAdapter.Fill(this.lYPlanMange.LY_MaterielRequirements, parentId, "外协", "OWE");

                //}


                string  updstr = " update LY_MaterielRequirements  " +
                     "  set owecount= (isnull(plancount,0)+isnull(lastplancount,0) +isnull(bott,0)+isnull(all_plan_noget,0))  - (isnull(storecount,0) + isnull(purchasing_num,0) + isnull(outsourceing_num,0) + isnull(machining_num,0)) "
                     + " where sortname = '外协' and plan_id=" + parentId.ToString();

                //tttttt


               
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

                    ////int parentId = int.Parse(this.ly_material_plan_mainDataGridView.CurrentRow.Cells["id"].Value.ToString());
                    this.lY_MaterielRequirementsTableAdapter.Fill(this.lYPlanMange.LY_MaterielRequirements, parentId, "外协", "OWE");

                }

               
            }
        }

        private void ThreadFun()
        {
            //usetime = usetime + 1;
            

         
           // Thread.Sleep(1000);//Sleep　a　while　to　show　window　　

            //this.Invoke(this.myIncrease, new object[] { usetime });
            // // NewfrmWaiting  myProcessBar = null;
            // myProcessBar = new NewfrmWaiting();//　Init　increase　event　　

            //myProcessBar.ShowDialog();
          
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
           

            //Thread thdSub = new Thread(new ThreadStart(ThreadFun)); 
            //       thdSub.Start();　
            
            
        }

        private void toolStripButton22_Click(object sender, EventArgs e)
        {
            if (null == this.ly_material_plan_mainDataGridView.CurrentRow) return;
            int parentId = int.Parse(this.ly_material_plan_mainDataGridView.CurrentRow.Cells["id"].Value.ToString());
            
            if ("显示全部" == toolStripButton22.Text)
            {
                toolStripButton22.Text = "显示欠料";
                this.lY_MaterielRequirementsTableAdapter.Fill(this.lYPlanMange.LY_MaterielRequirements, parentId, "外协","OWE");
            }
            else
            {
                toolStripButton22.Text = "显示全部";
                this.lY_MaterielRequirementsTableAdapter.Fill(this.lYPlanMange.LY_MaterielRequirements, parentId, "外协","ALL");
            }

           
        }

        private void 打印PToolStripButton1_Click(object sender, EventArgs e)
        {
            if (null == this.lY_MaterielRequirementsDataGridView.CurrentRow) return;



            //string balanceFlag = this.xD_Sel_SellBalanceDataGridView.CurrentRow.Cells["结算"].Value.ToString();



            //NewFrm.Show(this); 2018-04-03


            BaseReportView queryForm = new BaseReportView();

            queryForm.Text = "中原精密生产计划物料需求表";

            queryForm.Printdata = this.lYPlanMange;

            queryForm.PrintCrystalReport = new LY_MaterielRequirements();

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

            //string selectFormula;
            ////selectFormula = "{ly_store_planitemcount.status}  =   '原料' and {ly_store_planitemcount.owemoney} >0 ";
            //selectFormula = "{ly_store_planitemcount.状态}  =   '原料'  and {ly_store_planitemcount.计划欠料金额}>0 ";
            //queryForm.PrintCrystalReport.DataDefinition.RecordSelectionFormula = selectFormula;

            //((CrystalDecisions.CrystalReports.Engine.TextObject)(queryForm.PrintCrystalReport.DataDefinition.GroupNameFields.GroupHeaderSection1.ReportObjects["Text24"])).Text = "潼关中金冶炼有限责任公司原料结算单";


            //queryForm.PrintCrystalReport = new XD_SellBalance_All();

            //string nowCardNumber = this.xD_Sel_SellBalanceDataGridView.CurrentRow.Cells["编号"].Value.ToString();

            //NewFrm.Hide(this);2018-04-03

            queryForm.ShowDialog();
        }

        private void toolStripButton35_Click(object sender, EventArgs e)
        {
            /////////////////////////////

            string planNum = this.ly_material_plan_mainDataGridView.CurrentRow.Cells["计划编号"].Value.ToString();

            if (Check_ifApproved(planNum))
            {

                MessageBox.Show("计划已经批准,不能修改数据...", "注意");
                return;
            }



            if (null == this.lY_MaterielRequirementspurDataGridView.CurrentRow) return;


            //---------------------------
            //if (!string.IsNullOrEmpty(lY_MaterielRequirementspurDataGridView.CurrentRow.Cells["已排跟单cg"].Value.ToString()))
            //{
            //    if (decimal.Parse(lY_MaterielRequirementspurDataGridView.CurrentRow.Cells["已排跟单cg"].Value.ToString()) > 0)
            //    {
            //        MessageBox.Show("已排跟单,不能修改数据...", "注意");
            //        return;
            //    }
            //}
            //---------------------------



            string nowoperptar = this.lY_MaterielRequirementspurDataGridView.CurrentRow.Cells["计划员1"].Value.ToString();

            if (nowoperptar != SQLDatabase.nowUserName())
            {
                MessageBox.Show("请计划员:" + nowoperptar + "修改", "注意");
                return;
            }

          
            this.toolStripButton35.Enabled = false;
            this.toolStripButton23.Enabled = true;
            foreach (DataGridViewColumn dvc in lY_MaterielRequirementspurDataGridView.Columns)
            {

                if ("采购未到" == dvc.DataPropertyName
                     || "外协未到" == dvc.DataPropertyName
                     || "机加在制" == dvc.DataPropertyName
                      || "缓急" == dvc.DataPropertyName)
                {
                    dvc.ReadOnly = false;
                    //dvc.DefaultCellStyle.BackColor = Color.Teal;
                    //dvc.DefaultCellStyle.ForeColor = Color.White;
                }
                else if ("欠料" == dvc.DataPropertyName)
                {
                    dvc.ReadOnly = false;
                    //dvc.DefaultCellStyle.BackColor = Color.White ;
                    //dvc.DefaultCellStyle.ForeColor = Color.Red ;
                }

            }
        }

        //private void 保存SToolStripButton_Click(object sender, EventArgs e)
        //{
        //    this.lY_MaterielRequirementsDataGridView.EndEdit();
        //    this.lY_MaterielRequirementsBindingSource.EndEdit();

        //    try
        //    {
        //        this.lY_MaterielRequirementsTableAdapter.Update(this.lYPlanMange.LY_MaterielRequirements);
        //    }
        //    catch (SqlException sqle)
        //    {


        //        MessageBox.Show(sqle.Message);
        //    }

        //    foreach (DataGridViewColumn dvc in lY_MaterielRequirementsDataGridView.Columns)
        //    {

        //        if ("采购未到" == dvc.DataPropertyName
        //             || "外协未到" == dvc.DataPropertyName
        //             || "机加在制" == dvc.DataPropertyName)
        //        {
        //            dvc.ReadOnly = true;
        //            //dvc.DefaultCellStyle.BackColor = Color.Teal;
        //            //dvc.DefaultCellStyle.ForeColor = Color.White;
        //        }
        //        else if ("欠料" == dvc.DataPropertyName)
        //        {
        //            dvc.ReadOnly = true;
        //            //dvc.DefaultCellStyle.BackColor = Color.White;
        //            //dvc.DefaultCellStyle.ForeColor = Color.Red;
        //        }

        //    }

        //    this.toolStripButton34.Enabled = true;
        //    this.保存SToolStripButton.Enabled = false;
        //}

        private void toolStripButton23_Click(object sender, EventArgs e)
        {
            this.lY_MaterielRequirementspurDataGridView.EndEdit();
            this.lY_MaterielRequirementsBindingSource.EndEdit();

            try
            {
                this.lY_MaterielRequirementsTableAdapter.Update(this.lYPlanMange.LY_MaterielRequirements);
            }
            catch (SqlException sqle)
            {


                MessageBox.Show(sqle.Message);
            }

            foreach (DataGridViewColumn dvc in lY_MaterielRequirementspurDataGridView.Columns)
            {

                if ("采购未到" == dvc.DataPropertyName
                     || "外协未到" == dvc.DataPropertyName
                     || "机加在制" == dvc.DataPropertyName
                     || "缓急" == dvc.DataPropertyName)
                {
                    dvc.ReadOnly = true;
                    //dvc.DefaultCellStyle.BackColor = Color.Teal;
                    //dvc.DefaultCellStyle.ForeColor = Color.White;
                }
                else if ("欠料" == dvc.DataPropertyName)
                {
                    dvc.ReadOnly = true;
                    //dvc.DefaultCellStyle.BackColor = Color.White;
                    //dvc.DefaultCellStyle.ForeColor = Color.Red;
                }

            }

            this.toolStripButton35.Enabled = true;
            this.toolStripButton23.Enabled = false;
        }

        private void toolStripButton24_Click(object sender, EventArgs e)
        {
            if (null == this.lY_MaterielRequirementspurDataGridView.CurrentRow) return;



            //string balanceFlag = this.xD_Sel_SellBalanceDataGridView.CurrentRow.Cells["结算"].Value.ToString();



            //NewFrm.Show(this); 2018-04-03


            BaseReportView queryForm = new BaseReportView();

            queryForm.Text = "中原精密生产计划物料需求表";

            queryForm.Printdata = this.lYPlanMange;

            queryForm.PrintCrystalReport = new LY_MaterielRequirements();

           

            //NewFrm.Hide(this);2018-04-03

            queryForm.ShowDialog();
        }

        private void toolStripButton37_Click(object sender, EventArgs e)
        {
            if (null == this.ly_material_plan_mainDataGridView.CurrentRow) return;
            int parentId = int.Parse(this.ly_material_plan_mainDataGridView.CurrentRow.Cells["id"].Value.ToString());

            if ("显示全部" == toolStripButton37.Text)
            {
                toolStripButton37.Text = "显示欠料";
                this.lY_MaterielRequirementsTableAdapter.Fill(this.lYPlanMange.LY_MaterielRequirements, parentId, "外购", "OWE");
            }
            else
            {
                toolStripButton37.Text = "显示全部";
                this.lY_MaterielRequirementsTableAdapter.Fill(this.lYPlanMange.LY_MaterielRequirements, parentId, "外购", "ALL");
            }
        }

        private void toolStripButton31_Click(object sender, EventArgs e)
        {
            string planNum = this.ly_material_plan_mainDataGridView.CurrentRow.Cells["计划编号"].Value.ToString();

            if (Check_ifApproved(planNum))
            {

                MessageBox.Show("计划已经批准,不能修改数据...", "注意");
                return;
            }
            
            string message1 = "重新按照手工输入值计算欠件,继续吗？";
            string caption1 = "提示...";
            MessageBoxButtons buttons1 = MessageBoxButtons.YesNo;

            DialogResult result1;



            result1 = MessageBox.Show(message1, caption1, buttons1,
            MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

            if (result1 == DialogResult.Yes)
            {

                //this.lY_MaterielRequirementsDataGridView.EndEdit();
                this.lY_MaterielRequirementspurDataGridView.EndEdit();
                //this.lY_MaterielRequirementsMashineDataGridView.EndEdit();
                this.lY_MaterielRequirementsBindingSource.EndEdit();
                this.lY_MaterielRequirementsTableAdapter.Update(this.lYPlanMange.LY_MaterielRequirements);





                int parentId = int.Parse(this.ly_material_plan_mainDataGridView.CurrentRow.Cells["id"].Value.ToString());



                //string insstr = " delete ly_material_plan_main  where material_plan_num = '" + nowPlanNumber + "'";

                //string updstr = " update LY_MaterielRequirements  " +
                //        "  set owecount= (isnull(plancount,0) + isnull(all_plan_noget,0)+isnull(bott,0))  - (isnull(storecount,0) + isnull(purchasing_num,0) + isnull(outsourceing_num,0) + isnull(machining_num,0)) "
                //        + " where  plan_id=" + parentId.ToString();

                SqlConnection sqlConnection1 = new SqlConnection(SQLDatabase.Connectstring);
                SqlCommand cmd = new SqlCommand();


                //string updstr = " update LY_MaterielRequirements set storecount=dbo.f_LY_Storecount(itemno) FROM LY_MaterielRequirements WHERE (plan_id =" + parentId.ToString() + " ) AND (sortname ='外购') ";

                //cmd.CommandText = updstr;
                //cmd.CommandType = CommandType.Text;
                //cmd.Connection = sqlConnection1;

                //int temp = 0;

                //using (TransactionScope scope = new TransactionScope())
                //{

                //    sqlConnection1.Open();
                //    try
                //    {

                //        cmd.ExecuteNonQuery();



                //        scope.Complete();
                //        temp = 1;


                //    }
                //    catch (SqlException sqle)
                //    {


                //        MessageBox.Show(sqle.Message.Split('*')[0]);
                //    }


                //    finally
                //    {
                //        sqlConnection1.Close();


                //    }
                //}
                //if (1 == temp)
                //{

                //    //int parentId = int.Parse(this.ly_material_plan_mainDataGridView.CurrentRow.Cells["id"].Value.ToString());
                //    //this.lY_MaterielRequirementsTableAdapter.Fill(this.lYPlanMange.LY_MaterielRequirements, parentId, "外购", "OWE");

                //}

                string  updstr = " update LY_MaterielRequirements  " +
                       "  set owecount= (isnull(plancount,0)+isnull(lastplancount,0) +isnull(bott,0)+isnull(all_plan_noget,0))  - (isnull(storecount,0) + isnull(purchasing_num,0) + isnull(outsourceing_num,0) + isnull(machining_num,0)) "
                       + " where sortname = '外购' and plan_id=" + parentId.ToString();



             
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

                    //int parentId = int.Parse(this.ly_material_plan_mainDataGridView.CurrentRow.Cells["id"].Value.ToString());
                    this.lY_MaterielRequirementsTableAdapter.Fill(this.lYPlanMange.LY_MaterielRequirements, parentId, "外购", "OWE");

                }
               


            }
        }

        private void toolStripButton38_Click(object sender, EventArgs e)
        {
            string planNum = this.ly_material_plan_mainDataGridView.CurrentRow.Cells["计划编号"].Value.ToString();

            if (Check_ifApproved(planNum))
            {

                MessageBox.Show("计划已经批准,不能修改数据...", "注意");
                return;
            }
            
            string message1 = "重新按照手工输入值计算欠件,继续吗？";
            string caption1 = "提示...";
            MessageBoxButtons buttons1 = MessageBoxButtons.YesNo;

            DialogResult result1;



            result1 = MessageBox.Show(message1, caption1, buttons1,
            MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

            if (result1 == DialogResult.Yes)
            {

                //this.lY_MaterielRequirementsDataGridView.EndEdit();
                //this.lY_MaterielRequirementspurDataGridView.EndEdit();
                this.lY_MaterielRequirementsMashineDataGridView.EndEdit();
                this.lY_MaterielRequirementsBindingSource.EndEdit();
                this.lY_MaterielRequirementsTableAdapter.Update(this.lYPlanMange.LY_MaterielRequirements);





                int parentId = int.Parse(this.ly_material_plan_mainDataGridView.CurrentRow.Cells["id"].Value.ToString());



                //string insstr = " delete ly_material_plan_main  where material_plan_num = '" + nowPlanNumber + "'";

                //string updstr = " update LY_MaterielRequirements  " +
                //        "  set owecount= (isnull(plancount,0) + isnull(all_plan_noget,0))  - (isnull(storecount,0) + isnull(purchasing_num,0) + isnull(outsourceing_num,0) + isnull(machining_num,0)) "
                //        + " where  plan_id=" + parentId.ToString();

                SqlConnection sqlConnection1 = new SqlConnection(SQLDatabase.Connectstring);
                SqlCommand cmd = new SqlCommand();


                //string updstr = " update LY_MaterielRequirements set storecount=dbo.f_LY_Storecount(itemno) FROM LY_MaterielRequirements WHERE (plan_id =" + parentId.ToString() + " ) AND (sortname ='机加') ";

                //cmd.CommandText = updstr;
                //cmd.CommandType = CommandType.Text;
                //cmd.Connection = sqlConnection1;

                //int temp = 0;

                //using (TransactionScope scope = new TransactionScope())
                //{

                //    sqlConnection1.Open();
                //    try
                //    {

                //        cmd.ExecuteNonQuery();



                //        scope.Complete();
                //        temp = 1;


                //    }
                //    catch (SqlException sqle)
                //    {


                //        MessageBox.Show(sqle.Message.Split('*')[0]);
                //    }


                //    finally
                //    {
                //        sqlConnection1.Close();


                //    }
                //}
                //if (1 == temp)
                //{

                //    //int parentId = int.Parse(this.ly_material_plan_mainDataGridView.CurrentRow.Cells["id"].Value.ToString());
                //   //this.lY_MaterielRequirementsTableAdapter.Fill(this.lYPlanMange.LY_MaterielRequirements, parentId, "机加", "OWE");

                //}

                string  updstr = " update LY_MaterielRequirements  " +
                    "  set owecount= (isnull(plancount,0)+isnull(lastplancount,0) +isnull(bott,0)+isnull(all_plan_noget,0))  - (isnull(storecount,0) + isnull(purchasing_num,0) + isnull(outsourceing_num,0) + isnull(machining_num,0)) "
                    + " where sortname = '机加' and  plan_id=" + parentId.ToString();


               
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

                    //int parentId = int.Parse(this.ly_material_plan_mainDataGridView.CurrentRow.Cells["id"].Value.ToString());
                    this.lY_MaterielRequirementsTableAdapter.Fill(this.lYPlanMange.LY_MaterielRequirements, parentId, "机加", "OWE");

                }

              


            }
        }

        private void toolStripButton36_Click(object sender, EventArgs e)
        {
            /////////////////////////////

            string planNum = this.ly_material_plan_mainDataGridView.CurrentRow.Cells["计划编号"].Value.ToString();

            if (Check_ifApproved(planNum))
            {

                MessageBox.Show("计划已经批准,不能修改数据...", "注意");
                return;
            }

            if (null == this.lY_MaterielRequirementsMashineDataGridView.CurrentRow) return;
            //---------------------------
            //if (!string.IsNullOrEmpty(lY_MaterielRequirementsMashineDataGridView.CurrentRow.Cells["已排跟单jj"].Value.ToString()))
            //{
            //    if (decimal.Parse(lY_MaterielRequirementsMashineDataGridView.CurrentRow.Cells["已排跟单jj"].Value.ToString()) > 0)
            //    {
            //        MessageBox.Show("已排跟单,不能修改数据...", "注意");
            //        return;
            //    }
            //}
            //---------------------------
            string nowoperptar = this.lY_MaterielRequirementsMashineDataGridView.CurrentRow.Cells["计划员2"].Value.ToString();

            if (nowoperptar != SQLDatabase.nowUserName())
            {
                MessageBox.Show("请计划员:" + nowoperptar + "修改", "注意");
                return;
            }


            this.toolStripButton36.Enabled = false;
            this.toolStripButton32.Enabled = true;
            foreach (DataGridViewColumn dvc in lY_MaterielRequirementsMashineDataGridView.Columns)
            {

                if ("采购未到" == dvc.DataPropertyName
                     || "外协未到" == dvc.DataPropertyName
                     || "机加在制" == dvc.DataPropertyName
                     || "缓急" == dvc.DataPropertyName)
                {
                    dvc.ReadOnly = false;
                    //dvc.DefaultCellStyle.BackColor = Color.Teal;
                    //dvc.DefaultCellStyle.ForeColor = Color.White;
                }
                else if ("欠料" == dvc.DataPropertyName)
                {
                    dvc.ReadOnly = false;
                    //dvc.DefaultCellStyle.BackColor = Color.White ;
                    //dvc.DefaultCellStyle.ForeColor = Color.Red ;
                }

            }
        }

        private void toolStripButton32_Click(object sender, EventArgs e)
        {
            this.lY_MaterielRequirementsMashineDataGridView.EndEdit();
            this.lY_MaterielRequirementsBindingSource.EndEdit();

            try
            {
                this.lY_MaterielRequirementsTableAdapter.Update(this.lYPlanMange.LY_MaterielRequirements);
            }
            catch (SqlException sqle)
            {


                MessageBox.Show(sqle.Message);
            }

            foreach (DataGridViewColumn dvc in lY_MaterielRequirementsMashineDataGridView.Columns)
            {

                if ("采购未到" == dvc.DataPropertyName
                     || "外协未到" == dvc.DataPropertyName
                     || "机加在制" == dvc.DataPropertyName
                     || "缓急" == dvc.DataPropertyName)
                {
                    dvc.ReadOnly = true;
                    //dvc.DefaultCellStyle.BackColor = Color.Teal;
                    //dvc.DefaultCellStyle.ForeColor = Color.White;
                }
                else if ("欠料" == dvc.DataPropertyName)
                {
                    dvc.ReadOnly = true;
                    //dvc.DefaultCellStyle.BackColor = Color.White;
                    //dvc.DefaultCellStyle.ForeColor = Color.Red;
                }

            }

            this.toolStripButton36.Enabled = true;
            this.toolStripButton32.Enabled = false;
        }

        private void toolStripButton33_Click(object sender, EventArgs e)
        {
            if (null == this.lY_MaterielRequirementsMashineDataGridView.CurrentRow) return;



            //string balanceFlag = this.xD_Sel_SellBalanceDataGridView.CurrentRow.Cells["结算"].Value.ToString();



            //NewFrm.Show(this); 2018-04-03


            BaseReportView queryForm = new BaseReportView();

            queryForm.Text = "中原精密生产计划物料需求表";

            queryForm.Printdata = this.lYPlanMange;

            queryForm.PrintCrystalReport = new LY_MaterielRequirements();



            //NewFrm.Hide(this);2018-04-03

            queryForm.ShowDialog();
        }

        private void toolStripButton39_Click(object sender, EventArgs e)
        {
            if (null == this.ly_material_plan_mainDataGridView.CurrentRow) return;
            int parentId = int.Parse(this.ly_material_plan_mainDataGridView.CurrentRow.Cells["id"].Value.ToString());

            if ("显示全部" == toolStripButton39.Text)
            {
                toolStripButton39.Text = "显示欠料";
                this.lY_MaterielRequirementsTableAdapter.Fill(this.lYPlanMange.LY_MaterielRequirements, parentId, "机加", "OWE");
            }
            else
            {
                toolStripButton39.Text = "显示全部";
                this.lY_MaterielRequirementsTableAdapter.Fill(this.lYPlanMange.LY_MaterielRequirements, parentId, "机加", "ALL");
            }
        }

        private void lY_MaterielRequirementsDataGridView_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            DataGridView dgvc = sender as DataGridView;
           string temp=dgvc[e.ColumnIndex, e.RowIndex].EditedFormattedValue.ToString().Replace(" ", "");

           if (temp == "")
                      dgvc[e.ColumnIndex, e.RowIndex].Value = DBNull.Value; 
                  e.Cancel = false; 
        }

        private void lY_MaterielRequirementsDataGridView_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            //if (true == lY_MaterielRequirementsDataGridView.ReadOnly) return;

            DataGridView dgv = sender as DataGridView;

            if ("采购未到" == dgv.CurrentCell.OwningColumn.DataPropertyName
                   || "外协未到" == dgv.CurrentCell.OwningColumn.DataPropertyName
                   || "机加在制" == dgv.CurrentCell.OwningColumn.DataPropertyName
                    || "缓急" == dgv.CurrentCell.OwningColumn.DataPropertyName)
            {

                dgv.CurrentCell.Style.BackColor = Color.White;
                dgv.CurrentCell.Style.ForeColor = Color.Teal;
              
            }
            else if ("欠料" == dgv.CurrentCell.OwningColumn.DataPropertyName)
            {

                //dgv.CurrentCell.Style.BackColor = Color.White;
                //dgv.CurrentCell.Style.BackColor = Color.Red;
            }
            else if ("采购数量" == dgv.CurrentCell.OwningColumn.DataPropertyName)
            {

                
            }
          
        }

        private void lY_MaterielRequirementsDataGridView_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView dgv = sender as DataGridView;

            if ("采购未到" == dgv.CurrentCell.OwningColumn.DataPropertyName
                   || "外协未到" == dgv.CurrentCell.OwningColumn.DataPropertyName
                   || "机加在制" == dgv.CurrentCell.OwningColumn.DataPropertyName
                   || "缓急" == dgv.CurrentCell.OwningColumn.DataPropertyName)
            {

                dgv.CurrentCell.Style.BackColor = Color.Teal ;
                dgv.CurrentCell.Style.ForeColor = Color.White ;

            }
            //else if ("欠料" == dgv.CurrentCell.OwningColumn.DataPropertyName)
            //{
            //    if (false == dgv.CurrentCell.OwningColumn.ReadOnly)
            //    {
            //      if ( !string .IsNullOrEmpty ( dgv.CurrentRow.Cells[16].Value.ToString()) 
            //           && !string .IsNullOrEmpty (dgv.CurrentRow.Cells["转换比例"].Value.ToString ()))
            //      {
            //          dgv.EndEdit(); 
            //          dgv.CurrentRow.Cells["采购数量"].Value = decimal.Parse(dgv.CurrentRow.Cells[16].Value.ToString()) / decimal.Parse(dgv.CurrentRow.Cells["转换比例"].Value.ToString());
            //          dgv.Refresh();
                      
            //      }
            //    }
            //}
            //else if ("采购数量" == dgv.CurrentCell.OwningColumn.DataPropertyName)
            //{
            //    if (false == dgv.CurrentCell.OwningColumn.ReadOnly)
            //    {
            //        if (!string.IsNullOrEmpty(dgv.CurrentRow.Cells["采购数量"].Value.ToString())
            //          && !string.IsNullOrEmpty(dgv.CurrentRow.Cells["转换比例"].Value.ToString()))
            //        {
            //            dgv.EndEdit();
            //            dgv.CurrentRow.Cells["欠料"].Value = decimal.Parse(dgv.CurrentRow.Cells["采购数量"].Value.ToString()) * decimal.Parse(dgv.CurrentRow.Cells["转换比例"].Value.ToString());
            //            dgv.Refresh();
                        
            //        }
            //    }
            //}
        }

        private void lY_MaterielRequirementsDataGridView_CellLeave1(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView dgv = sender as DataGridView;

            if ("采购未到" == dgv.CurrentCell.OwningColumn.DataPropertyName
                   || "外协未到" == dgv.CurrentCell.OwningColumn.DataPropertyName
                   || "机加在制" == dgv.CurrentCell.OwningColumn.DataPropertyName
                   || "缓急" == dgv.CurrentCell.OwningColumn.DataPropertyName)
            {

                dgv.CurrentCell.Style.BackColor = Color.Teal;
                dgv.CurrentCell.Style.ForeColor = Color.White;

            }
            else if ("欠料" == dgv.CurrentCell.OwningColumn.DataPropertyName)
            {
                if (false == dgv.CurrentCell.OwningColumn.ReadOnly)
                {
                    if (!string.IsNullOrEmpty(dgv.CurrentRow.Cells["欠料"].Value.ToString())
                         && !string.IsNullOrEmpty(dgv.CurrentRow.Cells["转换比例"].Value.ToString()))
                    {
                        dgv.EndEdit();
                        dgv.CurrentRow.Cells["采购数量"].Value = decimal.Parse(dgv.CurrentRow.Cells["欠料"].Value.ToString()) / decimal.Parse(dgv.CurrentRow.Cells["转换比例"].Value.ToString());
                        dgv.Refresh();

                    }
                }
            }
            else if ("采购数量" == dgv.CurrentCell.OwningColumn.DataPropertyName)
            {
                if (false == dgv.CurrentCell.OwningColumn.ReadOnly)
                {
                    if (!string.IsNullOrEmpty(dgv.CurrentRow.Cells["采购数量"].Value.ToString())
                      && !string.IsNullOrEmpty(dgv.CurrentRow.Cells["转换比例"].Value.ToString()))
                    {
                        dgv.EndEdit();
                        dgv.CurrentRow.Cells["欠料"].Value = decimal.Parse(dgv.CurrentRow.Cells["采购数量"].Value.ToString()) * decimal.Parse(dgv.CurrentRow.Cells["转换比例"].Value.ToString());
                        dgv.Refresh();

                    }
                }
            }
        }

        private void toolStripButton40_Click(object sender, EventArgs e)
        {
            string message = "增加警戒物料计划吗？";
            string caption = "提示...";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;

            DialogResult result;



            result = MessageBox.Show(message, caption, buttons,
            MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

            if (result == DialogResult.Yes)
            {
                this.ly_material_plan_mainBindingSource.AddNew();
                this.ly_material_plan_mainDataGridView.CurrentRow.Cells["计划编号"].Value = GetMaxPlanCode();
                this.ly_material_plan_mainDataGridView.CurrentRow.Cells["制定日期"].Value = SQLDatabase.GetNowdate();
                this.ly_material_plan_mainDataGridView.CurrentRow.Cells["制定人"].Value = SQLDatabase.nowUserName();
                this.ly_material_plan_mainDataGridView.CurrentRow.Cells["is_alertPlan"].Value = 1;
                this.ly_material_plan_mainDataGridView.CurrentRow.Cells["说明"].Value ="警戒计划"+DateTime .Today .Date .ToString ("yyyyMMdd");
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

        private void bindingNavigatorDeleteItem4_Click(object sender, EventArgs e)
        {
            string planNum = this.ly_material_plan_mainDataGridView.CurrentRow.Cells["计划编号"].Value.ToString();

            if (Check_ifApproved(planNum))
            {

                MessageBox.Show("计划已经批准,不能修改数据...", "注意");
                return;
            }
            DataGridView dgv=null;

            if (3 == this.tabControl1.SelectedIndex)
            {
                dgv = this.lY_MaterielRequirementsDataGridView;

                if (!string.IsNullOrEmpty(dgv.CurrentRow.Cells["已排跟单wx"].Value.ToString()))
                {

                    MessageBox.Show("后续业务已经开始,不能删除数据", "注意");
                    return;
                }
            }
            else if (4 == this.tabControl1.SelectedIndex)
            {
                dgv = this.lY_MaterielRequirementspurDataGridView;

                if (!string.IsNullOrEmpty(dgv.CurrentRow.Cells["已排跟单cg"].Value.ToString()))
                {

                    MessageBox.Show("后续业务已经开始,不能删除数据", "注意");
                    return;
                }
            }
            else if (5 == this.tabControl1.SelectedIndex)
            {
                dgv = this.lY_MaterielRequirementsMashineDataGridView;
                if (!string.IsNullOrEmpty(dgv.CurrentRow.Cells["已排跟单jj"].Value.ToString()))
                {

                    MessageBox.Show("后续业务已经开始,不能删除数据", "注意");
                    return;
                }
            }


            if (null == dgv.CurrentRow) return;

          


            

            string message = "确定删除当前记录吗？";
            string caption = "提示...";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result;



            result = MessageBox.Show(message, caption, buttons,
            MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

            if (result == DialogResult.Yes)
            {

               //////////////////////////////////
                int tempcount = 0;
                int tempcount2 = 0;

                this.itemlist.Clear();
                foreach (DataGridViewRow dgr in dgv.Rows)
                {
                    if (true == dgr.Selected)
                    {
                        tempcount = tempcount + 1;



                        this.itemlist.Add(int.Parse (dgr.Cells[0].Value.ToString ()));

                    }
                }
                //////////////////////////////////////////////

                foreach (int nowitem in itemlist)
                {


                    lY_MaterielRequirementsBindingSource.RemoveAt(this.lY_MaterielRequirementsBindingSource.Find("id", nowitem));
                    //this.lY_MaterielRequirementsBindingSource.RemoveCurrent();

                    dgv.EndEdit();
                    lY_MaterielRequirementsBindingSource.EndEdit();



                    ///////////////////

                    this.Validate();


                    try
                    {
                        this.lY_MaterielRequirementsTableAdapter.Update(this.lYPlanMange.LY_MaterielRequirements);
                        tempcount = tempcount - 1;
                    }
                    catch (SqlException sqle)
                    {


                        MessageBox.Show(sqle.Message);
                    }
                    
                    //nowindex = this.lY_MaterielRequirementsBindingSource.Find("物料编码", nowitem);

                    //if (nowindex >= 0)
                    //{
                    //    this.lY_MaterielRequirementspurDataGridView.Rows[nowindex].Selected = true;
                    //}

                    //this.lY_MaterielRequirementsDataGridView.CurrentRow.i = this.lY_MaterielRequirementsDataGridView.Rows[nowindex];
                    //this.lY_MaterielRequirementsBindingSource.Position = nowindex;

                }

               // ////////////////////////////////////////////

               // foreach (DataGridViewRow dgr in dgv.Rows)
               // {


               //     if (true == dgr.Selected)
               //     {
               //         tempcount2 = tempcount2 + 1;
               //         //this.lY_MaterielRequirementsBindingSource.Position = this.lY_MaterielRequirementsBindingSource.Find("id", dgr.Cells[0].Value );

               //         lY_MaterielRequirementsBindingSource.RemoveAt(this.lY_MaterielRequirementsBindingSource.Find("id", dgr.Cells[0].Value));
               //         //this.lY_MaterielRequirementsBindingSource.RemoveCurrent();

               //         dgv.EndEdit();
               //         lY_MaterielRequirementsBindingSource.EndEdit();



               //         ///////////////////

               //         this.Validate();


               //         try
               //         {
               //             this.lY_MaterielRequirementsTableAdapter.Update(this.lYPlanMange.LY_MaterielRequirements);
               //             tempcount = tempcount - 1;
               //         }
               //         catch (SqlException sqle)
               //         {


               //             MessageBox.Show(sqle.Message);
               //         }
                        

                       
               //     }


               // }

               //////////////////////////////////////////

               // int aaa = tempcount;
                
                //this.lY_MaterielRequirementsBindingSource.RemoveCurrent();


                //dgv.EndEdit();
                //lY_MaterielRequirementsBindingSource.EndEdit();



                /////////////////////

                //this.Validate();


                //try
                //{
                //    this.lY_MaterielRequirementsTableAdapter.Update(this.lYPlanMange.LY_MaterielRequirements);
                //}
                //catch (SqlException sqle)
                //{


                //    MessageBox.Show(sqle.Message);
                //}
            }
        }

        private void bindingNavigatorAddNewItem5_Click(object sender, EventArgs e)
        {
            string planNum = this.ly_material_plan_mainDataGridView.CurrentRow.Cells["计划编号"].Value.ToString();

            if (Check_ifApproved(planNum))
            {

                MessageBox.Show("计划已经批准,不能修改数据...", "注意");
                return;
            }

            string fwhere  = "";

            if (3 == this.tabControl1.SelectedIndex)
            {
                fwhere = " where sort1='2'";
            }
            else if (4 == this.tabControl1.SelectedIndex)
            {
                fwhere = " where sort1='3'";
            }
            else if (5 == this.tabControl1.SelectedIndex)
            {
                fwhere = " where sort1='4'";
            }


            string sel = " SELECT   a.wzbh as 编号,a.mch as 名称,a.jph as 库位,a.xhc as 中方型号,a.xhc as 日方型号,a.gg as 规格,a.mch_jp as 简拼,a.mch_py as 拼音,b.sortname as 类别 FROM ly_inma0010 a left join ly_materrial_sort b on a.sort1=b.sortcode " + fwhere;

            QueryForm queryForm = new QueryForm();


            queryForm.Sel = sel;
            queryForm.Constr = SQLDatabase.Connectstring;

            //Set the Column Collection to the filter Table
            //queryForm.SetSourceColumns(this.billMainDataSet.BalanceBill.Columns);

            queryForm.ShowDialog();

            CountAlertPlanStruSingle(queryForm.Result);

            int parentId = int.Parse(this.ly_material_plan_mainDataGridView.CurrentRow.Cells["id"].Value.ToString());

            if (3 == this.tabControl1.SelectedIndex)
            {
                this.lY_MaterielRequirementsTableAdapter.Fill(this.lYPlanMange.LY_MaterielRequirements, parentId, "外协", "All");
            }
            else if (4 == this.tabControl1.SelectedIndex)
            {
                this.lY_MaterielRequirementsTableAdapter.Fill(this.lYPlanMange.LY_MaterielRequirements, parentId, "外购", "All");
            }
            else if (5 == this.tabControl1.SelectedIndex)
            {
                this.lY_MaterielRequirementsTableAdapter.Fill(this.lYPlanMange.LY_MaterielRequirements, parentId, "机加", "All");
            }

           

            //this.textBox3.Text = queryForm.Result;
        }

        private void toolStripButton41_Click(object sender, EventArgs e)
        {
            string message = "增加临时物料计划吗？";
            string caption = "提示...";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;

            DialogResult result;



            result = MessageBox.Show(message, caption, buttons,
            MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

            if (result == DialogResult.Yes)
            {
                this.ly_material_plan_mainBindingSource.AddNew();
                this.ly_material_plan_mainDataGridView.CurrentRow.Cells["计划编号"].Value = GetMaxPlanCode();
                this.ly_material_plan_mainDataGridView.CurrentRow.Cells["制定日期"].Value = SQLDatabase .GetNowdate();
                this.ly_material_plan_mainDataGridView.CurrentRow.Cells["制定人"].Value = SQLDatabase.nowUserName();
                this.ly_material_plan_mainDataGridView.CurrentRow.Cells["is_alertPlan"].Value = 1;
                this.ly_material_plan_mainDataGridView.CurrentRow.Cells["is_tempPlan"].Value = 1;
                this.ly_material_plan_mainDataGridView.CurrentRow.Cells["说明"].Value = "临时计划" + DateTime.Today.Date.ToString("yyyyMMdd");
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

        private void  UpdateRequirement(string nowitemno,int nowid,string nowstyle,string nowvalue)
        {
            if (string.IsNullOrEmpty(nowvalue)) return;
            
            SqlConnection sqlConnection1 = new SqlConnection(SQLDatabase.Connectstring);
            SqlCommand cmd = new SqlCommand();


            cmd.Parameters.Add("@nowitemno", SqlDbType.VarChar);
            cmd.Parameters["@nowitemno"].Value = nowitemno;

            cmd.Parameters.Add("@nowid", SqlDbType.Int );
            cmd.Parameters["@nowid"].Value = nowid;

            cmd.Parameters.Add("@changestyle", SqlDbType.VarChar);
            cmd.Parameters["@changestyle"].Value = nowstyle;

            cmd.Parameters.Add("@newvalue", SqlDbType.VarChar);
            cmd.Parameters["@newvalue"].Value = nowvalue;


            cmd.CommandText = "LY_UpdateRequirement";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = sqlConnection1;

            sqlConnection1.Open();
           int aaa= cmd.ExecuteNonQuery();
            sqlConnection1.Close();



          
        }

        private void lY_MaterielRequirementsDataGridView_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {

            DataGridView dgv = sender as DataGridView;
            if (null == dgv.CurrentRow) return;
            string itemno = dgv.CurrentRow.Cells["itemnoout"].Value.ToString();
            int nowid= int.Parse (dgv.CurrentRow.Cells["idout"].Value.ToString());
            //int loanid = int.Parse(this.xD_Client_LoanDataGridView.CurrentRow.Cells["Id_loan"].Value.ToString());

            if ("生产欠件WX" == dgv.CurrentCell.OwningColumn.Name)
            {



                GetItemNot(itemno);


               // UpdateRequirement(itemno, nowid, "sort", queryForm.Result);
            }
            ////////////////////////////////////////////////////////

            if ("外协未到WX" == dgv.CurrentCell.OwningColumn.Name || "采购未到WX" == dgv.CurrentCell.OwningColumn.Name || "机加在制WX" == dgv.CurrentCell.OwningColumn.Name)
            {

                QuertDetail(itemno);

            }
            ////////////////////////////////////////////////////////
            if ("类别wx" == dgv.CurrentCell.OwningColumn.Name)
            {

                string sel = "SELECT distinct sortcode as 代码,sortname as 种类 FROM ly_materrial_sort  order by sortcode";


                QueryForm queryForm = new QueryForm();


                queryForm.Sel = sel;
                queryForm.Constr = SQLDatabase.Connectstring;

                //Set the Column Collection to the filter Table
                //queryForm.SetSourceColumns(this.billMainDataSet.BalanceBill.Columns);

                queryForm.ShowDialog();


                UpdateRequirement(itemno, nowid, "sort", queryForm.Result);
            }

            //采购执行部门设置
            if (SQLDatabase.CheckHaveRight(SQLDatabase.NowUserID, "采购执行部门设置"))
            {
                if ("执行部门wx" == dgv.CurrentCell.OwningColumn.Name || "部门名称wx" == dgv.CurrentCell.OwningColumn.Name)
                {

                    string sel = "SELECT distinct bumenID as 代码,bumenMC as 名称 FROM T_bumen where parentID='00'  order by bumenID";


                    QueryForm queryForm = new QueryForm();


                    queryForm.Sel = sel;
                    queryForm.Constr = SQLDatabase.Connectstring;

                    //Set the Column Collection to the filter Table
                    //queryForm.SetSourceColumns(this.billMainDataSet.BalanceBill.Columns);

                    queryForm.ShowDialog();


                    UpdateRequirement(itemno, nowid, "exedept", queryForm.Result);
                }
            }
            //采购员设置
            if (SQLDatabase.CheckHaveRight(SQLDatabase.NowUserID, "物料采购员设置"))
            {
                if ("采购员wx" == dgv.CurrentCell.OwningColumn.Name || "采购员代码wx" == dgv.CurrentCell.OwningColumn.Name)
                {

                    string sel = "SELECT distinct yhbm as 代码,yhmc as 名称 FROM T_users order by yhbm";


                    QueryForm queryForm = new QueryForm();


                    queryForm.Sel = sel;
                    queryForm.Constr = SQLDatabase.Connectstring;

                    //Set the Column Collection to the filter Table
                    //queryForm.SetSourceColumns(this.billMainDataSet.BalanceBill.Columns);

                    queryForm.ShowDialog();


                    UpdateRequirement(itemno, nowid, "buyercode", queryForm.Result);
                }
            }

            int parentId = int.Parse(this.ly_material_plan_mainDataGridView.CurrentRow.Cells["id"].Value.ToString());
            string planNum = this.ly_material_plan_mainDataGridView.CurrentRow.Cells["计划编号"].Value.ToString();

            this.lY_MaterielRequirementsTableAdapter.Fill(this.lYPlanMange.LY_MaterielRequirements, parentId, "外协", "OWE");

            this.lY_MaterielRequirementsBindingSource.Position = this.lY_MaterielRequirementsBindingSource.Find("物料编码", itemno);

            //if (queryForm.DialogResult != DialogResult.Cancel)
            //{
            //    this.ly_inma0010ylTableAdapter.Fill(this.lYMaterialMange.ly_inma0010yl);

            //    this.ly_inma0010ylBindingSource.Position = this.ly_inma0010ylBindingSource.Find("物资编号", s);
            //}
        }

        private void QuertDetail(string itemno)
        {
            LY_MaterialTask_SumAnalysis queryForm = new LY_MaterialTask_SumAnalysis();



            queryForm.setFilter(itemno);
            queryForm.StartPosition = FormStartPosition.CenterParent;
            queryForm.ShowDialog(this);
        }

        private static void GetItemNot(string nowitemno)
        {
            string sel = "SELECT material_plan_num as 计划编码,plan_date as 计划日期, remark as 计划说明 ,itemno as 物料编码 ,absqty as 需求数量 ,out_qty as 已领数量 ,noget 未领数量 FROM ly_productMaterial_Noget  "
                        + " where itemno='" + nowitemno + "' order by  material_plan_num,itemno";


            QueryForm queryForm = new QueryForm();


            queryForm.Sel = sel;
            queryForm.Constr = SQLDatabase.Connectstring;

            //Set the Column Collection to the filter Table
            //queryForm.SetSourceColumns(this.billMainDataSet.BalanceBill.Columns);

            queryForm.ShowDialog();
        }

        private void lY_MaterielRequirementspurDataGridView_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            DataGridView dgv = sender as DataGridView;
            if (null == dgv.CurrentRow) return;
            string itemno = dgv.CurrentRow.Cells["itemnopur"].Value.ToString();
            int nowid = int.Parse(dgv.CurrentRow.Cells["idpur"].Value.ToString());
            //int loanid = int.Parse(this.xD_Client_LoanDataGridView.CurrentRow.Cells["Id_loan"].Value.ToString());

            //LY_MaterialAdd queryForm = new LY_MaterialAdd();

            //queryForm.statemode = "原料";
            //queryForm.runmode = "修改";
            //queryForm.material_code = itemno;

            //queryForm.StartPosition = FormStartPosition.CenterParent;
            //queryForm.ShowDialog();











            if ("生产欠件CG" == dgv.CurrentCell.OwningColumn.Name)
            {



                GetItemNot(itemno);


                // UpdateRequirement(itemno, nowid, "sort", queryForm.Result);
            }
            if ("外协未到CG" == dgv.CurrentCell.OwningColumn.Name || "采购未到CG" == dgv.CurrentCell.OwningColumn.Name || "机加在制CG" == dgv.CurrentCell.OwningColumn.Name)
            {

                QuertDetail(itemno);

            }
            ////////////////////////////////////////////////////////

            if ("类别cg" == dgv.CurrentCell.OwningColumn.Name)
            {

                string sel = "SELECT distinct sortcode as 代码,sortname as 种类 FROM ly_materrial_sort  order by sortcode";


                QueryForm queryForm = new QueryForm();


                queryForm.Sel = sel;
                queryForm.Constr = SQLDatabase.Connectstring;

                //Set the Column Collection to the filter Table
                //queryForm.SetSourceColumns(this.billMainDataSet.BalanceBill.Columns);

                queryForm.ShowDialog();


                UpdateRequirement(itemno, nowid, "sort", queryForm.Result);
            }
            if (SQLDatabase.CheckHaveRight(SQLDatabase.NowUserID, "采购执行部门设置"))
            {
                if ("执行部门cg" == dgv.CurrentCell.OwningColumn.Name || "部门名称cg" == dgv.CurrentCell.OwningColumn.Name)
                {

                    string sel = "SELECT distinct bumenID as 代码,bumenMC as 名称 FROM T_bumen where parentID='00'  order by bumenID";


                    QueryForm queryForm = new QueryForm();


                    queryForm.Sel = sel;
                    queryForm.Constr = SQLDatabase.Connectstring;

                    //Set the Column Collection to the filter Table
                    //queryForm.SetSourceColumns(this.billMainDataSet.BalanceBill.Columns);

                    queryForm.ShowDialog();


                    UpdateRequirement(itemno, nowid, "exedept", queryForm.Result);
                }
            }

            //采购员设置
            if (SQLDatabase.CheckHaveRight(SQLDatabase.NowUserID, "物料采购员设置"))
            {
                if ("采购员cg" == dgv.CurrentCell.OwningColumn.Name || "采购员代码cg" == dgv.CurrentCell.OwningColumn.Name)
                {

                    string sel = @" select distinct yhbm as 代码,yhmc as 名称 from T_users
                                where(bumen   like '0004%' and yhbm not in ('002', '011', '625', '916', '931', '009', '903', '905', '901', '912', '913', '969', '918')) order by yhbm";


                    QueryForm queryForm = new QueryForm();


                    queryForm.Sel = sel;
                    queryForm.Constr = SQLDatabase.Connectstring;

                    //Set the Column Collection to the filter Table
                    //queryForm.SetSourceColumns(this.billMainDataSet.BalanceBill.Columns);

                    queryForm.ShowDialog();


                    UpdateRequirement(itemno, nowid, "buyercode", queryForm.Result);
                }
            }






            //-------------------------------------------------------------------------------------------------------

            if ("当前计划需求cg" == dgv.CurrentCell.OwningColumn.Name)
            { 
                string planNum2 = this.ly_material_plan_mainDataGridView.CurrentRow.Cells["计划编号"].Value.ToString();

                if (Check_ifApproved(planNum2))
                {

                    MessageBox.Show("计划已经批准,不能修改数据...", "注意");
                    return;
                }

                if (null == this.lY_MaterielRequirementspurDataGridView.CurrentRow) return;

                if (!string.IsNullOrEmpty(lY_MaterielRequirementspurDataGridView.CurrentRow.Cells["已排跟单cg"].Value.ToString()))
                {
                    if (decimal.Parse(lY_MaterielRequirementspurDataGridView.CurrentRow.Cells["已排跟单cg"].Value.ToString()) > 0)
                    {
                        MessageBox.Show("已排跟单,不能修改数据...", "注意");
                        return;
                    }
                }
                string nowoperptar = this.lY_MaterielRequirementspurDataGridView.CurrentRow.Cells["计划员1"].Value.ToString();

                if (nowoperptar != SQLDatabase.nowUserName())
                {
                    MessageBox.Show("请计划员:" + nowoperptar + "修改", "注意");
                    return;
                }

                string id = this.lY_MaterielRequirementspurDataGridView.CurrentRow.Cells["idpur"].Value.ToString();

                ChangeValue queryForm = new ChangeValue();

                queryForm.OldValue = dgv.CurrentCell.Value.ToString();
                queryForm.NewValue = "";
                queryForm.ChangeMode = "value";
                queryForm.ShowDialog(); 


                if (queryForm.NewValue != "")
                {
                    string sql = "update LY_MaterielRequirements set  plancount= " + queryForm.NewValue + " WHERE id =" + id;

                    SqlConnection sqlConnection1 = new SqlConnection(SQLDatabase.Connectstring);
                    SqlCommand cmd = new SqlCommand(); 
                    cmd.CommandText = sql;
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

                            this.lY_MaterielRequirementspurDataGridView.CurrentRow.Cells["当前计划需求cg"].Value = queryForm.NewValue;
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
                return;
               
            }

            //----------------------------------------------------------------













            int parentId = int.Parse(this.ly_material_plan_mainDataGridView.CurrentRow.Cells["id"].Value.ToString());
            string planNum = this.ly_material_plan_mainDataGridView.CurrentRow.Cells["计划编号"].Value.ToString();

            this.lY_MaterielRequirementsTableAdapter.Fill(this.lYPlanMange.LY_MaterielRequirements, parentId, "外购", "OWE");

            this.lY_MaterielRequirementsBindingSource.Position = this.lY_MaterielRequirementsBindingSource.Find("物料编码", itemno);


             

        }

        private void lY_MaterielRequirementsMashineDataGridView_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            DataGridView dgv = sender as DataGridView;
            if (null == dgv.CurrentRow) return;
            string itemno = dgv.CurrentRow.Cells["itemnomachine"].Value.ToString();
            int nowid = int.Parse(dgv.CurrentRow.Cells["idmachine"].Value.ToString());
            //int loanid = int.Parse(this.xD_Client_LoanDataGridView.CurrentRow.Cells["Id_loan"].Value.ToString());

            if ("生产欠件JJ" == dgv.CurrentCell.OwningColumn.Name)
            {



                GetItemNot(itemno);


                // UpdateRequirement(itemno, nowid, "sort", queryForm.Result);
            }
            if ("外协未到JJ" == dgv.CurrentCell.OwningColumn.Name || "采购未到JJ" == dgv.CurrentCell.OwningColumn.Name || "机加在制JJ" == dgv.CurrentCell.OwningColumn.Name)
            {

                QuertDetail(itemno);

            }
            ////////////////////////////////////////////////////////

            if ("类别jj" == dgv.CurrentCell.OwningColumn.Name)
            {

                string sel = "SELECT distinct sortcode as 代码,sortname as 种类 FROM ly_materrial_sort  order by sortcode";


                QueryForm queryForm = new QueryForm();


                queryForm.Sel = sel;
                queryForm.Constr = SQLDatabase.Connectstring;

                //Set the Column Collection to the filter Table
                //queryForm.SetSourceColumns(this.billMainDataSet.BalanceBill.Columns);

                queryForm.ShowDialog();


                UpdateRequirement(itemno, nowid, "sort", queryForm.Result);
            }
            if (SQLDatabase.CheckHaveRight(SQLDatabase.NowUserID, "采购执行部门设置"))
            {
                if ("执行部门jj" == dgv.CurrentCell.OwningColumn.Name || "部门名称jj" == dgv.CurrentCell.OwningColumn.Name)
                {

                    string sel = "SELECT distinct bumenID as 代码,bumenMC as 名称 FROM T_bumen where parentID='00'  order by bumenID";


                    QueryForm queryForm = new QueryForm();


                    queryForm.Sel = sel;
                    queryForm.Constr = SQLDatabase.Connectstring;

                    //Set the Column Collection to the filter Table
                    //queryForm.SetSourceColumns(this.billMainDataSet.BalanceBill.Columns);

                    queryForm.ShowDialog();


                    UpdateRequirement(itemno, nowid, "exedept", queryForm.Result);
                }
            }


            int parentId = int.Parse(this.ly_material_plan_mainDataGridView.CurrentRow.Cells["id"].Value.ToString());
            string planNum = this.ly_material_plan_mainDataGridView.CurrentRow.Cells["计划编号"].Value.ToString();

            this.lY_MaterielRequirementsTableAdapter.Fill(this.lYPlanMange.LY_MaterielRequirements, parentId, "机加", "OWE");

            this.lY_MaterielRequirementsBindingSource.Position = this.lY_MaterielRequirementsBindingSource.Find("物料编码", itemno);
      
        }

        private void toolStripButtonUndo_Click(object sender, EventArgs e)
        {

            //foreach (DataGridViewRow dgr in lY_MaterielRequirementsDataGridView.Rows)
            //{


            //    bool ttt = dgr.Selected;


            //}

            if (SQLDatabase.NowUserID != "000")
            {
                MessageBox.Show("无权查看修改记录", "注意");
                return;
            }

            if (null == this.ly_material_plan_mainDataGridView.CurrentRow) return;

            int parentId = int.Parse(this.ly_material_plan_mainDataGridView.CurrentRow.Cells["id"].Value.ToString());
            string planNum = this.ly_material_plan_mainDataGridView.CurrentRow.Cells["计划编号"].Value.ToString();
            
            
            LY_MaterielRequirementsRecord queryForm = new LY_MaterielRequirementsRecord();



            //queryForm.OwnerForm = this;
            queryForm.NowparentId = parentId;
            queryForm.Nowsortname = "外协";

            queryForm.StartPosition = FormStartPosition.CenterParent;
            queryForm.ShowDialog();

            //queryForm.ShowDialog();

            this.lY_MaterielRequirementsTableAdapter.Fill(this.lYPlanMange.LY_MaterielRequirements, parentId, "外协", "All");

            this.lY_MaterielRequirementsBindingSource.Position = this.lY_MaterielRequirementsBindingSource.Count - 1;
            int nowindex;
            foreach (string nowitem in queryForm.Itemlist)
            {
               nowindex= this.lY_MaterielRequirementsBindingSource.Find("物料编码", nowitem);

               this.lY_MaterielRequirementsDataGridView.Rows[nowindex].Selected = true;

               //this.lY_MaterielRequirementsDataGridView.CurrentRow.i = this.lY_MaterielRequirementsDataGridView.Rows[nowindex];
               //this.lY_MaterielRequirementsBindingSource.Position = nowindex;
            
            }


            //if (queryForm.DialogResult != DialogResult.Cancel)
            //{
            //    //this.ly_inma0010ylTableAdapter.Fill(this.lYMaterialMange.ly_inma0010yl);
            //    //this.ly_inma0010ylBindingSource.Position = this.ly_inma0010ylBindingSource.Find("物资编号", queryForm.material_code);
            //}
           
        }

        private void toolStripButton42_Click(object sender, EventArgs e)
        {
            if (null == this.ly_material_plan_mainDataGridView.CurrentRow) return;

            if (SQLDatabase.NowUserID != "000")
            {
                MessageBox.Show("无权查看修改记录", "注意");
                return;
            }


            int parentId = int.Parse(this.ly_material_plan_mainDataGridView.CurrentRow.Cells["id"].Value.ToString());
            string planNum = this.ly_material_plan_mainDataGridView.CurrentRow.Cells["计划编号"].Value.ToString();


            LY_MaterielRequirementsRecord queryForm = new LY_MaterielRequirementsRecord();



            //queryForm.OwnerForm = this;
            queryForm.NowparentId = parentId;
            queryForm.Nowsortname = "外购";

            queryForm.StartPosition = FormStartPosition.CenterParent;
            queryForm.ShowDialog();

            this.lY_MaterielRequirementsTableAdapter.Fill(this.lYPlanMange.LY_MaterielRequirements, parentId, "外购", "All");

            this.lY_MaterielRequirementsBindingSource.Position = this.lY_MaterielRequirementsBindingSource.Count - 1;
            int nowindex;
            foreach (string nowitem in queryForm.Itemlist)
            {
                nowindex = this.lY_MaterielRequirementsBindingSource.Find("物料编码", nowitem);

                if (nowindex >= 0)
                {
                    this.lY_MaterielRequirementspurDataGridView.Rows[nowindex].Selected = true;
                }

                //this.lY_MaterielRequirementsDataGridView.CurrentRow.i = this.lY_MaterielRequirementsDataGridView.Rows[nowindex];
                //this.lY_MaterielRequirementsBindingSource.Position = nowindex;

            }
        }

        private void toolStripButton43_Click(object sender, EventArgs e)
        {
            if (null == this.ly_material_plan_mainDataGridView.CurrentRow) return;

            if (SQLDatabase.NowUserID != "000")
            {
                MessageBox.Show("无权查看修改记录", "注意");
                return;
            }


            int parentId = int.Parse(this.ly_material_plan_mainDataGridView.CurrentRow.Cells["id"].Value.ToString());
            string planNum = this.ly_material_plan_mainDataGridView.CurrentRow.Cells["计划编号"].Value.ToString();


            LY_MaterielRequirementsRecord queryForm = new LY_MaterielRequirementsRecord();



            //queryForm.OwnerForm = this;
            queryForm.NowparentId = parentId;
            queryForm.Nowsortname = "机加";

            queryForm.StartPosition = FormStartPosition.CenterParent;
            queryForm.ShowDialog(this);

            this.lY_MaterielRequirementsTableAdapter.Fill(this.lYPlanMange.LY_MaterielRequirements, parentId, "机加", "All");

            this.lY_MaterielRequirementsBindingSource.Position = this.lY_MaterielRequirementsBindingSource.Count - 1;
            int nowindex;
            foreach (string nowitem in queryForm.Itemlist)
            {
                nowindex = this.lY_MaterielRequirementsBindingSource.Find("物料编码", nowitem);

                this.lY_MaterielRequirementsMashineDataGridView.Rows[nowindex].Selected = true;

                //this.lY_MaterielRequirementsDataGridView.CurrentRow.i = this.lY_MaterielRequirementsDataGridView.Rows[nowindex];
                //this.lY_MaterielRequirementsBindingSource.Position = nowindex;

            }
        }

        private void 物料需求生成无警戒ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (null == this.ly_material_plan_mainDataGridView.CurrentRow) return;

            if (this.formState != "View") return;

            string isAlertplan = this.ly_material_plan_mainDataGridView.CurrentRow.Cells["is_alertPlan"].Value.ToString();

            if ("True" == isAlertplan) return;

            int parentId = int.Parse(this.ly_material_plan_mainDataGridView.CurrentRow.Cells["id"].Value.ToString());
            string planNum = this.ly_material_plan_mainDataGridView.CurrentRow.Cells["计划编号"].Value.ToString();





            //////////////////////////////////

            string message = "生成物料需求吗？";
            string caption = "提示...";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;

            DialogResult result;



            result = MessageBox.Show(message, caption, buttons,
            MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

            if (result == DialogResult.Yes)
            {
                // this.Invalidate();
                //  this.timer1.Interval = 1000;

                //  myIncrease = new IncreaseHandle(frmWaiting.Increase);

                NewFrm.Show(this);
                Thread.Sleep(500);
                //this.UseWaitCursor = true;
                //this.Enabled = false;
                //Thread thdSub = new Thread(new ThreadStart(ThreadFun));
                //thdSub.IsBackground = true;
                //thdSub.Start();　

                //int parentId = int.Parse(this.ly_material_plan_mainDataGridView.CurrentRow.Cells["id"].Value.ToString());
                //string planNum = this.ly_material_plan_mainDataGridView.CurrentRow.Cells["计划编号"].Value.ToString();

                // frmWaiting.Show(this);

                //timer1.Start();
                //myProcessBar = new NewfrmWaiting();//　Init　increase　event　　

                // myProcessBar.ShowDialog();




                SqlConnection sqlConnection1 = new SqlConnection(SQLDatabase.Connectstring);
                SqlCommand cmd = new SqlCommand();



                cmd.Parameters.Add("@planId", SqlDbType.Int);
                cmd.Parameters["@planId"].Value = parentId;

                cmd.Parameters.Add("@operator", SqlDbType.VarChar);
                cmd.Parameters["@operator"].Value = SQLDatabase.nowUserName();

                cmd.Parameters.Add("@isalert", SqlDbType.VarChar);
                cmd.Parameters["@isalert"].Value = "no";



                cmd.CommandText = "LY_MaterielRequirementsPlan";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = sqlConnection1;
                cmd.CommandTimeout = 0;

                sqlConnection1.Open();
                cmd.ExecuteNonQuery();
                sqlConnection1.Close();


                ly_material_plan_explodeTableAdapter.Fill(this.lYPlanMange.ly_material_plan_explode, parentId);
                this.ly_store_planitemcountTableAdapter.Fill(this.lYPlanMange.ly_store_planitemcount, parentId);
                this.ly_plan_getmaterial_departmentTableAdapter.Fill(this.lYPlanMange.ly_plan_getmaterial_department, planNum);

               NewFrm.Hide(this);

                //this.UseWaitCursor = false ;
                //this.Enabled = true;
                //thdSub.Abort ();　
                //usetime = 0;
                //timer1.Stop();
                // frmWaiting.Hide(this);
                // myProcessBar = null;
            }
        }

        private void ly_material_plan_detailDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void toolStripButton44_Click(object sender, EventArgs e)
        {

            string planNum1 = this.ly_material_plan_mainDataGridView.CurrentRow.Cells["计划编号"].Value.ToString();

            if (Check_ifApproved(planNum1))
            {

                MessageBox.Show("计划已经批准,不能修改数据...", "注意");
                return;
            }
            
            string message1 = "按照确认欠件数量计算机加原料需求,继续吗？";
            string caption1 = "提示...";
            MessageBoxButtons buttons1 = MessageBoxButtons.YesNo;

            DialogResult result1;



            result1 = MessageBox.Show(message1, caption1, buttons1,
            MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

            if (result1 == DialogResult.Yes)
            {

                int parentId = int.Parse(this.ly_material_plan_mainDataGridView.CurrentRow.Cells["id"].Value.ToString());
                string planNum = this.ly_material_plan_mainDataGridView.CurrentRow.Cells["计划编号"].Value.ToString();
                
                //NewFrm.Show(this); 2018-04-03
              


                SqlConnection sqlConnection1 = new SqlConnection(SQLDatabase.Connectstring);
                SqlCommand cmd = new SqlCommand();



                cmd.Parameters.Add("@planId", SqlDbType.Int);
                cmd.Parameters["@planId"].Value = parentId;

                cmd.Parameters.Add("@operator", SqlDbType.VarChar);
                cmd.Parameters["@operator"].Value = SQLDatabase.nowUserName();

                //cmd.Parameters.Add("@isalert", SqlDbType.VarChar);
                //cmd.Parameters["@isalert"].Value = "no";



                cmd.CommandText = "LY_PlanExplode_Machine";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = sqlConnection1;
                cmd.CommandTimeout = 0;

                sqlConnection1.Open();
                cmd.ExecuteNonQuery();
                sqlConnection1.Close();


                //ly_material_plan_explodeTableAdapter.Fill(this.lYPlanMange.ly_material_plan_explode, parentId);
                //this.ly_store_planitemcountTableAdapter.Fill(this.lYPlanMange.ly_store_planitemcount, parentId);
                //this.ly_plan_getmaterial_departmentTableAdapter.Fill(this.lYPlanMange.ly_plan_getmaterial_department, planNum);

                //NewFrm.Hide(this);2018-04-03
               




            }
        }

        private void lY_MaterielRequirementsDataGridView_SelectionChanged(object sender, EventArgs e)
        {
            SetDisplayColumn(sender as DataGridView);
        }

        private void SetDisplayColumn(DataGridView sender)
        {
            DataGridView dgv = sender;

            if (null == dgv.CurrentRow) return;

            string nowgeometry;

            if (!string.IsNullOrEmpty(dgv.CurrentRow.Cells["料型"].Value.ToString()))
            {
                nowgeometry = dgv.CurrentRow.Cells["料型"].Value.ToString();
            }
            else
            {
                nowgeometry = "ELSE";
            }

            if ("ELSE" == nowgeometry)
            {
                dgv.Columns["直径"].Visible = false;
                dgv.Columns["宽度"].Visible = false;
                dgv.Columns["长度"].Visible = false;
                dgv.Columns["高度"].Visible = false;
               // dgv.Columns["input_count"].Visible = false;
                dgv.Columns["比重"].Visible = false;
                //dgv.Columns["out_count"].Visible = false;
                //dgv.Columns["tec_qty"].Visible = false;


            }

            if ("棒料" == nowgeometry)
            {
                dgv.Columns["直径"].Visible = true ;
                dgv.Columns["宽度"].Visible = false;
                dgv.Columns["长度"].Visible = true ;
                dgv.Columns["高度"].Visible = false;
                // dgv.Columns["input_count"].Visible = false;
                dgv.Columns["比重"].Visible = true ;
            }

            if ("板料" == nowgeometry)
            {
                

                dgv.Columns["直径"].Visible = false;
                dgv.Columns["宽度"].Visible = true ;
                dgv.Columns["长度"].Visible = true ;
                dgv.Columns["高度"].Visible = true ;
               
                dgv.Columns["比重"].Visible = true ;
            }
        }

        private void toolStripButton45_Click(object sender, EventArgs e)
        {
            if (null == this.ly_material_plan_detailDataGridView.CurrentRow) return;



            //string balanceFlag = this.xD_Sel_SellBalanceDataGridView.CurrentRow.Cells["结算"].Value.ToString();



            //NewFrm.Show(this); 2018-04-03


            BaseReportView queryForm = new BaseReportView();

            queryForm.Text = "中原精密生产计划表";

            queryForm.Printdata = this.lYPlanMange;

            queryForm.PrintCrystalReport = new LY_ProductReport_Main();

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

            //NewFrm.Hide(this);2018-04-03

            queryForm.ShowDialog();
        }

        private void ly_material_plan_explodeDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void toolStripButton46_Click(object sender, EventArgs e)
        {
            ExportDataGridviewTOExcell.ExportDataGridview(this.ly_store_planitemcountDataGridView1, true);
        }

        private Boolean Check_ifApproved(string  noPlanNum)
        {
            SqlConnection sqlConnection1 = new SqlConnection(SQLDatabase.Connectstring);
            SqlCommand cmd = new SqlCommand();

            Boolean Approve_flag = true;

            cmd.Parameters.Add("@NowPlannum", SqlDbType.VarChar);
            cmd.Parameters["@NowPlannum"].Value = noPlanNum;


            cmd.CommandText = "LY_GetPlan_Approve";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = sqlConnection1;

            sqlConnection1.Open();
            Approve_flag = Boolean.Parse ( cmd.ExecuteScalar().ToString());
            sqlConnection1.Close();



            return Approve_flag;

        }


        private void ly_material_plan_mainDataGridView_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            DataGridView dgv = sender as DataGridView;

            if (null == dgv.CurrentRow) return;

            if (!SQLDatabase.CheckHaveRight(SQLDatabase.NowUserID, "生产计划审定"))
            {
                MessageBox.Show("无批准权限...", "注意");

                return;
            
            }

            //if (this.ly_sales_contract_mainDataGridView.CurrentRow.Cells["录入人c"].Value.ToString() != SQLDatabase.nowUserName())
            //{

            //    MessageBox.Show("请" + this.ly_sales_contract_mainDataGridView.CurrentRow.Cells["录入人c"].Value.ToString() + "修改...", "注意");

            //    return;
            //}


            if ("批准" == dgv.CurrentCell.OwningColumn.Name)
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

                if ("True" == dgv.CurrentRow.Cells["批准"].Value.ToString())
                {
                    dgv.CurrentRow.Cells["批准"].Value = "False";
                    
                    dgv.CurrentRow.Cells["批准人"].Value = DBNull.Value;
                    dgv.CurrentRow.Cells["批准日期"].Value = DBNull.Value;
                  
                }
                else
                {

                    dgv.CurrentRow.Cells["批准"].Value = "True";
                  
                    dgv.CurrentRow.Cells["批准人"].Value = SQLDatabase.nowUserName();
                    dgv.CurrentRow.Cells["批准日期"].Value = SQLDatabase.GetNowdate();
                 
                }


                this.ly_material_plan_mainDataGridView.EndEdit();
                this.ly_material_plan_mainBindingSource.EndEdit();

                this.ly_material_plan_mainTableAdapter.Update(this.lYPlanMange.ly_material_plan_main);



                return;

            }
            ////////////////////////////////////////////////////////////////////////
        }

        private void 批量指定执行部门ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!SQLDatabase.CheckHaveRight(SQLDatabase.NowUserID, "采购执行部门设置"))
            {
                return;
            }


            //if (SQLDatabase.CheckHaveRight(SQLDatabase.NowUserID, "物料采购员设置"))
            //{
            //    if ("采购员cg" == dgv.CurrentCell.OwningColumn.Name || "采购员代码cg" == dgv.CurrentCell.OwningColumn.Name)
            //    {

            //        string sel = "SELECT distinct yhbm as 代码,yhmc as 名称 FROM T_users order by yhbm";


            //        QueryForm queryForm = new QueryForm();


            //        queryForm.Sel = sel;
            //        queryForm.Constr = SQLDatabase.Connectstring;

            //        //Set the Column Collection to the filter Table
            //        //queryForm.SetSourceColumns(this.billMainDataSet.BalanceBill.Columns);

            //        queryForm.ShowDialog();


            //        UpdateRequirement(itemno, nowid, "buyercode", queryForm.Result);
            //    }
            //}
            //////////////////////////

            DataGridView dgv = this.lY_MaterielRequirementsDataGridView;

            if (3 == this.tabControl1.SelectedIndex)
            {
                dgv = this.lY_MaterielRequirementsDataGridView;
                if (null == dgv.CurrentRow) return;
            }
            if (4 == this.tabControl1.SelectedIndex)
            {
                dgv = this.lY_MaterielRequirementspurDataGridView;
                if (null == dgv.CurrentRow) return;
            }
            if (5 == this.tabControl1.SelectedIndex)
            {
                dgv = this.lY_MaterielRequirementsMashineDataGridView;
                if (null == dgv.CurrentRow) return;

            }





            string sel = "SELECT distinct bumenID as 代码,bumenMC as 名称 FROM T_bumen where parentID='00'  order by bumenID";


            QueryForm queryForm = new QueryForm();


            queryForm.Sel = sel;
            queryForm.Constr = SQLDatabase.Connectstring;

            //Set the Column Collection to the filter Table
            //queryForm.SetSourceColumns(this.billMainDataSet.BalanceBill.Columns);

            queryForm.ShowDialog();

            string itemno = "";
            int nowid = -1;
            this.itemlist.Clear();

            foreach (DataGridViewRow dgr in dgv.Rows)
            {
                if (true == dgr.Selected)
                {

                    this.itemlist.Add(int.Parse(dgr.Cells[0].Value.ToString()));


                    if (3 == this.tabControl1.SelectedIndex)
                    {
                        itemno = dgr.Cells["itemnoout"].Value.ToString();
                        nowid = int.Parse(dgr.Cells["idout"].Value.ToString());
                    }
                    if (4 == this.tabControl1.SelectedIndex)
                    {
                        itemno = dgr.Cells["itemnopur"].Value.ToString();
                        nowid = int.Parse(dgr.Cells["idpur"].Value.ToString());

                    }
                    if (5 == this.tabControl1.SelectedIndex)
                    {
                        itemno = dgr.Cells["itemnomachine"].Value.ToString();
                        nowid = int.Parse(dgr.Cells["idmachine"].Value.ToString());

                    }

                    UpdateRequirement(itemno, nowid, "exedept", queryForm.Result);

                }
            }




            int parentId = int.Parse(this.ly_material_plan_mainDataGridView.CurrentRow.Cells["id"].Value.ToString());
            string planNum = this.ly_material_plan_mainDataGridView.CurrentRow.Cells["计划编号"].Value.ToString();

            int nowindex = 0;

            if (3 == this.tabControl1.SelectedIndex)
            {
                this.lY_MaterielRequirementsTableAdapter.Fill(this.lYPlanMange.LY_MaterielRequirements, parentId, "外协", "OWE");


                foreach (int nowitem in itemlist)
                {
                    nowindex = this.lY_MaterielRequirementsBindingSource.Find("id", nowitem);
                    dgv.Rows[nowindex].Selected = true;
                }

            }
            if (4 == this.tabControl1.SelectedIndex)
            {
                this.lY_MaterielRequirementsTableAdapter.Fill(this.lYPlanMange.LY_MaterielRequirements, parentId, "外购", "OWE");

                foreach (int nowitem in itemlist)
                {
                    nowindex = this.lY_MaterielRequirementsBindingSource.Find("id", nowitem);
                    dgv.Rows[nowindex].Selected = true;
                }
            }
            if (5 == this.tabControl1.SelectedIndex)
            {
                this.lY_MaterielRequirementsTableAdapter.Fill(this.lYPlanMange.LY_MaterielRequirements, parentId, "机加", "OWE");

                foreach (int nowitem in itemlist)
                {
                    nowindex = this.lY_MaterielRequirementsBindingSource.Find("id", nowitem);
                    dgv.Rows[nowindex].Selected = true;
                }

            }


            



        }

        private void ly_material_plan_explodeDataGridView_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            string planNum = this.ly_material_plan_mainDataGridView.CurrentRow.Cells["计划编号"].Value.ToString();

            if (Check_ifApproved(planNum))
            {

                MessageBox.Show("计划已经批准,不能修改数据...", "注意");
                return;
            }

            DataGridView dgv = sender as DataGridView;







            if ("种类1" == dgv.CurrentCell.OwningColumn.Name)
            {          
                if (SQLDatabase.CheckHaveRight(SQLDatabase.NowUserID, "物料来源设置"))
                {

                }
                else
                {
                    MessageBox.Show("无物料种类设置权限", "注意");
                    return;
                }
                

                string s = dgv.CurrentRow.Cells["itemno"].Value.ToString();
                string sel = "SELECT distinct sortcode as 代码,sortname as 种类 FROM ly_materrial_sort  order by sortcode";
                QueryForm queryForm = new QueryForm();
                queryForm.Sel = sel;
                queryForm.Constr = SQLDatabase.Connectstring;
                queryForm.ShowDialog();




                if (queryForm.Result != "")
                {
                    dgv.CurrentRow.Cells["种类1"].Value = queryForm.Result1;
                    dgv.CurrentRow.Cells["sort1"].Value = queryForm.Result;
                    this.ly_material_plan_explodeBindingSource.EndEdit();
                    this.ly_material_plan_explodeTableAdapter.Update(this.lYPlanMange.ly_material_plan_explode);
                    SaveDetailsort(s, queryForm.Result);



                }
                return;

            }
        }
    
        private void 批量进行代料ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string planNum = this.ly_material_plan_mainDataGridView.CurrentRow.Cells["计划编号"].Value.ToString();
            string planid = this.ly_material_plan_mainDataGridView.CurrentRow.Cells["id"].Value.ToString();
            if (Check_ifApproved(planNum))
            {

                MessageBox.Show("计划已经批准,不能修改数据...", "注意");
                return;
            }
            if (null == ly_material_plan_explodeDataGridView.CurrentRow) return;

            string planitemno = this.ly_material_plan_explodeDataGridView.CurrentRow.Cells["plan_itemno"].Value.ToString(); //顶级物料

            //string levels =Convert.ToInt32( this.ly_material_plan_explodeDataGridView.CurrentRow.Cells["层次"].Value.ToString()); //层次
            //if (levels < 1)
            //{
            //    MessageBox.Show("被代物料必须大于1层...", "注意");
            //    return;
            //}



            string nowitemno = this.ly_material_plan_explodeDataGridView.CurrentRow.Cells["物料编码"].Value.ToString();

            string sel = "SELECT  wzbh as 编码,mch as 名称, xhc as 型号, gg as 规格,mch_py, mch_jp FROM ly_inma0010 " +
                         "   order by wzbh";



            QueryForm queryForm = new QueryForm();


            queryForm.Sel = sel;
            queryForm.Constr = SQLDatabase.Connectstring;
            queryForm.Nodiscol = 4;
           
            queryForm.ShowDialog();


            if (string.IsNullOrEmpty(queryForm.Result))
            {
                return;
            }
            string message1 = "用新物料:" + queryForm.Result + " 替换结构明细中的所有编码为：" + nowitemno + "，继续吗？";
            string caption1 = "提示...";
            MessageBoxButtons buttons1 = MessageBoxButtons.YesNo;

            DialogResult result1;



            result1 = MessageBox.Show(message1, caption1, buttons1,
            MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

            if (result1 == DialogResult.Yes)
            {


                SqlConnection sqlConnection1 = new SqlConnection(SQLDatabase.Connectstring);
                SqlCommand cmd = new SqlCommand();

 

                cmd.Parameters.Add("@ori_temno", SqlDbType.VarChar);
                cmd.Parameters["@ori_temno"].Value = nowitemno;

                cmd.Parameters.Add("@replace_itemno", SqlDbType.VarChar);
                cmd.Parameters["@replace_itemno"].Value = queryForm.Result;



                cmd.Parameters.Add("@plan_itemno", SqlDbType.VarChar);
                cmd.Parameters["@plan_itemno"].Value = planitemno;

                cmd.Parameters.Add("@plan_id", SqlDbType.Int);
                cmd.Parameters["@plan_id"].Value = planid;


                cmd.CommandText = "LY_Product_ReplaceItem";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = sqlConnection1;

                sqlConnection1.Open();
                cmd.ExecuteNonQuery();
                sqlConnection1.Close();

                ly_material_plan_explodeTableAdapter.Fill(this.lYPlanMange.ly_material_plan_explode,int.Parse( planid));
            }

            MessageBox.Show("替换完成,请检查替换结果...", "注意");
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void toolStripButton47_Click(object sender, EventArgs e)
        {

            return;
            // 批量导入物料
            string sql = @" SELECT [序号] ,[物料编号] FROM [dbo].[LY_CountAlert_Plan_More] where 实际订单数量 is not null";

            DataTable dt = null;
            using (SqlConnection connection = new SqlConnection(SQLDatabase.Connectstring))
            {

                SqlDataAdapter adapter = new SqlDataAdapter(sql, connection);
                DataSet ds = new DataSet();
                adapter.Fill(ds);
                dt = ds.Tables[0];
            }
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                CountAlertPlanStruSingle(dt.Rows[i][1].ToString().Trim());
               
            }
            MessageBox.Show("成功...", "注意");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.ly_material_plan_mainTableAdapter.Fill(this.lYPlanMange.ly_material_plan_main, "SCJH");
        }

        private void 批量指定采购员ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!SQLDatabase.CheckHaveRight(SQLDatabase.NowUserID, "物料采购员设置"))
            {
                return;
            }

            DataGridView dgv = this.lY_MaterielRequirementsDataGridView;

            if (3 == this.tabControl1.SelectedIndex)
            {
                dgv = this.lY_MaterielRequirementsDataGridView;
                if (null == dgv.CurrentRow) return;
            }
            if (4 == this.tabControl1.SelectedIndex)
            {
                dgv = this.lY_MaterielRequirementspurDataGridView;
                if (null == dgv.CurrentRow) return;
            }
            if (5 == this.tabControl1.SelectedIndex)
            {
                return;

            }





            //string sel = "SELECT distinct yhbm as 代码,yhmc as 名称 FROM T_users order by yhbm";
            string sel = @"select yhbm as 工号, yhmc as 姓名 from T_users_Purchase_View ";

            QueryForm queryForm = new QueryForm();


            queryForm.Sel = sel;
            queryForm.Constr = SQLDatabase.Connectstring;

            //Set the Column Collection to the filter Table
            //queryForm.SetSourceColumns(this.billMainDataSet.BalanceBill.Columns);

            queryForm.ShowDialog();

            string itemno = "";
            int nowid = -1;
            this.itemlist.Clear();

            foreach (DataGridViewRow dgr in dgv.Rows)
            {
                if (true == dgr.Selected)
                {

                    this.itemlist.Add(int.Parse(dgr.Cells[0].Value.ToString()));


                    if (3 == this.tabControl1.SelectedIndex)
                    {
                        itemno = dgr.Cells["itemnoout"].Value.ToString();
                        nowid = int.Parse(dgr.Cells["idout"].Value.ToString());
                    }
                    if (4 == this.tabControl1.SelectedIndex)
                    {
                        itemno = dgr.Cells["itemnopur"].Value.ToString();
                        nowid = int.Parse(dgr.Cells["idpur"].Value.ToString());

                    }
                    //if (5 == this.tabControl1.SelectedIndex)
                    //{
                    //    itemno = dgr.Cells["itemnomachine"].Value.ToString();
                    //    nowid = int.Parse(dgr.Cells["idmachine"].Value.ToString());

                    //}

                    UpdateRequirement(itemno, nowid, "buyercode", queryForm.Result);

                }
            }




            int parentId = int.Parse(this.ly_material_plan_mainDataGridView.CurrentRow.Cells["id"].Value.ToString());
            string planNum = this.ly_material_plan_mainDataGridView.CurrentRow.Cells["计划编号"].Value.ToString();

            int nowindex = 0;

            if (3 == this.tabControl1.SelectedIndex)
            {
                this.lY_MaterielRequirementsTableAdapter.Fill(this.lYPlanMange.LY_MaterielRequirements, parentId, "外协", "OWE");


                foreach (int nowitem in itemlist)
                {
                    nowindex = this.lY_MaterielRequirementsBindingSource.Find("id", nowitem);
                    dgv.Rows[nowindex].Selected = true;
                }

            }
            if (4 == this.tabControl1.SelectedIndex)
            {
                this.lY_MaterielRequirementsTableAdapter.Fill(this.lYPlanMange.LY_MaterielRequirements, parentId, "外购", "OWE");

                foreach (int nowitem in itemlist)
                {
                    nowindex = this.lY_MaterielRequirementsBindingSource.Find("id", nowitem);
                    dgv.Rows[nowindex].Selected = true;
                }
            }
            //if (5 == this.tabControl1.SelectedIndex)
            //{
            //    this.lY_MaterielRequirementsTableAdapter.Fill(this.lYPlanMange.LY_MaterielRequirements, parentId, "机加", "OWE");

            //    foreach (int nowitem in itemlist)
            //    {
            //        nowindex = this.lY_MaterielRequirementsBindingSource.Find("id", nowitem);
            //        dgv.Rows[nowindex].Selected = true;
            //    }

            //}
        }

        private void ly_store_planitemcountDataGridView1_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            DataGridView dgv = sender as DataGridView;
            if (null == dgv.CurrentRow) return;
            string itemno = dgv.CurrentRow.Cells["物料编码P"].Value.ToString();
            //int nowid = int.Parse(dgv.CurrentRow.Cells["idpur"].Value.ToString());
           

            if ("计划未领" == dgv.CurrentCell.OwningColumn.Name)
            {



                GetItemNot(itemno);


                // UpdateRequirement(itemno, nowid, "sort", queryForm.Result);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string sql = @"select material_plan_num  from  ly_material_plan_main where id<15143 and id> 5351 and isnull(plan_preparemateriel,0)= 1";
            DataTable dt = null;
            using (SqlConnection connection = new SqlConnection(SQLDatabase.Connectstring))
            {

                SqlDataAdapter adapter = new SqlDataAdapter(sql, connection);
                DataSet ds = new DataSet();
                adapter.Fill(ds);
                dt = ds.Tables[0];
            }
            NewFrm.Show(this);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                clear("外协", dt.Rows[i]["material_plan_num"].ToString());
                clear("外购", dt.Rows[i]["material_plan_num"].ToString());
                clear("机加", dt.Rows[i]["material_plan_num"].ToString());
            }

            NewFrm.Hide(this);


        }

        protected void clear(string sortname,string plan_num)
        {

            SqlConnection sqlConnection1 = new SqlConnection(SQLDatabase.Connectstring);
            SqlCommand cmd = new SqlCommand();



            cmd.Parameters.Add("@sortname", SqlDbType.VarChar);
            cmd.Parameters["@sortname"].Value = sortname;

            cmd.Parameters.Add("@plan_num", SqlDbType.VarChar);
            cmd.Parameters["@plan_num"].Value = plan_num;



            cmd.CommandText = "clearPlan";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = sqlConnection1;

            sqlConnection1.Open();
            cmd.ExecuteNonQuery();
            sqlConnection1.Close();


        }
    }
}
