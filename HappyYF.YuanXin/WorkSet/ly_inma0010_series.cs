using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HappyYF.Infrastructure.Repositories;
using System.Transactions;
using DataGridFilter;


namespace HappyYF.YuanXin.WorkSet
{
    public partial class ly_inma0010_series : Form
    {
        List<string> itemlist = new List<string>();

        public ly_inma0010_series()
        {
            InitializeComponent();
        }
 

        private void LY_MaterialBom_Load(object sender, EventArgs e)
        {
     
            this.ly_inma0010_seriesTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;

            // TODO: 这行代码将数据加载到表“lYMaterialMange.ly_inma0010_series”中。您可以根据需要移动或删除它。
            this.ly_inma0010_seriesTableAdapter.Fill(this.lYMaterialMange.ly_inma0010_series);


        }

        private void toolStripTextBox1_Enter(object sender, EventArgs e)
        {
            toolStripTextBox1.Text = "";

            this.lyinma0010seriesBindingSource.Filter = "";

      
        }
        
        private void toolStripTextBox1_KeyUp(object sender, KeyEventArgs e)
        {
            string filterString;


            filterString = GetDataGridviewMultiFilterString.DGVMultiFilterString(this.ly_inma0010DataGridView, this.toolStripTextBox1.Text);
            this.lyinma0010seriesBindingSource.Filter = "(" + filterString + ")";



        }

       
 

       

      

        private void bindingNavigatorAddNewItem_Click(object sender, EventArgs e)
        {
            seriesAdd queryForm = new seriesAdd();

            queryForm.type_code = "";
            queryForm.runmode = "增加"; 

            queryForm.StartPosition = FormStartPosition.CenterParent;
            queryForm.ShowDialog();

            if (queryForm.DialogResult != DialogResult.Cancel)
            {
                this.ly_inma0010_seriesTableAdapter.Fill(this.lYMaterialMange.ly_inma0010_series);
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if (null == ly_inma0010DataGridView.CurrentRow) return;

           

            string s = this.ly_inma0010DataGridView.CurrentRow.Cells["编码"].Value.ToString();

            seriesAdd queryForm = new seriesAdd();
             
            queryForm.runmode = "修改";
            queryForm.type_code = s;

            queryForm.StartPosition = FormStartPosition.CenterParent;
            queryForm.ShowDialog();

            if (queryForm.DialogResult != DialogResult.Cancel)
            {
                this.ly_inma0010_seriesTableAdapter.Fill(this.lYMaterialMange.ly_inma0010_series);
            }

        }
 
        private void bindingNavigatorDeleteItem_Click(object sender, EventArgs e)
        {
            if (ly_inma0010DataGridView.CurrentRow == null)
                return;
            string message = "确定删除当前记录吗？";
            string caption = "提示...";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result; 
            
            result = MessageBox.Show(message, caption, buttons,
            MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

            if (result == DialogResult.Yes)
            {
                try
                {

                    this.lyinma0010seriesBindingSource.RemoveCurrent();
                    ly_inma0010DataGridView.EndEdit();
                    lyinma0010seriesBindingSource.EndEdit();
                    this.ly_inma0010_seriesTableAdapter.Update(this.lYMaterialMange.ly_inma0010_series);
                }
                catch (SqlException ex)
                {

                    MessageBox.Show(ex.Message.ToString(), "注意");
                }

            }
        }

        private void toolStripButton16_Click(object sender, EventArgs e)
        {
            ExportDataGridviewTOExcellA.ExportDataGridview(this.ly_inma0010DataGridView, true);
        }

     

      




    }
}
