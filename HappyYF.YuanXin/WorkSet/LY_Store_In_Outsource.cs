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

namespace HappyYF.YuanXin.WorkSet
{
    public partial class LY_Store_In_Outsource : Form
    {


        public LY_Store_In_Outsource()
        {
            InitializeComponent();
        }

        private void LY_Machine_Load(object sender, EventArgs e)
        {

            this.dateTimePicker1.Text = DateTime.Today.AddMonths(-3).Date.ToString();
            this.dateTimePicker2.Text = DateTime.Today.AddDays(1).Date.ToString();

          

            this.ly_outsource_order_inspection_InstoreTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
           
            this.ly_store_in_WXTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;

            this.ly_outsource_order_inspection_InstoreTableAdapter.Fill(this.lYOutsourceData.ly_outsource_order_inspection_Instore, DateTime.Parse(this.dateTimePicker1.Text).Date, DateTime.Parse(this.dateTimePicker2.Text).Date,SQLDatabase .NowUserID);
          
          

      

        }

     

       

     

        private bool CheckFinished()
        {
            if ("True" == this.ly_production_order_inspectionAll_InstoreDataGridView.CurrentRow.Cells["完成"].Value.ToString())
            {
                MessageBox.Show("跟单已经完成,操作取消", "注意");
                return true;

            }
            else
            {
                return false;
            }

           
        }

     
     
     
    

     

      

        private void toolStripTextBox2_Enter(object sender, EventArgs e)
        {
            toolStripTextBox2.Text = "";

            this.ly_outsource_order_inspection_InstoreBindingSource.Filter = "";
        }

        private void toolStripTextBox2_KeyUp(object sender, KeyEventArgs e)
        {
            string dFilter = "";

            dFilter = GetDataGridviewMultiFilterString.DGVMultiFilterString(this.ly_production_order_inspectionAll_InstoreDataGridView, this.toolStripTextBox2.Text);

            if (null == dFilter)
                dFilter = "";

            this.ly_outsource_order_inspection_InstoreBindingSource.Filter = dFilter;
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
            this.ly_outsource_order_inspection_InstoreTableAdapter.Fill(this.lYOutsourceData.ly_outsource_order_inspection_Instore, DateTime.Parse(this.dateTimePicker1.Text).Date, DateTime.Parse(this.dateTimePicker2.Text).Date, SQLDatabase.NowUserID);
        }

        private void ly_production_order_inspectionAll_InstoreDataGridView_SelectionChanged(object sender, EventArgs e)
        {
            if (null != this.ly_production_order_inspectionAll_InstoreDataGridView.CurrentRow)
            {


                string nowproductionorderAllNum = this.ly_production_order_inspectionAll_InstoreDataGridView.CurrentRow.Cells["总检单号"].Value.ToString();

                this.ly_store_in_WXTableAdapter.Fill(this.lYQualityInspector.ly_store_in_WX, nowproductionorderAllNum);

                //set_processOrder_Num();


            }
            else
            {
                this.ly_store_in_WXTableAdapter.Fill(this.lYQualityInspector.ly_store_in_WX, "");
            }
        }

        private void bindingNavigatorAddNewItem_Click(object sender, EventArgs e)
        {
            //if (CheckFinished()) return;

            if (null == this.ly_production_order_inspectionAll_InstoreDataGridView.CurrentRow) return;



            string nowproductionorderAllNum = this.ly_production_order_inspectionAll_InstoreDataGridView.CurrentRow.Cells["总检单号"].Value.ToString();

            // if ("下料" == nowordername) return;  ly_machinepart_process_workDataGridView

            string message = "增加外协检验入库记录吗？";
            string caption = "提示...";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;

            DialogResult result;



            result = MessageBox.Show(message, caption, buttons,
            MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

            if (result == DialogResult.Yes)
            {


                decimal inspect_count;
                decimal instore_count;

                string  purchase_unit;
                decimal unit_ratio;



                if (!string.IsNullOrEmpty(this.ly_production_order_inspectionAll_InstoreDataGridView.CurrentRow.Cells["通过数量"].Value.ToString()))
                {
                    inspect_count = decimal.Parse(this.ly_production_order_inspectionAll_InstoreDataGridView.CurrentRow.Cells["通过数量"].Value.ToString());
                }
                else
                {
                    inspect_count = 0;
                }

                 if (!string.IsNullOrEmpty(this.ly_production_order_inspectionAll_InstoreDataGridView.CurrentRow.Cells["转换比例0"].Value.ToString()))
                {
                    unit_ratio = decimal.Parse(this.ly_production_order_inspectionAll_InstoreDataGridView.CurrentRow.Cells["转换比例0"].Value.ToString());
                }
                else
                {
                    unit_ratio = 0;
                }

                  if (!string.IsNullOrEmpty(this.ly_production_order_inspectionAll_InstoreDataGridView.CurrentRow.Cells["采购单位0"].Value.ToString()))
                {
                    purchase_unit = this.ly_production_order_inspectionAll_InstoreDataGridView.CurrentRow.Cells["采购单位0"].Value.ToString();
                }
                else
                {
                    purchase_unit = "";
                }



                //if (!string.IsNullOrEmpty(this.ly_production_order_inspectionAll_InstoreDataGridView.CurrentRow.Cells["入库数量0"].Value.ToString()))
                //{
                //    instore_count = decimal.Parse(this.ly_production_order_inspectionAll_InstoreDataGridView.CurrentRow.Cells["入库数量0"].Value.ToString());
                //}
                //else
                //{
                //    instore_count = 0;
                //}


                instore_count = 0;

                if (null != this.ly_store_in_JGDataGridView.CurrentRow)
                {

                    foreach (DataGridViewRow dgr in ly_store_in_JGDataGridView.Rows)
                    {

                        if (string.IsNullOrEmpty(dgr.Cells["采购数量"].Value.ToString())) continue;
                        instore_count = instore_count + decimal.Parse(dgr.Cells["采购数量"].Value.ToString());



                    }
                }





                if (instore_count > inspect_count)
                {

                    MessageBox.Show("该总检单检验通过工件已全部入库,不能增加总检入库记录", "注意");

                    return;
                }

               
                else
                {

                    this.ly_store_in_WXBindingSource.AddNew();

                    this.ly_store_in_JGDataGridView.CurrentRow.Cells["入库单号"].Value = GetMaxStoreInnum();
                    
                    this.ly_store_in_JGDataGridView.CurrentRow.Cells["物料编号"].Value = this.ly_production_order_inspectionAll_InstoreDataGridView.CurrentRow.Cells["编码"].Value;
                    this.ly_store_in_JGDataGridView.CurrentRow.Cells["总检单号1"].Value = this.ly_production_order_inspectionAll_InstoreDataGridView.CurrentRow.Cells["总检单号"].Value;

                    this.ly_store_in_JGDataGridView.CurrentRow.Cells["采购数量"].Value = inspect_count - instore_count;
                    //this.ly_store_in_JGDataGridView.CurrentRow.Cells["入库数量"].Value = (inspect_count - instore_count) * unit_ratio;
                    this.ly_store_in_JGDataGridView.CurrentRow.Cells["入库数量"].Value = (inspect_count - instore_count) * 1;
                    this.ly_store_in_JGDataGridView.CurrentRow.Cells["转换比例"].Value =  unit_ratio;
                    this.ly_store_in_JGDataGridView.CurrentRow.Cells["采购单位"].Value = purchase_unit;

                    this.ly_store_in_JGDataGridView.CurrentRow.Cells["送交人"].Value = this.ly_production_order_inspectionAll_InstoreDataGridView.CurrentRow.Cells["质检员"].Value;
                    
                    this.ly_store_in_JGDataGridView.CurrentRow.Cells["入库日期"].Value = SQLDatabase.GetNowdate().ToString(); ;

                    this.ly_store_in_JGDataGridView.CurrentRow.Cells["录入人"].Value = SQLDatabase.nowUserName();


                    this.ly_store_in_JGDataGridView.CurrentRow.Cells["入库类别"].Value = "外协";

                   


                }






                SaveChanged();




            }
        }

        private void SaveChanged()
        {

            this.ly_store_in_JGDataGridView.EndEdit();

            this.Validate();
            this.ly_store_in_WXBindingSource.EndEdit();

            this.ly_store_in_WXTableAdapter.Update(this.lYQualityInspector.ly_store_in_WX );



            string nowproductionorderAllNum = "";

            if (null != this.ly_production_order_inspectionAll_InstoreDataGridView.CurrentRow)
            {
                nowproductionorderAllNum = this.ly_production_order_inspectionAll_InstoreDataGridView.CurrentRow.Cells["总检单号"].Value.ToString();
            }

            ////////////////////////////////
            int instore_Id;

            if (null != this.ly_store_in_JGDataGridView.CurrentRow)
            {
                instore_Id = int.Parse(this.ly_store_in_JGDataGridView.CurrentRow.Cells["instoreId"].Value.ToString());
            }
            else
            {
                instore_Id = 0;
            }


            int inspect_Id;
            if (null != this.ly_production_order_inspectionAll_InstoreDataGridView.CurrentRow)
            {
                inspect_Id = int.Parse(this.ly_production_order_inspectionAll_InstoreDataGridView.CurrentRow.Cells["inspectId"].Value.ToString());
            }
            else
            {
                inspect_Id = 0;
            }

            ////////////////////////////////////


           

           // this.ly_store_in_JGTableAdapter.Fill(this.lYQualityInspector.ly_store_in_JG, nowproductionorderAllNum);

            this.ly_outsource_order_inspection_InstoreTableAdapter.Fill(this.lYOutsourceData.ly_outsource_order_inspection_Instore, DateTime.Parse(this.dateTimePicker1.Text).Date, DateTime.Parse(this.dateTimePicker2.Text).Date, SQLDatabase.NowUserID);

            this.ly_outsource_order_inspection_InstoreBindingSource.Position = this.ly_outsource_order_inspection_InstoreBindingSource.Find("id", inspect_Id);


            this.ly_store_in_WXBindingSource.Position = this.ly_store_in_WXBindingSource.Find("id", instore_Id);




        }

        private string GetMaxStoreInnum()
        {

            SqlConnection sqlConnection1 = new SqlConnection(SQLDatabase.Connectstring);
            SqlCommand cmd = new SqlCommand();

            string MaxPlanCode = "";

            //cmd.Parameters.Add("@Plan_mode", SqlDbType.VarChar);
            //cmd.Parameters["@Plan_mode"].Value = "LLJH";


            cmd.CommandText = "LY_Get_InNumber";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = sqlConnection1;

            sqlConnection1.Open();
            MaxPlanCode = cmd.ExecuteScalar().ToString();
            sqlConnection1.Close();



            return MaxPlanCode;

        }

        private void bindingNavigatorDeleteItem_Click(object sender, EventArgs e)
        {


            if (null == this.ly_store_in_JGDataGridView.CurrentRow) return;

           // if (CheckFinished()) return;



            if ("True" == this.ly_store_in_JGDataGridView.CurrentRow.Cells["签证"].Value.ToString())
            {
                MessageBox.Show("已经签证，不能删除(实需删除，请先删除签证标记)", "注意");
                return;

            }

            string nowoperptar = this.ly_store_in_JGDataGridView.CurrentRow.Cells["录入人"].Value.ToString();

            if (nowoperptar != SQLDatabase.nowUserName())
            {
                MessageBox.Show("请录入人:" + nowoperptar + "删除", "注意");
                return;
            }



            string message1 = "当前总检记录将被删除，继续吗？";
            string caption1 = "提示...";
            MessageBoxButtons buttons1 = MessageBoxButtons.YesNo;

            DialogResult result1;



            result1 = MessageBox.Show(message1, caption1, buttons1,
            MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

            if (result1 == DialogResult.Yes)
            {

                this.ly_store_in_WXBindingSource.RemoveCurrent();



                SaveChanged();




            }
        }

        private void ly_store_in_JGDataGridView_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            //if (CheckFinished()) return;

            if ("True" == this.ly_store_in_JGDataGridView.CurrentRow.Cells["签证"].Value.ToString())
            {
                MessageBox.Show("已经签证，不能删除(实需删除，请先删除签证标记)", "注意");
                return;

            }

            string nowoperptar = this.ly_store_in_JGDataGridView.CurrentRow.Cells["录入人"].Value.ToString();

            if (nowoperptar != SQLDatabase.nowUserName())
            {
                MessageBox.Show("请录入人:" + nowoperptar + "修改", "注意");
                return;
            }

            DataGridView dgv = sender as DataGridView;


          

            if ("采购数量" == dgv.CurrentCell.OwningColumn.Name)
            {

                ChangeValue queryForm = new ChangeValue();

                queryForm.OldValue = dgv.CurrentCell.Value.ToString();
                queryForm.NewValue = "";
                queryForm.ChangeMode = "value";
                queryForm.ShowDialog();




                if (queryForm.NewValue != "")
                {
                    dgv.CurrentRow.Cells["采购数量"].Value = queryForm.NewValue;

                    /////////////////////////////////////////////////////////////////////////////////////////////
                    decimal inspect_count;
                    decimal instore_count;

                    string purchase_unit;
                    decimal unit_ratio;

                



                    instore_count = 0;

                    if (null != this.ly_store_in_JGDataGridView.CurrentRow)
                    {

                        foreach (DataGridViewRow dgr in ly_store_in_JGDataGridView.Rows)
                        {

                            if (string.IsNullOrEmpty(dgr.Cells["采购数量"].Value.ToString())) continue;
                            instore_count = instore_count + decimal.Parse(dgr.Cells["采购数量"].Value.ToString());



                        }
                    }

                    



                    if (!string.IsNullOrEmpty(this.ly_production_order_inspectionAll_InstoreDataGridView.CurrentRow.Cells["通过数量"].Value.ToString()))
                    {
                        inspect_count = decimal.Parse(this.ly_production_order_inspectionAll_InstoreDataGridView.CurrentRow.Cells["通过数量"].Value.ToString());
                    }
                    else
                    {
                        inspect_count = 0;
                    }

                    if (!string.IsNullOrEmpty(this.ly_production_order_inspectionAll_InstoreDataGridView.CurrentRow.Cells["转换比例0"].Value.ToString()))
                    {
                        unit_ratio = decimal.Parse(this.ly_production_order_inspectionAll_InstoreDataGridView.CurrentRow.Cells["转换比例0"].Value.ToString());
                    }
                    else
                    {
                        unit_ratio = 0;
                    }

                    if (!string.IsNullOrEmpty(this.ly_production_order_inspectionAll_InstoreDataGridView.CurrentRow.Cells["采购单位0"].Value.ToString()))
                    {
                        purchase_unit = this.ly_production_order_inspectionAll_InstoreDataGridView.CurrentRow.Cells["采购单位0"].Value.ToString();
                    }
                    else
                    {
                        purchase_unit = "";
                    }


                    if (instore_count > inspect_count)
                    {

                        MessageBox.Show("入库数不能大于机加总检单提交的数量,请检查输入...", "注意");

                        if (string.IsNullOrEmpty(queryForm.OldValue))
                        {
                            dgv.CurrentRow.Cells["采购数量"].Value = DBNull.Value;
                        }
                        else
                        {
                            dgv.CurrentRow.Cells["采购数量"].Value = queryForm.OldValue;
                        }

                        return;
                    }

                    else
                    {
                        //dgv.CurrentRow.Cells["送检"].Value = queryForm.OldValue;

                        if (string.IsNullOrEmpty(queryForm.NewValue))
                        {
                            dgv.CurrentRow.Cells["入库数量"].Value = DBNull.Value;
                        }
                        else
                        {
                            dgv.CurrentRow.Cells["入库数量"].Value = decimal.Parse(queryForm.NewValue) * unit_ratio;
                        }
                       

                      
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


         

            ///////////////////////////////////////////////////////////////////////////////


            if ("入库日期" == dgv.CurrentCell.OwningColumn.Name)
            {

                ChangeValue queryForm = new ChangeValue();

                queryForm.OldValue = dgv.CurrentCell.Value.ToString();
                queryForm.NewValue = "";
                queryForm.ChangeMode = "datetime";
                queryForm.ShowDialog();




                if (queryForm.NewValue != "")
                {
                    dgv.CurrentRow.Cells["入库日期"].Value = queryForm.NewValue;
                    //dgv.CurrentRow.Cells["discount_money"].Value = DBNull.Value;

                    //dgv.CurrentRow.Cells["approve_flag"].Value = "False";
                    SaveChanged();


                    //CountPlanStru();

                }
                else
                {


                }
                return;


                /////////////////////////////////////

                //DatePicker queryForm = new DatePicker();
                //queryForm.Pt = pt;

                //if (null != (dgv.CurrentCell.Value))
                //    queryForm.NowDate = dgv.CurrentCell.Value.ToString();

                //queryForm.ShowDialog();



                //if (null != queryForm.NowDate)
                //{

                //    dgv.CurrentRow.Cells["检验日期"].Value = queryForm.NowDate;
                //    SaveChanged();

                //}
                //return;
            }









            ///////////////////////////////////////////////////////

            if ("备注" == dgv.CurrentCell.OwningColumn.Name)
            {

                ChangeValue queryForm = new ChangeValue();

                queryForm.OldValue = dgv.CurrentCell.Value.ToString();
                queryForm.NewValue = "";
                queryForm.ChangeMode = "longstring";
                queryForm.ShowDialog();




                if (queryForm.NewValue != "")
                {
                    dgv.CurrentRow.Cells["备注"].Value = queryForm.NewValue;
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

         //////////////////////////////////

            ///////////////////////////////////////////////////////

            if ("送交人" == dgv.CurrentCell.OwningColumn.Name)
            {




                
                string sel;

                

                    sel = "SELECT   yhmc as 姓名 FROM T_users where bumen='000401'";
               
              

                QueryForm queryForm = new QueryForm();


                queryForm.Sel = sel;
                queryForm.Constr = SQLDatabase.Connectstring;

                //Set the Column Collection to the filter Table
                //queryForm.SetSourceColumns(this.billMainDataSet.BalanceBill.Columns);

                queryForm.ShowDialog();


                if (queryForm.Result != "")
                {
                    dgv.CurrentRow.Cells["送交人"].Value = queryForm.Result;
                    //dgv.CurrentRow.Cells["discount_money"].Value = DBNull.Value;

                    //dgv.CurrentRow.Cells["approve_flag"].Value = "False";
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

          
        }

        private void 打印PToolStripButton_Click(object sender, EventArgs e)
        {
            if (null == this.ly_store_in_JGDataGridView.CurrentRow) return;

            string nowinput_num;
            nowinput_num = this.ly_store_in_JGDataGridView.CurrentRow.Cells["入库单号"].Value.ToString();

           //---- NewFrm.Show(this); ;

            //this.ly_plan_getmaterialTableAdapter.Fill(this.lYPlanMange.ly_plan_getmaterial, parentId);

            BaseReportView queryForm = new BaseReportView();

            queryForm.Text = "中原精密外协入库单";

            queryForm.Printdata = this.lYQualityInspector ;

            queryForm.PrintCrystalReport = new LY_WaixieRukudan();


            string selectFormula;

            selectFormula = "{ly_store_in_WX.入库单号}  =   '" + nowinput_num + "'  ";
            queryForm.PrintCrystalReport.DataDefinition.RecordSelectionFormula = selectFormula;

            //---- NewFrm.Hide(this);

            queryForm.ShowDialog();
        }

       
    }
}
