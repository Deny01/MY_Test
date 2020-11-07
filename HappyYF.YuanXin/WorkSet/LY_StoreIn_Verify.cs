using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Transactions;
using HappyYF.Infrastructure.Repositories;
using DataGridFilter;

namespace HappyYF.YuanXin.WorkSet
{
    public partial class LY_StoreIn_Verify : Form
    {
        public LY_StoreIn_Verify()
        {
            InitializeComponent();
            this.ly_store_innumverifyTableAdapter.CommandTimeout = 0;
        }





        private void LY_StoreOut_Verify_Load(object sender, EventArgs e)
        {
            //this.dateTimePicker1.Text = (DateTime.Today.AddDays(26 - DateTime.Today.Day)).AddMonths(-1).Date.ToString();
            //this.dateTimePicker2.Text = DateTime.Today.AddDays(26 - DateTime.Today.Day).Date.ToString();

            this.dateTimePicker1.Text = (DateTime.Today.AddDays(1 - DateTime.Today.Day)).AddMonths(-1).Date.AddMonths(1).ToString();
            this.dateTimePicker2.Text = DateTime.Today.AddDays(1 - DateTime.Today.Day).AddDays(-1).Date.AddMonths(1).ToString();

            this.ly_store_innumverifyTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            this.ly_store_in_innumTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.ly_store_innumverifyBindingSource.Filter = "";
            this.ly_store_innumverifyTableAdapter.Fill(this.lYStoreMange.ly_store_innumverify, DateTime.Parse(this.dateTimePicker1.Text).Date, DateTime.Parse(this.dateTimePicker2.Text).AddDays(1).Date);
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            if (null == this.ly_store_innumverifyDataGridView.CurrentRow) return;
            FilterForm filterForm = new FilterForm();



            List<string> ls = new List<string>();
            ls.Add("id");


            filterForm.SetSourceColumns(ly_store_innumverifyDataGridView.Columns, ls);

            filterForm.ShowDialog();

            this.ly_store_innumverifyBindingSource.Filter = filterForm.GetFilterString();
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            if (null == this.ly_store_innumverifyDataGridView.CurrentRow) return;
            SortForm DataSort = new SortForm();

            List<string> ls = new List<string>();
            ls.Add("id");


            DataSort.SetSortColumns(ly_store_innumverifyDataGridView.Columns, ls);
            DataSort.ShowDialog();
            this.ly_store_innumverifyBindingSource.Sort = DataSort.GetSortString();
        }



        private void ly_store_outnumDateDataGridView_SelectionChanged(object sender, EventArgs e)
        {
            if (null == ly_store_innumverifyDataGridView.CurrentRow) return;




            string inNum = this.ly_store_innumverifyDataGridView.CurrentRow.Cells["入库单号"].Value.ToString();


            this.ly_store_in_innumTableAdapter.Fill(this.lYStoreMange.ly_store_in_innum, inNum, SQLDatabase.NowUserID);



        }

        private void Savefinished(string flag)
        {
            string inNum = this.ly_store_innumverifyDataGridView.CurrentRow.Cells["入库单号"].Value.ToString();
            string delstr;

            if ("1" == flag)
            {
                delstr = " update ly_store_in set finished=" + flag + ",verify_people='" + SQLDatabase.nowUserName() + "',verify_date=getdate() where in_number = '" + inNum + "'";
            }
            else
            {
                delstr = " update ly_store_in set finished=" + flag + ",verify_people=null,verify_date=null where in_number = '" + inNum + "'";
            }

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
                    temp = 1;


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
            if (1 == temp)
            {


                //this.ly_material_plan_mainTableAdapter.Fill(this.lYPlanMange.ly_material_plan_main, "SCJH");
            }

        }

        private void ly_store_outnumDateDataGridView_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            DataGridView dgv = sender as DataGridView;






            if ("签证" == dgv.CurrentCell.OwningColumn.Name)
            {
                //return;
                for (int i = 0; i < ly_store_in_ylDataGridView.Rows.Count; i++)
                {
                    decimal k = 0;
                    k = decimal.Parse(ly_store_in_ylDataGridView.Rows[i].Cells["TotalB"].Value.ToString());
                    if (k < 0)
                    {
                        MessageBox.Show("入库单中出现了负数");

                        return;

                    }

                }

                if ("True" != dgv.CurrentRow.Cells["签证"].Value.ToString())
                {

                    string message = "确定签证吗?";
                    string caption = "提示...";
                    MessageBoxButtons buttons = MessageBoxButtons.YesNo;
                    DialogResult result;



                    result = MessageBox.Show(message, caption, buttons,
                    MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

                    if (result == DialogResult.Yes)
                    {

                        //dgv.CurrentRow.Cells["discount_money"].Value = dgv.CurrentRow.Cells["apply_money"].Value;
                        dgv.CurrentRow.Cells["签证"].Value = "True";
                        dgv.CurrentRow.Cells["签证人"].Value = SQLDatabase.nowUserName();
                        dgv.CurrentRow.Cells["签证日期"].Value = DateTime.Now.ToString();
                        Savefinished("1");
                    }

                }
                else
                {

                    if (SQLDatabase.nowUserName() != dgv.CurrentRow.Cells["签证人"].Value.ToString())
                    {

                        MessageBox.Show("请签证人:" + dgv.CurrentRow.Cells["签证人"].Value.ToString() + " 修改");

                        return;
                    }


                    string message = "取消签证吗?";
                    string caption = "提示...";
                    MessageBoxButtons buttons = MessageBoxButtons.YesNo;
                    DialogResult result;



                    result = MessageBox.Show(message, caption, buttons,
                    MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

                    if (result == DialogResult.Yes)
                    {
                        //dgv.CurrentRow.Cells["discount_money"].Value = DBNull.Value;
                        //dgv.CurrentRow.Cells["apply_money"].Value = DBNull.Value;
                        dgv.CurrentRow.Cells["签证"].Value = "False";
                        dgv.CurrentRow.Cells["签证人"].Value = "";
                        dgv.CurrentRow.Cells["签证日期"].Value = "";
                        Savefinished("0");
                    }
                }

                return;
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            LY_StoreinSet queryForm = new LY_StoreinSet();


            queryForm.StartPosition = FormStartPosition.CenterParent;
            queryForm.ShowDialog();
        }

        private void 查看入库明细ToolStripMenuItem_Click(object sender, EventArgs e)
        {

            LY_Store_InOutQuery_View queryForm = new LY_Store_InOutQuery_View();


            string s = this.ly_store_in_ylDataGridView.CurrentRow.Cells["物料编号1"].Value.ToString();

            queryForm.material_code = s;

            queryForm.StartPosition = FormStartPosition.CenterParent;
            queryForm.ShowDialog();


        }

        private void 查看入库价格来源ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ly_store_in_ylDataGridView.CurrentRow == null) return;
            string wzbh = ly_store_in_ylDataGridView.CurrentRow.Cells["物料编号1"].Value.ToString();
            string djbh = ly_store_in_ylDataGridView.CurrentRow.Cells["单据编号1"].Value.ToString();
            string machine_Code = ly_store_in_ylDataGridView.CurrentRow.Cells["机器码"].Value.ToString();
            string storeInId = ly_store_in_ylDataGridView.CurrentRow.Cells["id"].Value.ToString();
            if (string.IsNullOrEmpty(djbh)) return;
            if (djbh.Length < 2) return;
            string Rs = djbh.Substring(0, 2);
            switch (Rs)
            {
                case "CG":

                    LY_GetPurchasePrice queryForm = new LY_GetPurchasePrice();
                    queryForm.InStr = djbh;
                    queryForm.Code = wzbh;
                    queryForm.StartPosition = FormStartPosition.CenterParent;
                    queryForm.ShowDialog();

                    break;




                case "GD":

                    LY_GetRestructuringPrice queryFormDG = new LY_GetRestructuringPrice();
                    queryFormDG.InStr = djbh;
                    queryFormDG.Code = machine_Code;
                    queryFormDG.StartPosition = FormStartPosition.CenterParent;
                    queryFormDG.ShowDialog();

                    break;

                case "GQ":

                    LY_GetRestructuringPrice queryFormQG = new LY_GetRestructuringPrice();
                    queryFormQG.InStr = djbh;
                    queryFormQG.Code = machine_Code;
                    queryFormQG.StartPosition = FormStartPosition.CenterParent;
                    queryFormQG.ShowDialog();

                    break;

                case "DZ":

                    LY_GetQzDzPrice queryFormDZ = new LY_GetQzDzPrice();
                    queryFormDZ.InStr = djbh;
                    queryFormDZ.Code = machine_Code;
                    queryFormDZ.StartPosition = FormStartPosition.CenterParent;
                    queryFormDZ.ShowDialog();

                    break;

                case "QZ":

                    LY_GetQzDzPrice queryFormQZ = new LY_GetQzDzPrice();
                    queryFormQZ.InStr = djbh;
                    queryFormQZ.Code = machine_Code;
                    queryFormQZ.StartPosition = FormStartPosition.CenterParent;
                    queryFormQZ.ShowDialog();

                    break;

                case "ZJ":

                    LY_GetMachinePrice queryFormZJ = new LY_GetMachinePrice();
                    queryFormZJ.InStrId = storeInId;
                    queryFormZJ.StartPosition = FormStartPosition.CenterParent;
                    queryFormZJ.ShowDialog();

                    break;

                case "JY":

                    LY_OutSourcePrice queryFormJY = new LY_OutSourcePrice();
                    queryFormJY.InStrId = storeInId;
                    queryFormJY.StartPosition = FormStartPosition.CenterParent;
                    queryFormJY.ShowDialog();

                    break;


                default:
                    //Console.WriteLine("Default case");
                    break;
            }
        }

        private void toolStripTextBox1_Enter(object sender, EventArgs e)
        {
            //toolStripTextBox1.Text = "";

            //this.ly_store_innumverifyBindingSource.Filter = "";
        }

        private void toolStripTextBox1_KeyUp(object sender, KeyEventArgs e)
        {
            //string filterString;


            //filterString = GetDataGridviewMultiFilterString.DGVMultiFilterString(this.ly_store_innumverifyDataGridView, this.toolStripTextBox1.Text);
            //this.ly_store_innumverifyBindingSource.Filter = "(" + filterString + ")";
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            if (toolStripTextBox1.Text == "")
            {
                this.ly_store_innumverifyBindingSource.Filter = "";
            }
            else
            {
                string filterString;


                filterString = GetDataGridviewMultiFilterString.DGVMultiFilterString(this.ly_store_innumverifyDataGridView, this.toolStripTextBox1.Text);
                this.ly_store_innumverifyBindingSource.Filter = "(" + filterString + ")";
            }
        }

        private static void UpdatecategoryInfo(string nowcategory, string nowId)
        {
            string updstr = " update ly_store_in  " +
                      "  set  category = '" + nowcategory + "',categoryFin=dbo.f_category_conversion('" + nowcategory + "') where  id=" + nowId + "";


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

        private void ly_store_in_ylDataGridView_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            DataGridView dgv = sender as DataGridView;






            if ("组别" == dgv.CurrentCell.OwningColumn.Name)
            {
                //return;




                string message = "确定修改组别吗?";
                string caption = "提示...";
                MessageBoxButtons buttons = MessageBoxButtons.YesNo;
                DialogResult result;



                result = MessageBox.Show(message, caption, buttons,
                MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

                if (result == DialogResult.Yes)
                {
                    string nowid = dgv.CurrentRow.Cells["id"].Value.ToString();

                    string sel = @"select category_code as 编码,category_name as 名称 from dbo.ly_materialcategory order by category_code";
                    QueryForm queryForm = new QueryForm();


                    queryForm.Sel = sel;
                    queryForm.Constr = SQLDatabase.Connectstring;

                    queryForm.ShowDialog();
                    if (queryForm.Result != "")
                    {
                        UpdatecategoryInfo(queryForm.Result1, nowid);

                        string inNum = this.ly_store_innumverifyDataGridView.CurrentRow.Cells["入库单号"].Value.ToString();


                        this.ly_store_in_innumTableAdapter.Fill(this.lYStoreMange.ly_store_in_innum, inNum, SQLDatabase.NowUserID);

                    }






                    return;
                }

            }
            //else
            //{

            //    if (SQLDatabase.nowUserName() != dgv.CurrentRow.Cells["签证人"].Value.ToString())
            //    {

            //        MessageBox.Show("请签证人:" + dgv.CurrentRow.Cells["签证人"].Value.ToString() + " 修改");

            //        return;
            //    }


            //    string message = "取消签证吗?";
            //    string caption = "提示...";
            //    MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            //    DialogResult result;



            //    result = MessageBox.Show(message, caption, buttons,
            //    MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

            //    if (result == DialogResult.Yes)
            //    {
            //        //dgv.CurrentRow.Cells["discount_money"].Value = DBNull.Value;
            //        //dgv.CurrentRow.Cells["apply_money"].Value = DBNull.Value;
            //        dgv.CurrentRow.Cells["签证"].Value = "False";
            //        dgv.CurrentRow.Cells["签证人"].Value = "";
            //        dgv.CurrentRow.Cells["签证日期"].Value = "";
            //        Savefinished("0");
            //    }
            //}

            return;

        }

        private void ly_store_in_ylDataGridView_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            if (string.IsNullOrEmpty(ly_store_in_ylDataGridView.Rows[e.RowIndex].Cells["差异"].Value.ToString()))
            {
                ly_store_in_ylDataGridView.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.Red;
                return;
            }
            decimal status = decimal.Parse(ly_store_in_ylDataGridView.Rows[e.RowIndex].Cells["差异"].Value.ToString());

            if (status > 10 || status < -10 )

            {
                ly_store_in_ylDataGridView.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.Red;  //整行颜

            }
        }
    }
}
