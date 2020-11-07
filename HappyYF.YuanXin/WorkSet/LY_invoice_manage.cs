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
using DataGridFilter;

namespace HappyYF.YuanXin.WorkSet
{
    public partial class LY_invoice_manage : Form
    {


        public LY_invoice_manage()
        {
            InitializeComponent();
        }

        private void LY_Machine_Load(object sender, EventArgs e)
        {
            this.ly_invoice_contrat_lockTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            this.ly_store_inInvoiceNewTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            this.ly_invoice_payTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            this.dateTimePicker1.Text = SQLDatabase.GetNowdate().AddMonths(-3).Date.ToString();
            this.dateTimePicker2.Text = SQLDatabase.GetNowdate().AddDays(0).Date.ToString();

            this.ly_invoice_contrat_lock_outsourceTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            this.ly_store_inInvoice_outsourceTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            this.dateTimePicker3.Text = SQLDatabase.GetNowdate().AddMonths(-3).Date.ToString();
            this.dateTimePicker4.Text = SQLDatabase.GetNowdate().AddDays(0).Date.ToString();

            this.ly_invoice_contrat_lock_machineTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            ly_outmachine_contract_detail_selTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            this.dateTimePicker5.Text = SQLDatabase.GetNowdate().AddMonths(-3).Date.ToString();
            this.dateTimePicker6.Text = SQLDatabase.GetNowdate().AddDays(0).Date.ToString();

        }







        private void toolStripButton53_Click(object sender, EventArgs e)
        {
            this.ly_invoice_contrat_lockTableAdapter.Fill(this.lYMaterielRequirements.ly_invoice_contrat_lock, this.dateTimePicker1.Value, this.dateTimePicker2.Value.AddDays(1));
            if (SQLDatabase.CheckHaveRight(SQLDatabase.NowUserID, "发票总管理权限"))
            {
          
            }
            else
            {
                string salespeople = SQLDatabase.nowUserName();
                lyinvoicecontratlockBindingSource.Filter = "采购员='"+ salespeople + "'";

            }
         


        }

        private void toolStripTextBox1_KeyUp(object sender, KeyEventArgs e)
        {

            string filterString;


            filterString = GetDataGridviewMultiFilterString.DGVMultiFilterString(this.ly_cg, this.toolStripTextBox1.Text);

            if (SQLDatabase.CheckHaveRight(SQLDatabase.NowUserID, "发票总管理权限"))
            {
                this.lyinvoicecontratlockBindingSource.Filter = "(" + filterString + ")";
                //管理员和财务部可以看到全部信息
            }
            else
            {
                string salespeople = SQLDatabase.nowUserName();
                this.lyinvoicecontratlockBindingSource.Filter = "(" + filterString + ") and 采购员='" + salespeople + "'";
       

            }

            
        }

        private void toolStripTextBox1_Enter(object sender, EventArgs e)
        {
            toolStripTextBox1.Text = "";
            if (SQLDatabase.CheckHaveRight(SQLDatabase.NowUserID, "发票总管理权限"))
            {
                this.lyinvoicecontratlockBindingSource.Filter = " ";

                //管理员和财务部可以看到全部信息
            }
            else
            {
                string salespeople = SQLDatabase.nowUserName();
                lyinvoicecontratlockBindingSource.Filter = "采购员='" + salespeople + "'";

            }

        }

        private void ly_cg_SelectionChanged(object sender, EventArgs e)
        {
            if (ly_cg.CurrentRow == null)
            {
                this.ly_store_inInvoiceNewTableAdapter.Fill(this.lYStoreMange.ly_store_inInvoice_New, "");
                this.ly_invoice_payTableAdapter.Fill(this.lYStoreMange.ly_invoice_pay, -1);
            }
            else
            {
                string concode = ly_cg.CurrentRow.Cells["合同编号cg"].Value.ToString();
                string invoice_id = ly_cg.CurrentRow.Cells["fpid"].Value.ToString();
                string contract_id = ly_cg.CurrentRow.Cells["contract_id"].Value.ToString();
                if (string.IsNullOrEmpty(invoice_id))
                {
                    this.ly_store_inInvoiceNewTableAdapter.Fill(this.lYStoreMange.ly_store_inInvoice_New, "");
                }
                else
                {
                    this.ly_store_inInvoiceNewTableAdapter.Fill(this.lYStoreMange.ly_store_inInvoice_New, concode);
                    this.lystoreinInvoiceNewBindingSource.Filter = "invoice_id=" + invoice_id;
                }
                this.ly_invoice_payTableAdapter.Fill(this.lYStoreMange.ly_invoice_pay, int.Parse(contract_id));
            }
        }

        private void toolStripButton11_Click(object sender, EventArgs e)
        {
            //this.ly_invoice_contrat_lock_outsourceTableAdapter.Fill(this.lYMaterielRequirements.ly_invoice_contrat_lock_outsource, this.dateTimePicker3.Value, this.dateTimePicker4.Value.AddDays(1));

        }

        private void ly_wx_SelectionChanged(object sender, EventArgs e)
        {

            if (ly_wx.CurrentRow == null)
            {
                this.ly_store_inInvoice_outsourceTableAdapter.Fill(this.lYStoreMange.ly_store_inInvoice_outsource, "");
                this.ly_invoice_payTableAdapter.Fill(this.lYStoreMange.ly_invoice_pay, -1);
            }
            else
            {
                string concode = ly_wx.CurrentRow.Cells["合同编号1"].Value.ToString();
                string invoice_id = ly_wx.CurrentRow.Cells["发票id1"].Value.ToString();

                this.ly_store_inInvoice_outsourceTableAdapter.Fill(this.lYStoreMange.ly_store_inInvoice_outsource, concode);
                this.lystoreinInvoiceoutsourceBindingSource.Filter = "invoice_id=" + invoice_id;
                this.ly_invoice_payTableAdapter.Fill(this.lYStoreMange.ly_invoice_pay, int.Parse(invoice_id));
            }
        }
        private string GetMaxPay(int k)
        {
            SqlConnection sqlConnection = new SqlConnection(SQLDatabase.Connectstring);
            SqlCommand cmd = new SqlCommand();

            string Maxmoeny= "";

            cmd.Parameters.Add("@contarct_id", SqlDbType.Int);
            cmd.Parameters["@contarct_id"].Value =k;


            cmd.CommandText = "LY_Get_MaxPayMoney";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = sqlConnection;

            sqlConnection.Open();
            Maxmoeny = cmd.ExecuteScalar().ToString();
            sqlConnection.Close();



            return Maxmoeny;
        }

 

        

        private void toolStripTextBox4_Enter(object sender, EventArgs e)
        {
            toolStripTextBox4.Text = "";

            this.lyinvoicecontratlockoutsourceBindingSource.Filter = " ";

        }

        private void toolStripTextBox4_KeyUp(object sender, KeyEventArgs e)
        {
            string filterString;


            filterString = GetDataGridviewMultiFilterString.DGVMultiFilterString(this.ly_wx, this.toolStripTextBox4.Text);

            this.lyinvoicecontratlockoutsourceBindingSource.Filter = "(" + filterString + ")";
        }

        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            if (ly_cg_fk.CurrentRow == null)
                return;
            if (this.ly_cg_fk.CurrentRow.Cells["审批"].Value.ToString() == "True")
            {
                MessageBox.Show("已经审批...", "注意");
                return;
            }
            string salespeople = this.ly_cg_fk.CurrentRow.Cells["申请人"].Value.ToString();

            if (salespeople != SQLDatabase.nowUserName())
            {
                MessageBox.Show("请申请人:" + salespeople + "删除", "注意");
                return;
            }


            this.lyinvoicepayBindingSource.RemoveCurrent();
            this.ly_cg_fk.EndEdit();
            this.lyinvoicepayBindingSource.EndEdit();
            this.ly_invoice_payTableAdapter.Update(this.lYStoreMange.ly_invoice_pay);

        }

        private void toolStripButton23_Click(object sender, EventArgs e)
        {
            if (ly_wx_fk.CurrentRow == null)
                return;
            if (this.ly_wx_fk.CurrentRow.Cells["审批1"].Value.ToString() == "True")
            {
                MessageBox.Show("已经锁定...", "注意");
                return;
            }
            string salespeople = this.ly_wx_fk.CurrentRow.Cells["操作人1"].Value.ToString();

            if (salespeople != SQLDatabase.nowUserName())
            {
                MessageBox.Show("请操作人:" + salespeople + "删除", "注意");
                return;
            }


            this.lyinvoicepayBindingSource.RemoveCurrent();
            this.ly_cg_fk.EndEdit();
            this.lyinvoicepayBindingSource.EndEdit();
            this.ly_invoice_payTableAdapter.Update(this.lYStoreMange.ly_invoice_pay);
        }

        private void ly_cg_fk_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (ly_cg_fk.CurrentRow == null)
                return;
             
        }

        private void ly_wx_fk_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (ly_wx_fk.CurrentRow == null)
                return;
            if (this.ly_wx_fk.CurrentRow.Cells["审批1"].Value.ToString() == "True")
            {
                MessageBox.Show("已经锁定...", "注意");
                return;
            }
            string salespeople = this.ly_wx_fk.CurrentRow.Cells["操作人1"].Value.ToString();

            if (salespeople != SQLDatabase.nowUserName())
            {
                MessageBox.Show("请操作人:" + salespeople + "修改", "注意");
                return;
            }

            DataGridView dgv = sender as DataGridView;


            if ("pay_time1" == dgv.CurrentCell.OwningColumn.Name)
            {

                DatePicker queryForm = new DatePicker();


                if (null != (dgv.CurrentCell.Value))
                    queryForm.NowDate = dgv.CurrentCell.Value.ToString();
                else
                    queryForm.NowDate = SQLDatabase.GetNowdate().Date.ToString();



                queryForm.ShowDialog();
                if (!string.IsNullOrEmpty(queryForm.NowDate))
                {

                    dgv.CurrentRow.Cells["pay_time1"].Value = queryForm.NowDate;
                }
                this.ly_wx_fk.EndEdit();
                this.lyinvoicepayBindingSource.EndEdit();
                this.ly_invoice_payTableAdapter.Update(this.lYStoreMange.ly_invoice_pay);
                return;
            }
        }

        private void ly_cg_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (ly_cg.CurrentRow == null)
            {
                return;
            }
            if (!SQLDatabase.CheckHaveRight(SQLDatabase.NowUserID, "发票总管理权限"))
            {
                MessageBox.Show("无权限锁定", "注意");
                return;
            }

            DataGridView dgv = sender as DataGridView;
            if ("锁定" == dgv.CurrentCell.OwningColumn.Name)
            {
                string sql = "";
                if ("True" == dgv.CurrentRow.Cells["锁定"].Value.ToString())
                {
                    dgv.CurrentRow.Cells["锁定"].Value = "False";
                    dgv.CurrentRow.Cells["锁定人"].Value = DBNull.Value;
                    dgv.CurrentRow.Cells["锁定时间"].Value = DBNull.Value;

                    sql = @"update   ly_invoice_contract  SET is_lock =null  ,is_lock_time =null  ,is_lock_people =null  where id =" + dgv.CurrentRow.Cells["fpid"].Value.ToString();

                }
                else
                {
                    dgv.CurrentRow.Cells["锁定"].Value = "True";
                    dgv.CurrentRow.Cells["锁定人"].Value = SQLDatabase.nowUserName();
                    dgv.CurrentRow.Cells["锁定时间"].Value = SQLDatabase.GetNowtime();
                    sql = @"update   ly_invoice_contract  SET is_lock =1 ,is_lock_time ='" + SQLDatabase.GetNowtime() + "'  ,is_lock_people ='" + SQLDatabase.nowUserName() + "'  where id =" + dgv.CurrentRow.Cells["fpid"].Value.ToString();

                }


                using (SqlConnection con = new SqlConnection(SQLDatabase.Connectstring))
                {

                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {

                        con.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
                return;
            }
        }

        private void ly_wx_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (ly_wx.CurrentRow == null)
            {
                return;
            }
            if (!SQLDatabase.CheckHaveRight(SQLDatabase.NowUserID, "发票总管理权限"))
            {
                MessageBox.Show("无权限", "注意");
                return;
            }

            DataGridView dgv = sender as DataGridView;
            if ("锁定1" == dgv.CurrentCell.OwningColumn.Name)
            {
                string sql = "";
                if ("True" == dgv.CurrentRow.Cells["锁定1"].Value.ToString())
                {
                    dgv.CurrentRow.Cells["锁定1"].Value = "False";
                    dgv.CurrentRow.Cells["锁定人1"].Value = DBNull.Value;
                    dgv.CurrentRow.Cells["锁定时间1"].Value = DBNull.Value;
                    sql = @"update   ly_invoice_contract  SET is_lock =null  ,is_lock_time =null  ,is_lock_people =null  where id =" + dgv.CurrentRow.Cells["发票id1"].Value.ToString();

                }
                else
                {

                    dgv.CurrentRow.Cells["锁定1"].Value = "True";
                    dgv.CurrentRow.Cells["锁定人1"].Value = SQLDatabase.nowUserName();
                    dgv.CurrentRow.Cells["锁定时间1"].Value = SQLDatabase.GetNowtime();
                    sql = @"update   ly_invoice_contract  SET is_lock =1 ,is_lock_time ='" + SQLDatabase.GetNowtime() + "'  ,is_lock_people ='" + SQLDatabase.nowUserName() + "'  where id =" + dgv.CurrentRow.Cells["发票id1"].Value.ToString();

                }

                using (SqlConnection con = new SqlConnection(SQLDatabase.Connectstring))
                {

                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {

                        con.Open();
                        cmd.ExecuteNonQuery();
                    }
                }

                return;
            }
        }

        private void toolStripButton28_Click(object sender, EventArgs e)
        {
            //this.ly_invoice_contrat_lock_machineTableAdapter.Fill(this.lYMaterielRequirements.ly_invoice_contrat_lock_machine, this.dateTimePicker5.Value, this.dateTimePicker6.Value.AddDays(1));

        }

        private void ly_machine_SelectionChanged(object sender, EventArgs e)
        {

            if (ly_machine.CurrentRow == null)
            {
                this.ly_outmachine_contract_detail_selTableAdapter.Fill(this.lYProductMange.ly_outmachine_contract_detail_sel, "");
                this.ly_invoice_payTableAdapter.Fill(this.lYStoreMange.ly_invoice_pay, -1);
            }
            else
            {
                string concode = ly_machine.CurrentRow.Cells["合同编号2"].Value.ToString();
                string invoice_id = ly_machine.CurrentRow.Cells["发票id2"].Value.ToString();
                this.ly_outmachine_contract_detail_selTableAdapter.Fill(this.lYProductMange.ly_outmachine_contract_detail_sel, concode);

                this.lyoutmachinecontractdetailselBindingSource.Filter = "发票id=" + invoice_id;
                this.ly_invoice_payTableAdapter.Fill(this.lYStoreMange.ly_invoice_pay, int.Parse(invoice_id));
            }
        }

        private void ly_machine_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (ly_machine.CurrentRow == null)
            {
                return;
            }

            if (!SQLDatabase.CheckHaveRight(SQLDatabase.NowUserID, "发票总管理权限"))
            {
                MessageBox.Show("无权限", "注意");
                return;
            }
            DataGridView dgv = sender as DataGridView;
            if ("锁定2" == dgv.CurrentCell.OwningColumn.Name)
            {
                string sql = "";
                if ("True" == dgv.CurrentRow.Cells["锁定2"].Value.ToString())
                {
                    dgv.CurrentRow.Cells["锁定2"].Value = "False";
                    dgv.CurrentRow.Cells["锁定人2"].Value = DBNull.Value;
                    dgv.CurrentRow.Cells["锁定时间2"].Value = DBNull.Value;
                    sql = @"update   ly_invoice_contract  SET is_lock =null  ,is_lock_time =null  ,is_lock_people =null  where id =" + dgv.CurrentRow.Cells["发票id2"].Value.ToString();

                }
                else
                {

                    dgv.CurrentRow.Cells["锁定2"].Value = "True";
                    dgv.CurrentRow.Cells["锁定人2"].Value = SQLDatabase.nowUserName();
                    dgv.CurrentRow.Cells["锁定时间2"].Value = SQLDatabase.GetNowtime();
                    sql = @"update   ly_invoice_contract  SET is_lock =1 ,is_lock_time ='" + SQLDatabase.GetNowtime() + "'  ,is_lock_people ='" + SQLDatabase.nowUserName() + "'  where id =" + dgv.CurrentRow.Cells["发票id2"].Value.ToString();

                }

                using (SqlConnection con = new SqlConnection(SQLDatabase.Connectstring))
                {

                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {

                        con.Open();
                        cmd.ExecuteNonQuery();
                    }
                }

                return;
            }
        }

        private void toolStripTextBox8_Enter(object sender, EventArgs e)
        {
            toolStripTextBox8.Text = "";

            this.lyinvoicecontratlockmachineBindingSource.Filter = " ";
        }

        private void toolStripTextBox8_KeyUp(object sender, KeyEventArgs e)
        {
            string filterString;


            filterString = GetDataGridviewMultiFilterString.DGVMultiFilterString(this.ly_machine, this.toolStripTextBox8.Text);

            this.lyinvoicecontratlockmachineBindingSource.Filter = "(" + filterString + ")";
        }

     

        private void toolStripButton40_Click(object sender, EventArgs e)
        {

            if (ly_machine_rk.CurrentRow == null)
                return;
            if (this.ly_machine_rk.CurrentRow.Cells["审批2"].Value.ToString() == "True")
            {
                MessageBox.Show("已经锁定...", "注意");
                return;
            }
            string salespeople = this.ly_machine_rk.CurrentRow.Cells["操作人2"].Value.ToString();

            if (salespeople != SQLDatabase.nowUserName())
            {
                MessageBox.Show("请操作人:" + salespeople + "删除", "注意");
                return;
            }


            this.lyinvoicepayBindingSource.RemoveCurrent();
            this.ly_machine_rk.EndEdit();
            this.lyinvoicepayBindingSource.EndEdit();
            this.ly_invoice_payTableAdapter.Update(this.lYStoreMange.ly_invoice_pay);
        }

        private void ly_machine_rk_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (ly_machine_rk.CurrentRow == null)
                return;
            if (this.ly_machine_rk.CurrentRow.Cells["审批2"].Value.ToString() == "True")
            {
                MessageBox.Show("已经锁定...", "注意");
                return;
            }
            string salespeople = this.ly_machine_rk.CurrentRow.Cells["操作人2"].Value.ToString();

            if (salespeople != SQLDatabase.nowUserName())
            {
                MessageBox.Show("请操作人:" + salespeople + "修改", "注意");
                return;
            }

            DataGridView dgv = sender as DataGridView;


            if ("pay_time2" == dgv.CurrentCell.OwningColumn.Name)
            {

                DatePicker queryForm = new DatePicker();


                if (null != (dgv.CurrentCell.Value))
                    queryForm.NowDate = dgv.CurrentCell.Value.ToString();
                else
                    queryForm.NowDate = SQLDatabase.GetNowdate().Date.ToString();



                queryForm.ShowDialog();
                if (!string.IsNullOrEmpty(queryForm.NowDate))
                {

                    dgv.CurrentRow.Cells["pay_time2"].Value = queryForm.NowDate;
                }
                this.ly_machine_rk.EndEdit();
                this.lyinvoicepayBindingSource.EndEdit();
                this.ly_invoice_payTableAdapter.Update(this.lYStoreMange.ly_invoice_pay);
                return;
            }
        }

        private void toolStripButton16_Click(object sender, EventArgs e)
        {
            ExportDataGridviewTOExcell.ExportDataGridview(this.ly_cg, true);
        }
 
        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            //LY_pay_money queryForm = new LY_pay_money();
  


            //queryForm.StartPosition = FormStartPosition.CenterParent;
            //queryForm.ShowDialog();

            //if (queryForm.DialogResult != DialogResult.Cancel)
            //{
            //    tabControl1.SelectedIndex = 1;
            //    this.lyinvoicecontratlockBindingSource.Position = this.lyinvoicecontratlockBindingSource.Find("contract_id", queryForm.win_contract_id);
            //    this.ly_invoice_payTableAdapter.Fill(this.lYStoreMange.ly_invoice_pay, int.Parse(queryForm.win_contract_id));

            //  //  toolStripTextBox1.Text= queryForm.win_contract_id;
            //}
        }
    }
}
