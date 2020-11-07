namespace TGZJ.Manger
{
    partial class Bumen
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
            System.Windows.Forms.Label parentIDLabel;
            System.Windows.Forms.Label bumenIDLabel;
            System.Windows.Forms.Label bumenBMLabel;
            System.Windows.Forms.Label bumenMCLabel;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Bumen));
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.新增部门ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.修改部门ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.删除部门ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.parentIDTextBox = new System.Windows.Forms.TextBox();
            this.t_bumenBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.bumenDataSet = new TGZJ.DATA.BumenDataSet();
            this.bumenIDTextBox = new System.Windows.Forms.TextBox();
            this.bumenBMTextBox = new System.Windows.Forms.TextBox();
            this.bumenMCTextBox = new System.Windows.Forms.TextBox();
            this.t_bumenTableAdapter = new TGZJ.DATA.BumenDataSetTableAdapters.T_bumenTableAdapter();
            this.tableAdapterManager = new TGZJ.DATA.BumenDataSetTableAdapters.TableAdapterManager();
            this.t_bumenBindingNavigator = new System.Windows.Forms.BindingNavigator(this.components);
            this.bindingNavigatorCountItem = new System.Windows.Forms.ToolStripLabel();
            this.bindingNavigatorMoveFirstItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorMovePreviousItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.bindingNavigatorPositionItem = new System.Windows.Forms.ToolStripTextBox();
            this.bindingNavigatorSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.bindingNavigatorMoveNextItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorMoveLastItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.bindingNavigatorAddNewItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorDeleteItem = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.t_bumenBindingNavigatorSaveItem = new System.Windows.Forms.ToolStripButton();
            parentIDLabel = new System.Windows.Forms.Label();
            bumenIDLabel = new System.Windows.Forms.Label();
            bumenBMLabel = new System.Windows.Forms.Label();
            bumenMCLabel = new System.Windows.Forms.Label();
            this.contextMenuStrip1.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.t_bumenBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bumenDataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.t_bumenBindingNavigator)).BeginInit();
            this.t_bumenBindingNavigator.SuspendLayout();
            this.SuspendLayout();
            // 
            // parentIDLabel
            // 
            parentIDLabel.AutoSize = true;
            parentIDLabel.Location = new System.Drawing.Point(12, 67);
            parentIDLabel.Name = "parentIDLabel";
            parentIDLabel.Size = new System.Drawing.Size(65, 12);
            parentIDLabel.TabIndex = 0;
            parentIDLabel.Text = "parent ID:";
            parentIDLabel.Visible = false;
            // 
            // bumenIDLabel
            // 
            bumenIDLabel.AutoSize = true;
            bumenIDLabel.Location = new System.Drawing.Point(382, 15);
            bumenIDLabel.Name = "bumenIDLabel";
            bumenIDLabel.Size = new System.Drawing.Size(35, 12);
            bumenIDLabel.TabIndex = 2;
            bumenIDLabel.Text = "编码:";
            // 
            // bumenBMLabel
            // 
            bumenBMLabel.AutoSize = true;
            bumenBMLabel.Location = new System.Drawing.Point(200, 94);
            bumenBMLabel.Name = "bumenBMLabel";
            bumenBMLabel.Size = new System.Drawing.Size(59, 12);
            bumenBMLabel.TabIndex = 4;
            bumenBMLabel.Text = "bumen BM:";
            bumenBMLabel.Visible = false;
            // 
            // bumenMCLabel
            // 
            bumenMCLabel.AutoSize = true;
            bumenMCLabel.Location = new System.Drawing.Point(12, 15);
            bumenMCLabel.Name = "bumenMCLabel";
            bumenMCLabel.Size = new System.Drawing.Size(59, 12);
            bumenMCLabel.TabIndex = 6;
            bumenMCLabel.Text = "部门名称:";
            // 
            // treeView1
            // 
            this.treeView1.ContextMenuStrip = this.contextMenuStrip1;
            this.treeView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView1.Location = new System.Drawing.Point(0, 0);
            this.treeView1.Name = "treeView1";
            this.treeView1.Size = new System.Drawing.Size(603, 200);
            this.treeView1.TabIndex = 1;
            this.treeView1.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterSelect);
            this.treeView1.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeView1_NodeMouseClick);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.新增部门ToolStripMenuItem,
            this.修改部门ToolStripMenuItem,
            this.删除部门ToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.ShowCheckMargin = true;
            this.contextMenuStrip1.ShowImageMargin = false;
            this.contextMenuStrip1.Size = new System.Drawing.Size(95, 70);
            // 
            // 新增部门ToolStripMenuItem
            // 
            this.新增部门ToolStripMenuItem.Name = "新增部门ToolStripMenuItem";
            this.新增部门ToolStripMenuItem.Size = new System.Drawing.Size(94, 22);
            this.新增部门ToolStripMenuItem.Text = "新增";
            this.新增部门ToolStripMenuItem.Click += new System.EventHandler(this.新增部门ToolStripMenuItem_Click);
            // 
            // 修改部门ToolStripMenuItem
            // 
            this.修改部门ToolStripMenuItem.Name = "修改部门ToolStripMenuItem";
            this.修改部门ToolStripMenuItem.Size = new System.Drawing.Size(94, 22);
            this.修改部门ToolStripMenuItem.Text = "修改";
            // 
            // 删除部门ToolStripMenuItem
            // 
            this.删除部门ToolStripMenuItem.Name = "删除部门ToolStripMenuItem";
            this.删除部门ToolStripMenuItem.Size = new System.Drawing.Size(94, 22);
            this.删除部门ToolStripMenuItem.Text = "删除";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 25);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.AutoScroll = true;
            this.splitContainer1.Panel1.BackColor = System.Drawing.Color.Transparent;
            this.splitContainer1.Panel1.Controls.Add(parentIDLabel);
            this.splitContainer1.Panel1.Controls.Add(this.parentIDTextBox);
            this.splitContainer1.Panel1.Controls.Add(bumenIDLabel);
            this.splitContainer1.Panel1.Controls.Add(this.bumenIDTextBox);
            this.splitContainer1.Panel1.Controls.Add(bumenBMLabel);
            this.splitContainer1.Panel1.Controls.Add(this.bumenBMTextBox);
            this.splitContainer1.Panel1.Controls.Add(bumenMCLabel);
            this.splitContainer1.Panel1.Controls.Add(this.bumenMCTextBox);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.treeView1);
            this.splitContainer1.Size = new System.Drawing.Size(603, 352);
            this.splitContainer1.SplitterDistance = 148;
            this.splitContainer1.TabIndex = 2;
            // 
            // parentIDTextBox
            // 
            this.parentIDTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.t_bumenBindingSource, "parentID", true));
            this.parentIDTextBox.Location = new System.Drawing.Point(83, 64);
            this.parentIDTextBox.Name = "parentIDTextBox";
            this.parentIDTextBox.Size = new System.Drawing.Size(100, 21);
            this.parentIDTextBox.TabIndex = 1;
            this.parentIDTextBox.Visible = false;
            // 
            // t_bumenBindingSource
            // 
            this.t_bumenBindingSource.DataMember = "T_bumen";
            this.t_bumenBindingSource.DataSource = this.bumenDataSet;
            this.t_bumenBindingSource.CurrentChanged += new System.EventHandler(this.t_bumenBindingSource_CurrentChanged);
            // 
            // bumenDataSet
            // 
            this.bumenDataSet.DataSetName = "BumenDataSet";
            this.bumenDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // bumenIDTextBox
            // 
            this.bumenIDTextBox.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.bumenIDTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.t_bumenBindingSource, "bumenID", true));
            this.bumenIDTextBox.Location = new System.Drawing.Point(423, 15);
            this.bumenIDTextBox.Name = "bumenIDTextBox";
            this.bumenIDTextBox.ReadOnly = true;
            this.bumenIDTextBox.Size = new System.Drawing.Size(132, 21);
            this.bumenIDTextBox.TabIndex = 3;
            // 
            // bumenBMTextBox
            // 
            this.bumenBMTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.t_bumenBindingSource, "bumenBM", true));
            this.bumenBMTextBox.Location = new System.Drawing.Point(271, 91);
            this.bumenBMTextBox.Name = "bumenBMTextBox";
            this.bumenBMTextBox.Size = new System.Drawing.Size(100, 21);
            this.bumenBMTextBox.TabIndex = 5;
            this.bumenBMTextBox.Visible = false;
            // 
            // bumenMCTextBox
            // 
            this.bumenMCTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.t_bumenBindingSource, "bumenMC", true));
            this.bumenMCTextBox.Location = new System.Drawing.Point(83, 15);
            this.bumenMCTextBox.Name = "bumenMCTextBox";
            this.bumenMCTextBox.Size = new System.Drawing.Size(288, 21);
            this.bumenMCTextBox.TabIndex = 7;
            // 
            // t_bumenTableAdapter
            // 
            this.t_bumenTableAdapter.ClearBeforeFill = true;
            // 
            // tableAdapterManager
            // 
            this.tableAdapterManager.BackupDataSetBeforeUpdate = false;
            this.tableAdapterManager.T_bumenTableAdapter = this.t_bumenTableAdapter;
            this.tableAdapterManager.UpdateOrder = TGZJ.DATA.BumenDataSetTableAdapters.TableAdapterManager.UpdateOrderOption.InsertUpdateDelete;
            // 
            // t_bumenBindingNavigator
            // 
            this.t_bumenBindingNavigator.AddNewItem = null;
            this.t_bumenBindingNavigator.BindingSource = this.t_bumenBindingSource;
            this.t_bumenBindingNavigator.CountItem = this.bindingNavigatorCountItem;
            this.t_bumenBindingNavigator.DeleteItem = null;
            this.t_bumenBindingNavigator.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
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
            this.toolStripButton1,
            this.t_bumenBindingNavigatorSaveItem});
            this.t_bumenBindingNavigator.Location = new System.Drawing.Point(0, 0);
            this.t_bumenBindingNavigator.MoveFirstItem = this.bindingNavigatorMoveFirstItem;
            this.t_bumenBindingNavigator.MoveLastItem = this.bindingNavigatorMoveLastItem;
            this.t_bumenBindingNavigator.MoveNextItem = this.bindingNavigatorMoveNextItem;
            this.t_bumenBindingNavigator.MovePreviousItem = this.bindingNavigatorMovePreviousItem;
            this.t_bumenBindingNavigator.Name = "t_bumenBindingNavigator";
            this.t_bumenBindingNavigator.PositionItem = this.bindingNavigatorPositionItem;
            this.t_bumenBindingNavigator.Size = new System.Drawing.Size(603, 25);
            this.t_bumenBindingNavigator.TabIndex = 3;
            this.t_bumenBindingNavigator.Text = "bindingNavigator1";
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
            // bindingNavigatorAddNewItem
            // 
            this.bindingNavigatorAddNewItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorAddNewItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorAddNewItem.Image")));
            this.bindingNavigatorAddNewItem.Name = "bindingNavigatorAddNewItem";
            this.bindingNavigatorAddNewItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorAddNewItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorAddNewItem.Text = "新添";
            this.bindingNavigatorAddNewItem.Click += new System.EventHandler(this.bindingNavigatorAddNewItem_Click);
            // 
            // bindingNavigatorDeleteItem
            // 
            this.bindingNavigatorDeleteItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorDeleteItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorDeleteItem.Image")));
            this.bindingNavigatorDeleteItem.Name = "bindingNavigatorDeleteItem";
            this.bindingNavigatorDeleteItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorDeleteItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorDeleteItem.Text = "删除";
            this.bindingNavigatorDeleteItem.Click += new System.EventHandler(this.bindingNavigatorDeleteItem_Click);
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton1.Text = "修改";
            this.toolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // t_bumenBindingNavigatorSaveItem
            // 
            this.t_bumenBindingNavigatorSaveItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.t_bumenBindingNavigatorSaveItem.Image = ((System.Drawing.Image)(resources.GetObject("t_bumenBindingNavigatorSaveItem.Image")));
            this.t_bumenBindingNavigatorSaveItem.Name = "t_bumenBindingNavigatorSaveItem";
            this.t_bumenBindingNavigatorSaveItem.Size = new System.Drawing.Size(23, 22);
            this.t_bumenBindingNavigatorSaveItem.Text = "保存数据";
            this.t_bumenBindingNavigatorSaveItem.Click += new System.EventHandler(this.t_bumenBindingNavigatorSaveItem_Click_1);
            // 
            // Bumen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(603, 377);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.t_bumenBindingNavigator);
            this.Name = "Bumen";
            this.Text = "部门设置";
            this.Load += new System.EventHandler(this.Bumen_Load);
            this.contextMenuStrip1.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.t_bumenBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bumenDataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.t_bumenBindingNavigator)).EndInit();
            this.t_bumenBindingNavigator.ResumeLayout(false);
            this.t_bumenBindingNavigator.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 新增部门ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 修改部门ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 删除部门ToolStripMenuItem;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private TGZJ.DATA.BumenDataSet bumenDataSet;
        private System.Windows.Forms.BindingSource t_bumenBindingSource;
        private TGZJ.DATA.BumenDataSetTableAdapters.T_bumenTableAdapter t_bumenTableAdapter;
        private TGZJ.DATA.BumenDataSetTableAdapters.TableAdapterManager tableAdapterManager;
        private System.Windows.Forms.BindingNavigator t_bumenBindingNavigator;
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
        private System.Windows.Forms.ToolStripButton t_bumenBindingNavigatorSaveItem;
        private System.Windows.Forms.TextBox parentIDTextBox;
        private System.Windows.Forms.TextBox bumenIDTextBox;
        private System.Windows.Forms.TextBox bumenBMTextBox;
        private System.Windows.Forms.TextBox bumenMCTextBox;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
    }
}