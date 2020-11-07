namespace HappyYF.YuanXin.WorkSet
{
    partial class LY_Express_Mange
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LY_Express_Mange));
            this.lYSalseMange = new HappyYF.YuanXin.Data.LYSalseMange();
            this.ly_express_companyBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.ly_express_companyTableAdapter = new HappyYF.YuanXin.Data.LYSalseMangeTableAdapters.ly_express_companyTableAdapter();
            this.tableAdapterManager = new HappyYF.YuanXin.Data.LYSalseMangeTableAdapters.TableAdapterManager();
            this.ly_express_companyBindingNavigator = new System.Windows.Forms.BindingNavigator(this.components);
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
            this.ly_express_companyBindingNavigatorSaveItem = new System.Windows.Forms.ToolStripButton();
            this.ly_express_companyDataGridView = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.lYSalseMange)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ly_express_companyBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ly_express_companyBindingNavigator)).BeginInit();
            this.ly_express_companyBindingNavigator.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ly_express_companyDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // lYSalseMange
            // 
            this.lYSalseMange.DataSetName = "LYSalseMange";
            this.lYSalseMange.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // ly_express_companyBindingSource
            // 
            this.ly_express_companyBindingSource.DataMember = "ly_express_company";
            this.ly_express_companyBindingSource.DataSource = this.lYSalseMange;
            // 
            // ly_express_companyTableAdapter
            // 
            this.ly_express_companyTableAdapter.ClearBeforeFill = true;
            // 
            // tableAdapterManager
            // 
            this.tableAdapterManager.BackupDataSetBeforeUpdate = false;
            this.tableAdapterManager.ly_company_informationTableAdapter = null;
            this.tableAdapterManager.ly_express_companyTableAdapter = this.ly_express_companyTableAdapter;
            this.tableAdapterManager.ly_lsptb_selTableAdapter = null;
            this.tableAdapterManager.ly_material_plan_mainperiodTableAdapter = null;
            this.tableAdapterManager.ly_material_plan_mainSingleTableAdapter = null;
            this.tableAdapterManager.ly_material_plan_mainTableAdapter = null;
            this.tableAdapterManager.ly_plan_getmaterial_departmentTableAdapter = null;
            this.tableAdapterManager.ly_plan_getmaterial1TableAdapter = null;
            this.tableAdapterManager.ly_plan_getmaterialSingleTableAdapter = null;
            this.tableAdapterManager.ly_plan_getmaterialTableAdapter = null;
            this.tableAdapterManager.ly_sales_clientTableAdapter = null;
            this.tableAdapterManager.ly_sales_contract_classTableAdapter = null;
            this.tableAdapterManager.ly_sales_contract_detailTableAdapter = null;
            this.tableAdapterManager.ly_sales_contract_main1TableAdapter = null;
            this.tableAdapterManager.ly_sales_contract_mainSingleTableAdapter = null;
            this.tableAdapterManager.ly_sales_contract_mainTableAdapter = null;
            this.tableAdapterManager.ly_sales_contract_styleTableAdapter = null;
            this.tableAdapterManager.ly_sales_contract_terms_forcontractTableAdapter = null;
            this.tableAdapterManager.ly_sales_contract_termsTableAdapter = null;
            this.tableAdapterManager.ly_sales_deliver_detailTableAdapter = null;
            this.tableAdapterManager.ly_sales_deliverTableAdapter = null;
            this.tableAdapterManager.ly_sales_groupSingleTableAdapter = null;
            this.tableAdapterManager.ly_sales_groupTableAdapter = null;
            this.tableAdapterManager.ly_sales_test_detail1TableAdapter = null;
            this.tableAdapterManager.ly_sales_test_detailTableAdapter = null;
            this.tableAdapterManager.ly_sales_testTableAdapter = null;
            this.tableAdapterManager.ly_salesdiscountTableAdapter = null;
            this.tableAdapterManager.ly_salespersonTableAdapter = null;
            this.tableAdapterManager.ly_salesregionTableAdapter = null;
            this.tableAdapterManager.UpdateOrder = HappyYF.YuanXin.Data.LYSalseMangeTableAdapters.TableAdapterManager.UpdateOrderOption.InsertUpdateDelete;
            // 
            // ly_express_companyBindingNavigator
            // 
            this.ly_express_companyBindingNavigator.AddNewItem = this.bindingNavigatorAddNewItem;
            this.ly_express_companyBindingNavigator.BindingSource = this.ly_express_companyBindingSource;
            this.ly_express_companyBindingNavigator.CountItem = this.bindingNavigatorCountItem;
            this.ly_express_companyBindingNavigator.DeleteItem = this.bindingNavigatorDeleteItem;
            this.ly_express_companyBindingNavigator.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
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
            this.ly_express_companyBindingNavigatorSaveItem});
            this.ly_express_companyBindingNavigator.Location = new System.Drawing.Point(0, 0);
            this.ly_express_companyBindingNavigator.MoveFirstItem = this.bindingNavigatorMoveFirstItem;
            this.ly_express_companyBindingNavigator.MoveLastItem = this.bindingNavigatorMoveLastItem;
            this.ly_express_companyBindingNavigator.MoveNextItem = this.bindingNavigatorMoveNextItem;
            this.ly_express_companyBindingNavigator.MovePreviousItem = this.bindingNavigatorMovePreviousItem;
            this.ly_express_companyBindingNavigator.Name = "ly_express_companyBindingNavigator";
            this.ly_express_companyBindingNavigator.PositionItem = this.bindingNavigatorPositionItem;
            this.ly_express_companyBindingNavigator.Size = new System.Drawing.Size(605, 25);
            this.ly_express_companyBindingNavigator.TabIndex = 0;
            this.ly_express_companyBindingNavigator.Text = "bindingNavigator1";
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
            // ly_express_companyBindingNavigatorSaveItem
            // 
            this.ly_express_companyBindingNavigatorSaveItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ly_express_companyBindingNavigatorSaveItem.Image = ((System.Drawing.Image)(resources.GetObject("ly_express_companyBindingNavigatorSaveItem.Image")));
            this.ly_express_companyBindingNavigatorSaveItem.Name = "ly_express_companyBindingNavigatorSaveItem";
            this.ly_express_companyBindingNavigatorSaveItem.Size = new System.Drawing.Size(23, 23);
            this.ly_express_companyBindingNavigatorSaveItem.Text = "保存数据";
            this.ly_express_companyBindingNavigatorSaveItem.Click += new System.EventHandler(this.ly_express_companyBindingNavigatorSaveItem_Click);
            // 
            // ly_express_companyDataGridView
            // 
            this.ly_express_companyDataGridView.AllowUserToAddRows = false;
            this.ly_express_companyDataGridView.AutoGenerateColumns = false;
            this.ly_express_companyDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.ly_express_companyDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2,
            this.dataGridViewTextBoxColumn3,
            this.dataGridViewTextBoxColumn4,
            this.dataGridViewTextBoxColumn5});
            this.ly_express_companyDataGridView.DataSource = this.ly_express_companyBindingSource;
            this.ly_express_companyDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ly_express_companyDataGridView.Location = new System.Drawing.Point(0, 25);
            this.ly_express_companyDataGridView.Name = "ly_express_companyDataGridView";
            this.ly_express_companyDataGridView.RowHeadersWidth = 19;
            this.ly_express_companyDataGridView.RowTemplate.Height = 23;
            this.ly_express_companyDataGridView.Size = new System.Drawing.Size(605, 352);
            this.ly_express_companyDataGridView.TabIndex = 1;
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
            this.dataGridViewTextBoxColumn2.DataPropertyName = "编码";
            this.dataGridViewTextBoxColumn2.HeaderText = "编码";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.Width = 60;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.DataPropertyName = "公司名称";
            this.dataGridViewTextBoxColumn3.HeaderText = "公司名称";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.Width = 200;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.DataPropertyName = "快递电话";
            this.dataGridViewTextBoxColumn4.HeaderText = "快递电话";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.DataPropertyName = "快递员";
            this.dataGridViewTextBoxColumn5.HeaderText = "快递员";
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            this.dataGridViewTextBoxColumn5.Width = 80;
            // 
            // LY_Express_Mange
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(605, 377);
            this.Controls.Add(this.ly_express_companyDataGridView);
            this.Controls.Add(this.ly_express_companyBindingNavigator);
            this.Name = "LY_Express_Mange";
            this.Text = "快递公司管理";
            this.Load += new System.EventHandler(this.LY_Salesregion_Mange_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.LY_Salesregion_Mange_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.lYSalseMange)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ly_express_companyBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ly_express_companyBindingNavigator)).EndInit();
            this.ly_express_companyBindingNavigator.ResumeLayout(false);
            this.ly_express_companyBindingNavigator.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ly_express_companyDataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private HappyYF.YuanXin.Data.LYSalseMange lYSalseMange;
        private System.Windows.Forms.BindingSource ly_express_companyBindingSource;
        private HappyYF.YuanXin.Data.LYSalseMangeTableAdapters.ly_express_companyTableAdapter ly_express_companyTableAdapter;
        private HappyYF.YuanXin.Data.LYSalseMangeTableAdapters.TableAdapterManager tableAdapterManager;
        private System.Windows.Forms.BindingNavigator ly_express_companyBindingNavigator;
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
        private System.Windows.Forms.ToolStripButton ly_express_companyBindingNavigatorSaveItem;
        private System.Windows.Forms.DataGridView ly_express_companyDataGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;

    }
}