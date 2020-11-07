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
    public partial class LY_Materiel_Supplier_Set : Form
    {
        private string sortcode;

        private string sortmode;

        
        private int selectionIdx = 0;

        List<string> itemlist = new List<string>();

        public string Sortmode
        {
            get { return sortmode; }
            set { sortmode = value; }
        }


        public LY_Materiel_Supplier_Set()
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

                this.groupBox2.Text = "可选采购物料供应商";
                this.groupBox3.Text = "采购物料列表";
            }
            else if ("WX" == this.Sortmode)
            {
                this.Text = "外协物料加工商设置";
                this.sortcode = "2";

                this.groupBox2.Text = "可选外协物料加工商";
                this.groupBox3.Text = "外协物料列表";
            }
            else if ("WT" == this.Sortmode)
            {
                this.Text = "机加工艺委托商设置";
                this.sortcode = "4";

                this.groupBox2.Text = "可选机加工艺委托商";
                this.groupBox3.Text = "机加物料列表";
            }

            this.ly_inma0010_sortTableAdapter.Fill(this.lYMaterielRequirements.ly_inma0010_sort, this.sortcode, this.sortcode);
        }

        private void LY_Materiel_Supplier_Set_Load(object sender, EventArgs e)
        {


            this.ly_materiel_supplier_MOQTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            this.ly_materiel_supplierTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            this.ly_inma0010_sortTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            this.ly_supplier_list_SelTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;

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

            this.groupBox1.Text = itemno + ":供应商列表";

                
            this.ly_materiel_supplierTableAdapter.Fill(this.lYMaterielRequirements.ly_materiel_supplier, itemno,id2);
            this.ly_supplier_list_SelTableAdapter.Fill(this.lYMaterielRequirements.ly_supplier_list_Sel, this.sortcode, itemno,id2 );

           
        }

        private void toolStripTextBox2_Enter(object sender, EventArgs e)
        {
            toolStripTextBox2.Text = "";

            this.ly_supplier_list_SelBindingSource.Filter = "";
        }

        private void toolStripTextBox2_KeyUp(object sender, KeyEventArgs e)
        {
            string filterString = GetDataGridviewMultiFilterString.DGVMultiFilterString(this.ly_supplier_list_SelDataGridView, this.toolStripTextBox2.Text);



            if (null == filterString)
                filterString = "";

            this.ly_supplier_list_SelBindingSource.Filter = filterString;
        }

        private void ly_supplier_list_SelDataGridView_DoubleClick(object sender, EventArgs e)
        {
            if (null == this.ly_supplier_list_SelDataGridView.CurrentRow) return;
            


            //string supplierNum = this.ly_supplier_list_SelDataGridView.CurrentRow.Cells["编码"].Value.ToString();
            //string itemno = ly_inma0010_sortDataGridView.CurrentRow .Cells["物资编号"].Value.ToString();
            //int  id2 =int .Parse ( ly_inma0010_sortDataGridView.CurrentRow.Cells["id2"].Value.ToString());

            ///////////////////////////////

            string supplierNum = this.ly_supplier_list_SelDataGridView.CurrentRow.Cells["编码"].Value.ToString();
            string itemno="";
            int id2=0;

            foreach (DataGridViewRow dgr in ly_inma0010_sortDataGridView.Rows)
            {
                if (true == dgr.Selected)
                {

                    itemno = dgr.Cells["物资编号"].Value.ToString();

                    id2 = int.Parse(dgr.Cells["id2"].Value.ToString());

                    this.ly_materiel_supplierTableAdapter.Fill(this.lYMaterielRequirements.ly_materiel_supplier, itemno, id2);

                    if (this.ly_materiel_supplierBindingSource.Find("供应商编码", supplierNum) < 0)
                    {

                        this.ly_materiel_supplierBindingSource.AddNew();

                        this.ly_materiel_supplierDataGridView.CurrentRow.Cells["供应商编码"].Value = supplierNum;
                        this.ly_materiel_supplierDataGridView.CurrentRow.Cells["物料编码"].Value = itemno;
                        this.ly_materiel_supplierDataGridView.CurrentRow.Cells["id22"].Value = id2;
                        this.ly_materiel_supplierDataGridView.CurrentRow.Cells["顺序"].Value = this.ly_materiel_supplierDataGridView.Rows.Count;

                        this.ly_materiel_supplierDataGridView.EndEdit();
                        this.ly_materiel_supplierBindingSource.EndEdit();

                        this.ly_materiel_supplierTableAdapter.Update(this.lYMaterielRequirements.ly_materiel_supplier);
                    }



                   
                


                }

                

            }
            if ("" != itemno)
            {
                this.ly_materiel_supplierTableAdapter.Fill(this.lYMaterielRequirements.ly_materiel_supplier, itemno, id2);
                this.ly_supplier_list_SelTableAdapter.Fill(this.lYMaterielRequirements.ly_supplier_list_Sel, this.sortcode, itemno, id2);
            }
            /////////////////////////////////



            //this.ly_materiel_supplierBindingSource.AddNew();

            //this .ly_materiel_supplierDataGridView.CurrentRow .Cells ["供应商编码"].Value =supplierNum;
            //this.ly_materiel_supplierDataGridView.CurrentRow.Cells["物料编码"].Value = itemno;
            //this.ly_materiel_supplierDataGridView.CurrentRow.Cells["id22"].Value = id2;
            //this.ly_materiel_supplierDataGridView.CurrentRow.Cells["顺序"].Value = this.ly_materiel_supplierDataGridView.Rows .Count;

            //this .ly_materiel_supplierDataGridView.EndEdit ();
            //this.ly_materiel_supplierBindingSource.EndEdit ();

            //this .ly_materiel_supplierTableAdapter.Update ( this .lYMaterielRequirements.ly_materiel_supplier);



            //this.ly_materiel_supplierTableAdapter.Fill(this.lYMaterielRequirements.ly_materiel_supplier, itemno, id2);
            //this.ly_supplier_list_SelTableAdapter.Fill(this.lYMaterielRequirements.ly_supplier_list_Sel, this.sortcode, itemno, id2);

           
          
           
        }

        private void bindingNavigatorDeleteItem1_Click(object sender, EventArgs e)
        {
            if (null == this.ly_materiel_supplierDataGridView.CurrentRow) return;

            string nowsupplier = this.ly_materiel_supplierDataGridView.CurrentRow.Cells["供应商编码"].Value.ToString();
            string nowsuppliername = this.ly_materiel_supplierDataGridView.CurrentRow.Cells["供应商名称"].Value.ToString();


            string message1 = "当前(供应商 " + nowsupplier + ":"+nowsuppliername + ")将被删除，继续吗？";
            string caption1 = "提示...";
            MessageBoxButtons buttons1 = MessageBoxButtons.YesNo;

            DialogResult result1;



            result1 = MessageBox.Show(message1, caption1, buttons1,
            MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

            if (result1 == DialogResult.Yes)
            {
                this.ly_materiel_supplierBindingSource.RemoveCurrent();

                foreach (DataGridViewRow dgr in ly_materiel_supplierDataGridView.Rows)
                {
                    dgr.Cells["顺序"].Value = dgr.Index + 1;

                }

                SaveChanged();

                string itemno = ly_inma0010_sortDataGridView.CurrentRow.Cells["物资编号"].Value.ToString();
                int  id2 =int .Parse ( ly_inma0010_sortDataGridView.CurrentRow.Cells["id2"].Value.ToString());
                this.ly_materiel_supplierTableAdapter.Fill(this.lYMaterielRequirements.ly_materiel_supplier, itemno,id2 );
                this.ly_supplier_list_SelTableAdapter.Fill(this.lYMaterielRequirements.ly_supplier_list_Sel, this.sortcode, itemno,id2 );



            }
        }

        private void SaveChanged()
        {
            this.ly_materiel_supplierDataGridView.EndEdit();
            this.ly_materiel_supplierBindingSource.EndEdit();

            this.ly_materiel_supplierTableAdapter.Update(this.lYMaterielRequirements.ly_materiel_supplier);
        }

        private void ly_materiel_supplierDataGridView_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
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


            /////////////////////////////////////////////////////

            if ("购买比例" == dgv.CurrentCell.OwningColumn.Name)
            {

                ChangeValue queryForm = new ChangeValue();

                queryForm.OldValue = dgv.CurrentCell.Value.ToString();
                queryForm.NewValue = "";
                queryForm.ChangeMode = "value";
                queryForm.ShowDialog();




                if (queryForm.NewValue != "")
                {
                   

                    decimal sumratio =decimal .Parse ( queryForm.NewValue);
                    decimal tempvalue=0;

                    if (0 >= sumratio || 100 < sumratio)
                    {

                        MessageBox.Show("购买比例应 大于0 小于等于100 ...", "提示");
                        return;
                    }

                    foreach (DataGridViewRow dgr in ly_materiel_supplierDataGridView.Rows)
                    {

                        if (dgr.Index != dgv.CurrentRow.Index)
                        {
                            if (string.IsNullOrEmpty(dgr.Cells["购买比例"].Value.ToString()))
                            {
                                tempvalue = 0;
                            }
                            else
                            {
                                tempvalue = decimal.Parse(dgr.Cells["购买比例"].Value.ToString());
                            }
                            sumratio = sumratio + tempvalue;
                        }

                    }

                    if (100 < sumratio)
                    {

                        MessageBox.Show("所有供应商比例和 不能大于 100 ...", "提示");
                        return;
                    }

                    dgv.CurrentRow.Cells["购买比例"].Value = queryForm.NewValue;
                    SaveChanged();


                   

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
                    //hT_Manage_ItemDataGridView.CurrentRow.Cells["apply_money"].Value = queryForm.NewValue;
                    //dgv.CurrentRow.Cells["discount_money"].Value = DBNull.Value;
                    //dgv.CurrentRow.Cells["apply_money"].Value = DBNull.Value;
                    //dgv.CurrentRow.Cells["approve_flag"].Value = "False";
                    //SaveChanged();

                }
                return;

            }


            /////////////////////////////////////////////////////

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

        private void ly_materiel_supplierDataGridView_SelectionChanged(object sender, EventArgs e)
        {

            if (null == ly_materiel_supplierDataGridView.CurrentRow)
            {
                //this.ly_sales_receive_itemDetail_repairTableAdapter.Fill(this.lYSalseMange2.ly_sales_receive_itemDetail_repair, -1);
                //this.ly_sales_receive_itemDetail_repair_wasteTableAdapter.Fill(this.lYSalseMange2.ly_sales_receive_itemDetail_repair_waste, -11);
                //this.ly_sales_receive_itemDetail_repair_returnTableAdapter.Fill(this.lYSalseMange2.ly_sales_receive_itemDetail_repair_return, -11);

                this.ly_materiel_supplier_MOQTableAdapter.Fill(this.lYMaterielRequirements.ly_materiel_supplier_MOQ, -1);

                return;
            }


            int nowId = int.Parse(ly_materiel_supplierDataGridView.CurrentRow.Cells["moqId"].Value.ToString());

            this.ly_materiel_supplier_MOQTableAdapter.Fill(this.lYMaterielRequirements.ly_materiel_supplier_MOQ, nowId);

            //this.ly_sales_receive_itemDetail_repair_returnTableAdapter.Fill(this.lYSalseMange2.ly_sales_receive_itemDetail_repair_return, nowId);
            //this.ly_sales_receive_itemDetail_repairTableAdapter.Fill(this.lYSalseMange2.ly_sales_receive_itemDetail_repair, nowId);
            //this.ly_sales_receive_itemDetail_repair_wasteTableAdapter.Fill(this.lYSalseMange2.ly_sales_receive_itemDetail_repair_waste, nowId);
          
            
            
            
            
            ///////////////////////////////////////////////////////////////////
            //DataGridView dgv = sender as DataGridView;

            //if ((dgv.Rows.Count > 0) && (dgv.SelectedRows.Count > 0))
            //{

            //    if (dgv.Rows.Count <= selectionIdx)
            //        selectionIdx = dgv.Rows.Count - 1;
            //    dgv.Rows[selectionIdx].Selected = true;
            //    dgv.CurrentCell = dgv.Rows[selectionIdx].Cells["单价"];
            //} 

        }

        private void ly_materiel_supplierDataGridView_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex >= 0)
                selectionIdx = e.RowIndex;
        }

        private void ly_materiel_supplierDataGridView_CellMouseMove(object sender, DataGridViewCellMouseEventArgs e)
        {
            DataGridView dgv = sender as DataGridView;

            if ((e.Clicks < 2) && (e.Button == MouseButtons.Left))
            {
                if ((e.ColumnIndex == -1) && (e.RowIndex > -1))
                    dgv.DoDragDrop(dgv.Rows[e.RowIndex], DragDropEffects.Move);
            } 
        }

        private int index2 = 0;
        private void ly_materiel_supplierDataGridView_DragDrop(object sender, DragEventArgs e)
        {
            DataGridView dgv = sender as DataGridView;

           

            int idx = GetRowFromPoint(dgv,e.X, e.Y);
            if (idx < 0) return;
            index2 = idx;
            if (e.Data.GetDataPresent(typeof(DataGridViewRow)))
            {
               
                DataGridViewRow row = (DataGridViewRow)e.Data.GetData(typeof(DataGridViewRow));

                int tempOrder = row.Index ;
               // this.gqis.Ins_Incontrol(idx, row.Cells[0].Value.ToString());

               

                //dgv.Rows[idx].Cells["顺序"].Value = tempOrder;
                //dgv.Rows[idx].Cells["顺序"].Value = tempOrder;

                if (idx > row.Index)
                {
                    foreach (DataGridViewRow dgvr in dgv.Rows)
                    {

                        if ( dgvr.Index>row .Index  && dgvr .Index <=idx)
                        {
                            dgvr.Cells["顺序"].Value = dgvr.Index ;

                        }
                    }
                }
                if (idx < row.Index)
                {
                    foreach (DataGridViewRow dgvr in dgv.Rows)
                    {

                        if (dgvr.Index >=idx  && dgvr.Index < row .Index )
                        {
                            dgvr.Cells["顺序"].Value = dgvr.Index+2;

                        }
                    }
                }
             

                row.Cells["顺序"].Value = idx + 1;
               // dgv.Rows[idx].Cells["顺序"].Value = row.Index + 1;
               

                SaveChanged();
                string itemno = ly_inma0010_sortDataGridView.CurrentRow.Cells["物资编号"].Value.ToString();
                int  id2 =int .Parse ( ly_inma0010_sortDataGridView.CurrentRow.Cells["id2"].Value.ToString());       

                this.ly_materiel_supplierTableAdapter.Fill(this.lYMaterielRequirements.ly_materiel_supplier, itemno,id2 );

                dgv.Rows[idx].Selected = true;
                dgv.CurrentCell = dgv.Rows[idx].Cells["顺序"];

              
                //selectionIdx = idx;
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

        private void ly_materiel_supplierDataGridView_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move; 
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            LY_SupplierMange queryForm = new LY_SupplierMange();
            queryForm.WindowState = FormWindowState.Maximized;

            queryForm.Sortmode = this.Sortmode; // "CG";
          
            queryForm.ShowDialog();

            if (null == ly_inma0010_sortDataGridView.CurrentRow) return;


            string itemno = ly_inma0010_sortDataGridView.CurrentRow .Cells["物资编号"].Value.ToString();
            int   id2 =int .Parse ( ly_inma0010_sortDataGridView.CurrentRow .Cells["id2"].Value.ToString());
           


            this.ly_materiel_supplierTableAdapter.Fill(this.lYMaterielRequirements.ly_materiel_supplier, itemno,id2 );
            this.ly_supplier_list_SelTableAdapter.Fill(this.lYMaterielRequirements.ly_supplier_list_Sel, this.sortcode, itemno,id2 );

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

            //采购员设置
            if (SQLDatabase.CheckHaveRight(SQLDatabase.NowUserID, "物料采购员设置"))
            {
                if ("采购员" == dgv.CurrentCell.OwningColumn.Name )
                {

                    //string sel = "SELECT distinct yhbm as 代码,yhmc as 名称 FROM T_users order by yhbm";

                    string sel = "select yhbm as 工号, yhmc as 姓名 from  T_users where bumen like '0004%'  and yhbm not  in ('916','931','009','903','905')";

                    QueryForm queryForm = new QueryForm();


                    queryForm.Sel = sel;
                    queryForm.Constr = SQLDatabase.Connectstring;

                    //Set the Column Collection to the filter Table
                    //queryForm.SetSourceColumns(this.billMainDataSet.BalanceBill.Columns);

                    queryForm.ShowDialog();
                    dgv.CurrentRow.Cells["buyer_code"].Value = queryForm.Result;
                    dgv.CurrentRow.Cells["采购员"].Value = queryForm.Result1;

                    this.ly_inma0010_sortBindingSource.EndEdit();

                    this.ly_inma0010_sortTableAdapter.Update(this.lYMaterielRequirements.ly_inma0010_sort);

                    //UpdateRequirement(itemno, nowid, "buyercode", queryForm.Result);
                }
            }

            //////////////////////////////////////

            //if ("种类存疑" == dgv.CurrentCell.OwningColumn.Name)
            //{

            //    if ("True" == dgv.CurrentRow.Cells["种类存疑"].Value.ToString())
            //    {
            //        dgv.CurrentRow.Cells["种类存疑"].Value = "False";

            //    }
            //    else
            //    {

            //        dgv.CurrentRow.Cells["种类存疑"].Value = "True";
            //    }



            //    this.ly_inma0010_sortBindingSource.EndEdit();

            //    this.ly_inma0010_sortTableAdapter.Update(this.lYMaterielRequirements.ly_inma0010_sort);



            //    return;

            //}

            /////////////////////////////////////
        }

        private void ly_inma0010_sortDataGridView_CellLeave(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void toolStripButton12_Click(object sender, EventArgs e)
        {
           

            int nowparent_id = int.Parse(ly_materiel_supplierDataGridView.CurrentRow.Cells["moqId"].Value.ToString());


            this.ly_materiel_supplier_MOQBindingSource.AddNew();


            this.ly_materiel_supplier_MOQDataGridView.CurrentRow.Cells["parent_id"].Value = nowparent_id;





            this.ly_materiel_supplier_MOQBindingSource.EndEdit();

            this.ly_materiel_supplier_MOQTableAdapter.Update(this.lYMaterielRequirements.ly_materiel_supplier_MOQ);

            //string suppliercode = this.ly_supplier_listDataGridView.CurrentRow.Cells["编码"].Value.ToString();

            //this.ly_materiel_supplier_viewTableAdapter.Fill(this.lYMaterielRequirements.ly_materiel_supplier_view, suppliercode);

            //this.ly_materiel_supplier_viewBindingSource.Position = this.ly_materiel_supplier_viewBindingSource.Find("id", nowparent_id);

           ///////////////////////

            string itemno = ly_inma0010_sortDataGridView.CurrentRow.Cells["物资编号"].Value.ToString();//ly_inma0010_sortDataGridView
            int id2 = int.Parse(ly_inma0010_sortDataGridView.CurrentRow.Cells["id2"].Value.ToString());

            this.groupBox1.Text = itemno + ":供应商列表";


            this.ly_materiel_supplierTableAdapter.Fill(this.lYMaterielRequirements.ly_materiel_supplier, itemno, id2);
            this.ly_supplier_list_SelTableAdapter.Fill(this.lYMaterielRequirements.ly_supplier_list_Sel, this.sortcode, itemno, id2);

            this.ly_materiel_supplierBindingSource.Position = this.ly_materiel_supplierBindingSource.Find("id", nowparent_id);

        
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

        private void SaveMOQ()
        {
            try
            {

                int nowparent_id = int.Parse(ly_materiel_supplierDataGridView.CurrentRow.Cells["moqId"].Value.ToString());

                this.ly_materiel_supplier_MOQBindingSource.EndEdit();

                this.ly_materiel_supplier_MOQTableAdapter.Update(this.lYMaterielRequirements.ly_materiel_supplier_MOQ);


                string itemno = ly_inma0010_sortDataGridView.CurrentRow.Cells["物资编号"].Value.ToString();//ly_inma0010_sortDataGridView
                int id2 = int.Parse(ly_inma0010_sortDataGridView.CurrentRow.Cells["id2"].Value.ToString());

                this.groupBox1.Text = itemno + ":供应商列表";


                this.ly_materiel_supplierTableAdapter.Fill(this.lYMaterielRequirements.ly_materiel_supplier, itemno, id2);
                this.ly_supplier_list_SelTableAdapter.Fill(this.lYMaterielRequirements.ly_supplier_list_Sel, this.sortcode, itemno, id2);

                this.ly_materiel_supplierBindingSource.Position = this.ly_materiel_supplierBindingSource.Find("id", nowparent_id);

            }

            catch (SqlException sqle)
            {

                MessageBox.Show(sqle.Message.Split('\r')[0], "注意");

            }
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

        private void SetMaterielLeadtime()
        {
            DataGridView dgv = null;


            dgv = this.ly_materiel_supplierDataGridView;

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

        private void 统一指定提前期ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetMaterielLeadtime();
        }

        private void 统一制定采购员ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!SQLDatabase.CheckHaveRight(SQLDatabase.NowUserID, "物料采购员设置"))
            {
                return;
            }

            DataGridView dgv = ly_inma0010_sortDataGridView;





            string nowitemno;

            //string sel = "SELECT distinct yhbm as 代码,yhmc as 名称 FROM T_users order by yhbm";

            string sel = @"select yhbm as 工号, yhmc as 姓名 from T_users_Purchase_View ";
            QueryForm queryForm = new QueryForm();


            queryForm.Sel = sel;
            queryForm.Constr = SQLDatabase.Connectstring;

            //Set the Column Collection to the filter Table
            //queryForm.SetSourceColumns(this.billMainDataSet.BalanceBill.Columns);

            queryForm.ShowDialog();

            //for (int i = 0; i < this.dataGridView1.Columns.Count; i++)

            //    this.dataGridView1.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;

            dgv.Columns["采购员"].SortMode = DataGridViewColumnSortMode.NotSortable;

            NewFrm.Show(this);
            foreach (DataGridViewRow dgr in dgv.Rows)
            {
                if (true == dgr.Selected)
                {

                    nowitemno= dgr.Cells["物资编号"].Value.ToString();

                    this.itemlist.Add(nowitemno);

                    //dgr.Cells["buyer_code"].Value = queryForm.Result;
                    //dgr.Cells["采购员"].Value = queryForm.Result1;

                    NewFrm.Notify(this, "正在更新:  (" + nowitemno + ")" + "   采购员");


                    UpdateSupplier(nowitemno, queryForm.Result, queryForm.Result1);

                    //this.ly_inma0010_sortBindingSource.EndEdit();

                    //this.ly_inma0010_sortTableAdapter.Update(this.lYMaterielRequirements.ly_inma0010_sort);


                    //UpdateRequirement(itemno, nowid, "buyercode", queryForm.Result);

                    //this.ly_inma0010_sortBindingSource.EndEdit();

                    //this.ly_inma0010_sortTableAdapter.Update(this.lYMaterielRequirements.ly_inma0010_sort);

                }
            }

            dgv.Columns["采购员"].SortMode = DataGridViewColumnSortMode.Automatic;
            // this.lY_MaterielRequirementsTableAdapter.Fill(this.lYPlanMange.LY_MaterielRequirements, parentId, "外协", "OWE");
            this.ly_inma0010_sortTableAdapter.Fill(this.lYMaterielRequirements.ly_inma0010_sort, this.sortcode, this.sortcode);
            NewFrm.Hide(this);

            int nowindex = 0;
            foreach (string  nowitem in itemlist)
            {
                nowindex = this.ly_inma0010_sortBindingSource.Find("物资编号", nowitem);
                dgv.Rows[nowindex].Selected = true;
            }



            // dgv.Columns["采购员"].SortMode = DataGridViewColumnSortMode.Automatic;
        }

        private void UpdateSupplier(string nowitemno,string nowbuyercode, string nowbuyer )
        {
            if (string.IsNullOrEmpty(nowbuyer)) return;

            SqlConnection sqlConnection1 = new SqlConnection(SQLDatabase.Connectstring);
            SqlCommand cmd = new SqlCommand();


            cmd.Parameters.Add("@nowitemno", SqlDbType.VarChar);
            cmd.Parameters["@nowitemno"].Value = nowitemno;

            //cmd.Parameters.Add("@nowid", SqlDbType.Int);
            //cmd.Parameters["@nowid"].Value = nowid;

            cmd.Parameters.Add("@nowbuyer_code", SqlDbType.VarChar);
            cmd.Parameters["@nowbuyer_code"].Value = nowbuyercode;

            cmd.Parameters.Add("@nowbuyer", SqlDbType.VarChar);
            cmd.Parameters["@nowbuyer"].Value = nowbuyer;


            cmd.CommandText = "LY_UpdateSupplier";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = sqlConnection1;

            sqlConnection1.Open();
            int aaa = cmd.ExecuteNonQuery();
            sqlConnection1.Close();




        }

        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            ly_inma0010_sortDataGridView.Columns["采购员"].SortMode = DataGridViewColumnSortMode.Automatic;
            this.ly_inma0010_sortTableAdapter.Fill(this.lYMaterielRequirements.ly_inma0010_sort, this.sortcode, this.sortcode);
        }

        //private void fillToolStripButton_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        this.ly_materiel_supplier_MOQTableAdapter.Fill(this.lYMaterielRequirements.ly_materiel_supplier_MOQ, new System.Nullable<int>(((int)(System.Convert.ChangeType(parent_idToolStripTextBox.Text, typeof(int))))));
        //    }
        //    catch (System.Exception ex)
        //    {
        //        System.Windows.Forms.MessageBox.Show(ex.Message);
        //    }

        //}








    }
}
