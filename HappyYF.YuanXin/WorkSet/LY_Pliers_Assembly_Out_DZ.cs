using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Transactions;
using System.Windows.Forms;
using HappyYF.Infrastructure.Repositories;

namespace HappyYF.YuanXin.WorkSet
{
    public partial class LY_Pliers_Assembly_Out_DZ : Form
    {
        public LY_Pliers_Assembly_Out_DZ()
        {
            InitializeComponent();
        }

        private void LY_QualityInspection_Benchwork_Load(object sender, EventArgs e)
        {
            this.dateTimePicker1.Text = DateTime.Today.AddMonths(-3).Date.ToString();
            this.dateTimePicker2.Text = DateTime.Today.AddDays(0).Date.ToString();
            lY_productiontask_period_AllTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            ly_restructuring_employWarehouseTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;

            ly_Restructuring_outTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            ly_product_task_requestSingle_taskcodeTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            ly_product_task_returnSingle_taskcodeNewTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;

            ly_store_out_productionSingleTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            ly_store_in_production_returnSingleTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            ly_Restructuring_inTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;



            this.lY_productiontask_period_AllBindingSource.Filter = "跟单编号 like 'DZ%'";

            //.Connection.ConnectionString = SQLDatabase.Connectstring;
            this.lY_productiontask_period_AllTableAdapter.Fill(this.lYProductMange.LY_productiontask_period_All, this.dateTimePicker1.Value, this.dateTimePicker2.Value.AddDays(1), SQLDatabase.NowUserID);

            this.ly_restructuring_employWarehouseTableAdapter.Fill(this.lYSalseRepair.ly_restructuring_employWarehouse, SQLDatabase.NowUserID);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.lY_productiontask_period_AllBindingSource.Filter = "跟单编号 like 'DZ%'";
            this.lY_productiontask_period_AllTableAdapter.Fill(this.lYProductMange.LY_productiontask_period_All, this.dateTimePicker1.Value, this.dateTimePicker2.Value.AddDays(1), SQLDatabase.NowUserID);

        }

        private void toolStripTextBox2_KeyUp(object sender, KeyEventArgs e)
        {
            string dFilter = GetDataGridviewMultiFilterString.DGVMultiFilterString(this.ly_production_orderDataGridView, this.toolStripTextBox2.Text);

            if (null == dFilter)
                dFilter = "跟单编号 like 'DZ%'";

            this.lY_productiontask_period_AllBindingSource.Filter = dFilter;
        }

        private void toolStripTextBox2_Enter(object sender, EventArgs e)
        {
            toolStripTextBox2.Text = "";

            this.lY_productiontask_period_AllBindingSource.Filter = "跟单编号 like 'DZ%'";
        }

        private void ly_production_orderDataGridView_SelectionChanged(object sender, EventArgs e)
        {

            reLoad();
        }
        protected void reLoad()
        {
            if (null != this.ly_production_orderDataGridView.CurrentRow)
            {

                string nowproductionorderNum = this.ly_production_orderDataGridView.CurrentRow.Cells["任务单号"].Value.ToString();

                if (nowproductionorderNum.Contains("DZ"))
                {
                    this.lY_productiontask_period_AllBindingSource.Filter = "跟单编号 like 'DZ%'";
                }
                string workName = ly_production_orderDataGridView.CurrentRow.Cells["工人"].Value.ToString();
                ly_product_task_requestSingle_taskcodeTableAdapter.Fill(this.lYProductMange.ly_product_task_requestSingle_taskcode, nowproductionorderNum);
                ly_product_task_returnSingle_taskcodeNewTableAdapter.Fill(this.lYProductMange.ly_product_task_returnSingle_taskcodeNew, nowproductionorderNum);

                ly_store_out_productionSingleTableAdapter.Fill(this.lYSalseRepair.ly_store_out_productionSingle, workName, nowproductionorderNum, SQLDatabase.NowUserID);
                ly_store_in_production_returnSingleTableAdapter.Fill(this.lYSalseRepair.ly_store_in_production_returnSingle, workName, nowproductionorderNum, SQLDatabase.NowUserID);
            }
            else
            {
                ly_product_task_requestSingle_taskcodeTableAdapter.Fill(this.lYProductMange.ly_product_task_requestSingle_taskcode, "");
                ly_product_task_returnSingle_taskcodeNewTableAdapter.Fill(this.lYProductMange.ly_product_task_returnSingle_taskcodeNew, "");
                ly_store_out_productionSingleTableAdapter.Fill(this.lYSalseRepair.ly_store_out_productionSingle, "", "", "");
                ly_store_in_production_returnSingleTableAdapter.Fill(this.lYSalseRepair.ly_store_in_production_returnSingle, "", "", "");

            }

            if (null != this.ly_store_outnumDataGridView.CurrentRow)
            {

                string outNum = this.ly_store_outnumDataGridView.CurrentRow.Cells["out_number"].Value.ToString();


                this.ly_Restructuring_outTableAdapter.Fill(this.lYStoreMange.ly_Restructuring_out, outNum, SQLDatabase.nowUserName());
            }
            else
            {
                this.ly_Restructuring_outTableAdapter.Fill(this.lYStoreMange.ly_Restructuring_out, "", "");
            }
            if (null != this.dataGridView2.CurrentRow)
            {

                string outNum = this.dataGridView2.CurrentRow.Cells["out_number_new"].Value.ToString();


                this.ly_Restructuring_inTableAdapter.Fill(this.lYStoreMange.ly_Restructuring_in, outNum, SQLDatabase.nowUserName());
            }
            else
            {
                this.ly_Restructuring_inTableAdapter.Fill(this.lYStoreMange.ly_Restructuring_in, "", "");
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.comboBox2.SelectedValue == null)
            {
                return;
            }
            this.ly_product_task_requestSingle_taskcodeBindingSource.Filter = "warehouse='" + this.comboBox2.SelectedValue.ToString() + "'";
            this.ly_product_task_returnSingle_taskcodeNewBindingSource.Filter = "warehouse='" + this.comboBox2.SelectedValue.ToString() + "'";

            this.ly_store_out_productionSingleBindingSource.Filter = "warehouse='" + this.comboBox2.SelectedValue.ToString() + "'";
            this.ly_Restructuring_outBindingSource.Filter = "仓库类别='" + this.comboBox2.SelectedValue.ToString() + "'";
            this.ly_store_in_production_returnSingleBindingSource.Filter = "warehouse='" + this.comboBox2.SelectedValue.ToString() + "'";
            this.ly_Restructuring_inBindingSource.Filter = "仓库类别='" + this.comboBox2.SelectedValue.ToString() + "'";
        }

        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            if (ly_product_task_requestSingle_taskcodeDataGridView.Rows.Count <= 0)
            {
                MessageBox.Show("物料为空！"); return;
            }

            string message = "确定领料出库吗?";
            string caption = "提示...";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result;



            result = MessageBox.Show(message, caption, buttons,
            MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

            if (result == DialogResult.Yes)
            {

                CountStoreOutAutoSingle();
            }
        }




        private void CountStoreOutAutoSingle()
        {
            //---------------------------------------------------------------

            string id = ly_production_orderDataGridView.CurrentRow.Cells["id_Main_New"].Value.ToString();
            this.lY_productiontask_period_AllBindingSource.Filter = "跟单编号 like 'DZ%'";
            this.lY_productiontask_period_AllTableAdapter.Fill(this.lYProductMange.LY_productiontask_period_All, this.dateTimePicker1.Value, this.dateTimePicker2.Value.AddDays(1), SQLDatabase.NowUserID);

            this.lY_productiontask_period_AllBindingSource.Position = lY_productiontask_period_AllBindingSource.Find("id", id);

            //---------------------------------------------------------------
            string taskCode = ly_production_orderDataGridView.CurrentRow.Cells["任务单号"].Value.ToString();
            string workName = ly_production_orderDataGridView.CurrentRow.Cells["工人"].Value.ToString();
            string workCode = ly_production_orderDataGridView.CurrentRow.Cells["工号"].Value.ToString();


            SqlConnection sqlConnection1 = new SqlConnection(SQLDatabase.Connectstring);
            SqlCommand cmd = new SqlCommand();



            cmd.Parameters.Add("@pruductionOrder_num", SqlDbType.VarChar);
            cmd.Parameters["@pruductionOrder_num"].Value = taskCode;


            string outNum = GetMaxOutNum();
            cmd.Parameters.Add("@out_number", SqlDbType.VarChar);
            cmd.Parameters["@out_number"].Value = outNum;

            cmd.Parameters.Add("@faliaoren", SqlDbType.VarChar);
            cmd.Parameters["@faliaoren"].Value = SQLDatabase.nowUserName();

            cmd.Parameters.Add("@workname", SqlDbType.VarChar);
            cmd.Parameters["@workname"].Value = workName;


            cmd.Parameters.Add("@workCode", SqlDbType.VarChar);
            cmd.Parameters["@workCode"].Value = workCode;


            cmd.CommandText = "LY_store_out_productionSingle";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = sqlConnection1;

            sqlConnection1.Open();
            cmd.ExecuteNonQuery();
            sqlConnection1.Close();

            reLoad();

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

        private void ly_store_outnumDataGridView_SelectionChanged(object sender, EventArgs e)
        {
            if (null != this.ly_store_outnumDataGridView.CurrentRow)
            {

                string outNum = this.ly_store_outnumDataGridView.CurrentRow.Cells["out_number"].Value.ToString();


                this.ly_Restructuring_outTableAdapter.Fill(this.lYStoreMange.ly_Restructuring_out, outNum, SQLDatabase.nowUserName());
            }
        }

        private void dataGridView2_SelectionChanged(object sender, EventArgs e)
        {
            if (null != this.dataGridView2.CurrentRow)
            {

                string outNum = this.dataGridView2.CurrentRow.Cells["out_number_new"].Value.ToString();


                this.ly_Restructuring_inTableAdapter.Fill(this.lYStoreMange.ly_Restructuring_in, outNum, SQLDatabase.nowUserName());
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if (ly_product_task_returnSingle_taskcodeNewDataGridView.Rows.Count <= 0)
            {
                MessageBox.Show("物料为空！"); return;
            }

            string message = "确定退料入库吗?";
            string caption = "提示...";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result;



            result = MessageBox.Show(message, caption, buttons,
            MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

            if (result == DialogResult.Yes)
            {

                CountStoreInAutoSingle();
            }
        }
        private void CountStoreInAutoSingle()
        { //---------------------------------------------------------------

            string id = ly_production_orderDataGridView.CurrentRow.Cells["id_Main_New"].Value.ToString();
            this.lY_productiontask_period_AllBindingSource.Filter = "跟单编号 like 'DZ%'";
            this.lY_productiontask_period_AllTableAdapter.Fill(this.lYProductMange.LY_productiontask_period_All, this.dateTimePicker1.Value, this.dateTimePicker2.Value.AddDays(1), SQLDatabase.NowUserID);

            this.lY_productiontask_period_AllBindingSource.Position = lY_productiontask_period_AllBindingSource.Find("id", id);

            //---------------------------------------------------------------

            string taskCode = ly_production_orderDataGridView.CurrentRow.Cells["任务单号"].Value.ToString();
            string workName = ly_production_orderDataGridView.CurrentRow.Cells["工人"].Value.ToString();
            string workCode = ly_production_orderDataGridView.CurrentRow.Cells["工号"].Value.ToString();


            SqlConnection sqlConnection1 = new SqlConnection(SQLDatabase.Connectstring);
            SqlCommand cmd = new SqlCommand();



            cmd.Parameters.Add("@pruductionOrder_num", SqlDbType.VarChar);
            cmd.Parameters["@pruductionOrder_num"].Value = taskCode;


            string outNum = GetMaxOutNum();
            cmd.Parameters.Add("@out_number", SqlDbType.VarChar);
            cmd.Parameters["@out_number"].Value = outNum;

            cmd.Parameters.Add("@faliaoren", SqlDbType.VarChar);
            cmd.Parameters["@faliaoren"].Value = SQLDatabase.nowUserName();

            cmd.Parameters.Add("@workname", SqlDbType.VarChar);
            cmd.Parameters["@workname"].Value = workName;

            cmd.Parameters.Add("@workCode", SqlDbType.VarChar);
            cmd.Parameters["@workCode"].Value = workCode;


            cmd.CommandText = "LY_store_In_productionSingle";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = sqlConnection1;

            sqlConnection1.Open();
            cmd.ExecuteNonQuery();
            sqlConnection1.Close();

            reLoad();

        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            if (null == this.ly_store_outDataGridView.CurrentRow) return;


            BaseReportView queryForm = new BaseReportView();

            queryForm.Text = "中原精密追加领料单";

            queryForm.Printdata = this.lYStoreMange;


            queryForm.PrintCrystalReport = new LY_lingliaodan_ZJ();


            queryForm.ShowDialog();
        }

        private void toolStripButton17_Click(object sender, EventArgs e)
        {
            // 领料单
            if (null == this.dataGridView2.CurrentRow) return;


            BaseReportView queryForm = new BaseReportView();

            queryForm.Text = "中原精密追加退料单";

            queryForm.Printdata = this.lYStoreMange;


            queryForm.PrintCrystalReport = new LY_tuiliaodan_ZJ();


            queryForm.ShowDialog();
        }

     

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == 0)
            {
                tabControl2.SelectedIndex = 0;
            }
            else
            {
                tabControl2.SelectedIndex = 1;
            }
        }

        private void ly_store_outDataGridView_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            DataGridView dgv = sender as DataGridView;


            if (null == this.ly_store_outnumDataGridView.CurrentRow) return;
            if (null == this.ly_store_outDataGridView.CurrentRow) return;

            if (SQLDatabase.nowUserName() != ly_store_outnumDataGridView.CurrentRow.Cells["发料人"].Value.ToString())
            {
                MessageBox.Show("请发料人:" + ly_store_outnumDataGridView.CurrentRow.Cells["发料人"].Value.ToString() + " 修改");
                return;
            }


            if ("True" == ly_store_outnumDataGridView.CurrentRow.Cells["finished"].Value.ToString())
            {
                MessageBox.Show("已经签证,领料单不能修改...");
                return;

            }



            if ("领料数量" == dgv.CurrentCell.OwningColumn.Name)
            {

                if (dgv.CurrentRow.Cells["仓库类别"].Value.ToString() == "成品")
                {
                    MessageBox.Show("成品数量不可以修改...");
                    return;
                }

                ChangeValue queryForm = new ChangeValue();

                queryForm.OldValue = dgv.CurrentCell.Value.ToString();
                queryForm.NewValue = "";
                queryForm.ChangeMode = "value";
                queryForm.ShowDialog();


                if (queryForm.NewValue != "")
                {
                    if (decimal.Parse(queryForm.NewValue) <0)
                    {
                        MessageBox.Show("领料数量必须大于0..."); return;
                    }

                    dgv.CurrentRow.Cells["领料数量"].Value = queryForm.NewValue;

                }
                else
                {

                }


                this.ly_store_outDataGridView.EndEdit();
                this.Validate();
                this.ly_Restructuring_outBindingSource.EndEdit();
                this.ly_Restructuring_outTableAdapter.Update(this.lYStoreMange.ly_Restructuring_out);
                reLoad();
                return;

            }
        }

        private void dataGridView1_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            DataGridView dgv = sender as DataGridView;

            if (null == this.dataGridView2.CurrentRow) return;
            if (null == this.dataGridView1.CurrentRow) return;

            if (SQLDatabase.nowUserName() != dataGridView2.CurrentRow.Cells["收料人_T"].Value.ToString())
            {

                MessageBox.Show("请收料人:" + dataGridView2.CurrentRow.Cells["收料人_T"].Value.ToString() + " 修改");

                return;
            }


            if ("True" == dataGridView2.CurrentRow.Cells["签证_T"].Value.ToString())
            {
                MessageBox.Show("已经签证,退料单不能修改...");
                return;

            }




            if ("领料数量_T" == dgv.CurrentCell.OwningColumn.Name)
            {

                ChangeValue queryForm = new ChangeValue();

                queryForm.OldValue = dgv.CurrentCell.Value.ToString();
                queryForm.NewValue = "";
                queryForm.ChangeMode = "value";
                queryForm.ShowDialog();


                if (queryForm.NewValue != "")
                {
                    if (decimal.Parse(queryForm.NewValue) > 0)
                    {
                        MessageBox.Show("退料数量填写负数..."); return;
                    }

                    dgv.CurrentRow.Cells["领料数量_T"].Value = queryForm.NewValue;

                }
                else
                {

                }

                this.dataGridView1.EndEdit();
                this.Validate();
                this.ly_Restructuring_inBindingSource.EndEdit();
                this.ly_Restructuring_inTableAdapter.Update(this.lYStoreMange.ly_Restructuring_in);
                reLoad();
                if (dataGridView2.Rows.Count == 0)
                {
                    if (null != this.ly_production_orderDataGridView.CurrentRow)
                    {
                        string taskMumber = this.ly_production_orderDataGridView.CurrentRow.Cells["任务单号"].Value.ToString();

                        string workName = ly_production_orderDataGridView.CurrentRow.Cells["工人"].Value.ToString();
                        ly_store_in_production_returnSingleTableAdapter.Fill(this.lYSalseRepair.ly_store_in_production_returnSingle, workName, taskMumber, SQLDatabase.NowUserID);
                    }
                }

                return;
            }
        }

        private void bindingNavigatorDeleteItem_Click(object sender, EventArgs e)
        {
            if (null == this.ly_store_outnumDataGridView.CurrentRow) return;


            if (SQLDatabase.nowUserName() != ly_store_outnumDataGridView.CurrentRow.Cells["发料人"].Value.ToString())
            {

                MessageBox.Show("请发料人:" + ly_store_outnumDataGridView.CurrentRow.Cells["发料人"].Value.ToString() + " 删除");

                return;
            }

            string outnumber = this.ly_store_outnumDataGridView.CurrentRow.Cells["out_number"].Value.ToString();
            string nowwarehouse = this.ly_store_outnumDataGridView.CurrentRow.Cells["warehouse"].Value.ToString();

            if ("True" == ly_store_outnumDataGridView.CurrentRow.Cells["finished"].Value.ToString())
            {
                MessageBox.Show("已经签证,领料单不能删除...");
                return;

            }


            string message = "删除当前领料单:" + outnumber + "吗？";
            string caption = "提示...";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;

            DialogResult result;



            result = MessageBox.Show(message, caption, buttons,
            MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

            if (result == DialogResult.Yes)
            {
                string delstr = " delete ly_store_out  from ly_store_out left join ly_inma0010 on ly_store_out.wzbh=ly_inma0010.wzbh  " +
                   " where ly_store_out.out_number = '" + outnumber + "' and ly_inma0010.warehouse='" + nowwarehouse + "'";

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

                    this.ly_store_out_productionSingleBindingSource.RemoveCurrent();


                }
                reLoad();
                if (null == this.ly_store_outnumDataGridView.CurrentRow)
                {
                    this.ly_Restructuring_outTableAdapter.Fill(this.lYStoreMange.ly_Restructuring_out, "-1", "-1");
                }

            }
        }

        private void toolStripButton3_Click_1(object sender, EventArgs e)
        {
            if (null == this.dataGridView2.CurrentRow) return;


            if (SQLDatabase.nowUserName() != dataGridView2.CurrentRow.Cells["收料人_T"].Value.ToString())
            {

                MessageBox.Show("请收料人:" + dataGridView2.CurrentRow.Cells["收料人_T"].Value.ToString() + " 删除");

                return;
            }

            string outnumber = this.dataGridView2.CurrentRow.Cells["out_number_new"].Value.ToString();
            string nowwarehouse = this.dataGridView2.CurrentRow.Cells["仓库_T"].Value.ToString();

            if ("True" == dataGridView2.CurrentRow.Cells["签证_T"].Value.ToString())
            {
                MessageBox.Show("已经签证,退料单不能删除...");
                return;

            }


            string message = "删除当前退料单:" + outnumber + "吗？";
            string caption = "提示...";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;

            DialogResult result;



            result = MessageBox.Show(message, caption, buttons,
            MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

            if (result == DialogResult.Yes)
            {
                string delstr = " delete ly_store_out  from ly_store_out left join ly_inma0010 on ly_store_out.wzbh=ly_inma0010.wzbh  " +
                   " where ly_store_out.out_number = '" + outnumber + "' and ly_inma0010.warehouse='" + nowwarehouse + "'";

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

                    this.ly_store_in_production_returnSingleBindingSource.RemoveCurrent();

                }
                reLoad();
                if (null == this.dataGridView2.CurrentRow)
                {
                    this.ly_Restructuring_inTableAdapter.Fill(this.lYStoreMange.ly_Restructuring_in, "-1", "-1");

                }
            }
        }
    }
}
