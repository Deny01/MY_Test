namespace HappyYF.YuanXin.WorkSet
{
    partial class LY_Drawing_levelSet
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LY_Drawing_levelSet));
            this.lYMaterialMange = new HappyYF.YuanXin.Data.LYMaterialMange();
            this.ly_drawing_levelSetBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.ly_drawing_levelSetTableAdapter = new HappyYF.YuanXin.Data.LYMaterialMangeTableAdapters.ly_drawing_levelSetTableAdapter();
            this.tableAdapterManager = new HappyYF.YuanXin.Data.LYMaterialMangeTableAdapters.TableAdapterManager();
            this.ly_drawing_levelSetBindingNavigator = new System.Windows.Forms.BindingNavigator(this.components);
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
            this.ly_drawing_levelSetBindingNavigatorSaveItem = new System.Windows.Forms.ToolStripButton();
            this.ly_drawing_levelSetDataGridView = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.lYMaterialMange)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ly_drawing_levelSetBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ly_drawing_levelSetBindingNavigator)).BeginInit();
            this.ly_drawing_levelSetBindingNavigator.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ly_drawing_levelSetDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // lYMaterialMange
            // 
            this.lYMaterialMange.DataSetName = "LYMaterialMange";
            this.lYMaterialMange.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // ly_drawing_levelSetBindingSource
            // 
            this.ly_drawing_levelSetBindingSource.DataMember = "ly_drawing_levelSet";
            this.ly_drawing_levelSetBindingSource.DataSource = this.lYMaterialMange;
            // 
            // ly_drawing_levelSetTableAdapter
            // 
            this.ly_drawing_levelSetTableAdapter.ClearBeforeFill = true;
            // 
            // tableAdapterManager
            // 
            this.tableAdapterManager.BackupDataSetBeforeUpdate = false;
            this.tableAdapterManager.ly_bm0031TableAdapter = null;
            this.tableAdapterManager.ly_drawing_levelSetTableAdapter = this.ly_drawing_levelSetTableAdapter;
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
            // ly_drawing_levelSetBindingNavigator
            // 
            this.ly_drawing_levelSetBindingNavigator.AddNewItem = this.bindingNavigatorAddNewItem;
            this.ly_drawing_levelSetBindingNavigator.BindingSource = this.ly_drawing_levelSetBindingSource;
            this.ly_drawing_levelSetBindingNavigator.CountItem = this.bindingNavigatorCountItem;
            this.ly_drawing_levelSetBindingNavigator.DeleteItem = this.bindingNavigatorDeleteItem;
            this.ly_drawing_levelSetBindingNavigator.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
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
            this.ly_drawing_levelSetBindingNavigatorSaveItem});
            this.ly_drawing_levelSetBindingNavigator.Location = new System.Drawing.Point(0, 0);
            this.ly_drawing_levelSetBindingNavigator.MoveFirstItem = this.bindingNavigatorMoveFirstItem;
            this.ly_drawing_levelSetBindingNavigator.MoveLastItem = this.bindingNavigatorMoveLastItem;
            this.ly_drawing_levelSetBindingNavigator.MoveNextItem = this.bindingNavigatorMoveNextItem;
            this.ly_drawing_levelSetBindingNavigator.MovePreviousItem = this.bindingNavigatorMovePreviousItem;
            this.ly_drawing_levelSetBindingNavigator.Name = "ly_drawing_levelSetBindingNavigator";
            this.ly_drawing_levelSetBindingNavigator.PositionItem = this.bindingNavigatorPositionItem;
            this.ly_drawing_levelSetBindingNavigator.Size = new System.Drawing.Size(495, 25);
            this.ly_drawing_levelSetBindingNavigator.TabIndex = 0;
            this.ly_drawing_levelSetBindingNavigator.Text = "bindingNavigator1";
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
            // ly_drawing_levelSetBindingNavigatorSaveItem
            // 
            this.ly_drawing_levelSetBindingNavigatorSaveItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ly_drawing_levelSetBindingNavigatorSaveItem.Image = ((System.Drawing.Image)(resources.GetObject("ly_drawing_levelSetBindingNavigatorSaveItem.Image")));
            this.ly_drawing_levelSetBindingNavigatorSaveItem.Name = "ly_drawing_levelSetBindingNavigatorSaveItem";
            this.ly_drawing_levelSetBindingNavigatorSaveItem.Size = new System.Drawing.Size(23, 22);
            this.ly_drawing_levelSetBindingNavigatorSaveItem.Text = "保存数据";
            this.ly_drawing_levelSetBindingNavigatorSaveItem.Click += new System.EventHandler(this.ly_drawing_levelBindingNavigatorSaveItem_Click);
            // 
            // ly_drawing_levelSetDataGridView
            // 
            this.ly_drawing_levelSetDataGridView.AutoGenerateColumns = false;
            this.ly_drawing_levelSetDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ly_drawing_levelSetDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2,
            this.dataGridViewTextBoxColumn3});
            this.ly_drawing_levelSetDataGridView.DataSource = this.ly_drawing_levelSetBindingSource;
            this.ly_drawing_levelSetDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ly_drawing_levelSetDataGridView.Location = new System.Drawing.Point(0, 25);
            this.ly_drawing_levelSetDataGridView.Name = "ly_drawing_levelSetDataGridView";
            this.ly_drawing_levelSetDataGridView.RowTemplate.Height = 23;
            this.ly_drawing_levelSetDataGridView.Size = new System.Drawing.Size(495, 439);
            this.ly_drawing_levelSetDataGridView.TabIndex = 1;
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
            this.dataGridViewTextBoxColumn2.DataPropertyName = "级别";
            this.dataGridViewTextBoxColumn2.HeaderText = "级别";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.DataPropertyName = "备注";
            this.dataGridViewTextBoxColumn3.HeaderText = "备注";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.Width = 300;
            // 
            // LY_Drawing_levelSet
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(495, 464);
            this.Controls.Add(this.ly_drawing_levelSetDataGridView);
            this.Controls.Add(this.ly_drawing_levelSetBindingNavigator);
            this.Name = "LY_Drawing_levelSet";
            this.Text = "图纸级别设置";
            this.Load += new System.EventHandler(this.LY_Drawing_levelSet_Load);
            ((System.ComponentModel.ISupportInitialize)(this.lYMaterialMange)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ly_drawing_levelSetBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ly_drawing_levelSetBindingNavigator)).EndInit();
            this.ly_drawing_levelSetBindingNavigator.ResumeLayout(false);
            this.ly_drawing_levelSetBindingNavigator.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ly_drawing_levelSetDataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private HappyYF.YuanXin.Data.LYMaterialMange lYMaterialMange;
        private System.Windows.Forms.BindingSource ly_drawing_levelSetBindingSource;
        private HappyYF.YuanXin.Data.LYMaterialMangeTableAdapters.ly_drawing_levelSetTableAdapter ly_drawing_levelSetTableAdapter;
        private HappyYF.YuanXin.Data.LYMaterialMangeTableAdapters.TableAdapterManager tableAdapterManager;
        private System.Windows.Forms.BindingNavigator ly_drawing_levelSetBindingNavigator;
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
        private System.Windows.Forms.ToolStripButton ly_drawing_levelSetBindingNavigatorSaveItem;
        private System.Windows.Forms.DataGridView ly_drawing_levelSetDataGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;



    }
}