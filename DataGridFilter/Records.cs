using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Collections.Generic;

namespace DataGridFilter
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class FormRecords : System.Windows.Forms.Form
	{

		private System.Windows.Forms.Panel panelrecords;
		private System.Windows.Forms.DataGrid DataGridRecords;
		private DataColumnCollection  TableColumnCollection;
		private DataTable  TableFilterData;

		private DataSet DataSetRecords;
		private DataView ViewRecords;
		private CheckedListBox CheckedColumns;
		private System.Windows.Forms.Button ButtonDataFilter;
        private System.Windows.Forms.Button ButtonCoulmnFilter;
        private HuochangpandianDataSet huochangpandianDataSet;
        private BindingSource t_stoc_stocktakBindingSource;
        private DataGridFilter.HuochangpandianDataSetTableAdapters.t_stoc_stocktakTableAdapter t_stoc_stocktakTableAdapter;
        private Button button1;
        private DataGridView t_stoc_stocktakDataGridView;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn8;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn9;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn10;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn11;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn12;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn13;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn14;
        private Button button2;
        private IContainer components;

		public FormRecords()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            this.panelrecords = new System.Windows.Forms.Panel();
            this.DataGridRecords = new System.Windows.Forms.DataGrid();
            this.ButtonDataFilter = new System.Windows.Forms.Button();
            this.ButtonCoulmnFilter = new System.Windows.Forms.Button();
            this.huochangpandianDataSet = new DataGridFilter.HuochangpandianDataSet();
            this.t_stoc_stocktakBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.t_stoc_stocktakTableAdapter = new DataGridFilter.HuochangpandianDataSetTableAdapters.t_stoc_stocktakTableAdapter();
            this.button1 = new System.Windows.Forms.Button();
            this.t_stoc_stocktakDataGridView = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn10 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn11 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn12 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn13 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn14 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.button2 = new System.Windows.Forms.Button();
            this.panelrecords.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DataGridRecords)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.huochangpandianDataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.t_stoc_stocktakBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.t_stoc_stocktakDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // panelrecords
            // 
            this.panelrecords.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panelrecords.Controls.Add(this.DataGridRecords);
            this.panelrecords.Location = new System.Drawing.Point(12, 256);
            this.panelrecords.Name = "panelrecords";
            this.panelrecords.Padding = new System.Windows.Forms.Padding(10);
            this.panelrecords.Size = new System.Drawing.Size(576, 138);
            this.panelrecords.TabIndex = 0;
            // 
            // DataGridRecords
            // 
            this.DataGridRecords.DataMember = "";
            this.DataGridRecords.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DataGridRecords.HeaderForeColor = System.Drawing.SystemColors.ControlText;
            this.DataGridRecords.Location = new System.Drawing.Point(10, 10);
            this.DataGridRecords.Name = "DataGridRecords";
            this.DataGridRecords.Size = new System.Drawing.Size(552, 114);
            this.DataGridRecords.TabIndex = 0;
            // 
            // ButtonDataFilter
            // 
            this.ButtonDataFilter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.ButtonDataFilter.Location = new System.Drawing.Point(559, 424);
            this.ButtonDataFilter.Name = "ButtonDataFilter";
            this.ButtonDataFilter.Size = new System.Drawing.Size(88, 27);
            this.ButtonDataFilter.TabIndex = 1;
            this.ButtonDataFilter.Text = "Data Filter ";
            this.ButtonDataFilter.Click += new System.EventHandler(this.ButtonDataFilter_Click);
            // 
            // ButtonCoulmnFilter
            // 
            this.ButtonCoulmnFilter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.ButtonCoulmnFilter.Location = new System.Drawing.Point(448, 424);
            this.ButtonCoulmnFilter.Name = "ButtonCoulmnFilter";
            this.ButtonCoulmnFilter.Size = new System.Drawing.Size(88, 26);
            this.ButtonCoulmnFilter.TabIndex = 2;
            this.ButtonCoulmnFilter.Text = "Coulmn Filter";
            this.ButtonCoulmnFilter.Click += new System.EventHandler(this.ButtonCoulmnFilter_Click);
            // 
            // huochangpandianDataSet
            // 
            this.huochangpandianDataSet.DataSetName = "HuochangpandianDataSet";
            this.huochangpandianDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // t_stoc_stocktakBindingSource
            // 
            this.t_stoc_stocktakBindingSource.DataMember = "t_stoc_stocktak";
            this.t_stoc_stocktakBindingSource.DataSource = this.huochangpandianDataSet;
            // 
            // t_stoc_stocktakTableAdapter
            // 
            this.t_stoc_stocktakTableAdapter.ClearBeforeFill = true;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(671, 399);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 3;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // t_stoc_stocktakDataGridView
            // 
            this.t_stoc_stocktakDataGridView.AutoGenerateColumns = false;
            this.t_stoc_stocktakDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2,
            this.dataGridViewTextBoxColumn3,
            this.dataGridViewTextBoxColumn4,
            this.dataGridViewTextBoxColumn5,
            this.dataGridViewTextBoxColumn6,
            this.dataGridViewTextBoxColumn7,
            this.dataGridViewTextBoxColumn8,
            this.dataGridViewTextBoxColumn9,
            this.dataGridViewTextBoxColumn10,
            this.dataGridViewTextBoxColumn11,
            this.dataGridViewTextBoxColumn12,
            this.dataGridViewTextBoxColumn13,
            this.dataGridViewTextBoxColumn14});
            this.t_stoc_stocktakDataGridView.DataSource = this.t_stoc_stocktakBindingSource;
            this.t_stoc_stocktakDataGridView.Location = new System.Drawing.Point(12, 21);
            this.t_stoc_stocktakDataGridView.Name = "t_stoc_stocktakDataGridView";
            this.t_stoc_stocktakDataGridView.RowTemplate.Height = 24;
            this.t_stoc_stocktakDataGridView.Size = new System.Drawing.Size(780, 220);
            this.t_stoc_stocktakDataGridView.TabIndex = 4;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.DataPropertyName = "id_num";
            this.dataGridViewTextBoxColumn1.HeaderText = "id_num";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.DataPropertyName = "年份";
            this.dataGridViewTextBoxColumn2.HeaderText = "年份";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.DataPropertyName = "廠別";
            this.dataGridViewTextBoxColumn3.HeaderText = "廠別";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.DataPropertyName = "盤點票號";
            this.dataGridViewTextBoxColumn4.HeaderText = "盤點票號";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.DataPropertyName = "父件";
            this.dataGridViewTextBoxColumn5.HeaderText = "父件";
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            // 
            // dataGridViewTextBoxColumn6
            // 
            this.dataGridViewTextBoxColumn6.DataPropertyName = "parent_desc";
            this.dataGridViewTextBoxColumn6.HeaderText = "parent_desc";
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            // 
            // dataGridViewTextBoxColumn7
            // 
            this.dataGridViewTextBoxColumn7.DataPropertyName = "item_code";
            this.dataGridViewTextBoxColumn7.HeaderText = "item_code";
            this.dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
            // 
            // dataGridViewTextBoxColumn8
            // 
            this.dataGridViewTextBoxColumn8.DataPropertyName = "short_desc";
            this.dataGridViewTextBoxColumn8.HeaderText = "short_desc";
            this.dataGridViewTextBoxColumn8.Name = "dataGridViewTextBoxColumn8";
            // 
            // dataGridViewTextBoxColumn9
            // 
            this.dataGridViewTextBoxColumn9.DataPropertyName = "checkqty";
            this.dataGridViewTextBoxColumn9.HeaderText = "checkqty";
            this.dataGridViewTextBoxColumn9.Name = "dataGridViewTextBoxColumn9";
            // 
            // dataGridViewTextBoxColumn10
            // 
            this.dataGridViewTextBoxColumn10.DataPropertyName = "reviseqty";
            this.dataGridViewTextBoxColumn10.HeaderText = "reviseqty";
            this.dataGridViewTextBoxColumn10.Name = "dataGridViewTextBoxColumn10";
            // 
            // dataGridViewTextBoxColumn11
            // 
            this.dataGridViewTextBoxColumn11.DataPropertyName = "unit";
            this.dataGridViewTextBoxColumn11.HeaderText = "unit";
            this.dataGridViewTextBoxColumn11.Name = "dataGridViewTextBoxColumn11";
            // 
            // dataGridViewTextBoxColumn12
            // 
            this.dataGridViewTextBoxColumn12.DataPropertyName = "warehouse";
            this.dataGridViewTextBoxColumn12.HeaderText = "warehouse";
            this.dataGridViewTextBoxColumn12.Name = "dataGridViewTextBoxColumn12";
            // 
            // dataGridViewTextBoxColumn13
            // 
            this.dataGridViewTextBoxColumn13.DataPropertyName = "locate";
            this.dataGridViewTextBoxColumn13.HeaderText = "locate";
            this.dataGridViewTextBoxColumn13.Name = "dataGridViewTextBoxColumn13";
            // 
            // dataGridViewTextBoxColumn14
            // 
            this.dataGridViewTextBoxColumn14.DataPropertyName = "remark";
            this.dataGridViewTextBoxColumn14.HeaderText = "remark";
            this.dataGridViewTextBoxColumn14.Name = "dataGridViewTextBoxColumn14";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(671, 358);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 5;
            this.button2.Text = "button2";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // FormRecords
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 15);
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(804, 478);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.t_stoc_stocktakDataGridView);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.ButtonCoulmnFilter);
            this.Controls.Add(this.ButtonDataFilter);
            this.Controls.Add(this.panelrecords);
            this.Name = "FormRecords";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.FormRecords_Load);
            this.panelrecords.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DataGridRecords)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.huochangpandianDataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.t_stoc_stocktakBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.t_stoc_stocktakDataGridView)).EndInit();
            this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new FormRecords());
		}



		private void FormRecords_Load(object sender, System.EventArgs e)
		{
            // TODO: This line of code loads data into the 'huochangpandianDataSet.t_stoc_stocktak' table. You can move, or remove it, as needed.
            this.t_stoc_stocktakTableAdapter.Connection.ConnectionString = "Data Source=DENY;Initial Catalog=new_chy;User ID=sa;password=deny01;Connect Timeout=1200";
            this.t_stoc_stocktakTableAdapter.Fill(this.huochangpandianDataSet.t_stoc_stocktak);
			try
			{
				//Fill the DataSet from XML file OR from any other source 
              
				DataSetRecords = new DataSet("DataSetRecords");
				
				//Make sure That Products.xml file located in the "exe" folder
				DataSetRecords.ReadXml("Products.xml", XmlReadMode.Auto );
				
				//Get the Table Column Collection
				TableColumnCollection = DataSetRecords.Tables[0].Columns;
				
				ViewRecords = DataSetRecords.Tables[0].DefaultView;
				
				DataGridTableStyle    GridStyle = new DataGridTableStyle();
				GridStyle.MappingName = DataSetRecords.Tables[0].TableName ;
				GridStyle.AlternatingBackColor=System.Drawing.Color.AliceBlue ;
				GridStyle.GridLineColor = System.Drawing.Color.MediumSlateBlue;
				
				DataGridRecords.TableStyles.Add(GridStyle);
				DataGridRecords.SetDataBinding(ViewRecords,"");
				
			}
			catch (System.Exception a_Ex)
			{
				MessageBox.Show(a_Ex.Message);
			}
		}

		private void ButtonCoulmnFilter_Click(object sender, System.EventArgs e)
		{

			ColumnFilterForm FilterForm = new ColumnFilterForm();
           
			//Set the Column Collection to the checklist object
			FilterForm.SetSourceColumns(TableColumnCollection);
			
			FilterForm.ShowDialog();

            //Get the column check list
			CheckedColumns= FilterForm.GetSelectedColumns();

			//Set The mapping Data Grid Table Style according to the selected coulmns;
			SetTableBySelectedCoulmns(DataSetRecords.Tables[0].TableName);
			
			DataGridRecords.SetDataBinding(ViewRecords,null);
		
		}

		private void ButtonDataFilter_Click(object sender, System.EventArgs e)
		{
			DataFilterForm DataFilter = new DataFilterForm();
           
			//Set the Column Collection to the filter Table
			DataFilter.SetSourceColumns(TableColumnCollection);
			
			DataFilter.ShowDialog();

			//The TableFilterData Table contains the user restriction
			TableFilterData = DataFilter.GetFilterDataTable();

			SetTableByDataFilter();

		}


		private void SetTableBySelectedCoulmns(string TableMappingName)
		{
			
			
			DataGridTableStyle    GridStyle = new DataGridTableStyle();
			
			DataGridColumnStyle   TextBoxStyle; //Use for System.Boolean only
			DataGridColumnStyle   BoolStyle; //Use for all Data Types which different form System.Boolean
			
			string CoulmnDataType;  // hold the column Data Type 
			
			try
			{
				//clear the previous Table Styles
				DataGridRecords.TableStyles.Clear();
				GridStyle.MappingName = TableMappingName;

				foreach (DataColumn Column in TableColumnCollection )
				{
					CoulmnDataType = Column.DataType.ToString();

                    // The "CheckedColumns" contains the column which the user select to show 
					// Column that not belong to the mapping will not show on the grid
                    
					if (CheckedColumns.CheckedItems.Contains(Column.ColumnName))
					{
						switch (CoulmnDataType)
						{
							// The DataGrid Coulmn Style support two major column types: Bool and Text 
							case ("System.Boolean"):
							{
								BoolStyle = new DataGridBoolColumn ();
								BoolStyle.HeaderText=Column.ColumnName;
								BoolStyle.MappingName=Column.ColumnName;
								BoolStyle.Width=100 ;
								GridStyle.GridColumnStyles.Add(BoolStyle);
							}

							break;

							default:
							{
								TextBoxStyle= new DataGridTextBoxColumn();
								TextBoxStyle.HeaderText=Column.ColumnName;
								TextBoxStyle.MappingName=Column.ColumnName;
								TextBoxStyle.Width=100;
								// The NUllText attribute enable to replace the default null value for empty cells 
								TextBoxStyle.NullText = string.Empty ;
								GridStyle.GridColumnStyles.Add(TextBoxStyle);
							}
							break;
						}
                  
					}
				}
					
				//Set the Grid Style & Color
				GridStyle.AlternatingBackColor=System.Drawing.Color.AliceBlue ;
				GridStyle.GridLineColor = System.Drawing.Color.MediumSlateBlue;
				DataGridRecords.TableStyles.Add(GridStyle);

			}
			catch (System.Exception a_Ex)
			{
				MessageBox.Show(a_Ex.Message);
			}

		}

		
		private void SetTableByDataFilter()
		{
			
         
			ViewRecords =  new DataView( DataSetRecords.Tables[0]);
            
			try
			{
				// Build the RowFilter statement according to the user restriction 
				foreach (DataRow FilterRow in TableFilterData.Rows)
				{
				
					if (FilterRow["Operation"].ToString() != string.Empty && FilterRow["ColumnData"].ToString() != string.Empty)
					{
						// Add the "AND" operator only from the second filter condition 
						// The RowFilter get statement which simallar to the Where condition in sql query
						// For example "GroupID = '6' AND GroupName LIKE 'A%' 
						if (ViewRecords.RowFilter == string.Empty)
						{
							ViewRecords.RowFilter = FilterRow["ColumnName"].ToString() + " " + FilterRow["Operation"].ToString() + " '" + FilterRow["ColumnData"].ToString()+"' ";
						}
					
						else
						{
							ViewRecords.RowFilter += " AND " + FilterRow["ColumnName"].ToString()+" " + FilterRow["Operation"].ToString() +" '"+ FilterRow["ColumnData"].ToString()+"'";
						}
					
					}
				}

				
				DataGridRecords.SetDataBinding(ViewRecords,"");
			}
			

			catch (System.Exception ex)
			{
              MessageBox.Show(ex.Message);
			}

		}

        private void t_stoc_stocktakBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.t_stoc_stocktakBindingSource.EndEdit();
            this.t_stoc_stocktakTableAdapter.Update(this.huochangpandianDataSet.t_stoc_stocktak);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            DataFilterForm DataFilter = new DataFilterForm();

            //Set the Column Collection to the filter Table
            DataFilter.SetSourceColumns(this.huochangpandianDataSet.t_stoc_stocktak.Columns);

            DataFilter.ShowDialog();

            //The TableFilterData Table contains the user restriction
            //TableFilterData = DataFilter.GetFilterDataTable();
            this.t_stoc_stocktakBindingSource.Filter = DataFilter.GetFilterString();

            //SetTableByDataFilter();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FilterForm filterForm = new FilterForm();
            List<string> ls = new List<string> ();
            //Set the Column Collection to the filter Table
            filterForm.SetSourceColumns(this.huochangpandianDataSet.t_stoc_stocktak.Columns,ls);

            filterForm.ShowDialog();

            //The TableFilterData Table contains the user restriction
            //TableFilterData = DataFilter.GetFilterDataTable();
            //this.t_stoc_stocktakBindingSource.Filter = DataFilter.GetFilterString();

        }

		
	}
}
