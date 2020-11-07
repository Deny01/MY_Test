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
using System.Transactions;

namespace HappyYF.YuanXin.WorkSet
{
    public partial class LY_MaterialMange : Form
    {
        public LY_MaterialMange()
        {
            InitializeComponent();
        }

        private void ly_inma0010BindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.ly_inma0010ylBindingSource.EndEdit();
            this.ly_inma0010ylTableAdapter.Update(this.lYMaterialMange.ly_inma0010yl );

        }

        private void LY_MaterialMange_Load(object sender, EventArgs e)
        {
            
            this.ly_materrial_sortTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;           
            this.ly_materrial_sortTableAdapter.Fill(this.lYMaterialMange.ly_materrial_sort);
            this.ly_inma0010ylTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
           
            this.ly_inma0010ylTableAdapter.Fill(this.lYMaterialMange.ly_inma0010yl);

        }

        private void toolStripTextBox1_Enter(object sender, EventArgs e)
        {
            toolStripTextBox1.Text = "";
           
            this.ly_inma0010ylBindingSource.Filter = "";
        }

        private void toolStripTextBox1_KeyUp(object sender, KeyEventArgs e)
        {
            string filterString;


            filterString = GetDataGridviewMultiFilterString.DGVMultiFilterString(this.ly_inma0010DataGridView, this.toolStripTextBox1.Text);


            //this.ly_sales_standard_Report_zhongchengBindingSource.Filter = "(" + filterString + ") or 清单号='合计'";

            this.ly_inma0010ylBindingSource.Filter = "(" + filterString + ")";
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            ExportDataGridviewTOExcell.ExportDataGridview(this.ly_inma0010DataGridView, true);
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            FilterForm filterForm = new FilterForm();

         

            List<string> ls = new List<string>();
            ls.Add("id");


            filterForm.SetSourceColumns(ly_inma0010DataGridView.Columns , ls);

            filterForm.ShowDialog();

            this.ly_inma0010ylBindingSource.Filter = filterForm.GetFilterString();
        }

        private void bindingNavigatorAddNewItem_Click(object sender, EventArgs e)
        {
            //////////////////////////////////////

           

            LY_MaterialAdd queryForm = new LY_MaterialAdd();

            queryForm.material_code = "";
            queryForm.runmode = "增加";
            queryForm.statemode = "原料";

            queryForm.StartPosition = FormStartPosition.CenterParent;
            queryForm.ShowDialog();

            if (queryForm.DialogResult != DialogResult.Cancel)
            {
                this.ly_inma0010ylTableAdapter.Fill(this.lYMaterialMange.ly_inma0010yl);
                this.ly_inma0010ylBindingSource.Position = this.ly_inma0010ylBindingSource.Find("物资编号", queryForm.material_code);


                SQLDatabase.dataChangeREC(ly_inma0010DataGridView.CurrentRow.Cells["id"].Value.ToString(), "ly_inma0010", "INS", this.Text);


            }

            ///////////////////////////////////////////

           
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if (null == ly_inma0010DataGridView.CurrentRow) return;


            DataGridView dgv = sender as DataGridView;
            if ("商务型号" == dgv.CurrentCell.OwningColumn.Name)
            {
                string wzbh = dgv.CurrentRow.Cells["物资编号"].Value.ToString();
                string swxh = dgv.CurrentCell.Value == null ? "" : dgv.CurrentCell.Value.ToString().Trim();
                if (swxh == "")
                {
                    swxh = dgv.CurrentRow.Cells["中方型号"].Value.ToString();
                }
                ChangeValue queryForm1 = new ChangeValue();

                queryForm1.OldValue = swxh;
                queryForm1.NewValue = swxh;
                queryForm1.ChangeMode = "string";
                queryForm1.ShowDialog();
                if (queryForm1.NewValue != "")
                {
                    string updstr = " update ly_inma0010  " +
                         "  set commercial_description='" + queryForm1.NewValue + "'"
                         + " where  wzbh='" + wzbh + "'";
                    SqlConnection sqlConnection1 = new SqlConnection(SQLDatabase.Connectstring);
                    SqlCommand cmd = new SqlCommand();

                    cmd.CommandText = updstr;
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = sqlConnection1;
                    cmd.CommandTimeout = 0;

                    using (TransactionScope scope = new TransactionScope())
                    {

                        sqlConnection1.Open();
                        try
                        {
                            cmd.ExecuteNonQuery();
                            scope.Complete();
                            dgv.CurrentCell.Value = queryForm1.NewValue;

                        }
                        catch (SqlException sqle)
                        {
                            MessageBox.Show(sqle.Message.Split('*')[0]);
                        }
                        finally
                        {
                            sqlConnection1.Close();
                        }
                    }
                }

                return;

            }




            string s = this.ly_inma0010DataGridView.CurrentRow.Cells["物资编号"].Value.ToString();
            string nowxh = this.ly_inma0010DataGridView.CurrentRow.Cells["id"].Value.ToString();
            //int loanid = int.Parse(this.xD_Client_LoanDataGridView.CurrentRow.Cells["Id_loan"].Value.ToString());

            LY_MaterialAdd queryForm = new LY_MaterialAdd();

            queryForm.statemode = "原料";
            queryForm.runmode = "修改";
            queryForm.material_code = s;

            queryForm.StartPosition = FormStartPosition.CenterParent;
            queryForm.ShowDialog();

            if (queryForm.DialogResult != DialogResult.Cancel)
            {
                this.ly_inma0010ylTableAdapter.Fill(this.lYMaterialMange.ly_inma0010yl);

                int nowpos = this.ly_inma0010ylBindingSource.Find("id", nowxh);

                //if (nowpos < 1)
                //{
                //    nowpos = this.ly_inma0010ylBindingSource.Find("物资编号", nowxh);
                //}

                this.ly_inma0010ylBindingSource.Position = nowpos; // this.ly_inma0010ylBindingSource.Find("物资编号", s);

                SQLDatabase.dataChangeREC(ly_inma0010DataGridView.CurrentRow.Cells["id"].Value.ToString(), "ly_inma0010", "UPD", this.Text);
            }

        }
        protected bool checkLock()
        {
            if (null == ly_inma0010DataGridView.CurrentRow)
            { return false; }

            else
            {
                if (this.ly_inma0010DataGridView.CurrentRow.Cells["fin_lock"].Value.ToString() == "True" || this.ly_inma0010DataGridView.CurrentRow.Cells["tec_lock"].Value.ToString() == "True" || this.ly_inma0010DataGridView.CurrentRow.Cells["pro_lock"].Value.ToString() == "True")
                {
                    return false;

                }
                else
                {
                    return true;
                }
            }
        }
        private void bindingNavigatorDeleteItem_Click(object sender, EventArgs e)
        {
         

            if (!checkLock())
            {
                MessageBox.Show("已经锁定无法修改...", "注意");
                return;

            }
            if (!SQLDatabase.CheckHaveRight(SQLDatabase.NowUserID, "指定物料负责人"))
            {
                string salespeople = this.ly_inma0010DataGridView.CurrentRow.Cells["负责人"].Value.ToString();
                if (!string.IsNullOrEmpty(salespeople))
                {
                    if (salespeople != SQLDatabase.nowUserName())
                    {
                        MessageBox.Show("请负责人:" + salespeople + "操作", "注意");
                        return;
                    }
                }
                else
                {
                    MessageBox.Show("请联系技术部领导进行负责人指定...", "注意");
                    return;
                }
            }

            DialogResult dr;
            dr = new CheckKeywordsell().ShowDialog(this);

            if (dr != DialogResult.OK)
            {
                return;
            }
            

            
            string message = "确定删除当前记录吗？";
            string caption = "提示...";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result;



            result = MessageBox.Show(message, caption, buttons,
            MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

            if (result == DialogResult.Yes)
            {

                SQLDatabase.dataChangeREC(ly_inma0010DataGridView.CurrentRow.Cells["id"].Value.ToString(), "ly_inma0010", "DEL", this.Text);
                
                this.ly_inma0010ylBindingSource.RemoveCurrent();


                ly_inma0010DataGridView.EndEdit();
                ly_inma0010ylBindingSource.EndEdit();


                this.ly_inma0010ylTableAdapter.Update(this.lYMaterialMange.ly_inma0010yl);

                //string s = this.xD_Sel_SellBalanceDataGridView.CurrentRow.Cells["编号"].Value.ToString();

                //this.hS_ClientPaymentTableAdapter.Fill(this.xD_SellBalance.HS_ClientPayment, s);

               
            }
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            if (null == this.ly_inma0010DataGridView.CurrentRow) return;
            SortForm DataSort = new SortForm();

            List<string> ls = new List<string>();
            ls.Add("id");


            DataSort.SetSortColumns(this.lYMaterialMange.ly_inma0010yl.Columns, ls);
            DataSort.ShowDialog();
            this.ly_inma0010ylBindingSource.Sort = DataSort.GetSortString();
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            this.ly_inma0010ylTableAdapter.Fill(this.lYMaterialMange.ly_inma0010yl);
        }
    }
}
