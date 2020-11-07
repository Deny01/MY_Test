using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using DataGridFilter;
using HappyYF.Infrastructure.Repositories;
 
 

namespace HappyYF.YuanXin.WorkSet
{
    public partial class LY_Quality_qzdz_Rep : Form
    {
        public LY_Quality_qzdz_Rep()
        {
            InitializeComponent();
        }

       

        private void button1_Click(object sender, EventArgs e)
        {
            bind();
        }

        private void LY_Quality_Control_PurchaseRep_Load(object sender, EventArgs e)
        {
            this.dateTimePicker1.Text = DateTime.Today.AddMonths(-1).Date.ToString();
            this.dateTimePicker2.Text = DateTime.Today.Date.ToString();
            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 0;
            //////////////////////////////////////////////////////////////////////////////////////////

            this.qz_dz_reportTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            qz_dz_report_TotalTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
        }

        private void toolStripButton10_Click(object sender, EventArgs e)
        {
            ExportDataGridviewTOExcellA.ExportDataGridview(this.ly_purchase_contract_inspectionRepDataGridView, true);
        }

       

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {


            bind();
        }
        protected void bind()
        {
            this.qz_dz_reportTableAdapter.Fill(this.lYQualityInspector.qz_dz_report, DateTime.Parse(this.dateTimePicker1.Text).Date,
                           DateTime.Parse(this.dateTimePicker2.Text).Date.AddDays(1));

            this.qz_dz_report_TotalTableAdapter.Fill(this.lYQualityInspector.qz_dz_report_Total, DateTime.Parse(this.dateTimePicker1.Text).Date,
                           DateTime.Parse(this.dateTimePicker2.Text).Date.AddDays(1));

            string tyle = ((comboBox2.SelectedIndex == 0 || comboBox2.SelectedIndex == -1)? "" : (comboBox2.SelectedIndex == 1 ? "QZ" : "DZ"));
            string category = ((comboBox1.SelectedIndex == 0 || comboBox1.SelectedIndex == -1)?  "": (comboBox1.SelectedIndex == 1 ? "半成品" : "成品"));
            if (tyle == "" && category == "")
            {
                this.qzdzreportBindingSource.Filter = "";

            }

            else
            {
                if (tyle != "" && category != "")
                {
                    this.qzdzreportBindingSource.Filter = " category='" + category + "' and pruductionOrder_num like  '" + tyle + "%' ";

                }
                else
                {
                    if (category == "")
                    { }
                    else
                    {
                        this.qzdzreportBindingSource.Filter = " category='" + category + "'";
                    }
                    if (tyle == "")
                    { }
                    else
                    {
                        this.qzdzreportBindingSource.Filter = "  pruductionOrder_num like  '" + tyle + "%'";
                    }
                }
            }
        }
        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            bind();


        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if (null == this.ly_purchase_contract_inspectionRepDataGridView.CurrentRow) return;

            FilterForm filterForm = new FilterForm();

             

            List<string> ls = new List<string>();
            ls.Add("id");


            filterForm.SetSourceColumns(this.lYQualityInspector.qz_dz_report.Columns, ls);

            filterForm.ShowDialog();

            this.qzdzreportBindingSource.Filter = filterForm.GetFilterString();
        }

        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            ExportDataGridviewTOExcellA.ExportDataGridview(this.dataGridView1, true);
        }
    }
}
