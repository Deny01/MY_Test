namespace HappyYF.YuanXin.WorkSet
{
    partial class LY_Query_week_cost
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LY_Query_week));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.lYSalseRepair = new HappyYF.YuanXin.Data.LYSalseRepair();
            this.f_Week_infoBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.f_Week_infoTableAdapter = new HappyYF.YuanXin.Data.LYSalseRepairTableAdapters.f_Week_infoTableAdapter();
            this.tableAdapterManager = new HappyYF.YuanXin.Data.LYSalseRepairTableAdapters.TableAdapterManager();
            this.f_Week_infoBindingNavigator = new System.Windows.Forms.BindingNavigator(this.components);
            this.bindingNavigatorCountItem = new System.Windows.Forms.ToolStripLabel();
            this.bindingNavigatorMoveFirstItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorMovePreviousItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.bindingNavigatorPositionItem = new System.Windows.Forms.ToolStripTextBox();
            this.bindingNavigatorSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.bindingNavigatorMoveNextItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorMoveLastItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripTextBox1 = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripTextBox2 = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.f_Week_infoDataGridView = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.起始日期 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.结束日期 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.lYSalseRepair)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.f_Week_infoBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.f_Week_infoBindingNavigator)).BeginInit();
            this.f_Week_infoBindingNavigator.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.f_Week_infoDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // lYSalseRepair
            // 
            this.lYSalseRepair.DataSetName = "LYSalseRepair";
            this.lYSalseRepair.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // f_Week_infoBindingSource
            // 
            this.f_Week_infoBindingSource.DataMember = "f_Week_info";
            this.f_Week_infoBindingSource.DataSource = this.lYSalseRepair;
            // 
            // f_Week_infoTableAdapter
            // 
            this.f_Week_infoTableAdapter.ClearBeforeFill = true;
            // 
            // tableAdapterManager
            // 
            this.tableAdapterManager.BackupDataSetBeforeUpdate = false;
            this.tableAdapterManager.Connection = null;
            this.tableAdapterManager.ly_inform_queryforsalseTableAdapter = null;
            this.tableAdapterManager.ly_inform_queryforstoreTableAdapter = null;
            this.tableAdapterManager.ly_sales_client_RepairTableAdapter = null;
            this.tableAdapterManager.UpdateOrder = HappyYF.YuanXin.Data.LYSalseRepairTableAdapters.TableAdapterManager.UpdateOrderOption.InsertUpdateDelete;
            // 
            // f_Week_infoBindingNavigator
            // 
            this.f_Week_infoBindingNavigator.AddNewItem = null;
            this.f_Week_infoBindingNavigator.BindingSource = this.f_Week_infoBindingSource;
            this.f_Week_infoBindingNavigator.CountItem = this.bindingNavigatorCountItem;
            this.f_Week_infoBindingNavigator.DeleteItem = null;
            this.f_Week_infoBindingNavigator.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.bindingNavigatorMoveFirstItem,
            this.bindingNavigatorMovePreviousItem,
            this.bindingNavigatorSeparator,
            this.bindingNavigatorPositionItem,
            this.bindingNavigatorCountItem,
            this.bindingNavigatorSeparator1,
            this.bindingNavigatorMoveNextItem,
            this.bindingNavigatorMoveLastItem,
            this.bindingNavigatorSeparator2,
            this.toolStripLabel1,
            this.toolStripTextBox1,
            this.toolStripLabel2,
            this.toolStripTextBox2,
            this.toolStripButton1});
            this.f_Week_infoBindingNavigator.Location = new System.Drawing.Point(0, 0);
            this.f_Week_infoBindingNavigator.MoveFirstItem = this.bindingNavigatorMoveFirstItem;
            this.f_Week_infoBindingNavigator.MoveLastItem = this.bindingNavigatorMoveLastItem;
            this.f_Week_infoBindingNavigator.MoveNextItem = this.bindingNavigatorMoveNextItem;
            this.f_Week_infoBindingNavigator.MovePreviousItem = this.bindingNavigatorMovePreviousItem;
            this.f_Week_infoBindingNavigator.Name = "f_Week_infoBindingNavigator";
            this.f_Week_infoBindingNavigator.PositionItem = this.bindingNavigatorPositionItem;
            this.f_Week_infoBindingNavigator.Size = new System.Drawing.Size(564, 25);
            this.f_Week_infoBindingNavigator.TabIndex = 0;
            this.f_Week_infoBindingNavigator.Text = "bindingNavigator1";
            // 
            // bindingNavigatorCountItem
            // 
            this.bindingNavigatorCountItem.Name = "bindingNavigatorCountItem";
            this.bindingNavigatorCountItem.Size = new System.Drawing.Size(35, 22);
            this.bindingNavigatorCountItem.Text = "/ {0}";
            this.bindingNavigatorCountItem.ToolTipText = "总项数";
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
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(59, 22);
            this.toolStripLabel1.Text = "开始年份:";
            // 
            // toolStripTextBox1
            // 
            this.toolStripTextBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.toolStripTextBox1.Name = "toolStripTextBox1";
            this.toolStripTextBox1.Size = new System.Drawing.Size(60, 25);
            // 
            // toolStripLabel2
            // 
            this.toolStripLabel2.Name = "toolStripLabel2";
            this.toolStripLabel2.Size = new System.Drawing.Size(59, 22);
            this.toolStripLabel2.Text = "结束年份:";
            // 
            // toolStripTextBox2
            // 
            this.toolStripTextBox2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.toolStripTextBox2.Name = "toolStripTextBox2";
            this.toolStripTextBox2.Size = new System.Drawing.Size(60, 25);
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(33, 22);
            this.toolStripButton1.Text = "刷新";
            this.toolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // f_Week_infoDataGridView
            // 
            this.f_Week_infoDataGridView.AllowUserToAddRows = false;
            this.f_Week_infoDataGridView.AllowUserToDeleteRows = false;
            this.f_Week_infoDataGridView.AutoGenerateColumns = false;
            this.f_Week_infoDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.f_Week_infoDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn6,
            this.起始日期,
            this.结束日期,
            this.dataGridViewTextBoxColumn2,
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn3});
            this.f_Week_infoDataGridView.DataSource = this.f_Week_infoBindingSource;
            this.f_Week_infoDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.f_Week_infoDataGridView.Location = new System.Drawing.Point(0, 25);
            this.f_Week_infoDataGridView.Name = "f_Week_infoDataGridView";
            this.f_Week_infoDataGridView.ReadOnly = true;
            this.f_Week_infoDataGridView.RowHeadersWidth = 19;
            this.f_Week_infoDataGridView.RowTemplate.Height = 23;
            this.f_Week_infoDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.f_Week_infoDataGridView.Size = new System.Drawing.Size(564, 451);
            this.f_Week_infoDataGridView.TabIndex = 2;
            this.f_Week_infoDataGridView.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.f_Week_infoDataGridView_CellMouseDoubleClick);
            // 
            // dataGridViewTextBoxColumn6
            // 
            this.dataGridViewTextBoxColumn6.DataPropertyName = "年周";
            this.dataGridViewTextBoxColumn6.HeaderText = "年周";
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            this.dataGridViewTextBoxColumn6.ReadOnly = true;
            // 
            // 起始日期
            // 
            this.起始日期.DataPropertyName = "起始日期";
            dataGridViewCellStyle1.Format = "yyyy-MM-dd";
            this.起始日期.DefaultCellStyle = dataGridViewCellStyle1;
            this.起始日期.HeaderText = "起始日期";
            this.起始日期.Name = "起始日期";
            this.起始日期.ReadOnly = true;
            // 
            // 结束日期
            // 
            this.结束日期.DataPropertyName = "结束日期";
            dataGridViewCellStyle2.Format = "yyyy-MM-dd";
            this.结束日期.DefaultCellStyle = dataGridViewCellStyle2;
            this.结束日期.HeaderText = "结束日期";
            this.结束日期.Name = "结束日期";
            this.结束日期.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.DataPropertyName = "年份";
            this.dataGridViewTextBoxColumn2.HeaderText = "年份";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.DataPropertyName = "tSeqID";
            this.dataGridViewTextBoxColumn1.HeaderText = "tSeqID";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn1.Visible = false;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.DataPropertyName = "周序";
            this.dataGridViewTextBoxColumn3.HeaderText = "周序";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            this.dataGridViewTextBoxColumn3.Visible = false;
            // 
            // LY_Query_week
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(564, 476);
            this.Controls.Add(this.f_Week_infoDataGridView);
            this.Controls.Add(this.f_Week_infoBindingNavigator);
            this.Name = "LY_Query_week";
            this.Text = "年周区间选择";
            this.Load += new System.EventHandler(this.LY_Query_week_Load);
            ((System.ComponentModel.ISupportInitialize)(this.lYSalseRepair)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.f_Week_infoBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.f_Week_infoBindingNavigator)).EndInit();
            this.f_Week_infoBindingNavigator.ResumeLayout(false);
            this.f_Week_infoBindingNavigator.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.f_Week_infoDataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private HappyYF.YuanXin.Data.LYSalseRepair lYSalseRepair;
        private System.Windows.Forms.BindingSource f_Week_infoBindingSource;
        private HappyYF.YuanXin.Data.LYSalseRepairTableAdapters.f_Week_infoTableAdapter f_Week_infoTableAdapter;
        private HappyYF.YuanXin.Data.LYSalseRepairTableAdapters.TableAdapterManager tableAdapterManager;
        private System.Windows.Forms.BindingNavigator f_Week_infoBindingNavigator;
        private System.Windows.Forms.ToolStripLabel bindingNavigatorCountItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveFirstItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMovePreviousItem;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator;
        private System.Windows.Forms.ToolStripTextBox bindingNavigatorPositionItem;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator1;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveNextItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveLastItem;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator2;
        private System.Windows.Forms.DataGridView f_Week_infoDataGridView;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripTextBox toolStripTextBox1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel2;
        private System.Windows.Forms.ToolStripTextBox toolStripTextBox2;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private System.Windows.Forms.DataGridViewTextBoxColumn 起始日期;
        private System.Windows.Forms.DataGridViewTextBoxColumn 结束日期;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
    }
}