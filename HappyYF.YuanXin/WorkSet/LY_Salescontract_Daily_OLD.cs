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
    public partial class LY_Salescontract_Daily_OLD : Form
    {
        private string nowfilterStr = "";
        private string nowusercode = "";
        private string nowfillstragecode = "";

        private string nowclientCode = "";
        private string nowinnerCode = "";
        private string contractCanchenged = "";

        public LY_Salescontract_Daily_OLD()
        {
            InitializeComponent();
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
            this.ly_sales_contract_mainBindingSource.Filter = " 提交=1 ";
            this.ly_sales_contract_main1BindingSource.Filter = " 提交=1 ";
            
            this.ly_lsptb_selTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            this.ly_lsptb_selTableAdapter.Fill(this.lYSalseMange.ly_lsptb_sel);

            this.nowusercode = SQLDatabase.NowUserID;
            
            this.ly_sales_contract_main1TableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            this.ly_sales_contract_terms_forcontractTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            this.ly_sales_contract_detailTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
          

            
            
            
            this.ly_sales_contract_mainTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            //this.ly_sales_contract_mainTableAdapter.Fill(this.lYSalseMange.ly_sales_contract_main,"");

            this.dateTimePicker1.Text = DateTime.Today.AddMonths(-6).Date.ToString();
            this.dateTimePicker2.Text = DateTime.Today.AddDays(1).Date.ToString();

            this.ly_sales_clientTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            this.ly_sales_clientTableAdapter.Fill(this.lYSalseMange.ly_sales_client);

            string selAllString;

            if (SQLDatabase.CheckHaveRight(SQLDatabase.NowUserID, "营业综合信息"))
            {

                selAllString = "SELECT  a.salesregion_code, a.salesregion_code+':'+a.salesregion_name as salesregion_name,b.yhbm,b.yhbm+':'+b.yhmc as yhmc FROM  ly_salesregion a left join T_users b on a.salesregion_code=b.salesregion_code ORDER BY  salesregion_code ";
            }
            else if (SQLDatabase.CheckHaveRight(SQLDatabase.NowUserID, "营业区域信息"))
            {
              
                selAllString = "SELECT  a.salesregion_code, a.salesregion_code+':'+a.salesregion_name as salesregion_name,b.yhbm,b.yhbm+':'+b.yhmc as yhmc FROM  ly_salesregion a left join T_users b on a.salesregion_code=b.salesregion_code  where b.salesregion_code='" + SQLDatabase.nowSalesregioncode () + "' ORDER BY  salesregion_code ";
            }
            else
            {
                selAllString = "SELECT  a.salesregion_code, a.salesregion_code+':'+a.salesregion_name as salesregion_name,b.yhbm,b.yhbm+':'+b.yhmc as yhmc FROM  ly_salesregion a left join T_users b on a.salesregion_code=b.salesregion_code  where b.yhbm='" + SQLDatabase.NowUserID + "' ORDER BY  salesregion_code ";
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
            else
            {
                //this.treeView1.Visible = false;
                this.splitContainer1.Panel1Collapsed = true;
                this.nowfilterStr = "salesperson_code='" + SQLDatabase.NowUserID + "'";
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
            this.ly_sales_clientBindingSource.Filter = e.Node.Tag.ToString() ;
            this.nowfilterStr = e.Node.Tag.ToString();

            if (e.Node.Level == 2)
            {
                this.ly_sales_clientDataGridView.Columns["yhbm"].Visible = false;
                this.ly_sales_clientDataGridView.Columns["yhmc"].Visible = false;

                this.nowusercode = this.nowfilterStr.Substring(this.nowfilterStr.Length - 4, 3);
                this.nowfillstragecode = "single";

                this.ly_sales_contract_main1DataGridView.SelectionChanged -= ly_sales_contract_main1DataGridView_SelectionChanged;
                this.ly_sales_contract_main1TableAdapter.Fill(this.lYSalseMange.ly_sales_contract_main1, this.nowusercode,"single", this.dateTimePicker1.Value , this.dateTimePicker2.Value );
                AddSummationRow_New(ly_sales_contract_main1BindingSource, ly_sales_contract_main1DataGridView);
                this.ly_sales_contract_main1DataGridView.SelectionChanged += ly_sales_contract_main1DataGridView_SelectionChanged;
               
            }
            else if (e.Node.Level == 1)
            {
                this.ly_sales_clientDataGridView.Columns["yhbm"].Visible = true ;
                this.ly_sales_clientDataGridView.Columns["yhmc"].Visible = true ;

                this.nowusercode = this.nowfilterStr.Substring(this.nowfilterStr.Length - 3, 2);
                this.nowfillstragecode = "region";

                this.ly_sales_contract_main1DataGridView.SelectionChanged -= ly_sales_contract_main1DataGridView_SelectionChanged;
                this.ly_sales_contract_main1TableAdapter.Fill(this.lYSalseMange.ly_sales_contract_main1, this.nowusercode, "region", this.dateTimePicker1.Value, this.dateTimePicker2.Value);
                AddSummationRow_New(ly_sales_contract_main1BindingSource, ly_sales_contract_main1DataGridView);
                this.ly_sales_contract_main1DataGridView.SelectionChanged += ly_sales_contract_main1DataGridView_SelectionChanged;
            }
            else if (e.Node.Level == 0)
            {
                this.ly_sales_clientDataGridView.Columns["yhbm"].Visible = true;
                this.ly_sales_clientDataGridView.Columns["yhmc"].Visible = true;

                this.nowusercode = "";
                this.nowfillstragecode = "full";

                this.ly_sales_contract_main1DataGridView.SelectionChanged -= ly_sales_contract_main1DataGridView_SelectionChanged;
                this.ly_sales_contract_main1TableAdapter.Fill(this.lYSalseMange.ly_sales_contract_main1, this.nowusercode, "full", this.dateTimePicker1.Value, this.dateTimePicker2.Value);
                AddSummationRow_New(ly_sales_contract_main1BindingSource, ly_sales_contract_main1DataGridView);
                this.ly_sales_contract_main1DataGridView.SelectionChanged += ly_sales_contract_main1DataGridView_SelectionChanged;
            }

           

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

        private void t_usersDataGridView_SelectionChanged(object sender, EventArgs e)
        {
            //this.itemofserviceTableAdapter.Fill(this.yonghuDataSet.Itemofservice, this.t_usersDataGridView.CurrentRow.Cells["yhbm"].Value.ToString());
            //this.itemofserviceBindingSource .Filter = "ItemofserviceNumber = '" + this.t_usersDataGridView.CurrentRow.Cells["yhbm"].Value.ToString() + "'";
            //if (null != this.t_usersDataGridView.CurrentRow)
               
            //this.itemofserviceBindingSource.Position = this.itemofserviceBindingSource.Find("ItemofserviceNumber", this.t_usersDataGridView.CurrentRow.Cells["duty_class"].Value.ToString());
        }

        private void duty_classComboBox_Click(object sender, EventArgs e)
        {
            //this.itemofserviceTableAdapter.Fill(this.yonghuDataSet.Itemofservice, this.t_usersDataGridView.CurrentRow.Cells["yhbm"].Value.ToString());
        }

        private void ly_employe_warehouseDataGridView_DoubleClick(object sender, EventArgs e)
        {
            //if (null == ly_employe_warehouseDataGridView.CurrentRow) return;
            //if (this.yhbmTextBox.ReadOnly != true) return;

            //string warehouseName = this.ly_employe_warehouseDataGridView.CurrentRow.Cells["warehousename"].Value.ToString();
            //string haveRight = this.ly_employe_warehouseDataGridView.CurrentRow.Cells["haveright"].Value.ToString();



            //if (haveRight == "0")
            //{




            //    string insStr = " INSERT INTO ly_employe_warehouse  " +
            //   "( yonghu_code,warehouse_name) " +
            //   " values ('" + yhbmTextBox.Text + "','" + warehouseName + "' )";


            //    using (TransactionScope scope = new TransactionScope())
            //    {

            //        SqlConnection sqlConnection1 = new SqlConnection(Program.dataBase.MakeConnectString());
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
            //else
            //{

            //    string delStr = " delete ly_employe_warehouse  " +
            // "where  yonghu_code='"+ yhbmTextBox.Text + "'" + " and warehouse_name ='" + warehouseName + "'";
           

            //    using (TransactionScope scope = new TransactionScope())
            //    {

            //        SqlConnection sqlConnection1 = new SqlConnection(Program.dataBase.MakeConnectString());
            //        SqlCommand cmd = new SqlCommand();

            //        cmd.CommandText = delStr;
            //        cmd.CommandType = CommandType.Text;
            //        cmd.Connection = sqlConnection1;



            //        sqlConnection1.Open();
            //        cmd.ExecuteNonQuery();

            //        sqlConnection1.Close();

            //        scope.Complete();
            //    }
            
            //}



            //this.ly_employe_warehouseTableAdapter.Fill(this.yonghuDataSet.ly_employe_warehouse, yhbmTextBox.Text);

            //this.ly_employe_warehouseBindingSource.Position = this.ly_employe_warehouseBindingSource.Find("warehousename", warehouseName);
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
                this.ly_sales_contract_mainTableAdapter.Fill(this.lYSalseMange.ly_sales_contract_main, "");

                return;
            }

            string nowclientcode = this.ly_sales_clientDataGridView.CurrentRow.Cells["客户编码"].Value.ToString();
            string nowclientname = this.ly_sales_clientDataGridView.CurrentRow.Cells["客户名称"].Value.ToString();

            this.groupBox1.Text = nowclientname + "合同列表";

            this.ly_sales_contract_mainTableAdapter.Fill(this.lYSalseMange.ly_sales_contract_main, nowclientcode);
           


        }

        private void toolStripButton8_Click(object sender, EventArgs e)
        {
            if (null == ly_sales_clientDataGridView.CurrentRow)
            {
                this.ly_sales_contract_mainTableAdapter.Fill(this.lYSalseMange.ly_sales_contract_main, "");

                return;
            }

            string nowclientcode = this.ly_sales_clientDataGridView.CurrentRow.Cells["客户编码"].Value.ToString();
            
            LY_Salescontract_Add queryForm = new LY_Salescontract_Add();

            queryForm.salesclientcode = nowclientcode;
            queryForm.runmode = "增加";


            queryForm.StartPosition = FormStartPosition.CenterParent;
            queryForm.ShowDialog();

            if (queryForm.DialogResult != DialogResult.Cancel)
            {
                this.ly_sales_contract_mainTableAdapter.Fill(this.lYSalseMange.ly_sales_contract_main, nowclientcode);



                this.ly_sales_contract_mainBindingSource.Position = this.ly_sales_contract_mainBindingSource.Find("内部编码", queryForm.contractinner_code);

                this.ly_sales_contract_main1DataGridView.SelectionChanged -= ly_sales_contract_main1DataGridView_SelectionChanged;
                this.ly_sales_contract_main1TableAdapter.Fill(this.lYSalseMange.ly_sales_contract_main1, this.nowusercode, "single", this.dateTimePicker1.Value, this.dateTimePicker2.Value);
                this.ly_sales_contract_main1DataGridView.SelectionChanged += ly_sales_contract_main1DataGridView_SelectionChanged;
            }
        }

        private void toolStripButton9_Click(object sender, EventArgs e)
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

            string nowclientcode = this.ly_sales_clientDataGridView.CurrentRow.Cells["客户编码"].Value.ToString();
            string nowinnercode = this.ly_sales_contract_mainDataGridView.CurrentRow.Cells["内部编码"].Value.ToString();

            LY_Salescontract_Add queryForm = new LY_Salescontract_Add();

            queryForm.salesclientcode = nowclientcode;
            queryForm.contractinner_code = nowinnercode;
            queryForm.runmode = "修改";


            queryForm.StartPosition = FormStartPosition.CenterParent;
            queryForm.ShowDialog();

            if (queryForm.DialogResult != DialogResult.Cancel)
            {
                this.ly_sales_contract_mainTableAdapter.Fill(this.lYSalseMange.ly_sales_contract_main, nowclientcode);



                this.ly_sales_contract_mainBindingSource.Position = this.ly_sales_contract_mainBindingSource.Find("内部编码", queryForm.contractinner_code);

                this.ly_sales_contract_main1DataGridView.SelectionChanged -= ly_sales_contract_main1DataGridView_SelectionChanged;
                this.ly_sales_contract_main1TableAdapter.Fill(this.lYSalseMange.ly_sales_contract_main1, this.nowusercode, "single", this.dateTimePicker1.Value, this.dateTimePicker2.Value);
                this.ly_sales_contract_main1DataGridView.SelectionChanged += ly_sales_contract_main1DataGridView_SelectionChanged;
            }
        }

        private void toolStripButton10_Click(object sender, EventArgs e)
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

                this.ly_sales_contract_mainBindingSource.RemoveCurrent();


                ly_sales_contract_mainDataGridView.EndEdit();
                ly_sales_contract_mainBindingSource.EndEdit();


               
                this.ly_sales_contract_mainTableAdapter.Update(this.lYSalseMange.ly_sales_contract_main);

                this.ly_sales_contract_main1DataGridView.SelectionChanged -= ly_sales_contract_main1DataGridView_SelectionChanged;
                this.ly_sales_contract_main1TableAdapter.Fill(this.lYSalseMange.ly_sales_contract_main1, this.nowusercode, "single", this.dateTimePicker1.Value, this.dateTimePicker2.Value);
                this.ly_sales_contract_main1DataGridView.SelectionChanged += ly_sales_contract_main1DataGridView_SelectionChanged;
            }
        }

        private void toolStripTextBox3_KeyUp(object sender, KeyEventArgs e)
        {
            string filterString;


            filterString = " 提交=1   " + "  and ( " + GetDataGridviewMultiFilterString.DGVMultiFilterString(this.ly_sales_contract_mainDataGridView, this.toolStripTextBox3.Text) + ")";


            this.ly_sales_contract_mainBindingSource.Filter = filterString;
        }

        private void toolStripTextBox3_Enter(object sender, EventArgs e)
        {
            toolStripTextBox3.Text = "";

            this.ly_sales_contract_mainBindingSource.Filter = " 提交=1 ";
        }

        private void SaveContract()
        {
            this.ly_sales_contract_mainDataGridView.EndEdit();
            this.ly_sales_contract_mainBindingSource.EndEdit();

            this.ly_sales_contract_mainTableAdapter.Update(this.lYSalseMange.ly_sales_contract_main);



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



            int  nowcontractdetailid;
            if (null != ly_sales_contract_detailDataGridView.CurrentRow)
            {
                nowcontractdetailid = int.Parse(ly_sales_contract_detailDataGridView.CurrentRow.Cells["id_detail"].Value.ToString());
            }
            else
            {
                nowcontractdetailid = 0;
            }


            string nowclientcode = this.ly_sales_contract_mainDataGridView.CurrentRow.Cells["客户编码c"].Value.ToString();




            this.ly_sales_contract_mainTableAdapter.Fill(this.lYSalseMange.ly_sales_contract_main, nowclientcode);
            this.ly_sales_contract_mainBindingSource.Position = this.ly_sales_contract_mainBindingSource.Find("id", nowcontractId);

            this.ly_sales_contract_main1DataGridView.SelectionChanged -= ly_sales_contract_main1DataGridView_SelectionChanged;
            this.ly_sales_contract_main1TableAdapter.Fill(this.lYSalseMange.ly_sales_contract_main1, this.nowusercode, "single", this.dateTimePicker1.Value, this.dateTimePicker2.Value);
            this.ly_sales_contract_main1DataGridView.SelectionChanged += ly_sales_contract_main1DataGridView_SelectionChanged;

            this.ly_sales_contract_detailBindingSource.Position = this.ly_sales_contract_detailBindingSource.Find("id", nowcontractdetailid);
            //this.ly_purchase_contract_detailBindingSource.Position = this.ly_purchase_contract_detailBindingSource.Find("物料编号", nowcontractdetailcode);

        }

        private void ly_sales_contract_mainDataGridView_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            DataGridView dgv = sender as DataGridView;

            if (null == dgv.CurrentRow) return;




         
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
                
                
                
                
                
            //    if ("True" == dgv.CurrentRow.Cells["提交"].Value.ToString())
            //    {
            //        dgv.CurrentRow.Cells["提交"].Value = "False";

            //    }
            //    else
            //    {

            //        dgv.CurrentRow.Cells["提交"].Value = "True";
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


            //if ("True" == dgv.CurrentRow.Cells["审核"].Value.ToString())
            //{
            //    MessageBox.Show("合同已经审批,不能修改数据...", "注意");
            //    return;

            //}



           

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

            if ("批准" == dgv.CurrentCell.OwningColumn.Name)
            {

                if ("True" == dgv.CurrentRow.Cells["批准"].Value.ToString())
                {
                    dgv.CurrentRow.Cells["批准"].Value = "False";
                    dgv.CurrentRow.Cells["批准人"].Value = DBNull.Value;
                    dgv.CurrentRow.Cells["批准日期"].Value = DBNull.Value;
                    dgv.CurrentRow.Cells["合同文本交付"].Value = DBNull.Value;
                }
                else
                {

                    dgv.CurrentRow.Cells["批准"].Value = "True";
                    dgv.CurrentRow.Cells["批准人"].Value = SQLDatabase.nowUserName();
                    dgv.CurrentRow.Cells["批准日期"].Value = SQLDatabase.GetNowdate();
                    dgv.CurrentRow.Cells["合同文本交付"].Value = SQLDatabase.GetNowdate();
                }



                SaveContract();



                return;

            }
            /////////////////////////////////////////////////////////////
            if ("批准日期" == dgv.CurrentCell.OwningColumn.Name)
            {

                ChangeValue queryForm = new ChangeValue();

                queryForm.OldValue = dgv.CurrentCell.Value.ToString();
                queryForm.NewValue = "";
                queryForm.ChangeMode = "datetime";
                queryForm.ShowDialog();




                if (queryForm.NewValue != "")
                {
                    dgv.CurrentRow.Cells["批准日期"].Value = queryForm.NewValue;
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
            /////////////////////////////////////////////////////////////

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
            ///////////////////////////////////////////////////////////////
            if ("已交" == dgv.CurrentCell.OwningColumn.Name)
            {

                if ("True" == dgv.CurrentRow.Cells["已交"].Value.ToString())
                {
                    dgv.CurrentRow.Cells["已交"].Value = "False";

                }
                else
                {

                    dgv.CurrentRow.Cells["已交"].Value = "True";
                }



                SaveContract();



                return;

            }
            ///////////////////////////////////////////////////////////////

            if ("合同文本交付" == dgv.CurrentCell.OwningColumn.Name)
            {

               

                ChangeValue queryForm = new ChangeValue();

                queryForm.OldValue = dgv.CurrentCell.Value.ToString();
                queryForm.NewValue = "";
                queryForm.ChangeMode = "datetime";
                queryForm.ShowDialog();


                if (queryForm.NewValue != "")
                {
                    dgv.CurrentRow.Cells["合同文本交付"].Value = queryForm.NewValue;

                }
                else
                {

                    dgv.CurrentRow.Cells["合同文本交付"].Value = DBNull.Value;

                }



                SaveContract();



                return;

            }
            //////////////////////////////////////////////////////////////////////////

            if ("开票日期" == dgv.CurrentCell.OwningColumn.Name)
            {

                ChangeValue queryForm = new ChangeValue();

                queryForm.OldValue = dgv.CurrentCell.Value.ToString();
                queryForm.NewValue = "";
                queryForm.ChangeMode = "datetime";
                queryForm.ShowDialog();


                if (queryForm.NewValue != "")
               
                {
                    dgv.CurrentRow.Cells["开票日期"].Value = queryForm.NewValue;
                    dgv.CurrentRow.Cells["开票人"].Value = SQLDatabase.nowUserName();
                }
                else
                {

                    dgv.CurrentRow.Cells["开票日期"].Value = DBNull.Value;
                    dgv.CurrentRow.Cells["开票人"].Value = DBNull.Value; 
                }



                SaveContract();



                return;

            }
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

            //if ("类别" == dgv.CurrentCell.OwningColumn.Name )
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


            /////////////////////////////////////////////////////////

            //if ("合同编码" == dgv.CurrentCell.OwningColumn.Name)
            //{


            //       ChangeValue queryForm = new ChangeValue();

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


            /////////////////////////////////////////////////////////
            }
        private void 修改当前合同ToolStripMenuItem_Click(object sender, EventArgs e)
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
                MessageBox.Show("合同已经审批,不能修改数据...", "注意");
                return;

            }

            if ("True" == ly_sales_contract_mainDataGridView.CurrentRow.Cells["审核"].Value.ToString())
            {
                MessageBox.Show("合同已经审批,不能修改数据...", "注意");
                return;

            }

            string nowclientcode = this.ly_sales_clientDataGridView.CurrentRow.Cells["客户编码"].Value.ToString();
            string nowinnercode = this.ly_sales_contract_mainDataGridView.CurrentRow.Cells["内部编码"].Value.ToString();

            LY_Salescontract_Add queryForm = new LY_Salescontract_Add();

            queryForm.salesclientcode = nowclientcode;
            queryForm.contractinner_code = nowinnercode;
            queryForm.runmode = "修改";


            queryForm.StartPosition = FormStartPosition.CenterParent;
            queryForm.ShowDialog();

            if (queryForm.DialogResult != DialogResult.Cancel)
            {
                this.ly_sales_contract_mainTableAdapter.Fill(this.lYSalseMange.ly_sales_contract_main, nowclientcode);



                this.ly_sales_contract_mainBindingSource.Position = this.ly_sales_contract_mainBindingSource.Find("内部编码", queryForm.contractinner_code);
            }
        }

        private void 删除当前合同ToolStripMenuItem_Click(object sender, EventArgs e)
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
                MessageBox.Show("合同已经审批,不能删除数据...", "注意");
                return;

            }

            if ("True" == ly_sales_contract_mainDataGridView.CurrentRow.Cells["审核"].Value.ToString())
            {
                MessageBox.Show("合同已经审批,不能删除数据...", "注意");
                return;

            }


            string message = "确定删除当前合同记录吗？";
            string caption = "提示...";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result;



            result = MessageBox.Show(message, caption, buttons,
            MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

            if (result == DialogResult.Yes)
            {

                this.ly_sales_contract_mainBindingSource.RemoveCurrent();


                ly_sales_contract_mainDataGridView.EndEdit();
                ly_sales_contract_mainBindingSource.EndEdit();



                this.ly_sales_contract_mainTableAdapter.Update(this.lYSalseMange.ly_sales_contract_main);
            }
        }

        private void 同步合同客户信息ToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (null == ly_sales_contract_mainDataGridView.CurrentRow)
            {


                return;
            }

            if ("True" == ly_sales_contract_mainDataGridView.CurrentRow.Cells["提交"].Value.ToString())
            {
                MessageBox.Show("合同已经提交,不能同步合同客户数据...", "注意");
                return;

            }

            if ("True" == ly_sales_contract_mainDataGridView.CurrentRow.Cells["批准"].Value.ToString())
            {
                MessageBox.Show("合同已经审批,不能同步合同客户数据...", "注意");
                return;

            }

            if ("True" == ly_sales_contract_mainDataGridView.CurrentRow.Cells["审核"].Value.ToString())
            {
                MessageBox.Show("合同已经审批,不能同步合同客户数据...", "注意");
                return;

            }

            string nowclientcode = this.ly_sales_clientDataGridView.CurrentRow.Cells["客户编码"].Value.ToString();
            string nowinnercode = this.ly_sales_contract_mainDataGridView.CurrentRow.Cells["内部编码"].Value.ToString();

           
               
            
            SqlConnection sqlConnection1 = new SqlConnection(SQLDatabase.Connectstring);
            SqlCommand cmd = new SqlCommand();



            cmd.Parameters.Add("@contract_inner_code", SqlDbType.VarChar);
            cmd.Parameters["@contract_inner_code"].Value = nowinnercode;

            //cmd.Parameters.Add("@loanId", SqlDbType.Int);
            //cmd.Parameters["@loanId"].Value = loanId;

            //cmd.Parameters.Add("@item_name", SqlDbType.VarChar);
            //cmd.Parameters["@item_name"].Value = item_name;


            cmd.CommandText = "LY_syn_salse_contractClient";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = sqlConnection1;

            sqlConnection1.Open();
             cmd.ExecuteNonQuery();
            sqlConnection1.Close();

            this.ly_sales_contract_mainTableAdapter.Fill(this.lYSalseMange.ly_sales_contract_main, nowclientcode);

            this.ly_sales_contract_mainBindingSource.Position = this.ly_sales_contract_mainBindingSource.Find("内部编码", nowinnercode);
            
            
        }

        private void ly_sales_contract_main1DataGridView_SelectionChanged(object sender, EventArgs e)
        {
            if (null == ly_sales_contract_main1DataGridView.CurrentRow)
            {
                this.ly_sales_contract_terms_forcontractTableAdapter.Fill(this.lYSalseMange.ly_sales_contract_terms_forcontract, "");

                return;
            } 

            this .nowinnerCode  = ly_sales_contract_main1DataGridView.CurrentRow.Cells["内部编码1"].Value.ToString();
            this.nowclientCode = ly_sales_contract_main1DataGridView.CurrentRow.Cells["客户编码c1"].Value.ToString();

            this.ly_sales_contract_terms_forcontractTableAdapter.Fill(this.lYSalseMange.ly_sales_contract_terms_forcontract, nowinnerCode);
            this.ly_sales_contract_detailTableAdapter.Fill(this.lYSalseMange.ly_sales_contract_detail, nowinnerCode, 0);



            if ("True" == ly_sales_contract_main1DataGridView.CurrentRow.Cells["提交1"].Value.ToString() ||
                "True" == ly_sales_contract_main1DataGridView.CurrentRow.Cells["批准1"].Value.ToString() )
            {
                this.contractCanchenged = "False";
            }
            else
            {
                this.contractCanchenged = "True";
            }

            this.ly_sales_contract_mainDataGridView.SelectionChanged -= ly_sales_contract_mainDataGridView_SelectionChanged;
            this.ly_sales_clientBindingSource.Position = this.ly_sales_clientBindingSource.Find("客户编码", this.nowclientCode);
            this.ly_sales_contract_mainBindingSource.Position = this.ly_sales_contract_mainBindingSource.Find("内部编码", this.nowinnerCode);
            this.ly_sales_contract_mainDataGridView.SelectionChanged += ly_sales_contract_mainDataGridView_SelectionChanged;




        }

        private void toolStripButton21_Click(object sender, EventArgs e)
        {
            this.ly_sales_contract_main1TableAdapter.Fill(this.lYSalseMange.ly_sales_contract_main1, this.nowusercode, this.nowfillstragecode, this.dateTimePicker1.Value, this.dateTimePicker2.Value);
            AddSummationRow_New(ly_sales_contract_main1BindingSource, ly_sales_contract_main1DataGridView);
        }

       

        private void ly_sales_contract_mainDataGridView_SelectionChanged(object sender, EventArgs e)
        {
            if (null == ly_sales_contract_mainDataGridView.CurrentRow)
            {
                this.ly_sales_contract_terms_forcontractTableAdapter.Fill(this.lYSalseMange.ly_sales_contract_terms_forcontract, "");
                this.ly_sales_contract_detailTableAdapter.Fill(this.lYSalseMange.ly_sales_contract_detail, "", 0);

                return;
            }

            this .nowinnerCode = ly_sales_contract_mainDataGridView.CurrentRow.Cells["内部编码"].Value.ToString();
            this.nowclientCode = ly_sales_contract_mainDataGridView.CurrentRow.Cells["客户编码c"].Value.ToString();

            this.ly_sales_contract_terms_forcontractTableAdapter.Fill(this.lYSalseMange.ly_sales_contract_terms_forcontract, nowinnerCode);
            this.ly_sales_contract_detailTableAdapter.Fill(this.lYSalseMange.ly_sales_contract_detail, nowinnerCode, 0);



            if ("True" == ly_sales_contract_mainDataGridView.CurrentRow.Cells["提交"].Value.ToString() ||
                "True" == ly_sales_contract_mainDataGridView.CurrentRow.Cells["批准"].Value.ToString())
                
            {
             this .contractCanchenged ="False";
            }
            else 
            {
              this .contractCanchenged ="True";
            }


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

            if (nowsalesprice==0)
            {

                MessageBox.Show("产品无定价,不能销售...", "注意");
                return;
            }



            string nowitemno = this.ly_lsptb_selDataGridView.CurrentRow.Cells["物料编号2"].Value.ToString();









            InserSalseContractDetail(this.nowinnerCode , nowitemno, 1);



            this.ly_sales_contract_detailTableAdapter.Fill(this.lYSalseMange.ly_sales_contract_detail, nowinnerCode, 0);

            this.ly_sales_contract_detailBindingSource.Position = this.ly_sales_contract_detailBindingSource.Find("产品编码", nowitemno);

            this.ly_sales_contract_detailDataGridView.CurrentCell =this.ly_sales_contract_detailDataGridView.CurrentRow .Cells["单价"]; 
        }

        private static void InserSalseContractDetail(string innerCode, string nowitem, decimal nowabsqty)
        {
            string insStr = " INSERT INTO ly_sales_contract_detail  " +
           "( contract_inner_code,itemno,qty) " +
           " values ('" + innerCode + "','" + nowitem + "'," + nowabsqty + ")";


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

        private void ly_sales_contract_detailDataGridView_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            return;
            
            DataGridView dgv = sender as DataGridView;

            if (null == dgv.CurrentRow) return;

            if ("False" == this.contractCanchenged)
            {
                MessageBox.Show("合同已经提交执行,不能修改数据...", "注意");
                return;

            }



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
            this.ly_sales_clientDataGridView.SelectionChanged -= ly_sales_clientDataGridView_SelectionChanged;
            this.ly_sales_clientTableAdapter.Fill(this.lYSalseMange.ly_sales_client);
            this.ly_sales_clientDataGridView.SelectionChanged += ly_sales_clientDataGridView_SelectionChanged;

            this.ly_sales_clientBindingSource.Position = this.ly_sales_clientBindingSource.Find("客户编码", nowclientCode);

            this.ly_sales_contract_mainTableAdapter.Fill(this.lYSalseMange.ly_sales_contract_main, this .nowclientCode );
            this.ly_sales_contract_mainBindingSource.Position = this.ly_sales_contract_mainBindingSource.Find("id", nowcontractId);

            this.ly_sales_contract_main1DataGridView.SelectionChanged -= ly_sales_contract_main1DataGridView_SelectionChanged;
            this.ly_sales_contract_main1TableAdapter.Fill(this.lYSalseMange.ly_sales_contract_main1, this.nowusercode, "single", this.dateTimePicker1.Value, this.dateTimePicker2.Value);
            this.ly_sales_contract_main1DataGridView.SelectionChanged += ly_sales_contract_main1DataGridView_SelectionChanged;

            //this.ly_sales_contract_detailTableAdapter.Fill(this.lYSalseMange.ly_sales_contract_detail, nowinnerCode);

            this.ly_sales_contract_detailBindingSource.Position = this.ly_sales_contract_detailBindingSource.Find("id", nowdetailId);
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

        private void 删除当前记录ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DeleteDetail();
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
            if (null == this.ly_sales_contract_detailDataGridView.CurrentRow) return;

            if ("" == ly_sales_contract_mainDataGridView.CurrentRow.Cells["类别"].Value.ToString())
            {

                MessageBox.Show("请选择合同类别,然后打印...", "注意");
                return;
            }

            NewFrm.Show(this); ;

            //this.ly_plan_getmaterialTableAdapter.Fill(this.lYPlanMange.ly_plan_getmaterial, parentId);

            BaseReportView queryForm = new BaseReportView();

            queryForm.Text = "中原精密营业合同";

            queryForm.Printdata = this.lYSalseMange;


            if ("工矿" == ly_sales_contract_mainDataGridView.CurrentRow.Cells["类别"].Value.ToString())
            {
                queryForm.PrintCrystalReport = new LY_YingyeHetong();
            }
            else
            {

                queryForm.PrintCrystalReport = new LY_YingyeHetong_XH();
            }


            //string selectFormula;

            //selectFormula = "{ly_store_planitemcount.状态}  =   '原料'  and {ly_store_planitemcount.欠料金额}>0 ";
            //queryForm.PrintCrystalReport.DataDefinition.RecordSelectionFormula = selectFormula;

            NewFrm.Hide(this);

            queryForm.ShowDialog();
        }

        private void toolStripTextBox5_KeyUp(object sender, KeyEventArgs e)
        {
            string filterString;


            
            filterString = " 提交=1   " + "  and ( " + GetDataGridviewMultiFilterString.DGVMultiFilterString(this.ly_sales_contract_main1DataGridView, this.toolStripTextBox5.Text) + ")";


            this.ly_sales_contract_main1BindingSource.Filter = filterString;
        }

        private void toolStripTextBox5_Enter(object sender, EventArgs e)
        {
            toolStripTextBox5.Text = "";

            this.ly_sales_contract_main1BindingSource.Filter = " 提交=1 ";
        }

        private void toolStripButton16_Click(object sender, EventArgs e)
        {
            ExportDataGridviewTOExcell.ExportDataGridview(this.ly_sales_contract_main1DataGridView, true);
        }

        private void toolStripButton7_Click(object sender, EventArgs e)
        {
            ExportDataGridviewTOExcell.ExportDataGridview(this.ly_sales_contract_mainDataGridView, true);
        }

        private void ly_sales_contract_detailDataGridView_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            string  isgood ="yes";

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

                    if ( nowrealdis > (nowproductdis * nowuserdis)/100 && nowrealdis > nowclientdis)
                    {
                        foreach ( DataGridViewCell dgc in dgr.Cells )
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
            if (null == this.ly_sales_contract_detailDataGridView.CurrentRow) return;

            //if ("" == ly_sales_contract_mainDataGridView.CurrentRow.Cells["类别"].Value.ToString())
            //{

            //    MessageBox.Show("请选择合同类别,然后打印...", "注意");
            //    return;
            //}

            NewFrm.Show(this); ;

            //this.ly_plan_getmaterialTableAdapter.Fill(this.lYPlanMange.ly_plan_getmaterial, parentId);

            BaseReportView queryForm = new BaseReportView();

            queryForm.Text = "中原精密营业合同";

            queryForm.Printdata = this.lYSalseMange;


            if ("True" != this.ly_sales_contract_mainDataGridView.CurrentRow.Cells["借用"].Value.ToString())
            {
                queryForm.PrintCrystalReport = new LY_YingyeHetong_FH();
            }
            else
            {

                queryForm.PrintCrystalReport = new LY_YingyeHetong_FHJY();
            }



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

            NewFrm.Hide(this);

            queryForm.ShowDialog();
        }

     

        //private void fillToolStripButton_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        this.ly_sales_contract_detailTableAdapter.Fill(this.lYSalseMange.ly_sales_contract_detail, contract_inner_codeToolStripTextBox.Text);
        //    }
        //    catch (System.Exception ex)
        //    {
        //        System.Windows.Forms.MessageBox.Show(ex.Message);
        //    }

        //}

       
       
       

       
    }
}
