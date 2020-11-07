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
    public partial class LY_Salescontract_Deliver : Form
    {

        private int selectionIdx = 0;
        //private string formState;
        //private int nowRow;
       
        private string nowusercode = "";
       
       
        private string nowinnerCode = "";
        private string nowcontractCode = "";



        public LY_Salescontract_Deliver()
        {
            InitializeComponent();
        }


        public void Find_planlocation(string nowplannum, string ifActivate)
        {

            //if ("yes" == ifActivate)
            //{
            this.ly_material_plan_mainperiodproTableAdapter.Fill(this.lYSalseMange.ly_material_plan_mainperiodpro, "LSPT", this.dateTimePicker1.Value, this.dateTimePicker2.Value);
            //}

            this.ly_material_plan_mainperiodproBindingSource.Position = this.ly_material_plan_mainperiodproBindingSource.Find("计划编号", nowplannum);



        }
       
        private void Yonghu_Load(object sender, EventArgs e)
        {
            
            
       

            this.ly_sales_contract_main_forbusinessTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            this.ly_sales_contract_detailTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            
            this.ly_sales_groupTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
     

           
            this.f_PlanExtend_LSPTTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;

            this.ly_plan_getmaterial_departmentTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;

            this.ly_sales_testTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            this.ly_sales_test_detail2TableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;

            this.ly_sales_deliverTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            this.ly_sales_deliver_detailTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;


            this.ly_plan_getmaterial_deliverTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;

         
          
       

            this.nowusercode = SQLDatabase.NowUserID;
            
          

            

            this.dateTimePicker1.Text = DateTime.Today.AddMonths(-6).Date.ToString();
            this.dateTimePicker2.Text = DateTime.Today.AddDays(1).Date.ToString();


            this.ly_material_plan_mainperiodproBindingSource.Filter = "出库指令=1";
            this.ly_material_plan_mainperiodproTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            this.ly_material_plan_mainperiodproTableAdapter.Fill(this.lYSalseMange.ly_material_plan_mainperiodpro, "LSPT", this.dateTimePicker1.Value, this.dateTimePicker2.Value);


            

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


            this.ly_material_plan_mainperiodproBindingSource.Filter = filterString;
        }

        private void toolStripTextBox5_Enter(object sender, EventArgs e)
        {
            toolStripTextBox5.Text = "";

            this.ly_material_plan_mainperiodproBindingSource.Filter = "";
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

        }

        private void toolStripButton28_Click(object sender, EventArgs e)
        {
            this.ly_material_plan_mainperiodproTableAdapter.Fill(this.lYSalseMange.ly_material_plan_mainperiodpro, "LSPT", this.dateTimePicker1.Value, this.dateTimePicker2.Value);
        }

        private void treeView2_AfterSelect(object sender, TreeViewEventArgs e)
        {


            if (e.Node.Level < 1)
            {
                //this.ly_plan_getmaterial1TableAdapter.Fill(this.lYSalseMange.ly_plan_getmaterial1, "", "", 0);
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
                //this.ly_plan_getmaterial1TableAdapter.Fill(this.lYSalseMange.ly_plan_getmaterial1, nowplannum, nowgroupnum, nowparentid);


                this.ly_sales_testTableAdapter.Fill(this.lYSalseMange.ly_sales_test, 0 - nowparentid);
                this.ly_sales_deliverTableAdapter.Fill(this.lYSalseMange.ly_sales_deliver, 0 - nowparentid);
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

            //this.ly_plan_getmaterial1TableAdapter.Fill(this.lYSalseMange.ly_plan_getmaterial1, nowplannum, nowgroupnum, int.Parse (nowNodeTag));

            treeView2.AfterSelect += treeView2_AfterSelect;
        }

        private void ly_material_plan_mainDataGridView_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            DataGridView dgv = sender as DataGridView;

            if (null == dgv.CurrentRow) return;
           
            ///////////////////////////////////////////////////////////
            if ("营业发货0" == dgv.CurrentCell.OwningColumn.Name)
            {

                string nowplannum = dgv.CurrentRow.Cells["计划编号0"].Value.ToString();

               

                if ("True" == dgv.CurrentRow.Cells["营业发货0"].Value.ToString())
                {
                    if (dgv.CurrentRow.Cells["inner_flag"].Value.ToString() == "True")
                    {
                        MessageBox.Show("已经申请开票无法修改...", "注意");
                        return;
                    }


                    //if (SQLDatabase.updateMainplan(nowplannum, "营业发货", "0", SQLDatabase.nowUserName()))
                    //{
                    //    dgv.CurrentRow.Cells["营业发货0"].Value = "False";
                    //    dgv.CurrentRow.Cells["实发日期"].Value = DBNull.Value;
                    //}
                    MessageBox.Show("已经操作，无法修改...", "注意");
                    return;
                }
                else
                {
                    string id = dgv.CurrentRow.Cells["idDataGridViewTextBoxColumn"].Value.ToString();
                    this.ly_material_plan_mainperiodproTableAdapter.Fill(this.lYSalseMange.ly_material_plan_mainperiodpro, "LSPT", this.dateTimePicker1.Value, this.dateTimePicker2.Value);
                    if (id != "")
                    {
                        this.ly_material_plan_mainperiodproBindingSource.Position = this.ly_material_plan_mainperiodproBindingSource.Find("id", id);
                    }

                    if ("True" != ly_material_plan_mainDataGridView.CurrentRow.Cells["出库指令0"].Value.ToString())
                    {
                        MessageBox.Show("无出库指令,不能设置发货标记...", "注意");
                        return;

                    }
                  
                    //判断是否是直发
                    string billcode = ly_material_plan_mainDataGridView.CurrentRow.Cells["计划编号0"].Value.ToString();
                    DataTable dt_newqu = null;
                         string sqlqu = @" select a.out_bill_code,isnull(a.directrun_flag,0)as directrun_flag from (SELECT contract_inner_code, out_bill_code, approve, approve_people, 
                               approve_date, remark, inner_flag, directrun_flag
                         FROM ly_sales_contract_main
                         UNION
                         SELECT borrow_code AS contract_inner_code, out_bill_code, out_approve,
                               out_approve_people, out_approve_date, remark, 0 AS inner_flag,
                               1 AS directrun_flag
                         FROM ly_sales_borrow) a

                         where a.out_bill_code = '"+ billcode + "'";
                    using (SqlConnection connection = new SqlConnection(SQLDatabase.Connectstring))
                    {

                        SqlDataAdapter adapter = new SqlDataAdapter(sqlqu, connection);
                        DataSet ds = new DataSet();
                        adapter.Fill(ds);
                        dt_newqu = ds.Tables[0];
                    }
                    if (dt_newqu.Rows.Count > 0)
                    {
                        if (dt_newqu.Rows[0]["directrun_flag"].ToString() == "1")
                        {

                        }
                        else
                        {
                            if ("True" != ly_material_plan_mainDataGridView.CurrentRow.Cells["质检配调0"].Value.ToString())
                            {
                                MessageBox.Show("无配调质检,不能设置发货标记...", "注意");
                                return;

                            }
                        }
                    }



                    if (SQLDatabase.updateMainplan(nowplannum, "营业发货", "1", SQLDatabase.nowUserName()))
                    {
                        dgv.CurrentRow.Cells["营业发货0"].Value = "True";
                        dgv.CurrentRow.Cells["实发日期"].Value =SQLDatabase.GetNowdate();
                    }
                }



                this.ly_material_plan_mainperiodproBindingSource.EndEdit();
               // this.ly_material_plan_mainperiodTableAdapter.Update(this.lYSalseMange.ly_material_plan_mainperiod);



                return;

            }
            ///////////////////////////////////////////////////////////////

            ///////////////////////////////////////////////////////////
            if ("发货备注" == dgv.CurrentCell.OwningColumn.Name)
            {

                string nowplannum = dgv.CurrentRow.Cells["计划编号0"].Value.ToString();




                ChangeValue queryForm = new ChangeValue();

                queryForm.OldValue = dgv.CurrentCell.Value.ToString();
                queryForm.NewValue = "";
                queryForm.ChangeMode = "longstring";
                queryForm.ShowDialog();




                if (queryForm.NewValue != "")
                {
                    if (SQLDatabase.updateMainplan(nowplannum, "发货备注", queryForm.NewValue, SQLDatabase.nowUserName()))
                    {
                        dgv.CurrentRow.Cells["发货备注"].Value =queryForm.OldValue + queryForm.NewValue;
                    }
                   

                }
                else
                {

                }
             
                return;

            }
            /////////////////////////////////////////////////////////////////
            
        }

        private string GetMaxDeliverNum()
        {
            SqlConnection sqlConnection1 = new SqlConnection(SQLDatabase.Connectstring);
            SqlCommand cmd = new SqlCommand();

            string MaxProductionorder = "";

            cmd.Parameters.Add("@deliver_mode", SqlDbType.VarChar);
            cmd.Parameters["@deliver_mode"].Value = "YY";


            cmd.CommandText = "LY_GetMax_Deliver_Num";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = sqlConnection1;

            sqlConnection1.Open();
            MaxProductionorder = cmd.ExecuteScalar().ToString();
            sqlConnection1.Close();



            return MaxProductionorder;
        }

        private void 增加配调检验记录ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (null == ly_sales_test_detail2DataGridView.CurrentRow) return;

            string message = "增加营业发货记录吗？";
            string caption = "提示...";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;

            DialogResult result;



            result = MessageBox.Show(message, caption, buttons,
            MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

            if (result == DialogResult.Yes)
            {
                this.ly_sales_deliverBindingSource.AddNew();

                this.ly_sales_deliverDataGridView.CurrentRow.Cells["发货单号5"].Value = GetMaxDeliverNum();

                this.ly_sales_deliverDataGridView.CurrentRow.Cells["发货日期5"].Value = SQLDatabase.GetNowdate().ToString(); ;

                this.ly_sales_deliverDataGridView.CurrentRow.Cells["经办人5"].Value = SQLDatabase.nowUserName();

                this.ly_sales_deliverDataGridView.CurrentRow.Cells["收件人5"].Value = this.ly_sales_groupDataGridView.CurrentRow.Cells["收件人"].Value;
                this.ly_sales_deliverDataGridView.CurrentRow.Cells["邮编5"].Value = this.ly_sales_groupDataGridView.CurrentRow.Cells["邮编"].Value;
                this.ly_sales_deliverDataGridView.CurrentRow.Cells["收件地址5"].Value = this.ly_sales_groupDataGridView.CurrentRow.Cells["地址"].Value;
                this.ly_sales_deliverDataGridView.CurrentRow.Cells["客户电话5"].Value = this.ly_sales_groupDataGridView.CurrentRow.Cells["电话"].Value;




                int parentId = int.Parse(this.ly_sales_groupDataGridView.CurrentRow.Cells["group_id"].Value.ToString());
                this.ly_sales_deliverDataGridView.CurrentRow.Cells["group_id5"].Value = parentId;

                this.ly_sales_deliverBindingSource.EndEdit();
                this.ly_sales_deliverTableAdapter.Update(this.lYSalseMange.ly_sales_deliver);

             
               
              

            }
        }

        private void 增加检验记录明细ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // if (null == ly_sales_testDataGridView.CurrentRow) return;

            //string message = "增加检验记录明细吗？";
            //string caption = "提示...";
            //MessageBoxButtons buttons = MessageBoxButtons.YesNo;

            //DialogResult result;



            //result = MessageBox.Show(message, caption, buttons,
            //MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

            //if (result == DialogResult.Yes)
            //{
            //    this.ly_sales_test_detailBindingSource.AddNew();

            //    //this.ly_sales_test_detailDataGridView.CurrentRow.Cells["配调记录"].Value = GetMaxRecordNum();

            //    //this.ly_sales_testDataGridView.CurrentRow.Cells["配调日期0"].Value = SQLDatabase.GetNowdate().ToString(); ;

            //    //this.ly_sales_testDataGridView.CurrentRow.Cells["配调人"].Value = SQLDatabase.nowUserName();

            //    int parentId = int.Parse(this.ly_sales_testDataGridView.CurrentRow.Cells["record_id"].Value.ToString());
            //    this.ly_sales_test_detailDataGridView.CurrentRow.Cells["record_id1"].Value = parentId;

            //    this.ly_sales_test_detailBindingSource.EndEdit();
            //    this.ly_sales_test_detailTableAdapter.Update(this.lYSalseMange.ly_sales_test_detail);
            //}
        }

        private void ly_sales_testDataGridView_SelectionChanged(object sender, EventArgs e)
        {
            if (null == ly_sales_testDataGridView.CurrentRow)
            {
                this.ly_sales_test_detail2TableAdapter.Fill(this.lYSalseMange.ly_sales_test_detail2, -1);
                return;
            }

            int parentId = int.Parse(this.ly_sales_testDataGridView.CurrentRow.Cells["record_id"].Value.ToString());

            this.ly_sales_test_detail2TableAdapter.Fill(this.lYSalseMange.ly_sales_test_detail2, parentId);


        }

        private void 删除记录明细ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (null == this.ly_sales_deliver_detailDataGridView.CurrentRow) return;

            if ("True" == ly_material_plan_mainDataGridView.CurrentRow.Cells["营业发货0"].Value.ToString())
            {
                MessageBox.Show("已经发货,不能删除数据...", "注意");
                return;

            }

            //string diaodu = this.ly_sales_deliverDataGridView.CurrentRow.Cells["经办人5"].Value.ToString();

            //if (diaodu != SQLDatabase.nowUserName())
            //{
            //    MessageBox.Show("请经办人:" + diaodu + "删除", "注意");
            //    return;
            //}

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

                this.ly_sales_deliver_detailBindingSource.RemoveCurrent();

                this.ly_sales_deliver_detailBindingSource.EndEdit();
                this.ly_sales_deliver_detailTableAdapter.Update(this.lYSalseMange.ly_sales_deliver_detail );

                int parentId = int.Parse(this.ly_sales_testDataGridView.CurrentRow.Cells["record_id"].Value.ToString());

                this.ly_sales_test_detail2TableAdapter.Fill(this.lYSalseMange.ly_sales_test_detail2, parentId);

                string nowplannum = ly_material_plan_mainDataGridView.CurrentRow.Cells["计划编号0"].Value.ToString();

                this.f_PlanExtend_LSPTTableAdapter.Fill(this.lYSalseMange.f_PlanExtend_LSPT, nowplannum);

             

                //int nowgroupid = int.Parse(ly_sales_groupDataGridView.CurrentRow.Cells["group_id"].Value.ToString());

                //string nowplannum = ly_material_plan_mainDataGridView.CurrentRow.Cells["计划编号0"].Value.ToString();
                //string nowcontractnum = ly_material_plan_mainDataGridView.CurrentRow.Cells["计划编号0"].Value.ToString();
                //string nowgroupnum = ly_sales_groupDataGridView.CurrentRow.Cells["配套编码"].Value.ToString();

                //this.ly_plan_getmaterial1TableAdapter.Fill(this.lYSalseMange.ly_plan_getmaterial1, nowplannum, nowgroupnum, 0 - nowgroupid);


                ////////////////////////////

                int nowgroupid = 0;
                string nowgroupnum = "";

               
                string nowcontractnum = ly_material_plan_mainDataGridView.CurrentRow.Cells["计划编号0"].Value.ToString();

                if (null != ly_sales_groupDataGridView.CurrentRow)
                {
                    nowgroupnum = ly_sales_groupDataGridView.CurrentRow.Cells["配套编码"].Value.ToString();

                    nowgroupid = int.Parse(ly_sales_groupDataGridView.CurrentRow.Cells["group_id"].Value.ToString());

                    //this.ly_plan_getmaterial1TableAdapter.Fill(this.lYSalseMange.ly_plan_getmaterial1, nowplannum, nowgroupnum, 0 - nowgroupid);
                }



                this.ly_plan_getmaterial_deliverTableAdapter.Fill(this.lYSalseMange.ly_plan_getmaterial_deliver, nowplannum, nowgroupnum, nowgroupid);





            }
        }

        private void 删除ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (null == this.ly_sales_deliverDataGridView.CurrentRow) return;

            if ("True" == ly_material_plan_mainDataGridView.CurrentRow.Cells["营业发货0"].Value.ToString())
            {
                MessageBox.Show("已经发货,不能删除数据...", "注意");
                return;

            }

            //string diaodu = this.ly_sales_deliverDataGridView.CurrentRow.Cells["经办人5"].Value.ToString();

            //if (diaodu != SQLDatabase.nowUserName())
            //{
            //    MessageBox.Show("请经办人:" + diaodu + "删除", "注意");
            //    return;
            //}

            //if (ly_production_order_detailDataGridView.RowCount > 0)
            //{
            //    MessageBox.Show("跟单已有工序安排,删除所有工序安排后才能删除跟单...", "注意");
            //    return;

            //}

            //string nowproductionorder = this.ly_production_orderDataGridView.CurrentRow.Cells["跟单编号"].Value.ToString();


            string message1 = "当前(发货记录)将被删除，继续吗？";
            string caption1 = "提示...";
            MessageBoxButtons buttons1 = MessageBoxButtons.YesNo;

            DialogResult result1;



            result1 = MessageBox.Show(message1, caption1, buttons1,
            MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

            if (result1 == DialogResult.Yes)
            {


                this.ly_sales_deliverBindingSource.RemoveCurrent();

                this.ly_sales_deliverBindingSource.EndEdit();
                this.ly_sales_deliverTableAdapter.Update(this.lYSalseMange.ly_sales_deliver );

                int nowgroupid = int.Parse(ly_sales_groupDataGridView.CurrentRow.Cells["group_id"].Value.ToString());

                string nowplannum = ly_material_plan_mainDataGridView.CurrentRow.Cells["计划编号0"].Value.ToString();
                string nowcontractnum = ly_material_plan_mainDataGridView.CurrentRow.Cells["计划编号0"].Value.ToString();
                string nowgroupnum = ly_sales_groupDataGridView.CurrentRow.Cells["配套编码"].Value.ToString();

                //this.ly_plan_getmaterial1TableAdapter.Fill(this.lYSalseMange.ly_plan_getmaterial1, nowplannum, nowgroupnum, 0 - nowgroupid);

            }
        }

        private void ly_sales_groupDataGridView_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            
            int nowgroupid = 0;
            string nowgroupnum = "";

            string nowplannum = ly_material_plan_mainDataGridView.CurrentRow.Cells["计划编号0"].Value.ToString();
            string nowcontractnum = ly_material_plan_mainDataGridView.CurrentRow.Cells["计划编号0"].Value.ToString();

            if (null != ly_sales_groupDataGridView.Rows [e .RowIndex ])
            {
                 nowgroupnum = ly_sales_groupDataGridView.Rows [e .RowIndex ].Cells["配套编码"].Value.ToString();

                 nowgroupid = int.Parse(ly_sales_groupDataGridView.Rows[e.RowIndex].Cells["group_id"].Value.ToString());

                //this.ly_plan_getmaterial1TableAdapter.Fill(this.lYSalseMange.ly_plan_getmaterial1, nowplannum, nowgroupnum, 0 - nowgroupid);
            }

            this.ly_sales_testTableAdapter.Fill(this.lYSalseMange.ly_sales_test, nowgroupid);
            this.ly_sales_deliverTableAdapter.Fill(this.lYSalseMange.ly_sales_deliver , nowgroupid);

            this.ly_plan_getmaterial_deliverTableAdapter.Fill(this.lYSalseMange.ly_plan_getmaterial_deliver, nowplannum, nowgroupnum, nowgroupid);


            
        }

       

        private void ly_sales_testDataGridView_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            //DataGridView dgv = sender as DataGridView;

            //if (null == dgv.CurrentRow) return;

            //////////////////////////////////////////////////////////////////////////


            //if ("配调日期0" == dgv.CurrentCell.OwningColumn.Name)
            //{

            //    ChangeValue queryForm = new ChangeValue();

            //    queryForm.OldValue = dgv.CurrentCell.Value.ToString();
            //    queryForm.NewValue = "";
            //    queryForm.ChangeMode = "datetime";
            //    queryForm.ShowDialog();




            //    if (queryForm.NewValue != "")
            //    {
            //        dgv.CurrentRow.Cells["配调日期0"].Value = queryForm.NewValue;
            //        //dgv.CurrentRow.Cells["discount_money"].Value = DBNull.Value;

            //        //dgv.CurrentRow.Cells["approve_flag"].Value = "False";
            //        this.ly_sales_testBindingSource.EndEdit();
            //        this.ly_sales_testTableAdapter.Update(this.lYSalseMange.ly_sales_test);


            //        //CountPlanStru();

            //    }
            //    else
            //    {


            //    }
            //    return;

            //}

            ///////////////////////////////

        }

        private void ly_sales_test_detailDataGridView_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            DataGridView dgv = sender as DataGridView;

            if (null == dgv.CurrentRow) return;
            if (null == ly_sales_deliverDataGridView.CurrentRow) return;

            if ("True" == this.ly_sales_test_detail2DataGridView.CurrentRow.Cells["已排"].Value.ToString())
            {

                return;
            }


            string machineNum = this.ly_sales_test_detail2DataGridView.CurrentRow.Cells["出厂编码"].Value.ToString ();

            if ("" == machineNum)
            {
                MessageBox.Show("产品无出厂编码,不能出库...","注意");
                return;
            }
            

            this. ly_sales_deliver_detailBindingSource.AddNew();

            this.ly_sales_deliver_detailDataGridView.CurrentRow.Cells["deliver_id6"].Value = this.ly_sales_deliverDataGridView.CurrentRow.Cells["deliver_id"].Value;

            this.ly_sales_deliver_detailDataGridView.CurrentRow.Cells["test_detail_id6"].Value = this.ly_sales_test_detail2DataGridView.CurrentRow.Cells["test_detail_id"].Value;

            this.ly_sales_deliver_detailDataGridView.CurrentRow.Cells["序号6"].Value = this.ly_sales_deliver_detailDataGridView.RowCount;

            this.ly_sales_deliver_detailDataGridView.CurrentRow.Cells["物料编码6"].Value = this.ly_sales_test_detail2DataGridView.CurrentRow.Cells["产品编码"].Value;
            this.ly_sales_deliver_detailDataGridView.CurrentRow.Cells["出厂编码6"].Value = this.ly_sales_test_detail2DataGridView.CurrentRow.Cells["出厂编码"].Value;
            this.ly_sales_deliver_detailDataGridView.CurrentRow.Cells["发货数量6"].Value = 1;
            //this.ly_sales_deliver_detailDataGridView.CurrentRow.Cells["备注6"].Value = this.ly_sales_groupDataGridView.CurrentRow.Cells["电话"].Value;




            //int parentId = int.Parse(this.ly_sales_groupDataGridView.CurrentRow.Cells["group_id"].Value.ToString());
            //this.ly_sales_deliverDataGridView.CurrentRow.Cells["group_id5"].Value = parentId;

            this.ly_sales_deliver_detailBindingSource.EndEdit();
            this.ly_sales_deliver_detailTableAdapter.Update(this.lYSalseMange.ly_sales_deliver_detail);

            int parentId = int.Parse(this.ly_sales_testDataGridView.CurrentRow.Cells["record_id"].Value.ToString());

            this.ly_sales_test_detail2TableAdapter.Fill(this.lYSalseMange.ly_sales_test_detail2, parentId);


            ///////////////////////////////
            //if ("备注" == dgv.CurrentCell.OwningColumn.Name)
            //{

            //    ChangeValue queryForm = new ChangeValue();

            //    queryForm.OldValue = dgv.CurrentCell.Value.ToString();
            //    queryForm.NewValue = "";
            //    queryForm.ChangeMode = "longstring";
            //    queryForm.ShowDialog();




            //    if (queryForm.NewValue != "")
            //    {
            //        dgv.CurrentRow.Cells["备注"].Value = queryForm.NewValue;
            //        //dgv.CurrentRow.Cells["discount_money"].Value = DBNull.Value;

            //        //dgv.CurrentRow.Cells["approve_flag"].Value = "False";
            //        this.ly_sales_test_detailBindingSource.EndEdit();
            //        this.ly_sales_test_detailTableAdapter.Update(this.lYSalseMange.ly_sales_test_detail);

            //        //CountPlanStru();

            //    }
            //    else
            //    {

            //    }
            //    return;

            //}


            /////////////////////////////////////////////////////////

            //if ("安装类型" == dgv.CurrentCell.OwningColumn.Name)
            //{

            //    ChangeValue queryForm = new ChangeValue();

            //    queryForm.OldValue = dgv.CurrentCell.Value.ToString();
            //    queryForm.NewValue = "";
            //    queryForm.ChangeMode = "string";
            //    queryForm.ShowDialog();




            //    if (queryForm.NewValue != "")
            //    {
            //        dgv.CurrentRow.Cells["安装类型"].Value = queryForm.NewValue;
            //        //dgv.CurrentRow.Cells["discount_money"].Value = DBNull.Value;

            //        //dgv.CurrentRow.Cells["approve_flag"].Value = "False";
            //        this.ly_sales_test_detailBindingSource.EndEdit();
            //        this.ly_sales_test_detailTableAdapter.Update(this.lYSalseMange.ly_sales_test_detail);

            //        //CountPlanStru();

            //    }
            //    else
            //    {

            //    }
            //    return;

            //}


            /////////////////////////////////////////////////////////
            //if ("油缸编号" == dgv.CurrentCell.OwningColumn.Name)
            //{

            //    ChangeValue queryForm = new ChangeValue();

            //    queryForm.OldValue = dgv.CurrentCell.Value.ToString();
            //    queryForm.NewValue = "";
            //    queryForm.ChangeMode = "string";
            //    queryForm.ShowDialog();




            //    if (queryForm.NewValue != "")
            //    {
            //        dgv.CurrentRow.Cells["油缸编号"].Value = queryForm.NewValue;
            //        //dgv.CurrentRow.Cells["discount_money"].Value = DBNull.Value;

            //        //dgv.CurrentRow.Cells["approve_flag"].Value = "False";
            //        this.ly_sales_test_detailBindingSource.EndEdit();
            //        this.ly_sales_test_detailTableAdapter.Update(this.lYSalseMange.ly_sales_test_detail);

            //        //CountPlanStru();

            //    }
            //    else
            //    {

            //    }
            //    return;

            //}


            /////////////////////////////////////////////////////////
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
            //        this.ly_sales_test_detailBindingSource.EndEdit();
            //        this.ly_sales_test_detailTableAdapter.Update(this.lYSalseMange.ly_sales_test_detail);

            //        //CountPlanStru();

            //    }
            //    else
            //    {

            //    }
            //    return;

            //}


            /////////////////////////////////////////////////////////

            //if ("装置出厂编号" == dgv.CurrentCell.OwningColumn.Name)
            //{

            //    ChangeValue queryForm = new ChangeValue();

            //    queryForm.OldValue = dgv.CurrentCell.Value.ToString();
            //    queryForm.NewValue = "";
            //    queryForm.ChangeMode = "string";
            //    queryForm.ShowDialog();




            //    if (queryForm.NewValue != "")
            //    {
            //        dgv.CurrentRow.Cells["装置出厂编号"].Value = queryForm.NewValue;
            //        //dgv.CurrentRow.Cells["discount_money"].Value = DBNull.Value;

            //        //dgv.CurrentRow.Cells["approve_flag"].Value = "False";
            //        this.ly_sales_test_detailBindingSource.EndEdit();
            //        this.ly_sales_test_detailTableAdapter.Update(this.lYSalseMange.ly_sales_test_detail);

            //        //CountPlanStru();

            //    }
            //    else
            //    {

            //    }
            //    return;

            //}


            /////////////////////////////////////////////////////////
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
            //        this.ly_sales_test_detailBindingSource.EndEdit();
            //        this.ly_sales_test_detailTableAdapter.Update(this.lYSalseMange.ly_sales_test_detail);

            //        //CountPlanStru();

            //    }
            //    else
            //    {

            //    }
            //    return;

            //}


            /////////////////////////////////////////////////////////
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
            //        this.ly_sales_test_detailBindingSource.EndEdit();
            //        this.ly_sales_test_detailTableAdapter.Update(this.lYSalseMange.ly_sales_test_detail);

            //        //CountPlanStru();

            //    }
            //    else
            //    {

            //    }
            //    return;

            //}


            /////////////////////////////////////////////////////////
            //if ("DIS_SN设定" == dgv.CurrentCell.OwningColumn.Name)
            //{

            //    ChangeValue queryForm = new ChangeValue();

            //    queryForm.OldValue = dgv.CurrentCell.Value.ToString();
            //    queryForm.NewValue = "";
            //    queryForm.ChangeMode = "string";
            //    queryForm.ShowDialog();




            //    if (queryForm.NewValue != "")
            //    {
            //        dgv.CurrentRow.Cells["DIS_SN设定"].Value = queryForm.NewValue;
            //        //dgv.CurrentRow.Cells["discount_money"].Value = DBNull.Value;

            //        //dgv.CurrentRow.Cells["approve_flag"].Value = "False";
            //        this.ly_sales_test_detailBindingSource.EndEdit();
            //        this.ly_sales_test_detailTableAdapter.Update(this.lYSalseMange.ly_sales_test_detail);

            //        //CountPlanStru();

            //    }
            //    else
            //    {

            //    }
            //    return;

            //}


            /////////////////////////////////////////////////////////
            //if ("控制仪出厂编号" == dgv.CurrentCell.OwningColumn.Name)
            //{

            //    ChangeValue queryForm = new ChangeValue();

            //    queryForm.OldValue = dgv.CurrentCell.Value.ToString();
            //    queryForm.NewValue = "";
            //    queryForm.ChangeMode = "string";
            //    queryForm.ShowDialog();




            //    if (queryForm.NewValue != "")
            //    {
            //        dgv.CurrentRow.Cells["控制仪出厂编号"].Value = queryForm.NewValue;
            //        //dgv.CurrentRow.Cells["discount_money"].Value = DBNull.Value;

            //        //dgv.CurrentRow.Cells["approve_flag"].Value = "False";
            //        this.ly_sales_test_detailBindingSource.EndEdit();
            //        this.ly_sales_test_detailTableAdapter.Update(this.lYSalseMange.ly_sales_test_detail);

            //        //CountPlanStru();

            //    }
            //    else
            //    {

            //    }
            //    return;

            //}


            /////////////////////////////////////////////////////////

        }

        private void toolStripButton1_Click_1(object sender, EventArgs e)
        {
            LY_Salescontract_DeliverQuery queryForm = new LY_Salescontract_DeliverQuery();



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


        public void find_NowProduc(string nowplan, string nowgroup, string nowdelivercode, int nowid)
        {



            this.ly_material_plan_mainperiodproBindingSource.Position = this.ly_material_plan_mainperiodproBindingSource.Find("计划编号", nowplan);

            this.ly_sales_groupBindingSource.Position = this.ly_sales_groupBindingSource.Find("配套编码", nowgroup);

            this.ly_sales_deliverBindingSource.Position = this.ly_sales_deliverBindingSource.Find("发货单号", nowdelivercode);

            this.ly_sales_deliver_detailBindingSource.Position = this.ly_sales_deliver_detailBindingSource.Find("id", nowid);



            
          
             

            
            
        }

        private void ly_sales_deliverDataGridView_SelectionChanged(object sender, EventArgs e)
        {
            if (null == ly_sales_deliverDataGridView.CurrentRow)
            {
                this.ly_sales_deliver_detailTableAdapter.Fill(this.lYSalseMange.ly_sales_deliver_detail, -1);
                return;
            }

            int parentId = int.Parse(this.ly_sales_deliverDataGridView.CurrentRow.Cells["deliver_id"].Value.ToString());

            this.ly_sales_deliver_detailTableAdapter.Fill(this.lYSalseMange.ly_sales_deliver_detail, parentId);
        }

        private void f_PlanExtend_LSPTDataGridView_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            DataGridView dgv = sender as DataGridView;

            if (null == dgv.CurrentRow) return;
            if (null == ly_sales_deliverDataGridView.CurrentRow) return;

            if (2 > int.Parse (this.f_PlanExtend_LSPTDataGridView.CurrentRow.Cells["层次3"].Value.ToString()))
            {

                return;
            }

            if (1 > decimal.Parse(this.f_PlanExtend_LSPTDataGridView.CurrentRow.Cells["未排3"].Value.ToString()))
            {

                return;
            }

            string nowitemno = this.f_PlanExtend_LSPTDataGridView.CurrentRow.Cells["产品编码3"].Value.ToString();

            int hadarrenged = this.ly_sales_deliver_detailBindingSource.Find("物料编码", nowitemno);
            this.ly_sales_deliver_detailBindingSource.Position = hadarrenged;

            if (0 <= hadarrenged)
            {
                decimal  nowqty = decimal.Parse (this.ly_sales_deliver_detailDataGridView.Rows[hadarrenged].Cells["发货数量6"].Value.ToString());

                nowqty = nowqty + 1;
                this.ly_sales_deliver_detailDataGridView.Rows[hadarrenged].Cells["发货数量6"].Value = nowqty;
            }
            else
            {

                this.ly_sales_deliver_detailBindingSource.AddNew();

                this.ly_sales_deliver_detailDataGridView.CurrentRow.Cells["deliver_id6"].Value = this.ly_sales_deliverDataGridView.CurrentRow.Cells["deliver_id"].Value;

                this.ly_sales_deliver_detailDataGridView.CurrentRow.Cells["test_detail_id6"].Value = this.f_PlanExtend_LSPTDataGridView.CurrentRow.Cells["real_id"].Value;

                this.ly_sales_deliver_detailDataGridView.CurrentRow.Cells["序号6"].Value = this.ly_sales_deliver_detailDataGridView.RowCount;

                this.ly_sales_deliver_detailDataGridView.CurrentRow.Cells["物料编码6"].Value = this.f_PlanExtend_LSPTDataGridView.CurrentRow.Cells["产品编码3"].Value;
                //this.ly_sales_deliver_detailDataGridView.CurrentRow.Cells["出厂编码6"].Value = this.ly_sales_test_detail2DataGridView.CurrentRow.Cells["出厂编码"].Value;
                this.ly_sales_deliver_detailDataGridView.CurrentRow.Cells["发货数量6"].Value = 1;
                //this.ly_sales_deliver_detailDataGridView.CurrentRow.Cells["备注6"].Value = this.ly_sales_groupDataGridView.CurrentRow.Cells["电话"].Value;

            }


            //int parentId = int.Parse(this.ly_sales_groupDataGridView.CurrentRow.Cells["group_id"].Value.ToString());
            //this.ly_sales_deliverDataGridView.CurrentRow.Cells["group_id5"].Value = parentId;

            this.ly_sales_deliver_detailBindingSource.EndEdit();
            this.ly_sales_deliver_detailTableAdapter.Update(this.lYSalseMange.ly_sales_deliver_detail);


             string nowplannum = ly_material_plan_mainDataGridView.CurrentRow.Cells["计划编号0"].Value.ToString();

             this.f_PlanExtend_LSPTTableAdapter.Fill(this.lYSalseMange.f_PlanExtend_LSPT, nowplannum);

            //int parentId = int.Parse(this.ly_sales_testDataGridView.CurrentRow.Cells["record_id"].Value.ToString());

            //this.ly_sales_test_detail2TableAdapter.Fill(this.lYSalseMange.ly_sales_test_detail2, parentId);
        }

        private void ly_sales_deliver_detailDataGridView_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex >= 0)
                selectionIdx = e.RowIndex;
        }

        private void ly_sales_deliver_detailDataGridView_CellMouseMove(object sender, DataGridViewCellMouseEventArgs e)
        {
            DataGridView dgv = sender as DataGridView;

            if ((e.Clicks < 2) && (e.Button == MouseButtons.Left))
            {
                if ((e.ColumnIndex == -1) && (e.RowIndex > -1))
                    dgv.DoDragDrop(dgv.Rows[e.RowIndex], DragDropEffects.Move);
            } 
        }
        private int GetRowFromPoint(DataGridView dgv, int x, int y)
        {
            for (int i = 0; i < dgv.RowCount; i++)
            {
                Rectangle rec = dgv.GetRowDisplayRectangle(i, false);

                if (dgv.RectangleToScreen(rec).Contains(x, y))
                    return i;
            }

            return -1;
        }
        private void ly_sales_deliver_detailDataGridView_DragDrop(object sender, DragEventArgs e)
        {
            DataGridView dgv = sender as DataGridView;



            int idx = GetRowFromPoint(dgv, e.X, e.Y);
            if (idx < 0) return;
            //index2 = idx;
            if (e.Data.GetDataPresent(typeof(DataGridViewRow)))
            {

                DataGridViewRow row = (DataGridViewRow)e.Data.GetData(typeof(DataGridViewRow));

                int tempOrder = row.Index;
                // this.gqis.Ins_Incontrol(idx, row.Cells[0].Value.ToString());



                //dgv.Rows[idx].Cells["顺序"].Value = tempOrder;
                //dgv.Rows[idx].Cells["顺序"].Value = tempOrder;

                if (idx > row.Index)
                {
                    foreach (DataGridViewRow dgvr in dgv.Rows)
                    {

                        if (dgvr.Index > row.Index && dgvr.Index <= idx)
                        {
                            dgvr.Cells["序号6"].Value = dgvr.Index;

                        }
                    }
                }
                if (idx < row.Index)
                {
                    foreach (DataGridViewRow dgvr in dgv.Rows)
                    {

                        if (dgvr.Index >= idx && dgvr.Index < row.Index)
                        {
                            dgvr.Cells["序号6"].Value = dgvr.Index + 2;

                        }
                    }
                }


                row.Cells["序号6"].Value = idx + 1;
                // dgv.Rows[idx].Cells["顺序"].Value = row.Index + 1;

                SaveChanged();

               

                dgv.Rows[idx].Selected = true;
                dgv.CurrentCell = dgv.Rows[idx].Cells["序号6"];


                //selectionIdx = idx;
            } 
        }
        private void SaveChanged()
        {



            this.ly_sales_deliver_detailBindingSource.EndEdit();
            this.ly_sales_deliver_detailTableAdapter.Update(this.lYSalseMange.ly_sales_deliver_detail);


            int parentId = int.Parse(this.ly_sales_deliverDataGridView.CurrentRow.Cells["deliver_id"].Value.ToString());

            this.ly_sales_deliver_detailTableAdapter.Fill(this.lYSalseMange.ly_sales_deliver_detail, parentId);



            foreach (DataGridViewRow dgr in ly_sales_deliver_detailDataGridView.Rows)
            {
                dgr.Cells["序号6"].Value = dgr.Index + 1;

            }

            this.ly_sales_deliver_detailBindingSource.EndEdit();

            this.ly_sales_deliver_detailTableAdapter.Update(this.lYSalseMange.ly_sales_deliver_detail);
            this.ly_sales_deliver_detailTableAdapter.Fill(this.lYSalseMange.ly_sales_deliver_detail, parentId);
        }

        private void ly_sales_deliver_detailDataGridView_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move; 
        }

        private void ly_sales_deliverDataGridView_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            DataGridView dgv = sender as DataGridView;

            if (null == dgv.CurrentRow) return;


            /////////////////////////////////////////////////////////////////

            //if ("发货日期5" == dgv.CurrentCell.OwningColumn.Name)
            //{

            //    if ("True" == dgv.CurrentRow.Cells["已发5"].Value.ToString())
            //    {
            //        MessageBox.Show("已经发货,不能修改发货日期...", "注意");

            //        return;
            //    }

            //    ChangeValue queryForm = new ChangeValue();

            //    queryForm.OldValue = dgv.CurrentCell.Value.ToString();
            //    queryForm.NewValue = "";
            //    queryForm.ChangeMode = "datetime";
            //    queryForm.ShowDialog();


            //    if (queryForm.NewValue != "")
            //    {
            //        dgv.CurrentRow.Cells["合同文本交付"].Value = queryForm.NewValue;

            //    }
            //    else
            //    {

            //        dgv.CurrentRow.Cells["合同文本交付"].Value = DBNull.Value;

            //    }



            //    SaveContract();



            //    return;

            //}
            //////////////////////////////////////////////////////////////////////////





           

            ///////////////////////////////////////////////////////////
            if ("已发5" == dgv.CurrentCell.OwningColumn.Name)
            {





                if ("True" == dgv.CurrentRow.Cells["已发5"].Value.ToString())
                {
                    dgv.CurrentRow.Cells["已发5"].Value = "False";
                    dgv.CurrentRow.Cells["经办人5"].Value =DBNull.Value; 
                }
                else
                {

                    dgv.CurrentRow.Cells["已发5"].Value = "True";
                    dgv.CurrentRow.Cells["经办人5"].Value = SQLDatabase.nowUserName();
                }



                SaveContract();



                return;

            }
            ///////////////////////////////////////////////////////////////


            //if ("True" == dgv.CurrentRow.Cells["已发5"].Value.ToString())
            //{
            //    MessageBox.Show("已经发货,不能修改数据...", "注意");
            //    return;

            //}


          


            if ("发货日期5" == dgv.CurrentCell.OwningColumn.Name)
            {

                ChangeValue queryForm = new ChangeValue();

                queryForm.OldValue = dgv.CurrentCell.Value.ToString();
                queryForm.NewValue = "";
                queryForm.ChangeMode = "datetime";
                queryForm.ShowDialog();


                if (queryForm.NewValue != "")
                {
                    dgv.CurrentRow.Cells["发货日期5"].Value = queryForm.NewValue;
                    dgv.CurrentRow.Cells["经办人5"].Value = SQLDatabase.nowUserName();
                }
                else
                {

                    dgv.CurrentRow.Cells["发货日期5"].Value = DBNull.Value;
                    dgv.CurrentRow.Cells["经办人5"].Value = DBNull.Value;
                }



                SaveContract();



                return;

            }
            ////////////////////////////////////////////////////////////////////////

            if ("客户名称5" == dgv.CurrentCell.OwningColumn.Name)
            {

                ChangeValue queryForm = new ChangeValue();

                queryForm.OldValue = dgv.CurrentCell.Value.ToString();
                queryForm.NewValue = "";
                queryForm.ChangeMode = "string";
                queryForm.ShowDialog();




                if (queryForm.NewValue != "")
                {
                    dgv.CurrentRow.Cells["客户名称5"].Value = queryForm.NewValue;
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


            /////////////////////////////
            if ("收件人5" == dgv.CurrentCell.OwningColumn.Name)
            {

                ChangeValue queryForm = new ChangeValue();

                queryForm.OldValue = dgv.CurrentCell.Value.ToString();
                queryForm.NewValue = "";
                queryForm.ChangeMode = "string";
                queryForm.ShowDialog();




                if (queryForm.NewValue != "")
                {
                    dgv.CurrentRow.Cells["收件人5"].Value = queryForm.NewValue;
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
            if ("邮编5" == dgv.CurrentCell.OwningColumn.Name)
            {

                ChangeValue queryForm = new ChangeValue();

                queryForm.OldValue = dgv.CurrentCell.Value.ToString();
                queryForm.NewValue = "";
                queryForm.ChangeMode = "string";
                queryForm.ShowDialog();




                if (queryForm.NewValue != "")
                {
                    dgv.CurrentRow.Cells["邮编5"].Value = queryForm.NewValue;
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
            if ("客户电话5" == dgv.CurrentCell.OwningColumn.Name)
            {

                ChangeValue queryForm = new ChangeValue();

                queryForm.OldValue = dgv.CurrentCell.Value.ToString();
                queryForm.NewValue = "";
                queryForm.ChangeMode = "string";
                queryForm.ShowDialog();




                if (queryForm.NewValue != "")
                {
                    dgv.CurrentRow.Cells["客户电话5"].Value = queryForm.NewValue;
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
            if ("收件地址5" == dgv.CurrentCell.OwningColumn.Name)
            {

                ChangeValue queryForm = new ChangeValue();

                queryForm.OldValue = dgv.CurrentCell.Value.ToString();
                queryForm.NewValue = "";
                queryForm.ChangeMode = "longstring";
                queryForm.ShowDialog();




                if (queryForm.NewValue != "")
                {
                    dgv.CurrentRow.Cells["收件地址5"].Value = queryForm.NewValue;
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


            /////////////////////////////////////////////////////////
            if ("快递单号5" == dgv.CurrentCell.OwningColumn.Name)
            {

                ChangeValue queryForm = new ChangeValue();

                queryForm.OldValue = dgv.CurrentCell.Value.ToString();
                queryForm.NewValue = "";
                queryForm.ChangeMode = "string";
                queryForm.ShowDialog();




                if (queryForm.NewValue != "")
                {
                    dgv.CurrentRow.Cells["快递单号5"].Value = queryForm.NewValue;
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


            /////////////////////////////////////////////////////////

            //if ("类别" == dgv.CurrentCell.OwningColumn.Name)
            //{






            //    string sel;



            //    sel = "SELECT  class_code as 编码, class_name as 名称 FROM ly_sales_contract_class ";



            //    QueryForm queryForm = new QueryForm();


            //    queryForm.Sel = sel;
            //    queryForm.Constr = SQLDatabase.Connectstring;

            //    //Set the Column Collection to the filter Table
            //    //queryForm.SetSourceColumns(this.billMainDataSet.BalanceBill.Columns);

            //    queryForm.ShowDialog();


            //    if (queryForm.Result != "")
            //    {
            //        dgv.CurrentRow.Cells["类别码"].Value = queryForm.Result;
            //        //dgv.CurrentRow.Cells["discount_money"].Value = DBNull.Value;

            //        //dgv.CurrentRow.Cells["approve_flag"].Value = "False";
            //        SaveContract();


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


            ///////////////////////////////////////////////////////////

            //if ("属性" == dgv.CurrentCell.OwningColumn.Name)
            //{






            //    string sel;



            //    sel = "SELECT  style_code as 编码, style_name as 名称 FROM ly_sales_contract_style ";



            //    QueryForm queryForm = new QueryForm();


            //    queryForm.Sel = sel;
            //    queryForm.Constr = SQLDatabase.Connectstring;

            //    //Set the Column Collection to the filter Table
            //    //queryForm.SetSourceColumns(this.billMainDataSet.BalanceBill.Columns);

            //    queryForm.ShowDialog();


            //    if (queryForm.Result != "")
            //    {
            //        dgv.CurrentRow.Cells["属性码"].Value = queryForm.Result;
            //        //dgv.CurrentRow.Cells["discount_money"].Value = DBNull.Value;

            //        //dgv.CurrentRow.Cells["approve_flag"].Value = "False";
            //        SaveContract();


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


            /////////////////////////////////////////////////////////
            if ("快递公司5" == dgv.CurrentCell.OwningColumn.Name)
            {






                string sel;



                sel = "SELECT  express_name as 快递公司, express_people as 快递员,express_phone as 快递电话 FROM ly_express_company ";



                QueryForm queryForm = new QueryForm();


                queryForm.Sel = sel;
                queryForm.Constr = SQLDatabase.Connectstring;

                //Set the Column Collection to the filter Table
                //queryForm.SetSourceColumns(this.billMainDataSet.BalanceBill.Columns);

                queryForm.ShowDialog();


                if (queryForm.Result != "")
                {
                    dgv.CurrentRow.Cells["快递公司5"].Value = queryForm.Result;
                    dgv.CurrentRow.Cells["快递员5"].Value = queryForm.Result1;
                    dgv.CurrentRow.Cells["快递电话5"].Value = queryForm.Result2;
                    //dgv.CurrentRow.Cells["discount_money"].Value = DBNull.Value;

                    //dgv.CurrentRow.Cells["approve_flag"].Value = "False";
                    SaveContract();


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


            /////////////////////////////////////////////////////////

            //if ("合同编码" == dgv.CurrentCell.OwningColumn.Name)
            //{


            //    ChangeValue queryForm = new ChangeValue();

            //    queryForm.OldValue = dgv.CurrentCell.Value.ToString();
            //    queryForm.NewValue = "";
            //    queryForm.ChangeMode = "string";
            //    queryForm.ShowDialog();




            //    if (queryForm.NewValue != "")
            //    {

            //        dgv.CurrentRow.Cells["合同编码"].Value = queryForm.NewValue;

            //        int main_Id = int.Parse(dgv.CurrentRow.Cells["id_main"].Value.ToString());



            //        //string insstr = " delete ly_material_plan_main  where material_plan_num = '" + nowPlanNumber + "'";

            //        string updstr = " update ly_sales_contract_main  " +
            //                "  set contract_code=  '" + queryForm.NewValue + "' where  id=" + main_Id.ToString();


            //        SqlConnection sqlConnection1 = new SqlConnection(SQLDatabase.Connectstring);
            //        SqlCommand cmd = new SqlCommand();

            //        cmd.CommandText = updstr;
            //        cmd.CommandType = CommandType.Text;
            //        cmd.Connection = sqlConnection1;

            //        int temp = 0;

            //        using (TransactionScope scope = new TransactionScope())
            //        {

            //            sqlConnection1.Open();
            //            try
            //            {

            //                cmd.ExecuteNonQuery();



            //                scope.Complete();



            //            }
            //            catch (SqlException sqle)
            //            {


            //                MessageBox.Show(sqle.Message.Split('*')[0]);
            //            }


            //            finally
            //            {
            //                sqlConnection1.Close();


            //            }
            //        }

            //    }



            //    return;

            //}
        }

        private void SaveContract()
        {
            this.ly_sales_deliverBindingSource.EndEdit();
            this.ly_sales_deliverTableAdapter.Update(this.lYSalseMange.ly_sales_deliver);

        }

        private void 快递公司设置ToolStripMenuItem_Click(object sender, EventArgs e)
        {

            //int index;
            //int upperBound;

         

            //Screen[] screens = Screen.AllScreens;
            //upperBound = screens.GetUpperBound(0);

            //for (index = 0; index <= upperBound; index++)
            //{



            //    MessageBox.Show("Device Name: " + screens[index].DeviceName + " Bounds: " + screens[index].Bounds.ToString() + " Type: " + screens[index].GetType().ToString() + " Working Area: " + screens[index].WorkingArea.ToString() + " Primary Screen: " + screens[index].Primary.ToString());
               
            //}


         
           
            LY_Express_Mange queryForm = new LY_Express_Mange();

            //queryForm.salesclient_code = "";
            //queryForm.runmode = "增加";


            queryForm.StartPosition = FormStartPosition.CenterParent;
            queryForm.ShowDialog();

        }

        private void ly_sales_deliver_detailDataGridView_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            DataGridView dgv = sender as DataGridView;

            if (null == dgv.CurrentRow) return;




          

          
            /////////////////////////////
            if ("备注6" == dgv.CurrentCell.OwningColumn.Name)
            {

                ChangeValue queryForm = new ChangeValue();

                queryForm.OldValue = dgv.CurrentCell.Value.ToString();
                queryForm.NewValue = "";
                queryForm.ChangeMode = "longstring";
                queryForm.ShowDialog();




                if (queryForm.NewValue != "")
                {
                    dgv.CurrentRow.Cells["备注6"].Value = queryForm.NewValue;
                    //dgv.CurrentRow.Cells["discount_money"].Value = DBNull.Value;

                    //dgv.CurrentRow.Cells["approve_flag"].Value = "False";
                    this.ly_sales_deliver_detailBindingSource.EndEdit();
                    this.ly_sales_deliver_detailTableAdapter.Update(this.lYSalseMange.ly_sales_deliver_detail);


                    //CountPlanStru();

                }
                else
                {

                }
                return;

            }

            /////////////////////////////////////
            /////////////////////////////
            if ("实发数量" == dgv.CurrentCell.OwningColumn.Name)
            {

                ChangeValue queryForm = new ChangeValue();

                queryForm.OldValue = dgv.CurrentCell.Value.ToString();
                queryForm.NewValue = "";
                queryForm.ChangeMode = "value";
                queryForm.ShowDialog();




                if (queryForm.NewValue != "")
                {
                    dgv.CurrentRow.Cells["实发数量"].Value = queryForm.NewValue;
                    //dgv.CurrentRow.Cells["discount_money"].Value = DBNull.Value;

                    //dgv.CurrentRow.Cells["approve_flag"].Value = "False";
                    this.ly_sales_deliver_detailBindingSource.EndEdit();
                    this.ly_sales_deliver_detailTableAdapter.Update(this.lYSalseMange.ly_sales_deliver_detail);


                    //CountPlanStru();

                }
                else
                {

                }
                return;

            }

            /////////////////////////////////////


        

        }

        private void 装箱单打印ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (null == this.ly_sales_deliver_detailDataGridView.CurrentRow) return;

          

            //NewFrm.Show(this); ;

            //this.ly_plan_getmaterialTableAdapter.Fill(this.lYPlanMange.ly_plan_getmaterial, parentId);

            BaseReportView queryForm = new BaseReportView();

            queryForm.Text = "中原精密营业发货装箱单";

            queryForm.Printdata = this.lYSalseMange;




            queryForm.PrintCrystalReport = new LY_YingyeHetong_FHZX();
           


           
           // NewFrm.Hide(this);

            queryForm.ShowDialog();
        }

        private void ly_plan_getmaterialDataGridView_CellMouseDoubleClick_1(object sender, DataGridViewCellMouseEventArgs e)
        {
            DataGridView dgv = sender as DataGridView;

            if (null == dgv.CurrentRow) return;
            if (null == ly_sales_deliverDataGridView.CurrentRow) return;

            //if ("True" == this.ly_sales_test_detail2DataGridView.CurrentRow.Cells["已排"].Value.ToString())
            //{

            //    return;
            //}


            //string machineNum = this.ly_sales_test_detail2DataGridView.CurrentRow.Cells["出厂编码"].Value.ToString();

            //if ("" == machineNum)
            //{
            //    MessageBox.Show("产品无出厂编码,不能出库...", "注意");
            //    return;
            //}

            string nowitemno;
                  if (null != dgv.CurrentRow)
                {
                    nowitemno = dgv.CurrentRow.Cells["发货编码fj"].Value.ToString();
                }
                else
                {
                    nowitemno = "";
                }
                  int hadarranged = ly_sales_deliver_detailBindingSource.Find("物料编码", nowitemno);
            //hadarranged = -9;

            if (-1 < hadarranged)
            {
                ly_sales_deliver_detailBindingSource.Position = hadarranged;

                decimal  nowcount = 0;

                if (null != dgv.CurrentRow)
                {
                    nowcount = decimal.Parse(this.ly_sales_deliver_detailDataGridView.CurrentRow.Cells["发货数量6"].Value.ToString());
                }
                else
                {
                    nowcount = 0;
                }

               
                this.ly_sales_deliver_detailDataGridView.CurrentRow.Cells["发货数量6"].Value = nowcount + 1;

            }

            else
            {
                this.ly_sales_deliver_detailBindingSource.AddNew();

                this.ly_sales_deliver_detailDataGridView.CurrentRow.Cells["deliver_id6"].Value = this.ly_sales_deliverDataGridView.CurrentRow.Cells["deliver_id"].Value;

                //this.ly_sales_deliver_detailDataGridView.CurrentRow.Cells["test_detail_id6"].Value = this.ly_sales_test_detail2DataGridView.CurrentRow.Cells["test_detail_id"].Value;

                this.ly_sales_deliver_detailDataGridView.CurrentRow.Cells["序号6"].Value = this.ly_sales_deliver_detailDataGridView.RowCount;

                this.ly_sales_deliver_detailDataGridView.CurrentRow.Cells["物料编码6"].Value = dgv.CurrentRow.Cells["发货编码fj"].Value;
                //this.ly_sales_deliver_detailDataGridView.CurrentRow.Cells["出厂编码6"].Value = this.ly_sales_test_detail2DataGridView.CurrentRow.Cells["出厂编码"].Value;
                this.ly_sales_deliver_detailDataGridView.CurrentRow.Cells["发货数量6"].Value = 1;
                //this.ly_sales_deliver_detailDataGridView.CurrentRow.Cells["备注6"].Value = this.ly_sales_groupDataGridView.CurrentRow.Cells["电话"].Value;

            }


            //int parentId = int.Parse(this.ly_sales_groupDataGridView.CurrentRow.Cells["group_id"].Value.ToString());
            //this.ly_sales_deliverDataGridView.CurrentRow.Cells["group_id5"].Value = parentId;

            this.ly_sales_deliver_detailBindingSource.EndEdit();
            this.ly_sales_deliver_detailTableAdapter.Update(this.lYSalseMange.ly_sales_deliver_detail);

            ////////////////////////////

            int nowgroupid = 0;
            string nowgroupnum = "";

            string nowplannum = ly_material_plan_mainDataGridView.CurrentRow.Cells["计划编号0"].Value.ToString();
            string nowcontractnum = ly_material_plan_mainDataGridView.CurrentRow.Cells["计划编号0"].Value.ToString();

            if (null != ly_sales_groupDataGridView.CurrentRow)
            {
                nowgroupnum = ly_sales_groupDataGridView.CurrentRow .Cells["配套编码"].Value.ToString();

                nowgroupid = int.Parse(ly_sales_groupDataGridView.CurrentRow.Cells["group_id"].Value.ToString());

                //this.ly_plan_getmaterial1TableAdapter.Fill(this.lYSalseMange.ly_plan_getmaterial1, nowplannum, nowgroupnum, 0 - nowgroupid);
            }

          

            this.ly_plan_getmaterial_deliverTableAdapter.Fill(this.lYSalseMange.ly_plan_getmaterial_deliver, nowplannum, nowgroupnum, nowgroupid);


        }

        private void 生成装箱总单ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (null == f_PlanExtend_LSPTDataGridView.CurrentRow) return;

            string message = "生成装箱单吗？";
            string caption = "提示...";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;

            DialogResult result;



            result = MessageBox.Show(message, caption, buttons,
            MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

            if (result == DialogResult.Yes)
            {
                this.ly_sales_deliverBindingSource.AddNew();

                string nowdelivernum = GetMaxDeliverNum();
                this.ly_sales_deliverDataGridView.CurrentRow.Cells["发货单号5"].Value = nowdelivernum;

                this.ly_sales_deliverDataGridView.CurrentRow.Cells["发货日期5"].Value = SQLDatabase.GetNowdate().ToString(); ;

                this.ly_sales_deliverDataGridView.CurrentRow.Cells["经办人5"].Value = SQLDatabase.nowUserName();

                this.ly_sales_deliverDataGridView.CurrentRow.Cells["收件人5"].Value = this.ly_sales_groupDataGridView.CurrentRow.Cells["收件人"].Value;
                this.ly_sales_deliverDataGridView.CurrentRow.Cells["邮编5"].Value = this.ly_sales_groupDataGridView.CurrentRow.Cells["邮编"].Value;
                this.ly_sales_deliverDataGridView.CurrentRow.Cells["收件地址5"].Value = this.ly_sales_groupDataGridView.CurrentRow.Cells["地址"].Value;
                this.ly_sales_deliverDataGridView.CurrentRow.Cells["客户电话5"].Value = this.ly_sales_groupDataGridView.CurrentRow.Cells["电话"].Value;

                this.ly_sales_deliverDataGridView.CurrentRow.Cells["客户名称5"].Value = this.ly_material_plan_mainDataGridView.CurrentRow.Cells["客户名称0"].Value;


                int parentId = int.Parse(this.ly_sales_groupDataGridView.CurrentRow.Cells["group_id"].Value.ToString());
                this.ly_sales_deliverDataGridView.CurrentRow.Cells["group_id5"].Value = parentId;

                this.ly_sales_deliverBindingSource.EndEdit();
                this.ly_sales_deliverTableAdapter.Update(this.lYSalseMange.ly_sales_deliver);

                //AddDetail(nowdelivernum);

                if (null == ly_sales_deliverDataGridView.CurrentRow) return;

                //if ("True" == this.ly_sales_test_detail2DataGridView.CurrentRow.Cells["已排"].Value.ToString())
                //{

                //    return;
                //}


                //string machineNum = this.ly_sales_test_detail2DataGridView.CurrentRow.Cells["出厂编码"].Value.ToString();

                //if ("" == machineNum)
                //{
                //    MessageBox.Show("产品无出厂编码,不能出库...", "注意");
                //    return;
                //}

                int deliverId = int.Parse(this.ly_sales_deliverDataGridView.CurrentRow.Cells["deliver_id"].Value.ToString());

                foreach (DataGridViewRow dgr in f_PlanExtend_LSPTDataGridView.Rows)
                {
                    this.ly_sales_deliver_detailBindingSource.AddNew();

                    this.ly_sales_deliver_detailDataGridView.CurrentRow.Cells["deliver_id6"].Value = this.ly_sales_deliverDataGridView.CurrentRow.Cells["deliver_id"].Value;

                    this.ly_sales_deliver_detailDataGridView.CurrentRow.Cells["test_detail_id6"].Value = dgr.Cells["real_id"].Value;

                    this.ly_sales_deliver_detailDataGridView.CurrentRow.Cells["序号6"].Value = this.ly_sales_deliver_detailDataGridView.RowCount;

                    this.ly_sales_deliver_detailDataGridView.CurrentRow.Cells["物料编码6"].Value = dgr.Cells["产品编码3"].Value;

                    this.ly_sales_deliver_detailDataGridView.CurrentRow.Cells["配套编码6"].Value = dgr.Cells["配套编码3"].Value;
                    this.ly_sales_deliver_detailDataGridView.CurrentRow.Cells["配套名称6"].Value = dgr.Cells["配套名称3"].Value;

                    //配套编码3this.ly_sales_deliver_detailDataGridView.CurrentRow.Cells["出厂编码6"].Value = this.ly_sales_test_detail2DataGridView.CurrentRow.Cells["出厂编码"].Value;
                    this.ly_sales_deliver_detailDataGridView.CurrentRow.Cells["发货数量6"].Value = dgr.Cells["数量3"].Value;
                    this.ly_sales_deliver_detailDataGridView.CurrentRow.Cells["实发数量"].Value = dgr.Cells["数量3"].Value; 
                    //this.ly_sales_deliver_detailDataGridView.CurrentRow.Cells["备注6"].Value = this.ly_sales_groupDataGridView.CurrentRow.Cells["电话"].Value;

                }


                //int parentId = int.Parse(this.ly_sales_groupDataGridView.CurrentRow.Cells["group_id"].Value.ToString());
                //this.ly_sales_deliverDataGridView.CurrentRow.Cells["group_id5"].Value = parentId;

                this.ly_sales_deliver_detailBindingSource.EndEdit();
                this.ly_sales_deliver_detailTableAdapter.Update(this.lYSalseMange.ly_sales_deliver_detail);



                this.ly_sales_deliver_detailTableAdapter.Fill(this.lYSalseMange.ly_sales_deliver_detail, deliverId);

                //int parentId = int.Parse(this.ly_sales_testDataGridView.CurrentRow.Cells["record_id"].Value.ToString());

                //this.ly_sales_test_detail2TableAdapter.Fill(this.lYSalseMange.ly_sales_test_detail2, parentId);



            }
        }

        private void AddDetail(string delivernum)
        {
           
            
            
            ////////////////////////////////////////////////////////////////////////////////////////
            SqlConnection sqlConnection1 = new SqlConnection(SQLDatabase.Connectstring);
            SqlCommand cmd = new SqlCommand();

            string MaxProductionorder = "";

            cmd.Parameters.Add("@deliver_mode", SqlDbType.VarChar);
            cmd.Parameters["@deliver_mode"].Value = "YY";


            cmd.CommandText = "LY_Make_Deliver_Detail";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = sqlConnection1;

            sqlConnection1.Open();
            MaxProductionorder = cmd.ExecuteScalar().ToString();
            sqlConnection1.Close();



            //return MaxProductionorder;
        }

        private void 装箱单打印总表ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (null == this.ly_sales_deliver_detailDataGridView.CurrentRow) return;



            //NewFrm.Show(this); 

            //this.ly_plan_getmaterialTableAdapter.Fill(this.lYPlanMange.ly_plan_getmaterial, parentId);

            BaseReportView queryForm = new BaseReportView();

            queryForm.Text = "中原精密营业发货装箱单";

            queryForm.Printdata = this.lYSalseMange;




            queryForm.PrintCrystalReport = new LY_YingyeHetong_FHZX2();




           // NewFrm.Hide(this);

            queryForm.ShowDialog();
        }

        private void ly_material_plan_mainDataGridView_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            //if (null == ly_material_plan_mainDataGridView.CurrentRow)
            //{

            //    this.ly_sales_groupTableAdapter.Fill(this.lYSalseMange.ly_sales_group, "");
            //    this.f_PlanExtend_LSPTTableAdapter.Fill(this.lYSalseMange.f_PlanExtend_LSPT, "");
            //    this.ly_plan_getmaterial_departmentTableAdapter.Fill(this.lYSalseMange.ly_plan_getmaterial_department, "");

            //    return;
            //}

            //string nowplannum = ly_material_plan_mainDataGridView.Rows [e .RowIndex].Cells["计划编号0"].Value.ToString();
            //// string nowcontractnum = ly_material_plan_mainDataGridView.CurrentRow.Cells["计划编号"].Value.ToString();


            ////this.ly_sales_groupTableAdapter.Fill(this.lYSalseMange.ly_sales_group, nowplannum);
            ////this.f_PlanExtend_LSPTTableAdapter.Fill(this.lYSalseMange.f_PlanExtend_LSPT, nowplannum);
            ////this.ly_plan_getmaterial_departmentTableAdapter.Fill(this.lYSalseMange.ly_plan_getmaterial_department, nowplannum);


            //MakeGroupTreeView(nowplannum);
        }

        private void ly_material_plan_mainDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            //if (null == this.ly_sales_contract_detailDataGridView.CurrentRow) return;

            //if ("True" != this.ly_sales_contract_mainDataGridView.CurrentRow.Cells["批准"].Value.ToString())
            //{

            //    MessageBox.Show("请先确定 执行,然后打印...", "注意");
            //    return;
            //}

            ////NewFrm.Show(this); ;

            ////this.ly_plan_getmaterialTableAdapter.Fill(this.lYPlanMange.ly_plan_getmaterial, parentId);

            //BaseReportView queryForm = new BaseReportView();

            //queryForm.Text = "中原精密营业合同";

            //queryForm.setchackBoxCansee(true);

            //queryForm.Printdata = this.lYSalseMange;
            //queryForm.company = ly_sales_contract_mainDataGridView.CurrentRow.Cells["公司"].Value.ToString();

            //if ("中原" == ly_sales_contract_mainDataGridView.CurrentRow.Cells["公司"].Value.ToString())
            //{
            //    queryForm.PrintCrystalReport = new LY_YingyeHetong_FH();
            //}
            //else
            //{

            //    queryForm.PrintCrystalReport = new LY_YingyeHetong_FHzhongc();
            //}





          


            //NewFrm.Hide(this);

            //queryForm.ShowDialog();
        }

        private void ly_material_plan_mainDataGridView_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            //string isgood = "yes";

            //decimal nowmoney;



            foreach (DataGridViewRow dgr in ly_material_plan_mainDataGridView.Rows)
            {


                //if ("" != dgr.Cells["单据备注"].Value.ToString())
                //{
                //    nowmoney = decimal.Parse(dgr.Cells["单据备注"].Value.ToString());
                //}
                //else
                //{
                //    nowmoney = 0;
                //}







                if (!string.IsNullOrEmpty(dgr.Cells["单据备注"].Value.ToString()))
                {
                    foreach (DataGridViewCell dgc in dgr.Cells)
                    {

                        dgc.Style.BackColor = Color.White;
                        dgc.Style.ForeColor = Color.Red;
                    }
                }

                //else
                //{ 


                //}



            }
        }

        private void 送货单打印ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (null == this.ly_material_plan_mainDataGridView.CurrentRow) return;

            //if ("True" != this.ly_sales_contract_mainDataGridView.CurrentRow.Cells["批准"].Value.ToString())
            //{

            //    MessageBox.Show("请先确定 执行,然后打印...", "注意");
            //    return;
            //}

            string nowbuseness = this.ly_material_plan_mainDataGridView.CurrentRow.Cells["内部编码0"].Value.ToString();
            string nowcontract = this.ly_material_plan_mainDataGridView.CurrentRow.Cells["合同编号0"].Value.ToString();
       

            this.ly_sales_contract_detailTableAdapter.Fill(this.lYSalseMange.ly_sales_contract_detail, nowcontract,0);
            this.ly_sales_contract_main_forbusinessTableAdapter.Fill(this.lYSalseMange.ly_sales_contract_main_forbusiness, nowbuseness);

            //NewFrm.Show(this); 

            //this.ly_plan_getmaterialTableAdapter.Fill(this.lYPlanMange.ly_plan_getmaterial, parentId);

            //this.ly_sales_contract_detailTableAdapter.Fill(this.lYSalseMange.ly_sales_contract_detail, nowinnerCode, 0);

            BaseReportView queryForm = new BaseReportView();

            queryForm.Text = "中原精密送货单";

            queryForm.setchackBoxCansee(false);

            queryForm.Printdata = this.lYSalseMange;
            //queryForm.company = "中原";

            //if ("中原" == ly_sales_contract_mainDataGridView.CurrentRow.Cells["公司"].Value.ToString())
            //{
            queryForm.PrintCrystalReport = new LY_YingyeHetong_SHD2();
            //}
            //else
            //{

            //    queryForm.PrintCrystalReport = new LY_YingyeHetong_FHzhongc();
            //}





        


           // NewFrm.Hide(this);

            queryForm.ShowDialog();
        }

        //private void fillToolStripButton_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        this.ly_material_plan_mainperiodproTableAdapter.Fill(this.lYSalseMange.ly_material_plan_mainperiodpro, jhlbToolStripTextBox.Text, new System.Nullable<System.DateTime>(((System.DateTime)(System.Convert.ChangeType(begindateToolStripTextBox.Text, typeof(System.DateTime))))), new System.Nullable<System.DateTime>(((System.DateTime)(System.Convert.ChangeType(enddateToolStripTextBox.Text, typeof(System.DateTime))))));
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
        //        this.ly_sales_contract_main_forbusinessTableAdapter.Fill(this.lYSalseMange.ly_sales_contract_main_forbusiness, contract_codeToolStripTextBox.Text);
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
        //        this.ly_sales_contract_detailTableAdapter.Fill(this.lYSalseMange.ly_sales_contract_detail, contract_inner_codeToolStripTextBox.Text, ((int)(System.Convert.ChangeType(nowindexToolStripTextBox.Text, typeof(int)))));
        //    }
        //    catch (System.Exception ex)
        //    {
        //        System.Windows.Forms.MessageBox.Show(ex.Message);
        //    }

        //}

      

      
       

       
    }

        
}
