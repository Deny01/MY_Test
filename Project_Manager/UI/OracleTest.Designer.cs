namespace Project_Manager.UI
{
    partial class OracleTest
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OracleTest));
            this.dataSet1 = new Project_Manager.DataSet1();
            this.fIR_ORA_TABLEBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.fIR_ORA_TABLETableAdapter = new Project_Manager.DataSet1TableAdapters.FIR_ORA_TABLETableAdapter();
            this.tableAdapterManager = new Project_Manager.DataSet1TableAdapters.TableAdapterManager();
            this.fIR_ORA_TABLEBindingNavigator = new System.Windows.Forms.BindingNavigator(this.components);
            this.bindingNavigatorMoveFirstItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorMovePreviousItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.bindingNavigatorPositionItem = new System.Windows.Forms.ToolStripTextBox();
            this.bindingNavigatorCountItem = new System.Windows.Forms.ToolStripLabel();
            this.bindingNavigatorSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.bindingNavigatorMoveNextItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorMoveLastItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.bindingNavigatorAddNewItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorDeleteItem = new System.Windows.Forms.ToolStripButton();
            this.fIR_ORA_TABLEBindingNavigatorSaveItem = new System.Windows.Forms.ToolStripButton();
            this.fIR_ORA_TABLEDataGridView = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataSet1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fIR_ORA_TABLEBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fIR_ORA_TABLEBindingNavigator)).BeginInit();
            this.fIR_ORA_TABLEBindingNavigator.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fIR_ORA_TABLEDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // dataSet1
            // 
            this.dataSet1.DataSetName = "DataSet1";
            this.dataSet1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // fIR_ORA_TABLEBindingSource
            // 
            this.fIR_ORA_TABLEBindingSource.DataMember = "FIR_ORA_TABLE";
            this.fIR_ORA_TABLEBindingSource.DataSource = this.dataSet1;
            // 
            // fIR_ORA_TABLETableAdapter
            // 
            this.fIR_ORA_TABLETableAdapter.ClearBeforeFill = true;
            // 
            // tableAdapterManager
            // 
            this.tableAdapterManager.BackupDataSetBeforeUpdate = false;
            this.tableAdapterManager.FIR_ORA_TABLETableAdapter = this.fIR_ORA_TABLETableAdapter;
            this.tableAdapterManager.UpdateOrder = Project_Manager.DataSet1TableAdapters.TableAdapterManager.UpdateOrderOption.InsertUpdateDelete;
            // 
            // fIR_ORA_TABLEBindingNavigator
            // 
            this.fIR_ORA_TABLEBindingNavigator.AddNewItem = this.bindingNavigatorAddNewItem;
            this.fIR_ORA_TABLEBindingNavigator.BindingSource = this.fIR_ORA_TABLEBindingSource;
            this.fIR_ORA_TABLEBindingNavigator.CountItem = this.bindingNavigatorCountItem;
            this.fIR_ORA_TABLEBindingNavigator.DeleteItem = this.bindingNavigatorDeleteItem;
            this.fIR_ORA_TABLEBindingNavigator.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
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
            this.fIR_ORA_TABLEBindingNavigatorSaveItem});
            this.fIR_ORA_TABLEBindingNavigator.Location = new System.Drawing.Point(0, 0);
            this.fIR_ORA_TABLEBindingNavigator.MoveFirstItem = this.bindingNavigatorMoveFirstItem;
            this.fIR_ORA_TABLEBindingNavigator.MoveLastItem = this.bindingNavigatorMoveLastItem;
            this.fIR_ORA_TABLEBindingNavigator.MoveNextItem = this.bindingNavigatorMoveNextItem;
            this.fIR_ORA_TABLEBindingNavigator.MovePreviousItem = this.bindingNavigatorMovePreviousItem;
            this.fIR_ORA_TABLEBindingNavigator.Name = "fIR_ORA_TABLEBindingNavigator";
            this.fIR_ORA_TABLEBindingNavigator.PositionItem = this.bindingNavigatorPositionItem;
            this.fIR_ORA_TABLEBindingNavigator.Size = new System.Drawing.Size(407, 25);
            this.fIR_ORA_TABLEBindingNavigator.TabIndex = 0;
            this.fIR_ORA_TABLEBindingNavigator.Text = "bindingNavigator1";
            // 
            // bindingNavigatorMoveFirstItem
            // 
            this.bindingNavigatorMoveFirstItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMoveFirstItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMoveFirstItem.Image")));
            this.bindingNavigatorMoveFirstItem.Name = "bindingNavigatorMoveFirstItem";
            this.bindingNavigatorMoveFirstItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMoveFirstItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorMoveFirstItem.Text = "移到第一条记录";
            // 
            // bindingNavigatorMovePreviousItem
            // 
            this.bindingNavigatorMovePreviousItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMovePreviousItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMovePreviousItem.Image")));
            this.bindingNavigatorMovePreviousItem.Name = "bindingNavigatorMovePreviousItem";
            this.bindingNavigatorMovePreviousItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMovePreviousItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorMovePreviousItem.Text = "移到上一条记录";
            // 
            // bindingNavigatorSeparator
            // 
            this.bindingNavigatorSeparator.Name = "bindingNavigatorSeparator";
            this.bindingNavigatorSeparator.Size = new System.Drawing.Size(6, 25);
            // 
            // bindingNavigatorPositionItem
            // 
            this.bindingNavigatorPositionItem.AccessibleName = "位置";
            this.bindingNavigatorPositionItem.AutoSize = false;
            this.bindingNavigatorPositionItem.Name = "bindingNavigatorPositionItem";
            this.bindingNavigatorPositionItem.Size = new System.Drawing.Size(50, 21);
            this.bindingNavigatorPositionItem.Text = "0";
            this.bindingNavigatorPositionItem.ToolTipText = "当前位置";
            // 
            // bindingNavigatorCountItem
            // 
            this.bindingNavigatorCountItem.Name = "bindingNavigatorCountItem";
            this.bindingNavigatorCountItem.Size = new System.Drawing.Size(30, 13);
            this.bindingNavigatorCountItem.Text = "/ {0}";
            this.bindingNavigatorCountItem.ToolTipText = "总项数";
            // 
            // bindingNavigatorSeparator1
            // 
            this.bindingNavigatorSeparator1.Name = "bindingNavigatorSeparator";
            this.bindingNavigatorSeparator1.Size = new System.Drawing.Size(6, 6);
            // 
            // bindingNavigatorMoveNextItem
            // 
            this.bindingNavigatorMoveNextItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMoveNextItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMoveNextItem.Image")));
            this.bindingNavigatorMoveNextItem.Name = "bindingNavigatorMoveNextItem";
            this.bindingNavigatorMoveNextItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMoveNextItem.Size = new System.Drawing.Size(23, 20);
            this.bindingNavigatorMoveNextItem.Text = "移到下一条记录";
            // 
            // bindingNavigatorMoveLastItem
            // 
            this.bindingNavigatorMoveLastItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMoveLastItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMoveLastItem.Image")));
            this.bindingNavigatorMoveLastItem.Name = "bindingNavigatorMoveLastItem";
            this.bindingNavigatorMoveLastItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMoveLastItem.Size = new System.Drawing.Size(23, 20);
            this.bindingNavigatorMoveLastItem.Text = "移到最后一条记录";
            // 
            // bindingNavigatorSeparator2
            // 
            this.bindingNavigatorSeparator2.Name = "bindingNavigatorSeparator";
            this.bindingNavigatorSeparator2.Size = new System.Drawing.Size(6, 6);
            // 
            // bindingNavigatorAddNewItem
            // 
            this.bindingNavigatorAddNewItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorAddNewItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorAddNewItem.Image")));
            this.bindingNavigatorAddNewItem.Name = "bindingNavigatorAddNewItem";
            this.bindingNavigatorAddNewItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorAddNewItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorAddNewItem.Text = "新添";
            // 
            // bindingNavigatorDeleteItem
            // 
            this.bindingNavigatorDeleteItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorDeleteItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorDeleteItem.Image")));
            this.bindingNavigatorDeleteItem.Name = "bindingNavigatorDeleteItem";
            this.bindingNavigatorDeleteItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorDeleteItem.Size = new System.Drawing.Size(23, 20);
            this.bindingNavigatorDeleteItem.Text = "删除";
            // 
            // fIR_ORA_TABLEBindingNavigatorSaveItem
            // 
            this.fIR_ORA_TABLEBindingNavigatorSaveItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.fIR_ORA_TABLEBindingNavigatorSaveItem.Image = ((System.Drawing.Image)(resources.GetObject("fIR_ORA_TABLEBindingNavigatorSaveItem.Image")));
            this.fIR_ORA_TABLEBindingNavigatorSaveItem.Name = "fIR_ORA_TABLEBindingNavigatorSaveItem";
            this.fIR_ORA_TABLEBindingNavigatorSaveItem.Size = new System.Drawing.Size(23, 23);
            this.fIR_ORA_TABLEBindingNavigatorSaveItem.Text = "保存数据";
            this.fIR_ORA_TABLEBindingNavigatorSaveItem.Click += new System.EventHandler(this.fIR_ORA_TABLEBindingNavigatorSaveItem_Click);
            // 
            // fIR_ORA_TABLEDataGridView
            // 
            this.fIR_ORA_TABLEDataGridView.AutoGenerateColumns = false;
            this.fIR_ORA_TABLEDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.fIR_ORA_TABLEDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2});
            this.fIR_ORA_TABLEDataGridView.DataSource = this.fIR_ORA_TABLEBindingSource;
            this.fIR_ORA_TABLEDataGridView.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.fIR_ORA_TABLEDataGridView.Location = new System.Drawing.Point(0, 122);
            this.fIR_ORA_TABLEDataGridView.Name = "fIR_ORA_TABLEDataGridView";
            this.fIR_ORA_TABLEDataGridView.RowTemplate.Height = 23;
            this.fIR_ORA_TABLEDataGridView.Size = new System.Drawing.Size(407, 177);
            this.fIR_ORA_TABLEDataGridView.TabIndex = 1;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.DataPropertyName = "ID";
            this.dataGridViewTextBoxColumn1.HeaderText = "ID";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.DataPropertyName = "NAME";
            this.dataGridViewTextBoxColumn2.HeaderText = "NAME";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(33, 44);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 21);
            this.textBox1.TabIndex = 2;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(162, 41);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 3;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // OracleTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(407, 299);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.fIR_ORA_TABLEDataGridView);
            this.Controls.Add(this.fIR_ORA_TABLEBindingNavigator);
            this.Name = "OracleTest";
            this.Text = "OracleTest";
            this.Load += new System.EventHandler(this.OracleTest_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataSet1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fIR_ORA_TABLEBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fIR_ORA_TABLEBindingNavigator)).EndInit();
            this.fIR_ORA_TABLEBindingNavigator.ResumeLayout(false);
            this.fIR_ORA_TABLEBindingNavigator.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fIR_ORA_TABLEDataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DataSet1 dataSet1;
        private System.Windows.Forms.BindingSource fIR_ORA_TABLEBindingSource;
        private Project_Manager.DataSet1TableAdapters.FIR_ORA_TABLETableAdapter fIR_ORA_TABLETableAdapter;
        private Project_Manager.DataSet1TableAdapters.TableAdapterManager tableAdapterManager;
        private System.Windows.Forms.BindingNavigator fIR_ORA_TABLEBindingNavigator;
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
        private System.Windows.Forms.ToolStripButton fIR_ORA_TABLEBindingNavigatorSaveItem;
        private System.Windows.Forms.DataGridView fIR_ORA_TABLEDataGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button button1;
    }
}