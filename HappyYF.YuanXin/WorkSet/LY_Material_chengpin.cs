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
using System.Transactions;
using DataGridFilter;


namespace HappyYF.YuanXin.WorkSet
{
    public partial class LY_Material_chengpin : Form
    {

        string parentNum = "noSet";

        public LY_Material_chengpin()
        {
            InitializeComponent();
        }

        private void ly_inma0010BindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.ly_inma00101BindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.lYMaterialMange);

        }

        private void LY_MaterialBom_Load(object sender, EventArgs e)
        {
            if (SQLDatabase.CheckHaveRight(SQLDatabase.NowUserID, "物料成本计算"))
            {
                this.ly_inma0010DataGridView.Columns["成本单价"].Visible = true;
                this.toolStripButton6.Visible = true;
            }
            else
            {
                this.ly_inma0010DataGridView.Columns["成本单价"].Visible = false;
                this.toolStripButton6.Visible = false ;
            }
            
            
            
            this.ly_inma00101TableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            this.ly_inma00101TableAdapter.Fill(this.lYMaterialMange.ly_inma00101);
           
          

        }

        private void toolStripTextBox1_Enter(object sender, EventArgs e)
        {
            toolStripTextBox1.Text = "";

            this.ly_inma00101BindingSource.Filter = "";
        }
        
        private void toolStripTextBox1_KeyUp(object sender, KeyEventArgs e)
        {
           

            string filterString;


            filterString = GetDataGridviewMultiFilterString.DGVMultiFilterString(this.ly_inma0010DataGridView, this.toolStripTextBox1.Text);


            this.ly_inma00101BindingSource.Filter = filterString;
        }

       

        private void ly_inma0010DataGridView_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            return;
            if (null == ly_inma0010DataGridView.CurrentRow) return;
            string s = this.ly_inma0010DataGridView.CurrentRow.Cells["物资编号"].Value.ToString();
            //int loanid = int.Parse(this.xD_Client_LoanDataGridView.CurrentRow.Cells["Id_loan"].Value.ToString());

            LY_MaterialAdd queryForm = new LY_MaterialAdd();

            queryForm.statemode = "成品";
            queryForm.runmode = "修改";
            queryForm.material_code = s;

            queryForm.StartPosition = FormStartPosition.CenterParent;
            queryForm.ShowDialog();

            //if (queryForm.DialogResult != DialogResult.Cancel)
            //{
            //    this.ly_inma0010cpTableAdapter.Fill(this.lYMaterialMange.ly_inma0010cp);

            //    this.ly_inma0010cpBindingSource.Position = this.ly_inma0010cpBindingSource.Find("物资编号", s);
            //}
            
            //if (null == ly_inma0010DataGridView.CurrentRow) return;
            //string s = this.ly_inma0010DataGridView.CurrentRow.Cells["物资编号"].Value.ToString();

            //string selAllString = "SELECT     parentno, itemno,  itemno +':' +itemname as itemname  from f_BomExtend('" + s +"',1) ORDER BY  id_num ";
            //string cString = SQLDatabase.Connectstring; ;
            //SqlDataAdapter bomAdapter = new SqlDataAdapter(selAllString, cString);

            //bomAdapter.SelectCommand.CommandTimeout = 0;

            //DataSet bomData = new DataSet();
            //bomAdapter.Fill(bomData);

            //this.treeView1.Nodes.Clear();
            //MakeTreeView(bomData.Tables[0], null, null);
            ////this.treeView1.ExpandAll();
            //this.treeView1.SelectedNode = this.treeView1.Nodes[0];
            //this.treeView1.SelectedNode.Expand();

            //this.groupBox1.Text = s + "BOM结构图";
           
        }

       

      

        private void bindingNavigatorAddNewItem_Click(object sender, EventArgs e)
        {
            LY_MaterialAdd queryForm = new LY_MaterialAdd();

            queryForm.material_code = "";
            queryForm.runmode = "增加";
            queryForm.statemode = "成品";

            queryForm.StartPosition = FormStartPosition.CenterParent;
            queryForm.ShowDialog();

            //if (queryForm.DialogResult != DialogResult.Cancel)
            //{
            //    this.ly_inma0010cpTableAdapter.Fill(this.lYMaterialMange.ly_inma0010cp);
            //    this.ly_inma0010cpBindingSource.Position = this.ly_inma0010cpBindingSource.Find("物资编号", queryForm.material_code);
            //}
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if (null == ly_inma0010DataGridView.CurrentRow) return;
            string s = this.ly_inma0010DataGridView.CurrentRow.Cells["物资编号"].Value.ToString();
            //int loanid = int.Parse(this.xD_Client_LoanDataGridView.CurrentRow.Cells["Id_loan"].Value.ToString());

            LY_MaterialAdd queryForm = new LY_MaterialAdd();

            queryForm.statemode = "成品";
            queryForm.runmode = "修改";
            queryForm.material_code = s;

            queryForm.StartPosition = FormStartPosition.CenterParent;
            queryForm.ShowDialog();

            //if (queryForm.DialogResult != DialogResult.Cancel)
            //{
            //    this.ly_inma0010cpTableAdapter.Fill(this.lYMaterialMange.ly_inma0010cp);

            //    this.ly_inma0010cpBindingSource.Position = this.ly_inma0010cpBindingSource.Find("物资编号", s);
            //}

        }

        private void bindingNavigatorDeleteItem_Click(object sender, EventArgs e)
        {
            string message = "确定删除当前记录吗？";
            string caption = "提示...";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result;



            result = MessageBox.Show(message, caption, buttons,
            MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

            if (result == DialogResult.Yes)
            {

                //this.ly_inma0010cpBindingSource.RemoveCurrent();


                //ly_inma0010DataGridView.EndEdit();
                //ly_inma0010cpBindingSource.EndEdit();


                //this.ly_inma0010cpTableAdapter.Update(this.lYMaterialMange.ly_inma0010cp);

                //string s = this.xD_Sel_SellBalanceDataGridView.CurrentRow.Cells["编号"].Value.ToString();

                //this.hS_ClientPaymentTableAdapter.Fill(this.xD_SellBalance.HS_ClientPayment, s);


            }
        }

        private void ly_inma0010DataGridView_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            DataGridView dgv = sender as DataGridView;

            if (null == dgv.CurrentRow) return;



            if ("指导单价" == dgv.CurrentCell.OwningColumn.Name)
            {


                ChangeValue queryForm = new ChangeValue();

                queryForm.OldValue = dgv.CurrentCell.Value.ToString();
                queryForm.NewValue = "";
                queryForm.ChangeMode = "value";
                queryForm.ShowDialog();


                if (queryForm.NewValue != "")
                {

                    dgv.CurrentRow.Cells["指导单价"].Value = queryForm.NewValue;

                    int main_Id = int.Parse(dgv.CurrentRow.Cells["id"].Value.ToString());




                    string updstr = " update ly_inma0010  " +
                            "  set sales_price=  " + queryForm.NewValue + " where  id=" + main_Id.ToString();


                    SqlConnection sqlConnection1 = new SqlConnection(SQLDatabase.Connectstring);
                    SqlCommand cmd = new SqlCommand();

                    cmd.CommandText = updstr;
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

                }



                return;

            }

            ////////////////////////////////////////////////////////////////////////////////////
            if ("产品折扣" == dgv.CurrentCell.OwningColumn.Name)
            {


                ChangeValue queryForm = new ChangeValue();

                queryForm.OldValue = dgv.CurrentCell.Value.ToString();
                queryForm.NewValue = "";
                queryForm.ChangeMode = "value";
                queryForm.ShowDialog();


                int main_Id = int.Parse(dgv.CurrentRow.Cells["id"].Value.ToString());
                string updstr;

                if (queryForm.NewValue != "")
                {

                    dgv.CurrentRow.Cells["产品折扣"].Value = queryForm.NewValue;

                  
                     updstr = " update ly_inma0010  " +
                            "  set product_discount=  " + queryForm.NewValue + " where  id=" + main_Id.ToString();


                  

                }
                else
                {

                    dgv.CurrentRow.Cells["产品折扣"].Value = DBNull.Value;


                    updstr = " update ly_inma0010  " +
                           "  set product_discount= null where  id=" + main_Id.ToString();

                }


                SqlConnection sqlConnection1 = new SqlConnection(SQLDatabase.Connectstring);
                SqlCommand cmd = new SqlCommand();

                cmd.CommandText = updstr;
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



                return;

            }


            ///////////////////////////////////////////////////////
        }

        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            if (null == this.ly_inma0010DataGridView.CurrentRow) return;

            string nowplannum;
            string noename;

            NewFrm.Show(this);
            foreach (DataGridViewRow dgr in ly_inma0010DataGridView.Rows)
            {
                nowplannum = dgr.Cells["物资编号"].Value.ToString();

                noename = dgr.Cells["名称"].Value.ToString();

                //this.toolStripLabel3.Text = plannum;
                //this.toolStripLabel3.Invalidate();

                NewFrm.Notify(this, "正在计算:  (" + nowplannum + ") " + noename + "   成本");



                Countmoney(nowplannum);

            }

            NewFrm.Hide(this);
        }

        private static void Countmoney(string nowplannum)
        {
            string updstr = " update ly_inma0010  " +
                          "  set mprice=dbo.f_Item_price(wzbh,1)  "
                          + " where  wzbh='" + nowplannum + "'";



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

        private void toolStripButton7_Click(object sender, EventArgs e)
        {
            ExportDataGridviewTOExcell.ExportDataGridview(this.ly_inma0010DataGridView, true);
        }

        private void toolStripButton8_Click(object sender, EventArgs e)
        {
            string updstr = " update ly_inma0010  " +
                          "  set salse_cost_price= ISNULL(mprice, 0) + ISNULL(pprice, 0) + ISNULL(assembly_price, 0) + ISNULL(standardset_price, 0) + ISNULL(operation_cost, 0),  " +
                           " salse_cost_price_novat= ISNULL(mprice_novat, 0) + ISNULL(pprice_novat, 0) + ISNULL(assembly_price_novat, 0) + ISNULL(standardset_price_novat, 0) + ISNULL(operation_cost_novat, 0)  ";


                         



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

        
    }
}
