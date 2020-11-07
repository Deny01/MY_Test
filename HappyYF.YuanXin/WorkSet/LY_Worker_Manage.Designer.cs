namespace HappyYF.YuanXin.WorkSet
{
    partial class LY_Worker_Manage
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LY_Worker_Manage));
            this.ly_worker_listBindingNavigator = new System.Windows.Forms.BindingNavigator(this.components);
            this.ly_worker_listBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.lYMaterielRequirements = new HappyYF.YuanXin.Data.LYMaterielRequirements();
            this.bindingNavigatorCountItem = new System.Windows.Forms.ToolStripLabel();
            this.bindingNavigatorMoveFirstItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorMovePreviousItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.bindingNavigatorPositionItem = new System.Windows.Forms.ToolStripTextBox();
            this.bindingNavigatorSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.bindingNavigatorMoveNextItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorMoveLastItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.bindingNavigatorAddNewItem = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorDeleteItem = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
            this.yX_fillCard_MoneyBindingNavigatorSaveItem = new System.Windows.Forms.ToolStripButton();
            this.ly_worker_listDataGridView = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.在职 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.部门 = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.lyproddeptBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.lYMaterialMange = new HappyYF.YuanXin.Data.LYMaterialMange();
            this.工号 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.姓名 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.性别 = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.电话 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.身份证号 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.联系地址 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ly_prod_deptTableAdapter = new HappyYF.YuanXin.Data.LYMaterialMangeTableAdapters.ly_prod_deptTableAdapter();
            this.ly_worker_listTableAdapter = new HappyYF.YuanXin.Data.LYMaterielRequirementsTableAdapters.ly_worker_listTableAdapter();
            this.tableAdapterManager = new HappyYF.YuanXin.Data.LYMaterielRequirementsTableAdapters.TableAdapterManager();
            ((System.ComponentModel.ISupportInitialize)(this.ly_worker_listBindingNavigator)).BeginInit();
            this.ly_worker_listBindingNavigator.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ly_worker_listBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lYMaterielRequirements)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ly_worker_listDataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lyproddeptBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lYMaterialMange)).BeginInit();
            this.SuspendLayout();
            // 
            // ly_worker_listBindingNavigator
            // 
            this.ly_worker_listBindingNavigator.AddNewItem = null;
            this.ly_worker_listBindingNavigator.BindingSource = this.ly_worker_listBindingSource;
            this.ly_worker_listBindingNavigator.CountItem = this.bindingNavigatorCountItem;
            this.ly_worker_listBindingNavigator.DeleteItem = null;
            this.ly_worker_listBindingNavigator.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
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
            this.toolStripButton1,
            this.bindingNavigatorDeleteItem,
            this.toolStripButton2,
            this.yX_fillCard_MoneyBindingNavigatorSaveItem});
            this.ly_worker_listBindingNavigator.Location = new System.Drawing.Point(0, 0);
            this.ly_worker_listBindingNavigator.MoveFirstItem = this.bindingNavigatorMoveFirstItem;
            this.ly_worker_listBindingNavigator.MoveLastItem = this.bindingNavigatorMoveLastItem;
            this.ly_worker_listBindingNavigator.MoveNextItem = this.bindingNavigatorMoveNextItem;
            this.ly_worker_listBindingNavigator.MovePreviousItem = this.bindingNavigatorMovePreviousItem;
            this.ly_worker_listBindingNavigator.Name = "ly_worker_listBindingNavigator";
            this.ly_worker_listBindingNavigator.PositionItem = this.bindingNavigatorPositionItem;
            this.ly_worker_listBindingNavigator.Size = new System.Drawing.Size(1016, 25);
            this.ly_worker_listBindingNavigator.TabIndex = 0;
            this.ly_worker_listBindingNavigator.Text = "bindingNavigator1";
            // 
            // ly_worker_listBindingSource
            // 
            this.ly_worker_listBindingSource.DataMember = "ly_worker_list";
            this.ly_worker_listBindingSource.DataSource = this.lYMaterielRequirements;
            // 
            // lYMaterielRequirements
            // 
            this.lYMaterielRequirements.DataSetName = "LYMaterielRequirements";
            this.lYMaterielRequirements.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
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
            // bindingNavigatorAddNewItem
            // 
            this.bindingNavigatorAddNewItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorAddNewItem.Image")));
            this.bindingNavigatorAddNewItem.Name = "bindingNavigatorAddNewItem";
            this.bindingNavigatorAddNewItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorAddNewItem.Size = new System.Drawing.Size(49, 22);
            this.bindingNavigatorAddNewItem.Text = "增加";
            this.bindingNavigatorAddNewItem.Click += new System.EventHandler(this.bindingNavigatorAddNewItem_Click);
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(49, 22);
            this.toolStripButton1.Text = "修改";
            this.toolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // bindingNavigatorDeleteItem
            // 
            this.bindingNavigatorDeleteItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorDeleteItem.Image")));
            this.bindingNavigatorDeleteItem.Name = "bindingNavigatorDeleteItem";
            this.bindingNavigatorDeleteItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorDeleteItem.Size = new System.Drawing.Size(49, 22);
            this.bindingNavigatorDeleteItem.Text = "删除";
            this.bindingNavigatorDeleteItem.Click += new System.EventHandler(this.bindingNavigatorDeleteItem_Click);
            // 
            // toolStripButton2
            // 
            this.toolStripButton2.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton2.Image")));
            this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton2.Name = "toolStripButton2";
            this.toolStripButton2.Size = new System.Drawing.Size(49, 22);
            this.toolStripButton2.Text = "取消";
            this.toolStripButton2.Click += new System.EventHandler(this.toolStripButton2_Click);
            // 
            // yX_fillCard_MoneyBindingNavigatorSaveItem
            // 
            this.yX_fillCard_MoneyBindingNavigatorSaveItem.Image = ((System.Drawing.Image)(resources.GetObject("yX_fillCard_MoneyBindingNavigatorSaveItem.Image")));
            this.yX_fillCard_MoneyBindingNavigatorSaveItem.Name = "yX_fillCard_MoneyBindingNavigatorSaveItem";
            this.yX_fillCard_MoneyBindingNavigatorSaveItem.Size = new System.Drawing.Size(49, 22);
            this.yX_fillCard_MoneyBindingNavigatorSaveItem.Text = "保存";
            this.yX_fillCard_MoneyBindingNavigatorSaveItem.Click += new System.EventHandler(this.yX_fillCard_MoneyBindingNavigatorSaveItem_Click);
            // 
            // ly_worker_listDataGridView
            // 
            this.ly_worker_listDataGridView.AllowUserToAddRows = false;
            this.ly_worker_listDataGridView.AutoGenerateColumns = false;
            this.ly_worker_listDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.ly_worker_listDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.在职,
            this.部门,
            this.工号,
            this.姓名,
            this.性别,
            this.电话,
            this.身份证号,
            this.联系地址});
            this.ly_worker_listDataGridView.DataSource = this.ly_worker_listBindingSource;
            this.ly_worker_listDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ly_worker_listDataGridView.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.ly_worker_listDataGridView.Location = new System.Drawing.Point(0, 25);
            this.ly_worker_listDataGridView.Name = "ly_worker_listDataGridView";
            this.ly_worker_listDataGridView.RowHeadersWidth = 19;
            this.ly_worker_listDataGridView.RowTemplate.Height = 23;
            this.ly_worker_listDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.ly_worker_listDataGridView.Size = new System.Drawing.Size(1016, 716);
            this.ly_worker_listDataGridView.TabIndex = 1;
            this.ly_worker_listDataGridView.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.ly_worker_listDataGridView_CellEnter);
            this.ly_worker_listDataGridView.CellLeave += new System.Windows.Forms.DataGridViewCellEventHandler(this.ly_worker_listDataGridView_CellLeave);
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.DataPropertyName = "id";
            this.dataGridViewTextBoxColumn1.HeaderText = "id";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn1.Visible = false;
            // 
            // 在职
            // 
            this.在职.DataPropertyName = "在职";
            this.在职.HeaderText = "在职";
            this.在职.Name = "在职";
            this.在职.Width = 39;
            // 
            // 部门
            // 
            this.部门.DataPropertyName = "部门";
            this.部门.DataSource = this.lyproddeptBindingSource;
            this.部门.DisplayMember = "prodname";
            this.部门.HeaderText = "部门";
            this.部门.Name = "部门";
            this.部门.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.部门.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.部门.ValueMember = "prodcode";
            // 
            // lyproddeptBindingSource
            // 
            this.lyproddeptBindingSource.DataMember = "ly_prod_dept";
            this.lyproddeptBindingSource.DataSource = this.lYMaterialMange;
            // 
            // lYMaterialMange
            // 
            this.lYMaterialMange.DataSetName = "LYMaterialMange";
            this.lYMaterialMange.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // 工号
            // 
            this.工号.DataPropertyName = "工号";
            this.工号.HeaderText = "工号";
            this.工号.Name = "工号";
            this.工号.Width = 60;
            // 
            // 姓名
            // 
            this.姓名.DataPropertyName = "姓名";
            this.姓名.HeaderText = "姓名";
            this.姓名.Name = "姓名";
            this.姓名.Width = 80;
            // 
            // 性别
            // 
            this.性别.DataPropertyName = "性别";
            this.性别.HeaderText = "性别";
            this.性别.Items.AddRange(new object[] {
            "男",
            "女"});
            this.性别.Name = "性别";
            this.性别.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.性别.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.性别.Width = 50;
            // 
            // 电话
            // 
            this.电话.DataPropertyName = "电话";
            this.电话.HeaderText = "电话";
            this.电话.Name = "电话";
            this.电话.Width = 120;
            // 
            // 身份证号
            // 
            this.身份证号.DataPropertyName = "身份证号";
            this.身份证号.HeaderText = "身份证号";
            this.身份证号.Name = "身份证号";
            this.身份证号.Width = 120;
            // 
            // 联系地址
            // 
            this.联系地址.DataPropertyName = "联系地址";
            this.联系地址.HeaderText = "联系地址";
            this.联系地址.Name = "联系地址";
            this.联系地址.Width = 300;
            // 
            // ly_prod_deptTableAdapter
            // 
            this.ly_prod_deptTableAdapter.ClearBeforeFill = true;
            // 
            // ly_worker_listTableAdapter
            // 
            this.ly_worker_listTableAdapter.ClearBeforeFill = true;
            // 
            // tableAdapterManager
            // 
            this.tableAdapterManager.BackupDataSetBeforeUpdate = false;
            this.tableAdapterManager.ly_borrow_storeTableAdapter = null;
            this.tableAdapterManager.ly_company_information_purchaseTableAdapter = null;
            this.tableAdapterManager.ly_contract_terms_forpurchaseTableAdapter = null;
            this.tableAdapterManager.ly_contract_terms_forsupplierTableAdapter = null;
            this.tableAdapterManager.ly_inma0010_sortTableAdapter = null;
            this.tableAdapterManager.ly_invoice_contrat_lock_outsourceTableAdapter = null;
            this.tableAdapterManager.ly_invoice_contrat_lockTableAdapter = null;
            this.tableAdapterManager.ly_invoiceTableAdapter = null;
            this.tableAdapterManager.ly_machinepart_process_fororderTableAdapter = null;
            this.tableAdapterManager.ly_machinepart_process_outselfmakeTableAdapter = null;
            this.tableAdapterManager.ly_machinepart_processTableAdapter = null;
            this.tableAdapterManager.ly_manufacturing_procedureTableAdapter = null;
            this.tableAdapterManager.ly_material_replacelistTableAdapter = null;
            this.tableAdapterManager.ly_materiel_supplier_MOQTableAdapter = null;
            this.tableAdapterManager.ly_materiel_supplier_viewTableAdapter = null;
            this.tableAdapterManager.ly_materiel_supplierTableAdapter = null;
            this.tableAdapterManager.ly_outsourcepart_process_sumTableAdapter = null;
            this.tableAdapterManager.ly_PrepaymentTableAdapter = null;
            this.tableAdapterManager.ly_production_order_detail1TableAdapter = null;
            this.tableAdapterManager.ly_production_order_detailTableAdapter = null;
            this.tableAdapterManager.ly_production_order_materialrequisitionTableAdapter = null;
            this.tableAdapterManager.ly_production_orderTableAdapter = null;
            this.tableAdapterManager.ly_purchase_contract_appendTableAdapter = null;
            this.tableAdapterManager.ly_purchase_contract_detail_modificationTableAdapter = null;
            this.tableAdapterManager.ly_purchase_contract_detailQCTableAdapter = null;
            this.tableAdapterManager.ly_purchase_contract_detailTableAdapter = null;
            this.tableAdapterManager.ly_purchase_contract_main11TableAdapter = null;
            this.tableAdapterManager.ly_purchase_contract_main1TableAdapter = null;
            this.tableAdapterManager.ly_purchase_contract_mainTableAdapter = null;
            this.tableAdapterManager.ly_purchase_partTableAdapter = null;
            this.tableAdapterManager.ly_purchase_prepareforplanTableAdapter = null;
            this.tableAdapterManager.ly_Restructuring_requestTableAdapter = null;
            this.tableAdapterManager.ly_sales_contract_terms_machineTableAdapter = null;
            this.tableAdapterManager.ly_sales_contract_terms_outsourceTableAdapter = null;
            this.tableAdapterManager.ly_sales_contract_terms_purchaseTableAdapter = null;
            this.tableAdapterManager.ly_store_out_JGTableAdapter = null;
            this.tableAdapterManager.ly_supplier_listTableAdapter = null;
            this.tableAdapterManager.ly_worker_listDZTableAdapter = null;
            this.tableAdapterManager.ly_worker_listQZTableAdapter = null;
            this.tableAdapterManager.ly_worker_listTableAdapter = this.ly_worker_listTableAdapter;
            this.tableAdapterManager.UpdateOrder = HappyYF.YuanXin.Data.LYMaterielRequirementsTableAdapters.TableAdapterManager.UpdateOrderOption.InsertUpdateDelete;
            // 
            // LY_Worker_Manage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1016, 741);
            this.Controls.Add(this.ly_worker_listDataGridView);
            this.Controls.Add(this.ly_worker_listBindingNavigator);
            this.Name = "LY_Worker_Manage";
            this.Text = "人员管理";
            this.Load += new System.EventHandler(this.LY_Worker_Manage_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ly_worker_listBindingNavigator)).EndInit();
            this.ly_worker_listBindingNavigator.ResumeLayout(false);
            this.ly_worker_listBindingNavigator.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ly_worker_listBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lYMaterielRequirements)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ly_worker_listDataGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lyproddeptBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lYMaterialMange)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private HappyYF.YuanXin.Data.LYMaterielRequirements lYMaterielRequirements;
        private System.Windows.Forms.BindingSource ly_worker_listBindingSource;
        private HappyYF.YuanXin.Data.LYMaterielRequirementsTableAdapters.ly_worker_listTableAdapter ly_worker_listTableAdapter;
        private HappyYF.YuanXin.Data.LYMaterielRequirementsTableAdapters.TableAdapterManager tableAdapterManager;
        private System.Windows.Forms.BindingNavigator ly_worker_listBindingNavigator;
        private System.Windows.Forms.ToolStripLabel bindingNavigatorCountItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveFirstItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMovePreviousItem;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator;
        private System.Windows.Forms.ToolStripTextBox bindingNavigatorPositionItem;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator1;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveNextItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveLastItem;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator2;
        private System.Windows.Forms.DataGridView ly_worker_listDataGridView;
        private HappyYF.YuanXin.Data.LYMaterialMange lYMaterialMange;
        private System.Windows.Forms.BindingSource lyproddeptBindingSource;
        private HappyYF.YuanXin.Data.LYMaterialMangeTableAdapters.ly_prod_deptTableAdapter ly_prod_deptTableAdapter;
        private System.Windows.Forms.ToolStripButton bindingNavigatorAddNewItem;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.ToolStripButton bindingNavigatorDeleteItem;
        private System.Windows.Forms.ToolStripButton toolStripButton2;
        private System.Windows.Forms.ToolStripButton yX_fillCard_MoneyBindingNavigatorSaveItem;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewCheckBoxColumn 在职;
        private System.Windows.Forms.DataGridViewComboBoxColumn 部门;
        private System.Windows.Forms.DataGridViewTextBoxColumn 工号;
        private System.Windows.Forms.DataGridViewTextBoxColumn 姓名;
        private System.Windows.Forms.DataGridViewComboBoxColumn 性别;
        private System.Windows.Forms.DataGridViewTextBoxColumn 电话;
        private System.Windows.Forms.DataGridViewTextBoxColumn 身份证号;
        private System.Windows.Forms.DataGridViewTextBoxColumn 联系地址;
    }
}