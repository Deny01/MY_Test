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
    public partial class SumAnalysis : Form
    {
         private DataSet ds;
        BindingSource bindingSource2;

        GroupQueryItemSelector gqis;
        private int selectionIdx = 0;

        
        public SumAnalysis()
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

            string sWhere = "  where  YY_DataBase = '" + SQLDatabase .nowUserName() + "'";
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


            //////////////////////////////////////////////////

            if (this.checkBoxleftDays.Checked)
            {
                header = header + "  count(*)   as 人数,";
                //ender = ender + "  count(*),";

                if (textBoxleftDays.Text.Replace(" ", "").Length > 0)
                {

                    sWhere = sWhere + " and  ( count(*)  = '" + textBoxleftDays.Text + "') ";


                }



            }


            //////////////////////////////////////////////////
            if (this.checkBox14.Checked)
            {
                header = header + "  a.provescore   as 证书分数,";
                ender = ender + "  a.provescore,";

                if (textBox12.Text.Replace(" ", "").Length > 0)
                {

                    sWhere = sWhere + " and  ( a.provescore  = '" + textBox12.Text + "') ";


                }



            }


            //////////////////////////////////////////////////

            if (this.checkBox15.Checked)
            {
                header = header + "  a.performancescore   as 绩效分数,";
                ender = ender + "  a.performancescore,";

                if (textBox13.Text.Replace(" ", "").Length > 0)
                {

                    sWhere = sWhere + " and  ( a.performancescore  = '" + textBox13.Text + "') ";


                }



            }

            //////////////////////////////////////////////////

            if (this.checkBox7.Checked)
            {
                header = header + "  a.hmWorkyear   as 工龄,";
                ender = ender + "  a.hmWorkyear,";

                if (textBox6.Text.Replace(" ", "").Length > 0)
                {

                    sWhere = sWhere + " and  ( a.hmWorkyear  = '" + textBox6.Text + "') ";


                }



            }

            //////////////////////////////////////////////////

            if (this.checkBox9.Checked)
            {
                header = header + "  a.hminbankyear   as 业龄,";
                ender = ender + "  a.hminbankyear,";

                if (textBox7.Text.Replace(" ", "").Length > 0)
                {

                    sWhere = sWhere + " and  ( a.hminbankyear  = '" + textBox7.Text + "') ";


                }



            }

            //////////////////////////////////////////////////

            if (this.checkBox10.Checked)
            {
                header = header + "  a.hmTradeyear   as 行龄,";
                ender = ender + "  a.hmTradeyear,";

                if (textBox8.Text.Replace(" ", "").Length > 0)
                {

                    sWhere = sWhere + " and  ( a.hmTradeyear  = '" + textBox8.Text + "') ";


                }



            }
            //////////////////////////////////////////////////

            if (this.checkBox11.Checked)
            {
                header = header + "  a.hmEdulevel   as 学历,";
                ender = ender + "  a.hmEdulevel,";

                if (textBox9.Text.Replace(" ", "").Length > 0)
                {

                    sWhere = sWhere + " and  ( a.hmEdulevel  = '" + textBox9.Text + "') ";


                }



            }

            //////////////////////////////////////////////////

            if (this.checkBox12.Checked)
            {
                header = header + "  a.hmTechTitle   as 职称,";
                ender = ender + "  a.hmTechTitle,";

                if (textBox10.Text.Replace(" ", "").Length > 0)
                {

                    sWhere = sWhere + " and  ( a.hmTechTitle  = '" + textBox10.Text + "') ";


                }



            }

            //////////////////////////////////////////////////

            if (this.checkBox13.Checked)
            {
                header = header + "  a.hmProfessionalqualification   as 专业资格,";
                ender = ender + "  a.hmProfessionalqualification,";

                if (textBox11.Text.Replace(" ", "").Length > 0)
                {

                    sWhere = sWhere + " and  ( a.hmProfessionalqualification  = '" + textBox11.Text + "') ";


                }



            }

            //////////////////////////////////////////////////

            if (this.checkBox16.Checked)
            {
                header = header + "  a.cpID   as 职级,";
                ender = ender + "  a.cpID,";

                if (textBox14.Text.Replace(" ", "").Length > 0)
                {

                    sWhere = sWhere + " and  ( a.cpID  = '" + textBox14.Text + "') ";


                }



            }

            //////////////////////////////////////////////////

            if (this.checkBox17.Checked)
            {
                header = header + "  a.dpID   as 职档,";
                ender = ender + "  a.dpID,";

                if (textBox15.Text.Replace(" ", "").Length > 0)
                {

                    sWhere = sWhere + " and  ( a.dpID  = '" + textBox15.Text + "') ";


                }



            }

            //////////////////////////////////////////////////

            if (this.checkBox18.Checked)
            {
                header = header + "  a.countstyle   as 计算方式,";
                ender = ender + "  a.countstyle,";

                if (textBox16.Text.Replace(" ", "").Length > 0)
                {

                    sWhere = sWhere + " and  ( a.countstyle  = '" + textBox16.Text + "') ";


                }



            }



            //    //////////////////////////////////////////////////


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
            string sel = "SELECT distinct year_code as 年份 FROM BK_MonthSalay_Rec where YY_DataBase='" + SQLDatabase.nowUserName() + "' order by year_code desc";


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
            string sel = "SELECT distinct upocName as 部门全称 FROM BK_MonthSalay_Rec where YY_DataBase='" + SQLDatabase.nowUserName() + "' order by upocName"; 
            

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



            string header = " select ";
            string ender = " group by ";


            // string sWhere = "  where ((a.service_number IS NULL) OR (a.service_number is not null)) ";

            string sWhere = "  where  YY_DataBase = '" + SQLDatabase.nowUserName() + "'";

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


            string body = " cast(avg(isnull(a.cpdp_yearsalary,0)) as decimal(18,2)) as 职级职档年薪," +
                              " cast(avg(isnull(a.yearSalaryMoney,0)) as decimal(18,2)) as 年终奖扣减," +
                              " cast(avg(isnull(a.bk_basesalary,0)) as decimal(18,2)) as 基本工资," +
                              " cast(sum(isnull(a.bk_basesalary,0)) as decimal(18,2)) as 基本工资汇总," +
                              " cast(sum(isnull(a.bk_basesalary,0)*isnull(a.regularwageRatio,0)/100) as decimal(18,2)) as 原始基本固定薪酬汇总," +
                              " cast(sum(isnull(a.bk_basesalary,0)*isnull(a.floatingwageRatio,0)/100) as decimal(18,2)) as 原始基本浮动薪酬汇总" +
               
                          " FROM BK_MonthSalay_Rec  AS a ";
            

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
        }

        private void kahao_Click(object sender, EventArgs e)
        {

            string sel = "SELECT distinct hmWorkNo as 工号,hmName as 姓名 FROM BK_MonthSalay_Rec where YY_DataBase='" + SQLDatabase.nowUserName() + "'  order by hmWorkNo";


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

            string sel = "SELECT distinct month_code as 月份 FROM BK_MonthSalay_Rec where YY_DataBase='" + SQLDatabase.nowUserName() + "' order by month_code";

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

            decimal sum_bk_basesalary = 0;
            decimal sum_regularwage = 0;
            decimal sum_floatingwage = 0;
            //decimal sum_accrual = 0;
            //decimal sum_compensate_accrual = 0;
            //decimal sum_left_accrual = 0;

            //decimal sum_shangye_dis = 0;
            //decimal sum_jiaoqiang_dis = 0;
            //decimal sum_huowu_dis = 0;
            //decimal sum_chechuan_dis = 0;
            //decimal sum_shangyeaddjiaoqiang = 0;



            foreach (DataGridViewRow dr in dg.Rows)
            {
                if (System.DBNull.Value == dr.Cells["基本工资汇总"].Value)
                    sum_bk_basesalary = sum_bk_basesalary + 0;
                else
                    sum_bk_basesalary = sum_bk_basesalary + decimal.Parse(dr.Cells["基本工资汇总"].Value.ToString());

                if (System.DBNull.Value == dr.Cells["原始基本固定薪酬汇总"].Value)
                    sum_regularwage = sum_regularwage + 0;
                else
                    sum_regularwage = sum_regularwage + decimal.Parse(dr.Cells["原始基本固定薪酬汇总"].Value.ToString());

                if (System.DBNull.Value == dr.Cells["原始基本浮动薪酬汇总"].Value)
                    sum_floatingwage = sum_floatingwage + 0;
                else
                    sum_floatingwage = sum_floatingwage + decimal.Parse(dr.Cells["原始基本浮动薪酬汇总"].Value.ToString());


                //if (System.DBNull.Value == dr.Cells["车船税"].Value)
                //    sum_accrual = sum_accrual + 0;
                //else
                //    sum_accrual = sum_accrual + decimal.Parse(dr.Cells["车船税"].Value.ToString());

                //if (System.DBNull.Value == dr.Cells["保险总额"].Value)
                //    sum_compensate_accrual = sum_compensate_accrual + 0;
                //else
                //    sum_compensate_accrual = sum_compensate_accrual + decimal.Parse(dr.Cells["保险总额"].Value.ToString());



                //if (System.DBNull.Value == dr.Cells["商业返点"].Value)
                //    sum_shangye_dis = sum_shangye_dis + 0;
                //else
                //    sum_shangye_dis = sum_shangye_dis + decimal.Parse(dr.Cells["商业返点"].Value.ToString());

                //if (System.DBNull.Value == dr.Cells["交强返点"].Value)
                //    sum_jiaoqiang_dis = sum_jiaoqiang_dis + 0;
                //else
                //    sum_jiaoqiang_dis = sum_jiaoqiang_dis + decimal.Parse(dr.Cells["交强返点"].Value.ToString());


                

                //     if (System.DBNull.Value == dr.Cells["商业加交强返点"].Value)
                //         sum_shangyeaddjiaoqiang = sum_shangyeaddjiaoqiang + 0;
                //else
                //         sum_shangyeaddjiaoqiang = sum_shangyeaddjiaoqiang + decimal.Parse(dr.Cells["商业加交强返点"].Value.ToString());

                //if (System.DBNull.Value == dr.Cells["货物返点"].Value)
                //    sum_huowu_dis = sum_huowu_dis + 0;
                //else
                //    sum_huowu_dis = sum_huowu_dis + decimal.Parse(dr.Cells["货物返点"].Value.ToString());

                //if (System.DBNull.Value == dr.Cells["车船返点"].Value)
                //    sum_chechuan_dis = sum_chechuan_dis + 0;
                //else
                //    sum_chechuan_dis = sum_chechuan_dis + decimal.Parse(dr.Cells["车船返点"].Value.ToString());

             

            }
            bs.AddNew();

            //decimal sum_shangye_dis = 0;
            //decimal sum_jiaoqiang_dis = 0;
            //decimal sum_huowu_dis = 0;
            //decimal sum_chechuan_dis = 0;
            dg.CurrentRow.Cells["基本工资汇总"].Value = sum_bk_basesalary;
            dg.CurrentRow.Cells["原始基本固定薪酬汇总"].Value = sum_regularwage;
            dg.CurrentRow.Cells["原始基本浮动薪酬汇总"].Value = sum_floatingwage;
            //dg.CurrentRow.Cells["车船返点"].Value = sum_chechuan_dis;
            //dg.CurrentRow.Cells["商业险"].Value = sum_principal;
            //dg.CurrentRow.Cells["交强险"].Value = sum_compensate_principal;
            //dg.CurrentRow.Cells["货物险"].Value = sum_left_principal;
            //dg.CurrentRow.Cells["车船税"].Value = sum_accrual;
            //dg.CurrentRow.Cells["保险总额"].Value = sum_compensate_accrual;
            //dg.CurrentRow.Cells["商业加交强返点"].Value = sum_shangyeaddjiaoqiang;

       

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
            string sel = "SELECT distinct hmName as 姓名,hmWorkNo as 工号 FROM BK_MonthSalay_Rec where YY_DataBase='" + SQLDatabase.nowUserName() + "'  order by hmName";


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
            string sel = "SELECT distinct osgName as 岗位 FROM BK_MonthSalay_Rec where YY_DataBase='" + SQLDatabase.nowUserName() + "'  order by osgName";
            QueryForm queryForm = new QueryForm();


            queryForm.Sel = sel;
            queryForm.Constr = SQLDatabase.Connectstring;

            //Set the Column Collection to the filter Table
            //queryForm.SetSourceColumns(this.billMainDataSet.BalanceBill.Columns);nowUserName

            queryForm.ShowDialog();

            this.loanstyletextBox.Text = queryForm.Result;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string sel = "SELECT distinct ocName as 部门 FROM BK_MonthSalay_Rec where YY_DataBase='" + SQLDatabase.nowUserName() + "'  order by ocName";


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
            string sel = "SELECT distinct categoryName as 岗位类别 FROM BK_MonthSalay_Rec where YY_DataBase='" + SQLDatabase.nowUserName() + "'  order by categoryName";


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
            string sel = "SELECT distinct sequenceName as 岗位序列 FROM BK_MonthSalay_Rec where YY_DataBase='" + SQLDatabase.nowUserName() + "'  order by sequenceName";


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
            string sel = "SELECT distinct hmGender as 性别 FROM BK_MonthSalay_Rec where YY_DataBase='" + SQLDatabase.nowUserName() + "'  order by hmGender";


            QueryForm queryForm = new QueryForm();


            queryForm.Sel = sel;
            queryForm.Constr = SQLDatabase.Connectstring;

            //Set the Column Collection to the filter Table
            //queryForm.SetSourceColumns(this.billMainDataSet.BalanceBill.Columns);

            queryForm.ShowDialog();

            this.textBox2.Text = queryForm.Result;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            string sel = "SELECT distinct hmEdulevel as 学历 FROM BK_MonthSalay_Rec where YY_DataBase='" + SQLDatabase.nowUserName() + "'  order by hmEdulevel";


            QueryForm queryForm = new QueryForm();


            queryForm.Sel = sel;
            queryForm.Constr = SQLDatabase.Connectstring;

            //Set the Column Collection to the filter Table
            //queryForm.SetSourceColumns(this.billMainDataSet.BalanceBill.Columns);

            queryForm.ShowDialog();

            this.textBox9.Text = queryForm.Result;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            string sel = "SELECT distinct hmTechTitle as 学历 FROM BK_MonthSalay_Rec where YY_DataBase='" + SQLDatabase.nowUserName() + "'  order by hmTechTitle";


            QueryForm queryForm = new QueryForm();


            queryForm.Sel = sel;
            queryForm.Constr = SQLDatabase.Connectstring;

            //Set the Column Collection to the filter Table
            //queryForm.SetSourceColumns(this.billMainDataSet.BalanceBill.Columns);

            queryForm.ShowDialog();

            this.textBox10.Text = queryForm.Result;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            string sel = "SELECT distinct hmProfessionalqualification as 专业资格 FROM BK_MonthSalay_Rec where YY_DataBase='" + SQLDatabase.nowUserName() + "'  order by hmProfessionalqualification";


            QueryForm queryForm = new QueryForm();


            queryForm.Sel = sel;
            queryForm.Constr = SQLDatabase.Connectstring;

            //Set the Column Collection to the filter Table
            //queryForm.SetSourceColumns(this.billMainDataSet.BalanceBill.Columns);

            queryForm.ShowDialog();

            this.textBox11.Text = queryForm.Result;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            string sel = "SELECT distinct cpID as 职级 FROM BK_MonthSalay_Rec where YY_DataBase='" + SQLDatabase.nowUserName() + "'  order by cpID";


            QueryForm queryForm = new QueryForm();


            queryForm.Sel = sel;
            queryForm.Constr = SQLDatabase.Connectstring;

            //Set the Column Collection to the filter Table
            //queryForm.SetSourceColumns(this.billMainDataSet.BalanceBill.Columns);

            queryForm.ShowDialog();

            this.textBox14.Text = queryForm.Result;
        }

        private void button10_Click(object sender, EventArgs e)
        {
            string sel = "SELECT distinct dpID as 职档 FROM BK_MonthSalay_Rec where YY_DataBase='" + SQLDatabase.nowUserName() + "'  order by dpID";


            QueryForm queryForm = new QueryForm();


            queryForm.Sel = sel;
            queryForm.Constr = SQLDatabase.Connectstring;

            //Set the Column Collection to the filter Table
            //queryForm.SetSourceColumns(this.billMainDataSet.BalanceBill.Columns);

            queryForm.ShowDialog();

            this.textBox15.Text = queryForm.Result;
        }

        private void button11_Click(object sender, EventArgs e)
        {
            string sel = "SELECT distinct countstyle as 计算方式 FROM BK_MonthSalay_Rec where YY_DataBase='" + SQLDatabase.nowUserName() + "'  order by countstyle";


            QueryForm queryForm = new QueryForm();


            queryForm.Sel = sel;
            queryForm.Constr = SQLDatabase.Connectstring;

            //Set the Column Collection to the filter Table
            //queryForm.SetSourceColumns(this.billMainDataSet.BalanceBill.Columns);

            queryForm.ShowDialog();

            this.textBox16.Text = queryForm.Result;
        }

        private void checkBoxinsuranceCompany_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox ckb = sender as CheckBox;

            string gq_DatacolumnName="";
            string gq_DatatableName="a";
            Control nowcontrol=null;

            string gq_Name = ckb.Text.Substring(0, ckb.Text.Length - 1);

            switch (gq_Name)
            {
                case "年份":
                    gq_DatacolumnName = "year_code";
                     //gq_DatatableName;
                     nowcontrol = this.textBoxinsuranceComPany;
                break ;

                case "月份":
                    gq_DatacolumnName = "month_code";
                    //gq_DatatableName;
                    nowcontrol = this.textBoxbillNumber;
                break;

                case "部门全称":
                gq_DatacolumnName = "upocName";
                    //gq_DatatableName;
                    nowcontrol = this.textBoxvehicleNumber;
                break;

                case "部门":
                gq_DatacolumnName = "ocName";
                //gq_DatatableName;
                nowcontrol = this.textBox1;
                break;
                
                case "岗位":
                gq_DatacolumnName = "osgName";
                //gq_DatatableName;
                nowcontrol = this.loanstyletextBox;
                break;
                case "岗位类别":
                gq_DatacolumnName = "categoryName";
                //gq_DatatableName;
                nowcontrol = this.textBox4;
                break;
                case "岗位序列":
                gq_DatacolumnName = "sequenceName";
                //gq_DatatableName;
                nowcontrol = this.textBox5;
                break;
                case "工号":
                gq_DatacolumnName = "hmWorkNo";
                //gq_DatatableName;
                nowcontrol = this.textBoxclientNumber;
                break;
                case "姓名":
                gq_DatacolumnName = "hmName";
                //gq_DatatableName;
                nowcontrol = this.textBoxclientName;
                break;
                case "性别":
                gq_DatacolumnName = "hmGender";
                //gq_DatatableName;
                nowcontrol = this.textBox2;
                break;
                case "工作日期":
                gq_DatacolumnName = "hmWorkDate";
                //gq_DatatableName;
                nowcontrol = this.textBox3;
                break;
                case "从业日期":
                gq_DatacolumnName = "hminbankDate";
                //gq_DatatableName;
                nowcontrol = this.textBoxpayDate;
                break;
                case "入行日期":
                gq_DatacolumnName = "hmTradeDate";
                //gq_DatatableName;
                nowcontrol = this.textBoxtermDate;
                break;
                case "人数":
                gq_DatacolumnName = "count(*)";
                //gq_DatatableName;
                nowcontrol = this.textBoxleftDays;
                break;
                case "证书分数":
                gq_DatacolumnName = "provescore";
                //gq_DatatableName;
                nowcontrol = this.textBox12;
                break;
                case "绩效分数":
                gq_DatacolumnName = "performancescore";
                //gq_DatatableName;
                nowcontrol = this.textBox13;
                break;
                case "工龄":
                gq_DatacolumnName = "hmWorkyear";
                //gq_DatatableName;
                nowcontrol = this.textBox6;
                break;
                case "业龄":
                gq_DatacolumnName = "hminbankyear";
                //gq_DatatableName;
                nowcontrol = this.textBox7;
                break;
                case "行龄":
                gq_DatacolumnName = "hmTradeyear";
                //gq_DatatableName;
                nowcontrol = this.textBox8;
                break;
                case "学历":
                gq_DatacolumnName = "hmEdulevel";
                //gq_DatatableName;
                nowcontrol = this.textBox9;
                break;
                case "职称":
                gq_DatacolumnName = "hmTechTitle";
                //gq_DatatableName;
                nowcontrol = this.textBox10;
                break;
                case "专业资格":
                gq_DatacolumnName = "hmProfessionalqualification";
                //gq_DatatableName;
                nowcontrol = this.textBox11;
                break;
                case "职级":
                gq_DatacolumnName = "cpID";
                //gq_DatatableName;
                nowcontrol = this.textBox14;
                break;
                case "职档":
                gq_DatacolumnName = "dpID";
                //gq_DatatableName;
                nowcontrol = this.textBox15;
                break;
                case "计算方式":
                gq_DatacolumnName = "countstyle";
                //gq_DatatableName;
                nowcontrol = this.textBox16;
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
       
    }
}
