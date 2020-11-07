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
    public partial class LY_Quality_Control_JG_MainRep : Form
    {
        public LY_Quality_Control_JG_MainRep()
        {
            InitializeComponent();
        }

       

        private void button1_Click(object sender, EventArgs e)
        {
            this.lY_productionorder_list_RepTableAdapter.Fill(this.lYQualityInspector.LY_productionorder_list_Rep, DateTime.Parse(this.dateTimePicker1.Text).Date, DateTime.Parse(this.dateTimePicker2.Text).Date);
        }

        private void LY_Quality_Control_PurchaseRep_Load(object sender, EventArgs e)
        {
            this.dateTimePicker1.Text = DateTime.Today.AddMonths(-3).Date.ToString();
            this.dateTimePicker2.Text = DateTime.Today.AddDays(1).Date.ToString(); 

            this.lY_productionorder_list_RepTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
        }

        private void toolStripButton10_Click(object sender, EventArgs e)
        {
            ExportDataGridviewTOExcellA.ExportDataGridview(this.ly_purchase_contract_inspectionRepDataGridView, true);
        }

        private void 打印PToolStripButton_Click(object sender, EventArgs e)
        {
            if (null == this.ly_purchase_contract_inspectionRepDataGridView.CurrentRow) return; 
            BaseReportView queryForm = new BaseReportView();

            queryForm.Text = "中原精密质检报告";
            queryForm.Printdata = this.lYQualityInspector;
            queryForm.PrintCrystalReport = new LY_ZJ_XJ_MainRep();
            queryForm.ShowDialog();
        }

        private void toolStripTextBox3_Enter(object sender, EventArgs e)
        {
            toolStripTextBox3.Text = "";

            this.lYproductionorderlistRepBindingSource.Filter = "";
        }

        private void toolStripTextBox3_KeyUp(object sender, KeyEventArgs e)
        {
            string filterString;


            filterString = GetDataGridviewMultiFilterString.DGVMultiFilterString(this.ly_purchase_contract_inspectionRepDataGridView, this.toolStripTextBox3.Text);


            this.lYproductionorderlistRepBindingSource.Filter = filterString;
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if (null == this.ly_purchase_contract_inspectionRepDataGridView.CurrentRow) return;

            BaseReportView queryForm = new BaseReportView();

            string nowCheckNum = this.ly_purchase_contract_inspectionRepDataGridView.CurrentRow.Cells["总检单号"].Value.ToString();
            string code = this.ly_purchase_contract_inspectionRepDataGridView.CurrentRow.Cells["物料编码"].Value.ToString();


            queryForm.Text = "中原精密质检报告";
            queryForm.Printdata = this.lYQualityInspector;
            queryForm.PrintCrystalReport = new LY_ZJ_XJ_MainRep();
     


            string selectFormula;

            selectFormula = "{lY_productionorder_list_Rep.总检单号}  =   '" + nowCheckNum + "' and {lY_productionorder_list_Rep.物料编码}  =   '" + code + "' ";
            queryForm.PrintCrystalReport.DataDefinition.RecordSelectionFormula = selectFormula;

            queryForm.ShowDialog();
        }
    }
}
