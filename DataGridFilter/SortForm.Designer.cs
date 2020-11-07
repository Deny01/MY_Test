namespace DataGridFilter
{
    partial class SortForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.columnNameTableDataGridView = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnNameTableBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.filterDataSet = new DataGridFilter.FilterDataSet();
            this.sortTableBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.sortTableDataGridView = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.columnNameTableDataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.columnNameTableBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.filterDataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sortTableBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sortTableDataGridView)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.SuspendLayout();
            // 
            // columnNameTableDataGridView
            // 
            this.columnNameTableDataGridView.AllowUserToAddRows = false;
            this.columnNameTableDataGridView.AutoGenerateColumns = false;
            this.columnNameTableDataGridView.BackgroundColor = System.Drawing.SystemColors.ActiveBorder;
            this.columnNameTableDataGridView.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.columnNameTableDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1});
            this.columnNameTableDataGridView.DataSource = this.columnNameTableBindingSource;
            this.columnNameTableDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.columnNameTableDataGridView.GridColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.columnNameTableDataGridView.Location = new System.Drawing.Point(0, 0);
            this.columnNameTableDataGridView.Name = "columnNameTableDataGridView";
            this.columnNameTableDataGridView.ReadOnly = true;
            this.columnNameTableDataGridView.RowTemplate.Height = 24;
            this.columnNameTableDataGridView.Size = new System.Drawing.Size(134, 211);
            this.columnNameTableDataGridView.TabIndex = 1;
            this.columnNameTableDataGridView.DoubleClick += new System.EventHandler(this.columnNameTableDataGridView_DoubleClick);
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.DataPropertyName = "ColumnName";
            this.dataGridViewTextBoxColumn1.HeaderText = "◊÷∂Œ√˚";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            // 
            // columnNameTableBindingSource
            // 
            this.columnNameTableBindingSource.DataMember = "ColumnNameTable";
            this.columnNameTableBindingSource.DataSource = this.filterDataSet;
            // 
            // filterDataSet
            // 
            this.filterDataSet.DataSetName = "FilterDataSet";
            this.filterDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // sortTableBindingSource
            // 
            this.sortTableBindingSource.DataMember = "SortTable";
            this.sortTableBindingSource.DataSource = this.filterDataSet;
            // 
            // sortTableDataGridView
            // 
            this.sortTableDataGridView.AllowUserToAddRows = false;
            this.sortTableDataGridView.AutoGenerateColumns = false;
            this.sortTableDataGridView.BackgroundColor = System.Drawing.SystemColors.ActiveBorder;
            this.sortTableDataGridView.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.sortTableDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn2,
            this.dataGridViewTextBoxColumn3});
            this.sortTableDataGridView.DataSource = this.sortTableBindingSource;
            this.sortTableDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sortTableDataGridView.GridColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.sortTableDataGridView.Location = new System.Drawing.Point(0, 0);
            this.sortTableDataGridView.Name = "sortTableDataGridView";
            this.sortTableDataGridView.RowTemplate.Height = 24;
            this.sortTableDataGridView.Size = new System.Drawing.Size(264, 211);
            this.sortTableDataGridView.TabIndex = 1;
            this.sortTableDataGridView.DoubleClick += new System.EventHandler(this.sortTableDataGridView_DoubleClick);
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.DataPropertyName = "ColumnName";
            this.dataGridViewTextBoxColumn2.HeaderText = "◊÷∂Œ√˚";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.DataPropertyName = "Down";
            this.dataGridViewTextBoxColumn3.FalseValue = "ASC";
            this.dataGridViewTextBoxColumn3.HeaderText = "Ωµ–Ú";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewTextBoxColumn3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.dataGridViewTextBoxColumn3.TrueValue = "DESC";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.columnNameTableDataGridView);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.sortTableDataGridView);
            this.splitContainer1.Size = new System.Drawing.Size(402, 211);
            this.splitContainer1.SplitterDistance = 134;
            this.splitContainer1.TabIndex = 2;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.splitContainer1);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.button1);
            this.splitContainer2.Size = new System.Drawing.Size(402, 246);
            this.splitContainer2.SplitterDistance = 211;
            this.splitContainer2.TabIndex = 3;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(315, 3);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "»∑ ∂®";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // SortForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(402, 246);
            this.Controls.Add(this.splitContainer2);
            this.Name = "SortForm";
            this.Text = "≈≈–Ú…Ë÷√";
            ((System.ComponentModel.ISupportInitialize)(this.columnNameTableDataGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.columnNameTableBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.filterDataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sortTableBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sortTableDataGridView)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private FilterDataSet filterDataSet;
        private System.Windows.Forms.BindingSource columnNameTableBindingSource;
        private System.Windows.Forms.DataGridView columnNameTableDataGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.BindingSource sortTableBindingSource;
        public  System.Windows.Forms.DataGridView sortTableDataGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.Button button1;
    }
}