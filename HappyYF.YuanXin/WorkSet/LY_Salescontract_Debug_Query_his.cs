using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Transactions;
using HappyYF.Infrastructure.Repositories;
using DataGridFilter;

namespace HappyYF.YuanXin.WorkSet
{
    public partial class LY_Salescontract_Debug_Query_his : Form
    {
        private LY_Salescontract_GroupDebug ownerForm;


        int nowid= 0;

        string nowrecord= "";

        string nowgroup = "";

        string nowplan = "";

       

        //int detail_Id=0;

        public LY_Salescontract_GroupDebug OwnerForm
        {
            get { return ownerForm; }
            set { ownerForm = value; }
        }

        public LY_Salescontract_Debug_Query_his()
        {
            InitializeComponent();
        }

        private void LY_Production_order_Mange_Load(object sender, EventArgs e)
        {
           
           

            //this.Location = this.ownerForm.PointToScreen(new Point(this.ownerForm.Location .X+10 ,this .ownerForm .Location .Y+5 ));
            //this .Width =this .ownerForm.Width-20 ;
            //this .Height =360;

            //MakeDropdownlist();

            this.WindowState = FormWindowState.Maximized;
            
            //this.dateTimePicker1.Text = DateTime.Today.AddMonths(-1).Date.ToString();
            this.dateTimePicker1.Text = "2009-01-01";
            this.dateTimePicker2.Text = DateTime.Today.AddDays(1).Date.ToString();




            this.lY_qualityDebug_historyInfoTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;

            //this.ly_sales_test_detail1TableAdapter.Fill(this.lYSalseMange.ly_sales_test_detail1,DateTime.Parse(this.dateTimePicker1.Text).Date, DateTime.Parse(this.dateTimePicker2.Text).Date);

            this.lY_qualityDebug_historyInfoTableAdapter.Fill(this.lYSalseMange.LY_qualityDebug_historyInfo, DateTime.Parse(this.dateTimePicker1.Text).Date, DateTime.Parse(this.dateTimePicker2.Text).Date);
            
            
            ///////////////////////
            DataGridViewColumnSelector cs = new DataGridViewColumnSelector(lY_qualityDebug_historyInfoDataGridView, this.Text);
            cs.MaxHeight = 180;
            cs.Width = 800;

            cs.Set_dgvColumns();
            ////////////////////////////////



           

        }

        //private void MakeDropdownlist()
        //{
        //    this.toolStripDropDownButton1.DropDownItems.Clear();

        //    this.toolStripDropDownButton1.DropDownItems.Add("全部");
        //    this.toolStripDropDownButton1.DropDownItems.Add("杜海峰");
        //    this.toolStripDropDownButton1.DropDownItems.Add("王维东");
        //}

       


        private void toolStripTextBox2_Enter(object sender, EventArgs e)
        {
            toolStripTextBox2.Text = "";

            this.lY_qualityDebug_historyInfoBindingSource.Filter = "";
        }

        private void toolStripTextBox2_KeyUp(object sender, KeyEventArgs e)
        {
            string dFilter = "";

            dFilter = GetDataGridviewMultiFilterString.DGVMultiFilterString(this.lY_qualityDebug_historyInfoDataGridView, this.toolStripTextBox2.Text);

            if (null == dFilter)
                dFilter = "";

            this.lY_qualityDebug_historyInfoBindingSource.Filter = dFilter;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //this.ly_sales_test_detail1TableAdapter.Fill(this.lYSalseMange.ly_sales_test_detail1, DateTime.Parse(this.dateTimePicker1.Text).Date, DateTime.Parse(this.dateTimePicker2.Text).Date);
            this.lY_qualityDebug_historyInfoTableAdapter.Fill(this.lYSalseMange.LY_qualityDebug_historyInfo, DateTime.Parse(this.dateTimePicker1.Text).Date, DateTime.Parse(this.dateTimePicker2.Text).Date);
            

        }

       

        private void ly_sales_test_detail1DataGridView_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            //if (null == this.ly_sales_test_detail1DataGridView.CurrentRow)
            //{

                
            //    return;
                
            //}



            //nowid = int.Parse(this.ly_sales_test_detail1DataGridView.Rows[e.RowIndex].Cells["id"].Value.ToString());

            //nowrecord = this.ly_sales_test_detail1DataGridView.Rows[e.RowIndex].Cells["配调记录"].Value.ToString();

            //nowgroup = this.ly_sales_test_detail1DataGridView.Rows[e.RowIndex].Cells["配套号"].Value.ToString();

            //nowplan = this.ly_sales_test_detail1DataGridView.Rows[e.RowIndex].Cells["依赖书号"].Value.ToString();

            //if (null != this.OwnerForm)
            //{
            //    this.OwnerForm.find_NowProduc(nowplan, nowgroup, nowrecord, nowid);
            //}

            ////////////////////////////////////////////

        }

        private void toolStripButton8_Click(object sender, EventArgs e)
        {
            ExportDataGridviewTOExcell.ExportDataGridview(this.lY_qualityDebug_historyInfoDataGridView, true);
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if (null == this.lY_qualityDebug_historyInfoDataGridView.CurrentRow) return;

            FilterForm filterForm = new FilterForm();

            //SumQueryDataSet qds;
            //qds = new SumQueryDataSet();

            List<string> ls = new List<string>();
            ls.Add("id");


            filterForm.SetSourceColumns(this.lYSalseMange.LY_qualityDebug_historyInfo.Columns, ls);

            filterForm.ShowDialog();

            this.lY_qualityDebug_historyInfoBindingSource.Filter = filterForm.GetFilterString();
            //AddSummationRow_New(ly_sales_repairstandard_ReportBindingSource, ly_sales_contract_standard_ReportDataGridView);
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            if (null == this.lY_qualityDebug_historyInfoDataGridView.CurrentRow) return;
            SortForm DataSort = new SortForm();

            List<string> ls = new List<string>();
            ls.Add("id");


            DataSort.SetSortColumns(this.lYSalseMange.LY_qualityDebug_historyInfo.Columns, ls);
            DataSort.ShowDialog();
            this.lY_qualityDebug_historyInfoBindingSource.Sort = DataSort.GetSortString() ;
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            this.lY_qualityDebug_historyInfoTableAdapter.Fill(this.lYSalseMange.LY_qualityDebug_historyInfo, DateTime.Parse(this.dateTimePicker1.Text).Date, DateTime.Parse(this.dateTimePicker2.Text).Date);
            
        }

        private void ly_sales_test_detail1DataGridView_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
           
              DataGridView dgv = sender as DataGridView;

            if (null == dgv.CurrentRow) return;

            /////////////////////////////
            if ("操作人" == dgv.CurrentCell.OwningColumn.Name)
            {


                if (e.Button == MouseButtons.Right)
                {
                    //MessageBox.Show(dgv.CurrentCell.Value.ToString());
                    this.lY_qualityDebug_historyInfoBindingSource.Filter = " 操作人 = '" + dgv.CurrentCell.Value.ToString() + "'";
                }
                //else
                //{
                //    MessageBox.Show(dgv.CurrentCell.Value.ToString());

                //}
            }

            if ("客户名称1" == dgv.CurrentCell.OwningColumn.Name)
            {


                if (e.Button == MouseButtons.Right)
                {
                    //MessageBox.Show(dgv.CurrentCell.Value.ToString());


                    this.lY_qualityDebug_historyInfoBindingSource.Filter = " 客户名称 = '" + dgv.CurrentCell.Value.ToString() + "'";
                }
                //else
                //{
                //    MessageBox.Show(dgv.CurrentCell.Value.ToString());

                //}借用出厂编号
            }

            if ("出厂编号" == dgv.CurrentCell.OwningColumn.Name)
            {


                if (e.Button == MouseButtons.Right)
                {
                    //MessageBox.Show(dgv.CurrentCell.Value.ToString());


                    this.lY_qualityDebug_historyInfoBindingSource.Filter = " 出厂编号 = '" + dgv.CurrentCell.Value.ToString() + "'";
                }
                //else
                //{
                //    MessageBox.Show(dgv.CurrentCell.Value.ToString());

                //}借用出厂编号
            }



        }

        //private void toolStripDropDownButton1_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        //{
        //    string  i ;

        //    i = e.ClickedItem.Text;
        //}

        
      

       
       

       

       



    }
}
