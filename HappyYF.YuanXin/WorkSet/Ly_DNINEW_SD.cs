using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using HappyYF.Infrastructure.Repositories;

namespace HappyYF.YuanXin.WorkSet
{
    public partial class Ly_DNINEW_SD : Form
    {
        public Ly_DNINEW_SD()
        {
            InitializeComponent();
        }

       
 

        private void LY_Quality_Control_PurchaseRep_Load(object sender, EventArgs e)
        {
            loaddata();

            this.dNI_ByTimeTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            dNI_ByTimeTableAdapter.CommandTimeout = 0;
        }
        protected void loaddata()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add("Id", typeof(int));
            dt.Columns.Add("Name", typeof(string));

            dt.Rows.Add(new object[] { 2020, "2020" });
            dt.Rows.Add(new object[] { 2021, "2021" });
            dt.Rows.Add(new object[] { 2022, "2022" }); 


            comboBox1.DisplayMember = "Name";
            comboBox1.ValueMember = "Id"; 
            comboBox1.DataSource = dt;
            comboBox1.SelectedIndex = 0;


            DataTable dt2 = new DataTable();

            dt2.Columns.Add("Id", typeof(int));
            dt2.Columns.Add("Name", typeof(string));
               
            dt2.Rows.Add(new object[] { 1, "1" });
            dt2.Rows.Add(new object[] { 2, "2" });
            dt2.Rows.Add(new object[] { 3, "3" });
            dt2.Rows.Add(new object[] { 4, "4" });
            dt2.Rows.Add(new object[] { 5, "5" });
            dt2.Rows.Add(new object[] { 6, "6" });
            dt2.Rows.Add(new object[] { 7, "7" });
            dt2.Rows.Add(new object[] { 8, "8" });
            dt2.Rows.Add(new object[] { 9, "9" });
            dt2.Rows.Add(new object[] { 10, "10" });
            dt2.Rows.Add(new object[] { 11, "11" });
            dt2.Rows.Add(new object[] { 12, "12" });

            comboBox2.DisplayMember = "Name";
            comboBox2.ValueMember = "Id";
            comboBox2.DataSource = dt2;
            comboBox2.SelectedIndex = 0;
        }
        private void toolStripButton10_Click(object sender, EventArgs e)
        {
            ExportDataGridviewTOExcellA.ExportDataGridview(this.ly_purchase_contract_inspectionRepDataGridView, true);
        }

        
        private void toolStripTextBox3_Enter(object sender, EventArgs e)
        {
            toolStripTextBox3.Text = "";

            this.dNIByTimeBindingSource.Filter = "";
        }

        private void toolStripTextBox3_KeyUp(object sender, KeyEventArgs e)
        {
            string filterString;


            filterString = GetDataGridviewMultiFilterString.DGVMultiFilterString(this.ly_purchase_contract_inspectionRepDataGridView, this.toolStripTextBox3.Text);


            this.dNIByTimeBindingSource.Filter = filterString;
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            string year = comboBox1.SelectedValue.ToString();
            string month= comboBox2.SelectedValue.ToString();

            DateTime dt = DateTime.Parse(year + "-" + month + "-01");
            DateTime dt2 = dt.AddMonths(1);
            NewFrm.Show(this);
            Thread.Sleep(1000);
            this.dNI_ByTimeTableAdapter.Fill(this.lYQualityInspector.DNI_ByTime, dt, dt2);
            NewFrm.Hide(this);
        }
    }
}
