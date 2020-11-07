namespace TGZJ.Manger
{
    partial class CPUAuthorization
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CPUAuthorization));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.machineAuthorization = new TGZJ.DATA.MachineAuthorization();
            this.t_AuthorizationBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.t_AuthorizationTableAdapter = new TGZJ.DATA.MachineAuthorizationTableAdapters.T_AuthorizationTableAdapter();
            this.tableAdapterManager = new TGZJ.DATA.MachineAuthorizationTableAdapters.TableAdapterManager();
            this.t_AuthorizationBindingNavigator = new System.Windows.Forms.BindingNavigator(this.components);
            this.bindingNavigatorCountItem = new System.Windows.Forms.ToolStripLabel();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorMoveFirstItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorMovePreviousItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.bindingNavigatorPositionItem = new System.Windows.Forms.ToolStripTextBox();
            this.bindingNavigatorSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.bindingNavigatorMoveNextItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorMoveLastItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.t_AuthorizationBindingNavigatorSaveItem = new System.Windows.Forms.ToolStripButton();
            this.t_AuthorizationDataGridView = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewCheckBoxColumn1 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.machineAuthorization)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.t_AuthorizationBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.t_AuthorizationBindingNavigator)).BeginInit();
            this.t_AuthorizationBindingNavigator.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.t_AuthorizationDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // machineAuthorization
            // 
            this.machineAuthorization.DataSetName = "MachineAuthorization";
            this.machineAuthorization.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // t_AuthorizationBindingSource
            // 
            this.t_AuthorizationBindingSource.DataMember = "T_Authorization";
            this.t_AuthorizationBindingSource.DataSource = this.machineAuthorization;
            // 
            // t_AuthorizationTableAdapter
            // 
            this.t_AuthorizationTableAdapter.ClearBeforeFill = true;
            // 
            // tableAdapterManager
            // 
            this.tableAdapterManager.BackupDataSetBeforeUpdate = false;
            this.tableAdapterManager.T_AuthorizationTableAdapter = this.t_AuthorizationTableAdapter;
            this.tableAdapterManager.UpdateOrder = TGZJ.DATA.MachineAuthorizationTableAdapters.TableAdapterManager.UpdateOrderOption.InsertUpdateDelete;
            // 
            // t_AuthorizationBindingNavigator
            // 
            this.t_AuthorizationBindingNavigator.AddNewItem = null;
            this.t_AuthorizationBindingNavigator.BindingSource = this.t_AuthorizationBindingSource;
            this.t_AuthorizationBindingNavigator.CountItem = this.bindingNavigatorCountItem;
            this.t_AuthorizationBindingNavigator.DeleteItem = this.toolStripButton1;
            this.t_AuthorizationBindingNavigator.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.bindingNavigatorMoveFirstItem,
            this.bindingNavigatorMovePreviousItem,
            this.bindingNavigatorSeparator,
            this.bindingNavigatorPositionItem,
            this.bindingNavigatorCountItem,
            this.bindingNavigatorSeparator1,
            this.bindingNavigatorMoveNextItem,
            this.bindingNavigatorMoveLastItem,
            this.bindingNavigatorSeparator2,
            this.toolStripButton1,
            this.t_AuthorizationBindingNavigatorSaveItem});
            this.t_AuthorizationBindingNavigator.Location = new System.Drawing.Point(0, 0);
            this.t_AuthorizationBindingNavigator.MoveFirstItem = this.bindingNavigatorMoveFirstItem;
            this.t_AuthorizationBindingNavigator.MoveLastItem = this.bindingNavigatorMoveLastItem;
            this.t_AuthorizationBindingNavigator.MoveNextItem = this.bindingNavigatorMoveNextItem;
            this.t_AuthorizationBindingNavigator.MovePreviousItem = this.bindingNavigatorMovePreviousItem;
            this.t_AuthorizationBindingNavigator.Name = "t_AuthorizationBindingNavigator";
            this.t_AuthorizationBindingNavigator.PositionItem = this.bindingNavigatorPositionItem;
            this.t_AuthorizationBindingNavigator.Size = new System.Drawing.Size(605, 25);
            this.t_AuthorizationBindingNavigator.TabIndex = 0;
            this.t_AuthorizationBindingNavigator.Text = "bindingNavigator1";
            // 
            // bindingNavigatorCountItem
            // 
            this.bindingNavigatorCountItem.Name = "bindingNavigatorCountItem";
            this.bindingNavigatorCountItem.Size = new System.Drawing.Size(35, 22);
            this.bindingNavigatorCountItem.Text = "/ {0}";
            this.bindingNavigatorCountItem.ToolTipText = "总项数";
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.RightToLeftAutoMirrorImage = true;
            this.toolStripButton1.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton1.Text = "删除";
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
            // t_AuthorizationBindingNavigatorSaveItem
            // 
            this.t_AuthorizationBindingNavigatorSaveItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.t_AuthorizationBindingNavigatorSaveItem.Image = ((System.Drawing.Image)(resources.GetObject("t_AuthorizationBindingNavigatorSaveItem.Image")));
            this.t_AuthorizationBindingNavigatorSaveItem.Name = "t_AuthorizationBindingNavigatorSaveItem";
            this.t_AuthorizationBindingNavigatorSaveItem.Size = new System.Drawing.Size(23, 22);
            this.t_AuthorizationBindingNavigatorSaveItem.Text = "保存数据";
            this.t_AuthorizationBindingNavigatorSaveItem.Click += new System.EventHandler(this.t_AuthorizationBindingNavigatorSaveItem_Click);
            // 
            // t_AuthorizationDataGridView
            // 
            this.t_AuthorizationDataGridView.AllowUserToAddRows = false;
            this.t_AuthorizationDataGridView.AllowUserToDeleteRows = false;
            this.t_AuthorizationDataGridView.AutoGenerateColumns = false;
            this.t_AuthorizationDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.t_AuthorizationDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn2,
            this.dataGridViewTextBoxColumn3,
            this.dataGridViewCheckBoxColumn1,
            this.dataGridViewTextBoxColumn4});
            this.t_AuthorizationDataGridView.DataSource = this.t_AuthorizationBindingSource;
            this.t_AuthorizationDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.t_AuthorizationDataGridView.Location = new System.Drawing.Point(0, 25);
            this.t_AuthorizationDataGridView.Name = "t_AuthorizationDataGridView";
            this.t_AuthorizationDataGridView.RowHeadersWidth = 19;
            this.t_AuthorizationDataGridView.RowTemplate.Height = 23;
            this.t_AuthorizationDataGridView.Size = new System.Drawing.Size(605, 347);
            this.t_AuthorizationDataGridView.TabIndex = 1;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.DataPropertyName = "cpu_id";
            this.dataGridViewTextBoxColumn2.FillWeight = 250F;
            this.dataGridViewTextBoxColumn2.HeaderText = "CPU_编号";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            this.dataGridViewTextBoxColumn2.Width = 250;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.DataPropertyName = "user_name";
            this.dataGridViewTextBoxColumn3.HeaderText = "用户名称";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            // 
            // dataGridViewCheckBoxColumn1
            // 
            this.dataGridViewCheckBoxColumn1.DataPropertyName = "permit";
            this.dataGridViewCheckBoxColumn1.FillWeight = 80F;
            this.dataGridViewCheckBoxColumn1.HeaderText = "授权";
            this.dataGridViewCheckBoxColumn1.Name = "dataGridViewCheckBoxColumn1";
            this.dataGridViewCheckBoxColumn1.Width = 80;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.DataPropertyName = "sys_date";
            dataGridViewCellStyle1.Format = "G";
            dataGridViewCellStyle1.NullValue = null;
            this.dataGridViewTextBoxColumn4.DefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridViewTextBoxColumn4.FillWeight = 150F;
            this.dataGridViewTextBoxColumn4.HeaderText = "系统时间";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.ReadOnly = true;
            this.dataGridViewTextBoxColumn4.Width = 150;
            // 
            // CPUAuthorization
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(605, 372);
            this.Controls.Add(this.t_AuthorizationDataGridView);
            this.Controls.Add(this.t_AuthorizationBindingNavigator);
            this.Name = "CPUAuthorization";
            this.Text = "机器授权";
            this.Load += new System.EventHandler(this.CPUAuthorization_Load);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.CPUAuthorization_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.machineAuthorization)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.t_AuthorizationBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.t_AuthorizationBindingNavigator)).EndInit();
            this.t_AuthorizationBindingNavigator.ResumeLayout(false);
            this.t_AuthorizationBindingNavigator.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.t_AuthorizationDataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private TGZJ.DATA.MachineAuthorization machineAuthorization;
        private System.Windows.Forms.BindingSource t_AuthorizationBindingSource;
        private TGZJ.DATA.MachineAuthorizationTableAdapters.T_AuthorizationTableAdapter t_AuthorizationTableAdapter;
        private TGZJ.DATA.MachineAuthorizationTableAdapters.TableAdapterManager tableAdapterManager;
        private System.Windows.Forms.BindingNavigator t_AuthorizationBindingNavigator;
        private System.Windows.Forms.ToolStripLabel bindingNavigatorCountItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveFirstItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMovePreviousItem;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator;
        private System.Windows.Forms.ToolStripTextBox bindingNavigatorPositionItem;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator1;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveNextItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveLastItem;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator2;
        private System.Windows.Forms.ToolStripButton t_AuthorizationBindingNavigatorSaveItem;
        private System.Windows.Forms.DataGridView t_AuthorizationDataGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
    }
}