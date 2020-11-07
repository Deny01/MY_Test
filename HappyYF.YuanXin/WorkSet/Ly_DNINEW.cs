using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DataGridFilter;
using HappyYF.Infrastructure.Repositories;

namespace HappyYF.YuanXin.WorkSet
{
    public partial class Ly_DNINEW : Form
    {
        public Ly_DNINEW()
        {
           
            InitializeComponent();
            this.getOrderByBussCodeAllTableAdapter.CommandTimeout = 0;
        }

       

        private void button1_Click(object sender, EventArgs e)
        {
            NewFrm.Show(this);


            string salespeople =SQLDatabase.NowUserID;
            if (SQLDatabase.CheckHaveRight(salespeople, "设置客户期初应收"))
            {
                this.getOrderByBussCodeAllTableAdapter.Fill(this.lYSalseMange.GetOrderByBussCodeAll, this.dateTimePicker1.Value
               , this.dateTimePicker2.Value.AddDays(1),"");

            }
            else if (SQLDatabase.CheckHaveRight(salespeople, "查看客户期初应收"))
            {
                this.getOrderByBussCodeAllTableAdapter.Fill(this.lYSalseMange.GetOrderByBussCodeAll, this.dateTimePicker1.Value
                           , this.dateTimePicker2.Value.AddDays(1), "");
                
            }
            else
            {


                this.getOrderByBussCodeAllTableAdapter.Fill(this.lYSalseMange.GetOrderByBussCodeAll, this.dateTimePicker1.Value
               , this.dateTimePicker2.Value.AddDays(1), salespeople); 

            }
           


            AddSummationRow_New(getOrderByBussCodeAllBindingSource, dataGridView5);



            NewFrm.Hide(this);
        }

        private void LY_Quality_Control_PurchaseRep_Load(object sender, EventArgs e)
        {
            this.dateTimePicker1.MinDate = DateTime.Parse("2019-01-01");
       
            this.dateTimePicker1.Text = DateTime.Parse("2019-06-01").ToString();
            this.dateTimePicker2.Text = DateTime.Today.Date.ToString();
            //this.dateTimePicker3.MinDate = DateTime.Parse("2019-01-01");
            this.dateTimePicker3.Text = new DateTime(DateTime.Now.Year, 1, 1).ToString("yyyy-MM-dd");
            this.dateTimePicker4.Text = DateTime.Today.Date.ToString();

            this.getOrderByBussCodeAllTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            this.getOrderWzbhByBussCodeAllTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;

            this.getOrderByBussCodeAll_ByOutTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;


            string salespeople = SQLDatabase.NowUserID;
            if (SQLDatabase.CheckHaveRight(salespeople, "设置客户期初应收"))
            {
               

            }
            else if (SQLDatabase.CheckHaveRight(salespeople, "查看客户期初应收"))
            {

                tabPage4.Parent = null;
                tabPage2.Parent = null;
                this.dataGridView5.Columns[19].Visible = false;
            }
            else
            {


                this.dataGridView5.Columns[19].Visible = false;
                tabPage4.Parent = null;
                tabPage2.Parent = null;

            }

        }

        private void toolStripButton10_Click(object sender, EventArgs e)
        {
            ExportDataGridviewTOExcellA.ExportDataGridview(this.dataGridView5, true);
        }
 

        private void toolStripTextBox3_Enter(object sender, EventArgs e)
        {
            toolStripTextBox3.Text = "";

            this.getOrderByBussCodeAllBindingSource.Filter = "";
            AddSummationRow_New(getOrderByBussCodeAllBindingSource, dataGridView5);
        }

        private void toolStripTextBox3_KeyUp(object sender, KeyEventArgs e)
        {
            string filterString;


            filterString = GetDataGridviewMultiFilterString.DGVMultiFilterString(this.dataGridView5, this.toolStripTextBox3.Text);


            this.getOrderByBussCodeAllBindingSource.Filter = "(" + filterString + ") or salesperson_name='_合计'";
            AddSummationRow_New(getOrderByBussCodeAllBindingSource, dataGridView5);
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

            if (-1 != bs.Find("salesperson_name", "_合计"))
            {
                bs.RemoveAt(bs.Find("salesperson_name", "_合计"));
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
                }

            }
 
            sumdr["salesperson_name"] = "_合计";
      
            ((DataSet)bs.DataSource).Tables[bs.DataMember].Rows.Add(sumdr);
            bs.ResetBindings(true);

        }

        private void AddSummationRow_New2(BindingSource bs, DataGridView dgv)
        {
            //InitializeApp();
            //return;

            DataRow sumdr = (((DataSet)bs.DataSource).Tables[bs.DataMember]).NewRow();

            if (-1 != bs.Find("out_number", "_合计"))
            {
                bs.RemoveAt(bs.Find("out_number", "_合计"));
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
                }

            }

            sumdr["out_number"] = "_合计";

            ((DataSet)bs.DataSource).Tables[bs.DataMember].Rows.Add(sumdr);
            bs.ResetBindings(true);

        }
        //private void AddSummationRow(BindingSource bs, DataGridView dg)
        //{
        //    if (null == dg.CurrentRow) return;

        //    decimal sum_qty = 0;
        //    decimal sum_qty_ylj = 0;
        //    decimal sum_qty_wxf = 0;


        //    foreach (DataGridViewRow dr in dg.Rows)
        //    {
        //        if (System.DBNull.Value == dr.Cells["金额1"].Value)
        //            sum_qty = sum_qty + 0;
        //        else
        //            sum_qty = sum_qty + decimal.Parse(dr.Cells["金额1"].Value.ToString());

        //        if (System.DBNull.Value == dr.Cells["金额2"].Value)
        //            sum_qty_ylj = sum_qty_ylj + 0;
        //        else
        //            sum_qty_ylj = sum_qty_ylj + decimal.Parse(dr.Cells["金额2"].Value.ToString());

        //        if (System.DBNull.Value == dr.Cells["TotalB"].Value)
        //            sum_qty_wxf = sum_qty_wxf + 0;
        //        else
        //            sum_qty_wxf = sum_qty_wxf + decimal.Parse(dr.Cells["TotalB"].Value.ToString()); 
        //    }
        //    bs.AddNew();


        //    dg.CurrentRow.Cells["金额1"].Value = sum_qty;
        //    dg.CurrentRow.Cells["金额2"].Value = sum_qty_ylj;
        //    dg.CurrentRow.Cells["TotalB"].Value = sum_qty_wxf; 
        //    bs.EndEdit();

        //    bs.Position = 0;




        //}

        private void toolStripTextBox1_Enter(object sender, EventArgs e)
        {
            toolStripTextBox1.Text = "";

            this.getOrderWzbhByBussCodeAllBindingSource.Filter = "";
        }

        private void toolStripTextBox1_KeyUp(object sender, KeyEventArgs e)
        {
            string filterString;


            filterString = GetDataGridviewMultiFilterString.DGVMultiFilterString(this.dataGridView1, this.toolStripTextBox1.Text);


            this.getOrderWzbhByBussCodeAllBindingSource.Filter = filterString;
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            ExportDataGridviewTOExcellA.ExportDataGridview(this.dataGridView1, true);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            NewFrm.Show(this);
            string salespeople = SQLDatabase.NowUserID;
            if (SQLDatabase.CheckHaveRight(salespeople, "设置客户期初应收"))
            {
                this.getOrderWzbhByBussCodeAllTableAdapter.Fill(this.lYSalseMange.GetOrderWzbhByBussCodeAll, this.dateTimePicker1.Value
                           , this.dateTimePicker2.Value.AddDays(1), "");
            }
            else if (SQLDatabase.CheckHaveRight(salespeople, "查看客户期初应收"))
            {


                this.getOrderWzbhByBussCodeAllTableAdapter.Fill(this.lYSalseMange.GetOrderWzbhByBussCodeAll, this.dateTimePicker1.Value
                            , this.dateTimePicker2.Value.AddDays(1), "");

            }
            else
            {

                this.getOrderWzbhByBussCodeAllTableAdapter.Fill(this.lYSalseMange.GetOrderWzbhByBussCodeAll, this.dateTimePicker1.Value
                            , this.dateTimePicker2.Value.AddDays(1), salespeople);
            }


           
            NewFrm.Hide(this);
        }

        private void toolStripButton7_Click(object sender, EventArgs e)
        {
            if (null == this.dataGridView5.CurrentRow) return;

            FilterForm filterForm = new FilterForm();




            List<string> ls = new List<string>();
            ls.Add("id");
        

            filterForm.SetSourceColumns(this.lYSalseMange.GetOrderByBussCodeAll.Columns, ls);

            filterForm.ShowDialog();

            this.getOrderByBussCodeAllBindingSource.Filter = filterForm.GetFilterString();
            AddSummationRow_New(getOrderByBussCodeAllBindingSource, dataGridView5);
        }

        private void toolStripButton8_Click(object sender, EventArgs e)
        {
            if (null == this.dataGridView5.CurrentRow) return;
            SortForm DataSort = new SortForm();

            List<string> ls = new List<string>();
            ls.Add("id");


            DataSort.SetSortColumns(this.lYSalseMange.GetOrderByBussCodeAll.Columns, ls);
            DataSort.ShowDialog();
            this.getOrderByBussCodeAllBindingSource.Sort = DataSort.GetSortString();
            AddSummationRow_New(getOrderByBussCodeAllBindingSource, dataGridView5);
        }

       

        private void toolStripTextBox4_KeyUp(object sender, KeyEventArgs e)
        {
            string filterString;


            filterString = GetDataGridviewMultiFilterString.DGVMultiFilterString(this.dataGridView2, this.toolStripTextBox4.Text);


            this.getOrderByBussCodeAllByOutBindingSource.Filter = "(" + filterString + ") or out_number='_合计'";

            AddSummationRow_New2(getOrderByBussCodeAllByOutBindingSource, dataGridView2);
        }

        private void toolStripTextBox4_Enter(object sender, EventArgs e)
        {
            toolStripTextBox4.Text = "";

            this.getOrderByBussCodeAllByOutBindingSource.Filter = "";
            AddSummationRow_New2(getOrderByBussCodeAllByOutBindingSource, dataGridView2);
        }

        private void toolStripButton13_Click_1(object sender, EventArgs e)
        {
            ExportDataGridviewTOExcellA.ExportDataGridview(this.dataGridView2, true);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            NewFrm.Show(this);
            this.getOrderByBussCodeAll_ByOutTableAdapter.Fill(this.lYSalseMange.GetOrderByBussCodeAll_ByOut, this.dateTimePicker3.Value
             , this.dateTimePicker4.Value.AddDays(1));
            AddSummationRow_New2(getOrderByBussCodeAllByOutBindingSource, dataGridView2);
            NewFrm.Hide(this);
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
