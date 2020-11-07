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


 namespace HappyYF.YuanXin.WorkSet
{
    public partial class LY_Salesclient_Receivables_Mange : Form
    {
        public LY_Salesclient_Receivables_Mange()
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
            this.ly_sales_client_receivablesTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            this.ly_sales_client_receivablesTableAdapter.Fill(this.lYSalseMange.ly_sales_client_receivables);
            
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

            SetViewState("View");

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
            this.ly_sales_client_receivablesBindingSource.Filter = e.Node.Tag.ToString();

            if (e.Node.Level == 2)
            {
                this.ly_sales_clientDataGridView.Columns["yhbm"].Visible = false;
                this.ly_sales_clientDataGridView.Columns["yhmc"].Visible = false;
            }
            else
            {
                this.ly_sales_clientDataGridView.Columns["yhbm"].Visible = true ;
                this.ly_sales_clientDataGridView.Columns["yhmc"].Visible = true ;
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
            this.ly_sales_client_receivablesBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.lYSalseMange);

        }

        private void toolStripTextBox1_KeyUp(object sender, KeyEventArgs e)
        {
            string filterString ;

            if ("" == this.treeView1.SelectedNode.Tag.ToString())
            {
                filterString = GetDataGridviewMultiFilterString.DGVMultiFilterString(this.ly_sales_clientDataGridView, this.toolStripTextBox1.Text);

            }
            else
            {

                filterString = this.treeView1.SelectedNode.Tag.ToString() + " and (" + GetDataGridviewMultiFilterString.DGVMultiFilterString(this.ly_sales_clientDataGridView, this.toolStripTextBox1.Text)+")"; 
            }

            if (null == filterString)
                filterString = this.treeView1.SelectedNode.Tag.ToString();

            this.ly_sales_client_receivablesBindingSource.Filter = filterString;
        }

        private void toolStripTextBox1_Enter(object sender, EventArgs e)
        {
            toolStripTextBox1.Text = "";

            if (SQLDatabase.CheckHaveRight(SQLDatabase.NowUserID, "营业综合信息"))
            {
                this.ly_sales_client_receivablesBindingSource.Filter = this.treeView1.SelectedNode.Tag.ToString();
            }
            else if (SQLDatabase.CheckHaveRight(SQLDatabase.NowUserID, "营业区域信息"))
            {
                this.ly_sales_client_receivablesBindingSource.Filter = "salesregion_code='" + SQLDatabase.nowSalesregioncode() + "'";
            }
            else
            {
                this.ly_sales_client_receivablesBindingSource.Filter = "salesperson_code='" + SQLDatabase.NowUserID + "'";

            }


            //this.ly_sales_clientBindingSource.Filter = this.treeView1.SelectedNode.Tag.ToString();
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
                this.ly_sales_client_receivablesTableAdapter.Fill(this.lYSalseMange.ly_sales_client_receivables);

                this.treeView1.SelectedNode = FindNode(this.treeView1.Nodes, queryForm.salesperson_code);

                this.ly_sales_client_receivablesBindingSource.Position = this.ly_sales_client_receivablesBindingSource.Find("客户编码", queryForm.salesclient_code);
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
                this.ly_sales_client_receivablesTableAdapter.Fill(this.lYSalseMange.ly_sales_client_receivables);

                this.treeView1.SelectedNode = FindNode(this.treeView1.Nodes, queryForm.salesperson_code);

                this.ly_sales_client_receivablesBindingSource.Position = this.ly_sales_client_receivablesBindingSource.Find("客户编码", s);


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
                this.ly_sales_client_receivablesTableAdapter.Fill(this.lYSalseMange.ly_sales_client_receivables);

                this.treeView1.SelectedNode = FindNode(this.treeView1.Nodes, queryForm.salesperson_code);
                this.ly_sales_client_receivablesBindingSource.Position = this.ly_sales_client_receivablesBindingSource.Find("客户编码", s);


            }

        }

        private void bindingNavigatorDeleteItem_Click(object sender, EventArgs e)
        {
            if (null == this.ly_sales_clientDataGridView.CurrentRow)
            {
                return;
            }

            int all_count;

            if (!string.IsNullOrEmpty(this.ly_sales_clientDataGridView.CurrentRow.Cells ["业务总数"].Value .ToString ()))
            {
                all_count = int.Parse(this.ly_sales_clientDataGridView.CurrentRow.Cells["业务总数"].Value.ToString());
            }
            else
            {
                all_count = 0;
            }

            if (0 < all_count)
            {
                MessageBox.Show("当前客户已有业务记录,不能删除数据...", "注意");
                return;
            }
            
            string message = "确定删除当前客户记录吗？";
            string caption = "提示...";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result;



            result = MessageBox.Show(message, caption, buttons,
            MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

            if (result == DialogResult.Yes)
            {

                this.ly_sales_client_receivablesBindingSource.RemoveCurrent();


                ly_sales_clientDataGridView.EndEdit();
                ly_sales_client_receivablesBindingSource.EndEdit();


                this.ly_sales_client_receivablesTableAdapter.Update(this.lYSalseMange.ly_sales_client_receivables);
            }
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            ExportDataGridviewTOExcell.ExportDataGridview(this.ly_sales_clientDataGridView, true);
        }

       

       
    }
}
