using DataGridFilter;
using HappyYF.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Transactions;
using System.Windows.Forms;

namespace HappyYF.YuanXin.WorkSet
{
    public partial class LY_Store_InOutQuery_View : Form
    {
        public string material_code;
        public LY_Store_InOutQuery_View()
        {
            InitializeComponent();
            this.f_Item_dynamicpriceTableAdapter.CommandTimeout = 0;
            this.f_Item_dynamicpriceBTableAdapter.CommandTimeout = 0;
            this.f_Item_dynamicpriceCTableAdapter.CommandTimeout = 0;
        }



        private void LY_MaterialMange_Load(object sender, EventArgs e)
        {


            this.f_Item_dynamicpriceETableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            this.f_Item_dynamicpriceCTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            this.f_Item_dynamicpriceBTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            this.f_Item_dynamicpriceTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;

            this.average_Cost_ViewTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;



            //this.ly_plan_getmaterialTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;


            //this.ly_store_in_ylTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;

            //this.ly_store_outqueryTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;


            //this.ly_inma0010TableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;

            //this.ly_inma0010TableAdapter.Fill(this.lYStoreMange.ly_inma0010, SQLDatabase.NowUserID);


            
        






        }




        private void SetRowBackgroundInOut(DataGridView dgv)
        {
            foreach (DataGridViewRow dgr in dgv.Rows)
            {
                //if ("入库" == dgr.Cells["出入"].Value.ToString())
                if ("入库" == dgr.Cells[1].Value.ToString())
                {
                    dgr.DefaultCellStyle.BackColor = Color.Teal;
                    dgr.DefaultCellStyle.ForeColor = Color.White;
                }
                else
                {
                    dgr.DefaultCellStyle.BackColor = Color.White;
                    dgr.DefaultCellStyle.ForeColor = Color.Black;
                }




            }


        }












        private void f_Item_dynamicpriceDataGridView_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {

            DataGridView dgv = sender as DataGridView;
            SetRowBackgroundInOut(dgv);




        }

        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(material_code))
            {
                this.f_Item_dynamicpriceTableAdapter.Fill(this.lYStoreMange.f_Item_dynamicprice, material_code);
            }



        }

        private void toolStripButton12_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(material_code))
            {
                this.f_Item_dynamicpriceBTableAdapter.Fill(this.lYStoreMange.f_Item_dynamicpriceB, material_code);
            }

        }


        private void toolStripButton17_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(material_code))
            {
                this.f_Item_dynamicpriceCTableAdapter.Fill(this.lYStoreMange.f_Item_dynamicpriceC, material_code);
            }
        }

        private void toolStripButton27_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(material_code))
            {
                this.f_Item_dynamicpriceETableAdapter.Fill(this.lYStoreMange.f_Item_dynamicpriceE, material_code);
            }
        }

        private void toolStripButton28_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(material_code))
            {
                this.average_Cost_ViewTableAdapter.Fill(this.lYStoreMange.Average_Cost_View, material_code);
            }

        }

        private void 查看入库来源明细ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (average_Cost_ViewDataGridView.CurrentRow == null) return;
            string wzbh = average_Cost_ViewDataGridView.CurrentRow.Cells["物料编码"].Value.ToString();
            string djbh = average_Cost_ViewDataGridView.CurrentRow.Cells["单据编号"].Value.ToString();
            string machine_Code = average_Cost_ViewDataGridView.CurrentRow.Cells["机器码"].Value.ToString();

            if (string.IsNullOrEmpty(djbh)) return;
            if (djbh.Length < 2) return;
            string Rs = djbh.Substring(0, 2);
            switch (Rs)
            {
                case "CG":

                    LY_GetPurchasePrice queryForm = new LY_GetPurchasePrice();
                    queryForm.InStr = djbh;
                    queryForm.Code = wzbh;
                    queryForm.StartPosition = FormStartPosition.CenterParent;
                    queryForm.ShowDialog();

                    break;
                case "GD":

                    LY_GetRestructuringPrice queryFormDG = new LY_GetRestructuringPrice();
                    queryFormDG.InStr = djbh;
                    queryFormDG.Code = machine_Code;
                    queryFormDG.StartPosition = FormStartPosition.CenterParent;
                    queryFormDG.ShowDialog();

                    break;

                case "GQ":

                    LY_GetRestructuringPrice queryFormQG = new LY_GetRestructuringPrice();
                    queryFormQG.InStr = djbh;
                    queryFormQG.Code = machine_Code;
                    queryFormQG.StartPosition = FormStartPosition.CenterParent;
                    queryFormQG.ShowDialog();

                    break;
                case "DZ":

                    LY_GetQzDzPrice queryFormDZ = new LY_GetQzDzPrice();
                    queryFormDZ.InStr = djbh;
                    queryFormDZ.Code = machine_Code;
                    queryFormDZ.StartPosition = FormStartPosition.CenterParent;
                    queryFormDZ.ShowDialog();

                    break;

                case "QZ":

                    LY_GetQzDzPrice queryFormQZ = new LY_GetQzDzPrice();
                    queryFormQZ.InStr = djbh;
                    queryFormQZ.Code = machine_Code;
                    queryFormQZ.StartPosition = FormStartPosition.CenterParent;
                    queryFormQZ.ShowDialog();

                    break;
                default:
                    //Console.WriteLine("Default case");
                    break;
            }



        }








    }
}