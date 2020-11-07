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
    public partial class LY_SalseContract_StandardReport_financial : Form
    {
        private string nowfilterStr = "";
        private string nowusercode = "";
        private string nowfillstragecode = "";

        private DataSet ds3;
        BindingSource bindingSource3;

        public LY_SalseContract_StandardReport_financial()
        {
            InitializeComponent();
            this.ly_sales_standard_Report_financialTableAdapter.CommandTimeout = 0;
            this.ly_sales_standard_SumMarpossNewTableAdapter.CommandTimeout = 0;
            this.ly_sales_standard_SumMarpossNew2TableAdapter.CommandTimeout = 0;
            //InitializeApp();

            
        }

        public void InitializeApp()
        {
           

            //Connect Grid with DataSource
            //this.dataGridViewSummary1.AutoGenerateColumns = true;
            //this.dataGridViewSummary1.DataSource = this .ly_sales_contract_standard_ReportBindingSource;
            this.dataGridViewSummary1.SummaryColumns = new string[] { "金额" };
            this.dataGridViewSummary1.SumRowHeaderText = "合计";
            this.dataGridViewSummary1.DisplaySumRowHeader = true;
            this.dataGridViewSummary1.RefreshSummary();

            //Show the SummaryBox
            //this.dataGridViewSummary1.SummaryRowVisible = true;  
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

                //this.yhbmTextBox.ReadOnly = true ;
                //this.yhmcTextBox.ReadOnly = true ;
                //this.pwdTextBox.ReadOnly = true ;
                //this.group_IdComboBox.Enabled = false;
                //this.month_salaryTextBox.ReadOnly = true;

                //this.bindingNavigatorAddNewItem.Enabled = true;
                //this.bindingNavigatorDeleteItem.Enabled = true;
                //this.toolStripButton1.Enabled = true;
                //this.t_usersBindingNavigatorSaveItem.Enabled = false;

                //this.bindingNavigatorMoveFirstItem.Enabled = true;
                //this.bindingNavigatorMoveLastItem.Enabled = true;
                //this.bindingNavigatorMovePreviousItem.Enabled = true;
                //this.bindingNavigatorMoveNextItem.Enabled = true;
                //this.bindingNavigatorPositionItem.Enabled = true;

                //this.genderComboBox.Enabled = false;
                //this.identityCardTextBox.ReadOnly = true;
                //this.phoneTextBox.ReadOnly = true;
                //this.addressTextBox.ReadOnly = true;
                //this.inwork_dayDateTimePicker.Enabled = false;
                //this.in_active_serviceCheckBox.Enabled = false;
                ////this.duty_classComboBox.Enabled = false;
                //this.onlineTextBox.ReadOnly = true;

                this.treeView1.Focus();


            }
            else
            {
                //this.yhbmTextBox.ReadOnly = false  ;
                //this.yhmcTextBox.ReadOnly = false  ;
                //this.pwdTextBox.ReadOnly = false  ;
                //this.group_IdComboBox.Enabled = true ;
                //this.month_salaryTextBox.ReadOnly = false ;

                //this.bindingNavigatorAddNewItem.Enabled = false;
                //this.bindingNavigatorDeleteItem.Enabled = false;
                //this.toolStripButton1.Enabled = false;
                //this.t_usersBindingNavigatorSaveItem.Enabled = true;

                //this.bindingNavigatorMoveFirstItem.Enabled = false;
                //this.bindingNavigatorMoveLastItem.Enabled = false;
                //this.bindingNavigatorMovePreviousItem.Enabled = false;
                //this.bindingNavigatorMoveNextItem.Enabled = false;
                //this.bindingNavigatorPositionItem.Enabled = false;

                //this.genderComboBox.Enabled = true ;
                //this.identityCardTextBox.ReadOnly = false ;
                //this.phoneTextBox.ReadOnly = false ;
                //this.addressTextBox.ReadOnly = false ;
                //this.inwork_dayDateTimePicker.Enabled = true ;
                //this.in_active_serviceCheckBox.Enabled = true ;
                ////this.duty_classComboBox.Enabled = true;
                //this.onlineTextBox.ReadOnly = false ;

            }

        }

        private void Yonghu_Load(object sender, EventArgs e)
        {

            this.tabPage2.Parent = null;
            this.tabPage3.Parent = null;
            this.tabPage4.Parent = null;
            this.tabPage5.Parent = null;
            this.tabPage6.Parent = null;
            this.tabPage7.Parent = null;

          
            this.nowusercode = SQLDatabase.NowUserID;

            //this.dataGridViewSummary1.SummaryColumns;

            this.ly_sales_standard_SumTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            this.ly_sales_standard_Report_giveTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            this.ly_sales_standard_Report_financialTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;

           

            this.ly_sales_standard_SumMarpossTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            
            this.ly_sales_standard_SumMarpossNewTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;

            this.ly_sales_standard_SumMarpossNew2TableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;



            

            //this.dateTimePicker5.Text = SQLDatabase.GetNowdate().AddYears(-1).Date.Year.ToString() + "-12" + "-26";
            this.dateTimePicker5.Text = SQLDatabase.GetNowdate().Date.Year.ToString() + "-01" + "-01";
           
            this.dateTimePicker6.Text = SQLDatabase.GetNowdate().AddDays(0).Date.ToString();

            //this.dateTimePicker7.Text = SQLDatabase.GetNowdate().AddYears(-1).Date.Year.ToString() + "-12" + "-26";
            this.dateTimePicker7.Text = SQLDatabase.GetNowdate().Date.Year.ToString() + "-01" + "-01";
            this.dateTimePicker8.Text = SQLDatabase.GetNowdate().AddDays(0).Date.ToString();

            this.dateTimePicker9.Text = SQLDatabase.GetNowdate().Date.Year.ToString() + "-01" + "-01";
            this.dateTimePicker10.Text = SQLDatabase.GetNowdate().AddDays(0).Date.ToString();

            this.dateTimePicker11.Text = SQLDatabase.GetNowdate().Date.Year.ToString() + "-01" + "-01";
            this.dateTimePicker12.Text = SQLDatabase.GetNowdate().AddDays(0).Date.ToString();

            this.dateTimePicker13.Text = SQLDatabase.GetNowdate().Date.Year.ToString() + "-01" + "-01";
            this.dateTimePicker14.Text = SQLDatabase.GetNowdate().AddDays(0).Date.ToString();
         

         

            this.dateTimePicker3.Text = DateTime.Today.AddMonths(-1).Date.ToString();
            this.dateTimePicker4.Text = DateTime.Today.AddDays(0).Date.ToString();
           
          
            this.dateTimePicker1.Text = DateTime.Today.AddMonths(-1).Date.ToString();
            this.dateTimePicker2.Text = DateTime.Today.AddDays(0).Date.ToString();

          
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
              
                
            }

            //this.ly_sales_contract_main1DataGridView.SelectionChanged -= ly_sales_contract_main1DataGridView_SelectionChanged;
            //this.ly_sales_contract_main1TableAdapter.Fill(this.lYSalseMange.ly_sales_contract_main1,"","full" );
            //this.ly_sales_contract_main1DataGridView.SelectionChanged += ly_sales_contract_main1DataGridView_SelectionChanged;

            //SetViewState("View");

        }

      



        private void MakeTreeView(DataTable table, string salesregionCode, System.Windows.Forms.TreeNode PNode)
        {


            DataRow[] dr;
            string now_salesregion_code;
            string last_salesregion_code="___";

          

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

        private void bindingNavigatorAddNewItem_Click(object sender, EventArgs e)
        {

        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            //this.ly_sales_clientBindingSource.Filter = e.Node.Tag.ToString() ;
            this.nowfilterStr = e.Node.Tag.ToString();

            if (e.Node.Level == 2)
            {
                //this.ly_sales_clientDataGridView.Columns["yhbm"].Visible = false;
                //this.ly_sales_clientDataGridView.Columns["yhmc"].Visible = false;

                this.nowusercode = this.nowfilterStr.Substring(this.nowfilterStr.Length - 4, 3);
                this.nowfillstragecode = "single";


                //this.ly_sales_contract_main1DataGridView.SelectionChanged -= ly_sales_contract_main1DataGridView_SelectionChanged;
                //this.ly_sales_contract_main1TableAdapter.Fill(this.lYSalseMange.ly_sales_contract_main1, this.nowusercode,"single", this.dateTimePicker1.Value , this.dateTimePicker2.Value );
                //AddSummationRow_New(ly_sales_contract_main1BindingSource, ly_sales_contract_main1DataGridView);
                //this.ly_sales_contract_main1DataGridView.SelectionChanged += ly_sales_contract_main1DataGridView_SelectionChanged;

            }
            else if (e.Node.Level == 1)
            {
                //this.ly_sales_clientDataGridView.Columns["yhbm"].Visible = true ;
                //this.ly_sales_clientDataGridView.Columns["yhmc"].Visible = true ;

                this.nowusercode = this.nowfilterStr.Substring(this.nowfilterStr.Length - 3, 2);
                this.nowfillstragecode = "region";

                //this.ly_sales_contract_main1DataGridView.SelectionChanged -= ly_sales_contract_main1DataGridView_SelectionChanged;
                //this.ly_sales_contract_main1TableAdapter.Fill(this.lYSalseMange.ly_sales_contract_main1, this.nowusercode, "region", this.dateTimePicker1.Value, this.dateTimePicker2.Value);
                //AddSummationRow_New(ly_sales_contract_main1BindingSource, ly_sales_contract_main1DataGridView);
                //this.ly_sales_contract_main1DataGridView.SelectionChanged += ly_sales_contract_main1DataGridView_SelectionChanged;
            }
            else if (e.Node.Level == 0)
            {
                //this.ly_sales_clientDataGridView.Columns["yhbm"].Visible = true;
                //this.ly_sales_clientDataGridView.Columns["yhmc"].Visible = true;

                this.nowusercode = "";
                this.nowfillstragecode = "full";

                if (SQLDatabase.CheckHaveRight(SQLDatabase.NowUserID, "营业区域信息"))
                {
                    this.nowusercode = this.nowfilterStr.Substring(this.nowfilterStr.Length - 3, 2);
                    this.nowfillstragecode = "region";
                }

                //this.ly_sales_contract_main1DataGridView.SelectionChanged -= ly_sales_contract_main1DataGridView_SelectionChanged;
                //this.ly_sales_contract_main1TableAdapter.Fill(this.lYSalseMange.ly_sales_contract_main1, this.nowusercode, "full", this.dateTimePicker1.Value, this.dateTimePicker2.Value);
                //AddSummationRow_New(ly_sales_contract_main1BindingSource, ly_sales_contract_main1DataGridView);
                //this.ly_sales_contract_main1DataGridView.SelectionChanged += ly_sales_contract_main1DataGridView_SelectionChanged;
            }

            //if (0 == this.tabControl1.SelectedIndex)
            //{

            //    this.ly_sales_standard_SumTableAdapter.Fill(this.lYSalseRepair.ly_sales_standard_Sum, this.nowusercode, this.nowfillstragecode, this.dateTimePicker5.Value, this.dateTimePicker6.Value.AddDays(1));
            //    SetDGVFir(this.ly_sales_standard_SumDataGridView);
            //}

            //if (1 == this.tabControl1.SelectedIndex)
            //{
            //    TableForSumWeek();
            //}

            if (0 == this.tabControl1.SelectedIndex || 2 == this.tabControl1.SelectedIndex)
            {

                //this.ly_sales_standard_Report_giveTableAdapter.Fill(this.lYSalseMange.ly_sales_borrow_detail_all, this.nowusercode, this.nowfillstragecode, this.dateTimePicker1.Value, this.dateTimePicker2.Value);
                this.ly_sales_standard_Report_giveTableAdapter.Fill(this.lYSalseMange2.ly_sales_standard_Report_give, this.nowusercode, this.nowfillstragecode, this.dateTimePicker1.Value, this.dateTimePicker2.Value.AddDays(1));
                AddSummationRow_New(ly_sales_standard_Report_giveBindingSource, ly_sales_standard_Report_giveDataGridView);

                this.ly_sales_standard_Report_financialTableAdapter.Fill(this.lYSalseMange2.ly_sales_standard_Report_financial, this.nowusercode, this.nowfillstragecode, this.dateTimePicker3.Value, this.dateTimePicker4.Value.AddDays(1));
               
                
                InitializeApp();

                AddSummationRow_New(ly_sales_standard_Report_financialBindingSource, ly_sales_contract_standard_ReportDataGridView);
            }

            if (4 == this.tabControl1.SelectedIndex)
            {

                this.ly_sales_standard_SumMarpossTableAdapter.Fill(this.lYSalseRepair.ly_sales_standard_SumMarposs, this.nowusercode, this.nowfillstragecode, this.dateTimePicker9.Value, this.dateTimePicker10.Value.AddDays(1));

                SetDGVFir(this.ly_sales_standard_SumMarpossDataGridView);
            }


        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {

        }

        private void t_usersBindingSource_CurrentChanged(object sender, EventArgs e)
        {
            System.Windows.Forms.BindingSource bs = sender as BindingSource;

            //this.itemofserviceBindingSource.Filter = "Teacher = '" + ((DataRowView)bs.Current)["yhbm"] + "'";
            //this.duty_classComboBox.DataSource = null;
            //this.duty_classComboBox.DataSource = this.itemofserviceBindingSource;
            //this.duty_classComboBox.DisplayMember = "ItemofserviceName";
            //this.duty_classComboBox.ValueMember = "ItemofserviceNumber";
            if (null != (DataRowView)bs.Current)
            {
                //this.ly_employe_warehouseTableAdapter.Fill(this.yonghuDataSet.ly_employe_warehouse, ((DataRowView)bs.Current)["yhbm"].ToString());
                //this.itemofserviceTableAdapter.Fill(this.yonghuDataSet.Itemofservice, ((DataRowView)bs.Current)["yhbm"].ToString());
            }
           

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

        private void AddSummationRow_New(BindingSource bs, DataGridView dgv)
        {
            //InitializeApp();
            //return;

            DataRow sumdr = (((DataSet)bs.DataSource).Tables[bs.DataMember]).NewRow();

            if (-1 != bs.Find("清单号", "合计"))
            {
                bs.RemoveAt(bs.Find("清单号", "合计"));
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
                            if ("年份" != dgvCell.OwningColumn.HeaderText && "月份" != dgvCell.OwningColumn.HeaderText)
                            {
                                if (sumdr[dgvCell.OwningColumn.DataPropertyName] == null || (sumdr[dgvCell.OwningColumn.DataPropertyName] is DBNull))
                                    sumdr[dgvCell.OwningColumn.DataPropertyName] = 0;


                                sumdr[dgvCell.OwningColumn.DataPropertyName] = Convert.ToInt64(sumdr[dgvCell.OwningColumn.DataPropertyName]) + Convert.ToInt64(dgvCell.Value);
                            }
                        }
                        else if (IsDecimal(dgvCell.Value))
                        {
                            if ("年份" != dgvCell.OwningColumn.HeaderText && "月份" != dgvCell.OwningColumn.HeaderText )
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

            sumdr["sumorder"] = "02";
            sumdr["清单号"] = "合计";
            sumdr["客户"] = "";
            sumdr["税务"] = "";
            ((DataSet)bs.DataSource).Tables[bs.DataMember].Rows.Add(sumdr);
            bs.ResetBindings(true);

        }

       

        private void toolStripButton16_Click_1(object sender, EventArgs e)
        {
            ExportDataGridviewTOExcell.ExportDataGridview(this.ly_sales_standard_Report_giveDataGridView, true);
        }

        private void toolStripButton21_Click_1(object sender, EventArgs e)
        {
            this.ly_sales_standard_Report_giveTableAdapter.Fill(this.lYSalseMange2.ly_sales_standard_Report_give, this.nowusercode, this.nowfillstragecode, this.dateTimePicker1.Value, this.dateTimePicker2.Value.AddDays(1));
            AddSummationRow_New(ly_sales_standard_Report_giveBindingSource, ly_sales_standard_Report_giveDataGridView);

        }

        private void toolStripTextBox4_KeyUp(object sender, KeyEventArgs e)
        {
            string filterString;


            filterString = GetDataGridviewMultiFilterString.DGVMultiFilterString(this.ly_sales_standard_Report_giveDataGridView, this.toolStripTextBox4.Text);


            this.ly_sales_standard_Report_giveBindingSource.Filter = filterString;
        }

       

        private void toolStripTextBox4_Enter(object sender, EventArgs e)
        {
            toolStripTextBox4.Text = "";

            this.ly_sales_standard_Report_giveBindingSource.Filter = "";
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            ExportDataGridviewTOExcell.ExportDataGridview(this.ly_sales_contract_standard_ReportDataGridView, true);
        }

        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            //this.ly_sales_contract_standard_ReportTableAdapter.Fill(this.lYSalseMange2.ly_sales_contract_standard_Report, this.nowusercode, this.nowfillstragecode, this.dateTimePicker3.Value, this.dateTimePicker4.Value.AddDays(1));
            this.ly_sales_standard_Report_financialTableAdapter.Fill(this.lYSalseMange2.ly_sales_standard_Report_financial, this.nowusercode, this.nowfillstragecode, this.dateTimePicker3.Value, this.dateTimePicker4.Value.AddDays(1));
               
            
            // InitializeApp();

            AddSummationRow_New(ly_sales_standard_Report_financialBindingSource, ly_sales_contract_standard_ReportDataGridView);
            
            //AddSummationRow_New(ly_sales_contract_standard_ReportBindingSource, ly_sales_contract_standard_ReportDataGridView);
            

        }

        public void refresh_weekdata( string  begindate ,string  enddate)
        {

            this.dateTimePicker3.Text = begindate;
            this.dateTimePicker4.Text = enddate;

            //this.ly_sales_contract_standard_ReportTableAdapter.Fill(this.lYSalseMange2.ly_sales_contract_standard_Report, this.nowusercode, this.nowfillstragecode, this.dateTimePicker3.Value, this.dateTimePicker4.Value.AddDays(1));

            this.ly_sales_standard_Report_financialTableAdapter.Fill(this.lYSalseMange2.ly_sales_standard_Report_financial, this.nowusercode, this.nowfillstragecode, this.dateTimePicker3.Value, this.dateTimePicker4.Value.AddDays(1));
               
            // InitializeApp();

            AddSummationRow_New(ly_sales_standard_Report_financialBindingSource, ly_sales_contract_standard_ReportDataGridView);
           


        }

        private void ly_sales_contract_standard_ReportDataGridView_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            string isgood = "yes";

            decimal nowmoney;

           

            foreach (DataGridViewRow dgr in ly_sales_contract_standard_ReportDataGridView.Rows)
            {


                if ("" != dgr.Cells["金额"].Value.ToString())
                {
                    nowmoney = decimal.Parse(dgr.Cells["金额"].Value.ToString());
                }
                else
                {
                    nowmoney = 0;
                }







                if (nowmoney <= 0)
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

        private void toolStripButton7_Click(object sender, EventArgs e)
        {
            if (null == this.ly_sales_contract_standard_ReportDataGridView.CurrentRow)    return;

            FilterForm filterForm = new FilterForm();

            //SumQueryDataSet qds;
            //qds = new SumQueryDataSet();

            List<string> ls = new List<string>();
            ls.Add("id");


            filterForm.SetSourceColumns(this .lYSalseMange2.ly_sales_contract_standard_Report.Columns, ls);

            filterForm.ShowDialog();

            string filterstr = filterForm.GetFilterString();
            if (!string.IsNullOrEmpty(filterstr))
            {

                this.ly_sales_standard_Report_financialBindingSource.Filter = "(" + filterstr + ") or 清单号='合计'";
            }

          
            AddSummationRow_New(ly_sales_standard_Report_financialBindingSource, ly_sales_contract_standard_ReportDataGridView);

        }

        private void toolStripButton8_Click(object sender, EventArgs e)
        {
            if (null == this.ly_sales_contract_standard_ReportDataGridView.CurrentRow) return;
            SortForm DataSort = new SortForm();

            List<string> ls = new List<string>();
            ls.Add("id");


            DataSort.SetSortColumns(this.lYSalseMange2.ly_sales_contract_standard_Report.Columns, ls);
            DataSort.ShowDialog();
            this.ly_sales_standard_Report_financialBindingSource.Sort = " sumorder asc," + DataSort.GetSortString();
            AddSummationRow_New(ly_sales_standard_Report_financialBindingSource, ly_sales_contract_standard_ReportDataGridView);

           
        }

        private void toolStripTextBox2_KeyUp(object sender, KeyEventArgs e)
        {
            string filterString;


            filterString = GetDataGridviewMultiFilterString.DGVMultiFilterString(this.ly_sales_contract_standard_ReportDataGridView, this.toolStripTextBox2.Text);


            this.ly_sales_standard_Report_financialBindingSource.Filter = "(" + filterString + ") or 清单号='合计'";
            AddSummationRow_New(ly_sales_standard_Report_financialBindingSource, ly_sales_contract_standard_ReportDataGridView);

            


           
        }

        private void toolStripTextBox2_Enter(object sender, EventArgs e)
        {
            toolStripTextBox2.Text = "";

            this.ly_sales_standard_Report_financialBindingSource.Filter = "";
            AddSummationRow_New(ly_sales_standard_Report_financialBindingSource, ly_sales_contract_standard_ReportDataGridView);

            

        }

        private void ly_sales_contract_standard_ReportDataGridView_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            DataGridView dgv = sender as DataGridView;

            if (null == dgv.CurrentRow) return;

            string nowcontract_code = dgv.CurrentRow.Cells["清单号"].Value.ToString();

            

            string client_code = dgv.CurrentRow.Cells["客户编码"].Value.ToString();
            DataTable dt_client = null;
            string sqlMax = @"select top 1 start_time from ly_sales_client where salesclient_code='"+ client_code + "' order by id desc";
            using (SqlConnection connection = new SqlConnection(SQLDatabase.Connectstring))
            {
       
                SqlDataAdapter adapter = new SqlDataAdapter(sqlMax, connection);
                DataSet ds = new DataSet();
                adapter.Fill(ds);
                dt_client = ds.Tables[0];
            }
            if (dt_client.Rows.Count <= 0)
            {
                MessageBox.Show("  没有期初...", "注意");
                return;

            }
            else
            {
                if (string.IsNullOrEmpty(dt_client.Rows[0][0].ToString()))
                {

                    MessageBox.Show("  没有期初...", "注意");
                    return;

                }
            }



            if (dgv.CurrentRow.Cells["提交清单"].Value.ToString() != "True")
            {

                MessageBox.Show(" 提交清单没有提交（该合同配套书发货没有全部完成）...", "注意");
                return;
            }

            if (!string.IsNullOrEmpty(dgv.CurrentRow.Cells["开票日期"].Value.ToString()))
            {
                DateTime time =DateTime.Parse( dgv.CurrentRow.Cells["开票日期"].Value.ToString());
                DateTime now = SQLDatabase.GetNowtime();
                if ((now - time).Days >= 30)
                {
                    MessageBox.Show("超过30天...","注意");
                    return;
                }
            }
        



            string updstr="";

            ///////////////////////////////////////////////////////////////

            if ("开票日期" == dgv.CurrentCell.OwningColumn.Name)
            {

                //if ("XS" != nowcontract_code.Substring(0, 2))
                //{

                //    MessageBox.Show("只能修改合同开票日期...", "注意");
                //    return;

                //}

                ChangeValue queryForm = new ChangeValue();

                queryForm.OldValue = dgv.CurrentCell.Value.ToString();
                queryForm.NewValue = "";
                queryForm.ChangeMode = "datetime";
                queryForm.ShowDialog();





                if (queryForm.NewValue != "")
                {
                    dgv.CurrentRow.Cells["开票日期"].Value = queryForm.NewValue;
                    dgv.CurrentRow.Cells["开票人"].Value = SQLDatabase.nowUserName();

                    if ("XS" == nowcontract_code.Substring(0, 2))
                    {
                        updstr = " update ly_sales_contract_main  " +
                                       "  set invoice_date=  '" + queryForm.NewValue + "',invoice_people='" + SQLDatabase.nowUserName() + "' where  contract_inner_code='" + nowcontract_code + "'";
                    }
                    else
                    {

                        updstr = " update ly_sales_receive  " +
                                            "  set invoice_date=  '" + queryForm.NewValue + "',invoice_people='" + SQLDatabase.nowUserName() + "' where  repair_bill_code='" + nowcontract_code + "'";
                    }

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
                }


                return;
            }
            ////////////////////////////////////////////////////////////////////////

            ///////////////////////////////////////////////////////////////

            if ("发票号" == dgv.CurrentCell.OwningColumn.Name)
            {

                //if ("XS" != nowcontract_code.Substring(0, 2))
                //{

                //    MessageBox.Show("只能修改合同开票日期...", "注意");
                //    return;

                //}

                ChangeValue queryForm = new ChangeValue();

                queryForm.OldValue = dgv.CurrentCell.Value.ToString();
                queryForm.NewValue = "";
                queryForm.ChangeMode = "string";
                queryForm.ShowDialog();





                if (queryForm.NewValue != "")
                {
                    dgv.CurrentRow.Cells["发票号"].Value = queryForm.NewValue;
                    dgv.CurrentRow.Cells["开票人"].Value = SQLDatabase.nowUserName();
                    dgv.CurrentRow.Cells["开票日期"].Value = SQLDatabase.GetNowdate();
                    if ("XS" == nowcontract_code.Substring(0, 2))
                    {
                        updstr = " update ly_sales_contract_main  " +
                                       "  set invoice_num=  '" + queryForm.NewValue + "',invoice_date='"+ SQLDatabase.GetNowdate() + "',invoice_people='"+ SQLDatabase.nowUserName() + "' where  contract_inner_code='" + nowcontract_code + "'";
                    }
                    else
                    {
                        updstr = " update ly_sales_receive  " +
                                          "  set invoice_num=  '" + queryForm.NewValue + "',invoice_date='" + SQLDatabase.GetNowdate() + "',invoice_people='" + SQLDatabase.nowUserName() + "'  where  repair_bill_code='" + nowcontract_code + "'";
                    }



                }
                else
                {

                    dgv.CurrentRow.Cells["发票号"].Value = DBNull.Value;
                    dgv.CurrentRow.Cells["开票人"].Value = DBNull.Value;
                    dgv.CurrentRow.Cells["开票日期"].Value = DBNull.Value;

                    if ("XS" == nowcontract_code.Substring(0, 2))
                    {
                        updstr = " update ly_sales_contract_main  " +
                                   "  set invoice_num= null ,invoice_date=null,invoice_people=null where  contract_inner_code='" + nowcontract_code + "'";
                    }
                    else
                    {
                        updstr = " update ly_sales_receive  " +
                                   "  set invoice_num= null ,invoice_date=null,invoice_people=null where  repair_bill_code='" + nowcontract_code + "'";
                    }

                }

                 
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



                //////////////////////////////



                return;

            }
            ////////////////////////////////////////////////////////////////////////
            ///////////////////////////////////////////////////////////////

            if ("预收款" == dgv.CurrentCell.OwningColumn.Name)
            {

                //if ("XS" != nowcontract_code.Substring(0, 2))
                //{

                //    MessageBox.Show("只能修改合同开票日期...", "注意");
                //    return;

                //}

                ChangeValue queryForm = new ChangeValue();

                queryForm.OldValue = dgv.CurrentCell.Value.ToString();
                queryForm.NewValue = "";
                queryForm.ChangeMode = "value";
                queryForm.ShowDialog();





                if (queryForm.NewValue != "")
                {
                    dgv.CurrentRow.Cells["预收款"].Value = queryForm.NewValue;
                    //dgv.CurrentRow.Cells["开票人"].Value = SQLDatabase.nowUserName;

                    if ("XS" == nowcontract_code.Substring(0, 2))
                    {
                        updstr = " update ly_sales_contract_main  " +
                                       "  set prepaid_fee=  '" + queryForm.NewValue + "' where  contract_inner_code='" + nowcontract_code + "'";
                    }
                    else
                    {
                        updstr = " update ly_sales_receive  " +
                                          "  set prepaid_fee=  '" + queryForm.NewValue + "' where  repair_bill_code='" + nowcontract_code + "'";
                    }

                    //if ("XS" != nowcontract_code.Substring(0, 2))
                    //{
                    //    updstr = " update ly_sales_contract_main  " +
                    //                   "  set invoice_date=  '" + queryForm.NewValue + "',invoice_people='" + SQLDatabase.nowUserName + "' where  contract_inner_code='" + nowcontract_code + "'";
                    //}



                }
                else
                {

                    //dgv.CurrentRow.Cells["预收款"].Value = DBNull.Value;
                    ////dgv.CurrentRow.Cells["开票人"].Value = DBNull.Value;

                    //if ("XS" == nowcontract_code.Substring(0, 2))
                    //{
                    //    updstr = " update ly_sales_contract_main  " +
                    //               "  set prepaid_fee= null where  contract_inner_code='" + nowcontract_code + "'";
                    //}
                    //else
                    //{
                    //    updstr = " update ly_sales_receive  " +
                    //               "  set prepaid_fee= null where  repair_bill_code='" + nowcontract_code + "'";
                    //}


                }


                ///////////////////




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



                //////////////////////////////



                return;

            }
            ////////////////////////////////////////////////////////////////////////
            ///////////////////////////////////////////////////////////////

            if ("回款" == dgv.CurrentCell.OwningColumn.Name)
            {

                //if ("XS" != nowcontract_code.Substring(0, 2))
                //{

                //    MessageBox.Show("只能修改合同开票日期...", "注意");
                //    return;

                //}

                ChangeValue queryForm = new ChangeValue();

                queryForm.OldValue = dgv.CurrentCell.Value.ToString();
                queryForm.NewValue = "";
                queryForm.ChangeMode = "value";
                queryForm.ShowDialog();


 

                if (queryForm.NewValue != "")
                {
                    dgv.CurrentRow.Cells["回款"].Value = queryForm.NewValue;
                    //dgv.CurrentRow.Cells["开票人"].Value = SQLDatabase.nowUserName;

                    if ("XS" == nowcontract_code.Substring(0, 2))
                    {
                        updstr = " update ly_sales_contract_main  " +
                                       "  set back_fee=  '" + queryForm.NewValue + "' where  contract_inner_code='" + nowcontract_code + "'";
                    }
                    else
                    {
                        updstr = " update ly_sales_receive  " +
                                          "  set back_fee=  '" + queryForm.NewValue + "' where  repair_bill_code='" + nowcontract_code + "'";
                    }

                    //if ("XS" != nowcontract_code.Substring(0, 2))
                    //{
                    //    updstr = " update ly_sales_contract_main  " +
                    //                   "  set invoice_date=  '" + queryForm.NewValue + "',invoice_people='" + SQLDatabase.nowUserName + "' where  contract_inner_code='" + nowcontract_code + "'";
                    //}



                }
                else
                {

                    //dgv.CurrentRow.Cells["回款"].Value = DBNull.Value;
                    ////dgv.CurrentRow.Cells["开票人"].Value = DBNull.Value;

                    //if ("XS" == nowcontract_code.Substring(0, 2))
                    //{
                    //    updstr = " update ly_sales_contract_main  " +
                    //               "  set back_fee= null where  contract_inner_code='" + nowcontract_code + "'";
                    //}
                    //else
                    //{
                    //    updstr = " update ly_sales_receive  " +
                    //               "  set back_fee= null where  repair_bill_code='" + nowcontract_code + "'";
                    //}


                }


                ///////////////////




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



                //////////////////////////////



                return;

            }
            ////////////////////////////////////////////////////////////////////////
            ///////////////////////////////////////////////////////////////

            if ("开票人" == dgv.CurrentCell.OwningColumn.Name)
            {

                //if ("XS" != nowcontract_code.Substring(0, 2))
                //{

                //    MessageBox.Show("只能修改合同开票日期...", "注意");
                //    return;

                //}

                ChangeValue queryForm = new ChangeValue();

                queryForm.OldValue = dgv.CurrentCell.Value.ToString();
                queryForm.NewValue = "";
                queryForm.ChangeMode = "string";
                queryForm.ShowDialog();





                if (queryForm.NewValue != "")
                {
                    dgv.CurrentRow.Cells["开票人"].Value = queryForm.NewValue;
                    //dgv.CurrentRow.Cells["开票人"].Value = SQLDatabase.nowUserName;

                    if ("XS" == nowcontract_code.Substring(0, 2))
                    {
                        updstr = " update ly_sales_contract_main  " +
                                       "  set invoice_people=  '" + queryForm.NewValue + "' where  contract_inner_code='" + nowcontract_code + "'";
                    }
                    else
                    {
                        updstr = " update ly_sales_receive  " +
                                          "  set invoice_people=  '" + queryForm.NewValue + "' where  repair_bill_code='" + nowcontract_code + "'";
                    }

                    //if ("XS" != nowcontract_code.Substring(0, 2))
                    //{
                    //    updstr = " update ly_sales_contract_main  " +
                    //                   "  set invoice_date=  '" + queryForm.NewValue + "',invoice_people='" + SQLDatabase.nowUserName + "' where  contract_inner_code='" + nowcontract_code + "'";
                    //}



                }
                else
                {

                    //dgv.CurrentRow.Cells["开票人"].Value = DBNull.Value;
                    ////dgv.CurrentRow.Cells["开票人"].Value = DBNull.Value;

                    //if ("XS" == nowcontract_code.Substring(0, 2))
                    //{
                    //    updstr = " update ly_sales_contract_main  " +
                    //               "  set invoice_people= null where  contract_inner_code='" + nowcontract_code + "'";
                    //}
                    //else
                    //{
                    //    updstr = " update ly_sales_receive  " +
                    //               "  set invoice_people= null where  repair_bill_code='" + nowcontract_code + "'";
                    //}


                }


                ///////////////////




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



                //////////////////////////////



                return;

            }
            ////////////////////////////////////////////////////////////////////////
            ///////////////////////////////////////////////////////////////

            if ("预收日期" == dgv.CurrentCell.OwningColumn.Name)
            {

                //if ("XS" != nowcontract_code.Substring(0, 2))
                //{

                //    MessageBox.Show("只能修改合同开票日期...", "注意");
                //    return;

                //}

                ChangeValue queryForm = new ChangeValue();

                queryForm.OldValue = dgv.CurrentCell.Value.ToString();
                queryForm.NewValue = "";
                queryForm.ChangeMode = "datetime";
                queryForm.ShowDialog();





                if (queryForm.NewValue != "")
                {
                    dgv.CurrentRow.Cells["预收日期"].Value = queryForm.NewValue;
                    //dgv.CurrentRow.Cells["开票人"].Value = SQLDatabase.nowUserName;

                    if ("XS" == nowcontract_code.Substring(0, 2))
                    {
                        updstr = " update ly_sales_contract_main  " +
                                       "  set prepaid_date=  '" + queryForm.NewValue + "' where  contract_inner_code='" + nowcontract_code + "'";
                    }
                    else
                    {
                        updstr = " update ly_sales_receive  " +
                                          "  set prepaid_date=  '" + queryForm.NewValue + "' where  repair_bill_code='" + nowcontract_code + "'";
                    }

                    //if ("XS" != nowcontract_code.Substring(0, 2))
                    //{
                    //    updstr = " update ly_sales_contract_main  " +
                    //                   "  set invoice_date=  '" + queryForm.NewValue + "',invoice_people='" + SQLDatabase.nowUserName + "' where  contract_inner_code='" + nowcontract_code + "'";
                    //}



                }
                else
                {

                    //dgv.CurrentRow.Cells["预收日期"].Value = DBNull.Value;
                    ////dgv.CurrentRow.Cells["开票人"].Value = DBNull.Value;

                    //if ("XS" == nowcontract_code.Substring(0, 2))
                    //{
                    //    updstr = " update ly_sales_contract_main  " +
                    //               "  set prepaid_date= null where  contract_inner_code='" + nowcontract_code + "'";
                    //}
                    //else
                    //{
                    //    updstr = " update ly_sales_receive  " +
                    //               "  set prepaid_date= null where  repair_bill_code='" + nowcontract_code + "'";
                    //}


                }


                ///////////////////




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



                //////////////////////////////



                return;

            }
            ////////////////////////////////////////////////////////////////////////
            ///////////////////////////////////////////////////////////////

            if ("回款日期" == dgv.CurrentCell.OwningColumn.Name)
            {

                //if ("XS" != nowcontract_code.Substring(0, 2))
                //{

                //    MessageBox.Show("只能修改合同开票日期...", "注意");
                //    return;

                //}

                ChangeValue queryForm = new ChangeValue();

                queryForm.OldValue = dgv.CurrentCell.Value.ToString();
                queryForm.NewValue = "";
                queryForm.ChangeMode = "datetime";
                queryForm.ShowDialog();





                if (queryForm.NewValue != "")
                {
                    dgv.CurrentRow.Cells["回款日期"].Value = queryForm.NewValue;
                    //dgv.CurrentRow.Cells["开票人"].Value = SQLDatabase.nowUserName;

                    if ("XS" == nowcontract_code.Substring(0, 2))
                    {
                        updstr = " update ly_sales_contract_main  " +
                                       "  set back_date=  '" + queryForm.NewValue + "' where  contract_inner_code='" + nowcontract_code + "'";
                    }
                    else
                    {
                        updstr = " update ly_sales_receive  " +
                                          "  set back_date=  '" + queryForm.NewValue + "' where  repair_bill_code='" + nowcontract_code + "'";
                    }

                    //if ("XS" != nowcontract_code.Substring(0, 2))
                    //{
                    //    updstr = " update ly_sales_contract_main  " +
                    //                   "  set invoice_date=  '" + queryForm.NewValue + "',invoice_people='" + SQLDatabase.nowUserName + "' where  contract_inner_code='" + nowcontract_code + "'";
                    //}



                }
                else
                {

                    //dgv.CurrentRow.Cells["回款日期"].Value = DBNull.Value;
                    ////dgv.CurrentRow.Cells["开票人"].Value = DBNull.Value;

                    //if ("XS" == nowcontract_code.Substring(0, 2))
                    //{
                    //    updstr = " update ly_sales_contract_main  " +
                    //               "  set back_date= null where  contract_inner_code='" + nowcontract_code + "'";
                    //}
                    //else
                    //{
                    //    updstr = " update ly_sales_receive  " +
                    //               "  set back_date= null where  repair_bill_code='" + nowcontract_code + "'";
                    //}


                }


                ///////////////////




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



                //////////////////////////////



                return;

            }
            ////////////////////////////////////////////////////////////////////////
            ///////////////////////////////////////////////////////////////

            if ("佣金日期" == dgv.CurrentCell.OwningColumn.Name)
            {

                //if ("XS" != nowcontract_code.Substring(0, 2))
                //{

                //    MessageBox.Show("只能修改合同开票日期...", "注意");
                //    return;

                //}

                ChangeValue queryForm = new ChangeValue();

                queryForm.OldValue = dgv.CurrentCell.Value.ToString();
                queryForm.NewValue = "";
                queryForm.ChangeMode = "datetime";
                queryForm.ShowDialog();





                if (queryForm.NewValue != "")
                {
                    dgv.CurrentRow.Cells["佣金日期"].Value = queryForm.NewValue;
                    //dgv.CurrentRow.Cells["开票人"].Value = SQLDatabase.nowUserName;

                    if ("XS" == nowcontract_code.Substring(0, 2))
                    {
                        updstr = " update ly_sales_contract_main  " +
                                       "  set commission_date=  '" + queryForm.NewValue + "' where  contract_inner_code='" + nowcontract_code + "'";
                    }
                    else
                    {
                        updstr = " update ly_sales_receive  " +
                                          "  set commission_date=  '" + queryForm.NewValue + "' where  repair_bill_code='" + nowcontract_code + "'";
                    }

                    //if ("XS" != nowcontract_code.Substring(0, 2))
                    //{
                    //    updstr = " update ly_sales_contract_main  " +
                    //                   "  set invoice_date=  '" + queryForm.NewValue + "',invoice_people='" + SQLDatabase.nowUserName + "' where  contract_inner_code='" + nowcontract_code + "'";
                    //}



                }
                else
                {

                    //dgv.CurrentRow.Cells["佣金日期"].Value = DBNull.Value;
                    ////dgv.CurrentRow.Cells["开票人"].Value = DBNull.Value;

                    //if ("XS" == nowcontract_code.Substring(0, 2))
                    //{
                    //    updstr = " update ly_sales_contract_main  " +
                    //               "  set commission_date= null where  contract_inner_code='" + nowcontract_code + "'";
                    //}
                    //else
                    //{
                    //    updstr = " update ly_sales_receive  " +
                    //               "  set commission_date= null where  repair_bill_code='" + nowcontract_code + "'";
                    //}


                }


                ///////////////////




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



                //////////////////////////////



                return;
            }
            ////////////////////////////////////////////////////////////////////////
        }

        private void toolStripButton9_Click(object sender, EventArgs e)
        {
            this.ly_sales_standard_Report_financialBindingSource.Filter = " 公司='中原' or 清单号='合计'";
            AddSummationRow_New(ly_sales_standard_Report_financialBindingSource, ly_sales_contract_standard_ReportDataGridView);

           


        }

        private void toolStripButton10_Click(object sender, EventArgs e)
        {
            this.ly_sales_standard_Report_financialBindingSource.Filter = " 公司='中成'or 清单号='合计'";
            AddSummationRow_New(ly_sales_standard_Report_financialBindingSource, ly_sales_contract_standard_ReportDataGridView);

          

        }

        private void toolStripButton11_Click(object sender, EventArgs e)
        {
            this.ly_sales_standard_Report_financialBindingSource.Filter = "";
            AddSummationRow_New(ly_sales_standard_Report_financialBindingSource, ly_sales_contract_standard_ReportDataGridView);

        }

        private void dataGridViewSummary1_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            DataGridView dgv = sender as DataGridView;

            if (null == dgv.CurrentRow) return;

            string nowcontract_code = dgv.CurrentRow.Cells["清单号1"].Value.ToString();

            ///////////////////////////////////////////////////////////////

            if ("开票日期1" == dgv.CurrentCell.OwningColumn.Name)
            {

                if ("XS" != nowcontract_code.Substring(0, 2))
                {

                    MessageBox.Show("只能修改合同开票日期...", "注意");
                    return;

                }

                ChangeValue queryForm = new ChangeValue();

                queryForm.OldValue = dgv.CurrentCell.Value.ToString();
                queryForm.NewValue = "";
                queryForm.ChangeMode = "datetime";
                queryForm.ShowDialog();



                string updstr;

                if (queryForm.NewValue != "")
                {
                    dgv.CurrentRow.Cells["开票日期1"].Value = queryForm.NewValue;
                    //dgv.CurrentRow.Cells["开票人"].Value = "宋美彰";
                    updstr = " update ly_sales_contract_main  " +
                                   "  set invoice_date=  '" + queryForm.NewValue + "',invoice_people='宋美彰' where  contract_inner_code='" + nowcontract_code + "'";



                }
                else
                {

                    dgv.CurrentRow.Cells["开票日期1"].Value = DBNull.Value;
                    //dgv.CurrentRow.Cells["开票人"].Value = DBNull.Value;
                    updstr = " update ly_sales_contract_main  " +
                               "  set invoice_date= null, invoice_people=null where  contract_inner_code='" + nowcontract_code + "'";


                }


                ///////////////////




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



                //////////////////////////////



                return;

            }
            ////////////////////////////////////////////////////////////////////////
        }

        private void ly_sales_contract_standard_ReportDataGridView_Sorted(object sender, EventArgs e)
        {
            
            DataGridView nowview = sender as DataGridView;

            string nowdirection = "asc";
            string nowsort=this.ly_sales_standard_Report_financialBindingSource.Sort;

              
            //nowview.SortedColumn.DataPropertyName;
            //nowview.

            // DataGridViewColumn nowColumn = nowview.Columns[e.ColumnIndex].DataPropertyName;

            //DataGridViewColumn oldColumn = dataGridView1.SortedColumn;

            if (null!=nowsort && nowsort.Substring(nowsort .Length-5) == " desc")
            //if (nowview.SortOrder == System.Windows.Forms.SortOrder.Ascending)
            {
                nowdirection = " asc";
            }
            else
            {
                nowdirection = " desc";
            }


            this.ly_sales_standard_Report_financialBindingSource.Sort = " sumorder asc," + nowview.SortedColumn.DataPropertyName + nowdirection;
            AddSummationRow_New(ly_sales_standard_Report_financialBindingSource, ly_sales_contract_standard_ReportDataGridView);
      
        
        }

        private void ly_sales_contract_standard_ReportDataGridView_ColumnSortModeChanged(object sender, DataGridViewColumnEventArgs e)
        {
            //DataGridView nowview = sender as DataGridView;

            //string nowdirection="";
            ////nowview.SortedColumn.DataPropertyName;
            ////nowview.

            //   // DataGridViewColumn nowColumn = nowview.Columns[e.ColumnIndex].DataPropertyName;

            //   //DataGridViewColumn oldColumn = dataGridView1.SortedColumn;
           

            // if ( nowview.SortOrder == System.Windows.Forms.SortOrder.Ascending)
            // {
            //     nowdirection=" asc";
            // }
            // else 
            // {
            //    nowdirection=" desc";
            // }


            // this.ly_sales_contract_standard_ReportBindingSource.Sort =" sumorder asc,"+ nowview.SortedColumn.DataPropertyName + nowdirection;
            //   AddSummationRow_New(ly_sales_contract_standard_ReportBindingSource, ly_sales_contract_standard_ReportDataGridView);

              
           
        }

        private void ly_sales_contract_standard_ReportDataGridView_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {

        }

        private void toolStripButton22_Click(object sender, EventArgs e)
        {
            this.ly_sales_standard_SumTableAdapter.Fill(this.lYSalseRepair.ly_sales_standard_Sum, this.nowusercode, this.nowfillstragecode, this.dateTimePicker5.Value, this.dateTimePicker6.Value.AddDays(1));

            //this.ly_sales_standard_SumMarpossTableAdapter.Fill(this.lYSalseRepair.ly_sales_standard_SumMarposs, salesperson_codeToolStripTextBox.Text, selcodeToolStripTextBox.Text, new System.Nullable<System.DateTime>(((System.DateTime)(System.Convert.ChangeType(begindateToolStripTextBox.Text, typeof(System.DateTime))))), new System.Nullable<System.DateTime>(((System.DateTime)(System.Convert.ChangeType(enddateToolStripTextBox.Text, typeof(System.DateTime))))));
            

            SetDGVFir(this.ly_sales_standard_SumDataGridView); 
        }

        private void SetDGVFir(DataGridView nowDGV)
        {
            //nowDGV.Columns[0].Visible = false;

            for (int i = 0; i < nowDGV.Columns.Count; i++)
            {
                nowDGV.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;



                //nowDGV.Columns[i].ValueType System .Type .nowDGV.Columns[5].DefaultCellStyle.Alignment

                if ("Decimal" == nowDGV.Columns[i].ValueType.Name)
                {
                    nowDGV.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

                }

                if ("Int32" == nowDGV.Columns[i].ValueType.Name)
                {
                    nowDGV.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

                }

                if (nowDGV.Columns[i].HeaderText.Contains("总计") || nowDGV.Columns[i].HeaderText.Contains("增长率"))
                {
                    nowDGV.Columns[i].DefaultCellStyle.BackColor = Color.SkyBlue;
                    nowDGV.Columns[i].DefaultCellStyle.ForeColor = Color.Red;
                }

                if (nowDGV.Columns[i].HeaderText.Contains("总计区域") || nowDGV.Columns[i].HeaderText.Contains("合计"))
                {
                    nowDGV.Columns[i].DefaultCellStyle.BackColor = Color.Teal;
                    nowDGV.Columns[i].DefaultCellStyle.ForeColor = Color.White;
                }



            }



        }

        private void toolStripButton17_Click(object sender, EventArgs e)
        {
            ExportDataGridviewTOExcell.ExportDataGridview(this.ly_sales_standard_SumDataGridView, true);
        }

        private void toolStripButton18_Click(object sender, EventArgs e)
        {
            if (null == this.ly_sales_standard_SumDataGridView.CurrentRow) return;

            NewFrm.Show(this);


           
            BaseReportView queryForm = new BaseReportView();

            queryForm.Text = "营业部成品发货汇总";

            queryForm.Printdata = this.lYSalseRepair;

            queryForm.PrintCrystalReport = new LY_YingyeHetong_Huizong();

            //string selectFormula;

            //selectFormula = "{ly_store_planitemcount.状态}  =   '原料'  and {ly_store_planitemcount.欠料金额}>0 ";
            //queryForm.PrintCrystalReport.DataDefinition.RecordSelectionFormula = selectFormula;

            NewFrm.Hide(this);

            queryForm.ShowDialog();
        }

        private void toolStripButton19_Click(object sender, EventArgs e)
        {
            if (null == this.ly_sales_standard_SumDataGridView.CurrentRow) return;

            FilterForm filterForm = new FilterForm();




            //SumQueryDataSet qds;
            //qds = new SumQueryDataSet();

            List<string> ls = new List<string>();
            ls.Add("id");


            filterForm.SetSourceColumns(this.lYSalseRepair.ly_sales_standard_Sum.Columns, ls);

            filterForm.ShowDialog();

            this.ly_sales_standard_SumBindingSource.Filter = filterForm.GetFilterString();
        }

        private void toolStripButton20_Click(object sender, EventArgs e)
        {
            if (null == this.ly_sales_standard_SumDataGridView.CurrentRow) return;
            SortForm DataSort = new SortForm();

            List<string> ls = new List<string>();
            ls.Add("id");


            DataSort.SetSortColumns(this.lYSalseRepair.ly_sales_standard_Sum.Columns, ls);
            DataSort.ShowDialog();
            this.ly_sales_standard_SumBindingSource.Sort = DataSort.GetSortString();
        }

        private void toolStripButton31_Click(object sender, EventArgs e)
        {

            NewFrm.Show(this);
            TableForSumWeek();
            NewFrm.Hide(this);
        }

      

        private void TableForSumWeek()
        {
            

            SqlDataAdapter myAdapter1 = new SqlDataAdapter();

            myAdapter1.SelectCommand = new SqlCommand();

            myAdapter1.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter1.SelectCommand.CommandText = "ly_sales_standard_SumWeek";
            myAdapter1.SelectCommand.Connection = new SqlConnection(SQLDatabase.Connectstring);
            myAdapter1.SelectCommand.CommandTimeout = 0;

            myAdapter1.SelectCommand.Parameters.Add("@salesperson_code", SqlDbType.VarChar);
            myAdapter1.SelectCommand.Parameters["@salesperson_code"].Value =  this.nowusercode;

            myAdapter1.SelectCommand.Parameters.Add("@selcode", SqlDbType.VarChar);
            myAdapter1.SelectCommand.Parameters["@selcode"].Value = this.nowfillstragecode;

            myAdapter1.SelectCommand.Parameters.Add("@begindate", SqlDbType.DateTime);
            myAdapter1.SelectCommand.Parameters["@begindate"].Value = this.dateTimePicker7.Value;

            myAdapter1.SelectCommand.Parameters.Add("@enddate", SqlDbType.DateTime);
            myAdapter1.SelectCommand.Parameters["@enddate"].Value = this.dateTimePicker8.Value.AddDays(1);

          
       
         


            if (null != ds3)
                ds3.Dispose();

            ds3 = new DataSet();

            myAdapter1.Fill(ds3);


            myAdapter1.Dispose();

            //this.dataGridView1.DataSource = allData;


            this.dataGridView1.Columns.Clear();

            this.dataGridView1.AutoGenerateColumns = true;
            dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCellsExceptHeaders;

            //BindingSource bindingSource1 = new BindingSource();

            if (null != bindingSource3)
                this.bindingSource3.Dispose();

            this.bindingSource3 = new BindingSource();


            bindingSource3.DataSource = ds3.Tables[0];
            dataGridView1.DataSource = bindingSource3;

            this.bindingNavigator4.BindingSource = bindingSource3;

            bindingSource3.ResetBindings(true);


            SetDGV(this.dataGridView1);
        }

        private void GetWeekPeriod(string nowweek, out string nowweekperiod)
        {
            SqlConnection sqlConnection1 = new SqlConnection(SQLDatabase.Connectstring);
            SqlCommand cmd = new SqlCommand();



            cmd.Parameters.Add("@nowweek", SqlDbType.VarChar);
            cmd.Parameters["@nowweek"].Value = nowweek;


            cmd.Parameters.Add("@weekperiod", SqlDbType.VarChar);
            cmd.Parameters["@weekperiod"].Direction = ParameterDirection.Output;
            cmd.Parameters["@weekperiod"].Size = 30;








            cmd.CommandText = "LY_GetWeek_Period";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = sqlConnection1;

            sqlConnection1.Open();
            cmd.ExecuteNonQuery();
            sqlConnection1.Close();

            nowweekperiod = cmd.Parameters["@weekperiod"].Value.ToString();
         
        
        }
        private void SetDGV(DataGridView nowDGV)
        {
            //nowDGV.Columns[0].Visible = false;

            for (int i = 0; i < nowDGV.Columns.Count; i++)
            {
                nowDGV.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;

                if (i < 5)
                {

                    nowDGV.Columns[i].Frozen = true;
                }

                //nowDGV.Columns[i].ValueType System .Type .nowDGV.Columns[5].DefaultCellStyle.Alignment

                string weekperiod="---";

                if ("Decimal" == nowDGV.Columns[i].ValueType.Name)
                {
                    nowDGV.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

                    if ("总计" != nowDGV.Columns[i].Name)
                    {
                        GetWeekPeriod(nowDGV.Columns[i].Name, out weekperiod);

                        nowDGV.Columns[i].ToolTipText = weekperiod;
                    }

                }

                if (nowDGV.Columns[i].HeaderText.Contains("总计") || nowDGV.Columns[i].HeaderText.Contains("增长率"))
                {
                    //nowDGV.Columns[i].DefaultCellStyle.BackColor = Color.SkyBlue;
                    nowDGV.Columns[i].DefaultCellStyle.ForeColor = Color.Red;

                   
                }

                if (nowDGV.Columns[i].HeaderText.Contains("总计区域") || nowDGV.Columns[i].HeaderText.Contains("合计"))
                {
                    nowDGV.Columns[i].DefaultCellStyle.BackColor = Color.Teal;
                    nowDGV.Columns[i].DefaultCellStyle.ForeColor = Color.White;
                }



            }

          

        }

        private void toolStripButton32_Click(object sender, EventArgs e)
        {
            //LY_Query_week queryForm = new LY_Query_week();

            //queryForm.OwnerForm  = this;


            //queryForm.StartPosition = FormStartPosition.CenterParent;
            //queryForm.ShowDialog(this);

        }

        private void ly_sales_contract_standard_ReportDataGridView_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            DataGridView dgv = sender as DataGridView;

            if (null == dgv.CurrentRow) return;

            /////////////////////////////
            if ("税务" == dgv.CurrentCell.OwningColumn.Name)
            {


                if (e.Button == MouseButtons.Right)
                {
                    //MessageBox.Show(dgv.CurrentCell.Value.ToString()); "(" + filterString + ") or 清单号='合计'";
                    this.ly_sales_standard_Report_financialBindingSource.Filter = " (税务 = '" + dgv.CurrentCell.Value.ToString() + "') or 清单号='合计'";
                    AddSummationRow_New(ly_sales_standard_Report_financialBindingSource, ly_sales_contract_standard_ReportDataGridView);
               
              
                }
                //else
                //{
                //    MessageBox.Show(dgv.CurrentCell.Value.ToString());

                //}
            }

            if ("清单号" == dgv.CurrentCell.OwningColumn.Name)
            {


                if (e.Button == MouseButtons.Right)
                {
                    //MessageBox.Show(dgv.CurrentCell.Value.ToString());


                    this.ly_sales_standard_Report_financialBindingSource.Filter = " (substring(清单号,1,2) = '" + dgv.CurrentCell.Value.ToString().Substring(0, 2) + "') or 清单号='合计'";
                    AddSummationRow_New(ly_sales_standard_Report_financialBindingSource, ly_sales_contract_standard_ReportDataGridView);
                
                
                }
                //else
                //{
                //    MessageBox.Show(dgv.CurrentCell.Value.ToString());

                //}借用
            }

            if ("内容" == dgv.CurrentCell.OwningColumn.Name)
            {
               

                if (e.Button == MouseButtons.Right)
                {
                    //MessageBox.Show(dgv.CurrentCell.Value.ToString());
                    string nowremark = dgv.CurrentCell.Value.ToString();

                    if ("维修" == dgv.CurrentCell.Value.ToString().Substring(0, 2))
                    {

                        this.ly_sales_standard_Report_financialBindingSource.Filter = " (内容= '" + dgv.CurrentCell.Value.ToString() + "') or 清单号='合计'";
                        AddSummationRow_New(ly_sales_standard_Report_financialBindingSource, ly_sales_contract_standard_ReportDataGridView);
                   
                    
                    
                      
                    }
                    else
                    {
                        this.ly_sales_standard_Report_financialBindingSource.Filter = " (substring(清单号,1,2) = 'XS') or 清单号='合计'";
                        AddSummationRow_New(ly_sales_standard_Report_financialBindingSource, ly_sales_contract_standard_ReportDataGridView);
                   
                    
                    }
                }
                //else
                //{
                //    MessageBox.Show(dgv.CurrentCell.Value.ToString());

                //}借用
            }

            
        }

        private void toolStripButton38_Click(object sender, EventArgs e)
        {
           // this.ly_sales_standard_SumTableAdapter.Fill(this.lYSalseRepair.ly_sales_standard_Sum, this.nowusercode, this.nowfillstragecode, this.dateTimePicker5.Value, this.dateTimePicker6.Value.AddDays(1));

            this.ly_sales_standard_SumMarpossTableAdapter.Fill(this.lYSalseRepair.ly_sales_standard_SumMarposs, this.nowusercode, this.nowfillstragecode, this.dateTimePicker9.Value, this.dateTimePicker10.Value.AddDays(1));

            SetDGVFir(this.ly_sales_standard_SumMarpossDataGridView); 
        }

        private void toolStripButton37_Click(object sender, EventArgs e)
        {
            ExportDataGridviewTOExcell.ExportDataGridview(this.ly_sales_standard_SumMarpossDataGridView, true);
        }

        private void toolStripButton39_Click(object sender, EventArgs e)
        {
            LY_Marposs_StyleSet queryForm = new LY_Marposs_StyleSet();


            queryForm.StartPosition = FormStartPosition.CenterParent;
            queryForm.ShowDialog();
        }

        private void ly_sales_standard_SumDataGridView_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            DataGridView dgv = sender as DataGridView;

            if (null == dgv.CurrentRow) return;

            /////////////////////////////
            if ("成品编号" == dgv.CurrentCell.OwningColumn.Name)
            {

                string nowitemno = dgv.CurrentCell.Value.ToString();

                string sel = "SELECT  style_code as 类码,name_english as 英文名称,name_chinese as 中文名称 FROM ly_material_marposs_style order by style_code";
               

                QueryForm queryForm = new QueryForm();


                queryForm.Sel = sel;
                queryForm.Constr = SQLDatabase.Connectstring;

                //Set the Column Collection to the filter Table
                //queryForm.SetSourceColumns(this.billMainDataSet.BalanceBill.Columns);

                queryForm.ShowDialog();

                if (string.IsNullOrEmpty(queryForm.Result))
                {
                    return;
                }

                ///////////////////////

                string updstr = " update ly_inma0010  " +
                               "  set marposs_style=  '" + queryForm.Result + "' where  wzbh='" + nowitemno+"'";


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

                /////////////////////////

                this.ly_sales_standard_SumTableAdapter.Fill(this.lYSalseRepair.ly_sales_standard_Sum, this.nowusercode, this.nowfillstragecode, this.dateTimePicker5.Value, this.dateTimePicker6.Value.AddDays(1));

               

                SetDGVFir(this.ly_sales_standard_SumDataGridView);

                this.ly_sales_standard_SumBindingSource.Position = this.ly_sales_standard_SumBindingSource.Find("成品编号", nowitemno);

                return;

            }
        }

        //private void fillToolStripButton_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        this.ly_sales_standard_SumMarpossNewTableAdapter.Fill(this.lYSalseRepair.ly_sales_standard_SumMarpossNew, salesperson_codeToolStripTextBox.Text, selcodeToolStripTextBox.Text, new System.Nullable<System.DateTime>(((System.DateTime)(System.Convert.ChangeType(begindateToolStripTextBox.Text, typeof(System.DateTime))))), new System.Nullable<System.DateTime>(((System.DateTime)(System.Convert.ChangeType(enddateToolStripTextBox.Text, typeof(System.DateTime))))));
        //    }
        //    catch (System.Exception ex)
        //    {
        //        System.Windows.Forms.MessageBox.Show(ex.Message);
        //    }

        //}

        private void toolStripButton46_Click(object sender, EventArgs e)
        {
            // this.ly_sales_standard_SumTableAdapter.Fill(this.lYSalseRepair.ly_sales_standard_Sum, this.nowusercode, this.nowfillstragecode, this.dateTimePicker5.Value, this.dateTimePicker6.Value.AddDays(1));
            NewFrm.Show(this);
            
            this.ly_sales_standard_SumMarpossNewTableAdapter.Fill(this.lYSalseRepair.ly_sales_standard_SumMarpossNew, this.nowusercode, this.nowfillstragecode, this.dateTimePicker11.Value, this.dateTimePicker12.Value.AddDays(1));

            SetDGVFir(this.ly_sales_standard_SumMarpossNewDataGridView);
            NewFrm.Hide(this);
        }

        private void toolStripButton45_Click(object sender, EventArgs e)
        {
            ExportDataGridviewTOExcell.ExportDataGridview(this.ly_sales_standard_SumMarpossNewDataGridView, true);
        }

        private void toolStripButton53_Click(object sender, EventArgs e)
        {
            NewFrm.Show(this);

            this.ly_sales_standard_SumMarpossNew2TableAdapter.Fill(this.lYSalseRepair.ly_sales_standard_SumMarpossNew2, this.nowusercode, this.nowfillstragecode, this.dateTimePicker13.Value, this.dateTimePicker14.Value.AddDays(1));

            SetDGVFir(this.ly_sales_standard_SumMarpossNew2DataGridView);
            NewFrm.Hide(this);
        }

        private void toolStripButton52_Click(object sender, EventArgs e)
        {
            ExportDataGridviewTOExcell.ExportDataGridview(this.ly_sales_standard_SumMarpossNew2DataGridView, true);
        }

       

      
       
       
       

       
    }
}
