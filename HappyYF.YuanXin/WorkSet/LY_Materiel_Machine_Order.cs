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
using DataGridFilter;

namespace HappyYF.YuanXin.WorkSet
{
    public partial class LY_Materiel_Machine_Order : Form
    {
        private string sortcode;

        private string sortmode;

        
        private int selectionIdx = 0;



        public string Sortmode
        {
            get { return sortmode; }
            set { sortmode = value; }
        }


        public LY_Materiel_Machine_Order()
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
                this.Text = "采购物料供应商设置";
                this.sortcode = "3";

              
                this.groupBox3.Text = "采购物料列表";
            }
            else if ("WX" == this.Sortmode)
            {
                this.Text = "外协物料加工商设置";
                this.sortcode = "2";

             
                this.groupBox3.Text = "外协物料列表";
            }
            else if ("WT" == this.Sortmode)
            {
                this.Text = "机加物料跟单查询";
                this.sortcode = "4";

               
                this.groupBox3.Text = "机加物料列表";
            }

            this.ly_inma0010_sortTableAdapter.Fill(this.lYMaterielRequirements.ly_inma0010_sort, this.sortcode, this.sortcode);
        }

        private void LY_Materiel_Supplier_Set_Load(object sender, EventArgs e)
        {
            
            this.lY_productionorder_list_foritemnoTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
                   
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
            int id2 = int.Parse (dgv.Rows[e.RowIndex].Cells["id2"].Value.ToString());

            this.groupBox1.Text = itemno + ":跟单列表";

            this.lY_productionorder_list_foritemnoTableAdapter.Fill(this.lYQualityInspector.LY_productionorder_list_foritemno, itemno);
 
   
            //this.ly_materiel_supplierTableAdapter.Fill(this.lYMaterielRequirements.ly_materiel_supplier, itemno,id2);
            //this.ly_supplier_list_SelTableAdapter.Fill(this.lYMaterielRequirements.ly_supplier_list_Sel, this.sortcode, itemno,id2 );

           
        }

       


       
        private void ly_inma0010_sortDataGridView_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            DataGridView dgv = sender as DataGridView;

            if (null == dgv.CurrentRow) return;

            ////this.nowclientCode = ly_sales_businessDataGridView.CurrentRow.Cells["客户编码"].Value.ToString();
            ////this.nowcontractCode = ly_sales_businessDataGridView.CurrentRow.Cells["业务编码"].Value.ToString();

            //int now_id_main = int.Parse(dgv.CurrentRow.Cells["id_main"].Value.ToString());

            //string nowColumnName = dgv.CurrentCell.OwningColumn.Name;

            //this.ly_sales_contract_main_forbusinessTableAdapter.Fill(this.lYSalseMange.ly_sales_contract_main_forbusiness, this.nowcontractCode);

            //this.ly_sales_contract_main_forbusinessBindingSource.Position = this.ly_sales_contract_main_forbusinessBindingSource.Find("id", now_id_main);

            //dgv.CurrentCell = this.ly_sales_contract_mainDataGridView.CurrentRow.Cells[nowColumnName];
            
            ////}
            ////////////////////////////////////////////////////////////////////////////





            //if ("True" == dgv.CurrentRow.Cells["批准"].Value.ToString() && "000" != SQLDatabase.NowUserID)
            //{
            //    MessageBox.Show("合同已经执行,不能修改数据...", "注意");
            //    return;

            //}

            //////////////////////////////////////

            if ("加工周期" == dgv.CurrentCell.OwningColumn.Name)
            {

                //if ("True" == dgv.CurrentRow.Cells["种类存疑"].Value.ToString())
                //{
                //    dgv.CurrentRow.Cells["种类存疑"].Value = "False";

                //}
                //else
                //{

                //    dgv.CurrentRow.Cells["种类存疑"].Value = "True";
                //}



                //this.ly_inma0010_sortBindingSource.EndEdit();

                //this.ly_inma0010_sortTableAdapter.Update(this.lYMaterielRequirements.ly_inma0010_sort);



                //return;

                //////////////////////
                ChangeValue queryForm = new ChangeValue();

                queryForm.OldValue = dgv.CurrentCell.Value.ToString();
                queryForm.NewValue = "";
                queryForm.ChangeMode = "value";
                queryForm.ShowDialog();




                if (queryForm.NewValue != "")
                {
                    dgv.CurrentRow.Cells["加工周期"].Value = queryForm.NewValue;
                    //dgv.CurrentRow.Cells["discount_money"].Value = DBNull.Value;

                    //dgv.CurrentRow.Cells["approve_flag"].Value = "False";
                    //this.ly_production_order_materialrequisitionDataGridView.EndEdit();
                    //this.ly_production_order_materialrequisitionBindingSource.EndEdit();

                    //this.ly_production_order_materialrequisitionTableAdapter.Update(this.lYMaterielRequirements.ly_production_order_materialrequisition);


                    //CountPlanStru();

                }
                else
                {
                    //hT_Manage_ItemDataGridView.CurrentRow.Cells["apply_money"].Value = queryForm.NewValue;
                    dgv.CurrentRow.Cells["加工周期"].Value = DBNull.Value;
                    //dgv.CurrentRow.Cells["apply_money"].Value = DBNull.Value;
                    //dgv.CurrentRow.Cells["approve_flag"].Value = "False";
                    //SaveChanged();

                }


                this.ly_inma0010_sortBindingSource.EndEdit();

                this.ly_inma0010_sortTableAdapter.Update(this.lYMaterielRequirements.ly_inma0010_sort);

                return;

                //////////////////////

            }

            /////////////////////////////////////
        }

        //private void fillToolStripButton_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        this.lY_productionorder_list_foritemnoTableAdapter.Fill(this.lYQualityInspector.LY_productionorder_list_foritemno, itemnoToolStripTextBox.Text);
            
            
        //    }
        //    catch (System.Exception ex)
        //    {
        //        System.Windows.Forms.MessageBox.Show(ex.Message);
        //    }

        //}

        //private void fillToolStripButton_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        this.lY_productionorder_listTableAdapter.Fill(this.lYQualityInspector.LY_productionorder_list, new System.Nullable<System.DateTime>(((System.DateTime)(System.Convert.ChangeType(begindateToolStripTextBox.Text, typeof(System.DateTime))))), new System.Nullable<System.DateTime>(((System.DateTime)(System.Convert.ChangeType(enddateToolStripTextBox.Text, typeof(System.DateTime))))));
        //    }
        //    catch (System.Exception ex)
        //    {
        //        System.Windows.Forms.MessageBox.Show(ex.Message);
        //    }

        //}

       

      

       

      
       





    }
}
