using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using HappyYF.Infrastructure.Repositories;
using System.Transactions;
using DataGridFilter;


namespace HappyYF.YuanXin.WorkSet
{
    public partial class LY_SalseContract_StandardReport_Tec : Form
    {
        private string nowfilterStr = "";
        private string nowusercode = "";
        private string nowfillstragecode = "";

        private DataSet ds3;
        BindingSource bindingSource3;

        public LY_SalseContract_StandardReport_Tec()
        {
            InitializeComponent();

        }

        public void InitializeApp()
        {



        }

        private void t_usersBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
 
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




            this.nowusercode = SQLDatabase.NowUserID;
            //this.ly_plan_getmaterial_departmentTableAdapter.Fill(this.lYPlanMange.ly_plan_getmaterial_department,
                 this.ly_plan_getmaterial_departmentTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            this.ly_sales_standard_Report_give_forTecTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;





            this.dateTimePicker1.Text = DateTime.Today.AddMonths(-1).Date.ToString();
            this.dateTimePicker2.Text = DateTime.Today.AddDays(0).Date.ToString();


            string selAllString;

            if (SQLDatabase.CheckHaveRight(SQLDatabase.NowUserID, "营业综合信息"))
            {

                selAllString = "SELECT  a.salesregion_code, a.salesregion_code+':'+a.salesregion_name as salesregion_name,b.yhbm,b.yhbm+':'+b.yhmc as yhmc FROM  ly_salesregion a left join T_users b on a.salesregion_code=b.salesregion_code ORDER BY  salesregion_code ";
            }
            else if (SQLDatabase.CheckHaveRight(SQLDatabase.NowUserID, "营业区域信息"))
            {

                selAllString = "SELECT  a.salesregion_code, a.salesregion_code+':'+a.salesregion_name as salesregion_name,b.yhbm,b.yhbm+':'+b.yhmc as yhmc FROM  ly_salesregion a left join T_users b on a.salesregion_code=b.salesregion_code  where b.salesregion_code='" + SQLDatabase.nowSalesregioncode() + "' ORDER BY  salesregion_code ";
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

                //this.ly_sales_contract_main1DataGridView.SelectionChanged -= ly_sales_contract_main1DataGridView_SelectionChanged;
                //this.ly_sales_contract_main1TableAdapter.Fill(this.lYSalseMange.ly_sales_contract_main1, this.nowusercode, "full", this.dateTimePicker1.Value, this.dateTimePicker2.Value);
                //AddSummationRow_New(ly_sales_contract_main1BindingSource, ly_sales_contract_main1DataGridView);
                //this.ly_sales_contract_main1DataGridView.SelectionChanged += ly_sales_contract_main1DataGridView_SelectionChanged;
            }



            if (0 == this.tabControl1.SelectedIndex)
            {

                //this.ly_sales_standard_Report_giveTableAdapter.Fill(this.lYSalseMange.ly_sales_borrow_detail_all, this.nowusercode, this.nowfillstragecode, this.dateTimePicker1.Value, this.dateTimePicker2.Value);
                this.ly_sales_standard_Report_give_forTecTableAdapter.Fill(this.lYSalseMange2.ly_sales_standard_Report_give_forTec, this.nowusercode, this.nowfillstragecode, this.dateTimePicker1.Value, this.dateTimePicker2.Value.AddDays(1));
                AddSummationRow_New(ly_sales_standard_Report_give_forTecBindingSource, ly_sales_standard_Report_giveDataGridView);


                InitializeApp();


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
                            if ("年份" != dgvCell.OwningColumn.HeaderText && "月份" != dgvCell.OwningColumn.HeaderText)
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
            this.ly_sales_standard_Report_give_forTecTableAdapter.Fill(this.lYSalseMange2.ly_sales_standard_Report_give_forTec, this.nowusercode, this.nowfillstragecode, this.dateTimePicker1.Value, this.dateTimePicker2.Value.AddDays(1));
            AddSummationRow_New(ly_sales_standard_Report_give_forTecBindingSource, ly_sales_standard_Report_giveDataGridView);

        }

        private void toolStripTextBox4_KeyUp(object sender, KeyEventArgs e)
        {
            string filterString;


            filterString = GetDataGridviewMultiFilterString.DGVMultiFilterString(this.ly_sales_standard_Report_giveDataGridView, this.toolStripTextBox4.Text);


            this.ly_sales_standard_Report_give_forTecBindingSource.Filter = filterString;
        }



        private void toolStripTextBox4_Enter(object sender, EventArgs e)
        {
            toolStripTextBox4.Text = "";

            this.ly_sales_standard_Report_give_forTecBindingSource.Filter = "";
        }

        private void ly_sales_standard_Report_giveDataGridView_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            DataGridView dgv = sender as DataGridView;

            if (null == dgv.CurrentRow) return;

        


            if ("项目编码1" == dgv.CurrentCell.OwningColumn.Name)
            {


                string nowcontract_innerNum = dgv.CurrentRow.Cells["清单号1"].Value.ToString();
                 

                string sel = "select  distinct remark as 项目编码 from ly_material_plan_main where remark like ('X2017-%') or remark like ('X2018-%')  order by remark";


                QueryForm queryForm = new QueryForm();


                queryForm.Sel = sel;
                queryForm.Constr = SQLDatabase.Connectstring;
                queryForm.Nodiscol = 1;

                queryForm.ShowDialog();


                string updstr;

                if ( !string.IsNullOrEmpty( queryForm.Result))
                {
                    dgv.CurrentRow.Cells["项目编码1"].Value = queryForm.Result;
      
                    dgv.CurrentRow.Cells["执行人1"].Value = SQLDatabase.nowUserName();
                    dgv.CurrentRow.Cells["分配时间1"].Value =DateTime.Now;
                    updstr = " update ly_sales_contract_main  " +
                                   "  set tec_project_num=  '" + queryForm.Result + "',tec_project_people='" + SQLDatabase.nowUserName() + "',tec_project_date=getdate()  where  contract_inner_code='" + nowcontract_innerNum + "'";



                }
                else
                {

                    dgv.CurrentRow.Cells["项目编码1"].Value = DBNull.Value;
                    dgv.CurrentRow.Cells["执行人1"].Value= DBNull.Value;
                    dgv.CurrentRow.Cells["分配时间1"].Value = DBNull.Value;

                    updstr = " update ly_sales_contract_main  " +
                               "  set tec_project_num= null,tec_project_people=null,tec_project_date=null where  contract_inner_code='" + nowcontract_innerNum + "'";


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

                return;
            }
        }

        private void ly_sales_standard_Report_giveDataGridView_SelectionChanged(object sender, EventArgs e)
        {
            if (null == ly_sales_standard_Report_giveDataGridView.CurrentRow)
            {

                return;
            }


            string planNum = this.ly_sales_standard_Report_giveDataGridView.CurrentRow.Cells["依赖书号1"].Value.ToString();
            this.ly_plan_getmaterial_departmentTableAdapter.Fill(this.lYPlanMange.ly_plan_getmaterial_department, planNum);


            //////this.ly_plan_getmaterial_departmentBindingSource.Filter = "领料部门='asd'";

            this.ly_store_outnumTableAdapter.Fill(this.lYStoreMange.ly_store_outnum, planNum, SQLDatabase.NowUserID);

            if (null == this.ly_store_outnumDataGridView.CurrentRow)
            {

                this.ly_store_outTableAdapter.Fill(this.lYStoreMange.ly_store_out, "asd", SQLDatabase.NowUserID);
            }

        }

        private void ly_store_outnumDataGridView_SelectionChanged(object sender, EventArgs e)
        {
            if (null == ly_store_outnumDataGridView.CurrentRow) return;


            if (null != this.ly_store_outnumDataGridView.CurrentRow)
            {

                string outNum = this.ly_store_outnumDataGridView.CurrentRow.Cells["out_number"].Value.ToString();


                this.ly_store_outTableAdapter.Fill(this.lYStoreMange.ly_store_out, outNum, SQLDatabase.nowUserName());




            }

        }

       
    }
}