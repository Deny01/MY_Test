using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Transactions;
using HappyYF.Infrastructure.Repositories;
using DataGridFilter;

namespace HappyYF.YuanXin.WorkSet
{
    public partial class LY_MaterialTask_SumAnalysis : Form
    {
        public LY_MaterialTask_SumAnalysis()
        {
            InitializeComponent();
            this.ly_material_task_viewNewTableAdapter.CommandTimeout = 0;
        }

        private void LY_MaterialTask_SumAnalysis_Load(object sender, EventArgs e)
        {
            this.ly_material_task_viewNewTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            //this.ly_material_task_viewNewTableAdapter.Fill(this.lYPlanMange.ly_material_task_viewNew);

        }

        private void toolStripButton22_Click(object sender, EventArgs e)
        {
            NewFrm.Show(this);
            this.ly_material_task_viewNewTableAdapter.Fill(this.lYPlanMange.ly_material_task_viewNew);
            AddSummationRow_New(ly_material_task_viewNewBindingSource, ly_material_task_viewNewDataGridView);
            NewFrm.Hide(this);
        }

        protected bool IsInteger(object o)
        {
            if (o is Int64)
                return true;
            if (o is Int32)
                return true;
            if (o is Int16)
                return true;
            return false;
        }
        protected bool IsDecimal(object o)
        {
            if (o is Decimal)
                return true;
            if (o is Single)
                return true;
            if (o is Double)
                return true;
            return false;
        }

        private void AddSummationRow_New(BindingSource bs, DataGridView dgv)
        {
            //InitializeApp();
            //return;

            DataRow sumdr = (((DataSet)bs.DataSource).Tables[bs.DataMember]).NewRow();

            if (-1 != bs.Find("计划编码", "合计"))
            {
                bs.RemoveAt(bs.Find("计划编码", "合计"));
            }

            foreach (DataGridViewRow dgvRow in dgv.Rows)
            {
                foreach (DataGridViewCell dgvCell in dgvRow.Cells)
                {
                    //foreach (DataGridViewColumn dgvColumn in dgv.Columns)
                    //{
                    if (dgvCell.Value != null && !(dgvCell.Value is DBNull))
                    {
                        if (IsInteger(dgvCell.Value))
                        {
                            if ("年份" != dgvCell.OwningColumn.HeaderText && "月份" != dgvCell.OwningColumn.HeaderText)
                            {
                                if (sumdr[dgvCell.OwningColumn.DataPropertyName] == null || (sumdr[dgvCell.OwningColumn.DataPropertyName] is DBNull))
                                    sumdr[dgvCell.OwningColumn.DataPropertyName] = 0;


                                sumdr[dgvCell.OwningColumn.DataPropertyName] = Convert.ToInt64(sumdr[dgvCell.OwningColumn.DataPropertyName]) + Convert.ToInt64(dgvCell.Value);
                            }
                        }
                        else if (IsDecimal(dgvCell.Value))
                        {
                            if ("年份" != dgvCell.OwningColumn.HeaderText && "月份" != dgvCell.OwningColumn.HeaderText)
                            {
                                if (sumdr[dgvCell.OwningColumn.DataPropertyName] == null || (sumdr[dgvCell.OwningColumn.DataPropertyName] is DBNull))
                                    sumdr[dgvCell.OwningColumn.DataPropertyName] = 0;
                                //dgvCell .OwningColumn.Name  dgvCell.ColumnIndex

                                sumdr[dgvCell.OwningColumn.DataPropertyName] = Convert.ToDecimal(sumdr[dgvCell.OwningColumn.DataPropertyName]) + Convert.ToDecimal(dgvCell.Value);
                            }
                        }

                        //sumBox.Text = string.Format("{0}", sumBox.Tag);
                        //sumBox.Invalidate();

                    }
                    //}
                }

            }

            //sumdr["sumorder"] = "02";
            sumdr["计划编码"] = "合计";
            //sumdr["客户"] = "";
            //sumdr["税务"] = "";
            ((DataSet)bs.DataSource).Tables[bs.DataMember].Rows.Add(sumdr);
            bs.ResetBindings(true);

        }

        private void toolStripTextBox3_KeyUp(object sender, KeyEventArgs e)
        {
            string filterString;


            filterString = GetDataGridviewMultiFilterString.DGVMultiFilterString(this.ly_material_task_viewNewDataGridView, this.toolStripTextBox3.Text);


           
            this.ly_material_task_viewNewBindingSource.Filter = "(" + filterString + ") or 计划编码='合计'";
            AddSummationRow_New(ly_material_task_viewNewBindingSource, ly_material_task_viewNewDataGridView);
        }

        public void setFilter(string filterString)
        {

            this.ly_material_task_viewNewBindingSource.Filter = "(物料编码='" + filterString + "') or 计划编码='合计'";
            this.ly_material_task_viewNewTableAdapter.Fill(this.lYPlanMange.ly_material_task_viewNew);
           
            AddSummationRow_New(ly_material_task_viewNewBindingSource, ly_material_task_viewNewDataGridView);
        
        
        }

        private void toolStripTextBox3_Enter(object sender, EventArgs e)
        {
            toolStripTextBox3.Text = "";

            this.ly_material_task_viewNewBindingSource.Filter = "";
            AddSummationRow_New(ly_material_task_viewNewBindingSource, ly_material_task_viewNewDataGridView);
        }

        private void toolStripButton17_Click(object sender, EventArgs e)
        {
            ExportDataGridviewTOExcell.ExportDataGridview(this.ly_material_task_viewNewDataGridView, true);
        }

        private void toolStripButton19_Click(object sender, EventArgs e)
        {
            if (null == this.ly_material_task_viewNewDataGridView.CurrentRow) return;

            FilterForm filterForm = new FilterForm();

            //SumQueryDataSet qds;
            //qds = new SumQueryDataSet();

            List<string> ls = new List<string>();
            ls.Add("id");


            filterForm.SetSourceColumns(this.lYPlanMange.ly_material_task_viewNew.Columns, ls);

            filterForm.ShowDialog();

            string filterstr = filterForm.GetFilterString();
            if (!string.IsNullOrEmpty(filterstr))
            {

                this.ly_material_task_viewNewBindingSource.Filter = "(" + filterstr + ") or 计划编码='合计'";
            }
            AddSummationRow_New(ly_material_task_viewNewBindingSource, ly_material_task_viewNewDataGridView);
        }

        private void toolStripButton20_Click(object sender, EventArgs e)
        {
            if (null == this.ly_material_task_viewNewDataGridView.CurrentRow) return;
            SortForm DataSort = new SortForm();

            List<string> ls = new List<string>();
            ls.Add("id");


            DataSort.SetSortColumns(this.lYPlanMange.ly_material_task_viewNew.Columns, ls);
            DataSort.ShowDialog();
            this.ly_material_task_viewNewBindingSource.Sort = DataSort.GetSortString();
            AddSummationRow_New(ly_material_task_viewNewBindingSource, ly_material_task_viewNewDataGridView);
        }

        private void ly_material_task_viewNewDataGridView_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {

            if (null == ly_material_task_viewNewDataGridView.CurrentRow) return;
            string s = this.ly_material_task_viewNewDataGridView.CurrentRow.Cells["物料编码"].Value.ToString();
            string nowplannum = this.ly_material_task_viewNewDataGridView.CurrentRow.Cells["计划编码"].Value.ToString();


            //string nowxh = this.ly_inma0010DataGridView.CurrentRow.Cells["id"].Value.ToString();
            //int loanid = int.Parse(this.xD_Client_LoanDataGridView.CurrentRow.Cells["Id_loan"].Value.ToString());

            ly_task_detail queryForm = new ly_task_detail();

            //queryForm.statemode = "原料";
            //queryForm.runmode = "修改";
            queryForm.material_code = s;
            queryForm.nowplannum = nowplannum;
            queryForm.StartPosition = FormStartPosition.CenterParent;
            queryForm.ShowDialog();

           
        }
    }
}
