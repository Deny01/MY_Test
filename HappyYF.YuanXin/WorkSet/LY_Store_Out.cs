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
    public partial class LY_Store_Out  : Form
    {
         string formState = "View";

         public LY_Store_Out()
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



            this.ly_store_planoutTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            //this.ly_plan_parentlistTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;


            this.ly_plan_deptlistTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            this.ly_plan_parentlistTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;

            this.ly_store_outnumTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            this.ly_store_outTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            
            this.ly_plan_getmaterialTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            
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

       
        private void toolStripTextBox2_KeyUp(object sender, KeyEventArgs e)
        {
            string dFilter = "";

            //for (int i = 0; i < this.hT_Vehicle_collectDataGridView.ColumnCount; i++)
            for (int i = 1; i < 10; i++)
            {
                string tempColumnName = this.ly_store_planoutDataGridView.Columns[i].DataPropertyName;

                if (i != 9)
                    dFilter = dFilter + tempColumnName + " like  '*" + this.toolStripTextBox2.Text + "*' or ";
                else
                    dFilter = dFilter + tempColumnName + " like  '*" + this.toolStripTextBox2.Text + "*' ";

            }

            if (this.toolStripTextBox2.Text.Replace(" ", "").Length > 0)

                this.ly_store_planoutBindingSource.Filter = dFilter;
            else
                this.ly_store_planoutBindingSource.Filter = " ";
        }

        private void toolStripTextBox2_Enter(object sender, EventArgs e)
        {
            toolStripTextBox2.Text = "";

            this.ly_store_planoutBindingSource.Filter = "";
        }

        private void ly_material_plan_mainDataGridView_SelectionChanged(object sender, EventArgs e)
        {
            if (this.formState == "View")
            {
                if (null != this.ly_material_plan_mainDataGridView.CurrentRow)
                {
                    int parentId = int.Parse(this.ly_material_plan_mainDataGridView.CurrentRow.Cells["id"].Value.ToString());
                    string planNum = this.ly_material_plan_mainDataGridView.CurrentRow.Cells["计划编号"].Value.ToString();



                    this.ly_plan_getmaterialTableAdapter.Fill(this.lYPlanMange.ly_plan_getmaterial, planNum);
                    this.ly_store_planoutTableAdapter.Fill(this.lYStoreMange.ly_store_planout, planNum);
                    this.ly_store_outnumTableAdapter.Fill(this.lYStoreMange.ly_store_outnum, planNum, SQLDatabase.NowUserID);

                    this.ly_plan_parentlistTableAdapter.Fill(this.lYStoreMange.ly_plan_parentlist, "asd", "asd");
                    this.ly_plan_deptlistTableAdapter.Fill(this.lYStoreMange.ly_plan_deptlist, "asd");

                    this.ly_plan_deptlistTableAdapter.Fill(this.lYStoreMange.ly_plan_deptlist, planNum);

                    if ( this.comboBox1.Items .Count >0)
                    {
                        this.comboBox1.SelectedIndex = 0;
                        this.ly_plan_parentlistTableAdapter.Fill(this.lYStoreMange.ly_plan_parentlist, planNum, this .comboBox1 .SelectedValue .ToString ());
                    }
                   
                    //this.ly_plan_parentlistTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;

                    this.ly_plan_getmaterialBindingSource.Filter = "领料部门='asd'";

                    this.groupBox3.Text = planNum +":物料列表";

                   
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
                MessageBox.Show("已经完成,不能删除...");

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
                this.ly_store_outBindingSource.RemoveCurrent();
                SaveChanged();

            }
        }

        private void SaveChanged()
        {
            ///////////////////////////

            this.ly_store_outDataGridView.EndEdit();


            this.ly_store_outBindingSource.EndEdit();



            this.ly_store_outTableAdapter.Update(this.lYStoreMange .ly_store_out );

            string planNum = this.ly_material_plan_mainDataGridView.CurrentRow.Cells["计划编号"].Value.ToString();

            this.ly_plan_getmaterialTableAdapter.Fill(this.lYPlanMange.ly_plan_getmaterial, planNum);



        }


        private void ly_material_plan_detailDataGridView_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            
            DataGridView dgv = sender as DataGridView;

            if ("True" == ly_store_outnumDataGridView.CurrentRow.Cells["finished"].Value.ToString())
            {
                MessageBox.Show("已经完成领料,不能修改...");

                return;

            }


            if ("领料数量1" == dgv.CurrentCell.OwningColumn.Name)
            {

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
            
                
                    //int parentId = int.Parse(this.ly_material_plan_mainDataGridView.CurrentRow.Cells["id"].Value.ToString());
            string outnumber = this.ly_store_outnumDataGridView.CurrentRow.Cells["out_number"].Value.ToString();

            if ("True" == ly_store_outnumDataGridView.CurrentRow.Cells["finished"].Value.ToString())
            {
                MessageBox.Show("已经完成,不能删除...");

                return;
            
            }
                
           

            //////////////////////////////////

            string message = "删除当前流水号:" + outnumber + "吗？";
            string caption = "提示...";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;

            DialogResult result;



            result = MessageBox.Show(message, caption, buttons,
            MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

            if (result == DialogResult.Yes)
            {

                string delstr = " delete ly_store_out  where out_number = '" + outnumber + "'";


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



              

            }
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

        private void CountStoreOutAuto()
        {
            if (null == this.ly_material_plan_mainDataGridView.CurrentRow) return;
           
           
            string planNum = this.ly_material_plan_mainDataGridView.CurrentRow.Cells["计划编号"].Value.ToString();

            //string componentNum = this.lY_dayget_material_selDataGridView.CurrentRow.Cells["物资编号"].Value.ToString();
            int parentId = int.Parse(this.ly_material_plan_mainDataGridView.CurrentRow.Cells["id"].Value.ToString());
           
            
            frmWaiting.Show(this);
            
            SqlConnection sqlConnection1 = new SqlConnection(SQLDatabase.Connectstring);
            SqlCommand cmd = new SqlCommand();



            cmd.Parameters.Add("@plan_num", SqlDbType.VarChar );
            cmd.Parameters["@plan_num"].Value = planNum;

            cmd.Parameters.Add("@prod_dept", SqlDbType.VarChar );
            cmd.Parameters["@prod_dept"].Value = comboBox1 .SelectedValue ;

            cmd.Parameters.Add("@parentno", SqlDbType.VarChar);
            cmd.Parameters["@parentno"].Value = comboBox2.SelectedValue;

            string outNum = GetMaxOutNum();
            cmd.Parameters.Add("@out_number", SqlDbType.VarChar);
            cmd.Parameters["@out_number"].Value = outNum;

            cmd.Parameters.Add("@faliaoren", SqlDbType.VarChar);
            cmd.Parameters["@faliaoren"].Value = SQLDatabase.nowUserName();

                      
               
                
               
                
                //,@ varchar(20))  

            cmd.CommandText = "LY_store_out_input";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = sqlConnection1;

            sqlConnection1.Open();
            cmd.ExecuteNonQuery();
            sqlConnection1.Close();



            this.ly_store_outTableAdapter.Fill(this.lYStoreMange.ly_store_out, outNum, SQLDatabase.nowUserName());
            this.ly_plan_getmaterialTableAdapter.Fill(this.lYPlanMange.ly_plan_getmaterial, planNum);
            this.ly_store_outnumTableAdapter.Fill(this.lYStoreMange.ly_store_outnum, planNum, SQLDatabase.NowUserID);

            frmWaiting.Hide(this);
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

                this.ly_plan_getmaterialDataGridView.Columns["父件"].Visible = true;
                this.ly_plan_getmaterialDataGridView.Columns["父件名称"].Visible = true;
                this.ly_plan_getmaterialDataGridView.Columns["生产数量"].Visible = true;

                //this.comboBox1.Visible = false;
                this.label2.Visible = true;
                this.comboBox2.Visible = true ;

                this.ly_material_plan_mainTableAdapter.Fill(this.lYPlanMange.ly_material_plan_main, "SCJH");
            }
            else if (tsc.SelectedIndex == 1)
            {
                this.ly_plan_getmaterialDataGridView.Columns["父件"].Visible = false;
                this.ly_plan_getmaterialDataGridView.Columns["父件名称"].Visible = false;
                this.ly_plan_getmaterialDataGridView.Columns["生产数量"].Visible = false;

                //this.comboBox1.Visible = false;
                this.label2.Visible = false ;
                this.comboBox2.Visible = false;

                this.ly_material_plan_mainTableAdapter.Fill(this.lYPlanMange.ly_material_plan_main, "LLJH");
            }
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
                if (this.toolStripComboBox1.SelectedIndex == 0)
                {
                    this.ly_plan_parentlistTableAdapter.Fill(this.lYStoreMange.ly_plan_parentlist, planNum, this.comboBox1.SelectedValue.ToString());

                    this.ly_plan_getmaterialBindingSource.Filter = "领料部门='asd'";
                }

                if (this.toolStripComboBox1.SelectedIndex == 1)
                {
                    this.ly_plan_getmaterialBindingSource.Filter = "领料部门='" + this.comboBox1.SelectedValue.ToString() + "'";
                }
            }

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (null != this.comboBox1.SelectedValue)
            {
                if (this.toolStripComboBox1.SelectedIndex == 0)
                {
                    if (null != this.comboBox2.SelectedValue)
                    {
                        this.ly_plan_getmaterialBindingSource.Filter = "领料部门='" + this.comboBox1.SelectedValue.ToString() + "' and 父件='" + this.comboBox2.SelectedValue.ToString() + "'";
                    }
                }
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton rdb = sender as RadioButton;
            if (rdb.Checked)
            {
                SetFormState("View");
                this.ly_store_outTableAdapter.Fill(this.lYStoreMange.ly_store_out, "asd", SQLDatabase.nowUserName());
                //this.ly_plan_getmaterialTableAdapter.Fill(this.lYPlanMange.ly_plan_getmaterial, "asd");
                this.ly_plan_getmaterialBindingSource.Filter = "领料部门='asd'";
            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
             RadioButton rdb = sender as RadioButton;
             if (rdb.Checked)
             {
                 SetFormState("Edit");
                 this.ly_store_outTableAdapter.Fill(this.lYStoreMange.ly_store_out, "asd", SQLDatabase.nowUserName());
             }
        }

        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            if (null == this.ly_material_plan_mainDataGridView.CurrentRow) return;
            if (null == ly_plan_getmaterialDataGridView.CurrentRow) return;

            CountStoreOutAuto();

           
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

                    string deptcode =this.ly_store_outnumDataGridView.CurrentRow.Cells["out_deptcode"].Value.ToString();
                    string parentno = this.ly_store_outnumDataGridView.CurrentRow.Cells["parentitemno"].Value.ToString();

                    if (!string.IsNullOrEmpty(deptcode))
                    {
                        if (this.toolStripComboBox1.SelectedIndex == 0)
                        {
                            if (!string.IsNullOrEmpty(parentno))
                            {
                                this.ly_plan_getmaterialBindingSource.Filter = "领料部门='" + deptcode + "' and 父件='" + parentno + "'";
                                this.comboBox1.SelectedValue = deptcode;
                                this.comboBox2.SelectedValue = parentno;
                            }
                        }
                    }
                    if (this.toolStripComboBox1.SelectedIndex == 1)
                    {
                        this.ly_plan_getmaterialBindingSource.Filter = "";
                    }

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
                ly_store_planoutBindingSource.Filter = "物料编号='" + nowitem+"'";

            ly_store_outDataGridView.SelectionChanged -= ly_store_outDataGridView_SelectionChanged;

            this.ly_store_outBindingSource.Position = this.ly_store_outBindingSource.Find("物料编号", nowitem);
            
            ly_store_outDataGridView.SelectionChanged += ly_store_outDataGridView_SelectionChanged;
        }

        private void ly_store_outDataGridView_SelectionChanged(object sender, EventArgs e)
        {
            if (null == this.ly_store_outDataGridView.CurrentRow) return;
            if (null == this.ly_plan_getmaterialDataGridView.CurrentRow) return;

            DataGridView dgv = sender as DataGridView;

            string nowitem = dgv.CurrentRow.Cells["物料编号1"].Value.ToString();

            ly_plan_getmaterialDataGridView.SelectionChanged -= ly_plan_getmaterialDataGridView_SelectionChanged;
            this.ly_plan_getmaterialBindingSource.Position = this.ly_plan_getmaterialBindingSource.Find("物料编号", nowitem);
            ly_plan_getmaterialDataGridView.SelectionChanged += ly_plan_getmaterialDataGridView_SelectionChanged;
        }

        private void ly_store_outnumDataGridView_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            DataGridView dgv = sender as DataGridView;



            if ("finished" == dgv.CurrentCell.OwningColumn.Name)
            {



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





            frmWaiting.Show(this);

            //this.ly_plan_getmaterialTableAdapter.Fill(this.lYPlanMange.ly_plan_getmaterial, parentId);

            BaseReportView queryForm = new BaseReportView();

            queryForm.Text = "中原精密生产领料单";

            queryForm.Printdata = this.lYStoreMange;

            queryForm.PrintCrystalReport = new LY_Lingliaodan();


            //string selectFormula;

            //selectFormula = "{ly_store_planitemcount.状态}  =   '原料'  and {ly_store_planitemcount.欠料金额}>0 ";
            //queryForm.PrintCrystalReport.DataDefinition.RecordSelectionFormula = selectFormula;

            frmWaiting.Hide(this);

            queryForm.ShowDialog();
        }

        //private void fillToolStripButton_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        this.ly_store_outnumTableAdapter.Fill(this.lYStoreMange.ly_store_outnum, plan_numToolStripTextBox.Text);
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
        //        this.ly_store_planoutTableAdapter.Fill(this.lYStoreMange.ly_store_planout, plan_numToolStripTextBox.Text);
        //    }
        //    catch (System.Exception ex)
        //    {
        //        System.Windows.Forms.MessageBox.Show(ex.Message);
        //    }

        //}

      

       


      
    }
}
