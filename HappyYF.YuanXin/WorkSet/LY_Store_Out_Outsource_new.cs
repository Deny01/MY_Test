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
    public partial class LY_Store_Out_Outsource_new : Form
    {

        private string formState;
        string nowOutNum;
        string nowdate;


        public LY_Store_Out_Outsource_new()
        {
            InitializeComponent();
        }

        private void LY_Machine_Load(object sender, EventArgs e)
        {

            this.dateTimePicker1.Text = DateTime.Today.AddYears(-1).Date.ToString();
            this.dateTimePicker2.Text = DateTime.Today.AddDays(1).Date.ToString();

            //this.dateTimePicker3.Text = DateTime.Today.AddMonths(-3).Date.ToString();
            //this.dateTimePicker4.Text = DateTime.Today.AddDays(1).Date.ToString();

           
            this.ly_storeout_employWarehouse1TableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            this.ly_outsource_order_materialrequisition_newTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            this.ly_outsource_contract_instoreTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;

            this.ly_outsource_contract_instoreTableAdapter.Fill(this.lYOutsourceData.ly_outsource_contract_instore, DateTime.Parse(this.dateTimePicker1.Text).Date, DateTime.Parse(this.dateTimePicker2.Text).Date);
            

            this.ly_store_outnum_outsourceTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
          

            this.ly_store_out_JGTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;


           

            this.radioButton1.Checked = true;

            this.SetFormState("View");

   

        }

     

       

     

        //private bool CheckFinished()
        //{
        //    if ("True" == this.ly_production_order_materialrequisitionDataGridView.CurrentRow.Cells["完成"].Value.ToString())
        //    {
        //        MessageBox.Show("跟单已经完成,操作取消", "注意");
        //        return true;

        //    }
        //    else
        //    {
        //        return false;
        //    }

           
        //}

     
     
     
    

     

      

        //private void toolStripTextBox2_Enter(object sender, EventArgs e)
        //{
        //    toolStripTextBox2.Text = "";

        //    this.ly_production_order_materialrequisitionOutstoreBindingSource.Filter = "";
        //}

        //private void toolStripTextBox2_KeyUp(object sender, KeyEventArgs e)
        //{
        //    string dFilter = "";

        //    dFilter = GetDataGridviewMultiFilterString.DGVMultiFilterString(this.ly_production_order_materialrequisitionDataGridView, this.toolStripTextBox2.Text);

        //    if (null == dFilter)
        //        dFilter = "";

        //    this.ly_production_order_materialrequisitionOutstoreBindingSource.Filter = dFilter;
        //}

        private void toolStripTextBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            //if (e.KeyChar.ToString() == "\r")
            //{
            //    e.Handled = true;



            //}
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.ly_outsource_contract_instoreTableAdapter.Fill(this.lYOutsourceData.ly_outsource_contract_instore, DateTime.Parse(this.dateTimePicker1.Text).Date, DateTime.Parse(this.dateTimePicker2.Text).Date);

        }

       

        private void bindingNavigatorAddNewItem_Click(object sender, EventArgs e)
        {
            //if (CheckFinished()) return;

            //if (null == this.ly_production_order_materialrequisitionDataGridView.CurrentRow) return;



            //string nowmaterialrequisitionnum = this.ly_production_order_materialrequisitionDataGridView.CurrentRow.Cells["领料单号5"].Value.ToString();

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



                //if (!string.IsNullOrEmpty(this.ly_production_order_materialrequisitionDataGridView.CurrentRow.Cells["数量5"].Value.ToString()))
                //{
                //    request_count = decimal.Parse(this.ly_production_order_materialrequisitionDataGridView.CurrentRow.Cells["数量5"].Value.ToString());
                //}
                //else
                //{
                //    request_count = 0;
                //}


                //if (!string.IsNullOrEmpty(this.ly_production_order_inspectionAll_InstoreDataGridView.CurrentRow.Cells["入库数量0"].Value.ToString()))
                //{
                //    instore_count = decimal.Parse(this.ly_production_order_inspectionAll_InstoreDataGridView.CurrentRow.Cells["入库数量0"].Value.ToString());
                //}
                //else
                //{
                //    instore_count = 0;
                //}


                hadget_count = 0;

                if (null != this.ly_store_outDataGridView.CurrentRow)
                {

                    foreach (DataGridViewRow dgr in ly_store_outDataGridView.Rows)
                    {

                        if (string.IsNullOrEmpty(dgr.Cells["领料数量"].Value.ToString())) continue;
                        hadget_count = hadget_count + decimal.Parse(dgr.Cells["领料数量"].Value.ToString());



                    }
                }





                //if (hadget_count >= request_count)
                //{

                //    MessageBox.Show("该领料单数量已全部领完,不能增加领料出库记录", "注意");

                //    return;
                //}

               
                else
                {

                    this.ly_store_out_JGBindingSource.AddNew();

                    //this.ly_store_outDataGridView.CurrentRow.Cells["领料单号"].Value = this.ly_production_order_materialrequisitionDataGridView.CurrentRow.Cells["领料单号5"].Value;
                    //this.ly_store_outDataGridView.CurrentRow.Cells["出库单号"].Value = GetMaxStoreOutnum();

                    //this.ly_store_outDataGridView.CurrentRow.Cells["物料编号"].Value = this.ly_production_order_materialrequisitionDataGridView.CurrentRow.Cells["物料编号5"].Value;
                   

                    //this.ly_store_outDataGridView.CurrentRow.Cells["领料数量"].Value = request_count - hadget_count;
                    //this.ly_store_outDataGridView.CurrentRow.Cells["领料人"].Value = this.ly_production_order_materialrequisitionDataGridView.CurrentRow.Cells["姓名5"].Value;


                    this.ly_store_outDataGridView.CurrentRow.Cells["出库日期"].Value = SQLDatabase.GetNowdate().ToString(); ;

                    this.ly_store_outDataGridView.CurrentRow.Cells["发料人"].Value = SQLDatabase.nowUserName();


                    this.ly_store_outDataGridView.CurrentRow.Cells["出库类别"].Value = "外协生产";

                   


                }






                SaveChanged();

               

                //this.ly_store_outTableAdapter.Fill(this.lYStoreMange.ly_store_out, nowOutNum);
                //this.ly_store_outBindingSource.Position = this.ly_store_outBindingSource.Find("物料编号", componentNum);




            }
        }

        private void SaveChanged()
        {

            this.ly_store_outDataGridView.EndEdit();

            this.Validate();
            this.ly_store_out_JGBindingSource.EndEdit();

            this.ly_store_out_JGTableAdapter.Update(this.lYMaterielRequirements.ly_store_out_JG);



            //string nowmaterialrequisitionnum = "";

            //if (null != this.ly_production_order_materialrequisitionDataGridView.CurrentRow)
            //{
            //    nowmaterialrequisitionnum = this.ly_production_order_materialrequisitionDataGridView.CurrentRow.Cells["领料单号5"].Value.ToString();
            //}

            //////////////////////////////
            int outstore_Id;

            if (null != this.ly_store_outDataGridView.CurrentRow)
            {
                outstore_Id = int.Parse(this.ly_store_outDataGridView.CurrentRow.Cells["outstore_Id"].Value.ToString());
            }
            else
            {
                outstore_Id = 0;
            }


            int request_Id;
 

            string nowcontract = this.ly_outsource_contract_detailQClistDataGridView.CurrentRow.Cells["合同编号0"].Value.ToString();
            string wzbh= this.ly_outsource_contract_detailQClistDataGridView.CurrentRow.Cells["物料编号0"].Value.ToString();


            this.ly_outsource_order_materialrequisition_newTableAdapter.Fill(this.lYOutsourceData.ly_outsource_order_materialrequisition_new, nowcontract,wzbh);
   



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

                if (null != this.ly_outsource_order_materialrequisitionDataGridView.CurrentRow)
                {
                    stanterdnum = decimal.Parse(this.ly_outsource_order_materialrequisitionDataGridView.CurrentRow.Cells["未领数量1"].Value.ToString());
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
        }

        private void SetFormState(string state)
        {
            // view edit add save cancle

            if ("View" == state)
            {
                this.formState = "View";

                this.comboBox2.Enabled = false;
                toolStripButton7.Visible = false;
                button1.Enabled = true;

                this.ly_store_outnumDailyDataGridView.Enabled = true;
                this.ly_outsource_contract_detailQClistDataGridView.Enabled = true;

                this.toolStripButton1.Enabled = true;
                this.toolStripButton2.Enabled = true;
                this.toolStripButton6.Enabled = false;
                this.toolStripButton5.Enabled = false;
                this.toolStripButton4.Enabled = true;
                //this.bindingNavigatorAddNewItem.Enabled = true;



                this.toolStripTextBox1.Enabled = true;



                this.ly_store_outDataGridView.ReadOnly = true;

            }
            else
            {
                this.formState = "Edit";

                this.comboBox2.Enabled = true;
                toolStripButton7.Visible = true;
                button1.Enabled = false;

                this.ly_store_outnumDailyDataGridView.Enabled = false;
                this.ly_outsource_contract_detailQClistDataGridView.Enabled = false;

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


                    //this.ly_material_plan_mainTableAdapter.Fill(this.lYPlanMange.ly_material_plan_main, "SCJH");
                }

                string nowcontract = this.ly_outsource_contract_detailQClistDataGridView.CurrentRow.Cells["合同编号0"].Value.ToString();
                string wzbh = this.ly_outsource_contract_detailQClistDataGridView.CurrentRow.Cells["物料编号0"].Value.ToString();



                this.ly_outsource_order_materialrequisition_newTableAdapter.Fill(this.lYOutsourceData.ly_outsource_order_materialrequisition_new, nowcontract, wzbh);
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

            if (null == this.ly_store_outnumDailyDataGridView.CurrentRow) 
            {
                 this.ly_store_out_JGTableAdapter.Fill(this.lYMaterielRequirements .ly_store_out_JG , "");
                return;
            }

            //int parentId = int.Parse(this.ly_material_plan_mainDataGridView.CurrentRow.Cells["id"].Value.ToString());
            string nowOutNum = this.ly_store_outnumDailyDataGridView.CurrentRow.Cells["出库单号0"].Value.ToString();


            this.ly_store_out_JGTableAdapter.Fill(this.lYMaterielRequirements.ly_store_out_JG, nowOutNum);


            //string outNum = this.ly_store_outnumDataGridView.CurrentRow.Cells["out_number"].Value.ToString();


            //this.ly_store_outTableAdapter.Fill(this.lYStoreMange.ly_store_out, outNum);


            string warehouse = this.ly_store_outnumDailyDataGridView.CurrentRow.Cells["仓库"].Value.ToString();

            if (!string.IsNullOrEmpty(warehouse))
            {
 
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

                this.nowOutNum =GetMaxStoreOutnum();
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

                



                SaveChanged();
 
            }

    
        }

        private void 打印PToolStripButton_Click(object sender, EventArgs e)
        {
            if (this.formState != "View")
            {
                MessageBox.Show("请保存数据后打印...", "注意");
                return;
            }
            
            if (null == this.ly_store_outDataGridView.CurrentRow) return;

            //int parentId = int.Parse(this.ly_material_plan_mainDataGridView.CurrentRow.Cells["id"].Value.ToString());





            frmWaiting.Show(this);

            //this.ly_plan_getmaterialTableAdapter.Fill(this.lYPlanMange.ly_plan_getmaterial, parentId);

            BaseReportView queryForm = new BaseReportView();

            queryForm.Text = "中原精密外协领料出库单";

            queryForm.Printdata = this.lYMaterielRequirements;

            queryForm.PrintCrystalReport = new LY_LingliaodanJG();


            //string selectFormula;

            //selectFormula = "{ly_store_planitemcount.状态}  =   '原料'  and {ly_store_planitemcount.欠料金额}>0 ";
            //queryForm.PrintCrystalReport.DataDefinition.RecordSelectionFormula = selectFormula;

            frmWaiting.Hide(this);

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
            
            this.ly_outsource_contract_instoreBindingSource.Filter = "";
        }

        private void toolStripTextBox3_KeyUp(object sender, KeyEventArgs e)
        {
            string dFilter = "";
            
            dFilter = GetDataGridviewMultiFilterString.DGVMultiFilterString(this.ly_outsource_contract_detailQClistDataGridView, this.toolStripTextBox3.Text);

            if (null == dFilter)
                dFilter = "";

            this.ly_outsource_contract_instoreBindingSource.Filter = dFilter;
        }

 

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton rdb = sender as RadioButton;
            if (rdb.Checked)
            {
                SetFormState("View");
              
                this.ly_store_out_JGTableAdapter.Fill(this.lYMaterielRequirements.ly_store_out_JG, "asd");
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
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (null != this.comboBox2.SelectedValue)
            {


                this.ly_outsource_order_materialrequisition_newBindingSource.Filter = "warehouse='" + this.comboBox2.SelectedValue.ToString() + "'";
                
            }
        }

        private void toolStripButton7_Click(object sender, EventArgs e)
        {
            if (null == this.ly_outsource_contract_detailQClistDataGridView.CurrentRow) return;
            if (null == ly_outsource_order_materialrequisitionDataGridView.CurrentRow) return;
   

            //if ("True" != ly_outsource_contract_detailQClistDataGridView.CurrentRow.Cells["批准"].Value.ToString())
            //{
            //    MessageBox.Show("合同未经批准,不能领料出库...", "注意");
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
           
            if (null == comboBox2.SelectedValue) return;


            string nowcontract = this.ly_outsource_contract_detailQClistDataGridView.CurrentRow.Cells["合同编号0"].Value.ToString();
            string wzbh= this.ly_outsource_contract_detailQClistDataGridView.CurrentRow.Cells["物料编号0"].Value.ToString();

       

            frmWaiting.Show(this);

            SqlConnection sqlConnection1 = new SqlConnection(SQLDatabase.Connectstring);
            SqlCommand cmd = new SqlCommand();


            cmd.Parameters.Add("@parentitemno", SqlDbType.VarChar);
            cmd.Parameters["@parentitemno"].Value = wzbh;

            cmd.Parameters.Add("@contract_num", SqlDbType.VarChar);
            cmd.Parameters["@contract_num"].Value = nowcontract;

           

            cmd.Parameters.Add("@warehousename", SqlDbType.VarChar);
            cmd.Parameters["@warehousename"].Value = comboBox2.SelectedValue;

            string outNum = GetMaxOutNum();
            cmd.Parameters.Add("@out_number", SqlDbType.VarChar);
            cmd.Parameters["@out_number"].Value = outNum;

            cmd.Parameters.Add("@faliaoren", SqlDbType.VarChar);
            cmd.Parameters["@faliaoren"].Value = SQLDatabase.nowUserName();


             
            cmd.CommandText = "LY_store_out_input_outsource_new";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = sqlConnection1;

            sqlConnection1.Open();
            cmd.ExecuteNonQuery();
            sqlConnection1.Close();



            this.ly_store_out_JGTableAdapter.Fill(this.lYMaterielRequirements.ly_store_out_JG, outNum);

            this.ly_outsource_order_materialrequisition_newTableAdapter.Fill(this.lYOutsourceData.ly_outsource_order_materialrequisition_new, nowcontract, wzbh);

            this.ly_store_outnum_outsourceTableAdapter.Fill(this.lYOutsourceData.ly_store_outnum_outsource, nowcontract);
           
        

            frmWaiting.Hide(this);
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

        private void ly_store_outDataGridView_SelectionChanged(object sender, EventArgs e)
        {
            if (null == this.ly_store_outDataGridView.CurrentRow) return;
            if (null == this.ly_outsource_order_materialrequisitionDataGridView.CurrentRow) return;

            DataGridView dgv = sender as DataGridView;

            string nowitem = dgv.CurrentRow.Cells["物料编号"].Value.ToString();

            //ly_outsource_order_materialrequisitionDataGridView.SelectionChanged -= ly_outsource_order_materialrequisitionSUMDataGridView_SelectionChanged;
            //this.ly_outsource_order_materialrequisition_newBindingSource.Position = this.ly_outsource_order_materialrequisition_newBindingSource.Find("物料编号", nowitem);
            //ly_outsource_order_materialrequisitionDataGridView.SelectionChanged += ly_outsource_order_materialrequisitionSUMDataGridView_SelectionChanged;
        }
 

        private void ly_outsource_contract_detailQClistDataGridView_SelectionChanged_1(object sender, EventArgs e)
        {
            if (this.formState == "View")
            {
                if (null != this.ly_outsource_contract_detailQClistDataGridView.CurrentRow)
                {
                    // int parentId = int.Parse(this.ly_material_plan_mainDataGridView.CurrentRow.Cells["id"].Value.ToString());

                    this.ly_storeout_employWarehouse1TableAdapter.Fill(this.lYStoreMange.ly_storeout_employWarehouse1, "asd", SQLDatabase.NowUserID);

                    string nowcontract = this.ly_outsource_contract_detailQClistDataGridView.CurrentRow.Cells["合同编号0"].Value.ToString();

                    string wzbh = this.ly_outsource_contract_detailQClistDataGridView.CurrentRow.Cells["物料编号0"].Value.ToString();


                    this.ly_outsource_order_materialrequisition_newTableAdapter.Fill(this.lYOutsourceData.ly_outsource_order_materialrequisition_new, nowcontract,wzbh);
                    this.ly_store_outnum_outsourceTableAdapter.Fill(this.lYOutsourceData.ly_store_outnum_outsource, nowcontract);


                    this.ly_storeout_employWarehouse1TableAdapter.Fill(this.lYStoreMange.ly_storeout_employWarehouse1, nowcontract, SQLDatabase.NowUserID);
 


                }
            }
            else
            {
                // this.yX_taocan_mainBindingSource.Position = this.nowRow;
            }
        }
    }
}
