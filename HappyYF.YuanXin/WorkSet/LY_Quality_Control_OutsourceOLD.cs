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
using System.Transactions;

namespace HappyYF.YuanXin.WorkSet
{
    public partial class LY_Quality_Control_OutsourceOLD : Form
    {

        string formState = "View";
     
        string InspectionCode = "";
        string nowdate;



        public LY_Quality_Control_OutsourceOLD()
        {
            InitializeComponent();
        }

        private void LY_Machine_Load(object sender, EventArgs e)
        {

           

            this.toolStripTextBox2.Visible = true;
            this.label1.Visible = true;
            this.label2.Visible = true;
            this.dateTimePicker1.Visible = true;
            this.dateTimePicker2.Visible = true;
            this.button1.Visible = true;



           

            this.dateTimePicker1.Text = DateTime.Today.AddMonths(-1).Date.ToString();
            this.dateTimePicker2.Text = DateTime.Today.AddDays(1).Date.ToString();
            //////////////////////////////////////////////////////////////////////////////////////////


            this.ly_outsource_contract_mainQClistTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            this.ly_outsource_contract_detailQCTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;

            this.ly_outsource_contract_inspection_MainTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            this.ly_outsource_contract_inspectionTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;





            this.ly_outsource_contract_mainQClistTableAdapter.Fill(this.lYOutsourceData .ly_outsource_contract_mainQClist, DateTime.Parse(this.dateTimePicker1.Text).Date, DateTime.Parse(this.dateTimePicker2.Text).Date);

            
          

            SetFormState("View");

        }

        private void SetFormState(string state)
        {
            // view edit add save cancle

            //return;

            if ("View" == state)
            {
                this.formState = "View";

                this.ly_purchase_contract_mainlistDataGridView.Enabled = true;
                this.bindingNavigator1.Enabled = true;

                this.toolStripButton2.Enabled = true;
                this.toolStripButton5.Enabled = true;
                this.保存SToolStripButton.Enabled = false;
                this.toolStripButton6.Enabled = true;

                this.ly_purchase_contract_inspection_MainDataGridView.Enabled = true;

                this.button1.Enabled = true;
               





            }
            else
            {
                this.formState = "Edit";

                this.ly_purchase_contract_mainlistDataGridView.Enabled = false ;
                this.bindingNavigator1.Enabled = false;

                this.toolStripButton2.Enabled = false ;
                this.toolStripButton5.Enabled = false ;
                this.保存SToolStripButton.Enabled = true ;
                this.toolStripButton6.Enabled = false ;


                this.ly_purchase_contract_inspection_MainDataGridView.Enabled = false ;

                this.button1.Enabled = false ;

            }


        }

      

       



        private bool CheckFinished()
        {

            if (0 < decimal .Parse ( this.ly_purchase_contract_inspectionDataGridView.CurrentRow .Cells ["入库数量8"].Value .ToString ()))
            {
                MessageBox.Show("已有入库记录,操作取消", "注意");
                return true;

            }
            else
            {
                return false;
            }
          
        }

     
        private string GetMaxContractInspectionCode()
        {
           

            SqlConnection sqlConnection1 = new SqlConnection(SQLDatabase.Connectstring);
            SqlCommand cmd = new SqlCommand();

            string MaxInspectionNum = "";

            //cmd.Parameters.Add("@Production_mode", SqlDbType.VarChar);
            //cmd.Parameters["@Production_mode"].Value = "ZJJG";


            cmd.CommandText = "LY_GetMax_OutsourceInspectionCode";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = sqlConnection1;

            sqlConnection1.Open();
            MaxInspectionNum = cmd.ExecuteScalar().ToString();
            sqlConnection1.Close();



            return MaxInspectionNum;
        }

        private void SaveChanged()
        {

          

            string nowcontractcode = this.ly_purchase_contract_mainlistDataGridView.CurrentRow.Cells["合同编号1"].Value.ToString();
            string componentNum = ly_purchase_contract_inspectionDataGridView.CurrentRow.Cells["物料编号8"].Value.ToString();

            this.ly_purchase_contract_inspectionDataGridView.EndEdit();
            this.ly_outsource_contract_inspectionBindingSource.EndEdit();
            this.ly_outsource_contract_inspectionTableAdapter.Update(this.lYOutsourceData .ly_outsource_contract_inspection);

            this.ly_outsource_contract_detailQCTableAdapter.Fill(this.lYOutsourceData .ly_outsource_contract_detailQC, nowcontractcode);
            this.ly_outsource_contract_detailQCBindingSource.Position = this.ly_outsource_contract_detailQCBindingSource.Find("物料编号", componentNum);

        
           
        }

        private void ly_production_order_inspectionDataGridView_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {

            if (CheckFinished()) return;

            DataGridView dgv = sender as DataGridView;

          
          

            if ("送检数量8" == dgv.CurrentCell.OwningColumn.Name)
            {

                ChangeValue queryForm = new ChangeValue();

                queryForm.OldValue = dgv.CurrentCell.Value.ToString();
                queryForm.NewValue = "";
                queryForm.ChangeMode = "value";
                queryForm.ShowDialog();




                if (queryForm.NewValue != "")
                {
                    dgv.CurrentRow.Cells["送检数量8"].Value = queryForm.NewValue;

                    ///////////////////////////////////////////////////////////////////////////////////////////////
                    //if (!string.IsNullOrEmpty(queryForm.NewValue))
                    //{
                    //    inspect_count = decimal.Parse(queryForm.NewValue);
                    //}
                    //else
                    //{
                    //    inspect_count = 0;
                    //}
                  
                    //if (!string.IsNullOrEmpty(dgv.CurrentRow.Cells["合格数量8"].Value.ToString()))
                    //{
                    //    qualified_count = decimal.Parse(dgv.CurrentRow.Cells["合格数量8"].Value.ToString());
                    //}
                    //else
                    //{
                    //    qualified_count = 0;
                    //}

                    //if (!string.IsNullOrEmpty(dgv.CurrentRow.Cells["可用数量8"].Value.ToString()))
                    //{
                    //    canuse_count = decimal.Parse(dgv.CurrentRow.Cells["可用数量8"].Value.ToString());
                    //}
                    //else
                    //{
                    //    canuse_count = 0;
                    //}

                    //waste_count = inspect_count - qualified_count - canuse_count;

                   

                    //decimal send_inspect_count = 0;

                    //if (null != this.ly_production_order_inspectionAll_MainDataGridView.CurrentRow)
                    //{

                    //    foreach (DataGridViewRow dgr in ly_production_order_inspectionAll_MainDataGridView.Rows)
                    //    {

                    //        if (string.IsNullOrEmpty(dgr.Cells["送检"].Value.ToString())) continue;
                    //        send_inspect_count = send_inspect_count + decimal.Parse(dgr.Cells["送检"].Value.ToString());



                    //    }
                    //}

                    //if (1 > inspect_count)
                    //{

                    //    MessageBox.Show("末序无序检记录,不能增加总检记录", "注意");

                    //    return;
                    //}

                    //if (send_inspect_count >= inspect_count)
                    //{
                    //    //dgv.CurrentRow.Cells["送检"].Value = queryForm.OldValue;

                    //    if (string.IsNullOrEmpty(queryForm.OldValue))
                    //    {
                    //        dgv.CurrentRow.Cells["送检"].Value = DBNull.Value;
                    //    }
                    //    else
                    //    {
                    //        dgv.CurrentRow.Cells["送检"].Value = queryForm.OldValue;
                    //    }
                    //    MessageBox.Show("末序数量已经全部送交总检,不能增加总检记录", "注意");

                    //    return;
                    //}


                    ////////////////////////////////////////////////////////////////////////////////////////////

                    if (!CheckInput(dgv))
                    {
                        if (string.IsNullOrEmpty(queryForm.OldValue))
                        {
                            dgv.CurrentRow.Cells["送检数量8"].Value = DBNull.Value;
                        }
                        else
                        {
                            dgv.CurrentRow.Cells["送检数量8"].Value = queryForm.OldValue;
                        }
                        return;
                    }
                    
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


            ///////////////////////////////////////////////////////
            if ("合格数量8" == dgv.CurrentCell.OwningColumn.Name)
            {

                ChangeValue queryForm = new ChangeValue();

                queryForm.OldValue = dgv.CurrentCell.Value.ToString();
                queryForm.NewValue = "";
                queryForm.ChangeMode = "value";
                queryForm.ShowDialog();




                if (queryForm.NewValue != "")
                {
                    dgv.CurrentRow.Cells["合格数量8"].Value = queryForm.NewValue;


                    if (!CheckInput(dgv))
                    {
                        if (string.IsNullOrEmpty(queryForm.OldValue))
                        {
                            dgv.CurrentRow.Cells["合格数量8"].Value = DBNull.Value;
                        }
                        else
                        {
                            dgv.CurrentRow.Cells["合格数量8"].Value = queryForm.OldValue;
                        }
                        return;
                    }



                    SaveChanged();




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

            ///////////////////////////////////////////////////////////////////////////////
          

//            if ("检验日期" == dgv.CurrentCell.OwningColumn.Name)
//            {

//                ChangeValue queryForm = new ChangeValue();

//                queryForm.OldValue = dgv.CurrentCell.Value.ToString();
//                queryForm.NewValue = "";
//                queryForm.ChangeMode = "datetime";
//                queryForm.ShowDialog();




//                if (queryForm.NewValue != "")
//                {
//                    dgv.CurrentRow.Cells["检验日期"].Value = queryForm.NewValue;
//                    //dgv.CurrentRow.Cells["discount_money"].Value = DBNull.Value;

//                    //dgv.CurrentRow.Cells["approve_flag"].Value = "False";
//                    SaveChanged();


//                    //CountPlanStru();

//                }
//                else
//                {
                   

//                }
//                return;
                
                
//                /////////////////////////////////////

//                //DatePicker queryForm = new DatePicker();
//                //queryForm.Pt = pt;

//                //if (null != (dgv.CurrentCell.Value))
//                //    queryForm.NowDate = dgv.CurrentCell.Value.ToString();

//                //queryForm.ShowDialog();



//                //if (null != queryForm.NowDate)
//                //{

//                //    dgv.CurrentRow.Cells["检验日期"].Value = queryForm.NowDate;
//                //    SaveChanged();

//                //}
//                //return;
//            }









            ///////////////////////////////////////////////////////

            if ("检验说明8" == dgv.CurrentCell.OwningColumn.Name)
            {

                ChangeValue queryForm = new ChangeValue();

                queryForm.OldValue = dgv.CurrentCell.Value.ToString();
                queryForm.NewValue = "";
                queryForm.ChangeMode = "longstring";
                queryForm.ShowDialog();




                if (queryForm.NewValue != "")
                {
                    dgv.CurrentRow.Cells["检验说明8"].Value = queryForm.NewValue;
                    //dgv.CurrentRow.Cells["discount_money"].Value = DBNull.Value;

                    //dgv.CurrentRow.Cells["approve_flag"].Value = "False";
                    SaveChanged();


                    //CountPlanStru();

                }
                else
                {

                }
                return;

            }

            ///////////////////////////////////////////////////////
            //if ("可用数量8" == dgv.CurrentCell.OwningColumn.Name)
            //{

            //    ChangeValue queryForm = new ChangeValue();

            //    queryForm.OldValue = dgv.CurrentCell.Value.ToString();
            //    queryForm.NewValue = "";
            //    queryForm.ChangeMode = "value";
            //    queryForm.ShowDialog();




            //    if (queryForm.NewValue != "")
            //    {
            //        dgv.CurrentRow.Cells["可用数量8"].Value = queryForm.NewValue;


            //        if (!CheckCanuse(dgv))
            //        {
            //            if (string.IsNullOrEmpty(queryForm.OldValue))
            //            {
            //                dgv.CurrentRow.Cells["可用数量8"].Value = DBNull.Value;
            //            }
            //            else
            //            {
            //                dgv.CurrentRow.Cells["可用数量8"].Value = queryForm.OldValue;
            //            }
            //            return;
            //        }

            //        dgv.CurrentRow.Cells["审查日期8"].Value = SQLDatabase.GetNowdate().ToString();

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

            ///////////////////////////////

            //if ("审查意见8" == dgv.CurrentCell.OwningColumn.Name)
            //{

            //    ChangeValue queryForm = new ChangeValue();

            //    queryForm.OldValue = dgv.CurrentCell.Value.ToString();
            //    queryForm.NewValue = "";
            //    queryForm.ChangeMode = "longstring";
            //    queryForm.ShowDialog();




            //    if (queryForm.NewValue != "")
            //    {
            //        dgv.CurrentRow.Cells["审查意见8"].Value = queryForm.NewValue;
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

            ///////////////////////////////
            ///////////////////////////////////////////////////////////////////////////////////


            //if ("审查日期8" == dgv.CurrentCell.OwningColumn.Name)
            //{

            //    ChangeValue queryForm = new ChangeValue();

            //    queryForm.OldValue = dgv.CurrentCell.Value.ToString();
            //    queryForm.NewValue = "";
            //    queryForm.ChangeMode = "datetime";
            //    queryForm.ShowDialog();




            //    if (queryForm.NewValue != "")
            //    {
            //        dgv.CurrentRow.Cells["审查日期8"].Value = queryForm.NewValue;
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


            ///////////////////////////////////////
        }

        private bool  CheckInput(DataGridView dgv)
        {
            decimal  qualified_count;
            decimal inspect_count;
            decimal canuse_count;


            if (!string.IsNullOrEmpty(dgv.CurrentRow.Cells["送检数量8"].Value.ToString()))
            {
                inspect_count = decimal.Parse(dgv.CurrentRow.Cells["送检数量8"].Value.ToString());
            }
            else
            {
                inspect_count = 0;
            }

            if (!string.IsNullOrEmpty(dgv.CurrentRow.Cells["合格数量8"].Value.ToString()))
            {
                qualified_count = decimal.Parse(dgv.CurrentRow.Cells["合格数量8"].Value.ToString());
            }
            else
            {
                qualified_count = 0;
            }

            if (!string.IsNullOrEmpty(dgv.CurrentRow.Cells["可用数量8"].Value.ToString()))
            {
                canuse_count = decimal.Parse(dgv.CurrentRow.Cells["可用数量8"].Value.ToString());
            }
            else
            {
                canuse_count = 0;
            }

            if ((qualified_count + canuse_count) > inspect_count)
            {
                MessageBox.Show("合格数量不能大于送检数量", "注意");

                return false;
            }
            else
            {
                dgv.CurrentRow.Cells["废品数量8"].Value = inspect_count - ( qualified_count + canuse_count);
                return true;
            }
        }

        private bool CheckCanuse(DataGridView dgv)
        {
            decimal qualified_count;
            decimal inspect_count;
          


            if (!string.IsNullOrEmpty(dgv.CurrentRow.Cells["送检数量8"].Value.ToString()))
            {
                inspect_count = decimal.Parse(dgv.CurrentRow.Cells["送检数量8"].Value.ToString());
            }
            else
            {
                inspect_count = 0;
            }

            if (!string.IsNullOrEmpty(dgv.CurrentRow.Cells["合格数量8"].Value.ToString()))
            {
                qualified_count = decimal.Parse(dgv.CurrentRow.Cells["合格数量8"].Value.ToString());
            }
            else
            {
                qualified_count = 0;
            }
            
            
            
            decimal waste_count;
            decimal canuse_count;


            if (!string.IsNullOrEmpty(dgv.CurrentRow.Cells["废品数量8"].Value.ToString()))
            {
                waste_count = decimal.Parse(dgv.CurrentRow.Cells["废品数量8"].Value.ToString());
            }
            else
            {
                waste_count = 0;
            }

            if (!string.IsNullOrEmpty(dgv.CurrentRow.Cells["可用数量8"].Value.ToString()))
            {
                canuse_count = decimal.Parse(dgv.CurrentRow.Cells["可用数量8"].Value.ToString());
            }
            else
            {
                canuse_count = 0;
            }

            if (canuse_count > (inspect_count - qualified_count))
            {
                MessageBox.Show("可用数量不能大于废品数量", "注意");

                return false;
            }
            else
            {
                dgv.CurrentRow.Cells["废品数量8"].Value = inspect_count - (qualified_count + canuse_count);
                dgv.CurrentRow.Cells["技术审查8"].Value =SQLDatabase.nowUserName();
                dgv.CurrentRow.Cells["审查日期8"].Value = SQLDatabase.GetNowdate().ToString();

                
                return true;
            }
        }

      
     

        private bool CheckJudged(DataGridView dgv)
        {


          return true;

           // decimal std_canuse_count;
           // decimal std_waste_count;



           // if (!string.IsNullOrEmpty(ly_production_order_inspectionAll_MainDataGridView.CurrentRow.Cells["可用"].Value.ToString()))
           // {
           //     std_canuse_count = decimal.Parse(ly_production_order_inspectionAll_MainDataGridView.CurrentRow.Cells["可用"].Value.ToString());
           // }
           // else
           // {
           //     std_canuse_count = 0;
           // }

           // if (!string.IsNullOrEmpty(ly_production_order_inspectionAll_MainDataGridView.CurrentRow.Cells["废品"].Value.ToString()))
           // {
           //     std_waste_count = decimal.Parse(ly_production_order_inspectionAll_MainDataGridView.CurrentRow.Cells["废品"].Value.ToString());
           // }
           // else
           // {
           //     std_waste_count = 0;
           // }



           // decimal waste_count = 0;
           // decimal canuse_count = 0;


           

           // if (null != dgv.CurrentRow)
           // {

           //     foreach (DataGridViewRow dgr in dgv.Rows)
           //     {

           //         if (string.IsNullOrEmpty(dgr.Cells["废品数量"].Value.ToString())) continue;
           //         waste_count = waste_count + decimal.Parse(dgr.Cells["废品数量"].Value.ToString());



           //     }
           // }

           // if (null != dgv.CurrentRow)
           // {

           //     foreach (DataGridViewRow dgr in dgv.Rows)
           //     {

           //         if (string.IsNullOrEmpty(dgr.Cells["可用数量"].Value.ToString())) continue;
           //         canuse_count = canuse_count + decimal.Parse(dgr.Cells["可用数量"].Value.ToString());



           //     }
           // }




           /////////////////////////////////////////////////////////////



           // if (waste_count > std_waste_count || canuse_count > std_canuse_count)
           // {
           //     MessageBox.Show("废品判定数量大于总检废品数量,请检验输入数据...", "注意");

           //     return false;
           // }
           // else
           // {
              
           //     return true;
           // }
        }

        private void button1_Click(object sender, EventArgs e)
        {

            this.ly_outsource_contract_mainQClistTableAdapter.Fill(this.lYOutsourceData .ly_outsource_contract_mainQClist , DateTime.Parse(this.dateTimePicker1.Text).Date, DateTime.Parse(this.dateTimePicker2.Text).Date);
        }


        private void toolStripTextBox2_Enter(object sender, EventArgs e)
        {
            toolStripTextBox2.Text = "";

            this.ly_outsource_contract_mainQClistBindingSource.Filter = "";
        }

        private void toolStripTextBox2_KeyUp(object sender, KeyEventArgs e)
        {
            string dFilter = "";

            dFilter = GetDataGridviewMultiFilterString.DGVMultiFilterString(this.ly_purchase_contract_mainlistDataGridView, this.toolStripTextBox2.Text);

            if (null == dFilter)
                dFilter = "";

            this.ly_outsource_contract_mainQClistBindingSource.Filter = dFilter;
        }

        private void lY_productionorder_listDataGridView_SelectionChanged(object sender, EventArgs e)
        {
            


            ////////////////////////////////////////////////////

            if (null == ly_purchase_contract_mainlistDataGridView.CurrentRow)
            {
                this.ly_outsource_contract_detailQCTableAdapter.Fill(this.lYOutsourceData .ly_outsource_contract_detailQC , "");
             

                return;
            }


            string nowcontractcode = this.ly_purchase_contract_mainlistDataGridView.CurrentRow.Cells["合同编号1"].Value.ToString();
            //string nowsuppliercode = this.ly_purchase_contract_mainlistDataGridView.CurrentRow.Cells["供应商编号6"].Value.ToString();

            int nowplanid = int.Parse(this.ly_purchase_contract_mainlistDataGridView.CurrentRow.Cells["plan_id"].Value.ToString());




            this.ly_outsource_contract_detailQCTableAdapter.Fill(this.lYOutsourceData.ly_outsource_contract_detailQC, nowcontractcode);
            this.ly_outsource_contract_inspection_MainTableAdapter.Fill(this.lYOutsourceData .ly_outsource_contract_inspection_Main , nowcontractcode);

            //ly_purchase_contract_inspection_MainTableAdapter

            ////////////////////////////////////////////////////////
         

              
        }

       

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            this.SetFormState("Edit");
            //this.nowInNum = GetMaxStoreInnum();  GetMaxContractInspectionCode()
            this.InspectionCode = "NewInspectionCode";
            //this.nowdate = DateTime.Now.Date.ToString();
            this.nowdate = SQLDatabase.GetNowdate().ToString();
            this.ly_outsource_contract_inspectionTableAdapter.Fill(this.lYOutsourceData .ly_outsource_contract_inspection , "");
        }

        private void ly_purchase_contract_inspection_MainDataGridView_SelectionChanged(object sender, EventArgs e)
        {


            ////////////////////////////////////////////////////

            if (null == ly_purchase_contract_inspection_MainDataGridView.CurrentRow)
            {
                this.ly_outsource_contract_inspectionTableAdapter.Fill(this.lYOutsourceData.ly_outsource_contract_inspection, "");


                return;
            }


            InspectionCode = this.ly_purchase_contract_inspection_MainDataGridView.CurrentRow.Cells["检验单号"].Value.ToString();



            this.ly_outsource_contract_inspectionTableAdapter.Fill(this.lYOutsourceData.ly_outsource_contract_inspection, InspectionCode);

            ////////////////////////////////////////////////////////
         
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {


            /////////////////////////////

            if (null == ly_purchase_contract_inspection_MainDataGridView.CurrentRow)
            {
                this.ly_outsource_contract_inspectionTableAdapter.Fill(this.lYOutsourceData.ly_outsource_contract_inspection, "");


                return;
            }



            string nowoperator = this.ly_purchase_contract_inspection_MainDataGridView.CurrentRow.Cells["质检员"].Value.ToString();

            if (nowoperator != SQLDatabase.nowUserName())
            {
                MessageBox.Show("请质检员:" + nowoperator + "修改", "注意");
                return;
            }

            //if ("True" == ly_store_outnumDailyDataGridView.CurrentRow.Cells["签证"].Value.ToString())
            //{
            //    MessageBox.Show("已经入库,不能修改检测数据...");

            //    return;

            //}




            this.InspectionCode = this.ly_purchase_contract_inspection_MainDataGridView.CurrentRow.Cells["检验单号"].Value.ToString();
            this.nowdate = this.ly_purchase_contract_inspection_MainDataGridView.CurrentRow.Cells["检验日期"].Value.ToString();


            this.SetFormState("Edit");
        }

        private void toolStripButton6_Click(object sender, EventArgs e)
        {

            if (null == ly_purchase_contract_inspection_MainDataGridView.CurrentRow)
            {
                this.ly_outsource_contract_inspectionTableAdapter.Fill(this.lYOutsourceData.ly_outsource_contract_inspection, "");


                return;
            }



            string nowoperator = this.ly_purchase_contract_inspection_MainDataGridView.CurrentRow.Cells["质检员"].Value.ToString();

            if (nowoperator != SQLDatabase.nowUserName())
            {
                MessageBox.Show("请质检员:" + nowoperator + "删除", "注意");
                return;
            }

            //if ("True" == ly_store_outnumDailyDataGridView.CurrentRow.Cells["签证"].Value.ToString())
            //{
            //    MessageBox.Show("已经入库,不能删除检验单...");

            //    return;

            //}


            //////////////////

            this.InspectionCode = this.ly_purchase_contract_inspection_MainDataGridView.CurrentRow.Cells["检验单号"].Value.ToString();


            string message = "删除当前检验单:" + InspectionCode + "吗？";
            string caption = "提示...";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;

            DialogResult result;



            result = MessageBox.Show(message, caption, buttons,
            MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

            if (result == DialogResult.Yes)
            {

                string delstr = " delete ly_outsource_contract_inspection  from ly_outsource_contract_inspection  " +
                    " where contract_inspection_code = '" + InspectionCode + "'";


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

                this.ly_outsource_contract_inspection_MainBindingSource.RemoveCurrent();





            }
        }

        private void 保存SToolStripButton_Click(object sender, EventArgs e)
        {

            string nowcontractcode = this.ly_purchase_contract_mainlistDataGridView.CurrentRow.Cells["合同编号1"].Value.ToString();



            this.ly_outsource_contract_detailQCTableAdapter.Fill(this.lYOutsourceData.ly_outsource_contract_detailQC, nowcontractcode);

            this.SetFormState("View");
        }

        private void ly_purchase_contract_detailDataGridView_DoubleClick(object sender, EventArgs e)
        {
            if (this.formState == "View") return;
            if (null == ly_purchase_contract_detailDataGridView.CurrentRow) return;

            string componentNum = this.ly_purchase_contract_detailDataGridView.CurrentRow.Cells["物料编号7"].Value.ToString();
            string nowcontractcode = this.ly_purchase_contract_detailDataGridView.CurrentRow.Cells["合同编号7"].Value.ToString();
            string nowInspectQty = this.ly_purchase_contract_detailDataGridView.CurrentRow.Cells["未检数量"].Value.ToString();

            string nowid2 = this.ly_purchase_contract_detailDataGridView.CurrentRow.Cells["id2"].Value.ToString();
            string nowpartname = this.ly_purchase_contract_detailDataGridView.CurrentRow.Cells["方式"].Value.ToString();
            string nowinstoreflag = this.ly_purchase_contract_detailDataGridView.CurrentRow.Cells["入库"].Value.ToString();

            if ("True" == nowinstoreflag)
            {
                nowinstoreflag = "1";
            }
            else
            {
                nowinstoreflag = "0";

            }

            if (0 >= decimal.Parse ( nowInspectQty))
            {
                MessageBox.Show("外协物料已全部送检,操作取消", "注意");
                return;
            }


            int hadarranged = ly_outsource_contract_inspectionBindingSource.Find("物料编号", componentNum);

            if (-1 < hadarranged)
            {
                MessageBox.Show("当前检验单已经安排该物料,修改该物料数量即可...", "注意");
                return;

            }

            string newFlag = "N";

            if (this.InspectionCode == "NewInspectionCode")
            {

                this.InspectionCode = GetMaxContractInspectionCode();
                newFlag = "Y";
            }
            else
            {

                newFlag = "N";
            }





            string insStr = " INSERT INTO ly_outsource_contract_inspection  " +
         "( contract_code,contract_inspection_code,itemno,inspect_count,inspect_date,quality_inspector,id2,part_name,instore_flag) " +
         " values ('" + nowcontractcode + "','" + InspectionCode + "','" + componentNum + "'," + nowInspectQty + ",'" + nowdate + "','" + SQLDatabase.nowUserName() + "'," + nowid2 + ",'" + nowpartname + "'," + nowinstoreflag + " )";



            using (TransactionScope scope = new TransactionScope())
            {

                SqlConnection sqlConnection1 = new SqlConnection(SQLDatabase.Connectstring);
                SqlCommand cmd = new SqlCommand();

                cmd.CommandText = insStr;
                cmd.CommandType = CommandType.Text;
                cmd.Connection = sqlConnection1;



                sqlConnection1.Open();
                cmd.ExecuteNonQuery();

                sqlConnection1.Close();

                scope.Complete();
            }


         

            //if (newFlag == "Y")
            //{
            //    this.ly_store_outnumDailyDataGridView.SelectionChanged -= ly_store_innumDataGridView_SelectionChanged;
            //    this.ly_store_outnumDailyTableAdapter.Fill(this.lYStoreMange.ly_store_outnumDaily, DateTime.Parse(this.dateTimePicker1.Text).Date, DateTime.Now.AddDays(1).Date, SQLDatabase.NowUserID);
            //    this.ly_store_outnumDailyBindingSource.Position = this.ly_store_outnumDailyBindingSource.Find("出库单号", nowOutNum);
            //    this.ly_store_outnumDailyDataGridView.SelectionChanged += ly_store_innumDataGridView_SelectionChanged;

            //}

            //if (null == ly_purchase_contract_inspection_MainDataGridView.CurrentRow)
            //{
            //    this.ly_purchase_contract_inspectionTableAdapter.Fill(this.lYQualityInspector.ly_purchase_contract_inspection, "");


            //    return;
            //}

            //string nowcontractcode = this.ly_purchase_contract_mainlistDataGridView.CurrentRow.Cells["合同编号1"].Value.ToString();
            this.ly_outsource_contract_detailQCTableAdapter.Fill(this.lYOutsourceData.ly_outsource_contract_detailQC, nowcontractcode);
            this.ly_outsource_contract_detailQCBindingSource.Position = this.ly_outsource_contract_detailQCBindingSource.Find("物料编号", componentNum);

            //this.InspectionCode = this.ly_purchase_contract_inspection_MainDataGridView.CurrentRow.Cells["检验单号"].Value.ToString();

            this.ly_purchase_contract_inspection_MainDataGridView.SelectionChanged -= this.ly_purchase_contract_inspection_MainDataGridView_SelectionChanged;
            this.ly_outsource_contract_inspection_MainTableAdapter.Fill(this.lYOutsourceData .ly_outsource_contract_inspection_Main, nowcontractcode);
            this.ly_outsource_contract_inspection_MainBindingSource.Position = this.ly_outsource_contract_inspection_MainBindingSource.Find("检验单号", InspectionCode);
            this.ly_purchase_contract_inspection_MainDataGridView.SelectionChanged += this.ly_purchase_contract_inspection_MainDataGridView_SelectionChanged;

            this.ly_outsource_contract_inspectionTableAdapter.Fill(this.lYOutsourceData .ly_outsource_contract_inspection , InspectionCode);

            this.ly_outsource_contract_inspectionBindingSource.Position = this.ly_outsource_contract_inspectionBindingSource.Find("物料编号", componentNum);
        }

        private void 删除ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.formState == "View") return;

            //if ("True" == ly_store_outDataGridView.CurrentRow.Cells["finished1"].Value.ToString())
            //{
            //    MessageBox.Show("已经入库,不能删除   ...");

            //    return;

            //}

            if (null == this.ly_purchase_contract_inspectionDataGridView.CurrentRow)
            {
                return;
            }


            string nowoperptar = this.ly_purchase_contract_inspectionDataGridView.CurrentRow.Cells["质检员8"].Value.ToString();

            if (nowoperptar != SQLDatabase.nowUserName())
            {
                MessageBox.Show("请质检员:" + nowoperptar + "删除", "注意");
                return;
            }



            string message = "确定删除当前物料记录吗？";
            string caption = "提示...";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result;



            result = MessageBox.Show(message, caption, buttons,
            MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

            if (result == DialogResult.Yes)
            {
                string nowcontractcode = this.ly_purchase_contract_mainlistDataGridView.CurrentRow.Cells["合同编号1"].Value.ToString();

                this.ly_outsource_contract_inspectionBindingSource.RemoveCurrent();
                this.ly_outsource_contract_inspectionTableAdapter.Update(this.lYOutsourceData.ly_outsource_contract_inspection);

                this.ly_purchase_contract_inspection_MainDataGridView.SelectionChanged -= this.ly_purchase_contract_inspection_MainDataGridView_SelectionChanged;
                this.ly_outsource_contract_inspection_MainTableAdapter.Fill(this.lYOutsourceData.ly_outsource_contract_inspection_Main, nowcontractcode);
                this.ly_outsource_contract_inspection_MainBindingSource.Position = this.ly_outsource_contract_inspection_MainBindingSource.Find("检验单号", InspectionCode);
                this.ly_purchase_contract_inspection_MainDataGridView.SelectionChanged += this.ly_purchase_contract_inspection_MainDataGridView_SelectionChanged;


              


            }
        }

        private void SaveIndate(string indate)
        {
            string inspectnumber = this.ly_purchase_contract_inspection_MainDataGridView.CurrentRow.Cells["检验单号"].Value.ToString();

            string delstr = " update ly_outsource_contract_inspection  set inspect_date='" + indate + "'" +
                " from ly_outsource_contract_inspection " +
               " where ly_outsource_contract_inspection.contract_inspection_code = '" + inspectnumber + "'";


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

        private void ly_purchase_contract_inspection_MainDataGridView_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            DataGridView dgv = sender as DataGridView;

            if (SQLDatabase.nowUserName() != dgv.CurrentRow.Cells["质检员"].Value.ToString())
            {

                MessageBox.Show("请质检员:" + dgv.CurrentRow.Cells["质检员"].Value.ToString() + " 修改");

                return;
            }



            /////////////////////////////////////////////////////
            if ("检验日期" == dgv.CurrentCell.OwningColumn.Name)
            {
                //if ("True" == dgv.CurrentRow.Cells["签证"].Value.ToString())
                //{
                //    MessageBox.Show("已经签证,不能修改入库日期...");

                //    return;

                //}



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

       
      

     ////////////////////////////////////////////////////

       
        

     
      
    }
}
