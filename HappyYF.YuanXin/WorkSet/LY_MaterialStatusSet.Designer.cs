﻿namespace HappyYF.YuanXin.WorkSet
{
    partial class LY_MaterialStatusSet
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LY_MaterialStatusSet));
            this.lYMaterialMange = new HappyYF.YuanXin.Data.LYMaterialMange();
            this.ly_materialstatusBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.ly_materialstatusTableAdapter = new HappyYF.YuanXin.Data.LYMaterialMangeTableAdapters.ly_materialstatusTableAdapter();
            this.tableAdapterManager = new HappyYF.YuanXin.Data.LYMaterialMangeTableAdapters.TableAdapterManager();
            this.ly_materialstatusBindingNavigator = new System.Windows.Forms.BindingNavigator(this.components);
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
            this.ly_materialstatusBindingNavigatorSaveItem = new System.Windows.Forms.ToolStripButton();
            this.ly_materialstatusDataGridView = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.lYMaterialMange)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ly_materialstatusBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ly_materialstatusBindingNavigator)).BeginInit();
            this.ly_materialstatusBindingNavigator.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ly_materialstatusDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // lYMaterialMange
            // 
            this.lYMaterialMange.DataSetName = "LYMaterialMange";
            this.lYMaterialMange.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // ly_materialstatusBindingSource
            // 
            this.ly_materialstatusBindingSource.DataMember = "ly_materialstatus";
            this.ly_materialstatusBindingSource.DataSource = this.lYMaterialMange;
            // 
            // ly_materialstatusTableAdapter
            // 
            this.ly_materialstatusTableAdapter.ClearBeforeFill = true;
            // 
            // tableAdapterManager
            // 
            this.tableAdapterManager.BackupDataSetBeforeUpdate = false;
            this.tableAdapterManager.ly_inma0010addTableAdapter = null;
            this.tableAdapterManager.ly_inma0010cpTableAdapter = null;
            this.tableAdapterManager.ly_inma0010TableAdapter = null;
            this.tableAdapterManager.ly_materialstatusTableAdapter = this.ly_materialstatusTableAdapter;
            this.tableAdapterManager.ly_unitsetTableAdapter = null;
            this.tableAdapterManager.UpdateOrder = HappyYF.YuanXin.Data.LYMaterialMangeTableAdapters.TableAdapterManager.UpdateOrderOption.InsertUpdateDelete;
            // 
            // ly_materialstatusBindingNavigator
            // 
            this.ly_materialstatusBindingNavigator.AddNewItem = this.bindingNavigatorAddNewItem;
            this.ly_materialstatusBindingNavigator.BindingSource = this.ly_materialstatusBindingSource;
            this.ly_materialstatusBindingNavigator.CountItem = this.bindingNavigatorCountItem;
            this.ly_materialstatusBindingNavigator.DeleteItem = this.bindingNavigatorDeleteItem;
            this.ly_materialstatusBindingNavigator.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
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
            this.ly_materialstatusBindingNavigatorSaveItem});
            this.ly_materialstatusBindingNavigator.Location = new System.Drawing.Point(0, 0);
            this.ly_materialstatusBindingNavigator.MoveFirstItem = this.bindingNavigatorMoveFirstItem;
            this.ly_materialstatusBindingNavigator.MoveLastItem = this.bindingNavigatorMoveLastItem;
            this.ly_materialstatusBindingNavigator.MoveNextItem = this.bindingNavigatorMoveNextItem;
            this.ly_materialstatusBindingNavigator.MovePreviousItem = this.bindingNavigatorMovePreviousItem;
            this.ly_materialstatusBindingNavigator.Name = "ly_materialstatusBindingNavigator";
            this.ly_materialstatusBindingNavigator.PositionItem = this.bindingNavigatorPositionItem;
            this.ly_materialstatusBindingNavigator.Size = new System.Drawing.Size(336, 25);
            this.ly_materialstatusBindingNavigator.TabIndex = 0;
            this.ly_materialstatusBindingNavigator.Text = "bindingNavigator1";
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
            // ly_materialstatusBindingNavigatorSaveItem
            // 
            this.ly_materialstatusBindingNavigatorSaveItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ly_materialstatusBindingNavigatorSaveItem.Image = ((System.Drawing.Image)(resources.GetObject("ly_materialstatusBindingNavigatorSaveItem.Image")));
            this.ly_materialstatusBindingNavigatorSaveItem.Name = "ly_materialstatusBindingNavigatorSaveItem";
            this.ly_materialstatusBindingNavigatorSaveItem.Size = new System.Drawing.Size(23, 23);
            this.ly_materialstatusBindingNavigatorSaveItem.Text = "保存数据";
            this.ly_materialstatusBindingNavigatorSaveItem.Click += new System.EventHandler(this.ly_materialstatusBindingNavigatorSaveItem_Click);
            // 
            // ly_materialstatusDataGridView
            // 
            this.ly_materialstatusDataGridView.AutoGenerateColumns = false;
            this.ly_materialstatusDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ly_materialstatusDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2});
            this.ly_materialstatusDataGridView.DataSource = this.ly_materialstatusBindingSource;
            this.ly_materialstatusDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ly_materialstatusDataGridView.Location = new System.Drawing.Point(0, 25);
            this.ly_materialstatusDataGridView.Name = "ly_materialstatusDataGridView";
            this.ly_materialstatusDataGridView.RowTemplate.Height = 23;
            this.ly_materialstatusDataGridView.Size = new System.Drawing.Size(336, 171);
            this.ly_materialstatusDataGridView.TabIndex = 1;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.DataPropertyName = "status_code";
            this.dataGridViewTextBoxColumn1.HeaderText = "状态代码";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.DataPropertyName = "status_name";
            this.dataGridViewTextBoxColumn2.HeaderText = "状态名称";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            // 
            // LY_MaterialStatusSet
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(336, 196);
            this.Controls.Add(this.ly_materialstatusDataGridView);
            this.Controls.Add(this.ly_materialstatusBindingNavigator);
            this.Name = "LY_MaterialStatusSet";
            this.Text = "物料状态设置";
            this.Load += new System.EventHandler(this.LY_MaterialStatusSet_Load);
            ((System.ComponentModel.ISupportInitialize)(this.lYMaterialMange)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ly_materialstatusBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ly_materialstatusBindingNavigator)).EndInit();
            this.ly_materialstatusBindingNavigator.ResumeLayout(false);
            this.ly_materialstatusBindingNavigator.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ly_materialstatusDataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private HappyYF.YuanXin.Data.LYMaterialMange lYMaterialMange;
        private System.Windows.Forms.BindingSource ly_materialstatusBindingSource;
        private HappyYF.YuanXin.Data.LYMaterialMangeTableAdapters.ly_materialstatusTableAdapter ly_materialstatusTableAdapter;
        private HappyYF.YuanXin.Data.LYMaterialMangeTableAdapters.TableAdapterManager tableAdapterManager;
        private System.Windows.Forms.BindingNavigator ly_materialstatusBindingNavigator;
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
        private System.Windows.Forms.ToolStripButton ly_materialstatusBindingNavigatorSaveItem;
        private System.Windows.Forms.DataGridView ly_materialstatusDataGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
    }
}