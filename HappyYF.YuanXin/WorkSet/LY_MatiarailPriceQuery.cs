using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Transactions;
using System.Windows.Forms;
using HappyYF.Infrastructure.Repositories;
using DataGridFilter;

namespace HappyYF.YuanXin.WorkSet
{
    public partial class LY_MatiarailPriceQuery : Form
    {

        public string material_code="queryform";

        public void retrieveitemPrice(string nowitemcode)
        {
            this.splitContainer1.Panel1Collapsed = true;
            this.ly_itemPurchasepriceTableAdapter.Fill(this.genQuey.ly_itemPurchaseprice, nowitemcode);
        
        }

        public LY_MatiarailPriceQuery()
        {
            InitializeComponent();
        }

       
        private void LY_MatiarailPriceQuery_Load(object sender, EventArgs e)
        {
            this.ly_itemPurchasepriceTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            this.ly_purchase_contract_detailTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;


            if ("queryform" == material_code)
            {
                this.ly_purchase_contract_detailTableAdapter.Fill(this.genQuey.ly_purchase_contract_detail);
            }
            else
            {
                this.splitContainer1.Panel1Collapsed = true;
                this.ly_itemPurchasepriceTableAdapter.Fill(this.genQuey.ly_itemPurchaseprice, material_code);
            }

        }

        private void ly_purchase_contract_detailDataGridView_SelectionChanged(object sender, EventArgs e)
        {

            if (null == this.ly_purchase_contract_detailDataGridView.CurrentRow)
            {
                this.ly_itemPurchasepriceTableAdapter.Fill(this.genQuey.ly_itemPurchaseprice, "");
                return;
            }
            
            string nowitemcode = this.ly_purchase_contract_detailDataGridView.CurrentRow.Cells["编码"].Value.ToString();

            this.ly_itemPurchasepriceTableAdapter.Fill(this.genQuey.ly_itemPurchaseprice, nowitemcode);
        }

        private void ly_purchase_contract_detailDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void toolStripTextBox5_KeyUp(object sender, KeyEventArgs e)
        {
            string filterString;


            filterString = GetDataGridviewMultiFilterString.DGVMultiFilterString(this.ly_purchase_contract_detailDataGridView, this.toolStripTextBox5.Text);


            this.ly_purchase_contract_detailBindingSource.Filter = filterString;
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            ExportDataGridviewTOExcell.ExportDataGridview(this.ly_purchase_contract_detailDataGridView, true);
        }

        private void ly_itemPurchasepriceDataGridView_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            DataGridView dgv = sender as DataGridView;

            if (null == dgv.CurrentRow) return;

            if (!SQLDatabase.CheckHaveRight(SQLDatabase.NowUserID, "采购单价忽略"))
            {

                MessageBox.Show("无采购单价忽略权限...", "注意");
                return;
            }

            string salespeople = dgv.CurrentRow.Cells["忽略设置"].Value.ToString();

            if (!string .IsNullOrEmpty(salespeople) && salespeople != SQLDatabase.nowUserName())
            {
                MessageBox.Show("请设置人:" + salespeople + "取消", "注意");
                return;
            }

            //if ("True" == ly_sales_contract_mainDataGridView.CurrentRow.Cells["批准"].Value.ToString()
            //      && "中成折扣" != dgv.CurrentCell.OwningColumn.Name
            //      )
            //{
            //    MessageBox.Show("合同已经执行,不能修改数据...", "注意");
            //    return;

            //}

            string nowid = dgv.CurrentRow.Cells["id"].Value.ToString();
            string nowsort = dgv.CurrentRow.Cells["sort1"].Value.ToString();
            string nowflag;


            if ("True" == dgv.CurrentRow.Cells["忽略计价"].Value.ToString())
            {
                dgv.CurrentRow.Cells["忽略计价"].Value = "False";
                dgv.CurrentRow.Cells["忽略设置"].Value = DBNull.Value;
                nowflag = "0";
              
            }
            else
            {

                dgv.CurrentRow.Cells["忽略计价"].Value = "True";
                dgv.CurrentRow.Cells["忽略设置"].Value = SQLDatabase.nowUserName();
                nowflag = "1";
            }

           
            string nowupdStr;

            if ("2" == nowsort)
            {
                if ("1" == nowflag)
                {

                    nowupdStr = " update ly_outsource_contract_detail  " +
                                 "  set ignore_price=" + nowflag + ", ignore_price_people='" + SQLDatabase.nowUserName() + "'"
                                 + " where  id=" + nowid;
                }
                else
                {
                    nowupdStr = " update ly_outsource_contract_detail  " +
                                "  set ignore_price=" + nowflag + ", ignore_price_people=null " + " where  id=" + nowid;
                }
            }
            else if ("3" == nowsort)
            {
                if ("1" == nowflag)
                {
                    nowupdStr = " update ly_purchase_contract_detail  " +
                                            "  set ignore_price=" + nowflag + ", ignore_price_people='" + SQLDatabase.nowUserName() + "'"
                                            + " where  id=" + nowid;
                }
                else

                {
                    nowupdStr = " update ly_purchase_contract_detail  " +
                                           "  set ignore_price=" + nowflag + ", ignore_price_people=null "         + " where  id=" + nowid;
                }
            }
            else
            {

                return;
            }



            SqlConnection sqlConnection0 = new SqlConnection(SQLDatabase.Connectstring);
            SqlCommand cmd0 = new SqlCommand();

            cmd0.CommandText = nowupdStr;
            cmd0.CommandType = CommandType.Text;
            cmd0.Connection = sqlConnection0;
            cmd0.CommandTimeout = 0;

            TransactionOptions tOpt0 = new TransactionOptions();

            //tOpt.IsolationLevel = IsolationLevel.ReadCommitted; //设置TransactionOptions模式

            tOpt0.Timeout = new TimeSpan(5, 5, 0); ; // 设置超时时间为5分钟   new TimeSpan(0, 5, 0);                       
            //using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required, tOpt))


            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, tOpt0))
            {


                sqlConnection0.Open();
                try
                {

                    cmd0.ExecuteNonQuery();



                    //



                }
                catch (SqlException sqle)
                {


                    MessageBox.Show(sqle.Message.Split('*')[0]);
                }


                finally
                {
                    sqlConnection0.Close();


                }

                scope.Complete();
            }


        }

        private void ly_itemPurchasepriceDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
