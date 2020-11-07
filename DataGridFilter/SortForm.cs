using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DataGridFilter
{
    public partial class SortForm : Form
    {

        private string SortString;

        private void SetSortString()
        {
            
            this.sortTableBindingSource.EndEdit();
            try
            {
                // Build the RowFilter statement according to the user restriction 
                for (int i = 0; i < this.sortTableDataGridView.Rows.Count ; i++)

                {

                    
                        // Add the "AND" operator only from the second filter condition 
                        // The RowFilter get statement which simallar to the Where condition in sql query
                        // For example "GroupID = '6' AND GroupName LIKE 'A%' 
                        if (this.SortString == string.Empty)
                        {
                            this.SortString = this.sortTableDataGridView.Rows[i].Cells[0].Value.ToString() + " " + this.sortTableDataGridView.Rows[i].Cells[1].Value.ToString() + " ,";
                        }

                        else
                        {
                            this.SortString += this.sortTableDataGridView.Rows[i].Cells[0].Value.ToString() + " " + this.sortTableDataGridView.Rows[i].Cells[1].Value.ToString() + " ,";
                        }

                    
                }



            }


            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public String GetSortString()
        {
            this.SetSortString();
            if (null != this.SortString)
            {
            this.SortString = this.SortString.Substring(0, this.SortString.Length - 1);
            }
            return SortString;
        }
        public void SetSortColumns(DataColumnCollection Columns, List<string> ls)
        {

            DataRow SortRow;

            try
            {




                foreach (DataColumn col in Columns)
                {

                    if (!ls.Contains(col.ColumnName.ToString()))
                    {
                        SortRow = this.filterDataSet.ColumnNameTable.NewRow();
                        SortRow["ColumnName"] = col.ColumnName.ToString();
                        this.filterDataSet.ColumnNameTable.Rows.Add(SortRow);
                    }

                }


            }


            catch (System.Exception a_Ex)
            {
                MessageBox.Show(a_Ex.Message);
            }
        }
        public void SetSortColumns(DataGridViewColumnCollection Columns, List<string> ls)
        {

            DataRow SortRow;

            try
            {




                foreach (DataGridViewColumn col in Columns)
                {

                    if (!ls.Contains(col.DataPropertyName.ToString()))
                    {
                        if (col.Visible == true)
                        {
                            SortRow = this.filterDataSet.ColumnNameTable.NewRow();
                            SortRow["ColumnName"] = col.DataPropertyName.ToString();
                            this.filterDataSet.ColumnNameTable.Rows.Add(SortRow);
                        }
                    }

                }


            }


            catch (System.Exception a_Ex)
            {
                MessageBox.Show(a_Ex.Message);
            }
        }
        public SortForm()
        {
            InitializeComponent();
        }

        private void columnNameTableDataGridView_DoubleClick(object sender, EventArgs e)
        {
            if (null != this.columnNameTableBindingSource.Current)
            {
                this.sortTableBindingSource.AddNew();
                this.sortTableDataGridView.CurrentRow.Cells[0].Value = this.columnNameTableDataGridView.CurrentRow.Cells[0].Value;
                this.columnNameTableBindingSource.RemoveCurrent();
                //this.columnNameTableBindingSource.Current[0];
            }
        }

        private void sortTableDataGridView_DoubleClick(object sender, EventArgs e)
        {
            if (null != this.sortTableBindingSource.Current)
            {
                this.columnNameTableBindingSource.AddNew();
                this.columnNameTableDataGridView.CurrentRow.Cells[0].Value = this.sortTableDataGridView.CurrentRow.Cells[0].Value;
                this.sortTableBindingSource.RemoveCurrent();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}