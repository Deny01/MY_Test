namespace HappyYF.YuanXin.WorkSet
{
    partial class LY_Salesborror_Add
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
            System.Windows.Forms.Label idLabel1;
            System.Windows.Forms.Label 借用单号Label;
            System.Windows.Forms.Label 借用日期Label;
            System.Windows.Forms.Label 借用类别Label;
            System.Windows.Forms.Label 开始日期Label;
            System.Windows.Forms.Label 返还日期Label;
            System.Windows.Forms.Label 借用目的Label;
            System.Windows.Forms.Label 业务编号Label;
            System.Windows.Forms.Label 客户编码Label1;
            System.Windows.Forms.Label 备注Label1;
            System.Windows.Forms.Label 录入日期Label1;
            System.Windows.Forms.Label 借用人Label;
            System.Windows.Forms.Label 借用部门Label;
            this.客户编码ComboBox = new System.Windows.Forms.ComboBox();
            this.ly_sales_borrow_singleBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.lYSalseMange = new HappyYF.YuanXin.Data.LYSalseMange();
            this.lysalesclientBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.内部编码TextBox = new System.Windows.Forms.TextBox();
            this.合同编码TextBox = new System.Windows.Forms.TextBox();
            this.tableAdapterManager = new HappyYF.YuanXin.Data.LYSalseMangeTableAdapters.TableAdapterManager();
            this.idTextBox1 = new System.Windows.Forms.TextBox();
            this.借用日期DateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.借用类别ComboBox = new System.Windows.Forms.ComboBox();
            this.lysalesborrowstyleBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.开始日期DateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.返还日期DateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.借用目的TextBox = new System.Windows.Forms.TextBox();
            this.备注TextBox1 = new System.Windows.Forms.TextBox();
            this.录入日期DateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.借用人TextBox = new System.Windows.Forms.TextBox();
            this.借用部门TextBox = new System.Windows.Forms.TextBox();
            this.ly_sales_clientTableAdapter = new HappyYF.YuanXin.Data.LYSalseMangeTableAdapters.ly_sales_clientTableAdapter();
            this.ly_sales_borrow_singleTableAdapter = new HappyYF.YuanXin.Data.LYSalseMangeTableAdapters.ly_sales_borrow_singleTableAdapter();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.ly_sales_borrow_styleTableAdapter = new HappyYF.YuanXin.Data.LYSalseMangeTableAdapters.ly_sales_borrow_styleTableAdapter();
            idLabel1 = new System.Windows.Forms.Label();
            借用单号Label = new System.Windows.Forms.Label();
            借用日期Label = new System.Windows.Forms.Label();
            借用类别Label = new System.Windows.Forms.Label();
            开始日期Label = new System.Windows.Forms.Label();
            返还日期Label = new System.Windows.Forms.Label();
            借用目的Label = new System.Windows.Forms.Label();
            业务编号Label = new System.Windows.Forms.Label();
            客户编码Label1 = new System.Windows.Forms.Label();
            备注Label1 = new System.Windows.Forms.Label();
            录入日期Label1 = new System.Windows.Forms.Label();
            借用人Label = new System.Windows.Forms.Label();
            借用部门Label = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.ly_sales_borrow_singleBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lYSalseMange)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lysalesclientBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lysalesborrowstyleBindingSource)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // idLabel1
            // 
            idLabel1.AutoSize = true;
            idLabel1.Location = new System.Drawing.Point(125, 36);
            idLabel1.Name = "idLabel1";
            idLabel1.Size = new System.Drawing.Size(23, 12);
            idLabel1.TabIndex = 0;
            idLabel1.Text = "id:";
            // 
            // 借用单号Label
            // 
            借用单号Label.AutoSize = true;
            借用单号Label.Location = new System.Drawing.Point(25, 94);
            借用单号Label.Name = "借用单号Label";
            借用单号Label.Size = new System.Drawing.Size(59, 12);
            借用单号Label.TabIndex = 2;
            借用单号Label.Text = "借用单号:";
            // 
            // 借用日期Label
            // 
            借用日期Label.AutoSize = true;
            借用日期Label.Location = new System.Drawing.Point(25, 68);
            借用日期Label.Name = "借用日期Label";
            借用日期Label.Size = new System.Drawing.Size(59, 12);
            借用日期Label.TabIndex = 4;
            借用日期Label.Text = "借用日期:";
            // 
            // 借用类别Label
            // 
            借用类别Label.AutoSize = true;
            借用类别Label.Location = new System.Drawing.Point(25, 148);
            借用类别Label.Name = "借用类别Label";
            借用类别Label.Size = new System.Drawing.Size(59, 12);
            借用类别Label.TabIndex = 6;
            借用类别Label.Text = "借用类别:";
            借用类别Label.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.借用类别Label_MouseDoubleClick);
            // 
            // 开始日期Label
            // 
            开始日期Label.AutoSize = true;
            开始日期Label.Location = new System.Drawing.Point(305, 67);
            开始日期Label.Name = "开始日期Label";
            开始日期Label.Size = new System.Drawing.Size(59, 12);
            开始日期Label.TabIndex = 8;
            开始日期Label.Text = "开始日期:";
            // 
            // 返还日期Label
            // 
            返还日期Label.AutoSize = true;
            返还日期Label.Location = new System.Drawing.Point(305, 94);
            返还日期Label.Name = "返还日期Label";
            返还日期Label.Size = new System.Drawing.Size(59, 12);
            返还日期Label.TabIndex = 10;
            返还日期Label.Text = "返还日期:";
            // 
            // 借用目的Label
            // 
            借用目的Label.AutoSize = true;
            借用目的Label.Location = new System.Drawing.Point(305, 122);
            借用目的Label.Name = "借用目的Label";
            借用目的Label.Size = new System.Drawing.Size(59, 12);
            借用目的Label.TabIndex = 12;
            借用目的Label.Text = "借用目的:";
            // 
            // 业务编号Label
            // 
            业务编号Label.AutoSize = true;
            业务编号Label.Location = new System.Drawing.Point(25, 121);
            业务编号Label.Name = "业务编号Label";
            业务编号Label.Size = new System.Drawing.Size(59, 12);
            业务编号Label.TabIndex = 14;
            业务编号Label.Text = "业务编号:";
            // 
            // 客户编码Label1
            // 
            客户编码Label1.AutoSize = true;
            客户编码Label1.Location = new System.Drawing.Point(25, 28);
            客户编码Label1.Name = "客户编码Label1";
            客户编码Label1.Size = new System.Drawing.Size(59, 12);
            客户编码Label1.TabIndex = 16;
            客户编码Label1.Text = "客    户:";
            // 
            // 备注Label1
            // 
            备注Label1.AutoSize = true;
            备注Label1.Location = new System.Drawing.Point(305, 201);
            备注Label1.Name = "备注Label1";
            备注Label1.Size = new System.Drawing.Size(35, 12);
            备注Label1.TabIndex = 36;
            备注Label1.Text = "备注:";
            // 
            // 录入日期Label1
            // 
            录入日期Label1.AutoSize = true;
            录入日期Label1.Location = new System.Drawing.Point(25, 229);
            录入日期Label1.Name = "录入日期Label1";
            录入日期Label1.Size = new System.Drawing.Size(59, 12);
            录入日期Label1.TabIndex = 38;
            录入日期Label1.Text = "录入日期:";
            // 
            // 借用人Label
            // 
            借用人Label.AutoSize = true;
            借用人Label.Location = new System.Drawing.Point(25, 174);
            借用人Label.Name = "借用人Label";
            借用人Label.Size = new System.Drawing.Size(47, 12);
            借用人Label.TabIndex = 42;
            借用人Label.Text = "借用人:";
            // 
            // 借用部门Label
            // 
            借用部门Label.AutoSize = true;
            借用部门Label.Location = new System.Drawing.Point(25, 201);
            借用部门Label.Name = "借用部门Label";
            借用部门Label.Size = new System.Drawing.Size(59, 12);
            借用部门Label.TabIndex = 44;
            借用部门Label.Text = "借用部门:";
            // 
            // 客户编码ComboBox
            // 
            this.客户编码ComboBox.DataBindings.Add(new System.Windows.Forms.Binding("SelectedValue", this.ly_sales_borrow_singleBindingSource, "客户编码", true));
            this.客户编码ComboBox.DataSource = this.lysalesclientBindingSource;
            this.客户编码ComboBox.DisplayMember = "客户名称";
            this.客户编码ComboBox.Enabled = false;
            this.客户编码ComboBox.ForeColor = System.Drawing.Color.Teal;
            this.客户编码ComboBox.FormattingEnabled = true;
            this.客户编码ComboBox.Location = new System.Drawing.Point(90, 25);
            this.客户编码ComboBox.Name = "客户编码ComboBox";
            this.客户编码ComboBox.Size = new System.Drawing.Size(485, 20);
            this.客户编码ComboBox.TabIndex = 38;
            this.客户编码ComboBox.ValueMember = "客户编码";
            // 
            // ly_sales_borrow_singleBindingSource
            // 
            this.ly_sales_borrow_singleBindingSource.DataMember = "ly_sales_borrow_single";
            this.ly_sales_borrow_singleBindingSource.DataSource = this.lYSalseMange;
            // 
            // lYSalseMange
            // 
            this.lYSalseMange.DataSetName = "LYSalseMange";
            this.lYSalseMange.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // lysalesclientBindingSource
            // 
            this.lysalesclientBindingSource.DataMember = "ly_sales_client";
            this.lysalesclientBindingSource.DataSource = this.lYSalseMange;
            // 
            // 内部编码TextBox
            // 
            this.内部编码TextBox.BackColor = System.Drawing.Color.Teal;
            this.内部编码TextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.ly_sales_borrow_singleBindingSource, "借用单号", true));
            this.内部编码TextBox.ForeColor = System.Drawing.Color.White;
            this.内部编码TextBox.Location = new System.Drawing.Point(90, 91);
            this.内部编码TextBox.Name = "内部编码TextBox";
            this.内部编码TextBox.ReadOnly = true;
            this.内部编码TextBox.Size = new System.Drawing.Size(182, 21);
            this.内部编码TextBox.TabIndex = 5;
            // 
            // 合同编码TextBox
            // 
            this.合同编码TextBox.BackColor = System.Drawing.Color.Teal;
            this.合同编码TextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.ly_sales_borrow_singleBindingSource, "业务编号", true));
            this.合同编码TextBox.ForeColor = System.Drawing.Color.White;
            this.合同编码TextBox.Location = new System.Drawing.Point(90, 118);
            this.合同编码TextBox.Name = "合同编码TextBox";
            this.合同编码TextBox.Size = new System.Drawing.Size(182, 21);
            this.合同编码TextBox.TabIndex = 7;
            // 
            // tableAdapterManager
            // 
            this.tableAdapterManager.BackupDataSetBeforeUpdate = false;
            this.tableAdapterManager.Connection = null;
            this.tableAdapterManager.ly_company_informationTableAdapter = null;
            this.tableAdapterManager.ly_express_companyTableAdapter = null;
            this.tableAdapterManager.ly_lsptb_selTableAdapter = null;
            this.tableAdapterManager.ly_material_plan_mainperiodproTableAdapter = null;
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
            this.tableAdapterManager.ly_sales_client_receivablesTableAdapter = null;
            this.tableAdapterManager.ly_sales_clientTableAdapter = null;
            this.tableAdapterManager.ly_sales_contract_classTableAdapter = null;
            this.tableAdapterManager.ly_sales_contract_detailTableAdapter = null;
            this.tableAdapterManager.ly_sales_contract_main_forbusinessTableAdapter = null;
            this.tableAdapterManager.ly_sales_contract_main_forbusinessZCTableAdapter = null;
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
            // idTextBox1
            // 
            this.idTextBox1.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.ly_sales_borrow_singleBindingSource, "id", true));
            this.idTextBox1.Location = new System.Drawing.Point(178, 33);
            this.idTextBox1.Name = "idTextBox1";
            this.idTextBox1.Size = new System.Drawing.Size(200, 21);
            this.idTextBox1.TabIndex = 1;
            // 
            // 借用日期DateTimePicker
            // 
            this.借用日期DateTimePicker.CustomFormat = "yyyy-MM-dd";
            this.借用日期DateTimePicker.DataBindings.Add(new System.Windows.Forms.Binding("Value", this.ly_sales_borrow_singleBindingSource, "借用日期", true));
            this.借用日期DateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.借用日期DateTimePicker.Location = new System.Drawing.Point(90, 64);
            this.借用日期DateTimePicker.Name = "借用日期DateTimePicker";
            this.借用日期DateTimePicker.Size = new System.Drawing.Size(182, 21);
            this.借用日期DateTimePicker.TabIndex = 5;
            // 
            // 借用类别ComboBox
            // 
            this.借用类别ComboBox.DataBindings.Add(new System.Windows.Forms.Binding("SelectedValue", this.ly_sales_borrow_singleBindingSource, "借用类别", true));
            this.借用类别ComboBox.DataSource = this.lysalesborrowstyleBindingSource;
            this.借用类别ComboBox.DisplayMember = "类别名称";
            this.借用类别ComboBox.Enabled = false;
            this.借用类别ComboBox.ForeColor = System.Drawing.Color.Teal;
            this.借用类别ComboBox.FormattingEnabled = true;
            this.借用类别ComboBox.Location = new System.Drawing.Point(90, 145);
            this.借用类别ComboBox.Name = "借用类别ComboBox";
            this.借用类别ComboBox.Size = new System.Drawing.Size(182, 20);
            this.借用类别ComboBox.TabIndex = 7;
            this.借用类别ComboBox.ValueMember = "类别编码";
            // 
            // lysalesborrowstyleBindingSource
            // 
            this.lysalesborrowstyleBindingSource.DataMember = "ly_sales_borrow_style";
            this.lysalesborrowstyleBindingSource.DataSource = this.lYSalseMange;
            // 
            // 开始日期DateTimePicker
            // 
            this.开始日期DateTimePicker.CustomFormat = "yyyy-MM-dd";
            this.开始日期DateTimePicker.DataBindings.Add(new System.Windows.Forms.Binding("Value", this.ly_sales_borrow_singleBindingSource, "开始日期", true));
            this.开始日期DateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.开始日期DateTimePicker.Location = new System.Drawing.Point(370, 63);
            this.开始日期DateTimePicker.Name = "开始日期DateTimePicker";
            this.开始日期DateTimePicker.Size = new System.Drawing.Size(205, 21);
            this.开始日期DateTimePicker.TabIndex = 9;
            // 
            // 返还日期DateTimePicker
            // 
            this.返还日期DateTimePicker.CustomFormat = "yyyy-MM-dd";
            this.返还日期DateTimePicker.DataBindings.Add(new System.Windows.Forms.Binding("Value", this.ly_sales_borrow_singleBindingSource, "返还日期", true));
            this.返还日期DateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.返还日期DateTimePicker.Location = new System.Drawing.Point(370, 90);
            this.返还日期DateTimePicker.Name = "返还日期DateTimePicker";
            this.返还日期DateTimePicker.Size = new System.Drawing.Size(205, 21);
            this.返还日期DateTimePicker.TabIndex = 11;
            // 
            // 借用目的TextBox
            // 
            this.借用目的TextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.ly_sales_borrow_singleBindingSource, "借用目的", true));
            this.借用目的TextBox.Location = new System.Drawing.Point(370, 117);
            this.借用目的TextBox.Multiline = true;
            this.借用目的TextBox.Name = "借用目的TextBox";
            this.借用目的TextBox.Size = new System.Drawing.Size(205, 65);
            this.借用目的TextBox.TabIndex = 13;
            // 
            // 备注TextBox1
            // 
            this.备注TextBox1.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.ly_sales_borrow_singleBindingSource, "备注", true));
            this.备注TextBox1.Location = new System.Drawing.Point(370, 188);
            this.备注TextBox1.Multiline = true;
            this.备注TextBox1.Name = "备注TextBox1";
            this.备注TextBox1.Size = new System.Drawing.Size(205, 58);
            this.备注TextBox1.TabIndex = 37;
            // 
            // 录入日期DateTimePicker1
            // 
            this.录入日期DateTimePicker1.CustomFormat = "yyyy-MM-dd";
            this.录入日期DateTimePicker1.DataBindings.Add(new System.Windows.Forms.Binding("Value", this.ly_sales_borrow_singleBindingSource, "录入日期", true));
            this.录入日期DateTimePicker1.Enabled = false;
            this.录入日期DateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.录入日期DateTimePicker1.Location = new System.Drawing.Point(90, 225);
            this.录入日期DateTimePicker1.Name = "录入日期DateTimePicker1";
            this.录入日期DateTimePicker1.Size = new System.Drawing.Size(182, 21);
            this.录入日期DateTimePicker1.TabIndex = 39;
            // 
            // 借用人TextBox
            // 
            this.借用人TextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.ly_sales_borrow_singleBindingSource, "借用人", true));
            this.借用人TextBox.Location = new System.Drawing.Point(90, 171);
            this.借用人TextBox.Name = "借用人TextBox";
            this.借用人TextBox.ReadOnly = true;
            this.借用人TextBox.Size = new System.Drawing.Size(182, 21);
            this.借用人TextBox.TabIndex = 43;
            // 
            // 借用部门TextBox
            // 
            this.借用部门TextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.ly_sales_borrow_singleBindingSource, "借用部门", true));
            this.借用部门TextBox.Location = new System.Drawing.Point(90, 198);
            this.借用部门TextBox.Name = "借用部门TextBox";
            this.借用部门TextBox.ReadOnly = true;
            this.借用部门TextBox.Size = new System.Drawing.Size(182, 21);
            this.借用部门TextBox.TabIndex = 45;
            // 
            // ly_sales_clientTableAdapter
            // 
            this.ly_sales_clientTableAdapter.ClearBeforeFill = true;
            // 
            // ly_sales_borrow_singleTableAdapter
            // 
            this.ly_sales_borrow_singleTableAdapter.ClearBeforeFill = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.button2);
            this.groupBox2.Controls.Add(this.button1);
            this.groupBox2.Controls.Add(this.客户编码ComboBox);
            this.groupBox2.Controls.Add(借用单号Label);
            this.groupBox2.Controls.Add(借用日期Label);
            this.groupBox2.Controls.Add(this.借用日期DateTimePicker);
            this.groupBox2.Controls.Add(this.内部编码TextBox);
            this.groupBox2.Controls.Add(this.合同编码TextBox);
            this.groupBox2.Controls.Add(备注Label1);
            this.groupBox2.Controls.Add(借用类别Label);
            this.groupBox2.Controls.Add(this.备注TextBox1);
            this.groupBox2.Controls.Add(客户编码Label1);
            this.groupBox2.Controls.Add(录入日期Label1);
            this.groupBox2.Controls.Add(this.借用类别ComboBox);
            this.groupBox2.Controls.Add(this.录入日期DateTimePicker1);
            this.groupBox2.Controls.Add(开始日期Label);
            this.groupBox2.Controls.Add(业务编号Label);
            this.groupBox2.Controls.Add(借用人Label);
            this.groupBox2.Controls.Add(this.开始日期DateTimePicker);
            this.groupBox2.Controls.Add(this.借用人TextBox);
            this.groupBox2.Controls.Add(this.借用目的TextBox);
            this.groupBox2.Controls.Add(借用部门Label);
            this.groupBox2.Controls.Add(返还日期Label);
            this.groupBox2.Controls.Add(this.借用部门TextBox);
            this.groupBox2.Controls.Add(借用目的Label);
            this.groupBox2.Controls.Add(this.返还日期DateTimePicker);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(599, 327);
            this.groupBox2.TabIndex = 46;
            this.groupBox2.TabStop = false;
            // 
            // button2
            // 
            this.button2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button2.Location = new System.Drawing.Point(452, 269);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 47;
            this.button2.Text = "取消";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(371, 269);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 46;
            this.button1.Text = "确定";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // ly_sales_borrow_styleTableAdapter
            // 
            this.ly_sales_borrow_styleTableAdapter.ClearBeforeFill = true;
            // 
            // LY_Salesborror_Add
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(599, 327);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.idTextBox1);
            this.Controls.Add(idLabel1);
            this.Name = "LY_Salesborror_Add";
            this.Text = "增加修改借用信息";
            this.Load += new System.EventHandler(this.LY_MaterialAdd_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ly_sales_borrow_singleBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lYSalseMange)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lysalesclientBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lysalesborrowstyleBindingSource)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private HappyYF.YuanXin.Data.LYSalseMange lYSalseMange;
        private HappyYF.YuanXin.Data.LYSalseMangeTableAdapters.TableAdapterManager tableAdapterManager;
        private System.Windows.Forms.TextBox 内部编码TextBox;
        private System.Windows.Forms.TextBox 合同编码TextBox;
        private System.Windows.Forms.ComboBox 客户编码ComboBox;
        private System.Windows.Forms.BindingSource lysalesclientBindingSource;
        private HappyYF.YuanXin.Data.LYSalseMangeTableAdapters.ly_sales_clientTableAdapter ly_sales_clientTableAdapter;
        private System.Windows.Forms.BindingSource ly_sales_borrow_singleBindingSource;
        private HappyYF.YuanXin.Data.LYSalseMangeTableAdapters.ly_sales_borrow_singleTableAdapter ly_sales_borrow_singleTableAdapter;
        private System.Windows.Forms.TextBox idTextBox1;
        private System.Windows.Forms.DateTimePicker 借用日期DateTimePicker;
        private System.Windows.Forms.ComboBox 借用类别ComboBox;
        private System.Windows.Forms.DateTimePicker 开始日期DateTimePicker;
        private System.Windows.Forms.DateTimePicker 返还日期DateTimePicker;
        private System.Windows.Forms.TextBox 借用目的TextBox;
        private System.Windows.Forms.TextBox 备注TextBox1;
        private System.Windows.Forms.DateTimePicker 录入日期DateTimePicker1;
        private System.Windows.Forms.TextBox 借用人TextBox;
        private System.Windows.Forms.TextBox 借用部门TextBox;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.BindingSource lysalesborrowstyleBindingSource;
        private HappyYF.YuanXin.Data.LYSalseMangeTableAdapters.ly_sales_borrow_styleTableAdapter ly_sales_borrow_styleTableAdapter;
    }
}