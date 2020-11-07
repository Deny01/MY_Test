using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Transactions;
using HappyYF.Infrastructure.Repositories;
using DataGridFilter;

namespace HappyYF.YuanXin.WorkSet
{
    public partial class LY_Gxh_Plan : Form
    {
        string formState = "View";

        public LY_Gxh_Plan()
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
            this.ly_plan_getmaterialPlanGxhOutTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            this.ly_plan_getmaterialPlanGxhInTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
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

            this.ly_material_plan_mainTableAdapter.Fill(this.lYPlanMange.ly_material_plan_main, "GXJH");

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
            cmd.Parameters["@Plan_mode"].Value = "GXJH";


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
                    this.lY_dayget_material_sel_newTableAdapter.Fill(this.lYPlanMange.LY_dayget_material_sel_new, parentId, SQLDatabase.nowUserDepartment());
                    this.ly_plan_getmaterialPlanGxhOutTableAdapter.Fill(this.lYPlanMange.ly_plan_getmaterialPlanGxhOut, planNum);
                    this.ly_plan_getmaterialPlanGxhInTableAdapter.Fill(this.lYPlanMange.ly_plan_getmaterialPlanGxhIn, planNum);
                    this.groupBox3.Text = planNum + ":物料列表";


                }
            }

        }

        private void ly_inma0010_planselDataGridView_DoubleClick(object sender, EventArgs e)
        {
            if (null == this.ly_material_plan_mainDataGridView.CurrentRow) return;
            if (null == lY_dayget_material_selDataGridView.CurrentRow) return;
            if (dataGridView1.Rows.Count > 0)
            {

                MessageBox.Show("已经有改型号物料存在...", "注意");
                return;
            }
            if (Check_ifApproved(this.ly_material_plan_mainDataGridView.CurrentRow.Cells["计划编号"].Value.ToString()))
            {

                MessageBox.Show("计划已经批准,不能改动...", "注意");
                return;
            }


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
                    this.ly_plan_getmaterialPlanGxhOutTableAdapter.Fill(this.lYPlanMange.ly_plan_getmaterialPlanGxhOut, planNum);
                    this.ly_plan_getmaterialPlanGxhInTableAdapter.Fill(this.lYPlanMange.ly_plan_getmaterialPlanGxhIn, planNum);
                    this.lY_dayget_material_sel_newBindingSource.Position = this.lY_dayget_material_sel_newBindingSource.Find("物资编号", componentNum);
                }

            }
        }

        private void SaveChanged()
        {
            this.ly_plan_getmaterialDataGridView.EndEdit();
            this.lyplangetmaterialPlanGxhOutBindingSource.EndEdit();
            this.ly_plan_getmaterialPlanGxhOutTableAdapter.Update(this.lYPlanMange.ly_plan_getmaterialPlanGxhOut);
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
                return;
                ChangeValue queryForm = new ChangeValue();

                queryForm.OldValue = dgv.CurrentCell.Value.ToString();
                queryForm.NewValue = "";
                queryForm.ChangeMode = "value";
                queryForm.ShowDialog();

                if (queryForm.NewValue != "")
                {
                    dgv.CurrentRow.Cells["领料数量"].Value = queryForm.NewValue;
                    SaveChanged();

                }
                return;

            }


            if ("新旧" == dgv.CurrentCell.OwningColumn.Name)
            {
                string sel = "SELECT oldnew as 新旧 FROM ly_finishproduct_newold  ";
                QueryForm queryForm = new QueryForm();
                queryForm.Sel = sel;
                queryForm.Constr = SQLDatabase.Connectstring;
                queryForm.ShowDialog();

                if (queryForm.Result != "")
                {
                    dgv.CurrentRow.Cells["新旧"].Value = queryForm.Result;
                    SaveChanged();

                }
                return;

            }


        }



        private void CountPlanStru()
        {
            if (null == this.ly_material_plan_mainDataGridView.CurrentRow) return;

            if ("True" == this.ly_material_plan_mainDataGridView.CurrentRow.Cells["启用"].Value.ToString())
            {
                MessageBox.Show("改型号计划已经审批,不能增加条目...", "注意");
                return;
            }

            int qty = 0;
            ChangeValue queryForm = new ChangeValue();
            queryForm.Text = "改型号数量";
            queryForm.ChangeMode = "value";
            queryForm.ShowDialog();

            if (queryForm.NewValue != "")
            {
                try
                {
                    qty = int.Parse(queryForm.NewValue);
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



            string planNum = this.ly_material_plan_mainDataGridView.CurrentRow.Cells["计划编号"].Value.ToString();

            string componentNum = this.lY_dayget_material_selDataGridView.CurrentRow.Cells["物资编号"].Value.ToString();
            int parentId = int.Parse(this.ly_material_plan_mainDataGridView.CurrentRow.Cells["id"].Value.ToString());
            string prod_dept = SQLDatabase.nowUserDepartmentBig();

            NewFrm.Show(this);

            SqlConnection sqlConnection1 = new SqlConnection(SQLDatabase.Connectstring);
            SqlCommand cmd = new SqlCommand();

            cmd.Parameters.Add("@number", SqlDbType.Int);
            cmd.Parameters["@number"].Value = qty;

            cmd.Parameters.Add("@planId", SqlDbType.Int);
            cmd.Parameters["@planId"].Value = parentId;

            cmd.Parameters.Add("@prod_dept", SqlDbType.VarChar);
            cmd.Parameters["@prod_dept"].Value = prod_dept;

            cmd.Parameters.Add("@itemno", SqlDbType.VarChar);
            cmd.Parameters["@itemno"].Value = componentNum;


            cmd.CommandText = "LY_Dayget_input_GXH";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = sqlConnection1;

            sqlConnection1.Open();
            cmd.ExecuteNonQuery();
            sqlConnection1.Close();



            this.lY_dayget_material_sel_newTableAdapter.Fill(this.lYPlanMange.LY_dayget_material_sel_new, parentId, SQLDatabase.nowUserDepartment());
            this.ly_plan_getmaterialPlanGxhOutTableAdapter.Fill(this.lYPlanMange.ly_plan_getmaterialPlanGxhOut, planNum);
            this.ly_plan_getmaterialPlanGxhInTableAdapter.Fill(this.lYPlanMange.ly_plan_getmaterialPlanGxhIn, planNum);
            NewFrm.Hide(this);
        }




        private void toolStripButton9_Click(object sender, EventArgs e)
        {
            ExportDataGridviewTOExcell.ExportDataGridview(this.ly_plan_getmaterialDataGridView, true);
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

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
             
            if (null == this.dataGridView1.CurrentRow) return;
            string jhbh = this.ly_material_plan_mainDataGridView.CurrentRow.Cells["计划编号"].Value.ToString();
            if (Check_ifApproved(jhbh))
            {

                MessageBox.Show("计划已经批准,不能改动...", "注意");
                return;
            }
            if ("True" == this.ly_material_plan_mainDataGridView.CurrentRow.Cells["启用"].Value.ToString())
            {
                MessageBox.Show("领料计划已经审批,不能删除条目...", "注意");
                return;
            }

            int parentId = int.Parse(this.ly_material_plan_mainDataGridView.CurrentRow.Cells["id"].Value.ToString());
            decimal k = 0;
            for (int i = 0; i < this.ly_plan_getmaterialDataGridView.Rows.Count; i++)
            {
                if (!string.IsNullOrEmpty(this.ly_plan_getmaterialDataGridView.Rows[i].Cells["已领数量"].Value.ToString()))
                {
                    k += (decimal.Parse(this.ly_plan_getmaterialDataGridView.Rows[i].Cells["已领数量"].Value.ToString()));

                }

            }
            if (0 < k)
            {
                MessageBox.Show("已有领料记录,不能删除条目...", "注意");
                return;
            }
            if (!string.IsNullOrEmpty(this.dataGridView1.CurrentRow.Cells["已入数量"].Value.ToString()))
            {
                k += (decimal.Parse(this.dataGridView1.CurrentRow.Cells["已入数量"].Value.ToString()));
            }
            if (0 < k)
            {
                MessageBox.Show("已有入库记录,不能删除条目...", "注意");
                return;
            }

            string message1 = "当前出入库物料将被删除，继续吗？";
            string caption1 = "提示...";
            MessageBoxButtons buttons1 = MessageBoxButtons.YesNo;

            DialogResult result1;



            result1 = MessageBox.Show(message1, caption1, buttons1,
            MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

            if (result1 == DialogResult.Yes)
            {

                string delstr = " delete ly_plan_getmaterial  where plan_id = " + parentId + "";

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
                    this.ly_plan_getmaterialPlanGxhOutTableAdapter.Fill(this.lYPlanMange.ly_plan_getmaterialPlanGxhOut, planNum);
                    this.ly_plan_getmaterialPlanGxhInTableAdapter.Fill(this.lYPlanMange.ly_plan_getmaterialPlanGxhIn, planNum);

                }

            }
        }
    }
}