namespace HappyYF.YuanXin.WorkSet
{
    partial class SumCard
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SumCard));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.sumCard_DataSet = new HappyYF.YuanXin.Data.SumCard_DataSet();
            this.sum_Count_ViewBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.sum_Count_ViewTableAdapter = new HappyYF.YuanXin.Data.SumCard_DataSetTableAdapters.Sum_Count_ViewTableAdapter();
            this.tableAdapterManager = new HappyYF.YuanXin.Data.SumCard_DataSetTableAdapters.TableAdapterManager();
            this.sum_Count_ViewBindingNavigator = new System.Windows.Forms.BindingNavigator(this.components);
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
            this.sum_Count_ViewBindingNavigatorSaveItem = new System.Windows.Forms.ToolStripButton();
            this.sum_Count_ViewDataGridView = new System.Windows.Forms.DataGridView();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.卡号 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.姓名 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.卡面金额 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.标准学费 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.卡面余额 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.本卡折扣 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.实充金额 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.实收学费 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.实充余额 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.已上应收 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.已上实收 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.已上折扣 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.性别 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.年龄 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.生日 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.学校 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.年级 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.家长姓名 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.联系电话 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.家庭住址 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.家长单位 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.是否退卡 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.办卡时间 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.sumCard_DataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sum_Count_ViewBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sum_Count_ViewBindingNavigator)).BeginInit();
            this.sum_Count_ViewBindingNavigator.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.sum_Count_ViewDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // sumCard_DataSet
            // 
            this.sumCard_DataSet.DataSetName = "SumCard_DataSet";
            this.sumCard_DataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // sum_Count_ViewBindingSource
            // 
            this.sum_Count_ViewBindingSource.DataMember = "Sum_Count_View";
            this.sum_Count_ViewBindingSource.DataSource = this.sumCard_DataSet;
            // 
            // sum_Count_ViewTableAdapter
            // 
            this.sum_Count_ViewTableAdapter.ClearBeforeFill = true;
            // 
            // tableAdapterManager
            // 
            this.tableAdapterManager.BackupDataSetBeforeUpdate = false;
            this.tableAdapterManager.Connection = null;
            this.tableAdapterManager.UpdateOrder = HappyYF.YuanXin.Data.SumCard_DataSetTableAdapters.TableAdapterManager.UpdateOrderOption.InsertUpdateDelete;
            // 
            // sum_Count_ViewBindingNavigator
            // 
            this.sum_Count_ViewBindingNavigator.AddNewItem = this.bindingNavigatorAddNewItem;
            this.sum_Count_ViewBindingNavigator.BindingSource = this.sum_Count_ViewBindingSource;
            this.sum_Count_ViewBindingNavigator.CountItem = this.bindingNavigatorCountItem;
            this.sum_Count_ViewBindingNavigator.DeleteItem = this.bindingNavigatorDeleteItem;
            this.sum_Count_ViewBindingNavigator.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
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
            this.sum_Count_ViewBindingNavigatorSaveItem,
            this.toolStripSeparator1,
            this.toolStripButton1});
            this.sum_Count_ViewBindingNavigator.Location = new System.Drawing.Point(0, 0);
            this.sum_Count_ViewBindingNavigator.MoveFirstItem = this.bindingNavigatorMoveFirstItem;
            this.sum_Count_ViewBindingNavigator.MoveLastItem = this.bindingNavigatorMoveLastItem;
            this.sum_Count_ViewBindingNavigator.MoveNextItem = this.bindingNavigatorMoveNextItem;
            this.sum_Count_ViewBindingNavigator.MovePreviousItem = this.bindingNavigatorMovePreviousItem;
            this.sum_Count_ViewBindingNavigator.Name = "sum_Count_ViewBindingNavigator";
            this.sum_Count_ViewBindingNavigator.PositionItem = this.bindingNavigatorPositionItem;
            this.sum_Count_ViewBindingNavigator.Size = new System.Drawing.Size(706, 25);
            this.sum_Count_ViewBindingNavigator.TabIndex = 0;
            this.sum_Count_ViewBindingNavigator.Text = "bindingNavigator1";
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
            // sum_Count_ViewBindingNavigatorSaveItem
            // 
            this.sum_Count_ViewBindingNavigatorSaveItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.sum_Count_ViewBindingNavigatorSaveItem.Enabled = false;
            this.sum_Count_ViewBindingNavigatorSaveItem.Image = ((System.Drawing.Image)(resources.GetObject("sum_Count_ViewBindingNavigatorSaveItem.Image")));
            this.sum_Count_ViewBindingNavigatorSaveItem.Name = "sum_Count_ViewBindingNavigatorSaveItem";
            this.sum_Count_ViewBindingNavigatorSaveItem.Size = new System.Drawing.Size(23, 22);
            this.sum_Count_ViewBindingNavigatorSaveItem.Text = "保存数据";
            // 
            // sum_Count_ViewDataGridView
            // 
            this.sum_Count_ViewDataGridView.AllowUserToAddRows = false;
            this.sum_Count_ViewDataGridView.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.sum_Count_ViewDataGridView.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.sum_Count_ViewDataGridView.AutoGenerateColumns = false;
            this.sum_Count_ViewDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.sum_Count_ViewDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.卡号,
            this.姓名,
            this.卡面金额,
            this.标准学费,
            this.卡面余额,
            this.本卡折扣,
            this.实充金额,
            this.实收学费,
            this.实充余额,
            this.已上应收,
            this.已上实收,
            this.已上折扣,
            this.性别,
            this.年龄,
            this.生日,
            this.学校,
            this.年级,
            this.家长姓名,
            this.联系电话,
            this.家庭住址,
            this.家长单位,
            this.是否退卡,
            this.办卡时间});
            this.sum_Count_ViewDataGridView.DataSource = this.sum_Count_ViewBindingSource;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.Teal;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.sum_Count_ViewDataGridView.DefaultCellStyle = dataGridViewCellStyle2;
            this.sum_Count_ViewDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sum_Count_ViewDataGridView.Location = new System.Drawing.Point(0, 25);
            this.sum_Count_ViewDataGridView.Name = "sum_Count_ViewDataGridView";
            this.sum_Count_ViewDataGridView.ReadOnly = true;
            this.sum_Count_ViewDataGridView.RowHeadersWidth = 19;
            this.sum_Count_ViewDataGridView.RowTemplate.Height = 23;
            this.sum_Count_ViewDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.sum_Count_ViewDataGridView.Size = new System.Drawing.Size(706, 273);
            this.sum_Count_ViewDataGridView.TabIndex = 1;
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(45, 22);
            this.toolStripButton1.Text = "Excell";
            this.toolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // 卡号
            // 
            this.卡号.DataPropertyName = "卡号";
            this.卡号.FillWeight = 60F;
            this.卡号.HeaderText = "卡号";
            this.卡号.Name = "卡号";
            this.卡号.ReadOnly = true;
            this.卡号.Width = 60;
            // 
            // 姓名
            // 
            this.姓名.DataPropertyName = "姓名";
            this.姓名.FillWeight = 60F;
            this.姓名.HeaderText = "姓名";
            this.姓名.Name = "姓名";
            this.姓名.ReadOnly = true;
            this.姓名.Width = 60;
            // 
            // 卡面金额
            // 
            this.卡面金额.DataPropertyName = "卡面金额";
            this.卡面金额.FillWeight = 80F;
            this.卡面金额.HeaderText = "卡面金额";
            this.卡面金额.Name = "卡面金额";
            this.卡面金额.ReadOnly = true;
            this.卡面金额.Width = 80;
            // 
            // 标准学费
            // 
            this.标准学费.DataPropertyName = "标准学费";
            this.标准学费.FillWeight = 80F;
            this.标准学费.HeaderText = "标准学费";
            this.标准学费.Name = "标准学费";
            this.标准学费.ReadOnly = true;
            this.标准学费.Width = 80;
            // 
            // 卡面余额
            // 
            this.卡面余额.DataPropertyName = "卡面余额";
            this.卡面余额.FillWeight = 80F;
            this.卡面余额.HeaderText = "卡面余额";
            this.卡面余额.Name = "卡面余额";
            this.卡面余额.ReadOnly = true;
            this.卡面余额.Width = 80;
            // 
            // 本卡折扣
            // 
            this.本卡折扣.DataPropertyName = "本卡折扣";
            this.本卡折扣.FillWeight = 80F;
            this.本卡折扣.HeaderText = "本卡折扣";
            this.本卡折扣.Name = "本卡折扣";
            this.本卡折扣.ReadOnly = true;
            this.本卡折扣.Width = 80;
            // 
            // 实充金额
            // 
            this.实充金额.DataPropertyName = "实充金额";
            this.实充金额.FillWeight = 80F;
            this.实充金额.HeaderText = "实充金额";
            this.实充金额.Name = "实充金额";
            this.实充金额.ReadOnly = true;
            this.实充金额.Width = 80;
            // 
            // 实收学费
            // 
            this.实收学费.DataPropertyName = "实收学费";
            this.实收学费.FillWeight = 80F;
            this.实收学费.HeaderText = "实收学费";
            this.实收学费.Name = "实收学费";
            this.实收学费.ReadOnly = true;
            this.实收学费.Width = 80;
            // 
            // 实充余额
            // 
            this.实充余额.DataPropertyName = "实充余额";
            this.实充余额.FillWeight = 80F;
            this.实充余额.HeaderText = "实充余额";
            this.实充余额.Name = "实充余额";
            this.实充余额.ReadOnly = true;
            this.实充余额.Width = 80;
            // 
            // 已上应收
            // 
            this.已上应收.DataPropertyName = "已上应收";
            this.已上应收.FillWeight = 80F;
            this.已上应收.HeaderText = "已上应收";
            this.已上应收.Name = "已上应收";
            this.已上应收.ReadOnly = true;
            this.已上应收.Width = 80;
            // 
            // 已上实收
            // 
            this.已上实收.DataPropertyName = "已上实收";
            this.已上实收.FillWeight = 80F;
            this.已上实收.HeaderText = "已上实收";
            this.已上实收.Name = "已上实收";
            this.已上实收.ReadOnly = true;
            this.已上实收.Width = 80;
            // 
            // 已上折扣
            // 
            this.已上折扣.DataPropertyName = "已上折扣";
            this.已上折扣.FillWeight = 80F;
            this.已上折扣.HeaderText = "已上折扣";
            this.已上折扣.Name = "已上折扣";
            this.已上折扣.ReadOnly = true;
            this.已上折扣.Width = 80;
            // 
            // 性别
            // 
            this.性别.DataPropertyName = "性别";
            this.性别.FillWeight = 60F;
            this.性别.HeaderText = "性别";
            this.性别.Name = "性别";
            this.性别.ReadOnly = true;
            this.性别.Width = 60;
            // 
            // 年龄
            // 
            this.年龄.DataPropertyName = "年龄";
            this.年龄.FillWeight = 60F;
            this.年龄.HeaderText = "年龄";
            this.年龄.Name = "年龄";
            this.年龄.ReadOnly = true;
            this.年龄.Width = 60;
            // 
            // 生日
            // 
            this.生日.DataPropertyName = "生日";
            this.生日.HeaderText = "生日";
            this.生日.Name = "生日";
            this.生日.ReadOnly = true;
            // 
            // 学校
            // 
            this.学校.DataPropertyName = "学校";
            this.学校.HeaderText = "学校";
            this.学校.Name = "学校";
            this.学校.ReadOnly = true;
            // 
            // 年级
            // 
            this.年级.DataPropertyName = "年级";
            this.年级.HeaderText = "年级";
            this.年级.Name = "年级";
            this.年级.ReadOnly = true;
            // 
            // 家长姓名
            // 
            this.家长姓名.DataPropertyName = "家长姓名";
            this.家长姓名.HeaderText = "家长姓名";
            this.家长姓名.Name = "家长姓名";
            this.家长姓名.ReadOnly = true;
            // 
            // 联系电话
            // 
            this.联系电话.DataPropertyName = "联系电话";
            this.联系电话.HeaderText = "联系电话";
            this.联系电话.Name = "联系电话";
            this.联系电话.ReadOnly = true;
            // 
            // 家庭住址
            // 
            this.家庭住址.DataPropertyName = "家庭住址";
            this.家庭住址.HeaderText = "家庭住址";
            this.家庭住址.Name = "家庭住址";
            this.家庭住址.ReadOnly = true;
            // 
            // 家长单位
            // 
            this.家长单位.DataPropertyName = "家长单位";
            this.家长单位.HeaderText = "家长单位";
            this.家长单位.Name = "家长单位";
            this.家长单位.ReadOnly = true;
            // 
            // 是否退卡
            // 
            this.是否退卡.DataPropertyName = "是否退卡";
            this.是否退卡.HeaderText = "是否退卡";
            this.是否退卡.Name = "是否退卡";
            this.是否退卡.ReadOnly = true;
            // 
            // 办卡时间
            // 
            this.办卡时间.DataPropertyName = "办卡时间";
            this.办卡时间.HeaderText = "办卡时间";
            this.办卡时间.Name = "办卡时间";
            this.办卡时间.ReadOnly = true;
            // 
            // SumCard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(706, 298);
            this.Controls.Add(this.sum_Count_ViewDataGridView);
            this.Controls.Add(this.sum_Count_ViewBindingNavigator);
            this.Name = "SumCard";
            this.Text = "会员卡消费统计";
            this.Load += new System.EventHandler(this.SumCard_Load);
            ((System.ComponentModel.ISupportInitialize)(this.sumCard_DataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sum_Count_ViewBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sum_Count_ViewBindingNavigator)).EndInit();
            this.sum_Count_ViewBindingNavigator.ResumeLayout(false);
            this.sum_Count_ViewBindingNavigator.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.sum_Count_ViewDataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private HappyYF.YuanXin.Data.SumCard_DataSet sumCard_DataSet;
        private System.Windows.Forms.BindingSource sum_Count_ViewBindingSource;
        private HappyYF.YuanXin.Data.SumCard_DataSetTableAdapters.Sum_Count_ViewTableAdapter sum_Count_ViewTableAdapter;
        private HappyYF.YuanXin.Data.SumCard_DataSetTableAdapters.TableAdapterManager tableAdapterManager;
        private System.Windows.Forms.BindingNavigator sum_Count_ViewBindingNavigator;
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
        private System.Windows.Forms.ToolStripButton sum_Count_ViewBindingNavigatorSaveItem;
        private System.Windows.Forms.DataGridView sum_Count_ViewDataGridView;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.DataGridViewTextBoxColumn 卡号;
        private System.Windows.Forms.DataGridViewTextBoxColumn 姓名;
        private System.Windows.Forms.DataGridViewTextBoxColumn 卡面金额;
        private System.Windows.Forms.DataGridViewTextBoxColumn 标准学费;
        private System.Windows.Forms.DataGridViewTextBoxColumn 卡面余额;
        private System.Windows.Forms.DataGridViewTextBoxColumn 本卡折扣;
        private System.Windows.Forms.DataGridViewTextBoxColumn 实充金额;
        private System.Windows.Forms.DataGridViewTextBoxColumn 实收学费;
        private System.Windows.Forms.DataGridViewTextBoxColumn 实充余额;
        private System.Windows.Forms.DataGridViewTextBoxColumn 已上应收;
        private System.Windows.Forms.DataGridViewTextBoxColumn 已上实收;
        private System.Windows.Forms.DataGridViewTextBoxColumn 已上折扣;
        private System.Windows.Forms.DataGridViewTextBoxColumn 性别;
        private System.Windows.Forms.DataGridViewTextBoxColumn 年龄;
        private System.Windows.Forms.DataGridViewTextBoxColumn 生日;
        private System.Windows.Forms.DataGridViewTextBoxColumn 学校;
        private System.Windows.Forms.DataGridViewTextBoxColumn 年级;
        private System.Windows.Forms.DataGridViewTextBoxColumn 家长姓名;
        private System.Windows.Forms.DataGridViewTextBoxColumn 联系电话;
        private System.Windows.Forms.DataGridViewTextBoxColumn 家庭住址;
        private System.Windows.Forms.DataGridViewTextBoxColumn 家长单位;
        private System.Windows.Forms.DataGridViewTextBoxColumn 是否退卡;
        private System.Windows.Forms.DataGridViewTextBoxColumn 办卡时间;
    }
}