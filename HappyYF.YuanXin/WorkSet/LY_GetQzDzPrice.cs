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
    public partial class LY_GetQzDzPrice : Form
    {
        public string InStr = "";
        public string Code = "";
        public LY_GetQzDzPrice()
        {
            InitializeComponent();
        }




        private void LY_Quality_Control_PurchaseRep_Load(object sender, EventArgs e)
        {

            this.getQzDzFiveRePriceTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            this.productionTaskTimeTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            this.getProductPriceByBomTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            if (!string.IsNullOrEmpty(InStr) && !string.IsNullOrEmpty(Code))
            {

                this.getProductPriceByBomTableAdapter.Fill(this.lYQualityInspector.GetProductPriceByBom, InStr, Code);
                this.getQzDzFiveRePriceTableAdapter.Fill(this.lYQualityInspector.GetQzDzFiveRePrice, InStr, Code);
                this.productionTaskTimeTableAdapter.Fill(this.lYQualityInspector.ProductionTaskTime, InStr);
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
