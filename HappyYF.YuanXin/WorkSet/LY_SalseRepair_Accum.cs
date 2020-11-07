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
    public partial class LY_SalseRepair_Accum : Form
    {
        public LY_SalseRepair_Accum()
        {
            InitializeComponent();
        }

        //private void fillToolStripButton_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        this.ly_sales_repairAccum_ReportTableAdapter.Fill(this.lYSalseRepair.ly_sales_repairAccum_Report, new System.Nullable<System.DateTime>(((System.DateTime)(System.Convert.ChangeType(begindateToolStripTextBox.Text, typeof(System.DateTime))))), new System.Nullable<System.DateTime>(((System.DateTime)(System.Convert.ChangeType(enddateToolStripTextBox.Text, typeof(System.DateTime))))));
        //    }
        //    catch (System.Exception ex)
        //    {
        //        System.Windows.Forms.MessageBox.Show(ex.Message);
        //    }

        //}

        private void LY_SalseRepair_Accum_Load(object sender, EventArgs e)
        {
            
            
            this.dateTimePicker3.Text = SQLDatabase.GetNowdate().AddYears (-1).Year + "-01" + "-01";

            this.dateTimePicker4.Text = SQLDatabase.GetNowdate().AddDays(0).Date.ToString();

            this.ly_sales_repairAccum_ReportTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
        }

        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            NewFrm.Show(this);
            this.ly_sales_repairAccum_ReportTableAdapter.Fill(this.lYSalseRepair.ly_sales_repairAccum_Report, this.dateTimePicker3.Value, this.dateTimePicker4.Value.AddDays(1).Date);
            NewFrm.Hide(this);
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            ExportDataGridviewTOExcell.ExportDataGridview(this.ly_sales_repairAccum_ReportDataGridView, true);
        }
    }
}
