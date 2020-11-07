namespace HappyYF.YuanXin.WorkSet
{
    partial class LY_StoreinSet
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LY_StoreinSet));
            this.lYStoreMange = new HappyYF.YuanXin.Data.LYStoreMange();
            this.ly_store_in_stylesetBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.ly_store_in_stylesetTableAdapter = new HappyYF.YuanXin.Data.LYStoreMangeTableAdapters.ly_store_in_stylesetTableAdapter();
            this.tableAdapterManager = new HappyYF.YuanXin.Data.LYStoreMangeTableAdapters.TableAdapterManager();
            this.ly_store_in_stylesetBindingNavigator = new System.Windows.Forms.BindingNavigator(this.components);
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
            this.ly_store_in_stylesetBindingNavigatorSaveItem = new System.Windows.Forms.ToolStripButton();
            this.ly_store_in_stylesetDataGridView = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.lYStoreMange)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ly_store_in_stylesetBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ly_store_in_stylesetBindingNavigator)).BeginInit();
            this.ly_store_in_stylesetBindingNavigator.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ly_store_in_stylesetDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // lYStoreMange
            // 
            this.lYStoreMange.DataSetName = "LYStoreMange";
            this.lYStoreMange.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // ly_store_in_stylesetBindingSource
            // 
            this.ly_store_in_stylesetBindingSource.DataMember = "ly_store_in_styleset";
            this.ly_store_in_stylesetBindingSource.DataSource = this.lYStoreMange;
            // 
            // ly_store_in_stylesetTableAdapter
            // 
            this.ly_store_in_stylesetTableAdapter.ClearBeforeFill = true;
            // 
            // tableAdapterManager
            // 
            this.tableAdapterManager.BackupDataSetBeforeUpdate = false;
            this.tableAdapterManager.ly_inma0010TableAdapter = null;
            this.tableAdapterManager.ly_stocktake_detailTableAdapter = null;
            this.tableAdapterManager.ly_stocktake_mainTableAdapter = null;
            this.tableAdapterManager.ly_stocktake_noselectItemTableAdapter = null;
            this.tableAdapterManager.ly_store_in_innumTableAdapter = null;
            this.tableAdapterManager.ly_store_in_stylesetTableAdapter = this.ly_store_in_stylesetTableAdapter;
            this.tableAdapterManager.ly_store_in_ylTableAdapter = null;
            this.tableAdapterManager.ly_store_inTableAdapter = null;
            this.tableAdapterManager.ly_store_out_stylesetTableAdapter = null;
            this.tableAdapterManager.ly_store_outqueryTableAdapter = null;
            this.tableAdapterManager.ly_store_outTableAdapter = null;
            this.tableAdapterManager.ly_store_planoutTableAdapter = null;
            this.tableAdapterManager.UpdateOrder = HappyYF.YuanXin.Data.LYStoreMangeTableAdapters.TableAdapterManager.UpdateOrderOption.InsertUpdateDelete;
            // 
            // ly_store_in_stylesetBindingNavigator
            // 
            this.ly_store_in_stylesetBindingNavigator.AddNewItem = this.bindingNavigatorAddNewItem;
            this.ly_store_in_stylesetBindingNavigator.BindingSource = this.ly_store_in_stylesetBindingSource;
            this.ly_store_in_stylesetBindingNavigator.CountItem = this.bindingNavigatorCountItem;
            this.ly_store_in_stylesetBindingNavigator.DeleteItem = this.bindingNavigatorDeleteItem;
            this.ly_store_in_stylesetBindingNavigator.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
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
            this.ly_store_in_stylesetBindingNavigatorSaveItem});
            this.ly_store_in_stylesetBindingNavigator.Location = new System.Drawing.Point(0, 0);
            this.ly_store_in_stylesetBindingNavigator.MoveFirstItem = this.bindingNavigatorMoveFirstItem;
            this.ly_store_in_stylesetBindingNavigator.MoveLastItem = this.bindingNavigatorMoveLastItem;
            this.ly_store_in_stylesetBindingNavigator.MoveNextItem = this.bindingNavigatorMoveNextItem;
            this.ly_store_in_stylesetBindingNavigator.MovePreviousItem = this.bindingNavigatorMovePreviousItem;
            this.ly_store_in_stylesetBindingNavigator.Name = "ly_store_in_stylesetBindingNavigator";
            this.ly_store_in_stylesetBindingNavigator.PositionItem = this.bindingNavigatorPositionItem;
            this.ly_store_in_stylesetBindingNavigator.Size = new System.Drawing.Size(384, 25);
            this.ly_store_in_stylesetBindingNavigator.TabIndex = 0;
            this.ly_store_in_stylesetBindingNavigator.Text = "bindingNavigator1";
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
            this.bindingNavigatorCountItem.Size = new System.Drawing.Size(35, 12);
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
            // ly_store_in_stylesetBindingNavigatorSaveItem
            // 
            this.ly_store_in_stylesetBindingNavigatorSaveItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ly_store_in_stylesetBindingNavigatorSaveItem.Image = ((System.Drawing.Image)(resources.GetObject("ly_store_in_stylesetBindingNavigatorSaveItem.Image")));
            this.ly_store_in_stylesetBindingNavigatorSaveItem.Name = "ly_store_in_stylesetBindingNavigatorSaveItem";
            this.ly_store_in_stylesetBindingNavigatorSaveItem.Size = new System.Drawing.Size(23, 23);
            this.ly_store_in_stylesetBindingNavigatorSaveItem.Text = "保存数据";
            this.ly_store_in_stylesetBindingNavigatorSaveItem.Click += new System.EventHandler(this.ly_store_in_stylesetBindingNavigatorSaveItem_Click);
            // 
            // ly_store_in_stylesetDataGridView
            // 
            this.ly_store_in_stylesetDataGridView.AutoGenerateColumns = false;
            this.ly_store_in_stylesetDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ly_store_in_stylesetDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2});
            this.ly_store_in_stylesetDataGridView.DataSource = this.ly_store_in_stylesetBindingSource;
            this.ly_store_in_stylesetDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ly_store_in_stylesetDataGridView.Location = new System.Drawing.Point(0, 25);
            this.ly_store_in_stylesetDataGridView.Name = "ly_store_in_stylesetDataGridView";
            this.ly_store_in_stylesetDataGridView.RowTemplate.Height = 23;
            this.ly_store_in_stylesetDataGridView.Size = new System.Drawing.Size(384, 324);
            this.ly_store_in_stylesetDataGridView.TabIndex = 1;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.DataPropertyName = "stylecode";
            this.dataGridViewTextBoxColumn1.HeaderText = "编码";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.DataPropertyName = "stylename";
            this.dataGridViewTextBoxColumn2.HeaderText = "名称";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            // 
            // LY_StoreinSet
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(384, 349);
            this.Controls.Add(this.ly_store_in_stylesetDataGridView);
            this.Controls.Add(this.ly_store_in_stylesetBindingNavigator);
            this.Name = "LY_StoreinSet";
            this.Text = "入库类别设置";
            this.Load += new System.EventHandler(this.LY_StoreinSet_Load);
            ((System.ComponentModel.ISupportInitialize)(this.lYStoreMange)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ly_store_in_stylesetBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ly_store_in_stylesetBindingNavigator)).EndInit();
            this.ly_store_in_stylesetBindingNavigator.ResumeLayout(false);
            this.ly_store_in_stylesetBindingNavigator.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ly_store_in_stylesetDataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private HappyYF.YuanXin.Data.LYStoreMange lYStoreMange;
        private System.Windows.Forms.BindingSource ly_store_in_stylesetBindingSource;
        private HappyYF.YuanXin.Data.LYStoreMangeTableAdapters.ly_store_in_stylesetTableAdapter ly_store_in_stylesetTableAdapter;
        private HappyYF.YuanXin.Data.LYStoreMangeTableAdapters.TableAdapterManager tableAdapterManager;
        private System.Windows.Forms.BindingNavigator ly_store_in_stylesetBindingNavigator;
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
        private System.Windows.Forms.ToolStripButton ly_store_in_stylesetBindingNavigatorSaveItem;
        private System.Windows.Forms.DataGridView ly_store_in_stylesetDataGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
    }
}