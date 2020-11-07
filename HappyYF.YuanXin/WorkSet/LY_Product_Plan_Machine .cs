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
    public partial class LY_Product_Plan  : Form
    {
         string formState = "View";

         public LY_Product_Plan()
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

            this.ly_plan_getmaterialTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            this.ly_store_planitemcountTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            this.ly_material_plan_detailTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            this.ly_inma0010_planselTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            this.ly_material_plan_explodeTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;

            this.ly_material_plan_mainTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            this.ly_material_plan_mainTableAdapter.Fill(this.lYPlanMange.ly_material_plan_main,"SCJH");

            SetFormState("View");

        }

        private void SetFormState(string state)
        {
            // view edit add save cancle

            //return;

            if ("View" == state)
            {
                this.formState = "View";

                this.制定日期DateTimePicker.Enabled = false;

                this.说明TextBox.ReadOnly = true;
                this.启用CheckBox.Enabled  = false ;
                this.完成CheckBox.Enabled  = false ;
              



                this.bindingNavigatorMoveFirstItem.Enabled = true;
                this.bindingNavigatorMoveLastItem.Enabled = true;
                this.bindingNavigatorMoveNextItem.Enabled = true;
                this.bindingNavigatorMovePreviousItem.Enabled = true;
                this.bindingNavigatorPositionItem.Enabled = true;




                toolStripButton2.Enabled = true;
                bindingNavigatorDeleteItem.Enabled = true;
                bindingNavigatorAddNewItem.Enabled = true;
                ly_material_plan_mainBindingNavigatorSaveItem.Enabled = false;

                //yX_clientBindingNavigatorSaveItem.Enabled = true ;


                ly_material_plan_mainDataGridView.Enabled = true;


            }
            else
            {
                this.formState = "Edit";

                this.制定日期DateTimePicker.Enabled = true ;

                this.说明TextBox.ReadOnly = false ;
                this.启用CheckBox.Enabled  = true ;
                this.完成CheckBox.Enabled  = true;




                this.bindingNavigatorMoveFirstItem.Enabled = false ;
                this.bindingNavigatorMoveLastItem.Enabled = false ;
                this.bindingNavigatorMoveNextItem.Enabled = false ;
                this.bindingNavigatorMovePreviousItem.Enabled = false ;
                this.bindingNavigatorPositionItem.Enabled = false ;




                toolStripButton2.Enabled = false ;
                bindingNavigatorDeleteItem.Enabled = false ;
                bindingNavigatorAddNewItem.Enabled = false ;
                ly_material_plan_mainBindingNavigatorSaveItem.Enabled = true ;

                //yX_clientBindingNavigatorSaveItem.Enabled = true ;


                ly_material_plan_mainDataGridView.Enabled = false ;

              
            }


        }

        private string GetMaxPlanCode()
        {
            SqlConnection sqlConnection1 = new SqlConnection(SQLDatabase.Connectstring);
            SqlCommand cmd = new SqlCommand();

            string MaxPlanCode = "";

            cmd.Parameters.Add("@Plan_mode", SqlDbType.VarChar);
            cmd.Parameters["@Plan_mode"].Value = "SCJH";


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
            string message = "增加物料计划吗？";
            string caption = "提示...";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;

            DialogResult result;



            result = MessageBox.Show(message, caption, buttons,
            MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

            if (result == DialogResult.Yes)
            {
                this.ly_material_plan_mainBindingSource.AddNew();
                this.ly_material_plan_mainDataGridView.CurrentRow.Cells["计划编号"].Value = GetMaxPlanCode();
                this.ly_material_plan_mainDataGridView.CurrentRow.Cells["制定日期"].Value =DateTime .Now ;
                this.ly_material_plan_mainDataGridView.CurrentRow.Cells["制定人"].Value = SQLDatabase.nowUserName();
                this.ly_material_plan_mainBindingSource.EndEdit();

                this.Validate();
                this.ly_material_plan_mainBindingSource.EndEdit();

             

                    this.ly_material_plan_mainTableAdapter.Update(this.lYPlanMange.ly_material_plan_main);

              

                SetFormState("Edit");
                this.制定日期DateTimePicker.Focus();

                //DataRowView nowCard = (DataRowView)this.yX_clientBindingSource.Current;

                //   nowCard["Card_number"].; nowCard.

            }
        }

        private void bindingNavigatorDeleteItem_Click(object sender, EventArgs e)
        {
            if (null == this.ly_material_plan_mainDataGridView.CurrentRow) return;

            if (this.ly_material_plan_detailDataGridView.RowCount > 0)
            {
                MessageBox.Show("计划已有物料记录，不能删除(实需删除，请先删除该计划的物料记录)", "注意");
                return;

            }

            string nowPlanNumber = this.ly_material_plan_mainDataGridView.CurrentRow.Cells["计划编号"].Value.ToString();


            string message1 = "当前(计划：" + nowPlanNumber + ")将被删除，继续吗？";
            string caption1 = "提示...";
            MessageBoxButtons buttons1 = MessageBoxButtons.YesNo;

            DialogResult result1;



            result1 = MessageBox.Show(message1, caption1, buttons1,
            MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

            if (result1 == DialogResult.Yes)
            {

                string delstr = " delete ly_material_plan_main  where material_plan_num = '" + nowPlanNumber + "'";

           
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


                    this.ly_material_plan_mainTableAdapter.Fill(this.lYPlanMange.ly_material_plan_main, "SCJH");
                }


            }
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            if (null == ly_material_plan_mainDataGridView.CurrentRow) return;

            SetFormState("Edit");
        }

       
        private void toolStripTextBox2_KeyUp(object sender, KeyEventArgs e)
        {
            string dFilter = "";

            //for (int i = 0; i < this.hT_Vehicle_collectDataGridView.ColumnCount; i++)
            for (int i = 1; i < 10; i++)
            {
                string tempColumnName = this.ly_inma0010_planselDataGridView.Columns[i].DataPropertyName;

                if (i != 9)
                    dFilter = dFilter + tempColumnName + " like  '*" + this.toolStripTextBox2.Text + "*' or ";
                else
                    dFilter = dFilter + tempColumnName + " like  '*" + this.toolStripTextBox2.Text + "*' ";

            }

            if (this.toolStripTextBox2.Text.Replace(" ", "").Length > 0)

                this.ly_inma0010_planselBindingSource.Filter = dFilter;
            else
                this.ly_inma0010_planselBindingSource.Filter = " ";
        }

        private void toolStripTextBox2_Enter(object sender, EventArgs e)
        {
            toolStripTextBox2.Text = "";

            this.ly_inma0010_planselBindingSource.Filter = "";
        }

        private void ly_material_plan_mainDataGridView_SelectionChanged(object sender, EventArgs e)
        {
            if (this.formState == "View")
            {
                if (null != this.ly_material_plan_mainDataGridView.CurrentRow)
                {
                    int parentId = int.Parse(this.ly_material_plan_mainDataGridView.CurrentRow.Cells["id"].Value.ToString());
                    string planNum = this.ly_material_plan_mainDataGridView.CurrentRow.Cells["计划编号"].Value.ToString();

                    this.ly_material_plan_detailTableAdapter.Fill(this.lYPlanMange.ly_material_plan_detail, parentId);
                    this.ly_inma0010_planselTableAdapter .Fill(this.lYPlanMange.ly_inma0010_plansel, parentId);
                    ly_material_plan_explodeTableAdapter.Fill(this.lYPlanMange.ly_material_plan_explode, parentId);
                    this.ly_store_planitemcountTableAdapter.Fill(this.lYPlanMange.ly_store_planitemcount, parentId);
                    this.ly_plan_getmaterialTableAdapter.Fill(this.lYPlanMange.ly_plan_getmaterial, planNum);


                    this.groupBox3.Text = planNum +":物料列表";

                   
                }
            }
            else
            {
                // this.yX_taocan_mainBindingSource.Position = this.nowRow;
            }
        }

        private void ly_inma0010_planselDataGridView_DoubleClick(object sender, EventArgs e)
        {
            if (null == this.ly_material_plan_mainDataGridView.CurrentRow) return;
            if (null == ly_inma0010_planselDataGridView.CurrentRow) return;

            string componentNum = this.ly_inma0010_planselDataGridView.CurrentRow.Cells["物资编号"].Value.ToString();
            int parentId = int.Parse(this.ly_material_plan_mainDataGridView.CurrentRow.Cells["id"].Value.ToString());



          




                string insStr = " INSERT INTO ly_material_plan_detail  " +
               "( plan_id,wzbh) " +
               " values ('" + parentId + "','" + componentNum + "' )";


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
            


            this.ly_material_plan_detailTableAdapter.Fill(this.lYPlanMange.ly_material_plan_detail, parentId);
            this.ly_inma0010_planselTableAdapter.Fill(this.lYPlanMange.ly_inma0010_plansel, parentId);

            this.ly_material_plan_detailBindingSource.Position = this.ly_material_plan_detailBindingSource.Find("编号", componentNum);
        }

        private void 删除子件ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (null == this.ly_material_plan_detailDataGridView.CurrentRow) return;


            int nowId = int.Parse(this.ly_material_plan_detailDataGridView.CurrentRow.Cells["id1"].Value.ToString());
            int parentId = int.Parse(this.ly_material_plan_mainDataGridView.CurrentRow.Cells["id"].Value.ToString());
            string componentNum = this.ly_material_plan_detailDataGridView.CurrentRow.Cells["物料编号"].Value.ToString();


            string message1 = "当前(物料：" + componentNum + ")将被删除，继续吗？";
            string caption1 = "提示...";
            MessageBoxButtons buttons1 = MessageBoxButtons.YesNo;

            DialogResult result1;



            result1 = MessageBox.Show(message1, caption1, buttons1,
            MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

            if (result1 == DialogResult.Yes)
            {

                string delstr = " delete ly_material_plan_detail  where id = " + nowId + "";





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
                    this.ly_material_plan_detailTableAdapter.Fill(this.lYPlanMange.ly_material_plan_detail, parentId);
                    this.ly_inma0010_planselTableAdapter.Fill(this.lYPlanMange.ly_inma0010_plansel, parentId);

                    this.ly_inma0010_planselBindingSource.Position = this.ly_inma0010_planselBindingSource.Find("物资编号", componentNum);

                    CountPlanStru();
                }


            }
        }

        private void SaveChanged()
        {
            ///////////////////////////

            this.ly_material_plan_detailDataGridView.EndEdit();


            this.ly_material_plan_detailBindingSource.EndEdit();



            this.ly_material_plan_detailTableAdapter.Update(this.lYPlanMange.ly_material_plan_detail);



        }


        private void ly_material_plan_detailDataGridView_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            DataGridView dgv = sender as DataGridView;




            if ("数量" == dgv.CurrentCell.OwningColumn.Name)
            {

                ChangeValue queryForm = new ChangeValue();

                queryForm.OldValue = dgv.CurrentCell.Value.ToString();
                queryForm.NewValue = "";
                queryForm.ChangeMode = "value";
                queryForm.ShowDialog();




                if (queryForm.NewValue != "")
                {
                    dgv.CurrentRow.Cells["数量"].Value = queryForm.NewValue;
                    //dgv.CurrentRow.Cells["discount_money"].Value = DBNull.Value;

                    //dgv.CurrentRow.Cells["approve_flag"].Value = "False";
                    SaveChanged();

                 
                    CountPlanStru();

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

          


            if ("备注" == dgv.CurrentCell.OwningColumn.Name)
            {
                ChangeValue queryForm = new ChangeValue();

                queryForm.OldValue = dgv.CurrentCell.Value.ToString();
                queryForm.NewValue = "";
                queryForm.ShowDialog();




                if (queryForm.NewValue != "")
                {
                    dgv.CurrentRow.Cells["备注"].Value = queryForm.NewValue;
                    //dgv.CurrentRow.Cells["discount_money"].Value = DBNull.Value;

                    //dgv.CurrentRow.Cells["approve_flag"].Value = "False";
                    SaveChanged();

                }
                else
                {
                   

                }
                return;
            }
        }

        private void 计划物料计算ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (null == this.ly_material_plan_mainDataGridView.CurrentRow) return;

            if (this.formState != "View") return ;
            
                
                    int parentId = int.Parse(this.ly_material_plan_mainDataGridView.CurrentRow.Cells["id"].Value.ToString());
                    string planNum = this.ly_material_plan_mainDataGridView.CurrentRow.Cells["计划编号"].Value.ToString();

                  
                
           

            //////////////////////////////////
            
            string message = "计算物料计划物料需求吗？";
            string caption = "提示...";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;

            DialogResult result;



            result = MessageBox.Show(message, caption, buttons,
            MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

            if (result == DialogResult.Yes)
            {
                CountPlanStru();


                   



              

            }
        }

        private void CountPlanStru()
        {
            if (null == this.ly_material_plan_mainDataGridView.CurrentRow) return;

            int parentId = int.Parse(this.ly_material_plan_mainDataGridView.CurrentRow.Cells["id"].Value.ToString());
            string planNum = this.ly_material_plan_mainDataGridView.CurrentRow.Cells["计划编号"].Value.ToString();
            
            frmWaiting.Show(this);
            
            SqlConnection sqlConnection1 = new SqlConnection(SQLDatabase.Connectstring);
            SqlCommand cmd = new SqlCommand();



            cmd.Parameters.Add("@planId", SqlDbType.Int);
            cmd.Parameters["@planId"].Value = parentId;


            cmd.CommandText = "LY_PlanExplode";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = sqlConnection1;

            sqlConnection1.Open();
            cmd.ExecuteNonQuery();
            sqlConnection1.Close();


            ly_material_plan_explodeTableAdapter.Fill(this.lYPlanMange.ly_material_plan_explode, parentId);
            this.ly_store_planitemcountTableAdapter.Fill(this.lYPlanMange.ly_store_planitemcount, parentId);
            this.ly_plan_getmaterialTableAdapter.Fill(this.lYPlanMange.ly_plan_getmaterial, planNum);

            frmWaiting.Hide(this);
        }

        private void 打印PToolStripButton_Click(object sender, EventArgs e)
        {
            if (null == this.ly_store_planitemcountDataGridView1.CurrentRow) return;



            //string balanceFlag = this.xD_Sel_SellBalanceDataGridView.CurrentRow.Cells["结算"].Value.ToString();



            frmWaiting.Show(this);


            BaseReportView queryForm = new BaseReportView();

            queryForm.Text = "中原精密生产计划需求表";

            queryForm.Printdata = this.lYPlanMange;

            queryForm.PrintCrystalReport = new LY_ProductReport();

            //string nowCardNumber = this.xD_Sel_SellBalanceDataGridView.CurrentRow.Cells["编号"].Value.ToString();

            //string selectFormula;
            //selectFormula = "{XD_Sel_SellBalance.编号}   =   '" + nowCardNumber + "'";
            //queryForm.CrystalReportViewer1.SelectionFormula = selectFormula;

            //if (this.radioButton2.Checked)
            //{
            //    string selectFormula;
            //    selectFormula = "{XD_Sel_SellBalance.Card_number}   =   '" + nowCardNumber + "'";
            //    queryForm.PrintCrystalReport.DataDefinition.RecordSelectionFormula = selectFormula;
            //}




            //queryForm.PrintCrystalReport = new XD_SellBalance_All();

            //string nowCardNumber = this.xD_Sel_SellBalanceDataGridView.CurrentRow.Cells["编号"].Value.ToString();

            frmWaiting.Hide(this);

            queryForm.ShowDialog();
        }

        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            if (null == this.ly_store_planitemcountDataGridView1.CurrentRow) return;



            //string balanceFlag = this.xD_Sel_SellBalanceDataGridView.CurrentRow.Cells["结算"].Value.ToString();



            frmWaiting.Show(this);


            BaseReportView queryForm = new BaseReportView();

            queryForm.Text = "中原精密生产计划欠料表";

            queryForm.Printdata = this.lYPlanMange;

            queryForm.PrintCrystalReport = new LY_ProductReport_Owe();

            //string nowCardNumber = this.xD_Sel_SellBalanceDataGridView.CurrentRow.Cells["编号"].Value.ToString();

            //string selectFormula;
            //selectFormula = "{XD_Sel_SellBalance.编号}   =   '" + nowCardNumber + "'";
            //queryForm.CrystalReportViewer1.SelectionFormula = selectFormula;

            //if (this.radioButton2.Checked)
            //{
            //    string selectFormula;
            //    selectFormula = "{XD_Sel_SellBalance.Card_number}   =   '" + nowCardNumber + "'";
            //    queryForm.PrintCrystalReport.DataDefinition.RecordSelectionFormula = selectFormula;
            //}

            string selectFormula;
            //selectFormula = "{ly_store_planitemcount.status}  =   '原料' and {ly_store_planitemcount.owemoney} >0 ";
            selectFormula = "{ly_store_planitemcount.状态}  =   '原料'  and {ly_store_planitemcount.欠料金额}>0 ";
            queryForm.PrintCrystalReport.DataDefinition.RecordSelectionFormula = selectFormula;

            //((CrystalDecisions.CrystalReports.Engine.TextObject)(queryForm.PrintCrystalReport.DataDefinition.GroupNameFields.GroupHeaderSection1.ReportObjects["Text24"])).Text = "潼关中金冶炼有限责任公司原料结算单";


            //queryForm.PrintCrystalReport = new XD_SellBalance_All();

            //string nowCardNumber = this.xD_Sel_SellBalanceDataGridView.CurrentRow.Cells["编号"].Value.ToString();

            frmWaiting.Hide(this);

            queryForm.ShowDialog();
        }

        private void toolStripButton7_Click(object sender, EventArgs e)
        {
            if (null == this.ly_material_plan_explodeDataGridView.CurrentRow) return;

            int parentId = int.Parse(this.ly_material_plan_mainDataGridView.CurrentRow.Cells["id"].Value.ToString());


          


            frmWaiting.Show(this);

            //this.ly_plan_getmaterialTableAdapter.Fill(this.lYPlanMange.ly_plan_getmaterial, parentId);

            BaseReportView queryForm = new BaseReportView();

            queryForm.Text = "中原精密生产领料计划";

            queryForm.Printdata = this.lYPlanMange;

            queryForm.PrintCrystalReport = new LY_GetMaterial();


            //string selectFormula;

            //selectFormula = "{ly_store_planitemcount.状态}  =   '原料'  and {ly_store_planitemcount.欠料金额}>0 ";
            //queryForm.PrintCrystalReport.DataDefinition.RecordSelectionFormula = selectFormula;

            frmWaiting.Hide(this);

            queryForm.ShowDialog();
        }

        private void toolStripButton8_Click(object sender, EventArgs e)
        {
            ExportDataGridviewTOExcell.ExportDataGridview(this.ly_material_plan_explodeDataGridView, true);
        }

        private void toolStripButton9_Click(object sender, EventArgs e)
        {
            ExportDataGridviewTOExcell.ExportDataGridview(this.ly_plan_getmaterialDataGridView, true);
        }

        //private void fillToolStripButton_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        this.ly_plan_getmaterialTableAdapter.Fill(this.lYPlanMange.ly_plan_getmaterial, new System.Nullable<int>(((int)(System.Convert.ChangeType(plan_idToolStripTextBox.Text, typeof(int))))));
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
        //        this.ly_material_plan_explodeTableAdapter.Fill(this.lYPlanMange.ly_material_plan_explode, new System.Nullable<int>(((int)(System.Convert.ChangeType(plan_idToolStripTextBox.Text, typeof(int))))));
        //    }
        //    catch (System.Exception ex)
        //    {
        //        System.Windows.Forms.MessageBox.Show(ex.Message);
        //    }

        //}

       

        

      
    }
}
