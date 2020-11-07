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
    public partial class LY_Quality_SumAnalysis : Form
    {
         private DataSet ds;
        BindingSource bindingSource2;

        GroupQueryItemSelector gqis;
        private int selectionIdx = 0;


        public LY_Quality_SumAnalysis()
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

            string sWhere = "  where  YY_DataBase = '" + SQLDatabase .nowUserName () + "'";
            //string sWhere = "  where  1 = 1 and b.Client_Code is not null ";


            //if (this.checkBox8.Checked)
            //{

            //    sWhere = sWhere + "  and ( a.from_day >= '" + dateTimePicker1.Text + "' and a.from_day <= '" + dateTimePicker2.Text + "') ";


            //}

            //////////////////////////////////////////////////

            if (this.checkBoxinsuranceCompany.Checked)
            {
                //header = header + " (case  when a.item_company is not null then '已结' when b.balance_Date is null  then '未结' end ) as 是否结账,";
                //ender = ender + " (case  when b.balance_Date is not null then '已结' when b.balance_Date is null  then '未结' end ),";

                header = header + "  a.year_code  as 年份,";
                ender = ender + " a.year_code,";


                if (textBoxinsuranceComPany.Text.Replace(" ", "").Length > 0)
                {

                    sWhere = sWhere + " and  ( a.year_code = '" + textBoxinsuranceComPany.Text + "') ";


                }

            }
            
            //////////////////////////////////////////////////////

            if (this.checkBoxbillNumber.Checked)
            {
                header = header + " a.month_code  as 月份,";
                ender = ender + " a.month_code ,";

                if (textBoxbillNumber.Text.Replace(" ", "").Length > 0)
                {

                    sWhere = sWhere + " and  ( a.month_code  = '" + textBoxbillNumber.Text + "') ";


                }



            }


          

            //////////////////////////////////////////////////
            if (this.checkBoxvehicleNumber.Checked)
            {
                header = header + " a.upocName  as 部门全称,";
                ender = ender + " a.upocName ,";

                if (textBoxvehicleNumber.Text.Replace(" ", "").Length > 0)
                {

                    sWhere = sWhere + " and  ( a.upocName  = '" + textBoxvehicleNumber.Text + "') ";


                }



            }


            if (this.checkBox2.Checked)
            {
                header = header + " a.ocName  as 部门,";
                ender = ender + " a.ocName ,";

                if (textBox1.Text.Replace(" ", "").Length > 0)
                {

                    sWhere = sWhere + " and  ( a.ocName  = '" + textBox1.Text + "') ";


                }



            }


            //////////////////////////////////////////////////

            if (this.loanstylecheckBox.Checked)
            {
                header = header + "  a.osgName   as 岗位,";
                ender = ender + "  a.osgName,";

                if (loanstyletextBox.Text.Replace(" ", "").Length > 0)
                {

                    sWhere = sWhere + " and  ( a.osgName  = '" + loanstyletextBox.Text + "') ";


                }



            }


            //////////////////////////////////////////////////

            //////////////////////////////////////////////////

            if (this.checkBox5.Checked)
            {
                header = header + "  a.categoryName   as 岗位类别,";
                ender = ender + "  a.categoryName,";

                if (textBox4.Text.Replace(" ", "").Length > 0)
                {

                    sWhere = sWhere + " and  ( a.categoryName  = '" + textBox4.Text + "') ";


                }



            }


            //////////////////////////////////////////////////
            if (this.checkBox6.Checked)
            {
                header = header + "  a.sequenceName   as 岗位序列,";
                ender = ender + "  a.sequenceName,";

                if (textBox5.Text.Replace(" ", "").Length > 0)
                {

                    sWhere = sWhere + " and  ( a.sequenceName  = '" + textBox5.Text + "') ";


                }



            }


            //////////////////////////////////////////////////
            if (this.checkBoxclientNumber.Checked)
            {
                header = header + "  a.hmWorkNo   as 工号,";
                ender = ender + "  a.hmWorkNo,";

                if (textBoxclientNumber.Text.Replace(" ", "").Length > 0)
                {

                    sWhere = sWhere + " and  ( a.hmWorkNo  = '" + textBoxclientNumber.Text + "') ";


                }



            }
            //////////////////////////////////////////////////
            if (this.checkBoxclientName.Checked)
            {
                header = header + "  a.hmName   as 姓名,";
                ender = ender + "  a.hmName,";

                if (textBoxclientName.Text.Replace(" ", "").Length > 0)
                {

                    sWhere = sWhere + " and  ( a.hmName  = '" + textBoxclientName.Text + "') ";


                }



            }


            //////////////////////////////////////////////////

            if (this.checkBox3.Checked)
            {
                header = header + "  a.hmGender   as 性别,";
                ender = ender + "  a.hmGender,";

                if (textBox2.Text.Replace(" ", "").Length > 0)
                {

                    sWhere = sWhere + " and  ( a.hmGender  = '" + textBox2.Text + "') ";


                }



            }


            //////////////////////////////////////////////////


            if (this.checkBox4.Checked)
            {
                header = header + "  a.hmWorkDate   as 工作日期,";
                ender = ender + "  a.hmWorkDate,";

                if (textBox3.Text.Replace(" ", "").Length > 0)
                {

                    sWhere = sWhere + " and  ( a.hmWorkDate  = '" + textBox3.Text + "') ";


                }



            }


            //////////////////////////////////////////////////

            if (this.checkBoxtermDate.Checked)
            {
                header = header + "  a.hminbankDate   as 从业日期,";
                ender = ender + "  a.hminbankDate,";

                if (textBoxpayDate.Text.Replace(" ", "").Length > 0)
                {

                    sWhere = sWhere + " and  ( a.hminbankDate  = '" + textBoxpayDate.Text + "') ";


                }



            }


            //////////////////////////////////////////////////


            if (this.checkBox1.Checked)
            {
                header = header + "  a.hmTradeDate   as 入行日期,";
                ender = ender + "  a.hmTradeDate,";

                if (textBoxtermDate.Text.Replace(" ", "").Length > 0)
                {

                    sWhere = sWhere + " and  ( a.hmTradeDate  = '" + textBoxtermDate.Text + "') ";


                }



            }


            ////////////////////////////////////////////////////

            //if (this.checkBoxleftDays.Checked)
            //{
            //    header = header + "  count(*)   as 人数,";
            //    //ender = ender + "  count(*),";

            //    if (textBoxleftDays.Text.Replace(" ", "").Length > 0)
            //    {

            //        sWhere = sWhere + " and  ( count(*)  = '" + textBoxleftDays.Text + "') ";


            //    }



            //}


            //////////////////////////////////////////////////////
            //if (this.checkBox14.Checked)
            //{
            //    header = header + "  a.provescore   as 证书分数,";
            //    ender = ender + "  a.provescore,";

            //    if (textBox12.Text.Replace(" ", "").Length > 0)
            //    {

            //        sWhere = sWhere + " and  ( a.provescore  = '" + textBox12.Text + "') ";


            //    }



            //}


            //////////////////////////////////////////////////////

            //if (this.checkBox15.Checked)
            //{
            //    header = header + "  a.performancescore   as 绩效分数,";
            //    ender = ender + "  a.performancescore,";

            //    if (textBox13.Text.Replace(" ", "").Length > 0)
            //    {

            //        sWhere = sWhere + " and  ( a.performancescore  = '" + textBox13.Text + "') ";


            //    }



            //}

            ////////////////////////////////////////////////////

            //if (this.checkBox7.Checked)
            //{
            //    header = header + "  a.hmWorkyear   as 工龄,";
            //    ender = ender + "  a.hmWorkyear,";

            //    if (textBox6.Text.Replace(" ", "").Length > 0)
            //    {

            //        sWhere = sWhere + " and  ( a.hmWorkyear  = '" + textBox6.Text + "') ";


            //    }



            //}

            ////////////////////////////////////////////////////

            //if (this.checkBox9.Checked)
            //{
            //    header = header + "  a.hminbankyear   as 业龄,";
            //    ender = ender + "  a.hminbankyear,";

            //    if (textBox7.Text.Replace(" ", "").Length > 0)
            //    {

            //        sWhere = sWhere + " and  ( a.hminbankyear  = '" + textBox7.Text + "') ";


            //    }



            //}

            ////////////////////////////////////////////////////

            //if (this.checkBox10.Checked)
            //{
            //    header = header + "  a.hmTradeyear   as 行龄,";
            //    ender = ender + "  a.hmTradeyear,";

            //    if (textBox8.Text.Replace(" ", "").Length > 0)
            //    {

            //        sWhere = sWhere + " and  ( a.hmTradeyear  = '" + textBox8.Text + "') ";


            //    }



            //}
            ////////////////////////////////////////////////////

            //if (this.checkBox11.Checked)
            //{
            //    header = header + "  a.hmEdulevel   as 学历,";
            //    ender = ender + "  a.hmEdulevel,";

            //    if (textBox9.Text.Replace(" ", "").Length > 0)
            //    {

            //        sWhere = sWhere + " and  ( a.hmEdulevel  = '" + textBox9.Text + "') ";


            //    }



            //}

            ////////////////////////////////////////////////////

            //if (this.checkBox12.Checked)
            //{
            //    header = header + "  a.hmTechTitle   as 职称,";
            //    ender = ender + "  a.hmTechTitle,";

            //    if (textBox10.Text.Replace(" ", "").Length > 0)
            //    {

            //        sWhere = sWhere + " and  ( a.hmTechTitle  = '" + textBox10.Text + "') ";


            //    }



            //}

            ////////////////////////////////////////////////////

            if (this.checkBox13.Checked)
            {
                header = header + "  a.hmProfessionalqualification   as 专业资格,";
                ender = ender + "  a.hmProfessionalqualification,";

                if (textBox11.Text.Replace(" ", "").Length > 0)
                {

                    sWhere = sWhere + " and  ( a.hmProfessionalqualification  = '" + textBox11.Text + "') ";


                }



            }

            ////////////////////////////////////////////////////

            //if (this.checkBox16.Checked)
            //{
            //    header = header + "  a.cpID   as 职级,";
            //    ender = ender + "  a.cpID,";

            //    if (textBox14.Text.Replace(" ", "").Length > 0)
            //    {

            //        sWhere = sWhere + " and  ( a.cpID  = '" + textBox14.Text + "') ";


            //    }



            //}

            //////////////////////////////////////////////////////

            //if (this.checkBox17.Checked)
            //{
            //    header = header + "  a.dpID   as 职档,";
            //    ender = ender + "  a.dpID,";

            //    if (textBox15.Text.Replace(" ", "").Length > 0)
            //    {

            //        sWhere = sWhere + " and  ( a.dpID  = '" + textBox15.Text + "') ";


            //    }



            //}

            ////////////////////////////////////////////////////

            //if (this.checkBox18.Checked)
            //{
            //    header = header + "  a.countstyle   as 计算方式,";
            //    ender = ender + "  a.countstyle,";

            //    if (textBox16.Text.Replace(" ", "").Length > 0)
            //    {

            //        sWhere = sWhere + " and  ( a.countstyle  = '" + textBox16.Text + "') ";


            //    }



            //}



            ////    //////////////////////////////////////////////////


                if (ender.Length > 0)
                {
                    ender = ender.Substring(0, ender.Length - 1);

                }
                else
                {
                    ender = "";
                }

          


            


          


                string body = " cast(avg(isnull(a.cpdp_yearsalary,0)) as decimal(18,2)) as 职级职档年薪," +
                              " cast(avg(isnull(a.yearSalaryMoney,0)) as decimal(18,2)) as 年终奖扣减," +
                              " cast(avg(isnull(a.bk_basesalary,0)) as decimal(18,2)) as 基本工资," +
                              " cast(sum(isnull(a.bk_basesalary,0)) as decimal(18,2)) as 基本工资汇总," +
                              " cast(sum(isnull(a.bk_basesalary,0)*isnull(a.regularwageRatio,0)/100) as decimal(18,2)) as 原始基本固定薪酬汇总," +
                              " cast(sum(isnull(a.bk_basesalary,0)*isnull(a.floatingwageRatio,0)/100) as decimal(18,2)) as 原始基本浮动薪酬汇总" +
                         
                          " FROM BK_MonthSalay_Rec  AS a ";
                      
            
            string sel;

            //sWhere = "b.Client_Code is not null and " + sWhere;

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
            SetDGV(this.dataGridView1);
        }

        private void SetDGV(DataGridView nowDGV)
        {
            //nowDGV.Columns[0].Visible = false;

            for (int i = 0; i < nowDGV.Columns.Count; i++)
            {
                //nowDGV.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;

                //nowDGV.Columns[i].ValueType System .Type .nowDGV.Columns[5].DefaultCellStyle.Alignment

                if ("Decimal" == nowDGV.Columns[i].ValueType.Name)
                {
                    nowDGV.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

                }



            }

        }

        //大类
        private void dalei_Click(object sender, EventArgs e)
        {
            //string sel = "SELECT GroupName as 大类名称,cast (GroupID as varchar(20)) as 大类代码 FROM YX_group_main";
            string sel = "SELECT distinct inspect_year as 年份 FROM ly_inspection_machine_View order by inspect_year desc";


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
            string sel = "SELECT distinct inspect_date as 部门全称 FROM ly_inspection_machine_View order by inspect_date desc"; 
            

            QueryForm queryForm = new QueryForm();


            queryForm.Sel = sel;
            queryForm.Constr = SQLDatabase.Connectstring;

            //Set the Column Collection to the filter Table
            //queryForm.SetSourceColumns(this.billMainDataSet.BalanceBill.Columns);

            queryForm.ShowDialog();

            this.textBoxvehicleNumber.Text = queryForm.Result;
        }

      

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

            NewFrm.Show(this);

            string header = " select ";
            string ender = " group by ";


            string sWhere = "  where  1 = 1 ";

            if (this.checkBox8.Checked)
            {

                sWhere = sWhere + "  and ( a.inspect_date >= '" + dateTimePicker1.Text + "' and a.inspect_date < '" + dateTimePicker2.Text + "') ";

            }

            header = header + this.gqis.MakeAllHeader();
            ender = ender + this.gqis.MakeAllheaderEnder();
            sWhere = sWhere + this.gqis.MakeAllWhere();

            if (ender.Length > 0)
            {
                ender = ender.Substring(0, ender.Length - 1);

            }
            else
            {
                ender = "";
            }

            string body0 = " cast(sum(isnull(a.inspect_count,0)) as decimal(18,0)) as 送检数量," +
                              " cast(sum(isnull(a.qualified_count,0)) as decimal(18,0)) as 合格数量," +
                              " cast(sum(isnull(a.qualified_count,0))/sum(isnull(a.inspect_count,1))*100 as decimal(18,2)) as 合格率," +
                              " cast(sum(isnull(a.canuse_count,0)) as decimal(18,0)) as 回用数量," +
                              " cast(sum(isnull(a.canuse_count,0))/sum(isnull(a.inspect_count,1))*100 as decimal(18,2)) as 回用率," +
                              " cast(sum(isnull(a.remake_count,0)) as decimal(18,0)) as 返修数量," +
                              " cast(sum(isnull(a.remake_count,0))/sum(isnull(a.inspect_count,1))*100 as decimal(18,2)) as 返修率," +
                              " cast(sum(isnull(a.waste_count,0)) as decimal(18,0)) as 废品数量," +
                              " cast(sum(isnull(a.waste_count,0))/sum(isnull(a.inspect_count,1))*100 as decimal(18,2)) as 废品率";



            string body1 = ", cast(sum(isnull(a.prepare_time,0))/60 as decimal(18,2)) as 准终工时," +
                              " cast(sum(isnull(a.work_hours,0))/60 as decimal(18,2)) as 合格工时," +

                              " cast(isnull(sum(a.work_hours),0)/(isnull(sum(a.work_hours),1)" +
                              " +isnull(sum(a.canuse_hours),1)+isnull(sum(a.remake_hours),1)" +
                              " +isnull(sum(a.waste_hours),1))*100 as decimal(18,2)) as 合格率_工时, " +

                              " cast(sum(isnull(a.canuse_hours,0))/60 as decimal(18,2)) as 回用工时," +

                             " cast(isnull(sum(a.canuse_hours),0)/(isnull(sum(a.work_hours),1)" +
                              " +isnull(sum(a.canuse_hours),1)+isnull(sum(a.remake_hours),1)" +
                              " +isnull(sum(a.waste_hours),1))*100 as decimal(18,2)) as 回用率_工时, " +

                              " cast(sum(isnull(a.remake_hours,0))/60 as decimal(18,2)) as 返修工时," +

                              " cast(isnull(sum(a.remake_hours),0)/(isnull(sum(a.work_hours),1)" +
                              " +isnull(sum(a.canuse_hours),1)+isnull(sum(a.remake_hours),1)" +
                              " +isnull(sum(a.waste_hours),1))*100 as decimal(18,2)) as 返修率_工时, " +

                              " cast(sum(isnull(a.remake_hours_accumulation,0))/60 as decimal(18,2)) as 返修耗费工时," +
                              " cast(sum(isnull(a.waste_hours,0))/60 as decimal(18,2)) as 废品工时," +

                              " cast(isnull(sum(a.waste_hours),0)/(isnull(sum(a.work_hours),1)" +
                              " +isnull(sum(a.canuse_hours),1)+isnull(sum(a.remake_hours),1)" +
                              " +isnull(sum(a.waste_hours),1))*100 as decimal(18,2)) as 废品率_工时, " +

                              " cast(sum(isnull(a.waste_accumulation_hours,0))/60 as decimal(18,2)) as 废品前序工时," +
                              " cast(sum(isnull(a.all_work_hours,0))/60 as decimal(18,2)) as 总工时";


            string body2 = ", cast(sum(isnull(a.prepare_money,0)) as decimal(18,2)) as 准终工价," +
                              " cast(sum(isnull(a.work_hours_money,0)) as decimal(18,2)) as 合格工价," +
                              " cast(sum(isnull(a.canuse_hours_money,0)) as decimal(18,2)) as 回用工价," +
                              " cast(sum(isnull(a.remake_hours_money,0)) as decimal(18,2)) as 返修工价," +
                              " cast(sum(isnull(a.remake_hours_accumulation_money,0)) as decimal(18,2)) as 返修耗费工价," +
                              " cast(sum(isnull(a.waste_hours_money,0)) as decimal(18,2)) as 废品工价," +
                              " cast(sum(isnull(a.waste_accumulation_hours_money,0)) as decimal(18,2)) as 废品前序工价," +
                              " cast(sum(isnull(a.waste_count_money,0)) as decimal(18,2)) as 废品成本," +
                              " cast(sum(isnull(a.all_work_money,0)) as decimal(18,2)) as 总工价";



            string body3 = " FROM ly_inspection_machine_View  AS a ";


            string body = body0;

            if (this.checkBox10.Checked)
            {
                body = body + body1;
            }

            if (this.checkBox11.Checked)
            {
                body = body + body2;
            }

            body = body + body3;
            string sel;

           

            if (ender.Length != 9)
            {

                sel = header + body + sWhere + ender;
            }
            else
            {
                sel = header + body + sWhere;

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
            SetDGV(this.dataGridView1);


            //return;
            
            ////SumQuery qds;
            ////qds = new SumQuery();

            //HappyYF.YuanXin.Data.BK_BaseSalary qds;
            //qds = new HappyYF.YuanXin.Data.BK_BaseSalary();

            

            //SortForm DataSort = new SortForm();
            ////DataGridView dv = (DataGridView)DataSort.Controls["sortTableDataGridView"];

            //DataGridView dv = DataSort.sortTableDataGridView;
            //dv.Columns[1].Visible = false;



            //DataSort.Text = "分组设置";

            //List<string> ls = new List<string>();
            //ls.Add("id");
           
            //DataSort.SetSortColumns(qds.SMXBK_HumanMain_MonthSalry.Columns,ls );
            //DataSort.ShowDialog();

            //string groupStr = DataSort.GetSortString();

            //////////////
            //if (null != groupStr)
            //{

            //    string[] result = groupStr.Split(new Char[] { ',' });

            //    string header = " select ";
            //    string ender = " group by ";

            //    string sWhere = "  where ( 1 = 1) ";

            //    if (this.checkBox8.Checked)
            //    {

            //        sWhere = sWhere + "  and ( a.pay_date >= '" + dateTimePicker1.Text + "' and a.pay_date <= '" + dateTimePicker2.Text + "') ";


            //    }


            //    //if (sWhere.Length > 3)
            //    //{
            //    //    sWhere = " where " + sWhere.Substring(0, sWhere.Length - 3);

            //    //}
            //    //else
            //    //{
            //    //    sWhere = "";
            //    //}


            //    if (result.Length > 0)
            //    {
            //        for (int i = 0; i < result.Length; i++)
            //        {
            //            header = header + selstr(result[i]) + ",";

            //            if (i < result.Length - 1)
            //            {
            //                ender = ender + grustr(result[i]) + ",";
            //            }
            //            else
            //            {
            //                ender = ender + grustr(result[i]);
            //            }

            //        }
            //    }




            //    string body = " sum( isnull(a.shangye,0)+isnull(a.shangye_tail,0))  as 商业险, " +
            //                  " sum( isnull(a.shangye,0)*isnull(a.shangye_dis,0)/100+isnull(a.shangye_tail,0)*isnull(a.shangye_tail_dis,0)/100)  as 商业返点, " +
            //                  " sum(isnull(a.jiaoqiang,0)+ isnull(a.jiaoqiang_tail,0)) as 交强险, " +
            //                    " sum(isnull(a.jiaoqiang,0)*isnull(a.jiaoqiang_dis,0)/100+ isnull(a.jiaoqiang_tail,0)*isnull(a.jiaoqiang_tail_dis,0)/100) as 交强返点, " +
            //                  " sum( isnull(a.huowu,0)+isnull(a.huowu_tail,0))  as 货物险, " +
            //                   " sum( isnull(a.huowu,0)*isnull(a.huowu_dis,0)/100+isnull(a.huowu_tail,0)*isnull(a.huowu_tail_dis,0)/100)  as 货物返点, " +
            //                  " sum(isnull(a.chechuan,0)+isnull(a.chechuan_tail,0)) as 车船税, " +
            //                    " sum(isnull(a.chechuan,0)*isnull(a.chechuan_dis,0)/100 + isnull(a.chechuan_tail,0)*isnull(a.chechuan_tail_dis,0)/100) as 车船返点, " +
            //                  " sum( isnull(a.shangye,0) + isnull(a.shangye_tail,0)+ isnull(a.jiaoqiang,0)+ isnull(a.jiaoqiang_tail,0)+isnull(a.huowu,0)+isnull(a.huowu_tail,0) + isnull(a.chechuan,0)+ isnull(a.chechuan_tail,0)) as 保险总额 " +
            //        //"  sum(isnull(a.accrual_money,0))  as 利息, " +
            //        //" sum(isnull(a.compensate_accrual_money,0)) as 还息, " +
            //        //" ( sum(isnull( a.accrual_money,0)) -    sum(isnull(a.compensate_accrual_money,0))) as 欠息," +
            //        //" left(max(convert(varchar(20),accrual_date,120)),10)  as 计息日期" +
            //              " FROM HT_Insurance_Item AS a LEFT OUTER JOIN " +
            //              "  XD_Client_Loan AS b ON a.loan_id = b.Id " +
            //              " LEFT OUTER JOIN " +
            //              " YX_client AS c on b.Client_Code = c.Card_number ";
                
            //    string sel;


            //    if (result.Length > 0)
            //        sel = header + body + sWhere + ender;
            //    else
            //        sel = header + body + sWhere;

            //    SqlDataAdapter myAdapter = new SqlDataAdapter(sel, SQLDatabase .Connectstring );

            //    //DataSet allData = new DataSet();


            //    //myAdapter.Fill(allData);

            //    if (null != ds)
            //        ds.Dispose();

            //    ds = new DataSet();

            //    myAdapter.Fill(ds);
            //    myAdapter.Dispose();

            //    //this.dataGridView1.DataSource = allData;


            //    this.dataGridView1.Columns.Clear();

            //    this.dataGridView1.AutoGenerateColumns = true;
            //    dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCellsExceptHeaders;

            //    //BindingSource bindingSource1 = new BindingSource();



            //    //DataTableReader reader = new DataTableReader(allData.Tables[0]);

            //    //this.billMainDataSet.BalanceBill.Clear();
            //    //this.billMainDataSet.BalanceBill.Load(reader);
            //    //ds = allData;
            //    if (null != bindingSource2)
            //        this.bindingSource2.Dispose();

            //    this.bindingSource2 = new BindingSource();
            //    this.bindingSource2.DataSource = ds.Tables[0];
            //    dataGridView1.DataSource = bindingSource2;

            //    this.bindingNavigator1.BindingSource = bindingSource2;

            //    bindingSource2.ResetBindings(true);

            //    AddSummationRow(bindingSource2, dataGridView1);

            //}
            NewFrm.Hide(this);
        }

        private void kahao_Click(object sender, EventArgs e)
        {

            string sel = "SELECT distinct itemno as 工件编号,mch as 工件名称 FROM ly_inspection_machine_View  order by itemno";


            QueryForm queryForm = new QueryForm();


            queryForm.Sel = sel;
            queryForm.Constr = SQLDatabase.Connectstring;

            //Set the Column Collection to the filter Table
            //queryForm.SetSourceColumns(this.billMainDataSet.BalanceBill.Columns);

            queryForm.ShowDialog();

            this.textBoxclientNumber.Text = queryForm.Result;
        }

        
        //private void xiaofei_Click(object sender, EventArgs e)
        //{
        //    string sel = "SELECT distinct consumer_class as 消费类别  FROM YX_daywork_record";


        //    QueryForm queryForm = new QueryForm();


        //    queryForm.Sel = sel;
        //    queryForm.Constr = SQLDatabase.Connectstring;

        //    //Set the Column Collection to the filter Table
        //    //queryForm.SetSourceColumns(this.billMainDataSet.BalanceBill.Columns);

        //    queryForm.ShowDialog();

        //    this.textBoxleftDays.Text = queryForm.Result;
        //}

        private void taocan_Click(object sender, EventArgs e)
        {

            string sel = "SELECT distinct inspect_month as 月份 FROM ly_inspection_machine_View order by inspect_month";

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
           // return;
            
            //this.yX_daywork_recordDataGridView1.Columns[6].summation
            //this.dailyoperation.YX_daywork_record.Columns[7].
            if (null == dg.CurrentRow) return;

              //" cast(sum(isnull(a.bk_basesalary,0)) as decimal(18,2)) as 基本工资汇总," +
              //                " cast(sum(isnull(a.bk_basesalary,0)*isnull(a.regularwageRatio,0)/100) as decimal(18,2)) as 固定工资汇总," +
              //                " cast(sum(isnull(a.bk_basesalary,0)*isnull(a.floatingwageRatio,0)/100) as decimal(18,2)) as 浮动工资汇总" +

            decimal sum_songjian = 0;

            decimal sum_hege = 0;
            decimal sum_hegelv = 0;
            decimal sum_hege_gs = 0;
            decimal sum_hegegj = 0;

            decimal sum_huiyong = 0;
            decimal sum_huiyonglv = 0;
            decimal sum_huiyong_gs = 0;
            decimal sum_huiyonggj = 0;

            decimal sum_fanxiu = 0;
            decimal sum_fanxiulv = 0;
            decimal sum_fanxiu_gs = 0;
            decimal sum_fanxiugj = 0;

            decimal sum_fanxiu_xhgs = 0;
            decimal sum_fanxiu_xhgj = 0;


            decimal sum_feipin = 0;
            decimal sum_feipinlv = 0;
            decimal sum_feipings = 0;
            decimal sum_feipingj = 0;

            decimal sum_feipin_qvgs = 0;
            decimal sum_feipin_qvgj = 0;
            

            decimal sum_zhunzhong_gs = 0;
            decimal sum_zhunzhong_gj = 0;
            decimal sum_feipin_cb = 0;
           
           
            

            decimal sum_all_gs = 0;
            decimal sum_all_gj = 0;



           



            foreach (DataGridViewRow dr in dg.Rows)
            {
                if (System.DBNull.Value == dr.Cells["送检数量"].Value)
                    sum_songjian = sum_songjian + 0;
                else
                    sum_songjian = sum_songjian + decimal.Parse(dr.Cells["送检数量"].Value.ToString());

                if (System.DBNull.Value == dr.Cells["合格数量"].Value)
                    sum_hege = sum_hege + 0;
                else
                    sum_hege = sum_hege + decimal.Parse(dr.Cells["合格数量"].Value.ToString());

                if (System.DBNull.Value == dr.Cells["回用数量"].Value)
                    sum_huiyong = sum_huiyong + 0;
                else
                    sum_huiyong = sum_huiyong + decimal.Parse(dr.Cells["回用数量"].Value.ToString());

                if (System.DBNull.Value == dr.Cells["返修数量"].Value)
                    sum_fanxiu = sum_fanxiu + 0;
                else
                    sum_fanxiu = sum_fanxiu + decimal.Parse(dr.Cells["返修数量"].Value.ToString());

                if (System.DBNull.Value == dr.Cells["废品数量"].Value)
                    sum_feipin = sum_feipin + 0;
                else
                    sum_feipin = sum_feipin + decimal.Parse(dr.Cells["废品数量"].Value.ToString());


              

             

            }
            sum_hegelv = sum_hege / sum_songjian;
            sum_huiyonglv = sum_huiyong / sum_songjian;
            sum_fanxiulv = sum_fanxiu / sum_songjian;
            sum_feipinlv = sum_feipin / sum_songjian;



            if (this.checkBox10.Checked)
            {
                foreach (DataGridViewRow dr in dg.Rows)
                {

                    if (System.DBNull.Value == dr.Cells["准终工时"].Value)
                        sum_zhunzhong_gs = sum_zhunzhong_gs + 0;
                    else
                        sum_zhunzhong_gs = sum_hege_gs + decimal.Parse(dr.Cells["准终工时"].Value.ToString());


                    if (System.DBNull.Value == dr.Cells["合格工时"].Value)
                        sum_hege_gs = sum_hege_gs + 0;
                    else
                        sum_hege_gs = sum_hege_gs + decimal.Parse(dr.Cells["合格工时"].Value.ToString());


                    if (System.DBNull.Value == dr.Cells["回用工时"].Value)
                        sum_huiyong_gs = sum_huiyong_gs + 0;
                    else
                        sum_huiyong_gs = sum_huiyong_gs + decimal.Parse(dr.Cells["回用工时"].Value.ToString());

                    if (System.DBNull.Value == dr.Cells["返修工时"].Value)
                        sum_fanxiu_gs = sum_fanxiu_gs + 0;
                    else
                        sum_fanxiu_gs = sum_fanxiu_gs + decimal.Parse(dr.Cells["返修工时"].Value.ToString());

                    if (System.DBNull.Value == dr.Cells["返修耗费工时"].Value)
                        sum_fanxiu_xhgs = sum_fanxiu_xhgs + 0;
                    else
                        sum_fanxiu_xhgs = sum_fanxiu_xhgs + decimal.Parse(dr.Cells["返修耗费工时"].Value.ToString());

                    if (System.DBNull.Value == dr.Cells["废品工时"].Value)
                        sum_feipings = sum_feipings + 0;
                    else
                        sum_feipings = sum_feipings + decimal.Parse(dr.Cells["废品工时"].Value.ToString());

                    if (System.DBNull.Value == dr.Cells["废品前序工时"].Value)
                        sum_feipin_qvgs = sum_feipin_qvgs + 0;
                    else
                        sum_feipin_qvgs = sum_feipin_qvgs + decimal.Parse(dr.Cells["废品前序工时"].Value.ToString());

                    if (System.DBNull.Value == dr.Cells["总工时"].Value)
                        sum_all_gs = sum_all_gs + 0;
                    else
                        sum_all_gs = sum_all_gs + decimal.Parse(dr.Cells["总工时"].Value.ToString());
                }
            
            
            }

            if (this.checkBox11.Checked)
            {
                foreach (DataGridViewRow dr in dg.Rows)
                {
                    if (System.DBNull.Value == dr.Cells["准终工价"].Value)
                        sum_zhunzhong_gj = sum_zhunzhong_gj + 0;
                    else
                        sum_zhunzhong_gj = sum_zhunzhong_gj + decimal.Parse(dr.Cells["准终工价"].Value.ToString());

                    if (System.DBNull.Value == dr.Cells["合格工价"].Value)
                        sum_hegegj = sum_hegegj + 0;
                    else
                        sum_hegegj = sum_hegegj + decimal.Parse(dr.Cells["合格工价"].Value.ToString());

                    if (System.DBNull.Value == dr.Cells["回用工价"].Value)
                        sum_huiyonggj = sum_huiyonggj + 0;
                    else
                        sum_huiyonggj = sum_huiyonggj + decimal.Parse(dr.Cells["回用工价"].Value.ToString());

                    if (System.DBNull.Value == dr.Cells["返修耗费工价"].Value)
                        sum_fanxiu_xhgj = sum_fanxiu_xhgj + 0;
                    else
                        sum_fanxiu_xhgj = sum_fanxiu_xhgj + decimal.Parse(dr.Cells["返修耗费工价"].Value.ToString());

                    if (System.DBNull.Value == dr.Cells["返修工价"].Value)
                        sum_fanxiugj = sum_fanxiugj + 0;
                    else
                        sum_fanxiugj = sum_fanxiugj + decimal.Parse(dr.Cells["返修工价"].Value.ToString());

                     if (System.DBNull.Value == dr.Cells["废品工价"].Value)
                         sum_feipingj = sum_feipingj + 0;
                    else
                         sum_feipingj = sum_feipingj + decimal.Parse(dr.Cells["废品工价"].Value.ToString());

                      if (System.DBNull.Value == dr.Cells["废品前序工价"].Value)
                          sum_feipin_qvgj = sum_feipin_qvgj + 0;
                    else
                          sum_feipin_qvgj = sum_feipin_qvgj + decimal.Parse(dr.Cells["废品前序工价"].Value.ToString());

                      if (System.DBNull.Value == dr.Cells["废品成本"].Value)
                          sum_feipin_cb = sum_feipin_cb + 0;
                      else
                          sum_feipin_cb = sum_feipin_cb + decimal.Parse(dr.Cells["废品成本"].Value.ToString());

                      if (System.DBNull.Value == dr.Cells["总工价"].Value)
                          sum_all_gj = sum_all_gj + 0;
                      else
                          sum_all_gj = sum_all_gj + decimal.Parse(dr.Cells["总工价"].Value.ToString());
                }


            }


            bs.AddNew();


            dg.CurrentRow.Cells["送检数量"].Value = sum_songjian;
            dg.CurrentRow.Cells["合格数量"].Value = sum_hege;
            dg.CurrentRow.Cells["回用数量"].Value = sum_huiyong;
            dg.CurrentRow.Cells["返修数量"].Value = sum_fanxiu;
            dg.CurrentRow.Cells["废品数量"].Value = sum_feipin;

            dg.CurrentRow.Cells["合格率"].Value = decimal.Round((sum_hegelv*100),2);
            dg.CurrentRow.Cells["回用率"].Value = decimal.Round((sum_huiyonglv*100),2);
            dg.CurrentRow.Cells["返修率"].Value = decimal.Round((sum_fanxiulv*100),2);
            dg.CurrentRow.Cells["废品率"].Value = decimal.Round((sum_feipinlv * 100), 2);


            decimal temp_ll;
            if (this.checkBox10.Checked)
            {
                dg.CurrentRow.Cells["准终工时"].Value = sum_zhunzhong_gs;
                dg.CurrentRow.Cells["合格工时"].Value = sum_hege_gs;

              
                dg.CurrentRow.Cells["回用工时"].Value = sum_huiyong_gs;
                dg.CurrentRow.Cells["返修工时"].Value = sum_fanxiu_gs;
                dg.CurrentRow.Cells["返修耗费工时"].Value = sum_fanxiu_xhgs;
                dg.CurrentRow.Cells["废品工时"].Value = sum_feipings;
                dg.CurrentRow.Cells["废品前序工时"].Value = sum_feipin_qvgs;
                dg.CurrentRow.Cells["总工时"].Value = sum_all_gs;

                if (sum_hege_gs <= 0)
                {
                    temp_ll = 1;

                }
                else
                {
                    temp_ll = sum_hege_gs;
                }
                dg.CurrentRow.Cells["合格率_工时"].Value = decimal.Round(sum_hege_gs / (temp_ll + sum_huiyong_gs + sum_fanxiu_gs + sum_feipings) * 100, 2);
                if (sum_huiyong_gs <= 0)
                {
                    temp_ll =1;

                }
                else
                {
                    temp_ll = sum_huiyong_gs;
                }
                dg.CurrentRow.Cells["回用率_工时"].Value = decimal.Round(sum_huiyong_gs / (sum_hege_gs + temp_ll + sum_fanxiu_gs + sum_feipings) * 100, 2);
                if (sum_fanxiu_gs <= 0)
                {
                    temp_ll =1;

                }
                else
                {
                    temp_ll = sum_fanxiu_gs;
                }
                dg.CurrentRow.Cells["返修率_工时"].Value = decimal.Round(sum_fanxiu_gs / (sum_hege_gs + sum_huiyong_gs + temp_ll + sum_feipings) * 100, 2);
                if (sum_feipings <= 0)
                {
                    temp_ll = 1;

                }
                else
                {
                    temp_ll = sum_feipings;
                }




                dg.CurrentRow.Cells["废品率_工时"].Value = decimal.Round(sum_feipings / (sum_hege_gs + sum_huiyong_gs + sum_fanxiu_gs + temp_ll) * 100, 2);



            }

            if (this.checkBox11.Checked)
            {
                dg.CurrentRow.Cells["准终工价"].Value = sum_zhunzhong_gj;
                dg.CurrentRow.Cells["合格工价"].Value = sum_hegegj;
                dg.CurrentRow.Cells["回用工价"].Value = sum_huiyonggj;
                dg.CurrentRow.Cells["返修工价"].Value = sum_fanxiugj;
                dg.CurrentRow.Cells["返修耗费工价"].Value = sum_fanxiu_xhgj;
                dg.CurrentRow.Cells["废品工价"].Value = sum_feipingj;
                dg.CurrentRow.Cells["废品前序工价"].Value = sum_feipin_qvgj;
                dg.CurrentRow.Cells["废品成本"].Value = sum_feipin_cb;
                dg.CurrentRow.Cells["总工价"].Value = sum_all_gj;


            }

            bs.EndEdit();

            bs.Position = 0;




        }

        

        private void chehao_Click(object sender, EventArgs e)
        {
            string sel = "SELECT distinct process_order as 工序 FROM ly_inspection_machine_View  order by process_order";


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

        private void SumAnalysis_Load(object sender, EventArgs e)
        {
            gqis = new GroupQueryItemSelector(this.dataGridView2, this.Text);
            gqis.DgvBindList();
        }

        private void loanstylebutton_Click(object sender, EventArgs e)
        {
            string sel = "SELECT distinct material_plan_num as 生产计划,remark as 计划说明 FROM ly_inspection_machine_View  order by material_plan_num";
            QueryForm queryForm = new QueryForm();


            queryForm.Sel = sel;
            queryForm.Constr = SQLDatabase.Connectstring;

            //Set the Column Collection to the filter Table
            //queryForm.SetSourceColumns(this.billMainDataSet.BalanceBill.Columns);

            queryForm.ShowDialog();

            this.loanstyletextBox.Text = queryForm.Result;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string sel = "SELECT distinct inspect_source as 检验类别 FROM ly_inspection_machine_View ";


            QueryForm queryForm = new QueryForm();


            queryForm.Sel = sel;
            queryForm.Constr = SQLDatabase.Connectstring;

            //Set the Column Collection to the filter Table
            //queryForm.SetSourceColumns(this.billMainDataSet.BalanceBill.Columns);

            queryForm.ShowDialog();

            this.textBox1.Text = queryForm.Result;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string sel = "SELECT distinct pruductionOrder_num as 跟单编号 FROM ly_inspection_machine_View  order by pruductionOrder_num";


            QueryForm queryForm = new QueryForm();


            queryForm.Sel = sel;
            queryForm.Constr = SQLDatabase.Connectstring;

            //Set the Column Collection to the filter Table
            //queryForm.SetSourceColumns(this.billMainDataSet.BalanceBill.Columns);

            queryForm.ShowDialog();

            this.textBox4.Text = queryForm.Result;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            string sel = "SELECT distinct operator as 调度 FROM ly_inspection_machine_View  ";


            QueryForm queryForm = new QueryForm();


            queryForm.Sel = sel;
            queryForm.Constr = SQLDatabase.Connectstring;

            //Set the Column Collection to the filter Table
            //queryForm.SetSourceColumns(this.billMainDataSet.BalanceBill.Columns);

            queryForm.ShowDialog();

            this.textBox5.Text = queryForm.Result;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string sel = "SELECT distinct sequence_name as 工艺名称 FROM ly_inspection_machine_View ";


            QueryForm queryForm = new QueryForm();


            queryForm.Sel = sel;
            queryForm.Constr = SQLDatabase.Connectstring;

            //Set the Column Collection to the filter Table
            //queryForm.SetSourceColumns(this.billMainDataSet.BalanceBill.Columns);

            queryForm.ShowDialog();

            this.textBox2.Text = queryForm.Result;
        }

        //private void button6_Click(object sender, EventArgs e)
        //{
        //    string sel = "SELECT distinct hmEdulevel as 学历 FROM BK_MonthSalay_Rec where YY_DataBase='" + SQLDatabase.nowUserName() + "'  order by hmEdulevel";


        //    QueryForm queryForm = new QueryForm();


        //    queryForm.Sel = sel;
        //    queryForm.Constr = SQLDatabase.Connectstring;

        //    //Set the Column Collection to the filter Table
        //    //queryForm.SetSourceColumns(this.billMainDataSet.BalanceBill.Columns);

        //    queryForm.ShowDialog();

        //    this.textBox9.Text = queryForm.Result;
        //}

        //private void button7_Click(object sender, EventArgs e)
        //{
        //    string sel = "SELECT distinct hmTechTitle as 学历 FROM BK_MonthSalay_Rec where YY_DataBase='" + SQLDatabase.nowUserName() + "'  order by hmTechTitle";


        //    QueryForm queryForm = new QueryForm();


        //    queryForm.Sel = sel;
        //    queryForm.Constr = SQLDatabase.Connectstring;

        //    //Set the Column Collection to the filter Table
        //    //queryForm.SetSourceColumns(this.billMainDataSet.BalanceBill.Columns);

        //    queryForm.ShowDialog();

        //    this.textBox10.Text = queryForm.Result;
        //}

        private void button8_Click(object sender, EventArgs e)
        {
            string sel = "SELECT distinct process_style as 生产方式 FROM ly_inspection_machine_View  order by process_style";


            QueryForm queryForm = new QueryForm();


            queryForm.Sel = sel;
            queryForm.Constr = SQLDatabase.Connectstring;

            //Set the Column Collection to the filter Table
            //queryForm.SetSourceColumns(this.billMainDataSet.BalanceBill.Columns);

            queryForm.ShowDialog();

            this.textBox11.Text = queryForm.Result;
        }

        //private void button9_Click(object sender, EventArgs e)
        //{
        //    string sel = "SELECT distinct cpID as 职级 FROM BK_MonthSalay_Rec where YY_DataBase='" + SQLDatabase.nowUserName() + "'  order by cpID";


        //    QueryForm queryForm = new QueryForm();


        //    queryForm.Sel = sel;
        //    queryForm.Constr = SQLDatabase.Connectstring;

        //    //Set the Column Collection to the filter Table
        //    //queryForm.SetSourceColumns(this.billMainDataSet.BalanceBill.Columns);

        //    queryForm.ShowDialog();

        //    this.textBox14.Text = queryForm.Result;
        //}

        //private void button10_Click(object sender, EventArgs e)
        //{
        //    string sel = "SELECT distinct dpID as 职档 FROM BK_MonthSalay_Rec where YY_DataBase='" + SQLDatabase.nowUserName() + "'  order by dpID";


        //    QueryForm queryForm = new QueryForm();


        //    queryForm.Sel = sel;
        //    queryForm.Constr = SQLDatabase.Connectstring;

        //    //Set the Column Collection to the filter Table
        //    //queryForm.SetSourceColumns(this.billMainDataSet.BalanceBill.Columns);

        //    queryForm.ShowDialog();

        //    this.textBox15.Text = queryForm.Result;
        //}

        //private void button11_Click(object sender, EventArgs e)
        //{
        //    string sel = "SELECT distinct countstyle as 计算方式 FROM BK_MonthSalay_Rec where YY_DataBase='" + SQLDatabase.nowUserName() + "'  order by countstyle";


        //    QueryForm queryForm = new QueryForm();


        //    queryForm.Sel = sel;
        //    queryForm.Constr = SQLDatabase.Connectstring;

        //    //Set the Column Collection to the filter Table
        //    //queryForm.SetSourceColumns(this.billMainDataSet.BalanceBill.Columns);

        //    queryForm.ShowDialog();

        //    this.textBox16.Text = queryForm.Result;
        //}

        private void checkBoxinsuranceCompany_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox ckb = sender as CheckBox;

            string gq_DatacolumnName="";
            string gq_DatatableName="a";
            Control nowcontrol=null;

            string gq_Name = ckb.Text.Substring(0, ckb.Text.Length - 1);

            switch (gq_Name)
            {
                case "检验类别":
                    gq_DatacolumnName = "inspect_source";
                     //gq_DatatableName;
                    nowcontrol = this.textBox1;
                break ;

                case "生产方式":
                     gq_DatacolumnName = "process_style";
                    //gq_DatatableName;
                     nowcontrol = this.textBox11;
                break;

                case "生产计划":
                gq_DatacolumnName = "material_plan_num";
                    //gq_DatatableName;
                nowcontrol = this.loanstyletextBox;
                break;

                case "跟单编号":
                gq_DatacolumnName = "pruductionOrder_num";
                //gq_DatatableName;
                nowcontrol = this.textBox4;
                break;

                case "调度":
                gq_DatacolumnName = "operator";
                //gq_DatatableName;
                nowcontrol = this.textBox5;
                break;
                case "检验单号":
                gq_DatacolumnName = "pruductionInspection_num";
                //gq_DatatableName;
                nowcontrol = this.textBoxpayDate;
                break;
                case "年份":
                gq_DatacolumnName = "inspect_year";
                //gq_DatatableName;
                nowcontrol = this.textBoxinsuranceComPany;
                break;
                case "月份":
                gq_DatacolumnName = "inspect_month";
                //gq_DatatableName;
                nowcontrol = this.textBoxbillNumber;
                break;
                case "检验日期":
                gq_DatacolumnName = "inspect_date";
                //gq_DatatableName;
                nowcontrol = this.textBoxvehicleNumber;
                break;
                case "质检员":
                gq_DatacolumnName = "quality_inspector";
                //gq_DatatableName;
                nowcontrol = this.textBoxtermDate;
                break;
                case "工件编号":
                gq_DatacolumnName = "itemno";
                //gq_DatatableName;
                nowcontrol = this.textBoxclientNumber;
                break;
                case "中方型号":
                gq_DatacolumnName = "xhc";
                //gq_DatatableName;
                nowcontrol = this.textBox6;
                break;
                case "加工工序":
                gq_DatacolumnName = "process_order";
                //gq_DatatableName;
                nowcontrol = this.textBoxclientName;
                break;
                case "工艺名称":
                gq_DatacolumnName = "sequence_name";
                //gq_DatatableName;
                nowcontrol = this.textBox2;
                break;
                case "工人":
                gq_DatacolumnName = "work_name";
                //gq_DatatableName;
                nowcontrol = this.textBox3;
                break;
                case "质检意见":
                gq_DatacolumnName = "qa_remark";
                //gq_DatatableName;
                nowcontrol = this.textBox7;
                break;

            default:
               break;
            }
        

            if ( true == ckb .Checked)
            {
                this.gqis.Add_Incontrol(new GroupQueryItem(gq_Name, gq_DatacolumnName, gq_DatatableName, nowcontrol));
            }
            else
            {


                this.gqis.Del_Incontrol(gq_Name);
            
            }
            //gqis.DgvBindList();
        }

        private void dataGridView2_SelectionChanged(object sender, EventArgs e)
        {
            DataGridView dgv = sender as DataGridView;

            if ((dgv.Rows.Count > 0) && (dgv.SelectedRows.Count > 0))
            {

                if (dgv.Rows.Count <= selectionIdx)
                    selectionIdx = dgv.Rows.Count - 1;
                dgv.Rows[selectionIdx].Selected = true;
                dgv.CurrentCell = dgv.Rows[selectionIdx].Cells[0];
            } 
        }

        private void dataGridView2_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex >= 0)
                selectionIdx = e.RowIndex;
        }

        private void dataGridView2_CellMouseMove(object sender, DataGridViewCellMouseEventArgs e)
        {
            DataGridView dgv = sender as DataGridView;
           
            if ((e.Clicks < 2) && (e.Button == MouseButtons.Left))
            {
                if ((e.ColumnIndex == -1) && (e.RowIndex > -1))
                    dgv.DoDragDrop(dgv.Rows[e.RowIndex], DragDropEffects.Move);
            } 
        }

        private void dataGridView2_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            //int idx = GetRowFromPoint(e.X, e.Y);

            //if (idx < 0) return;
            //dataGridView.Rows[idx].Selected = true;
            //dataGridView.CurrentCell = dataGridView.Rows[idx].Cells[0];
        }

        private int GetRowFromPoint(int x, int y)
        {
            for (int i = 0; i < dataGridView2.RowCount; i++)
            {
                Rectangle rec = dataGridView2.GetRowDisplayRectangle(i, false);

                if (dataGridView2.RectangleToScreen(rec).Contains(x, y))
                    return i;
            }

            return -1;
        }

        private int index2 = 0;
        private void dataGridView2_DragDrop(object sender, DragEventArgs e)
        {
            DataGridView dgv = sender as DataGridView;

            //int idx = GetRowFromPoint(e.X, e.Y);

            //if (idx < 0) return;

            //if (e.Data.GetDataPresent(typeof(DataGridViewRow)))
            //{
            //    DataGridViewRow row = (DataGridViewRow)e.Data.GetData(typeof(DataGridViewRow));
            //    dgv.Rows.Remove(row);
            //    dgv.Rows.Insert(idx, row);
            //    selectionIdx = idx;
            //}

            int idx = GetRowFromPoint(e.X, e.Y);
            if (idx < 0) return;
            index2 = idx;
            if (e.Data.GetDataPresent(typeof(DataGridViewRow)))
            {
                //DataGridViewRow row = (DataGridViewRow)e.Data.GetData(typeof(DataGridViewRow));
                //dataGridView.Rows.Remove(row);
                //dataGridView.Rows.Insert(idx, row);
                //selectionIdx = idx;
                DataGridViewRow row = (DataGridViewRow)e.Data.GetData(typeof(DataGridViewRow));

                //DataRow nr = ((DataTable)dgv.DataSource).NewRow();
                //for (int i = 0; i < row.Cells.Count; i++)
                //{
                //    nr[i] = row.Cells[i].Value;
                //}
                //dgv.Rows.Remove(row);
                //((DataTable)dgv.DataSource).Rows.InsertAt(nr, idx);
                this.gqis.Ins_Incontrol(idx, row.Cells[0].Value.ToString());
                selectionIdx = idx;
            } 
        }

        private void dataGridView2_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move; 
        }

        private void payDate_Click(object sender, EventArgs e)
        {
            string sel = "SELECT distinct pruductionInspection_num as 检验单号 FROM ly_inspection_machine_View  order by pruductionInspection_num";


            QueryForm queryForm = new QueryForm();


            queryForm.Sel = sel;
            queryForm.Constr = SQLDatabase.Connectstring;

            //Set the Column Collection to the filter Table
            //queryForm.SetSourceColumns(this.billMainDataSet.BalanceBill.Columns);

            queryForm.ShowDialog();

            this.textBoxpayDate.Text = queryForm.Result;
        }

        private void termDate_Click(object sender, EventArgs e)
        {
            string sel = "SELECT distinct quality_inspector as 质检员 FROM ly_inspection_machine_View  order by quality_inspector";


            QueryForm queryForm = new QueryForm();


            queryForm.Sel = sel;
            queryForm.Constr = SQLDatabase.Connectstring;

            //Set the Column Collection to the filter Table
            //queryForm.SetSourceColumns(this.billMainDataSet.BalanceBill.Columns);

            queryForm.ShowDialog();

            this.textBoxtermDate.Text = queryForm.Result;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            string sel = "SELECT distinct xhc as 中方型号 FROM ly_inspection_machine_View  order by xhc";


            QueryForm queryForm = new QueryForm();


            queryForm.Sel = sel;
            queryForm.Constr = SQLDatabase.Connectstring;

            //Set the Column Collection to the filter Table
            //queryForm.SetSourceColumns(this.billMainDataSet.BalanceBill.Columns);

            queryForm.ShowDialog();

            this.textBox6.Text = queryForm.Result;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string sel = "SELECT distinct work_name as 姓名,work_code as 编号 FROM ly_inspection_machine_View  order by work_code";


            QueryForm queryForm = new QueryForm();


            queryForm.Sel = sel;
            queryForm.Constr = SQLDatabase.Connectstring;

            //Set the Column Collection to the filter Table
            //queryForm.SetSourceColumns(this.billMainDataSet.BalanceBill.Columns);

            queryForm.ShowDialog();

            this.textBox3.Text = queryForm.Result;
        }

        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            DataGridView dgv = sender as DataGridView;
            if (e.RowIndex > -1)
            {
                if ((dgv.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.GetType() == typeof(decimal)
                      || dgv.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.GetType() == typeof(double)
                      || dgv.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.GetType() == typeof(int)))
                {
                    if (Convert.ToDecimal(dgv.Rows[e.RowIndex].Cells[e.ColumnIndex].Value) == 0)
                    {
                        e.Value = System.DBNull.Value;
                    }
                }
            }       
        }
       
    }
}
