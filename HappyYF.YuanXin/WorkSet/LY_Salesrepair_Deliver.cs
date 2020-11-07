using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Transactions;
using System.Windows.Forms;
using HappyYF.Infrastructure.Repositories;
using DataGridFilter;


 namespace HappyYF.YuanXin.WorkSet
{
    public partial class LY_Salesrepair_Deliver : Form
    {

        private int selectionIdx = 0;
        //private string formState;
        //private int nowRow;
       
        private string nowusercode = "";
       
       
        private string nowinnerCode = "";
        private string nowcontractCode = "";



        public LY_Salesrepair_Deliver()
        {
            InitializeComponent();
        }

      

       
        private void Yonghu_Load(object sender, EventArgs e)
        {

            this.dateTimePicker1.Text = DateTime.Today.AddMonths(-3).Date.ToString();
            this.dateTimePicker2.Text = DateTime.Today.AddDays(1).Date.ToString();

            this.ly_sales_receive_itemInstoreTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            this.ly_sales_receiveTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;


            this.ly_sales_deliverTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            this.ly_sales_deliver_detailTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;


            this.ly_sales_receiveTableAdapter.Fill(this.lYSalseMange2.ly_sales_receive, "", "full", this.dateTimePicker1.Value, this.dateTimePicker2.Value);

           
            this.ly_sales_receive_itemInstoreBindingSource.Filter = "去向  = '维修'";

         
          
       

            this.nowusercode = SQLDatabase.NowUserID;
            
          

            

            //this.dateTimePicker1.Text = DateTime.Today.AddMonths(-6).Date.ToString();
            //this.dateTimePicker2.Text = DateTime.Today.AddDays(1).Date.ToString();


            //this.ly_material_plan_mainperiodBindingSource.Filter = "出库指令=1";
            //this.ly_material_plan_mainperiodTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            //this.ly_material_plan_mainperiodTableAdapter.Fill(this.lYSalseMange.ly_material_plan_mainperiod, "LSPT", this.dateTimePicker1.Value, this.dateTimePicker2.Value);


            

        }

     
      


        private string GetMaxDeliverNum()
        {
            SqlConnection sqlConnection1 = new SqlConnection(SQLDatabase.Connectstring);
            SqlCommand cmd = new SqlCommand();

            string MaxProductionorder = "";

            cmd.Parameters.Add("@deliver_mode", SqlDbType.VarChar);
            cmd.Parameters["@deliver_mode"].Value = "YY";


            cmd.CommandText = "LY_GetMax_Deliver_Num";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = sqlConnection1;

            sqlConnection1.Open();
            MaxProductionorder = cmd.ExecuteScalar().ToString();
            sqlConnection1.Close();



            return MaxProductionorder;
        }

        private void 增加配调检验记录ToolStripMenuItem_Click(object sender, EventArgs e)
        {
          //if (null == ly_sales_test_detail2DataGridView.CurrentRow) return;

          //  string message = "增加营业发货记录吗？";
          //  string caption = "提示...";
          //  MessageBoxButtons buttons = MessageBoxButtons.YesNo;

          //  DialogResult result;



          //  result = MessageBox.Show(message, caption, buttons,
          //  MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

          //  if (result == DialogResult.Yes)
          //  {
          //      this.ly_sales_deliverBindingSource.AddNew();

          //      this.ly_sales_deliverDataGridView.CurrentRow.Cells["发货单号5"].Value = GetMaxDeliverNum();

          //      this.ly_sales_deliverDataGridView.CurrentRow.Cells["发货日期5"].Value = SQLDatabase.GetNowdate().ToString(); ;

          //      this.ly_sales_deliverDataGridView.CurrentRow.Cells["经办人5"].Value = SQLDatabase.nowUserName();

          //      this.ly_sales_deliverDataGridView.CurrentRow.Cells["收件人5"].Value = this.ly_sales_groupDataGridView.CurrentRow.Cells["收件人"].Value;
          //      this.ly_sales_deliverDataGridView.CurrentRow.Cells["邮编5"].Value = this.ly_sales_groupDataGridView.CurrentRow.Cells["邮编"].Value;
          //      this.ly_sales_deliverDataGridView.CurrentRow.Cells["收件地址5"].Value = this.ly_sales_groupDataGridView.CurrentRow.Cells["地址"].Value;
          //      this.ly_sales_deliverDataGridView.CurrentRow.Cells["客户电话5"].Value = this.ly_sales_groupDataGridView.CurrentRow.Cells["电话"].Value;




          //      int parentId = int.Parse(this.ly_sales_groupDataGridView.CurrentRow.Cells["group_id"].Value.ToString());
          //      this.ly_sales_deliverDataGridView.CurrentRow.Cells["group_id5"].Value = parentId;

          //      this.ly_sales_deliverBindingSource.EndEdit();
          //      this.ly_sales_deliverTableAdapter.Update(this.lYSalseMange.ly_sales_deliver);

             
               
              

          //  }
        }

        private void 增加检验记录明细ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // if (null == ly_sales_testDataGridView.CurrentRow) return;

            //string message = "增加检验记录明细吗？";
            //string caption = "提示...";
            //MessageBoxButtons buttons = MessageBoxButtons.YesNo;

            //DialogResult result;



            //result = MessageBox.Show(message, caption, buttons,
            //MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

            //if (result == DialogResult.Yes)
            //{
            //    this.ly_sales_test_detailBindingSource.AddNew();

            //    //this.ly_sales_test_detailDataGridView.CurrentRow.Cells["配调记录"].Value = GetMaxRecordNum();

            //    //this.ly_sales_testDataGridView.CurrentRow.Cells["配调日期0"].Value = SQLDatabase.GetNowdate().ToString(); ;

            //    //this.ly_sales_testDataGridView.CurrentRow.Cells["配调人"].Value = SQLDatabase.nowUserName();

            //    int parentId = int.Parse(this.ly_sales_testDataGridView.CurrentRow.Cells["record_id"].Value.ToString());
            //    this.ly_sales_test_detailDataGridView.CurrentRow.Cells["record_id1"].Value = parentId;

            //    this.ly_sales_test_detailBindingSource.EndEdit();
            //    this.ly_sales_test_detailTableAdapter.Update(this.lYSalseMange.ly_sales_test_detail);
            //}
        }

       

        private void 删除记录明细ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //if (null == this.ly_sales_deliver_detailDataGridView.CurrentRow) return;

            //if ("True" == ly_material_plan_mainDataGridView.CurrentRow.Cells["营业发货0"].Value.ToString())
            //{
            //    MessageBox.Show("已经发货,不能删除数据...", "注意");
            //    return;

            //}


            //string message1 = "当前(记录明细)将被删除，继续吗？";
            //string caption1 = "提示...";
            //MessageBoxButtons buttons1 = MessageBoxButtons.YesNo;

            //DialogResult result1;



            //result1 = MessageBox.Show(message1, caption1, buttons1,
            //MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

            //if (result1 == DialogResult.Yes)
            //{

            //    this.ly_sales_deliver_detailBindingSource.RemoveCurrent();

            //    this.ly_sales_deliver_detailBindingSource.EndEdit();
            //    this.ly_sales_deliver_detailTableAdapter.Update(this.lYSalseMange.ly_sales_deliver_detail );

            //    int parentId = int.Parse(this.ly_sales_testDataGridView.CurrentRow.Cells["record_id"].Value.ToString());

            //    this.ly_sales_test_detail2TableAdapter.Fill(this.lYSalseMange.ly_sales_test_detail2, parentId);

            //    string nowplannum = ly_material_plan_mainDataGridView.CurrentRow.Cells["计划编号0"].Value.ToString();

            //    this.f_PlanExtend_LSPTTableAdapter.Fill(this.lYSalseMange.f_PlanExtend_LSPT, nowplannum);

             

            //    //int nowgroupid = int.Parse(ly_sales_groupDataGridView.CurrentRow.Cells["group_id"].Value.ToString());

            //    //string nowplannum = ly_material_plan_mainDataGridView.CurrentRow.Cells["计划编号0"].Value.ToString();
            //    //string nowcontractnum = ly_material_plan_mainDataGridView.CurrentRow.Cells["计划编号0"].Value.ToString();
            //    //string nowgroupnum = ly_sales_groupDataGridView.CurrentRow.Cells["配套编码"].Value.ToString();

            //    //this.ly_plan_getmaterial1TableAdapter.Fill(this.lYSalseMange.ly_plan_getmaterial1, nowplannum, nowgroupnum, 0 - nowgroupid);


            //    ////////////////////////////

            //    int nowgroupid = 0;
            //    string nowgroupnum = "";

               
            //    string nowcontractnum = ly_material_plan_mainDataGridView.CurrentRow.Cells["计划编号0"].Value.ToString();

            //    if (null != ly_sales_groupDataGridView.CurrentRow)
            //    {
            //        nowgroupnum = ly_sales_groupDataGridView.CurrentRow.Cells["配套编码"].Value.ToString();

            //        nowgroupid = int.Parse(ly_sales_groupDataGridView.CurrentRow.Cells["group_id"].Value.ToString());

            //        //this.ly_plan_getmaterial1TableAdapter.Fill(this.lYSalseMange.ly_plan_getmaterial1, nowplannum, nowgroupnum, 0 - nowgroupid);
            //    }



            //    this.ly_plan_getmaterial_deliverTableAdapter.Fill(this.lYSalseMange.ly_plan_getmaterial_deliver, nowplannum, nowgroupnum, nowgroupid);





            //}
        }

        private void 删除ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //if (null == this.ly_sales_deliverDataGridView.CurrentRow) return;

            //if ("True" == ly_material_plan_mainDataGridView.CurrentRow.Cells["营业发货0"].Value.ToString())
            //{
            //    MessageBox.Show("已经发货,不能删除数据...", "注意");
            //    return;

            //}

            ////string diaodu = this.ly_sales_deliverDataGridView.CurrentRow.Cells["经办人5"].Value.ToString();

            ////if (diaodu != SQLDatabase.nowUserName())
            ////{
            ////    MessageBox.Show("请经办人:" + diaodu + "删除", "注意");
            ////    return;
            ////}

            ////if (ly_production_order_detailDataGridView.RowCount > 0)
            ////{
            ////    MessageBox.Show("跟单已有工序安排,删除所有工序安排后才能删除跟单...", "注意");
            ////    return;

            ////}

            ////string nowproductionorder = this.ly_production_orderDataGridView.CurrentRow.Cells["跟单编号"].Value.ToString();


            //string message1 = "当前(发货记录)将被删除，继续吗？";
            //string caption1 = "提示...";
            //MessageBoxButtons buttons1 = MessageBoxButtons.YesNo;

            //DialogResult result1;



            //result1 = MessageBox.Show(message1, caption1, buttons1,
            //MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

            //if (result1 == DialogResult.Yes)
            //{


            //    this.ly_sales_deliverBindingSource.RemoveCurrent();

            //    this.ly_sales_deliverBindingSource.EndEdit();
            //    this.ly_sales_deliverTableAdapter.Update(this.lYSalseMange.ly_sales_deliver );

            //    int nowgroupid = int.Parse(ly_sales_groupDataGridView.CurrentRow.Cells["group_id"].Value.ToString());

            //    string nowplannum = ly_material_plan_mainDataGridView.CurrentRow.Cells["计划编号0"].Value.ToString();
            //    string nowcontractnum = ly_material_plan_mainDataGridView.CurrentRow.Cells["计划编号0"].Value.ToString();
            //    string nowgroupnum = ly_sales_groupDataGridView.CurrentRow.Cells["配套编码"].Value.ToString();

            //    //this.ly_plan_getmaterial1TableAdapter.Fill(this.lYSalseMange.ly_plan_getmaterial1, nowplannum, nowgroupnum, 0 - nowgroupid);

            //}
        }

     

     
        private void ly_sales_deliverDataGridView_SelectionChanged(object sender, EventArgs e)
        {
            if (null == ly_sales_deliverDataGridView.CurrentRow)
            {
                this.ly_sales_deliver_detailTableAdapter.Fill(this.lYSalseMange.ly_sales_deliver_detail, -1);
                return;
            }

            int parentId = int.Parse(this.ly_sales_deliverDataGridView.CurrentRow.Cells["deliver_id"].Value.ToString());

            this.ly_sales_deliver_detailTableAdapter.Fill(this.lYSalseMange.ly_sales_deliver_detail, parentId);
        }

       

        private void ly_sales_deliver_detailDataGridView_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex >= 0)
                selectionIdx = e.RowIndex;
        }

        private void ly_sales_deliver_detailDataGridView_CellMouseMove(object sender, DataGridViewCellMouseEventArgs e)
        {
            DataGridView dgv = sender as DataGridView;

            if ((e.Clicks < 2) && (e.Button == MouseButtons.Left))
            {
                if ((e.ColumnIndex == -1) && (e.RowIndex > -1))
                    dgv.DoDragDrop(dgv.Rows[e.RowIndex], DragDropEffects.Move);
            } 
        }
        private int GetRowFromPoint(DataGridView dgv, int x, int y)
        {
            for (int i = 0; i < dgv.RowCount; i++)
            {
                Rectangle rec = dgv.GetRowDisplayRectangle(i, false);

                if (dgv.RectangleToScreen(rec).Contains(x, y))
                    return i;
            }

            return -1;
        }
        private void ly_sales_deliver_detailDataGridView_DragDrop(object sender, DragEventArgs e)
        {
            DataGridView dgv = sender as DataGridView;



            int idx = GetRowFromPoint(dgv, e.X, e.Y);
            if (idx < 0) return;
            //index2 = idx;
            if (e.Data.GetDataPresent(typeof(DataGridViewRow)))
            {

                DataGridViewRow row = (DataGridViewRow)e.Data.GetData(typeof(DataGridViewRow));

                int tempOrder = row.Index;
                // this.gqis.Ins_Incontrol(idx, row.Cells[0].Value.ToString());



                //dgv.Rows[idx].Cells["顺序"].Value = tempOrder;
                //dgv.Rows[idx].Cells["顺序"].Value = tempOrder;

                if (idx > row.Index)
                {
                    foreach (DataGridViewRow dgvr in dgv.Rows)
                    {

                        if (dgvr.Index > row.Index && dgvr.Index <= idx)
                        {
                            dgvr.Cells["序号6"].Value = dgvr.Index;

                        }
                    }
                }
                if (idx < row.Index)
                {
                    foreach (DataGridViewRow dgvr in dgv.Rows)
                    {

                        if (dgvr.Index >= idx && dgvr.Index < row.Index)
                        {
                            dgvr.Cells["序号6"].Value = dgvr.Index + 2;

                        }
                    }
                }


                row.Cells["序号6"].Value = idx + 1;
                // dgv.Rows[idx].Cells["顺序"].Value = row.Index + 1;

                SaveChanged();

               

                dgv.Rows[idx].Selected = true;
                dgv.CurrentCell = dgv.Rows[idx].Cells["序号6"];


                //selectionIdx = idx;
            } 
        }
        private void SaveChanged()
        {



            this.ly_sales_deliver_detailBindingSource.EndEdit();
            this.ly_sales_deliver_detailTableAdapter.Update(this.lYSalseMange.ly_sales_deliver_detail);


            int parentId = int.Parse(this.ly_sales_deliverDataGridView.CurrentRow.Cells["deliver_id"].Value.ToString());

            this.ly_sales_deliver_detailTableAdapter.Fill(this.lYSalseMange.ly_sales_deliver_detail, parentId);



            foreach (DataGridViewRow dgr in ly_sales_deliver_detailDataGridView.Rows)
            {
                dgr.Cells["序号6"].Value = dgr.Index + 1;

            }

            this.ly_sales_deliver_detailBindingSource.EndEdit();

            this.ly_sales_deliver_detailTableAdapter.Update(this.lYSalseMange.ly_sales_deliver_detail);
            this.ly_sales_deliver_detailTableAdapter.Fill(this.lYSalseMange.ly_sales_deliver_detail, parentId);
        }

        private void ly_sales_deliver_detailDataGridView_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move; 
        }

        private void ly_sales_deliverDataGridView_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            DataGridView dgv = sender as DataGridView;

            if (null == dgv.CurrentRow) return;


            /////////////////////////////////////////////////////////////////

            //if ("发货日期5" == dgv.CurrentCell.OwningColumn.Name)
            //{

            //    if ("True" == dgv.CurrentRow.Cells["已发5"].Value.ToString())
            //    {
            //        MessageBox.Show("已经发货,不能修改发货日期...", "注意");

            //        return;
            //    }

            //    ChangeValue queryForm = new ChangeValue();

            //    queryForm.OldValue = dgv.CurrentCell.Value.ToString();
            //    queryForm.NewValue = "";
            //    queryForm.ChangeMode = "datetime";
            //    queryForm.ShowDialog();


            //    if (queryForm.NewValue != "")
            //    {
            //        dgv.CurrentRow.Cells["合同文本交付"].Value = queryForm.NewValue;

            //    }
            //    else
            //    {

            //        dgv.CurrentRow.Cells["合同文本交付"].Value = DBNull.Value;

            //    }



            //    SaveContract();



            //    return;

            //}
            //////////////////////////////////////////////////////////////////////////





           

            ///////////////////////////////////////////////////////////
            if ("已发5" == dgv.CurrentCell.OwningColumn.Name)
            {





                if ("True" == dgv.CurrentRow.Cells["已发5"].Value.ToString())
                {
                    dgv.CurrentRow.Cells["已发5"].Value = "False";
                    dgv.CurrentRow.Cells["经办人5"].Value =DBNull.Value; 
                }
                else
                {

                    dgv.CurrentRow.Cells["已发5"].Value = "True";
                    dgv.CurrentRow.Cells["经办人5"].Value = SQLDatabase.nowUserName();
                }



                SaveContract();



                return;

            }
            ///////////////////////////////////////////////////////////////


            //if ("True" == dgv.CurrentRow.Cells["已发5"].Value.ToString())
            //{
            //    MessageBox.Show("已经发货,不能修改数据...", "注意");
            //    return;

            //}


          


            if ("发货日期5" == dgv.CurrentCell.OwningColumn.Name)
            {

                ChangeValue queryForm = new ChangeValue();

                queryForm.OldValue = dgv.CurrentCell.Value.ToString();
                queryForm.NewValue = "";
                queryForm.ChangeMode = "datetime";
                queryForm.ShowDialog();


                if (queryForm.NewValue != "")
                {
                    dgv.CurrentRow.Cells["发货日期5"].Value = queryForm.NewValue;
                    dgv.CurrentRow.Cells["经办人5"].Value = SQLDatabase.nowUserName();
                }
                else
                {

                    dgv.CurrentRow.Cells["发货日期5"].Value = DBNull.Value;
                    dgv.CurrentRow.Cells["经办人5"].Value = DBNull.Value;
                }



                SaveContract();



                return;

            }
            ////////////////////////////////////////////////////////////////////////

            if ("客户名称5" == dgv.CurrentCell.OwningColumn.Name)
            {

                ChangeValue queryForm = new ChangeValue();

                queryForm.OldValue = dgv.CurrentCell.Value.ToString();
                queryForm.NewValue = "";
                queryForm.ChangeMode = "string";
                queryForm.ShowDialog();




                if (queryForm.NewValue != "")
                {
                    dgv.CurrentRow.Cells["客户名称5"].Value = queryForm.NewValue;
                    //dgv.CurrentRow.Cells["discount_money"].Value = DBNull.Value;

                    //dgv.CurrentRow.Cells["approve_flag"].Value = "False";
                    SaveContract();

                    //CountPlanStru();

                }
                else
                {

                }
                return;

            }
            /////////////////////////////


            /////////////////////////////
            if ("收件人5" == dgv.CurrentCell.OwningColumn.Name)
            {

                ChangeValue queryForm = new ChangeValue();

                queryForm.OldValue = dgv.CurrentCell.Value.ToString();
                queryForm.NewValue = "";
                queryForm.ChangeMode = "string";
                queryForm.ShowDialog();




                if (queryForm.NewValue != "")
                {
                    dgv.CurrentRow.Cells["收件人5"].Value = queryForm.NewValue;
                    //dgv.CurrentRow.Cells["discount_money"].Value = DBNull.Value;

                    //dgv.CurrentRow.Cells["approve_flag"].Value = "False";
                    SaveContract();

                    //CountPlanStru();

                }
                else
                {

                }
                return;

            }
            /////////////////////////////
            if ("邮编5" == dgv.CurrentCell.OwningColumn.Name)
            {

                ChangeValue queryForm = new ChangeValue();

                queryForm.OldValue = dgv.CurrentCell.Value.ToString();
                queryForm.NewValue = "";
                queryForm.ChangeMode = "string";
                queryForm.ShowDialog();




                if (queryForm.NewValue != "")
                {
                    dgv.CurrentRow.Cells["邮编5"].Value = queryForm.NewValue;
                    //dgv.CurrentRow.Cells["discount_money"].Value = DBNull.Value;

                    //dgv.CurrentRow.Cells["approve_flag"].Value = "False";
                    SaveContract();

                    //CountPlanStru();

                }
                else
                {

                }
                return;

            }

            /////////////////////////////
            if ("客户电话5" == dgv.CurrentCell.OwningColumn.Name)
            {

                ChangeValue queryForm = new ChangeValue();

                queryForm.OldValue = dgv.CurrentCell.Value.ToString();
                queryForm.NewValue = "";
                queryForm.ChangeMode = "string";
                queryForm.ShowDialog();




                if (queryForm.NewValue != "")
                {
                    dgv.CurrentRow.Cells["客户电话5"].Value = queryForm.NewValue;
                    //dgv.CurrentRow.Cells["discount_money"].Value = DBNull.Value;

                    //dgv.CurrentRow.Cells["approve_flag"].Value = "False";
                    SaveContract();

                    //CountPlanStru();

                }
                else
                {

                }
                return;

            }
            /////////////////////////////
            if ("收件地址5" == dgv.CurrentCell.OwningColumn.Name)
            {

                ChangeValue queryForm = new ChangeValue();

                queryForm.OldValue = dgv.CurrentCell.Value.ToString();
                queryForm.NewValue = "";
                queryForm.ChangeMode = "longstring";
                queryForm.ShowDialog();




                if (queryForm.NewValue != "")
                {
                    dgv.CurrentRow.Cells["收件地址5"].Value = queryForm.NewValue;
                    //dgv.CurrentRow.Cells["discount_money"].Value = DBNull.Value;

                    //dgv.CurrentRow.Cells["approve_flag"].Value = "False";
                    SaveContract();

                    //CountPlanStru();

                }
                else
                {

                }
                return;

            }


            /////////////////////////////////////////////////////////
            if ("快递单号5" == dgv.CurrentCell.OwningColumn.Name)
            {

                ChangeValue queryForm = new ChangeValue();

                queryForm.OldValue = dgv.CurrentCell.Value.ToString();
                queryForm.NewValue = "";
                queryForm.ChangeMode = "string";
                queryForm.ShowDialog();




                if (queryForm.NewValue != "")
                {
                    dgv.CurrentRow.Cells["快递单号5"].Value = queryForm.NewValue;
                    //dgv.CurrentRow.Cells["discount_money"].Value = DBNull.Value;

                    //dgv.CurrentRow.Cells["approve_flag"].Value = "False";
                    SaveContract();

                    //CountPlanStru();

                }
                else
                {

                }
                return;

            }


            /////////////////////////////////////////////////////////

            //if ("类别" == dgv.CurrentCell.OwningColumn.Name)
            //{






            //    string sel;



            //    sel = "SELECT  class_code as 编码, class_name as 名称 FROM ly_sales_contract_class ";



            //    QueryForm queryForm = new QueryForm();


            //    queryForm.Sel = sel;
            //    queryForm.Constr = SQLDatabase.Connectstring;

            //    //Set the Column Collection to the filter Table
            //    //queryForm.SetSourceColumns(this.billMainDataSet.BalanceBill.Columns);

            //    queryForm.ShowDialog();


            //    if (queryForm.Result != "")
            //    {
            //        dgv.CurrentRow.Cells["类别码"].Value = queryForm.Result;
            //        //dgv.CurrentRow.Cells["discount_money"].Value = DBNull.Value;

            //        //dgv.CurrentRow.Cells["approve_flag"].Value = "False";
            //        SaveContract();


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


            ///////////////////////////////////////////////////////////

            //if ("属性" == dgv.CurrentCell.OwningColumn.Name)
            //{






            //    string sel;



            //    sel = "SELECT  style_code as 编码, style_name as 名称 FROM ly_sales_contract_style ";



            //    QueryForm queryForm = new QueryForm();


            //    queryForm.Sel = sel;
            //    queryForm.Constr = SQLDatabase.Connectstring;

            //    //Set the Column Collection to the filter Table
            //    //queryForm.SetSourceColumns(this.billMainDataSet.BalanceBill.Columns);

            //    queryForm.ShowDialog();


            //    if (queryForm.Result != "")
            //    {
            //        dgv.CurrentRow.Cells["属性码"].Value = queryForm.Result;
            //        //dgv.CurrentRow.Cells["discount_money"].Value = DBNull.Value;

            //        //dgv.CurrentRow.Cells["approve_flag"].Value = "False";
            //        SaveContract();


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
            if ("快递公司5" == dgv.CurrentCell.OwningColumn.Name)
            {






                string sel;



                sel = "SELECT  express_name as 快递公司, express_people as 快递员,express_phone as 快递电话 FROM ly_express_company ";



                QueryForm queryForm = new QueryForm();


                queryForm.Sel = sel;
                queryForm.Constr = SQLDatabase.Connectstring;

                //Set the Column Collection to the filter Table
                //queryForm.SetSourceColumns(this.billMainDataSet.BalanceBill.Columns);

                queryForm.ShowDialog();


                if (queryForm.Result != "")
                {
                    dgv.CurrentRow.Cells["快递公司5"].Value = queryForm.Result;
                    dgv.CurrentRow.Cells["快递员5"].Value = queryForm.Result1;
                    dgv.CurrentRow.Cells["快递电话5"].Value = queryForm.Result2;
                    //dgv.CurrentRow.Cells["discount_money"].Value = DBNull.Value;

                    //dgv.CurrentRow.Cells["approve_flag"].Value = "False";
                    SaveContract();


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


            /////////////////////////////////////////////////////////

            //if ("合同编码" == dgv.CurrentCell.OwningColumn.Name)
            //{


            //    ChangeValue queryForm = new ChangeValue();

            //    queryForm.OldValue = dgv.CurrentCell.Value.ToString();
            //    queryForm.NewValue = "";
            //    queryForm.ChangeMode = "string";
            //    queryForm.ShowDialog();




            //    if (queryForm.NewValue != "")
            //    {

            //        dgv.CurrentRow.Cells["合同编码"].Value = queryForm.NewValue;

            //        int main_Id = int.Parse(dgv.CurrentRow.Cells["id_main"].Value.ToString());



            //        //string insstr = " delete ly_material_plan_main  where material_plan_num = '" + nowPlanNumber + "'";

            //        string updstr = " update ly_sales_contract_main  " +
            //                "  set contract_code=  '" + queryForm.NewValue + "' where  id=" + main_Id.ToString();


            //        SqlConnection sqlConnection1 = new SqlConnection(SQLDatabase.Connectstring);
            //        SqlCommand cmd = new SqlCommand();

            //        cmd.CommandText = updstr;
            //        cmd.CommandType = CommandType.Text;
            //        cmd.Connection = sqlConnection1;

            //        int temp = 0;

            //        using (TransactionScope scope = new TransactionScope())
            //        {

            //            sqlConnection1.Open();
            //            try
            //            {

            //                cmd.ExecuteNonQuery();



            //                scope.Complete();



            //            }
            //            catch (SqlException sqle)
            //            {


            //                MessageBox.Show(sqle.Message.Split('*')[0]);
            //            }


            //            finally
            //            {
            //                sqlConnection1.Close();


            //            }
            //        }

            //    }



            //    return;

            //}
        }

        private void SaveContract()
        {
            this.ly_sales_deliverBindingSource.EndEdit();
            this.ly_sales_deliverTableAdapter.Update(this.lYSalseMange.ly_sales_deliver);

        }

        private void 快递公司设置ToolStripMenuItem_Click(object sender, EventArgs e)
        {

            //int index;
            //int upperBound;

         

            //Screen[] screens = Screen.AllScreens;
            //upperBound = screens.GetUpperBound(0);

            //for (index = 0; index <= upperBound; index++)
            //{



            //    MessageBox.Show("Device Name: " + screens[index].DeviceName + " Bounds: " + screens[index].Bounds.ToString() + " Type: " + screens[index].GetType().ToString() + " Working Area: " + screens[index].WorkingArea.ToString() + " Primary Screen: " + screens[index].Primary.ToString());
               
            //}


         
           
            LY_Express_Mange queryForm = new LY_Express_Mange();

            //queryForm.salesclient_code = "";
            //queryForm.runmode = "增加";


            queryForm.StartPosition = FormStartPosition.CenterParent;
            queryForm.ShowDialog();

        }

        private void ly_sales_deliver_detailDataGridView_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            DataGridView dgv = sender as DataGridView;

            if (null == dgv.CurrentRow) return;




          

          
            /////////////////////////////
            if ("备注6" == dgv.CurrentCell.OwningColumn.Name)
            {

                ChangeValue queryForm = new ChangeValue();

                queryForm.OldValue = dgv.CurrentCell.Value.ToString();
                queryForm.NewValue = "";
                queryForm.ChangeMode = "longstring";
                queryForm.ShowDialog();




                if (queryForm.NewValue != "")
                {
                    dgv.CurrentRow.Cells["备注6"].Value = queryForm.NewValue;
                    //dgv.CurrentRow.Cells["discount_money"].Value = DBNull.Value;

                    //dgv.CurrentRow.Cells["approve_flag"].Value = "False";
                    this.ly_sales_deliver_detailBindingSource.EndEdit();
                    this.ly_sales_deliver_detailTableAdapter.Update(this.lYSalseMange.ly_sales_deliver_detail);


                    //CountPlanStru();

                }
                else
                {

                }
                return;

            }

            /////////////////////////////////////
            /////////////////////////////
            if ("实发数量" == dgv.CurrentCell.OwningColumn.Name)
            {

                ChangeValue queryForm = new ChangeValue();

                queryForm.OldValue = dgv.CurrentCell.Value.ToString();
                queryForm.NewValue = "";
                queryForm.ChangeMode = "value";
                queryForm.ShowDialog();




                if (queryForm.NewValue != "")
                {
                    dgv.CurrentRow.Cells["实发数量"].Value = queryForm.NewValue;
                    //dgv.CurrentRow.Cells["discount_money"].Value = DBNull.Value;

                    //dgv.CurrentRow.Cells["approve_flag"].Value = "False";
                    this.ly_sales_deliver_detailBindingSource.EndEdit();
                    this.ly_sales_deliver_detailTableAdapter.Update(this.lYSalseMange.ly_sales_deliver_detail);


                    //CountPlanStru();

                }
                else
                {

                }
                return;

            }

            /////////////////////////////////////


        

        }

        private void 装箱单打印ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (null == this.ly_sales_deliver_detailDataGridView.CurrentRow) return;

          

            NewFrm.Show(this); ;

            //this.ly_plan_getmaterialTableAdapter.Fill(this.lYPlanMange.ly_plan_getmaterial, parentId);

            BaseReportView queryForm = new BaseReportView();

            queryForm.Text = "中原精密营业发货装箱单";

            queryForm.Printdata = this.lYSalseMange;




            queryForm.PrintCrystalReport = new LY_YingyeHetong_FHZX();
           


           
            NewFrm.Hide(this);

            queryForm.ShowDialog();
        }

      

        private void 生成装箱总单ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //if (null == f_PlanExtend_LSPTDataGridView.CurrentRow) return;

            //string message = "生成装箱单吗？";
            //string caption = "提示...";
            //MessageBoxButtons buttons = MessageBoxButtons.YesNo;

            //DialogResult result;



            //result = MessageBox.Show(message, caption, buttons,
            //MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

            //if (result == DialogResult.Yes)
            //{
            //    this.ly_sales_deliverBindingSource.AddNew();

            //    string nowdelivernum = GetMaxDeliverNum();
            //    this.ly_sales_deliverDataGridView.CurrentRow.Cells["发货单号5"].Value = nowdelivernum;

            //    this.ly_sales_deliverDataGridView.CurrentRow.Cells["发货日期5"].Value = SQLDatabase.GetNowdate().ToString(); ;

            //    this.ly_sales_deliverDataGridView.CurrentRow.Cells["经办人5"].Value = SQLDatabase.nowUserName();

            //    this.ly_sales_deliverDataGridView.CurrentRow.Cells["收件人5"].Value = this.ly_sales_groupDataGridView.CurrentRow.Cells["收件人"].Value;
            //    this.ly_sales_deliverDataGridView.CurrentRow.Cells["邮编5"].Value = this.ly_sales_groupDataGridView.CurrentRow.Cells["邮编"].Value;
            //    this.ly_sales_deliverDataGridView.CurrentRow.Cells["收件地址5"].Value = this.ly_sales_groupDataGridView.CurrentRow.Cells["地址"].Value;
            //    this.ly_sales_deliverDataGridView.CurrentRow.Cells["客户电话5"].Value = this.ly_sales_groupDataGridView.CurrentRow.Cells["电话"].Value;

            //    this.ly_sales_deliverDataGridView.CurrentRow.Cells["客户名称5"].Value = this.ly_material_plan_mainDataGridView.CurrentRow.Cells["客户名称0"].Value;


            //    int parentId = int.Parse(this.ly_sales_groupDataGridView.CurrentRow.Cells["group_id"].Value.ToString());
            //    this.ly_sales_deliverDataGridView.CurrentRow.Cells["group_id5"].Value = parentId;

            //    this.ly_sales_deliverBindingSource.EndEdit();
            //    this.ly_sales_deliverTableAdapter.Update(this.lYSalseMange.ly_sales_deliver);

            //    //AddDetail(nowdelivernum);

            //    if (null == ly_sales_deliverDataGridView.CurrentRow) return;

            //    //if ("True" == this.ly_sales_test_detail2DataGridView.CurrentRow.Cells["已排"].Value.ToString())
            //    //{

            //    //    return;
            //    //}


            //    //string machineNum = this.ly_sales_test_detail2DataGridView.CurrentRow.Cells["出厂编码"].Value.ToString();

            //    //if ("" == machineNum)
            //    //{
            //    //    MessageBox.Show("产品无出厂编码,不能出库...", "注意");
            //    //    return;
            //    //}

            //    int deliverId = int.Parse(this.ly_sales_deliverDataGridView.CurrentRow.Cells["deliver_id"].Value.ToString());

            //    foreach (DataGridViewRow dgr in f_PlanExtend_LSPTDataGridView.Rows)
            //    {
            //        this.ly_sales_deliver_detailBindingSource.AddNew();

            //        this.ly_sales_deliver_detailDataGridView.CurrentRow.Cells["deliver_id6"].Value = this.ly_sales_deliverDataGridView.CurrentRow.Cells["deliver_id"].Value;

            //        this.ly_sales_deliver_detailDataGridView.CurrentRow.Cells["test_detail_id6"].Value = dgr.Cells["real_id"].Value;

            //        this.ly_sales_deliver_detailDataGridView.CurrentRow.Cells["序号6"].Value = this.ly_sales_deliver_detailDataGridView.RowCount;

            //        this.ly_sales_deliver_detailDataGridView.CurrentRow.Cells["物料编码6"].Value = dgr.Cells["产品编码3"].Value;

            //        this.ly_sales_deliver_detailDataGridView.CurrentRow.Cells["配套编码6"].Value = dgr.Cells["配套编码3"].Value;
            //        this.ly_sales_deliver_detailDataGridView.CurrentRow.Cells["配套名称6"].Value = dgr.Cells["配套名称3"].Value;

            //        //配套编码3this.ly_sales_deliver_detailDataGridView.CurrentRow.Cells["出厂编码6"].Value = this.ly_sales_test_detail2DataGridView.CurrentRow.Cells["出厂编码"].Value;
            //        this.ly_sales_deliver_detailDataGridView.CurrentRow.Cells["发货数量6"].Value = dgr.Cells["数量3"].Value;
            //        this.ly_sales_deliver_detailDataGridView.CurrentRow.Cells["实发数量"].Value = dgr.Cells["数量3"].Value; 
            //        //this.ly_sales_deliver_detailDataGridView.CurrentRow.Cells["备注6"].Value = this.ly_sales_groupDataGridView.CurrentRow.Cells["电话"].Value;

            //    }


            //    //int parentId = int.Parse(this.ly_sales_groupDataGridView.CurrentRow.Cells["group_id"].Value.ToString());
            //    //this.ly_sales_deliverDataGridView.CurrentRow.Cells["group_id5"].Value = parentId;

            //    this.ly_sales_deliver_detailBindingSource.EndEdit();
            //    this.ly_sales_deliver_detailTableAdapter.Update(this.lYSalseMange.ly_sales_deliver_detail);



            //    this.ly_sales_deliver_detailTableAdapter.Fill(this.lYSalseMange.ly_sales_deliver_detail, deliverId);

            //    //int parentId = int.Parse(this.ly_sales_testDataGridView.CurrentRow.Cells["record_id"].Value.ToString());

            //    //this.ly_sales_test_detail2TableAdapter.Fill(this.lYSalseMange.ly_sales_test_detail2, parentId);



            //}
        }

        private void AddDetail(string delivernum)
        {
           
            
            
            ////////////////////////////////////////////////////////////////////////////////////////
            SqlConnection sqlConnection1 = new SqlConnection(SQLDatabase.Connectstring);
            SqlCommand cmd = new SqlCommand();

            string MaxProductionorder = "";

            cmd.Parameters.Add("@deliver_mode", SqlDbType.VarChar);
            cmd.Parameters["@deliver_mode"].Value = "YY";


            cmd.CommandText = "LY_Make_Deliver_Detail";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = sqlConnection1;

            sqlConnection1.Open();
            MaxProductionorder = cmd.ExecuteScalar().ToString();
            sqlConnection1.Close();



            //return MaxProductionorder;
        }

        private void 装箱单打印总表ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (null == this.ly_sales_deliver_detailDataGridView.CurrentRow) return;



            NewFrm.Show(this); ;

            //this.ly_plan_getmaterialTableAdapter.Fill(this.lYPlanMange.ly_plan_getmaterial, parentId);

            BaseReportView queryForm = new BaseReportView();

            queryForm.Text = "中原精密营业发货装箱单";

            queryForm.Printdata = this.lYSalseMange;




            queryForm.PrintCrystalReport = new LY_YingyeHetong_FHZX2();




            NewFrm.Hide(this);

            queryForm.ShowDialog();
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
          //  this.ly_store_innum_purchaseTableAdapter.Fill(this.lYStoreMange.ly_store_innum_purchase, nowreceiveCode, SQLDatabase.NowUserID);

        }

        //private void fillToolStripButton_Click_1(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        this.ly_sales_receive_itemInstoreTableAdapter.Fill(this.lYSalseMange2.ly_sales_receive_itemInstore, receive_codeToolStripTextBox.Text, yonghu_codeToolStripTextBox.Text);
        //     this.ly_sales_receiveTableAdapter.Fill(this.lYSalseMange2.ly_sales_receive, salesperson_codeToolStripTextBox.Text, selcodeToolStripTextBox.Text, new System.Nullable<System.DateTime>(((System.DateTime)(System.Convert.ChangeType(begin_dateToolStripTextBox.Text, typeof(System.DateTime))))), new System.Nullable<System.DateTime>(((System.DateTime)(System.Convert.ChangeType(end_dateToolStripTextBox.Text, typeof(System.DateTime))))));
           
            
            
        //    }
        //    catch (System.Exception ex)
        //    {
        //        System.Windows.Forms.MessageBox.Show(ex.Message);
        //    }

        //}

      

       
       

       
    }

        
}
