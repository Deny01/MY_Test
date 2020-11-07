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
    public partial class LY_Material_apprend : Form
    {

        string parentNum = "noSet";

        public LY_Material_apprend()
        {
            InitializeComponent();
        }

        private void ly_inma0010BindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.ly_inma0010fjBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.lYMaterialMange);

        }

        private void LY_MaterialBom_Load(object sender, EventArgs e)
        {
           
           
            this.ly_prod_deptTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            this.ly_prod_deptTableAdapter.Fill(this.lYMaterialMange.ly_prod_dept);

          
            this.ly_inma0010fjTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;

            this.ly_inma0010fjTableAdapter.Fill(this.lYMaterialMange.ly_inma0010fj);

        }

        private void toolStripTextBox1_Enter(object sender, EventArgs e)
        {
            toolStripTextBox1.Text = "";

            this.ly_inma0010fjBindingSource.Filter = "";
        }
        
        private void toolStripTextBox1_KeyUp(object sender, KeyEventArgs e)
        {
            string dFilter = "";

            //for (int i = 0; i < this.hT_Vehicle_collectDataGridView.ColumnCount; i++)
            for (int i = 1; i < 10; i++)
            {
                string tempColumnName = this.ly_inma0010DataGridView.Columns[i].DataPropertyName;

                if (i != 9)
                    dFilter = dFilter + tempColumnName + " like  '*" + this.toolStripTextBox1.Text + "*' or ";
                else
                    dFilter = dFilter + tempColumnName + " like  '*" + this.toolStripTextBox1.Text + "*' ";

            }

            if (this.toolStripTextBox1.Text.Replace(" ", "").Length > 0)

                this.ly_inma0010fjBindingSource.Filter = dFilter;
            else
                this.ly_inma0010fjBindingSource.Filter = " ";
        }

       

        private void ly_inma0010DataGridView_MouseDoubleClick(object sender, MouseEventArgs e)
        {

            if (null == ly_inma0010DataGridView.CurrentRow) return;
            string s = this.ly_inma0010DataGridView.CurrentRow.Cells["物资编号"].Value.ToString();
            //int loanid = int.Parse(this.xD_Client_LoanDataGridView.CurrentRow.Cells["Id_loan"].Value.ToString());

            LY_MaterialAdd queryForm = new LY_MaterialAdd();

            queryForm.statemode = "附件";
            queryForm.runmode = "修改";
            queryForm.material_code = s;

            queryForm.StartPosition = FormStartPosition.CenterParent;
            queryForm.ShowDialog();

            if (queryForm.DialogResult != DialogResult.Cancel)
            {
                this.ly_inma0010fjTableAdapter.Fill(this.lYMaterialMange.ly_inma0010fj);

                this.ly_inma0010fjBindingSource.Position = this.ly_inma0010fjBindingSource.Find("物资编号", s);
            }
            
            //if (null == ly_inma0010DataGridView.CurrentRow) return;
            //string s = this.ly_inma0010DataGridView.CurrentRow.Cells["物资编号"].Value.ToString();

            //string selAllString = "SELECT     parentno, itemno,  itemno +':' +itemname as itemname  from f_BomExtend('" + s +"',1) ORDER BY  id_num ";
            //string cString = SQLDatabase.Connectstring; ;
            //SqlDataAdapter bomAdapter = new SqlDataAdapter(selAllString, cString);

            //bomAdapter.SelectCommand.CommandTimeout = 0;

            //DataSet bomData = new DataSet();
            //bomAdapter.Fill(bomData);

            //this.treeView1.Nodes.Clear();
            //MakeTreeView(bomData.Tables[0], null, null);
            ////this.treeView1.ExpandAll();
            //this.treeView1.SelectedNode = this.treeView1.Nodes[0];
            //this.treeView1.SelectedNode.Expand();

            //this.groupBox1.Text = s + "BOM结构图";
           
        }

       

      

        private void bindingNavigatorAddNewItem_Click(object sender, EventArgs e)
        {
            LY_MaterialAdd queryForm = new LY_MaterialAdd();

            queryForm.material_code = "";
            queryForm.runmode = "增加";
            queryForm.statemode = "附件";

            queryForm.StartPosition = FormStartPosition.CenterParent;
            queryForm.ShowDialog();

            if (queryForm.DialogResult != DialogResult.Cancel)
            {
                this.ly_inma0010fjTableAdapter.Fill(this.lYMaterialMange.ly_inma0010fj);
                this.ly_inma0010fjBindingSource.Position = this.ly_inma0010fjBindingSource.Find("物资编号", queryForm.material_code);
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if (null == ly_inma0010DataGridView.CurrentRow) return;
            string s = this.ly_inma0010DataGridView.CurrentRow.Cells["物资编号"].Value.ToString();
            //int loanid = int.Parse(this.xD_Client_LoanDataGridView.CurrentRow.Cells["Id_loan"].Value.ToString());

            LY_MaterialAdd queryForm = new LY_MaterialAdd();

            queryForm.statemode = "附件";
            queryForm.runmode = "修改";
            queryForm.material_code = s;

            queryForm.StartPosition = FormStartPosition.CenterParent;
            queryForm.ShowDialog();

            if (queryForm.DialogResult != DialogResult.Cancel)
            {
                this.ly_inma0010fjTableAdapter.Fill(this.lYMaterialMange.ly_inma0010fj);

                this.ly_inma0010fjBindingSource.Position = this.ly_inma0010fjBindingSource.Find("物资编号", s);
            }

        }

        private void bindingNavigatorDeleteItem_Click(object sender, EventArgs e)
        {
            string message = "确定删除当前记录吗？";
            string caption = "提示...";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result;



            result = MessageBox.Show(message, caption, buttons,
            MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

            if (result == DialogResult.Yes)
            {

                this.ly_inma0010fjBindingSource.RemoveCurrent();


                ly_inma0010DataGridView.EndEdit();
                ly_inma0010fjBindingSource.EndEdit();


                this.ly_inma0010fjTableAdapter.Update(this.lYMaterialMange.ly_inma0010fj);

                //string s = this.xD_Sel_SellBalanceDataGridView.CurrentRow.Cells["编号"].Value.ToString();

                //this.hS_ClientPaymentTableAdapter.Fill(this.xD_SellBalance.HS_ClientPayment, s);


            }
        }

        private void toolStripButton16_Click(object sender, EventArgs e)
        {
            ExportDataGridviewTOExcell.ExportDataGridview(this.ly_inma0010DataGridView, true);
        }

        //private void fillToolStripButton_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        this.bom_material_selTableAdapter.Fill(this.lYMaterialMange.bom_material_sel, wzbhToolStripTextBox.Text);
        //    }
        //    catch (System.Exception ex)
        //    {
        //        System.Windows.Forms.MessageBox.Show(ex.Message);
        //    }

        //}

        
    }
}
