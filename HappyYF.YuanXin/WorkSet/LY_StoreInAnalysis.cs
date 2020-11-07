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
    public partial class LY_StoreInAnalysis : Form
    {
         private DataSet ds;
        BindingSource bindingSource2;

        public LY_StoreInAnalysis()
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

                sWhere = sWhere + "  and ( a.input_date >= '" + dateTimePicker1.Text + "' and a.input_date < '" + dateTimePicker2.Text + "') ";


            }

            ///////////////////////////////////////////////////
            if (this.checkBox16.Checked)
            {
                header = header + " a.finished  as 签证,";
                ender = ender + " a.finished,";

                if (textBox12.Text.Replace(" ", "").Length > 0)
                {
                    if ("未签" == textBox12.Text)
                    {
                        sWhere = sWhere + " and  ( isnull(a.finished,0) = 0) ";
                    }
                    else if("已签" == textBox12.Text)
                    {
                        sWhere = sWhere + " and  ( a.finished = 1) ";
                    }


                }



            }

            ///////////////////////////////////////////////////
            if (this.checkBox20.Checked)
            {
                header = header + " a.warehouse  as 仓库,";
                ender = ender + " a.warehouse,";

                if (textBox15.Text.Replace(" ", "").Length > 0)
                {

                    sWhere = sWhere + " and  ( a.warehouse = '" + textBox15.Text + "') ";


                }



            }
            ///////////////////////////////////////////////////
            if (this.checkBox18.Checked)
            {
                header = header + " isnull(a.in_style,'未录')  as 入库类别,";
                ender = ender + " a.in_style,";

                if (textBox14.Text.Replace(" ", "").Length > 0)
                {

                    sWhere = sWhere + " and  ( a.in_style = '" + textBox14.Text + "') ";


                }



            }

            ///////////////////////////////////////////////////
            if (this.checkBox17.Checked)
            {
                header = header + " isnull(a.in_number,'盘点入库')  as 入库单号,";
                ender = ender + " a.in_number,";

                if (textBox13.Text.Replace(" ", "").Length > 0)
                {

                    sWhere = sWhere + " and  ( a.in_number = '" + textBox13.Text + "') ";


                }



            }

            ///////////////////////////////////////////////////
            if (this.checkBox15.Checked)
            {
                header = header + " a.verify_people  as 签证人,";
                ender = ender + " a.verify_people,";

                if (textBox8.Text.Replace(" ", "").Length > 0)
                {

                    sWhere = sWhere + " and  ( a.verify_people = '" + textBox8.Text + "') ";


                }



            }



            //////////////////////////////////////////////////
            /////////////////////////////////////////

            if (this.checkBox10.Checked)
            {
                header = header + " a.status as 状态,";
                ender = ender + " a.status,";

                if (textBox10.Text.Replace(" ", "").Length > 0)
                {

                    sWhere = sWhere + " and   a.status = '" + textBox10.Text + "'";


                }




            }

          

            ///////////////////////////////////////////////////
            if (this.checkBox1.Checked)
            {
                header = header + " a.sortname  as 种类,";
                ender = ender + " a.sortname,";

                if (textBox1.Text.Replace(" ", "").Length > 0)
                {

                    sWhere = sWhere + " and  ( a.sortname = '" + textBox1.Text + "') ";


                }



            }

           
           

            //////////////////////////////////////////////////


            if (this.checkBox11.Checked)
            {
                header = header + " isnull(a.bill_code,'--')  as 单据编号,";
                ender = ender + " isnull(a.bill_code,'--'),";

                if (textBox11.Text.Replace(" ", "").Length > 0)
                {

                    sWhere = sWhere + " and  ( a.bill_code = '" + textBox11.Text + "') ";

                   
                }



            }


            /////////////////////////////////////////

            if (this.checkBox3.Checked)
            {
                header = header + " a.wzbh as 物料编号,a.dw as 单位,";
                ender = ender + " a.wzbh,a.dw,";

                if (textBox3.Text.Replace(" ", "").Length > 0)
                {

                    sWhere = sWhere + " and   a.wzbh = '" + textBox3.Text + "'";


                }




            }

            ///////////////////////////////////////////////////

            if (this.checkBox2.Checked)
            {
                header = header + " a.jph as 库位,";
                ender = ender + " a.jph,";

                if (textBox2.Text.Replace(" ", "").Length > 0)
                {

                    sWhere = sWhere + " and   a.jph = '" + textBox2.Text + "'";


                }


            }



            ///////////////////////////////////////////////////

            if (this.checkBox7.Checked)
            {
                header = header + " a.mch as 物料名称,";
                ender = ender + " a.mch,";

                if (textBox7.Text.Replace(" ", "").Length > 0)
                {

                    sWhere = sWhere + " and   a.mch = '" + textBox7.Text + "'";


                }

            }




            ////////////////////////////////////////////////////

            if (this.checkBox19.Checked)
            {
                header = header + "a.gg as 规格,";
                ender = ender + "a.gg,";

                if (textBox19.Text.Replace(" ", "").Length > 0)
                {

                    sWhere = sWhere + " and   a.gg = '" + textBox19.Text + "'";


                }
            }

            ////////////////////////////////////////////////////

            if (this.checkBox12.Checked)
            {
                header = header + "a.xhc as 中方型号,";
                ender = ender + "a.xhc,";

                if (textBox5.Text.Replace(" ", "").Length > 0)
                {

                    sWhere = sWhere + " and   a.xhc = '" + textBox5.Text + "'";


                }
            }

        
          

            //////////////////////////////////////////////////


            if (this.checkBox13.Checked)
            {
                header = header + "a.xhj as 日方型号,";
                ender = ender + "a.xhj,";

                if (textBox6.Text.Replace(" ", "").Length > 0)
                {

                    sWhere = sWhere + " and   a.xhj = '" + textBox6.Text + "'";


                }
            }




            //////////////////////////////////////////////////

            if (this.checkBox9.Checked)
            {
                header = header + " a.employe  as 采购员,";
                ender = ender + " a.employe ,";

                if (textBox9.Text.Replace(" ", "").Length > 0)
                {


                    sWhere = sWhere + " and  ( a.employe  = '" + textBox9.Text + "') ";

                    
                }



            }
            /////////////////////////////////////////////////



            if (this.checkBox14.Checked)
            {
                header = header + " a.operoter  as 库管员,";
                ender = ender + " a.operoter,";

                if (textBox4.Text.Replace(" ", "").Length > 0)
                {

                    if ("" != textBox4.Text)
                    {


                        sWhere = sWhere + " and a.operoter= '" + textBox4.Text + "'";


                    }
                }


            }

       


            //////////////////////////////////////////////////



               //////////////////////////////////////////////////


                if (this.checkBox4.Checked)
                {
                    header = header + " year(a.input_date) as 年份, ";
                    ender = ender + " year(a.input_date),";

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


                if (this.checkBox5.Checked)
                {
                    header = header + " year(a.input_date) as 年份, month(a.input_date) as 月份, ";
                    ender = ender + " year(a.input_date),month(a.input_date),";

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

                if (this.checkBox6.Checked)
                {
                    header = header + " a.input_date as 日期,  ";
                    ender = ender + " a.input_date,";

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


                if (ender.Length > 0)
                {
                    ender = ender.Substring(0, ender.Length - 1);

                }
                else
                {
                    ender = "";
                }





                string body = " sum( isnull(a.qty,0))  as 数量 " +
                              //" case when sum( isnull(a.qty,0))>0 then sum(isnull(a.store_money,0))/sum( isnull(a.qty,0)) else 0 end as 库存单价, " +
                              //" sum(isnull(a.store_money,0)) as 库存金额, " +
                              //" case when sum( isnull(a.qty,0))>0 then sum(isnull(a.buy_money,0))/sum( isnull(a.qty,0)) else 0 end as 采购单价, " +
                              //" sum(isnull(a.buy_money,0)) as 采购金额 " +
                              " FROM ly_store_in_View AS a ";
                              //"  sum(isnull(a.accrual_money,0))  as 利息, " +
                              //" sum(isnull(a.compensate_accrual_money,0)) as 还息, " +
                              //" ( sum(isnull( a.accrual_money,0)) -    sum(isnull(a.compensate_accrual_money,0))) as 欠息," +
                              //" left(max(convert(varchar(20),accrual_date,120)),10)  as 计息日期" +
                          //" FROM ly_store_in_View AS a LEFT OUTER JOIN " +
                          //"  ly_inma0010 AS b ON a.wzbh = b.wzbh " +
                          //" LEFT OUTER JOIN " +
                          //" ly_materrial_sort AS c on b.sort1 = c.sortcode ";
                      
                 
            
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
            myAdapter.SelectCommand.CommandTimeout = 0;

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

            this.dataGridView1.Columns["数量"].DefaultCellStyle.Alignment =DataGridViewContentAlignment.MiddleRight ; 
            this.dataGridView1.Columns["数量"].DefaultCellStyle.Format ="N0";

            //this.dataGridView1.Columns["库存单价"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            //this.dataGridView1.Columns["库存单价"].DefaultCellStyle.Format = "N2";

            //this.dataGridView1.Columns["库存金额"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            //this.dataGridView1.Columns["库存金额"].DefaultCellStyle.Format = "N2";

            //this.dataGridView1.Columns["采购单价"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            //this.dataGridView1.Columns["采购单价"].DefaultCellStyle.Format = "N2";

            //this.dataGridView1.Columns["采购金额"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            //this.dataGridView1.Columns["采购金额"].DefaultCellStyle.Format = "N2";
                
             


            this.bindingNavigator1.BindingSource = bindingSource2;

            bindingSource2.ResetBindings(true);

            AddSummationRow(bindingSource2, dataGridView1);
        }

        //大类
        private void dalei_Click(object sender, EventArgs e)
        {
            //string sel = "SELECT GroupName as 大类名称,cast (GroupID as varchar(20)) as 大类代码 FROM YX_group_main";
            string sel = "SELECT distinct employe as 采购员 FROM ly_store_in_View";


            QueryForm queryForm = new QueryForm();


            queryForm.Sel = sel;
            queryForm.Constr = SQLDatabase .Connectstring;

            //Set the Column Collection to the filter Table
            //queryForm.SetSourceColumns(this.billMainDataSet.BalanceBill.Columns);

            queryForm.ShowDialog();

            this.textBox9.Text = queryForm.Result;
        }

        private void xiangmu_Click(object sender, EventArgs e)
        {
            string sel = "SELECT sortname as 种类名称,sortcode as 种类编号 FROM ly_materrial_sort  "; 
            

            QueryForm queryForm = new QueryForm();


            queryForm.Sel = sel;
            queryForm.Constr = SQLDatabase.Connectstring;

            //Set the Column Collection to the filter Table
            //queryForm.SetSourceColumns(this.billMainDataSet.BalanceBill.Columns);

            queryForm.ShowDialog();

            this.textBox1.Text = queryForm.Result;
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
                case "客户编号":
                    res = " isnull(b.Client_Code,'其它')  as 客户编号";
                    break;
                case "客户姓名":
                    res = " c.Name  as 客户姓名";
                    break;
                case "车号":
                    res = " b.vehicle_code  as 车号";
                    break;
                case "摘要":
                    res = " d.remark as 摘要";
                    break;
                case "是否结账":
                    res = " (case  when b.balance_Date is not null then '已结' when b.balance_Date is null  then '未结' end ) as 是否结账";
                    break;
                case "业务人员":
                    res = " c.employee  as 业务人员";
                    break;
                case "年份":
                    res = " year(a.accrual_date) as 年份";
                    break;
                case "月份":
                    res = " year(a.accrual_date) as 年份, month(a.accrual_date) as 月份";
                    break;
                case "日期":
                    res = " a.accrual_date as 日期";
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
                case "客户编号":
                    res = " isnull(b.Client_Code,'其它')";
                    break;
                case "客户姓名":
                    res = " c.Name";
                    break;
                case "车号":
                    res = " b.vehicle_code";
                    break;
                case "摘要":
                    res = " d.remark";
                    break;
                case "是否结账":
                    res = " (case  when b.balance_Date is not null then '已结' when b.balance_Date is null  then '未结' end )";
                    break;
                case "业务人员":
                    res = " c.employee";
                    break;

                case "年份":
                    res = " year(a.accrual_date)";
                    break;
              
                case "月份":
                    res = " year(a.accrual_date),month(a.accrual_date)";
                    break;
                case "日期":
                    res = " a.accrual_date";
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
           
            DataSort.SetSortColumns(qds.QueryColumns.Columns,ls );
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

                    sWhere = sWhere + "  and ( a.accrual_date >= '" + dateTimePicker1.Text + "' and a.accrual_date <= '" + dateTimePicker2.Text + "') ";


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

                string body = " sum( isnull(a.principal_money,0))  as 本金, " +
                           " sum(isnull(a.compensate_money,0)) as 还本, " +
                           " (sum( isnull(a.principal_money,0)) - sum(isnull(a.compensate_money,0))) as 欠本," +
                           "  sum(isnull(a.accrual_money,0))  as 利息, " +
                           " sum(isnull(a.compensate_accrual_money,0)) as 还息, " +
                           " ( sum(isnull( a.accrual_money,0)) -    sum(isnull(a.compensate_accrual_money,0))) as 欠息," +
                           " left(max(convert(varchar(20),accrual_date,120)),10)  as 计息日期" +
                       " FROM XD_Client_Loanaccrual AS a LEFT OUTER JOIN " +
                       "  XD_Client_Loan AS b ON a.loan_id = b.Id " +
                       " LEFT OUTER JOIN " +
                       " YX_client AS c on b.Client_Code = c.Card_number "; //+
                       //" LEFT OUTER JOIN " +
                       //" XD_client_compensate_loan AS d on a.loan_id = d.loan_id and a.accrual_date = d.compensate_date ";
                
                
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

            string sel = "SELECT distinct bill_code as 单据编号 FROM ly_store_in";


            QueryForm queryForm = new QueryForm();


            queryForm.Sel = sel;
            queryForm.Constr = SQLDatabase.Connectstring;

            //Set the Column Collection to the filter Table
            //queryForm.SetSourceColumns(this.billMainDataSet.BalanceBill.Columns);

            queryForm.ShowDialog();

            this.textBox11.Text = queryForm.Result;
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

            this.textBox9.Text = queryForm.Result;
        }

        private void taocan_Click(object sender, EventArgs e)
        {

            string sel = "SELECT distinct status as 物料状态 FROM ly_store_in_view";

            QueryForm queryForm = new QueryForm();


            queryForm.Sel = sel;
            queryForm.Constr = SQLDatabase.Connectstring;

            //Set the Column Collection to the filter Table
            //queryForm.SetSourceColumns(this.billMainDataSet.BalanceBill.Columns);

            queryForm.ShowDialog();

            this.textBox10.Text = queryForm.Result;
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

            decimal sum_qty = 0;
            decimal sum_storemoney = 0;
            decimal sum_buymoney = 0;
         
           
            //decimal sum_accrual = 0;
            //decimal sum_compensate_accrual = 0;
            //decimal sum_left_accrual = 0;



            foreach (DataGridViewRow dr in dg.Rows)
            {
                if (System.DBNull.Value == dr.Cells["数量"].Value)
                    sum_qty = sum_qty + 0;
                else
                    sum_qty = sum_qty + decimal.Parse(dr.Cells["数量"].Value.ToString());

                //if (System.DBNull.Value == dr.Cells["库存金额"].Value)
                //    sum_storemoney = sum_storemoney + 0;
                //else
                //    sum_storemoney = sum_storemoney + decimal.Parse(dr.Cells["库存金额"].Value.ToString());

                //if (System.DBNull.Value == dr.Cells["采购金额"].Value)
                //    sum_buymoney = sum_buymoney + 0;
                //else
                //    sum_buymoney = sum_buymoney + decimal.Parse(dr.Cells["采购金额"].Value.ToString());


                //if (System.DBNull.Value == dr.Cells["利息"].Value)
                //    sum_accrual = sum_accrual + 0;
                //else
                //    sum_accrual = sum_accrual + decimal.Parse(dr.Cells["利息"].Value.ToString());

                //if (System.DBNull.Value == dr.Cells["还息"].Value)
                //    sum_compensate_accrual = sum_compensate_accrual + 0;
                //else
                //    sum_compensate_accrual = sum_compensate_accrual + decimal.Parse(dr.Cells["还息"].Value.ToString());

                //if (System.DBNull.Value == dr.Cells["欠息"].Value)
                //    sum_left_accrual = sum_left_accrual + 0;
                //else
                //    sum_left_accrual = sum_left_accrual + decimal.Parse(dr.Cells["欠息"].Value.ToString());

             

            }
            bs.AddNew();


            dg.CurrentRow.Cells["数量"].Value = sum_qty;
            //dg.CurrentRow.Cells["库存金额"].Value = sum_storemoney;
            //dg.CurrentRow.Cells["采购金额"].Value = sum_buymoney;
            //dg.CurrentRow.Cells["利息"].Value = sum_accrual;
            //dg.CurrentRow.Cells["还息"].Value = sum_compensate_accrual;
            //dg.CurrentRow.Cells["欠息"].Value = sum_left_accrual;

       

            bs.EndEdit();

            bs.Position = 0;




        }

        private void button19_Click(object sender, EventArgs e)
        {
            //string sel = "SELECT  '(' +isnull(yhbm,'')+')'  + isnull(yhmc,'') as 服务人员 FROM T_users";

            string sel = "SELECT distinct gg as 规格 FROM ly_inma0010";

            QueryForm queryForm = new QueryForm();


            queryForm.Sel = sel;
            queryForm.Constr = SQLDatabase.Connectstring;

            //Set the Column Collection to the filter Table
            //queryForm.SetSourceColumns(this.billMainDataSet.BalanceBill.Columns);

            queryForm.ShowDialog();

            this.textBox19.Text = queryForm.Result;
        }

        private void chehao_Click(object sender, EventArgs e)
        {
            string sel = "SELECT   wzbh as 编号,mch as 名称 FROM ly_inma0010";


            QueryForm queryForm = new QueryForm();


            queryForm.Sel = sel;
            queryForm.Constr = SQLDatabase.Connectstring;

            //Set the Column Collection to the filter Table
            //queryForm.SetSourceColumns(this.billMainDataSet.BalanceBill.Columns);

            queryForm.ShowDialog();

            this.textBox3.Text = queryForm.Result;
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

        private void button2_Click(object sender, EventArgs e)
        {
            string sel = "SELECT   mch as 名称,wzbh as 编号 FROM ly_inma0010";


            QueryForm queryForm = new QueryForm();


            queryForm.Sel = sel;
            queryForm.Constr = SQLDatabase.Connectstring;

            //Set the Column Collection to the filter Table
            //queryForm.SetSourceColumns(this.billMainDataSet.BalanceBill.Columns);

            queryForm.ShowDialog();

            this.textBox7.Text = queryForm.Result;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string sel = "SELECT distinct  jph as 库位 FROM ly_inma0010";


            QueryForm queryForm = new QueryForm();


            queryForm.Sel = sel;
            queryForm.Constr = SQLDatabase.Connectstring;

            //Set the Column Collection to the filter Table
            //queryForm.SetSourceColumns(this.billMainDataSet.BalanceBill.Columns);

            queryForm.ShowDialog();

            this.textBox2.Text = queryForm.Result;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string sel = "SELECT distinct  xhc as 中方型号 FROM ly_inma0010";


            QueryForm queryForm = new QueryForm();


            queryForm.Sel = sel;
            queryForm.Constr = SQLDatabase.Connectstring;

            //Set the Column Collection to the filter Table
            //queryForm.SetSourceColumns(this.billMainDataSet.BalanceBill.Columns);

            queryForm.ShowDialog();

            this.textBox5.Text = queryForm.Result;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string sel = "SELECT distinct  xhj as 日方型号 FROM ly_inma0010";


            QueryForm queryForm = new QueryForm();


            queryForm.Sel = sel;
            queryForm.Constr = SQLDatabase.Connectstring;

            //Set the Column Collection to the filter Table
            //queryForm.SetSourceColumns(this.billMainDataSet.BalanceBill.Columns);

            queryForm.ShowDialog();

            this.textBox6.Text = queryForm.Result;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            string sel = "SELECT distinct  operoter as 库管员 FROM ly_store_in_View";


            QueryForm queryForm = new QueryForm();


            queryForm.Sel = sel;
            queryForm.Constr = SQLDatabase.Connectstring;

            //Set the Column Collection to the filter Table
            //queryForm.SetSourceColumns(this.billMainDataSet.BalanceBill.Columns);

            queryForm.ShowDialog();

            this.textBox4.Text = queryForm.Result;
        }

        private void button10_Click(object sender, EventArgs e)
        {
            string sel = "SELECT distinct  warehouse as 仓库 FROM ly_store_in_View";


            QueryForm queryForm = new QueryForm();


            queryForm.Sel = sel;
            queryForm.Constr = SQLDatabase.Connectstring;

            //Set the Column Collection to the filter Table
            //queryForm.SetSourceColumns(this.billMainDataSet.BalanceBill.Columns);

            queryForm.ShowDialog();

            this.textBox15.Text = queryForm.Result;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            string sel = "SELECT distinct  isnull(in_style,'盘点') as 入库类别 FROM ly_store_in_View";


            QueryForm queryForm = new QueryForm();


            queryForm.Sel = sel;
            queryForm.Constr = SQLDatabase.Connectstring;

            //Set the Column Collection to the filter Table
            //queryForm.SetSourceColumns(this.billMainDataSet.BalanceBill.Columns);

            queryForm.ShowDialog();

            this.textBox14.Text = queryForm.Result;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            string sel = "SELECT distinct  isnull(in_number,'盘点入库') as 入库单号 FROM ly_store_in_View";


            QueryForm queryForm = new QueryForm();


            queryForm.Sel = sel;
            queryForm.Constr = SQLDatabase.Connectstring;

            //Set the Column Collection to the filter Table
            //queryForm.SetSourceColumns(this.billMainDataSet.BalanceBill.Columns);

            queryForm.ShowDialog();

            this.textBox13.Text = queryForm.Result;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            string sel = "SELECT distinct  verify_people as 签证人 FROM ly_store_in_View";


            QueryForm queryForm = new QueryForm();


            queryForm.Sel = sel;
            queryForm.Constr = SQLDatabase.Connectstring;

            //Set the Column Collection to the filter Table
            //queryForm.SetSourceColumns(this.billMainDataSet.BalanceBill.Columns);

            queryForm.ShowDialog();

            this.textBox8.Text = queryForm.Result;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            string sel = "SELECT distinct  case when isnull(finished,0)=0 then '未签' else '已签' end as 签证 FROM ly_store_in_View";


            QueryForm queryForm = new QueryForm();


            queryForm.Sel = sel;
            queryForm.Constr = SQLDatabase.Connectstring;

            //Set the Column Collection to the filter Table
            //queryForm.SetSourceColumns(this.billMainDataSet.BalanceBill.Columns);

            queryForm.ShowDialog();

            this.textBox12.Text = queryForm.Result;
        }

     
       
    }
}
