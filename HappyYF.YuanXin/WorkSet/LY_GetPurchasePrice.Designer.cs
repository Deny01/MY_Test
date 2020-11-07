namespace HappyYF.YuanXin.WorkSet
{
    partial class LY_GetPurchasePrice
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LY_GetPurchasePrice));
            this.lYQualityInspector = new HappyYF.YuanXin.Data.LYQualityInspector();
            this.tableAdapterManager = new HappyYF.YuanXin.Data.LYQualityInspectorTableAdapters.TableAdapterManager();
            this.ly_purchase_contract_inspectionRepBindingNavigator = new System.Windows.Forms.BindingNavigator(this.components);
            this.bindingNavigatorCountItem = new System.Windows.Forms.ToolStripLabel();
            this.bindingNavigatorMoveFirstItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorMovePreviousItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.bindingNavigatorPositionItem = new System.Windows.Forms.ToolStripTextBox();
            this.bindingNavigatorSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.bindingNavigatorMoveNextItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorMoveLastItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton10 = new System.Windows.Forms.ToolStripButton();
            this.ly_purchase_contract_inspectionRepDataGridView = new System.Windows.Forms.DataGridView();
            this.contract_code = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.contract_price = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tax_rate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.contract_price_novat = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.supplier_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.contract_inspection_code = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.quality_inspector = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.itemno = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.mch = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.xhc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gg = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.purchase_qty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.in_qty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.qualified_count = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.canuse_count = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.waste_count = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dw = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.idDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.getpurchasePriceBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.get_purchasePriceTableAdapter = new HappyYF.YuanXin.Data.LYQualityInspectorTableAdapters.Get_purchasePriceTableAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.lYQualityInspector)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ly_purchase_contract_inspectionRepBindingNavigator)).BeginInit();
            this.ly_purchase_contract_inspectionRepBindingNavigator.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ly_purchase_contract_inspectionRepDataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.getpurchasePriceBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // lYQualityInspector
            // 
            this.lYQualityInspector.DataSetName = "LYQualityInspector";
            this.lYQualityInspector.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
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
            this.tableAdapterManager.UpdateOrder = HappyYF.YuanXin.Data.LYQualityInspectorTableAdapters.TableAdapterManager.UpdateOrderOption.InsertUpdateDelete;
            // 
            // ly_purchase_contract_inspectionRepBindingNavigator
            // 
            this.ly_purchase_contract_inspectionRepBindingNavigator.AddNewItem = null;
            this.ly_purchase_contract_inspectionRepBindingNavigator.BindingSource = this.getpurchasePriceBindingSource;
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
            this.bindingNavigatorSeparator2,
            this.toolStripButton10});
            this.ly_purchase_contract_inspectionRepBindingNavigator.Location = new System.Drawing.Point(0, 0);
            this.ly_purchase_contract_inspectionRepBindingNavigator.MoveFirstItem = this.bindingNavigatorMoveFirstItem;
            this.ly_purchase_contract_inspectionRepBindingNavigator.MoveLastItem = this.bindingNavigatorMoveLastItem;
            this.ly_purchase_contract_inspectionRepBindingNavigator.MoveNextItem = this.bindingNavigatorMoveNextItem;
            this.ly_purchase_contract_inspectionRepBindingNavigator.MovePreviousItem = this.bindingNavigatorMovePreviousItem;
            this.ly_purchase_contract_inspectionRepBindingNavigator.Name = "ly_purchase_contract_inspectionRepBindingNavigator";
            this.ly_purchase_contract_inspectionRepBindingNavigator.PositionItem = this.bindingNavigatorPositionItem;
            this.ly_purchase_contract_inspectionRepBindingNavigator.Size = new System.Drawing.Size(878, 25);
            this.ly_purchase_contract_inspectionRepBindingNavigator.TabIndex = 0;
            this.ly_purchase_contract_inspectionRepBindingNavigator.Text = "bindingNavigator1";
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
            // toolStripButton10
            // 
            this.toolStripButton10.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton10.Name = "toolStripButton10";
            this.toolStripButton10.Size = new System.Drawing.Size(45, 22);
            this.toolStripButton10.Text = "EXCELL";
            this.toolStripButton10.Click += new System.EventHandler(this.toolStripButton10_Click);
            // 
            // ly_purchase_contract_inspectionRepDataGridView
            // 
            this.ly_purchase_contract_inspectionRepDataGridView.AllowUserToAddRows = false;
            this.ly_purchase_contract_inspectionRepDataGridView.AllowUserToDeleteRows = false;
            this.ly_purchase_contract_inspectionRepDataGridView.AutoGenerateColumns = false;
            this.ly_purchase_contract_inspectionRepDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.ly_purchase_contract_inspectionRepDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.contract_code,
            this.contract_price,
            this.tax_rate,
            this.contract_price_novat,
            this.supplier_name,
            this.contract_inspection_code,
            this.quality_inspector,
            this.itemno,
            this.mch,
            this.xhc,
            this.gg,
            this.purchase_qty,
            this.in_qty,
            this.qualified_count,
            this.canuse_count,
            this.waste_count,
            this.dw,
            this.idDataGridViewTextBoxColumn});
            this.ly_purchase_contract_inspectionRepDataGridView.DataSource = this.getpurchasePriceBindingSource;
            this.ly_purchase_contract_inspectionRepDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ly_purchase_contract_inspectionRepDataGridView.Location = new System.Drawing.Point(0, 25);
            this.ly_purchase_contract_inspectionRepDataGridView.Name = "ly_purchase_contract_inspectionRepDataGridView";
            this.ly_purchase_contract_inspectionRepDataGridView.ReadOnly = true;
            this.ly_purchase_contract_inspectionRepDataGridView.RowHeadersWidth = 19;
            this.ly_purchase_contract_inspectionRepDataGridView.RowTemplate.Height = 23;
            this.ly_purchase_contract_inspectionRepDataGridView.Size = new System.Drawing.Size(878, 484);
            this.ly_purchase_contract_inspectionRepDataGridView.TabIndex = 2;
            // 
            // contract_code
            // 
            this.contract_code.DataPropertyName = "contract_code";
            this.contract_code.HeaderText = "合同编号";
            this.contract_code.Name = "contract_code";
            this.contract_code.ReadOnly = true;
            // 
            // contract_price
            // 
            this.contract_price.DataPropertyName = "contract_price";
            this.contract_price.HeaderText = "合同单价";
            this.contract_price.Name = "contract_price";
            this.contract_price.ReadOnly = true;
            this.contract_price.Width = 75;
            // 
            // tax_rate
            // 
            this.tax_rate.DataPropertyName = "tax_rate";
            this.tax_rate.HeaderText = "税率";
            this.tax_rate.Name = "tax_rate";
            this.tax_rate.ReadOnly = true;
            this.tax_rate.Width = 65;
            // 
            // contract_price_novat
            // 
            this.contract_price_novat.DataPropertyName = "contract_price_novat";
            this.contract_price_novat.HeaderText = "税后价格";
            this.contract_price_novat.Name = "contract_price_novat";
            this.contract_price_novat.ReadOnly = true;
            this.contract_price_novat.Width = 80;
            // 
            // supplier_name
            // 
            this.supplier_name.DataPropertyName = "supplier_name";
            this.supplier_name.HeaderText = "供应商";
            this.supplier_name.Name = "supplier_name";
            this.supplier_name.ReadOnly = true;
            // 
            // contract_inspection_code
            // 
            this.contract_inspection_code.DataPropertyName = "contract_inspection_code";
            this.contract_inspection_code.HeaderText = "质检单号";
            this.contract_inspection_code.Name = "contract_inspection_code";
            this.contract_inspection_code.ReadOnly = true;
            // 
            // quality_inspector
            // 
            this.quality_inspector.DataPropertyName = "quality_inspector";
            this.quality_inspector.HeaderText = "质检员";
            this.quality_inspector.Name = "quality_inspector";
            this.quality_inspector.ReadOnly = true;
            // 
            // itemno
            // 
            this.itemno.DataPropertyName = "itemno";
            this.itemno.HeaderText = "物料编号";
            this.itemno.Name = "itemno";
            this.itemno.ReadOnly = true;
            // 
            // mch
            // 
            this.mch.DataPropertyName = "mch";
            this.mch.HeaderText = "物料名称";
            this.mch.Name = "mch";
            this.mch.ReadOnly = true;
            // 
            // xhc
            // 
            this.xhc.DataPropertyName = "xhc";
            this.xhc.HeaderText = "型号";
            this.xhc.Name = "xhc";
            this.xhc.ReadOnly = true;
            // 
            // gg
            // 
            this.gg.DataPropertyName = "gg";
            this.gg.HeaderText = "规格";
            this.gg.Name = "gg";
            this.gg.ReadOnly = true;
            // 
            // purchase_qty
            // 
            this.purchase_qty.DataPropertyName = "purchase_qty";
            this.purchase_qty.HeaderText = "采购数量";
            this.purchase_qty.Name = "purchase_qty";
            this.purchase_qty.ReadOnly = true;
            // 
            // in_qty
            // 
            this.in_qty.DataPropertyName = "in_qty";
            this.in_qty.HeaderText = "入库数量";
            this.in_qty.Name = "in_qty";
            this.in_qty.ReadOnly = true;
            // 
            // qualified_count
            // 
            this.qualified_count.DataPropertyName = "qualified_count";
            this.qualified_count.HeaderText = "合格数量";
            this.qualified_count.Name = "qualified_count";
            this.qualified_count.ReadOnly = true;
            // 
            // canuse_count
            // 
            this.canuse_count.DataPropertyName = "canuse_count";
            this.canuse_count.HeaderText = "回用数量";
            this.canuse_count.Name = "canuse_count";
            this.canuse_count.ReadOnly = true;
            // 
            // waste_count
            // 
            this.waste_count.DataPropertyName = "waste_count";
            this.waste_count.HeaderText = "打废数量";
            this.waste_count.Name = "waste_count";
            this.waste_count.ReadOnly = true;
            // 
            // dw
            // 
            this.dw.DataPropertyName = "dw";
            this.dw.HeaderText = "单位";
            this.dw.Name = "dw";
            this.dw.ReadOnly = true;
            // 
            // idDataGridViewTextBoxColumn
            // 
            this.idDataGridViewTextBoxColumn.DataPropertyName = "id";
            this.idDataGridViewTextBoxColumn.HeaderText = "id";
            this.idDataGridViewTextBoxColumn.Name = "idDataGridViewTextBoxColumn";
            this.idDataGridViewTextBoxColumn.ReadOnly = true;
            this.idDataGridViewTextBoxColumn.Visible = false;
            // 
            // getpurchasePriceBindingSource
            // 
            this.getpurchasePriceBindingSource.DataMember = "Get_purchasePrice";
            this.getpurchasePriceBindingSource.DataSource = this.lYQualityInspector;
            // 
            // get_purchasePriceTableAdapter
            // 
            this.get_purchasePriceTableAdapter.ClearBeforeFill = true;
            // 
            // LY_GetPurchasePrice
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(878, 509);
            this.Controls.Add(this.ly_purchase_contract_inspectionRepDataGridView);
            this.Controls.Add(this.ly_purchase_contract_inspectionRepBindingNavigator);
            this.Name = "LY_GetPurchasePrice";
            this.Text = "采购合同";
            this.Load += new System.EventHandler(this.LY_Quality_Control_PurchaseRep_Load);
            ((System.ComponentModel.ISupportInitialize)(this.lYQualityInspector)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ly_purchase_contract_inspectionRepBindingNavigator)).EndInit();
            this.ly_purchase_contract_inspectionRepBindingNavigator.ResumeLayout(false);
            this.ly_purchase_contract_inspectionRepBindingNavigator.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ly_purchase_contract_inspectionRepDataGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.getpurchasePriceBindingSource)).EndInit();
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
        private System.Windows.Forms.ToolStripButton toolStripButton10;
        private System.Windows.Forms.BindingSource getpurchasePriceBindingSource;
        private Data.LYQualityInspectorTableAdapters.Get_purchasePriceTableAdapter get_purchasePriceTableAdapter;
        private System.Windows.Forms.DataGridViewTextBoxColumn contract_code;
        private System.Windows.Forms.DataGridViewTextBoxColumn contract_price;
        private System.Windows.Forms.DataGridViewTextBoxColumn tax_rate;
        private System.Windows.Forms.DataGridViewTextBoxColumn contract_price_novat;
        private System.Windows.Forms.DataGridViewTextBoxColumn supplier_name;
        private System.Windows.Forms.DataGridViewTextBoxColumn contract_inspection_code;
        private System.Windows.Forms.DataGridViewTextBoxColumn quality_inspector;
        private System.Windows.Forms.DataGridViewTextBoxColumn itemno;
        private System.Windows.Forms.DataGridViewTextBoxColumn mch;
        private System.Windows.Forms.DataGridViewTextBoxColumn xhc;
        private System.Windows.Forms.DataGridViewTextBoxColumn gg;
        private System.Windows.Forms.DataGridViewTextBoxColumn purchase_qty;
        private System.Windows.Forms.DataGridViewTextBoxColumn in_qty;
        private System.Windows.Forms.DataGridViewTextBoxColumn qualified_count;
        private System.Windows.Forms.DataGridViewTextBoxColumn canuse_count;
        private System.Windows.Forms.DataGridViewTextBoxColumn waste_count;
        private System.Windows.Forms.DataGridViewTextBoxColumn dw;
        private System.Windows.Forms.DataGridViewTextBoxColumn idDataGridViewTextBoxColumn;
    }
}