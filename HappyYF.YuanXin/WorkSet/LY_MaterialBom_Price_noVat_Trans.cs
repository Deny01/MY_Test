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
using DataGridFilter;
//using Microsoft.Office.Interop.Excel;

namespace HappyYF.YuanXin.WorkSet
{
    public partial class LY_MaterialBom_Price_noVat_Trans : Form
    {

        string parentNum = "noSet";

        //Excel.Application _Application = null;
        //Excel.Workbook _Workbook = null;//Microsoft Excel 工作簿 
        //Excel.Worksheet _Worksheet = null;


        public LY_MaterialBom_Price_noVat_Trans()
        {
            InitializeComponent();
            this.ly_item_allcost_NoVatTableAdapter.CommandTimeout = 0;
        }

        private void ly_inma0010BindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.ly_inma0010cpBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.lYMaterialMange);

        }

        private void LY_MaterialBom_Load(object sender, EventArgs e)
        {
            //this.ly_item_allcostTableAdapter.Fill(this.lYMaterialMange.ly_item_allcost, itemnoToolStripTextBox.Text);

            this.ly_item_allcost_NoVatTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            this.ly_prod_deptTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            this.ly_prod_deptTableAdapter.Fill(this.lYMaterialMange.ly_prod_dept);

          

            this.ly_sales_matingBom_CostTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            this.bom_material_selTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            this.ly_bm0031TableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            this.ly_inma0010cpTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;

            if (SQLDatabase.CheckHaveRight(SQLDatabase.NowUserID, "物料成本计算"))
            {
               
                this.toolStripButton6.Visible = true;
            }
            else
            {
              
                this.toolStripButton6.Visible = false;
            }

            this.ly_inma0010cpTableAdapter.Fill(this.lYMaterialMange.ly_inma0010cp);

        }

        private void toolStripTextBox1_Enter(object sender, EventArgs e)
        {
            toolStripTextBox1.Text = "";

            this.ly_inma0010cpBindingSource.Filter = "";
        }
        
        private void toolStripTextBox1_KeyUp(object sender, KeyEventArgs e)
        {
            string dFilter = "";

            dFilter = GetDataGridviewMultiFilterString.DGVMultiFilterString(this.ly_inma0010DataGridView, this.toolStripTextBox1.Text);

            if (null == dFilter)
                dFilter = "";

            this.ly_inma0010cpBindingSource.Filter = dFilter;
            //for (int i = 0; i < this.hT_Vehicle_collectDataGridView.ColumnCount; i++)

            //
            //for (int i = 1; i < 10; i++)
            //{
            //    string tempColumnName = this.ly_inma0010DataGridView.Columns[i].DataPropertyName;

            //    if (i != 9)
            //        dFilter = dFilter + tempColumnName + " like  '*" + this.toolStripTextBox1.Text + "*' or ";
            //    else
            //        dFilter = dFilter + tempColumnName + " like  '*" + this.toolStripTextBox1.Text + "*' ";

            //}

            //if (this.toolStripTextBox1.Text.Replace(" ", "").Length > 0)

            //    this.ly_inma0010cpBindingSource.Filter = dFilter;
            //else
            //    this.ly_inma0010cpBindingSource.Filter = " ";
        }

        private void MakeTreeView(DataTable table, string ParentID, System.Windows.Forms.TreeNode PNode)
        {


            DataRow[] dr;

            if (null == ParentID)
                dr = table.Select("parentno is null");
            else
            {
               
                string expression;
                expression = "parentno='" + ParentID + "'";

                dr = table.Select(expression);
            }
            try
            {
                if (dr.Length > 0)
                {
                    foreach (DataRow d in dr)
                    {

                        System.Windows.Forms.TreeNode TNode = new System.Windows.Forms.TreeNode();//absqty,itemprice ,itemmoney  
                        //TNode.Text = d["itemname"].ToString() + d["allmoney"].ToString() + "  " + d["qty_set"].ToString() + "  " + d["itemprice"].ToString() + "  (" + d["itemmoney"].ToString() + ")+(" + d["itemprocessingmoney"].ToString() + ")";

                        TNode.Text = d["itemname"].ToString() + d["allmoney"].ToString() + "     (" + d["qty_set"].ToString() + "  " + d["itemprice"].ToString() + "  (" + d["itemmoney"].ToString() + ")+(" + d["itemoutsourceall"].ToString() + ")+(" + d["itemoutsourcemachineall"].ToString() + ")+(" + d["itemprocessingmoneyall"].ToString() + ")+(" + d["assemblymoneyall"].ToString() + ")+(" + d["standardsetmoney"].ToString() + ")+(" + d["operation_cost"].ToString() + "))";
                       
                        TNode.Tag = d["itemno"].ToString();
                        if (PNode == null)
                        {
                            this.treeView1.Nodes.Add(TNode);
                        }
                        else
                        {
                            PNode.Nodes.Add(TNode);
                        }

                        MakeTreeView(table, d["itemno"].ToString(), TNode);
                    }
                }
                else
                {
                    return;
                }
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
            }
        }

        private void ly_inma0010DataGridView_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (null == ly_inma0010DataGridView.CurrentRow) return;

            NewFrm.Show(this);
            string s = this.ly_inma0010DataGridView.CurrentRow.Cells["物资编号"].Value.ToString();


            this.ly_sales_matingBom_CostTableAdapter.Fill(this.lYMaterialMange.ly_sales_matingBom_Cost, s);

            string selAllString = "SELECT     parentno, itemno, cast(itemno +':'+itemname as char(30))  as itemname,cast(qty_set as decimal(18,3)) as qty_set,itemprice ,itemmoney,itemoutsourceall,itemoutsourcemachineall,assemblymoneyall,standardsetmoney,itemprocessingmoneyall,operation_cost,isnull(itemmoney,0)+isnull(itemoutsourceall,0)+isnull(itemoutsourcemachineall,0)+isnull(itemprocessingmoneyall,0)+isnull(assemblymoneyall,0)+isnull(standardsetmoney,0)+isnull(operation_cost,0) as allmoney from f_BomExtend_price_novatTrans('" + s + "',1) ORDER BY  id_num ";
            string cString = SQLDatabase.Connectstring; ;
            SqlDataAdapter bomAdapter = new SqlDataAdapter(selAllString, cString);

            bomAdapter.SelectCommand.CommandTimeout = 0;

            DataSet bomData = new DataSet();
            bomAdapter.Fill(bomData);

            this.treeView1.Nodes.Clear();
            MakeTreeView(bomData.Tables[0], null, null);
            //this.treeView1.ExpandAll();
            this.treeView1.SelectedNode = this.treeView1.Nodes[0];
            this.treeView1.SelectedNode.Expand();

            this.groupBox1.Text = s + "BOM结构图";

            NewFrm.Hide(this);
           
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            parentNum = e.Node.Tag.ToString();

            int rowCount =this.ly_bm0031TableAdapter.Fill(this.lYMaterialMange.ly_bm0031, e.Node.Tag.ToString());
            this.bom_material_selTableAdapter.Fill(this.lYMaterialMange.bom_material_sel, e.Node.Tag.ToString());

            this.groupBox4.Text = parentNum + "子件列表(共" + rowCount.ToString() + "项)";
        }

        private void toolStripTextBox2_Enter(object sender, EventArgs e)
        {
            toolStripTextBox2.Text = "";

            this.bom_material_selBindingSource.Filter = "";
        }

        private void toolStripTextBox2_KeyUp(object sender, KeyEventArgs e)
        {
            string dFilter = "";

            //for (int i = 0; i < this.hT_Vehicle_collectDataGridView.ColumnCount; i++)
            for (int i = 1; i < 10; i++)
            {
                string tempColumnName = this.bom_material_selDataGridView.Columns[i].DataPropertyName;

                if (i != 9)
                    dFilter = dFilter + tempColumnName + " like  '*" + this.toolStripTextBox2.Text + "*' or ";
                else
                    dFilter = dFilter + tempColumnName + " like  '*" + this.toolStripTextBox2.Text + "*' ";

            }

            if (this.toolStripTextBox2.Text.Replace(" ", "").Length > 0)

                this.bom_material_selBindingSource.Filter = dFilter;
            else
                this.bom_material_selBindingSource.Filter = " ";
        }

        private void bom_material_selDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void bom_material_selDataGridView_DoubleClick(object sender, EventArgs e)
        {
            if (null == bom_material_selDataGridView.CurrentRow) return;
            string componentNum = this.bom_material_selDataGridView.CurrentRow.Cells["物资编号1"].Value.ToString();

            
           

            if (parentNum != "noSet")
            {




                string insStr = " INSERT INTO ly_bm0031  " +
               "( parent,component) " +
               " values ('" + parentNum + "','" + componentNum + "' )";


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


            this.ly_bm0031TableAdapter.Fill(this.lYMaterialMange.ly_bm0031, parentNum);
            this.bom_material_selTableAdapter.Fill(this.lYMaterialMange.bom_material_sel, parentNum);

            this.ly_bm0031BindingSource.Position = this.ly_bm0031BindingSource.Find("component", componentNum);
        }

        private void 删除子件ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (null == this.ly_bm0031DataGridView.CurrentRow) return;


            int nowId =int .Parse ( this.ly_bm0031DataGridView.CurrentRow.Cells["id"].Value.ToString());
            string componentNum = this.ly_bm0031DataGridView.CurrentRow.Cells["component"].Value.ToString();


            string message1 = "当前(子件：" + componentNum + ")将被删除，继续吗？";
            string caption1 = "提示...";
            MessageBoxButtons buttons1 = MessageBoxButtons.YesNo;

            DialogResult result1;



            result1 = MessageBox.Show(message1, caption1, buttons1,
            MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

            if (result1 == DialogResult.Yes)
            {

                string delstr = " delete ly_bm0031  where id = " + nowId + "";

        



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
                    this.ly_bm0031TableAdapter.Fill(this.lYMaterialMange.ly_bm0031, parentNum);
                    this.bom_material_selTableAdapter.Fill(this.lYMaterialMange.bom_material_sel, parentNum);

                    this.bom_material_selBindingSource.Position = this.bom_material_selBindingSource.Find("物资编号", componentNum);
                }


            }
        }

        private void SaveChanged()
        {
            ///////////////////////////

            this.ly_bm0031DataGridView.EndEdit();


            this.ly_bm0031BindingSource.EndEdit();



            this.ly_bm0031TableAdapter.Update(this.lYMaterialMange.ly_bm0031);



        }

        private void ly_bm0031DataGridView_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            DataGridView dgv = sender as DataGridView;




            if ("qty_set" == dgv.CurrentCell.OwningColumn.Name)
            {

                ChangeValue queryForm = new ChangeValue();

                queryForm.OldValue = dgv.CurrentCell.Value.ToString();
                queryForm.NewValue = "";
                queryForm.ChangeMode = "value";
                queryForm.ShowDialog();




                if (queryForm.NewValue != "")
                {
                    dgv.CurrentRow.Cells["qty_set"].Value = queryForm.NewValue;
                    //dgv.CurrentRow.Cells["discount_money"].Value = DBNull.Value;

                    //dgv.CurrentRow.Cells["approve_flag"].Value = "False";
                    SaveChanged();

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

            ///////////////////////////////////////////////////////

            //if ("oper_dept" == dgv.CurrentCell.OwningColumn.Name)
            //{



            //    string sel = "SELECT a.prodcode as 编码,a.prodname as 名称 FROM ly_prod_dept a ";


            //    QueryForm queryForm = new QueryForm();


            //    queryForm.Sel = sel;
            //    queryForm.Constr = SQLDatabase.Connectstring;

        

            //    queryForm.ShowDialog();

              


            //    dgv.CurrentRow.Cells["oper_dept"].Value = queryForm.Result; ;
            //        SaveChanged();

              

            //    return;
            //}
            ////////////////////////////////////////////////////////

         
            /////////////////////////////////////////////////////

            if ("out_prod" == dgv.CurrentCell.OwningColumn.Name)
            {





                if ("True" != dgv.CurrentRow.Cells["out_prod"].Value.ToString())
                {

                    string message = "确定出制单(领料单)吗?";
                    string caption = "提示...";
                    MessageBoxButtons buttons = MessageBoxButtons.YesNo;
                    DialogResult result;



                    result = MessageBox.Show(message, caption, buttons,
                    MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

                    if (result == DialogResult.Yes)
                    {

                        //dgv.CurrentRow.Cells["discount_money"].Value = dgv.CurrentRow.Cells["apply_money"].Value;
                        dgv.CurrentRow.Cells["out_prod"].Value = "True";
                        SaveChanged();
                    }

                }
                else
                {

                    string message = "取消出制单(领料单)吗?";
                    string caption = "提示...";
                    MessageBoxButtons buttons = MessageBoxButtons.YesNo;
                    DialogResult result;



                    result = MessageBox.Show(message, caption, buttons,
                    MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

                    if (result == DialogResult.Yes)
                    {
                        //dgv.CurrentRow.Cells["discount_money"].Value = DBNull.Value;
                        //dgv.CurrentRow.Cells["apply_money"].Value = DBNull.Value;
                        dgv.CurrentRow.Cells["out_prod"].Value = "False";
                        SaveChanged();
                    }
                }

                return;
            }


            ///////////////////////////////////////////////////

            if ("qty_set_waste" == dgv.CurrentCell.OwningColumn.Name)
            {
                ChangeValue queryForm = new ChangeValue();

                queryForm.OldValue = dgv.CurrentCell.Value.ToString();
                queryForm.NewValue = "";
                queryForm.ChangeMode = "value";
                queryForm.ShowDialog();




                if (queryForm.NewValue != "")
                {
                    dgv.CurrentRow.Cells["qty_set_waste"].Value = queryForm.NewValue;
                    //dgv.CurrentRow.Cells["discount_money"].Value = DBNull.Value;

                    //dgv.CurrentRow.Cells["approve_flag"].Value = "False";
                    SaveChanged();

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

            ///////////////////////////////////////////////////

            //if ("pf" == dgv.CurrentCell.OwningColumn.Name)
            //{
            //    ChangeValue queryForm = new ChangeValue();

            //    queryForm.OldValue = dgv.CurrentCell.Value.ToString();
            //    queryForm.NewValue = "";
            //    queryForm.ChangeMode = "value";
            //    queryForm.ShowDialog();




            //    if (queryForm.NewValue != "")
            //    {
            //        dgv.CurrentRow.Cells["qty_set_waste"].Value = queryForm.NewValue;
            //        //dgv.CurrentRow.Cells["discount_money"].Value = DBNull.Value;

            //        //dgv.CurrentRow.Cells["approve_flag"].Value = "False";
            //        SaveChanged();

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

            ///////////////////////////////////////////////////

            if ("pf" == dgv.CurrentCell.OwningColumn.Name)
            {

                //if (!HaveRight(SQLDatabase.NowUserID, "管理费优惠批准"))
                //{

                //    MessageBox.Show("没有管理费优惠批准权限...", "注意");
                //    return;
                //}



                if ("True" != dgv.CurrentRow.Cells["pf"].Value.ToString())
                {

                    string message = "设定物料出库吗？";
                    string caption = "提示...";
                    MessageBoxButtons buttons = MessageBoxButtons.YesNo;
                    DialogResult result;



                    result = MessageBox.Show(message, caption, buttons,
                    MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

                    if (result == DialogResult.Yes)
                    {

                       //dgv.CurrentRow.Cells["discount_money"].Value = dgv.CurrentRow.Cells["apply_money"].Value;
                        dgv.CurrentRow.Cells["pf"].Value = "True";
                        SaveChanged();
                    }

                }
                else
                {

                    string message = "取消物料出库吗？";
                    string caption = "提示...";
                    MessageBoxButtons buttons = MessageBoxButtons.YesNo;
                    DialogResult result;



                    result = MessageBox.Show(message, caption, buttons,
                    MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

                    if (result == DialogResult.Yes)
                    {
                        //dgv.CurrentRow.Cells["discount_money"].Value = DBNull.Value;
                        //dgv.CurrentRow.Cells["apply_money"].Value = DBNull.Value;
                        dgv.CurrentRow.Cells["pf"].Value = "False";
                        SaveChanged();
                    }
                }

                return;
            }




            /////////////////////////////////////////////////////
        }

        private void bindingNavigatorAddNewItem_Click(object sender, EventArgs e)
        {
            LY_MaterialAdd queryForm = new LY_MaterialAdd();

            queryForm.material_code = "";
            queryForm.runmode = "增加";
            queryForm.statemode = "成品";

            queryForm.StartPosition = FormStartPosition.CenterParent;
            queryForm.ShowDialog();

            if (queryForm.DialogResult != DialogResult.Cancel)
            {
                this.ly_inma0010cpTableAdapter.Fill(this.lYMaterialMange.ly_inma0010cp);
                this.ly_inma0010cpBindingSource.Position = this.ly_inma0010cpBindingSource.Find("物资编号", queryForm.material_code);
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if (null == ly_inma0010DataGridView.CurrentRow) return;
            string s = this.ly_inma0010DataGridView.CurrentRow.Cells["物资编号"].Value.ToString();
            //int loanid = int.Parse(this.xD_Client_LoanDataGridView.CurrentRow.Cells["Id_loan"].Value.ToString());

            LY_MaterialAdd queryForm = new LY_MaterialAdd();

            queryForm.statemode = "成品";
            queryForm.runmode = "修改";
            queryForm.material_code = s;

            queryForm.StartPosition = FormStartPosition.CenterParent;
            queryForm.ShowDialog();

            if (queryForm.DialogResult != DialogResult.Cancel)
            {
                this.ly_inma0010cpTableAdapter.Fill(this.lYMaterialMange.ly_inma0010cp);

                this.ly_inma0010cpBindingSource.Position = this.ly_inma0010cpBindingSource.Find("物资编号", s);
            }

        }

        private void bindingNavigatorDeleteItem_Click(object sender, EventArgs e)
        {
            string message = "确定删除当前记录吗？";
            string caption = "提示...";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result;



            result = MessageBox.Show(message, caption, buttons,
            MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

            if (result == DialogResult.Yes)
            {

                this.ly_inma0010cpBindingSource.RemoveCurrent();


                ly_inma0010DataGridView.EndEdit();
                ly_inma0010cpBindingSource.EndEdit();


                this.ly_inma0010cpTableAdapter.Update(this.lYMaterialMange.ly_inma0010cp);

                //string s = this.xD_Sel_SellBalanceDataGridView.CurrentRow.Cells["编号"].Value.ToString();

                //this.hS_ClientPaymentTableAdapter.Fill(this.xD_SellBalance.HS_ClientPayment, s);


            }
        }

        private void toolStripButton6_Click(object sender, EventArgs e)
        {

            if (null == this.ly_inma0010DataGridView.CurrentRow) return;

            string nowplannum;
            string noename;

            NewFrm.Show(this);
            foreach (DataGridViewRow dgr in ly_inma0010DataGridView.Rows)
            {
                nowplannum = dgr.Cells["物资编号"].Value.ToString();

                noename = dgr.Cells["名称"].Value.ToString();

                //this.toolStripLabel3.Text = plannum;
                //this.toolStripLabel3.Invalidate();

                NewFrm.Notify(this, "正在计算:  (" + nowplannum + ")" + noename + "   成本");



                Countmoney(nowplannum);
               
            }

            NewFrm.Hide(this);

            this.ly_inma0010cpTableAdapter.Fill(this.lYMaterialMange.ly_inma0010cp);
            ///////////////////////

            //   string insStr = " INSERT INTO ly_bm0031  " +
            //   "( parent,component) " +
            //   " values ('" + parentNum + "','" + componentNum + "' )";


            //    using (TransactionScope scope = new TransactionScope())
            //    {

            //        SqlConnection sqlConnection1 = new SqlConnection(SQLDatabase.Connectstring);
            //        SqlCommand cmd = new SqlCommand();

            //        cmd.CommandText = insStr;
            //        cmd.CommandType = CommandType.Text;
            //        cmd.Connection = sqlConnection1;



            //        sqlConnection1.Open();
            //        cmd.ExecuteNonQuery();

            //        sqlConnection1.Close();

            //        scope.Complete();
            //    }
            //}

        }

        private static void Countmoney(string nowplannum)
        {

            //string updstrclear = " update ly_inma0010  " +
            //              "  set mprice_novat_trans=0,pprice_novat_trans=0,assembly_novat_trans=0,standardset_novat_trans=0,operation_novat_trans=0,outsource_trans=0,outsource_machine_trans=0 "
            //              + " where  wzbh='" + nowplannum + "'";



            //SqlConnection sqlConnection0 = new SqlConnection(SQLDatabase.Connectstring);
            //SqlCommand cmd0 = new SqlCommand();

            //cmd0.CommandText = updstrclear;
            //cmd0.CommandType = CommandType.Text;
            //cmd0.Connection = sqlConnection0;
            //cmd0.CommandTimeout = 0;

            //TransactionOptions tOpt0 = new TransactionOptions();

            ////tOpt.IsolationLevel = IsolationLevel.ReadCommitted; //设置TransactionOptions模式

            //tOpt0.Timeout = new TimeSpan(5, 5, 0); ; // 设置超时时间为5分钟   new TimeSpan(0, 5, 0);                       
            ////using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required, tOpt))


            //using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, tOpt0))
            //{


            //    sqlConnection0.Open();
            //    try
            //    {

            //        cmd0.ExecuteNonQuery();



            //        //



            //    }
            //    catch (SqlException sqle)
            //    {


            //        MessageBox.Show(sqle.Message.Split('*')[0]);
            //    }


            //    finally
            //    {
            //        sqlConnection0.Close();


            //    }

            //    scope.Complete();
            //}


            ////////////////////////////////////////////////////////////////////////////////


            SqlConnection sqlConnection1 = new SqlConnection(SQLDatabase.Connectstring);
            SqlCommand cmd = new SqlCommand();



            cmd.Parameters.Add("@itemno", SqlDbType.VarChar);
            cmd.Parameters["@itemno"].Value = nowplannum;


            cmd.CommandText = "LY_CalculateCostValues";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandTimeout = 0;
            cmd.Connection = sqlConnection1;

            sqlConnection1.Open();
            cmd.ExecuteNonQuery();
            sqlConnection1.Close();


            ////////////////////////////////////////////////////////////////////////


            //string updstr = @" update ly_inma0010  " +
            //              "  set mprice_novatTrans=dbo.f_Item_price_novatTrans(wzbh,1),pprice_novatTrans=dbo.f_Item_price_processingCost_novatTrans(wzbh,1)," +
            //              "assembly_price_novat=dbo.f_Item_price_assemblyCost_novat(wzbh,1),standardset_price_novat=dbo.f_Item_price_standardsetCost_novat(wzbh,1)," +
            //              "operation_cost_novat=dbo.f_Item_price_operationCost_novat(wzbh,1),outsource_trans=dbo.f_Item_price_outsourceCost_novatTrans(wzbh,1),outsource_machine_trans=dbo.f_Item_price_outsource_machineCost_novatTrans(wzbh,1)"
            //              + " where  wzbh='" + nowplannum + "'";



            //SqlConnection sqlConnection1 = new SqlConnection(SQLDatabase.Connectstring);
            //SqlCommand cmd = new SqlCommand();

            //cmd.CommandText = updstr;
            //cmd.CommandType = CommandType.Text;
            //cmd.Connection = sqlConnection1;
            //cmd.CommandTimeout = 0;

            //TransactionOptions tOpt = new TransactionOptions();

            ////tOpt.IsolationLevel = IsolationLevel.ReadCommitted; //设置TransactionOptions模式

            //tOpt.Timeout = new TimeSpan(5, 5, 0); ; // 设置超时时间为5分钟   new TimeSpan(0, 5, 0);                       
            ////using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required, tOpt))


            //using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, tOpt))
            //{

             
            //    sqlConnection1.Open();
            //    try
            //    {

            //        cmd.ExecuteNonQuery();



            //        //



            //    }
            //    catch (SqlException sqle)
            //    {


            //        MessageBox.Show(sqle.Message.Split('*')[0]);
            //    }


            //    finally
            //    {
            //        sqlConnection1.Close();


            //    }

            //    scope.Complete();
            //}
        }

        private void 查看加工费用ToolStripMenuItem_Click(object sender, EventArgs e)
        {
           string  nowitemNum = this.treeView1.SelectedNode.Tag.ToString();
           string nowitemname = this.treeView1.SelectedNode.Text;

            LY_Material_PriceQuery queryForm = new LY_Material_PriceQuery();

            //
            queryForm.ItemNum = nowitemNum;
            queryForm.Itemname = nowitemname;
            queryForm.StartPosition = FormStartPosition.CenterParent;
            queryForm.ShowDialog();

            
        }

        private void 计算当前物料成本ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (null == this.ly_inma0010DataGridView.CurrentRow) return;

            string nowplannum;
            string noename;

            NewFrm.Show(this);

            nowplannum = this.ly_inma0010DataGridView.CurrentRow.Cells["物资编号"].Value.ToString();

            noename = this.ly_inma0010DataGridView.CurrentRow.Cells["名称"].Value.ToString();

                //this.toolStripLabel3.Text = plannum;
                //this.toolStripLabel3.Invalidate();

                NewFrm.Notify(this, "正在计算:  (" + nowplannum + ")" + noename + "   成本");



                Countmoney(nowplannum);

          

            NewFrm.Hide(this);

            this.ly_inma0010cpTableAdapter.Fill(this.lYMaterialMange.ly_inma0010cp);

            this.ly_inma0010cpBindingSource.Position = this.ly_inma0010cpBindingSource.Find("物资编号", nowplannum);
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {

            string nowitemNum = this.ly_sales_matingBomDataGridView.CurrentRow.Cells["备件编码"].Value.ToString();
            string nowitemname = this.ly_sales_matingBomDataGridView.CurrentRow.Cells["备件名称"].Value.ToString();

            LY_Material_PriceQuery queryForm = new LY_Material_PriceQuery();


            queryForm.ItemNum = nowitemNum;
            queryForm.Itemname = nowitemname;
            queryForm.StartPosition = FormStartPosition.CenterParent;
            queryForm.ShowDialog();
        }

        private void 查看成本结构ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string nowitemNum = this.ly_sales_matingBomDataGridView.CurrentRow.Cells["备件编码"].Value.ToString();
            string nowitemname = this.ly_sales_matingBomDataGridView.CurrentRow.Cells["备件名称"].Value.ToString();

            LY_MaterialBom_single queryForm = new LY_MaterialBom_single();


            queryForm.ItemNum = nowitemNum;
            queryForm.Itemname = nowitemname;
            queryForm.StartPosition = FormStartPosition.CenterParent;
            queryForm.ShowDialog();
        }

        //private void fillToolStripButton_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        this.ly_sales_matingBom_CostTableAdapter.Fill(this.lYMaterialMange.ly_sales_matingBom_Cost, parentToolStripTextBox.Text);
        //    }
        //    catch (System.Exception ex)
        //    {
        //        System.Windows.Forms.MessageBox.Show(ex.Message);
        //    }

        //}

        //private void ExportExcel(TreeNodeCollection TC)
        //{
        //    string pathFileName = string.Empty;
        //    System.Windows.Forms.SaveFileDialog saveFileDialog = new System.Windows.Forms.SaveFileDialog();
        //    saveFileDialog.Filter = "Excel File(*.xls)|*.xls";
        //    saveFileDialog.FilterIndex = 0;
        //    saveFileDialog.AddExtension = true;
        //    saveFileDialog.RestoreDirectory = true;
        //    saveFileDialog.CreatePrompt = false;
        //    saveFileDialog.Title = "Export Excel File To";
        //    if (saveFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
        //    {
        //        pathFileName = saveFileDialog.FileName;
        //    }
        //    else
        //    { return; }

        //    try
        //    {
        //        _Application = new Excel.ApplicationClass();
        //        _Application.Visible = false;
        //        _Workbook = _Application.Workbooks.Add(System.Reflection.Missing.Value);
        //        _Worksheet = _Workbook.Sheets["Sheet1"] as Excel.Worksheet;
        //        _Worksheet.Name = "你的工作表";//工作表名

        //        int cellCount = WriteCell(TC, 2, 1);//从第二行第一列开始写入.,可以换成别的....^_^

        //        /*添加一个总标题*/
        //        TreeNode rootNode = new TreeNode("总标题");
        //        WriteExcel(1, 1, cellCount, rootNode.Text);
        //        _Worksheet.get_Range("A1", "A1").Font.Bold = true; //设置标题字体,粗体  
        //        _Worksheet.get_Range("A1", "A1").Font.Size = 20;      //设置字体大小
        //        _Worksheet.get_Range("A1", "A1").Cells.Interior.Color = System.Drawing.Color.FromArgb(255, 204, 153).ToArgb();

        //        //设置单元格的背景色

        //        _Application.DisplayAlerts = false;//保存Excel的时候，不弹出是否保存的窗口直接进行保存
        //        _Application.AlertBeforeOverwriting = false;

        //        _Workbook.Save();
        //        _Application.Save(pathFileName);
        //    }
        //    catch (System.Exception e)
        //    {
        //        System.Windows.Forms.MessageBox.Show("创建保存Excel文件出错!\r\n原因:" + e.Message);
        //    }
        //    finally
        //    {
        //        if (_Application != null)
        //            _Application.Quit();
        //        _Workbook = null;
        //        _Worksheet = null;
        //        int generation = System.GC.GetGeneration(_Application);
        //        _Application = null;
        //        System.GC.Collect(generation);//垃圾回收
        //    }
        //}
        ///// <summary>
        ///// 遍历treeview并写入
        ///// </summary>
        ///// <param name="rootNodes">treeview 节点集合</param>
        ///// <param name="excelRow">写入的行:从1开始</param>
        ///// <param name="excelColumn">写入的列:从1开始</param>
        ///// <returns>占用的列数</returns>
        //private int WriteCell(TreeNodeCollection rootNodes, int excelRow, int excelColumn)
        //{
        //    int cellCount = 1;
        //    if (rootNodes.Count < 1)
        //    {
        //        return cellCount;
        //    }
        //    int cellCountSum = 0;
        //    foreach (TreeNode TNode in rootNodes)
        //    {
        //        cellCount = WriteCell(TNode.Nodes, excelRow + 1, excelColumn);
        //        WriteExcel(excelRow, excelColumn, cellCount, TNode.Text);
        //        cellCountSum += cellCount;
        //        excelColumn += cellCount;
        //    }
        //    return cellCountSum;
        //}

        //private void WriteExcel(int excelRow, int excelColumn, int cellCountSum, string excelValue)
        //{
        //    Excel.Range _Range = null;
        //    //如列数超过26,请在这里做一些修改....
        //    string point1 = ((char)(excelColumn + 64)).ToString() + excelRow.ToString();//单元格起始点 如:A1
        //    string point2 = ((char)(excelColumn + cellCountSum + 64 - 1)).ToString() + excelRow.ToString();//单元格结束点 如:B4

        //    _Range = _Worksheet.get_Range(point1, point2);//获取单元格
        //    if (cellCountSum > 0)
        //    {
        //        _Range.MergeCells = true; //合并单元格
        //    }
        //    _Range.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;//内容水平居中
        //    _Range.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;//内容垂直居中 
        //    _Range.Cells.Interior.Color = System.Drawing.Color.FromArgb(255, 0, 0).ToArgb();       //设置单元格的背景色
        //    _Worksheet.Cells[_Range.Row, _Range.Column] = excelValue;//把内容写入单元格
        //}
        //private void Form1_Load(object sender, EventArgs e)
        //{
        //    this.treeView2.ExpandAll();
        //}

        private void 导出ExcellToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.treeView1.ExpandAll();
            //ExportExcel(this.treeView1.Nodes);
        }

        private void 查看单价取用ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string nowitemNum = this.treeView1.SelectedNode.Tag.ToString();
            string nowitemname = this.treeView1.SelectedNode.Text;

            LY_Material_Costdetail queryForm = new LY_Material_Costdetail();


            queryForm.ItemNum = nowitemNum;
            queryForm.Itemname = nowitemname;
            queryForm.StartPosition = FormStartPosition.CenterParent;
            queryForm.ShowDialog();
        }

        private void ly_inma0010DataGridView_SelectionChanged(object sender, EventArgs e)
        {
            if (null == ly_inma0010DataGridView.CurrentRow) return;

          
            string s = this.ly_inma0010DataGridView.CurrentRow.Cells["物资编号"].Value.ToString();


            this.ly_sales_matingBom_CostTableAdapter.Fill(this.lYMaterialMange.ly_sales_matingBom_Cost, s);

         
        }

        private void toolStripButton7_Click(object sender, EventArgs e)
        {
            NewFrm.Show(this);
            this.ly_item_allcost_NoVatTableAdapter.Fill(this.lYMaterialMange.ly_item_allcost_NoVat, this.parentNum);
            
            NewFrm.Hide(this);
        }

        private void toolStripButton10_Click(object sender, EventArgs e)
        {
            //ExportDataGridviewTOExcellA.ExportDataGridview(this.ly_item_allcost_NoVatDataGridView, true);

            string nowplannum;
            string noename;
            string savepath;

            System.Windows.Forms.FolderBrowserDialog dialog = new System.Windows.Forms.FolderBrowserDialog();
            dialog.Description = "请选择存放文件夹";
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                if (string.IsNullOrEmpty(dialog.SelectedPath))
                {
                    MessageBox.Show("文件夹路径不能为空", "提示");
                    return;
                }

                savepath = dialog.SelectedPath;
            }
            else
            {
                return;
            }
            nowplannum = ly_inma0010DataGridView.CurrentRow.Cells["物资编号"].Value.ToString();
            string nowtype = ly_inma0010DataGridView.CurrentRow.Cells["中方型号"].Value.ToString();

            //ExportDataGridviewTOExcellA.ExportExcel(nowplannum + " " + nowtype, this.ly_item_allcost_NoVatDataGridView, savepath + "\\" + nowplannum + ".xls");

        }

        private void toolStripButton8_Click(object sender, EventArgs e)
        {
            ExportDataGridviewTOExcellA.ExportDataGridview(this.ly_inma0010DataGridView, true);
        }

        private void toolStripButton9_Click(object sender, EventArgs e)
        {
            if (null == this.ly_inma0010DataGridView.CurrentRow) return;

            string nowplannum;
            string noename;
            string savepath;

               System.Windows.Forms.FolderBrowserDialog dialog = new System.Windows.Forms.FolderBrowserDialog();
            dialog.Description = "请选择存放文件夹";
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                if (string.IsNullOrEmpty(dialog.SelectedPath))
                {
                    MessageBox.Show("文件夹路径不能为空", "提示");
                    return ;
                }

                savepath = dialog.SelectedPath; 
            }
            else
            {
                return;
            }


            NewFrm.Show(this);
            foreach (DataGridViewRow dgr in ly_inma0010DataGridView.Rows)
            {
                nowplannum = dgr.Cells["物资编号"].Value.ToString();

                noename = dgr.Cells["名称"].Value.ToString();

                string nowtype = dgr.Cells["中方型号"].Value.ToString();

                //this.toolStripLabel3.Text = plannum;
                //this.toolStripLabel3.Invalidate();

                NewFrm.Notify(this, "正在计算导出:  (" + nowplannum + " " + nowtype + ")" + noename + "   成本");

                this.ly_item_allcost_NoVatTableAdapter.Fill(this.lYMaterialMange.ly_item_allcost_NoVat, nowplannum);


                //ExportDataGridviewTOExcellA.ExportExcel(nowplannum + " " + nowtype, this.ly_item_allcost_NoVatDataGridView, savepath + "\\" + nowplannum + ".xls");

            }

            NewFrm.Hide(this);

          

           
        }

        private void 批量导出明细ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (null == this.ly_inma0010DataGridView.CurrentRow) return;

            string nowplannum;
            string noename;
            string savepath;

            System.Windows.Forms.FolderBrowserDialog dialog = new System.Windows.Forms.FolderBrowserDialog();
            dialog.Description = "请选择存放文件夹";
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                if (string.IsNullOrEmpty(dialog.SelectedPath))
                {
                    MessageBox.Show("文件夹路径不能为空", "提示");
                    return;
                }

                savepath = dialog.SelectedPath;
            }
            else
            {
                return;
            }


            NewFrm.Show(this);
            foreach (DataGridViewRow dgr in ly_inma0010DataGridView.Rows)
            {
                nowplannum = dgr.Cells["物资编号"].Value.ToString();

                noename = dgr.Cells["名称"].Value.ToString();

                string nowtype = dgr.Cells["中方型号"].Value.ToString();

                //this.toolStripLabel3.Text = plannum;
                //this.toolStripLabel3.Invalidate();

                NewFrm.Notify(this, "正在计算导出:  (" + nowplannum + " " + nowtype + ")" + noename + "   成本");

                this.ly_item_allcost_NoVatTableAdapter.Fill(this.lYMaterialMange.ly_item_allcost_NoVat, nowplannum);


                //ExportDataGridviewTOExcellA.ExportExcel(nowplannum + " " + nowtype, this.ly_item_allcost_NoVatDataGridView, savepath + "\\" + nowplannum + ".xls");

            }

            NewFrm.Hide(this);
        }

        private void toolStripButton11_Click(object sender, EventArgs e)
        {
            FilterForm filterForm = new FilterForm();



            List<string> ls = new List<string>();
            ls.Add("id");


            filterForm.SetSourceColumns(ly_inma0010DataGridView.Columns, ls);

            filterForm.ShowDialog();

            string nowfilter = filterForm.GetFilterString();
            if (string.IsNullOrEmpty(nowfilter))
            {
                this.ly_inma0010cpBindingSource.Filter = nowfilter;
            }
            else
            {
                //this.ly_inma0010BindingSource.Filter = "(" + nowfilter + ")" + " or 仓库='总计'";
                this.ly_inma0010cpBindingSource.Filter = nowfilter;
            }
        } 
            
            //this.ly_inma0010BindingSource.Filter = filterForm.GetFilterString() ;

       

        //private void fillToolStripButton_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        this.ly_item_allcostTableAdapter.Fill(this.lYMaterialMange.ly_item_allcost, itemnoToolStripTextBox.Text);
        //    }
        //    catch (System.Exception ex)
        //    {
        //        System.Windows.Forms.MessageBox.Show(ex.Message);
        //    }

        //}

       
        
    }
}
