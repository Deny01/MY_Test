using HappyYF.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HappyYF.YuanXin.WorkSet
{
    public partial class machine_query : Form
    {
        public machine_query()
        {
            InitializeComponent();
            this.ly_production_detail_viewTableAdapter.CommandTimeout = 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            NewFrm.Show(this);
            this.ly_production_detail_viewTableAdapter.Fill(this.lYProductMange.ly_production_detail_view, this.dateTimePicker1.Value, this.dateTimePicker2.Value.AddDays(1));
            NewFrm.Hide(this);
        }

        private void machine_query_Load(object sender, EventArgs e)
        {
            this.dateTimePicker1.Text = DateTime.Today.AddMonths(-1).ToString();
            this.dateTimePicker2.Text = DateTime.Today.AddDays(1).Date.ToString();

            this.ly_production_detail_viewTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            NewFrm.Show(this);
            this.ly_production_detail_viewTableAdapter.Fill(this.lYProductMange.ly_production_detail_view, this.dateTimePicker1.Value, this.dateTimePicker2.Value.AddDays(1));
            NewFrm.Hide(this);
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            ExportDataGridviewTOExcellA.ExportDataGridview(this.ly_production_detail_viewDataGridView, true);
        }

        private void toolStripTextBox1_Enter(object sender, EventArgs e)
        {
            toolStripTextBox1.Text = "";

            this.lyproductiondetailviewBindingSource.Filter = "";
        }

        private void toolStripTextBox1_KeyUp(object sender, KeyEventArgs e)
        {
            string filterString;


            filterString = GetDataGridviewMultiFilterString.DGVMultiFilterString(this.ly_production_detail_viewDataGridView, this.toolStripTextBox1.Text);


            this.lyproductiondetailviewBindingSource.Filter = filterString;
        }
    }
}
