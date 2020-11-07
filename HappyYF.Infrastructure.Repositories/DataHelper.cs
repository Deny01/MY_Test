using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HappyYF.Infrastructure.DomainBase;
using System.Data;
using System.Diagnostics;


using System.Windows.Forms;


using System.IO;
//using Microsoft.Office.Interop.Excel;


namespace HappyYF.Infrastructure.Repositories
{
    /// <summary>
    /// Static helper class used by the factories when getting 
    /// data from ADO.NET objects (i.e. IDataReader)
    /// </summary>
    public static class DataHelper
    {
        #region Static Data Helper Methods

        public static DateTime GetDateTime(object value)
        {
            DateTime dateValue = DateTime.MinValue;
            if ((value != null) && (value != DBNull.Value))
            {
                dateValue = (DateTime)value;
            }
            return dateValue;
        }
        public static String ToSBC(String input)
        {
            // 半角转全角：
            char[] c = input.ToCharArray();
            for (int i = 0; i < c.Length; i++)
            {
                if (c[i] == 32)
                {
                    c[i] = (char)12288;
                    continue;
                }
                if (c[i] < 127)
                    c[i] = (char)(c[i] + 65248);
            }
            return new String(c);
        }

        public static String ToDBC(String input)
        {
            char[] c = input.ToCharArray();
            for (int i = 0; i < c.Length; i++)
            {
                if (c[i] == 12288)
                {
                    c[i] = (char)32;
                    continue;
                }
                if (c[i] > 65280 && c[i] < 65375)
                    c[i] = (char)(c[i] - 65248);
            }
            return new String(c);
        }
        public static DateTime? GetNullableDateTime(object value)
        {
            DateTime? dateTimeValue = null;
            DateTime dbDateTimeValue;
            if (value != null && !Convert.IsDBNull(value))
            {
                if (DateTime.TryParse(value.ToString(), out dbDateTimeValue))
                {
                    dateTimeValue = dbDateTimeValue;
                }
            }
            return dateTimeValue;
        }

        public static int GetInteger(object value)
        {
            int integerValue = 0;
            if (value != null && !Convert.IsDBNull(value))
            {
                int.TryParse(value.ToString(), out integerValue);
            }
            return integerValue;
        }

        public static int? GetNullableInteger(object value)
        {
            int? integerValue = null;
            int parseIntegerValue = 0;
            if (value != null && !Convert.IsDBNull(value))
            {
                if (int.TryParse(value.ToString(), out parseIntegerValue))
                {
                    integerValue = parseIntegerValue;
                }
            }
            return integerValue;
        }

        public static decimal GetDecimal(object value)
        {
            decimal decimalValue = 0;
            if (value != null && !Convert.IsDBNull(value))
            {
                decimal.TryParse(value.ToString(), out decimalValue);
            }
            return decimalValue;
        }

        public static decimal? GetNullableDecimal(object value)
        {
            decimal? decimalValue = null;
            decimal parseDecimalValue = 0;
            if (value != null && !Convert.IsDBNull(value))
            {
                if (decimal.TryParse(value.ToString(), out parseDecimalValue))
                {
                    decimalValue = parseDecimalValue;
                }
            }
            return decimalValue;
        }

        public static double GetDouble(object value)
        {
            double doubleValue = 0;
            if (value != null && !Convert.IsDBNull(value))
            {
                double.TryParse(value.ToString(), out doubleValue);
            }
            return doubleValue;
        }

        public static double? GetNullableDouble(object value)
        {
            double? doubleValue = null;
            double parseDoubleValue = 0;
            if (value != null && !Convert.IsDBNull(value))
            {
                if (double.TryParse(value.ToString(), out parseDoubleValue))
                {
                    doubleValue = parseDoubleValue;
                }
            }

            return doubleValue;
        }

        public static Guid GetGuid(object value)
        {
            Guid guidValue = Guid.Empty;
            if (value != null && !Convert.IsDBNull(value))
            {
                try
                {
                    guidValue = new Guid(value.ToString());
                }
                catch (FormatException ex)
                {
                    // really do nothing, because we want to return a value for the guid = Guid.Empty;
                }
            }
            return guidValue;
        }

        public static Guid? GetNullableGuid(object value)
        {
            Guid? guidValue = null;
            if (value != null && !Convert.IsDBNull(value))
            {
                try
                {
                    guidValue = new Guid(value.ToString());
                }
                catch (FormatException ex)
                {
                    // really do nothing, because we want to return a value for the guid = null;
                }
            }
            return guidValue;
        }

        public static string GetString(object value)
        {
            string stringValue = string.Empty;
            if (value != null && !Convert.IsDBNull(value))
            {
                stringValue = value.ToString().Trim();
            }
            return stringValue;
        }

        public static bool GetBoolean(object value)
        {
            bool bReturn = false;
            if (value != null && value != DBNull.Value)
            {
                bReturn = Convert.ToBoolean(value);
            }
            return bReturn;
        }

        public static bool? GetNullableBoolean(object value)
        {
            bool? bReturn = null;
            if (value != null && value != DBNull.Value)
            {
                bReturn = (bool)value;
            }

            return bReturn;
        }

        public static T GetEnumValue<T>(string databaseValue) where T : struct
        {
            T enumValue = default(T);

            object parsedValue = Enum.Parse(typeof(T), databaseValue);
            if (parsedValue != null)
            {
                enumValue = (T)parsedValue;
            }

            return enumValue;
        }

        public static string EntityListToDelimited<T>(IList<T> entities) where T : EntityBase
        {
            StringBuilder builder = new StringBuilder(20);
            if (entities != null)
            {
                for (int i = 0; i < entities.Count; i++)
                {
                    if (i > 0)
                    {
                        builder.Append(",");
                    }
                    builder.Append(entities[i].Key.ToString());
                }
            }
            return builder.ToString();
        }

        public static bool ReaderContainsColumnName(DataTable schemaTable, string columnName)
        {
            bool containsColumnName = false;
            foreach (DataRow row in schemaTable.Rows)
            {
                if (row["ColumnName"].ToString() == columnName)
                {
                    containsColumnName = true;
                    break;
                }
            }
            return containsColumnName;
        }

        public static object GetSqlValue(object value)
        {
            if (value != null)
            {
                if (value is Guid)
                {
                    return GetSqlValue((Guid)value);
                }
                else
                {
                    return value;
                }
            }
            else
            {
                return "NULL";
            }
        }

        public static object GetSqlValue(string value)
        {
            if (value != null)
            {
                return string.Format("N'{0}'", value);
            }
            else
            {
                return "NULL";
            }
        }

        public static object GetSqlValue(DateTime value)
        {
            if (value != null)
            {
                return string.Format("N'{0}'", value.ToString());
            }
            else
            {
                return "NULL";
            }
        }

        public static object GetSqlValue(Guid value)
        {
            if (value != null)
            {
                return string.Format("N'{0}'", value.ToString());
            }
            else
            {
                return "NULL";
            }
        }

        #endregion
    }
    public static class DatagridviewToDatatable
    {
        public static DataTable GetTableFromView(System.Windows.Forms.DataGridView dv, DataTable st)
        {
            DataTable dt = new DataTable();
            dt = st.Clone();


            //DataColumn dc;
            //for (int i = 0; i < dv.Columns.Count; i++)
            //{
            //    dc = new DataColumn();
            //    dc.ColumnName = dv.Columns[i].HeaderText.ToString();
            //    dt.Columns.Add(dc);
            //}
            for (int j = 0; j < dv.Rows.Count; j++)
            {
                DataRow dr = dt.NewRow();
                for (int x = 0; x < dv.Columns.Count; x++)
                {
                    string cname = dv.Columns[x].DataPropertyName;

                    if (!string.IsNullOrEmpty(cname))
                    {
                        if ("电话" == cname || "驾驶员电话" == cname)
                        {
                            if (dv.Rows[j].Cells[x].Value.ToString().Length > 12)
                                dr[cname] = dv.Rows[j].Cells[x].Value.ToString().Substring(0, 12);
                            else
                                dr[cname] = dv.Rows[j].Cells[x].Value.ToString();
                        }
                        else
                        {
                            dr[cname] = dv.Rows[j].Cells[x].Value;
                        }
                    }

                }
                dt.Rows.Add(dr);
            }
            return dt;


        }


    }
    public static class GetDataGridviewMultiFilterString
    {
        public static string DGVMultiFilterString(System.Windows.Forms.DataGridView gridView, string filterText)
        {
            string dFilter = "";



            for (int i = 0; i < gridView.ColumnCount; i++)
            {
                if (null != gridView.Columns[i].ValueType && gridView.Columns[i].ValueType.FullName == "System.String")
                {
                    dFilter = dFilter + gridView.Columns[i].DataPropertyName + " like  '*" + filterText + "*' or ";
                }

            }
            dFilter = dFilter.Substring(0, dFilter.Length - 3);
            return dFilter;


        }
    }

     ///////////////////////////

    public static class Netfunction
    {
        public static bool Ping(string remoteHost)
        {
            bool Flag = false;
            Process proc = new Process();
            try
            {
                proc.StartInfo.FileName = "cmd.exe";
                proc.StartInfo.UseShellExecute = false;
                proc.StartInfo.RedirectStandardInput = true;
                proc.StartInfo.RedirectStandardOutput = true;
                proc.StartInfo.RedirectStandardError = true;
                proc.StartInfo.CreateNoWindow = true;
                proc.Start();
                string dosLine = @"ping -n 1 " + remoteHost;
                proc.StandardInput.WriteLine(dosLine);
                proc.StandardInput.WriteLine("exit");
                while (proc.HasExited == false)
                {
                    proc.WaitForExit(500);
                }
                string pingResult = proc.StandardOutput.ReadToEnd();
                if (pingResult.IndexOf("(0% loss)") != -1 || pingResult.IndexOf("(0% 丢失)") != -1)
                {
                    Flag = true;
                }
                proc.StandardOutput.Close();
            }
            catch (Exception ex)
            {
            }
            finally
            {
                try
                {
                    proc.Close();
                    proc.Dispose();
                }
                catch
                {
                }
            }
            return Flag;
        }

        public static bool Connect(string remoteHost, string userName, string passWord)
        {
            //if (!Ping(remoteHost))
            //{
            //    return false;
            //}
            bool Flag = true;
            Process proc = new Process();
            try
            {
                proc.StartInfo.FileName = "cmd.exe";
                proc.StartInfo.UseShellExecute = false;
                proc.StartInfo.RedirectStandardInput = true;
                proc.StartInfo.RedirectStandardOutput = true;
                proc.StartInfo.RedirectStandardError = true;
                proc.StartInfo.CreateNoWindow = true;
                proc.Start();
                string dosLine = @"net use \\" + remoteHost + " " + passWord + " " + " /user:" + userName + ">NUL";
                //string dosLine = @"net use " + path + " /User:" + userName + " " + passWord + " /PERSISTENT:YES";


                //WebClient client = new WebClient();
                //NetworkCredential cred = new NetworkCredential("username", "password", "172.16.0.222");
                //client.Credentials = cred;
                //client.DownloadFile("file://172.16.0.222/test/111.txt", "111.txt");


                proc.StandardInput.WriteLine(dosLine);
                proc.StandardInput.WriteLine("exit");
                while (proc.HasExited == false)
                {
                    proc.WaitForExit(10000);
                }
                string errormsg = proc.StandardError.ReadToEnd();
                if (errormsg != "")
                {
                    Flag = false;
                }
                proc.StandardError.Close();
            }
            catch (Exception ex)
            {
                Flag = false;
            }
            finally
            {
                try
                {
                    proc.Close();
                    proc.Dispose();
                }
                catch
                {
                }
            }
            return Flag;
        }

        public static void DisConnect(string remoteHost, string userName, string passWord)
        {
            //if (!Ping(remoteHost))
            //{
            //    return false;
            //}
            bool Flag = true;
            Process proc = new Process();
            try
            {
                proc.StartInfo.FileName = "cmd.exe";
                proc.StartInfo.UseShellExecute = false;
                proc.StartInfo.RedirectStandardInput = true;
                proc.StartInfo.RedirectStandardOutput = true;
                proc.StartInfo.RedirectStandardError = true;
                proc.StartInfo.CreateNoWindow = true;
                proc.Start();
                //string dosLine = @"net use \\" + remoteHost + " " + " /delete ";
                string dosLine = @"net use * /delete /y ";
                //string dosLine = @"net use " + path + " /User:" + userName + " " + passWord + " /PERSISTENT:YES";


                //WebClient client = new WebClient();
                //NetworkCredential cred = new NetworkCredential("username", "password", "172.16.0.222");
                //client.Credentials = cred;
                //client.DownloadFile("file://172.16.0.222/test/111.txt", "111.txt");


                proc.StandardInput.WriteLine(dosLine);
                proc.StandardInput.WriteLine("exit");
                while (proc.HasExited == false)
                {
                    proc.WaitForExit(1000);
                }
                string errormsg = proc.StandardError.ReadToEnd();
                if (errormsg != "")
                {
                    Flag = false;
                }
                proc.StandardError.Close();
            }
            catch (Exception ex)
            {
                Flag = false;
            }
            finally
            {
                try
                {
                    proc.Close();
                    proc.Dispose();
                }
                catch
                {
                }
            }
            //return Flag;
        }
    }

     //////////////////////////////////////
  
   
    public static class ExportDataGridviewTOExcell
    {
        public static bool ExportDataGridview(System.Windows.Forms.DataGridView gridView, bool isShowExcle)
        {
            if (gridView.Rows.Count == 0)
                return false;
            //建立Excel对象
            Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();

            excel.Application.Workbooks.Add(true);
            excel.Visible = isShowExcle;
            //生成字段名称...
            int realcolumnCount = 0;

            for (int i = 0; i < gridView.ColumnCount; i++)
            {
                if (gridView.Columns[i].Visible)
                {

                    //excel.Cells[1, i + 1] = gridView.Columns[i].HeaderText;

                    realcolumnCount = realcolumnCount + 1;
                    excel.Cells[1, realcolumnCount] = gridView.Columns[i].HeaderText;

                }
            }
            //填充数据


            for (int i = 0; i <= gridView.RowCount - 1; i++)
            {
                realcolumnCount = 0;
                for (int j = 0; j < gridView.ColumnCount; j++)
                {

                    if (gridView.Columns[j].Visible)
                    {
                        realcolumnCount = realcolumnCount + 1;
                        if (gridView[j, i].ValueType == typeof(string))
                        {
                            excel.Cells[i + 2, realcolumnCount] = "'" + gridView[j, i].Value.ToString(); 
                        }
                        else if (gridView[j, i].ValueType == typeof(DateTime ))
                        {
                            if (gridView[j, i].Value.ToString().Length >= 10)
                            {
                                excel.Cells[i + 2, realcolumnCount] = "'" + ((DateTime)gridView[j, i].Value).ToString("yyyy-MM-dd").Substring(0, 10);
                                //excel.Cells[i + 2, realcolumnCount] = ((DateTime)gridView[j, i].Value).ToString("U").Substring(0, 10);
                            }
                        }
                        else if (gridView[j, i].ValueType == typeof(decimal))
                        {
                           
                                //excel.Cells[i + 2, realcolumnCount] =((decimal)gridView[j, i].Value).ToString("#,#.00#" );
                           //if ( gridView[j, i].Value != DBNull.Value )
                            if (null != gridView[j, i].Value && gridView[j, i].Value != DBNull.Value)
                                //excel.Cells[i + 2, realcolumnCount] = ((decimal)gridView[j, i].Value).ToString("#,000.00");
                            excel.Cells[i + 2, realcolumnCount] = ((decimal)gridView[j, i].Value).ToString();


                        }
                        else if (gridView[j, i].ValueType == typeof(bool))
                        {

                            //excel.Cells[i + 2, realcolumnCount] =((decimal)gridView[j, i].Value).ToString("#,#.00#" );
                            //if ( gridView[j, i].Value != DBNull.Value )
                            if ("True" == gridView[j, i].Value.ToString())
                            {

                                excel.Cells[i + 2, realcolumnCount] = "YES";
                            }
                            else
                            {
                                excel.Cells[i + 2, realcolumnCount] = "NO";

                            }


                        }
                        else
                        {
                            if (null != gridView[j, i].Value)
                                excel.Cells[i + 2, realcolumnCount] = gridView[j, i].Value.ToString();
                        }
                    }
                }
            }
            return true;
        }

    

    }

    //public static class ExportTreeviewTOExcell
    //{
    //    public static bool ExportTreeviewTOExcell(System.Windows.Forms.TreeNodeCollection TC , bool isShowExcle)
    //    {
    //        if (gridView.Rows.Count == 0)
    //            return false;

    //        if (TC.Count < 1)
    //        {
    //            return false;
    //        }

    //        //建立Excel对象
    //        Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();

    //        excel.Application.Workbooks.Add(true);
    //        excel.Visible = isShowExcle;
    //        //生成字段名称...
    //        int realcolumnCount = 0;

    //        for (int i = 0; i < gridView.ColumnCount; i++)
    //        {
    //            if (gridView.Columns[i].Visible)
    //            {

    //                //excel.Cells[1, i + 1] = gridView.Columns[i].HeaderText;

    //                realcolumnCount = realcolumnCount + 1;
    //                excel.Cells[1, realcolumnCount] = gridView.Columns[i].HeaderText;

    //            }
    //        }
    //        //填充数据


    //        for (int i = 0; i <= gridView.RowCount - 1; i++)
    //        {
    //            realcolumnCount = 0;
    //            for (int j = 0; j < gridView.ColumnCount; j++)
    //            {

    //                if (gridView.Columns[j].Visible)
    //                {
    //                    realcolumnCount = realcolumnCount + 1;
    //                    if (gridView[j, i].ValueType == typeof(string))
    //                    {
    //                        excel.Cells[i + 2, realcolumnCount] = "'" + gridView[j, i].Value.ToString();
    //                    }
    //                    else if (gridView[j, i].ValueType == typeof(DateTime))
    //                    {
    //                        if (gridView[j, i].Value.ToString().Length >= 10)
    //                        {
    //                            excel.Cells[i + 2, realcolumnCount] = "'" + ((DateTime)gridView[j, i].Value).ToString("yyyy-MM-dd").Substring(0, 10);
    //                            //excel.Cells[i + 2, realcolumnCount] = ((DateTime)gridView[j, i].Value).ToString("U").Substring(0, 10);
    //                        }
    //                    }
    //                    else if (gridView[j, i].ValueType == typeof(decimal))
    //                    {

    //                        //excel.Cells[i + 2, realcolumnCount] =((decimal)gridView[j, i].Value).ToString("#,#.00#" );
    //                        //if ( gridView[j, i].Value != DBNull.Value )
    //                        if (null != gridView[j, i].Value && gridView[j, i].Value != DBNull.Value)
    //                            excel.Cells[i + 2, realcolumnCount] = ((decimal)gridView[j, i].Value).ToString("#,000.00");


    //                    }
    //                    else
    //                    {
    //                        if (null != gridView[j, i].Value)
    //                            excel.Cells[i + 2, realcolumnCount] = gridView[j, i].Value.ToString();
    //                    }
    //                }
    //            }
    //        }
    //        return true;
    //    }



    //}

    public static class ExportDataGridviewTOExcellA
    {
        public static bool ExportDataGridview(System.Windows.Forms.DataGridView gridView, bool isShowExcle)
        {
            if (gridView.Rows.Count == 0)
                return false;
            //建立Excel对象
            Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();

            excel.Application.Workbooks.Add(true);
            excel.Visible = isShowExcle;
            //生成字段名称...
            int realcolumnCount = 0;

            for (int i = 0; i < gridView.ColumnCount; i++)
            {
                if (gridView.Columns[i].Visible)
                {

                    //excel.Cells[1, i + 1] = gridView.Columns[i].HeaderText;

                    realcolumnCount = realcolumnCount + 1;
                    excel.Cells[1, realcolumnCount] = gridView.Columns[i].HeaderText;

                }
            }
            //填充数据


            for (int i = 0; i <= gridView.RowCount - 1; i++)
            {
                realcolumnCount = 0;
                for (int j = 0; j < gridView.ColumnCount; j++)
                {

                    if (gridView.Columns[j].Visible)
                    {
                        realcolumnCount = realcolumnCount + 1;
                        if (gridView[j, i].ValueType == typeof(string))
                        {
                            excel.Cells[i + 2, realcolumnCount] = "'" + gridView[j, i].Value.ToString();
                        }
                        else if (gridView[j, i].ValueType == typeof(DateTime))
                        {
                            if (gridView[j, i].Value.ToString().Length >= 10)
                            {
                                excel.Cells[i + 2, realcolumnCount] = "'" + ((DateTime)gridView[j, i].Value).ToString("yyyy-MM-dd").Substring(0, 10);
                                //excel.Cells[i + 2, realcolumnCount] = ((DateTime)gridView[j, i].Value).ToString("U").Substring(0, 10);
                            }
                        }
                        else if (gridView[j, i].ValueType == typeof(decimal))
                        {

                            //excel.Cells[i + 2, realcolumnCount] =((decimal)gridView[j, i].Value).ToString("#,#.00#" );
                            //if ( gridView[j, i].Value != DBNull.Value )
                            if (null != gridView[j, i].Value && gridView[j, i].Value != DBNull.Value)
                                excel.Cells[i + 2, realcolumnCount] = ((decimal)gridView[j, i].Value).ToString("#,000.00000");


                        }
                        else
                        {
                            if (null != gridView[j, i].Value)
                                excel.Cells[i + 2, realcolumnCount] = gridView[j, i].Value.ToString();
                        }
                    }
                }
            }
            return true;
        }

        //public static int ExportExcel(string strCaption, System.Windows.Forms.DataGridView myDGV, SaveFileDialog saveFileDialog)
        //public static int ExportExcel(string strCaption, System.Windows.Forms.DataGridView myDGV,string savepath)
        //{
        //    int result = 9999;

        //    //保存

        //    //System.Windows.Forms.FolderBrowserDialog dialog = new System.Windows.Forms.FolderBrowserDialog();
        //    //dialog.Description = "请存放文件夹";
        //    //if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
        //    //{
        //    //    if (string.IsNullOrEmpty(dialog.SelectedPath))
        //    //    {
        //    //       MessageBox.Show( "文件夹路径不能为空", "提示");
        //    //        return 9999;
        //    //    }



        //        //saveFileDialog.Filter = "_execl files (*.xls)|*.xls";
        //        //saveFileDialog.FilterIndex = 0;
        //        //saveFileDialog.RestoreDirectory = true;
        //        ////saveFileDialog.CreatePrompt = true;
        //        //saveFileDialog.Title = "Export Excel File";
        //        //if (saveFileDialog.ShowDialog() == DialogResult.OK)
        //        //{
        //        //    if (saveFileDialog.FileName == "")
        //        //    {
        //        //        MessageBox.Show("请输入保存文件名！");
        //        //        saveFileDialog.ShowDialog();
        //        //    }
        //        // 列索引，行索引，总列数，总行数
        //        int ColIndex = 0;
        //        int RowIndex = 0;
        //        int Col_count = myDGV.ColumnCount;
        //        int Row_count = myDGV.RowCount;

        //        if (Row_count == 0)
        //        {
        //            result = 1;
        //        }

        //    // 创建Excel对象
        //    Microsoft.Office.Interop.Excel.Application xlApp = new Microsoft.Office.Interop.Excel.ApplicationClass();

        //    if (xlApp == null)
        //        {
        //            result = 2;
        //        }
        //        try
        //        {
        //            // 创建Excel工作薄
        //            Microsoft.Office.Interop.Excel.Workbook xlBook = xlApp.Workbooks.Add(true);
        //            Microsoft.Office.Interop.Excel.Worksheet xlSheet = (Microsoft.Office.Interop.Excel.Worksheet)xlBook.Worksheets[1];
        //            // 设置标题
        //            Microsoft.Office.Interop.Excel.Range range = xlSheet.get_Range(xlApp.Cells[1, 1], xlApp.Cells[1, Col_count]); //标题所占的单元格数与DataGridView中的列数相同
        //            range.MergeCells = true;
        //            xlApp.ActiveCell.FormulaR1C1 = strCaption;
        //            xlApp.ActiveCell.Font.Size = 20;
        //            xlApp.ActiveCell.Font.Bold = true;
        //            xlApp.ActiveCell.HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlCenter;
        //            // 创建缓存数据
        //            object[,] objData = new object[Row_count + 1, Col_count];
        //            //获取列标题
        //            foreach (DataGridViewColumn col in myDGV.Columns)
        //            {
        //                objData[RowIndex, ColIndex++] = col.HeaderText;
        //            }
        //            // 获取数据
        //            for (RowIndex = 1; RowIndex <= Row_count; RowIndex++)
        //            {
        //                for (ColIndex = 0; ColIndex < Col_count; ColIndex++)
        //                {
        //                    if (myDGV[ColIndex, RowIndex - 1].ValueType == typeof(string)
        //                        || myDGV[ColIndex, RowIndex - 1].ValueType == typeof(DateTime))//这里就是验证DataGridView单元格中的类型,如果是string或是DataTime类型,则在放入缓存时在该内容前加入" ";
        //                    {
        //                        objData[RowIndex, ColIndex] = "" + myDGV[ColIndex, RowIndex - 1].Value;
        //                    }
        //                    else
        //                    {
        //                        objData[RowIndex, ColIndex] = myDGV[ColIndex, RowIndex - 1].Value;
        //                    }
        //                }
        //                System.Windows.Forms.Application.DoEvents();
        //            }
        //            // 写入Excel
        //            range = xlSheet.get_Range(xlApp.Cells[2, 1], xlApp.Cells[Row_count+2, Col_count]);
        //            range.Value2 = objData;

        //            xlBook.Saved = true;


        //            //xlBook.SaveCopyAs(saveFileDialog.FileName);

        //             xlBook.SaveCopyAs(savepath);
        //        }
        //        catch (Exception err)
        //        {
        //            result = 9999;
        //        }
        //        finally
        //        {
        //            xlApp.Quit();
        //            GC.Collect(); //强制回收
        //        }
        //        //返回值
        //        result = 0;


        //    return result;
        //}


        
    }

}
