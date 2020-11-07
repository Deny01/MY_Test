using System;
using System.Data;
using System.Windows.Forms;

using CrystalDecisions.Shared;
using CrystalDecisions.CrystalReports.Engine;


namespace HappyYF.YuanXin.WorkSet
{
    public partial class BaseReportView : Form
    {
        public string company;
        private DataSet printdata;

        public DataSet Printdata
        {
            get { return printdata; }
            set { printdata = value; }
        }

        private ReportClass printCrystalReport;

        public ReportClass PrintCrystalReport
        {
            get { return printCrystalReport; }
            set { printCrystalReport = value; }
        }

        public CrystalDecisions.Windows.Forms.CrystalReportViewer CrystalReportViewer1
        {
            get { return crystalReportViewer1; }
            //set { printCrystalReport = value; }
        }

        private string outfilename;

        public string Outfilename
        {
            get { return outfilename; }
            set { outfilename = value; }
        }

           

        public BaseReportView()
        {
            InitializeComponent();
        }

        private void BaseReportView_Load(object sender, EventArgs e)
        {

            //((HappyYF.YuanXin.WorkSet.LY_YingyeHetong_FH)(printCrystalReport)).Section1.ReportObjects["批准日期2"].ObjectFormat.EnableSuppress

            this.crystalReportViewer1.ReportSource = this.printCrystalReport;

            this.crystalReportViewer1.SelectionFormula = this.printCrystalReport.DataDefinition.RecordSelectionFormula;






            this.printCrystalReport.SetDataSource(this.printdata);

            if (true == this.checkBox1.Visible)
            {
                if ("中原" == this.company)
                {

                    
                    ((HappyYF.YuanXin.WorkSet.LY_YingyeHetong_FH)(printCrystalReport)).Section1.ReportObjects["批准日期2"].ObjectFormat.EnableSuppress = true;
                    ((HappyYF.YuanXin.WorkSet.LY_YingyeHetong_FH)(printCrystalReport)).Section1.ReportObjects["Text15"].ObjectFormat.EnableSuppress = false;
                }
                else if ("中成" == this.company)
                {
                    ((HappyYF.YuanXin.WorkSet.LY_YingyeHetong_FHzhongc)(printCrystalReport)).Section1.ReportObjects["批准日期2"].ObjectFormat.EnableSuppress = true;
                    ((HappyYF.YuanXin.WorkSet.LY_YingyeHetong_FHzhongc)(printCrystalReport)).Section1.ReportObjects["Text15"].ObjectFormat.EnableSuppress = false;
                }
                else if ("财务" == this.company)
                {

                    ((HappyYF.YuanXin.WorkSet.LY_YingyeHetong_FH_zcdisf)(printCrystalReport)).Section1.ReportObjects["批准日期1"].ObjectFormat.EnableSuppress = true;
                    ((HappyYF.YuanXin.WorkSet.LY_YingyeHetong_FH_zcdisf)(printCrystalReport)).Section1.ReportObjects["Text15"].ObjectFormat.EnableSuppress = false;
                }
                else if ("中原中成" == this.company)
                {
                    ((HappyYF.YuanXin.WorkSet.LY_YingyeHetong_FH_zcdisf)(printCrystalReport)).Section1.ReportObjects["批准日期2"].ObjectFormat.EnableSuppress = true;
                    ((HappyYF.YuanXin.WorkSet.LY_YingyeHetong_FH_zcdisf)(printCrystalReport)).Section1.ReportObjects["Text15"].ObjectFormat.EnableSuppress = false;
                }
                else if ("中原代发" == this.company)
                {
                    ((HappyYF.YuanXin.WorkSet.LY_YingyeHetong_FHzhongcnotax)(printCrystalReport)).Section1.ReportObjects["批准日期2"].ObjectFormat.EnableSuppress = true;
                    ((HappyYF.YuanXin.WorkSet.LY_YingyeHetong_FHzhongcnotax)(printCrystalReport)).Section1.ReportObjects["Text15"].ObjectFormat.EnableSuppress = false;
                }
                else if ("中成出口" == this.company)
                {
                    ((HappyYF.YuanXin.WorkSet.LY_YingyeHetong_FHout)(printCrystalReport)).Section1.ReportObjects["批准日期2"].ObjectFormat.EnableSuppress = true;
                    ((HappyYF.YuanXin.WorkSet.LY_YingyeHetong_FHout)(printCrystalReport)).Section1.ReportObjects["Text15"].ObjectFormat.EnableSuppress = false;
                }
                 else 
                {

                    ((HappyYF.YuanXin.WorkSet.LY_YingyeHetong_FH_zcdis)(printCrystalReport)).Section1.ReportObjects["批准日期2"].ObjectFormat.EnableSuppress = true;
                    ((HappyYF.YuanXin.WorkSet.LY_YingyeHetong_FH_zcdis)(printCrystalReport)).Section1.ReportObjects["Text15"].ObjectFormat.EnableSuppress = false;
                }
            }
            this.crystalReportViewer1.RefreshReport();
            //this.checkBox1.Checked = false;
            this.WindowState = FormWindowState.Maximized;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public void setchackBoxCansee(bool  cansee)
        {

            this.checkBox1.Visible = cansee;
        }
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (this.checkBox1.Checked)
            {
                if ("中原" == this.company)
                {

                    ((HappyYF.YuanXin.WorkSet.LY_YingyeHetong_FH)(printCrystalReport)).Section1.ReportObjects["批准日期2"].ObjectFormat.EnableSuppress = false;
                    ((HappyYF.YuanXin.WorkSet.LY_YingyeHetong_FH)(printCrystalReport)).Section1.ReportObjects["Text15"].ObjectFormat.EnableSuppress = true;
                }
                else if ("中成" == this.company)
                {
                    ((HappyYF.YuanXin.WorkSet.LY_YingyeHetong_FHzhongc)(printCrystalReport)).Section1.ReportObjects["批准日期2"].ObjectFormat.EnableSuppress = false;
                    ((HappyYF.YuanXin.WorkSet.LY_YingyeHetong_FHzhongc)(printCrystalReport)).Section1.ReportObjects["Text15"].ObjectFormat.EnableSuppress = true;
                }
                else if ("财务" == this.company)
                {

                    ((HappyYF.YuanXin.WorkSet.LY_YingyeHetong_FH_zcdisf)(printCrystalReport)).Section1.ReportObjects["批准日期1"].ObjectFormat.EnableSuppress =false ;
                    ((HappyYF.YuanXin.WorkSet.LY_YingyeHetong_FH_zcdisf)(printCrystalReport)).Section1.ReportObjects["Text15"].ObjectFormat.EnableSuppress = true;
                }
                else if ("中原中成" == this.company)
                {
                    ((HappyYF.YuanXin.WorkSet.LY_YingyeHetong_FH_zcdisf)(printCrystalReport)).Section1.ReportObjects["批准日期2"].ObjectFormat.EnableSuppress = true;
                    ((HappyYF.YuanXin.WorkSet.LY_YingyeHetong_FH_zcdisf)(printCrystalReport)).Section1.ReportObjects["Text15"].ObjectFormat.EnableSuppress = false;
                }
                else if ("中原代发" == this.company)
                {
                    ((HappyYF.YuanXin.WorkSet.LY_YingyeHetong_FHzhongcnotax)(printCrystalReport)).Section1.ReportObjects["批准日期2"].ObjectFormat.EnableSuppress = true;
                    ((HappyYF.YuanXin.WorkSet.LY_YingyeHetong_FHzhongcnotax)(printCrystalReport)).Section1.ReportObjects["Text15"].ObjectFormat.EnableSuppress = false;
                }
                else
                {

                    ((HappyYF.YuanXin.WorkSet.LY_YingyeHetong_FH_zcdis)(printCrystalReport)).Section1.ReportObjects["批准日期2"].ObjectFormat.EnableSuppress = false;
                    ((HappyYF.YuanXin.WorkSet.LY_YingyeHetong_FH_zcdis)(printCrystalReport)).Section1.ReportObjects["Text15"].ObjectFormat.EnableSuppress = true;
                }
                this.crystalReportViewer1.RefreshReport();
            }
            else
            {
                if ("中原" == this.company)
                {
                    ((HappyYF.YuanXin.WorkSet.LY_YingyeHetong_FH)(printCrystalReport)).Section1.ReportObjects["批准日期2"].ObjectFormat.EnableSuppress = true;
                    ((HappyYF.YuanXin.WorkSet.LY_YingyeHetong_FH)(printCrystalReport)).Section1.ReportObjects["Text15"].ObjectFormat.EnableSuppress = false; ;
                }
                else if ("中成" == this.company)
                {
                    ((HappyYF.YuanXin.WorkSet.LY_YingyeHetong_FHzhongc)(printCrystalReport)).Section1.ReportObjects["批准日期2"].ObjectFormat.EnableSuppress = true;
                    ((HappyYF.YuanXin.WorkSet.LY_YingyeHetong_FHzhongc)(printCrystalReport)).Section1.ReportObjects["Text15"].ObjectFormat.EnableSuppress = false; ;
                }
                else if ("财务" == this.company)
                {

                    ((HappyYF.YuanXin.WorkSet.LY_YingyeHetong_FH_zcdisf)(printCrystalReport)).Section1.ReportObjects["批准日期1"].ObjectFormat.EnableSuppress = true;
                    ((HappyYF.YuanXin.WorkSet.LY_YingyeHetong_FH_zcdisf)(printCrystalReport)).Section1.ReportObjects["Text15"].ObjectFormat.EnableSuppress = false;
                }
                else if ("中原中成" == this.company)
                {
                    ((HappyYF.YuanXin.WorkSet.LY_YingyeHetong_FH_zcdisf)(printCrystalReport)).Section1.ReportObjects["批准日期2"].ObjectFormat.EnableSuppress = true;
                    ((HappyYF.YuanXin.WorkSet.LY_YingyeHetong_FH_zcdisf)(printCrystalReport)).Section1.ReportObjects["Text15"].ObjectFormat.EnableSuppress = false;
                }
                else if ("中原代发" == this.company)
                {
                    ((HappyYF.YuanXin.WorkSet.LY_YingyeHetong_FHzhongcnotax)(printCrystalReport)).Section1.ReportObjects["批准日期2"].ObjectFormat.EnableSuppress = true;
                    ((HappyYF.YuanXin.WorkSet.LY_YingyeHetong_FHzhongcnotax)(printCrystalReport)).Section1.ReportObjects["Text15"].ObjectFormat.EnableSuppress = false;
                }
                else
                {
                    ((HappyYF.YuanXin.WorkSet.LY_YingyeHetong_FH_zcdis)(printCrystalReport)).Section1.ReportObjects["批准日期2"].ObjectFormat.EnableSuppress = true;
                    ((HappyYF.YuanXin.WorkSet.LY_YingyeHetong_FH_zcdis)(printCrystalReport)).Section1.ReportObjects["Text15"].ObjectFormat.EnableSuppress = false; ;
                
                }
                this.crystalReportViewer1.RefreshReport();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();

            sfd.Filter = "PDF 文件 (*.pdf)|*.pdf";
            sfd.FileName = this .outfilename;

           

            if (sfd.ShowDialog() == DialogResult.OK)
            {


                //this.crystalReportViewer1.ExportReport();

                DiskFileDestinationOptions diskOpts =
                ExportOptions.CreateDiskFileDestinationOptions();

                ExportOptions exportOpts = new ExportOptions();
                exportOpts.ExportFormatType =
                   ExportFormatType.PortableDocFormat;
                exportOpts.ExportDestinationType =
                   ExportDestinationType.DiskFile;

                diskOpts.DiskFileName = sfd.FileName;
                exportOpts.ExportDestinationOptions = diskOpts;

                printCrystalReport.Export(exportOpts);

                MessageBox.Show("PDF导出完成", "注意");
            }

          

        }

        private void crystalReportViewer1_Load(object sender, EventArgs e)
        {

        }
    }
}
