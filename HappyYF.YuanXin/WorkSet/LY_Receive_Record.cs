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
    public partial class LY_Receive_Record : Form
    {
        private string nowfilterStr = "";
        private string nowusercode = "";

        private string formState;
        private int nowRow;



        private string nowfillstragecode = "";

        private string ori_direction;
        private string dis_direction;

        private decimal now_fanku = 0;
        private decimal now_tuiku = 0;




        public LY_Receive_Record()
        {
            InitializeComponent();

            this.ly_sales_receive_sel_newTableAdapter.CommandTimeout = 0;
        }

        private void t_usersBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            //this.Validate();
            //this.t_usersBindingSource.EndEdit();
            ////this.tableAdapterManager.UpdateAll(this.yonghuDataSet);
            //this.t_usersTableAdapter.Update( this.yonghuDataSet.T_users);

            SetViewState("View");

        }

        private void SetViewState(string state)
        {
            if ("View" == state)
            {



                this.treeView1.Focus();


            }
            else
            {

            }

        }

        private void Yonghu_Load(object sender, EventArgs e)
        {

            this.f_sales_receive_MainTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;

            this.t_usersTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            this.t_usersTableAdapter.Fill(this.lYSalseMange2.T_users);

            this.dateTimePicker1.Text = DateTime.Today.AddMonths(-1).Date.ToString();
            this.dateTimePicker2.Text = DateTime.Today.AddDays(1).Date.ToString();

            this.SetFormState("View");


            this.ly_sales_borrow_detail_forreceiveTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            this.ly_sales_contract_detail_forreceiveTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            this.ly_sales_borrow_detail_forreceiveDiffTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;


            this.ly_sales_receive_itemDetail_childTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            this.ly_sales_receive_itemDetailTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            this.ly_sales_receive_itemsTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            this.ly_sales_receive_allitemsTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;


            this.ly_sales_receive_sel_newTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            //this.ly_sales_receiveTableAdapter.Fill(this.lYSalseMange2.ly_sales_receive, this.dateTimePicker1.Value, this.dateTimePicker2.Value);

            this.ly_lsptb_selTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            this.ly_lsptb_selTableAdapter.Fill(this.lYSalseMange.ly_lsptb_sel);



            //this.ly_material_plan_mainTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            // this.ly_material_plan_mainTableAdapter.Fill(this.lYSalseMange.ly_material_plan_main, jhlbToolStripTextBox.Text);




            this.nowusercode = SQLDatabase.NowUserID;











            string selAllString;

            if (SQLDatabase.CheckHaveRight(SQLDatabase.NowUserID, "营业综合信息"))
            {

                selAllString = "SELECT  a.salesregion_code, a.salesregion_code+':'+a.salesregion_name as salesregion_name,b.yhbm,b.yhbm+':'+b.yhmc as yhmc FROM  ly_salesregion a left join T_users b on a.salesregion_code=b.salesregion_code ORDER BY  salesregion_code ";
                selAllString = "SELECT distinct  a.salesregion_code, a.salesregion_code+':'+a.salesregion_name as salesregion_name,a.salesperson_code as yhbm,a.salesperson_code+':'+a.salesperson_name as yhmc FROM  ly_sales_client_forreceive a ORDER BY  salesregion_code ";
            }
            else if (SQLDatabase.CheckHaveRight(SQLDatabase.NowUserID, "营业区域信息"))
            {

                selAllString = "SELECT  a.salesregion_code, a.salesregion_code+':'+a.salesregion_name as salesregion_name,b.yhbm,b.yhbm+':'+b.yhmc as yhmc FROM  ly_salesregion a left join T_users b on a.salesregion_code=b.salesregion_code  where b.salesregion_code='" + SQLDatabase.nowSalesregioncode() + "' ORDER BY  salesregion_code ";
                selAllString = "SELECT distinct  a.salesregion_code, a.salesregion_code+':'+a.salesregion_name as salesregion_name,a.salesperson_code as yhbm,a.salesperson_code+':'+a.salesperson_name as yhmc FROM  ly_sales_client_forreceive a  where a.salesregion_code='" + SQLDatabase.nowSalesregioncode() + "' ORDER BY  salesregion_code ";
            }
            else
            {
                selAllString = "SELECT  a.salesregion_code, a.salesregion_code+':'+a.salesregion_name as salesregion_name,b.yhbm,b.yhbm+':'+b.yhmc as yhmc FROM  ly_salesregion a left join T_users b on a.salesregion_code=b.salesregion_code  where b.yhbm='" + SQLDatabase.NowUserID + "' ORDER BY  salesregion_code ";
                selAllString = "SELECT distinct  a.salesregion_code, a.salesregion_code+':'+a.salesregion_name as salesregion_name,a.salesperson_code as yhbm,a.salesperson_code+':'+a.salesperson_name as yhmc FROM  ly_sales_client_forreceive a where a.salesperson_code='" + SQLDatabase.NowUserID + "' ORDER BY  salesregion_code "; ;
            }



            SqlDataAdapter salesregionAdapter = new SqlDataAdapter(selAllString, SQLDatabase.Connectstring);

            DataSet salesregionData = new DataSet();
            salesregionAdapter.Fill(salesregionData);


            System.Windows.Forms.TreeNode TNode = new System.Windows.Forms.TreeNode();
            TNode.Text = "中原精密有限公司";

            if (SQLDatabase.CheckHaveRight(SQLDatabase.NowUserID, "营业综合信息"))
            {
                TNode.Tag = "";
            }
            else if (SQLDatabase.CheckHaveRight(SQLDatabase.NowUserID, "营业区域信息"))
            {
                TNode.Tag = "salesregion_code='" + SQLDatabase.nowSalesregioncode() + "'";
            }
            else
            {
                TNode.Tag = "salesperson_code='" + SQLDatabase.NowUserID + "'";

            }

            this.treeView1.Nodes.Add(TNode);

            MakeTreeView(salesregionData.Tables[0], null, TNode);

            this.treeView1.ExpandAll();

            if (SQLDatabase.CheckHaveRight(SQLDatabase.NowUserID, "营业综合信息"))
            {
                //this.treeView1.Visible = true;
                this.splitContainer1.Panel1Collapsed = false;
            }
            else if (SQLDatabase.CheckHaveRight(SQLDatabase.NowUserID, "营业区域信息"))
            {
                //this.treeView1.Visible = true;
                this.splitContainer1.Panel1Collapsed = false;
                //this.nowfilterStr = "salesperson_code='" + SQLDatabase.NowUserID + "'";
                this.nowfilterStr = "salesregion_code='" + SQLDatabase.nowSalesregioncode() + "'";
            }
            else
            {
                //this.treeView1.Visible = false;
                this.splitContainer1.Panel1Collapsed = true;
                this.nowfilterStr = "salesperson_code='" + SQLDatabase.NowUserID + "'";


                this.nowfillstragecode = "single";
                this.nowusercode = SQLDatabase.NowUserID;

                //this.ly_sales_businessTableAdapter.Fill(this.lYSalseMange.ly_sales_business, this.nowusercode, this.nowfillstragecode, this.dateTimePicker1.Value, this.dateTimePicker2.Value);


            }

            //this.ly_sales_contract_main1DataGridView.SelectionChanged -= ly_sales_contract_main1DataGridView_SelectionChanged;
            //this.ly_sales_contract_main1TableAdapter.Fill(this.lYSalseMange.ly_sales_contract_main1,"","full" );
            //this.ly_sales_contract_main1DataGridView.SelectionChanged += ly_sales_contract_main1DataGridView_SelectionChanged;

            //SetViewState("View");

        }





        private void MakeTreeView(DataTable table, string salesregionCode, System.Windows.Forms.TreeNode PNode)
        {


            DataRow[] dr;
            string now_salesregion_code;
            string last_salesregion_code = "___";



            dr = table.Select("salesregion_code is not  null");

            System.Windows.Forms.TreeNode TNode = null;
            System.Windows.Forms.TreeNode CNode = null;

            foreach (DataRow d in dr)
            {
                now_salesregion_code = d["salesregion_name"].ToString();



                if (last_salesregion_code != now_salesregion_code)
                {

                    TNode = new System.Windows.Forms.TreeNode();

                    TNode.Text = d["salesregion_name"].ToString();

                    if (SQLDatabase.CheckHaveRight(SQLDatabase.NowUserID, "营业综合信息"))
                    {
                        TNode.Tag = "salesregion_code='" + d["salesregion_code"].ToString() + "'";
                    }
                    else if (SQLDatabase.CheckHaveRight(SQLDatabase.NowUserID, "营业区域信息"))
                    {
                        TNode.Tag = "salesregion_code='" + d["salesregion_code"].ToString() + "'";
                    }

                    else
                    {

                        TNode.Tag = "salesperson_code='" + SQLDatabase.NowUserID + "'";
                    }
                    if (PNode == null)
                    {
                        this.treeView1.Nodes.Add(TNode);
                    }
                    else
                    {
                        PNode.Nodes.Add(TNode);
                        CNode = new System.Windows.Forms.TreeNode();
                        CNode.Text = d["yhmc"].ToString();
                        CNode.Tag = "salesperson_code='" + d["yhbm"].ToString() + "'";
                        if (TNode == null)
                        {
                            this.treeView1.Nodes.Add(TNode);
                        }
                        else
                        {
                            TNode.Nodes.Add(CNode);
                        }
                    }
                }
                else
                {
                    CNode = new System.Windows.Forms.TreeNode();
                    CNode.Text = d["yhmc"].ToString();
                    CNode.Tag = "salesperson_code='" + d["yhbm"].ToString() + "'";
                    if (TNode == null)
                    {
                        this.treeView1.Nodes.Add(TNode);
                    }
                    else
                    {
                        TNode.Nodes.Add(CNode);
                    }


                }
                last_salesregion_code = now_salesregion_code;

            }



        }



        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            this.nowfilterStr = e.Node.Tag.ToString();

            if (e.Node.Level == 2)
            {


                this.nowusercode = this.nowfilterStr.Substring(this.nowfilterStr.Length - 4, 3);
                this.nowfillstragecode = "single";



                //this.ly_sales_contract_main1DataGridView.SelectionChanged -= ly_sales_contract_main1DataGridView_SelectionChanged;
                //this.ly_sales_contract_main1TableAdapter.Fill(this.lYSalseMange.ly_sales_contract_main1, this.nowusercode,"single", this.dateTimePicker1.Value , this.dateTimePicker2.Value );
                //AddSummationRow_New(ly_sales_contract_main1BindingSource, ly_sales_contract_main1DataGridView);
                //this.ly_sales_contract_main1DataGridView.SelectionChanged += ly_sales_contract_main1DataGridView_SelectionChanged;

            }
            else if (e.Node.Level == 1)
            {

                this.nowusercode = this.nowfilterStr.Substring(this.nowfilterStr.Length - 3, 2);
                this.nowfillstragecode = "region";


            }
            else if (e.Node.Level == 0)
            {

                this.nowusercode = "";
                this.nowfillstragecode = "full";
                if (SQLDatabase.CheckHaveRight(SQLDatabase.NowUserID, "营业区域信息"))
                {
                    this.nowusercode = this.nowfilterStr.Substring(this.nowfilterStr.Length - 3, 2);
                    this.nowfillstragecode = "region";
                }


            }



            this.ly_sales_receive_sel_newTableAdapter.Fill(this.lYSalseMange2.ly_sales_receive_sel_new, this.nowusercode, this.nowfillstragecode, this.dateTimePicker1.Value, this.dateTimePicker2.Value);

            // AddSummationRow_New(ly_sales_contract_main1BindingSource, ly_sales_contract_main1DataGridView);
            //this.ly_sales_contract_main1DataGridView.SelectionChanged += ly_sales_contract_main1DataGridView_SelectionChanged;

            this.groupBox1.Text = e.Node.Text + "收件信息列表";
        }



        private TreeNode FindNode(TreeNodeCollection tnParent, string strValue)
        {

            if (tnParent == null) return null;

            //if (tnParent.Text == strValue) return tnParent;



            TreeNode tnRet = null;

            foreach (TreeNode tn in tnParent)
            {



                if (tn.Text == strValue)
                {
                    tnRet = tn;
                }
                else
                {

                    tnRet = FindNode(tn.Nodes, strValue);
                }

                if (tnRet != null) break;

            }

            return tnRet;

        }






        private void toolStripButton21_Click(object sender, EventArgs e)
        {

            this.ly_sales_receive_sel_newTableAdapter.Fill(this.lYSalseMange2.ly_sales_receive_sel_new, this.nowusercode, this.nowfillstragecode, this.dateTimePicker1.Value, this.dateTimePicker2.Value);
            //AddSummationRow_New(ly_sales_contract_main1BindingSource, ly_sales_contract_main1DataGridView);
        }







        private void ly_sales_contract_detailDataGridView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
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
                        e.Value = "";
                    }
                }
            }
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

            return;
            if (null == dgv.CurrentRow) return;

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

        private void toolStripButton27_Click(object sender, EventArgs e)
        {

        }

        private void toolStripTextBox5_KeyUp(object sender, KeyEventArgs e)
        {
            string filterString;


            filterString = GetDataGridviewMultiFilterString.DGVMultiFilterString(this.ly_sales_receiveDataGridView, this.toolStripTextBox5.Text);


            this.ly_sales_receive_sel_newBindingSource.Filter = filterString;
        }

        private void toolStripTextBox5_Enter(object sender, EventArgs e)
        {
            toolStripTextBox5.Text = "";

            this.ly_sales_receive_sel_newBindingSource.Filter = "";
        }

        private void toolStripButton16_Click(object sender, EventArgs e)
        {
            //ExportDataGridviewTOExcell.ExportDataGridview(this.ly_sales_businessDataGridView, true);
        }






        private TreeNode FindGroupNode(TreeNodeCollection tnParent, string strValue)
        {

            if (tnParent == null) return null;

            //if (tnParent.Text == strValue) return tnParent;



            TreeNode tnRet = null;

            foreach (TreeNode tn in tnParent)
            {



                if (tn.Tag.ToString() == strValue)
                {
                    tnRet = tn;
                }
                else
                {

                    tnRet = FindGroupNode(tn.Nodes, strValue);
                }

                if (tnRet != null) break;

            }

            return tnRet;

        }




        private void toolStripButton8_Click(object sender, EventArgs e)
        {
            if (null == ly_sales_receiveDataGridView.CurrentRow)
            {

                return;
            }

            string nowoperpter = this.ly_sales_receiveDataGridView.CurrentRow.Cells["收件人"].Value.ToString();
            if (nowoperpter != SQLDatabase.nowUserName())
            {
                MessageBox.Show("请收件人:" + nowoperpter + "删除", "注意");
                return;
            }

            if (0 < ly_sales_receive_itemsDataGridView.RowCount)
            {
                MessageBox.Show("先删除收件项目，才能删除收件单...", "注意");
                return;

            }

            //if ("True" == ly_sales_contract_mainDataGridView.CurrentRow.Cells["批准"].Value.ToString())
            //{
            //    MessageBox.Show("合同已经执行,不能删除数据...", "注意");
            //    return;

            //}

            //if ("True" == ly_sales_contract_mainDataGridView.CurrentRow.Cells["审核"].Value.ToString())
            //{
            //    MessageBox.Show("合同已经审批,不能删除数据...", "注意");
            //    return;

            //}


            string message = "确定删除当前收件记录吗？";
            string caption = "提示...";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result;



            result = MessageBox.Show(message, caption, buttons,
            MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

            if (result == DialogResult.Yes)
            {

                this.ly_sales_receive_sel_newBindingSource.RemoveCurrent();


                ly_sales_receiveDataGridView.EndEdit();
                ly_sales_receive_sel_newBindingSource.EndEdit();



                this.ly_sales_receive_sel_newTableAdapter.Update(this.lYSalseMange2.ly_sales_receive_sel_new);

                //this.ly_sales_contract_main1DataGridView.SelectionChanged -= ly_sales_contract_main1DataGridView_SelectionChanged;
                //this.ly_sales_contract_main1TableAdapter.Fill(this.lYSalseMange.ly_sales_contract_main1, this.nowusercode, "single", this.dateTimePicker1.Value, this.dateTimePicker2.Value);
                //this.ly_sales_contract_main1DataGridView.SelectionChanged += ly_sales_contract_main1DataGridView_SelectionChanged;
            }
        }




        //private void toolStripButton18_Click(object sender, EventArgs e)
        //{
        //    ExportDataGridviewTOExcell.ExportDataGridview(this.ly_sales_contract_mainDataGridView, true);
        //}





        private void toolStripTextBox6_KeyUp(object sender, KeyEventArgs e)
        {
            string filterString;
            ToolStripTextBox tst = sender as ToolStripTextBox;

            filterString = GetDataGridviewMultiFilterString.DGVMultiFilterString(this.ly_lsptb_selDataGridView, tst.Text);


            this.ly_lsptb_selBindingSource.Filter = filterString;
        }

        private void toolStripTextBox6_Enter(object sender, EventArgs e)
        {
            ToolStripTextBox tst = sender as ToolStripTextBox;
            tst.Text = "";

            this.ly_lsptb_selBindingSource.Filter = "";
        }

        private void ly_lsptb_selDataGridView_DoubleClick(object sender, EventArgs e)
        {
            if (null == this.ly_sales_receiveDataGridView.CurrentRow) return;
            if (null == ly_lsptb_selDataGridView.CurrentRow) return;

            //if ("False" == this.contractCanchenged)
            //{
            //    MessageBox.Show("合同已经提交执行,不能修改数据...", "注意");
            //    return;

            //}

            //decimal nowsalesprice;
            //if ("" != this.ly_lsptb_selDataGridView.CurrentRow.Cells["营业定价3"].Value.ToString())
            //{
            //    nowsalesprice = decimal.Parse(this.ly_lsptb_selDataGridView.CurrentRow.Cells["营业定价3"].Value.ToString());
            //}
            //else
            //{
            //    nowsalesprice = 0;
            //}

            //if (nowsalesprice == 0)
            //{

            //    MessageBox.Show("产品无定价,不能销售...", "注意");
            //    return;
            //}



            string nowitemno = this.ly_lsptb_selDataGridView.CurrentRow.Cells["物料编号a"].Value.ToString();


            string nowreceiveCode = ly_sales_receiveDataGridView.CurrentRow.Cells["收件编号"].Value.ToString();



            int hadarranged = ly_sales_receive_itemsBindingSource.Find("产品编码", nowitemno);
            if (-1 < hadarranged)
            {
                this.ly_sales_receive_itemsBindingSource.Position = this.ly_sales_receive_itemsBindingSource.Find("产品编码", nowitemno);
                MessageBox.Show("当前收件编号已有选择,修改数量即可...", "注意");
                return;

            }

            InserSalseReceiveItems(nowreceiveCode, nowitemno);



            this.ly_sales_receive_itemsTableAdapter.Fill(this.lYSalseMange2.ly_sales_receive_items, nowreceiveCode);

            this.ly_sales_receive_itemsBindingSource.Position = this.ly_sales_receive_itemsBindingSource.Find("产品编码", nowitemno);

            this.ly_sales_receive_itemsDataGridView.CurrentCell = this.ly_sales_receive_itemsDataGridView.CurrentRow.Cells["收件数量"];
        }
        private static void InserSalseReceiveItems(string receiveCode, string nowitem)
        {

            string insStr;


            insStr = " INSERT INTO ly_sales_receive_items  " +
         "( receive_code,itemno) " +
         " values ('" + receiveCode + "','" + nowitem + "')";




            using (TransactionScope scope = new TransactionScope())
            {

                SqlConnection sqlConnection1 = new SqlConnection(SQLDatabase.Connectstring);
                SqlCommand cmd = new SqlCommand();

                cmd.CommandText = insStr;
                cmd.CommandType = CommandType.Text;
                cmd.Connection = sqlConnection1;



                sqlConnection1.Open();
                cmd.ExecuteNonQuery();

                sqlConnection1.Close();

                scope.Complete();
            }
        }

        private void ly_sales_contract_detailDataGridView_CellFormatting_1(object sender, DataGridViewCellFormattingEventArgs e)
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
                        e.Value = "";
                    }
                }
            }
        }



        private void SaveDetailItem()
        {
            //this.ly_sales_contract_detailBindingSource.EndEdit();
            //this.ly_sales_contract_detailTableAdapter.Update(this.lYSalseMange.ly_sales_contract_detail);

            //int nowdetailId;
            //if (null != ly_sales_contract_detailDataGridView.CurrentRow)
            //{
            //    nowdetailId = int.Parse(ly_sales_contract_detailDataGridView.CurrentRow.Cells["id_detail"].Value.ToString());
            //}
            //else
            //{
            //    nowdetailId = 0;
            //}


            //int nowcontractId;
            //if (null != ly_sales_contract_mainDataGridView.CurrentRow)
            //{
            //    nowcontractId = int.Parse(ly_sales_contract_mainDataGridView.CurrentRow.Cells["id_main"].Value.ToString());
            //}
            //else
            //{
            //    nowcontractId = 0;
            //}


            ////if (null == this.ly_sales_clientDataGridView.CurrentRow)
            ////{ 
            ////  this.ly_sales_clientDataGridView.
            ////}

            ////string nowclientcode = this.ly_sales_clientDataGridView.CurrentRow.Cells["客户编码"].Value.ToString();
            ////this.ly_sales_clientDataGridView.SelectionChanged -= ly_sales_clientDataGridView_SelectionChanged;
            ////this.ly_sales_clientTableAdapter.Fill(this.lYSalseMange.ly_sales_client);
            ////this.ly_sales_clientDataGridView.SelectionChanged += ly_sales_clientDataGridView_SelectionChanged;

            ////this.ly_sales_clientBindingSource.Position = this.ly_sales_clientBindingSource.Find("客户编码", nowclientCode);

            //this.ly_sales_contract_mainDataGridView.CurrentRow.Cells["录入人c"].Value = SQLDatabase.nowUserName();
            //this.ly_sales_contract_mainDataGridView.EndEdit();
            //this.ly_sales_contract_main_forbusinessBindingSource.EndEdit();
            //this.ly_sales_contract_main_forbusinessTableAdapter.Update(this.lYSalseMange.ly_sales_contract_main_forbusiness);

            //this.ly_sales_contract_main_forbusinessTableAdapter.Fill(this.lYSalseMange.ly_sales_contract_main_forbusiness, this.nowcontractCode);
            //this.ly_sales_contract_main_forbusinessBindingSource.Position = this.ly_sales_contract_main_forbusinessBindingSource.Find("id", nowcontractId);

            ////this.ly_sales_contract_main1DataGridView.SelectionChanged -= ly_sales_contract_main1DataGridView_SelectionChanged;
            ////this.ly_sales_contract_main1TableAdapter.Fill(this.lYSalseMange.ly_sales_contract_main1, this.nowusercode, this.nowfillstragecode, this.dateTimePicker1.Value, this.dateTimePicker2.Value);
            ////this.ly_sales_contract_main1DataGridView.SelectionChanged += ly_sales_contract_main1DataGridView_SelectionChanged;

            ////this.ly_sales_contract_detailTableAdapter.Fill(this.lYSalseMange.ly_sales_contract_detail, nowinnerCode);

            //this.ly_sales_contract_detailBindingSource.Position = this.ly_sales_contract_detailBindingSource.Find("id", nowdetailId);

            //this.ly_sales_contract_detailDataGridView.CurrentRow.Cells["单件折扣"].Value = this.ly_sales_contract_detailDataGridView.CurrentRow.Cells["单件折扣"].Value;
            //this.ly_sales_contract_detailDataGridView.CurrentRow.Cells["单价"].Value = this.ly_sales_contract_detailDataGridView.CurrentRow.Cells["单价"].Value;

            //this.ly_sales_contract_detailDataGridView.EndEdit();
            //this.ly_sales_contract_detailBindingSource.EndEdit();
            //this.ly_sales_contract_detailTableAdapter.Update(this.lYSalseMange.ly_sales_contract_detail);
        }



        private void bindingNavigatorDeleteItem2_Click(object sender, EventArgs e)
        {
            DeleteDetail();
        }
        private void DeleteDetail()
        {
            //if (null == ly_sales_contract_detailDataGridView.CurrentRow)
            //{


            //    return;
            //}

            //if ("False" == this.contractCanchenged)
            //{
            //    MessageBox.Show("合同已经提交,不能删除数据...", "注意");
            //    return;

            //}


            //string message = "确定删除当前条目吗？";
            //string caption = "提示...";
            //MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            //DialogResult result;



            //result = MessageBox.Show(message, caption, buttons,
            //MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

            //if (result == DialogResult.Yes)
            //{

            //    this.ly_sales_contract_detailBindingSource.RemoveCurrent();


            //    ly_sales_contract_detailDataGridView.EndEdit();
            //    ly_sales_contract_detailBindingSource.EndEdit();



            //    this.ly_sales_contract_detailTableAdapter.Update(this.lYSalseMange.ly_sales_contract_detail);


            //}
        }





        private void toolStripButton38_Click(object sender, EventArgs e)
        {
            this.ly_lsptb_selTableAdapter.Fill(this.lYSalseMange.ly_lsptb_sel);
        }

        private void toolStripButton6_Click(object sender, EventArgs e)
        {

            string nowclientcode = "111";

            LY_Salesreceive_Add queryForm = new LY_Salesreceive_Add();

            queryForm.salesclientcode = nowclientcode;
            queryForm.runmode = "增加";


            queryForm.StartPosition = FormStartPosition.CenterParent;
            queryForm.ShowDialog();

            if (queryForm.DialogResult != DialogResult.Cancel)
            {
                this.ly_sales_receive_sel_newTableAdapter.Fill(this.lYSalseMange2.ly_sales_receive_sel_new, this.nowusercode, this.nowfillstragecode, this.dateTimePicker1.Value, this.dateTimePicker2.Value);

                this.ly_sales_receive_sel_newBindingSource.Position = this.ly_sales_receive_sel_newBindingSource.Find("收件编号", queryForm.receive_code);


            }
        }

        private void toolStripButton7_Click(object sender, EventArgs e)
        {
            if (null == ly_sales_receiveDataGridView.CurrentRow) return;


            string nowoperpter = this.ly_sales_receiveDataGridView.CurrentRow.Cells["收件人"].Value.ToString();
            if (nowoperpter != SQLDatabase.nowUserName())
            {
                MessageBox.Show("请收件人:" + nowoperpter + "修改", "注意");
                return;
            }

            string nowrecordCode = ly_sales_receiveDataGridView.CurrentRow.Cells["收件编号"].Value.ToString();


            LY_Salesreceive_Add queryForm = new LY_Salesreceive_Add();

            queryForm.salesclientcode = "111";
            queryForm.receive_code = nowrecordCode;
            queryForm.runmode = "修改";


            queryForm.StartPosition = FormStartPosition.CenterParent;
            queryForm.ShowDialog();

            if (queryForm.DialogResult != DialogResult.Cancel)
            {
                this.ly_sales_receive_sel_newTableAdapter.Fill(this.lYSalseMange2.ly_sales_receive_sel_new, this.nowusercode, this.nowfillstragecode, this.dateTimePicker1.Value, this.dateTimePicker2.Value);

                this.ly_sales_receive_sel_newBindingSource.Position = this.ly_sales_receive_sel_newBindingSource.Find("收件编号", queryForm.receive_code);


            }
        }

        private void ly_sales_receiveDataGridView_SelectionChanged(object sender, EventArgs e)
        {
            if (null == ly_sales_receiveDataGridView.CurrentRow)
            {
                this.ly_sales_receive_itemsTableAdapter.Fill(this.lYSalseMange2.ly_sales_receive_items, "asd");
                return;
            }


            string nowreceiveCode = ly_sales_receiveDataGridView.CurrentRow.Cells["收件编号"].Value.ToString();
            this.ly_sales_receive_itemsTableAdapter.Fill(this.lYSalseMange2.ly_sales_receive_items, nowreceiveCode);
            this.ly_sales_receive_allitemsTableAdapter.Fill(this.lYSalseMange2.ly_sales_receive_allitems, nowreceiveCode);


            if (null == ly_sales_receive_itemsDataGridView.CurrentRow)
            {
                this.ly_sales_receive_itemDetailTableAdapter.Fill(this.lYSalseMange2.ly_sales_receive_itemDetail, -1);
            }
        }

        private void ly_sales_receive_itemsDataGridView_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            DataGridView dgv = sender as DataGridView;

            if (null == dgv.CurrentRow) return;

            string nowoperpter = this.ly_sales_receiveDataGridView.CurrentRow.Cells["收件人"].Value.ToString();
            if (nowoperpter != SQLDatabase.nowUserName())
            {
                MessageBox.Show("请收件人:" + nowoperpter + "修改", "注意");
                return;
            }


            //if ("True" == ly_material_plan_mainDataGridView.CurrentRow.Cells["生产审批0"].Value.ToString())
            //{
            //    MessageBox.Show("依赖书已经生产审批,不能修改数据...", "注意");
            //    return;

            //}






            ///////////////////////////////////////////////////////////////
            if ("收件数量" == dgv.CurrentCell.OwningColumn.Name)
            {

                //if ("True" == dgv.CurrentRow.Cells["审批"].Value.ToString())
                //{

                //    MessageBox.Show("合同已经审批,不能修改数据...", "注意");
                //    return;
                //}

                ChangeValue queryForm = new ChangeValue();

                queryForm.OldValue = dgv.CurrentCell.Value.ToString();
                queryForm.NewValue = "";
                queryForm.ChangeMode = "value";
                queryForm.ShowDialog();




                if (queryForm.NewValue != "")
                {



                    dgv.CurrentRow.Cells["收件数量"].Value = queryForm.NewValue;


                    //dgv.CurrentRow.Cells["discount_money"].Value = DBNull.Value;

                    //dgv.CurrentRow.Cells["approve_flag"].Value = "False";ly_plan_getmaterialDataGridView

                    this.ly_sales_receive_itemsBindingSource.EndEdit();
                    try
                    {

                        this.ly_sales_receive_itemsTableAdapter.Update(this.lYSalseMange2.ly_sales_receive_items);

                        string nowitemno = dgv.CurrentRow.Cells["产品编码"].Value.ToString();


                        string nowreceiveCode = ly_sales_receiveDataGridView.CurrentRow.Cells["收件编号"].Value.ToString();


                        this.ly_sales_receive_itemsTableAdapter.Fill(this.lYSalseMange2.ly_sales_receive_items, nowreceiveCode);

                        this.ly_sales_receive_itemsBindingSource.Position = this.ly_sales_receive_itemsBindingSource.Find("产品编码", nowitemno);

                        this.ly_sales_receive_itemsDataGridView.CurrentCell = this.ly_sales_receive_itemsDataGridView.CurrentRow.Cells["收件数量"];
                    }

                    catch (SqlException sqle)
                    {

                        MessageBox.Show(sqle.Message.Split('\r')[0], "注意");
                        //MessageBox.Show(sqle.Message, "注意");
                    }





                }
                else
                {
                    //hT_Manage_ItemDataGridView.CurrentRow.Cells["apply_money"].Value = queryForm.NewValue;
                    //dgv.CurrentRow.Cells["discount_money"].Value = DBNull.Value;
                    //dgv.CurrentRow.Cells["apply_money"].Value = DBNull.Value;
                    //dgv.CurrentRow.Cells["approve_flag"].Value = "False";
                    //SaveChanged();

                }
                return;

            }



            /////////////////////////////////////////////////////////////////

            //if ("设备要求5" == dgv.CurrentCell.OwningColumn.Name)
            //{

            //    ChangeValue queryForm = new ChangeValue();

            //    queryForm.OldValue = dgv.CurrentCell.Value.ToString();
            //    queryForm.NewValue = "";
            //    queryForm.ChangeMode = "longstring";
            //    queryForm.ShowDialog();




            //    if (queryForm.NewValue != "")
            //    {
            //        dgv.CurrentRow.Cells["设备要求5"].Value = queryForm.NewValue;
            //        //dgv.CurrentRow.Cells["discount_money"].Value = DBNull.Value;

            //        //dgv.CurrentRow.Cells["approve_flag"].Value = "False";
            //        this.ly_plan_getmaterialBindingSource.EndEdit();
            //        this.ly_plan_getmaterialTableAdapter.Update(this.lYSalseMange.ly_plan_getmaterial);

            //        //this.ly_sales_contract_detailTableAdapter.Fill(this.lYSalseMange.ly_sales_contract_detail, nowinnerCode, 0);

            //        //CountPlanStru();

            //    }
            //    else
            //    {

            //    }
            //    return;

            //}
        }



        private void ly_sales_receive_itemsDataGridView_SelectionChanged(object sender, EventArgs e)
        {
            if (null == ly_sales_receive_itemsDataGridView.CurrentRow)
            {
                this.ly_sales_receive_itemDetailTableAdapter.Fill(this.lYSalseMange2.ly_sales_receive_itemDetail, -1);
                this.ly_sales_borrow_detail_forreceiveTableAdapter.Fill(this.lYSalseMange2.ly_sales_borrow_detail_forreceive, "aaa", "asd");
                this.ly_sales_contract_detail_forreceiveTableAdapter.Fill(this.lYSalseMange2.ly_sales_contract_detail_forreceive, "aaa", "asd");

                this.ly_sales_borrow_detail_forreceiveDiffTableAdapter.Fill(this.lYSalseMange2.ly_sales_borrow_detail_forreceiveDiff, "aaa", "asd");

                return;


            }



            int nowId = int.Parse(ly_sales_receive_itemsDataGridView.CurrentRow.Cells["id_items"].Value.ToString());

            string nowitemno = ly_sales_receive_itemsDataGridView.CurrentRow.Cells["产品编码"].Value.ToString();
            string clientcode = ly_sales_receiveDataGridView.CurrentRow.Cells["客户编码"].Value.ToString();


            this.ly_sales_receive_itemDetailTableAdapter.Fill(this.lYSalseMange2.ly_sales_receive_itemDetail, nowId);

            this.ly_sales_borrow_detail_forreceiveTableAdapter.Fill(this.lYSalseMange2.ly_sales_borrow_detail_forreceive, clientcode, nowitemno);
            this.ly_sales_contract_detail_forreceiveTableAdapter.Fill(this.lYSalseMange2.ly_sales_contract_detail_forreceive, nowitemno, clientcode);
            this.ly_sales_borrow_detail_forreceiveDiffTableAdapter.Fill(this.lYSalseMange2.ly_sales_borrow_detail_forreceiveDiff, clientcode, nowitemno);


            ///////////////////////////////////////////////////////////////////////////////

            DataGridView dgv = this.ly_sales_borrow_detail_clientDataGridView;

            foreach (DataGridViewRow dgr in dgv.Rows)
            {

                if (string.IsNullOrEmpty(dgr.Cells["未还数量jy"].Value.ToString())) continue;
                now_fanku = now_fanku + int.Parse(dgr.Cells["未还数量jy"].Value.ToString(), System.Globalization.NumberStyles.Number);



            }
            //////////////////////////////////////////////////////////
            dgv = this.ly_sales_contract_detail_forreceiveDataGridView;

            foreach (DataGridViewRow dgr in dgv.Rows)
            {

                if (string.IsNullOrEmpty(dgr.Cells["未退数量"].Value.ToString())) continue;
                now_tuiku = now_tuiku + int.Parse(dgr.Cells["未退数量"].Value.ToString(), System.Globalization.NumberStyles.Number);



            }

            //////////////////////////////////////////////////////////
            // 
        }

        private void SetFormState(string state)
        {
            // view edit add save cancle

            if ("View" == state)
            {
                this.formState = "View";

                this.toolStripButton1.Enabled = true;
                this.toolStripButton2.Enabled = true;
                this.toolStripButton3.Enabled = true;
                this.yX_fillCard_MoneyBindingNavigatorSaveItem.Enabled = false;
                this.toolStripButton4.Enabled = false;





                this.toolStripButton44.Enabled = true;
                this.toolStripButton45.Enabled = true;
                this.toolStripButton46.Enabled = true;
                this.toolStripButton47.Enabled = true;
                this.toolStripTextBox10.Enabled = true;



                this.ly_sales_receive_itemDetailDataGridView.ReadOnly = true;






            }
            else
            {
                this.formState = "Edit";

                this.ly_sales_receive_itemDetailDataGridView.ReadOnly = false;

                if (null != ly_sales_receive_itemDetailDataGridView.CurrentRow)
                    this.nowRow = ly_sales_receive_itemDetailDataGridView.CurrentRow.Index;

                this.toolStripButton1.Enabled = false;
                this.toolStripButton2.Enabled = false;
                this.toolStripButton3.Enabled = false;
                this.yX_fillCard_MoneyBindingNavigatorSaveItem.Enabled = true;
                this.toolStripButton4.Enabled = true;





                this.toolStripButton44.Enabled = false;
                this.toolStripButton45.Enabled = false;
                this.toolStripButton46.Enabled = false;
                this.toolStripButton47.Enabled = false;
                this.toolStripTextBox10.Enabled = false;



                this.ly_sales_receive_itemDetailDataGridView.ReadOnly = false;


                this.ly_sales_receive_itemDetailDataGridView.Columns["质检员"].ReadOnly = true;
                this.ly_sales_receive_itemDetailDataGridView.Columns["质检日期"].ReadOnly = true;
                this.ly_sales_receive_itemDetailDataGridView.Columns["质检意见"].ReadOnly = true;

                this.ly_sales_receive_itemDetailDataGridView.Columns["维修接收"].ReadOnly = true;
                this.ly_sales_receive_itemDetailDataGridView.Columns["维修接收日期"].ReadOnly = true;
                this.ly_sales_receive_itemDetailDataGridView.Columns["维修接收人"].ReadOnly = true;

                this.ly_sales_receive_itemDetailDataGridView.Columns["质检"].ReadOnly = true;
                this.ly_sales_receive_itemDetailDataGridView.Columns["故障现象"].ReadOnly = true;
                this.ly_sales_receive_itemDetailDataGridView.Columns["维修日期"].ReadOnly = true;

                //this.ly_sales_receive_itemDetailDataGridView.Columns["出厂编号改制"].ReadOnly = true;
                //this.ly_sales_receive_itemDetailDataGridView.Columns["产品型号改制"].ReadOnly = true;


                this.ly_sales_receive_itemDetailDataGridView.Columns["维修人"].ReadOnly = true;

                this.ly_sales_receive_itemDetailDataGridView.Columns["外修客户编码"].ReadOnly = true;
                this.ly_sales_receive_itemDetailDataGridView.Columns["外修客户名称"].ReadOnly = true;







            }


        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {

            string nowoperpter = this.ly_sales_receiveDataGridView.CurrentRow.Cells["收件人"].Value.ToString();

            if (SQLDatabase.NowUserID != "000")
            {
                if (nowoperpter != SQLDatabase.nowUserName())
                {
                    MessageBox.Show("请收件人:" + nowoperpter + "修改", "注意");
                    return;
                }


                if ("True" == this.ly_sales_receive_itemDetailDataGridView.CurrentRow.Cells["维修接收"].Value.ToString())
                {
                    MessageBox.Show("已经确认接收，不能删除收件项目明细...", "注意");
                    return;

                }
            }
            this.SetFormState("Edit");
        }


        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            if (null == this.ly_sales_receive_itemDetailDataGridView.CurrentRow) return;

            string nowoperpter = this.ly_sales_receiveDataGridView.CurrentRow.Cells["收件人"].Value.ToString();
            if (nowoperpter != SQLDatabase.nowUserName())
            {
                MessageBox.Show("请收件人:" + nowoperpter + "删除", "注意");
                return;
            }

            if ("True" == this.ly_sales_receive_itemDetailDataGridView.CurrentRow.Cells["维修接收"].Value.ToString())
            {
                MessageBox.Show("已经确认接收，不能删除收件项目明细...", "注意");
                return;

            }

            string message1 = "当前记录将被删除，继续吗？";
            string caption1 = "提示...";
            MessageBoxButtons buttons1 = MessageBoxButtons.YesNo;

            DialogResult result1;



            result1 = MessageBox.Show(message1, caption1, buttons1,
            MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

            if (result1 == DialogResult.Yes)
            {
                this.ly_sales_receive_itemDetailBindingSource.RemoveCurrent();

                Saveitemdetail();

            }
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            this.SetFormState("View");

            int nowId = int.Parse(ly_sales_receive_itemsDataGridView.CurrentRow.Cells["id_items"].Value.ToString());

            this.ly_sales_receive_itemDetailTableAdapter.Fill(this.lYSalseMange2.ly_sales_receive_itemDetail, nowId);

        }

        private void yX_fillCard_MoneyBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            Saveitemdetail();

            this.SetFormState("View");
        }
        private string get_dbg_time(string machine_num)
        {
            string sql = " select dbo.f_get_debug_time('" + machine_num + "')";
            DataTable dt = null;
            using (SqlConnection connection = new SqlConnection(SQLDatabase.Connectstring))
            {

                SqlDataAdapter adapter = new SqlDataAdapter(sql, connection);
                DataSet ds = new DataSet();
                adapter.Fill(ds);
                dt = ds.Tables[0];
            }
            if (string.IsNullOrEmpty(dt.Rows[0][0].ToString()))
            {
                return "";
            }
            else
            {
                return dt.Rows[0][0].ToString();
            }
        }
        private void Saveitemdetail()
        {
            //if (ly_sales_receiveDataGridView.CurrentRow == null) return;
            //string receive_time = ly_sales_receiveDataGridView.CurrentRow.Cells["收件日期"].Value.ToString();
            //string warranty = ly_sales_receiveDataGridView.CurrentRow.Cells["warranty1"].Value.ToString();
            //string period_in_scale = ly_sales_receiveDataGridView.CurrentRow.Cells["period_in_scale1"].Value.ToString();
            //string period_out_scale = ly_sales_receiveDataGridView.CurrentRow.Cells["period_out_scale1"].Value.ToString();
            //string period_att_in_scale = ly_sales_receiveDataGridView.CurrentRow.Cells["period_att_in_scale1"].Value.ToString();
            //string period_att_out_scale = ly_sales_receiveDataGridView.CurrentRow.Cells["period_att_out_scale1"].Value.ToString();

            //for (int i = 0; i < ly_sales_receive_itemDetailDataGridView.Rows.Count; i++)
            //{
            //    if (!string.IsNullOrEmpty(ly_sales_receive_itemDetailDataGridView.Rows[i].Cells["制造编号"].Value.ToString()))
            //    {
            //        string machine_num = ly_sales_receive_itemDetailDataGridView.Rows[i].Cells["制造编号"].Value.ToString();
            //        string debug_time = get_dbg_time(machine_num);


            //        if (debug_time != "")
            //        {
            //            ly_sales_receive_itemDetailDataGridView.Rows[i].Cells["debug_date_new"].Value = DateTime.Parse(debug_time);
            //            ly_sales_receive_itemDetailDataGridView.Rows[i].Cells["delivery_new"].Value = (DateTime.Parse(receive_time) - DateTime.Parse(debug_time)).Days;
            //        }
            //        ly_sales_receive_itemDetailDataGridView.Rows[i].Cells["warranty_new"].Value = warranty;
            //        ly_sales_receive_itemDetailDataGridView.Rows[i].Cells["receive_date_new"].Value = DateTime.Parse(receive_time);
            //        ly_sales_receive_itemDetailDataGridView.Rows[i].Cells["period_in_scale"].Value = period_in_scale;
            //        ly_sales_receive_itemDetailDataGridView.Rows[i].Cells["period_out_scale"].Value = period_out_scale;
            //        ly_sales_receive_itemDetailDataGridView.Rows[i].Cells["period_att_in_scale"].Value = period_att_in_scale;
            //        ly_sales_receive_itemDetailDataGridView.Rows[i].Cells["period_att_out_scale"].Value = period_att_out_scale;
            //        //ly_sales_receive_itemDetailDataGridView.EndEdit();
            //    }
            //}



            this.ly_sales_receive_itemDetailBindingSource.EndEdit();
            this.ly_sales_receive_itemDetailTableAdapter.Update(this.lYSalseMange2.ly_sales_receive_itemDetail);

            int nowId = int.Parse(ly_sales_receive_itemsDataGridView.CurrentRow.Cells["id_items"].Value.ToString());

            string nowreceiveCode = ly_sales_receiveDataGridView.CurrentRow.Cells["收件编号"].Value.ToString();


            this.ly_sales_receive_allitemsTableAdapter.Fill(this.lYSalseMange2.ly_sales_receive_allitems, nowreceiveCode);

            this.ly_sales_receive_itemsTableAdapter.Fill(this.lYSalseMange2.ly_sales_receive_items, nowreceiveCode);

            this.ly_sales_receive_itemsBindingSource.Position = this.ly_sales_receive_itemsBindingSource.Find("id", nowId);



        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if (null == ly_sales_receive_itemsDataGridView.CurrentRow) return;

            string nowoperpter = this.ly_sales_receiveDataGridView.CurrentRow.Cells["收件人"].Value.ToString();
            if (nowoperpter != SQLDatabase.nowUserName())
            {
                MessageBox.Show("请收件人:" + nowoperpter + "增加", "注意");
                return;
            }




            int nowId = int.Parse(ly_sales_receive_itemsDataGridView.CurrentRow.Cells["id_items"].Value.ToString());

            this.ly_sales_receive_itemDetailBindingSource.AddNew();



            this.ly_sales_receive_itemDetailDataGridView.CurrentRow.Cells["parent_id"].Value = nowId;
            this.ly_sales_receive_itemDetailDataGridView.CurrentRow.Cells["去向"].Value = "维修";
            this.ly_sales_receive_itemDetailDataGridView.CurrentRow.Cells["维修方式"].Value = "依旧";



            this.SetFormState("Edit");
        }

        private void ly_sales_receive_itemDetailDataGridView_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            DataGridView dgv = sender as DataGridView;
            if (e.Control is DataGridViewTextBoxEditingControl)
            {
                DataGridViewTextBoxEditingControl editingControl = (DataGridViewTextBoxEditingControl)e.Control;
                if (dgv.CurrentCell.OwningColumn.Name == "制造编号")
                {
                    editingControl.CharacterCasing = CharacterCasing.Upper;
                }
                else
                {
                    editingControl.CharacterCasing = CharacterCasing.Normal;
                }
            }


            //if (e.Control is DataGridViewComboBoxEditingControl)
            //{
            //    if (dgv.CurrentCell.OwningColumn.Name == "去向")
            //    {
            //        //ComboBox nowcombobox = e.Control as ComboBox;


            //        //nowcombobox.SelectedIndexChanged -= new EventHandler(ComboBox_SelectedIndexChanged);
            //        //nowcombobox.SelectedIndexChanged += new EventHandler(ComboBox_SelectedIndexChanged);

            //        DataGridViewComboBoxEditingControl nowcombobox1 = e.Control as DataGridViewComboBoxEditingControl;

            //        nowcombobox1.SelectedIndexChanged -= new EventHandler(ComboBox_SelectedIndexChanged);
            //        nowcombobox1.SelectedIndexChanged += new EventHandler(ComboBox_SelectedIndexChanged);



            //    }
            //    else
            //    {
            //        DataGridViewComboBoxEditingControl nowcombobox1 = e.Control as DataGridViewComboBoxEditingControl;

            //        nowcombobox1.SelectedIndexChanged -= new EventHandler(ComboBox_SelectedIndexChanged);
            //        //nowcombobox1.SelectedIndexChanged += new EventHandler(ComboBox_SelectedIndexChanged);
            //    }
            //}  

        }

        private void ComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataGridViewComboBoxEditingControl nowcombobox = sender as DataGridViewComboBoxEditingControl;

            if (null == nowcombobox.Text)
            {
                this.dis_direction = "";
            }
            else
            {
                this.dis_direction = nowcombobox.Text;
            }



            Dealwith_direction(ori_direction, dis_direction);


        }

        private bool Dealwith_direction(string ori_dir, string dis_dir)
        {
            if (ori_dir == dis_dir)
            {
                return true;
            }


            //if ("" == ori_dir)
            //{

            //    return true;
            //    //if ("维修" == dis_dir)
            //    //{
            //    //    //MessageBox.Show( "处理" + dis_dir, "");
            //    //}
            //    //if ("退库" == dis_dir)
            //    //{
            //    //    if (this.now_tuiku > 0)
            //    //    {
            //    //        string sel = "SELECT  out_bill_code as 生产计划,itemno,mch,owe_qty  FROM dbo.f_sales_contract_detail_forreceive('cp000401','YY0000008')  ";
            //    //        QueryForm queryForm = new QueryForm();


            //    //        queryForm.Sel = sel;
            //    //        queryForm.Constr = SQLDatabase.Connectstring;

            //    //        //Set the Column Collection to the filter Table
            //    //        //queryForm.SetSourceColumns(this.billMainDataSet.BalanceBill.Columns);

            //    //        queryForm.ShowDialog();
            //    //        int ii = 0;
            //    //        //this.loanstyletextBox.Text = queryForm.Result;
            //    //    }
            //    //    else
            //    //    {
            //    //        MessageBox.Show("无可退库数据,请重新选择去向", "注意");
            //    //        this.dis_direction = ori_dir;

            //    //    }
            //    //}
            //    //if ("返库" == dis_dir)
            //    //{
            //    //    MessageBox.Show("处理" + dis_dir, "");
            //    //}

            //}
            ///////////////////////////////////////////////////////////////////////////////
            //if ("维修" == ori_dir)
            //{
            if ("维修" == dis_dir || "改制" == dis_dir || "退库" == dis_dir || "外退" == dis_dir)
            {
                int now_oriId = int.Parse(ly_sales_receive_itemDetailDataGridView.CurrentRow.Cells["detail_id"].Value.ToString(), System.Globalization.NumberStyles.Integer);
                Cancle_receiveIns(now_oriId);
            }

            //if ("退库" == dis_dir)
            //{
            //    int now_oriId = int.Parse(ly_sales_receive_itemDetailDataGridView.CurrentRow.Cells["detail_id"].Value.ToString(), System.Globalization.NumberStyles.Integer);
            //    Cancle_receiveIns(now_oriId);    

            //    if (this.now_tuiku > 0)
            //    {

            //        /////////////////////////////////////////////////////////

            //         string nowitemno = ly_sales_receive_itemsDataGridView.CurrentRow.Cells["产品编码"].Value.ToString();
            //        string clientcode = ly_sales_receiveDataGridView.CurrentRow.Cells["客户编码"].Value.ToString();


            //        string sel = "SELECT  out_bill_code as 依赖书号,itemno as 编号,mch as 名称 ,xhc as 型号,cast(owe_qty as int) as 数量 " +
            //                     " FROM dbo.f_sales_contract_detail_forreceive('" + nowitemno + "','" + clientcode + "')  "+
            //                     " where isnull(owe_qty,0)>0";
            //        QueryForm queryForm = new QueryForm();


            //        queryForm.Sel = sel;
            //        queryForm.Constr = SQLDatabase.Connectstring;

            //        //Set the Column Collection to the filter Table
            //        //queryForm.SetSourceColumns(this.billMainDataSet.BalanceBill.Columns);

            //        queryForm.ShowDialog();

            //        if (string .IsNullOrEmpty ( queryForm.Result))
            //        {
            //            MessageBox.Show("退库清单执行后(有依赖书号),才能绑定退库数据...", "注意");
            //            this.dis_direction = ori_dir;
            //            return false;
            //        }

            //        string now_billcode = queryForm.Result;
            //        string now_receivecode = ly_sales_receiveDataGridView.CurrentRow.Cells["收件编号"].Value.ToString();
            //        //int now_oriId =  int.Parse(ly_sales_receive_itemDetailDataGridView.CurrentRow.Cells["detail_id"].Value.ToString(), System.Globalization.NumberStyles.Integer);

            //        Make_receiveIns(now_receivecode, now_billcode,nowitemno, now_oriId);



            //        return true;

            //    }
            //    else
            //    {
            //        MessageBox.Show("无可退库数据,请重新选择去向", "注意");
            //        this.dis_direction = ori_dir;
            //        return false;

            //    }
            //}

            if ("返库" == dis_dir || "外返" == dis_dir)
            {
                // MessageBox.Show("处理" + dis_dir, "");
                int now_oriId = int.Parse(ly_sales_receive_itemDetailDataGridView.CurrentRow.Cells["detail_id"].Value.ToString(), System.Globalization.NumberStyles.Integer);
                Cancle_receiveIns(now_oriId);

                if (this.now_fanku > 0)
                {


                    /////////////////////////////////////////////////////////

                    string nowitemno = ly_sales_receive_itemsDataGridView.CurrentRow.Cells["产品编码"].Value.ToString();
                    string clientcode = ly_sales_receiveDataGridView.CurrentRow.Cells["客户编码"].Value.ToString();


                    string sel = "SELECT  out_bill_code as 依赖书号,itemno as 编号,mch as 名称 ,xhc as 型号,cast(owe_qty as int) as 数量 " +
                                 " FROM dbo.f_sales_borrow_detail_forreceive('" + nowitemno + "','" + clientcode + "')  " +
                                 " where isnull(owe_qty,0)>0";
                    QueryForm queryForm = new QueryForm();


                    queryForm.Sel = sel;
                    queryForm.Constr = SQLDatabase.Connectstring;

                    //Set the Column Collection to the filter Table
                    //queryForm.SetSourceColumns(this.billMainDataSet.BalanceBill.Columns);

                    queryForm.ShowDialog();

                    if (string.IsNullOrEmpty(queryForm.Result))
                    {
                        MessageBox.Show("借用单执行后(有依赖书号),才能绑定返库库数据...", "注意");
                        this.dis_direction = ori_dir;
                        return false;
                    }

                    string now_billcode = queryForm.Result;
                    string now_receivecode = ly_sales_receiveDataGridView.CurrentRow.Cells["收件编号"].Value.ToString();
                    //int now_oriId = int.Parse(ly_sales_receive_itemDetailDataGridView.CurrentRow.Cells["detail_id"].Value.ToString(), System.Globalization.NumberStyles.Integer);

                    Make_receiveIns(now_receivecode, now_billcode, nowitemno, now_oriId);



                    return true;
                }
                else
                {
                    MessageBox.Show("无可返库数据,请重新选择去向", "注意");
                    this.dis_direction = ori_dir;
                    return false;

                }

            }

            ///////////////////////////////////////

            if ("借新还旧" == dis_dir || "借旧还新" == dis_dir)
            {
                // MessageBox.Show("处理" + dis_dir, "");
                int now_oriId = int.Parse(ly_sales_receive_itemDetailDataGridView.CurrentRow.Cells["detail_id"].Value.ToString(), System.Globalization.NumberStyles.Integer);
                Cancle_receiveIns(now_oriId);

                //if (this.now_fanku > 0)
                {


                    /////////////////////////////////////////////////////////

                    string nowitemno = ly_sales_receive_itemsDataGridView.CurrentRow.Cells["产品编码"].Value.ToString();
                    string clientcode = ly_sales_receiveDataGridView.CurrentRow.Cells["客户编码"].Value.ToString();
                    string nowstyle = "no...";

                    string nowstylename = "no...";

                    if ("借新还旧" == dis_dir)
                    {
                        nowstyle = "oldtonew";
                        nowstylename = "借新还旧";
                    }
                    if ("借旧还新" == dis_dir)
                    {
                        nowstyle = "newtoold";
                        nowstylename = "借旧还新";
                    }


                    string sel = "SELECT  out_bill_code as 依赖书号,itemno as 编号,mch as 名称 ,xhc as 型号,cast(owe_qty as int) as 数量 " +
                                 " FROM dbo.f_sales_borrow_detail_forreceiveOldORnew('" + nowitemno + "','" + clientcode + "','" + nowstyle + "')  " +
                                 " where isnull(owe_qty,0)>0";
                    QueryForm queryForm = new QueryForm();


                    queryForm.Sel = sel;
                    queryForm.Constr = SQLDatabase.Connectstring;

                    //Set the Column Collection to the filter Table
                    //queryForm.SetSourceColumns(this.billMainDataSet.BalanceBill.Columns);

                    queryForm.ShowDialog();

                    if (string.IsNullOrEmpty(queryForm.Result))
                    {
                        MessageBox.Show("借用单执行后(有依赖书号),才能绑定返库库数据...", "注意");
                        this.dis_direction = ori_dir;
                        return false;
                    }

                    string now_billcode = queryForm.Result;
                    string now_receivecode = ly_sales_receiveDataGridView.CurrentRow.Cells["收件编号"].Value.ToString();
                    //int now_oriId = int.Parse(ly_sales_receive_itemDetailDataGridView.CurrentRow.Cells["detail_id"].Value.ToString(), System.Globalization.NumberStyles.Integer);

                    Make_receiveInsDiff(now_receivecode, now_billcode, nowitemno, now_oriId, queryForm.Result1, nowstylename);



                    return true;
                }
                //else
                //{
                //    MessageBox.Show("无可返库数据,请重新选择去向", "注意");
                //    this.dis_direction = ori_dir;
                //    return false;

                //}

            }


            ///////////////////////////////////////


            return true;
            //}
            ///////////////////////////////////////////////////

            //if ("退库" == ori_dir)
            //{
            //    if ("维修" == dis_dir)
            //    {
            //       // MessageBox.Show("清理" + ori_dir + "---处理" + dis_dir, "");

            //        int now_oriId = int.Parse(ly_sales_receive_itemDetailDataGridView.CurrentRow.Cells["detail_id"].Value.ToString(), System.Globalization.NumberStyles.Integer);
            //        Cancle_receiveIns(now_oriId);

            //        return true;
            //    }

            //    if ("返库" == dis_dir)
            //    {
            //        MessageBox.Show("清理" + ori_dir + "---处理" + dis_dir, "");
            //        return true;
            //    }
            //    return true;

            //}
            /////////////////////////////////////////////////

            //if ("返库" == ori_dir)
            //{

            //    if ("维修" == dis_dir)
            //    {
            //        MessageBox.Show("清理" + ori_dir + "---处理" + dis_dir, "");
            //        return true;
            //    }

            //    if ("退库" == dis_dir)
            //    {
            //        MessageBox.Show("清理" + ori_dir + "---处理" + dis_dir, "");
            //        return true;
            //    }

            //    return true;

            //}

            // return true;
        }

        private void Recountdata()
        {

            int nowId = int.Parse(ly_sales_receive_itemsDataGridView.CurrentRow.Cells["id_items"].Value.ToString());

            string nowitemno = ly_sales_receive_itemsDataGridView.CurrentRow.Cells["产品编码"].Value.ToString();
            string clientcode = ly_sales_receiveDataGridView.CurrentRow.Cells["客户编码"].Value.ToString();


            //this.ly_sales_receive_itemDetailTableAdapter.Fill(this.lYSalseMange2.ly_sales_receive_itemDetail, nowId);

            this.ly_sales_borrow_detail_forreceiveTableAdapter.Fill(this.lYSalseMange2.ly_sales_borrow_detail_forreceive, clientcode, nowitemno);
            this.ly_sales_contract_detail_forreceiveTableAdapter.Fill(this.lYSalseMange2.ly_sales_contract_detail_forreceive, nowitemno, clientcode);
            this.ly_sales_borrow_detail_forreceiveDiffTableAdapter.Fill(this.lYSalseMange2.ly_sales_borrow_detail_forreceiveDiff, clientcode, nowitemno);

            ///////////////////////////////////////////////////////////////////////////////

            DataGridView dgv = this.ly_sales_borrow_detail_clientDataGridView;
            now_fanku = 0;

            foreach (DataGridViewRow dgr in dgv.Rows)
            {

                if (string.IsNullOrEmpty(dgr.Cells["未还数量jy"].Value.ToString())) continue;
                now_fanku = now_fanku + int.Parse(dgr.Cells["未还数量jy"].Value.ToString(), System.Globalization.NumberStyles.Number);



            }
            //////////////////////////////////////////////////////////

            dgv = this.ly_sales_contract_detail_forreceiveDataGridView;

            now_tuiku = 0;

            foreach (DataGridViewRow dgr in dgv.Rows)
            {

                if (string.IsNullOrEmpty(dgr.Cells["未退数量"].Value.ToString())) continue;
                now_tuiku = now_tuiku + int.Parse(dgr.Cells["未退数量"].Value.ToString(), System.Globalization.NumberStyles.Number);



            }

            //////////////////////////////////////////////////////////
        }
        private void Make_receiveIns(string receive_code, string material_plan_num, string itemno, int ori_id)
        {
            string insstr = " insert ly_sales_receivebind  " +
                         "  (receive_code,material_plan_num,itemno,qty,ori_id)"
                         + " values"
                         + "('" + receive_code + "','" + material_plan_num + "','" + itemno + "',1," + ori_id.ToString() + ")";


            SqlConnection sqlConnection1 = new SqlConnection(SQLDatabase.Connectstring);
            SqlCommand cmd = new SqlCommand();

            cmd.CommandText = insstr;
            cmd.CommandType = CommandType.Text;
            cmd.Connection = sqlConnection1;

            int temp = 0;

            using (TransactionScope scope = new TransactionScope())
            {

                sqlConnection1.Open();
                try
                {

                    cmd.ExecuteNonQuery();



                    scope.Complete();


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

            Recountdata();
        }

        private void Make_receiveInsDiff(string receive_code, string material_plan_num, string itemno, int ori_id, string returned_itemno, string return_style)
        {
            string insstr = " insert ly_sales_receivebind  " +
                         "  (receive_code,material_plan_num,itemno,qty,ori_id,returned_itemno,return_style)"
                         + " values"
                         + "('" + receive_code + "','" + material_plan_num + "','" + itemno + "',1," + ori_id.ToString() + ",'" + returned_itemno + "','" + return_style + "')";


            SqlConnection sqlConnection1 = new SqlConnection(SQLDatabase.Connectstring);
            SqlCommand cmd = new SqlCommand();

            cmd.CommandText = insstr;
            cmd.CommandType = CommandType.Text;
            cmd.Connection = sqlConnection1;

            int temp = 0;

            using (TransactionScope scope = new TransactionScope())
            {

                sqlConnection1.Open();
                try
                {

                    cmd.ExecuteNonQuery();



                    scope.Complete();


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

            Recountdata();
        }
        private void Cancle_receiveIns(int ori_id)
        {
            string delstr = " delete ly_sales_receivebind  "

                         + " where ori_id =" + ori_id.ToString();


            SqlConnection sqlConnection1 = new SqlConnection(SQLDatabase.Connectstring);
            SqlCommand cmd = new SqlCommand();

            cmd.CommandText = delstr;
            cmd.CommandType = CommandType.Text;
            cmd.Connection = sqlConnection1;

            int temp = 0;

            using (TransactionScope scope = new TransactionScope())
            {

                sqlConnection1.Open();
                try
                {

                    cmd.ExecuteNonQuery();



                    scope.Complete();


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
            Recountdata();
        }
        private void ly_sales_receive_itemDetailDataGridView_SelectionChanged(object sender, EventArgs e)
        {
            if (null == ly_sales_receive_itemDetailDataGridView.CurrentRow)
            {
                this.ly_sales_receive_itemDetail_childTableAdapter.Fill(this.lYSalseMange2.ly_sales_receive_itemDetail_child, -9);
                //this.ly_sales_borrow_detail_clientTableAdapter.Fill(this.lYSalseMange.ly_sales_borrow_detail_client, "aaa", "asd");
                return;
            }


            if ("True" == this.ly_sales_receive_itemDetailDataGridView.CurrentRow.Cells["维修接收"].Value.ToString())
            {

                this.SetFormState("View");

            }

            if ("Edit" == this.formState)
            {
                if ("True" == this.ly_sales_receive_itemDetailDataGridView.CurrentRow.Cells["维修接收"].Value.ToString())
                {
                    this.ly_sales_receive_itemDetailDataGridView.Columns["制造编号"].ReadOnly = true;
                    this.ly_sales_receive_itemDetailDataGridView.Columns["去向"].ReadOnly = true;

                }
                else
                {
                    this.ly_sales_receive_itemDetailDataGridView.Columns["制造编号"].ReadOnly = false;
                    this.ly_sales_receive_itemDetailDataGridView.Columns["去向"].ReadOnly = false;

                }
            }

            int nowId = int.Parse(ly_sales_receive_itemDetailDataGridView.CurrentRow.Cells["detail_id"].Value.ToString());

            this.ly_sales_receive_itemDetail_childTableAdapter.Fill(this.lYSalseMange2.ly_sales_receive_itemDetail_child, nowId);


            //string nowitemno = ly_sales_receive_itemsDataGridView.CurrentRow.Cells["产品编码"].Value.ToString();
            //string clientcode = ly_sales_receiveDataGridView.CurrentRow.Cells["客户编码"].Value.ToString();


            //this.ly_sales_receive_itemDetailTableAdapter.Fill(this.lYSalseMange2.ly_sales_receive_itemDetail, nowId);

            //this.ly_sales_borrow_detail_clientTableAdapter.Fill(this.lYSalseMange.ly_sales_borrow_detail_client, clientcode, nowitemno);
        }

        private void ly_sales_receive_itemDetailDataGridView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView dgv = sender as DataGridView;

            if (null == dgv.CurrentCell) return;

            if ("去向" == dgv.CurrentCell.OwningColumn.Name)
            {
                this.dis_direction = dgv.CurrentCell.Value.ToString();

                if (Dealwith_direction(ori_direction, dis_direction))
                {

                    this.ori_direction = this.dis_direction;
                    Saveitemdetail();
                }
                else
                {
                    ly_sales_receive_itemDetailDataGridView.CellValueChanged -= ly_sales_receive_itemDetailDataGridView_CellValueChanged;

                    dgv.CurrentCell.Value = "维修";
                    dgv.RefreshEdit();
                    Saveitemdetail();
                    ly_sales_receive_itemDetailDataGridView.CellValueChanged += ly_sales_receive_itemDetailDataGridView_CellValueChanged;

                }


            }


        }

        private void ly_sales_receive_itemDetailDataGridView_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (ly_sales_receive_itemDetailDataGridView.IsCurrentCellDirty)
            {
                ly_sales_receive_itemDetailDataGridView.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }

        }

        private void ly_sales_receive_itemDetailDataGridView_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView dgv = sender as DataGridView;

            if ("去向" == dgv.CurrentCell.OwningColumn.Name)
            {
                this.ori_direction = dgv.CurrentCell.Value.ToString();

            }

        }

        private void 增加备件ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (null == ly_sales_receive_itemDetailDataGridView.CurrentRow)
            {
                //MessageBox.Show("依赖书未创建,不能增加数据...", "注意");
                return;
            }

            string nowoperpter = this.ly_sales_receiveDataGridView.CurrentRow.Cells["收件人"].Value.ToString();
            if (nowoperpter != SQLDatabase.nowUserName())
            {
                MessageBox.Show("请收件人:" + nowoperpter + "增加", "注意");
                return;
            }

            if ("True" == this.ly_sales_receive_itemDetailDataGridView.CurrentRow.Cells["维修接收"].Value.ToString())
            {
                MessageBox.Show("已经维修接收，不能修改数据", "注意");
                return;
            }

            //if (null == ly_sales_groupDataGridView.CurrentRow)
            //{
            //    MessageBox.Show("配套未设置,不能增加数据...", "注意");
            //    return;
            //}

            //if ("True" == ly_material_plan_mainDataGridView.CurrentRow.Cells["配套完成0"].Value.ToString())
            //{
            //    MessageBox.Show("依赖书已经技术配套,不能增加数据...", "注意");
            //    return;

            //}

            LY_SalesProduct_Sel queryForm = new LY_SalesProduct_Sel();

            //queryForm.salesclientcode = nowclientcode;
            //queryForm.runmode = "增加";


            queryForm.StartPosition = FormStartPosition.CenterParent;
            queryForm.ShowDialog();

            if (queryForm.DialogResult != DialogResult.Cancel)
            {
                //if (null == ly_sales_contract_detailDataGridView.CurrentRow) return;





                string nowitemno = queryForm.nowitemno;
                // string nowitemname = queryForm.nowitemname;

                string nowunit = queryForm.nowunit;

                int nowId = int.Parse(ly_sales_receive_itemDetailDataGridView.CurrentRow.Cells["detail_id"].Value.ToString());

                // this.ly_sales_receive_itemDetail_childTableAdapter.Fill(this.lYSalseMange2.ly_sales_receive_itemDetail_child, nowId);



                int hadarranged = ly_sales_receive_itemDetail_childBindingSource.Find("备件编号", nowitemno);
                if (-1 < hadarranged)
                {
                    ly_sales_receive_itemDetail_childBindingSource.Position = hadarranged;
                    MessageBox.Show("当前备件已有选择,修改数量即可...", "注意");
                    return;

                }

                //decimal nowabsqty;
                //if ("" != this.ly_sales_contract_detailDataGridView.CurrentRow.Cells["未配套"].Value.ToString())
                //{
                //    nowabsqty = decimal.Parse(ly_sales_contract_detailDataGridView.CurrentRow.Cells["未配套"].Value.ToString());
                //}
                //else
                //{
                //    nowabsqty = 0;
                //}

                //if (nowabsqty == 0)
                //{

                //    MessageBox.Show("产品全部配出,操作取消...", "注意");
                //    return;
                //}





                this.ly_sales_receive_itemDetail_childBindingSource.AddNew();


                this.ly_sales_receive_itemDetail_childDataGridView.CurrentRow.Cells["备件编号"].Value = nowitemno;
                this.ly_sales_receive_itemDetail_childDataGridView.CurrentRow.Cells["parent_id3"].Value = nowId;




                this.ly_sales_receive_itemDetail_childBindingSource.EndEdit();

                this.ly_sales_receive_itemDetail_childTableAdapter.Update(this.lYSalseMange2.ly_sales_receive_itemDetail_child);



                //this.ly_sales_contract_detailTableAdapter.Fill(this.lYSalseMange.ly_sales_contract_detail, nowinnerCode, 0);

                this.ly_sales_receive_itemDetail_childTableAdapter.Fill(this.lYSalseMange2.ly_sales_receive_itemDetail_child, nowId);

                this.ly_sales_receive_itemDetail_childBindingSource.Position = this.ly_sales_receive_itemDetail_childBindingSource.Find("备件编号", nowitemno);



            }
        }

        private void 删除备件ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (null == ly_sales_receive_itemDetail_childDataGridView.CurrentRow)
            {

                return;
            }

            string nowoperpter = this.ly_sales_receiveDataGridView.CurrentRow.Cells["收件人"].Value.ToString();
            if (nowoperpter != SQLDatabase.nowUserName())
            {
                MessageBox.Show("请收件人:" + nowoperpter + "删除", "注意");
                return;
            }



            if ("True" == this.ly_sales_receive_itemDetailDataGridView.CurrentRow.Cells["维修接收"].Value.ToString())
            {
                MessageBox.Show("已经维修接收，不能修改数据", "注意");
                return;
            }

            //if ("True" == ly_material_plan_mainDataGridView.CurrentRow.Cells["配套完成0"].Value.ToString())
            //{
            //    MessageBox.Show("依赖书已经技术配套,不能删除数据...", "注意");
            //    return;

            //}

            //string salespeople = this.ly_plan_getmaterialDataGridView.CurrentRow.Cells["录入人5"].Value.ToString();

            //if (salespeople != SQLDatabase.nowUserName())
            //{
            //    MessageBox.Show("请录入人:" + salespeople + "删除", "注意");
            //    return;
            //}

            //if ("True" == ly_sales_contract_mainDataGridView.CurrentRow.Cells["提交"].Value.ToString())
            //{
            //    MessageBox.Show("合同已经提交,不能删除数据...", "注意");
            //    return;

            //}

            //if ("True" == ly_sales_contract_mainDataGridView.CurrentRow.Cells["批准"].Value.ToString())
            //{
            //    MessageBox.Show("合同已经执行,不能删除数据...", "注意");
            //    return;

            //}

            //if ("True" == ly_sales_contract_mainDataGridView.CurrentRow.Cells["审核"].Value.ToString())
            //{
            //    MessageBox.Show("合同已经审批,不能删除数据...", "注意");
            //    return;

            //}


            string message = "确定删除当前备件吗？";
            string caption = "提示...";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result;



            result = MessageBox.Show(message, caption, buttons,
            MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

            if (result == DialogResult.Yes)
            {




                this.ly_sales_receive_itemDetail_childBindingSource.RemoveCurrent();


                this.ly_sales_receive_itemDetail_childBindingSource.EndEdit();

                this.ly_sales_receive_itemDetail_childTableAdapter.Update(this.lYSalseMange2.ly_sales_receive_itemDetail_child);


                //this.ly_sales_contract_detailTableAdapter.Fill(this.lYSalseMange.ly_sales_contract_detail, nowinnerCode, 0);

                //if (null != ly_plan_getmaterialDataGridView.CurrentRow)
                //{

                //    string nowitemno = ly_plan_getmaterialDataGridView.CurrentRow.Cells["产品编号5"].Value.ToString();
                //    this.ly_sales_contract_detailBindingSource.Position = this.ly_sales_contract_detailBindingSource.Find("产品编码", nowitemno);
                //}

                //string nowplannum = ly_material_plan_mainDataGridView.CurrentRow.Cells["计划编号0"].Value.ToString();
                //MakeGroupTreeView(nowplannum);


                //this.ly_sales_contract_main1DataGridView.SelectionChanged -= ly_sales_contract_main1DataGridView_SelectionChanged;
                //this.ly_sales_contract_main1TableAdapter.Fill(this.lYSalseMange.ly_sales_contract_main1, this.nowusercode, "single", this.dateTimePicker1.Value, this.dateTimePicker2.Value);
                //this.ly_sales_contract_main1DataGridView.SelectionChanged += ly_sales_contract_main1DataGridView_SelectionChanged;
            }
        }

        private void ly_sales_receive_itemDetail_childDataGridView_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            DataGridView dgv = sender as DataGridView;

            if (null == dgv.CurrentRow) return;


            //if ("True" == ly_material_plan_mainDataGridView.CurrentRow.Cells["配套完成0"].Value.ToString())
            //{
            //    MessageBox.Show("依赖书已经技术配套,不能修改数据...", "注意");
            //    return;

            //}









            //if ("True" == dgv.CurrentRow.Cells["提交"].Value.ToString())
            //{
            //    MessageBox.Show("合同已经提交,不能修改数据...", "注意");
            //    return;

            //}


            //if ("True" == dgv.CurrentRow.Cells["审核"].Value.ToString())
            //{
            //    MessageBox.Show("合同已经审批,不能修改数据...", "注意");
            //    return;

            //}

            ///////////////////////////////////////////////////////////////
            if ("数量3" == dgv.CurrentCell.OwningColumn.Name)
            {

                //if ("True" == dgv.CurrentRow.Cells["审批"].Value.ToString())
                //{

                //    MessageBox.Show("合同已经审批,不能修改数据...", "注意");
                //    return;
                //}

                ChangeValue queryForm = new ChangeValue();

                queryForm.OldValue = dgv.CurrentCell.Value.ToString();
                queryForm.NewValue = "";
                queryForm.ChangeMode = "value";
                queryForm.ShowDialog();




                if (queryForm.NewValue != "")
                {



                    dgv.CurrentRow.Cells["数量3"].Value = queryForm.NewValue;
                    //dgv.CurrentRow.Cells["discount_money"].Value = DBNull.Value;

                    //dgv.CurrentRow.Cells["approve_flag"].Value = "False";

                    this.ly_sales_receive_itemDetail_childBindingSource.EndEdit();

                    this.ly_sales_receive_itemDetail_childTableAdapter.Update(this.lYSalseMange2.ly_sales_receive_itemDetail_child);





                    //CountPlanStru();

                }
                else
                {
                    //hT_Manage_ItemDataGridView.CurrentRow.Cells["apply_money"].Value = queryForm.NewValue;
                    //dgv.CurrentRow.Cells["discount_money"].Value = DBNull.Value;
                    //dgv.CurrentRow.Cells["apply_money"].Value = DBNull.Value;
                    //dgv.CurrentRow.Cells["approve_flag"].Value = "False";
                    //SaveChanged();

                }
                return;

            }



            ///////////////////////////////////////////////////////////////

            if ("备注3" == dgv.CurrentCell.OwningColumn.Name)
            {

                ChangeValue queryForm = new ChangeValue();

                queryForm.OldValue = dgv.CurrentCell.Value.ToString();
                queryForm.NewValue = "";
                queryForm.ChangeMode = "longstring";
                queryForm.ShowDialog();




                if (queryForm.NewValue != "")
                {
                    dgv.CurrentRow.Cells["备注3"].Value = queryForm.NewValue;
                    //dgv.CurrentRow.Cells["discount_money"].Value = DBNull.Value;

                    //dgv.CurrentRow.Cells["approve_flag"].Value = "False";
                    this.ly_sales_receive_itemDetail_childBindingSource.EndEdit();

                    this.ly_sales_receive_itemDetail_childTableAdapter.Update(this.lYSalseMange2.ly_sales_receive_itemDetail_child);


                    //this.ly_sales_contract_detailTableAdapter.Fill(this.lYSalseMange.ly_sales_contract_detail, nowinnerCode, 0);

                    //CountPlanStru();

                }
                else
                {

                }
                return;

            }
        }

        private void bindingNavigatorDeleteItem4_Click(object sender, EventArgs e)
        {
            if (null == ly_sales_receive_itemsDataGridView.CurrentRow)
            {


                return;
            }

            string nowoperpter = this.ly_sales_receiveDataGridView.CurrentRow.Cells["收件人"].Value.ToString();
            if (nowoperpter != SQLDatabase.nowUserName())
            {
                MessageBox.Show("请收件人:" + nowoperpter + "删除", "注意");
                return;
            }
            if (0 < ly_sales_receive_itemDetailDataGridView.RowCount)
            {
                MessageBox.Show("先删除项目明细，才能删除收件项目...", "注意");
                return;

            }
            //}

            //if ("True" == ly_sales_contract_mainDataGridView.CurrentRow.Cells["批准"].Value.ToString())
            //{
            //    MessageBox.Show("合同已经执行,不能删除数据...", "注意");
            //    return;

            //}

            //if ("True" == ly_sales_contract_mainDataGridView.CurrentRow.Cells["审核"].Value.ToString())
            //{
            //    MessageBox.Show("合同已经审批,不能删除数据...", "注意");
            //    return;

            //}


            string message = "确定删除当前收件项目吗？";
            string caption = "提示...";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result;



            result = MessageBox.Show(message, caption, buttons,
            MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

            if (result == DialogResult.Yes)
            {

                this.ly_sales_receive_itemsBindingSource.RemoveCurrent();


                ly_sales_receive_itemsDataGridView.EndEdit();
                ly_sales_receive_itemsBindingSource.EndEdit();



                ly_sales_receive_itemsTableAdapter.Update(this.lYSalseMange2.ly_sales_receive_items);

                //this.ly_sales_contract_main1DataGridView.SelectionChanged -= ly_sales_contract_main1DataGridView_SelectionChanged;
                //this.ly_sales_contract_main1TableAdapter.Fill(this.lYSalseMange.ly_sales_contract_main1, this.nowusercode, "single", this.dateTimePicker1.Value, this.dateTimePicker2.Value);
                //this.ly_sales_contract_main1DataGridView.SelectionChanged += ly_sales_contract_main1DataGridView_SelectionChanged;
            }
        }

        private void toolStripButton27_Click_1(object sender, EventArgs e)
        {
            if (null == this.ly_sales_receive_itemsDataGridView.CurrentRow) return;

            //if ("True" != this.ly_sales_contract_mainDataGridView.CurrentRow.Cells["批准"].Value.ToString())
            //{

            //    MessageBox.Show("请先确定 执行,然后打印...", "注意");
            //    return;
            //}

            NewFrm.Show(this); ;

            //this.ly_plan_getmaterialTableAdapter.Fill(this.lYPlanMange.ly_plan_getmaterial, parentId);

            BaseReportView queryForm = new BaseReportView();

            queryForm.Text = "中原精密维修收件交接单";

            queryForm.setchackBoxCansee(false);

            queryForm.Printdata = this.lYSalseMange2;
            //queryForm.company = ly_sales_contract_mainDataGridView.CurrentRow.Cells["公司"].Value.ToString();



            queryForm.PrintCrystalReport = new LY_sales_repair_jiaojie();






            NewFrm.Hide(this);

            queryForm.ShowDialog();
        }

        private void ly_sales_receive_itemDetailDataGridView_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
            //this.ly_sales_receive_itemDetailBindingSource.EndEdit();
            //this.ly_sales_receive_itemDetailTableAdapter.Update(this.lYSalseMange2.ly_sales_receive_itemDetail);



        }

        private void ly_sales_receive_itemDetailDataGridView_Leave(object sender, EventArgs e)
        {
            this.ly_sales_receive_itemDetailBindingSource.EndEdit();
            this.ly_sales_receive_itemDetailTableAdapter.Update(this.lYSalseMange2.ly_sales_receive_itemDetail);
        }

        private void ly_sales_receive_itemDetailDataGridView_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if ("View" != this.formState) return;

            DataGridView dgv = sender as DataGridView;

            if (null == dgv.CurrentRow) return;


            if ("True" == dgv.CurrentRow.Cells["质检"].Value.ToString())
            {
                MessageBox.Show("已经质检,不能修改数据...", "注意");
                return;
            }

            // /////////////////////////////////////////////////////////////////

            //if ("外修" == dgv.CurrentRow.Cells["去向"].Value.ToString() && ("外修客户编码" == dgv.CurrentCell.OwningColumn.Name || "外修客户名称" == dgv.CurrentCell.OwningColumn.Name))
            //{

            //    string sel;



            //    sel = "SELECT  repairclient_code as 编码, repairclient_name as 名称 FROM ly_sales_client_Repair  ";


            //    QueryForm queryForm = new QueryForm();


            //    queryForm.Sel = sel;
            //    queryForm.Constr = SQLDatabase.Connectstring;

            //    //Set the Column Collection to the filter Table
            //    //queryForm.SetSourceColumns(this.billMainDataSet.BalanceBill.Columns);

            //    queryForm.ShowDialog();


            //    if (!string.IsNullOrEmpty(queryForm.Result))
            //    {
            //        dgv.CurrentRow.Cells["外修客户编码"].Value = queryForm.Result;
            //        dgv.CurrentRow.Cells["外修客户名称"].Value = queryForm.Result1;
            //        //dgv.CurrentRow.Cells["维修接收日期"].Value = SQLDatabase.GetNowdate();



            //    }
            //    else
            //    {

            //        dgv.CurrentRow.Cells["外修客户编码"].Value = DBNull.Value;
            //        dgv.CurrentRow.Cells["外修客户名称"].Value = DBNull.Value;
            //        //dgv.CurrentRow.Cells["维修接收日期"].Value = DBNull.Value;

            //    }

            //    this.ly_sales_receive_itemDetailBindingSource.EndEdit();
            //    this.ly_sales_receive_itemDetailTableAdapter.Update(this.lYSalseMange2.ly_sales_receive_itemDetail);

            //    return;

            //}

            //    /////////////////////////////////////////////////////////////////////////////////

            //if ("维修接收" == dgv.CurrentCell.OwningColumn.Name)
            //{
            //    if ("True" == dgv.CurrentRow.Cells["维修接收"].Value.ToString())
            //    {
            //       dgv.CurrentRow.Cells["维修接收"].Value = "False";
            //    }
            //    else
            //    {
            //        dgv.CurrentRow.Cells["维修接收"].Value = "True";
            //    }

            //}

            ///////////////////////////////////////////////////////////////
            if ("出厂编号改制" == dgv.CurrentCell.OwningColumn.Name)
            {

                //if ("True" == dgv.CurrentRow.Cells["审批"].Value.ToString())
                //{

                //    MessageBox.Show("合同已经审批,不能修改数据...", "注意");
                //    return;
                //}

                ChangeValue queryForm = new ChangeValue();

                queryForm.OldValue = dgv.CurrentCell.Value.ToString();
                queryForm.NewValue = "";
                queryForm.ChangeMode = "string";
                queryForm.ShowDialog();




                if (queryForm.NewValue != "")
                {



                    dgv.CurrentRow.Cells["出厂编号改制"].Value = queryForm.NewValue;


                    //dgv.CurrentRow.Cells["discount_money"].Value = DBNull.Value;

                    //dgv.CurrentRow.Cells["approve_flag"].Value = "False";ly_plan_getmaterialDataGridView

                    this.ly_sales_receive_itemDetailBindingSource.EndEdit();
                    this.ly_sales_receive_itemDetailTableAdapter.Update(this.lYSalseMange2.ly_sales_receive_itemDetail);





                }

                return;

            }

            ///////////////////////////////////////////////////////////////
            if ("产品型号改制" == dgv.CurrentCell.OwningColumn.Name)
            {

                LY_SalesProduct_Sel queryForm = new LY_SalesProduct_Sel();

                //queryForm.salesclientcode = nowclientcode;
                //queryForm.runmode = "增加";


                queryForm.StartPosition = FormStartPosition.CenterParent;
                queryForm.ShowDialog();

                if (queryForm.DialogResult != DialogResult.Cancel)
                {

                    string nowitemno = queryForm.nowitemno;

                    dgv.CurrentRow.Cells["产品型号改制"].Value = queryForm.nowitemxhc;
                    dgv.CurrentRow.Cells["产品编码改制"].Value = queryForm.nowitemno;


                    // 




                    //dgv.CurrentRow.Cells["discount_money"].Value = DBNull.Value;

                    //dgv.CurrentRow.Cells["approve_flag"].Value = "False";ly_plan_getmaterialDataGridView

                    this.ly_sales_receive_itemDetailBindingSource.EndEdit();
                    this.ly_sales_receive_itemDetailTableAdapter.Update(this.lYSalseMange2.ly_sales_receive_itemDetail);

                }
                else
                {

                    dgv.CurrentRow.Cells["产品型号改制"].Value = DBNull.Value;
                    dgv.CurrentRow.Cells["产品编码改制"].Value = DBNull.Value;

                    //dgv.CurrentRow.Cells["discount_money"].Value = DBNull.Value;

                    //dgv.CurrentRow.Cells["approve_flag"].Value = "False";ly_plan_getmaterialDataGridView

                    this.ly_sales_receive_itemDetailBindingSource.EndEdit();
                    this.ly_sales_receive_itemDetailTableAdapter.Update(this.lYSalseMange2.ly_sales_receive_itemDetail);
                }



            }
            return;
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            if (null == ly_sales_receiveDataGridView.CurrentRow)
            {
                this.ly_sales_receive_itemsTableAdapter.Fill(this.lYSalseMange2.ly_sales_receive_items, "asd");
                return;
            }


            if (null == this.ly_sales_receive_itemsDataGridView.CurrentRow) return;



            string nowreceiveCode = ly_sales_receiveDataGridView.CurrentRow.Cells["收件编号"].Value.ToString();


            //this.ly_plan_getmaterialTableAdapter.Fill(this.lYPlanMange.ly_plan_getmaterial, parentId);

            this.f_sales_receive_MainTableAdapter.Fill(this.lYSalseMange2.f_sales_receive_Main, nowreceiveCode);

            BaseReportView queryForm = new BaseReportView();

            queryForm.Text = "中原精密维修收件交接单";

            queryForm.setchackBoxCansee(false);

            queryForm.Printdata = this.lYSalseMange2;


            queryForm.PrintCrystalReport = new LY_sales_repair_jjMain();


            queryForm.ShowDialog();
        }

        private void toolStripButton9_Click(object sender, EventArgs e)
        {
            LY_Sales_RepairClientSet queryForm = new LY_Sales_RepairClientSet();


            queryForm.StartPosition = FormStartPosition.CenterParent;
            queryForm.ShowDialog();
        }

        private string GetMaxSupplementcode()
        {
            SqlConnection sqlConnection1 = new SqlConnection(SQLDatabase.Connectstring);
            SqlCommand cmd = new SqlCommand();

            string MaxProductionorder = "";

            cmd.Parameters.Add("@Production_mode", SqlDbType.VarChar);
            cmd.Parameters["@Production_mode"].Value = "BM";


            cmd.CommandText = "LY_GetMax_SupplementMachinecode";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = sqlConnection1;

            sqlConnection1.Open();
            MaxProductionorder = cmd.ExecuteScalar().ToString();
            sqlConnection1.Close();



            return MaxProductionorder;
        }
        private void 增补制造编号ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //string nowoperpter = this.ly_sales_receiveDataGridView.CurrentRow.Cells["收件人"].Value.ToString();
            //if (nowoperpter != SQLDatabase.nowUserName())
            //{
            //    MessageBox.Show("请收件人:" + nowoperpter + "删除", "注意");
            //    return;
            //}
            //if (ly_sales_receiveDataGridView.CurrentRow == null) return;
            if ("True" == this.ly_sales_receive_itemDetailDataGridView.CurrentRow.Cells["维修接收"].Value.ToString())
            {
                MessageBox.Show("已经维修接收，不能修改数据", "注意");
                return;
            }

            this.ly_sales_receive_itemDetailDataGridView.CurrentRow.Cells["制造编号"].Value = DBNull.Value;
            this.ly_sales_receive_itemDetailBindingSource.EndEdit();
            this.ly_sales_receive_itemDetailTableAdapter.Update(this.lYSalseMange2.ly_sales_receive_itemDetail);

            this.ly_sales_receive_itemDetailDataGridView.CurrentRow.Cells["制造编号"].Value = GetMaxSupplementcode();

 


            this.ly_sales_receive_itemDetailBindingSource.EndEdit();
            this.ly_sales_receive_itemDetailTableAdapter.Update(this.lYSalseMange2.ly_sales_receive_itemDetail);

        }



    }
}
