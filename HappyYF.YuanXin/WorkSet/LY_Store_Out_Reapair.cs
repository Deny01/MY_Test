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

namespace HappyYF.YuanXin.WorkSet
{
    public partial class LY_Store_Out_Reapair : Form
    {
        string formState = "View";
        
        public LY_Store_Out_Reapair()
        {
            InitializeComponent();
        }

        private void LY_Store_Out_Reapair_Load(object sender, EventArgs e)
        {
              this.dateTimePicker1.Text = DateTime.Today.AddMonths(-6).Date.ToString();
              this.dateTimePicker2.Text = DateTime.Today.AddDays(1).Date.ToString();


              this.ly_storeout_employWarehouseTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;      
              this.ly_store_outTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;      
              this.ly_store_outnumTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;

             //this.ly_store_outnumRepairreturnTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
             //this.ly_store_innum_repairreturnTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;

              this.ly_store_outRepairReturnTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
              this.ly_store_outnumRepairreturnTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;


             //this.ly_store_outRepairReturnTableAdapter.Fill(this.lYStoreMange.ly_store_outRepairReturn, out_numberToolStripTextBox.Text, yonghu_nameToolStripTextBox.Text);
             //this.ly_store_outnumRepairreturnTableAdapter.Fill(this.lYSalseRepair.ly_store_outnumRepairreturn, receive_codeToolStripTextBox.Text, worknameToolStripTextBox.Text, yonghu_codeToolStripTextBox.Text);
            
            
            
          
           
              this.ly_sales_receive_itemDetail_repairTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            this.ly_sales_receive_itemDetail_repair_returnINTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;

            
              this.ly_sales_receive_itemTaskTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;

            this.ly_sales_receive_itemTaskBindingSource.Filter = "repair_sector='公司维修'";
            this.ly_sales_receive_itemTaskTableAdapter.Fill(this.lYSalseRepair.ly_sales_receive_itemTask, this.dateTimePicker1.Value, this.dateTimePicker2.Value);


              //    this.ly_salesrepair_outMainTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;

              //this.ly_salesrepair_outMainTableAdapter.Fill(this.lYSalseRepair.ly_salesrepair_outMain, this.dateTimePicker1.Value, this.dateTimePicker2.Value);

              this.radioButton1.Checked = true;

              SetFormState("View");

                 
        }

        private void SetFormState(string state)
        {
            // view edit add save cancle

            //return;

            if ("View" == state)
            {
                this.formState = "View";

                this.dateTimePicker1.Enabled = true;
                this.dateTimePicker2.Enabled = true;


                //this.comboBox1.Enabled = false;
                this.comboBox2.Enabled = false ;


                this.bindingNavigatorMoveFirstItem.Enabled = true;
                this.bindingNavigatorMoveLastItem.Enabled = true;
                this.bindingNavigatorMoveNextItem.Enabled = true;
                this.bindingNavigatorMovePreviousItem.Enabled = true;
                this.bindingNavigatorPositionItem.Enabled = true;



                toolStripButton6.Visible = false;
                //toolStripButton2.Enabled = true;
                //bindingNavigatorDeleteItem.Enabled = true;
                //bindingNavigatorAddNewItem.Enabled = true;
                //ly_material_plan_mainBindingNavigatorSaveItem.Enabled = false;

                //yX_clientBindingNavigatorSaveItem.Enabled = true ;
                this.ly_salesrepair_outMainBindingNavigator.Enabled = true;

                ly_sales_receive_itemTaskDataGridView.Enabled = true;
                ly_store_outnumDataGridView.Enabled = true;


            }
            else
            {
                this.formState = "Edit";

                this.dateTimePicker1.Enabled = false;
                this.dateTimePicker2.Enabled = false;


                //this.comboBox1.Enabled = true;
                this.comboBox2.Enabled = true;




                this.bindingNavigatorMoveFirstItem.Enabled = false;
                this.bindingNavigatorMoveLastItem.Enabled = false;
                this.bindingNavigatorMoveNextItem.Enabled = false;
                this.bindingNavigatorMovePreviousItem.Enabled = false;
                this.bindingNavigatorPositionItem.Enabled = false;



                toolStripButton6.Visible = true;
                //toolStripButton2.Enabled = false;
                //bindingNavigatorDeleteItem.Enabled = false;
                //bindingNavigatorAddNewItem.Enabled = false;
                //ly_material_plan_mainBindingNavigatorSaveItem.Enabled = true;

                //yX_clientBindingNavigatorSaveItem.Enabled = true ;
                this.ly_salesrepair_outMainBindingNavigator.Enabled = false;

                ly_sales_receive_itemTaskDataGridView.Enabled = false;
                ly_store_outnumDataGridView.Enabled = false;

            }


        }

        private void ly_salesrepair_outMainDataGridView_SelectionChanged(object sender, EventArgs e)
        {
            if (null == ly_sales_receive_itemTaskDataGridView.CurrentRow)
            {
                this.ly_sales_receive_itemDetail_repairTableAdapter.Fill(this.lYSalseMange2.ly_sales_receive_itemDetail_repair,-999);
                this.ly_sales_receive_itemDetail_repair_returnINTableAdapter.Fill(this.lYSalseMange2.ly_sales_receive_itemDetail_repair_returnIN, -999,"");

                this.ly_store_outnumTableAdapter.Fill(this.lYSalseRepair.ly_store_outnum, "asd", "asd", SQLDatabase.NowUserID);
                this.ly_store_outnumRepairreturnTableAdapter.Fill(this.lYSalseRepair.ly_store_outnumRepairreturn, "asd", "asd", SQLDatabase.NowUserID);
                return;
            }

            //this.ly_store_outRepairReturnTableAdapter.Fill(this.lYStoreMange.ly_store_outRepairReturn, out_numberToolStripTextBox.Text, yonghu_nameToolStripTextBox.Text);
            //this.ly_store_outnumRepairreturnTableAdapter.Fill(this.lYSalseRepair.ly_store_outnumRepairreturn, receive_codeToolStripTextBox.Text, worknameToolStripTextBox.Text, yonghu_codeToolStripTextBox.Text);
            

            if (this.formState == "View")
            {
                string nowreceiveCode = ly_sales_receive_itemTaskDataGridView.CurrentRow.Cells["任务单号"].Value.ToString();
                string workname = ly_sales_receive_itemTaskDataGridView.CurrentRow.Cells["领料人"].Value.ToString();
                int task_id=int .Parse ( ly_sales_receive_itemTaskDataGridView.CurrentRow.Cells["task_id"].Value.ToString());

                //this.ly_salesrepair_outDetailBindingSource.Filter = "仓库='asd'";
                //this.ly_salesrepair_outDetailTableAdapter.Fill(this.lYSalseRepair.ly_salesrepair_outDetail, "07", nowreceiveCode, workname);

                this.ly_sales_receive_itemDetail_repairBindingSource.Filter = "仓库='asd'";
                this.ly_sales_receive_itemDetail_repairTableAdapter.Fill(this.lYSalseMange2.ly_sales_receive_itemDetail_repair, task_id);

             

                this.ly_store_outnumTableAdapter.Fill(this.lYSalseRepair.ly_store_outnum, nowreceiveCode, workname, SQLDatabase.NowUserID);

                this.ly_storeout_employWarehouseTableAdapter.Fill(this.lYSalseRepair.ly_storeout_employWarehouse, workname, nowreceiveCode, SQLDatabase.NowUserID);

                 //this.ly_store_outnumRepairreturnTableAdapter.Fill(this.lYSalseRepair.ly_store_in_innumRepair, 

                // this.ly_sales_receive_itemDetail_repair_returnINBindingSource.Filter = "仓库='asd'";
                this.ly_sales_receive_itemDetail_repair_returnINTableAdapter.Fill(this.lYSalseMange2.ly_sales_receive_itemDetail_repair_returnIN, task_id, SQLDatabase.NowUserID);


                this.ly_store_outnumRepairreturnTableAdapter.Fill(this.lYSalseRepair.ly_store_outnumRepairreturn, nowreceiveCode, workname, SQLDatabase.NowUserID);

                if (null == ly_store_outnumDataGridView.CurrentRow)
                {
                    this.ly_store_outTableAdapter.Fill(this.lYStoreMange.ly_store_out, "asd", SQLDatabase.nowUserName());
                }
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            //this.ly_salesrepair_outMainTableAdapter.Fill(this.lYSalseRepair.ly_salesrepair_outMain, this.dateTimePicker1.Value, this.dateTimePicker2.Value);
            this.ly_sales_receive_itemTaskTableAdapter.Fill(this.lYSalseRepair.ly_sales_receive_itemTask, this.dateTimePicker1.Value, this.dateTimePicker2.Value);
    
        }

       

        private void ly_store_outnumDataGridView_SelectionChanged(object sender, EventArgs e)
        {
            if (null == ly_store_outnumDataGridView.CurrentRow) return;

            if (this.formState == "View")
            {
                if (null != this.ly_store_outnumDataGridView.CurrentRow)
                {

                    string outNum = this.ly_store_outnumDataGridView.CurrentRow.Cells["out_number"].Value.ToString();


                    this.ly_store_outTableAdapter.Fill(this.lYStoreMange.ly_store_out, outNum, SQLDatabase.nowUserName());

                    string deptcode = this.ly_store_outnumDataGridView.CurrentRow.Cells["out_deptcode"].Value.ToString();
                    string warehouse = this.ly_store_outnumDataGridView.CurrentRow.Cells["warehouse"].Value.ToString();

                    if (!string.IsNullOrEmpty(warehouse))
                    {


                        this.ly_sales_receive_itemDetail_repairBindingSource.Filter = "仓库='" + warehouse + "'";
                        // this.comboBox1.SelectedValue = deptcode;
                            this.comboBox2.SelectedValue = warehouse;
                    



                    }



                }
            }
            else
            {
                // this.yX_taocan_mainBindingSource.Position = this.nowRow;
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (null != this.comboBox2.SelectedValue)
            {


                this.ly_sales_receive_itemDetail_repairBindingSource.Filter = "仓库='" + this.comboBox2.SelectedValue.ToString() + "'";
                this.ly_store_outBindingSource.Filter = "仓库类别='" + this.comboBox2.SelectedValue.ToString() + "'";
              
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton rdb = sender as RadioButton;
            if (rdb.Checked)
            {
                SetFormState("View");

                this.ly_sales_receive_itemDetail_repairBindingSource.Filter = "仓库='asd'";
            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton rdb = sender as RadioButton;
            if (rdb.Checked)
            {
                SetFormState("Edit");
                //this.ly_store_outTableAdapter.Fill(this.lYStoreMange.ly_store_out, "asd");
            }
        }

        private void 计划物料计算ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (null == this.ly_store_outnumDataGridView.CurrentRow) return;

            if (this.formState != "View") return;

            if (SQLDatabase.nowUserName() != ly_store_outnumDataGridView.CurrentRow.Cells["发料人"].Value.ToString())
            {

                MessageBox.Show("请发料人:" + ly_store_outnumDataGridView.CurrentRow.Cells["发料人"].Value.ToString() + " 删除");

                return;
            }


            //int parentId = int.Parse(this.ly_material_plan_mainDataGridView.CurrentRow.Cells["id"].Value.ToString());
            string outnumber = this.ly_store_outnumDataGridView.CurrentRow.Cells["out_number"].Value.ToString();
            string nowwarehouse = this.ly_store_outnumDataGridView.CurrentRow.Cells["warehouse"].Value.ToString();

            if ("True" == ly_store_outDataGridView.CurrentRow.Cells["finished1"].Value.ToString())
            {
                MessageBox.Show("已经签证,领料单不能删除...");

                return;

            }



            //////////////////////////////////

            string message = "删除当前领料单:" + outnumber + "吗？";
            string caption = "提示...";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;

            DialogResult result;



            result = MessageBox.Show(message, caption, buttons,
            MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

            if (result == DialogResult.Yes)
            {

                //string delstr = " delete ly_store_out  where out_number = '" + outnumber + "'";

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


                    //this.ly_material_plan_mainTableAdapter.Fill(this.lYPlanMange.ly_material_plan_main, "SCJH");
                }

                this.ly_store_outnumBindingSource.RemoveCurrent();

                string nowtaskCode = ly_sales_receive_itemTaskDataGridView.CurrentRow.Cells["任务单号"].Value.ToString();
                string workname = ly_sales_receive_itemTaskDataGridView.CurrentRow.Cells["领料人"].Value.ToString();
                int task_id = int.Parse(ly_sales_receive_itemTaskDataGridView.CurrentRow.Cells["task_id"].Value.ToString());


             



                //this.ly_salesrepair_outDetailBindingSource.Filter = "仓库='asd'";
                this.ly_sales_receive_itemDetail_repairTableAdapter.Fill(this.lYSalseMange2.ly_sales_receive_itemDetail_repair, task_id);
                //this.ly_store_outnumTableAdapter.Fill(this.lYSalseRepair.ly_store_outnum, nowtaskCode, workname, SQLDatabase.NowUserID);

                if (null == ly_store_outnumDataGridView.CurrentRow)
                {
                    this.ly_store_outTableAdapter.Fill(this.lYStoreMange.ly_store_out, "asd", SQLDatabase.nowUserName());
                }
               


            }
        }

        private void 删除子件ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (null == this.ly_store_outDataGridView.CurrentRow) return;

            if ("True" == ly_store_outnumDataGridView.CurrentRow.Cells["finished"].Value.ToString())
            {
                MessageBox.Show("已经签证,不能删除...");

                return;

            }


            int nowId = int.Parse(this.ly_store_outDataGridView.CurrentRow.Cells["idout"].Value.ToString());

            string componentNum = this.ly_store_outDataGridView.CurrentRow.Cells["物料编号1"].Value.ToString();


            string message1 = "当前(物料：" + componentNum + ")将被删除，继续吗？";
            string caption1 = "提示...";
            MessageBoxButtons buttons1 = MessageBoxButtons.YesNo;

            DialogResult result1;



            result1 = MessageBox.Show(message1, caption1, buttons1,
            MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

            if (result1 == DialogResult.Yes)
            {
                //ly_store_outDataGridView
                this.ly_store_outBindingSource.RemoveCurrent();
                SaveChanged();

            }
        }

        private void SaveChanged()
        {
            ///////////////////////////

            this.ly_store_outDataGridView.EndEdit();


            this.ly_store_outBindingSource.EndEdit();



            this.ly_store_outTableAdapter.Update(this.lYStoreMange.ly_store_out);

            string nowtaskCode = ly_sales_receive_itemTaskDataGridView.CurrentRow.Cells["任务单号"].Value.ToString();
            string workname = ly_sales_receive_itemTaskDataGridView.CurrentRow.Cells["领料人"].Value.ToString();
            int task_id = int.Parse(ly_sales_receive_itemTaskDataGridView.CurrentRow.Cells["task_id"].Value.ToString());






            //this.ly_salesrepair_outDetailBindingSource.Filter = "仓库='asd'";
            this.ly_sales_receive_itemDetail_repairTableAdapter.Fill(this.lYSalseMange2.ly_sales_receive_itemDetail_repair, task_id);
           // this.ly_store_outnumTableAdapter.Fill(this.lYSalseRepair.ly_store_outnum, nowtaskCode, workname, SQLDatabase.NowUserID);


        }

        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            if (null == this.ly_sales_receive_itemTaskDataGridView.CurrentRow) return;
            //if (null == ly_plan_getmaterialDataGridView.CurrentRow) return;

            if (this.ly_sales_receive_itemTaskDataGridView.RowCount < 1) return;



            //if ("True" != this.ly_sales_contract_main1DataGridView.CurrentRow.Cells["批准1"].Value.ToString())
            //{
            //    MessageBox.Show("营业部未发出配套出库指令,不能出库...", "注意");
            //    return;
            //}

            //if ("True" != this.ly_sales_contract_main1DataGridView.CurrentRow.Cells["审核1"].Value.ToString())
            //{
            //    MessageBox.Show("未经生产部审核,不能出库...", "注意");
            //    return;
            //}


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



            //string nowreceiveCode = ly_salesrepair_outMainDataGridView.CurrentRow.Cells["收件单号"].Value.ToString();
            //string workname = ly_salesrepair_outMainDataGridView.CurrentRow.Cells["维修人"].Value.ToString();

            string nowtaskCode = ly_sales_receive_itemTaskDataGridView.CurrentRow.Cells["任务单号"].Value.ToString();
            string workname = ly_sales_receive_itemTaskDataGridView.CurrentRow.Cells["领料人"].Value.ToString();
            int task_id = int.Parse(ly_sales_receive_itemTaskDataGridView.CurrentRow.Cells["task_id"].Value.ToString());


            NewFrm.Show(this);

            SqlConnection sqlConnection1 = new SqlConnection(SQLDatabase.Connectstring);
            SqlCommand cmd = new SqlCommand();



            cmd.Parameters.Add("@taskCode", SqlDbType.VarChar);
            cmd.Parameters["@taskCode"].Value = nowtaskCode;

           

                cmd.Parameters.Add("@prod_dept", SqlDbType.VarChar);
                cmd.Parameters["@prod_dept"].Value = "07";
         


            if (null == comboBox2.SelectedValue)
            {
                cmd.Parameters.Add("@warehousename", SqlDbType.VarChar);
                cmd.Parameters["@warehousename"].Value = "---";
            }
            else
            {
                cmd.Parameters.Add("@warehousename", SqlDbType.VarChar);
                cmd.Parameters["@warehousename"].Value = comboBox2.SelectedValue;
            }

            string outNum = GetMaxOutNum();
            cmd.Parameters.Add("@out_number", SqlDbType.VarChar);
            cmd.Parameters["@out_number"].Value = outNum;

            cmd.Parameters.Add("@faliaoren", SqlDbType.VarChar);
            cmd.Parameters["@faliaoren"].Value = SQLDatabase.nowUserName();

          

            cmd.CommandText = "LY_store_out_repairs";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = sqlConnection1;
            cmd.CommandTimeout = 0;
            sqlConnection1.Open();
            cmd.ExecuteNonQuery();
            sqlConnection1.Close();



            //this.ly_salesrepair_outDetailBindingSource.Filter = "仓库='asd'";
            this.ly_sales_receive_itemDetail_repairTableAdapter.Fill(this.lYSalseMange2.ly_sales_receive_itemDetail_repair, task_id);
            this.ly_store_outnumTableAdapter.Fill(this.lYSalseRepair.ly_store_outnum, nowtaskCode, workname, SQLDatabase.NowUserID);

            //this.ly_storeout_employWarehouseTableAdapter.Fill(this.lYSalseRepair.ly_storeout_employWarehouse, nowreceiveCode, workname, SQLDatabase.NowUserID);


            if (null == ly_store_outnumDataGridView.CurrentRow)
            {
                this.ly_store_outTableAdapter.Fill(this.lYStoreMange.ly_store_out, "asd", SQLDatabase.nowUserName());
            }

            NewFrm.Hide(this);
        }

        private string GetMaxOutNum()
        {
            SqlConnection sqlConnection1 = new SqlConnection(SQLDatabase.Connectstring);
            SqlCommand cmd = new SqlCommand();

            string MaxPlanCode = "";

            //cmd.Parameters.Add("@Plan_mode", SqlDbType.VarChar);
            //cmd.Parameters["@Plan_mode"].Value = "LLJH";


            cmd.CommandText = "LY_Get_OutNumber";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = sqlConnection1;

            sqlConnection1.Open();
            MaxPlanCode = cmd.ExecuteScalar().ToString();
            sqlConnection1.Close();



            return MaxPlanCode;
        }

        private void ly_sales_receive_itemDetail_repairDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void ly_sales_receive_itemTaskDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void dataGridViewRepairreturn_SelectionChanged(object sender, EventArgs e)
        {

            if (null != this.dataGridViewRepairreturn.CurrentRow)
            {

                string outNum = this.dataGridViewRepairreturn.CurrentRow.Cells["领料单号re"].Value.ToString();

                //string nowreceiveCode = ly_sales_receive_itemTaskDataGridView.CurrentRow.Cells["任务单号"].Value.ToString();
                //string workname = ly_sales_receive_itemTaskDataGridView.CurrentRow.Cells["领料人"].Value.ToString();
                //int task_id = int.Parse(ly_sales_receive_itemTaskDataGridView.CurrentRow.Cells["task_id"].Value.ToString());



                this.ly_store_outRepairReturnTableAdapter.Fill(this.lYStoreMange.ly_store_outRepairReturn, outNum, SQLDatabase.nowUserName());

                //this.ly_store_outnumRepairreturnTableAdapter.Fill(this.lYSalseRepair.ly_store_outnumRepairreturn, nowreceiveCode, workname, SQLDatabase.NowUserID);


                //string deptcode = this.dataGridViewRepairreturn.CurrentRow.Cells["out_deptcode"].Value.ToString();
                //string warehouse = this.dataGridViewRepairreturn.CurrentRow.Cells["warehouse"].Value.ToString();

                //if (!string.IsNullOrEmpty(warehouse))
                //{


                //    this.ly_sales_receive_itemDetail_repairBindingSource.Filter = "仓库='" + warehouse + "'";
                //    // this.comboBox1.SelectedValue = deptcode;
                //    //this.comboBox2.SelectedValue = warehouse;




                //}



            }

            //string nowreceiveCode = ly_sales_receive_itemTaskDataGridView.CurrentRow.Cells["任务单号"].Value.ToString();
            //string workname = ly_sales_receive_itemTaskDataGridView.CurrentRow.Cells["领料人"].Value.ToString();
            //int task_id = int.Parse(ly_sales_receive_itemTaskDataGridView.CurrentRow.Cells["task_id"].Value.ToString());

            ////this.ly_salesrepair_outDetailBindingSource.Filter = "仓库='asd'";
           

            //this.ly_store_outnumRepairreturnTableAdapter.Fill(this.lYSalseRepair.ly_store_outnumRepairreturn, nowreceiveCode, workname, SQLDatabase.NowUserID);

            ///////////////////////
            
            //if (null == this.dataGridViewRepairreturn.CurrentRow)
            //{
            //    this.ly_store_outnumRepairreturnTableAdapter.Fill(this.lYSalseRepair.ly_store_in_innumRepair, "", SQLDatabase.NowUserID);
            //    return;
            //}


            //string nowInNum = this.dataGridViewRepairreturn.CurrentRow.Cells["入库单号"].Value.ToString();



            //this.ly_store_outnumRepairreturnTableAdapter.Fill(this.lYSalseRepair.ly_store_in_innumRepair, nowInNum, SQLDatabase.NowUserID);
        }

        private void ly_store_outnumDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void bindingNavigatorAddNewItem1_Click(object sender, EventArgs e)
        {
            if (null == this.ly_sales_receive_itemTaskDataGridView.CurrentRow) return;
            if (null == ly_sales_receive_itemDetail_repair_returnDataGridView.CurrentRow) return;

            string message = "确定退料入库吗?";
            string caption = "提示...";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result;



            result = MessageBox.Show(message, caption, buttons,
            MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

            if (result == DialogResult.Yes)
            {

                CountStoreInAuto();
            }
        }
        private string GetMaxStoreInnum()
        {

            SqlConnection sqlConnection1 = new SqlConnection(SQLDatabase.Connectstring);
            SqlCommand cmd = new SqlCommand();

            string MaxStoreInnum = "";

            //cmd.Parameters.Add("@Plan_mode", SqlDbType.VarChar);
            //cmd.Parameters["@Plan_mode"].Value = "LLJH";


            cmd.CommandText = "LY_Get_InNumber";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = sqlConnection1;

            sqlConnection1.Open();
            MaxStoreInnum = cmd.ExecuteScalar().ToString();
            sqlConnection1.Close();



            return MaxStoreInnum;

        }
        private void CountStoreInAuto()
        {
            if (null == this.ly_sales_receive_itemTaskDataGridView.CurrentRow) return;
            if (null == ly_sales_receive_itemDetail_repair_returnDataGridView.CurrentRow) return;


            string nowtaskCode = ly_sales_receive_itemTaskDataGridView.CurrentRow.Cells["任务单号"].Value.ToString();
            string workname = ly_sales_receive_itemTaskDataGridView.CurrentRow.Cells["领料人"].Value.ToString();
            int task_id = int.Parse(ly_sales_receive_itemTaskDataGridView.CurrentRow.Cells["task_id"].Value.ToString());

            string nowreceiveCode = ly_sales_receive_itemTaskDataGridView.CurrentRow.Cells["任务单号"].Value.ToString();


            NewFrm.Show(this);

            SqlConnection sqlConnection1 = new SqlConnection(SQLDatabase.Connectstring);
            SqlCommand cmd = new SqlCommand();



            cmd.Parameters.Add("@task_code", SqlDbType.VarChar);
            cmd.Parameters["@task_code"].Value = nowtaskCode;

           
            cmd.Parameters.Add("@parent_id", SqlDbType.Int);
            cmd.Parameters["@parent_id"].Value = task_id;

            cmd.Parameters.Add("@yonghu_code", SqlDbType.VarChar);
            cmd.Parameters["@yonghu_code"].Value = SQLDatabase.NowUserID;

            
            //string inNum = GetMaxStoreInnum();
            string inNum = GetMaxOutNum();
            cmd.Parameters.Add("@in_number", SqlDbType.VarChar);
            cmd.Parameters["@in_number"].Value = inNum;

            cmd.Parameters.Add("@shouliaoren", SqlDbType.VarChar);
            cmd.Parameters["@shouliaoren"].Value = SQLDatabase.nowUserName();



            //,@ varchar(20))  

            cmd.CommandText = "LY_repair_return_in";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = sqlConnection1;

            sqlConnection1.Open();
            cmd.ExecuteNonQuery();
            sqlConnection1.Close();

            this.ly_sales_receive_itemDetail_repair_returnINTableAdapter.Fill(this.lYSalseMange2.ly_sales_receive_itemDetail_repair_returnIN, task_id, SQLDatabase.NowUserID);


            //this.ly_store_innum_repairreturnTableAdapter.Fill(this.lYSalseRepair.ly_store_innum_repairreturn, nowtaskCode, SQLDatabase.NowUserID);
            //this.ly_store_outnumRepairreturnTableAdapter.Fill(this.lYSalseRepair.ly_store_outnumRepairreturn, nowreceiveCode, workname, SQLDatabase.NowUserID);
            this.ly_store_outnumRepairreturnTableAdapter.Fill(this.lYSalseRepair.ly_store_outnumRepairreturn, nowreceiveCode, workname, SQLDatabase.NowUserID);


            NewFrm.Hide(this);
        }

        private void bindingNavigatorDeleteItem1_Click(object sender, EventArgs e)
        {
            if (null == this.dataGridViewRepairreturn.CurrentRow) return;

            //if (this.formState != "View") return;

            if (SQLDatabase.nowUserName() != dataGridViewRepairreturn.CurrentRow.Cells["发料人re"].Value.ToString())
            {

                MessageBox.Show("请收料人:" + dataGridViewRepairreturn.CurrentRow.Cells["发料人re"].Value.ToString() + " 删除");

                return;
            }


            //int parentId = int.Parse(this.ly_material_plan_mainDataGridView.CurrentRow.Cells["id"].Value.ToString());
            string innumber = this.dataGridViewRepairreturn.CurrentRow.Cells["领料单号re"].Value.ToString();
            //string nowwarehouse = this.ly_store_outnumDataGridView.CurrentRow.Cells["warehouse"].Value.ToString();

            if ("True" == dataGridViewRepairreturn.CurrentRow.Cells["签证re"].Value.ToString())
            {
                MessageBox.Show("已经签证,入库单不能删除...");

                return;

            }



            //////////////////////////////////

            string message = "删除当前退料出库单:" + innumber + "吗？";
            string caption = "提示...";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;

            DialogResult result;



            result = MessageBox.Show(message, caption, buttons,
            MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

            if (result == DialogResult.Yes)
            {

                //string delstr = " delete ly_store_out  where out_number = '" + outnumber + "'";

                string delstr = " delete ly_store_out  from ly_store_out   " +
                   " where ly_store_out.out_number = '" + innumber + "'";



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


                    //this.ly_material_plan_mainTableAdapter.Fill(this.lYPlanMange.ly_material_plan_main, "SCJH");
                }

                string nowtaskCode = ly_sales_receive_itemTaskDataGridView.CurrentRow.Cells["任务单号"].Value.ToString();
                string workname = ly_sales_receive_itemTaskDataGridView.CurrentRow.Cells["领料人"].Value.ToString();
                int task_id = int.Parse(ly_sales_receive_itemTaskDataGridView.CurrentRow.Cells["task_id"].Value.ToString());

                string nowreceiveCode = ly_sales_receive_itemTaskDataGridView.CurrentRow.Cells["任务单号"].Value.ToString();


                this.ly_sales_receive_itemDetail_repair_returnINTableAdapter.Fill(this.lYSalseMange2.ly_sales_receive_itemDetail_repair_returnIN, task_id, SQLDatabase.NowUserID);


                this.ly_store_outnumRepairreturnTableAdapter.Fill(this.lYSalseRepair.ly_store_outnumRepairreturn, nowreceiveCode, workname, SQLDatabase.NowUserID);



            }
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (null == this.dataGridViewRepairreturn.CurrentRow) return;

            if ("True" == dataGridViewRepairreturn.CurrentRow.Cells["签证re"].Value.ToString())
            {
                MessageBox.Show("已经签证,入库信息不能修改...");

                return;

              

            }


            string componentNum = this.ly_store_outRepairReturndataGridView.CurrentRow.Cells["物料编号re"].Value.ToString();


            string message1 = "当前(物料：" + componentNum + ")将被删除，继续吗？";
            string caption1 = "提示...";
            MessageBoxButtons buttons1 = MessageBoxButtons.YesNo;

            DialogResult result1;



            result1 = MessageBox.Show(message1, caption1, buttons1,
            MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

            if (result1 == DialogResult.Yes)
            {
                //ly_store_outDataGridView
                this.ly_store_outRepairReturnBindingSource.RemoveCurrent();
                Savedetail();

                this.ly_sales_receive_itemDetail_repair_returnINBindingSource.Position = this.ly_sales_receive_itemDetail_repair_returnINBindingSource.Find("编码", componentNum);

            }
        }


        private void Savedetail()
        {
            ///////////////////////

            

            string nowtaskCode = ly_sales_receive_itemTaskDataGridView.CurrentRow.Cells["任务单号"].Value.ToString();
            string workname = ly_sales_receive_itemTaskDataGridView.CurrentRow.Cells["领料人"].Value.ToString();
            int task_id = int.Parse(ly_sales_receive_itemTaskDataGridView.CurrentRow.Cells["task_id"].Value.ToString());

            string nowreceiveCode = ly_sales_receive_itemTaskDataGridView.CurrentRow.Cells["任务单号"].Value.ToString();




            string nowitem = this.ly_sales_receive_itemDetail_repair_returnDataGridView.CurrentRow.Cells["退料编号"].Value.ToString();
            string nowInNum = this.dataGridViewRepairreturn.CurrentRow.Cells["领料单号re"].Value.ToString();

            ly_store_outRepairReturndataGridView.EndEdit();
            ly_store_outRepairReturnBindingSource.EndEdit();

            this.ly_store_outRepairReturnTableAdapter.Update(this.lYStoreMange.ly_store_outRepairReturn);

           

            this.ly_sales_receive_itemDetail_repair_returnINTableAdapter.Fill(this.lYSalseMange2.ly_sales_receive_itemDetail_repair_returnIN, task_id, SQLDatabase.NowUserID);
            //this.ly_store_innum_repairreturnTableAdapter.Fill(this.lYSalseRepair.ly_store_innum_repairreturn, nowtaskCode, SQLDatabase.NowUserID);

            this.ly_store_outnumRepairreturnTableAdapter.Fill(this.lYSalseRepair.ly_store_outnumRepairreturn, nowreceiveCode, workname, SQLDatabase.NowUserID);


            this.ly_store_outnumRepairreturnBindingSource.Position = this.ly_store_outnumRepairreturnBindingSource.Find("out_number", nowInNum);
            this.ly_store_outRepairReturnBindingSource.Position = this.ly_store_outRepairReturnBindingSource.Find("物料编号", nowitem);
            //this.ly_purchase_contract_inspection_DetailInBindingSource.Position = this.ly_purchase_contract_inspection_DetailInBindingSource.Find("物料编号", nowitem);


        }

        private void ly_store_outRepairReturndataGridView_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            DataGridView dgv = sender as DataGridView;

            if ("True" == dataGridViewRepairreturn.CurrentRow.Cells["签证"].Value.ToString())
            {
                MessageBox.Show("已经签证,入库信息不能修改...");

                return;

            }

            if (SQLDatabase.nowUserName() != (dataGridViewRepairreturn.CurrentRow.Cells["收料人"].Value.ToString()))
            {

                MessageBox.Show("请收料人:" + dataGridViewRepairreturn.CurrentRow.Cells["收料人"].Value.ToString() + " 修改");

                return;
            }


            if ("入库数量" == dgv.CurrentCell.OwningColumn.Name)
            {

                ChangeValue queryForm = new ChangeValue();

                queryForm.OldValue = dgv.CurrentCell.Value.ToString();
                queryForm.NewValue = "";
                queryForm.ChangeMode = "value";
                queryForm.ShowDialog();

                //decimal oldnum = decimal.Parse(dgv.CurrentCell.Value.ToString());
                //decimal notinnum = decimal.Parse(dgv.CurrentRow.Cells["storecount"].Value.ToString());
                //decimal stanterdnum = 0;

                //if (null != this.ly_plan_getmaterialDataGridView.CurrentRow)
                //{
                //    stanterdnum = decimal.Parse(this.ly_plan_getmaterialDataGridView.CurrentRow.Cells["未领数量"].Value.ToString());
                //}

                if (queryForm.NewValue != "")
                {
                    //decimal newnum = decimal.Parse(queryForm.NewValue);


                    //if ((newnum - oldnum) > storenum)
                    //{
                    //    MessageBox.Show("库存不足,操作取消...");

                    //}
                    //else if (newnum - oldnum > stanterdnum)
                    //{
                    //    MessageBox.Show("领料超计划,操作取消...");
                    //}
                    //else
                    //{
                    dgv.CurrentRow.Cells["入库数量"].Value = queryForm.NewValue;
                    //dgv.CurrentRow.Cells["discount_money"].Value = DBNull.Value;

                    //dgv.CurrentRow.Cells["approve_flag"].Value = "False";
                    Savedetail();

                    //}


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



        }

        private void toolStripTextBox5_KeyUp(object sender, KeyEventArgs e)
        {
            string filterString;


            filterString = GetDataGridviewMultiFilterString.DGVMultiFilterString(this.ly_sales_receive_itemTaskDataGridView, this.toolStripTextBox5.Text);


            this.ly_sales_receive_itemTaskBindingSource.Filter ="("+ filterString+ ") and repair_sector='公司维修'";
        }

        private void toolStripTextBox5_Enter(object sender, EventArgs e)
        {
            toolStripTextBox5.Text = "";

            this.ly_sales_receive_itemTaskBindingSource.Filter = "repair_sector='公司维修'";
        }

        private void toolStripButton9_Click(object sender, EventArgs e)
        {
            if (null == this.ly_store_outDataGridView.CurrentRow) return;

            //int parentId = int.Parse(this.ly_material_plan_mainDataGridView.CurrentRow.Cells["id"].Value.ToString());





            frmWaiting.Show(this);

            //this.ly_plan_getmaterialTableAdapter.Fill(this.lYPlanMange.ly_plan_getmaterial, parentId);

            BaseReportView queryForm = new BaseReportView();

            queryForm.Text = "中原精密维修领料单";

            queryForm.Printdata = this.lYStoreMange;

            queryForm.PrintCrystalReport = new LY_Lingliaodan_WX();


            //string selectFormula;

            //selectFormula = "{ly_store_planitemcount.状态}  =   '原料'  and {ly_store_planitemcount.欠料金额}>0 ";
            //queryForm.PrintCrystalReport.DataDefinition.RecordSelectionFormula = selectFormula;

            frmWaiting.Hide(this);

            queryForm.ShowDialog();
        }

        private void toolStripButton10_Click(object sender, EventArgs e)
        {
            if (null == this.ly_store_outDataGridView.CurrentRow) return;

            //int parentId = int.Parse(this.ly_material_plan_mainDataGridView.CurrentRow.Cells["id"].Value.ToString());





            frmWaiting.Show(this);

            //this.ly_plan_getmaterialTableAdapter.Fill(this.lYPlanMange.ly_plan_getmaterial, parentId);

            BaseReportView queryForm = new BaseReportView();

            queryForm.Text = "中原精密维修欠料单";

            queryForm.Printdata = this.lYSalseMange2;

            queryForm.PrintCrystalReport = new LY_Lingliaodan_WXQL();

            //queryForm.PrintCrystalReport.ParameterFields["faliaoren"].CurrentValues.Add( SQLDatabase.nowUserName());

            //queryForm.PrintCrystalReport.ParameterFields["faliaoren"].CurrentValues=new CrystalDecisions.Shared.ParameterValues( SQLDatabase.nowUserName());
          
            
            //queryForm.PrintCrystalReport.SetParameterValue("faliaoren", SQLDatabase.nowUserName());


            //queryForm.PrintCrystalReport.ParameterFields["faliaoren"].CurrentValues.AddValue(SQLDatabase.nowUserName());
            string selectFormula;

            selectFormula = " {ly_sales_receive_itemDetail_repair.未领数量}>0 ";
            queryForm.PrintCrystalReport.DataDefinition.RecordSelectionFormula = selectFormula;

            //string selectFormula;

            //selectFormula = "{ly_store_planitemcount.状态}  =   '原料'  and {ly_store_planitemcount.欠料金额}>0 ";
            //queryForm.PrintCrystalReport.DataDefinition.RecordSelectionFormula = selectFormula;

            frmWaiting.Hide(this);

            queryForm.ShowDialog();

            
        }

        private void 打印PToolStripButton_Click(object sender, EventArgs e)
        {
            if (null == this.ly_store_outRepairReturndataGridView.CurrentRow) return;



            NewFrm.Show(this); ;

            //this.ly_plan_getmaterialTableAdapter.Fill(this.lYPlanMange.ly_plan_getmaterial, parentId);

            BaseReportView queryForm = new BaseReportView();

            queryForm.Text = "中原精密维修退料出库单";

            queryForm.Printdata = this.lYStoreMange ;

            queryForm.PrintCrystalReport = new LY_Weixiutuiliaodan();


            //string selectFormula;

            //selectFormula = "{ly_store_planitemcount.状态}  =   '原料'  and {ly_store_planitemcount.欠料金额}>0 ";
            //queryForm.PrintCrystalReport.DataDefinition.RecordSelectionFormula = selectFormula;

            NewFrm.Hide(this);

            queryForm.ShowDialog();
        }

        //private void fillToolStripButton_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        this.ly_store_outnumRepairreturnTableAdapter.Fill(this.lYSalseRepair.ly_store_outnumRepairreturn, receive_codeToolStripTextBox.Text, worknameToolStripTextBox.Text, yonghu_codeToolStripTextBox.Text);
            
            
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
        //        this.ly_store_outRepairReturnTableAdapter.Fill(this.lYStoreMange.ly_store_outRepairReturn, out_numberToolStripTextBox.Text, yonghu_nameToolStripTextBox.Text);
        //        this.ly_store_outnumRepairreturnTableAdapter.Fill(this.lYSalseRepair.ly_store_outnumRepairreturn, receive_codeToolStripTextBox.Text, worknameToolStripTextBox.Text, yonghu_codeToolStripTextBox.Text);
            
            
        //    }
        //    catch (System.Exception ex)
        //    {
        //        System.Windows.Forms.MessageBox.Show(ex.Message);
        //    }

        //}
      
       

       

       
    }
}
