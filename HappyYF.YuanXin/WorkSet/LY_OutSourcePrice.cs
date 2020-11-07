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
    public partial class LY_OutSourcePrice : Form
    {
        public string InStrId = "";
 
        public LY_OutSourcePrice()
        {
            InitializeComponent();
        }




        private void LY_Quality_Control_PurchaseRep_Load(object sender, EventArgs e)
        {

            this.getOutSourceFiveInPriceTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            this.getOutSourceFiveOutPriceTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            this.getOutSourceFivePrice_OutSourceTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            if (!string.IsNullOrEmpty(InStrId)  )
            {


                this.getOutSourceFiveInPriceTableAdapter.Fill(this.lYQualityInspector.GetOutSourceFiveInPrice, int.Parse(InStrId));
                this.getOutSourceFiveOutPriceTableAdapter.Fill(this.lYQualityInspector.GetOutSourceFiveOutPrice, int.Parse(InStrId));
                this.getOutSourceFivePrice_OutSourceTableAdapter.Fill(this.lYQualityInspector.GetOutSourceFivePrice_OutSource, int.Parse(InStrId));
            }
        }

        private void toolStripButton10_Click(object sender, EventArgs e)
        {
             ExportDataGridviewTOExcellA.ExportDataGridview(this.ly_purchase_contract_inspectionRepDataGridView, true);
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            ExportDataGridviewTOExcellA.ExportDataGridview(this.dataGridView1, true);
        }

        private void toolStripButton11_Click(object sender, EventArgs e)
        {
            ExportDataGridviewTOExcellA.ExportDataGridview(this.dataGridView2, true);
        }
    }
}
