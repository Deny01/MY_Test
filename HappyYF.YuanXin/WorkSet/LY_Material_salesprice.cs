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
    public partial class LY_Material_salesprice : Form
    {
        List<string> itemlist = new List<string>();

        public LY_Material_salesprice()
        {
            InitializeComponent();
        }

        private void ly_inma0010BindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.ly_inma0010cpBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.lYMaterialMange);

        }

        private void LY_MaterialBom_Load(object sender, EventArgs e)
        {
           
            this.ly_prod_deptTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            this.ly_prod_deptTableAdapter.Fill(this.lYMaterialMange.ly_prod_dept);

          
            this.ly_inma0010cpTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;

            this.ly_inma0010cpTableAdapter.Fill(this.lYMaterialMange.ly_inma0010cp);

        }

        private void toolStripTextBox1_Enter(object sender, EventArgs e)
        {
            toolStripTextBox1.Text = "";

            this.ly_inma0010cpBindingSource.Filter = "";

      
        }
        
        private void toolStripTextBox1_KeyUp(object sender, KeyEventArgs e)
        {
            string filterString;


            filterString = GetDataGridviewMultiFilterString.DGVMultiFilterString(this.ly_inma0010DataGridView, this.toolStripTextBox1.Text);
            this.ly_inma0010cpBindingSource.Filter = "(" + filterString + ")";



        }

       

        private void ly_inma0010DataGridView_MouseDoubleClick(object sender, MouseEventArgs e)
        {

            if (null == ly_inma0010DataGridView.CurrentRow) return;

            //if (!checkLock())
            //{
            //    MessageBox.Show("已经锁定无法修改...", "注意");
            //    return;

            //}

            //string salespeople = this.ly_inma0010DataGridView.CurrentRow.Cells["负责人"].Value.ToString();
            //if (!string.IsNullOrEmpty(salespeople))
            //{
            //    if (salespeople != SQLDatabase.nowUserName())
            //    {
            //        MessageBox.Show("请负责人:" + salespeople + "操作", "注意");
            //        return;
            //    }
            //}
            //else
            //{
            //    MessageBox.Show("请联系技术部领导进行负责人指定...", "注意");
            //    return;
            //}

            DataGridView dgv = sender as DataGridView;
            if ("商务型号" == dgv.CurrentCell.OwningColumn.Name)
            {
                string wzbh= dgv.CurrentRow.Cells["物资编号"].Value.ToString();
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
            LY_MaterialAdd queryForm = new LY_MaterialAdd();

            queryForm.statemode = "成品";
            queryForm.runmode = "修改";
            queryForm.material_code = s;

            queryForm.StartPosition = FormStartPosition.CenterParent;
            queryForm.ShowDialog();

            if (queryForm.DialogResult != DialogResult.Cancel)
            {
                this.ly_inma0010cpTableAdapter.Fill(this.lYMaterialMange.ly_inma0010cp);

                this.ly_inma0010cpBindingSource.Position = this.ly_inma0010cpBindingSource.Find("物资编号", s);
            }
            
           
           
        }

       

      

        private void bindingNavigatorAddNewItem_Click(object sender, EventArgs e)
        {
            LY_MaterialAdd queryForm = new LY_MaterialAdd();

            queryForm.material_code = "";
            queryForm.runmode = "增加";
            queryForm.statemode = "成品";

            queryForm.StartPosition = FormStartPosition.CenterParent;
            queryForm.ShowDialog();

            if (queryForm.DialogResult != DialogResult.Cancel)
            {
                this.ly_inma0010cpTableAdapter.Fill(this.lYMaterialMange.ly_inma0010cp);
                this.ly_inma0010cpBindingSource.Position = this.ly_inma0010cpBindingSource.Find("物资编号", queryForm.material_code);
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if (null == ly_inma0010DataGridView.CurrentRow) return;

           

            string s = this.ly_inma0010DataGridView.CurrentRow.Cells["物资编号"].Value.ToString(); 

            LY_MaterialAdd queryForm = new LY_MaterialAdd();

            queryForm.statemode = "成品";
            queryForm.runmode = "修改";
            queryForm.material_code = s;

            queryForm.StartPosition = FormStartPosition.CenterParent;
            queryForm.ShowDialog();

            if (queryForm.DialogResult != DialogResult.Cancel)
            {
                this.ly_inma0010cpTableAdapter.Fill(this.lYMaterialMange.ly_inma0010cp);

                this.ly_inma0010cpBindingSource.Position = this.ly_inma0010cpBindingSource.Find("物资编号", s);
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
            string message = "确定删除当前记录吗？";
            string caption = "提示...";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result;


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

            result = MessageBox.Show(message, caption, buttons,
            MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

            if (result == DialogResult.Yes)
            {
                try
                {

                    this.ly_inma0010cpBindingSource.RemoveCurrent();
                    ly_inma0010DataGridView.EndEdit();
                    ly_inma0010cpBindingSource.EndEdit();
                    this.ly_inma0010cpTableAdapter.Update(this.lYMaterialMange.ly_inma0010cp);
                }
                catch (SqlException ex)

                {

                    MessageBox.Show(ex.Message.ToString(), "注意");
                }

            }
        }

        private void toolStripButton16_Click(object sender, EventArgs e)
        {
            ExportDataGridviewTOExcellA.ExportDataGridview(this.ly_inma0010DataGridView, true);
        }

        private void ly_inma0010DataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void 删除子件ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!SQLDatabase.CheckHaveRight(SQLDatabase.NowUserID, "指定物料负责人"))
            {
                return;
            }

            DataGridView dgv = ly_inma0010DataGridView;





            string nowitemno;

            string sel = "SELECT distinct yhbm as 代码,yhmc as 名称 FROM T_users where bumen='0007'  order by yhbm";


            QueryForm queryForm = new QueryForm();


            queryForm.Sel = sel;
            queryForm.Constr = SQLDatabase.Connectstring;

            queryForm.ShowDialog();


            dgv.Columns["负责人"].SortMode = DataGridViewColumnSortMode.NotSortable;
            NewFrm.Show(this);
            foreach (DataGridViewRow dgr in dgv.Rows)
            {
                if (true == dgr.Selected)
                {

                    nowitemno = dgr.Cells["物资编号"].Value.ToString();

                    this.itemlist.Add(nowitemno);

                    NewFrm.Notify(this, "正在更新:  (" + nowitemno + ")" + "   负责人");
                    Update_work_people(nowitemno, queryForm.Result1);
                }
            }

            this.ly_inma0010cpTableAdapter.Fill(this.lYMaterialMange.ly_inma0010cp);

            NewFrm.Hide(this);

            int nowindex = 0;
            foreach (string nowitem in itemlist)
            {
                nowindex = this.ly_inma0010cpBindingSource.Find("物资编号", nowitem);
                dgv.Rows[nowindex].Selected = true;
            }


        }


        private void Update_work_people(string nowitemno, string WorkPeople)
        {
            if (string.IsNullOrEmpty(WorkPeople)) return;

            SqlConnection sqlConnection1 = new SqlConnection(SQLDatabase.Connectstring);
            SqlCommand cmd = new SqlCommand();


            cmd.Parameters.Add("@nowitemno", SqlDbType.VarChar);
            cmd.Parameters["@nowitemno"].Value = nowitemno;

 

            cmd.Parameters.Add("@work_people", SqlDbType.VarChar);
            cmd.Parameters["@work_people"].Value = WorkPeople;
             

            cmd.CommandText = "LY_UpdateWorkPeople";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = sqlConnection1;

            sqlConnection1.Open();
            int aaa = cmd.ExecuteNonQuery();
            sqlConnection1.Close();




        }

        private void 设置产品系列ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!SQLDatabase.CheckHaveRight(SQLDatabase.NowUserID, "设置产品系列"))
            {
                return;
            }

            DataGridView dgv = ly_inma0010DataGridView;





            string nowitemno;

            string sel = "SELECT distinct series_code as 系列编码,series_name as 系列名称 FROM ly_inma0010_series ";


            QueryForm queryForm = new QueryForm();


            queryForm.Sel = sel;
            queryForm.Constr = SQLDatabase.Connectstring;

            queryForm.ShowDialog();


            dgv.Columns["系列码"].SortMode = DataGridViewColumnSortMode.NotSortable;
            NewFrm.Show(this);
            foreach (DataGridViewRow dgr in dgv.Rows)
            {
                if (true == dgr.Selected)
                {

                    nowitemno = dgr.Cells["物资编号"].Value.ToString();

                    this.itemlist.Add(nowitemno);

                    NewFrm.Notify(this, "正在更新:  (" + nowitemno + ")" + "   产品系列");
                    Update_ser(nowitemno, queryForm.Result, queryForm.Result1);
                }
            }

            this.ly_inma0010cpTableAdapter.Fill(this.lYMaterialMange.ly_inma0010cp);

            NewFrm.Hide(this);

            int nowindex = 0;
            foreach (string nowitem in itemlist)
            {
                nowindex = this.ly_inma0010cpBindingSource.Find("物资编号", nowitem);
                dgv.Rows[nowindex].Selected = true;
            }

        }

        private void Update_ser(string nowitemno, string code,string name)
        {
            if (string.IsNullOrEmpty(code)) return;

            SqlConnection sqlConnection1 = new SqlConnection(SQLDatabase.Connectstring);
            SqlCommand cmd = new SqlCommand();


            cmd.Parameters.Add("@nowitemno", SqlDbType.VarChar);
            cmd.Parameters["@nowitemno"].Value = nowitemno;



            cmd.Parameters.Add("@code", SqlDbType.VarChar);
            cmd.Parameters["@code"].Value = code;
            cmd.Parameters.Add("@name", SqlDbType.VarChar);
            cmd.Parameters["@name"].Value = name;

            cmd.CommandText = "LY_Updateser";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = sqlConnection1;

            sqlConnection1.Open();
            int aaa = cmd.ExecuteNonQuery();
            sqlConnection1.Close();




        }
    }
}
