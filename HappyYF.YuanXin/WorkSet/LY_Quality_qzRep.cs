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
    public partial class LY_Quality_qzRep : Form
    {
        public LY_Quality_qzRep()
        {
            InitializeComponent();
        }

       

        private void button1_Click(object sender, EventArgs e)
        {
            this.lY_productiontask_periodRepTableAdapter.Fill(this.lYQualityInspector.LY_productiontask_periodRep, DateTime.Parse(this.dateTimePicker1.Text).Date, DateTime.Parse(this.dateTimePicker2.Text).Date,"QZ","");
        }

        private void LY_Quality_Control_PurchaseRep_Load(object sender, EventArgs e)
        {
            this.dateTimePicker1.Text = DateTime.Today.AddMonths(-3).Date.ToString();
            this.dateTimePicker2.Text = DateTime.Today.AddDays(1).Date.ToString();
            //////////////////////////////////////////////////////////////////////////////////////////

            this.lY_productiontask_periodRepTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
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
            queryForm.PrintCrystalReport = new LY_ZJ_CG_R();
            queryForm.ShowDialog();
        }
    }
}
