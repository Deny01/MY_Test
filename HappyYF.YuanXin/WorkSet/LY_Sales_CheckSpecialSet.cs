using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HappyYF.Infrastructure.Repositories;

namespace HappyYF.YuanXin.WorkSet
{
    public partial class LY_Sales_CheckSpecialSet : Form
    {

        public  int  nowparentId;

        public int nowspecialparentId;

        public LY_Sales_CheckSpecialSet()
        {
            InitializeComponent();
        }

        private void ly_sales_matingBomBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            //this.Validate();
            //this.ly_sales_matingBomBindingSource.EndEdit();
            //this.tableAdapterManager.UpdateAll(this.lYSalseMange2);

        }

        private void LY_Sales_CheckStandardSet_Load(object sender, EventArgs e)
        {
            this.ly_sales_matingBom_special_TableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            this.ly_sales_matingBom_mainspecialTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;


            this.ly_sales_matingBom_mainspecialTableAdapter.Fill(this.lYTecSet.ly_sales_matingBom_mainspecial, nowparentId);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void ly_sales_matingBom_mainspecialDataGridView_SelectionChanged(object sender, EventArgs e)
        {
            if (null == ly_sales_matingBom_mainspecialDataGridView.CurrentRow)
            {

                

                return;
            }

            nowspecialparentId =int .Parse ( ly_sales_matingBom_mainspecialDataGridView.CurrentRow.Cells["special_id"].Value.ToString());

            this.ly_sales_matingBom_special_TableAdapter.Fill(this.lYTecSet.ly_sales_matingBom_special_, nowspecialparentId);
           


        }

        private void 删除当前版本ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (null == ly_sales_matingBom_mainspecialDataGridView.CurrentRow)
            {

                return;
            }

            //if ("True" == ly_material_plan_mainDataGridView.CurrentRow.Cells["配套完成0"].Value.ToString())
            //{
            //    MessageBox.Show("依赖书已经技术配套,不能删除数据...", "注意");
            //    return;

            //}

            string setpeople = this.ly_sales_matingBom_mainspecialDataGridView.CurrentRow.Cells["版本设定"].Value.ToString();

            if (setpeople != SQLDatabase.nowUserName())
            {
                MessageBox.Show("请版本设定人:" + setpeople + "删除", "注意");
                return;
            }

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


            string message = "确定删除当前特殊配置吗？";
            string caption = "提示...";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result;



            result = MessageBox.Show(message, caption, buttons,
            MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

            if (result == DialogResult.Yes)
            {




                this.ly_sales_matingBom_mainspecialBindingSource.RemoveCurrent();


                ly_sales_matingBom_mainspecialDataGridView.EndEdit();
                ly_sales_matingBom_mainspecialBindingSource.EndEdit();



                this.ly_sales_matingBom_mainspecialTableAdapter.Update(this.lYTecSet.ly_sales_matingBom_mainspecial);

                //string nowplannum = ly_material_plan_mainDataGridView.CurrentRow.Cells["计划编号0"].Value.ToString();
                //MakeGroupTreeView(nowplannum);



                //this.ly_sales_contract_detailTableAdapter.Fill(this.lYSalseMange.ly_sales_contract_detail, nowinnerCode, 0);

                //if (null != ly_plan_getmaterialDataGridView.CurrentRow)
                //{

                //    string nowitemno = ly_plan_getmaterialDataGridView.CurrentRow.Cells["产品编号5"].Value.ToString();
                //    this.ly_sales_contract_detailBindingSource.Position = this.ly_sales_contract_detailBindingSource.Find("产品编码", nowitemno);
                //}

                //this.ly_sales_contract_main1DataGridView.SelectionChanged -= ly_sales_contract_main1DataGridView_SelectionChanged;
                //this.ly_sales_contract_main1TableAdapter.Fill(this.lYSalseMange.ly_sales_contract_main1, this.nowusercode, "single", this.dateTimePicker1.Value, this.dateTimePicker2.Value);
                //this.ly_sales_contract_main1DataGridView.SelectionChanged += ly_sales_contract_main1DataGridView_SelectionChanged;
            }
        }

        private void ly_sales_matingBom_mainspecialDataGridView_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            DataGridView dgv = sender as DataGridView;

            if (null == dgv.CurrentRow) return;


            string setpeople = dgv.CurrentRow.Cells["版本设定"].Value.ToString();

            if (setpeople != SQLDatabase.nowUserName())
            {
                MessageBox.Show("请版本设定人:" + setpeople + "修改", "注意");
                return;
            }


            ///////////////////////////////////////////////////////////////

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
                    //dgv.CurrentRow.Cells["discount_money"].Value = DBNull.Value;

                    //dgv.CurrentRow.Cells["approve_flag"].Value = "False";
                    ly_sales_matingBom_mainspecialBindingSource.EndEdit();

                    this.ly_sales_matingBom_mainspecialTableAdapter.Update(this.lYTecSet.ly_sales_matingBom_mainspecial);

                    //this.ly_sales_contract_detailTableAdapter.Fill(this.lYSalseMange.ly_sales_contract_detail, nowinnerCode, 0);

                    //CountPlanStru();

                }
                else
                {

                }
                return;

            }
         



            ///////////////////////////////////////////////////////////////

            if ("版本名称" == dgv.CurrentCell.OwningColumn.Name)
            {

                ChangeValue queryForm = new ChangeValue();

                queryForm.OldValue = dgv.CurrentCell.Value.ToString();
                queryForm.NewValue = "";
                queryForm.ChangeMode = "string";
                queryForm.ShowDialog();




                if (queryForm.NewValue != "")
                {
                    dgv.CurrentRow.Cells["版本名称"].Value = queryForm.NewValue;
                    //dgv.CurrentRow.Cells["discount_money"].Value = DBNull.Value;

                    //dgv.CurrentRow.Cells["approve_flag"].Value = "False";
                    ly_sales_matingBom_mainspecialBindingSource.EndEdit();
                                        
                    this.ly_sales_matingBom_mainspecialTableAdapter.Update(this.lYTecSet.ly_sales_matingBom_mainspecial);

                    //this.ly_sales_contract_detailTableAdapter.Fill(this.lYSalseMange.ly_sales_contract_detail, nowinnerCode, 0);

                    //CountPlanStru();

                }
                else
                {

                }
                return;

            }
        }

        //private void fillToolStripButton_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        this.ly_sales_matingBom_mainspecialTableAdapter.Fill(this.lYTecSet.ly_sales_matingBom_mainspecial, ((int)(System.Convert.ChangeType(nowIdToolStripTextBox.Text, typeof(int)))));
        //    }
        //    catch (System.Exception ex)
        //    {
        //        System.Windows.Forms.MessageBox.Show(ex.Message);
        //    }

        //}

        //private void fillToolStripButton_Click_1(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        this.ly_sales_matingBom_special_TableAdapter.Fill(this.lYTecSet.ly_sales_matingBom_special_, new System.Nullable<int>(((int)(System.Convert.ChangeType(parent_idToolStripTextBox.Text, typeof(int))))));
        //    }
        //    catch (System.Exception ex)
        //    {
        //        System.Windows.Forms.MessageBox.Show(ex.Message);
        //    }

        //}

        
    }
}
