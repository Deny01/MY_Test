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
    public partial class LY_SalseBorrow_Mange : Form
    {
        private string nowfilterStr = "";
        private string nowusercode = "";
        private string nowfillstragecode = "";

        private string nowclientCode = "";
        private string nowborrowcode = "";
        private string borrowCanchenged = "";

        private string isborrow= "";

        public LY_SalseBorrow_Mange()
        {
            InitializeComponent();
            this.ly_lsptb_selTableAdapter.CommandTimeout = 0;
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

            

            this.ly_lsptb_selTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            this.ly_lsptb_selTableAdapter.Fill(this.lYSalseMange.ly_lsptb_sel);

            this.nowusercode = SQLDatabase.NowUserID;

            this.ly_sales_borrow_detailTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;

            this.ly_sales_borrow_detail_allTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            this.ly_sales_borrow_detail_sumTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            
            this.ly_sales_borrow_detail_clientTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            this.ly_sales_borrow_detail_clientSumTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            
           
            this.ly_sales_borrow_clientTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
          
            this.dateTimePicker1.Text = DateTime.Today.AddMonths(-12).Date.ToString();
            this.dateTimePicker2.Text = DateTime.Today.AddDays(1).Date.ToString();

            this.ly_sales_clientTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            this.ly_sales_clientTableAdapter.Fill(this.lYSalseMange.ly_sales_client);

            string selAllString;

            //if (SQLDatabase.CheckHaveRight(SQLDatabase.NowUserID, "营业综合信息"))
            //{

            //    selAllString = "SELECT  a.salesregion_code, a.salesregion_code+':'+a.salesregion_name as salesregion_name,b.yhbm,b.yhbm+':'+b.yhmc as yhmc FROM  ly_salesregion a left join T_users b on a.salesregion_code=b.salesregion_code ORDER BY  salesregion_code ";
            //}
            //else if (SQLDatabase.CheckHaveRight(SQLDatabase.NowUserID, "营业区域信息"))
            //{
              
            //    selAllString = "SELECT  a.salesregion_code, a.salesregion_code+':'+a.salesregion_name as salesregion_name,b.yhbm,b.yhbm+':'+b.yhmc as yhmc FROM  ly_salesregion a left join T_users b on a.salesregion_code=b.salesregion_code  where b.salesregion_code='" + SQLDatabase.nowSalesregioncode () + "' ORDER BY  salesregion_code ";
            //}
            //else
            //{
            //    selAllString = "SELECT  a.salesregion_code, a.salesregion_code+':'+a.salesregion_name as salesregion_name,b.yhbm,b.yhbm+':'+b.yhmc as yhmc FROM  ly_salesregion a left join T_users b on a.salesregion_code=b.salesregion_code  where b.yhbm='" + SQLDatabase.NowUserID + "' ORDER BY  salesregion_code ";
            //}

            if (SQLDatabase.CheckHaveRight(SQLDatabase.NowUserID, "营业综合信息"))
            {

                selAllString = "SELECT  a.salesregion_code, a.salesregion_code+':'+a.salesregion_name as salesregion_name,b.yhbm,b.yhbm+':'+b.yhmc as yhmc FROM  ly_salesregion a left join T_users b on a.salesregion_code=b.salesregion_code ORDER BY  salesregion_code ";
                selAllString = "SELECT distinct  a.salesregion_code, a.salesregion_code+':'+a.salesregion_name as salesregion_name,a.salesperson_code as yhbm,a.salesperson_code+':'+a.salesperson_name as yhmc FROM  ly_sales_client_forborrow a ORDER BY  salesregion_code ";
            }
            else if (SQLDatabase.CheckHaveRight(SQLDatabase.NowUserID, "营业区域信息"))
            {

                selAllString = "SELECT  a.salesregion_code, a.salesregion_code+':'+a.salesregion_name as salesregion_name,b.yhbm,b.yhbm+':'+b.yhmc as yhmc FROM  ly_salesregion a left join T_users b on a.salesregion_code=b.salesregion_code  where b.salesregion_code='" + SQLDatabase.nowSalesregioncode() + "' ORDER BY  salesregion_code ";
                selAllString = "SELECT distinct  a.salesregion_code, a.salesregion_code+':'+a.salesregion_name as salesregion_name,a.salesperson_code as yhbm,a.salesperson_code+':'+a.salesperson_name as yhmc FROM  ly_sales_client_forborrow a  where a.salesregion_code='" + SQLDatabase.nowSalesregioncode() + "' ORDER BY  salesregion_code ";
            }
            else
            {
                selAllString = "SELECT  a.salesregion_code, a.salesregion_code+':'+a.salesregion_name as salesregion_name,b.yhbm,b.yhbm+':'+b.yhmc as yhmc FROM  ly_salesregion a left join T_users b on a.salesregion_code=b.salesregion_code  where b.yhbm='" + SQLDatabase.NowUserID + "' ORDER BY  salesregion_code ";
                selAllString = "SELECT distinct  a.salesregion_code, a.salesregion_code+':'+a.salesregion_name as salesregion_name,a.salesperson_code as yhbm,a.salesperson_code+':'+a.salesperson_name as yhmc FROM  ly_sales_client_forborrow a where a.salesperson_code='" + SQLDatabase.NowUserID + "' ORDER BY  salesregion_code "; ;
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

                this.nowfilterStr = " in_use=0";
                this.ly_sales_clientBindingSource.Filter = this.nowfilterStr;
                this.splitContainer1.Panel1Collapsed = false;
            }
            else if (SQLDatabase.CheckHaveRight(SQLDatabase.NowUserID, "营业区域信息"))
            {
                //this.treeView1.Visible = true;
                this.splitContainer1.Panel1Collapsed = false;
                //this.nowfilterStr = "salesperson_code='" + SQLDatabase.NowUserID + "'";
                this.nowfilterStr = "salesregion_code='" + SQLDatabase.nowSalesregioncode() + "' and in_use=0";
            }
            else
            {
                //this.treeView1.Visible = false;
                this.splitContainer1.Panel1Collapsed = true;
                this.nowfilterStr = "salesperson_code='" + SQLDatabase.NowUserID + "' and in_use=0";
                this.ly_sales_clientBindingSource.Filter = this.nowfilterStr;
                
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
            if ("" == e.Node.Tag.ToString())
            {
                this.nowfilterStr =  " in_use=0 ";
            }
            else
            {
                this.nowfilterStr = "(" + e.Node.Tag.ToString() + ") and in_use=0 ";
            }
            
           

            this.ly_sales_clientBindingSource.Filter = this.nowfilterStr;
            

            if (e.Node.Level == 2)
            {
                this.ly_sales_clientDataGridView.Columns["yhbm"].Visible = false;
                this.ly_sales_clientDataGridView.Columns["yhmc"].Visible = false;

                this.nowusercode = this.nowfilterStr.Substring(this.nowfilterStr.Length - 4, 3);
                this.nowfillstragecode = "single";

                
                //this.ly_sales_contract_main1DataGridView.SelectionChanged -= ly_sales_contract_main1DataGridView_SelectionChanged;
                //this.ly_sales_contract_main1TableAdapter.Fill(this.lYSalseMange.ly_sales_contract_main1, this.nowusercode,"single", this.dateTimePicker1.Value , this.dateTimePicker2.Value );
                //AddSummationRow_New(ly_sales_contract_main1BindingSource, ly_sales_contract_main1DataGridView);
                //this.ly_sales_contract_main1DataGridView.SelectionChanged += ly_sales_contract_main1DataGridView_SelectionChanged;
               
            }
            else if (e.Node.Level == 1)
            {
                this.ly_sales_clientDataGridView.Columns["yhbm"].Visible = true ;
                this.ly_sales_clientDataGridView.Columns["yhmc"].Visible = true ;

                this.nowusercode = this.nowfilterStr.Substring(this.nowfilterStr.Length - 3, 2);
                this.nowfillstragecode = "region";

                //this.ly_sales_contract_main1DataGridView.SelectionChanged -= ly_sales_contract_main1DataGridView_SelectionChanged;
                //this.ly_sales_contract_main1TableAdapter.Fill(this.lYSalseMange.ly_sales_contract_main1, this.nowusercode, "region", this.dateTimePicker1.Value, this.dateTimePicker2.Value);
                //AddSummationRow_New(ly_sales_contract_main1BindingSource, ly_sales_contract_main1DataGridView);
                //this.ly_sales_contract_main1DataGridView.SelectionChanged += ly_sales_contract_main1DataGridView_SelectionChanged;
            }
            else if (e.Node.Level == 0)
            {
                this.ly_sales_clientDataGridView.Columns["yhbm"].Visible = true;
                this.ly_sales_clientDataGridView.Columns["yhmc"].Visible = true;

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

            this.ly_sales_borrow_detail_allTableAdapter.Fill(this.lYSalseMange.ly_sales_borrow_detail_all, this.nowusercode, this.nowfillstragecode, this.dateTimePicker1.Value, this.dateTimePicker2.Value);
            this.ly_sales_borrow_detail_sumTableAdapter.Fill(this.lYSalseMange.ly_sales_borrow_detail_sum, this.nowusercode, this.nowfillstragecode, this.dateTimePicker1.Value, this.dateTimePicker2.Value);



            this.groupBox2.Text = e.Node.Text + "客户信息列表";
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

     

        private void ly_sales_clientBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.ly_sales_clientBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.lYSalseMange);

        }

        private void toolStripTextBox1_KeyUp(object sender, KeyEventArgs e)
        {
            string filterString;

          //  if ("" == this.treeView1.SelectedNode.Tag.ToString())
            if ("" == this.nowfilterStr)
            {
                filterString = GetDataGridviewMultiFilterString.DGVMultiFilterString(this.ly_sales_clientDataGridView, this.toolStripTextBox1.Text);

            }
            else
            {

                filterString = this.nowfilterStr + " and (" + GetDataGridviewMultiFilterString.DGVMultiFilterString(this.ly_sales_clientDataGridView, this.toolStripTextBox1.Text) + ")"; 
            }

            if (null == filterString)
                filterString = this.treeView1.SelectedNode.Tag.ToString(); 

            this.ly_sales_clientBindingSource.Filter = filterString;
        }

        private void toolStripTextBox1_Enter(object sender, EventArgs e)
        {
            toolStripTextBox1.Text = "";

            //this.ly_sales_clientBindingSource.Filter = this.treeView1.SelectedNode.Tag.ToString();
            this.ly_sales_clientBindingSource.Filter = this.nowfilterStr;
        }

        private void bindingNavigatorAddNewItem_Click_1(object sender, EventArgs e)
        {
            LY_SalesclientAdd queryForm = new LY_SalesclientAdd();

            queryForm.salesclient_code  = "";
            queryForm.runmode = "增加";
           

            queryForm.StartPosition = FormStartPosition.CenterParent;
            queryForm.ShowDialog();

            if (queryForm.DialogResult != DialogResult.Cancel)
            {
                this.ly_sales_clientTableAdapter.Fill(this.lYSalseMange.ly_sales_client);

                this.treeView1.SelectedNode = FindNode(this.treeView1.Nodes, queryForm.salesperson_code);

                this.ly_sales_clientBindingSource.Position = this.ly_sales_clientBindingSource.Find("客户编码", queryForm .salesclient_code);

             
            
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


        private void ly_sales_clientDataGridView_DoubleClick(object sender, EventArgs e)
        {
            if (null == ly_sales_clientDataGridView.CurrentRow) return;
            string s = this.ly_sales_clientDataGridView.CurrentRow.Cells["客户编码"].Value.ToString();
            //int loanid = int.Parse(this.xD_Client_LoanDataGridView.CurrentRow.Cells["Id_loan"].Value.ToString());

            LY_SalesclientAdd queryForm = new LY_SalesclientAdd();


            queryForm.runmode = "修改";
            queryForm.salesclient_code = s;


            queryForm.StartPosition = FormStartPosition.CenterParent;
            queryForm.ShowDialog();

            if (queryForm.DialogResult != DialogResult.Cancel)
            {
                this.ly_sales_clientTableAdapter.Fill(this.lYSalseMange.ly_sales_client);

                this.treeView1.SelectedNode = FindNode(this.treeView1.Nodes, queryForm.salesperson_code);

                this.ly_sales_clientBindingSource.Position = this.ly_sales_clientBindingSource.Find("客户编码", s);


            }
        }

        private void toolStripButton1_Click_1(object sender, EventArgs e)
        {
            if (null == ly_sales_clientDataGridView.CurrentRow) return;
            string s = this.ly_sales_clientDataGridView.CurrentRow.Cells["客户编码"].Value.ToString();
            //int loanid = int.Parse(this.xD_Client_LoanDataGridView.CurrentRow.Cells["Id_loan"].Value.ToString());

            LY_SalesclientAdd queryForm = new LY_SalesclientAdd();

          
            queryForm.runmode = "修改";
            queryForm.salesclient_code  = s;
           

            queryForm.StartPosition = FormStartPosition.CenterParent;
            queryForm.ShowDialog();

            if (queryForm.DialogResult != DialogResult.Cancel)
            {
                this.ly_sales_clientTableAdapter.Fill(this.lYSalseMange.ly_sales_client);

                this.treeView1.SelectedNode = FindNode(this.treeView1.Nodes, queryForm.salesperson_code);
                this.ly_sales_clientBindingSource.Position = this.ly_sales_clientBindingSource.Find("客户编码", s);


            }

        }

        private void bindingNavigatorDeleteItem_Click(object sender, EventArgs e)
        {
            string message = "确定删除当前客户记录吗？";
            string caption = "提示...";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result;



            result = MessageBox.Show(message, caption, buttons,
            MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

            if (result == DialogResult.Yes)
            {

                this.ly_sales_clientBindingSource.RemoveCurrent();


                ly_sales_clientDataGridView.EndEdit();
                ly_sales_clientBindingSource.EndEdit();


                this.ly_sales_clientTableAdapter.Update(this.lYSalseMange.ly_sales_client);
            }
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            ExportDataGridviewTOExcell.ExportDataGridview(this.ly_sales_clientDataGridView, true);
        }

        private void ly_sales_clientDataGridView_SelectionChanged(object sender, EventArgs e)
        {
            if (null == ly_sales_clientDataGridView.CurrentRow) 
            {
               
                this.ly_sales_borrow_clientTableAdapter.Fill(this.lYSalseMange.ly_sales_borrow_client, "aaa");
                return;
            }

            this.nowclientCode = this.ly_sales_clientDataGridView.CurrentRow.Cells["客户编码"].Value.ToString();
            string nowclientname = this.ly_sales_clientDataGridView.CurrentRow.Cells["客户名称"].Value.ToString();

            this.groupBox1.Text = nowclientname + "借用单据列表";

            this.ly_sales_borrow_detail_clientTableAdapter.Fill(this.lYSalseMange.ly_sales_borrow_detail_client, nowclientCode,"all");
            this.ly_sales_borrow_detail_clientSumTableAdapter.Fill(this.lYSalseMange.ly_sales_borrow_detail_clientSum, nowclientCode);

            this.ly_sales_borrow_clientTableAdapter.Fill(this.lYSalseMange.ly_sales_borrow_client, nowclientCode);
            if (null == this.ly_sales_borrowDataGridView.CurrentRow)
            {

                this.ly_sales_borrow_detailTableAdapter.Fill(this.lYSalseMange.ly_sales_borrow_detail, "-111");
            }
           


        }

        private void toolStripButton8_Click(object sender, EventArgs e)
        {
            if (null == ly_sales_clientDataGridView.CurrentRow)
            {
               // this.ly_sales_contract_mainTableAdapter.Fill(this.lYSalseMange.ly_sales_contract_main, "");

                return;
            }

            

            string nowcontractcode =null ;
           //string nowclientcode = this.ly_sales_businessDataGridView.CurrentRow.Cells["客户编码"].Value.ToString();

            LY_Salesborror_Add queryForm = new LY_Salesborror_Add();

            queryForm.contract_code = nowcontractcode;
            queryForm.salesclientcode = this.nowclientCode;
            queryForm.borrowstyle = "02";
            queryForm.runmode = "增加";


            queryForm.StartPosition = FormStartPosition.CenterParent;
            queryForm.ShowDialog();

            if (queryForm.DialogResult != DialogResult.Cancel)
            {
                this.ly_sales_borrow_clientTableAdapter.Fill(this.lYSalseMange.ly_sales_borrow_client, this.nowclientCode );
                
                this.ly_sales_borrow_clientBindingSource.Position = this.ly_sales_borrow_clientBindingSource.Find("借用单号", queryForm.borrowcode);

               
            }
        }

        private void toolStripButton9_Click(object sender, EventArgs e)
        {
            if (null == ly_sales_borrowDataGridView.CurrentRow)
            {

                //this.ly_sales_contract_main_forbusinessTableAdapter.Fill(this.lYSalseMange.ly_sales_contract_main_forbusiness, "");

                return;
            }

            if ("True" == ly_sales_borrowDataGridView.CurrentRow.Cells["生产审批j"].Value.ToString())
            {
                MessageBox.Show("借用已经生产审批,不能修改数据...", "注意");
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

            if ("02" != ly_sales_borrowDataGridView.CurrentRow.Cells["借用类别j"].Value.ToString())
            {
                MessageBox.Show("非个人借用,不能在这里修改数据...", "注意");
                return;

            }


            string nowcontractcode = null ;
            //string nowclientcode = this.ly_sales_businessDataGridView.CurrentRow.Cells["客户编码"].Value.ToString();

            LY_Salesborror_Add queryForm = new LY_Salesborror_Add();

            queryForm.contract_code = nowcontractcode;
            queryForm.salesclientcode = this .nowclientCode ;
            queryForm.borrowcode = this.nowborrowcode ;
            queryForm.borrowstyle = "02";
            queryForm.runmode = "修改";


            queryForm.StartPosition = FormStartPosition.CenterParent;
            queryForm.ShowDialog();

            if (queryForm.DialogResult != DialogResult.Cancel)
            {
                this.ly_sales_borrow_clientTableAdapter.Fill(this.lYSalseMange.ly_sales_borrow_client, this.nowclientCode);

                this.ly_sales_borrow_clientBindingSource.Position = this.ly_sales_borrow_clientBindingSource.Find("借用单号", queryForm.borrowcode);
               
            }
        }

        private void toolStripButton10_Click(object sender, EventArgs e)
        {

            if (null == ly_sales_borrowDataGridView.CurrentRow)
            {

                //this.ly_sales_contract_main_forbusinessTableAdapter.Fill(this.lYSalseMange.ly_sales_contract_main_forbusiness, "");生产审批j

                return;
            }

            if ("True" == ly_sales_borrowDataGridView.CurrentRow.Cells["生产审批j"].Value.ToString())
            {
                MessageBox.Show("借用已经生产审批,不能删除数据...", "注意");
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

            if ("02" != ly_sales_borrowDataGridView.CurrentRow.Cells["借用类别j"].Value.ToString())
            {
                MessageBox.Show("非个人借用,不能在这里删除...", "注意");
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

                this.ly_sales_borrow_clientBindingSource.RemoveCurrent();


                ly_sales_borrowDataGridView.EndEdit();
                ly_sales_borrow_clientBindingSource.EndEdit();



                this.ly_sales_borrow_clientTableAdapter.Update(this.lYSalseMange.ly_sales_borrow_client);

              
            }
        }

        private void toolStripTextBox3_KeyUp(object sender, KeyEventArgs e)
        {
            //string filterString;


            //filterString = GetDataGridviewMultiFilterString.DGVMultiFilterString(this.ly_sales_contract_mainDataGridView, this.toolStripTextBox3.Text);


            //this.ly_sales_contract_mainBindingSource.Filter = filterString;
        }

        private void toolStripTextBox3_Enter(object sender, EventArgs e)
        {
            //toolStripTextBox3.Text = "";

            //this.ly_sales_contract_mainBindingSource.Filter ="";
        }

        private void SaveContract()
        {
            //this.ly_sales_contract_mainDataGridView.CurrentRow.Cells["录入人"].Value = SQLDatabase.nowUserName();
            
            //this.ly_sales_contract_mainDataGridView.EndEdit();
            //this.ly_sales_contract_mainBindingSource.EndEdit();

            //this.ly_sales_contract_mainTableAdapter.Update(this.lYSalseMange.ly_sales_contract_main);



            //RefreshData();
        }

        private void RefreshData()
        {









            //int nowcontractId;
            //if (null != ly_sales_contract_mainDataGridView.CurrentRow)
            //{
            //    nowcontractId = int.Parse(ly_sales_contract_mainDataGridView.CurrentRow.Cells["id_main"].Value.ToString());
            //}
            //else
            //{
            //    nowcontractId = 0;
            //}

            ////int nowcontractdetailId;
            ////if (null != ly_purchase_contract_detailDataGridView.CurrentRow)
            ////{
            ////    nowcontractdetailId = int.Parse(ly_purchase_contract_detailDataGridView.CurrentRow.Cells["id7"].Value.ToString());
            ////}
            ////else
            ////{
            ////    nowcontractdetailId = 0;
            ////}



            //int  nowcontractdetailid;
            //if (null != ly_sales_contract_detailDataGridView.CurrentRow)
            //{
            //    nowcontractdetailid = int.Parse(ly_sales_contract_detailDataGridView.CurrentRow.Cells["id_detail"].Value.ToString());
            //}
            //else
            //{
            //    nowcontractdetailid = 0;
            //}


            //string nowclientcode = this.ly_sales_contract_mainDataGridView.CurrentRow.Cells["客户编码c"].Value.ToString();




            //this.ly_sales_contract_mainTableAdapter.Fill(this.lYSalseMange.ly_sales_contract_main, nowclientcode);
            //this.ly_sales_contract_mainBindingSource.Position = this.ly_sales_contract_mainBindingSource.Find("id", nowcontractId);

            //this.ly_sales_contract_main1DataGridView.SelectionChanged -= ly_sales_contract_main1DataGridView_SelectionChanged;
            //this.ly_sales_contract_main1TableAdapter.Fill(this.lYSalseMange.ly_sales_contract_main1, this.nowusercode, "single", this.dateTimePicker1.Value, this.dateTimePicker2.Value);
            //this.ly_sales_contract_main1DataGridView.SelectionChanged += ly_sales_contract_main1DataGridView_SelectionChanged;

            //this.ly_sales_contract_detailBindingSource.Position = this.ly_sales_contract_detailBindingSource.Find("id", nowcontractdetailid);
            //this.ly_purchase_contract_detailBindingSource.Position = this.ly_purchase_contract_detailBindingSource.Find("物料编号", nowcontractdetailcode);

        }

        
        private void 修改当前合同ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //if (null == ly_sales_contract_mainDataGridView.CurrentRow)
            //{


            //    return;
            //}

            //if ("True" == ly_sales_contract_mainDataGridView.CurrentRow.Cells["提交"].Value.ToString())
            //{
            //    MessageBox.Show("合同已经提交,不能修改数据...", "注意");
            //    return;

            //}

            //if ("True" == ly_sales_contract_mainDataGridView.CurrentRow.Cells["批准"].Value.ToString())
            //{
            //    MessageBox.Show("合同已经审批,不能修改数据...", "注意");
            //    return;

            //}

            //if ("True" == ly_sales_contract_mainDataGridView.CurrentRow.Cells["审核"].Value.ToString())
            //{
            //    MessageBox.Show("合同已经审批,不能修改数据...", "注意");
            //    return;

            //}

            //string nowclientcode = this.ly_sales_clientDataGridView.CurrentRow.Cells["客户编码"].Value.ToString();
            //string nowinnercode = this.ly_sales_contract_mainDataGridView.CurrentRow.Cells["内部编码"].Value.ToString();

            //LY_Salescontract_Add queryForm = new LY_Salescontract_Add();

            //queryForm.salesclientcode = nowclientcode;
            //queryForm.contractinner_code = nowinnercode;
            //queryForm.runmode = "修改";


            //queryForm.StartPosition = FormStartPosition.CenterParent;
            //queryForm.ShowDialog();

            //if (queryForm.DialogResult != DialogResult.Cancel)
            //{
            //    this.ly_sales_contract_mainTableAdapter.Fill(this.lYSalseMange.ly_sales_contract_main, nowclientcode);



            //    this.ly_sales_contract_mainBindingSource.Position = this.ly_sales_contract_mainBindingSource.Find("内部编码", queryForm.contractinner_code);
            //}
        }

        private void 删除当前合同ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //if (null == ly_sales_contract_mainDataGridView.CurrentRow)
            //{


            //    return;
            //}

            //if ("True" == ly_sales_contract_mainDataGridView.CurrentRow.Cells["提交"].Value.ToString())
            //{
            //    MessageBox.Show("合同已经提交,不能删除数据...", "注意");
            //    return;

            //}

            //if ("True" == ly_sales_contract_mainDataGridView.CurrentRow.Cells["批准"].Value.ToString())
            //{
            //    MessageBox.Show("合同已经审批,不能删除数据...", "注意");
            //    return;

            //}

            //if ("True" == ly_sales_contract_mainDataGridView.CurrentRow.Cells["审核"].Value.ToString())
            //{
            //    MessageBox.Show("合同已经审批,不能删除数据...", "注意");
            //    return;

            //}


            //string message = "确定删除当前合同记录吗？";
            //string caption = "提示...";
            //MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            //DialogResult result;



            //result = MessageBox.Show(message, caption, buttons,
            //MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

            //if (result == DialogResult.Yes)
            //{

            //    this.ly_sales_contract_mainBindingSource.RemoveCurrent();


            //    ly_sales_contract_mainDataGridView.EndEdit();
            //    ly_sales_contract_mainBindingSource.EndEdit();



            //    this.ly_sales_contract_mainTableAdapter.Update(this.lYSalseMange.ly_sales_contract_main);
            //}
        }

        private void 同步合同客户信息ToolStripMenuItem_Click(object sender, EventArgs e)
        {

            //if (null == ly_sales_contract_mainDataGridView.CurrentRow)
            //{


            //    return;
            //}

            //if ("True" == ly_sales_contract_mainDataGridView.CurrentRow.Cells["提交"].Value.ToString())
            //{
            //    MessageBox.Show("合同已经提交,不能同步合同客户数据...", "注意");
            //    return;

            //}

            //if ("True" == ly_sales_contract_mainDataGridView.CurrentRow.Cells["批准"].Value.ToString())
            //{
            //    MessageBox.Show("合同已经审批,不能同步合同客户数据...", "注意");
            //    return;

            //}

            //if ("True" == ly_sales_contract_mainDataGridView.CurrentRow.Cells["审核"].Value.ToString())
            //{
            //    MessageBox.Show("合同已经审批,不能同步合同客户数据...", "注意");
            //    return;

            //}

            //string nowclientcode = this.ly_sales_clientDataGridView.CurrentRow.Cells["客户编码"].Value.ToString();
            //string nowinnercode = this.ly_sales_contract_mainDataGridView.CurrentRow.Cells["内部编码"].Value.ToString();

           
               
            
            //SqlConnection sqlConnection1 = new SqlConnection(SQLDatabase.Connectstring);
            //SqlCommand cmd = new SqlCommand();



            //cmd.Parameters.Add("@contract_inner_code", SqlDbType.VarChar);
            //cmd.Parameters["@contract_inner_code"].Value = nowinnercode;

            ////cmd.Parameters.Add("@loanId", SqlDbType.Int);
            ////cmd.Parameters["@loanId"].Value = loanId;

            ////cmd.Parameters.Add("@item_name", SqlDbType.VarChar);
            ////cmd.Parameters["@item_name"].Value = item_name;


            //cmd.CommandText = "LY_syn_salse_contractClient";
            //cmd.CommandType = CommandType.StoredProcedure;
            //cmd.Connection = sqlConnection1;

            //sqlConnection1.Open();
            // cmd.ExecuteNonQuery();
            //sqlConnection1.Close();

            //this.ly_sales_contract_mainTableAdapter.Fill(this.lYSalseMange.ly_sales_contract_main, nowclientcode);

            //this.ly_sales_contract_mainBindingSource.Position = this.ly_sales_contract_mainBindingSource.Find("内部编码", nowinnercode);
            
            
        }

       

        private void toolStripButton21_Click(object sender, EventArgs e)
        {

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

        private void ly_lsptb_selDataGridView_DoubleClick(object sender, EventArgs e)
        {
            if (null == this.ly_sales_borrowDataGridView.CurrentRow) return;
            if (null == ly_lsptb_selDataGridView.CurrentRow) return;

            if ("False" == this.borrowCanchenged)
            {
                MessageBox.Show("借用单已经提交执行,不能修改数据...", "注意");
                return;

            }

            if ("02" != ly_sales_borrowDataGridView.CurrentRow.Cells["借用类别j"].Value.ToString())
            {
                MessageBox.Show("非个人借用,不能在这里操作数据...", "注意");
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

          

            this.ly_sales_borrow_detailTableAdapter.Fill(this.lYSalseMange.ly_sales_borrow_detail, this.nowborrowcode);

            this.ly_sales_borrow_detailBindingSource.Position = this.ly_sales_borrow_detailBindingSource.Find("产品编码", nowitemno);
        }

        private static void InserSalseContractDetail(string innerCode, string nowitem, decimal nowabsqty,string isborrow)
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

      

     

        private void bindingNavigatorDeleteItem2_Click(object sender, EventArgs e)
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

            if ("02" != ly_sales_borrowDataGridView.CurrentRow.Cells["借用类别j"].Value.ToString())
            {
                MessageBox.Show("非个人借用,不能在这里删除数据...", "注意");
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

     

        private void 删除当前记录ToolStripMenuItem_Click(object sender, EventArgs e)
        {
           // DeleteDetail();
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
                            if ("合同天数" != dgvCell.OwningColumn.HeaderText && "到期天数" != dgvCell.OwningColumn.HeaderText && "折扣利率" != dgvCell.OwningColumn.HeaderText )
                            {
                                if (sumdr[dgvCell.OwningColumn.DataPropertyName] == null || (sumdr[dgvCell.OwningColumn.DataPropertyName] is DBNull))
                                    sumdr[dgvCell.OwningColumn.DataPropertyName] = 0;


                                sumdr[dgvCell.OwningColumn.DataPropertyName] = Convert.ToInt64(sumdr[dgvCell.OwningColumn.DataPropertyName]) + Convert.ToInt64(dgvCell.Value);
                            }
                        }
                        else if (IsDecimal(dgvCell.Value))
                        {
                            if ("合同天数" != dgvCell.OwningColumn.HeaderText && "到期天数" != dgvCell.OwningColumn.HeaderText && "折扣利率" != dgvCell.OwningColumn.HeaderText )
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
            if (null == this.ly_sales_borrow_detailDataGridView.CurrentRow) return;

            //if ("" == ly_sales_contract_mainDataGridView.CurrentRow.Cells["类别"].Value.ToString())
            //{

            //    MessageBox.Show("请选择合同类别,然后打印...", "注意");
            //    return;
            //}

           // NewFrm.Show(this); ;

            //this.ly_plan_getmaterialTableAdapter.Fill(this.lYPlanMange.ly_plan_getmaterial, parentId);

            BaseReportView queryForm = new BaseReportView();

            queryForm.Text = "中原精密借用单";

            queryForm.Printdata = this.lYSalseMange;


            //if ("工矿" == ly_sales_contract_mainDataGridView.CurrentRow.Cells["类别"].Value.ToString())
            //{
            //    queryForm.PrintCrystalReport = new LY_YingyeHetong();
            //}
            //else
            //{

            //    queryForm.PrintCrystalReport = new LY_YingyeHetong_FHJY();
            //}

            queryForm.PrintCrystalReport = new LY_YingyeHetong_FHJY2();
            //string selectFormula;

            //selectFormula = "{ly_store_planitemcount.状态}  =   '原料'  and {ly_store_planitemcount.欠料金额}>0 ";
            //queryForm.PrintCrystalReport.DataDefinition.RecordSelectionFormula = selectFormula;

            //NewFrm.Hide(this);

            queryForm.ShowDialog();
        }

        private void toolStripTextBox5_KeyUp(object sender, KeyEventArgs e)
        {
            //string filterString;


            //filterString = GetDataGridviewMultiFilterString.DGVMultiFilterString(this.ly_sales_contract_main1DataGridView, this.toolStripTextBox5.Text);


            //this.ly_sales_contract_main1BindingSource.Filter = filterString;
        }

        private void toolStripTextBox5_Enter(object sender, EventArgs e)
        {
            //toolStripTextBox5.Text = "";

            //this.ly_sales_contract_main1BindingSource.Filter = "";
        }

        private void toolStripButton16_Click(object sender, EventArgs e)
        {

        }

        private void toolStripButton7_Click(object sender, EventArgs e)
        {
            ExportDataGridviewTOExcell.ExportDataGridview(this.ly_sales_borrowDataGridView, true);
        }

     

        private void toolStripButton28_Click(object sender, EventArgs e)
        {
           

            int nowclientId;
            if (null != ly_sales_clientDataGridView.CurrentRow)
            {
                nowclientId = int.Parse(ly_sales_clientDataGridView.CurrentRow.Cells["clientid"].Value.ToString());
            }
            else
            {
                nowclientId = 0;
            }


            
            this.ly_sales_clientDataGridView.SelectionChanged -= ly_sales_clientDataGridView_SelectionChanged;
            this.ly_sales_clientTableAdapter.Fill(this.lYSalseMange.ly_sales_client);
            this.ly_sales_clientBindingSource.Position = this.ly_sales_clientBindingSource.Find("id", nowclientId);
            this.ly_sales_clientDataGridView.SelectionChanged += ly_sales_clientDataGridView_SelectionChanged;
        }

        private void toolStripButton29_Click(object sender, EventArgs e)
        {
            this.ly_lsptb_selTableAdapter.Fill(this.lYSalseMange.ly_lsptb_sel);
        }

       

        private void ly_sales_borrowDataGridView_SelectionChanged(object sender, EventArgs e)
        {
            if (null == ly_sales_borrowDataGridView.CurrentRow)
            {
                
                return;
            }

            this.nowborrowcode = ly_sales_borrowDataGridView.CurrentRow.Cells["借用单号"].Value.ToString();
           


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

        private void ly_sales_borrow_detailDataGridView_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            DataGridView dgv = sender as DataGridView;

            if (null == dgv.CurrentRow) return;

            if ("False" == this.borrowCanchenged)
            {
                MessageBox.Show("借用单已经提交执行,不能修改数据...", "注意");
                return;

            }

            if ("02" != ly_sales_borrowDataGridView.CurrentRow.Cells["借用类别j"].Value.ToString())
            {
                MessageBox.Show("非个人借用,不能在这里修改数据...", "注意");
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


            if ("新旧" == dgv.CurrentCell.OwningColumn.Name)
            {

                //ChangeValue queryForm = new ChangeValue();

                //queryForm.OldValue = dgv.CurrentCell.Value.ToString();
                //queryForm.NewValue = "";
                //queryForm.ChangeMode = "string";
                //queryForm.ShowDialog();




                //if (queryForm.NewValue != "")
                //{
                //    dgv.CurrentRow.Cells["新旧"].Value = queryForm.NewValue;
                //    //dgv.CurrentRow.Cells["discount_money"].Value = DBNull.Value;

                //    //dgv.CurrentRow.Cells["approve_flag"].Value = "False";
                //    ly_sales_borrow_detailDataGridView.EndEdit();
                //    ly_sales_borrow_detailBindingSource.EndEdit();

                //    this.ly_sales_borrow_detailTableAdapter.Update(this.lYSalseMange.ly_sales_borrow_detail);

                //    //CountPlanStru();

                //}
                //else
                //{
                //    //hT_Manage_ItemDataGridView.CurrentRow.Cells["apply_money"].Value = queryForm.NewValue;
                //    //dgv.CurrentRow.Cells["discount_money"].Value = DBNull.Value;
                //    //dgv.CurrentRow.Cells["apply_money"].Value = DBNull.Value;
                //    //dgv.CurrentRow.Cells["approve_flag"].Value = "False";
                //    //SaveChanged();

                //}
                //return;

                //////////////////////////

                string sel;



                sel = "SELECT  class_name as 新旧, id as 代码 FROM ly_sales_borrow_class ";


                QueryForm queryForm = new QueryForm();


                queryForm.Sel = sel;
                queryForm.Constr = SQLDatabase.Connectstring;

                //Set the Column Collection to the filter Table
                //queryForm.SetSourceColumns(this.billMainDataSet.BalanceBill.Columns);

                queryForm.ShowDialog();


                if (queryForm.Result != "")
                {
                    dgv.CurrentRow.Cells["新旧"].Value = queryForm.Result;


                }
                else
                {

                    dgv.CurrentRow.Cells["新旧"].Value = DBNull.Value;

                }

                ly_sales_borrow_detailDataGridView.EndEdit();
                ly_sales_borrow_detailBindingSource.EndEdit();

                this.ly_sales_borrow_detailTableAdapter.Update(this.lYSalseMange.ly_sales_borrow_detail);



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

        private void toolStripButton16_Click_1(object sender, EventArgs e)
        {
            ExportDataGridviewTOExcell.ExportDataGridview(this.ly_sales_borrow_detail_allDataGridView, true);
        }

        private void toolStripButton21_Click_1(object sender, EventArgs e)
        {
            this.ly_sales_borrow_detail_allTableAdapter.Fill(this.lYSalseMange.ly_sales_borrow_detail_all, this.nowusercode, this.nowfillstragecode, this.dateTimePicker1.Value, this.dateTimePicker2.Value);
            this.ly_sales_borrow_detail_sumTableAdapter.Fill(this.lYSalseMange.ly_sales_borrow_detail_sum, this.nowusercode, this.nowfillstragecode, this.dateTimePicker1.Value, this.dateTimePicker2.Value);

        }

        private void toolStripTextBox4_KeyUp(object sender, KeyEventArgs e)
        {
            string filterString;


            filterString = GetDataGridviewMultiFilterString.DGVMultiFilterString(this.ly_sales_borrow_detail_allDataGridView, this.toolStripTextBox4.Text);


            this.ly_sales_borrow_detail_allBindingSource.Filter = filterString;
        }

       

        private void toolStripTextBox4_Enter(object sender, EventArgs e)
        {
            toolStripTextBox4.Text = "";

            this.ly_sales_borrow_detail_allBindingSource.Filter = "";
        }

        private void ly_sales_borrowDataGridView_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            DataGridView dgv = sender as DataGridView;

            if (null == dgv.CurrentRow) return;

            if ("True" == ly_sales_borrowDataGridView.CurrentRow.Cells["生产审批j"].Value.ToString())
            {
                MessageBox.Show("借用已经生产审批,不能修改数据...", "注意");
                return;

            }

            if ("02" != ly_sales_borrowDataGridView.CurrentRow.Cells["借用类别j"].Value.ToString())
            {
                MessageBox.Show("非个人借用,不能在这里修改数据...", "注意");
                return;

            }

            if ("True" == dgv.CurrentRow.Cells["批准j"].Value.ToString())
            {
                MessageBox.Show("借用单已经部门批准,不能修改数据...", "注意");
                return;

            }

            /////////////////////////////////////////////////////////

            if ("直发" == dgv.CurrentCell.OwningColumn.Name)
            {



                if ("True" == dgv.CurrentRow.Cells["直发"].Value.ToString())
                {
                    dgv.CurrentRow.Cells["直发"].Value = "False";

                }
                else
                {

                    dgv.CurrentRow.Cells["直发"].Value = "True";
                }



                ly_sales_borrowDataGridView.EndEdit();
                this.ly_sales_borrow_clientBindingSource.EndEdit();



                this.ly_sales_borrow_clientTableAdapter.Update(this.lYSalseMange.ly_sales_borrow_client);


                return;

            }
            ///////////////////////////////////////////////////////////
            if ("提交j" == dgv.CurrentCell.OwningColumn.Name)
            {
                ////////////////////////////////////////////////////////////////////////

                if (this.ly_sales_borrow_detailDataGridView.Rows.Count < 1)
                {
                    MessageBox.Show("借用单无明细数据,不能提交...", "注意");
                    return;
                
                }

                ///////////////////////////////////////////////////////////////////////


                if ("True" == dgv.CurrentRow.Cells["提交j"].Value.ToString())
                {
                    dgv.CurrentRow.Cells["提交j"].Value = "False";

                }
                else
                {

                    dgv.CurrentRow.Cells["提交j"].Value = "True";
                }



                ly_sales_borrowDataGridView.EndEdit();
                this.ly_sales_borrow_clientBindingSource.EndEdit();



                this.ly_sales_borrow_clientTableAdapter.Update(this.lYSalseMange.ly_sales_borrow_client);


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
            //    this.ly_sales_borrow_clientBindingSource.EndEdit();



            //    this.ly_sales_borrow_clientTableAdapter.Update(this.lYSalseMange.ly_sales_borrow_client);



            //    return;

            //}
            /////////////////////////////////////////////////////////////////////
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
            //    this.ly_sales_borrow_clientBindingSource.EndEdit();



            //    this.ly_sales_borrow_clientTableAdapter.Update(this.lYSalseMange.ly_sales_borrow_client);


            //    return;

            //}

            /////////////////////////////////////////////////////////////////
            //if ("执行j" == dgv.CurrentCell.OwningColumn.Name)
            //{



            //    if ("True" == dgv.CurrentRow.Cells["执行j"].Value.ToString())
            //    {
            //        dgv.CurrentRow.Cells["执行j"].Value = "False";

            //    }
            //    else
            //    {

            //        dgv.CurrentRow.Cells["执行j"].Value = "True";
            //    }



            //    ly_sales_borrowDataGridView.EndEdit();
            //    this.ly_sales_borrow_clientBindingSource.EndEdit();



            //    this.ly_sales_borrow_clientTableAdapter.Update(this.lYSalseMange.ly_sales_borrow_client);



            //    return;

            //}
            /////////////////////////////////////////////////////////////////
        }

        //private void fillToolStripButton_Click(object sender, EventArgs e)
        //{
        //    try  
        //    {
        //        this.ly_sales_borrow_detail_clientSumTableAdapter.Fill(this.lYSalseMange.ly_sales_borrow_detail_clientSum, client_codeToolStripTextBox.Text);
        //    }
        //    catch (System.Exception ex)
        //    {
        //        System.Windows.Forms.MessageBox.Show(ex.Message);
        //    }

        //}

        
      
        
     

       
       

       
    }
}
