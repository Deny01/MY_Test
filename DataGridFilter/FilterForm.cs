using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace DataGridFilter
{
    public partial class FilterForm : Form
    {
        private string FilterString;

        private void SetFilterString()
        {

            this.filterTableBindingSource.EndEdit();
            try
            {
                // Build the RowFilter statement according to the user restriction 
                foreach (DataRow FilRow in this.filterDataSet.FilterTable.Rows)
                {

                    if (FilRow["字段"].ToString() != string.Empty && FilRow["关系"].ToString() != string.Empty && FilRow["数值"].ToString() != string.Empty)
                    {
                        // Add the "AND" operator only from the second filter condition 
                        // The RowFilter get statement which simallar to the Where condition in sql query
                        // For example "GroupID = '6' AND GroupName LIKE 'A%' 
                        if (this.FilterString == string.Empty)
                        {
                            this.FilterString = FilRow["字段"].ToString() + " " + FilRow["关系"].ToString() + " '" + FilRow["数值"].ToString() + "' " + FilRow["逻辑"].ToString() + " ";
                        }

                        else
                        {
                            this.FilterString += FilRow["字段"].ToString() + " " + FilRow["关系"].ToString() + " '" + FilRow["数值"].ToString() + "' " + FilRow["逻辑"].ToString() + " ";
                        }

                    }
                }



            }


            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public bool GetCheck()
        {

            return this.checkBox1.Checked;
        }
        public String GetFilterString()
        {
            this.SetFilterString();
            return FilterString;
        }


        public FilterForm()
        {
            InitializeComponent();
        }

        private void filterTableBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {

        }
        public void SetSourceColumns(DataColumnCollection Columns, List<string> ls)
        {

            DataRow FilterRow;

            try
            {




                foreach (DataColumn col in Columns)
                {
                    if (!ls.Contains(col.ColumnName.ToString()))
                    {
                        FilterRow = this.filterDataSet.ColumnNameTable.NewRow();
                        FilterRow["ColumnName"] = col.ColumnName.ToString();
                        this.filterDataSet.ColumnNameTable.Rows.Add(FilterRow);
                    }

                }


            }

            catch (System.Exception a_Ex)
            {
                MessageBox.Show(a_Ex.Message);
            }
        }

        public void SetSourceColumns(DataGridViewColumnCollection  Columns, List<string> ls)
        {

            DataRow FilterRow;

            try
            {




                foreach (DataGridViewColumn  col in Columns)
                {
                    if (!ls.Contains(col.DataPropertyName.ToString()))
                    {
                        if (col.Visible == true)
                        {
                            FilterRow = this.filterDataSet.ColumnNameTable.NewRow();
                            FilterRow["ColumnName"] = col.DataPropertyName.ToString();
                            this.filterDataSet.ColumnNameTable.Rows.Add(FilterRow);
                        }
                    }

                }


            }

            catch (System.Exception a_Ex)
            {
                MessageBox.Show(a_Ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void bindingNavigatorAddNewItem_Click(object sender, EventArgs e)
        {

        }
    }
}