using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HappyYF.Infrastructure.Repositories;
using DataGridFilter;
using System.Threading;

namespace HappyYF.YuanXin.WorkSet
{
    public partial class LY_Product_MonthReport : Form
    {
        public LY_Product_MonthReport()
        {
            InitializeComponent();
            this.ly_production_detail_viewTableAdapter.CommandTimeout = 0;
            this.ly_production_submit_periodTableAdapter.CommandTimeout = 0;
        }

        //private void lY_productiontask_periodBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        //{
        //    this.Validate();
        //    this.lY_productiontask_periodBindingSource.EndEdit();
        //    this.tableAdapterManager.UpdateAll(this.lYProductMange);

        //}

        //private void fillToolStripButton_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        this.lY_productiontask_periodTableAdapter.Fill(this.lYProductMange.LY_productiontask_period, new System.Nullable<System.DateTime>(((System.DateTime)(System.Convert.ChangeType(begindateToolStripTextBox.Text, typeof(System.DateTime))))), new System.Nullable<System.DateTime>(((System.DateTime)(System.Convert.ChangeType(enddateToolStripTextBox.Text, typeof(System.DateTime))))), selcodeToolStripTextBox.Text);
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
        //        this.ly_production_submit_periodTableAdapter.Fill(this.lYProductMange.ly_production_submit_period, new System.Nullable<System.DateTime>(((System.DateTime)(System.Convert.ChangeType(begindateToolStripTextBox.Text, typeof(System.DateTime))))), new System.Nullable<System.DateTime>(((System.DateTime)(System.Convert.ChangeType(enddateToolStripTextBox.Text, typeof(System.DateTime))))));
        //    }
        //    catch (System.Exception ex)
        //    {
        //        System.Windows.Forms.MessageBox.Show(ex.Message);
        //    }

        //}

        private void LY_Product_MonthReport_Load(object sender, EventArgs e)
        {
            DateTime nowdate = SQLDatabase.GetNowdate();
            int nowday = 0 - nowdate.Day + 1;

            this.dateTimePicker5.Text = nowdate.AddDays(nowday).Date.ToString();
            this.dateTimePicker6.Text = nowdate.AddDays(nowday).AddMonths(1).AddDays(-1).Date.ToString();


            ///////////////////////
            DataGridViewColumnSelector cs = new DataGridViewColumnSelector(ly_production_detail_viewSingleDataGridView, this.Text);
            cs.MaxHeight = 180;
            cs.Width = 800;

            cs.Set_dgvColumns();
            ////////////////////////////////

           
           this.ly_production_detail_viewSingleTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;

            this.ly_production_submit_periodTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            this.ly_production_detail_viewTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;

            this.ly_production_detail_viewTableAdapter.Fill(this.lYProductMange.ly_production_detail_view, this.dateTimePicker5.Value, this.dateTimePicker6.Value.AddDays(1));

            this.ly_production_submit_periodTableAdapter.Fill(this.lYProductMange.ly_production_submit_period, this.dateTimePicker5.Value, this.dateTimePicker6.Value.AddDays(1));
        }

        private void toolStripButton22_Click(object sender, EventArgs e)
        {
            NewFrm.Show(this);
            Thread.Sleep(100);
            this.ly_production_submit_periodTableAdapter.Fill(this.lYProductMange.ly_production_submit_period, this.dateTimePicker5.Value, this.dateTimePicker6.Value.AddDays(1));
            this.ly_production_detail_viewTableAdapter.Fill(this.lYProductMange.ly_production_detail_view, this.dateTimePicker5.Value, this.dateTimePicker6.Value.AddDays(1));
            NewFrm.Hide(this);
        }

        private void toolStripButton19_Click(object sender, EventArgs e)
        {
            if (null == this.ly_production_submit_periodDataGridView.CurrentRow) return;

            FilterForm filterForm = new FilterForm();




            //SumQueryDataSet qds;
            //qds = new SumQueryDataSet();

            List<string> ls = new List<string>();
            ls.Add("id");


            filterForm.SetSourceColumns(this.lYProductMange.ly_production_submit_period.Columns, ls);

            filterForm.ShowDialog();

            this.ly_production_submit_periodBindingSource.Filter = filterForm.GetFilterString();
        }

        private void toolStripButton20_Click(object sender, EventArgs e)
        {
            if (null == this.ly_production_submit_periodDataGridView.CurrentRow) return;
            SortForm DataSort = new SortForm();

            List<string> ls = new List<string>();
            ls.Add("id");


            DataSort.SetSortColumns(this.lYProductMange.ly_production_submit_period.Columns, ls);
            DataSort.ShowDialog();
            this.ly_production_submit_periodBindingSource.Sort = DataSort.GetSortString();
        }

        private void toolStripTextBox1_KeyUp(object sender, KeyEventArgs e)
        {
            string filterString;


            filterString = GetDataGridviewMultiFilterString.DGVMultiFilterString(this.ly_production_detail_viewDataGridView, this.toolStripTextBox1.Text);


            this.ly_production_detail_viewBindingSource.Filter = filterString;
           // AddSummationRow_New(ly_sales_contract_standard_ReportBindingSource, ly_sales_contract_standard_ReportDataGridView);
        }

        private void toolStripTextBox1_Enter(object sender, EventArgs e)
        {
            toolStripTextBox1.Text = "";

            this.ly_production_detail_viewBindingSource.Filter = "";
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if (null == this.ly_production_detail_viewDataGridView.CurrentRow) return;

            FilterForm filterForm = new FilterForm();




            //SumQueryDataSet qds;
            //qds = new SumQueryDataSet();

            List<string> ls = new List<string>();
            ls.Add("id");


            filterForm.SetSourceColumns(this.lYProductMange.ly_production_detail_view.Columns, ls);

            filterForm.ShowDialog();

            this.ly_production_detail_viewBindingSource.Filter = filterForm.GetFilterString();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            if (null == this.ly_production_detail_viewDataGridView.CurrentRow) return;
            SortForm DataSort = new SortForm();

            List<string> ls = new List<string>();
            ls.Add("id");


            DataSort.SetSortColumns(this.lYProductMange.ly_production_detail_view.Columns, ls);
            DataSort.ShowDialog();
            this.ly_production_detail_viewBindingSource.Sort = DataSort.GetSortString();
        }

        private void toolStripButton17_Click(object sender, EventArgs e)
        {
            ExportDataGridviewTOExcell.ExportDataGridview(this.ly_production_submit_periodDataGridView, true);
        }

        private void ly_production_submit_periodDataGridView_SelectionChanged(object sender, EventArgs e)
        {
            if (null == this.ly_production_submit_periodDataGridView.CurrentRow) return;

            string nowdept = this.ly_production_submit_periodDataGridView.CurrentRow.Cells["部门0"].Value.ToString();
            string nowworker = this.ly_production_submit_periodDataGridView.CurrentRow.Cells["工号0"].Value.ToString();
            string nowitemno = this.ly_production_submit_periodDataGridView.CurrentRow.Cells["成品编码0"].Value.ToString();

            this.ly_production_detail_viewSingleTableAdapter.Fill(this.lYProductMange.ly_production_detail_viewSingle, this.dateTimePicker5.Value, this.dateTimePicker6.Value.AddDays(1), nowitemno, nowworker, nowdept);


        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            if (null == this.ly_production_submit_periodDataGridView.CurrentRow) return;

          
            NewFrm.Show(this); ;

          
           
            BaseReportView queryForm = new BaseReportView();

           

            queryForm.setchackBoxCansee(false);

            queryForm.Printdata = this.lYProductMange;
          
           
                queryForm.Text = "中原精密生产报表";
                queryForm.PrintCrystalReport = new LY_ProductReport_period();
           


            NewFrm.Hide(this);

            queryForm.ShowDialog();
        }

        //private void fillToolStripButton_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        this.ly_production_detail_viewSingleTableAdapter.Fill(this.lYProductMange.ly_production_detail_viewSingle, new System.Nullable<System.DateTime>(((System.DateTime)(System.Convert.ChangeType(begindateToolStripTextBox.Text, typeof(System.DateTime))))), new System.Nullable<System.DateTime>(((System.DateTime)(System.Convert.ChangeType(enddateToolStripTextBox.Text, typeof(System.DateTime))))), wzbhToolStripTextBox.Text, oper_deptToolStripTextBox.Text);
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
        //        this.ly_production_detail_viewTableAdapter.Fill(this.lYProductMange.ly_production_detail_view, new System.Nullable<System.DateTime>(((System.DateTime)(System.Convert.ChangeType(begindateToolStripTextBox.Text, typeof(System.DateTime))))), new System.Nullable<System.DateTime>(((System.DateTime)(System.Convert.ChangeType(enddateToolStripTextBox.Text, typeof(System.DateTime))))));
        //    }
        //    catch (System.Exception ex)
        //    {
        //        System.Windows.Forms.MessageBox.Show(ex.Message);
        //    }

        //}
    }
}
