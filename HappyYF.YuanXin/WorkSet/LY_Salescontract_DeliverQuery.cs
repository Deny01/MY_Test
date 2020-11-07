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
    public partial class LY_Salescontract_DeliverQuery : Form
    {
        private LY_Salescontract_Deliver ownerForm;


        int nowid= 0;

        string nowdelivercode = "";

        string nowgroup = "";

        string nowplan = "";

       

        //int detail_Id=0;

        public LY_Salescontract_Deliver OwnerForm
        {
            get { return ownerForm; }
            set { ownerForm = value; }
        }

        public LY_Salescontract_DeliverQuery()
        {
            InitializeComponent();
        }

        private void LY_Production_order_Mange_Load(object sender, EventArgs e)
        {
            
            //this.Location = this.ownerForm.PointToScreen(new Point(this.ownerForm.Location .X+10 ,this .ownerForm .Location .Y+5 ));
            //this .Width =this .ownerForm.Width-20 ;
            //this .Height =360;

            this.WindowState = FormWindowState.Maximized;
            
            this.dateTimePicker1.Text = DateTime.Today.AddDays(-3).Date.ToString();
            this.dateTimePicker2.Text = DateTime.Today.AddDays(1).Date.ToString();

            this.ly_sales_deliver_detail1TableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            this.ly_sales_deliver_detail1TableAdapter.Fill(this.lYSalseMange.ly_sales_deliver_detail1, DateTime.Parse(this.dateTimePicker1.Text).Date, DateTime.Parse(this.dateTimePicker2.Text).Date);


            ///////////////////////
            DataGridViewColumnSelector cs = new DataGridViewColumnSelector(ly_sales_deliver_detail1DataGridView, this.Text);
            cs.MaxHeight = 180;
            cs.Width = 800;

            cs.Set_dgvColumns();
            ////////////////////////////////



           

        }

       


        private void toolStripTextBox2_Enter(object sender, EventArgs e)
        {
            toolStripTextBox2.Text = "";

            this.ly_sales_deliver_detail1BindingSource.Filter = "";
        }

        private void toolStripTextBox2_KeyUp(object sender, KeyEventArgs e)
        {
            string dFilter = "";

            dFilter = GetDataGridviewMultiFilterString.DGVMultiFilterString(this.ly_sales_deliver_detail1DataGridView, this.toolStripTextBox2.Text);

            if (null == dFilter)
                dFilter = "";

            this.ly_sales_deliver_detail1BindingSource.Filter = dFilter;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.ly_sales_deliver_detail1TableAdapter.Fill(this.lYSalseMange.ly_sales_deliver_detail1, DateTime.Parse(this.dateTimePicker1.Text).Date, DateTime.Parse(this.dateTimePicker2.Text).Date);

        }

       

        private void ly_sales_test_detail1DataGridView_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (null == this.ly_sales_deliver_detail1DataGridView.CurrentRow)
            {



                return;


            }



            nowid = int.Parse(this.ly_sales_deliver_detail1DataGridView.Rows[e.RowIndex].Cells["id"].Value.ToString());

            nowdelivercode= this.ly_sales_deliver_detail1DataGridView.Rows[e.RowIndex].Cells["发货单号"].Value.ToString();

            nowgroup = this.ly_sales_deliver_detail1DataGridView.Rows[e.RowIndex].Cells["配套号"].Value.ToString();

            nowplan = this.ly_sales_deliver_detail1DataGridView.Rows[e.RowIndex].Cells["依赖书号"].Value.ToString();

            this.OwnerForm.find_NowProduc(nowplan, nowgroup, nowdelivercode, nowid);

            ////////////////////////////////////////////

        }

        private void toolStripButton8_Click(object sender, EventArgs e)
        {
            ExportDataGridviewTOExcell.ExportDataGridview(this.ly_sales_deliver_detail1DataGridView, true);
        }

        
      

       
       

       

       



    }
}
