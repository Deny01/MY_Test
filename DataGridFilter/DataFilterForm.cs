using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

namespace DataGridFilter
{
	/// <summary>
	/// Summary description for DataFilter.
	/// </summary>
	public class DataFilterForm : System.Windows.Forms.Form
	{

		private DataTable FilterDataTable;
		private DataTable OperationDataTable;
        private DataTable ClumnDataTable;
        private string FilterString;

        DataRow FilterRow;
		
		// Add new oprrations according to the operation which support by the database you connect
		private string [] Operations = {"<","<=",">",">=","=","<>","LIKE"};

		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.DataGrid DataGridFilterData;
		private System.Windows.Forms.Button ButtonSelect;
		private System.Windows.Forms.Panel panel2;
        private Button button1;
        private DataSet dataSet1;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public DataFilterForm()
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
				if(components != null)
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.DataGridFilterData = new System.Windows.Forms.DataGrid();
            this.ButtonSelect = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.dataSet1 = new System.Data.DataSet();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DataGridFilterData)).BeginInit();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataSet1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.DataGridFilterData);
            this.panel1.Location = new System.Drawing.Point(8, 9);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(390, 189);
            this.panel1.TabIndex = 0;
            // 
            // DataGridFilterData
            // 
            this.DataGridFilterData.DataMember = "";
            this.DataGridFilterData.HeaderForeColor = System.Drawing.SystemColors.ControlText;
            this.DataGridFilterData.Location = new System.Drawing.Point(8, 9);
            this.DataGridFilterData.Name = "DataGridFilterData";
            this.DataGridFilterData.Size = new System.Drawing.Size(372, 177);
            this.DataGridFilterData.TabIndex = 0;
            // 
            // ButtonSelect
            // 
            this.ButtonSelect.BackColor = System.Drawing.Color.LightSteelBlue;
            this.ButtonSelect.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.ButtonSelect.Location = new System.Drawing.Point(269, 3);
            this.ButtonSelect.Name = "ButtonSelect";
            this.ButtonSelect.Size = new System.Drawing.Size(96, 26);
            this.ButtonSelect.TabIndex = 1;
            this.ButtonSelect.Text = "Select";
            this.ButtonSelect.UseVisualStyleBackColor = false;
            this.ButtonSelect.Click += new System.EventHandler(this.ButtonSelect_Click);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Gainsboro;
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.button1);
            this.panel2.Controls.Add(this.ButtonSelect);
            this.panel2.Location = new System.Drawing.Point(8, 204);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(390, 36);
            this.panel2.TabIndex = 23;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(174, 6);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 24;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // dataSet1
            // 
            this.dataSet1.DataSetName = "NewDataSet";
            // 
            // DataFilterForm
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 15);
            this.ClientSize = new System.Drawing.Size(410, 244);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "DataFilterForm";
            this.Text = "查詢條件";
            this.Load += new System.EventHandler(this.DataFilterForm_Load);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DataGridFilterData)).EndInit();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataSet1)).EndInit();
            this.ResumeLayout(false);

		}
		#endregion

		private void DataFilterForm_Load(object sender, System.EventArgs e)
		{
			DataGridTableStyle    GridStyle = new DataGridTableStyle();
			
			
			GridStyle.MappingName = "FilterData" ;




            //DataGridColumnStyle  ColumnName= new DataGridTextBoxColumn();
            //ColumnName.HeaderText="Column Name";
            //ColumnName.MappingName="ColumnName";
            //ColumnName.Width=150;
            //ColumnName.ReadOnly = true;
            //GridStyle.GridColumnStyles.Add(ColumnName);
            //dataGridView1
            DataGridColumnStyle ColumnName = new DataGridComboBoxColumn("ColumnName", ClumnDataTable, "ClumnMingcheng", "ClumnMingcheng", DataGridFilterData);
            //DataGridColumnStyle ColumnName = new DataGridComboBoxColumn("ColumnName", ClumnDataTable, "ClumnMingcheng", "ClumnMingcheng", dataGridView1);

            ColumnName.Width = 95;
            ColumnName.HeaderText = "字段";
            ColumnName.NullText = string.Empty;
            GridStyle.GridColumnStyles.Add(ColumnName);

			
			DataGridColumnStyle  ColumnOperation= new DataGridComboBoxColumn("Operation", OperationDataTable, "ColumnOperation", "ColumnOperation", DataGridFilterData);
           // DataGridColumnStyle ColumnOperation = new DataGridComboBoxColumn("Operation", OperationDataTable, "ColumnOperation", "ColumnOperation", dataGridView1);

			
			ColumnOperation.Width=51;
            ColumnOperation.HeaderText = "關系";
			ColumnOperation.NullText = string.Empty;
			GridStyle.GridColumnStyles.Add(ColumnOperation);

			DataGridColumnStyle  ColumnData= new DataGridTextBoxColumn();
			ColumnData.HeaderText="數值";
			ColumnData.MappingName="ColumnData";
			ColumnData.NullText= string.Empty;
			ColumnData.Width=95;
			GridStyle.GridColumnStyles.Add(ColumnData);

            DataGridColumnStyle ColumnLogic = new DataGridTextBoxColumn();
            ColumnLogic.HeaderText = "邏輯";
            ColumnLogic.MappingName = "Logic";
            ColumnLogic.NullText = string.Empty;
            ColumnLogic.Width = 51;
            GridStyle.GridColumnStyles.Add(ColumnLogic);

			
			GridStyle.AlternatingBackColor=System.Drawing.Color.AliceBlue ;
			GridStyle.GridLineColor = System.Drawing.Color.MediumSlateBlue;
				
			DataGridFilterData.TableStyles.Add(GridStyle);
            //dataGridView1.TableStyles.Add(GridStyle);
            //dataGridView1

			DataGridFilterData.DataSource = FilterDataTable;
            //dataGridView1.DataSource = FilterDataTable;
			
		}


		public void SetSourceColumns (DataColumnCollection Columns)
		{

			//DataRow FilterRow;
			DataRow OperationRow;
            DataRow ColumnRow;
			try
			{

				OperationDataTable =new DataTable("OperationDataTable");
				DataColumn CloumnOperation = new DataColumn("ColumnOperation",System.Type.GetType("System.String"));
				OperationDataTable.Columns.Add(CloumnOperation);
			
                 

				foreach (string Oper in Operations)
				{
                  OperationRow = OperationDataTable.NewRow();
					OperationRow["ColumnOperation"] = Oper;
				  OperationDataTable.Rows.Add(OperationRow);
				}

                ClumnDataTable = new DataTable("ClumnDataTable");
                DataColumn ClumnMingcheng = new DataColumn("ClumnMingcheng", System.Type.GetType("System.String"));
                ClumnDataTable.Columns.Add(ClumnMingcheng);

                foreach (DataColumn col in Columns)
                {
                    ColumnRow = ClumnDataTable.NewRow();
                    ColumnRow["ClumnMingcheng"] = col.ColumnName.ToString();
                    ClumnDataTable.Rows.Add(ColumnRow);
                }
				 

				FilterDataTable = new DataTable("FilterData");
			
				DataColumn ColumnName = new DataColumn("ColumnName",System.Type.GetType("System.String"));
				DataColumn ColumnOperation = new DataColumn("Operation",System.Type.GetType("System.String"));
				DataColumn ColumnFilterData = new DataColumn("ColumnData",System.Type.GetType("System.String"));
                DataColumn ColumnLogic = new DataColumn("Logic", System.Type.GetType("System.String"));
				
				FilterDataTable.Columns.Add(ColumnName);
				FilterDataTable.Columns.Add(ColumnOperation);				
				FilterDataTable.Columns.Add(ColumnFilterData);
                FilterDataTable.Columns.Add(ColumnLogic);
				

                //foreach (DataColumn col in Columns)
                //{
                   FilterRow = FilterDataTable.NewRow();
                //    FilterRow["ColumnName"] = col.ColumnName.ToString();
                  FilterDataTable.Rows.Add(FilterRow);

                //}

				
			}

			catch (System.Exception a_Ex)
			{
				MessageBox.Show(a_Ex.Message);
			}
		}

		
        
        public DataTable GetFilterDataTable()
		{
			return FilterDataTable;
		}
        private void SetFilterString()
        {
            

            try
            {
                // Build the RowFilter statement according to the user restriction 
                foreach (DataRow FilRow in FilterDataTable.Rows)
                {

                    if (FilterRow["Operation"].ToString() != string.Empty && FilterRow["ColumnData"].ToString() != string.Empty)
                    {
                        // Add the "AND" operator only from the second filter condition 
                        // The RowFilter get statement which simallar to the Where condition in sql query
                        // For example "GroupID = '6' AND GroupName LIKE 'A%' 
                        if (this.FilterString == string.Empty)
                        {
                            this.FilterString = FilRow["ColumnName"].ToString() + " " + FilRow["Operation"].ToString() + " '" + FilRow["ColumnData"].ToString() + "' " + FilRow["Logic"].ToString() + " ";
                        }

                        else
                        {
                            this.FilterString += FilRow["ColumnName"].ToString() + " " + FilRow["Operation"].ToString() + " '" + FilRow["ColumnData"].ToString() + "' " + FilRow["Logic"].ToString() + " ";
                        }

                    }
                }


                
            }


            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public String GetFilterString()
        {
            return FilterString;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //DataRow FilterRow;
            FilterRow = FilterDataTable.NewRow();
            //FilterRow["ColumnName"] = col.ColumnName.ToString();
            FilterDataTable.Rows.Add(FilterRow);
        }

        private void ButtonSelect_Click(object sender, EventArgs e)
        {
            this.SetFilterString();
            //MessageBox.Show("hehe");
        }
	}
}
