using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Transactions;
using HappyYF.Infrastructure.Repositories;
using DataGridFilter;

namespace HappyYF.YuanXin.WorkSet
{
    public partial class LY_Store_Out_Sales : Form
    {
        string formState = "View";
        string formStateBorrow = "View";
        
        public LY_Store_Out_Sales()
        {
            InitializeComponent();

        
        }

        public void Find_planlocation(string nowplannum, string ifActivate)
        {

            //if ("yes" == ifActivate)
            //{
            //this.ly_material_plan_mainperiodTableAdapter.Fill(this.lYSalseMange.ly_material_plan_mainperiod, "LSPT", this.dateTimePicker1.Value, this.dateTimePicker2.Value);
            ////}依赖书号  ly_sales_contract_main1BindingSource

            //this.ly_material_plan_mainperiodBindingSource.Position = this.ly_material_plan_mainperiodBindingSource.Find("计划编号", nowplannum);
            if ("XS" == ifActivate.Substring(0, 2))
            {
                this.tabControl1.SelectedIndex = 0;
                this.ly_sales_contract_main1TableAdapter.Fill(this.lYSalseMange.ly_sales_contract_main1, "", "full", this.dateTimePicker1.Value, this.dateTimePicker2.Value);
                this.ly_sales_contract_main1BindingSource.Position = this.ly_sales_contract_main1BindingSource.Find("依赖书号", nowplannum);
            }
            else
            {
                this.tabControl1.SelectedIndex = 1;
          
                this.ly_sales_borrow_periodTableAdapter.Fill(this.lYSalseMange.ly_sales_borrow_period, this.dateTimePicker3.Value, this.dateTimePicker4.Value);
                this.ly_sales_borrow_periodBindingSource.Position = this.ly_sales_borrow_periodBindingSource.Find("依赖书号", nowplannum);
            
            }


        }

        private void LY_Store_Out_Sales_Load(object sender, EventArgs e)
        {


            this.dateTimePicker1.Text = DateTime.Today.AddMonths(-6).Date.ToString();
            this.dateTimePicker2.Text = DateTime.Today.AddDays(1).Date.ToString();

            this.ly_sales_groupTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            this.ly_sales_outbindTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;

            this.ly_storeout_employWarehouse2TableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            this.ly_plan_deptlist2TableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;

            this.ly_storeout_employWarehouseTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            this.ly_plan_deptlistTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;

            this.ly_sales_contract_detail_sumTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
           
            this.ly_plan_getmaterial_departmentTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;

            this.ly_store_outnumTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            this.ly_store_outTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;

            this.ly_sales_contract_main1TableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            this.ly_sales_contract_main1TableAdapter.Fill(this.lYSalseMange.ly_sales_contract_main1, "", "full", this.dateTimePicker1.Value, this.dateTimePicker2.Value);

            this.radioButton1.Checked = true;

            SetFormState("View");

            ///////////////////////////////////////////////////////////////////////////////////////////////////////
            this.dateTimePicker3.Text = DateTime.Today.AddMonths(-6).Date.ToString();
            this.dateTimePicker4.Text = DateTime.Today.AddDays(1).Date.ToString();

            //this.comboBox3.SelectedIndexChanged -= comboBox3_SelectedIndexChanged;
            //this.comboBox4.SelectedIndexChanged -= comboBox4_SelectedIndexChanged;

            this.ly_store_outnum_borrowTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;

            this.ly_store_borrow_outTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            

            this.ly_sales_borrow_periodTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;

            //this.ly_sales_borrowDataGridView.SelectionChanged -= ly_sales_borrowDataGridView_SelectionChanged;
            //this.ly_sales_borrow_periodTableAdapter.Fill(this.lYSalseMange.ly_sales_borrow_period, this.dateTimePicker3.Value, this.dateTimePicker4.Value);
            //this.ly_sales_borrowDataGridView.SelectionChanged += ly_sales_borrowDataGridView_SelectionChanged;

            this.radioButton3.Checked = true;

         


            SetFormStateBorrow("View");
        }

        private void SetFormState(string state)
        {
            // view edit add save cancle

            //return;

            if ("View" == state)
            {
                this.formState = "View";

                this.dateTimePicker1.Enabled = true;
                this.dateTimePicker2.Enabled = true;

                
                this.comboBox1.Enabled = false;
                this.comboBox2.Enabled = false;


                this.bindingNavigatorMoveFirstItem.Enabled = true;
                this.bindingNavigatorMoveLastItem.Enabled = true;
                this.bindingNavigatorMoveNextItem.Enabled = true;
                this.bindingNavigatorMovePreviousItem.Enabled = true;
                this.bindingNavigatorPositionItem.Enabled = true;



                toolStripButton6.Visible = false;
                //toolStripButton2.Enabled = true;
                //bindingNavigatorDeleteItem.Enabled = true;
                //bindingNavigatorAddNewItem.Enabled = true;
                //ly_material_plan_mainBindingNavigatorSaveItem.Enabled = false;

                //yX_clientBindingNavigatorSaveItem.Enabled = true ;
                this.bindingNavigator1.Enabled  = true;

                ly_sales_contract_main1DataGridView.Enabled = true;
                ly_store_outnumDataGridView.Enabled = true;


            }
            else
            {
                this.formState = "Edit";

                this.dateTimePicker1.Enabled = false;
                this.dateTimePicker2.Enabled = false;


                this.comboBox1.Enabled = true;
                this.comboBox2.Enabled = true;




                this.bindingNavigatorMoveFirstItem.Enabled = false;
                this.bindingNavigatorMoveLastItem.Enabled = false;
                this.bindingNavigatorMoveNextItem.Enabled = false;
                this.bindingNavigatorMovePreviousItem.Enabled = false;
                this.bindingNavigatorPositionItem.Enabled = false;



                toolStripButton6.Visible = true;
                //toolStripButton2.Enabled = false;
                //bindingNavigatorDeleteItem.Enabled = false;
                //bindingNavigatorAddNewItem.Enabled = false;
                //ly_material_plan_mainBindingNavigatorSaveItem.Enabled = true;

                //yX_clientBindingNavigatorSaveItem.Enabled = true ;
                this.bindingNavigator1.Enabled = false;

                ly_sales_contract_main1DataGridView.Enabled = false;
                ly_store_outnumDataGridView.Enabled = false;

            }


        }

        private void SetFormStateBorrow(string state)
        {
            // view edit add save cancle

            //return;

            if ("View" == state)
            {
                this.formStateBorrow = "View";

                this.dateTimePicker3.Enabled = true;
                this.dateTimePicker4.Enabled = true;


                this.comboBox3.Enabled = false;
                this.comboBox4.Enabled = false;


                this.toolStripButton1.Enabled = true;
                this.toolStripButton2.Enabled = true;
                this.toolStripButton3.Enabled = true;
                this.toolStripButton4.Enabled = true;
                this.toolStripTextBox1.Enabled = true;



                toolStripButton8.Visible = false;
                //toolStripButton2.Enabled = true;
                //bindingNavigatorDeleteItem.Enabled = true;
                //bindingNavigatorAddNewItem.Enabled = true;
                //ly_material_plan_mainBindingNavigatorSaveItem.Enabled = false;

                //yX_clientBindingNavigatorSaveItem.Enabled = true ;
                this.bindingNavigator3.Enabled = true;

                ly_sales_borrowDataGridView.Enabled = true;
                ly_store_outnum_borrowDataGridView.Enabled = true;


            }
            else
            {
                this.formStateBorrow = "Edit";

                this.dateTimePicker3.Enabled = false;
                this.dateTimePicker4.Enabled = false;


                this.comboBox3.Enabled = true;
                this.comboBox4.Enabled = true;




                this.toolStripButton1.Enabled = false ;
                this.toolStripButton2.Enabled = false ;
                this.toolStripButton3.Enabled = false;
                this.toolStripButton4.Enabled = false;
                this.toolStripTextBox1.Enabled = false;



                toolStripButton8.Visible = true;
                //toolStripButton2.Enabled = false;
                //bindingNavigatorDeleteItem.Enabled = false;
                //bindingNavigatorAddNewItem.Enabled = false;
                //ly_material_plan_mainBindingNavigatorSaveItem.Enabled = true;

                //yX_clientBindingNavigatorSaveItem.Enabled = true ;
                this.bindingNavigator3.Enabled = false;

                ly_sales_borrowDataGridView.Enabled = false ;
                ly_store_outnum_borrowDataGridView.Enabled = false ;

            }


        }

        private void toolStripButton21_Click(object sender, EventArgs e)
        {
            this.ly_sales_contract_main1TableAdapter.Fill(this.lYSalseMange.ly_sales_contract_main1,"",  "full", this.dateTimePicker1.Value, this.dateTimePicker2.Value);
        }

        private void toolStripTextBox5_KeyUp(object sender, KeyEventArgs e)
        {
            string filterString;


            filterString = GetDataGridviewMultiFilterString.DGVMultiFilterString(this.ly_sales_contract_main1DataGridView, this.toolStripTextBox5.Text);


            this.ly_sales_contract_main1BindingSource.Filter = filterString;
        }

        private void toolStripTextBox5_Enter(object sender, EventArgs e)
        {
            toolStripTextBox5.Text = "";

            this.ly_sales_contract_main1BindingSource.Filter = "";
        }

      

        private void ly_sales_contract_main1DataGridView_SelectionChanged(object sender, EventArgs e)
        {
            if (null == ly_sales_contract_main1DataGridView.CurrentRow)
            {
                
                return;
            }


            if (this.formState == "View")
            {
                string nowinnerCode = ly_sales_contract_main1DataGridView.CurrentRow.Cells["内部编码1"].Value.ToString();

               
                this.ly_sales_contract_detail_sumTableAdapter.Fill(this.lYSalseMange.ly_sales_contract_detail_sum, nowinnerCode, "contract");



                string planNum = this.ly_sales_contract_main1DataGridView.CurrentRow.Cells["依赖书号"].Value.ToString();

                this.ly_sales_outbindTableAdapter.Fill(this.lYSalseMange.ly_sales_outbind, nowinnerCode);


                this.ly_plan_getmaterial_departmentTableAdapter.Fill(this.lYPlanMange.ly_plan_getmaterial_department, planNum);

                this.ly_storeout_employWarehouse2TableAdapter.Fill(this.lYSalseMange2.ly_storeout_employWarehouse2, "asd", "asd", "asd","asd");
                this.ly_plan_deptlist2TableAdapter.Fill(this.lYSalseMange2.ly_plan_deptlist2, "asd", "asd");

                this.ly_plan_deptlist2TableAdapter.Fill(this.lYSalseMange2.ly_plan_deptlist2, planNum, nowinnerCode);

                if (this.comboBox1.Items.Count > 0)
                {
                    this.comboBox1.SelectedIndex = 0;
                    this.ly_storeout_employWarehouse2TableAdapter.Fill(this.lYSalseMange2.ly_storeout_employWarehouse2, planNum, this.comboBox1.SelectedValue.ToString(), SQLDatabase.NowUserID, nowinnerCode);
                }

                        this.ly_plan_getmaterial_departmentBindingSource.Filter = "领料部门='asd'";

                        this.ly_store_outnumTableAdapter.Fill(this.lYStoreMange.ly_store_outnum, planNum, SQLDatabase.NowUserID);

                        if (null == this.ly_store_outnumDataGridView.CurrentRow)
                        {

                            this.ly_store_outTableAdapter.Fill(this.lYStoreMange.ly_store_out, "asd", SQLDatabase.nowUserName());
                        }

                /////////////////////////////////////////////////





                //        this.ly_store_planoutTableAdapter.Fill(this.lYStoreMange.ly_store_planout, planNum);










                //        if (null == this.ly_store_outnumDataGridView.CurrentRow)
                //        {
                //            this.ly_store_outTableAdapter.Fill(this.lYStoreMange.ly_store_out, "asd");

                //        }

                //        this.groupBox3.Text = planNum + ":物料列表";


                //    }
                //}

                this.ly_sales_groupTableAdapter.Fill(this.lYSalseMange.ly_sales_group, planNum);
                MakeGroupTreeView(planNum);

            }


        }

        private void MakeGroupTreeView(string nowplannum)
        {
            ////////////////////////////////////////////

            this.treeView2.Nodes.Clear();

            System.Windows.Forms.TreeNode PNode = new System.Windows.Forms.TreeNode();
            PNode.Text = nowplannum;

            string nowplanremark ="";

            PNode.ToolTipText = nowplanremark;
            PNode.Tag = "---";



            //if (SQLDatabase.CheckHaveRight(SQLDatabase.NowUserID, "营业综合信息"))
            //{
            //    TNode.Tag = "";
            //}
            //else if (SQLDatabase.CheckHaveRight(SQLDatabase.NowUserID, "营业区域信息"))
            //{
            //    TNode.Tag = "salesregion_code='" + SQLDatabase.nowSalesregioncode() + "'";
            //}
            //else
            //{
            //    TNode.Tag = "salesperson_code='" + SQLDatabase.NowUserID + "'";

            //}

            this.treeView2.ShowNodeToolTips = true;

            this.treeView2.Nodes.Add(PNode);

            /////////////////////////////////////////////

            DataGridView dgv = this.ly_sales_groupDataGridView;

            string nowgroupCode;
            string nowgroupName;
            string nowremark;
            string nowgroupid;


            foreach (DataGridViewRow dgr in dgv.Rows)
            {

                if (string.IsNullOrEmpty(dgr.Cells["配套编码"].Value.ToString())) continue;
                //nowgroupCode = noworderValue + int.Parse(dgr.Cells["配套编码"].Value.ToString(), System.Globalization.NumberStyles.Number);
                nowgroupCode = dgr.Cells["配套编码"].Value.ToString();
                nowgroupName = dgr.Cells["配套名称"].Value.ToString();
                nowremark = dgr.Cells["配套说明"].Value.ToString();
                nowgroupid = "-" + dgr.Cells["group_id"].Value.ToString();

                System.Windows.Forms.TreeNode TNode = new System.Windows.Forms.TreeNode();

                TNode.Text = nowgroupCode + ":" + nowgroupName;
                TNode.ToolTipText = nowremark;
                TNode.Tag = nowgroupid;
                PNode.Nodes.Add(TNode);


                /////////////////////////
                //if (null == ly_inma0010DataGridView.CurrentRow) return;
                //string s = this.ly_inma0010DataGridView.CurrentRow.Cells["物资编号"].Value.ToString();

                //string selAllString = "SELECT   a.id,  a.parent_id,a.parentno, a.itemno, b.fir_style+':'+ a.itemno +' ' + a.itemname+' '+b.xhc+' '+cast(cast(a.absqty  as int) as varchar(3))+' '+a.unit as itemname  from ly_plan_getmaterial a left join " +
                //                      "      ly_inma0010 AS b ON a.itemno = b.wzbh          " +
                //                      " where material_plan_num='" + nowplannum + "' and sales_group_code='" + nowgroupCode + "' order by b.fir_style,a.itemno";

                //string selAllString = "SELECT   a.id,  a.parent_id,a.parentno, a.gitemno, b.fir_style+':'+  b.xhc+' '+cast(cast(a.absqty  as int) as varchar(3))+ a.unit as itemname,a.gremark  from ly_plan_getmaterial a left join " +
                //                      "      ly_inma0010 AS b ON a.gitemno = b.wzbh          " +
                //                      " where material_plan_num='" + nowplannum + "' and sales_group_code='" + nowgroupCode + "' order by b.fir_style,a.gitemno";


                string selAllString = "SELECT   a.id,  a.parent_id,a.parentno, a.gitemno, COALESCE(b.fir_style,a.gitemname,'')+':'+  b.xhc+' '+cast(cast(isnull(a.absqty,0)  as int) as varchar(3))+ a.unit as itemname,a.gremark  from ly_plan_getmaterial a left join " +
                                     "      ly_inma0010 AS b ON a.gitemno = b.wzbh          " +
                                     " where material_plan_num='" + nowplannum + "' and sales_group_code='" + nowgroupCode + "' order by b.fir_style,a.gitemno";



                string cString = SQLDatabase.Connectstring; ;
                SqlDataAdapter bomAdapter = new SqlDataAdapter(selAllString, cString);

                bomAdapter.SelectCommand.CommandTimeout = 0;

                DataSet bomData = new DataSet();
                bomAdapter.Fill(bomData);


                MakeTreeView2(bomData.Tables[0], nowgroupid, TNode);
                //this.treeView1.ExpandAll();
                //this.treeView1.SelectedNode = this.treeView1.Nodes[0];
                //this.treeView1.SelectedNode.Expand();

                //this.groupBox1.Text = s + "BOM结构图";
                //////////////////////////


                //MakeTreeView2();

            }

            this.treeView2.ExpandAll();

        }
        private void MakeTreeView2(DataTable table, string ParentID, System.Windows.Forms.TreeNode PNode)
        {


            DataRow[] dr;

            if (null == ParentID)
                dr = table.Select("parent_id is null");
            else
            {

                string expression;
                expression = "parent_id=" + ParentID + "";

                dr = table.Select(expression);
            }

            try
            {
                if (dr.Length > 0)
                {
                    foreach (DataRow d in dr)
                    {

                        System.Windows.Forms.TreeNode TNode = new System.Windows.Forms.TreeNode();
                        TNode.Text = d["itemname"].ToString();
                        TNode.Tag = d["id"].ToString();
                        TNode.ToolTipText = d["gremark"].ToString();
                        if (PNode == null)
                        {
                            this.treeView2.Nodes.Add(TNode);
                        }
                        else
                        {
                            PNode.Nodes.Add(TNode);
                        }

                        MakeTreeView2(table, d["id"].ToString(), TNode);
                    }
                }
                else
                {
                    return;
                }
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton rdb = sender as RadioButton;
            if (rdb.Checked)
            {
                SetFormState("View");
                //this.ly_store_outTableAdapter.Fill(this.lYStoreMange.ly_store_out, "asd");
                ////this.ly_plan_getmaterialTableAdapter.Fill(this.lYPlanMange.ly_plan_getmaterial, "asd");
                this.ly_plan_getmaterial_departmentBindingSource.Filter = "领料部门='asd'";
            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton rdb = sender as RadioButton;
            if (rdb.Checked)
            {
                SetFormState("Edit");
                //this.ly_store_outTableAdapter.Fill(this.lYStoreMange.ly_store_out, "asd");
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

            string planNum = this.ly_sales_contract_main1DataGridView.CurrentRow.Cells["依赖书号"].Value.ToString();
            string nowinnerCode = ly_sales_contract_main1DataGridView.CurrentRow.Cells["内部编码1"].Value.ToString();

            //MessageBox.Show(this .comboBox1 .SelectedValue .ToString ());
            if (null != this.comboBox1.SelectedValue)
            {
                this.ly_storeout_employWarehouse2TableAdapter.Fill(this.lYSalseMange2.ly_storeout_employWarehouse2, planNum, this.comboBox1.SelectedValue.ToString(), SQLDatabase.NowUserID, nowinnerCode);
                if (0 == this.comboBox2.Items.Count)
                {
                    this.ly_plan_getmaterial_departmentBindingSource.Filter = "领料部门='" + "asd" + "'";
                }
               
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (null != this.comboBox1.SelectedValue)
            {
              
                if (null != this.comboBox2.SelectedValue)
                {
                    if ("全部" != this.comboBox2.SelectedValue.ToString())
                    {

                        this.ly_plan_getmaterial_departmentBindingSource.Filter = "领料部门='" + this.comboBox1.SelectedValue.ToString() + "' and 仓库='" + this.comboBox2.SelectedValue.ToString() + "'";
                        this.ly_sales_outbindBindingSource.Filter = "仓库='" + this.comboBox2.SelectedValue.ToString() + "'";
                    }
                    else
                    {

                        this.ly_plan_getmaterial_departmentBindingSource.Filter = "领料部门='" + this.comboBox1.SelectedValue.ToString() + "'";
                    }
                }
              
            }
            else
            {
                this.ly_plan_getmaterial_departmentBindingSource.Filter = "领料部门='" + "asd" + "'";
                this.ly_sales_outbindBindingSource.Filter = "仓库='" + "asd" + "'";
            }
        }

        private void ly_store_outnumDataGridView_SelectionChanged(object sender, EventArgs e)
        {
            if (null == ly_store_outnumDataGridView.CurrentRow) return;

            if (this.formState == "View")
            {
                if (null != this.ly_store_outnumDataGridView.CurrentRow)
                {

                    string outNum = this.ly_store_outnumDataGridView.CurrentRow.Cells["out_number"].Value.ToString();


                    this.ly_store_outTableAdapter.Fill(this.lYStoreMange.ly_store_out, outNum, SQLDatabase.nowUserName());

                    string deptcode = this.ly_store_outnumDataGridView.CurrentRow.Cells["out_deptcode"].Value.ToString();
                    string warehouse = this.ly_store_outnumDataGridView.CurrentRow.Cells["warehouse"].Value.ToString();

                    if (!string.IsNullOrEmpty(deptcode))
                    {
                        if ("全部" == warehouse)
                        {

                            this.ly_plan_getmaterial_departmentBindingSource.Filter = "领料部门='" + deptcode + "'";
                            this.comboBox1.SelectedValue = deptcode;
                            this.comboBox2.SelectedValue = warehouse;
                        }
                        else
                        {
                            this.ly_plan_getmaterial_departmentBindingSource.Filter = "领料部门='" + deptcode + "' and 仓库='" + warehouse + "'";
                            this.comboBox1.SelectedValue = deptcode;
                            this.comboBox2.SelectedValue = warehouse;
                        }



                    }

                   

                }
            }
            else
            {
                // this.yX_taocan_mainBindingSource.Position = this.nowRow;
            }
        }

        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            if (null == this.ly_sales_contract_main1DataGridView.CurrentRow) return;
            //if (null == ly_plan_getmaterialDataGridView.CurrentRow) return;

            //if ("True" == ly_sales_contract_main1DataGridView.CurrentRow.Cells["赠送"].Value.ToString())
            //{
            //    if (string.IsNullOrEmpty(ly_sales_contract_main1DataGridView.CurrentRow.Cells["项目编码"].Value.ToString()))
            //    {
            //        MessageBox.Show("缺少技术部设置,不能出库...", "注意");
            //        return;
            //    }

            //}

            if (this.ly_plan_getmaterialDataGridView.RowCount < 1 && this.ly_sales_outbindDataGridView.RowCount < 1) return;



            if ("True" != this.ly_sales_contract_main1DataGridView.CurrentRow.Cells["批准1"].Value.ToString())
            {
                MessageBox.Show("营业部未发出配套出库指令,不能出库...", "注意");
                return;
            }

            if ("True" != this.ly_sales_contract_main1DataGridView.CurrentRow.Cells["审核1"].Value.ToString())
            {
                MessageBox.Show("未经生产部审核,不能出库...", "注意");
                return;
            }


            string message = "确定领料出库吗?";
            string caption = "提示...";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result;



            result = MessageBox.Show(message, caption, buttons,
            MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

            if (result == DialogResult.Yes)
            {

                CountStoreOutAuto();
            }


        }

        private void CountStoreOutAuto()
        {
            if (null == this.ly_sales_contract_main1DataGridView.CurrentRow) return;
            //if (null == comboBox1.SelectedValue) return;
            //if (null == comboBox2.SelectedValue) return;

            if (this.ly_plan_getmaterialDataGridView.RowCount < 1 && this.ly_sales_outbindDataGridView.RowCount < 1) return;


            string planNum = this.ly_sales_contract_main1DataGridView.CurrentRow.Cells["依赖书号"].Value.ToString();
            string innerCode = this.ly_sales_contract_main1DataGridView.CurrentRow.Cells["内部编码1"].Value.ToString();


            NewFrm.Show(this);

            SqlConnection sqlConnection1 = new SqlConnection(SQLDatabase.Connectstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandTimeout = 0;


            cmd.Parameters.Add("@plan_num", SqlDbType.VarChar);
            cmd.Parameters["@plan_num"].Value = planNum;

            if (null == comboBox1.SelectedValue)
            {

                cmd.Parameters.Add("@prod_dept", SqlDbType.VarChar);
                cmd.Parameters["@prod_dept"].Value = "07";
            }
            else
            {
                cmd.Parameters.Add("@prod_dept", SqlDbType.VarChar);
                cmd.Parameters["@prod_dept"].Value = comboBox1.SelectedValue;
            }


            if (null == comboBox2.SelectedValue)
            {
                cmd.Parameters.Add("@warehousename", SqlDbType.VarChar);
                cmd.Parameters["@warehousename"].Value = "---";
            }
            else
            {
                cmd.Parameters.Add("@warehousename", SqlDbType.VarChar);
                cmd.Parameters["@warehousename"].Value = comboBox2.SelectedValue;
            }

            string outNum = GetMaxOutNum();
            cmd.Parameters.Add("@out_number", SqlDbType.VarChar);
            cmd.Parameters["@out_number"].Value = outNum;

            cmd.Parameters.Add("@faliaoren", SqlDbType.VarChar);
            cmd.Parameters["@faliaoren"].Value = SQLDatabase.nowUserName();

            cmd.Parameters.Add("@innercode", SqlDbType.VarChar);
            cmd.Parameters["@innercode"].Value = innerCode;







            //@innercode

            cmd.CommandText = "LY_store_out_contract";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = sqlConnection1;
            cmd.CommandTimeout = 0;
            sqlConnection1.Open();
            cmd.ExecuteNonQuery();
            sqlConnection1.Close();



            this.ly_store_outTableAdapter.Fill(this.lYStoreMange.ly_store_out, outNum, SQLDatabase.nowUserName());
            this.ly_plan_getmaterial_departmentTableAdapter.Fill(this.lYPlanMange.ly_plan_getmaterial_department, planNum);


            this.ly_sales_outbindTableAdapter.Fill(this.lYSalseMange.ly_sales_outbind, innerCode);


            this.ly_store_outnumTableAdapter.Fill(this.lYStoreMange.ly_store_outnum, planNum, SQLDatabase.NowUserID);

            NewFrm.Hide(this);
        }
        private string GetMaxOutNum()
        {
            SqlConnection sqlConnection1 = new SqlConnection(SQLDatabase.Connectstring);
            SqlCommand cmd = new SqlCommand();

            string MaxPlanCode = "";

            //cmd.Parameters.Add("@Plan_mode", SqlDbType.VarChar);
            //cmd.Parameters["@Plan_mode"].Value = "LLJH";


            cmd.CommandText = "LY_Get_OutNumber";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = sqlConnection1;

            sqlConnection1.Open();
            MaxPlanCode = cmd.ExecuteScalar().ToString();
            sqlConnection1.Close();



            return MaxPlanCode;
        }

        private void 计划物料计算ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (null == this.ly_store_outnumDataGridView.CurrentRow) return;

            if (this.formState != "View") return;

            if (SQLDatabase.nowUserName() != ly_store_outnumDataGridView.CurrentRow.Cells["发料人"].Value.ToString())
            {

                MessageBox.Show("请发料人:" + ly_store_outnumDataGridView.CurrentRow.Cells["发料人"].Value.ToString() + " 删除");

                return;
            }


            //int parentId = int.Parse(this.ly_material_plan_mainDataGridView.CurrentRow.Cells["id"].Value.ToString());
            string outnumber = this.ly_store_outnumDataGridView.CurrentRow.Cells["out_number"].Value.ToString();
            string nowwarehouse = this.ly_store_outnumDataGridView.CurrentRow.Cells["warehouse"].Value.ToString();

            if ("True" == ly_store_outDataGridView.CurrentRow.Cells["finished1"].Value.ToString())
            {
                MessageBox.Show("已经签证,领料单不能删除...");

                return;

            }



            //////////////////////////////////

            string message = "删除当前领料单:" + outnumber + "吗？";
            string caption = "提示...";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;

            DialogResult result;



            result = MessageBox.Show(message, caption, buttons,
            MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

            if (result == DialogResult.Yes)
            {

                //string delstr = " delete ly_store_out  where out_number = '" + outnumber + "'";

                string delstr = " delete ly_store_out  from ly_store_out left join ly_inma0010 on ly_store_out.wzbh=ly_inma0010.wzbh  " +
                   " where ly_store_out.out_number = '" + outnumber + "' and ly_inma0010.warehouse='" + nowwarehouse + "'";



                SqlConnection sqlConnection1 = new SqlConnection(SQLDatabase.Connectstring);
                SqlCommand cmd = new SqlCommand();

                cmd.CommandText = delstr;
                cmd.CommandType = CommandType.Text;
                cmd.Connection = sqlConnection1;

                int temp = 0;

                using (TransactionScope scope = new TransactionScope())
                {

                    sqlConnection1.Open();
                    try
                    {

                        cmd.ExecuteNonQuery();



                        scope.Complete();
                        temp = 1;


                    }
                    catch (SqlException sqle)
                    {


                        MessageBox.Show(sqle.Message.Split('*')[0]);
                    }


                    finally
                    {
                        sqlConnection1.Close();


                    }
                }
                if (1 == temp)
                {


                    //this.ly_material_plan_mainTableAdapter.Fill(this.lYPlanMange.ly_material_plan_main, "SCJH");
                }

                this.ly_store_outnumBindingSource.RemoveCurrent();

                string planNum = this.ly_sales_contract_main1DataGridView.CurrentRow.Cells["依赖书号"].Value.ToString();
                this.ly_plan_getmaterial_departmentTableAdapter.Fill(this.lYPlanMange.ly_plan_getmaterial_department, planNum);

                string innerCode = this.ly_sales_contract_main1DataGridView.CurrentRow.Cells["内部编码1"].Value.ToString();
                this.ly_sales_outbindTableAdapter.Fill(this.lYSalseMange.ly_sales_outbind, innerCode);
              


            }
        }

        private void ly_store_outnumDataGridView_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            DataGridView dgv = sender as DataGridView;

            if (SQLDatabase.nowUserName() != dgv.CurrentRow.Cells["发料人"].Value.ToString())
            {

                MessageBox.Show("请发料人:" + dgv.CurrentRow.Cells["发料人"].Value.ToString() + " 修改");

                return;
            }

            /////////////////////////////////

            if ("out_style" == dgv.CurrentCell.OwningColumn.Name)
            {
                if ("True" == ly_store_outDataGridView.CurrentRow.Cells["finished1"].Value.ToString())
                {
                    MessageBox.Show("已经签证,不能修改出库类别...");

                    return;

                }





                string sel = "SELECT a.stylecode as 编码,a.stylename as 名称 FROM ly_store_out_styleset a ";


                QueryForm queryForm = new QueryForm();


                queryForm.Sel = sel;
                queryForm.Constr = SQLDatabase.Connectstring;



                queryForm.ShowDialog();




                //dgv.CurrentRow.Cells["out_style"].Value = queryForm.Result;
                dgv.CurrentCell.Value = queryForm.Result1;
                SaveOutStyle(queryForm.Result1);



                return;

            }


            /////////////////////////////////////////////////////

            if ("employe" == dgv.CurrentCell.OwningColumn.Name)
            {
                if ("True" == ly_store_outDataGridView.CurrentRow.Cells["finished1"].Value.ToString())
                {
                    MessageBox.Show("已经签证,不能修改领料人...");

                    return;

                }



                ChangeValue queryForm = new ChangeValue();

                queryForm.OldValue = dgv.CurrentCell.Value.ToString();
                queryForm.NewValue = "";
                queryForm.ChangeMode = "string";
                queryForm.ShowDialog();



                if (queryForm.NewValue != "")
                {

                    dgv.CurrentCell.Value = queryForm.NewValue;

                    SaveEmploye(queryForm.NewValue);

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



            if ("finished" == dgv.CurrentCell.OwningColumn.Name)
            {
                return;


                if ("True" != dgv.CurrentRow.Cells["finished"].Value.ToString())
                {

                    string message = "确定领料完成吗?";
                    string caption = "提示...";
                    MessageBoxButtons buttons = MessageBoxButtons.YesNo;
                    DialogResult result;



                    result = MessageBox.Show(message, caption, buttons,
                    MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

                    if (result == DialogResult.Yes)
                    {

                        //dgv.CurrentRow.Cells["discount_money"].Value = dgv.CurrentRow.Cells["apply_money"].Value;
                        dgv.CurrentRow.Cells["finished"].Value = "True";
                        Savefinished("1");
                    }

                }
                else
                {

                    string message = "取消领料完成吗?";
                    string caption = "提示...";
                    MessageBoxButtons buttons = MessageBoxButtons.YesNo;
                    DialogResult result;



                    result = MessageBox.Show(message, caption, buttons,
                    MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

                    if (result == DialogResult.Yes)
                    {
                        //dgv.CurrentRow.Cells["discount_money"].Value = DBNull.Value;
                        //dgv.CurrentRow.Cells["apply_money"].Value = DBNull.Value;
                        dgv.CurrentRow.Cells["finished"].Value = "False";
                        Savefinished("0");
                    }
                }

                return;
            }
        }

        private void SaveEmploye(string employeName)
        {


            string outNum = this.ly_store_outnumDataGridView.CurrentRow.Cells["out_number"].Value.ToString();
            string nowwarehouse = this.ly_store_outnumDataGridView.CurrentRow.Cells["warehouse"].Value.ToString();



            string delstr = " update ly_store_out  set employe='" + employeName + "'" +
              " from ly_store_out left join ly_inma0010 on ly_store_out.wzbh=ly_inma0010.wzbh " +
             " where ly_store_out.out_number = '" + outNum + "' and ly_inma0010.warehouse='" + nowwarehouse + "'";



            SqlConnection sqlConnection1 = new SqlConnection(SQLDatabase.Connectstring);
            SqlCommand cmd = new SqlCommand();

            cmd.CommandText = delstr;
            cmd.CommandType = CommandType.Text;
            cmd.Connection = sqlConnection1;

            int temp = 0;

            using (TransactionScope scope = new TransactionScope())
            {

                sqlConnection1.Open();
                try
                {

                    cmd.ExecuteNonQuery();



                    scope.Complete();
                    temp = 1;


                }
                catch (SqlException sqle)
                {


                    MessageBox.Show(sqle.Message.Split('*')[0]);
                }


                finally
                {
                    sqlConnection1.Close();


                }
            }
            if (1 == temp)
            {


                //this.ly_material_plan_mainTableAdapter.Fill(this.lYPlanMange.ly_material_plan_main, "SCJH");
            }

        }
        private void SaveOutStyle(string outstyle)
        {
            string outNum = this.ly_store_outnumDataGridView.CurrentRow.Cells["out_number"].Value.ToString();
            string nowwarehouse = this.ly_store_outnumDataGridView.CurrentRow.Cells["warehouse"].Value.ToString();

            //string delstr = " update ly_store_out set out_style='" + outstyle + "' where out_number = '" + outNum + "'";

            string delstr = " update ly_store_out  set out_style='" + outstyle + "'" +
              " from ly_store_out left join ly_inma0010 on ly_store_out.wzbh=ly_inma0010.wzbh " +
             " where ly_store_out.out_number = '" + outNum + "' and ly_inma0010.warehouse='" + nowwarehouse + "'";


            SqlConnection sqlConnection1 = new SqlConnection(SQLDatabase.Connectstring);
            SqlCommand cmd = new SqlCommand();

            cmd.CommandText = delstr;
            cmd.CommandType = CommandType.Text;
            cmd.Connection = sqlConnection1;

            int temp = 0;

            using (TransactionScope scope = new TransactionScope())
            {

                sqlConnection1.Open();
                try
                {

                    cmd.ExecuteNonQuery();



                    scope.Complete();
                    temp = 1;


                }
                catch (SqlException sqle)
                {


                    MessageBox.Show(sqle.Message.Split('*')[0]);
                }


                finally
                {
                    sqlConnection1.Close();


                }
            }
            if (1 == temp)
            {


                //this.ly_material_plan_mainTableAdapter.Fill(this.lYPlanMange.ly_material_plan_main, "SCJH");
            }

        }
        private void Savefinished(string flag)
        {
            string outNum = this.ly_store_outnumDataGridView.CurrentRow.Cells["out_number"].Value.ToString();

            string delstr = " update ly_store_out set finished=" + flag + " where out_number = '" + outNum + "'";


            SqlConnection sqlConnection1 = new SqlConnection(SQLDatabase.Connectstring);
            SqlCommand cmd = new SqlCommand();

            cmd.CommandText = delstr;
            cmd.CommandType = CommandType.Text;
            cmd.Connection = sqlConnection1;

            int temp = 0;

            using (TransactionScope scope = new TransactionScope())
            {

                sqlConnection1.Open();
                try
                {

                    cmd.ExecuteNonQuery();



                    scope.Complete();
                    temp = 1;


                }
                catch (SqlException sqle)
                {


                    MessageBox.Show(sqle.Message.Split('*')[0]);
                }


                finally
                {
                    sqlConnection1.Close();


                }
            }
            if (1 == temp)
            {


                //this.ly_material_plan_mainTableAdapter.Fill(this.lYPlanMange.ly_material_plan_main, "SCJH");
            }

        }

        private void ly_plan_getmaterialDataGridView_SelectionChanged(object sender, EventArgs e)
        {
            if (null == this.ly_plan_getmaterialDataGridView.CurrentRow) return;
            if (null == this.ly_store_outDataGridView.CurrentRow) return;

            /////////////////////////

            DataGridView dgv = sender as DataGridView;




            string nowitem = dgv.CurrentRow.Cells["物料编号"].Value.ToString();
          

            ly_store_outDataGridView.SelectionChanged -= ly_store_outDataGridView_SelectionChanged;

            this.ly_store_outBindingSource.Position = this.ly_store_outBindingSource.Find("物料编号", nowitem);

            ly_store_outDataGridView.SelectionChanged += ly_store_outDataGridView_SelectionChanged;
        }

        private void ly_store_outDataGridView_SelectionChanged(object sender, EventArgs e)
        {
            if (null == this.ly_store_outDataGridView.CurrentRow) return;
            if (null == this.ly_plan_getmaterialDataGridView.CurrentRow) return;

            DataGridView dgv = sender as DataGridView;

            string nowitem = dgv.CurrentRow.Cells["物料编号1"].Value.ToString();

            ly_plan_getmaterialDataGridView.SelectionChanged -= ly_plan_getmaterialDataGridView_SelectionChanged;
            this.ly_plan_getmaterial_departmentBindingSource.Position = this.ly_plan_getmaterial_departmentBindingSource.Find("物料编号", nowitem);
            ly_plan_getmaterialDataGridView.SelectionChanged += ly_plan_getmaterialDataGridView_SelectionChanged;
        }

        private void ly_store_outDataGridView_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {

            DataGridView dgv = sender as DataGridView;

            if ("True" == ly_store_outDataGridView.CurrentRow.Cells["finished1"].Value.ToString())
            {
                MessageBox.Show("已经签证,领料单不能修改...");

                return;

            }

            if (SQLDatabase.nowUserName() != (ly_store_outnumDataGridView.CurrentRow.Cells["发料人"].Value.ToString()))
            {

                MessageBox.Show("请发料人:" + ly_store_outnumDataGridView.CurrentRow.Cells["发料人"].Value.ToString() + " 修改");

                return;
            }


            if ("领料数量1" == dgv.CurrentCell.OwningColumn.Name)
            {

                ChangeValue queryForm = new ChangeValue();

                queryForm.OldValue = dgv.CurrentCell.Value.ToString();
                queryForm.NewValue = "";
                queryForm.ChangeMode = "value";
                queryForm.ShowDialog();

                decimal oldnum = decimal.Parse(dgv.CurrentCell.Value.ToString());
                decimal storenum = decimal.Parse(dgv.CurrentRow.Cells["storecount"].Value.ToString());
                decimal stanterdnum = 0;

                if (null != this.ly_plan_getmaterialDataGridView.CurrentRow)
                {
                    stanterdnum = decimal.Parse(this.ly_plan_getmaterialDataGridView.CurrentRow.Cells["未领数量"].Value.ToString());
                }

                if (queryForm.NewValue != "")
                {
                    decimal newnum = decimal.Parse(queryForm.NewValue);


                    if ((newnum - oldnum) > storenum)
                    {
                        MessageBox.Show("库存不足,操作取消...");

                    }
                    else if (newnum - oldnum > stanterdnum)
                    {
                        MessageBox.Show("领料超计划,操作取消...");
                    }
                    else
                    {
                        dgv.CurrentRow.Cells["领料数量1"].Value = queryForm.NewValue;
                        //dgv.CurrentRow.Cells["discount_money"].Value = DBNull.Value;

                        //dgv.CurrentRow.Cells["approve_flag"].Value = "False";
                        SaveChanged();
                    }


                    // CountPlanStru();

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




            if ("部门名称" == dgv.CurrentCell.OwningColumn.Name)
            {

                return;
                //ChangeValue queryForm = new ChangeValue();

                //queryForm.OldValue = dgv.CurrentCell.Value.ToString();
                //queryForm.NewValue = "";
                //queryForm.ShowDialog();




                //if (queryForm.NewValue != "")
                //{
                //    dgv.CurrentRow.Cells["部门名称"].Value = queryForm.NewValue;
                //    //dgv.CurrentRow.Cells["discount_money"].Value = DBNull.Value;

                //    //dgv.CurrentRow.Cells["approve_flag"].Value = "False";
                //    SaveChanged();

                //}
                //else
                //{


                //}
                //return;

                //////////////////

                string sel = "SELECT a.prodname as 编码,a.prodcode as 名称 FROM ly_prod_dept a ";


                QueryForm queryForm = new QueryForm();


                queryForm.Sel = sel;
                queryForm.Constr = SQLDatabase.Connectstring;



                queryForm.ShowDialog();




                dgv.CurrentRow.Cells["部门名称"].Value = queryForm.Result; ;
                SaveChanged();



                return;
            }
        }
        private void SaveChanged()
        {
            ///////////////////////////

            this.ly_store_outDataGridView.EndEdit();


            this.ly_store_outBindingSource.EndEdit();



            this.ly_store_outTableAdapter.Update(this.lYStoreMange.ly_store_out);

            string planNum = this.ly_sales_contract_main1DataGridView.CurrentRow.Cells["依赖书号"].Value.ToString();

            this.ly_plan_getmaterial_departmentTableAdapter.Fill(this.lYPlanMange.ly_plan_getmaterial_department, planNum);



        }

        private void 删除子件ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (null == this.ly_store_outDataGridView.CurrentRow) return;

            if ("True" == ly_store_outnumDataGridView.CurrentRow.Cells["finished"].Value.ToString())
            {
                MessageBox.Show("已经签证,不能删除...");

                return;

            }


            int nowId = int.Parse(this.ly_store_outDataGridView.CurrentRow.Cells["idout"].Value.ToString());
           
            string componentNum = this.ly_store_outDataGridView.CurrentRow.Cells["物料编号1"].Value.ToString();


            string message1 = "当前(物料：" + componentNum + ")将被删除，继续吗？";
            string caption1 = "提示...";
            MessageBoxButtons buttons1 = MessageBoxButtons.YesNo;

            DialogResult result1;



            result1 = MessageBox.Show(message1, caption1, buttons1,
            MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

            if (result1 == DialogResult.Yes)
            {
                //ly_store_outDataGridView
                this.ly_store_outBindingSource.RemoveCurrent();
                SaveChanged();

            }
        }

        private void 打印PToolStripButton_Click(object sender, EventArgs e)
        {
            if (null == this.ly_store_outDataGridView.CurrentRow) return;

            //int parentId = int.Parse(this.ly_material_plan_mainDataGridView.CurrentRow.Cells["id"].Value.ToString());





            frmWaiting.Show(this);

            //this.ly_plan_getmaterialTableAdapter.Fill(this.lYPlanMange.ly_plan_getmaterial, parentId);

            BaseReportView queryForm = new BaseReportView();

            queryForm.Text = "中原精密生产领料单";

            queryForm.Printdata = this.lYStoreMange;

            queryForm.PrintCrystalReport = new LY_Lingliaodan();


            //string selectFormula;

            //selectFormula = "{ly_store_planitemcount.状态}  =   '原料'  and {ly_store_planitemcount.欠料金额}>0 ";
            //queryForm.PrintCrystalReport.DataDefinition.RecordSelectionFormula = selectFormula;

            frmWaiting.Hide(this);

            queryForm.ShowDialog();
        }

        private void toolStripButton7_Click(object sender, EventArgs e)
        {
            if (null == this.ly_plan_getmaterialDataGridView.CurrentRow) return;

          





            frmWaiting.Show(this);

            //this.ly_plan_getmaterialTableAdapter.Fill(this.lYPlanMange.ly_plan_getmaterial, parentId);

            BaseReportView queryForm = new BaseReportView();

            queryForm.Text = "中原精密配套欠料表";

            queryForm.Printdata = this.lYPlanMange;

            queryForm.PrintCrystalReport = new LY_PlanOwe();


            string selectFormula;

            selectFormula = "{ly_plan_getmaterial_department.仓库}  =   '" + this.comboBox2.Text + "'  and {ly_plan_getmaterial_department.未领数量}>0 ";
            queryForm.PrintCrystalReport.DataDefinition.RecordSelectionFormula = selectFormula;

            frmWaiting.Hide(this);

            queryForm.ShowDialog();
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            this.ly_sales_borrow_periodTableAdapter.Fill(this.lYSalseMange.ly_sales_borrow_period, this.dateTimePicker3.Value, this.dateTimePicker4.Value);
        
        }

        private void ly_sales_borrowDataGridView_SelectionChanged(object sender, EventArgs e)
        {

            if (this.tabControl1.SelectedIndex == 1)
            {
                if (null == ly_sales_borrowDataGridView.CurrentRow)
                {

                    return;
                }

                if (this.formStateBorrow == "View")
                {
                    string planNum = this.ly_sales_borrowDataGridView.CurrentRow.Cells["依赖书号jy"].Value.ToString();
                    this.ly_plan_getmaterial_departmentBindingSource.Filter = "";
                    this.ly_plan_getmaterial_departmentTableAdapter.Fill(this.lYPlanMange.ly_plan_getmaterial_department, planNum);

                    this.ly_storeout_employWarehouseTableAdapter.Fill(this.lYStoreMange.ly_storeout_employWarehouse, "asd", "asd", "asd");
                    this.ly_plan_deptlistTableAdapter.Fill(this.lYStoreMange.ly_plan_deptlist, "asd");

                    this.ly_plan_deptlistTableAdapter.Fill(this.lYStoreMange.ly_plan_deptlist, planNum);

                    if (this.comboBox3.Items.Count > 0)
                    {
                        this.comboBox3.SelectedIndex = 0;
                        this.ly_storeout_employWarehouseTableAdapter.Fill(this.lYStoreMange.ly_storeout_employWarehouse, planNum, this.comboBox3.SelectedValue.ToString(), SQLDatabase.NowUserID);
                    }

                    this.ly_plan_getmaterial_departmentBindingSource.Filter = "领料部门='asd'";

                    this.ly_store_outnum_borrowTableAdapter.Fill(this.lYStoreMange.ly_store_outnum_borrow, planNum, SQLDatabase.NowUserID);

                    if (null == ly_store_outnum_borrowDataGridView.CurrentRow)
                    {

                       
                        this.ly_store_borrow_outTableAdapter.Fill(this.lYStoreMange.ly_store_borrow_out, "asd");
                    }




                    this.ly_store_outnumTableAdapter.Fill(this.lYStoreMange.ly_store_outnum, planNum, SQLDatabase.NowUserID);


                    if (null == this.ly_store_outnumjyDataGridView.CurrentRow)
                    {


                        this.ly_store_outTableAdapter.Fill(this.lYStoreMange.ly_store_out, "asd", SQLDatabase.nowUserName());


                    }

                    string nowinnerCode = ly_sales_borrowDataGridView.CurrentRow.Cells["借用单号"].Value.ToString();
                    this.ly_sales_contract_detail_sumTableAdapter.Fill(this.lYSalseMange.ly_sales_contract_detail_sum, nowinnerCode, "borrow");
                }
            }

           
        }

        private void tabControl1_Selected(object sender, TabControlEventArgs e)
        {
            //if (this.tabControl1.SelectedIndex == 0)
            //{
              
            //    this.radioButton1.Checked = true;
                
            //    if (null == ly_sales_contract_main1DataGridView.CurrentRow)
            //    {

            //        return;
            //    }
                
            //    if (this.formState == "View")
            //    {
            //        this.comboBox1.SelectedIndexChanged += comboBox1_SelectedIndexChanged;
            //        this.comboBox2.SelectedIndexChanged += comboBox2_SelectedIndexChanged;
            //        this.comboBox3.SelectedIndexChanged -= comboBox3_SelectedIndexChanged;
            //        this.comboBox4.SelectedIndexChanged -= comboBox4_SelectedIndexChanged;

                    
            //        string nowinnerCode = ly_sales_contract_main1DataGridView.CurrentRow.Cells["内部编码1"].Value.ToString();


            //        this.ly_sales_contract_detail_sumTableAdapter.Fill(this.lYSalseMange.ly_sales_contract_detail_sum, nowinnerCode, "contract");



            //        string planNum = this.ly_sales_contract_main1DataGridView.CurrentRow.Cells["依赖书号"].Value.ToString();

            //        this.ly_sales_outbindTableAdapter.Fill(this.lYSalseMange.ly_sales_outbind, nowinnerCode);


            //        this.ly_plan_getmaterial_departmentTableAdapter.Fill(this.lYPlanMange.ly_plan_getmaterial_department, planNum);

            //        this.ly_storeout_employWarehouseTableAdapter.Fill(this.lYStoreMange.ly_storeout_employWarehouse, "asd", "asd", "asd");
            //        this.ly_plan_deptlistTableAdapter.Fill(this.lYStoreMange.ly_plan_deptlist, "asd");

            //        this.ly_plan_deptlistTableAdapter.Fill(this.lYStoreMange.ly_plan_deptlist, planNum);

            //        if (this.comboBox1.Items.Count > 0)
            //        {
            //            this.comboBox1.SelectedIndex = 0;
            //            this.ly_storeout_employWarehouseTableAdapter.Fill(this.lYStoreMange.ly_storeout_employWarehouse, planNum, this.comboBox1.SelectedValue.ToString(), SQLDatabase.NowUserID);
            //        }

            //        this.ly_plan_getmaterial_departmentBindingSource.Filter = "领料部门='asd'";

            //        this.ly_store_outnumTableAdapter.Fill(this.lYStoreMange.ly_store_outnum, planNum, SQLDatabase.NowUserID);

                  

            //    }
            //}
            //else if (this.tabControl1.SelectedIndex == 1)
            //{

              
            //    this.radioButton3.Checked = true;

            //    //this.ly_sales_borrowDataGridView.SelectionChanged -= ly_sales_borrowDataGridView_SelectionChanged;
            //    //this.ly_sales_borrow_periodTableAdapter.Fill(this.lYSalseMange.ly_sales_borrow_period, this.dateTimePicker3.Value, this.dateTimePicker4.Value);
            //    //this.ly_sales_borrowDataGridView.SelectionChanged += ly_sales_borrowDataGridView_SelectionChanged;

            //    if (null == ly_sales_borrowDataGridView.CurrentRow)
            //    {

            //        return;
            //    }
            //    if (this.formStateBorrow == "View")
            //    {
            //        this.comboBox1.SelectedIndexChanged -= comboBox1_SelectedIndexChanged;
            //        this.comboBox2.SelectedIndexChanged -= comboBox2_SelectedIndexChanged;
            //        this.comboBox3.SelectedIndexChanged -= comboBox3_SelectedIndexChanged;
            //        this.comboBox4.SelectedIndexChanged -= comboBox4_SelectedIndexChanged;

                   


            //        string planNum = this.ly_sales_borrowDataGridView.CurrentRow.Cells["依赖书号jy"].Value.ToString();
            //        this.ly_plan_getmaterial_departmentBindingSource.Filter = "";
            //        this.ly_plan_getmaterial_departmentTableAdapter.Fill(this.lYPlanMange.ly_plan_getmaterial_department, planNum);

            //        this.ly_storeout_employWarehouseTableAdapter.Fill(this.lYStoreMange.ly_storeout_employWarehouse, "asd", "asd", "asd");
            //        this.ly_plan_deptlistTableAdapter.Fill(this.lYStoreMange.ly_plan_deptlist, "asd");

            //        this.ly_plan_deptlistTableAdapter.Fill(this.lYStoreMange.ly_plan_deptlist, planNum);

            //        if (this.comboBox3.Items.Count > 0)
            //        {
            //            this.comboBox3.SelectedIndex = 0;
            //            this.ly_storeout_employWarehouseTableAdapter.Fill(this.lYStoreMange.ly_storeout_employWarehouse, planNum, this.comboBox3.SelectedValue.ToString(), SQLDatabase.NowUserID);
            //        }

            //        this.ly_plan_getmaterial_departmentBindingSource.Filter = "领料部门='asd'";

            //        this.ly_store_outnum_borrowTableAdapter.Fill(this.lYStoreMange.ly_store_outnum_borrow, planNum, SQLDatabase.NowUserID);

            //        string nowinnerCode = ly_sales_borrowDataGridView.CurrentRow.Cells["借用单号"].Value.ToString();
            //        this.ly_sales_contract_detail_sumTableAdapter.Fill(this.lYSalseMange.ly_sales_contract_detail_sum, nowinnerCode, "borrow");


            //        this.comboBox1.SelectedIndexChanged -= comboBox1_SelectedIndexChanged;
            //        this.comboBox2.SelectedIndexChanged -= comboBox2_SelectedIndexChanged;
            //        this.comboBox3.SelectedIndexChanged += comboBox3_SelectedIndexChanged;
            //        this.comboBox4.SelectedIndexChanged += comboBox4_SelectedIndexChanged;

                  

                    

            //    }
            //}
        }

        private void ly_store_outnum_borrowDataGridView_SelectionChanged(object sender, EventArgs e)
        {

            if (this.formStateBorrow == "View")
            {
                if (null == ly_store_outnum_borrowDataGridView.CurrentRow) return;

                string outNum = this.ly_store_outnum_borrowDataGridView.CurrentRow.Cells["out_number_borrow"].Value.ToString();

                this.ly_store_borrow_outTableAdapter.Fill(this.lYStoreMange.ly_store_borrow_out, outNum);


                string deptcode = this.ly_store_outnum_borrowDataGridView.CurrentRow.Cells["out_deptcodejy"].Value.ToString();
                string warehouse = this.ly_store_outnum_borrowDataGridView.CurrentRow.Cells["warehousejy"].Value.ToString();


                if (!string.IsNullOrEmpty(deptcode))
                {
                    if ("全部" == warehouse)
                    {

                        this.ly_plan_getmaterial_departmentBindingSource.Filter = "领料部门='" + deptcode + "'";
                        this.comboBox3.SelectedValue = deptcode;
                        this.comboBox4.SelectedValue = warehouse;
                    }
                    else
                    {
                        this.ly_plan_getmaterial_departmentBindingSource.Filter = "领料部门='" + deptcode + "' and 仓库='" + warehouse + "'";
                        this.comboBox3.SelectedValue = deptcode;
                        this.comboBox4.SelectedValue = warehouse;
                    }



                }
            }


            //if (this.formState == "View")
            //{
            //    if (null != this.ly_store_outnumDataGridView.CurrentRow)
            //    {

            //        string outNum = this.ly_store_outnumDataGridView.CurrentRow.Cells["out_number"].Value.ToString();


            //        this.ly_store_outTableAdapter.Fill(this.lYStoreMange.ly_store_out, outNum);

            //        string deptcode = this.ly_store_outnumDataGridView.CurrentRow.Cells["out_deptcode"].Value.ToString();
            //        string warehouse = this.ly_store_outnumDataGridView.CurrentRow.Cells["warehouse"].Value.ToString();

            //        if (!string.IsNullOrEmpty(deptcode))
            //        {
            //            if ("全部" == warehouse)
            //            {

            //                this.ly_plan_getmaterial_departmentBindingSource.Filter = "领料部门='" + deptcode + "'";
            //                this.comboBox1.SelectedValue = deptcode;
            //                this.comboBox2.SelectedValue = warehouse;
            //            }
            //            else
            //            {
            //                this.ly_plan_getmaterial_departmentBindingSource.Filter = "领料部门='" + deptcode + "' and 仓库='" + warehouse + "'";
            //                this.comboBox1.SelectedValue = deptcode;
            //                this.comboBox2.SelectedValue = warehouse;
            //            }



            //        }



            //    }
            //}
            //else
            //{
            //    // this.yX_taocan_mainBindingSource.Position = this.nowRow;
            //}
            
        }

        private void toolStripButton8_Click(object sender, EventArgs e)
        {
            if (null == this.ly_sales_borrowDataGridView.CurrentRow) return;
            if (null == dataGridView1.CurrentRow) return;



            if ("True" != this.ly_sales_borrowDataGridView.CurrentRow.Cells["执行j"].Value.ToString())
            {
                MessageBox.Show("营业部未发出借用出库指令,不能出库...", "注意");
                return;
            }
            if ("True" != this.ly_sales_borrowDataGridView.CurrentRow.Cells["生产审批j"].Value.ToString())
            {
                MessageBox.Show("生产部未审核出库单,不能出库...", "注意");
                return;
            }
 
            if (comboBox4.SelectedValue.ToString().Trim()=="成品")
            {
                if (string.IsNullOrEmpty(this.dataGridView1.CurrentRow.Cells["新旧_new"].Value.ToString()))
                {
                    MessageBox.Show("请联系营业，设置成品的新旧属性...", "注意");
                    return;
                }
           
            }

            string message = "确定借用发货出库吗?";
            string caption = "提示...";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result;



            result = MessageBox.Show(message, caption, buttons,
            MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

            if (result == DialogResult.Yes)
            {

                CountStoreBorrowOutAuto();
            }
        }

        private void CountStoreBorrowOutAuto()
        {
            if (null == this.ly_sales_borrowDataGridView.CurrentRow) return;
            if (null == comboBox3.SelectedValue) return;
            if (null == comboBox4.SelectedValue) return;


            string planNum = this.ly_sales_borrowDataGridView.CurrentRow.Cells["依赖书号jy"].Value.ToString();
            string innerCode = this.ly_sales_borrowDataGridView.CurrentRow.Cells["借用单号"].Value.ToString();

            NewFrm.Show(this);
            //frmWaiting.Show(this);

            SqlConnection sqlConnection1 = new SqlConnection(SQLDatabase.Connectstring);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandTimeout = 0;


            cmd.Parameters.Add("@plan_num", SqlDbType.VarChar);
            cmd.Parameters["@plan_num"].Value = planNum;

            cmd.Parameters.Add("@prod_dept", SqlDbType.VarChar);
            cmd.Parameters["@prod_dept"].Value = comboBox3.SelectedValue;

            cmd.Parameters.Add("@warehousename", SqlDbType.VarChar);
            cmd.Parameters["@warehousename"].Value = comboBox4.SelectedValue;

            string outNum = GetMaxOutNum();
            cmd.Parameters.Add("@out_number", SqlDbType.VarChar);
            cmd.Parameters["@out_number"].Value = outNum;

            cmd.Parameters.Add("@faliaoren", SqlDbType.VarChar);
            cmd.Parameters["@faliaoren"].Value = SQLDatabase.nowUserName();

            cmd.Parameters.Add("@innercode", SqlDbType.VarChar);
            cmd.Parameters["@innercode"].Value = innerCode;







            //@innercode

            cmd.CommandText = "LY_store_out_contract";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = sqlConnection1;

            sqlConnection1.Open();
            cmd.ExecuteNonQuery();
            sqlConnection1.Close();



            //this.ly_store_outTableAdapter.Fill(this.lYStoreMange.ly_store_out, outNum);
            //this.ly_plan_getmaterial_departmentTableAdapter.Fill(this.lYPlanMange.ly_plan_getmaterial_department, planNum);
            //this.ly_store_outnumTableAdapter.Fill(this.lYStoreMange.ly_store_outnum, planNum, SQLDatabase.NowUserID);

           // this.ly_store_borrow_outTableAdapter.Fill(this.lYStoreMange.ly_store_borrow_out, outNum);
            this.ly_plan_getmaterial_departmentTableAdapter.Fill(this.lYPlanMange.ly_plan_getmaterial_department, planNum);
            this.ly_store_outnum_borrowTableAdapter.Fill(this.lYStoreMange.ly_store_outnum_borrow, planNum, SQLDatabase.NowUserID);

            NewFrm.Hide(this);
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton rdb = sender as RadioButton;
            if (rdb.Checked)
            {
                SetFormStateBorrow("View");
                //this.ly_store_outTableAdapter.Fill(this.lYStoreMange.ly_store_out, "asd");
                ////this.ly_plan_getmaterialTableAdapter.Fill(this.lYPlanMange.ly_plan_getmaterial, "asd");
                this.ly_plan_getmaterial_departmentBindingSource.Filter = "领料部门='asd'";
            }
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton rdb = sender as RadioButton;
            if (rdb.Checked)
            {
                SetFormStateBorrow("Edit");
                //this.ly_store_outTableAdapter.Fill(this.lYStoreMange.ly_store_out, "asd");
            }
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (null == this.ly_sales_borrowDataGridView.CurrentRow) return;
            if (null== this.ly_sales_borrowDataGridView.CurrentRow.Cells["依赖书号jy"].Value) return;
            
            string planNum = this.ly_sales_borrowDataGridView.CurrentRow.Cells["依赖书号jy"].Value.ToString();

            //MessageBox.Show(this .comboBox1 .SelectedValue .ToString ());
            if (null != this.comboBox3.SelectedValue)
            {
                this.ly_storeout_employWarehouseTableAdapter.Fill(this.lYStoreMange.ly_storeout_employWarehouse, planNum, this.comboBox3.SelectedValue.ToString(), SQLDatabase.NowUserID);
                if (0 == this.comboBox4.Items.Count)
                {
                    this.ly_plan_getmaterial_departmentBindingSource.Filter = "领料部门='" + "asd" + "'";
                }
                else 
                
                {
                    this.comboBox4.SelectedIndex = 1;
                    this.ly_plan_getmaterial_departmentBindingSource.Filter = "领料部门='" + this.comboBox3.SelectedValue.ToString() + "' and 仓库='" + this.comboBox4.SelectedValue.ToString() + "'";
                }

            }
        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (null != this.comboBox3.SelectedValue)
            {

                if (null != this.comboBox4.SelectedValue)
                {
                    if ("全部" != this.comboBox4.SelectedValue.ToString())
                    {

                        this.ly_plan_getmaterial_departmentBindingSource.Filter = "领料部门='" + this.comboBox3.SelectedValue.ToString() + "' and 仓库='" + this.comboBox4.SelectedValue.ToString() + "'";
                    }
                    else
                    {

                        this.ly_plan_getmaterial_departmentBindingSource.Filter = "领料部门='" + this.comboBox3.SelectedValue.ToString() + "'";
                    }
                }

            }
            else
            {
                this.ly_plan_getmaterial_departmentBindingSource.Filter = "领料部门='" + "asd" + "'";

            }
        }

        private void ly_store_outnumjyDataGridView_SelectionChanged(object sender, EventArgs e)
        {
            if (null == ly_store_outnumjyDataGridView.CurrentRow) return;

            if (this.formStateBorrow == "View")
            {
                if (null != this.ly_store_outnumjyDataGridView.CurrentRow)
                {

                    string outNum = this.ly_store_outnumjyDataGridView.CurrentRow.Cells["out_number1"].Value.ToString();


                    this.ly_store_outTableAdapter.Fill(this.lYStoreMange.ly_store_out, outNum, SQLDatabase.nowUserName());

                    string deptcode = this.ly_store_outnumjyDataGridView.CurrentRow.Cells["out_deptcode1"].Value.ToString();
                    string warehouse = this.ly_store_outnumjyDataGridView.CurrentRow.Cells["warehousejy1"].Value.ToString();

                    if (!string.IsNullOrEmpty(deptcode))
                    {
                        if ("全部" == warehouse)
                        {

                            this.ly_plan_getmaterial_departmentBindingSource.Filter = "领料部门='" + deptcode + "'";
                            this.comboBox3.SelectedValue = deptcode;
                            this.comboBox4.SelectedValue = warehouse;
                        }
                        else
                        {
                            this.ly_plan_getmaterial_departmentBindingSource.Filter = "领料部门='" + deptcode + "' and 仓库='" + warehouse + "'";
                            this.comboBox3.SelectedValue = deptcode;
                            this.comboBox4.SelectedValue = warehouse;
                        }



                    }



                }
            }
            else
            {
                // this.yX_taocan_mainBindingSource.Position = this.nowRow;
            }
        }

        private void toolStripButton9_Click(object sender, EventArgs e)
        {
            if (null == this.ly_store_outDataGridView.CurrentRow) return;

            //int parentId = int.Parse(this.ly_material_plan_mainDataGridView.CurrentRow.Cells["id"].Value.ToString());





            frmWaiting.Show(this);

            //this.ly_plan_getmaterialTableAdapter.Fill(this.lYPlanMange.ly_plan_getmaterial, parentId);

            BaseReportView queryForm = new BaseReportView();

            queryForm.Text = "中原精密营业合同成品出库单";

            queryForm.Printdata = this.lYStoreMange;

            if ("True" == ly_sales_contract_main1DataGridView.CurrentRow.Cells["赠送"].Value.ToString())
            {
                //queryForm.PrintCrystalReport = new LY_Lingliaodan_give();
                queryForm.PrintCrystalReport = new LY_Lingliaodan();

            }
            else
            {



                queryForm.PrintCrystalReport = new LY_Lingliaodan();
            }


            //string selectFormula;

            //selectFormula = "{ly_store_planitemcount.状态}  =   '原料'  and {ly_store_planitemcount.欠料金额}>0 ";
            //queryForm.PrintCrystalReport.DataDefinition.RecordSelectionFormula = selectFormula;

            frmWaiting.Hide(this);

            queryForm.ShowDialog();
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.tabControl1.SelectedIndex == 0)
            {

                this.radioButton1.Checked = true;

                if (null == ly_sales_contract_main1DataGridView.CurrentRow)
                {

                    return;
                }

                if (this.formState == "View")
                {
                    //this.comboBox1.SelectedIndexChanged += comboBox1_SelectedIndexChanged;
                    //this.comboBox2.SelectedIndexChanged += comboBox2_SelectedIndexChanged;
                    //this.comboBox3.SelectedIndexChanged -= comboBox3_SelectedIndexChanged;
                    //this.comboBox4.SelectedIndexChanged -= comboBox4_SelectedIndexChanged;


                    string nowinnerCode = ly_sales_contract_main1DataGridView.CurrentRow.Cells["内部编码1"].Value.ToString();


                    this.ly_sales_contract_detail_sumTableAdapter.Fill(this.lYSalseMange.ly_sales_contract_detail_sum, nowinnerCode, "contract");



                    string planNum = this.ly_sales_contract_main1DataGridView.CurrentRow.Cells["依赖书号"].Value.ToString();

                    this.ly_sales_outbindTableAdapter.Fill(this.lYSalseMange.ly_sales_outbind, nowinnerCode);


                    this.ly_plan_getmaterial_departmentTableAdapter.Fill(this.lYPlanMange.ly_plan_getmaterial_department, planNum);

                    this.ly_storeout_employWarehouse2TableAdapter.Fill(this.lYSalseMange2.ly_storeout_employWarehouse2, "asd", "asd", "asd", "asd");
                    this.ly_plan_deptlist2TableAdapter.Fill(this.lYSalseMange2 .ly_plan_deptlist2, "asd","asd");

                    this.ly_plan_deptlist2TableAdapter.Fill(this.lYSalseMange2.ly_plan_deptlist2, planNum, nowinnerCode);

                    if (this.comboBox1.Items.Count > 0)
                    {
                        this.comboBox1.SelectedIndex = 0;
                        this.ly_storeout_employWarehouse2TableAdapter.Fill(this.lYSalseMange2.ly_storeout_employWarehouse2, planNum, this.comboBox1.SelectedValue.ToString(), SQLDatabase.NowUserID, nowinnerCode);
                    }

                    this.ly_plan_getmaterial_departmentBindingSource.Filter = "领料部门='asd'";

                    this.ly_store_outnumTableAdapter.Fill(this.lYStoreMange.ly_store_outnum, planNum, SQLDatabase.NowUserID);



                }
            }
            else if (this.tabControl1.SelectedIndex == 1)
            {


                this.radioButton3.Checked = true;

                //this.ly_sales_borrowDataGridView.SelectionChanged -= ly_sales_borrowDataGridView_SelectionChanged;
                //this.ly_sales_borrow_periodTableAdapter.Fill(this.lYSalseMange.ly_sales_borrow_period, this.dateTimePicker3.Value, this.dateTimePicker4.Value);
                //this.ly_sales_borrowDataGridView.SelectionChanged += ly_sales_borrowDataGridView_SelectionChanged;

                if (null == ly_sales_borrowDataGridView.CurrentRow)
                {
                    this.ly_sales_contract_detail_sumTableAdapter.Fill(this.lYSalseMange.ly_sales_contract_detail_sum, "asd", "borrow");
                    this.ly_store_outnumTableAdapter.Fill(this.lYStoreMange.ly_store_outnum, "asd", SQLDatabase.NowUserID);
                    return;
                }
                if (this.formStateBorrow == "View")
                {
                    //this.comboBox1.SelectedIndexChanged -= comboBox1_SelectedIndexChanged;
                    //this.comboBox2.SelectedIndexChanged -= comboBox2_SelectedIndexChanged;
                 



                    string planNum = this.ly_sales_borrowDataGridView.CurrentRow.Cells["依赖书号jy"].Value.ToString();
                    this.ly_plan_getmaterial_departmentBindingSource.Filter = "";
                    this.ly_plan_getmaterial_departmentTableAdapter.Fill(this.lYPlanMange.ly_plan_getmaterial_department, planNum);

                    this.ly_storeout_employWarehouseTableAdapter.Fill(this.lYStoreMange.ly_storeout_employWarehouse, "asd", "asd", "asd");
                    this.ly_plan_deptlistTableAdapter.Fill(this.lYStoreMange.ly_plan_deptlist, "asd");

                    this.ly_plan_deptlistTableAdapter.Fill(this.lYStoreMange.ly_plan_deptlist, planNum);

                    if (this.comboBox3.Items.Count > 0)
                    {
                        this.comboBox3.SelectedIndex = 0;
                        this.ly_storeout_employWarehouseTableAdapter.Fill(this.lYStoreMange.ly_storeout_employWarehouse, planNum, this.comboBox3.SelectedValue.ToString(), SQLDatabase.NowUserID);
                    }

                    this.ly_plan_getmaterial_departmentBindingSource.Filter = "领料部门='asd'";

                    this.ly_store_outnum_borrowTableAdapter.Fill(this.lYStoreMange.ly_store_outnum_borrow, planNum, SQLDatabase.NowUserID);

                    string nowinnerCode = ly_sales_borrowDataGridView.CurrentRow.Cells["借用单号"].Value.ToString();
                    this.ly_sales_contract_detail_sumTableAdapter.Fill(this.lYSalseMange.ly_sales_contract_detail_sum, nowinnerCode, "borrow");

                    this.ly_store_outnumTableAdapter.Fill(this.lYStoreMange.ly_store_outnum, planNum, SQLDatabase.NowUserID);


                    //this.comboBox1.SelectedIndexChanged -= comboBox1_SelectedIndexChanged;
                    //this.comboBox2.SelectedIndexChanged -= comboBox2_SelectedIndexChanged;
                    //this.comboBox3.SelectedIndexChanged += comboBox3_SelectedIndexChanged;
                    //this.comboBox4.SelectedIndexChanged += comboBox4_SelectedIndexChanged;





                }
            }
        }

       

        private void 删除借用发货ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (null == this.ly_store_outnum_borrowDataGridView.CurrentRow) return;

            if (this.formStateBorrow != "View") return;

            if (SQLDatabase.nowUserName() != ly_store_outnum_borrowDataGridView.CurrentRow.Cells["发料人jy"].Value.ToString())
            {

                MessageBox.Show("请借出人:" + ly_store_outnum_borrowDataGridView.CurrentRow.Cells["发料人jy"].Value.ToString() + " 删除");

                return;
            }


            //int parentId = int.Parse(this.ly_material_plan_mainDataGridView.CurrentRow.Cells["id"].Value.ToString());
            string outnumber = this.ly_store_outnum_borrowDataGridView.CurrentRow.Cells["out_number_borrow"].Value.ToString();
            string nowwarehouse = this.ly_store_outnum_borrowDataGridView.CurrentRow.Cells["warehousejy"].Value.ToString();

            if ("True" == ly_store_outnum_borrowDataGridView.CurrentRow.Cells["签证jy"].Value.ToString())
            {
                MessageBox.Show("已经签证,借用领料单不能删除...");

                return;

            }
            

            string message = "删除当前借用领料单:" + outnumber + "吗？";
            string caption = "提示...";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;

            DialogResult result;



            result = MessageBox.Show(message, caption, buttons,
            MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

            if (result == DialogResult.Yes)
            {


                string delstr = " delete ly_store_borrow_out  from ly_store_borrow_out left join ly_inma0010 on ly_store_borrow_out.wzbh=ly_inma0010.wzbh  " +
                " where ly_store_borrow_out.out_number = '" + outnumber + "' and ly_inma0010.warehouse='" + nowwarehouse + "'";



                string deloldStr = " delete ly_store_out_old  from ly_store_out_old left join ly_inma0010 on ly_store_out_old.wzbh=ly_inma0010.wzbh  " +
                   " where ly_store_out_old.out_number = '" + outnumber + "' and ly_inma0010.warehouse='" + nowwarehouse + "'";


                SqlConnection myConn = new SqlConnection(SQLDatabase.Connectstring);
                myConn.Open();
                SqlCommand myComm = new SqlCommand();
 
                SqlTransaction myTran;


                int temp = 0;

                myTran = myConn.BeginTransaction();
                try
                { 
                    myComm.Connection = myConn;
                    myComm.Transaction = myTran;  



                    myComm.CommandText = delstr;
                    myComm.ExecuteNonQuery(); 
                                             
                    myComm.CommandText = deloldStr;
                    myComm.ExecuteNonQuery();
        




                    myTran.Commit();



                    temp = 1;

                }
                catch (Exception err)
                {
                    myTran.Rollback();
                    MessageBox.Show("事务操作出错，系统信息：" + err.Message);
                }
                finally
                {
                    myConn.Close();
                }


 

 

                //string delstr = " delete ly_store_borrow_out  from ly_store_borrow_out left join ly_inma0010 on ly_store_borrow_out.wzbh=ly_inma0010.wzbh  " +
                //   " where ly_store_borrow_out.out_number = '" + outnumber + "' and ly_inma0010.warehouse='" + nowwarehouse + "'"; 
                //SqlConnection sqlConnection1 = new SqlConnection(SQLDatabase.Connectstring);
                //SqlCommand cmd = new SqlCommand(); 
                //cmd.CommandText = delstr;
                //cmd.CommandType = CommandType.Text;
                //cmd.Connection = sqlConnection1; 

                //int temp = 0;

                //using (TransactionScope scope = new TransactionScope())
                //{

                //    sqlConnection1.Open();
                //    try
                //    {

                //        cmd.ExecuteNonQuery(); 
                //        scope.Complete();
                //        temp = 1;


                //    }
                //    catch (SqlException sqle)
                //    { 
                //        MessageBox.Show(sqle.Message.Split('*')[0]);
                //    }


                //    finally
                //    {
                //        sqlConnection1.Close(); 
                //    }
                //}
                if (1 == temp)
                {

                    this.ly_store_outnum_borrowBindingSource.RemoveCurrent();
                    //this.ly_material_plan_mainTableAdapter.Fill(this.lYPlanMange.ly_material_plan_main, "SCJH");
                }






            }
        }

        private void 删除当前借用出库单ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (null == this.ly_store_outnumjyDataGridView.CurrentRow) return;

            if (this.formStateBorrow != "View") return;

            if (SQLDatabase.nowUserName() != ly_store_outnumjyDataGridView.CurrentRow.Cells["发料人jy1"].Value.ToString())
            {

                MessageBox.Show("请发料人:" + ly_store_outnumjyDataGridView.CurrentRow.Cells["发料人jy1"].Value.ToString() + " 删除");

                return;
            }


            //int parentId = int.Parse(this.ly_material_plan_mainDataGridView.CurrentRow.Cells["id"].Value.ToString());
            string outnumber = this.ly_store_outnumjyDataGridView.CurrentRow.Cells["out_number1"].Value.ToString();
            string nowwarehouse = this.ly_store_outnumjyDataGridView.CurrentRow.Cells["warehousejy1"].Value.ToString();

            if ("True" == ly_store_outnumjyDataGridView.CurrentRow.Cells["签证jy1"].Value.ToString())
            {
                MessageBox.Show("已经签证,借用领料单不能删除...");

                return;

            }



            //////////////////////////////////

            string message = "删除当前借用领料单:" + outnumber + "吗？";
            string caption = "提示...";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;

            DialogResult result;



            result = MessageBox.Show(message, caption, buttons,
            MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

            if (result == DialogResult.Yes)
            {

                //string delstr = " delete ly_store_out  where out_number = '" + outnumber + "'";

                string delstr = " delete ly_store_out  from ly_store_out left join ly_inma0010 on ly_store_out.wzbh=ly_inma0010.wzbh  " +
                   " where ly_store_out.out_number = '" + outnumber + "' and ly_inma0010.warehouse='" + nowwarehouse + "'";



                SqlConnection sqlConnection1 = new SqlConnection(SQLDatabase.Connectstring);
                SqlCommand cmd = new SqlCommand();

                cmd.CommandText = delstr;
                cmd.CommandType = CommandType.Text;
                cmd.Connection = sqlConnection1;

                int temp = 0;

                using (TransactionScope scope = new TransactionScope())
                {

                    sqlConnection1.Open();
                    try
                    {

                        cmd.ExecuteNonQuery();



                        scope.Complete();
                        temp = 1;


                    }
                    catch (SqlException sqle)
                    {


                        MessageBox.Show(sqle.Message.Split('*')[0]);
                    }


                    finally
                    {
                        sqlConnection1.Close();


                    }
                }
                if (1 == temp)
                {


                    //this.ly_material_plan_mainTableAdapter.Fill(this.lYPlanMange.ly_material_plan_main, "SCJH");
                }

                this.ly_store_outnumBindingSource.RemoveCurrent();





            }
        }

        private void toolStripButton15_Click(object sender, EventArgs e)
        {

            LY_sales_outInformquery queryForm = new LY_sales_outInformquery();

            queryForm.OwnerForm = this;


            queryForm.StartPosition = FormStartPosition.CenterParent;
            queryForm.ShowDialog(this);




            if (queryForm.DialogResult != DialogResult.Cancel)
            {
                //this.ly_inma0010ylTableAdapter.Fill(this.lYMaterialMange.ly_inma0010yl);
                //this.ly_inma0010ylBindingSource.Position = this.ly_inma0010ylBindingSource.Find("物资编号", queryForm.material_code);
            }          
        }

        private void toolStripButton55_Click(object sender, EventArgs e)
        {
            LY_Salescontract_OweQueryPro queryForm = new LY_Salescontract_OweQueryPro();

            //queryForm.OwnerForm = this;


            queryForm.StartPosition = FormStartPosition.CenterParent;
            queryForm.ShowDialog(this);

        }

        private void toolStripButton13_Click(object sender, EventArgs e)
        {
            if (null == this.ly_store_outjyDataGridView.CurrentRow) return;

            //int parentId = int.Parse(this.ly_material_plan_mainDataGridView.CurrentRow.Cells["id"].Value.ToString());





            frmWaiting.Show(this);

            //this.ly_plan_getmaterialTableAdapter.Fill(this.lYPlanMange.ly_plan_getmaterial, parentId);

            BaseReportView queryForm = new BaseReportView();

            queryForm.Text = "中原精密营业借用出库单";

            queryForm.Printdata = this.lYStoreMange;

            queryForm.PrintCrystalReport = new LY_LingliaodanBorrow();


            //string selectFormula;

            //selectFormula = "{ly_store_planitemcount.状态}  =   '原料'  and {ly_store_planitemcount.欠料金额}>0 ";
            //queryForm.PrintCrystalReport.DataDefinition.RecordSelectionFormula = selectFormula;

            frmWaiting.Hide(this);

            queryForm.ShowDialog();
        }

        private void ly_sales_contract_main1DataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        //private void fillToolStripButton_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        this.ly_sales_groupTableAdapter.Fill(this.lYSalseMange.ly_sales_group, material_plan_numToolStripTextBox.Text);
        //    }
        //    catch (System.Exception ex)
        //    {
        //        System.Windows.Forms.MessageBox.Show(ex.Message);
        //    }

        //}
    }
}
