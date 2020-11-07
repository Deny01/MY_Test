using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;

using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HappyYF.Infrastructure.Repositories;

namespace HappyYF.YuanXin.WorkSet
{
    public partial class SumCard : Form
    {
        public SumCard()
        {
            InitializeComponent();
        }

        private void SumCard_Load(object sender, EventArgs e)
        {
            this.sum_Count_ViewTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            this.tableAdapterManager.Connection = this.sum_Count_ViewTableAdapter.Connection;
            
            this.sum_Count_ViewTableAdapter.Fill(this.sumCard_DataSet.Sum_Count_View);

            AddSummationRow(sum_Count_ViewBindingSource, sum_Count_ViewDataGridView);

        }

        private void AddSummationRow(BindingSource bs, DataGridView dg)
        {
            //this.yX_daywork_recordDataGridView1.Columns[6].summation
            //this.dailyoperation.YX_daywork_record.Columns[7].
            if (null == dg.CurrentRow) return;

            decimal sum_allmoney = 0;
            decimal sum_stdmoney = 0;
            decimal sum_cardbalance = 0;
            decimal sum_carddiscount = 0;
            decimal sum_allincomemoney = 0;
            decimal sum_realmoney = 0;
            decimal sum_realbalance = 0;
            decimal sum_hadreadstdmoney = 0;
            decimal sum_hadreadrealmoney = 0;
            decimal sum_hadreaddiscount = 0;



            foreach (DataGridViewRow dr in dg.Rows)
            {
                if (System.DBNull.Value == dr.Cells["卡面金额"].Value)
                    sum_allmoney = sum_allmoney + 0;
                else
                    sum_allmoney = sum_allmoney + decimal.Parse(dr.Cells["卡面金额"].Value.ToString());

                if (System.DBNull.Value == dr.Cells["标准学费"].Value)
                    sum_stdmoney = sum_stdmoney + 0;
                else
                    sum_stdmoney = sum_stdmoney + decimal.Parse(dr.Cells["标准学费"].Value.ToString());

                if (System.DBNull.Value == dr.Cells["卡面余额"].Value)
                    sum_cardbalance = sum_cardbalance + 0;
                else
                    sum_cardbalance = sum_cardbalance + decimal.Parse(dr.Cells["卡面余额"].Value.ToString());


                if (System.DBNull.Value == dr.Cells["本卡折扣"].Value)
                    sum_carddiscount = sum_carddiscount + 0;
                else
                    sum_carddiscount = sum_carddiscount + decimal.Parse(dr.Cells["本卡折扣"].Value.ToString());

                if (System.DBNull.Value == dr.Cells["实充金额"].Value)
                    sum_allincomemoney = sum_allincomemoney + 0;
                else
                    sum_allincomemoney = sum_allincomemoney + decimal.Parse(dr.Cells["实充金额"].Value.ToString());


                if (System.DBNull.Value == dr.Cells["实收学费"].Value)
                    sum_realmoney = sum_realmoney + 0;
                else
                    sum_realmoney = sum_realmoney + decimal.Parse(dr.Cells["实收学费"].Value.ToString());

                if (System.DBNull.Value == dr.Cells["已上应收"].Value)
                    sum_hadreadstdmoney = sum_hadreadstdmoney + 0;
                else
                    sum_hadreadstdmoney = sum_hadreadstdmoney + decimal.Parse(dr.Cells["已上应收"].Value.ToString());

                if (System.DBNull.Value == dr.Cells["已上实收"].Value)
                    sum_hadreadrealmoney = sum_hadreadrealmoney + 0;
                else
                    sum_hadreadrealmoney = sum_hadreadrealmoney + decimal.Parse(dr.Cells["已上实收"].Value.ToString());

                if (System.DBNull.Value == dr.Cells["已上折扣"].Value)
                    sum_hadreaddiscount = sum_hadreaddiscount + 0;
                else
                    sum_hadreaddiscount = sum_hadreaddiscount + decimal.Parse(dr.Cells["已上折扣"].Value.ToString());


                //if ( 0 >  decimal.Parse(dr.Cells[9].Value .ToString()))
                //dr.DefaultCellStyle.BackColor = Color.Red;

            }
            bs.AddNew();

            dg.CurrentRow.Cells["卡号"].Value = "合计";
            dg.CurrentRow.Cells["卡面金额"].Value = sum_allmoney;
            dg.CurrentRow.Cells["标准学费"].Value = sum_stdmoney;
            dg.CurrentRow.Cells["卡面余额"].Value = sum_cardbalance;
            dg.CurrentRow.Cells["本卡折扣"].Value = sum_carddiscount;
            dg.CurrentRow.Cells["实充金额"].Value = sum_allincomemoney;
            dg.CurrentRow.Cells["实收学费"].Value = sum_realmoney;
            dg.CurrentRow.Cells["已上应收"].Value = sum_hadreadstdmoney;
            dg.CurrentRow.Cells["已上实收"].Value = sum_hadreadrealmoney;
            dg.CurrentRow.Cells["已上折扣"].Value = sum_hadreaddiscount;

            bs.EndEdit();

            bs.Position = 0;




        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            ExportDataGridview(sum_Count_ViewDataGridView, true);
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
    }
}
