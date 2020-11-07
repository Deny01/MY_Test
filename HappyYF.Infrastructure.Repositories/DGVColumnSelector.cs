using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Xml;
using System.Data ;
using System.Xml.Serialization;
using System.IO;

namespace HappyYF.Infrastructure.Repositories
{
    //[XmlRoot("CheckedListBox_ser"), Serializable]
    //public class CheckedListBox_ser : CheckedListBox
    //{
    //  public   CheckedListBox_ser()
    //    {
    //        CheckedItems_ser = base.CheckedItems;
    //    }
    //  [XmlElement("CheckedItems_ser")]
    //  public CheckedItemCollection CheckedItems_ser;
    
    //}
    
    public  class DataGridViewColumnSelector
    {
        // the DataGridView to which the DataGridViewColumnSelector is attached
        private DataGridView mDataGridView = null;
        // a CheckedListBox containing the column header text and checkboxes
        private CheckedListBox mCheckedListBox;
        // a ToolStripDropDown object used to show the popup
        private ToolStripDropDown mPopup;

        private DataSet dgv_columnList = new DataSet() ;
        private string formName = "";

        private XmlSerializer xmlDataDef = new XmlSerializer(typeof(DataSet));

        /// <summary>
        /// The max height of the popup
        /// </summary>
        public int MaxHeight = 600;
        /// <summary>
        /// The width of the popup
        /// </summary>
        public int Width = 1200;

        /// <summary>
        /// Gets or sets the DataGridView to which the DataGridViewColumnSelector is attached
        /// </summary>
        public DataGridView DataGridView
        {
            get { return mDataGridView; }
            set
            {
                // If any, remove handler from current DataGridView 
                if (mDataGridView != null) mDataGridView.CellMouseClick -= new DataGridViewCellMouseEventHandler(mDataGridView_CellMouseClick);
                // Set the new DataGridView
                mDataGridView = value;
                // Attach CellMouseClick handler to DataGridView
                if (mDataGridView != null) mDataGridView.CellMouseClick += new DataGridViewCellMouseEventHandler(mDataGridView_CellMouseClick);

                //mPopup.Closing += new ToolStripDropDownClosingEventHandler(mPopup_Closing);
                mPopup.Closed += new ToolStripDropDownClosedEventHandler(mPopup_Closed);
            }
        }

        void mPopup_Closed(object sender, ToolStripDropDownClosedEventArgs e)
        {
            ColumnItems_Save();
        }

        void mPopup_Closing(object sender, ToolStripDropDownClosingEventArgs e)
        {
            //throw new NotImplementedException();
            //ColumnItems_Save();
        }

        // When user right-clicks the cell origin, it clears and fill the CheckedListBox with
        // columns header text. Then it shows the popup. 
        // In this way the CheckedListBox items are always refreshed to reflect changes occurred in 
        // DataGridView columns (column additions or name changes and so on).
        void mDataGridView_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            //if (e.Button == MouseButtons.Right && e.RowIndex == -1 && e.ColumnIndex == -1)
            if (e.Button == MouseButtons.Right && e.ColumnIndex == -1)
            {
                //ResetColumn_list();
                ColumnItems_Load();
                mPopup.Show(mDataGridView.PointToScreen(new Point(e.X, e.Y + mDataGridView.ColumnHeadersHeight)));
            }
        }

        private void ResetColumn_list()
        {
           
            DataRow row;
            mCheckedListBox.Items.Clear();
            dgv_columnList.Tables["dgvcolumns"].Rows.Clear();
            foreach (DataGridViewColumn c in mDataGridView.Columns)
            {
                mCheckedListBox.Items.Add(c.HeaderText, c.Visible);

                row = dgv_columnList.Tables["dgvcolumns"].NewRow();
                row["columnText"] = c.HeaderText;
                row["columnVisible"] = c.Visible;
                dgv_columnList.Tables["dgvcolumns"].Rows.Add(row);
            }
            int PreferredHeight = (mCheckedListBox.Items.Count * 9) + 7;
            mCheckedListBox.Height = (PreferredHeight < MaxHeight) ? PreferredHeight : MaxHeight;
            mCheckedListBox.Width = this.Width;
            mCheckedListBox.MultiColumn = true;

           
            //mPopup.Show(mDataGridView.PointToScreen(new Point(e.X, e.Y + mDataGridView.ColumnHeadersHeight)));
        }

        // The constructor creates an instance of CheckedListBox and ToolStripDropDown.
        // the CheckedListBox is hosted by ToolStripControlHost, which in turn is
        // added to ToolStripDropDown.
        public DataGridViewColumnSelector()
        {
            mCheckedListBox = new CheckedListBox();
            mCheckedListBox.CheckOnClick = true;
            mCheckedListBox.ItemCheck += new ItemCheckEventHandler(mCheckedListBox_ItemCheck);

            ToolStripControlHost mControlHost = new ToolStripControlHost(mCheckedListBox);
            mControlHost.Padding = Padding.Empty;
            mControlHost.Margin = Padding.Empty;
            mControlHost.AutoSize = false;

            mPopup = new ToolStripDropDown();
            mPopup.Padding = Padding.Empty;
            mPopup.Items.Add(mControlHost);
        }

        public DataGridViewColumnSelector(DataGridView dgv,string winformName)
            : this()
        {
            this.DataGridView = dgv;
            this.formName = winformName;
            MakeDataset();

        }

        // When user checks / unchecks a checkbox, the related column visibility is 
        // switched.
        void mCheckedListBox_ItemCheck(object sender, ItemCheckEventArgs e)
        {
          //CheckBox cb = mCheckedListBox .Items[e .Index] as CheckBox;
          if ( null != mDataGridView.Columns[mCheckedListBox.Items[e.Index].ToString ()])
            mDataGridView.Columns[mCheckedListBox.Items[e.Index].ToString ()].Visible = (e.NewValue == CheckState.Checked);


            if (null != dgv_columnList.Tables["dgvcolumns"].Rows.Find(mCheckedListBox.Items[e.Index].ToString()))
            dgv_columnList.Tables["dgvcolumns"].Rows.Find(mCheckedListBox.Items[e.Index].ToString())["columnVisible"] = (e.NewValue == CheckState.Checked);
           

        }
         void ColumnItems_Load()
        {
            XmlTextReader xmlReader = null;
          
            //Directory.GetCurrentDirectory
            try
            {
                if (!Directory.Exists(Directory.GetCurrentDirectory() + "\\dgvSet"))
                {
                    Directory.CreateDirectory(Directory.GetCurrentDirectory() + "\\dgvSet");
                }
                if (File.Exists(Directory.GetCurrentDirectory() + "\\dgvSet\\" + formName + mDataGridView.Name))
                {
                    xmlReader = new XmlTextReader(Directory.GetCurrentDirectory() + "\\dgvSet\\" + formName + mDataGridView.Name);
                    this.dgv_columnList = (DataSet )xmlDataDef.Deserialize(xmlReader);
                    xmlReader.Close();

                    mCheckedListBox.Items.Clear();
                    foreach (DataRow dr in dgv_columnList.Tables["dgvcolumns"].Rows )
                    {
                        mCheckedListBox.Items.Add( (string)dr["columnText"],(Boolean )dr["columnVisible"]);

                    
                    }
                    int PreferredHeight = (mCheckedListBox.Items.Count * 9) + 7;
                    mCheckedListBox.Height = (PreferredHeight < MaxHeight) ? PreferredHeight : MaxHeight;
                    mCheckedListBox.Width = this.Width;
                    mCheckedListBox.MultiColumn = true;
                }
                else
                {
                    ResetColumn_list();
                    ColumnItems_Save();
                }

                //UpdatePortalDefinitionProperties(pd.tabs, null);
            }
            catch (Exception e)
            {
                if (xmlReader != null)
                {
                    xmlReader.Close();
                }
                throw new Exception(e.Message, e);
            }


            
        
        }
        void ColumnItems_Save()
        {
            //if (File.Exists(Directory.GetCurrentDirectory() + "\\dgvSet\\" + mDataGridView.Parent.Name + mDataGridView.Name))
            //    return;
            
            XmlTextWriter xmlWriter = null;
            try
            {
                //DataRow row;
                //dgv_columnList.Tables["dgvcolumns"].Rows.Clear();
                //foreach (DataGridViewColumn c in mDataGridView.Columns)
                //{
                   

                //    row = dgv_columnList.Tables["dgvcolumns"].NewRow();
                //    row["columnText"] = c.HeaderText;
                //    row["columnVisible"] = c.Visible;
                //    dgv_columnList.Tables["dgvcolumns"].Rows.Add(row);
                //}

                xmlWriter = new XmlTextWriter(Directory.GetCurrentDirectory() + "\\dgvSet\\" + formName + mDataGridView.Name, System.Text.Encoding.UTF8);
                xmlWriter.Formatting = Formatting.Indented;
                xmlDataDef.Serialize(xmlWriter, this.dgv_columnList );
                xmlWriter.Close();
            }
            catch (Exception e)
            {
                if (xmlWriter != null)
                {
                    xmlWriter.Close();
                }
                throw new Exception(e.Message, e);
            }
        
        }

        private void MakeDataset()
        {
            // Create a new DataTable.
            System.Data.DataTable table = new DataTable("dgvcolumns");
            // Declare variables for DataColumn and DataRow objects.
            DataColumn column;
           // DataRow row;

            // Create new DataColumn, set DataType, 
            // ColumnName and add to DataTable.    
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            // System.Type.GetType("System.String ");
            column.ColumnName = "columnText";
            column.ReadOnly = true;
            column.Unique = true;
            // Add the Column to the DataColumnCollection.
            table.Columns.Add(column);

            // Create second column.
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.Boolean");
            column.ColumnName = "columnVisible";
            //column.AutoIncrement = false;
            //column.Caption = "ParentItem";
            //column.ReadOnly = false;
            //column.Unique = false;
            // Add the column to the table.
            table.Columns.Add(column);

            // Make the ID column the primary key column.
            DataColumn[] PrimaryKeyColumns = new DataColumn[1];
            PrimaryKeyColumns[0] = table.Columns["columnText"];
            table.PrimaryKey = PrimaryKeyColumns;

            //// Instantiate the DataSet variable.
            //dataSet = new DataSet();
            //// Add the new DataTable to the DataSet.
            dgv_columnList.Tables.Add(table);

            // Create three new DataRow objects and add 
            // them to the DataTable
            //for (int i = 0; i <= 2; i++)
            //{
            //    row = table.NewRow();
            //    row["id"] = i;
            //    row["ParentItem"] = "ParentItem " + i;
            //    table.Rows.Add(row);
            //}
        }
        public void Set_dgvColumns()
        {
            XmlTextReader xmlReader = null;
            if (File.Exists(Directory.GetCurrentDirectory() + "\\dgvSet\\" + formName + mDataGridView.Name))
            {
                xmlReader = new XmlTextReader(Directory.GetCurrentDirectory() + "\\dgvSet\\" + formName + mDataGridView.Name);
                this.dgv_columnList = (DataSet)xmlDataDef.Deserialize(xmlReader);
                xmlReader.Close();

                foreach (DataRow dr in dgv_columnList.Tables["dgvcolumns"].Rows)
                {
                    //mCheckedListBox.Items.Add((string)dr["columnText"], (Boolean)dr["columnVisible"]);

                    if (null !=mDataGridView.Columns[(string)dr["columnText"]])
                    mDataGridView.Columns[(string)dr["columnText"]].Visible = (Boolean)dr["columnVisible"];


                }
            }
            
           
        
        }

    }
}
