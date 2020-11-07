using HappyYF.Infrastructure.Repositories;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading;
using System.Windows.Forms;

namespace HappyYF.YuanXin.WorkSet
{
    public partial class store_out_tool_ck : Form
    {
        string formState = "View";
        public store_out_tool_ck()
        {
            InitializeComponent();

        }

        private void LY_MaterialTask_SumAnalysis_Load(object sender, EventArgs e)
        {


            this.ly_plan_getmaterialPlan_ToolTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            this.ly_material_plan_mainTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            this.ly_storeout_employWarehouseTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            this.ly_store_out_toolTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;

            this.ly_material_plan_mainTableAdapter.Fill(this.lYPlanMange.ly_material_plan_main, "GJJH");
            this.ly_material_plan_mainBindingSource.Filter = "启用='true'";
            SetFormState("View");

        }






        private void SetFormState(string state)
        {

            if ("View" == state)
            {
                this.formState = "View";

                this.制定日期DateTimePicker.Enabled = false;

                this.说明TextBox.ReadOnly = true;
                this.启用CheckBox.Enabled = false;

                this.bindingNavigatorMoveFirstItem.Enabled = true;
                this.bindingNavigatorMoveLastItem.Enabled = true;
                this.bindingNavigatorMoveNextItem.Enabled = true;
                this.bindingNavigatorMovePreviousItem.Enabled = true;
                this.bindingNavigatorPositionItem.Enabled = true;

                
                ly_material_plan_mainDataGridView.Enabled = true;

            }
            else
            {
                this.formState = "Edit";

                this.制定日期DateTimePicker.Enabled = true;

                this.说明TextBox.ReadOnly = false;
                this.bindingNavigatorMoveFirstItem.Enabled = false;
                this.bindingNavigatorMoveLastItem.Enabled = false;
                this.bindingNavigatorMoveNextItem.Enabled = false;
                this.bindingNavigatorMovePreviousItem.Enabled = false;
                this.bindingNavigatorPositionItem.Enabled = false;
 
                ly_material_plan_mainDataGridView.Enabled = false;


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


        private void ly_plan_getmaterialDataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (Check_ifApproved(this.ly_material_plan_mainDataGridView.CurrentRow.Cells["计划编号"].Value.ToString()))
            {

                MessageBox.Show("计划已经批准,不能改动...", "注意");
                return;
            }



        }

        private void ly_material_plan_mainDataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView dgv = sender as DataGridView;

            if (null == dgv.CurrentRow) return;

            if (!SQLDatabase.CheckHaveRight(SQLDatabase.NowUserID, "计划领料审批"))
            {
                MessageBox.Show("无批准权限...", "注意");
                return;

            }



        }

        private void ly_material_plan_mainDataGridView_SelectionChanged(object sender, EventArgs e)
        {

            if (this.formState == "View")
            {
                if (null != this.ly_material_plan_mainDataGridView.CurrentRow)
                {
                    int parentId = int.Parse(this.ly_material_plan_mainDataGridView.CurrentRow.Cells["id"].Value.ToString());
                    string planNum = this.ly_material_plan_mainDataGridView.CurrentRow.Cells["计划编号"].Value.ToString();
                    this.ly_plan_getmaterialPlan_ToolTableAdapter.Fill(this.lYPlanMange.ly_plan_getmaterialPlan_Tool, planNum);
                    this.groupBox3.Text = planNum + ":物料列表";
                    string dptCode = this.ly_material_plan_mainDataGridView.CurrentRow.Cells["部门码"].Value.ToString();
                    this.ly_storeout_employWarehouseTableAdapter.Fill(this.lYStoreMange.ly_storeout_employWarehouse, planNum, dptCode, SQLDatabase.NowUserID);
                    this.ly_store_out_toolTableAdapter.Fill(this.lYStoreMange.ly_store_out_tool,  SQLDatabase.nowUserName(), planNum);
                }
            }
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            if (null == this.ly_material_plan_mainDataGridView.CurrentRow) return;
            if (null == ly_plan_getmaterialDataGridView.CurrentRow) return;

 
            string message = "确定领料出库吗?";
            string caption = "提示...";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result;



            result = MessageBox.Show(message, caption, buttons,
            MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

            if (result == DialogResult.Yes)
            {

                CountStoreOutAuto();
            }
        }
        private void CountStoreOutAuto()
        {
            if (null == this.ly_material_plan_mainDataGridView.CurrentRow) return;
      
            string nowOutstyle = "工具出库";


            string planNum = this.ly_material_plan_mainDataGridView.CurrentRow.Cells["计划编号"].Value.ToString();
 
            int parentId = int.Parse(this.ly_material_plan_mainDataGridView.CurrentRow.Cells["id"].Value.ToString());
            string dptCode = this.ly_material_plan_mainDataGridView.CurrentRow.Cells["部门码"].Value.ToString();

            NewFrm.Show(this);

            Thread.Sleep(1000);
            SqlConnection sqlConnection1 = new SqlConnection(SQLDatabase.Connectstring);
            SqlCommand cmd = new SqlCommand();

            cmd.Parameters.Add("@plan_num", SqlDbType.VarChar);
            cmd.Parameters["@plan_num"].Value = planNum;
            cmd.Parameters.Add("@prod_dept", SqlDbType.VarChar);
            cmd.Parameters["@prod_dept"].Value = dptCode;
            cmd.Parameters.Add("@warehousename", SqlDbType.VarChar);
            cmd.Parameters["@warehousename"].Value = comboBox2.SelectedValue;
            string outNum = GetMaxOutNum();
            cmd.Parameters.Add("@out_number", SqlDbType.VarChar);
            cmd.Parameters["@out_number"].Value = outNum;
            cmd.Parameters.Add("@faliaoren", SqlDbType.VarChar);
            cmd.Parameters["@faliaoren"].Value = SQLDatabase.nowUserName();
            cmd.Parameters.Add("@nowoutstyle", SqlDbType.VarChar);
            cmd.Parameters["@nowoutstyle"].Value =  nowOutstyle;
            cmd.Parameters.Add("@nowsuboutstyle", SqlDbType.VarChar);
            cmd.Parameters["@nowsuboutstyle"].Value ="";
            cmd.CommandText = "LY_store_out_input_department_Tool";
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


            this.ly_store_out_toolTableAdapter.Fill(this.lYStoreMange.ly_store_out_tool, SQLDatabase.nowUserName(), planNum );
            this.ly_plan_getmaterialPlan_ToolTableAdapter.Fill(this.lYPlanMange.ly_plan_getmaterialPlan_Tool, planNum);

            NewFrm.Hide(this);
        }

        private string GetMaxOutNum()
        {
            SqlConnection sqlConnection1 = new SqlConnection(SQLDatabase.Connectstring);
            SqlCommand cmd = new SqlCommand();

            string MaxPlanCode = ""; 
            cmd.CommandText = "LY_Get_OutNumber_Tool";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = sqlConnection1;

            sqlConnection1.Open();
            MaxPlanCode = cmd.ExecuteScalar().ToString();
            sqlConnection1.Close();



            return MaxPlanCode;
        }

      

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            
                if (null != this.comboBox2.SelectedValue)
                {
                    if ("全部" != this.comboBox2.SelectedValue.ToString())
                    {
                       
                        this.lyplangetmaterialPlanToolBindingSource.Filter = "warehouse='" + this.comboBox2.SelectedValue.ToString() + "'";
                    }
                 
                }
          
        }

        private void 打印PToolStripButton_Click(object sender, EventArgs e)
        {
            if (null == this.ly_store_outDataGridView.CurrentRow) return; 

            string out_number = this.ly_store_outDataGridView.CurrentRow.Cells["out_number"].Value.ToString();



            NewFrm.Show(this); 
            BaseReportView queryForm = new BaseReportView();

            queryForm.Text = "中原精密生产领料单";

            queryForm.Printdata = this.lYStoreMange;

        
            queryForm.PrintCrystalReport = new LY_Lingliaodan_Tool();
            string selectFormula;

            selectFormula = "{ly_store_out_tool.out_number}  =   '" + out_number + "' ";
            queryForm.PrintCrystalReport.DataDefinition.RecordSelectionFormula = selectFormula;



            NewFrm.Hide(this);

            queryForm.ShowDialog();
        }
    }
}
