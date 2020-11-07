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
    public partial class LY_BorrowPersonal_Query : Form
    {
        private LY_SalseBorrow_Daily ownerForm;


        int nowplanid= 0;

        string nowitemno = "";

        string nowproduction_order = "";

        int nowprocess_order = 0;

        //int detail_Id=0;

        public LY_SalseBorrow_Daily OwnerForm
        {
            get { return ownerForm; }
            set { ownerForm = value; }
        }

        public LY_BorrowPersonal_Query()
        {
            InitializeComponent();
        }

        private void LY_Production_order_Mange_Load(object sender, EventArgs e)
        {

            this.tabPage1.Parent = null;
            this.tabPage2.Parent = null;
            
            this.ly_sales_borrow_NoexecuteBindingSource.Filter = "借用类别='02'";
            
            //this.ly_sales_contract_detailTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            this.ly_sales_borrow_detailTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            
            //this.ly_material_plan_main_forperiodTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            //this.ly_material_plan_main_forperiodTableAdapter.Fill(this.lYSalseMange2.ly_material_plan_main_forperiod);

            this.ly_sales_borrow_NoexecuteTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            this.ly_sales_borrow_NoexecuteTableAdapter.Fill(this.lYSalseMange2.ly_sales_borrow_Noexecute);

            //this.ly_sales_contract_main_forperiodTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            //this.ly_sales_contract_main_forperiodTableAdapter.Fill(this.lYSalseMange2.ly_sales_contract_main_forperiod);

            //this.Location = this.ownerForm.PointToScreen(new Point(this.ownerForm.Location .X+10 ,this .ownerForm .Location .Y+5 ));
            //this .Width =this .ownerForm.Width-20 ;
            //this .Height =360;

            //this.WindowState = FormWindowState.Maximized;
            
            this.dateTimePicker1.Text = DateTime.Today.AddMonths(-3).Date.ToString();
            this.dateTimePicker2.Text = DateTime.Today.AddDays(1).Date.ToString();

            this.dateTimePicker3.Text = DateTime.Today.AddMonths(-3).Date.ToString();
            this.dateTimePicker4.Text = DateTime.Today.AddDays(1).Date.ToString();

           

            //this.ly_production_recordsTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            //this.lY_productionorder_listTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;

            //this.lY_productionorder_listTableAdapter.Fill(this.lYQualityInspector.LY_productionorder_list, DateTime.Parse(this.dateTimePicker1.Text).Date, DateTime.Parse(this.dateTimePicker2.Text).Date);


           

        }

       


        private void toolStripTextBox2_Enter(object sender, EventArgs e)
        {
            toolStripTextBox2.Text = "";

            this.ly_sales_contract_main_forperiodBindingSource.Filter = "";
        }

        private void toolStripTextBox2_KeyUp(object sender, KeyEventArgs e)
        {
            string dFilter = "";

            dFilter = GetDataGridviewMultiFilterString.DGVMultiFilterString(this.ly_sales_contract_mainDataGridView, this.toolStripTextBox2.Text);

            if (null == dFilter)
                dFilter = "";

            this.ly_sales_contract_main_forperiodBindingSource.Filter = dFilter;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //this.lY_productionorder_listTableAdapter.Fill(this.lYQualityInspector.LY_productionorder_list, DateTime.Parse(this.dateTimePicker1.Text).Date, DateTime.Parse(this.dateTimePicker2.Text).Date);
        }

        private void lY_productionorder_listDataGridView_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (null == this.ly_sales_contract_mainDataGridView.CurrentRow)
            {

                return;

            }

            string nowinnerCode =ly_sales_contract_mainDataGridView.Rows [e.RowIndex].Cells["内部编码"].Value.ToString();
            string nowcontractCode = ly_sales_contract_mainDataGridView.Rows[e.RowIndex].Cells["合同编码"].Value.ToString();
          
            this.ly_sales_contract_detailTableAdapter.Fill(this.lYSalseMange.ly_sales_contract_detail, nowinnerCode, 0);


            //this.OwnerForm.find_NowContract(nowcontractCode, nowinnerCode);

            //////////////////////////////////////////



        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.ly_material_plan_main_forperiodTableAdapter.Fill(this.lYSalseMange2.ly_material_plan_main_forperiod);
        }

        private void toolStripTextBox4_KeyUp(object sender, KeyEventArgs e)
        {
            string dFilter = "";

            dFilter = GetDataGridviewMultiFilterString.DGVMultiFilterString(this.ly_material_plan_mainDataGridView, this.toolStripTextBox4.Text);

            if (null == dFilter)
                dFilter = "";

            this.ly_material_plan_main_forperiodBindingSource.Filter = dFilter;
        }

        private void toolStripTextBox4_Enter(object sender, EventArgs e)
        {
            toolStripTextBox4.Text = "";

            this.ly_material_plan_main_forperiodBindingSource.Filter = "";
        }

        private void ly_production_recordsDataGridView_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            
            
           ////recordsDataGridView.Rows[e.RowIndex].Cells["工序号"].Value.ToString ()))
           // {
           //      nowprocess_order = int.Parse(this.ly_production_recordsDataGridView.Rows[e.RowIndex].Cells["工序号"].Value.ToString());
           // }

           // //this.OwnerForm.find_NowProduc(nowplanid, nowitemno, nowproduction_order, nowprocess_order);

           // ///////////////////////////////////////////////

           
        }

      


        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //TabControl tc = sender as TabControl; 

            //if (this.tabPage1 == tc.SelectedTab)
            //{


            //    if (null == this.lY_productionorder_listDataGridView.CurrentRow)
            //    {


            //        return;


            //    }

            //     nowplanid = int.Parse(this.lY_productionorder_listDataGridView.CurrentRow.Cells["plan_id"].Value.ToString());

            //     nowitemno = this.lY_productionorder_listDataGridView.CurrentRow .Cells["物料编码"].Value.ToString();

            //     nowproduction_order = this.lY_productionorder_listDataGridView.CurrentRow.Cells["跟单编号"].Value.ToString();

            //    //this.OwnerForm.find_NowProduc(nowplanid, nowitemno, nowproduction_order);

            //    ////////////////////////////////////////////





           
            //}

            //if (this.tabPage2 == tc.SelectedTab)
            //{
            //    if (null == this.ly_production_recordsDataGridView.CurrentRow)
            //    {

                    
            //        return;
            //    }

            //     nowplanid = int.Parse(this.ly_production_recordsDataGridView.CurrentRow.Cells["plan_id2"].Value.ToString());

            //     nowitemno = this.ly_production_recordsDataGridView.CurrentRow.Cells["物料编号2"].Value.ToString();

            //     nowproduction_order = this.ly_production_recordsDataGridView.CurrentRow.Cells["跟单编号2"].Value.ToString();

            //     nowprocess_order = 0;

            //    if (!string.IsNullOrEmpty(this.ly_production_recordsDataGridView.CurrentRow.Cells["工序号"].Value.ToString()))
            //    {
            //        nowprocess_order = int.Parse(this.ly_production_recordsDataGridView.CurrentRow.Cells["工序号"].Value.ToString());
            //    }

            //    //this.OwnerForm.find_NowProduc(nowplanid, nowitemno, nowproduction_order, nowprocess_order);

            //    ///////////////////////////////////////////////

            
            //}
        }

      

     
      

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            //if (this.checkBox1.Checked)
            //{

            //    this.ly_production_recordsBindingSource.Filter = "";

            //}
            //else
            //{

            //    this.ly_production_recordsBindingSource.Filter = "处理=0";
            //}
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            this.ly_sales_contract_main_forperiodTableAdapter.Fill(this.lYSalseMange2.ly_sales_contract_main_forperiod);
        }

        private void ly_material_plan_mainDataGridView_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (null == this.ly_material_plan_mainDataGridView.CurrentRow)
            {

                return;

            }

            string nowplanCode = ly_material_plan_mainDataGridView.Rows[e.RowIndex].Cells["计划编号0"].Value.ToString();
            string nowcontractCode = ly_material_plan_mainDataGridView.Rows[e.RowIndex].Cells["内部编码0"].Value.ToString();



           // this.OwnerForm.find_NowPlan(nowcontractCode, nowplanCode);
        }

      
        private void ly_sales_borrowDataGridView_SelectionChanged(object sender, EventArgs e)
        {
            if (null == ly_sales_borrowDataGridView.CurrentRow)
            {
                


                return;
            }

            string nowborrowcode = ly_sales_borrowDataGridView.CurrentRow.Cells["借用单号"].Value.ToString();
            //this.nowclientCode = ly_sales_borrowDataGridView.CurrentRow.Cells["客户编码c"].Value.ToString();


            this.ly_sales_borrow_detailTableAdapter.Fill(this.lYSalseMange.ly_sales_borrow_detail, nowborrowcode);
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            this.ly_sales_borrow_NoexecuteTableAdapter.Fill(this.lYSalseMange2.ly_sales_borrow_Noexecute);
        }

        private void ly_sales_borrowDataGridView_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (null == this.ly_sales_borrowDataGridView.CurrentRow)
            {

                return;

            }

            string nowborrowCode = ly_sales_borrowDataGridView.Rows[e.RowIndex].Cells["借用单号"].Value.ToString();
            string nowcontractCode = ly_sales_borrowDataGridView.Rows[e.RowIndex].Cells["合同编号B"].Value.ToString();

            string nowclientCode = ly_sales_borrowDataGridView.Rows[e.RowIndex].Cells["客户编码br"].Value.ToString();



            this.OwnerForm.find_NowBorrow(nowclientCode, nowborrowCode);
        }

       

       
       



    }
}
