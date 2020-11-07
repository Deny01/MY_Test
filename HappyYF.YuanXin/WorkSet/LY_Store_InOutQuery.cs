using DataGridFilter;
using HappyYF.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Transactions;
using System.Windows.Forms;

namespace HappyYF.YuanXin.WorkSet
{
    public partial class LY_Store_InOutQuery : Form
    {
        public LY_Store_InOutQuery()
        {
            InitializeComponent();
           this.f_Item_dynamicpriceTableAdapter.CommandTimeout = 0;
            this.f_Item_dynamicpriceBTableAdapter.CommandTimeout = 0;
            this.f_Item_dynamicpriceCTableAdapter.CommandTimeout = 0;
        }

        private void  CountMoney(BindingSource bs, DataGridView dg)
        {
            int haveHere = bs.Find("仓库", "总计");

            if (haveHere > -1)
            {
                bs .RemoveAt( haveHere);
            }
            
            if (null == dg.CurrentRow) return;

           
            decimal sum_storemoney = 0;
            decimal in_storemoney = 0;
            decimal out_storemoney = 0;
            decimal planout_money = 0;
            decimal plan_storemoney = 0;

            //decimal sum_accrual = 0;
            //decimal sum_compensate_accrual = 0;
            //decimal sum_left_accrual = 0;



            foreach (DataGridViewRow dr in dg.Rows)
            {
                //if (System.DBNull.Value == dr.Cells["数量"].Value)
                //    sum_qty = sum_qty + 0;
                //else
                //    sum_qty = sum_qty + decimal.Parse(dr.Cells["数量"].Value.ToString());

                if ("总计" == dr.Cells["仓库"].Value.ToString())
                {
                    dg.Rows.Remove(dr);

                }
                else
                {

                    if (System.DBNull.Value == dr.Cells["库存金额"].Value)
                        sum_storemoney = sum_storemoney + 0;
                    else
                        sum_storemoney = sum_storemoney + decimal.Parse(dr.Cells["库存金额"].Value.ToString());

                    if (System.DBNull.Value == dr.Cells["入库金额"].Value)
                        in_storemoney = in_storemoney + 0;
                    else
                        in_storemoney = in_storemoney + decimal.Parse(dr.Cells["入库金额"].Value.ToString());

                    if (System.DBNull.Value == dr.Cells["出库金额"].Value)
                        out_storemoney = out_storemoney + 0;
                    else
                        out_storemoney = out_storemoney + decimal.Parse(dr.Cells["出库金额"].Value.ToString());
                    //////////////////////////////
                    if (System.DBNull.Value == dr.Cells["计划出库金额"].Value)
                        planout_money = planout_money + 0;
                    else
                        planout_money = planout_money + decimal.Parse(dr.Cells["计划出库金额"].Value.ToString());

                    if (System.DBNull.Value == dr.Cells["计划库存金额"].Value)
                        plan_storemoney = plan_storemoney + 0;
                    else
                        plan_storemoney = plan_storemoney + decimal.Parse(dr.Cells["计划库存金额"].Value.ToString());


                   
                }

                //if (System.DBNull.Value == dr.Cells["采购金额"].Value)
                //    sum_buymoney = sum_buymoney + 0;
                //else
                //    sum_buymoney = sum_buymoney + decimal.Parse(dr.Cells["采购金额"].Value.ToString());


                //if (System.DBNull.Value == dr.Cells["利息"].Value)
                //    sum_accrual = sum_accrual + 0;
                //else
                //    sum_accrual = sum_accrual + decimal.Parse(dr.Cells["利息"].Value.ToString());

                //if (System.DBNull.Value == dr.Cells["还息"].Value)
                //    sum_compensate_accrual = sum_compensate_accrual + 0;
                //else
                //    sum_compensate_accrual = sum_compensate_accrual + decimal.Parse(dr.Cells["还息"].Value.ToString());

                //if (System.DBNull.Value == dr.Cells["欠息"].Value)
                //    sum_left_accrual = sum_left_accrual + 0;
                //else
                //    sum_left_accrual = sum_left_accrual + decimal.Parse(dr.Cells["欠息"].Value.ToString());



            }
            bs.AddNew();


            dg.CurrentRow.Cells["仓库"].Value = "总计";
            dg.CurrentRow.Cells["库存金额"].Value = sum_storemoney;
            dg.CurrentRow.Cells["入库金额"].Value = in_storemoney;
            dg.CurrentRow.Cells["出库金额"].Value = out_storemoney;
            dg.CurrentRow.Cells["计划出库金额"].Value = planout_money;
            dg.CurrentRow.Cells["计划库存金额"].Value = plan_storemoney;
            dg.CurrentRow.Cells["物资编号"].Value = "---";
            dg.CurrentRow.Cells["库存底线"].Value = -1;
            dg.CurrentRow.Cells["库存警戒"].Value = 0;
            //dg.CurrentRow.Cells["欠息"].Value = sum_left_accrual;

           



            bs.EndEdit();

            bs.Position = 0;
        }

        private void LY_MaterialMange_Load(object sender, EventArgs e)
        {
            this.tabPage8.Parent = null;
            this.tabPage9.Parent = null;

            SetHavePricerIGHT();

            NewFrm.Show(this);
            this.f_Item_dynamicpriceETableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            this.f_Item_dynamicpriceCTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            this.f_Item_dynamicpriceBTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            this.f_Item_dynamicpriceTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;

            this.average_Cost_ViewTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;

            

            this.ly_plan_getmaterialTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;


            this.ly_store_in_ylTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;

            this.ly_store_outqueryTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;


            this.ly_inma0010TableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;

            this.ly_inma0010TableAdapter.Fill(this.lYStoreMange.ly_inma0010, SQLDatabase.NowUserID);

            SetRowBackground();

            /////////////////////////单价金额查看


           

            SetRowBackground();
            NewFrm.Hide(this);

        }

        private void SetHavePricerIGHT()
        {
            if (SQLDatabase.CheckHaveRight(SQLDatabase.NowUserID, "单价金额查看"))
            {
                DataGridViewColumnSelector cs = new DataGridViewColumnSelector(ly_inma0010DataGridView, this.Text);
                cs.MaxHeight = 180;
                cs.Width = 800;

                cs.Set_dgvColumns();
                ////////////////////////////////

                CountMoney(ly_inma0010BindingSource, ly_inma0010DataGridView);

                ly_inma0010DataGridView.Columns["入库金额"].Visible = true;
                ly_inma0010DataGridView.Columns["出库金额"].Visible = true;
                ly_inma0010DataGridView.Columns["库存单价"].Visible = true;
                ly_inma0010DataGridView.Columns["库存金额"].Visible = true;
                ly_inma0010DataGridView.Columns["计划出库金额"].Visible = true;
                ly_inma0010DataGridView.Columns["计划库存金额"].Visible = true;
                ly_inma0010DataGridView.Columns["dynamic_price"].Visible = true;
                ly_inma0010DataGridView.Columns["machine_price"].Visible = true;
                ly_inma0010DataGridView.Columns["assembly_price"].Visible = true;
                ly_inma0010DataGridView.Columns["outsource_price"].Visible = true;
                ly_inma0010DataGridView.Columns["machine_outsource_price"].Visible = true;




            }
            else
            {
                ly_inma0010DataGridView.Columns["入库金额"].Visible = false;
                ly_inma0010DataGridView.Columns["出库金额"].Visible = false;
                ly_inma0010DataGridView.Columns["库存单价"].Visible = false;
                ly_inma0010DataGridView.Columns["库存金额"].Visible = false;
                ly_inma0010DataGridView.Columns["计划出库金额"].Visible = false;
                ly_inma0010DataGridView.Columns["计划库存金额"].Visible = false;
                ly_inma0010DataGridView.Columns["dynamic_price"].Visible = false;
                ly_inma0010DataGridView.Columns["machine_price"].Visible = false;
                ly_inma0010DataGridView.Columns["assembly_price"].Visible = false;
                ly_inma0010DataGridView.Columns["outsource_price"].Visible = false;
                ly_inma0010DataGridView.Columns["machine_outsource_price"].Visible = false;

                this.tabPage6.Parent = null;
                this.tabPage4.Parent = null;
                this.tabPage7.Parent = null;
                this.tabPage8.Parent = null;
                this.tabPage9.Parent = null;


            }
        }

        private void toolStripTextBox1_Enter(object sender, EventArgs e)
        {
            toolStripTextBox1.Text = "";
           
            this.ly_inma0010BindingSource.Filter = " ";

            CountMoney(ly_inma0010BindingSource, ly_inma0010DataGridView);
        }

        private void toolStripTextBox1_KeyUp(object sender, KeyEventArgs e)
        {
            //string dFilter = "";

            ////for (int i = 0; i < this.hT_Vehicle_collectDataGridView.ColumnCount; i++)
            //for (int i = 1; i < 10; i++)
            //{
            //    string tempColumnName = this.ly_inma0010DataGridView.Columns[i].DataPropertyName;
              
            //    if (i != 9)
            //        dFilter = dFilter + tempColumnName + " like  '*" + this.toolStripTextBox1.Text + "*' or ";
            //    else
            //        dFilter = dFilter + tempColumnName + " like  '*" + this.toolStripTextBox1.Text + "*' ";

            //}

            //if (this.toolStripTextBox1.Text.Replace(" ", "").Length > 0)

            //    this.ly_inma0010BindingSource.Filter =  dFilter + " or 仓库='总计'";
            //else
            //    this.ly_inma0010BindingSource.Filter = " ";

            //CountMoney(ly_inma0010BindingSource, ly_inma0010DataGridView);

            ////////////////////////////

            string filterString;


            filterString = GetDataGridviewMultiFilterString.DGVMultiFilterString(this.ly_inma0010DataGridView, this.toolStripTextBox1.Text);


            //this.ly_sales_standard_Report_zhongchengBindingSource.Filter = "(" + filterString + ") or 清单号='合计'";

            this.ly_inma0010BindingSource.Filter = "(" + filterString + ") or 仓库='总计'";

            CountMoney(ly_inma0010BindingSource, ly_inma0010DataGridView);

            //AddSummationRow_New(ly_sales_standard_Report_zhongchengBindingSource, ly_sales_contract_standard_ReportDataGridView);
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            ExportDataGridviewTOExcell.ExportDataGridview(this.ly_inma0010DataGridView, true);
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            FilterForm filterForm = new FilterForm();

         

            List<string> ls = new List<string>();
            ls.Add("id");


            filterForm.SetSourceColumns(ly_inma0010DataGridView.Columns , ls);

            filterForm.ShowDialog();

            string nowfilter = filterForm.GetFilterString();
            if (string.IsNullOrEmpty(nowfilter))
            {
                this.ly_inma0010BindingSource.Filter = nowfilter;
            }
            else
            {
                this.ly_inma0010BindingSource.Filter = "(" + nowfilter + ")" + " or 仓库='总计'";
            }
            
            //this.ly_inma0010BindingSource.Filter = filterForm.GetFilterString() ;

            CountMoney(ly_inma0010BindingSource, ly_inma0010DataGridView);
        }

       

        private void SetRowBackground()
        {
            foreach (DataGridViewRow dgr in ly_inma0010DataGridView.Rows)
            {
                if ("总计" == dgr.Cells["仓库"].Value.ToString())
                { 
                }
                else 
                {
                    //    if (0 >= decimal.Parse(dgr.Cells["库存警戒"].Value.ToString())
                    //        && 0 < decimal.Parse(dgr.Cells["库存底线"].Value.ToString()))
                    //        dgr.DefaultCellStyle.BackColor = Color.Cyan;

                    if (0 > decimal.Parse(dgr.Cells["库存警戒"].Value.ToString())
                        && 0 < decimal.Parse(dgr.Cells["库存底线"].Value.ToString()))
                        dgr.DefaultCellStyle.BackColor = Color.Cyan;
                }
                   
              


            }

           
        }

        private void SetRowBackgroundInOut( DataGridView  dgv)
        {
            foreach (DataGridViewRow dgr in dgv.Rows)
            {
                //if ("入库" == dgr.Cells["出入"].Value.ToString())
                if ("入库" == dgr.Cells[1].Value.ToString())
                {
                    dgr.DefaultCellStyle.BackColor = Color.Teal ;
                    dgr.DefaultCellStyle.ForeColor = Color.White;
                }
                else
                {
                    dgr.DefaultCellStyle.BackColor = Color.White ;
                    dgr.DefaultCellStyle.ForeColor = Color.Black ;
                }




            }


        }

        private void ly_inma0010DataGridView_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            SetRowBackground();
        }

        private void ly_inma0010DataGridView_SelectionChanged(object sender, EventArgs e)
        {
            if (null == ly_inma0010DataGridView.CurrentRow) return;

            string s = this.ly_inma0010DataGridView.CurrentRow.Cells["物资编号"].Value.ToString();

            this .ly_store_in_ylTableAdapter.Fill ( this .lYStoreMange .ly_store_in_yl ,s);
            this.ly_store_outqueryTableAdapter.Fill(this.lYStoreMange.ly_store_outquery, s);
            this.ly_plan_getmaterialTableAdapter.Fill(this.lYStoreMange.ly_plan_getmaterial,s);
            this.f_Item_dynamicpriceTableAdapter.Fill(this.lYStoreMange.f_Item_dynamicprice, "asdasd");

            //SetRowBackgroundInOut();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            NewFrm.Show(this);

            this.ly_inma0010TableAdapter.Fill(this.lYStoreMange.ly_inma0010, SQLDatabase.NowUserID);

            CountMoney(ly_inma0010BindingSource, ly_inma0010DataGridView);

            NewFrm.Hide(this);
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            if (null == this.ly_inma0010DataGridView.CurrentRow) return;
            SortForm DataSort = new SortForm();

            List<string> ls = new List<string>();
            ls.Add("id");


            DataSort.SetSortColumns(ly_inma0010DataGridView.Columns, ls);
            DataSort.ShowDialog();
            this.ly_inma0010BindingSource.Sort = DataSort.GetSortString();
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            if (null == this.ly_inma0010DataGridView.CurrentRow) return;

            string nowplannum;
            string noename;

            NewFrm.Show(this);
            foreach (DataGridViewRow dgr in ly_inma0010DataGridView.Rows)
            {
                nowplannum = dgr.Cells["物资编号"].Value.ToString();

                noename = dgr.Cells["名称"].Value.ToString();

                //this.toolStripLabel3.Text = plannum;
                //this.toolStripLabel3.Invalidate();

                NewFrm.Notify(this, "正在计算:  (" + nowplannum + ")" + noename + "   借用");



                Countmoney(nowplannum);

            }

            NewFrm.Hide(this);

            NewFrm.Show(this);

            this.ly_inma0010TableAdapter.Fill(this.lYStoreMange.ly_inma0010, SQLDatabase.NowUserID);

            CountMoney(ly_inma0010BindingSource, ly_inma0010DataGridView);

            NewFrm.Hide(this);
        }


        private static void Countmoney(string nowplannum)
        {
            string updstr = " update ly_inma0010  " +
                          "  set borrowcount=dbo.f_LY_borrow_Storecount(wzbh) "
                          + " where  wzbh='" + nowplannum + "'";



            SqlConnection sqlConnection1 = new SqlConnection(SQLDatabase.Connectstring);
            SqlCommand cmd = new SqlCommand();

            cmd.CommandText = updstr;
            cmd.CommandType = CommandType.Text;
            cmd.Connection = sqlConnection1;
            cmd.CommandTimeout = 0;

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

        private void ly_inma0010DataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void  SaveDetailItem(string nowitemno, string  nowvottomvalue)
        {

            ///////////////////

            string updstr = " update ly_inma0010  " +
                           "  set bott=  " + nowvottomvalue
                           + " where  wzbh='" + nowitemno+"'";


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
                    temp = 1;


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




            ////////////////////////////

        }



        private void SaveDetailsort(string nowitemno, string nowsortvalue)
        {

            ///////////////////

            string updstr = " update ly_inma0010  " +
                           "  set sort1=  '" + nowsortvalue
                           + "' where  wzbh='" + nowitemno + "'";


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
                    temp = 1;


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




            ////////////////////////////

        }
            //////////////////

        private void SaveDetailcategory(string nowitemno, string nowsortvalue)
        {

            ///////////////////

            string updstr = " update ly_inma0010  " +
                           "  set category=  '" + nowsortvalue
                           + "' where  wzbh='" + nowitemno + "'";


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
                    temp = 1;


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




            ////////////////////////////

        }

        private void SaveDetailmarposs(string nowitemno, string nowsortvalue)
        {

            ///////////////////

            string updstr = " update ly_inma0010  " +
                           "  set marposs_flag=  '" + nowsortvalue
                           + "' where  wzbh='" + nowitemno + "'";


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
                    temp = 1;


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




            ////////////////////////////

        }

        private void SaveDetaillocation(string nowitemno, string nowlocationvalue)
        {

            ///////////////////

            string updstr = " update ly_inma0010  " +
                           "  set jph=  '" + nowlocationvalue
                           + "' where  wzbh='" + nowitemno + "'";


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
                    temp = 1;


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




            ////////////////////////////

        }
        private void SaveDetailItemPrice(string nowitemno, string nowvottomvalue)
        {

            ///////////////////

            string updstr = " update ly_inma0010  " +
                           "  set dj=  " + nowvottomvalue
                           + " where  wzbh='" + nowitemno + "'";


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
                    temp = 1;


                }
                catch (SqlException sqle)
                {


                    MessageBox.Show(sqle.Message.Split('*')[0]);
                }

                            //
                finally
                {
                    sqlConnection1.Close();


                }
            }




            ////////////////////////////

        }

        private void ly_inma0010DataGridView_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            DataGridView dgv = sender as DataGridView;

            if (null == dgv.CurrentRow) return;

            ///////////////////////////////////////////////////////////////
            if ("Marposs" == dgv.CurrentCell.OwningColumn.Name)
            {
                //


                if (SQLDatabase.CheckHaveRight(SQLDatabase.NowUserID, "Marposs物料设置"))
                {

                }
                else
                {
                    MessageBox.Show("无Marposs物料设置权限", "注意");
                    return;
                }



                string s = this.ly_inma0010DataGridView.CurrentRow.Cells["物资编号"].Value.ToString();

                if ("True" == dgv.CurrentRow.Cells["Marposs"].Value.ToString())
                {
                    dgv.CurrentRow.Cells["Marposs"].Value = "False";

                    SaveDetailmarposs(s, "0");

                }
                else
                {

                    dgv.CurrentRow.Cells["Marposs"].Value = "True";
                    SaveDetailmarposs(s, "1");
                }



             
                 



              
                return;

            }

            ///////////////////////////////////////////////////////////////
            if ("组别" == dgv.CurrentCell.OwningColumn.Name)
            {
                //


                if (SQLDatabase.CheckHaveRight(SQLDatabase.NowUserID, "物料组别设置"))
                {

                }
                else
                {
                    MessageBox.Show("无物料组别设置权限", "注意");
                    return;
                }




                string s = this.ly_inma0010DataGridView.CurrentRow.Cells["物资编号"].Value.ToString();

                string sel = "SELECT distinct category_code as 代码,category_name as 组别 FROM ly_materialcategory  order by category_code";


                QueryForm queryForm = new QueryForm();


                queryForm.Sel = sel;
                queryForm.Constr = SQLDatabase.Connectstring;

                //Set the Column Collection to the filter Table
                //queryForm.SetSourceColumns(this.billMainDataSet.BalanceBill.Columns);

                queryForm.ShowDialog();




                if (queryForm.Result != "")
                {
                    dgv.CurrentRow.Cells["组别"].Value = queryForm.Result1;
                    //dgv.CurrentRow.Cells["discount_money"].Value = DBNull.Value;

                    //dgv.CurrentRow.Cells["approve_flag"].Value = "False";
                    SaveDetailcategory(s, queryForm.Result1);


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
            if ("种类" == dgv.CurrentCell.OwningColumn.Name)
            {
                //


                if (SQLDatabase.CheckHaveRight(SQLDatabase.NowUserID, "库存查询物料种类设置"))
                {

                }
                else
                {
                    MessageBox.Show("无物料种类设置权限", "注意");
                    return;
                }




                string s = this.ly_inma0010DataGridView.CurrentRow.Cells["物资编号"].Value.ToString();

                string sel = "SELECT distinct sortcode as 代码,sortname as 种类 FROM ly_materrial_sort  order by sortcode";


                QueryForm queryForm = new QueryForm();


                queryForm.Sel = sel;
                queryForm.Constr = SQLDatabase.Connectstring;

                //Set the Column Collection to the filter Table
                //queryForm.SetSourceColumns(this.billMainDataSet.BalanceBill.Columns);

                queryForm.ShowDialog();

              


                if (queryForm.Result != "")
                {
                    dgv.CurrentRow.Cells["种类"].Value = queryForm.Result1;
                    //dgv.CurrentRow.Cells["discount_money"].Value = DBNull.Value;

                    //dgv.CurrentRow.Cells["approve_flag"].Value = "False";
                    SaveDetailsort(s, queryForm.Result);


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
            if ("库存位置" == dgv.CurrentCell.OwningColumn.Name)
            {
                //


                if (SQLDatabase.CheckHaveRight(SQLDatabase.NowUserID, "库存位置设置"))
                {

                }
                else
                {
                    MessageBox.Show("无库存位置设置权限", "注意");
                    return;
                }

             

                string s = this.ly_inma0010DataGridView.CurrentRow.Cells["物资编号"].Value.ToString();

                ChangeValue queryForm = new ChangeValue();

                queryForm.OldValue = dgv.CurrentCell.Value.ToString();
                queryForm.NewValue = "";
                queryForm.ChangeMode = "string";
                queryForm.ShowDialog();


                if (queryForm.NewValue != "")
                {
                    dgv.CurrentRow.Cells["库存位置"].Value = queryForm.NewValue;
                    //dgv.CurrentRow.Cells["discount_money"].Value = DBNull.Value;

                    //dgv.CurrentRow.Cells["approve_flag"].Value = "False";
                    SaveDetaillocation(s, queryForm.NewValue);


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
            if ("库存底线" == dgv.CurrentCell.OwningColumn.Name)
            {
                //


                if (SQLDatabase.CheckHaveRight(SQLDatabase.NowUserID, "库存底线设置"))
                {

                }
                else
                {
                    MessageBox.Show("无库存底线设置权限", "注意");
                    return;
                }
            



                string s = this.ly_inma0010DataGridView.CurrentRow.Cells["物资编号"].Value.ToString();

                ChangeValue queryForm = new ChangeValue();

                queryForm.OldValue = dgv.CurrentCell.Value.ToString();
                queryForm.NewValue = "";
                queryForm.ChangeMode = "value";
                queryForm.ShowDialog();


                if (queryForm.NewValue != "")
                {
                    dgv.CurrentRow.Cells["库存底线"].Value = queryForm.NewValue;
                    //dgv.CurrentRow.Cells["discount_money"].Value = DBNull.Value;

                    //dgv.CurrentRow.Cells["approve_flag"].Value = "False";
                    SaveDetailItem(s, queryForm.NewValue);


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
            //////

            if ("库存单价" == dgv.CurrentCell.OwningColumn.Name)
            {
                //


                if (SQLDatabase.CheckHaveRight(SQLDatabase.NowUserID, "库存单价设置"))
                {

                }
                else
                {
                    MessageBox.Show("无库存单价设置权限", "注意");
                    return;
                }




                string s = this.ly_inma0010DataGridView.CurrentRow.Cells["物资编号"].Value.ToString();

                ChangeValue queryForm = new ChangeValue();

                queryForm.OldValue = dgv.CurrentCell.Value.ToString();
                queryForm.NewValue = "";
                queryForm.ChangeMode = "value";
                queryForm.ShowDialog();


                if (queryForm.NewValue != "")
                {
                    dgv.CurrentRow.Cells["库存单价"].Value = queryForm.NewValue;
                    //dgv.CurrentRow.Cells["discount_money"].Value = DBNull.Value;

                    //dgv.CurrentRow.Cells["approve_flag"].Value = "False";
                    SaveDetailItemPrice(s, queryForm.NewValue);


                    //CountPlanStru();

                }
                else
                {
                    dgv.CurrentRow.Cells["库存单价"].Value = DBNull.Value;
                    //dgv.CurrentRow.Cells["discount_money"].Value = DBNull.Value;

                    //dgv.CurrentRow.Cells["approve_flag"].Value = "False";
                    SaveDetailItemPrice(s, "null");
                }
                return;

            }
            ///////////////////////////////////////////////////////////////
        }

        private void f_Item_dynamicpriceDataGridView_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {

            DataGridView dgv = sender as DataGridView;
            SetRowBackgroundInOut(dgv);




        }

        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            if (null == ly_inma0010DataGridView.CurrentRow) return;

            string s = this.ly_inma0010DataGridView.CurrentRow.Cells["物资编号"].Value.ToString();

           
            this.f_Item_dynamicpriceTableAdapter.Fill(this.lYStoreMange.f_Item_dynamicprice, s);
        }
        Point pt = new Point();
        private void toolStripButton7_Click(object sender, EventArgs e)
        {
            //if (SQLDatabase.NowUserID != "000")
            //{
            //    MessageBox.Show("只有管理员可以操作！"); return;
            //} 

            if (SQLDatabase.CheckHaveRight(SQLDatabase.NowUserID, "更新库位价格"))
            {

            }
            else
            {
                MessageBox.Show("无更新库位价格权限", "注意");
                return;
            }

            ChangeValue queryForm = new ChangeValue();
             
            queryForm.NewValue = "";
            queryForm.ChangeMode = "datetime";
            queryForm.ShowDialog();


            if (queryForm.NewValue != "")
            {

                NewFrm.Show(this);

                SqlConnection sqlConnection1 = new SqlConnection(SQLDatabase.Connectstring);
                SqlCommand cmd = new SqlCommand();

                cmd.Parameters.Add("@begintime", SqlDbType.DateTime);
                cmd.Parameters["@begintime"].Value = Convert.ToDateTime(queryForm.NewValue);

                cmd.CommandText = "opening_inventory_count";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = sqlConnection1;
                cmd.CommandTimeout = 0;
                sqlConnection1.Open();
                cmd.ExecuteNonQuery();
                sqlConnection1.Close();

                NewFrm.Hide(this);

            }
            else
            {

                return;


            }




            //DatePicker queryForm = new DatePicker();
            //queryForm.Pt = pt;

            
            //queryForm.NowDate = DateTime.Now.ToString();

            //queryForm.ShowDialog();



            //if (null != queryForm.NowDate)
            //{
            //    NewFrm.Show(this);

            //    SqlConnection sqlConnection1 = new SqlConnection(SQLDatabase.Connectstring);
            //    SqlCommand cmd = new SqlCommand();

            //    cmd.Parameters.Add("@begintime", SqlDbType.DateTime);
            //    cmd.Parameters["@begintime"].Value = Convert.ToDateTime(queryForm.NowDate);

            //    cmd.CommandText = "opening_inventory_count";
            //    cmd.CommandType = CommandType.StoredProcedure;
            //    cmd.CommandType = CommandType.StoredProcedure;
            //    cmd.Connection = sqlConnection1;

            //    sqlConnection1.Open();
            //    cmd.ExecuteNonQuery();
            //    sqlConnection1.Close();

            //    NewFrm.Hide(this);

            //}

        }

        private void toolStripButton12_Click(object sender, EventArgs e)
        {
            if (null == ly_inma0010DataGridView.CurrentRow) return;

            string s = this.ly_inma0010DataGridView.CurrentRow.Cells["物资编号"].Value.ToString();


            this.f_Item_dynamicpriceBTableAdapter.Fill(this.lYStoreMange.f_Item_dynamicpriceB, s);
        }

        //private void fillToolStripButton_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        this.f_Item_dynamicpriceCTableAdapter.Fill(this.lYStoreMange.f_Item_dynamicpriceC, itemnoToolStripTextBox.Text);
        //    }
        //    catch (System.Exception ex)
        //    {
        //        System.Windows.Forms.MessageBox.Show(ex.Message);
        //    }

        //}

        private void toolStripButton17_Click(object sender, EventArgs e)
        {
            if (null == ly_inma0010DataGridView.CurrentRow) return;

            string s = this.ly_inma0010DataGridView.CurrentRow.Cells["物资编号"].Value.ToString();


            this.f_Item_dynamicpriceCTableAdapter.Fill(this.lYStoreMange.f_Item_dynamicpriceC, s);
        }

        private void toolStripButton27_Click(object sender, EventArgs e)
        {
            if (null == ly_inma0010DataGridView.CurrentRow) return;

            string s = this.ly_inma0010DataGridView.CurrentRow.Cells["物资编号"].Value.ToString();


            this.f_Item_dynamicpriceETableAdapter.Fill(this.lYStoreMange.f_Item_dynamicpriceE, s);
        }

        private void toolStripButton28_Click(object sender, EventArgs e)
        {
            if (null == ly_inma0010DataGridView.CurrentRow) return;

            string s = this.ly_inma0010DataGridView.CurrentRow.Cells["物资编号"].Value.ToString();
            this.average_Cost_ViewTableAdapter.Fill(this.lYStoreMange.Average_Cost_View, s);
        }

        private void 查看入库价格来源ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null) return;
            if ("出库" == dataGridView1.CurrentRow.Cells["出入B"].Value.ToString()) return;

            string wzbh = dataGridView1.CurrentRow.Cells["wzbhB"].Value.ToString();
            string djbh = dataGridView1.CurrentRow.Cells["原始单据B"].Value.ToString();
            string machine_Code = dataGridView1.CurrentRow.Cells["machine_num"].Value.ToString();
            string storeInId = dataGridView1.CurrentRow.Cells["origin_id"].Value.ToString();

            if (string.IsNullOrEmpty(djbh)) return;

            

            if (djbh.Length < 2) return;

            string Rs = djbh.Substring(0, 2);

            switch (Rs)
            {
                case "CG":

                    LY_GetPurchasePrice queryForm = new LY_GetPurchasePrice();

                    queryForm.InStr = djbh;
                    queryForm.Code = wzbh;

                    queryForm.StartPosition = FormStartPosition.CenterParent;

                    queryForm.ShowDialog();

                    break;




                case "GD":

                    LY_GetRestructuringPrice queryFormDG = new LY_GetRestructuringPrice();

                    queryFormDG.InStr = djbh;
                    queryFormDG.Code = machine_Code;

                    queryFormDG.StartPosition = FormStartPosition.CenterParent;

                    queryFormDG.ShowDialog();

                    break;

                case "GQ":

                    LY_GetRestructuringPrice queryFormQG = new LY_GetRestructuringPrice();

                    queryFormQG.InStr = djbh;
                    queryFormQG.Code = machine_Code;

                    queryFormQG.StartPosition = FormStartPosition.CenterParent;

                    queryFormQG.ShowDialog();

                    break;

                case "DZ":

                    LY_GetQzDzPrice queryFormDZ = new LY_GetQzDzPrice();

                    queryFormDZ.InStr = djbh;
                    queryFormDZ.Code = machine_Code;

                    queryFormDZ.StartPosition = FormStartPosition.CenterParent;
                    queryFormDZ.ShowDialog();

                    break;

                case "QZ":

                    LY_GetQzDzPrice queryFormQZ = new LY_GetQzDzPrice();

                    queryFormQZ.InStr = djbh;
                    queryFormQZ.Code = machine_Code;

                    queryFormQZ.StartPosition = FormStartPosition.CenterParent;

                    queryFormQZ.ShowDialog();

                    break;

                case "ZJ":

                    LY_GetMachinePrice queryFormZJ = new LY_GetMachinePrice();

                    queryFormZJ.InStrId = storeInId;
                    queryFormZJ.StartPosition = FormStartPosition.CenterParent;

                    queryFormZJ.ShowDialog();

                    break;

                case "JY":

                    LY_OutSourcePrice queryFormJY = new LY_OutSourcePrice();

                    queryFormJY.InStrId = storeInId;

                    queryFormJY.StartPosition = FormStartPosition.CenterParent;
                    queryFormJY.ShowDialog();

                    break;


                default:
                    //Console.WriteLine("Default case");
                    break;
            }
        }

        //private void fillToolStripButton_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        this.average_Cost_ViewTableAdapter.Fill(this.lYStoreMange.Average_Cost_View, wzbhToolStripTextBox.Text);

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
        //        this.f_Item_dynamicpriceETableAdapter.Fill(this.lYStoreMange.f_Item_dynamicpriceE, itemnoToolStripTextBox.Text);
        //    }
        //    catch (System.Exception ex)
        //    {
        //        System.Windows.Forms.MessageBox.Show(ex.Message);
        //    }

        //}
    }
}
