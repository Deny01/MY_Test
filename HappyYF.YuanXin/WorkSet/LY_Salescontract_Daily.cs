using HappyYF.Infrastructure.Repositories;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Transactions;
using System.Windows.Forms;


namespace HappyYF.YuanXin.WorkSet
{
    public partial class LY_Salescontract_Daily : Form
    {
        private string nowfilterStr = "";
        private string nowusercode = "";


        private string nowclientCode = "";
        private string nowinnerCode = "";
        private string nowcontractCode = "";

        private string nowborrowcode = "";

        private string nowfillstragecode = "";

        private string contractCanchenged = "";
        private string borrowCanchenged = "";


        private string isborrow = "";

        private int selectionIdx = 0;

        public LY_Salescontract_Daily()
        {
            InitializeComponent();
        }

        public void Find_billlocation(string nowbillnum, string nowcontractcode)
        {

            this.tabControl1.SelectedIndex = 2;
            this.ly_sales_businessBindingSource.Position = this.ly_sales_businessBindingSource.Find("业务编码", nowcontractcode);

            this.ly_sales_contract_main_forbusinessBindingSource.Position = this.ly_sales_contract_main_forbusinessBindingSource.Find("内部编码", nowbillnum);




        }

        private void t_usersBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            //this.Validate();
            //this.t_usersBindingSource.EndEdit();
            ////this.tableAdapterManager.UpdateAll(this.yonghuDataSet);
            //this.t_usersTableAdapter.Update( this.yonghuDataSet.T_users);

            SetViewState("View");

        }

        private void SetViewState(string state)
        {
            if ("View" == state)
            {



                this.treeView1.Focus();


            }
            else
            {

            }

        }

        private void Yonghu_Load(object sender, EventArgs e)
        {
            this.ly_lsptb_selTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            this.ly_lsptb_selTableAdapter.Fill(this.lYSalseMange.ly_lsptb_sel);


            this.ly_sales_outbindTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;

            this.ly_sales_businessTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;

            this.ly_sales_contract_main_forbusinessTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            this.ly_sales_contract_main_forbusinessBindingSource.Filter = " 提交=1 ";

            this.ly_sales_contract_terms_forcontractTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            this.ly_sales_contract_detailTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;

            this.ly_sales_borrowTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;

            this.ly_sales_borrowBindingSource.Filter = " 提交=1 ";


            this.ly_sales_borrow_detailTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;


            this.ly_sales_groupTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            //this.ly_sales_groupTableAdapter.Fill(this.lYSalseMange.ly_sales_group);

            this.ly_plan_getmaterialTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;


            this.ly_material_plan_mainTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            // this.ly_material_plan_mainTableAdapter.Fill(this.lYSalseMange.ly_material_plan_main, jhlbToolStripTextBox.Text);




            this.nowusercode = SQLDatabase.NowUserID;









            this.dateTimePicker1.Text = DateTime.Today.AddMonths(-6).Date.ToString();
            this.dateTimePicker2.Text = DateTime.Today.AddDays(1).Date.ToString();


            string selAllString;

            if (SQLDatabase.CheckHaveRight(SQLDatabase.NowUserID, "营业综合信息"))
            {

                selAllString = "SELECT  a.salesregion_code, a.salesregion_code+':'+a.salesregion_name as salesregion_name,b.yhbm,b.yhbm+':'+b.yhmc as yhmc FROM  ly_salesregion a left join T_users b on a.salesregion_code=b.salesregion_code ORDER BY  salesregion_code ";
                selAllString = "SELECT distinct  a.salesregion_code, a.salesregion_code+':'+a.salesregion_name as salesregion_name,a.salesperson_code as yhbm,a.salesperson_code+':'+a.salesperson_name as yhmc FROM  ly_sales_client_forcontract a ORDER BY  salesregion_code ";
            }
            else if (SQLDatabase.CheckHaveRight(SQLDatabase.NowUserID, "营业区域信息"))
            {

                selAllString = "SELECT  a.salesregion_code, a.salesregion_code+':'+a.salesregion_name as salesregion_name,b.yhbm,b.yhbm+':'+b.yhmc as yhmc FROM  ly_salesregion a left join T_users b on a.salesregion_code=b.salesregion_code  where b.salesregion_code='" + SQLDatabase.nowSalesregioncode() + "' ORDER BY  salesregion_code ";
                selAllString = "SELECT distinct  a.salesregion_code, a.salesregion_code+':'+a.salesregion_name as salesregion_name,a.salesperson_code as yhbm,a.salesperson_code+':'+a.salesperson_name as yhmc FROM  ly_sales_client_forcontract a  where a.salesregion_code='" + SQLDatabase.nowSalesregioncode() + "' ORDER BY  salesregion_code ";
            }
            else
            {
                selAllString = "SELECT  a.salesregion_code, a.salesregion_code+':'+a.salesregion_name as salesregion_name,b.yhbm,b.yhbm+':'+b.yhmc as yhmc FROM  ly_salesregion a left join T_users b on a.salesregion_code=b.salesregion_code  where b.yhbm='" + SQLDatabase.NowUserID + "' ORDER BY  salesregion_code ";
                selAllString = "SELECT distinct  a.salesregion_code, a.salesregion_code+':'+a.salesregion_name as salesregion_name,a.salesperson_code as yhbm,a.salesperson_code+':'+a.salesperson_name as yhmc FROM  ly_sales_client_forcontract a where a.salesperson_code='" + SQLDatabase.NowUserID + "' ORDER BY  salesregion_code "; ;
            }


            SqlDataAdapter salesregionAdapter = new SqlDataAdapter(selAllString, SQLDatabase.Connectstring);

            DataSet salesregionData = new DataSet();
            salesregionAdapter.Fill(salesregionData);


            System.Windows.Forms.TreeNode TNode = new System.Windows.Forms.TreeNode();
            TNode.Text = "中原精密有限公司";

            if (SQLDatabase.CheckHaveRight(SQLDatabase.NowUserID, "营业综合信息"))
            {
                TNode.Tag = "";
            }
            else if (SQLDatabase.CheckHaveRight(SQLDatabase.NowUserID, "营业区域信息"))
            {
                TNode.Tag = "salesregion_code='" + SQLDatabase.nowSalesregioncode() + "'";
            }
            else
            {
                TNode.Tag = "salesperson_code='" + SQLDatabase.NowUserID + "'";

            }

            this.treeView1.Nodes.Add(TNode);

            MakeTreeView(salesregionData.Tables[0], null, TNode);

            this.treeView1.ExpandAll();

            if (SQLDatabase.CheckHaveRight(SQLDatabase.NowUserID, "营业综合信息"))
            {
                //this.treeView1.Visible = true;
                this.splitContainer1.Panel1Collapsed = false;
            }
            else if (SQLDatabase.CheckHaveRight(SQLDatabase.NowUserID, "营业区域信息"))
            {
                //this.treeView1.Visible = true;
                this.splitContainer1.Panel1Collapsed = false;
                //this.nowfilterStr = "salesperson_code='" + SQLDatabase.NowUserID + "'";
                this.nowfilterStr = "salesregion_code='" + SQLDatabase.nowSalesregioncode() + "'";
            }
            else
            {
                //this.treeView1.Visible = false;
                this.splitContainer1.Panel1Collapsed = true;
                this.nowfilterStr = "salesperson_code='" + SQLDatabase.NowUserID + "'";


                this.nowfillstragecode = "single";
                this.nowusercode = SQLDatabase.NowUserID;

                this.ly_sales_businessTableAdapter.Fill(this.lYSalseMange.ly_sales_business, this.nowusercode, this.nowfillstragecode, this.dateTimePicker1.Value, this.dateTimePicker2.Value);


            }

            //this.ly_sales_contract_main1DataGridView.SelectionChanged -= ly_sales_contract_main1DataGridView_SelectionChanged;
            //this.ly_sales_contract_main1TableAdapter.Fill(this.lYSalseMange.ly_sales_contract_main1,"","full" );
            //this.ly_sales_contract_main1DataGridView.SelectionChanged += ly_sales_contract_main1DataGridView_SelectionChanged;

            //SetViewState("View");

            //this.tabPage1.Parent = null;

            this.treeView1.SelectedNode = TNode;

        }





        private void MakeTreeView(DataTable table, string salesregionCode, System.Windows.Forms.TreeNode PNode)
        {


            DataRow[] dr;
            string now_salesregion_code;
            string last_salesregion_code = "___";



            dr = table.Select("salesregion_code is not  null");

            System.Windows.Forms.TreeNode TNode = null;
            System.Windows.Forms.TreeNode CNode = null;

            foreach (DataRow d in dr)
            {
                now_salesregion_code = d["salesregion_name"].ToString();



                if (last_salesregion_code != now_salesregion_code)
                {

                    TNode = new System.Windows.Forms.TreeNode();

                    TNode.Text = d["salesregion_name"].ToString();

                    if (SQLDatabase.CheckHaveRight(SQLDatabase.NowUserID, "营业综合信息"))
                    {
                        TNode.Tag = "salesregion_code='" + d["salesregion_code"].ToString() + "'";
                    }
                    else if (SQLDatabase.CheckHaveRight(SQLDatabase.NowUserID, "营业区域信息"))
                    {
                        TNode.Tag = "salesregion_code='" + d["salesregion_code"].ToString() + "'";
                    }

                    else
                    {

                        TNode.Tag = "salesperson_code='" + SQLDatabase.NowUserID + "'";
                    }
                    if (PNode == null)
                    {
                        this.treeView1.Nodes.Add(TNode);
                    }
                    else
                    {
                        PNode.Nodes.Add(TNode);
                        CNode = new System.Windows.Forms.TreeNode();
                        CNode.Text = d["yhmc"].ToString();
                        CNode.Tag = "salesperson_code='" + d["yhbm"].ToString() + "'";
                        if (TNode == null)
                        {
                            this.treeView1.Nodes.Add(TNode);
                        }
                        else
                        {
                            TNode.Nodes.Add(CNode);
                        }
                    }
                }
                else
                {
                    CNode = new System.Windows.Forms.TreeNode();
                    CNode.Text = d["yhmc"].ToString();
                    CNode.Tag = "salesperson_code='" + d["yhbm"].ToString() + "'";
                    if (TNode == null)
                    {
                        this.treeView1.Nodes.Add(TNode);
                    }
                    else
                    {
                        TNode.Nodes.Add(CNode);
                    }


                }
                last_salesregion_code = now_salesregion_code;

            }



        }



        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            this.nowfilterStr = e.Node.Tag.ToString();

            if (e.Node.Level == 2)
            {


                this.nowusercode = this.nowfilterStr.Substring(this.nowfilterStr.Length - 4, 3);
                this.nowfillstragecode = "single";

                //this.ly_sales_contract_main1DataGridView.SelectionChanged -= ly_sales_contract_main1DataGridView_SelectionChanged;
                //this.ly_sales_contract_main1TableAdapter.Fill(this.lYSalseMange.ly_sales_contract_main1, this.nowusercode,"single", this.dateTimePicker1.Value , this.dateTimePicker2.Value );
                //AddSummationRow_New(ly_sales_contract_main1BindingSource, ly_sales_contract_main1DataGridView);
                //this.ly_sales_contract_main1DataGridView.SelectionChanged += ly_sales_contract_main1DataGridView_SelectionChanged;

            }
            else if (e.Node.Level == 1)
            {

                this.nowusercode = this.nowfilterStr.Substring(this.nowfilterStr.Length - 3, 2);
                this.nowfillstragecode = "region";

                //this.ly_sales_contract_main1DataGridView.SelectionChanged -= ly_sales_contract_main1DataGridView_SelectionChanged;
                //this.ly_sales_contract_main1TableAdapter.Fill(this.lYSalseMange.ly_sales_contract_main1, this.nowusercode, "region", this.dateTimePicker1.Value, this.dateTimePicker2.Value);
                //AddSummationRow_New(ly_sales_contract_main1BindingSource, ly_sales_contract_main1DataGridView);
                //this.ly_sales_contract_main1DataGridView.SelectionChanged += ly_sales_contract_main1DataGridView_SelectionChanged;
            }
            else if (e.Node.Level == 0)
            {

                this.nowusercode = "";
                this.nowfillstragecode = "full";
                if (SQLDatabase.CheckHaveRight(SQLDatabase.NowUserID, "营业区域信息"))
                {
                    this.nowusercode = this.nowfilterStr.Substring(this.nowfilterStr.Length - 3, 2);
                    this.nowfillstragecode = "region";
                }

                // this.ly_sales_contract_main1DataGridView.SelectionChanged -= ly_sales_contract_main1DataGridView_SelectionChanged;
                //this.ly_sales_contract_main1TableAdapter.Fill(this.lYSalseMange.ly_sales_contract_main1, this.nowusercode, this.nowfillstragecode, this.dateTimePicker1.Value, this.dateTimePicker2.Value);
                //AddSummationRow_New(ly_sales_contract_main1BindingSource, ly_sales_contract_main1DataGridView);
                //this.ly_sales_contract_main1DataGridView.SelectionChanged += ly_sales_contract_main1DataGridView_SelectionChanged;
            }

            //this.ly_sales_contract_main1DataGridView.SelectionChanged -= ly_sales_contract_main1DataGridView_SelectionChanged;

            this.ly_sales_businessTableAdapter.Fill(this.lYSalseMange.ly_sales_business, this.nowusercode, this.nowfillstragecode, this.dateTimePicker1.Value, this.dateTimePicker2.Value);

            // AddSummationRow_New(ly_sales_contract_main1BindingSource, ly_sales_contract_main1DataGridView);
            //this.ly_sales_contract_main1DataGridView.SelectionChanged += ly_sales_contract_main1DataGridView_SelectionChanged;

            this.groupBox1.Text = e.Node.Text + "业务信息列表";
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




        private void ly_sales_contract_main1DataGridView_SelectionChanged(object sender, EventArgs e)
        {
            if (null == ly_sales_businessDataGridView.CurrentRow)
            {
                //this.ly_sales_contract_detailTableAdapter.Fill(this.lYSalseMange.ly_sales_contract_detail, "", 0);
                this.ly_material_plan_mainTableAdapter.Fill(this.lYSalseMange.ly_material_plan_main, "SSS", "");
                this.ly_sales_contract_main_forbusinessTableAdapter.Fill(this.lYSalseMange.ly_sales_contract_main_forbusiness, "");
                return;
            }

            //this.nowinnerCode = ly_sales_contract_main1DataGridView.CurrentRow.Cells["内部编码1"].Value.ToString();
            this.nowclientCode = ly_sales_businessDataGridView.CurrentRow.Cells["客户编码"].Value.ToString();
            this.nowcontractCode = ly_sales_businessDataGridView.CurrentRow.Cells["业务编码"].Value.ToString();

            // this.ly_sales_contract_detailTableAdapter.Fill(this.lYSalseMange.ly_sales_contract_detail, nowinnerCode, 0);

            this.ly_material_plan_mainTableAdapter.Fill(this.lYSalseMange.ly_material_plan_main, "LSPT", nowcontractCode);

            this.ly_sales_borrowTableAdapter.Fill(this.lYSalseMange.ly_sales_borrow, this.nowcontractCode);

            if (null == this.ly_sales_borrowDataGridView.CurrentRow)
            {

                this.ly_sales_borrow_detailTableAdapter.Fill(this.lYSalseMange.ly_sales_borrow_detail, "-111");
            }

            this.ly_sales_contract_main_forbusinessTableAdapter.Fill(this.lYSalseMange.ly_sales_contract_main_forbusiness, this.nowcontractCode);

            if (null == ly_sales_contract_mainDataGridView.CurrentRow)
            {
                this.ly_sales_contract_terms_forcontractTableAdapter.Fill(this.lYSalseMange.ly_sales_contract_terms_forcontract, "-aqwaaa");
                this.ly_sales_contract_detailTableAdapter.Fill(this.lYSalseMange.ly_sales_contract_detail, "", 0);
            }

            //if (null == ly_sales_borrowDataGridView.CurrentRow)
            //{
            //    this.ly_sales_contract_terms_forcontractTableAdapter.Fill(this.lYSalseMange.ly_sales_contract_terms_forcontract, "-aqwaaa");
            //    this.ly_sales_contract_detailTableAdapter.Fill(this.lYSalseMange.ly_sales_contract_detail, "", 0);
            //}





        }

        private void toolStripButton21_Click(object sender, EventArgs e)
        {

            this.ly_sales_businessTableAdapter.Fill(this.lYSalseMange.ly_sales_business, this.nowusercode, this.nowfillstragecode, this.dateTimePicker1.Value, this.dateTimePicker2.Value);
            //AddSummationRow_New(ly_sales_contract_main1BindingSource, ly_sales_contract_main1DataGridView);
        }




        private void ly_sales_contract_detailDataGridView_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            //DataGridView dgv = sender as DataGridView;

            //if (null == dgv.CurrentRow) return;

            //if ("False" == this.contractCanchenged)
            //{
            //    MessageBox.Show("合同已经提交执行,不能修改数据...", "注意");
            //    return;

            //}



            /////////////////////////////////////////////////////////////////
            //if ("商标" == dgv.CurrentCell.OwningColumn.Name)
            //{

            //    ChangeValue queryForm = new ChangeValue();

            //    queryForm.OldValue = dgv.CurrentCell.Value.ToString();
            //    queryForm.NewValue = "";
            //    queryForm.ChangeMode = "string";
            //    queryForm.ShowDialog();




            //    if (queryForm.NewValue != "")
            //    {
            //        dgv.CurrentRow.Cells["商标"].Value = queryForm.NewValue;
            //        //dgv.CurrentRow.Cells["discount_money"].Value = DBNull.Value;

            //        //dgv.CurrentRow.Cells["approve_flag"].Value = "False";
            //        SaveDetailItem();

            //        //CountPlanStru();

            //    }
            //    else
            //    {

            //    }
            //    return;

            //}
            /////////////////////////////////////////////////////////////////
            //if ("数量" == dgv.CurrentCell.OwningColumn.Name)
            //{

            //    if ("True" == dgv.CurrentRow.Cells["审批"].Value.ToString())
            //    {

            //        MessageBox.Show("合同已经审批,不能修改数据...", "注意");
            //        return;
            //    }

            //    ChangeValue queryForm = new ChangeValue();

            //    queryForm.OldValue = dgv.CurrentCell.Value.ToString();
            //    queryForm.NewValue = "";
            //    queryForm.ChangeMode = "value";
            //    queryForm.ShowDialog();




            //    if (queryForm.NewValue != "")
            //    {
            //        dgv.CurrentRow.Cells["数量"].Value = queryForm.NewValue;
            //        //dgv.CurrentRow.Cells["discount_money"].Value = DBNull.Value;

            //        //dgv.CurrentRow.Cells["approve_flag"].Value = "False";
            //        SaveDetailItem();


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
            //if ("单价" == dgv.CurrentCell.OwningColumn.Name)
            //{



            //    if ("True" == dgv.CurrentRow.Cells["审批"].Value.ToString())
            //    {

            //        MessageBox.Show("合同已经审批,不能修改数据...", "注意");
            //        return;
            //    }

            //    ChangeValue queryForm = new ChangeValue();

            //    queryForm.OldValue = dgv.CurrentCell.Value.ToString();
            //    queryForm.NewValue = "";
            //    queryForm.ChangeMode = "value";
            //    queryForm.ShowDialog();




            //    if (queryForm.NewValue != "")
            //    {
            //        dgv.CurrentRow.Cells["单价"].Value = queryForm.NewValue;

            //        decimal nowsalesprice;

            //        if ("" != dgv.CurrentRow.Cells["营业定价"].Value.ToString())
            //        {
            //            nowsalesprice = decimal.Parse(dgv.CurrentRow.Cells["营业定价"].Value.ToString());
            //        }
            //        else
            //        {
            //            nowsalesprice = 1;
            //        }

            //        dgv.CurrentRow.Cells["单件折扣"].Value = (nowsalesprice - decimal.Parse(queryForm.NewValue)) / nowsalesprice * 100;
            //        //dgv.CurrentRow.Cells["discount_money"].Value = DBNull.Value;

            //        //dgv.CurrentRow.Cells["approve_flag"].Value = "False";
            //        SaveDetailItem();


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


            ///////////////////////////////////////////////////////

            //if ("单件折扣" == dgv.CurrentCell.OwningColumn.Name)
            //{


            //    if ("True" == dgv.CurrentRow.Cells["审批"].Value.ToString())
            //    {

            //        MessageBox.Show("合同已经审批,不能修改数据...", "注意");
            //        return;
            //    }



            //    ChangeValue queryForm = new ChangeValue();

            //    queryForm.OldValue = dgv.CurrentCell.Value.ToString();
            //    queryForm.NewValue = "";
            //    queryForm.ChangeMode = "value";
            //    queryForm.ShowDialog();




            //    if (queryForm.NewValue != "")
            //    {
            //        dgv.CurrentRow.Cells["单件折扣"].Value = queryForm.NewValue;

            //        decimal nowsalesprice;

            //        if ("" != dgv.CurrentRow.Cells["营业定价"].Value.ToString())
            //        {
            //            nowsalesprice = decimal.Parse(dgv.CurrentRow.Cells["营业定价"].Value.ToString());
            //        }
            //        else
            //        {
            //            nowsalesprice = 0;
            //        }

            //        dgv.CurrentRow.Cells["单价"].Value =nowsalesprice*( 100- decimal.Parse (queryForm.NewValue))/100;
            //        //dgv.CurrentRow.Cells["discount_money"].Value = DBNull.Value;

            //        //dgv.CurrentRow.Cells["approve_flag"].Value = "False";
            //        SaveDetailItem();


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

            //if ("交货时间" == dgv.CurrentCell.OwningColumn.Name)
            //{

            //    ChangeValue queryForm = new ChangeValue();

            //    queryForm.OldValue = dgv.CurrentCell.Value.ToString();
            //    queryForm.NewValue = "";
            //    queryForm.ChangeMode = "datetime";
            //    queryForm.ShowDialog();




            //    if (queryForm.NewValue != "")
            //    {
            //        dgv.CurrentRow.Cells["交货时间"].Value = queryForm.NewValue;
            //        //dgv.CurrentRow.Cells["discount_money"].Value = DBNull.Value;

            //        //dgv.CurrentRow.Cells["approve_flag"].Value = "False";
            //        SaveDetailItem();


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


            ///////////////////////////////////////////////////////
            ///////////////////////////////////////////////////////////////////

            ////if ("审批" == dgv.CurrentCell.OwningColumn.Name)
            ////{

            ////    decimal nowrealdis;

            ////    if ("" != dgv.CurrentRow.Cells["单件折扣"].Value.ToString())
            ////    {
            ////        nowrealdis = decimal.Parse(dgv.CurrentRow.Cells["单件折扣"].Value.ToString());
            ////    }
            ////    else
            ////    {
            ////        nowrealdis = 0;
            ////    }

            ////    decimal nowleaderdiscount = SQLDatabase.nowUserdiscount();

            ////    if (nowrealdis > nowleaderdiscount)
            ////    {
            ////        MessageBox.Show("折扣权限不够,审批取消...", "注意");
            ////        return;

            ////    }


            ////    if ("True" == dgv.CurrentRow.Cells["审批"].Value.ToString())
            ////    {
            ////        dgv.CurrentRow.Cells["审批"].Value = "False";
            ////        dgv.CurrentRow.Cells["审批人"].Value = DBNull.Value;
            ////        dgv.CurrentRow.Cells["审批日期"].Value = DBNull.Value;
            ////        dgv.CurrentRow.Cells["审批人折扣"].Value = DBNull.Value;
            ////    }
            ////    else
            ////    {

            ////        dgv.CurrentRow.Cells["审批"].Value = "True";
            ////        dgv.CurrentRow.Cells["审批人"].Value = SQLDatabase.nowUserName();
            ////        dgv.CurrentRow.Cells["审批日期"].Value = SQLDatabase.GetNowdate();
            ////        dgv.CurrentRow.Cells["审批人折扣"].Value = SQLDatabase.nowUserdiscount();
            ////    }



            ////    SaveDetailItem();



            ////    return;

            ////}
            ///////////////////////////////////////////////////////////////////

            //if ("备注2" == dgv.CurrentCell.OwningColumn.Name)
            //{

            //    ChangeValue queryForm = new ChangeValue();

            //    queryForm.OldValue = dgv.CurrentCell.Value.ToString();
            //    queryForm.NewValue = "";
            //    queryForm.ChangeMode = "longstring";
            //    queryForm.ShowDialog();




            //    if (queryForm.NewValue != "")
            //    {
            //        dgv.CurrentRow.Cells["备注2"].Value = queryForm.NewValue;
            //        //dgv.CurrentRow.Cells["discount_money"].Value = DBNull.Value;

            //        //dgv.CurrentRow.Cells["approve_flag"].Value = "False";
            //        SaveDetailItem();


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


            ///////////////////////////////////////////////////////
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

        private void AddSummationRow_New(BindingSource bs, DataGridView dgv)
        {

            return;
            if (null == dgv.CurrentRow) return;

            DataRow sumdr = (((DataSet)bs.DataSource).Tables[bs.DataMember]).NewRow();

            if (-1 != bs.Find("客户名称", "合计"))
            {
                bs.RemoveAt(bs.Find("客户名称", "合计"));
            }

            foreach (DataGridViewRow dgvRow in dgv.Rows)
            {
                foreach (DataGridViewCell dgvCell in dgvRow.Cells)
                {
                    //foreach (DataGridViewColumn dgvColumn in dgv.Columns)
                    //{
                    if (dgvCell.Value != null && !(dgvCell.Value is DBNull))
                    {
                        if (IsInteger(dgvCell.Value))
                        {
                            if ("合同天数" != dgvCell.OwningColumn.HeaderText && "到期天数" != dgvCell.OwningColumn.HeaderText && "折扣利率" != dgvCell.OwningColumn.HeaderText)
                            {
                                if (sumdr[dgvCell.OwningColumn.DataPropertyName] == null || (sumdr[dgvCell.OwningColumn.DataPropertyName] is DBNull))
                                    sumdr[dgvCell.OwningColumn.DataPropertyName] = 0;


                                sumdr[dgvCell.OwningColumn.DataPropertyName] = Convert.ToInt64(sumdr[dgvCell.OwningColumn.DataPropertyName]) + Convert.ToInt64(dgvCell.Value);
                            }
                        }
                        else if (IsDecimal(dgvCell.Value))
                        {
                            if ("合同天数" != dgvCell.OwningColumn.HeaderText && "到期天数" != dgvCell.OwningColumn.HeaderText && "折扣利率" != dgvCell.OwningColumn.HeaderText)
                            {
                                if (sumdr[dgvCell.OwningColumn.DataPropertyName] == null || (sumdr[dgvCell.OwningColumn.DataPropertyName] is DBNull))
                                    sumdr[dgvCell.OwningColumn.DataPropertyName] = 0;
                                //dgvCell .OwningColumn.Name  dgvCell.ColumnIndex

                                sumdr[dgvCell.OwningColumn.DataPropertyName] = Convert.ToDecimal(sumdr[dgvCell.OwningColumn.DataPropertyName]) + Convert.ToDecimal(dgvCell.Value);
                            }
                        }

                        //sumBox.Text = string.Format("{0}", sumBox.Tag);
                        //sumBox.Invalidate();

                    }
                    //}
                }

            }
            sumdr["客户名称"] = "合计";
            sumdr["id"] = 0;
            ((DataSet)bs.DataSource).Tables[bs.DataMember].Rows.Add(sumdr);
            bs.ResetBindings(true);

        }

        private void toolStripButton27_Click(object sender, EventArgs e)
        {

        }

        private void toolStripTextBox5_KeyUp(object sender, KeyEventArgs e)
        {
            string filterString;


            filterString = GetDataGridviewMultiFilterString.DGVMultiFilterString(this.ly_sales_businessDataGridView, this.toolStripTextBox5.Text);


            this.ly_sales_businessBindingSource.Filter = filterString;
        }

        private void toolStripTextBox5_Enter(object sender, EventArgs e)
        {
            toolStripTextBox5.Text = "";

            this.ly_sales_businessBindingSource.Filter = "";
        }

        private void toolStripButton16_Click(object sender, EventArgs e)
        {
            ExportDataGridviewTOExcell.ExportDataGridview(this.ly_sales_businessDataGridView, true);
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

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if (null == ly_material_plan_mainDataGridView.CurrentRow)
            {

                return;
            }

            if ("True" == ly_material_plan_mainDataGridView.CurrentRow.Cells["配套完成0"].Value.ToString())
            {
                MessageBox.Show("依赖书已经技术配套,不能修改数据...", "注意");
                return;

            }

            // this.nowinnerCode;

            LY_Salescontract_GroupAdd queryForm = new LY_Salescontract_GroupAdd();

            queryForm.contractinner_code = nowcontractCode;
            queryForm.nowcontractCode = nowcontractCode;
            queryForm.now_plannum = this.ly_material_plan_mainDataGridView.CurrentRow.Cells["计划编号0"].Value.ToString();

            queryForm.runmode = "修改";


            queryForm.StartPosition = FormStartPosition.CenterParent;
            queryForm.ShowDialog();

            if (queryForm.DialogResult != DialogResult.Cancel)
            {
                this.ly_material_plan_mainTableAdapter.Fill(this.lYSalseMange.ly_material_plan_main, "LSPT", nowcontractCode);



                this.ly_material_plan_mainBindingSource.Position = this.ly_material_plan_mainBindingSource.Find("内部编码", queryForm.now_plannum);



            }
        }

        private void bindingNavigatorDeleteItem_Click(object sender, EventArgs e)
        {
            if (null == ly_material_plan_mainDataGridView.CurrentRow)
            {

                return;
            }

            string salespeople = this.ly_material_plan_mainDataGridView.CurrentRow.Cells["录入人0"].Value.ToString();

            if (salespeople != SQLDatabase.nowUserName())
            {
                MessageBox.Show("请录入人:" + salespeople + "删除", "注意");
                return;
            }

            if ("True" == ly_material_plan_mainDataGridView.CurrentRow.Cells["配套完成0"].Value.ToString())
            {
                MessageBox.Show("依赖书已经技术配套,不能删除数据...", "注意");
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




                this.ly_material_plan_mainBindingSource.RemoveCurrent();


                ly_material_plan_mainDataGridView.EndEdit();
                ly_material_plan_mainBindingSource.EndEdit();



                this.ly_material_plan_mainTableAdapter.Update(this.lYSalseMange.ly_material_plan_main);

                //this.ly_sales_contract_main1DataGridView.SelectionChanged -= ly_sales_contract_main1DataGridView_SelectionChanged;
                //this.ly_sales_contract_main1TableAdapter.Fill(this.lYSalseMange.ly_sales_contract_main1, this.nowusercode, "single", this.dateTimePicker1.Value, this.dateTimePicker2.Value);
                //this.ly_sales_contract_main1DataGridView.SelectionChanged += ly_sales_contract_main1DataGridView_SelectionChanged;
            }
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
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

        private void ly_material_plan_mainDataGridView_SelectionChanged(object sender, EventArgs e)
        {
            if (null == ly_material_plan_mainDataGridView.CurrentRow)
            {

                this.ly_sales_groupTableAdapter.Fill(this.lYSalseMange.ly_sales_group, "");
                this.treeView2.Nodes.Clear();
                return;
            }

            string nowplannum = ly_material_plan_mainDataGridView.CurrentRow.Cells["计划编号0"].Value.ToString();
            // string nowcontractnum = ly_material_plan_mainDataGridView.CurrentRow.Cells["计划编号"].Value.ToString();


            this.ly_sales_groupTableAdapter.Fill(this.lYSalseMange.ly_sales_group, nowplannum);

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

                //string selAllString = "SELECT   a.id,  a.parent_id,a.parentno, a.gitemno, b.fir_style+':'+  b.xhc+' '+cast(cast(a.absqty  as int) as varchar(3))+ a.unit as itemname,a.gremark  from ly_plan_getmaterial a left join " +
                //                      "      ly_inma0010 AS b ON a.gitemno = b.wzbh          " +
                //                      " where material_plan_num='" + nowplannum + "' and sales_group_code='" + nowgroupCode + "' order by b.fir_style,a.gitemno";


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

            string nowgroupname = ly_sales_groupDataGridView.CurrentRow.Cells["配套名称"].Value.ToString();


            if ("备件" == nowgroupname)
            {
                this.ly_plan_getmaterialDataGridView.Columns["转赠批准"].Visible = true;
                this.ly_plan_getmaterialDataGridView.Columns["转赠批准人"].Visible = true;
            }
            else
            {
                this.ly_plan_getmaterialDataGridView.Columns["转赠批准"].Visible = false;
                this.ly_plan_getmaterialDataGridView.Columns["转赠批准人"].Visible = false;
            }

            this.treeView2.SelectedNode = FindGroupNode(this.treeView2.Nodes, nowgroupid.ToString());


            //this.ly_plan_getmaterialTableAdapter.Fill(this.lYSalseMange.ly_plan_getmaterial, nowplannum, nowgroupnum, nowgroupid);
        }

        private void ly_sales_contract_detailDataGridView_DoubleClick(object sender, EventArgs e)
        {
            //if (null == ly_sales_contract_detailDataGridView.CurrentRow) return;

            //if ("True" == ly_material_plan_mainDataGridView.CurrentRow.Cells["配套完成0"].Value.ToString())
            //{
            //    MessageBox.Show("依赖书已经技术配套,不能增加数据...", "注意");
            //    return;

            //}

            //string nowmaterialplannum = ly_material_plan_mainDataGridView.CurrentRow.Cells["计划编号0"].Value.ToString();
            //string nowcontractcode = ly_material_plan_mainDataGridView.CurrentRow.Cells["合同编号0"].Value.ToString();
            //string nowsales_group_code = ly_sales_groupDataGridView.CurrentRow.Cells["配套编码"].Value.ToString();


            //string nowparentno = ly_sales_groupDataGridView.CurrentRow.Cells["配套编码"].Value.ToString();
            //string nowparentname = ly_sales_groupDataGridView.CurrentRow.Cells["配套名称"].Value.ToString();

            //string nowitemno = ly_sales_contract_detailDataGridView.CurrentRow.Cells["产品编码"].Value.ToString();
            //string nowitemname = ly_sales_contract_detailDataGridView.CurrentRow.Cells["产品名称"].Value.ToString();

            //string nowunit = ly_sales_contract_detailDataGridView.CurrentRow.Cells["单位"].Value.ToString();


            //int nowparent_id = 0 - int.Parse(ly_sales_groupDataGridView.CurrentRow.Cells["group_id"].Value.ToString());
            ////int nowparent_id=-1;



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


            //this.ly_plan_getmaterialDataGridView.CurrentRow.Cells["发货编码"].Value = nowitemno;
            //this.ly_plan_getmaterialDataGridView.CurrentRow.Cells["发货名称"].Value = nowitemname;

            ////this.ly_plan_getmaterialDataGridView.CurrentRow.Cells["产品型号5"].Value = nowitemno;
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

            //this.ly_plan_getmaterialTableAdapter.Fill(this.lYSalseMange.ly_plan_getmaterial, nowmaterialplannum, nowsales_group_code, nowparent_id);

            //this.ly_plan_getmaterialBindingSource.Position = this.ly_plan_getmaterialBindingSource.Find("产品编号", nowitemno);






            //this.ly_sales_contract_detailBindingSource.Position = this.ly_sales_contract_detailBindingSource.Find("产品编码", nowitemno);

            //string nowplannum = ly_material_plan_mainDataGridView.CurrentRow.Cells["计划编号0"].Value.ToString();
            //MakeGroupTreeView(nowplannum);


            ////this.ly_sales_contract_detailDataGridView.CurrentCell = this.ly_sales_contract_detailDataGridView.CurrentRow.Cells["单价"];
        }

        private void ly_plan_getmaterialDataGridView_SelectionChanged(object sender, EventArgs e)
        {
            if (null == ly_plan_getmaterialDataGridView.CurrentRow)
            {

                return;
            }

            string nowitemno = ly_plan_getmaterialDataGridView.CurrentRow.Cells["产品编号5"].Value.ToString();
            this.ly_sales_contract_detailBindingSource.Position = this.ly_sales_contract_detailBindingSource.Find("产品编码", nowitemno);
        }

        private void bindingNavigatorDeleteItem1_Click(object sender, EventArgs e)
        {
            if (null == ly_plan_getmaterialDataGridView.CurrentRow)
            {

                return;
            }

            if ("True" == ly_material_plan_mainDataGridView.CurrentRow.Cells["配套完成0"].Value.ToString())
            {
                MessageBox.Show("依赖书已经技术配套,不能删除数据...", "注意");
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


                this.ly_sales_contract_detailTableAdapter.Fill(this.lYSalseMange.ly_sales_contract_detail, nowinnerCode, 0);

                if (null != ly_plan_getmaterialDataGridView.CurrentRow)
                {

                    string nowitemno = ly_plan_getmaterialDataGridView.CurrentRow.Cells["产品编号5"].Value.ToString();
                    this.ly_sales_contract_detailBindingSource.Position = this.ly_sales_contract_detailBindingSource.Find("产品编码", nowitemno);
                }

                string nowplannum = ly_material_plan_mainDataGridView.CurrentRow.Cells["计划编号0"].Value.ToString();
                MakeGroupTreeView(nowplannum);


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

        private void ly_plan_getmaterialDataGridView_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            DataGridView dgv = sender as DataGridView;

            if (null == dgv.CurrentRow) return;


            if ("True" == ly_material_plan_mainDataGridView.CurrentRow.Cells["配套完成0"].Value.ToString())
            {
                MessageBox.Show("依赖书已经技术配套,不能修改数据...", "注意");
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

                    //dgv.CurrentRow.Cells["approve_flag"].Value = "False";

                    this.ly_plan_getmaterialBindingSource.EndEdit();
                    this.ly_plan_getmaterialTableAdapter.Update(this.lYSalseMange.ly_plan_getmaterial);

                    this.ly_sales_contract_detailTableAdapter.Fill(this.lYSalseMange.ly_sales_contract_detail, nowinnerCode, 0);

                    string nowplannum = ly_material_plan_mainDataGridView.CurrentRow.Cells["计划编号0"].Value.ToString();
                    MakeGroupTreeView(nowplannum);



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

                    string nowplannum = ly_material_plan_mainDataGridView.CurrentRow.Cells["计划编号0"].Value.ToString();
                    MakeGroupTreeView(nowplannum);


                    //this.ly_sales_contract_detailTableAdapter.Fill(this.lYSalseMange.ly_sales_contract_detail, nowinnerCode, 0);

                    //CountPlanStru();

                }
                else
                {

                }
                return;

            }




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


            string nowplannum = ly_material_plan_mainDataGridView.CurrentRow.Cells["计划编号0"].Value.ToString();
            string nowcontractnum = ly_material_plan_mainDataGridView.CurrentRow.Cells["计划编号0"].Value.ToString();

            string nowgroupnum = ly_sales_groupDataGridView.CurrentRow.Cells["配套编码"].Value.ToString();
            string nowgroupname = ly_sales_groupDataGridView.CurrentRow.Cells["配套名称"].Value.ToString();

            if ("备件" == nowgroupname)
            {
                this.ly_plan_getmaterialDataGridView.Columns["转赠批准"].Visible = true;
                this.ly_plan_getmaterialDataGridView.Columns["转赠批准人"].Visible = true;
            }
            else
            {
                this.ly_plan_getmaterialDataGridView.Columns["转赠批准"].Visible = false;
                this.ly_plan_getmaterialDataGridView.Columns["转赠批准人"].Visible = false;
            }


            int nowparentid = int.Parse(e.Node.Tag.ToString());


            this.ly_plan_getmaterialTableAdapter.Fill(this.lYSalseMange.ly_plan_getmaterial, nowplannum, nowgroupnum, nowparentid);
        }

        private void ly_material_plan_mainDataGridView_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            return;

            DataGridView dgv = sender as DataGridView;

            if (null == dgv.CurrentRow) return;

            if ("True" == ly_material_plan_mainDataGridView.CurrentRow.Cells["配套完成0"].Value.ToString())
            {
                MessageBox.Show("依赖书已经技术配套,不能修改数据...", "注意");
                return;

            }

            ///////////////////////////////////////////////////////////
            if ("提交0" == dgv.CurrentCell.OwningColumn.Name)
            {


                if ("True" == dgv.CurrentRow.Cells["提交0"].Value.ToString())
                {
                    dgv.CurrentRow.Cells["提交0"].Value = "False";

                }
                else
                {

                    dgv.CurrentRow.Cells["提交0"].Value = "True";
                }



                this.ly_material_plan_mainBindingSource.EndEdit();
                this.ly_material_plan_mainTableAdapter.Update(this.lYSalseMange.ly_material_plan_main);



                return;

            }
            ///////////////////////////////////////////////////////////////
        }

        private void toolStripButton6_Click(object sender, EventArgs e)
        {


            string nowclientcode = "111";

            LY_Salescontract_Add1 queryForm = new LY_Salescontract_Add1();

            queryForm.salesclientcode = nowclientcode;
            queryForm.runmode = "增加";


            queryForm.StartPosition = FormStartPosition.CenterParent;
            queryForm.ShowDialog();

            if (queryForm.DialogResult != DialogResult.Cancel)
            {
                this.ly_sales_businessTableAdapter.Fill(this.lYSalseMange.ly_sales_business, this.nowusercode, this.nowfillstragecode, this.dateTimePicker1.Value, this.dateTimePicker2.Value);

                this.ly_sales_businessBindingSource.Position = this.ly_sales_businessBindingSource.Find("业务编码", queryForm.contractinner_code);


            }
        }

        private void toolStripButton7_Click(object sender, EventArgs e)
        {
            if (null == this.ly_sales_businessDataGridView.CurrentRow)
            {

                return;
            }

            string nowcontractinner_code = this.ly_sales_businessDataGridView.CurrentRow.Cells["业务编码"].Value.ToString();

            LY_Salescontract_Add1 queryForm = new LY_Salescontract_Add1();

            queryForm.contractinner_code = nowcontractinner_code;
            queryForm.runmode = "修改";


            queryForm.StartPosition = FormStartPosition.CenterParent;
            queryForm.ShowDialog();

            if (queryForm.DialogResult != DialogResult.Cancel)
            {
                this.ly_sales_businessTableAdapter.Fill(this.lYSalseMange.ly_sales_business, this.nowusercode, this.nowfillstragecode, this.dateTimePicker1.Value, this.dateTimePicker2.Value);

                this.ly_sales_businessBindingSource.Position = this.ly_sales_businessBindingSource.Find("业务编码", queryForm.contractinner_code);

            }
        }

        private void toolStripButton8_Click(object sender, EventArgs e)
        {
            if (null == ly_sales_businessDataGridView.CurrentRow)
            {


                return;
            }

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

        private void bindingNavigatorAddNewItem1_Click(object sender, EventArgs e)
        {
            if (null == ly_material_plan_mainDataGridView.CurrentRow)
            {
                MessageBox.Show("依赖书未创建,不能增加数据...", "注意");
                return;
            }

            if (null == ly_sales_groupDataGridView.CurrentRow)
            {
                MessageBox.Show("配套未设置,不能增加数据...", "注意");
                return;
            }

            if ("True" == ly_material_plan_mainDataGridView.CurrentRow.Cells["配套完成0"].Value.ToString())
            {
                MessageBox.Show("依赖书已经技术配套,不能增加数据...", "注意");
                return;

            }

            LY_SalesProduct_Sel queryForm = new LY_SalesProduct_Sel();

            //queryForm.salesclientcode = nowclientcode;
            //queryForm.runmode = "增加";


            queryForm.StartPosition = FormStartPosition.CenterParent;
            queryForm.ShowDialog();

            if (queryForm.DialogResult != DialogResult.Cancel)
            {
                //if (null == ly_sales_contract_detailDataGridView.CurrentRow) return;



                string nowmaterialplannum = ly_material_plan_mainDataGridView.CurrentRow.Cells["计划编号0"].Value.ToString();
                string nowcontractcode = ly_material_plan_mainDataGridView.CurrentRow.Cells["合同编号0"].Value.ToString();
                string nowsales_group_code = ly_sales_groupDataGridView.CurrentRow.Cells["配套编码"].Value.ToString();


                string nowparentno = ly_sales_groupDataGridView.CurrentRow.Cells["配套编码"].Value.ToString();
                string nowparentname = ly_sales_groupDataGridView.CurrentRow.Cells["配套名称"].Value.ToString();

                string nowitemno = queryForm.nowitemno;
                string nowitemname = queryForm.nowitemname;

                string nowunit = queryForm.nowunit;


                int nowparent_id = 0 - int.Parse(ly_sales_groupDataGridView.CurrentRow.Cells["group_id"].Value.ToString());

                int hadarranged = ly_plan_getmaterialBindingSource.Find("发货编码", nowitemno);
                if (-1 < hadarranged)
                {
                    ly_plan_getmaterialBindingSource.Position = hadarranged;
                    MessageBox.Show("当前产品已有选择,修改数量即可...", "注意");
                    return;

                }

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

                this.ly_plan_getmaterialDataGridView.CurrentRow.Cells["父件编号5"].Value = nowsales_group_code;

                this.ly_plan_getmaterialDataGridView.CurrentRow.Cells["父件名称5"].Value = nowparentname;

                this.ly_plan_getmaterialDataGridView.CurrentRow.Cells["parent_id5"].Value = nowparent_id;

                this.ly_plan_getmaterialBindingSource.EndEdit();

                this.ly_plan_getmaterialTableAdapter.Update(this.lYSalseMange.ly_plan_getmaterial);



                //this.ly_sales_contract_detailTableAdapter.Fill(this.lYSalseMange.ly_sales_contract_detail, nowinnerCode, 0);

                this.ly_plan_getmaterialTableAdapter.Fill(this.lYSalseMange.ly_plan_getmaterial, nowmaterialplannum, nowsales_group_code, nowparent_id);

                this.ly_plan_getmaterialBindingSource.Position = this.ly_plan_getmaterialBindingSource.Find("产品编号", nowitemno);






                //this.ly_sales_contract_detailBindingSource.Position = this.ly_sales_contract_detailBindingSource.Find("产品编码", nowitemno);

                string nowplannum = ly_material_plan_mainDataGridView.CurrentRow.Cells["计划编号0"].Value.ToString();
                MakeGroupTreeView(nowplannum);

            }
        }



        private void ly_sales_contract_mainDataGridView_SelectionChanged(object sender, EventArgs e)
        {
            if (null == ly_sales_contract_mainDataGridView.CurrentRow)
            {
                //this.ly_sales_contract_terms_forcontractTableAdapter.Fill(this.lYSalseMange.ly_sales_contract_terms_forcontract, "-aqwaaa");
                //this.ly_sales_contract_detailTableAdapter.Fill(this.lYSalseMange.ly_sales_contract_detail, "", 0);

                this.toolStripButton44.Visible = false;

                return;
            }

            this.nowinnerCode = ly_sales_contract_mainDataGridView.CurrentRow.Cells["内部编码"].Value.ToString();
            this.nowclientCode = ly_sales_contract_mainDataGridView.CurrentRow.Cells["客户编码c"].Value.ToString();

            this.ly_sales_contract_terms_forcontractTableAdapter.Fill(this.lYSalseMange.ly_sales_contract_terms_forcontract, nowinnerCode);
            this.ly_sales_contract_detailTableAdapter.Fill(this.lYSalseMange.ly_sales_contract_detail, nowinnerCode, 0);

            this.ly_sales_outbindTableAdapter.Fill(this.lYSalseMange.ly_sales_outbind, nowinnerCode);

            if ("True" == ly_sales_contract_mainDataGridView.CurrentRow.Cells["提交"].Value.ToString() ||
                "True" == ly_sales_contract_mainDataGridView.CurrentRow.Cells["批准"].Value.ToString())
            {
                this.contractCanchenged = "False";
            }
            else
            {
                this.contractCanchenged = "True";
            }

            if ("中原" == ly_sales_contract_mainDataGridView.CurrentRow.Cells["公司"].Value.ToString())
            {
                this.toolStripButton44.Visible = false;
                this.toolStripButton47.Visible = false;

                ly_sales_contract_mainDataGridView.Columns["中成执行"].Visible = false;
                ly_sales_contract_mainDataGridView.Columns["中成批准人"].Visible = false;
                ly_sales_contract_mainDataGridView.Columns["中成批准日期"].Visible = false;
                ly_sales_contract_mainDataGridView.Columns["中成金额0"].Visible = false;



                ly_sales_contract_detailDataGridView.Columns["中成折扣"].Visible = false;
                ly_sales_contract_detailDataGridView.Columns["中成金额"].Visible = false;




            }
            else
            {
                this.toolStripButton44.Visible = true;
                if ("True" == this.ly_sales_contract_mainDataGridView.CurrentRow.Cells["免税"].Value.ToString())
                {
                    this.toolStripButton47.Visible = true;
                }

                ly_sales_contract_mainDataGridView.Columns["中成执行"].Visible = true;
                ly_sales_contract_mainDataGridView.Columns["中成批准人"].Visible = true;
                ly_sales_contract_mainDataGridView.Columns["中成批准日期"].Visible = true;
                ly_sales_contract_mainDataGridView.Columns["中成金额0"].Visible = true;


                ly_sales_contract_detailDataGridView.Columns["中成折扣"].Visible = true;
                ly_sales_contract_detailDataGridView.Columns["中成金额"].Visible = true;

            }

            //if ("True" == ly_sales_contract_mainDataGridView.CurrentRow.Cells["借用"].Value.ToString())
            //{
            //    this.isborrow = "True";
            //}
            //else
            //{
            //    this.isborrow = "False";
            //}


        }

        private void toolStripButton18_Click(object sender, EventArgs e)
        {
            ExportDataGridviewTOExcell.ExportDataGridview(this.ly_sales_contract_mainDataGridView, true);
        }

        private void toolStripButton27_Click_1(object sender, EventArgs e)
        {
            if (null == this.ly_sales_contract_detailDataGridView.CurrentRow) return;

            if ("" == ly_sales_contract_mainDataGridView.CurrentRow.Cells["类别"].Value.ToString())
            {

                MessageBox.Show("请选择合同类别,然后打印...", "注意");
                return;
            }

            //NewFrm.Show(this); ;

            //this.ly_plan_getmaterialTableAdapter.Fill(this.lYPlanMange.ly_plan_getmaterial, parentId);

            BaseReportView queryForm = new BaseReportView();

            queryForm.Text = "中原精密营业合同";

            queryForm.Printdata = this.lYSalseMange;


            if ("工矿" == ly_sales_contract_mainDataGridView.CurrentRow.Cells["类别"].Value.ToString())
            {
                if ("中原" == ly_sales_contract_mainDataGridView.CurrentRow.Cells["公司"].Value.ToString())
                {
                    queryForm.PrintCrystalReport = new LY_YingyeHetong();
                }
                else
                {
                    queryForm.PrintCrystalReport = new LY_YingyeHetongZC();
                }
            }
            else
            {
                if ("中原" == ly_sales_contract_mainDataGridView.CurrentRow.Cells["公司"].Value.ToString())
                {
                    queryForm.PrintCrystalReport = new LY_YingyeHetong_XH();
                }
                else
                {
                    queryForm.PrintCrystalReport = new LY_YingyeHetong_XHZC();
                }

            }


            //string selectFormula;

            //selectFormula = "{ly_store_planitemcount.状态}  =   '原料'  and {ly_store_planitemcount.欠料金额}>0 ";
            //queryForm.PrintCrystalReport.DataDefinition.RecordSelectionFormula = selectFormula;

            // NewFrm.Hide(this);

            queryForm.ShowDialog();
        }

        private void toolStripButton19_Click(object sender, EventArgs e)
        {
            if (null == ly_sales_businessDataGridView.CurrentRow)
            {

                this.ly_sales_contract_main_forbusinessTableAdapter.Fill(this.lYSalseMange.ly_sales_contract_main_forbusiness, "");

                return;
            }

            string nowcontractcode = this.ly_sales_businessDataGridView.CurrentRow.Cells["业务编码"].Value.ToString();
            string nowclientcode = this.ly_sales_businessDataGridView.CurrentRow.Cells["客户编码"].Value.ToString();

            LY_Salescontract_Add queryForm = new LY_Salescontract_Add();

            queryForm.contract_code = nowcontractcode;
            queryForm.salesclientcode = nowclientcode;
            queryForm.runmode = "增加";


            queryForm.StartPosition = FormStartPosition.CenterParent;
            queryForm.ShowDialog();

            if (queryForm.DialogResult != DialogResult.Cancel)
            {
                this.ly_sales_contract_main_forbusinessTableAdapter.Fill(this.lYSalseMange.ly_sales_contract_main_forbusiness, nowcontractcode);



                this.ly_sales_contract_main_forbusinessBindingSource.Position = this.ly_sales_contract_main_forbusinessBindingSource.Find("内部编码", queryForm.contractinner_code);

                //this.ly_sales_contract_main1DataGridView.SelectionChanged -= ly_sales_contract_main1DataGridView_SelectionChanged;
                //this.ly_sales_contract_main1TableAdapter.Fill(this.lYSalseMange.ly_sales_contract_main1, this.nowusercode, "single", this.dateTimePicker1.Value, this.dateTimePicker2.Value);
                //this.ly_sales_contract_main1DataGridView.SelectionChanged += ly_sales_contract_main1DataGridView_SelectionChanged;
            }
        }

        private void toolStripButton20_Click(object sender, EventArgs e)
        {
            if (null == ly_sales_contract_mainDataGridView.CurrentRow)
            {


                return;
            }

            if ("True" == ly_sales_contract_mainDataGridView.CurrentRow.Cells["提交"].Value.ToString())
            {
                MessageBox.Show("合同已经提交,不能修改数据...", "注意");
                return;

            }

            if ("True" == ly_sales_contract_mainDataGridView.CurrentRow.Cells["批准"].Value.ToString())
            {
                MessageBox.Show("合同已经执行,不能修改数据...", "注意");
                return;

            }

            //if ("True" == ly_sales_contract_mainDataGridView.CurrentRow.Cells["审核"].Value.ToString())
            //{
            //    MessageBox.Show("合同已经审批,不能修改数据...", "注意");
            //    return;

            //}

            //string nowclientcode = this.ly_sales_clientDataGridView.CurrentRow.Cells["客户编码"].Value.ToString();
            string nowinnercode = this.ly_sales_contract_mainDataGridView.CurrentRow.Cells["内部编码"].Value.ToString();
            string nowcontractcode = this.ly_sales_businessDataGridView.CurrentRow.Cells["业务编码"].Value.ToString();

            LY_Salescontract_Add queryForm = new LY_Salescontract_Add();

            queryForm.salesclientcode = "111";
            queryForm.contractinner_code = nowinnercode;
            queryForm.runmode = "修改";


            queryForm.StartPosition = FormStartPosition.CenterParent;
            queryForm.ShowDialog();

            if (queryForm.DialogResult != DialogResult.Cancel)
            {
                this.ly_sales_contract_main_forbusinessTableAdapter.Fill(this.lYSalseMange.ly_sales_contract_main_forbusiness, nowcontractcode);



                this.ly_sales_contract_main_forbusinessBindingSource.Position = this.ly_sales_contract_main_forbusinessBindingSource.Find("内部编码", queryForm.contractinner_code);
                //this.ly_sales_contract_main1DataGridView.SelectionChanged -= ly_sales_contract_main1DataGridView_SelectionChanged;
                //this.ly_sales_contract_main1TableAdapter.Fill(this.lYSalseMange.ly_sales_contract_main1, this.nowusercode, "single", this.dateTimePicker1.Value, this.dateTimePicker2.Value);
                //this.ly_sales_contract_main1DataGridView.SelectionChanged += ly_sales_contract_main1DataGridView_SelectionChanged;
            }
        }

        private void toolStripButton22_Click(object sender, EventArgs e)
        {

            if (null == ly_sales_contract_mainDataGridView.CurrentRow)
            {


                return;
            }

            if ("True" == ly_sales_contract_mainDataGridView.CurrentRow.Cells["提交"].Value.ToString())
            {
                MessageBox.Show("合同已经提交,不能删除数据...", "注意");
                return;

            }

            if ("True" == ly_sales_contract_mainDataGridView.CurrentRow.Cells["批准"].Value.ToString())
            {
                MessageBox.Show("合同已经执行,不能删除数据...", "注意");
                return;

            }

            //if ("True" == ly_sales_contract_mainDataGridView.CurrentRow.Cells["审核"].Value.ToString())
            //{
            //    MessageBox.Show("合同已经审批,不能删除数据...", "注意");
            //    return;

            //}


            string message = "确定删除当前合同记录吗？";
            string caption = "提示...";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result;



            result = MessageBox.Show(message, caption, buttons,
            MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

            if (result == DialogResult.Yes)
            {

                this.ly_sales_contract_main_forbusinessBindingSource.RemoveCurrent();


                ly_sales_contract_mainDataGridView.EndEdit();
                ly_sales_contract_main_forbusinessBindingSource.EndEdit();



                this.ly_sales_contract_main_forbusinessTableAdapter.Update(this.lYSalseMange.ly_sales_contract_main_forbusiness);

                //this.ly_sales_contract_main1DataGridView.SelectionChanged -= ly_sales_contract_main1DataGridView_SelectionChanged;
                //this.ly_sales_contract_main1TableAdapter.Fill(this.lYSalseMange.ly_sales_contract_main1, this.nowusercode, "single", this.dateTimePicker1.Value, this.dateTimePicker2.Value);
                //this.ly_sales_contract_main1DataGridView.SelectionChanged += ly_sales_contract_main1DataGridView_SelectionChanged;
            }
        }



        private void ly_sales_contract_mainDataGridView_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            DataGridView dgv = sender as DataGridView;

            if (null == dgv.CurrentRow) return;

            if (this.ly_sales_contract_mainDataGridView.CurrentRow.Cells["录入人c"].Value.ToString() != SQLDatabase.nowUserName())
            {

                MessageBox.Show("请" + this.ly_sales_contract_mainDataGridView.CurrentRow.Cells["录入人c"].Value.ToString() + "修改...", "注意");

                return;
            }


            if ("清单提交" == dgv.CurrentCell.OwningColumn.Name)
             {
                if (!string.IsNullOrEmpty(dgv.CurrentRow.Cells["开票人"].Value.ToString()))
                {
                    MessageBox.Show("已经开票...", "注意");
                    return;
                }
                if (!string.IsNullOrEmpty(dgv.CurrentRow.Cells["开票日期"].Value.ToString()))
                {
                    MessageBox.Show("已经开票...", "注意");
                    return;
                }

                if ("True" == dgv.CurrentRow.Cells["清单提交"].Value.ToString())
                {
                    dgv.CurrentRow.Cells["清单提交"].Value = "False";
                    dgv.CurrentRow.Cells["提交人"].Value = DBNull.Value; 
                    dgv.CurrentRow.Cells["提交时间"].Value = DBNull.Value; 

                }
                else
                {
                    string lspt = dgv.CurrentRow.Cells["依赖书号ht"].Value.ToString();
                    
                    string sql = " select dbo.f_check_inner_flag('"+ lspt + "')";
                    DataTable dt = null;
                    using (SqlConnection connection = new SqlConnection(SQLDatabase.Connectstring))
                    { 
                        SqlDataAdapter adapter = new SqlDataAdapter(sql, connection);
                        DataSet ds = new DataSet();
                        adapter.Fill(ds);
                        dt = ds.Tables[0];
                    }
                    if (dt.Rows[0][0].ToString() == "0")
                    {
                        dgv.CurrentRow.Cells["清单提交"].Value = "True";
                        string username = SQLDatabase.nowUserName();
                        DateTime dt2t = SQLDatabase.GetNowdate();
                        dgv.CurrentRow.Cells["提交人"].Value = username;
                        dgv.CurrentRow.Cells["提交时间"].Value = dt2t;
                    }
                    else
                    {
                        MessageBox.Show("该配套书没有全部发货，无法提交...", "注意"); 
                        return;
                    }
                }


                SaveContract(); 

                return;

            }


            ///////////////////////////////////////////////////////////////

            if ("开票日期" == dgv.CurrentCell.OwningColumn.Name)
            {

                //if ("True" == dgv.CurrentRow.Cells["已交"].Value.ToString())
                //{
                //    MessageBox.Show("合同文本已经提交,不能修改交付日期...", "注意");

                //    return;
                //}

                return;

                ChangeValue queryForm = new ChangeValue();

                queryForm.OldValue = dgv.CurrentCell.Value.ToString();
                queryForm.NewValue = "";
                queryForm.ChangeMode = "datetime";
                queryForm.ShowDialog();


                if (queryForm.NewValue != "")
                {
                    dgv.CurrentRow.Cells["开票日期"].Value = queryForm.NewValue;
                    dgv.CurrentRow.Cells["开票人"].Value = "宋美彰";

                }
                else
                {

                    dgv.CurrentRow.Cells["开票日期"].Value = DBNull.Value;
                    dgv.CurrentRow.Cells["开票人"].Value = DBNull.Value;

                }



                SaveContract();



                return;

            }
            ////////////////////////////////////////////////////////////////////////
            if ("开票人" == dgv.CurrentCell.OwningColumn.Name)
            {

                //if ("True" == dgv.CurrentRow.Cells["已交"].Value.ToString())
                //{
                //    MessageBox.Show("合同文本已经提交,不能修改交付日期...", "注意");

                //    return;
                //}
                return;
                ChangeValue queryForm = new ChangeValue();

                queryForm.OldValue = dgv.CurrentCell.Value.ToString();
                queryForm.NewValue = "";
                queryForm.ChangeMode = "string";
                queryForm.ShowDialog();


                if (queryForm.NewValue != "")
                {
                    dgv.CurrentRow.Cells["开票人"].Value = queryForm.NewValue;


                }
                else
                {

                    dgv.CurrentRow.Cells["开票人"].Value = DBNull.Value;


                }



                SaveContract();



                return;

            }
            //////////////////////////////////////







            if ("True" != dgv.CurrentRow.Cells["发货"].Value.ToString())
            {

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

                        SaveContract();

                    }
                    else
                    {

                    }
                    return;

                }
                ////////////////////////////////////////////


                //if ("出口" == dgv.CurrentCell.OwningColumn.Name)
                //{

                //    ChangeValue queryForm = new ChangeValue();

                //    queryForm.OldValue = dgv.CurrentCell.Value.ToString();
                //    queryForm.NewValue = "";
                //    queryForm.ChangeMode = "value";
                //    queryForm.ShowDialog();




                //    if (queryForm.NewValue != "")
                //    {
                //        dgv.CurrentRow.Cells["税率"].Value = queryForm.NewValue;

                //        SaveContract();

                //    }
                //    else
                //    {

                //    }
                //    return;

                //}
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
                        SaveContract();

                        //CountPlanStru();

                    }
                    else
                    {
                        dgv.CurrentRow.Cells["备注"].Value = DBNull.Value;
                        SaveContract();
                    }
                    return;

                }


                /////////////////////////////////////////////////////////

                /////////////////////////////////////////////////////////
            }

            ////////////////////////////////////////////////////////////////////////

            if ("True" == dgv.CurrentRow.Cells["发货"].Value.ToString())
            {

                /////////////////////////////
                if ("备注" == dgv.CurrentCell.OwningColumn.Name)
                {

                    ChangeValue queryForm = new ChangeValue();

                    string nowoldvalue = dgv.CurrentCell.Value.ToString();

                    queryForm.OldValue = "";
                    queryForm.NewValue = "";
                    queryForm.ChangeMode = "longstring";
                    queryForm.ShowDialog();




                    if (queryForm.NewValue != "")
                    {
                        dgv.CurrentRow.Cells["备注"].Value = nowoldvalue + queryForm.NewValue;
                        //dgv.CurrentRow.Cells["discount_money"].Value = DBNull.Value;

                        //dgv.CurrentRow.Cells["approve_flag"].Value = "False";
                        SaveContract();

                        //CountPlanStru();

                    }
                    else
                    {
                        dgv.CurrentRow.Cells["备注"].Value = nowoldvalue;
                        SaveContract();
                    }
                    return;

                }






                /////////////////////////////////////////////////////////////////

                MessageBox.Show("合同已经发货,不能修改数据...", "注意");
                return;

            }



            if ("批准" == dgv.CurrentCell.OwningColumn.Name)
            {

                //if ("True" == dgv.CurrentRow.Cells["批准"].Value.ToString())
                //{
                //    MessageBox.Show("合同文本已经提交,不能修改交付日期...", "注意");

                //    return;
                //}

                //ChangeValue queryForm = new ChangeValue();

                //queryForm.OldValue = dgv.CurrentCell.Value.ToString();
                //queryForm.NewValue = "";
                //queryForm.ChangeMode = "datetime";
                //queryForm.ShowDialog();


                //if (queryForm.NewValue != "")
                //{
                //    dgv.CurrentRow.Cells["合同文本交付"].Value = queryForm.NewValue;

                //}
                //else
                //{

                //    dgv.CurrentRow.Cells["合同文本交付"].Value = DBNull.Value;

                //}

                if ("True" == dgv.CurrentRow.Cells["批准"].Value.ToString())
                {
                    dgv.CurrentRow.Cells["批准"].Value = "False";
                    dgv.CurrentRow.Cells["已交"].Value = "False";
                    dgv.CurrentRow.Cells["批准人"].Value = DBNull.Value;
                    dgv.CurrentRow.Cells["批准日期"].Value = DBNull.Value;
                    dgv.CurrentRow.Cells["合同文本交付"].Value = DBNull.Value;
                }
                else
                {
                    //if ("True" == dgv.CurrentRow.Cells["赠送1"].Value.ToString())
                    //{
                    //    if ( string.IsNullOrEmpty( dgv.CurrentRow.Cells["项目编码"].Value.ToString()))
                    //    {
                    //        MessageBox.Show("技术部未设置研发项目,不能修改数据...", "注意");
                    //        return;
                    //    }

                    //}

                    dgv.CurrentRow.Cells["批准"].Value = "True";
                    dgv.CurrentRow.Cells["已交"].Value = "True";
                    dgv.CurrentRow.Cells["批准人"].Value = SQLDatabase.nowUserName();
                    dgv.CurrentRow.Cells["批准日期"].Value = SQLDatabase.GetNowdate();
                    dgv.CurrentRow.Cells["合同文本交付"].Value = SQLDatabase.GetNowdate();
                }


                SaveContract();



                return;

            }
            ////////////////////////////////////////////////////////////////////////

            ///////////////////////////////////////////////////////////////

            if ("合同文本交付" == dgv.CurrentCell.OwningColumn.Name)
            {

                //if ("True" == dgv.CurrentRow.Cells["已交"].Value.ToString())
                //{
                //    MessageBox.Show("合同文本已经提交,不能修改交付日期...", "注意");

                //    return;
                //}

                ChangeValue queryForm = new ChangeValue();

                queryForm.OldValue = dgv.CurrentCell.Value.ToString();
                queryForm.NewValue = "";
                queryForm.ChangeMode = "datetime";
                queryForm.ShowDialog();


                if (queryForm.NewValue != "")
                {
                    dgv.CurrentRow.Cells["合同文本交付"].Value = queryForm.NewValue;
                    dgv.CurrentRow.Cells["已交"].Value = "True";

                }
                else
                {

                    dgv.CurrentRow.Cells["合同文本交付"].Value = DBNull.Value;
                    dgv.CurrentRow.Cells["已交"].Value = "False";

                }



                SaveContract();



                return;

            }
            ////////////////////////////////////////////////////////////////////////

            ///////////////////////////////////////////////////////////////

            if ("批准日期" == dgv.CurrentCell.OwningColumn.Name)
            {

                //if ("True" == dgv.CurrentRow.Cells["已交"].Value.ToString())
                //{
                //    MessageBox.Show("合同文本已经提交,不能修改交付日期...", "注意");

                //    return;
                //}

                ChangeValue queryForm = new ChangeValue();

                queryForm.OldValue = dgv.CurrentCell.Value.ToString();
                queryForm.NewValue = "";
                queryForm.ChangeMode = "datetime";
                queryForm.ShowDialog();


                if (queryForm.NewValue != "")
                {
                    dgv.CurrentRow.Cells["批准日期"].Value = queryForm.NewValue;


                }
                else
                {

                    dgv.CurrentRow.Cells["批准日期"].Value = DBNull.Value;


                }



                SaveContract();



                return;

            }
            ////////////////////////////////////////////////////////////////////////

            //if ("True" == dgv.CurrentRow.Cells["批准"].Value.ToString())
            //{
            //    MessageBox.Show("合同已经执行,不能修改数据...", "注意");
            //    return;

            //}


            /////////////////////////////////////////////////////////////
            //if ("提交" == dgv.CurrentCell.OwningColumn.Name)
            //{
            //    if (null == ly_sales_contract_detailDataGridView.CurrentRow) return;

            //    foreach (DataGridViewRow dgr in ly_sales_contract_detailDataGridView.Rows)
            //    {

            //        if (Color.Red == dgr.Cells["单件折扣"].Style.BackColor)
            //        {
            //            MessageBox.Show("折扣超权限,请修改合同单价,或者请上级审批,提交取消...", "注意");
            //            return;

            //        }

            //    }


            //    LY_Salescontract_SubmitSet queryForm = new LY_Salescontract_SubmitSet();

            //    string nowbillcode = ly_sales_contract_mainDataGridView.CurrentRow.Cells["依赖书号ht"].Value.ToString();

            //    queryForm.billcode = nowbillcode;
            //    queryForm.clientcode = this.nowclientCode;
            //    queryForm.contractcode = this.nowcontractCode;
            //    queryForm.innercode = this .nowinnerCode;
            //    queryForm.fromwhere = "contract";



            //    queryForm.runmode = "合同提交";

            //    queryForm.WindowState = FormWindowState.Maximized;
            //    queryForm.StartPosition = FormStartPosition.CenterParent;
            //    queryForm.ShowDialog();

            //    if (queryForm.DialogResult == DialogResult.Cancel)
            //    {
            //        dgv.CurrentRow.Cells["提交"].Value = "False";
            //        ly_sales_contract_mainDataGridView.CurrentRow.Cells["依赖书号ht"].Value = DBNull .Value ;
            //        //return;
            //    }
            //    else
            //    {
            //        ly_sales_contract_mainDataGridView.CurrentRow.Cells["依赖书号ht"].Value = queryForm.billcode;
            //        ly_sales_contract_mainDataGridView.EndEdit();

            //        if ("True" == dgv.CurrentRow.Cells["提交"].Value.ToString())
            //        {
            //            dgv.CurrentRow.Cells["提交"].Value = "False";

            //        }
            //        else
            //        {

            //            dgv.CurrentRow.Cells["提交"].Value = "True";
            //        }



            //       // SaveContract();
            //    }



            //    SaveContract();


            //    return;

            //}
            /////////////////////////////////////////////////////////////////


            //if ("True" == dgv.CurrentRow.Cells["提交"].Value.ToString())
            //{
            //    MessageBox.Show("合同已经提交,不能修改数据...", "注意");
            //    return;

            //}


            ////if ("True" == dgv.CurrentRow.Cells["审核"].Value.ToString())
            ////{
            ////    MessageBox.Show("合同已经审批,不能修改数据...", "注意");
            ////    return;

            ////}





            //if ("免税" == dgv.CurrentCell.OwningColumn.Name)
            //{

            //    if ("True" == dgv.CurrentRow.Cells["免税"].Value.ToString())
            //    {
            //        dgv.CurrentRow.Cells["免税"].Value = "False";

            //    }
            //    else
            //    {

            //        dgv.CurrentRow.Cells["免税"].Value = "True";
            //    }



            //    SaveContract();



            //    return;

            //}
            ///////////////////////////////////////////////////////////////

            //if ("借用" == dgv.CurrentCell.OwningColumn.Name)
            //{

            //    if ("True" == dgv.CurrentRow.Cells["借用"].Value.ToString())
            //    {
            //        dgv.CurrentRow.Cells["借用"].Value = "False";

            //    }
            //    else
            //    {

            //        dgv.CurrentRow.Cells["借用"].Value = "True";

            //    }



            //    SaveContract();



            //    return;

            //}
            ///////////////////////////////////////////////////////////////

            //if ("审核" == dgv.CurrentCell.OwningColumn.Name)
            //{

            //    if ("True" == dgv.CurrentRow.Cells["审核"].Value.ToString())
            //    {
            //        dgv.CurrentRow.Cells["审核"].Value = "False";
            //        dgv.CurrentRow.Cells["审核人"].Value = DBNull.Value;
            //    }
            //    else
            //    {

            //        dgv.CurrentRow.Cells["审核"].Value = "True";
            //        dgv.CurrentRow.Cells["审核人"].Value = SQLDatabase.nowUserName();
            //    }



            //    SaveContract();



            //    return;

            //}
            //////////////////////////////////////////////////////////////////////////
            /////////////////////////////////////////////////////////////////
            //if ("已交" == dgv.CurrentCell.OwningColumn.Name)
            //{

            //    if ("True" == dgv.CurrentRow.Cells["已交"].Value.ToString())
            //    {
            //        dgv.CurrentRow.Cells["已交"].Value = "False";

            //    }
            //    else
            //    {

            //        dgv.CurrentRow.Cells["已交"].Value = "True";
            //    }



            //    SaveContract();



            //    return;

            //}


            //if ("开票日期" == dgv.CurrentCell.OwningColumn.Name)
            //{

            //    ChangeValue queryForm = new ChangeValue();

            //    queryForm.OldValue = dgv.CurrentCell.Value.ToString();
            //    queryForm.NewValue = "";
            //    queryForm.ChangeMode = "datetime";
            //    queryForm.ShowDialog();


            //    if (queryForm.NewValue != "")
            //    {
            //        dgv.CurrentRow.Cells["开票日期"].Value = queryForm.NewValue;
            //        dgv.CurrentRow.Cells["开票人"].Value = SQLDatabase.nowUserName();
            //    }
            //    else
            //    {

            //        dgv.CurrentRow.Cells["开票日期"].Value = DBNull.Value;
            //        dgv.CurrentRow.Cells["开票人"].Value = DBNull.Value;
            //    }



            //    SaveContract();



            //    return;

            //}
            //////////////////////////////////////////////////////////////////////////


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
            //        SaveContract();

            //        //CountPlanStru();

            //    }
            //    else
            //    {

            //    }
            //    return;

            //}


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


            /////////////////////////////////////////////////////////

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
            //if ("公司" == dgv.CurrentCell.OwningColumn.Name)
            //{






            //    string sel;



            //    sel = "SELECT  company_code as 编码, company_name as 名称 FROM ly_company_information ";



            //    QueryForm queryForm = new QueryForm();


            //    queryForm.Sel = sel;
            //    queryForm.Constr = SQLDatabase.Connectstring;

            //    //Set the Column Collection to the filter Table
            //    //queryForm.SetSourceColumns(this.billMainDataSet.BalanceBill.Columns);

            //    queryForm.ShowDialog();


            //    if (queryForm.Result != "")
            //    {
            //        dgv.CurrentRow.Cells["公司编码"].Value = queryForm.Result;
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
            this.ly_sales_contract_mainDataGridView.CurrentRow.Cells["录入人c"].Value = SQLDatabase.nowUserName();

            this.ly_sales_contract_mainDataGridView.EndEdit();
            this.ly_sales_contract_main_forbusinessBindingSource.EndEdit();

            try
            {

                this.ly_sales_contract_main_forbusinessTableAdapter.Update(this.lYSalseMange.ly_sales_contract_main_forbusiness);




            }
            catch (SqlException sqle)
            {


                MessageBox.Show(sqle.Message.Split('*')[0]);
            }







            RefreshData();
        }

        private void RefreshData()
        {









            int nowcontractId;
            if (null != ly_sales_contract_mainDataGridView.CurrentRow)
            {
                nowcontractId = int.Parse(ly_sales_contract_mainDataGridView.CurrentRow.Cells["id_main"].Value.ToString());
            }
            else
            {
                nowcontractId = 0;
            }

            //int nowcontractdetailId;
            //if (null != ly_purchase_contract_detailDataGridView.CurrentRow)
            //{
            //    nowcontractdetailId = int.Parse(ly_purchase_contract_detailDataGridView.CurrentRow.Cells["id7"].Value.ToString());
            //}
            //else
            //{
            //    nowcontractdetailId = 0;
            //}



            int nowcontractdetailid;
            if (null != ly_sales_contract_detailDataGridView.CurrentRow)
            {
                nowcontractdetailid = int.Parse(ly_sales_contract_detailDataGridView.CurrentRow.Cells["id_detail"].Value.ToString());
            }
            else
            {
                nowcontractdetailid = 0;
            }


            string nowclientcode = this.ly_sales_contract_mainDataGridView.CurrentRow.Cells["客户编码c"].Value.ToString();




            this.ly_sales_contract_main_forbusinessTableAdapter.Fill(this.lYSalseMange.ly_sales_contract_main_forbusiness, nowcontractCode);
            this.ly_sales_contract_terms_forcontractBindingSource.Position = this.ly_sales_contract_terms_forcontractBindingSource.Find("id", nowcontractId);

            //this.ly_sales_contract_main1DataGridView.SelectionChanged -= ly_sales_contract_main1DataGridView_SelectionChanged;
            //this.ly_sales_contract_main1TableAdapter.Fill(this.lYSalseMange.ly_sales_contract_main1, this.nowusercode, "single", this.dateTimePicker1.Value, this.dateTimePicker2.Value);
            //this.ly_sales_contract_main1DataGridView.SelectionChanged += ly_sales_contract_main1DataGridView_SelectionChanged;

            this.ly_sales_contract_detailBindingSource.Position = this.ly_sales_contract_detailBindingSource.Find("id", nowcontractdetailid);
            //this.ly_purchase_contract_detailBindingSource.Position = this.ly_purchase_contract_detailBindingSource.Find("物料编号", nowcontractdetailcode);

        }

        private void toolStripTextBox6_KeyUp(object sender, KeyEventArgs e)
        {
            string filterString;
            ToolStripTextBox tst = sender as ToolStripTextBox;

            filterString = GetDataGridviewMultiFilterString.DGVMultiFilterString(this.ly_lsptb_selDataGridView, tst.Text);


            this.ly_lsptb_selBindingSource.Filter = filterString;
        }

        private void toolStripTextBox6_Enter(object sender, EventArgs e)
        {
            ToolStripTextBox tst = sender as ToolStripTextBox;
            tst.Text = "";

            this.ly_lsptb_selBindingSource.Filter = "";
        }

        private void ly_lsptb_selDataGridView_DoubleClick(object sender, EventArgs e)
        {
            if (null == this.ly_sales_contract_mainDataGridView.CurrentRow) return;
            if (null == ly_lsptb_selDataGridView.CurrentRow) return;

            if ("False" == this.contractCanchenged)
            {
                MessageBox.Show("合同已经提交执行,不能修改数据...", "注意");
                return;

            }

            decimal nowsalesprice;
            if ("" != this.ly_lsptb_selDataGridView.CurrentRow.Cells["营业定价3"].Value.ToString())
            {
                nowsalesprice = decimal.Parse(this.ly_lsptb_selDataGridView.CurrentRow.Cells["营业定价3"].Value.ToString());
            }
            else
            {
                nowsalesprice = 0;
            }

            if (nowsalesprice == 0)
            {

                MessageBox.Show("产品无定价,不能销售...", "注意");
                return;
            }



            string nowitemno = this.ly_lsptb_selDataGridView.CurrentRow.Cells["物料编号2"].Value.ToString();







            string isborrow = this.ly_sales_contract_mainDataGridView.CurrentRow.Cells["借用"].Value.ToString();

            InserSalseContractDetail(this.nowinnerCode, nowitemno, 1, isborrow);



            this.ly_sales_contract_detailTableAdapter.Fill(this.lYSalseMange.ly_sales_contract_detail, nowinnerCode, 0);

            this.ly_sales_contract_detailBindingSource.Position = this.ly_sales_contract_detailBindingSource.Find("产品编码", nowitemno);

            this.ly_sales_contract_detailDataGridView.CurrentCell = this.ly_sales_contract_detailDataGridView.CurrentRow.Cells["单价"];
        }
        private static void InserSalseContractDetail(string innerCode, string nowitem, decimal nowabsqty, string isborrow)
        {

            string insStr;

            if ("True" == isborrow)
            {
                insStr = " INSERT INTO ly_sales_contract_detail  " +
             "( contract_inner_code,itemno,brrow_qty) " +
             " values ('" + innerCode + "','" + nowitem + "'," + nowabsqty + ")";
            }
            else
            {
                insStr = " INSERT INTO ly_sales_contract_detail  " +
              "( contract_inner_code,itemno,qty) " +
              " values ('" + innerCode + "','" + nowitem + "'," + nowabsqty + ")";
            }


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

        private void ly_sales_contract_detailDataGridView_CellFormatting_1(object sender, DataGridViewCellFormattingEventArgs e)
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

        private void ly_sales_contract_detailDataGridView_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            string isgood = "yes";

            decimal nowrealdis;

            decimal nowuserdis;
            decimal nowclientdis;
            decimal nowproductdis;

            foreach (DataGridViewRow dgr in ly_sales_contract_detailDataGridView.Rows)
            {


                if ("" != dgr.Cells["单件折扣"].Value.ToString())
                {
                    nowrealdis = decimal.Parse(dgr.Cells["单件折扣"].Value.ToString());
                }
                else
                {
                    nowrealdis = 0;
                }
                ///////////////////////////////////////////////////////////////////////

                if ("" != dgr.Cells["产品折扣"].Value.ToString())
                {
                    nowproductdis = decimal.Parse(dgr.Cells["产品折扣"].Value.ToString());
                }
                else
                {
                    nowproductdis = 0;
                }
                /////////////////////////////////////////////////////////////////////////

                if ("" != dgr.Cells["营业员折扣"].Value.ToString())
                {
                    nowuserdis = decimal.Parse(dgr.Cells["营业员折扣"].Value.ToString());
                }
                else
                {
                    nowuserdis = 0;
                }
                ////////////////////////////////////////////////////////////////////////////////////

                if ("" != dgr.Cells["客户折扣"].Value.ToString())
                {
                    nowclientdis = decimal.Parse(dgr.Cells["客户折扣"].Value.ToString());
                }
                else
                {
                    nowclientdis = 0;
                }
                /////////////////////////////////////////////////////////////////////////////////////////////////////////




                if ("True" != dgr.Cells["审批"].Value.ToString())
                {

                    if (nowrealdis > (nowproductdis * nowuserdis) / 100 && nowrealdis > nowclientdis)
                    {
                        foreach (DataGridViewCell dgc in dgr.Cells)
                        {

                            dgc.Style.BackColor = Color.Red;
                            dgc.Style.ForeColor = Color.White;
                        }
                    }
                }
                //else
                //{ 


                //}



            }
        }

        private void SaveDetailItem()
        {
            this.ly_sales_contract_detailBindingSource.EndEdit();
            this.ly_sales_contract_detailTableAdapter.Update(this.lYSalseMange.ly_sales_contract_detail);

            int nowdetailId;
            if (null != ly_sales_contract_detailDataGridView.CurrentRow)
            {
                nowdetailId = int.Parse(ly_sales_contract_detailDataGridView.CurrentRow.Cells["id_detail"].Value.ToString());
            }
            else
            {
                nowdetailId = 0;
            }


            int nowcontractId;
            if (null != ly_sales_contract_mainDataGridView.CurrentRow)
            {
                nowcontractId = int.Parse(ly_sales_contract_mainDataGridView.CurrentRow.Cells["id_main"].Value.ToString());
            }
            else
            {
                nowcontractId = 0;
            }


            //if (null == this.ly_sales_clientDataGridView.CurrentRow)
            //{ 
            //  this.ly_sales_clientDataGridView.
            //}

            //string nowclientcode = this.ly_sales_clientDataGridView.CurrentRow.Cells["客户编码"].Value.ToString();
            //this.ly_sales_clientDataGridView.SelectionChanged -= ly_sales_clientDataGridView_SelectionChanged;
            //this.ly_sales_clientTableAdapter.Fill(this.lYSalseMange.ly_sales_client);
            //this.ly_sales_clientDataGridView.SelectionChanged += ly_sales_clientDataGridView_SelectionChanged;

            //this.ly_sales_clientBindingSource.Position = this.ly_sales_clientBindingSource.Find("客户编码", nowclientCode);

            //this.ly_sales_contract_mainDataGridView.CurrentRow.Cells["录入人c"].Value = SQLDatabase.nowUserName();
            this.ly_sales_contract_mainDataGridView.EndEdit();
            this.ly_sales_contract_main_forbusinessBindingSource.EndEdit();
            this.ly_sales_contract_main_forbusinessTableAdapter.Update(this.lYSalseMange.ly_sales_contract_main_forbusiness);

            this.ly_sales_contract_main_forbusinessTableAdapter.Fill(this.lYSalseMange.ly_sales_contract_main_forbusiness, this.nowcontractCode);
            this.ly_sales_contract_main_forbusinessBindingSource.Position = this.ly_sales_contract_main_forbusinessBindingSource.Find("id", nowcontractId);

            //this.ly_sales_contract_main1DataGridView.SelectionChanged -= ly_sales_contract_main1DataGridView_SelectionChanged;
            //this.ly_sales_contract_main1TableAdapter.Fill(this.lYSalseMange.ly_sales_contract_main1, this.nowusercode, this.nowfillstragecode, this.dateTimePicker1.Value, this.dateTimePicker2.Value);
            //this.ly_sales_contract_main1DataGridView.SelectionChanged += ly_sales_contract_main1DataGridView_SelectionChanged;

            //this.ly_sales_contract_detailTableAdapter.Fill(this.lYSalseMange.ly_sales_contract_detail, nowinnerCode);

            this.ly_sales_contract_detailBindingSource.Position = this.ly_sales_contract_detailBindingSource.Find("id", nowdetailId);

            this.ly_sales_contract_detailDataGridView.CurrentRow.Cells["单件折扣"].Value = this.ly_sales_contract_detailDataGridView.CurrentRow.Cells["单件折扣"].Value;
            this.ly_sales_contract_detailDataGridView.CurrentRow.Cells["单价"].Value = this.ly_sales_contract_detailDataGridView.CurrentRow.Cells["单价"].Value;

            foreach (DataGridViewRow dgr in ly_sales_contract_detailDataGridView.Rows)
            {
                dgr.Cells["顺序"].Value = dgr.Index + 1;

            }

            this.ly_sales_contract_detailDataGridView.EndEdit();
            this.ly_sales_contract_detailBindingSource.EndEdit();
            this.ly_sales_contract_detailTableAdapter.Update(this.lYSalseMange.ly_sales_contract_detail);
        }

        private void ly_sales_contract_detailDataGridView_CellMouseDoubleClick_1(object sender, DataGridViewCellMouseEventArgs e)
        {
            DataGridView dgv = sender as DataGridView;

            if (null == dgv.CurrentRow) return;



            if ("True" == ly_sales_contract_mainDataGridView.CurrentRow.Cells["批准"].Value.ToString()
                  && "中成折扣" != dgv.CurrentCell.OwningColumn.Name
                  )
            {
                MessageBox.Show("合同已经执行,不能修改数据...", "注意");
                return;

            }

            //////////////////////////////////////

            if ("赠送" == dgv.CurrentCell.OwningColumn.Name)
            {

                if ("True" == dgv.CurrentRow.Cells["赠送"].Value.ToString())
                {
                    dgv.CurrentRow.Cells["赠送"].Value = "False";

                }
                else
                {

                    dgv.CurrentRow.Cells["赠送"].Value = "True";
                }



                SaveDetailItem();



                return;

            }

            ///////////////////////////////////////////////////////////////
            if ("False" == this.contractCanchenged
                 && "中成折扣" != dgv.CurrentCell.OwningColumn.Name
                )
            {
                MessageBox.Show("合同已经提交执行,不能修改数据...", "注意");
                return;

            }

            //////////////////////////////////////

            ///////////////////////////////////////////////////////////////
            if ("商标" == dgv.CurrentCell.OwningColumn.Name)
            {

                ChangeValue queryForm = new ChangeValue();

                queryForm.OldValue = dgv.CurrentCell.Value.ToString();
                queryForm.NewValue = "";
                queryForm.ChangeMode = "string";
                queryForm.ShowDialog();




                if (queryForm.NewValue != "")
                {
                    dgv.CurrentRow.Cells["商标"].Value = queryForm.NewValue;
                    //dgv.CurrentRow.Cells["discount_money"].Value = DBNull.Value;

                    //dgv.CurrentRow.Cells["approve_flag"].Value = "False";
                    SaveDetailItem();

                    //CountPlanStru();

                }
                else
                {

                }
                return;

            }
            ///////////////////////////////////////////////////////////////
            if ("数量" == dgv.CurrentCell.OwningColumn.Name)
            {

                if ("True" == dgv.CurrentRow.Cells["审批"].Value.ToString())
                {

                    MessageBox.Show("合同已经审批,不能修改数据...", "注意");
                    return;
                }

                if ("True" == this.isborrow)
                {

                    MessageBox.Show("借用合同已经审批,不能输入合同数量...", "注意");
                    return;
                }

                ChangeValue queryForm = new ChangeValue();

                queryForm.OldValue = dgv.CurrentCell.Value.ToString();
                queryForm.NewValue = "";
                queryForm.ChangeMode = "value";
                queryForm.ShowDialog();




                if (queryForm.NewValue != "")
                {
                    dgv.CurrentRow.Cells["数量"].Value = queryForm.NewValue;
                    //dgv.CurrentRow.Cells["discount_money"].Value = DBNull.Value;

                    //dgv.CurrentRow.Cells["approve_flag"].Value = "False";
                    SaveDetailItem();


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
            if ("借用数" == dgv.CurrentCell.OwningColumn.Name)
            {

                if ("True" == dgv.CurrentRow.Cells["审批"].Value.ToString())
                {

                    MessageBox.Show("合同已经审批,不能修改数据...", "注意");
                    return;
                }

                if ("False" == this.isborrow)
                {

                    MessageBox.Show("普通合同已经审批,不能输入借用数量...", "注意");
                    return;
                }

                ChangeValue queryForm = new ChangeValue();

                queryForm.OldValue = dgv.CurrentCell.Value.ToString();
                queryForm.NewValue = "";
                queryForm.ChangeMode = "value";
                queryForm.ShowDialog();




                if (queryForm.NewValue != "")
                {
                    dgv.CurrentRow.Cells["借用数"].Value = queryForm.NewValue;
                    //dgv.CurrentRow.Cells["discount_money"].Value = DBNull.Value;

                    //dgv.CurrentRow.Cells["approve_flag"].Value = "False";
                    SaveDetailItem();


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
            if ("中成折扣" == dgv.CurrentCell.OwningColumn.Name)
            {

                //if ("True" == dgv.CurrentRow.Cells["审批"].Value.ToString())
                //{

                //    MessageBox.Show("合同已经审批,不能修改数据...", "注意");
                //    return;
                //}

                //if ("False" == this.isborrow)
                //{

                //    MessageBox.Show("普通合同已经审批,不能输入借用数量...", "注意");
                //    return;中成折扣设置
                //}



                if (!SQLDatabase.CheckHaveRight(SQLDatabase.NowUserID, "中成折扣设置"))
                {
                    return;
                }

                ChangeValue queryForm = new ChangeValue();

                queryForm.OldValue = dgv.CurrentCell.Value.ToString();
                queryForm.NewValue = "";
                queryForm.ChangeMode = "value";
                queryForm.ShowDialog();




                if (queryForm.NewValue != "")
                {
                    dgv.CurrentRow.Cells["中成折扣"].Value = queryForm.NewValue;
                    //dgv.CurrentRow.Cells["discount_money"].Value = DBNull.Value;

                    //dgv.CurrentRow.Cells["approve_flag"].Value = "False";
                    SaveDetailItem();


                    //CountPlanStru();

                }
                else
                {
                    dgv.CurrentRow.Cells["中成折扣"].Value = DBNull.Value;
                    //dgv.CurrentRow.Cells["discount_money"].Value = DBNull.Value;

                    //dgv.CurrentRow.Cells["approve_flag"].Value = "False";
                    SaveDetailItem();
                }
                return;

            }

            ///////////////////////////////////////////////////////////////
            if ("单价" == dgv.CurrentCell.OwningColumn.Name)
            {



                if ("True" == dgv.CurrentRow.Cells["审批"].Value.ToString())
                {

                    MessageBox.Show("合同已经审批,不能修改数据...", "注意");
                    return;
                }

                ChangeValue queryForm = new ChangeValue();

                queryForm.OldValue = dgv.CurrentCell.Value.ToString();
                queryForm.NewValue = "";
                queryForm.ChangeMode = "value";
                queryForm.setInFocus();
                queryForm.ShowDialog(this);





                if (queryForm.NewValue != "")
                {
                    dgv.CurrentRow.Cells["单价"].Value = queryForm.NewValue;

                    decimal nowsalesprice;

                    if ("" != dgv.CurrentRow.Cells["营业定价"].Value.ToString())
                    {
                        nowsalesprice = decimal.Parse(dgv.CurrentRow.Cells["营业定价"].Value.ToString());
                    }
                    else
                    {
                        nowsalesprice = 1;
                    }

                    dgv.CurrentRow.Cells["单件折扣"].Value = (nowsalesprice - decimal.Parse(queryForm.NewValue)) / nowsalesprice * 100;
                    //dgv.CurrentRow.Cells["discount_money"].Value = DBNull.Value;

                    //dgv.CurrentRow.Cells["approve_flag"].Value = "False";
                    SaveDetailItem();


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


            /////////////////////////////////////////////////////
            ///////////////////////////////////////////////////////////////
            if ("营业定价" == dgv.CurrentCell.OwningColumn.Name)
            {


                //if (!SQLDatabase.CheckHaveRight(SQLDatabase.NowUserID, "营业综合信息"))
                //{
                //    return;
                //}

                if ("Z" != dgv.CurrentRow.Cells["产品编码"].Value.ToString().Substring(0, 1))
                {
                    return;
                }


                ChangeValue queryForm = new ChangeValue();

                queryForm.OldValue = dgv.CurrentCell.Value.ToString();
                queryForm.NewValue = "";
                queryForm.ChangeMode = "value";
                queryForm.setInFocus();
                queryForm.ShowDialog(this);




                if (queryForm.NewValue != "")
                {
                    dgv.CurrentRow.Cells["营业定价"].Value = queryForm.NewValue;

                    decimal nowitemprice;
                    decimal nowsalesprice;

                    nowsalesprice = decimal.Parse(queryForm.NewValue);

                    if (nowsalesprice > 0)
                    {

                    }
                    else
                    {
                        nowsalesprice = 1;
                    }

                    if ("" != dgv.CurrentRow.Cells["单价"].Value.ToString())
                    {
                        nowitemprice = decimal.Parse(dgv.CurrentRow.Cells["单价"].Value.ToString());
                    }
                    else
                    {
                        nowitemprice = 0;
                    }

                    dgv.CurrentRow.Cells["单件折扣"].Value = (nowsalesprice - nowitemprice) / nowsalesprice * 100;
                    //dgv.CurrentRow.Cells["discount_money"].Value = DBNull.Value;

                    //dgv.CurrentRow.Cells["approve_flag"].Value = "False";
                    SaveDetailItem();


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


            /////////////////////////////////////////////////////

            if ("单件折扣" == dgv.CurrentCell.OwningColumn.Name)
            {


                if ("True" == dgv.CurrentRow.Cells["审批"].Value.ToString())
                {

                    MessageBox.Show("合同已经审批,不能修改数据...", "注意");
                    return;
                }



                ChangeValue queryForm = new ChangeValue();

                queryForm.OldValue = dgv.CurrentCell.Value.ToString();
                queryForm.NewValue = "";
                queryForm.ChangeMode = "value";
                queryForm.ShowDialog();




                if (queryForm.NewValue != "")
                {
                    dgv.CurrentRow.Cells["单件折扣"].Value = queryForm.NewValue;

                    decimal nowsalesprice;

                    if ("" != dgv.CurrentRow.Cells["营业定价"].Value.ToString())
                    {
                        nowsalesprice = decimal.Parse(dgv.CurrentRow.Cells["营业定价"].Value.ToString());
                    }
                    else
                    {
                        nowsalesprice = 0;
                    }

                    dgv.CurrentRow.Cells["单价"].Value = nowsalesprice * (100 - decimal.Parse(queryForm.NewValue)) / 100;
                    //dgv.CurrentRow.Cells["discount_money"].Value = DBNull.Value;

                    //dgv.CurrentRow.Cells["approve_flag"].Value = "False";
                    SaveDetailItem();


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


            ///////////////////////////////////////////////////////

            if ("交货时间" == dgv.CurrentCell.OwningColumn.Name)
            {

                ChangeValue queryForm = new ChangeValue();

                queryForm.OldValue = dgv.CurrentCell.Value.ToString();
                queryForm.NewValue = "";
                queryForm.ChangeMode = "datetime";
                queryForm.ShowDialog();




                if (queryForm.NewValue != "")
                {
                    dgv.CurrentRow.Cells["交货时间"].Value = queryForm.NewValue;
                    //dgv.CurrentRow.Cells["discount_money"].Value = DBNull.Value;

                    //dgv.CurrentRow.Cells["approve_flag"].Value = "False";
                    SaveDetailItem();


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


            /////////////////////////////////////////////////////
            /////////////////////////////////////////////////////////////////

            //if ("审批" == dgv.CurrentCell.OwningColumn.Name)
            //{

            //    decimal nowrealdis;

            //    if ("" != dgv.CurrentRow.Cells["单件折扣"].Value.ToString())
            //    {
            //        nowrealdis = decimal.Parse(dgv.CurrentRow.Cells["单件折扣"].Value.ToString());
            //    }
            //    else
            //    {
            //        nowrealdis = 0;
            //    }

            //    decimal nowleaderdiscount = SQLDatabase.nowUserdiscount();

            //    if (nowrealdis > nowleaderdiscount)
            //    {
            //        MessageBox.Show("折扣权限不够,审批取消...", "注意");
            //        return;

            //    }


            //    if ("True" == dgv.CurrentRow.Cells["审批"].Value.ToString())
            //    {
            //        dgv.CurrentRow.Cells["审批"].Value = "False";
            //        dgv.CurrentRow.Cells["审批人"].Value = DBNull.Value;
            //        dgv.CurrentRow.Cells["审批日期"].Value = DBNull.Value;
            //        dgv.CurrentRow.Cells["审批人折扣"].Value = DBNull.Value;
            //    }
            //    else
            //    {

            //        dgv.CurrentRow.Cells["审批"].Value = "True";
            //        dgv.CurrentRow.Cells["审批人"].Value = SQLDatabase.nowUserName();
            //        dgv.CurrentRow.Cells["审批日期"].Value = SQLDatabase.GetNowdate();
            //        dgv.CurrentRow.Cells["审批人折扣"].Value = SQLDatabase.nowUserdiscount();
            //    }



            //    SaveDetailItem();



            //    return;

            //}
            /////////////////////////////////////////////////////////////////

            if ("备注2" == dgv.CurrentCell.OwningColumn.Name)
            {

                ChangeValue queryForm = new ChangeValue();

                queryForm.OldValue = dgv.CurrentCell.Value.ToString();
                queryForm.NewValue = "";
                queryForm.ChangeMode = "longstring";
                queryForm.ShowDialog();




                if (queryForm.NewValue != "")
                {
                    dgv.CurrentRow.Cells["备注2"].Value = queryForm.NewValue;
                    //dgv.CurrentRow.Cells["discount_money"].Value = DBNull.Value;

                    //dgv.CurrentRow.Cells["approve_flag"].Value = "False";
                    SaveDetailItem();


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


            /////////////////////////////////////////////////////
        }

        private void bindingNavigatorDeleteItem2_Click(object sender, EventArgs e)
        {
            DeleteDetail();
        }
        private void DeleteDetail()
        {
            if (null == ly_sales_contract_detailDataGridView.CurrentRow)
            {


                return;
            }

            if ("False" == this.contractCanchenged)
            {
                MessageBox.Show("合同已经提交,不能删除数据...", "注意");
                return;

            }


            string message = "确定删除当前条目吗？";
            string caption = "提示...";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result;



            result = MessageBox.Show(message, caption, buttons,
            MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

            if (result == DialogResult.Yes)
            {

                this.ly_sales_contract_detailBindingSource.RemoveCurrent();


                ly_sales_contract_detailDataGridView.EndEdit();
                ly_sales_contract_detailBindingSource.EndEdit();



                this.ly_sales_contract_detailTableAdapter.Update(this.lYSalseMange.ly_sales_contract_detail);


            }
        }

        //private void fillToolStripButton_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        this.ly_sales_borrowTableAdapter.Fill(this.lYSalseMange.ly_sales_borrow, contract_codeToolStripTextBox.Text);
        //    }
        //    catch (System.Exception ex)
        //    {
        //        System.Windows.Forms.MessageBox.Show(ex.Message);
        //    }

        //}

        private void bindingNavigatorAddNewItem2_Click(object sender, EventArgs e)
        {
            if (null == ly_sales_businessDataGridView.CurrentRow)
            {

                //this.ly_sales_contract_main_forbusinessTableAdapter.Fill(this.lYSalseMange.ly_sales_contract_main_forbusiness, "");

                return;
            }

            string nowcontractcode = this.ly_sales_businessDataGridView.CurrentRow.Cells["业务编码"].Value.ToString();
            string nowclientcode = this.ly_sales_businessDataGridView.CurrentRow.Cells["客户编码"].Value.ToString();

            LY_Salesborror_Add queryForm = new LY_Salesborror_Add();

            queryForm.contract_code = nowcontractcode;
            queryForm.salesclientcode = nowclientcode;
            queryForm.borrowstyle = "01";
            queryForm.runmode = "增加";


            queryForm.StartPosition = FormStartPosition.CenterParent;
            queryForm.ShowDialog();

            if (queryForm.DialogResult != DialogResult.Cancel)
            {
                this.ly_sales_borrowTableAdapter.Fill(this.lYSalseMange.ly_sales_borrow, this.nowcontractCode);



                this.ly_sales_borrowBindingSource.Position = this.ly_sales_borrowBindingSource.Find("借用单号", queryForm.borrowcode);

                //this.ly_sales_contract_main1DataGridView.SelectionChanged -= ly_sales_contract_main1DataGridView_SelectionChanged;
                //this.ly_sales_contract_main1TableAdapter.Fill(this.lYSalseMange.ly_sales_contract_main1, this.nowusercode, "single", this.dateTimePicker1.Value, this.dateTimePicker2.Value);
                //this.ly_sales_contract_main1DataGridView.SelectionChanged += ly_sales_contract_main1DataGridView_SelectionChanged;
            }
        }

        private void ly_sales_borrowDataGridView_SelectionChanged(object sender, EventArgs e)
        {
            if (null == ly_sales_borrowDataGridView.CurrentRow)
            {
                //this.ly_sales_contract_terms_forcontractTableAdapter.Fill(this.lYSalseMange.ly_sales_contract_terms_forcontract, "-aqwaaa");
                //this.ly_sales_contract_detailTableAdapter.Fill(this.lYSalseMange.ly_sales_contract_detail, "", 0);



                return;
            }

            this.nowborrowcode = ly_sales_borrowDataGridView.CurrentRow.Cells["借用单号"].Value.ToString();
            //this.nowclientCode = ly_sales_borrowDataGridView.CurrentRow.Cells["客户编码c"].Value.ToString();


            this.ly_sales_borrow_detailTableAdapter.Fill(this.lYSalseMange.ly_sales_borrow_detail, nowborrowcode);



            if ("True" == ly_sales_borrowDataGridView.CurrentRow.Cells["提交j"].Value.ToString() ||
                "True" == ly_sales_borrowDataGridView.CurrentRow.Cells["批准j"].Value.ToString())
            {
                this.borrowCanchenged = "False";
            }
            else
            {
                this.borrowCanchenged = "True";
            }

        }

        private void toolStripButton42_Click(object sender, EventArgs e)
        {
            if (null == ly_sales_borrowDataGridView.CurrentRow)
            {

                //this.ly_sales_contract_main_forbusinessTableAdapter.Fill(this.lYSalseMange.ly_sales_contract_main_forbusiness, "");

                return;
            }

            if ("True" == ly_sales_borrowDataGridView.CurrentRow.Cells["提交j"].Value.ToString())
            {
                MessageBox.Show("借用已经提交,不能修改数据...", "注意");
                return;

            }

            if ("True" == ly_sales_borrowDataGridView.CurrentRow.Cells["批准j"].Value.ToString())
            {
                MessageBox.Show("借用已经批准,不能修改数据...", "注意");
                return;

            }

            string nowcontractcode = this.ly_sales_businessDataGridView.CurrentRow.Cells["业务编码"].Value.ToString();
            string nowclientcode = this.ly_sales_businessDataGridView.CurrentRow.Cells["客户编码"].Value.ToString();

            LY_Salesborror_Add queryForm = new LY_Salesborror_Add();

            queryForm.contract_code = nowcontractcode;
            queryForm.salesclientcode = nowclientcode;
            queryForm.borrowcode = this.nowborrowcode;
            queryForm.borrowstyle = "01";
            queryForm.runmode = "修改";


            queryForm.StartPosition = FormStartPosition.CenterParent;
            queryForm.ShowDialog();

            if (queryForm.DialogResult != DialogResult.Cancel)
            {
                this.ly_sales_borrowTableAdapter.Fill(this.lYSalseMange.ly_sales_borrow, this.nowcontractCode);



                this.ly_sales_borrowBindingSource.Position = this.ly_sales_borrowBindingSource.Find("借用单号", queryForm.borrowcode);

                //this.ly_sales_contract_main1DataGridView.SelectionChanged -= ly_sales_contract_main1DataGridView_SelectionChanged;
                //this.ly_sales_contract_main1TableAdapter.Fill(this.lYSalseMange.ly_sales_contract_main1, this.nowusercode, "single", this.dateTimePicker1.Value, this.dateTimePicker2.Value);
                //this.ly_sales_contract_main1DataGridView.SelectionChanged += ly_sales_contract_main1DataGridView_SelectionChanged;
            }
        }

        private void toolStripButton43_Click(object sender, EventArgs e)
        {

            if (null == ly_sales_borrowDataGridView.CurrentRow)
            {

                //this.ly_sales_contract_main_forbusinessTableAdapter.Fill(this.lYSalseMange.ly_sales_contract_main_forbusiness, "");

                return;
            }

            if ("True" == ly_sales_borrowDataGridView.CurrentRow.Cells["提交j"].Value.ToString())
            {
                MessageBox.Show("借用已经提交,不能删除数据...", "注意");
                return;

            }

            if ("True" == ly_sales_borrowDataGridView.CurrentRow.Cells["批准j"].Value.ToString())
            {
                MessageBox.Show("借用已经批准,不能删除数据...", "注意");
                return;

            }

            string message = "确定删除当前借用记录吗？";
            string caption = "提示...";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result;



            result = MessageBox.Show(message, caption, buttons,
            MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

            if (result == DialogResult.Yes)
            {

                this.ly_sales_borrowBindingSource.RemoveCurrent();


                ly_sales_borrowDataGridView.EndEdit();
                ly_sales_borrowBindingSource.EndEdit();



                this.ly_sales_borrowTableAdapter.Update(this.lYSalseMange.ly_sales_borrow);

                //this.ly_sales_contract_main1DataGridView.SelectionChanged -= ly_sales_contract_main1DataGridView_SelectionChanged;
                //this.ly_sales_contract_main1TableAdapter.Fill(this.lYSalseMange.ly_sales_contract_main1, this.nowusercode, "single", this.dateTimePicker1.Value, this.dateTimePicker2.Value);
                //this.ly_sales_contract_main1DataGridView.SelectionChanged += ly_sales_contract_main1DataGridView_SelectionChanged;
            }
        }

        private void toolStripButton38_Click(object sender, EventArgs e)
        {
            this.ly_lsptb_selTableAdapter.Fill(this.lYSalseMange.ly_lsptb_sel);
        }

        private void ly_lsptb_selDataGridViewJ_DoubleClick(object sender, EventArgs e)
        {
            if (null == this.ly_sales_borrowDataGridView.CurrentRow) return;
            if (null == ly_lsptb_selDataGridView.CurrentRow) return;

            if ("False" == this.borrowCanchenged)
            {
                MessageBox.Show("借用单已经提交执行,不能修改数据...", "注意");
                return;

            }

            decimal nowsalesprice;
            if ("" != this.ly_lsptb_selDataGridView.CurrentRow.Cells["营业定价3"].Value.ToString())
            {
                nowsalesprice = decimal.Parse(this.ly_lsptb_selDataGridView.CurrentRow.Cells["营业定价3"].Value.ToString());
            }
            else
            {
                nowsalesprice = 0;
            }

            if (nowsalesprice == 0)
            {

                MessageBox.Show("产品无定价,不能借出...", "注意");
                return;
            }



            string nowitemno = this.ly_lsptb_selDataGridView.CurrentRow.Cells["物料编号2"].Value.ToString();

            this.ly_sales_borrow_detailBindingSource.AddNew();

            this.ly_sales_borrow_detailDataGridView.CurrentRow.Cells["借用单号jy"].Value = this.nowborrowcode;
            this.ly_sales_borrow_detailDataGridView.CurrentRow.Cells["产品编码j"].Value = nowitemno;
            this.ly_sales_borrow_detailDataGridView.CurrentRow.Cells["借用数量j"].Value = 1;


            this.ly_sales_borrow_detailBindingSource.EndEdit();
            this.ly_sales_borrow_detailTableAdapter.Update(this.lYSalseMange.ly_sales_borrow_detail);

            //string isborrow = this.ly_sales_contract_mainDataGridView.CurrentRow.Cells["借用"].Value.ToString();

            //InserSalseContractDetail(this.nowinnerCode, nowitemno, 1, isborrow);



            this.ly_sales_borrow_detailTableAdapter.Fill(this.lYSalseMange.ly_sales_borrow_detail, this.nowborrowcode);

            this.ly_sales_borrow_detailBindingSource.Position = this.ly_sales_borrow_detailBindingSource.Find("产品编码", nowitemno);

            //this.ly_sales_contract_detailDataGridView.CurrentCell = this.ly_sales_contract_detailDataGridView.CurrentRow.Cells["单价"]; 
        }

        private void ly_sales_contract_terms_forcontractDataGridView_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            DataGridView dgv = sender as DataGridView;

            if (null == dgv.CurrentRow) return;

            if ("False" == this.contractCanchenged)
            {
                MessageBox.Show("合同已经执行,不能修改数据...", "注意");
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
                    this.ly_sales_contract_terms_forcontractBindingSource.EndEdit();
                    this.ly_sales_contract_terms_forcontractTableAdapter.Update(this.lYSalseMange.ly_sales_contract_terms_forcontract);

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
                    this.ly_sales_contract_terms_forcontractBindingSource.EndEdit();
                    this.ly_sales_contract_terms_forcontractTableAdapter.Update(this.lYSalseMange.ly_sales_contract_terms_forcontract);

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
                    this.ly_sales_contract_terms_forcontractBindingSource.EndEdit();
                    this.ly_sales_contract_terms_forcontractTableAdapter.Update(this.lYSalseMange.ly_sales_contract_terms_forcontract);

                    //CountPlanStru();

                }
                else
                {

                }
                return;

            }


            ///////////////////////////////////////////////////////
        }

        private void bindingNavigatorDeleteItem4_Click(object sender, EventArgs e)
        {
            if (null == this.ly_sales_borrow_detailDataGridView.CurrentRow)
            {


                return;
            }

            if ("False" == this.borrowCanchenged)
            {
                MessageBox.Show("借用单已经提交,不能删除数据...", "注意");
                return;

            }


            string message = "确定删除当前条目吗？";
            string caption = "提示...";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result;



            result = MessageBox.Show(message, caption, buttons,
            MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

            if (result == DialogResult.Yes)
            {

                this.ly_sales_borrow_detailBindingSource.RemoveCurrent();


                ly_sales_borrow_detailDataGridView.EndEdit();
                ly_sales_borrow_detailBindingSource.EndEdit();

                this.ly_sales_borrow_detailTableAdapter.Update(this.lYSalseMange.ly_sales_borrow_detail);


            }
        }

        private void ly_sales_borrow_detailDataGridView_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            DataGridView dgv = sender as DataGridView;

            if (null == dgv.CurrentRow) return;

            if ("False" == this.borrowCanchenged)
            {
                MessageBox.Show("借用单已经提交执行,不能修改数据...", "注意");
                return;

            }






            ///////////////////////////////////////////////////////////////
            if ("借用数量j" == dgv.CurrentCell.OwningColumn.Name)
            {

                //if ("True" == dgv.CurrentRow.Cells["审批"].Value.ToString())
                //{

                //    MessageBox.Show("合同已经审批,不能修改数据...", "注意");
                //    return;
                //}

                //if ("False" == this.isborrow)
                //{

                //    MessageBox.Show("普通合同已经审批,不能输入借用数量...", "注意");
                //    return;
                //}

                ChangeValue queryForm = new ChangeValue();

                queryForm.OldValue = dgv.CurrentCell.Value.ToString();
                queryForm.NewValue = "";
                queryForm.ChangeMode = "value";
                queryForm.ShowDialog();




                if (queryForm.NewValue != "")
                {
                    dgv.CurrentRow.Cells["借用数量j"].Value = queryForm.NewValue;
                    //dgv.CurrentRow.Cells["discount_money"].Value = DBNull.Value;

                    //dgv.CurrentRow.Cells["approve_flag"].Value = "False";
                    ly_sales_borrow_detailDataGridView.EndEdit();
                    ly_sales_borrow_detailBindingSource.EndEdit();

                    this.ly_sales_borrow_detailTableAdapter.Update(this.lYSalseMange.ly_sales_borrow_detail);


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


            if ("备注j" == dgv.CurrentCell.OwningColumn.Name)
            {

                ChangeValue queryForm = new ChangeValue();

                queryForm.OldValue = dgv.CurrentCell.Value.ToString();
                queryForm.NewValue = "";
                queryForm.ChangeMode = "longstring";
                queryForm.ShowDialog();




                if (queryForm.NewValue != "")
                {
                    dgv.CurrentRow.Cells["备注j"].Value = queryForm.NewValue;
                    //dgv.CurrentRow.Cells["discount_money"].Value = DBNull.Value;

                    //dgv.CurrentRow.Cells["approve_flag"].Value = "False";
                    ly_sales_borrow_detailDataGridView.EndEdit();
                    ly_sales_borrow_detailBindingSource.EndEdit();

                    this.ly_sales_borrow_detailTableAdapter.Update(this.lYSalseMange.ly_sales_borrow_detail);

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


            /////////////////////////////////////////////////////
        }

        private void ly_sales_borrowDataGridView_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            DataGridView dgv = sender as DataGridView;

            if (null == dgv.CurrentRow) return;



            //////////////////////////////////////////////////////////////////////////





            if ("True" == dgv.CurrentRow.Cells["营业发货jy"].Value.ToString())
            {
                MessageBox.Show("借用已经发货,不能修改...", "注意");
                return;

            }

            if ("False" == dgv.CurrentRow.Cells["批准j"].Value.ToString())
            {
                MessageBox.Show("借用单未经 部门批准,不能执行...", "注意");
                return;

            }

            /////////////////////////////////////////////////////////


            /////////////////////////////////////////////////////////////
            ///////////////////////////////////////////////////////////////

            if ("借用目的" == dgv.CurrentCell.OwningColumn.Name)
            {


                ChangeValue queryForm = new ChangeValue();

                queryForm.OldValue = dgv.CurrentCell.Value.ToString();
                queryForm.NewValue = "";
                queryForm.ChangeMode = "longstring";
                queryForm.ShowDialog();




                if (queryForm.NewValue != "")
                {
                    dgv.CurrentRow.Cells["借用目的"].Value = queryForm.NewValue;
                    //dgv.CurrentRow.Cells["discount_money"].Value = DBNull.Value;

                    //dgv.CurrentRow.Cells["approve_flag"].Value = "False";
                    ly_sales_borrowDataGridView.EndEdit();
                    ly_sales_borrowBindingSource.EndEdit();



                    this.ly_sales_borrowTableAdapter.Update(this.lYSalseMange.ly_sales_borrow);

                    //CountPlanStru();

                }
                else
                {

                }
                return;

            }
            ///////////////////////////////////////////////////////////////////

            //if ("批准j" == dgv.CurrentCell.OwningColumn.Name)
            //{



            //    if ("True" == dgv.CurrentRow.Cells["批准j"].Value.ToString())
            //    {
            //        dgv.CurrentRow.Cells["批准j"].Value = "False";

            //    }
            //    else
            //    {

            //        dgv.CurrentRow.Cells["批准j"].Value = "True";
            //    }



            //    ly_sales_borrowDataGridView.EndEdit();
            //    ly_sales_borrowBindingSource.EndEdit();



            //    this.ly_sales_borrowTableAdapter.Update(this.lYSalseMange.ly_sales_borrow);



            //    return;

            //}
            /////////////////////////////////////////////////////////////////
            //if ("生产审批j" == dgv.CurrentCell.OwningColumn.Name)
            //{



            //    if ("True" == dgv.CurrentRow.Cells["生产审批j"].Value.ToString())
            //    {
            //        dgv.CurrentRow.Cells["生产审批j"].Value = "False";

            //    }
            //    else
            //    {

            //        dgv.CurrentRow.Cells["生产审批j"].Value = "True";
            //    }



            //    ly_sales_borrowDataGridView.EndEdit();
            //    ly_sales_borrowBindingSource.EndEdit();



            //    this.ly_sales_borrowTableAdapter.Update(this.lYSalseMange.ly_sales_borrow);



            //    return;

            //}

            ///////////////////////////////////////////////////////////////
            if ("执行j" == dgv.CurrentCell.OwningColumn.Name)
            {



                if ("True" == dgv.CurrentRow.Cells["执行j"].Value.ToString())
                {
                    dgv.CurrentRow.Cells["执行j"].Value = "False";
                    dgv.CurrentRow.Cells["执行人"].Value = DBNull.Value;

                }
                else
                {

                    dgv.CurrentRow.Cells["执行j"].Value = "True";
                    dgv.CurrentRow.Cells["执行人"].Value = SQLDatabase.nowUserName();
                }



                ly_sales_borrowDataGridView.EndEdit();
                ly_sales_borrowBindingSource.EndEdit();



                this.ly_sales_borrowTableAdapter.Update(this.lYSalseMange.ly_sales_borrow);



                return;

            }
            /////////////////////////////////////////////////////////////



        }

        private void 生成合同ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (null == this.ly_material_plan_mainDataGridView.CurrentRow)
            {
                return;
            }

            string message = "确定当前依赖书生成合同吗？";
            string caption = "提示...";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result;



            result = MessageBox.Show(message, caption, buttons,
            MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

            if (result == DialogResult.Yes)
            {
                //NewFrm.Show(this);

                string nowplannum;

                nowplannum = this.ly_material_plan_mainDataGridView.CurrentRow.Cells["计划编号0"].Value.ToString();

                MakeSalesContract(nowplannum);

                this.ly_sales_contract_main_forbusinessTableAdapter.Fill(this.lYSalseMange.ly_sales_contract_main_forbusiness, this.nowcontractCode);
                this.tabControl1.SelectedIndex = 2;

                //NewFrm.Hide(this);

            }


        }

        private void MakeSalesContract(string plannum)
        {




            SqlConnection sqlConnection1 = new SqlConnection(SQLDatabase.Connectstring);
            SqlCommand cmd = new SqlCommand();



            cmd.Parameters.Add("@plan_num", SqlDbType.VarChar);
            cmd.Parameters["@plan_num"].Value = plannum;

            //cmd.Parameters.Add("@prod_dept", SqlDbType.VarChar);
            //cmd.Parameters["@prod_dept"].Value = comboBox1.SelectedValue;

            //cmd.Parameters.Add("@warehousename", SqlDbType.VarChar);
            //cmd.Parameters["@warehousename"].Value = comboBox2.SelectedValue;

            //string outNum = GetMaxOutNum();
            //cmd.Parameters.Add("@out_number", SqlDbType.VarChar);
            //cmd.Parameters["@out_number"].Value = outNum;

            cmd.Parameters.Add("@inputman", SqlDbType.VarChar);
            cmd.Parameters["@inputman"].Value = SQLDatabase.nowUserName();

            //cmd.Parameters.Add("@innercode", SqlDbType.VarChar);
            //cmd.Parameters["@innercode"].Value = innerCode;









            cmd.CommandText = "LY_plan_to_contract";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = sqlConnection1;



            sqlConnection1.Open();

            try
            {
                NewFrm.Show(this);
                cmd.ExecuteNonQuery();
                NewFrm.Hide(this);
                MessageBox.Show("合同生成功,请补充合同相关数据", "注意");
            }
            catch (SqlException sqle)
            {
                MessageBox.Show("合同生成失败,请检验相关数据", "注意");
                NewFrm.Hide(this);
            }
            finally
            {
                sqlConnection1.Close();

            }







        }

        private void 生成依赖书ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (null == this.ly_material_plan_mainDataGridView.CurrentRow)
            {
                return;
            }

            string message = "确定当前依赖书生成借用吗？";
            string caption = "提示...";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result;



            result = MessageBox.Show(message, caption, buttons,
            MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

            if (result == DialogResult.Yes)
            {
                //NewFrm.Show(this);

                string nowplannum;

                nowplannum = this.ly_material_plan_mainDataGridView.CurrentRow.Cells["计划编号0"].Value.ToString();

                MakeSalesBorrow(nowplannum);

                this.ly_sales_borrowTableAdapter.Fill(this.lYSalseMange.ly_sales_borrow, this.nowcontractCode);
                this.tabControl1.SelectedIndex = 1;

                //NewFrm.Hide(this);

            }
        }

        private void MakeSalesBorrow(string plannum)
        {




            SqlConnection sqlConnection1 = new SqlConnection(SQLDatabase.Connectstring);
            SqlCommand cmd = new SqlCommand();



            cmd.Parameters.Add("@plan_num", SqlDbType.VarChar);
            cmd.Parameters["@plan_num"].Value = plannum;

            //cmd.Parameters.Add("@prod_dept", SqlDbType.VarChar);
            //cmd.Parameters["@prod_dept"].Value = comboBox1.SelectedValue;

            //cmd.Parameters.Add("@warehousename", SqlDbType.VarChar);
            //cmd.Parameters["@warehousename"].Value = comboBox2.SelectedValue;

            //string outNum = GetMaxOutNum();
            //cmd.Parameters.Add("@out_number", SqlDbType.VarChar);
            //cmd.Parameters["@out_number"].Value = outNum;

            cmd.Parameters.Add("@inputman", SqlDbType.VarChar);
            cmd.Parameters["@inputman"].Value = SQLDatabase.nowUserName();

            //cmd.Parameters.Add("@innercode", SqlDbType.VarChar);
            //cmd.Parameters["@innercode"].Value = innerCode;









            cmd.CommandText = "LY_plan_to_borrow";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = sqlConnection1;



            sqlConnection1.Open();

            try
            {
                NewFrm.Show(this);
                cmd.ExecuteNonQuery();
                NewFrm.Hide(this);
                MessageBox.Show("借用生成功,请补充合同相关数据", "注意");
            }
            catch (SqlException sqle)
            {
                MessageBox.Show("借用生成失败,请检验相关数据", "注意");
                NewFrm.Hide(this);
            }
            finally
            {
                sqlConnection1.Close();

            }







        }

        private void toolStripButton52_Click(object sender, EventArgs e)
        {
            if (null == this.ly_sales_contract_detailDataGridView.CurrentRow) return;

            if ("True" != this.ly_sales_contract_mainDataGridView.CurrentRow.Cells["批准"].Value.ToString())
            {

                MessageBox.Show("请先确定 执行,然后打印...", "注意");
                return;
            }

            //NewFrm.Show(this); ;

            //this.ly_plan_getmaterialTableAdapter.Fill(this.lYPlanMange.ly_plan_getmaterial, parentId);

            BaseReportView queryForm = new BaseReportView();

            queryForm.Text = "中原精密营业合同";

            queryForm.setchackBoxCansee(true);

            queryForm.Printdata = this.lYSalseMange;
            queryForm.company = ly_sales_contract_mainDataGridView.CurrentRow.Cells["公司"].Value.ToString();

            if ("中原" == ly_sales_contract_mainDataGridView.CurrentRow.Cells["公司"].Value.ToString())
            {
                queryForm.PrintCrystalReport = new LY_YingyeHetong_FH();
            }
            else
            {

                queryForm.PrintCrystalReport = new LY_YingyeHetong_FHzhongc();
            }





            //if ("True" != this.ly_sales_contract_mainDataGridView.CurrentRow.Cells["借用"].Value.ToString())
            //{
            //    queryForm.PrintCrystalReport = new LY_YingyeHetong_FH();
            //}
            //else
            //{

            //    queryForm.PrintCrystalReport = new LY_YingyeHetong_FHJY();
            //}



            //if ("工矿" == ly_sales_contract_mainDataGridView.CurrentRow.Cells["类别"].Value.ToString())
            //{
            //    queryForm.PrintCrystalReport = new LY_YingyeHetong();
            //}
            //else
            //{

            //    queryForm.PrintCrystalReport = new LY_YingyeHetong_XH();
            //}


            //string selectFormula;

            //selectFormula = "{ly_store_planitemcount.状态}  =   '原料'  and {ly_store_planitemcount.欠料金额}>0 ";
            //queryForm.PrintCrystalReport.DataDefinition.RecordSelectionFormula = selectFormula;


            //NewFrm.Hide(this);

            queryForm.ShowDialog();
        }

        private void toolStripButton53_Click(object sender, EventArgs e)
        {
            //LY_Production_order_Mange queryForm = new LY_Production_order_Mange();

            //queryForm.OwnerForm = this;


            //queryForm.StartPosition = FormStartPosition.CenterParent;
            //queryForm.ShowDialog(this);




            //if (queryForm.DialogResult != DialogResult.Cancel)
            //{
            //    //this.ly_inma0010ylTableAdapter.Fill(this.lYMaterialMange.ly_inma0010yl);
            //    //this.ly_inma0010ylBindingSource.Position = this.ly_inma0010ylBindingSource.Find("物资编号", queryForm.material_code);
            //}

            ///////////////////////////////////////////////////////////////////////////////

            LY_Salescontract_Query queryForm = new LY_Salescontract_Query();

            queryForm.OwnerForm = this;


            queryForm.StartPosition = FormStartPosition.CenterParent;
            queryForm.ShowDialog(this);




            if (queryForm.DialogResult != DialogResult.Cancel)
            {
                //this.ly_inma0010ylTableAdapter.Fill(this.lYMaterialMange.ly_inma0010yl);
                //this.ly_inma0010ylBindingSource.Position = this.ly_inma0010ylBindingSource.Find("物资编号", queryForm.material_code);
            }

        }

        public void find_NowContract(string nowcontract, string nowinnercode)
        {

            this.tabControl1.SelectedIndex = 2;
            this.ly_sales_businessBindingSource.Position = this.ly_sales_businessBindingSource.Find("业务编码", nowcontract);

            this.ly_sales_contract_main_forbusinessBindingSource.Position = this.ly_sales_contract_main_forbusinessBindingSource.Find("内部编码", nowinnercode);



        }

        public void find_NowBorrow(string nowcontract, string nowborrow)
        {

            this.tabControl1.SelectedIndex = 1;
            this.ly_sales_businessBindingSource.Position = this.ly_sales_businessBindingSource.Find("业务编码", nowcontract);

            this.ly_sales_borrowBindingSource.Position = this.ly_sales_borrowBindingSource.Find("借用单号", nowborrow);



        }


        public void find_NowPlan(string nowcontract, string nowplancode)
        {

            this.tabControl1.SelectedIndex = 0;
            this.ly_sales_businessBindingSource.Position = this.ly_sales_businessBindingSource.Find("业务编码", nowcontract);

            this.ly_material_plan_mainBindingSource.Position = this.ly_material_plan_mainBindingSource.Find("计划编号", nowplancode);



        }

        private void toolStripButton55_Click(object sender, EventArgs e)
        {
            LY_Salescontract_OweQuery queryForm = new LY_Salescontract_OweQuery();

            queryForm.OwnerForm = this;


            queryForm.StartPosition = FormStartPosition.CenterParent;
            queryForm.ShowDialog(this);




            if (queryForm.DialogResult != DialogResult.Cancel)
            {
                //this.ly_inma0010ylTableAdapter.Fill(this.lYMaterialMange.ly_inma0010yl);
                //this.ly_inma0010ylBindingSource.Position = this.ly_inma0010ylBindingSource.Find("物资编号", queryForm.material_code);
            }
        }

        private void toolStripButton56_Click(object sender, EventArgs e)
        {
            if (null == this.ly_sales_borrow_detailDataGridView.CurrentRow) return;



            NewFrm.Show(this); ;

            //this.ly_plan_getmaterialTableAdapter.Fill(this.lYPlanMange.ly_plan_getmaterial, parentId);

            BaseReportView queryForm = new BaseReportView();

            queryForm.Text = "中原精密借用单";

            queryForm.Printdata = this.lYSalseMange;




            queryForm.PrintCrystalReport = new LY_YingyeHetong_FHJY();
            //string selectFormula;

            //selectFormula = "{ly_store_planitemcount.状态}  =   '原料'  and {ly_store_planitemcount.欠料金额}>0 ";
            //queryForm.PrintCrystalReport.DataDefinition.RecordSelectionFormula = selectFormula;

            NewFrm.Hide(this);

            queryForm.ShowDialog();
        }

        private void 复制ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string sClipboadStr = this.ly_sales_contract_mainDataGridView.CurrentCell.Value.ToString();
            Clipboard.SetText(sClipboadStr);

        }

        private void ly_sales_contract_detailDataGridView_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex >= 0)
                selectionIdx = e.RowIndex;
        }

        private void ly_sales_contract_detailDataGridView_CellMouseMove(object sender, DataGridViewCellMouseEventArgs e)
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
        private void ly_sales_contract_detailDataGridView_DragDrop(object sender, DragEventArgs e)
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
                            dgvr.Cells["顺序"].Value = dgvr.Index;

                        }
                    }
                }
                if (idx < row.Index)
                {
                    foreach (DataGridViewRow dgvr in dgv.Rows)
                    {

                        if (dgvr.Index >= idx && dgvr.Index < row.Index)
                        {
                            dgvr.Cells["顺序"].Value = dgvr.Index + 2;

                        }
                    }
                }


                row.Cells["顺序"].Value = idx + 1;
                // dgv.Rows[idx].Cells["顺序"].Value = row.Index + 1;

                SaveDetailItem();



                dgv.Rows[idx].Selected = true;
                dgv.CurrentCell = dgv.Rows[idx].Cells["顺序"];


                //selectionIdx = idx;
            }
        }

        private void ly_sales_contract_detailDataGridView_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;
        }

        private void ly_sales_contract_mainDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void ly_sales_businessDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void toolStripButton57_Click(object sender, EventArgs e)
        {

            //System.Media.SystemSounds.Beep.Play();
            //System.Media.SystemSounds.Exclamation.Play();

            LY_sales_outquery queryForm = new LY_sales_outquery();

            queryForm.OwnerForm = this;


            queryForm.StartPosition = FormStartPosition.CenterParent;
            queryForm.ShowDialog(this);




            if (queryForm.DialogResult != DialogResult.Cancel)
            {
                //this.ly_inma0010ylTableAdapter.Fill(this.lYMaterialMange.ly_inma0010yl);
                //this.ly_inma0010ylBindingSource.Position = this.ly_inma0010ylBindingSource.Find("物资编号", queryForm.material_code);
            }
        }

        private void toolStripButton58_Click(object sender, EventArgs e)
        {
            if (null == this.ly_sales_contract_detailDataGridView.CurrentRow) return;

            //if ("True" != this.ly_sales_contract_mainDataGridView.CurrentRow.Cells["批准"].Value.ToString())
            //{

            //    MessageBox.Show("请先确定 执行,然后打印...", "注意");
            //    return;
            //}

            //NewFrm.Show(this); ;

            //this.ly_plan_getmaterialTableAdapter.Fill(this.lYPlanMange.ly_plan_getmaterial, parentId);

            BaseReportView queryForm = new BaseReportView();

            queryForm.Text = "中原精密营业合同";

            queryForm.setchackBoxCansee(true);

            queryForm.Printdata = this.lYSalseMange;
            queryForm.company = ly_sales_contract_mainDataGridView.CurrentRow.Cells["公司"].Value.ToString();

            if ("中原" == ly_sales_contract_mainDataGridView.CurrentRow.Cells["公司"].Value.ToString())
            {
                queryForm.PrintCrystalReport = new LY_YingyeHetong_FH();
            }
            else
            {

                queryForm.PrintCrystalReport = new LY_YingyeHetong_FHzhongc();
            }







            //NewFrm.Hide(this);

            queryForm.ShowDialog();
        }

        private void toolStripButton44_Click(object sender, EventArgs e)
        {
            if (null == this.ly_sales_contract_detailDataGridView.CurrentRow) return;

            //if ("True" != this.ly_sales_contract_mainDataGridView.CurrentRow.Cells["批准"].Value.ToString())
            //{

            //    MessageBox.Show("请先确定 执行,然后打印...", "注意");
            //    return;
            //}

            NewFrm.Show(this); ;

            //this.ly_plan_getmaterialTableAdapter.Fill(this.lYPlanMange.ly_plan_getmaterial, parentId);

            BaseReportView queryForm = new BaseReportView();

            queryForm.Text = "中原精密营业合同";

            queryForm.setchackBoxCansee(true);

            queryForm.Printdata = this.lYSalseMange;
            queryForm.company = "中成折扣";

            //if ("中原" == ly_sales_contract_mainDataGridView.CurrentRow.Cells["公司"].Value.ToString())
            //{
            //    queryForm.PrintCrystalReport = new LY_YingyeHetong_FH();
            //}
            //else
            //{

            //    queryForm.PrintCrystalReport = new LY_YingyeHetong_FHzhongc();
            //}


            queryForm.PrintCrystalReport = new LY_YingyeHetong_FH_zcdis();


            NewFrm.Hide(this);

            queryForm.ShowDialog();
        }

        private void toolStripButton45_Click(object sender, EventArgs e)
        {
            if (null == this.ly_sales_contract_detailDataGridView.CurrentRow) return;

            if ("中原" == ly_sales_contract_mainDataGridView.CurrentRow.Cells["公司"].Value.ToString() || "True" != this.ly_sales_contract_mainDataGridView.CurrentRow.Cells["免税"].Value.ToString())
            {

                MessageBox.Show("非中成免税,不能打印...", "注意");
                return;
            }

            if ("True" != this.ly_sales_contract_mainDataGridView.CurrentRow.Cells["批准"].Value.ToString())
            {

                MessageBox.Show("请先确定 执行,然后打印...", "注意");
                return;
            }

            NewFrm.Show(this); ;

            //this.ly_plan_getmaterialTableAdapter.Fill(this.lYPlanMange.ly_plan_getmaterial, parentId);

            BaseReportView queryForm = new BaseReportView();

            queryForm.Text = "中原精密营业合同";

            queryForm.setchackBoxCansee(true);

            queryForm.Printdata = this.lYSalseMange;
            queryForm.company = "中原代发";



            queryForm.PrintCrystalReport = new LY_YingyeHetong_FHzhongcnotax();








            NewFrm.Hide(this);

            queryForm.ShowDialog();
        }

        private void toolStripButton46_Click(object sender, EventArgs e)
        {

            if (null == this.ly_sales_contract_detailDataGridView.CurrentRow) return;

            if ("中原" == ly_sales_contract_mainDataGridView.CurrentRow.Cells["公司"].Value.ToString() || "True" != this.ly_sales_contract_mainDataGridView.CurrentRow.Cells["免税"].Value.ToString())
            {

                MessageBox.Show("非中成免税,不能打印...", "注意");
                return;
            }

            if ("True" != this.ly_sales_contract_mainDataGridView.CurrentRow.Cells["批准"].Value.ToString())
            {

                MessageBox.Show("请先确定 执行,然后打印...", "注意");
                return;
            }

            NewFrm.Show(this); ;

            //this.ly_plan_getmaterialTableAdapter.Fill(this.lYPlanMange.ly_plan_getmaterial, parentId);

            BaseReportView queryForm = new BaseReportView();

            queryForm.Text = "中原精密营业合同";

            queryForm.setchackBoxCansee(true);

            queryForm.Printdata = this.lYSalseMange;
            queryForm.company = "中原中成";



            //queryForm.PrintCrystalReport = new LY_YingyeHetong_FHzhongc();

            queryForm.PrintCrystalReport = new LY_YingyeHetong_FH_zcdisf();







            NewFrm.Hide(this);

            queryForm.ShowDialog();

        }

        private void toolStripButton47_Click(object sender, EventArgs e)
        {
            if (null == this.ly_sales_contract_detailDataGridView.CurrentRow) return;

            MessageBox.Show("暂缓执行...", "注意");
            return;

            if ("中原" == ly_sales_contract_mainDataGridView.CurrentRow.Cells["公司"].Value.ToString() || "True" != this.ly_sales_contract_mainDataGridView.CurrentRow.Cells["免税"].Value.ToString())
            {

                MessageBox.Show("非中成免税,不能打印...", "注意");
                return;
            }

            if ("True" != this.ly_sales_contract_mainDataGridView.CurrentRow.Cells["批准"].Value.ToString())
            {

                MessageBox.Show("请先确定 执行,然后打印...", "注意");
                return;
            }

            NewFrm.Show(this); ;

            //this.ly_plan_getmaterialTableAdapter.Fill(this.lYPlanMange.ly_plan_getmaterial, parentId);

            BaseReportView queryForm = new BaseReportView();

            queryForm.Text = "中原精密营业合同";

            queryForm.setchackBoxCansee(true);

            queryForm.Printdata = this.lYSalseMange;
            queryForm.company = "中原代发";



            queryForm.PrintCrystalReport = new LY_YingyeHetong_FHzhongcnotax();








            NewFrm.Hide(this);

            queryForm.ShowDialog();
        }

        private void toolStripButton83_Click(object sender, EventArgs e)
        {
            if (null == this.ly_material_plan_mainDataGridView.CurrentRow) return;

            //if ("True" != this.ly_sales_contract_mainDataGridView.CurrentRow.Cells["批准"].Value.ToString())
            //{

            //    MessageBox.Show("请先确定 执行,然后打印...", "注意");
            //    return;
            //}

            string nowbuseness = this.ly_material_plan_mainDataGridView.CurrentRow.Cells["内部编码0"].Value.ToString();
            string nowcontract = this.ly_material_plan_mainDataGridView.CurrentRow.Cells["合同编号0"].Value.ToString();


            this.ly_sales_contract_detailTableAdapter.Fill(this.lYSalseMange.ly_sales_contract_detail, nowcontract, 0);
            this.ly_sales_contract_main_forbusinessTableAdapter.Fill(this.lYSalseMange.ly_sales_contract_main_forbusiness, nowbuseness);

            NewFrm.Show(this);

            //this.ly_plan_getmaterialTableAdapter.Fill(this.lYPlanMange.ly_plan_getmaterial, parentId);

            //this.ly_sales_contract_detailTableAdapter.Fill(this.lYSalseMange.ly_sales_contract_detail, nowinnerCode, 0);

            BaseReportView queryForm = new BaseReportView();

            queryForm.Text = "中原精密送货单";

            queryForm.setchackBoxCansee(false);

            queryForm.Printdata = this.lYSalseMange;
            //queryForm.company = "中原";

            //if ("中原" == ly_sales_contract_mainDataGridView.CurrentRow.Cells["公司"].Value.ToString())
            //{
            queryForm.PrintCrystalReport = new LY_YingyeHetong_SHD();
            //}
            //else
            //{

            //    queryForm.PrintCrystalReport = new LY_YingyeHetong_FHzhongc();
            //}








            NewFrm.Hide(this);

            queryForm.ShowDialog();
        }

        private void toolStripButton84_Click(object sender, EventArgs e)
        {
            if (null == this.ly_sales_contract_detailDataGridView.CurrentRow) return;

            //if ("True" != this.ly_sales_contract_mainDataGridView.CurrentRow.Cells["批准"].Value.ToString())
            //{

            //    MessageBox.Show("请先确定 执行,然后打印...", "注意");
            //    return;
            //}

            NewFrm.Show(this); ;

            //this.ly_plan_getmaterialTableAdapter.Fill(this.lYPlanMange.ly_plan_getmaterial, parentId);

            BaseReportView queryForm = new BaseReportView();

            queryForm.Text = "中原精密营业合同";

            queryForm.setchackBoxCansee(true);

            queryForm.Printdata = this.lYSalseMange;
            queryForm.company = "中成出口";

            //if ("中原" == ly_sales_contract_mainDataGridView.CurrentRow.Cells["公司"].Value.ToString())
            //{
            //    queryForm.PrintCrystalReport = new LY_YingyeHetong_FH();
            //}
            //else
            //{

            //    queryForm.PrintCrystalReport = new LY_YingyeHetong_FHzhongc();
            //}


            queryForm.PrintCrystalReport = new LY_YingyeHetong_FHout();


            NewFrm.Hide(this);

            queryForm.ShowDialog();
        }

        private void toolStripButton85_Click(object sender, EventArgs e)
        {
            if (null == this.ly_sales_contract_detailDataGridView.CurrentRow) return;

            if ("True" != this.ly_sales_contract_mainDataGridView.CurrentRow.Cells["批准"].Value.ToString())
            {

                MessageBox.Show("请先确定 执行,然后打印...", "注意");
                return;
            }

            //NewFrm.Show(this); ;

            //this.ly_plan_getmaterialTableAdapter.Fill(this.lYPlanMange.ly_plan_getmaterial, parentId);

            BaseReportView queryForm = new BaseReportView();

            queryForm.Text = "中原精密营业合同";



            queryForm.Printdata = this.lYSalseMange;
            queryForm.company = ly_sales_contract_mainDataGridView.CurrentRow.Cells["公司"].Value.ToString();


            queryForm.PrintCrystalReport = new LY_YingyeHetong_D();

            queryForm.ShowDialog();
        }

        private void toolStripButton86_Click(object sender, EventArgs e)
        {


            if (null == this.ly_sales_contract_detailDataGridView.CurrentRow) return;


            NewFrm.Show(this);

            BaseReportView queryForm = new BaseReportView();

            queryForm.Text = "中原精密营业合同";


            queryForm.Printdata = this.lYSalseMange;


            queryForm.PrintCrystalReport = new LY_YingyeHetong_D_Hot();


            NewFrm.Hide(this);

            queryForm.ShowDialog();

        }

        private void toolStripButton87_Click(object sender, EventArgs e)
        {
            if (null == this.ly_sales_contract_detailDataGridView.CurrentRow) return;

            if ("" == ly_sales_contract_mainDataGridView.CurrentRow.Cells["类别"].Value.ToString())
            {

                MessageBox.Show("请选择合同类别,然后打印...", "注意");
                return;
            }


            BaseReportView queryForm = new BaseReportView();

            queryForm.Text = "中原精密营业合同";

            queryForm.Printdata = this.lYSalseMange;

            queryForm.PrintCrystalReport = new LY_YingyeHetong_XH_doall();


            queryForm.ShowDialog();
        }
    }
}
