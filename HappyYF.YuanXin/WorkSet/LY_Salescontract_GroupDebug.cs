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
using Project_Manager.AppServices;


namespace HappyYF.YuanXin.WorkSet
{
    public partial class LY_Salescontract_GroupDebug : Form
    {

        private string nowusercode = "";



        private string nowinnerCode = "";
        private string nowcontractCode = "";

        public string formState;
        private string saveState;
        int nowRow;



        public LY_Salescontract_GroupDebug()
        {
            InitializeComponent();
        }




        private void Yonghu_Load(object sender, EventArgs e)
        {

            this.ly_sales_groupTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;


            this.ly_plan_getmaterial1TableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            this.f_PlanExtend_LSPTTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;

            this.ly_plan_getmaterial_departmentTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;

            this.ly_sales_testTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            this.ly_sales_test_detail3TableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;


            this.ly_sales_test_detail3BindingSource.Sort = "套号,配调记录";

            this.nowusercode = SQLDatabase.NowUserID;





            this.dateTimePicker1.Text = DateTime.Today.AddMonths(-6).Date.ToString();
            this.dateTimePicker2.Text = DateTime.Today.AddDays(1).Date.ToString();


            this.ly_material_plan_mainperiodBindingSource.Filter = "";
            //this.ly_material_plan_mainperiodBindingSource.Filter = "出库指令=1";
            this.ly_material_plan_mainperiodTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            this.ly_material_plan_mainperiodTableAdapter.Fill(this.lYSalseMange.ly_material_plan_mainperiod, "LSPT", this.dateTimePicker1.Value, this.dateTimePicker2.Value);


            SetFormState("View");

        }






        private TreeNode FindNode(TreeNodeCollection tnParent, string strValue)
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


            //filterString = GetDataGridviewMultiFilterString.DGVMultiFilterString(this.ly_sales_contract_main1DataGridView, this.toolStripTextBox5.Text);


            //this.ly_sales_contract_main1BindingSource.Filter = filterString;
        }

        private void toolStripTextBox5_Enter(object sender, EventArgs e)
        {
            toolStripTextBox5.Text = "";

            //this.ly_sales_contract_main1BindingSource.Filter = "";
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
            // string nowcontractnum = ly_material_plan_mainDataGridView.CurrentRow.Cells["计划编号"].Value.ToString();


            this.ly_sales_groupTableAdapter.Fill(this.lYSalseMange.ly_sales_group, nowplannum);
            this.f_PlanExtend_LSPTTableAdapter.Fill(this.lYSalseMange.f_PlanExtend_LSPT, nowplannum);
            this.ly_plan_getmaterial_departmentTableAdapter.Fill(this.lYSalseMange.ly_plan_getmaterial_department, nowplannum);


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
            if (null == ly_sales_groupDataGridView.CurrentRow)
            {

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

        private void ly_sales_groupDataGridView_SelectionChanged(object sender, EventArgs e)
        {
            if (null == ly_sales_groupDataGridView.CurrentRow)
            {

                this.ly_plan_getmaterial1TableAdapter.Fill(this.lYSalseMange.ly_plan_getmaterial1, "", "", 0);

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



        private void bindingNavigatorDeleteItem1_Click(object sender, EventArgs e)
        {
            if (null == ly_plan_getmaterialDataGridView.CurrentRow)
            {

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




                this.ly_plan_getmaterial1BindingSource.RemoveCurrent();


                ly_plan_getmaterialDataGridView.EndEdit();
                ly_plan_getmaterial1BindingSource.EndEdit();



                this.ly_plan_getmaterial1TableAdapter.Update(this.lYSalseMange.ly_plan_getmaterial1);


                //this.ly_sales_contract_detailTableAdapter.Fill(this.lYSalseMange.ly_sales_contract_detail, nowinnerCode, 0);

                if (null != ly_plan_getmaterialDataGridView.CurrentRow)
                {

                    string nowitemno = ly_plan_getmaterialDataGridView.CurrentRow.Cells["产品编号5"].Value.ToString();
                    //this.ly_sales_contract_detailBindingSource.Position = this.ly_sales_contract_detailBindingSource.Find("产品编码", nowitemno);
                }

                string nowplannum = ly_material_plan_mainDataGridView.CurrentRow.Cells["计划编号0"].Value.ToString();
                string nowNodeTag = this.treeView2.SelectedNode.Tag.ToString();
                MakeGroupTreeView(nowplannum);

                this.treeView2.SelectedNode = FindGroupNode(this.treeView2.Nodes, nowNodeTag);


                //this.ly_sales_contract_main1DataGridView.SelectionChanged -= ly_sales_contract_main1DataGridView_SelectionChanged;
                //this.ly_sales_contract_main1TableAdapter.Fill(this.lYSalseMange.ly_sales_contract_main1, this.nowusercode, "single", this.dateTimePicker1.Value, this.dateTimePicker2.Value);
                //this.ly_sales_contract_main1DataGridView.SelectionChanged += ly_sales_contract_main1DataGridView_SelectionChanged;
            }
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            if (null == ly_sales_groupDataGridView.CurrentRow)
            {

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

        private void ly_plan_getmaterialDataGridView_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            DataGridView dgv = sender as DataGridView;

            if (null == dgv.CurrentRow) return;







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

            /////////////////////////////////////////////////////////////////
            //if ("数量5" == dgv.CurrentCell.OwningColumn.Name)
            //{

            //    //if ("True" == dgv.CurrentRow.Cells["审批"].Value.ToString())
            //    //{

            //    //    MessageBox.Show("合同已经审批,不能修改数据...", "注意");
            //    //    return;
            //    //}

            //    ChangeValue queryForm = new ChangeValue();

            //    queryForm.OldValue = dgv.CurrentCell.Value.ToString();
            //    queryForm.NewValue = "";
            //    queryForm.ChangeMode = "value";
            //    queryForm.ShowDialog();




            //    if (queryForm.NewValue != "")


            //    {

            //        //decimal allabsqty;
            //        //if ("" != this.ly_sales_contract_detailDataGridView.CurrentRow.Cells["数量"].Value.ToString())
            //        //{
            //        //    allabsqty = decimal.Parse(ly_sales_contract_detailDataGridView.CurrentRow.Cells["数量"].Value.ToString());
            //        //}
            //        //else
            //        //{
            //        //    allabsqty = 0;
            //        //}

            //        //decimal haveabsqty;
            //        //if ("" != this.ly_sales_contract_detailDataGridView.CurrentRow.Cells["已配套"].Value.ToString())
            //        //{
            //        //    haveabsqty = decimal.Parse(ly_sales_contract_detailDataGridView.CurrentRow.Cells["已配套"].Value.ToString());
            //        //}
            //        //else
            //        //{
            //        //    haveabsqty = 0;
            //        //}



            //        //decimal nowabsqty;
            //        //if ("" != this.ly_sales_contract_detailDataGridView.CurrentRow.Cells["未配套"].Value.ToString())
            //        //{
            //        //    nowabsqty = decimal.Parse(ly_sales_contract_detailDataGridView.CurrentRow.Cells["未配套"].Value.ToString());
            //        //}
            //        //else
            //        //{
            //        //    nowabsqty = 0;
            //        //}

            //        //if ((allabsqty - (haveabsqty - decimal.Parse(queryForm.OldValue) + decimal.Parse(queryForm.NewValue))) < 0)

            //        ////if (nowabsqty == 0)
            //        //{

            //        //    MessageBox.Show("数量不合适,操作取消...", "注意");
            //        //    return;
            //        //}

            //        dgv.CurrentRow.Cells["数量5"].Value = queryForm.NewValue;
            //        //dgv.CurrentRow.Cells["discount_money"].Value = DBNull.Value;

            //        //dgv.CurrentRow.Cells["approve_flag"].Value = "False";

            //        this.ly_plan_getmaterialBindingSource.EndEdit();
            //        this.ly_plan_getmaterialTableAdapter.Update(this.lYSalseMange.ly_plan_getmaterial);

            //        //this.ly_sales_contract_detailTableAdapter.Fill(this.lYSalseMange.ly_sales_contract_detail, nowinnerCode, 0);

            //        string nowplannum = ly_material_plan_mainDataGridView.CurrentRow.Cells["计划编号0"].Value.ToString();
            //        string nowNodeTag = this.treeView2.SelectedNode.Tag.ToString();
            //        MakeGroupTreeView(nowplannum);



            //        this.treeView2.SelectedNode = FindGroupNode(this.treeView2.Nodes, nowNodeTag);

            //        //CountPlanStru();

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



            /////////////////////////////////////////////////////////////////

            //if ("设备要求5" == dgv.CurrentCell.OwningColumn.Name)
            //{

            //    ChangeValue queryForm = new ChangeValue();

            //    queryForm.OldValue = dgv.CurrentCell.Value.ToString();
            //    queryForm.NewValue = "";
            //    queryForm.ChangeMode = "longstring";
            //    queryForm.ShowDialog();




            //    if (queryForm.NewValue != "")
            //    {
            //        dgv.CurrentRow.Cells["设备要求5"].Value = queryForm.NewValue;
            //        //dgv.CurrentRow.Cells["discount_money"].Value = DBNull.Value;

            //        //dgv.CurrentRow.Cells["approve_flag"].Value = "False";
            //        this.ly_plan_getmaterialBindingSource.EndEdit();
            //        this.ly_plan_getmaterialTableAdapter.Update(this.lYSalseMange.ly_plan_getmaterial);

            //        //this.ly_sales_contract_detailTableAdapter.Fill(this.lYSalseMange.ly_sales_contract_detail, nowinnerCode, 0);

            //        //CountPlanStru();

            //    }
            //    else
            //    {

            //    }
            //    return;

            //}

            //////////////////////////////////////

            /////////////////////////////////////////////////////////////////

            //if ("产品编号5" == dgv.CurrentCell.OwningColumn.Name || "产品型号5" == dgv.CurrentCell.OwningColumn.Name || "产品名称5" == dgv.CurrentCell.OwningColumn.Name)
            //{

            //    LY_Salescontract_ItemChange_Sel queryForm = new LY_Salescontract_ItemChange_Sel();


            //    queryForm.ShowDialog();


            //    if (queryForm.nowitemno != "")
            //    {
            //        dgv.CurrentRow.Cells["产品编号5"].Value = queryForm.nowitemno;
            //        dgv.CurrentRow.Cells["产品型号5"].Value = queryForm.nowitemxh;
            //        dgv.CurrentRow.Cells["产品名称5"].Value = queryForm.nowitemname;

            //        //dgv.CurrentRow.Cells["discount_money"].Value = DBNull.Value;

            //        //dgv.CurrentRow.Cells["approve_flag"].Value = "False";
            //        this.ly_plan_getmaterial1BindingSource.EndEdit();
            //        this.ly_plan_getmaterial1TableAdapter.Update(this.lYSalseMange.ly_plan_getmaterial1);

            //        //this.ly_sales_contract_detailTableAdapter.Fill(this.lYSalseMange.ly_sales_contract_detail, nowinnerCode, 0);

            //        //CountPlanStru();

            //    }
            //    else
            //    {

            //    }
            //    return;

            //}

            //////////////////////////////////////


        }

        private void toolStripButton28_Click(object sender, EventArgs e)
        {
            this.ly_material_plan_mainperiodTableAdapter.Fill(this.lYSalseMange.ly_material_plan_mainperiod, "LSPT", this.dateTimePicker1.Value, this.dateTimePicker2.Value);
        }

        private void treeView2_AfterSelect(object sender, TreeViewEventArgs e)
        {


            if (e.Node.Level < 1)
            {
                this.ly_plan_getmaterial1TableAdapter.Fill(this.lYSalseMange.ly_plan_getmaterial1, "", "", 0);
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




            string nowplannum = ly_material_plan_mainDataGridView.CurrentRow.Cells["计划编号0"].Value.ToString();
            string nowcontractnum = ly_material_plan_mainDataGridView.CurrentRow.Cells["计划编号0"].Value.ToString();

            string nowgroupnum = ly_sales_groupDataGridView.CurrentRow.Cells["配套编码"].Value.ToString();


            int nowparentid = int.Parse(e.Node.Tag.ToString());

            if (e.Node.Level == 1)
            {
                this.ly_plan_getmaterial1TableAdapter.Fill(this.lYSalseMange.ly_plan_getmaterial1, nowplannum, nowgroupnum, nowparentid);


                this.ly_sales_testTableAdapter.Fill(this.lYSalseMange.ly_sales_test, 0 - nowparentid);

                //this.ly_sales_test_detailTableAdapter.Fill(this.lYSalseMange.ly_sales_test_detail, new System.Nullable<int>(((int)(System.Convert.ChangeType(record_idToolStripTextBox.Text, typeof(int))))));

            }


            if (e.Node.Level > 1)
            {
                this.f_PlanExtend_LSPTBindingSource.Position = this.f_PlanExtend_LSPTBindingSource.Find("real_id", nowparentid);
            }
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

            this.ly_plan_getmaterial1TableAdapter.Fill(this.lYSalseMange.ly_plan_getmaterial1, nowplannum, nowgroupnum, int.Parse(nowNodeTag));

            treeView2.AfterSelect += treeView2_AfterSelect;
        }

        private void ly_material_plan_mainDataGridView_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            DataGridView dgv = sender as DataGridView;

            if (null == dgv.CurrentRow) return;

            if ("True" == ly_material_plan_mainDataGridView.CurrentRow.Cells["营业发货0"].Value.ToString())
            {
                MessageBox.Show("依赖书已经发货,不能修改数据...", "注意");
                return;

            }

            ///////////////////////////////////////////////////////////
            if ("质检配调0" == dgv.CurrentCell.OwningColumn.Name)
            {


                string nowplannum = dgv.CurrentRow.Cells["计划编号0"].Value.ToString();


                if ("True" == dgv.CurrentRow.Cells["质检配调0"].Value.ToString())
                {

                    if (SQLDatabase.updateMainplan(nowplannum, "质检配调", "0", DBNull.Value.ToString()))
                    {
                        dgv.CurrentRow.Cells["质检配调0"].Value = "False";
                        dgv.CurrentRow.Cells["配调工程师"].Value = DBNull.Value;
                        dgv.CurrentRow.Cells["配调日期6"].Value = DBNull.Value;
                    }

                }
                else
                {
                    if (SQLDatabase.updateMainplan(nowplannum, "质检配调", "1", SQLDatabase.nowUserName()))
                    {
                        dgv.CurrentRow.Cells["质检配调0"].Value = "True";
                        dgv.CurrentRow.Cells["配调工程师"].Value = SQLDatabase.nowUserName();
                        dgv.CurrentRow.Cells["配调日期6"].Value = SQLDatabase.GetNowdate().Date;
                    }
                }

                SQLDatabase.dataChangeREC(dgv.CurrentRow.Cells["mainplan_id"].Value.ToString(), "ly_material_plan_main", "UPD", this.Text);

                this.ly_material_plan_mainperiodBindingSource.EndEdit();
                //this.ly_material_plan_mainperiodTableAdapter.Update(this.lYSalseMange.ly_material_plan_mainperiod);



                return;

            }
            ///////////////////////////////////////////////////////////////

            ///////////////////////////////////////////////////////////
            if ("配调日期6" == dgv.CurrentCell.OwningColumn.Name)
            {


                string nowplannum = dgv.CurrentRow.Cells["计划编号0"].Value.ToString();


                ChangeValue queryForm = new ChangeValue();

                queryForm.OldValue = dgv.CurrentCell.Value.ToString();
                queryForm.NewValue = "";
                queryForm.ChangeMode = "datetime";
                queryForm.ShowDialog();


                if (queryForm.NewValue != "")
                {

                    if (SQLDatabase.updateMainplan(nowplannum, "配调日期", queryForm.NewValue, SQLDatabase.nowUserName()))
                    {
                        dgv.CurrentRow.Cells["配调日期6"].Value = queryForm.NewValue;
                    }


                }
                else
                {
                    if (SQLDatabase.updateMainplan(nowplannum, "配调日期", "null", SQLDatabase.nowUserName()))
                    {
                        dgv.CurrentRow.Cells["配调日期6"].Value = DBNull.Value;
                    }


                }



                this.ly_material_plan_mainperiodBindingSource.EndEdit();
                //this.ly_material_plan_mainperiodTableAdapter.Update(this.lYSalseMange.ly_material_plan_mainperiod);



                return;

            }
            ///////////////////////////////////////////////////////////////
            /////////////////////////////////////////////////////////////
            //if ("生产审批0" == dgv.CurrentCell.OwningColumn.Name)
            //{
            //    //if (null == ly_sales_contract_detailDataGridView.CurrentRow) return;

            //    //foreach (DataGridViewRow dgr in ly_sales_contract_detailDataGridView.Rows)
            //    //{

            //    //    if (Color.Red == dgr.Cells["单件折扣"].Style.BackColor)
            //    //    {
            //    //        MessageBox.Show("折扣超权限,请修改合同单价,或者请上级审批,提交取消...", "注意");
            //    //        return;

            //    //    }

            //    //}





            //    if ("True" == dgv.CurrentRow.Cells["生产审批0"].Value.ToString())
            //    {
            //        dgv.CurrentRow.Cells["生产审批0"].Value = "False";

            //    }
            //    else
            //    {

            //        dgv.CurrentRow.Cells["生产审批0"].Value = "True";
            //    }



            //    this.ly_material_plan_mainperiodBindingSource.EndEdit();
            //    this.ly_material_plan_mainperiodTableAdapter.Update(this.lYSalseMange.ly_material_plan_mainperiod);



            //    return;

            //}
            /////////////////////////////////////////////////////////////////
        }

        private string GetMaxRecordNum()
        {
            SqlConnection sqlConnection1 = new SqlConnection(SQLDatabase.Connectstring);
            SqlCommand cmd = new SqlCommand();

            string MaxProductionorder = "";

            cmd.Parameters.Add("@record_mode", SqlDbType.VarChar);
            cmd.Parameters["@record_mode"].Value = "PT";


            cmd.CommandText = "LY_GetMax_PT_recordNum";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = sqlConnection1;

            sqlConnection1.Open();
            MaxProductionorder = cmd.ExecuteScalar().ToString();
            sqlConnection1.Close();



            return MaxProductionorder;
        }

        private void 增加配调检验记录ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void 增加检验记录明细ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (null == ly_sales_testDataGridView.CurrentRow) return;

            string message = "增加检验记录明细吗？";
            string caption = "提示...";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;

            DialogResult result;



            result = MessageBox.Show(message, caption, buttons,
            MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

            if (result == DialogResult.Yes)
            {
                this.ly_sales_test_detail3BindingSource.AddNew();

                //this.ly_sales_test_detailDataGridView.CurrentRow.Cells["配调记录"].Value = GetMaxRecordNum();

                //this.ly_sales_testDataGridView.CurrentRow.Cells["配调日期0"].Value = SQLDatabase.GetNowdate().ToString(); ;

                //this.ly_sales_testDataGridView.CurrentRow.Cells["配调人"].Value = SQLDatabase.nowUserName();

                int parentId = int.Parse(this.ly_sales_testDataGridView.CurrentRow.Cells["record_id"].Value.ToString());
                this.ly_sales_test_detailDataGridView.CurrentRow.Cells["record_id1"].Value = parentId;

                this.ly_sales_test_detail3BindingSource.EndEdit();
                this.ly_sales_test_detail3TableAdapter.Update(this.lYSalseMange.ly_sales_test_detail3);
            }
        }

        private void ly_sales_testDataGridView_SelectionChanged(object sender, EventArgs e)
        {
            return;

            if (null == ly_sales_testDataGridView.CurrentRow)
            {
                this.ly_sales_test_detail3TableAdapter.Fill(this.lYSalseMange.ly_sales_test_detail3, -1);
                return;
            }

            int parentId = int.Parse(this.ly_sales_testDataGridView.CurrentRow.Cells["record_id"].Value.ToString());

            this.ly_sales_test_detail3TableAdapter.Fill(this.lYSalseMange.ly_sales_test_detail3, parentId);


        }

        private void 删除记录明细ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (null == this.ly_sales_test_detailDataGridView.CurrentRow) return;

            //if (this.ly_material_plan_detailDataGridView.RowCount > 0)
            //{
            //    MessageBox.Show("计划已有物料记录，不能删除(实需删除，请先删除该计划的物料记录)", "注意");
            //    return;

            //}

            string diaodu = this.ly_sales_testDataGridView.CurrentRow.Cells["配调人"].Value.ToString();

            if (diaodu != SQLDatabase.nowUserName())
            {
                MessageBox.Show("请配调人:" + diaodu + "删除", "注意");
                return;
            }

            if ("True" == ly_material_plan_mainDataGridView.CurrentRow.Cells["营业发货0"].Value.ToString())
            {
                MessageBox.Show("依赖书已经发货,不能删除数据...", "注意");
                return;

            }

            //if (ly_production_order_detailDataGridView.RowCount > 0)
            //{
            //    MessageBox.Show("跟单已有工序安排,删除所有工序安排后才能删除跟单...", "注意");
            //    return;

            //}

            //string nowproductionorder = this.ly_production_orderDataGridView.CurrentRow.Cells["跟单编号"].Value.ToString();


            string message1 = "当前(记录明细)将被删除，继续吗？";
            string caption1 = "提示...";
            MessageBoxButtons buttons1 = MessageBoxButtons.YesNo;

            DialogResult result1;



            result1 = MessageBox.Show(message1, caption1, buttons1,
            MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

            if (result1 == DialogResult.Yes)
            {

                this.ly_sales_test_detail3BindingSource.RemoveCurrent();

                this.ly_sales_test_detail3BindingSource.EndEdit();
                this.ly_sales_test_detail3TableAdapter.Update(this.lYSalseMange.ly_sales_test_detail3);

                int nowgroupid = int.Parse(ly_sales_groupDataGridView.CurrentRow.Cells["group_id"].Value.ToString());

                string nowplannum = ly_material_plan_mainDataGridView.CurrentRow.Cells["计划编号0"].Value.ToString();
                string nowcontractnum = ly_material_plan_mainDataGridView.CurrentRow.Cells["计划编号0"].Value.ToString();
                string nowgroupnum = ly_sales_groupDataGridView.CurrentRow.Cells["配套编码"].Value.ToString();

                this.ly_plan_getmaterial1TableAdapter.Fill(this.lYSalseMange.ly_plan_getmaterial1, nowplannum, nowgroupnum, 0 - nowgroupid);





            }
        }

        private void 删除ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (null == this.ly_sales_test_detailDataGridView.CurrentRow) return;

            //if (this.ly_material_plan_detailDataGridView.RowCount > 0)
            //{
            //    MessageBox.Show("计划已有物料记录，不能删除(实需删除，请先删除该计划的物料记录)", "注意");
            //    return;

            //}

            string diaodu = this.ly_sales_test_detailDataGridView.CurrentRow.Cells["配调人1"].Value.ToString();

            if (diaodu != SQLDatabase.nowUserName())
            {
                MessageBox.Show("请配调人:" + diaodu + "删除", "注意");
                return;
            }

            if ("True" == ly_material_plan_mainDataGridView.CurrentRow.Cells["营业发货0"].Value.ToString())
            {
                MessageBox.Show("依赖书已经发货,不能删除数据...", "注意");
                return;

            }
            if ("True" == ly_material_plan_mainDataGridView.CurrentRow.Cells["质检配调0"].Value.ToString())
            {
                MessageBox.Show("配调数据已经提交,不能修改数据...", "注意");
                return;

            }
            //if (ly_production_order_detailDataGridView.RowCount > 0)
            //{
            //    MessageBox.Show("跟单已有工序安排,删除所有工序安排后才能删除跟单...", "注意");
            //    return;

            //}

            //string nowproductionorder = this.ly_production_orderDataGridView.CurrentRow.Cells["跟单编号"].Value.ToString();


            string message1 = "当前(配调记录)将被删除，继续吗？";
            string caption1 = "提示...";
            MessageBoxButtons buttons1 = MessageBoxButtons.YesNo;

            DialogResult result1;



            result1 = MessageBox.Show(message1, caption1, buttons1,
            MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

            if (result1 == DialogResult.Yes)
            {




                int record_Id = int.Parse(ly_sales_test_detailDataGridView.CurrentRow.Cells["record_id1"].Value.ToString());



                //string insstr = " delete ly_material_plan_main  where material_plan_num = '" + nowPlanNumber + "'";

                string delstr = " delete ly_sales_test  " +
                           " where  id=" + record_Id.ToString();


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


                        this.ly_sales_test_detail3BindingSource.RemoveCurrent();





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

                ////////////////////////////////////
                int nowgroupid = int.Parse(ly_sales_groupDataGridView.CurrentRow.Cells["group_id"].Value.ToString());

                string nowplannum = ly_material_plan_mainDataGridView.CurrentRow.Cells["计划编号0"].Value.ToString();

                string nowgroupnum = ly_sales_groupDataGridView.CurrentRow.Cells["配套编码"].Value.ToString();





                this.ly_plan_getmaterial1TableAdapter.Fill(this.lYSalseMange.ly_plan_getmaterial1, nowplannum, nowgroupnum, 0 - nowgroupid);

                this.ly_sales_test_detail3TableAdapter.Fill(this.lYSalseMange.ly_sales_test_detail3, nowgroupid);

                //////////////////////////////////////


            }
        }

        private void ly_sales_groupDataGridView_RowEnter(object sender, DataGridViewCellEventArgs e)
        {


            int nowgroupid = int.Parse(ly_sales_groupDataGridView.Rows[e.RowIndex].Cells["group_id"].Value.ToString());

            string nowplannum = ly_material_plan_mainDataGridView.CurrentRow.Cells["计划编号0"].Value.ToString();
            string nowcontractnum = ly_material_plan_mainDataGridView.CurrentRow.Cells["计划编号0"].Value.ToString();

            if (null != ly_sales_groupDataGridView.CurrentRow)
            {
                string nowgroupnum = ly_sales_groupDataGridView.CurrentRow.Cells["配套编码"].Value.ToString();


                this.ly_plan_getmaterial1TableAdapter.Fill(this.lYSalseMange.ly_plan_getmaterial1, nowplannum, nowgroupnum, 0 - nowgroupid);
            }

            //this.ly_sales_testTableAdapter.Fill(this.lYSalseMange.ly_sales_test, nowgroupid);
            this.ly_sales_test_detail3TableAdapter.Fill(this.lYSalseMange.ly_sales_test_detail3, nowgroupid);


        }

        private void ly_plan_getmaterialDataGridView_DoubleClick(object sender, EventArgs e)
        {
            if (null == ly_plan_getmaterialDataGridView.CurrentRow) return;
            if (null == ly_sales_test_detailDataGridView.CurrentRow) return;

            if ("True" == ly_material_plan_mainDataGridView.CurrentRow.Cells["质检配调0"].Value.ToString())
            {
                MessageBox.Show("配调数据已经提交,不能修改数据...", "注意");
                return;

            }

            int nowgroupid = int.Parse(ly_sales_groupDataGridView.CurrentRow.Cells["group_id"].Value.ToString());

            string nowplannum = ly_material_plan_mainDataGridView.CurrentRow.Cells["计划编号0"].Value.ToString();
            string nowcontractnum = ly_material_plan_mainDataGridView.CurrentRow.Cells["计划编号0"].Value.ToString();
            string nowgroupnum = ly_sales_groupDataGridView.CurrentRow.Cells["配套编码"].Value.ToString();


            string nowitemno = ly_plan_getmaterialDataGridView.CurrentRow.Cells["发货编码"].Value.ToString();
            string nowitemstyle = ly_plan_getmaterialDataGridView.CurrentRow.Cells["产品大类"].Value.ToString();

            string nowdebugCode = ly_sales_test_detailDataGridView.CurrentRow.Cells["配调记录1"].Value.ToString();


            decimal nownotabsqty;
            if ("" != this.ly_plan_getmaterialDataGridView.CurrentRow.Cells["未调"].Value.ToString())
            {
                nownotabsqty = decimal.Parse(ly_plan_getmaterialDataGridView.CurrentRow.Cells["未调"].Value.ToString());
            }
            else
            {
                nownotabsqty = 0;
            }

            if (nownotabsqty == 0)
            {

                MessageBox.Show("该产品已全部配调检验,操作取消...", "注意");
                return;
            }



            if ("01控制部" == nowitemstyle)
            {
                this.ly_sales_test_detailDataGridView.CurrentRow.Cells["控制仪物料编码"].Value = nowitemno;
            }

            if ("02测量部" == nowitemstyle)
            {
                this.ly_sales_test_detailDataGridView.CurrentRow.Cells["装置物料编码"].Value = nowitemno;
            }
            if ("03驱动部" == nowitemstyle)
            {
                this.ly_sales_test_detailDataGridView.CurrentRow.Cells["驱动物料编码"].Value = nowitemno;
            }


            this.ly_sales_test_detail3BindingSource.EndEdit();

            this.ly_sales_test_detail3TableAdapter.Update(this.lYSalseMange.ly_sales_test_detail3);

            this.ly_plan_getmaterial1TableAdapter.Fill(this.lYSalseMange.ly_plan_getmaterial1, nowplannum, nowgroupnum, 0 - nowgroupid);
            this.ly_plan_getmaterial1BindingSource.Position = this.ly_plan_getmaterial1BindingSource.Find("发货编码", nowitemno);


            this.ly_sales_test_detail3TableAdapter.Fill(this.lYSalseMange.ly_sales_test_detail3, nowgroupid);

            this.ly_sales_test_detail3BindingSource.Position = this.ly_sales_test_detail3BindingSource.Find("配调记录", nowdebugCode);



        }

        private void ly_sales_testDataGridView_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            DataGridView dgv = sender as DataGridView;

            if (null == dgv.CurrentRow) return;

            ////////////////////////////////////////////////////////////////////////


            if ("配调日期0" == dgv.CurrentCell.OwningColumn.Name)
            {

                ChangeValue queryForm = new ChangeValue();

                queryForm.OldValue = dgv.CurrentCell.Value.ToString();
                queryForm.NewValue = "";
                queryForm.ChangeMode = "datetime";
                queryForm.ShowDialog();




                if (queryForm.NewValue != "")
                {
                    dgv.CurrentRow.Cells["配调日期0"].Value = queryForm.NewValue;
                    //dgv.CurrentRow.Cells["discount_money"].Value = DBNull.Value;

                    //dgv.CurrentRow.Cells["approve_flag"].Value = "False";
                    //this.ly_sales_testBindingSource.EndEdit();
                    //this.ly_sales_testTableAdapter.Update(this.lYSalseMange.ly_sales_test);

                    ////////////////////////////////////////////////////////





                    int record_Id = int.Parse(dgv.CurrentRow.Cells["record_id1"].Value.ToString());



                    //string insstr = " delete ly_material_plan_main  where material_plan_num = '" + nowPlanNumber + "'";

                    string updstr = " update ly_sales_test  " +
                                "  set test_date=  '" + queryForm.NewValue + "' where  id=" + record_Id.ToString();


                    SqlConnection sqlConnection1 = new SqlConnection(SQLDatabase.Connectstring);
                    SqlCommand cmd = new SqlCommand();

                    cmd.CommandText = updstr;
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



                    /////////////////////////////////////////////////////////


                    //CountPlanStru();

                }
                else
                {


                }
                return;

            }

            /////////////////////////////

        }

        private void ly_sales_test_detailDataGridView_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {

            if (null == this.ly_sales_test_detailDataGridView.CurrentRow) return;

            //if (this.ly_material_plan_detailDataGridView.RowCount > 0)
            //{
            //    MessageBox.Show("计划已有物料记录，不能删除(实需删除，请先删除该计划的物料记录)", "注意");
            //    return;

            //}

            string diaodu = this.ly_sales_test_detailDataGridView.CurrentRow.Cells["配调人1"].Value.ToString();

            if (!string.IsNullOrEmpty(diaodu) && diaodu != SQLDatabase.nowUserName())
            {
                MessageBox.Show("请配调人:" + diaodu + "修改", "注意");
                return;
            }





            DataGridView dgv = sender as DataGridView;

            if (null == dgv.CurrentRow) return;

            /////////////////////////////
            if ("配调日期1" == dgv.CurrentCell.OwningColumn.Name)
            {

                ChangeValue queryForm = new ChangeValue();

                queryForm.OldValue = dgv.CurrentCell.Value.ToString();
                queryForm.NewValue = "";
                queryForm.ChangeMode = "datetime";
                queryForm.ShowDialog();




                if (queryForm.NewValue != "")
                {
                    dgv.CurrentRow.Cells["配调日期1"].Value = queryForm.NewValue;
                    //dgv.CurrentRow.Cells["discount_money"].Value = DBNull.Value;

                    //dgv.CurrentRow.Cells["approve_flag"].Value = "False";
                    //this.ly_sales_testBindingSource.EndEdit();
                    //this.ly_sales_testTableAdapter.Update(this.lYSalseMange.ly_sales_test);
                    ////////////////////////////////////////////////////////





                    int record_Id = int.Parse(dgv.CurrentRow.Cells["record_id1"].Value.ToString());



                    //string insstr = " delete ly_material_plan_main  where material_plan_num = '" + nowPlanNumber + "'";

                    string updstr = " update ly_sales_test  " +
                                "  set test_date=  '" + queryForm.NewValue + "' where  id=" + record_Id.ToString();


                    SqlConnection sqlConnection1 = new SqlConnection(SQLDatabase.Connectstring);
                    SqlCommand cmd = new SqlCommand();

                    cmd.CommandText = updstr;
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = sqlConnection1;

                    int temp = 0;

                    using (TransactionScope scope = new TransactionScope())
                    {

                        sqlConnection1.Open();
                        try
                        {

                            cmd.ExecuteNonQuery();

                            SQLDatabase.dataChangeREC(this.ly_sales_testDataGridView.CurrentRow.Cells["record_id"].Value.ToString(), "ly_sales_test", "UPD", this.Text);
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



                    /////////////////////////////////////////////////////////


                    //CountPlanStru();

                }
                else
                {


                }
                return;

            }
            /////////////////////////////
            if ("套号" == dgv.CurrentCell.OwningColumn.Name)
            {

                ChangeValue queryForm = new ChangeValue();

                queryForm.OldValue = dgv.CurrentCell.Value.ToString();
                queryForm.NewValue = "";
                queryForm.ChangeMode = "decimal";
                queryForm.ShowDialog();




                if (queryForm.NewValue != "")
                {
                    dgv.CurrentRow.Cells["套号"].Value = queryForm.NewValue;
                    //dgv.CurrentRow.Cells["discount_money"].Value = DBNull.Value;

                    //dgv.CurrentRow.Cells["approve_flag"].Value = "False";
                    //this.ly_sales_testBindingSource.EndEdit();
                    //this.ly_sales_testTableAdapter.Update(this.lYSalseMange.ly_sales_test);
                    ////////////////////////////////////////////////////////





                    int record_Id = int.Parse(dgv.CurrentRow.Cells["record_id1"].Value.ToString());



                    //string insstr = " delete ly_material_plan_main  where material_plan_num = '" + nowPlanNumber + "'";

                    string updstr = " update ly_sales_test  " +
                                "  set set_num=  " + queryForm.NewValue + " where  id=" + record_Id.ToString();


                    SqlConnection sqlConnection1 = new SqlConnection(SQLDatabase.Connectstring);
                    SqlCommand cmd = new SqlCommand();

                    cmd.CommandText = updstr;
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = sqlConnection1;

                    int temp = 0;

                    using (TransactionScope scope = new TransactionScope())
                    {

                        sqlConnection1.Open();
                        try
                        {

                            cmd.ExecuteNonQuery();

                            /////SQLDatabase.dataChangeREC(this.ly_sales_testDataGridView.CurrentRow.Cells["record_id"].Value.ToString(), "ly_sales_test", "UPD", this.Text);
                            scope.Complete();



                        }
                        catch (SqlException sqle)
                        {


                            MessageBox.Show(sqle.Message.Split('*')[0]);
                        }


                        finally
                        {
                            sqlConnection1.Close();

                            this.ly_sales_test_detail3BindingSource.Sort = "套号,配调记录";


                        }
                    }



                    /////////////////////////////////////////////////////////


                    //CountPlanStru();

                }
                else
                {


                }
                return;

            }

            /////////////////////////////
            if ("备注" == dgv.CurrentCell.OwningColumn.Name)
            {

                ChangeValue queryForm = new ChangeValue();

                queryForm.OldValue = dgv.CurrentCell.Value.ToString();
                queryForm.NewValue = "";
                queryForm.ChangeMode = "longstring";
                queryForm.ShowDialog();




                if (queryForm.NewValue != "")
                {
                    dgv.CurrentRow.Cells["备注"].Value = queryForm.NewValue;
                    //dgv.CurrentRow.Cells["discount_money"].Value = DBNull.Value;

                    //dgv.CurrentRow.Cells["approve_flag"].Value = "False";
                    this.ly_sales_test_detail3BindingSource.EndEdit();
                    this.ly_sales_test_detail3TableAdapter.Update(this.lYSalseMange.ly_sales_test_detail3);

                    SQLDatabase.dataChangeREC(this.ly_sales_testDataGridView.CurrentRow.Cells["record_id"].Value.ToString(), "ly_sales_test", "UPD", this.Text);

                    //CountPlanStru();

                }
                else
                {

                }
                return;

            }


            ///////////////////////////////////////////////////////

            if ("安装类型" == dgv.CurrentCell.OwningColumn.Name)
            {

                ChangeValue queryForm = new ChangeValue();

                queryForm.OldValue = dgv.CurrentCell.Value.ToString();
                queryForm.NewValue = "";
                queryForm.ChangeMode = "string";
                queryForm.ShowDialog();




                if (queryForm.NewValue != "")
                {
                    dgv.CurrentRow.Cells["安装类型"].Value = queryForm.NewValue;
                    //dgv.CurrentRow.Cells["discount_money"].Value = DBNull.Value;

                    //dgv.CurrentRow.Cells["approve_flag"].Value = "False";
                    this.ly_sales_test_detail3BindingSource.EndEdit();
                    this.ly_sales_test_detail3TableAdapter.Update(this.lYSalseMange.ly_sales_test_detail3);

                    SQLDatabase.dataChangeREC(this.ly_sales_testDataGridView.CurrentRow.Cells["record_id"].Value.ToString(), "ly_sales_test", "UPD", this.Text);

                    //CountPlanStru();

                }
                else
                {

                }
                return;

            }


            ///////////////////////////////////////////////////////
            if ("油缸编号" == dgv.CurrentCell.OwningColumn.Name)
            {

                if (null == ly_material_plan_mainDataGridView.CurrentRow)
                {
                    return;
                }


                ChangeValue queryForm2 = new ChangeValue();

                queryForm2.OldValue = dgv.CurrentCell.Value.ToString();
                queryForm2.NewValue = "";
                queryForm2.ChangeMode = "longstring";
                queryForm2.ShowDialog();
                if (queryForm2.NewValue.Trim() == "")
                {
                    return;
                }
                string likestr = queryForm2.NewValue.Trim();
                string nowplannum = ly_material_plan_mainDataGridView.CurrentRow.Cells["计划编号0"].Value.ToString();

                string sel = @"select  machine_num as 制造编号,work_name as 工人,wzbh as 物料号, mch as 名称, xhc as 型号, gg  as 规格
                        from ly_production_detail_view where  ifQualified=1 and isinstore=1 and 
                        warehouse ='成品' and fir_style in('03驱动部' ,'02测量部','01控制部') and machine_num like '%"+ likestr 
                        + "%' union select a.machine_num ,a.employe,a.wzbh ,b.mch,b.xhc,b.gg from ly_store_in  a left join  ly_inma0010 b on a.wzbh = b.wzbh where machine_num is not null and machine_num like '%"+ likestr + "%'";
                QueryForm queryForm = new QueryForm();


                queryForm.Sel = sel;
                queryForm.Constr = SQLDatabase.Connectstring;
            
                queryForm.ShowDialog();

           

                if (queryForm.Result != "")
                {


                    if (ly_material_plan_mainDataGridView.CurrentRow != null && !string.IsNullOrEmpty(ly_material_plan_mainDataGridView.CurrentRow.Cells["合同编号0"].Value.ToString()))
                    {

                        DataTable dt = null;
                        using (SqlConnection connection = new SqlConnection(SQLDatabase.Connectstring))
                        {

                            SqlDataAdapter adapter = new SqlDataAdapter(@"SELECT  top 1  e.test_date FROM ly_sales_test_detail AS a LEFT OUTER JOIN ly_sales_test AS e ON a.record_id = e.id  where qd_num = '" + queryForm.Result + "' order by e.test_date desc", connection);
                            DataSet ds = new DataSet();
                            adapter.Fill(ds);
                            dt = ds.Tables[0];
                        }
                        if (dt.Rows.Count > 0)
                        {
                            DateTime dtNow = DateTime.Parse(dt.Rows[0][0].ToString());//获取最近一次的配调日期  
                            DataTable dt2 = null;
                            using (SqlConnection connection = new SqlConnection(SQLDatabase.Connectstring))
                            {
                                string sql = "select top 1  receive_date from ly_sales_receive_itemDetail where machine_num ='" + queryForm.Result + "' and (towhere='返库' or towhere='退库') order by receive_date desc";

                                SqlDataAdapter adapter = new SqlDataAdapter(sql, connection);
                                DataSet ds = new DataSet();
                                adapter.Fill(ds);
                                dt2 = ds.Tables[0];
                            }
                            if (dt2.Rows.Count <= 0)
                            {
                                MessageBox.Show("已经配调但是没有返库信息，再次输入将重复", "注意");
                                return;
                            }
                            else
                            {
                                DateTime dtNew2 = DateTime.Parse(dt2.Rows[0][0].ToString());//获取最近一次的收件日期  
                                if (dtNew2 < dtNow)
                                {
                                    MessageBox.Show("最近一次的配调时间小于最近一次的返库时间，再次输入将重复", "注意");
                                    return;
                                }
                            }

                        }
                    }




                    dgv.CurrentRow.Cells["油缸编号"].Value = queryForm.Result;
                    //dgv.CurrentRow.Cells["discount_money"].Value = DBNull.Value;

                    //dgv.CurrentRow.Cells["approve_flag"].Value = "False";
                    this.ly_sales_test_detail3BindingSource.EndEdit();
                    this.ly_sales_test_detail3TableAdapter.Update(this.lYSalseMange.ly_sales_test_detail3);

                    SQLDatabase.dataChangeREC(this.ly_sales_testDataGridView.CurrentRow.Cells["record_id"].Value.ToString(), "ly_sales_test", "UPD", this.Text);

                    //CountPlanStru();

                }
                else
                {

                }
                return;

            }


            ///////////////////////////////////////////////////////
            //if ("装置操作者" == dgv.CurrentCell.OwningColumn.Name)
            //{

            //    ChangeValue queryForm = new ChangeValue();

            //    queryForm.OldValue = dgv.CurrentCell.Value.ToString();
            //    queryForm.NewValue = "";
            //    queryForm.ChangeMode = "string";
            //    queryForm.ShowDialog();




            //    if (queryForm.NewValue != "")
            //    {
            //        dgv.CurrentRow.Cells["装置操作者"].Value = queryForm.NewValue;
            //        //dgv.CurrentRow.Cells["discount_money"].Value = DBNull.Value;

            //        //dgv.CurrentRow.Cells["approve_flag"].Value = "False";
            //        this.ly_sales_test_detail3BindingSource.EndEdit();
            //        this.ly_sales_test_detail3TableAdapter.Update(this.lYSalseMange.ly_sales_test_detail3);

            //        SQLDatabase.dataChangeREC(this.ly_sales_testDataGridView.CurrentRow.Cells["record_id"].Value.ToString(), "ly_sales_test", "UPD", this.Text);

            //        //CountPlanStru();

            //    }
            //    else
            //    {

            //    }
            //    return;

            //}


            ///////////////////////////////////////////////////////

            if ("装置出厂编号" == dgv.CurrentCell.OwningColumn.Name)
            {

          

                if (null == ly_material_plan_mainDataGridView.CurrentRow)
                {
                    return;
                }

                ChangeValue queryForm2 = new ChangeValue();

                queryForm2.OldValue = dgv.CurrentCell.Value.ToString();
                queryForm2.NewValue = "";
                queryForm2.ChangeMode = "longstring";
                queryForm2.ShowDialog();
                if (queryForm2.NewValue.Trim() == "")
                {
                    return;
                }
                string likestr = queryForm2.NewValue.Trim();


                string nowplannum = ly_material_plan_mainDataGridView.CurrentRow.Cells["计划编号0"].Value.ToString();

                string sel = @"select  machine_num as 制造编号,work_name as 工人,wzbh as 物料号,gg  as 规格, mch as 名称, xhc as 型号
                        from ly_production_detail_view where  ifQualified=1 and isinstore=1 and 
                        warehouse ='成品' and fir_style in('03驱动部' ,'02测量部','01控制部') and machine_num like '%" + likestr
                      + "%' union select a.machine_num ,a.employe,a.wzbh ,b.gg,b.mch,b.xhc from ly_store_in  a left join  ly_inma0010 b on a.wzbh = b.wzbh where machine_num is not null and machine_num like '%" + likestr + "%'";


                //string sel = @"select  machine_num as 制造编号,work_name as 工人,wzbh as 物料号,gg  as 规格, mch as 名称, xhc as 型号
                //        from ly_production_detail_view where  ifQualified=1 and isinstore=1 and 
                //      warehouse ='成品' and fir_style in('03驱动部' ,'02测量部','01控制部') ";
                QueryForm queryForm = new QueryForm();


                queryForm.Sel = sel;
                queryForm.Constr = SQLDatabase.Connectstring;
              
                queryForm.ShowDialog();
               

                if (queryForm.Result != "")
                {

                    if (ly_material_plan_mainDataGridView.CurrentRow != null && !string.IsNullOrEmpty(ly_material_plan_mainDataGridView.CurrentRow.Cells["合同编号0"].Value.ToString()))
                    {

                        DataTable dt = null;
                        using (SqlConnection connection = new SqlConnection(SQLDatabase.Connectstring))
                        {

                            SqlDataAdapter adapter = new SqlDataAdapter(@"SELECT  top 1  e.test_date FROM ly_sales_test_detail AS a LEFT OUTER JOIN ly_sales_test AS e ON a.record_id = e.id  where cl_num = '" + queryForm.Result + "' order by e.test_date desc", connection);
                            DataSet ds = new DataSet();
                            adapter.Fill(ds);
                            dt = ds.Tables[0];
                        }
                        if (dt.Rows.Count > 0)
                        {
                            DateTime dtNow = DateTime.Parse(dt.Rows[0][0].ToString());//获取最近一次的配调日期  
                            DataTable dt2 = null;
                            using (SqlConnection connection = new SqlConnection(SQLDatabase.Connectstring))
                            {
                                string sql = "select top 1  receive_date from ly_sales_receive_itemDetail where machine_num ='" + queryForm.Result + "' and (towhere='返库' or towhere='退库') order by receive_date desc";

                                SqlDataAdapter adapter = new SqlDataAdapter(sql, connection);
                                DataSet ds = new DataSet();
                                adapter.Fill(ds);
                                dt2 = ds.Tables[0];
                            }
                            if (dt2.Rows.Count <= 0)
                            {
                                MessageBox.Show("已经配调但是没有返库信息，再次输入将重复", "注意");
                                return;
                            }
                            else
                            {
                                DateTime dtNew2 = DateTime.Parse(dt2.Rows[0][0].ToString());//获取最近一次的收件日期  
                                if (dtNew2 < dtNow)
                                {
                                    MessageBox.Show("最近一次的配调时间小于最近一次的返库时间，再次输入将重复", "注意");
                                    return;
                                }
                            }

                        }
                    }


                    dgv.CurrentRow.Cells["装置规格"].Value = queryForm.Result3;
                    dgv.CurrentRow.Cells["装置操作者"].Value = queryForm.Result1;
                    dgv.CurrentRow.Cells["装置出厂编号"].Value = queryForm.Result;
                    //dgv.CurrentRow.Cells["discount_money"].Value = DBNull.Value;

                    //dgv.CurrentRow.Cells["approve_flag"].Value = "False";
                    this.ly_sales_test_detail3BindingSource.EndEdit();
                    this.ly_sales_test_detail3TableAdapter.Update(this.lYSalseMange.ly_sales_test_detail3);

                    SQLDatabase.dataChangeREC(this.ly_sales_testDataGridView.CurrentRow.Cells["record_id"].Value.ToString(), "ly_sales_test", "UPD", this.Text);
                    //CountPlanStru();

                }
                else
                {

                }
                return;

            }


            ///////////////////////////////////////////////////////
            //if ("装置规格" == dgv.CurrentCell.OwningColumn.Name)
            //{

            //    ChangeValue queryForm = new ChangeValue();

            //    queryForm.OldValue = dgv.CurrentCell.Value.ToString();
            //    queryForm.NewValue = "";
            //    queryForm.ChangeMode = "string";
            //    queryForm.ShowDialog();




            //    if (queryForm.NewValue != "")
            //    {
            //        dgv.CurrentRow.Cells["装置规格"].Value = queryForm.NewValue;
            //        //dgv.CurrentRow.Cells["discount_money"].Value = DBNull.Value;

            //        //dgv.CurrentRow.Cells["approve_flag"].Value = "False";
            //        this.ly_sales_test_detail3BindingSource.EndEdit();
            //        this.ly_sales_test_detail3TableAdapter.Update(this.lYSalseMange.ly_sales_test_detail3);

            //        SQLDatabase.dataChangeREC(this.ly_sales_testDataGridView.CurrentRow.Cells["record_id"].Value.ToString(), "ly_sales_test", "UPD", this.Text);
            //        //CountPlanStru();

            //    }
            //    else
            //    {

            //    }
            //    return;

            //}


            ///////////////////////////////////////////////////////
            //if ("控制仪操作者" == dgv.CurrentCell.OwningColumn.Name)
            //{

            //    ChangeValue queryForm = new ChangeValue();

            //    queryForm.OldValue = dgv.CurrentCell.Value.ToString();
            //    queryForm.NewValue = "";
            //    queryForm.ChangeMode = "string";
            //    queryForm.ShowDialog();




            //    if (queryForm.NewValue != "")
            //    {
            //        dgv.CurrentRow.Cells["控制仪操作者"].Value = queryForm.NewValue;
            //        //dgv.CurrentRow.Cells["discount_money"].Value = DBNull.Value;

            //        //dgv.CurrentRow.Cells["approve_flag"].Value = "False";
            //        this.ly_sales_test_detail3BindingSource.EndEdit();
            //        this.ly_sales_test_detail3TableAdapter.Update(this.lYSalseMange.ly_sales_test_detail3);

            //        SQLDatabase.dataChangeREC(this.ly_sales_testDataGridView.CurrentRow.Cells["record_id"].Value.ToString(), "ly_sales_test", "UPD", this.Text);
            //        //CountPlanStru();

            //    }
            //    else
            //    {

            //    }
            //    return;

            //}


            ///////////////////////////////////////////////////////
            if ("DIS_SN设定" == dgv.CurrentCell.OwningColumn.Name)
            {

                ChangeValue queryForm = new ChangeValue();

                queryForm.OldValue = dgv.CurrentCell.Value.ToString();
                queryForm.NewValue = "";
                queryForm.ChangeMode = "string";
                queryForm.ShowDialog();




                if (queryForm.NewValue != "")
                {
                    dgv.CurrentRow.Cells["DIS_SN设定"].Value = queryForm.NewValue;
                    //dgv.CurrentRow.Cells["discount_money"].Value = DBNull.Value;

                    //dgv.CurrentRow.Cells["approve_flag"].Value = "False";
                    this.ly_sales_test_detail3BindingSource.EndEdit();
                    this.ly_sales_test_detail3TableAdapter.Update(this.lYSalseMange.ly_sales_test_detail3);

                    SQLDatabase.dataChangeREC(this.ly_sales_testDataGridView.CurrentRow.Cells["record_id"].Value.ToString(), "ly_sales_test", "UPD", this.Text);
                    //CountPlanStru();

                }
                else
                {

                }
                return;

            }


            ///////////////////////////////////////////////////////
            if ("控制仪出厂编号" == dgv.CurrentCell.OwningColumn.Name)
            {

                //ChangeValue queryForm = new ChangeValue();

                //queryForm.OldValue = dgv.CurrentCell.Value.ToString();
                //queryForm.NewValue = "";
                //queryForm.ChangeMode = "string";
                //queryForm.ShowDialog();


                if (null == ly_material_plan_mainDataGridView.CurrentRow)
                {
                    return;
                }
                ChangeValue queryForm2 = new ChangeValue();

                queryForm2.OldValue = dgv.CurrentCell.Value.ToString();
                queryForm2.NewValue = "";
                queryForm2.ChangeMode = "longstring";
                queryForm2.ShowDialog();
                if (queryForm2.NewValue.Trim() == "")
                {
                    return;
                }
                string likestr = queryForm2.NewValue.Trim();

                string nowplannum = ly_material_plan_mainDataGridView.CurrentRow.Cells["计划编号0"].Value.ToString();

                string sel = @"select  machine_num as 制造编号,work_name as 工人,wzbh as 物料号, mch as 名称, xhc as 型号, gg  as 规格
                        from ly_production_detail_view where  ifQualified=1 and isinstore=1 and 
                        warehouse ='成品' and fir_style in('03驱动部' ,'02测量部','01控制部') and machine_num like '%" + likestr
                     + "%' union select a.machine_num ,a.employe,a.wzbh ,b.mch,b.xhc,b.gg from ly_store_in  a left join  ly_inma0010 b on a.wzbh = b.wzbh where machine_num is not null and machine_num like '%" + likestr + "%'";


                //string sel = @"select  machine_num as 制造编号,work_name as 工人,wzbh as 物料号, mch as 名称, xhc as 型号, gg  as 规格
                //        from ly_production_detail_view where  ifQualified=1 and isinstore=1 and 
                //        warehouse ='成品' and fir_style in('03驱动部' ,'02测量部','01控制部') ";
                QueryForm queryForm = new QueryForm();


                queryForm.Sel = sel;
                queryForm.Constr = SQLDatabase.Connectstring;
            
                queryForm.ShowDialog();

              

                if (queryForm.Result != "")
                {



                    if (ly_material_plan_mainDataGridView.CurrentRow != null && !string.IsNullOrEmpty(ly_material_plan_mainDataGridView.CurrentRow.Cells["合同编号0"].Value.ToString()))
                    {

                        DataTable dt = null;
                        using (SqlConnection connection = new SqlConnection(SQLDatabase.Connectstring))
                        {

                            SqlDataAdapter adapter = new SqlDataAdapter(@"SELECT  top 1  e.test_date FROM ly_sales_test_detail AS a LEFT OUTER JOIN ly_sales_test AS e ON a.record_id = e.id  where kz_num = '" + queryForm.Result + "' order by e.test_date desc", connection);
                            DataSet ds = new DataSet();
                            adapter.Fill(ds);
                            dt = ds.Tables[0];
                        }
                        if (dt.Rows.Count > 0)
                        {
                            DateTime dtNow = DateTime.Parse(dt.Rows[0][0].ToString());//获取最近一次的配调日期  
                            DataTable dt2 = null;
                            using (SqlConnection connection = new SqlConnection(SQLDatabase.Connectstring))
                            {
                                string sql = "select top 1  receive_date from ly_sales_receive_itemDetail where machine_num ='" + queryForm.Result + "' and (towhere='返库' or towhere='退库') order by receive_date desc";

                                SqlDataAdapter adapter = new SqlDataAdapter(sql, connection);
                                DataSet ds = new DataSet();
                                adapter.Fill(ds);
                                dt2 = ds.Tables[0];
                            }
                            if (dt2.Rows.Count <= 0)
                            {
                                MessageBox.Show("已经配调但是没有返库信息，再次输入将重复", "注意");
                                return;
                            }
                            else
                            {
                                DateTime dtNew2 = DateTime.Parse(dt2.Rows[0][0].ToString());//获取最近一次的收件日期  
                                if (dtNew2 < dtNow)
                                {
                                    MessageBox.Show("最近一次的配调时间小于最近一次的返库时间，再次输入将重复", "注意");
                                    return;
                                }
                            }

                        }
                    }



                    dgv.CurrentRow.Cells["控制仪操作者"].Value = queryForm.Result1;
                    dgv.CurrentRow.Cells["控制仪出厂编号"].Value = queryForm.Result;
                    //dgv.CurrentRow.Cells["discount_money"].Value = DBNull.Value;

                    //dgv.CurrentRow.Cells["approve_flag"].Value = "False";
                    this.ly_sales_test_detail3BindingSource.EndEdit();
                    this.ly_sales_test_detail3TableAdapter.Update(this.lYSalseMange.ly_sales_test_detail3);

                    SQLDatabase.dataChangeREC(this.ly_sales_testDataGridView.CurrentRow.Cells["record_id"].Value.ToString(), "ly_sales_test", "UPD", this.Text);
                    //CountPlanStru();

                }
                else
                {

                }
                return;

            }


            ///////////////////////////////////////////////////////

        }

        private void toolStripButton1_Click_1(object sender, EventArgs e)
        {
            LY_Salescontract_GroupDebug_Query queryForm = new LY_Salescontract_GroupDebug_Query();



            queryForm.OwnerForm = this;
            //queryForm.runmode = "增加";
            //queryForm.statemode = "原料";

            queryForm.StartPosition = FormStartPosition.CenterParent;
            queryForm.ShowDialog(this);

            //queryForm.ShowDialog();


            if (queryForm.DialogResult != DialogResult.Cancel)
            {
                //this.ly_inma0010ylTableAdapter.Fill(this.lYMaterialMange.ly_inma0010yl);
                //this.ly_inma0010ylBindingSource.Position = this.ly_inma0010ylBindingSource.Find("物资编号", queryForm.material_code);
            }
        }


        public void find_NowProduc(string nowplan, string nowgroup, string nowrecord, int nowid)
        {



            this.ly_material_plan_mainperiodBindingSource.Position = this.ly_material_plan_mainperiodBindingSource.Find("计划编号", nowplan);

            this.ly_sales_groupBindingSource.Position = this.ly_sales_groupBindingSource.Find("配套编码", nowgroup);

            this.ly_sales_testBindingSource.Position = this.ly_sales_testBindingSource.Find("配调记录号", nowrecord);

            this.ly_sales_test_detail3BindingSource.Position = this.ly_sales_test_detail3BindingSource.Find("id", nowid);




        }

        private void 自动生成记录明细ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (null == ly_sales_groupDataGridView.CurrentRow) return;

            if ("True" == ly_material_plan_mainDataGridView.CurrentRow.Cells["质检配调0"].Value.ToString())
            {
                MessageBox.Show("配调数据已经提交,不能修改数据...", "注意");
                return;

            }

            string message = "自动生成检验记录吗？";
            string caption = "提示...";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;

            DialogResult result;



            result = MessageBox.Show(message, caption, buttons,
            MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

            if (result == DialogResult.Yes)
            {
                int nowcount = 0;
                string nowrecodeNum = "";

                if (!string.IsNullOrEmpty(this.ly_sales_groupDataGridView.CurrentRow.Cells["数量"].Value.ToString()))
                {
                    nowcount = (int)decimal.Parse(this.ly_sales_groupDataGridView.CurrentRow.Cells["数量"].Value.ToString());
                }
                else
                {
                    nowcount = 0;
                }

                if (nowcount == 0) return;

                if (null == this.ly_plan_getmaterialDataGridView.CurrentRow) return;

                int parentId = int.Parse(this.ly_sales_groupDataGridView.CurrentRow.Cells["group_id"].Value.ToString());

                for (int i = 1; i <= nowcount; i++)
                {

                    this.ly_sales_testBindingSource.AddNew();

                    nowrecodeNum = GetMaxRecordNum();
                    this.ly_sales_testDataGridView.CurrentRow.Cells["配调记录"].Value = nowrecodeNum;

                    this.ly_sales_testDataGridView.CurrentRow.Cells["配调日期0"].Value = SQLDatabase.GetNowdate().ToString(); ;

                    this.ly_sales_testDataGridView.CurrentRow.Cells["配调人"].Value = SQLDatabase.nowUserName();


                    this.ly_sales_testDataGridView.CurrentRow.Cells["group_id0"].Value = parentId;

                    this.ly_sales_testBindingSource.EndEdit();
                    this.ly_sales_testTableAdapter.Update(this.lYSalseMange.ly_sales_test);

                    int recordId = int.Parse(this.ly_sales_testDataGridView.CurrentRow.Cells["record_id"].Value.ToString());

                    this.ly_sales_test_detail3TableAdapter.Fill(this.lYSalseMange.ly_sales_test_detail3, parentId);

                    this.ly_sales_test_detail3BindingSource.Position = this.ly_sales_test_detail3BindingSource.Find("配调记录", nowrecodeNum);

                    foreach (DataGridViewRow dgr in this.ly_plan_getmaterialDataGridView.Rows)
                    {

                        int kz_count = 0;
                        int cl_count = 0;
                        int qd_count = 0;


                        ///////////////////////////////////////

                        string nowitemstyle = dgr.Cells["产品大类"].Value.ToString();
                        string nowitemno = dgr.Cells["发货编码"].Value.ToString();

                        decimal nownotabsqty;
                        if ("" != dgr.Cells["未调"].Value.ToString())
                        {
                            nownotabsqty = decimal.Parse(dgr.Cells["未调"].Value.ToString());
                        }
                        else
                        {
                            nownotabsqty = 0;
                        }

                        if (nownotabsqty == 0)
                        {

                            //MessageBox.Show("该产品已全部配调检验,操作取消...", "注意");
                            continue;
                        }



                        if ("01控制部" == nowitemstyle)
                        {
                            this.ly_sales_test_detailDataGridView.CurrentRow.Cells["控制仪物料编码"].Value = nowitemno;
                        }

                        if ("02测量部" == nowitemstyle)
                        {
                            this.ly_sales_test_detailDataGridView.CurrentRow.Cells["装置物料编码"].Value = nowitemno;
                        }
                        if ("03驱动部" == nowitemstyle)
                        {
                            this.ly_sales_test_detailDataGridView.CurrentRow.Cells["驱动物料编码"].Value = nowitemno;
                        }


                        this.ly_sales_test_detail3BindingSource.EndEdit();

                        this.ly_sales_test_detail3TableAdapter.Update(this.lYSalseMange.ly_sales_test_detail3);

                        SQLDatabase.dataChangeREC(this.ly_sales_testDataGridView.CurrentRow.Cells["record_id"].Value.ToString(), "ly_sales_test", "ADD", this.Text);

                        //this.ly_plan_getmaterial1TableAdapter.Fill(this.lYSalseMange.ly_plan_getmaterial1, nowplannum, nowgroupnum, 0 - nowgroupid);
                        //this.ly_plan_getmaterial1BindingSource.Position = this.ly_plan_getmaterial1BindingSource.Find("发货编码", nowitemno);


                        //this.ly_sales_test_detail3TableAdapter.Fill(this.lYSalseMange.ly_sales_test_detail3, nowgroupid);

                        //this.ly_sales_test_detail3BindingSource.Position = this.ly_sales_test_detail3BindingSource.Find("配调记录", nowdebugCode);
                        /////////////////////////////////////







                    }

                    this.ly_sales_test_detail3TableAdapter.Fill(this.lYSalseMange.ly_sales_test_detail3, parentId);
                    string nowplannum = ly_material_plan_mainDataGridView.CurrentRow.Cells["计划编号0"].Value.ToString();
                    string nowcontractnum = ly_material_plan_mainDataGridView.CurrentRow.Cells["计划编号0"].Value.ToString();
                    string nowgroupnum = ly_sales_groupDataGridView.CurrentRow.Cells["配套编码"].Value.ToString();
                    this.ly_plan_getmaterial1TableAdapter.Fill(this.lYSalseMange.ly_plan_getmaterial1, nowplannum, nowgroupnum, 0 - parentId);

                }






                /////////////////////////////////////////////////





            }
        }

        private void SetFormState(string state)
        {
            // view edit add save cancle

            if ("View" == state)
            {
                this.formState = "View";

                this.toolStripButton7.Enabled = true;
                this.toolStripButton8.Enabled = false;
                this.yX_fillCard_MoneyBindingNavigatorSaveItem.Enabled = false;
                this.bindingNavigatorDeleteItem.Enabled = true;
                this.bindingNavigatorAddNewItem.Enabled = true;




                this.toolStripButton2.Enabled = true;
                this.toolStripButton3.Enabled = true;
                this.toolStripButton4.Enabled = true;
                this.toolStripButton6.Enabled = true;
                this.toolStripTextBox1.Enabled = true;



                this.ly_sales_test_detailDataGridView.ReadOnly = true;






            }
            else
            {
                this.formState = "Edit";

                this.ly_sales_test_detailDataGridView.ReadOnly = false;

                if (null != ly_sales_test_detailDataGridView.CurrentRow)
                    this.nowRow = ly_sales_test_detailDataGridView.CurrentRow.Index;

                this.toolStripButton7.Enabled = false;
                this.toolStripButton8.Enabled = true;
                this.yX_fillCard_MoneyBindingNavigatorSaveItem.Enabled = true;
                this.bindingNavigatorDeleteItem.Enabled = false;
                this.bindingNavigatorAddNewItem.Enabled = false;





                this.toolStripButton2.Enabled = false;
                this.toolStripButton3.Enabled = false;
                this.toolStripButton4.Enabled = false;
                this.toolStripButton6.Enabled = false;
                this.toolStripTextBox1.Enabled = false;



                this.ly_sales_test_detailDataGridView.ReadOnly = false;


                this.ly_sales_test_detailDataGridView.Columns["配调记录1"].ReadOnly = true;
                this.ly_sales_test_detailDataGridView.Columns["配调人1"].ReadOnly = true;

                this.ly_sales_test_detailDataGridView.Columns["控制仪物料编码"].ReadOnly = true;
                this.ly_sales_test_detailDataGridView.Columns["控制仪型号"].ReadOnly = true;


                this.ly_sales_test_detailDataGridView.Columns["装置物料编码"].ReadOnly = true;
                this.ly_sales_test_detailDataGridView.Columns["装置型号"].ReadOnly = true;



                this.ly_sales_test_detailDataGridView.Columns["驱动物料编码"].ReadOnly = true;
                this.ly_sales_test_detailDataGridView.Columns["驱动型号"].ReadOnly = true;








            }


        }

        private void bindingNavigatorAddNewItem_Click(object sender, EventArgs e)
        {


            //////////////////////

            if (null == ly_sales_groupDataGridView.CurrentRow) return;

            if ("True" == ly_material_plan_mainDataGridView.CurrentRow.Cells["质检配调0"].Value.ToString())
            {
                MessageBox.Show("配调数据已经提交,不能修改数据...", "注意");
                return;

            }

            string message = "增加配调检验记录吗？";
            string caption = "提示...";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;

            DialogResult result;



            result = MessageBox.Show(message, caption, buttons,
            MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

            if (result == DialogResult.Yes)
            {
                this.ly_sales_testBindingSource.AddNew();

                this.ly_sales_testDataGridView.CurrentRow.Cells["配调记录"].Value = GetMaxRecordNum();

                this.ly_sales_testDataGridView.CurrentRow.Cells["配调日期0"].Value = SQLDatabase.GetNowdate().ToString(); ;

                this.ly_sales_testDataGridView.CurrentRow.Cells["配调人"].Value = SQLDatabase.nowUserName();

                int parentId = int.Parse(this.ly_sales_groupDataGridView.CurrentRow.Cells["group_id"].Value.ToString());
                this.ly_sales_testDataGridView.CurrentRow.Cells["group_id0"].Value = parentId;

                this.ly_sales_testBindingSource.EndEdit();
                this.ly_sales_testTableAdapter.Update(this.lYSalseMange.ly_sales_test);

                //if (null == ly_sales_testDataGridView.CurrentRow)
                //{
                //    this.ly_sales_test_detail3TableAdapter.Fill(this.lYSalseMange.ly_sales_test_detail3, -1);
                //    return;
                //}

                int recordId = int.Parse(this.ly_sales_testDataGridView.CurrentRow.Cells["record_id"].Value.ToString());

                this.ly_sales_test_detail3TableAdapter.Fill(this.lYSalseMange.ly_sales_test_detail3, parentId);

                this.ly_sales_test_detail3BindingSource.Position = this.ly_sales_test_detail3BindingSource.Find("record_id", recordId);

                foreach (DataGridViewRow dgr in this.ly_plan_getmaterialDataGridView.Rows)
                {






                    ////////////////////////////////

                    string nowitemstyle = dgr.Cells["产品大类"].Value.ToString();
                    string nowitemno = dgr.Cells["发货编码"].Value.ToString();

                    decimal nownotabsqty;
                    if ("" != dgr.Cells["未调"].Value.ToString())
                    {
                        nownotabsqty = decimal.Parse(dgr.Cells["未调"].Value.ToString());
                    }
                    else
                    {
                        nownotabsqty = 0;
                    }

                    if (nownotabsqty == 0)
                    {

                        //MessageBox.Show("该产品已全部配调检验,操作取消...", "注意");
                        continue;
                    }



                    if ("01控制部" == nowitemstyle)
                    {
                        this.ly_sales_test_detailDataGridView.CurrentRow.Cells["控制仪物料编码"].Value = nowitemno;
                    }

                    if ("02测量部" == nowitemstyle)
                    {
                        this.ly_sales_test_detailDataGridView.CurrentRow.Cells["装置物料编码"].Value = nowitemno;
                    }
                    if ("03驱动部" == nowitemstyle)
                    {
                        this.ly_sales_test_detailDataGridView.CurrentRow.Cells["驱动物料编码"].Value = nowitemno;
                    }


                    this.ly_sales_test_detail3BindingSource.EndEdit();

                    this.ly_sales_test_detail3TableAdapter.Update(this.lYSalseMange.ly_sales_test_detail3);

                    SQLDatabase.dataChangeREC(this.ly_sales_testDataGridView.CurrentRow.Cells["record_id"].Value.ToString(), "ly_sales_test", "ADD", this.Text);

                    //this.ly_plan_getmaterial1TableAdapter.Fill(this.lYSalseMange.ly_plan_getmaterial1, nowplannum, nowgroupnum, 0 - nowgroupid);
                    //this.ly_plan_getmaterial1BindingSource.Position = this.ly_plan_getmaterial1BindingSource.Find("发货编码", nowitemno);


                    //this.ly_sales_test_detail3TableAdapter.Fill(this.lYSalseMange.ly_sales_test_detail3, nowgroupid);

                    //this.ly_sales_test_detail3BindingSource.Position = this.ly_sales_test_detail3BindingSource.Find("配调记录", nowdebugCode);
                    /////////////////////////////////////







                }

                this.ly_sales_test_detail3TableAdapter.Fill(this.lYSalseMange.ly_sales_test_detail3, parentId);
                string nowplannum = ly_material_plan_mainDataGridView.CurrentRow.Cells["计划编号0"].Value.ToString();
                string nowcontractnum = ly_material_plan_mainDataGridView.CurrentRow.Cells["计划编号0"].Value.ToString();
                string nowgroupnum = ly_sales_groupDataGridView.CurrentRow.Cells["配套编码"].Value.ToString();
                this.ly_plan_getmaterial1TableAdapter.Fill(this.lYSalseMange.ly_plan_getmaterial1, nowplannum, nowgroupnum, 0 - parentId);


                //this.SetFormState("Edit");
                //this.saveState = "Add";


                // ly_worker_listDataGridView.CurrentCell = ly_worker_listDataGridView.CurrentRow.Cells["工号"];







            }
        }

        private void toolStripButton7_Click(object sender, EventArgs e)
        {

            if (null == this.ly_sales_test_detailDataGridView.CurrentRow) return;

            //if (this.ly_material_plan_detailDataGridView.RowCount > 0)
            //{
            //    MessageBox.Show("计划已有物料记录，不能删除(实需删除，请先删除该计划的物料记录)", "注意");
            //    return;

            //}

            string diaodu = this.ly_sales_test_detailDataGridView.CurrentRow.Cells["配调人1"].Value.ToString();

            if (diaodu != SQLDatabase.nowUserName())
            {
                MessageBox.Show("请配调人:" + diaodu + "修改", "注意");
                return;
            }


            this.SetFormState("Edit");
            this.saveState = "Change";
        }

        private void bindingNavigatorDeleteItem_Click(object sender, EventArgs e)
        {
            if (null == this.ly_sales_test_detailDataGridView.CurrentRow) return;

            //if (this.ly_material_plan_detailDataGridView.RowCount > 0)
            //{
            //    MessageBox.Show("计划已有物料记录，不能删除(实需删除，请先删除该计划的物料记录)", "注意");
            //    return;

            //}

            string diaodu = this.ly_sales_test_detailDataGridView.CurrentRow.Cells["配调人1"].Value.ToString();

            if (diaodu != SQLDatabase.nowUserName())
            {
                MessageBox.Show("请配调人:" + diaodu + "删除", "注意");
                return;
            }

            if ("True" == ly_material_plan_mainDataGridView.CurrentRow.Cells["营业发货0"].Value.ToString())
            {
                MessageBox.Show("依赖书已经发货,不能删除数据...", "注意");
                return;

            }
            if ("True" == ly_material_plan_mainDataGridView.CurrentRow.Cells["质检配调0"].Value.ToString())
            {
                MessageBox.Show("配调数据已经提交,不能修改数据...", "注意");
                return;

            }
            //if (ly_production_order_detailDataGridView.RowCount > 0)
            //{
            //    MessageBox.Show("跟单已有工序安排,删除所有工序安排后才能删除跟单...", "注意");
            //    return;

            //}

            //string nowproductionorder = this.ly_production_orderDataGridView.CurrentRow.Cells["跟单编号"].Value.ToString();


            string message1 = "当前(配调记录)将被删除，继续吗？";
            string caption1 = "提示...";
            MessageBoxButtons buttons1 = MessageBoxButtons.YesNo;

            DialogResult result1;



            result1 = MessageBox.Show(message1, caption1, buttons1,
            MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

            if (result1 == DialogResult.Yes)
            {




                int record_Id = int.Parse(ly_sales_test_detailDataGridView.CurrentRow.Cells["record_id1"].Value.ToString());

                SQLDatabase.dataChangeREC(ly_sales_test_detailDataGridView.CurrentRow.Cells["record_id1"].Value.ToString(), "ly_sales_test", "DEL", this.Text);


                //string insstr = " delete ly_material_plan_main  where material_plan_num = '" + nowPlanNumber + "'";

                string delstr = " delete ly_sales_test  " +
                           " where  id=" + record_Id.ToString();


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




                        this.ly_sales_test_detail3BindingSource.RemoveCurrent();



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

                ////////////////////////////////////
                int nowgroupid = int.Parse(ly_sales_groupDataGridView.CurrentRow.Cells["group_id"].Value.ToString());

                string nowplannum = ly_material_plan_mainDataGridView.CurrentRow.Cells["计划编号0"].Value.ToString();

                string nowgroupnum = ly_sales_groupDataGridView.CurrentRow.Cells["配套编码"].Value.ToString();





                this.ly_plan_getmaterial1TableAdapter.Fill(this.lYSalseMange.ly_plan_getmaterial1, nowplannum, nowgroupnum, 0 - nowgroupid);

                this.ly_sales_test_detail3TableAdapter.Fill(this.lYSalseMange.ly_sales_test_detail3, nowgroupid);

                //////////////////////////////////////


            }

            this.SetFormState("View");
        }

        private void toolStripButton8_Click(object sender, EventArgs e)
        {
            this.saveState = "Cancle";



            this.SetFormState("View");
            //this.ly_worker_listTableAdapter.Fill(this.lYMaterielRequirements.ly_worker_list, this.prod_code);
        }

        private void yX_fillCard_MoneyBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            if (null == ly_sales_test_detailDataGridView.CurrentRow) return;

            //this.ly_sales_test_detail3BindingSource.EndEdit();
            //this.ly_sales_test_detail3TableAdapter.Update(this.lYSalseMange.ly_sales_test_detail3);

            if (!this.Validate()) return;

            this.ly_sales_test_detail3BindingSource.EndEdit();
            this.ly_sales_test_detail3TableAdapter.Update(this.lYSalseMange.ly_sales_test_detail3);

            SQLDatabase.dataChangeREC(this.ly_sales_testDataGridView.CurrentRow.Cells["record_id"].Value.ToString(), "ly_sales_test", "UPD", this.Text);



            this.SetFormState("View");
        }

        private void ly_sales_test_detailDataGridView_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            if ("View" == this.formState) return;

            if (10 == e.ColumnIndex || 15 == e.ColumnIndex || 19 == e.ColumnIndex || 20 == e.ColumnIndex)
            {
                AppSet appSet = AppSet.Load();

                if (0 < appSet.KeyboardInputIndex)
                {
                    InputLanguage.CurrentInputLanguage = InputLanguage.InstalledInputLanguages[appSet.KeyboardInputIndex];
                }
            }
        }

        private void ly_sales_test_detailDataGridView_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
            if ("View" == this.formState) return;

            if (10 == e.ColumnIndex || 15 == e.ColumnIndex || 19 == e.ColumnIndex || 20 == e.ColumnIndex)
            {
                InputLanguage.CurrentInputLanguage = InputLanguage.DefaultInputLanguage;
            }
        }

        private void toolStripTextBox5_KeyUp_1(object sender, KeyEventArgs e)
        {
            string filterString;


            filterString = GetDataGridviewMultiFilterString.DGVMultiFilterString(this.ly_material_plan_mainDataGridView, this.toolStripTextBox5.Text);

            if ("" != filterString)
            {
                //this.ly_material_plan_mainperiodBindingSource.Filter = "出库指令=1 and ( " + filterString +" )";
                this.ly_material_plan_mainperiodBindingSource.Filter = filterString;
            }
            else
            {
                //this.ly_material_plan_mainperiodBindingSource.Filter = "出库指令=1";
                this.ly_material_plan_mainperiodBindingSource.Filter = "";
            }
        }

        private void toolStripTextBox5_Enter_1(object sender, EventArgs e)
        {
            toolStripTextBox5.Text = "";

            this.ly_material_plan_mainperiodBindingSource.Filter = "";
            //this.ly_material_plan_mainperiodBindingSource.Filter = "出库指令=1";
        }

        private void toolStripButton9_Click(object sender, EventArgs e)
        {
            LY_QualityDebug_Sumreport queryForm = new LY_QualityDebug_Sumreport();

            //queryForm.OwnerForm = this;


            //queryForm.StartPosition = FormStartPosition.CenterParent;

            queryForm.WindowState = FormWindowState.Maximized;
            queryForm.ShowDialog(this);

        }

        private void 自动生成记录明细部分ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (null == ly_sales_groupDataGridView.CurrentRow) return;

            if ("True" == ly_material_plan_mainDataGridView.CurrentRow.Cells["质检配调0"].Value.ToString())
            {
                MessageBox.Show("配调数据已经提交,不能修改数据...", "注意");
                return;

            }

            string message = "自动生成检验记录吗？";
            string caption = "提示...";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;

            DialogResult result;



            result = MessageBox.Show(message, caption, buttons,
            MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

            if (result == DialogResult.Yes)
            {
                int nowcount = 0;

                if (!string.IsNullOrEmpty(this.ly_sales_groupDataGridView.CurrentRow.Cells["数量"].Value.ToString()))
                {
                    nowcount = (int)decimal.Parse(this.ly_sales_groupDataGridView.CurrentRow.Cells["数量"].Value.ToString());
                }
                else
                {
                    nowcount = 0;
                }

                if (nowcount == 0) return;

                ChangeValue queryForm = new ChangeValue();

                queryForm.OldValue = "";
                queryForm.NewValue = "";
                queryForm.ChangeMode = "decimal";
                queryForm.ShowDialog();




                if (queryForm.NewValue == "") return;

                if (int.Parse(queryForm.NewValue) < nowcount)
                {

                    nowcount = int.Parse(queryForm.NewValue);
                }



                if (null == this.ly_plan_getmaterialDataGridView.CurrentRow) return;

                int parentId = int.Parse(this.ly_sales_groupDataGridView.CurrentRow.Cells["group_id"].Value.ToString());

                for (int i = 1; i <= nowcount; i++)
                {

                    this.ly_sales_testBindingSource.AddNew();

                    this.ly_sales_testDataGridView.CurrentRow.Cells["配调记录"].Value = GetMaxRecordNum();

                    this.ly_sales_testDataGridView.CurrentRow.Cells["配调日期0"].Value = SQLDatabase.GetNowdate().ToString(); ;

                    this.ly_sales_testDataGridView.CurrentRow.Cells["配调人"].Value = SQLDatabase.nowUserName();


                    this.ly_sales_testDataGridView.CurrentRow.Cells["group_id0"].Value = parentId;

                    this.ly_sales_testBindingSource.EndEdit();
                    this.ly_sales_testTableAdapter.Update(this.lYSalseMange.ly_sales_test);

                    int recordId = int.Parse(this.ly_sales_testDataGridView.CurrentRow.Cells["record_id"].Value.ToString());

                    this.ly_sales_test_detail3TableAdapter.Fill(this.lYSalseMange.ly_sales_test_detail3, parentId);

                    this.ly_sales_test_detail3BindingSource.Position = this.ly_sales_test_detail3BindingSource.Find("record_id", recordId);

                    foreach (DataGridViewRow dgr in this.ly_plan_getmaterialDataGridView.Rows)
                    {

                        int kz_count = 0;
                        int cl_count = 0;
                        int qd_count = 0;


                        ///////////////////////////////////////

                        string nowitemstyle = dgr.Cells["产品大类"].Value.ToString();
                        string nowitemno = dgr.Cells["发货编码"].Value.ToString();

                        decimal nownotabsqty;
                        if ("" != dgr.Cells["未调"].Value.ToString())
                        {
                            nownotabsqty = decimal.Parse(dgr.Cells["未调"].Value.ToString());
                        }
                        else
                        {
                            nownotabsqty = 0;
                        }

                        if (nownotabsqty == 0)
                        {

                            //MessageBox.Show("该产品已全部配调检验,操作取消...", "注意");
                            continue;
                        }



                        if ("01控制部" == nowitemstyle)
                        {
                            this.ly_sales_test_detailDataGridView.CurrentRow.Cells["控制仪物料编码"].Value = nowitemno;
                        }

                        if ("02测量部" == nowitemstyle)
                        {
                            this.ly_sales_test_detailDataGridView.CurrentRow.Cells["装置物料编码"].Value = nowitemno;
                        }
                        if ("03驱动部" == nowitemstyle)
                        {
                            this.ly_sales_test_detailDataGridView.CurrentRow.Cells["驱动物料编码"].Value = nowitemno;
                        }


                        this.ly_sales_test_detail3BindingSource.EndEdit();

                        this.ly_sales_test_detail3TableAdapter.Update(this.lYSalseMange.ly_sales_test_detail3);

                        SQLDatabase.dataChangeREC(this.ly_sales_testDataGridView.CurrentRow.Cells["record_id"].Value.ToString(), "ly_sales_test", "ADD", this.Text);



                    }

                    this.ly_sales_test_detail3TableAdapter.Fill(this.lYSalseMange.ly_sales_test_detail3, parentId);
                    string nowplannum = ly_material_plan_mainDataGridView.CurrentRow.Cells["计划编号0"].Value.ToString();
                    string nowcontractnum = ly_material_plan_mainDataGridView.CurrentRow.Cells["计划编号0"].Value.ToString();
                    string nowgroupnum = ly_sales_groupDataGridView.CurrentRow.Cells["配套编码"].Value.ToString();
                    this.ly_plan_getmaterial1TableAdapter.Fill(this.lYSalseMange.ly_plan_getmaterial1, nowplannum, nowgroupnum, 0 - parentId);

                }






                /////////////////////////////////////////////////





            }
        }

        private void 手动填写ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (null == this.ly_sales_test_detailDataGridView.CurrentRow) return;

            string diaodu = this.ly_sales_test_detailDataGridView.CurrentRow.Cells["配调人1"].Value.ToString();

            if (!string.IsNullOrEmpty(diaodu) && diaodu != SQLDatabase.nowUserName())
            {
                MessageBox.Show("请配调人:" + diaodu + "修改", "注意");
                return;
            }

            DataGridView dgv = ly_sales_test_detailDataGridView;

            if (null == dgv.CurrentRow) return;

            if ("油缸编号" == dgv.CurrentCell.OwningColumn.Name)
            {

                ChangeValue queryForm = new ChangeValue();

                queryForm.OldValue = dgv.CurrentCell.Value.ToString();
                queryForm.NewValue = "";
                queryForm.ChangeMode = "string";
                queryForm.ShowDialog();


                if (queryForm.NewValue != "")
                {


                    if (ly_material_plan_mainDataGridView.CurrentRow != null && !string.IsNullOrEmpty(ly_material_plan_mainDataGridView.CurrentRow.Cells["合同编号0"].Value.ToString()))
                    {

                        DataTable dt = null;
                        using (SqlConnection connection = new SqlConnection(SQLDatabase.Connectstring))
                        {

                            SqlDataAdapter adapter = new SqlDataAdapter(@"SELECT  top 1  e.test_date FROM ly_sales_test_detail AS a LEFT OUTER JOIN ly_sales_test AS e ON a.record_id = e.id  where qd_num = '" + queryForm.NewValue + "' order by e.test_date desc", connection);
                            DataSet ds = new DataSet();
                            adapter.Fill(ds);
                            dt = ds.Tables[0];
                        }
                        if (dt.Rows.Count > 0)
                        {
                            DateTime dtNow = DateTime.Parse(dt.Rows[0][0].ToString());//获取最近一次的配调日期  
                            DataTable dt2 = null;
                            using (SqlConnection connection = new SqlConnection(SQLDatabase.Connectstring))
                            {
                                string sql = "select top 1  receive_date from ly_sales_receive_itemDetail where machine_num ='" + queryForm.NewValue + "' and (towhere='返库' or towhere='退库') order by receive_date desc";

                                SqlDataAdapter adapter = new SqlDataAdapter(sql, connection);
                                DataSet ds = new DataSet();
                                adapter.Fill(ds);
                                dt2 = ds.Tables[0];
                            }
                            if (dt2.Rows.Count <= 0)
                            {
                                MessageBox.Show("已经配调但是没有返库信息，再次输入将重复", "注意");
                                return;
                            }
                            else
                            {
                                DateTime dtNew2 = DateTime.Parse(dt2.Rows[0][0].ToString());//获取最近一次的收件日期  
                                if (dtNew2 < dtNow)
                                {
                                    MessageBox.Show("最近一次的配调时间小于最近一次的返库时间，再次输入将重复", "注意");
                                    return;
                                }
                            }

                        }
                    }




                    dgv.CurrentRow.Cells["油缸编号"].Value = queryForm.NewValue;
                    this.ly_sales_test_detail3BindingSource.EndEdit();
                    this.ly_sales_test_detail3TableAdapter.Update(this.lYSalseMange.ly_sales_test_detail3);

                    SQLDatabase.dataChangeREC(this.ly_sales_testDataGridView.CurrentRow.Cells["record_id"].Value.ToString(), "ly_sales_test", "UPD", this.Text);

                }
                else
                {

                }
                return;
            }


            if ("装置操作者" == dgv.CurrentCell.OwningColumn.Name)
            {

                ChangeValue queryForm = new ChangeValue();

                queryForm.OldValue = dgv.CurrentCell.Value.ToString();
                queryForm.NewValue = "";
                queryForm.ChangeMode = "string";
                queryForm.ShowDialog();

                if (queryForm.NewValue != "")
                {
                    dgv.CurrentRow.Cells["装置操作者"].Value = queryForm.NewValue;
                    this.ly_sales_test_detail3BindingSource.EndEdit();
                    this.ly_sales_test_detail3TableAdapter.Update(this.lYSalseMange.ly_sales_test_detail3);

                    SQLDatabase.dataChangeREC(this.ly_sales_testDataGridView.CurrentRow.Cells["record_id"].Value.ToString(), "ly_sales_test", "UPD", this.Text);
                }
                else
                {

                }
                return;

            }


            ///////////////////////////////////////////////////////

            if ("装置出厂编号" == dgv.CurrentCell.OwningColumn.Name)
            {

                ChangeValue queryForm = new ChangeValue();

                queryForm.OldValue = dgv.CurrentCell.Value.ToString();
                queryForm.NewValue = "";
                queryForm.ChangeMode = "string";
                queryForm.ShowDialog();




                if (queryForm.NewValue != "")
                {

                    if (ly_material_plan_mainDataGridView.CurrentRow != null && !string.IsNullOrEmpty(ly_material_plan_mainDataGridView.CurrentRow.Cells["合同编号0"].Value.ToString()))
                    {

                        DataTable dt = null;
                        using (SqlConnection connection = new SqlConnection(SQLDatabase.Connectstring))
                        {

                            SqlDataAdapter adapter = new SqlDataAdapter(@"SELECT  top 1  e.test_date FROM ly_sales_test_detail AS a LEFT OUTER JOIN ly_sales_test AS e ON a.record_id = e.id  where cl_num = '" + queryForm.NewValue + "' order by e.test_date desc", connection);
                            DataSet ds = new DataSet();
                            adapter.Fill(ds);
                            dt = ds.Tables[0];
                        }
                        if (dt.Rows.Count > 0)
                        {
                            DateTime dtNow = DateTime.Parse(dt.Rows[0][0].ToString());//获取最近一次的配调日期  
                            DataTable dt2 = null;
                            using (SqlConnection connection = new SqlConnection(SQLDatabase.Connectstring))
                            {
                                string sql = "select top 1  receive_date from ly_sales_receive_itemDetail where machine_num ='" + queryForm.NewValue + "' and (towhere='返库' or towhere='退库') order by receive_date desc";

                                SqlDataAdapter adapter = new SqlDataAdapter(sql, connection);
                                DataSet ds = new DataSet();
                                adapter.Fill(ds);
                                dt2 = ds.Tables[0];
                            }
                            if (dt2.Rows.Count <= 0)
                            {
                                MessageBox.Show("已经配调但是没有返库信息，再次输入将重复", "注意");
                                return;
                            }
                            else
                            {
                                DateTime dtNew2 = DateTime.Parse(dt2.Rows[0][0].ToString());//获取最近一次的收件日期  
                                if (dtNew2 < dtNow)
                                {
                                    MessageBox.Show("最近一次的配调时间小于最近一次的返库时间，再次输入将重复", "注意");
                                    return;
                                }
                            }

                        }
                    }

                    dgv.CurrentRow.Cells["装置出厂编号"].Value = queryForm.NewValue;
                    this.ly_sales_test_detail3BindingSource.EndEdit();
                    this.ly_sales_test_detail3TableAdapter.Update(this.lYSalseMange.ly_sales_test_detail3);

                    SQLDatabase.dataChangeREC(this.ly_sales_testDataGridView.CurrentRow.Cells["record_id"].Value.ToString(), "ly_sales_test", "UPD", this.Text);

                }
                else
                {

                }
                return;

            }

            if ("装置规格" == dgv.CurrentCell.OwningColumn.Name)
            {

                ChangeValue queryForm = new ChangeValue();

                queryForm.OldValue = dgv.CurrentCell.Value.ToString();
                queryForm.NewValue = "";
                queryForm.ChangeMode = "string";
                queryForm.ShowDialog();




                if (queryForm.NewValue != "")
                {
                    dgv.CurrentRow.Cells["装置规格"].Value = queryForm.NewValue;
                    this.ly_sales_test_detail3BindingSource.EndEdit();
                    this.ly_sales_test_detail3TableAdapter.Update(this.lYSalseMange.ly_sales_test_detail3);

                    SQLDatabase.dataChangeREC(this.ly_sales_testDataGridView.CurrentRow.Cells["record_id"].Value.ToString(), "ly_sales_test", "UPD", this.Text);

                }
                else
                {

                }
                return;

            }


            /////////////////////////////////////////////////////
            if ("控制仪操作者" == dgv.CurrentCell.OwningColumn.Name)
            {

                ChangeValue queryForm = new ChangeValue();

                queryForm.OldValue = dgv.CurrentCell.Value.ToString();
                queryForm.NewValue = "";
                queryForm.ChangeMode = "string";
                queryForm.ShowDialog();




                if (queryForm.NewValue != "")
                {
                    dgv.CurrentRow.Cells["控制仪操作者"].Value = queryForm.NewValue;
                    this.ly_sales_test_detail3BindingSource.EndEdit();
                    this.ly_sales_test_detail3TableAdapter.Update(this.lYSalseMange.ly_sales_test_detail3);

                    SQLDatabase.dataChangeREC(this.ly_sales_testDataGridView.CurrentRow.Cells["record_id"].Value.ToString(), "ly_sales_test", "UPD", this.Text);
                    //CountPlanStru();

                }
                else
                {

                }
                return;

            }


            ///////////////////////////////////////////////////////
            if ("控制仪出厂编号" == dgv.CurrentCell.OwningColumn.Name)
            {

                ChangeValue queryForm = new ChangeValue();

                queryForm.OldValue = dgv.CurrentCell.Value.ToString();
                queryForm.NewValue = "";
                queryForm.ChangeMode = "string";
                queryForm.ShowDialog();




                if (queryForm.NewValue != "")
                {



                    if (ly_material_plan_mainDataGridView.CurrentRow != null && !string.IsNullOrEmpty(ly_material_plan_mainDataGridView.CurrentRow.Cells["合同编号0"].Value.ToString()))
                    {

                        DataTable dt = null;
                        using (SqlConnection connection = new SqlConnection(SQLDatabase.Connectstring))
                        {

                            SqlDataAdapter adapter = new SqlDataAdapter(@"SELECT  top 1  e.test_date FROM ly_sales_test_detail AS a LEFT OUTER JOIN ly_sales_test AS e ON a.record_id = e.id  where kz_num = '" + queryForm.NewValue + "' order by e.test_date desc", connection);
                            DataSet ds = new DataSet();
                            adapter.Fill(ds);
                            dt = ds.Tables[0];
                        }
                        if (dt.Rows.Count > 0)
                        {
                            DateTime dtNow = DateTime.Parse(dt.Rows[0][0].ToString());//获取最近一次的配调日期  
                            DataTable dt2 = null;
                            using (SqlConnection connection = new SqlConnection(SQLDatabase.Connectstring))
                            {
                                string sql = "select top 1  receive_date from ly_sales_receive_itemDetail where machine_num ='" + queryForm.NewValue + "' and (towhere='返库' or towhere='退库') order by receive_date desc";

                                SqlDataAdapter adapter = new SqlDataAdapter(sql, connection);
                                DataSet ds = new DataSet();
                                adapter.Fill(ds);
                                dt2 = ds.Tables[0];
                            }
                            if (dt2.Rows.Count <= 0)
                            {
                                MessageBox.Show("已经配调但是没有返库信息，再次输入将重复", "注意");
                                return;
                            }
                            else
                            {
                                DateTime dtNew2 = DateTime.Parse(dt2.Rows[0][0].ToString());//获取最近一次的收件日期  
                                if (dtNew2 < dtNow)
                                {
                                    MessageBox.Show("最近一次的配调时间小于最近一次的返库时间，再次输入将重复", "注意");
                                    return;
                                }
                            }

                        }
                    }

                    dgv.CurrentRow.Cells["控制仪出厂编号"].Value = queryForm.NewValue;
                    //dgv.CurrentRow.Cells["discount_money"].Value = DBNull.Value;

                    //dgv.CurrentRow.Cells["approve_flag"].Value = "False";
                    this.ly_sales_test_detail3BindingSource.EndEdit();
                    this.ly_sales_test_detail3TableAdapter.Update(this.lYSalseMange.ly_sales_test_detail3);

                    SQLDatabase.dataChangeREC(this.ly_sales_testDataGridView.CurrentRow.Cells["record_id"].Value.ToString(), "ly_sales_test", "UPD", this.Text);
                    //CountPlanStru();

                }
                else
                {

                }
                return;
            }
        }
    }
}