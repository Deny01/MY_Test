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


namespace HappyYF.YuanXin.WorkSet
{
    public partial class LY_MaterialBom_query : Form
    {

        string parentNum = "noSet";

        public LY_MaterialBom_query()
        {
            InitializeComponent();
        }

        private void ly_inma0010BindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.ly_inma0010cpBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.lYMaterialMange);

        }

        private void LY_MaterialBom_Load(object sender, EventArgs e)
        {
           
            this.ly_prod_deptTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            this.ly_prod_deptTableAdapter.Fill(this.lYMaterialMange.ly_prod_dept);

            
            this.bomquery_revTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;

            this.ly_Bom_expend1TableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            this.ly_Bom_expendTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            this.bom_material_selTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            this.ly_bm0031TableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            this.ly_inma0010cpTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;

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

        private void MakeTreeView(DataTable table, string ParentID, System.Windows.Forms.TreeNode PNode,int nowlevel)
        {


            DataRow[] dr;

            if (null == ParentID)
                dr = table.Select("parentno is null");
            else
            {
               
                string expression;
                expression = "parentno='" + ParentID + "' and level=" + (nowlevel+1) .ToString ();

                dr = table.Select(expression);
            }
            try
            {
                if (dr.Length > 0)
                {
                    foreach (DataRow d in dr)
                    {

                        System.Windows.Forms.TreeNode TNode = new System.Windows.Forms.TreeNode();
                        TNode.Text = d["itemname"].ToString();
                        TNode.Tag = d["itemno"].ToString();



                        if (string.IsNullOrEmpty(d["parentno"].ToString()))
                        {
                            TNode.ToolTipText = "********" + d["absqty"].ToString();
                        }
                        else
                        {
                            TNode.ToolTipText = d["parentno"].ToString() + d["absqty"].ToString();
                        }
                        if (PNode == null)
                        {
                            this.treeView1.Nodes.Add(TNode);
                        }
                        else
                        {
                            PNode.Nodes.Add(TNode);
                        }

                        MakeTreeView(table, d["itemno"].ToString(), TNode, int.Parse (d["level"].ToString()));
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
            //if (null == ly_inma0010DataGridView.CurrentRow) return;
            //string s = this.ly_inma0010DataGridView.CurrentRow.Cells["物资编号"].Value.ToString();

            //string selAllString = "SELECT     parentno, itemno,  itemno +':' +itemname as itemname  from f_BomExtend('" + s +"',1) ORDER BY  id_num ";
            //string cString = SQLDatabase.Connectstring; ;
            //SqlDataAdapter bomAdapter = new SqlDataAdapter(selAllString, cString);

            //bomAdapter.SelectCommand.CommandTimeout = 0;

            //DataSet bomData = new DataSet();
            //bomAdapter.Fill(bomData);

            //this.treeView1.Nodes.Clear();
            //MakeTreeView(bomData.Tables[0], null, null);
            ////this.treeView1.ExpandAll();
            //this.treeView1.SelectedNode = this.treeView1.Nodes[0];
            //this.treeView1.SelectedNode.Expand();

            //this.groupBox1.Text = s + "BOM结构图";
           
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            parentNum = e.Node.Tag.ToString();

            int rowCount =this.ly_bm0031TableAdapter.Fill(this.lYMaterialMange.ly_bm0031, e.Node.Tag.ToString());
            this.bom_material_selTableAdapter.Fill(this.lYMaterialMange.bom_material_sel, e.Node.Tag.ToString());
            this.ly_Bom_expendTableAdapter.Fill(this.lYMaterialMange.ly_Bom_expend, e.Node.Tag.ToString());
            this.ly_Bom_expend1TableAdapter.Fill(this.lYMaterialMange.ly_Bom_expend1, 
                                                      e.Node.Tag.ToString(),
                                                      decimal .Parse ( e.Node.ToolTipText.Substring (8)),
                                                      e .Node.ToolTipText.Substring (0,8));

            this.bomquery_revTableAdapter.Fill(this.lYMaterialMange.bomquery_rev, parentNum, 0, 1);



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
            return;
            
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
            return;
            
            if (null == this.ly_bm0031DataGridView.CurrentRow) return;

            
            
            //////////////////////////////////////




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


            try
            {
                this.ly_bm0031TableAdapter.Update(this.lYMaterialMange.ly_bm0031);
            }
            catch (SqlException sqe)
            {

                MessageBox.Show(sqe .Message , "注意");
                return;


            }



        }

        private void ly_bm0031DataGridView_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {

            //return;
            
            DataGridView dgv = sender as DataGridView;

            ///////////////////////////////////////////////////

            if ("pf" == dgv.CurrentCell.OwningColumn.Name)
            {

                if (!SQLDatabase.CheckHaveRight(SQLDatabase.NowUserID, "BOM出库设定"))
                {
                    MessageBox.Show("无出库设定权限...", "注意");

                    return;

                }

                if ("虚拟" == dgv.CurrentRow.Cells["warehouse"].Value.ToString())
                {

                    MessageBox.Show("虚拟物料不能出库...", "注意");
                    return;

                }


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

            ///////////////////////////////////

            return;




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

            /////////////////////////////////////////////////////

            //if ("pf" == dgv.CurrentCell.OwningColumn.Name)
            //{

            //    //if (!HaveRight(SQLDatabase.NowUserID, "管理费优惠批准"))
            //    //{

            //    //    MessageBox.Show("没有管理费优惠批准权限...", "注意");
            //    //    return;
            //    //}



            //    if ("True" != dgv.CurrentRow.Cells["pf"].Value.ToString())
            //    {

            //        string message = "设定物料出库吗？";
            //        string caption = "提示...";
            //        MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            //        DialogResult result;



            //        result = MessageBox.Show(message, caption, buttons,
            //        MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

            //        if (result == DialogResult.Yes)
            //        {

            //           //dgv.CurrentRow.Cells["discount_money"].Value = dgv.CurrentRow.Cells["apply_money"].Value;
            //            dgv.CurrentRow.Cells["pf"].Value = "True";
            //            SaveChanged();
            //        }

            //    }
            //    else
            //    {

            //        string message = "取消物料出库吗？";
            //        string caption = "提示...";
            //        MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            //        DialogResult result;



            //        result = MessageBox.Show(message, caption, buttons,
            //        MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

            //        if (result == DialogResult.Yes)
            //        {
            //            //dgv.CurrentRow.Cells["discount_money"].Value = DBNull.Value;
            //            //dgv.CurrentRow.Cells["apply_money"].Value = DBNull.Value;
            //            dgv.CurrentRow.Cells["pf"].Value = "False";
            //            SaveChanged();
            //        }
            //    }

            //    return;
            //}




            ///////////////////////////////////////////////////////
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

        private void 删除全部子件ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            return;
            
            if (null == this.ly_bm0031DataGridView.CurrentRow) return;


            //int nowId = int.Parse(this.ly_bm0031DataGridView.CurrentRow.Cells["id"].Value.ToString());
            //string componentNum = this.ly_bm0031DataGridView.CurrentRow.Cells["component"].Value.ToString();

            if (null == ly_inma0010DataGridView.CurrentRow) return;
            string s = this.ly_inma0010DataGridView.CurrentRow.Cells["物资编号"].Value.ToString();

           

            string message1 = "当前(全部子件)将被删除，继续吗？";
            string caption1 = "提示...";
            MessageBoxButtons buttons1 = MessageBoxButtons.YesNo;

            DialogResult result1;



            result1 = MessageBox.Show(message1, caption1, buttons1,
            MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

            if (result1 == DialogResult.Yes)
            {

                string delstr = " delete ly_bm0031  where parent = '" + s  + "'";





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
                    //this.ly_bm0031TableAdapter.Fill(this.lYMaterialMange.ly_bm0031, parentNum);
                    //this.bom_material_selTableAdapter.Fill(this.lYMaterialMange.bom_material_sel, parentNum);

                    //this.bom_material_selBindingSource.Position = this.bom_material_selBindingSource.Find("物资编号", componentNum);

                    /////////////////////
                    string selAllString = "SELECT   level,  parentno, itemno,  itemno +':' +itemname as itemname ,absqty from f_BomExtend('" + s + "',1) ORDER BY  id_num ";
                    string cString = SQLDatabase.Connectstring; ;
                    SqlDataAdapter bomAdapter = new SqlDataAdapter(selAllString, cString);

                    bomAdapter.SelectCommand.CommandTimeout = 0;

                    DataSet bomData = new DataSet();
                    bomAdapter.Fill(bomData);

                    this.treeView1.Nodes.Clear();
                    MakeTreeView(bomData.Tables[0], null, null,0);
                    //this.treeView1.ExpandAll();
                    this.treeView1.SelectedNode = this.treeView1.Nodes[0];
                    this.treeView1.SelectedNode.Expand();

                    this.groupBox1.Text = s + "BOM结构图";

                }


            }
        }

        private void ly_inma0010DataGridView_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (null == ly_inma0010DataGridView.CurrentRow) return;
            //string s = this.ly_inma0010DataGridView.CurrentRow.Cells["物资编号"].Value.ToString();
            string s = this.ly_inma0010DataGridView.Rows[e.RowIndex].Cells["物资编号"].Value.ToString();

            this.bomquery_revTableAdapter.Fill(this.lYMaterialMange.bomquery_rev, s, 0, 1);
            

            string selAllString = "SELECT   level,  parentno, itemno,  itemno +':' +itemname as itemname,absqty  from f_BomExtend('" + s + "',1) ORDER BY  id_num ";
            string cString = SQLDatabase.Connectstring; ;
            SqlDataAdapter bomAdapter = new SqlDataAdapter(selAllString, cString);

            bomAdapter.SelectCommand.CommandTimeout = 0;

            DataSet bomData = new DataSet();
            bomAdapter.Fill(bomData);

            this.treeView1.Nodes.Clear();
            MakeTreeView(bomData.Tables[0], null, null,0);
            //this.treeView1.ExpandAll();
            this.treeView1.SelectedNode = this.treeView1.Nodes[0];
            this.treeView1.SelectedNode.Expand();

            this.groupBox1.Text = s + "BOM结构图";
        }

        private void 物料结构复制ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            return;
            
            if (null == ly_inma0010DataGridView.CurrentRow) return;


            if (SQLDatabase.CheckHaveRight(SQLDatabase.NowUserID, "物料结构复制"))
            {
                
            }
            else
            {
                MessageBox.Show("无产品结构(BOM)复制权限", "注意");
                return;
            }
            
           
            string nowitemno = this.ly_inma0010DataGridView.CurrentRow.Cells["物资编号"].Value.ToString();

            string sel = "SELECT  wzbh as 编码,mch as 名称, xhc as 型号, gg as 规格,mch_py, mch_jp FROM ly_inma0010 " + 
                         "   WHERE (status <> '原料') AND (status <> '基料') AND (warehouse <> '附件')  order by wzbh";



            QueryForm queryForm = new QueryForm();


            queryForm.Sel = sel;
            queryForm.Constr = SQLDatabase.Connectstring;
            queryForm.Nodiscol = 4;
            //Set the Column Collection to the filter Table
            //queryForm.SetSourceColumns(this.billMainDataSet.BalanceBill.Columns);

            queryForm.ShowDialog();


          if ( string .IsNullOrEmpty (queryForm.Result))
          {
             return ;
          }
           copyStru( queryForm.Result ,  nowitemno);

            ////////////////////////////////////////////////

           string selAllString = "SELECT   level,  parentno, itemno,  itemno +':' +itemname as itemname,absqty  from f_BomExtend('" + nowitemno + "',1) ORDER BY  id_num ";
           string cString = SQLDatabase.Connectstring; ;
           SqlDataAdapter bomAdapter = new SqlDataAdapter(selAllString, cString);

           bomAdapter.SelectCommand.CommandTimeout = 0;

           DataSet bomData = new DataSet();
           bomAdapter.Fill(bomData);

           this.treeView1.Nodes.Clear();
           MakeTreeView(bomData.Tables[0], null, null,0);
           //this.treeView1.ExpandAll();
           this.treeView1.SelectedNode = this.treeView1.Nodes[0];
           this.treeView1.SelectedNode.Expand();

           this.groupBox1.Text = nowitemno + "BOM结构图";
        }

        private void copyStru(string oriitemno, string disitemno)
        {
            SqlConnection sqlConnection1 = new SqlConnection(SQLDatabase.Connectstring);
            SqlCommand cmd = new SqlCommand();

            string contractinnerCode = "";

            //cmd.Parameters.Add("@Client_mode", SqlDbType.VarChar);
            //cmd.Parameters["@Client_mode"].Value = "YY";

            //cmd.Parameters.Add("@loanId", SqlDbType.Int);
            //cmd.Parameters["@loanId"].Value = loanId;

            cmd.Parameters.Add("@oriitemno", SqlDbType.VarChar);
            cmd.Parameters["@oriitemno"].Value = oriitemno;

            cmd.Parameters.Add("@disitemno", SqlDbType.VarChar);
            cmd.Parameters["@disitemno"].Value = disitemno;


            cmd.CommandText = "LY_Copy_Bomstru";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = sqlConnection1;

            sqlConnection1.Open();
            cmd.ExecuteNonQuery();
            sqlConnection1.Close();



            //return contractinnerCode;

        }

        private void toolStripButton16_Click(object sender, EventArgs e)
        {
            ExportDataGridviewTOExcellA.ExportDataGridview(this.ly_Bom_expendDataGridView, true);
        }

        private void toolStripButton10_Click(object sender, EventArgs e)
        {
            ExportDataGridviewTOExcellA.ExportDataGridview(this.dataGridView1, true);
        }

        private void toolStripButton11_Click(object sender, EventArgs e)
        {
            ExportDataGridviewTOExcellA.ExportDataGridview(this.ly_bm0031DataGridView, true);
        }

        private void ly_inma0010DataGridView_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (ly_inma0010DataGridView.CurrentRow == null)
            {
                return;
            }
            DataGridView dgv = sender as DataGridView;
            if ("fin_lock" == dgv.CurrentCell.OwningColumn.Name)
            {
                if (!SQLDatabase.CheckHaveRight(SQLDatabase.NowUserID, "BOM财务锁定"))
                {
                    MessageBox.Show("无权限操作", "注意");
                    return;
                }

                if ("True" == dgv.CurrentRow.Cells["fin_lock"].Value.ToString())
                {
                    dgv.CurrentRow.Cells["fin_lock"].Value = "False";
                    dgv.CurrentRow.Cells["fin_lock_people"].Value = DBNull.Value;
                    dgv.CurrentRow.Cells["fin_lock_time"].Value = DBNull.Value;

                }
                else
                {

                    dgv.CurrentRow.Cells["fin_lock"].Value = "True";
                    dgv.CurrentRow.Cells["fin_lock_people"].Value = SQLDatabase.nowUserName();
                    dgv.CurrentRow.Cells["fin_lock_time"].Value = SQLDatabase.GetNowtime();
                }
 
                this.ly_inma0010DataGridView.EndEdit();
                this.ly_inma0010cpBindingSource.EndEdit();
                this.ly_inma0010cpTableAdapter.Update(this.lYMaterialMange.ly_inma0010cp);
                return;
            }
            if ("tec_lock" == dgv.CurrentCell.OwningColumn.Name)
            {
                if (!SQLDatabase.CheckHaveRight(SQLDatabase.NowUserID, "BOM技术锁定"))
                {
                    MessageBox.Show("无权限操作", "注意");
                    return;
                }

                if ("True" == dgv.CurrentRow.Cells["tec_lock"].Value.ToString())
                {
                    dgv.CurrentRow.Cells["tec_lock"].Value = "False";
                    dgv.CurrentRow.Cells["tec_lock_people"].Value = DBNull.Value;
                    dgv.CurrentRow.Cells["tec_lock_time"].Value = DBNull.Value;

                }
                else
                {

                    dgv.CurrentRow.Cells["tec_lock"].Value = "True";
                    dgv.CurrentRow.Cells["tec_lock_people"].Value = SQLDatabase.nowUserName();
                    dgv.CurrentRow.Cells["tec_lock_time"].Value = SQLDatabase.GetNowtime();
                }
                this.ly_inma0010DataGridView.EndEdit();
                this.ly_inma0010cpBindingSource.EndEdit();
                this.ly_inma0010cpTableAdapter.Update(this.lYMaterialMange.ly_inma0010cp);
                return;
            }
            if ("pro_lock" == dgv.CurrentCell.OwningColumn.Name)
            {
                if (!SQLDatabase.CheckHaveRight(SQLDatabase.NowUserID, "BOM生产锁定"))
                {
                    MessageBox.Show("无权限操作", "注意");
                    return;
                }

                if ("True" == dgv.CurrentRow.Cells["pro_lock"].Value.ToString())
                {
                    dgv.CurrentRow.Cells["pro_lock"].Value = "False";
                    dgv.CurrentRow.Cells["pro_lock_people"].Value = DBNull.Value;
                    dgv.CurrentRow.Cells["pro_lock_time"].Value = DBNull.Value;

                }
                else
                {

                    dgv.CurrentRow.Cells["pro_lock"].Value = "True";
                    dgv.CurrentRow.Cells["pro_lock_people"].Value = SQLDatabase.nowUserName();
                    dgv.CurrentRow.Cells["pro_lock_time"].Value = SQLDatabase.GetNowtime();
                }
                this.ly_inma0010DataGridView.EndEdit();
                this.ly_inma0010cpBindingSource.EndEdit();
                this.ly_inma0010cpTableAdapter.Update(this.lYMaterialMange.ly_inma0010cp);
                return;
            }



        }

        //private void fillToolStripButton_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        this.bomquery_revTableAdapter.Fill(this.lYMaterialMange.bomquery_rev, item_codeToolStripTextBox.Text, new System.Nullable<byte>(((byte)(System.Convert.ChangeType(max_levelToolStripTextBox.Text, typeof(byte))))), new System.Nullable<decimal>(((decimal)(System.Convert.ChangeType(qtyToolStripTextBox.Text, typeof(decimal))))));

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
        //        this.ly_Bom_expend1TableAdapter.Fill(this.lYMaterialMange.ly_Bom_expend1, itemnoToolStripTextBox.Text, ((int)(System.Convert.ChangeType(nowcountToolStripTextBox.Text, typeof(int)))));
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
        //        this.ly_Bom_expendTableAdapter.Fill(this.lYMaterialMange.ly_Bom_expend, itemnoToolStripTextBox.Text);
        //    }
        //    catch (System.Exception ex)
        //    {
        //        System.Windows.Forms.MessageBox.Show(ex.Message);
        //    }

        //}




    }
}
