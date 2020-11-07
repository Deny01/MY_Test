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
    public partial class LY_Store_ClassQuery : Form
    {
        private string nowsort;

        public string Nowsort
        {
            get { return nowsort; }
            set { nowsort = value; }
        }

        private string nowsortcode;

        public string Nowsortcode
        {
            get { return nowsortcode; }
            set { nowsortcode = value; }
        }
        
        public LY_Store_ClassQuery()
        {
            InitializeComponent();
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

            //decimal sum_accrual = 0;
            //decimal sum_compensate_accrual = 0;
            //decimal sum_left_accrual = 0;



            foreach (DataGridViewRow dr in dg.Rows)
            {
                //if (System.DBNull.Value == dr.Cells["数量"].Value)
                //    sum_qty = sum_qty + 0;
                //else
                //    sum_qty = sum_qty + decimal.Parse(dr.Cells["数量"].Value.ToString());

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
                    //////////////////////////////
                    if (System.DBNull.Value == dr.Cells["计划出库金额"].Value)
                        planout_money = planout_money + 0;
                    else
                        planout_money = planout_money + decimal.Parse(dr.Cells["计划出库金额"].Value.ToString());

                    if (System.DBNull.Value == dr.Cells["计划库存金额"].Value)
                        plan_storemoney = plan_storemoney + 0;
                    else
                        plan_storemoney = plan_storemoney + decimal.Parse(dr.Cells["计划库存金额"].Value.ToString());


                   
                }

                //if (System.DBNull.Value == dr.Cells["采购金额"].Value)
                //    sum_buymoney = sum_buymoney + 0;
                //else
                //    sum_buymoney = sum_buymoney + decimal.Parse(dr.Cells["采购金额"].Value.ToString());


                //if (System.DBNull.Value == dr.Cells["利息"].Value)
                //    sum_accrual = sum_accrual + 0;
                //else
                //    sum_accrual = sum_accrual + decimal.Parse(dr.Cells["利息"].Value.ToString());

                //if (System.DBNull.Value == dr.Cells["还息"].Value)
                //    sum_compensate_accrual = sum_compensate_accrual + 0;
                //else
                //    sum_compensate_accrual = sum_compensate_accrual + decimal.Parse(dr.Cells["还息"].Value.ToString());

                //if (System.DBNull.Value == dr.Cells["欠息"].Value)
                //    sum_left_accrual = sum_left_accrual + 0;
                //else
                //    sum_left_accrual = sum_left_accrual + decimal.Parse(dr.Cells["欠息"].Value.ToString());



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
            //dg.CurrentRow.Cells["欠息"].Value = sum_left_accrual;

           



            bs.EndEdit();

            bs.Position = 0;
        }

        private void LY_MaterialMange_Load(object sender, EventArgs e)
        {
            NewFrm.Show(this);

            this.Text = this.nowsort + "库存";
            
            this.ly_plan_getmaterialTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
           

            this.ly_store_in_ylTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;

            this.ly_store_outqueryTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;


            this.lY_Inventory_queryClassTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;

            this.lY_Inventory_queryClassTableAdapter.Fill(this.lYStoreMange.LY_Inventory_queryClass, this.nowsortcode);

           

            SetRowBackground();

            ///////////////////////
            DataGridViewColumnSelector cs = new DataGridViewColumnSelector(ly_inma0010DataGridView, this.Text);
            cs.MaxHeight = 180;
            cs.Width = 800;

            cs.Set_dgvColumns();
            //////////////////////////////// this.lY_Inventory_queryClassTableAdapter.Fill(this.lYStoreMange.LY_Inventory_queryClass, this.nowsortcode);


            CountMoney(lY_Inventory_queryClassBindingSource, ly_inma0010DataGridView);

            SetRowBackground();
            NewFrm.Hide(this);

        }

        private void toolStripTextBox1_Enter(object sender, EventArgs e)
        {
            toolStripTextBox1.Text = "";

            this.lY_Inventory_queryClassBindingSource.Filter = "";

            //CountMoney(ly_inma0010BindingSource, ly_inma0010DataGridView);
        }

        private void toolStripTextBox1_KeyUp(object sender, KeyEventArgs e)
        {
            string dFilter = "";

            //for (int i = 0; i < this.hT_Vehicle_collectDataGridView.ColumnCount; i++)
            for (int i = 1; i < 10; i++)
            {
                string tempColumnName = this.ly_inma0010DataGridView.Columns[i].DataPropertyName;
              
                if (i != 9)
                    dFilter = dFilter + tempColumnName + " like  '*" + this.toolStripTextBox1.Text + "*' or ";
                else
                    dFilter = dFilter + tempColumnName + " like  '*" + this.toolStripTextBox1.Text + "*' ";

            }

            if (this.toolStripTextBox1.Text.Replace(" ", "").Length > 0)

                this.lY_Inventory_queryClassBindingSource.Filter = dFilter + " or 仓库='总计'";
            else
                this.lY_Inventory_queryClassBindingSource.Filter = " ";

            CountMoney(lY_Inventory_queryClassBindingSource, ly_inma0010DataGridView);
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
                this.lY_Inventory_queryClassBindingSource.Filter = nowfilter;
            }
            else
            {
                this.lY_Inventory_queryClassBindingSource.Filter = "(" + nowfilter + ")" + " or 仓库='总计'";
            }
            
            //this.ly_inma0010BindingSource.Filter = filterForm.GetFilterString() ;

            CountMoney(lY_Inventory_queryClassBindingSource, ly_inma0010DataGridView);
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

        private void ly_inma0010DataGridView_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            SetRowBackground();
        }

        private void ly_inma0010DataGridView_SelectionChanged(object sender, EventArgs e)
        {
            if (null == ly_inma0010DataGridView.CurrentRow) return;

            string s = this.ly_inma0010DataGridView.CurrentRow.Cells["物资编号"].Value.ToString();

            this .ly_store_in_ylTableAdapter.Fill ( this .lYStoreMange .ly_store_in_yl ,s);
            this.ly_store_outqueryTableAdapter.Fill(this.lYStoreMange.ly_store_outquery, s);
            this.ly_plan_getmaterialTableAdapter.Fill(this.lYStoreMange.ly_plan_getmaterial,s);
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            NewFrm.Show(this);

            this.lY_Inventory_queryClassTableAdapter.Fill(this.lYStoreMange.LY_Inventory_queryClass, this.nowsortcode);
            CountMoney(lY_Inventory_queryClassBindingSource, ly_inma0010DataGridView);

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
            this.lY_Inventory_queryClassBindingSource.Sort = DataSort.GetSortString();
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            if (null == this.ly_inma0010DataGridView.CurrentRow) return;

            string nowplannum;
            string noename;

            NewFrm.Show(this);
            foreach (DataGridViewRow dgr in ly_inma0010DataGridView.Rows)
            {
                nowplannum = dgr.Cells["物资编号"].Value.ToString();

                noename = dgr.Cells["名称"].Value.ToString();

                //this.toolStripLabel3.Text = plannum;
                //this.toolStripLabel3.Invalidate();

                NewFrm.Notify(this, "正在计算:  (" + nowplannum + ")" + noename + "   借用");



                Countmoney(nowplannum);

            }

            NewFrm.Hide(this);

            NewFrm.Show(this);

            this.lY_Inventory_queryClassTableAdapter.Fill(this.lYStoreMange.LY_Inventory_queryClass, this.nowsortcode);

            CountMoney(lY_Inventory_queryClassBindingSource, ly_inma0010DataGridView);

            NewFrm.Hide(this);
        }


        private static void Countmoney(string nowplannum)
        {
            string updstr = " update ly_inma0010  " +
                          "  set borrowcount=dbo.f_LY_borrow_Storecount(wzbh) "
                          + " where  wzbh='" + nowplannum + "'";



            SqlConnection sqlConnection1 = new SqlConnection(SQLDatabase.Connectstring);
            SqlCommand cmd = new SqlCommand();

            cmd.CommandText = updstr;
            cmd.CommandType = CommandType.Text;
            cmd.Connection = sqlConnection1;
            cmd.CommandTimeout = 0;

            using (TransactionScope scope = new TransactionScope())
            {

                sqlConnection1.Open();
                try
                {

                    cmd.ExecuteNonQuery();



                    scope.Complete();



                }
                catch (SqlException sqle)
                {


                    MessageBox.Show(sqle.Message.Split('*')[0]);
                }


                finally
                {
                    sqlConnection1.Close();


                }


            }
        }

        private void ly_inma0010DataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        //private void fillToolStripButton_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        this.lY_Inventory_queryClassTableAdapter.Fill(this.lYStoreMange.LY_Inventory_queryClass, yonghu_codeToolStripTextBox.Text);
        //    }
        //    catch (System.Exception ex)
        //    {
        //        System.Windows.Forms.MessageBox.Show(ex.Message);
        //    }

        //}

       
    }
}
