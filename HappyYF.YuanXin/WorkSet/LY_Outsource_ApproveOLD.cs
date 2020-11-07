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
    public partial class LY_Outsource_ApproveOLD : Form
    {

        string formState = "View";
        public LY_Outsource_ApproveOLD()
        {
            InitializeComponent();
        }

        private void LY_Machine_Load(object sender, EventArgs e)
        {

            this.ly_material_plan_mainTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            this.ly_material_plan_mainTableAdapter.Fill(this.lYMaterielRequirements.ly_material_plan_main, "SCJH");

            this.ly_materiel_supplierTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            this.ly_purchase_prepareforplanTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            this.ly_outsource_contract_mainTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;

            this.ly_outsource_contract_detailTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
           
            

            this.lY_MaterielRequirementsPurchaseTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            this.lY_MaterielRequirementsPurchaseTableAdapter.SelCommandText = "LY_MaterielRequirementsOutsource";

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

                    this.ly_outsource_contract_mainTableAdapter.Fill(this.lYOutsourceData .ly_outsource_contract_main , planNum);
                }
            }

                  
                 
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
                return;
            }


            string itemno = dgv.Rows[e.RowIndex].Cells["物料编码1"].Value.ToString();
            string itemnoname = dgv.Rows[e.RowIndex].Cells["物料名称1"].Value.ToString();
            int  id2 =int .Parse ( dgv.Rows[e.RowIndex].Cells["id2"].Value.ToString());
            this.groupBox3.Text = itemno +" "+ itemnoname + ":物料供应商";


            this.ly_materiel_supplierTableAdapter.Fill(this.lYMaterielRequirements.ly_materiel_supplier, itemno, id2);
        }

        private void ly_purchase_contract_detailDataGridView_SelectionChanged(object sender, EventArgs e)
        {
            //lY_MaterielRequirementsPurchaseSupplierDataGridView

          
        }

        private void ly_purchase_contract_mainDataGridView_SelectionChanged(object sender, EventArgs e)
        {
            if (null == ly_purchase_contract_mainDataGridView.CurrentRow )
            {
                this.ly_outsource_contract_detailTableAdapter.Fill(this.lYOutsourceData .ly_outsource_contract_detail , "");
                //this.lY_MaterielRequirementsPurchaseSupplierTableAdapter.Fill(this.lYMaterielRequirements.LY_MaterielRequirementsPurchaseSupplier, 0, "外购", "");

                return;
            }


            string nowcontractcode = this.ly_purchase_contract_mainDataGridView.CurrentRow .Cells["合同编号"].Value.ToString();
            string nowsuppliercode=this.ly_purchase_contract_mainDataGridView.CurrentRow .Cells["供应商编号6"].Value.ToString();

            int parentId = int.Parse(this.ly_material_plan_mainDataGridView.CurrentRow .Cells["parentid"].Value.ToString());
            string planNum = this.ly_material_plan_mainDataGridView.CurrentRow.Cells["计划编号"].Value.ToString();



            //this.lY_MaterielRequirementsPurchaseSupplierTableAdapter.Fill(this.lYMaterielRequirements.LY_MaterielRequirementsPurchaseSupplier, parentId, "外购", nowsuppliercode);

            this.ly_outsource_contract_detailTableAdapter.Fill(this.lYOutsourceData .ly_outsource_contract_detail , nowcontractcode);
        }

        private void ly_materiel_supplierDataGridView_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (null == ly_materiel_supplierDataGridView.CurrentRow) return;

            int parentId = int.Parse(this.ly_material_plan_mainDataGridView.CurrentRow.Cells["parentid"].Value.ToString());
            
            string nowsupplierCode = this.ly_materiel_supplierDataGridView.CurrentRow.Cells["供应商编码2"].Value.ToString();

            string nowsplanNum = this.lY_MaterielRequirementsDataGridView.CurrentRow.Cells["计划编号1"].Value.ToString();

            string nowitemno = this.lY_MaterielRequirementsDataGridView.CurrentRow.Cells["物料编码1"].Value.ToString();

            string nowQty = this.lY_MaterielRequirementsDataGridView.CurrentRow.Cells["未排数量1"].Value.ToString();


          
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
               "( plan_num,sortcode,itemno,qty,supplier_code) " +
               " values ('" + nowsplanNum + "','" + "3" + "','" + nowitemno + "'," + nowQty + ",'" + nowsupplierCode + "' )";


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

            if ("需求数量" == dgv.CurrentCell.OwningColumn.Name)
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

                    if (newQty > (oldQty + nowQty))
                    {
                        MessageBox.Show("修改数量超出需求数量,操作取消", "注意");
                        return;

                    }

                    dgv.CurrentRow.Cells["需求数量"].Value = queryForm.NewValue;


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

                NewFrm.Hide(this);

                int parentId = int.Parse(this.ly_material_plan_mainDataGridView.CurrentRow.Cells["parentid"].Value.ToString());
                //string planNum = this.ly_material_plan_mainDataGridView.Rows[e.RowIndex].Cells["计划编号"].Value.ToString();

                //NewFrm.Show(this.ParentForm);
                this.lY_MaterielRequirementsPurchaseTableAdapter.Fill(this.lYMaterielRequirements.LY_MaterielRequirementsPurchase, parentId, "外购");
                //NewFrm.Hide(this.ParentForm);

                this.ly_purchase_prepareforplanTableAdapter.Fill(this.lYMaterielRequirements.ly_purchase_prepareforplan, planNum, "3");

                this.ly_outsource_contract_mainTableAdapter.Fill(this.lYOutsourceData.ly_outsource_contract_main, planNum);


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

            this.ly_outsource_contract_detailTableAdapter.Update(this.lYOutsourceData .ly_outsource_contract_detail );

            RefreshData();
        }

        private void ly_purchase_contract_mainDataGridView_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            
            DataGridView dgv = sender as DataGridView;


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


            //int nowcontractId;
            //if (null != dgv.CurrentRow)
            //{
            //    nowcontractId = int.Parse(dgv.CurrentRow.Cells["id6"].Value.ToString());
            //}
            //else
            //{
            //    nowcontractId = 0;
            //}




            //if (null != this.ly_material_plan_mainDataGridView.CurrentRow)
            //{

            //    string planNum = this.ly_material_plan_mainDataGridView.CurrentRow.Cells["计划编号"].Value.ToString();


            //    string nowOwningColumnName = dgv.CurrentCell.OwningColumn.Name;
            //    this.ly_purchase_contract_mainTableAdapter.Fill(this.lYMaterielRequirements.ly_purchase_contract_main, planNum);

            //    this.ly_purchase_contract_mainBindingSource.Position = this.ly_purchase_contract_mainBindingSource.Find("id", nowcontractId);

               
            //    ly_purchase_contract_mainDataGridView.CurrentCell = ly_purchase_contract_mainDataGridView.CurrentRow.Cells[nowOwningColumnName];
            //}

            //dgv = ly_purchase_contract_mainDataGridView;

            /////////////////////////////////////////////////////////

            //if ("True" == dgv.CurrentRow.Cells["批准"].Value.ToString())
            //{
            //    MessageBox.Show("合同已经审批,不能修改数据...", "注意");
            //    return;
            
            //}

          

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

        private void ly_purchase_contract_detailDataGridView_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView dgv = sender as DataGridView;

            if (null == dgv.CurrentRow)
            {
                this.ly_materiel_supplierTableAdapter.Fill(this.lYMaterielRequirements.ly_materiel_supplier, "",0);
                return;
            }


            string itemno = dgv.Rows[e.RowIndex].Cells["物料编号7"].Value.ToString();
            string itemnoname = dgv.Rows[e.RowIndex].Cells["物料名称7"].Value.ToString();
            int id2 =int .Parse ( dgv.Rows[e.RowIndex].Cells["id2"].Value.ToString());
            this.groupBox3.Text = itemno + " " + itemnoname + ":物料供应商";


            this.ly_materiel_supplierTableAdapter.Fill(this.lYMaterielRequirements.ly_materiel_supplier, itemno,id2 );
        }

        private void ly_purchase_contract_detailDataGridView_RowEnter_1(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView dgv = sender as DataGridView;

            if (null == dgv.CurrentRow)
            {
                this.ly_materiel_supplierTableAdapter.Fill(this.lYMaterielRequirements.ly_materiel_supplier, "", 0);
                return;
            }


            string itemno = dgv.Rows[e.RowIndex].Cells["物料编号7"].Value.ToString();
            string itemnoname = dgv.Rows[e.RowIndex].Cells["物料名称7"].Value.ToString();
            int id2 = int.Parse(dgv.Rows[e.RowIndex].Cells["id27"].Value.ToString());
            this.groupBox3.Text = itemno + " " + itemnoname + ":物料供应商";


            this.ly_materiel_supplierTableAdapter.Fill(this.lYMaterielRequirements.ly_materiel_supplier, itemno, id2);
        }

     
       

       

       ///////////////////////////////////////////////////
      
    }
}
