namespace HappyYF.YuanXin.WorkSet
{
    partial class LY_WxExcelRep
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LY_WxExcelRep));
            this.ly_purchase_contract_inspectionRepBindingNavigator = new System.Windows.Forms.BindingNavigator(this.components);
            this.salesexcelBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.lYSalseMange2 = new HappyYF.YuanXin.Data.LYSalseMange2();
            this.bindingNavigatorCountItem = new System.Windows.Forms.ToolStripLabel();
            this.bindingNavigatorMoveFirstItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorMovePreviousItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.bindingNavigatorPositionItem = new System.Windows.Forms.ToolStripTextBox();
            this.bindingNavigatorSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.bindingNavigatorMoveNextItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorMoveLastItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.ly_purchase_contract_inspectionRepDataGridView = new System.Windows.Forms.DataGridView();
            this.button1 = new System.Windows.Forms.Button();
            this.tableAdapterManager = new HappyYF.YuanXin.Data.LYQualityInspectorTableAdapters.TableAdapterManager();
            this.sales_excelTableAdapter = new HappyYF.YuanXin.Data.LYSalseMange2TableAdapters.sales_excelTableAdapter();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.删除该文件ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.下载文件ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.filenameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.filepath = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.rececodeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.savatimeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.savepeo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.ly_purchase_contract_inspectionRepBindingNavigator)).BeginInit();
            this.ly_purchase_contract_inspectionRepBindingNavigator.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.salesexcelBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lYSalseMange2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ly_purchase_contract_inspectionRepDataGridView)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // ly_purchase_contract_inspectionRepBindingNavigator
            // 
            this.ly_purchase_contract_inspectionRepBindingNavigator.AddNewItem = null;
            this.ly_purchase_contract_inspectionRepBindingNavigator.BindingSource = this.salesexcelBindingSource;
            this.ly_purchase_contract_inspectionRepBindingNavigator.CountItem = this.bindingNavigatorCountItem;
            this.ly_purchase_contract_inspectionRepBindingNavigator.DeleteItem = null;
            this.ly_purchase_contract_inspectionRepBindingNavigator.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.bindingNavigatorMoveFirstItem,
            this.bindingNavigatorMovePreviousItem,
            this.bindingNavigatorSeparator,
            this.bindingNavigatorPositionItem,
            this.bindingNavigatorCountItem,
            this.bindingNavigatorSeparator1,
            this.bindingNavigatorMoveNextItem,
            this.bindingNavigatorMoveLastItem,
            this.bindingNavigatorSeparator2});
            this.ly_purchase_contract_inspectionRepBindingNavigator.Location = new System.Drawing.Point(0, 0);
            this.ly_purchase_contract_inspectionRepBindingNavigator.MoveFirstItem = this.bindingNavigatorMoveFirstItem;
            this.ly_purchase_contract_inspectionRepBindingNavigator.MoveLastItem = this.bindingNavigatorMoveLastItem;
            this.ly_purchase_contract_inspectionRepBindingNavigator.MoveNextItem = this.bindingNavigatorMoveNextItem;
            this.ly_purchase_contract_inspectionRepBindingNavigator.MovePreviousItem = this.bindingNavigatorMovePreviousItem;
            this.ly_purchase_contract_inspectionRepBindingNavigator.Name = "ly_purchase_contract_inspectionRepBindingNavigator";
            this.ly_purchase_contract_inspectionRepBindingNavigator.PositionItem = this.bindingNavigatorPositionItem;
            this.ly_purchase_contract_inspectionRepBindingNavigator.Size = new System.Drawing.Size(1016, 25);
            this.ly_purchase_contract_inspectionRepBindingNavigator.TabIndex = 0;
            this.ly_purchase_contract_inspectionRepBindingNavigator.Text = "bindingNavigator1";
            // 
            // salesexcelBindingSource
            // 
            this.salesexcelBindingSource.DataMember = "sales_excel";
            this.salesexcelBindingSource.DataSource = this.lYSalseMange2;
            // 
            // lYSalseMange2
            // 
            this.lYSalseMange2.DataSetName = "LYSalseMange2";
            this.lYSalseMange2.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // bindingNavigatorCountItem
            // 
            this.bindingNavigatorCountItem.Name = "bindingNavigatorCountItem";
            this.bindingNavigatorCountItem.Size = new System.Drawing.Size(35, 22);
            this.bindingNavigatorCountItem.Text = "/ {0}";
            this.bindingNavigatorCountItem.ToolTipText = "总项数";
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
            this.bindingNavigatorMoveNextItem.Text = "移到下一条记录";
            // 
            // bindingNavigatorMoveLastItem
            // 
            this.bindingNavigatorMoveLastItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMoveLastItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMoveLastItem.Image")));
            this.bindingNavigatorMoveLastItem.Name = "bindingNavigatorMoveLastItem";
            this.bindingNavigatorMoveLastItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMoveLastItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorMoveLastItem.Text = "移到最后一条记录";
            // 
            // bindingNavigatorSeparator2
            // 
            this.bindingNavigatorSeparator2.Name = "bindingNavigatorSeparator2";
            this.bindingNavigatorSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // ly_purchase_contract_inspectionRepDataGridView
            // 
            this.ly_purchase_contract_inspectionRepDataGridView.AllowUserToAddRows = false;
            this.ly_purchase_contract_inspectionRepDataGridView.AllowUserToDeleteRows = false;
            this.ly_purchase_contract_inspectionRepDataGridView.AutoGenerateColumns = false;
            this.ly_purchase_contract_inspectionRepDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.ly_purchase_contract_inspectionRepDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.id,
            this.filenameDataGridViewTextBoxColumn,
            this.filepath,
            this.rececodeDataGridViewTextBoxColumn,
            this.savatimeDataGridViewTextBoxColumn,
            this.savepeo});
            this.ly_purchase_contract_inspectionRepDataGridView.ContextMenuStrip = this.contextMenuStrip1;
            this.ly_purchase_contract_inspectionRepDataGridView.DataSource = this.salesexcelBindingSource;
            this.ly_purchase_contract_inspectionRepDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ly_purchase_contract_inspectionRepDataGridView.Location = new System.Drawing.Point(0, 25);
            this.ly_purchase_contract_inspectionRepDataGridView.Name = "ly_purchase_contract_inspectionRepDataGridView";
            this.ly_purchase_contract_inspectionRepDataGridView.ReadOnly = true;
            this.ly_purchase_contract_inspectionRepDataGridView.RowHeadersWidth = 19;
            this.ly_purchase_contract_inspectionRepDataGridView.RowTemplate.Height = 23;
            this.ly_purchase_contract_inspectionRepDataGridView.Size = new System.Drawing.Size(1016, 716);
            this.ly_purchase_contract_inspectionRepDataGridView.TabIndex = 2;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(227, 2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(82, 21);
            this.button1.TabIndex = 37;
            this.button1.Text = "上传表格";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // tableAdapterManager
            // 
            this.tableAdapterManager.BackupDataSetBeforeUpdate = false;
            this.tableAdapterManager.Connection = null;
            this.tableAdapterManager.ly_machinepart_process_forremakeTableAdapter = null;
            this.tableAdapterManager.ly_machinepart_process_sum_forremakeOrderTableAdapter = null;
            this.tableAdapterManager.ly_production_order_inspectionAll_MainTableAdapter = null;
            this.tableAdapterManager.ly_production_order_inspectionAllTableAdapter = null;
            this.tableAdapterManager.ly_production_order_inspectionRepTableAdapter = null;
            this.tableAdapterManager.ly_production_order_inspectionTableAdapter = null;
            this.tableAdapterManager.ly_production_order_remake_detailTableAdapter = null;
            this.tableAdapterManager.ly_production_order_remakeMenage1TableAdapter = null;
            this.tableAdapterManager.ly_production_order_remakeMenageTableAdapter = null;
            this.tableAdapterManager.ly_production_order_remakeTableAdapter = null;
            this.tableAdapterManager.ly_production_recordsTableAdapter = null;
            this.tableAdapterManager.ly_production_task_requestSingleTableAdapter = null;
            this.tableAdapterManager.LY_productionorder_list_foritemnoTableAdapter = null;
            this.tableAdapterManager.LY_productionorder_listTableAdapter = null;
            this.tableAdapterManager.LY_productionorder_qualityTableAdapter = null;
            this.tableAdapterManager.ly_purchase_contract_inspectionsingleTableAdapter = null;
            this.tableAdapterManager.ly_purchase_contract_inspectionTableAdapter = null;
            this.tableAdapterManager.ly_store_in_JGTableAdapter = null;
            this.tableAdapterManager.ly_store_in_WXTableAdapter = null;
            this.tableAdapterManager.ly_store_inTableAdapter = null;
            this.tableAdapterManager.t_outsource_approveTableAdapter = null;
            this.tableAdapterManager.UpdateOrder = HappyYF.YuanXin.Data.LYQualityInspectorTableAdapters.TableAdapterManager.UpdateOrderOption.InsertUpdateDelete;
            // 
            // sales_excelTableAdapter
            // 
            this.sales_excelTableAdapter.ClearBeforeFill = true;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.删除该文件ToolStripMenuItem,
            this.下载文件ToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(119, 48);
            // 
            // 删除该文件ToolStripMenuItem
            // 
            this.删除该文件ToolStripMenuItem.Name = "删除该文件ToolStripMenuItem";
            this.删除该文件ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.删除该文件ToolStripMenuItem.Text = "删除文件";
            this.删除该文件ToolStripMenuItem.Click += new System.EventHandler(this.删除该文件ToolStripMenuItem_Click);
            // 
            // 下载文件ToolStripMenuItem
            // 
            this.下载文件ToolStripMenuItem.Name = "下载文件ToolStripMenuItem";
            this.下载文件ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.下载文件ToolStripMenuItem.Text = "下载文件";
            this.下载文件ToolStripMenuItem.Click += new System.EventHandler(this.下载文件ToolStripMenuItem_Click);
            // 
            // id
            // 
            this.id.DataPropertyName = "id";
            this.id.HeaderText = "id";
            this.id.Name = "id";
            this.id.ReadOnly = true;
            this.id.Visible = false;
            // 
            // filenameDataGridViewTextBoxColumn
            // 
            this.filenameDataGridViewTextBoxColumn.DataPropertyName = "file_name";
            this.filenameDataGridViewTextBoxColumn.HeaderText = "名称";
            this.filenameDataGridViewTextBoxColumn.Name = "filenameDataGridViewTextBoxColumn";
            this.filenameDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // filepath
            // 
            this.filepath.DataPropertyName = "file_path";
            this.filepath.HeaderText = "地址";
            this.filepath.Name = "filepath";
            this.filepath.ReadOnly = true;
            // 
            // rececodeDataGridViewTextBoxColumn
            // 
            this.rececodeDataGridViewTextBoxColumn.DataPropertyName = "rece_code";
            this.rececodeDataGridViewTextBoxColumn.HeaderText = "收件号";
            this.rececodeDataGridViewTextBoxColumn.Name = "rececodeDataGridViewTextBoxColumn";
            this.rececodeDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // savatimeDataGridViewTextBoxColumn
            // 
            this.savatimeDataGridViewTextBoxColumn.DataPropertyName = "sava_time";
            this.savatimeDataGridViewTextBoxColumn.HeaderText = "上传时间";
            this.savatimeDataGridViewTextBoxColumn.Name = "savatimeDataGridViewTextBoxColumn";
            this.savatimeDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // savepeo
            // 
            this.savepeo.DataPropertyName = "save_peo";
            this.savepeo.HeaderText = "上传人";
            this.savepeo.Name = "savepeo";
            this.savepeo.ReadOnly = true;
            // 
            // LY_WxExcelRep
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1016, 741);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.ly_purchase_contract_inspectionRepDataGridView);
            this.Controls.Add(this.ly_purchase_contract_inspectionRepBindingNavigator);
            this.Name = "LY_WxExcelRep";
            this.Text = "自定义表格上传";
            this.Load += new System.EventHandler(this.LY_Quality_Control_PurchaseRep_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ly_purchase_contract_inspectionRepBindingNavigator)).EndInit();
            this.ly_purchase_contract_inspectionRepBindingNavigator.ResumeLayout(false);
            this.ly_purchase_contract_inspectionRepBindingNavigator.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.salesexcelBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lYSalseMange2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ly_purchase_contract_inspectionRepDataGridView)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private Data.LYQualityInspectorTableAdapters.TableAdapterManager tableAdapterManager;
        private System.Windows.Forms.BindingNavigator ly_purchase_contract_inspectionRepBindingNavigator;
        private System.Windows.Forms.ToolStripLabel bindingNavigatorCountItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveFirstItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMovePreviousItem;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator;
        private System.Windows.Forms.ToolStripTextBox bindingNavigatorPositionItem;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator1;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveNextItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveLastItem;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator2;
        private System.Windows.Forms.DataGridView ly_purchase_contract_inspectionRepDataGridView;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.BindingSource salesexcelBindingSource;
        private Data.LYSalseMange2 lYSalseMange2;
        private Data.LYSalseMange2TableAdapters.sales_excelTableAdapter sales_excelTableAdapter;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 删除该文件ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 下载文件ToolStripMenuItem;
        private System.Windows.Forms.DataGridViewTextBoxColumn id;
        private System.Windows.Forms.DataGridViewTextBoxColumn filenameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn filepath;
        private System.Windows.Forms.DataGridViewTextBoxColumn rececodeDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn savatimeDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn savepeo;
    }
}