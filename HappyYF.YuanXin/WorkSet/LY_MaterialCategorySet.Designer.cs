namespace HappyYF.YuanXin.WorkSet
{
    partial class LY_MaterialCategorySet
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LY_MaterialCategorySet));
            this.lYMaterialMange = new HappyYF.YuanXin.Data.LYMaterialMange();
            this.ly_materialcategoryBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.ly_materialcategoryTableAdapter = new HappyYF.YuanXin.Data.LYMaterialMangeTableAdapters.ly_materialcategoryTableAdapter();
            this.tableAdapterManager = new HappyYF.YuanXin.Data.LYMaterialMangeTableAdapters.TableAdapterManager();
            this.ly_materialcategoryBindingNavigator = new System.Windows.Forms.BindingNavigator(this.components);
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
            this.ly_materialcategoryBindingNavigatorSaveItem = new System.Windows.Forms.ToolStripButton();
            this.ly_materialcategoryDataGridView = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.lYMaterialMange)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ly_materialcategoryBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ly_materialcategoryBindingNavigator)).BeginInit();
            this.ly_materialcategoryBindingNavigator.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ly_materialcategoryDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // lYMaterialMange
            // 
            this.lYMaterialMange.DataSetName = "LYMaterialMange";
            this.lYMaterialMange.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // ly_materialcategoryBindingSource
            // 
            this.ly_materialcategoryBindingSource.DataMember = "ly_materialcategory";
            this.ly_materialcategoryBindingSource.DataSource = this.lYMaterialMange;
            // 
            // ly_materialcategoryTableAdapter
            // 
            this.ly_materialcategoryTableAdapter.ClearBeforeFill = true;
            // 
            // tableAdapterManager
            // 
            this.tableAdapterManager.BackupDataSetBeforeUpdate = false;
            this.tableAdapterManager.ly_bm0031TableAdapter = null;
            this.tableAdapterManager.ly_drawing_levelSetTableAdapter = null;
            this.tableAdapterManager.ly_employe_warehouseTableAdapter = null;
            this.tableAdapterManager.ly_firststyle_setTableAdapter = null;
            this.tableAdapterManager.ly_inma0010_drawingTableAdapter = null;
            this.tableAdapterManager.ly_inma001011TableAdapter = null;
            this.tableAdapterManager.ly_inma00101TableAdapter = null;
            this.tableAdapterManager.ly_inma0010addTableAdapter = null;
            this.tableAdapterManager.ly_inma0010cpTableAdapter = null;
            this.tableAdapterManager.ly_inma0010fjTableAdapter = null;
            this.tableAdapterManager.ly_inma0010machineTableAdapter = null;
            this.tableAdapterManager.ly_inma0010TableAdapter = null;
            this.tableAdapterManager.ly_inma0010ylTableAdapter = null;
            this.tableAdapterManager.ly_material_getmethodTableAdapter = null;
            this.tableAdapterManager.ly_material_plan_detail_endProductTableAdapter = null;
            this.tableAdapterManager.ly_materialcategoryTableAdapter = this.ly_materialcategoryTableAdapter;
            this.tableAdapterManager.ly_materialstatusTableAdapter = null;
            this.tableAdapterManager.ly_materrial_sortTableAdapter = null;
            this.tableAdapterManager.ly_prod_deptTableAdapter = null;
            this.tableAdapterManager.ly_sales_matingBom_CostTableAdapter = null;
            this.tableAdapterManager.ly_secondstyle_setTableAdapter = null;
            this.tableAdapterManager.ly_unitset1TableAdapter = null;
            this.tableAdapterManager.ly_unitsetTableAdapter = null;
            this.tableAdapterManager.ly_warehouseTableAdapter = null;
            this.tableAdapterManager.UpdateOrder = HappyYF.YuanXin.Data.LYMaterialMangeTableAdapters.TableAdapterManager.UpdateOrderOption.InsertUpdateDelete;
            // 
            // ly_materialcategoryBindingNavigator
            // 
            this.ly_materialcategoryBindingNavigator.AddNewItem = this.bindingNavigatorAddNewItem;
            this.ly_materialcategoryBindingNavigator.BindingSource = this.ly_materialcategoryBindingSource;
            this.ly_materialcategoryBindingNavigator.CountItem = this.bindingNavigatorCountItem;
            this.ly_materialcategoryBindingNavigator.DeleteItem = this.bindingNavigatorDeleteItem;
            this.ly_materialcategoryBindingNavigator.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
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
            this.ly_materialcategoryBindingNavigatorSaveItem});
            this.ly_materialcategoryBindingNavigator.Location = new System.Drawing.Point(0, 0);
            this.ly_materialcategoryBindingNavigator.MoveFirstItem = this.bindingNavigatorMoveFirstItem;
            this.ly_materialcategoryBindingNavigator.MoveLastItem = this.bindingNavigatorMoveLastItem;
            this.ly_materialcategoryBindingNavigator.MoveNextItem = this.bindingNavigatorMoveNextItem;
            this.ly_materialcategoryBindingNavigator.MovePreviousItem = this.bindingNavigatorMovePreviousItem;
            this.ly_materialcategoryBindingNavigator.Name = "ly_materialcategoryBindingNavigator";
            this.ly_materialcategoryBindingNavigator.PositionItem = this.bindingNavigatorPositionItem;
            this.ly_materialcategoryBindingNavigator.Size = new System.Drawing.Size(316, 25);
            this.ly_materialcategoryBindingNavigator.TabIndex = 0;
            this.ly_materialcategoryBindingNavigator.Text = "bindingNavigator1";
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
            // ly_materialcategoryBindingNavigatorSaveItem
            // 
            this.ly_materialcategoryBindingNavigatorSaveItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ly_materialcategoryBindingNavigatorSaveItem.Image = ((System.Drawing.Image)(resources.GetObject("ly_materialcategoryBindingNavigatorSaveItem.Image")));
            this.ly_materialcategoryBindingNavigatorSaveItem.Name = "ly_materialcategoryBindingNavigatorSaveItem";
            this.ly_materialcategoryBindingNavigatorSaveItem.Size = new System.Drawing.Size(23, 23);
            this.ly_materialcategoryBindingNavigatorSaveItem.Text = "保存数据";
            this.ly_materialcategoryBindingNavigatorSaveItem.Click += new System.EventHandler(this.ly_materialcategoryBindingNavigatorSaveItem_Click);
            // 
            // ly_materialcategoryDataGridView
            // 
            this.ly_materialcategoryDataGridView.AutoGenerateColumns = false;
            this.ly_materialcategoryDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ly_materialcategoryDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2});
            this.ly_materialcategoryDataGridView.DataSource = this.ly_materialcategoryBindingSource;
            this.ly_materialcategoryDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ly_materialcategoryDataGridView.Location = new System.Drawing.Point(0, 25);
            this.ly_materialcategoryDataGridView.Name = "ly_materialcategoryDataGridView";
            this.ly_materialcategoryDataGridView.RowTemplate.Height = 23;
            this.ly_materialcategoryDataGridView.Size = new System.Drawing.Size(316, 269);
            this.ly_materialcategoryDataGridView.TabIndex = 1;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.DataPropertyName = "组码";
            this.dataGridViewTextBoxColumn1.HeaderText = "组码";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.DataPropertyName = "组名";
            this.dataGridViewTextBoxColumn2.HeaderText = "组名";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            // 
            // LY_MaterialCategorySet
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(316, 294);
            this.Controls.Add(this.ly_materialcategoryDataGridView);
            this.Controls.Add(this.ly_materialcategoryBindingNavigator);
            this.Name = "LY_MaterialCategorySet";
            this.Text = "物料组别";
            this.Load += new System.EventHandler(this.LY_MaterialCategorySet_Load);
            ((System.ComponentModel.ISupportInitialize)(this.lYMaterialMange)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ly_materialcategoryBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ly_materialcategoryBindingNavigator)).EndInit();
            this.ly_materialcategoryBindingNavigator.ResumeLayout(false);
            this.ly_materialcategoryBindingNavigator.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ly_materialcategoryDataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private HappyYF.YuanXin.Data.LYMaterialMange lYMaterialMange;
        private System.Windows.Forms.BindingSource ly_materialcategoryBindingSource;
        private HappyYF.YuanXin.Data.LYMaterialMangeTableAdapters.ly_materialcategoryTableAdapter ly_materialcategoryTableAdapter;
        private HappyYF.YuanXin.Data.LYMaterialMangeTableAdapters.TableAdapterManager tableAdapterManager;
        private System.Windows.Forms.BindingNavigator ly_materialcategoryBindingNavigator;
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
        private System.Windows.Forms.ToolStripButton ly_materialcategoryBindingNavigatorSaveItem;
        private System.Windows.Forms.DataGridView ly_materialcategoryDataGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
    }
}