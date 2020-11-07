namespace TGZJ.DayReport
{
    partial class DayWorkReport
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
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.dayWorkReport1 = new TGZJ.DATA.DayWorkReport();
            this.yltrTableAdapter = new TGZJ.DATA.DayWorkReportTableAdapters.yltrTableAdapter();
            this.lllhslTableAdapter = new TGZJ.DATA.DayWorkReportTableAdapters.lllhslTableAdapter();
            this.tableAdapterManager = new TGZJ.DATA.DayWorkReportTableAdapters.TableAdapterManager();
            this.zyclxhTableAdapter = new TGZJ.DATA.DayWorkReportTableAdapters.zyclxhTableAdapter();
            this.sbkcsjTableAdapter = new TGZJ.DATA.DayWorkReportTableAdapters.sbkcsjTableAdapter();
            this.jklTableAdapter = new TGZJ.DATA.DayWorkReportTableAdapters.jklTableAdapter();
            this.bzTableAdapter = new TGZJ.DATA.DayWorkReportTableAdapters.bzTableAdapter();
            this.crystalReportViewer1 = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            this.DayReportCrystalReport1 = new TGZJ.DayReport.DayReportCrystalReport();
            this.zkTableAdapter = new TGZJ.DATA.DayWorkReportTableAdapters.zkTableAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.dayWorkReport1)).BeginInit();
            this.SuspendLayout();
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.CustomFormat = "yyyy-MM-dd";
            this.dateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePicker1.Location = new System.Drawing.Point(364, 0);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(92, 21);
            this.dateTimePicker1.TabIndex = 1;
            this.dateTimePicker1.ValueChanged += new System.EventHandler(this.dateTimePicker1_ValueChanged);
            // 
            // dayWorkReport1
            // 
            this.dayWorkReport1.DataSetName = "DayWorkReport";
            this.dayWorkReport1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // yltrTableAdapter
            // 
            this.yltrTableAdapter.ClearBeforeFill = true;
            // 
            // lllhslTableAdapter
            // 
            this.lllhslTableAdapter.ClearBeforeFill = true;
            // 
            // tableAdapterManager
            // 
            this.tableAdapterManager.BackupDataSetBeforeUpdate = false;
            this.tableAdapterManager.Connection = null;
            this.tableAdapterManager.UpdateOrder = TGZJ.DATA.DayWorkReportTableAdapters.TableAdapterManager.UpdateOrderOption.InsertUpdateDelete;
            // 
            // zyclxhTableAdapter
            // 
            this.zyclxhTableAdapter.ClearBeforeFill = true;
            // 
            // sbkcsjTableAdapter
            // 
            this.sbkcsjTableAdapter.ClearBeforeFill = true;
            // 
            // jklTableAdapter
            // 
            this.jklTableAdapter.ClearBeforeFill = true;
            // 
            // bzTableAdapter
            // 
            this.bzTableAdapter.ClearBeforeFill = true;
            // 
            // crystalReportViewer1
            // 
            this.crystalReportViewer1.ActiveViewIndex = 0;
            this.crystalReportViewer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.crystalReportViewer1.DisplayGroupTree = false;
            this.crystalReportViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.crystalReportViewer1.Location = new System.Drawing.Point(0, 0);
            this.crystalReportViewer1.Name = "crystalReportViewer1";
            this.crystalReportViewer1.ReportSource = this.DayReportCrystalReport1;
            this.crystalReportViewer1.Size = new System.Drawing.Size(826, 510);
            this.crystalReportViewer1.TabIndex = 0;
            // 
            // zkTableAdapter
            // 
            this.zkTableAdapter.ClearBeforeFill = true;
            // 
            // DayWorkReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(826, 510);
            this.Controls.Add(this.dateTimePicker1);
            this.Controls.Add(this.crystalReportViewer1);
            this.Name = "DayWorkReport";
            this.Text = "生产日报";
            this.Load += new System.EventHandler(this.DayWorkReport_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dayWorkReport1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private CrystalDecisions.Windows.Forms.CrystalReportViewer crystalReportViewer1;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private DayReportCrystalReport DayReportCrystalReport1;
        private TGZJ.DATA.DayWorkReport dayWorkReport1;
        private TGZJ.DATA.DayWorkReportTableAdapters.yltrTableAdapter yltrTableAdapter;
        private TGZJ.DATA.DayWorkReportTableAdapters.lllhslTableAdapter lllhslTableAdapter;
        private TGZJ.DATA.DayWorkReportTableAdapters.TableAdapterManager tableAdapterManager;
        private TGZJ.DATA.DayWorkReportTableAdapters.zyclxhTableAdapter zyclxhTableAdapter;
        private TGZJ.DATA.DayWorkReportTableAdapters.sbkcsjTableAdapter sbkcsjTableAdapter;
        private TGZJ.DATA.DayWorkReportTableAdapters.jklTableAdapter jklTableAdapter;
        private TGZJ.DATA.DayWorkReportTableAdapters.bzTableAdapter bzTableAdapter;
        private TGZJ.DATA.DayWorkReportTableAdapters.zkTableAdapter zkTableAdapter;
    }
}