using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.Data.SqlClient;

using HappyYF.Infrastructure.Repositories;
using System.Transactions;
using DataGridFilter;

namespace HappyYF.YuanXin.WorkSet
{
    public partial class LY_Drawing_ManageRec : Form
    {
        public LY_Drawing_ManageRec()
        {
            InitializeComponent();
        }

        //private void fillToolStripButton_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        this.ly_drawing_recordTableAdapter.Fill(this.lYMaterialMange.ly_drawing_record, new System.Nullable<System.DateTime>(((System.DateTime)(System.Convert.ChangeType(begindateToolStripTextBox.Text, typeof(System.DateTime))))), new System.Nullable<System.DateTime>(((System.DateTime)(System.Convert.ChangeType(enddateToolStripTextBox.Text, typeof(System.DateTime))))));
        //    }
        //    catch (System.Exception ex)
        //    {
        //        System.Windows.Forms.MessageBox.Show(ex.Message);
        //    }

        //}

        

        private void LY_Drawing_ManageRec_Load(object sender, EventArgs e)
        {
            // TODO: 这行代码将数据加载到表“lYMaterialMange.ly_inma0010cp”中。您可以根据需要移动或移除它。
            this.ly_inma0010cpTableAdapter.Fill(this.lYMaterialMange.ly_inma0010cp);
            
            
            this.ly_drawing_recordTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            this.ly_drawing_record1TableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;

            this.dateTimePicker1.Text = DateTime.Today.AddMonths(-6).Date.ToString();
            this.dateTimePicker2.Text = DateTime.Today.AddDays(1).Date.ToString();
        }

        private void toolStripButton28_Click(object sender, EventArgs e)
        {
            this.ly_drawing_recordTableAdapter.Fill(this.lYMaterialMange.ly_drawing_record, this.dateTimePicker1.Value, this.dateTimePicker2.Value);
        }

        private void ly_inma0010DataGridView_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (null == ly_inma0010DataGridView.CurrentRow) return;
            //string s = this.ly_inma0010DataGridView.CurrentRow.Cells["物资编号"].Value.ToString();
            string s = this.ly_inma0010DataGridView.Rows[e.RowIndex].Cells["物资编号"].Value.ToString();

            this.ly_drawing_record1TableAdapter.Fill(this.lYMaterialMange.ly_drawing_record1, s);

        }

        private void toolStripTextBox1_KeyUp(object sender, KeyEventArgs e)
        {
            string dFilter = "";

            dFilter = GetDataGridviewMultiFilterString.DGVMultiFilterString(this.ly_inma0010DataGridView, this.toolStripTextBox1.Text);

            if (null == dFilter)
                dFilter = "";

            this.ly_inma0010cpBindingSource.Filter = dFilter;
        }

        private void toolStripTextBox1_Enter(object sender, EventArgs e)
        {
            toolStripTextBox1.Text = "";

            this.ly_inma0010cpBindingSource.Filter = "";
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            ExportDataGridviewTOExcell.ExportDataGridview(this.ly_drawing_recordDataGridView, true);
        }
    }
}
