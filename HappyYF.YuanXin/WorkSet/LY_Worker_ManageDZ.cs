using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HappyYF.Infrastructure.Repositories;
using Project_Manager.AppServices;

namespace HappyYF.YuanXin.WorkSet
{
    public partial class LY_Worker_ManageDZ : Form
    {
        public string formState;
        private string saveState;
        int nowRow;

        Point pt = new Point();

        
        
        private string prod_code;

        public string Prod_code
        {
            get { return prod_code; }
            set { prod_code = value; }
        }

        public LY_Worker_ManageDZ()
        {
            InitializeComponent();
        }

        private void LY_Worker_Manage_Load(object sender, EventArgs e)
        {
            SetFormState("View");

            this.ly_prod_deptTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            this.ly_prod_deptTableAdapter.Fill(this.lYMaterialMange.ly_prod_dept);

            this.ly_worker_listTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            this.ly_worker_listTableAdapter.Fill(this.lYMaterielRequirements.ly_worker_list,this .prod_code );

            if ("04" == this.prod_code)
            {
                this.Text = "机加人员管理";

            }
        }

        private void SetFormState(string state)
        {
            // view edit add save cancle

            if ("View" == state)
            {
                this.formState = "View";

                this.toolStripButton1.Enabled = true;
                this.toolStripButton2.Enabled = false;
                this.yX_fillCard_MoneyBindingNavigatorSaveItem.Enabled = false;
                this.bindingNavigatorDeleteItem.Enabled = true;
                this.bindingNavigatorAddNewItem.Enabled = true;
               
               


                this.bindingNavigatorMoveFirstItem.Enabled = true;
                this.bindingNavigatorMoveLastItem.Enabled = true;
                this.bindingNavigatorMoveNextItem.Enabled = true;
                this.bindingNavigatorMovePreviousItem.Enabled = true;
                this.bindingNavigatorPositionItem.Enabled = true;



                this.ly_worker_listDataGridView.ReadOnly = true;






            }
            else
            {
                this.formState = "Edit";

                this.ly_worker_listDataGridView.ReadOnly = false;

                if (null != ly_worker_listDataGridView.CurrentRow)
                    this.nowRow = ly_worker_listDataGridView.CurrentRow.Index;

                this.toolStripButton1.Enabled = false;
                this.toolStripButton2.Enabled = true;
                this.yX_fillCard_MoneyBindingNavigatorSaveItem.Enabled = true;
                this.bindingNavigatorDeleteItem.Enabled = false;
                this.bindingNavigatorAddNewItem.Enabled = false;
             



                this.bindingNavigatorMoveFirstItem.Enabled = false;
                this.bindingNavigatorMoveLastItem.Enabled = false;
                this.bindingNavigatorMoveNextItem.Enabled = false;
                this.bindingNavigatorMovePreviousItem.Enabled = false;
                this.bindingNavigatorPositionItem.Enabled = false;



                this.ly_worker_listDataGridView.ReadOnly = false;

                
                this.ly_worker_listDataGridView.Columns["部门"].ReadOnly = true;
               
               


            }


        }

      

        private void bindingNavigatorAddNewItem_Click(object sender, EventArgs e)
        {
            this.ly_worker_listBindingSource.AddNew();


            ly_worker_listDataGridView.CurrentRow.Cells["部门"].Value = "04";
            ly_worker_listDataGridView.CurrentRow.Cells["在职"].Value = "True";

            this.SetFormState("Edit");
            this.saveState = "Add";


            ly_worker_listDataGridView.CurrentCell = ly_worker_listDataGridView.CurrentRow.Cells["工号"];
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            this.SetFormState("Edit");
            this.saveState = "Change";
        }

        private void bindingNavigatorDeleteItem_Click(object sender, EventArgs e)
        {
            if (null == ly_worker_listDataGridView.CurrentRow) return;

            //DialogResult dr;
            //dr = new CheckKeywordsell().ShowDialog(this);

            //if (dr != DialogResult.OK)
            //{
            //    return;
            //}

            //if (SQLDatabase.nowUserName() != xD_client_compensate_loan1DataGridView.CurrentRow.Cells["operator1"].Value.ToString())
            //{

            //    MessageBox.Show("请输入人：" + xD_client_compensate_loan1DataGridView.CurrentRow.Cells["operator1"].Value.ToString() + "  删除记录...");
            //    return;

            //}

            string message = "确定删除当前记录吗？";
            string caption = "提示...";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result;



            result = MessageBox.Show(message, caption, buttons,
            MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

            if (result == DialogResult.Yes)
            {

                this.ly_worker_listBindingSource.RemoveCurrent();

                //if (0 <= this.xD_client_compensate_loan1BindingSource.Find("operator", "合计"))
                //    this.xD_client_compensate_loan1BindingSource.RemoveAt(this.xD_client_compensate_loan1BindingSource.Find("operator", "合计"));

                ly_worker_listDataGridView.EndEdit();
                ly_worker_listBindingSource.EndEdit();

                //this.tableAdapterManager1.UpdateAll(this.xD_loan);
                this.ly_worker_listTableAdapter.Update(this.lYMaterielRequirements.ly_worker_list);

                this.ly_worker_listTableAdapter.Fill(this.lYMaterielRequirements.ly_worker_list,this .prod_code );

                //AddSummationRowCard(xD_client_compensate_loan1BindingSource, xD_client_compensate_loan1DataGridView);
            }

            this.SetFormState("View");
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            this.saveState = "Cancle";


           
            this.SetFormState("View");
            this.ly_worker_listTableAdapter.Fill(this.lYMaterielRequirements.ly_worker_list, this.prod_code);
        }

        private void yX_fillCard_MoneyBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            if (null == ly_worker_listDataGridView.CurrentRow) return;

            ly_worker_listDataGridView.EndEdit();
            ly_worker_listBindingSource.EndEdit();

            if (!this.Validate()) return;

            //if (!CheckInput()) return;







            this.ly_worker_listTableAdapter.Update(this.lYMaterielRequirements.ly_worker_list);

            this.ly_worker_listTableAdapter.Fill(this.lYMaterielRequirements.ly_worker_list, this.prod_code);

          
            this.SetFormState("View");
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (this.formState != "View")
            {

                if (keyData == Keys.Enter)
                {
                    DataGridView dc = this.ly_worker_listDataGridView;

                    if (8 == dc.CurrentCell.ColumnIndex && dc.CurrentRow.Index != (dc.RowCount - 1))
                    {
                        //if (CheckInput())
                        //{
                            System.Windows.Forms.SendKeys.Send("{tab}");

                        //}


                    }


                    if (8 == dc.CurrentCell.ColumnIndex && dc.CurrentRow.Index == (dc.RowCount - 1))
                    {
                        ly_worker_listDataGridView.EndEdit();
                        ly_worker_listBindingSource.EndEdit();

                        //if (!this.Validate()) return;


                        //if (CheckInput())
                        //{

                        this.ly_worker_listTableAdapter.Update(this.lYMaterielRequirements.ly_worker_list);

                            /////////////////////////////////66



                            this.ly_worker_listBindingSource.AddNew();


                            ly_worker_listDataGridView.CurrentRow.Cells["部门"].Value = "04";
                            ly_worker_listDataGridView.CurrentRow.Cells["在职"].Value = "True";

                            ly_worker_listDataGridView.CurrentCell = ly_worker_listDataGridView.CurrentRow.Cells["工号"];

                        //}




                      

                    }
                    else
                    {

                        System.Windows.Forms.SendKeys.Send("{tab}");
                    }
                    return true;
                }
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void ly_worker_listDataGridView_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (4 == e.ColumnIndex || 8 == e.ColumnIndex)
            {
                AppSet appSet = AppSet.Load();

                if (0 < appSet.KeyboardInputIndex)
                {
                    InputLanguage.CurrentInputLanguage = InputLanguage.InstalledInputLanguages[appSet.KeyboardInputIndex];
                }
            }
        }

        private void ly_worker_listDataGridView_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
            if (4 == e.ColumnIndex || 8 == e.ColumnIndex)
            {
                InputLanguage.CurrentInputLanguage = InputLanguage.DefaultInputLanguage;
            }
        }
    }
}
