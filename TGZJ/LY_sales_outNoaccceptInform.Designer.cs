namespace TGZJ
{
    partial class LY_sales_outNoaccceptInform
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LY_sales_outNoaccceptInform));
            this.ly_inform_recordBindingNavigator = new System.Windows.Forms.BindingNavigator(this.components);
            this.ly_inform_noacceptBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.informDataSet = new TGZJ.DATA.InformDataSet();
            this.bindingNavigatorCountItem = new System.Windows.Forms.ToolStripLabel();
            this.bindingNavigatorMoveFirstItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorMovePreviousItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.bindingNavigatorPositionItem = new System.Windows.Forms.ToolStripTextBox();
            this.bindingNavigatorSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.bindingNavigatorMoveNextItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorMoveLastItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.ly_inform_recordDataGridView = new System.Windows.Forms.DataGridView();
            this.tableAdapterManager1 = new TGZJ.DATA.InformDataSetTableAdapters.TableAdapterManager();
            this.ly_inform_noacceptTableAdapter = new TGZJ.DATA.InformDataSetTableAdapters.ly_inform_noacceptTableAdapter();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.依赖书号 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.合同编号 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.清单编号 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.收到 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dataGridViewTextBoxColumn12 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.接收时间 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.客户 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewCheckBoxColumn2 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dataGridViewTextBoxColumn8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.ly_inform_recordBindingNavigator)).BeginInit();
            this.ly_inform_recordBindingNavigator.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ly_inform_noacceptBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.informDataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ly_inform_recordDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // ly_inform_recordBindingNavigator
            // 
            this.ly_inform_recordBindingNavigator.AddNewItem = null;
            this.ly_inform_recordBindingNavigator.BindingSource = this.ly_inform_noacceptBindingSource;
            this.ly_inform_recordBindingNavigator.CountItem = this.bindingNavigatorCountItem;
            this.ly_inform_recordBindingNavigator.DeleteItem = null;
            this.ly_inform_recordBindingNavigator.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.bindingNavigatorMoveFirstItem,
            this.bindingNavigatorMovePreviousItem,
            this.bindingNavigatorSeparator,
            this.bindingNavigatorPositionItem,
            this.bindingNavigatorCountItem,
            this.bindingNavigatorSeparator1,
            this.bindingNavigatorMoveNextItem,
            this.bindingNavigatorMoveLastItem,
            this.bindingNavigatorSeparator2});
            this.ly_inform_recordBindingNavigator.Location = new System.Drawing.Point(0, 0);
            this.ly_inform_recordBindingNavigator.MoveFirstItem = this.bindingNavigatorMoveFirstItem;
            this.ly_inform_recordBindingNavigator.MoveLastItem = this.bindingNavigatorMoveLastItem;
            this.ly_inform_recordBindingNavigator.MoveNextItem = this.bindingNavigatorMoveNextItem;
            this.ly_inform_recordBindingNavigator.MovePreviousItem = this.bindingNavigatorMovePreviousItem;
            this.ly_inform_recordBindingNavigator.Name = "ly_inform_recordBindingNavigator";
            this.ly_inform_recordBindingNavigator.PositionItem = this.bindingNavigatorPositionItem;
            this.ly_inform_recordBindingNavigator.Size = new System.Drawing.Size(978, 25);
            this.ly_inform_recordBindingNavigator.TabIndex = 0;
            this.ly_inform_recordBindingNavigator.Text = "bindingNavigator1";
            // 
            // ly_inform_noacceptBindingSource
            // 
            this.ly_inform_noacceptBindingSource.DataMember = "ly_inform_noaccept";
            this.ly_inform_noacceptBindingSource.DataSource = this.informDataSet;
            // 
            // informDataSet
            // 
            this.informDataSet.DataSetName = "InformDataSet";
            this.informDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
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
            // ly_inform_recordDataGridView
            // 
            this.ly_inform_recordDataGridView.AllowUserToAddRows = false;
            this.ly_inform_recordDataGridView.AllowUserToDeleteRows = false;
            this.ly_inform_recordDataGridView.AutoGenerateColumns = false;
            this.ly_inform_recordDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.ly_inform_recordDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn7,
            this.dataGridViewTextBoxColumn2,
            this.dataGridViewTextBoxColumn3,
            this.依赖书号,
            this.合同编号,
            this.清单编号,
            this.收到,
            this.dataGridViewTextBoxColumn12,
            this.接收时间,
            this.客户,
            this.dataGridViewCheckBoxColumn2,
            this.dataGridViewTextBoxColumn8,
            this.dataGridViewTextBoxColumn6,
            this.dataGridViewTextBoxColumn4});
            this.ly_inform_recordDataGridView.DataSource = this.ly_inform_noacceptBindingSource;
            this.ly_inform_recordDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ly_inform_recordDataGridView.Location = new System.Drawing.Point(0, 25);
            this.ly_inform_recordDataGridView.Name = "ly_inform_recordDataGridView";
            this.ly_inform_recordDataGridView.ReadOnly = true;
            this.ly_inform_recordDataGridView.RowHeadersWidth = 19;
            this.ly_inform_recordDataGridView.RowTemplate.Height = 23;
            this.ly_inform_recordDataGridView.Size = new System.Drawing.Size(978, 483);
            this.ly_inform_recordDataGridView.TabIndex = 2;
            this.ly_inform_recordDataGridView.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.ly_inform_recordDataGridView_CellMouseDoubleClick);
            this.ly_inform_recordDataGridView.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.ly_inform_recordDataGridView_CellContentClick);
            // 
            // tableAdapterManager1
            // 
            this.tableAdapterManager1.BackupDataSetBeforeUpdate = false;
            this.tableAdapterManager1.Connection = null;
            this.tableAdapterManager1.ly_inform_noacceptTableAdapter = null;
            this.tableAdapterManager1.ly_inform_recordTableAdapter = null;
            this.tableAdapterManager1.UpdateOrder = TGZJ.DATA.InformDataSetTableAdapters.TableAdapterManager.UpdateOrderOption.InsertUpdateDelete;
            // 
            // ly_inform_noacceptTableAdapter
            // 
            this.ly_inform_noacceptTableAdapter.ClearBeforeFill = true;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.DataPropertyName = "id";
            this.dataGridViewTextBoxColumn1.HeaderText = "id";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn1.Visible = false;
            // 
            // dataGridViewTextBoxColumn7
            // 
            this.dataGridViewTextBoxColumn7.DataPropertyName = "通知类别";
            this.dataGridViewTextBoxColumn7.HeaderText = "通知类别";
            this.dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
            this.dataGridViewTextBoxColumn7.ReadOnly = true;
            this.dataGridViewTextBoxColumn7.Width = 80;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.DataPropertyName = "通知人";
            this.dataGridViewTextBoxColumn2.HeaderText = "通知人";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            this.dataGridViewTextBoxColumn2.Width = 70;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.DataPropertyName = "通知时间";
            this.dataGridViewTextBoxColumn3.HeaderText = "通知时间";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            // 
            // 依赖书号
            // 
            this.依赖书号.DataPropertyName = "依赖书号";
            this.依赖书号.HeaderText = "依赖书号";
            this.依赖书号.Name = "依赖书号";
            this.依赖书号.ReadOnly = true;
            // 
            // 合同编号
            // 
            this.合同编号.DataPropertyName = "合同编号";
            this.合同编号.HeaderText = "合同编号";
            this.合同编号.Name = "合同编号";
            this.合同编号.ReadOnly = true;
            // 
            // 清单编号
            // 
            this.清单编号.DataPropertyName = "清单编号";
            this.清单编号.HeaderText = "清单编号";
            this.清单编号.Name = "清单编号";
            this.清单编号.ReadOnly = true;
            // 
            // 收到
            // 
            this.收到.DataPropertyName = "收到";
            this.收到.HeaderText = "收到";
            this.收到.Name = "收到";
            this.收到.ReadOnly = true;
            this.收到.Width = 39;
            // 
            // dataGridViewTextBoxColumn12
            // 
            this.dataGridViewTextBoxColumn12.DataPropertyName = "接收人";
            this.dataGridViewTextBoxColumn12.HeaderText = "接收人";
            this.dataGridViewTextBoxColumn12.Name = "dataGridViewTextBoxColumn12";
            this.dataGridViewTextBoxColumn12.ReadOnly = true;
            this.dataGridViewTextBoxColumn12.Width = 70;
            // 
            // 接收时间
            // 
            this.接收时间.DataPropertyName = "接收时间";
            this.接收时间.HeaderText = "接收时间";
            this.接收时间.Name = "接收时间";
            this.接收时间.ReadOnly = true;
            // 
            // 客户
            // 
            this.客户.DataPropertyName = "客户";
            this.客户.HeaderText = "客户";
            this.客户.Name = "客户";
            this.客户.ReadOnly = true;
            // 
            // dataGridViewCheckBoxColumn2
            // 
            this.dataGridViewCheckBoxColumn2.DataPropertyName = "营业确认";
            this.dataGridViewCheckBoxColumn2.HeaderText = "营业确认";
            this.dataGridViewCheckBoxColumn2.Name = "dataGridViewCheckBoxColumn2";
            this.dataGridViewCheckBoxColumn2.ReadOnly = true;
            this.dataGridViewCheckBoxColumn2.Visible = false;
            // 
            // dataGridViewTextBoxColumn8
            // 
            this.dataGridViewTextBoxColumn8.DataPropertyName = "营业确认时间";
            this.dataGridViewTextBoxColumn8.HeaderText = "确认时间";
            this.dataGridViewTextBoxColumn8.Name = "dataGridViewTextBoxColumn8";
            this.dataGridViewTextBoxColumn8.ReadOnly = true;
            this.dataGridViewTextBoxColumn8.Visible = false;
            // 
            // dataGridViewTextBoxColumn6
            // 
            this.dataGridViewTextBoxColumn6.DataPropertyName = "acceptpersoncode";
            this.dataGridViewTextBoxColumn6.HeaderText = "acceptpersoncode";
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            this.dataGridViewTextBoxColumn6.ReadOnly = true;
            this.dataGridViewTextBoxColumn6.Visible = false;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.DataPropertyName = "接收人编码";
            this.dataGridViewTextBoxColumn4.HeaderText = "接收人编码";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.ReadOnly = true;
            this.dataGridViewTextBoxColumn4.Visible = false;
            // 
            // LY_sales_outNoaccceptInform
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(978, 508);
            this.Controls.Add(this.ly_inform_recordDataGridView);
            this.Controls.Add(this.ly_inform_recordBindingNavigator);
            this.Name = "LY_sales_outNoaccceptInform";
            this.Text = "营业发货通知未到";
            this.Load += new System.EventHandler(this.LY_sales_outInform_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ly_inform_recordBindingNavigator)).EndInit();
            this.ly_inform_recordBindingNavigator.ResumeLayout(false);
            this.ly_inform_recordBindingNavigator.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ly_inform_noacceptBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.informDataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ly_inform_recordDataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.BindingNavigator ly_inform_recordBindingNavigator;
        private System.Windows.Forms.ToolStripLabel bindingNavigatorCountItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveFirstItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMovePreviousItem;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator;
        private System.Windows.Forms.ToolStripTextBox bindingNavigatorPositionItem;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator1;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveNextItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveLastItem;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator2;
        private System.Windows.Forms.DataGridView ly_inform_recordDataGridView;
        private TGZJ.DATA.InformDataSet informDataSet;
        private TGZJ.DATA.InformDataSetTableAdapters.TableAdapterManager tableAdapterManager1;
        private System.Windows.Forms.BindingSource ly_inform_noacceptBindingSource;
        private TGZJ.DATA.InformDataSetTableAdapters.ly_inform_noacceptTableAdapter ly_inform_noacceptTableAdapter;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn 依赖书号;
        private System.Windows.Forms.DataGridViewTextBoxColumn 合同编号;
        private System.Windows.Forms.DataGridViewTextBoxColumn 清单编号;
        private System.Windows.Forms.DataGridViewCheckBoxColumn 收到;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn12;
        private System.Windows.Forms.DataGridViewTextBoxColumn 接收时间;
        private System.Windows.Forms.DataGridViewTextBoxColumn 客户;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn8;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
    }
}