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
    public partial class LY_SalseRepair_QCReport : Form
    {
        private string nowfilterStr = "";
        private string nowusercode = "";
        private string nowfillstragecode = "";



        public LY_SalseRepair_QCReport()
        {
            InitializeComponent();
            this.lY_salesWarranty_queryQCTableAdapter.CommandTimeout  = 0;
            this.ly_sales_receive_itemDetail_repairA_AllByWzbh1TableAdapter.CommandTimeout = 0;
        }

        //private void t_usersBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        //{
        //    //this.Validate();
        //    //this.t_usersBindingSource.EndEdit();
        //    ////this.tableAdapterManager.UpdateAll(this.yonghuDataSet);
        //    //this.t_usersTableAdapter.Update( this.yonghuDataSet.T_users);

        //    SetViewState("View");

        //}

       

        private void Yonghu_Load(object sender, EventArgs e)
        {

            

          
            this.nowusercode = SQLDatabase.NowUserID;

            this.ly_sales_receive_itemDetail_repair_returnTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            this.ly_sales_receive_itemDetail_repair_wasteTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            this.ly_sales_receive_itemDetail_repairA_AllByWzbh1TableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            




            this.ly_sales_receive_itemDetail_repairTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;

            this. lY_salesWarranty_queryQCTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;


            ly_sales_receive_itemDetail_repairA_AllTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;



            this.dateTimePicker1.Text = DateTime.Today.AddMonths(-1).Date.ToString();
            this.dateTimePicker2.Text = DateTime.Today.AddDays(0).Date.ToString();

          
         

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

        //private void AddSummationRow_New(BindingSource bs, DataGridView dgv)
        //{
        //    DataRow sumdr = (((DataSet)bs.DataSource).Tables[bs.DataMember]).NewRow();

        //    if (-1 != bs.Find("清单号", "合计"))
        //    {
        //        bs.RemoveAt(bs.Find("清单号", "合计"));
        //    }

        //    foreach (DataGridViewRow dgvRow in dgv.Rows)
        //    {
        //        foreach (DataGridViewCell dgvCell in dgvRow.Cells)
        //        {
        //            //foreach (DataGridViewColumn dgvColumn in dgv.Columns)
        //            //{
        //            if (dgvCell.Value != null && !(dgvCell.Value is DBNull))
        //            {
        //                if (IsInteger(dgvCell.Value))
        //                {
        //                    if ("年份" != dgvCell.OwningColumn.HeaderText && "月份" != dgvCell.OwningColumn.HeaderText)
        //                    {
        //                        if (sumdr[dgvCell.OwningColumn.DataPropertyName] == null || (sumdr[dgvCell.OwningColumn.DataPropertyName] is DBNull))
        //                            sumdr[dgvCell.OwningColumn.DataPropertyName] = 0;


        //                        sumdr[dgvCell.OwningColumn.DataPropertyName] = Convert.ToInt64(sumdr[dgvCell.OwningColumn.DataPropertyName]) + Convert.ToInt64(dgvCell.Value);
        //                    }
        //                }
        //                else if (IsDecimal(dgvCell.Value))
        //                {
        //                    if ("年份" != dgvCell.OwningColumn.HeaderText && "月份" != dgvCell.OwningColumn.HeaderText )
        //                    {
        //                        if (sumdr[dgvCell.OwningColumn.DataPropertyName] == null || (sumdr[dgvCell.OwningColumn.DataPropertyName] is DBNull))
        //                            sumdr[dgvCell.OwningColumn.DataPropertyName] = 0;
        //                        //dgvCell .OwningColumn.Name  dgvCell.ColumnIndex

        //                        sumdr[dgvCell.OwningColumn.DataPropertyName] = Convert.ToDecimal(sumdr[dgvCell.OwningColumn.DataPropertyName]) + Convert.ToDecimal(dgvCell.Value);
        //                    }
        //                }

        //                //sumBox.Text = string.Format("{0}", sumBox.Tag);
        //                //sumBox.Invalidate();

        //            }
        //            //}
        //        }

        //    }
        //    sumdr["清单号"] = "合计";
        //    sumdr["客户"] = "";
        //    sumdr["税务"] = "";
        //    ((DataSet)bs.DataSource).Tables[bs.DataMember].Rows.Add(sumdr);
        //    bs.ResetBindings(true);

        //}

       

        //private void toolStripButton16_Click_1(object sender, EventArgs e)
        //{
        //    ExportDataGridviewTOExcell.ExportDataGridview(this.ly_sales_contract_standard_ReportDataGridView, true);
        //}

        //private void toolStripButton21_Click_1(object sender, EventArgs e)
        //{
        //    this.ly_sales_repairstandard_ReportTableAdapter.Fill(this.lYSalseMange2.ly_sales_repairstandard_Report, this.nowusercode, this.nowfillstragecode, this.dateTimePicker1.Value, this.dateTimePicker2.Value.AddDays(1));
        //    AddSummationRow_New(ly_sales_repairstandard_ReportBindingSource, ly_sales_contract_standard_ReportDataGridView);

        //}

        //private void toolStripTextBox4_KeyUp(object sender, KeyEventArgs e)
        //{
        //    string filterString;


        //    filterString = GetDataGridviewMultiFilterString.DGVMultiFilterString(this.ly_sales_standard_Report_giveDataGridView, this.toolStripTextBox4.Text);


        //    this.ly_sales_standard_Report_giveBindingSource.Filter = filterString;
        //}

       

        //private void toolStripTextBox4_Enter(object sender, EventArgs e)
        //{
        //    toolStripTextBox4.Text = "";

        //    this.ly_sales_standard_Report_giveBindingSource.Filter = "";
        //}

        //private void toolStripButton5_Click(object sender, EventArgs e)
        //{
        //    ExportDataGridviewTOExcell.ExportDataGridview(this.ly_sales_contract_standard_ReportDataGridView, true);
        //}

        //private void toolStripButton6_Click(object sender, EventArgs e)
        //{
        //    this.ly_sales_repairstandard_ReportTableAdapter.Fill(this.lYSalseMange2.ly_sales_repairstandard_Report, this.nowusercode, this.nowfillstragecode, this.dateTimePicker3.Value, this.dateTimePicker4.Value.AddDays(1));
            
            
        //    AddSummationRow_New(ly_sales_repairstandard_ReportBindingSource, ly_sales_contract_standard_ReportDataGridView);

        //}

        //private void ly_sales_contract_standard_ReportDataGridView_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        //{
        //    string isgood = "yes";

        //    decimal nowmoney;

           

        //    foreach (DataGridViewRow dgr in ly_sales_contract_standard_ReportDataGridView.Rows)
        //    {


        //        if ("" != dgr.Cells["金额"].Value.ToString())
        //        {
        //            nowmoney = decimal.Parse(dgr.Cells["金额"].Value.ToString());
        //        }
        //        else
        //        {
        //            nowmoney = 0;
        //        }







        //        if (nowmoney <= 0)
        //            {
        //                foreach (DataGridViewCell dgc in dgr.Cells)
        //                {

        //                    dgc.Style.BackColor = Color.White;
        //                    dgc.Style.ForeColor = Color.Red;
        //                }
        //            }
              
        //        //else
        //        //{ 


        //        //}



        //    }
        //}

        //private void toolStripButton7_Click(object sender, EventArgs e)
        //{
        //    if (null == this.ly_sales_contract_standard_ReportDataGridView.CurrentRow)    return;

        //    FilterForm filterForm = new FilterForm();

        //    //SumQueryDataSet qds;
        //    //qds = new SumQueryDataSet();

        //    List<string> ls = new List<string>();
        //    ls.Add("id");


        //    filterForm.SetSourceColumns(this .lYSalseMange2.ly_sales_contract_standard_Report.Columns, ls);

        //    filterForm.ShowDialog();

        //    this.ly_sales_repairstandard_ReportBindingSource.Filter = filterForm.GetFilterString();
        //    AddSummationRow_New(ly_sales_repairstandard_ReportBindingSource, ly_sales_contract_standard_ReportDataGridView);

        //}

        //private void toolStripButton8_Click(object sender, EventArgs e)
        //{
        //    if (null == this.ly_sales_contract_standard_ReportDataGridView.CurrentRow) return;
        //    SortForm DataSort = new SortForm();

        //    List<string> ls = new List<string>();
        //    ls.Add("id");


        //    DataSort.SetSortColumns(this.lYSalseMange2.ly_sales_contract_standard_Report.Columns, ls);
        //    DataSort.ShowDialog();
        //    //this.ly_sales_contract_standard_ReportBindingSource.Sort ="(" +DataSort.GetSortString()+") or 清单号='合计'";
        //}

        //private void toolStripTextBox2_KeyUp(object sender, KeyEventArgs e)
        //{
        //    string filterString;


        //    filterString = GetDataGridviewMultiFilterString.DGVMultiFilterString(this.ly_sales_contract_standard_ReportDataGridView, this.toolStripTextBox2.Text);


        //    this.ly_sales_repairstandard_ReportBindingSource.Filter = "(" + filterString + ") or 清单号='合计'";
        //    AddSummationRow_New(ly_sales_repairstandard_ReportBindingSource, ly_sales_contract_standard_ReportDataGridView);
        //}

        //private void toolStripTextBox2_Enter(object sender, EventArgs e)
        //{
        //    toolStripTextBox2.Text = "";

        //    this.ly_sales_repairstandard_ReportBindingSource.Filter = "";
        //}

        //private void ly_sales_contract_standard_ReportDataGridView_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        //{
        //    DataGridView dgv = sender as DataGridView;

        //    if (null == dgv.CurrentRow) return;

        //    string nowcontract_code = dgv.CurrentRow.Cells["收件单号"].Value.ToString();

        //    ///////////////////////////////////////////////////////////////

        //    if ("开票日期" == dgv.CurrentCell.OwningColumn.Name)
        //    {

        //        //if ("XS" != nowcontract_code.Substring(0, 2))
        //        //{

        //        //    MessageBox.Show("只能修改合同开票日期...", "注意");
        //        //    return;
                
        //        //}

        //        ChangeValue queryForm = new ChangeValue();

        //        queryForm.OldValue = dgv.CurrentCell.Value.ToString();
        //        queryForm.NewValue = "";
        //        queryForm.ChangeMode = "datetime";
        //        queryForm.ShowDialog();


                
        //        string updstr;

        //        if (queryForm.NewValue != "")
        //        {
        //            dgv.CurrentRow.Cells["开票日期"].Value = queryForm.NewValue;
        //            //dgv.CurrentRow.Cells["开票人"].Value = "宋美彰";
        //            updstr = " update ly_sales_receive  " +
        //                        "  set invoice_date=  '" + queryForm.NewValue + "',invoice_people='宋美彰' where  receive_code='" + nowcontract_code + "'";



        //        }
        //        else
        //        {

        //            dgv.CurrentRow.Cells["开票日期"].Value = DBNull.Value;
        //            //dgv.CurrentRow.Cells["开票人"].Value = DBNull.Value;
        //            updstr = " update ly_sales_receive  " +
        //                        "  set invoice_date= null, invoice_people=null where  receive_code='" + nowcontract_code + "'";


        //        }


        //        ///////////////////

               


        //                SqlConnection sqlConnection1 = new SqlConnection(SQLDatabase.Connectstring);
        //                SqlCommand cmd = new SqlCommand();

        //                cmd.CommandText = updstr;
        //                cmd.CommandType = CommandType.Text;
        //                cmd.Connection = sqlConnection1;

        //                //sqlConnection1.Open();
        //                //cmd.ExecuteNonQuery();
        //                //sqlConnection1.Close();

        //                int temp = 0;

        //                using (TransactionScope scope = new TransactionScope())
        //                {

        //                    sqlConnection1.Open();
        //                    try
        //                    {

        //                        cmd.ExecuteNonQuery();



        //                        scope.Complete();



        //                    }
        //                    catch (SqlException sqle)
        //                    {


        //                        MessageBox.Show(sqle.Message.Split('*')[0]);
        //                    }


        //                    finally
        //                    {
        //                        sqlConnection1.Close();


        //                    }
        //                }



        //                ////////////////////////////



        //        return;

        //    }
        //    ////////////////////////////////////////////////////////////////////////
        //    ///////////////////////////////////////////////////////////////

        //    if ("免税" == dgv.CurrentCell.OwningColumn.Name)
        //    {

        //        //if ("XS" != nowcontract_code.Substring(0, 2))
        //        //{

        //        //    MessageBox.Show("只能修改合同开票日期...", "注意");
        //        //    return;

        //        //}

        //        string updstr;

        //        if ("True" == dgv.CurrentRow.Cells["免税"].Value.ToString())
        //        {
        //            dgv.CurrentRow.Cells["免税"].Value = "False";

        //            updstr = " update ly_sales_receive  " +
        //                      "  set have_tax= 0 where  receive_code='" + nowcontract_code + "'";



        //        }
        //        else
        //        {

        //            dgv.CurrentRow.Cells["免税"].Value = "True";

        //            updstr = " update ly_sales_receive  " +
        //                      "  set have_tax= 1 where  receive_code='" + nowcontract_code + "'";


        //        }




               

            



        //        SqlConnection sqlConnection1 = new SqlConnection(SQLDatabase.Connectstring);
        //        SqlCommand cmd = new SqlCommand();

        //        cmd.CommandText = updstr;
        //        cmd.CommandType = CommandType.Text;
        //        cmd.Connection = sqlConnection1;

        //        //sqlConnection1.Open();
        //        //cmd.ExecuteNonQuery();
        //        //sqlConnection1.Close();

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



        //        ////////////////////////////



        //        return;

        //    }
        //    ////////////////////////////////////////////////////////////////////////
        //}

        //private void toolStripButton9_Click(object sender, EventArgs e)
        //{
        //    this.ly_sales_repairstandard_ReportBindingSource.Filter = " 公司='中原' or 清单号='合计'";
        //    AddSummationRow_New(ly_sales_repairstandard_ReportBindingSource, ly_sales_contract_standard_ReportDataGridView);

        //}

        //private void toolStripButton10_Click(object sender, EventArgs e)
        //{
        //    this.ly_sales_repairstandard_ReportBindingSource.Filter = " 公司='中成'or 清单号='合计'";
        //    AddSummationRow_New(ly_sales_repairstandard_ReportBindingSource, ly_sales_contract_standard_ReportDataGridView);

        //}

        //private void toolStripButton11_Click(object sender, EventArgs e)
        //{
        //    this.ly_sales_repairstandard_ReportBindingSource.Filter = "";
        //    AddSummationRow_New(ly_sales_repairstandard_ReportBindingSource, ly_sales_contract_standard_ReportDataGridView);

        //}

        //private void ly_sales_contract_standard_ReportDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        //{

        //}

        private void toolStripButton19_Click(object sender, EventArgs e)
        {
            NewFrm.Show(this);
            this.lY_salesWarranty_queryQCDataGridView.SelectionChanged -= lY_salesWarranty_queryQCDataGridView_SelectionChanged;
            if (this.radioButton2.Checked)
            {
                this.lY_salesWarranty_queryQCTableAdapter.Fill(this.lYSalseMange2.LY_salesWarranty_queryQC, this.dateTimePicker1.Value.Date, this.dateTimePicker2.Value.Date.AddDays(1), "inspect");
                this.ly_sales_receive_itemDetail_repairA_AllTableAdapter.Fill(this.lYSalseMange2.ly_sales_receive_itemDetail_repairA_All, this.dateTimePicker1.Value, this.dateTimePicker2.Value.AddDays(1), "inspect");
            }
            else
            {
                this.lY_salesWarranty_queryQCTableAdapter.Fill(this.lYSalseMange2.LY_salesWarranty_queryQC, this.dateTimePicker1.Value.Date , this.dateTimePicker2.Value.Date.AddDays(1), "receive");
                this.ly_sales_receive_itemDetail_repairA_AllTableAdapter.Fill(this.lYSalseMange2.ly_sales_receive_itemDetail_repairA_All, this.dateTimePicker1.Value, this.dateTimePicker2.Value.AddDays(1), "receive");

            }
            this.lY_salesWarranty_queryQCDataGridView.SelectionChanged += lY_salesWarranty_queryQCDataGridView_SelectionChanged;
            NewFrm.Hide(this);
        }

        private void toolStripButton20_Click(object sender, EventArgs e)
        {
            this.lY_salesWarranty_queryQCBindingSource.Filter = " 超期=1";
            this.lysalesreceiveitemDetailrepairAAllBindingSource.Filter = " ifOvertime=1";
        }

        private void toolStripButton21_Click(object sender, EventArgs e)
        {
            this.lY_salesWarranty_queryQCBindingSource.Filter = " 超期=0";
            this.lysalesreceiveitemDetailrepairAAllBindingSource.Filter = " ifOvertime=0";
        }

        private void toolStripButton22_Click(object sender, EventArgs e)
        {
            this.lY_salesWarranty_queryQCBindingSource.Filter = "";
            this.lysalesreceiveitemDetailrepairAAllBindingSource.Filter = "";
        }

        private void toolStripButton17_Click(object sender, EventArgs e)
        {
            if (null == this.lY_salesWarranty_queryQCDataGridView.CurrentRow) return;

            FilterForm filterForm = new FilterForm();

            //SumQueryDataSet qds;
            //qds = new SumQueryDataSet();

            List<string> ls = new List<string>();
            ls.Add("id");


            filterForm.SetSourceColumns(this.lYSalseMange2.LY_salesWarranty_queryQC.Columns, ls);

            filterForm.ShowDialog();

            this.lY_salesWarranty_queryQCBindingSource.Filter = filterForm.GetFilterString();
        }

        private void toolStripButton18_Click(object sender, EventArgs e)
        {
            if (null == this.lY_salesWarranty_queryQCDataGridView.CurrentRow) return;
            SortForm DataSort = new SortForm();

            List<string> ls = new List<string>();
            ls.Add("id");


            DataSort.SetSortColumns(this.lYSalseMange2.LY_salesWarranty_queryQC.Columns, ls);
            DataSort.ShowDialog();




            if (null == this.dataGridView1.CurrentRow) return;
            SortForm DataSort2 = new SortForm();

            List<string> ls2 = new List<string>();
            ls2.Add("id");


            DataSort2.SetSortColumns(this.lYSalseMange2.ly_sales_receive_itemDetail_repairA_All.Columns, ls2);
            DataSort2.ShowDialog();




        }

       

        private void toolStripTextBox3_KeyUp(object sender, KeyEventArgs e)
        {
            string filterString;


            filterString = GetDataGridviewMultiFilterString.DGVMultiFilterString(this.lY_salesWarranty_queryQCDataGridView, this.toolStripTextBox3.Text);


            this.lY_salesWarranty_queryQCBindingSource.Filter = filterString;

            string filterString2;


            filterString2 = GetDataGridviewMultiFilterString.DGVMultiFilterString(this.dataGridView1, this.toolStripTextBox3.Text);


            this.lysalesreceiveitemDetailrepairAAllBindingSource.Filter = filterString2;

        }

        private void toolStripTextBox3_Enter(object sender, EventArgs e)
        {
            toolStripTextBox3.Text = "";

            this.lY_salesWarranty_queryQCBindingSource.Filter = "";
            this.lysalesreceiveitemDetailrepairAAllBindingSource.Filter = "";
        }

        private void toolStripButton16_Click(object sender, EventArgs e)
        {
            ExportDataGridviewTOExcell.ExportDataGridview(this.lY_salesWarranty_queryQCDataGridView, true);
        }

        

        private void lY_salesWarranty_queryQCDataGridView_SelectionChanged(object sender, EventArgs e)
        {
            if (null == lY_salesWarranty_queryQCDataGridView.CurrentRow)
            {
                this.ly_sales_receive_itemDetail_repairTableAdapter.Fill(this.lYSalseMange2.ly_sales_receive_itemDetail_repair, -1);
                this.ly_sales_receive_itemDetail_repair_wasteTableAdapter.Fill(this.lYSalseMange2.ly_sales_receive_itemDetail_repair_waste, -11);
                this.ly_sales_receive_itemDetail_repair_returnTableAdapter.Fill(this.lYSalseMange2.ly_sales_receive_itemDetail_repair_return, -11);
               
                return;
            }


            int nowId = int.Parse(lY_salesWarranty_queryQCDataGridView.CurrentRow.Cells["detail_id"].Value.ToString());

            this.ly_sales_receive_itemDetail_repair_returnTableAdapter.Fill(this.lYSalseMange2.ly_sales_receive_itemDetail_repair_return, nowId);
            this.ly_sales_receive_itemDetail_repairTableAdapter.Fill(this.lYSalseMange2.ly_sales_receive_itemDetail_repair, nowId);
            this.ly_sales_receive_itemDetail_repair_wasteTableAdapter.Fill(this.lYSalseMange2.ly_sales_receive_itemDetail_repair_waste, nowId);



        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            ExportDataGridviewTOExcell.ExportDataGridview(this.dataGridView1, true);
        }

  

        private void toolStripButton26_Click(object sender, EventArgs e)
        {
            NewFrm.Show(this);
            if (this.radioButton2.Checked)
            {
                this.ly_sales_receive_itemDetail_repairA_AllByWzbh1TableAdapter.Fill(this.lYSalseMange2.ly_sales_receive_itemDetail_repairA_AllByWzbh1, this.dateTimePicker1.Value.Date, this.dateTimePicker2.Value.Date.AddDays(1), "inspect", -1);

            }
            else
            {
                this.ly_sales_receive_itemDetail_repairA_AllByWzbh1TableAdapter.Fill(this.lYSalseMange2.ly_sales_receive_itemDetail_repairA_AllByWzbh1, this.dateTimePicker1.Value.Date, this.dateTimePicker2.Value.Date.AddDays(1), "receive", -1);
            }
            NewFrm.Hide(this);
        }

        private void toolStripButton25_Click(object sender, EventArgs e)
        {
            NewFrm.Show(this);
            if (this.radioButton2.Checked)
            {
                this.ly_sales_receive_itemDetail_repairA_AllByWzbh1TableAdapter.Fill(this.lYSalseMange2.ly_sales_receive_itemDetail_repairA_AllByWzbh1, this.dateTimePicker1.Value.Date, this.dateTimePicker2.Value.Date.AddDays(1), "inspect", 0);

            }
            else
            {
                this.ly_sales_receive_itemDetail_repairA_AllByWzbh1TableAdapter.Fill(this.lYSalseMange2.ly_sales_receive_itemDetail_repairA_AllByWzbh1, this.dateTimePicker1.Value.Date, this.dateTimePicker2.Value.Date.AddDays(1), "receive", 0);
            }
            NewFrm.Hide(this);
        }

        private void toolStripButton24_Click(object sender, EventArgs e)
        {
            NewFrm.Show(this);
            if (this.radioButton2.Checked)
            {
                this.ly_sales_receive_itemDetail_repairA_AllByWzbh1TableAdapter.Fill(this.lYSalseMange2.ly_sales_receive_itemDetail_repairA_AllByWzbh1, this.dateTimePicker1.Value.Date, this.dateTimePicker2.Value.Date.AddDays(1), "inspect", 1);

            }
            else
            {
                this.ly_sales_receive_itemDetail_repairA_AllByWzbh1TableAdapter.Fill(this.lYSalseMange2.ly_sales_receive_itemDetail_repairA_AllByWzbh1, this.dateTimePicker1.Value.Date, this.dateTimePicker2.Value.Date.AddDays(1), "receive", 1);
            }
            NewFrm.Hide(this);
        }

        private void toolStripButton10_Click(object sender, EventArgs e)
        {
            ExportDataGridviewTOExcell.ExportDataGridview(this.ly_sales_receive_itemDetail_repairA_AllByWzbh1DataGridView, true);
        }

        private void toolStripButton11_Click(object sender, EventArgs e)
        {
            NewFrm.Show(this);
            this.lY_salesWarranty_queryQCDataGridView.SelectionChanged -= lY_salesWarranty_queryQCDataGridView_SelectionChanged;
            if (this.radioButton2.Checked)
            {
                //this.lY_salesWarranty_queryQCTableAdapter.Fill(this.lYSalseMange2.LY_salesWarranty_queryQC, this.dateTimePicker1.Value.Date, this.dateTimePicker2.Value.Date.AddDays(1), "inspect");
                this.ly_sales_receive_itemDetail_repairA_AllTableAdapter.Fill(this.lYSalseMange2.ly_sales_receive_itemDetail_repairA_All, this.dateTimePicker1.Value, this.dateTimePicker2.Value.AddDays(1), "inspect");
            }
            else
            {
                //this.lY_salesWarranty_queryQCTableAdapter.Fill(this.lYSalseMange2.LY_salesWarranty_queryQC, this.dateTimePicker1.Value.Date, this.dateTimePicker2.Value.Date.AddDays(1), "receive");
                this.ly_sales_receive_itemDetail_repairA_AllTableAdapter.Fill(this.lYSalseMange2.ly_sales_receive_itemDetail_repairA_All, this.dateTimePicker1.Value, this.dateTimePicker2.Value.AddDays(1), "receive");

            }
            this.lY_salesWarranty_queryQCDataGridView.SelectionChanged += lY_salesWarranty_queryQCDataGridView_SelectionChanged;
            NewFrm.Hide(this);
        }

        private void toolStripTextBox5_KeyUp(object sender, KeyEventArgs e)
        {
            
                 string filterString;

            filterString = GetDataGridviewMultiFilterString.DGVMultiFilterString(this.dataGridView1, this.toolStripTextBox5.Text);
            this.lysalesreceiveitemDetailrepairAAllBindingSource.Filter =  filterString;
        }

        private void toolStripTextBox5_Enter(object sender, EventArgs e)
        {
            toolStripTextBox5.Text = "";

            this.lysalesreceiveitemDetailrepairAAllBindingSource.Filter = " ";
        }
    }
}
