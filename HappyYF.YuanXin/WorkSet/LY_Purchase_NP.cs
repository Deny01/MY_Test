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

namespace HappyYF.YuanXin.WorkSet
{
    public partial class LY_Purchase_NP : Form
    {
        private string nowsuppilercode = "All";
        private string nowsuppilername = "Allalllaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";

        public LY_Purchase_NP()
        {
            InitializeComponent();
        }

        private TreeNode FindGroupNode(TreeNodeCollection tnParent, string strValue)
        {

            if (tnParent == null) return null;

            //if (tnParent.Text == strValue) return tnParent;



            TreeNode tnRet = null;

            foreach (TreeNode tn in tnParent)
            {



                if (tn.Tag.ToString() == strValue)
                {
                    tnRet = tn;
                }
                else
                {

                    tnRet = FindGroupNode(tn.Nodes, strValue);
                }

                if (tnRet != null) break;

            }

            return tnRet;

        }

        private void MakeTreeView(DataTable table,  System.Windows.Forms.TreeNode PNode)
        {


            DataRow[] dr;
            string now_work_code;
            //string last_work_code = "___";



            dr = table.Select("supplier_code is not  null");

            System.Windows.Forms.TreeNode TNode = null;
            System.Windows.Forms.TreeNode CNode = null;

            foreach (DataRow d in dr)
            {
                now_work_code = d["supplier_code"].ToString();



             

                    TNode = new System.Windows.Forms.TreeNode();

                    TNode.Text = d["supplier_name"].ToString();

                   
                        TNode.Tag =  d["supplier_code"].ToString() ;
                   
                       
                        PNode.Nodes.Add(TNode);
                     
                  
               

            }



        }

        private void LY_Machine_Outsource_Load(object sender, EventArgs e)
        {

            this.ly_Prepayment_NPTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            this.ly_actualpayment_NPTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            this.ly_payable_NPTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;


            this.ly_supplier_listTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            this.ly_supplier_listTableAdapter.Fill(this.lYMaterielRequirements.ly_supplier_list, "5");





            //if (SQLDatabase.CheckHaveRight(SQLDatabase.NowUserID, "外协合同审批"))
            //{
            //    this.lY_outmachine_contract_main_selBindingSource.Filter = "";
            //}
            //else
            //{
            //    this.lY_outmachine_contract_main_selBindingSource.Filter = "责任人='" + SQLDatabase.nowUserName() + "'";
            //}




            //SetTreeview();


        }

        private void SetTreeview()
        {
            string selAllString;



            //selAllString = "SELECT  a.salesregion_code, a.salesregion_code+':'+a.salesregion_name as salesregion_name,b.yhbm,b.yhbm+':'+b.yhmc as yhmc FROM  ly_salesregion a left join T_users b on a.salesregion_code=b.salesregion_code ORDER BY  salesregion_code ";
            //selAllString = " select distinct a.work_code,case when a.work_code='019' then a.work_code+':公司外协'  else a.work_code+':'+ isnull(b.supplier_name,'')+' '+ isnull(b.supplier_contacts,'') end as work_name from ly_production_order_detail a left join ly_supplier_list b on a.work_code=b.supplier_code where (isnull(a.outorder_flag,0)=1 or a.work_code='019') and a.work_code is not null ";
            selAllString = " select  a.supplier_code, a.supplier_code+':'+ isnull(a.supplier_name,'')  as supplier_name from ly_supplier_list a  where left(a.supplier_code,2)='NP' ";

            SqlDataAdapter supplierAdapter = new SqlDataAdapter(selAllString, SQLDatabase.Connectstring);

            DataSet supplierData = new DataSet();
            supplierAdapter.Fill(supplierData);


            System.Windows.Forms.TreeNode TNode = new System.Windows.Forms.TreeNode();
            TNode.Text = "中原精密非生产采购供应商";


            TNode.Tag = "All";
            this.treeView1.Nodes.Clear();

            this.treeView1.Nodes.Add(TNode);

            MakeTreeView(supplierData.Tables[0], TNode);

            this.treeView1.ExpandAll();
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            this.nowsuppilercode = e.Node.Tag.ToString();
            this.nowsuppilername = e.Node.Text.Substring(8, e.Node.Text.Length - 8) ;

            this.ly_Prepayment_NPTableAdapter.Fill(this.lYFinancialMange.ly_Prepayment_NP, this.nowsuppilercode);
            this.ly_actualpayment_NPTableAdapter.Fill(this.lYFinancialMange.ly_actualpayment_NP, this.nowsuppilercode);
            this.ly_payable_NPTableAdapter.Fill(this.lYFinancialMange.ly_payable_NP, this.nowsuppilercode);


            // this.lY_outmachine_contract_main_selTableAdapter.Fill(this.lYProductMange.LY_outmachine_contract_main_sel, nowsuppilercode);




        }

       

      

       

       

      

       

       

       
     
      
       
     
       

      

        private TreeNode FindNode(TreeNodeCollection tnParent, string strValue)
        {

            if (tnParent == null) return null;

            //if (tnParent.Text == strValue) return tnParent;



            TreeNode tnRet = null;

            foreach (TreeNode tn in tnParent)
            {



                if (tn.Tag.ToString() == strValue)
                {
                    tnRet = tn;
                }
                else
                {

                    tnRet = FindNode(tn.Nodes, strValue);
                }

                if (tnRet != null) break;

            }

            return tnRet;

        }

        private void 统一指定税率ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LY_SupplierMange queryForm = new LY_SupplierMange();
            queryForm.WindowState = FormWindowState.Maximized;

            queryForm.Sortmode = "NP"; // "CG";

            queryForm.ShowDialog();

            SetTreeview();

            treeView1.AfterSelect -= treeView1_AfterSelect;

            this.treeView1.SelectedNode = FindGroupNode(this.treeView1.Nodes, queryForm.Nowsupplier_code );

            //this.ly_plan_getmaterialTableAdapter.Fill(this.lYSalseMange.ly_plan_getmaterial, nowplannum, nowgroupnum, int.Parse(nowNodeTag));

            treeView1.AfterSelect += treeView1_AfterSelect;

        }

        private void toolStripButton13_Click(object sender, EventArgs e)
        {


            this.ly_Prepayment_NPTableAdapter.Fill(this.lYFinancialMange.ly_Prepayment_NP, this.nowsuppilercode);




        }

        private void toolStripButton14_Click(object sender, EventArgs e)
        {
            this.ly_actualpayment_NPTableAdapter.Fill(this.lYFinancialMange.ly_actualpayment_NP, this.nowsuppilercode);
        }

        private void toolStripButton15_Click(object sender, EventArgs e)
        {
            this.ly_payable_NPTableAdapter.Fill(this.lYFinancialMange.ly_payable_NP, this.nowsuppilercode);
        }

        private void bindingNavigatorAddNewItem_Click(object sender, EventArgs e)
        {
            LY_add_Prepayment_NP queryForm = new LY_add_Prepayment_NP();

            queryForm.nowsuppilercode = this.nowsuppilercode;
            queryForm.StartPosition = FormStartPosition.CenterParent;
            queryForm.ShowDialog();


            if (queryForm.DialogResult != DialogResult.Cancel)
            {
                this.ly_Prepayment_NPTableAdapter.Fill(this.lYFinancialMange.ly_Prepayment_NP, this.nowsuppilercode);
               // this.lyinvoiceBindingSource.Position = this.lyinvoiceBindingSource.Find("invoice_code", queryForm.invoice_code_add);

            }
        }

        private void bindingNavigatorDeleteItem_Click(object sender, EventArgs e)
        {
            if (ly_Prepayment_NPDataGridView.CurrentRow == null)
                return;
            if (!SQLDatabase.CheckHaveRight(SQLDatabase.NowUserID, "非采购供应商账款删除"))
            {
                MessageBox.Show("无删除权限", "注意");
                return;
            }
            else
            {
                string salespeople = this.ly_Prepayment_NPDataGridView.CurrentRow.Cells["支付人"].Value.ToString();
                if (!string.IsNullOrEmpty(salespeople))
                {
                    if (salespeople != SQLDatabase.nowUserName())
                    {
                        MessageBox.Show("请支付人:" + salespeople + "删除", "注意");
                        return;
                    }
                }
            }
            //if (ly_fpDataGridView.CurrentRow.Cells["lock_flag"].Value.ToString() == "True")
            //{
            //    MessageBox.Show("已经锁定无法操作...", "注意");
            //    return;
            //}

            //if (ly_invoice_detailDataGridView.Rows.Count > 0)
            //{
            //    MessageBox.Show("该发票已经绑定入库单...", "注意");
            //    return;
            //}
            string caption = "提示...";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;

            DialogResult result;

            string message = "确定要删除吗";

            result = MessageBox.Show(message, caption, buttons,
            MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

            if (result == DialogResult.Yes)
            {
                this.ly_Prepayment_NPBindingSource.RemoveCurrent();
                this.ly_Prepayment_NPTableAdapter.Update(this.lYFinancialMange.ly_Prepayment_NP);
                
            }
            else
            {
                return;
            }
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            LY_add_actualpayment_NP queryForm = new LY_add_actualpayment_NP();

            queryForm.nowsuppilercode = this.nowsuppilercode;
            queryForm.StartPosition = FormStartPosition.CenterParent;
            queryForm.ShowDialog();


            if (queryForm.DialogResult != DialogResult.Cancel)
            {
                this.ly_actualpayment_NPTableAdapter.Fill(this.lYFinancialMange.ly_actualpayment_NP, this.nowsuppilercode);
                // this.lyinvoiceBindingSource.Position = this.lyinvoiceBindingSource.Find("invoice_code", queryForm.invoice_code_add);

            }
        }

        private void toolStripButton11_Click(object sender, EventArgs e)
        {
            LY_add_payable_NP queryForm = new LY_add_payable_NP();

            queryForm.nowsuppilercode = this.nowsuppilercode;
            queryForm.StartPosition = FormStartPosition.CenterParent;
            queryForm.ShowDialog();


            if (queryForm.DialogResult != DialogResult.Cancel)
            {
                this.ly_payable_NPTableAdapter.Fill(this.lYFinancialMange.ly_payable_NP, this.nowsuppilercode);
                // this.lyinvoiceBindingSource.Position = this.lyinvoiceBindingSource.Find("invoice_code", queryForm.invoice_code_add);

            }
        }

        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            if (ly_actualpayment_NPDataGridView.CurrentRow == null)
                return;
            if (!SQLDatabase.CheckHaveRight(SQLDatabase.NowUserID, "非采购供应商账款删除"))
            {
                MessageBox.Show("无删除权限", "注意");
                return;
            }
            else
            {
                string salespeople = this.ly_actualpayment_NPDataGridView.CurrentRow.Cells["支付人ac"].Value.ToString();
                if (!string.IsNullOrEmpty(salespeople))
                {
                    if (salespeople != SQLDatabase.nowUserName())
                    {
                        MessageBox.Show("请支付人:" + salespeople + "删除", "注意");
                        return;
                    }
                }
            }
            //if (ly_fpDataGridView.CurrentRow.Cells["lock_flag"].Value.ToString() == "True")
            //{
            //    MessageBox.Show("已经锁定无法操作...", "注意");
            //    return;
            //}

            //if (ly_invoice_detailDataGridView.Rows.Count > 0)
            //{
            //    MessageBox.Show("该发票已经绑定入库单...", "注意");
            //    return;
            //}
            string caption = "提示...";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;

            DialogResult result;

            string message = "确定要删除吗";

            result = MessageBox.Show(message, caption, buttons,
            MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

            if (result == DialogResult.Yes)
            {
                this.ly_actualpayment_NPBindingSource.RemoveCurrent();
                this.ly_actualpayment_NPTableAdapter.Update(this.lYFinancialMange.ly_actualpayment_NP);

            }
            else
            {
                return;
            }
        }

        private void toolStripButton12_Click(object sender, EventArgs e)
        {
            if (ly_payable_NPDataGridView.CurrentRow == null)
                return;
            if (!SQLDatabase.CheckHaveRight(SQLDatabase.NowUserID, "非采购供应商账款删除"))
            {
                MessageBox.Show("无删除权限", "注意");
                return;
            }
            else
            {
                string salespeople = this.ly_payable_NPDataGridView.CurrentRow.Cells["录入人"].Value.ToString();
                if (!string.IsNullOrEmpty(salespeople))
                {
                    if (salespeople != SQLDatabase.nowUserName())
                    {
                        MessageBox.Show("请录入人:" + salespeople + "删除", "注意");
                        return;
                    }
                }
            }
            //if (ly_fpDataGridView.CurrentRow.Cells["lock_flag"].Value.ToString() == "True")
            //{
            //    MessageBox.Show("已经锁定无法操作...", "注意");
            //    return;
            //}

            //if (ly_invoice_detailDataGridView.Rows.Count > 0)
            //{
            //    MessageBox.Show("该发票已经绑定入库单...", "注意");
            //    return;
            //}
            string caption = "提示...";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;

            DialogResult result;

            string message = "确定要删除吗";

            result = MessageBox.Show(message, caption, buttons,
            MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

            if (result == DialogResult.Yes)
            {
                this.ly_payable_NPBindingSource.RemoveCurrent();
                this.ly_payable_NPTableAdapter.Update(this.lYFinancialMange.ly_payable_NP );

            }
            else
            {
                return;
            }
        }

        private void ly_Prepayment_NPDataGridView_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            DataGridView dgv = sender as DataGridView;

            if (null == dgv.CurrentRow) return;

            if (!SQLDatabase.CheckHaveRight(SQLDatabase.NowUserID, "非采购供应商账款删除"))
            {
                MessageBox.Show("无修改权限", "注意");
                return;
            }
            else
            {
                string salespeople = dgv.CurrentRow.Cells["支付人"].Value.ToString();
                if (!string.IsNullOrEmpty(salespeople))
                {
                    if (salespeople != SQLDatabase.nowUserName())
                    {
                        MessageBox.Show("请支付人:" + salespeople + "修改", "注意");
                        return;
                    }
                }
            }






            if ("支付日期pre" == dgv.CurrentCell.OwningColumn.Name)
            {



                ChangeValue queryForm = new ChangeValue();

                queryForm.OldValue = dgv.CurrentCell.Value.ToString();
                queryForm.NewValue = "";
                queryForm.ChangeMode = "datetime";
                queryForm.ShowDialog();


                if (queryForm.NewValue != "")
                {
                    dgv.CurrentRow.Cells["支付日期pre"].Value = queryForm.NewValue;

                    this.ly_Prepayment_NPBindingSource.EndEdit();
                    this.ly_Prepayment_NPTableAdapter.Update(this.lYFinancialMange.ly_Prepayment_NP);

                }
                else
                {

                    // dgv.CurrentRow.Cells["支付日期pre"].Value = DBNull.Value;


                }







                return;

            }
            ////////////////////////////////////////////////////////////////////////

            /////////////////////////////
            if ("备注pre" == dgv.CurrentCell.OwningColumn.Name)
            {

                ChangeValue queryForm = new ChangeValue();

                queryForm.OldValue = dgv.CurrentCell.Value.ToString();
                queryForm.NewValue = "";
                queryForm.ChangeMode = "longstring";
                queryForm.ShowDialog();




                if (queryForm.NewValue != "")
                {
                    dgv.CurrentRow.Cells["备注pre"].Value = queryForm.NewValue;
                    //dgv.CurrentRow.Cells["discount_money"].Value = DBNull.Value;

                    this.ly_Prepayment_NPBindingSource.EndEdit();
                    this.ly_Prepayment_NPTableAdapter.Update(this.lYFinancialMange.ly_Prepayment_NP);

                }
                else
                {

                }
                return;

            }



            ///////////////////////////////////////////////////////////////
            if ("预付金额" == dgv.CurrentCell.OwningColumn.Name)
            {



              

                ChangeValue queryForm = new ChangeValue();

                queryForm.OldValue = dgv.CurrentCell.Value.ToString();
                queryForm.NewValue = "";
                queryForm.ChangeMode = "value";
                queryForm.setInFocus();
                queryForm.ShowDialog(this);





                if (queryForm.NewValue != "")
                {
                    dgv.CurrentRow.Cells["预付金额"].Value = queryForm.NewValue;

                    this.ly_Prepayment_NPBindingSource.EndEdit();
                    this.ly_Prepayment_NPTableAdapter.Update(this.lYFinancialMange.ly_Prepayment_NP);



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







        }

        private void ly_actualpayment_NPDataGridView_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            DataGridView dgv = sender as DataGridView;

            if (null == dgv.CurrentRow) return;

            if (!SQLDatabase.CheckHaveRight(SQLDatabase.NowUserID, "非采购供应商账款删除"))
            {
                MessageBox.Show("无修改权限", "注意");
                return;
            }
            else
            {
                string salespeople = dgv.CurrentRow.Cells["支付人ac"].Value.ToString();
                if (!string.IsNullOrEmpty(salespeople))
                {
                    if (salespeople != SQLDatabase.nowUserName())
                    {
                        MessageBox.Show("请支付人:" + salespeople + "修改", "注意");
                        return;
                    }
                }
            }






            if ("支付日期ac" == dgv.CurrentCell.OwningColumn.Name)
            {



                ChangeValue queryForm = new ChangeValue();

                queryForm.OldValue = dgv.CurrentCell.Value.ToString();
                queryForm.NewValue = "";
                queryForm.ChangeMode = "datetime";
                queryForm.ShowDialog();


                if (queryForm.NewValue != "")
                {
                    dgv.CurrentRow.Cells["支付日期ac"].Value = queryForm.NewValue;

                    this.ly_actualpayment_NPBindingSource.EndEdit();
                    this.ly_actualpayment_NPTableAdapter.Update(this.lYFinancialMange.ly_actualpayment_NP );

                }
                else
                {

                    // dgv.CurrentRow.Cells["支付日期pre"].Value = DBNull.Value;


                }







                return;

            }
            ////////////////////////////////////////////////////////////////////////

            /////////////////////////////
            if ("备注ac" == dgv.CurrentCell.OwningColumn.Name)
            {

                ChangeValue queryForm = new ChangeValue();

                queryForm.OldValue = dgv.CurrentCell.Value.ToString();
                queryForm.NewValue = "";
                queryForm.ChangeMode = "longstring";
                queryForm.ShowDialog();




                if (queryForm.NewValue != "")
                {
                    dgv.CurrentRow.Cells["备注ac"].Value = queryForm.NewValue;
                    //dgv.CurrentRow.Cells["discount_money"].Value = DBNull.Value;

                    this.ly_actualpayment_NPBindingSource.EndEdit();
                    this.ly_actualpayment_NPTableAdapter.Update(this.lYFinancialMange.ly_actualpayment_NP);

                }
                else
                {

                }
                return;

            }



            ///////////////////////////////////////////////////////////////
            if ("付款金额" == dgv.CurrentCell.OwningColumn.Name)
            {





                ChangeValue queryForm = new ChangeValue();

                queryForm.OldValue = dgv.CurrentCell.Value.ToString();
                queryForm.NewValue = "";
                queryForm.ChangeMode = "value";
                queryForm.setInFocus();
                queryForm.ShowDialog(this);





                if (queryForm.NewValue != "")
                {
                    dgv.CurrentRow.Cells["付款金额"].Value = queryForm.NewValue;

                    this.ly_actualpayment_NPBindingSource.EndEdit();
                    this.ly_actualpayment_NPTableAdapter.Update(this.lYFinancialMange.ly_actualpayment_NP);


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
        }

        private void ly_payable_NPDataGridView_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            DataGridView dgv = sender as DataGridView;

            if (null == dgv.CurrentRow) return;

            if (!SQLDatabase.CheckHaveRight(SQLDatabase.NowUserID, "非采购供应商账款删除"))
            {
                MessageBox.Show("无修改权限", "注意");
                return;
            }
            else
            {
                string salespeople = dgv.CurrentRow.Cells["录入人"].Value.ToString();
                if (!string.IsNullOrEmpty(salespeople))
                {
                    if (salespeople != SQLDatabase.nowUserName())
                    {
                        MessageBox.Show("请录入人:" + salespeople + "修改", "注意");
                        return;
                    }
                }
            }






            if ("录入日期" == dgv.CurrentCell.OwningColumn.Name)
            {



                ChangeValue queryForm = new ChangeValue();

                queryForm.OldValue = dgv.CurrentCell.Value.ToString();
                queryForm.NewValue = "";
                queryForm.ChangeMode = "datetime";
                queryForm.ShowDialog();


                if (queryForm.NewValue != "")
                {
                    dgv.CurrentRow.Cells["录入日期"].Value = queryForm.NewValue;

                    ly_payable_NPBindingSource.EndEdit();
                    ly_payable_NPTableAdapter.Update(this.lYFinancialMange.ly_payable_NP);

                }
                else
                {

                    // dgv.CurrentRow.Cells["支付日期pre"].Value = DBNull.Value;


                }







                return;

            }
            ////////////////////////////////////////////////////////////////////////

            /////////////////////////////
            if ("备注pay" == dgv.CurrentCell.OwningColumn.Name)
            {

                ChangeValue queryForm = new ChangeValue();

                queryForm.OldValue = dgv.CurrentCell.Value.ToString();
                queryForm.NewValue = "";
                queryForm.ChangeMode = "longstring";
                queryForm.ShowDialog();




                if (queryForm.NewValue != "")
                {
                    dgv.CurrentRow.Cells["备注pay"].Value = queryForm.NewValue;
                    //dgv.CurrentRow.Cells["discount_money"].Value = DBNull.Value;

                    ly_payable_NPBindingSource.EndEdit();
                    ly_payable_NPTableAdapter.Update(this.lYFinancialMange.ly_payable_NP);

                }
                else
                {

                }
                return;

            }



            ///////////////////////////////////////////////////////////////
            if ("应付金额" == dgv.CurrentCell.OwningColumn.Name)
            {





                ChangeValue queryForm = new ChangeValue();

                queryForm.OldValue = dgv.CurrentCell.Value.ToString();
                queryForm.NewValue = "";
                queryForm.ChangeMode = "value";
                queryForm.setInFocus();
                queryForm.ShowDialog(this);





                if (queryForm.NewValue != "")
                {
                    dgv.CurrentRow.Cells["应付金额"].Value = queryForm.NewValue;

                    ly_payable_NPBindingSource.EndEdit();
                    ly_payable_NPTableAdapter.Update(this.lYFinancialMange.ly_payable_NP);


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
        }

        
        private void toolStripButton22_Click(object sender, EventArgs e)
        {
            //LY_SupplierMange queryForm = new LY_SupplierMange();
            //queryForm.WindowState = FormWindowState.Maximized;

            //queryForm.Sortmode = "NP"; // "CG";

            //queryForm.ShowDialog();

            //if (queryForm.DialogResult != DialogResult.Cancel)
            //{
            //    this.ly_supplier_listTableAdapter.Fill(this.lYMaterielRequirements.ly_supplier_list, "5");
            //    this.ly_supplier_listBindingSource.Position = this.ly_supplier_listBindingSource.Find("编码", queryForm.Nowsupplier_code );

               
            //}

            //SetTreeview();

            //treeView1.AfterSelect -= treeView1_AfterSelect;

            //this.treeView1.SelectedNode = FindGroupNode(this.treeView1.Nodes, queryForm.Nowsupplier_code);

            ////this.ly_plan_getmaterialTableAdapter.Fill(this.lYSalseMange.ly_plan_getmaterial, nowplannum, nowgroupnum, int.Parse(nowNodeTag));

            //treeView1.AfterSelect += treeView1_AfterSelect;
        }

        private void toolStripTextBox3_KeyUp(object sender, KeyEventArgs e)
        {
            string filterString = GetDataGridviewMultiFilterString.DGVMultiFilterString(this.ly_supplier_listDataGridView, this.toolStripTextBox3.Text);

          
            if (null == filterString)
                filterString = "";


            this.ly_supplier_listBindingSource.Filter = filterString;
        }

        private void toolStripTextBox3_Enter(object sender, EventArgs e)
        {
            toolStripTextBox3.Text = "";

            this.ly_supplier_listBindingSource.Filter = "";
        }

        private void ly_supplier_listDataGridView_SelectionChanged(object sender, EventArgs e)
        {
            if (null == ly_supplier_listDataGridView.CurrentRow) return;
            string s = this.ly_supplier_listDataGridView.CurrentRow.Cells["编码"].Value.ToString();

            this.nowsuppilercode = s;

            this.ly_Prepayment_NPTableAdapter.Fill(this.lYFinancialMange.ly_Prepayment_NP, this.nowsuppilercode);
            this.ly_actualpayment_NPTableAdapter.Fill(this.lYFinancialMange.ly_actualpayment_NP, this.nowsuppilercode);
            this.ly_payable_NPTableAdapter.Fill(this.lYFinancialMange.ly_payable_NP, this.nowsuppilercode);

        }

        private void toolStripButton23_Click(object sender, EventArgs e)
        {
            LY_SupplierMange queryForm = new LY_SupplierMange();
            queryForm.WindowState = FormWindowState.Maximized;

            queryForm.Sortmode = "NP"; // "CG";

            queryForm.ShowDialog();

            if (queryForm.DialogResult != DialogResult.Cancel)
            {
                this.ly_supplier_listTableAdapter.Fill(this.lYMaterielRequirements.ly_supplier_list, "5");
                this.ly_supplier_listBindingSource.Position = this.ly_supplier_listBindingSource.Find("编码", queryForm.Nowsupplier_code);


            }
        }

        //private void fillToolStripButton_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        this.ly_Prepayment_NPTableAdapter.Fill(this.lYFinancialMange.ly_Prepayment_NP, supplier_codeToolStripTextBox.Text);
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
        //        this.ly_actualpayment_NPTableAdapter.Fill(this.lYFinancialMange.ly_actualpayment_NP, supplier_codeToolStripTextBox.Text);
        //    }
        //    catch (System.Exception ex)
        //    {
        //        System.Windows.Forms.MessageBox.Show(ex.Message);
        //    }

        //}

        //private void fillToolStripButton_Click_2(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        this.ly_payable_NPTableAdapter.Fill(this.lYFinancialMange.ly_payable_NP, supplier_codeToolStripTextBox.Text);
        //    }
        //    catch (System.Exception ex)
        //    {
        //        System.Windows.Forms.MessageBox.Show(ex.Message);
        //    }

        //}
    }
}
