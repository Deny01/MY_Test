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



namespace TGZJ
{
    public partial class LY_sales_outInform : Form
    {
        private MainForm ownerForm;

        public MainForm OwnerForm
        {
            get { return ownerForm; }
            set { ownerForm = value; }
        }

        public LY_sales_outInform()
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
            this.ly_inform_recordTableAdapter1.Connection.ConnectionString = SQLDatabase.Connectstring;
            this.ly_inform_recordTableAdapter1.Fill(this.informDataSet.ly_inform_record, SQLDatabase.NowUserID);

           
        }

        private void ly_inform_recordDataGridView_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            DataGridView dgv = sender as DataGridView;

            if (null == dgv.CurrentRow) return;

            /////////////////////////////////////////////////////////////////

            if ("收到" == dgv.CurrentCell.OwningColumn.Name)
            {



                if ("True" == dgv.CurrentRow.Cells["收到"].Value.ToString())
                {
                    //dgv.CurrentRow.Cells["收到"].Value = "False";

                    //dgv.CurrentRow.Cells["接收时间"].Value = DBNull.Value;
                 
                }
                else
                {

                    dgv.CurrentRow.Cells["收到"].Value = "True";

                    dgv.CurrentRow.Cells["接收时间"].Value = SQLDatabase.GetNowtime();
                  
                }

                this.Validate();
                this.ly_inform_recordBindingSource1.EndEdit();
              
                this.ly_inform_recordTableAdapter1.Update (this.informDataSet.ly_inform_record);



                return;

            }
            ////////////////////////////////////////////////////////////////////////goto_Salescontract_Deliver
            if ("依赖书号" == dgv.CurrentCell.OwningColumn.Name)
            {

                if (!string.IsNullOrEmpty(dgv.CurrentRow.Cells["依赖书号"].Value.ToString()))
                {
                    string nowplannum = dgv.CurrentRow.Cells["依赖书号"].Value.ToString();
                    string nowinnercode = dgv.CurrentRow.Cells["清单编号"].Value.ToString();
                    this.ownerForm.goto_Salescontract_Deliver(nowplannum, nowinnercode);
                }
                return;

            }

           ////////////////////////////////////////////////////////////////////////
        }

        private void ly_inform_recordDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

       
    }
}
