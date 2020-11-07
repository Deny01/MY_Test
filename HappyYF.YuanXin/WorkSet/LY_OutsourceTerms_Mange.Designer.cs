namespace HappyYF.YuanXin.WorkSet
{
    partial class LY_OutsourceTerms_Mange
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LY_OutsourceTerms_Mange));
            this.ly_sales_contract_termsBindingNavigator = new System.Windows.Forms.BindingNavigator(this.components);
            this.bindingNavigatorAddNewItem = new System.Windows.Forms.ToolStripButton();
            this.ly_sales_contract_terms_outsourceBindingSource = new System.Windows.Forms.BindingSource(this.components);
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
            this.ly_sales_contract_termsBindingNavigatorSaveItem = new System.Windows.Forms.ToolStripButton();
            this.ly_sales_contract_termsDataGridView = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ly_sales_contract_terms_outsourceTableAdapter = new HappyYF.YuanXin.Data.LYMaterielRequirementsTableAdapters.ly_sales_contract_terms_outsourceTableAdapter();
            this.tableAdapterManager1 = new HappyYF.YuanXin.Data.LYMaterielRequirementsTableAdapters.TableAdapterManager();
            ((System.ComponentModel.ISupportInitialize)(this.ly_sales_contract_termsBindingNavigator)).BeginInit();
            this.ly_sales_contract_termsBindingNavigator.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ly_sales_contract_terms_outsourceBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lYMaterielRequirements)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ly_sales_contract_termsDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // ly_sales_contract_termsBindingNavigator
            // 
            this.ly_sales_contract_termsBindingNavigator.AddNewItem = this.bindingNavigatorAddNewItem;
            this.ly_sales_contract_termsBindingNavigator.BindingSource = this.ly_sales_contract_terms_outsourceBindingSource;
            this.ly_sales_contract_termsBindingNavigator.CountItem = this.bindingNavigatorCountItem;
            this.ly_sales_contract_termsBindingNavigator.DeleteItem = this.bindingNavigatorDeleteItem;
            this.ly_sales_contract_termsBindingNavigator.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
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
            this.ly_sales_contract_termsBindingNavigatorSaveItem});
            this.ly_sales_contract_termsBindingNavigator.Location = new System.Drawing.Point(0, 0);
            this.ly_sales_contract_termsBindingNavigator.MoveFirstItem = this.bindingNavigatorMoveFirstItem;
            this.ly_sales_contract_termsBindingNavigator.MoveLastItem = this.bindingNavigatorMoveLastItem;
            this.ly_sales_contract_termsBindingNavigator.MoveNextItem = this.bindingNavigatorMoveNextItem;
            this.ly_sales_contract_termsBindingNavigator.MovePreviousItem = this.bindingNavigatorMovePreviousItem;
            this.ly_sales_contract_termsBindingNavigator.Name = "ly_sales_contract_termsBindingNavigator";
            this.ly_sales_contract_termsBindingNavigator.PositionItem = this.bindingNavigatorPositionItem;
            this.ly_sales_contract_termsBindingNavigator.Size = new System.Drawing.Size(895, 25);
            this.ly_sales_contract_termsBindingNavigator.TabIndex = 0;
            this.ly_sales_contract_termsBindingNavigator.Text = "bindingNavigator1";
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
            // ly_sales_contract_terms_outsourceBindingSource
            // 
            this.ly_sales_contract_terms_outsourceBindingSource.DataMember = "ly_sales_contract_terms_outsource";
            this.ly_sales_contract_terms_outsourceBindingSource.DataSource = this.lYMaterielRequirements;
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
            // ly_sales_contract_termsBindingNavigatorSaveItem
            // 
            this.ly_sales_contract_termsBindingNavigatorSaveItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ly_sales_contract_termsBindingNavigatorSaveItem.Image = ((System.Drawing.Image)(resources.GetObject("ly_sales_contract_termsBindingNavigatorSaveItem.Image")));
            this.ly_sales_contract_termsBindingNavigatorSaveItem.Name = "ly_sales_contract_termsBindingNavigatorSaveItem";
            this.ly_sales_contract_termsBindingNavigatorSaveItem.Size = new System.Drawing.Size(23, 22);
            this.ly_sales_contract_termsBindingNavigatorSaveItem.Text = "保存数据";
            this.ly_sales_contract_termsBindingNavigatorSaveItem.Click += new System.EventHandler(this.ly_sales_contract_termsBindingNavigatorSaveItem_Click_1);
            // 
            // ly_sales_contract_termsDataGridView
            // 
            this.ly_sales_contract_termsDataGridView.AllowUserToAddRows = false;
            this.ly_sales_contract_termsDataGridView.AllowUserToDeleteRows = false;
            this.ly_sales_contract_termsDataGridView.AutoGenerateColumns = false;
            this.ly_sales_contract_termsDataGridView.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells;
            this.ly_sales_contract_termsDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.ly_sales_contract_termsDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2,
            this.dataGridViewTextBoxColumn3,
            this.dataGridViewTextBoxColumn4});
            this.ly_sales_contract_termsDataGridView.DataSource = this.ly_sales_contract_terms_outsourceBindingSource;
            this.ly_sales_contract_termsDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ly_sales_contract_termsDataGridView.Location = new System.Drawing.Point(0, 25);
            this.ly_sales_contract_termsDataGridView.Name = "ly_sales_contract_termsDataGridView";
            this.ly_sales_contract_termsDataGridView.RowHeadersWidth = 19;
            this.ly_sales_contract_termsDataGridView.RowTemplate.DefaultCellStyle.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.ly_sales_contract_termsDataGridView.RowTemplate.Height = 23;
            this.ly_sales_contract_termsDataGridView.Size = new System.Drawing.Size(895, 454);
            this.ly_sales_contract_termsDataGridView.TabIndex = 1;
            this.ly_sales_contract_termsDataGridView.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.ly_sales_contract_termsDataGridView_CellContentClick);
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
            this.dataGridViewTextBoxColumn2.DataPropertyName = "编号";
            this.dataGridViewTextBoxColumn2.HeaderText = "编号";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.Width = 39;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.DataPropertyName = "条款选项";
            this.dataGridViewTextBoxColumn3.HeaderText = "条款选项";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.Width = 300;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.DataPropertyName = "条款描述";
            this.dataGridViewTextBoxColumn4.HeaderText = "条款描述";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.Width = 500;
            // 
            // ly_sales_contract_terms_outsourceTableAdapter
            // 
            this.ly_sales_contract_terms_outsourceTableAdapter.ClearBeforeFill = true;
            // 
            // tableAdapterManager1
            // 
            this.tableAdapterManager1.BackupDataSetBeforeUpdate = false;
            this.tableAdapterManager1.ly_inma0010_sortTableAdapter = null;
            this.tableAdapterManager1.ly_machinepart_process_fororderTableAdapter = null;
            this.tableAdapterManager1.ly_machinepart_process_outselfmakeTableAdapter = null;
            this.tableAdapterManager1.ly_machinepart_processTableAdapter = null;
            this.tableAdapterManager1.ly_manufacturing_procedureTableAdapter = null;
            this.tableAdapterManager1.ly_material_replacelistTableAdapter = null;
            this.tableAdapterManager1.ly_materiel_supplier_MOQTableAdapter = null;
            this.tableAdapterManager1.ly_materiel_supplier_viewTableAdapter = null;
            this.tableAdapterManager1.ly_materiel_supplierTableAdapter = null;
            this.tableAdapterManager1.ly_production_order_detail1TableAdapter = null;
            this.tableAdapterManager1.ly_production_order_detailTableAdapter = null;
            this.tableAdapterManager1.ly_production_order_materialrequisitionTableAdapter = null;
            this.tableAdapterManager1.ly_production_orderTableAdapter = null;
            this.tableAdapterManager1.ly_purchase_contract_detailQCTableAdapter = null;
            this.tableAdapterManager1.ly_purchase_contract_detailTableAdapter = null;
            this.tableAdapterManager1.ly_purchase_contract_main1TableAdapter = null;
            this.tableAdapterManager1.ly_purchase_contract_mainTableAdapter = null;
            this.tableAdapterManager1.ly_purchase_partTableAdapter = null;
            this.tableAdapterManager1.ly_purchase_prepareforplanTableAdapter = null;
            this.tableAdapterManager1.ly_sales_contract_terms_outsourceTableAdapter = this.ly_sales_contract_terms_outsourceTableAdapter;
            this.tableAdapterManager1.ly_sales_contract_terms_purchaseTableAdapter = null;
            this.tableAdapterManager1.ly_store_out_JGTableAdapter = null;
            this.tableAdapterManager1.ly_supplier_listTableAdapter = null;
            this.tableAdapterManager1.ly_worker_listDZTableAdapter = null;
            this.tableAdapterManager1.ly_worker_listQZTableAdapter = null;
            this.tableAdapterManager1.ly_worker_listTableAdapter = null;
            this.tableAdapterManager1.UpdateOrder = HappyYF.YuanXin.Data.LYMaterielRequirementsTableAdapters.TableAdapterManager.UpdateOrderOption.InsertUpdateDelete;
            // 
            // LY_OutsourceTerms_Mange
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(895, 479);
            this.Controls.Add(this.ly_sales_contract_termsDataGridView);
            this.Controls.Add(this.ly_sales_contract_termsBindingNavigator);
            this.Name = "LY_OutsourceTerms_Mange";
            this.Text = "外协合同条款设置";
            this.Load += new System.EventHandler(this.LY_Salesperson_Mange_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.LY_Salesperson_Mange_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.ly_sales_contract_termsBindingNavigator)).EndInit();
            this.ly_sales_contract_termsBindingNavigator.ResumeLayout(false);
            this.ly_sales_contract_termsBindingNavigator.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ly_sales_contract_terms_outsourceBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lYMaterielRequirements)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ly_sales_contract_termsDataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.BindingNavigator ly_sales_contract_termsBindingNavigator;
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
        private System.Windows.Forms.ToolStripButton ly_sales_contract_termsBindingNavigatorSaveItem;
        private System.Windows.Forms.DataGridView ly_sales_contract_termsDataGridView;
        private HappyYF.YuanXin.Data.LYMaterielRequirements lYMaterielRequirements;
        private System.Windows.Forms.BindingSource ly_sales_contract_terms_outsourceBindingSource;
        private HappyYF.YuanXin.Data.LYMaterielRequirementsTableAdapters.ly_sales_contract_terms_outsourceTableAdapter ly_sales_contract_terms_outsourceTableAdapter;
        private HappyYF.YuanXin.Data.LYMaterielRequirementsTableAdapters.TableAdapterManager tableAdapterManager1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;


    }
}