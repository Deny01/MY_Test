using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;




namespace DataGridFilter
{
	/// <summary>
	/// Summary description for ColumnFilterForm.
	/// </summary>
	public class ColumnFilterForm : System.Windows.Forms.Form
	{
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.CheckedListBox ClbShowColumn;
		private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button buttonSelect;
        private Panel panel2;
        private firstCrystalReport firstCrystalReport1;
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
        private FilterDataSet filterDataSet;
        private BindingSource filterDataSetBindingSource;
        private CrystalDecisions.Windows.Forms.CrystalReportViewer crystalReportViewer1;
        private HuochangpandianDataSet huochangpandianDataSet1;
        private IContainer components;

		public ColumnFilterForm()
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
            this.components = new System.ComponentModel.Container();
            this.t_stoc_stocktakBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.huochangpandianDataSet = new DataGridFilter.HuochangpandianDataSet();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.ClbShowColumn = new System.Windows.Forms.CheckedListBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.buttonSelect = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.crystalReportViewer1 = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            this.firstCrystalReport1 = new DataGridFilter.firstCrystalReport();
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
            this.filterDataSet = new DataGridFilter.FilterDataSet();
            this.filterDataSetBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.huochangpandianDataSet1 = new DataGridFilter.HuochangpandianDataSet();
            ((System.ComponentModel.ISupportInitialize)(this.t_stoc_stocktakBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.huochangpandianDataSet)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.t_stoc_stocktakDataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.filterDataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.filterDataSetBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.huochangpandianDataSet1)).BeginInit();
            this.SuspendLayout();
            // 
            // t_stoc_stocktakBindingSource
            // 
            this.t_stoc_stocktakBindingSource.DataMember = "t_stoc_stocktak";
            this.t_stoc_stocktakBindingSource.DataSource = this.huochangpandianDataSet;
            // 
            // huochangpandianDataSet
            // 
            this.huochangpandianDataSet.DataSetName = "HuochangpandianDataSet";
            this.huochangpandianDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.ClbShowColumn);
            this.groupBox1.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.groupBox1.Location = new System.Drawing.Point(8, 9);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(184, 443);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Show Column";
            // 
            // ClbShowColumn
            // 
            this.ClbShowColumn.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.ClbShowColumn.Location = new System.Drawing.Point(8, 18);
            this.ClbShowColumn.Name = "ClbShowColumn";
            this.ClbShowColumn.Size = new System.Drawing.Size(168, 404);
            this.ClbShowColumn.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Gainsboro;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.buttonSelect);
            this.panel1.Location = new System.Drawing.Point(8, 462);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(184, 36);
            this.panel1.TabIndex = 22;
            // 
            // buttonSelect
            // 
            this.buttonSelect.BackColor = System.Drawing.Color.LightSteelBlue;
            this.buttonSelect.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonSelect.Location = new System.Drawing.Point(104, 5);
            this.buttonSelect.Name = "buttonSelect";
            this.buttonSelect.Size = new System.Drawing.Size(72, 26);
            this.buttonSelect.TabIndex = 0;
            this.buttonSelect.Text = "Select";
            this.buttonSelect.UseVisualStyleBackColor = false;
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel2.Controls.Add(this.crystalReportViewer1);
            this.panel2.Location = new System.Drawing.Point(208, 12);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(423, 327);
            this.panel2.TabIndex = 23;
            // 
            // crystalReportViewer1
            // 
            this.crystalReportViewer1.ActiveViewIndex = 0;
            this.crystalReportViewer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.crystalReportViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.crystalReportViewer1.Location = new System.Drawing.Point(0, 0);
            this.crystalReportViewer1.Name = "crystalReportViewer1";
            this.crystalReportViewer1.ReportSource = this.firstCrystalReport1;
            this.crystalReportViewer1.Size = new System.Drawing.Size(419, 323);
            this.crystalReportViewer1.TabIndex = 0;
            // 
            // t_stoc_stocktakTableAdapter
            // 
            this.t_stoc_stocktakTableAdapter.ClearBeforeFill = true;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(109, 504);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 24;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // t_stoc_stocktakDataGridView
            // 
            this.t_stoc_stocktakDataGridView.AllowUserToAddRows = false;
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
            this.t_stoc_stocktakDataGridView.Location = new System.Drawing.Point(210, 345);
            this.t_stoc_stocktakDataGridView.Name = "t_stoc_stocktakDataGridView";
            this.t_stoc_stocktakDataGridView.RowTemplate.Height = 24;
            this.t_stoc_stocktakDataGridView.Size = new System.Drawing.Size(300, 220);
            this.t_stoc_stocktakDataGridView.TabIndex = 24;
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
            // filterDataSet
            // 
            this.filterDataSet.DataSetName = "FilterDataSet";
            this.filterDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // filterDataSetBindingSource
            // 
            this.filterDataSetBindingSource.DataSource = this.filterDataSet;
            this.filterDataSetBindingSource.Position = 0;
            // 
            // huochangpandianDataSet1
            // 
            this.huochangpandianDataSet1.DataSetName = "HuochangpandianDataSet";
            this.huochangpandianDataSet1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // ColumnFilterForm
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 15);
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(658, 585);
            this.Controls.Add(this.t_stoc_stocktakDataGridView);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.groupBox1);
            this.Name = "ColumnFilterForm";
            this.Load += new System.EventHandler(this.ColumnFilterForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.t_stoc_stocktakBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.huochangpandianDataSet)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.t_stoc_stocktakDataGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.filterDataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.filterDataSetBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.huochangpandianDataSet1)).EndInit();
            this.ResumeLayout(false);

		}
		#endregion

		public void SetSourceColumns (DataColumnCollection Columns)
		{
			try
			{
				foreach (DataColumn col in Columns)
				{
					ClbShowColumn.Items.Add(col.ColumnName.ToString());
				}
			
			}
			catch (System.Exception a_Ex)
			{
				MessageBox.Show(a_Ex.Message);
			}
		}

		public CheckedListBox GetSelectedColumns()
		{
			return ClbShowColumn;
		}

        private void t_stoc_stocktakBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.t_stoc_stocktakBindingSource.EndEdit();
            this.t_stoc_stocktakTableAdapter.Update(this.huochangpandianDataSet.t_stoc_stocktak);

           

        }

        private void ColumnFilterForm_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'huochangpandianDataSet.t_stoc_stocktak' table. You can move, or remove it, as needed.
            this.firstCrystalReport1.SetDataSource(this.huochangpandianDataSet);
            
            this.t_stoc_stocktakTableAdapter.Connection.ConnectionString = "Data Source=DENY;Initial Catalog=new_chy;User ID=sa;password=deny01;Connect Timeout=1200";
            this.t_stoc_stocktakTableAdapter.Fill(this.huochangpandianDataSet.t_stoc_stocktak);

            //this.reportViewer1.RefreshReport();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            
            this.t_stoc_stocktakBindingSource.Filter = "廠別='１'";
            this.huochangpandianDataSet1.t_stoc_stocktak.Rows.Clear();
            //this.huochangpandianDataSet.t_stoc_stocktak.DefaultView.Count
            //this.t_stoc_stocktakDataGridView.Rows.
            DataRow dr;
           
            foreach (DataGridViewRow hererow in this.t_stoc_stocktakDataGridView.Rows)
            {
                //dataGridView1.Rows.Add(rowArray);
                dr = this.huochangpandianDataSet1.t_stoc_stocktak.NewRow();
                for (int i = 1; i < hererow.Cells.Count -1 ; i++)
                {
                    if (null != hererow.Cells[i].Value)
                    {
                        dr[i] = hererow.Cells[i].Value;
                    }
                    else 
                    {
                        dr[i] =System.DBNull.Value;
                    }
                
                }
                this.huochangpandianDataSet1.t_stoc_stocktak.Rows.Add(dr);
                //this.huochangpandianDataSet1.t_stoc_stocktak.Rows.Add(hererow);
            }

            
            this.firstCrystalReport1.SetDataSource(this.huochangpandianDataSet1);
            //this.firstCrystalReport1.Refresh();
            this.crystalReportViewer1.RefreshReport();
        }

		
	}
}
