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
    public partial class LY_Salescontract_Daily_ZC : Form
    {
        private string nowfilterStr = "";
        private string nowusercode = "";
       

        private string nowclientCode = "";
        private string nowinnerCode = "";
        private string nowcontractCode = "";

        private string nowborrowcode = "";

        private string nowfillstragecode = "";

        private string contractCanchenged = "";
        private string borrowCanchenged = "";


        private string isborrow = "";

        private int selectionIdx = 0;

        public LY_Salescontract_Daily_ZC()
        {
            InitializeComponent();
        }

        private void Yonghu_Load(object sender, EventArgs e)
        {
          
            
            this.ly_sales_outbindTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;

            this.ly_sales_standard_Report_zhongchengTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;

            this.ly_sales_contract_main_forbusinessZCTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            //this.ly_sales_contract_main_forbusinessBindingSource.Filter = " 提交=1 ";
           
            this.ly_sales_contract_terms_forcontractTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            this.ly_sales_contract_detailTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;

           
           
            

            this.nowusercode = SQLDatabase.NowUserID;
            
           
          
            

            this.dateTimePicker1.Text = DateTime.Today.AddMonths(-6).Date.ToString();
            this.dateTimePicker2.Text = DateTime.Today.AddDays(0).Date.ToString();

            this.dateTimePicker3.Text = DateTime.Today.AddMonths(-1).Date.ToString();
            this.dateTimePicker4.Text = DateTime.Today.AddDays(0).Date.ToString();

            this.ly_sales_contract_main_forbusinessZCTableAdapter.Fill(this.lYSalseMange.ly_sales_contract_main_forbusinessZC, this.dateTimePicker1.Value, this.dateTimePicker2.Value.AddDays(1));


         

        }

      

      
       



        private void ly_sales_contract_detailDataGridView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            DataGridView dgv = sender as DataGridView;
            if (e.RowIndex > -1)
            {
                if ((dgv.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.GetType() == typeof(decimal)
                      || dgv.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.GetType() == typeof(double)
                      || dgv.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.GetType() == typeof(int)))
                {
                    if (Convert.ToDecimal(dgv.Rows[e.RowIndex].Cells[e.ColumnIndex].Value) == 0)
                    {
                        e.Value = "";
                    }
                }
            }       
        }

        protected bool IsInteger(object o)
        {
            if (o is Int64)
                return true;
            if (o is Int32)
                return true;
            if (o is Int16)
                return true;
            return false;
        }
        protected bool IsDecimal(object o)
        {
            if (o is Decimal)
                return true;
            if (o is Single)
                return true;
            if (o is Double)
                return true;
            return false;
        }

        private void AddSummationRow_New1(BindingSource bs, DataGridView dgv)
        {

            return;
            if (null == dgv.CurrentRow) return;
            
            DataRow sumdr = (((DataSet)bs.DataSource).Tables[bs.DataMember]).NewRow();

            if (-1 != bs.Find("客户名称", "合计"))
            {
                bs.RemoveAt(bs.Find("客户名称", "合计"));
            }

            foreach (DataGridViewRow dgvRow in dgv.Rows)
            {
                foreach (DataGridViewCell dgvCell in dgvRow.Cells)
                {
                    //foreach (DataGridViewColumn dgvColumn in dgv.Columns)
                    //{
                    if (dgvCell.Value != null && !(dgvCell.Value is DBNull))
                    {
                        if (IsInteger(dgvCell.Value))
                        {
                            if ("合同天数" != dgvCell.OwningColumn.HeaderText && "到期天数" != dgvCell.OwningColumn.HeaderText && "折扣利率" != dgvCell.OwningColumn.HeaderText )
                            {
                                if (sumdr[dgvCell.OwningColumn.DataPropertyName] == null || (sumdr[dgvCell.OwningColumn.DataPropertyName] is DBNull))
                                    sumdr[dgvCell.OwningColumn.DataPropertyName] = 0;


                                sumdr[dgvCell.OwningColumn.DataPropertyName] = Convert.ToInt64(sumdr[dgvCell.OwningColumn.DataPropertyName]) + Convert.ToInt64(dgvCell.Value);
                            }
                        }
                        else if (IsDecimal(dgvCell.Value))
                        {
                            if ("合同天数" != dgvCell.OwningColumn.HeaderText && "到期天数" != dgvCell.OwningColumn.HeaderText && "折扣利率" != dgvCell.OwningColumn.HeaderText )
                            {
                                if (sumdr[dgvCell.OwningColumn.DataPropertyName] == null || (sumdr[dgvCell.OwningColumn.DataPropertyName] is DBNull))
                                    sumdr[dgvCell.OwningColumn.DataPropertyName] = 0;
                                //dgvCell .OwningColumn.Name  dgvCell.ColumnIndex

                                sumdr[dgvCell.OwningColumn.DataPropertyName] = Convert.ToDecimal(sumdr[dgvCell.OwningColumn.DataPropertyName]) + Convert.ToDecimal(dgvCell.Value);
                            }
                        }

                        //sumBox.Text = string.Format("{0}", sumBox.Tag);
                        //sumBox.Invalidate();

                    }
                    //}
                }

            }
            sumdr["客户名称"] = "合计";
            sumdr["id"] = 0;
            ((DataSet)bs.DataSource).Tables[bs.DataMember].Rows.Add(sumdr);
            bs.ResetBindings(true);

        }

        private void toolStripButton27_Click(object sender, EventArgs e)
        {

        }

        private void toolStripTextBox5_KeyUp(object sender, KeyEventArgs e)
        {
            //string filterString;


            //filterString = GetDataGridviewMultiFilterString.DGVMultiFilterString(this.ly_sales_businessDataGridView, this.toolStripTextBox5.Text);


            //this.ly_sales_businessBindingSource.Filter = filterString;
        }

        private void toolStripTextBox5_Enter(object sender, EventArgs e)
        {
            //toolStripTextBox5.Text = "";

            //this.ly_sales_businessBindingSource.Filter = "";
        }

        private void toolStripButton16_Click(object sender, EventArgs e)
        {
            //ExportDataGridviewTOExcell.ExportDataGridview(this.ly_sales_businessDataGridView, true);
        }

        private void bindingNavigatorAddNewItem_Click(object sender, EventArgs e)
        {
           // if (null == ly_sales_businessDataGridView.CurrentRow)
           // {

           //     return;
           // }

           //// this.nowinnerCode;

           // LY_Salescontract_GroupAdd queryForm = new LY_Salescontract_GroupAdd();

           // queryForm.contractinner_code = nowcontractCode;
           // queryForm.nowcontractCode = nowcontractCode;
           // queryForm.runmode = "增加";


           // queryForm.StartPosition = FormStartPosition.CenterParent;
           // queryForm.ShowDialog();

           // if (queryForm.DialogResult != DialogResult.Cancel)
           // {
           //     this.ly_material_plan_mainTableAdapter.Fill(this.lYSalseMange.ly_material_plan_main, "LSPT", nowcontractCode);



           //    this.ly_material_plan_mainBindingSource.Position = this.ly_material_plan_mainBindingSource.Find("内部编码", queryForm.now_plannum );

                
           // }
        }

      


        private void ly_sales_contract_mainDataGridView_SelectionChanged(object sender, EventArgs e)
        {
            if (null == ly_sales_contract_mainDataGridView.CurrentRow)
            {
                //this.ly_sales_contract_terms_forcontractTableAdapter.Fill(this.lYSalseMange.ly_sales_contract_terms_forcontract, "-aqwaaa");
                //this.ly_sales_contract_detailTableAdapter.Fill(this.lYSalseMange.ly_sales_contract_detail, "", 0);

                //this.toolStripButton44.Visible = false;

                return;
            }

            this.nowinnerCode = ly_sales_contract_mainDataGridView.CurrentRow.Cells["内部编码"].Value.ToString();
            this.nowclientCode = ly_sales_contract_mainDataGridView.CurrentRow.Cells["客户编码c"].Value.ToString();

            this.ly_sales_contract_terms_forcontractTableAdapter.Fill(this.lYSalseMange.ly_sales_contract_terms_forcontract, nowinnerCode);
            this.ly_sales_contract_detailTableAdapter.Fill(this.lYSalseMange.ly_sales_contract_detail, nowinnerCode, 0);

            this.ly_sales_outbindTableAdapter.Fill(this.lYSalseMange.ly_sales_outbind, nowinnerCode);

            if ("True" == ly_sales_contract_mainDataGridView.CurrentRow.Cells["提交"].Value.ToString() ||
                "True" == ly_sales_contract_mainDataGridView.CurrentRow.Cells["批准"].Value.ToString())
            {
                this.contractCanchenged = "False";
            }
            else
            {
                this.contractCanchenged = "True";
            }

            if ("中原" == ly_sales_contract_mainDataGridView.CurrentRow.Cells["公司"].Value.ToString())
            {
                //this.toolStripButton44.Visible = false;

                ly_sales_contract_mainDataGridView.Columns["中成执行"].Visible = false;
                ly_sales_contract_mainDataGridView.Columns["中成批准人"].Visible = false;
                ly_sales_contract_mainDataGridView.Columns["中成批准日期"].Visible = false;
                ly_sales_contract_mainDataGridView.Columns["中成金额0"].Visible = false;



                ly_sales_contract_detailDataGridView.Columns["中成折扣"].Visible = false;
                ly_sales_contract_detailDataGridView.Columns["中成金额"].Visible = false;



                
            }
            else
            {
                //this.toolStripButton44.Visible = true;

                ly_sales_contract_mainDataGridView.Columns["中成执行"].Visible = true;
                ly_sales_contract_mainDataGridView.Columns["中成批准人"].Visible = true;
                ly_sales_contract_mainDataGridView.Columns["中成批准日期"].Visible = true;
                ly_sales_contract_mainDataGridView.Columns["中成金额0"].Visible = true ;


                ly_sales_contract_detailDataGridView.Columns["中成折扣"].Visible = true ;
                ly_sales_contract_detailDataGridView.Columns["中成金额"].Visible = true ;

            }

            //if ("True" == ly_sales_contract_mainDataGridView.CurrentRow.Cells["借用"].Value.ToString())
            //{
            //    this.isborrow = "True";
            //}
            //else
            //{
            //    this.isborrow = "False";
            //}


        }

        private void toolStripButton18_Click(object sender, EventArgs e)
        {
            ExportDataGridviewTOExcell.ExportDataGridview(this.ly_sales_contract_mainDataGridView, true);
        }

        private void toolStripButton27_Click_1(object sender, EventArgs e)
        {
            if (null == this.ly_sales_contract_detailDataGridView.CurrentRow) return;

            if ("" == ly_sales_contract_mainDataGridView.CurrentRow.Cells["类别"].Value.ToString())
            {

                MessageBox.Show("请选择合同类别,然后打印...", "注意");
                return;
            }

            NewFrm.Show(this); ;

            //this.ly_plan_getmaterialTableAdapter.Fill(this.lYPlanMange.ly_plan_getmaterial, parentId);

            BaseReportView queryForm = new BaseReportView();

            queryForm.Text = "中原精密营业合同";

            queryForm.Printdata = this.lYSalseMange;


            if ("工矿" == ly_sales_contract_mainDataGridView.CurrentRow.Cells["类别"].Value.ToString())
            {
                if ("中原" == ly_sales_contract_mainDataGridView.CurrentRow.Cells["公司"].Value.ToString())
                {
                    queryForm.PrintCrystalReport = new LY_YingyeHetong();
                }
                else
                {
                    queryForm.PrintCrystalReport = new LY_YingyeHetongZC();
                }
            }
            else
            {
                if ("中原" == ly_sales_contract_mainDataGridView.CurrentRow.Cells["公司"].Value.ToString())
                {
                    queryForm.PrintCrystalReport = new LY_YingyeHetong_XH();
                }
                else
                {
                    queryForm.PrintCrystalReport = new LY_YingyeHetong_XHZC();
                }
               
            }


            //string selectFormula;

            //selectFormula = "{ly_store_planitemcount.状态}  =   '原料'  and {ly_store_planitemcount.欠料金额}>0 ";
            //queryForm.PrintCrystalReport.DataDefinition.RecordSelectionFormula = selectFormula;

            NewFrm.Hide(this);

            queryForm.ShowDialog();
        }

       

       
        

        private void ly_sales_contract_mainDataGridView_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            DataGridView dgv = sender as DataGridView;

            if (null == dgv.CurrentRow) return;

            /////////////////////////////////////////////////////////////////

            //if ("开票日期" == dgv.CurrentCell.OwningColumn.Name)
            //{

            //    //if ("True" == dgv.CurrentRow.Cells["已交"].Value.ToString())
            //    //{
            //    //    MessageBox.Show("合同文本已经提交,不能修改交付日期...", "注意");

            //    //    return;
            //    //}

            //    ChangeValue queryForm = new ChangeValue();

            //    queryForm.OldValue = dgv.CurrentCell.Value.ToString();
            //    queryForm.NewValue = "";
            //    queryForm.ChangeMode = "datetime";
            //    queryForm.ShowDialog();


            //    if (queryForm.NewValue != "")
            //    {
            //        dgv.CurrentRow.Cells["开票日期"].Value = queryForm.NewValue;
            //        dgv.CurrentRow.Cells["开票人"].Value = "宋美彰";

            //    }
            //    else
            //    {

            //        dgv.CurrentRow.Cells["开票日期"].Value = DBNull.Value;
            //        dgv.CurrentRow.Cells["开票人"].Value = DBNull.Value;

            //    }



            //    SaveContract();



            //    return;

            //}
            //////////////////////////////////////////////////////////////////////////
            //if ("开票人" == dgv.CurrentCell.OwningColumn.Name)
            //{

            //    //if ("True" == dgv.CurrentRow.Cells["已交"].Value.ToString())
            //    //{
            //    //    MessageBox.Show("合同文本已经提交,不能修改交付日期...", "注意");

            //    //    return;
            //    //}

            //    ChangeValue queryForm = new ChangeValue();

            //    queryForm.OldValue = dgv.CurrentCell.Value.ToString();
            //    queryForm.NewValue = "";
            //    queryForm.ChangeMode = "string";
            //    queryForm.ShowDialog();


            //    if (queryForm.NewValue != "")
            //    {
            //        dgv.CurrentRow.Cells["开票人"].Value = queryForm.NewValue;


            //    }
            //    else
            //    {

            //        dgv.CurrentRow.Cells["开票人"].Value = DBNull.Value;


            //    }



            //    SaveContract();



            //    return;

            //}
            //////////////////////////////////////////////////////////////////////////

            //if ("True" == dgv.CurrentRow.Cells["发货"].Value.ToString())
            //{
            //    MessageBox.Show("合同已经发货,不能修改数据...", "注意");
            //    return;

            //}
            ///////////////////////////////
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
            //        SaveContract();

            //        //CountPlanStru();

            //    }
            //    else
            //    {
            //        dgv.CurrentRow.Cells["备注"].Value = DBNull.Value;
            //        SaveContract();
            //    }
            //    return;

            //}


            ///////////////////////////////////////////////////////////


            /////////////////////////////////////////////////////////////////

            if ("中成执行" == dgv.CurrentCell.OwningColumn.Name)
            {

                //if ("True" == dgv.CurrentRow.Cells["批准"].Value.ToString())
                //{
                //    MessageBox.Show("合同文本已经提交,不能修改交付日期...", "注意");

                //    return;
                //}

                //ChangeValue queryForm = new ChangeValue();

                //queryForm.OldValue = dgv.CurrentCell.Value.ToString();
                //queryForm.NewValue = "";
                //queryForm.ChangeMode = "datetime";
                //queryForm.ShowDialog();


                //if (queryForm.NewValue != "")
                //{
                //    dgv.CurrentRow.Cells["合同文本交付"].Value = queryForm.NewValue;

                //}
                //else
                //{

                //    dgv.CurrentRow.Cells["合同文本交付"].Value = DBNull.Value;

                //}

                if ("True" == dgv.CurrentRow.Cells["中成执行"].Value.ToString())
                {
                    dgv.CurrentRow.Cells["中成执行"].Value = "False";
                    
                    dgv.CurrentRow.Cells["中成批准人"].Value = DBNull.Value;
                    dgv.CurrentRow.Cells["中成批准日期"].Value = DBNull.Value;
                    
                }
                else
                {

                    dgv.CurrentRow.Cells["中成执行"].Value = "True";

                    dgv.CurrentRow.Cells["中成批准人"].Value = SQLDatabase.nowUserName();
                    dgv.CurrentRow.Cells["中成批准日期"].Value = SQLDatabase.GetNowdate();
                    
                }


                SaveContract();



                return;

            }
            ////////////////////////////////////////////////////////////////////////

            /////////////////////////////////////////////////////////////////

            //if ("合同文本交付" == dgv.CurrentCell.OwningColumn.Name)
            //{

            //    //if ("True" == dgv.CurrentRow.Cells["已交"].Value.ToString())
            //    //{
            //    //    MessageBox.Show("合同文本已经提交,不能修改交付日期...", "注意");

            //    //    return;
            //    //}

            //    ChangeValue queryForm = new ChangeValue();

            //    queryForm.OldValue = dgv.CurrentCell.Value.ToString();
            //    queryForm.NewValue = "";
            //    queryForm.ChangeMode = "datetime";
            //    queryForm.ShowDialog();


            //    if (queryForm.NewValue != "")
            //    {
            //        dgv.CurrentRow.Cells["合同文本交付"].Value = queryForm.NewValue;
            //        dgv.CurrentRow.Cells["已交"].Value = "True";

            //    }
            //    else
            //    {

            //        dgv.CurrentRow.Cells["合同文本交付"].Value = DBNull.Value;
            //        dgv.CurrentRow.Cells["已交"].Value = "False";

            //    }



            //    SaveContract();



            //    return;

            //}
            //////////////////////////////////////////////////////////////////////////

            ///////////////////////////////////////////////////////////////

            if ("中成批准日期" == dgv.CurrentCell.OwningColumn.Name)
            {

                //if ("True" == dgv.CurrentRow.Cells["已交"].Value.ToString())
                //{
                //    MessageBox.Show("合同文本已经提交,不能修改交付日期...", "注意");

                //    return;
                //}

                ChangeValue queryForm = new ChangeValue();

                queryForm.OldValue = dgv.CurrentCell.Value.ToString();
                queryForm.NewValue = "";
                queryForm.ChangeMode = "datetime";
                queryForm.ShowDialog();


                if (queryForm.NewValue != "")
                {
                    dgv.CurrentRow.Cells["中成批准日期"].Value = queryForm.NewValue;
                    

                }
                else
                {

                    dgv.CurrentRow.Cells["中成批准日期"].Value = DBNull.Value;
                    

                }



                SaveContract();



                return;

            }
            ////////////////////////////////////////////////////////////////////////

            //if ("True" == dgv.CurrentRow.Cells["批准"].Value.ToString())
            //{
            //    MessageBox.Show("合同已经执行,不能修改数据...", "注意");
            //    return;

            //}


            /////////////////////////////////////////////////////////////
            //if ("提交" == dgv.CurrentCell.OwningColumn.Name)
            //{
            //    if (null == ly_sales_contract_detailDataGridView.CurrentRow) return;

            //    foreach (DataGridViewRow dgr in ly_sales_contract_detailDataGridView.Rows)
            //    {

            //        if (Color.Red == dgr.Cells["单件折扣"].Style.BackColor)
            //        {
            //            MessageBox.Show("折扣超权限,请修改合同单价,或者请上级审批,提交取消...", "注意");
            //            return;

            //        }

            //    }


            //    LY_Salescontract_SubmitSet queryForm = new LY_Salescontract_SubmitSet();

            //    string nowbillcode = ly_sales_contract_mainDataGridView.CurrentRow.Cells["依赖书号ht"].Value.ToString();

            //    queryForm.billcode = nowbillcode;
            //    queryForm.clientcode = this.nowclientCode;
            //    queryForm.contractcode = this.nowcontractCode;
            //    queryForm.innercode = this .nowinnerCode;
            //    queryForm.fromwhere = "contract";



            //    queryForm.runmode = "合同提交";

            //    queryForm.WindowState = FormWindowState.Maximized;
            //    queryForm.StartPosition = FormStartPosition.CenterParent;
            //    queryForm.ShowDialog();

            //    if (queryForm.DialogResult == DialogResult.Cancel)
            //    {
            //        dgv.CurrentRow.Cells["提交"].Value = "False";
            //        ly_sales_contract_mainDataGridView.CurrentRow.Cells["依赖书号ht"].Value = DBNull .Value ;
            //        //return;
            //    }
            //    else
            //    {
            //        ly_sales_contract_mainDataGridView.CurrentRow.Cells["依赖书号ht"].Value = queryForm.billcode;
            //        ly_sales_contract_mainDataGridView.EndEdit();

            //        if ("True" == dgv.CurrentRow.Cells["提交"].Value.ToString())
            //        {
            //            dgv.CurrentRow.Cells["提交"].Value = "False";

            //        }
            //        else
            //        {

            //            dgv.CurrentRow.Cells["提交"].Value = "True";
            //        }



            //       // SaveContract();
            //    }



            //    SaveContract();


            //    return;

            //}
            /////////////////////////////////////////////////////////////////


            //if ("True" == dgv.CurrentRow.Cells["提交"].Value.ToString())
            //{
            //    MessageBox.Show("合同已经提交,不能修改数据...", "注意");
            //    return;

            //}


            ////if ("True" == dgv.CurrentRow.Cells["审核"].Value.ToString())
            ////{
            ////    MessageBox.Show("合同已经审批,不能修改数据...", "注意");
            ////    return;

            ////}





            //if ("免税" == dgv.CurrentCell.OwningColumn.Name)
            //{

            //    if ("True" == dgv.CurrentRow.Cells["免税"].Value.ToString())
            //    {
            //        dgv.CurrentRow.Cells["免税"].Value = "False";

            //    }
            //    else
            //    {

            //        dgv.CurrentRow.Cells["免税"].Value = "True";
            //    }



            //    SaveContract();



            //    return;

            //}
            ///////////////////////////////////////////////////////////////

            //if ("借用" == dgv.CurrentCell.OwningColumn.Name)
            //{

            //    if ("True" == dgv.CurrentRow.Cells["借用"].Value.ToString())
            //    {
            //        dgv.CurrentRow.Cells["借用"].Value = "False";

            //    }
            //    else
            //    {

            //        dgv.CurrentRow.Cells["借用"].Value = "True";

            //    }



            //    SaveContract();



            //    return;

            //}
            ///////////////////////////////////////////////////////////////

            //if ("审核" == dgv.CurrentCell.OwningColumn.Name)
            //{

            //    if ("True" == dgv.CurrentRow.Cells["审核"].Value.ToString())
            //    {
            //        dgv.CurrentRow.Cells["审核"].Value = "False";
            //        dgv.CurrentRow.Cells["审核人"].Value = DBNull.Value;
            //    }
            //    else
            //    {

            //        dgv.CurrentRow.Cells["审核"].Value = "True";
            //        dgv.CurrentRow.Cells["审核人"].Value = SQLDatabase.nowUserName();
            //    }



            //    SaveContract();



            //    return;

            //}
            //////////////////////////////////////////////////////////////////////////
            /////////////////////////////////////////////////////////////////
            //if ("已交" == dgv.CurrentCell.OwningColumn.Name)
            //{

            //    if ("True" == dgv.CurrentRow.Cells["已交"].Value.ToString())
            //    {
            //        dgv.CurrentRow.Cells["已交"].Value = "False";

            //    }
            //    else
            //    {

            //        dgv.CurrentRow.Cells["已交"].Value = "True";
            //    }



            //    SaveContract();



            //    return;

            //}


            //if ("开票日期" == dgv.CurrentCell.OwningColumn.Name)
            //{

            //    ChangeValue queryForm = new ChangeValue();

            //    queryForm.OldValue = dgv.CurrentCell.Value.ToString();
            //    queryForm.NewValue = "";
            //    queryForm.ChangeMode = "datetime";
            //    queryForm.ShowDialog();


            //    if (queryForm.NewValue != "")
            //    {
            //        dgv.CurrentRow.Cells["开票日期"].Value = queryForm.NewValue;
            //        dgv.CurrentRow.Cells["开票人"].Value = SQLDatabase.nowUserName();
            //    }
            //    else
            //    {

            //        dgv.CurrentRow.Cells["开票日期"].Value = DBNull.Value;
            //        dgv.CurrentRow.Cells["开票人"].Value = DBNull.Value;
            //    }



            //    SaveContract();



            //    return;

            //}
            //////////////////////////////////////////////////////////////////////////


            //if ("签订日期" == dgv.CurrentCell.OwningColumn.Name)
            //{

            //    ChangeValue queryForm = new ChangeValue();

            //    queryForm.OldValue = dgv.CurrentCell.Value.ToString();
            //    queryForm.NewValue = "";
            //    queryForm.ChangeMode = "datetime";
            //    queryForm.ShowDialog();




            //    if (queryForm.NewValue != "")
            //    {
            //        dgv.CurrentRow.Cells["签订日期"].Value = queryForm.NewValue;
            //        //dgv.CurrentRow.Cells["discount_money"].Value = DBNull.Value;

            //        //dgv.CurrentRow.Cells["approve_flag"].Value = "False";
            //        SaveContract();


            //        //CountPlanStru();

            //    }
            //    else
            //    {


            //    }
            //    return;

            //}

            ///////////////////////////////
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
            //        SaveContract();

            //        //CountPlanStru();

            //    }
            //    else
            //    {

            //    }
            //    return;

            //}


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


            /////////////////////////////////////////////////////////

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
            //if ("公司" == dgv.CurrentCell.OwningColumn.Name)
            //{






            //    string sel;



            //    sel = "SELECT  company_code as 编码, company_name as 名称 FROM ly_company_information ";



            //    QueryForm queryForm = new QueryForm();


            //    queryForm.Sel = sel;
            //    queryForm.Constr = SQLDatabase.Connectstring;

            //    //Set the Column Collection to the filter Table
            //    //queryForm.SetSourceColumns(this.billMainDataSet.BalanceBill.Columns);

            //    queryForm.ShowDialog();


            //    if (queryForm.Result != "")
            //    {
            //        dgv.CurrentRow.Cells["公司编码"].Value = queryForm.Result;
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
            //this.ly_sales_contract_mainDataGridView.CurrentRow.Cells["录入人c"].Value = SQLDatabase.nowUserName();

            this.ly_sales_contract_mainDataGridView.EndEdit();
            this.ly_sales_contract_main_forbusinessZCBindingSource.EndEdit();

            this.ly_sales_contract_main_forbusinessZCTableAdapter.Update(this.lYSalseMange.ly_sales_contract_main_forbusinessZC);



            RefreshData();
        }

        private void RefreshData()
        {









            int nowcontractId;
            if (null != ly_sales_contract_mainDataGridView.CurrentRow)
            {
                nowcontractId = int.Parse(ly_sales_contract_mainDataGridView.CurrentRow.Cells["id_main"].Value.ToString());
            }
            else
            {
                nowcontractId = 0;
            }

            //int nowcontractdetailId;
            //if (null != ly_purchase_contract_detailDataGridView.CurrentRow)
            //{
            //    nowcontractdetailId = int.Parse(ly_purchase_contract_detailDataGridView.CurrentRow.Cells["id7"].Value.ToString());
            //}
            //else
            //{
            //    nowcontractdetailId = 0;
            //}



            int nowcontractdetailid;
            if (null != ly_sales_contract_detailDataGridView.CurrentRow)
            {
                nowcontractdetailid = int.Parse(ly_sales_contract_detailDataGridView.CurrentRow.Cells["id_detail"].Value.ToString());
            }
            else
            {
                nowcontractdetailid = 0;
            }


            string nowclientcode = this.ly_sales_contract_mainDataGridView.CurrentRow.Cells["客户编码c"].Value.ToString();




            this.ly_sales_contract_main_forbusinessZCTableAdapter.Fill(this.lYSalseMange.ly_sales_contract_main_forbusinessZC, this.dateTimePicker1.Value, this.dateTimePicker2.Value.AddDays(1));
            this.ly_sales_contract_main_forbusinessZCBindingSource.Position = this.ly_sales_contract_main_forbusinessZCBindingSource.Find("id", nowcontractId);
            
            this.ly_sales_contract_terms_forcontractBindingSource.Position = this.ly_sales_contract_terms_forcontractBindingSource.Find("id", nowcontractId);

            //this.ly_sales_contract_main1DataGridView.SelectionChanged -= ly_sales_contract_main1DataGridView_SelectionChanged;
            //this.ly_sales_contract_main1TableAdapter.Fill(this.lYSalseMange.ly_sales_contract_main1, this.nowusercode, "single", this.dateTimePicker1.Value, this.dateTimePicker2.Value);
            //this.ly_sales_contract_main1DataGridView.SelectionChanged += ly_sales_contract_main1DataGridView_SelectionChanged;

            this.ly_sales_contract_detailBindingSource.Position = this.ly_sales_contract_detailBindingSource.Find("id", nowcontractdetailid);
            //this.ly_purchase_contract_detailBindingSource.Position = this.ly_purchase_contract_detailBindingSource.Find("物料编号", nowcontractdetailcode);

        }

       
       
        private static void InserSalseContractDetail(string innerCode, string nowitem, decimal nowabsqty, string isborrow)
        {

            string insStr;

            if ("True" == isborrow)
            {
                insStr = " INSERT INTO ly_sales_contract_detail  " +
             "( contract_inner_code,itemno,brrow_qty) " +
             " values ('" + innerCode + "','" + nowitem + "'," + nowabsqty + ")";
            }
            else
            {
                insStr = " INSERT INTO ly_sales_contract_detail  " +
              "( contract_inner_code,itemno,qty) " +
              " values ('" + innerCode + "','" + nowitem + "'," + nowabsqty + ")";
            }


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
        }

        private void ly_sales_contract_detailDataGridView_CellFormatting_1(object sender, DataGridViewCellFormattingEventArgs e)
        {
            DataGridView dgv = sender as DataGridView;
            if (e.RowIndex > -1)
            {
                if ((dgv.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.GetType() == typeof(decimal)
                      || dgv.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.GetType() == typeof(double)
                      || dgv.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.GetType() == typeof(int)))
                {
                    if (Convert.ToDecimal(dgv.Rows[e.RowIndex].Cells[e.ColumnIndex].Value) == 0)
                    {
                        e.Value = "";
                    }
                }
            }       
        }

        private void ly_sales_contract_detailDataGridView_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            string isgood = "yes";

            decimal nowrealdis;

            decimal nowuserdis;
            decimal nowclientdis;
            decimal nowproductdis;

            foreach (DataGridViewRow dgr in ly_sales_contract_detailDataGridView.Rows)
            {


                if ("" != dgr.Cells["单件折扣"].Value.ToString())
                {
                    nowrealdis = decimal.Parse(dgr.Cells["单件折扣"].Value.ToString());
                }
                else
                {
                    nowrealdis = 0;
                }
                ///////////////////////////////////////////////////////////////////////

                if ("" != dgr.Cells["产品折扣"].Value.ToString())
                {
                    nowproductdis = decimal.Parse(dgr.Cells["产品折扣"].Value.ToString());
                }
                else
                {
                    nowproductdis = 0;
                }
                /////////////////////////////////////////////////////////////////////////

                if ("" != dgr.Cells["营业员折扣"].Value.ToString())
                {
                    nowuserdis = decimal.Parse(dgr.Cells["营业员折扣"].Value.ToString());
                }
                else
                {
                    nowuserdis = 0;
                }
                ////////////////////////////////////////////////////////////////////////////////////

                if ("" != dgr.Cells["客户折扣"].Value.ToString())
                {
                    nowclientdis = decimal.Parse(dgr.Cells["客户折扣"].Value.ToString());
                }
                else
                {
                    nowclientdis = 0;
                }
                /////////////////////////////////////////////////////////////////////////////////////////////////////////




                if ("True" != dgr.Cells["审批"].Value.ToString())
                {

                    if (nowrealdis > (nowproductdis * nowuserdis) / 100 && nowrealdis > nowclientdis)
                    {
                        foreach (DataGridViewCell dgc in dgr.Cells)
                        {

                            dgc.Style.BackColor = Color.Red;
                            dgc.Style.ForeColor = Color.White;
                        }
                    }
                }
                //else
                //{ 


                //}



            }
        }

        private void SaveDetailItem()
        {
            this.ly_sales_contract_detailBindingSource.EndEdit();
            this.ly_sales_contract_detailTableAdapter.Update(this.lYSalseMange.ly_sales_contract_detail);

            int nowdetailId;
            if (null != ly_sales_contract_detailDataGridView.CurrentRow)
            {
                nowdetailId = int.Parse(ly_sales_contract_detailDataGridView.CurrentRow.Cells["id_detail"].Value.ToString());
            }
            else
            {
                nowdetailId = 0;
            }


            int nowcontractId;
            if (null != ly_sales_contract_mainDataGridView.CurrentRow)
            {
                nowcontractId = int.Parse(ly_sales_contract_mainDataGridView.CurrentRow.Cells["id_main"].Value.ToString());
            }
            else
            {
                nowcontractId = 0;
            }


            //if (null == this.ly_sales_clientDataGridView.CurrentRow)
            //{ 
            //  this.ly_sales_clientDataGridView.
            //}

            //string nowclientcode = this.ly_sales_clientDataGridView.CurrentRow.Cells["客户编码"].Value.ToString();
            //this.ly_sales_clientDataGridView.SelectionChanged -= ly_sales_clientDataGridView_SelectionChanged;
            //this.ly_sales_clientTableAdapter.Fill(this.lYSalseMange.ly_sales_client);
            //this.ly_sales_clientDataGridView.SelectionChanged += ly_sales_clientDataGridView_SelectionChanged;

            //this.ly_sales_clientBindingSource.Position = this.ly_sales_clientBindingSource.Find("客户编码", nowclientCode);

            //this.ly_sales_contract_mainDataGridView.CurrentRow.Cells["录入人c"].Value = SQLDatabase.nowUserName();
            this.ly_sales_contract_mainDataGridView.EndEdit();
            this.ly_sales_contract_main_forbusinessZCBindingSource.EndEdit();
            this.ly_sales_contract_main_forbusinessZCTableAdapter.Update(this.lYSalseMange.ly_sales_contract_main_forbusinessZC);

            this.ly_sales_contract_main_forbusinessZCTableAdapter.Fill(this.lYSalseMange.ly_sales_contract_main_forbusinessZC, this.dateTimePicker1.Value, this.dateTimePicker2.Value.AddDays(1)); ;
            this.ly_sales_contract_main_forbusinessZCBindingSource.Position = this.ly_sales_contract_main_forbusinessZCBindingSource.Find("id", nowcontractId);

            //this.ly_sales_contract_main1DataGridView.SelectionChanged -= ly_sales_contract_main1DataGridView_SelectionChanged;
            //this.ly_sales_contract_main1TableAdapter.Fill(this.lYSalseMange.ly_sales_contract_main1, this.nowusercode, this.nowfillstragecode, this.dateTimePicker1.Value, this.dateTimePicker2.Value);
            //this.ly_sales_contract_main1DataGridView.SelectionChanged += ly_sales_contract_main1DataGridView_SelectionChanged;

            //this.ly_sales_contract_detailTableAdapter.Fill(this.lYSalseMange.ly_sales_contract_detail, nowinnerCode);

            this.ly_sales_contract_detailBindingSource.Position = this.ly_sales_contract_detailBindingSource.Find("id", nowdetailId);

            this.ly_sales_contract_detailDataGridView.CurrentRow.Cells["单件折扣"].Value = this.ly_sales_contract_detailDataGridView.CurrentRow.Cells["单件折扣"].Value;
            this.ly_sales_contract_detailDataGridView.CurrentRow.Cells["单价"].Value = this.ly_sales_contract_detailDataGridView.CurrentRow.Cells["单价"].Value;

            foreach (DataGridViewRow dgr in ly_sales_contract_detailDataGridView.Rows)
            {
                dgr.Cells["顺序"].Value = dgr.Index + 1;

            }

            this.ly_sales_contract_detailDataGridView.EndEdit();
            this.ly_sales_contract_detailBindingSource.EndEdit();
            this.ly_sales_contract_detailTableAdapter.Update(this.lYSalseMange.ly_sales_contract_detail);
        }

        private void ly_sales_contract_detailDataGridView_CellMouseDoubleClick_1(object sender, DataGridViewCellMouseEventArgs e)
        {
            DataGridView dgv = sender as DataGridView;

            if (null == dgv.CurrentRow) return;

            if ("True" == ly_sales_contract_mainDataGridView.CurrentRow.Cells["批准"].Value.ToString()
                  && "中成折扣" != dgv.CurrentCell.OwningColumn.Name
                  )
            {
                MessageBox.Show("合同已经执行,不能修改数据...", "注意");
                return;

            }

            ////////////////////////////////////////

            //if ("赠送" == dgv.CurrentCell.OwningColumn.Name)
            //{

            //    if ("True" == dgv.CurrentRow.Cells["赠送"].Value.ToString())
            //    {
            //        dgv.CurrentRow.Cells["赠送"].Value = "False";

            //    }
            //    else
            //    {

            //        dgv.CurrentRow.Cells["赠送"].Value = "True";
            //    }



            //    SaveDetailItem();



            //    return;

            //}

            /////////////////////////////////////////////////////////////////
            if ("False" == this.contractCanchenged
                 && "中成折扣" != dgv.CurrentCell.OwningColumn.Name
                )
            {
                MessageBox.Show("合同已经提交执行,不能修改数据...", "注意");
                return;

            }

            //////////////////////////////////////

            /////////////////////////////////////////////////////////////////
            //if ("商标" == dgv.CurrentCell.OwningColumn.Name)
            //{

            //    ChangeValue queryForm = new ChangeValue();

            //    queryForm.OldValue = dgv.CurrentCell.Value.ToString();
            //    queryForm.NewValue = "";
            //    queryForm.ChangeMode = "string";
            //    queryForm.ShowDialog();




            //    if (queryForm.NewValue != "")
            //    {
            //        dgv.CurrentRow.Cells["商标"].Value = queryForm.NewValue;
            //        //dgv.CurrentRow.Cells["discount_money"].Value = DBNull.Value;

            //        //dgv.CurrentRow.Cells["approve_flag"].Value = "False";
            //        SaveDetailItem();

            //        //CountPlanStru();

            //    }
            //    else
            //    {

            //    }
            //    return;

            //}
            /////////////////////////////////////////////////////////////////
            //if ("数量" == dgv.CurrentCell.OwningColumn.Name)
            //{

            //    if ("True" == dgv.CurrentRow.Cells["审批"].Value.ToString())
            //    {

            //        MessageBox.Show("合同已经审批,不能修改数据...", "注意");
            //        return;
            //    }

            //    if ("True" == this.isborrow)
            //    {

            //        MessageBox.Show("借用合同已经审批,不能输入合同数量...", "注意");
            //        return;
            //    }

            //    ChangeValue queryForm = new ChangeValue();

            //    queryForm.OldValue = dgv.CurrentCell.Value.ToString();
            //    queryForm.NewValue = "";
            //    queryForm.ChangeMode = "value";
            //    queryForm.ShowDialog();




            //    if (queryForm.NewValue != "")
            //    {
            //        dgv.CurrentRow.Cells["数量"].Value = queryForm.NewValue;
            //        //dgv.CurrentRow.Cells["discount_money"].Value = DBNull.Value;

            //        //dgv.CurrentRow.Cells["approve_flag"].Value = "False";
            //        SaveDetailItem();


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

            /////////////////////////////////////////////////////////////////
            //if ("借用数" == dgv.CurrentCell.OwningColumn.Name)
            //{

            //    if ("True" == dgv.CurrentRow.Cells["审批"].Value.ToString())
            //    {

            //        MessageBox.Show("合同已经审批,不能修改数据...", "注意");
            //        return;
            //    }

            //    if ("False" == this.isborrow)
            //    {

            //        MessageBox.Show("普通合同已经审批,不能输入借用数量...", "注意");
            //        return;
            //    }

            //    ChangeValue queryForm = new ChangeValue();

            //    queryForm.OldValue = dgv.CurrentCell.Value.ToString();
            //    queryForm.NewValue = "";
            //    queryForm.ChangeMode = "value";
            //    queryForm.ShowDialog();




            //    if (queryForm.NewValue != "")
            //    {
            //        dgv.CurrentRow.Cells["借用数"].Value = queryForm.NewValue;
            //        //dgv.CurrentRow.Cells["discount_money"].Value = DBNull.Value;

            //        //dgv.CurrentRow.Cells["approve_flag"].Value = "False";
            //        SaveDetailItem();


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

            /////////////////////////////////////////////////////////////////
            if ("中成折扣" == dgv.CurrentCell.OwningColumn.Name)
            {

                //if ("True" == dgv.CurrentRow.Cells["审批"].Value.ToString())
                //{

                //    MessageBox.Show("合同已经审批,不能修改数据...", "注意");
                //    return;
                //}

                //if ("False" == this.isborrow)
                //{

                //    MessageBox.Show("普通合同已经审批,不能输入借用数量...", "注意");
                //    return;
                //}

                ChangeValue queryForm = new ChangeValue();

                queryForm.OldValue = dgv.CurrentCell.Value.ToString();
                queryForm.NewValue = "";
                queryForm.ChangeMode = "value";
                queryForm.ShowDialog();




                if (queryForm.NewValue != "")
                {
                    dgv.CurrentRow.Cells["中成折扣"].Value = queryForm.NewValue;
                    //dgv.CurrentRow.Cells["discount_money"].Value = DBNull.Value;

                    //dgv.CurrentRow.Cells["approve_flag"].Value = "False";
                    SaveDetailItem();


                    //CountPlanStru();

                }
                else
                {
                    dgv.CurrentRow.Cells["中成折扣"].Value = DBNull.Value;
                    //dgv.CurrentRow.Cells["discount_money"].Value = DBNull.Value;

                    //dgv.CurrentRow.Cells["approve_flag"].Value = "False";
                    SaveDetailItem();
                }
                return;

            }

            /////////////////////////////////////////////////////////////////
            //if ("单价" == dgv.CurrentCell.OwningColumn.Name)
            //{



            //    if ("True" == dgv.CurrentRow.Cells["审批"].Value.ToString())
            //    {

            //        MessageBox.Show("合同已经审批,不能修改数据...", "注意");
            //        return;
            //    }

            //    ChangeValue queryForm = new ChangeValue();

            //    queryForm.OldValue = dgv.CurrentCell.Value.ToString();
            //    queryForm.NewValue = "";
            //    queryForm.ChangeMode = "value";
            //    queryForm.setInFocus();
            //    queryForm.ShowDialog(this);





            //    if (queryForm.NewValue != "")
            //    {
            //        dgv.CurrentRow.Cells["单价"].Value = queryForm.NewValue;

            //        decimal nowsalesprice;

            //        if ("" != dgv.CurrentRow.Cells["营业定价"].Value.ToString())
            //        {
            //            nowsalesprice = decimal.Parse(dgv.CurrentRow.Cells["营业定价"].Value.ToString());
            //        }
            //        else
            //        {
            //            nowsalesprice = 1;
            //        }

            //        dgv.CurrentRow.Cells["单件折扣"].Value = (nowsalesprice - decimal.Parse(queryForm.NewValue)) / nowsalesprice * 100;
            //        //dgv.CurrentRow.Cells["discount_money"].Value = DBNull.Value;

            //        //dgv.CurrentRow.Cells["approve_flag"].Value = "False";
            //        SaveDetailItem();


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


            ///////////////////////////////////////////////////////
            /////////////////////////////////////////////////////////////////
            //if ("营业定价" == dgv.CurrentCell.OwningColumn.Name)
            //{


            //    //if (!SQLDatabase.CheckHaveRight(SQLDatabase.NowUserID, "营业综合信息"))
            //    //{
            //    //    return;
            //    //}

            //    if ("Z" != dgv.CurrentRow.Cells["产品编码"].Value.ToString().Substring(0, 1))
            //    {
            //        return;
            //    }


            //    ChangeValue queryForm = new ChangeValue();

            //    queryForm.OldValue = dgv.CurrentCell.Value.ToString();
            //    queryForm.NewValue = "";
            //    queryForm.ChangeMode = "value";
            //    queryForm.setInFocus();
            //    queryForm.ShowDialog(this);




            //    if (queryForm.NewValue != "")
            //    {
            //        dgv.CurrentRow.Cells["营业定价"].Value = queryForm.NewValue;

            //        decimal nowitemprice;
            //        decimal nowsalesprice;

            //        nowsalesprice = decimal.Parse(queryForm.NewValue);

            //        if (nowsalesprice > 0)
            //        {

            //        }
            //        else
            //        {
            //            nowsalesprice = 1;
            //        }

            //        if ("" != dgv.CurrentRow.Cells["单价"].Value.ToString())
            //        {
            //            nowitemprice = decimal.Parse(dgv.CurrentRow.Cells["单价"].Value.ToString());
            //        }
            //        else
            //        {
            //            nowitemprice = 0;
            //        }

            //        dgv.CurrentRow.Cells["单件折扣"].Value = (nowsalesprice - nowitemprice) / nowsalesprice * 100;
            //        //dgv.CurrentRow.Cells["discount_money"].Value = DBNull.Value;

            //        //dgv.CurrentRow.Cells["approve_flag"].Value = "False";
            //        SaveDetailItem();


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


            ///////////////////////////////////////////////////////

            //if ("单件折扣" == dgv.CurrentCell.OwningColumn.Name)
            //{


            //    if ("True" == dgv.CurrentRow.Cells["审批"].Value.ToString())
            //    {

            //        MessageBox.Show("合同已经审批,不能修改数据...", "注意");
            //        return;
            //    }



            //    ChangeValue queryForm = new ChangeValue();

            //    queryForm.OldValue = dgv.CurrentCell.Value.ToString();
            //    queryForm.NewValue = "";
            //    queryForm.ChangeMode = "value";
            //    queryForm.ShowDialog();




            //    if (queryForm.NewValue != "")
            //    {
            //        dgv.CurrentRow.Cells["单件折扣"].Value = queryForm.NewValue;

            //        decimal nowsalesprice;

            //        if ("" != dgv.CurrentRow.Cells["营业定价"].Value.ToString())
            //        {
            //            nowsalesprice = decimal.Parse(dgv.CurrentRow.Cells["营业定价"].Value.ToString());
            //        }
            //        else
            //        {
            //            nowsalesprice = 0;
            //        }

            //        dgv.CurrentRow.Cells["单价"].Value = nowsalesprice * (100 - decimal.Parse(queryForm.NewValue)) / 100;
            //        //dgv.CurrentRow.Cells["discount_money"].Value = DBNull.Value;

            //        //dgv.CurrentRow.Cells["approve_flag"].Value = "False";
            //        SaveDetailItem();


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

            //if ("交货时间" == dgv.CurrentCell.OwningColumn.Name)
            //{

            //    ChangeValue queryForm = new ChangeValue();

            //    queryForm.OldValue = dgv.CurrentCell.Value.ToString();
            //    queryForm.NewValue = "";
            //    queryForm.ChangeMode = "datetime";
            //    queryForm.ShowDialog();




            //    if (queryForm.NewValue != "")
            //    {
            //        dgv.CurrentRow.Cells["交货时间"].Value = queryForm.NewValue;
            //        //dgv.CurrentRow.Cells["discount_money"].Value = DBNull.Value;

            //        //dgv.CurrentRow.Cells["approve_flag"].Value = "False";
            //        SaveDetailItem();


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


            ///////////////////////////////////////////////////////
            /////////////////////////////////////////////////////////////////

            //if ("审批" == dgv.CurrentCell.OwningColumn.Name)
            //{

            //    decimal nowrealdis;

            //    if ("" != dgv.CurrentRow.Cells["单件折扣"].Value.ToString())
            //    {
            //        nowrealdis = decimal.Parse(dgv.CurrentRow.Cells["单件折扣"].Value.ToString());
            //    }
            //    else
            //    {
            //        nowrealdis = 0;
            //    }

            //    decimal nowleaderdiscount = SQLDatabase.nowUserdiscount();

            //    if (nowrealdis > nowleaderdiscount)
            //    {
            //        MessageBox.Show("折扣权限不够,审批取消...", "注意");
            //        return;

            //    }


            //    if ("True" == dgv.CurrentRow.Cells["审批"].Value.ToString())
            //    {
            //        dgv.CurrentRow.Cells["审批"].Value = "False";
            //        dgv.CurrentRow.Cells["审批人"].Value = DBNull.Value;
            //        dgv.CurrentRow.Cells["审批日期"].Value = DBNull.Value;
            //        dgv.CurrentRow.Cells["审批人折扣"].Value = DBNull.Value;
            //    }
            //    else
            //    {

            //        dgv.CurrentRow.Cells["审批"].Value = "True";
            //        dgv.CurrentRow.Cells["审批人"].Value = SQLDatabase.nowUserName();
            //        dgv.CurrentRow.Cells["审批日期"].Value = SQLDatabase.GetNowdate();
            //        dgv.CurrentRow.Cells["审批人折扣"].Value = SQLDatabase.nowUserdiscount();
            //    }



            //    SaveDetailItem();



            //    return;

            //}
            ///////////////////////////////////////////////////////////////////

            //if ("备注2" == dgv.CurrentCell.OwningColumn.Name)
            //{

            //    ChangeValue queryForm = new ChangeValue();

            //    queryForm.OldValue = dgv.CurrentCell.Value.ToString();
            //    queryForm.NewValue = "";
            //    queryForm.ChangeMode = "longstring";
            //    queryForm.ShowDialog();




            //    if (queryForm.NewValue != "")
            //    {
            //        dgv.CurrentRow.Cells["备注2"].Value = queryForm.NewValue;
            //        //dgv.CurrentRow.Cells["discount_money"].Value = DBNull.Value;

            //        //dgv.CurrentRow.Cells["approve_flag"].Value = "False";
            //        SaveDetailItem();


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


            /////////////////////////////////////////////////////
        }

        private void bindingNavigatorDeleteItem2_Click(object sender, EventArgs e)
        {
            DeleteDetail();
        }
        private void DeleteDetail()
        {
            if (null == ly_sales_contract_detailDataGridView.CurrentRow)
            {


                return;
            }

            if ("False" == this.contractCanchenged)
            {
                MessageBox.Show("合同已经提交,不能删除数据...", "注意");
                return;

            }


            string message = "确定删除当前条目吗？";
            string caption = "提示...";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result;



            result = MessageBox.Show(message, caption, buttons,
            MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

            if (result == DialogResult.Yes)
            {

                this.ly_sales_contract_detailBindingSource.RemoveCurrent();


                ly_sales_contract_detailDataGridView.EndEdit();
                ly_sales_contract_detailBindingSource.EndEdit();



                this.ly_sales_contract_detailTableAdapter.Update(this.lYSalseMange.ly_sales_contract_detail);


            }
        }

       

        private void ly_sales_contract_terms_forcontractDataGridView_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            DataGridView dgv = sender as DataGridView;

            if (null == dgv.CurrentRow) return;

            if ("False" == this.contractCanchenged)
            {
                MessageBox.Show("合同已经执行,不能修改数据...", "注意");
                return;

            }



            ///////////////////////////////////////////////////////////////
            if ("编号" == dgv.CurrentCell.OwningColumn.Name)
            {

                ChangeValue queryForm = new ChangeValue();

                queryForm.OldValue = dgv.CurrentCell.Value.ToString();
                queryForm.NewValue = "";
                queryForm.ChangeMode = "string";
                queryForm.ShowDialog();




                if (queryForm.NewValue != "")
                {
                    dgv.CurrentRow.Cells["编号"].Value = queryForm.NewValue;
                    //dgv.CurrentRow.Cells["discount_money"].Value = DBNull.Value;

                    //dgv.CurrentRow.Cells["approve_flag"].Value = "False";
                    this.ly_sales_contract_terms_forcontractBindingSource.EndEdit();
                    this.ly_sales_contract_terms_forcontractTableAdapter.Update(this.lYSalseMange.ly_sales_contract_terms_forcontract);

                    //CountPlanStru();

                }
                else
                {

                }
                return;

            }
            ///////////////////////////////////////////////////////////////
            if ("条款选项" == dgv.CurrentCell.OwningColumn.Name)
            {

                ChangeValue queryForm = new ChangeValue();

                queryForm.OldValue = dgv.CurrentCell.Value.ToString();
                queryForm.NewValue = "";
                queryForm.ChangeMode = "longstring";
                queryForm.ShowDialog();




                if (queryForm.NewValue != "")
                {
                    dgv.CurrentRow.Cells["条款选项"].Value = queryForm.NewValue;
                    //dgv.CurrentRow.Cells["discount_money"].Value = DBNull.Value;

                    //dgv.CurrentRow.Cells["approve_flag"].Value = "False";
                    this.ly_sales_contract_terms_forcontractBindingSource.EndEdit();
                    this.ly_sales_contract_terms_forcontractTableAdapter.Update(this.lYSalseMange.ly_sales_contract_terms_forcontract);

                    //CountPlanStru();

                }
                else
                {

                }
                return;

            }

            ///////////////////////////////////////////////////////////////
            if ("条款描述" == dgv.CurrentCell.OwningColumn.Name)
            {

                ChangeValue queryForm = new ChangeValue();

                queryForm.OldValue = dgv.CurrentCell.Value.ToString();
                queryForm.NewValue = "";
                queryForm.ChangeMode = "longstring";
                queryForm.ShowDialog();




                if (queryForm.NewValue != "")
                {
                    dgv.CurrentRow.Cells["条款描述"].Value = queryForm.NewValue;
                    //dgv.CurrentRow.Cells["discount_money"].Value = DBNull.Value;

                    //dgv.CurrentRow.Cells["approve_flag"].Value = "False";
                    this.ly_sales_contract_terms_forcontractBindingSource.EndEdit();
                    this.ly_sales_contract_terms_forcontractTableAdapter.Update(this.lYSalseMange.ly_sales_contract_terms_forcontract);

                    //CountPlanStru();

                }
                else
                {

                }
                return;

            }


            ///////////////////////////////////////////////////////
        }

       



        private void ly_sales_contract_detailDataGridView_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex >= 0)
                selectionIdx = e.RowIndex;
        }

        private void ly_sales_contract_detailDataGridView_CellMouseMove(object sender, DataGridViewCellMouseEventArgs e)
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
        private void ly_sales_contract_detailDataGridView_DragDrop(object sender, DragEventArgs e)
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
                            dgvr.Cells["顺序"].Value = dgvr.Index;

                        }
                    }
                }
                if (idx < row.Index)
                {
                    foreach (DataGridViewRow dgvr in dgv.Rows)
                    {

                        if (dgvr.Index >= idx && dgvr.Index < row.Index)
                        {
                            dgvr.Cells["顺序"].Value = dgvr.Index + 2;

                        }
                    }
                }


                row.Cells["顺序"].Value = idx + 1;
                // dgv.Rows[idx].Cells["顺序"].Value = row.Index + 1;

                SaveDetailItem();



                dgv.Rows[idx].Selected = true;
                dgv.CurrentCell = dgv.Rows[idx].Cells["顺序"];


                //selectionIdx = idx;
            } 
        }

        private void ly_sales_contract_detailDataGridView_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move; 
        }

     

       

       

        private void toolStripButton44_Click(object sender, EventArgs e)
        {
            if (null == this.ly_sales_contract_detailDataGridView.CurrentRow) return;

            //if ("True" != this.ly_sales_contract_mainDataGridView.CurrentRow.Cells["批准"].Value.ToString())
            //{

            //    MessageBox.Show("请先确定 执行,然后打印...", "注意");
            //    return;
            //}

            NewFrm.Show(this); ;

            //this.ly_plan_getmaterialTableAdapter.Fill(this.lYPlanMange.ly_plan_getmaterial, parentId);

            BaseReportView queryForm = new BaseReportView();

            queryForm.Text = "中原精密营业合同";

            queryForm.setchackBoxCansee(true);

            queryForm.Printdata = this.lYSalseMange;
            queryForm.company = "财务";

            //if ("中原" == ly_sales_contract_mainDataGridView.CurrentRow.Cells["公司"].Value.ToString())
            //{
            //    queryForm.PrintCrystalReport = new LY_YingyeHetong_FH();
            //}
            //else
            //{

            //    queryForm.PrintCrystalReport = new LY_YingyeHetong_FHzhongc();
            //}


            queryForm.PrintCrystalReport = new LY_YingyeHetong_FH_zcdisf();


            NewFrm.Hide(this);

            queryForm.ShowDialog();
        }

        private void toolStripButton21_Click(object sender, EventArgs e)
        {
          
            this.ly_sales_contract_main_forbusinessZCTableAdapter.Fill(this.lYSalseMange.ly_sales_contract_main_forbusinessZC, this.dateTimePicker1.Value, this.dateTimePicker2.Value.AddDays(1));
            //AddSummationRow_New(ly_sales_contract_main1BindingSource, ly_sales_contract_main1DataGridView);
        }

        private void toolStripTextBox1_KeyUp(object sender, KeyEventArgs e)
        {
            string filterString;


            filterString = GetDataGridviewMultiFilterString.DGVMultiFilterString(this.ly_sales_contract_mainDataGridView, this.toolStripTextBox1.Text);


            this.ly_sales_contract_main_forbusinessZCBindingSource.Filter = filterString;
        }

        private void toolStripTextBox1_Enter(object sender, EventArgs e)
        {
            toolStripTextBox1.Text = "";

            this.ly_sales_contract_main_forbusinessZCBindingSource.Filter = "";
        }





        private void AddSummationRow_New(BindingSource bs, DataGridView dgv)
        {
            //InitializeApp();
            //return;

            DataRow sumdr = (((DataSet)bs.DataSource).Tables[bs.DataMember]).NewRow();

            if (-1 != bs.Find("清单号", "合计"))
            {
                bs.RemoveAt(bs.Find("清单号", "合计"));
            }

            foreach (DataGridViewRow dgvRow in dgv.Rows)
            {
                foreach (DataGridViewCell dgvCell in dgvRow.Cells)
                {
                    //foreach (DataGridViewColumn dgvColumn in dgv.Columns)
                    //{
                    if (dgvCell.Value != null && !(dgvCell.Value is DBNull))
                    {
                        if (IsInteger(dgvCell.Value))
                        {
                            if ("年份" != dgvCell.OwningColumn.HeaderText && "月份" != dgvCell.OwningColumn.HeaderText)
                            {
                                if (sumdr[dgvCell.OwningColumn.DataPropertyName] == null || (sumdr[dgvCell.OwningColumn.DataPropertyName] is DBNull))
                                    sumdr[dgvCell.OwningColumn.DataPropertyName] = 0;


                                sumdr[dgvCell.OwningColumn.DataPropertyName] = Convert.ToInt64(sumdr[dgvCell.OwningColumn.DataPropertyName]) + Convert.ToInt64(dgvCell.Value);
                            }
                        }
                        else if (IsDecimal(dgvCell.Value))
                        {
                            if ("年份" != dgvCell.OwningColumn.HeaderText && "月份" != dgvCell.OwningColumn.HeaderText)
                            {
                                if (sumdr[dgvCell.OwningColumn.DataPropertyName] == null || (sumdr[dgvCell.OwningColumn.DataPropertyName] is DBNull))
                                    sumdr[dgvCell.OwningColumn.DataPropertyName] = 0;
                                //dgvCell .OwningColumn.Name  dgvCell.ColumnIndex

                                sumdr[dgvCell.OwningColumn.DataPropertyName] = Convert.ToDecimal(sumdr[dgvCell.OwningColumn.DataPropertyName]) + Convert.ToDecimal(dgvCell.Value);
                            }
                        }

                        //sumBox.Text = string.Format("{0}", sumBox.Tag);
                        //sumBox.Invalidate();

                    }
                    //}
                }

            }

            sumdr["sumorder"] = "02";
            sumdr["清单号"] = "合计";
            sumdr["客户"] = "";
            sumdr["税务"] = "";
            ((DataSet)bs.DataSource).Tables[bs.DataMember].Rows.Add(sumdr);
            bs.ResetBindings(true);

        }

       

        private void toolStripButton10_Click(object sender, EventArgs e)
        {
            this.ly_sales_standard_Report_zhongchengTableAdapter.Fill(this.lYSalseMange2.ly_sales_standard_Report_zhongcheng,  this.dateTimePicker3.Value, this.dateTimePicker4.Value.AddDays(1));
            // InitializeApp();

            AddSummationRow_New(ly_sales_standard_Report_zhongchengBindingSource, ly_sales_contract_standard_ReportDataGridView);
            
        }

        private void toolStripButton7_Click(object sender, EventArgs e)
        {
            ExportDataGridviewTOExcell.ExportDataGridview(this.ly_sales_contract_standard_ReportDataGridView, true);
        }

        public void refresh_weekdata(string begindate, string enddate)
        {

            this.dateTimePicker3.Text = begindate;
            this.dateTimePicker4.Text = enddate;

           

            this.ly_sales_standard_Report_zhongchengTableAdapter.Fill(this.lYSalseMange2.ly_sales_standard_Report_zhongcheng, this.dateTimePicker3.Value, this.dateTimePicker4.Value.AddDays(1));
            // InitializeApp();

            AddSummationRow_New(ly_sales_standard_Report_zhongchengBindingSource, ly_sales_contract_standard_ReportDataGridView);



        }


        private void toolStripButton32_Click(object sender, EventArgs e)
        {
            LY_Query_week_ZC queryForm = new LY_Query_week_ZC();

            queryForm.OwnerForm = this;


            queryForm.StartPosition = FormStartPosition.CenterParent;
            queryForm.ShowDialog(this);
        }

        private void toolStripButton8_Click(object sender, EventArgs e)
        {
            if (null == this.ly_sales_contract_standard_ReportDataGridView.CurrentRow) return;

            FilterForm filterForm = new FilterForm();

            //SumQueryDataSet qds;
            //qds = new SumQueryDataSet();

            List<string> ls = new List<string>();
            ls.Add("id");


            filterForm.SetSourceColumns(this.lYSalseMange2.ly_sales_standard_Report_zhongcheng.Columns, ls);

            filterForm.ShowDialog();

            string filterstr = filterForm.GetFilterString();
            if (!string.IsNullOrEmpty(filterstr))
            {

                this.ly_sales_standard_Report_zhongchengBindingSource.Filter = "(" + filterstr + ") or 清单号='合计'";
            }
            AddSummationRow_New(ly_sales_standard_Report_zhongchengBindingSource, ly_sales_contract_standard_ReportDataGridView);

        }

        private void toolStripButton9_Click(object sender, EventArgs e)
        {
            if (null == this.ly_sales_contract_standard_ReportDataGridView.CurrentRow) return;
            SortForm DataSort = new SortForm();

            List<string> ls = new List<string>();
            ls.Add("id");


            DataSort.SetSortColumns(this.lYSalseMange2.ly_sales_standard_Report_zhongcheng.Columns, ls);
            DataSort.ShowDialog();
            this.ly_sales_standard_Report_zhongchengBindingSource.Sort = " sumorder asc," + DataSort.GetSortString();
            AddSummationRow_New(ly_sales_standard_Report_zhongchengBindingSource, ly_sales_contract_standard_ReportDataGridView);
        }

        private void toolStripTextBox2_KeyUp(object sender, KeyEventArgs e)
        {
            string filterString;


            filterString = GetDataGridviewMultiFilterString.DGVMultiFilterString(this.ly_sales_contract_standard_ReportDataGridView, this.toolStripTextBox2.Text);


            this.ly_sales_standard_Report_zhongchengBindingSource.Filter = "(" + filterString + ") or 清单号='合计'";
            AddSummationRow_New(ly_sales_standard_Report_zhongchengBindingSource, ly_sales_contract_standard_ReportDataGridView);
        }

        private void toolStripTextBox2_Enter(object sender, EventArgs e)
        {
            toolStripTextBox2.Text = "";

            this.ly_sales_standard_Report_zhongchengBindingSource.Filter = "";
            AddSummationRow_New(ly_sales_standard_Report_zhongchengBindingSource, ly_sales_contract_standard_ReportDataGridView);
        }

        private void ly_sales_contract_standard_ReportDataGridView_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            DataGridView dgv = sender as DataGridView;

            if (null == dgv.CurrentRow) return;

            string nowcontract_code = dgv.CurrentRow.Cells["清单号"].Value.ToString();

            if ("XS" != nowcontract_code.Substring(0, 2))
            {

                MessageBox.Show("只能修改合同开票日期...", "注意");
                return;

            }


            ///////////////////////////////////////////////////////////////

            if ("开票日期zc" == dgv.CurrentCell.OwningColumn.Name)
            {

               
                ChangeValue queryForm = new ChangeValue();

                queryForm.OldValue = dgv.CurrentCell.Value.ToString();
                queryForm.NewValue = "";
                queryForm.ChangeMode = "datetime";
                queryForm.ShowDialog();



                string updstr;

                if (queryForm.NewValue != "")
                {
                    dgv.CurrentRow.Cells["开票日期zc"].Value = queryForm.NewValue;
                    //dgv.CurrentRow.Cells["开票人"].Value = "宋美彰";
                    updstr = " update ly_sales_contract_main  " +
                                   "  set invoice_date=  '" + queryForm.NewValue + "' where  contract_inner_code='" + nowcontract_code + "'";



                }
                else
                {

                    dgv.CurrentRow.Cells["开票日期zc"].Value = DBNull.Value;
                    //dgv.CurrentRow.Cells["开票人"].Value = DBNull.Value;
                    updstr = " update ly_sales_contract_main  " +
                               "  set invoice_date= null, invoice_people=null where  contract_inner_code='" + nowcontract_code + "'";


                }


                ///////////////////




                SqlConnection sqlConnection1 = new SqlConnection(SQLDatabase.Connectstring);
                SqlCommand cmd = new SqlCommand();

                cmd.CommandText = updstr;
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



                //////////////////////////////



                return;

            }
            ///////////////////////////////////////////////////////////////

            if ("开票人zc" == dgv.CurrentCell.OwningColumn.Name)
            {


                ChangeValue queryForm = new ChangeValue();

                queryForm.OldValue = dgv.CurrentCell.Value.ToString();
                queryForm.NewValue = "";
                queryForm.ChangeMode = "string";
                queryForm.ShowDialog();



                string updstr;

                if (queryForm.NewValue != "")
                {
                    dgv.CurrentRow.Cells["开票人zc"].Value = queryForm.NewValue;
                    //dgv.CurrentRow.Cells["开票人"].Value = "宋美彰";
                    updstr = " update ly_sales_contract_main  " +
                                   "  set invoice_people=  '" + queryForm.NewValue + "' where  contract_inner_code='" + nowcontract_code + "'";



                }
                else
                {

                    dgv.CurrentRow.Cells["开票人zc"].Value = DBNull.Value;
                    //dgv.CurrentRow.Cells["开票人"].Value = DBNull.Value;
                    updstr = " update ly_sales_contract_main  " +
                               "  set invoice_date= null, invoice_people=null where  contract_inner_code='" + nowcontract_code + "'";


                }


                ///////////////////




                SqlConnection sqlConnection1 = new SqlConnection(SQLDatabase.Connectstring);
                SqlCommand cmd = new SqlCommand();

                cmd.CommandText = updstr;
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



                //////////////////////////////



                return;

            }
            ///////////////////////////////////////////////////////////////

            if ("中原开票日期" == dgv.CurrentCell.OwningColumn.Name)
            {


                ChangeValue queryForm = new ChangeValue();

                queryForm.OldValue = dgv.CurrentCell.Value.ToString();
                queryForm.NewValue = "";
                queryForm.ChangeMode = "datetime";
                queryForm.ShowDialog();



                string updstr;

                if (queryForm.NewValue != "")
                {
                    dgv.CurrentRow.Cells["中原开票日期"].Value = queryForm.NewValue;
                    //dgv.CurrentRow.Cells["开票人"].Value = "宋美彰";
                    updstr = " update ly_sales_contract_main  " +
                                   "  set invoice_toZC_date=  '" + queryForm.NewValue + "' where  contract_inner_code='" + nowcontract_code + "'";



                }
                else
                {

                    dgv.CurrentRow.Cells["中原开票日期"].Value = DBNull.Value;
                    //dgv.CurrentRow.Cells["开票人"].Value = DBNull.Value;
                    updstr = " update ly_sales_contract_main  " +
                               "  set invoice_toZC_date= null, invoice_toZC_people=null where  contract_inner_code='" + nowcontract_code + "'";


                }


                ///////////////////




                SqlConnection sqlConnection1 = new SqlConnection(SQLDatabase.Connectstring);
                SqlCommand cmd = new SqlCommand();

                cmd.CommandText = updstr;
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



                //////////////////////////////



                return;

            }
            ///////////////////////////////////////////////////////////////

            if ("中原开票人" == dgv.CurrentCell.OwningColumn.Name)
            {


                ChangeValue queryForm = new ChangeValue();

                queryForm.OldValue = dgv.CurrentCell.Value.ToString();
                queryForm.NewValue = "";
                queryForm.ChangeMode = "string";
                queryForm.ShowDialog();



                string updstr;

                if (queryForm.NewValue != "")
                {
                    dgv.CurrentRow.Cells["中原开票人"].Value = queryForm.NewValue;
                    //dgv.CurrentRow.Cells["开票人"].Value = "宋美彰";
                    updstr = " update ly_sales_contract_main  " +
                                   "  set invoice_toZC_people=  '" + queryForm.NewValue + "' where  contract_inner_code='" + nowcontract_code + "'";



                }
                else
                {

                    dgv.CurrentRow.Cells["中原开票人"].Value = DBNull.Value;
                    //dgv.CurrentRow.Cells["开票人"].Value = DBNull.Value;
                    updstr = " update ly_sales_contract_main  " +
                               "  set invoice_toZC_date= null, invoice_toZC_people=null where  contract_inner_code='" + nowcontract_code + "'";


                }


                ///////////////////




                SqlConnection sqlConnection1 = new SqlConnection(SQLDatabase.Connectstring);
                SqlCommand cmd = new SqlCommand();

                cmd.CommandText = updstr;
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



                //////////////////////////////



                return;

            }

            ////////////////////////////////////////////////////////////////////////
        }

       
     ////////////////////////////////////////////////////////////////

       
    }
}
