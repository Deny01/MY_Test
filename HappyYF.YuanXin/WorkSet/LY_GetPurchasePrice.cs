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
    public partial class LY_GetPurchasePrice : Form
    {
        public string InStr = "";
        public string Code = "";
        public LY_GetPurchasePrice()
        {
            InitializeComponent();
        }




        private void LY_Quality_Control_PurchaseRep_Load(object sender, EventArgs e)
        {

            this.get_purchasePriceTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            if (!string.IsNullOrEmpty(InStr) && !string.IsNullOrEmpty(Code))
            {
                this.get_purchasePriceTableAdapter.Fill(this.lYQualityInspector.Get_purchasePrice, InStr, Code);
            }
        }

        private void toolStripButton10_Click(object sender, EventArgs e)
        {
            ExportDataGridviewTOExcellA.ExportDataGridview(this.ly_purchase_contract_inspectionRepDataGridView, true);
        }

        
    }
}
