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
using System.Windows.Forms.DataVisualization.Charting;
using HappyYF.Infrastructure.Repositories;
using DataGridFilter;
using System.Threading;


 namespace HappyYF.YuanXin.WorkSet
{
    public partial class LY_SalseContract_StandardSum : Form
    {
        private string nowfilterStr = "";
        private string nowusercode = "";
        private string nowfillstragecode = "full";

        private DataSet ds;
        BindingSource bindingSource2;

        private DataSet ds3;
        BindingSource bindingSource3;

        private DataSet ds5;
        BindingSource bindingSource5;

        private DataSet ds6;
        BindingSource bindingSource6;


        private DataSet ds2;

        private DataSet ds7;
        BindingSource bindingSource7;

        private DataSet ds8;
        BindingSource bindingSource8;



        public LY_SalseContract_StandardSum()
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

            

          
            this.nowusercode = SQLDatabase.NowUserID;

            this.comboBox1.SelectedIndex = 0;

            //this.ly_sales_standard_Report_giveTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            this.ly_sales_standard_SumTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;



            this.dateTimePicker3.Text = SQLDatabase.GetNowdate().AddYears(-1).Date.ToString();
            this.dateTimePicker4.Text = SQLDatabase.GetNowdate().AddDays(0).Date.ToString();


            //this.dateTimePicker1.Text = SQLDatabase.GetNowdate().AddYears(-3).Date.Year.ToString() + "-12" + "-26";
            this.dateTimePicker1.Text = SQLDatabase.GetNowdate().Date.Year.ToString() + "-01" + "-01";
            this.dateTimePicker2.Text = SQLDatabase.GetNowdate().AddDays(0).Date.ToString();

            //this.dateTimePicker5.Text = SQLDatabase.GetNowdate().AddYears(-1).Date.Year.ToString() + "-12" + "-26";
            this.dateTimePicker5.Text = SQLDatabase.GetNowdate().Date.Year.ToString() + "-01" + "-01";
            this.dateTimePicker6.Text = SQLDatabase.GetNowdate().AddDays(0).Date.ToString();


            //this.dateTimePicker7.Text = SQLDatabase.GetNowdate().AddYears(-3).Date.Year.ToString() + "-12" + "-26";
            this.dateTimePicker7.Text = SQLDatabase.GetNowdate().Date.Year.ToString() + "-01" + "-01";
            this.dateTimePicker8.Text = SQLDatabase.GetNowdate().AddDays(0).Date.ToString();


            this.dateTimePicker9.Text = SQLDatabase.GetNowdate().AddYears(-3).Date.Year.ToString() + "-01" + "-01";
            this.dateTimePicker10.Text = SQLDatabase.GetNowdate().AddDays(0).Date.ToString();

            this.dateTimePicker11.Text = SQLDatabase.GetNowdate().AddYears(-3).Date.Year.ToString() + "-01" + "-01";
            this.dateTimePicker12.Text = SQLDatabase.GetNowdate().AddDays(0).Date.ToString();

            this.dateTimePicker18.Text = SQLDatabase.GetNowdate().AddYears(-3).Date.Year.ToString() + "-01" + "-01";
            this.dateTimePicker19.Text = SQLDatabase.GetNowdate().AddDays(0).Date.ToString();


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

            if (SQLDatabase.CheckHaveRight(SQLDatabase.NowUserID, "营业综合信息") )
            {
                //this.treeView1.Visible = true;
                this.splitContainer1.Panel1Collapsed = false;
            }
            else if ( SQLDatabase.CheckHaveRight(SQLDatabase.NowUserID, "营业区域信息"))
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


            this.tabPage1.Parent = null;
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

            //this.ly_sales_standard_Report_giveTableAdapter.Fill(this.lYSalseMange.ly_sales_borrow_detail_all, this.nowusercode, this.nowfillstragecode, this.dateTimePicker1.Value, this.dateTimePicker2.Value);
            //this.ly_sales_standard_Report_giveTableAdapter.Fill(this.lYSalseMange2.ly_sales_standard_Report_give, this.nowusercode, this.nowfillstragecode, this.dateTimePicker1.Value, this.dateTimePicker2.Value.AddDays(1));
            //AddSummationRow_New(ly_sales_standard_Report_giveBindingSource, ly_sales_standard_Report_giveDataGridView);

            if (0 == this.tabControl1.SelectedIndex)
            {

                this.ly_sales_standard_SumTableAdapter.Fill(this.lYSalseRepair.ly_sales_standard_Sum, this.nowusercode, this.nowfillstragecode, this.dateTimePicker3.Value, this.dateTimePicker4.Value.AddDays(1));
                SetDGVFir(this.ly_sales_standard_SumDataGridView);
            }

            if (1 == this.tabControl1.SelectedIndex)
            {

                NewFrm.Show(this);


                CreateSeriesCom();
                LoadMonthdataCom();
                NewFrm.Hide(this);
            }

          
            // AddSummationRow_New(ly_sales_standard_SumBindingSource, ly_sales_standard_SumDataGridView);

         
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
            sumdr["清单号"] = "合计";
            sumdr["客户"] = "";
            sumdr["税务"] = "";
            ((DataSet)bs.DataSource).Tables[bs.DataMember].Rows.Add(sumdr);
            bs.ResetBindings(true);

        }

       

        private void toolStripButton16_Click_1(object sender, EventArgs e)
        {
            ExportDataGridviewTOExcell.ExportDataGridview(this.dataGridView1, true);
           
        }

        private void SetDGV7(DataGridView nowDGV)
        {
            //nowDGV.Columns[0].Visible = false;

            for (int i = 0; i < nowDGV.Columns.Count; i++)
            {
               // nowDGV.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;



                //nowDGV.Columns[i].ValueType System .Type .nowDGV.Columns[5].DefaultCellStyle.Alignment

                if ("Decimal" == nowDGV.Columns[i].ValueType.Name)
                {
                    nowDGV.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

                }

                //if (nowDGV.Columns[i].HeaderText.Contains("总计") || nowDGV.Columns[i].HeaderText.Contains("增长率"))
                //{
                //    nowDGV.Columns[i].DefaultCellStyle.BackColor = Color.SkyBlue;
                //    nowDGV.Columns[i].DefaultCellStyle.ForeColor = Color.Red;
                //}

                if (nowDGV.Columns[i].HeaderText.Contains("Sum") || nowDGV.Columns[i].HeaderText.Contains("合计"))
                {
                    nowDGV.Columns[i].DefaultCellStyle.BackColor = Color.Teal;
                    nowDGV.Columns[i].DefaultCellStyle.ForeColor = Color.White;
                }



            }



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
        private void SetDGV(DataGridView nowDGV)
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

                if (nowDGV.Columns[i].HeaderText.Contains("总计") || nowDGV.Columns[i].HeaderText.Contains("增长率"))
                {
                    nowDGV.Columns[i].DefaultCellStyle.BackColor = Color.SkyBlue;
                    nowDGV.Columns[i].DefaultCellStyle.ForeColor = Color.Red  ;
                }

                if (nowDGV.Columns[i].HeaderText.Contains("总计区域") || nowDGV.Columns[i].HeaderText.Contains("合计"))
                {
                    nowDGV.Columns[i].DefaultCellStyle.BackColor = Color.Teal;
                    nowDGV.Columns[i].DefaultCellStyle.ForeColor  = Color.White ;
                }



            }

            string nowreigon;

            if (nowDGV.RowCount >= 2)
            {

                nowreigon = nowDGV.Rows[0].Cells["区域"].Value.ToString();
                for (int i = 1; i < nowDGV.RowCount; i++)
                {
                   

                    if (nowDGV.Rows[i].Cells["区域"].Value.ToString() == nowreigon)
                    {

                        for (int j = 0; j < nowDGV.Columns.Count; j++)
                        {
                            if (nowDGV.Columns[j].HeaderText.Contains("区域"))
                            {


                                nowDGV.Rows[i].Cells[j].Value = DBNull.Value;


                            }
                        }
                    }
                    else
                    {
                        nowreigon = nowDGV.Rows[i].Cells["区域"].Value.ToString();
                    }



                }
            }

        }

        private void toolStripButton21_Click_1(object sender, EventArgs e)
        {


            //SqlDataAdapter myAdapter = new SqlDataAdapter(sel, SQLDatabase.Connectstring);

            SqlDataAdapter myAdapter1 = new SqlDataAdapter();

            myAdapter1.SelectCommand = new SqlCommand();

            myAdapter1.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter1.SelectCommand.CommandText = "ly_sales_standard_Sumregion";
            myAdapter1.SelectCommand.Connection = new SqlConnection(SQLDatabase.Connectstring);
            myAdapter1.SelectCommand.CommandTimeout = 0;

            myAdapter1.SelectCommand.Parameters.Add("@begindate", SqlDbType.DateTime);
            myAdapter1.SelectCommand.Parameters["@begindate"].Value = this.dateTimePicker1.Value;

            myAdapter1.SelectCommand.Parameters.Add("@enddate", SqlDbType.DateTime);
            myAdapter1.SelectCommand.Parameters["@enddate"].Value = this.dateTimePicker2.Value.AddDays(1);




            if (null != ds)
                ds.Dispose();

            ds = new DataSet();

            myAdapter1.Fill(ds);


            myAdapter1.Dispose();

            //this.dataGridView1.DataSource = allData;


            this.dataGridView1.Columns.Clear();

            this.dataGridView1.AutoGenerateColumns = true;
            dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCellsExceptHeaders;

            //BindingSource bindingSource1 = new BindingSource();

            if (null != bindingSource2)
                this.bindingSource2.Dispose();

            this.bindingSource2 = new BindingSource();


            bindingSource2.DataSource = ds.Tables[0];
            dataGridView1.DataSource = bindingSource2;

           this.bindingNavigator2.BindingSource = bindingSource2;

            bindingSource2.ResetBindings(true);


            SetDGV(this.dataGridView1);

            
            
            
            
            
            //this.ly_sales_standard_Report_giveTableAdapter.Fill(this.lYSalseMange2.ly_sales_standard_Report_give, this.nowusercode, this.nowfillstragecode, this.dateTimePicker1.Value, this.dateTimePicker2.Value.AddDays(1));
            //AddSummationRow_New(ly_sales_standard_Report_giveBindingSource, ly_sales_standard_Report_giveDataGridView);

        }

        private void toolStripTextBox4_KeyUp(object sender, KeyEventArgs e)
        {
            string filterString;


            //filterString = GetDataGridviewMultiFilterString.DGVMultiFilterString(this.ly_sales_standard_Report_giveDataGridView, this.toolStripTextBox4.Text);


            //this.ly_sales_standard_Report_giveBindingSource.Filter = filterString;
        }

       

        private void toolStripTextBox4_Enter(object sender, EventArgs e)
        {
            toolStripTextBox4.Text = "";

            //this.ly_sales_standard_Report_giveBindingSource.Filter = "";
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            ExportDataGridviewTOExcell.ExportDataGridview(this.ly_sales_standard_SumDataGridView, true);
        }

        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            this.ly_sales_standard_SumTableAdapter.Fill(this.lYSalseRepair.ly_sales_standard_Sum, this.nowusercode, this.nowfillstragecode, this.dateTimePicker3.Value, this.dateTimePicker4.Value.AddDays(1));

            SetDGVFir(this.ly_sales_standard_SumDataGridView); 
            // AddSummationRow_New(ly_sales_standard_SumBindingSource, ly_sales_standard_SumDataGridView);


        }

        private void ly_sales_contract_standard_ReportDataGridView_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            //string isgood = "yes";

            //decimal nowmoney;

           

            //foreach (DataGridViewRow dgr in ly_sales_contract_standard_ReportDataGridView.Rows)
            //{


            //    if ("" != dgr.Cells["金额"].Value.ToString())
            //    {
            //        nowmoney = decimal.Parse(dgr.Cells["金额"].Value.ToString());
            //    }
            //    else
            //    {
            //        nowmoney = 0;
            //    }







            //    if (nowmoney <= 0)
            //        {
            //            foreach (DataGridViewCell dgc in dgr.Cells)
            //            {

            //                dgc.Style.BackColor = Color.White;
            //                dgc.Style.ForeColor = Color.Red;
            //            }
            //        }
              
            //    //else
            //    //{ 


            //    //}



            //}
        }

        private void toolStripButton7_Click(object sender, EventArgs e)
        {
            if (null == this.ly_sales_standard_SumDataGridView.CurrentRow) return;

            FilterForm filterForm = new FilterForm();


            

            //SumQueryDataSet qds;
            //qds = new SumQueryDataSet();

            List<string> ls = new List<string>();
            ls.Add("id");


            filterForm.SetSourceColumns(this .lYSalseRepair.ly_sales_standard_Sum.Columns, ls);

            filterForm.ShowDialog();

            this.ly_sales_standard_SumBindingSource.Filter =  filterForm.GetFilterString();
            //AddSummationRow_New(ly_sales_contract_standard_ReportBindingSource, ly_sales_contract_standard_ReportDataGridView);

        }

        private void toolStripButton8_Click(object sender, EventArgs e)
        {
            if (null == this.ly_sales_standard_SumDataGridView.CurrentRow) return;
            SortForm DataSort = new SortForm();

            List<string> ls = new List<string>();
            ls.Add("id");


            DataSort.SetSortColumns(this.lYSalseRepair.ly_sales_standard_Sum.Columns, ls);
            DataSort.ShowDialog();
            this.ly_sales_standard_SumBindingSource.Sort = DataSort.GetSortString();
        }

        private void toolStripTextBox2_KeyUp(object sender, KeyEventArgs e)
        {
            string filterString;


            filterString = GetDataGridviewMultiFilterString.DGVMultiFilterString(this.ly_sales_standard_SumDataGridView, this.toolStripTextBox2.Text);


            this.ly_sales_standard_SumBindingSource.Filter = filterString ;
            //AddSummationRow_New(ly_sales_contract_standard_ReportBindingSource, ly_sales_contract_standard_ReportDataGridView);
        }

        private void toolStripTextBox2_Enter(object sender, EventArgs e)
        {
            toolStripTextBox2.Text = "";

            this.ly_sales_standard_SumBindingSource.Filter = "";
        }

        private void ly_sales_contract_standard_ReportDataGridView_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            //DataGridView dgv = sender as DataGridView;

            //if (null == dgv.CurrentRow) return;

            //string nowcontract_code = dgv.CurrentRow.Cells["清单号"].Value.ToString();

            /////////////////////////////////////////////////////////////////

            //if ("开票日期" == dgv.CurrentCell.OwningColumn.Name)
            //{

            //    if ("XS" != nowcontract_code.Substring(0, 2))
            //    {

            //        MessageBox.Show("只能修改合同开票日期...", "注意");
            //        return;
                
            //    }

            //    ChangeValue queryForm = new ChangeValue();

            //    queryForm.OldValue = dgv.CurrentCell.Value.ToString();
            //    queryForm.NewValue = "";
            //    queryForm.ChangeMode = "datetime";
            //    queryForm.ShowDialog();


                
            //    string updstr;

            //    if (queryForm.NewValue != "")
            //    {
            //        dgv.CurrentRow.Cells["开票日期"].Value = queryForm.NewValue;
            //        //dgv.CurrentRow.Cells["开票人"].Value = "宋美彰";
            //     updstr = " update ly_sales_contract_main  " +
            //                    "  set invoice_date=  '" + queryForm.NewValue + "',invoice_people='宋美彰' where  contract_inner_code='" + nowcontract_code + "'";



            //    }
            //    else
            //    {

            //        dgv.CurrentRow.Cells["开票日期"].Value = DBNull.Value;
            //        //dgv.CurrentRow.Cells["开票人"].Value = DBNull.Value;
            //         updstr = " update ly_sales_contract_main  " +
            //                    "  set invoice_date= null, invoice_people=null where  contract_inner_code='" + nowcontract_code + "'";


            //    }


            //    ///////////////////

               


            //            SqlConnection sqlConnection1 = new SqlConnection(SQLDatabase.Connectstring);
            //            SqlCommand cmd = new SqlCommand();

            //            cmd.CommandText = updstr;
            //            cmd.CommandType = CommandType.Text;
            //            cmd.Connection = sqlConnection1;

            //            int temp = 0;

            //            using (TransactionScope scope = new TransactionScope())
            //            {

            //                sqlConnection1.Open();
            //                try
            //                {

            //                    cmd.ExecuteNonQuery();



            //                    scope.Complete();



            //                }
            //                catch (SqlException sqle)
            //                {


            //                    MessageBox.Show(sqle.Message.Split('*')[0]);
            //                }


            //                finally
            //                {
            //                    sqlConnection1.Close();


            //                }
            //            }

                  

            //    //////////////////////////////



            //    return;

            //}
            //////////////////////////////////////////////////////////////////////////
        }

        private void toolStripButton9_Click(object sender, EventArgs e)
        {
            //this.ly_sales_contract_standard_ReportBindingSource.Filter = " 公司='中原' or 清单号='合计'";
            //AddSummationRow_New(ly_sales_contract_standard_ReportBindingSource, ly_sales_contract_standard_ReportDataGridView);

        }

        private void toolStripButton10_Click(object sender, EventArgs e)
        {

        }

        private void toolStripButton11_Click(object sender, EventArgs e)
        {
            //this.ly_sales_contract_standard_ReportBindingSource.Filter = "";
            //AddSummationRow_New(ly_sales_contract_standard_ReportBindingSource, ly_sales_contract_standard_ReportDataGridView);

        }

        private void toolStripButton10_Click_1(object sender, EventArgs e)
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

        private void toolStripButton15_Click(object sender, EventArgs e)
        {

            LoadMonthdata();
            CreateSeries();
           ////////////////////////////////////////////////

           // ChartArea chartArea1 = new ChartArea();

           // this.chart2.ChartAreas.Add(chartArea1);

  
           //Series series1 = new Series();
           

           //this.chart2.Series.Add(series1);


          
           //DataView dv = new DataView(ds3.Tables[0]);

           //this.chart2.Series[0].Points.DataBindXY(dv, "营业员", dv, "总计");




        }

        private void LoadMonthdata()
        {
            if (this.radioButton6.Checked)
            {

                TableForMonth();
            }

            if (this.radioButton5.Checked)
            {
                TableForQuarter(); ;
            }
        }



        private void TableForMonth()
        {
            string nowstyle = "全部";

            if (this.radioButton1.Checked)
            {

                nowstyle = "全部";
                // this.chart2.Titles[0].Text = "营业部销售(合同+维修)统计图";
            }
            else if (this.radioButton2.Checked)
            {

                nowstyle = "合同";
                //this.chart2.Titles[0].Text = "营业部销售(合同)统计图";
            }

            else if (this.radioButton3.Checked)
            {
                //this.chart2.Titles[0].Text = "营业部销售(维修)统计图";
                nowstyle = "维修";
            }

            SqlDataAdapter myAdapter1 = new SqlDataAdapter();

            myAdapter1.SelectCommand = new SqlCommand();

            myAdapter1.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter1.SelectCommand.CommandText = "ly_sales_standard_SumMonth";
            myAdapter1.SelectCommand.Connection = new SqlConnection(SQLDatabase.Connectstring);
            myAdapter1.SelectCommand.CommandTimeout = 0;

            myAdapter1.SelectCommand.Parameters.Add("@begindate", SqlDbType.DateTime);
            myAdapter1.SelectCommand.Parameters["@begindate"].Value = this.dateTimePicker5.Value;

            myAdapter1.SelectCommand.Parameters.Add("@enddate", SqlDbType.DateTime);
            myAdapter1.SelectCommand.Parameters["@enddate"].Value = this.dateTimePicker6.Value.AddDays(1);

            myAdapter1.SelectCommand.Parameters.Add("@nowstyle", SqlDbType.VarChar);
            myAdapter1.SelectCommand.Parameters["@nowstyle"].Value = nowstyle;




            if (null != ds3)
                ds3.Dispose();

            ds3 = new DataSet();

            myAdapter1.Fill(ds3);


            myAdapter1.Dispose();

            //this.dataGridView1.DataSource = allData;


            this.dataGridView2.Columns.Clear();

            this.dataGridView2.AutoGenerateColumns = true;
            dataGridView2.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCellsExceptHeaders;

            //BindingSource bindingSource1 = new BindingSource();

            if (null != bindingSource3)
                this.bindingSource3.Dispose();

            this.bindingSource3 = new BindingSource();


            bindingSource3.DataSource = ds3.Tables[0];
            dataGridView2.DataSource = bindingSource3;

            this.bindingNavigator3.BindingSource = bindingSource3;

            bindingSource3.ResetBindings(true);


            SetDGV(this.dataGridView2);
        }

        private void TableForQuarter()
        {
            string nowstyle = "全部";

            if (this.radioButton1.Checked)
            {

                nowstyle = "全部";
                // this.chart2.Titles[0].Text = "营业部销售(合同+维修)统计图";
            }
            else if (this.radioButton2.Checked)
            {

                nowstyle = "合同";
                //this.chart2.Titles[0].Text = "营业部销售(合同)统计图";
            }

            else if (this.radioButton3.Checked)
            {
                //this.chart2.Titles[0].Text = "营业部销售(维修)统计图";
                nowstyle = "维修";
            }

            SqlDataAdapter myAdapter1 = new SqlDataAdapter();

            myAdapter1.SelectCommand = new SqlCommand();

            myAdapter1.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter1.SelectCommand.CommandText = "ly_sales_standard_SumQuarter";
            myAdapter1.SelectCommand.Connection = new SqlConnection(SQLDatabase.Connectstring);
            myAdapter1.SelectCommand.CommandTimeout = 0;

            myAdapter1.SelectCommand.Parameters.Add("@begindate", SqlDbType.DateTime);
            myAdapter1.SelectCommand.Parameters["@begindate"].Value = this.dateTimePicker5.Value;

            myAdapter1.SelectCommand.Parameters.Add("@enddate", SqlDbType.DateTime);
            myAdapter1.SelectCommand.Parameters["@enddate"].Value = this.dateTimePicker6.Value.AddDays(1);

            myAdapter1.SelectCommand.Parameters.Add("@nowstyle", SqlDbType.VarChar);
            myAdapter1.SelectCommand.Parameters["@nowstyle"].Value = nowstyle;




            if (null != ds3)
                ds3.Dispose();

            ds3 = new DataSet();

            myAdapter1.Fill(ds3);


            myAdapter1.Dispose();

            //this.dataGridView1.DataSource = allData;


            this.dataGridView2.Columns.Clear();

            this.dataGridView2.AutoGenerateColumns = true;
            dataGridView2.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCellsExceptHeaders;

            //BindingSource bindingSource1 = new BindingSource();

            if (null != bindingSource3)
                this.bindingSource3.Dispose();

            this.bindingSource3 = new BindingSource();


            bindingSource3.DataSource = ds3.Tables[0];
            dataGridView2.DataSource = bindingSource3;

            this.bindingNavigator3.BindingSource = bindingSource3;

            bindingSource3.ResetBindings(true);


            SetDGV(this.dataGridView2);
        }

        private void button1_Click(object sender, EventArgs e)
        {

            CreateSeries();
            LoadMonthdata();
        }

        private void CreateSeries()
        {
            if (this.radioButton6.Checked)
            {
                ChartFormonth();
            }

            if (this.radioButton5.Checked)
            {
                ChartForquarter();
            }
            
        }

        private void ChartFormonth()
        {
            NewFrm.Show(this);

            chart2.Series.Clear();







            string sqlStr = "SELECT  yhbm, yhmc FROM T_users WHERE (bumen = '000301')  union select '999','汇总' ";

            string nowstyle = "全部";

            if (this.radioButton1.Checked)
            {

                nowstyle = "全部";
                this.chart2.Titles[0].Text = "营业部销售(合同+维修)月份统计图";
            }
            else if (this.radioButton2.Checked)
            {

                nowstyle = "合同";
                this.chart2.Titles[0].Text = "营业部销售(合同)月份统计图";
            }

            else if (this.radioButton3.Checked)
            {
                this.chart2.Titles[0].Text = "营业部销售(维修)月份统计图";
                nowstyle = "维修";
            }




            using (SqlConnection conn = new SqlConnection(SQLDatabase.Connectstring))
            {
                using (SqlCommand cmd = new SqlCommand(sqlStr, conn))
                {

                    conn.Open();
                    SqlDataReader sdr = cmd.ExecuteReader();

                    string nowName;

                    while (sdr.Read())
                    {

                        //UserInfo userInfo = new UserInfo();

                        //userInfo.ID = (Guid)sdr["ID"];

                        //userInfo.LoginName = sdr["LoginName"].ToString();

                        //userInfo.LoginPwd = sdr["LoginPwd"].ToString();
                        nowName = sdr["yhmc"].ToString();


                        //////////////////////////////@nowstyle
                        SqlDataAdapter myAdapter = new SqlDataAdapter();

                        myAdapter.SelectCommand = new SqlCommand();

                        myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                        myAdapter.SelectCommand.CommandText = "ly_sales_standard_ForChart";
                        myAdapter.SelectCommand.Connection = new SqlConnection(SQLDatabase.Connectstring);
                        myAdapter.SelectCommand.CommandTimeout = 0;

                        myAdapter.SelectCommand.Parameters.Add("@begindate", SqlDbType.DateTime);
                        myAdapter.SelectCommand.Parameters["@begindate"].Value = this.dateTimePicker5.Value;

                        myAdapter.SelectCommand.Parameters.Add("@enddate", SqlDbType.DateTime);
                        myAdapter.SelectCommand.Parameters["@enddate"].Value = this.dateTimePicker6.Value.AddDays(1);

                        myAdapter.SelectCommand.Parameters.Add("@nowsalesperson", SqlDbType.VarChar);
                        myAdapter.SelectCommand.Parameters["@nowsalesperson"].Value = nowName;

                        myAdapter.SelectCommand.Parameters.Add("@nowstyle", SqlDbType.VarChar);
                        myAdapter.SelectCommand.Parameters["@nowstyle"].Value = nowstyle;


                        if (null != ds2)
                            ds2.Dispose();

                        ds2 = new DataSet();

                        myAdapter.Fill(ds2);


                        myAdapter.Dispose();

                        DataView dv = new DataView(ds2.Tables[0]);

                        //this .chart1 .ChartAreas[0].
                        //this .chart1.Titles .
                        Series tempseries = new Series();

                        tempseries.Name = nowName;

                        //this.chart2.ChartAreas[0].Area3DStyle.Enable3D = true;


                        //this.chart2.Series[0].ChartType = SeriesChartType.Column;
                        tempseries.ChartType = SeriesChartType.Column;
                        tempseries["DrawingStyle"] = "Cylinder";

                        tempseries.LegendText = nowName;

                        //tempseries.Color = Color.Cyan;
                        tempseries["PixelPointWidth"] = "90";

                        //tempseries.ToolTip = nowName + tempseries.Label;
                        tempseries.IsValueShownAsLabel = true;

                        if ("汇总" == nowName)
                        {
                            tempseries["LabelStyle"] = "Bottom";

                            tempseries.ChartArea = "ChartArea2";
                            tempseries.LabelForeColor = Color.Blue;
                            tempseries.LabelBackColor = Color.Transparent;
                        }
                        else
                        {
                            tempseries.ChartArea = "ChartArea1";
                            tempseries.IsValueShownAsLabel = false;
                        }
                        tempseries.Points.DataBindXY(dv, "月份", dv, "金额");


                        chart2.Series.Add(tempseries);



                    }
                    sdr.Close();


                    ///////////////////////////////
                }
            }

            chart2.ChartAreas["ChartArea1"].RecalculateAxesScale();
            chart2.ChartAreas["ChartArea2"].RecalculateAxesScale();



            NewFrm.Hide(this);
        }

        private void ChartForquarter()
        {
            NewFrm.Show(this);

            chart2.Series.Clear();







            string sqlStr = "SELECT  yhbm, yhmc FROM T_users WHERE (bumen = '000301')  union select '999','汇总' ";

            string nowstyle = "全部";

            if (this.radioButton1.Checked)
            {

                nowstyle = "全部";
                this.chart2.Titles[0].Text = "营业部销售(合同+维修)季度统计图";
            }
            else if (this.radioButton2.Checked)
            {

                nowstyle = "合同";
                this.chart2.Titles[0].Text = "营业部销售(合同)季度统计图";
            }

            else if (this.radioButton3.Checked)
            {
                this.chart2.Titles[0].Text = "营业部销售(维修)季度统计图";
                nowstyle = "维修";
            }




            using (SqlConnection conn = new SqlConnection(SQLDatabase.Connectstring))
            {
                using (SqlCommand cmd = new SqlCommand(sqlStr, conn))
                {

                    conn.Open();
                    SqlDataReader sdr = cmd.ExecuteReader();

                    string nowName;

                    while (sdr.Read())
                    {

                        //UserInfo userInfo = new UserInfo();

                        //userInfo.ID = (Guid)sdr["ID"];

                        //userInfo.LoginName = sdr["LoginName"].ToString();

                        //userInfo.LoginPwd = sdr["LoginPwd"].ToString();
                        nowName = sdr["yhmc"].ToString();


                        //////////////////////////////@nowstyle
                        SqlDataAdapter myAdapter = new SqlDataAdapter();

                        myAdapter.SelectCommand = new SqlCommand();

                        myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                        myAdapter.SelectCommand.CommandText = "ly_sales_standard_ForChartQuarter";
                        myAdapter.SelectCommand.Connection = new SqlConnection(SQLDatabase.Connectstring);
                        myAdapter.SelectCommand.CommandTimeout = 0;

                        myAdapter.SelectCommand.Parameters.Add("@begindate", SqlDbType.DateTime);
                        myAdapter.SelectCommand.Parameters["@begindate"].Value = this.dateTimePicker5.Value;

                        myAdapter.SelectCommand.Parameters.Add("@enddate", SqlDbType.DateTime);
                        myAdapter.SelectCommand.Parameters["@enddate"].Value = this.dateTimePicker6.Value.AddDays(1);

                        myAdapter.SelectCommand.Parameters.Add("@nowsalesperson", SqlDbType.VarChar);
                        myAdapter.SelectCommand.Parameters["@nowsalesperson"].Value = nowName;

                        myAdapter.SelectCommand.Parameters.Add("@nowstyle", SqlDbType.VarChar);
                        myAdapter.SelectCommand.Parameters["@nowstyle"].Value = nowstyle;


                        if (null != ds2)
                            ds2.Dispose();

                        ds2 = new DataSet();

                        myAdapter.Fill(ds2);


                        myAdapter.Dispose();

                        DataView dv = new DataView(ds2.Tables[0]);

                        //this .chart1 .ChartAreas[0].
                        //this .chart1.Titles .
                        Series tempseries = new Series();

                        tempseries.Name = nowName;

                        //this.chart2.ChartAreas[0].Area3DStyle.Enable3D = true;


                        //this.chart2.Series[0].ChartType = SeriesChartType.Column;
                        tempseries.ChartType = SeriesChartType.Column;
                        tempseries["DrawingStyle"] = "Cylinder";

                        tempseries.LegendText = nowName;

                        //tempseries.Color = Color.Cyan;
                        tempseries["PixelPointWidth"] = "90";

                        //tempseries.ToolTip = nowName + tempseries.Label;
                        tempseries.IsValueShownAsLabel = true;

                        if ("汇总" == nowName)
                        {
                            tempseries["LabelStyle"] = "Bottom";

                            tempseries.ChartArea = "ChartArea2";
                            tempseries.LabelForeColor = Color.Blue;
                            tempseries.LabelBackColor = Color.Transparent;
                        }
                        else
                        {
                            tempseries.ChartArea = "ChartArea1";
                            tempseries.IsValueShownAsLabel = false;
                        }
                        tempseries.Points.DataBindXY(dv, "季度", dv, "金额");


                        chart2.Series.Add(tempseries);



                    }
                    sdr.Close();


                    ///////////////////////////////
                }
            }

            chart2.ChartAreas["ChartArea1"].RecalculateAxesScale();
            chart2.ChartAreas["ChartArea2"].RecalculateAxesScale();



            NewFrm.Hide(this);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox nowcom = sender as ComboBox;

            if (null == this.chart2.Series) return;

            if ("曲线"==nowcom .Text)
            {
                //this.chart2.Series[0].ChartType = SeriesChartType.Spline;

                foreach (Series ses in this.chart2.Series)
                {

                    ses.ChartType = SeriesChartType.Spline;
                }
            }
            else if ("柱状" == nowcom.Text)
            {
                //this.chart2.Series[0].ChartType = SeriesChartType.Column;
                foreach (Series ses in this.chart2.Series)
                {

                    ses.ChartType = SeriesChartType.Column;
                }
            }
            else
            {

            }

        }

        private void chart2_GetToolTipText(object sender, ToolTipEventArgs e)
        {
            switch (e.HitTestResult.ChartElementType)
            {
                case ChartElementType.Axis:
                    e.Text = e.HitTestResult.Axis.Name;
                    break;
                //case ChartElementType..SBLargeDecrement:
                //    e.Text = "A scrollbar large decrement button";
                //    break;
                //case ChartElementType.SBLargeIncrement:
                //    e.Text = "A scrollbar large increment button";
                //    break;
                //case ChartElementType.SBSmallDecrement:
                //    e.Text = "A scrollbar small decrement button";
                //    break;
                //case ChartElementType.SBSmallIncrement:
                //    e.Text = "A scrollbar small increment button";
                //    break;
                //case ChartElementType.SBThumbTracker:
                //    e.Text = "A scrollbar tracking thumb";
                //    break;
                //case ChartElementType.SBZoomReset:
                //    e.Text = "The ZoomReset button of a scrollbar";
                //    break;((System.Windows.Forms.DataVisualization.Charting.DataPoint)(e.HitTestResult.Object)).YValues[0]
                case ChartElementType.DataPoint:
                    e.Text = e.HitTestResult.Series.Name + ":" + ((DataPoint )e.HitTestResult.Object).YValues[0].ToString();
                    break;
                //case ChartElementType.GridLines:
                //    e.Text = "Grid Lines";
                //    break;
                case ChartElementType.LegendArea:
                    e.Text = "图例";
                    break;
                case ChartElementType.LegendItem:
                    e.Text = "Legend Item";
                    break;
                case ChartElementType.PlottingArea:
                    e.Text = "";
                    break;
                case ChartElementType.StripLines:
                    e.Text = "Strip Lines";
                    break;
                case ChartElementType.TickMarks:
                    e.Text = "节点";
                    break;
                case ChartElementType.Title:
                    e.Text = "标题";
                    break;
            }

        }

        private void chart2_CursorPositionChanged(object sender, CursorEventArgs e)
        {

        }

        private void chart2_CursorPositionChanging(object sender, CursorEventArgs e)
        {
            //SetPosition(e.Axis, e.NewPosition);

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton nowradio = sender as RadioButton;

            if (nowradio.Checked)
            {

                CreateSeries();
                LoadMonthdata();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            NewFrm.Show(this);

            
            CreateSeriesCom();
            LoadMonthdataCom();
            NewFrm.Hide(this);
        }

        private void CreateSeriesCom()
        {
            if (this.radioButton6.Checked)
            {
                ChartFormonthCom();
            }

            //if (this.radioButton5.Checked)
            //{
            //    ChartForquarter();
            //}

        }

        private void ChartFormonthCom()
        {
            //NewFrm.Show(this);

            chart3.Series.Clear();


            string sqlStr="";


            if ("full" == this.nowfillstragecode)
            {

                sqlStr = " select   distinct 	cast(a.approve_year as varchar(5)) +'_'+a.salesregion_code + a.salesregion_name as 区域 " +
                                 " FROM ly_sales_contract_detail_View  AS a  " +
                                 " where   ( a.approve = '已执行') and a.give_flag= '销售' and  (approve_date >='" + this.dateTimePicker7.Text + "') AND (approve_date < '" + this.dateTimePicker8.Value.AddDays(1).ToString("yyyy-MM-dd") + "') " +
                                 " union all " +
                                 " select  distinct 	cast(a.approve_year as varchar(5)) +'_汇总' as 区域 " +
                                 " FROM ly_sales_contract_detail_View  AS a  " +
                                 " where   ( a.approve = '已执行') and a.give_flag= '销售' and  (approve_date >='" + this.dateTimePicker7.Text + "') AND (approve_date < '" + this.dateTimePicker8.Value.AddDays(1).ToString("yyyy-MM-dd") + "') " +
                                 " order by 区域 ";
            }
            if ("region" == this.nowfillstragecode)
            {

                sqlStr = " select   distinct 	cast(a.approve_year as varchar(5)) +'_'+a.salesregion_code + a.salesregion_name as 区域 " +
                                 " FROM ly_sales_contract_detail_View  AS a  " +
                                 " where salesregion_code='" + this.nowusercode + "' and  ( a.approve = '已执行') and a.give_flag= '销售' and  (approve_date >='" + this.dateTimePicker7.Text + "') AND (approve_date < '" + this.dateTimePicker8.Value.AddDays(1).ToString("yyyy-MM-dd") + "') " +
                                 " union all " +
                                 " select 	salesregion_name +'_汇总' as 区域 " +
                                 " FROM ly_salesregion  AS a  " +
                                 " where   ( a.salesregion_code ='" + this.nowusercode + "')  " +
                                 " order by 区域 ";
            }

            if ("single" == this.nowfillstragecode)
            {

                sqlStr = " select   distinct 	cast(a.approve_year as varchar(5)) +'_'+a.salesperson_code + a.salesperson_name as 区域 " +
                                 " FROM ly_sales_contract_detail_View  AS a  " +
                                 " where salesperson_code='" + this.nowusercode + "' and  ( a.approve = '已执行') and a.give_flag= '销售' and  (approve_date >='" + this.dateTimePicker7.Text + "') AND (approve_date < '" + this.dateTimePicker8.Value.AddDays(1).ToString("yyyy-MM-dd") + "') " +
                                 " union all " +
                                 " select 	yhmc +'_汇总' as 区域 " +
                                 " FROM T_users  AS a  " +
                                 " where   ( a.yhbm ='" + this.nowusercode + "')  " +
                                 " order by 区域 ";
            }
           
            
            string nowstyle = "全部";

            if (this.radioButton9.Checked)
            {

                nowstyle = "全部";
                this.chart3.Titles[0].Text = "销售(合同+维修)月份对比统计图";
            }
            else if (this.radioButton8.Checked)
            {

                nowstyle = "合同";
                this.chart3.Titles[0].Text = "销售(合同)月份对比统计图";
            }

            else if (this.radioButton7.Checked)
            {
                this.chart3.Titles[0].Text = "销售(维修)月份对比统计图";
                nowstyle = "维修";
            }




            using (SqlConnection conn = new SqlConnection(SQLDatabase.Connectstring))
            {
                using (SqlCommand cmd = new SqlCommand(sqlStr, conn))
                {

                    conn.Open();
                    SqlDataReader sdr = cmd.ExecuteReader();

                    string nowName;

                    while (sdr.Read())
                    {

                        //UserInfo userInfo = new UserInfo();

                        //userInfo.ID = (Guid)sdr["ID"];

                        //userInfo.LoginName = sdr["LoginName"].ToString();

                        //userInfo.LoginPwd = sdr["LoginPwd"].ToString();
                        nowName = sdr["区域"].ToString();


                        //////////////////////////////@nowstyle
                        SqlDataAdapter myAdapter = new SqlDataAdapter();

                        myAdapter.SelectCommand = new SqlCommand();

                        myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                        myAdapter.SelectCommand.CommandText = "ly_sales_standard_SumMoney_Single";
                        myAdapter.SelectCommand.Connection = new SqlConnection(SQLDatabase.Connectstring);
                        myAdapter.SelectCommand.CommandTimeout = 0;

                        myAdapter.SelectCommand.Parameters.Add("@salesperson_code", SqlDbType.VarChar);
                        myAdapter.SelectCommand.Parameters["@salesperson_code"].Value = this.nowusercode;

                        myAdapter.SelectCommand.Parameters.Add("@selcode", SqlDbType.VarChar);
                        myAdapter.SelectCommand.Parameters["@selcode"].Value = this.nowfillstragecode;



                        myAdapter.SelectCommand.Parameters.Add("@begindate", SqlDbType.DateTime);
                        myAdapter.SelectCommand.Parameters["@begindate"].Value = this.dateTimePicker7.Value;

                        myAdapter.SelectCommand.Parameters.Add("@enddate", SqlDbType.DateTime);
                        myAdapter.SelectCommand.Parameters["@enddate"].Value = this.dateTimePicker8.Value.AddDays(1);

                        myAdapter.SelectCommand.Parameters.Add("@nowstyle", SqlDbType.VarChar);
                        myAdapter.SelectCommand.Parameters["@nowstyle"].Value = nowstyle;


                         myAdapter.SelectCommand.Parameters.Add("@nowname", SqlDbType.VarChar);
                         myAdapter.SelectCommand.Parameters["@nowname"].Value = nowName;


                    
         

                        if (null != ds2)
                            ds2.Dispose();

                        ds2 = new DataSet();

                        myAdapter.Fill(ds2);


                        myAdapter.Dispose();

                        if (ds2.Tables.Count < 1)
                        {
                            continue;
                        
                        }

                        DataView dv = new DataView(ds2.Tables[0]);

                        //this .chart1 .ChartAreas[0].
                        //this .chart1.Titles .
                        Series tempseries = new Series();

                        tempseries.Name = nowName;

                        //this.chart2.ChartAreas[0].Area3DStyle.Enable3D = true;


                        //this.chart2.Series[0].ChartType = SeriesChartType.Column;

                        //ses.ChartType = SeriesChartType.Spline;
                        tempseries.ChartType = SeriesChartType.Spline;
                        tempseries["DrawingStyle"] = "Cylinder";

                        tempseries.LegendText = nowName;

                        //tempseries.Color = Color.Cyan;
                        tempseries["PixelPointWidth"] = "20";

                        tempseries.MarkerStyle = MarkerStyle.Circle;
                        tempseries.MarkerSize = 3;
                        tempseries.MarkerColor = Color.Magenta;
                        tempseries.MarkerBorderColor = Color.Red;
                        tempseries.MarkerBorderWidth = 1;

                        //tempseries.ToolTip = nowName + tempseries.Label;
                        tempseries.IsValueShownAsLabel = true;

                        if (nowName.Contains("汇总"))
                        {
                            tempseries["LabelStyle"] = "Bottom";

                            tempseries.ChartArea = "ChartArea2";
                            tempseries.LabelForeColor = Color.Blue;
                            tempseries.LabelBackColor = Color.Transparent;
                        }
                        else
                        {
                            tempseries.ChartArea = "ChartArea1";
                            tempseries.IsValueShownAsLabel = false;
                        }
                        tempseries.Points.DataBindXY(dv, "月份", dv, "金额");


                        chart3.Series.Add(tempseries);



                    }
                    sdr.Close();


                    ///////////////////////////////
                }
            }

            chart3.ChartAreas["ChartArea1"].AxisX.IntervalAutoMode = IntervalAutoMode.VariableCount;    
            chart3.ChartAreas["ChartArea2"].AxisX.IntervalAutoMode = IntervalAutoMode.VariableCount;    

            chart3.ChartAreas["ChartArea1"].RecalculateAxesScale();
            chart3.ChartAreas["ChartArea2"].RecalculateAxesScale();



            //NewFrm.Hide(this);
        }

        private void LoadMonthdataCom()
        {

            TableForMonthCom();
            
            //if (this.radioButton6.Checked)
            //{

            //    TableForMonth();
            //}

            //if (this.radioButton5.Checked)
            //{
            //    TableForQuarter(); ;
            //}
        }

        private void TableForMonthCom()
        {
            string nowstyle = "全部";

            if (this.radioButton9.Checked)
            {

                nowstyle = "全部";
                // this.chart2.Titles[0].Text = "营业部销售(合同+维修)统计图";
            }
            else if (this.radioButton8.Checked)
            {

                nowstyle = "合同";
                //this.chart2.Titles[0].Text = "营业部销售(合同)统计图";
            }

            else if (this.radioButton7.Checked)
            {
                //this.chart2.Titles[0].Text = "营业部销售(维修)统计图";
                nowstyle = "维修";
            }

            SqlDataAdapter myAdapter1 = new SqlDataAdapter();

            myAdapter1.SelectCommand = new SqlCommand();

            myAdapter1.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter1.SelectCommand.CommandText = "ly_sales_standard_SumMoney";
            myAdapter1.SelectCommand.Connection = new SqlConnection(SQLDatabase.Connectstring);
            myAdapter1.SelectCommand.CommandTimeout = 0;

            myAdapter1.SelectCommand.Parameters.Add("@salesperson_code", SqlDbType.VarChar);
            myAdapter1.SelectCommand.Parameters["@salesperson_code"].Value = this.nowusercode;

            myAdapter1.SelectCommand.Parameters.Add("@selcode", SqlDbType.VarChar);
            myAdapter1.SelectCommand.Parameters["@selcode"].Value = this.nowfillstragecode;



            myAdapter1.SelectCommand.Parameters.Add("@begindate", SqlDbType.DateTime);
            myAdapter1.SelectCommand.Parameters["@begindate"].Value = this.dateTimePicker7.Value;

            myAdapter1.SelectCommand.Parameters.Add("@enddate", SqlDbType.DateTime);
            myAdapter1.SelectCommand.Parameters["@enddate"].Value = this.dateTimePicker8.Value.AddDays(1);

            myAdapter1.SelectCommand.Parameters.Add("@nowstyle", SqlDbType.VarChar);
            myAdapter1.SelectCommand.Parameters["@nowstyle"].Value = nowstyle;

         


            if (null != ds5)
                ds5.Dispose();

            ds5 = new DataSet();

            myAdapter1.Fill(ds5);


            myAdapter1.Dispose();

            //this.dataGridView1.DataSource = allData;


            this.dataGridView3.Columns.Clear();

            this.dataGridView3.AutoGenerateColumns = true;
            dataGridView3.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCellsExceptHeaders;

            //BindingSource bindingSource1 = new BindingSource();

            if (null != bindingSource5)
                this.bindingSource5.Dispose();

            this.bindingSource5 = new BindingSource();


            bindingSource5.DataSource = ds5.Tables[0];
            dataGridView3.DataSource = bindingSource5;

            //this.bindingNavigator3.BindingSource = bindingSource3;

            bindingSource5.ResetBindings(true);


            SetDGVCom(this.dataGridView3);
        }

        private void SetDGVCom(DataGridView nowDGV)
        {
            //nowDGV.Columns[0].Visible = false;

            for (int i = 0; i < nowDGV.Columns.Count; i++)
            {
                nowDGV.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;



                //nowDGV.Columns[i].ValueType System .Type .nowDGV.Columns[5].DefaultCellStyle.Alignment

                if ("Decimal" == nowDGV.Columns[i].ValueType.Name)
                {
                    nowDGV.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    nowDGV.Columns[i].DefaultCellStyle.Format = "F2";

                }

                //if (nowDGV.Columns[i].HeaderText.Contains("总计") || nowDGV.Columns[i].HeaderText.Contains("增长率"))
                //{
                //    nowDGV.Columns[i].DefaultCellStyle.BackColor = Color.SkyBlue;
                //    nowDGV.Columns[i].DefaultCellStyle.ForeColor = Color.Red;
                //}

                if ( nowDGV.Columns[i].HeaderText.Contains("合计"))
                {
                    nowDGV.Columns[i].DefaultCellStyle.BackColor = Color.Teal;
                    nowDGV.Columns[i].DefaultCellStyle.ForeColor = Color.White;
                }



            }

            for (int i = 1; i < nowDGV.RowCount; i++)
            {


                if (nowDGV.Rows[i].Cells[1].Value.ToString() == "汇总")
                {

                    for (int j = 0; j < nowDGV.Columns.Count; j++)
                    {


                        nowDGV.Rows[i].Cells[j].Style.BackColor = Color.LightBlue;
                        nowDGV.Rows[i].Cells[j].Style.ForeColor = Color.Red;

                           
                       
                    }
                }
                //else
                //{
                //    nowreigon = nowDGV.Rows[i].Cells["区域"].Value.ToString();
                //}



            }

            //string nowreigon;

            //if (nowDGV.RowCount >= 2)
            //{

            //    nowreigon = nowDGV.Rows[0].Cells["区域"].Value.ToString();
            //    for (int i = 1; i < nowDGV.RowCount; i++)
            //    {


            //        if (nowDGV.Rows[i].Cells["区域"].Value.ToString() == nowreigon)
            //        {

            //            for (int j = 0; j < nowDGV.Columns.Count; j++)
            //            {
            //                if (nowDGV.Columns[j].HeaderText.Contains("区域"))
            //                {


            //                    nowDGV.Rows[i].Cells[j].Value = DBNull.Value;


            //                }
            //            }
            //        }
            //        else
            //        {
            //            nowreigon = nowDGV.Rows[i].Cells["区域"].Value.ToString();
            //        }



            //    }
            //}

        }

        private void radioButton9_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton nowradio = sender as RadioButton;

            if (nowradio.Checked)
            {

                NewFrm.Show(this);


                CreateSeriesCom();
                LoadMonthdataCom();
                NewFrm.Hide(this);
            }
        }

        private void ly_sales_standard_SumDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void chart2_Click(object sender, EventArgs e)
        {

        }

        private void toolStripButton26_Click(object sender, EventArgs e)
        {
            SqlDataAdapter myAdapter1 = new SqlDataAdapter();

            myAdapter1.SelectCommand = new SqlCommand();

            myAdapter1.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter1.SelectCommand.CommandText = "ly_sales_standard_SumMoney_ForSalesPeople";
            myAdapter1.SelectCommand.Connection = new SqlConnection(SQLDatabase.Connectstring);
            myAdapter1.SelectCommand.CommandTimeout = 0;

            myAdapter1.SelectCommand.Parameters.Add("@salesperson_code", SqlDbType.VarChar);
            myAdapter1.SelectCommand.Parameters["@salesperson_code"].Value = this.nowusercode;

            myAdapter1.SelectCommand.Parameters.Add("@selcode", SqlDbType.VarChar);
            myAdapter1.SelectCommand.Parameters["@selcode"].Value = this.nowfillstragecode;



            myAdapter1.SelectCommand.Parameters.Add("@begindate", SqlDbType.DateTime);
            myAdapter1.SelectCommand.Parameters["@begindate"].Value = this.dateTimePicker9.Value;

            myAdapter1.SelectCommand.Parameters.Add("@enddate", SqlDbType.DateTime);
            myAdapter1.SelectCommand.Parameters["@enddate"].Value = this.dateTimePicker10.Value.AddDays(1);

            myAdapter1.SelectCommand.Parameters.Add("@nowstyle", SqlDbType.VarChar);
            myAdapter1.SelectCommand.Parameters["@nowstyle"].Value = "test";

            NewFrm.Show(this);


            if (null != ds6)
                ds6.Dispose();

            ds6 = new DataSet();

            myAdapter1.Fill(ds6);


            myAdapter1.Dispose();

            //this.dataGridView1.DataSource = allData;


            this.dataGridView6.Columns.Clear();

            this.dataGridView6.AutoGenerateColumns = true;
            dataGridView6.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCellsExceptHeaders;

            //BindingSource bindingSource1 = new BindingSource();

            if (null != bindingSource6)
                this.bindingSource6.Dispose();

            this.bindingSource6 = new BindingSource();


            bindingSource6.DataSource = ds6.Tables[0];
            dataGridView6.DataSource = bindingSource6;

            this.bindingNavigator6.BindingSource = bindingSource6;

            bindingSource6.ResetBindings(true);


            SetDGVCom(this.dataGridView6);

            NewFrm.Hide(this);
        }

        private void toolStripButton22_Click(object sender, EventArgs e)
        {
            ExportDataGridviewTOExcell.ExportDataGridview(this.dataGridView6, true);
        }

        private void toolStripButton33_Click(object sender, EventArgs e)
        {
            string nowstyle = "Contract";

            if (this.radioButton12.Checked)
            {

                nowstyle = "Contract";
                
            }
            else if (this.radioButton11.Checked)
            {

                nowstyle = "Mainteance";
               
            }

            else if (this.radioButton10.Checked)
            {
               
                nowstyle = "All";
            }

            SqlDataAdapter myAdapter1 = new SqlDataAdapter();

            myAdapter1.SelectCommand = new SqlCommand();

            myAdapter1.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter1.SelectCommand.CommandText = "ly_sales_standard_Sumcustomer";
            myAdapter1.SelectCommand.Connection = new SqlConnection(SQLDatabase.Connectstring);
            myAdapter1.SelectCommand.CommandTimeout = 0;

            myAdapter1.SelectCommand.Parameters.Add("@begindate", SqlDbType.DateTime);
            myAdapter1.SelectCommand.Parameters["@begindate"].Value = this.dateTimePicker11.Value;

            myAdapter1.SelectCommand.Parameters.Add("@enddate", SqlDbType.DateTime);
            myAdapter1.SelectCommand.Parameters["@enddate"].Value = this.dateTimePicker12.Value.AddDays(1);

            myAdapter1.SelectCommand.Parameters.Add("@nowstyle", SqlDbType.VarChar);
            myAdapter1.SelectCommand.Parameters["@nowstyle"].Value = nowstyle;




            if (null != ds7)
                ds7.Dispose();

            ds7 = new DataSet();

            NewFrm.Show(this);
            Thread.Sleep(500);

            myAdapter1.Fill(ds7);


            myAdapter1.Dispose();

            //this.dataGridView1.DataSource = allData;


            this.dataGridView5.Columns.Clear();

            this.dataGridView5.AutoGenerateColumns = true;
            dataGridView5.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCellsExceptHeaders;

            //BindingSource bindingSource1 = new BindingSource();

            if (null != bindingSource7)
                this.bindingSource7.Dispose();

            this.bindingSource7 = new BindingSource();


            bindingSource7.DataSource = ds7.Tables[0];
            dataGridView5.DataSource = bindingSource7;

            this.bindingNavigator7.BindingSource = bindingSource7;

            bindingSource7.ResetBindings(true);


            SetDGV7(this.dataGridView5);

            NewFrm.Hide(this);
        }

        private void toolStripButton34_Click(object sender, EventArgs e)
        {
            string nowstyle = "Contract";

            if (this.radioButton15.Checked)
            {

                nowstyle = "Contract";

            }
            else if (this.radioButton16.Checked)
            {

                nowstyle = "Mainteance";

            }

            else if (this.radioButton17.Checked)
            {

                nowstyle = "All";
            }

            SqlDataAdapter myAdapter1 = new SqlDataAdapter();

            myAdapter1.SelectCommand = new SqlCommand();

            myAdapter1.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter1.SelectCommand.CommandText = "ly_sales_standard_Sumsalesperson";
            myAdapter1.SelectCommand.Connection = new SqlConnection(SQLDatabase.Connectstring);
            myAdapter1.SelectCommand.CommandTimeout = 0;

            myAdapter1.SelectCommand.Parameters.Add("@begindate", SqlDbType.DateTime);
            myAdapter1.SelectCommand.Parameters["@begindate"].Value = this.dateTimePicker18.Value;

            myAdapter1.SelectCommand.Parameters.Add("@enddate", SqlDbType.DateTime);
            myAdapter1.SelectCommand.Parameters["@enddate"].Value = this.dateTimePicker19.Value.AddDays(1);

            myAdapter1.SelectCommand.Parameters.Add("@nowstyle", SqlDbType.VarChar);
            myAdapter1.SelectCommand.Parameters["@nowstyle"].Value = nowstyle;




            if (null != ds8)
                ds8.Dispose();

            ds8 = new DataSet();

            NewFrm.Show(this);
            Thread.Sleep(500);

            myAdapter1.Fill(ds8);


            myAdapter1.Dispose();

            //this.dataGridView1.DataSource = allData;


            this.dataGridView8.Columns.Clear();

            this.dataGridView8.AutoGenerateColumns = true;
            dataGridView8.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCellsExceptHeaders;

            //BindingSource bindingSource1 = new BindingSource();

            if (null != bindingSource8)
                this.bindingSource8.Dispose();

            this.bindingSource8 = new BindingSource();


            bindingSource8.DataSource = ds8.Tables[0];
            dataGridView8.DataSource = bindingSource8;

            this.bindingNavigator8.BindingSource = bindingSource8;

            bindingSource8.ResetBindings(true);


            SetDGV7(this.dataGridView8);

            NewFrm.Hide(this);
        }



        //private void SetPosition(Axis axis, double position)
        //{
        //    if (double.IsNaN(position))
        //        return;

        //    if (axis. == AxisName.X)
        //    {
        //        // Convert Double to DateTime.
        //        DateTime dateTimeX = DateTime.FromOADate(position);

        //        // Set X cursor position to edit Control
        //        CursorX.Text = dateTimeX.ToLongDateString();
        //    }
        //    else
        //    {
        //        // Set Y cursor position to edit Control
        //        CursorY.Text = position.ToString();
        //    }
        //}







    }
}
