using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HappyYF.Infrastructure.Repositories;

namespace HappyYF.YuanXin.WorkSet
{
    public partial class out_source_approve : Form
    {
        public out_source_approve()
        {
            InitializeComponent();
        }



        private void button1_Click(object sender, EventArgs e)
        {
            this.t_outsource_approveTableAdapter.Fill(this.lYQualityInspector.t_outsource_approve, DateTime.Parse(this.dateTimePicker1.Text).Date, DateTime.Parse(this.dateTimePicker2.Text).Date.AddDays(1));
        }

        private void LY_Quality_Control_PurchaseRep_Load(object sender, EventArgs e)
        {
            this.dateTimePicker1.Text = DateTime.Today.AddMonths(-3).Date.ToString();
            this.dateTimePicker2.Text = DateTime.Today.AddDays(1).Date.ToString();
            //////////////////////////////////////////////////////////////////////////////////////////

            this.t_outsource_approveTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            this.t_outsource_approveTableAdapter.Fill(this.lYQualityInspector.t_outsource_approve, DateTime.Parse(this.dateTimePicker1.Text).Date, DateTime.Parse(this.dateTimePicker2.Text).Date.AddDays(1));

        }




        private void toolStripTextBox3_Enter(object sender, EventArgs e)
        {
            toolStripTextBox3.Text = "";

            this.toutsourceapproveBindingSource.Filter = "";
        }

        private void toolStripTextBox3_KeyUp(object sender, KeyEventArgs e)
        {
            string filterString;


            filterString = GetDataGridviewMultiFilterString.DGVMultiFilterString(this.ly_purchase_contract_inspectionRepDataGridView, this.toolStripTextBox3.Text);


            this.toutsourceapproveBindingSource.Filter = filterString;
        }

        private void ly_purchase_contract_inspectionRepDataGridView_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            DataGridView dgv = sender as DataGridView;
            if (dgv.CurrentRow == null)
                return;
            if (SQLDatabase.CheckHaveRight(SQLDatabase.NowUserID, "外协退料过检"))
            {


            }
            else
            {
                MessageBox.Show("无权限,不能通过...", "注意");
                return;

            }
            if ("True" == dgv.CurrentRow.Cells["入库"].Value.ToString())
            {
                MessageBox.Show("已入库,不能通过...", "注意");
                return;
            }
            if ("审核" == dgv.CurrentCell.OwningColumn.Name)
            {


                if ("True" == dgv.CurrentRow.Cells["审核"].Value.ToString())
                {
                    string people = dgv.CurrentRow.Cells["审核人"].Value.ToString();
                    if (people != SQLDatabase.nowUserName())
                    {
                        MessageBox.Show("请审核人：" + people + " 操作", "注意");
                        return;
                    }

                    dgv.CurrentRow.Cells["审核"].Value = "False";
                    dgv.CurrentRow.Cells["审核人"].Value = DBNull.Value;
                    dgv.CurrentRow.Cells["审核时间"].Value = DBNull.Value;

                }
                else
                {

                    dgv.CurrentRow.Cells["审核"].Value = "True";
                    dgv.CurrentRow.Cells["审核人"].Value = SQLDatabase.nowUserName();
                    dgv.CurrentRow.Cells["审核时间"].Value = DateTime.Now;
                }
                this.ly_purchase_contract_inspectionRepDataGridView.EndEdit();
                this.toutsourceapproveBindingSource.EndEdit();

                this.t_outsource_approveTableAdapter.Update(this.lYQualityInspector.t_outsource_approve);
                return;
            }
        }
    }
}