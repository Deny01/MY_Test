using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.IO;
using System.Windows.Forms;
using System.Collections;


namespace KReport
{
 public class HappyReport
	{
        public void Show( DataTable  rep_dt,string dt_name)
     {
         KReport.Engine.Report report;

         report = new KReport.Engine.Report();
         if (!Directory.Exists(Directory.GetCurrentDirectory() + "\\ReportSet"))
         {
             Directory.CreateDirectory(Directory.GetCurrentDirectory() + "\\ReportSet");
         }


         // report.FileName = Directory.GetCurrentDirectory() + "\\ReportSet\\" + dt_name;


         if (File.Exists(Directory.GetCurrentDirectory() + "\\ReportSet\\" + dt_name))
         {
             report.FileName = Directory.GetCurrentDirectory() + "\\ReportSet\\" + dt_name;
             report.Load();
         }
            else 
         {
             MessageBox.Show("报表不存在,请首先设计报表...","注意");
             return;
             //report.FileName = Directory.GetCurrentDirectory() + "\\ReportSet\\" + "一方报表模板";
             //report.Load();
         }
         report.AddSource(rep_dt, dt_name);

         report.Show();
     }

        public void Show(ArrayList rep_dt, string dt_name)
        {
            KReport.Engine.Report report;

            report = new KReport.Engine.Report();
            if (!Directory.Exists(Directory.GetCurrentDirectory() + "\\ReportSet"))
            {
                Directory.CreateDirectory(Directory.GetCurrentDirectory() + "\\ReportSet");
            }


            // report.FileName = Directory.GetCurrentDirectory() + "\\ReportSet\\" + dt_name;


            if (File.Exists(Directory.GetCurrentDirectory() + "\\ReportSet\\" + dt_name))
            {
                report.FileName = Directory.GetCurrentDirectory() + "\\ReportSet\\" + dt_name;
                report.Load();
            }
            else
            {
                MessageBox.Show("报表不存在,请首先设计报表...", "注意");
                return;
                //report.FileName = Directory.GetCurrentDirectory() + "\\ReportSet\\" + "一方报表模板";
                //report.Load();
            }
            report.AddSource(rep_dt, dt_name);

            report.Show();
        }
        
    
     
        public void ShowDesigner(DataTable rep_dt, string dt_name,bool haveDesignright)
        {
            KReport.Engine.Report report;

            report = new KReport.Engine.Report();
            if (!Directory.Exists(Directory.GetCurrentDirectory() + "\\ReportSet"))
            {
                Directory.CreateDirectory(Directory.GetCurrentDirectory() + "\\ReportSet");
            }


            // report.FileName = Directory.GetCurrentDirectory() + "\\ReportSet\\" + dt_name;


            if (File.Exists(Directory.GetCurrentDirectory() + "\\ReportSet\\" + dt_name))
            {
                report.FileName = Directory.GetCurrentDirectory() + "\\ReportSet\\" + dt_name;
                report.Load();
            }
            else
            {
                File.Copy(Directory.GetCurrentDirectory() + "\\ReportSet\\" + "一方报表模板", Directory.GetCurrentDirectory() + "\\ReportSet\\" + dt_name);
                report.FileName = Directory.GetCurrentDirectory() + "\\ReportSet\\" + dt_name;
                report.Load();
            }
            report.AddSource(rep_dt, dt_name);

            report.ShowDesigner(haveDesignright);
        }

        public void ShowDesigner(ArrayList rep_dt, string dt_name, bool haveDesignright)
        {
            KReport.Engine.Report report;

            report = new KReport.Engine.Report();
            if (!Directory.Exists(Directory.GetCurrentDirectory() + "\\ReportSet"))
            {
                Directory.CreateDirectory(Directory.GetCurrentDirectory() + "\\ReportSet");
            }


            // report.FileName = Directory.GetCurrentDirectory() + "\\ReportSet\\" + dt_name;


            if (File.Exists(Directory.GetCurrentDirectory() + "\\ReportSet\\" + dt_name))
            {
                report.FileName = Directory.GetCurrentDirectory() + "\\ReportSet\\" + dt_name;
                report.Load();
            }
            else
            {
                File.Copy(Directory.GetCurrentDirectory() + "\\ReportSet\\" + "一方报表模板", Directory.GetCurrentDirectory() + "\\ReportSet\\" + dt_name);
                report.FileName = Directory.GetCurrentDirectory() + "\\ReportSet\\" + dt_name;
                report.Load();
            }
            report.AddSource(rep_dt, dt_name);

            report.ShowDesigner(true );
        }
	}

 public static class HappyReport_Print
 {
     public static void ExportHappyReport(System.Windows.Forms.DataGridView gridView, DataTable nowdata_table,System.Windows.Forms.BindingSource nowbs,string nowtablename)
     {
         if (null == gridView.CurrentRow) return;

         HappyReport hr = new HappyReport();

         //string nowCardNumber = this.xD_Sel_SellBalanceDataGridView.CurrentRow.Cells["编号"].Value.ToString();

         DataTable print_table = nowdata_table.Clone();



         foreach (DataRowView drv in ((DataView)nowbs.List))
         {
             print_table.ImportRow(drv.Row);
         }

         //hr.Show(this.dH_MonthReport_Data.DH_MonthReportData_in, "鼎和月份合同处理统计表");
         hr.Show(print_table, nowtablename);
     }

     public static void DesignHappyReport(System.Windows.Forms.DataGridView gridView, DataTable nowdata_table,System.Windows.Forms.BindingSource nowbs,string nowtablename)
     {
         if (null == gridView.CurrentRow) return;
         HappyReport hr = new HappyReport();

         DataTable print_table = nowdata_table.Clone();



         foreach (DataRowView drv in ((DataView)nowbs.List))
         {
             print_table.ImportRow(drv.Row);
         }

         //hr.ShowDesigner(this.dH_MonthReport_Data.DH_MonthReportData_in, "鼎和月份合同处理统计表");
         hr.ShowDesigner(print_table, nowtablename,true );
     }
 
 }
}
