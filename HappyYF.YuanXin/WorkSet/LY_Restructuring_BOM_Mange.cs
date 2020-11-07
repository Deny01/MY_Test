using System;

using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;

using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HappyYF.Infrastructure.Repositories;
using DataGridFilter;

namespace HappyYF.YuanXin.WorkSet
{
    public partial class LY_Restructuring_BOM_Mange : Form
    {
        public LY_Restructuring_BOM_Mange()
        {
            InitializeComponent();
        }

        private void ly_Restructuring_Bom_mainBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            SaveMain();

        }

        private void SaveMain()
        {
           
            if (string.IsNullOrEmpty(this.ly_Restructuring_Bom_mainDataGridView.CurrentRow.Cells["改后编码"].Value.ToString()))
            {
                MessageBox.Show("必须输入改后编码", "注意");
                return;
            }

            if (string.IsNullOrEmpty(this.ly_Restructuring_Bom_mainDataGridView.CurrentRow.Cells["改前编码"].Value.ToString()))
            {
                MessageBox.Show("必须输入改前编码", "注意");
                return;
            }

            this.Validate();
            try
            {
                this.ly_Restructuring_Bom_mainBindingSource.EndEdit();
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message, "注意");
                return;
            }

            try
            {
                this.ly_Restructuring_Bom_mainTableAdapter.Update(this.lYPlanMange.ly_Restructuring_Bom_main);
                this.ly_Restructuring_Bom_mainTableAdapter.Fill(this.lYPlanMange.ly_Restructuring_Bom_main);
            }
            catch (SqlException sqle)
            {
                if (2601 == sqle.Number)
                {
                    MessageBox.Show("系统已经存在同样的改制条目（改前编码 改后编码），请重新选择", "注意");
                }
                else
                {
                    MessageBox.Show(sqle.Message, "注意");
                }

            }
            finally
            {
           

            }
        }

        private void LY_Restructuring_BOM_Mange_Load(object sender, EventArgs e)
        {

            this.ly_Restructuring_Bom_requestTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            this.ly_Restructuring_Bom_returnTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;

            this.ly_Bom_expendTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            this.ly_Bom_expend1TableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;

            this.ly_Restructuring_Bom_mainTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            this.ly_Restructuring_Bom_mainTableAdapter.Fill(this.lYPlanMange.ly_Restructuring_Bom_main);

        }

        private void ly_Restructuring_Bom_mainDataGridView_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            DataGridView dgv = sender as DataGridView;
            string inspector = this.ly_Restructuring_Bom_mainDataGridView.CurrentRow.Cells["制定人"].Value.ToString();
            if (!string.IsNullOrEmpty(inspector))
            {
                if (inspector != SQLDatabase.nowUserName())
                {
                    MessageBox.Show("请提交人:" + inspector + "操作", "注意");
                    return;
                }
            }

            if (!string.IsNullOrEmpty(dgv.CurrentRow.Cells["改后编码"].Value.ToString()) && !string.IsNullOrEmpty(dgv.CurrentRow.Cells["改前编码"].Value.ToString()))
            {
                string wzbh= dgv.CurrentRow.Cells["改后编码"].Value.ToString();
                string ori = dgv.CurrentRow.Cells["改前编码"].Value.ToString();
              
                //using (SqlConnection con = new SqlConnection(SQLDatabase.Connectstring))
                //{
                //    string sql = "select count(1) from  ly_material_plan_detail where wzbh='" + wzbh + "' and origin_itemno='" + ori + "'";

                //    using (SqlCommand cmd = new SqlCommand(sql, con))
                //    {

                //        con.Open();
                //        int k = Convert.ToInt32(cmd.ExecuteScalar());
                //        if (k > 0)
                //        {
                //            MessageBox.Show("已有改制计划不可操作", "注意");
                //            return;
                //        }
                //    }
                //}




            }



            if ("改后编码" == dgv.CurrentCell.OwningColumn.Name)
            {
 
                string sel = "SELECT   wzbh as 编号,mch as 名称,xhc as 型号,xhj as 日方,gg as 规格 FROM ly_inma0010";


                QueryForm queryForm = new QueryForm();


                queryForm.Sel = sel;
                queryForm.Constr = SQLDatabase.Connectstring;



                queryForm.ShowDialog();

                if (!string.IsNullOrEmpty(queryForm.Result))
                {



                    dgv.CurrentRow.Cells["改后编码"].Value = queryForm.Result;
                    dgv.CurrentRow.Cells["制定人"].Value = SQLDatabase.nowUserName();
                    SaveMain();


                }
                else
                {

                    return;
                }




            }

            if ("改前编码" == dgv.CurrentCell.OwningColumn.Name)
            {




                string sel = "SELECT   wzbh as 编号,mch as 名称,xhc as 型号,xhj as 日方,gg as 规格 FROM ly_inma0010";


                QueryForm queryForm = new QueryForm();


                queryForm.Sel = sel;
                queryForm.Constr = SQLDatabase.Connectstring;



                queryForm.ShowDialog();

                if (!string.IsNullOrEmpty(queryForm.Result))
                {



                    dgv.CurrentRow.Cells["改前编码"].Value = queryForm.Result;
                    SaveMain();
                     
                }
                else
                {

                    return;
                }
                 

            }
 

            if ("工时" == dgv.CurrentCell.OwningColumn.Name)
            {
                ChangeValue queryForm = new ChangeValue();

                queryForm.OldValue = dgv.CurrentCell.Value.ToString();
                queryForm.NewValue = "";
                queryForm.ChangeMode = "value";
                queryForm.ShowDialog();




                if (queryForm.NewValue != "")
                {
                    dgv.CurrentRow.Cells["工时"].Value = queryForm.NewValue;
 
                    SaveMain();


                }
                else
                {


                }
                return;
            }

 

            if ("工时单价" == dgv.CurrentCell.OwningColumn.Name)
            {
                ChangeValue queryForm = new ChangeValue();

                queryForm.OldValue = dgv.CurrentCell.Value.ToString();
                queryForm.NewValue = "";
                queryForm.ChangeMode = "value";
                queryForm.ShowDialog();

                if (queryForm.NewValue != "")
                {
                    dgv.CurrentRow.Cells["工时单价"].Value = queryForm.NewValue;
                    SaveMain();

                }
                else
                {


                }
                return;
            }
            

            if ("备注" == dgv.CurrentCell.OwningColumn.Name)
            {
                ChangeValue queryForm = new ChangeValue();

                queryForm.OldValue = dgv.CurrentCell.Value.ToString();
                queryForm.NewValue = "";
                queryForm.ChangeMode = "longstring";
                queryForm.ShowDialog();


                if (queryForm.NewValue != "")
                {
                    dgv.CurrentRow.Cells["备注"].Value = queryForm.NewValue;
                    SaveMain();
                }
                else
                {


                }
                return;
            }

           
        }



        private void ly_Restructuring_Bom_mainDataGridView_SelectionChanged(object sender, EventArgs e)
        {
            DataGridView dgv = sender as DataGridView;

            if (null == dgv.CurrentRow)
            {
                return;
            }
            string nowdisitemno = dgv.CurrentRow.Cells["改后编码"].Value.ToString();
            string noworiitemno = dgv.CurrentRow.Cells["改前编码"].Value.ToString();

            int nowid_main = int.Parse(dgv.CurrentRow.Cells["id_main"].Value.ToString());

            this.ly_Restructuring_Bom_requestTableAdapter.Fill(this.lYMaterialMange.ly_Restructuring_Bom_request, nowid_main);
            this.ly_Restructuring_Bom_returnTableAdapter.Fill(this.lYMaterialMange.ly_Restructuring_Bom_return, nowid_main);

            this.ly_Bom_expendTableAdapter.Fill(this.lYMaterialMange.ly_Bom_expend, nowdisitemno);
            this.ly_Bom_expend1TableAdapter.Fill(this.lYMaterialMange.ly_Bom_expend1, noworiitemno, 1, noworiitemno);
        }

        private void ly_Bom_expendDataGridView_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (this.ly_Restructuring_Bom_mainDataGridView.CurrentRow == null) return;
            string inspector = this.ly_Restructuring_Bom_mainDataGridView.CurrentRow.Cells["制定人"].Value.ToString();

            if (inspector != SQLDatabase.nowUserName())
            {
                MessageBox.Show("请提交人:" + inspector + "操作", "注意");
                return;
            }

            DataGridView dgv = sender as DataGridView;
            if (null == dgv.CurrentRow) return;

           
            int nowid_main = int.Parse(ly_Restructuring_Bom_mainDataGridView.CurrentRow.Cells["id_main"].Value.ToString());
            string nowitemno = dgv .CurrentRow.Cells["编码1"].Value.ToString();
            if (0 <= this.ly_Restructuring_Bom_requestBindingSource.Find("itemno", nowitemno))
            {
                MessageBox.Show("改制需求换件已经选择,修改数量即可...", "注意");
                this.ly_Restructuring_Bom_requestBindingSource.Position = this.ly_Restructuring_Bom_requestBindingSource.Find("itemno", nowitemno);
                return;

            }

            this.ly_Restructuring_Bom_requestBindingSource.AddNew();

            this.ly_Restructuring_Bom_requestDataGridView.CurrentRow.Cells["parent_id1"].Value = nowid_main;
            this.ly_Restructuring_Bom_requestDataGridView.CurrentRow.Cells["编码req"].Value = nowitemno;
            this.ly_Restructuring_Bom_requestBindingSource.EndEdit();

            this.ly_Restructuring_Bom_requestTableAdapter.Update(this.lYMaterialMange.ly_Restructuring_Bom_request);

            this.ly_Restructuring_Bom_requestTableAdapter.Fill(this.lYMaterialMange.ly_Restructuring_Bom_request, nowid_main);
            this.ly_Restructuring_Bom_requestBindingSource.Position = this.ly_Restructuring_Bom_requestBindingSource.Find("itemno", nowitemno);

        }

        private void ly_Bom_expend1DataGridView_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (this.ly_Restructuring_Bom_mainDataGridView.CurrentRow == null) return;
            string inspector = this.ly_Restructuring_Bom_mainDataGridView.CurrentRow.Cells["制定人"].Value.ToString();

            if (inspector != SQLDatabase.nowUserName())
            {
                MessageBox.Show("请提交人:" + inspector + "操作", "注意");
                return;
            }
            DataGridView dgv = sender as DataGridView;
            if (null == dgv.CurrentRow) return;


            int nowid_main = int.Parse(ly_Restructuring_Bom_mainDataGridView.CurrentRow.Cells["id_main"].Value.ToString());

            string nowitemno = dgv.CurrentRow.Cells["编码2"].Value.ToString();
            if (0 <= this.ly_Restructuring_Bom_returnBindingSource.Find("itemno", nowitemno))
            {
                MessageBox.Show("改制退料件已经选择,修改数量即可...", "注意");
                this.ly_Restructuring_Bom_returnBindingSource.Position = this.ly_Restructuring_Bom_returnBindingSource.Find("itemno", nowitemno);
                return;

            }

            this.ly_Restructuring_Bom_returnBindingSource.AddNew();


            this.ly_Restructuring_Bom_returnDataGridView.CurrentRow.Cells["parent_id2"].Value = nowid_main;
            this.ly_Restructuring_Bom_returnDataGridView.CurrentRow.Cells["编码ret"].Value = nowitemno;






            this.ly_Restructuring_Bom_returnBindingSource.EndEdit();

            this.ly_Restructuring_Bom_returnTableAdapter.Update(this.lYMaterialMange.ly_Restructuring_Bom_return);

            this.ly_Restructuring_Bom_returnTableAdapter.Fill(this.lYMaterialMange.ly_Restructuring_Bom_return, nowid_main);
            this.ly_Restructuring_Bom_returnBindingSource.Position = this.ly_Restructuring_Bom_returnBindingSource.Find("itemno", nowitemno);

        }

        private void bindingNavigatorDeleteItem1_Click(object sender, EventArgs e)
        {
            if (this.ly_Restructuring_Bom_mainDataGridView.CurrentRow == null) return;
            string inspector = this.ly_Restructuring_Bom_mainDataGridView.CurrentRow.Cells["制定人"].Value.ToString();

            if (inspector != SQLDatabase.nowUserName())
            {
                MessageBox.Show("请提交人:" + inspector + "操作", "注意");
                return;
            }
            this.ly_Restructuring_Bom_requestBindingSource.RemoveCurrent();
            SaveReq();

        }

        private void SaveReq()
        {
            
            this.ly_Restructuring_Bom_requestBindingSource.EndEdit();

            this.ly_Restructuring_Bom_requestTableAdapter.Update(this.lYMaterialMange.ly_Restructuring_Bom_request);
        }

        private void bindingNavigatorDeleteItem2_Click(object sender, EventArgs e)
        {
            if (this.ly_Restructuring_Bom_mainDataGridView.CurrentRow == null) return;
            string inspector = this.ly_Restructuring_Bom_mainDataGridView.CurrentRow.Cells["制定人"].Value.ToString();

            if (inspector != SQLDatabase.nowUserName())
            {
                MessageBox.Show("请提交人:" + inspector + "操作", "注意");
                return;
            }
            this.ly_Restructuring_Bom_returnBindingSource.RemoveCurrent();
            SaveRet();

        }

        private void SaveRet()
        {
           
            this.ly_Restructuring_Bom_returnBindingSource.EndEdit();

            this.ly_Restructuring_Bom_returnTableAdapter.Update(this.lYMaterialMange.ly_Restructuring_Bom_return);
        }

        private void toolStripTextBox2_KeyUp(object sender, KeyEventArgs e)
        {
            string dFilter = "";

            dFilter = GetDataGridviewMultiFilterString.DGVMultiFilterString(this.ly_Bom_expendDataGridView, this.toolStripTextBox2.Text);

            if (null == dFilter)
                dFilter = "";

            this.ly_Bom_expendBindingSource.Filter = dFilter;
        }

        private void toolStripTextBox2_Enter(object sender, EventArgs e)
        {
            toolStripTextBox2.Text = "";

            this.ly_Bom_expendBindingSource.Filter = "";
        }

        private void toolStripTextBox1_KeyUp(object sender, KeyEventArgs e)
        {
            string dFilter = "";

            dFilter = GetDataGridviewMultiFilterString.DGVMultiFilterString(this.ly_Bom_expend1DataGridView, this.toolStripTextBox1.Text);

            if (null == dFilter)
                dFilter = "";

            this.ly_Bom_expend1BindingSource.Filter = dFilter;
        }

        private void toolStripTextBox1_Enter(object sender, EventArgs e)
        {
            toolStripTextBox1.Text = "";

            this.ly_Bom_expend1BindingSource.Filter = "";
        }

        private void ly_Restructuring_Bom_requestDataGridView_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            DataGridView dgv = sender as DataGridView;
            if ("备注2" == dgv.CurrentCell.OwningColumn.Name)
            {
                ChangeValue queryForm = new ChangeValue();

                queryForm.OldValue = dgv.CurrentCell.Value.ToString();
                queryForm.NewValue = "";
                queryForm.ChangeMode = "longstring";
                queryForm.ShowDialog();


                if (queryForm.NewValue != "")
                {
                    dgv.CurrentRow.Cells["备注2"].Value = queryForm.NewValue;
                    SaveReq();
                }
                else
                {


                }
                return;
            }

            if ("数量1" == dgv.CurrentCell.OwningColumn.Name)
            {

                ChangeValue queryForm = new ChangeValue();

                queryForm.OldValue = dgv.CurrentCell.Value.ToString();
                queryForm.NewValue = "";
                queryForm.ChangeMode = "value";
                queryForm.ShowDialog();

                if (queryForm.NewValue != "")
                {
                    dgv.CurrentRow.Cells["数量1"].Value = queryForm.NewValue;
                    SaveReq(); 

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
 

        }

        private void ly_Restructuring_Bom_returnDataGridView_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
           
            DataGridView dgv = sender as DataGridView;

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
                    SaveRet();
                }
                else
                {


                }
                return;
            }




            if ("数量2" == dgv.CurrentCell.OwningColumn.Name)
            {

                ChangeValue queryForm = new ChangeValue();

                queryForm.OldValue = dgv.CurrentCell.Value.ToString();
                queryForm.NewValue = "";
                queryForm.ChangeMode = "value";
                queryForm.ShowDialog();




                if (queryForm.NewValue != "")
                {
                    dgv.CurrentRow.Cells["数量2"].Value = queryForm.NewValue;
                    SaveRet();

                }
                else
                { 

                }
                return;

            }

 
        }

        private void bindingNavigatorAddNewItem_Click(object sender, EventArgs e)
        {
            this.ly_Restructuring_Bom_mainBindingSource.AddNew(); 

            //this.ly_Restructuring_Bom_mainBindingSource.EndEdit();

            //this.ly_Restructuring_Bom_mainTableAdapter.Update(this.lYPlanMange.ly_Restructuring_Bom_main);
            //this.ly_Restructuring_Bom_mainTableAdapter.Fill(this.lYPlanMange.ly_Restructuring_Bom_main);
        }

        private void bindingNavigatorDeleteItem_Click(object sender, EventArgs e)
        {
            string inspector = this.ly_Restructuring_Bom_mainDataGridView.CurrentRow.Cells["制定人"].Value.ToString();

            if (inspector != SQLDatabase.nowUserName())
            {
                MessageBox.Show("请提交人:" + inspector + "操作", "注意");
                return;
            }
            this.ly_Restructuring_Bom_mainBindingSource.RemoveCurrent();
            this.ly_Restructuring_Bom_mainBindingSource.EndEdit();

            this.ly_Restructuring_Bom_mainTableAdapter.Update(this.lYPlanMange.ly_Restructuring_Bom_main);
            this.ly_Restructuring_Bom_mainTableAdapter.Fill(this.lYPlanMange.ly_Restructuring_Bom_main);
        }

        private void ly_Restructuring_Bom_mainDataGridView_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            MessageBox.Show("改前编码和改后编码不能为空！");return;
        }
    }
}
