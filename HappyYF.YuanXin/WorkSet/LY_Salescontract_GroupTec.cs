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
    public partial class LY_Salescontract_GroupTec : Form
    {
       
        private string nowusercode = "";
       

       
        private string nowinnerCode = "";
        private string nowcontractCode = "";

       

      

        public LY_Salescontract_GroupTec()
        {
            InitializeComponent();
        }

      

       
        private void Yonghu_Load(object sender, EventArgs e)
        {
            this.ly_inma0010fjTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            this.ly_inma0010fjTableAdapter.Fill(this.lYMaterialMange.ly_inma0010fj);


            this.ly_lsptb_selTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            this.ly_lsptb_selTableAdapter.Fill(this.lYSalseMange.ly_lsptb_sel);

            this.ly_sales_groupTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
     

            this.ly_plan_getmaterialTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            this.f_PlanExtend_LSPTTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;

            this.ly_plan_getmaterial_departmentTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;

            this.ly_sales_businessTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            this.ly_material_plan_mainTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
           
           

           
      
       

            this.nowusercode = SQLDatabase.NowUserID;


            //this.dateTimePicker3.Text = DateTime.Today.AddMonths(-6).Date.ToString();
            //this.dateTimePicker4.Text = DateTime.Today.AddDays(1).Date.ToString();
            

            this.dateTimePicker1.Text = DateTime.Today.AddMonths(-6).Date.ToString();
            this.dateTimePicker2.Text = DateTime.Today.AddDays(1).Date.ToString();

            this.ly_material_plan_mainperiodproTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;

            this.ly_sales_businessDataGridView.SelectionChanged -= this.ly_sales_businessDataGridView_SelectionChanged;
            this.ly_sales_businessTableAdapter.Fill(this.lYSalseMange.ly_sales_business, "", "full", this.dateTimePicker1.Value, this.dateTimePicker2.Value);
            this.ly_sales_businessDataGridView.SelectionChanged += this.ly_sales_businessDataGridView_SelectionChanged;

            this.ly_material_plan_mainDataGridView.SelectionChanged -= ly_material_plan_mainDataGridView_SelectionChanged;
            this.ly_material_plan_mainperiodproTableAdapter.Fill(this.lYSalseMange.ly_material_plan_mainperiodpro, "LSPT", this.dateTimePicker1.Value, this.dateTimePicker2.Value);
            this.ly_material_plan_mainDataGridView.SelectionChanged += ly_material_plan_mainDataGridView_SelectionChanged;


            this.ly_material_plan_mainperiodproBindingSource.Filter = "录入人 not in('系统直发','系统自动','系统充借')";
            this.ly_material_plan_mainBindingSource.Filter = "录入人 not in('系统直发','系统自动','系统充借')";
        }

      


      

        private TreeNode FindNode( TreeNodeCollection tnParent, string strValue)
        {

            if (tnParent == null) return null;

            //if (tnParent.Text == strValue) return tnParent;



            TreeNode tnRet = null;

            foreach (TreeNode tn in tnParent)
            {



                if (tn.Text == strValue)
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

      

       

        private void toolStripTextBox5_KeyUp(object sender, KeyEventArgs e)
        {
            string filterString;


            filterString = GetDataGridviewMultiFilterString.DGVMultiFilterString(this.ly_material_plan_mainDataGridView, this.toolStripTextBox5.Text);


            this.ly_material_plan_mainperiodproBindingSource.Filter = "("+filterString+")" + " and 录入人 not in('系统直发','系统自动','系统充借')";
        }

        private void toolStripTextBox5_Enter(object sender, EventArgs e)
        {
            toolStripTextBox5.Text = "";

            this.ly_material_plan_mainperiodproBindingSource.Filter = "录入人 not in('系统直发','系统自动','系统充借')";
        }

        //private void toolStripButton16_Click(object sender, EventArgs e)
        //{
        //    ExportDataGridviewTOExcell.ExportDataGridview(this.ly_sales_contract_main1DataGridView, true);
        //}

       

        private void toolStripButton1_Click(object sender, EventArgs e)
        {

        }

       

        private void ly_material_plan_mainDataGridView_SelectionChanged(object sender, EventArgs e)
        {
            if (null == ly_material_plan_mainDataGridView.CurrentRow)
            {

                this.ly_sales_groupTableAdapter.Fill(this.lYSalseMange.ly_sales_group, "");
                this.f_PlanExtend_LSPTTableAdapter.Fill(this.lYSalseMange.f_PlanExtend_LSPT, "");
                this.ly_plan_getmaterial_departmentTableAdapter.Fill(this.lYSalseMange.ly_plan_getmaterial_department, "");

                return;
            }

            string nowplannum = ly_material_plan_mainDataGridView.CurrentRow.Cells["计划编号0"].Value.ToString();
            string nowcontractnum = ly_material_plan_mainDataGridView.CurrentRow.Cells["内部编码0"].Value.ToString();


            if (0 == this.tabControl3.SelectedIndex)
            {

                this.dataGridView2.SelectionChanged -= this.dataGridView2_SelectionChanged;
                this.ly_sales_businessBindingSource.Position = this.ly_sales_businessBindingSource.Find("业务编码", nowcontractnum);
                this.ly_material_plan_mainBindingSource.Position = this.ly_material_plan_mainBindingSource.Find("计划编号", nowplannum);
                this.dataGridView2.SelectionChanged += this.dataGridView2_SelectionChanged;
            }



            this.ly_sales_groupTableAdapter.Fill(this.lYSalseMange.ly_sales_group, nowplannum);
            this.f_PlanExtend_LSPTTableAdapter.Fill(this.lYSalseMange.f_PlanExtend_LSPT, nowplannum);
            this.ly_plan_getmaterial_departmentTableAdapter.Fill(this.lYSalseMange.ly_plan_getmaterial_department, nowplannum);

            //this.nowplanremark = ly_material_plan_mainDataGridView.CurrentRow.Cells["配套要求0"].Value.ToString();
           
            MakeGroupTreeView(nowplannum);

          
        }

        private void MakeGroupTreeView(string nowplannum)
        {
            ////////////////////////////////////////////

            this.treeView2.Nodes.Clear();

            System.Windows.Forms.TreeNode PNode = new System.Windows.Forms.TreeNode();
            PNode.Text = nowplannum;

            string nowplanremark = ly_material_plan_mainDataGridView.CurrentRow.Cells["配套要求0"].Value.ToString();

            PNode.ToolTipText = nowplanremark;
            PNode.Tag = "---";



            //if (SQLDatabase.CheckHaveRight(SQLDatabase.NowUserID, "营业综合信息"))
            //{
            //    TNode.Tag = "";
            //}
            //else if (SQLDatabase.CheckHaveRight(SQLDatabase.NowUserID, "营业区域信息"))
            //{
            //    TNode.Tag = "salesregion_code='" + SQLDatabase.nowSalesregioncode() + "'";
            //}
            //else
            //{
            //    TNode.Tag = "salesperson_code='" + SQLDatabase.NowUserID + "'";

            //}

            this.treeView2.ShowNodeToolTips = true;

            this.treeView2.Nodes.Add(PNode);

            /////////////////////////////////////////////

            DataGridView dgv = this.ly_sales_groupDataGridView;

            string nowgroupCode;
            string nowgroupName;
            string nowremark;
            string nowgroupid;


            foreach (DataGridViewRow dgr in dgv.Rows)
            {

                if (string.IsNullOrEmpty(dgr.Cells["配套编码"].Value.ToString())) continue;
                //nowgroupCode = noworderValue + int.Parse(dgr.Cells["配套编码"].Value.ToString(), System.Globalization.NumberStyles.Number);
                nowgroupCode = dgr.Cells["配套编码"].Value.ToString();
                nowgroupName = dgr.Cells["配套名称"].Value.ToString();
                nowremark = dgr.Cells["配套说明"].Value.ToString();
                nowgroupid = "-" + dgr.Cells["group_id"].Value.ToString();

                System.Windows.Forms.TreeNode TNode = new System.Windows.Forms.TreeNode();

                TNode.Text = nowgroupCode + ":" + nowgroupName;
                TNode.ToolTipText = nowremark;
                TNode.Tag = nowgroupid;
                PNode.Nodes.Add(TNode);


                /////////////////////////
                //if (null == ly_inma0010DataGridView.CurrentRow) return;
                //string s = this.ly_inma0010DataGridView.CurrentRow.Cells["物资编号"].Value.ToString();

                //string selAllString = "SELECT   a.id,  a.parent_id,a.parentno, a.itemno, b.fir_style+':'+ a.itemno +' ' + a.itemname+' '+b.xhc+' '+cast(cast(a.absqty  as int) as varchar(3))+' '+a.unit as itemname  from ly_plan_getmaterial a left join " +
                //                      "      ly_inma0010 AS b ON a.itemno = b.wzbh          " +
                //                      " where material_plan_num='" + nowplannum + "' and sales_group_code='" + nowgroupCode + "' order by b.fir_style,a.itemno";

                string selAllString = "SELECT   a.id,  a.parent_id,a.parentno, a.gitemno, COALESCE(b.fir_style,a.gitemname,'')+':'+  b.xhc+' '+cast(cast(isnull(a.absqty,0)  as int) as varchar(3))+ a.unit as itemname,a.gremark  from ly_plan_getmaterial a left join " +
                                      "      ly_inma0010 AS b ON a.gitemno = b.wzbh          " +
                                      " where material_plan_num='" + nowplannum + "' and sales_group_code='" + nowgroupCode + "' order by b.fir_style,a.gitemno";




                string cString = SQLDatabase.Connectstring; ;
                SqlDataAdapter bomAdapter = new SqlDataAdapter(selAllString, cString);

                bomAdapter.SelectCommand.CommandTimeout = 0;

                DataSet bomData = new DataSet();
                bomAdapter.Fill(bomData);


                MakeTreeView2(bomData.Tables[0], nowgroupid, TNode);
                //this.treeView1.ExpandAll();
                //this.treeView1.SelectedNode = this.treeView1.Nodes[0];
                //this.treeView1.SelectedNode.Expand();

                //this.groupBox1.Text = s + "BOM结构图";
                //////////////////////////


                //MakeTreeView2();

            }

            this.treeView2.ExpandAll();

        }

        private void MakeTreeView2(DataTable table, string ParentID, System.Windows.Forms.TreeNode PNode)
        {


            DataRow[] dr;

            if (null == ParentID)
                dr = table.Select("parent_id is null");
            else
            {

                string expression;
                expression = "parent_id=" + ParentID + "";

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
                        TNode.Tag = d["id"].ToString();
                        TNode.ToolTipText = d["gremark"].ToString();
                        if (PNode == null)
                        {
                            this.treeView2.Nodes.Add(TNode);
                        }
                        else
                        {
                            PNode.Nodes.Add(TNode);
                        }

                        MakeTreeView2(table, d["id"].ToString(), TNode);
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

        private void toolStripButton3_Click(object sender, EventArgs e)
        {

        }

        private void ly_sales_groupDataGridView_SelectionChanged(object sender, EventArgs e)
        {
            if (null == ly_sales_groupDataGridView.CurrentRow)
            {

                this.ly_plan_getmaterialTableAdapter.Fill(this.lYSalseMange.ly_plan_getmaterial, "", "", 0);

                return;
            }

            string nowplannum = ly_material_plan_mainDataGridView.CurrentRow.Cells["计划编号0"].Value.ToString();
            string nowcontractnum = ly_material_plan_mainDataGridView.CurrentRow.Cells["计划编号0"].Value.ToString();
            string nowgroupnum = ly_sales_groupDataGridView.CurrentRow.Cells["配套编码"].Value.ToString();
            int nowgroupid = 0 - int.Parse(ly_sales_groupDataGridView.CurrentRow.Cells["group_id"].Value.ToString());

            this.treeView2.SelectedNode = FindGroupNode(this.treeView2.Nodes, nowgroupid.ToString());
        }

        private void ly_sales_contract_detailDataGridView_DoubleClick(object sender, EventArgs e)
        {
            //if (null == ly_sales_contract_detailDataGridView.CurrentRow) return;

            ////if ("False" == this.contractCanchenged)
            ////{
            ////    MessageBox.Show("合同已经提交执行,不能修改数据...", "注意");
            ////    return;

            ////}

            //string nowmaterialplannum = ly_material_plan_mainDataGridView.CurrentRow.Cells["计划编号0"].Value.ToString();
            //string nowcontractcode = ly_material_plan_mainDataGridView.CurrentRow.Cells["合同编号0"].Value.ToString();
            //string nowsales_group_code = ly_sales_groupDataGridView.CurrentRow.Cells["配套编码"].Value.ToString();


            //string nowparentno = ly_sales_groupDataGridView.CurrentRow.Cells["配套编码"].Value.ToString();
            //string nowparentname = ly_sales_groupDataGridView.CurrentRow.Cells["配套名称"].Value.ToString();

            //string nowitemno = ly_sales_contract_detailDataGridView.CurrentRow.Cells["产品编码"].Value.ToString();
            //string nowitemname = ly_sales_contract_detailDataGridView.CurrentRow.Cells["产品名称"].Value.ToString();

            //string nowunit = ly_sales_contract_detailDataGridView.CurrentRow.Cells["单位"].Value.ToString();

           

            //int nowparent_id=-1;



            //decimal nowabsqty;
            //if ("" != this.ly_sales_contract_detailDataGridView.CurrentRow.Cells["未配套"].Value.ToString())
            //{
            //    nowabsqty = decimal.Parse(ly_sales_contract_detailDataGridView.CurrentRow.Cells["未配套"].Value.ToString());
            //}
            //else
            //{
            //    nowabsqty = 0;
            //}

            //if (nowabsqty == 0)
            //{

            //    MessageBox.Show("产品全部配出,操作取消...", "注意");
            //    return;
            //}





            //this.ly_plan_getmaterialBindingSource.AddNew();


            //this.ly_plan_getmaterialDataGridView.CurrentRow.Cells["产品编号5"].Value = nowitemno;
            //this.ly_plan_getmaterialDataGridView.CurrentRow.Cells["产品名称5"].Value = nowitemname;

            //this.ly_plan_getmaterialDataGridView.CurrentRow.Cells["产品型号5"].Value = nowitemno;
            //this.ly_plan_getmaterialDataGridView.CurrentRow.Cells["单位5"].Value = nowunit;

            //this.ly_plan_getmaterialDataGridView.CurrentRow.Cells["数量5"].Value = nowabsqty;

            //this.ly_plan_getmaterialDataGridView.CurrentRow.Cells["录入人5"].Value = SQLDatabase.nowUserName();

            //this.ly_plan_getmaterialDataGridView.CurrentRow.Cells["合同编号5"].Value = nowcontractcode;

            //this.ly_plan_getmaterialDataGridView.CurrentRow.Cells["计划编号5"].Value = nowmaterialplannum;

            //this.ly_plan_getmaterialDataGridView.CurrentRow.Cells["配套编码5"].Value = nowsales_group_code;

            //this.ly_plan_getmaterialDataGridView.CurrentRow.Cells["父件编号5"].Value = nowsales_group_code;

            //this.ly_plan_getmaterialDataGridView.CurrentRow.Cells["父件名称5"].Value = nowparentname;

            //this.ly_plan_getmaterialDataGridView.CurrentRow.Cells["parent_id5"].Value = nowparent_id;

            //this.ly_plan_getmaterialBindingSource.EndEdit();

            //this.ly_plan_getmaterialTableAdapter.Update(this.lYSalseMange.ly_plan_getmaterial);



            //this.ly_sales_contract_detailTableAdapter.Fill(this.lYSalseMange.ly_sales_contract_detail, nowinnerCode, 0);

            //this.ly_plan_getmaterialTableAdapter.Fill(this.lYSalseMange.ly_plan_getmaterial, nowmaterialplannum, nowsales_group_code, nowsales_group_code);

            //this.ly_plan_getmaterialBindingSource.Position = this.ly_plan_getmaterialBindingSource.Find("产品编号", nowitemno);
           



           

            //this.ly_sales_contract_detailBindingSource.Position = this.ly_sales_contract_detailBindingSource.Find("产品编码", nowitemno);

            //string nowplannum = ly_material_plan_mainDataGridView.CurrentRow.Cells["计划编号0"].Value.ToString();
            //MakeGroupTreeView(nowplannum);


            ////this.ly_sales_contract_detailDataGridView.CurrentCell = this.ly_sales_contract_detailDataGridView.CurrentRow.Cells["单价"];
        }

        private void ly_plan_getmaterialDataGridView_SelectionChanged(object sender, EventArgs e)
        {
            //if (null == ly_plan_getmaterialDataGridView.CurrentRow)
            //{

            //    return;
            //}

            //string nowitemno = ly_plan_getmaterialDataGridView.CurrentRow.Cells["产品编号5"].Value.ToString();
            //this.ly_sales_contract_detailBindingSource.Position = this.ly_sales_contract_detailBindingSource.Find("产品编码", nowitemno);
        }

        private void bindingNavigatorDeleteItem1_Click(object sender, EventArgs e)
        {
            if (null == ly_plan_getmaterialDataGridView.CurrentRow)
            {

                return;
            }

            if ("True" == ly_material_plan_mainDataGridView.CurrentRow.Cells["生产审批0"].Value.ToString())
            {
                MessageBox.Show("依赖书已经生产审批,不能删除数据...", "注意");
                return;

            }

            string salespeople = this.ly_plan_getmaterialDataGridView.CurrentRow.Cells["录入人5"].Value.ToString();

            if (salespeople != SQLDatabase.nowUserName())
            {
                MessageBox.Show("请录入人:" + salespeople + "删除", "注意");
                return;
            }

            //if ("True" == ly_sales_contract_mainDataGridView.CurrentRow.Cells["提交"].Value.ToString())
            //{
            //    MessageBox.Show("合同已经提交,不能删除数据...", "注意");
            //    return;

            //}

            //if ("True" == ly_sales_contract_mainDataGridView.CurrentRow.Cells["批准"].Value.ToString())
            //{
            //    MessageBox.Show("合同已经执行,不能删除数据...", "注意");
            //    return;

            //}

            //if ("True" == ly_sales_contract_mainDataGridView.CurrentRow.Cells["审核"].Value.ToString())
            //{
            //    MessageBox.Show("合同已经审批,不能删除数据...", "注意");
            //    return;

            //}


            string message = "确定删除当前配套产品吗？";
            string caption = "提示...";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result;



            result = MessageBox.Show(message, caption, buttons,
            MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

            if (result == DialogResult.Yes)
            {




                this.ly_plan_getmaterialBindingSource.RemoveCurrent();


                ly_plan_getmaterialDataGridView.EndEdit();
                ly_plan_getmaterialBindingSource.EndEdit();



                this.ly_plan_getmaterialTableAdapter.Update(this.lYSalseMange.ly_plan_getmaterial);


                //this.ly_sales_contract_detailTableAdapter.Fill(this.lYSalseMange.ly_sales_contract_detail, nowinnerCode, 0);

                if (null != ly_plan_getmaterialDataGridView.CurrentRow)
                {

                    string nowitemno = ly_plan_getmaterialDataGridView.CurrentRow.Cells["产品编号5"].Value.ToString();
                    //this.ly_sales_contract_detailBindingSource.Position = this.ly_sales_contract_detailBindingSource.Find("产品编码", nowitemno);
                }

                string nowplannum = ly_material_plan_mainDataGridView.CurrentRow.Cells["计划编号0"].Value.ToString();
                string nowNodeTag = this.treeView2.SelectedNode.Tag.ToString();

                //this.nowplanremark = ly_material_plan_mainDataGridView.CurrentRow.Cells["配套要求0"].Value.ToString();

                MakeGroupTreeView(nowplannum);
             
                this.treeView2.SelectedNode = FindGroupNode(this.treeView2.Nodes, nowNodeTag);


                //this.ly_sales_contract_main1DataGridView.SelectionChanged -= ly_sales_contract_main1DataGridView_SelectionChanged;
                //this.ly_sales_contract_main1TableAdapter.Fill(this.lYSalseMange.ly_sales_contract_main1, this.nowusercode, "single", this.dateTimePicker1.Value, this.dateTimePicker2.Value);
                //this.ly_sales_contract_main1DataGridView.SelectionChanged += ly_sales_contract_main1DataGridView_SelectionChanged;
            }
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {

        }

        private void ly_plan_getmaterialDataGridView_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            DataGridView dgv = sender as DataGridView;

            if (null == dgv.CurrentRow) return;


            if ("True" == ly_material_plan_mainDataGridView.CurrentRow.Cells["生产审批0"].Value.ToString())
            {
                MessageBox.Show("依赖书已经生产审批,不能修改数据...", "注意");
                return;

            }




            //if ("True" == dgv.CurrentRow.Cells["批准"].Value.ToString())
            //{
            //    MessageBox.Show("合同已经执行,不能修改数据...", "注意");
            //    return;

            //}


           


            //if ("True" == dgv.CurrentRow.Cells["提交"].Value.ToString())
            //{
            //    MessageBox.Show("合同已经提交,不能修改数据...", "注意");
            //    return;

            //}


            //if ("True" == dgv.CurrentRow.Cells["审核"].Value.ToString())
            //{
            //    MessageBox.Show("合同已经审批,不能修改数据...", "注意");
            //    return;

            //}
            ///////////////////////////////////////////////////////////////
            if ("台用量1" == dgv.CurrentCell.OwningColumn.Name)
            {

                return;

                ChangeValue queryForm = new ChangeValue();

                queryForm.OldValue = dgv.CurrentCell.Value.ToString();
                queryForm.NewValue = "";
                queryForm.ChangeMode = "value";
                queryForm.ShowDialog();




                if (queryForm.NewValue != "")
                {



                    dgv.CurrentRow.Cells["台用量1"].Value = queryForm.NewValue;
                    //dgv.CurrentRow.Cells["discount_money"].Value = DBNull.Value;

                    //dgv.CurrentRow.Cells["approve_flag"].Value = "False";

                    this.ly_plan_getmaterialBindingSource.EndEdit();
                    this.ly_plan_getmaterialTableAdapter.Update(this.lYSalseMange.ly_plan_getmaterial);

                    //this.ly_sales_contract_detailTableAdapter.Fill(this.lYSalseMange.ly_sales_contract_detail, nowinnerCode, 0);

                    //string nowplannum = ly_material_plan_mainDataGridView.CurrentRow.Cells["计划编号0"].Value.ToString();
                    //string nowNodeTag = this.treeView2.SelectedNode.Tag.ToString();
                    //MakeGroupTreeView(nowplannum);



                    //this.treeView2.SelectedNode = FindGroupNode(this.treeView2.Nodes, nowNodeTag);

                    //CountPlanStru();

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

            ///////////////////////////////////////////////////////////////
            if ("数量5" == dgv.CurrentCell.OwningColumn.Name)
            {

                //if ("True" == dgv.CurrentRow.Cells["审批"].Value.ToString())
                //{

                //    MessageBox.Show("合同已经审批,不能修改数据...", "注意");
                //    return;
                //}

                ChangeValue queryForm = new ChangeValue();

                queryForm.OldValue = dgv.CurrentCell.Value.ToString();
                queryForm.NewValue = "";
                queryForm.ChangeMode = "value";
                queryForm.ShowDialog();

               


                if (queryForm.NewValue != "")


                {

                    //decimal allabsqty;
                    //if ("" != this.ly_sales_contract_detailDataGridView.CurrentRow.Cells["数量"].Value.ToString())
                    //{
                    //    allabsqty = decimal.Parse(ly_sales_contract_detailDataGridView.CurrentRow.Cells["数量"].Value.ToString());
                    //}
                    //else
                    //{
                    //    allabsqty = 0;
                    //}

                    //decimal haveabsqty;
                    //if ("" != this.ly_sales_contract_detailDataGridView.CurrentRow.Cells["已配套"].Value.ToString())
                    //{
                    //    haveabsqty = decimal.Parse(ly_sales_contract_detailDataGridView.CurrentRow.Cells["已配套"].Value.ToString());
                    //}
                    //else
                    //{
                    //    haveabsqty = 0;
                    //}



                    //decimal nowabsqty;
                    //if ("" != this.ly_sales_contract_detailDataGridView.CurrentRow.Cells["未配套"].Value.ToString())
                    //{
                    //    nowabsqty = decimal.Parse(ly_sales_contract_detailDataGridView.CurrentRow.Cells["未配套"].Value.ToString());
                    //}
                    //else
                    //{
                    //    nowabsqty = 0;
                    //}

                    //if ((allabsqty - (haveabsqty - decimal.Parse(queryForm.OldValue) + decimal.Parse(queryForm.NewValue))) < 0)

                    ////if (nowabsqty == 0)
                    //{

                    //    MessageBox.Show("数量不合适,操作取消...", "注意");
                    //    return;
                    //}
                    
                    dgv.CurrentRow.Cells["数量5"].Value = queryForm.NewValue;

                    
                    //dgv.CurrentRow.Cells["discount_money"].Value = DBNull.Value;

                    //dgv.CurrentRow.Cells["approve_flag"].Value = "False";ly_plan_getmaterialDataGridView

                    this.ly_plan_getmaterialBindingSource.EndEdit();
                    try
                    {

                        this.ly_plan_getmaterialTableAdapter.Update(this.lYSalseMange.ly_plan_getmaterial);
                    }

                    catch (SqlException sqle)
                    {

                        MessageBox.Show(sqle .Message.Split ('\r')[0], "注意");
                        //MessageBox.Show(sqle.Message, "注意");
                    }

                    string nowitemno = ly_plan_getmaterialDataGridView.CurrentRow.Cells["发货编码"].Value.ToString();

                   
                    //string nowcontractnum = ly_material_plan_mainDataGridView.CurrentRow.Cells["计划编号0"].Value.ToString();
                    //string nowgroupnum = ly_sales_groupDataGridView.CurrentRow.Cells["配套编码"].Value.ToString();
                    //int nowgroupid = 0 - int.Parse(ly_sales_groupDataGridView.CurrentRow.Cells["group_id"].Value.ToString());

                    //this.ly_sales_contract_detailTableAdapter.Fill(this.lYSalseMange.ly_sales_contract_detail, nowinnerCode, 0);

                    string nowplannum = ly_material_plan_mainDataGridView.CurrentRow.Cells["计划编号0"].Value.ToString();
                    string nowNodeTag = this.treeView2.SelectedNode.Tag.ToString();
                    MakeGroupTreeView(nowplannum);

                   

                    this.treeView2.SelectedNode = FindGroupNode(this.treeView2.Nodes, nowNodeTag);


                    ////

                    //this.ly_plan_getmaterialTableAdapter.Fill(this.lYSalseMange.ly_plan_getmaterial, nowplannum, nowgroupnum, nowgroupid);

                    this.ly_plan_getmaterialBindingSource.Position = this.ly_plan_getmaterialBindingSource.Find("产品编号", nowitemno);

                    //CountPlanStru();

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



            ///////////////////////////////////////////////////////////////

            if ("设备要求5" == dgv.CurrentCell.OwningColumn.Name)
            {

                ChangeValue queryForm = new ChangeValue();

                queryForm.OldValue = dgv.CurrentCell.Value.ToString();
                queryForm.NewValue = "";
                queryForm.ChangeMode = "longstring";
                queryForm.ShowDialog();




                if (queryForm.NewValue != "")
                {
                    dgv.CurrentRow.Cells["设备要求5"].Value = queryForm.NewValue;
                    //dgv.CurrentRow.Cells["discount_money"].Value = DBNull.Value;

                    //dgv.CurrentRow.Cells["approve_flag"].Value = "False";
                    this.ly_plan_getmaterialBindingSource.EndEdit();
                    this.ly_plan_getmaterialTableAdapter.Update(this.lYSalseMange.ly_plan_getmaterial);

                    //this.ly_sales_contract_detailTableAdapter.Fill(this.lYSalseMange.ly_sales_contract_detail, nowinnerCode, 0);

                    //CountPlanStru();

                }
                else
                {

                }
                return;

            }


          

        }

        private void toolStripButton28_Click(object sender, EventArgs e)
        {

            this.ly_sales_businessDataGridView.SelectionChanged -= this.ly_sales_businessDataGridView_SelectionChanged;
            this.ly_sales_businessTableAdapter.Fill(this.lYSalseMange.ly_sales_business, "", "full", this.dateTimePicker1.Value, this.dateTimePicker2.Value);
            this.ly_sales_businessDataGridView.SelectionChanged += this.ly_sales_businessDataGridView_SelectionChanged;

            
            this.ly_material_plan_mainDataGridView.SelectionChanged -= ly_material_plan_mainDataGridView_SelectionChanged;
            this.ly_material_plan_mainperiodproTableAdapter.Fill(this.lYSalseMange.ly_material_plan_mainperiodpro, "LSPT", this.dateTimePicker1.Value, this.dateTimePicker2.Value);
            this.ly_material_plan_mainDataGridView.SelectionChanged += ly_material_plan_mainDataGridView_SelectionChanged;
        }

        private void treeView2_AfterSelect(object sender, TreeViewEventArgs e)
        {


            if (e.Node.Level < 1)
            {
                this.ly_plan_getmaterialTableAdapter.Fill(this.lYSalseMange.ly_plan_getmaterial, "", "", 0);
                return;
            }

            int nowgroupid;

            System.Windows.Forms.TreeNode tempNode = new System.Windows.Forms.TreeNode();

            tempNode = e.Node;

            while (tempNode.Level != 1)
            {

                tempNode = tempNode.Parent;
            }

            nowgroupid = 0 - int.Parse(tempNode.Tag.ToString());

            this.ly_sales_groupDataGridView.SelectionChanged -= ly_sales_groupDataGridView_SelectionChanged;

            this.ly_sales_groupBindingSource.Position = this.ly_sales_groupBindingSource.Find("id", nowgroupid);

            this.ly_sales_groupDataGridView.SelectionChanged += ly_sales_groupDataGridView_SelectionChanged;

            //2018-09-25方新添加
            if (ly_sales_groupDataGridView.CurrentRow == null)
                return;

            string nowplannum = ly_material_plan_mainDataGridView.CurrentRow.Cells["计划编号0"].Value.ToString();
            string nowcontractnum = ly_material_plan_mainDataGridView.CurrentRow.Cells["计划编号0"].Value.ToString();

            string nowgroupnum = ly_sales_groupDataGridView.CurrentRow.Cells["配套编码"].Value.ToString();


            int nowparentid = int.Parse(e.Node.Tag.ToString());


            this.ly_plan_getmaterialTableAdapter.Fill(this.lYSalseMange.ly_plan_getmaterial, nowplannum, nowgroupnum, nowparentid);


            if (e.Node.Level > 1)
            {
                this.f_PlanExtend_LSPTBindingSource.Position = this.f_PlanExtend_LSPTBindingSource.Find("real_id", nowparentid);
            }
        }

        private void toolStripTextBox6_KeyUp(object sender, KeyEventArgs e)
        {
            string filterString;


            filterString = GetDataGridviewMultiFilterString.DGVMultiFilterString(this.ly_lsptb_selDataGridView, this.toolStripTextBox6.Text);


            this.ly_lsptb_selBindingSource.Filter = filterString;
        }

        private void toolStripTextBox6_Enter(object sender, EventArgs e)
        {
            toolStripTextBox6.Text = "";

            this.ly_lsptb_selBindingSource.Filter = "";
        }

        private void toolStripTextBox1_KeyUp(object sender, KeyEventArgs e)
        {
            string filterString;


            filterString = GetDataGridviewMultiFilterString.DGVMultiFilterString(this.ly_inma0010DataGridView, this.toolStripTextBox1.Text);


            this.ly_inma0010fjBindingSource.Filter = filterString;
        }

        private void toolStripTextBox1_Enter(object sender, EventArgs e)
        {
            toolStripTextBox1.Text = "";

            this.ly_inma0010fjBindingSource.Filter = "";
        }

        private void toolStripButton3_Click_1(object sender, EventArgs e)
        {
            this.ly_inma0010fjTableAdapter.Fill(this.lYMaterialMange.ly_inma0010fj);

        }

        private void toolStripButton29_Click(object sender, EventArgs e)
        {
            this.ly_lsptb_selTableAdapter.Fill(this.lYSalseMange.ly_lsptb_sel);
        }

        private void ly_lsptb_selDataGridView_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (null == ly_lsptb_selDataGridView.CurrentRow) return;

            if (null == this.treeView2.SelectedNode)
            {
                MessageBox.Show("请选择父件...", "注意");
                return;
            }
            if (this.treeView2.SelectedNode.Level < 1)
            {
                MessageBox.Show("父件选择不合适...", "注意");
                return;
            }
            if ("True" == ly_material_plan_mainDataGridView.CurrentRow.Cells["配套完成0"].Value.ToString())
            {
                MessageBox.Show("依赖书已经配套完成,不能修改数据...", "注意");
                return;

            }

            if ("True" == ly_material_plan_mainDataGridView.CurrentRow.Cells["生产审批0"].Value.ToString())
            {
                MessageBox.Show("依赖书已经生产审批,不能修改数据...", "注意");
                return;

            }
            //2018-09-25方新添加
            if (ly_sales_groupDataGridView.CurrentRow == null)
            {
                MessageBox.Show("没有配套编码...", "注意");
                return;

            }
            string nowmaterialplannum = ly_material_plan_mainDataGridView.CurrentRow.Cells["计划编号0"].Value.ToString();
            string nowcontractcode = ly_material_plan_mainDataGridView.CurrentRow.Cells["合同编号0"].Value.ToString();

            string nowsales_group_code = ly_sales_groupDataGridView.CurrentRow.Cells["配套编码"].Value.ToString();


            //string nowparentno = ly_sales_groupDataGridView.CurrentRow.Cells["配套编码"].Value.ToString();
            //string nowparentname = ly_sales_groupDataGridView.CurrentRow.Cells["配套名称"].Value.ToString();

            string nowitemno = ly_lsptb_selDataGridView.CurrentRow.Cells["物料编号2"].Value.ToString();
            string nowitemname = ly_lsptb_selDataGridView.CurrentRow.Cells["物料名称2"].Value.ToString();

            string nowunit = ly_lsptb_selDataGridView.CurrentRow.Cells["单位2"].Value.ToString();

            string nowsecstyle = ly_lsptb_selDataGridView.CurrentRow.Cells["sec_style"].Value.ToString();
            //string now se sec_style 
            //int nowparent_id = 0 - int.Parse(ly_sales_groupDataGridView.CurrentRow.Cells["group_id"].Value.ToString());

            int nowparent_id = int.Parse(this.treeView2.SelectedNode.Tag.ToString());

            //string nowparent_firstyle = Getparent_firStyle(nowparent_id);

            //decimal nowparent_count = Getparent_count(nowparent_id);



            if ("测量" == nowsecstyle && 0 < nowparent_id)
            {

                string nowparent_firstyle = "";
                decimal nowparent_count = 0;
                string nowparent_xhj = "";
                decimal nowstandardcount = 0;
                decimal nowgivecount = 0;




                Getparent_Information(nowparent_id, out nowparent_firstyle, out  nowparent_count, out nowparent_xhj, out  nowstandardcount, out  nowgivecount);

          
                
                
                if ("02测量部" != nowparent_firstyle)
                {
                    MessageBox.Show("测量备件只能配在测量部...", "注意");
                    return;

                }
                else
                {

                    this.ly_plan_getmaterialBindingSource.AddNew();


                    this.ly_plan_getmaterialDataGridView.CurrentRow.Cells["发货编码"].Value = nowitemno;
                    this.ly_plan_getmaterialDataGridView.CurrentRow.Cells["发货名称"].Value = nowitemname;

                    this.ly_plan_getmaterialDataGridView.CurrentRow.Cells["standardcount"].Value = nowstandardcount;
                    this.ly_plan_getmaterialDataGridView.CurrentRow.Cells["givecount"].Value = nowgivecount;

                    //this.ly_plan_getmaterialDataGridView.CurrentRow.Cells["产品型号5"].Value = nowitemno;
                    this.ly_plan_getmaterialDataGridView.CurrentRow.Cells["单位5"].Value = nowunit;

                    //this.ly_plan_getmaterialDataGridView.CurrentRow.Cells["数量5"].Value = nowabsqty;

                    this.ly_plan_getmaterialDataGridView.CurrentRow.Cells["录入人5"].Value = SQLDatabase.nowUserName();

                    this.ly_plan_getmaterialDataGridView.CurrentRow.Cells["合同编号5"].Value = nowcontractcode;

                    this.ly_plan_getmaterialDataGridView.CurrentRow.Cells["计划编号5"].Value = nowmaterialplannum;

                    this.ly_plan_getmaterialDataGridView.CurrentRow.Cells["配套编码5"].Value = nowsales_group_code;

                    this.ly_plan_getmaterialDataGridView.CurrentRow.Cells["台用量1"].Value = ly_lsptb_selDataGridView.CurrentRow.Cells["台用量"].Value;

                    this.ly_plan_getmaterialDataGridView.CurrentRow.Cells["产品型号5"].Value = ly_lsptb_selDataGridView.CurrentRow.Cells["中方型号0"].Value;
                    this.ly_plan_getmaterialDataGridView.CurrentRow.Cells["gg"].Value = ly_lsptb_selDataGridView.CurrentRow.Cells["规格0"].Value;

                    //this.ly_plan_getmaterialDataGridView.CurrentRow.Cells["父件名称5"].Value = nowparentname;

                    this.ly_plan_getmaterialDataGridView.CurrentRow.Cells["parent_id5"].Value = nowparent_id;

                    this.ly_plan_getmaterialBindingSource.EndEdit();

                    this.ly_plan_getmaterialTableAdapter.Update(this.lYSalseMange.ly_plan_getmaterial);



                    //this.ly_sales_contract_detailTableAdapter.Fill(this.lYSalseMange.ly_sales_contract_detail, nowinnerCode, 0);

                    //this.ly_plan_getmaterialTableAdapter.Fill(this.lYSalseMange.ly_plan_getmaterial, nowmaterialplannum, nowsales_group_code, nowparent_id);




                    string nowNodeTag = this.treeView2.SelectedNode.Tag.ToString();


                    MakeGroupTreeView(nowmaterialplannum);

                    this.treeView2.SelectedNode = FindGroupNode(this.treeView2.Nodes, nowNodeTag);

                    this.ly_plan_getmaterialBindingSource.Position = this.ly_plan_getmaterialBindingSource.Find("产品编号", nowitemno);
                }
                return;
            }


            //int nowparent_id=-1;



            //decimal nowabsqty;
            //if ("" != this.ly_sales_contract_detailDataGridView.CurrentRow.Cells["未配套"].Value.ToString())
            //{
            //    nowabsqty = decimal.Parse(ly_sales_contract_detailDataGridView.CurrentRow.Cells["未配套"].Value.ToString());
            //}
            //else
            //{
            //    nowabsqty = 0;
            //}

            //if (nowabsqty == 0)
            //{

            //    MessageBox.Show("产品全部配出,操作取消...", "注意");
            //    return;
            //}





            this.ly_plan_getmaterialBindingSource.AddNew();


            this.ly_plan_getmaterialDataGridView.CurrentRow.Cells["发货编码"].Value = nowitemno;
            this.ly_plan_getmaterialDataGridView.CurrentRow.Cells["发货名称"].Value = nowitemname;

            //this.ly_plan_getmaterialDataGridView.CurrentRow.Cells["产品型号5"].Value = nowitemno;
            this.ly_plan_getmaterialDataGridView.CurrentRow.Cells["单位5"].Value = nowunit;

            //this.ly_plan_getmaterialDataGridView.CurrentRow.Cells["数量5"].Value = nowabsqty;

            this.ly_plan_getmaterialDataGridView.CurrentRow.Cells["录入人5"].Value = SQLDatabase.nowUserName();

            this.ly_plan_getmaterialDataGridView.CurrentRow.Cells["合同编号5"].Value = nowcontractcode;

            this.ly_plan_getmaterialDataGridView.CurrentRow.Cells["计划编号5"].Value = nowmaterialplannum;

            this.ly_plan_getmaterialDataGridView.CurrentRow.Cells["配套编码5"].Value = nowsales_group_code;

            this.ly_plan_getmaterialDataGridView.CurrentRow.Cells["台用量1"].Value = ly_lsptb_selDataGridView.CurrentRow.Cells["台用量"].Value;

            this.ly_plan_getmaterialDataGridView.CurrentRow.Cells["产品型号5"].Value = ly_lsptb_selDataGridView.CurrentRow.Cells["中方型号0"].Value;
            this.ly_plan_getmaterialDataGridView.CurrentRow.Cells["gg"].Value = ly_lsptb_selDataGridView.CurrentRow.Cells["规格0"].Value;

            //this.ly_plan_getmaterialDataGridView.CurrentRow.Cells["父件名称5"].Value = nowparentname;

            this.ly_plan_getmaterialDataGridView.CurrentRow.Cells["parent_id5"].Value = nowparent_id;

            this.ly_plan_getmaterialBindingSource.EndEdit();

            this.ly_plan_getmaterialTableAdapter.Update(this.lYSalseMange.ly_plan_getmaterial);



            //this.ly_sales_contract_detailTableAdapter.Fill(this.lYSalseMange.ly_sales_contract_detail, nowinnerCode, 0);

            //this.ly_plan_getmaterialTableAdapter.Fill(this.lYSalseMange.ly_plan_getmaterial, nowmaterialplannum, nowsales_group_code, nowparent_id);

            


            string nowNodeTag1 = this.treeView2.SelectedNode.Tag .ToString ();

          
            MakeGroupTreeView(nowmaterialplannum);

            this.treeView2.SelectedNode = FindGroupNode(this.treeView2.Nodes, nowNodeTag1);

            this.ly_plan_getmaterialBindingSource.Position = this.ly_plan_getmaterialBindingSource.Find("产品编号", nowitemno);
        }


        private void   Getparent_Information(int itemid,out string firStyle,out decimal parentCount,out string  parentXHJ,out decimal standardcount ,out decimal givecount )
        {
            SqlConnection sqlConnection1 = new SqlConnection(SQLDatabase.Connectstring);
            SqlCommand cmd = new SqlCommand();

         

            cmd.Parameters.Add("@NowId", SqlDbType.Int);
            cmd.Parameters["@NowId"].Value = itemid;

            
            cmd.Parameters.Add("@nowfir_style", SqlDbType.VarChar);
            cmd.Parameters["@nowfir_style"].Direction = ParameterDirection.Output;
            cmd.Parameters["@nowfir_style"].Size  = 10;


          
            cmd.Parameters.Add("@nowparentCount", SqlDbType.Decimal);
            cmd.Parameters["@nowparentCount"].Direction = ParameterDirection.Output;
            cmd.Parameters["@nowparentCount"].Size  =10;



           
            cmd.Parameters.Add("@parentXHJ", SqlDbType.VarChar );
            cmd.Parameters["@parentXHJ"].Direction = ParameterDirection.Output;
            cmd.Parameters["@parentXHJ"].Size  = 50;

            cmd.Parameters.Add("@nowstandardcount", SqlDbType.Decimal);
            cmd.Parameters["@nowstandardcount"].Direction = ParameterDirection.Output;
            cmd.Parameters["@nowstandardcount"].Size  =10;

            cmd.Parameters.Add("@nowgivecount", SqlDbType.Decimal);
            cmd.Parameters["@nowgivecount"].Direction = ParameterDirection.Output;
            cmd.Parameters["@nowgivecount"].Size  =10;



         
            cmd.CommandText = "LY_GetParent_Fir";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = sqlConnection1;

            sqlConnection1.Open();
           cmd.ExecuteNonQuery();
            sqlConnection1.Close();

            firStyle = cmd.Parameters["@nowfir_style"].Value.ToString();
            parentCount = decimal.Parse(cmd.Parameters["@nowparentCount"].Value.ToString());
            parentXHJ = cmd.Parameters["@parentXHJ"].Value.ToString();

            standardcount = decimal.Parse(cmd.Parameters["@nowstandardcount"].Value.ToString());
            givecount = decimal.Parse(cmd.Parameters["@nowgivecount"].Value.ToString());
            //return Parentfir_Style;

        }

        private string Getparent_firStyle(int itemid)
        {
            SqlConnection sqlConnection1 = new SqlConnection(SQLDatabase.Connectstring);
            SqlCommand cmd = new SqlCommand();

            string Parentfir_Style = "";

            cmd.Parameters.Add("@NowId", SqlDbType.Int);
            cmd.Parameters["@NowId"].Value = itemid;


            cmd.CommandText = "LY_GetParent_Fir";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = sqlConnection1;

            sqlConnection1.Open();
            Parentfir_Style = cmd.ExecuteScalar().ToString();
            sqlConnection1.Close();



            return Parentfir_Style;
        
        }

        private decimal  Getparent_count(int itemid)
        {
            SqlConnection sqlConnection1 = new SqlConnection(SQLDatabase.Connectstring);
            SqlCommand cmd = new SqlCommand();

            decimal  ParentCount = 0;

            cmd.Parameters.Add("@NowId", SqlDbType.Int);
            cmd.Parameters["@NowId"].Value = itemid;


            cmd.CommandText = "LY_GetParent_Count";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = sqlConnection1;

            sqlConnection1.Open();
            ParentCount =decimal .Parse ( cmd.ExecuteScalar().ToString());
            sqlConnection1.Close();



            return ParentCount;

        }

        private void ly_inma0010DataGridView_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {

            if (null == ly_inma0010DataGridView.CurrentRow) return;

            if (null==this.treeView2.SelectedNode)
            {
                MessageBox.Show("请选择父件...", "注意");
                return;
            }

            if (this.treeView2.SelectedNode.Level < 1)
            {
                MessageBox.Show("父件选择不合适...", "注意");
                return;
            }

            if ("True" == ly_material_plan_mainDataGridView.CurrentRow.Cells["生产审批0"].Value.ToString())
            {
                MessageBox.Show("依赖书已经生产审批,不能修改数据...", "注意");
                return;

            }

            string nowmaterialplannum = ly_material_plan_mainDataGridView.CurrentRow.Cells["计划编号0"].Value.ToString();
            string nowcontractcode = ly_material_plan_mainDataGridView.CurrentRow.Cells["合同编号0"].Value.ToString();

            string nowsales_group_code = ly_sales_groupDataGridView.CurrentRow.Cells["配套编码"].Value.ToString();


            //string nowparentno = ly_sales_groupDataGridView.CurrentRow.Cells["配套编码"].Value.ToString();
            //string nowparentname = ly_sales_groupDataGridView.CurrentRow.Cells["配套名称"].Value.ToString();

            string nowitemno = ly_inma0010DataGridView.CurrentRow.Cells["物资编号"].Value.ToString();
            string nowitemname = ly_inma0010DataGridView.CurrentRow.Cells["名称"].Value.ToString();

            string nowunit = ly_inma0010DataGridView.CurrentRow.Cells["单位"].Value.ToString();


            //int nowparent_id = 0 - int.Parse(ly_sales_groupDataGridView.CurrentRow.Cells["group_id"].Value.ToString());

            int nowparent_id = int.Parse(this.treeView2.SelectedNode.Tag.ToString());


            //int nowparent_id=-1;



            //decimal nowabsqty;
            //if ("" != this.ly_sales_contract_detailDataGridView.CurrentRow.Cells["未配套"].Value.ToString())
            //{
            //    nowabsqty = decimal.Parse(ly_sales_contract_detailDataGridView.CurrentRow.Cells["未配套"].Value.ToString());
            //}
            //else
            //{
            //    nowabsqty = 0;
            //}

            //if (nowabsqty == 0)
            //{

            //    MessageBox.Show("产品全部配出,操作取消...", "注意");
            //    return;
            //}





            this.ly_plan_getmaterialBindingSource.AddNew();


            this.ly_plan_getmaterialDataGridView.CurrentRow.Cells["发货编码"].Value = nowitemno;
            this.ly_plan_getmaterialDataGridView.CurrentRow.Cells["发货名称"].Value = nowitemname;

            //this.ly_plan_getmaterialDataGridView.CurrentRow.Cells["产品型号5"].Value = nowitemno;
            this.ly_plan_getmaterialDataGridView.CurrentRow.Cells["单位5"].Value = nowunit;

            //this.ly_plan_getmaterialDataGridView.CurrentRow.Cells["数量5"].Value = nowabsqty;

            this.ly_plan_getmaterialDataGridView.CurrentRow.Cells["录入人5"].Value = SQLDatabase.nowUserName();

            this.ly_plan_getmaterialDataGridView.CurrentRow.Cells["合同编号5"].Value = nowcontractcode;

            this.ly_plan_getmaterialDataGridView.CurrentRow.Cells["计划编号5"].Value = nowmaterialplannum;

            this.ly_plan_getmaterialDataGridView.CurrentRow.Cells["配套编码5"].Value = nowsales_group_code;

            //this.ly_plan_getmaterialDataGridView.CurrentRow.Cells["父件编号5"].Value = nowsales_group_code;

            //this.ly_plan_getmaterialDataGridView.CurrentRow.Cells["父件名称5"].Value = nowparentname;

            this.ly_plan_getmaterialDataGridView.CurrentRow.Cells["parent_id5"].Value = nowparent_id;

            this.ly_plan_getmaterialBindingSource.EndEdit();

            this.ly_plan_getmaterialTableAdapter.Update(this.lYSalseMange.ly_plan_getmaterial);



            //this.ly_sales_contract_detailTableAdapter.Fill(this.lYSalseMange.ly_sales_contract_detail, nowinnerCode, 0);

            this.ly_plan_getmaterialTableAdapter.Fill(this.lYSalseMange.ly_plan_getmaterial, nowmaterialplannum, nowsales_group_code, nowparent_id);

            this.ly_plan_getmaterialBindingSource.Position = this.ly_plan_getmaterialBindingSource.Find("产品编号", nowitemno);


            string nowNodeTag = this.treeView2.SelectedNode.Tag.ToString();


            MakeGroupTreeView(nowmaterialplannum);

            this.treeView2.SelectedNode = FindGroupNode(this.treeView2.Nodes, nowNodeTag);
        }

        private void f_PlanExtend_LSPTDataGridView_SelectionChanged(object sender, EventArgs e)
        {
            if (null == f_PlanExtend_LSPTDataGridView.CurrentRow)
            {

                //this.ly_plan_getmaterialTableAdapter.Fill(this.lYSalseMange.ly_plan_getmaterial, "", "", 0);

                return;
            }

            string nowplannum = ly_material_plan_mainDataGridView.CurrentRow.Cells["计划编号0"].Value.ToString();
            
            string nowgroupnum = ly_sales_groupDataGridView.CurrentRow.Cells["配套编码"].Value.ToString();


            string nowNodeTag = this.f_PlanExtend_LSPTDataGridView.CurrentRow.Cells["real_id"].Value.ToString();

            treeView2.AfterSelect -= treeView2_AfterSelect;

            this.treeView2.SelectedNode = FindGroupNode(this.treeView2.Nodes, nowNodeTag);

            this.ly_plan_getmaterialTableAdapter.Fill(this.lYSalseMange.ly_plan_getmaterial, nowplannum, nowgroupnum, int.Parse (nowNodeTag));

            treeView2.AfterSelect += treeView2_AfterSelect;
        }

        private void ly_material_plan_mainDataGridView_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            DataGridView dgv = sender as DataGridView;

            if (null == dgv.CurrentRow) return;

            if ("True" == ly_material_plan_mainDataGridView.CurrentRow.Cells["生产审批0"].Value.ToString())
            {
                MessageBox.Show("依赖书已经生产审批,不能修改数据...", "注意");
                return;

            }

            //if ("True" == ly_material_plan_mainDataGridView.CurrentRow.Cells["配套完成0"].Value.ToString())
            //{
            //    MessageBox.Show("依赖书已经配套完成,不能修改数据...", "注意");
            //    return;

            //}

            ///////////////////////////////////////////////////////////配套完成0
            if ("发货日期0" == dgv.CurrentCell.OwningColumn.Name)
            {



                string nowplannum = ly_material_plan_mainDataGridView.CurrentRow.Cells["计划编号0"].Value.ToString();

                ChangeValue queryForm = new ChangeValue();

                queryForm.OldValue = dgv.CurrentCell.Value.ToString();
                queryForm.NewValue = "";
                queryForm.ChangeMode = "datetime";
                queryForm.ShowDialog();


                if (queryForm.NewValue != "")
                {
                    //dgv.CurrentRow.Cells["发货日期0"].Value = queryForm.NewValue;

                    if (SQLDatabase.updateMainplan(nowplannum, "发货日期", queryForm.NewValue, SQLDatabase.nowUserName()))
                    {

                        dgv.CurrentRow.Cells["发货日期0"].Value = queryForm.NewValue;
                    }


                }
                else
                {

                    //dgv.CurrentRow.Cells["发货日期0"].Value = DBNull.Value;

                    if (SQLDatabase.updateMainplan(nowplannum, "发货日期", "null", SQLDatabase.nowUserName()))
                    {

                        dgv.CurrentRow.Cells["发货日期0"].Value = DBNull.Value;
                    }


                }

                SQLDatabase.dataChangeREC(dgv.CurrentRow.Cells["mainplan_id"].Value.ToString(), "ly_material_plan_main", "UPD", this.Text);

                this.ly_material_plan_mainperiodproBindingSource.EndEdit();
                //this.ly_material_plan_mainperiodproTableAdapter.Update(this.lYSalseMange.ly_material_plan_mainperiodpro);

                //string nowplannum = ly_material_plan_mainDataGridView.CurrentRow.Cells["计划编号0"].Value.ToString();
                this.f_PlanExtend_LSPTTableAdapter.Fill(this.lYSalseMange.f_PlanExtend_LSPT, nowplannum);



                return;

            }
            ///////////////////////////////////////////////////////////////

            ///////////////////////////////////////////////////////////
            if ("配套完成0" == dgv.CurrentCell.OwningColumn.Name)
            {
                //if (null == ly_sales_contract_detailDataGridView.CurrentRow) return;

                //foreach (DataGridViewRow dgr in ly_sales_contract_detailDataGridView.Rows)
                //{

                //    if (Color.Red == dgr.Cells["单件折扣"].Style.BackColor)
                //    {
                //        MessageBox.Show("折扣超权限,请修改合同单价,或者请上级审批,提交取消...", "注意");
                //        return;

                //    }

                //}


                string nowplannum = ly_material_plan_mainDataGridView.CurrentRow.Cells["计划编号0"].Value.ToString();


                if ("True" == dgv.CurrentRow.Cells["配套完成0"].Value.ToString())
                {
                    if (SQLDatabase.updateMainplan (nowplannum, "配套完成", "0", SQLDatabase.nowUserName()))
                    {

                        dgv.CurrentRow.Cells["配套完成0"].Value = "False";
                        dgv.CurrentRow.Cells["配套工程师0"].Value = DBNull.Value;
                        dgv.CurrentRow.Cells["回复日期0"].Value = DBNull.Value;
                    }

                }
                else
                {
                    if (SQLDatabase.updateMainplan(nowplannum, "配套完成", "1", SQLDatabase.nowUserName()))
                    {
                        dgv.CurrentRow.Cells["配套完成0"].Value = "True";
                        dgv.CurrentRow.Cells["配套工程师0"].Value = SQLDatabase.nowUserName();
                        dgv.CurrentRow.Cells["回复日期0"].Value = SQLDatabase.GetNowdate();
                    }
                }

                SQLDatabase.dataChangeREC(dgv.CurrentRow.Cells["mainplan_id"].Value.ToString(), "ly_material_plan_main", "UPD", this.Text);

                this.ly_material_plan_mainperiodproBindingSource.EndEdit();

               // this.ly_material_plan_mainperiodproTableAdapter.Update(this.lYSalseMange.ly_material_plan_mainperiodpro);

                //string nowplannum = ly_material_plan_mainDataGridView.CurrentRow.Cells["计划编号0"].Value.ToString();
                this.f_PlanExtend_LSPTTableAdapter.Fill(this.lYSalseMange.f_PlanExtend_LSPT, nowplannum);

                return;

            }
            ///////////////////////////////////////////////////////////////
        }

        private void toolStripButton10_Click(object sender, EventArgs e)
        {
            if (null == this.dataGridView1.CurrentRow) return;

            // int parentId = int.Parse(this.ly_material_plan_mainDataGridView.CurrentRow.Cells["id"].Value.ToString());





            NewFrm.Show(this);


            //this.ly_plan_getmaterialTableAdapter.Fill(this.lYPlanMange.ly_plan_getmaterial, parentId);

            BaseReportView queryForm = new BaseReportView();

            queryForm.Text = "中原精密临时配套表";

            queryForm.Printdata = this.lYSalseMange;

            queryForm.PrintCrystalReport = new LY_PlanTemp2();


            //string selectFormula;

            //selectFormula = "{ly_store_planitemcount.状态}  =   '原料'  and {ly_store_planitemcount.欠料金额}>0 ";
            //queryForm.PrintCrystalReport.DataDefinition.RecordSelectionFormula = selectFormula;

            NewFrm.Hide(this);

            queryForm.ShowDialog();
        }

        private void toolStripButton52_Click(object sender, EventArgs e)
        {
            if (null == this.ly_material_plan_mainDataGridView.CurrentRow) return;

            string nowplannum = ly_material_plan_mainDataGridView.CurrentRow.Cells["计划编号0"].Value.ToString();
           
            this.f_PlanExtend_LSPTTableAdapter.Fill(this.lYSalseMange.f_PlanExtend_LSPT, nowplannum);

            /////////////////////////////////////////////////////////////////////////////////////////
            
            if (null == this.f_PlanExtend_LSPTDataGridView.CurrentRow) return;



            NewFrm.Show(this); ;



            BaseReportView queryForm = new BaseReportView();

            queryForm.Text = "中原精密综合配套";

            queryForm.Printdata = this.lYSalseMange;

            queryForm.PrintCrystalReport = new LY_YingyeHetong_ZHFH();




            NewFrm.Hide(this);

            queryForm.ShowDialog();
        }

        private void toolStripButton9_Click(object sender, EventArgs e)
        {


            string nowclientcode = "111";

            LY_Salescontract_Add1 queryForm = new LY_Salescontract_Add1();

            queryForm.salesclientcode = nowclientcode;
            queryForm.runmode = "增加";


            queryForm.StartPosition = FormStartPosition.CenterParent;
            queryForm.ShowDialog();

            if (queryForm.DialogResult != DialogResult.Cancel)
            {
                this.ly_material_plan_mainperiodproTableAdapter.Fill(this.lYSalseMange.ly_material_plan_mainperiodpro, "LSPT", this.dateTimePicker1.Value, this.dateTimePicker2.Value);

                this.ly_material_plan_mainperiodproBindingSource.Position = this.ly_material_plan_mainperiodproBindingSource.Find("内部编码", queryForm.contractinner_code);


            }
        }

        private void toolStripButton11_Click(object sender, EventArgs e)
        {
            if (null == this.ly_material_plan_mainDataGridView.CurrentRow)
            {

                return;
            }

            string nowcontractinner_code = this.ly_material_plan_mainDataGridView.CurrentRow.Cells["内部编码0"].Value.ToString();

            LY_Salescontract_Add1 queryForm = new LY_Salescontract_Add1();

            queryForm.contractinner_code = nowcontractinner_code;
            queryForm.runmode = "修改";


            queryForm.StartPosition = FormStartPosition.CenterParent;
            queryForm.ShowDialog();

            if (queryForm.DialogResult != DialogResult.Cancel)
            {
                this.ly_material_plan_mainperiodproTableAdapter.Fill(this.lYSalseMange.ly_material_plan_mainperiodpro, "LSPT", this.dateTimePicker1.Value, this.dateTimePicker2.Value);

                this.ly_material_plan_mainperiodproBindingSource.Position = this.ly_material_plan_mainperiodproBindingSource.Find("内部编码", queryForm.contractinner_code);


            }
        }

        private void toolStripButton13_Click(object sender, EventArgs e)
        {
            if (null == ly_material_plan_mainDataGridView.CurrentRow)
            {

                return;
            }

            if ("True" == ly_material_plan_mainDataGridView.CurrentRow.Cells["配套完成0"].Value.ToString())
            {
                MessageBox.Show("依赖书已经技术配套,不能增加配套...", "注意");
                return;

            }

            // this.nowinnerCode;

            LY_Salescontract_GroupDetailAdd queryForm = new LY_Salescontract_GroupDetailAdd();

            string nowplannum = ly_material_plan_mainDataGridView.CurrentRow.Cells["计划编号0"].Value.ToString();

            queryForm.now_plannum = nowplannum;
            queryForm.nowsales_groupcode = "";

            //queryForm.nowclient_receiver = this.ly_sales_contract_main1DataGridView.CurrentRow.Cells["客户联系人"].Value.ToString();
            //queryForm.nowsalesclient_phone = this.ly_sales_contract_main1DataGridView.CurrentRow.Cells["客户电话"].Value.ToString();
            //queryForm.nowpost_code = this.ly_sales_contract_main1DataGridView.CurrentRow.Cells["客户邮编"].Value.ToString();
            //queryForm.nowsalesclient_address = this.ly_sales_contract_main1DataGridView.CurrentRow.Cells["客户地址"].Value.ToString();



            queryForm.runmode = "增加";


            queryForm.StartPosition = FormStartPosition.CenterParent;
            queryForm.ShowDialog();

            if (queryForm.DialogResult != DialogResult.Cancel)
            {
                this.ly_sales_groupTableAdapter.Fill(this.lYSalseMange.ly_sales_group, nowplannum);



                this.ly_sales_groupBindingSource.Position = this.ly_sales_groupBindingSource.Find("配套编码", queryForm.nowsales_groupcode);

                //string nowplannum = ly_material_plan_mainDataGridView.CurrentRow.Cells["计划编号0"].Value.ToString();
                MakeGroupTreeView(nowplannum);


            }
        }

        private void toolStripButton14_Click(object sender, EventArgs e)
        {
            if (null == ly_sales_groupDataGridView.CurrentRow)
            {

                return;
            }

            if ("True" == ly_material_plan_mainDataGridView.CurrentRow.Cells["配套完成0"].Value.ToString())
            {
                MessageBox.Show("依赖书已经技术配套,不能修改数据...", "注意");
                return;

            }

            // this.nowinnerCode;

            LY_Salescontract_GroupDetailAdd queryForm = new LY_Salescontract_GroupDetailAdd();

            string nowplannum = ly_material_plan_mainDataGridView.CurrentRow.Cells["计划编号0"].Value.ToString();
            string nowgroupcode = ly_sales_groupDataGridView.CurrentRow.Cells["配套编码"].Value.ToString();

            queryForm.now_plannum = nowplannum;
            queryForm.nowsales_groupcode = nowgroupcode;




            queryForm.runmode = "修改";


            queryForm.StartPosition = FormStartPosition.CenterParent;
            queryForm.ShowDialog();

            if (queryForm.DialogResult != DialogResult.Cancel)
            {
                this.ly_sales_groupTableAdapter.Fill(this.lYSalseMange.ly_sales_group, nowplannum);



                this.ly_sales_groupBindingSource.Position = this.ly_sales_groupBindingSource.Find("配套编码", queryForm.nowsales_groupcode);

                //string nowplannum = ly_material_plan_mainDataGridView.CurrentRow.Cells["计划编号0"].Value.ToString();
                MakeGroupTreeView(nowplannum);



            }
        }

        private void toolStripButton15_Click(object sender, EventArgs e)
        {
            if (null == ly_sales_groupDataGridView.CurrentRow)
            {

                return;
            }

            if ("True" == ly_material_plan_mainDataGridView.CurrentRow.Cells["配套完成0"].Value.ToString())
            {
                MessageBox.Show("依赖书已经技术配套,不能删除数据...", "注意");
                return;

            }

            //string salespeople = this.ly_sales_groupDataGridView.CurrentRow.Cells["录入人5"].Value.ToString();

            //if (salespeople != SQLDatabase.nowUserName())
            //{
            //    MessageBox.Show("请录入人:" + salespeople + "删除", "注意");
            //    return;
            //}

            //if ("True" == ly_sales_contract_mainDataGridView.CurrentRow.Cells["提交"].Value.ToString())
            //{
            //    MessageBox.Show("合同已经提交,不能删除数据...", "注意");
            //    return;

            //}

            //if ("True" == ly_sales_contract_mainDataGridView.CurrentRow.Cells["批准"].Value.ToString())
            //{
            //    MessageBox.Show("合同已经执行,不能删除数据...", "注意");
            //    return;

            //}

            //if ("True" == ly_sales_contract_mainDataGridView.CurrentRow.Cells["审核"].Value.ToString())
            //{
            //    MessageBox.Show("合同已经审批,不能删除数据...", "注意");
            //    return;

            //}


            string message = "确定删除当前配套吗？";
            string caption = "提示...";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result;



            result = MessageBox.Show(message, caption, buttons,
            MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

            if (result == DialogResult.Yes)
            {




                this.ly_sales_groupBindingSource.RemoveCurrent();


                ly_sales_groupDataGridView.EndEdit();
                ly_sales_groupBindingSource.EndEdit();



                this.ly_sales_groupTableAdapter.Update(this.lYSalseMange.ly_sales_group);

                string nowplannum = ly_material_plan_mainDataGridView.CurrentRow.Cells["计划编号0"].Value.ToString();
                MakeGroupTreeView(nowplannum);



                //this.ly_sales_contract_detailTableAdapter.Fill(this.lYSalseMange.ly_sales_contract_detail, nowinnerCode, 0);

                //if (null != ly_plan_getmaterialDataGridView.CurrentRow)
                //{

                //    string nowitemno = ly_plan_getmaterialDataGridView.CurrentRow.Cells["产品编号5"].Value.ToString();
                //    this.ly_sales_contract_detailBindingSource.Position = this.ly_sales_contract_detailBindingSource.Find("产品编码", nowitemno);
                //}

                //this.ly_sales_contract_main1DataGridView.SelectionChanged -= ly_sales_contract_main1DataGridView_SelectionChanged;
                //this.ly_sales_contract_main1TableAdapter.Fill(this.lYSalseMange.ly_sales_contract_main1, this.nowusercode, "single", this.dateTimePicker1.Value, this.dateTimePicker2.Value);
                //this.ly_sales_contract_main1DataGridView.SelectionChanged += ly_sales_contract_main1DataGridView_SelectionChanged;
            }
        }

        private void 复制当前配套表ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (null == this.ly_material_plan_mainDataGridView.CurrentRow) return;
            
            string message = "确定复制当前配套吗？";
            string caption = "提示...";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result;



            result = MessageBox.Show(message, caption, buttons,
            MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

            if (result == DialogResult.Yes)
            {
                string nowclientcode = "111";

                LY_Salescontract_Add1 queryForm = new LY_Salescontract_Add1();

                queryForm.salesclientcode = nowclientcode;
                queryForm.runmode = "复制";

                string oldPlannum = this.ly_material_plan_mainDataGridView.CurrentRow.Cells["计划编号0"].Value.ToString();


                queryForm.StartPosition = FormStartPosition.CenterParent;
                queryForm.ShowDialog();

                if (queryForm.DialogResult != DialogResult.Cancel)
                {
                    toolStripTextBox5.Text = "";

                    this.ly_material_plan_mainperiodproBindingSource.Filter = "";

                    this.ly_material_plan_mainperiodproTableAdapter.Fill(this.lYSalseMange.ly_material_plan_mainperiodpro, "LSPT", this.dateTimePicker1.Value, this.dateTimePicker2.Value);

                    this.ly_material_plan_mainperiodproBindingSource.Position = this.ly_material_plan_mainperiodproBindingSource.Find("内部编码", queryForm.contractinner_code);

                    string newPlannum = this.ly_material_plan_mainDataGridView.CurrentRow.Cells["计划编号0"].Value.ToString();

                    copyplan(oldPlannum, newPlannum);

                    this.ly_material_plan_mainperiodproTableAdapter.Fill(this.lYSalseMange.ly_material_plan_mainperiodpro, "LSPT", this.dateTimePicker1.Value, this.dateTimePicker2.Value);

                    this.ly_material_plan_mainperiodproBindingSource.Position = this.ly_material_plan_mainperiodproBindingSource.Find("内部编码", queryForm.contractinner_code);

                }
            }
        }


        private void copyplan( string oldplan,string newplan)
        {
            SqlConnection sqlConnection1 = new SqlConnection(SQLDatabase.Connectstring);
            SqlCommand cmd = new SqlCommand();

            string contractinnerCode = "";

            //cmd.Parameters.Add("@Client_mode", SqlDbType.VarChar);
            //cmd.Parameters["@Client_mode"].Value = "YY";

            //cmd.Parameters.Add("@loanId", SqlDbType.Int);
            //cmd.Parameters["@loanId"].Value = loanId;

            cmd.Parameters.Add("@oldplan", SqlDbType.VarChar);
            cmd.Parameters["@oldplan"].Value = oldplan;

            cmd.Parameters.Add("@newplan", SqlDbType.VarChar);
            cmd.Parameters["@newplan"].Value = newplan;


            cmd.CommandText = "LY_CopyPlanDetail_Tec";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = sqlConnection1;

            sqlConnection1.Open();
            cmd.ExecuteNonQuery();
            sqlConnection1.Close();



            //return contractinnerCode;
        
        }

        private void 保存标准配置ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (null == this.treeView2.SelectedNode)
            {
                MessageBox.Show("请选择保存配置项...", "注意");
                return;
            }
            if (this.treeView2.SelectedNode.Level !=2)
            {
                MessageBox.Show("只有成品才可以保存配置...", "注意");
                return;
            }

            //if ("True" == ly_material_plan_mainDataGridView.CurrentRow.Cells["生产审批0"].Value.ToString())
            //{
            //    MessageBox.Show("依赖书已经生产审批,不能修改数据...", "注意");
            //    return;

            //}

            string message = "保存当前配套明细为标准配置吗？";
            string caption = "提示...";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result;



            result = MessageBox.Show(message, caption, buttons,
            MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

            if (result == DialogResult.Yes)
            {

                int nowparent_id = int.Parse(this.treeView2.SelectedNode.Tag.ToString());

                Save_MatingSet(nowparent_id);



                string nowNodeTag1 = this.treeView2.SelectedNode.Tag.ToString();

                string nowmaterialplannum = ly_material_plan_mainDataGridView.CurrentRow.Cells["计划编号0"].Value.ToString();

                MakeGroupTreeView(nowmaterialplannum);

                this.treeView2.SelectedNode = FindGroupNode(this.treeView2.Nodes, nowNodeTag1);

                MessageBox.Show("保存配置成功!", "注意");
            }

           
        }
        private void Save_MatingSet(int itemid)
        {
            SqlConnection sqlConnection1 = new SqlConnection(SQLDatabase.Connectstring);
            SqlCommand cmd = new SqlCommand();



            cmd.Parameters.Add("@NowId", SqlDbType.Int);
            cmd.Parameters["@NowId"].Value = itemid;



            cmd.CommandText = "LY_Save_MatingSet";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = sqlConnection1;

            sqlConnection1.Open();
            cmd.ExecuteNonQuery();
            sqlConnection1.Close();

         

        }

      

        private void 导入标准配置ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (null == this.treeView2.SelectedNode)
            {
                MessageBox.Show("请选择设备...", "注意");
                return;
            }
            if (this.treeView2.SelectedNode.Level != 2)
            {
                MessageBox.Show("只有成品才可以导入标准配置...", "注意");
                return;
            }

            if ("True" == ly_material_plan_mainDataGridView.CurrentRow.Cells["配套完成0"].Value.ToString())
            {
                MessageBox.Show("依赖书已经技术配套,不能修改数据...", "注意");
                return;

            }

            string message = "导入标准配置吗？";
            string caption = "提示...";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result;



            result = MessageBox.Show(message, caption, buttons,
            MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

            if (result == DialogResult.Yes)
            {

                int nowparent_id = int.Parse(this.treeView2.SelectedNode.Tag.ToString());

                Get_MatingSet(nowparent_id);



                string nowNodeTag1 = this.treeView2.SelectedNode.Tag.ToString();

                string nowmaterialplannum = ly_material_plan_mainDataGridView.CurrentRow.Cells["计划编号0"].Value.ToString();

                MakeGroupTreeView(nowmaterialplannum);

                this.treeView2.SelectedNode = FindGroupNode(this.treeView2.Nodes, nowNodeTag1);

                MessageBox.Show("导入标准配置成功!", "注意");
            }
        }

        private void Get_MatingSet(int itemid)
        {
            SqlConnection sqlConnection1 = new SqlConnection(SQLDatabase.Connectstring);
            SqlCommand cmd = new SqlCommand();



            cmd.Parameters.Add("@NowId", SqlDbType.Int);
            cmd.Parameters["@NowId"].Value = itemid;



            cmd.CommandText = "LY_Get_MatingSet";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = sqlConnection1;

            sqlConnection1.Open();
            cmd.ExecuteNonQuery();
            sqlConnection1.Close();



        }

        private void 查看标准配置ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (null == this.treeView2.SelectedNode)
            {
                MessageBox.Show("请选择设备...", "注意");
                return;
            }

         
            if (this.treeView2.SelectedNode.Level != 2)
            {
                MessageBox.Show("只有成品才可以查看标准配置...", "注意");
                return;
            }
            
            LY_Sales_CheckStandardSet queryForm = new LY_Sales_CheckStandardSet();

            //queryForm.OwnerForm = this;

            int nowparent_id = int.Parse(this.treeView2.SelectedNode.Tag.ToString());
            queryForm.nowparentId = nowparent_id;

            queryForm.StartPosition = FormStartPosition.CenterParent;
            queryForm.ShowDialog(this);




            if (queryForm.DialogResult != DialogResult.Cancel)
            {
                //this.ly_inma0010ylTableAdapter.Fill(this.lYMaterialMange.ly_inma0010yl);
                //this.ly_inma0010ylBindingSource.Position = this.ly_inma0010ylBindingSource.Find("物资编号", queryForm.material_code);
            }
        }

        //private void toolStripButton21_Click(object sender, EventArgs e)
        //{
        //    this.ly_sales_businessTableAdapter.Fill(this.lYSalseMange.ly_sales_business, "", "full", this.dateTimePicker3.Value, this.dateTimePicker4.Value);
            
            
        //    //AddSummationRow_New(ly_sales_contract_main1BindingSource, ly_sales_contract_main1DataGridView);
        //}

        private void ly_sales_businessDataGridView_SelectionChanged(object sender, EventArgs e)
        {
            if (null == ly_sales_businessDataGridView.CurrentRow)
            {
                //this.ly_sales_contract_detailTableAdapter.Fill(this.lYSalseMange.ly_sales_contract_detail, "", 0);
                this.ly_material_plan_mainTableAdapter.Fill(this.lYSalseMange.ly_material_plan_main, "SSS", "");
               
                return;
            }

            //this.nowinnerCode = ly_sales_contract_main1DataGridView.CurrentRow.Cells["内部编码1"].Value.ToString();
            string nowclientCode = ly_sales_businessDataGridView.CurrentRow.Cells["客户编码"].Value.ToString();
            this .nowcontractCode  = ly_sales_businessDataGridView.CurrentRow.Cells["业务编码"].Value.ToString();

            // this.ly_sales_contract_detailTableAdapter.Fill(this.lYSalseMange.ly_sales_contract_detail, nowinnerCode, 0);
            // this.dataGridView2.SelectionChanged -= dataGridView2_SelectionChanged;
            //this.ly_material_plan_mainDataGridView.SelectionChanged -= this.ly_material_plan_mainDataGridView_SelectionChanged;
            this.ly_material_plan_mainTableAdapter.Fill(this.lYSalseMange.ly_material_plan_main, "LSPT", nowcontractCode);


            if (null == dataGridView2.CurrentRow)
            {

                this.ly_sales_groupTableAdapter.Fill(this.lYSalseMange.ly_sales_group, "");
                this.f_PlanExtend_LSPTTableAdapter.Fill(this.lYSalseMange.f_PlanExtend_LSPT, "");
                this.ly_plan_getmaterial_departmentTableAdapter.Fill(this.lYSalseMange.ly_plan_getmaterial_department, "");
                this.treeView2.Nodes.Clear();

               
            }
            //this.ly_material_plan_mainDataGridView.SelectionChanged += this.ly_material_plan_mainDataGridView_SelectionChanged;
           // this.dataGridView2.SelectionChanged += dataGridView2_SelectionChanged;

         
        }

        private void dataGridView2_SelectionChanged(object sender, EventArgs e)
        {
            

          
            ////////////////////////////////////////////////

            if (null == dataGridView2.CurrentRow)
            {

                this.ly_sales_groupTableAdapter.Fill(this.lYSalseMange.ly_sales_group, "");
                this.f_PlanExtend_LSPTTableAdapter.Fill(this.lYSalseMange.f_PlanExtend_LSPT, "");
                this.ly_plan_getmaterial_departmentTableAdapter.Fill(this.lYSalseMange.ly_plan_getmaterial_department, "");

                return;
            }

            string nowplannum = dataGridView2.CurrentRow.Cells["计划编号1"].Value.ToString();
            // string nowcontractnum = ly_material_plan_mainDataGridView.CurrentRow.Cells["计划编号"].Value.ToString();


            //this.ly_material_plan_mainDataGridView.SelectionChanged -= this.ly_material_plan_mainDataGridView_SelectionChanged;
            this.ly_material_plan_mainperiodproBindingSource.Position = this.ly_material_plan_mainperiodproBindingSource.Find("计划编号", nowplannum);
            //this.ly_material_plan_mainDataGridView.SelectionChanged += this.ly_material_plan_mainDataGridView_SelectionChanged;

            //this.ly_sales_groupTableAdapter.Fill(this.lYSalseMange.ly_sales_group, nowplannum);
            //this.f_PlanExtend_LSPTTableAdapter.Fill(this.lYSalseMange.f_PlanExtend_LSPT, nowplannum);
            //this.ly_plan_getmaterial_departmentTableAdapter.Fill(this.lYSalseMange.ly_plan_getmaterial_department, nowplannum);

            ////this.nowplanremark = dataGridView2.CurrentRow.Cells["配套要求1"].Value.ToString();

            //MakeGroupTreeView(nowplannum);


        }

        private void tabControl3_Selected(object sender, TabControlEventArgs e)
        {
            //if (0 == e.TabPageIndex)
            //{
            //    if (null == ly_material_plan_mainDataGridView.CurrentRow)
            //    {

            //        this.ly_sales_groupTableAdapter.Fill(this.lYSalseMange.ly_sales_group, "");
            //        this.f_PlanExtend_LSPTTableAdapter.Fill(this.lYSalseMange.f_PlanExtend_LSPT, "");
            //        this.ly_plan_getmaterial_departmentTableAdapter.Fill(this.lYSalseMange.ly_plan_getmaterial_department, "");

            //        return;
            //    }

            //    string nowplannum = ly_material_plan_mainDataGridView.CurrentRow.Cells["计划编号0"].Value.ToString();
            //    // string nowcontractnum = ly_material_plan_mainDataGridView.CurrentRow.Cells["计划编号"].Value.ToString();


            //    this.ly_sales_groupTableAdapter.Fill(this.lYSalseMange.ly_sales_group, nowplannum);
            //    this.f_PlanExtend_LSPTTableAdapter.Fill(this.lYSalseMange.f_PlanExtend_LSPT, nowplannum);
            //    this.ly_plan_getmaterial_departmentTableAdapter.Fill(this.lYSalseMange.ly_plan_getmaterial_department, nowplannum);


            //    MakeGroupTreeView(nowplannum);
            //}
            //else
            //    if (1 == e.TabPageIndex)
            //    {
            //        if (null == dataGridView2.CurrentRow)
            //        {

            //            this.ly_sales_groupTableAdapter.Fill(this.lYSalseMange.ly_sales_group, "");
            //            this.f_PlanExtend_LSPTTableAdapter.Fill(this.lYSalseMange.f_PlanExtend_LSPT, "");
            //            this.ly_plan_getmaterial_departmentTableAdapter.Fill(this.lYSalseMange.ly_plan_getmaterial_department, "");

            //            return;
            //        }

            //        string nowplannum = dataGridView2.CurrentRow.Cells["计划编号1"].Value.ToString();
            //        // string nowcontractnum = ly_material_plan_mainDataGridView.CurrentRow.Cells["计划编号"].Value.ToString();


            //        this.ly_sales_groupTableAdapter.Fill(this.lYSalseMange.ly_sales_group, nowplannum);
            //        this.f_PlanExtend_LSPTTableAdapter.Fill(this.lYSalseMange.f_PlanExtend_LSPT, nowplannum);
            //        this.ly_plan_getmaterial_departmentTableAdapter.Fill(this.lYSalseMange.ly_plan_getmaterial_department, nowplannum);


            //        MakeGroupTreeView(nowplannum);
            //    }
                
        }

        private void toolStripButton21_Click_1(object sender, EventArgs e)
        {
            if (null == ly_sales_businessDataGridView.CurrentRow)
            {


                return;
            }

            string salespeople = this.ly_sales_businessDataGridView.CurrentRow.Cells["录入人B"].Value.ToString();
            if (salespeople != SQLDatabase.nowUserName()  )
            {
                if ("000" != SQLDatabase.NowUserID)
                {
                    MessageBox.Show("请录入人:" + salespeople + "删除", "注意");
                    return;
                }
            }

            if (this.dataGridView2.RowCount > 0)
            {
                MessageBox.Show("已有依赖书记录，不能删除(实需删除，请先删除依赖书记录)", "注意");
                return;

            }

            //if (this.ly_sales_borrowDataGridView.RowCount > 0)
            //{
            //    MessageBox.Show("已有借用记录，不能删除(实需删除，请先删除借用记录)", "注意");
            //    return;

            //}

            //if (this.ly_sales_contract_mainDataGridView.RowCount > 0)
            //{
            //    MessageBox.Show("已有合同发货清单记录，不能删除(实需删除，请先删除合同发货清单记录)", "注意");
            //    return;

            //}

            //if ("True" == ly_sales_businessDataGridView.CurrentRow.Cells["提交1"].Value.ToString())
            //{
            //    MessageBox.Show("当前业务已经提交为合同,不能删除数据...", "注意");
            //    return;

            //}

            //if ("True" == ly_sales_contract_mainDataGridView.CurrentRow.Cells["批准"].Value.ToString())
            //{
            //    MessageBox.Show("合同已经执行,不能删除数据...", "注意");
            //    return;

            //}

            //if ("True" == ly_sales_contract_mainDataGridView.CurrentRow.Cells["审核"].Value.ToString())
            //{
            //    MessageBox.Show("合同已经审批,不能删除数据...", "注意");
            //    return;

            //}
            if ("000" != SQLDatabase.NowUserID)
            {
                return;
            }
            string message = "确定删除当前业务记录吗？";
            string caption = "提示...";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result;



            result = MessageBox.Show(message, caption, buttons,
            MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

            if (result == DialogResult.Yes)
            {

                this.ly_sales_businessBindingSource.RemoveCurrent();


                ly_sales_businessDataGridView.EndEdit();
                ly_sales_businessBindingSource.EndEdit();



                this.ly_sales_businessTableAdapter.Update(this.lYSalseMange.ly_sales_business);

                //this.ly_sales_contract_main1DataGridView.SelectionChanged -= ly_sales_contract_main1DataGridView_SelectionChanged;
                //this.ly_sales_contract_main1TableAdapter.Fill(this.lYSalseMange.ly_sales_contract_main1, this.nowusercode, "single", this.dateTimePicker1.Value, this.dateTimePicker2.Value);
                //this.ly_sales_contract_main1DataGridView.SelectionChanged += ly_sales_contract_main1DataGridView_SelectionChanged;
            }
        }

        private void bindingNavigatorAddNewItem_Click(object sender, EventArgs e)
        {
            if (null == ly_sales_businessDataGridView.CurrentRow)
            {

                return;
            }

            // this.nowinnerCode;

            LY_Salescontract_GroupAdd queryForm = new LY_Salescontract_GroupAdd();

            queryForm.contractinner_code = nowcontractCode;
            queryForm.nowcontractCode = nowcontractCode;
            queryForm.runmode = "增加";


            queryForm.StartPosition = FormStartPosition.CenterParent;
            queryForm.ShowDialog();

            if (queryForm.DialogResult != DialogResult.Cancel)
            {
                this.ly_material_plan_mainTableAdapter.Fill(this.lYSalseMange.ly_material_plan_main, "LSPT", nowcontractCode);



                this.ly_material_plan_mainBindingSource.Position = this.ly_material_plan_mainBindingSource.Find("内部编码", queryForm.now_plannum);


            }
        }

        private void toolStripButton20_Click(object sender, EventArgs e)
        {
            if (null == dataGridView2.CurrentRow)
            {

                return;
            }

            string salespeople = this.dataGridView2.CurrentRow.Cells["录入人1"].Value.ToString();

            if (salespeople != SQLDatabase.nowUserName())
            {
                MessageBox.Show("请录入人:" + salespeople + "修改", "注意");
                return;
            }

            if ("True" == dataGridView2.CurrentRow.Cells["配套完成1"].Value.ToString())
            {
                MessageBox.Show("依赖书已经技术配套,不能修改数据...", "注意");
                return;

            }

            if ("True" == dataGridView2.CurrentRow.Cells["生产审批1"].Value.ToString())
            {
                MessageBox.Show("依赖书已经生产审批,不能修改数据...", "注意");
                return;

            }

            if (!string.IsNullOrEmpty(dataGridView2.CurrentRow.Cells["发货清单1"].Value.ToString()))
            {
                MessageBox.Show("依赖书已经绑定发货清单,不能删除数据...", "注意");
                return;

            }

            // this.nowinnerCode;

            LY_Salescontract_GroupAdd queryForm = new LY_Salescontract_GroupAdd();

            queryForm.contractinner_code = nowcontractCode;
            queryForm.nowcontractCode = nowcontractCode;
            queryForm.now_plannum = this.dataGridView2.CurrentRow.Cells["计划编号1"].Value.ToString();

            queryForm.runmode = "修改";


            queryForm.StartPosition = FormStartPosition.CenterParent;
            queryForm.ShowDialog();

            if (queryForm.DialogResult != DialogResult.Cancel)
            {

                SQLDatabase.dataChangeREC(dataGridView2.CurrentRow.Cells["id_plan"].Value.ToString(), "ly_material_plan_main", "UPD", this.Text);
                
                this.ly_material_plan_mainTableAdapter.Fill(this.lYSalseMange.ly_material_plan_main, "LSPT", nowcontractCode);



                this.ly_material_plan_mainBindingSource.Position = this.ly_material_plan_mainBindingSource.Find("内部编码", queryForm.now_plannum);



            }
        }

        private void bindingNavigatorDeleteItem_Click(object sender, EventArgs e)
        {
            if (null == dataGridView2.CurrentRow)
            {

                return;
            }

            string salespeople = this.dataGridView2.CurrentRow.Cells["录入人1"].Value.ToString();

            if (salespeople != SQLDatabase.nowUserName())
            {
                MessageBox.Show("请录入人:" + salespeople + "删除", "注意");
                return;
            }

            if ("True" == dataGridView2.CurrentRow.Cells["配套完成1"].Value.ToString())
            {
                MessageBox.Show("依赖书已经技术配套,不能删除数据...", "注意");
                return;

            }

            if ("True" == dataGridView2.CurrentRow.Cells["生产审批1"].Value.ToString())
            {
                MessageBox.Show("依赖书已经生产审批,不能删除数据...", "注意");
                return;

            }

            if ("True" == dataGridView2.CurrentRow.Cells["营业执行1"].Value.ToString())
            {
                MessageBox.Show("依赖书经由营业执行,不能删除数据...", "注意");
                return;

            }

            if ( !string .IsNullOrEmpty( dataGridView2.CurrentRow.Cells["发货清单1"].Value.ToString()))
            {
                MessageBox.Show("依赖书已经绑定发货清单,不能删除数据...", "注意");
                return;

            }

            //if ("True" == ly_sales_contract_mainDataGridView.CurrentRow.Cells["批准"].Value.ToString())
            //{
            //    MessageBox.Show("合同已经执行,不能删除数据...", "注意");
            //    return;

            //}

            //if ("True" == ly_sales_contract_mainDataGridView.CurrentRow.Cells["审核"].Value.ToString())
            //{
            //    MessageBox.Show("合同已经审批,不能删除数据...", "注意");
            //    return;

            //}


            string message = "确定删除当前依赖书吗？";
            string caption = "提示...";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result;



            result = MessageBox.Show(message, caption, buttons,
            MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

            if (result == DialogResult.Yes)
            {


                SQLDatabase.dataChangeREC(dataGridView2.CurrentRow.Cells["id_plan"].Value.ToString(), "ly_material_plan_main", "DEL", this.Text);

                this.ly_material_plan_mainBindingSource.RemoveCurrent();


                ly_material_plan_mainDataGridView.EndEdit();
                ly_material_plan_mainBindingSource.EndEdit();



                this.ly_material_plan_mainTableAdapter.Update(this.lYSalseMange.ly_material_plan_main);


                this.ly_material_plan_mainDataGridView.SelectionChanged -= ly_material_plan_mainDataGridView_SelectionChanged;
                this.ly_material_plan_mainperiodproTableAdapter.Fill(this.lYSalseMange.ly_material_plan_mainperiodpro, "LSPT", this.dateTimePicker1.Value, this.dateTimePicker2.Value);
                this.ly_material_plan_mainDataGridView.SelectionChanged += ly_material_plan_mainDataGridView_SelectionChanged;


                //this.ly_sales_contract_main1DataGridView.SelectionChanged -= ly_sales_contract_main1DataGridView_SelectionChanged;
                //this.ly_sales_contract_main1TableAdapter.Fill(this.lYSalseMange.ly_sales_contract_main1, this.nowusercode, "single", this.dateTimePicker1.Value, this.dateTimePicker2.Value);
                //this.ly_sales_contract_main1DataGridView.SelectionChanged += ly_sales_contract_main1DataGridView_SelectionChanged;
            }
        }

        private void toolStripButton12_Click(object sender, EventArgs e)
        {

        }

        private void 保存特殊配置ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (null == this.treeView2.SelectedNode)
            {
                MessageBox.Show("请选择保存配置项...", "注意");
                return;
            }
            if (this.treeView2.SelectedNode.Level != 2)
            {
                MessageBox.Show("只有成品才可以保存配置...", "注意");
                return;
            }


            string message = "保存当前配套明细为特殊配置吗？";
            string caption = "提示...";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result;



            result = MessageBox.Show(message, caption, buttons,
            MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

            if (result == DialogResult.Yes)
            {

                int nowparent_id = int.Parse(this.treeView2.SelectedNode.Tag.ToString());

                Save_SpecialMatingSet(nowparent_id,SQLDatabase.nowUserName());



                //string nowNodeTag1 = this.treeView2.SelectedNode.Tag.ToString();

                //string nowmaterialplannum = ly_material_plan_mainDataGridView.CurrentRow.Cells["计划编号0"].Value.ToString();

                //MakeGroupTreeView(nowmaterialplannum);

                //this.treeView2.SelectedNode = FindGroupNode(this.treeView2.Nodes, nowNodeTag1);

                MessageBox.Show("保存配置成功!", "注意");
            }
        }


        private void Save_SpecialMatingSet(int itemid,string emploename)
        {
            SqlConnection sqlConnection1 = new SqlConnection(SQLDatabase.Connectstring);
            SqlCommand cmd = new SqlCommand();



            cmd.Parameters.Add("@NowId", SqlDbType.Int);
            cmd.Parameters["@NowId"].Value = itemid;

            cmd.Parameters.Add("@Nowemploename", SqlDbType.VarChar);
            cmd.Parameters["@Nowemploename"].Value = emploename;



            cmd.CommandText = "LY_Save_MatingSpecialSet";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = sqlConnection1;

            sqlConnection1.Open();
            cmd.ExecuteNonQuery();
            sqlConnection1.Close();



        }

        private void 导入特殊配置ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (null == this.treeView2.SelectedNode)
            {
                MessageBox.Show("请选择设备...", "注意");
                return;
            }


            if (this.treeView2.SelectedNode.Level != 2)
            {
                MessageBox.Show("只有成品才可以导入特殊配置...", "注意");
                return;
            }

            LY_Sales_CheckSpecialSet queryForm = new LY_Sales_CheckSpecialSet();

            //queryForm.OwnerForm = this;

            int nowparent_id = int.Parse(this.treeView2.SelectedNode.Tag.ToString());
            queryForm.nowparentId = nowparent_id;

            queryForm.StartPosition = FormStartPosition.CenterParent;
            queryForm.ShowDialog(this);




            if (queryForm.DialogResult == DialogResult.Cancel)
            {
               return ;
            }

            ///////////////////////////////////////////////////////////////

            if ("True" == ly_material_plan_mainDataGridView.CurrentRow.Cells["配套完成0"].Value.ToString())
            {
                MessageBox.Show("依赖书已经技术配套,不能修改数据...", "注意");
                return;

            }

            string message = "导入标准配置吗？";
            string caption = "提示...";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result;



            result = MessageBox.Show(message, caption, buttons,
            MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

            if (result == DialogResult.Yes)
            {

                //int nowparent_id = int.Parse(this.treeView2.SelectedNode.Tag.ToString());

                Get_SpecialMatingSet(nowparent_id,queryForm.nowspecialparentId);



                string nowNodeTag1 = this.treeView2.SelectedNode.Tag.ToString();

                string nowmaterialplannum = ly_material_plan_mainDataGridView.CurrentRow.Cells["计划编号0"].Value.ToString();

                MakeGroupTreeView(nowmaterialplannum);

                this.treeView2.SelectedNode = FindGroupNode(this.treeView2.Nodes, nowNodeTag1);

                MessageBox.Show("导入特殊配置成功!", "注意");
            }
        }

        private void Get_SpecialMatingSet(int itemid,int parentid )
        {
            SqlConnection sqlConnection1 = new SqlConnection(SQLDatabase.Connectstring);
            SqlCommand cmd = new SqlCommand();



            cmd.Parameters.Add("@NowId", SqlDbType.Int);
            cmd.Parameters["@NowId"].Value = itemid;

            cmd.Parameters.Add("@parentid", SqlDbType.Int);
            cmd.Parameters["@parentid"].Value = parentid;






            cmd.CommandText = "LY_Get_SpecialMatingSet";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = sqlConnection1;

            sqlConnection1.Open();
            cmd.ExecuteNonQuery();
            sqlConnection1.Close();



        }

        private void 分组内配套表复制ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (null == this.dataGridView2.CurrentRow) return;

            string message = "确定分组内复制当前配套表吗？";
            string caption = "提示...";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result;



            result = MessageBox.Show(message, caption, buttons,
            MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

            if (result == DialogResult.Yes)
            {


                string oldPlannum = this.dataGridView2.CurrentRow.Cells["计划编号1"].Value.ToString();





                NewFrm.Show(this);

                copygroupplan(oldPlannum);


                this.ly_material_plan_mainDataGridView.SelectionChanged -= ly_material_plan_mainDataGridView_SelectionChanged;
                this.ly_material_plan_mainperiodproTableAdapter.Fill(this.lYSalseMange.ly_material_plan_mainperiodpro, "LSPT", this.dateTimePicker1.Value, this.dateTimePicker2.Value);
                this.ly_material_plan_mainDataGridView.SelectionChanged += ly_material_plan_mainDataGridView_SelectionChanged;

                this.ly_material_plan_mainTableAdapter.Fill(this.lYSalseMange.ly_material_plan_main, "LSPT", nowcontractCode);

               

               
                
                NewFrm.Hide(this);

                    //this.ly_material_plan_mainperiodTableAdapter.Fill(this.lYSalseMange.ly_material_plan_mainperiod, "LSPT", this.dateTimePicker1.Value, this.dateTimePicker2.Value);

                    //this.ly_material_plan_mainperiodBindingSource.Position = this.ly_material_plan_mainperiodBindingSource.Find("内部编码", queryForm.contractinner_code);

               
            }
        }

        private void copygroupplan(string oldplan)
        {
            SqlConnection sqlConnection1 = new SqlConnection(SQLDatabase.Connectstring);
            SqlCommand cmd = new SqlCommand();

            string contractinnerCode = "";

            //cmd.Parameters.Add("@Client_mode", SqlDbType.VarChar);
            //cmd.Parameters["@Client_mode"].Value = "YY";

            //cmd.Parameters.Add("@loanId", SqlDbType.Int);
            //cmd.Parameters["@loanId"].Value = loanId;

            cmd.Parameters.Add("@oldplan", SqlDbType.VarChar);
            cmd.Parameters["@oldplan"].Value = oldplan;

            cmd.Parameters.Add("@newoperator", SqlDbType.VarChar);
            cmd.Parameters["@newoperator"].Value = SQLDatabase .nowUserName();


            cmd.CommandText = "LY_CopyPlanDetail_Tec_Group";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = sqlConnection1;

            sqlConnection1.Open();
            cmd.ExecuteNonQuery();
            sqlConnection1.Close();



            //return contractinnerCode;

        }

        

       
       
       

       
    }
}
