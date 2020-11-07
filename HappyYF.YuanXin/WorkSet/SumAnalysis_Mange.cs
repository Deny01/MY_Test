using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient ;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DataGridFilter;
using HappyYF.Infrastructure.Repositories;
using HappyYF.YuanXin.Data;

namespace HappyYF.YuanXin.WorkSet
{
    public partial class SumAnalysis_Mange : Form
    {
         private DataSet ds;
        BindingSource bindingSource2;

        public SumAnalysis_Mange()
        {
            InitializeComponent();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            
            string header = " select ";
            string ender = " group by ";

           
           // string sWhere = "  where ((a.service_number IS NULL) OR (a.service_number is not null)) ";

            string sWhere = "  where  1 = 1 ";

            if (this.checkBox8.Checked)
            {

                sWhere = sWhere + "  and ( a.pay_date >= '" + dateTimePicker1.Text + "' and a.pay_date <= '" + dateTimePicker2.Text + "') ";


            }

            //////////////////////////////////////////////////
            
            if (this.checkBoxinsuranceCompany.Checked)
            {
                //header = header + " (case  when a.item_company is not null then '已结' when b.balance_Date is null  then '未结' end ) as 是否结账,";
                //ender = ender + " (case  when b.balance_Date is not null then '已结' when b.balance_Date is null  then '未结' end ),";

                header = header + "  a.item_company  as 保险公司,";
                ender = ender + " a.item_company,";


                if (textBoxinsuranceComPany.Text.Replace(" ", "").Length > 0)
                {

                    sWhere = sWhere + " and  ( a.item_company = '" + textBoxinsuranceComPany.Text + "') ";


                }

            }
            
            //////////////////////////////////////////////////////

            if (this.checkBoxbillNumber.Checked)
            {
                header = header + " a.bill_number  as 保险单号,";
                ender = ender + " a.bill_number ,";

                if (textBoxbillNumber.Text.Replace(" ", "").Length > 0)
                {

                    sWhere = sWhere + " and  ( a.bill_number  = '" + textBoxbillNumber.Text + "') ";


                }



            }


            //////////////////////////////////////////////////


           
            if (this.checkBoxpayDate.Checked)
            {
                header = header + " a.pay_date  as 交款日期,";
                ender = ender + " a.pay_date ,";

                if (textBoxpayDate.Text.Replace(" ", "").Length > 0)
                {

                    sWhere = sWhere + " and  ( a.pay_date  = '" + textBoxpayDate.Text + "') ";


                }



            }


            //////////////////////////////////////////////////

            if (this.checkBoxtermDate.Checked)
            {
                header = header + " a.to_day  as 到期日期,";
                ender = ender + " a.from_day,a.to_day ,";

                if (textBoxtermDate.Text.Replace(" ", "").Length > 0)
                {

                    sWhere = sWhere + " and  ( a.to_day  = '" + textBoxtermDate.Text + "') ";


                }



            }


            //////////////////////////////////////////////////
            

            if (this.checkBoxleftDays.Checked)
            {
                header = header + " case when month_money>0 then  DATEDIFF(day, GETDATE(), to_day) else DATEDIFF(day, GETDATE(), from_day) end   as 剩余天数,";
                ender = ender + " case when month_money>0 then  DATEDIFF(day, GETDATE(), to_day) else DATEDIFF(day, GETDATE(), from_day) end ,";

                if (textBoxleftDays.Text.Replace(" ", "").Length > 0)
                {

                    sWhere = sWhere + " and  ( case when month_money>0 then  DATEDIFF(day, GETDATE(), to_day) else DATEDIFF(day, GETDATE(), from_day) end)  = '" + textBoxleftDays.Text + "') ";


                }



            }


            //////////////////////////////////////////////////







            if (this.checkBoxclientNumber.Checked)
            {
                header = header + " isnull(b.Client_Code,'其它')  as 客户编号,";
                ender = ender + " isnull(b.Client_Code,'其它'),";

                if (textBoxclientNumber.Text.Replace(" ", "").Length > 0)
                {

                    sWhere = sWhere + " and  ( b.Client_Code = '" + textBoxclientNumber.Text + "') ";

                   
                }



            }

            /////////////////////////////////////////

            if (this.checkBoxclientName.Checked)
            {
                header = header + " c.Name as 客户姓名,";
                ender = ender + " c.Name,";

                if (textBoxclientName.Text.Replace(" ", "").Length > 0)
                {

                    sWhere = sWhere + " and   c.Name = '" + textBoxclientName.Text + "'";


                }




            }


            ///////////////////////////////////////////////////

            if (this.checkBoxvehicleNumber.Checked)
            {
                header = header + " b.vehicle_code  as 车号,";
                ender = ender + " b.vehicle_code,";

                if (textBoxvehicleNumber.Text.Replace(" ", "").Length > 0)
                {

                    sWhere = sWhere + " and  ( b.vehicle_code = '" + textBoxvehicleNumber.Text + "') ";

                
                }



            }
            ///////////////////////////////////////////////////

            if (this.loanstylecheckBox.Checked)
            {
                header = header + " (case when b.Loan_style='转往外地' then '转往外地' when  b.Loan_style='车辆销户' then '车辆销户' else '公司车辆' end)  as 车辆去向,";
                ender = ender + " (case when b.Loan_style='转往外地' then '转往外地' when  b.Loan_style='车辆销户' then '车辆销户' else '公司车辆' end),";

                if (loanstyletextBox.Text.Replace(" ", "").Length > 0)
                {

                    sWhere = sWhere + " and  ( (case when b.Loan_style='转往外地' then '转往外地' when  b.Loan_style='车辆销户' then '车辆销户' else '公司车辆' end) = '" + loanstyletextBox.Text + "') ";


                }



            }

   
               //////////////////////////////////////////////////


                if (this.checkBoxyear.Checked)
                {
                    header = header + " year(a.pay_date) as 年份, ";
                    ender = ender + " year(a.pay_date),";

                    //if (textBox4.Text.Replace(" ", "").Length > 0)
                    //{
                    //    if (sWhere == "")
                    //    {
                    //        sWhere = " where a.dayclient_name = '" + textBox4.Text + "'";
                    //    }
                    //    else
                    //    {
                    //        sWhere = sWhere + " and   a.dayclient_name = '" + textBox4.Text + "'";

                    //    }
                    //}



            }
                //////////////////////////////////////////////////

         

         

        
          

            //////////////////////////////////////////////////


                if (this.checkBoxmonth.Checked)
                {
                    header = header + " year(a.pay_date) as 年份, month(a.pay_date) as 月份, ";
                    ender = ender + " year(a.pay_date),month(a.pay_date),";

                    //if (textBox4.Text.Replace(" ", "").Length > 0)
                    //{
                    //    if (sWhere == "")
                    //    {
                    //        sWhere = " where a.dayclient_name = '" + textBox4.Text + "'";
                    //    }
                    //    else
                    //    {
                    //        sWhere = sWhere + " and   a.dayclient_name = '" + textBox4.Text + "'";

                    //    }
                    //}



                }
                //////////////////////////////////////////////////

                if (this.checkBoxdate.Checked)
                {
                    header = header + " a.pay_date as 日期,  ";
                    ender = ender + " a.pay_date,";

                    //if (textBox4.Text.Replace(" ", "").Length > 0)
                    //{
                    //    if (sWhere == "")
                    //    {
                    //        sWhere = " where a.dayclient_name = '" + textBox4.Text + "'";
                    //    }
                    //    else
                    //    {
                    //        sWhere = sWhere + " and   a.dayclient_name = '" + textBox4.Text + "'";

                    //    }
                    //}



                }
                //////////////////////////////////////////////////


                
                //////////////////////////////////////////////////

                if (this.haveprintcheckBox.Checked)
                {
                    header = header + " (case when isnull(a.print_times,0) >0 then '已打印' else '未打印' end)  as 是否打印,";
                    ender = ender + " (case when isnull(a.print_times,0) >0 then '已打印' else '未打印' end) ,";

                    if ( this.haveprintcomboBox.Text.Replace(" ", "").Length > 0)
                    {
                        if (this.haveprintcomboBox.Text.Replace(" ", "")!="全部" )
                            sWhere = sWhere + " and  ( (case when isnull(a.print_times,0) >0 then '已打印' else '未打印' end)  = '" + haveprintcomboBox.Text + "') ";


                    }



                }


                //////////////////////////////////////////////////
                //////////////////////////////////////////////////

                if (this.havehandincheckBox.Checked)
                {
                    header = header + " (case when isnull(a.handin_money,'未交') = '已交' and isnull(a.print_times,0)>0 then '已交' else '未交' end)  as 是否交款,";
                    ender = ender + " (case when isnull(a.handin_money,'未交') = '已交'and isnull(a.print_times,0)>0  then '已交' else '未交' end) ,";

                    if (this.havehandincomboBox.Text.Replace(" ", "").Length > 0)
                    {
                        if (this.havehandincomboBox.Text.Replace(" ", "") != "全部")
                            sWhere = sWhere + " and  (( case when isnull(a.handin_money,'未交') = '已交'and isnull(a.print_times,0)>0  then '已交' else '未交' end)  = '" + havehandincomboBox.Text + "') ";


                    }


                }


                //////////////////////////////////////////////////

                ///////////////////////////////////////////////////

                if (this.handincheckBox.Checked)
                {
                    header = header + " a.operator  as 收款人,";
                    ender = ender + " a.operator,";

                    if (handintextBox.Text.Replace(" ", "").Length > 0)
                    {

                        sWhere = sWhere + " and  ( a.operator = '" + handintextBox.Text + "') ";


                    }



                }

                ///////////////////////////////////////////////////

                if (this.handinDatecheckBox.Checked)
                {
                    header = header + " convert(char(10),a.handin_date,120)  as 收款日期,";
                    ender = ender + " convert(char(10),a.handin_date,120),";

                    if (handinDatetextBox.Text.Replace(" ", "").Length > 0)
                    {

                        sWhere = sWhere + " and  ( convert(char(10),a.handin_date,120) = '" + handinDatetextBox.Text + "') ";


                    }



                }


                //////////////////////////////////////////////////


                if (ender.Length > 0)
                {
                    ender = ender.Substring(0, ender.Length - 1);

                }
                else
                {
                    ender = "";
                }

          


            


          

                //string body = " count(*) as 已上课时,  convert(decimal(18,2),round(sum(d.schooling / d.times),2))  as 标准应收, " +
                //                                " convert(decimal(18,2),round(sum( d.tuition/d.times),2))  as 实收金额, convert(decimal(18,2),round(sum(d.schooling /d.times) - sum( d.tuition/d.times),2))  as 实际折扣" +
                //       " FROM YX_daywork_record AS a LEFT OUTER JOIN " +
                //       "  Itemofservice AS b ON a.itemofservice_number = b.ItemofserviceNumber " +
                //       " LEFT OUTER JOIN " +
                //       " YX_group_detail AS c on a.itemofservice_number = c.ItemofserviceNumber " +
                //       " LEFT OUTER JOIN " +
                //       " HQ_classStudent AS d on a.studentclass_Id = d.Id " +
                //       " LEFT OUTER JOIN " +
                //       " YX_client AS e on a.Card_number = e.Card_number " +
                //   " LEFT OUTER JOIN " +
                //          " T_users AS f on a.service_people = f.yhbm " +
                //   " LEFT OUTER JOIN " +
                //          " T_users AS g on a.operator = g.yhbm ";

                string body = " avg( isnull(a.month_money,0))  as 平均月费, " +
                              " sum( isnull(a.item_pay_money,0)+ isnull(a.discount_money,0))  as 应收, " +
                              " sum(isnull(a.discount_money,0)) as 优惠, " +
                              " sum( isnull(a.item_real_money,0))  as 实收 " +
                           
                             // " sum( isnull(a.shangye,0) + isnull(a.jiaoqiang,0)+isnull(a.huowu,0) + isnull(a.chechuan,0)) as 保险总额 " +
                              //"  sum(isnull(a.accrual_money,0))  as 利息, " +
                              //" sum(isnull(a.compensate_accrual_money,0)) as 还息, " +
                              //" ( sum(isnull( a.accrual_money,0)) -    sum(isnull(a.compensate_accrual_money,0))) as 欠息," +
                              //" left(max(convert(varchar(20),accrual_date,120)),10)  as 计息日期" +
                          " FROM HT_Manage_Item AS a LEFT OUTER JOIN " +
                          "  XD_Client_Loan AS b ON a.loan_id = b.Id " +
                          " LEFT OUTER JOIN " +
                          " YX_client AS c on b.Client_Code = c.Card_number ";
                          //+
                          //" LEFT OUTER JOIN " +
                          //" XD_client_compensate_loan AS d on a.loan_id = d.loan_id and a.accrual_date = d.compensate_date  ";
                 
            
            string sel;

            if (ender.Length  != 9)
            {

                sel = header + body + sWhere + ender;
            }
            else
            {
                sel = header + body + sWhere ;
            
            }


            SqlDataAdapter myAdapter = new SqlDataAdapter(sel, SQLDatabase.Connectstring);

            //DataSet allData = new DataSet();


            //myAdapter.Fill(allData);


            if (null != ds)
                ds.Dispose();

            ds = new DataSet();

            myAdapter.Fill(ds);


            myAdapter.Dispose();

            //this.dataGridView1.DataSource = allData;


            this.dataGridView1.Columns.Clear();

            this.dataGridView1.AutoGenerateColumns = true;
            dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCellsExceptHeaders;

            //BindingSource bindingSource1 = new BindingSource();

            if (null != bindingSource2)
                this.bindingSource2.Dispose();

            this.bindingSource2 = new BindingSource();


            bindingSource2.DataSource = ds.Tables[0];
            dataGridView1.DataSource = bindingSource2;

            this.bindingNavigator1.BindingSource = bindingSource2;

            bindingSource2.ResetBindings(true);

            AddSummationRow(bindingSource2, dataGridView1);
        }

        //大类
        private void dalei_Click(object sender, EventArgs e)
        {
            //string sel = "SELECT GroupName as 大类名称,cast (GroupID as varchar(20)) as 大类代码 FROM YX_group_main";
            string sel = "SELECT distinct item_company as 保险公司 FROM HT_Insurance_Item";


            QueryForm queryForm = new QueryForm();


            queryForm.Sel = sel;
            queryForm.Constr = SQLDatabase .Connectstring;

            //Set the Column Collection to the filter Table
            //queryForm.SetSourceColumns(this.billMainDataSet.BalanceBill.Columns);

            queryForm.ShowDialog();

            this.textBoxinsuranceComPany.Text = queryForm.Result;
        }

        private void xiangmu_Click(object sender, EventArgs e)
        {
            string sel = "SELECT a.vehicle_code as 车号,b.Card_number as 客户编号, b.Name as 姓名 FROM XD_Client_Loan a " +
                          "    Left join  YX_client b on a.Client_Code = b.Card_number ";
            

            QueryForm queryForm = new QueryForm();


            queryForm.Sel = sel;
            queryForm.Constr = SQLDatabase.Connectstring;

            //Set the Column Collection to the filter Table
            //queryForm.SetSourceColumns(this.billMainDataSet.BalanceBill.Columns);

            queryForm.ShowDialog();

            this.textBoxvehicleNumber.Text = queryForm.Result;
        }

        //private void chehao_Click(object sender, EventArgs e)
        //{
        //    string sel = "SELECT distinct vehicle_code as 车号 FROM YX_daywork_record " +

        //    " where vehicle_code is not null and len(vehicle_code) > 0 and (service_number is null or len(service_number) < 1) ";


        //    QueryForm queryForm = new QueryForm();


        //    queryForm.Sel = sel;
        //    queryForm.Constr = SQLDatabase.Connectstring;

        //    //Set the Column Collection to the filter Table
        //    //queryForm.SetSourceColumns(this.billMainDataSet.BalanceBill.Columns);

        //    queryForm.ShowDialog();

        //    this.textBox3.Text = queryForm.Result;
        //}

        //private void kehu_Click(object sender, EventArgs e)
        //{

        //    string sel = "SELECT  Name as 姓名, Card_number as 卡号 FROM YX_client";


        //    QueryForm queryForm = new QueryForm();


        //    queryForm.Sel = sel;
        //    queryForm.Constr = SQLDatabase.Connectstring;

        //    //Set the Column Collection to the filter Table
        //    //queryForm.SetSourceColumns(this.billMainDataSet.BalanceBill.Columns);

        //    queryForm.ShowDialog();

        //    this.textBox3.Text = queryForm.Result;
        //}

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if (null == ds)
                return;

            FilterForm filterForm = new FilterForm();

            //SumQueryDataSet qds;
            //qds = new SumQueryDataSet();

             List < string > ls = new List<string> ();
             ls.Add("id");
   

            filterForm.SetSourceColumns(ds.Tables[0].Columns,ls);

            filterForm.ShowDialog();

            this.bindingSource2.Filter = filterForm.GetFilterString();
        }

        public bool ExportDataGridview(DataGridView gridView, bool isShowExcle)
        {
            if (gridView.Rows.Count == 0)
                return false;
            //建立Excel对象
            Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();
            excel.Application.Workbooks.Add(true);
            excel.Visible = isShowExcle;
            //生成字段名称
            for (int i = 0; i < gridView.ColumnCount; i++)
            {
                excel.Cells[1, i + 1] = gridView.Columns[i].HeaderText;
            }
            //填充数据
            for (int i = 0; i <= gridView.RowCount - 1; i++)
            {
                for (int j = 0; j < gridView.ColumnCount; j++)
                {
                    if (gridView[j, i].ValueType == typeof(string))
                    {
                        excel.Cells[i + 2, j + 1] = "'" + gridView[j, i].Value.ToString();
                    }
                    else
                    {
                        excel.Cells[i + 2, j + 1] = gridView[j, i].Value.ToString();
                    }
                }
            }
            return true;
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            ExportDataGridview(dataGridView1, true);
        }

        private string selstr(string str)
        {
            string res;

            switch (str.Replace(" ", ""))
            {
               
                case "保险公司":
                    res = " a.item_company  as 保险公司";
                    break;
                case "保险单号":
                    res = "  a.bill_number  as 保险单号";
                    break;
                case "交款日期":
                    res = " a.pay_date  as 交款日期";
                    break;
                case "到期日期":
                    res = " a.to_day  as 到期日期";
                    break;
                case "剩余天数":
                    res = " DATEDIFF(day, GETDATE(), a.to_day)   as 剩余天数";
                    break;
                case "车主姓名":
                    res = " c.Name as 客户姓名";
                    break;
                case "车主车号":
                    res = " b.vehicle_code  as 车号";
                    break;
                case "车主编号":
                    res = " isnull(b.Client_Code,'其它')  as 客户编号";
                    break;
                case "年份":
                    res = " year(a.pay_date) as 年份";
                    break;
                case "月份":
                    res = " year(a.pay_date) as 年份, month(a.pay_date) as 月份";
                    break;
                case "日期":
                    res = " a.pay_date as 日期";
                    break;
            
                default:
                    res = "";
                    break;


            }

            return res;
        }

        private string grustr(string str)
        {

            string res;

            switch (str.Replace(" ", ""))
            {
                 case "保险公司":
                    res = " a.item_company";
                    break;
                 case "保险单号":
                    res = " a.bill_number";
                    break;
                 case "交款日期":
                    res = " a.pay_date";
                    break;
                 case "到期日期":
                    res = " a.to_day";
                    break;
                 case "剩余天数":
                    res = " DATEDIFF(day, GETDATE(), a.to_day)";
                    break;
                 case "车主姓名":
                    res = " c.Name";
                    break;
                 case "车主车号":
                    res = " b.vehicle_code";
                    break;
                 case "车主编号":
                    res = " isnull(b.Client_Code,'其它')";
                    break;


                case "年份":
                    res = " year(a.pay_date)";
                    break;
              
                case "月份":
                    res = " year(a.pay_date),month(a.pay_date)";
                    break;
                case "日期":
                    res = " a.pay_date";
                    break;

             
                default:
                    res = "";
                    break;


            }

            return res;
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            SumQuery qds;
            qds = new SumQuery();

            SortForm DataSort = new SortForm();
            //DataGridView dv = (DataGridView)DataSort.Controls["sortTableDataGridView"];

            DataGridView dv = DataSort.sortTableDataGridView;
            dv.Columns[1].Visible = false;



            DataSort.Text = "分组设置";

            List<string> ls = new List<string>();
            ls.Add("id");

            DataSort.SetSortColumns(qds.QueryColumns_Mange.Columns, ls);
            DataSort.ShowDialog();

            string groupStr = DataSort.GetSortString();

            ////////////
            if (null != groupStr)
            {

                string[] result = groupStr.Split(new Char[] { ',' });

                string header = " select ";
                string ender = " group by ";

                string sWhere = "  where ( 1 = 1) ";

                if (this.checkBox8.Checked)
                {

                    sWhere = sWhere + "  and ( a.pay_date >= '" + dateTimePicker1.Text + "' and a.pay_date <= '" + dateTimePicker2.Text + "') ";


                }


                //if (sWhere.Length > 3)
                //{
                //    sWhere = " where " + sWhere.Substring(0, sWhere.Length - 3);

                //}
                //else
                //{
                //    sWhere = "";
                //}


                if (result.Length > 0)
                {
                    for (int i = 0; i < result.Length; i++)
                    {
                        header = header + selstr(result[i]) + ",";

                        if (i < result.Length - 1)
                        {
                            ender = ender + grustr(result[i]) + ",";
                        }
                        else
                        {
                            ender = ender + grustr(result[i]);
                        }

                    }
                }


                string body = " avg( isnull(a.month_money,0))  as 平均月费, " +
                                             " sum( isnull(a.item_pay_money,0))  as 应收, " +
                                             " sum(isnull(a.discount_money,0)) as 优惠, " +
                                             " sum( isnull(a.item_real_money,0))  as 实收 " +

                                            // " sum( isnull(a.shangye,0) + isnull(a.jiaoqiang,0)+isnull(a.huowu,0) + isnull(a.chechuan,0)) as 保险总额 " +
                    //"  sum(isnull(a.accrual_money,0))  as 利息, " +
                    //" sum(isnull(a.compensate_accrual_money,0)) as 还息, " +
                    //" ( sum(isnull( a.accrual_money,0)) -    sum(isnull(a.compensate_accrual_money,0))) as 欠息," +
                    //" left(max(convert(varchar(20),accrual_date,120)),10)  as 计息日期" +
                                         " FROM HT_Manage_Item AS a LEFT OUTER JOIN " +
                                         "  XD_Client_Loan AS b ON a.loan_id = b.Id " +
                                         " LEFT OUTER JOIN " +
                                         " YX_client AS c on b.Client_Code = c.Card_number ";
                
                string sel;


                if (result.Length > 0)
                    sel = header + body + sWhere + ender;
                else
                    sel = header + body + sWhere;

                SqlDataAdapter myAdapter = new SqlDataAdapter(sel, SQLDatabase .Connectstring );

                //DataSet allData = new DataSet();


                //myAdapter.Fill(allData);

                if (null != ds)
                    ds.Dispose();

                ds = new DataSet();

                myAdapter.Fill(ds);
                myAdapter.Dispose();

                //this.dataGridView1.DataSource = allData;


                this.dataGridView1.Columns.Clear();

                this.dataGridView1.AutoGenerateColumns = true;
                dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCellsExceptHeaders;

                //BindingSource bindingSource1 = new BindingSource();



                //DataTableReader reader = new DataTableReader(allData.Tables[0]);

                //this.billMainDataSet.BalanceBill.Clear();
                //this.billMainDataSet.BalanceBill.Load(reader);
                //ds = allData;
                if (null != bindingSource2)
                    this.bindingSource2.Dispose();

                this.bindingSource2 = new BindingSource();
                this.bindingSource2.DataSource = ds.Tables[0];
                dataGridView1.DataSource = bindingSource2;

                this.bindingNavigator1.BindingSource = bindingSource2;

                bindingSource2.ResetBindings(true);

                AddSummationRow(bindingSource2, dataGridView1);

            }
        }

        private void kahao_Click(object sender, EventArgs e)
        {

            string sel = "SELECT  Card_number as 卡号, Name as 姓名 FROM YX_client";


            QueryForm queryForm = new QueryForm();


            queryForm.Sel = sel;
            queryForm.Constr = SQLDatabase.Connectstring;

            //Set the Column Collection to the filter Table
            //queryForm.SetSourceColumns(this.billMainDataSet.BalanceBill.Columns);

            queryForm.ShowDialog();

            this.textBoxclientNumber.Text = queryForm.Result;
        }

        
        private void xiaofei_Click(object sender, EventArgs e)
        {
            string sel = "SELECT distinct consumer_class as 消费类别  FROM YX_daywork_record";


            QueryForm queryForm = new QueryForm();


            queryForm.Sel = sel;
            queryForm.Constr = SQLDatabase.Connectstring;

            //Set the Column Collection to the filter Table
            //queryForm.SetSourceColumns(this.billMainDataSet.BalanceBill.Columns);

            queryForm.ShowDialog();

            this.textBoxleftDays.Text = queryForm.Result;
        }

        private void taocan_Click(object sender, EventArgs e)
        {

            string sel = "SELECT distinct bill_number as 保险单号, item_company  as 保险公司 FROM HT_Insurance_Item";

            QueryForm queryForm = new QueryForm();


            queryForm.Sel = sel;
            queryForm.Constr = SQLDatabase.Connectstring;

            //Set the Column Collection to the filter Table
            //queryForm.SetSourceColumns(this.billMainDataSet.BalanceBill.Columns);

            queryForm.ShowDialog();

            this.textBoxbillNumber.Text = queryForm.Result;
        }

        private void fuwu_Click(object sender, EventArgs e)
        {

            string sel = "SELECT  Service_number as 服务号, Taocan_number as 套餐号,name as 套餐名 FROM YX_taocan_main_real";


            QueryForm queryForm = new QueryForm();


            queryForm.Sel = sel;
            queryForm.Constr = SQLDatabase.Connectstring;

            //Set the Column Collection to the filter Table
            //queryForm.SetSourceColumns(this.billMainDataSet.BalanceBill.Columns);

            queryForm.ShowDialog();

            //this.textBox12.Text = queryForm.Result;
        }

        private void AddSummationRow(BindingSource bs, DataGridView dg)
        {
            //this.yX_daywork_recordDataGridView1.Columns[6].summation
            //this.dailyoperation.YX_daywork_record.Columns[7].
            if (null == dg.CurrentRow) return;

            decimal sum_principal = 0;
            decimal sum_compensate_principal = 0;
            decimal sum_left_principal = 0;
            decimal sum_accrual = 0;
            decimal sum_compensate_accrual = 0;
            decimal sum_left_accrual = 0;



            foreach (DataGridViewRow dr in dg.Rows)
            {
                if (System.DBNull.Value == dr.Cells["应收"].Value)
                    sum_principal = sum_principal + 0;
                else
                    sum_principal = sum_principal + decimal.Parse(dr.Cells["应收"].Value.ToString());

                if (System.DBNull.Value == dr.Cells["优惠"].Value)
                    sum_compensate_principal = sum_compensate_principal + 0;
                else
                    sum_compensate_principal = sum_compensate_principal + decimal.Parse(dr.Cells["优惠"].Value.ToString());

                if (System.DBNull.Value == dr.Cells["实收"].Value)
                    sum_left_principal = sum_left_principal + 0;
                else
                    sum_left_principal = sum_left_principal + decimal.Parse(dr.Cells["实收"].Value.ToString());


                //if (System.DBNull.Value == dr.Cells["车船税"].Value)
                //    sum_accrual = sum_accrual + 0;
                //else
                //    sum_accrual = sum_accrual + decimal.Parse(dr.Cells["车船税"].Value.ToString());

                //if (System.DBNull.Value == dr.Cells["保险总额"].Value)
                //    sum_compensate_accrual = sum_compensate_accrual + 0;
                //else
                //    sum_compensate_accrual = sum_compensate_accrual + decimal.Parse(dr.Cells["保险总额"].Value.ToString());

                //if (System.DBNull.Value == dr.Cells["欠息"].Value)
                //    sum_left_accrual = sum_left_accrual + 0;
                //else
                //    sum_left_accrual = sum_left_accrual + decimal.Parse(dr.Cells["欠息"].Value.ToString());

             

            }
            bs.AddNew();


            dg.CurrentRow.Cells["应收"].Value = sum_principal;
            dg.CurrentRow.Cells["优惠"].Value = sum_compensate_principal;
            dg.CurrentRow.Cells["实收"].Value = sum_left_principal;
            //dg.CurrentRow.Cells["车船税"].Value = sum_accrual;
            //dg.CurrentRow.Cells["保险总额"].Value = sum_compensate_accrual;
            //dg.CurrentRow.Cells["欠息"].Value = sum_left_accrual;

       

            bs.EndEdit();

            bs.Position = 0;




        }

        private void button19_Click(object sender, EventArgs e)
        {
            //string sel = "SELECT  '(' +isnull(yhbm,'')+')'  + isnull(yhmc,'') as 服务人员 FROM T_users";

            string sel = "SELECT distinct yhmc as 教师, yhbm  as 编号 FROM T_users";

            QueryForm queryForm = new QueryForm();


            queryForm.Sel = sel;
            queryForm.Constr = SQLDatabase.Connectstring;

            //Set the Column Collection to the filter Table
            //queryForm.SetSourceColumns(this.billMainDataSet.BalanceBill.Columns);

            queryForm.ShowDialog();

            //this.textBox19.Text = queryForm.Result;
        }

        private void chehao_Click(object sender, EventArgs e)
        {
            string sel = "SELECT   Name as 姓名,Card_number as 卡号 FROM YX_client";


            QueryForm queryForm = new QueryForm();


            queryForm.Sel = sel;
            queryForm.Constr = SQLDatabase.Connectstring;

            //Set the Column Collection to the filter Table
            //queryForm.SetSourceColumns(this.billMainDataSet.BalanceBill.Columns);

            queryForm.ShowDialog();

            this.textBoxclientName.Text = queryForm.Result;
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            if (null == this.dataGridView1.CurrentRow) return;
            SortForm DataSort = new SortForm();

            List<string> ls = new List<string>();
            ls.Add("id");


            DataSort.SetSortColumns(this.ds.Tables[0].Columns, ls);
            DataSort.ShowDialog();
            this.bindingSource2.Sort = DataSort.GetSortString();
        }

        private void handinbutton_Click(object sender, EventArgs e)
        {
            string sel = "SELECT distinct  operator as 收款人 FROM HT_Manage_Item";


            QueryForm queryForm = new QueryForm();


            queryForm.Sel = sel;
            queryForm.Constr = SQLDatabase.Connectstring;

            //Set the Column Collection to the filter Table
            //queryForm.SetSourceColumns(this.billMainDataSet.BalanceBill.Columns);

            queryForm.ShowDialog();

            this.handintextBox.Text = queryForm.Result;
        }

        private void handinDatebutton_Click(object sender, EventArgs e)
        {
            DatePicker queryForm = new DatePicker();
           

           
                queryForm.NowDate = DateTime .Today .Date.ToString () ;

            queryForm.ShowDialog();



            if (null != queryForm.NowDate)
            {

                handinDatetextBox.Text  = queryForm.NowDate.Substring(0,10);
              

            }
        }

        private void loanstylebutton_Click(object sender, EventArgs e)
        {
            string sel = "SELECT distinct  (case when b.Loan_style='转往外地' then '转往外地' when  b.Loan_style='车辆销户' then '车辆销户' else '公司车辆' end) as 车辆去向 FROM XD_Client_Loan b ";
            QueryForm queryForm = new QueryForm();


            queryForm.Sel = sel;
            queryForm.Constr = SQLDatabase.Connectstring;

            //Set the Column Collection to the filter Table
            //queryForm.SetSourceColumns(this.billMainDataSet.BalanceBill.Columns);

            queryForm.ShowDialog();

            this.loanstyletextBox.Text = queryForm.Result;
        }
       
    }
}
