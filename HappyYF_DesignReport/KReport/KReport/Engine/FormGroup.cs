namespace KReport.Engine
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class FormGroup : Form
    {
        private Button btnAdicionar;
        private Button BtnOk;
        private Button button3;
        private ComboBox comboFields;
        private IContainer components = null;
        private Report currentReport = null;
        private ReportDataFieldInfo[] fields;
        private static FormGroup formgroup = null;
        private Label label1;
        private ListBox listGroups;
        DesignerReport parent;

        public FormGroup()
        {
            this.InitializeComponent();
        }

        private void btnAdicionar_Click(object sender, EventArgs e)
        {
            if (!this.HasGroup(this.comboFields.SelectedItem.ToString()))
            {
                this.listGroups.Items.Add(this.comboFields.SelectedItem.ToString());
            }
        }

        private void BtnOk_Click(object sender, EventArgs e)
        {
            foreach (string str in this.listGroups.Items)
            {
                if (!this.HasReportGroup(str))
                {
                    BandGroup band = new BandGroup {
                        FieldName = str
                    };
                    this.currentReport.AddBand(band);
                }
            }
            base.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {

            //if (this.HasReportGroup(this.listGroups.SelectedItem.ToString()))
            //{
            //    this.currentReport.Bands.RemoveAt(group.BandHeader.Index);
            //    this.currentReport.Bands.RemoveAt(group.BandFooder.Index);

            //    this.currentReport.BandsGroup.RemoveAt(group.Index);
            //}
            BandGroup bg = null ;
            foreach (BandGroup group in this.currentReport.BandsGroup)
            {
                if (group.FieldName == this.listGroups.SelectedItem.ToString())
                {

                    bg = group;
                   
                }

            }
            if (null != bg)
            {
                parent.RemoveBand(bg);
            }
            this.listGroups.Items.RemoveAt(this.listGroups.SelectedIndex);
            
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool HasGroup(string fieldname)
        {
            foreach (string str in this.listGroups.Items)
            {
                if (fieldname == str)
                {
                    return true;
                }
            }
            return false;
        }

        private bool HasReportGroup(string fieldname)
        {
            foreach (BandGroup group in this.currentReport.BandsGroup)
            {
                if (group.FieldName == fieldname)
                {
                    return true;
                }
            }
            return false;
        }

        private void Inicialize()
        {
            IReportData source = this.currentReport.DataSources.GetSource(this.currentReport.MasterDataSource);
            if (source != null)
            {
                this.fields = source.Fields;
                this.comboFields.Items.Clear();
                if (this.fields.Length > 0)
                {
                    foreach (ReportDataFieldInfo info in this.fields)
                    {
                        this.comboFields.Items.Add(info.FieldName);
                    }
                    this.comboFields.SelectedIndex = 0;
                }
                this.listGroups.Items.Clear();
                foreach (BandGroup group in this.currentReport.BandsGroup)
                {
                    this.listGroups.Items.Add(group.FieldName);
                }
            }
        }

        private void InitializeComponent()
        {
            this.comboFields = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.listGroups = new System.Windows.Forms.ListBox();
            this.btnAdicionar = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.BtnOk = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // comboFields
            // 
            this.comboFields.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboFields.FormattingEnabled = true;
            this.comboFields.Location = new System.Drawing.Point(67, 142);
            this.comboFields.Name = "comboFields";
            this.comboFields.Size = new System.Drawing.Size(212, 20);
            this.comboFields.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(2, 145);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "选择字段:";
            // 
            // listGroups
            // 
            this.listGroups.FormattingEnabled = true;
            this.listGroups.ItemHeight = 12;
            this.listGroups.Location = new System.Drawing.Point(4, 2);
            this.listGroups.MultiColumn = true;
            this.listGroups.Name = "listGroups";
            this.listGroups.Size = new System.Drawing.Size(275, 124);
            this.listGroups.TabIndex = 2;
            // 
            // btnAdicionar
            // 
            this.btnAdicionar.Location = new System.Drawing.Point(285, 31);
            this.btnAdicionar.Name = "btnAdicionar";
            this.btnAdicionar.Size = new System.Drawing.Size(83, 23);
            this.btnAdicionar.TabIndex = 3;
            this.btnAdicionar.Text = "增加";
            this.btnAdicionar.UseVisualStyleBackColor = true;
            this.btnAdicionar.Click += new System.EventHandler(this.btnAdicionar_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(285, 70);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(83, 23);
            this.button3.TabIndex = 6;
            this.button3.Text = "移除";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // BtnOk
            // 
            this.BtnOk.Location = new System.Drawing.Point(285, 142);
            this.BtnOk.Name = "BtnOk";
            this.BtnOk.Size = new System.Drawing.Size(83, 23);
            this.BtnOk.TabIndex = 7;
            this.BtnOk.Text = "确定";
            this.BtnOk.UseVisualStyleBackColor = true;
            this.BtnOk.Click += new System.EventHandler(this.BtnOk_Click);
            // 
            // FormGroup
            // 
            this.ClientSize = new System.Drawing.Size(380, 194);
            this.Controls.Add(this.BtnOk);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.btnAdicionar);
            this.Controls.Add(this.listGroups);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.comboFields);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormGroup";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "分组设置";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        public static DialogResult ShowDialog(Report report, DesignerReport parentform)
        {
            if (formgroup == null)
            {
                formgroup = new FormGroup();
            }
            formgroup.currentReport = report;
            formgroup.parent = parentform;
            formgroup.Inicialize();
            return formgroup.ShowDialog();
        }
    }
}

