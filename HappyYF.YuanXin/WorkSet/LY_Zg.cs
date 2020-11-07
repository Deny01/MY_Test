using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HappyYF.Infrastructure.Repositories;

namespace HappyYF.YuanXin.WorkSet
{
    public partial class LY_Zg : Form
    {
        public LY_Zg()
        {
            InitializeComponent();
            this.lY_Invoice_storeInAll_ByTimeTableAdapter.CommandTimeout = 0;
        }

       

        //private void button1_Click(object sender, EventArgs e)
        //{
        //    NewFrm.Show(this);
        //    this.lY_Invoice_storeInTableAdapter.Fill(this.lYQualityInspector.LY_Invoice_storeIn, DateTime.Parse(this.dateTimePicker1.Text).Date, DateTime.Parse(this.dateTimePicker2.Text).Date.AddDays(1),0);
        //    NewFrm.Hide(this);
        //}

        private void LY_Quality_Control_PurchaseRep_Load(object sender, EventArgs e)
        {
          
            //this.dateTimePicker1.MinDate = DateTime.Parse("2019-04-30"); 
            //this.dateTimePicker1.Text =new DateTime(DateTime.Now.Year, 1, 1).ToString("yyyy-MM-dd");
            //this.dateTimePicker2.Text = DateTime.Today.AddDays(1).Date.ToString();

            this.dateTimePicker3.MinDate = DateTime.Parse("2019-04-30");
            this.dateTimePicker3.Text = new DateTime(DateTime.Now.Year, 1, 1).ToString("yyyy-MM-dd");
            this.dateTimePicker4.Text = DateTime.Today.Date.ToString();

            this.dateTimePicker5.MinDate = DateTime.Parse("2019-04-30");
            this.dateTimePicker5.Text = new DateTime(DateTime.Now.Year, 1, 1).ToString("yyyy-MM-dd");
            this.dateTimePicker6.Text = DateTime.Today.Date.ToString();

            this.lY_Invoice_storeInTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;

            this.lY_Invoice_storeInAllTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;

            this.lY_Invoice_storeInAll_ByTimeTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
        }

        private void toolStripButton10_Click(object sender, EventArgs e)
        {
            ExportDataGridviewTOExcellA.ExportDataGridview(this.lY_Invoice_Instore_BindlistDataGridView, true);
        }

     
 
        private void button2_Click(object sender, EventArgs e)
        {
            NewFrm.Show(this);
            this.lY_Invoice_storeInAllTableAdapter.Fill(this.lYQualityInspector.LY_Invoice_storeInAll, DateTime.Parse(this.dateTimePicker3.Text).Date, DateTime.Parse(this.dateTimePicker4.Text).Date.AddDays(1));

            AddSummationRow_New(lYInvoicestoreInAllBindingSource, dataGridView1);

            NewFrm.Hide(this);
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            ExportDataGridviewTOExcellA.ExportDataGridview(this.dataGridView1, true);
        }

        private void toolStripTextBox1_KeyUp(object sender, KeyEventArgs e)
        {
            string filterString; 

            filterString = GetDataGridviewMultiFilterString.DGVMultiFilterString(this.dataGridView1, this.toolStripTextBox1.Text); 
            this.lYInvoicestoreInAllBindingSource.Filter = "(" + filterString + ") or supplier_code='_合计'";
            AddSummationRow_New(lYInvoicestoreInAllBindingSource, dataGridView1);
        }

        private void toolStripTextBox1_Enter(object sender, EventArgs e)
        {
            toolStripTextBox1.Text = ""; 
            this.lYInvoicestoreInAllBindingSource.Filter = "";
            AddSummationRow_New(lYInvoicestoreInAllBindingSource, dataGridView1);
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

            if (-1 != bs.Find("supplier_code", "_合计"))
            {
                bs.RemoveAt(bs.Find("supplier_code", "_合计"));
            }

            foreach (DataGridViewRow dgvRow in dgv.Rows)
            {
                foreach (DataGridViewCell dgvCell in dgvRow.Cells)
                { 
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

                                sumdr[dgvCell.OwningColumn.DataPropertyName] = Convert.ToDecimal(sumdr[dgvCell.OwningColumn.DataPropertyName]) + Convert.ToDecimal(dgvCell.Value);
                            }
                        }
                         

                    }
                }

            }

            sumdr["supplier_code"] = "_合计";

            ((DataSet)bs.DataSource).Tables[bs.DataMember].Rows.Add(sumdr);
            bs.ResetBindings(true);

        }
 
        private void button3_Click(object sender, EventArgs e)
        {
 
            NewFrm.Show(this);
            this.lY_Invoice_storeInAll_ByTimeTableAdapter.Fill(this.lYQualityInspector.LY_Invoice_storeInAll_ByTime, DateTime.Parse(this.dateTimePicker5.Text).Date, DateTime.Parse(this.dateTimePicker6.Text).Date.AddDays(1));
            NewFrm.Hide(this);
           
        }

        private void toolStripButton11_Click(object sender, EventArgs e)
        {
            ExportDataGridviewTOExcellA.ExportDataGridview(this.dataGridView2, true);
        }

        private void toolStripTextBox4_KeyUp(object sender, KeyEventArgs e)
        {
            string filterString;

            filterString = GetDataGridviewMultiFilterString.DGVMultiFilterString(this.dataGridView2, this.toolStripTextBox4.Text);
            this.lYInvoicestoreInAllByTimeBindingSource.Filter = "(" + filterString + ") ";
        }

        private void toolStripTextBox4_Enter(object sender, EventArgs e)
        {
            toolStripTextBox4.Text = "";
            this.lYInvoicestoreInAllByTimeBindingSource.Filter = ""; 
        }

       

        private void toolStripButton15_Click(object sender, EventArgs e)
        {
            ExportDataGridviewTOExcellA.ExportDataGridview(this.lY_Invoice_Instore_BindlistDataGridView, true);
        }

        private void toolStripButton16_Click(object sender, EventArgs e)
        {
            if (dataGridView2.CurrentRow == null)
                return;
            string sup = dataGridView2.CurrentRow.Cells["客户名称"].Value.ToString();
            NewFrm.Show(this);
            this.lY_Invoice_storeInTableAdapter.Fill(this.lYQualityInspector.LY_Invoice_storeIn,
                DateTime.Parse(this.dateTimePicker5.Text).Date, DateTime.Parse(this.dateTimePicker6.Text).Date.AddDays(1), sup);
            NewFrm.Hide(this);
        }
    }
}
