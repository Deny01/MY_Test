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
    public partial class LY_Picking_Statistical : Form
    {
        public LY_Picking_Statistical()
        {
            InitializeComponent();
        }

       

        private void button1_Click(object sender, EventArgs e)
        {
            this.ly_production_task_requestSingleTableAdapter.Fill(this.lYQualityInspector.ly_production_task_requestSingle, DateTime.Parse(this.dateTimePicker1.Text).Date, DateTime.Parse(this.dateTimePicker2.Text).Date.AddDays(1));
            this.add_wzbh_RepTableAdapter.Fill(this.lYQualityInspector.add_wzbh_Rep, DateTime.Parse(this.dateTimePicker1.Text).Date, DateTime.Parse(this.dateTimePicker2.Text).Date.AddDays(1));
        }

        private void LY_Quality_Control_PurchaseRep_Load(object sender, EventArgs e)
        {
            this.dateTimePicker1.Text = DateTime.Today.AddMonths(-3).Date.ToString();
            this.dateTimePicker2.Text = DateTime.Today.Date.ToString();
            //////////////////////////////////////////////////////////////////////////////////////////

            this.ly_production_task_requestSingleTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            this.add_wzbh_RepTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
        }

        private void toolStripButton10_Click(object sender, EventArgs e)
        {
            ExportDataGridviewTOExcellA.ExportDataGridview(this.ly_purchase_contract_inspectionRepDataGridView, true);
        }

   
        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            ExportDataGridviewTOExcellA.ExportDataGridview(this.dataGridView1, true);
        }

        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            if (null == this.ly_purchase_contract_inspectionRepDataGridView.CurrentRow) return;

            BaseReportView queryForm = new BaseReportView();

            string task = this.ly_purchase_contract_inspectionRepDataGridView.CurrentRow.Cells["任务单"].Value.ToString();
            string item = this.ly_purchase_contract_inspectionRepDataGridView.CurrentRow.Cells["物料号"].Value.ToString();
            queryForm.Text = "中原精密追加领料说明报告";

            queryForm.Printdata = this.lYQualityInspector;

            queryForm.PrintCrystalReport = new LY_Add_Rep();


            string selectFormula;

            selectFormula = "{ly_production_task_requestSingle.pruductionOrder_num}  =   '" + task + "' and {ly_production_task_requestSingle.itemno} =   '" + item + "'";
            queryForm.PrintCrystalReport.DataDefinition.RecordSelectionFormula = selectFormula;

            queryForm.ShowDialog();
        }
    }
}
