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
    public partial class LY_Receive_Repair_OUT : Form
    {
        private string nowfilterStr = "";
        private string nowusercode = "";

        private string formState;
        private int nowRow;

        

        private string nowfillstragecode = "";





        public LY_Receive_Repair_OUT()
        {
            InitializeComponent();
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
            this.ly_inma0010ylTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            this.ly_inma0010ylTableAdapter.Fill(this.lYMaterialMange.ly_inma0010yl);
            this.t_usersTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            this.t_usersTableAdapter.Fill(this.lYSalseMange2.T_users);

            this.dateTimePicker1.Text = DateTime.Today.AddMonths(-1).Date.ToString();
            this.dateTimePicker2.Text = DateTime.Today.AddDays(1).Date.ToString();

            this.SetFormState("View");


          
            
            this.ly_sales_receive_itemDetail_repairTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            this.ly_sales_receive_itemDetailTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            this.ly_sales_receive_itemsTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
           


            this.ly_sales_receiveTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            //this.ly_sales_receiveTableAdapter.Fill(this.lYSalseMange2.ly_sales_receive, this.dateTimePicker1.Value, this.dateTimePicker2.Value);

          
           

            //this.ly_material_plan_mainTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
           // this.ly_material_plan_mainTableAdapter.Fill(this.lYSalseMange.ly_material_plan_main, jhlbToolStripTextBox.Text);

            this.ly_sales_receiveBindingSource.Filter = "外修数量  >0";
            this.ly_sales_receive_itemsBindingSource.Filter = "外修数量  >0";
            this.ly_sales_receive_itemDetailBindingSource.Filter = "去向  = '外修'or 去向  = '外返' or 去向  = '外退'";

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
            string last_salesregion_code="___";

          

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



            this.ly_sales_receiveTableAdapter.Fill(this.lYSalseMange2.ly_sales_receive, this.nowusercode, this.nowfillstragecode, this.dateTimePicker1.Value, this.dateTimePicker2.Value);
           
           // AddSummationRow_New(ly_sales_contract_main1BindingSource, ly_sales_contract_main1DataGridView);
            //this.ly_sales_contract_main1DataGridView.SelectionChanged += ly_sales_contract_main1DataGridView_SelectionChanged;

            this.groupBox1.Text = e.Node.Text + "收件信息列表";
        }

      

        private TreeNode FindNode( TreeNodeCollection tnParent, string strValue)
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

            this.ly_sales_receiveTableAdapter.Fill(this.lYSalseMange2.ly_sales_receive, this.nowusercode, this.nowfillstragecode, this.dateTimePicker1.Value, this.dateTimePicker2.Value);
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
                            if ("合同天数" != dgvCell.OwningColumn.HeaderText && "到期天数" != dgvCell.OwningColumn.HeaderText && "折扣利率" != dgvCell.OwningColumn.HeaderText )
                            {
                                if (sumdr[dgvCell.OwningColumn.DataPropertyName] == null || (sumdr[dgvCell.OwningColumn.DataPropertyName] is DBNull))
                                    sumdr[dgvCell.OwningColumn.DataPropertyName] = 0;


                                sumdr[dgvCell.OwningColumn.DataPropertyName] = Convert.ToInt64(sumdr[dgvCell.OwningColumn.DataPropertyName]) + Convert.ToInt64(dgvCell.Value);
                            }
                        }
                        else if (IsDecimal(dgvCell.Value))
                        {
                            if ("合同天数" != dgvCell.OwningColumn.HeaderText && "到期天数" != dgvCell.OwningColumn.HeaderText && "折扣利率" != dgvCell.OwningColumn.HeaderText )
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

            if (!string.IsNullOrEmpty(filterString))
            {
                this.ly_sales_receiveBindingSource.Filter = "外修数量  >0" + " and (" + filterString + ")";
            }
        }

        private void toolStripTextBox5_Enter(object sender, EventArgs e)
        {
            toolStripTextBox5.Text = "";

            //this.ly_sales_receiveBindingSource.Filter = "";
            this.ly_sales_receiveBindingSource.Filter = "外修数量  >0";
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



                if (tn.Tag .ToString () == strValue)
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

            //if ("True" == ly_sales_businessDataGridView.CurrentRow.Cells["提交1"].Value.ToString())
            //{
            //    MessageBox.Show("当前业务已经提交为合同,不能删除数据...", "注意");
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


            string message = "确定删除当前收件记录吗？";
            string caption = "提示...";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result;



            result = MessageBox.Show(message, caption, buttons,
            MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

            if (result == DialogResult.Yes)
            {

                this.ly_sales_receiveBindingSource.RemoveCurrent();


                ly_sales_receiveDataGridView.EndEdit();
                ly_sales_receiveBindingSource.EndEdit();



                this.ly_sales_receiveTableAdapter.Update(this.lYSalseMange2.ly_sales_receive);

                //this.ly_sales_contract_main1DataGridView.SelectionChanged -= ly_sales_contract_main1DataGridView_SelectionChanged;
                //this.ly_sales_contract_main1TableAdapter.Fill(this.lYSalseMange.ly_sales_contract_main1, this.nowusercode, "single", this.dateTimePicker1.Value, this.dateTimePicker2.Value);
                //this.ly_sales_contract_main1DataGridView.SelectionChanged += ly_sales_contract_main1DataGridView_SelectionChanged;
            }
        }

    


        //private void toolStripButton18_Click(object sender, EventArgs e)
        //{
        //    ExportDataGridviewTOExcell.ExportDataGridview(this.ly_sales_contract_mainDataGridView, true);
        //}

      

       

        
       

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
                this.ly_sales_receiveTableAdapter.Fill(this.lYSalseMange2.ly_sales_receive, this.nowusercode, this.nowfillstragecode, this.dateTimePicker1.Value, this.dateTimePicker2.Value);

                this.ly_sales_receiveBindingSource.Position = this.ly_sales_receiveBindingSource.Find("收件编号", queryForm.receive_code);


            }
        }

        private void toolStripButton7_Click(object sender, EventArgs e)
        {
            if (null == ly_sales_receiveDataGridView.CurrentRow) return;

            string nowrecordCode = ly_sales_receiveDataGridView.CurrentRow.Cells["收件编号"].Value.ToString();
          
            
            LY_Salesreceive_Add queryForm = new LY_Salesreceive_Add();

            queryForm.salesclientcode = "111";
            queryForm.receive_code = nowrecordCode;
            queryForm.runmode = "修改";


            queryForm.StartPosition = FormStartPosition.CenterParent;
            queryForm.ShowDialog();

            if (queryForm.DialogResult != DialogResult.Cancel)
            {
                this.ly_sales_receiveTableAdapter.Fill(this.lYSalseMange2.ly_sales_receive, this.nowusercode, this.nowfillstragecode, this.dateTimePicker1.Value, this.dateTimePicker2.Value);

                this.ly_sales_receiveBindingSource.Position = this.ly_sales_receiveBindingSource.Find("收件编号", queryForm.receive_code);


            }
        }

        private void ly_sales_receiveDataGridView_SelectionChanged(object sender, EventArgs e)
        {
            if (null == ly_sales_receiveDataGridView.CurrentRow)
            {
                this.ly_sales_receive_itemsTableAdapter.Fill(this.lYSalseMange2.ly_sales_receive_items,"asd");
                return;
            }


            string nowreceiveCode = ly_sales_receiveDataGridView.CurrentRow.Cells["收件编号"].Value.ToString();
            this.ly_sales_receive_itemsTableAdapter.Fill(this.lYSalseMange2.ly_sales_receive_items, nowreceiveCode);

            if (null == ly_sales_receive_itemsDataGridView.CurrentRow)
            {
                this.ly_sales_receive_itemDetailTableAdapter.Fill(this.lYSalseMange2.ly_sales_receive_itemDetail,-1);
            }
        }

        private void ly_sales_receive_itemsDataGridView_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            return;
            
            DataGridView dgv = sender as DataGridView;

            if (null == dgv.CurrentRow) return;


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
                return;
            }


            int nowId = int.Parse ( ly_sales_receive_itemsDataGridView.CurrentRow.Cells["id_items"].Value.ToString());

            string nowitemno = ly_sales_receive_itemsDataGridView.CurrentRow.Cells["产品编码"].Value.ToString();
            string clientcode = ly_sales_receiveDataGridView.CurrentRow.Cells["客户编码"].Value.ToString();


            this.ly_sales_receive_itemDetailTableAdapter.Fill(this.lYSalseMange2.ly_sales_receive_itemDetail, nowId);

            
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
             



            }


        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            this.SetFormState("Edit");
        }

       
        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            if (null == this.ly_sales_receive_itemDetailDataGridView.CurrentRow) return;

           

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

        private void Saveitemdetail()
        {
            this.ly_sales_receive_itemDetailBindingSource.EndEdit();
            this.ly_sales_receive_itemDetailTableAdapter.Update(this.lYSalseMange2.ly_sales_receive_itemDetail);

            int nowId = int.Parse(ly_sales_receive_itemsDataGridView.CurrentRow.Cells["id_items"].Value.ToString());

            string nowreceiveCode = ly_sales_receiveDataGridView.CurrentRow.Cells["收件编号"].Value.ToString();

         


            this.ly_sales_receive_itemsTableAdapter.Fill(this.lYSalseMange2.ly_sales_receive_items, nowreceiveCode);

            this.ly_sales_receive_itemsBindingSource.Position = this.ly_sales_receive_itemsBindingSource.Find("id", nowId);

         

        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if (null == ly_sales_receive_itemsDataGridView.CurrentRow) return;

            int nowId = int.Parse(ly_sales_receive_itemsDataGridView.CurrentRow.Cells["id_items"].Value.ToString());
            this.ly_sales_receive_itemDetailBindingSource.AddNew();

           

            this.ly_sales_receive_itemDetailDataGridView.CurrentRow.Cells["parent_id"].Value = nowId;

         
           
            
            this.SetFormState("Edit");
        }

        private void SaveDetail()
        {


            this.ly_sales_receive_itemDetailDataGridView.EndEdit();
            this.ly_sales_receive_itemDetailBindingSource.EndEdit();

            this.ly_sales_receive_itemDetailTableAdapter.Update(this.lYSalseMange2.ly_sales_receive_itemDetail);



          
        }


        private void ly_sales_receive_itemDetailDataGridView_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            DataGridView dgv = sender as DataGridView;

            if (null == dgv.CurrentRow) return;


            if ("True" == dgv.CurrentRow.Cells["质检"].Value.ToString())
            {
                MessageBox.Show("已经质检,不能修改数据...", "注意");
                return;

            }

            if ("True" == dgv.CurrentRow.Cells["维修接收"].Value.ToString())
            {
                MessageBox.Show("已经外修确认,不能修改数据...", "注意");
                return;

            }


            if (!string.IsNullOrEmpty(dgv.CurrentRow.Cells["维修接收人"].Value.ToString()))
            {

                if (dgv.CurrentRow.Cells["维修接收人"].Value.ToString() != SQLDatabase.nowUserName())
                {
                    MessageBox.Show("请" + dgv.CurrentRow.Cells["维修接收人"].Value.ToString() + "修改数据...", "注意");
                    return;

                }
            }

            /////////////////////////////////////////////////////////////////

            if ("外修客户编码" == dgv.CurrentCell.OwningColumn.Name || "外修客户名称" == dgv.CurrentCell.OwningColumn.Name)
            {

                string sel;



                sel = "SELECT  repairclient_code as 编码, repairclient_name as 名称 FROM ly_sales_client_Repair  ";


                QueryForm queryForm = new QueryForm();


                queryForm.Sel = sel;
                queryForm.Constr = SQLDatabase.Connectstring;

                //Set the Column Collection to the filter Table
                //queryForm.SetSourceColumns(this.billMainDataSet.BalanceBill.Columns);

                queryForm.ShowDialog();


                if (!string.IsNullOrEmpty(queryForm.Result))
                {
                    dgv.CurrentRow.Cells["外修客户编码"].Value = queryForm.Result;
                    dgv.CurrentRow.Cells["外修客户名称"].Value = queryForm.Result1;
                    dgv.CurrentRow.Cells["维修接收人"].Value = SQLDatabase.nowUserName();
                    //dgv.CurrentRow.Cells["维修接收"].Value = "True";
                    dgv.CurrentRow.Cells["维修接收日期"].Value = SQLDatabase.GetNowdate();
                  



                }
                else
                {

                    dgv.CurrentRow.Cells["外修客户编码"].Value = DBNull.Value;
                    dgv.CurrentRow.Cells["外修客户名称"].Value = DBNull.Value;
                    dgv.CurrentRow.Cells["维修接收人"].Value = DBNull.Value;
                    //dgv.CurrentRow.Cells["维修接收"].Value = "True";
                    dgv.CurrentRow.Cells["维修接收日期"].Value = DBNull.Value;

                }

                SaveDetail();
                return;

            }

            /////////////////////////////////////////////////////////////////////////////////

            ///////////////////////////////////////////////////////////////////

            //if ("维修接收人" == dgv.CurrentCell.OwningColumn.Name)
            //{

            //    string sel;



            //    sel = "SELECT  yhmc as 姓名, yhmc as 代码 FROM T_users WHERE (bumen = '000302') ";


            //    QueryForm queryForm = new QueryForm();


            //    queryForm.Sel = sel;
            //    queryForm.Constr = SQLDatabase.Connectstring;

            //    //Set the Column Collection to the filter Table
            //    //queryForm.SetSourceColumns(this.billMainDataSet.BalanceBill.Columns);

            //    queryForm.ShowDialog();


            //    if (!string.IsNullOrEmpty(queryForm.Result))
            //    {
            //        dgv.CurrentRow.Cells["维修接收人"].Value = queryForm.Result;
            //        dgv.CurrentRow.Cells["维修接收"].Value = "True";
            //        dgv.CurrentRow.Cells["维修接收日期"].Value = SQLDatabase.GetNowdate();
                   


            //    }
            //    else
            //    {

            //        dgv.CurrentRow.Cells["维修接收人"].Value = DBNull.Value;
            //        dgv.CurrentRow.Cells["维修接收"].Value = "False";
            //        dgv.CurrentRow.Cells["维修接收日期"].Value = DBNull.Value;

            //    }

            //    /////////////////////////////////////////////////////////////////////////////////
            //    //if ("True" == dgv.CurrentRow.Cells["维修接收"].Value.ToString())
            //    //{
            //    //    dgv.CurrentRow.Cells["维修接收"].Value = "False";
                    
            //    //    dgv.CurrentRow.Cells["维修接收人"].Value = DBNull.Value;
            //    //    dgv.CurrentRow.Cells["维修接收日期"].Value = DBNull.Value;
                    
            //    //}
            //    //else
            //    //{

            //    //    dgv.CurrentRow.Cells["维修接收"].Value = "True";

            //    //    dgv.CurrentRow.Cells["维修接收人"].Value = SQLDatabase.nowUserName();
            //    //    dgv.CurrentRow.Cells["维修接收日期"].Value = SQLDatabase.GetNowdate();
                   
            //    //}


            //    SaveDetail();



            //    return;

            //}
            //////////////////////////////////////////////////////////////////////////
            //if ("维修方式" == dgv.CurrentCell.OwningColumn.Name)
            //{

            //    string sel;



            //    sel = "SELECT  style_name as 方式, id as 代码 FROM ly_sales_repair_style ";


            //    QueryForm queryForm = new QueryForm();


            //    queryForm.Sel = sel;
            //    queryForm.Constr = SQLDatabase.Connectstring;

            //    //Set the Column Collection to the filter Table
            //    //queryForm.SetSourceColumns(this.billMainDataSet.BalanceBill.Columns);

            //    queryForm.ShowDialog();


            //    if (queryForm.Result != "")
            //    {
            //        dgv.CurrentRow.Cells["维修方式"].Value = queryForm.Result;


            //    }
            //    else
            //    {

            //        dgv.CurrentRow.Cells["维修方式"].Value = DBNull.Value;

            //    }

            //    SaveDetail();



            //    return;

            //}
            ///////////////////////////////////////////////////////////////

            if ("维修接收日期" == dgv.CurrentCell.OwningColumn.Name)
            {

                //if ("True" == dgv.CurrentRow.Cells["已交"].Value.ToString())
                //{
                //    MessageBox.Show("合同文本已经提交,不能修改交付日期...", "注意");

                //    return;
                //}

                ChangeValue queryForm = new ChangeValue();

                queryForm.OldValue = dgv.CurrentCell.Value.ToString();
                queryForm.NewValue = "";
                queryForm.ChangeMode = "datetime";
                queryForm.ShowDialog();


                if (queryForm.NewValue != "")
                {
                    dgv.CurrentRow.Cells["维修接收日期"].Value = queryForm.NewValue;


                }
                else
                {

                    dgv.CurrentRow.Cells["维修接收日期"].Value = DBNull.Value;


                }



                SaveDetail();



                return;

            }
            ///////////////////////////////////////////////////////////////////////////////
            //if ("维修日期" == dgv.CurrentCell.OwningColumn.Name)
            //{

            //    //if ("True" == dgv.CurrentRow.Cells["已交"].Value.ToString())
            //    //{
            //    //    MessageBox.Show("合同文本已经提交,不能修改交付日期...", "注意");

            //    //    return;
            //    //}

            //    ChangeValue queryForm = new ChangeValue();

            //    queryForm.OldValue = dgv.CurrentCell.Value.ToString();
            //    queryForm.NewValue = "";
            //    queryForm.ChangeMode = "datetime";
            //    queryForm.ShowDialog();


            //    if (queryForm.NewValue != "")
            //    {
            //        dgv.CurrentRow.Cells["维修日期"].Value = queryForm.NewValue;


            //    }
            //    else
            //    {

            //        dgv.CurrentRow.Cells["维修日期"].Value = DBNull.Value;


            //    }



            //    SaveDetail();



            //    return;

            //}
            //////////////////////////////////////////////////////////////////////////
            //////////////////////////////////////////////////////////////////////////
            //if ("交货人" == dgv.CurrentCell.OwningColumn.Name)
            //{

            //    string sel;



            //    sel = "SELECT  yhmc as 姓名, yhmc as 代码 FROM T_users WHERE (bumen = '000302') ";


            //    QueryForm queryForm = new QueryForm();


            //    queryForm.Sel = sel;
            //    queryForm.Constr = SQLDatabase.Connectstring;

            //    //Set the Column Collection to the filter Table
            //    //queryForm.SetSourceColumns(this.billMainDataSet.BalanceBill.Columns);

            //    queryForm.ShowDialog();


            //    if (queryForm.Result != "")
            //    {
            //        dgv.CurrentRow.Cells["交货人"].Value = queryForm.Result;
                    
               
            //    }
            //    else
            //    {

            //        dgv.CurrentRow.Cells["交货人"].Value = DBNull.Value;
                    
            //    }
            
            //    SaveDetail();



            //    return;

            //}
            //////////////////////////////////////////////////////////////////////////
            ///////////////////////////////
            //if ("故障现象" == dgv.CurrentCell.OwningColumn.Name)
            //{

            //    ChangeValue queryForm = new ChangeValue();

            //    queryForm.OldValue = dgv.CurrentCell.Value.ToString();
            //    queryForm.NewValue = "";
            //    queryForm.ChangeMode = "longstring";
            //    queryForm.ShowDialog();




            //    if (queryForm.NewValue != "")
            //    {
            //        dgv.CurrentRow.Cells["故障现象"].Value = queryForm.NewValue;
                   

            //    }
            //    else
            //    {
            //        dgv.CurrentRow.Cells["故障现象"].Value = DBNull.Value;
                   
            //    }

            //    SaveDetail();
            //    return;

            //}
            /////////////////////////////////////////////////////////////////////

            //if ("质检" == dgv.CurrentCell.OwningColumn.Name)
            //{



            //    if ("True" == dgv.CurrentRow.Cells["质检"].Value.ToString())
            //    {
            //        dgv.CurrentRow.Cells["质检"].Value = "False";

            //        dgv.CurrentRow.Cells["质检员"].Value = DBNull.Value;
            //        dgv.CurrentRow.Cells["质检日期"].Value = DBNull.Value;

            //    }
            //    else
            //    {

            //        dgv.CurrentRow.Cells["质检"].Value = "True";

            //        dgv.CurrentRow.Cells["质检员"].Value = SQLDatabase.nowUserName();
            //        dgv.CurrentRow.Cells["质检日期"].Value = SQLDatabase.GetNowdate();

            //    }


            //    SaveDetail();



            //    return;

            //}
            //////////////////////////////////////////////////////////////////////////
            ///////////////////////////////
            //if ("质检意见" == dgv.CurrentCell.OwningColumn.Name)
            //{

            //    ChangeValue queryForm = new ChangeValue();

            //    queryForm.OldValue = dgv.CurrentCell.Value.ToString();
            //    queryForm.NewValue = "";
            //    queryForm.ChangeMode = "longstring";
            //    queryForm.ShowDialog();




            //    if (queryForm.NewValue != "")
            //    {
            //        dgv.CurrentRow.Cells["质检意见"].Value = queryForm.NewValue;


            //    }
            //    else
            //    {
            //        dgv.CurrentRow.Cells["质检意见"].Value = DBNull.Value;

            //    }

            //    SaveDetail();
            //    return;

            //}
            ///////////////////////////////////////////////////////////////////
         
        }

        private void ly_sales_receive_itemDetailDataGridView_SelectionChanged(object sender, EventArgs e)
        {


            if (null == ly_sales_receive_itemDetailDataGridView.CurrentRow)
            {
                this.ly_sales_receive_itemDetail_repairTableAdapter.Fill(this.lYSalseMange2.ly_sales_receive_itemDetail_repair, -1);
                return;
            }


            int nowId = int.Parse(ly_sales_receive_itemDetailDataGridView.CurrentRow.Cells["detail_id"].Value.ToString());


            this.ly_sales_receive_itemDetail_repairTableAdapter.Fill(this.lYSalseMange2.ly_sales_receive_itemDetail_repair, nowId);
          

            
        }

        private void toolStripTextBox6_KeyUp(object sender, KeyEventArgs e)
        {
            string filterString;


            filterString = GetDataGridviewMultiFilterString.DGVMultiFilterString(this.ly_inma0010DataGridView, this.toolStripTextBox6.Text);


            this.ly_inma0010ylBindingSource.Filter = filterString;
        }

        private void toolStripTextBox6_Enter(object sender, EventArgs e)
        {
            toolStripTextBox6.Text = "";

            this.ly_inma0010ylBindingSource.Filter = "";
        }

        private void ly_inma0010DataGridView_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (null == ly_inma0010DataGridView.CurrentRow) return;

            string nowitemno = ly_inma0010DataGridView.CurrentRow.Cells["物资编号"].Value.ToString();
            int nowparent_id = int.Parse(ly_sales_receive_itemDetailDataGridView.CurrentRow.Cells["detail_id"].Value.ToString());

            if (0 <= this.ly_sales_receive_itemDetail_repairBindingSource.Find("更换件编号", nowitemno))
            {
                MessageBox.Show("更换件已经选择,修改数量即可...", "注意");
                this.ly_sales_receive_itemDetail_repairBindingSource.Position = this.ly_sales_receive_itemDetail_repairBindingSource.Find("更换件编号", nowitemno);
                return;

            }

            this.ly_sales_receive_itemDetail_repairBindingSource.AddNew();


            this.ly_sales_receive_itemDetail_repairDataGridView.CurrentRow.Cells["parent_id1"].Value = nowparent_id;
            this.ly_sales_receive_itemDetail_repairDataGridView.CurrentRow.Cells["更换件编号"].Value = nowitemno;

           
       



            this.ly_sales_receive_itemDetail_repairBindingSource.EndEdit();

            this.ly_sales_receive_itemDetail_repairTableAdapter.Update(this.lYSalseMange2.ly_sales_receive_itemDetail_repair);

            this.ly_sales_receive_itemDetail_repairTableAdapter.Fill(this.lYSalseMange2.ly_sales_receive_itemDetail_repair, nowparent_id);

            this.ly_sales_receive_itemDetail_repairBindingSource.Position = this.ly_sales_receive_itemDetail_repairBindingSource.Find("更换件编号", nowitemno);
        }

        private void ly_sales_receive_itemDetail_repairDataGridView_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            DataGridView dgv = sender as DataGridView;

            if (null == dgv.CurrentRow) return;


            //if ("True" == ly_material_plan_mainDataGridView.CurrentRow.Cells["生产审批0"].Value.ToString())
            //{
            //    MessageBox.Show("依赖书已经生产审批,不能修改数据...", "注意");
            //    return;

            //}




         
            if ("数量" == dgv.CurrentCell.OwningColumn.Name)
            {

                if ("True" == dgv.CurrentRow.Cells["领料批准"].Value.ToString())
                {

                    MessageBox.Show("已经批准,不能修改数据...", "注意");
                    return;
                }

                ChangeValue queryForm = new ChangeValue();

                queryForm.OldValue = dgv.CurrentCell.Value.ToString();
                queryForm.NewValue = "";
                queryForm.ChangeMode = "value";
                queryForm.ShowDialog();




                if (queryForm.NewValue != "")
                {

                   

                    dgv.CurrentRow.Cells["数量"].Value = queryForm.NewValue;


                    //dgv.CurrentRow.Cells["discount_money"].Value = DBNull.Value;

                    //dgv.CurrentRow.Cells["approve_flag"].Value = "False";ly_plan_getmaterialDataGridView

                    this.ly_sales_receive_itemDetail_repairBindingSource.EndEdit();

                   
                    try
                    {

                        this.ly_sales_receive_itemDetail_repairTableAdapter.Update(this.lYSalseMange2.ly_sales_receive_itemDetail_repair);
                    }

                    catch (SqlException sqle)
                    {

                        MessageBox.Show(sqle.Message.Split('\r')[0], "注意");
                       
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



            ///////////////////////////////////////////////////////////////
        }

        private void bindingNavigatorDeleteItem1_Click(object sender, EventArgs e)
        {
            if (null == ly_sales_receive_itemDetail_repairDataGridView.CurrentRow)
            {

                return;
            }

            //if ("True" == ly_material_plan_mainDataGridView.CurrentRow.Cells["生产审批0"].Value.ToString())
            //{
            //    MessageBox.Show("依赖书已经生产审批,不能删除数据...", "注意");
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


            string message = "确定删除当前更换件吗？";
            string caption = "提示...";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result;



            result = MessageBox.Show(message, caption, buttons,
            MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

            if (result == DialogResult.Yes)
            {




                this.ly_sales_receive_itemDetail_repairBindingSource.RemoveCurrent();


                ly_sales_receive_itemDetail_repairDataGridView.EndEdit();
                ly_sales_receive_itemDetail_repairBindingSource.EndEdit();



                this.ly_sales_receive_itemDetail_repairTableAdapter.Update(this.lYSalseMange2.ly_sales_receive_itemDetail_repair);


               
            }
        }

        private void toolStripButton19_Click(object sender, EventArgs e)
        {
            LY_Sales_RepairClientSet queryForm = new LY_Sales_RepairClientSet();


            queryForm.StartPosition = FormStartPosition.CenterParent;
            queryForm.ShowDialog();
        }

        //private void fillToolStripButton_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        this.ly_sales_receive_itemDetail_repairTableAdapter.Fill(this.lYSalseMange2.ly_sales_receive_itemDetail_repair, new System.Nullable<int>(((int)(System.Convert.ChangeType(parent_idToolStripTextBox.Text, typeof(int))))));
        //    }
        //    catch (System.Exception ex)
        //    {
        //        System.Windows.Forms.MessageBox.Show(ex.Message);
        //    }

        //}

       

      
       

     ////////////////////////////////////////////////////////////////

       
    }
}
