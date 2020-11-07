namespace HappyYF.YuanXin.WorkSet
{
    partial class LY_Receive_Repair_Wastestyle
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LY_Receive_Repair_Wastestyle));
            this.ly_store_out_stylesetBindingNavigator = new System.Windows.Forms.BindingNavigator(this.components);
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
            this.ly_store_out_stylesetBindingNavigatorSaveItem = new System.Windows.Forms.ToolStripButton();
            this.lYSalseRepair = new HappyYF.YuanXin.Data.LYSalseRepair();
            this.ly_receive_repair_wastestyleBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.ly_receive_repair_wastestyleTableAdapter = new HappyYF.YuanXin.Data.LYSalseRepairTableAdapters.ly_receive_repair_wastestyleTableAdapter();
            this.tableAdapterManager1 = new HappyYF.YuanXin.Data.LYSalseRepairTableAdapters.TableAdapterManager();
            this.ly_receive_repair_wastestyleDataGridView = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.ly_store_out_stylesetBindingNavigator)).BeginInit();
            this.ly_store_out_stylesetBindingNavigator.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lYSalseRepair)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ly_receive_repair_wastestyleBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ly_receive_repair_wastestyleDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // ly_store_out_stylesetBindingNavigator
            // 
            this.ly_store_out_stylesetBindingNavigator.AddNewItem = this.bindingNavigatorAddNewItem;
            this.ly_store_out_stylesetBindingNavigator.BindingSource = this.ly_receive_repair_wastestyleBindingSource;
            this.ly_store_out_stylesetBindingNavigator.CountItem = this.bindingNavigatorCountItem;
            this.ly_store_out_stylesetBindingNavigator.DeleteItem = this.bindingNavigatorDeleteItem;
            this.ly_store_out_stylesetBindingNavigator.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
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
            this.ly_store_out_stylesetBindingNavigatorSaveItem});
            this.ly_store_out_stylesetBindingNavigator.Location = new System.Drawing.Point(0, 0);
            this.ly_store_out_stylesetBindingNavigator.MoveFirstItem = this.bindingNavigatorMoveFirstItem;
            this.ly_store_out_stylesetBindingNavigator.MoveLastItem = this.bindingNavigatorMoveLastItem;
            this.ly_store_out_stylesetBindingNavigator.MoveNextItem = this.bindingNavigatorMoveNextItem;
            this.ly_store_out_stylesetBindingNavigator.MovePreviousItem = this.bindingNavigatorMovePreviousItem;
            this.ly_store_out_stylesetBindingNavigator.Name = "ly_store_out_stylesetBindingNavigator";
            this.ly_store_out_stylesetBindingNavigator.PositionItem = this.bindingNavigatorPositionItem;
            this.ly_store_out_stylesetBindingNavigator.Size = new System.Drawing.Size(378, 25);
            this.ly_store_out_stylesetBindingNavigator.TabIndex = 0;
            this.ly_store_out_stylesetBindingNavigator.Text = "bindingNavigator1";
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
            // ly_store_out_stylesetBindingNavigatorSaveItem
            // 
            this.ly_store_out_stylesetBindingNavigatorSaveItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ly_store_out_stylesetBindingNavigatorSaveItem.Image = ((System.Drawing.Image)(resources.GetObject("ly_store_out_stylesetBindingNavigatorSaveItem.Image")));
            this.ly_store_out_stylesetBindingNavigatorSaveItem.Name = "ly_store_out_stylesetBindingNavigatorSaveItem";
            this.ly_store_out_stylesetBindingNavigatorSaveItem.Size = new System.Drawing.Size(23, 22);
            this.ly_store_out_stylesetBindingNavigatorSaveItem.Text = "保存数据";
            this.ly_store_out_stylesetBindingNavigatorSaveItem.Click += new System.EventHandler(this.ly_store_out_stylesetBindingNavigatorSaveItem_Click);
            // 
            // lYSalseRepair
            // 
            this.lYSalseRepair.DataSetName = "LYSalseRepair";
            this.lYSalseRepair.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // ly_receive_repair_wastestyleBindingSource
            // 
            this.ly_receive_repair_wastestyleBindingSource.DataMember = "ly_receive_repair_wastestyle";
            this.ly_receive_repair_wastestyleBindingSource.DataSource = this.lYSalseRepair;
            // 
            // ly_receive_repair_wastestyleTableAdapter
            // 
            this.ly_receive_repair_wastestyleTableAdapter.ClearBeforeFill = true;
            // 
            // tableAdapterManager1
            // 
            this.tableAdapterManager1.BackupDataSetBeforeUpdate = false;
            this.tableAdapterManager1.ly_inform_queryforsalseTableAdapter = null;
            this.tableAdapterManager1.ly_inform_queryforstoreTableAdapter = null;
            this.tableAdapterManager1.ly_material_marposs_styleTableAdapter = null;
            this.tableAdapterManager1.ly_receive_repair_wastestyleTableAdapter = this.ly_receive_repair_wastestyleTableAdapter;
            this.tableAdapterManager1.ly_sales_client_RepairTableAdapter = null;
            this.tableAdapterManager1.UpdateOrder = HappyYF.YuanXin.Data.LYSalseRepairTableAdapters.TableAdapterManager.UpdateOrderOption.InsertUpdateDelete;
            // 
            // ly_receive_repair_wastestyleDataGridView
            // 
            this.ly_receive_repair_wastestyleDataGridView.AutoGenerateColumns = false;
            this.ly_receive_repair_wastestyleDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ly_receive_repair_wastestyleDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn3,
            this.dataGridViewTextBoxColumn4});
            this.ly_receive_repair_wastestyleDataGridView.DataSource = this.ly_receive_repair_wastestyleBindingSource;
            this.ly_receive_repair_wastestyleDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ly_receive_repair_wastestyleDataGridView.Location = new System.Drawing.Point(0, 25);
            this.ly_receive_repair_wastestyleDataGridView.Name = "ly_receive_repair_wastestyleDataGridView";
            this.ly_receive_repair_wastestyleDataGridView.RowHeadersWidth = 19;
            this.ly_receive_repair_wastestyleDataGridView.RowTemplate.Height = 23;
            this.ly_receive_repair_wastestyleDataGridView.Size = new System.Drawing.Size(378, 331);
            this.ly_receive_repair_wastestyleDataGridView.TabIndex = 2;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.DataPropertyName = "stylecode";
            this.dataGridViewTextBoxColumn3.HeaderText = "废料类别";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.DataPropertyName = "stylename";
            this.dataGridViewTextBoxColumn4.HeaderText = "废料名称";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            // 
            // LY_Receive_Repair_Wastestyle
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(378, 356);
            this.Controls.Add(this.ly_receive_repair_wastestyleDataGridView);
            this.Controls.Add(this.ly_store_out_stylesetBindingNavigator);
            this.Name = "LY_Receive_Repair_Wastestyle";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "营业维修废料类别设置";
            this.Load += new System.EventHandler(this.LY_StoreoutSet_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ly_store_out_stylesetBindingNavigator)).EndInit();
            this.ly_store_out_stylesetBindingNavigator.ResumeLayout(false);
            this.ly_store_out_stylesetBindingNavigator.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lYSalseRepair)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ly_receive_repair_wastestyleBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ly_receive_repair_wastestyleDataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.BindingNavigator ly_store_out_stylesetBindingNavigator;
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
        private System.Windows.Forms.ToolStripButton ly_store_out_stylesetBindingNavigatorSaveItem;
        private HappyYF.YuanXin.Data.LYSalseRepair lYSalseRepair;
        private System.Windows.Forms.BindingSource ly_receive_repair_wastestyleBindingSource;
        private HappyYF.YuanXin.Data.LYSalseRepairTableAdapters.ly_receive_repair_wastestyleTableAdapter ly_receive_repair_wastestyleTableAdapter;
        private HappyYF.YuanXin.Data.LYSalseRepairTableAdapters.TableAdapterManager tableAdapterManager1;
        private System.Windows.Forms.DataGridView ly_receive_repair_wastestyleDataGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
    }
}