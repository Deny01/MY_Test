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
    public partial class LY_Outsource_Approve : Form
    {

        string formState = "View";
        public LY_Outsource_Approve()
        {
            InitializeComponent();
        }

        private void LY_Machine_Load(object sender, EventArgs e)
        {

            if (this.tabPage10 == this.tabControl4.SelectedTab)
            {
                this.ly_purchase_contract_detailDataGridView.SelectionChanged += ly_purchase_contract_detailDataGridView_SelectionChanged;
                this.ly_outsource_contract_detailaddDataGridView.SelectionChanged -= ly_outsource_contract_detailaddDataGridView_SelectionChanged;
            }
            if (this.tabPage11 == this.tabControl4.SelectedTab)
            {
                this.ly_purchase_contract_detailDataGridView.SelectionChanged -= ly_purchase_contract_detailDataGridView_SelectionChanged;
                this.ly_outsource_contract_detailaddDataGridView.SelectionChanged += ly_outsource_contract_detailaddDataGridView_SelectionChanged;
            }
            
            this.ly_machinepart_processTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            this.ly_bm0031TableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            this.ly_outsource_order_detail_selTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            this.ly_outsource_order_materialrequisitionTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;

          
            this.ly_materiel_supplierTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            this.ly_purchase_prepareforplanTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            this.ly_outsource_contract_mainTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;

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

            this.lY_MaterielRequirementsPurchaseSupplierTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            this.lY_MaterielRequirementsPurchaseSupplierTableAdapter.SelCommandText = "LY_MaterielRequirementsOutsourceSupplier";
           

            this.ly_material_plan_mainTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            this.ly_material_plan_mainTableAdapter.Fill(this.lYMaterielRequirements.ly_material_plan_main, "SCJH");

            SetFormState("View");

        }

        private void SetFormState(string state)
        {
            // view edit add save cancle

            //return;

            if ("View" == state)
            {
                this.formState = "View";

              



                this.bindingNavigatorMoveFirstItem.Enabled = true;
                this.bindingNavigatorMoveLastItem.Enabled = true;
                this.bindingNavigatorMoveNextItem.Enabled = true;
                this.bindingNavigatorMovePreviousItem.Enabled = true;
                this.bindingNavigatorPositionItem.Enabled = true;





            }
            else
            {
               



                this.bindingNavigatorMoveFirstItem.Enabled = false;
                this.bindingNavigatorMoveLastItem.Enabled = false;
                this.bindingNavigatorMoveNextItem.Enabled = false;
                this.bindingNavigatorMovePreviousItem.Enabled = false;
                this.bindingNavigatorPositionItem.Enabled = false;




            }


        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            this.ly_material_plan_mainTableAdapter.Fill(this.lYMaterielRequirements.ly_material_plan_main, "SCJH");

            SetFormState("View");
        }

       

        private void ly_material_plan_mainDataGridView_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (this.formState == "View")
            {

                if (null != this.ly_material_plan_mainDataGridView.CurrentRow)
                {
                    int parentId = int.Parse(this.ly_material_plan_mainDataGridView.Rows[e.RowIndex].Cells["parentid"].Value.ToString());
                    string planNum = this.ly_material_plan_mainDataGridView.Rows[e.RowIndex].Cells["计划编号"].Value.ToString();

                    //NewFrm.Show(this.ParentForm);
                    this.lY_MaterielRequirementsPurchaseTableAdapter.Fill(this.lYMaterielRequirements.LY_MaterielRequirementsPurchase, parentId, "外协");
                    //NewFrm.Hide(this.ParentForm);

                    this.ly_purchase_prepareforplanTableAdapter.Fill(this.lYMaterielRequirements.ly_purchase_prepareforplan, planNum, "2");

                    this.ly_purchase_contract_mainDataGridView.SelectionChanged -= ly_purchase_contract_mainDataGridView_SelectionChanged;


                    ClearDatagridview();

                    this.ly_outsource_contract_mainTableAdapter.Fill(this .lYOutsourceData .ly_outsource_contract_main, planNum);

                    this.ly_purchase_contract_mainDataGridView.SelectionChanged += ly_purchase_contract_mainDataGridView_SelectionChanged; 

                    //this.ly_purchase_contract_detailPlanDataGridView.SelectionChanged -= ly_purchase_contract_detailPlanDataGridView_SelectionChanged;
                    this.ly_purchase_contract_detailPlanTableAdapter.Fill(this.lYMaterielRequirements.ly_purchase_contract_detailPlan, planNum);
                    //this.ly_purchase_contract_detailPlanDataGridView.SelectionChanged += ly_purchase_contract_detailPlanDataGridView_SelectionChanged;
                }
            }

                  
                 
        }

        private void ClearDatagridview()
        {
            this.lY_MaterielRequirementsPurchaseSupplierTableAdapter.Fill(this.lYMaterielRequirements.LY_MaterielRequirementsPurchaseSupplier, 0, "外协", "");
            this.ly_outsource_order_materialrequisitionTableAdapter.Fill(this.lYOutsourceData.ly_outsource_order_materialrequisition, "");
            this.ly_outsource_contract_detailTableAdapter.Fill(this.lYOutsourceData.ly_outsource_contract_detail, "");

            this.ly_materiel_supplierTableAdapter.Fill(this.lYMaterielRequirements.ly_materiel_supplier, "", 0);
            this.ly_machinepart_processTableAdapter.Fill(this.lYMaterielRequirements.ly_machinepart_process, "", 0);
            this.ly_bm0031TableAdapter.Fill(this.lYMaterialMange.ly_bm0031, "");
        }

        private void lY_MaterielRequirementsDataGridView_SelectionChanged(object sender, EventArgs e)
        {
            //if (null == this.lY_MaterielRequirementsDataGridView.CurrentRow) return;

            //this.ly_materiel_supplierTableAdapter.Fill(this.lYMaterielRequirements.ly_materiel_supplier, itemnoToolStripTextBox.Text);
        }

        private void lY_MaterielRequirementsDataGridView_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView dgv = sender as DataGridView;

            if (null == dgv.CurrentRow)
            {
                this.ly_materiel_supplierTableAdapter.Fill(this.lYMaterielRequirements.ly_materiel_supplier, "",0);
                this.ly_machinepart_processTableAdapter.Fill(this.lYMaterielRequirements.ly_machinepart_process, "", 0);

                this.ly_bm0031TableAdapter.Fill(this.lYMaterialMange.ly_bm0031, "");
                return;
            }


            string itemno = dgv.Rows[e.RowIndex].Cells["物料编码1"].Value.ToString();// lY_MaterielRequirementsDataGridView
            string itemnoname = dgv.Rows[e.RowIndex].Cells["物料名称1"].Value.ToString();
            int  id2 =int .Parse ( dgv.Rows[e.RowIndex].Cells["id2"].Value.ToString());

            //this.groupBox3.Text = itemno +" "+ itemnoname + ":物料供应商";


            this.ly_materiel_supplierTableAdapter.Fill(this.lYMaterielRequirements.ly_materiel_supplier, itemno,id2 );
            this.ly_machinepart_processTableAdapter.Fill(this.lYMaterielRequirements.ly_machinepart_process, itemno,id2 );

            this.ly_bm0031TableAdapter.Fill(this.lYMaterialMange.ly_bm0031, itemno);
        }

        private void ly_purchase_contract_detailDataGridView_SelectionChanged(object sender, EventArgs e)
        {
            DataGridView dgv = sender as DataGridView;

            if (null == dgv.CurrentRow)
            {
                //this.ly_machinepart_process_outsourceTableAdapter.Fill(this.lYOutsourceData.ly_machinepart_process_outsource, "", "",0);
                this.ly_outsource_order_detail_selTableAdapter.Fill(this.lYOutsourceData.ly_outsource_order_detail_sel,"","",0,"");
                return;
            }

            string nowitemno = dgv.CurrentRow.Cells["物料编号7"].Value.ToString();
            string nowcontractcode = dgv.CurrentRow.Cells["合同编号7"].Value.ToString();
            string nowaddcode = dgv.CurrentRow.Cells["加工码7"].Value.ToString();
            int nowid27 =int .Parse ( dgv.CurrentRow.Cells["id27"].Value.ToString());

            this.ly_outsource_order_materialrequisitionBindingSource.Filter = "父件编码='" + nowitemno + "' and 加工码='" + nowaddcode + "'";
            this.ly_outsource_order_detail_selTableAdapter.Fill(this.lYOutsourceData.ly_outsource_order_detail_sel, nowcontractcode, nowitemno, 0, nowaddcode);
        }

        private void ly_purchase_contract_mainDataGridView_SelectionChanged(object sender, EventArgs e)
        {
            if (null == ly_purchase_contract_mainDataGridView.CurrentRow )
            {
                this.ly_outsource_contract_detailTableAdapter.Fill(this.lYOutsourceData .ly_outsource_contract_detail , "");
                this.ly_outsource_contract_detailaddTableAdapter.Fill(this.lYOutsourceData.ly_outsource_contract_detailadd, "");
                this.lY_MaterielRequirementsPurchaseSupplierTableAdapter.Fill(this.lYMaterielRequirements.LY_MaterielRequirementsPurchaseSupplier, 0, "外协", "");
                this.ly_outsource_order_materialrequisitionTableAdapter.Fill(this.lYOutsourceData.ly_outsource_order_materialrequisition, "");

                return;
            }


            string nowcontractcode = this.ly_purchase_contract_mainDataGridView.CurrentRow .Cells["合同编号"].Value.ToString();
            string nowsuppliercode=this.ly_purchase_contract_mainDataGridView.CurrentRow .Cells["供应商编号6"].Value.ToString();

            int parentId = int.Parse(this.ly_material_plan_mainDataGridView.CurrentRow .Cells["parentid"].Value.ToString());
            string planNum = this.ly_material_plan_mainDataGridView.CurrentRow.Cells["计划编号"].Value.ToString();



            this.lY_MaterielRequirementsPurchaseSupplierTableAdapter.Fill(this.lYMaterielRequirements.LY_MaterielRequirementsPurchaseSupplier, parentId, "外协", nowsuppliercode);

            this.ly_outsource_contract_detailTableAdapter.Fill(this.lYOutsourceData .ly_outsource_contract_detail , nowcontractcode);
            this.ly_outsource_contract_detailaddTableAdapter.Fill(this.lYOutsourceData.ly_outsource_contract_detailadd, nowcontractcode);
            this.ly_outsource_order_materialrequisitionTableAdapter.Fill(this.lYOutsourceData.ly_outsource_order_materialrequisition, nowcontractcode);

            this.ly_company_information_outsourceTableAdapter.Fill(this.lYOutsourceData.ly_company_information_outsource, nowcontractcode);
            this.ly_contract_terms_foroutsourceTableAdapter.Fill(this.lYOutsourceData.ly_contract_terms_foroutsource, nowcontractcode);

            
        }

        private void ly_materiel_supplierDataGridView_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {

            return;
            if (null == ly_materiel_supplierDataGridView.CurrentRow) return;

            int parentId = int.Parse(this.ly_material_plan_mainDataGridView.CurrentRow.Cells["parentid"].Value.ToString());
            
            string nowsupplierCode = this.ly_materiel_supplierDataGridView.CurrentRow.Cells["供应商编码2"].Value.ToString();

            string nowsplanNum = this.lY_MaterielRequirementsDataGridView.CurrentRow.Cells["计划编号1"].Value.ToString();

            string nowitemno = this.lY_MaterielRequirementsDataGridView.CurrentRow.Cells["物料编码1"].Value.ToString();

            string nowQty = this.lY_MaterielRequirementsDataGridView.CurrentRow.Cells["未排数量1"].Value.ToString();

            string nowid2 = this.lY_MaterielRequirementsDataGridView.CurrentRow.Cells["id2"].Value.ToString();

            string nowpartname = this.lY_MaterielRequirementsDataGridView.CurrentRow.Cells["方式"].Value.ToString();

            string nowinstoreflag = this.lY_MaterielRequirementsDataGridView.CurrentRow.Cells["入库"].Value.ToString();


            if ("True" == nowinstoreflag)
            {
                nowinstoreflag = "1";
            }
            else
            {
                nowinstoreflag = "0";
            }


          
            int hadarrenged = this.ly_purchase_prepareforplanBindingSource.Find("findcode", nowsupplierCode + nowitemno);

            if (0 <= hadarrenged)
            {
                MessageBox.Show("该物料供应商已有选择,不能重复添加", "注意");
                return;
            
            }

            if (0 >= decimal.Parse(nowQty))
            {
                MessageBox.Show("未排数量为0,操作取消", "注意");
                return;

            }



            string insStr = " INSERT INTO ly_purchase_prepareforplan  " +
              "( plan_num,sortcode,itemno,qty,purchase_qty,supplier_code,id2,part_name,instore_flag) " +
              " values ('" + nowsplanNum + "','" + "2" + "','" + nowitemno + "'," + nowQty + "," + nowQty + ",'" + nowsupplierCode + "'," + nowid2 + ",'" + nowpartname + "'," + nowinstoreflag + " )";



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

                this.ly_purchase_prepareforplanTableAdapter.Fill(this.lYMaterielRequirements.ly_purchase_prepareforplan, nowsplanNum, "2");
                this.ly_purchase_prepareforplanBindingSource.Position = this.ly_purchase_prepareforplanBindingSource.Find("findcode", nowsupplierCode + nowitemno);
               
               

                //NewFrm.Show(this.ParentForm);
                this.lY_MaterielRequirementsPurchaseTableAdapter.Fill(this.lYMaterielRequirements.LY_MaterielRequirementsPurchase, parentId, "外协");
                //NewFrm.Hide(this.ParentForm);


               
                this.lY_MaterielRequirementsPurchaseBindingSource.Position = this.lY_MaterielRequirementsPurchaseBindingSource.Find("物料编码", nowitemno);
               
        }

       

        private void ly_purchase_prepareforplanDataGridView_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            //if (CheckFinished()) return;
            DataGridView dgv = sender as DataGridView;

            if ("采购数量0" == dgv.CurrentCell.OwningColumn.Name)
            {

                ChangeValue queryForm = new ChangeValue();

                queryForm.OldValue = dgv.CurrentCell.Value.ToString();
                queryForm.NewValue = "";
                queryForm.ChangeMode = "value";
                queryForm.ShowDialog();




                if (queryForm.NewValue != "")
                {

                    /////////////////////////////////////////////////////////////////////////////////////////////

                    decimal oldQty = decimal.Parse(queryForm.OldValue);

                    decimal newQty = decimal.Parse(queryForm.NewValue);

                    decimal nowQty = decimal.Parse(this.lY_MaterielRequirementsDataGridView.CurrentRow.Cells["未排数量1"].Value.ToString());

                    //if (newQty > (oldQty + nowQty))
                    //{
                    //    MessageBox.Show("修改数量超出需求数量,操作取消", "注意");
                    //    return;

                    //}

                    dgv.CurrentRow.Cells["采购数量0"].Value = queryForm.NewValue;


                    SaveChanged();

                    return;

                }
            }


            ///////////////////////////////////////////////////////
        }

        private void SaveChanged()
        {
            this.ly_purchase_prepareforplanDataGridView.EndEdit();
            this.ly_purchase_prepareforplanBindingSource.EndEdit();

            this.ly_purchase_prepareforplanTableAdapter.Update(this.lYMaterielRequirements.ly_purchase_prepareforplan);

            RefreshData();
        }

        private void RefreshData()
        {

           
          
            


            int parentId = int.Parse(this.ly_material_plan_mainDataGridView.CurrentRow.Cells["parentid"].Value.ToString());
            string planNum = this.ly_material_plan_mainDataGridView.CurrentRow.Cells["计划编号"].Value.ToString();

            string nowitemno;
            if (null != this.ly_purchase_prepareforplanDataGridView.CurrentRow)
            {
                nowitemno = this.ly_purchase_prepareforplanDataGridView.CurrentRow.Cells["物料编号"].Value.ToString();
            }
            else
            {
                nowitemno = "";
            }

            


            int nowcontractId;
            if (null != ly_purchase_contract_mainDataGridView.CurrentRow)
            {
                nowcontractId = int.Parse(ly_purchase_contract_mainDataGridView.CurrentRow.Cells["id6"].Value.ToString());
            }
            else
            {
                nowcontractId = 0;
            }

            int nowcontractdetailId;
            if (null != ly_purchase_contract_detailDataGridView.CurrentRow)
            {
                nowcontractdetailId = int.Parse(ly_purchase_contract_detailDataGridView.CurrentRow.Cells["id7"].Value.ToString());
            }
            else
            {
                nowcontractdetailId = 0;
            }



            string  nowcontractdetailcode;
            if (null != ly_purchase_contract_detailDataGridView.CurrentRow)
            {
                nowcontractdetailcode = ly_purchase_contract_detailDataGridView.CurrentRow.Cells["物料编号7"].Value.ToString();
            }
            else
            {
                nowcontractdetailcode = "";
            }







            this.lY_MaterielRequirementsPurchaseTableAdapter.Fill(this.lYMaterielRequirements.LY_MaterielRequirementsPurchase, parentId, "外协");


            this.ly_purchase_prepareforplanTableAdapter.Fill(this.lYMaterielRequirements.ly_purchase_prepareforplan, planNum, "2");
             this.lY_MaterielRequirementsPurchaseBindingSource.Position = this.lY_MaterielRequirementsPurchaseBindingSource.Find("物料编码", nowitemno);
           
             this.ly_outsource_contract_mainTableAdapter.Fill(this.lYOutsourceData .ly_outsource_contract_main, planNum);
             this.ly_outsource_contract_mainBindingSource.Position = this.ly_outsource_contract_mainBindingSource.Find("id", nowcontractId);
            this.ly_outsource_contract_detailBindingSource.Position = this.ly_outsource_contract_detailBindingSource.Find("id", nowcontractdetailId);
            this.ly_outsource_contract_detailBindingSource.Position = this.ly_outsource_contract_detailBindingSource.Find("物料编号", nowcontractdetailcode);
           
        }

        private void ly_purchase_prepareforplanDataGridView_SelectionChanged(object sender, EventArgs e)
        {
            DataGridView dgv = sender as DataGridView;

            if (null == dgv.CurrentRow)
            {
                return;
            
            }

            string nowitemno =dgv.CurrentRow.Cells["物料编号"].Value.ToString();
            string nowsupplier = dgv.CurrentRow.Cells["供应商编号"].Value.ToString();

            this.lY_MaterielRequirementsPurchaseBindingSource.Position = this.lY_MaterielRequirementsPurchaseBindingSource.Find("物料编码", nowitemno);
            this.ly_materiel_supplierBindingSource.Position = this.ly_materiel_supplierBindingSource.Find("供应商编码", nowsupplier);
        }

        private void bindingNavigatorDeleteItem_Click(object sender, EventArgs e)
        {
            if (null == this.ly_purchase_prepareforplanDataGridView.CurrentRow) return;

           

            string message1 = "当前记录将被删除，继续吗？";
            string caption1 = "提示...";
            MessageBoxButtons buttons1 = MessageBoxButtons.YesNo;

            DialogResult result1;



            result1 = MessageBox.Show(message1, caption1, buttons1,
            MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

            if (result1 == DialogResult.Yes)
            {

                this.ly_purchase_prepareforplanBindingSource.RemoveCurrent();



                SaveChanged();




            }
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            if (null == this.ly_purchase_prepareforplanDataGridView.CurrentRow) return;



            string message1 = "当前选择记录生成采购合同，继续吗？";
            string caption1 = "提示...";
            MessageBoxButtons buttons1 = MessageBoxButtons.YesNo;

            DialogResult result1;



            result1 = MessageBox.Show(message1, caption1, buttons1,
            MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

            if (result1 == DialogResult.Yes)
            {

                

              
                string planNum = this.ly_material_plan_mainDataGridView.CurrentRow.Cells["计划编号"].Value.ToString();

                NewFrm.Show(this);

                SqlConnection sqlConnection1 = new SqlConnection(SQLDatabase.Connectstring);
                SqlCommand cmd = new SqlCommand();



                cmd.Parameters.Add("@plan_num", SqlDbType.VarChar);
                cmd.Parameters["@plan_num"].Value = planNum;

                cmd.Parameters.Add("@buyer", SqlDbType.VarChar);
                cmd.Parameters["@buyer"].Value = SQLDatabase.nowUserName();


                cmd.CommandText = "LY_make_outsource_contract";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = sqlConnection1;
                cmd.CommandTimeout = 0;

                sqlConnection1.Open();
                cmd.ExecuteNonQuery();
                sqlConnection1.Close();


                //ly_material_plan_explodeTableAdapter.Fill(this.lYPlanMange.ly_material_plan_explode, parentId);
                //this.ly_store_planitemcountTableAdapter.Fill(this.lYPlanMange.ly_store_planitemcount, parentId);
                //this.ly_plan_getmaterial_departmentTableAdapter.Fill(this.lYPlanMange.ly_plan_getmaterial_department , planNum);

                NewFrm.Hide(this);

                int parentId = int.Parse(this.ly_material_plan_mainDataGridView.CurrentRow.Cells["parentid"].Value.ToString());
                //string planNum = this.ly_material_plan_mainDataGridView.Rows[e.RowIndex].Cells["计划编号"].Value.ToString();

                //NewFrm.Show(this.ParentForm);
                this.lY_MaterielRequirementsPurchaseTableAdapter.Fill(this.lYMaterielRequirements.LY_MaterielRequirementsPurchase, parentId, "外协");
                //NewFrm.Hide(this.ParentForm);

                this.ly_purchase_prepareforplanTableAdapter.Fill(this.lYMaterielRequirements.ly_purchase_prepareforplan, planNum, "2");

                this.ly_outsource_contract_mainTableAdapter.Fill(this.lYOutsourceData .ly_outsource_contract_main , planNum);


                this.tabControl2.SelectedTab = this.tabPage3;


            }
        }

       

        private void SaveContract()
        {
            this.ly_purchase_contract_mainDataGridView.EndEdit();
            this.ly_outsource_contract_mainBindingSource.EndEdit();

            this.ly_outsource_contract_mainTableAdapter.Update(this.lYOutsourceData .ly_outsource_contract_main );
            
            

            RefreshData();
        }

        private void SaveContractdetail()
        {
            this.ly_purchase_contract_detailDataGridView.EndEdit();
            this.ly_outsource_contract_detailBindingSource.EndEdit();

            this.ly_outsource_contract_detailTableAdapter.Update(this.lYOutsourceData.ly_outsource_contract_detail);

            RefreshData();
        }

        private void ly_purchase_contract_mainDataGridView_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            DataGridView dgv = sender as DataGridView;


            /////////////////////////////////////////////////////////////
            //if ("批准" == dgv.CurrentCell.OwningColumn.Name)
            //{

            //    if ("True" == dgv.CurrentRow.Cells["批准"].Value.ToString())
            //    {
            //        dgv.CurrentRow.Cells["批准"].Value = "False";

            //    }
            //    else
            //    {

            //        dgv.CurrentRow.Cells["批准"].Value = "True";
            //    }



            //    SaveContract();



            //    return;

            //}
            /////////////////////////////////////////////////////////////


            int nowcontractId;
            if (null != dgv.CurrentRow)
            {
                nowcontractId = int.Parse(dgv.CurrentRow.Cells["id6"].Value.ToString());
            }
            else
            {
                nowcontractId = 0;
            }




            if (null != this.ly_material_plan_mainDataGridView.CurrentRow)
            {

                string planNum = this.ly_material_plan_mainDataGridView.CurrentRow.Cells["计划编号"].Value.ToString();


                string nowOwningColumnName = dgv.CurrentCell.OwningColumn.Name;
                this.ly_outsource_contract_mainTableAdapter.Fill(this.lYOutsourceData .ly_outsource_contract_main , planNum);

                this.ly_outsource_contract_mainBindingSource.Position = this.ly_outsource_contract_mainBindingSource.Find("id", nowcontractId);

               
                ly_purchase_contract_mainDataGridView.CurrentCell = ly_purchase_contract_mainDataGridView.CurrentRow.Cells[nowOwningColumnName];
            }

            dgv = ly_purchase_contract_mainDataGridView;

            ///////////////////////////////////////////////////////

            if ("True" == dgv.CurrentRow.Cells["审定"].Value.ToString())
            {
                MessageBox.Show("合同已经审定,不能修改数据...", "注意");
                return;

            }

            foreach (DataGridViewRow dgr in ly_purchase_contract_detailDataGridView.Rows)
                    {

                        if (string.IsNullOrEmpty( dgr.Cells["合同单价"].Value.ToString().Replace(" ", "")))
                        {
                            MessageBox.Show("合同明细中存在无单价条目,不能通过...", "注意");
                            return;
                        }
                      

                    }

            

            ///////////////////////////////////////////////////////////
            if ("批准" == dgv.CurrentCell.OwningColumn.Name)
            {

                if ("True" == dgv.CurrentRow.Cells["批准"].Value.ToString())
                {
                    dgv.CurrentRow.Cells["批准"].Value = "False";
                    dgv.CurrentRow.Cells["审批人"].Value = DBNull.Value;

                }
                else
                {

                    dgv.CurrentRow.Cells["批准"].Value = "True";
                    dgv.CurrentRow.Cells["审批人"].Value = SQLDatabase.nowUserName();
                }



                SaveContract();



                return;

            }
            ///////////////////////////////////////////////////////////

            ///////////////////////////////////////////////////////////
            if ("审核" == dgv.CurrentCell.OwningColumn.Name)
            {

                if ("True" == dgv.CurrentRow.Cells["审核"].Value.ToString())
                {
                    dgv.CurrentRow.Cells["审核"].Value = "False";
                    dgv.CurrentRow.Cells["审核人"].Value = DBNull.Value;

                }
                else
                {

                    dgv.CurrentRow.Cells["审核"].Value = "True";
                    dgv.CurrentRow.Cells["审核人"].Value = SQLDatabase.nowUserName();
                }



                SaveContract();



                return;

            }
            ///////////////////////////////////////////////////////////

          

            if ("签订日期" == dgv.CurrentCell.OwningColumn.Name)
            {

                ChangeValue queryForm = new ChangeValue();

                queryForm.OldValue = dgv.CurrentCell.Value.ToString();
                queryForm.NewValue = "";
                queryForm.ChangeMode = "datetime";
                queryForm.ShowDialog();




                if (queryForm.NewValue != "")
                {
                    dgv.CurrentRow.Cells["签订日期"].Value = queryForm.NewValue;
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
            if ("备注说明" == dgv.CurrentCell.OwningColumn.Name)
            {

                ChangeValue queryForm = new ChangeValue();

                queryForm.OldValue = dgv.CurrentCell.Value.ToString();
                queryForm.NewValue = "";
                queryForm.ChangeMode = "longstring";
                queryForm.ShowDialog();




                if (queryForm.NewValue != "")
                {
                    dgv.CurrentRow.Cells["备注说明"].Value = queryForm.NewValue;
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

            ///////////////////////////////////////////////////////////
            if ("供方合同号" == dgv.CurrentCell.OwningColumn.Name)
            {

                ChangeValue queryForm = new ChangeValue();

                queryForm.OldValue = dgv.CurrentCell.Value.ToString();
                queryForm.NewValue = "";
                queryForm.ChangeMode = "string";
                queryForm.ShowDialog();




                if (queryForm.NewValue != "")
                {
                    dgv.CurrentRow.Cells["供方合同号"].Value = queryForm.NewValue;
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

            ///////////////////////////////////////////////////////////
            if ("提交" == dgv.CurrentCell.OwningColumn.Name)
            {

                if ("True" == dgv.CurrentRow.Cells["提交"].Value.ToString())
                {
                    dgv.CurrentRow.Cells["提交"].Value = "False";

                }
                else
                {

                    dgv.CurrentRow.Cells["提交"].Value = "True";
                }



                SaveContract();





                return; 

            }
            ///////////////////////////////////////////////////////////
        }

        private void bindingNavigatorDeleteItem1_Click(object sender, EventArgs e)
        {
            DataGridView dgv =  ly_purchase_contract_mainDataGridView;

            if (null == dgv.CurrentRow) return;

            int nowcontractId;
            if (null != dgv.CurrentRow)
            {
                nowcontractId = int.Parse(dgv.CurrentRow.Cells["id6"].Value.ToString());
            }
            else
            {
                nowcontractId = 0;
            }




            if (null != this.ly_material_plan_mainDataGridView.CurrentRow)
            {

                string planNum = this.ly_material_plan_mainDataGridView.CurrentRow.Cells["计划编号"].Value.ToString();


                string nowOwningColumnName = dgv.CurrentCell.OwningColumn.Name;
                this.ly_outsource_contract_mainTableAdapter.Fill(this.lYOutsourceData .ly_outsource_contract_main , planNum);

                this.ly_outsource_contract_mainBindingSource.Position = this.ly_outsource_contract_mainBindingSource.Find("id", nowcontractId);


                ly_purchase_contract_mainDataGridView.CurrentCell = ly_purchase_contract_mainDataGridView.CurrentRow.Cells[nowOwningColumnName];
            }
            

            if ("True" == dgv.CurrentRow.Cells["批准"].Value.ToString())
            {
                MessageBox.Show("合同已经审批,不能删除数据...", "注意");
                return;

            }

             string message1 = "删除当前选择合同，继续吗？";
            string caption1 = "提示...";
            MessageBoxButtons buttons1 = MessageBoxButtons.YesNo;

            DialogResult result1;



            result1 = MessageBox.Show(message1, caption1, buttons1,
            MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

            if (result1 == DialogResult.Yes)
            {
                this.ly_outsource_contract_mainBindingSource.RemoveCurrent();



                SaveContract();

            }



           
        }

        private void ly_purchase_contract_detailDataGridView_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            return;
            
            DataGridView dgv = ly_purchase_contract_mainDataGridView;


            int nowcontractId;
            if (null != dgv.CurrentRow)
            {
                nowcontractId = int.Parse(dgv.CurrentRow.Cells["id6"].Value.ToString());
            }
            else
            {
                nowcontractId = 0;
            }


            dgv = ly_purchase_contract_detailDataGridView;

            int nowcontractdetailId;
            if (null != dgv.CurrentRow)
            {
                nowcontractdetailId = int.Parse(dgv.CurrentRow.Cells["id7"].Value.ToString());
            }
            else
            {
                nowcontractdetailId = 0;
            }


            if (null != this.ly_material_plan_mainDataGridView.CurrentRow)
            {

                string planNum = this.ly_material_plan_mainDataGridView.CurrentRow.Cells["计划编号"].Value.ToString();


                string nowOwningColumnName = dgv.CurrentCell.OwningColumn.Name;
                this.ly_outsource_contract_mainTableAdapter.Fill(this.lYOutsourceData .ly_outsource_contract_main , planNum);

                this.ly_outsource_contract_mainBindingSource.Position = this.ly_outsource_contract_mainBindingSource.Find("id", nowcontractId);
                this.ly_outsource_contract_detailBindingSource.Position = this.ly_outsource_contract_detailBindingSource.Find("id", nowcontractdetailId);


                ly_purchase_contract_detailDataGridView.CurrentCell = ly_purchase_contract_detailDataGridView.CurrentRow.Cells[nowOwningColumnName];
            }

            

            ///////////////////////////////////////////////////////

            if ("True" == ly_purchase_contract_mainDataGridView.CurrentRow.Cells["批准"].Value.ToString())
            {
                MessageBox.Show("合同已经审批,不能修改数据...", "注意");
                return;

            }

         ///////////////////////////////////////////////////////////////

            /////////////////////////////
            if ("备注7" == dgv.CurrentCell.OwningColumn.Name)
            {

                ChangeValue queryForm = new ChangeValue();

                queryForm.OldValue = dgv.CurrentCell.Value.ToString();
                queryForm.NewValue = "";
                queryForm.ChangeMode = "longstring";
                queryForm.ShowDialog();




                if (queryForm.NewValue != "")
                {
                    dgv.CurrentRow.Cells["备注7"].Value = queryForm.NewValue;
                    //dgv.CurrentRow.Cells["discount_money"].Value = DBNull.Value;

                    //dgv.CurrentRow.Cells["approve_flag"].Value = "False";
                    SaveContractdetail();

                    //CountPlanStru();

                }
                else
                {

                }
                return;

            }
            /////////////////////////////
            if ("合同单价" == dgv.CurrentCell.OwningColumn.Name)
            {

                ChangeValue queryForm = new ChangeValue();

                queryForm.OldValue = dgv.CurrentCell.Value.ToString();
                queryForm.NewValue = "";
                queryForm.ChangeMode = "value";
                queryForm.ShowDialog();




                if (queryForm.NewValue != "")
                {
                    dgv.CurrentRow.Cells["合同单价"].Value = queryForm.NewValue;
                    //dgv.CurrentRow.Cells["discount_money"].Value = DBNull.Value;

                    //dgv.CurrentRow.Cells["approve_flag"].Value = "False";
                    SaveContractdetail();

                    //CountPlanStru();

                }
                else
                {

                }
                return;

            }

            /////////////////////////////
            if ("采购数量" == dgv.CurrentCell.OwningColumn.Name)
            {

                ChangeValue queryForm = new ChangeValue();

                queryForm.OldValue = dgv.CurrentCell.Value.ToString();
                queryForm.NewValue = "";
                queryForm.ChangeMode = "value";
                queryForm.ShowDialog();

                string nowitemno;
                if (null != dgv.CurrentRow)
                {
                    nowitemno = dgv.CurrentRow.Cells["物料编号7"].Value.ToString();
                }
                else
                {
                    nowitemno = "";
                }

                

                  this.lY_MaterielRequirementsPurchaseSupplierBindingSource.Position =   this.lY_MaterielRequirementsPurchaseSupplierBindingSource.Find ("物料编码",nowitemno);


                  decimal oldqty;
                  decimal newqty;
                  decimal notarrangeqty;
                  if (null != lY_MaterielRequirementsPurchaseSupplierDataGridView.CurrentRow)
                  {
                      notarrangeqty = decimal.Parse(lY_MaterielRequirementsPurchaseSupplierDataGridView.CurrentRow.Cells["未排数量8"].Value.ToString());
                  }
                  else
                  {
                      notarrangeqty = 0;
                  }


                  if (queryForm.NewValue != "")
                  {
                      if (!string.IsNullOrEmpty(queryForm.OldValue))
                      {
                          oldqty = decimal.Parse(queryForm.OldValue);
                      }
                      else
                      {
                          oldqty = 0;
                      }

                      if (!string.IsNullOrEmpty(queryForm.NewValue))
                      {
                          newqty = decimal.Parse(queryForm.NewValue);
                      }
                      else
                      {
                          newqty = 0;
                      }
                    

                      //if (newqty > (notarrangeqty + oldqty))
                      //{
                      //    MessageBox.Show("修改数量超出计划需求数量,操作取消...", "注意");
                      //    return;

                      
                      //}

                      dgv.CurrentRow.Cells["采购数量"].Value = queryForm.NewValue;
                      //dgv.CurrentRow.Cells["discount_money"].Value = DBNull.Value;

                      //dgv.CurrentRow.Cells["approve_flag"].Value = "False";
                      SaveContractdetail();

                      //CountPlanStru();

                  }
                  else
                  {

                  }
                return;

            }

        ////////////////////////////////////////////////////////////////

        }

        private void lY_MaterielRequirementsPurchaseSupplierDataGridView_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {

            return;
            DataGridView dgv = lY_MaterielRequirementsPurchaseSupplierDataGridView;

            if ("未排数量8" == dgv.CurrentCell.OwningColumn.Name)
            {
                decimal notarrangeqty;
                if (null != dgv.CurrentRow)
                {
                    notarrangeqty = decimal.Parse(dgv.CurrentRow.Cells["未排数量8"].Value.ToString());
                }
                else
                {
                    notarrangeqty = 0;
                }

                if (0 >= notarrangeqty)
                {
                    MessageBox.Show("未排数量为0,操作取消", "注意");
                    return;
                
                }

                string nowitemno;
                if (null != dgv.CurrentRow)
                {
                    nowitemno = dgv.CurrentRow.Cells["物料编码8"].Value.ToString();
                }
                else
                {
                    nowitemno = "";
                }

                string nowaddcode="000";

                int nowid2;
                if (null != dgv.CurrentRow)
                {
                    nowid2 =int .Parse ( dgv.CurrentRow.Cells["id28"].Value.ToString());
                }
                else
                {
                    nowid2 = 0;
                }

                string nowinstoreflag;
                if (null != dgv.CurrentRow)
                {
                    nowinstoreflag = dgv.CurrentRow.Cells["入库8"].Value.ToString();
                }
                else
                {
                    nowinstoreflag = "";
                }
                string nowpartname;
                if (null != dgv.CurrentRow)
                {
                    nowpartname = dgv.CurrentRow.Cells["方式8"].Value.ToString();
                }
                else
                {
                    nowpartname = "";
                }

                int hadarranged = ly_outsource_contract_detailBindingSource.Find("物料编号", nowitemno);

                if (-1 < hadarranged)
                {
                    MessageBox.Show("当前合同已经安排该物料采购,修改合同中该物料数量即可...", "注意");
                    return;

                }
                else
                {
                    string nowcontractcode;
                    if (null != ly_purchase_contract_mainDataGridView.CurrentRow)
                    {
                       nowcontractcode = this.ly_purchase_contract_mainDataGridView.CurrentRow.Cells["合同编号"].Value.ToString();
                    }
                    else
                    {
                        nowcontractcode = "";
                    }
                    this.ly_purchase_contract_detailDataGridView.SelectionChanged -= ly_purchase_contract_detailDataGridView_SelectionChanged;
                    this.ly_outsource_contract_detailBindingSource.AddNew();
                    this.ly_purchase_contract_detailDataGridView.SelectionChanged += ly_purchase_contract_detailDataGridView_SelectionChanged;

                    this.ly_purchase_contract_detailDataGridView.CurrentRow.Cells["合同编号7"].Value = nowcontractcode;
                    this.ly_purchase_contract_detailDataGridView.CurrentRow.Cells["物料编号7"].Value = nowitemno;
                    this.ly_purchase_contract_detailDataGridView.CurrentRow.Cells["数量7"].Value = notarrangeqty;
                    this.ly_purchase_contract_detailDataGridView.CurrentRow.Cells["采购数量"].Value = notarrangeqty;

                    this.ly_purchase_contract_detailDataGridView.CurrentRow.Cells["id27"].Value = nowid2 ;
                    this.ly_purchase_contract_detailDataGridView.CurrentRow.Cells["加工码7"].Value = nowaddcode ;
                    this.ly_purchase_contract_detailDataGridView.CurrentRow.Cells["入库7"].Value = nowinstoreflag;
                    this.ly_purchase_contract_detailDataGridView.CurrentRow.Cells["方式7"].Value = nowpartname;

                    SaveContractdetail();
                
                }


            }
        }

        private void bindingNavigatorDeleteItem2_Click(object sender, EventArgs e)
        {
            DataGridView dgv = ly_purchase_contract_mainDataGridView;


            int nowcontractId;
            if (null != dgv.CurrentRow)
            {
                nowcontractId = int.Parse(dgv.CurrentRow.Cells["id6"].Value.ToString());
            }
            else
            {
                nowcontractId = 0;
            }


            dgv = ly_purchase_contract_detailDataGridView;

            int nowcontractdetailId;
            if (null != dgv.CurrentRow)
            {
                nowcontractdetailId = int.Parse(dgv.CurrentRow.Cells["id7"].Value.ToString());
            }
            else
            {
                nowcontractdetailId = 0;
            }


            if (null != this.ly_material_plan_mainDataGridView.CurrentRow)
            {

                string planNum = this.ly_material_plan_mainDataGridView.CurrentRow.Cells["计划编号"].Value.ToString();


                string nowOwningColumnName = dgv.CurrentCell.OwningColumn.Name;
                this.ly_outsource_contract_mainTableAdapter.Fill(this.lYOutsourceData .ly_outsource_contract_main , planNum);

                this.ly_outsource_contract_mainBindingSource.Position = this.ly_outsource_contract_mainBindingSource.Find("id", nowcontractId);
                this.ly_outsource_contract_detailBindingSource.Position = this.ly_outsource_contract_detailBindingSource.Find("id", nowcontractdetailId);


                ly_purchase_contract_detailDataGridView.CurrentCell = ly_purchase_contract_detailDataGridView.CurrentRow.Cells[nowOwningColumnName];
            }



            ///////////////////////////////////////////////////////

            if ("True" == ly_purchase_contract_mainDataGridView.CurrentRow.Cells["批准"].Value.ToString())
            {
                MessageBox.Show("合同已经审批,不能修改数据...", "注意");
                return;

            }

            ///////////////////////////////////////////////////////////////

            string message1 = "删除当前选择合同物料，继续吗？";
            string caption1 = "提示...";
            MessageBoxButtons buttons1 = MessageBoxButtons.YesNo;

            DialogResult result1;



            result1 = MessageBox.Show(message1, caption1, buttons1,
            MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

            if (result1 == DialogResult.Yes)
            {
                this.ly_outsource_contract_detailBindingSource.RemoveCurrent();



                SaveContractdetail();

            }


        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            ExportDataGridviewTOExcell.ExportDataGridview(this.ly_purchase_prepareforplanDataGridView, true);
        }

        private void 打印PToolStripButton_Click(object sender, EventArgs e)
        {
            if (null == this.ly_purchase_contract_detailDataGridView.CurrentRow) return;



            NewFrm.Show(this); ;

            //this.ly_plan_getmaterialTableAdapter.Fill(this.lYPlanMange.ly_plan_getmaterial, parentId);

            BaseReportView queryForm = new BaseReportView();

            queryForm.Text = "中原精密外协订单";

            queryForm.Printdata = this.lYOutsourceData ;

            queryForm.PrintCrystalReport = new LY_WaixieDingdan();


            //string selectFormula;

            //selectFormula = "{ly_store_planitemcount.状态}  =   '原料'  and {ly_store_planitemcount.欠料金额}>0 ";
            //queryForm.PrintCrystalReport.DataDefinition.RecordSelectionFormula = selectFormula;

            NewFrm.Hide(this);

            queryForm.ShowDialog();
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

        private void 增加物料供应商ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if ( !SQLDatabase .CheckHaveRight(SQLDatabase .NowUserID,"物料供应商设置"))
            {
                MessageBox.Show("没有物料供应商设置权限...", "注意");
                return;
            }

            string itemno = lY_MaterielRequirementsDataGridView.CurrentRow.Cells["物料编码1"].Value.ToString();
            int  id2 =int .Parse ( lY_MaterielRequirementsDataGridView.CurrentRow.Cells["id2"].Value.ToString());

            this.ly_materiel_supplierTableAdapter.Fill(this.lYMaterielRequirements.ly_materiel_supplier, itemno,id2 );

            LY_Materiel_Supplier_Set queryForm = new LY_Materiel_Supplier_Set();
            queryForm.WindowState = FormWindowState.Maximized;
            queryForm.Sortmode = "WX";
            queryForm.LoadSingleData(itemno);
            queryForm.ShowDialog();

            this.ly_materiel_supplierTableAdapter.Fill(this.lYMaterielRequirements.ly_materiel_supplier, itemno,id2 );
        }

        private void ly_purchase_contract_detailPlanDataGridView_SelectionChanged(object sender, EventArgs e)
        {
            if (null == ly_purchase_contract_detailPlanDataGridView.CurrentRow)
            {

                return;
            }


            string nowcontractcode = this.ly_purchase_contract_detailPlanDataGridView.CurrentRow.Cells["合同编号6"].Value.ToString();
            string nowitemno = this.ly_purchase_contract_detailPlanDataGridView.CurrentRow.Cells["物料编号6"].Value.ToString();
            string nowaddcode = this.ly_purchase_contract_detailPlanDataGridView.CurrentRow.Cells["加工码6"].Value.ToString();

            this.ly_outsource_contract_mainBindingSource.Position = this.ly_outsource_contract_mainBindingSource.Find("合同编号", nowcontractcode);
            this.ly_outsource_contract_detailBindingSource.Position = this.ly_outsource_contract_detailBindingSource.Find("findcode", nowitemno + nowaddcode);

        }

        private void toolStripTextBox1_KeyUp(object sender, KeyEventArgs e)
        {
            string filterString = GetDataGridviewMultiFilterString.DGVMultiFilterString(this.ly_purchase_contract_detailPlanDataGridView, this.toolStripTextBox1.Text);



            if (null == filterString)
                filterString = "";

            this.ly_purchase_contract_detailPlanBindingSource.Filter = filterString;
        }

        private void toolStripTextBox1_Enter(object sender, EventArgs e)
        {
            toolStripTextBox1.Text = "";

            this.ly_purchase_contract_detailPlanBindingSource.Filter = "";
        }

       

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (!SQLDatabase.CheckHaveRight(SQLDatabase.NowUserID, "外协件工艺设置"))
            {
                MessageBox.Show("没有物料外协件工艺设置权限...", "注意");
                return;
            }

            string itemno = lY_MaterielRequirementsDataGridView.CurrentRow.Cells["物料编码1"].Value.ToString();
          
            int id2 = int.Parse(lY_MaterielRequirementsDataGridView.CurrentRow.Cells["id2"].Value.ToString());
          

            LY_Outsource_Process queryForm = new LY_Outsource_Process();
            queryForm.WindowState = FormWindowState.Maximized;
           // queryForm.Sortmode = "WX";
            queryForm.LoadSingleData(itemno);
            queryForm.ShowDialog();

            this.ly_machinepart_processTableAdapter.Fill(this.lYMaterielRequirements.ly_machinepart_process, itemno,id2) ;
        }

        private void ly_bm0031DataGridView_SelectionChanged(object sender, EventArgs e)
        {
            DataGridView dgv = sender as DataGridView;

            if (null == dgv.CurrentRow) return;

            string nowgeometry;

            if (!string.IsNullOrEmpty(this.ly_bm0031DataGridView.CurrentRow.Cells["geometry"].Value.ToString()))
            {
                nowgeometry = this.ly_bm0031DataGridView.CurrentRow.Cells["geometry"].Value.ToString();
            }
            else
            {
                nowgeometry = "ELSE";
            }

            if ("ELSE" == nowgeometry)
            {
                dgv.Columns["diameter"].Visible = false;
                dgv.Columns["width"].Visible = false;
                dgv.Columns["length"].Visible = false;
                dgv.Columns["height"].Visible = false;
                dgv.Columns["input_count"].Visible = false;
                dgv.Columns["specific_weight"].Visible = false;

            }

            if ("棒料" == nowgeometry)
            {
                dgv.Columns["diameter"].Visible = true;
                dgv.Columns["width"].Visible = false;
                dgv.Columns["length"].Visible = true;
                dgv.Columns["height"].Visible = false;
                dgv.Columns["input_count"].Visible = true;
                dgv.Columns["specific_weight"].Visible = true;
            }

            if ("板料" == nowgeometry)
            {
                dgv.Columns["diameter"].Visible = false;
                dgv.Columns["width"].Visible = true;
                dgv.Columns["length"].Visible = true;
                dgv.Columns["height"].Visible = true;
                dgv.Columns["input_count"].Visible = true;
                dgv.Columns["specific_weight"].Visible = true;
            }
        }

        private void 外协件原料设置ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!SQLDatabase.CheckHaveRight(SQLDatabase.NowUserID, "外协件原料设置"))
            {
                MessageBox.Show("没有物料外协件原料设置权限...", "注意");
                return;
            }

            string itemno = lY_MaterielRequirementsDataGridView.CurrentRow.Cells["物料编码1"].Value.ToString();

            int id2 = int.Parse(lY_MaterielRequirementsDataGridView.CurrentRow.Cells["id2"].Value.ToString());


            LY_MaterialBom_Machine queryForm = new LY_MaterialBom_Machine();
            queryForm.WindowState = FormWindowState.Maximized;
            // queryForm.Sortmode = "WX";
            queryForm.LoadSingleData(itemno);
            queryForm.ShowDialog();

            this.ly_bm0031TableAdapter.Fill(this.lYMaterialMange.ly_bm0031, itemno);
        }

        private void 合同外协件工艺调整ToolStripMenuItem_Click(object sender, EventArgs e)
        {
           

            if (null == this.ly_purchase_contract_detailDataGridView.CurrentRow) return;

            DataGridView dgv = ly_purchase_contract_detailDataGridView;


            string nowitemno = dgv.CurrentRow.Cells["物料编号7"].Value.ToString();
            string nowcontractcode = dgv.CurrentRow.Cells["合同编号7"].Value.ToString();
            string nowaddcode = dgv.CurrentRow.Cells["加工码7"].Value.ToString();
            int nowid27 = int.Parse(dgv.CurrentRow.Cells["id27"].Value.ToString());


            LY_Machine_Process_Outsource queryForm = new LY_Machine_Process_Outsource();



            //queryForm.Owner  = this;
            queryForm.Nowitemno = nowitemno;
            queryForm.Nowcontractcode = nowcontractcode;
            queryForm.Nowid2 = nowid27;
            queryForm.Nowaddcode = nowaddcode;

            queryForm.StartPosition = FormStartPosition.CenterParent;
            queryForm.ShowDialog(this);

            //queryForm.ShowDialog();

            this.ly_outsource_order_detail_selTableAdapter.Fill(this.lYOutsourceData.ly_outsource_order_detail_sel, nowcontractcode, nowitemno, 0, nowaddcode);
           // dgv = ly_outsource_order_detail_selDataGridView;
           //// ly_outsource_order_detail_selDataGridView
           // foreach (DataGridViewRow dgr in dgv.Rows)
           // {

           //     if ("下料" == dgr.Cells["序名3"].Value.ToString())
           //     {
           //         dgr.Cells["姓名3"].Value = SQLDatabase.nowUserName();
           //         dgr.Cells["工号3"].Value = "";
           //         dgr.Cells["合同数量3"].Value = ly_purchase_contract_detailDataGridView.CurrentRow.Cells["采购数量"].Value;
           //         dgr.Cells["数量3"].Value = ly_purchase_contract_detailDataGridView.CurrentRow.Cells["采购数量"].Value;
           //         dgr.Cells["合格3"].Value = ly_purchase_contract_detailDataGridView.CurrentRow.Cells["采购数量"].Value;

           //         dgv.EndEdit();
           //         this.ly_outsource_order_detail_selBindingSource.EndEdit();
           //         this.ly_outsource_order_detail_selTableAdapter.Update(this.lYOutsourceData.ly_outsource_order_detail_sel);

                    
           //     }

           //     if ("外协" == dgr.Cells["序名3"].Value.ToString())
           //     {
           //         dgr.Cells["姓名3"].Value = this.ly_purchase_contract_mainDataGridView.CurrentRow.Cells["供应商名称6"].Value;
           //         dgr.Cells["工号3"].Value = this.ly_purchase_contract_mainDataGridView.CurrentRow.Cells["供应商编号6"].Value;
           //         dgr.Cells["合同数量3"].Value = ly_purchase_contract_detailDataGridView.CurrentRow.Cells["采购数量"].Value;
           //         dgr.Cells["数量3"].Value = ly_purchase_contract_detailDataGridView.CurrentRow.Cells["采购数量"].Value;

           //         dgv.EndEdit();
           //         this.ly_outsource_order_detail_selBindingSource.EndEdit();
           //         this.ly_outsource_order_detail_selTableAdapter.Update(this.lYOutsourceData.ly_outsource_order_detail_sel);

                   
           //     }

              
           // }

           

           // this.ly_outsource_order_detail_selTableAdapter.Fill(this.lYOutsourceData.ly_outsource_order_detail_sel, nowcontractcode, nowitemno, 0);


            if (queryForm.DialogResult != DialogResult.Cancel)
            {
                //this.ly_inma0010ylTableAdapter.Fill(this.lYMaterialMange.ly_inma0010yl);
                //this.ly_inma0010ylBindingSource.Position = this.ly_inma0010ylBindingSource.Find("物资编号", queryForm.material_code);
            }

        }

        private string GetMaxMaterialrequisitioncode(string nowcontractcode,string nowparentitemno)
        {
            SqlConnection sqlConnection1 = new SqlConnection(SQLDatabase.Connectstring);
            SqlCommand cmd = new SqlCommand();

            string Materialrequisitioncode = "";

           

            cmd.Parameters.Add("@contract_code", SqlDbType.VarChar);
            cmd.Parameters["@contract_code"].Value = nowcontractcode;

            cmd.Parameters.Add("@parentitemno", SqlDbType.VarChar);
            cmd.Parameters["@parentitemno"].Value = nowparentitemno;


            cmd.CommandText = "LY_GetMax_Materialrequisitioncode_outsource";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = sqlConnection1;

            sqlConnection1.Open();
            Materialrequisitioncode = cmd.ExecuteScalar().ToString();
            sqlConnection1.Close();



            return Materialrequisitioncode;
        }
        private void toolStripButton9_Click(object sender, EventArgs e)
        {
           

            //if ("True" == this.ly_production_order_materialrequisitionDataGridView.CurrentRow.Cells["追加5"].Value.ToString())
            //{

            //    MessageBox.Show("追加领料单不能继续追加领料,可以在原领料单上继续追加领料的操作...", "注意");
            //    return;
            //}

            if (null == this.ly_purchase_contract_detailDataGridView.CurrentRow) return;

            int ifxialiao = this.ly_outsource_order_detail_selBindingSource.Find("序名", "下料");
            int nowdetailId;

            if (0 > ifxialiao)
            {
                MessageBox.Show("无下料工艺,不能追加领料...", "注意");
                return;
            }
            else
            {
                this.ly_outsource_order_detail_selBindingSource.Position = ifxialiao;
              
                nowdetailId = int.Parse(this.ly_outsource_order_detail_selDataGridView.Rows[ifxialiao].Cells["id_order"].Value.ToString());

            }

            DataGridView dgv = ly_purchase_contract_detailDataGridView;


            string nowparentitemno = dgv.CurrentRow.Cells["物料编号7"].Value.ToString();
            string nowcontractcode = dgv.CurrentRow.Cells["合同编号7"].Value.ToString();
           
            string nowbuyer = this.ly_purchase_contract_mainDataGridView.CurrentRow.Cells["采购员"].Value.ToString();
            string nowmaterialname = dgv.CurrentRow.Cells["物料名称7"].Value.ToString();

            string message1 = "确认追加(合同：" + nowcontractcode + "  " + nowmaterialname + "(" + nowparentitemno + ") " + nowbuyer + " )原材料领料吗?";
            string caption1 = "提示...";
            MessageBoxButtons buttons1 = MessageBoxButtons.YesNo;

            DialogResult result1;



            result1 = MessageBox.Show(message1, caption1, buttons1,
            MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

            if (result1 == DialogResult.Yes)
            {


                string nowrequisition_style ="WXLL";


                string nowmaterialrequisition_num = GetMaxMaterialrequisitioncode(nowcontractcode, nowparentitemno);
              

                string nowitemno;

                ///////////////////////////////////////////////////////





                string sel = " SELECT   a.wzbh as 编号,a.mch as 名称,a.jph as 库位,a.xhc as 中方型号,a.xhc as 日方型号,a.gg as 规格,a.mch_jp as 简拼,a.mch_py as 拼音,b.sortname as类别 FROM ly_inma0010 a left join ly_materrial_sort b on a.sort1=b.sortcode  where a.status='原料' or a.status='基料' " ;

                

                QueryForm queryForm = new QueryForm();


                queryForm.Sel = sel;
                queryForm.Constr = SQLDatabase.Connectstring;

                //Set the Column Collection to the filter Table
                //queryForm.SetSourceColumns(this.billMainDataSet.BalanceBill.Columns);

                queryForm.ShowDialog();


                if (!string.IsNullOrEmpty(queryForm.Result))
                {
                    nowitemno = queryForm.Result;
                    //dgv.CurrentRow.Cells["discount_money"].Value = DBNull.Value;



                }
                else
                {
                    return;

                }



                ///////////////////////////////////////////////////////



                this.ly_outsource_order_materialrequisitionBindingSource.AddNew();



                this.ly_outsource_order_materialrequisitionDataGridView.CurrentRow.Cells["detail_id"].Value = nowdetailId;
                this.ly_outsource_order_materialrequisitionDataGridView.CurrentRow.Cells["追加"].Value = "True";
                this.ly_outsource_order_materialrequisitionDataGridView.CurrentRow.Cells["类别"].Value = nowrequisition_style;
                
                this.ly_outsource_order_materialrequisitionDataGridView.CurrentRow.Cells["领料单号"].Value = nowmaterialrequisition_num;
                this.ly_outsource_order_materialrequisitionDataGridView.CurrentRow.Cells["父件编码"].Value = nowparentitemno;
                this.ly_outsource_order_materialrequisitionDataGridView.CurrentRow.Cells["合同编号8"].Value = nowcontractcode;
                this.ly_outsource_order_materialrequisitionDataGridView.CurrentRow.Cells["物料编号8"].Value = nowitemno;
                this.ly_outsource_order_materialrequisitionDataGridView.CurrentRow.Cells["姓名"].Value = nowbuyer;
                this.ly_outsource_order_materialrequisitionDataGridView.CurrentRow.Cells["下单人"].Value = nowbuyer;
                this.ly_outsource_order_materialrequisitionDataGridView.CurrentRow.Cells["安排日期"].Value = SQLDatabase .GetNowdate();
               
                this.ly_outsource_order_materialrequisitionDataGridView.EndEdit();
                this.ly_outsource_order_materialrequisitionBindingSource.EndEdit();

                this.ly_outsource_order_materialrequisitionTableAdapter.Update(this.lYOutsourceData.ly_outsource_order_materialrequisition);
                this.ly_outsource_order_materialrequisitionTableAdapter.Fill(this.lYOutsourceData.ly_outsource_order_materialrequisition, nowcontractcode);

                this.ly_outsource_order_materialrequisitionBindingSource.Position = this.ly_outsource_order_materialrequisitionBindingSource.Count - 1;
            }
        }

        private void toolStripButton10_Click(object sender, EventArgs e)
        {
            if (null == ly_outsource_order_materialrequisitionDataGridView.CurrentRow) return;

            //string diaodu = this.ly_outsource_order_materialrequisitionDataGridView.CurrentRow.Cells["调度5"].Value.ToString();

            //if (diaodu != SQLDatabase.nowUserName())
            //{
            //    MessageBox.Show("请调度:" + diaodu + "删除", "注意");
            //    return;
            //}



            if ("True" != this.ly_outsource_order_materialrequisitionDataGridView.CurrentRow.Cells["追加"].Value.ToString())
            {

                MessageBox.Show("只有追加领料单才能删除...", "注意");
                return;
            }

            decimal nowgetqty;



            if ("" != this.ly_outsource_order_materialrequisitionDataGridView.CurrentRow.Cells["已领"].Value.ToString())
            {
                nowgetqty = decimal.Parse(this.ly_outsource_order_materialrequisitionDataGridView.CurrentRow.Cells["已领"].Value.ToString());
            }
            else
            {
                nowgetqty = 0;
            }

            if (nowgetqty > 0)
            {
                MessageBox.Show("已有领料记录,删除领料记录后才能删除领料单...", "注意");
                return;

            }

            string nowmaterialrequisitionnum = this.ly_outsource_order_materialrequisitionDataGridView.CurrentRow.Cells["领料单号"].Value.ToString();

            string message1 = "当前(领料单：" + nowmaterialrequisitionnum + ")将被删除，继续吗？";
            string caption1 = "提示...";
            MessageBoxButtons buttons1 = MessageBoxButtons.YesNo;

            DialogResult result1;



            result1 = MessageBox.Show(message1, caption1, buttons1,
            MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

            if (result1 == DialogResult.Yes)
            {

                this.ly_outsource_order_materialrequisitionBindingSource.RemoveCurrent();


                this.ly_outsource_order_materialrequisitionDataGridView.EndEdit();
                this.ly_outsource_order_materialrequisitionBindingSource.EndEdit();

                this.ly_outsource_order_materialrequisitionTableAdapter.Update(this.lYOutsourceData.ly_outsource_order_materialrequisition);




            }
        }

        private void toolStripButton11_Click(object sender, EventArgs e)
        {

            string nowitemno = ly_purchase_contract_detailDataGridView.CurrentRow.Cells["物料编号7"].Value.ToString();
          

           

            if ("显示父件" == toolStripButton11.Text)
            {
                toolStripButton11.Text = "显示合同";
                this.ly_outsource_order_materialrequisitionBindingSource.Filter = "";
                this.toolStripButton10.Enabled = false;
                this.toolStripButton9.Enabled = false;
            }
            else
            {
                toolStripButton11.Text = "显示父件";
                this.ly_outsource_order_materialrequisitionBindingSource.Filter = "父件编码='" + nowitemno + "'";
                this.toolStripButton10.Enabled = true ;
                this.toolStripButton9.Enabled = true ;
                
            }
        }

        private void SaveLingliao()
        {
            this.ly_outsource_order_materialrequisitionDataGridView.EndEdit();
            this.ly_outsource_order_materialrequisitionBindingSource.EndEdit();

            this.ly_outsource_order_materialrequisitionTableAdapter.Update(this.lYOutsourceData.ly_outsource_order_materialrequisition);



           
        }

        private void ly_outsource_order_materialrequisitionDataGridView_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            DataGridView dgv = sender as DataGridView;


            //if ("True" != this.ly_outsource_order_materialrequisitionDataGridView.CurrentRow.Cells["追加"].Value.ToString())
            //{

            //    MessageBox.Show("只有追加领料单才能修改...", "注意");
            //    return;
            //}

            /////////////////////////////////////////////////////////

            //if ("True" == dgv.CurrentRow.Cells["批准10"].Value.ToString())
            //{
            //    MessageBox.Show("领料单已经审批,不能修改数据...", "注意");
            //    return;

            //}
           
            ///////////////////////////////////////////////////////////
            if ("批准10" == dgv.CurrentCell.OwningColumn.Name)
            {

                if ("True" == dgv.CurrentRow.Cells["批准10"].Value.ToString())
                {
                    dgv.CurrentRow.Cells["批准10"].Value = "False";
                    dgv.CurrentRow.Cells["批准人"].Value = DBNull.Value;

                }
                else
                {

                    dgv.CurrentRow.Cells["批准10"].Value = "True";
                    dgv.CurrentRow.Cells["批准人"].Value = SQLDatabase.nowUserName();
                }



                SaveLingliao();



                return;

            }
            ///////////////////////////////////////////////////////////

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

            /////////////////////////////
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
                    SaveLingliao();

                    //CountPlanStru();

                }
                else
                {

                }
                return;

            }
////////////////////////////////////////////////////////////////////
            if ("备注10" == dgv.CurrentCell.OwningColumn.Name)
            {

                ChangeValue queryForm = new ChangeValue();

                queryForm.OldValue = dgv.CurrentCell.Value.ToString();
                queryForm.NewValue = "";
                queryForm.ChangeMode = "longstring";
                queryForm.ShowDialog();




                if (queryForm.NewValue != "")
                {
                    dgv.CurrentRow.Cells["备注10"].Value = queryForm.NewValue;
                    //dgv.CurrentRow.Cells["discount_money"].Value = DBNull.Value;

                    //dgv.CurrentRow.Cells["approve_flag"].Value = "False";
                    SaveLingliao();

                    //CountPlanStru();

                }
                else
                {

                }
                return;

            }

            ///////////////////////////////////////////////////////////
            if ("批准意见" == dgv.CurrentCell.OwningColumn.Name)
            {

                ChangeValue queryForm = new ChangeValue();

                queryForm.OldValue = dgv.CurrentCell.Value.ToString();
                queryForm.NewValue = "";
                queryForm.ChangeMode = "longstring";
                queryForm.ShowDialog();




                if (queryForm.NewValue != "")
                {
                    dgv.CurrentRow.Cells["批准意见"].Value = queryForm.NewValue;
                    //dgv.CurrentRow.Cells["discount_money"].Value = DBNull.Value;

                    //dgv.CurrentRow.Cells["approve_flag"].Value = "False";
                    SaveLingliao();

                    //CountPlanStru();

                }
                else
                {

                }
                return;

            }
        }

        private void ly_outsource_contract_detailaddDataGridView_SelectionChanged(object sender, EventArgs e)
        {
            DataGridView dgv = sender as DataGridView;

            if (null == dgv.CurrentRow)
            {
                //this.ly_machinepart_process_outsourceTableAdapter.Fill(this.lYOutsourceData.ly_machinepart_process_outsource, "", "",0);
                this.ly_outsource_order_detail_selTableAdapter.Fill(this.lYOutsourceData.ly_outsource_order_detail_sel, "", "", 0, "");
                return;
            }

            string nowitemno = dgv.CurrentRow.Cells["物料编号9"].Value.ToString();
            string nowcontractcode = dgv.CurrentRow.Cells["合同编号9"].Value.ToString();
            string nowaddcode = dgv.CurrentRow.Cells["加工码9"].Value.ToString();
            int nowid27 = int.Parse(dgv.CurrentRow.Cells["id29"].Value.ToString());

            this.ly_outsource_order_materialrequisitionBindingSource.Filter = "父件编码='" + nowitemno + "' and 加工码='" + nowaddcode + "'";
            this.ly_outsource_order_detail_selTableAdapter.Fill(this.lYOutsourceData.ly_outsource_order_detail_sel, nowcontractcode, nowitemno, 0, nowaddcode);
        }

        private void tabControl4_SelectedIndexChanged(object sender, EventArgs e)
        {
             TabControl tc = sender as TabControl;

             if (this.tabPage10 == tc.SelectedTab)
             {
                 this.ly_purchase_contract_detailDataGridView.SelectionChanged += ly_purchase_contract_detailDataGridView_SelectionChanged;
                 this.ly_outsource_contract_detailaddDataGridView .SelectionChanged-= ly_outsource_contract_detailaddDataGridView_SelectionChanged;

                 DataGridView dgv = ly_purchase_contract_detailDataGridView;

                 if (null == dgv.CurrentRow)
                 {
                     //this.ly_machinepart_process_outsourceTableAdapter.Fill(this.lYOutsourceData.ly_machinepart_process_outsource, "", "",0);
                     this.ly_outsource_order_detail_selTableAdapter.Fill(this.lYOutsourceData.ly_outsource_order_detail_sel, "", "", 0, "");
                     return;
                 }

                 string nowitemno = dgv.CurrentRow.Cells["物料编号7"].Value.ToString();
                 string nowcontractcode = dgv.CurrentRow.Cells["合同编号7"].Value.ToString();
                 string nowaddcode = dgv.CurrentRow.Cells["加工码7"].Value.ToString();
                 int nowid27 = int.Parse(dgv.CurrentRow.Cells["id27"].Value.ToString());

                 this.ly_outsource_order_materialrequisitionBindingSource.Filter = "父件编码='" + nowitemno + "' and 加工码='" + nowaddcode + "'";
                 this.ly_outsource_order_detail_selTableAdapter.Fill(this.lYOutsourceData.ly_outsource_order_detail_sel, nowcontractcode, nowitemno, 0, nowaddcode);
             }
             if (this.tabPage11 == tc.SelectedTab)
             {
                 this.ly_purchase_contract_detailDataGridView.SelectionChanged -= ly_purchase_contract_detailDataGridView_SelectionChanged;
                 this.ly_outsource_contract_detailaddDataGridView.SelectionChanged += ly_outsource_contract_detailaddDataGridView_SelectionChanged;

                 DataGridView dgv = ly_outsource_contract_detailaddDataGridView;

                 if (null == dgv.CurrentRow)
                 {
                     //this.ly_machinepart_process_outsourceTableAdapter.Fill(this.lYOutsourceData.ly_machinepart_process_outsource, "", "",0);
                     this.ly_outsource_order_detail_selTableAdapter.Fill(this.lYOutsourceData.ly_outsource_order_detail_sel, "", "", 0, "");
                     return;
                 }

                 string nowitemno = dgv.CurrentRow.Cells["物料编号9"].Value.ToString();
                 string nowcontractcode = dgv.CurrentRow.Cells["合同编号9"].Value.ToString();
                 string nowaddcode = dgv.CurrentRow.Cells["加工码9"].Value.ToString();
                 int nowid27 = int.Parse(dgv.CurrentRow.Cells["id29"].Value.ToString());

                 this.ly_outsource_order_materialrequisitionBindingSource.Filter = "父件编码='" + nowitemno + "' and 加工码='" + nowaddcode + "'";
                 this.ly_outsource_order_detail_selTableAdapter.Fill(this.lYOutsourceData.ly_outsource_order_detail_sel, nowcontractcode, nowitemno, 0, nowaddcode);
             }
        }

        private void toolStripButton12_Click(object sender, EventArgs e)
        {
            if (null == this.ly_purchase_contract_detailDataGridView.CurrentRow) return;



            NewFrm.Show(this); ;

            //this.ly_plan_getmaterialTableAdapter.Fill(this.lYPlanMange.ly_plan_getmaterial, parentId);

            BaseReportView queryForm = new BaseReportView();

            queryForm.Text = "中原精密外协合同";

            queryForm.Printdata = this.lYOutsourceData;

            queryForm.PrintCrystalReport = new LY_WaixieHetong();


            //string selectFormula;

            //selectFormula = "{ly_store_planitemcount.状态}  =   '原料'  and {ly_store_planitemcount.欠料金额}>0 ";
            //queryForm.PrintCrystalReport.DataDefinition.RecordSelectionFormula = selectFormula;

            NewFrm.Hide(this);

            queryForm.ShowDialog();
        }

        private void toolStripButton14_Click(object sender, EventArgs e)
        {
            if (null == this.ly_purchase_contract_detailDataGridView.CurrentRow) return;


            NewFrm.Show(this); ;

            //this.ly_plan_getmaterialTableAdapter.Fill(this.lYPlanMange.ly_plan_getmaterial, parentId);

            BaseReportView queryForm = new BaseReportView();

            queryForm.Text = "中原精密外协合同";

            queryForm.Printdata = this.lYOutsourceData;
            queryForm.PrintCrystalReport = new LY_WaixieHetongNew();


            //string selectFormula;

            //selectFormula = "{ly_store_planitemcount.状态}  =   '原料'  and {ly_store_planitemcount.欠料金额}>0 ";
            //queryForm.PrintCrystalReport.DataDefinition.RecordSelectionFormula = selectFormula;

            NewFrm.Hide(this);

            queryForm.ShowDialog();
        }

        //private void fillToolStripButton_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        this.ly_contract_terms_foroutsourceTableAdapter.Fill(this.lYOutsourceData.ly_contract_terms_foroutsource, contract_inner_codeToolStripTextBox.Text);
        //    }
        //    catch (System.Exception ex)
        //    {
        //        System.Windows.Forms.MessageBox.Show(ex.Message);
        //    }

        //}

        //private void fillToolStripButton_Click_1(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        this.ly_company_information_outsourceTableAdapter.Fill(this.lYOutsourceData.ly_company_information_outsource, contract_codeToolStripTextBox.Text);
        //    }
        //    catch (System.Exception ex)
        //    {
        //        System.Windows.Forms.MessageBox.Show(ex.Message);
        //    }

        //}

      

       ///////////////////////////////////////////////////
      
    }
}
