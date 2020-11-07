using HappyYF.Infrastructure.Repositories;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading;
using System.Windows.Forms;

namespace HappyYF.YuanXin.WorkSet
{
    public partial class store_out_tool : Form
    {
        string formState = "View";
        public store_out_tool()
        {
            InitializeComponent();
 
        }

        private void LY_MaterialTask_SumAnalysis_Load(object sender, EventArgs e)
        {
      
            this.ly_store_out_toolTotalTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            this.ly_store_out_toolTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            this.ly_plan_getmaterialPlan_ToolTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            this.ly_material_plan_mainTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;

            this.lY_material_sel_toolTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring; 


            if (!SQLDatabase.CheckHaveRight(SQLDatabase.NowUserID, "计划领料审批"))
            {
                this.ly_material_plan_mainBindingSource.Filter = "制定人='" + SQLDatabase.nowUserName() + "'";
            }
            else
            {
                if ("000" == SQLDatabase.NowUserID || "998" == SQLDatabase.NowUserID || "66" == SQLDatabase.NowUserID)
                {
                    this.ly_material_plan_mainBindingSource.Filter = "";
                }
                else if ("999" == SQLDatabase.NowUserID)
                {
                    this.ly_material_plan_mainBindingSource.Filter = "部门='0003'";
                }
                else
                {
                    this.ly_material_plan_mainBindingSource.Filter = "部门='" + SQLDatabase.nowUserDepartmentBig() + "'";
                }
            }

            this.ly_material_plan_mainTableAdapter.Fill(this.lYPlanMange.ly_material_plan_main, "GJJH");

            SetFormState("View");

        }

        private void toolStripButton22_Click(object sender, EventArgs e)
        {
        
            this.ly_store_out_toolTotalTableAdapter.Fill(this.lYPlanMange.ly_store_out_toolTotal);
          
        }



        private void toolStripTextBox3_KeyUp(object sender, KeyEventArgs e)
        {
            string filterString;


            filterString = GetDataGridviewMultiFilterString.DGVMultiFilterString(this.ly_material_task_viewNewDataGridView, this.toolStripTextBox3.Text); 
            this.lystoreouttoolTotalBindingSource.Filter = "(" + filterString + ")";
            
        }

 
        private void toolStripTextBox3_Enter(object sender, EventArgs e)
        {
            toolStripTextBox3.Text = "";

            this.lystoreouttoolTotalBindingSource.Filter = "";
            
        }

        private void toolStripButton17_Click(object sender, EventArgs e)
        {
            ExportDataGridviewTOExcell.ExportDataGridview(this.ly_material_task_viewNewDataGridView, true);
        }

        private void ly_material_task_viewNewDataGridView_SelectionChanged(object sender, EventArgs e)
        {
            if (ly_material_task_viewNewDataGridView.CurrentRow == null)
                return;
            string wzbh = this.ly_material_task_viewNewDataGridView.CurrentRow.Cells["物料编码"].Value.ToString();
            this.ly_store_out_toolTableAdapter.Fill(this.lYPlanMange.ly_store_out_tool,wzbh);
        }

        private void bindingNavigatorAddNewItem_Click(object sender, EventArgs e)
        {
            string message = "增加工具领用计划吗？";
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

                toolStripButton2.Enabled = true;
                bindingNavigatorAddNewItem.Enabled = true;
                ly_material_plan_mainBindingNavigatorSaveItem.Enabled = false;
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

                toolStripButton2.Enabled = false;
                bindingNavigatorAddNewItem.Enabled = false;
                ly_material_plan_mainBindingNavigatorSaveItem.Enabled = true;
                ly_material_plan_mainDataGridView.Enabled = false;


            }


        }
        private string GetMaxPlanCode()
        {
            SqlConnection sqlConnection1 = new SqlConnection(SQLDatabase.Connectstring);
            SqlCommand cmd = new SqlCommand();

            string MaxPlanCode = "";

            cmd.Parameters.Add("@Plan_mode", SqlDbType.VarChar);
            cmd.Parameters["@Plan_mode"].Value = "GJJH";


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

        private void ly_material_plan_mainBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.ly_material_plan_mainBindingSource.EndEdit();
            this.ly_material_plan_mainTableAdapter.Update(this.lYPlanMange.ly_material_plan_main);

            SetFormState("View");
        }

        private void ly_plan_getmaterialDataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
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
                     
                    dgv.CurrentRow.Cells["领料数量"].Value = queryForm.NewValue;

                    this.ly_plan_getmaterialDataGridView.EndEdit();  
                    this.lyplangetmaterialPlanToolBindingSource.EndEdit(); 
                    this.ly_plan_getmaterialPlan_ToolTableAdapter.Update(this.lYPlanMange.ly_plan_getmaterialPlan_Tool);

                }
              
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

                    this.lY_material_sel_toolTableAdapter.Fill(this.lYPlanMange.LY_material_sel_tool, parentId); 
                }
            }
        }

        private void lY_dayget_material_selDataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (null == this.ly_material_plan_mainDataGridView.CurrentRow) return;
            if (null == lY_dayget_material_selDataGridView.CurrentRow) return;

            if (Check_ifApproved(this.ly_material_plan_mainDataGridView.CurrentRow.Cells["计划编号"].Value.ToString()))
            {

                MessageBox.Show("计划已经批准,不能改动...", "注意");
                return;
            } 
            CountPlanStru();
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

            NewFrm.Show(this);

            SqlConnection sqlConnection1 = new SqlConnection(SQLDatabase.Connectstring);
            SqlCommand cmd = new SqlCommand();



            cmd.Parameters.Add("@planId", SqlDbType.Int);
            cmd.Parameters["@planId"].Value = parentId;

            cmd.Parameters.Add("@prod_dept", SqlDbType.VarChar);
            cmd.Parameters["@prod_dept"].Value = prod_dept;

            cmd.Parameters.Add("@itemno", SqlDbType.VarChar);
            cmd.Parameters["@itemno"].Value = componentNum;


            cmd.CommandText = "LY_Dayget_input_tool";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = sqlConnection1;

            sqlConnection1.Open();
            cmd.ExecuteNonQuery();
            sqlConnection1.Close();



            this.lY_material_sel_toolTableAdapter.Fill(this.lYPlanMange.LY_material_sel_tool, parentId);
            this.ly_plan_getmaterialPlan_ToolTableAdapter.Fill(this.lYPlanMange.ly_plan_getmaterialPlan_Tool, planNum);

            NewFrm.Hide(this);
        }

        private void toolStripTextBox6_Enter(object sender, EventArgs e)
        {
            toolStripTextBox6.Text = "";

            this.lYmaterialseltoolBindingSource.Filter = "";
        }

        private void toolStripTextBox6_KeyUp(object sender, KeyEventArgs e)
        {
            string filterString;


            filterString = GetDataGridviewMultiFilterString.DGVMultiFilterString(this.lY_dayget_material_selDataGridView, this.toolStripTextBox6.Text);
            this.lYmaterialseltoolBindingSource.Filter = "(" + filterString + ")";

        }
    }
}
