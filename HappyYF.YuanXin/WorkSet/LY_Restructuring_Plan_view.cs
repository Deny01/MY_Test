using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Transactions;
using HappyYF.Infrastructure.Repositories;
using DataGridFilter;

namespace HappyYF.YuanXin.WorkSet
{
    public partial class LY_Restructuring_Plan_view: Form
    {
         string formState = "View";

         public LY_Restructuring_Plan_view()
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


            this.ly_Restructuring_request_standardTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            this.ly_Restructuring_return_standardTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;

            this.ly_material_plan_mainTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            this.lY_restructuring_material_selTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;


            this.ly_restructuring_plan_detail_AlldeptTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;

           






            //if (!SQLDatabase.CheckHaveRight(SQLDatabase.NowUserID, "改制领料审批"))
            //{
            //    this.ly_material_plan_mainBindingSource.Filter = "制定人='" + SQLDatabase.nowUserName()+"'";
            //    //this.完成CheckBox.Visible = false;

            //}


            this.ly_material_plan_mainTableAdapter.Fill(this.lYPlanMange.ly_material_plan_main,"GZJH");

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


                ly_material_plan_mainDataGridView.Enabled = true;

                //button8.Enabled = false;


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


                ly_material_plan_mainDataGridView.Enabled = false ;

                //button8.Enabled = true;
            }


        }

        private string GetMaxPlanCode()
        {
            SqlConnection sqlConnection1 = new SqlConnection(SQLDatabase.Connectstring);
            SqlCommand cmd = new SqlCommand();

            string MaxPlanCode = "";

            cmd.Parameters.Add("@Plan_mode", SqlDbType.VarChar);
            cmd.Parameters["@Plan_mode"].Value = "GZJH";


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
            string message = "增加改制计划吗？";
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

            if (this.ly_restructuring_plan_detailDataGridView.RowCount > 0)
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

                this.lY_restructuring_material_selBindingSource.Filter = dFilter;
            else
                this.lY_restructuring_material_selBindingSource.Filter = " ";
        }

        private void toolStripTextBox2_Enter(object sender, EventArgs e)
        {
            toolStripTextBox2.Text = "";

            this.lY_restructuring_material_selBindingSource.Filter = "";
        }

        private void ly_material_plan_mainDataGridView_SelectionChanged(object sender, EventArgs e)
        {
            if (this.formState == "View")
            {
                if (null != this.ly_material_plan_mainDataGridView.CurrentRow)
                {
                    int parentId = int.Parse(this.ly_material_plan_mainDataGridView.CurrentRow.Cells["id"].Value.ToString());
                    string planNum = this.ly_material_plan_mainDataGridView.CurrentRow.Cells["计划编号"].Value.ToString();

                    
                    this.lY_restructuring_material_selTableAdapter.Fill(this.lYPlanMange.LY_restructuring_material_sel , parentId);

                   
                    this.ly_restructuring_plan_detail_AlldeptTableAdapter.Fill(this.lYProductMange.ly_restructuring_plan_detail_Alldept , parentId);



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

        }

        private void 删除子件ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (null == this.ly_restructuring_plan_detailDataGridView.CurrentRow) return;


            if (Check_ifApproved(this.ly_material_plan_mainDataGridView.CurrentRow.Cells["计划编号"].Value.ToString()))
            {

                MessageBox.Show("计划已经批准,不能改动...", "注意");
                return;
            }


            int nowId = int.Parse(this.ly_restructuring_plan_detailDataGridView.CurrentRow.Cells["id1"].Value.ToString());
            int parentId = int.Parse(this.ly_material_plan_mainDataGridView.CurrentRow.Cells["id"].Value.ToString());
            string componentNum = this.ly_restructuring_plan_detailDataGridView.CurrentRow.Cells["编号"].Value.ToString();


            string message1 = "当前(物料：" + componentNum + ")将被删除，继续吗？";
            string caption1 = "提示...";
            MessageBoxButtons buttons1 = MessageBoxButtons.YesNo;

            DialogResult result1;



            result1 = MessageBox.Show(message1, caption1, buttons1,
            MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

            if (result1 == DialogResult.Yes)
            {
                using (SqlConnection con = new SqlConnection(SQLDatabase.Connectstring))
                {
                    string sql = "select count(1) from dbo.ly_restructuring_task where restructuring_id ="+ nowId;

                    using (SqlCommand cmd2 = new SqlCommand(sql, con))
                    {

                        con.Open();
                        int k = Convert.ToInt32(cmd2.ExecuteScalar());
                        if (k > 0)
                        {
                            MessageBox.Show("已有改制任务单不可操作", "注意");
                            return;
                        }
                    }
                }

                

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
                    this.lY_restructuring_material_selTableAdapter.Fill(this.lYPlanMange.LY_restructuring_material_sel, parentId);

                    this.ly_restructuring_plan_detail_AlldeptTableAdapter.Fill(this.lYProductMange.ly_restructuring_plan_detail_Alldept , parentId);

                    this.lY_restructuring_material_selBindingSource.Position = this.lY_restructuring_material_selBindingSource.Find("物资编号", componentNum);

                    //CountPlanStru();
                }
                if (null == this.ly_restructuring_plan_detailDataGridView.CurrentRow)
                {
               

                    this.ly_Restructuring_request_standardTableAdapter.Fill(this.lYProductMange.ly_Restructuring_request_standard, -1);


                    this.ly_Restructuring_return_standardTableAdapter.Fill(this.lYProductMange.ly_Restructuring_return_standard, -1);

                }
            }
        }

        private void SaveChanged()
        {
            this.ly_restructuring_plan_detailDataGridView.EndEdit();


            this.ly_restructuring_plan_detail_AlldeptBindingSource.EndEdit();

            try
            {

                this.ly_restructuring_plan_detail_AlldeptTableAdapter.Update(this.lYProductMange.ly_restructuring_plan_detail_Alldept);

            }
            catch (SqlException sqle)
            {

                MessageBox.Show(sqle.Message, "注意");

            }

            if (null != this.ly_material_plan_mainDataGridView.CurrentRow)
            {
                int parentId = int.Parse(this.ly_material_plan_mainDataGridView.CurrentRow.Cells["id"].Value.ToString());
                string planNum = this.ly_material_plan_mainDataGridView.CurrentRow.Cells["计划编号"].Value.ToString();
                this.lY_restructuring_material_selTableAdapter.Fill(this.lYPlanMange.LY_restructuring_material_sel, parentId);
                this.ly_restructuring_plan_detail_AlldeptTableAdapter.Fill(this.lYProductMange.ly_restructuring_plan_detail_Alldept, parentId);

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

        private void ly_material_plan_detailDataGridView_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {

        }


        private static void InserPlanDetail(string componentNum, int parentId, decimal nowabsqty,string componentNum_Old)
        {
            string insStr = " INSERT INTO ly_material_plan_detail  " +
           "( plan_id,wzbh,plan_count,origin_itemno) " +
           " values ('" + parentId + "','" + componentNum + "'," + nowabsqty + ",'"+ componentNum_Old + "')";


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

        private static void InserPlanDetail_Orign(string componentNum, int parentId, decimal nowabsqty)
        {
            string insStr = " INSERT INTO ly_material_plan_detail  " +
           "( plan_id,origin_itemno,plan_count) " +
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
 
       

 

   
        private void toolStripButton9_Click(object sender, EventArgs e)
        {
            //ExportDataGridviewTOExcell.ExportDataGridview(this.ly_plan_getmaterialDataGridView, true);
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
            QueryForm queryForm = new QueryForm();


            queryForm.Sel = sel;
            queryForm.Constr = SQLDatabase.Connectstring;
            queryForm.Nodiscol = 4;
            //Set the Column Collection to the filter Table
            //queryForm.SetSourceColumns(this.billMainDataSet.BalanceBill.Columns);

            queryForm.ShowDialog();

            
        }

        private void ly_material_plan_mainDataGridView_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {

        }



        private void ly_restructuring_plan_detailDataGridView_SelectionChanged(object sender, EventArgs e)
        {
            if (null != this.ly_restructuring_plan_detailDataGridView.CurrentRow)
            {
                int parentId = int.Parse(this.ly_restructuring_plan_detailDataGridView.CurrentRow.Cells["id1"].Value.ToString());
                string planNum = this.ly_material_plan_mainDataGridView.CurrentRow.Cells["计划编号"].Value.ToString();


                this.ly_Restructuring_request_standardTableAdapter.Fill(this.lYProductMange.ly_Restructuring_request_standard, parentId);


                this.ly_Restructuring_return_standardTableAdapter.Fill(this.lYProductMange.ly_Restructuring_return_standard, parentId);

            }
            else
            {

                this.ly_Restructuring_request_standardTableAdapter.Fill(this.lYProductMange.ly_Restructuring_request_standard, -1);


                this.ly_Restructuring_return_standardTableAdapter.Fill(this.lYProductMange.ly_Restructuring_return_standard, -1);

            }
        }

        private void ly_material_plan_mainDataGridView_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView dgv = sender as DataGridView;

            if (null == dgv.CurrentRow) return;




            if ("plan_approve_two" == dgv.CurrentCell.OwningColumn.Name)
            {

                if (!string.IsNullOrEmpty(dgv.CurrentRow.Cells["plan_approve_people_two"].Value.ToString()))
                {
                    if (dgv.CurrentRow.Cells["plan_approve_people_two"].Value.ToString() != SQLDatabase.nowUserName())
                    {

                        MessageBox.Show("请" + dgv.CurrentRow.Cells["plan_approve_people_two"].Value.ToString() + "修改...", "注意");

                        return;
                    }
                }


                if ("True" == dgv.CurrentRow.Cells["plan_approve_two"].Value.ToString())
                {


                    dgv.CurrentRow.Cells["plan_approve_two"].Value = "False";

                    dgv.CurrentRow.Cells["plan_approve_people_two"].Value = DBNull.Value;
                    dgv.CurrentRow.Cells["plan_approve_date_two"].Value = DBNull.Value;

                }
                else
                {

                    dgv.CurrentRow.Cells["plan_approve_two"].Value = "True";

                    dgv.CurrentRow.Cells["plan_approve_people_two"].Value = SQLDatabase.nowUserName();
                    dgv.CurrentRow.Cells["plan_approve_date_two"].Value = SQLDatabase.GetNowdate();

                }


                this.ly_material_plan_mainDataGridView.EndEdit();
                this.ly_material_plan_mainBindingSource.EndEdit();

                this.ly_material_plan_mainTableAdapter.Update(this.lYPlanMange.ly_material_plan_main);



                return;
            }
        }
    }
}
