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
    public partial class LY_StockTake : Form
    {
        private string formState;
        int nowRow;

        int lastRow = -1;
        int lastcolumn;

        public LY_StockTake()
        {
            InitializeComponent();
            this.ly_stocktake_mainTableAdapter.CommandTimeout = 0;
        }

        private void ly_stocktake_mainBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            SaveChenged();


        }

        private void SaveChenged()
        {
            this.ly_stocktake_mainDataGridView.EndEdit();
            this.ly_stocktake_mainBindingSource.EndEdit();

            //if (false == this.Validate()) return;

            if (checkInput())
            {
                //frmWaiting.Show(this);
                NewFrm.Show(this);

                NewFrm.Notify(this, "正在生成盘点数据，请等待...");

                this.ly_stocktake_mainTableAdapter.CommandTimeout = 0;
                this.ly_stocktake_mainTableAdapter.Update(this.lYStoreMange.ly_stocktake_main);

                //frmWaiting.Hide(this);

                NewFrm.Hide(this);

                //this.hT_Maintenance_ItemTableAdapter.Fill(this.hT_Insurance.HT_Maintenance_Item, loanId);

                this.SetFormState("View");
            }
        }

        private bool checkInput()
        {

            if (string.IsNullOrEmpty(ly_stocktake_mainDataGridView.CurrentRow.Cells["盘点日期"].Value.ToString()))
            {
                MessageBox.Show("请输入盘点日期...", "注意");
                ly_stocktake_mainDataGridView.CurrentCell = ly_stocktake_mainDataGridView.CurrentRow.Cells["盘点日期"];

                return false;
            }
            if (string.IsNullOrEmpty(ly_stocktake_mainDataGridView.CurrentRow.Cells["盘点编号1"].Value.ToString()))
            {
                MessageBox.Show("请输入盘点编号...", "注意");
                ly_stocktake_mainDataGridView.CurrentCell = ly_stocktake_mainDataGridView.CurrentRow.Cells["盘点编号"];

                return false;
            }

            if (string.IsNullOrEmpty(ly_stocktake_mainDataGridView.CurrentRow.Cells["说明"].Value.ToString()))
            {
                MessageBox.Show("请输入盘点说明...", "注意");
                ly_stocktake_mainDataGridView.CurrentCell = ly_stocktake_mainDataGridView.CurrentRow.Cells["说明"];

                return false;
            }







            return true;
        }

        private void LY_StockTake_Load(object sender, EventArgs e)
        {
            SetFormState("View");


            this.ly_stocktake_detail_ONline_Explode01TableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            this.ly_stocktake_detail_ONline_Explode02TableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            this.ly_stocktake_detail_ONline_Explode03TableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;

            this.ly_stocktake_detail_ONline01TableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            this.ly_stocktake_detail_ONline02TableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            this.ly_stocktake_detail_ONline03TableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;

            this.ly_stocktake_detailTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;

            this.ly_stocktake_mainTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            this.ly_stocktake_mainTableAdapter.Fill(this.lYStoreMange.ly_stocktake_main);

            _ly_stocktake_detail_ONline_Explode01TotalTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            _ly_stocktake_detail_ONline_Explode02TotalTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            ///////////////////////
            DataGridViewColumnSelector cs = new DataGridViewColumnSelector(ly_stocktake_detailDataGridView, this.Text);
            cs.MaxHeight = 180;
            cs.Width = 800;

            cs.Set_dgvColumns();
            ////////////////////////////////
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            FilterForm filterForm = new FilterForm();



            List<string> ls = new List<string>();
            ls.Add("id");


            filterForm.SetSourceColumns(ly_stocktake_detailDataGridView.Columns, ls);

            filterForm.ShowDialog();

            this.ly_stocktake_detailBindingSource.Filter = filterForm.GetFilterString();
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            ExportDataGridviewTOExcell.ExportDataGridview(this.ly_stocktake_detailDataGridView, true);
        }

        private void toolStripTextBox1_Enter(object sender, EventArgs e)
        {
            toolStripTextBox1.Text = "";

            this.ly_stocktake_detailBindingSource.Filter = "";
        }

        private void toolStripTextBox1_KeyUp(object sender, KeyEventArgs e)
        {
            string dFilter = "";

            //for (int i = 0; i < this.hT_Vehicle_collectDataGridView.ColumnCount; i++)
            for (int i = 2; i < 8; i++)
            {
                string tempColumnName = this.ly_stocktake_detailDataGridView.Columns[i].DataPropertyName;

                if (i != 7)
                    dFilter = dFilter + tempColumnName + " like  '*" + this.toolStripTextBox1.Text + "*' or ";
                else
                    dFilter = dFilter + tempColumnName + " like  '*" + this.toolStripTextBox1.Text + "*' ";

            }

            if (this.toolStripTextBox1.Text.Replace(" ", "").Length > 0)

                this.ly_stocktake_detailBindingSource.Filter = dFilter;
            else
                this.ly_stocktake_detailBindingSource.Filter = " ";
        }

        private void ly_stocktake_mainDataGridView_SelectionChanged(object sender, EventArgs e)
        {
            if (null == ly_stocktake_mainDataGridView.CurrentRow) return;

            if (this.formState == "View")
            {
                string checkNum;

                if (null != ly_stocktake_mainDataGridView.CurrentRow)
                {

                    checkNum = this.ly_stocktake_mainDataGridView.CurrentRow.Cells["盘点编号1"].Value.ToString();
                    this.ly_stocktake_detailTableAdapter.Fill(this.lYStoreMange.ly_stocktake_detail, checkNum, SQLDatabase.NowUserID);

                    this.ly_stocktake_detail_ONline01TableAdapter.Fill(this.lYStoreMange.ly_stocktake_detail_ONline01, checkNum);
                    this.ly_stocktake_detail_ONline02TableAdapter.Fill(this.lYStoreMange.ly_stocktake_detail_ONline02, checkNum);
                    this.ly_stocktake_detail_ONline03TableAdapter.Fill(this.lYStoreMange.ly_stocktake_detail_ONline03, checkNum);

                    this.ly_stocktake_detail_ONline_Explode01TableAdapter.Fill(this.lYStoreMange.ly_stocktake_detail_ONline_Explode01, checkNum);
                    this.ly_stocktake_detail_ONline_Explode02TableAdapter.Fill(this.lYStoreMange.ly_stocktake_detail_ONline_Explode02, checkNum);
                    this.ly_stocktake_detail_ONline_Explode03TableAdapter.Fill(this.lYStoreMange.ly_stocktake_detail_ONline_Explode03, checkNum);


                    _ly_stocktake_detail_ONline_Explode01TotalTableAdapter.Fill(this.lYStoreMange._ly_stocktake_detail_ONline_Explode01Total, checkNum);
                    _ly_stocktake_detail_ONline_Explode02TotalTableAdapter.Fill(this.lYStoreMange._ly_stocktake_detail_ONline_Explode02Total, checkNum);
                }
            }
            else
            {
                this.ly_stocktake_mainBindingSource.Position = this.nowRow;

                //this.hT_Insurance_ItemDataGridView.CurrentCell = this.hT_Insurance_ItemDataGridView.CurrentRow.Cells[this.hT_Insurance_ItemDataGridView.CurrentCell.ColumnIndex];
            }

            if ("True" == ly_stocktake_mainDataGridView.CurrentRow.Cells["完成"].Value.ToString())
            {
                this.ly_stocktake_detailDataGridView.ReadOnly = true;

                this.ly_stocktake_detail_ONline_Explode01DataGridView.ReadOnly = true;
                this.ly_stocktake_detail_ONline_Explode02DataGridView.ReadOnly = true;
                this.ly_stocktake_detail_ONline_Explode03DataGridView.ReadOnly = true;

            }
            else
            {
                this.ly_stocktake_detailDataGridView.ReadOnly = false;
                this.ly_stocktake_detail_ONline_Explode01DataGridView.ReadOnly = false;
                this.ly_stocktake_detail_ONline_Explode02DataGridView.ReadOnly = false;
                this.ly_stocktake_detail_ONline_Explode03DataGridView.ReadOnly = false;
                setDetailColumn();

            }
        }

        private void SetFormState(string state)
        {
            // view edit add save cancle

            if ("View" == state)
            {
                this.formState = "View";

                this.toolStripButton1.Enabled = true;
                this.toolStripButton3.Enabled = false;
                this.hT_Manage_ItemBindingNavigatorSaveItem.Enabled = false;
                this.bindingNavigatorDeleteItem.Enabled = true;
                this.bindingNavigatorAddNewItem.Enabled = true;




                this.bindingNavigatorMoveFirstItem.Enabled = true;
                this.bindingNavigatorMoveLastItem.Enabled = true;
                this.bindingNavigatorMoveNextItem.Enabled = true;
                this.bindingNavigatorMovePreviousItem.Enabled = true;
                this.bindingNavigatorPositionItem.Enabled = true;



                this.ly_stocktake_mainDataGridView.ReadOnly = true;






            }
            else
            {
                this.formState = "Edit";

                this.nowRow = ly_stocktake_mainDataGridView.CurrentRow.Index;

                this.ly_stocktake_mainDataGridView.ReadOnly = false;

                // if (null != ly_stocktake_mainDataGridView.CurrentRow)


                this.toolStripButton1.Enabled = false;
                this.toolStripButton3.Enabled = true;
                this.hT_Manage_ItemBindingNavigatorSaveItem.Enabled = true;
                this.bindingNavigatorDeleteItem.Enabled = false;
                this.bindingNavigatorAddNewItem.Enabled = false;




                this.bindingNavigatorMoveFirstItem.Enabled = false;
                this.bindingNavigatorMoveLastItem.Enabled = false;
                this.bindingNavigatorMoveNextItem.Enabled = false;
                this.bindingNavigatorMovePreviousItem.Enabled = false;
                this.bindingNavigatorPositionItem.Enabled = false;



                this.ly_stocktake_mainDataGridView.ReadOnly = false;

                //this.xD_client_compensate_planDataGridView.Columns["RowNumber"].ReadOnly = true;
                //this.hT_Manage_ItemDataGridView.Columns["LeftDays"].ReadOnly = true;
                //this.hT_Manage_ItemDataGridView.Columns["left_money"].ReadOnly = true;
                this.ly_stocktake_mainDataGridView.Columns["盘点编号1"].ReadOnly = true;
                this.ly_stocktake_mainDataGridView.Columns["完成"].ReadOnly = true;
                this.ly_stocktake_mainDataGridView.Columns["盘点负责"].ReadOnly = true;
                this.ly_stocktake_mainDataGridView.Columns["sys_date1"].ReadOnly = true;
                this.ly_stocktake_mainDataGridView.Columns["库存金额"].ReadOnly = true;
                this.ly_stocktake_mainDataGridView.Columns["盘点金额"].ReadOnly = true;
                this.ly_stocktake_mainDataGridView.Columns["盈亏金额"].ReadOnly = true;



            }


        }

        private void decimalTextBox_Validating(object sender, CancelEventArgs e)
        {
            TextBox tb = sender as TextBox;
            tb.Text = tb.Text.Replace(" ", "");
            tb.DataBindings[0].Parse += new ConvertEventHandler(bind_Parse);
        }

        void bind_Parse(object sender, ConvertEventArgs e)//控件值更改时发生
        {
            if (e.Value.ToString() == "")
                e.Value = DBNull.Value;
        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {


            //if (keyData == Keys.Enter)
            //{
            //    if ((ly_stocktake_mainDataGridView.ColumnCount - 1) == ly_stocktake_mainDataGridView.CurrentCell.ColumnIndex)
            //        ly_stocktake_mainDataGridView.CurrentCell = ly_stocktake_mainDataGridView.CurrentRow.Cells["check_date"];
            //    else
            //        System.Windows.Forms.SendKeys.Send("{tab}");
            //}
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void bindingNavigatorAddNewItem_Click(object sender, EventArgs e)
        {

            if (!SQLDatabase.CheckHaveRight(SQLDatabase.NowUserID, "盘点数据批准"))
            {

                MessageBox.Show("没有盘点数据批准权限...", "注意");
                return;
            }

            this.ly_stocktake_mainBindingSource.AddNew();



            ly_stocktake_mainDataGridView.CurrentRow.Cells["完成"].Value = false;
            ly_stocktake_mainDataGridView.CurrentRow.Cells["盘点负责"].Value = SQLDatabase.nowUserName();


            ly_stocktake_mainDataGridView.CurrentRow.Cells["盘点编号1"].Value = GetCheckNumber();
            ly_stocktake_mainDataGridView.CurrentRow.Cells["盘点日期"].Value = DateTime.Now;
            //hT_Manage_ItemDataGridView.CurrentRow.Cells["to_day"].Value = periodBeginDate.AddMonths(3).AddDays(-1);
            //ly_stocktake_mainDataGridView.CurrentRow.Cells["to_day"].Value = periodBeginDate.AddMonths(3);

            this.SetFormState("Edit");
            ly_stocktake_mainDataGridView.CurrentCell = ly_stocktake_mainDataGridView.CurrentRow.Cells["盘点日期"];
        }

        private string GetCheckNumber()
        {
            SqlConnection sqlConnection1 = new SqlConnection(SQLDatabase.Connectstring);
            SqlCommand cmd = new SqlCommand();

            string checkNum = "";

            //cmd.Parameters.Add("@datestyle", SqlDbType.VarChar );
            //cmd.Parameters["@datestyle"].Value = datestyle;

            //cmd.Parameters.Add("@loanId", SqlDbType.Int);
            //cmd.Parameters["@loanId"].Value = loanId;

            //cmd.Parameters.Add("@item_name", SqlDbType.VarChar);
            //cmd.Parameters["@item_name"].Value = item_name;


            cmd.CommandText = "LY_GetStocktake_Number";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = sqlConnection1;

            sqlConnection1.Open();
            checkNum = cmd.ExecuteScalar().ToString();
            sqlConnection1.Close();



            return checkNum;

        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if (!SQLDatabase.CheckHaveRight(SQLDatabase.NowUserID, "盘点数据批准"))
            {

                MessageBox.Show("没有盘点数据批准权限...", "注意");
                return;
            }

            this.SetFormState("Edit");
        }

        private void bindingNavigatorDeleteItem_Click(object sender, EventArgs e)
        {
            if (null == ly_stocktake_mainDataGridView.CurrentRow) return;

            if (!SQLDatabase.CheckHaveRight(SQLDatabase.NowUserID, "盘点数据批准"))
            {

                MessageBox.Show("没有盘点数据批准权限...", "注意");
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

                this.ly_stocktake_mainBindingSource.RemoveCurrent();


                ly_stocktake_mainDataGridView.EndEdit();
                ly_stocktake_mainBindingSource.EndEdit();

                this.ly_stocktake_mainTableAdapter.Update(this.lYStoreMange.ly_stocktake_main);

                //this.hT_Maintenance_ItemTableAdapter.Fill(this.hT_Insurance.HT_Maintenance_Item, this.loanId);

                //AddSummationRowCard(xD_client_compensate_loan1BindingSource, xD_client_compensate_planDataGridView);
            }

            this.SetFormState("View");
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            this.SetFormState("View");
            this.ly_stocktake_mainTableAdapter.Fill(this.lYStoreMange.ly_stocktake_main);
        }

        private void ly_stocktake_mainDataGridView_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {

            DataGridView dgv = sender as DataGridView;

            if ("完成" == dgv.CurrentCell.OwningColumn.Name)
            {

                if (!SQLDatabase.CheckHaveRight(SQLDatabase.NowUserID, "盘点数据批准"))
                {

                    MessageBox.Show("没有盘点数据批准权限...", "注意");
                    return;
                }



                if ("True" != dgv.CurrentRow.Cells["完成"].Value.ToString())
                {

                    string message = "确定盘点完成吗?";
                    string caption = "提示...";
                    MessageBoxButtons buttons = MessageBoxButtons.YesNo;
                    DialogResult result;



                    result = MessageBox.Show(message, caption, buttons,
                    MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

                    if (result == DialogResult.Yes)
                    {

                        //dgv.CurrentRow.Cells["discount_money"].Value = dgv.CurrentRow.Cells["apply_money"].Value;
                        dgv.CurrentRow.Cells["完成"].Value = "True";
                        this.ly_stocktake_detailDataGridView.ReadOnly = true;
                        SaveChenged();
                    }

                }
                else
                {

                    string message = "取消盘点完成吗吗?";
                    string caption = "提示...";
                    MessageBoxButtons buttons = MessageBoxButtons.YesNo;
                    DialogResult result;



                    result = MessageBox.Show(message, caption, buttons,
                    MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

                    if (result == DialogResult.Yes)
                    {
                        //dgv.CurrentRow.Cells["discount_money"].Value = DBNull.Value;
                        //dgv.CurrentRow.Cells["apply_money"].Value = DBNull.Value;
                        dgv.CurrentRow.Cells["完成"].Value = "False";
                        this.ly_stocktake_detailDataGridView.ReadOnly = false;
                        setDetailColumn();
                        SaveChenged();
                    }
                }

                return;
            }
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            if (null == this.ly_stocktake_detailDataGridView.CurrentRow) return;
            SortForm DataSort = new SortForm();

            List<string> ls = new List<string>();
            ls.Add("id");


            DataSort.SetSortColumns(this.lYStoreMange.ly_stocktake_detail.Columns, ls);
            DataSort.ShowDialog();
            this.ly_stocktake_detailBindingSource.Sort = DataSort.GetSortString();
        }

        private void ly_stocktake_detailDataGridView_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            DataGridView dgv = sender as DataGridView;

            if (true == dgv.ReadOnly) return;

            DataGridViewCell dgc = dgv.Rows[e.RowIndex].Cells[e.ColumnIndex];



            if ("盘点数量" == dgc.OwningColumn.DataPropertyName)
            {

                decimal newCheckCount;

                if ("" == e.FormattedValue.ToString().Replace(" ", ""))
                {
                    dgc.Value = DBNull.Value;
                    SaveDetail();
                    //e.Cancel = false ;
                    return;

                }

                if (!decimal.TryParse(e.FormattedValue.ToString(), out newCheckCount))
                {
                    MessageBox.Show("数据格式错误...", "注意");
                    e.Cancel = true;

                }
                else
                {
                    SaveDetail();
                }
            }
            else
            {

                return;

            }






        }

        private void SaveDetail()
        {
            //this.ly_stocktake_detailDataGridView.EndEdit();
            //this.ly_stocktake_detailBindingSource.EndEdit();

            this.ly_stocktake_detailDataGridView.EndEdit();
            this.ly_stocktake_detailBindingSource.EndEdit();
            this.ly_stocktake_detailTableAdapter.Update(this.lYStoreMange.ly_stocktake_detail);

            DataGridView dgv = ly_stocktake_detailDataGridView;

            decimal checknum;

            if (!string.IsNullOrEmpty(dgv.CurrentRow.Cells["盘点数量"].Value.ToString()))
                checknum = decimal.Parse(dgv.CurrentRow.Cells["盘点数量"].Value.ToString());
            else
                checknum = 0;

            decimal unitprice;

            if (!string.IsNullOrEmpty(dgv.CurrentRow.Cells["库存单价"].Value.ToString()))
                unitprice = decimal.Parse(dgv.CurrentRow.Cells["库存单价"].Value.ToString());
            else
                unitprice = 0;

            decimal storenumber;

            if (!string.IsNullOrEmpty(dgv.CurrentRow.Cells["库存数量"].Value.ToString()))
                storenumber = decimal.Parse(dgv.CurrentRow.Cells["库存数量"].Value.ToString());
            else
                storenumber = 0;

            decimal borrowumber;

            if (!string.IsNullOrEmpty(dgv.CurrentRow.Cells["借用数量"].Value.ToString()))
                borrowumber = decimal.Parse(dgv.CurrentRow.Cells["借用数量"].Value.ToString());
            else
                borrowumber = 0;



            dgv.CurrentRow.Cells["盘点金额1"].OwningColumn.ReadOnly = false;
            dgv.CurrentRow.Cells["盘点金额1"].Value = checknum * unitprice;
            dgv.CurrentRow.Cells["盘点金额1"].OwningColumn.ReadOnly = true;

            dgv.CurrentRow.Cells["盈亏数量"].OwningColumn.ReadOnly = false;
            dgv.CurrentRow.Cells["盈亏数量"].Value = checknum + borrowumber - storenumber;
            dgv.CurrentRow.Cells["盈亏数量"].OwningColumn.ReadOnly = true;

            dgv.CurrentRow.Cells["盈亏金额1"].OwningColumn.ReadOnly = false;
            dgv.CurrentRow.Cells["盈亏金额1"].Value = checknum * unitprice + borrowumber * unitprice - storenumber * unitprice;
            dgv.CurrentRow.Cells["盈亏金额1"].OwningColumn.ReadOnly = true;







            //this.ly_stocktake_detailDataGridView.EndEdit();
            //this.ly_stocktake_detailBindingSource.EndEdit();
            //this.ly_stocktake_detailTableAdapter.Update(this.lYStoreMange.ly_stocktake_detail);
        }

        private void ly_stocktake_detailDataGridView_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.Cancel = false;
        }

        private void setDetailColumn()
        {
            foreach (DataGridViewColumn dvc in ly_stocktake_detailDataGridView.Columns)
            {
                if ("盘点数量" != dvc.DataPropertyName)
                {
                    dvc.ReadOnly = true;
                }
                else
                {
                    dvc.ReadOnly = false;
                }

            }

            foreach (DataGridViewColumn dvc in ly_stocktake_detail_ONline_Explode01DataGridView.Columns)
            {
                if ("盘点数量" != dvc.DataPropertyName)
                {
                    dvc.ReadOnly = true;
                }
                else
                {
                    dvc.ReadOnly = false;
                }

            }

            foreach (DataGridViewColumn dvc in ly_stocktake_detail_ONline_Explode02DataGridView.Columns)
            {
                if ("盘点数量" != dvc.DataPropertyName)
                {
                    dvc.ReadOnly = true;
                }
                else
                {
                    dvc.ReadOnly = false;
                }

            }

            foreach (DataGridViewColumn dvc in ly_stocktake_detail_ONline_Explode03DataGridView.Columns)
            {
                if ("盘点数量" != dvc.DataPropertyName)
                {
                    dvc.ReadOnly = true;
                }
                else
                {
                    dvc.ReadOnly = false;
                }

            }

        }
        private void SetlastColumn()
        {

            DataGridView dgv = ly_stocktake_detailDataGridView;

            //lastRow=e .RowIndex ;
            //lastcolumn=e .ColumnIndex;
            if (-1 == lastRow) return;

            decimal checknum;

            if (!string.IsNullOrEmpty(dgv.Rows[lastRow].Cells["盘点数量"].Value.ToString()))
                checknum = decimal.Parse(dgv.Rows[lastRow].Cells["盘点数量"].Value.ToString());
            else
                checknum = 0;

            decimal borrownum;

            if (!string.IsNullOrEmpty(dgv.Rows[lastRow].Cells["借用数量"].Value.ToString()))
                borrownum = decimal.Parse(dgv.Rows[lastRow].Cells["借用数量"].Value.ToString());
            else
                borrownum = 0;


            decimal unitprice;

            if (!string.IsNullOrEmpty(dgv.Rows[lastRow].Cells["库存单价"].Value.ToString()))
                unitprice = decimal.Parse(dgv.Rows[lastRow].Cells["库存单价"].Value.ToString());
            else
                unitprice = 0;

            decimal storenumber;

            if (!string.IsNullOrEmpty(dgv.Rows[lastRow].Cells["库存数量"].Value.ToString()))
                storenumber = decimal.Parse(dgv.Rows[lastRow].Cells["库存数量"].Value.ToString());
            else
                storenumber = 0;



            dgv.Rows[lastRow].Cells["盘点金额1"].Value = checknum * unitprice;
            dgv.Rows[lastRow].Cells["借用金额"].Value = borrownum * unitprice;
            dgv.Rows[lastRow].Cells["盈亏数量"].Value = checknum + borrownum - storenumber;
            dgv.Rows[lastRow].Cells["盈亏金额1"].Value = checknum * unitprice + borrownum * unitprice - storenumber * unitprice;


        }
        private void ly_stocktake_detailDataGridView_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (true == ly_stocktake_detailDataGridView.ReadOnly) return;
            if ("盘点数量" == ly_stocktake_detailDataGridView.CurrentCell.OwningColumn.DataPropertyName)
            {
                //SetlastColumn();
                ly_stocktake_detailDataGridView.CurrentCell.Style.BackColor = Color.White;
                ly_stocktake_detailDataGridView.CurrentCell.Style.ForeColor = Color.Teal;

                lastRow = e.RowIndex;
                lastcolumn = e.ColumnIndex;
            }
        }

        private void ly_stocktake_detailDataGridView_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
            if (true == ly_stocktake_detailDataGridView.ReadOnly) return;
            if ("盘点数量" == ly_stocktake_detailDataGridView.CurrentCell.OwningColumn.DataPropertyName)
            {
                

                ly_stocktake_detailDataGridView.CurrentCell.Style.BackColor = Color.Teal;
                ly_stocktake_detailDataGridView.CurrentCell.Style.ForeColor = Color.White;
            }
        }

        private void bindingNavigatorAddNewItem1_Click(object sender, EventArgs e)
        {
            if (null == this.ly_stocktake_mainDataGridView.CurrentRow) return;

            if ("True" == ly_stocktake_mainDataGridView.CurrentRow.Cells["完成"].Value.ToString())
            {
                MessageBox.Show("盘点已完成,操作取消...", "注意");
                return;

            }

            int parentid = int.Parse(this.ly_stocktake_mainDataGridView.CurrentRow.Cells["id_main"].Value.ToString());
            string checknumber = this.ly_stocktake_mainDataGridView.CurrentRow.Cells["盘点编号1"].Value.ToString();



            LY_StockTake_AddItem queryForm = new LY_StockTake_AddItem();


            queryForm.check_num = checknumber;
            //queryForm.to_date = DateTime.Parse(this.hT_ConveyanceVerify_MainDataGridView.CurrentRow.Cells["to_date"].Value.ToString());

            queryForm.StartPosition = FormStartPosition.CenterParent;
            queryForm.ShowDialog();

            if ("" != queryForm.itemnumber)
            {

                this.ly_stocktake_detailBindingSource.AddNew();
                this.ly_stocktake_detailDataGridView.CurrentRow.Cells["parent_id"].Value = parentid;
                this.ly_stocktake_detailDataGridView.CurrentRow.Cells["盘点编号"].Value = queryForm.check_num;
                this.ly_stocktake_detailDataGridView.CurrentRow.Cells["物料编号"].Value = queryForm.itemnumber;
                this.ly_stocktake_detailDataGridView.CurrentRow.Cells["库存单价"].Value = queryForm.unit_price;
                this.ly_stocktake_detailDataGridView.CurrentRow.Cells["库存数量"].Value = queryForm.store_number;
                this.ly_stocktake_detailDataGridView.CurrentRow.Cells["借用数量"].Value = queryForm.borrow_number;


                this.ly_stocktake_detailDataGridView.CurrentRow.Cells["盘点数量"].Value = DBNull.Value;


                this.ly_stocktake_detailDataGridView.CurrentRow.Cells["latest_Intime"].Value = queryForm.latest_intime;
                this.ly_stocktake_detailDataGridView.CurrentRow.Cells["latest_Outtime"].Value = queryForm.latest_outtime;
                this.ly_stocktake_detailDataGridView.CurrentRow.Cells["latest_Activitytime"].Value = queryForm.latest_activitytime;

                this.ly_stocktake_detailDataGridView.CurrentRow.Cells["remark"].Value = queryForm.nowremark;

                this.ly_stocktake_detailDataGridView.EndEdit();

                this.ly_stocktake_detailBindingSource.EndEdit();




                //if (false == this.Validate()) return;


                this.ly_stocktake_detailTableAdapter.Update(this.lYStoreMange.ly_stocktake_detail);


                this.ly_stocktake_detailTableAdapter.Fill(this.lYStoreMange.ly_stocktake_detail, queryForm.check_num, SQLDatabase.NowUserID);


                this.ly_stocktake_detailBindingSource.Position = this.ly_stocktake_detailBindingSource.Find("物料编号", queryForm.itemnumber);
            }
        }

        private void bindingNavigatorDeleteItem1_Click(object sender, EventArgs e)
        {
            if (null == this.ly_stocktake_detailDataGridView.CurrentRow) return;

            if ("True" == ly_stocktake_mainDataGridView.CurrentRow.Cells["完成"].Value.ToString())
            {
                MessageBox.Show("盘点已完成,操作取消...", "注意");
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

                this.ly_stocktake_detailBindingSource.RemoveCurrent();

                this.ly_stocktake_detailDataGridView.EndEdit();
                this.ly_stocktake_detailBindingSource.EndEdit();
                this.ly_stocktake_detailTableAdapter.Update(this.lYStoreMange.ly_stocktake_detail);


            }
        }

        private void ly_stocktake_detailDataGridView_CellValidated(object sender, DataGridViewCellEventArgs e)
        {


        }

        private void toolStripButton6_Click(object sender, EventArgs e)
        {

            if (null == this.ly_stocktake_mainDataGridView.CurrentRow) return;

            string checkNum = this.ly_stocktake_mainDataGridView.CurrentRow.Cells["盘点编号1"].Value.ToString();


            this.ly_stocktake_mainTableAdapter.Fill(this.lYStoreMange.ly_stocktake_main);
            this.ly_stocktake_mainBindingSource.Position = this.ly_stocktake_mainBindingSource.Find("盘点编号", checkNum);
        }

        private void toolStripButton7_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ly_stocktake_detail_ONline01DataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void toolStripButton16_Click(object sender, EventArgs e)
        {
            ExportDataGridviewTOExcell.ExportDataGridview(this.ly_stocktake_detail_ONline_Explode01DataGridView, true);
        }

        private void toolStripButton17_Click(object sender, EventArgs e)
        {
            ExportDataGridviewTOExcell.ExportDataGridview(this.ly_stocktake_detail_ONline_Explode02DataGridView, true);
        }

        private void toolStripButton18_Click(object sender, EventArgs e)
        {
            ExportDataGridviewTOExcell.ExportDataGridview(this.ly_stocktake_detail_ONline_Explode03DataGridView, true);
        }

        private void ly_stocktake_detail_ONline_Explode01DataGridView_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView dgv = sender as DataGridView;

            if (true == dgv.ReadOnly) return;

            DataGridViewCell dgc = dgv.Rows[e.RowIndex].Cells[e.ColumnIndex];

            //ly_stocktake_detailDataGridView.CurrentCell.OwningColumn.DataPropertyName

            if ("盘点数量" == dgc.OwningColumn.DataPropertyName)
            {
                //SetlastColumn();
                dgc.Style.BackColor = Color.White;
                dgc.Style.ForeColor = Color.Teal;

                lastRow = e.RowIndex;
                lastcolumn = e.ColumnIndex;
            }
        }

        private void ly_stocktake_detail_ONline_Explode01DataGridView_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView dgv = sender as DataGridView;

            if (true == dgv.ReadOnly) return;

            DataGridViewCell dgc = dgv.Rows[e.RowIndex].Cells[e.ColumnIndex];
            if ("盘点数量" == dgc.OwningColumn.DataPropertyName)
            {





                dgc.Style.BackColor = Color.Teal;
                dgc.Style.ForeColor = Color.White;
            }
        }

        private void ly_stocktake_detail_ONline_Explode01DataGridView_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if ("True" == ly_stocktake_mainDataGridView.CurrentRow.Cells["完成"].Value.ToString())
            {
                //MessageBox.Show("盘点已完成,操作取消...", "注意");
                return;

            }

            DataGridView dgv = sender as DataGridView;

            if (true == dgv.ReadOnly) return;

            DataGridViewCell dgc = dgv.Rows[e.RowIndex].Cells[e.ColumnIndex];



            if ("盘点数量" == dgc.OwningColumn.DataPropertyName)
            {

                decimal newCheckCount;

                if ("" == e.FormattedValue.ToString().Replace(" ", ""))
                {
                    dgc.Value = DBNull.Value;
                    SaveOnline();
                    //e.Cancel = false ;
                    return;

                }

                if (!decimal.TryParse(e.FormattedValue.ToString(), out newCheckCount))
                {
                    MessageBox.Show("数据格式错误...", "注意");
                    e.Cancel = true;

                }
                else
                {
                    SaveOnline();
                }
            }
            else
            {

                return;

            }
        }

        private void SaveOnline()
        {



            if (this.tabControl1.SelectedIndex == 1)
            {

                this.ly_stocktake_detail_ONline_Explode01DataGridView.EndEdit();
                this.ly_stocktake_detail_ONline_Explode01BindingSource.EndEdit();
                this.ly_stocktake_detail_ONline_Explode01TableAdapter.Update(this.lYStoreMange.ly_stocktake_detail_ONline_Explode01);


            }
            else if (this.tabControl1.SelectedIndex == 2)
            {
                this.ly_stocktake_detail_ONline_Explode02DataGridView.EndEdit();
                this.ly_stocktake_detail_ONline_Explode02BindingSource.EndEdit();
                this.ly_stocktake_detail_ONline_Explode02TableAdapter.Update(this.lYStoreMange.ly_stocktake_detail_ONline_Explode02);

            }
            else if (this.tabControl1.SelectedIndex == 3)
            {
                this.ly_stocktake_detail_ONline_Explode03DataGridView.EndEdit();
                this.ly_stocktake_detail_ONline_Explode03BindingSource.EndEdit();
                this.ly_stocktake_detail_ONline_Explode03TableAdapter.Update(this.lYStoreMange.ly_stocktake_detail_ONline_Explode03);

            }

            //decimal checknum;

            //if (!string.IsNullOrEmpty(dgv.CurrentRow.Cells["盘点数量"].Value.ToString()))
            //    checknum = decimal.Parse(dgv.CurrentRow.Cells["盘点数量"].Value.ToString());
            //else
            //    checknum = 0;

            //decimal unitprice;

            //if (!string.IsNullOrEmpty(dgv.CurrentRow.Cells["库存单价"].Value.ToString()))
            //    unitprice = decimal.Parse(dgv.CurrentRow.Cells["库存单价"].Value.ToString());
            //else
            //    unitprice = 0;

            //decimal storenumber;

            //if (!string.IsNullOrEmpty(dgv.CurrentRow.Cells["库存数量"].Value.ToString()))
            //    storenumber = decimal.Parse(dgv.CurrentRow.Cells["库存数量"].Value.ToString());
            //else
            //    storenumber = 0;

            //decimal borrowumber;

            //if (!string.IsNullOrEmpty(dgv.CurrentRow.Cells["借用数量"].Value.ToString()))
            //    borrowumber = decimal.Parse(dgv.CurrentRow.Cells["借用数量"].Value.ToString());
            //else
            //    borrowumber = 0;



            //dgv.CurrentRow.Cells["盘点金额1"].OwningColumn.ReadOnly = false;
            //dgv.CurrentRow.Cells["盘点金额1"].Value = checknum * unitprice;
            //dgv.CurrentRow.Cells["盘点金额1"].OwningColumn.ReadOnly = true;

            //dgv.CurrentRow.Cells["盈亏数量"].OwningColumn.ReadOnly = false;
            //dgv.CurrentRow.Cells["盈亏数量"].Value = checknum + borrowumber - storenumber;
            //dgv.CurrentRow.Cells["盈亏数量"].OwningColumn.ReadOnly = true;

            //dgv.CurrentRow.Cells["盈亏金额1"].OwningColumn.ReadOnly = false;
            //dgv.CurrentRow.Cells["盈亏金额1"].Value = checknum * unitprice + borrowumber * unitprice - storenumber * unitprice;
            //dgv.CurrentRow.Cells["盈亏金额1"].OwningColumn.ReadOnly = true;



        }

        private void toolStripButton27_Click(object sender, EventArgs e)
        {
            if (null == this.ly_stocktake_detailDataGridView.CurrentRow) return;

            //if ("" == ly_sales_contract_mainDataGridView.CurrentRow.Cells["类别"].Value.ToString())
            //{

            //    MessageBox.Show("请选择合同类别,然后打印...", "注意");
            //    return;
            //}

            NewFrm.Show(this); ;

            //this.ly_plan_getmaterialTableAdapter.Fill(this.lYPlanMange.ly_plan_getmaterial, parentId);

            BaseReportView queryForm = new BaseReportView();

            queryForm.Text = "中原精密盘点报表";

            queryForm.Printdata = this.lYStoreMange;




            queryForm.PrintCrystalReport = new LY_InventoryRepore();

            NewFrm.Hide(this);

            queryForm.ShowDialog();
        }

        private void ly_stocktake_detailDataGridView_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if ("True" == ly_stocktake_mainDataGridView.CurrentRow.Cells["完成"].Value.ToString())
            {
                MessageBox.Show("盘点已完成,操作取消...", "注意");
                return;

            }

            DataGridView dgv = sender as DataGridView;

            if (null == dgv.CurrentRow) return;


            if ("旧货数量" == dgv.CurrentCell.OwningColumn.Name)
            {


                ChangeValue queryForm = new ChangeValue();

                queryForm.OldValue = dgv.CurrentCell.Value.ToString();
                queryForm.NewValue = "";
                queryForm.ChangeMode = "value";
                queryForm.ShowDialog();




                if (queryForm.NewValue != "")
                {
                    dgv.CurrentRow.Cells["旧货数量"].Value = queryForm.NewValue;

                    this.ly_stocktake_detailDataGridView.EndEdit();
                    this.ly_stocktake_detailBindingSource.EndEdit();
                    this.ly_stocktake_detailTableAdapter.Update(this.lYStoreMange.ly_stocktake_detail);

                }
                else
                {

                    dgv.CurrentRow.Cells["旧货数量"].Value = DBNull.Value;

                    this.ly_stocktake_detailDataGridView.EndEdit();
                    this.ly_stocktake_detailBindingSource.EndEdit();
                    this.ly_stocktake_detailTableAdapter.Update(this.lYStoreMange.ly_stocktake_detail);
                }
                return;

            }

            if ("借用数量" == dgv.CurrentCell.OwningColumn.Name)
            {


                ChangeValue queryForm = new ChangeValue();

                queryForm.OldValue = dgv.CurrentCell.Value.ToString();
                queryForm.NewValue = "";
                queryForm.ChangeMode = "value";
                queryForm.ShowDialog();




                if (queryForm.NewValue != "")
                {
                    dgv.CurrentRow.Cells["借用数量"].Value = queryForm.NewValue;

                    this.ly_stocktake_detailDataGridView.EndEdit();
                    this.ly_stocktake_detailBindingSource.EndEdit();
                    this.ly_stocktake_detailTableAdapter.Update(this.lYStoreMange.ly_stocktake_detail);

                }
                else
                {

                    dgv.CurrentRow.Cells["借用数量"].Value = DBNull.Value;

                    this.ly_stocktake_detailDataGridView.EndEdit();
                    this.ly_stocktake_detailBindingSource.EndEdit();
                    this.ly_stocktake_detailTableAdapter.Update(this.lYStoreMange.ly_stocktake_detail);
                }
                return;

            }


        }

        private void ly_stocktake_mainDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void toolStripButton25_Click(object sender, EventArgs e)
        {
            ExportDataGridviewTOExcell.ExportDataGridview(this.dataGridView1, true);
        }

        private void toolStripButton31_Click(object sender, EventArgs e)
        {
            ExportDataGridviewTOExcell.ExportDataGridview(this.dataGridView2, true);
        }


    }
}