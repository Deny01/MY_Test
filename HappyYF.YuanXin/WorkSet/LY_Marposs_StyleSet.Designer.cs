namespace HappyYF.YuanXin.WorkSet
{
    partial class LY_Marposs_StyleSet
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LY_Marposs_StyleSet));
            this.ly_sales_repair_sectorsBindingNavigator = new System.Windows.Forms.BindingNavigator(this.components);
            this.bindingNavigatorAddNewItem = new System.Windows.Forms.ToolStripButton();
            this.ly_material_marposs_styleBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.lYSalseRepair = new HappyYF.YuanXin.Data.LYSalseRepair();
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
            this.ly_sales_repair_sectorsBindingNavigatorSaveItem = new System.Windows.Forms.ToolStripButton();
            this.ly_material_marposs_styleTableAdapter = new HappyYF.YuanXin.Data.LYSalseRepairTableAdapters.ly_material_marposs_styleTableAdapter();
            this.tableAdapterManager = new HappyYF.YuanXin.Data.LYSalseRepairTableAdapters.TableAdapterManager();
            this.ly_material_marposs_styleDataGridView = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.ly_sales_repair_sectorsBindingNavigator)).BeginInit();
            this.ly_sales_repair_sectorsBindingNavigator.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ly_material_marposs_styleBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lYSalseRepair)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ly_material_marposs_styleDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // ly_sales_repair_sectorsBindingNavigator
            // 
            this.ly_sales_repair_sectorsBindingNavigator.AddNewItem = this.bindingNavigatorAddNewItem;
            this.ly_sales_repair_sectorsBindingNavigator.BindingSource = this.ly_material_marposs_styleBindingSource;
            this.ly_sales_repair_sectorsBindingNavigator.CountItem = this.bindingNavigatorCountItem;
            this.ly_sales_repair_sectorsBindingNavigator.DeleteItem = this.bindingNavigatorDeleteItem;
            this.ly_sales_repair_sectorsBindingNavigator.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
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
            this.ly_sales_repair_sectorsBindingNavigatorSaveItem});
            this.ly_sales_repair_sectorsBindingNavigator.Location = new System.Drawing.Point(0, 0);
            this.ly_sales_repair_sectorsBindingNavigator.MoveFirstItem = this.bindingNavigatorMoveFirstItem;
            this.ly_sales_repair_sectorsBindingNavigator.MoveLastItem = this.bindingNavigatorMoveLastItem;
            this.ly_sales_repair_sectorsBindingNavigator.MoveNextItem = this.bindingNavigatorMoveNextItem;
            this.ly_sales_repair_sectorsBindingNavigator.MovePreviousItem = this.bindingNavigatorMovePreviousItem;
            this.ly_sales_repair_sectorsBindingNavigator.Name = "ly_sales_repair_sectorsBindingNavigator";
            this.ly_sales_repair_sectorsBindingNavigator.PositionItem = this.bindingNavigatorPositionItem;
            this.ly_sales_repair_sectorsBindingNavigator.Size = new System.Drawing.Size(430, 25);
            this.ly_sales_repair_sectorsBindingNavigator.TabIndex = 0;
            this.ly_sales_repair_sectorsBindingNavigator.Text = "bindingNavigator1";
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
            // ly_material_marposs_styleBindingSource
            // 
            this.ly_material_marposs_styleBindingSource.DataMember = "ly_material_marposs_style";
            this.ly_material_marposs_styleBindingSource.DataSource = this.lYSalseRepair;
            // 
            // lYSalseRepair
            // 
            this.lYSalseRepair.DataSetName = "LYSalseRepair";
            this.lYSalseRepair.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
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
            // ly_sales_repair_sectorsBindingNavigatorSaveItem
            // 
            this.ly_sales_repair_sectorsBindingNavigatorSaveItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ly_sales_repair_sectorsBindingNavigatorSaveItem.Image = ((System.Drawing.Image)(resources.GetObject("ly_sales_repair_sectorsBindingNavigatorSaveItem.Image")));
            this.ly_sales_repair_sectorsBindingNavigatorSaveItem.Name = "ly_sales_repair_sectorsBindingNavigatorSaveItem";
            this.ly_sales_repair_sectorsBindingNavigatorSaveItem.Size = new System.Drawing.Size(23, 22);
            this.ly_sales_repair_sectorsBindingNavigatorSaveItem.Text = "保存数据";
            this.ly_sales_repair_sectorsBindingNavigatorSaveItem.Click += new System.EventHandler(this.ly_sales_repair_sectorsBindingNavigatorSaveItem_Click);
            // 
            // ly_material_marposs_styleTableAdapter
            // 
            this.ly_material_marposs_styleTableAdapter.ClearBeforeFill = true;
            // 
            // tableAdapterManager
            // 
            this.tableAdapterManager.BackupDataSetBeforeUpdate = false;
            this.tableAdapterManager.ly_inform_queryforsalseTableAdapter = null;
            this.tableAdapterManager.ly_inform_queryforstoreTableAdapter = null;
            this.tableAdapterManager.ly_material_marposs_styleTableAdapter = this.ly_material_marposs_styleTableAdapter;
            this.tableAdapterManager.ly_receive_repair_wastestyleTableAdapter = null;
            this.tableAdapterManager.ly_restructuring_employWarehouseTableAdapter = null;
            this.tableAdapterManager.ly_sales_client_RepairTableAdapter = null;
            this.tableAdapterManager.ly_sales_receive_itemDetail_repairOutTableAdapter = null;
            this.tableAdapterManager.ly_sales_receive_itemTaskTableAdapter = null;
            this.tableAdapterManager.ly_store_in_innumRepairTableAdapter = null;
            this.tableAdapterManager.UpdateOrder = HappyYF.YuanXin.Data.LYSalseRepairTableAdapters.TableAdapterManager.UpdateOrderOption.InsertUpdateDelete;
            // 
            // ly_material_marposs_styleDataGridView
            // 
            this.ly_material_marposs_styleDataGridView.AutoGenerateColumns = false;
            this.ly_material_marposs_styleDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.ly_material_marposs_styleDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2,
            this.dataGridViewTextBoxColumn3});
            this.ly_material_marposs_styleDataGridView.DataSource = this.ly_material_marposs_styleBindingSource;
            this.ly_material_marposs_styleDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ly_material_marposs_styleDataGridView.Location = new System.Drawing.Point(0, 25);
            this.ly_material_marposs_styleDataGridView.Name = "ly_material_marposs_styleDataGridView";
            this.ly_material_marposs_styleDataGridView.RowHeadersWidth = 19;
            this.ly_material_marposs_styleDataGridView.RowTemplate.Height = 23;
            this.ly_material_marposs_styleDataGridView.Size = new System.Drawing.Size(430, 328);
            this.ly_material_marposs_styleDataGridView.TabIndex = 1;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.DataPropertyName = "类码";
            this.dataGridViewTextBoxColumn1.HeaderText = "类码";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.DataPropertyName = "英文类名";
            this.dataGridViewTextBoxColumn2.HeaderText = "英文类名";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.DataPropertyName = "中文类名";
            this.dataGridViewTextBoxColumn3.HeaderText = "中文类名";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            // 
            // LY_Marposs_StyleSet
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(430, 353);
            this.Controls.Add(this.ly_material_marposs_styleDataGridView);
            this.Controls.Add(this.ly_sales_repair_sectorsBindingNavigator);
            this.Name = "LY_Marposs_StyleSet";
            this.Text = "Marposs大类设置";
            this.Load += new System.EventHandler(this.LY_Sales_RepairSet_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ly_sales_repair_sectorsBindingNavigator)).EndInit();
            this.ly_sales_repair_sectorsBindingNavigator.ResumeLayout(false);
            this.ly_sales_repair_sectorsBindingNavigator.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ly_material_marposs_styleBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lYSalseRepair)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ly_material_marposs_styleDataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.BindingNavigator ly_sales_repair_sectorsBindingNavigator;
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
        private System.Windows.Forms.ToolStripButton ly_sales_repair_sectorsBindingNavigatorSaveItem;
        private HappyYF.YuanXin.Data.LYSalseRepair lYSalseRepair;
        private System.Windows.Forms.BindingSource ly_material_marposs_styleBindingSource;
        private HappyYF.YuanXin.Data.LYSalseRepairTableAdapters.ly_material_marposs_styleTableAdapter ly_material_marposs_styleTableAdapter;
        private HappyYF.YuanXin.Data.LYSalseRepairTableAdapters.TableAdapterManager tableAdapterManager;
        private System.Windows.Forms.DataGridView ly_material_marposs_styleDataGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
    }
}