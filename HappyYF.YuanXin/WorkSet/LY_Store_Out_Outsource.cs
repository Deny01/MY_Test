using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HappyYF.Infrastructure.Repositories;
using DataGridFilter;
using System.Transactions;

namespace HappyYF.YuanXin.WorkSet
{
    public partial class LY_Store_Out_Outsource : Form
    {

        private string formState;
        string nowOutNum;
        string nowdate;


        public LY_Store_Out_Outsource()
        {
            InitializeComponent();
        }

        private void LY_Machine_Load(object sender, EventArgs e)
        {

            this.dateTimePicker1.Text = DateTime.Today.AddYears(-1).Date.ToString();
            this.dateTimePicker2.Text = DateTime.Today.AddDays(1).Date.ToString();

            this.ly_storeout_employWarehouse1TableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            this.ly_outsource_order_materialrequisitionSUMTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            this.ly_outsource_contractForrequest_ViewTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            this.ly_outsource_orderSUM_NewTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;

            this.ly_outsource_contractForrequest_ViewTableAdapter.Fill(this.lYOutsourceData.ly_outsource_contractForrequest_View, DateTime.Parse(this.dateTimePicker1.Text).Date, DateTime.Parse(this.dateTimePicker2.Text).Date);



            this.ly_store_outnum_outsourceTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;


            this.ly_store_out_JGTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;

            this.ly_store_out_JG2TableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;


            this.radioButton1.Checked = true;

            this.SetFormState("View");




        }









        private void toolStripTextBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            //if (e.KeyChar.ToString() == "\r")
            //{
            //    e.Handled = true;



            //}
        }

        private void button1_Click(object sender, EventArgs e)
        {

            this.ly_outsource_contractForrequest_ViewTableAdapter.Fill(this.lYOutsourceData.ly_outsource_contractForrequest_View, DateTime.Parse(this.dateTimePicker1.Text).Date, DateTime.Parse(this.dateTimePicker2.Text).Date);
        }



        private void bindingNavigatorAddNewItem_Click(object sender, EventArgs e)
        {

            string message = "增加外协生产领料记录吗？";
            string caption = "提示...";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;

            DialogResult result;



            result = MessageBox.Show(message, caption, buttons,
            MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

            if (result == DialogResult.Yes)
            {


                decimal request_count;
                decimal hadget_count;




                hadget_count = 0;

                if (null != this.ly_store_outDataGridView.CurrentRow)
                {

                    foreach (DataGridViewRow dgr in ly_store_outDataGridView.Rows)
                    {

                        if (string.IsNullOrEmpty(dgr.Cells["领料数量"].Value.ToString())) continue;
                        hadget_count = hadget_count + decimal.Parse(dgr.Cells["领料数量"].Value.ToString());



                    }
                }



                else
                {

                    this.ly_store_out_JGBindingSource.AddNew();


                    this.ly_store_outDataGridView.CurrentRow.Cells["出库日期"].Value = SQLDatabase.GetNowdate().ToString(); ;

                    this.ly_store_outDataGridView.CurrentRow.Cells["发料人"].Value = SQLDatabase.nowUserName();


                    this.ly_store_outDataGridView.CurrentRow.Cells["出库类别"].Value = "外协生产";
                }


                SaveChanged();


            }
        }

        private void SaveChanged()
        {

            this.ly_store_outDataGridView.EndEdit();

            this.Validate();
            this.ly_store_out_JGBindingSource.EndEdit();

            this.ly_store_out_JGTableAdapter.Update(this.lYMaterielRequirements.ly_store_out_JG);


            int outstore_Id;

            if (null != this.ly_store_outDataGridView.CurrentRow)
            {
                outstore_Id = int.Parse(this.ly_store_outDataGridView.CurrentRow.Cells["outstore_Id"].Value.ToString());
            }
            else
            {
                outstore_Id = 0;
            }

            string nowcontract = this.ly_outsource_contractForrequest_ViewDataGridView.CurrentRow.Cells["合同编码"].Value.ToString();

            this.ly_outsource_order_materialrequisitionSUMTableAdapter.Fill(this.lYOutsourceData.ly_outsource_order_materialrequisitionSUM, nowcontract);
            this.ly_outsource_orderSUM_NewTableAdapter.Fill(this.lYOutsourceData.ly_outsource_orderSUM_New, nowcontract);

        }

        private string GetMaxStoreOutnum()
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

        private void bindingNavigatorDeleteItem_Click(object sender, EventArgs e)
        {


            if (null == this.ly_store_outDataGridView.CurrentRow) return;

            //if (CheckFinished()) return;



            if ("True" == this.ly_store_outDataGridView.CurrentRow.Cells["签证"].Value.ToString())
            {
                MessageBox.Show("已经签证，不能删除(实需删除，请先删除签证标记)", "注意");
                return;

            }

            string nowoperptar = this.ly_store_outDataGridView.CurrentRow.Cells["发料人"].Value.ToString();

            if (nowoperptar != SQLDatabase.nowUserName())
            {
                MessageBox.Show("请发料人:" + nowoperptar + "删除", "注意");
                return;
            }



            string message1 = "当前出库记录将被删除，继续吗？";
            string caption1 = "提示...";
            MessageBoxButtons buttons1 = MessageBoxButtons.YesNo;

            DialogResult result1;



            result1 = MessageBox.Show(message1, caption1, buttons1,
            MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

            if (result1 == DialogResult.Yes)
            {

                this.ly_store_out_JGBindingSource.RemoveCurrent();



                SaveChanged();




            }
        }





        private void ly_production_order_materialrequisitionDataGridView_SelectionChanged(object sender, EventArgs e)
        {
            //if (null != this.ly_production_order_materialrequisitionDataGridView.CurrentRow)
            //{


            //    string nowmaterialrequisitionnum = this.ly_production_order_materialrequisitionDataGridView.CurrentRow.Cells["领料单号5"].Value.ToString();

            //    this.ly_store_out_JGTableAdapter.Fill(this.lYMaterielRequirements.ly_store_out_JG, nowmaterialrequisitionnum);

            //    //set_processOrder_Num();


            //}
            //else
            //{
            //    this.ly_store_out_JGTableAdapter.Fill(this.lYMaterielRequirements .ly_store_out_JG , "");
            //}
        }





        private void ly_store_outDataGridView_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {

            DataGridView dgv = sender as DataGridView;

            if ("True" == this.ly_store_outDataGridView.CurrentRow.Cells["签证"].Value.ToString())
            {
                MessageBox.Show("已经签证，不能修改(实需删除，请先删除签证标记)", "注意");
                return;

            }



            string nowoperptar = this.ly_store_outDataGridView.CurrentRow.Cells["发料人"].Value.ToString();

            if (nowoperptar != SQLDatabase.nowUserName())
            {
                MessageBox.Show("请发料人:" + nowoperptar + "修改", "注意");
                return;
            }




            if ("领料数量" == dgv.CurrentCell.OwningColumn.Name)
            {

                ChangeValue queryForm = new ChangeValue();

                queryForm.OldValue = dgv.CurrentCell.Value.ToString();
                queryForm.NewValue = "";
                queryForm.ChangeMode = "value";
                queryForm.ShowDialog();

                decimal oldnum = decimal.Parse(dgv.CurrentCell.Value.ToString());
                decimal storenum = decimal.Parse(dgv.CurrentRow.Cells["storecount"].Value.ToString());
                decimal stanterdnum = 0;

                if (null != this.ly_outsource_order_materialrequisitionSUMDataGridView.CurrentRow)
                {
                    stanterdnum = decimal.Parse(this.ly_outsource_order_materialrequisitionSUMDataGridView.CurrentRow.Cells["未领数量1"].Value.ToString());
                }

                if (queryForm.NewValue != "")
                {
                    decimal newnum = decimal.Parse(queryForm.NewValue);


                    if ((newnum - oldnum) > storenum)
                    {
                        MessageBox.Show("库存不足,操作取消...");

                    }
                    else if (newnum - oldnum > stanterdnum)
                    {
                        MessageBox.Show("领料超计划,操作取消...");
                    }
                    else
                    {
                        dgv.CurrentRow.Cells["领料数量"].Value = queryForm.NewValue;
                        //dgv.CurrentRow.Cells["discount_money"].Value = DBNull.Value;

                        //dgv.CurrentRow.Cells["approve_flag"].Value = "False";
                        SaveChanged();
                    }


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




            //if ("部门名称" == dgv.CurrentCell.OwningColumn.Name)
            //{

            //    return;
            //    //ChangeValue queryForm = new ChangeValue();

            //    //queryForm.OldValue = dgv.CurrentCell.Value.ToString();
            //    //queryForm.NewValue = "";
            //    //queryForm.ShowDialog();




            //    //if (queryForm.NewValue != "")
            //    //{
            //    //    dgv.CurrentRow.Cells["部门名称"].Value = queryForm.NewValue;
            //    //    //dgv.CurrentRow.Cells["discount_money"].Value = DBNull.Value;

            //    //    //dgv.CurrentRow.Cells["approve_flag"].Value = "False";
            //    //    SaveChanged();

            //    //}
            //    //else
            //    //{


            //    //}
            //    //return;

            //    //////////////////

            //    string sel = "SELECT a.prodname as 编码,a.prodcode as 名称 FROM ly_prod_dept a ";


            //    QueryForm queryForm = new QueryForm();


            //    queryForm.Sel = sel;
            //    queryForm.Constr = SQLDatabase.Connectstring;



            //    queryForm.ShowDialog();




            //    dgv.CurrentRow.Cells["部门名称"].Value = queryForm.Result; ;
            //    SaveChanged();



            //    return;
            //}



            ///////////////////////////////////////////////////////////////here....

            //if (CheckFinished()) return;

            //if ("True" == this.ly_store_outDataGridView.CurrentRow.Cells["签证"].Value.ToString())
            //{
            //    MessageBox.Show("已经签证，不能修改(实需删除，请先删除签证标记)", "注意");
            //    return;

            //}

            //string nowoperptar = this.ly_store_outDataGridView.CurrentRow.Cells["发料人"].Value.ToString();

            //if (nowoperptar != SQLDatabase.nowUserName())
            //{
            //    MessageBox.Show("请发料人:" + nowoperptar + "修改", "注意");
            //    return;
            //}

            //DataGridView dgv = sender as DataGridView;




            //if ("领料数量" == dgv.CurrentCell.OwningColumn.Name)
            //{

            //    ChangeValue queryForm = new ChangeValue();

            //    queryForm.OldValue = dgv.CurrentCell.Value.ToString();
            //    queryForm.NewValue = "";
            //    queryForm.ChangeMode = "value";
            //    queryForm.ShowDialog();




            //    if (queryForm.NewValue != "")
            //    {
            //        dgv.CurrentRow.Cells["领料数量"].Value = queryForm.NewValue;

            //        /////////////////////////////////////////////////////////////////////////////////////////////
            //        decimal request_count;
            //        decimal hadget_count;



            //        //if (!string.IsNullOrEmpty(this.ly_production_order_materialrequisitionDataGridView.CurrentRow.Cells["数量5"].Value.ToString()))
            //        //{
            //        //    request_count = decimal.Parse(this.ly_production_order_materialrequisitionDataGridView.CurrentRow.Cells["数量5"].Value.ToString());
            //        //}
            //        //else
            //        //{
            //        //    request_count = 0;
            //        //}


            //        //if (!string.IsNullOrEmpty(this.ly_production_order_inspectionAll_InstoreDataGridView.CurrentRow.Cells["入库数量0"].Value.ToString()))
            //        //{
            //        //    instore_count = decimal.Parse(this.ly_production_order_inspectionAll_InstoreDataGridView.CurrentRow.Cells["入库数量0"].Value.ToString());
            //        //}
            //        //else
            //        //{
            //        //    instore_count = 0;
            //        //}


            ////        hadget_count = 0;

            ////        if (null != this.ly_store_outDataGridView.CurrentRow)
            ////        {

            ////            foreach (DataGridViewRow dgr in ly_store_outDataGridView.Rows)
            ////            {

            ////                if (string.IsNullOrEmpty(dgr.Cells["领料数量"].Value.ToString())) continue;
            ////                hadget_count = hadget_count + decimal.Parse(dgr.Cells["领料数量"].Value.ToString());



            ////            }
            ////        }








            //        //if (hadget_count >= request_count)
            //        //{

            //        //    MessageBox.Show("该领料单数量已全部领完,不能增加领料出库记录", "注意");

            //        //    if (string.IsNullOrEmpty(queryForm.OldValue))
            //        //    {
            //        //        dgv.CurrentRow.Cells["领料数量"].Value = DBNull.Value;
            //        //    }
            //        //    else
            //        //    {
            ////        //        dgv.CurrentRow.Cells["领料数量"].Value = queryForm.OldValue;
            ////        //    }

            ////        //    return;
            ////        //}





            ////        SaveChanged();


            ////        //CountPlanStru();

            ////    }
            ////    else
            ////    {
            ////        //hT_Manage_ItemDataGridView.CurrentRow.Cells["apply_money"].Value = queryForm.NewValue;
            ////        //dgv.CurrentRow.Cells["discount_money"].Value = DBNull.Value;
            ////        //dgv.CurrentRow.Cells["apply_money"].Value = DBNull.Value;
            ////        //dgv.CurrentRow.Cells["approve_flag"].Value = "False";
            ////        //SaveChanged();

            ////    }
            //    return;

            //}




            /////////////////////////////////////////////////////////////////////////////////


            //if ("出库日期" == dgv.CurrentCell.OwningColumn.Name)
            //{

            //    ChangeValue queryForm = new ChangeValue();

            //    queryForm.OldValue = dgv.CurrentCell.Value.ToString();
            //    queryForm.NewValue = "";
            //    queryForm.ChangeMode = "datetime";
            //    queryForm.ShowDialog();




            //    if (queryForm.NewValue != "")
            //    {
            //        dgv.CurrentRow.Cells["出库日期"].Value = queryForm.NewValue;
            //        //dgv.CurrentRow.Cells["discount_money"].Value = DBNull.Value;

            //        //dgv.CurrentRow.Cells["approve_flag"].Value = "False";
            //        SaveChanged();


            //        //CountPlanStru();

            //    }
            //    else
            //    {


            //    }
            //    return;


            //    /////////////////////////////////////

            //    //DatePicker queryForm = new DatePicker();
            //    //queryForm.Pt = pt;

            //    //if (null != (dgv.CurrentCell.Value))
            //    //    queryForm.NowDate = dgv.CurrentCell.Value.ToString();

            //    //queryForm.ShowDialog();



            //    //if (null != queryForm.NowDate)
            //    //{

            //    //    dgv.CurrentRow.Cells["检验日期"].Value = queryForm.NowDate;
            //    //    SaveChanged();

            //    //}
            //    //return;
            //}









            /////////////////////////////////////////////////////////

            //if ("备注" == dgv.CurrentCell.OwningColumn.Name)
            //{

            //    ChangeValue queryForm = new ChangeValue();

            //    queryForm.OldValue = dgv.CurrentCell.Value.ToString();
            //    queryForm.NewValue = "";
            //    queryForm.ChangeMode = "longstring";
            //    queryForm.ShowDialog();




            //    if (queryForm.NewValue != "")
            //    {
            //        dgv.CurrentRow.Cells["备注"].Value = queryForm.NewValue;
            //        //dgv.CurrentRow.Cells["discount_money"].Value = DBNull.Value;

            //        //dgv.CurrentRow.Cells["approve_flag"].Value = "False";
            //        SaveChanged();


            //        //CountPlanStru();

            //    }
            //    else
            //    {

            //    }
            //    return;

            //}

            //////////////////////////////////

            ///////////////////////////////////////////////////////

            //if ("送交人" == dgv.CurrentCell.OwningColumn.Name)
            //{





            //    string sel;



            //    sel = "SELECT   yhmc as 姓名 FROM T_users where bumen='000401'";



            //    QueryForm queryForm = new QueryForm();


            //    queryForm.Sel = sel;
            //    queryForm.Constr = SQLDatabase.Connectstring;

            //    //Set the Column Collection to the filter Table
            //    //queryForm.SetSourceColumns(this.billMainDataSet.BalanceBill.Columns);

            //    queryForm.ShowDialog();


            //    if (queryForm.Result != "")
            //    {
            //        dgv.CurrentRow.Cells["送交人"].Value = queryForm.Result;
            //        //dgv.CurrentRow.Cells["discount_money"].Value = DBNull.Value;

            //        //dgv.CurrentRow.Cells["approve_flag"].Value = "False";
            //        SaveChanged();


            //        //CountPlanStru();

            //    }
            //    else
            //    {
            //        //hT_Manage_ItemDataGridView.CurrentRow.Cells["apply_money"].Value = queryForm.NewValue;
            //        //dgv.CurrentRow.Cells["discount_money"].Value = DBNull.Value;
            //        //dgv.CurrentRow.Cells["apply_money"].Value = DBNull.Value;
            //        //dgv.CurrentRow.Cells["approve_flag"].Value = "False";
            //        //SaveChanged();

            //    }
            //    return;

            //}


            /////////////////////////////////////////////////////////
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //this.ly_store_outnumDailyTableAdapter.Fill(this.lYMaterielRequirements.ly_store_outnumDaily, DateTime.Parse(this.dateTimePicker3.Text).Date, DateTime.Parse(this.dateTimePicker4.Text).Date);
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            this.SetFormState("Edit");

            this.nowOutNum = "NewOutnum";

            this.nowdate = SQLDatabase.GetNowdate().ToString();

            this.ly_store_out_JGTableAdapter.Fill(this.lYMaterielRequirements.ly_store_out_JG, "asd");
            this.ly_store_out_JG2TableAdapter.Fill(this.lYMaterielRequirements.ly_store_out_JG2, "asd");
        }

        private void SetFormState(string state)
        {
            // view edit add save cancle

            if ("View" == state)
            {
                this.formState = "View";

                this.comboBox2.Enabled = false;
                toolStripButton7.Visible = false;
                toolStripButton17.Visible = false;
                toolStripButton18.Visible = false;
                button1.Enabled = true;

                this.ly_store_outnumDailyDataGridView.Enabled = true;
                this.ly_outsource_contractForrequest_ViewDataGridView.Enabled = true;

                this.toolStripButton1.Enabled = true;
                this.toolStripButton2.Enabled = true;
                this.toolStripButton6.Enabled = false;
                this.toolStripButton5.Enabled = false;
                this.toolStripButton4.Enabled = true;
                this.toolStripTextBox1.Enabled = true;



                this.ly_store_outDataGridView.ReadOnly = true;





            }
            else
            {
                this.formState = "Edit";

                this.comboBox2.Enabled = true;
                toolStripButton7.Visible = true;
                toolStripButton17.Visible = true;
                toolStripButton18.Visible = true;
                button1.Enabled = false;

                this.ly_store_outnumDailyDataGridView.Enabled = false;
                this.ly_outsource_contractForrequest_ViewDataGridView.Enabled = false;

                this.ly_store_outDataGridView.ReadOnly = false;


                this.toolStripButton1.Enabled = false;
                this.toolStripButton2.Enabled = false;
                this.toolStripButton6.Enabled = true;
                this.toolStripButton5.Enabled = true;
                this.toolStripButton4.Enabled = false;

                this.toolStripTextBox1.Enabled = false;




            }


        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {


            /////////////////////////////

            if (null == this.ly_store_outnumDailyDataGridView.CurrentRow) return;

            //if (!string.IsNullOrEmpty(ly_store_outnumDailyDataGridView.CurrentRow.Cells["计划编号"].Value.ToString()))
            //{
            //    MessageBox.Show("计划发料只能在这里浏览,不能修改", "注意");
            //    return;
            //}

            string nowoperptar = this.ly_store_outnumDailyDataGridView.CurrentRow.Cells["发料人0"].Value.ToString();

            if (nowoperptar != SQLDatabase.nowUserName())
            {
                MessageBox.Show("请发料人:" + nowoperptar + "修改", "注意");
                return;
            }

            if ("True" == ly_store_outnumDailyDataGridView.CurrentRow.Cells["签证0"].Value.ToString())
            {
                MessageBox.Show("已经签证,不能修改入库数据...");

                return;

            }



            this.nowOutNum = ly_store_outnumDailyDataGridView.CurrentRow.Cells["出库单号0"].Value.ToString();
            this.nowdate = ly_store_outnumDailyDataGridView.CurrentRow.Cells["出库日期0"].Value.ToString();


            this.SetFormState("Edit");
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            if (null == this.ly_store_outnumDailyDataGridView.CurrentRow) return;

            string nowoperptar = this.ly_store_outnumDailyDataGridView.CurrentRow.Cells["发料人0"].Value.ToString();

            if (nowoperptar != SQLDatabase.nowUserName())
            {
                MessageBox.Show("请发料人:" + nowoperptar + "删除", "注意");
                return;
            }

            if ("True" == ly_store_outnumDailyDataGridView.CurrentRow.Cells["签证0"].Value.ToString())
            {
                MessageBox.Show("已经签证,不能删除出库单...");

                return;

            }

            string outnumber = ly_store_outnumDailyDataGridView.CurrentRow.Cells["出库单号0"].Value.ToString();
            string storename = ly_store_outnumDailyDataGridView.CurrentRow.Cells["仓库"].Value.ToString();

            string message = "删除当前领料单:" + outnumber + "吗？";
            string caption = "提示...";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;

            DialogResult result;



            result = MessageBox.Show(message, caption, buttons,
            MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

            if (result == DialogResult.Yes)
            {

                string delstr = " delete ly_store_out  from ly_store_out left join ly_inma0010 on ly_store_out.wzbh=ly_inma0010.wzbh  " +
                    " where ly_store_out.out_number = '" + outnumber + "' and ly_inma0010.warehouse='" + storename + "'";


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

                string nowcontract = this.ly_outsource_contractForrequest_ViewDataGridView.CurrentRow.Cells["合同编码"].Value.ToString();



                this.ly_outsource_order_materialrequisitionSUMTableAdapter.Fill(this.lYOutsourceData.ly_outsource_order_materialrequisitionSUM, nowcontract);
                this.ly_outsource_orderSUM_NewTableAdapter.Fill(this.lYOutsourceData.ly_outsource_orderSUM_New, nowcontract);
                this.ly_store_outnum_outsourceTableAdapter.Fill(this.lYOutsourceData.ly_store_outnum_outsource, nowcontract);



            }
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            SaveChanged();


            this.SetFormState("View");
        }

        private void ly_store_outnumDailyDataGridView_SelectionChanged(object sender, EventArgs e)
        {
            if (this.formState != "View")
            {

                return;
            }
            string nowcontract = this.ly_outsource_contractForrequest_ViewDataGridView.CurrentRow.Cells["合同编码"].Value.ToString();
            if (null == this.ly_store_outnumDailyDataGridView.CurrentRow)
            {
                this.ly_store_out_JGTableAdapter.Fill(this.lYMaterielRequirements.ly_store_out_JG, "");
                this.ly_store_out_JG2TableAdapter.Fill(this.lYMaterielRequirements.ly_store_out_JG2, "");
                return;
            }

            //int parentId = int.Parse(this.ly_material_plan_mainDataGridView.CurrentRow.Cells["id"].Value.ToString());
            string nowOutNum = this.ly_store_outnumDailyDataGridView.CurrentRow.Cells["出库单号0"].Value.ToString();


            this.ly_store_out_JGTableAdapter.Fill(this.lYMaterielRequirements.ly_store_out_JG, nowOutNum);

            this.ly_store_out_JG2TableAdapter.Fill(this.lYMaterielRequirements.ly_store_out_JG2, nowcontract);
            //string outNum = this.ly_store_outnumDataGridView.CurrentRow.Cells["out_number"].Value.ToString();


            //this.ly_store_outTableAdapter.Fill(this.lYStoreMange.ly_store_out, outNum);


            string warehouse = this.ly_store_outnumDailyDataGridView.CurrentRow.Cells["仓库"].Value.ToString();

            if (!string.IsNullOrEmpty(warehouse))
            {

                this.ly_outsource_order_materialrequisitionSUMBindingSource.Filter = "仓库='" + warehouse + "'";

                this.comboBox2.SelectedValue = warehouse;




            }




        }

        private void ly_production_order_materialrequisitionDataGridView_DoubleClick(object sender, EventArgs e)
        {
            if (this.formState == "View") return;

            //if (null == this.ly_production_order_materialrequisitionDataGridView.CurrentRow) return;

            //if (CheckFinished()) return;
            /////////////////////////////////////////////////////////////////////////////////////////

            string newFlag = "N";

            if (this.nowOutNum == "NewOutnum")
            {

                this.nowOutNum = GetMaxStoreOutnum();
                newFlag = "Y";
            }
            else
            {

                newFlag = "N";
            }

            //string nowmaterialrequisitionnum = this.ly_production_order_materialrequisitionDataGridView.CurrentRow.Cells["领料单号5"].Value.ToString();
            //string componentNum = this.ly_inma0010_inoutDataGridView.CurrentRow.Cells["物资编号"].Value.ToString();



            // if ("下料" == nowordername) return;  ly_machinepart_process_workDataGridView

            string message = "增加外协生产领料记录吗？";
            string caption = "提示...";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;

            DialogResult result;



            result = MessageBox.Show(message, caption, buttons,
            MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

            if (result == DialogResult.Yes)
            {


                decimal request_count;
                decimal hadget_count;
                decimal store_count;
                decimal real_outcount;

                //if (!string.IsNullOrEmpty(this.ly_production_order_materialrequisitionDataGridView.CurrentRow.Cells["库存"].Value.ToString()))
                //{
                //    store_count = decimal.Parse(this.ly_production_order_materialrequisitionDataGridView.CurrentRow.Cells["库存"].Value.ToString());
                //}
                //else
                //{
                //    store_count = 0;
                //}





                //if (!string.IsNullOrEmpty(this.ly_production_order_materialrequisitionDataGridView.CurrentRow.Cells["数量5"].Value.ToString()))
                //{
                //    request_count = decimal.Parse(this.ly_production_order_materialrequisitionDataGridView.CurrentRow.Cells["数量5"].Value.ToString());
                //}
                //else
                //{
                //    request_count = 0;
                //}

                //已领



                hadget_count = 0;

                //if (!string.IsNullOrEmpty(this.ly_production_order_materialrequisitionDataGridView.CurrentRow.Cells["已领5"].Value.ToString()))
                //{
                //    hadget_count = decimal.Parse(this.ly_production_order_materialrequisitionDataGridView.CurrentRow.Cells["已领5"].Value.ToString());
                //}
                //else
                //{
                //    hadget_count = 0;
                //}

                //if (null != this.ly_store_outDataGridView.CurrentRow)
                //{

                //    foreach (DataGridViewRow dgr in ly_store_outDataGridView.Rows)
                //    {

                //        if (string.IsNullOrEmpty(dgr.Cells["领料数量"].Value.ToString())) continue;
                //        hadget_count = hadget_count + decimal.Parse(dgr.Cells["领料数量"].Value.ToString());



                //    }
                //}





                //if (hadget_count >= request_count)
                //{

                //    MessageBox.Show("该领料单数量已全部领完,不能增加领料出库记录", "注意");

                //    return;
                //}


                //else
                //{
                //    if (request_count - hadget_count <= store_count)
                //    {
                //        real_outcount = request_count - hadget_count;
                //    }
                //    else
                //    {

                //        real_outcount = store_count;
                //    }

                //    this.ly_store_out_JGBindingSource.AddNew();

                //    //this.ly_store_outDataGridView.CurrentRow.Cells["领料单号"].Value = this.ly_production_order_materialrequisitionDataGridView.CurrentRow.Cells["领料单号5"].Value;
                //    //this.ly_store_outDataGridView.CurrentRow.Cells["出库单号"].Value = this.nowOutNum;

                //    //this.ly_store_outDataGridView.CurrentRow.Cells["物料编号"].Value = this.ly_production_order_materialrequisitionDataGridView.CurrentRow.Cells["物料编号5"].Value;


                //    //this.ly_store_outDataGridView.CurrentRow.Cells["领料数量"].Value = real_outcount;
                //    //this.ly_store_outDataGridView.CurrentRow.Cells["领料人"].Value = this.ly_production_order_materialrequisitionDataGridView.CurrentRow.Cells["姓名5"].Value;


                //    this.ly_store_outDataGridView.CurrentRow.Cells["出库日期"].Value = this.nowdate;

                //    this.ly_store_outDataGridView.CurrentRow.Cells["发料人"].Value = SQLDatabase.nowUserName();
                //    this.ly_store_outDataGridView.CurrentRow.Cells["领料部门"].Value = "06";


                //    this.ly_store_outDataGridView.CurrentRow.Cells["出库类别"].Value = "外协生产";




                //}






                SaveChanged();

                //if (newFlag == "Y")
                //{
                //this.ly_store_outnumDailyDataGridView.SelectionChanged -= ly_store_outnumDailyDataGridView_SelectionChanged;
                //this.ly_store_outnumDailyTableAdapter.Fill(this.lYMaterielRequirements.ly_store_outnumDaily, DateTime.Parse(this.dateTimePicker3.Text).Date, DateTime.Now.AddDays(1).Date);
                //this.ly_store_outnumDailyBindingSource.Position = this.ly_store_outnumDailyBindingSource.Find("出库单号", nowOutNum);
                //this.ly_store_outnumDailyDataGridView.SelectionChanged += ly_store_outnumDailyDataGridView_SelectionChanged;


                //}




            }

            /////////////////////////////////////////////////////////////////////////////////////////////




        }

        private void 打印PToolStripButton_Click(object sender, EventArgs e)
        {
            if (this.formState != "View")
            {
                MessageBox.Show("请保存数据后打印...", "注意");
                return;
            }

            if (null == this.ly_store_outDataGridView.CurrentRow) return;


            NewFrm.Show(this);


            BaseReportView queryForm = new BaseReportView();

            queryForm.Text = "中原精密外协领料出库单";

            queryForm.Printdata = this.lYMaterielRequirements;

            queryForm.PrintCrystalReport = new LY_LingliaodanWX();



            NewFrm.Hide(this);

            queryForm.ShowDialog();
        }

        private void 删除ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (null == this.ly_store_outDataGridView.CurrentRow) return;

            //if (CheckFinished()) return;



            if ("True" == this.ly_store_outDataGridView.CurrentRow.Cells["签证"].Value.ToString())
            {
                MessageBox.Show("已经签证，不能删除(实需删除，请先删除签证标记)", "注意");
                return;

            }

            string nowoperptar = this.ly_store_outDataGridView.CurrentRow.Cells["发料人"].Value.ToString();

            if (nowoperptar != SQLDatabase.nowUserName())
            {
                MessageBox.Show("请发料人:" + nowoperptar + "删除", "注意");
                return;
            }



            string message1 = "当前出库记录将被删除，继续吗？";
            string caption1 = "提示...";
            MessageBoxButtons buttons1 = MessageBoxButtons.YesNo;

            DialogResult result1;

            result1 = MessageBox.Show(message1, caption1, buttons1,
            MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

            if (result1 == DialogResult.Yes)
            {

                this.ly_store_out_JGBindingSource.RemoveCurrent();

                SaveChanged();
            }
        }

        private void toolStripTextBox3_Enter(object sender, EventArgs e)
        {
            toolStripTextBox3.Text = "";

            this.ly_outsource_contractForrequest_ViewBindingSource.Filter = "";
        }

        private void toolStripTextBox3_KeyUp(object sender, KeyEventArgs e)
        {
            string dFilter = "";

            dFilter = GetDataGridviewMultiFilterString.DGVMultiFilterString(this.ly_outsource_contractForrequest_ViewDataGridView, this.toolStripTextBox3.Text);

            if (null == dFilter)
                dFilter = "";

            this.ly_outsource_contractForrequest_ViewBindingSource.Filter = dFilter;
        }

        private void ly_outsource_contractForrequest_ViewDataGridView_SelectionChanged(object sender, EventArgs e)
        {
            if (this.formState == "View")
            {
                if (null != this.ly_outsource_contractForrequest_ViewDataGridView.CurrentRow)
                {
                    // int parentId = int.Parse(this.ly_material_plan_mainDataGridView.CurrentRow.Cells["id"].Value.ToString());

                    this.ly_storeout_employWarehouse1TableAdapter.Fill(this.lYStoreMange.ly_storeout_employWarehouse1, "asd", SQLDatabase.NowUserID);

                    string nowcontract = this.ly_outsource_contractForrequest_ViewDataGridView.CurrentRow.Cells["合同编码"].Value.ToString();



                    this.ly_outsource_order_materialrequisitionSUMTableAdapter.Fill(this.lYOutsourceData.ly_outsource_order_materialrequisitionSUM, nowcontract);
                    this.ly_outsource_orderSUM_NewTableAdapter.Fill(this.lYOutsourceData.ly_outsource_orderSUM_New, nowcontract);
                    this.ly_store_outnum_outsourceTableAdapter.Fill(this.lYOutsourceData.ly_store_outnum_outsource, nowcontract);


                    this.ly_storeout_employWarehouse1TableAdapter.Fill(this.lYStoreMange.ly_storeout_employWarehouse1, nowcontract, SQLDatabase.NowUserID);

                }
            }
            else
            {
                // this.yX_taocan_mainBindingSource.Position = this.nowRow;
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton rdb = sender as RadioButton;
            if (rdb.Checked)
            {
                SetFormState("View");

                this.ly_store_out_JGTableAdapter.Fill(this.lYMaterielRequirements.ly_store_out_JG, "asd");
                this.ly_store_out_JG2TableAdapter.Fill(this.lYMaterielRequirements.ly_store_out_JG2, "asd");
                this.ly_storeout_employWarehouse1TableAdapter.Fill(this.lYStoreMange.ly_storeout_employWarehouse1, "asd", SQLDatabase.NowUserID);


            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton rdb = sender as RadioButton;
            if (rdb.Checked)
            {
                SetFormState("Edit");
                this.ly_store_out_JGTableAdapter.Fill(this.lYMaterielRequirements.ly_store_out_JG, "asd");
                this.ly_store_out_JG2TableAdapter.Fill(this.lYMaterielRequirements.ly_store_out_JG2, "asd");
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (null != this.comboBox2.SelectedValue)
            {


                this.ly_outsource_order_materialrequisitionSUMBindingSource.Filter = "仓库='" + this.comboBox2.SelectedValue.ToString() + "'";

            }
        }

        private void toolStripButton7_Click(object sender, EventArgs e)
        {
            if (null == this.ly_outsource_contractForrequest_ViewDataGridView.CurrentRow) return;
            if (null == ly_outsource_order_materialrequisitionSUMDataGridView.CurrentRow) return;


            if ("True" != ly_outsource_contractForrequest_ViewDataGridView.CurrentRow.Cells["批准"].Value.ToString())
            {
                MessageBox.Show("合同未经批准,不能领料出库...", "注意");
                return;

            }


            //if (2 == this.toolStripComboBox1.SelectedIndex)
            //{
            //    if ("True" != this.ly_material_plan_mainDataGridView.CurrentRow.Cells["出库指令"].Value.ToString())
            //    {
            //        MessageBox.Show("营业部未发出临时配套出库指令,不能出库...", "注意");
            //        return;
            //    }
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
            if (null == this.ly_outsource_order_materialrequisitionSUMDataGridView.CurrentRow) return;

            if (null == comboBox2.SelectedValue) return;


            string nowcontract = this.ly_outsource_contractForrequest_ViewDataGridView.CurrentRow.Cells["合同编码"].Value.ToString();


            //string componentNum = this.lY_dayget_material_selDataGridView.CurrentRow.Cells["物资编号"].Value.ToString();
            //int parentId = int.Parse(this.ly_material_plan_mainDataGridView.CurrentRow.Cells["id"].Value.ToString());

            NewFrm.Show(this);
            //frmWaiting.Show(this);

            SqlConnection sqlConnection1 = new SqlConnection(SQLDatabase.Connectstring);
            SqlCommand cmd = new SqlCommand();



            cmd.Parameters.Add("@contract_num", SqlDbType.VarChar);
            cmd.Parameters["@contract_num"].Value = nowcontract;



            cmd.Parameters.Add("@warehousename", SqlDbType.VarChar);
            cmd.Parameters["@warehousename"].Value = comboBox2.SelectedValue;

            string outNum = GetMaxOutNum();
            cmd.Parameters.Add("@out_number", SqlDbType.VarChar);
            cmd.Parameters["@out_number"].Value = outNum;

            cmd.Parameters.Add("@faliaoren", SqlDbType.VarChar);
            cmd.Parameters["@faliaoren"].Value = SQLDatabase.nowUserName();






            //,@ varchar(20))  

            cmd.CommandText = "LY_store_out_input_outsource";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = sqlConnection1;
            cmd.CommandTimeout = 0;



            sqlConnection1.Open();
            cmd.ExecuteNonQuery();
            sqlConnection1.Close();



            this.ly_store_out_JGTableAdapter.Fill(this.lYMaterielRequirements.ly_store_out_JG, outNum);
            this.ly_store_out_JG2TableAdapter.Fill(this.lYMaterielRequirements.ly_store_out_JG2, nowcontract); 

            this.ly_outsource_order_materialrequisitionSUMTableAdapter.Fill(this.lYOutsourceData.ly_outsource_order_materialrequisitionSUM, nowcontract);

            this.ly_outsource_orderSUM_NewTableAdapter.Fill(this.lYOutsourceData.ly_outsource_orderSUM_New, nowcontract);

            this.ly_store_outnum_outsourceTableAdapter.Fill(this.lYOutsourceData.ly_store_outnum_outsource, nowcontract);



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

        private void ly_store_outDataGridView_SelectionChanged(object sender, EventArgs e)
        {
            if (null == this.ly_store_outDataGridView.CurrentRow) return;
            if (null == this.ly_outsource_order_materialrequisitionSUMDataGridView.CurrentRow) return;

            DataGridView dgv = sender as DataGridView;

            string nowitem = dgv.CurrentRow.Cells["物料编号"].Value.ToString();

            ly_outsource_order_materialrequisitionSUMDataGridView.SelectionChanged -= ly_outsource_order_materialrequisitionSUMDataGridView_SelectionChanged;
            this.ly_outsource_order_materialrequisitionSUMBindingSource.Position = this.ly_outsource_order_materialrequisitionSUMBindingSource.Find("物料编号", nowitem);
            ly_outsource_order_materialrequisitionSUMDataGridView.SelectionChanged += ly_outsource_order_materialrequisitionSUMDataGridView_SelectionChanged;
        }

        private void ly_outsource_order_materialrequisitionSUMDataGridView_SelectionChanged(object sender, EventArgs e)
        {
            if (null == this.ly_outsource_order_materialrequisitionSUMDataGridView.CurrentRow) return;

            if (null == this.ly_store_outDataGridView.CurrentRow) return;



            DataGridView dgv = sender as DataGridView;




            string nowitem = dgv.CurrentRow.Cells["物料编号1"].Value.ToString();


            ly_store_outDataGridView.SelectionChanged -= ly_store_outDataGridView_SelectionChanged;

            this.ly_store_out_JGBindingSource.Position = this.ly_store_out_JGBindingSource.Find("物料编号", nowitem);

            ly_store_outDataGridView.SelectionChanged += ly_store_outDataGridView_SelectionChanged;
        }

        //private void 调整出库数量ToolStripMenuItem_Click(object sender, EventArgs e)
        //{
        //    if (this.dataGridView1.CurrentRow == null)
        //        return;

        //    if (this.ly_outsource_contractForrequest_ViewDataGridView.CurrentRow == null)
        //        return;
        //    string nowcontract = this.ly_outsource_contractForrequest_ViewDataGridView.CurrentRow.Cells["合同编码"].Value.ToString();
        //    string wzbh = this.dataGridView1.CurrentRow.Cells["物料编号总"].Value.ToString();  //调整的物料编号

        //    this.ly_outsource_order_materialrequisitionSUMTableAdapter.Fill(this.lYOutsourceData.ly_outsource_order_materialrequisitionSUM, nowcontract);

        //    this.lyoutsourceorderSUMNewBindingSource.Position = this.lyoutsourceorderSUMNewBindingSource.Find("物料编号", wzbh);

        //    if (this.dataGridView1.CurrentRow.Cells["已领总"].Value.ToString() == "0.00" ||
        //        this.dataGridView1.CurrentRow.Cells["已领总"].Value.ToString() == "0.000" ||
        //        this.dataGridView1.CurrentRow.Cells["已领总"].Value.ToString() == "0.0000" ||
        //        this.dataGridView1.CurrentRow.Cells["已领总"].Value.ToString() == "0.00000" ||
        //        this.dataGridView1.CurrentRow.Cells["已领总"].Value.ToString() == "0.0" ||
        //        this.dataGridView1.CurrentRow.Cells["已领总"].Value.ToString() == "0")
        //    {
        //        MessageBox.Show("调整出库数量必须在先将该物料出库之后调整...", "注意");
        //        return;
        //    }

        //    decimal kucun = decimal.Parse(this.dataGridView1.CurrentRow.Cells["库存总"].Value.ToString());//最新库存？
        //    decimal lingliao = decimal.Parse(this.dataGridView1.CurrentRow.Cells["领料总"].Value.ToString());//初始领料 
        //    ChangeValue queryForm = new ChangeValue();
 
        //    queryForm.NewValue = "";

        //    queryForm.ChangeMode = "value";
        //    queryForm.ShowDialog();

        //    if (!string.IsNullOrEmpty(queryForm.NewValue))
        //    {

        //        decimal tiaozheng = decimal.Parse(queryForm.NewValue);//调整的数量 
        //        //if (tiaozheng <= 0)
        //        //{
        //        //    MessageBox.Show("调整出库数量必须大于0，如果小于0，请按退料流程，提交质检审批...", "注意");
        //        //    return;
        //        //}

        //        if (tiaozheng == 0)
        //        {
        //            MessageBox.Show("调整出库数量为0...", "注意");
        //            return;
        //        }

        //        if (tiaozheng > kucun)
        //        {
        //            MessageBox.Show("调整出库数量已经大于实时库存...", "注意");
        //            return;
        //        }

        //        DataTable dt = GetLingLiao(nowcontract, wzbh);
        //        if (dt == null)
        //        {
        //            MessageBox.Show("该合同没有该物料信息...", "注意");
        //            return;
        //        }
        //        decimal totalQty = 0;//计算总物料需求
        //        for (int i = 0; i < dt.Rows.Count; i++)
        //        {
        //            totalQty += decimal.Parse(dt.Rows[i]["qty"].ToString());
        //        }
        //        //循环插入

        //        string outNum = GetMaxOutNum();//本次出库单号
        //        decimal sum = 0;


        //        NewFrm.Show(this);
        //        DateTime dttime = DateTime.Now;
        //        for (int i = 0; i < dt.Rows.Count; i++)
        //        {
        //            string paritem = dt.Rows[i]["parentitemno"].ToString();
        //            string lingliaoren = dt.Rows[i]["operator"].ToString();
        //            decimal trueqty = 0;
        //            if (i == dt.Rows.Count - 1) //余数递补
        //            {
        //                trueqty = tiaozheng - sum;
        //            }
        //            else
        //            {
        //                trueqty = Math.Round((decimal.Parse(dt.Rows[i]["qty"].ToString()) / totalQty) * tiaozheng);
        //                sum += trueqty;
        //            }



        //            string sql = @"insert into ly_store_out  ([warehouse] ,[parentitemno] ,[wzbh] ,[qty]  ,[bill_code] ,[out_number] ,[out_deptcode] ,
        //                                 [out_date] ,[remark],[employe] ,[operoter]  ,[finished] ,[out_style])
        //                                 values  ('" + comboBox2.SelectedValue + "','" + paritem + "','" + wzbh + "'," + trueqty + ",'" + nowcontract + "','" + outNum + "','0004','" + dttime
        //                                             + "','单条实发','" + lingliaoren + "','" + SQLDatabase.nowUserName() + "',0,'外协领料')";


        //            using (SqlConnection con = new SqlConnection(SQLDatabase.Connectstring))
        //            {

        //                using (SqlCommand cmd = new SqlCommand(sql, con))
        //                {

        //                    con.Open();
        //                    cmd.ExecuteNonQuery();
        //                }
        //            }
        //        }



        //        this.ly_outsource_order_materialrequisitionSUMTableAdapter.Fill(this.lYOutsourceData.ly_outsource_order_materialrequisitionSUM, nowcontract);

        //        this.ly_outsource_orderSUM_NewTableAdapter.Fill(this.lYOutsourceData.ly_outsource_orderSUM_New, nowcontract);

        //        this.ly_store_outnum_outsourceTableAdapter.Fill(this.lYOutsourceData.ly_store_outnum_outsource, nowcontract);

        //        NewFrm.Hide(this);
        //    }

        //    else
        //    {
        //        return;

        //    }

        //}





        //根据合同号找到该物料的领料明细
        protected DataTable GetLingLiao(string concode, string wzbh)
        {
            string sql = @"SELECT a_1.contract_code, a_1.parentitemno, a_1.itemno, a_1.qty,a_1.operator FROM (SELECT contract_code, parentitemno, itemno, SUM(isnull(qty,0)) AS qty,operator
                           FROM ly_outsource_order_materialrequisition AS a WHERE (ISNULL(approve_flag, 0) = 1) GROUP BY contract_code, parentitemno, itemno,operator) AS a_1 
                           WHERE (a_1.contract_code = '" + concode + "') and itemno='" + wzbh + "'";


            DataTable dt = null;
            using (SqlConnection connection = new SqlConnection(SQLDatabase.Connectstring))
            {

                SqlDataAdapter adapter = new SqlDataAdapter(sql, connection);
                DataSet ds = new DataSet();
                adapter.Fill(ds);
                dt = ds.Tables[0];
            }

            return dt;
        }

        private void toolStripButton16_Click(object sender, EventArgs e)
        {
            if (this.formState != "View")
            {
                MessageBox.Show("请保存数据后打印...", "注意");
                return;
            }

            if (null == this.ly_store_outDataGridView.CurrentRow) return;


            NewFrm.Show(this);


            BaseReportView queryForm = new BaseReportView();

            queryForm.Text = "中原精密外协领料出库单";

            queryForm.Printdata = this.lYMaterielRequirements;

            queryForm.PrintCrystalReport = new LY_LingliaodanWX2();



            NewFrm.Hide(this);

            queryForm.ShowDialog();
        }
        #region
        //private void 提交退料质检ToolStripMenuItem_Click(object sender, EventArgs e)
        //{
        //    if (this.dataGridView1.CurrentRow == null)
        //        return;

        //    if (this.ly_outsource_contractForrequest_ViewDataGridView.CurrentRow == null)
        //        return;
        //    string nowcontract = this.ly_outsource_contractForrequest_ViewDataGridView.CurrentRow.Cells["合同编码"].Value.ToString();


        //    if (this.dataGridView1.CurrentRow.Cells["已领总"].Value.ToString() == "0.00" ||
        //        this.dataGridView1.CurrentRow.Cells["已领总"].Value.ToString() == "0.000" ||
        //        this.dataGridView1.CurrentRow.Cells["已领总"].Value.ToString() == "0.0000" ||
        //        this.dataGridView1.CurrentRow.Cells["已领总"].Value.ToString() == "0.00000" ||
        //        this.dataGridView1.CurrentRow.Cells["已领总"].Value.ToString() == "0.0" ||
        //        this.dataGridView1.CurrentRow.Cells["已领总"].Value.ToString() == "0")
        //    {
        //        MessageBox.Show("调整出库数量必须在先将该物料出库之后调整...", "注意");
        //        return;
        //    }
        //    ChangeValue queryForm = new ChangeValue();


        //    queryForm.NewValue = "";

        //    queryForm.ChangeMode = "value";
        //    queryForm.ShowDialog();
        //    decimal tiaozheng = 0;
        //    if (!string.IsNullOrEmpty(queryForm.NewValue))
        //    {

        //        tiaozheng = decimal.Parse(queryForm.NewValue);//调整的数量 
        //        if (tiaozheng >= 0)
        //        {
        //            MessageBox.Show("退料数量必须小于0...", "注意");
        //            return;
        //        }


        //        string wzbh = this.dataGridView1.CurrentRow.Cells["物料编号总"].Value.ToString();  //调整的物料编号


        //        string sql = @"insert into  t_outsource_approve 
        //                   ([ContractCode],[ItemNo] ,[ApproveCount] ,[IsInStore],[SavePeo],[SaveTime])
        //           values  ('" + nowcontract + "','" + wzbh + "','" + tiaozheng + "',0 ,'" + SQLDatabase.nowUserName() + "','" + DateTime.Now + "')";

        //        int k = 0;
        //        using (SqlConnection con = new SqlConnection(SQLDatabase.Connectstring))
        //        {

        //            using (SqlCommand cmd = new SqlCommand(sql, con))
        //            {

        //                con.Open();
        //                k = cmd.ExecuteNonQuery();
        //            }
        //        }
        //        if (k > 0)
        //        {
        //            MessageBox.Show("提交成功...", "注意");
        //            return;
        //        }
        //    }
        //}
      
        //private void 进行退料操作ToolStripMenuItem_Click(object sender, EventArgs e)
        //{

        //    if (this.dataGridView1.CurrentRow == null)
        //        return;

        //    if (this.ly_outsource_contractForrequest_ViewDataGridView.CurrentRow == null)
        //        return;
        //    string nowcontract = this.ly_outsource_contractForrequest_ViewDataGridView.CurrentRow.Cells["合同编码"].Value.ToString();


        //    if (this.dataGridView1.CurrentRow.Cells["已领总"].Value.ToString() == "0.00" ||
        //        this.dataGridView1.CurrentRow.Cells["已领总"].Value.ToString() == "0.000" ||
        //        this.dataGridView1.CurrentRow.Cells["已领总"].Value.ToString() == "0.0000" ||
        //        this.dataGridView1.CurrentRow.Cells["已领总"].Value.ToString() == "0.00000" ||
        //        this.dataGridView1.CurrentRow.Cells["已领总"].Value.ToString() == "0.0" ||
        //        this.dataGridView1.CurrentRow.Cells["已领总"].Value.ToString() == "0")
        //    {
        //        MessageBox.Show("调整出库数量必须在先将该物料出库之后调整...", "注意");
        //        return;
        //    }
        //    decimal lingliao = decimal.Parse(this.dataGridView1.CurrentRow.Cells["领料总"].Value.ToString());//初始领料
        //    decimal yiling = decimal.Parse(this.dataGridView1.CurrentRow.Cells["已领总"].Value.ToString());//已经领料
        //    decimal weiling = lingliao - yiling;
        //    decimal kucun = decimal.Parse(this.dataGridView1.CurrentRow.Cells["库存总"].Value.ToString());//最新库存？
        //    string wzbh = this.dataGridView1.CurrentRow.Cells["物料编号总"].Value.ToString();  //调整的物料编号

        //    string sel = @"SELECT a.Id as 编号,a.ItemNo as 物料号,  a.ContractCode as 合同编号,a.ApproveCount as 数量, a.IsCheck as 审核, a.IsInStore as 入库,b.mch as 名称,b.xhc as 型号,
        //                          b.gg as 规格,b.dw as 单位, a.CheckPeo as 审核人, a.CheckTime as 审核时间  
        //                   , a.SavePeo as 提交人,a.SaveTime as 提交时间 FROM t_outsource_approve a left join ly_inma0010 b on a.ItemNo=b.wzbh where a.ContractCode='" + nowcontract + "' and a.ItemNo='" + wzbh + "'";

        //    QueryForm queryForm = new QueryForm();
        //    queryForm.Sel = sel;
        //    queryForm.Constr = SQLDatabase.Connectstring;

        //    queryForm.ShowDialog();

        //    if (queryForm.Result4 != "True")
        //    {
        //        MessageBox.Show("未质检审批...", "注意");
        //        return;
        //    }
        //    if (!string.IsNullOrEmpty(queryForm.Result5))
        //    {
        //        if (queryForm.Result5 == "True")
        //        {
        //            MessageBox.Show("已入库...", "注意");
        //            return;
        //        }
        //    }

        //    if (!string.IsNullOrEmpty(queryForm.Result3))
        //    {

        //        decimal tiaozheng = decimal.Parse(queryForm.Result3);//调整的数量 
        //        if (tiaozheng >= 0)
        //        {
        //            MessageBox.Show("退料数量必须小于0", "注意");
        //            return;
        //        }

        //        if (tiaozheng > kucun)
        //        {
        //            MessageBox.Show("调整出库数量已经大于实时库存...", "注意");
        //            return;
        //        }

        //        DataTable dt = GetLingLiao(nowcontract, wzbh);
        //        if (dt == null)
        //        {
        //            MessageBox.Show("该合同没有该物料信息...", "注意");
        //            return;
        //        }
        //        decimal totalQty = 0;//计算总物料需求
        //        for (int i = 0; i < dt.Rows.Count; i++)
        //        {
        //            totalQty += decimal.Parse(dt.Rows[i]["qty"].ToString());
        //        }
        //        //循环插入

        //        string outNum = GetMaxOutNum();//本次出库单号
        //        decimal sum = 0;
        //        int k = 0;

        //        NewFrm.Show(this);
        //        DateTime dttime = DateTime.Now;
        //        for (int i = 0; i < dt.Rows.Count; i++)
        //        {
        //            string paritem = dt.Rows[i]["parentitemno"].ToString();
        //            string lingliaoren = dt.Rows[i]["operator"].ToString();
        //            decimal trueqty = 0;
        //            if (i == dt.Rows.Count - 1) //余数递补
        //            {
        //                trueqty = tiaozheng - sum;
        //            }
        //            else
        //            {
        //                trueqty = Math.Round((decimal.Parse(dt.Rows[i]["qty"].ToString()) / totalQty) * tiaozheng);
        //                sum += trueqty;
        //            }



        //            string sql = @"insert into ly_store_out  ([warehouse] ,[parentitemno] ,[wzbh] ,[qty]  ,[bill_code] ,[out_number] ,[out_deptcode] ,
        //                                 [out_date] ,[remark],[employe] ,[operoter]  ,[finished] ,[out_style])
        //                                 values  ('" + comboBox2.SelectedValue + "','" + paritem + "','" + wzbh + "'," + trueqty + ",'" + nowcontract + "','" + outNum + "','0004','" + dttime
        //                                             + "','外协领料调整','" + lingliaoren + "','" + SQLDatabase.nowUserName() + "',0,'外协领料')";


        //            using (SqlConnection con = new SqlConnection(SQLDatabase.Connectstring))
        //            {

        //                using (SqlCommand cmd = new SqlCommand(sql, con))
        //                {

        //                    con.Open();
        //                    k += cmd.ExecuteNonQuery();
        //                }
        //            }
        //        }

        //        string sqlUp = @"update t_outsource_approve set IsInStore=1 where Id=" + queryForm.Result;


        //        using (SqlConnection con = new SqlConnection(SQLDatabase.Connectstring))
        //        {

        //            using (SqlCommand cmd = new SqlCommand(sqlUp, con))
        //            {

        //                con.Open();
        //                cmd.ExecuteNonQuery();
        //            }
        //        }

        //        this.ly_outsource_order_materialrequisitionSUMTableAdapter.Fill(this.lYOutsourceData.ly_outsource_order_materialrequisitionSUM, nowcontract);

        //        this.ly_outsource_orderSUM_NewTableAdapter.Fill(this.lYOutsourceData.ly_outsource_orderSUM_New, nowcontract);

        //        this.ly_store_outnum_outsourceTableAdapter.Fill(this.lYOutsourceData.ly_store_outnum_outsource, nowcontract);

        //        NewFrm.Hide(this);
        //    }

        //    else
        //    {
        //        return;

        //    }

        //}
        #endregion

        private void dataGridView1_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            DataGridView dgv = sender as DataGridView;
            if (dgv.CurrentRow == null)
                return;

            string people = dgv.CurrentRow.Cells["填写人"].Value.ToString();
            string concode = dgv.CurrentRow.Cells["合同编号总"].Value.ToString();
            string wzbg = dgv.CurrentRow.Cells["物料编号总"].Value.ToString();
            decimal kucun = decimal.Parse(this.dataGridView1.CurrentRow.Cells["库存总"].Value.ToString());//最新库存？
            decimal yiling = decimal.Parse(dataGridView1.CurrentRow.Cells["已领总"].Value.ToString());
            decimal trueOut = decimal.Parse(dataGridView1.CurrentRow.Cells["领料总"].Value.ToString());

            this.ly_outsource_order_materialrequisitionSUMTableAdapter.Fill(this.lYOutsourceData.ly_outsource_order_materialrequisitionSUM, concode);

            this.lyoutsourceorderSUMNewBindingSource.Position = this.lyoutsourceorderSUMNewBindingSource.Find("物料编号", wzbg);

            string nowpeople = SQLDatabase.nowUserName();
            DateTime nowdate = DateTime.Now;
            if (!string.IsNullOrEmpty(people))
            {
                if (people != nowpeople)
                {
                    MessageBox.Show("填写人：" + people + " 操作", "注意");
                    return;
                }
            }
            if ("TrueOutQtyTotal" == dgv.CurrentCell.OwningColumn.Name)
            {
                if (dgv.CurrentRow.Cells["实发"].Value.ToString() != "0.00000" && dgv.CurrentRow.Cells["实发"].Value.ToString() != "")
                {
                    if (yiling != trueOut)
                    {
                        MessageBox.Show("已经填写并且出库，不可更改", "注意");
                        return;
                    }
                }

                ChangeValue queryForm = new ChangeValue();

                queryForm.NewValue = "";
                queryForm.OldValue = dgv.CurrentRow.Cells["TrueOutQtyTotal"].Value.ToString();
                queryForm.ChangeMode = "value";
                queryForm.ShowDialog();
                if (!string.IsNullOrEmpty(queryForm.NewValue))
                {
                    decimal lingliao=decimal.Parse( dgv.CurrentRow.Cells["领料总"].Value.ToString());
                    decimal tureqty =   decimal.Parse(queryForm.NewValue)- lingliao ;
                    if ((tureqty) > kucun)
                    {
                        MessageBox.Show("库存不足"); return;
                    }
                    string ID = dgv.CurrentRow.Cells["编号"].Value.ToString();
                    if (!string.IsNullOrEmpty(ID))
                    {

                        string sql = "update t_outsource_true_storeout set TrueOutQtyTotal="+ decimal.Parse(queryForm.NewValue) + ",TrueOutQty=" + tureqty + " ,SaveTime='" + nowdate
                            + "' where ContractCode='" + concode + "' and ItemNo='" + wzbg + " ' and Id="+ ID;
                        dgv.CurrentRow.Cells["TrueOutQtyTotal"].Value = decimal.Parse(queryForm.NewValue);
                        dgv.CurrentRow.Cells["实发"].Value = tureqty;
                        dgv.CurrentRow.Cells["实发填写时间"].Value = nowdate;
                        using (SqlConnection con = new SqlConnection(SQLDatabase.Connectstring))
                        {

                            using (SqlCommand cmd = new SqlCommand(sql, con))
                            {

                                con.Open();
                                cmd.ExecuteNonQuery();
                            }
                        }
                    }

                    else
                    {
                        string sql = @"INSERT INTO [t_outsource_true_storeout] ([ContractCode] ,[ItemNo],[TrueOutQty],[TrueInQty],[SavePeo],[SaveTime],[TrueOutQtyTotal])
                                      VALUES ('" + concode + "','" + wzbg + "'," + tureqty + ",0,'" + nowpeople + "','" + nowdate + "',"+ decimal.Parse(queryForm.NewValue) + ")";
                        dgv.CurrentRow.Cells["TrueOutQtyTotal"].Value = decimal.Parse(queryForm.NewValue);
                        dgv.CurrentRow.Cells["实发"].Value = tureqty;
                        dgv.CurrentRow.Cells["填写人"].Value = nowpeople;
                        dgv.CurrentRow.Cells["实发填写时间"].Value = nowdate;
                        using (SqlConnection con = new SqlConnection(SQLDatabase.Connectstring))
                        {

                            using (SqlCommand cmd = new SqlCommand(sql, con))
                            {

                                con.Open();
                                cmd.ExecuteNonQuery();
                            }
                        }

                    }
                }
                return;
            }
            if ("实退" == dgv.CurrentCell.OwningColumn.Name)
            {
                if (dgv.CurrentRow.Cells["实退"].Value.ToString() != "0.00000" && dgv.CurrentRow.Cells["实退"].Value.ToString() != "")
                {

                    if (yiling != trueOut)
                    {
                        MessageBox.Show("已经填写并且出库，不可更改", "注意");
                        return;
                    }
                }

                ChangeValue queryForm = new ChangeValue();

                queryForm.NewValue = "";
                queryForm.OldValue = dgv.CurrentRow.Cells["实退"].Value.ToString();
                queryForm.ChangeMode = "value";
                queryForm.ShowDialog();
                if (!string.IsNullOrEmpty(queryForm.NewValue))
                {
                    decimal tureqty = decimal.Parse(queryForm.NewValue);
                    tureqty = 0 - tureqty;
                    if (tureqty > 0)
                    {
                        MessageBox.Show("直接填写退料正数数量", "注意");
                        return;
                    }
                    string ID = dgv.CurrentRow.Cells["编号"].Value.ToString();
                    if (!string.IsNullOrEmpty(ID))
                    {
                        string sql = "update t_outsource_true_storeout set TrueInQty=" + tureqty + " ,SaveTimeIn='" + nowdate
                            + "' where ContractCode='" + concode + "' and ItemNo='" + wzbg + "'  and Id=" + ID;
                        dgv.CurrentRow.Cells["实退"].Value = tureqty;
                        dgv.CurrentRow.Cells["实退填写时间"].Value = nowdate;
                        using (SqlConnection con = new SqlConnection(SQLDatabase.Connectstring))
                        {

                            using (SqlCommand cmd = new SqlCommand(sql, con))
                            {

                                con.Open();
                                cmd.ExecuteNonQuery();
                            }
                        }
                    }

                    else
                    {
                        string sql = @"INSERT INTO [t_outsource_true_storeout] ([ContractCode] ,[ItemNo],[TrueOutQty],[TrueInQty],[SavePeo],[SaveTimeIn])
                                      VALUES ('" + concode + "','" + wzbg + "',0," + tureqty + ",'" + nowpeople + "','" + nowdate + "')";
                        dgv.CurrentRow.Cells["实退"].Value = tureqty;
                        dgv.CurrentRow.Cells["填写人"].Value = nowpeople;
                        dgv.CurrentRow.Cells["实退填写时间"].Value = nowdate;
                        using (SqlConnection con = new SqlConnection(SQLDatabase.Connectstring))
                        {

                            using (SqlCommand cmd = new SqlCommand(sql, con))
                            {

                                con.Open();
                                cmd.ExecuteNonQuery();
                            }
                        }

                    }
                }
                return;
            }


        }

        private void toolStripButton17_Click(object sender, EventArgs e)
        {

            if (null == this.ly_outsource_contractForrequest_ViewDataGridView.CurrentRow)
            {
                return;
            }
            string name = SQLDatabase.nowUserName();
        
            string nowcontract = this.ly_outsource_contractForrequest_ViewDataGridView.CurrentRow.Cells["合同编码"].Value.ToString();
            this.ly_outsource_orderSUM_NewTableAdapter.Fill(this.lYOutsourceData.ly_outsource_orderSUM_New, nowcontract);

            string outNum = GetMaxOutNum();//本次出库单号
            NewFrm.Show(this);
            DateTime dttime = DateTime.Now;
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                decimal trueOut = 0;
                trueOut = decimal.Parse(dataGridView1.Rows[i].Cells["领料总"].Value.ToString());
                if (!string.IsNullOrEmpty(dataGridView1.Rows[i].Cells["实发"].Value.ToString()))
                {
                    trueOut += decimal.Parse(dataGridView1.Rows[i].Cells["实发"].Value.ToString());

                }
 
                decimal yiling = decimal.Parse(dataGridView1.Rows[i].Cells["已领总"].Value.ToString());
                trueOut = (trueOut - yiling);

                decimal kucun = decimal.Parse(this.dataGridView1.Rows[i].Cells["库存总"].Value.ToString());//最新库存？
                string wzbh = this.dataGridView1.Rows[i].Cells["物料编号总"].Value.ToString();  //调整的物料编号
              

                if (trueOut > kucun)
                {
                    continue;
                }
                if (trueOut <=0)
                {
                    continue;
                }

                DataTable dt = GetLingLiao(nowcontract, wzbh);
                if (dt == null)
                {
                    continue;
                }
                decimal totalQty = 0;//计算总物料需求
                for (int j = 0; j< dt.Rows.Count; j++)
                {
                    totalQty += decimal.Parse(dt.Rows[j]["qty"].ToString());
                }
                //循环插入 
              
                decimal sum = 0;

                for (int j = 0; j < dt.Rows.Count; j++)
                {
                    string paritem = dt.Rows[j]["parentitemno"].ToString();
                    string lingliaoren = dt.Rows[j]["operator"].ToString();
                    decimal trueqty = 0;
                    if (j == dt.Rows.Count - 1) //余数递补
                    {
                        trueqty = trueOut - sum;
                    }
                    else
                    {
                        trueqty = Math.Round((decimal.Parse(dt.Rows[j]["qty"].ToString()) / totalQty) * trueOut);
                        sum += trueqty;
                    }


                    string sql = @"insert into ly_store_out  ([warehouse] ,[parentitemno] ,[wzbh] ,[qty]  ,[bill_code] ,[out_number] ,[out_deptcode] ,
                                         [out_date] ,[remark],[employe] ,[operoter]  ,[finished] ,[out_style])
                                         values  ('" + comboBox2.SelectedValue + "','" + paritem + "','" + wzbh + "'," + trueqty + ",'" + nowcontract + "','" + outNum + "','0004','" + dttime
                                                     + "','实发','" + lingliaoren + "','" + name + "',0,'外协领料')";


                    using (SqlConnection con = new SqlConnection(SQLDatabase.Connectstring))
                    {

                        using (SqlCommand cmd = new SqlCommand(sql, con))
                        {

                            con.Open();
                            cmd.ExecuteNonQuery();
                        }
                    }
                } 
            }

 

            this.ly_store_outnum_outsourceTableAdapter.Fill(this.lYOutsourceData.ly_store_outnum_outsource, nowcontract);
            this.ly_outsource_order_materialrequisitionSUMTableAdapter.Fill(this.lYOutsourceData.ly_outsource_order_materialrequisitionSUM, nowcontract);

            this.ly_outsource_orderSUM_NewTableAdapter.Fill(this.lYOutsourceData.ly_outsource_orderSUM_New, nowcontract);

            this.ly_store_outnum_outsourceTableAdapter.Fill(this.lYOutsourceData.ly_store_outnum_outsource, nowcontract);

          
            NewFrm.Hide(this);

        }

        
        private void toolStripButton18_Click_1(object sender, EventArgs e)
        {
            if (null == this.ly_outsource_contractForrequest_ViewDataGridView.CurrentRow)
            {
                return;
            }
            string name = SQLDatabase.nowUserName();

            string nowcontract = this.ly_outsource_contractForrequest_ViewDataGridView.CurrentRow.Cells["合同编码"].Value.ToString();
            this.ly_outsource_orderSUM_NewTableAdapter.Fill(this.lYOutsourceData.ly_outsource_orderSUM_New, nowcontract);

            string outNum = GetMaxOutNum();//本次出库单号
            NewFrm.Show(this);
            DateTime dttime = DateTime.Now;
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                decimal trueOut = 0;
                if (!string.IsNullOrEmpty(dataGridView1.Rows[i].Cells["实退"].Value.ToString()))
                {
                    trueOut += decimal.Parse(dataGridView1.Rows[i].Cells["实退"].Value.ToString());

                }
                else
                {
                    continue;
                }
                if (trueOut >= 0)
                {
                    continue;
                }
                string wzbh = this.dataGridView1.Rows[i].Cells["物料编号总"].Value.ToString();  //调整的物料编号


                DataTable dt = GetLingLiao(nowcontract, wzbh);
                if (dt == null)
                {
                    continue;
                }
                decimal totalQty = 0;//计算总物料需求
                for (int j = 0; j < dt.Rows.Count; j++)
                {
                    totalQty += decimal.Parse(dt.Rows[j]["qty"].ToString());
                }
                //循环插入 

                decimal sum = 0;

                for (int j = 0; j < dt.Rows.Count; j++)
                {
                    string paritem = dt.Rows[j]["parentitemno"].ToString();
                    string lingliaoren = dt.Rows[j]["operator"].ToString();
                    decimal trueqty = 0;
                    if (j == dt.Rows.Count - 1) //余数递补
                    {
                        trueqty = trueOut - sum;
                    }
                    else
                    {
                        trueqty = Math.Round((decimal.Parse(dt.Rows[j]["qty"].ToString()) / totalQty) * trueOut);
                        sum += trueqty;
                    }


                    string sql = @"insert into ly_store_out  ([warehouse] ,[parentitemno] ,[wzbh] ,[qty]  ,[bill_code] ,[out_number] ,[out_deptcode] ,
                                         [out_date] ,[remark],[employe] ,[operoter]  ,[finished] ,[out_style])
                                         values  ('" + comboBox2.SelectedValue + "','" + paritem + "','" + wzbh + "'," + trueqty + ",'" + nowcontract + "','" + outNum + "','0004','" + dttime
                                                     + "','实退','" + lingliaoren + "','" + name + "',0,'外协领料')";


                    using (SqlConnection con = new SqlConnection(SQLDatabase.Connectstring))
                    {

                        using (SqlCommand cmd = new SqlCommand(sql, con))
                        {

                            con.Open();
                            cmd.ExecuteNonQuery();
                        }
                    }
                }
            }



            this.ly_store_outnum_outsourceTableAdapter.Fill(this.lYOutsourceData.ly_store_outnum_outsource, nowcontract);
            this.ly_outsource_order_materialrequisitionSUMTableAdapter.Fill(this.lYOutsourceData.ly_outsource_order_materialrequisitionSUM, nowcontract);

            this.ly_outsource_orderSUM_NewTableAdapter.Fill(this.lYOutsourceData.ly_outsource_orderSUM_New, nowcontract);

            this.ly_store_outnum_outsourceTableAdapter.Fill(this.lYOutsourceData.ly_store_outnum_outsource, nowcontract);
 
            NewFrm.Hide(this);


        }
    }
}