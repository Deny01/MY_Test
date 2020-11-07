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
    public partial class LY_ProductTask_SumAnalysis : Form
    {
         private DataSet ds;
        BindingSource bindingSource2;

        GroupQueryItemSelector gqis;
        private int selectionIdx = 0;


        public LY_ProductTask_SumAnalysis()
        {
            InitializeComponent();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            
           
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
            string sel;
            if (this.radioButton1.Checked)
            {

                sel = "SELECT distinct yearcode as 年份 FROM ly_production_task_view order by yearcode desc";
            }
            else
            {
                sel = "SELECT distinct yearcode as 年份 FROM ly_production_task_view order by yearcode desc";
            }


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
            string sel = "SELECT distinct plan_date as 计划日期 FROM ly_production_task_view order by plan_date desc"; ;
           
            

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
            string orderstr= " order by ";


            string sWhere = "  where  1 = 1 ";

            if (this.checkBox8.Checked)
            {
                if (this.radioButton1.Checked)
                {
                    sWhere = sWhere + "  and ( a.plan_date >= '" + dateTimePicker1.Text + "' and a.plan_date < '" + dateTimePicker2.Text + "') ";
                }
                else
                {
                    sWhere = sWhere + "  and ( a.plan_date >= '" + dateTimePicker1.Text + "' and a.plan_date < '" + dateTimePicker2.Text + "') ";
                }

            }

            if (this.radioButton5.Checked)
            {

            }
            else
            {
                if (this.radioButton4.Checked)
                {
                    sWhere = sWhere + "  and  dbo.f_category_conversion(category)='" + this.radioButton4.Text + "' ";
                }
                if (this.radioButton3.Checked)
                {
                    sWhere = sWhere + "  and  dbo.f_category_conversion(category)='" + this.radioButton3.Text + "' ";
                }

            }

            header = header + this.gqis.MakeAllHeader();
            ender = ender + this.gqis.MakeAllheaderEnder();
            sWhere = sWhere + this.gqis.MakeAllWhere();
            orderstr = orderstr + this.gqis.MakeAllheaderEnder();

            if (ender.Length > 0)
            {
                ender = ender.Substring(0, ender.Length - 1);

            }
            else
            {
                ender = "";
            }

            string body = " cast(avg(isnull(a.plan_count,0)) as decimal(18,0)) as 计划数," +
                              " cast(sum(isnull(a.make_num,0)) as decimal(18,0)) as 任务数," +
                              " cast(sum(isnull(a.inspect_qty,0)) as decimal(18,0)) as 送检数," +
                              " cast(sum(isnull(a.inspect_fir_qty,0)) as decimal(18,0)) as 初检合格," +
                              " cast(sum(isnull(a.in_qty,0)) as decimal(18,0)) as 入库数," +
                              " cast(sum( isnull(a.make_num,0) - isnull(a.in_qty,0)) as decimal(18,0)) as 任务未入库," +
                               //" cast(avg(isnull(a.mprice_novat,0)) as decimal(18,0)) as 物料成本单价," +
                               // " cast(avg(isnull(a.mprice_novat,0)) as decimal(18,0)) * cast(sum( isnull(a.make_num,0) - isnull(a.in_qty,0)) as decimal(18,0)) as 未入库原料成本," +
                              " cast(((cast (sum( isnull(a.inspect_fir_qty,0)) as decimal(18,2))  )/" +
            " case when  sum(isnull(a.inspect_qty,0))<>0 then  sum(isnull(a.inspect_qty,0)) else null end *100 ) as decimal(18,2)) as 初检率";





            string body3 = " FROM ly_production_task_view  AS a ";


            body = body + body3;

            string sel;

           

            if (ender.Length != 9)
            {
                orderstr = ender.Replace("group by", "order by");
                sel = header + body + sWhere + ender + orderstr;
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


            NewFrm.Hide(this);
        }

        private void kahao_Click(object sender, EventArgs e)
        {

            string sel = "SELECT distinct wzbh as 成品编号,mch as 成品名称,xhc as 规格型号, dbo.fn_GetPinyin(mch) as py,LOWER(dbo.fGetPy(mch)) as jp FROM ly_production_task_view  order by wzbh";


            QueryForm queryForm = new QueryForm();


            queryForm.Sel = sel;
            queryForm.Nodiscol = 3;
            queryForm.Constr = SQLDatabase.Connectstring;

            //Set the Column Collection to the filter Table
            //queryForm.SetSourceColumns(this.billMainDataSet.BalanceBill.Columns);

            queryForm.ShowDialog();

            this.textBoxclientNumber.Text = queryForm.Result;
        }

        
      
        private void taocan_Click(object sender, EventArgs e)
        {

            string sel;
            if (this.radioButton1.Checked)
            {

                sel = "SELECT distinct monthcode as 月份 FROM ly_production_task_view order by monthcode ";
            }
            else
            {
                sel = "SELECT distinct monthcode as 月份 FROM ly_production_task_view order by monthcode ";
            }

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

            int  sum_qty = 0;

            decimal sum_jine= 0;
            decimal sum_zhekou = 0;

            decimal sum_chujian = 0;
            decimal sum_zhekoulv = 0;

            decimal sum_no_instore = 0;

            decimal sum_plan = 0;
         

          



           



            foreach (DataGridViewRow dr in dg.Rows)
            {
                if (System.DBNull.Value == dr.Cells["任务数"].Value)
                    sum_qty = sum_qty + 0;
                else
                    sum_qty = sum_qty + int.Parse(dr.Cells["任务数"].Value.ToString());

                if (System.DBNull.Value == dr.Cells["送检数"].Value)
                    sum_jine = sum_jine + 0;
                else
                    sum_jine = sum_jine + decimal.Parse(dr.Cells["送检数"].Value.ToString());

                if (System.DBNull.Value == dr.Cells["入库数"].Value)
                    sum_zhekou = sum_zhekou + 0;
                else
                    sum_zhekou = sum_zhekou + decimal.Parse(dr.Cells["入库数"].Value.ToString());

                if (System.DBNull.Value == dr.Cells["任务未入库"].Value)
                    sum_no_instore = sum_no_instore + 0;
                else
                    sum_no_instore = sum_no_instore + decimal.Parse(dr.Cells["任务未入库"].Value.ToString());


                if (System.DBNull.Value == dr.Cells["初检合格"].Value)
                    sum_chujian = sum_chujian + 0;
                else
                    sum_chujian = sum_chujian + decimal.Parse(dr.Cells["初检合格"].Value.ToString());

                if (System.DBNull.Value == dr.Cells["计划数"].Value)
                    sum_plan = sum_plan + 0;
                else
                    sum_plan = sum_plan + decimal.Parse(dr.Cells["计划数"].Value.ToString());

               
              

             

            }

            if (0 != (sum_jine ))
            {
                sum_zhekoulv = 100 * sum_chujian / (sum_jine);
            }
          


          


            bs.AddNew();


            dg.CurrentRow.Cells["任务数"].Value = sum_qty;
            dg.CurrentRow.Cells["送检数"].Value = sum_jine;
            dg.CurrentRow.Cells["入库数"].Value = sum_zhekou;
            dg.CurrentRow.Cells["任务未入库"].Value = sum_no_instore;
            dg.CurrentRow.Cells["初检合格"].Value = sum_chujian;
            dg.CurrentRow.Cells["计划数"].Value = sum_plan;
            dg.CurrentRow.Cells["初检率"].Value = decimal.Round(sum_zhekoulv, 2); 
          
         
            bs.EndEdit();

            bs.Position = 0;




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

       

        private void button1_Click(object sender, EventArgs e)
        {
            string sel = "SELECT distinct material_plan_num  as  计划 FROM ly_production_task_view order by material_plan_num ";


            QueryForm queryForm = new QueryForm();


            queryForm.Sel = sel;
            queryForm.Constr = SQLDatabase.Connectstring;

            //Set the Column Collection to the filter Table
            //queryForm.SetSourceColumns(this.billMainDataSet.BalanceBill.Columns);

            queryForm.ShowDialog();

            this.textBox1.Text = queryForm.Result;
        }

       

        

       

     

        private void button8_Click(object sender, EventArgs e)
        {
            string sel = "SELECT distinct oper_dept as 部门 FROM ly_production_task_view";


            QueryForm queryForm = new QueryForm();


            queryForm.Sel = sel;
            queryForm.Constr = SQLDatabase.Connectstring;

            //Set the Column Collection to the filter Table
            //queryForm.SetSourceColumns(this.billMainDataSet.BalanceBill.Columns);

            queryForm.ShowDialog();

            this.textBox11.Text = queryForm.Result;
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
                case "计划":
                    gq_DatacolumnName = "material_plan_num";
                     //gq_DatatableName;
                    nowcontrol = this.textBox1;
                break ;

                case "部门":
                gq_DatacolumnName = "oper_dept";
                    //gq_DatatableName;
                     nowcontrol = this.textBox11;
                break;

               

             
                case "装配人":
                gq_DatacolumnName = "work_code";
                //gq_DatatableName;
                nowcontrol = this.textBoxtermDate;
                break;
                case "年份":
                if (this.radioButton1.Checked)
                {
                    gq_DatacolumnName = "yearcode";
                }
                else
                {

                    gq_DatacolumnName = "yearcode";
                }
                //gq_DatatableName;
                nowcontrol = this.textBoxinsuranceComPany;
                break;
                case "月份":
              
                //gq_DatatableName;
                if (this.radioButton1.Checked)
                {
                    gq_DatacolumnName = "monthcode";
                }
                else
                {

                    gq_DatacolumnName = "monthcode";
                }
                nowcontrol = this.textBoxbillNumber;
                break;
                case "计划日期":
                gq_DatacolumnName = "plan_date";
                //gq_DatatableName;
                nowcontrol = this.textBoxvehicleNumber;
                break;
                

                case "大类":
                gq_DatacolumnName = "fir_style";
                //gq_DatatableName;
                nowcontrol = this.textBox9;
                break;
               
               
                case "成品编号":
                gq_DatacolumnName = "wzbh";
                //gq_DatatableName;
                nowcontrol = this.textBoxclientNumber;
                break;
                case "任务单号":
                gq_DatacolumnName = "pruductionOrder_num";
                //gq_DatatableName;
                nowcontrol = this.textBox6;
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

      

        private void termDate_Click(object sender, EventArgs e)
        {
            string sel = "SELECT distinct work_code as 工人 FROM ly_production_task_view  order by work_code";


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
            string sel = "SELECT distinct pruductionOrder_num as 任务单号 FROM ly_production_task_view  order by pruductionOrder_num desc";


            QueryForm queryForm = new QueryForm();


            queryForm.Sel = sel;
            queryForm.Constr = SQLDatabase.Connectstring;

            //Set the Column Collection to the filter Table
            //queryForm.SetSourceColumns(this.billMainDataSet.BalanceBill.Columns);

            queryForm.ShowDialog();

            this.textBox6.Text = queryForm.Result;
        }

      

        protected bool IsInteger(object o)
        {
            if (o is Int64)
                return true;
            if (o is Int32)
                return true;
            if (o is Int16)
                return true;
            return false;
        }
        protected bool IsDecimal(object o)
        {
            if (o is Decimal)
                return true;
            if (o is Single)
                return true;
            if (o is Double)
                return true;
            return false;
        }

        private void AddSummationRow_New(BindingSource bs, DataGridView dgv)
        {
            DataRow sumdr = (((DataSet)bs.DataSource).Tables[bs.DataMember]).NewRow();

            if (-1 != bs.Find("客户名称", "合计"))
            {
                bs.RemoveAt(bs.Find("客户名称", "合计"));
            }

            foreach (DataGridViewRow dgvRow in dgv.Rows)
            {
                foreach (DataGridViewCell dgvCell in dgvRow.Cells)
                {
                    //foreach (DataGridViewColumn dgvColumn in dgv.Columns)
                    //{
                    if (dgvCell.Value != null && !(dgvCell.Value is DBNull))
                    {
                        if (IsInteger(dgvCell.Value))
                        {
                            if ("合同天数" != dgvCell.OwningColumn.HeaderText && "到期天数" != dgvCell.OwningColumn.HeaderText && "折扣利率" != dgvCell.OwningColumn.HeaderText)
                            {
                                if (sumdr[dgvCell.OwningColumn.DataPropertyName] == null || (sumdr[dgvCell.OwningColumn.DataPropertyName] is DBNull))
                                    sumdr[dgvCell.OwningColumn.DataPropertyName] = 0;


                                sumdr[dgvCell.OwningColumn.DataPropertyName] = Convert.ToInt64(sumdr[dgvCell.OwningColumn.DataPropertyName]) + Convert.ToInt64(dgvCell.Value);
                            }
                        }
                        else if (IsDecimal(dgvCell.Value))
                        {
                            if ("合同天数" != dgvCell.OwningColumn.HeaderText && "到期天数" != dgvCell.OwningColumn.HeaderText && "折扣利率" != dgvCell.OwningColumn.HeaderText)
                            {
                                if (sumdr[dgvCell.OwningColumn.DataPropertyName] == null || (sumdr[dgvCell.OwningColumn.DataPropertyName] is DBNull))
                                    sumdr[dgvCell.OwningColumn.DataPropertyName] = 0;
                                //dgvCell .OwningColumn.Name  dgvCell.ColumnIndex

                                sumdr[dgvCell.OwningColumn.DataPropertyName] = Convert.ToDecimal(sumdr[dgvCell.OwningColumn.DataPropertyName]) + Convert.ToDecimal(dgvCell.Value);
                            }
                        }

                        //sumBox.Text = string.Format("{0}", sumBox.Tag);
                        //sumBox.Invalidate();

                    }
                    //}
                }

            }
            sumdr["客户名称"] = "合计";
            sumdr["id"] = 0;
            ((DataSet)bs.DataSource).Tables[bs.DataMember].Rows.Add(sumdr);
            bs.ResetBindings(true);

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

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (this.checkBoxinsuranceCompany.Checked)
            {

                //this.gqis.Del_Incontrol("年份");
                this.checkBoxinsuranceCompany.Checked = false ;
                this.checkBoxinsuranceCompany.Checked = true;
            }
            if (this.checkBoxbillNumber.Checked)
            {

                //this.gqis.Del_Incontrol("月份");
                this.checkBoxbillNumber.Checked = false ;
                this.checkBoxbillNumber.Checked = true;
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            string sel = "SELECT distinct fir_style as 大类 FROM ly_production_task_view  order by fir_style";


            QueryForm queryForm = new QueryForm();


            queryForm.Sel = sel;
            queryForm.Constr = SQLDatabase.Connectstring;

            //Set the Column Collection to the filter Table
            //queryForm.SetSourceColumns(this.billMainDataSet.BalanceBill.Columns);

            queryForm.ShowDialog();

            this.textBox9.Text = queryForm.Result;
        }

      

       


        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            LY_Product_MonthReport queryForm = new LY_Product_MonthReport();

            //queryForm.OwnerForm = this;


            queryForm.StartPosition = FormStartPosition.CenterParent;

            queryForm.WindowState = FormWindowState.Maximized;
            queryForm.ShowDialog(this);




           
        }

        private void toolStripButton7_Click(object sender, EventArgs e)
        {
            //LY_SalseContract_StandardSum queryForm = new LY_SalseContract_StandardSum();

            ////queryForm.OwnerForm = this;


            ////queryForm.StartPosition = FormStartPosition.CenterParent;

            //queryForm.WindowState = FormWindowState.Maximized;
            //queryForm.ShowDialog(this);
        }

        private void textBoxinsuranceComPany_TextChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

       
       
    }
}
