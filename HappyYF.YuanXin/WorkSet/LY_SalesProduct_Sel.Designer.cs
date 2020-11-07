namespace HappyYF.YuanXin.WorkSet
{
    partial class LY_SalesProduct_Sel
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LY_SalesProduct_Sel));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.lYSalseMange = new HappyYF.YuanXin.Data.LYSalseMange();
            this.ly_lsptb_selBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.ly_lsptb_selTableAdapter = new HappyYF.YuanXin.Data.LYSalseMangeTableAdapters.ly_lsptb_selTableAdapter();
            this.tableAdapterManager = new HappyYF.YuanXin.Data.LYSalseMangeTableAdapters.TableAdapterManager();
            this.ly_lsptb_selDataGridView = new System.Windows.Forms.DataGridView();
            this.bindingNavigator3 = new System.Windows.Forms.BindingNavigator(this.components);
            this.toolStripLabel3 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripLabel4 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripTextBox6 = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripSeparator14 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton22 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton23 = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator15 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripTextBox7 = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripSeparator16 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton24 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton25 = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton26 = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorAddNewItem1 = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorDeleteItem1 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton29 = new System.Windows.Forms.ToolStripButton();
            this.lsptbId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.warehouse = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn82 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.物料编号2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.物料名称2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.mch_jp = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.mch_py = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.中方型号2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn84 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.单位2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.营业定价3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn44 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.台用量 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.领用数 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.库存 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.计划库存 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn89 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn90 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn91 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.lYSalseMange)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ly_lsptb_selBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ly_lsptb_selDataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingNavigator3)).BeginInit();
            this.bindingNavigator3.SuspendLayout();
            this.SuspendLayout();
            // 
            // lYSalseMange
            // 
            this.lYSalseMange.DataSetName = "LYSalseMange";
            this.lYSalseMange.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // ly_lsptb_selBindingSource
            // 
            this.ly_lsptb_selBindingSource.DataMember = "ly_lsptb_sel";
            this.ly_lsptb_selBindingSource.DataSource = this.lYSalseMange;
            // 
            // ly_lsptb_selTableAdapter
            // 
            this.ly_lsptb_selTableAdapter.ClearBeforeFill = true;
            // 
            // tableAdapterManager
            // 
            this.tableAdapterManager.BackupDataSetBeforeUpdate = false;
            this.tableAdapterManager.ly_company_informationTableAdapter = null;
            this.tableAdapterManager.ly_express_companyTableAdapter = null;
            this.tableAdapterManager.ly_lsptb_selTableAdapter = this.ly_lsptb_selTableAdapter;
            this.tableAdapterManager.ly_material_plan_mainperiodTableAdapter = null;
            this.tableAdapterManager.ly_material_plan_mainSingleTableAdapter = null;
            this.tableAdapterManager.ly_material_plan_mainTableAdapter = null;
            this.tableAdapterManager.ly_plan_getmaterial_deliverTableAdapter = null;
            this.tableAdapterManager.ly_plan_getmaterial_departmentTableAdapter = null;
            this.tableAdapterManager.ly_plan_getmaterial1TableAdapter = null;
            this.tableAdapterManager.ly_plan_getmaterialSingleTableAdapter = null;
            this.tableAdapterManager.ly_plan_getmaterialTableAdapter = null;
            this.tableAdapterManager.ly_sales_borrow_clientTableAdapter = null;
            this.tableAdapterManager.ly_sales_borrow_detailTableAdapter = null;
            this.tableAdapterManager.ly_sales_borrow_periodTableAdapter = null;
            this.tableAdapterManager.ly_sales_borrow_singleTableAdapter = null;
            this.tableAdapterManager.ly_sales_borrow_styleTableAdapter = null;
            this.tableAdapterManager.ly_sales_borrowTableAdapter = null;
            this.tableAdapterManager.ly_sales_business_singleTableAdapter = null;
            this.tableAdapterManager.ly_sales_businessTableAdapter = null;
            this.tableAdapterManager.ly_sales_clientTableAdapter = null;
            this.tableAdapterManager.ly_sales_contract_classTableAdapter = null;
            this.tableAdapterManager.ly_sales_contract_detailTableAdapter = null;
            this.tableAdapterManager.ly_sales_contract_main_forbusinessTableAdapter = null;
            this.tableAdapterManager.ly_sales_contract_main1TableAdapter = null;
            this.tableAdapterManager.ly_sales_contract_mainSingleTableAdapter = null;
            this.tableAdapterManager.ly_sales_contract_mainTableAdapter = null;
            this.tableAdapterManager.ly_sales_contract_styleTableAdapter = null;
            this.tableAdapterManager.ly_sales_contract_terms_forcontractTableAdapter = null;
            this.tableAdapterManager.ly_sales_contract_termsTableAdapter = null;
            this.tableAdapterManager.ly_sales_deliver_detail2TableAdapter = null;
            this.tableAdapterManager.ly_sales_deliver_detailTableAdapter = null;
            this.tableAdapterManager.ly_sales_deliverTableAdapter = null;
            this.tableAdapterManager.ly_sales_groupSingleTableAdapter = null;
            this.tableAdapterManager.ly_sales_groupTableAdapter = null;
            this.tableAdapterManager.ly_sales_outbindTableAdapter = null;
            this.tableAdapterManager.ly_sales_test_detail1TableAdapter = null;
            this.tableAdapterManager.ly_sales_test_detail3TableAdapter = null;
            this.tableAdapterManager.ly_sales_test_detailTableAdapter = null;
            this.tableAdapterManager.ly_sales_testTableAdapter = null;
            this.tableAdapterManager.ly_salesdiscountTableAdapter = null;
            this.tableAdapterManager.ly_salespersonTableAdapter = null;
            this.tableAdapterManager.ly_salesregion_secondTableAdapter = null;
            this.tableAdapterManager.ly_salesregionTableAdapter = null;
            this.tableAdapterManager.UpdateOrder = HappyYF.YuanXin.Data.LYSalseMangeTableAdapters.TableAdapterManager.UpdateOrderOption.InsertUpdateDelete;
            // 
            // ly_lsptb_selDataGridView
            // 
            this.ly_lsptb_selDataGridView.AllowUserToAddRows = false;
            this.ly_lsptb_selDataGridView.AllowUserToDeleteRows = false;
            this.ly_lsptb_selDataGridView.AutoGenerateColumns = false;
            this.ly_lsptb_selDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.ly_lsptb_selDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.lsptbId,
            this.warehouse,
            this.dataGridViewTextBoxColumn82,
            this.物料编号2,
            this.物料名称2,
            this.mch_jp,
            this.mch_py,
            this.中方型号2,
            this.dataGridViewTextBoxColumn84,
            this.单位2,
            this.营业定价3,
            this.dataGridViewTextBoxColumn44,
            this.台用量,
            this.领用数,
            this.库存,
            this.计划库存,
            this.dataGridViewTextBoxColumn89,
            this.dataGridViewTextBoxColumn90,
            this.dataGridViewTextBoxColumn91});
            this.ly_lsptb_selDataGridView.DataSource = this.ly_lsptb_selBindingSource;
            this.ly_lsptb_selDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ly_lsptb_selDataGridView.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.ly_lsptb_selDataGridView.Location = new System.Drawing.Point(0, 25);
            this.ly_lsptb_selDataGridView.Name = "ly_lsptb_selDataGridView";
            this.ly_lsptb_selDataGridView.ReadOnly = true;
            this.ly_lsptb_selDataGridView.RowHeadersWidth = 19;
            this.ly_lsptb_selDataGridView.RowTemplate.Height = 23;
            this.ly_lsptb_selDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.ly_lsptb_selDataGridView.Size = new System.Drawing.Size(671, 548);
            this.ly_lsptb_selDataGridView.TabIndex = 4;
            this.ly_lsptb_selDataGridView.DoubleClick += new System.EventHandler(this.ly_lsptb_selDataGridView_DoubleClick);
            // 
            // bindingNavigator3
            // 
            this.bindingNavigator3.AddNewItem = null;
            this.bindingNavigator3.BindingSource = this.ly_lsptb_selBindingSource;
            this.bindingNavigator3.CountItem = this.toolStripLabel3;
            this.bindingNavigator3.DeleteItem = null;
            this.bindingNavigator3.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel4,
            this.toolStripTextBox6,
            this.toolStripSeparator14,
            this.toolStripButton22,
            this.toolStripButton23,
            this.toolStripSeparator15,
            this.toolStripTextBox7,
            this.toolStripLabel3,
            this.toolStripSeparator16,
            this.toolStripButton24,
            this.toolStripButton25,
            this.bindingNavigatorSeparator3,
            this.toolStripButton26,
            this.bindingNavigatorAddNewItem1,
            this.bindingNavigatorDeleteItem1,
            this.toolStripButton29});
            this.bindingNavigator3.Location = new System.Drawing.Point(0, 0);
            this.bindingNavigator3.MoveFirstItem = this.toolStripButton22;
            this.bindingNavigator3.MoveLastItem = this.toolStripButton25;
            this.bindingNavigator3.MoveNextItem = this.toolStripButton24;
            this.bindingNavigator3.MovePreviousItem = this.toolStripButton23;
            this.bindingNavigator3.Name = "bindingNavigator3";
            this.bindingNavigator3.PositionItem = this.toolStripTextBox7;
            this.bindingNavigator3.Size = new System.Drawing.Size(671, 25);
            this.bindingNavigator3.TabIndex = 5;
            this.bindingNavigator3.Text = "bindingNavigator3";
            // 
            // toolStripLabel3
            // 
            this.toolStripLabel3.Name = "toolStripLabel3";
            this.toolStripLabel3.Size = new System.Drawing.Size(35, 22);
            this.toolStripLabel3.Text = "/ {0}";
            this.toolStripLabel3.ToolTipText = "总项数";
            // 
            // toolStripLabel4
            // 
            this.toolStripLabel4.Name = "toolStripLabel4";
            this.toolStripLabel4.Size = new System.Drawing.Size(35, 22);
            this.toolStripLabel4.Text = "检索:";
            // 
            // toolStripTextBox6
            // 
            this.toolStripTextBox6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.toolStripTextBox6.Name = "toolStripTextBox6";
            this.toolStripTextBox6.Size = new System.Drawing.Size(80, 25);
            this.toolStripTextBox6.Enter += new System.EventHandler(this.toolStripTextBox6_Enter);
            this.toolStripTextBox6.KeyUp += new System.Windows.Forms.KeyEventHandler(this.toolStripTextBox6_KeyUp);
            // 
            // toolStripSeparator14
            // 
            this.toolStripSeparator14.Name = "toolStripSeparator14";
            this.toolStripSeparator14.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripButton22
            // 
            this.toolStripButton22.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton22.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton22.Image")));
            this.toolStripButton22.Name = "toolStripButton22";
            this.toolStripButton22.RightToLeftAutoMirrorImage = true;
            this.toolStripButton22.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton22.Text = "移到第一条记录";
            // 
            // toolStripButton23
            // 
            this.toolStripButton23.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton23.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton23.Image")));
            this.toolStripButton23.Name = "toolStripButton23";
            this.toolStripButton23.RightToLeftAutoMirrorImage = true;
            this.toolStripButton23.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton23.Text = "移到上一条记录";
            // 
            // toolStripSeparator15
            // 
            this.toolStripSeparator15.Name = "toolStripSeparator15";
            this.toolStripSeparator15.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripTextBox7
            // 
            this.toolStripTextBox7.AccessibleName = "位置";
            this.toolStripTextBox7.AutoSize = false;
            this.toolStripTextBox7.Name = "toolStripTextBox7";
            this.toolStripTextBox7.Size = new System.Drawing.Size(50, 21);
            this.toolStripTextBox7.Text = "0";
            this.toolStripTextBox7.ToolTipText = "当前位置";
            // 
            // toolStripSeparator16
            // 
            this.toolStripSeparator16.Name = "toolStripSeparator16";
            this.toolStripSeparator16.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripButton24
            // 
            this.toolStripButton24.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton24.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton24.Image")));
            this.toolStripButton24.Name = "toolStripButton24";
            this.toolStripButton24.RightToLeftAutoMirrorImage = true;
            this.toolStripButton24.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton24.Text = "移到下一条记录";
            // 
            // toolStripButton25
            // 
            this.toolStripButton25.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton25.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton25.Image")));
            this.toolStripButton25.Name = "toolStripButton25";
            this.toolStripButton25.RightToLeftAutoMirrorImage = true;
            this.toolStripButton25.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton25.Text = "移到最后一条记录";
            // 
            // bindingNavigatorSeparator3
            // 
            this.bindingNavigatorSeparator3.Name = "bindingNavigatorSeparator3";
            this.bindingNavigatorSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripButton26
            // 
            this.toolStripButton26.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButton26.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton26.Image")));
            this.toolStripButton26.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton26.Name = "toolStripButton26";
            this.toolStripButton26.Size = new System.Drawing.Size(33, 22);
            this.toolStripButton26.Text = "确定";
            // 
            // bindingNavigatorAddNewItem1
            // 
            this.bindingNavigatorAddNewItem1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorAddNewItem1.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorAddNewItem1.Image")));
            this.bindingNavigatorAddNewItem1.Name = "bindingNavigatorAddNewItem1";
            this.bindingNavigatorAddNewItem1.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorAddNewItem1.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorAddNewItem1.Text = "新添";
            this.bindingNavigatorAddNewItem1.Visible = false;
            // 
            // bindingNavigatorDeleteItem1
            // 
            this.bindingNavigatorDeleteItem1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorDeleteItem1.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorDeleteItem1.Image")));
            this.bindingNavigatorDeleteItem1.Name = "bindingNavigatorDeleteItem1";
            this.bindingNavigatorDeleteItem1.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorDeleteItem1.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorDeleteItem1.Text = "删除";
            this.bindingNavigatorDeleteItem1.Visible = false;
            // 
            // toolStripButton29
            // 
            this.toolStripButton29.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButton29.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton29.Image")));
            this.toolStripButton29.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton29.Name = "toolStripButton29";
            this.toolStripButton29.Size = new System.Drawing.Size(33, 22);
            this.toolStripButton29.Text = "刷新";
            this.toolStripButton29.Click += new System.EventHandler(this.toolStripButton29_Click);
            // 
            // lsptbId
            // 
            this.lsptbId.DataPropertyName = "id";
            this.lsptbId.HeaderText = "id";
            this.lsptbId.Name = "lsptbId";
            this.lsptbId.ReadOnly = true;
            this.lsptbId.Visible = false;
            // 
            // warehouse
            // 
            this.warehouse.DataPropertyName = "仓库";
            this.warehouse.HeaderText = "仓库";
            this.warehouse.Name = "warehouse";
            this.warehouse.ReadOnly = true;
            this.warehouse.Visible = false;
            this.warehouse.Width = 39;
            // 
            // dataGridViewTextBoxColumn82
            // 
            this.dataGridViewTextBoxColumn82.DataPropertyName = "库位";
            this.dataGridViewTextBoxColumn82.HeaderText = "库位";
            this.dataGridViewTextBoxColumn82.Name = "dataGridViewTextBoxColumn82";
            this.dataGridViewTextBoxColumn82.ReadOnly = true;
            this.dataGridViewTextBoxColumn82.Visible = false;
            this.dataGridViewTextBoxColumn82.Width = 70;
            // 
            // 物料编号2
            // 
            this.物料编号2.DataPropertyName = "物料编号";
            this.物料编号2.HeaderText = "物料编号";
            this.物料编号2.Name = "物料编号2";
            this.物料编号2.ReadOnly = true;
            this.物料编号2.Width = 70;
            // 
            // 物料名称2
            // 
            this.物料名称2.DataPropertyName = "物料名称";
            this.物料名称2.HeaderText = "物料名称";
            this.物料名称2.Name = "物料名称2";
            this.物料名称2.ReadOnly = true;
            this.物料名称2.Width = 180;
            // 
            // mch_jp
            // 
            this.mch_jp.DataPropertyName = "mch_jp";
            this.mch_jp.HeaderText = "mch_jp";
            this.mch_jp.Name = "mch_jp";
            this.mch_jp.ReadOnly = true;
            this.mch_jp.Visible = false;
            // 
            // mch_py
            // 
            this.mch_py.DataPropertyName = "mch_py";
            this.mch_py.HeaderText = "mch_py";
            this.mch_py.Name = "mch_py";
            this.mch_py.ReadOnly = true;
            this.mch_py.Visible = false;
            // 
            // 中方型号2
            // 
            this.中方型号2.DataPropertyName = "中方型号";
            this.中方型号2.HeaderText = "中方型号";
            this.中方型号2.Name = "中方型号2";
            this.中方型号2.ReadOnly = true;
            this.中方型号2.Width = 110;
            // 
            // dataGridViewTextBoxColumn84
            // 
            this.dataGridViewTextBoxColumn84.DataPropertyName = "规格";
            this.dataGridViewTextBoxColumn84.HeaderText = "规格";
            this.dataGridViewTextBoxColumn84.Name = "dataGridViewTextBoxColumn84";
            this.dataGridViewTextBoxColumn84.ReadOnly = true;
            this.dataGridViewTextBoxColumn84.Visible = false;
            // 
            // 单位2
            // 
            this.单位2.DataPropertyName = "单位";
            this.单位2.HeaderText = "单位";
            this.单位2.Name = "单位2";
            this.单位2.ReadOnly = true;
            this.单位2.Width = 39;
            // 
            // 营业定价3
            // 
            this.营业定价3.DataPropertyName = "营业定价";
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle1.Format = "N2";
            dataGridViewCellStyle1.NullValue = null;
            this.营业定价3.DefaultCellStyle = dataGridViewCellStyle1;
            this.营业定价3.HeaderText = "营业定价";
            this.营业定价3.Name = "营业定价3";
            this.营业定价3.ReadOnly = true;
            this.营业定价3.Visible = false;
            this.营业定价3.Width = 80;
            // 
            // dataGridViewTextBoxColumn44
            // 
            this.dataGridViewTextBoxColumn44.DataPropertyName = "产品折扣";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle2.Format = "N2";
            dataGridViewCellStyle2.NullValue = null;
            this.dataGridViewTextBoxColumn44.DefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridViewTextBoxColumn44.HeaderText = "产品折扣";
            this.dataGridViewTextBoxColumn44.Name = "dataGridViewTextBoxColumn44";
            this.dataGridViewTextBoxColumn44.ReadOnly = true;
            this.dataGridViewTextBoxColumn44.Visible = false;
            this.dataGridViewTextBoxColumn44.Width = 80;
            // 
            // 台用量
            // 
            this.台用量.DataPropertyName = "台用量";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle3.Format = "N0";
            dataGridViewCellStyle3.NullValue = null;
            this.台用量.DefaultCellStyle = dataGridViewCellStyle3;
            this.台用量.HeaderText = "台用量";
            this.台用量.Name = "台用量";
            this.台用量.ReadOnly = true;
            this.台用量.Visible = false;
            this.台用量.Width = 50;
            // 
            // 领用数
            // 
            this.领用数.DataPropertyName = "领用数";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.HotPink;
            dataGridViewCellStyle4.Format = "N0";
            dataGridViewCellStyle4.NullValue = null;
            this.领用数.DefaultCellStyle = dataGridViewCellStyle4;
            this.领用数.HeaderText = "领用数";
            this.领用数.Name = "领用数";
            this.领用数.ReadOnly = true;
            this.领用数.Visible = false;
            this.领用数.Width = 50;
            // 
            // 库存
            // 
            this.库存.DataPropertyName = "库存";
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle5.Format = "N0";
            dataGridViewCellStyle5.NullValue = null;
            this.库存.DefaultCellStyle = dataGridViewCellStyle5;
            this.库存.HeaderText = "库存";
            this.库存.Name = "库存";
            this.库存.ReadOnly = true;
            this.库存.Width = 50;
            // 
            // 计划库存
            // 
            this.计划库存.DataPropertyName = "计划库存";
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle6.Format = "N2";
            dataGridViewCellStyle6.NullValue = null;
            this.计划库存.DefaultCellStyle = dataGridViewCellStyle6;
            this.计划库存.HeaderText = "计划库存";
            this.计划库存.Name = "计划库存";
            this.计划库存.ReadOnly = true;
            this.计划库存.Visible = false;
            this.计划库存.Width = 70;
            // 
            // dataGridViewTextBoxColumn89
            // 
            this.dataGridViewTextBoxColumn89.DataPropertyName = "备注";
            this.dataGridViewTextBoxColumn89.HeaderText = "备注";
            this.dataGridViewTextBoxColumn89.Name = "dataGridViewTextBoxColumn89";
            this.dataGridViewTextBoxColumn89.ReadOnly = true;
            this.dataGridViewTextBoxColumn89.Visible = false;
            this.dataGridViewTextBoxColumn89.Width = 50;
            // 
            // dataGridViewTextBoxColumn90
            // 
            this.dataGridViewTextBoxColumn90.DataPropertyName = "种类";
            this.dataGridViewTextBoxColumn90.HeaderText = "种类";
            this.dataGridViewTextBoxColumn90.Name = "dataGridViewTextBoxColumn90";
            this.dataGridViewTextBoxColumn90.ReadOnly = true;
            this.dataGridViewTextBoxColumn90.Visible = false;
            this.dataGridViewTextBoxColumn90.Width = 39;
            // 
            // dataGridViewTextBoxColumn91
            // 
            this.dataGridViewTextBoxColumn91.DataPropertyName = "状态";
            this.dataGridViewTextBoxColumn91.HeaderText = "状态";
            this.dataGridViewTextBoxColumn91.Name = "dataGridViewTextBoxColumn91";
            this.dataGridViewTextBoxColumn91.ReadOnly = true;
            this.dataGridViewTextBoxColumn91.Visible = false;
            this.dataGridViewTextBoxColumn91.Width = 39;
            // 
            // LY_SalesProduct_Sel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(671, 573);
            this.Controls.Add(this.ly_lsptb_selDataGridView);
            this.Controls.Add(this.bindingNavigator3);
            this.Name = "LY_SalesProduct_Sel";
            this.Text = "营业产品选择";
            this.Load += new System.EventHandler(this.LY_SalesProduct_Sel_Load);
            ((System.ComponentModel.ISupportInitialize)(this.lYSalseMange)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ly_lsptb_selBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ly_lsptb_selDataGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingNavigator3)).EndInit();
            this.bindingNavigator3.ResumeLayout(false);
            this.bindingNavigator3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private HappyYF.YuanXin.Data.LYSalseMange lYSalseMange;
        private System.Windows.Forms.BindingSource ly_lsptb_selBindingSource;
        private HappyYF.YuanXin.Data.LYSalseMangeTableAdapters.ly_lsptb_selTableAdapter ly_lsptb_selTableAdapter;
        private HappyYF.YuanXin.Data.LYSalseMangeTableAdapters.TableAdapterManager tableAdapterManager;
        private System.Windows.Forms.DataGridView ly_lsptb_selDataGridView;
        private System.Windows.Forms.BindingNavigator bindingNavigator3;
        private System.Windows.Forms.ToolStripLabel toolStripLabel3;
        private System.Windows.Forms.ToolStripLabel toolStripLabel4;
        private System.Windows.Forms.ToolStripTextBox toolStripTextBox6;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator14;
        private System.Windows.Forms.ToolStripButton toolStripButton22;
        private System.Windows.Forms.ToolStripButton toolStripButton23;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator15;
        private System.Windows.Forms.ToolStripTextBox toolStripTextBox7;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator16;
        private System.Windows.Forms.ToolStripButton toolStripButton24;
        private System.Windows.Forms.ToolStripButton toolStripButton25;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator3;
        private System.Windows.Forms.ToolStripButton toolStripButton26;
        private System.Windows.Forms.ToolStripButton bindingNavigatorAddNewItem1;
        private System.Windows.Forms.ToolStripButton bindingNavigatorDeleteItem1;
        private System.Windows.Forms.ToolStripButton toolStripButton29;
        private System.Windows.Forms.DataGridViewTextBoxColumn lsptbId;
        private System.Windows.Forms.DataGridViewTextBoxColumn warehouse;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn82;
        private System.Windows.Forms.DataGridViewTextBoxColumn 物料编号2;
        private System.Windows.Forms.DataGridViewTextBoxColumn 物料名称2;
        private System.Windows.Forms.DataGridViewTextBoxColumn mch_jp;
        private System.Windows.Forms.DataGridViewTextBoxColumn mch_py;
        private System.Windows.Forms.DataGridViewTextBoxColumn 中方型号2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn84;
        private System.Windows.Forms.DataGridViewTextBoxColumn 单位2;
        private System.Windows.Forms.DataGridViewTextBoxColumn 营业定价3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn44;
        private System.Windows.Forms.DataGridViewTextBoxColumn 台用量;
        private System.Windows.Forms.DataGridViewTextBoxColumn 领用数;
        private System.Windows.Forms.DataGridViewTextBoxColumn 库存;
        private System.Windows.Forms.DataGridViewTextBoxColumn 计划库存;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn89;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn90;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn91;
    }
}