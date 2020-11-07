using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

using System.Transactions;

using HappyYF.Infrastructure.Repositories;
using DataGridFilter;

namespace HappyYF.YuanXin.WorkSet
{


    public partial class LY_repair_query : Form
    {

 
        public LY_repair_query()
        {
            InitializeComponent();
        }

 


        private void toolStripButton31_Click(object sender, EventArgs e)
        {
            NewFrm.Show(this);
            this.ly_repair_queryRepTableAdapter.Fill(this.lYSalseMange2.ly_repair_queryRep, this.dateTimePicker7.Value, this.dateTimePicker8.Value.AddDays(1));
            NewFrm.Hide(this);
        }

        private void LY_Runing_DashBoard_Load(object sender, EventArgs e)
        {

            ly_repair_queryTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            ly_inma0010cpTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            this.ly_repair_queryRepTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            this.dateTimePicker7.Text = DateTime.Today.AddMonths(-1).Date.ToString();
            this.dateTimePicker8.Text = DateTime.Today.AddDays(0).Date.ToString();

            // TODO: 这行代码将数据加载到表“lYMaterialMange.ly_inma0010cp”中。您可以根据需要移动或删除它。
            this.ly_inma0010cpTableAdapter.Fill(this.lYMaterialMange.ly_inma0010cp);
            // TODO: 这行代码将数据加载到表“lYMaterielRequirements.bom_material_for_machine_base”中。您可以根据需要移动或删除它。

            // TODO: 这行代码将数据加载到表“lYSalseMange2.ly_repair_query”中。您可以根据需要移动或删除它。
            this.ly_repair_queryTableAdapter.Fill(this.lYSalseMange2.ly_repair_query);

        }

        private void toolStripButton27_Click(object sender, EventArgs e)
        {
            ExportDataGridviewTOExcellA.ExportDataGridview(this.dataGridView1, true);
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            string message1 = "当前记录将被删除，继续吗？";
            string caption1 = "提示...";
            MessageBoxButtons buttons1 = MessageBoxButtons.YesNo;

            DialogResult result;



            result = MessageBox.Show(message1, caption1, buttons1,
            MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

            if (result == DialogResult.Yes)
            {

                this.lyrepairqueryBindingSource.RemoveCurrent();

                SaveChanged();
            }
        }

        private void SaveChanged()
        {

            this.ly_borrow_store_Grid.EndEdit();
            this.lyrepairqueryBindingSource.EndEdit();

            this.ly_repair_queryTableAdapter.Update(this.lYSalseMange2.ly_repair_query);
            this.ly_repair_queryTableAdapter.Fill(this.lYSalseMange2.ly_repair_query); 

        }

        private void bom_material_for_machine_baseDataGridView_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (null == this.bom_material_for_machine_baseDataGridView.CurrentRow) return;



            string itemno = this.bom_material_for_machine_baseDataGridView.CurrentRow.Cells["物资编号"].Value.ToString();

            
            this.lyrepairqueryBindingSource.AddNew();

            this.ly_borrow_store_Grid.CurrentRow.Cells["物料号"].Value = itemno; 
            SaveChanged();
        }

        private void toolStripTextBox1_KeyUp(object sender, KeyEventArgs e)
        {
            string filterString = GetDataGridviewMultiFilterString.DGVMultiFilterString(this.bom_material_for_machine_baseDataGridView, this.toolStripTextBox1.Text);



            if (null == filterString)
                filterString = "";

            this.lyinma0010cpBindingSource.Filter = filterString;
        }

        private void toolStripTextBox1_Enter(object sender, EventArgs e)
        {
            toolStripTextBox1.Text = "";

            this.lyinma0010cpBindingSource.Filter = "";//bom_material_sel_replace
        }
    }
}