using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.Data.SqlClient;

using System.Transactions;

using HappyYF.Infrastructure.Repositories;



namespace HappyYF.YuanXin.WorkSet
{
    public partial class LY_sales_outquery : Form
    {
        private LY_Salescontract_Daily ownerForm;

        public LY_Salescontract_Daily OwnerForm
        {
            get { return ownerForm; }
            set { ownerForm = value; }
        }

        public LY_sales_outquery()
        {
            InitializeComponent();
        }

        private void ly_inform_recordBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            //this.Validate();
            //this.ly_inform_recordBindingSource1.EndEdit();
            //this.tableAdapterManager.UpdateAll(this.lYSalseRepair);

        }

       

        private void LY_sales_outInform_Load(object sender, EventArgs e)
        {

            this.dateTimePicker1.Text = DateTime.Today.AddDays(-7).Date.ToString();
            this.dateTimePicker2.Text = DateTime.Today.Date.ToString();


            //this.dateTimePicker6.Value.AddDays(1)

            this.ly_inform_queryforsalseTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            this.ly_inform_queryforsalseTableAdapter.Fill(this.lYSalseRepair.ly_inform_queryforsalse, SQLDatabase.nowUserName(),  this.dateTimePicker1.Value, this.dateTimePicker2.Value.AddDays(1));
        
           
        }

        private void ly_inform_recordDataGridView_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
           // return;
            
            DataGridView dgv = sender as DataGridView;

            if (null == dgv.CurrentRow) return;

            ///////////////////////////////////////////////////////////////////

            //if ("收到" == dgv.CurrentCell.OwningColumn.Name)
            //{



            //    if ("True" == dgv.CurrentRow.Cells["收到"].Value.ToString())
            //    {
            //        //dgv.CurrentRow.Cells["收到"].Value = "False";

            //        //dgv.CurrentRow.Cells["接收时间"].Value = DBNull.Value;
                 
            //    }
            //    else
            //    {

            //        dgv.CurrentRow.Cells["收到"].Value = "True";

            //        dgv.CurrentRow.Cells["接收时间"].Value = SQLDatabase.GetNowtime();
                  
            //    }

            //    this.Validate();
            //    this.ly_inform_noacceptBindingSource.EndEdit();

            //    this.ly_inform_noacceptTableAdapter.Update(this.informDataSet.ly_inform_noaccept);



            //    return;

            //}
            ////////////////////////////////////////////////////////////////////////合同编号
            if ("清单编号1" == dgv.CurrentCell.OwningColumn.Name)
            {

                if (!string.IsNullOrEmpty(dgv.CurrentRow.Cells["清单编号1"].Value.ToString()) && !string.IsNullOrEmpty(dgv.CurrentRow.Cells["合同编号1"].Value.ToString()))
                {
                    string nowbillnum = dgv.CurrentRow.Cells["清单编号1"].Value.ToString();
                    string nowcontractnum = dgv.CurrentRow.Cells["合同编号1"].Value.ToString();
                    this.ownerForm.Find_billlocation(nowbillnum, nowcontractnum);
                }
                return;

            }

           ////////////////////////////////////////////////////////////////////////
        }

        private void ly_inform_recordDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void ly_inform_queryforsalseBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.ly_inform_queryforsalseBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.lYSalseRepair);

        }

        //private void fillToolStripButton_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        this.ly_inform_queryforsalseTableAdapter.Fill(this.lYSalseRepair.ly_inform_queryforsalse, informcontentToolStripTextBox.Text, new System.Nullable<System.DateTime>(((System.DateTime)(System.Convert.ChangeType(begindateToolStripTextBox.Text, typeof(System.DateTime))))), new System.Nullable<System.DateTime>(((System.DateTime)(System.Convert.ChangeType(enddateToolStripTextBox.Text, typeof(System.DateTime))))));
        //    }
        //    catch (System.Exception ex)
        //    {
        //        System.Windows.Forms.MessageBox.Show(ex.Message);
        //    }

        //}

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
             this.ly_inform_queryforsalseTableAdapter.Fill(this.lYSalseRepair.ly_inform_queryforsalse, SQLDatabase.nowUserName(),  this.dateTimePicker1.Value, this.dateTimePicker2.Value.AddDays(1));
        
        }

        //private void fillToolStripButton_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        this.ly_inform_noacceptTableAdapter.Fill(this.informDataSet.ly_inform_noaccept, informcontentToolStripTextBox.Text);
        //    }
        //    catch (System.Exception ex)
        //    {
        //        System.Windows.Forms.MessageBox.Show(ex.Message);
        //    }

        //}

       
    }
}
