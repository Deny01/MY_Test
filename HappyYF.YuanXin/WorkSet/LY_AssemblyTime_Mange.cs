using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Transactions;
using HappyYF.Infrastructure.Repositories;

namespace HappyYF.YuanXin.WorkSet
{
    public partial class LY_AssemblyTime_Mange : Form
    {
        public LY_AssemblyTime_Mange()
        {
            InitializeComponent();
        }

        private void ly_assembly_timeBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.ly_assembly_timeBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.lYTecSet);

        }

        private void LY_AssemblyTime_Mange_Load(object sender, EventArgs e)
        {
            this.lY_Assembly_material_selTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            this.lY_Assembly_material_selTableAdapter.Fill(this.lYTecSet.LY_Assembly_material_sel);

            this.ly_assembly_timeTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            this.ly_assembly_timeTableAdapter.Fill(this.lYTecSet.ly_assembly_time);

        }

        private void bom_material_selDataGridView_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (null == bom_material_selDataGridView.CurrentRow) return;
            string componentNum = this.bom_material_selDataGridView.CurrentRow.Cells["物资编号1"].Value.ToString(); 
               

                string insStr = " INSERT INTO ly_assembly_time  " +
               "( Item_Code) " +
               " values ('"  + componentNum + "' )";


                using (TransactionScope scope = new TransactionScope())
                {

                    SqlConnection sqlConnection1 = new SqlConnection(SQLDatabase.Connectstring);
                    SqlCommand cmd = new SqlCommand();

                    cmd.CommandText = insStr;
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = sqlConnection1;



                    sqlConnection1.Open();
                    cmd.ExecuteNonQuery();

                    sqlConnection1.Close();

                    scope.Complete();
                }



            
            this.lY_Assembly_material_selTableAdapter.Fill(this.lYTecSet.LY_Assembly_material_sel);
            this.ly_assembly_timeTableAdapter.Fill(this.lYTecSet.ly_assembly_time);
            this.ly_assembly_timeBindingSource.Position = this.ly_assembly_timeBindingSource.Find("编码", componentNum);
        }

        private void toolStripTextBox2_KeyUp(object sender, KeyEventArgs e)
        {
            string filterString;


            filterString = GetDataGridviewMultiFilterString.DGVMultiFilterString(this.bom_material_selDataGridView, this.toolStripTextBox2.Text);

                     

            this.lY_Assembly_material_selBindingSource.Filter = filterString;

           
        }

        private void toolStripTextBox2_Enter(object sender, EventArgs e)
        {
            toolStripTextBox2.Text = "";

            this.lY_Assembly_material_selBindingSource.Filter = " ";

         
        }

        private void toolStripTextBox3_KeyUp(object sender, KeyEventArgs e)
        {
            string filterString;


            filterString = GetDataGridviewMultiFilterString.DGVMultiFilterString(this.ly_assembly_timeDataGridView, this.toolStripTextBox3.Text);



            this.ly_assembly_timeBindingSource.Filter = filterString;
        }

        private void toolStripTextBox3_Enter(object sender, EventArgs e)
        {
            toolStripTextBox3.Text = "";

            this.ly_assembly_timeBindingSource.Filter = " ";
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            ExportDataGridviewTOExcellA.ExportDataGridview(this.ly_assembly_timeDataGridView, true);
        }

        private void bindingNavigatorDeleteItem_Click(object sender, EventArgs e)
        {
             
            string componentNum = this.ly_assembly_timeDataGridView.CurrentRow.Cells["编码"].Value.ToString();


            string message = "确定删除当前记录吗？";
            string caption = "提示...";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result;



            result = MessageBox.Show(message, caption, buttons,
            MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

            if (result == DialogResult.Yes)
            {
                this.Validate();

                this.ly_assembly_timeBindingSource.RemoveCurrent();

                SaveChanged();

                this.lY_Assembly_material_selBindingSource.Position = this.lY_Assembly_material_selBindingSource.Find("物资编号", componentNum);
            }
        }

        private void SaveChanged()
        {
            ly_assembly_timeDataGridView.EndEdit();

            this.ly_assembly_timeBindingSource.EndEdit();
            this.ly_assembly_timeTableAdapter.Update(this.lYTecSet.ly_assembly_time);
        }

        private void ly_assembly_timeDataGridView_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            DataGridView dgv = sender as DataGridView;




            if ("工时" == dgv.CurrentCell.OwningColumn.Name)
            {
                if (!SQLDatabase.CheckHaveRight(SQLDatabase.NowUserID, "装配工时因子"))
                {
                    MessageBox.Show("无权限！");return;
                }
                ChangeValue queryForm = new ChangeValue();

                queryForm.OldValue = dgv.CurrentCell.Value.ToString();
                queryForm.NewValue = "";
                queryForm.ChangeMode = "value";
                queryForm.ShowDialog();




                if (queryForm.NewValue != "")
                {
                    dgv.CurrentRow.Cells["工时"].Value = queryForm.NewValue; 
                    SaveChanged();

                }
                else
                {
                    

                }
                return;

            }


            /////////////////////////////////////////////////////

            if ("单价" == dgv.CurrentCell.OwningColumn.Name)
            {
                if (!SQLDatabase.CheckHaveRight(SQLDatabase.NowUserID, "装配工时因子"))
                {
                    MessageBox.Show("无权限！"); return;
                }
                ChangeValue queryForm = new ChangeValue();

                queryForm.OldValue = dgv.CurrentCell.Value.ToString();
                queryForm.NewValue = "";
                queryForm.ChangeMode = "value";
                queryForm.ShowDialog();




                if (queryForm.NewValue != "")
                {
                    dgv.CurrentRow.Cells["单价"].Value = queryForm.NewValue; 
                    SaveChanged();

                }
                else
                { 

                }
                return;

            }


            /////////////////////////////////////////////////////
            if ("因子" == dgv.CurrentCell.OwningColumn.Name)
            {
                if (!SQLDatabase.CheckHaveRight(SQLDatabase.NowUserID, "装配工时因子"))
                {
                    MessageBox.Show("无因子设置权限！"); return;
                }
                ChangeValue queryForm = new ChangeValue();

                queryForm.OldValue = dgv.CurrentCell.Value.ToString();
                queryForm.NewValue = "";
                queryForm.ChangeMode = "value";
                queryForm.ShowDialog(); 

                if (queryForm.NewValue != "")
                {
                    dgv.CurrentRow.Cells["因子"].Value = queryForm.NewValue; 
                    SaveChanged();

                }
                else
                {
                    

                }
                return;

            }
            if ("T6因子" == dgv.CurrentCell.OwningColumn.Name)
            {
                if (!SQLDatabase.CheckHaveRight(SQLDatabase.NowUserID, "T6因子设置"))
                {
                    MessageBox.Show("无权限！"); return;
                }
                ChangeValue queryForm = new ChangeValue();

                queryForm.OldValue = dgv.CurrentCell.Value.ToString();
                queryForm.NewValue = "";
                queryForm.ChangeMode = "value";
                queryForm.ShowDialog();




                if (queryForm.NewValue != "")
                {
                    dgv.CurrentRow.Cells["T6因子"].Value = queryForm.NewValue; 
                    SaveChanged();

                }
                else
                { 

                }
                return;

            }
          
        }
    }
}
