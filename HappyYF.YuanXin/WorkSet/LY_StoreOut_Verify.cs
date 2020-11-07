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
    public partial class LY_StoreOut_Verify : Form
    {
        public LY_StoreOut_Verify()
        {
            InitializeComponent();
            this.ly_store_outnumDateTableAdapter.CommandTimeout = 0;
        }



       

        private void LY_StoreOut_Verify_Load(object sender, EventArgs e)
        {
            //this.dateTimePicker1.Text = (DateTime.Today.AddDays(26 - DateTime.Today.Day)).AddMonths(-1).Date.ToString();
            //this.dateTimePicker2.Text = DateTime.Today.AddDays(26 - DateTime.Today.Day).Date.ToString();

            this.dateTimePicker1.Text = (DateTime.Today.AddDays(1 - DateTime.Today.Day)).AddMonths(-1).Date.AddMonths(1).ToString();
            this.dateTimePicker2.Text = DateTime.Today.AddDays(1 - DateTime.Today.Day).AddDays(-1).Date.AddMonths(1).ToString();

            this.ly_store_outnumDateTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            this.ly_store_outFinTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.ly_store_outnumDateBindingSource.Filter = "";
            this.ly_store_outnumDateTableAdapter.Fill(this.lYStoreMange.ly_store_outnumDate, DateTime.Parse(this.dateTimePicker1.Text).Date, DateTime.Parse(this.dateTimePicker2.Text).AddDays(1).Date);
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            if (null == this.ly_store_outnumDateDataGridView.CurrentRow) return;
            FilterForm filterForm = new FilterForm();



            List<string> ls = new List<string>();
            ls.Add("id");


            filterForm.SetSourceColumns(ly_store_outnumDateDataGridView.Columns, ls);

            filterForm.ShowDialog();

            this.ly_store_outnumDateBindingSource.Filter = filterForm.GetFilterString();
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            if (null == this.ly_store_outnumDateDataGridView.CurrentRow) return;
            SortForm DataSort = new SortForm();

            List<string> ls = new List<string>();
            ls.Add("id");


            DataSort.SetSortColumns(ly_store_outnumDateDataGridView.Columns, ls);
            DataSort.ShowDialog();
            this.ly_store_outnumDateBindingSource.Sort = DataSort.GetSortString();
        }

       

        private void ly_store_outnumDateDataGridView_SelectionChanged(object sender, EventArgs e)
        {
            if (null == ly_store_outnumDateDataGridView.CurrentRow) return;




            string outNum = this.ly_store_outnumDateDataGridView.CurrentRow.Cells["出库单号"].Value.ToString();


                    this.ly_store_outFinTableAdapter.Fill(this.lYStoreMange.ly_store_outFin, outNum);

                   
            
        }

        private void Savefinished(string flag)
        {
            string outNum = this.ly_store_outnumDateDataGridView.CurrentRow.Cells["出库单号"].Value.ToString();
            string delstr;

            if ("1" == flag)
            {
                delstr = " update ly_store_out set finished=" + flag + ",verify_people='" + SQLDatabase.nowUserName() + "',verify_date=getdate() where out_number = '" + outNum + "'";
            }
            else
            {
                delstr = " update ly_store_out set finished=" + flag + ",verify_people=null,verify_date=null where out_number = '" + outNum + "'";
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

                for (int i = 0; i < ly_store_outDataGridView.Rows.Count; i++)
                {
                    decimal k = 0;
                    k = decimal.Parse(ly_store_outDataGridView.Rows[i].Cells["TotalB"].Value.ToString());
                    if (k <= 0)
                    {
                        //MessageBox.Show("出库单中出现了负数");
                     
                        //return;

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
            LY_StoreoutSet queryForm = new LY_StoreoutSet();


            queryForm.StartPosition = FormStartPosition.CenterParent;
            queryForm.ShowDialog();
        }

        private void ly_store_outDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            LY_StoreoutSubitemSet queryForm = new LY_StoreoutSubitemSet();


            queryForm.StartPosition = FormStartPosition.CenterParent;
            queryForm.ShowDialog();
        }

        private void 查看入库明细ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LY_Store_InOutQuery_View queryForm = new LY_Store_InOutQuery_View();


            string s = this.ly_store_outDataGridView.CurrentRow.Cells["物料编号1"].Value.ToString();

            queryForm.material_code = s;

            queryForm.StartPosition = FormStartPosition.CenterParent;
            queryForm.ShowDialog();
        }

        private void toolStripTextBox1_Enter(object sender, EventArgs e)
        {
            //toolStripTextBox1.Text = "";

            //this.ly_store_outnumDateBindingSource.Filter = "";
        }

        private void toolStripTextBox1_KeyUp(object sender, KeyEventArgs e)
        {
            //string filterString;


            //filterString = GetDataGridviewMultiFilterString.DGVMultiFilterString(this.ly_store_outnumDateDataGridView, this.toolStripTextBox1.Text);
            //this.ly_store_outnumDateBindingSource.Filter = "(" + filterString + ")";
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            if (toolStripTextBox1.Text == "")
            {
                this.ly_store_outnumDateBindingSource.Filter = "";
            }
            else
            {
                string filterString;


                filterString = GetDataGridviewMultiFilterString.DGVMultiFilterString(this.ly_store_outnumDateDataGridView, this.toolStripTextBox1.Text);
                this.ly_store_outnumDateBindingSource.Filter = "(" + filterString + ")";
            }
        }

        private void ly_store_outDataGridView_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            if (string.IsNullOrEmpty(ly_store_outDataGridView.Rows[e.RowIndex].Cells["差异"].Value.ToString()))
            {
                ly_store_outDataGridView.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.Red;  //整行颜
                return;
            }
            decimal status =decimal.Parse( ly_store_outDataGridView.Rows[e.RowIndex].Cells["差异"].Value.ToString());

            if (status > 10 || status < -10    )

            {
                ly_store_outDataGridView.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.Red;  //整行颜
               // ly_store_outDataGridView.Rows[e.RowIndex].Cells["差异"].Style.BackColor = Color.Red;  //某个单元格颜色
            }

 
        }
    }
}
