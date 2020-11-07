using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TGZJ.DayReport
{
    public partial class DayWorkReport : Form
    {
        public DayWorkReport()
        {
            InitializeComponent();
            this.crystalReportViewer1.RefreshReport();
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {


            FillDayWorkReport(this.dateTimePicker1.Value.Date);
        }

        private void FillDayWorkReport( DateTime nowDay)
        {
            ((CrystalDecisions.CrystalReports.Engine.TextObject)(this.DayReportCrystalReport1.Section1.ReportObjects["Text2"])).Text = this.dateTimePicker1.Value.Date.ToString("yyyy-MM-dd");
            
            //Yltr
            this.yltrTableAdapter.Fill(this.dayWorkReport1.yltr, nowDay);
            this.DayReportCrystalReport1.Subreports["YltrCrystalReport.rpt"].SetDataSource(this.dayWorkReport1.Tables["yltr"]);

            // Lllhsl
            this.lllhslTableAdapter.Fill(this.dayWorkReport1.lllhsl, nowDay);
            this.DayReportCrystalReport1.Subreports["LllhslCrystalReport.rpt"].SetDataSource(this.dayWorkReport1.Tables["lllhsl"]);
            //Zyclxh
            this.zyclxhTableAdapter.Fill(this.dayWorkReport1.zyclxh,nowDay );
            this.DayReportCrystalReport1.Subreports["ZyclxhCrystalReport.rpt"].SetDataSource(this.dayWorkReport1.Tables["zyclxh"]);
            
            //
            //Sbkcsj
            this.sbkcsjTableAdapter.Fill(this.dayWorkReport1.sbkcsj, nowDay);
            this.DayReportCrystalReport1.Subreports["SbkcsjCrystalReport.rpt"].SetDataSource(this.dayWorkReport1.Tables["sbkcsj"]);

            //Jkl
            this.jklTableAdapter.Fill(this.dayWorkReport1.jkl, nowDay);
            this.DayReportCrystalReport1.Subreports["JklCrystalReport.rpt"].SetDataSource(this.dayWorkReport1.Tables["jkl"]);

            //Bz
            
            this.bzTableAdapter.Fill(this.dayWorkReport1.bz, nowDay);
            this.DayReportCrystalReport1.Subreports["BzCrystalReport.rpt"].SetDataSource(this.dayWorkReport1.Tables["bz"]);
             // Zk
            this.zkTableAdapter.Fill(this.dayWorkReport1.zk, nowDay);
            this.DayReportCrystalReport1.SetDataSource(this.dayWorkReport1.Tables["zk"]);
            
            
            this.crystalReportViewer1.RefreshReport();
        }

        private void DayWorkReport_Load(object sender, EventArgs e)
        {
            
            

        }

      

      

       

      

       

       
      
    }
}
