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
using DataGridFilter;
using HappyYF.Infrastructure.Repositories;


 namespace HappyYF.YuanXin.WorkSet
{
    public partial class LY_Receivable_Manage : Form
    {
        public LY_Receivable_Manage()
        {
            InitializeComponent();
            this.getOrderByBussCodeTableAdapter.CommandTimeout = 0;
            this.getOrderByBussCodeNoInvoiceTableAdapter.CommandTimeout = 0;
        }

 

        private void Yonghu_Load(object sender, EventArgs e)
        {

            this.dateTimePicker1.MinDate = DateTime.Parse("2019-06-01");
            this.dateTimePicker7.MinDate = DateTime.Parse("2019-06-01");
            DateTime dtnew = DateTime.Now.AddDays(1 - DateTime.Now.Day).Date;
            string st = dtnew.ToString();
            //DateTime.Parse("2019-06-01").ToString();  
            string et = dtnew.AddMonths(1).AddDays(-1).ToString();
            this.dateTimePicker1.Text = st;
            this.dateTimePicker2.Text = et;


            this.dateTimePicker7.Text = st;
            this.dateTimePicker8.Text = et;
            this.ly_sales_clientreceivablesNewsTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            this.getOrderByBussCodeTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            this.getOrderByBussCodeNoInvoiceTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            this.t_financeReceivablesTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            this.ly_sales_client_otherTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            this.ly_sales_client_other_changeTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;

            this.t_financeReceivables_otherTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;


//            this.ly_sales_client_otherTableAdapter.Fill(this.lYSalseMange.ly_sales_client_other, this.dateTimePicker7.Value
//                , this.dateTimePicker8.Value.AddDays(1));
//            string salespeople = SQLDatabase.NowUserID;
//            if (SQLDatabase.CheckHaveRight(salespeople, "设置客户期初应收"))
//            {
//                this.ly_sales_clientreceivablesNewsTableAdapter.Fill(this.lYSalseMange.ly_sales_clientreceivablesNews, this.dateTimePicker1.Value
//    , this.dateTimePicker2.Value.AddDays(1), "");
//            }
//            else if (SQLDatabase.CheckHaveRight(salespeople, "查看客户期初应收"))
//            {
//                this.ly_sales_clientreceivablesNewsTableAdapter.Fill(this.lYSalseMange.ly_sales_clientreceivablesNews, this.dateTimePicker1.Value
//, this.dateTimePicker2.Value.AddDays(1), "");
//            }
//            else
//            {

//                this.ly_sales_clientreceivablesNewsTableAdapter.Fill(this.lYSalseMange.ly_sales_clientreceivablesNews, this.dateTimePicker1.Value
//, this.dateTimePicker2.Value.AddDays(1), salespeople);

//                //lysalesclientreceivablesNewsBindingSource.Filter = "yhbm='" + salespeople + "'";


//            }
        }

      



 
 
 
  

       
  
        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            ExportDataGridviewTOExcell.ExportDataGridview(this.ly_sales_clientDataGridView, true);
        }

        
      
        private void SaveChanged()
        {

            this.ly_sales_clientDataGridView.EndEdit(); 
            this.Validate();
            this.lysalesclientreceivablesNewsBindingSource.EndEdit();
            this.ly_sales_clientreceivablesNewsTableAdapter.Update(this.lYSalseMange.ly_sales_clientreceivablesNews);
             
 
        }

       
     

        private void ly_sales_clientDataGridView_SelectionChanged_1(object sender, EventArgs e)
        {
       
            loaddata();

            if ((dataGridView1.Rows.Count + dataGridView5.Rows.Count) > 10)
            {
                NewFrm.Show(this);

                CreateRep();
                NewFrm.Hide(this);
            }
            else
            {
                CreateRep();
            }
        }


        protected void loaddata()
        {
            if (this.ly_sales_clientDataGridView.CurrentRow == null)
                return;
         
            string bussCode = this.ly_sales_clientDataGridView.CurrentRow.Cells["客户编码"].Value.ToString();
            this.getOrderByBussCodeTableAdapter.Fill(this.lYSalseMange.GetOrderByBussCode, bussCode, "yes", this.dateTimePicker1.Value
                , this.dateTimePicker2.Value.AddDays(1));

            this.getOrderByBussCodeNoInvoiceTableAdapter.Fill(this.lYSalseMange.GetOrderByBussCodeNoInvoice, bussCode, "no", this.dateTimePicker1.Value
             , this.dateTimePicker2.Value.AddDays(1));
            this.t_financeReceivablesTableAdapter.Fill(this.lYSalseMange.t_financeReceivables,bussCode, this.dateTimePicker1.Value
                , this.dateTimePicker2.Value.AddDays(1));
            AddSummationRow_New2(getOrderByBussCodeBindingSource, dataGridView1);
            AddSummationRow_New2(getOrderByBussCodeNoInvoiceBindingSource, dataGridView5);
        }
        #region
        private void AddSummationRow_New2(BindingSource bs, DataGridView dgv)
        {

            DataRow sumdr = (((DataSet)bs.DataSource).Tables[bs.DataMember]).NewRow();

            if (-1 != bs.Find("税务", "_合计"))
            {
                bs.RemoveAt(bs.Find("税务", "_合计"));
            }

            foreach (DataGridViewRow dgvRow in dgv.Rows)
            {
                foreach (DataGridViewCell dgvCell in dgvRow.Cells)
                {
                    if (dgvCell.Value != null && !(dgvCell.Value is DBNull))
                    {
                        if (IsInteger(dgvCell.Value))
                        {
                            if ("年份" != dgvCell.OwningColumn.HeaderText && "月份" != dgvCell.OwningColumn.HeaderText && "税率" != dgvCell.OwningColumn.HeaderText)
                            {
                                if (sumdr[dgvCell.OwningColumn.DataPropertyName] == null || (sumdr[dgvCell.OwningColumn.DataPropertyName] is DBNull))
                                    sumdr[dgvCell.OwningColumn.DataPropertyName] = 0;


                                   sumdr[dgvCell.OwningColumn.DataPropertyName] = Convert.ToInt64(sumdr[dgvCell.OwningColumn.DataPropertyName]) + Convert.ToInt64(dgvCell.Value);
                            }
                        }
                        else if (IsDecimal(dgvCell.Value))
                        {
                            if ("年份" != dgvCell.OwningColumn.HeaderText && "月份" != dgvCell.OwningColumn.HeaderText && "税率" != dgvCell.OwningColumn.HeaderText)
                            {
                                if (sumdr[dgvCell.OwningColumn.DataPropertyName] == null || (sumdr[dgvCell.OwningColumn.DataPropertyName] is DBNull))
                                    sumdr[dgvCell.OwningColumn.DataPropertyName] = 0;
                                sumdr[dgvCell.OwningColumn.DataPropertyName] = Convert.ToDecimal(sumdr[dgvCell.OwningColumn.DataPropertyName]) + Convert.ToDecimal(dgvCell.Value);
                            }
                        }


                    }
                }

            }


            sumdr["税务"] = "_合计";
            ((DataSet)bs.DataSource).Tables[bs.DataMember].Rows.Add(sumdr);
            bs.ResetBindings(true);

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
        #endregion
    

        private void bindingNavigatorAddNewItem_Click(object sender, EventArgs e)
        {
            if (this.ly_sales_clientDataGridView.CurrentRow == null)
                return;
            string bussCode = this.ly_sales_clientDataGridView.CurrentRow.Cells["客户编码"].Value.ToString();
            string bussName = this.ly_sales_clientDataGridView.CurrentRow.Cells["客户名称"].Value.ToString();

            if (!SQLDatabase.CheckHaveRight(SQLDatabase.NowUserID, "设置客户期初应收"))
            {
                MessageBox.Show("没有操作的权限...", "注意");
                return;
            }
            if (string.IsNullOrEmpty(this.ly_sales_clientDataGridView.CurrentRow.Cells["start_time"].Value.ToString()))
            {
                MessageBox.Show("该客户暂时没有设置期初时间，请先设置...", "注意");
                return;
            }
            LY_add_paymoeny queryForm = new LY_add_paymoeny();
            queryForm.client_code_add = bussCode; 
            queryForm.client_name_add = bussName;
 
 
            queryForm.StartPosition = FormStartPosition.CenterParent;
            queryForm.ShowDialog();

            if (queryForm.DialogResult != DialogResult.Cancel)
            {
                LoadAll(); 

            }
        }

        private void toolStripButton11_Click(object sender, EventArgs e)
        {
            ExportDataGridviewTOExcell.ExportDataGridview(this.dataGridView2, true);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            LoadAll();
        }


        private void LoadAll()
        {
            //NewFrm.Show(this);
           
            string id = "";
            if (ly_sales_clientDataGridView.CurrentRow != null)
            {
                id = ly_sales_clientDataGridView.CurrentRow.Cells["id_b"].Value.ToString();
            }
            this.ly_sales_clientDataGridView.SelectionChanged -= this.ly_sales_clientDataGridView_SelectionChanged_1;

            string sape = SQLDatabase.NowUserID;

            if (SQLDatabase.CheckHaveRight(sape, "设置客户期初应收"))
            {
                this.ly_sales_clientreceivablesNewsTableAdapter.Fill(this.lYSalseMange.ly_sales_clientreceivablesNews, this.dateTimePicker1.Value
            , this.dateTimePicker2.Value.AddDays(1),"");

                if (radioButton2.Checked)
                {
                    lysalesclientreceivablesNewsBindingSource.Filter = " start_time is not null ";
                }
                else
                {
                    this.lysalesclientreceivablesNewsBindingSource.Filter = "";
                }
            }
            else if (SQLDatabase.CheckHaveRight(sape, "查看客户期初应收")) //公司领导级
            {
                this.ly_sales_clientreceivablesNewsTableAdapter.Fill(this.lYSalseMange.ly_sales_clientreceivablesNews, this.dateTimePicker1.Value
               , this.dateTimePicker2.Value.AddDays(1),"");

                if (radioButton2.Checked)
                {
                    lysalesclientreceivablesNewsBindingSource.Filter = " start_time is not null ";
                }
                else
                {
                    this.lysalesclientreceivablesNewsBindingSource.Filter = "";
                }
            }
            else
            {
                string salespeople = sape;
                this.ly_sales_clientreceivablesNewsTableAdapter.Fill(this.lYSalseMange.ly_sales_clientreceivablesNews, this.dateTimePicker1.Value
                            , this.dateTimePicker2.Value.AddDays(1), sape);
                if (radioButton2.Checked)
                {
                 
                    //lysalesclientreceivablesNewsBindingSource.Filter = " start_time is not null  and yhbm='" + salespeople + "' ";
                    lysalesclientreceivablesNewsBindingSource.Filter = " start_time is not null ";
                }
                else
                {
                    this.lysalesclientreceivablesNewsBindingSource.Filter = "";
                    //this.lysalesclientreceivablesNewsBindingSource.Filter = " yhbm='" + salespeople + "' ";
                }
            }
            this.ly_sales_clientDataGridView.SelectionChanged += this.ly_sales_clientDataGridView_SelectionChanged_1;


            if (id != "")
            {
                this.lysalesclientreceivablesNewsBindingSource.Position = this.lysalesclientreceivablesNewsBindingSource.Find("id", id);
            }
            //NewFrm.Hide(this);
           
        }
        private void toolStripTextBox1_Enter_1(object sender, EventArgs e)
        {
            toolStripTextBox1.Text = "";

            this.lysalesclientreceivablesNewsBindingSource.Filter = "";
            //if (SQLDatabase.CheckHaveRight(SQLDatabase.NowUserID, "设置客户期初应收"))
            //{
            //    toolStripTextBox1.Text = "";

            //    this.lysalesclientreceivablesNewsBindingSource.Filter = "";
            //}
            //else if (SQLDatabase.CheckHaveRight(SQLDatabase.NowUserID, "查看客户期初应收"))
            //{
            //    toolStripTextBox1.Text = "";

            //    this.lysalesclientreceivablesNewsBindingSource.Filter = "";
            //}
            //else
            //{

            //    toolStripTextBox1.Text = "";
            //    string salespeople = SQLDatabase.NowUserID;
            //    this.lysalesclientreceivablesNewsBindingSource.Filter = " yhbm='" + salespeople + "' ";
            //}


        }

        private void toolStripTextBox1_KeyUp_1(object sender, KeyEventArgs e)
        {
            this.ly_sales_clientDataGridView.SelectionChanged -= this.ly_sales_clientDataGridView_SelectionChanged_1;



            //if (SQLDatabase.CheckHaveRight(SQLDatabase.NowUserID, "设置客户期初应收"))
            //{
            //    string filterString;

            //    filterString = GetDataGridviewMultiFilterString.DGVMultiFilterString(this.ly_sales_clientDataGridView, this.toolStripTextBox1.Text);

            //    this.lysalesclientreceivablesNewsBindingSource.Filter = filterString;
            //}
            //else if (SQLDatabase.CheckHaveRight(SQLDatabase.NowUserID, "查看客户期初应收"))
            //{
            //    string filterString;

            //    filterString = GetDataGridviewMultiFilterString.DGVMultiFilterString(this.ly_sales_clientDataGridView, this.toolStripTextBox1.Text);

            //    this.lysalesclientreceivablesNewsBindingSource.Filter = filterString;
            //}
            //else
            //{
            //    string filterString;

            //    filterString = GetDataGridviewMultiFilterString.DGVMultiFilterString(this.ly_sales_clientDataGridView, this.toolStripTextBox1.Text);
            //    string salespeople = SQLDatabase.NowUserID;
            //    this.lysalesclientreceivablesNewsBindingSource.Filter = "(" + filterString + ") and yhbm='" + salespeople + "'";

            //}
            string filterString;

            filterString = GetDataGridviewMultiFilterString.DGVMultiFilterString(this.ly_sales_clientDataGridView, this.toolStripTextBox1.Text);

            this.lysalesclientreceivablesNewsBindingSource.Filter = filterString;

            this.ly_sales_clientDataGridView.SelectionChanged += this.ly_sales_clientDataGridView_SelectionChanged_1;
           
            loaddata(); 
            if ((dataGridView1.Rows.Count + dataGridView5.Rows.Count) > 10)
            {
                NewFrm.Show(this); 
                CreateRep();
                NewFrm.Hide(this);
            }
            else
            {
                CreateRep();
            }
       
        }

        private void toolStripTextBox2_Enter(object sender, EventArgs e)
        {

            toolStripTextBox2.Text = "";

            this.getOrderByBussCodeBindingSource.Filter = "";
        }

        private void toolStripTextBox2_KeyUp(object sender, KeyEventArgs e)
        {
            string filterString;

            filterString = GetDataGridviewMultiFilterString.DGVMultiFilterString(this.dataGridView1, this.toolStripTextBox2.Text);

            this.getOrderByBussCodeBindingSource.Filter = filterString;
        }

        private void bindingNavigatorDeleteItem_Click(object sender, EventArgs e)
        {
            if (dataGridView2.CurrentRow == null)
                return;
            if (!SQLDatabase.CheckHaveRight(SQLDatabase.NowUserID, "设置客户期初应收"))
            {
                MessageBox.Show("没有操作的权限...", "注意");
                return;
            }
          

            string id = dataGridView2.CurrentRow.Cells["id"].Value.ToString();
            if (this.ly_sales_clientDataGridView.CurrentRow == null)
                return;
            string bussCode = this.ly_sales_clientDataGridView.CurrentRow.Cells["客户编码"].Value.ToString(); 
            this.t_financeReceivablesTableAdapter.Fill(this.lYSalseMange.t_financeReceivables, bussCode, this.dateTimePicker1.Value
                , this.dateTimePicker2.Value.AddDays(1));
            this.tfinanceReceivablesBindingSource.Position = this.tfinanceReceivablesBindingSource.Find("id", id);

            string salespeople = this.dataGridView2.CurrentRow.Cells["操作人"].Value.ToString();
            if (!string.IsNullOrEmpty(salespeople))
            {
                if (salespeople != SQLDatabase.nowUserName())
                {
                    MessageBox.Show("请操作人:" + salespeople + "删除", "注意");
                    return;
                }
            }
            string remark = dataGridView2.CurrentRow.Cells["remark"].Value.ToString();
            if (!string.IsNullOrEmpty(remark))
            {
                if (remark.Contains("GR") && remark.Contains("调整"))
                {
                    MessageBox.Show("该条数据是转客户产生...", "注意");
                    return;
                }
            }

                if (MessageBox.Show("确定要删除此次到款信息?", "提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                if (dataGridView2.CurrentRow.Index != (dataGridView2.Rows.Count - 1))
                {
                    MessageBox.Show("请先删除最近的到款...", "注意");
                    return;
                }


                string sql = "delete from t_financeReceivables where id="+id;
                int k = 0;
                using (SqlConnection con = new SqlConnection(SQLDatabase.Connectstring))
                {

                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {

                        con.Open();
                       k= cmd.ExecuteNonQuery();
                    }
                }
                if (k > 0)
                {
                    LoadAll(); 
                }
            }
        }

        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            ExportDataGridviewTOExcell.ExportDataGridview(this.dataGridView1, true);
        }

        private void dataGridView2_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (dataGridView2.CurrentRow == null)
            {
                return;
            }
            if (!SQLDatabase.CheckHaveRight(SQLDatabase.NowUserID, "设置客户期初应收"))
            {
                MessageBox.Show("没有操作的权限...", "注意");
                return;
            }
           
            string salespeople = this.dataGridView2.CurrentRow.Cells["操作人"].Value.ToString();
            if (!string.IsNullOrEmpty(salespeople))
            {
                if (salespeople != SQLDatabase.nowUserName())
                {
                    MessageBox.Show("请操作人:" + salespeople + "修改", "注意");
                    return;
                }
            }

            DataGridView dgv = sender as DataGridView;




          
            if ("到款时间2" == dgv.CurrentCell.OwningColumn.Name)
            {
                string id = dataGridView2.CurrentRow.Cells["id"].Value.ToString();
                string chanageid = dataGridView2.CurrentRow.Cells["change_id"].Value.ToString();
                string codeclient = dataGridView2.CurrentRow.Cells["客户编码2"].Value.ToString();
                if (!string.IsNullOrEmpty(chanageid))
                {
                    MessageBox.Show("该条数据是调整应收产生...", "注意");
                    return;
                }
                string remark = dataGridView2.CurrentRow.Cells["到款时间2"].Value.ToString();
                 
                ChangeValue queryForm = new ChangeValue();

                queryForm.OldValue = dgv.CurrentCell.Value.ToString();
                queryForm.NewValue = "";
                queryForm.ChangeMode = "datetime";
                queryForm.ShowDialog();



                if (queryForm.NewValue != "")
                {

                    DataTable dt = null;
                    string sqls = @"select [pay_money_time]  from [t_financeReceivables] 
                                  where client_code='" + codeclient + "' and id<>"+id+" order by id asc";
                    using (SqlConnection connection = new SqlConnection(SQLDatabase.Connectstring))
                    {

                        SqlDataAdapter adapter = new SqlDataAdapter(sqls, connection);
                        DataSet ds = new DataSet();
                        adapter.Fill(ds);
                        dt = ds.Tables[0];
                    }
                    if (dt.Rows.Count > 0)
                    {
                        if (DateTime.Parse(dt.Rows[dt.Rows.Count - 1]["pay_money_time"].ToString()) > DateTime.Parse(queryForm.NewValue))
                        {
                            MessageBox.Show("该次到款时间不能小于上次到款时间，请重新输入");
                            return;
                        }
                    }


                    dgv.CurrentRow.Cells["到款时间2"].Value = queryForm.NewValue;
                    string sql = "update t_financeReceivables set pay_money_time='" + queryForm.NewValue + "' where id=" + id;
                    int k = 0;
                    using (SqlConnection con = new SqlConnection(SQLDatabase.Connectstring))
                    {

                        using (SqlCommand cmd = new SqlCommand(sql, con))
                        {

                            con.Open();
                            k = cmd.ExecuteNonQuery();
                        }
                    }
                    if (k > 0)
                    {

                        //MessageBox.Show("修改成功...", "注意");
                    }
                }

                return;


               

            }

            if ("remark" == dgv.CurrentCell.OwningColumn.Name)
            {
                string id = dataGridView2.CurrentRow.Cells["id"].Value.ToString();
                string chanageid = dataGridView2.CurrentRow.Cells["change_id"].Value.ToString();
                if (!string.IsNullOrEmpty(chanageid))
                {
                    MessageBox.Show("该条数据是调整应收产生...", "注意");
                    return;
                }
                string remark = dataGridView2.CurrentRow.Cells["remark"].Value.ToString();
                if (!string.IsNullOrEmpty(remark))
                {
                    if (remark.Contains("GR") && remark.Contains("调整"))
                    {
                        MessageBox.Show("该条数据是转客户产生...", "注意");
                        return;
                    }
                }
                ChangeValue queryForm = new ChangeValue();

                queryForm.OldValue = dgv.CurrentCell.Value.ToString();
                queryForm.NewValue = "";
                queryForm.ChangeMode = "longstring";
                queryForm.ShowDialog();


                
                if (queryForm.NewValue != "")
                {
                    dgv.CurrentRow.Cells["remark"].Value = queryForm.NewValue;
                    string sql = "update t_financeReceivables set remark='" + queryForm.NewValue + "' where id=" + id;
                    int k = 0;
                    using (SqlConnection con = new SqlConnection(SQLDatabase.Connectstring))
                    {

                        using (SqlCommand cmd = new SqlCommand(sql, con))
                        {

                            con.Open();
                            k = cmd.ExecuteNonQuery();
                        }
                    }
                    if (k > 0)
                    {

                        //MessageBox.Show("修改成功...", "注意");
                    }
                }

                return;
            }
       

            
        }

      

        private void toolStripButton16_Click(object sender, EventArgs e)
        {
            ExportDataGridviewTOExcell.ExportDataGridview(this.dataGridView3, true);
        }

        private void dataGridView3_SelectionChanged(object sender, EventArgs e)
        {
            if (this.dataGridView3.CurrentRow == null)
                return;
            string bussCode = this.dataGridView3.CurrentRow.Cells["客户编码3"].Value.ToString();
          
            this.t_financeReceivables_otherTableAdapter.Fill(this.lYSalseMange.t_financeReceivables_other, bussCode, this.dateTimePicker7.Value
                , this.dateTimePicker8.Value.AddDays(1));

            this.ly_sales_client_other_changeTableAdapter.Fill(this.lYSalseMange.ly_sales_client_other_change, bussCode);
        }

        private void toolStripButton21_Click(object sender, EventArgs e)
        {

            if (this.dataGridView3.CurrentRow == null)
                return;
            if (this.dataGridView6.CurrentRow == null)
                return;
            string bussCode = this.dataGridView3.CurrentRow.Cells["客户编码3"].Value.ToString();
            string bussName = this.dataGridView3.CurrentRow.Cells["客户名称3"].Value.ToString();

            if (!SQLDatabase.CheckHaveRight(SQLDatabase.NowUserID, "设置客户期初应收"))
            {
                MessageBox.Show("没有操作的权限...", "注意");
                return;
            }
            if (!string.IsNullOrEmpty(this.dataGridView6.CurrentRow.Cells["changecode"].Value.ToString()))
            {
                MessageBox.Show("该客户已经调整至新的ERP客户...", "注意");
                return;
            }

            LY_add_paymoeny_other queryForm = new LY_add_paymoeny_other();
            queryForm.client_code_add = bussCode;
            queryForm.client_name_add = bussName;
            queryForm.StartPosition = FormStartPosition.CenterParent;
            queryForm.ShowDialog();

            if (queryForm.DialogResult != DialogResult.Cancel)
            { 
                loadAllOther();
            }
        }

        private void toolStripButton22_Click(object sender, EventArgs e)
        {
            if (dataGridView4.CurrentRow == null)
                return;
            if (!SQLDatabase.CheckHaveRight(SQLDatabase.NowUserID, "设置客户期初应收"))
            {
                MessageBox.Show("没有操作的权限...", "注意");
                return;
            }
            string id = dataGridView4.CurrentRow.Cells["id4"].Value.ToString();
            if (this.dataGridView3.CurrentRow == null)
                return;
            if (this.dataGridView6.CurrentRow == null)
                return;
            string bussCode = this.dataGridView3.CurrentRow.Cells["客户编码3"].Value.ToString();
            this.t_financeReceivables_otherTableAdapter.Fill(this.lYSalseMange.t_financeReceivables_other, bussCode, this.dateTimePicker7.Value
                 , this.dateTimePicker8.Value.AddDays(1));
            this.tfinanceReceivablesotherBindingSource.Position = this.tfinanceReceivablesotherBindingSource.Find("id", id);
            if (!string.IsNullOrEmpty(this.dataGridView6.CurrentRow.Cells["changecode"].Value.ToString()))
            {
                MessageBox.Show("该客户已经调整至新的ERP客户...", "注意");
                return;
            }


            if (MessageBox.Show("确定要删除此次到款信息?", "提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                if (dataGridView4.CurrentRow.Index != (dataGridView4.Rows.Count - 1))
                {
                    MessageBox.Show("请先删除最近的到款...", "注意");
                    return;
                }


                string sql = "delete from t_financeReceivables_other where id=" + id;
                int k = 0;
                using (SqlConnection con = new SqlConnection(SQLDatabase.Connectstring))
                {

                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {

                        con.Open();
                        k = cmd.ExecuteNonQuery();
                    }
                }
                if (k > 0)
                {
                    loadAllOther(); 
       
                }
            }
        }

        

        private void toolStripButton23_Click(object sender, EventArgs e)
        {
            ExportDataGridviewTOExcell.ExportDataGridview(this.dataGridView4, true);
        }

        private void toolStripTextBox5_Enter(object sender, EventArgs e)
        {
            toolStripTextBox5.Text = "";

            this.lysalesclientotherBindingSource.Filter = "";
        }

        private void toolStripTextBox5_KeyUp(object sender, KeyEventArgs e)
        {
            string filterString;

            filterString = GetDataGridviewMultiFilterString.DGVMultiFilterString(this.dataGridView3, this.toolStripTextBox5.Text);

            this.lysalesclientotherBindingSource.Filter = filterString;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            loadAllOther();
        }
        protected void loadAllOther()
        {
            string id = "";
            if (dataGridView3.CurrentRow != null)
            {
                id = dataGridView3.CurrentRow.Cells["id_c"].Value.ToString();
            }

            this.ly_sales_client_otherTableAdapter.Fill(this.lYSalseMange.ly_sales_client_other, this.dateTimePicker7.Value
                , this.dateTimePicker8.Value.AddDays(1));
            if (id != "")
            {
                this.lysalesclientotherBindingSource.Position = this.lysalesclientotherBindingSource.Find("id", id);
            }
        }

        private void dataGridView4_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (dataGridView4.CurrentRow == null)
            {
                return;
            }
            if (!SQLDatabase.CheckHaveRight(SQLDatabase.NowUserID, "设置客户期初应收"))
            {
                MessageBox.Show("没有操作的权限...", "注意");
                return;
            }

            string salespeople = this.dataGridView4.CurrentRow.Cells["操作人4"].Value.ToString();
            if (!string.IsNullOrEmpty(salespeople))
            {
                if (salespeople != SQLDatabase.nowUserName())
                {
                    MessageBox.Show("请操作人:" + salespeople + "修改", "注意");
                    return;
                }
            }

            DataGridView dgv = sender as DataGridView;

            if ("到款时间3" == dgv.CurrentCell.OwningColumn.Name)
            {
                string id = dataGridView4.CurrentRow.Cells["id4"].Value.ToString();
                string codeclient = dataGridView4.CurrentRow.Cells["客户编码4"].Value.ToString();

                ChangeValue queryForm = new ChangeValue();

                queryForm.OldValue = dgv.CurrentCell.Value.ToString();
                queryForm.NewValue = "";
                queryForm.ChangeMode = "datetime";
                queryForm.ShowDialog();



                if (queryForm.NewValue != "")
                {
                    
                    DataTable dt = null;
                    string sqls = @"select [pay_money_time]  from [t_financeReceivables_other] 
                                  where client_code='" + codeclient + "' and id<>" + id + " order by id asc";
                    using (SqlConnection connection = new SqlConnection(SQLDatabase.Connectstring))
                    {

                        SqlDataAdapter adapter = new SqlDataAdapter(sqls, connection);
                        DataSet ds = new DataSet();
                        adapter.Fill(ds);
                        dt = ds.Tables[0];
                    }
                    if (dt.Rows.Count > 0)
                    {
                        if (DateTime.Parse(dt.Rows[dt.Rows.Count - 1]["pay_money_time"].ToString()) > DateTime.Parse(queryForm.NewValue))
                        {
                            MessageBox.Show("该次到款时间不能小于上次到款时间，请重新输入");
                            return;
                        }
                    }


                    dgv.CurrentRow.Cells["到款时间3"].Value = queryForm.NewValue;
                    string sql = "update t_financeReceivables_other set pay_money_time='" + queryForm.NewValue + "' where id=" + id;
                    int k = 0;
                    using (SqlConnection con = new SqlConnection(SQLDatabase.Connectstring))
                    {

                        using (SqlCommand cmd = new SqlCommand(sql, con))
                        {

                            con.Open();
                            k = cmd.ExecuteNonQuery();
                        }
                    }
                    if (k > 0)
                    {
                    }
                }

                return;
            }


            if ("remake4" == dgv.CurrentCell.OwningColumn.Name)
            {
                string id = dataGridView4.CurrentRow.Cells["id4"].Value.ToString();
             

                ChangeValue queryForm = new ChangeValue();

                queryForm.OldValue = dgv.CurrentCell.Value.ToString();
                queryForm.NewValue = "";
                queryForm.ChangeMode = "longstring";
                queryForm.ShowDialog();



                if (queryForm.NewValue != "")
                {
                    dgv.CurrentRow.Cells["remake4"].Value = queryForm.NewValue;
                    string sql = "update t_financeReceivables_other set remark='" + queryForm.NewValue + "' where id=" + id;
                    int k = 0;
                    using (SqlConnection con = new SqlConnection(SQLDatabase.Connectstring))
                    {

                        using (SqlCommand cmd = new SqlCommand(sql, con))
                        {

                            con.Open();
                            k = cmd.ExecuteNonQuery();
                        }
                    }
                    if (k > 0)
                    { 
                    }
                }

                return;
            }
        }

        private void toolStripButton24_Click(object sender, EventArgs e)
        {
            if (!SQLDatabase.CheckHaveRight(SQLDatabase.NowUserID, "设置客户期初应收"))
            {
                MessageBox.Show("没有操作的权限...", "注意");
                return;
            }
            LY_add_clientOther queryForm = new LY_add_clientOther();
           
            queryForm.StartPosition = FormStartPosition.CenterParent;
            queryForm.ShowDialog();

            if (queryForm.DialogResult != DialogResult.Cancel)
            {
                this.ly_sales_client_otherTableAdapter.Fill(this.lYSalseMange.ly_sales_client_other, this.dateTimePicker7.Value
                , this.dateTimePicker8.Value.AddDays(1));
            }
        
        }
        #region
        private void 设置应收数据ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.ly_sales_clientDataGridView.CurrentRow == null)
                return;

            if (!SQLDatabase.CheckHaveRight(SQLDatabase.NowUserID, "设置客户期初应收"))
            {
                MessageBox.Show("没有操作的权限...", "注意");
                return;
            }
            if (dataGridView2.Rows.Count > 0)
            {
                MessageBox.Show("已经产生后续计算...", "注意");
                return;
            }
            if (this.ly_sales_clientDataGridView.CurrentRow.Cells["start_time"].Value.ToString() != "")
            {
                MessageBox.Show("已经设置期初...", "注意");
                return;
            }
                
            LY_add_clientERP queryForm = new LY_add_clientERP();
     

            queryForm.client_code_add = this.ly_sales_clientDataGridView.CurrentRow.Cells["客户编码"].Value.ToString();
            queryForm.client_code_id = this.ly_sales_clientDataGridView.CurrentRow.Cells["id_b"].Value.ToString();
            queryForm.StartPosition = FormStartPosition.CenterParent;
            queryForm.ShowDialog();
      
            if (queryForm.DialogResult != DialogResult.Cancel)
            {
                LoadAll();
            }
         
        }
 
        #endregion

        private void toolStripTextBox8_Enter(object sender, EventArgs e)
        {
            toolStripTextBox8.Text = "";

            this.getOrderByBussCodeNoInvoiceBindingSource.Filter = "";
        }

        private void toolStripTextBox8_KeyUp(object sender, KeyEventArgs e)
        {
            string filterString;

            filterString = GetDataGridviewMultiFilterString.DGVMultiFilterString(this.dataGridView5, this.toolStripTextBox8.Text);

            this.getOrderByBussCodeNoInvoiceBindingSource.Filter = filterString;
        }

        private void toolStripButton29_Click(object sender, EventArgs e)
        {
            ExportDataGridviewTOExcell.ExportDataGridview(this.dataGridView5, true);
        }
        #region
        private void dataGridView6_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (!SQLDatabase.CheckHaveRight(SQLDatabase.NowUserID, "设置客户期初应收"))
            {
                MessageBox.Show("没有操作的权限...", "注意");
                return;
            }
            if (null == this.dataGridView6.CurrentRow) return;
            DataGridView dgv = sender as DataGridView;
            if (!string.IsNullOrEmpty(dgv.CurrentRow.Cells["changecode"].Value.ToString()))
            {
                MessageBox.Show("该客户已经调整过一次，并且生成记录，如果需要再次调整，请联系管理员，进行谨慎操作...", "注意");
                return;
            }
            string client_old = this.dataGridView6.CurrentRow.Cells["salesclientcode"].Value.ToString();

            if ("changecode" == dgv.CurrentCell.OwningColumn.Name || "changename" == dgv.CurrentCell.OwningColumn.Name)
            {
                 
                string sel = "select  salesclient_code as 新客户编码, salesclient_name as 新客户名称 from ly_sales_client  ";
                QueryForm queryForm = new QueryForm();
                queryForm.Sel = sel;
                queryForm.Constr = SQLDatabase.Connectstring;
                queryForm.ShowDialog();

                if (queryForm.Result != "")
                {
                    if (string.IsNullOrEmpty(queryForm.Result))
                    {
                        return;
                    }

                    DataTable dt_newqu = null;
                    string sqlqu = "select start_time from  ly_sales_client where salesclient_code='"+ queryForm.Result + "'";
                    using (SqlConnection connection = new SqlConnection(SQLDatabase.Connectstring))
                    {

                        SqlDataAdapter adapter = new SqlDataAdapter(sqlqu, connection);
                        DataSet ds = new DataSet();
                        adapter.Fill(ds);
                        dt_newqu = ds.Tables[0];
                    }
                    if (dt_newqu.Rows.Count > 0)
                    {
                        if (string.IsNullOrEmpty(dt_newqu.Rows[0][0].ToString()))
                        {
                            MessageBox.Show("请先设置调整后客户的期初...", "注意");
                            return;
                        }

                    }
                    else
                    {

                        MessageBox.Show("没有操作的客户...", "注意");
                        return;
                    }



                    //查询旧客户的付款记录
                    DataTable dt_new = null;
                    string sql_new = @"select [id],[client_code],[start_yingshou],[start_yushou],[pay_money],[pay_money_time]  ,[pay_moeny_peo],[sys_time],[order_start_time],[now_yingshou],[now_yingshou_end] ,[now_yushou_end],isnull(now_yingshou_change,0) as now_yingshou_change
                           from [t_financeReceivables_other] where client_code='" + client_old + "' order by id asc";
                    using (SqlConnection connection = new SqlConnection(SQLDatabase.Connectstring))
                    {

                        SqlDataAdapter adapter = new SqlDataAdapter(sql_new, connection);
                        DataSet ds = new DataSet();
                        adapter.Fill(ds);
                        dt_new = ds.Tables[0];
                    }
                    decimal m = 0;
                    decimal n = 0;
                    if (dt_new.Rows.Count <= 0)
                    {
                        //第一次 插入数据
                        DataTable dt_client = null;
                        string sql_client = @"select isnull(start_yingshou_money,0),isnull(strart_yushou_money,0) from ly_sales_client_other where salesclient_code='" + client_old + "'";
                        using (SqlConnection connection = new SqlConnection(SQLDatabase.Connectstring))
                        {

                            SqlDataAdapter adapter = new SqlDataAdapter(sql_client, connection);
                            DataSet ds = new DataSet();
                            adapter.Fill(ds);
                            dt_client = ds.Tables[0];
                        }
                        m = decimal.Parse(dt_client.Rows[0][0].ToString());
                        n = decimal.Parse(dt_client.Rows[0][1].ToString());

                    }
                    else
                    {
                         
                        m = decimal.Parse(dt_new.Rows[dt_new.Rows.Count - 1]["now_yingshou_end"].ToString()); //最近一次的应收
                        n = decimal.Parse(dt_new.Rows[dt_new.Rows.Count - 1]["now_yushou_end"].ToString());//最近一次的预收 
                    }




                    DataTable dt = null;
                    string sql = @"select [id],[client_code],[start_yingshou],[start_yushou],[pay_money],[pay_money_time]  ,[pay_moeny_peo],[sys_time],[order_start_time],[now_yingshou],[now_yingshou_end] ,[now_yushou_end],isnull(now_yingshou_change,0) as now_yingshou_change
                           from[t_financeReceivables] where client_code='" + queryForm.Result + "' order by id asc";
                    using (SqlConnection connection = new SqlConnection(SQLDatabase.Connectstring))
                    {

                        SqlDataAdapter adapter = new SqlDataAdapter(sql, connection);
                        DataSet ds = new DataSet();
                        adapter.Fill(ds);
                        dt = ds.Tables[0];
                    }
                    decimal daokuan = n - m;
              
                
                
                    decimal yingshou = 0; //本期应收

                    decimal chushiyingshou = 0;
                    decimal chushiyushou = 0;
                    if (dt.Rows.Count <= 0)
                    {
                        //第一次 插入数据
                        DataTable dt_client = null;
                        string sql_client = @"select isnull(start_yingshou_money,0),isnull(strart_yushou_money,0) from ly_sales_client where salesclient_code='" + queryForm.Result + "'";
                        using (SqlConnection connection = new SqlConnection(SQLDatabase.Connectstring))
                        {

                            SqlDataAdapter adapter = new SqlDataAdapter(sql_client, connection);
                            DataSet ds = new DataSet();
                            adapter.Fill(ds);
                            dt_client = ds.Tables[0];
                        }
                        chushiyingshou = decimal.Parse(dt_client.Rows[0][0].ToString());
                        chushiyushou = decimal.Parse(dt_client.Rows[0][1].ToString());

                    }
                 
                    decimal yue = yingshou + chushiyingshou - chushiyushou - daokuan; //本次应收+初次应收-初次预收-到款 =当前余额



                 



                    SqlConnection myConn = new SqlConnection(SQLDatabase.Connectstring);
                    myConn.Open();
                    SqlCommand myComm = new SqlCommand();
                    try
                    {
                        myComm.Connection = myConn;


                        myComm.CommandText = @"INSERT INTO  [t_financeReceivables]
                                              ([client_code],[start_yingshou]
                                              ,[start_yushou] ,[pay_money],[pay_money_time]
                                              ,[pay_moeny_peo]  
                                              ,[now_yingshou] ,[now_yingshou_end],[now_yushou_end] ,[remark]           
                                              )
                                VALUES
                                              ('" + queryForm.Result + "'," + chushiyingshou + "," + chushiyushou + "," + daokuan + ",'" +
                                               DateTime.Now+ "','" + SQLDatabase.nowUserName() +
                                                "', " + yingshou + ",0,0,'" + client_old + " 调整') ";

                        myComm.CommandText +=  @" INSERT INTO  [t_financeReceivables_other]
                                              ([client_code]
                                              ,[start_yingshou]
                                              ,[start_yushou]
                                              ,[pay_money]
                                              ,[pay_money_time]
                                              ,[pay_moeny_peo] 
                                 
                                              ,[now_yingshou]
                                              ,[now_yingshou_end]
                                              ,[now_yushou_end]
                                              ,[remark]           
                                              )
                                VALUES
                                              ('" + client_old + "'," + m + "," + n + "," + (0-daokuan) + ",'" +
                                                     DateTime.Now + "','" + SQLDatabase.nowUserName() +
                                                      "' ," + yingshou + ",0,0, '" + client_old + " 调整')";

                      
                        myComm.ExecuteNonQuery();






                        //验证通过

                        dgv.CurrentRow.Cells["changecode"].Value = queryForm.Result;
                        dgv.CurrentRow.Cells["changename"].Value = queryForm.Result1;
                        dgv.CurrentRow.Cells["changetime"].Value = DateTime.Now;
                        dgv.CurrentRow.Cells["changepeo"].Value = SQLDatabase.nowUserName();
                        this.dataGridView6.EndEdit(); 
                        this.Validate();
                        this.lysalesclientotherchangeBindingSource.EndEdit(); 
                        this.ly_sales_client_other_changeTableAdapter.Update(this.lYSalseMange.ly_sales_client_other_change);
                        loadAllOther();
                    }
                    catch (Exception err)
                    {
                        throw new ApplicationException("事务操作出错，系统信息：" + err.Message);
                    }
                    finally
                    {
                        myConn.Close();
                    }

                }

                
                return;

            }

        }
        #endregion



        #region
        protected void CreateRep()
        {
            if (this.ly_sales_clientDataGridView.CurrentRow == null)
                return;

            dataGridView7.DataSource = null;
            //初始化表格 
            int m = dataGridView5.Rows.Count - 1; //未开票
            int n = dataGridView1.Rows.Count - 1; //已开票
            int k = dataGridView2.Rows.Count; //付款记录
            int p = 1;//客户记录
            int[] arry = new int[] { (m + n), k, p };
            Array.Sort(arry);
            int maxRow = arry[arry.Length - 1]; //多少行数据


            DataTable dt = new DataTable("mytable");
            DataColumn dc1 = new DataColumn("客户编码", Type.GetType("System.String"));
            DataColumn dc2 = new DataColumn("期初应收", Type.GetType("System.Decimal"));
            DataColumn dc3 = new DataColumn("期初预收", Type.GetType("System.Decimal"));
            DataColumn dc4 = new DataColumn("到款金额", Type.GetType("System.Decimal"));
            DataColumn dc5 = new DataColumn("到款时间", Type.GetType("System.DateTime"));
            DataColumn dc6 = new DataColumn("备注", Type.GetType("System.String"));
            DataColumn dc7 = new DataColumn("清单日期", Type.GetType("System.DateTime"));
            DataColumn dc8 = new DataColumn("清单号", Type.GetType("System.String"));
            DataColumn dc9 = new DataColumn("合同号", Type.GetType("System.String"));
            DataColumn dc10 = new DataColumn("合同金额", Type.GetType("System.Decimal"));
            DataColumn dc11 = new DataColumn("开票日期", Type.GetType("System.DateTime"));
            DataColumn dc12 = new DataColumn("发票号", Type.GetType("System.String"));
            DataColumn dc13 = new DataColumn("开票金额", Type.GetType("System.Decimal"));
            DataColumn dc14 = new DataColumn("应收结余", Type.GetType("System.Decimal"));
            DataColumn dc15 = new DataColumn("预收结余", Type.GetType("System.Decimal"));
            dt.Columns.Add(dc1);
            dt.Columns.Add(dc2);
            dt.Columns.Add(dc3);
            dt.Columns.Add(dc4);
            dt.Columns.Add(dc5);
            dt.Columns.Add(dc6);
            dt.Columns.Add(dc7);
            dt.Columns.Add(dc8);
            dt.Columns.Add(dc9);
            dt.Columns.Add(dc10);
            dt.Columns.Add(dc11);
            dt.Columns.Add(dc12);
            dt.Columns.Add(dc13);
            dt.Columns.Add(dc14);
            dt.Columns.Add(dc15);
            for (int i = 0; i < maxRow; i++)
            {
                DataRow dr = dt.NewRow();
                dr["客户编码"] = null;
                dr["期初应收"] = DBNull.Value;
                dr["期初预收"] = DBNull.Value;
                dr["到款金额"] = DBNull.Value;
                dr["到款时间"] = DBNull.Value;
                dr["备注"] = null;
                dr["清单日期"] = DBNull.Value;
                dr["清单号"] = null;
                dr["合同号"] = null;
                dr["合同金额"] = DBNull.Value;
                dr["开票日期"] = DBNull.Value;
                dr["发票号"] = null;
                dr["开票金额"] = DBNull.Value;
                dr["应收结余"] = DBNull.Value;
                dr["预收结余"] = DBNull.Value;
                dt.Rows.Add(dr);
            }

            string client_code = this.ly_sales_clientDataGridView.CurrentRow.Cells["客户编码"].Value.ToString();
            string sql_c = @"select  start_yingshou  ,start_yushou   ,end_yingshou   ,end_yushou ,kaipiao ,fukuan from  dbo.f_GetMoneyNew('" + client_code + "'," +
                   "'" + DateTime.Parse(this.dateTimePicker1.Text).Date + "','" + DateTime.Parse(this.dateTimePicker2.Text).Date.AddDays(1) + "')";
            DataTable dt_client_c = null;
            using (SqlConnection connection = new SqlConnection(SQLDatabase.Connectstring))
            {

                SqlDataAdapter adapter = new SqlDataAdapter(sql_c, connection);
                DataSet ds = new DataSet();
                adapter.Fill(ds);
                dt_client_c = ds.Tables[0];
            }



            decimal start_yingshou = decimal.Parse(dt_client_c.Rows[0]["start_yingshou"].ToString()); ;
            decimal start_yushou = decimal.Parse(dt_client_c.Rows[0]["start_yushou"].ToString());
            decimal end_yingshou = decimal.Parse(dt_client_c.Rows[0]["end_yingshou"].ToString()); ;
            decimal end_yushou = decimal.Parse(dt_client_c.Rows[0]["end_yushou"].ToString());



            //未开票
            for (int i = 0; i < m; i++)
            {
                dt.Rows[i]["清单日期"] = DateTime.Parse(dataGridView5.Rows[i].Cells["清单日期2"].Value.ToString());
                dt.Rows[i]["清单号"] = dataGridView5.Rows[i].Cells["清单号2"].Value.ToString();
                dt.Rows[i]["合同号"] = dataGridView5.Rows[i].Cells["合同号2"].Value.ToString();
                dt.Rows[i]["合同金额"] = decimal.Parse(dataGridView5.Rows[i].Cells["金额2"].Value.ToString());

            }
            //已开票
            for (int i = 0; i < n; i++)
            {

                dt.Rows[i + m]["清单日期"] = DateTime.Parse(dataGridView1.Rows[i].Cells["清单日期1"].Value.ToString());
                dt.Rows[i + m]["清单号"] = dataGridView1.Rows[i].Cells["清单号1"].Value.ToString();
                dt.Rows[i + m]["合同号"] = dataGridView1.Rows[i].Cells["合同号1"].Value.ToString();
                dt.Rows[i + m]["合同金额"] = decimal.Parse(dataGridView1.Rows[i].Cells["金额1"].Value.ToString());
                dt.Rows[i + m]["开票日期"] = DateTime.Parse(dataGridView1.Rows[i].Cells["开票日期1"].Value.ToString());

                dt.Rows[i + m]["发票号"] = dataGridView1.Rows[i].Cells["发票号1"].Value.ToString();
                if (!string.IsNullOrEmpty(dataGridView1.Rows[i].Cells["发票号1"].Value.ToString()) && !string.IsNullOrEmpty(dataGridView1.Rows[i].Cells["开票日期1"].Value.ToString()))
                {
                    dt.Rows[i + m]["开票金额"] = decimal.Parse(dataGridView1.Rows[i].Cells["金额1"].Value.ToString());
                }

            }


            dt.DefaultView.Sort = "清单日期 asc";//按Id倒序

            dt = dt.DefaultView.ToTable();//返回一个新的DataTable

            for (int i = 0; i < k; i++)
            {

                dt.Rows[i]["到款金额"] = decimal.Parse(dataGridView2.Rows[i].Cells["到款金额2"].Value.ToString());
                dt.Rows[i]["到款时间"] = DateTime.Parse(dataGridView2.Rows[i].Cells["到款时间2"].Value.ToString());
                dt.Rows[i]["备注"] = dataGridView2.Rows[i].Cells["remark"].Value.ToString();

            }


            dt.Rows[0]["客户编码"] = client_code;
            dt.Rows[0]["期初应收"] = start_yingshou;
            dt.Rows[0]["期初预收"] = start_yushou;
            dt.Rows[0]["应收结余"] = end_yingshou;
            dt.Rows[0]["预收结余"] = end_yushou;

            decimal qc_1 = 0;
            decimal qc_2 = 0;

            decimal dk_1 = 0;
            decimal ht_1 = 0;
            decimal fp_1 = 0;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                qc_1 += string.IsNullOrEmpty(dt.Rows[i]["期初应收"].ToString()) == true ? 0 : decimal.Parse(dt.Rows[i]["期初应收"].ToString());
                qc_2 += string.IsNullOrEmpty(dt.Rows[i]["期初预收"].ToString()) == true ? 0 : decimal.Parse(dt.Rows[i]["期初预收"].ToString());
                dk_1 += string.IsNullOrEmpty(dt.Rows[i]["到款金额"].ToString()) == true ? 0 : decimal.Parse(dt.Rows[i]["到款金额"].ToString());
                ht_1 += string.IsNullOrEmpty(dt.Rows[i]["合同金额"].ToString()) == true ? 0 : decimal.Parse(dt.Rows[i]["合同金额"].ToString());
                fp_1 += string.IsNullOrEmpty(dt.Rows[i]["开票金额"].ToString()) == true ? 0 : decimal.Parse(dt.Rows[i]["开票金额"].ToString());
            }

            DataRow dr_1 = dt.NewRow();
            dr_1["客户编码"] = "_小计";
            dr_1["期初应收"] = qc_1;
            dr_1["期初预收"] = qc_2;
            dr_1["到款金额"] = dk_1;
            dr_1["到款时间"] = DBNull.Value;
            dr_1["备注"] = null;
            dr_1["清单日期"] = DBNull.Value;
            dr_1["清单号"] = null;
            dr_1["合同号"] = null;
            dr_1["合同金额"] = ht_1;
            dr_1["开票日期"] = DBNull.Value;
            dr_1["发票号"] = null;
            dr_1["开票金额"] = fp_1;
            dr_1["应收结余"] = DBNull.Value;
            dr_1["预收结余"] = DBNull.Value;
            dt.Rows.Add(dr_1);



            decimal fin = 0;
            fin = qc_2 + dk_1 - qc_1 - ht_1;



            DataRow dr_2 = dt.NewRow();


            dr_2["客户编码"] = "_总欠款";

            dr_2["期初应收"] = fin;
            dr_2["期初预收"] = DBNull.Value;
            dr_2["到款金额"] = DBNull.Value;
            dr_2["到款时间"] = DBNull.Value;
            dr_2["备注"] = null;
            dr_2["清单日期"] = DBNull.Value;
            dr_2["清单号"] = null;
            dr_2["合同号"] = null;
            dr_2["合同金额"] = DBNull.Value;
            dr_2["开票日期"] = DBNull.Value;
            dr_2["发票号"] = null;
            dr_2["开票金额"] = DBNull.Value;

            dt.Rows.Add(dr_2);


            dataGridView7.DataSource = dt;
            DataGridViewCellStyle dgvcs = new DataGridViewCellStyle();
            dgvcs.BackColor = Color.Plum;
            for (int i = 0; i < this.dataGridView7.Columns.Count; i++)
            {

                this.dataGridView7.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
                if (i == 7 || i == 8)
                { }
                else
                {
                    this.dataGridView7.Columns[i].Width = 77;
                }
                if (i == 13 || i == 14)
                {
                    dataGridView7.Columns[i].DefaultCellStyle = dgvcs;  //整列
                }
            }
        }



        #endregion

        private void toolStripButton34_Click(object sender, EventArgs e)
        {
            ExportDataGridviewTOExcell.ExportDataGridview(this.dataGridView7, true);
        }

        private void 取消期初设置ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.ly_sales_clientDataGridView.CurrentRow == null)
                return;

            if (!SQLDatabase.CheckHaveRight(SQLDatabase.NowUserID, "设置客户期初应收"))
            {
                MessageBox.Show("没有操作的权限...", "注意");
                return;
            }
           
            string client_code_add = this.ly_sales_clientDataGridView.CurrentRow.Cells["客户编码"].Value.ToString();
            SqlConnection myConn = new SqlConnection(SQLDatabase.Connectstring);
            myConn.Open();
            SqlCommand myComm = new SqlCommand();
            try
            {
                myComm.Connection = myConn;


                myComm.CommandText = @"update ly_sales_client set start_time=null ,start_yingshou_money=null,strart_yushou_money=null 
                                     where salesclient_code='"+ client_code_add + "' ";

                myComm.CommandText += @" delete from t_financeReceivables where client_code='" + client_code_add + "'";


                myComm.ExecuteNonQuery();
                LoadAll();

            }
            catch (Exception err)
            {
                throw new ApplicationException("事务操作出错，系统信息：" + err.Message);
            }
            finally
            {
                myConn.Close();
            }
        }

        private void dataGridView5_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (dataGridView5.CurrentRow == null)
            {
                return;
            }
            if (!SQLDatabase.CheckHaveRight(SQLDatabase.NowUserID, "设置客户期初应收"))
            {
                MessageBox.Show("没有操作的权限...", "注意");
                return;
            }

            

            DataGridView dgv = sender as DataGridView;

            if ("备注" == dgv.CurrentCell.OwningColumn.Name)
            {
                string qdh2 = dataGridView5.CurrentRow.Cells["清单号2"].Value.ToString();
               
             
                ChangeValue queryForm = new ChangeValue();

                queryForm.OldValue = dgv.CurrentCell.Value.ToString();
                queryForm.NewValue = "";
                queryForm.ChangeMode = "longstring";
                queryForm.ShowDialog();



                if (queryForm.NewValue != "")
                {
                    dgv.CurrentRow.Cells["备注"].Value = queryForm.NewValue;
                    string sql = "";
                    if (qdh2.Contains("XS"))
                    {
                        sql = "update ly_sales_contract_main set inner_remark='" + queryForm.NewValue + "' where contract_inner_code='"+ qdh2 + "'";

                    }
                    else
                    {
                        sql = "update ly_sales_receive set inner_remark='" + queryForm.NewValue + "' where repair_bill_code='" + qdh2 + "'";

                    }
                    int k = 0;
                    using (SqlConnection con = new SqlConnection(SQLDatabase.Connectstring))
                    {

                        using (SqlCommand cmd = new SqlCommand(sql, con))
                        {

                            con.Open();
                            k = cmd.ExecuteNonQuery();
                        }
                    }
                    if (k > 0)
                    {

                        //MessageBox.Show("修改成功...", "注意");
                    }
                }

                return;
            }


        }

        private void dataGridView1_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (dataGridView1.CurrentRow == null)
            {
                return;
            }
            if (!SQLDatabase.CheckHaveRight(SQLDatabase.NowUserID, "设置客户期初应收"))
            {
                MessageBox.Show("没有操作的权限...", "注意");
                return;
            }



            DataGridView dgv = sender as DataGridView;

            if ("inner_remark" == dgv.CurrentCell.OwningColumn.Name)
            {
                string qdh2 = dataGridView1.CurrentRow.Cells["清单号1"].Value.ToString();


                ChangeValue queryForm = new ChangeValue();

                queryForm.OldValue = dgv.CurrentCell.Value.ToString();
                queryForm.NewValue = "";
                queryForm.ChangeMode = "longstring";
                queryForm.ShowDialog();



                if (queryForm.NewValue != "")
                {
                    dgv.CurrentRow.Cells["inner_remark"].Value = queryForm.NewValue;
                    string sql = "";
                    if (qdh2.Contains("XS"))
                    {
                        sql = "update ly_sales_contract_main set inner_remark='" + queryForm.NewValue + "' where contract_inner_code='" + qdh2 + "'";

                    }
                    else
                    {
                        sql = "update ly_sales_receive set inner_remark='" + queryForm.NewValue + "' where repair_bill_code='" + qdh2 + "'";

                    }
                    int k = 0;
                    using (SqlConnection con = new SqlConnection(SQLDatabase.Connectstring))
                    {

                        using (SqlCommand cmd = new SqlCommand(sql, con))
                        {

                            con.Open();
                            k = cmd.ExecuteNonQuery();
                        }
                    }
                    if (k > 0)
                    {

                        //MessageBox.Show("修改成功...", "注意");
                    }
                }

                return;
            }
        }

        private void toolStripButton4_Click_1(object sender, EventArgs e)
        {
            ExportDataGridviewTOExcell.ExportDataGridview(this.ly_sales_clientDataGridView, true);
        }
    }
}
