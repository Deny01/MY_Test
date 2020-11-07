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
    public partial class LY_GetRestructuringPrice : Form
    {
        public string InStr = "";
        public string Code = "";
        public LY_GetRestructuringPrice()
        {
            InitializeComponent();
        }




        private void LY_Quality_Control_PurchaseRep_Load(object sender, EventArgs e)
        {

            this.getRestructuringFiveRePriceTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            this.restructuringTaskTimeTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            if (!string.IsNullOrEmpty(InStr) && !string.IsNullOrEmpty(Code))
            {


                this.getRestructuringFiveRePriceTableAdapter.Fill(this.lYQualityInspector.GetRestructuringFiveRePrice, InStr, Code);
                this.restructuringTaskTimeTableAdapter.Fill(this.lYQualityInspector.RestructuringTaskTime, InStr);
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
    }
}
