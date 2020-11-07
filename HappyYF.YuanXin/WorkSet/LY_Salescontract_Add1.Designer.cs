namespace HappyYF.YuanXin.WorkSet
{
    partial class LY_Salescontract_Add1
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
            System.Windows.Forms.Label idLabel;
            System.Windows.Forms.Label 客户编码Label;
            System.Windows.Forms.Label 录入日期Label;
            System.Windows.Forms.Label 录入人Label;
            System.Windows.Forms.Label 业务编码Label;
            System.Windows.Forms.Label 备注Label;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LY_Salescontract_Add1));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.备注TextBox = new System.Windows.Forms.TextBox();
            this.ly_sales_business_singleBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.lYSalseMange = new HappyYF.YuanXin.Data.LYSalseMange();
            this.button8 = new System.Windows.Forms.Button();
            this.客户编码ComboBox = new System.Windows.Forms.ComboBox();
            this.lysalesclientBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.录入日期DateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.录入人TextBox1 = new System.Windows.Forms.TextBox();
            this.合同编码TextBox = new System.Windows.Forms.TextBox();
            this.签订日期DateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.idTextBox = new System.Windows.Forms.TextBox();
            this.tableAdapterManager = new HappyYF.YuanXin.Data.LYSalseMangeTableAdapters.TableAdapterManager();
            this.ly_sales_clientTableAdapter = new HappyYF.YuanXin.Data.LYSalseMangeTableAdapters.ly_sales_clientTableAdapter();
            this.ly_sales_business_singleTableAdapter = new HappyYF.YuanXin.Data.LYSalseMangeTableAdapters.ly_sales_business_singleTableAdapter();
            idLabel = new System.Windows.Forms.Label();
            客户编码Label = new System.Windows.Forms.Label();
            录入日期Label = new System.Windows.Forms.Label();
            录入人Label = new System.Windows.Forms.Label();
            业务编码Label = new System.Windows.Forms.Label();
            备注Label = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ly_sales_business_singleBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lYSalseMange)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lysalesclientBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // idLabel
            // 
            idLabel.AutoSize = true;
            idLabel.Location = new System.Drawing.Point(431, 98);
            idLabel.Name = "idLabel";
            idLabel.Size = new System.Drawing.Size(23, 12);
            idLabel.TabIndex = 0;
            idLabel.Text = "id:";
            // 
            // 客户编码Label
            // 
            客户编码Label.AutoSize = true;
            客户编码Label.Location = new System.Drawing.Point(38, 65);
            客户编码Label.Name = "客户编码Label";
            客户编码Label.Size = new System.Drawing.Size(59, 12);
            客户编码Label.TabIndex = 12;
            客户编码Label.Text = "客户编码:";
            // 
            // 录入日期Label
            // 
            录入日期Label.AutoSize = true;
            录入日期Label.Location = new System.Drawing.Point(38, 93);
            录入日期Label.Name = "录入日期Label";
            录入日期Label.Size = new System.Drawing.Size(59, 12);
            录入日期Label.TabIndex = 28;
            录入日期Label.Text = "录入日期:";
            // 
            // 录入人Label
            // 
            录入人Label.AutoSize = true;
            录入人Label.Location = new System.Drawing.Point(38, 184);
            录入人Label.Name = "录入人Label";
            录入人Label.Size = new System.Drawing.Size(47, 12);
            录入人Label.TabIndex = 30;
            录入人Label.Text = "录入人:";
            // 
            // 业务编码Label
            // 
            业务编码Label.AutoSize = true;
            业务编码Label.Location = new System.Drawing.Point(38, 41);
            业务编码Label.Name = "业务编码Label";
            业务编码Label.Size = new System.Drawing.Size(59, 12);
            业务编码Label.TabIndex = 89;
            业务编码Label.Text = "业务编码:";
            // 
            // 备注Label
            // 
            备注Label.AutoSize = true;
            备注Label.Location = new System.Drawing.Point(38, 119);
            备注Label.Name = "备注Label";
            备注Label.Size = new System.Drawing.Size(35, 12);
            备注Label.TabIndex = 89;
            备注Label.Text = "备注:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(备注Label);
            this.groupBox1.Controls.Add(this.备注TextBox);
            this.groupBox1.Controls.Add(业务编码Label);
            this.groupBox1.Controls.Add(this.button8);
            this.groupBox1.Controls.Add(this.客户编码ComboBox);
            this.groupBox1.Controls.Add(this.录入日期DateTimePicker);
            this.groupBox1.Controls.Add(this.button2);
            this.groupBox1.Controls.Add(录入日期Label);
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(客户编码Label);
            this.groupBox1.Controls.Add(录入人Label);
            this.groupBox1.Controls.Add(this.录入人TextBox1);
            this.groupBox1.Controls.Add(this.合同编码TextBox);
            this.groupBox1.Controls.Add(this.签订日期DateTimePicker);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(531, 269);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // 备注TextBox
            // 
            this.备注TextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.ly_sales_business_singleBindingSource, "备注", true));
            this.备注TextBox.Location = new System.Drawing.Point(103, 116);
            this.备注TextBox.Multiline = true;
            this.备注TextBox.Name = "备注TextBox";
            this.备注TextBox.Size = new System.Drawing.Size(319, 59);
            this.备注TextBox.TabIndex = 90;
            // 
            // ly_sales_business_singleBindingSource
            // 
            this.ly_sales_business_singleBindingSource.DataMember = "ly_sales_business_single";
            this.ly_sales_business_singleBindingSource.DataSource = this.lYSalseMange;
            // 
            // lYSalseMange
            // 
            this.lYSalseMange.DataSetName = "LYSalseMange";
            this.lYSalseMange.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // button8
            // 
            this.button8.Image = ((System.Drawing.Image)(resources.GetObject("button8.Image")));
            this.button8.Location = new System.Drawing.Point(428, 62);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(23, 23);
            this.button8.TabIndex = 88;
            this.button8.UseVisualStyleBackColor = true;
            this.button8.Click += new System.EventHandler(this.button8_Click);
            // 
            // 客户编码ComboBox
            // 
            this.客户编码ComboBox.DataBindings.Add(new System.Windows.Forms.Binding("SelectedValue", this.ly_sales_business_singleBindingSource, "客户编码", true));
            this.客户编码ComboBox.DataSource = this.lysalesclientBindingSource;
            this.客户编码ComboBox.DisplayMember = "客户名称";
            this.客户编码ComboBox.Enabled = false;
            this.客户编码ComboBox.FormattingEnabled = true;
            this.客户编码ComboBox.Location = new System.Drawing.Point(103, 62);
            this.客户编码ComboBox.Name = "客户编码ComboBox";
            this.客户编码ComboBox.Size = new System.Drawing.Size(319, 20);
            this.客户编码ComboBox.TabIndex = 32;
            this.客户编码ComboBox.ValueMember = "客户编码";
            // 
            // lysalesclientBindingSource
            // 
            this.lysalesclientBindingSource.DataMember = "ly_sales_client";
            this.lysalesclientBindingSource.DataSource = this.lYSalseMange;
            // 
            // 录入日期DateTimePicker
            // 
            this.录入日期DateTimePicker.DataBindings.Add(new System.Windows.Forms.Binding("Value", this.ly_sales_business_singleBindingSource, "录入日期", true));
            this.录入日期DateTimePicker.Location = new System.Drawing.Point(103, 89);
            this.录入日期DateTimePicker.Name = "录入日期DateTimePicker";
            this.录入日期DateTimePicker.Size = new System.Drawing.Size(319, 21);
            this.录入日期DateTimePicker.TabIndex = 29;
            // 
            // button2
            // 
            this.button2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button2.Location = new System.Drawing.Point(370, 221);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 2;
            this.button2.Text = "取消";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(289, 221);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "确定";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // 录入人TextBox1
            // 
            this.录入人TextBox1.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.ly_sales_business_singleBindingSource, "录入人", true));
            this.录入人TextBox1.Enabled = false;
            this.录入人TextBox1.Location = new System.Drawing.Point(103, 181);
            this.录入人TextBox1.Name = "录入人TextBox1";
            this.录入人TextBox1.ReadOnly = true;
            this.录入人TextBox1.Size = new System.Drawing.Size(319, 21);
            this.录入人TextBox1.TabIndex = 31;
            // 
            // 合同编码TextBox
            // 
            this.合同编码TextBox.BackColor = System.Drawing.Color.Teal;
            this.合同编码TextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.ly_sales_business_singleBindingSource, "业务编码", true));
            this.合同编码TextBox.ForeColor = System.Drawing.Color.White;
            this.合同编码TextBox.Location = new System.Drawing.Point(103, 35);
            this.合同编码TextBox.Name = "合同编码TextBox";
            this.合同编码TextBox.Size = new System.Drawing.Size(319, 21);
            this.合同编码TextBox.TabIndex = 7;
            // 
            // 签订日期DateTimePicker
            // 
            this.签订日期DateTimePicker.Location = new System.Drawing.Point(103, 89);
            this.签订日期DateTimePicker.Name = "签订日期DateTimePicker";
            this.签订日期DateTimePicker.Size = new System.Drawing.Size(200, 21);
            this.签订日期DateTimePicker.TabIndex = 89;
            // 
            // idTextBox
            // 
            this.idTextBox.Enabled = false;
            this.idTextBox.Location = new System.Drawing.Point(460, 95);
            this.idTextBox.Name = "idTextBox";
            this.idTextBox.Size = new System.Drawing.Size(43, 21);
            this.idTextBox.TabIndex = 1;
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
            this.tableAdapterManager.ly_sales_client_other_changeTableAdapter = null;
            this.tableAdapterManager.ly_sales_client_otherTableAdapter = null;
            this.tableAdapterManager.ly_sales_client_receivablesTableAdapter = null;
            this.tableAdapterManager.ly_sales_clientreceivablesNewsTableAdapter = null;
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
            this.tableAdapterManager.t_financeReceivables_otherTableAdapter = null;
            this.tableAdapterManager.t_financeReceivablesTableAdapter = null;
            this.tableAdapterManager.UpdateOrder = HappyYF.YuanXin.Data.LYSalseMangeTableAdapters.TableAdapterManager.UpdateOrderOption.InsertUpdateDelete;
            // 
            // ly_sales_clientTableAdapter
            // 
            this.ly_sales_clientTableAdapter.ClearBeforeFill = true;
            // 
            // ly_sales_business_singleTableAdapter
            // 
            this.ly_sales_business_singleTableAdapter.ClearBeforeFill = true;
            // 
            // LY_Salescontract_Add1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(531, 269);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.idTextBox);
            this.Controls.Add(idLabel);
            this.Name = "LY_Salescontract_Add1";
            this.Text = "增加修改营业业务信息";
            this.Load += new System.EventHandler(this.LY_MaterialAdd_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ly_sales_business_singleBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lYSalseMange)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lysalesclientBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private HappyYF.YuanXin.Data.LYSalseMange lYSalseMange;
        private HappyYF.YuanXin.Data.LYSalseMangeTableAdapters.TableAdapterManager tableAdapterManager;
        private System.Windows.Forms.TextBox idTextBox;
        private System.Windows.Forms.TextBox 合同编码TextBox;
        private System.Windows.Forms.DateTimePicker 录入日期DateTimePicker;
        private System.Windows.Forms.TextBox 录入人TextBox1;
        private System.Windows.Forms.ComboBox 客户编码ComboBox;
        private System.Windows.Forms.BindingSource lysalesclientBindingSource;
        private HappyYF.YuanXin.Data.LYSalseMangeTableAdapters.ly_sales_clientTableAdapter ly_sales_clientTableAdapter;
        private System.Windows.Forms.Button button8;
        private System.Windows.Forms.DateTimePicker 签订日期DateTimePicker;
        private System.Windows.Forms.BindingSource ly_sales_business_singleBindingSource;
        private HappyYF.YuanXin.Data.LYSalseMangeTableAdapters.ly_sales_business_singleTableAdapter ly_sales_business_singleTableAdapter;
        private System.Windows.Forms.TextBox 备注TextBox;
    }
}