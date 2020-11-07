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
    public partial class LY_Material_Queryprice : Form
    {
        private int selectionIdx = 0;
        private  string formState;
        private int nowRow;

        public LY_Material_Queryprice()
        {
            InitializeComponent();
        }

        private void LY_Machine_Process_Load(object sender, EventArgs e)
        {
           
            this.ly_material_querypriceTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            this.bom_material_sel_querypriceTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;

            this.ly_material_querypriceTableAdapter.Fill(this.lYMaterielRequirements.ly_material_queryprice);
            this.bom_material_sel_querypriceTableAdapter.Fill(this.lYMaterielRequirements.bom_material_sel_queryprice);





        }

        private void toolStripTextBox1_Enter(object sender, EventArgs e)
        {
            toolStripTextBox1.Text = "";

            this.ly_material_querypriceBindingSource.Filter = "";//bom_material_sel_replace
        }

        private void toolStripTextBox1_KeyUp(object sender, KeyEventArgs e)
        {
            string filterString = GetDataGridviewMultiFilterString.DGVMultiFilterString(this.ly_material_querypriceDataGridView, this.toolStripTextBox1.Text);



            if (null == filterString)
                filterString = "";

            this.ly_material_querypriceBindingSource.Filter = filterString;
        }

       

        private void ly_manufacturing_procedure_selDataGridView_DoubleClick(object sender, EventArgs e)
        {
            if (null == this.bom_material_sel_replaceDataGridView.CurrentRow) return;



          
            string querypriceitemno = bom_material_sel_replaceDataGridView.CurrentRow.Cells["物资编号1"].Value.ToString();



            this.ly_material_querypriceBindingSource.AddNew();

            this.ly_material_querypriceDataGridView.CurrentRow.Cells["物料编码"].Value = querypriceitemno;
           

            SaveChanged();


            //this.ly_material_replacelistDataGridView.EndEdit();
            //this.ly_material_replacelistBindingSource.EndEdit();

            //this.ly_material_replacelistTableAdapter.Update(this.lYMaterielRequirements.ly_material_replacelist);

            this.ly_material_querypriceTableAdapter.Fill(this.lYMaterielRequirements.ly_material_queryprice);
            this.bom_material_sel_querypriceTableAdapter.Fill(this.lYMaterielRequirements.bom_material_sel_queryprice);




            this.ly_material_querypriceBindingSource.Position = this.ly_material_querypriceBindingSource.Find("物料编码", querypriceitemno);
        }

        private void 删除供应商ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if ("000" != SQLDatabase .NowUserID) return;
            if (null == this.ly_material_querypriceDataGridView.CurrentRow) return;

            string sequencenumber = this.ly_material_querypriceDataGridView.CurrentRow.Cells["物料编码"].Value.ToString();
            string sequencename = this.ly_material_querypriceDataGridView.CurrentRow.Cells["物料编码"].Value.ToString();


            string message1 = "当前(询价 " + sequencenumber + ":" + sequencename + ")将被删除，继续吗？";
            string caption1 = "提示...";
            MessageBoxButtons buttons1 = MessageBoxButtons.YesNo;

            DialogResult result1;



            result1 = MessageBox.Show(message1, caption1, buttons1,
            MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

            if (result1 == DialogResult.Yes)
            {
                this.ly_material_querypriceBindingSource.RemoveCurrent();



                SaveChanged();





            }
        }

        private void SaveChanged()
        {
            

            this.ly_material_querypriceDataGridView.EndEdit();
            this.ly_material_querypriceBindingSource.EndEdit();

            this.ly_material_querypriceTableAdapter.Update(this.lYMaterielRequirements.ly_material_queryprice);

            this.bom_material_sel_querypriceTableAdapter.Fill(this.lYMaterielRequirements.bom_material_sel_queryprice);


           
            //this.ly_material_querypriceTableAdapter.Fill(this.lYMaterielRequirements.ly_material_queryprice);
            //this.bom_material_sel_querypriceTableAdapter.Fill(this.lYMaterielRequirements.bom_material_sel_queryprice);




            //this.ly_material_querypriceBindingSource.Position = this.ly_material_querypriceBindingSource.Find("物料编码", querypriceitemno);






        }

      
      

      

        private void ly_machinepart_processDataGridView_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            DataGridView dgv = sender as DataGridView;




            if ("询价" == dgv.CurrentCell.OwningColumn.Name)
            {

                ChangeValue queryForm = new ChangeValue();

                queryForm.OldValue = dgv.CurrentCell.Value.ToString();
                queryForm.NewValue = "";
                queryForm.ChangeMode = "value";
                queryForm.ShowDialog();




                if (queryForm.NewValue != "")
                {
                    dgv.CurrentRow.Cells["询价"].Value = queryForm.NewValue;
                    //dgv.CurrentRow.Cells["discount_money"].Value = DBNull.Value;

                    //dgv.CurrentRow.Cells["approve_flag"].Value = "False";
                    SaveChanged();


                    //CountPlanStru();

                }
                else
                {
                    dgv.CurrentRow.Cells["询价"].Value = DBNull.Value;
                    SaveChanged();
                }
                return;

            }


         ///////////////////////////////////////////////////////////////////////////////

            if ("备注" == dgv.CurrentCell.OwningColumn.Name)
            {

                ChangeValue queryForm = new ChangeValue();

                queryForm.OldValue = dgv.CurrentCell.Value.ToString();
                queryForm.NewValue = "";
                queryForm.ChangeMode = "longstring";
                queryForm.ShowDialog();




                if (queryForm.NewValue != "")
                {
                    dgv.CurrentRow.Cells["备注"].Value = queryForm.NewValue;
                   
                    SaveChanged();


                   

                }
                else
                {
                   

                }
                return;

            }


            /////////////////////////////////////////////////////


          
        }

      

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            //this.SetFormState("Edit");
           // this.saveState = "Change";
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            //this.SetFormState("View");
            //string itemno = ly_inma0010_sortDataGridView.CurrentRow.Cells["物资编号"].Value.ToString();


            //this.ly_machinepart_processTableAdapter.Fill(this.lYMaterielRequirements.ly_machinepart_process, itemno);
        }

        private void yX_fillCard_MoneyBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            
            SaveChanged();
            //this.SetFormState("View");

        }

      

        private void ly_machinepart_processDataGridView_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            DataGridView dgvc = sender as DataGridView;
            string temp = dgvc[e.ColumnIndex, e.RowIndex].EditedFormattedValue.ToString().Replace(" ", "");

            if (temp == "")
            {
                dgvc[e.ColumnIndex, e.RowIndex].Value = DBNull.Value;
                e.Cancel = false;
            }
            else
            {

                MessageBox.Show("数据格式错误...");
            }
        }

       
        private void toolStripTextBox2_Enter(object sender, EventArgs e)
        {
            toolStripTextBox2.Text = "";

            this.bom_material_sel_querypriceBindingSource1.Filter = "";//
        }

        private void toolStripTextBox2_KeyUp(object sender, KeyEventArgs e)
        {
            string filterString = GetDataGridviewMultiFilterString.DGVMultiFilterString(this.bom_material_sel_replaceDataGridView, this.toolStripTextBox2.Text);



            if (null == filterString)
                filterString = "";

            this.bom_material_sel_querypriceBindingSource1.Filter = filterString;
        }
    }
}
