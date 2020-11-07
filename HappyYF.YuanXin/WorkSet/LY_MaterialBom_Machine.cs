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
    public partial class LY_MaterialBom_Machine : Form
    {

        string parentNum = "noSet";

        public LY_MaterialBom_Machine()
        {
            InitializeComponent();
        }

        public void LoadSingleData(string nowitemno)
        {
            this.toolStripTextBox1.Visible = false;
            this.toolStripLabel1.Visible = false;

            this.ly_inma0010machineBindingSource.Filter = "物资编号='" + nowitemno + "'";

            ////////////////////////////////////////////////

            if (null == ly_inma0010DataGridView.CurrentRow) return;
            string s = this.ly_inma0010DataGridView.CurrentRow.Cells["物资编号"].Value.ToString();

            string selAllString = "SELECT     parentno, itemno,  itemno +':' +itemname as itemname  from f_BomExtend('" + s + "',1) ORDER BY  id_num ";
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

        }

        private void ly_inma0010BindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.ly_inma0010machineBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.lYMaterialMange);

        }

        private void LY_MaterialBom_Load(object sender, EventArgs e)
        {
           
            
            
           
           
            this.ly_prod_deptTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            this.ly_prod_deptTableAdapter.Fill(this.lYMaterialMange.ly_prod_dept);

            this.bom_material_selmachineTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;

            this.ly_bm0031TableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            this.ly_inma0010machineTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;

            this.ly_inma0010machineTableAdapter.Fill(this.lYMaterialMange.ly_inma0010machine);

        }

        private void toolStripTextBox1_Enter(object sender, EventArgs e)
        {
            toolStripTextBox1.Text = "";

            this.ly_inma0010machineBindingSource.Filter = "";
        }
        
        private void toolStripTextBox1_KeyUp(object sender, KeyEventArgs e)
        {

            string filterString = GetDataGridviewMultiFilterString.DGVMultiFilterString(this.ly_inma0010DataGridView, this.toolStripTextBox1.Text);



            if (null == filterString)
                filterString = "";

            this.ly_inma0010machineBindingSource.Filter = filterString;
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

                        System.Windows.Forms.TreeNode TNode = new System.Windows.Forms.TreeNode();
                        TNode.Text = d["itemname"].ToString();
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
            string s = this.ly_inma0010DataGridView.CurrentRow.Cells["物资编号"].Value.ToString();

            string selAllString = "SELECT     parentno, itemno,  itemno +':' +itemname as itemname  from f_BomExtend('" + s +"',1) ORDER BY  id_num ";
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
           
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            parentNum = e.Node.Tag.ToString();

            int rowCount =this.ly_bm0031TableAdapter.Fill(this.lYMaterialMange.ly_bm0031, e.Node.Tag.ToString());
            this.bom_material_selmachineTableAdapter.Fill(this.lYMaterialMange.bom_material_selmachine, e.Node.Tag.ToString());

            this.groupBox4.Text = parentNum + "子件列表(共" + rowCount.ToString() + "项)";
        }

        private void toolStripTextBox2_Enter(object sender, EventArgs e)
        {
            toolStripTextBox2.Text = "";

            this.bom_material_selmachineBindingSource.Filter = "";
        }

        private void toolStripTextBox2_KeyUp(object sender, KeyEventArgs e)
        {
            

            string filterString = GetDataGridviewMultiFilterString.DGVMultiFilterString(this.bom_material_selDataGridView, this.toolStripTextBox2.Text);



            if (null == filterString)
                filterString = "";

            this.bom_material_selmachineBindingSource.Filter = filterString;
        }

        private void bom_material_selDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        protected bool checkLock()
        {
            if (null == ly_inma0010DataGridView.CurrentRow)
            { return false; }

            else
            {
                if (this.ly_inma0010DataGridView.CurrentRow.Cells["fin_lock"].Value.ToString() == "True" || this.ly_inma0010DataGridView.CurrentRow.Cells["tec_lock"].Value.ToString() == "True" || this.ly_inma0010DataGridView.CurrentRow.Cells["pro_lock"].Value.ToString() == "True")
                {
                    return false;

                }
                else
                {
                    return true;
                }
            }
        }
        private void bom_material_selDataGridView_DoubleClick(object sender, EventArgs e)
        {
            if (null == bom_material_selDataGridView.CurrentRow) return;
            string componentNum = this.bom_material_selDataGridView.CurrentRow.Cells["物资编号1"].Value.ToString();

            if (!checkLock())
            {
                MessageBox.Show("已经锁定无法修改...", "注意");
                return;

            }

            string salespeople = this.ly_inma0010DataGridView.CurrentRow.Cells["负责人"].Value.ToString();
            if (!string.IsNullOrEmpty(salespeople))
            {
                if (salespeople != SQLDatabase.nowUserName())
                {
                    MessageBox.Show("请负责人:" + salespeople + "操作", "注意");
                    return;
                }
            }
            else
            {
                MessageBox.Show("请联系技术部领导进行负责人指定...", "注意");
                return;
            }


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
            this.bom_material_selmachineTableAdapter.Fill(this.lYMaterialMange.bom_material_selmachine , parentNum);
            //string componentNum = this.bom_material_selDataGridView.CurrentRow.Cells["物资编号1"].Value.ToString();
            this.ly_bm0031BindingSource.Position = this.ly_bm0031BindingSource.Find("component", componentNum);
        }

        private void 删除子件ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (null == this.ly_bm0031DataGridView.CurrentRow) return;

            if (!checkLock())
            {
                MessageBox.Show("已经锁定无法修改...", "注意");
                return;

            }

            string salespeople = this.ly_inma0010DataGridView.CurrentRow.Cells["负责人"].Value.ToString();
            if (!string.IsNullOrEmpty(salespeople))
            {
                if (salespeople != SQLDatabase.nowUserName())
                {
                    MessageBox.Show("请负责人:" + salespeople + "操作", "注意");
                    return;
                }
            }
            else
            {
                MessageBox.Show("请联系技术部领导进行负责人指定...", "注意");
                return;
            }

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
                    this.bom_material_selmachineTableAdapter.Fill(this.lYMaterialMange.bom_material_selmachine, parentNum);

                    this.bom_material_selmachineBindingSource.Position = this.bom_material_selmachineBindingSource.Find("物资编号", componentNum);
                }


            }
        }

        private void SaveChanged()
        {
            ///////////////////////////

            this.ly_bm0031DataGridView.EndEdit();


            this.ly_bm0031BindingSource.EndEdit();



            this.ly_bm0031TableAdapter.Update(this.lYMaterialMange.ly_bm0031);

            string componentNum = this.ly_bm0031DataGridView.CurrentRow.Cells["component"].Value.ToString();

            this.ly_bm0031TableAdapter.Fill(this.lYMaterialMange.ly_bm0031, parentNum);
         
            this.ly_bm0031BindingSource.Position = this.ly_bm0031BindingSource.Find("component", componentNum);



        }

        private void ly_bm0031DataGridView_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            DataGridView dgv = sender as DataGridView;




            string nowgeometry;
            //decimal nowspecific_weight;
            //decimal nowdiameter;
            //decimal nowlength;
            //decimal nowwidth;
            //decimal nowheight;



            if (!string.IsNullOrEmpty(this.ly_bm0031DataGridView.CurrentRow.Cells["geometry"].Value.ToString()))
                    {
                        nowgeometry = this.ly_bm0031DataGridView.CurrentRow.Cells["geometry"].Value.ToString();
                    }
                    else
                    {
                        nowgeometry = "ELSE";
                    }

                    //if (!string.IsNullOrEmpty(this.ly_machinepart_process_workDataGridView.Rows[this.ly_machinepart_process_workDataGridView.RowCount - 1].Cells["本序可用"].Value.ToString()))
                    //{
                    //    canuse_count = decimal.Parse(this.ly_machinepart_process_workDataGridView.Rows[this.ly_machinepart_process_workDataGridView.RowCount - 1].Cells["本序可用"].Value.ToString());
                    //}
                    //else
                    //{
                    //    canuse_count = 0;
                    //}

                ////////////////////////////


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

                    SetUseNum(nowgeometry);
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

            if ("diameter" == dgv.CurrentCell.OwningColumn.Name)
            {

                if ("ELSE" == nowgeometry) return;
                if ("棒料" != nowgeometry) return;

              
                
                ChangeValue queryForm = new ChangeValue();

                queryForm.OldValue = dgv.CurrentCell.Value.ToString();
                queryForm.NewValue = "";
                queryForm.ChangeMode = "value";
                queryForm.ShowDialog();




                if (queryForm.NewValue != "")
                {
                    dgv.CurrentRow.Cells["diameter"].Value = queryForm.NewValue;
                    //dgv.CurrentRow.Cells["discount_money"].Value = DBNull.Value;

                    //dgv.CurrentRow.Cells["approve_flag"].Value = "False";


                    SetUseNum(nowgeometry);




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

            if ("length" == dgv.CurrentCell.OwningColumn.Name)
            {

                if ("ELSE" == nowgeometry) return;
                //if ("圆柱体" != nowgeometry) return;

                ChangeValue queryForm = new ChangeValue();

                queryForm.OldValue = dgv.CurrentCell.Value.ToString();
                queryForm.NewValue = "";
                queryForm.ChangeMode = "value";
                queryForm.ShowDialog();




                if (queryForm.NewValue != "")
                {
                    dgv.CurrentRow.Cells["length"].Value = queryForm.NewValue;
                    //dgv.CurrentRow.Cells["discount_money"].Value = DBNull.Value;
                    SetUseNum(nowgeometry);
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
            /////////////////////////////////////////////////

            if ("width" == dgv.CurrentCell.OwningColumn.Name)
            {
                if ("ELSE" == nowgeometry) return;
                if ("板料" != nowgeometry) return;
                
                ChangeValue queryForm = new ChangeValue();

                queryForm.OldValue = dgv.CurrentCell.Value.ToString();
                queryForm.NewValue = "";
                queryForm.ChangeMode = "value";
                queryForm.ShowDialog();




                if (queryForm.NewValue != "")
                {
                    dgv.CurrentRow.Cells["width"].Value = queryForm.NewValue;
                    //dgv.CurrentRow.Cells["discount_money"].Value = DBNull.Value;
                    SetUseNum(nowgeometry);
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
            /////////////////////////////////////////////////

            if ("height" == dgv.CurrentCell.OwningColumn.Name)
            {

                if ("ELSE" == nowgeometry) return;
                if ("板料" != nowgeometry) return;
                
                ChangeValue queryForm = new ChangeValue();

                queryForm.OldValue = dgv.CurrentCell.Value.ToString();
                queryForm.NewValue = "";
                queryForm.ChangeMode = "value";
                queryForm.ShowDialog();




                if (queryForm.NewValue != "")
                {
                    dgv.CurrentRow.Cells["height"].Value = queryForm.NewValue;
                    //dgv.CurrentRow.Cells["discount_money"].Value = DBNull.Value;
                    SetUseNum(nowgeometry);
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

            /////////////////////////////////////////////////

            if ("input_count" == dgv.CurrentCell.OwningColumn.Name)
            {

                if ("ELSE" == nowgeometry) return;
              

                ChangeValue queryForm = new ChangeValue();

                queryForm.OldValue = dgv.CurrentCell.Value.ToString();
                queryForm.NewValue = "";
                queryForm.ChangeMode = "value";
                queryForm.ShowDialog();




                if (queryForm.NewValue != "")
                {
                    dgv.CurrentRow.Cells["input_count"].Value = queryForm.NewValue;
                    //dgv.CurrentRow.Cells["discount_money"].Value = DBNull.Value;
                    SetUseNum(nowgeometry);
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

        private void SetUseNum(  string nowgeometry)
        {
           
            decimal nowspecific_weight;
            decimal nowdiameter;
            decimal nowlength;
            decimal nowwidth;
            decimal nowheight;
            decimal nowinputcount;
            decimal qtysetwaste;



            if (!string.IsNullOrEmpty(this.ly_bm0031DataGridView.CurrentRow.Cells["diameter"].Value.ToString()))
            {
                nowdiameter = decimal.Parse(this.ly_bm0031DataGridView.CurrentRow.Cells["diameter"].Value.ToString());
            }
            else
            {
                nowdiameter = 0;
            }

            if (!string.IsNullOrEmpty(this.ly_bm0031DataGridView.CurrentRow.Cells["specific_weight"].Value.ToString()))
            {
                nowspecific_weight = decimal.Parse(this.ly_bm0031DataGridView.CurrentRow.Cells["specific_weight"].Value.ToString());
            }
            else
            {
                nowspecific_weight = 0;
            }

            if (!string.IsNullOrEmpty(this.ly_bm0031DataGridView.CurrentRow.Cells["length"].Value.ToString()))
            {
                nowlength = decimal.Parse(this.ly_bm0031DataGridView.CurrentRow.Cells["length"].Value.ToString());
            }
            else
            {
                nowlength = 0;
            }

            if (!string.IsNullOrEmpty(this.ly_bm0031DataGridView.CurrentRow.Cells["width"].Value.ToString()))
            {
                nowwidth = decimal.Parse(this.ly_bm0031DataGridView.CurrentRow.Cells["width"].Value.ToString());
            }
            else
            {
                nowwidth = 0;
            }

            if (!string.IsNullOrEmpty(this.ly_bm0031DataGridView.CurrentRow.Cells["height"].Value.ToString()))
            {
                nowheight = decimal.Parse(this.ly_bm0031DataGridView.CurrentRow.Cells["height"].Value.ToString());
            }
            else
            {
                nowheight = 0;
            }

            if (!string.IsNullOrEmpty(this.ly_bm0031DataGridView.CurrentRow.Cells["input_count"].Value.ToString()))
            {
                nowinputcount = decimal.Parse(this.ly_bm0031DataGridView.CurrentRow.Cells["input_count"].Value.ToString());
            }
            else
            {
                nowinputcount =1;
            }

            if (!string.IsNullOrEmpty(this.ly_bm0031DataGridView.CurrentRow.Cells["qty_set_waste"].Value.ToString()))
            {
                qtysetwaste = 100 + decimal.Parse(this.ly_bm0031DataGridView.CurrentRow.Cells["qty_set_waste"].Value.ToString());
            }
            else
            {
                qtysetwaste = 100;
            }


            if ("棒料" == nowgeometry)
            {
                if (this.ly_bm0031DataGridView.CurrentRow.Cells["dw"].Value.ToString() == "m")
                {

                    this.ly_bm0031DataGridView.CurrentRow.Cells["qty_set"].Value = nowlength / 1000 *( qtysetwaste / 100 ) * nowinputcount;

                }

                else
                { 

                    this.ly_bm0031DataGridView.CurrentRow.Cells["qty_set"].Value = decimal.Parse("3.1415927") * nowdiameter * nowdiameter * nowlength / 4 * nowspecific_weight / 1000 / 1000 / nowinputcount * qtysetwaste / 100;
                }
            }

            if ("板料" == nowgeometry)
            {
                this.ly_bm0031DataGridView.CurrentRow.Cells["qty_set"].Value = nowlength * nowwidth * nowheight * nowspecific_weight / 1000 / 1000 / nowinputcount * qtysetwaste/100;

            }
        }

        private void bindingNavigatorAddNewItem_Click(object sender, EventArgs e)
        {
            LY_MaterialAdd queryForm = new LY_MaterialAdd();

            queryForm.material_code = "";
            queryForm.runmode = "增加";
            queryForm.statemode = "原料";

            queryForm.StartPosition = FormStartPosition.CenterParent;
            queryForm.ShowDialog();

            if (queryForm.DialogResult != DialogResult.Cancel)
            {
                this.ly_inma0010machineTableAdapter.Fill(this.lYMaterialMange.ly_inma0010machine);
                this.ly_inma0010machineBindingSource.Position = this.ly_inma0010machineBindingSource.Find("物资编号", queryForm.material_code);
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if (null == ly_inma0010DataGridView.CurrentRow) return;

            if (!checkLock())
            {
                MessageBox.Show("已经锁定无法修改...", "注意");
                return;

            }

            string salespeople = this.ly_inma0010DataGridView.CurrentRow.Cells["负责人"].Value.ToString();
            if (!string.IsNullOrEmpty(salespeople))
            {
                if (salespeople != SQLDatabase.nowUserName())
                {
                    MessageBox.Show("请负责人:" + salespeople + "操作", "注意");
                    return;
                }
            }
            else
            {
                MessageBox.Show("请联系技术部领导进行负责人指定...", "注意");
                return;
            }

            string s = this.ly_inma0010DataGridView.CurrentRow.Cells["物资编号"].Value.ToString();
            //int loanid = int.Parse(this.xD_Client_LoanDataGridView.CurrentRow.Cells["Id_loan"].Value.ToString());

            LY_MaterialAdd queryForm = new LY_MaterialAdd();

            queryForm.statemode = "原料";
            queryForm.runmode = "修改";
            queryForm.material_code = s;

            queryForm.StartPosition = FormStartPosition.CenterParent;
            queryForm.ShowDialog();

            if (queryForm.DialogResult != DialogResult.Cancel)
            {
                this.ly_inma0010machineTableAdapter.Fill(this.lYMaterialMange.ly_inma0010machine );

                this.ly_inma0010machineBindingSource.Position = this.ly_inma0010machineBindingSource.Find("物资编号", s);
            }

        }

        private void bindingNavigatorDeleteItem_Click(object sender, EventArgs e)
        {
            string message = "确定删除当前记录吗？";
            string caption = "提示...";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result;

            if (!checkLock())
            {
                MessageBox.Show("已经锁定无法修改...", "注意");
                return;

            }

            string salespeople = this.ly_inma0010DataGridView.CurrentRow.Cells["负责人"].Value.ToString();
            if (!string.IsNullOrEmpty(salespeople))
            {
                if (salespeople != SQLDatabase.nowUserName())
                {
                    MessageBox.Show("请负责人:" + salespeople + "操作", "注意");
                    return;
                }
            }
            else
            {
                MessageBox.Show("请联系技术部领导进行负责人指定...", "注意");
                return;
            }

            result = MessageBox.Show(message, caption, buttons,
            MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

            if (result == DialogResult.Yes)
            {

                this.ly_inma0010machineBindingSource.RemoveCurrent();


                ly_inma0010DataGridView.EndEdit();
                ly_inma0010machineBindingSource.EndEdit();


                this.ly_inma0010machineTableAdapter.Update(this.lYMaterialMange.ly_inma0010machine );

                //string s = this.xD_Sel_SellBalanceDataGridView.CurrentRow.Cells["编号"].Value.ToString();

                //this.hS_ClientPaymentTableAdapter.Fill(this.xD_SellBalance.HS_ClientPayment, s);


            }
        }

        private void 修改物料信息ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (null == bom_material_selDataGridView.CurrentRow) return;
            string s = this.bom_material_selDataGridView.CurrentRow.Cells["物资编号1"].Value.ToString();
            //int loanid = int.Parse(this.xD_Client_LoanDataGridView.CurrentRow.Cells["Id_loan"].Value.ToString());

            LY_MaterialAdd_BaseMatiral queryForm = new LY_MaterialAdd_BaseMatiral();

            queryForm.statemode = "原料";
            queryForm.runmode = "修改";
            queryForm.material_code = s;

            queryForm.StartPosition = FormStartPosition.CenterParent;
            queryForm.ShowDialog();

            if (queryForm.DialogResult != DialogResult.Cancel)
            {
                

                this.bom_material_selmachineTableAdapter.Fill(this.lYMaterialMange.bom_material_selmachine, parentNum);

                this.bom_material_selmachineBindingSource.Position = this.bom_material_selmachineBindingSource.Find("物资编号", s);
            }

        }

        private void ly_bm0031DataGridView_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            //SetDisplayColumn(sender as DataGridView);

         

          
           


        }

        private void SetDisplayColumn(DataGridView sender)
        {
            DataGridView dgv = sender ;

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
                dgv.Columns["specific_weight"].Visible = true ;
            }

            if ("板料" == nowgeometry)
            {
                dgv.Columns["diameter"].Visible = false;
                dgv.Columns["width"].Visible = true;
                dgv.Columns["length"].Visible = true;
                dgv.Columns["height"].Visible = true;
                dgv.Columns["input_count"].Visible = true;
                dgv.Columns["specific_weight"].Visible = true ;
            }
        }

        private void ly_bm0031DataGridView_SelectionChanged(object sender, EventArgs e)
        {
            SetDisplayColumn(sender as DataGridView);
        }

        //private void fillToolStripButton_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        this.bom_material_selTableAdapter.Fill(this.lYMaterialMange.bom_material_sel, wzbhToolStripTextBox.Text);
        //    }
        //    catch (System.Exception ex)
        //    {
        //        System.Windows.Forms.MessageBox.Show(ex.Message);
        //    }

        //}

        
    }
}
