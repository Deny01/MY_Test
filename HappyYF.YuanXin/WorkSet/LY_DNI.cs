using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Transactions;
using System.Windows.Forms;
using HappyYF.Infrastructure.Repositories;
using DataGridFilter;


namespace HappyYF.YuanXin.WorkSet
{
    public partial class LY_DNI : Form
    {
        private string nowfilterStr = "";
        private string nowusercode = "";
        private string nowfillstragecode = "";
 

        public LY_DNI()
        {
            InitializeComponent();
            this.ly_sales_cost_ReportTableAdapter.CommandTimeout = 0;
            this.ly_sales_standard_SumMarpossNewTableAdapter.CommandTimeout = 0;
            this.ly_sales_standard_SumMarpossNew2TableAdapter.CommandTimeout = 0;

        }

        public void InitializeApp()
        {



            this.dataGridViewSummary1.SummaryColumns = new string[] { "金额" };
            this.dataGridViewSummary1.SumRowHeaderText = "合计";
            this.dataGridViewSummary1.DisplaySumRowHeader = true;
            this.dataGridViewSummary1.RefreshSummary();


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

            //this.dataGridViewSummary1.SummaryColumns;

            this.ly_sales_standard_SumTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            this.ly_sales_standard_Report_giveTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            this.ly_sales_cost_ReportTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;

            this.ly_sales_standard_SumMarpossTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;

            this.ly_sales_standard_SumMarpossNewTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;

            this.ly_sales_standard_SumMarpossNew2TableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;

            this.ly_sales_cost_detailTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;

            ly_sales_contract_owe_query_checkbompriceTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;

            //this.dateTimePicker5.Text = SQLDatabase.GetNowdate().AddYears(-1).Date.Year.ToString() + "-12" + "-26";

            ly_DNI_ReportTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;



            this.dateTimePicker1.Text = DateTime.Today.AddMonths(-1).Date.ToString();
            this.dateTimePicker2.Text = DateTime.Today.AddDays(0).Date.ToString();

            this.dateTimePicker3.Text = DateTime.Today.AddMonths(-1).Date.ToString();
            this.dateTimePicker4.Text = DateTime.Today.AddDays(0).Date.ToString();



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


            }
            else if (e.Node.Level == 1)
            {


                this.nowusercode = this.nowfilterStr.Substring(this.nowfilterStr.Length - 3, 2);
                this.nowfillstragecode = "region";

            }
            else if (e.Node.Level == 0)
            {

                this.nowusercode = "";
                this.nowfillstragecode = "full";

            }



        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {

        }

        private void t_usersBindingSource_CurrentChanged(object sender, EventArgs e)
        {
            System.Windows.Forms.BindingSource bs = sender as BindingSource;

            if (null != (DataRowView)bs.Current)
            {

            }
        }







        private TreeNode FindNode(TreeNodeCollection tnParent, string strValue)
        {

            if (tnParent == null) return null;



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





        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            ExportDataGridviewTOExcell.ExportDataGridview(this.ly_sales_contract_standard_ReportDataGridView, true);
        }

        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            NewFrm.Show(this);

            lysalescostReportBindingSource.Filter = " 开票日期='' or 开票日期 is null ";
            if (radioButton1.Checked)
            {
                this.ly_sales_cost_ReportTableAdapter.Fill(this.lYSalseMange2.ly_sales_cost_Report, this.nowusercode, this.nowfillstragecode, this.dateTimePicker3.Value, this.dateTimePicker4.Value.AddDays(1), "approve");
            }

            else
            {
                this.ly_sales_cost_ReportTableAdapter.Fill(this.lYSalseMange2.ly_sales_cost_Report, this.nowusercode, this.nowfillstragecode, this.dateTimePicker3.Value, this.dateTimePicker4.Value.AddDays(1), "invoice");

            }
            AddSummationRow_New(lysalescostReportBindingSource, ly_sales_contract_standard_ReportDataGridView);

            NewFrm.Hide(this);


        }

        public void refresh_weekdata(string begindate, string enddate)
        {

            this.dateTimePicker3.Text = begindate;
            this.dateTimePicker4.Text = enddate;

            if (radioButton1.Checked)
            {
                this.ly_sales_cost_ReportTableAdapter.Fill(this.lYSalseMange2.ly_sales_cost_Report, this.nowusercode, this.nowfillstragecode, this.dateTimePicker3.Value, this.dateTimePicker4.Value.AddDays(1), "approve");
            }

            else
            {
                this.ly_sales_cost_ReportTableAdapter.Fill(this.lYSalseMange2.ly_sales_cost_Report, this.nowusercode, this.nowfillstragecode, this.dateTimePicker3.Value, this.dateTimePicker4.Value.AddDays(1), "invoice");

            }

            AddSummationRow_New(lysalescostReportBindingSource, ly_sales_contract_standard_ReportDataGridView);



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
            }
        }

        private void toolStripButton7_Click(object sender, EventArgs e)
        {
            if (null == this.ly_sales_contract_standard_ReportDataGridView.CurrentRow) return;

            FilterForm filterForm = new FilterForm();


            List<string> ls = new List<string>();
            ls.Add("id");


            filterForm.SetSourceColumns(this.lYSalseMange2.ly_sales_cost_Report.Columns, ls);

            filterForm.ShowDialog();

            string filterstr = filterForm.GetFilterString();
            if (!string.IsNullOrEmpty(filterstr))
            {

                this.lysalescostReportBindingSource.Filter = "(" + filterstr + ") or 清单号='合计'";
            }
            AddSummationRow_New(lysalescostReportBindingSource, ly_sales_contract_standard_ReportDataGridView);

        }

        private void toolStripButton8_Click(object sender, EventArgs e)
        {
            if (null == this.ly_sales_contract_standard_ReportDataGridView.CurrentRow) return;
            SortForm DataSort = new SortForm();

            List<string> ls = new List<string>();
            ls.Add("id");


            DataSort.SetSortColumns(this.lYSalseMange2.ly_sales_cost_Report.Columns, ls);
            DataSort.ShowDialog();
            this.lysalescostReportBindingSource.Sort = " sumorder asc," + DataSort.GetSortString();
            AddSummationRow_New(lysalescostReportBindingSource, ly_sales_contract_standard_ReportDataGridView);
        }

        private void toolStripTextBox2_KeyUp(object sender, KeyEventArgs e)
        {
            string filterString;


            filterString = GetDataGridviewMultiFilterString.DGVMultiFilterString(this.ly_sales_contract_standard_ReportDataGridView, this.toolStripTextBox2.Text);


            this.lysalescostReportBindingSource.Filter = "(" + filterString + ") or 清单号='合计'";
            AddSummationRow_New(lysalescostReportBindingSource, ly_sales_contract_standard_ReportDataGridView);

            //this.ly_sales_contract_standard_ReportDataGridView.SelectionChanged += ly_sales_contract_standard_ReportDataGridView_SelectionChanged_1;
        }

        private void toolStripTextBox2_Enter(object sender, EventArgs e)
        {
            toolStripTextBox2.Text = "";
            this.ly_sales_contract_standard_ReportDataGridView.SelectionChanged -= ly_sales_contract_standard_ReportDataGridView_SelectionChanged_1;
            ly_sales_cost_detailTableAdapter.Fill(this.lYSalseMange2.ly_sales_cost_detail, "");
            ly_sales_contract_owe_query_checkbompriceTableAdapter.Fill(this.lYSalseMange2.ly_sales_contract_owe_query_checkbomprice,
               "", this.dateTimePicker3.Value, this.dateTimePicker4.Value.AddDays(1));
            lysalescostReportBindingSource.Filter = " 开票日期='' or 开票日期 is null ";
            AddSummationRow_New(lysalescostReportBindingSource, ly_sales_contract_standard_ReportDataGridView);
        }

        private void ly_sales_contract_standard_ReportDataGridView_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (ly_sales_contract_standard_ReportDataGridView.CurrentRow == null)
            {
                ly_sales_cost_detailTableAdapter.Fill(this.lYSalseMange2.ly_sales_cost_detail, "");
                ly_sales_contract_owe_query_checkbompriceTableAdapter.Fill(this.lYSalseMange2.ly_sales_contract_owe_query_checkbomprice,
                   "", this.dateTimePicker3.Value, this.dateTimePicker4.Value.AddDays(1));
            }
            else
            {
                string innerCode = ly_sales_contract_standard_ReportDataGridView.CurrentRow.Cells["清单号"].Value.ToString();
                ly_sales_cost_detailTableAdapter.Fill(this.lYSalseMange2.ly_sales_cost_detail, innerCode);

                ly_sales_contract_owe_query_checkbompriceTableAdapter.Fill(this.lYSalseMange2.ly_sales_contract_owe_query_checkbomprice,
                    innerCode, this.dateTimePicker3.Value, this.dateTimePicker4.Value.AddDays(1));


            }

        }
        private void toolStripButton9_Click(object sender, EventArgs e)
        {
            this.lysalescostReportBindingSource.Filter = " 公司='中原' or 清单号='合计'";
            AddSummationRow_New(lysalescostReportBindingSource, ly_sales_contract_standard_ReportDataGridView);

        }

        private void toolStripButton10_Click(object sender, EventArgs e)
        {
            this.lysalescostReportBindingSource.Filter = " 公司='中成'or 清单号='合计'";
            AddSummationRow_New(lysalescostReportBindingSource, ly_sales_contract_standard_ReportDataGridView);

        }

        private void toolStripButton11_Click(object sender, EventArgs e)
        {
            lysalescostReportBindingSource.Filter = " 开票日期='' or 开票日期 is null ";
            AddSummationRow_New(lysalescostReportBindingSource, ly_sales_contract_standard_ReportDataGridView);

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




                SqlConnection sqlConnection1 = new SqlConnection(SQLDatabase.Connectstring);
                SqlCommand cmd = new SqlCommand();

                cmd.CommandText = updstr;
                cmd.CommandType = CommandType.Text;
                cmd.Connection = sqlConnection1;
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
        private void ly_sales_contract_standard_ReportDataGridView_Sorted(object sender, EventArgs e)
        {

            DataGridView nowview = sender as DataGridView;

            string nowdirection = "asc";
            string nowsort = this.lysalescostReportBindingSource.Sort;

            if (null != nowsort && nowsort.Substring(nowsort.Length - 5) == " desc")
            //if (nowview.SortOrder == System.Windows.Forms.SortOrder.Ascending)
            {
                nowdirection = " asc";
            }
            else
            {
                nowdirection = " desc";
            }


            this.lysalescostReportBindingSource.Sort = " sumorder asc," + nowview.SortedColumn.DataPropertyName + nowdirection;
            AddSummationRow_New(lysalescostReportBindingSource, ly_sales_contract_standard_ReportDataGridView);
        }

        private void ly_sales_contract_standard_ReportDataGridView_ColumnSortModeChanged(object sender, DataGridViewColumnEventArgs e)
        { }

        private void ly_sales_contract_standard_ReportDataGridView_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {

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

                string weekperiod = "---";

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
            //LY_Query_week_cost queryForm = new LY_Query_week_cost();

            //queryForm.OwnerForm = this;


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
                    this.lysalescostReportBindingSource.Filter = " (税务 = '" + dgv.CurrentCell.Value.ToString() + "') or 清单号='合计'";
                    AddSummationRow_New(lysalescostReportBindingSource, ly_sales_contract_standard_ReportDataGridView);
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


                    this.lysalescostReportBindingSource.Filter = " (substring(清单号,1,2) = '" + dgv.CurrentCell.Value.ToString().Substring(0, 2) + "') or 清单号='合计'";
                    AddSummationRow_New(lysalescostReportBindingSource, ly_sales_contract_standard_ReportDataGridView);
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

                        this.lysalescostReportBindingSource.Filter = " (内容= '" + dgv.CurrentCell.Value.ToString() + "') or 清单号='合计'";
                        AddSummationRow_New(lysalescostReportBindingSource, ly_sales_contract_standard_ReportDataGridView);
                    }
                    else
                    {
                        this.lysalescostReportBindingSource.Filter = " (substring(清单号,1,2) = 'XS') or 清单号='合计'";
                        AddSummationRow_New(lysalescostReportBindingSource, ly_sales_contract_standard_ReportDataGridView);
                    }
                }
                //else
                //{
                //    MessageBox.Show(dgv.CurrentCell.Value.ToString());

                //}借用
            }


        }







        private void toolStripButton58_Click(object sender, EventArgs e)
        {
            ExportDataGridviewTOExcell.ExportDataGridview(this.dataGridView2, true);
        }

        private void toolStripButton63_Click(object sender, EventArgs e)
        {
            ExportDataGridviewTOExcell.ExportDataGridview(this.dataGridView3, true);
        }



        private void ly_sales_contract_standard_ReportDataGridView_SelectionChanged_1(object sender, EventArgs e)
        {
            if (ly_sales_contract_standard_ReportDataGridView.CurrentRow == null)
            {
                ly_sales_cost_detailTableAdapter.Fill(this.lYSalseMange2.ly_sales_cost_detail, "");
                ly_sales_contract_owe_query_checkbompriceTableAdapter.Fill(this.lYSalseMange2.ly_sales_contract_owe_query_checkbomprice,
                   "", this.dateTimePicker3.Value, this.dateTimePicker4.Value.AddDays(1));
            }
            else
            {
                string innerCode = ly_sales_contract_standard_ReportDataGridView.CurrentRow.Cells["清单号"].Value.ToString();
                ly_sales_cost_detailTableAdapter.Fill(this.lYSalseMange2.ly_sales_cost_detail, innerCode);

                ly_sales_contract_owe_query_checkbompriceTableAdapter.Fill(this.lYSalseMange2.ly_sales_contract_owe_query_checkbomprice,
                    innerCode, this.dateTimePicker3.Value, this.dateTimePicker4.Value.AddDays(1));


            }
        }

        private void toolStripTextBox2_Leave(object sender, EventArgs e)
        {
            this.ly_sales_contract_standard_ReportDataGridView.SelectionChanged += ly_sales_contract_standard_ReportDataGridView_SelectionChanged_1;
        }

        private void toolStripButton12_Click(object sender, EventArgs e)
        {
            ExportDataGridviewTOExcell.ExportDataGridview(this.dataGridView1, true);
        }

        private void toolStripButton14_Click(object sender, EventArgs e)
        {
            NewFrm.Show(this);

            this.ly_DNI_ReportTableAdapter.Fill(this.lYSalseMange2.ly_DNI_Report, this.dateTimePicker1.Value, this.dateTimePicker2.Value.AddDays(1));

            AddSummationRow_New2(lyDNIReportBindingSource, dataGridView1);

            NewFrm.Hide(this);
        }


        private void AddSummationRow_New2(BindingSource bs, DataGridView dgv)
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


            sumdr["清单号"] = "合计";
            sumdr["客户"] = "";
            sumdr["税务"] = "";
            ((DataSet)bs.DataSource).Tables[bs.DataMember].Rows.Add(sumdr);
            bs.ResetBindings(true);

        }

        private void toolStripTextBox3_Enter(object sender, EventArgs e)
        {
            toolStripTextBox3.Text = "";

            lyDNIReportBindingSource.Filter = "";
            AddSummationRow_New2(lyDNIReportBindingSource, dataGridView1);
        }

        private void toolStripTextBox3_KeyUp(object sender, KeyEventArgs e)
        {
            string filterString;


            filterString = GetDataGridviewMultiFilterString.DGVMultiFilterString(this.dataGridView1, this.toolStripTextBox3.Text);


            this.lyDNIReportBindingSource.Filter = "(" + filterString + ") or 清单号='合计'";
            AddSummationRow_New2(lyDNIReportBindingSource, dataGridView1);

        }
    }
}
