using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using HappyYF.Infrastructure.Repositories;
using DataGridFilter;
using System.Transactions;
using System.Data.SqlClient;


namespace HappyYF.YuanXin.WorkSet
{
    public partial class LY_Finance_Store_InOutQuery : Form
    {
        public LY_Finance_Store_InOutQuery()
        {
            InitializeComponent();
            this.f_Item_dynamicpriceTableAdapter.CommandTimeout = 0;
            this.lY_Inventory_query_financialTableAdapter.CommandTimeout = 0;
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
            //NewFrm.Show(this);


            this.dateTimePicker5.Text = "1900" + "-01" + "-01";

            this.dateTimePicker6.Text = SQLDatabase.GetNowdate().AddDays(0).Date.ToString();

            this .average_Cost_ViewTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;

            this.f_Item_dynamicpriceTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            this.f_Item_dynamicpriceBTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
           

            this.ly_plan_getmaterialTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
          

            this.ly_store_in_ylTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;

            this.ly_store_in_yl_financialTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            this.ly_store_outquery_fiancialTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            this.ly_plan_getmaterialTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            this.f_Item_dynamicpriceTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;

            this.ly_store_outqueryTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;


            this.lY_Inventory_query_financialTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;

           // this.LY_Inventory_query_financialTableAdapter.Fill(this.lYStoreMange.LY_Inventory_query_financial , SQLDatabase.NowUserID);

            SetRowBackground();

            ///////////////////////
            DataGridViewColumnSelector cs = new DataGridViewColumnSelector(ly_inma0010DataGridView, this.Text);
            cs.MaxHeight = 180;
            cs.Width = 800;

            cs.Set_dgvColumns();
            ////////////////////////////////

            CountMoney(lY_Inventory_query_financialBindingSource, ly_inma0010DataGridView);

            SetRowBackground();
            //NewFrm.Hide(this);

        }

        private void toolStripTextBox1_Enter(object sender, EventArgs e)
        {
            toolStripTextBox1.Text = "";

            this.lY_Inventory_query_financialBindingSource.Filter = " ";

            CountMoney(lY_Inventory_query_financialBindingSource, ly_inma0010DataGridView);
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

            this.lY_Inventory_query_financialBindingSource.Filter = "(" + filterString + ") or 仓库='总计'";

            CountMoney(lY_Inventory_query_financialBindingSource, ly_inma0010DataGridView);

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
                this.lY_Inventory_query_financialBindingSource.Filter = nowfilter;
            }
            else
            {
                this.lY_Inventory_query_financialBindingSource.Filter = "(" + nowfilter + ")" + " or 仓库='总计'";
            }
            
            //this.ly_inma0010BindingSource.Filter = filterForm.GetFilterString() ;

            CountMoney(lY_Inventory_query_financialBindingSource, ly_inma0010DataGridView);
        }

        private void SetRowBackgroundInOut(DataGridView dgv)
        {
            foreach (DataGridViewRow dgr in dgv.Rows)
            {
                //if ("入库" == dgr.Cells["出入"].Value.ToString())
                if ("入库" == dgr.Cells[1].Value.ToString())
                {
                    dgr.DefaultCellStyle.BackColor = Color.Teal;
                    dgr.DefaultCellStyle.ForeColor = Color.White;
                }
                else
                {
                    dgr.DefaultCellStyle.BackColor = Color.White;
                    dgr.DefaultCellStyle.ForeColor = Color.Black;
                }




            }


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
                    if (0 >= decimal.Parse(dgr.Cells["库存警戒"].Value.ToString())
                        && 0 < decimal.Parse(dgr.Cells["库存底线"].Value.ToString()))
                        dgr.DefaultCellStyle.BackColor = Color.Cyan;
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

            this.ly_store_in_yl_financialTableAdapter.Fill(this.lYStoreMange.ly_store_in_yl_financial, s, this.dateTimePicker5.Value, this.dateTimePicker6.Value.AddDays(1));
            this.ly_store_outquery_fiancialTableAdapter.Fill(this.lYStoreMange.ly_store_outquery_fiancial, s, this.dateTimePicker5.Value, this.dateTimePicker6.Value.AddDays(1));
            this.ly_plan_getmaterialTableAdapter.Fill(this.lYStoreMange.ly_plan_getmaterial,s);
            this.f_Item_dynamicpriceTableAdapter.Fill(this.lYStoreMange.f_Item_dynamicprice, "asdasd");

            //SetRowBackgroundInOut(sender as DataGridView);
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            NewFrm.Show(this);
            this.ly_inma0010DataGridView.SelectionChanged -= ly_inma0010DataGridView_SelectionChanged;

            this.lY_Inventory_query_financialTableAdapter.Fill(this.lYStoreMange.LY_Inventory_query_financial, SQLDatabase.NowUserID, this.dateTimePicker5.Value, this.dateTimePicker6.Value.AddDays(1));

            CountMoney(lY_Inventory_query_financialBindingSource, ly_inma0010DataGridView);

            this.ly_inma0010DataGridView.SelectionChanged += ly_inma0010DataGridView_SelectionChanged;

            //this.ly_sales_standard_SumTableAdapter.Fill(this.lYSalseRepair.ly_sales_standard_Sum, this.nowusercode, this.nowfillstragecode, this.dateTimePicker5.Value, this.dateTimePicker6.Value.AddDays(1));


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
            this.lY_Inventory_query_financialBindingSource.Sort = DataSort.GetSortString();
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

            this.lY_Inventory_query_financialTableAdapter.Fill(this.lYStoreMange.LY_Inventory_query_financial , SQLDatabase.NowUserID, this.dateTimePicker5.Value, this.dateTimePicker6.Value.AddDays(1));

            CountMoney(lY_Inventory_query_financialBindingSource, ly_inma0010DataGridView);

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

        private void ly_inma0010DataGridView_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            DataGridView dgv = sender as DataGridView;

            if (null == dgv.CurrentRow) return;

          

          
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

            ///////////////////////////////////////////////////////////////
        }

        private void ly_inma0010DataGridView_CellMouseDoubleClick_1(object sender, DataGridViewCellMouseEventArgs e)
        {
            DataGridView dgv = sender as DataGridView;

            if (null == dgv.CurrentRow) return;
            
            ///////////////////////////////////////////////////////////////
            if ("采购单价" == dgv.CurrentCell.OwningColumn.Name || "税前采购" == dgv.CurrentCell.OwningColumn.Name)
            {
                //


                //if (SQLDatabase.CheckHaveRight(SQLDatabase.NowUserID, "库存位置设置"))
                //{

                //}
                //else
                //{
                //    MessageBox.Show("无库存位置设置权限", "注意");
                //    return;
                //}



                string s = this.ly_inma0010DataGridView.CurrentRow.Cells["物资编号"].Value.ToString();

                LY_MatiarailPriceQuery queryForm = new LY_MatiarailPriceQuery();

              
                queryForm.material_code = s;

                queryForm.StartPosition = FormStartPosition.CenterParent;

               
                queryForm.ShowDialog();

                //queryForm.retrieveitemPrice(s);

                //ChangeValue queryForm = new ChangeValue()

                //queryForm.OldValue = dgv.CurrentCell.Value.ToString();
                //queryForm.NewValue = "";
                //queryForm.ChangeMode = "string";
                //queryForm.ShowDialog();


                //if (queryForm.NewValue != "")
                //{
                //    dgv.CurrentRow.Cells["库存位置"].Value = queryForm.NewValue;
                //    //dgv.CurrentRow.Cells["discount_money"].Value = DBNull.Value;

                //    //dgv.CurrentRow.Cells["approve_flag"].Value = "False";
                //    SaveDetaillocation(s, queryForm.NewValue);


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
                return;

            }


            ///////////////////////////////////////////////////////////////
        }

        private void ly_inma0010DataGridView_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void f_Item_dynamicpriceDataGridView_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            DataGridView dgv = sender as DataGridView;
            SetRowBackgroundInOut(dgv);
        }

        private void toolStripButton7_Click(object sender, EventArgs e)
        {
            if (null == ly_inma0010DataGridView.CurrentRow) return;

            string s = this.ly_inma0010DataGridView.CurrentRow.Cells["物资编号"].Value.ToString();

          
            this.f_Item_dynamicpriceTableAdapter.Fill(this.lYStoreMange.f_Item_dynamicprice, s);
        }

        

       
        private void toolStripButton12_Click(object sender, EventArgs e)
        {
            if (null == ly_inma0010DataGridView.CurrentRow) return;

            string s = this.ly_inma0010DataGridView.CurrentRow.Cells["物资编号"].Value.ToString();


            this.f_Item_dynamicpriceBTableAdapter.Fill(this.lYStoreMange.f_Item_dynamicpriceB, s);

        }

        private void toolStripButton17_Click(object sender, EventArgs e)
        {
            if (null == ly_inma0010DataGridView.CurrentRow) return;

            string s = this.ly_inma0010DataGridView.CurrentRow.Cells["物资编号"].Value.ToString();


            this.f_Item_dynamicpriceCTableAdapter.Fill(this.lYStoreMange.f_Item_dynamicpriceC, s);
        }

        private void dataGridView1_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            DataGridView dgv = sender as DataGridView;
            SetRowBackgroundInOut(dgv);
        }

        private void dataGridView2_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            DataGridView dgv = sender as DataGridView;
            SetRowBackgroundInOut(dgv);
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

        private void toolStripButton28_Click(object sender, EventArgs e)
        {
            if (null == ly_inma0010DataGridView.CurrentRow) return;

            string s = this.ly_inma0010DataGridView.CurrentRow.Cells["物资编号"].Value.ToString();

                       
            this.average_Cost_ViewTableAdapter.Fill(this.lYStoreMange.Average_Cost_View, s);
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
        //        this.f_Item_dynamicpriceTableAdapter.Fill(this.lYStoreMange.f_Item_dynamicprice, itemnoToolStripTextBox.Text);
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
        //        this.ly_store_in_yl_financialTableAdapter.Fill(this.lYStoreMange.ly_store_in_yl_financial, wzbhToolStripTextBox.Text, new System.Nullable<System.DateTime>(((System.DateTime)(System.Convert.ChangeType(begindateToolStripTextBox.Text, typeof(System.DateTime))))), new System.Nullable<System.DateTime>(((System.DateTime)(System.Convert.ChangeType(enddateToolStripTextBox.Text, typeof(System.DateTime))))));
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
        //        this.ly_store_outquery_fiancialTableAdapter.Fill(this.lYStoreMange.ly_store_outquery_fiancial, wzbhToolStripTextBox.Text, new System.Nullable<System.DateTime>(((System.DateTime)(System.Convert.ChangeType(begindateToolStripTextBox.Text, typeof(System.DateTime))))), new System.Nullable<System.DateTime>(((System.DateTime)(System.Convert.ChangeType(enddateToolStripTextBox.Text, typeof(System.DateTime))))));
        //    }
        //    catch (System.Exception ex)
        //    {
        //        System.Windows.Forms.MessageBox.Show(ex.Message);
        //    }

        //}






    }
}
