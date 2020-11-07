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
 

namespace HappyYF.YuanXin.WorkSet
{
    public partial class LY_Waste_InOutQuery : Form
    {
        public LY_Waste_InOutQuery()
        {
            InitializeComponent();
           this.f_Item_dynamicpriceTableAdapter.CommandTimeout = 0;
        }

        private void  CountMoney(BindingSource bs, DataGridView dg)
        {
            int haveHere = bs.Find("仓库", "总计");

            if (haveHere > -1)
            {
                bs .RemoveAt( haveHere);
            }
            
            if (null == dg.CurrentRow) return;

           
            decimal sum_storemoney = 0;
            decimal in_storemoney = 0;
            decimal out_storemoney = 0;
            decimal planout_money = 0;
            decimal plan_storemoney = 0;
 


            foreach (DataGridViewRow dr in dg.Rows)
            {
   

                if ("总计" == dr.Cells["仓库"].Value.ToString())
                {
                    dg.Rows.Remove(dr);

                }
                else
                {

                    if (System.DBNull.Value == dr.Cells["库存金额"].Value)
                        sum_storemoney = sum_storemoney + 0;
                    else
                        sum_storemoney = sum_storemoney + decimal.Parse(dr.Cells["库存金额"].Value.ToString());

                    if (System.DBNull.Value == dr.Cells["入库金额"].Value)
                        in_storemoney = in_storemoney + 0;
                    else
                        in_storemoney = in_storemoney + decimal.Parse(dr.Cells["入库金额"].Value.ToString());

                    if (System.DBNull.Value == dr.Cells["出库金额"].Value)
                        out_storemoney = out_storemoney + 0;
                    else
                        out_storemoney = out_storemoney + decimal.Parse(dr.Cells["出库金额"].Value.ToString());
              
                    if (System.DBNull.Value == dr.Cells["计划出库金额"].Value)
                        planout_money = planout_money + 0;
                    else
                        planout_money = planout_money + decimal.Parse(dr.Cells["计划出库金额"].Value.ToString());

                    if (System.DBNull.Value == dr.Cells["计划库存金额"].Value)
                        plan_storemoney = plan_storemoney + 0;
                    else
                        plan_storemoney = plan_storemoney + decimal.Parse(dr.Cells["计划库存金额"].Value.ToString());


                   
                }

                


            }
            bs.AddNew(); 

            dg.CurrentRow.Cells["仓库"].Value = "总计";
            dg.CurrentRow.Cells["库存金额"].Value = sum_storemoney;
            dg.CurrentRow.Cells["入库金额"].Value = in_storemoney;
            dg.CurrentRow.Cells["出库金额"].Value = out_storemoney;
            dg.CurrentRow.Cells["计划出库金额"].Value = planout_money;
            dg.CurrentRow.Cells["计划库存金额"].Value = plan_storemoney;
            dg.CurrentRow.Cells["物资编号"].Value = "---";
            dg.CurrentRow.Cells["库存底线"].Value = -1;
            dg.CurrentRow.Cells["库存警戒"].Value = 0;  
            bs.EndEdit();

            bs.Position = 0;
        }

        private void LY_MaterialMange_Load(object sender, EventArgs e)
        {
            this.tabPage2.Parent = null;
            this.tabPage6.Parent = null;

            SetHavePricerIGHT();

            NewFrm.Show(this);


            this.f_Item_dynamicpriceTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;

            this.ly_plan_getmaterialTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;


            this.ly_waste_in_ylTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;

            this.ly_waste_outqueryTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;


            this.lY_InventoryWaste_queryTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;

            this.lY_InventoryWaste_queryTableAdapter.Fill(this.lYStoreMange.LY_InventoryWaste_query, SQLDatabase.NowUserID);

            SetRowBackground(); 

            SetRowBackground();
            NewFrm.Hide(this);

        }

        private void SetHavePricerIGHT()
        {
            if (SQLDatabase.CheckHaveRight(SQLDatabase.NowUserID, "单价金额查看"))
            {
                DataGridViewColumnSelector cs = new DataGridViewColumnSelector(ly_inma0010DataGridView, this.Text);
                cs.MaxHeight = 180;
                cs.Width = 800;

                cs.Set_dgvColumns(); 

                CountMoney(lY_InventoryWaste_queryBindingSource, ly_inma0010DataGridView);

                ly_inma0010DataGridView.Columns["入库金额"].Visible = true;
                ly_inma0010DataGridView.Columns["出库金额"].Visible = true;
                ly_inma0010DataGridView.Columns["库存单价"].Visible = true;
                ly_inma0010DataGridView.Columns["库存金额"].Visible = true;
                ly_inma0010DataGridView.Columns["计划出库金额"].Visible = true;
                ly_inma0010DataGridView.Columns["计划库存金额"].Visible = true;
                //ly_inma0010DataGridView.Columns["dynamic_price"].Visible = true;
                //ly_inma0010DataGridView.Columns["machine_price"].Visible = true;
                //ly_inma0010DataGridView.Columns["assembly_price"].Visible = true;
                //ly_inma0010DataGridView.Columns["outsource_price"].Visible = true;
                //ly_inma0010DataGridView.Columns["machine_outsource_price"].Visible = true;




            }
            else
            {
                ly_inma0010DataGridView.Columns["入库金额"].Visible = false;
                ly_inma0010DataGridView.Columns["出库金额"].Visible = false;
                ly_inma0010DataGridView.Columns["库存单价"].Visible = false;
                ly_inma0010DataGridView.Columns["库存金额"].Visible = false;
                ly_inma0010DataGridView.Columns["计划出库金额"].Visible = false;
                ly_inma0010DataGridView.Columns["计划库存金额"].Visible = false;
                //ly_inma0010DataGridView.Columns["dynamic_price"].Visible = false;
                //ly_inma0010DataGridView.Columns["machine_price"].Visible = false;
                //ly_inma0010DataGridView.Columns["assembly_price"].Visible = false;
                //ly_inma0010DataGridView.Columns["outsource_price"].Visible = false;
                //ly_inma0010DataGridView.Columns["machine_outsource_price"].Visible = false;

                this.tabPage6.Parent = null;


            }
        }

        private void toolStripTextBox1_Enter(object sender, EventArgs e)
        {
            toolStripTextBox1.Text = "";
           
            this.lY_InventoryWaste_queryBindingSource.Filter = " ";

            CountMoney(lY_InventoryWaste_queryBindingSource, ly_inma0010DataGridView);
        }

        private void toolStripTextBox1_KeyUp(object sender, KeyEventArgs e)
        {
            

            string filterString; 
            filterString = GetDataGridviewMultiFilterString.DGVMultiFilterString(this.ly_inma0010DataGridView, this.toolStripTextBox1.Text);
             

            this.lY_InventoryWaste_queryBindingSource.Filter = "(" + filterString + ") or 仓库='总计'";

            CountMoney(lY_InventoryWaste_queryBindingSource, ly_inma0010DataGridView);
 
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            ExportDataGridviewTOExcell.ExportDataGridview(this.ly_inma0010DataGridView, true);
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            FilterForm filterForm = new FilterForm();

         

            List<string> ls = new List<string>();
            ls.Add("id");


            filterForm.SetSourceColumns(ly_inma0010DataGridView.Columns , ls);

            filterForm.ShowDialog();

            string nowfilter = filterForm.GetFilterString();
            if (string.IsNullOrEmpty(nowfilter))
            {
                this.lY_InventoryWaste_queryBindingSource.Filter = nowfilter;
            }
            else
            {
                this.lY_InventoryWaste_queryBindingSource.Filter = "(" + nowfilter + ")" + " or 仓库='总计'";
            } 

            CountMoney(lY_InventoryWaste_queryBindingSource, ly_inma0010DataGridView);
        }



        private void SetRowBackground()
        {
            foreach (DataGridViewRow dgr in ly_inma0010DataGridView.Rows)
            {
                if ("总计" == dgr.Cells["仓库"].Value.ToString())
                {
                }
                else
                {
                    if (0 >= decimal.Parse(dgr.Cells["库存警戒"].Value.ToString())
                        && 0 < decimal.Parse(dgr.Cells["库存底线"].Value.ToString()))
                        dgr.DefaultCellStyle.BackColor = Color.Cyan;
                }
            }
        }

        private void SetRowBackgroundInOut()
        {
            foreach (DataGridViewRow dgr in f_Item_dynamicpriceDataGridView.Rows)
            {
                if ("入库" == dgr.Cells["出入"].Value.ToString())
                {
                    dgr.DefaultCellStyle.BackColor = Color.Teal ;
                    dgr.DefaultCellStyle.ForeColor = Color.White;
                }
                else
                {
                    dgr.DefaultCellStyle.BackColor = Color.White ;
                    dgr.DefaultCellStyle.ForeColor = Color.Black ;
                }




            }


        }

        private void ly_inma0010DataGridView_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            SetRowBackground();
        }

        private void ly_inma0010DataGridView_SelectionChanged(object sender, EventArgs e)
        {
            if (null == ly_inma0010DataGridView.CurrentRow) return;

            string s = this.ly_inma0010DataGridView.CurrentRow.Cells["物资编号"].Value.ToString();

            this .ly_waste_in_ylTableAdapter.Fill ( this .lYStoreMange.ly_waste_in_yl ,s);
            this.ly_waste_outqueryTableAdapter.Fill(this.lYStoreMange.ly_waste_outquery, s);
            this.ly_plan_getmaterialTableAdapter.Fill(this.lYStoreMange.ly_plan_getmaterial,s);
            this.f_Item_dynamicpriceTableAdapter.Fill(this.lYStoreMange.f_Item_dynamicprice, "");

            SetRowBackgroundInOut();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            //NewFrm.Show(this);

            this.lY_InventoryWaste_queryTableAdapter.Fill(this.lYStoreMange.LY_InventoryWaste_query, SQLDatabase.NowUserID);

            CountMoney(lY_InventoryWaste_queryBindingSource, ly_inma0010DataGridView);

            //NewFrm.Hide(this);
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            if (null == this.ly_inma0010DataGridView.CurrentRow) return;
            SortForm DataSort = new SortForm();

            List<string> ls = new List<string>();
            ls.Add("id");


            DataSort.SetSortColumns(ly_inma0010DataGridView.Columns, ls);
            DataSort.ShowDialog();
            this.lY_InventoryWaste_queryBindingSource.Sort = DataSort.GetSortString();
        }

       
  
 
        private void f_Item_dynamicpriceDataGridView_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            SetRowBackgroundInOut(); 

        }

        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            if (null == ly_inma0010DataGridView.CurrentRow) return;

            string s = this.ly_inma0010DataGridView.CurrentRow.Cells["物资编号"].Value.ToString();

           
            this.f_Item_dynamicpriceTableAdapter.Fill(this.lYStoreMange.f_Item_dynamicprice, s);
        }
 
    }
}
