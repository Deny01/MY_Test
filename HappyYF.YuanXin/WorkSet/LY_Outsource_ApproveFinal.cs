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
    public partial class LY_Outsource_ApproveFinal : Form
    {

        string formState = "View";
        public LY_Outsource_ApproveFinal()
        {
            InitializeComponent();
        }

        private void LY_Machine_Load(object sender, EventArgs e)
        {

         
            
           
            this.ly_bm0031TableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
           
            this.ly_outsource_order_materialrequisitionTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;

            ly_PrepaymentTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;


            this.ly_outsource_contract_main1TableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;

            this.ly_outsource_contract_detailTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
             this.ly_outsource_contract_detailaddTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
             

            this.ly_purchase_contract_detailPlanTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            this.ly_purchase_contract_detailPlanTableAdapter.SelCommandText =
           " SELECT a.id, a.contract_code, a.itemno, c.supplier_itemno, d.mch, d.jph, d.xhc, d.gg, " +
                 " case when d.purchase_unit is null then d.dw else d.purchase_unit end as dw," +
                "  d.purchase_unit, d.unit_ratio, a.qty, c.item_price, c.purchase_lead_time, " +
                 "   CASE WHEN isnull(a.contract_price, 0) " +
                 "   > 0 THEN a.contract_price * a.purchase_qty ELSE a.purchase_qty * c.item_price END AS " +
                 "    purchase_money, CASE WHEN isnull(a.contract_price, 0)" +
                 "   > 0 THEN a.contract_price ELSE c.item_price END AS contract_price, a.remark," +
                 "   a.purchase_qty, d.xhj, e.in_qty, (CASE WHEN isnull(a.contract_price, 0) " +
                 "   > 0 THEN a.contract_price ELSE c.item_price END) * ISNULL(e.in_qty, 0) " +
                 "   AS instore_money, ISNULL(a.purchase_qty, 0) - ISNULL(e.in_qty, 0) AS notin_qty, " +
                 "   (CASE WHEN isnull(a.contract_price, 0) " +
                 "   > 0 THEN a.contract_price ELSE c.item_price END) * (ISNULL(a.purchase_qty, 0) " +
                 "   - ISNULL(e.in_qty, 0)) AS notinstore_money, b.material_plan_num, d.mch_py, " +
                 "   d.mch_jp, ISNULL(a.id2, 0) AS id2, a.part_name, ISNULL(a.instore_flag, 1) " +
                 "   AS instore_flag, isnull(a.add_code,'000') as add_code " +
          "    FROM ly_outsource_contract_detail AS a LEFT OUTER JOIN " +
         
                 "   ly_outsource_contract_main AS b ON " +
                 "   a.contract_code = b.contract_code LEFT OUTER JOIN " +
                 "   ly_materiel_supplier AS c ON a.itemno = c.itemno AND " +
                 "   b.supplier_code = c.supplier_code AND ISNULL(a.id2, 0) = ISNULL(c.id2, 0) " +
                 "   LEFT OUTER JOIN " +
                 "   ly_inma0010 AS d ON a.itemno = d.wzbh LEFT OUTER JOIN " +
                 "      (SELECT d.contract_code, d.itemno,isnull(d.add_code,'000') as add_code, SUM(b.qty) AS in_qty " +
                 "     FROM ly_outsource_order_inspection AS a LEFT OUTER JOIN " +
                  "          ly_store_in AS b ON a.pruductionInspection_num = b.bill_code       LEFT OUTER JOIN " +
                  "         ly_outsource_order_detail as d  on a.detail_id =d.id left outer join   " +

                
                
                 "            ly_outsource_contract_main AS c ON " +
                 "           d.contract_code = c.contract_code " +
                 "      WHERE (c.material_plan_num = @material_plan_num and isnull(a.instore_flag,0)=1) " +
                 "     GROUP BY d.contract_code, d.itemno,isnull(d.add_code,'000')) AS e ON " +
                 "    a.contract_code = e.contract_code AND a.itemno = e.itemno and isnull(a.add_code,'000') =e.add_code " +
           "   WHERE (b.material_plan_num = @material_plan_num)  order by a.contract_code, a.itemno,isnull(a.add_code,'000') ";

            this.ly_company_information_outsourceTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            this.ly_contract_terms_foroutsourceTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
           

            this.lY_MaterielRequirementsPurchaseTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            this.lY_MaterielRequirementsPurchaseTableAdapter.SelCommandText ="LY_MaterielRequirementsOutsource";

       

            this.dateTimePicker1.Text = SQLDatabase.GetNowdate().AddMonths(-3).Date.ToString();
            this.dateTimePicker2.Text = SQLDatabase.GetNowdate().AddDays(0).Date.ToString();

            NewFrm.Show(this);


            this.ly_outsource_contract_main1TableAdapter.Fill(this.lYOutsourceData.ly_outsource_contract_main1, this.dateTimePicker1.Value, this.dateTimePicker2.Value.AddDays(1));

            AddSummationRow_New(ly_outsource_contract_main1BindingSource, ly_purchase_contract_mainDataGridView);
            NewFrm.Hide(this);

          

        }

      

    
       

       

        private void ClearDatagridview()
        {
           
            this.ly_outsource_order_materialrequisitionTableAdapter.Fill(this.lYOutsourceData.ly_outsource_order_materialrequisition, "");
            this.ly_outsource_contract_detailTableAdapter.Fill(this.lYOutsourceData.ly_outsource_contract_detail, "");

            
            this.ly_machinepart_processTableAdapter.Fill(this.lYMaterielRequirements.ly_machinepart_process, "", 0);
            this.ly_bm0031TableAdapter.Fill(this.lYMaterialMange.ly_bm0031, "");
        }

        private void lY_MaterielRequirementsDataGridView_SelectionChanged(object sender, EventArgs e)
        {
            //if (null == this.lY_MaterielRequirementsDataGridView.CurrentRow) return;

            //this.ly_materiel_supplierTableAdapter.Fill(this.lYMaterielRequirements.ly_materiel_supplier, itemnoToolStripTextBox.Text);
        }

       

        private void ly_purchase_contract_mainDataGridView_SelectionChanged(object sender, EventArgs e)
        {
            if (null == ly_purchase_contract_mainDataGridView.CurrentRow )
            {
                this.ly_outsource_contract_detailTableAdapter.Fill(this.lYOutsourceData .ly_outsource_contract_detail , "");
                this.ly_outsource_contract_detailaddTableAdapter.Fill(this.lYOutsourceData.ly_outsource_contract_detailadd, "");
              
                this.ly_outsource_order_materialrequisitionTableAdapter.Fill(this.lYOutsourceData.ly_outsource_order_materialrequisition, "");
                ly_PrepaymentTableAdapter.Fill(lYMaterielRequirements.ly_Prepayment, "");
                return;
            }


            string nowcontractcode = this.ly_purchase_contract_mainDataGridView.CurrentRow .Cells["合同编号"].Value.ToString();
            string nowsuppliercode=this.ly_purchase_contract_mainDataGridView.CurrentRow .Cells["供应商编号6"].Value.ToString();
            ly_PrepaymentTableAdapter.Fill(lYMaterielRequirements.ly_Prepayment, nowcontractcode);





            this.ly_outsource_contract_detailTableAdapter.Fill(this.lYOutsourceData .ly_outsource_contract_detail , nowcontractcode);
            this.ly_outsource_contract_detailaddTableAdapter.Fill(this.lYOutsourceData.ly_outsource_contract_detailadd, nowcontractcode);
            this.ly_outsource_order_materialrequisitionTableAdapter.Fill(this.lYOutsourceData.ly_outsource_order_materialrequisition, nowcontractcode);

            this.ly_company_information_outsourceTableAdapter.Fill(this.lYOutsourceData.ly_company_information_outsource, nowcontractcode);
            this.ly_contract_terms_foroutsourceTableAdapter.Fill(this.lYOutsourceData.ly_contract_terms_foroutsource, nowcontractcode);
            
        }

       

       

       

       

        private void SaveContract()
        {
            this.ly_purchase_contract_mainDataGridView.EndEdit();
            this.ly_outsource_contract_main1BindingSource.EndEdit();

            this.ly_outsource_contract_main1TableAdapter.Update(this.lYOutsourceData .ly_outsource_contract_main1 );
            
            

           
        }

     

        private void ly_purchase_contract_mainDataGridView_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            DataGridView dgv = sender as DataGridView;


            

            int nowcontractId;
            if (null != dgv.CurrentRow)
            {
                nowcontractId = int.Parse(dgv.CurrentRow.Cells["id6"].Value.ToString());
            }
            else
            {
                nowcontractId = 0;
            }




            //if (null != this.ly_material_plan_mainDataGridView.CurrentRow)
            //{

            //    string planNum = this.ly_material_plan_mainDataGridView.CurrentRow.Cells["计划编号"].Value.ToString();


            //    string nowOwningColumnName = dgv.CurrentCell.OwningColumn.Name;
            //    this.ly_outsource_contract_mainTableAdapter.Fill(this.lYOutsourceData .ly_outsource_contract_main , planNum);

            //    this.ly_outsource_contract_mainBindingSource.Position = this.ly_outsource_contract_mainBindingSource.Find("id", nowcontractId);

               
            //    ly_purchase_contract_mainDataGridView.CurrentCell = ly_purchase_contract_mainDataGridView.CurrentRow.Cells[nowOwningColumnName];
            //}

            dgv = ly_purchase_contract_mainDataGridView;

            ///////////////////////////////////////////////////////

            //if ("True" == dgv.CurrentRow.Cells["批准"].Value.ToString())
            //{
            //    MessageBox.Show("合同已经审批,不能修改数据...", "注意");
            //    return;
            
            //}

            foreach (DataGridViewRow dgr in ly_purchase_contract_detailDataGridView.Rows)
                    {

                        if (string.IsNullOrEmpty( dgr.Cells["合同单价"].Value.ToString().Replace(" ", "")))
                        {
                            MessageBox.Show("合同明细中存在无单价条目,不能通过...", "注意");
                            return;
                        }
                      

                    }

            

            ///////////////////////////////////////////////////////////
            if ("审定" == dgv.CurrentCell.OwningColumn.Name)
            {

                string nowcontractcode = dgv.CurrentRow.Cells["合同编号"].Value.ToString();
                
                if ("True" == dgv.CurrentRow.Cells["审定"].Value.ToString())
                {

                    if (SQLDatabase.updateOutsourceContractmain(nowcontractcode, "审定", "0", DBNull.Value.ToString()))
                    {
                        dgv.CurrentRow.Cells["审定"].Value = "False";
                        dgv.CurrentRow.Cells["审定人"].Value = DBNull.Value;
                    }

                }
                else
                {
                    if (SQLDatabase.updateOutsourceContractmain(nowcontractcode, "审定", "1", SQLDatabase.nowUserName()))
                    {
                        dgv.CurrentRow.Cells["审定"].Value = "True";
                        dgv.CurrentRow.Cells["审定人"].Value = SQLDatabase.nowUserName();
                    }
                }



                //SaveContract();



                return;

            }
            ///////////////////////////////////////////////////////////

            /////////////////////////////////////////////////////////////
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
            /////////////////////////////////////////////////////////////

          

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
            //if ("备注说明" == dgv.CurrentCell.OwningColumn.Name)
            //{

            //    ChangeValue queryForm = new ChangeValue();

            //    queryForm.OldValue = dgv.CurrentCell.Value.ToString();
            //    queryForm.NewValue = "";
            //    queryForm.ChangeMode = "longstring";
            //    queryForm.ShowDialog();




            //    if (queryForm.NewValue != "")
            //    {
            //        dgv.CurrentRow.Cells["备注说明"].Value = queryForm.NewValue;
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

            /////////////////////////////////////////////////////////////
            //if ("供方合同号" == dgv.CurrentCell.OwningColumn.Name)
            //{

            //    ChangeValue queryForm = new ChangeValue();

            //    queryForm.OldValue = dgv.CurrentCell.Value.ToString();
            //    queryForm.NewValue = "";
            //    queryForm.ChangeMode = "string";
            //    queryForm.ShowDialog();




            //    if (queryForm.NewValue != "")
            //    {
            //        dgv.CurrentRow.Cells["供方合同号"].Value = queryForm.NewValue;
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

            /////////////////////////////////////////////////////////////
            //if ("提交" == dgv.CurrentCell.OwningColumn.Name)
            //{

            //    if ("True" == dgv.CurrentRow.Cells["提交"].Value.ToString())
            //    {
            //        dgv.CurrentRow.Cells["提交"].Value = "False";

            //    }
            //    else
            //    {

            //        dgv.CurrentRow.Cells["提交"].Value = "True";
            //    }



            //    SaveContract();





            //    return; 

            //}
            /////////////////////////////////////////////////////////////
        }

       


       
       

        private void ly_purchase_contract_mainDataGridView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
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

       

       

       

        private void toolStripButton53_Click(object sender, EventArgs e)
        {
            NewFrm.Show(this);


            this.ly_outsource_contract_main1TableAdapter.Fill(this.lYOutsourceData.ly_outsource_contract_main1, this.dateTimePicker1.Value, this.dateTimePicker2.Value.AddDays(1));

            AddSummationRow_New(ly_outsource_contract_main1BindingSource, ly_purchase_contract_mainDataGridView);
            NewFrm.Hide(this);
        }

        private void toolStripTextBox3_KeyUp(object sender, KeyEventArgs e)
        {
            string filterString;


            filterString = GetDataGridviewMultiFilterString.DGVMultiFilterString(this.ly_purchase_contract_mainDataGridView, this.toolStripTextBox3.Text);


            this.ly_outsource_contract_main1BindingSource.Filter = "(" + filterString + ") or 合同编号='_合计'";
            AddSummationRow_New(ly_outsource_contract_main1BindingSource, ly_purchase_contract_mainDataGridView);
        }

        private void toolStripTextBox3_Enter(object sender, EventArgs e)
        {
            toolStripTextBox3.Text = "";

            this.ly_outsource_contract_main1BindingSource.Filter = "";
            AddSummationRow_New(ly_outsource_contract_main1BindingSource, ly_purchase_contract_mainDataGridView);
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
            sumdr["id"] = -999;
            //sumdr["税务"] = "";
            ((DataSet)bs.DataSource).Tables[bs.DataMember].Rows.Add(sumdr);
            bs.ResetBindings(true);

        }

      

        private void ly_cg_fk_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (ly_cg_fk.CurrentRow == null) return;
            DataGridView dgv = sender as DataGridView;
            if (ly_cg_fk.CurrentRow.Cells["审批1"].Value.ToString() == "False" || ly_cg_fk.CurrentRow.Cells["审批1"].Value.ToString() == "")
            {
                MessageBox.Show("未审批...", "注意");
                return;
            }
            if (ly_cg_fk.CurrentRow.Cells["付款1"].Value.ToString() == "True")
            {
                MessageBox.Show("已付款...", "注意");
                return;
            }
            if ("审定1" == dgv.CurrentCell.OwningColumn.Name)
            {
                string inspector = this.ly_cg_fk.CurrentRow.Cells["审定人1"].Value.ToString();
                if (!string.IsNullOrEmpty(inspector))
                {
                    if (inspector != SQLDatabase.nowUserName())
                    {
                        MessageBox.Show("请审定人:" + inspector + "操作", "注意");
                        return;
                    }
                }


                if ("True" == dgv.CurrentRow.Cells["审定1"].Value.ToString())
                {
                    dgv.CurrentRow.Cells["审定1"].Value = "False";

                    dgv.CurrentRow.Cells["审定人1"].Value = DBNull.Value;
                    dgv.CurrentRow.Cells["审定日期1"].Value = DBNull.Value;

                }
                else
                {

                    dgv.CurrentRow.Cells["审定1"].Value = "True";

                    dgv.CurrentRow.Cells["审定人1"].Value = SQLDatabase.nowUserName();
                    dgv.CurrentRow.Cells["审定日期1"].Value = SQLDatabase.GetNowdate();

                }

                this.ly_cg_fk.EndEdit();
                this.ly_PrepaymentBindingSource.EndEdit();
                this.ly_PrepaymentTableAdapter.Update(this.lYMaterielRequirements.ly_Prepayment);

                return;

            }
        }




        ///////////////////////////////////////////////////

    }
}
