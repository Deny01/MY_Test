namespace HappyYF.YuanXin.WorkSet
{
    partial class seriesAdd
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
            System.Windows.Forms.Label 编号;
            System.Windows.Forms.Label 名称;
            this.ly_inma0010addBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.lYMaterialMange = new HappyYF.YuanXin.Data.LYMaterialMange();
            this.lywarehouseBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.lyproddeptBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.lymaterrialsortBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.lymaterialstatusBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.lyunitsetBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.名称TextBox = new System.Windows.Forms.TextBox();
            this.物资编号TextBox = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.ly_inma0010addTableAdapter = new HappyYF.YuanXin.Data.LYMaterialMangeTableAdapters.ly_inma0010addTableAdapter();
            this.tableAdapterManager = new HappyYF.YuanXin.Data.LYMaterialMangeTableAdapters.TableAdapterManager();
            this.ly_materialstatusTableAdapter = new HappyYF.YuanXin.Data.LYMaterialMangeTableAdapters.ly_materialstatusTableAdapter();
            this.ly_unitsetTableAdapter = new HappyYF.YuanXin.Data.LYMaterialMangeTableAdapters.ly_unitsetTableAdapter();
            this.ly_materrial_sortTableAdapter = new HappyYF.YuanXin.Data.LYMaterialMangeTableAdapters.ly_materrial_sortTableAdapter();
            this.ly_prod_deptTableAdapter = new HappyYF.YuanXin.Data.LYMaterialMangeTableAdapters.ly_prod_deptTableAdapter();
            this.ly_warehouseTableAdapter = new HappyYF.YuanXin.Data.LYMaterialMangeTableAdapters.ly_warehouseTableAdapter();
            this.lysecondstylesetBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.lyfirststylesetBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.ly_firststyle_setTableAdapter = new HappyYF.YuanXin.Data.LYMaterialMangeTableAdapters.ly_firststyle_setTableAdapter();
            this.ly_secondstyle_setTableAdapter = new HappyYF.YuanXin.Data.LYMaterialMangeTableAdapters.ly_secondstyle_setTableAdapter();
            this.lymaterialcategoryBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.lymaterialgetmethodBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.ly_material_getmethodTableAdapter = new HappyYF.YuanXin.Data.LYMaterialMangeTableAdapters.ly_material_getmethodTableAdapter();
            this.ly_materialcategoryTableAdapter = new HappyYF.YuanXin.Data.LYMaterialMangeTableAdapters.ly_materialcategoryTableAdapter();
            编号 = new System.Windows.Forms.Label();
            名称 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.ly_inma0010addBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lYMaterialMange)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lywarehouseBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lyproddeptBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lymaterrialsortBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lymaterialstatusBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lyunitsetBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lysecondstylesetBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lyfirststylesetBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lymaterialcategoryBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lymaterialgetmethodBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // 编号
            // 
            编号.AutoSize = true;
            编号.Location = new System.Drawing.Point(36, 25);
            编号.Name = "编号";
            编号.Size = new System.Drawing.Size(35, 12);
            编号.TabIndex = 2;
            编号.Text = "编号:";
            // 
            // 名称
            // 
            名称.AutoSize = true;
            名称.Location = new System.Drawing.Point(36, 49);
            名称.Name = "名称";
            名称.Size = new System.Drawing.Size(35, 12);
            名称.TabIndex = 6;
            名称.Text = "名称:";
            // 
            // ly_inma0010addBindingSource
            // 
            this.ly_inma0010addBindingSource.DataMember = "ly_inma0010add";
            this.ly_inma0010addBindingSource.DataSource = this.lYMaterialMange;
            // 
            // lYMaterialMange
            // 
            this.lYMaterialMange.DataSetName = "LYMaterialMange";
            this.lYMaterialMange.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // lywarehouseBindingSource
            // 
            this.lywarehouseBindingSource.DataMember = "ly_warehouse";
            this.lywarehouseBindingSource.DataSource = this.lYMaterialMange;
            // 
            // lyproddeptBindingSource
            // 
            this.lyproddeptBindingSource.DataMember = "ly_prod_dept";
            this.lyproddeptBindingSource.DataSource = this.lYMaterialMange;
            // 
            // lymaterrialsortBindingSource
            // 
            this.lymaterrialsortBindingSource.DataMember = "ly_materrial_sort";
            this.lymaterrialsortBindingSource.DataSource = this.lYMaterialMange;
            // 
            // lymaterialstatusBindingSource
            // 
            this.lymaterialstatusBindingSource.DataMember = "ly_materialstatus";
            this.lymaterialstatusBindingSource.DataSource = this.lYMaterialMange;
            // 
            // lyunitsetBindingSource
            // 
            this.lyunitsetBindingSource.DataMember = "ly_unitset";
            this.lyunitsetBindingSource.DataSource = this.lYMaterialMange;
            // 
            // 名称TextBox
            // 
            this.名称TextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.ly_inma0010addBindingSource, "名称", true));
            this.名称TextBox.Location = new System.Drawing.Point(77, 49);
            this.名称TextBox.Name = "名称TextBox";
            this.名称TextBox.Size = new System.Drawing.Size(208, 21);
            this.名称TextBox.TabIndex = 7;
            this.名称TextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.名称TextBox_KeyPress);
            // 
            // 物资编号TextBox
            // 
            this.物资编号TextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.ly_inma0010addBindingSource, "物资编号", true));
            this.物资编号TextBox.Location = new System.Drawing.Point(77, 22);
            this.物资编号TextBox.MaxLength = 8;
            this.物资编号TextBox.Name = "物资编号TextBox";
            this.物资编号TextBox.Size = new System.Drawing.Size(208, 21);
            this.物资编号TextBox.TabIndex = 3;
            this.物资编号TextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.物资编号TextBox_KeyPress);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(77, 165);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "确定";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button2.Location = new System.Drawing.Point(210, 165);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 2;
            this.button2.Text = "取消";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // ly_inma0010addTableAdapter
            // 
            this.ly_inma0010addTableAdapter.ClearBeforeFill = true;
            // 
            // tableAdapterManager
            // 
            this.tableAdapterManager.BackupDataSetBeforeUpdate = false;
            this.tableAdapterManager.ly_bm0031TableAdapter = null;
            this.tableAdapterManager.ly_check_bompriceTableAdapter = null;
            this.tableAdapterManager.ly_drawing_levelSetTableAdapter = null;
            this.tableAdapterManager.ly_employe_warehouseTableAdapter = null;
            this.tableAdapterManager.ly_firststyle_setTableAdapter = null;
            this.tableAdapterManager.ly_inma0010_drawingTableAdapter = null;
            this.tableAdapterManager.ly_inma0010_seriesTableAdapter = null;
            this.tableAdapterManager.ly_inma001011TableAdapter = null;
            this.tableAdapterManager.ly_inma00101TableAdapter = null;
            this.tableAdapterManager.ly_inma0010addTableAdapter = this.ly_inma0010addTableAdapter;
            this.tableAdapterManager.ly_inma0010cpTableAdapter = null;
            this.tableAdapterManager.ly_inma0010fjTableAdapter = null;
            this.tableAdapterManager.ly_inma0010machineTableAdapter = null;
            this.tableAdapterManager.ly_inma0010TableAdapter = null;
            this.tableAdapterManager.ly_inma0010ylTableAdapter = null;
            this.tableAdapterManager.ly_material_getmethodTableAdapter = null;
            this.tableAdapterManager.ly_material_plan_detail_endProductTableAdapter = null;
            this.tableAdapterManager.ly_materialcategoryTableAdapter = null;
            this.tableAdapterManager.ly_materialstatusTableAdapter = null;
            this.tableAdapterManager.ly_materrial_sortTableAdapter = null;
            this.tableAdapterManager.ly_prod_deptTableAdapter = null;
            this.tableAdapterManager.ly_Restructuring_Bom_requestTableAdapter = null;
            this.tableAdapterManager.ly_Restructuring_Bom_returnTableAdapter = null;
            this.tableAdapterManager.ly_sales_matingBom_CostTableAdapter = null;
            this.tableAdapterManager.ly_secondstyle_setTableAdapter = null;
            this.tableAdapterManager.ly_unitset1TableAdapter = null;
            this.tableAdapterManager.ly_unitsetTableAdapter = null;
            this.tableAdapterManager.ly_warehouseTableAdapter = null;
            this.tableAdapterManager.t_financial_typeTableAdapter = null;
            this.tableAdapterManager.UpdateOrder = HappyYF.YuanXin.Data.LYMaterialMangeTableAdapters.TableAdapterManager.UpdateOrderOption.InsertUpdateDelete;
            // 
            // ly_materialstatusTableAdapter
            // 
            this.ly_materialstatusTableAdapter.ClearBeforeFill = true;
            // 
            // ly_unitsetTableAdapter
            // 
            this.ly_unitsetTableAdapter.ClearBeforeFill = true;
            // 
            // ly_materrial_sortTableAdapter
            // 
            this.ly_materrial_sortTableAdapter.ClearBeforeFill = true;
            // 
            // ly_prod_deptTableAdapter
            // 
            this.ly_prod_deptTableAdapter.ClearBeforeFill = true;
            // 
            // ly_warehouseTableAdapter
            // 
            this.ly_warehouseTableAdapter.ClearBeforeFill = true;
            // 
            // lysecondstylesetBindingSource
            // 
            this.lysecondstylesetBindingSource.DataMember = "ly_secondstyle_set";
            this.lysecondstylesetBindingSource.DataSource = this.lYMaterialMange;
            // 
            // lyfirststylesetBindingSource
            // 
            this.lyfirststylesetBindingSource.DataMember = "ly_firststyle_set";
            this.lyfirststylesetBindingSource.DataSource = this.lYMaterialMange;
            // 
            // ly_firststyle_setTableAdapter
            // 
            this.ly_firststyle_setTableAdapter.ClearBeforeFill = true;
            // 
            // ly_secondstyle_setTableAdapter
            // 
            this.ly_secondstyle_setTableAdapter.ClearBeforeFill = true;
            // 
            // lymaterialcategoryBindingSource
            // 
            this.lymaterialcategoryBindingSource.DataMember = "ly_materialcategory";
            this.lymaterialcategoryBindingSource.DataSource = this.lYMaterialMange;
            // 
            // lymaterialgetmethodBindingSource
            // 
            this.lymaterialgetmethodBindingSource.DataMember = "ly_material_getmethod";
            this.lymaterialgetmethodBindingSource.DataSource = this.lYMaterialMange;
            // 
            // ly_material_getmethodTableAdapter
            // 
            this.ly_material_getmethodTableAdapter.ClearBeforeFill = true;
            // 
            // ly_materialcategoryTableAdapter
            // 
            this.ly_materialcategoryTableAdapter.ClearBeforeFill = true;
            // 
            // seriesAdd
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(328, 240);
            this.Controls.Add(名称);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.名称TextBox);
            this.Controls.Add(this.button1);
            this.Controls.Add(编号);
            this.Controls.Add(this.物资编号TextBox);
            this.Name = "seriesAdd";
            this.Text = "类别管理";
            this.Load += new System.EventHandler(this.LY_MaterialAdd_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ly_inma0010addBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lYMaterialMange)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lywarehouseBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lyproddeptBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lymaterrialsortBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lymaterialstatusBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lyunitsetBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lysecondstylesetBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lyfirststylesetBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lymaterialcategoryBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lymaterialgetmethodBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private HappyYF.YuanXin.Data.LYMaterialMange lYMaterialMange;
        private System.Windows.Forms.BindingSource ly_inma0010addBindingSource;
        private HappyYF.YuanXin.Data.LYMaterialMangeTableAdapters.ly_inma0010addTableAdapter ly_inma0010addTableAdapter;
        private HappyYF.YuanXin.Data.LYMaterialMangeTableAdapters.TableAdapterManager tableAdapterManager;
        private System.Windows.Forms.TextBox 名称TextBox;
        private System.Windows.Forms.TextBox 物资编号TextBox;
        private System.Windows.Forms.BindingSource lymaterialstatusBindingSource;
        private HappyYF.YuanXin.Data.LYMaterialMangeTableAdapters.ly_materialstatusTableAdapter ly_materialstatusTableAdapter;
        private System.Windows.Forms.BindingSource lyunitsetBindingSource;
        private HappyYF.YuanXin.Data.LYMaterialMangeTableAdapters.ly_unitsetTableAdapter ly_unitsetTableAdapter;
        private System.Windows.Forms.BindingSource lymaterrialsortBindingSource;
        private HappyYF.YuanXin.Data.LYMaterialMangeTableAdapters.ly_materrial_sortTableAdapter ly_materrial_sortTableAdapter;
        private System.Windows.Forms.BindingSource lyproddeptBindingSource;
        private HappyYF.YuanXin.Data.LYMaterialMangeTableAdapters.ly_prod_deptTableAdapter ly_prod_deptTableAdapter;
        private System.Windows.Forms.BindingSource lywarehouseBindingSource;
        private HappyYF.YuanXin.Data.LYMaterialMangeTableAdapters.ly_warehouseTableAdapter ly_warehouseTableAdapter;
        private System.Windows.Forms.BindingSource lyfirststylesetBindingSource;
        private HappyYF.YuanXin.Data.LYMaterialMangeTableAdapters.ly_firststyle_setTableAdapter ly_firststyle_setTableAdapter;
        private System.Windows.Forms.BindingSource lysecondstylesetBindingSource;
        private HappyYF.YuanXin.Data.LYMaterialMangeTableAdapters.ly_secondstyle_setTableAdapter ly_secondstyle_setTableAdapter;
        private System.Windows.Forms.BindingSource lymaterialgetmethodBindingSource;
        private HappyYF.YuanXin.Data.LYMaterialMangeTableAdapters.ly_material_getmethodTableAdapter ly_material_getmethodTableAdapter;
        private System.Windows.Forms.BindingSource lymaterialcategoryBindingSource;
        private HappyYF.YuanXin.Data.LYMaterialMangeTableAdapters.ly_materialcategoryTableAdapter ly_materialcategoryTableAdapter;
    }
}