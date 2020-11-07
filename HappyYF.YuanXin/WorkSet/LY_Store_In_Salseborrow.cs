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
using DataGridFilter;

namespace HappyYF.YuanXin.WorkSet
{
    public partial class LY_Store_In_Salseborrow : Form
    {
        private string formState;
        string nowInNum;
        string nowdate;

        public LY_Store_In_Salseborrow()
        {
            InitializeComponent();
        }

        private void LY_Machine_Load(object sender, EventArgs e)
        {
            this.dateTimePicker1.Text = DateTime.Today.AddMonths(-6).Date.ToString();
            this.dateTimePicker2.Text = DateTime.Today.AddDays(1).Date.ToString();


           


            this.ly_store_in_diffAllTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            this.ly_store_in_diffTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            this.ly_store_out_diffAllTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            this.ly_store_out_diffTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;

       
           

            this.ly_sales_receive_sel_instoreTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            this.ly_sales_receive_itemInstoreTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            this.ly_store_borrow_innum_purchaseTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            this.ly_store_borrow_in_innumTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;

            this.ly_sales_receive_sel_instoreTableAdapter.Fill(this.lYSalseMange2.ly_sales_receive_sel_instore, "", "full", this.dateTimePicker1.Value, this.dateTimePicker2.Value);

            //this.ly_sales_receive_itemDetail1TableAdapter.Fill(this.lYSalseMange2.ly_sales_receive_itemDetail1, receive_codeToolStripTextBox.Text);
            this.ly_sales_receive_sel_instoreBindingSource.Filter = "返库数量 > 0 and 完成=0";

            this.ly_sales_receive_itemInstoreBindingSource.Filter = "去向  = '返库' or 去向  = '外返' or 去向  = '借新还旧'  or 去向  = '借旧还新' ";



            this.SetFormState("View");

        }


        private void SetFormState(string state)
        {
            // view edit add save cancle

            if ("View" == state)
            {
                this.formState = "View";
                //this.ly_purchase_contract_inspection_MainstoreInDataGridView.Enabled = true ;
               

               

            }
            else
            {
                this.formState = "Edit";
                //this.ly_purchase_contract_inspection_MainstoreInDataGridView.Enabled = false ;

            }


        }

     

       

     

       

        private string GetMaxStoreInnum()
        {

            SqlConnection sqlConnection1 = new SqlConnection(SQLDatabase.Connectstring);
            SqlCommand cmd = new SqlCommand();

            string MaxStoreInnum = "";

            //cmd.Parameters.Add("@Plan_mode", SqlDbType.VarChar);
            //cmd.Parameters["@Plan_mode"].Value = "LLJH";


            cmd.CommandText = "LY_Get_borrow_InNumber";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = sqlConnection1;

            sqlConnection1.Open();
            MaxStoreInnum = cmd.ExecuteScalar().ToString();
            sqlConnection1.Close();



            return MaxStoreInnum;

        }



        private void button1_Click(object sender, EventArgs e)
        {
               this.ly_sales_receive_sel_instoreTableAdapter.Fill(this.lYSalseMange2.ly_sales_receive_sel_instore, "", "full", this.dateTimePicker1.Value, this.dateTimePicker2.Value);
        }

      

       

      
        private void CountStoreInAuto()
        {

            if (null == this.ly_sales_receiveDataGridView.CurrentRow) return;
            if (null == this.ly_sales_receive_itemDetailDataGridView.CurrentRow) return;

            string nowreceiveCode = ly_sales_receiveDataGridView.CurrentRow.Cells["收件编号"].Value.ToString();

            NewFrm.Show(this);

            SqlConnection sqlConnection1 = new SqlConnection(SQLDatabase.Connectstring);
            SqlCommand cmd = new SqlCommand();



            cmd.Parameters.Add("@receive_code", SqlDbType.VarChar);
            cmd.Parameters["@receive_code"].Value = nowreceiveCode;

            cmd.Parameters.Add("@yonghu_code", SqlDbType.VarChar);
            cmd.Parameters["@yonghu_code"].Value = SQLDatabase.NowUserID;


            string inNum = GetMaxStoreInnum();
            cmd.Parameters.Add("@in_number", SqlDbType.VarChar);
            cmd.Parameters["@in_number"].Value = inNum;

            cmd.Parameters.Add("@shouliaoren", SqlDbType.VarChar);
            cmd.Parameters["@shouliaoren"].Value = SQLDatabase.nowUserName();







            //,@ varchar(20))  

            cmd.CommandText = "LY_store_in_borrowreturn";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = sqlConnection1;

            sqlConnection1.Open();
            cmd.ExecuteNonQuery();
            sqlConnection1.Close();

            this.ly_sales_receive_itemInstoreTableAdapter.Fill(this.lYSalseMange2.ly_sales_receive_itemInstore, nowreceiveCode, SQLDatabase.NowUserID);
            this.ly_store_borrow_innum_purchaseTableAdapter.Fill(this.lYStoreMange.ly_store_borrow_innum_purchase, nowreceiveCode, SQLDatabase.NowUserID);


            

            NewFrm.Hide(this);
        }

      

       

        private void ly_store_innum_purchaseDataGridView_SelectionChanged(object sender, EventArgs e)
        {
            if (null == this.ly_store_innum_purchaseDataGridView.CurrentRow)
            {
                this.ly_store_borrow_in_innumTableAdapter.Fill(this.lYStoreMange.ly_store_borrow_in_innum, "", SQLDatabase.NowUserID);
                this.ly_store_in_diffAllTableAdapter.Fill(this.lYSalseMange2.ly_store_in_diffAll, "", "");
                this.ly_store_out_diffAllTableAdapter.Fill(this.lYSalseMange2.ly_store_out_diffAll, "", "");
                return;
            }

            string nowreceiveCode = ly_sales_receiveDataGridView.CurrentRow.Cells["收件编号"].Value.ToString();

            string nowInNum = this.ly_store_innum_purchaseDataGridView.CurrentRow.Cells["入库单号"].Value.ToString();



            this.ly_store_borrow_in_innumTableAdapter.Fill(this.lYStoreMange.ly_store_borrow_in_innum, nowInNum, SQLDatabase.NowUserID);

            this.ly_store_in_diffAllTableAdapter.Fill(this.lYSalseMange2.ly_store_in_diffAll, nowInNum, nowreceiveCode);
            this.ly_store_out_diffAllTableAdapter.Fill(this.lYSalseMange2.ly_store_out_diffAll, nowInNum, nowreceiveCode);



            //private void fillToolStripButton_Click(object sender, EventArgs e)
            //{
            //    try
            //    {
            //        this.ly_store_in_diffAllTableAdapter.Fill(this.lYSalseMange2.ly_store_in_diffAll, bill_codeToolStripTextBox.Text, pruductionTaskInspection_numToolStripTextBox.Text);
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
            //        this.ly_store_in_diffTableAdapter.Fill(this.lYSalseMange2.ly_store_in_diff, in_numberToolStripTextBox.Text);
            //    }
            //    catch (System.Exception ex)
            //    {
            //        System.Windows.Forms.MessageBox.Show(ex.Message);
            //    }

            //}

            //private void fillToolStripButton2_Click(object sender, EventArgs e)
            //{
            //    try
            //    {
            //        this.ly_store_out_diffAllTableAdapter.Fill(this.lYSalseMange2.ly_store_out_diffAll, bill_codeToolStripTextBox1.Text, pruductionTaskInspection_numToolStripTextBox1.Text);
            //    }
            //    catch (System.Exception ex)
            //    {
            //        System.Windows.Forms.MessageBox.Show(ex.Message);
            //    }

            //}

            //private void fillToolStripButton3_Click(object sender, EventArgs e)
            //{
            //    try
            //    {
            //        this.ly_store_out_diffTableAdapter.Fill(this.lYSalseMange2.ly_store_out_diff, out_numberToolStripTextBox.Text);
            //    }
            //    catch (System.Exception ex)
            //    {
            //        System.Windows.Forms.MessageBox.Show(ex.Message);
            //    }

            //}
        }


       

        private void ly_store_in_ylDataGridView_SelectionChanged(object sender, EventArgs e)
        {
            //if (null == this.ly_store_in_ylDataGridView.CurrentRow) return;
          
            //DataGridView dgv = sender as DataGridView;

            //string nowitem = dgv.CurrentRow.Cells["物料编号1"].Value.ToString();

            //ly_purchase_contract_inspection_DetailInDataGridView.SelectionChanged -= ly_purchase_contract_inspection_DetailInDataGridView_SelectionChanged;
            //this.ly_purchase_contract_inspection_DetailInBindingSource.Position = this.ly_purchase_contract_inspection_DetailInBindingSource.Find("物料编号", nowitem);
            //ly_purchase_contract_inspection_DetailInDataGridView.SelectionChanged += ly_purchase_contract_inspection_DetailInDataGridView_SelectionChanged;
        }

       
        private void ly_store_in_ylDataGridView_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            return;
            
            DataGridView dgv = sender as DataGridView;

            if ("True" == ly_store_innum_purchaseDataGridView.CurrentRow.Cells["签证"].Value.ToString())
            {
                MessageBox.Show("已经签证,入库信息不能修改...");

                return;

            }

            if (SQLDatabase.nowUserName() != (ly_store_innum_purchaseDataGridView.CurrentRow.Cells["收料人"].Value.ToString()))
            {

                MessageBox.Show("请收料人:" + ly_store_innum_purchaseDataGridView.CurrentRow.Cells["收料人"].Value.ToString() + " 修改");

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
                       SaveChanged();
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
        private void SaveChanged()
        {
            ///////////////////////////

         
            ly_store_in_ylDataGridView.EndEdit();
            ly_store_borrow_in_innumBindingSource.EndEdit();

            this.ly_store_borrow_in_innumTableAdapter.Update(this.lYStoreMange.ly_store_borrow_in_innum);

            //string inspectioncode = this.ly_purchase_contract_inspection_MainstoreInDataGridView.CurrentRow.Cells["检验单号"].Value.ToString();

            //this.ly_store_innum_purchaseTableAdapter.Fill(this.lYStoreMange.ly_store_innum_purchase, inspectioncode, SQLDatabase.NowUserID);
            //this.ly_purchase_contract_inspection_DetailInTableAdapter.Fill(this.lYStoreMange.ly_purchase_contract_inspection_DetailIn, inspectioncode, SQLDatabase.NowUserID);


            //this.ly_store_innum_purchaseBindingSource.Position = this.ly_store_innum_purchaseBindingSource.Find("入库单号", nowInNum);
            //this.ly_store_in_innumBindingSource.Position = this.ly_store_in_innumBindingSource.Find("物料编号", nowitem);
            //this.ly_purchase_contract_inspection_DetailInBindingSource.Position = this.ly_purchase_contract_inspection_DetailInBindingSource.Find("物料编号", nowitem);


        }
        private void bindingNavigatorAddNewItem1_Click(object sender, EventArgs e)
        {
            //if (null == this.ly_purchase_contract_inspection_MainstoreInDataGridView.CurrentRow) return;
            //if (null == ly_purchase_contract_inspection_DetailInDataGridView.CurrentRow) return;

            string message = "确定借用返库吗?";
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

        private void bindingNavigatorDeleteItem1_Click(object sender, EventArgs e)
        {
            if (null == this.ly_store_innum_purchaseDataGridView.CurrentRow) return;

            //if (this.formState != "View") return;

            if (SQLDatabase.nowUserName() != ly_store_innum_purchaseDataGridView.CurrentRow.Cells["收料人"].Value.ToString())
            {

                MessageBox.Show("请收料人:" + ly_store_innum_purchaseDataGridView.CurrentRow.Cells["收料人"].Value.ToString() + " 删除");

                return;
            }


            //int parentId = int.Parse(this.ly_material_plan_mainDataGridView.CurrentRow.Cells["id"].Value.ToString());
            string innumber = this.ly_store_innum_purchaseDataGridView.CurrentRow.Cells["入库单号"].Value.ToString();
            //string nowwarehouse = this.ly_store_outnumDataGridView.CurrentRow.Cells["warehouse"].Value.ToString();

            if ("True" == ly_store_innum_purchaseDataGridView.CurrentRow.Cells["签证"].Value.ToString())
            {
                MessageBox.Show("已经签证,入库单不能删除...");

                return;

            }



            //////////////////////////////////

            string message = "删除当前入库单:" + innumber + "吗？";
            string caption = "提示...";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;

            DialogResult result;



            result = MessageBox.Show(message, caption, buttons,
            MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

            if (result == DialogResult.Yes)
            {

                //string delstr = " delete ly_store_out  where out_number = '" + outnumber + "'";

                string delstr = " delete ly_store_in  from ly_store_in   " +
                   " where ly_store_in.in_number = '" + innumber + "'";



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

                string nowreceiveCode = ly_sales_receiveDataGridView.CurrentRow.Cells["收件编号"].Value.ToString();

                this.ly_sales_receive_itemInstoreTableAdapter.Fill(this.lYSalseMange2.ly_sales_receive_itemInstore, nowreceiveCode, SQLDatabase.NowUserID);
                this.ly_store_borrow_innum_purchaseTableAdapter.Fill(this.lYStoreMange.ly_store_borrow_innum_purchase, nowreceiveCode, SQLDatabase.NowUserID);




            }
        }

        private void 打印PToolStripButton_Click(object sender, EventArgs e)
        {
            if (null == this.ly_store_innum_purchaseDataGridView.CurrentRow) return;



            NewFrm.Show(this); ;

            //this.ly_plan_getmaterialTableAdapter.Fill(this.lYPlanMange.ly_plan_getmaterial, parentId);

            BaseReportView queryForm = new BaseReportView();

            queryForm.Text = "中原精密借用返库单";

            queryForm.Printdata = this.lYStoreMange;

            queryForm.PrintCrystalReport = new LY_LingliaodanJGJIEyong();


            //string selectFormula;

            //selectFormula = "{ly_store_planitemcount.状态}  =   '原料'  and {ly_store_planitemcount.欠料金额}>0 ";
            //queryForm.PrintCrystalReport.DataDefinition.RecordSelectionFormula = selectFormula;

            NewFrm.Hide(this);

            queryForm.ShowDialog();
        }

        private void 删除子件ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (null == this.ly_store_in_ylDataGridView.CurrentRow) return;

            if ("True" == ly_store_innum_purchaseDataGridView.CurrentRow.Cells["签证"].Value.ToString())
            {
                MessageBox.Show("已经签证,入库信息不能修改...");

                return;

            }




            string componentNum = this.ly_store_in_ylDataGridView.CurrentRow.Cells["物料编号1"].Value.ToString();


            string message1 = "当前(物料：" + componentNum + ")将被删除，继续吗？";
            string caption1 = "提示...";
            MessageBoxButtons buttons1 = MessageBoxButtons.YesNo;

            DialogResult result1;



            result1 = MessageBox.Show(message1, caption1, buttons1,
            MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

            if (result1 == DialogResult.Yes)
            {
                //ly_store_outDataGridView
                this.ly_store_borrow_in_innumBindingSource.RemoveCurrent();
                SaveChanged();

                string nowreceiveCode = ly_sales_receiveDataGridView.CurrentRow.Cells["收件编号"].Value.ToString();


                this.ly_sales_receive_itemInstoreTableAdapter.Fill(this.lYSalseMange2.ly_sales_receive_itemInstore, nowreceiveCode, SQLDatabase.NowUserID);
                this.ly_store_borrow_innum_purchaseTableAdapter.Fill(this.lYStoreMange.ly_store_borrow_innum_purchase, nowreceiveCode, SQLDatabase.NowUserID);


                //this.ly_purchase_contract_inspection_DetailInBindingSource.Position = this.ly_purchase_contract_inspection_DetailInBindingSource.Find("物料编号", componentNum);

            }
        }

        private void SaveIndate(string indate)
        {
            string innumber = this.ly_store_innum_purchaseDataGridView.CurrentRow.Cells["入库单号"].Value.ToString();
          
            string delstr = " update ly_store_in  set input_date='" + indate + "'" +
                " from ly_store_in " +
               " where ly_store_in.in_number = '" + innumber + "'";


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

        }

        private void ly_store_innum_purchaseDataGridView_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            DataGridView dgv = sender as DataGridView;

            if (SQLDatabase.nowUserName() != dgv.CurrentRow.Cells["收料人"].Value.ToString())
            {

                MessageBox.Show("请收料人:" + dgv.CurrentRow.Cells["收料人"].Value.ToString() + " 修改");

                return;
            }

           

            /////////////////////////////////////////////////////
            if ("入库日期1" == dgv.CurrentCell.OwningColumn.Name)
            {
                if ("True" == dgv.CurrentRow.Cells["签证"].Value.ToString())
                {
                    MessageBox.Show("已经签证,不能修改入库日期...");

                    return;

                }



                ChangeValue queryForm = new ChangeValue();

                queryForm.OldValue = dgv.CurrentCell.Value.ToString();
                queryForm.NewValue = "";
                queryForm.ChangeMode = "datetime";
                queryForm.ShowDialog();


                if (queryForm.NewValue != "")
                {
                   
                    dgv.CurrentCell.Value = queryForm.NewValue;
                    SaveIndate(queryForm.NewValue);

                }


                return;

            }

            /////////////////////////////////////////////////////
        }

        private void ly_sales_receiveDataGridView_SelectionChanged(object sender, EventArgs e)
        {
            if (null == ly_sales_receiveDataGridView.CurrentRow)
            {


                this.ly_sales_receive_itemInstoreTableAdapter.Fill(this.lYSalseMange2.ly_sales_receive_itemInstore, "asd", SQLDatabase.NowUserID);
                return;
            }


            string nowreceiveCode = ly_sales_receiveDataGridView.CurrentRow.Cells["收件编号"].Value.ToString();

           
            this.ly_sales_receive_itemInstoreTableAdapter.Fill(this.lYSalseMange2.ly_sales_receive_itemInstore, nowreceiveCode, SQLDatabase.NowUserID);
            this.ly_store_borrow_innum_purchaseTableAdapter.Fill(this.lYStoreMange.ly_store_borrow_innum_purchase, nowreceiveCode, SQLDatabase.NowUserID);

        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
          
            if ("全部" == toolStripButton1.Text)
            {
                toolStripButton1.Text = "未完";
                this.ly_sales_receive_sel_instoreBindingSource.Filter = "返库数量 > 0 and 完成=0";

                
            }
            else
            {
                toolStripButton1.Text = "全部";
                this.ly_sales_receive_sel_instoreBindingSource.Filter = "返库数量 > 0";
            }

        }

        private void ly_store_in_diffAllDataGridView_SelectionChanged(object sender, EventArgs e)
        {
            if (null == this.ly_store_in_diffAllDataGridView.CurrentRow)
            {
                this.ly_store_in_diffTableAdapter.Fill(this.lYSalseMange2.ly_store_in_diff, "asd");
                return;
            }



            string nowInNum = this.ly_store_in_diffAllDataGridView.CurrentRow.Cells["入库单号diff"].Value.ToString();


            this.ly_store_in_diffTableAdapter.Fill(this.lYSalseMange2.ly_store_in_diff, nowInNum);
            

        }

        private void ly_store_out_diffAllDataGridView_SelectionChanged(object sender, EventArgs e)
        {
            if (null == this.ly_store_out_diffAllDataGridView.CurrentRow)
            {
                this.ly_store_out_diffTableAdapter.Fill(this.lYSalseMange2.ly_store_out_diff, "asd"); 
                return;
            }



            string nowOutNum = this.ly_store_out_diffAllDataGridView.CurrentRow.Cells["出库单号diff"].Value.ToString();

            this.ly_store_out_diffTableAdapter.Fill(this.lYSalseMange2.ly_store_out_diff, nowOutNum); 


        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            if (null == this.ly_store_innum_purchaseDataGridView.CurrentRow) return;



            NewFrm.Show(this); ;



            BaseReportView queryForm = new BaseReportView();

            queryForm.Text = "中原精密异型返库出入库单";

            queryForm.Printdata = this.lYSalseMange2;

            queryForm.PrintCrystalReport = new LY_Yixingchuruku();


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
        //        this.ly_store_in_diffAllTableAdapter.Fill(this.lYSalseMange2.ly_store_in_diffAll, bill_codeToolStripTextBox.Text, pruductionTaskInspection_numToolStripTextBox.Text);
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
        //        this.ly_store_in_diffTableAdapter.Fill(this.lYSalseMange2.ly_store_in_diff, in_numberToolStripTextBox.Text);
        //    }
        //    catch (System.Exception ex)
        //    {
        //        System.Windows.Forms.MessageBox.Show(ex.Message);
        //    }

        //}

        //private void fillToolStripButton2_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        this.ly_store_out_diffAllTableAdapter.Fill(this.lYSalseMange2.ly_store_out_diffAll, bill_codeToolStripTextBox1.Text, pruductionTaskInspection_numToolStripTextBox1.Text);
        //    }
        //    catch (System.Exception ex)
        //    {
        //        System.Windows.Forms.MessageBox.Show(ex.Message);
        //    }

        //}

        //private void fillToolStripButton3_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        this.ly_store_out_diffTableAdapter.Fill(this.lYSalseMange2.ly_store_out_diff, out_numberToolStripTextBox.Text);
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
        //        this.ly_store_borrow_in_innumTableAdapter.Fill(this.lYStoreMange.ly_store_borrow_in_innum, in_numberToolStripTextBox.Text, yonghu_codeToolStripTextBox.Text);
        //    }
        //    catch (System.Exception ex)
        //    {
        //        System.Windows.Forms.MessageBox.Show(ex.Message);
        //    }

        //}

      
     

      

      
    }
}
