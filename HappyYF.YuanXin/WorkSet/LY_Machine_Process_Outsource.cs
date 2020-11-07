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
    public partial class LY_Machine_Process_Outsource : Form
    {
        private int selectionIdx = 0;
        private  string formState;
        private int nowRow;


        string nowitemno;

        public string Nowitemno
        {
            get { return nowitemno; }
            set { nowitemno = value; }
        }
        string nowcontractcode;

        public string Nowcontractcode
        {
            get { return nowcontractcode; }
            set { nowcontractcode = value; }
        }

        string nowaddcode;

        public string Nowaddcode
        {
            get { return nowaddcode; }
            set { nowaddcode = value; }
        }

        int nowid2;

        public int Nowid2
        {
            get { return nowid2; }
            set { nowid2 = value; }
        }

        public LY_Machine_Process_Outsource()
        {
            InitializeComponent();
        }

        private void LY_Machine_Process_Load(object sender, EventArgs e)
        {
            SetFormState("View");

           

            this.ly_manufacturing_procedure_selTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            this.ly_machinepart_process_outsourceTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;

            this.groupBox1.Text ="合同:"+nowcontractcode  +" "+ nowitemno + ":加工工序";


            this.ly_manufacturing_procedure_selTableAdapter.Fill(this.lYMaterielRequirements.ly_manufacturing_procedure_sel);

            this.ly_machinepart_process_outsourceTableAdapter.Fill(this.lYOutsourceData.ly_machinepart_process_outsource, nowitemno, nowcontractcode, nowid2,nowaddcode );
          
        }

       
       

        private void ly_manufacturing_procedure_selDataGridView_DoubleClick(object sender, EventArgs e)
        {
            if (null == this.ly_manufacturing_procedure_selDataGridView.CurrentRow) return;



            string sequencenumber = this.ly_manufacturing_procedure_selDataGridView.CurrentRow.Cells["编号"].Value.ToString();
            string itemno = nowitemno ;



            this.ly_machinepart_process_outsourceBindingSource.AddNew();

            this.ly_machinepart_processDataGridView.CurrentRow.Cells["工序编号"].Value = sequencenumber;
            this.ly_machinepart_processDataGridView.CurrentRow.Cells["工件编码"].Value = itemno;
            this.ly_machinepart_processDataGridView.CurrentRow.Cells["顺序"].Value = this.ly_machinepart_processDataGridView.Rows.Count;
            this.ly_machinepart_processDataGridView.CurrentRow.Cells["合同编号"].Value = nowcontractcode ;
            this.ly_machinepart_processDataGridView.CurrentRow.Cells["id2"].Value = nowid2 ;
            this.ly_machinepart_processDataGridView.CurrentRow.Cells["加工码"].Value = nowaddcode ;

            this.ly_machinepart_processDataGridView.EndEdit();
            this.ly_machinepart_process_outsourceBindingSource.EndEdit();

            this.ly_machinepart_process_outsourceTableAdapter.Update(this .lYOutsourceData .ly_machinepart_process_outsource );



            this.ly_manufacturing_procedure_selTableAdapter.Fill(this.lYMaterielRequirements.ly_manufacturing_procedure_sel);
            this.ly_machinepart_process_outsourceTableAdapter.Fill(this.lYOutsourceData.ly_machinepart_process_outsource, nowitemno, nowcontractcode, nowid2,nowaddcode );

            foreach (DataGridViewRow dgr in ly_machinepart_processDataGridView.Rows)
            {
                dgr.Cells["顺序"].Value = dgr.Index + 1;

            }

            this.ly_machinepart_processDataGridView.EndEdit();
            this.ly_machinepart_process_outsourceBindingSource.EndEdit();

            this.ly_machinepart_process_outsourceTableAdapter.Update(this.lYOutsourceData.ly_machinepart_process_outsource);
            this.ly_machinepart_process_outsourceTableAdapter.Fill(this.lYOutsourceData.ly_machinepart_process_outsource, nowitemno, nowcontractcode, nowid2, nowaddcode);
        }

        private void 删除供应商ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (null == this.ly_machinepart_processDataGridView.CurrentRow) return;

            string sequencenumber = this.ly_machinepart_processDataGridView.CurrentRow.Cells["工序编号"].Value.ToString();
            string sequencename = this.ly_machinepart_processDataGridView.CurrentRow.Cells["工序名称"].Value.ToString();


            string message1 = "当前(工序 " + sequencenumber + ":" + sequencename + ")将被删除，继续吗？";
            string caption1 = "提示...";
            MessageBoxButtons buttons1 = MessageBoxButtons.YesNo;

            DialogResult result1;



            result1 = MessageBox.Show(message1, caption1, buttons1,
            MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

            if (result1 == DialogResult.Yes)
            {
                this.ly_machinepart_process_outsourceBindingSource.RemoveCurrent();

                foreach (DataGridViewRow dgr in ly_machinepart_processDataGridView.Rows)
                {
                    dgr.Cells["顺序"].Value = dgr.Index + 1;

                }

                SaveChanged();


                string itemno = nowitemno ;

                this.ly_manufacturing_procedure_selTableAdapter.Fill(this.lYMaterielRequirements.ly_manufacturing_procedure_sel);
                //this.ly_machinepart_processTableAdapter.Fill(this.lYMaterielRequirements.ly_machinepart_process, itemno);


            }
        }

        private void SaveChanged()
        {
            this.ly_machinepart_processDataGridView.EndEdit();
            this.ly_machinepart_process_outsourceBindingSource.EndEdit();

            this.ly_machinepart_process_outsourceTableAdapter.Update(this.lYOutsourceData .ly_machinepart_process_outsource);

            string itemno = nowitemno ;


            this.ly_machinepart_process_outsourceTableAdapter.Fill(this.lYOutsourceData.ly_machinepart_process_outsource, nowitemno, nowcontractcode ,nowid2 ,nowaddcode );

            foreach (DataGridViewRow dgr in ly_machinepart_processDataGridView.Rows)
            {
                dgr.Cells["顺序"].Value = dgr.Index + 1;

            }

            this.ly_machinepart_processDataGridView.EndEdit();
            this.ly_machinepart_process_outsourceBindingSource.EndEdit();

            this.ly_machinepart_process_outsourceTableAdapter.Update(this.lYOutsourceData.ly_machinepart_process_outsource);
            this.ly_machinepart_process_outsourceTableAdapter.Fill(this.lYOutsourceData.ly_machinepart_process_outsource, nowitemno, nowcontractcode, nowid2, nowaddcode);
        }

        private void ly_machinepart_processDataGridView_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex >= 0)
                selectionIdx = e.RowIndex;
        }

        private void ly_machinepart_processDataGridView_CellMouseMove(object sender, DataGridViewCellMouseEventArgs e)
        {
            DataGridView dgv = sender as DataGridView;

            if ((e.Clicks < 2) && (e.Button == MouseButtons.Left))
            {
                if ((e.ColumnIndex == -1) && (e.RowIndex > -1))
                    dgv.DoDragDrop(dgv.Rows[e.RowIndex], DragDropEffects.Move);
            } 
        }

        private int GetRowFromPoint(DataGridView dgv, int x, int y)
        {
            for (int i = 0; i < dgv.RowCount; i++)
            {
                Rectangle rec = dgv.GetRowDisplayRectangle(i, false);

                if (dgv.RectangleToScreen(rec).Contains(x, y))
                    return i;
            }

            return -1;
        }
        private void ly_machinepart_processDataGridView_DragDrop(object sender, DragEventArgs e)
        {
            DataGridView dgv = sender as DataGridView;



            int idx = GetRowFromPoint(dgv, e.X, e.Y);
            if (idx < 0) return;
            //index2 = idx;
            if (e.Data.GetDataPresent(typeof(DataGridViewRow)))
            {

                DataGridViewRow row = (DataGridViewRow)e.Data.GetData(typeof(DataGridViewRow));

                int tempOrder = row.Index;
                // this.gqis.Ins_Incontrol(idx, row.Cells[0].Value.ToString());



                //dgv.Rows[idx].Cells["顺序"].Value = tempOrder;
                //dgv.Rows[idx].Cells["顺序"].Value = tempOrder;

                if (idx > row.Index)
                {
                    foreach (DataGridViewRow dgvr in dgv.Rows)
                    {

                        if (dgvr.Index > row.Index && dgvr.Index <= idx)
                        {
                            dgvr.Cells["顺序"].Value = dgvr.Index;

                        }
                    }
                }
                if (idx < row.Index)
                {
                    foreach (DataGridViewRow dgvr in dgv.Rows)
                    {

                        if (dgvr.Index >= idx && dgvr.Index < row.Index)
                        {
                            dgvr.Cells["顺序"].Value = dgvr.Index + 2;

                        }
                    }
                }


                row.Cells["顺序"].Value = idx + 1;
                // dgv.Rows[idx].Cells["顺序"].Value = row.Index + 1;


                SaveChanged();
               

                dgv.Rows[idx].Selected = true;
                dgv.CurrentCell = dgv.Rows[idx].Cells["顺序"];


                //selectionIdx = idx;
            } 
        }

        private void ly_machinepart_processDataGridView_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move; 
        }

        private void ly_machinepart_processDataGridView_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            DataGridView dgv = sender as DataGridView;




            if ("准终时间" == dgv.CurrentCell.OwningColumn.Name)
            {

                ChangeValue queryForm = new ChangeValue();

                queryForm.OldValue = dgv.CurrentCell.Value.ToString();
                queryForm.NewValue = "";
                queryForm.ChangeMode = "value";
                queryForm.ShowDialog();




                if (queryForm.NewValue != "")
                {
                    dgv.CurrentRow.Cells["准终时间"].Value = queryForm.NewValue;
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


            /////////////////////////////////////////////////////

         

            if ("顺序" == dgv.CurrentCell.OwningColumn.Name)
            {

                ChangeValue queryForm = new ChangeValue();

                queryForm.OldValue = dgv.CurrentCell.Value.ToString();
                queryForm.NewValue = "";
                queryForm.ChangeMode = "value";
                queryForm.ShowDialog();




                if (queryForm.NewValue != "")
                {
                    dgv.CurrentRow.Cells["顺序"].Value = queryForm.NewValue;
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


            /////////////////////////////////////////////////////

            if ("单件工时" == dgv.CurrentCell.OwningColumn.Name)
            {

                ChangeValue queryForm = new ChangeValue();

                queryForm.OldValue = dgv.CurrentCell.Value.ToString();
                queryForm.NewValue = "";
                queryForm.ChangeMode = "value";
                queryForm.ShowDialog();




                if (queryForm.NewValue != "")
                {
                    dgv.CurrentRow.Cells["单件工时"].Value = queryForm.NewValue;
                    
                    SaveChanged();


                    //CountPlanStru();

                }
                else
                {
                   
                }
                return;

            }


            /////////////////////////////////////////////////////

            if ("加工说明" == dgv.CurrentCell.OwningColumn.Name)
            {

                ChangeValue queryForm = new ChangeValue();

                queryForm.OldValue = dgv.CurrentCell.Value.ToString();
                queryForm.NewValue = "";
                queryForm.ChangeMode = "longstring";
                queryForm.ShowDialog();




                if (queryForm.NewValue != "")
                {
                    dgv.CurrentRow.Cells["加工说明"].Value = queryForm.NewValue;
                   
                    SaveChanged();


                   

                }
                else
                {
                   

                }
                return;

            }


            /////////////////////////////////////////////////////


          
        }

        private void SetFormState(string state)
        {
            // view edit add save cancle

            if ("View" == state)
            {
                this.formState = "View";

                this.toolStripButton2.Enabled = true;
                this.toolStripButton3.Enabled = true ;
                this.yX_fillCard_MoneyBindingNavigatorSaveItem.Enabled = false;
                this.toolStripButton4.Enabled = false ;
               







                this.ly_machinepart_processDataGridView.ReadOnly = true;






            }
            else
            {
                this.formState = "Edit";

                this.ly_machinepart_processDataGridView.ReadOnly = false;

                if (null != ly_machinepart_processDataGridView.CurrentRow)
                    this.nowRow = ly_machinepart_processDataGridView.CurrentRow.Index;

                this.toolStripButton2.Enabled = false ;
                this.toolStripButton3.Enabled = false ;
                this.yX_fillCard_MoneyBindingNavigatorSaveItem.Enabled = true ;
                this.toolStripButton4.Enabled = true;
               





                this.ly_machinepart_processDataGridView.ReadOnly = false;


                this.ly_machinepart_processDataGridView.Columns["顺序"].ReadOnly = true;
                this.ly_machinepart_processDataGridView.Columns["工序名称"].ReadOnly = true;
                this.ly_machinepart_processDataGridView.Columns["工时单价"].ReadOnly = true;
                this.ly_machinepart_processDataGridView.Columns["准终工价"].ReadOnly = true;
                this.ly_machinepart_processDataGridView.Columns["准终累加"].ReadOnly = true;
                this.ly_machinepart_processDataGridView.Columns["单件工价"].ReadOnly = true;
                this.ly_machinepart_processDataGridView.Columns["单件累加"].ReadOnly = true;




            }


        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            this.SetFormState("Edit");
           // this.saveState = "Change";
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            this.SetFormState("View");
            string itemno = nowitemno ;


            this.ly_machinepart_process_outsourceTableAdapter.Fill(this.lYOutsourceData .ly_machinepart_process_outsource , nowitemno, nowcontractcode ,nowid2,nowaddcode  );
        }

        private void yX_fillCard_MoneyBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            
            SaveChanged();
            this.SetFormState("View");

        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
             DataGridView dc = this.ly_machinepart_processDataGridView;
            
            if (this.formState != "View" && null !=dc .CurrentRow )
            {

                if (keyData == Keys.Enter)
                {
                    //DataGridView dc = this.ly_machinepart_processDataGridView;

                    if (7 == dc.CurrentCell.ColumnIndex && dc.CurrentRow.Index != (dc.RowCount - 1))
                    {
                        dc.CurrentCell = dc.CurrentRow.Cells["单件工时"];


                    }
                     else if (10 == dc.CurrentCell.ColumnIndex && dc.CurrentRow.Index != (dc.RowCount - 1))
                    {
                        dc.CurrentCell = dc.CurrentRow.Cells["加工说明"];


                    }

                    else  if (13 == dc.CurrentCell.ColumnIndex && dc.CurrentRow.Index != (dc.RowCount - 1))
                    {

                        //System.Windows.Forms.SendKeys.Send("{tab}");

                        dc.EndEdit();
                        this.ly_machinepart_process_outsourceBindingSource.EndEdit();

                      

                       this.ly_machinepart_process_outsourceTableAdapter.Update(this.lYOutsourceData .ly_machinepart_process_outsource );

                       this.ly_machinepart_process_outsourceBindingSource.Position = dc.CurrentRow.Index + 1;

                        dc.CurrentCell = ly_machinepart_processDataGridView.CurrentRow.Cells["准终时间"];


                    }


                    //if (12 == dc.CurrentCell.ColumnIndex && dc.CurrentRow.Index == (dc.RowCount - 1))
                    //{
                    //    ly_machinepart_processDataGridView.EndEdit();
                    //    ly_machinepart_processBindingSource.EndEdit();

                       

                    //    this.ly_machinepart_processTableAdapter.Update(this.lYMaterielRequirements.ly_machinepart_process);


                    //    ly_machinepart_processBindingSource.Position = 0;

                    //    ly_machinepart_processDataGridView.CurrentCell = ly_machinepart_processDataGridView.CurrentRow.Cells["准终时间"];

                     






                    //}
                    else
                    {

                        if (7 == dc.CurrentCell.ColumnIndex )
                        {
                            dc.CurrentCell = dc.CurrentRow.Cells["单件工时"];


                        }
                        else if (10 == dc.CurrentCell.ColumnIndex)
                        {
                            dc.CurrentCell = dc.CurrentRow.Cells["加工说明"];


                        }
                        else
                        {

                            ly_machinepart_processDataGridView.EndEdit();
                            ly_machinepart_process_outsourceBindingSource.EndEdit();



                            this.ly_machinepart_process_outsourceTableAdapter.Update(this.lYOutsourceData .ly_machinepart_process_outsource );


                            ly_machinepart_process_outsourceBindingSource.Position = 0;

                            ly_machinepart_processDataGridView.CurrentCell = ly_machinepart_processDataGridView.CurrentRow.Cells["准终时间"];
                        }

                        //System.Windows.Forms.SendKeys.Send("{tab}");
                    }
                    return true;
                }
            }
            return base.ProcessCmdKey(ref msg, keyData);
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

        private void ly_machinepart_processDataGridView_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView dgv = sender as DataGridView;

            if ("准终时间" == dgv.CurrentCell.OwningColumn.DataPropertyName
                   || "单件工时" == dgv.CurrentCell.OwningColumn.DataPropertyName
                   || "加工说明" == dgv.CurrentCell.OwningColumn.DataPropertyName)
            {

                dgv.CurrentCell.Style.BackColor = Color.White;
                dgv.CurrentCell.Style.ForeColor = Color.Teal;

            }
        }

        private void ly_machinepart_processDataGridView_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView dgv = sender as DataGridView;

            if ("准终时间" == dgv.CurrentCell.OwningColumn.DataPropertyName
                   || "单件工时" == dgv.CurrentCell.OwningColumn.DataPropertyName
                   || "加工说明" == dgv.CurrentCell.OwningColumn.DataPropertyName)
            {

                dgv.CurrentCell.Style.BackColor = Color.Teal;
                dgv.CurrentCell.Style.ForeColor = Color.White;

            }
        }

        //private void fillToolStripButton_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        this.ly_machinepart_process_outsourceTableAdapter.Fill(this.lYOutsourceData.ly_machinepart_process_outsource, itemnoToolStripTextBox.Text, contract_codeToolStripTextBox.Text, new System.Nullable<int>(((int)(System.Convert.ChangeType(id2ToolStripTextBox.Text, typeof(int))))));
        //    }
        //    catch (System.Exception ex)
        //    {
        //        System.Windows.Forms.MessageBox.Show(ex.Message);
        //    }

        //}

      

      
    }
}
