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
    public partial class LY_Store_Out_Transfer  : Form
    {
         string formState = "View";

         string nowOutstyle="";
         string nowSubOutstyle="";

         public LY_Store_Out_Transfer()
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



            //this.ly_store_planoutTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            this.ly_materrial_sortTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            this.ly_storeout_employWarehouseTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;


            this.ly_plan_deptlistTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            this.ly_plan_parentlistTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;

            this.ly_store_out_transferMainTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            this.ly_store_out_transferDetailTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            
            this.lY_plan_getmaterial_transferOutTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            
            this.ly_material_plan_mainTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
           // this.ly_material_plan_mainTableAdapter.Fill(this.lYPlanMange.ly_material_plan_main,"LLJH");

            this.ly_material_plan_mainBindingSource.Filter = "启用='true'";

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

                this.comboBox1.Enabled = false;
                this.comboBox2.Enabled = false;


                this.bindingNavigatorMoveFirstItem.Enabled = true;
                this.bindingNavigatorMoveLastItem.Enabled = true;
                this.bindingNavigatorMoveNextItem.Enabled = true;
                this.bindingNavigatorMovePreviousItem.Enabled = true;
                this.bindingNavigatorPositionItem.Enabled = true;



                toolStripButton6.Visible = false;
                toolStripButton2.Enabled = true;
                bindingNavigatorDeleteItem.Enabled = true;
                bindingNavigatorAddNewItem.Enabled = true;
                ly_material_plan_mainBindingNavigatorSaveItem.Enabled = false;

                //yX_clientBindingNavigatorSaveItem.Enabled = true ;
                this.toolStripComboBox1.Enabled = true;

                ly_material_plan_mainDataGridView.Enabled = true;
                ly_store_outnumDataGridView.Enabled = true;


            }
            else
            {
                this.formState = "Edit";

                this.comboBox1.Enabled = true ;
                this.comboBox2.Enabled = true ;




                this.bindingNavigatorMoveFirstItem.Enabled = false ;
                this.bindingNavigatorMoveLastItem.Enabled = false ;
                this.bindingNavigatorMoveNextItem.Enabled = false ;
                this.bindingNavigatorMovePreviousItem.Enabled = false ;
                this.bindingNavigatorPositionItem.Enabled = false ;



                toolStripButton6.Visible = true ;
                toolStripButton2.Enabled = false ;
                bindingNavigatorDeleteItem.Enabled = false ;
                bindingNavigatorAddNewItem.Enabled = false ;
                ly_material_plan_mainBindingNavigatorSaveItem.Enabled = true ;

                //yX_clientBindingNavigatorSaveItem.Enabled = true ;
                this.toolStripComboBox1.Enabled = false;

                ly_material_plan_mainDataGridView.Enabled = false ;
                ly_store_outnumDataGridView.Enabled = false;
              
            }


        }

       

        private void bindingNavigatorAddNewItem_Click(object sender, EventArgs e)
        {
            
        }

        private void bindingNavigatorDeleteItem_Click(object sender, EventArgs e)
        {
            if (null == this.ly_material_plan_mainDataGridView.CurrentRow) return;

            //if (this.ly_material_plan_detailDataGridView.RowCount > 0)
            //{
            //    MessageBox.Show("计划已有物料记录，不能删除(实需删除，请先删除该计划的物料记录)", "注意");
            //    return;

            //}

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

       
        //private void toolStripTextBox2_KeyUp(object sender, KeyEventArgs e)
        //{
        //    string dFilter = "";

        //    //for (int i = 0; i < this.hT_Vehicle_collectDataGridView.ColumnCount; i++)
        //    for (int i = 1; i < 10; i++)
        //    {
        //        string tempColumnName = this.ly_store_planoutDataGridView.Columns[i].DataPropertyName;

        //        if (i != 9)
        //            dFilter = dFilter + tempColumnName + " like  '*" + this.toolStripTextBox2.Text + "*' or ";
        //        else
        //            dFilter = dFilter + tempColumnName + " like  '*" + this.toolStripTextBox2.Text + "*' ";

        //    }

        //    if (this.toolStripTextBox2.Text.Replace(" ", "").Length > 0)

        //        this.ly_store_planoutBindingSource.Filter = dFilter;
        //    else
        //        this.ly_store_planoutBindingSource.Filter = " ";
        //}

        //private void toolStripTextBox2_Enter(object sender, EventArgs e)
        //{
        //    toolStripTextBox2.Text = "";

        //    this.ly_store_planoutBindingSource.Filter = "";
        //}

        private void ly_material_plan_mainDataGridView_SelectionChanged(object sender, EventArgs e)
        {
            if (this.formState == "View")
            {
                if (null != this.ly_material_plan_mainDataGridView.CurrentRow)
                {
                    int parentId = int.Parse(this.ly_material_plan_mainDataGridView.CurrentRow.Cells["id"].Value.ToString());
                    string planNum = this.ly_material_plan_mainDataGridView.CurrentRow.Cells["计划编号"].Value.ToString();



                    this.lY_plan_getmaterial_transferOutTableAdapter.Fill(this.lYPlanMange.lY_plan_getmaterial_transferOut, planNum);
                    //this.ly_store_planoutTableAdapter.Fill(this.lYStoreMange.ly_store_planout, planNum);
                    this.ly_store_out_transferMainTableAdapter.Fill(this.lYStoreMange.ly_store_out_transferMain, planNum, SQLDatabase.NowUserID);

                    this.ly_storeout_employWarehouseTableAdapter.Fill(this.lYStoreMange.ly_storeout_employWarehouse, "asd", "asd", "asd");
                    this.ly_plan_deptlistTableAdapter.Fill(this.lYStoreMange.ly_plan_deptlist, "asd");

                    this.ly_plan_deptlistTableAdapter.Fill(this.lYStoreMange.ly_plan_deptlist, planNum);

                    if (this.comboBox1.Items.Count > 0)
                    {
                        this.comboBox1.SelectedIndex = 0;
                        this.ly_storeout_employWarehouseTableAdapter.Fill(this.lYStoreMange.ly_storeout_employWarehouse, planNum, this.comboBox1.SelectedValue.ToString(), SQLDatabase.NowUserID);
                    }

                    //this.ly_plan_parentlistTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;

                    this.lY_plan_getmaterial_transferOutBindingSource.Filter = "领料部门='asd'";

                    if (null == this.ly_store_outnumDataGridView.CurrentRow)
                    {
                        this.ly_store_out_transferDetailTableAdapter.Fill(this.lYStoreMange.ly_store_out_transferDetail, "asd", SQLDatabase.nowUserName());

                    }

                    this.groupBox3.Text = planNum + ":物料列表";


                }
                else
                {
  
                    this.lY_plan_getmaterial_transferOutTableAdapter.Fill(this.lYPlanMange.lY_plan_getmaterial_transferOut, "asd");
                    //this.ly_store_planoutTableAdapter.Fill(this.lYStoreMange.ly_store_planout, "asd");
                    this.ly_store_out_transferMainTableAdapter.Fill(this.lYStoreMange.ly_store_out_transferMain, "asd", SQLDatabase.NowUserID);

                    //this.ly_storeout_employWarehouseTableAdapter.Fill(this.lYStoreMange.ly_storeout_employWarehouse, "asd", "asd", "asd");
                    //this.ly_plan_deptlistTableAdapter.Fill(this.lYStoreMange.ly_plan_deptlist, "asd");

                    //this.ly_plan_deptlistTableAdapter.Fill(this.lYStoreMange.ly_plan_deptlist, "asd");

   

            

                    if (null == this.ly_store_outnumDataGridView.CurrentRow)
                    {
                        this.ly_store_out_transferDetailTableAdapter.Fill(this.lYStoreMange.ly_store_out_transferDetail, "asd", SQLDatabase.nowUserName());

                    }

                     

                }
            }
            else
            {
                // this.yX_taocan_mainBindingSource.Position = this.nowRow;
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
            int parentId = int.Parse(this.ly_material_plan_mainDataGridView.CurrentRow.Cells["id"].Value.ToString());
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
                this.ly_store_out_transferDetailBindingSource.RemoveCurrent();
                SaveChanged();

            }
        }

        private void SaveChanged()
        {
            ///////////////////////////

            this.ly_store_outDataGridView.EndEdit();


            this.ly_store_out_transferDetailBindingSource.EndEdit();



            this.ly_store_out_transferDetailTableAdapter.Update(this.lYStoreMange .ly_store_out_transferDetail);

            string planNum = this.ly_material_plan_mainDataGridView.CurrentRow.Cells["计划编号"].Value.ToString();

            this.lY_plan_getmaterial_transferOutTableAdapter.Fill(this.lYPlanMange.lY_plan_getmaterial_transferOut, planNum);



        }


        private void ly_material_plan_detailDataGridView_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            
            DataGridView dgv = sender as DataGridView;

            if ("True" == ly_store_outDataGridView.CurrentRow.Cells["finished1"].Value.ToString())
            {
                MessageBox.Show("已经签证,领料单不能修改...");

                return;

            }

            if ( SQLDatabase.nowUserName() != (ly_store_outnumDataGridView.CurrentRow.Cells["发料人"].Value.ToString()))
            {

                MessageBox.Show("请发料人:" + ly_store_outnumDataGridView.CurrentRow.Cells["发料人"].Value.ToString() + " 修改");

                return;
            }


            if ("领料数量1" == dgv.CurrentCell.OwningColumn.Name)
            {
                if (ly_store_outDataGridView.CurrentRow.Cells["仓库1"].Value.ToString().Contains("成品"))
                {

                    return;
                }


                ChangeValue queryForm = new ChangeValue();

                queryForm.OldValue = dgv.CurrentCell.Value.ToString();
                queryForm.NewValue = "";
                queryForm.ChangeMode = "value";
                queryForm.ShowDialog();

                decimal  oldnum = decimal.Parse(dgv.CurrentCell.Value.ToString());
                decimal storenum = decimal.Parse(dgv.CurrentRow .Cells ["storecount"].Value .ToString ());
                decimal stanterdnum = 0;

                if (null != this.ly_plan_getmaterialDataGridView.CurrentRow)
                {
                    stanterdnum = decimal.Parse(this.ly_plan_getmaterialDataGridView.CurrentRow.Cells["未领数量"].Value.ToString());
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
                        dgv.CurrentRow.Cells["领料数量1"].Value = queryForm.NewValue;
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




            if ("部门名称" == dgv.CurrentCell.OwningColumn.Name)
            {

                return;
                //ChangeValue queryForm = new ChangeValue();

                //queryForm.OldValue = dgv.CurrentCell.Value.ToString();
                //queryForm.NewValue = "";
                //queryForm.ShowDialog();




                //if (queryForm.NewValue != "")
                //{
                //    dgv.CurrentRow.Cells["部门名称"].Value = queryForm.NewValue;
                //    //dgv.CurrentRow.Cells["discount_money"].Value = DBNull.Value;

                //    //dgv.CurrentRow.Cells["approve_flag"].Value = "False";
                //    SaveChanged();

                //}
                //else
                //{


                //}
                //return;

                //////////////////

                string sel = "SELECT a.prodname as 编码,a.prodcode as 名称 FROM ly_prod_dept a ";


                QueryForm queryForm = new QueryForm();


                queryForm.Sel = sel;
                queryForm.Constr = SQLDatabase.Connectstring;



                queryForm.ShowDialog();




                dgv.CurrentRow.Cells["部门名称"].Value = queryForm.Result; ;
                SaveChanged();



                return;
            }
        }

        private void 计划物料计算ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (null == this.ly_store_outnumDataGridView.CurrentRow) return;

            if (this.formState != "View") return ;

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

                this.ly_store_out_transferMainBindingSource.RemoveCurrent();   



              

            }
        }

        private string GetMaxOutNum()
        {
            SqlConnection sqlConnection1 = new SqlConnection(SQLDatabase.Connectstring);
            SqlCommand cmd = new SqlCommand();

            string MaxPlanCode = "";

            //cmd.Parameters.Add("@Plan_mode", SqlDbType.VarChar);
            //cmd.Parameters["@Plan_mode"].Value = "LLJH";


            cmd.CommandText = "LY_Get_InNumber_maintenance";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = sqlConnection1;

            sqlConnection1.Open();
            MaxPlanCode = cmd.ExecuteScalar().ToString();
            sqlConnection1.Close();



            return MaxPlanCode;
        }

        private void CountStoreOutAuto()
        {
            if (null == this.ly_material_plan_mainDataGridView.CurrentRow) return;
            if (null == comboBox1.SelectedValue) return;
            if (null==comboBox2.SelectedValue) return ;

            this.nowSubOutstyle = "";


            string planNum = this.ly_material_plan_mainDataGridView.CurrentRow.Cells["计划编号"].Value.ToString();

            if ("LLJH" == planNum.Substring(0, 4) || "TZJH" == planNum.Substring(0, 4))
            {
                string sel = "SELECT a.stylecode as 编码,a.stylename as 名称 FROM ly_store_out_styleset a where a.stylecode<>'001'  ";


                QueryForm queryForm = new QueryForm();


                queryForm.Sel = sel;
                queryForm.Constr = SQLDatabase.Connectstring;



                queryForm.ShowDialog();



                if (string.IsNullOrEmpty(queryForm.Result1))
                {
                    MessageBox.Show("必须选择出库类别,才能出库...", "注意");
                    return;
                }
                else
                {
                    this.nowOutstyle = queryForm.Result1;

                    if ("物料消耗" == this.nowOutstyle)
                    {
                        string subsel = "SELECT a.substylecode as 子项编码,a.substylename as 子项名称 FROM ly_store_out_substyleset a ";


                        QueryForm subqueryForm = new QueryForm();


                        subqueryForm.Sel = subsel;
                        subqueryForm.Constr = SQLDatabase.Connectstring;



                        subqueryForm.ShowDialog();

                        if (string.IsNullOrEmpty(subqueryForm.Result1))
                        {
                            MessageBox.Show("必须选择物料消耗子项,才能出库...", "注意");
                            return;
                        }
                        else
                        {
                            this.nowSubOutstyle = subqueryForm.Result1;

                        }


                    }


                }

            }
            else if ("WXLL" == planNum.Substring(0, 4) )
            {

                this.nowOutstyle = "营业维修领料";
            }

                //string componentNum = this.lY_dayget_material_selDataGridView.CurrentRow.Cells["物资编号"].Value.ToString();
                int parentId = int.Parse(this.ly_material_plan_mainDataGridView.CurrentRow.Cells["id"].Value.ToString());


            NewFrm.Show(this);
            
            SqlConnection sqlConnection1 = new SqlConnection(SQLDatabase.Connectstring);
            SqlCommand cmd = new SqlCommand();



            cmd.Parameters.Add("@plan_num", SqlDbType.VarChar );
            cmd.Parameters["@plan_num"].Value = planNum;

            cmd.Parameters.Add("@prod_dept", SqlDbType.VarChar );
            cmd.Parameters["@prod_dept"].Value = comboBox1 .SelectedValue ;

            cmd.Parameters.Add("@warehousename", SqlDbType.VarChar);
            cmd.Parameters["@warehousename"].Value = comboBox2.SelectedValue;

            string outNum = GetMaxOutNum();
            cmd.Parameters.Add("@out_number", SqlDbType.VarChar);
            cmd.Parameters["@out_number"].Value = outNum;

            cmd.Parameters.Add("@faliaoren", SqlDbType.VarChar);
            cmd.Parameters["@faliaoren"].Value = SQLDatabase.nowUserName();

            cmd.Parameters.Add("@nowoutstyle", SqlDbType.VarChar);
            cmd.Parameters["@nowoutstyle"].Value = this.nowOutstyle;

            cmd.Parameters.Add("@nowsuboutstyle", SqlDbType.VarChar);
            cmd.Parameters["@nowsuboutstyle"].Value = this.nowSubOutstyle;



                      
               
                
              
                //,@ varchar(20))  

            cmd.CommandText = "LY_store_out_input_transfer";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = sqlConnection1;

            cmd.CommandTimeout = 0;
         
            sqlConnection1.Open();

           
            
           
            try
            {

                cmd.ExecuteNonQuery();



             


            }
            catch (SqlException sqle)
            {


                MessageBox.Show(sqle.Message.Split('*')[0]);
            }


            finally
            {
                sqlConnection1.Close();


            }


           

            // 

            this.ly_store_out_transferDetailTableAdapter.Fill(this.lYStoreMange.ly_store_out_transferDetail, outNum, SQLDatabase.nowUserName());
            this.lY_plan_getmaterial_transferOutTableAdapter.Fill(this.lYPlanMange.lY_plan_getmaterial_transferOut, planNum);
            this.ly_store_out_transferMainTableAdapter.Fill(this.lYStoreMange.ly_store_out_transferMain, planNum, SQLDatabase.NowUserID);

            NewFrm.Hide(this);
        }

       

     

        private void toolStripButton7_Click(object sender, EventArgs e)
        {
            if (null == this.ly_plan_getmaterialDataGridView.CurrentRow) return;

            int parentId = int.Parse(this.ly_material_plan_mainDataGridView.CurrentRow.Cells["id"].Value.ToString());





            frmWaiting.Show(this);

            //this.ly_plan_getmaterialTableAdapter.Fill(this.lYPlanMange.ly_plan_getmaterial, parentId);

            BaseReportView queryForm = new BaseReportView();

            queryForm.Text = "中原精密计划领料单";

            queryForm.Printdata = this.lYPlanMange;

            queryForm.PrintCrystalReport = new LY_GetMaterial_Dayget();


            //string selectFormula;

            //selectFormula = "{ly_store_planitemcount.状态}  =   '原料'  and {ly_store_planitemcount.欠料金额}>0 ";
            //queryForm.PrintCrystalReport.DataDefinition.RecordSelectionFormula = selectFormula;

            frmWaiting.Hide(this);

            queryForm.ShowDialog();
        }

      

        private void toolStripButton9_Click(object sender, EventArgs e)
        {
            ExportDataGridviewTOExcell.ExportDataGridview(this.ly_plan_getmaterialDataGridView, true);
        }

        private void toolStripComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ToolStripComboBox tsc = sender as ToolStripComboBox;

            //MessageBox .Show (tsc .SelectedIndex .ToString ());

            if (tsc.SelectedIndex == 0)
            {
                this.ly_plan_getmaterialDataGridView.Columns["父件"].Visible = false;
                this.ly_plan_getmaterialDataGridView.Columns["父件名称"].Visible = false;
                this.ly_plan_getmaterialDataGridView.Columns["生产数量"].Visible = false;
                this.ly_material_plan_mainBindingSource.Filter = "启用=1 and 计划编号>'WXLL0000013'";
                this.ly_material_plan_mainDataGridView.Columns["出库指令"].Visible = false;

                this.label2.Visible = true;
                this.comboBox2.Visible = true;

                //this.ly_material_plan_mainDataGridView.SelectionChanged -= this.ly_material_plan_mainDataGridView_SelectionChanged;
                this.ly_material_plan_mainTableAdapter.Fill(this.lYPlanMange.ly_material_plan_main, "WXLL");
                //this.ly_material_plan_mainDataGridView.SelectionChanged += this.ly_material_plan_mainDataGridView_SelectionChanged;
                //this.ly_plan_getmaterialDataGridView.Columns["父件"].Visible = false ;
                //this.ly_plan_getmaterialDataGridView.Columns["父件名称"].Visible = false ;
                //this.ly_plan_getmaterialDataGridView.Columns["生产数量"].Visible = false ;
                //this.ly_material_plan_mainBindingSource.Filter = "启用=1 and 批准=1";
                //this.ly_material_plan_mainDataGridView.Columns["出库指令"].Visible = false ;

                ////this.comboBox1.Visible = false;
                //this.label2.Visible = true  ;
                //this.comboBox2.Visible = true;

                //this.ly_material_plan_mainTableAdapter.Fill(this.lYPlanMange.ly_material_plan_main, "SCJH");
            }
            //else if (tsc.SelectedIndex == 1)
            //{
            //    this.ly_plan_getmaterialDataGridView.Columns["父件"].Visible = false;
            //    this.ly_plan_getmaterialDataGridView.Columns["父件名称"].Visible = false;
            //    this.ly_plan_getmaterialDataGridView.Columns["生产数量"].Visible = false;
            //    this.ly_material_plan_mainBindingSource.Filter = "启用=1";
            //    this.ly_material_plan_mainDataGridView.Columns["出库指令"].Visible = false ;

            //    //this.comboBox1.Visible = false;
                
            //    this.label2.Visible = true;
            //    this.comboBox2.Visible = true;
            //    //this.ly_material_plan_mainBindingSource.Filter = "";
            //    this.ly_material_plan_mainTableAdapter.Fill(this.lYPlanMange.ly_material_plan_main, "LLJH");
            //}

            //else if (tsc.SelectedIndex == 2)
            //{
            //    this.ly_plan_getmaterialDataGridView.Columns["父件"].Visible = false;
            //    this.ly_plan_getmaterialDataGridView.Columns["父件名称"].Visible = false;
            //    this.ly_plan_getmaterialDataGridView.Columns["生产数量"].Visible = false;

            //    this.ly_material_plan_mainDataGridView.Columns["出库指令"].Visible = true;

            //    //this.comboBox1.Visible = false;
            //    this.label2.Visible = true;
            //    this.comboBox2.Visible = true;
            //    this.ly_material_plan_mainBindingSource.Filter = "";
            //    this.ly_material_plan_mainTableAdapter.Fill(this.lYPlanMange.ly_material_plan_main, "SCPT");
            //}
            //else if (tsc.SelectedIndex == 3)
            //{
            //    this.ly_material_plan_mainBindingSource.Filter = "计划编号 = 'LSPT000aaaa'";
            //    return;
                
            //    this.ly_plan_getmaterialDataGridView.Columns["父件"].Visible = false;
            //    this.ly_plan_getmaterialDataGridView.Columns["父件名称"].Visible = false;
            //    this.ly_plan_getmaterialDataGridView.Columns["生产数量"].Visible = false;

            //    this.ly_material_plan_mainDataGridView.Columns["出库指令"].Visible = true;

            //    //this.comboBox1.Visible = false;
            //    this.label2.Visible = true;
            //    this.comboBox2.Visible = true;
            //    this.ly_material_plan_mainBindingSource.Filter = "计划编号 < 'LSPT0002096'";
            //    this.ly_material_plan_mainTableAdapter.Fill(this.lYPlanMange.ly_material_plan_main, "LSPT");
            //}
            //else if (tsc.SelectedIndex == 4)
            //{
            //    this.ly_plan_getmaterialDataGridView.Columns["父件"].Visible = false;
            //    this.ly_plan_getmaterialDataGridView.Columns["父件名称"].Visible = false;
            //    this.ly_plan_getmaterialDataGridView.Columns["生产数量"].Visible = false;
            //    this.ly_material_plan_mainBindingSource.Filter = "启用=1";
            //    this.ly_material_plan_mainDataGridView.Columns["出库指令"].Visible = false; 

            //    this.label2.Visible = true;
            //    this.comboBox2.Visible = true; 
            //    this.ly_material_plan_mainTableAdapter.Fill(this.lYPlanMange.ly_material_plan_main, "TZJH");
            //}
            //else if (tsc.SelectedIndex == 5)
            //{
            //    //MessageBox.Show("盘点调整中,出库暂停...");

            //    //return;

            //    this.ly_plan_getmaterialDataGridView.Columns["父件"].Visible = false;
            //    this.ly_plan_getmaterialDataGridView.Columns["父件名称"].Visible = false;
            //    this.ly_plan_getmaterialDataGridView.Columns["生产数量"].Visible = false;
            //    this.ly_material_plan_mainBindingSource.Filter = "启用=1 and 计划编号>'WXLL0000013'";
            //    this.ly_material_plan_mainDataGridView.Columns["出库指令"].Visible = false;

            //    this.label2.Visible = true;
            //    this.comboBox2.Visible = true;

            //    this.ly_material_plan_mainTableAdapter.Fill(this.lYPlanMange.ly_material_plan_main, "WXLL");
            //}
        }

        private void ly_plan_getmaterialDataGridView_DoubleClick(object sender, EventArgs e)
        {
            if (null == this.ly_material_plan_mainDataGridView.CurrentRow) return;
            if (null == ly_plan_getmaterialDataGridView.CurrentRow) return;

            //string componentNum = this.lY_dayget_material_selDataGridView.CurrentRow.Cells["物资编号"].Value.ToString();
            //int parentId = int.Parse(this.ly_material_plan_mainDataGridView.CurrentRow.Cells["id"].Value.ToString());


           // CountPlanStru();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int parentId = int.Parse(this.ly_material_plan_mainDataGridView.CurrentRow.Cells["id"].Value.ToString());
            string planNum = this.ly_material_plan_mainDataGridView.CurrentRow.Cells["计划编号"].Value.ToString();

            //MessageBox.Show(this .comboBox1 .SelectedValue .ToString ());
            if (null != this.comboBox1.SelectedValue)
            {
                this.ly_storeout_employWarehouseTableAdapter.Fill(this.lYStoreMange.ly_storeout_employWarehouse, planNum, this.comboBox1.SelectedValue.ToString(), SQLDatabase.NowUserID);
                if (0 == this.comboBox2.Items.Count)
                {
                    this.lY_plan_getmaterial_transferOutBindingSource.Filter = "领料部门='" + "asd" + "'";
                }
                //if (this.toolStripComboBox1.SelectedIndex == 0)
                //{
                //    this.ly_materrial_sortTableAdapter .Fill(this.lYStoreMange.ly_materrial_sort , planNum, this.comboBox1.SelectedValue.ToString());

                //    //this.ly_plan_getmaterial_departmentBindingSource.Filter = "领料部门='" + this.comboBox1.SelectedValue.ToString() + "'";
                //}

                //if (this.toolStripComboBox1.SelectedIndex == 1)
                //{
                //    //this.ly_plan_getmaterial_departmentBindingSource.Filter = "领料部门='" + this.comboBox1.SelectedValue.ToString() + "'";
                //}
            }

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (null != this.comboBox1.SelectedValue)
            {
                //if (this.toolStripComboBox1.SelectedIndex == 0)
                //{
                if (null != this.comboBox2.SelectedValue)
                {
                    if ("全部" != this.comboBox2.SelectedValue.ToString())
                    {
                        //this.ly_plan_getmaterial_departmentBindingSource.Filter = "";
                        this.lY_plan_getmaterial_transferOutBindingSource.Filter = "领料部门='" + this.comboBox1.SelectedValue.ToString() + "' and 仓库='" + this.comboBox2.SelectedValue.ToString() + "'";
                    }
                    else
                    {

                        this.lY_plan_getmaterial_transferOutBindingSource.Filter = "领料部门='" + this.comboBox1.SelectedValue.ToString() + "'";
                    }
                }
                //}
            }
            else
            {
                this.lY_plan_getmaterial_transferOutBindingSource.Filter = "领料部门='" + "asd" + "'";
            
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton rdb = sender as RadioButton;
            if (rdb.Checked)
            {
                SetFormState("View");
                this.ly_store_out_transferDetailTableAdapter.Fill(this.lYStoreMange.ly_store_out_transferDetail, "asd", SQLDatabase.nowUserName());
                //this.ly_plan_getmaterialTableAdapter.Fill(this.lYPlanMange.ly_plan_getmaterial, "asd");
                this.lY_plan_getmaterial_transferOutBindingSource.Filter = "领料部门='asd'";
            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
             RadioButton rdb = sender as RadioButton;
             if (rdb.Checked)
             {
                 SetFormState("Edit");
                 this.ly_store_out_transferDetailTableAdapter.Fill(this.lYStoreMange.ly_store_out_transferDetail, "asd", SQLDatabase.nowUserName());
             }
        }

        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            if (null == this.ly_material_plan_mainDataGridView.CurrentRow) return;
            if (null == ly_plan_getmaterialDataGridView.CurrentRow) return;


            //if (2 == this.toolStripComboBox1.SelectedIndex)
            //{
            //    if ("True" != this.ly_material_plan_mainDataGridView.CurrentRow.Cells["出库指令"].Value.ToString())
            //    {
            //        MessageBox.Show("营业部未发出临时配套出库指令,不能出库...", "注意");
            //        return;
            //    }
            //}

             string message = "确定调拨出库吗?";
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

        private void ly_store_outnumDataGridView_SelectionChanged(object sender, EventArgs e)
        {
            if (null == ly_store_outnumDataGridView.CurrentRow) return;

            if (this.formState == "View")
            {
                if (null != this.ly_store_outnumDataGridView.CurrentRow)
                {

                    string outNum = this.ly_store_outnumDataGridView.CurrentRow.Cells["out_number"].Value.ToString();


                    this.ly_store_out_transferDetailTableAdapter.Fill(this.lYStoreMange.ly_store_out_transferDetail, outNum, SQLDatabase.nowUserName());

                    string deptcode =this.ly_store_outnumDataGridView.CurrentRow.Cells["out_deptcode"].Value.ToString();
                    string warehouse = this.ly_store_outnumDataGridView.CurrentRow.Cells["warehouse"].Value.ToString();

                    if (!string.IsNullOrEmpty(deptcode))
                    {
                        if ("全部" == warehouse)
                        {

                            this.lY_plan_getmaterial_transferOutBindingSource.Filter = "领料部门='" + deptcode + "'";
                            this.comboBox1.SelectedValue = deptcode;
                            this.comboBox2.SelectedValue = warehouse;
                        }
                        else
                        {
                            this.lY_plan_getmaterial_transferOutBindingSource.Filter = "领料部门='" + deptcode + "' and 仓库='" + warehouse + "'";
                            this.comboBox1.SelectedValue = deptcode;
                            this.comboBox2.SelectedValue = warehouse;
                        }

                       

                    }

                    //if (!string.IsNullOrEmpty(deptcode))
                    //{
                    //    if (this.toolStripComboBox1.SelectedIndex == 0)
                    //    {
                    //        //if (!string.IsNullOrEmpty(parentno))
                    //        //{
                    //            this.ly_plan_getmaterial_departmentBindingSource.Filter = "领料部门='" + deptcode +  "'";
                    //            this.comboBox1.SelectedValue = deptcode;
                    //            this.comboBox2.SelectedValue = sortname;
                    //        //}
                    //    }
                    //}
                    //if (this.toolStripComboBox1.SelectedIndex == 1)
                    //{
                    //    this.comboBox1.SelectedValue = deptcode;
                    //    this.ly_plan_getmaterial_departmentBindingSource.Filter = "领料部门='" + deptcode + "'";
                    //}

                }
            }
            else
            {
                // this.yX_taocan_mainBindingSource.Position = this.nowRow;
            }
        }

        private void ly_plan_getmaterialDataGridView_SelectionChanged(object sender, EventArgs e)
        {
            if (null == this.ly_plan_getmaterialDataGridView.CurrentRow) return;
            if (null == this.ly_store_outDataGridView.CurrentRow) return;

            /////////////////////////

               DataGridView dgv = sender as DataGridView;




                string nowitem = dgv.CurrentRow.Cells["物料编号"].Value.ToString();
                //ly_store_planoutBindingSource.Filter = "物料编号='" + nowitem+"'";

            ly_store_outDataGridView.SelectionChanged -= ly_store_outDataGridView_SelectionChanged;

            this.ly_store_out_transferDetailBindingSource.Position = this.ly_store_out_transferDetailBindingSource.Find("物料编号", nowitem);
            
            ly_store_outDataGridView.SelectionChanged += ly_store_outDataGridView_SelectionChanged;
        }

        private void ly_store_outDataGridView_SelectionChanged(object sender, EventArgs e)
        {
            if (null == this.ly_store_outDataGridView.CurrentRow) return;
            if (null == this.ly_plan_getmaterialDataGridView.CurrentRow) return;

            DataGridView dgv = sender as DataGridView;

            string nowitem = dgv.CurrentRow.Cells["物料编号1"].Value.ToString();

            ly_plan_getmaterialDataGridView.SelectionChanged -= ly_plan_getmaterialDataGridView_SelectionChanged;
            this.lY_plan_getmaterial_transferOutBindingSource.Position = this.lY_plan_getmaterial_transferOutBindingSource.Find("物料编号", nowitem);
            ly_plan_getmaterialDataGridView.SelectionChanged += ly_plan_getmaterialDataGridView_SelectionChanged;
        }

        private void ly_store_outnumDataGridView_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            DataGridView dgv = sender as DataGridView;

               if (SQLDatabase .nowUserName()  != dgv.CurrentRow.Cells["发料人"].Value.ToString())
                {

                    MessageBox.Show("请发料人:" + dgv.CurrentRow.Cells["发料人"].Value.ToString() +" 修改");

                    return;
                }

            /////////////////////////////////

               if ("out_style" == dgv.CurrentCell.OwningColumn.Name)
               {
                   if ("True" == ly_store_outDataGridView.CurrentRow.Cells["finished1"].Value.ToString())
                   {
                       MessageBox.Show("已经签证,不能修改出库类别...");

                       return;

                   }





                   string sel = "SELECT a.stylecode as 编码,a.stylename as 名称 FROM ly_store_out_styleset a where a.stylecode<>'001'  ";


                   QueryForm queryForm = new QueryForm();


                   queryForm.Sel = sel;
                   queryForm.Constr = SQLDatabase.Connectstring;



                   queryForm.ShowDialog();




                   //dgv.CurrentRow.Cells["out_style"].Value = queryForm.Result;
                   dgv.CurrentCell.Value = queryForm.Result1;
                   SaveOutStyle(queryForm.Result1);



                   return;

               }


               /////////////////////////////////////////////////////

            if ("employe" == dgv.CurrentCell.OwningColumn.Name)
            {
                if ("True" == ly_store_outDataGridView.CurrentRow.Cells["finished1"].Value.ToString())
                {
                    MessageBox.Show("已经签证,不能修改领料人...");

                    return;

                }

             
                
                ChangeValue queryForm = new ChangeValue();

                queryForm.OldValue = dgv.CurrentCell.Value.ToString();
                queryForm.NewValue = "";
                queryForm.ChangeMode = "string";
                queryForm.ShowDialog();

              

                if (queryForm.NewValue != "")
                {

                    dgv.CurrentCell.Value = queryForm.NewValue;

                    SaveEmploye(queryForm.NewValue);

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



            if ("finished" == dgv.CurrentCell.OwningColumn.Name)
            {
               return;


                if ("True" != dgv.CurrentRow.Cells["finished"].Value.ToString())
                {

                    string message = "确定领料完成吗?";
                    string caption = "提示...";
                    MessageBoxButtons buttons = MessageBoxButtons.YesNo;
                    DialogResult result;



                    result = MessageBox.Show(message, caption, buttons,
                    MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

                    if (result == DialogResult.Yes)
                    {

                        //dgv.CurrentRow.Cells["discount_money"].Value = dgv.CurrentRow.Cells["apply_money"].Value;
                        dgv.CurrentRow.Cells["finished"].Value = "True";
                        Savefinished("1");
                    }

                }
                else
                {

                    string message = "取消领料完成吗?";
                    string caption = "提示...";
                    MessageBoxButtons buttons = MessageBoxButtons.YesNo;
                    DialogResult result;



                    result = MessageBox.Show(message, caption, buttons,
                    MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

                    if (result == DialogResult.Yes)
                    {
                        //dgv.CurrentRow.Cells["discount_money"].Value = DBNull.Value;
                        //dgv.CurrentRow.Cells["apply_money"].Value = DBNull.Value;
                        dgv.CurrentRow.Cells["finished"].Value = "False";
                        Savefinished("0");
                    }
                }

                return;
            }


           
        }
        private void SaveEmploye(string employeName)
        {
           

            string outNum = this.ly_store_outnumDataGridView.CurrentRow.Cells["out_number"].Value.ToString();
            string nowwarehouse = this.ly_store_outnumDataGridView.CurrentRow.Cells["warehouse"].Value.ToString();

          

            string delstr = " update ly_store_out  set employe='" + employeName + "'" +
              " from ly_store_out left join ly_inma0010 on ly_store_out.wzbh=ly_inma0010.wzbh " +
             " where ly_store_out.out_number = '" + outNum + "' and ly_inma0010.warehouse='" + nowwarehouse + "'";



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
        private void SaveOutStyle(string outstyle)
        {
            string outNum = this.ly_store_outnumDataGridView.CurrentRow.Cells["out_number"].Value.ToString();
            string nowwarehouse = this.ly_store_outnumDataGridView.CurrentRow.Cells["warehouse"].Value.ToString();

            //string delstr = " update ly_store_out set out_style='" + outstyle + "' where out_number = '" + outNum + "'";

            string delstr = " update ly_store_out  set out_style='" + outstyle + "'" +
              " from ly_store_out left join ly_inma0010 on ly_store_out.wzbh=ly_inma0010.wzbh " +
             " where ly_store_out.out_number = '" + outNum + "' and ly_inma0010.warehouse='" + nowwarehouse + "'";


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
        private void Savefinished(string  flag)
        {
            string outNum = this.ly_store_outnumDataGridView.CurrentRow.Cells["out_number"].Value.ToString();

            string delstr = " update ly_store_out set finished=" + flag + " where out_number = '" + outNum + "'";


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

        private void 打印PToolStripButton_Click(object sender, EventArgs e)
        {
            if (null == this.ly_store_outDataGridView.CurrentRow) return;

            //int parentId = int.Parse(this.ly_material_plan_mainDataGridView.CurrentRow.Cells["id"].Value.ToString());


            string nowDepartment = this.ly_store_outDataGridView.CurrentRow.Cells["DepartmentName"].Value.ToString();



            NewFrm.Show(this);
 
            BaseReportView queryForm = new BaseReportView();

            queryForm.Text = "中原精密生产领料单";

            queryForm.Printdata = this.lYStoreMange;

          
            queryForm.PrintCrystalReport = new LY_Lingliaodan_db();


            NewFrm.Hide(this);

            queryForm.ShowDialog();
        }

        private void toolStripButton7_Click_1(object sender, EventArgs e)
        {
            if (null == this.ly_plan_getmaterialDataGridView.CurrentRow) return;

            int parentId = int.Parse(this.ly_material_plan_mainDataGridView.CurrentRow.Cells["id"].Value.ToString());





            frmWaiting.Show(this);

            //this.ly_plan_getmaterialTableAdapter.Fill(this.lYPlanMange.ly_plan_getmaterial, parentId);

            BaseReportView queryForm = new BaseReportView();

            queryForm.Text = "中原精密配套欠料表";

            queryForm.Printdata = this.lYPlanMange;

            queryForm.PrintCrystalReport = new LY_PlanOwe();


            string selectFormula;

            selectFormula = "{ly_plan_getmaterial_department.仓库}  =   '"+this .comboBox2 .Text+"'  and {ly_plan_getmaterial_department.未领数量}>0 ";
            queryForm.PrintCrystalReport.DataDefinition.RecordSelectionFormula = selectFormula;

            frmWaiting.Hide(this);

            queryForm.ShowDialog();
        }

        private void ly_plan_getmaterialDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void ly_material_plan_mainDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void ly_store_outnumDataGridView_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {

        }

        //private void fillToolStripButton_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        this.ly_store_out_transferDetailTableAdapter.Fill(this.lYStoreMange.ly_store_out_transferDetail, out_numberToolStripTextBox.Text, yonghu_nameToolStripTextBox.Text);
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
        //        this.ly_store_out_transferMainTableAdapter.Fill(this.lYStoreMange.ly_store_out_transferMain, plan_numToolStripTextBox.Text, yonghu_codeToolStripTextBox.Text);
        //    }
        //    catch (System.Exception ex)
        //    {
        //        System.Windows.Forms.MessageBox.Show(ex.Message);
        //    }

        //}
    }
}
