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
    public partial class LY_Material_Replacemange : Form
    {
        private int selectionIdx = 0;
        private  string formState;
        private int nowRow;

        public LY_Material_Replacemange()
        {
            InitializeComponent();
        }

        private void LY_Machine_Process_Load(object sender, EventArgs e)
        {
            // TODO: 这行代码将数据加载到表“lYMaterielRequirements.bom_material_for_machine_base_New”中。您可以根据需要移动或删除它。
            this.bom_material_for_machine_base_NewTableAdapter.Fill(this.lYMaterielRequirements.bom_material_for_machine_base_New);
            // TODO: 这行代码将数据加载到表“lYMaterielRequirements.bom_material_for_machine_base_New”中。您可以根据需要移动或删除它。
            this.bom_material_for_machine_base_NewTableAdapter.Fill(this.lYMaterielRequirements.bom_material_for_machine_base_New);
            this.bom_material_for_machine_base_NewTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            this.bom_material_for_machine_base_NewTableAdapter.Fill(this.lYMaterielRequirements.bom_material_for_machine_base_New);


                    
            
          
            this.ly_material_replacelistTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            this.bom_material_sel_replace_NewTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;



      
           
        }

        private void toolStripTextBox1_Enter(object sender, EventArgs e)
        {
            toolStripTextBox1.Text = "";

            this.bom_material_for_machine_base_NewBindingSource.Filter = "";//bom_material_sel_replace
        }

        private void toolStripTextBox1_KeyUp(object sender, KeyEventArgs e)
        {
            string filterString = GetDataGridviewMultiFilterString.DGVMultiFilterString(this.bom_material_for_machine_baseDataGridView, this.toolStripTextBox1.Text);



            if (null == filterString)
                filterString = "";

            this.bom_material_for_machine_base_NewBindingSource.Filter = filterString;
        }

        private void ly_inma0010_sortDataGridView_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView dgv = sender as DataGridView;

            if (null == dgv.CurrentRow) return;


            string itemno = dgv.Rows[e.RowIndex].Cells["物资编号"].Value.ToString();

            this.groupBox1.Text = itemno + ":代料列表";

           
            this.ly_material_replacelistTableAdapter.Fill(this.lYMaterielRequirements.ly_material_replacelist,itemno);
            this.bom_material_sel_replace_NewTableAdapter.Fill(this.lYMaterielRequirements.bom_material_sel_replace_New, itemno);
        }

        private void ly_manufacturing_procedure_selDataGridView_DoubleClick(object sender, EventArgs e)
        {
            if (null == this.bom_material_sel_replaceDataGridView.CurrentRow) return;



            string itemno = this.bom_material_for_machine_baseDataGridView.CurrentRow.Cells["物资编号"].Value.ToString();
            string replaceitemno = bom_material_sel_replaceDataGridView.CurrentRow.Cells["物资编号1"].Value.ToString();



            this.ly_material_replacelistBindingSource.AddNew();

            this.ly_material_replacelistDataGridView.CurrentRow.Cells["物料编码"].Value = itemno;
            this.ly_material_replacelistDataGridView.CurrentRow.Cells["代料编码"].Value = replaceitemno;

            SaveChanged();


            //this.ly_material_replacelistDataGridView.EndEdit();
            //this.ly_material_replacelistBindingSource.EndEdit();

            //this.ly_material_replacelistTableAdapter.Update(this.lYMaterielRequirements.ly_material_replacelist);




            this.ly_material_replacelistTableAdapter.Fill(this.lYMaterielRequirements.ly_material_replacelist, itemno);
            this.bom_material_sel_replace_NewTableAdapter.Fill(this.lYMaterielRequirements.bom_material_sel_replace_New, itemno);
            this.ly_material_replacelistBindingSource.Position = this.ly_material_replacelistBindingSource.Find("代料编码", replaceitemno);
        }

        private void 删除供应商ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (null == this.ly_material_replacelistDataGridView.CurrentRow) return;

            string sequencenumber = this.ly_material_replacelistDataGridView.CurrentRow.Cells["代料编码"].Value.ToString();
            string sequencename = this.ly_material_replacelistDataGridView.CurrentRow.Cells["代料名称"].Value.ToString();


            string message1 = "当前(代料 " + sequencenumber + ":" + sequencename + ")将被删除，继续吗？";
            string caption1 = "提示...";
            MessageBoxButtons buttons1 = MessageBoxButtons.YesNo;

            DialogResult result1;



            result1 = MessageBox.Show(message1, caption1, buttons1,
            MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

            if (result1 == DialogResult.Yes)
            {
                this.ly_material_replacelistBindingSource.RemoveCurrent();

             

                SaveChanged();


            


            }
        }

        private void SaveChanged()
        {
            
            
            this.ly_material_replacelistDataGridView.EndEdit();
            this.ly_material_replacelistBindingSource.EndEdit();

            this.ly_material_replacelistTableAdapter.Update(this.lYMaterielRequirements.ly_material_replacelist);


            string itemno = this.bom_material_for_machine_baseDataGridView.CurrentRow.Cells["物资编号"].Value.ToString();
            this.bom_material_sel_replace_NewTableAdapter.Fill(this.lYMaterielRequirements.bom_material_sel_replace_New, itemno);




           
        }

      
      

      

        private void ly_machinepart_processDataGridView_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            DataGridView dgv = sender as DataGridView;




            if ("代料比例" == dgv.CurrentCell.OwningColumn.Name)
            {

                ChangeValue queryForm = new ChangeValue();

                queryForm.OldValue = dgv.CurrentCell.Value.ToString();
                queryForm.NewValue = "";
                queryForm.ChangeMode = "value";
                queryForm.ShowDialog();




                if (queryForm.NewValue != "")
                {
                    dgv.CurrentRow.Cells["代料比例"].Value = queryForm.NewValue;
                    //dgv.CurrentRow.Cells["discount_money"].Value = DBNull.Value;

                    //dgv.CurrentRow.Cells["approve_flag"].Value = "False";
                    SaveChanged();


                    //CountPlanStru();

                }
                else
                {
                   

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

            this.bom_material_sel_replace_NewBindingSource.Filter = "";//
        }

        private void toolStripTextBox2_KeyUp(object sender, KeyEventArgs e)
        {
            string filterString = GetDataGridviewMultiFilterString.DGVMultiFilterString(this.bom_material_sel_replaceDataGridView, this.toolStripTextBox2.Text);



            if (null == filterString)
                filterString = "";

            this.bom_material_sel_replace_NewBindingSource.Filter = filterString;
        }

        
    }
}
