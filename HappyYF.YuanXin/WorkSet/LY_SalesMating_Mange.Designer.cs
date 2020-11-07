namespace HappyYF.YuanXin.WorkSet
{
    partial class LY_SalesMating_Mange
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LY_SalesMating_Mange));
            this.lYSalseMange2 = new HappyYF.YuanXin.Data.LYSalseMange2();
            this.ly_sales_mating_setBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.ly_sales_mating_setTableAdapter = new HappyYF.YuanXin.Data.LYSalseMange2TableAdapters.ly_sales_mating_setTableAdapter();
            this.tableAdapterManager = new HappyYF.YuanXin.Data.LYSalseMange2TableAdapters.TableAdapterManager();
            this.ly_sales_mating_setBindingNavigator = new System.Windows.Forms.BindingNavigator(this.components);
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
            this.ly_sales_mating_setBindingNavigatorSaveItem = new System.Windows.Forms.ToolStripButton();
            this.ly_sales_mating_setDataGridView = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.lYSalseMange2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ly_sales_mating_setBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ly_sales_mating_setBindingNavigator)).BeginInit();
            this.ly_sales_mating_setBindingNavigator.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ly_sales_mating_setDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // lYSalseMange2
            // 
            this.lYSalseMange2.DataSetName = "LYSalseMange2";
            this.lYSalseMange2.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // ly_sales_mating_setBindingSource
            // 
            this.ly_sales_mating_setBindingSource.DataMember = "ly_sales_mating_set";
            this.ly_sales_mating_setBindingSource.DataSource = this.lYSalseMange2;
            // 
            // ly_sales_mating_setTableAdapter
            // 
            this.ly_sales_mating_setTableAdapter.ClearBeforeFill = true;
            // 
            // tableAdapterManager
            // 
            this.tableAdapterManager.BackupDataSetBeforeUpdate = false;
            this.tableAdapterManager.ly_material_plan_main_forperiodTableAdapter = null;
            this.tableAdapterManager.ly_plan_getmaterial_departmentTableAdapter = null;
            this.tableAdapterManager.ly_sales_mating_setTableAdapter = this.ly_sales_mating_setTableAdapter;
            this.tableAdapterManager.ly_sales_receive_singleTableAdapter = null;
            this.tableAdapterManager.ly_sales_receiveTableAdapter = null;
            this.tableAdapterManager.UpdateOrder = HappyYF.YuanXin.Data.LYSalseMange2TableAdapters.TableAdapterManager.UpdateOrderOption.InsertUpdateDelete;
            // 
            // ly_sales_mating_setBindingNavigator
            // 
            this.ly_sales_mating_setBindingNavigator.AddNewItem = this.bindingNavigatorAddNewItem;
            this.ly_sales_mating_setBindingNavigator.BindingSource = this.ly_sales_mating_setBindingSource;
            this.ly_sales_mating_setBindingNavigator.CountItem = this.bindingNavigatorCountItem;
            this.ly_sales_mating_setBindingNavigator.DeleteItem = this.bindingNavigatorDeleteItem;
            this.ly_sales_mating_setBindingNavigator.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
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
            this.ly_sales_mating_setBindingNavigatorSaveItem});
            this.ly_sales_mating_setBindingNavigator.Location = new System.Drawing.Point(0, 0);
            this.ly_sales_mating_setBindingNavigator.MoveFirstItem = this.bindingNavigatorMoveFirstItem;
            this.ly_sales_mating_setBindingNavigator.MoveLastItem = this.bindingNavigatorMoveLastItem;
            this.ly_sales_mating_setBindingNavigator.MoveNextItem = this.bindingNavigatorMoveNextItem;
            this.ly_sales_mating_setBindingNavigator.MovePreviousItem = this.bindingNavigatorMovePreviousItem;
            this.ly_sales_mating_setBindingNavigator.Name = "ly_sales_mating_setBindingNavigator";
            this.ly_sales_mating_setBindingNavigator.PositionItem = this.bindingNavigatorPositionItem;
            this.ly_sales_mating_setBindingNavigator.Size = new System.Drawing.Size(555, 25);
            this.ly_sales_mating_setBindingNavigator.TabIndex = 0;
            this.ly_sales_mating_setBindingNavigator.Text = "bindingNavigator1";
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
            // ly_sales_mating_setBindingNavigatorSaveItem
            // 
            this.ly_sales_mating_setBindingNavigatorSaveItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ly_sales_mating_setBindingNavigatorSaveItem.Image = ((System.Drawing.Image)(resources.GetObject("ly_sales_mating_setBindingNavigatorSaveItem.Image")));
            this.ly_sales_mating_setBindingNavigatorSaveItem.Name = "ly_sales_mating_setBindingNavigatorSaveItem";
            this.ly_sales_mating_setBindingNavigatorSaveItem.Size = new System.Drawing.Size(23, 23);
            this.ly_sales_mating_setBindingNavigatorSaveItem.Text = "保存数据";
            this.ly_sales_mating_setBindingNavigatorSaveItem.Click += new System.EventHandler(this.ly_sales_mating_setBindingNavigatorSaveItem_Click);
            // 
            // ly_sales_mating_setDataGridView
            // 
            this.ly_sales_mating_setDataGridView.AllowUserToAddRows = false;
            this.ly_sales_mating_setDataGridView.AllowUserToDeleteRows = false;
            this.ly_sales_mating_setDataGridView.AutoGenerateColumns = false;
            this.ly_sales_mating_setDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.ly_sales_mating_setDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2,
            this.dataGridViewTextBoxColumn3,
            this.dataGridViewTextBoxColumn4,
            this.dataGridViewTextBoxColumn5});
            this.ly_sales_mating_setDataGridView.DataSource = this.ly_sales_mating_setBindingSource;
            this.ly_sales_mating_setDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ly_sales_mating_setDataGridView.Location = new System.Drawing.Point(0, 25);
            this.ly_sales_mating_setDataGridView.Name = "ly_sales_mating_setDataGridView";
            this.ly_sales_mating_setDataGridView.RowHeadersWidth = 19;
            this.ly_sales_mating_setDataGridView.RowTemplate.Height = 23;
            this.ly_sales_mating_setDataGridView.Size = new System.Drawing.Size(555, 342);
            this.ly_sales_mating_setDataGridView.TabIndex = 1;
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
            this.dataGridViewTextBoxColumn2.Width = 70;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.DataPropertyName = "名称";
            this.dataGridViewTextBoxColumn3.HeaderText = "名称";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.DataPropertyName = "标配数量";
            this.dataGridViewTextBoxColumn4.HeaderText = "标配数量";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.DataPropertyName = "允许赠送";
            this.dataGridViewTextBoxColumn5.HeaderText = "允许赠送";
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            // 
            // LY_SalesMating_Mange
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(555, 367);
            this.Controls.Add(this.ly_sales_mating_setDataGridView);
            this.Controls.Add(this.ly_sales_mating_setBindingNavigator);
            this.Name = "LY_SalesMating_Mange";
            this.Text = "配套备件设置";
            this.Load += new System.EventHandler(this.LY_SalesMating_Mange_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.LY_SalesMating_Mange_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.lYSalseMange2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ly_sales_mating_setBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ly_sales_mating_setBindingNavigator)).EndInit();
            this.ly_sales_mating_setBindingNavigator.ResumeLayout(false);
            this.ly_sales_mating_setBindingNavigator.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ly_sales_mating_setDataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private HappyYF.YuanXin.Data.LYSalseMange2 lYSalseMange2;
        private System.Windows.Forms.BindingSource ly_sales_mating_setBindingSource;
        private HappyYF.YuanXin.Data.LYSalseMange2TableAdapters.ly_sales_mating_setTableAdapter ly_sales_mating_setTableAdapter;
        private HappyYF.YuanXin.Data.LYSalseMange2TableAdapters.TableAdapterManager tableAdapterManager;
        private System.Windows.Forms.BindingNavigator ly_sales_mating_setBindingNavigator;
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
        private System.Windows.Forms.ToolStripButton ly_sales_mating_setBindingNavigatorSaveItem;
        private System.Windows.Forms.DataGridView ly_sales_mating_setDataGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
    }
}