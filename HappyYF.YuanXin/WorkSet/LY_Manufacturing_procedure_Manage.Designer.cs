namespace HappyYF.YuanXin.WorkSet
{
    partial class LY_Manufacturing_procedure_Manage
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LY_Manufacturing_procedure_Manage));
            this.ly_manufacturing_procedureBindingNavigator = new System.Windows.Forms.BindingNavigator(this.components);
            this.bindingNavigatorAddNewItem = new System.Windows.Forms.ToolStripButton();
            this.ly_manufacturing_procedureBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.lYMaterielRequirements = new HappyYF.YuanXin.Data.LYMaterielRequirements();
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
            this.ly_manufacturing_procedureBindingNavigatorSaveItem = new System.Windows.Forms.ToolStripButton();
            this.ly_manufacturing_procedureDataGridView = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.工时因子 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.外协 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ly_manufacturing_procedureTableAdapter = new HappyYF.YuanXin.Data.LYMaterielRequirementsTableAdapters.ly_manufacturing_procedureTableAdapter();
            this.tableAdapterManager = new HappyYF.YuanXin.Data.LYMaterielRequirementsTableAdapters.TableAdapterManager();
            ((System.ComponentModel.ISupportInitialize)(this.ly_manufacturing_procedureBindingNavigator)).BeginInit();
            this.ly_manufacturing_procedureBindingNavigator.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ly_manufacturing_procedureBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lYMaterielRequirements)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ly_manufacturing_procedureDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // ly_manufacturing_procedureBindingNavigator
            // 
            this.ly_manufacturing_procedureBindingNavigator.AddNewItem = this.bindingNavigatorAddNewItem;
            this.ly_manufacturing_procedureBindingNavigator.BindingSource = this.ly_manufacturing_procedureBindingSource;
            this.ly_manufacturing_procedureBindingNavigator.CountItem = this.bindingNavigatorCountItem;
            this.ly_manufacturing_procedureBindingNavigator.DeleteItem = this.bindingNavigatorDeleteItem;
            this.ly_manufacturing_procedureBindingNavigator.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
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
            this.ly_manufacturing_procedureBindingNavigatorSaveItem});
            this.ly_manufacturing_procedureBindingNavigator.Location = new System.Drawing.Point(0, 0);
            this.ly_manufacturing_procedureBindingNavigator.MoveFirstItem = this.bindingNavigatorMoveFirstItem;
            this.ly_manufacturing_procedureBindingNavigator.MoveLastItem = this.bindingNavigatorMoveLastItem;
            this.ly_manufacturing_procedureBindingNavigator.MoveNextItem = this.bindingNavigatorMoveNextItem;
            this.ly_manufacturing_procedureBindingNavigator.MovePreviousItem = this.bindingNavigatorMovePreviousItem;
            this.ly_manufacturing_procedureBindingNavigator.Name = "ly_manufacturing_procedureBindingNavigator";
            this.ly_manufacturing_procedureBindingNavigator.PositionItem = this.bindingNavigatorPositionItem;
            this.ly_manufacturing_procedureBindingNavigator.Size = new System.Drawing.Size(699, 25);
            this.ly_manufacturing_procedureBindingNavigator.TabIndex = 0;
            this.ly_manufacturing_procedureBindingNavigator.Text = "bindingNavigator1";
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
            // ly_manufacturing_procedureBindingSource
            // 
            this.ly_manufacturing_procedureBindingSource.DataMember = "ly_manufacturing_procedure";
            this.ly_manufacturing_procedureBindingSource.DataSource = this.lYMaterielRequirements;
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
            // bindingNavigatorDeleteItem
            // 
            this.bindingNavigatorDeleteItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorDeleteItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorDeleteItem.Image")));
            this.bindingNavigatorDeleteItem.Name = "bindingNavigatorDeleteItem";
            this.bindingNavigatorDeleteItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorDeleteItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorDeleteItem.Text = "删除";
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
            // ly_manufacturing_procedureBindingNavigatorSaveItem
            // 
            this.ly_manufacturing_procedureBindingNavigatorSaveItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ly_manufacturing_procedureBindingNavigatorSaveItem.Image = ((System.Drawing.Image)(resources.GetObject("ly_manufacturing_procedureBindingNavigatorSaveItem.Image")));
            this.ly_manufacturing_procedureBindingNavigatorSaveItem.Name = "ly_manufacturing_procedureBindingNavigatorSaveItem";
            this.ly_manufacturing_procedureBindingNavigatorSaveItem.Size = new System.Drawing.Size(23, 22);
            this.ly_manufacturing_procedureBindingNavigatorSaveItem.Text = "保存数据";
            this.ly_manufacturing_procedureBindingNavigatorSaveItem.Click += new System.EventHandler(this.ly_manufacturing_procedureBindingNavigatorSaveItem_Click);
            // 
            // ly_manufacturing_procedureDataGridView
            // 
            this.ly_manufacturing_procedureDataGridView.AllowUserToAddRows = false;
            this.ly_manufacturing_procedureDataGridView.AutoGenerateColumns = false;
            this.ly_manufacturing_procedureDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.ly_manufacturing_procedureDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2,
            this.dataGridViewTextBoxColumn3,
            this.dataGridViewTextBoxColumn4,
            this.工时因子,
            this.外协,
            this.dataGridViewTextBoxColumn5,
            this.dataGridViewTextBoxColumn6,
            this.dataGridViewTextBoxColumn7});
            this.ly_manufacturing_procedureDataGridView.DataSource = this.ly_manufacturing_procedureBindingSource;
            this.ly_manufacturing_procedureDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ly_manufacturing_procedureDataGridView.Location = new System.Drawing.Point(0, 25);
            this.ly_manufacturing_procedureDataGridView.Name = "ly_manufacturing_procedureDataGridView";
            this.ly_manufacturing_procedureDataGridView.RowHeadersWidth = 19;
            this.ly_manufacturing_procedureDataGridView.RowTemplate.Height = 23;
            this.ly_manufacturing_procedureDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.ly_manufacturing_procedureDataGridView.Size = new System.Drawing.Size(699, 427);
            this.ly_manufacturing_procedureDataGridView.TabIndex = 1;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.DataPropertyName = "id";
            this.dataGridViewTextBoxColumn1.HeaderText = "id";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn1.Visible = false;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.DataPropertyName = "工序编号";
            this.dataGridViewTextBoxColumn2.HeaderText = "工序编号";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.DataPropertyName = "工序名称";
            this.dataGridViewTextBoxColumn3.HeaderText = "工序名称";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.DataPropertyName = "工时单价";
            this.dataGridViewTextBoxColumn4.HeaderText = "工时单价";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            // 
            // 工时因子
            // 
            this.工时因子.DataPropertyName = "scale";
            this.工时因子.HeaderText = "工时因子";
            this.工时因子.Name = "工时因子";
            // 
            // 外协
            // 
            this.外协.DataPropertyName = "out_flag";
            this.外协.HeaderText = "外协";
            this.外协.Name = "外协";
            this.外协.Width = 39;
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.DataPropertyName = "备注";
            this.dataGridViewTextBoxColumn5.HeaderText = "备注";
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            // 
            // dataGridViewTextBoxColumn6
            // 
            this.dataGridViewTextBoxColumn6.DataPropertyName = "operator";
            this.dataGridViewTextBoxColumn6.HeaderText = "operator";
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            this.dataGridViewTextBoxColumn6.Visible = false;
            // 
            // dataGridViewTextBoxColumn7
            // 
            this.dataGridViewTextBoxColumn7.DataPropertyName = "sys_date";
            this.dataGridViewTextBoxColumn7.HeaderText = "sys_date";
            this.dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
            this.dataGridViewTextBoxColumn7.Visible = false;
            // 
            // ly_manufacturing_procedureTableAdapter
            // 
            this.ly_manufacturing_procedureTableAdapter.ClearBeforeFill = true;
            // 
            // tableAdapterManager
            // 
            this.tableAdapterManager.BackupDataSetBeforeUpdate = false;
            this.tableAdapterManager.ly_borrow_storeTableAdapter = null;
            this.tableAdapterManager.ly_company_information_purchaseTableAdapter = null;
            this.tableAdapterManager.ly_contract_terms_forpurchaseTableAdapter = null;
            this.tableAdapterManager.ly_contract_terms_forsupplierTableAdapter = null;
            this.tableAdapterManager.ly_inma0010_sortTableAdapter = null;
            //this.tableAdapterManager.ly_invoice_contrat_lockTableAdapter = null;
            this.tableAdapterManager.ly_invoiceTableAdapter = null;
            this.tableAdapterManager.ly_machinepart_process_fororderTableAdapter = null;
            this.tableAdapterManager.ly_machinepart_process_outselfmakeTableAdapter = null;
            this.tableAdapterManager.ly_machinepart_processTableAdapter = null;
            this.tableAdapterManager.ly_manufacturing_procedureTableAdapter = this.ly_manufacturing_procedureTableAdapter;
            this.tableAdapterManager.ly_material_replacelistTableAdapter = null;
            this.tableAdapterManager.ly_materiel_supplier_MOQTableAdapter = null;
            this.tableAdapterManager.ly_materiel_supplier_viewTableAdapter = null;
            this.tableAdapterManager.ly_materiel_supplierTableAdapter = null;
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
            this.tableAdapterManager.ly_worker_listTableAdapter = null;
            this.tableAdapterManager.UpdateOrder = HappyYF.YuanXin.Data.LYMaterielRequirementsTableAdapters.TableAdapterManager.UpdateOrderOption.InsertUpdateDelete;
            // 
            // LY_Manufacturing_procedure_Manage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(699, 452);
            this.Controls.Add(this.ly_manufacturing_procedureDataGridView);
            this.Controls.Add(this.ly_manufacturing_procedureBindingNavigator);
            this.Name = "LY_Manufacturing_procedure_Manage";
            this.Text = "机加工序管理";
            this.Load += new System.EventHandler(this.LY_Manufacturing_procedure_Manage_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ly_manufacturing_procedureBindingNavigator)).EndInit();
            this.ly_manufacturing_procedureBindingNavigator.ResumeLayout(false);
            this.ly_manufacturing_procedureBindingNavigator.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ly_manufacturing_procedureBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lYMaterielRequirements)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ly_manufacturing_procedureDataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private HappyYF.YuanXin.Data.LYMaterielRequirements lYMaterielRequirements;
        private System.Windows.Forms.BindingSource ly_manufacturing_procedureBindingSource;
        private HappyYF.YuanXin.Data.LYMaterielRequirementsTableAdapters.ly_manufacturing_procedureTableAdapter ly_manufacturing_procedureTableAdapter;
        private HappyYF.YuanXin.Data.LYMaterielRequirementsTableAdapters.TableAdapterManager tableAdapterManager;
        private System.Windows.Forms.BindingNavigator ly_manufacturing_procedureBindingNavigator;
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
        private System.Windows.Forms.ToolStripButton ly_manufacturing_procedureBindingNavigatorSaveItem;
        private System.Windows.Forms.DataGridView ly_manufacturing_procedureDataGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn 工时因子;
        private System.Windows.Forms.DataGridViewCheckBoxColumn 外协;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;
    }
}