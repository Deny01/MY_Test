using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HappyYF.Infrastructure.Repositories;
using DataGridFilter;
 
using System.Data.SqlClient;

namespace HappyYF.YuanXin.WorkSet
{
    public partial class ly_invoiceMg : Form
    {
        public ly_invoiceMg()
        {
            InitializeComponent();
            this.ly_invoiceTableAdapter.CommandTimeout = 0;
        }

        protected void loadData()
        {
            string nowdatestyle;

                if (this.radioButton1.Checked)
            {
                nowdatestyle = "录入";
            }
            else 
            {
                nowdatestyle = "锁定";
            }

            NewFrm.Show(this);
          
            this.ly_invoiceTableAdapter.Fill(this.lYMaterielRequirements.ly_invoice, DateTime.Parse(this.dateTimePicker1.Text).Date, DateTime.Parse(this.dateTimePicker2.Text).Date, nowdatestyle);

            if (SQLDatabase.CheckHaveRight(SQLDatabase.NowUserID, "发票总管理权限"))
            {
                if ("0002" == SQLDatabase.nowUserDepartmentBig())
                {

                    this.lyinvoiceBindingSource.Filter = "submit=True";
                }
            }
            else
            {
                string salespeople = SQLDatabase.nowUserName();
                lyinvoiceBindingSource.Filter = "sava_people='" + salespeople + "'";
            }
            NewFrm.Hide(this);
        }

        private void LY_Manufacturing_procedure_Manage_Load(object sender, EventArgs e)
        {
            this.dateTimePicker1.MinDate = DateTime.Parse("2019-05-01");
            this.dateTimePicker1.Text = DateTime.Today.AddDays(-3).Date.ToString();
            this.dateTimePicker2.Text = DateTime.Today.AddDays(1).Date.ToString();


            this.ly_invoice_detail_NewTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            this.lY_Invoice_Instore_BindlistTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            this.ly_invoiceTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            ly_invoice_contract_byvcodeTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            ly_store_inInvoiceNewTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            ly_store_inInvoice_New_BindTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            // TODO: 这行代码将数据加载到表“lYMaterielRequirements.ly_invoice”中。您可以根据需要移动或删除它。

            //if ("0002" == SQLDatabase.nowUserDepartmentBig())
            //{

            //    this.lyinvoiceBindingSource.Filter = "submit=True";
            //}
            loadData();
        }
        #region
        private void ly_manufacturing_procedureDataGridView_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (null == ly_fpDataGridView.CurrentRow)
            {

                return;
            }
            DataGridView dgv = sender as DataGridView;
            if (SQLDatabase.CheckHaveRight(SQLDatabase.NowUserID, "发票总管理权限"))
            {
                if ("lock_flag" == dgv.CurrentCell.OwningColumn.Name)
                {
                     
                 

                    if ("True" == dgv.CurrentRow.Cells["lock_flag"].Value.ToString())
                    {

                        ////----------------------
                        //string sql = "SELECT  a.contract_code, c.confirm_flag FROM ly_payable_item_detail AS a left join ly_payable_plan c on a.payable_plan_num = c.payable_plan_num WHERE  c.confirm_flag=1";
                        //DataTable dt = null;
                        //using (SqlConnection connection = new SqlConnection(SQLDatabase.Connectstring))
                        //{

                        //    SqlDataAdapter adapter = new SqlDataAdapter(sql, connection);
                        //    DataSet ds = new DataSet();
                        //    adapter.Fill(ds);
                        //    dt = ds.Tables[0];
                        //}
                        //for (int i = 0; i < dt.Rows.Count; i++)
                        //{
                        //    for (int j = 0; j < ly_invoice_detailDataGridView.Rows.Count; j++)
                        //    {
                        //        if (dt.Rows[i]["contract_code"].ToString() == ly_invoice_detailDataGridView.Rows[j].Cells["contractcode0"].Value.ToString())
                        //        {

                        //            MessageBox.Show("已有付款计划不可操作！", "注意");
                        //            return;
                        //        }

                        //    }
                        //}
                        //-------------------



                        //dgv.CurrentRow.Cells["lock_flag"].Value = "False";
                        //dgv.CurrentRow.Cells["lock_people"].Value = DBNull.Value;
                        //dgv.CurrentRow.Cells["lock_time"].Value = DBNull.Value;

                        if (SQLDatabase.CheckHaveRight(SQLDatabase.NowUserID, "采购预付付款取消"))
                        {
                            dgv.CurrentRow.Cells["lock_flag"].Value = "False";
                            dgv.CurrentRow.Cells["lock_people"].Value = DBNull.Value;
                            dgv.CurrentRow.Cells["lock_time"].Value = DBNull.Value;
                        }
                        else
                        {

                            MessageBox.Show("无发票锁定取消权限...", "注意");

                            return;
                        }

                    }
                    else
                    {


                        if ("False" == dgv.CurrentRow.Cells["提交"].Value.ToString() || "" == dgv.CurrentRow.Cells["提交"].Value.ToString().Trim())
                        {

                            MessageBox.Show("采购员未提交不可锁定！", "注意");
                            return;
                        }

                        if (decimal.Round(decimal.Parse(dgv.CurrentRow.Cells["invoice_money_novat"].Value.ToString().Trim()), 1)
                            <= decimal.Round(decimal.Parse(dgv.CurrentRow.Cells["bind_money_ovat"].Value.ToString().Trim()), 1)

                            ||

                            decimal.Round(decimal.Parse(dgv.CurrentRow.Cells["发票金额"].Value.ToString().Trim()), 1)
                            <= decimal.Round(decimal.Parse(dgv.CurrentRow.Cells["绑定金额"].Value.ToString().Trim()), 1)
                            )
                        {




                            if (decimal.Round(decimal.Parse(dgv.CurrentRow.Cells["bind_money_ovat"].Value.ToString().Trim()), 1) -
                                decimal.Round(decimal.Parse(dgv.CurrentRow.Cells["invoice_money_novat"].Value.ToString().Trim()), 1) > 10
                                &&
                                decimal.Round(decimal.Parse(dgv.CurrentRow.Cells["绑定金额"].Value.ToString().Trim()), 1) -
                                decimal.Round(decimal.Parse(dgv.CurrentRow.Cells["发票金额"].Value.ToString().Trim()), 1) > 10)

                            {
                                MessageBox.Show("本次发票绑定金额异常过大，请联系管理员排查！", "注意");
                                return;
                            }
                            else
                            {
                                dgv.CurrentRow.Cells["lock_flag"].Value = "True";
                                dgv.CurrentRow.Cells["lock_people"].Value = SQLDatabase.nowUserName();
                                dgv.CurrentRow.Cells["lock_time"].Value = SQLDatabase.GetNowtime();
                            }




                        }
                        else
                        {
                            MessageBox.Show("发票金额与绑定金额不一致！", "注意");
                            return;
                        }


                      
                    }
                    save();
                    return;
                }
            }

            string salespeople = this.ly_fpDataGridView.CurrentRow.Cells["录入人"].Value.ToString();
            if (!string.IsNullOrEmpty(salespeople))
            {
                if (salespeople != SQLDatabase.nowUserName())
                {
                    MessageBox.Show("请录入人:" + salespeople + "修改", "注意");
                    return;
                }
            }

            //加锁
            if (ly_fpDataGridView.CurrentRow.Cells["lock_flag"].Value.ToString() == "True")
            {
                MessageBox.Show("已经锁定无法操作...", "注意");
                return;
            }

            if ("提交" == dgv.CurrentCell.OwningColumn.Name)
            {

                if ("True" == dgv.CurrentRow.Cells["提交"].Value.ToString())
                {
                    dgv.CurrentRow.Cells["提交"].Value = "False";
                    save();

                }
                else
                {
                    if (
                        //decimal.Round(decimal.Parse(dgv.CurrentRow.Cells["invoice_money_novat"].Value.ToString().Trim()),1)
                        //    <= decimal.Round(decimal.Parse(dgv.CurrentRow.Cells["bind_money_ovat"].Value.ToString().Trim()), 1)
                        //    ||

                            decimal.Round(decimal.Parse(dgv.CurrentRow.Cells["发票金额"].Value.ToString().Trim()), 2)
                            != decimal.Round(decimal.Parse(dgv.CurrentRow.Cells["绑定金额"].Value.ToString().Trim()), 2)  )
                    {
                        MessageBox.Show("本次发票绑定金额和发票金额不相等，请检查！", "注意");
                        return;




                        if (decimal.Round(decimal.Parse(dgv.CurrentRow.Cells["bind_money_ovat"].Value.ToString().Trim()), 1) -
                            decimal.Round(decimal.Parse(dgv.CurrentRow.Cells["invoice_money_novat"].Value.ToString().Trim()), 1) > 10
                            &&
                            decimal.Round(decimal.Parse(dgv.CurrentRow.Cells["绑定金额"].Value.ToString().Trim()), 1) -
                            decimal.Round(decimal.Parse(dgv.CurrentRow.Cells["发票金额"].Value.ToString().Trim()), 1) > 10)

                        {
                            MessageBox.Show("本次发票绑定金额异常过大，请排查！", "注意");
                            return;
                        }
                        else
                        {

                            dgv.CurrentRow.Cells["提交"].Value = "True";
                            save();
                        }





                    }
                    else
                    {
                        dgv.CurrentRow.Cells["提交"].Value = "True";
                        save();
                    }


                }
            }


            if ("录入人" == dgv.CurrentCell.OwningColumn.Name)
            {
                return;
                string sel = "select yhmc as 采购员 from dbo.T_users where bumen like '0004%' ";
                QueryForm queryForm = new QueryForm();
                queryForm.Sel = sel;
                queryForm.Constr = SQLDatabase.Connectstring;
                queryForm.ShowDialog();

                if (queryForm.Result != "")
                {
                    dgv.CurrentRow.Cells["录入人"].Value = queryForm.Result;
                    save();
                }

                return;
            }


            if ("发票类别" == dgv.CurrentCell.OwningColumn.Name)
            {
                if ("True" == dgv.CurrentRow.Cells["lock_flag"].Value.ToString() || "True" == dgv.CurrentRow.Cells["提交"].Value.ToString())
                {

                    MessageBox.Show("已经提交或锁定，不能修改！", "注意");
                    return;
                }
                string sel = "SELECT  id as 编号, tax_type_name as 发票类型 FROM ly_tax_type  ";
                QueryForm queryForm = new QueryForm();
                queryForm.Sel = sel;
                queryForm.Constr = SQLDatabase.Connectstring;
                queryForm.ShowDialog();

                if (queryForm.Result != "")
                {
                    dgv.CurrentRow.Cells["发票类别"].Value = queryForm.Result1;

                    save();
                }
                return;
            }

            if ("客户编码" == dgv.CurrentCell.OwningColumn.Name || "客户名称" == dgv.CurrentCell.OwningColumn.Name)
            {
                string sel = "select  supplier_code as 客户编码,supplier_name  客户名称  from ly_supplier_list ";
                QueryForm queryForm = new QueryForm();
                queryForm.Sel = sel;
                queryForm.Constr = SQLDatabase.Connectstring;
                queryForm.ShowDialog();

                if (queryForm.Result != "")
                {
                    dgv.CurrentRow.Cells["客户编码"].Value = queryForm.Result;
                    dgv.CurrentRow.Cells["客户名称"].Value = queryForm.Result1;
                    save();
                }

                return;
            }
            if ("发票号码" == dgv.CurrentCell.OwningColumn.Name)
            {

                ChangeValue queryForm = new ChangeValue();

                queryForm.OldValue = dgv.CurrentCell.Value.ToString();
                queryForm.NewValue = "";
                queryForm.ChangeMode = "string";
                queryForm.ShowDialog();


                if (queryForm.NewValue != "")
                {
                    for (int i = 0; i < ly_fpDataGridView.Rows.Count; i++)
                    {
                        if (ly_fpDataGridView.Rows[i].Cells["发票号码"].Value.ToString() == queryForm.NewValue)
                        {

                            MessageBox.Show("该张发票已经存在", "注意");
                            return;
                        }
                    }

                    dgv.CurrentRow.Cells["发票号码"].Value = queryForm.NewValue;
                    save();
                }


                return;
            }

            if ("发票日期" == dgv.CurrentCell.OwningColumn.Name)
            {

                DatePicker queryForm = new DatePicker();


                if (null != (dgv.CurrentCell.Value))
                    queryForm.NowDate = dgv.CurrentCell.Value.ToString();
                else
                    queryForm.NowDate = SQLDatabase.GetNowdate().Date.ToString();



                queryForm.ShowDialog();
                if (!string.IsNullOrEmpty(queryForm.NowDate))
                {
                    dgv.CurrentRow.Cells["发票日期"].Value = queryForm.NowDate;
                    save();
                }
                return;
            }

            if ("发票金额" == dgv.CurrentCell.OwningColumn.Name)
            {
                ChangeValue queryForm = new ChangeValue();

                queryForm.OldValue = dgv.CurrentCell.Value.ToString();
                queryForm.NewValue = "";
                queryForm.ChangeMode = "value";
                queryForm.ShowDialog();


                if (queryForm.NewValue != "")
                {
                    dgv.CurrentRow.Cells["发票金额"].Value = queryForm.NewValue;
                    save();
                }
                return;
            }
            if ("税率" == dgv.CurrentCell.OwningColumn.Name)
            {
                ChangeValue queryForm = new ChangeValue();

                queryForm.OldValue = dgv.CurrentCell.Value.ToString();
                queryForm.NewValue = "";
                queryForm.ChangeMode = "value";
                queryForm.ShowDialog();


                if (queryForm.NewValue != "")
                {
                    dgv.CurrentRow.Cells["税率"].Value = queryForm.NewValue;
                    save();
                }
                return;
            }
        }

        protected void save()
        {

            this.ly_fpDataGridView.EndEdit();
            this.lyinvoiceBindingSource.EndEdit();
            this.ly_invoiceTableAdapter.Update(this.lYMaterielRequirements.ly_invoice);

            loadData();

        }
        private void bindingNavigatorAddNewItem_Click(object sender, EventArgs e)
        {
            LY_add_invoice queryForm = new LY_add_invoice();

            queryForm.StartPosition = FormStartPosition.CenterParent;
            queryForm.ShowDialog();

            if (queryForm.DialogResult != DialogResult.Cancel)
            {
                this.ly_invoiceTableAdapter.Fill(this.lYMaterielRequirements.ly_invoice, DateTime.Parse(this.dateTimePicker1.Text).Date, DateTime.Parse(this.dateTimePicker2.Text).Date,"录入");
                this.lyinvoiceBindingSource.Position = this.lyinvoiceBindingSource.Find("invoice_code", queryForm.invoice_code_add);

            }
        }

        private void bindingNavigatorDeleteItem_Click(object sender, EventArgs e)
        {
            if (ly_fpDataGridView.CurrentRow == null)
                return;
            if (SQLDatabase.CheckHaveRight(SQLDatabase.NowUserID, "发票总管理权限"))
            {

            }
            else
            {
                string salespeople = this.ly_fpDataGridView.CurrentRow.Cells["录入人"].Value.ToString();
                if (!string.IsNullOrEmpty(salespeople))
                {
                    if (salespeople != SQLDatabase.nowUserName())
                    {
                        MessageBox.Show("请录入人:" + salespeople + "删除", "注意");
                        return;
                    }
                }
            }
            if (ly_fpDataGridView.CurrentRow.Cells["lock_flag"].Value.ToString() == "True")
            {
                MessageBox.Show("已经锁定无法操作...", "注意");
                return;
            }

            if (ly_invoice_detailDataGridView.Rows.Count > 0)
            {
                MessageBox.Show("该发票已经绑定入库单...", "注意");
                return;
            }
            string caption = "提示...";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;

            DialogResult result;

            string message = "确定要删除吗";

            result = MessageBox.Show(message, caption, buttons,
            MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

            if (result == DialogResult.Yes)
            {
                this.lyinvoiceBindingSource.RemoveCurrent();

                save();
            }
            else
            {
                return;
            }
       
        }
        #endregion
        private void toolStripButton13_Click(object sender, EventArgs e)
        {
        }

        private void ly_fpDataGridView_SelectionChanged(object sender, EventArgs e)
        {
            if (ly_fpDataGridView.CurrentRow == null)
            {

                this.ly_invoice_detail_NewTableAdapter.Fill(this.lYStoreMange.ly_invoice_detail_New, -1,"");
                this.lY_Invoice_Instore_BindlistTableAdapter.Fill(this.lYStoreMange.LY_Invoice_Instore_Bindlist, "", "");
                return;
            }

            string supplier_code = this.ly_fpDataGridView.CurrentRow.Cells["客户编码"].Value.ToString();
            int id_main = int.Parse(this.ly_fpDataGridView.CurrentRow.Cells["id_main"].Value.ToString());
            this.ly_invoice_detail_NewTableAdapter.Fill(this.lYStoreMange.ly_invoice_detail_New, id_main, supplier_code);
            this.lY_Invoice_Instore_BindlistTableAdapter.Fill(this.lYStoreMange.LY_Invoice_Instore_Bindlist, supplier_code, "");
        }

        private void toolStripButton14_Click(object sender, EventArgs e)
        {


        }

        private void dgv_cg_bind_SelectionChanged(object sender, EventArgs e)
        {
            LoadDataBind();

        }
        protected void LoadDataBind()
        {

        }
        private void ly_store_in_cg_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {

        }

        private void ly_store_in_cg_bind_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void toolStripTextBox1_KeyUp(object sender, KeyEventArgs e)
        {

            string filterString;


            filterString = GetDataGridviewMultiFilterString.DGVMultiFilterString(this.ly_fpDataGridView, this.toolStripTextBox1.Text);

            if (SQLDatabase.CheckHaveRight(SQLDatabase.NowUserID, "发票总管理权限"))
            {
               

                if ("0002" == SQLDatabase.nowUserDepartmentBig())
                {

                    this.lyinvoiceBindingSource.Filter = "submit=True and " + "(" + filterString + ")";
                }
                else
                {
                    this.lyinvoiceBindingSource.Filter = "(" + filterString + ")";
                }

            }
            else
            {
                string salespeople = SQLDatabase.nowUserName();
                this.lyinvoiceBindingSource.Filter = "(" + filterString + ") and sava_people='" + salespeople + "'";


            }
        }

        private void toolStripTextBox1_Enter(object sender, EventArgs e)
        {

            toolStripTextBox1.Text = "";
            if (SQLDatabase.CheckHaveRight(SQLDatabase.NowUserID, "发票总管理权限"))
            {
               

                if ("0002" == SQLDatabase.nowUserDepartmentBig())
                {

                    this.lyinvoiceBindingSource.Filter = "submit=True  " ;
                }
                else
                {
                    this.lyinvoiceBindingSource.Filter = " ";
                }

            }
            else
            {
                string salespeople = SQLDatabase.nowUserName();
                lyinvoiceBindingSource.Filter = "sava_people='" + salespeople + "'";

            }
        }

        private void toolStripTextBox5_Enter(object sender, EventArgs e)
        {
            toolStripTextBox5.Text = "";
            this.lY_Invoice_Instore_BindlistBindingSource.Filter = " ";
        }

        private void toolStripTextBox5_KeyUp(object sender, KeyEventArgs e)
        {
            string filterString;
            filterString = GetDataGridviewMultiFilterString.DGVMultiFilterString(this.lY_Invoice_Instore_BindlistDataGridView, this.toolStripTextBox5.Text);
            this.lY_Invoice_Instore_BindlistBindingSource.Filter = "(" + filterString + ")";
        }

        private void lY_Invoice_Instore_BindlistDataGridView_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (lY_Invoice_Instore_BindlistDataGridView.CurrentRow == null || null == ly_fpDataGridView.CurrentRow)
            {
                return;
            }
            string salespeople = this.ly_fpDataGridView.CurrentRow.Cells["录入人"].Value.ToString();
            if (!string.IsNullOrEmpty(salespeople))
            {
                if (salespeople != SQLDatabase.nowUserName())
                {
                    MessageBox.Show("请录入人:" + salespeople + "操作", "注意");
                    return;
                }
            }
            if (ly_fpDataGridView.CurrentRow.Cells["lock_flag"].Value.ToString() == "True")
            {
                MessageBox.Show("已经锁定无法操作...", "注意");
                return;
            }
            if ("True" == ly_fpDataGridView.CurrentRow.Cells["提交"].Value.ToString())
            {
                MessageBox.Show("已经提交无法操作...", "注意");
                return;
            }

            if (!string.IsNullOrEmpty(ly_fpDataGridView.CurrentRow.Cells["税率"].Value.ToString()) &&
                !string.IsNullOrEmpty(lY_Invoice_Instore_BindlistDataGridView.CurrentRow.Cells["税率1"].Value.ToString()))
            {
                if (decimal.Parse(ly_fpDataGridView.CurrentRow.Cells["税率"].Value.ToString()) - decimal.Parse(lY_Invoice_Instore_BindlistDataGridView.CurrentRow.Cells["税率1"].Value.ToString()) < 0
                    || decimal.Parse(ly_fpDataGridView.CurrentRow.Cells["税率"].Value.ToString()) - decimal.Parse(lY_Invoice_Instore_BindlistDataGridView.CurrentRow.Cells["税率1"].Value.ToString()) > 0)
                {
                    string caption = "提示...";
                    MessageBoxButtons buttons = MessageBoxButtons.YesNo;

                    DialogResult result;

                    string message = "合同税率与发票税率不一致，确定要继续绑定吗";

                    result = MessageBox.Show(message, caption, buttons,
                    MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

                    if (result == DialogResult.Yes)
                    {
                    }
                    else
                    {
                        return;
                    }
                }
            }
            if (lY_Invoice_Instore_BindlistDataGridView.CurrentRow.Cells["账期"].Value.ToString() == "")
            {
                string caption = "提示...";
                MessageBoxButtons buttons = MessageBoxButtons.YesNo;

                DialogResult result;

                string message = "该合同的账期为空，确定要继续绑定吗";

                result = MessageBox.Show(message, caption, buttons,
                MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

                if (result == DialogResult.Yes)
                {
                }
                else
                {
                    return;
                }
            }

            string fpid = ly_fpDataGridView.CurrentRow.Cells["id_main"].Value.ToString();
            //2019-04-09
            //decimal fp_money = decimal.Parse(ly_fpDataGridView.CurrentRow.Cells["发票金额"].Value.ToString());
            //decimal no_bind_moeny = decimal.Parse(lY_Invoice_Instore_BindlistDataGridView.CurrentRow.Cells["notbind_money"].Value.ToString() == "" ? "0" : lY_Invoice_Instore_BindlistDataGridView.CurrentRow.Cells["notbind_money"].Value.ToString());

            //20190409新增
            decimal fp_money = decimal.Parse(ly_fpDataGridView.CurrentRow.Cells["invoice_money_novat"].Value.ToString());
            decimal no_bind_moeny = decimal.Parse(lY_Invoice_Instore_BindlistDataGridView.CurrentRow.Cells["notbind_money_novat"].Value.ToString() == "" ? "0" : lY_Invoice_Instore_BindlistDataGridView.CurrentRow.Cells["notbind_money_novat"].Value.ToString());

            //
            string store_id = lY_Invoice_Instore_BindlistDataGridView.CurrentRow.Cells["store_id"].Value.ToString();
            decimal qty_in = decimal.Parse(lY_Invoice_Instore_BindlistDataGridView.CurrentRow.Cells["qty_in"].Value.ToString() == "" ? "0" : lY_Invoice_Instore_BindlistDataGridView.CurrentRow.Cells["qty_in"].Value.ToString());
            decimal bind_qty = decimal.Parse(lY_Invoice_Instore_BindlistDataGridView.CurrentRow.Cells["bind_qty"].Value.ToString() == "" ? "0" : lY_Invoice_Instore_BindlistDataGridView.CurrentRow.Cells["bind_qty"].Value.ToString());
             


            for (int i = 0; i < ly_invoice_detailDataGridView.Rows.Count; i++)
            {
                if (ly_invoice_detailDataGridView.Rows[i].Cells["instore_id0"].Value.ToString() == store_id)
                {
                    MessageBox.Show("该入库单已经有绑定记录，请直接修改绑定数量...", "注意");
                    return;
                }

            }

            //-------------------------


            decimal bind_now_qty = qty_in - bind_qty;
            if (bind_now_qty <= 0)
            {
                MessageBox.Show("该入库单绑定数量已经超过真实数量...", "注意");
                return;
            }
            decimal sumBindmoney = 0;
            for (int i = 0; i < ly_invoice_detailDataGridView.Rows.Count; i++)
            {
                //20190409
                //sumBindmoney += decimal.Parse(ly_invoice_detailDataGridView.Rows[i].Cells["bind_money"].Value.ToString() == "" ? "0.00" : ly_invoice_detailDataGridView.Rows[i].Cells["bind_money"].Value.ToString());
                sumBindmoney += decimal.Parse(ly_invoice_detailDataGridView.Rows[i].Cells["bind_money_novat2"].Value.ToString() == "" ? "0.00" : ly_invoice_detailDataGridView.Rows[i].Cells["bind_money_novat2"].Value.ToString());

            }

            //if ( Math.Round((no_bind_moeny + sumBindmoney),2) > Math.Round(fp_money,2))
            //{
            //    MessageBox.Show("绑定金额超过发票金额...", "注意");
            //    return;
            //}
            this.lyinvoicedetailNewBindingSource.AddNew(); 

            this.ly_invoice_detailDataGridView.CurrentRow.Cells["invoice_id0"].Value = fpid;
            this.ly_invoice_detailDataGridView.CurrentRow.Cells["instore_id0"].Value = store_id;
            this.ly_invoice_detailDataGridView.CurrentRow.Cells["bind_qty0"].Value = bind_now_qty;

            decimal bind_additional_fee = decimal.Parse(
              string.IsNullOrEmpty(lY_Invoice_Instore_BindlistDataGridView.CurrentRow.Cells["additional_fee_Notbind"].Value.ToString()) == true ? "0.00" :
                 lY_Invoice_Instore_BindlistDataGridView.CurrentRow.Cells["additional_fee_Notbind"].Value.ToString()
                );

            this.ly_invoice_detailDataGridView.CurrentRow.Cells["bind_additional_fee"].Value = bind_additional_fee;


            this.ly_invoice_detailDataGridView.EndEdit();
            this.lyinvoicedetailNewBindingSource.EndEdit();
            this.ly_invoice_detail_NewTableAdapter.Update(this.lYStoreMange.ly_invoice_detail_New);

            int id_main = int.Parse(this.ly_fpDataGridView.CurrentRow.Cells["id_main"].Value.ToString());


            loadData();
            lyinvoiceBindingSource.Position = lyinvoiceBindingSource.Find("id", id_main);

            string supplier_code = this.ly_fpDataGridView.CurrentRow.Cells["客户编码"].Value.ToString();
            this.ly_invoice_detail_NewTableAdapter.Fill(this.lYStoreMange.ly_invoice_detail_New, id_main, supplier_code);


       
            this.lY_Invoice_Instore_BindlistTableAdapter.Fill(this.lYStoreMange.LY_Invoice_Instore_Bindlist, supplier_code, "");

            this.lyinvoicedetailNewBindingSource.Position = lyinvoicedetailNewBindingSource.Find("instore_id", store_id);
            this.lY_Invoice_Instore_BindlistBindingSource.Position = lY_Invoice_Instore_BindlistBindingSource.Find("id", store_id);
        }

        private void bindingNavigatorDeleteItem1_Click(object sender, EventArgs e)
        {
            if (lY_Invoice_Instore_BindlistDataGridView.CurrentRow == null || null == ly_fpDataGridView.CurrentRow)
            {
                return;
            }
            string salespeople = this.ly_fpDataGridView.CurrentRow.Cells["录入人"].Value.ToString();
            if (!string.IsNullOrEmpty(salespeople))
            {
                if (salespeople != SQLDatabase.nowUserName())
                {
                    MessageBox.Show("请录入人:" + salespeople + "操作", "注意");
                    return;
                }
            }
            if (ly_fpDataGridView.CurrentRow.Cells["lock_flag"].Value.ToString() == "True")
            {
                MessageBox.Show("已经锁定无法操作...", "注意");
                return;
            }
            if ("True" == ly_fpDataGridView.CurrentRow.Cells["提交"].Value.ToString())
            {
                MessageBox.Show("已经提交无法操作...", "注意");
                return;
            }
            string caption = "提示...";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;

            DialogResult result;

            string message = "确定要删除吗";

            result = MessageBox.Show(message, caption, buttons,
            MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

            if (result == DialogResult.Yes)
            {
                lyinvoicedetailNewBindingSource.RemoveCurrent();
                this.ly_invoice_detailDataGridView.EndEdit();
                this.lyinvoicedetailNewBindingSource.EndEdit();
                this.ly_invoice_detail_NewTableAdapter.Update(this.lYStoreMange.ly_invoice_detail_New);

                string id_main = this.ly_fpDataGridView.CurrentRow.Cells["id_main"].Value.ToString();

                loadData();
                lyinvoiceBindingSource.Position = lyinvoiceBindingSource.Find("id", id_main);


                string supplier_code = this.ly_fpDataGridView.CurrentRow.Cells["客户编码"].Value.ToString();
                this.lY_Invoice_Instore_BindlistTableAdapter.Fill(this.lYStoreMange.LY_Invoice_Instore_Bindlist, supplier_code, "");
            }
            else
            {
                return;
            }
        }

        private void ly_invoice_detailDataGridView_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {


            if (ly_invoice_detailDataGridView.CurrentRow == null || null == ly_fpDataGridView.CurrentRow)
            {
                return;
            }
            DataGridView dgv = sender as DataGridView;
            ////////////////////////000

            if ("adjust_fee" == dgv.CurrentCell.OwningColumn.Name && "000"== SQLDatabase .NowUserID)
            {
                ChangeValue queryForm = new ChangeValue();

                queryForm.OldValue = dgv.CurrentCell.Value.ToString();
                queryForm.NewValue = "";
                queryForm.ChangeMode = "value";
                queryForm.ShowDialog();

                if (queryForm.NewValue != "")
                {


                    decimal adjustBindmoney = decimal.Parse(queryForm.NewValue);

                    if (1 > Math.Abs(adjustBindmoney))
                    {
                        dgv.CurrentRow.Cells["adjust_fee"].Value = queryForm.NewValue;
                    }
                    else
                    {
                        MessageBox.Show("校正绑定金额不能超过1...", "注意");
                        return;
                    }

                }

                else
                {
                    return;

                }



                this.ly_invoice_detailDataGridView.EndEdit();
                this.lyinvoicedetailNewBindingSource.EndEdit();
                this.ly_invoice_detail_NewTableAdapter.Update(this.lYStoreMange.ly_invoice_detail_New);


                string store_id = ly_invoice_detailDataGridView.CurrentRow.Cells["instore_id0"].Value.ToString();

                int id_main = int.Parse(this.ly_fpDataGridView.CurrentRow.Cells["id_main"].Value.ToString());

                loadData();
                lyinvoiceBindingSource.Position = lyinvoiceBindingSource.Find("id", id_main);
                string supplier_code = this.ly_fpDataGridView.CurrentRow.Cells["客户编码"].Value.ToString();
                this.ly_invoice_detail_NewTableAdapter.Fill(this.lYStoreMange.ly_invoice_detail_New, id_main, supplier_code);



                this.lY_Invoice_Instore_BindlistTableAdapter.Fill(this.lYStoreMange.LY_Invoice_Instore_Bindlist, supplier_code, "");

                this.lyinvoicedetailNewBindingSource.Position = lyinvoicedetailNewBindingSource.Find("instore_id", store_id);
                this.lY_Invoice_Instore_BindlistBindingSource.Position = lY_Invoice_Instore_BindlistBindingSource.Find("id", store_id);

                return;
            }



            ////////////////////////000
            string salespeople = this.ly_fpDataGridView.CurrentRow.Cells["录入人"].Value.ToString();

            if (!string.IsNullOrEmpty(salespeople))
            {
                if (salespeople != SQLDatabase.nowUserName())
                {
                    MessageBox.Show("请录入人:" + salespeople + "操作", "注意");
                    return;
                }
            }

            if (ly_fpDataGridView.CurrentRow.Cells["lock_flag"].Value.ToString() == "True")
            {
                MessageBox.Show("已经锁定无法操作...", "注意");
                return;
            }

            if ("True" == ly_fpDataGridView.CurrentRow.Cells["提交"].Value.ToString())
            {
                MessageBox.Show("已经提交无法操作...", "注意");
                return;

            }

           

            if ("bind_qty0" == dgv.CurrentCell.OwningColumn.Name)
            {
                ChangeValue queryForm = new ChangeValue();

                queryForm.OldValue = dgv.CurrentCell.Value.ToString();
                queryForm.NewValue = "";
                queryForm.ChangeMode = "value";
                queryForm.ShowDialog();

                if (queryForm.NewValue != "")
                {
                    string store_id = ly_invoice_detailDataGridView.CurrentRow.Cells["instore_id0"].Value.ToString();
                    for (int i = 0; i < lY_Invoice_Instore_BindlistDataGridView.Rows.Count; i++)
                    {
                        if (lY_Invoice_Instore_BindlistDataGridView.Rows[i].Cells["store_id"].Value.ToString() == store_id)
                        {
                            if (decimal.Parse(queryForm.NewValue) > (decimal.Parse(lY_Invoice_Instore_BindlistDataGridView.Rows[i].Cells["qty_in"].Value.ToString()) -
                               decimal.Parse(lY_Invoice_Instore_BindlistDataGridView.Rows[i].Cells["bind_qty"].Value.ToString()) + decimal.Parse(queryForm.OldValue)))
                            {

                                MessageBox.Show("修改的数量超过未绑定数量...", "注意");
                                return;
                            }
                        }
                    }
                    string id_detail = ly_invoice_detailDataGridView.CurrentRow.Cells["id_detail"].Value.ToString();
                    decimal contract_price = decimal.Parse(ly_invoice_detailDataGridView.CurrentRow.Cells["contract_price0"].Value.ToString());


                    decimal fp_money = decimal.Parse(ly_fpDataGridView.CurrentRow.Cells["invoice_money_novat"].Value.ToString());

                    decimal sumBindmoney = 0;
                    for (int i = 0; i < ly_invoice_detailDataGridView.Rows.Count; i++)
                    {
                        if (ly_invoice_detailDataGridView.Rows[i].Cells["id_detail"].Value.ToString() != id_detail)
                        {
                            //20190409   sumBindmoney += decimal.Parse(ly_invoice_detailDataGridView.Rows[i].Cells["bind_money"].Value.ToString() == "" ? "0" : ly_invoice_detailDataGridView.Rows[i].Cells["bind_money"].Value.ToString());
                            sumBindmoney += decimal.Parse(ly_invoice_detailDataGridView.Rows[i].Cells["bind_money_novat2"].Value.ToString() == "" ? "0" : ly_invoice_detailDataGridView.Rows[i].Cells["bind_money_novat2"].Value.ToString());

                        }
                    }
                    decimal nowbind = contract_price * decimal.Parse(queryForm.NewValue);
                    if ((nowbind + sumBindmoney) > fp_money)
                    {
                        //MessageBox.Show("修改后的总绑定金额超过发票金额...", "注意");
                        //return;
                    }
                    dgv.CurrentRow.Cells["bind_qty0"].Value = queryForm.NewValue;


                    this.ly_invoice_detailDataGridView.EndEdit();
                    this.lyinvoicedetailNewBindingSource.EndEdit();
                    this.ly_invoice_detail_NewTableAdapter.Update(this.lYStoreMange.ly_invoice_detail_New);




                    int id_main = int.Parse(this.ly_fpDataGridView.CurrentRow.Cells["id_main"].Value.ToString());

                    loadData();
                    lyinvoiceBindingSource.Position = lyinvoiceBindingSource.Find("id", id_main);
                    string supplier_code = this.ly_fpDataGridView.CurrentRow.Cells["客户编码"].Value.ToString();
                    this.ly_invoice_detail_NewTableAdapter.Fill(this.lYStoreMange.ly_invoice_detail_New, id_main, supplier_code);



                    this.lY_Invoice_Instore_BindlistTableAdapter.Fill(this.lYStoreMange.LY_Invoice_Instore_Bindlist, supplier_code, "");

                    this.lyinvoicedetailNewBindingSource.Position = lyinvoicedetailNewBindingSource.Find("instore_id", store_id);
                    this.lY_Invoice_Instore_BindlistBindingSource.Position = lY_Invoice_Instore_BindlistBindingSource.Find("id", store_id);
                }

                return;
            }

            ////////////////////////000

            if ("adjust_fee" == dgv.CurrentCell.OwningColumn.Name)
            {
                ChangeValue queryForm = new ChangeValue();

                queryForm.OldValue = dgv.CurrentCell.Value.ToString();
                queryForm.NewValue = "";
                queryForm.ChangeMode = "value";
                queryForm.ShowDialog();

                if (queryForm.NewValue != "")
                {


                    decimal adjustBindmoney = decimal.Parse(queryForm.NewValue);

                    if (1 > Math.Abs(adjustBindmoney))
                    {
                        dgv.CurrentRow.Cells["adjust_fee"].Value = queryForm.NewValue;
                    }
                    else
                    {
                        MessageBox.Show("校正绑定金额不能超过1...", "注意");
                        return;
                    }

                }

                else
                {
                    return;

                }



                this.ly_invoice_detailDataGridView.EndEdit();
                this.lyinvoicedetailNewBindingSource.EndEdit();
                this.ly_invoice_detail_NewTableAdapter.Update(this.lYStoreMange.ly_invoice_detail_New);


                string store_id = ly_invoice_detailDataGridView.CurrentRow.Cells["instore_id0"].Value.ToString();

                int id_main = int.Parse(this.ly_fpDataGridView.CurrentRow.Cells["id_main"].Value.ToString());

                loadData();
                lyinvoiceBindingSource.Position = lyinvoiceBindingSource.Find("id", id_main);
                string supplier_code = this.ly_fpDataGridView.CurrentRow.Cells["客户编码"].Value.ToString();
                this.ly_invoice_detail_NewTableAdapter.Fill(this.lYStoreMange.ly_invoice_detail_New, id_main, supplier_code);



                this.lY_Invoice_Instore_BindlistTableAdapter.Fill(this.lYStoreMange.LY_Invoice_Instore_Bindlist, supplier_code, "");

                this.lyinvoicedetailNewBindingSource.Position = lyinvoicedetailNewBindingSource.Find("instore_id", store_id);
                this.lY_Invoice_Instore_BindlistBindingSource.Position = lY_Invoice_Instore_BindlistBindingSource.Find("id", store_id);

                return;
            }



            ////////////////////////000


            if ("bind_additional_fee" == dgv.CurrentCell.OwningColumn.Name)
            {
                ChangeValue queryForm = new ChangeValue();

                queryForm.OldValue = dgv.CurrentCell.Value.ToString();
                queryForm.NewValue = "";
                queryForm.ChangeMode = "value";
                queryForm.ShowDialog();

                if (queryForm.NewValue != "")
                {
                    string store_id = ly_invoice_detailDataGridView.CurrentRow.Cells["instore_id0"].Value.ToString();

                    dgv.CurrentRow.Cells["bind_additional_fee"].Value = queryForm.NewValue;


                    this.ly_invoice_detailDataGridView.EndEdit();
                    this.lyinvoicedetailNewBindingSource.EndEdit();
                    this.ly_invoice_detail_NewTableAdapter.Update(this.lYStoreMange.ly_invoice_detail_New);




                    int id_main = int.Parse(this.ly_fpDataGridView.CurrentRow.Cells["id_main"].Value.ToString());

                    loadData();
                    lyinvoiceBindingSource.Position = lyinvoiceBindingSource.Find("id", id_main);
                    string supplier_code = this.ly_fpDataGridView.CurrentRow.Cells["客户编码"].Value.ToString();
                    this.ly_invoice_detail_NewTableAdapter.Fill(this.lYStoreMange.ly_invoice_detail_New, id_main, supplier_code);



                    this.lY_Invoice_Instore_BindlistTableAdapter.Fill(this.lYStoreMange.LY_Invoice_Instore_Bindlist, supplier_code, "");

                    this.lyinvoicedetailNewBindingSource.Position = lyinvoicedetailNewBindingSource.Find("instore_id", store_id);
                    this.lY_Invoice_Instore_BindlistBindingSource.Position = lY_Invoice_Instore_BindlistBindingSource.Find("id", store_id);
                }

                return;
            }

        }

        private void ly_invoice_detailDataGridView_SelectionChanged(object sender, EventArgs e)
        {
            if (ly_invoice_detailDataGridView.CurrentRow == null || null == ly_fpDataGridView.CurrentRow)
            {
                return;
            }
            string instore_id0 = ly_invoice_detailDataGridView.CurrentRow.Cells["instore_id0"].Value.ToString();

            if (!string.IsNullOrEmpty(instore_id0))
            {
                //toolStripTextBox5.Text = ly_invoice_detailDataGridView.CurrentRow.Cells["innumber0"].Value.ToString();
                this.lY_Invoice_Instore_BindlistBindingSource.Position = lY_Invoice_Instore_BindlistBindingSource.Find("id", instore_id0);
            }

            
          
        }

        private void button1_Click(object sender, EventArgs e)
        {
            loadData();
        }

        private void 批量所选绑定发票ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //MessageBox.Show("正在开发中", "注意");
            //return;
            DataGridView dgv = lY_Invoice_Instore_BindlistDataGridView;
            if (dgv.CurrentRow == null || null == ly_fpDataGridView.CurrentRow)
            {
                return;
            }
            string salespeople = this.ly_fpDataGridView.CurrentRow.Cells["录入人"].Value.ToString();
            if (!string.IsNullOrEmpty(salespeople))
            {
                if (salespeople != SQLDatabase.nowUserName())
                {
                    MessageBox.Show("请录入人:" + salespeople + "操作", "注意");
                    return;
                }
            }
            if (ly_fpDataGridView.CurrentRow.Cells["lock_flag"].Value.ToString() == "True")
            {
                MessageBox.Show("已经锁定无法操作...", "注意");
                return;
            }
            if ("True" == ly_fpDataGridView.CurrentRow.Cells["提交"].Value.ToString())
            {
                MessageBox.Show("已经提交无法操作...", "注意");
                return;
            }
            foreach (DataGridViewRow dgr in dgv.Rows)
            {
                if (true == dgr.Selected)
                {

                    string fpid = ly_fpDataGridView.CurrentRow.Cells["id_main"].Value.ToString();

                    decimal fp_money = decimal.Parse(ly_fpDataGridView.CurrentRow.Cells["invoice_money_novat"].Value.ToString());
                    decimal no_bind_moeny = decimal.Parse(dgr.Cells["notbind_money_novat"].Value.ToString() == "" ? "0" : dgr.Cells["notbind_money_novat"].Value.ToString());

                    string store_id = dgr.Cells["store_id"].Value.ToString();
                    decimal qty_in = decimal.Parse(dgr.Cells["qty_in"].Value.ToString() == "" ? "0" : dgr.Cells["qty_in"].Value.ToString());
                    decimal bind_qty = decimal.Parse(dgr.Cells["bind_qty"].Value.ToString() == "" ? "0" : dgr.Cells["bind_qty"].Value.ToString());
                    if (bind_qty >0)
                    {
                        break;
                    }

                    for (int i = 0; i < ly_invoice_detailDataGridView.Rows.Count; i++)
                    {
                        if (ly_invoice_detailDataGridView.Rows[i].Cells["instore_id0"].Value.ToString() == store_id)
                        {
                            MessageBox.Show("该入库单已经有绑定记录，请直接修改绑定数量...", "注意");
                            break;
                        }

                    }

                    decimal bind_now_qty = qty_in - bind_qty;
                    if (bind_now_qty <= 0)
                    {
                        MessageBox.Show("该入库单绑定数量已经超过真实数量...", "注意");
                        break;
                    }
                    decimal sumBindmoney = 0;
                    for (int i = 0; i < ly_invoice_detailDataGridView.Rows.Count; i++)
                    {
                        sumBindmoney += decimal.Parse(ly_invoice_detailDataGridView.Rows[i].Cells["bind_money_novat2"].Value.ToString() == "" ? "0.00" : ly_invoice_detailDataGridView.Rows[i].Cells["bind_money_novat2"].Value.ToString());
                    }
                    this.lyinvoicedetailNewBindingSource.AddNew();

                    this.ly_invoice_detailDataGridView.CurrentRow.Cells["invoice_id0"].Value = fpid;
                    this.ly_invoice_detailDataGridView.CurrentRow.Cells["instore_id0"].Value = store_id;
                    this.ly_invoice_detailDataGridView.CurrentRow.Cells["bind_qty0"].Value = bind_now_qty;

                    decimal bind_additional_fee = decimal.Parse(
                      string.IsNullOrEmpty(dgr.Cells["additional_fee_Notbind"].Value.ToString()) == true ? "0.00" :
                         dgr.Cells["additional_fee_Notbind"].Value.ToString()
                        );

                    this.ly_invoice_detailDataGridView.CurrentRow.Cells["bind_additional_fee"].Value = bind_additional_fee;
                    this.ly_invoice_detailDataGridView.EndEdit();
                    this.lyinvoicedetailNewBindingSource.EndEdit();
                    this.ly_invoice_detail_NewTableAdapter.Update(this.lYStoreMange.ly_invoice_detail_New);



                  
              
                }
            }
            int id_main = int.Parse(this.ly_fpDataGridView.CurrentRow.Cells["id_main"].Value.ToString());
            loadData();
            lyinvoiceBindingSource.Position = lyinvoiceBindingSource.Find("id", id_main);
            string supplier_code = this.ly_fpDataGridView.CurrentRow.Cells["客户编码"].Value.ToString(); 
            this.ly_invoice_detail_NewTableAdapter.Fill(this.lYStoreMange.ly_invoice_detail_New, id_main, supplier_code);
            this.lY_Invoice_Instore_BindlistTableAdapter.Fill(this.lYStoreMange.LY_Invoice_Instore_Bindlist, supplier_code, "");


        }

        private void toolStripTextBox1_KeyPress(object sender, KeyPressEventArgs e)
        {

        }
    }
}