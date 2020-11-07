using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HappyYF.Infrastructure.Repositories;
using DataGridFilter;

namespace HappyYF.YuanXin.WorkSet
{
    public partial class LY_QualityDebug_Sumreport : Form
    {
        public LY_QualityDebug_Sumreport()
        {
            InitializeComponent();
        }

        private void LY_QualityDebug_Sumreport_Load(object sender, EventArgs e)
        {
            this.ly_quality_debug_SumTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
           

            this.dateTimePicker5.Text = SQLDatabase.GetNowdate().Date.Year.ToString() + "-01" + "-01";
            this.dateTimePicker6.Text = SQLDatabase.GetNowdate().AddDays(0).Date.ToString();

        }

         private void SetDGVFir(DataGridView nowDGV)
        {
            //nowDGV.Columns[0].Visible = false;

            for (int i = 0; i < nowDGV.Columns.Count; i++)
            {
                nowDGV.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;



                //nowDGV.Columns[i].ValueType System .Type .nowDGV.Columns[5].DefaultCellStyle.Alignment

                if ("Decimal" == nowDGV.Columns[i].ValueType.Name)
                {
                    nowDGV.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

                }

                if (nowDGV.Columns[i].HeaderText.Contains("总计") || nowDGV.Columns[i].HeaderText.Contains("增长率"))
                {
                    nowDGV.Columns[i].DefaultCellStyle.BackColor = Color.SkyBlue;
                    nowDGV.Columns[i].DefaultCellStyle.ForeColor = Color.Red;
                }

                if (nowDGV.Columns[i].HeaderText.Contains("总计区域") || nowDGV.Columns[i].HeaderText.Contains("合计"))
                {
                    nowDGV.Columns[i].DefaultCellStyle.BackColor = Color.Teal;
                    nowDGV.Columns[i].DefaultCellStyle.ForeColor = Color.White;
                }



            }
         }

         private void toolStripButton22_Click(object sender, EventArgs e)
         {
             this.ly_quality_debug_SumTableAdapter.Fill(this.lYSalseRepair.ly_quality_debug_Sum, "full","full", this.dateTimePicker5.Value, this.dateTimePicker6.Value.AddDays(1));

             SetDGVFir(this.ly_sales_standard_SumDataGridView); 
         }

         private void toolStripButton17_Click(object sender, EventArgs e)
         {
             ExportDataGridviewTOExcell.ExportDataGridview(this.ly_sales_standard_SumDataGridView, true);
         }

         private void toolStripButton19_Click(object sender, EventArgs e)
         {
             if (null == this.ly_sales_standard_SumDataGridView.CurrentRow) return;

             FilterForm filterForm = new FilterForm();




             //SumQueryDataSet qds;
             //qds = new SumQueryDataSet();

             List<string> ls = new List<string>();
             ls.Add("id");


             filterForm.SetSourceColumns(this.lYSalseRepair.ly_quality_debug_Sum.Columns, ls);

             filterForm.ShowDialog();

             this.ly_quality_debug_SumBindingSource.Filter = filterForm.GetFilterString();
         }

         private void toolStripButton20_Click(object sender, EventArgs e)
         {
             if (null == this.ly_sales_standard_SumDataGridView.CurrentRow) return;
             SortForm DataSort = new SortForm();

             List<string> ls = new List<string>();
             ls.Add("id");


             DataSort.SetSortColumns(this.lYSalseRepair.ly_quality_debug_Sum.Columns, ls);
             DataSort.ShowDialog();
             this.ly_quality_debug_SumBindingSource.Sort = DataSort.GetSortString();
         }

       

        //private void fillToolStripButton_Click_1(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        this.ly_quality_debug_SumTableAdapter.Fill(this.lYSalseRepair.ly_quality_debug_Sum, salesperson_codeToolStripTextBox.Text, selcodeToolStripTextBox.Text, new System.Nullable<System.DateTime>(((System.DateTime)(System.Convert.ChangeType(begindateToolStripTextBox.Text, typeof(System.DateTime))))), new System.Nullable<System.DateTime>(((System.DateTime)(System.Convert.ChangeType(enddateToolStripTextBox.Text, typeof(System.DateTime))))));
        //    }
        //    catch (System.Exception ex)
        //    {
        //        System.Windows.Forms.MessageBox.Show(ex.Message);
        //    }

        //}
    }

       
}
