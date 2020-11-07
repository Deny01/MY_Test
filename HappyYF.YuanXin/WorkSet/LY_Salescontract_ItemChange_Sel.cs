using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Transactions;
using System.Windows.Forms;
using HappyYF.Infrastructure.Repositories;
using DataGridFilter;


 namespace HappyYF.YuanXin.WorkSet
{
    public partial class LY_Salescontract_ItemChange_Sel : Form
    {
       
              
        public  string nowitemno = "";
        public  string nowitemname= "";
        public string nowitemxh = "";


        public LY_Salescontract_ItemChange_Sel()
        {
            InitializeComponent();
        }

      

       
        private void Yonghu_Load(object sender, EventArgs e)
        {
            this.ly_inma0010fjTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            this.ly_inma0010fjTableAdapter.Fill(this.lYMaterialMange.ly_inma0010fj);


            this.ly_lsptb_selTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            this.ly_lsptb_selTableAdapter.Fill(this.lYSalseMange.ly_lsptb_sel);

          
        }

     
       
        private void toolStripTextBox6_KeyUp(object sender, KeyEventArgs e)
        {
            string filterString;


            filterString = GetDataGridviewMultiFilterString.DGVMultiFilterString(this.ly_lsptb_selDataGridView, this.toolStripTextBox6.Text);


            this.ly_lsptb_selBindingSource.Filter = filterString;
        }

        private void toolStripTextBox6_Enter(object sender, EventArgs e)
        {
            toolStripTextBox6.Text = "";

            this.ly_lsptb_selBindingSource.Filter = "";
        }

        private void toolStripTextBox1_KeyUp(object sender, KeyEventArgs e)
        {
            string filterString;


            filterString = GetDataGridviewMultiFilterString.DGVMultiFilterString(this.ly_inma0010DataGridView, this.toolStripTextBox1.Text);


            this.ly_inma0010fjBindingSource.Filter = filterString;
        }

        private void toolStripTextBox1_Enter(object sender, EventArgs e)
        {
            toolStripTextBox1.Text = "";

            this.ly_inma0010fjBindingSource.Filter = "";
        }

        private void toolStripButton3_Click_1(object sender, EventArgs e)
        {
            this.ly_inma0010fjTableAdapter.Fill(this.lYMaterialMange.ly_inma0010fj);

        }

        private void toolStripButton29_Click(object sender, EventArgs e)
        {
            this.ly_lsptb_selTableAdapter.Fill(this.lYSalseMange.ly_lsptb_sel);
        }

        private void ly_lsptb_selDataGridView_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (null == ly_lsptb_selDataGridView.CurrentRow) return;




            this.nowitemno = ly_lsptb_selDataGridView.CurrentRow.Cells["物料编号2"].Value.ToString();
            this.nowitemname = ly_lsptb_selDataGridView.CurrentRow.Cells["物料名称2"].Value.ToString();
            this.nowitemxh = ly_lsptb_selDataGridView.CurrentRow.Cells["中方型号2"].Value.ToString();

            this.Close();

        }

        private void ly_inma0010DataGridView_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (null == ly_inma0010DataGridView.CurrentRow) return;


            this.nowitemno = ly_inma0010DataGridView.CurrentRow.Cells["物资编号"].Value.ToString();
            this.nowitemname = ly_inma0010DataGridView.CurrentRow.Cells["名称"].Value.ToString();
            this.nowitemxh = ly_inma0010DataGridView.CurrentRow.Cells["中方型号"].Value.ToString();

            this.Close();
        }

      
       
    }
}
