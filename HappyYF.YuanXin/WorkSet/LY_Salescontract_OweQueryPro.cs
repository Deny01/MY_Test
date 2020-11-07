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
    public partial class LY_Salescontract_OweQueryPro : Form
    {
        private LY_Salescontract_GroupPro ownerForm;


        int nowplanid= 0;

        string nowitemno = "";

        string nowproduction_order = "";

        int nowprocess_order = 0;

        //int detail_Id=0;

        public LY_Salescontract_GroupPro OwnerForm
        {
            get { return ownerForm; }
            set { ownerForm = value; }
        }

        public LY_Salescontract_OweQueryPro()
        {
            InitializeComponent();
            this.ly_plan_getmaterial_departmentTableAdapter1.CommandTimeout = 0;
        }

        private void LY_Production_order_Mange_Load(object sender, EventArgs e)
        {

            this.ly_plan_getmaterial_departmentTableAdapter1.Connection.ConnectionString = SQLDatabase.Connectstring;

            this.ly_plan_getmaterial_departmentBindingSource1.Filter = "未领数量 > 0";
            
            //this.ly_material_plan_main_forperiodTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            //this.ly_material_plan_main_forperiodTableAdapter.Fill(this.lYSalseMange2.ly_material_plan_main_forperiod);
            //this.ly_sales_contract_main_forperiodTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            //this.ly_sales_contract_main_forperiodTableAdapter.Fill(this.lYSalseMange2.ly_sales_contract_main_forperiod);

            //this.Location = this.ownerForm.PointToScreen(new Point(this.ownerForm.Location .X+10 ,this .ownerForm .Location .Y+5 ));
            //this .Width =this .ownerForm.Width-20 ;
            //this .Height =360;

            //this.WindowState = FormWindowState.Maximized;
            
            this.dateTimePicker1.Text = DateTime.Today.AddMonths(-3).Date.ToString();
            this.dateTimePicker2.Text = DateTime.Today.AddDays(1).Date.ToString();

         

        }

       


        private void toolStripTextBox2_Enter(object sender, EventArgs e)
        {
            toolStripTextBox2.Text = "";

            this.ly_plan_getmaterial_departmentBindingSource1.Filter = "";
        }

        private void toolStripTextBox2_KeyUp(object sender, KeyEventArgs e)
        {
            string dFilter = "";
            string con_dFilter = "";

            if (this.checkBox1.Checked)
            {
                con_dFilter = " and 1=1";
            }
            else
            {
                con_dFilter = " and 未领数量 > 0";
            }

            dFilter = GetDataGridviewMultiFilterString.DGVMultiFilterString(this.dataGridView1, this.toolStripTextBox2.Text);

            if (null == dFilter)
                dFilter = "1=1";

            this.ly_plan_getmaterial_departmentBindingSource1.Filter ="("+ dFilter+ ")"+ con_dFilter;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //this.lY_productionorder_listTableAdapter.Fill(this.lYQualityInspector.LY_productionorder_list, DateTime.Parse(this.dateTimePicker1.Text).Date, DateTime.Parse(this.dateTimePicker2.Text).Date);
        }

        //private void lY_productionorder_listDataGridView_RowEnter(object sender, DataGridViewCellEventArgs e)
        //{
        //    if (null == this.ly_sales_contract_mainDataGridView.CurrentRow)
        //    {

        //        return;

        //    }

        //    string nowinnerCode =ly_sales_contract_mainDataGridView.Rows [e.RowIndex].Cells["内部编码"].Value.ToString();
        //    string nowcontractCode = ly_sales_contract_mainDataGridView.Rows[e.RowIndex].Cells["合同编码"].Value.ToString();
          
        //    this.ly_sales_contract_detailTableAdapter.Fill(this.lYSalseMange.ly_sales_contract_detail, nowinnerCode, 0);


        //    this.OwnerForm.find_NowContract(nowcontractCode, nowinnerCode);

        //    //////////////////////////////////////////



        //}

      

       

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
            NewFrm.Show(this );
            
            if (this.radioButton2.Checked)
            {
                this.ly_plan_getmaterial_departmentTableAdapter1.Fill(this.lYSalseMange2.ly_plan_getmaterial_department, "singned", this.dateTimePicker1.Value, this.dateTimePicker2.Value);
            }
            else
            {
                this.ly_plan_getmaterial_departmentTableAdapter1.Fill(this.lYSalseMange2.ly_plan_getmaterial_department, "runed", this.dateTimePicker1.Value, this.dateTimePicker2.Value);
            
            }
            NewFrm.Hide(this);
        }

        private void checkBox1_CheckedChanged_1(object sender, EventArgs e)
        {
            if (this.checkBox1.Checked)
            {
                this.ly_plan_getmaterial_departmentBindingSource1.Filter = "";
            }
            else
            {
                this.ly_plan_getmaterial_departmentBindingSource1.Filter = "未领数量 > 0";
            }
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            ExportDataGridviewTOExcell.ExportDataGridview(dataGridView1, true);
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if (null == this.dataGridView1.CurrentRow) return;

            FilterForm filterForm = new FilterForm();

            //SumQueryDataSet qds;
            //qds = new SumQueryDataSet();

            List<string> ls = new List<string>();
            ls.Add("id");

           
            filterForm.SetSourceColumns(this.lYSalseMange2.ly_plan_getmaterial_department.Columns, ls);

            filterForm.ShowDialog();

            this.ly_plan_getmaterial_departmentBindingSource1.Filter = filterForm.GetFilterString();
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            if (null == this.dataGridView1.CurrentRow) return;
            SortForm DataSort = new SortForm();

            List<string> ls = new List<string>();
            ls.Add("id");


            DataSort.SetSortColumns(this.lYSalseMange2.ly_plan_getmaterial_department.Columns, ls);
            DataSort.ShowDialog();

            this.ly_plan_getmaterial_departmentBindingSource1.Sort =  DataSort.GetSortString() ;
        }

        //private void fillToolStripButton_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        this.ly_plan_getmaterial_departmentTableAdapter.Fill(this.lYSalseMange.ly_plan_getmaterial_department, plan_numToolStripTextBox.Text);
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
        //        this.ly_plan_getmaterial_departmentTableAdapter1.Fill(this.lYSalseMange2.ly_plan_getmaterial_department, plan_numToolStripTextBox.Text);
        //    }
        //    catch (System.Exception ex)
        //    {
        //        System.Windows.Forms.MessageBox.Show(ex.Message);
        //    }

        //}

        //private void ly_material_plan_mainDataGridView_RowEnter(object sender, DataGridViewCellEventArgs e)
        //{
        //    if (null == this.ly_material_plan_mainDataGridView.CurrentRow)
        //    {

        //        return;

        //    }

        //    string nowplanCode = ly_material_plan_mainDataGridView.Rows[e.RowIndex].Cells["计划编号0"].Value.ToString();
        //    string nowcontractCode = ly_material_plan_mainDataGridView.Rows[e.RowIndex].Cells["内部编码0"].Value.ToString();



        //    this.OwnerForm.find_NowPlan(nowcontractCode, nowplanCode);
        //}

      
       



    }
}
