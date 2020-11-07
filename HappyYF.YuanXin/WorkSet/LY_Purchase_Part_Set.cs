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
    public partial class LY_Purchase_Part_Set : Form
    {
        private string sortcode;

        private string sortmode;

   


        public string Sortmode
        {
            get { return sortmode; }
            set { sortmode = value; }
        }


        public LY_Purchase_Part_Set()
        {
            InitializeComponent();
        }

        public void LoadSingleData(string nowitemno)
        {
            this.toolStripTextBox1.Visible = false;
            this.toolStripLabel1.Visible = false;

            this.ly_inma0010_sortBindingSource.Filter = "物资编号='" + nowitemno + "'";
        
        }

        public void LoadData()
        {

            if ("CG" == this.Sortmode)
            {
                this.Text = "协同采购设置";
                this.sortcode = "3";

                //this.groupBox2.Text = "可选采购物料供应商";
                //this.groupBox3.Text = "采购物料列表";
            }
            else if ("WX" == this.Sortmode)
            {
                this.Text = "外协物料加工商设置";
                this.sortcode = "2";

                //this.groupBox2.Text = "可选外协物料加工商";
                //this.groupBox3.Text = "外协物料列表";
            }
            else if ("WT" == this.Sortmode)
            {
                this.Text = "机加工艺委托商设置";
                this.sortcode = "4";

                //this.groupBox2.Text = "可选机加工艺委托商";
                //this.groupBox3.Text = "机加物料列表";
            }

            this.ly_inma0010_sortTableAdapter.Fill(this.lYMaterielRequirements.ly_inma0010_sort, this.sortcode, this.sortcode);
        }

        private void LY_Materiel_Supplier_Set_Load(object sender, EventArgs e)
        {

            this.ly_purchase_partTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            this.ly_inma0010_sortTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
          
            LoadData();

        }

        private void toolStripTextBox1_Enter(object sender, EventArgs e)
        {
            toolStripTextBox1.Text = "";

            this.ly_inma0010_sortBindingSource.Filter = "";
        }

        private void toolStripTextBox1_KeyUp(object sender, KeyEventArgs e)
        {
            string filterString = GetDataGridviewMultiFilterString.DGVMultiFilterString(this.ly_inma0010_sortDataGridView, this.toolStripTextBox1.Text);



            if (null == filterString)
                filterString = "";

            this.ly_inma0010_sortBindingSource.Filter = filterString;
        }

      

        private void ly_inma0010_sortDataGridView_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView dgv = sender as DataGridView;

            if (null == dgv.CurrentRow) return;


            string itemno = dgv.Rows[e.RowIndex].Cells["物资编号"].Value.ToString();//ly_inma0010_sortDataGridView


            this.ly_purchase_partTableAdapter.Fill(this.lYMaterielRequirements.ly_purchase_part, itemno);
                
          
        }

       

        private void SaveChanged()
        {
            
            
            this.Validate();

            int instoreflag = CheckInstoreNum();

            if (instoreflag > 1)
            {

                MessageBox.Show("协同采购选项只能有一项选择入库,操作取消...", "注意");
                return;

            }


                    
            ///////////////////////////


            this.ly_purchase_partBindingSource.EndEdit();

            this.ly_purchase_partTableAdapter.Update(this.lYMaterielRequirements.ly_purchase_part);
        }

        private int CheckInstoreNum()
        {
            int instoreflag = 0;

            //////////////////////////

            foreach (DataGridViewRow dgr in ly_purchase_partDataGridView.Rows)
            {



                if ("True" == dgr.Cells["入库"].Value.ToString())
                {
                    instoreflag = instoreflag + 1;

                }



            }
            return instoreflag;
        }

        private void bindingNavigatorAddNewItem1_Click(object sender, EventArgs e)
        {
           

             if (null == ly_inma0010_sortDataGridView.CurrentRow) return;

            string message = "增加协同采购选项吗？";
            string caption = "提示...";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;

            DialogResult result;



            result = MessageBox.Show(message, caption, buttons,
            MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

            if (result == DialogResult.Yes)
            {

                foreach (DataGridViewRow dgr in ly_purchase_partDataGridView.Rows)
                {

                    dgr.Cells["入库"].Value = "False";



                }
                
                this.ly_purchase_partBindingSource.AddNew();

                string nowitem = this.ly_inma0010_sortDataGridView.CurrentRow.Cells["物资编号"].Value.ToString();
                this.ly_purchase_partDataGridView.CurrentRow.Cells["物料编号"].Value = nowitem;


                this.ly_purchase_partDataGridView.CurrentRow.Cells["入库"].Value = "True";

               
                SaveChanged();
            }
        }

        private void 保存SToolStripButton_Click(object sender, EventArgs e)
        {
            SaveChanged();
        }

        private void bindingNavigatorDeleteItem1_Click(object sender, EventArgs e)
        {
            if (null == this.ly_purchase_partDataGridView.CurrentRow) return;



            string message1 = "当前协同采购选项将被删除，继续吗？";
            string caption1 = "提示...";
            MessageBoxButtons buttons1 = MessageBoxButtons.YesNo;

            DialogResult result1;



            result1 = MessageBox.Show(message1, caption1, buttons1,
            MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

            if (result1 == DialogResult.Yes)
            {
                if ("True" == ly_purchase_partDataGridView.CurrentRow.Cells["入库"].Value.ToString())
                {
                    if (ly_purchase_partDataGridView.Rows.Count >= 2)
                    {
                        this.ly_purchase_partBindingSource.RemoveCurrent();
                        ly_purchase_partDataGridView.Rows[ly_purchase_partDataGridView.Rows.Count - 1].Cells["入库"].Value = "True";
                    }
                    else
                    {
                        this.ly_purchase_partBindingSource.RemoveCurrent();
                    }
                }

                else
                {
                    this.ly_purchase_partBindingSource.RemoveCurrent();
                }


                SaveChanged();




            }
        }

        private void ly_purchase_partDataGridView_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {

            if (null == this.ly_purchase_partDataGridView.CurrentRow) return;
            
            
            DataGridView dgv = sender as DataGridView;



            ///////////////////////////////////////////////////////

            if ("协同描述" == dgv.CurrentCell.OwningColumn.Name)
            {

                ChangeValue queryForm = new ChangeValue();

                queryForm.OldValue = dgv.CurrentCell.Value.ToString();
                queryForm.NewValue = "";
                //queryForm.ChangeMode = "value";
                queryForm.ShowDialog();




                if (queryForm.NewValue != "")
                {
                    dgv.CurrentRow.Cells["协同描述"].Value = queryForm.NewValue;
                    //dgv.CurrentRow.Cells["discount_money"].Value = DBNull.Value;

                    //dgv.CurrentRow.Cells["approve_flag"].Value = "False";
                    SaveChanged();


               

                }
              
                return;

            }

            /////////////////////////////

            /////////////////////////////

            if ("入库" == dgv.CurrentCell.OwningColumn.Name)
            {


                if ("True" == dgv.CurrentRow.Cells["入库"].Value.ToString())
                {
                    dgv.CurrentRow.Cells["入库"].Value = "False";
                    dgv.Rows[dgv.Rows.Count - 1].Cells["入库"].Value = "True";

                }
                else
                {
                    foreach (DataGridViewRow dgr in dgv.Rows)
                    {

                        dgr.Cells["入库"].Value = "False";



                    }
                    dgv.CurrentRow.Cells["入库"].Value = "True";
                }



                SaveChanged();

              

             

              



                return;
            }


            ///////////////////////////////////////////////////////
        }

       
     


    }
}
