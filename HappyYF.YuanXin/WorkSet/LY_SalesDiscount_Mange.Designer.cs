namespace HappyYF.YuanXin.WorkSet
{
    partial class LY_SalesDiscount_Mange
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LY_SalesDiscount_Mange));
            this.ly_salesregionBindingNavigator = new System.Windows.Forms.BindingNavigator(this.components);
            this.bindingNavigatorAddNewItem = new System.Windows.Forms.ToolStripButton();
            this.ly_salesdiscountBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.lYSalseMange = new HappyYF.YuanXin.Data.LYSalseMange();
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
            this.ly_salesregionBindingNavigatorSaveItem = new System.Windows.Forms.ToolStripButton();
            this.tableAdapterManager = new HappyYF.YuanXin.Data.LYSalseMangeTableAdapters.TableAdapterManager();
            this.ly_salesdiscountTableAdapter = new HappyYF.YuanXin.Data.LYSalseMangeTableAdapters.ly_salesdiscountTableAdapter();
            this.ly_salesdiscountDataGridView = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.ly_salesregionBindingNavigator)).BeginInit();
            this.ly_salesregionBindingNavigator.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ly_salesdiscountBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lYSalseMange)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ly_salesdiscountDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // ly_salesregionBindingNavigator
            // 
            this.ly_salesregionBindingNavigator.AddNewItem = this.bindingNavigatorAddNewItem;
            this.ly_salesregionBindingNavigator.BindingSource = this.ly_salesdiscountBindingSource;
            this.ly_salesregionBindingNavigator.CountItem = this.bindingNavigatorCountItem;
            this.ly_salesregionBindingNavigator.DeleteItem = this.bindingNavigatorDeleteItem;
            this.ly_salesregionBindingNavigator.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
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
            this.ly_salesregionBindingNavigatorSaveItem});
            this.ly_salesregionBindingNavigator.Location = new System.Drawing.Point(0, 0);
            this.ly_salesregionBindingNavigator.MoveFirstItem = this.bindingNavigatorMoveFirstItem;
            this.ly_salesregionBindingNavigator.MoveLastItem = this.bindingNavigatorMoveLastItem;
            this.ly_salesregionBindingNavigator.MoveNextItem = this.bindingNavigatorMoveNextItem;
            this.ly_salesregionBindingNavigator.MovePreviousItem = this.bindingNavigatorMovePreviousItem;
            this.ly_salesregionBindingNavigator.Name = "ly_salesregionBindingNavigator";
            this.ly_salesregionBindingNavigator.PositionItem = this.bindingNavigatorPositionItem;
            this.ly_salesregionBindingNavigator.Size = new System.Drawing.Size(507, 25);
            this.ly_salesregionBindingNavigator.TabIndex = 0;
            this.ly_salesregionBindingNavigator.Text = "bindingNavigator1";
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
            // ly_salesdiscountBindingSource
            // 
            this.ly_salesdiscountBindingSource.DataMember = "ly_salesdiscount";
            this.ly_salesdiscountBindingSource.DataSource = this.lYSalseMange;
            // 
            // lYSalseMange
            // 
            this.lYSalseMange.DataSetName = "LYSalseMange";
            this.lYSalseMange.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
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
            // ly_salesregionBindingNavigatorSaveItem
            // 
            this.ly_salesregionBindingNavigatorSaveItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ly_salesregionBindingNavigatorSaveItem.Image = ((System.Drawing.Image)(resources.GetObject("ly_salesregionBindingNavigatorSaveItem.Image")));
            this.ly_salesregionBindingNavigatorSaveItem.Name = "ly_salesregionBindingNavigatorSaveItem";
            this.ly_salesregionBindingNavigatorSaveItem.Size = new System.Drawing.Size(23, 22);
            this.ly_salesregionBindingNavigatorSaveItem.Text = "保存数据";
            this.ly_salesregionBindingNavigatorSaveItem.Click += new System.EventHandler(this.ly_salesregionBindingNavigatorSaveItem_Click);
            // 
            // tableAdapterManager
            // 
            this.tableAdapterManager.BackupDataSetBeforeUpdate = false;
            this.tableAdapterManager.Connection = null;
            this.tableAdapterManager.ly_company_informationTableAdapter = null;
            this.tableAdapterManager.ly_lsptb_selTableAdapter = null;
            this.tableAdapterManager.ly_sales_clientTableAdapter = null;
            this.tableAdapterManager.ly_sales_contract_classTableAdapter = null;
            this.tableAdapterManager.ly_sales_contract_detailTableAdapter = null;
            this.tableAdapterManager.ly_sales_contract_main1TableAdapter = null;
            this.tableAdapterManager.ly_sales_contract_mainSingleTableAdapter = null;
            this.tableAdapterManager.ly_sales_contract_mainTableAdapter = null;
            this.tableAdapterManager.ly_sales_contract_styleTableAdapter = null;
            this.tableAdapterManager.ly_sales_contract_terms_forcontractTableAdapter = null;
            this.tableAdapterManager.ly_sales_contract_termsTableAdapter = null;
            this.tableAdapterManager.ly_salesdiscountTableAdapter = null;
            this.tableAdapterManager.ly_salespersonTableAdapter = null;
            this.tableAdapterManager.ly_salesregionTableAdapter = null;
            this.tableAdapterManager.UpdateOrder = HappyYF.YuanXin.Data.LYSalseMangeTableAdapters.TableAdapterManager.UpdateOrderOption.InsertUpdateDelete;
            // 
            // ly_salesdiscountTableAdapter
            // 
            this.ly_salesdiscountTableAdapter.ClearBeforeFill = true;
            // 
            // ly_salesdiscountDataGridView
            // 
            this.ly_salesdiscountDataGridView.AllowUserToAddRows = false;
            this.ly_salesdiscountDataGridView.AllowUserToDeleteRows = false;
            this.ly_salesdiscountDataGridView.AutoGenerateColumns = false;
            this.ly_salesdiscountDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.ly_salesdiscountDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2,
            this.dataGridViewTextBoxColumn3,
            this.dataGridViewTextBoxColumn4});
            this.ly_salesdiscountDataGridView.DataSource = this.ly_salesdiscountBindingSource;
            this.ly_salesdiscountDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ly_salesdiscountDataGridView.Location = new System.Drawing.Point(0, 25);
            this.ly_salesdiscountDataGridView.Name = "ly_salesdiscountDataGridView";
            this.ly_salesdiscountDataGridView.RowHeadersWidth = 19;
            this.ly_salesdiscountDataGridView.RowTemplate.Height = 23;
            this.ly_salesdiscountDataGridView.Size = new System.Drawing.Size(507, 313);
            this.ly_salesdiscountDataGridView.TabIndex = 1;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.DataPropertyName = "id";
            this.dataGridViewTextBoxColumn1.HeaderText = "id";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn1.Visible = false;
            this.dataGridViewTextBoxColumn1.Width = 51;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.DataPropertyName = "编码";
            this.dataGridViewTextBoxColumn2.HeaderText = "编码";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.DataPropertyName = "说明";
            this.dataGridViewTextBoxColumn3.HeaderText = "说明";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.DataPropertyName = "折扣";
            this.dataGridViewTextBoxColumn4.HeaderText = "折扣(%)";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            // 
            // LY_SalesDiscount_Mange
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(507, 338);
            this.Controls.Add(this.ly_salesdiscountDataGridView);
            this.Controls.Add(this.ly_salesregionBindingNavigator);
            this.Name = "LY_SalesDiscount_Mange";
            this.Text = "营业折扣设置";
            this.Load += new System.EventHandler(this.LY_Salesregion_Mange_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.LY_Salesregion_Mange_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.ly_salesregionBindingNavigator)).EndInit();
            this.ly_salesregionBindingNavigator.ResumeLayout(false);
            this.ly_salesregionBindingNavigator.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ly_salesdiscountBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lYSalseMange)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ly_salesdiscountDataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private HappyYF.YuanXin.Data.LYSalseMange lYSalseMange;
        private HappyYF.YuanXin.Data.LYSalseMangeTableAdapters.TableAdapterManager tableAdapterManager;
        private System.Windows.Forms.BindingNavigator ly_salesregionBindingNavigator;
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
        private System.Windows.Forms.ToolStripButton ly_salesregionBindingNavigatorSaveItem;
        private System.Windows.Forms.BindingSource ly_salesdiscountBindingSource;
        private HappyYF.YuanXin.Data.LYSalseMangeTableAdapters.ly_salesdiscountTableAdapter ly_salesdiscountTableAdapter;
        private System.Windows.Forms.DataGridView ly_salesdiscountDataGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
    }
}