namespace TGZJ.Manger
{
    partial class QuanxianFenpei
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(QuanxianFenpei));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.treeView2 = new System.Windows.Forms.TreeView();
            this.shouquanDataSet = new TGZJ.DATA.ShouquanDataSet();
            this.t_gongnengshouquanBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.t_gongnengshouquanTableAdapter = new TGZJ.DATA.ShouquanDataSetTableAdapters.T_gongnengshouquanTableAdapter();
            this.tableAdapterManager = new TGZJ.DATA.ShouquanDataSetTableAdapters.TableAdapterManager();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.shouquanDataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.t_gongnengshouquanBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.AutoScroll = true;
            this.splitContainer1.Panel1.Controls.Add(this.treeView1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.treeView2);
            this.splitContainer1.Size = new System.Drawing.Size(573, 408);
            this.splitContainer1.SplitterDistance = 202;
            this.splitContainer1.TabIndex = 0;
            // 
            // treeView1
            // 
            this.treeView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView1.ImageIndex = 0;
            this.treeView1.ImageList = this.imageList1;
            this.treeView1.Location = new System.Drawing.Point(0, 0);
            this.treeView1.Name = "treeView1";
            this.treeView1.SelectedImageIndex = 0;
            this.treeView1.Size = new System.Drawing.Size(202, 408);
            this.treeView1.TabIndex = 0;
            this.treeView1.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterSelect);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "bumen.bmp");
            this.imageList1.Images.SetKeyName(1, "yonghu.bmp");
            // 
            // treeView2
            // 
            this.treeView2.CheckBoxes = true;
            this.treeView2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.treeView2.Location = new System.Drawing.Point(0, 0);
            this.treeView2.Name = "treeView2";
            this.treeView2.Size = new System.Drawing.Size(367, 408);
            this.treeView2.TabIndex = 0;
            this.treeView2.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.treeView2_AfterCheck);
            // 
            // shouquanDataSet
            // 
            this.shouquanDataSet.DataSetName = "ShouquanDataSet";
            this.shouquanDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // t_gongnengshouquanBindingSource
            // 
            this.t_gongnengshouquanBindingSource.DataMember = "T_gongnengshouquan";
            this.t_gongnengshouquanBindingSource.DataSource = this.shouquanDataSet;
            // 
            // t_gongnengshouquanTableAdapter
            // 
            this.t_gongnengshouquanTableAdapter.ClearBeforeFill = true;
            // 
            // tableAdapterManager
            // 
            this.tableAdapterManager.BackupDataSetBeforeUpdate = false;
            this.tableAdapterManager.T_gongnengshouquanTableAdapter = this.t_gongnengshouquanTableAdapter;
            this.tableAdapterManager.UpdateOrder = TGZJ.DATA.ShouquanDataSetTableAdapters.TableAdapterManager.UpdateOrderOption.InsertUpdateDelete;
            // 
            // QuanxianFenpei
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(573, 408);
            this.Controls.Add(this.splitContainer1);
            this.Name = "QuanxianFenpei";
            this.Text = "用户权限分配";
            this.Load += new System.EventHandler(this.QuanxianFenpei_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.shouquanDataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.t_gongnengshouquanBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.TreeView treeView2;
        private System.Windows.Forms.ImageList imageList1;
        private TGZJ.DATA.ShouquanDataSet shouquanDataSet;
        private System.Windows.Forms.BindingSource t_gongnengshouquanBindingSource;
        private TGZJ.DATA.ShouquanDataSetTableAdapters.T_gongnengshouquanTableAdapter t_gongnengshouquanTableAdapter;
        private TGZJ.DATA.ShouquanDataSetTableAdapters.TableAdapterManager tableAdapterManager;
    }
}