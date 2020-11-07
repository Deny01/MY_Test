using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HappyYF.Infrastructure.Repositories;
using DataGridFilter;

namespace HappyYF.YuanXin.WorkSet
{
    public partial class LY_SalesProduct_Sel : Form
    {

       public  string nowitemno = "";
       public  string nowitemname = "";
       public string nowitemxhc = "";
       public  string nowunit = "";
        
        
        public LY_SalesProduct_Sel()
        {
            InitializeComponent();
        }

        private void ly_lsptb_selBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.ly_lsptb_selBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.lYSalseMange);

        }

        private void LY_SalesProduct_Sel_Load(object sender, EventArgs e)
        {
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

        private void toolStripButton29_Click(object sender, EventArgs e)
        {
            this.ly_lsptb_selTableAdapter.Fill(this.lYSalseMange.ly_lsptb_sel);
        }

        private void ly_lsptb_selDataGridView_DoubleClick(object sender, EventArgs e)
        {
            if (null == this.ly_lsptb_selDataGridView.CurrentRow)
            {
                return;

            }

            this.nowitemno = this.ly_lsptb_selDataGridView.CurrentRow.Cells["物料编号2"].Value.ToString();
            this.nowitemname  = this.ly_lsptb_selDataGridView.CurrentRow.Cells["物料名称2"].Value.ToString();
            this.nowitemxhc = this.ly_lsptb_selDataGridView.CurrentRow.Cells["中方型号2"].Value.ToString();
            this.nowunit = this.ly_lsptb_selDataGridView.CurrentRow.Cells["单位2"].Value.ToString();

            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
