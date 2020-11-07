using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HappyYF.Infrastructure.Repositories;

namespace HappyYF.YuanXin.WorkSet
{
    public partial class LY_StockTake_AddItem : Form
    {

        public string check_num = "";
        public string itemnumber = "";

        public decimal  unit_price=0;
        public decimal store_number=0;
        public decimal borrow_number = 0;

        public string latest_intime = "2000-01-01";
        public string latest_outtime = "2000-01-01";
        public string latest_activitytime = "2000-01-01";


        public string nowremark = "";


       

        public LY_StockTake_AddItem()
        {
            InitializeComponent();
        }

       

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ConveyanceVerify_AddVehicle_Load(object sender, EventArgs e)
        {
            this.ly_stocktake_noselectItemTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            this.ly_stocktake_noselectItemTableAdapter.Fill(this.lYStoreMange .ly_stocktake_noselectItem,this .check_num ,SQLDatabase .NowUserID );
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (null != this.ly_stocktake_noselectItemDataGridView.CurrentRow)
            {
                this.itemnumber = this.ly_stocktake_noselectItemDataGridView.CurrentRow.Cells["物资编号"].Value.ToString();
                this.nowremark = this.ly_stocktake_noselectItemDataGridView.CurrentRow.Cells["dataGridViewTextBoxColumn15"].Value.ToString();
                if (!string.IsNullOrEmpty(this.ly_stocktake_noselectItemDataGridView.CurrentRow.Cells["库存单价"].Value.ToString()))
                {
                    this.unit_price = decimal.Parse(this.ly_stocktake_noselectItemDataGridView.CurrentRow.Cells["库存单价"].Value.ToString());
                }
                if (!string.IsNullOrEmpty(this.ly_stocktake_noselectItemDataGridView.CurrentRow.Cells["库存数量"].Value.ToString()))
                {
                    this.store_number = decimal.Parse(this.ly_stocktake_noselectItemDataGridView.CurrentRow.Cells["库存数量"].Value.ToString());
                }

                if (!string.IsNullOrEmpty(this.ly_stocktake_noselectItemDataGridView.CurrentRow.Cells["借用数量"].Value.ToString()))
                {
                    this.borrow_number  = decimal.Parse(this.ly_stocktake_noselectItemDataGridView.CurrentRow.Cells["借用数量"].Value.ToString());
                }

                if (!string.IsNullOrEmpty(this.ly_stocktake_noselectItemDataGridView.CurrentRow.Cells["最近入库"].Value.ToString()))
                {
                    this.latest_intime = this.ly_stocktake_noselectItemDataGridView.CurrentRow.Cells["最近入库"].Value.ToString();
                }
                if (!string.IsNullOrEmpty(this.ly_stocktake_noselectItemDataGridView.CurrentRow.Cells["最近出库"].Value.ToString()))
                {
                    this.latest_outtime = this.ly_stocktake_noselectItemDataGridView.CurrentRow.Cells["最近出库"].Value.ToString();
                }
                if (!string.IsNullOrEmpty(this.ly_stocktake_noselectItemDataGridView.CurrentRow.Cells["最近活动"].Value.ToString()))
                {
                    this.latest_activitytime = this.ly_stocktake_noselectItemDataGridView.CurrentRow.Cells["最近活动"].Value.ToString();
                }
            }

            this.Close();
        }

        private void hT_ConveyanceVerify_AddVehicleDataGridView_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (null != this.ly_stocktake_noselectItemDataGridView.CurrentRow)
            {
                this.itemnumber = this.ly_stocktake_noselectItemDataGridView.CurrentRow.Cells["物资编号"].Value.ToString();
                this.nowremark = this.ly_stocktake_noselectItemDataGridView.CurrentRow.Cells["dataGridViewTextBoxColumn15"].Value.ToString();
                if (!string.IsNullOrEmpty(this.ly_stocktake_noselectItemDataGridView.CurrentRow.Cells["库存单价"].Value.ToString()))
                {
                    this.unit_price = decimal.Parse(this.ly_stocktake_noselectItemDataGridView.CurrentRow.Cells["库存单价"].Value.ToString());
                }
                if (!string.IsNullOrEmpty(this.ly_stocktake_noselectItemDataGridView.CurrentRow.Cells["库存数量"].Value.ToString()))
                {
                    this.store_number = decimal.Parse(this.ly_stocktake_noselectItemDataGridView.CurrentRow.Cells["库存数量"].Value.ToString());
                }

                if (!string.IsNullOrEmpty(this.ly_stocktake_noselectItemDataGridView.CurrentRow.Cells["借用数量"].Value.ToString()))
                {
                    this.borrow_number = decimal.Parse(this.ly_stocktake_noselectItemDataGridView.CurrentRow.Cells["借用数量"].Value.ToString());
                }

                if (!string.IsNullOrEmpty(this.ly_stocktake_noselectItemDataGridView.CurrentRow.Cells["最近入库"].Value.ToString()))
                {
                    this.latest_intime = this.ly_stocktake_noselectItemDataGridView.CurrentRow.Cells["最近入库"].Value.ToString();
                }
                if (!string.IsNullOrEmpty(this.ly_stocktake_noselectItemDataGridView.CurrentRow.Cells["最近出库"].Value.ToString()))
                {
                    this.latest_outtime = this.ly_stocktake_noselectItemDataGridView.CurrentRow.Cells["最近出库"].Value.ToString();
                }
                if (!string.IsNullOrEmpty(this.ly_stocktake_noselectItemDataGridView.CurrentRow.Cells["最近活动"].Value.ToString()))
                {
                    this.latest_activitytime = this.ly_stocktake_noselectItemDataGridView.CurrentRow.Cells["最近活动"].Value.ToString();
                }
            }

            this.Close();
        }

       
       
    }
}
