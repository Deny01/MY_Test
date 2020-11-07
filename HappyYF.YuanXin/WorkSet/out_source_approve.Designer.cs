namespace HappyYF.YuanXin.WorkSet
{
    partial class out_source_approve
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(out_source_approve));
            this.ly_purchase_contract_inspectionRepBindingNavigator = new System.Windows.Forms.BindingNavigator(this.components);
            this.toutsourceapproveBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.lYQualityInspector = new HappyYF.YuanXin.Data.LYQualityInspector();
            this.bindingNavigatorCountItem = new System.Windows.Forms.ToolStripLabel();
            this.toolStripTextBox3 = new System.Windows.Forms.ToolStripTextBox();
            this.bindingNavigatorMoveFirstItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorMovePreviousItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.bindingNavigatorPositionItem = new System.Windows.Forms.ToolStripTextBox();
            this.bindingNavigatorSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.bindingNavigatorMoveNextItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorMoveLastItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.ly_purchase_contract_inspectionRepDataGridView = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.button1 = new System.Windows.Forms.Button();
            this.dateTimePicker2 = new System.Windows.Forms.DateTimePicker();
            this.tableAdapterManager = new HappyYF.YuanXin.Data.LYQualityInspectorTableAdapters.TableAdapterManager();
            this.t_outsource_approveTableAdapter = new HappyYF.YuanXin.Data.LYQualityInspectorTableAdapters.t_outsource_approveTableAdapter();
            this.idDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.contractCodeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.itemNoDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.mchDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.xhcDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ggDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dwDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.approveCountDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.审核 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.审核人 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.审核时间 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.saveTimeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.savePeoDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.入库 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.ly_purchase_contract_inspectionRepBindingNavigator)).BeginInit();
            this.ly_purchase_contract_inspectionRepBindingNavigator.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.toutsourceapproveBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lYQualityInspector)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ly_purchase_contract_inspectionRepDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // ly_purchase_contract_inspectionRepBindingNavigator
            // 
            this.ly_purchase_contract_inspectionRepBindingNavigator.AddNewItem = null;
            this.ly_purchase_contract_inspectionRepBindingNavigator.BindingSource = this.toutsourceapproveBindingSource;
            this.ly_purchase_contract_inspectionRepBindingNavigator.CountItem = this.bindingNavigatorCountItem;
            this.ly_purchase_contract_inspectionRepBindingNavigator.DeleteItem = null;
            this.ly_purchase_contract_inspectionRepBindingNavigator.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripTextBox3,
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
            // toutsourceapproveBindingSource
            // 
            this.toutsourceapproveBindingSource.DataMember = "t_outsource_approve";
            this.toutsourceapproveBindingSource.DataSource = this.lYQualityInspector;
            // 
            // lYQualityInspector
            // 
            this.lYQualityInspector.DataSetName = "LYQualityInspector";
            this.lYQualityInspector.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // bindingNavigatorCountItem
            // 
            this.bindingNavigatorCountItem.Name = "bindingNavigatorCountItem";
            this.bindingNavigatorCountItem.Size = new System.Drawing.Size(35, 22);
            this.bindingNavigatorCountItem.Text = "/ {0}";
            this.bindingNavigatorCountItem.ToolTipText = "总项数";
            // 
            // toolStripTextBox3
            // 
            this.toolStripTextBox3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.toolStripTextBox3.Name = "toolStripTextBox3";
            this.toolStripTextBox3.Size = new System.Drawing.Size(100, 25);
            this.toolStripTextBox3.Enter += new System.EventHandler(this.toolStripTextBox3_Enter);
            this.toolStripTextBox3.KeyUp += new System.Windows.Forms.KeyEventHandler(this.toolStripTextBox3_KeyUp);
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
            this.idDataGridViewTextBoxColumn,
            this.contractCodeDataGridViewTextBoxColumn,
            this.itemNoDataGridViewTextBoxColumn,
            this.mchDataGridViewTextBoxColumn,
            this.xhcDataGridViewTextBoxColumn,
            this.ggDataGridViewTextBoxColumn,
            this.dwDataGridViewTextBoxColumn,
            this.approveCountDataGridViewTextBoxColumn,
            this.审核,
            this.审核人,
            this.审核时间,
            this.saveTimeDataGridViewTextBoxColumn,
            this.savePeoDataGridViewTextBoxColumn,
            this.入库});
            this.ly_purchase_contract_inspectionRepDataGridView.DataSource = this.toutsourceapproveBindingSource;
            this.ly_purchase_contract_inspectionRepDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ly_purchase_contract_inspectionRepDataGridView.Location = new System.Drawing.Point(0, 25);
            this.ly_purchase_contract_inspectionRepDataGridView.Name = "ly_purchase_contract_inspectionRepDataGridView";
            this.ly_purchase_contract_inspectionRepDataGridView.ReadOnly = true;
            this.ly_purchase_contract_inspectionRepDataGridView.RowHeadersWidth = 19;
            this.ly_purchase_contract_inspectionRepDataGridView.RowTemplate.Height = 23;
            this.ly_purchase_contract_inspectionRepDataGridView.Size = new System.Drawing.Size(1016, 716);
            this.ly_purchase_contract_inspectionRepDataGridView.TabIndex = 2;
            this.ly_purchase_contract_inspectionRepDataGridView.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.ly_purchase_contract_inspectionRepDataGridView_CellMouseDoubleClick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(335, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(23, 12);
            this.label1.TabIndex = 38;
            this.label1.Text = "从:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(460, 8);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(23, 12);
            this.label2.TabIndex = 39;
            this.label2.Text = "到:";
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.CustomFormat = "yyyy-MM-dd";
            this.dateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePicker1.Location = new System.Drawing.Point(364, 4);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(90, 21);
            this.dateTimePicker1.TabIndex = 35;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(587, 2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(56, 21);
            this.button1.TabIndex = 37;
            this.button1.Text = "检 索";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // dateTimePicker2
            // 
            this.dateTimePicker2.CustomFormat = "yyyy-MM-dd";
            this.dateTimePicker2.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePicker2.Location = new System.Drawing.Point(489, 3);
            this.dateTimePicker2.Name = "dateTimePicker2";
            this.dateTimePicker2.Size = new System.Drawing.Size(92, 21);
            this.dateTimePicker2.TabIndex = 36;
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
            // t_outsource_approveTableAdapter
            // 
            this.t_outsource_approveTableAdapter.ClearBeforeFill = true;
            // 
            // idDataGridViewTextBoxColumn
            // 
            this.idDataGridViewTextBoxColumn.DataPropertyName = "Id";
            this.idDataGridViewTextBoxColumn.HeaderText = "Id";
            this.idDataGridViewTextBoxColumn.Name = "idDataGridViewTextBoxColumn";
            this.idDataGridViewTextBoxColumn.ReadOnly = true;
            this.idDataGridViewTextBoxColumn.Visible = false;
            // 
            // contractCodeDataGridViewTextBoxColumn
            // 
            this.contractCodeDataGridViewTextBoxColumn.DataPropertyName = "ContractCode";
            this.contractCodeDataGridViewTextBoxColumn.HeaderText = "合同号";
            this.contractCodeDataGridViewTextBoxColumn.Name = "contractCodeDataGridViewTextBoxColumn";
            this.contractCodeDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // itemNoDataGridViewTextBoxColumn
            // 
            this.itemNoDataGridViewTextBoxColumn.DataPropertyName = "ItemNo";
            this.itemNoDataGridViewTextBoxColumn.HeaderText = "物料号";
            this.itemNoDataGridViewTextBoxColumn.Name = "itemNoDataGridViewTextBoxColumn";
            this.itemNoDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // mchDataGridViewTextBoxColumn
            // 
            this.mchDataGridViewTextBoxColumn.DataPropertyName = "mch";
            this.mchDataGridViewTextBoxColumn.HeaderText = "名称";
            this.mchDataGridViewTextBoxColumn.Name = "mchDataGridViewTextBoxColumn";
            this.mchDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // xhcDataGridViewTextBoxColumn
            // 
            this.xhcDataGridViewTextBoxColumn.DataPropertyName = "xhc";
            this.xhcDataGridViewTextBoxColumn.HeaderText = "型号";
            this.xhcDataGridViewTextBoxColumn.Name = "xhcDataGridViewTextBoxColumn";
            this.xhcDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // ggDataGridViewTextBoxColumn
            // 
            this.ggDataGridViewTextBoxColumn.DataPropertyName = "gg";
            this.ggDataGridViewTextBoxColumn.HeaderText = "规格";
            this.ggDataGridViewTextBoxColumn.Name = "ggDataGridViewTextBoxColumn";
            this.ggDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // dwDataGridViewTextBoxColumn
            // 
            this.dwDataGridViewTextBoxColumn.DataPropertyName = "dw";
            this.dwDataGridViewTextBoxColumn.HeaderText = "单位";
            this.dwDataGridViewTextBoxColumn.Name = "dwDataGridViewTextBoxColumn";
            this.dwDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // approveCountDataGridViewTextBoxColumn
            // 
            this.approveCountDataGridViewTextBoxColumn.DataPropertyName = "ApproveCount";
            this.approveCountDataGridViewTextBoxColumn.HeaderText = "退料数";
            this.approveCountDataGridViewTextBoxColumn.Name = "approveCountDataGridViewTextBoxColumn";
            this.approveCountDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // 审核
            // 
            this.审核.DataPropertyName = "IsCheck";
            this.审核.HeaderText = "审核";
            this.审核.Name = "审核";
            this.审核.ReadOnly = true;
            // 
            // 审核人
            // 
            this.审核人.DataPropertyName = "CheckPeo";
            this.审核人.HeaderText = "审核人";
            this.审核人.Name = "审核人";
            this.审核人.ReadOnly = true;
            // 
            // 审核时间
            // 
            this.审核时间.DataPropertyName = "CheckTime";
            this.审核时间.HeaderText = "审核时间";
            this.审核时间.Name = "审核时间";
            this.审核时间.ReadOnly = true;
            // 
            // saveTimeDataGridViewTextBoxColumn
            // 
            this.saveTimeDataGridViewTextBoxColumn.DataPropertyName = "SaveTime";
            this.saveTimeDataGridViewTextBoxColumn.HeaderText = "提交时间";
            this.saveTimeDataGridViewTextBoxColumn.Name = "saveTimeDataGridViewTextBoxColumn";
            this.saveTimeDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // savePeoDataGridViewTextBoxColumn
            // 
            this.savePeoDataGridViewTextBoxColumn.DataPropertyName = "SavePeo";
            this.savePeoDataGridViewTextBoxColumn.HeaderText = "提交人";
            this.savePeoDataGridViewTextBoxColumn.Name = "savePeoDataGridViewTextBoxColumn";
            this.savePeoDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // 入库
            // 
            this.入库.DataPropertyName = "IsInStore";
            this.入库.HeaderText = "入库";
            this.入库.Name = "入库";
            this.入库.ReadOnly = true;
            // 
            // out_source_approve
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1016, 741);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dateTimePicker1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.dateTimePicker2);
            this.Controls.Add(this.ly_purchase_contract_inspectionRepDataGridView);
            this.Controls.Add(this.ly_purchase_contract_inspectionRepBindingNavigator);
            this.Name = "out_source_approve";
            this.Text = "外协退料审批";
            this.Load += new System.EventHandler(this.LY_Quality_Control_PurchaseRep_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ly_purchase_contract_inspectionRepBindingNavigator)).EndInit();
            this.ly_purchase_contract_inspectionRepBindingNavigator.ResumeLayout(false);
            this.ly_purchase_contract_inspectionRepBindingNavigator.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.toutsourceapproveBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lYQualityInspector)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ly_purchase_contract_inspectionRepDataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Data.LYQualityInspector lYQualityInspector;
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
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DateTimePicker dateTimePicker2;
        private System.Windows.Forms.ToolStripTextBox toolStripTextBox3;
        private System.Windows.Forms.BindingSource toutsourceapproveBindingSource;
        private Data.LYQualityInspectorTableAdapters.t_outsource_approveTableAdapter t_outsource_approveTableAdapter;
        private System.Windows.Forms.DataGridViewTextBoxColumn idDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn contractCodeDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn itemNoDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn mchDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn xhcDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn ggDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn dwDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn approveCountDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn 审核;
        private System.Windows.Forms.DataGridViewTextBoxColumn 审核人;
        private System.Windows.Forms.DataGridViewTextBoxColumn 审核时间;
        private System.Windows.Forms.DataGridViewTextBoxColumn saveTimeDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn savePeoDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn 入库;
    }
}