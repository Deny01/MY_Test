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
    public partial class LY_Purchase_Approve_cw : Form
    {

        string formState = "View";
        public LY_Purchase_Approve_cw()
        {
            InitializeComponent();
        }

        private void LY_Machine_Load(object sender, EventArgs e)
        {


            this.ly_contract_terms_forpurchaseTableAdapter1.Connection.ConnectionString = SQLDatabase.Connectstring;
            this.ly_company_information_purchaseTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;

           
            this.ly_purchase_prepareforplanTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            this.ly_purchase_contract_main11TableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;

            this.ly_purchase_contract_detailTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
           
            

          

            //this.dateTimePicker1.Text = SQLDatabase.GetNowdate().Date.Year.ToString() + "-01" + "-01";
            this.dateTimePicker1.Text = SQLDatabase.GetNowdate().AddMonths(-3).Date .ToString ();
            this.dateTimePicker2.Text = SQLDatabase.GetNowdate().AddDays(0).Date.ToString();

            this.ly_purchase_contract_main11TableAdapter.Fill(this.lYMaterielRequirements.ly_purchase_contract_main11, this.dateTimePicker1.Value, this.dateTimePicker2.Value.AddDays(1));

            AddSummationRow_New(ly_purchase_contract_main11BindingSource, ly_purchase_contract_mainDataGridView);

        }

       
       

        private void ly_purchase_contract_mainDataGridView_SelectionChanged(object sender, EventArgs e)
        {
            if (null == ly_purchase_contract_mainDataGridView.CurrentRow )
            {
                this.ly_purchase_contract_detailTableAdapter.Fill(this.lYMaterielRequirements.ly_purchase_contract_detail, "");
                //this.lY_MaterielRequirementsPurchaseSupplierTableAdapter.Fill(this.lYMaterielRequirements.LY_MaterielRequirementsPurchaseSupplier, 0, "外购", "");

                return;
            }


            string nowcontractcode = this.ly_purchase_contract_mainDataGridView.CurrentRow .Cells["合同编号"].Value.ToString();
            string nowsuppliercode=this.ly_purchase_contract_mainDataGridView.CurrentRow .Cells["供应商编号6"].Value.ToString();

            



            //this.lY_MaterielRequirementsPurchaseSupplierTableAdapter.Fill(this.lYMaterielRequirements.LY_MaterielRequirementsPurchaseSupplier, parentId, "外购", nowsuppliercode);

            this.ly_purchase_contract_detailTableAdapter.Fill(this.lYMaterielRequirements.ly_purchase_contract_detail, nowcontractcode);

            this.ly_contract_terms_forpurchaseTableAdapter1.Fill(this.lYMaterielRequirements.ly_contract_terms_forpurchase, nowcontractcode);
            this.ly_company_information_purchaseTableAdapter.Fill(this.lYMaterielRequirements.ly_company_information_purchase, nowcontractcode);

        }

       
      
      

     

        private void SaveContract()
        {
            this.ly_purchase_contract_mainDataGridView.EndEdit();
            this.ly_purchase_contract_main11BindingSource.EndEdit();

            this.ly_purchase_contract_main11TableAdapter.Update(this.lYMaterielRequirements.ly_purchase_contract_main11);
            
            

           
        }

      

        private void ly_purchase_contract_mainDataGridView_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {

        }

        //private void bindingNavigatorDeleteItem1_Click(object sender, EventArgs e)
        //{
        //    DataGridView dgv =  ly_purchase_contract_mainDataGridView;

        //    if (null == dgv.CurrentRow) return;

        //    int nowcontractId;
        //    if (null != dgv.CurrentRow)
        //    {
        //        nowcontractId = int.Parse(dgv.CurrentRow.Cells["id6"].Value.ToString());
        //    }
        //    else
        //    {
        //        nowcontractId = 0;
        //    }




        //    if (null != this.ly_material_plan_mainDataGridView.CurrentRow)
        //    {

        //        string planNum = this.ly_material_plan_mainDataGridView.CurrentRow.Cells["计划编号"].Value.ToString();


        //        string nowOwningColumnName = dgv.CurrentCell.OwningColumn.Name;
        //        this.ly_purchase_contract_mainTableAdapter.Fill(this.lYMaterielRequirements.ly_purchase_contract_main, planNum);

        //        this.ly_purchase_contract_mainBindingSource.Position = this.ly_purchase_contract_mainBindingSource.Find("id", nowcontractId);


        //        ly_purchase_contract_mainDataGridView.CurrentCell = ly_purchase_contract_mainDataGridView.CurrentRow.Cells[nowOwningColumnName];
        //    }


        //    if ("True" == dgv.CurrentRow.Cells["批准"].Value.ToString())
        //    {
        //        MessageBox.Show("合同已经审批,不能删除数据...", "注意");
        //        return;

        //    }

        //     string message1 = "删除当前选择合同，继续吗？";
        //    string caption1 = "提示...";
        //    MessageBoxButtons buttons1 = MessageBoxButtons.YesNo;

        //    DialogResult result1;



        //    result1 = MessageBox.Show(message1, caption1, buttons1,
        //    MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

        //    if (result1 == DialogResult.Yes)
        //    {
        //        this.ly_purchase_contract_mainBindingSource.RemoveCurrent();



        //        SaveContract();

        //    }




        //}

        private void ly_purchase_contract_detailDataGridView_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            return;
            
         //   DataGridView dgv = ly_purchase_contract_mainDataGridView;


         //   int nowcontractId;
         //   if (null != dgv.CurrentRow)
         //   {
         //       nowcontractId = int.Parse(dgv.CurrentRow.Cells["id6"].Value.ToString());
         //   }
         //   else
         //   {
         //       nowcontractId = 0;
         //   }


         //   dgv = ly_purchase_contract_detailDataGridView;

         //   int nowcontractdetailId;
         //   if (null != dgv.CurrentRow)
         //   {
         //       nowcontractdetailId = int.Parse(dgv.CurrentRow.Cells["id7"].Value.ToString());
         //   }
         //   else
         //   {
         //       nowcontractdetailId = 0;
         //   }


         //   if (null != this.ly_material_plan_mainDataGridView.CurrentRow)
         //   {

         //       string planNum = this.ly_material_plan_mainDataGridView.CurrentRow.Cells["计划编号"].Value.ToString();


         //       string nowOwningColumnName = dgv.CurrentCell.OwningColumn.Name;
         //       this.ly_purchase_contract_mainTableAdapter.Fill(this.lYMaterielRequirements.ly_purchase_contract_main, planNum);

         //       this.ly_purchase_contract_mainBindingSource.Position = this.ly_purchase_contract_mainBindingSource.Find("id", nowcontractId);
         //       this.ly_purchase_contract_detailBindingSource.Position = this.ly_purchase_contract_detailBindingSource.Find("id", nowcontractdetailId);


         //       ly_purchase_contract_detailDataGridView.CurrentCell = ly_purchase_contract_detailDataGridView.CurrentRow.Cells[nowOwningColumnName];
         //   }

            

         //   ///////////////////////////////////////////////////////

         //   if ("True" == ly_purchase_contract_mainDataGridView.CurrentRow.Cells["批准"].Value.ToString())
         //   {
         //       MessageBox.Show("合同已经审批,不能修改数据...", "注意");
         //       return;

         //   }

         /////////////////////////////////////////////////////////////////

         //   /////////////////////////////
         //   if ("备注7" == dgv.CurrentCell.OwningColumn.Name)
         //   {

         //       ChangeValue queryForm = new ChangeValue();

         //       queryForm.OldValue = dgv.CurrentCell.Value.ToString();
         //       queryForm.NewValue = "";
         //       queryForm.ChangeMode = "longstring";
         //       queryForm.ShowDialog();




         //       if (queryForm.NewValue != "")
         //       {
         //           dgv.CurrentRow.Cells["备注7"].Value = queryForm.NewValue;
         //           //dgv.CurrentRow.Cells["discount_money"].Value = DBNull.Value;

         //           //dgv.CurrentRow.Cells["approve_flag"].Value = "False";
         //           SaveContractdetail();

         //           //CountPlanStru();

         //       }
         //       else
         //       {

         //       }
         //       return;

         //   }
         //   /////////////////////////////
         //   if ("合同单价" == dgv.CurrentCell.OwningColumn.Name)
         //   {

         //       ChangeValue queryForm = new ChangeValue();

         //       queryForm.OldValue = dgv.CurrentCell.Value.ToString();
         //       queryForm.NewValue = "";
         //       queryForm.ChangeMode = "value";
         //       queryForm.ShowDialog();




         //       if (queryForm.NewValue != "")
         //       {
         //           dgv.CurrentRow.Cells["合同单价"].Value = queryForm.NewValue;
         //           //dgv.CurrentRow.Cells["discount_money"].Value = DBNull.Value;

         //           //dgv.CurrentRow.Cells["approve_flag"].Value = "False";
         //           SaveContractdetail();

         //           //CountPlanStru();

         //       }
         //       else
         //       {

         //       }
         //       return;

         //   }

         //   /////////////////////////////
         //   if ("数量7" == dgv.CurrentCell.OwningColumn.Name)
         //   {

         //       ChangeValue queryForm = new ChangeValue();

         //       queryForm.OldValue = dgv.CurrentCell.Value.ToString();
         //       queryForm.NewValue = "";
         //       queryForm.ChangeMode = "value";
         //       queryForm.ShowDialog();

         //       string nowitemno;
         //       if (null != dgv.CurrentRow)
         //       {
         //           nowitemno = dgv.CurrentRow.Cells["物料编号7"].Value.ToString();
         //       }
         //       else
         //       {
         //           nowitemno = "";
         //       }

                

         //         this.lY_MaterielRequirementsPurchaseSupplierBindingSource.Position =   this.lY_MaterielRequirementsPurchaseSupplierBindingSource.Find ("物料编码",nowitemno);


         //         decimal oldqty;
         //         decimal newqty;
         //         decimal notarrangeqty;
         //         if (null != lY_MaterielRequirementsPurchaseSupplierDataGridView.CurrentRow)
         //         {
         //             notarrangeqty = decimal.Parse(lY_MaterielRequirementsPurchaseSupplierDataGridView.CurrentRow.Cells["未排数量8"].Value.ToString());
         //         }
         //         else
         //         {
         //             notarrangeqty = 0;
         //         }


         //         if (queryForm.NewValue != "")
         //         {
         //             if (!string.IsNullOrEmpty(queryForm.OldValue))
         //             {
         //                 oldqty = decimal.Parse(queryForm.OldValue);
         //             }
         //             else
         //             {
         //                 oldqty = 0;
         //             }

         //             if (!string.IsNullOrEmpty(queryForm.NewValue))
         //             {
         //                 newqty = decimal.Parse(queryForm.NewValue);
         //             }
         //             else
         //             {
         //                 newqty = 0;
         //             }
                    

         //             if (newqty > (notarrangeqty + oldqty))
         //             {
         //                 MessageBox.Show("修改数量超出计划需求数量,操作取消...", "注意");
         //                 return;

                      
         //             }
                      
         //             dgv.CurrentRow.Cells["数量7"].Value = queryForm.NewValue;
         //             //dgv.CurrentRow.Cells["discount_money"].Value = DBNull.Value;

         //             //dgv.CurrentRow.Cells["approve_flag"].Value = "False";
         //             SaveContractdetail();

         //             //CountPlanStru();

         //         }
         //         else
         //         {

         //         }
         //       return;

         //   }

        ////////////////////////////////////////////////////////////////

        }

       

      

      

      

        private void 打印PToolStripButton_Click(object sender, EventArgs e)
        {
            if (null == this.ly_purchase_contract_detailDataGridView.CurrentRow) return;



            NewFrm.Show(this); ;

            //this.ly_plan_getmaterialTableAdapter.Fill(this.lYPlanMange.ly_plan_getmaterial, parentId);

            BaseReportView queryForm = new BaseReportView();

            queryForm.Text = "中原精密采购订单";

            queryForm.Printdata = this.lYMaterielRequirements;

            queryForm.PrintCrystalReport = new LY_CaigouDingdan();


            //string selectFormula;

            //selectFormula = "{ly_store_planitemcount.状态}  =   '原料'  and {ly_store_planitemcount.欠料金额}>0 ";
            //queryForm.PrintCrystalReport.DataDefinition.RecordSelectionFormula = selectFormula;

            NewFrm.Hide(this);

            queryForm.ShowDialog();
        }

        private void toolStripButton53_Click(object sender, EventArgs e)
        {
            NewFrm.Show(this);

            
            this.ly_purchase_contract_main11TableAdapter.Fill(this.lYMaterielRequirements.ly_purchase_contract_main11, this.dateTimePicker1.Value, this.dateTimePicker2.Value.AddDays(1));

            AddSummationRow_New(ly_purchase_contract_main11BindingSource, ly_purchase_contract_mainDataGridView);
            NewFrm.Hide(this);
        }

        private void toolStripTextBox1_KeyUp(object sender, KeyEventArgs e)
        {
            string filterString;


            filterString = GetDataGridviewMultiFilterString.DGVMultiFilterString(this.ly_purchase_contract_mainDataGridView, this.toolStripTextBox1.Text);


            this.ly_purchase_contract_main11BindingSource.Filter = "(" + filterString + ") or 合同编号='_合计'";
            AddSummationRow_New(ly_purchase_contract_main11BindingSource, ly_purchase_contract_mainDataGridView);
        }

        private void toolStripTextBox1_Enter(object sender, EventArgs e)
        {
            toolStripTextBox1.Text = "";

            this.ly_purchase_contract_main11BindingSource.Filter = "";
            AddSummationRow_New(ly_purchase_contract_main11BindingSource, ly_purchase_contract_mainDataGridView);
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

        private void AddSummationRow_New(BindingSource bs, DataGridView dgv)
        {
            //InitializeApp();
            //return;

            DataRow sumdr = (((DataSet)bs.DataSource).Tables[bs.DataMember]).NewRow();

            if (-1 != bs.Find("合同编号", "_合计"))
            {
                bs.RemoveAt(bs.Find("合同编号", "_合计"));
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

            //sumdr["sumorder"] = "02";
            sumdr["合同编号"] = "_合计";
            sumdr["id"] =-999;
            //sumdr["税务"] = "";
            ((DataSet)bs.DataSource).Tables[bs.DataMember].Rows.Add(sumdr);
            bs.ResetBindings(true);

        }

       
       
       

       ///////////////////////////////////////////////////
      
    }
}
