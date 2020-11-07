using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.Data.SqlClient;

using HappyYF.Infrastructure.Repositories;
using DataGridFilter;


namespace HappyYF.YuanXin.WorkSet
{
    public partial class Sum_Query : Form
    {

        private string  stdfilterStr = " (班级状态 = 'Y' AND 在读 = 'Y')  ";
        private  string filterString ="";
        
        public Sum_Query()
        {
            InitializeComponent();
        }

        private void Sum_Query_Load(object sender, EventArgs e)
        {
            
            this.sum_queryTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            this.tableAdapterManager.Connection = this.sum_queryTableAdapter.Connection;

            this.sum_queryTableAdapter.Fill(this.sumCard_DataSet.Sum_query);
            this.sum_queryBindingSource.Filter = stdfilterStr;// +" and " + filterString;

            AddSummationRow(sum_queryBindingSource, sum_queryDataGridView);

        }
        private void AddSummationRow(BindingSource bs, DataGridView dg)
        {
            //this.yX_daywork_recordDataGridView1.Columns[6].summation
            //this.dailyoperation.YX_daywork_record.Columns[7].
            if (null == dg.CurrentRow) return;

            if (bs.Find("班号", "合计") > -1)
                bs.RemoveAt(bs.Find("班号", "合计"));


           
            decimal sum_stdmoney = 0;
            decimal sum_realmoney = 0;

            decimal sum_discount = 0;

            decimal sum_hadreadrealmoney = 0;

            //decimal sum_cardbalance = 0;
            
            decimal sum_leftmoney = 0;
            
            decimal sum_realbalance = 0;
            //decimal sum_hadreadstdmoney = 0;
           
           



            foreach (DataGridViewRow dr in dg.Rows)
            {
                
                if (System.DBNull.Value == dr.Cells["标准学费"].Value)
                    sum_stdmoney = sum_stdmoney + 0;
                else
                    sum_stdmoney = sum_stdmoney + decimal.Parse(dr.Cells["标准学费"].Value.ToString());

                if (System.DBNull.Value == dr.Cells["实交学费"].Value)
                    sum_realmoney = sum_realmoney + 0;
                else
                    sum_realmoney = sum_realmoney + decimal.Parse(dr.Cells["实交学费"].Value.ToString());


                if (System.DBNull.Value == dr.Cells["折扣"].Value)
                    sum_discount = sum_discount + 0;
                else
                    sum_discount = sum_discount + decimal.Parse(dr.Cells["折扣"].Value.ToString());

                
                if (System.DBNull.Value == dr.Cells["已用学费"].Value)
                    sum_hadreadrealmoney = sum_hadreadrealmoney + 0;
                else
                    sum_hadreadrealmoney = sum_hadreadrealmoney + decimal.Parse(dr.Cells["已用学费"].Value.ToString());

                if (System.DBNull.Value == dr.Cells["剩余学费"].Value)
                    sum_leftmoney = sum_leftmoney + 0;
                else
                    sum_leftmoney = sum_leftmoney + decimal.Parse(dr.Cells["剩余学费"].Value.ToString());


                //if ( 0 >  decimal.Parse(dr.Cells[9].Value .ToString()))
                //dr.DefaultCellStyle.BackColor = Color.Red;

            }
            bs.AddNew();

            dg.CurrentRow.Cells["班号"].Value = "合计";
            dg.CurrentRow.Cells["卡号"].Value = "";
           
            dg.CurrentRow.Cells["标准学费"].Value = sum_stdmoney;
            dg.CurrentRow.Cells["实交学费"].Value = sum_realmoney;
            dg.CurrentRow.Cells["折扣"].Value = sum_discount;
            dg.CurrentRow.Cells["已用学费"].Value = sum_hadreadrealmoney;
            dg.CurrentRow.Cells["剩余学费"].Value = sum_leftmoney;

            dg.CurrentRow.Cells["班级状态"].Value = "Y";
            dg.CurrentRow.Cells["在读"].Value = "Y";
           
            
            //dg.CurrentRow.Cells["已上应收"].Value = sum_hadreadstdmoney;
            //dg.CurrentRow.Cells["已上实收"].Value = sum_hadreadrealmoney;
            //dg.CurrentRow.Cells["已上折扣"].Value = sum_hadreaddiscount;

            bs.EndEdit();

            bs.Position = 0;




        }
        public bool ExportDataGridview(DataGridView gridView, bool isShowExcle)
        {
            if (gridView.Rows.Count == 0)
                return false;
            //建立Excel对象
            Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();
            excel.Application.Workbooks.Add(true);
            excel.Visible = isShowExcle;
            //生成字段名称
            for (int i = 0; i < gridView.ColumnCount; i++)
            {
                excel.Cells[1, i + 1] = gridView.Columns[i].HeaderText;
            }
            //填充数据
            for (int i = 0; i <= gridView.RowCount - 1; i++)
            {
                for (int j = 0; j < gridView.ColumnCount; j++)
                {
                    if (gridView[j, i].ValueType == typeof(string))
                    {
                        excel.Cells[i + 2, j + 1] = "'" + gridView[j, i].Value.ToString();
                    }
                    else
                    {
                        excel.Cells[i + 2, j + 1] = gridView[j, i].Value.ToString();
                    }
                }
            }
            return true;
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            ExportDataGridview(sum_queryDataGridView, true);
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            //if (null == this.sum_queryDataGridView.CurrentRow) return;

            FilterForm filterForm = new FilterForm();




            List<string> ls = new List<string>();
            ls.Add("id");


            filterForm.SetSourceColumns(this.sumCard_DataSet.Sum_query .Columns, ls);

            filterForm.ShowDialog();




               filterString = filterForm.GetFilterString();

               if (null == filterString)
                   filterString = "";
                   setfilterStr();
                   AddSummationRow(sum_queryBindingSource, sum_queryDataGridView);

        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            if (null == this.sum_queryDataGridView.CurrentRow) return;
            SortForm DataSort = new SortForm();

            List<string> ls = new List<string>();
            ls.Add("id");


            DataSort.SetSortColumns(this.sumCard_DataSet.Sum_query.Columns, ls);
            DataSort.ShowDialog();
            this.sum_queryBindingSource.Sort = DataSort.GetSortString();
        
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            this.sum_queryTableAdapter.Fill(this.sumCard_DataSet.Sum_query);




            setfilterStr();


            AddSummationRow(sum_queryBindingSource, sum_queryDataGridView);
        }

        private void setfilterStr()
        {
            if ("" == stdfilterStr && "" == filterString)
                this.sum_queryBindingSource.Filter = "";
            if ("" != stdfilterStr && "" == filterString)
                this.sum_queryBindingSource.Filter = stdfilterStr;
            if ("" == stdfilterStr && "" != filterString)
                this.sum_queryBindingSource.Filter = filterString;
            if ("" != stdfilterStr && "" != filterString)
                this.sum_queryBindingSource.Filter = stdfilterStr + " and " + filterString;
        }

        private void sum_queryDataGridView_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            DataGridView dgv = sender as DataGridView;

            if ("合计" == dgv.Rows[e.RowIndex].Cells["班号"].Value.ToString()) return;

            if (1 > decimal.Parse(dgv.Rows[e.RowIndex].Cells["剩余学费"].Value.ToString()))
                dgv.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.Red;

            //if (2 < decimal.Parse(dgv.Rows[e.RowIndex].Cells["剩余学费"].Value.ToString()))
            //    dgv.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.Green;

            if (1 > int .Parse ( dgv.Rows[e.RowIndex].Cells["剩余课时"].Value.ToString()))
                dgv.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.Red ;

            if (3 > int.Parse(dgv.Rows[e.RowIndex].Cells["剩余课时"].Value.ToString()))
                dgv.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.Orange ;

            
        }
        private void SetCheckbox()
        {
            if (this.checkBox1.Checked && this.checkBox2.Checked)
                this.stdfilterStr = "";
            if (!this.checkBox1.Checked && this.checkBox2.Checked)
                this.stdfilterStr = " (班级状态 = 'Y')  ";
            if (this.checkBox1.Checked && !this.checkBox2.Checked)
                this.stdfilterStr = " (在读 = 'Y')  ";
            if (!this.checkBox1.Checked && !this.checkBox2.Checked)
                this.stdfilterStr = " (班级状态 = 'Y' AND 在读 = 'Y')  ";
        }

        

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            SetCheckbox();
        }

      
        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            SetCheckbox();
        }

        private void sum_queryDataGridView_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            //DataGridView dv = ((DataGridView)sender);
            
            //if ("合计" == dv.Rows[e.RowIndex].Cells["班级"].Value.ToString().Replace(" ", ""))
            //    dv.Rows[e.RowIndex].Cells["班级状态"]. = false;
            
        }
    }
}
