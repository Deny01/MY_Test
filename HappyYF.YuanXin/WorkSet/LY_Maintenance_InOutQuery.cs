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
using System.Threading;


namespace HappyYF.YuanXin.WorkSet
{
    public partial class LY_Maintenance_InOutQuery : Form
    {
        public LY_Maintenance_InOutQuery()
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

            this.dateTimePicker1.Text = SQLDatabase.GetNowdate().AddDays(0).Date.ToString();

            //SetHavePricerIGHT();

            DataGridViewColumnSelector cs = new DataGridViewColumnSelector(ly_inma0010DataGridView, this.Text);
            cs.MaxHeight = 180;
            cs.Width = 800;

            cs.Set_dgvColumns();

            NewFrm.Show(this);
            Thread.Sleep(100);

            this.f_Item_dynamicpriceTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;

            this.ly_plan_getmaterialTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;


            this.ly_store_in_Maintenance_newTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;

            this.ly_store_out_Maintenancequery_newTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;


            this.lY_InventoryMaintenance_query_newTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;

            // this.lY_InventoryMaintenance_queryTableAdapter.Fill(this.lYStoreMange.LY_InventoryMaintenance_query, this .toolStripComboBox1.Text);

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

                CountMoney(lY_InventoryMaintenance_query_newBindingSource, ly_inma0010DataGridView);

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
           
            this.lY_InventoryMaintenance_query_newBindingSource.Filter = " ";

            //CountMoney(lY_InventoryMaintenance_query_newBindingSource, ly_inma0010DataGridView);
        }

        private void toolStripTextBox1_KeyUp(object sender, KeyEventArgs e)
        {
            

            string filterString; 
            filterString = GetDataGridviewMultiFilterString.DGVMultiFilterString(this.ly_inma0010DataGridView, this.toolStripTextBox1.Text);
             

            this.lY_InventoryMaintenance_query_newBindingSource.Filter = "(" + filterString + ") or 仓库='总计'";

            //CountMoney(lY_InventoryMaintenance_query_newBindingSource, ly_inma0010DataGridView);
 
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
                this.lY_InventoryMaintenance_query_newBindingSource.Filter = nowfilter;
            }
            else
            {
                this.lY_InventoryMaintenance_query_newBindingSource.Filter = "(" + nowfilter + ")" + " or 仓库='总计'";
            } 

            //CountMoney(lY_InventoryMaintenance_query_newBindingSource, ly_inma0010DataGridView);
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
            if (null == ly_inma0010DataGridView.CurrentRow)
            {
                this.ly_store_in_Maintenance_newTableAdapter.Fill(this.lYStoreMange.ly_store_in_Maintenance_new, "asd", this.toolStripComboBox1.Text, this.dateTimePicker1.Value.AddDays(1));
                this.ly_store_out_Maintenancequery_newTableAdapter.Fill(this.lYStoreMange.ly_store_out_Maintenancequery_new, "asd", this.toolStripComboBox1.Text, this.dateTimePicker1.Value.AddDays(1));
               

                return;
            }

            string s = this.ly_inma0010DataGridView.CurrentRow.Cells["物资编号"].Value.ToString();

            this.ly_store_in_Maintenance_newTableAdapter.Fill(this.lYStoreMange.ly_store_in_Maintenance_new, s, this.toolStripComboBox1.Text, this.dateTimePicker1.Value.AddDays(1));
            this.ly_store_out_Maintenancequery_newTableAdapter.Fill(this.lYStoreMange.ly_store_out_Maintenancequery_new, s, this.toolStripComboBox1.Text, this.dateTimePicker1.Value.AddDays(1));

        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            //NewFrm.Show(this);
            NewFrm.Show(this);
            Thread.Sleep(100);

            this.lY_InventoryMaintenance_query_newTableAdapter.Fill(this.lYStoreMange.LY_InventoryMaintenance_query_new, this.toolStripComboBox1.Text, this.dateTimePicker1.Value.AddDays(1));

            //CountMoney(lY_InventoryMaintenance_query_newBindingSource, ly_inma0010DataGridView);

            NewFrm.Hide(this);
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            if (null == this.ly_inma0010DataGridView.CurrentRow) return;
            SortForm DataSort = new SortForm();

            List<string> ls = new List<string>();
            ls.Add("id");


            DataSort.SetSortColumns(ly_inma0010DataGridView.Columns, ls);
            DataSort.ShowDialog();
            this.lY_InventoryMaintenance_query_newBindingSource.Sort = DataSort.GetSortString();
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

        private void toolStripComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ToolStripComboBox tsc = sender as ToolStripComboBox;

            if ("宁波维修" != tsc.Text)
            {
                this.lY_InventoryMaintenance_query_newTableAdapter.Fill(this.lYStoreMange.LY_InventoryMaintenance_query_new, "asd", this.dateTimePicker1.Value.AddDays(1));
                return;
            }

            NewFrm.Show(this);
            Thread.Sleep(100);

            this.lY_InventoryMaintenance_query_newTableAdapter.Fill(this.lYStoreMange.LY_InventoryMaintenance_query_new, tsc.Text, this.dateTimePicker1.Value.AddDays(1));
           // CountMoney(lY_InventoryMaintenance_query_newBindingSource, ly_inma0010DataGridView);

            NewFrm .Hide(this);

            //MessageBox .Show (tsc .SelectedIndex .ToString ());

            //if (tsc.SelectedIndex == 0)
            //{

            //    this.ly_plan_getmaterialDataGridView.Columns["父件"].Visible = false;
            //    this.ly_plan_getmaterialDataGridView.Columns["父件名称"].Visible = false;
            //    this.ly_plan_getmaterialDataGridView.Columns["生产数量"].Visible = false;
            //    this.ly_material_plan_mainBindingSource.Filter = "启用=1 and 批准=1";
            //    this.ly_material_plan_mainDataGridView.Columns["出库指令"].Visible = false;

            //    //this.comboBox1.Visible = false;
            //    this.label2.Visible = true;
            //    this.comboBox2.Visible = true;

            //    this.ly_material_plan_mainTableAdapter.Fill(this.lYPlanMange.ly_material_plan_main, "SCJH");
            //}
            //else if (tsc.SelectedIndex == 1)
            //{
            //    this.ly_plan_getmaterialDataGridView.Columns["父件"].Visible = false;
            //    this.ly_plan_getmaterialDataGridView.Columns["父件名称"].Visible = false;
            //    this.ly_plan_getmaterialDataGridView.Columns["生产数量"].Visible = false;
            //    this.ly_material_plan_mainBindingSource.Filter = "启用=1";
            //    this.ly_material_plan_mainDataGridView.Columns["出库指令"].Visible = false;

            //    //this.comboBox1.Visible = false;

            //    this.label2.Visible = true;
            //    this.comboBox2.Visible = true;
            //    //this.ly_material_plan_mainBindingSource.Filter = "";
            //    this.ly_material_plan_mainTableAdapter.Fill(this.lYPlanMange.ly_material_plan_main, "LLJH");
            //}

            //else if (tsc.SelectedIndex == 2)
            //{
            //    this.ly_plan_getmaterialDataGridView.Columns["父件"].Visible = false;
            //    this.ly_plan_getmaterialDataGridView.Columns["父件名称"].Visible = false;
            //    this.ly_plan_getmaterialDataGridView.Columns["生产数量"].Visible = false;

            //    this.ly_material_plan_mainDataGridView.Columns["出库指令"].Visible = true;

            //    //this.comboBox1.Visible = false;
            //    this.label2.Visible = true;
            //    this.comboBox2.Visible = true;
            //    this.ly_material_plan_mainBindingSource.Filter = "";
            //    this.ly_material_plan_mainTableAdapter.Fill(this.lYPlanMange.ly_material_plan_main, "SCPT");
            //}
            //else if (tsc.SelectedIndex == 3)
            //{
            //    this.ly_material_plan_mainBindingSource.Filter = "计划编号 = 'LSPT000aaaa'";
            //    return;

            //    this.ly_plan_getmaterialDataGridView.Columns["父件"].Visible = false;
            //    this.ly_plan_getmaterialDataGridView.Columns["父件名称"].Visible = false;
            //    this.ly_plan_getmaterialDataGridView.Columns["生产数量"].Visible = false;

            //    this.ly_material_plan_mainDataGridView.Columns["出库指令"].Visible = true;

            //    //this.comboBox1.Visible = false;
            //    this.label2.Visible = true;
            //    this.comboBox2.Visible = true;
            //    this.ly_material_plan_mainBindingSource.Filter = "计划编号 < 'LSPT0002096'";
            //    this.ly_material_plan_mainTableAdapter.Fill(this.lYPlanMange.ly_material_plan_main, "LSPT");
            //}
            //else if (tsc.SelectedIndex == 4)
            //{
            //    this.ly_plan_getmaterialDataGridView.Columns["父件"].Visible = false;
            //    this.ly_plan_getmaterialDataGridView.Columns["父件名称"].Visible = false;
            //    this.ly_plan_getmaterialDataGridView.Columns["生产数量"].Visible = false;
            //    this.ly_material_plan_mainBindingSource.Filter = "启用=1";
            //    this.ly_material_plan_mainDataGridView.Columns["出库指令"].Visible = false;

            //    this.label2.Visible = true;
            //    this.comboBox2.Visible = true;
            //    this.ly_material_plan_mainTableAdapter.Fill(this.lYPlanMange.ly_material_plan_main, "TZJH");
            //}
            //else if (tsc.SelectedIndex == 5)
            //{
            //    this.ly_plan_getmaterialDataGridView.Columns["父件"].Visible = false;
            //    this.ly_plan_getmaterialDataGridView.Columns["父件名称"].Visible = false;
            //    this.ly_plan_getmaterialDataGridView.Columns["生产数量"].Visible = false;
            //    this.ly_material_plan_mainBindingSource.Filter = "启用=1";
            //    this.ly_material_plan_mainDataGridView.Columns["出库指令"].Visible = false;

            //    this.label2.Visible = true;
            //    this.comboBox2.Visible = true;
            //    this.ly_material_plan_mainTableAdapter.Fill(this.lYPlanMange.ly_material_plan_main, "WXLL");
            //}
        }

       


        //private void fillToolStripButton_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        this.ly_store_in_Maintenance_newTableAdapter.Fill(this.lYStoreMange.ly_store_in_Maintenance_new, wzbhToolStripTextBox.Text, repair_sector_nameToolStripTextBox.Text, new System.Nullable<System.DateTime>(((System.DateTime)(System.Convert.ChangeType(markdateToolStripTextBox.Text, typeof(System.DateTime))))));
        //    }
        //    catch (System.Exception ex)
        //    {
        //        System.Windows.Forms.MessageBox.Show(ex.Message);
        //    }

        //}

        //private void fillToolStripButton_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        this.ly_store_in_Maintenance_ylTableAdapter.Fill(this.lYStoreMange.ly_store_in_Maintenance_yl, wzbhToolStripTextBox.Text, repair_sector_nameToolStripTextBox.Text);
        //    }
        //    catch (System.Exception ex)
        //    {
        //        System.Windows.Forms.MessageBox.Show(ex.Message);
        //    }

        //}

        //private void fillToolStripButton_Click_1(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        this.ly_store_out_MaintenancequeryTableAdapter.Fill(this.lYStoreMange.ly_store_out_Maintenancequery, wzbhToolStripTextBox.Text, repair_sector_nameToolStripTextBox.Text);
        //    }
        //    catch (System.Exception ex)
        //    {
        //        System.Windows.Forms.MessageBox.Show(ex.Message);
        //    }

        //}

        //    private void fillToolStripButton_Click(object sender, EventArgs e)
        //    {
        //        try
        //        {
        //            this.lY_InventoryMaintenance_queryTableAdapter.Fill(this.lYStoreMange.LY_InventoryMaintenance_query, yonghu_codeToolStripTextBox.Text);
        //        }
        //        catch (System.Exception ex)
        //        {
        //            System.Windows.Forms.MessageBox.Show(ex.Message);
        //        }

        //    }
    }
    }
