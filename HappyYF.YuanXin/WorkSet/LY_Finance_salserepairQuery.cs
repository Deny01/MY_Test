using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using HappyYF.Infrastructure.Repositories;
using DataGridFilter;
using System.Transactions;
using System.Data.SqlClient;
using DataGridFilter;


namespace HappyYF.YuanXin.WorkSet
{
    public partial class LY_Finance_salserepairQuery : Form
    {
        public LY_Finance_salserepairQuery()
        {
            InitializeComponent();
            this.ly_sales_repair_detail_View_NewTableAdapter.CommandTimeout = 0;
        }

        //private void fillToolStripButton_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        this.ly_sales_repair_detail_View_NewTableAdapter.Fill(this.lYSalseRepair.ly_sales_repair_detail_View_New, new System.Nullable<System.DateTime>(((System.DateTime)(System.Convert.ChangeType(begindateToolStripTextBox.Text, typeof(System.DateTime))))), new System.Nullable<System.DateTime>(((System.DateTime)(System.Convert.ChangeType(enddateToolStripTextBox.Text, typeof(System.DateTime))))));
        //    }
        //    catch (System.Exception ex)
        //    {
        //        System.Windows.Forms.MessageBox.Show(ex.Message);
        //    }

        //}

        private void ly_sales_repair_detail_View_NewBindingNavigator_RefreshItems(object sender, EventArgs e)
        {

        }

        private void LY_Finance_salserepairQuery_Load(object sender, EventArgs e)
        {
            this.dateTimePicker5.Text = "1900" + "-01" + "-01";

            this.dateTimePicker6.Text = SQLDatabase.GetNowdate().AddDays(0).Date.ToString();

            this.ly_sales_repair_detail_View_NewTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;


        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            NewFrm.Show(this);

            this.ly_sales_repair_detail_View_NewTableAdapter.Fill(this.lYSalseRepair.ly_sales_repair_detail_View_New,"aaa", this.dateTimePicker5.Value, this.dateTimePicker6.Value.AddDays(1));

            //CountMoney(lY_Inventory_query_financialBindingSource, ly_inma0010DataGridView);


            //this.ly_sales_standard_SumTableAdapter.Fill(this.lYSalseRepair.ly_sales_standard_Sum, this.nowusercode, this.nowfillstragecode, this.dateTimePicker5.Value, this.dateTimePicker6.Value.AddDays(1));


            NewFrm.Hide(this);
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            ExportDataGridviewTOExcell.ExportDataGridview(this.ly_sales_repair_detail_View_NewDataGridView, true);
        }

        private void toolStripButton19_Click(object sender, EventArgs e)
        {
            if (null == this.ly_sales_repair_detail_View_NewDataGridView.CurrentRow) return;

            FilterForm filterForm = new FilterForm();

            //SumQueryDataSet qds;
            //qds = new SumQueryDataSet();

            List<string> ls = new List<string>();
            ls.Add("id");


            filterForm.SetSourceColumns(this.lYSalseRepair.ly_sales_repair_detail_View_New.Columns, ls);

            filterForm.ShowDialog();

            string filterstr = filterForm.GetFilterString();
            if (!string.IsNullOrEmpty(filterstr))
            {

                this.ly_sales_repair_detail_View_NewBindingSource.Filter = filterstr;
            }
            //AddSummationRow_New(ly_sales_repair_detail_View_NewBindingSource, ly_sales_repair_detail_View_NewDataGridView);
        }
    }
}
