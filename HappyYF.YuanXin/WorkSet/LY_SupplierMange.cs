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
using System.Transactions;

namespace HappyYF.YuanXin.WorkSet
{
    public partial class LY_SupplierMange : Form
    {
        private string sortmode;
        public string Nowsupplier_code;

        public string Sortmode
        {
            get { return sortmode; }
            set { sortmode = value; }
        }

        private string sortcode;

        public string Sortcode
        {
            get { return sortcode; }
            set { sortcode = value; }
        }
        
        public LY_SupplierMange()
        {
            InitializeComponent();
        }

        private void ly_inma0010BindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            //this.Validate();
            //this.ly_inma0010ylBindingSource.EndEdit();
            //this.ly_inma0010ylTableAdapter.Update(this.lYMaterialMange.ly_inma0010yl );

        }
        public void LoadData()
        {

            if ("CG" == this.Sortmode)
            {
                this.Text = "采购供应商管理";
                this.Sortcode = "3";
            }
            else if ("WX" == this.Sortmode)
            {
                this.Text = "外协加工商管理";
                this.Sortcode = "2";
            }
            else if ("WT" == this.Sortmode)
            {
                this.Text = "机加委托商管理";
                this.Sortcode = "4";
            }
            else if ("NP" == this.Sortmode)
            {
                this.Text = "非采购供应商管理";
                this.Sortcode = "5";
            }
            this.ly_supplier_listTableAdapter.Fill(this.lYMaterielRequirements.ly_supplier_list, this.Sortcode);

        }

        private void LY_MaterialMange_Load(object sender, EventArgs e)
        {

            if ("CG" == this.Sortmode)
            {
                this.Text = "采购供应商管理";
                this.Sortcode = "3";
            }
            else if ("WX" == this.Sortmode)
            {
                this.Text = "外协加工商管理";
                this.Sortcode = "2";
            }
            else if ("WT" == this.Sortmode)
            {
                this.Text = "机加委托商管理";
                this.Sortcode = "4";
            }
            else if ("NP" == this.Sortmode)
            {
                this.Text = "非采购供应商管理";
                this.Sortcode = "5";
            }


            this.ly_contract_terms_forsupplierTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;

            this.ly_materiel_supplier_MOQTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;

            this.ly_inma0010_sort1TableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            
            this.ly_supplier_listTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            this.ly_supplier_listTableAdapter.Fill(this.lYMaterielRequirements.ly_supplier_list,this .Sortcode );



            this.ly_materiel_supplier_viewTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            //this.ly_materiel_supplier_viewTableAdapter.Fill(this.lYMaterielRequirements.ly_materiel_supplier_view, supplier_codeToolStripTextBox.Text);
            
            
            //this.ly_inma0010ylTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
           
            //this.ly_inma0010ylTableAdapter.Fill(this.lYMaterialMange.ly_inma0010yl);

        }

        private void toolStripTextBox1_Enter(object sender, EventArgs e)
        {
            toolStripTextBox1.Text = "";

            this.ly_supplier_listBindingSource.Filter = "";
        }

        private void toolStripTextBox1_KeyUp(object sender, KeyEventArgs e)
        {


            string filterString = GetDataGridviewMultiFilterString.DGVMultiFilterString(this.ly_supplier_listDataGridView, this.toolStripTextBox1.Text);

            //this.sMXBK_HumanMainBindingSource.Filter = GetDataGridviewMultiFilterString.DGVMultiFilterString(this.sMXBK_HumanMainDataGridView, this.toolStripTextBox2.Text);


            if (null == filterString)
                filterString = "";



            this.ly_supplier_listBindingSource.Filter = filterString;

            ////for (int i = 0; i < this.hT_Vehicle_collectDataGridView.ColumnCount; i++)
            //for (int i = 1; i < 11; i++)
            //{
            //    string tempColumnName = this.ly_inma0010DataGridView.Columns[i].DataPropertyName;
              
            //    if (i != 10)
            //        dFilter = dFilter + tempColumnName + " like  '*" + this.toolStripTextBox1.Text + "*' or ";
            //    else
            //        dFilter = dFilter + tempColumnName + " like  '*" + this.toolStripTextBox1.Text + "*' ";

            //}

            //if (this.toolStripTextBox1.Text.Replace(" ", "").Length > 0)

            //    this.ly_inma0010ylBindingSource.Filter =  dFilter;
            //else
            //    this.ly_inma0010ylBindingSource.Filter = " ";
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            ExportDataGridviewTOExcell.ExportDataGridview(this.ly_supplier_listDataGridView, true);
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            FilterForm filterForm = new FilterForm();

         

            List<string> ls = new List<string>();
            ls.Add("id");


            filterForm.SetSourceColumns(ly_supplier_listDataGridView.Columns, ls);

            filterForm.ShowDialog();

            this.ly_supplier_listBindingSource.Filter = filterForm.GetFilterString();
        }

        private void bindingNavigatorAddNewItem_Click(object sender, EventArgs e)
        {
            ////////////////////////////////////



            LY_SupplierAdd queryForm = new LY_SupplierAdd();

            queryForm.supplier_code  = "";
            queryForm.runmode = "增加";
            queryForm.sortmode  = this .sortmode;
            queryForm.Sortcode = this.sortcode;

            queryForm.StartPosition = FormStartPosition.CenterParent;
            queryForm.ShowDialog();

            if (queryForm.DialogResult != DialogResult.Cancel)
            {
                this.ly_supplier_listTableAdapter.Fill(this.lYMaterielRequirements.ly_supplier_list,this .Sortcode);
                this.ly_supplier_listBindingSource.Position = this.ly_supplier_listBindingSource.Find("编码", queryForm.supplier_code );

                this.Nowsupplier_code = queryForm.supplier_code;
            }

            /////////////////////////////////////////

           
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if (null == ly_supplier_listDataGridView.CurrentRow) return;
            string s = this.ly_supplier_listDataGridView.CurrentRow.Cells["编码"].Value.ToString();
            //int loanid = int.Parse(this.xD_Client_LoanDataGridView.CurrentRow.Cells["Id_loan"].Value.ToString());
            this.Nowsupplier_code = s;
            LY_SupplierAdd queryForm = new LY_SupplierAdd();

            queryForm.sortmode  = this .sortmode ;
            queryForm.runmode = "修改";
            queryForm.supplier_code  = s;
            queryForm.Sortcode = this.sortcode;

            queryForm.StartPosition = FormStartPosition.CenterParent;
            queryForm.ShowDialog();

            if (queryForm.DialogResult != DialogResult.Cancel)
            {
                this.ly_supplier_listTableAdapter.Fill(this.lYMaterielRequirements.ly_supplier_list,this .sortcode );
                this.ly_supplier_listBindingSource.Position = this.ly_supplier_listBindingSource.Find("编码", s );

               
            }

        }

        private void bindingNavigatorDeleteItem_Click(object sender, EventArgs e)
        {
            string message = "确定删除当前供应商记录吗？";
            string caption = "提示...";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result;



            result = MessageBox.Show(message, caption, buttons,
            MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

            if (result == DialogResult.Yes)
            {

                this.ly_supplier_listBindingSource.RemoveCurrent();


                ly_supplier_listDataGridView.EndEdit();
                ly_supplier_listBindingSource.EndEdit();


                this.ly_supplier_listTableAdapter.Update(this .lYMaterielRequirements .ly_supplier_list);

                //string s = this.xD_Sel_SellBalanceDataGridView.CurrentRow.Cells["编号"].Value.ToString();

                //this.hS_ClientPaymentTableAdapter.Fill(this.xD_SellBalance.HS_ClientPayment, s);


            }
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            //if (null == this.ly_inma0010DataGridView.CurrentRow) return;
            //SortForm DataSort = new SortForm();

            //List<string> ls = new List<string>();
            //ls.Add("id");


            //DataSort.SetSortColumns(this.lYMaterialMange.ly_inma0010yl.Columns, ls);
            //DataSort.ShowDialog();
            //this.ly_inma0010ylBindingSource.Sort = DataSort.GetSortString();
        }

        private void LY_SupplierMange_Activated(object sender, EventArgs e)
        {
            //LoadData();
        }

      

        private void ly_supplier_listDataGridView_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (null == this.ly_supplier_listDataGridView.CurrentRow) return;


            string suppliercode = this.ly_supplier_listDataGridView.Rows[e.RowIndex].Cells["编码"].Value.ToString();
            this.Nowsupplier_code = suppliercode;
            this.ly_materiel_supplier_viewTableAdapter.Fill(this.lYMaterielRequirements.ly_materiel_supplier_view, suppliercode);
            this.ly_inma0010_sort1TableAdapter.Fill(this.lYMaterielRequirements.ly_inma0010_sort1, this.Sortcode, suppliercode);

            saveContractTerm();
            this.ly_contract_terms_forsupplierTableAdapter.Fill(this.lYMaterielRequirements.ly_contract_terms_forsupplier, suppliercode);

           

        }

        private void ly_materiel_supplier_viewDataGridView_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            DataGridView dv = sender as DataGridView;

            dv.Rows[e.RowIndex].Cells["Column1"].Value = e.RowIndex + 1;
        }

        private void toolStripTextBox2_KeyUp(object sender, KeyEventArgs e)
        {
            string filterString = GetDataGridviewMultiFilterString.DGVMultiFilterString(this.ly_inma0010_sortDataGridView, this.toolStripTextBox2.Text);



            if (null == filterString)
                filterString = "";

            this.ly_inma0010_sort1BindingSource.Filter = filterString;
        }

        private void toolStripTextBox2_Enter(object sender, EventArgs e)
        {
            toolStripTextBox2.Text = "";

            this.ly_inma0010_sort1BindingSource.Filter = "";
        }

        private void ly_inma0010_sortDataGridView_DoubleClick(object sender, EventArgs e)
        {
            if (null == ly_inma0010_sortDataGridView.CurrentRow) return;
            

            string supplierNum = this.ly_supplier_listDataGridView.CurrentRow.Cells["编码"].Value.ToString();
            string itemno = ly_inma0010_sortDataGridView.CurrentRow.Cells["物资编号"].Value.ToString();
            int id2 = int.Parse(ly_inma0010_sortDataGridView.CurrentRow.Cells["id2"].Value.ToString());


            this.Nowsupplier_code = supplierNum;


            string insStr = " INSERT INTO ly_materiel_supplier  " +
               "( itemno,supplier_code,id2) " +
               " values ('" + itemno + "','" + supplierNum + "'," + id2 + " )";


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



                this.ly_materiel_supplier_viewTableAdapter.Fill(this.lYMaterielRequirements.ly_materiel_supplier_view, supplierNum);
                this.ly_inma0010_sort1TableAdapter.Fill(this.lYMaterielRequirements.ly_inma0010_sort1, this.Sortcode, supplierNum);
            //string componentNum = this.bom_material_selDataGridView.CurrentRow.Cells["物资编号1"].Value.ToString();
                this.ly_materiel_supplier_viewBindingSource.Position = this.ly_materiel_supplier_viewBindingSource.Find("物料编码", itemno);
        }

        private void ly_materiel_supplier_viewDataGridView_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            DataGridView dgv = sender as DataGridView;




            if ("单价" == dgv.CurrentCell.OwningColumn.Name)
            {

                ChangeValue queryForm = new ChangeValue();

                queryForm.OldValue = dgv.CurrentCell.Value.ToString();
                queryForm.NewValue = "";
                queryForm.ChangeMode = "value";
                queryForm.ShowDialog();




                if (queryForm.NewValue != "")
                {
                    dgv.CurrentRow.Cells["单价"].Value = queryForm.NewValue;
                    //dgv.CurrentRow.Cells["discount_money"].Value = DBNull.Value;

                    //dgv.CurrentRow.Cells["approve_flag"].Value = "False";
                    SaveChanged();


                    //CountPlanStru();

                }
                else
                {
                    //hT_Manage_ItemDataGridView.CurrentRow.Cells["apply_money"].Value = queryForm.NewValue;
                    //dgv.CurrentRow.Cells["discount_money"].Value = DBNull.Value;
                    //dgv.CurrentRow.Cells["apply_money"].Value = DBNull.Value;
                    //dgv.CurrentRow.Cells["approve_flag"].Value = "False";
                    //SaveChanged();

                }
                return;

            }


            ///////////////////////////////////////////////////////

            //if ("购买比例" == dgv.CurrentCell.OwningColumn.Name)
            //{

            //    ChangeValue queryForm = new ChangeValue();

            //    queryForm.OldValue = dgv.CurrentCell.Value.ToString();
            //    queryForm.NewValue = "";
            //    queryForm.ChangeMode = "value";
            //    queryForm.ShowDialog();




            //    if (queryForm.NewValue != "")
            //    {


            //        decimal sumratio = decimal.Parse(queryForm.NewValue);
            //        decimal tempvalue = 0;

            //        if (0 >= sumratio || 100 < sumratio)
            //        {

            //            MessageBox.Show("购买比例应 大于0 小于等于100 ...", "提示");
            //            return;
            //        }

            //        foreach (DataGridViewRow dgr in ly_materiel_supplierDataGridView.Rows)
            //        {

            //            if (dgr.Index != dgv.CurrentRow.Index)
            //            {
            //                if (string.IsNullOrEmpty(dgr.Cells["购买比例"].Value.ToString()))
            //                {
            //                    tempvalue = 0;
            //                }
            //                else
            //                {
            //                    tempvalue = decimal.Parse(dgr.Cells["购买比例"].Value.ToString());
            //                }
            //                sumratio = sumratio + tempvalue;
            //            }

            //        }

            //        if (100 < sumratio)
            //        {

            //            MessageBox.Show("所有供应商比例和 不能大于 100 ...", "提示");
            //            return;
            //        }

            //        dgv.CurrentRow.Cells["购买比例"].Value = queryForm.NewValue;
            //        SaveChanged();




            //    }
            //    else
            //    {


            //    }
            //    return;

            //}


            /////////////////////////////////////////////////////////

            //if ("顺序" == dgv.CurrentCell.OwningColumn.Name)
            //{

            //    ChangeValue queryForm = new ChangeValue();

            //    queryForm.OldValue = dgv.CurrentCell.Value.ToString();
            //    queryForm.NewValue = "";
            //    queryForm.ChangeMode = "value";
            //    queryForm.ShowDialog();




            //    if (queryForm.NewValue != "")
            //    {
            //        dgv.CurrentRow.Cells["顺序"].Value = queryForm.NewValue;
            //        //dgv.CurrentRow.Cells["discount_money"].Value = DBNull.Value;

            //        //dgv.CurrentRow.Cells["approve_flag"].Value = "False";
            //        SaveChanged();


            //        //CountPlanStru();

            //    }
            //    else
            //    {
            //        //hT_Manage_ItemDataGridView.CurrentRow.Cells["apply_money"].Value = queryForm.NewValue;
            //        //dgv.CurrentRow.Cells["discount_money"].Value = DBNull.Value;
            //        //dgv.CurrentRow.Cells["apply_money"].Value = DBNull.Value;
            //        //dgv.CurrentRow.Cells["approve_flag"].Value = "False";
            //        //SaveChanged();

            //    }
            //    return;

            //}


            ///////////////////////////////////////////////////////

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
                    //dgv.CurrentRow.Cells["discount_money"].Value = DBNull.Value;

                    //dgv.CurrentRow.Cells["approve_flag"].Value = "False";
                    SaveChanged();


                    //CountPlanStru();

                }
                else
                {
                    //hT_Manage_ItemDataGridView.CurrentRow.Cells["apply_money"].Value = queryForm.NewValue;
                    //dgv.CurrentRow.Cells["discount_money"].Value = DBNull.Value;
                    //dgv.CurrentRow.Cells["apply_money"].Value = DBNull.Value;
                    //dgv.CurrentRow.Cells["approve_flag"].Value = "False";
                    //SaveChanged();

                }
                return;

            }
            /////////////////////////////////////////////////////

            if ("供方编号" == dgv.CurrentCell.OwningColumn.Name)
            {

                ChangeValue queryForm = new ChangeValue();

                queryForm.OldValue = dgv.CurrentCell.Value.ToString();
                queryForm.NewValue = "";
                queryForm.ChangeMode = "string";
                queryForm.ShowDialog();




                if (queryForm.NewValue != "")
                {
                    dgv.CurrentRow.Cells["供方编号"].Value = queryForm.NewValue;
                    //dgv.CurrentRow.Cells["discount_money"].Value = DBNull.Value;

                    //dgv.CurrentRow.Cells["approve_flag"].Value = "False";
                    SaveChanged();


                    //CountPlanStru();

                }
                else
                {
                    //hT_Manage_ItemDataGridView.CurrentRow.Cells["apply_money"].Value = queryForm.NewValue;
                    //dgv.CurrentRow.Cells["discount_money"].Value = DBNull.Value;
                    //dgv.CurrentRow.Cells["apply_money"].Value = DBNull.Value;
                    //dgv.CurrentRow.Cells["approve_flag"].Value = "False";
                    //SaveChanged();

                }
                return;

            }

            /////////////////////////////////////////////////////


            if ("供货提前期" == dgv.CurrentCell.OwningColumn.Name)
            {
                ChangeValue queryForm = new ChangeValue();

                queryForm.OldValue = dgv.CurrentCell.Value.ToString();
                queryForm.NewValue = "";
                queryForm.ChangeMode = "value";
                queryForm.ShowDialog();




                if (queryForm.NewValue != "")
                {
                    dgv.CurrentRow.Cells["供货提前期"].Value = queryForm.NewValue;
                    //dgv.CurrentRow.Cells["discount_money"].Value = DBNull.Value;

                    //dgv.CurrentRow.Cells["approve_flag"].Value = "False";
                    SaveChanged();

                }
                else
                {


                }
                return;
            }
        }

        private void SaveChanged()
        {
            this.ly_materiel_supplier_viewDataGridView.EndEdit();
            this.ly_materiel_supplier_viewBindingSource.EndEdit();

            this.ly_materiel_supplier_viewTableAdapter.Update(this.lYMaterielRequirements.ly_materiel_supplier_view);
        }

        private void bindingNavigatorDeleteItem1_Click(object sender, EventArgs e)
        {
            string message = "确定删除当前记录吗？";
            string caption = "提示...";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result;



            result = MessageBox.Show(message, caption, buttons,
            MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

            if (result == DialogResult.Yes)
            {

                this.ly_materiel_supplier_viewBindingSource.RemoveCurrent();


                SaveChanged();


            }
        }

        private void toolStripButton11_Click(object sender, EventArgs e)
        {
            ExportDataGridviewTOExcell.ExportDataGridview(this.ly_materiel_supplier_viewDataGridView, true);
        }

        private void ly_materiel_supplier_viewDataGridView_SelectionChanged(object sender, EventArgs e)
        {
            if (null == ly_materiel_supplier_viewDataGridView.CurrentRow)
            {
                //this.ly_sales_receive_itemDetail_repairTableAdapter.Fill(this.lYSalseMange2.ly_sales_receive_itemDetail_repair, -1);
                //this.ly_sales_receive_itemDetail_repair_wasteTableAdapter.Fill(this.lYSalseMange2.ly_sales_receive_itemDetail_repair_waste, -11);
                //this.ly_sales_receive_itemDetail_repair_returnTableAdapter.Fill(this.lYSalseMange2.ly_sales_receive_itemDetail_repair_return, -11);

                this.ly_materiel_supplier_MOQTableAdapter.Fill(this.lYMaterielRequirements.ly_materiel_supplier_MOQ, -1);

                return;
            }


            int nowId = int.Parse(ly_materiel_supplier_viewDataGridView.CurrentRow.Cells["moqId"].Value.ToString());

            this.ly_materiel_supplier_MOQTableAdapter.Fill(this.lYMaterielRequirements.ly_materiel_supplier_MOQ, nowId);

            //this.ly_sales_receive_itemDetail_repair_returnTableAdapter.Fill(this.lYSalseMange2.ly_sales_receive_itemDetail_repair_return, nowId);
            //this.ly_sales_receive_itemDetail_repairTableAdapter.Fill(this.lYSalseMange2.ly_sales_receive_itemDetail_repair, nowId);
            //this.ly_sales_receive_itemDetail_repair_wasteTableAdapter.Fill(this.lYSalseMange2.ly_sales_receive_itemDetail_repair_waste, nowId);
          

        }

        private void toolStripButton12_Click(object sender, EventArgs e)
        {



            int nowparent_id = int.Parse(ly_materiel_supplier_viewDataGridView.CurrentRow.Cells["moqId"].Value.ToString());



            this.ly_materiel_supplier_MOQBindingSource.AddNew();


            this.ly_materiel_supplier_MOQDataGridView.CurrentRow.Cells["parent_id"].Value = nowparent_id;





            this.ly_materiel_supplier_MOQBindingSource.EndEdit();

            this.ly_materiel_supplier_MOQTableAdapter.Update(this.lYMaterielRequirements.ly_materiel_supplier_MOQ);

            string suppliercode = this.ly_supplier_listDataGridView.CurrentRow.Cells["编码"].Value.ToString();

            this.ly_materiel_supplier_viewTableAdapter.Fill(this.lYMaterielRequirements.ly_materiel_supplier_view, suppliercode);

            this.ly_materiel_supplier_viewBindingSource.Position = this.ly_materiel_supplier_viewBindingSource.Find("id", nowparent_id);
        
        
        }

        private void ly_materiel_supplier_MOQDataGridView_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            DataGridView dgv = sender as DataGridView;

            if (null == dgv.CurrentRow) return;


            //if ("True" == dgv.CurrentRow.Cells["审批"].Value.ToString())
            //{
            //    MessageBox.Show("依赖书已经生产审批,不能修改数据...", "注意");
            //    return;

            //}





            if ("起订量" == dgv.CurrentCell.OwningColumn.Name)
            {

                if ("True" == dgv.CurrentRow.Cells["审批"].Value.ToString())
                {

                    MessageBox.Show("已经批准,不能修改数据...", "注意");
                    return;
                }

                ChangeValue queryForm = new ChangeValue();

                queryForm.OldValue = dgv.CurrentCell.Value.ToString();
                queryForm.NewValue = "";
                queryForm.ChangeMode = "value";
                queryForm.ShowDialog();




                if (queryForm.NewValue != "")
                {



                    dgv.CurrentRow.Cells["起订量"].Value = queryForm.NewValue;


                    //dgv.CurrentRow.Cells["discount_money"].Value = DBNull.Value;

                    //dgv.CurrentRow.Cells["approve_flag"].Value = "False";ly_plan_getmaterialDataGridView

                


                  


                }
                else
                {
                    //hT_Manage_ItemDataGridView.CurrentRow.Cells["apply_money"].Value = queryForm.NewValue;
                    dgv.CurrentRow.Cells["起订量"].Value = DBNull.Value;
                    //dgv.CurrentRow.Cells["apply_money"].Value = DBNull.Value;
                    //dgv.CurrentRow.Cells["approve_flag"].Value = "False";
                    //SaveChanged();

                }

                SaveMOQ();
                return;

            }



            ///////////////////////////////////////////////////////////////

            if ("单价MOQ" == dgv.CurrentCell.OwningColumn.Name)
            {

                if ("True" == dgv.CurrentRow.Cells["审批"].Value.ToString())
                {

                    MessageBox.Show("已经批准,不能修改数据...", "注意");
                    return;
                }

                ChangeValue queryForm = new ChangeValue();

                queryForm.OldValue = dgv.CurrentCell.Value.ToString();
                queryForm.NewValue = "";
                queryForm.ChangeMode = "value";
                queryForm.ShowDialog();




                if (queryForm.NewValue != "")
                {



                    dgv.CurrentRow.Cells["单价MOQ"].Value = queryForm.NewValue;


                    //dgv.CurrentRow.Cells["discount_money"].Value = DBNull.Value;

                    //dgv.CurrentRow.Cells["approve_flag"].Value = "False";ly_plan_getmaterialDataGridView

                  


                  


                }
                else
                {

                    dgv.CurrentRow.Cells["单价MOQ"].Value = DBNull.Value;
                    //hT_Manage_ItemDataGridView.CurrentRow.Cells["apply_money"].Value = queryForm.NewValue;
                    //dgv.CurrentRow.Cells["discount_money"].Value = DBNull.Value;
                    //dgv.CurrentRow.Cells["apply_money"].Value = DBNull.Value;
                    //dgv.CurrentRow.Cells["approve_flag"].Value = "False";
                    //SaveChanged();

                }

                SaveMOQ();
                return;

            }



            ///////////////////////////////////////////////////////////////
            if ("提前期MOQ" == dgv.CurrentCell.OwningColumn.Name)
            {

                if ("True" == dgv.CurrentRow.Cells["审批"].Value.ToString())
                {

                    MessageBox.Show("已经批准,不能修改数据...", "注意");
                    return;
                }

                ChangeValue queryForm = new ChangeValue();

                queryForm.OldValue = dgv.CurrentCell.Value.ToString();
                queryForm.NewValue = "";
                queryForm.ChangeMode = "value";
                queryForm.ShowDialog();




                if (queryForm.NewValue != "")
                {



                    dgv.CurrentRow.Cells["提前期MOQ"].Value = queryForm.NewValue;


                    //dgv.CurrentRow.Cells["discount_money"].Value = DBNull.Value;

                    //dgv.CurrentRow.Cells["approve_flag"].Value = "False";ly_plan_getmaterialDataGridView

                   


                  


                }
                else
                {
                    //hT_Manage_ItemDataGridView.CurrentRow.Cells["apply_money"].Value = queryForm.NewValue;
                    dgv.CurrentRow.Cells["提前期MOQ"].Value = DBNull.Value;
                    //dgv.CurrentRow.Cells["apply_money"].Value = DBNull.Value;
                    //dgv.CurrentRow.Cells["approve_flag"].Value = "False";
                    //SaveChanged();

                }

                SaveMOQ();
                return;

            }



            ///////////////////////////////////////////////////////////////

          
            ////////////////////////////////////////////////////////////////////////

            /////////////////////////////
            if ("备注MOQ" == dgv.CurrentCell.OwningColumn.Name)
            {


                if ("True" == dgv.CurrentRow.Cells["审批"].Value.ToString())
                {

                    MessageBox.Show("已经批准,不能修改数据...", "注意");
                    return;
                }

                
                ChangeValue queryForm = new ChangeValue();

                queryForm.OldValue = dgv.CurrentCell.Value.ToString();
                queryForm.NewValue = "";
                queryForm.ChangeMode = "longstring";
                queryForm.ShowDialog();




                if (queryForm.NewValue != "")
                {
                    dgv.CurrentRow.Cells["备注MOQ"].Value = queryForm.NewValue;


                }
                else
                {
                    dgv.CurrentRow.Cells["备注MOQ"].Value = DBNull.Value;

                }
                SaveMOQ();
              
                return;

            }
            /////////////////////////////////////////////////////////////////

            if ("审批" == dgv.CurrentCell.OwningColumn.Name)
            {
                if (!SQLDatabase.CheckHaveRight(SQLDatabase.NowUserID, "MOQ审批"))
                {
                    MessageBox.Show("无MOQ审批权限,操作取消...", "注意");
                    return;

                }


                if ("True" == dgv.CurrentRow.Cells["审批"].Value.ToString())
                {
                    dgv.CurrentRow.Cells["审批"].Value = "False";

                    dgv.CurrentRow.Cells["审批人"].Value = DBNull.Value;
                    dgv.CurrentRow.Cells["审批日期"].Value = DBNull.Value;

                }
                else
                {

                    dgv.CurrentRow.Cells["审批"].Value = "True";

                    dgv.CurrentRow.Cells["审批人"].Value = SQLDatabase.nowUserName();
                    dgv.CurrentRow.Cells["审批日期"].Value = SQLDatabase.GetNowdate();

                }


                SaveMOQ();



                return;

            }
            ////////////////////////////////////////////////////////////////////////
            /////////////////////////////////
            ////if ("质检意见" == dgv.CurrentCell.OwningColumn.Name)
            ////{

            ////    ChangeValue queryForm = new ChangeValue();

            ////    queryForm.OldValue = dgv.CurrentCell.Value.ToString();
            ////    queryForm.NewValue = "";
            ////    queryForm.ChangeMode = "longstring";
            ////    queryForm.ShowDialog();




            ////    if (queryForm.NewValue != "")
            ////    {
            ////        dgv.CurrentRow.Cells["质检意见"].Value = queryForm.NewValue;


            ////    }
            ////    else
            ////    {
            ////        dgv.CurrentRow.Cells["质检意见"].Value = DBNull.Value;

            ////    }

            ////    SaveDetail();
            ////    return;

            ////}
            /////////////////////////////////////////////////////////////////////

            ///////////////////////////////////////////////////////////////////////////////
            //if ("维修日期" == dgv.CurrentCell.OwningColumn.Name)
            //{

            //    //if ("True" == dgv.CurrentRow.Cells["已交"].Value.ToString())
            //    //{
            //    //    MessageBox.Show("合同文本已经提交,不能修改交付日期...", "注意");

            //    //    return;
            //    //}

            //    ChangeValue queryForm = new ChangeValue();

            //    queryForm.OldValue = dgv.CurrentCell.Value.ToString();
            //    queryForm.NewValue = "";
            //    queryForm.ChangeMode = "datetime";
            //    queryForm.ShowDialog();


            //    if (queryForm.NewValue != "")
            //    {
            //        dgv.CurrentRow.Cells["维修日期"].Value = queryForm.NewValue;


            //    }
            //    else
            //    {

            //        dgv.CurrentRow.Cells["维修日期"].Value = DBNull.Value;


            //    }



            //    SaveDetail();



            //    return;

            //}
        }

        private void SaveMOQ()
        {
            try
            {
                int nowparent_id = int.Parse(ly_materiel_supplier_viewDataGridView.CurrentRow.Cells["moqId"].Value.ToString());

                this.ly_materiel_supplier_MOQBindingSource.EndEdit();

                this.ly_materiel_supplier_MOQTableAdapter.Update(this.lYMaterielRequirements.ly_materiel_supplier_MOQ);

                string suppliercode = this.ly_supplier_listDataGridView.CurrentRow.Cells["编码"].Value.ToString();

                this.ly_materiel_supplier_viewTableAdapter.Fill(this.lYMaterielRequirements.ly_materiel_supplier_view, suppliercode);

                this.ly_materiel_supplier_viewBindingSource.Position = this.ly_materiel_supplier_viewBindingSource.Find("id", nowparent_id);
            }

            catch (SqlException sqle)
            {

                MessageBox.Show(sqle.Message.Split('\r')[0], "注意");

            }
        }

        private void toolStripButton14_Click(object sender, EventArgs e)
        {
            if (null == ly_materiel_supplier_MOQDataGridView.CurrentRow)
            {


                return;
            }

            if ("True" == ly_materiel_supplier_MOQDataGridView.CurrentRow.Cells["审批"].Value.ToString())
            {
                MessageBox.Show("已经审批,不能删除数据...", "注意");
                return;

            }
           

            string message = "确定删除当前条目吗？";
            string caption = "提示...";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result;



            result = MessageBox.Show(message, caption, buttons,
            MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

            if (result == DialogResult.Yes)
            {

                this.ly_materiel_supplier_MOQBindingSource.RemoveCurrent();


                SaveMOQ();
            }
        }

       //////////////////////////////

        private void  SetMaterielLeadtime()
        {
            DataGridView dgv = null;


            dgv = this.ly_materiel_supplier_viewDataGridView;

            //string nowsupplier_code = "";

            //string itemno;
            //int id2;

            //if (null == dgv.CurrentRow) return nowsupplier_code;

            string message = "确定统一指定选中物料提前期吗？";
            string caption = "提示...";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result;



            result = MessageBox.Show(message, caption, buttons,
            MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

            if (result == DialogResult.Yes)
            {

                ChangeValue queryForm = new ChangeValue();

                queryForm.OldValue = dgv.CurrentCell.Value.ToString();
                queryForm.NewValue = "";
                queryForm.ChangeMode = "value";
                queryForm.ShowDialog();




                //if (queryForm.NewValue != "")
                //{
                //    dgv.CurrentRow.Cells["供货提前期"].Value = queryForm.NewValue;
                //    //dgv.CurrentRow.Cells["discount_money"].Value = DBNull.Value;

                //    //dgv.CurrentRow.Cells["approve_flag"].Value = "False";
                //SaveChanged();

                //}
                //else
                //{


                //}

                ///////////////////////////////////////////////////


                if (queryForm.NewValue == "")
                {
                    return;
                }

                foreach (DataGridViewRow dgr in dgv.Rows)
                {
                    if (true == dgr.Selected)
                    {


                        dgr.Cells["供货提前期"].Value = queryForm.NewValue; 
                        
                        //itemno = dgr.Cells["物料编码1"].Value.ToString();

                        //id2 = int.Parse(dgr.Cells["id2"].Value.ToString());

                        //NewFrm.Show(this);

                        //SqlConnection sqlConnection1 = new SqlConnection(SQLDatabase.Connectstring);
                        //SqlCommand cmd = new SqlCommand();



                        //cmd.Parameters.Add("@supplier_code", SqlDbType.VarChar);
                        //cmd.Parameters["@supplier_code"].Value = nowsupplier_code;

                        //cmd.Parameters.Add("@itemno", SqlDbType.VarChar);
                        //cmd.Parameters["@itemno"].Value = itemno;

                        //cmd.Parameters.Add("@id2", SqlDbType.Int);
                        //cmd.Parameters["@id2"].Value = id2;




                        //cmd.CommandText = "LY_set_Materiel_Supplier";
                        //cmd.CommandType = CommandType.StoredProcedure;
                        //cmd.Connection = sqlConnection1;
                        //cmd.CommandTimeout = 0;

                        //sqlConnection1.Open();
                        //cmd.ExecuteNonQuery();
                        //sqlConnection1.Close();


                        ////ly_material_plan_explodeTableAdapter.Fill(this.lYPlanMange.ly_material_plan_explode, parentId);
                        ////this.ly_store_planitemcountTableAdapter.Fill(this.lYPlanMange.ly_store_planitemcount, parentId);
                        ////this.ly_plan_getmaterial_departmentTableAdapter.Fill(this.lYPlanMange.ly_plan_getmaterial_department , planNum);

                        //NewFrm.Hide(this);


                    }
                }

                SaveChanged();
            }

            //return nowsupplier_code;
        }

        private void 统一制定提前期ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //if (!SQLDatabase.CheckHaveRight(SQLDatabase.NowUserID, "物料供应商设置"))
            //{
            //    MessageBox.Show("没有物料供应商设置权限...", "注意");
            //    return;
            //}
            SetMaterielLeadtime();
        }

        private void SetMaterielMOQ()
        {
            DataGridView dgv = null;


            dgv = this.ly_materiel_supplier_viewDataGridView;

            //string nowsupplier_code = "";

            //string itemno;
            //int id2;

            //if (null == dgv.CurrentRow) return nowsupplier_code;

            string message = "确定统一指定选中物料MOQ吗？";
            string caption = "提示...";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result;



            result = MessageBox.Show(message, caption, buttons,
            MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

            if (result == DialogResult.Yes)
            {

                ChangeValue_Muti queryForm = new ChangeValue_Muti();

                queryForm.OldValue = dgv.CurrentCell.Value.ToString();
                queryForm.NewValue = "";
                queryForm.ChangeMode = "value";
                queryForm.ShowDialog();




                ///////////////////////////////////////////////////


                if (queryForm.NewValue == "")
                {
                    return;
                }

                foreach (DataGridViewRow dgr in dgv.Rows)
                {
                    if (true == dgr.Selected)
                    {


                        //dgr.Cells["供货提前期"].Value = queryForm.NewValue;

                        int nowparent_id = int.Parse(dgr.Cells["moqId"].Value.ToString());



                        this.ly_materiel_supplier_MOQBindingSource.AddNew();


                        this.ly_materiel_supplier_MOQDataGridView.CurrentRow.Cells["parent_id"].Value = nowparent_id;


                        this.ly_materiel_supplier_MOQDataGridView.CurrentRow.Cells["起订量"].Value = queryForm.NewValue;

                        if (queryForm.NewValue2 != "")
                        {
                            this.ly_materiel_supplier_MOQDataGridView.CurrentRow.Cells["单价MOQ"].Value = queryForm.NewValue2;
                        }
                        if (queryForm.NewValue3 != "")
                        {
                            this.ly_materiel_supplier_MOQDataGridView.CurrentRow.Cells["提前期MOQ"].Value = queryForm.NewValue3;
                        }




                        this.ly_materiel_supplier_MOQBindingSource.EndEdit();

                        this.ly_materiel_supplier_MOQTableAdapter.Update(this.lYMaterielRequirements.ly_materiel_supplier_MOQ);

                        //string suppliercode = this.ly_supplier_listDataGridView.CurrentRow.Cells["编码"].Value.ToString();

                        //this.ly_materiel_supplier_viewTableAdapter.Fill(this.lYMaterielRequirements.ly_materiel_supplier_view, suppliercode);

                        //this.ly_materiel_supplier_viewBindingSource.Position = this.ly_materiel_supplier_viewBindingSource.Find("id", nowparent_id);

                        //NewFrm.Hide(this);


                    }
                }

                //SaveChanged();
            }

            //return nowsupplier_code;
        }

        private void 统一设置最小起订量MOQToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetMaterielMOQ();
        }

        private void toolStripButton15_Click(object sender, EventArgs e)
        {
            this.ly_contract_terms_forsupplierBindingSource.AddNew();

            string suppliercode = this.ly_supplier_listDataGridView.CurrentRow.Cells["编码"].Value.ToString();

            ly_contract_terms_forsupplierDataGridView.CurrentRow.Cells["supplier_code"].Value = suppliercode;
            

        }

        private void toolStripButton16_Click(object sender, EventArgs e)
        {
            if (null == ly_contract_terms_forsupplierDataGridView.CurrentRow) return;

           

            string message = "确定删除当前合同条款吗？";
            string caption = "提示...";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result;



            result = MessageBox.Show(message, caption, buttons,
            MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

            if (result == DialogResult.Yes)
            {

                this.ly_contract_terms_forsupplierBindingSource.RemoveCurrent();


                saveContractTerm();
            }
        }

        private void saveContractTerm()
        {
            ly_contract_terms_forsupplierDataGridView.EndEdit();
            ly_contract_terms_forsupplierBindingSource.EndEdit();

            this.ly_contract_terms_forsupplierTableAdapter.Update(this.lYMaterielRequirements.ly_contract_terms_forsupplier);
        }

        private void ly_sales_contract_termsBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            saveContractTerm();
        }

        private void ly_supplier_listDataGridView_RowLeave(object sender, DataGridViewCellEventArgs e)
        {
            //saveContractTerm();
        }

        private void ly_supplier_listDataGridView_SelectionChanged(object sender, EventArgs e)
        {
            int ee = 0;
        }

        private void Get_ContractSet(string nowsuppliercode)
        {
            SqlConnection sqlConnection1 = new SqlConnection(SQLDatabase.Connectstring);
            SqlCommand cmd = new SqlCommand();

            //string nowcontractcode = this.ly_purchase_contract_mainDataGridView.CurrentRow.Cells["合同编号"].Value.ToString();

            cmd.Parameters.Add("@suppliercode", SqlDbType.VarChar);
            cmd.Parameters["@suppliercode"].Value = nowsuppliercode;

            cmd.Parameters.Add("@contract_type", SqlDbType.VarChar);
            cmd.Parameters["@contract_type"].Value = this.Sortmode;




            cmd.CommandText = "LY_contract_termscopyForsupplier";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = sqlConnection1;

            sqlConnection1.Open();
            cmd.ExecuteNonQuery();
            sqlConnection1.Close();



        }

        private void 导入公司条款ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string message = "导入公司标准合同条款吗吗？";
            string caption = "提示...";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result;



            result = MessageBox.Show(message, caption, buttons,
            MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

            if (result == DialogResult.Yes)
            {

                //string nowcontractcode = this.ly_purchase_contract_mainDataGridView.CurrentRow.Cells["合同编号"].Value.ToString();

              
                string suppliercode = this.ly_supplier_listDataGridView.CurrentRow.Cells["编码"].Value.ToString();

               

                Get_ContractSet(suppliercode);


                this.ly_contract_terms_forsupplierTableAdapter.Fill(this.lYMaterielRequirements.ly_contract_terms_forsupplier, suppliercode);


                MessageBox.Show("导入合同标准条款成功!", "注意");
            }
        }

      

     

        //private void fillToolStripButton_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        this.ly_contract_terms_forsupplierTableAdapter.Fill(this.lYMaterielRequirements.ly_contract_terms_forsupplier, supplier_codeToolStripTextBox.Text);
           
        //    }
        //    catch (System.Exception ex)
        //    {
        //        System.Windows.Forms.MessageBox.Show(ex.Message);
        //    }

        //}

      //////////////////////////////
      

      
    }
}
