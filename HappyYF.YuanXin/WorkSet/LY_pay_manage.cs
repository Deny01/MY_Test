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
    public partial class LY_pay_manage : Form
    {
        public LY_pay_manage()
        {
            InitializeComponent();
        }

        protected void loaddata()
        {
            if (ly_pay_grid.CurrentRow == null)
            {
                ly_pay_contract_cgTableAdapter.Fill(this.LYStoreMange.ly_pay_contract_cg, "");
                return;
            }

            string contract_code = ly_pay_grid.CurrentRow.Cells["合同编号"].Value.ToString();
            if (contract_code.Contains("CG"))
            {
                ly_pay_contract_cgTableAdapter.Fill(this.LYStoreMange.ly_pay_contract_cg, contract_code);
                this.tabPage2.Parent = null;
                this.tabPage3.Parent = null;
            }
            else if (contract_code.Contains("WX"))
            {
                this.tabPage1.Parent = null;
                this.tabPage3.Parent = null;
            }
            else
            {
                this.tabPage1.Parent = null;
                this.tabPage2.Parent = null;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.ly_invoice_pay_timeTableAdapter.Fill(this.LYStoreMange.ly_invoice_pay_time, this.dateTimePicker1.Value, this.dateTimePicker2.Value.AddDays(1));
            this.ly_pay_approveTableAdapter.Fill(this.LYStoreMange.ly_pay_approve, this.dateTimePicker1.Value, this.dateTimePicker2.Value.AddDays(1));
        }

        private void LY_Quality_Control_PurchaseRep_Load(object sender, EventArgs e)
        {
            this.dateTimePicker1.Text = DateTime.Today.AddMonths(-1).Date.ToString();
            this.dateTimePicker2.Text = DateTime.Today.Date.ToString();
            this.ly_invoice_pay_timeTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            this.ly_pay_approveTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            this.ly_pay_contract_cgTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
        }

        private void toolStripButton10_Click(object sender, EventArgs e)
        {
            ExportDataGridviewTOExcellA.ExportDataGridview(this.ly_pay_grid, true);
        }

      

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            ExportDataGridviewTOExcellA.ExportDataGridview(this.ly_cg, true);
        }

        private void ly_pay_grid_SelectionChanged(object sender, EventArgs e)
        {
            loaddata();
        }

        private void ly_pay_grid_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (ly_pay_grid.CurrentRow == null)
            {
                ly_pay_contract_cgTableAdapter.Fill(this.LYStoreMange.ly_pay_contract_cg, "");
                return;
            }
            DataGridView dgv = sender as DataGridView;


            if ("审批" == dgv.CurrentCell.OwningColumn.Name)
            {
                if (dgv.CurrentRow.Cells["审定"].Value.ToString() == "True")
                {
                    MessageBox.Show("已经审定...", "注意");
                    return;
                }
                if (!SQLDatabase.CheckHaveRight(SQLDatabase.NowUserID, "付款审批"))
                {
                    MessageBox.Show("无付款审批权限", "注意");
                    return;
                }
                if ("True" == dgv.CurrentRow.Cells["审批"].Value.ToString())
                {
                    dgv.CurrentRow.Cells["审批"].Value = "False";
                    dgv.CurrentRow.Cells["审批人"].Value = DBNull.Value;
                    dgv.CurrentRow.Cells["审批时间"].Value = DBNull.Value;
                }
                else
                {
                    dgv.CurrentRow.Cells["审批"].Value = "True";
                    dgv.CurrentRow.Cells["审批人"].Value = SQLDatabase.nowUserName();
                    dgv.CurrentRow.Cells["审批时间"].Value = SQLDatabase.GetNowtime();
                }

                return;
            }

            if ("审定" == dgv.CurrentCell.OwningColumn.Name)
            {
                if (dgv.CurrentRow.Cells["付款"].Value.ToString() == "True")
                {
                    MessageBox.Show("已经付款...", "注意");
                    return;
                }
                if (!SQLDatabase.CheckHaveRight(SQLDatabase.NowUserID, "付款审定"))
                {
                    MessageBox.Show("无付款审定权限", "注意");
                    return;
                }

                if ("True" == dgv.CurrentRow.Cells["审定"].Value.ToString())
                {
                    dgv.CurrentRow.Cells["审定"].Value = "False";
                    dgv.CurrentRow.Cells["审定人"].Value = DBNull.Value;
                    dgv.CurrentRow.Cells["审定时间"].Value = DBNull.Value;
                }
                else
                {
                    dgv.CurrentRow.Cells["审定"].Value = "True";
                    dgv.CurrentRow.Cells["审定人"].Value = SQLDatabase.nowUserName();
                    dgv.CurrentRow.Cells["审定时间"].Value = SQLDatabase.GetNowtime();
                }
                return;
            }
            this.ly_pay_grid.EndEdit();
            this.lyinvoicepaytimeBindingSource.EndEdit();
            this.ly_invoice_pay_timeTableAdapter.Update(this.LYStoreMange.ly_invoice_pay_time);

            loaddata();

        }
    }
}
