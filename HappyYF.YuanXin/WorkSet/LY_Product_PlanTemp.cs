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
    public partial class LY_Product_PlanTemp  : Form
    {
         string formState = "View";
         string tempState = "View";
         int nowTempRow;

         public LY_Product_PlanTemp()
        {
            InitializeComponent();
        }

        private void ly_material_plan_mainBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.ly_material_plan_mainBindingSource.EndEdit();
            this.ly_material_plan_mainTableAdapter.Update(this.lYPlanMange.ly_material_plan_main);

            SetFormState("View");

            int parentId = int.Parse(this.ly_material_plan_mainDataGridView.CurrentRow.Cells["id"].Value.ToString());
            string planNum = this.ly_material_plan_mainDataGridView.CurrentRow.Cells["计划编号"].Value.ToString();

            //this.lY_dayget_material_selTableAdapter.Fill(this.lYPlanMange.LY_dayget_material_sel, parentId);
            this.ly_plan_getmaterialTableAdapter.Fill(this.lYPlanMange.ly_plan_getmaterial, planNum);

        }

        private void LY_Material_Plan_Load(object sender, EventArgs e)
        {

            NewFrm.Show(this);

            this.ly_lsptb_selTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;

            //this.ly_material_plan_mainDataGridView.SelectionChanged -= ly_material_plan_mainDataGridView_SelectionChanged;
            this.ly_lsptb_selTableAdapter.Fill(this.lYPlanMange.ly_lsptb_sel);
            //this.ly_material_plan_mainDataGridView.SelectionChanged += ly_material_plan_mainDataGridView_SelectionChanged;

            this.ly_prod_deptTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            this.ly_prod_deptTableAdapter.Fill(this.lYMaterialMange.ly_prod_dept);
            this.lyproddeptBindingSource.Sort = "prodcode DESC";

            if (this.comboBox1.Items.Count > 0)
            {
                this.comboBox1.SelectedIndex = 0;
            }

            this.ly_materialstatusTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            this.ly_materialstatusTableAdapter.Fill(this.lYMaterialMange.ly_materialstatus);

            this.ly_materrial_sortTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            this.ly_materrial_sortTableAdapter.Fill(this.lYMaterialMange.ly_materrial_sort);

            this.ly_unitsetTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            this.ly_unitsetTableAdapter.Fill(this.lYMaterialMange.ly_unitset);

            this.ly_warehouseTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            this.ly_warehouseTableAdapter.Fill(this.lYMaterialMange.ly_warehouse);

            this.lY_plantemp_material_selTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            this.lY_plantemp_material_selTableAdapter.Fill(this.lYPlanMange.LY_plantemp_material_sel);

            this.ly_lsptbTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            this.ly_lsptbTableAdapter.Fill(this.lYPlanMange.ly_lsptb);

            //this.lY_plantemp_material_selTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            //this.lY_plantemp_material_selTableAdapter.Fill(this.lYPlanMange.LY_plantemp_material_sel);

            //this.ly_plan_getmaterialTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            this.ly_plan_getmaterialTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            

            this.ly_material_plan_mainTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            this.ly_material_plan_mainTableAdapter.Fill(this.lYPlanMange.ly_material_plan_main,"LSPT");

            SetFormState("View");
            SetTempState("View");
            NewFrm.Hide(this);

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
                this.客户名称TextBox.Enabled = false;
                this.产品型号TextBox.Enabled = false;
                this.配套数量TextBox.Enabled = false;
              



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
                this.客户名称TextBox.Enabled = true ;
                this.产品型号TextBox.Enabled = true ;
                this.配套数量TextBox.Enabled = true ;
              




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

        private void SetTempState(string state)
        {
            // view edit add save cancle

            //return;

            if ("View" == state)
            {
                this.tempState = "View";

                this.toolStripButton20.Enabled = false;
                this.保存SToolStripButton.Enabled = false;

                this.toolStripButton7.Enabled = true;
                this.toolStripButton17.Enabled = true;
                this.toolStripButton19.Enabled = true;
                this.toolStripButton18.Enabled = true;
                this.toolStripButton6.Enabled = true;

                this.bindingNavigator3.Enabled = true;
                this.lY_plantemp_material_selDataGridView.Enabled = true;
                this.ly_lsptbDataGridView.ReadOnly = true;

              


            }
            else
            {
                this.tempState = "Edit";
                this.nowTempRow = ly_lsptbDataGridView.CurrentRow.Index;

                this.toolStripButton20.Enabled = true ;
                this.保存SToolStripButton.Enabled = true ;

                this.toolStripButton7.Enabled = false ;
                this.toolStripButton17.Enabled = false ;
                this.toolStripButton19.Enabled = false ;
                this.toolStripButton18.Enabled = false ;
                this.toolStripButton6.Enabled = false ;

                this.bindingNavigator3.Enabled = false ;
                this.lY_plantemp_material_selDataGridView.Enabled = false ;
                this.ly_lsptbDataGridView.ReadOnly = false;

               


            }


        }

        private string GetMaxPlanCode()
        {
            SqlConnection sqlConnection1 = new SqlConnection(SQLDatabase.Connectstring);
            SqlCommand cmd = new SqlCommand();

            string MaxPlanCode = "";

            cmd.Parameters.Add("@Plan_mode", SqlDbType.VarChar);
            cmd.Parameters["@Plan_mode"].Value = "LSPT";


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
            string message = "增加临时配套吗？";
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

            if (this.ly_plan_getmaterialDataGridView.RowCount > 0)
            {
                MessageBox.Show("已有临时配套物料记录，不能删除(实需删除，请先删除该计划的物料记录)", "注意");
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


                    this.ly_material_plan_mainTableAdapter.Fill(this.lYPlanMange.ly_material_plan_main, "LSPT");
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
            for (int i = 1; i < 8; i++)
            {
                string tempColumnName = this.ly_lsptb_selDataGridView.Columns[i].DataPropertyName;

                if (i != 7)
                    dFilter = dFilter + tempColumnName + " like  '*" + this.toolStripTextBox2.Text + "*' or ";
                else
                    dFilter = dFilter + tempColumnName + " like  '*" + this.toolStripTextBox2.Text + "*' ";

            }

            if (this.toolStripTextBox2.Text.Replace(" ", "").Length > 0)

                this.ly_lsptb_selBindingSource.Filter = dFilter;
            else
                this.ly_lsptb_selBindingSource.Filter = " ";
        }

        private void toolStripTextBox2_Enter(object sender, EventArgs e)
        {
            toolStripTextBox2.Text = "";

            this.ly_lsptb_selBindingSource.Filter = "";
        }

        private void ly_material_plan_mainDataGridView_SelectionChanged(object sender, EventArgs e)
        {
            if (this.formState == "View")
            {
                if (null != this.ly_material_plan_mainDataGridView.CurrentRow)
                {
                    int parentId = int.Parse(this.ly_material_plan_mainDataGridView.CurrentRow.Cells["id"].Value.ToString());
                    string planNum = this.ly_material_plan_mainDataGridView.CurrentRow.Cells["计划编号"].Value.ToString();

                    //this.lY_dayget_material_selTableAdapter.Fill(this.lYPlanMange.LY_dayget_material_sel, parentId);
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
            if (null == ly_lsptb_selDataGridView.CurrentRow) return;

            ly_lsptb_selDataGridView.EndEdit();





            NewFrm.Show(this); 

            string componentNum;
            decimal  nowabsqty ;
            decimal  nowtyl;
            string nowwarehouse;
            int ptbId;

            

            foreach (DataGridViewRow dgv in ly_lsptb_selDataGridView.Rows)
            {
                nowabsqty=0;
                if (decimal.TryParse(dgv.Cells["领用数"].Value.ToString(), out nowabsqty))
                {
                    if (0 < nowabsqty)
                    {
                        if (decimal.TryParse(dgv.Cells["台用量"].Value.ToString(), out nowtyl))
                        {

                        }
                        else
                        {

                            nowtyl = 0; 
                        
                        
                        }
                        componentNum = dgv.Cells["物料编号2"].Value.ToString();
                        nowwarehouse = dgv.Cells["warehouse"].Value.ToString();
                        ptbId = int.Parse(dgv.Cells["lsptbId"].Value.ToString());
                        NewFrm.Notify(this, componentNum);
                        CountPlanStru(nowabsqty, nowtyl, componentNum,nowwarehouse, ptbId);

     
                    }

                }

             
            }

            this.ly_lsptb_selTableAdapter.Fill(this.lYPlanMange.ly_lsptb_sel);
            string planNum = this.ly_material_plan_mainDataGridView.CurrentRow.Cells["计划编号"].Value.ToString();
            this.ly_plan_getmaterialTableAdapter.Fill(this.lYPlanMange.ly_plan_getmaterial, planNum);
            NewFrm.Hide(this);
        }

        private void 删除子件ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (null == this.ly_plan_getmaterialDataGridView.CurrentRow) return;


            int nowId = int.Parse(this.ly_plan_getmaterialDataGridView.CurrentRow.Cells["id1"].Value.ToString());
            int parentId = int.Parse(this.ly_material_plan_mainDataGridView.CurrentRow.Cells["id"].Value.ToString());
            string componentNum = this.ly_plan_getmaterialDataGridView.CurrentRow.Cells["物料编号"].Value.ToString();

            decimal newCheckCount;
            if (decimal.TryParse(this.ly_plan_getmaterialDataGridView.CurrentRow.Cells["已领数量"].Value.ToString(), out newCheckCount))
                {
                    if (0 < newCheckCount)
                    {
                        MessageBox.Show("已有领料记录,不能删除...", "注意");
                        return;
                    }

                }


            string message1 = "当前(物料：" + componentNum + ")将被删除，继续吗？";
            string caption1 = "提示...";
            MessageBoxButtons buttons1 = MessageBoxButtons.YesNo;

            DialogResult result1;



            result1 = MessageBox.Show(message1, caption1, buttons1,
            MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

            if (result1 == DialogResult.Yes)
            {

                string delstr = " delete ly_plan_getmaterial  where id = " + nowId + "";





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
                    this.ly_lsptb_selTableAdapter.Fill(this.lYPlanMange.ly_lsptb_sel );
                    string planNum = this.ly_material_plan_mainDataGridView.CurrentRow.Cells["计划编号"].Value.ToString();
                    this.ly_plan_getmaterialTableAdapter.Fill(this.lYPlanMange.ly_plan_getmaterial, planNum);


                    this.ly_lsptb_selBindingSource.Position = this.ly_lsptb_selBindingSource.Find("物料编号", componentNum);

                    //CountPlanStru();
                }


            }
        }

        private void SaveChanged()
        {
            ///////////////////////////

            this.ly_plan_getmaterialDataGridView.EndEdit();


            this.ly_plan_getmaterialBindingSource.EndEdit();



            this.ly_plan_getmaterialTableAdapter.Update(this.lYPlanMange.ly_plan_getmaterial);



        }


        private void ly_material_plan_detailDataGridView_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            DataGridView dgv = sender as DataGridView;


            decimal newCheckCount;
            if (decimal.TryParse(dgv.CurrentRow.Cells["已领数量"].Value.ToString(), out newCheckCount))
            {
                if (0 < newCheckCount)
                {
                    MessageBox.Show("已有领料记录,不能修改...", "注意");
                    return;
                }

            }


            if ("领料数量" == dgv.CurrentCell.OwningColumn.Name)
            {

                ChangeValue queryForm = new ChangeValue();

                queryForm.OldValue = dgv.CurrentCell.Value.ToString();
                queryForm.NewValue = "";
                queryForm.ChangeMode = "value";
                queryForm.ShowDialog();




                if (queryForm.NewValue != "")
                {
                    dgv.CurrentRow.Cells["领料数量"].Value = queryForm.NewValue;
                    //dgv.CurrentRow.Cells["discount_money"].Value = DBNull.Value;

                    //dgv.CurrentRow.Cells["approve_flag"].Value = "False";
                    SaveChanged();

                 
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



                dgv.CurrentRow.Cells["部门名称"].Value = queryForm.Result;
                dgv.CurrentRow.Cells["领料部门"].Value = queryForm.Result1;
               
                SaveChanged();



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
                //CountPlanStru(0,0);


                   



              

            }
        }

        private void CountPlanStru(decimal nowabsqty, decimal nowtyl, string componentNum,string nowwarehouse ,int ptbId)
        {
            if (null == this.ly_material_plan_mainDataGridView.CurrentRow) return;
           
           
            string planNum = this.ly_material_plan_mainDataGridView.CurrentRow.Cells["计划编号"].Value.ToString();

            
            int parentId = int.Parse(this.ly_material_plan_mainDataGridView.CurrentRow.Cells["id"].Value.ToString());
            string prod_dept = this .comboBox1.SelectedValue.ToString();
            
            //frmWaiting.Show(this);
            
            SqlConnection sqlConnection1 = new SqlConnection(SQLDatabase.Connectstring);
            SqlCommand cmd = new SqlCommand();



            cmd.Parameters.Add("@planId", SqlDbType.Int);
            cmd.Parameters["@planId"].Value = parentId;

            cmd.Parameters.Add("@prod_dept", SqlDbType.VarChar );
            cmd.Parameters["@prod_dept"].Value = prod_dept;

            cmd.Parameters.Add("@itemno", SqlDbType.VarChar );
            cmd.Parameters["@itemno"].Value = componentNum;

            cmd.Parameters.Add("@absqty", SqlDbType.Decimal);
            cmd.Parameters["@absqty"].Value = nowabsqty;

            cmd.Parameters.Add("@tyl", SqlDbType.Decimal);
            cmd.Parameters["@tyl"].Value = nowtyl;

            cmd.Parameters.Add("@nowwarehouse", SqlDbType.VarChar );
            cmd.Parameters["@nowwarehouse"].Value = nowwarehouse;

            cmd.Parameters.Add("@ptbId", SqlDbType.Int);
            cmd.Parameters["@ptbId"].Value = ptbId;




            




            cmd.CommandText = "LY_TempPlan_input";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = sqlConnection1;
            cmd.CommandTimeout = 0;

            sqlConnection1.Open();
            cmd.ExecuteNonQuery();
            sqlConnection1.Close();



            //this.ly_lsptb_selTableAdapter.Fill(this.lYPlanMange.ly_lsptb_sel );
            //this.ly_plan_getmaterialTableAdapter.Fill(this.lYPlanMange.ly_plan_getmaterial,planNum );

            //frmWaiting.Hide(this);
        }

        private void CountTempPlanStru()
        {
           // if (null == this.ly_material_plan_mainDataGridView.CurrentRow) return;


            //string planNum = this.ly_material_plan_mainDataGridView.CurrentRow.Cells["计划编号"].Value.ToString();

            string componentNum = this.lY_plantemp_material_selDataGridView.CurrentRow.Cells["编号"].Value.ToString();
            //int parentId = int.Parse(this.ly_material_plan_mainDataGridView.CurrentRow.Cells["id"].Value.ToString());
            //string prod_dept = "";

            NewFrm.Show(this); 


            SqlConnection sqlConnection1 = new SqlConnection(SQLDatabase.Connectstring);
            SqlCommand cmd = new SqlCommand();



            //cmd.Parameters.Add("@planId", SqlDbType.Int);
            //cmd.Parameters["@planId"].Value = parentId;

            //cmd.Parameters.Add("@prod_dept", SqlDbType.VarChar);
            //cmd.Parameters["@prod_dept"].Value = prod_dept;

            //cmd.Parameters.Add("@itemno", SqlDbType.VarChar);
            //cmd.Parameters["@itemno"].Value = componentNum;


            cmd.CommandText = " INSERT INTO ly_lsptb ([wzbh],[mch],[jph],[xhc],[gg],[dw],[sort],[status],[warehouse]) " +
                              " select [wzbh],[mch],[jph],[xhc],[gg],[dw],[sort1],[status],[warehouse] from ly_inma0010 where wzbh='" + componentNum + "'";
            cmd.CommandType = CommandType.Text ;
            cmd.Connection = sqlConnection1;

            sqlConnection1.Open();
            cmd.ExecuteNonQuery();
            sqlConnection1.Close();



           
            this.lY_plantemp_material_selTableAdapter.Fill(this.lYPlanMange.LY_plantemp_material_sel);
            
            this.ly_lsptbTableAdapter.Fill(this.lYPlanMange.ly_lsptb);

            this.ly_lsptbBindingSource.Position = this.ly_lsptbBindingSource.Find("物料编号", componentNum);

            NewFrm.Hide(this);
        }

       

        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            //if (null == this.ly_store_planitemcountDataGridView1.CurrentRow) return;



            ////string balanceFlag = this.xD_Sel_SellBalanceDataGridView.CurrentRow.Cells["结算"].Value.ToString();



            //frmWaiting.Show(this);


            //BaseReportView queryForm = new BaseReportView();

            //queryForm.Text = "中原精密生产计划欠料表";

            //queryForm.Printdata = this.lYPlanMange;

            //queryForm.PrintCrystalReport = new LY_ProductReport_Owe();

            ////string nowCardNumber = this.xD_Sel_SellBalanceDataGridView.CurrentRow.Cells["编号"].Value.ToString();

            ////string selectFormula;
            ////selectFormula = "{XD_Sel_SellBalance.编号}   =   '" + nowCardNumber + "'";
            ////queryForm.CrystalReportViewer1.SelectionFormula = selectFormula;

            ////if (this.radioButton2.Checked)
            ////{
            ////    string selectFormula;
            ////    selectFormula = "{XD_Sel_SellBalance.Card_number}   =   '" + nowCardNumber + "'";
            ////    queryForm.PrintCrystalReport.DataDefinition.RecordSelectionFormula = selectFormula;
            ////}

            //string selectFormula;
            ////selectFormula = "{ly_store_planitemcount.status}  =   '原料' and {ly_store_planitemcount.owemoney} >0 ";
            //selectFormula = "{ly_store_planitemcount.状态}  =   '原料'  and {ly_store_planitemcount.欠料金额}>0 ";
            //queryForm.PrintCrystalReport.DataDefinition.RecordSelectionFormula = selectFormula;

            ////((CrystalDecisions.CrystalReports.Engine.TextObject)(queryForm.PrintCrystalReport.DataDefinition.GroupNameFields.GroupHeaderSection1.ReportObjects["Text24"])).Text = "潼关中金冶炼有限责任公司原料结算单";


            ////queryForm.PrintCrystalReport = new XD_SellBalance_All();

            ////string nowCardNumber = this.xD_Sel_SellBalanceDataGridView.CurrentRow.Cells["编号"].Value.ToString();

            //frmWaiting.Hide(this);

            //queryForm.ShowDialog();
        }

        private void toolStripButton7_Click(object sender, EventArgs e)
        {
            if (null == this.ly_plan_getmaterialDataGridView.CurrentRow) return;

            int parentId = int.Parse(this.ly_material_plan_mainDataGridView.CurrentRow.Cells["id"].Value.ToString());





            NewFrm.Show(this); 


            //this.ly_plan_getmaterialTableAdapter.Fill(this.lYPlanMange.ly_plan_getmaterial, parentId);

            BaseReportView queryForm = new BaseReportView();

            queryForm.Text = "中原精密临时配套表";

            queryForm.Printdata = this.lYPlanMange;

            queryForm.PrintCrystalReport = new LY_PlanTemp();


            //string selectFormula;

            //selectFormula = "{ly_store_planitemcount.状态}  =   '原料'  and {ly_store_planitemcount.欠料金额}>0 ";
            //queryForm.PrintCrystalReport.DataDefinition.RecordSelectionFormula = selectFormula;

              NewFrm.Hide(this);

            queryForm.ShowDialog();
        }

        //private void toolStripButton8_Click(object sender, EventArgs e)
        //{
        //    ExportDataGridviewTOExcell.ExportDataGridview(this.ly_material_plan_explodeDataGridView, true);
        //}

        private void toolStripButton9_Click(object sender, EventArgs e)
        {
            ExportDataGridviewTOExcell.ExportDataGridview(this.ly_plan_getmaterialDataGridView, true);
        }

        private void 保存SToolStripButton_Click(object sender, EventArgs e)
        {
            this.ly_lsptbDataGridView.EndEdit();
            this.ly_lsptbBindingSource.EndEdit();
            this.ly_lsptbTableAdapter.Update(this.lYPlanMange.ly_lsptb);

            this.SetTempState("View");
        }

        private void toolStripButton6_Click_1(object sender, EventArgs e)
        {
           // this.lY_plantemp_material_selTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            this.lY_plantemp_material_selTableAdapter.Fill(this.lYPlanMange.LY_plantemp_material_sel);
            //this.ly_lsptbTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            this.ly_lsptbTableAdapter.Fill(this.lYPlanMange.ly_lsptb);
        }

        private void toolStripTextBox3_KeyUp(object sender, KeyEventArgs e)
        {
            string dFilter = "";

            //for (int i = 0; i < this.hT_Vehicle_collectDataGridView.ColumnCount; i++)
            for (int i = 1; i < 10; i++)
            {
                string tempColumnName = this.lY_plantemp_material_selDataGridView.Columns[i].DataPropertyName;

                if (i != 9)
                    dFilter = dFilter + tempColumnName + " like  '*" + this.toolStripTextBox3.Text + "*' or ";
                else
                    dFilter = dFilter + tempColumnName + " like  '*" + this.toolStripTextBox3.Text + "*' ";

            }

            if (this.toolStripTextBox3.Text.Replace(" ", "").Length > 0)

                this.lY_plantemp_material_selBindingSource.Filter = dFilter;
            else
                this.lY_plantemp_material_selBindingSource.Filter = " ";
        }

        private void toolStripTextBox3_Enter(object sender, EventArgs e)
        {
            toolStripTextBox3.Text = "";

            this.lY_plantemp_material_selBindingSource.Filter = "";
        }

        private void ly_lsptbDataGridView_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.Cancel = false;
        }

        private void lY_plantemp_material_selDataGridView_DoubleClick(object sender, EventArgs e)
        {
            
            if (null == lY_plantemp_material_selDataGridView.CurrentRow) return;

            //string componentNum = this.lY_dayget_material_selDataGridView.CurrentRow.Cells["物资编号"].Value.ToString();
            //int parentId = int.Parse(this.ly_material_plan_mainDataGridView.CurrentRow.Cells["id"].Value.ToString());
            
            
            CountTempPlanStru();
        }

        private void toolStripButton17_Click(object sender, EventArgs e)
        {
            string message = "增加临时配套物料吗？";
            string caption = "提示...";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;

            DialogResult result;



            result = MessageBox.Show(message, caption, buttons,
            MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

            if (result == DialogResult.Yes)
            {
                this.ly_lsptbBindingSource.AddNew();
                //this.ly_material_plan_mainDataGridView.CurrentRow.Cells["计划编号"].Value = GetMaxPlanCode();
                //this.ly_material_plan_mainDataGridView.CurrentRow.Cells["制定日期"].Value =DateTime .Now ;
                //this.ly_material_plan_mainDataGridView.CurrentRow.Cells["制定人"].Value = SQLDatabase.nowUserName();
                //this.ly_material_plan_mainBindingSource.EndEdit();

                //this.Validate();
                //this.ly_material_plan_mainBindingSource.EndEdit();



                //    this.ly_material_plan_mainTableAdapter.Update(this.lYPlanMange.ly_material_plan_main);



                SetTempState("Edit");
                //this.制定日期DateTimePicker.Focus();
            }
        }

        private void toolStripButton20_Click(object sender, EventArgs e)
        {
            this.SetTempState("View");
            this.ly_lsptbTableAdapter.Fill(this.lYPlanMange.ly_lsptb);
            
        }

        private void toolStripButton19_Click(object sender, EventArgs e)
        {
            if (null == this.ly_lsptbDataGridView.CurrentRow) return;

            //if (this.ly_material_plan_detailDataGridView.RowCount > 0)
            //{
            //    MessageBox.Show("计划已有物料记录，不能删除(实需删除，请先删除该计划的物料记录)", "注意");
            //    return;

            //}

            string nowItemNumber = this.ly_lsptbDataGridView.CurrentRow.Cells["物料编号1"].Value.ToString();


            string message1 = "当前(物料：" + nowItemNumber + ")将被删除，继续吗？";
            string caption1 = "提示...";
            MessageBoxButtons buttons1 = MessageBoxButtons.YesNo;

            DialogResult result1;



            result1 = MessageBox.Show(message1, caption1, buttons1,
            MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

            if (result1 == DialogResult.Yes)
            {

                this.ly_lsptbBindingSource.RemoveCurrent();

                this.ly_lsptbDataGridView.EndEdit();
                this.ly_lsptbBindingSource.EndEdit();
                this.ly_lsptbTableAdapter.Update(this.lYPlanMange.ly_lsptb);


                this.lY_plantemp_material_selTableAdapter.Fill(this.lYPlanMange.LY_plantemp_material_sel);

                this.lY_plantemp_material_selBindingSource.Position = this.lY_plantemp_material_selBindingSource.Find("编号", nowItemNumber);


            }
        }

        private void toolStripButton18_Click(object sender, EventArgs e)
        {
            if (null == this.ly_lsptbDataGridView.CurrentRow) return;
            
            SetTempState("Edit");
        }

        private void ly_lsptbDataGridView_SelectionChanged(object sender, EventArgs e)
        {
            if (null == ly_lsptbDataGridView.CurrentRow) return;

            if (this.tempState == "View")
            {

            }
            else
            {
                this.ly_lsptbBindingSource.Position = this.nowTempRow;

                //this.hT_Insurance_ItemDataGridView.CurrentCell = this.hT_Insurance_ItemDataGridView.CurrentRow.Cells[this.hT_Insurance_ItemDataGridView.CurrentCell.ColumnIndex];
            }
        }

        private void toolStripButton7_Click_1(object sender, EventArgs e)
        {
             string message1 = "新物料增加到原料表,继续吗?";
            string caption1 = "提示...";
            MessageBoxButtons buttons1 = MessageBoxButtons.YesNo;

            DialogResult result1;



            result1 = MessageBox.Show(message1, caption1, buttons1,
            MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

            if (result1 == DialogResult.Yes)
            {

                AddTempPlanToMaterial();

            }
        }

        private void AddTempPlanToMaterial()
        {
            //if (null == this.ly_material_plan_mainDataGridView.CurrentRow) return;


            //string planNum = this.ly_material_plan_mainDataGridView.CurrentRow.Cells["计划编号"].Value.ToString();

            //string componentNum = this.lY_dayget_material_selDataGridView.CurrentRow.Cells["物资编号"].Value.ToString();
            //int parentId = int.Parse(this.ly_material_plan_mainDataGridView.CurrentRow.Cells["id"].Value.ToString());
            //string prod_dept = "";

            NewFrm.Show(this); 


            SqlConnection sqlConnection1 = new SqlConnection(SQLDatabase.Connectstring);
            SqlCommand cmd = new SqlCommand();



            //cmd.Parameters.Add("@planId", SqlDbType.Int);
            //cmd.Parameters["@planId"].Value = parentId;

            //cmd.Parameters.Add("@prod_dept", SqlDbType.VarChar);
            //cmd.Parameters["@prod_dept"].Value = prod_dept;

            //cmd.Parameters.Add("@itemno", SqlDbType.VarChar);
            //cmd.Parameters["@itemno"].Value = componentNum;


            cmd.CommandText = "LY_Add_TempPlan_ToMaterial";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = sqlConnection1;

            sqlConnection1.Open();
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (SqlException sqle)
            {

                //MessageBox.Show(sqle.Message);
                MessageBox.Show("临时配套表中有重复的物料编号,请检查,操作取消...");
            }
            finally
            {
                sqlConnection1.Close();
            }



            this.lY_plantemp_material_selTableAdapter.Fill(this.lYPlanMange.LY_plantemp_material_sel);

             NewFrm.Hide(this);
        }

        private void ly_lsptb_selDataGridView_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (true == ly_lsptb_selDataGridView.ReadOnly) return;
            if ("领用数" == ly_lsptb_selDataGridView.CurrentCell.OwningColumn.DataPropertyName)
            {
                //SetlastColumn();
                ly_lsptb_selDataGridView.CurrentCell.Style.BackColor = Color.White;
                ly_lsptb_selDataGridView.CurrentCell.Style.ForeColor = Color.Black;


            }
        }

        private void ly_lsptb_selDataGridView_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
            if (true == ly_lsptb_selDataGridView.ReadOnly) return;
            if ("领用数" == ly_lsptb_selDataGridView.CurrentCell.OwningColumn.DataPropertyName)
            {





                ly_lsptb_selDataGridView.CurrentCell.Style.BackColor = Color.HotPink ;
                ly_lsptb_selDataGridView.CurrentCell.Style.ForeColor = Color.Black;
            }
        }

        

      
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
