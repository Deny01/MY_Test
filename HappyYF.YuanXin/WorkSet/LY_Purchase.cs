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
using System.Threading;

namespace HappyYF.YuanXin.WorkSet
{
    public partial class LY_Purchase : Form
    {

        string formState = "View";
        public LY_Purchase()
        {
            InitializeComponent();
            this.lY_MaterielRequirementsPurchaseTableAdapter.CommandTimeout = 0;
            this.ly_purchase_contract_mainTableAdapter.CommandTimeout = 0;
        }

        private void LY_Machine_Load(object sender, EventArgs e)
        {


            if (SQLDatabase.CheckHaveRight(SQLDatabase.NowUserID, "外协合同审批"))
            {
                this.ly_purchase_contract_mainBindingSource.Filter = "";
                this.lY_MaterielRequirementsPurchaseBindingSource.Filter = "";
                this.lY_MaterielRequirementsPurchaseSupplierBindingSource.Filter = "";
                this.ly_purchase_contract_main11BindingSource.Filter = "";
            }
            else
            {
                //this.ly_purchase_contract_mainBindingSource.Filter = "采购员='" + SQLDatabase.nowUserName() + "' and 执行部门='" + SQLDatabase .nowUserDepartment().Substring (0,4)+"'";

                this.ly_purchase_contract_mainBindingSource.Filter = "采购员='" + SQLDatabase.nowUserName() + "'" ;
                this .ly_purchase_contract_main11BindingSource.Filter = "采购员='" + SQLDatabase.nowUserName() + "'";
                this.ly_purchase_contract_detailPlanBindingSource.Filter = "buyer='" + SQLDatabase.nowUserName() + "'";
                //this .lY_MaterielRequirementsPurchaseBindingSource.Filter ="执行部门='" + SQLDatabase .nowUserDepartment().Substring (0,4)+"'";

                this.lY_MaterielRequirementsPurchaseBindingSource.Filter = "buyer_code='" + SQLDatabase.NowUserID + "'";
                this.lY_MaterielRequirementsPurchaseSupplierBindingSource.Filter = "buyer_code='" + SQLDatabase.NowUserID + "'";

            }
            this.ly_store_planitemcountTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;

            this.ly_purchase_contract_detail_modificationTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;

            this.ly_purchase_contract_main11TableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;


            this.ly_company_information_purchaseTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;

            this.ly_contract_terms_forpurchaseTableAdapter1.Connection.ConnectionString = SQLDatabase.Connectstring;
            this.ly_materiel_supplierTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            this.ly_purchase_prepareforplanTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            this.ly_purchase_contract_mainTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;

            this.ly_purchase_contract_detailTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;

            this.ly_purchase_contract_appendTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            

            this.ly_purchase_contract_detailPlanTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;

      

            this.lY_MaterielRequirementsPurchaseTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;

            this.lY_MaterielRequirementsPurchaseSupplierTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
           
            this.ly_material_plan_mainTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            this.ly_material_plan_mainTableAdapter.Fill(this.lYMaterielRequirements.ly_material_plan_main, "SCJH");
           
            this.ly_purchase_contract_main11TableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;

            
            this.dateTimePicker1.Text = SQLDatabase.GetNowdate().AddMonths(-1).Date.ToString();
            this.dateTimePicker2.Text = SQLDatabase.GetNowdate().AddDays(0).Date.ToString();

            //this.ly_purchase_contract_main11TableAdapter.Fill(this.lYMaterielRequirements.ly_purchase_contract_main11, this.dateTimePicker1.Value, this.dateTimePicker2.Value.AddDays(1));
            ly_PrepaymentTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;

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

                    NewFrm.Show(this.ParentForm);
                    Thread.Sleep(500);
                    this.lY_MaterielRequirementsPurchaseTableAdapter.Fill(this.lYMaterielRequirements.LY_MaterielRequirementsPurchase, parentId, "外购");
                   

                    this.ly_purchase_prepareforplanTableAdapter.Fill(this.lYMaterielRequirements.ly_purchase_prepareforplan, planNum, "3");

                    this.ly_purchase_contract_mainTableAdapter.Fill(this.lYMaterielRequirements.ly_purchase_contract_main, planNum);

                    this.ly_purchase_contract_detailPlanDataGridView.SelectionChanged -= ly_purchase_contract_detailPlanDataGridView_SelectionChanged;
                    this.ly_purchase_contract_detailPlanTableAdapter.Fill(this.lYMaterielRequirements.ly_purchase_contract_detailPlan, planNum);
                    this.ly_purchase_contract_detailPlanDataGridView.SelectionChanged += ly_purchase_contract_detailPlanDataGridView_SelectionChanged;
                    NewFrm.Hide(this.ParentForm);
                }
            }

                  
                 
        }

        private void lY_MaterielRequirementsDataGridView_SelectionChanged(object sender, EventArgs e)
        {
            //if (null == this.lY_MaterielRequirementsDataGridView.CurrentRow) return;

            
        }

        private void lY_MaterielRequirementsDataGridView_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView dgv = sender as DataGridView;

            if (null == dgv.CurrentRow)
            {
                this.ly_materiel_supplierTableAdapter.Fill(this.lYMaterielRequirements.ly_materiel_supplier, "",0);
                return;
            }


            string itemno = dgv.Rows[e.RowIndex].Cells["物料编码1"].Value.ToString();// lY_MaterielRequirementsDataGridView
            string itemnoname = dgv.Rows[e.RowIndex].Cells["物料名称1"].Value.ToString();
            int  id2 =int .Parse ( dgv.Rows[e.RowIndex].Cells["id2"].Value.ToString());

            this.groupBox3.Text = itemno +" "+ itemnoname + ":物料供应商";

            //this.ly_materiel_supplierBindingSource.Filter = " 供应商编码 like '%CG%' ";
            this.ly_materiel_supplierTableAdapter.Fill(this.lYMaterielRequirements.ly_materiel_supplier, itemno,id2 );
        }

        private void ly_purchase_contract_detailDataGridView_SelectionChanged(object sender, EventArgs e)
        {
            //lY_MaterielRequirementsPurchaseSupplierDataGridView
        }

        private void ly_purchase_contract_mainDataGridView_SelectionChanged(object sender, EventArgs e)
        {
            if (null == ly_purchase_contract_mainDataGridView.CurrentRow )
            {
                this.ly_purchase_contract_detailTableAdapter.Fill(this.lYMaterielRequirements.ly_purchase_contract_detail, "");
                this.lY_MaterielRequirementsPurchaseSupplierTableAdapter.Fill(this.lYMaterielRequirements.LY_MaterielRequirementsPurchaseSupplier, 0, "外购", "");
                this.ly_purchase_contract_appendTableAdapter.Fill(this.lYMaterielRequirements.ly_purchase_contract_append, "");
                this.ly_purchase_contract_detail_modificationTableAdapter.Fill(this.lYMaterielRequirements.ly_purchase_contract_detail_modification, "");
                ly_PrepaymentTableAdapter.Fill(lYMaterielRequirements.ly_Prepayment,"");
                return;
            }


            string nowcontractcode = this.ly_purchase_contract_mainDataGridView.CurrentRow .Cells["合同编号"].Value.ToString();
            string nowsuppliercode=this.ly_purchase_contract_mainDataGridView.CurrentRow .Cells["供应商编号6"].Value.ToString();
            string contract_id= this.ly_purchase_contract_mainDataGridView.CurrentRow.Cells["id6"].Value.ToString();


            int parentId = int.Parse(this.ly_material_plan_mainDataGridView.CurrentRow .Cells["parentid"].Value.ToString());
            string planNum = this.ly_material_plan_mainDataGridView.CurrentRow.Cells["计划编号"].Value.ToString();

            ly_PrepaymentTableAdapter.Fill(lYMaterielRequirements.ly_Prepayment, nowcontractcode);

            this.lY_MaterielRequirementsPurchaseSupplierTableAdapter.Fill(this.lYMaterielRequirements.LY_MaterielRequirementsPurchaseSupplier, parentId, "外购", nowsuppliercode);

            this.ly_purchase_contract_detail_modificationTableAdapter.Fill(this.lYMaterielRequirements.ly_purchase_contract_detail_modification, nowcontractcode);
            this.ly_purchase_contract_detailTableAdapter.Fill(this.lYMaterielRequirements.ly_purchase_contract_detail, nowcontractcode);
            this.ly_purchase_contract_appendTableAdapter.Fill(this.lYMaterielRequirements.ly_purchase_contract_append, nowcontractcode);

            this.ly_contract_terms_forpurchaseTableAdapter1.Fill(this.lYMaterielRequirements.ly_contract_terms_forpurchase, nowcontractcode);
            this.ly_company_information_purchaseTableAdapter.Fill(this.lYMaterielRequirements.ly_company_information_purchase, nowcontractcode);
 

        }

        private void ly_materiel_supplierDataGridView_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
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
               "( plan_num,sortcode,itemno,qty,purchase_qty,supplier_code,id2,part_name,instore_flag,buyer) " +
               " values ('" + nowsplanNum + "','" + "3" + "','" + nowitemno + "'," + nowQty + "," + nowQty + ",'" 
               + nowsupplierCode + "'," + nowid2 + ",'" + nowpartname + "'," + nowinstoreflag + ",'"+SQLDatabase.nowUserName()+"' )";


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

                this.ly_purchase_prepareforplanTableAdapter.Fill(this.lYMaterielRequirements.ly_purchase_prepareforplan, nowsplanNum, "3");
                this.ly_purchase_prepareforplanBindingSource.Position = this.ly_purchase_prepareforplanBindingSource.Find("findcode", nowsupplierCode + nowitemno);
               
               

                ////NewFrm.Show(this);2018-4-3(this.ParentForm);
                this.lY_MaterielRequirementsPurchaseTableAdapter.Fill(this.lYMaterielRequirements.LY_MaterielRequirementsPurchase, parentId, "外购");
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



               
              
          

            this.lY_MaterielRequirementsPurchaseTableAdapter.Fill(this.lYMaterielRequirements.LY_MaterielRequirementsPurchase, parentId, "外购");


            this.ly_purchase_prepareforplanTableAdapter.Fill(this.lYMaterielRequirements.ly_purchase_prepareforplan, planNum, "3");
             this.lY_MaterielRequirementsPurchaseBindingSource.Position = this.lY_MaterielRequirementsPurchaseBindingSource.Find("物料编码", nowitemno);

            this.ly_purchase_contract_mainTableAdapter.Fill(this.lYMaterielRequirements.ly_purchase_contract_main, planNum);
            this.ly_purchase_contract_mainBindingSource.Position = this.ly_purchase_contract_mainBindingSource.Find("id", nowcontractId);
            this.ly_purchase_contract_detailBindingSource.Position = this.ly_purchase_contract_detailBindingSource.Find("id", nowcontractdetailId);
            this.ly_purchase_contract_detailBindingSource.Position = this.ly_purchase_contract_detailBindingSource.Find("物料编号", nowcontractdetailcode);
           
        }

        private void RefreshDataAppend()
        {






            int parentId = int.Parse(this.ly_material_plan_mainDataGridView.CurrentRow.Cells["parentid"].Value.ToString());
            string planNum = this.ly_material_plan_mainDataGridView.CurrentRow.Cells["计划编号"].Value.ToString();

            //string nowitemno;
            //if (null != this.ly_purchase_prepareforplanDataGridView.CurrentRow)
            //{
            //    nowitemno = this.ly_purchase_prepareforplanDataGridView.CurrentRow.Cells["物料编号"].Value.ToString();
            //}
            //else
            //{
            //    nowitemno = "";
            //}

            // 

            int nowcontractId;
            if (null != ly_purchase_contract_mainDataGridView.CurrentRow)
            {
                nowcontractId = int.Parse(ly_purchase_contract_mainDataGridView.CurrentRow.Cells["id6"].Value.ToString());
            }
            else
            {
                nowcontractId = 0;
            }

            //int nowcontractappendId;
            //if (null != ly_purchase_contract_detailDataGridView.CurrentRow)
            //{
            //    nowcontractappendId = int.Parse(ly_purchase_contract_detailDataGridView.CurrentRow.Cells["id7"].Value.ToString());
            //}
            //else
            //{
            //    nowcontractappendId = 0;
            //}



            //string nowcontractdetailcode;
            //if (null != ly_purchase_contract_detailDataGridView.CurrentRow)
            //{
            //    nowcontractdetailcode = ly_purchase_contract_detailDataGridView.CurrentRow.Cells["物料编号7"].Value.ToString();
            //}
            //else
            //{
            //    nowcontractdetailcode = "";
            //}







            //this.lY_MaterielRequirementsPurchaseTableAdapter.Fill(this.lYMaterielRequirements.LY_MaterielRequirementsPurchase, parentId, "外购");


            //this.ly_purchase_prepareforplanTableAdapter.Fill(this.lYMaterielRequirements.ly_purchase_prepareforplan, planNum, "3");
            //this.lY_MaterielRequirementsPurchaseBindingSource.Position = this.lY_MaterielRequirementsPurchaseBindingSource.Find("物料编码", nowitemno);

            this.ly_purchase_contract_mainTableAdapter.Fill(this.lYMaterielRequirements.ly_purchase_contract_main, planNum);
            this.ly_purchase_contract_mainBindingSource.Position = this.ly_purchase_contract_mainBindingSource.Find("id", nowcontractId);
            //this.ly_purchase_contract_detailBindingSource.Position = this.ly_purchase_contract_detailBindingSource.Find("id", nowcontractdetailId);
            //this.ly_purchase_contract_detailBindingSource.Position = this.ly_purchase_contract_detailBindingSource.Find("物料编号", nowcontractdetailcode);

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
            if (!string.IsNullOrEmpty(this.ly_purchase_prepareforplanDataGridView.CurrentRow.Cells["采购员ca"].Value.ToString()))
            {
                if (this.ly_purchase_prepareforplanDataGridView.CurrentRow.Cells["采购员ca"].Value.ToString() != SQLDatabase.nowUserName())
                {
                    MessageBox.Show("只能删除自己的草案");return;
                }
            }

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

                //NewFrm.Show(this);2018-4-3(this);

                SqlConnection sqlConnection1 = new SqlConnection(SQLDatabase.Connectstring);
                SqlCommand cmd = new SqlCommand();



                cmd.Parameters.Add("@plan_num", SqlDbType.VarChar);
                cmd.Parameters["@plan_num"].Value = planNum;

                cmd.Parameters.Add("@buyer", SqlDbType.VarChar);
                cmd.Parameters["@buyer"].Value = SQLDatabase.nowUserName();


                cmd.CommandText = "LY_make_purchase_contract";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = sqlConnection1;
                cmd.CommandTimeout = 0;

                sqlConnection1.Open();
                cmd.ExecuteNonQuery();
                sqlConnection1.Close();


                //ly_material_plan_explodeTableAdapter.Fill(this.lYPlanMange.ly_material_plan_explode, parentId);
                //this.ly_store_planitemcountTableAdapter.Fill(this.lYPlanMange.ly_store_planitemcount, parentId);
                //this.ly_plan_getmaterial_departmentTableAdapter.Fill(this.lYPlanMange.ly_plan_getmaterial_department , planNum);

                //NewFrm.Hide(this);2018-4-3

                int parentId = int.Parse(this.ly_material_plan_mainDataGridView.CurrentRow.Cells["parentid"].Value.ToString());
                //string planNum = this.ly_material_plan_mainDataGridView.Rows[e.RowIndex].Cells["计划编号"].Value.ToString();

                ////NewFrm.Show(this);2018-4-3(this.ParentForm);
                this.lY_MaterielRequirementsPurchaseTableAdapter.Fill(this.lYMaterielRequirements.LY_MaterielRequirementsPurchase, parentId, "外购");
                //NewFrm.Hide(this.ParentForm);

                this.ly_purchase_prepareforplanTableAdapter.Fill(this.lYMaterielRequirements.ly_purchase_prepareforplan, planNum, "3");

                this.ly_purchase_contract_mainTableAdapter.Fill(this.lYMaterielRequirements.ly_purchase_contract_main, planNum);


                this.tabControl2.SelectedTab = this.tabPage3;


            }
        }

       

        private void SaveContract()
        {
            this.ly_purchase_contract_mainDataGridView.EndEdit();
            this.ly_purchase_contract_mainBindingSource.EndEdit();

            this.ly_purchase_contract_mainTableAdapter.Update(this.lYMaterielRequirements.ly_purchase_contract_main);
            
            

            RefreshData();
        }

        private void SaveContractdetail()
        {
            this.ly_purchase_contract_detailDataGridView.EndEdit();
            this.ly_purchase_contract_detailBindingSource.EndEdit();

            this.ly_purchase_contract_detailTableAdapter.Update(this.lYMaterielRequirements.ly_purchase_contract_detail);

            RefreshData();
        }

        private void SaveContractappend()
        {
            this.ly_purchase_contract_appendDataGridView.EndEdit();
            this.ly_purchase_contract_appendBindingSource.EndEdit();

         
            this.ly_purchase_contract_appendTableAdapter.Update(this.lYMaterielRequirements.ly_purchase_contract_append);
            RefreshDataAppend();
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
                this.ly_purchase_contract_mainTableAdapter.Fill(this.lYMaterielRequirements.ly_purchase_contract_main, planNum);

                this.ly_purchase_contract_mainBindingSource.Position = this.ly_purchase_contract_mainBindingSource.Find("id", nowcontractId);

               
                ly_purchase_contract_mainDataGridView.CurrentCell = ly_purchase_contract_mainDataGridView.CurrentRow.Cells[nowOwningColumnName];
            }

            dgv = ly_purchase_contract_mainDataGridView;

            ///////////////////////////////////////////////////////

            if ("True" == dgv.CurrentRow.Cells["批准"].Value.ToString())
            {
                MessageBox.Show("合同已经审批,不能修改数据...", "注意");
                return;
            
            }

            /////////////////////////////
            if ("税率0" == dgv.CurrentCell.OwningColumn.Name)
            {

                ChangeValue queryForm = new ChangeValue();

                queryForm.OldValue = dgv.CurrentCell.Value.ToString();
                queryForm.NewValue = "";
                queryForm.ChangeMode = "value";
                queryForm.ShowDialog();




                if (queryForm.NewValue != "")
                {
                    dgv.CurrentRow.Cells["税率0"].Value = queryForm.NewValue;
                    //dgv.CurrentRow.Cells["discount_money"].Value = DBNull.Value;

                    //dgv.CurrentRow.Cells["approve_flag"].Value = "False";
                    SaveContract();

                    //CountPlanStru();

                }
                else
                {
                    dgv.CurrentRow.Cells["税率0"].Value = 0;
                    //dgv.CurrentRow.Cells["discount_money"].Value = DBNull.Value;

                    //dgv.CurrentRow.Cells["approve_flag"].Value = "False";
                    SaveContract();
                }
                return;

            }

            /////////////////////////////
            if ("发票类别" == dgv.CurrentCell.OwningColumn.Name)
            {


                string sel = "SELECT  id as 编号, tax_type_name as 发票类型 FROM ly_tax_type  ";
                QueryForm queryForm = new QueryForm();


                queryForm.Sel = sel;
                queryForm.Constr = SQLDatabase.Connectstring;

                queryForm.ShowDialog();


                if (queryForm.Result != "")
                {

                    dgv.CurrentRow.Cells["发票类别"].Value = queryForm.Result1;


                }
                //else
                //{
                //    dgv.CurrentRow.Cells["发票类别"].Value = DBNull.Value;

                //}
                SaveContract();
                return;
            }
            /// /////////////////////////////

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


            if ("票到日期" == dgv.CurrentCell.OwningColumn.Name)
            {

                ChangeValue queryForm = new ChangeValue();

                queryForm.OldValue = dgv.CurrentCell.Value.ToString();
                queryForm.NewValue = "";
                queryForm.ChangeMode = "datetime";
                queryForm.ShowDialog();




                if (queryForm.NewValue != "")
                {
                    dgv.CurrentRow.Cells["票到日期"].Value = queryForm.NewValue;
                    dgv.CurrentRow.Cells["票到录入人"].Value = SQLDatabase.nowUserName();
                    //dgv.CurrentRow.Cells["discount_money"].Value = DBNull.Value;

                    //dgv.CurrentRow.Cells["approve_flag"].Value = "False";
                    SaveContract();


                    //CountPlanStru();

                }
                else
                {
                    dgv.CurrentRow.Cells["票到日期"].Value = DBNull.Value;
                    dgv.CurrentRow.Cells["票到录入人"].Value = DBNull.Value;
                    SaveContract();


                }
                return;

            }
            /////////////////////////////
            if ("账期" == dgv.CurrentCell.OwningColumn.Name)
            {

                ChangeValue queryForm = new ChangeValue();

                queryForm.OldValue = dgv.CurrentCell.Value.ToString();
                queryForm.NewValue = "";
                queryForm.ChangeMode = "number";
                queryForm.ShowDialog();




                if (queryForm.NewValue != "")
                {
                    dgv.CurrentRow.Cells["账期"].Value = queryForm.NewValue;
                    //dgv.CurrentRow.Cells["discount_money"].Value = DBNull.Value;

                    //dgv.CurrentRow.Cells["approve_flag"].Value = "False";
                    SaveContract();

                    //CountPlanStru();

                }
                else
                {
                    dgv.CurrentRow.Cells["账期"].Value = DBNull.Value;
                    SaveContract();

                    ;
                }
                return;

            }

            /////////////////////////////
            if ("运费" == dgv.CurrentCell.OwningColumn.Name)
            {

                ChangeValue queryForm = new ChangeValue();

                queryForm.OldValue = dgv.CurrentCell.Value.ToString();
                queryForm.NewValue = "";
                queryForm.ChangeMode = "number";
                queryForm.ShowDialog();




                if (queryForm.NewValue != "")
                {
                    dgv.CurrentRow.Cells["运费"].Value = queryForm.NewValue;
                    //dgv.CurrentRow.Cells["discount_money"].Value = DBNull.Value;

                    //dgv.CurrentRow.Cells["approve_flag"].Value = "False";
                    SaveContract();

                    //CountPlanStru();

                }
                else
                {
                    dgv.CurrentRow.Cells["运费"].Value = DBNull.Value;
                    SaveContract();

                    ;
                }
                return;

            }
            
            ///////////////////////////////////////////////////////////

            //if ("电话" == dgv.CurrentCell.OwningColumn.Name)
            //{

            //    ChangeValue queryForm = new ChangeValue();

            //    queryForm.OldValue = dgv.CurrentCell.Value.ToString();
            //    queryForm.NewValue = "";
            //    queryForm.ChangeMode = "string";
            //    queryForm.ShowDialog();




            //    if (queryForm.NewValue != "")
            //    {
            //        dgv.CurrentRow.Cells["电话"].Value = queryForm.NewValue;
            //        //dgv.CurrentRow.Cells["discount_money"].Value = DBNull.Value;

            //        //dgv.CurrentRow.Cells["approve_flag"].Value = "False";
            //        //SaveContract();

            //        //CountPlanStru();

            //    }
            //    else
            //    {
            //        dgv.CurrentRow.Cells["电话"].Value = DBNull.Value;
            //        //SaveContract();

                   
            //    }
            //    return;

            //}
            ////、ly_supplier_list_forContract
            /////////////////////////////////////////////////////////////
            /////////////////////////////////////////////////////////////////

            if ("电话" == dgv.CurrentCell.OwningColumn.Name)
            {

                ChangeValue queryForm = new ChangeValue();

                queryForm.OldValue = dgv.CurrentCell.Value.ToString();
                queryForm.NewValue = "";
                queryForm.ChangeMode = "string";
                queryForm.ShowDialog();


                string updstr;

                if (queryForm.NewValue != "")
                {
                    dgv.CurrentRow.Cells["电话"].Value = queryForm.NewValue; 

                    updstr = " update ly_supplier_list_forContract  " +
                              "  set supplier_phone='" + queryForm.NewValue + "' where  contract_code='" + dgv.CurrentRow.Cells["合同编号"].Value.ToString() + "'";



                }
                else
                {

                    dgv.CurrentRow.Cells["电话"].Value = DBNull.Value;

                    updstr = " update ly_supplier_list_forContract  " +
                              "  set supplier_phone= null where  contract_code='" + dgv.CurrentRow.Cells["合同编号"].Value.ToString() + "'";


                }










                SqlConnection sqlConnection1 = new SqlConnection(SQLDatabase.Connectstring);
                SqlCommand cmd = new SqlCommand();

                cmd.CommandText = updstr;
                cmd.CommandType = CommandType.Text;
                cmd.Connection = sqlConnection1;

                //sqlConnection1.Open();
                //cmd.ExecuteNonQuery();
                //sqlConnection1.Close();

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



                ////////////////////////////



                return;

            }
            ////////////////////////////////////////////////////////////////////////
            ///////////////////////////////////////////////////////////////

            if ("传真" == dgv.CurrentCell.OwningColumn.Name)
            {

                ChangeValue queryForm = new ChangeValue();

                queryForm.OldValue = dgv.CurrentCell.Value.ToString();
                queryForm.NewValue = "";
                queryForm.ChangeMode = "string";
                queryForm.ShowDialog();


                string updstr;

                if (queryForm.NewValue != "")
                {
                    dgv.CurrentRow.Cells["传真"].Value = queryForm.NewValue;

                    updstr = " update ly_supplier_list_forContract  " +
                              "  set fax_num='" + queryForm.NewValue + "' where  contract_code='" + dgv.CurrentRow.Cells["合同编号"].Value.ToString() + "'";



                }
                else
                {

                    dgv.CurrentRow.Cells["传真"].Value = DBNull.Value;

                    updstr = " update ly_supplier_list_forContract  " +
                              "  set fax_num= null where  contract_code='" + dgv.CurrentRow.Cells["合同编号"].Value.ToString() + "'";


                }










                SqlConnection sqlConnection1 = new SqlConnection(SQLDatabase.Connectstring);
                SqlCommand cmd = new SqlCommand();

                cmd.CommandText = updstr;
                cmd.CommandType = CommandType.Text;
                cmd.Connection = sqlConnection1;

                //sqlConnection1.Open();
                //cmd.ExecuteNonQuery();
                //sqlConnection1.Close();

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



                ////////////////////////////



                return;

            }
            ////////////////////////////////////////////////////////////////////////
            ///////////////////////////////////////////////////////////////

            if ("联系人" == dgv.CurrentCell.OwningColumn.Name)
            {

                ChangeValue queryForm = new ChangeValue();

                queryForm.OldValue = dgv.CurrentCell.Value.ToString();
                queryForm.NewValue = "";
                queryForm.ChangeMode = "string";
                queryForm.ShowDialog();


                string updstr;

                if (queryForm.NewValue != "")
                {
                    dgv.CurrentRow.Cells["联系人"].Value = queryForm.NewValue;

                    updstr = " update ly_supplier_list_forContract  " +
                              "  set supplier_contacts='" + queryForm.NewValue + "' where  contract_code='" + dgv.CurrentRow.Cells["合同编号"].Value.ToString() + "'";



                }
                else
                {

                    dgv.CurrentRow.Cells["联系人"].Value = DBNull.Value;

                    updstr = " update ly_supplier_list_forContract  " +
                              "  set supplier_contacts= null where  contract_code='" + dgv.CurrentRow.Cells["合同编号"].Value.ToString() + "'";


                }










                SqlConnection sqlConnection1 = new SqlConnection(SQLDatabase.Connectstring);
                SqlCommand cmd = new SqlCommand();

                cmd.CommandText = updstr;
                cmd.CommandType = CommandType.Text;
                cmd.Connection = sqlConnection1;

                //sqlConnection1.Open();
                //cmd.ExecuteNonQuery();
                //sqlConnection1.Close();

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



                ////////////////////////////



                return;

            }
            ////////////////////////////////////////////////////////////////////////
        }

        private void bindingNavigatorDeleteItem1_Click(object sender, EventArgs e)
        {
            DataGridView dgv =  ly_purchase_contract_mainDataGridView;

            if (null == dgv.CurrentRow) return;

            if (ly_cg_fk.Rows.Count > 0)
            {
                MessageBox.Show("已有预付,不能删除数据...", "注意");
                return;
            }


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
                this.ly_purchase_contract_mainTableAdapter.Fill(this.lYMaterielRequirements.ly_purchase_contract_main, planNum);

                this.ly_purchase_contract_mainBindingSource.Position = this.ly_purchase_contract_mainBindingSource.Find("id", nowcontractId);


                ly_purchase_contract_mainDataGridView.CurrentCell = ly_purchase_contract_mainDataGridView.CurrentRow.Cells[nowOwningColumnName];
            }


            decimal nowinputcount = 0;



            foreach (DataGridViewRow dgr in ly_purchase_contract_detailDataGridView.Rows)
            {

                if (!string.IsNullOrEmpty(ly_purchase_contract_detailDataGridView.CurrentRow.Cells["入库数量1"].Value.ToString()))
                {
                    nowinputcount = nowinputcount + decimal.Parse(dgr.Cells["入库数量1"].Value.ToString());
                }


            }

            if (nowinputcount > 0)
            {
                MessageBox.Show("已有入库记录,不能删除数据...", "注意");
                return;
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
                this.ly_purchase_contract_mainBindingSource.RemoveCurrent();



                SaveContract();

            }



           
        }

        private void ly_purchase_contract_detailDataGridView_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
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
                this.ly_purchase_contract_mainTableAdapter.Fill(this.lYMaterielRequirements.ly_purchase_contract_main, planNum);

                this.ly_purchase_contract_mainBindingSource.Position = this.ly_purchase_contract_mainBindingSource.Find("id", nowcontractId);
                this.ly_purchase_contract_detailBindingSource.Position = this.ly_purchase_contract_detailBindingSource.Find("id", nowcontractdetailId);

               
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
                    dgv.CurrentRow.Cells["备注7"].Value = DBNull.Value;
                    SaveContractdetail();
                }
                return;

            }

            if ("tax_type" == dgv.CurrentCell.OwningColumn.Name)
            {


                string sel = "SELECT  id as 编号, tax_type_name as 发票类型 FROM ly_tax_type  ";
                QueryForm queryForm = new QueryForm();


                queryForm.Sel = sel;
                queryForm.Constr = SQLDatabase.Connectstring;

                queryForm.ShowDialog();


                if (queryForm.Result != "")
                {

                    dgv.CurrentRow.Cells["tax_type"].Value = queryForm.Result1;


                }
                else
                {
                    dgv.CurrentRow.Cells["tax_type"].Value = DBNull.Value;

                }
                SaveContractdetail();
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
                    dgv.CurrentRow.Cells["合同单价"].Value =0;
                    //dgv.CurrentRow.Cells["discount_money"].Value = DBNull.Value;

                    //dgv.CurrentRow.Cells["approve_flag"].Value = "False";
                    SaveContractdetail();
                }
                return;

            }

            /////////////////////////////
            if ("税率" == dgv.CurrentCell.OwningColumn.Name)
            {

                ChangeValue queryForm = new ChangeValue();

                queryForm.OldValue = dgv.CurrentCell.Value.ToString();
                queryForm.NewValue = "";
                queryForm.ChangeMode = "value";
                queryForm.ShowDialog();




                if (queryForm.NewValue != "")
                {
                    dgv.CurrentRow.Cells["税率"].Value = queryForm.NewValue;
                    //dgv.CurrentRow.Cells["discount_money"].Value = DBNull.Value;

                    //dgv.CurrentRow.Cells["approve_flag"].Value = "False";
                    SaveContractdetail();

                    //CountPlanStru();

                }
                else
                {
                    dgv.CurrentRow.Cells["税率"].Value = 0;
                    //dgv.CurrentRow.Cells["discount_money"].Value = DBNull.Value;

                    //dgv.CurrentRow.Cells["approve_flag"].Value = "False";
                    SaveContractdetail();
                }
                return;

            }

            /////////////////////////////


            if ("到货日期" == dgv.CurrentCell.OwningColumn.Name)
            {
               
                ChangeValue queryForm = new ChangeValue();

                queryForm.OldValue = dgv.CurrentCell.Value.ToString();
                queryForm.NewValue = "";
                queryForm.ChangeMode = "datetime";
                queryForm.ShowDialog();




                if (queryForm.NewValue != "")
                {
                    dgv.CurrentRow.Cells["到货日期"].Value = queryForm.NewValue;
                    //dgv.CurrentRow.Cells["票到录入人"].Value = SQLDatabase.nowUserName();
                    //dgv.CurrentRow.Cells["discount_money"].Value = DBNull.Value;

                    //dgv.CurrentRow.Cells["approve_flag"].Value = "False";
                    SaveContractdetail();


                    //CountPlanStru();

                }
                else
                {
                    dgv.CurrentRow.Cells["到货日期"].Value = DBNull.Value;
                    //dgv.CurrentRow.Cells["票到录入人"].Value = DBNull.Value;
                    SaveContractdetail();


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


                      if (newqty > (notarrangeqty + oldqty))
                      {
                          MessageBox.Show("修改数量超出计划需求数量,操作取消...", "注意");
                          return;


                      }

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
            /////////////////////////////
            if ("工程费" == dgv.CurrentCell.OwningColumn.Name)
            {

                ChangeValue queryForm = new ChangeValue();

                queryForm.OldValue = dgv.CurrentCell.Value.ToString();
                queryForm.NewValue = "";
                queryForm.ChangeMode = "value";
                queryForm.ShowDialog();




                if (queryForm.NewValue != "")
                {
                    dgv.CurrentRow.Cells["工程费"].Value = queryForm.NewValue;
                    //dgv.CurrentRow.Cells["discount_money"].Value = DBNull.Value;

                    //dgv.CurrentRow.Cells["approve_flag"].Value = "False";
                    SaveContractdetail();

                    //CountPlanStru();

                }
                else
                {
                    dgv.CurrentRow.Cells["工程费"].Value = 0;
                    //dgv.CurrentRow.Cells["discount_money"].Value = DBNull.Value;

                    //dgv.CurrentRow.Cells["approve_flag"].Value = "False";
                    SaveContractdetail();
                }
                return;

            }

        ////////////////////////////////////////////////////////////////

        }

        private void lY_MaterielRequirementsPurchaseSupplierDataGridView_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            DataGridView dgv = lY_MaterielRequirementsPurchaseSupplierDataGridView;

            if ("True" == ly_purchase_contract_mainDataGridView.CurrentRow.Cells["批准"].Value.ToString())
            {
                MessageBox.Show("合同已经审批,不能修改数据...", "注意");
                return;

            }

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
                /////////////////
                string nowid2;
                if (null != dgv.CurrentRow)
                {
                    nowid2 = dgv.CurrentRow.Cells["id28"].Value.ToString();
                }
                else
                {
                    nowid2 = "";
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

                string nowinstoreflag;
                if (null != dgv.CurrentRow)
                {
                    nowinstoreflag = dgv.CurrentRow.Cells["入库8"].Value.ToString();
                }
                else
                {
                    nowinstoreflag = "";
                }

                /////////////////////

                string nowitemno;
                if (null != dgv.CurrentRow)
                {
                    nowitemno = dgv.CurrentRow.Cells["物料编码8"].Value.ToString();
                }
                else
                {
                    nowitemno = "";
                }

                int hadarranged = ly_purchase_contract_detailBindingSource.Find("物料编号", nowitemno);

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
                    
                    this.ly_purchase_contract_detailBindingSource.AddNew();

                    this.ly_purchase_contract_detailDataGridView.CurrentRow.Cells["合同编号7"].Value = nowcontractcode;
                    this.ly_purchase_contract_detailDataGridView.CurrentRow.Cells["物料编号7"].Value = nowitemno;
                    this.ly_purchase_contract_detailDataGridView.CurrentRow.Cells["数量7"].Value = notarrangeqty;
                    this.ly_purchase_contract_detailDataGridView.CurrentRow.Cells["采购数量"].Value = notarrangeqty;

                    this.ly_purchase_contract_detailDataGridView.CurrentRow.Cells["id27"].Value = nowid2;
                    this.ly_purchase_contract_detailDataGridView.CurrentRow.Cells["方式7"].Value = nowpartname;
                    this.ly_purchase_contract_detailDataGridView.CurrentRow.Cells["入库7"].Value = nowinstoreflag;

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
                this.ly_purchase_contract_mainTableAdapter.Fill(this.lYMaterielRequirements.ly_purchase_contract_main, planNum);

                this.ly_purchase_contract_mainBindingSource.Position = this.ly_purchase_contract_mainBindingSource.Find("id", nowcontractId);
                this.ly_purchase_contract_detailBindingSource.Position = this.ly_purchase_contract_detailBindingSource.Find("id", nowcontractdetailId);


                ly_purchase_contract_detailDataGridView.CurrentCell = ly_purchase_contract_detailDataGridView.CurrentRow.Cells[nowOwningColumnName];
            }

            decimal nowinputcount = 0;

            if (!string.IsNullOrEmpty(ly_purchase_contract_detailDataGridView.CurrentRow.Cells["入库数量1"].Value.ToString()))
            {
                nowinputcount = decimal.Parse(ly_purchase_contract_detailDataGridView.CurrentRow.Cells["入库数量1"].Value.ToString());
            }

            if (nowinputcount > 0)
            {
                MessageBox.Show("已有入库记录,不能删除数据...", "注意");
                return;
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
                this.ly_purchase_contract_detailBindingSource.RemoveCurrent();



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

            if ("True" != ly_purchase_contract_mainDataGridView.CurrentRow.Cells["批准"].Value.ToString())
            {
                MessageBox.Show("合同未经生产审批,不能打印...", "注意");
                return;

            }
            if ("True" != ly_purchase_contract_mainDataGridView.CurrentRow.Cells["审核"].Value.ToString())
            {
                MessageBox.Show("合同未经质检审核,不能打印...", "注意");
                return;

            }




            //NewFrm.Show(this);2018-4-3(this); 

            //this.ly_plan_getmaterialTableAdapter.Fill(this.lYPlanMange.ly_plan_getmaterial, parentId);

            BaseReportView queryForm = new BaseReportView();

            queryForm.Text = "中原精密采购订单";

            queryForm.Printdata = this.lYMaterielRequirements;

            queryForm.PrintCrystalReport = new LY_CaigouDingdan();


            //string selectFormula;

            //selectFormula = "{ly_store_planitemcount.状态}  =   '原料'  and {ly_store_planitemcount.欠料金额}>0 ";
            //queryForm.PrintCrystalReport.DataDefinition.RecordSelectionFormula = selectFormula;

            //NewFrm.Hide(this);2018-4-3

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
            queryForm.Sortmode = "CG";
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

            this.ly_purchase_contract_mainBindingSource.Position = this.ly_purchase_contract_mainBindingSource.Find("合同编号", nowcontractcode);
            this.ly_purchase_contract_detailBindingSource.Position = this.ly_purchase_contract_detailBindingSource.Find("物料编号", nowitemno);

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

        private void 增加部分加工供应商ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (null == ly_materiel_supplierDataGridView.CurrentRow) return;

            int parentId = int.Parse(this.ly_material_plan_mainDataGridView.CurrentRow.Cells["parentid"].Value.ToString());

            string nowsupplierCode = this.ly_materiel_supplierDataGridView.CurrentRow.Cells["供应商编码2"].Value.ToString();

            string nowsplanNum = this.lY_MaterielRequirementsDataGridView.CurrentRow.Cells["计划编号1"].Value.ToString();

            string nowitemno = this.lY_MaterielRequirementsDataGridView.CurrentRow.Cells["物料编码1"].Value.ToString();

            string nowQty = this.lY_MaterielRequirementsDataGridView.CurrentRow.Cells["需求数量1"].Value.ToString();



            int hadarrenged = this.ly_purchase_prepareforplanBindingSource.Find("findcode", nowsupplierCode + nowitemno);

            if (0 <= hadarrenged)
            {
                MessageBox.Show("该物料供应商已有选择,不能重复添加", "注意");
                return;

            }

            //if (0 >= decimal.Parse(nowQty))
            //{
            //    MessageBox.Show("未排数量为0,操作取消", "注意");
            //    return;

            //}



            string insStr = " INSERT INTO ly_purchase_prepareforplan  " +
               "( plan_num,sortcode,itemno,qty,purchase_qty,supplier_code) " +
               " values ('" + nowsplanNum + "','" + "3" + "','" + nowitemno + "'," + nowQty + "," + nowQty + ",'" + nowsupplierCode + "' )";


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

                //
            }

            this.ly_purchase_prepareforplanTableAdapter.Fill(this.lYMaterielRequirements.ly_purchase_prepareforplan, nowsplanNum, "3");
            this.ly_purchase_prepareforplanBindingSource.Position = this.ly_purchase_prepareforplanBindingSource.Find("findcode", nowsupplierCode + nowitemno);



            ////NewFrm.Show(this);2018-4-3(this.ParentForm);
            this.lY_MaterielRequirementsPurchaseTableAdapter.Fill(this.lYMaterielRequirements.LY_MaterielRequirementsPurchase, parentId, "外购");
            //NewFrm.Hide(this.ParentForm);



            this.lY_MaterielRequirementsPurchaseBindingSource.Position = this.lY_MaterielRequirementsPurchaseBindingSource.Find("物料编码", nowitemno);
        }

        private void 统一指定选中物料供应商ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!SQLDatabase.CheckHaveRight(SQLDatabase.NowUserID, "物料供应商设置"))
            {
                MessageBox.Show("没有物料供应商设置权限...", "注意");
                return;
            }
            SetMaterielSupplier();
        }

        private string SetMaterielSupplier()
        {
            DataGridView dgv = null;


            dgv = this.lY_MaterielRequirementsDataGridView;

            string nowsupplier_code = "";

            string itemno;
            int id2;

            if (null == dgv.CurrentRow) return nowsupplier_code;

            string message = "确定统一指定选中物料供应商吗？";
            string caption = "提示...";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result;



            result = MessageBox.Show(message, caption, buttons,
            MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

            if (result == DialogResult.Yes)
            {

                //////////////////////////////////////////////////



                string sel = "SELECT  supplier_code as 编号, supplier_name as 名称,supplier_py as 全拼,supplier_jp as 简拼 FROM ly_supplier_list WHERE (sort_code = '3')";




                QueryForm queryForm = new QueryForm();


                queryForm.Sel = sel;
                queryForm.Nodiscol = 2;
                queryForm.Constr = SQLDatabase.Connectstring;


                queryForm.ShowDialog();


                if (queryForm.Result != "")
                {

                    nowsupplier_code = queryForm.Result;



                }
                else
                {
                    nowsupplier_code = "";
                    return nowsupplier_code;
                }

                ///////////////////////////////////////////////////




                foreach (DataGridViewRow dgr in dgv.Rows)
                {
                    if (true == dgr.Selected)
                    {
                        if (string.IsNullOrEmpty(nowsupplier_code))
                        {
                            break;

                        }

                        itemno = dgr.Cells["物料编码1"].Value.ToString();

                        id2 = int.Parse(dgr.Cells["id2"].Value.ToString());

                        //NewFrm.Show(this);2018-4-3(this);

                        SqlConnection sqlConnection1 = new SqlConnection(SQLDatabase.Connectstring);
                        SqlCommand cmd = new SqlCommand();



                        cmd.Parameters.Add("@supplier_code", SqlDbType.VarChar);
                        cmd.Parameters["@supplier_code"].Value = nowsupplier_code;

                        cmd.Parameters.Add("@itemno", SqlDbType.VarChar);
                        cmd.Parameters["@itemno"].Value = itemno;

                        cmd.Parameters.Add("@id2", SqlDbType.Int);
                        cmd.Parameters["@id2"].Value = id2;




                        cmd.CommandText = "LY_set_Materiel_Supplier";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Connection = sqlConnection1;
                        cmd.CommandTimeout = 0;

                        sqlConnection1.Open();
                        cmd.ExecuteNonQuery();
                        sqlConnection1.Close();


                        //ly_material_plan_explodeTableAdapter.Fill(this.lYPlanMange.ly_material_plan_explode, parentId);
                        //this.ly_store_planitemcountTableAdapter.Fill(this.lYPlanMange.ly_store_planitemcount, parentId);
                        //this.ly_plan_getmaterial_departmentTableAdapter.Fill(this.lYPlanMange.ly_plan_getmaterial_department , planNum);

                        //NewFrm.Hide(this);2018-4-3


                    }
                }
            }

            return nowsupplier_code;
        }

        private void 统一指定选中物料供应商并生成草案ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!SQLDatabase.CheckHaveRight(SQLDatabase.NowUserID, "物料供应商设置"))
            {
                MessageBox.Show("没有物料供应商设置权限...", "注意");
                return;
            }
            
            string nowsupplier_code = SetMaterielSupplier();

            if ("" == nowsupplier_code) return;

            DataGridView dgv = null;


            dgv = this.lY_MaterielRequirementsDataGridView;

            int parentId;

            string nowsupplierCode = "";

            string nowsplanNum;

            string nowitemno = "";

            string nowQty;

            string nowid2;

            string nowpartname;

            string nowinstoreflag;

            parentId = int.Parse(this.ly_material_plan_mainDataGridView.CurrentRow.Cells["parentid"].Value.ToString());
            nowsplanNum = dgv.CurrentRow.Cells["计划编号1"].Value.ToString();
            foreach (DataGridViewRow dgr in dgv.Rows)
            {
                if (true == dgr.Selected)
                {



                    parentId = int.Parse(this.ly_material_plan_mainDataGridView.CurrentRow.Cells["parentid"].Value.ToString());

                    nowsupplierCode = nowsupplier_code;

                    nowsplanNum = dgr.Cells["计划编号1"].Value.ToString();

                    nowitemno = dgr.Cells["物料编码1"].Value.ToString();

                    nowQty = dgr.Cells["未排数量1"].Value.ToString();

                    nowid2 = dgr.Cells["id2"].Value.ToString();

                    nowpartname = dgr.Cells["方式"].Value.ToString();

                    nowinstoreflag = dgr.Cells["入库"].Value.ToString();


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
                        continue;

                    }

                    if (0 >= decimal.Parse(nowQty))
                    {
                        continue;

                    }



                    string insStr = " INSERT INTO ly_purchase_prepareforplan  " +
                      "( plan_num,sortcode,itemno,qty,purchase_qty,supplier_code,id2,part_name,instore_flag,buyer) " +
                      " values ('" + nowsplanNum + "','" + "3" + "','" + nowitemno + "'," + nowQty + "," + nowQty + ",'" +
                      nowsupplierCode + "'," + nowid2 + ",'" + nowpartname + "'," + nowinstoreflag + ",'"+ SQLDatabase.nowUserName() + "')";



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
            }

            this.ly_purchase_prepareforplanTableAdapter.Fill(this.lYMaterielRequirements.ly_purchase_prepareforplan, nowsplanNum, "3");
            this.ly_purchase_prepareforplanBindingSource.Position = this.ly_purchase_prepareforplanBindingSource.Find("findcode", nowsupplierCode + nowitemno);



            ////NewFrm.Show(this);2018-4-3(this.ParentForm);
            this.lY_MaterielRequirementsPurchaseTableAdapter.Fill(this.lYMaterielRequirements.LY_MaterielRequirementsPurchase, parentId, "外购");
            //NewFrm.Hide(this.ParentForm);



            this.lY_MaterielRequirementsPurchaseBindingSource.Position = this.lY_MaterielRequirementsPurchaseBindingSource.Find("物料编码", nowitemno);
        }

        private void toolStripButton37_Click(object sender, EventArgs e)
        {
            if ("显示全部" == toolStripButton37.Text)
            {
                toolStripButton37.Text = "显示未排";
                this.lY_MaterielRequirementsPurchaseBindingSource.Filter = "未排数量 > 0 and buyer_code='" + SQLDatabase.NowUserID + "' ";
            }
            else
            {
                toolStripButton37.Text = "显示全部";
                this.lY_MaterielRequirementsPurchaseBindingSource.Filter = " buyer_code='" + SQLDatabase.NowUserID + "' ";
            }
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            ExportDataGridviewTOExcell.ExportDataGridview(this.lY_MaterielRequirementsDataGridView, true);
        }

        private void toolStripButton12_Click(object sender, EventArgs e)
        {
            if (null == this.ly_purchase_contract_detailDataGridView.CurrentRow) return;

            if ("True" != ly_purchase_contract_mainDataGridView.CurrentRow.Cells["批准"].Value.ToString())
            {
                MessageBox.Show("合同未经生产审批,不能打印...", "注意");
                return;

            }

            string nowcontractcode = this.ly_purchase_contract_mainDataGridView.CurrentRow.Cells["合同编号"].Value.ToString();


            //NewFrm.Show(this);2018-4-3(this); ;

            //this.ly_plan_getmaterialTableAdapter.Fill(this.lYPlanMange.ly_plan_getmaterial, parentId); engineering cost

            BaseReportView queryForm = new BaseReportView();

            queryForm.Text = "中原精密采购合同";

            queryForm.Printdata = this.lYMaterielRequirements;
            queryForm.PrintCrystalReport = new LY_WaixiePurchaseHetong();

            queryForm.CrystalReportViewer1.ShowExportButton = false;

            queryForm.Outfilename = nowcontractcode;


            //string selectFormula;

            //selectFormula = "{ly_store_planitemcount.状态}  =   '原料'  and {ly_store_planitemcount.欠料金额}>0 ";
            //queryForm.PrintCrystalReport.DataDefinition.RecordSelectionFormula = selectFormula;

            //NewFrm.Hide(this);2018-4-3

            queryForm.ShowDialog();
        }

        private void groupBox5_Enter(object sender, EventArgs e)
        {

        }

        private void ly_sales_contract_terms_forcontractDataGridView_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            DataGridView dgv = sender as DataGridView;

            if (null == dgv.CurrentRow) return;

            if ("True" == ly_purchase_contract_mainDataGridView.CurrentRow.Cells["批准"].Value.ToString())
            {
                MessageBox.Show("合同已经生产审批,不能修改条款...", "注意");
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
                    this.ly_contract_terms_forpurchaseBindingSource1.EndEdit();
                    this.ly_contract_terms_forpurchaseTableAdapter1.Update(this.lYMaterielRequirements.ly_contract_terms_forpurchase);

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
                    this.ly_contract_terms_forpurchaseBindingSource1.EndEdit();
                    this.ly_contract_terms_forpurchaseTableAdapter1.Update(this.lYMaterielRequirements.ly_contract_terms_forpurchase);
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
                    this.ly_contract_terms_forpurchaseBindingSource1.EndEdit();
                    this.ly_contract_terms_forpurchaseTableAdapter1.Update(this.lYMaterielRequirements.ly_contract_terms_forpurchase);
                    //CountPlanStru();

                }
                else
                {

                }
                return;

            }


            ///////////////////////////////////////////////////////
        }

        private void 导入合同标准条款ToolStripMenuItem_Click(object sender, EventArgs e)
        {

            //if ("True" == ly_purchase_contract_mainDataGridView.CurrentRow.Cells["批准"].Value.ToString())
            //{
            //    MessageBox.Show("合同已经生产审批,不能修改条款...", "注意");
            //    return;

            //}

            if ("True" == ly_purchase_contract_mainDataGridView.CurrentRow.Cells["批准"].Value.ToString())
            {
                MessageBox.Show("合同已经生产审批,不能修改条款...", "注意");
                return;

            }


            string message = "导入供应商标准合同条款吗吗？";
            string caption = "提示...";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result;



            result = MessageBox.Show(message, caption, buttons,
            MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

            if (result == DialogResult.Yes)
            {

                string nowcontractcode = this.ly_purchase_contract_mainDataGridView.CurrentRow.Cells["合同编号"].Value.ToString();

               
                string nowsuppliercode = this.ly_purchase_contract_mainDataGridView.CurrentRow.Cells["供应商编号6"].Value.ToString();



                Get_ContractSet(nowcontractcode, nowsuppliercode);


                this.ly_contract_terms_forpurchaseTableAdapter1.Fill(this.lYMaterielRequirements.ly_contract_terms_forpurchase, nowcontractcode);
             
             

                MessageBox.Show("导入合同标准条款成功!", "注意");
            }
        }

        private void Get_ContractSet(string nowcontractcode, string nowsuppliercode)
        {
            SqlConnection sqlConnection1 = new SqlConnection(SQLDatabase.Connectstring);
            SqlCommand cmd = new SqlCommand();

            //string nowcontractcode = this.ly_purchase_contract_mainDataGridView.CurrentRow.Cells["合同编号"].Value.ToString();

            cmd.Parameters.Add("@contract_code", SqlDbType.VarChar);
            cmd.Parameters["@contract_code"].Value = nowcontractcode;

            cmd.Parameters.Add("@contract_type", SqlDbType.VarChar );
            cmd.Parameters["@contract_type"].Value = "purchase";

            cmd.Parameters.Add("@supplier_code", SqlDbType.VarChar);
            cmd.Parameters["@supplier_code"].Value = nowsuppliercode;




            cmd.CommandText = "LY_contract_termscopy_new";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = sqlConnection1;

            sqlConnection1.Open();
            cmd.ExecuteNonQuery();
            sqlConnection1.Close();



        }

        private void Set_ContractTerm(string nowcontractcode, string nowsuppliercode)
        {
            SqlConnection sqlConnection1 = new SqlConnection(SQLDatabase.Connectstring);
            SqlCommand cmd = new SqlCommand();

            //string nowcontractcode = this.ly_purchase_contract_mainDataGridView.CurrentRow.Cells["合同编号"].Value.ToString();

            cmd.Parameters.Add("@contract_code", SqlDbType.VarChar);
            cmd.Parameters["@contract_code"].Value = nowcontractcode;

            cmd.Parameters.Add("@contract_type", SqlDbType.VarChar );
            cmd.Parameters["@contract_type"].Value = "purchase";

            cmd.Parameters.Add("@supplier_code", SqlDbType.VarChar);
            cmd.Parameters["@supplier_code"].Value = nowsuppliercode;




            cmd.CommandText = "LY_contract_termscopy_set";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = sqlConnection1;

            sqlConnection1.Open();
            cmd.ExecuteNonQuery();
            sqlConnection1.Close();



        }

        private void 导入公司ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //if ("True" == ly_purchase_contract_mainDataGridView.CurrentRow.Cells["批准"].Value.ToString())
            //{
            //    MessageBox.Show("合同已经生产审批,不能修改条款...", "注意");
            //    return;

            //}



            string message = "导入公司基本信息吗？";
            string caption = "提示...";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result;



            result = MessageBox.Show(message, caption, buttons,
            MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

            if (result == DialogResult.Yes)
            {

                string nowcontractcode = this.ly_purchase_contract_mainDataGridView.CurrentRow.Cells["合同编号"].Value.ToString();


                Get_CompanySet(nowcontractcode);


                this.ly_company_information_purchaseTableAdapter.Fill(this.lYMaterielRequirements.ly_company_information_purchase, nowcontractcode);



                MessageBox.Show("导入公司信息成功!", "注意");
            }
        }

        private void Get_CompanySet(string nowcontractcode)
        {
            SqlConnection sqlConnection1 = new SqlConnection(SQLDatabase.Connectstring);
            SqlCommand cmd = new SqlCommand();

            //string nowcontractcode = this.ly_purchase_contract_mainDataGridView.CurrentRow.Cells["合同编号"].Value.ToString();

            cmd.Parameters.Add("@contract_code", SqlDbType.VarChar);
            cmd.Parameters["@contract_code"].Value = nowcontractcode;

            cmd.Parameters.Add("@contract_type", SqlDbType.VarChar);
            cmd.Parameters["@contract_type"].Value = "purchase";




            cmd.CommandText = "LY_contract_CompanyInfo";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = sqlConnection1;

            sqlConnection1.Open();
            cmd.ExecuteNonQuery();
            sqlConnection1.Close();



        }

        private void ly_purchase_contract_mainDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void toolStripButton6_Click(object sender, EventArgs e)
        {

        }

        private void 保存为供应商标准条款ToolStripMenuItem_Click(object sender, EventArgs e)
        {
                  string message = "将当前合同条款保存为供应商标准条款吗？";
            string caption = "提示...";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result;



            result = MessageBox.Show(message, caption, buttons,
            MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

            if (result == DialogResult.Yes)
            {

                string nowcontractcode = this.ly_purchase_contract_mainDataGridView.CurrentRow.Cells["合同编号"].Value.ToString();


                string nowsuppliercode = this.ly_purchase_contract_mainDataGridView.CurrentRow.Cells["供应商编号6"].Value.ToString();



                Set_ContractTerm(nowcontractcode, nowsuppliercode);


                //this.ly_contract_terms_forpurchaseTableAdapter1.Fill(this.lYMaterielRequirements.ly_contract_terms_forpurchase, nowcontractcode);



                MessageBox.Show("保存合同标准条款成功!", "注意");
            }
        }

        //private void fillToolStripButton_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        this.ly_store_planitemcountTableAdapter.Fill(this.lYPlanMange.ly_store_planitemcount, new System.Nullable<int>(((int)(System.Convert.ChangeType(plan_idToolStripTextBox.Text, typeof(int))))));
        //    }
        //    catch (System.Exception ex)
        //    {
        //        System.Windows.Forms.MessageBox.Show(ex.Message);
        //    }

        //}

        private void toolStripButton21_Click(object sender, EventArgs e)
        {
            if (null == this.ly_store_planitemcountDataGridView1.CurrentRow) return;



            //string balanceFlag = this.xD_Sel_SellBalanceDataGridView.CurrentRow.Cells["结算"].Value.ToString();



            //NewFrm.Show(this);2018-4-3(this);


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

            //NewFrm.Hide(this);2018-4-3

            queryForm.ShowDialog();
        }

        private void toolStripButton46_Click(object sender, EventArgs e)
        {
            ExportDataGridviewTOExcell.ExportDataGridview(this.ly_store_planitemcountDataGridView1, true);
        }

        private void toolStripButton24_Click(object sender, EventArgs e)
        {
            int parentId = int.Parse(this.ly_material_plan_mainDataGridView.CurrentRow.Cells["parentid"].Value.ToString());
            //string planNum = this.ly_material_plan_mainDataGridView.CurrentRow.Cells["计划编号"].Value.ToString();


            //NewFrm.Show(this);2018-4-3(this);
            this.ly_store_planitemcountTableAdapter.Fill(this.lYPlanMange.ly_store_planitemcount, parentId);

            //NewFrm.Hide(this);2018-4-3
        }

        private void 统一制定交货日期ToolStripMenuItem_Click(object sender, EventArgs e)
        {

            ///////////////////////////////////////////////////////

            if ("True" == ly_purchase_contract_mainDataGridView.CurrentRow.Cells["批准"].Value.ToString())
            {
                MessageBox.Show("合同已经审批,不能修改数据...", "注意");
                return;

            }

            ///////////////////////////////////////////////////////////////
            
            DataGridView dgv = ly_purchase_contract_detailDataGridView; ;

            ChangeValue queryForm = new ChangeValue();

            queryForm.OldValue = dgv.CurrentRow.Cells["到货日期"].Value.ToString();
            queryForm.NewValue = "";
            queryForm.ChangeMode = "datetime";
            queryForm.ShowDialog();




            if (queryForm.NewValue != "")
            {
                foreach (DataGridViewRow dgr in dgv.Rows)
                {
                    dgr.Cells["到货日期"].Value = queryForm.NewValue;

                    //SaveContractdetail();
                }
            }
            else
            {
                foreach (DataGridViewRow dgr in dgv.Rows)
                {
                    dgr.Cells["到货日期"].Value = DBNull.Value;
                    //dgv.CurrentRow.Cells["票到录入人"].Value = DBNull.Value;
                    //SaveContractdetail();
                }


            }


            SaveContractdetail();

                  

                  
        }

        private void 统一设置备注ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ///////////////////////////////////////////////////////

            if ("True" == ly_purchase_contract_mainDataGridView.CurrentRow.Cells["批准"].Value.ToString())
            {
                MessageBox.Show("合同已经审批,不能修改数据...", "注意");
                return;

            }

            ///////////////////////////////////////////////////////////////

            DataGridView dgv = ly_purchase_contract_detailDataGridView; ;

            ChangeValue queryForm = new ChangeValue();

            queryForm.OldValue = dgv.CurrentRow.Cells["备注7"].Value.ToString();
            queryForm.NewValue = "";
            queryForm.ChangeMode = "longstring";
            queryForm.ShowDialog();




            if (queryForm.NewValue != "")
            {
                foreach (DataGridViewRow dgr in dgv.Rows)
                {
                    dgr.Cells["备注7"].Value = queryForm.NewValue;

                    //SaveContractdetail();
                }
            }
            else
            {
                foreach (DataGridViewRow dgr in dgv.Rows)
                {
                    dgr.Cells["备注7"].Value = DBNull.Value;
                    //dgv.CurrentRow.Cells["票到录入人"].Value = DBNull.Value;
                    //SaveContractdetail();备注7"longstring"
                }


            }


            SaveContractdetail();
        }

        private void toolStripButton7_Click(object sender, EventArgs e)
        {

            if (null == ly_purchase_contract_mainDataGridView.CurrentRow)
            {
                
                return;
            }


            string nowcontractcode = this.ly_purchase_contract_mainDataGridView.CurrentRow.Cells["合同编号"].Value.ToString();

            DateTime  nowsignDate;

            if (!string.IsNullOrEmpty(this.ly_purchase_contract_mainDataGridView.CurrentRow.Cells["签订日期"].Value.ToString()))
            {
                nowsignDate = DateTime.Parse(this.ly_purchase_contract_mainDataGridView.CurrentRow.Cells["签订日期"].Value.ToString());
            }
            else 
            {

                MessageBox.Show("合同无签订日期,不能收料...", "注意");
                return;
            }
            
            LY_Quality_Control_PurchaseREC queryForm = new LY_Quality_Control_PurchaseREC();

            queryForm.NowcontractNum = nowcontractcode;
            queryForm.NowsignDate = nowsignDate;
            queryForm.WindowState = FormWindowState.Maximized;



            queryForm.StartPosition = FormStartPosition.CenterParent;
            queryForm.ShowDialog(this);




            if (queryForm.DialogResult != DialogResult.Cancel)
            {
                //this.ly_inma0010ylTableAdapter.Fill(this.lYMaterialMange.ly_inma0010yl);
                //this.ly_inma0010ylBindingSource.Position = this.ly_inma0010ylBindingSource.Find("物资编号", queryForm.material_code);
            }
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            string sel = "SELECT   wzbh as 编号,mch as 名称,xhc as 型号,xhj as 日方,gg as 规格 FROM ly_inma0010";


            QueryForm queryForm = new QueryForm();


            queryForm.Sel = sel;
            queryForm.Constr = SQLDatabase.Connectstring;



            queryForm.ShowDialog();

           
            ///////////////////////////////

            string nowitemno;
            if (!string.IsNullOrEmpty(queryForm.Result))
            {
                nowitemno = queryForm.Result;
            }
            else
            {
                return;
            }

            string nowcontractcode;
            if (null != ly_purchase_contract_mainDataGridView.CurrentRow)
            {
                nowcontractcode = this.ly_purchase_contract_mainDataGridView.CurrentRow.Cells["合同编号"].Value.ToString();
            }
            else
            {
                nowcontractcode = "";
            }

            this.ly_purchase_contract_appendBindingSource.AddNew();

            this.ly_purchase_contract_appendDataGridView.CurrentRow.Cells["合同编号9"].Value = nowcontractcode;
            this.ly_purchase_contract_appendDataGridView.CurrentRow.Cells["物料编号9"].Value = nowitemno;
            this.ly_purchase_contract_appendDataGridView.CurrentRow.Cells["合同单价9"].Value =0;
            //this.ly_purchase_contract_appendDataGridView.CurrentRow.Cells["采购数量"].Value = notarrangeqty;
             
            //this.ly_purchase_contract_appendDataGridView.CurrentRow.Cells["id27"].Value = nowid2;
            this.ly_purchase_contract_appendDataGridView.CurrentRow.Cells["方式9"].Value = "附加";
            this.ly_purchase_contract_appendDataGridView.CurrentRow.Cells["入库9"].Value ="True";

            SaveContractappend();


        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (null == this.ly_purchase_contract_appendDataGridView.CurrentRow) return;
            DataGridView dgv = ly_purchase_contract_appendDataGridView;
            if (dgv.CurrentRow == null) return;

            if ("True" == dgv.CurrentRow.Cells["审批"].Value.ToString())
            {

                MessageBox.Show("已经审批,无法修改...");
                return;

            }

            this.ly_purchase_contract_appendBindingSource.RemoveCurrent();
            SaveContractappend();
        }

        private void ly_purchase_contract_appendDataGridView_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
           

           DataGridView dgv = ly_purchase_contract_appendDataGridView;
            if (dgv.CurrentRow == null) return;

            if ("True" == dgv.CurrentRow.Cells["审批"].Value.ToString())
            {

                MessageBox.Show("已经审批,无法修改...");
                return;

            }







            if ("采购数量9" == dgv.CurrentCell.OwningColumn.Name)
            {

                ChangeValue queryForm = new ChangeValue();

                queryForm.OldValue = dgv.CurrentCell.Value.ToString();
                queryForm.NewValue = "";
                queryForm.ChangeMode = "value";
                queryForm.ShowDialog();




                if (queryForm.NewValue != "")
                {
                    dgv.CurrentRow.Cells["采购数量9"].Value = queryForm.NewValue;
                    //dgv.CurrentRow.Cells["discount_money"].Value = DBNull.Value;

                    //dgv.CurrentRow.Cells["approve_flag"].Value = "False";
                    SaveContractappend();

                    //CountPlanStru();

                }
                else
                {
                    dgv.CurrentRow.Cells["采购数量9"].Value = 0;
                    //dgv.CurrentRow.Cells["discount_money"].Value = DBNull.Value;

                    //dgv.CurrentRow.Cells["approve_flag"].Value = "False";
                    SaveContractappend();
                }
                return;

            }

            /////////////////////////////
        }


        private void SaveModification(string nowcontractcode)
        {
           

            this.ly_purchase_contract_detail_modificationDataGridView.EndEdit();
            this.lypurchasecontractdetailmodificationBindingSource.EndEdit();

            
            this.ly_purchase_contract_detail_modificationTableAdapter.Update(this.lYMaterielRequirements.ly_purchase_contract_detail_modification);

            this.ly_purchase_contract_detail_modificationTableAdapter.Fill(this.lYMaterielRequirements.ly_purchase_contract_detail_modification, nowcontractcode);



        }

        private void bindingNavigatorAddNewItem4_Click(object sender, EventArgs e)
        {

            string nowcontractcode = this.ly_purchase_contract_mainDataGridView.CurrentRow.Cells["合同编号"].Value.ToString();
            string sel;



            sel = " SELECT a.id, a.contract_code, a.itemno, d.mch, d.jph,   d.xhc, d.gg,d.dw " +
                   "  FROM ly_purchase_contract_detail AS a LEFT OUTER JOIN ly_inma0010 AS d ON a.itemno = d.wzbh " +
                   "  WHERE(a.contract_code ='"+ nowcontractcode + "') and a.itemno not in (select itemno from ly_purchase_contract_detail_modification  WHERE contract_code ='"+ nowcontractcode + "') ";



            QueryForm queryForm = new QueryForm();


            queryForm.Sel = sel;
            queryForm.Constr = SQLDatabase.Connectstring;

            //Set the Column Collection to the filter Table
            //queryForm.SetSourceColumns(this.billMainDataSet.BalanceBill.Columns);

            queryForm.ShowDialog();


            if (null != queryForm.Result && queryForm.Result != "")
            {

                this.lypurchasecontractdetailmodificationBindingSource.AddNew();


                this .ly_purchase_contract_detail_modificationDataGridView.CurrentRow.Cells["detail_id"].Value = queryForm.Result;
                this.ly_purchase_contract_detail_modificationDataGridView.CurrentRow.Cells["contract_code"].Value = queryForm.Result1;
                this.ly_purchase_contract_detail_modificationDataGridView.CurrentRow.Cells["itemno"].Value = queryForm.Result2;
                //dgv.CurrentRow.Cells["discount_money"].Value = DBNull.Value;

                //dgv.CurrentRow.Cells["approve_flag"].Value = "False";
                SaveModification(nowcontractcode);


               

            }
            else
            {
                //hT_Manage_ItemDataGridView.CurrentRow.Cells["apply_money"].Value = queryForm.NewValue;
                //dgv.CurrentRow.Cells["discount_money"].Value = DBNull.Value;
                //dgv.CurrentRow.Cells["apply_money"].Value = DBNull.Value;
                //dgv.CurrentRow.Cells["approve_flag"].Value = "False";
                //SaveChanged();

            }
        }

        private void bindingNavigatorDeleteItem4_Click(object sender, EventArgs e)
        {

            string nowcontractcode = this.ly_purchase_contract_mainDataGridView.CurrentRow.Cells["合同编号"].Value.ToString();
            this.lypurchasecontractdetailmodificationBindingSource.RemoveCurrent();

            SaveModification(nowcontractcode);
        }

        private void ly_purchase_contract_detail_modificationDataGridView_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            string nowcontractcode = this.ly_purchase_contract_mainDataGridView.CurrentRow.Cells["合同编号"].Value.ToString();
          

            DataGridView dgv = sender as DataGridView;

            if (null == dgv.CurrentRow) return;

            //if ("True" == dgv.CurrentRow.Cells["approve_flag"].Value.ToString() && "000" != SQLDatabase.NowUserID)
            //{
            //    MessageBox.Show("合同已经执行,不能修改数据...", "注意");
            //    return;

            //}

            if ("修正数量" == dgv.CurrentCell.OwningColumn.Name)
            {

                ChangeValue queryForm = new ChangeValue();

                queryForm.OldValue = dgv.CurrentCell.Value.ToString();
                queryForm.NewValue = "";
                queryForm.ChangeMode = "value";
                queryForm.ShowDialog();




                if (queryForm.NewValue != "")
                {
                    dgv.CurrentRow.Cells["修正数量"].Value = queryForm.NewValue;

                    SaveModification(nowcontractcode);

                }
                else
                {

                }
                return;

            }
            ///////////////////////////////////////


            if ("修正单价" == dgv.CurrentCell.OwningColumn.Name)
            {

                ChangeValue queryForm = new ChangeValue();

                queryForm.OldValue = dgv.CurrentCell.Value.ToString();
                queryForm.NewValue = "";
                queryForm.ChangeMode = "value";
                queryForm.ShowDialog();




                if (queryForm.NewValue != "")
                {
                    dgv.CurrentRow.Cells["修正单价"].Value = queryForm.NewValue;

                    SaveModification(nowcontractcode);

                }
                else
                {

                }
                return;

            }
            //////////////////////////////////////

            if ("approve_flag" == dgv.CurrentCell.OwningColumn.Name)
            {

                if ("True" == dgv.CurrentRow.Cells["approve_flag"].Value.ToString())
                {
                    dgv.CurrentRow.Cells["approve_flag"].Value = "False";
                    dgv.CurrentRow.Cells["approve_poeple"].Value = DBNull.Value;
                    dgv.CurrentRow.Cells["approve_date"].Value = DBNull.Value;
                }
                else
                {

                    dgv.CurrentRow.Cells["approve_flag"].Value = "True";
                    dgv.CurrentRow.Cells["approve_poeple"].Value = SQLDatabase.nowUserName();
                    dgv.CurrentRow.Cells["approve_date"].Value = SQLDatabase.GetNowdate();
                }



                SaveModification(nowcontractcode); 
                return;

            } 

        }

        private void toolStripButton27_Click(object sender, EventArgs e)
        { 
            LY_invoice_manage queryForm = new LY_invoice_manage(); 
            queryForm.StartPosition = FormStartPosition.CenterParent;
            queryForm.ShowDialog();
        }
 
 

        private void dataGridView4_SelectionChanged(object sender, EventArgs e)
        {
            if (null == ly_purchase_contract_mainlDataGridView.CurrentRow)
            {
                
                return;
            } 
            string nowcontractcode = this.ly_purchase_contract_mainlDataGridView.CurrentRow.Cells["合同编号L"].Value.ToString();
            string nowplancode = this.ly_purchase_contract_mainlDataGridView.CurrentRow.Cells["计划编号L"].Value.ToString(); 
            this.ly_material_plan_mainBindingSource.Position = this.ly_material_plan_mainBindingSource.Find("计划编号", nowplancode); 
            this.ly_purchase_contract_mainBindingSource.Position = this.ly_purchase_contract_mainBindingSource.Find("合同编号", nowcontractcode); 
        }

        private void toolStripButton53_Click_1(object sender, EventArgs e)
        {
           // NewFrm.Show(this);
           
            this.ly_purchase_contract_main11TableAdapter.Fill(this.lYMaterielRequirements.ly_purchase_contract_main11, this.dateTimePicker1.Value, this.dateTimePicker2.Value.AddDays(1));
          //  NewFrm.Hide(this);
        }

        private void toolStripButton13_Click_1(object sender, EventArgs e)
        {
            if (ly_purchase_contract_mainDataGridView.CurrentRow == null)
            {
                return;
            }
            string contrat_code_now = ly_purchase_contract_mainDataGridView.CurrentRow.Cells["合同编号"].Value.ToString();
            string salespeople = this.ly_purchase_contract_mainDataGridView.CurrentRow.Cells["采购员"].Value.ToString();
            decimal max_price =decimal.Parse( this.ly_purchase_contract_mainDataGridView.CurrentRow.Cells["总金额"].Value.ToString());
            if (salespeople != SQLDatabase.nowUserName())
            {
                MessageBox.Show("请采购员:" + salespeople + "申请", "注意");
                return;
            }
            LY_pay_money queryForm = new LY_pay_money();
            queryForm.contract_code_add = contrat_code_now;
            queryForm.max_moeny = max_price;
            queryForm.StartPosition = FormStartPosition.CenterParent;
            queryForm.ShowDialog();
            string remark = "";
            decimal price = 0;
            if (queryForm.DialogResult != DialogResult.Cancel)
            {
                remark = queryForm.txtRemark;
                price = queryForm.moeny;
            }
            this.lyPrepaymentBindingSource.AddNew();

 
            ly_cg_fk.CurrentRow.Cells["申请金额1"].Value = price;
            ly_cg_fk.CurrentRow.Cells["申请日期1"].Value = SQLDatabase.GetNowdate();
         
            ly_cg_fk.CurrentRow.Cells["合同编号1"].Value = contrat_code_now;
            ly_cg_fk.CurrentRow.Cells["备注1"].Value = remark;

            this.ly_cg_fk.EndEdit();
            this.lyPrepaymentBindingSource.EndEdit();
            this.ly_PrepaymentTableAdapter.Update(this.lYMaterielRequirements.ly_Prepayment);

        }

        private void toolStripButton14_Click(object sender, EventArgs e)
        {
            if (ly_purchase_contract_mainDataGridView.CurrentRow == null)
            {
                return;
            }
            string contrat_code_now = ly_purchase_contract_mainDataGridView.CurrentRow.Cells["合同编号"].Value.ToString();
            string salespeople = this.ly_purchase_contract_mainDataGridView.CurrentRow.Cells["采购员"].Value.ToString(); 
            if (salespeople != SQLDatabase.nowUserName())
            {
                MessageBox.Show("请采购员:" + salespeople + "删除", "注意");
                return;
            }
            if (ly_cg_fk.CurrentRow == null)
            { return; }
            if (ly_cg_fk.CurrentRow.Cells["审批1"].Value.ToString() == "True")
            {
                MessageBox.Show("已经审批...", "注意");
                return;
            }
            string caption = "提示...";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo; 
            DialogResult result; 
            string message = "确定要删除吗";

            result = MessageBox.Show(message, caption, buttons,
            MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

            if (result == DialogResult.Yes)
            {
                this.lyPrepaymentBindingSource.RemoveCurrent();
                this.ly_cg_fk.EndEdit();
                this.lyPrepaymentBindingSource.EndEdit();
                this.ly_PrepaymentTableAdapter.Update(this.lYMaterielRequirements.ly_Prepayment);
            }
             
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {

        }
    }
}
