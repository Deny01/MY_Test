namespace DataGridFilter
{
    partial class FilterForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FilterForm));
            this.filterDataSet = new DataGridFilter.FilterDataSet();
            this.filterTableBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.filterTableBindingNavigator = new System.Windows.Forms.BindingNavigator(this.components);
            this.bindingNavigatorAddNewItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorCountItem = new System.Windows.Forms.ToolStripLabel();
            this.bindingNavigatorDeleteItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorMoveFirstItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorMovePreviousItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.bindingNavigatorPositionItem = new System.Windows.Forms.ToolStripTextBox();
            this.bindingNavigatorSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.bindingNavigatorMoveNextItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorMoveLastItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.filterTableBindingNavigatorSaveItem = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.filterTableDataGridView = new System.Windows.Forms.DataGridView();
            this.filterTableBindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.filterDataSetBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.filterTableBindingSource2 = new System.Windows.Forms.BindingSource(this.components);
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.filterTableBindingSource3 = new System.Windows.Forms.BindingSource(this.components);
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.关系 = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.数值 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.逻辑 = new System.Windows.Forms.DataGridViewComboBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.filterDataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.filterTableBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.filterTableBindingNavigator)).BeginInit();
            this.filterTableBindingNavigator.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.filterTableDataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.filterTableBindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.filterDataSetBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.filterTableBindingSource2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.filterTableBindingSource3)).BeginInit();
            this.SuspendLayout();
            // 
            // filterDataSet
            // 
            this.filterDataSet.DataSetName = "FilterDataSet";
            this.filterDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // filterTableBindingSource
            // 
            this.filterTableBindingSource.DataMember = "FilterTable";
            this.filterTableBindingSource.DataSource = this.filterDataSet;
            // 
            // filterTableBindingNavigator
            // 
            this.filterTableBindingNavigator.AddNewItem = this.bindingNavigatorAddNewItem;
            this.filterTableBindingNavigator.BindingSource = this.filterTableBindingSource;
            this.filterTableBindingNavigator.CountItem = this.bindingNavigatorCountItem;
            this.filterTableBindingNavigator.DeleteItem = this.bindingNavigatorDeleteItem;
            this.filterTableBindingNavigator.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.bindingNavigatorMoveFirstItem,
            this.bindingNavigatorMovePreviousItem,
            this.bindingNavigatorSeparator,
            this.bindingNavigatorPositionItem,
            this.bindingNavigatorCountItem,
            this.bindingNavigatorSeparator1,
            this.bindingNavigatorMoveNextItem,
            this.bindingNavigatorMoveLastItem,
            this.bindingNavigatorSeparator2,
            this.bindingNavigatorAddNewItem,
            this.bindingNavigatorDeleteItem,
            this.filterTableBindingNavigatorSaveItem,
            this.toolStripButton1});
            this.filterTableBindingNavigator.Location = new System.Drawing.Point(0, 0);
            this.filterTableBindingNavigator.MoveFirstItem = this.bindingNavigatorMoveFirstItem;
            this.filterTableBindingNavigator.MoveLastItem = this.bindingNavigatorMoveLastItem;
            this.filterTableBindingNavigator.MoveNextItem = this.bindingNavigatorMoveNextItem;
            this.filterTableBindingNavigator.MovePreviousItem = this.bindingNavigatorMovePreviousItem;
            this.filterTableBindingNavigator.Name = "filterTableBindingNavigator";
            this.filterTableBindingNavigator.PositionItem = this.bindingNavigatorPositionItem;
            this.filterTableBindingNavigator.Size = new System.Drawing.Size(448, 25);
            this.filterTableBindingNavigator.TabIndex = 0;
            this.filterTableBindingNavigator.Text = "bindingNavigator1";
            // 
            // bindingNavigatorAddNewItem
            // 
            this.bindingNavigatorAddNewItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorAddNewItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorAddNewItem.Image")));
            this.bindingNavigatorAddNewItem.Name = "bindingNavigatorAddNewItem";
            this.bindingNavigatorAddNewItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorAddNewItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorAddNewItem.Text = "Add new";
            this.bindingNavigatorAddNewItem.Click += new System.EventHandler(this.bindingNavigatorAddNewItem_Click);
            // 
            // bindingNavigatorCountItem
            // 
            this.bindingNavigatorCountItem.Name = "bindingNavigatorCountItem";
            this.bindingNavigatorCountItem.Size = new System.Drawing.Size(35, 22);
            this.bindingNavigatorCountItem.Text = "/ {0}";
            this.bindingNavigatorCountItem.ToolTipText = "Total number of items";
            // 
            // bindingNavigatorDeleteItem
            // 
            this.bindingNavigatorDeleteItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorDeleteItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorDeleteItem.Image")));
            this.bindingNavigatorDeleteItem.Name = "bindingNavigatorDeleteItem";
            this.bindingNavigatorDeleteItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorDeleteItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorDeleteItem.Text = "Delete";
            // 
            // bindingNavigatorMoveFirstItem
            // 
            this.bindingNavigatorMoveFirstItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMoveFirstItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMoveFirstItem.Image")));
            this.bindingNavigatorMoveFirstItem.Name = "bindingNavigatorMoveFirstItem";
            this.bindingNavigatorMoveFirstItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMoveFirstItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorMoveFirstItem.Text = "Move first";
            // 
            // bindingNavigatorMovePreviousItem
            // 
            this.bindingNavigatorMovePreviousItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMovePreviousItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMovePreviousItem.Image")));
            this.bindingNavigatorMovePreviousItem.Name = "bindingNavigatorMovePreviousItem";
            this.bindingNavigatorMovePreviousItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMovePreviousItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorMovePreviousItem.Text = "Move previous";
            // 
            // bindingNavigatorSeparator
            // 
            this.bindingNavigatorSeparator.Name = "bindingNavigatorSeparator";
            this.bindingNavigatorSeparator.Size = new System.Drawing.Size(6, 25);
            // 
            // bindingNavigatorPositionItem
            // 
            this.bindingNavigatorPositionItem.AccessibleName = "Position";
            this.bindingNavigatorPositionItem.AutoSize = false;
            this.bindingNavigatorPositionItem.Name = "bindingNavigatorPositionItem";
            this.bindingNavigatorPositionItem.Size = new System.Drawing.Size(50, 22);
            this.bindingNavigatorPositionItem.Text = "0";
            this.bindingNavigatorPositionItem.ToolTipText = "Current position";
            // 
            // bindingNavigatorSeparator1
            // 
            this.bindingNavigatorSeparator1.Name = "bindingNavigatorSeparator1";
            this.bindingNavigatorSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // bindingNavigatorMoveNextItem
            // 
            this.bindingNavigatorMoveNextItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMoveNextItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMoveNextItem.Image")));
            this.bindingNavigatorMoveNextItem.Name = "bindingNavigatorMoveNextItem";
            this.bindingNavigatorMoveNextItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMoveNextItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorMoveNextItem.Text = "Move next";
            // 
            // bindingNavigatorMoveLastItem
            // 
            this.bindingNavigatorMoveLastItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMoveLastItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMoveLastItem.Image")));
            this.bindingNavigatorMoveLastItem.Name = "bindingNavigatorMoveLastItem";
            this.bindingNavigatorMoveLastItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMoveLastItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorMoveLastItem.Text = "Move last";
            // 
            // bindingNavigatorSeparator2
            // 
            this.bindingNavigatorSeparator2.Name = "bindingNavigatorSeparator2";
            this.bindingNavigatorSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // filterTableBindingNavigatorSaveItem
            // 
            this.filterTableBindingNavigatorSaveItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.filterTableBindingNavigatorSaveItem.Enabled = false;
            this.filterTableBindingNavigatorSaveItem.Image = ((System.Drawing.Image)(resources.GetObject("filterTableBindingNavigatorSaveItem.Image")));
            this.filterTableBindingNavigatorSaveItem.Name = "filterTableBindingNavigatorSaveItem";
            this.filterTableBindingNavigatorSaveItem.Size = new System.Drawing.Size(23, 22);
            this.filterTableBindingNavigatorSaveItem.Text = "Save Data";
            this.filterTableBindingNavigatorSaveItem.Click += new System.EventHandler(this.filterTableBindingNavigatorSaveItem_Click);
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton1.Text = "toolStripButton1";
            // 
            // filterTableDataGridView
            // 
            this.filterTableDataGridView.AutoGenerateColumns = false;
            this.filterTableDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.关系,
            this.数值,
            this.逻辑});
            this.filterTableDataGridView.DataSource = this.filterTableBindingSource;
            this.filterTableDataGridView.Location = new System.Drawing.Point(0, 25);
            this.filterTableDataGridView.Name = "filterTableDataGridView";
            this.filterTableDataGridView.RowTemplate.Height = 24;
            this.filterTableDataGridView.Size = new System.Drawing.Size(445, 205);
            this.filterTableDataGridView.TabIndex = 1;
            // 
            // filterTableBindingSource1
            // 
            this.filterTableBindingSource1.DataMember = "FilterTable";
            this.filterTableBindingSource1.DataSource = this.filterDataSetBindingSource;
            // 
            // filterDataSetBindingSource
            // 
            this.filterDataSetBindingSource.DataSource = this.filterDataSet;
            this.filterDataSetBindingSource.Position = 0;
            // 
            // filterTableBindingSource2
            // 
            this.filterTableBindingSource2.DataMember = "FilterTable";
            this.filterTableBindingSource2.DataSource = this.filterDataSetBindingSource;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(291, 238);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(67, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "确　定";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button2.Location = new System.Drawing.Point(369, 238);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(67, 23);
            this.button2.TabIndex = 3;
            this.button2.Text = "取　消";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(120, 242);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(120, 16);
            this.checkBox1.TabIndex = 4;
            this.checkBox1.Text = "在上次结果中查询";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // filterTableBindingSource3
            // 
            this.filterTableBindingSource3.DataMember = "FilterTable";
            this.filterTableBindingSource3.DataSource = this.filterDataSetBindingSource;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.DataPropertyName = "字段";
            this.dataGridViewTextBoxColumn1.DataSource = this.filterDataSet;
            this.dataGridViewTextBoxColumn1.DisplayMember = "ColumnNameTable.ColumnName";
            this.dataGridViewTextBoxColumn1.HeaderText = "字段";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewTextBoxColumn1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.dataGridViewTextBoxColumn1.ValueMember = "ColumnNameTable.ColumnName";
            // 
            // 关系
            // 
            this.关系.DataPropertyName = "关系";
            this.关系.HeaderText = "关系";
            this.关系.Items.AddRange(new object[] {
            "=",
            "<",
            "<=",
            ">",
            ">=",
            "<>",
            "LIKE",
            "NOT LIKE"});
            this.关系.Name = "关系";
            this.关系.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.关系.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // 数值
            // 
            this.数值.DataPropertyName = "数值";
            this.数值.HeaderText = "数值";
            this.数值.Name = "数值";
            // 
            // 逻辑
            // 
            this.逻辑.DataPropertyName = "逻辑";
            this.逻辑.HeaderText = "逻辑";
            this.逻辑.Items.AddRange(new object[] {
            "AND",
            "OR"});
            this.逻辑.Name = "逻辑";
            this.逻辑.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.逻辑.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // FilterForm
            // 
            this.AcceptButton = this.button1;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.button2;
            this.ClientSize = new System.Drawing.Size(448, 273);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.filterTableDataGridView);
            this.Controls.Add(this.filterTableBindingNavigator);
            this.Name = "FilterForm";
            this.Text = "FilterForm";
            ((System.ComponentModel.ISupportInitialize)(this.filterDataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.filterTableBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.filterTableBindingNavigator)).EndInit();
            this.filterTableBindingNavigator.ResumeLayout(false);
            this.filterTableBindingNavigator.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.filterTableDataGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.filterTableBindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.filterDataSetBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.filterTableBindingSource2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.filterTableBindingSource3)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private FilterDataSet filterDataSet;
        private System.Windows.Forms.BindingSource filterTableBindingSource;
        private System.Windows.Forms.BindingNavigator filterTableBindingNavigator;
        private System.Windows.Forms.ToolStripButton bindingNavigatorAddNewItem;
        private System.Windows.Forms.ToolStripLabel bindingNavigatorCountItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorDeleteItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveFirstItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMovePreviousItem;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator;
        private System.Windows.Forms.ToolStripTextBox bindingNavigatorPositionItem;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator1;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveNextItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveLastItem;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator2;
        private System.Windows.Forms.ToolStripButton filterTableBindingNavigatorSaveItem;
        private System.Windows.Forms.DataGridView filterTableDataGridView;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.DataGridViewComboBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewComboBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.BindingSource filterTableBindingSource1;
        private System.Windows.Forms.BindingSource filterDataSetBindingSource;
        private System.Windows.Forms.BindingSource filterTableBindingSource2;
        private System.Windows.Forms.BindingSource filterTableBindingSource3;
        private System.Windows.Forms.DataGridViewComboBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewComboBoxColumn 关系;
        private System.Windows.Forms.DataGridViewTextBoxColumn 数值;
        private System.Windows.Forms.DataGridViewComboBoxColumn 逻辑;
    }
}