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
    public partial class LY_Machine_Outsource : Form
    {
        private string nowsuppilercode = "All";
        private string nowsuppilername = "Allalllaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
        
        public LY_Machine_Outsource()
        {
            InitializeComponent();
        }

        private void MakeTreeView(DataTable table,  System.Windows.Forms.TreeNode PNode)
        {


            DataRow[] dr;
            string now_work_code;
            //string last_work_code = "___";



            dr = table.Select("work_code is not  null");

            System.Windows.Forms.TreeNode TNode = null;
            System.Windows.Forms.TreeNode CNode = null;

            foreach (DataRow d in dr)
            {
                now_work_code = d["work_code"].ToString();



             

                    TNode = new System.Windows.Forms.TreeNode();

                    TNode.Text = d["work_name"].ToString();

                   
                        TNode.Tag =  d["work_code"].ToString() ;
                   
                       
                        PNode.Nodes.Add(TNode);
                     
                  
               

            }



        }

        private void LY_Machine_Outsource_Load(object sender, EventArgs e)
        {

            ly_invoice_contractTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;

            this.ly_outmachine_contract_detail_selTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            this.lY_outmachine_contract_main_selTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
           

            this.ly_inma0010_costTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            this.ly_machinepart_process_fororderTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            this.lY_Machine_OutsourceJZTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;

            this.ly_contract_terms_formachineTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;

            this.ly_company_information_machineTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;

            this.ly_PrepaymentTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;

            this.dateTimePicker1.Text = DateTime.Today.AddMonths(-3).Date.ToString();
            this.dateTimePicker2.Text = DateTime.Today.AddDays(1).Date.ToString();


            if (SQLDatabase.CheckHaveRight(SQLDatabase.NowUserID, "外协合同审批"))
            {
                this.lY_outmachine_contract_main_selBindingSource.Filter = "";
            }
            else
            {
                this.lY_outmachine_contract_main_selBindingSource.Filter = "责任人='" + SQLDatabase.nowUserName() + "'";
            }




            SetTreeview();


        }

        private void SetTreeview()
        {
            string selAllString;



            //selAllString = "SELECT  a.salesregion_code, a.salesregion_code+':'+a.salesregion_name as salesregion_name,b.yhbm,b.yhbm+':'+b.yhmc as yhmc FROM  ly_salesregion a left join T_users b on a.salesregion_code=b.salesregion_code ORDER BY  salesregion_code ";
            //selAllString = " select distinct a.work_code,case when a.work_code='019' then a.work_code+':公司外协'  else a.work_code+':'+ isnull(b.supplier_name,'')+' '+ isnull(b.supplier_contacts,'') end as work_name from ly_production_order_detail a left join ly_supplier_list b on a.work_code=b.supplier_code where (isnull(a.outorder_flag,0)=1 or a.work_code='019') and a.work_code is not null ";
            selAllString = " select distinct a.work_code,case when a.work_code='019' then a.work_code+':公司外协'  else a.work_code+':'+ isnull(b.supplier_name,'') end as work_name from ly_production_order_detail a left join ly_supplier_list b on a.work_code=b.supplier_code where (isnull(a.outorder_flag,0)=1 or a.work_code='019') and a.work_code is not null ";

            SqlDataAdapter supplierAdapter = new SqlDataAdapter(selAllString, SQLDatabase.Connectstring);

            DataSet supplierData = new DataSet();
            supplierAdapter.Fill(supplierData);


            System.Windows.Forms.TreeNode TNode = new System.Windows.Forms.TreeNode();
            TNode.Text = "中原精密外协加工商";


            TNode.Tag = "All";
            this.treeView1.Nodes.Clear();

            this.treeView1.Nodes.Add(TNode);

            MakeTreeView(supplierData.Tables[0], TNode);

            this.treeView1.ExpandAll();
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            this.nowsuppilercode = e.Node.Tag.ToString();
            this.nowsuppilername = e.Node.Text.Substring(8, e.Node.Text.Length - 8) ;

            if (this.radioButton1.Checked)
            {
                this.lY_Machine_OutsourceJZTableAdapter.Fill(this.lYProductMange.LY_Machine_OutsourceJZ, this.nowsuppilercode, this.dateTimePicker1.Value, this.dateTimePicker2.Value, "arrange");
            }
            else
            {

                this.lY_Machine_OutsourceJZTableAdapter.Fill(this.lYProductMange.LY_Machine_OutsourceJZ, this.nowsuppilercode, this.dateTimePicker1.Value, this.dateTimePicker2.Value, "inspec");
            }



            this.lY_outmachine_contract_main_selTableAdapter.Fill(this.lYProductMange.LY_outmachine_contract_main_sel, nowsuppilercode);

            Setback();
                
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (this.radioButton1.Checked)
            {
                this.lY_Machine_OutsourceJZTableAdapter.Fill(this.lYProductMange.LY_Machine_OutsourceJZ, this.nowsuppilercode, this.dateTimePicker1.Value, this.dateTimePicker2.Value, "arrange");
            }
            else
            {

                this.lY_Machine_OutsourceJZTableAdapter.Fill(this.lYProductMange.LY_Machine_OutsourceJZ, this.nowsuppilercode, this.dateTimePicker1.Value, this.dateTimePicker2.Value, "inspec");
            }

        }

      

        private void lY_Machine_OutsourceJZDataGridView_SelectionChanged(object sender, EventArgs e)
        {

            if (null == this.lY_Machine_OutsourceJZDataGridView.CurrentRow)
            {
                return;
            }


            string nowitem = this.lY_Machine_OutsourceJZDataGridView.CurrentRow.Cells["物料编码"].Value.ToString();
            string nowproductionorderNum = this.lY_Machine_OutsourceJZDataGridView.CurrentRow.Cells["跟单编号1"].Value.ToString();
            int processorder = int.Parse(this.lY_Machine_OutsourceJZDataGridView.CurrentRow.Cells["加工工序"].Value.ToString());

            this.ly_inma0010_costTableAdapter.Fill(this.lYMaterielRequirements.ly_inma0010_cost, nowitem);



            this.ly_machinepart_process_fororderTableAdapter.Fill(this.lYMaterielRequirements.ly_machinepart_process_fororder, nowitem, nowproductionorderNum);

            this.ly_machinepart_process_fororderBindingSource.Position = this.ly_machinepart_process_fororderBindingSource.Find("顺序", processorder);
        }

        private void lY_outmachine_contract_main_selDataGridView_SelectionChanged(object sender, EventArgs e)
        {
            string nowcontract="";
            string nowcontract_id = "";

            if (null == this.lY_outmachine_contract_main_selDataGridView.CurrentRow)
            {
                ly_invoice_contractTableAdapter.Fill(this.lYStoreMange.ly_invoice_contract, "", -1);
                this.ly_PrepaymentTableAdapter.Fill(this.lYMaterielRequirements.ly_Prepayment,"");
            }
            else
            {
                nowcontract = this.lY_outmachine_contract_main_selDataGridView.CurrentRow.Cells["合同编号"].Value.ToString();
                nowcontract_id = this.lY_outmachine_contract_main_selDataGridView.CurrentRow.Cells["id"].Value.ToString();
            }
            this.ly_outmachine_contract_detail_selTableAdapter.Fill(this.lYProductMange.ly_outmachine_contract_detail_sel, nowcontract);
            this.ly_PrepaymentTableAdapter.Fill(this.lYMaterielRequirements.ly_Prepayment, nowcontract);
            this.ly_contract_terms_formachineTableAdapter.Fill(this.lYProductMange.ly_contract_terms_formachine, nowcontract);
            this.ly_company_information_machineTableAdapter.Fill(this.lYProductMange.ly_company_information_machine , nowcontract);
           // ly_invoice_contractTableAdapter.Fill(this.lYStoreMange.ly_invoice_contract, nowcontract, int.Parse(nowcontract_id));


        }

        private string GetMaxOutmachineorder()
        {
            SqlConnection sqlConnection1 = new SqlConnection(SQLDatabase.Connectstring);
            SqlCommand cmd = new SqlCommand();

            string MaxOutmachineorder = "";

            cmd.Parameters.Add("@Production_mode", SqlDbType.VarChar);
            cmd.Parameters["@Production_mode"].Value = "MO";


            cmd.CommandText = "LY_GetMax_Outmachineorder";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = sqlConnection1;

            sqlConnection1.Open();
            MaxOutmachineorder = cmd.ExecuteScalar().ToString();
            sqlConnection1.Close();



            return MaxOutmachineorder;
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if ("All" == nowsuppilercode || "019" == nowsuppilercode)
            {
                MessageBox.Show("请明确选择一个委托商", "注意");
                return;
            }

            string message = "增加外协加工结账单吗？";
            string caption = "提示...";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;

            DialogResult result;



            result = MessageBox.Show(message, caption, buttons,
            MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

            if (result == DialogResult.Yes)
            {
                this.lY_outmachine_contract_main_selBindingSource.AddNew();
                this.lY_outmachine_contract_main_selDataGridView.CurrentRow.Cells["合同编号"].Value = GetMaxOutmachineorder();

                this.lY_outmachine_contract_main_selDataGridView.CurrentRow.Cells["结算日期"].Value = SQLDatabase.GetNowdate().ToString(); ;

                this.lY_outmachine_contract_main_selDataGridView.CurrentRow.Cells["责任人"].Value = SQLDatabase.nowUserName();


                this.lY_outmachine_contract_main_selDataGridView.CurrentRow.Cells["审核"].Value = "True";


                this.lY_outmachine_contract_main_selDataGridView.CurrentRow.Cells["审核人"].Value = "默认";



                this.lY_outmachine_contract_main_selDataGridView.CurrentRow.Cells["委托商编号"].Value = nowsuppilercode;


                this.lY_outmachine_contract_main_selDataGridView.CurrentRow.Cells["委托商名称"].Value = nowsuppilername;

              
                SaveChanged();

                //this.ly_production_orderDataGridView.EndEdit();

                //this.Validate();
                //this.ly_production_orderBindingSource.EndEdit();



                //this.ly_production_orderTableAdapter.Update(this.lYMaterielRequirements.ly_production_order);

                //this.ly_production_orderTableAdapter.Fill(this.lYMaterielRequirements.ly_production_order, parentId, nowitem);

                //this.lY_MaterielRequirementsExecuteTableAdapter.Fill(this.lYMaterielRequirements.LY_MaterielRequirementsExecute, parentId, "机加");

                //SetFormState("Edit");

                //this.制定日期DateTimePicker.Focus();



            }
        }

        private void SaveChanged()
        {

            this.lY_outmachine_contract_main_selDataGridView.EndEdit();

            this.Validate();
            this.lY_outmachine_contract_main_selBindingSource.EndEdit();

            
            this.lY_outmachine_contract_main_selTableAdapter.Update(this.lYProductMange.LY_outmachine_contract_main_sel);

            if (this.radioButton1.Checked)
            {
                this.lY_Machine_OutsourceJZTableAdapter.Fill(this.lYProductMange.LY_Machine_OutsourceJZ, this.nowsuppilercode, this.dateTimePicker1.Value, this.dateTimePicker2.Value, "arrange");
            }
            else
            {

                this.lY_Machine_OutsourceJZTableAdapter.Fill(this.lYProductMange.LY_Machine_OutsourceJZ, this.nowsuppilercode, this.dateTimePicker1.Value, this.dateTimePicker2.Value, "inspec");
            }

            //this.ly_outmachine_contract_detail_selTableAdapter.Fill(this.lYProductMange.ly_outmachine_contract_detail_sel, nowsuppilercode);

            //this.ly_outmachine_contract_detail_selTableAdapter.Update(this.lYProductMange.ly_outmachine_contract_detail_sel);

            //int nowId = int.Parse(this.lY_outmachine_contract_main_selDataGridView.CurrentRow.Cells["id"].Value.ToString());

            //string nowitem = this.lY_MaterielRequirementsDataGridView.CurrentRow.Cells["物料编码1"].Value.ToString();

            //this.ly_production_orderTableAdapter.Fill(this.lYMaterielRequirements.ly_production_order, parentId, nowitem);

            //string now_require_id = this.lY_MaterielRequirementsDataGridView.CurrentRow.Cells["require_id"].Value.ToString();

            //this.lY_MaterielRequirementsExecuteTableAdapter.Fill(this.lYMaterielRequirements.LY_MaterielRequirementsExecute, parentId, "机加");

            //this.lY_MaterielRequirementsExecuteBindingSource.Position = this.lY_MaterielRequirementsExecuteBindingSource.Find("id", now_require_id);
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            DataGridView dgv = lY_outmachine_contract_main_selDataGridView;

            if (null == dgv.CurrentRow) return;
            if (ly_cg_fk.Rows.Count > 0)
            {
                MessageBox.Show("已有预付,不能删除数据...", "注意");
                return;
            }
            if ("True" == dgv.CurrentRow.Cells["批准"].Value.ToString())
            {
                MessageBox.Show("合同已经审批,不能删除数据...", "注意");
                return;

            }

             string message1 = "删除当前选择合同，继续吗？";
            string caption1 = "提示...";
            MessageBoxButtons buttons1 = MessageBoxButtons.YesNo;

            DialogResult result1;



            result1 = MessageBox.Show(message1, caption1, buttons1,
            MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

            if (result1 == DialogResult.Yes)
            {
                this.lY_outmachine_contract_main_selBindingSource.RemoveCurrent();



                SaveChanged();

            }



           
       
        }

        private void lY_Machine_OutsourceJZDataGridView_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            DataGridView dgv = lY_Machine_OutsourceJZDataGridView;

            if (null == dgv.CurrentRow) return;
            if (null == lY_outmachine_contract_main_selDataGridView.CurrentRow) return;

            if ("True" == lY_outmachine_contract_main_selDataGridView.CurrentRow.Cells["批准"].Value.ToString())
            {
                MessageBox.Show("合同已经审批,不能增加数据...", "注意");
                return;

            }

            decimal hegeqty;
            if (null != dgv.CurrentRow)
            {
                hegeqty = decimal.Parse(dgv.CurrentRow.Cells["合格数量a"].Value.ToString());
            }
            else
            {
                hegeqty = 0;
            }

            decimal huiyongqty;
            if (null != dgv.CurrentRow)
            {
                huiyongqty = decimal.Parse(dgv.CurrentRow.Cells["回用数量a"].Value.ToString());
            }
            else
            {
                huiyongqty = 0;
            }

            decimal fanxiuqty;
            if (null != dgv.CurrentRow)
            {
                fanxiuqty = decimal.Parse(dgv.CurrentRow.Cells["返修数量a"].Value.ToString());
            }
            else
            {
                fanxiuqty = 0;
            }


            decimal feipinjiezhangqty;
            if (null != dgv.CurrentRow)
            {
                feipinjiezhangqty = decimal.Parse(dgv.CurrentRow.Cells["废品结账数量"].Value.ToString());
            }
            else
            {
                feipinjiezhangqty = 0;
            }

            if (0 >= hegeqty + huiyongqty + fanxiuqty+ feipinjiezhangqty)
            {
                MessageBox.Show("可结账数量为0,操作取消", "注意");
                return;

            }
            /////////////////

            if (!string.IsNullOrEmpty(dgv.CurrentRow.Cells["结账单号"].Value.ToString()))
            {
                MessageBox.Show("条目已经处理,不能重复结账...", "注意");
                return;
            }

            int originid = int.Parse(dgv.CurrentRow.Cells["oriid"].Value.ToString());

            if (0 <= this.ly_outmachine_contract_detail_selBindingSource.Find("origin_id", originid))
            {
                MessageBox.Show("条目已经选择,不能重复...", "注意");
                this.ly_outmachine_contract_detail_selBindingSource.Position = this.ly_outmachine_contract_detail_selBindingSource.Find("origin_id", originid);
                return;

            }

            string nowcontractcode;
            if (null != lY_outmachine_contract_main_selDataGridView.CurrentRow)
            {
                nowcontractcode = this.lY_outmachine_contract_main_selDataGridView.CurrentRow.Cells["合同编号"].Value.ToString();
            }
            else
            {
                nowcontractcode = "";
            }

         

            if ("True" != dgv.CurrentRow.Cells["approve"].Value.ToString())
            {
                MessageBox.Show("外协价格没有审批...", "注意");
                return;

            } 
            if (string.IsNullOrEmpty(dgv.CurrentRow.Cells["外协审批"].Value.ToString()))
            {
                MessageBox.Show("没有外协价格，无法添加！"); return;
            }

            if ( (decimal.Parse( dgv.CurrentRow.Cells["外协审批"].Value.ToString() ) -0 ) <=0 )
            {
                MessageBox.Show("外协价格为0，无法添加！"); return;
            }
            dgv.CurrentRow.Cells["结账单号"].Value = nowcontractcode;

            this.ly_outmachine_contract_detail_selBindingSource.AddNew();
            if (!string.IsNullOrEmpty(dgv.CurrentRow.Cells["外协审批"].Value.ToString()) )
            {
                this.ly_outmachine_contract_detail_selDataGridView.CurrentRow.Cells["单价3"].Value = decimal.Parse(dgv.CurrentRow.Cells["外协审批"].Value.ToString());
            }


            this.ly_outmachine_contract_detail_selDataGridView.CurrentRow.Cells["合同编号3"].Value = nowcontractcode;
            this.ly_outmachine_contract_detail_selDataGridView.CurrentRow.Cells["origin_id"].Value = originid;

            Savedetail();
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            
            
            DataGridView dgv = ly_outmachine_contract_detail_selDataGridView;

            if (null == dgv.CurrentRow) return;

          

            if ("True" == lY_outmachine_contract_main_selDataGridView.CurrentRow.Cells["批准"].Value.ToString())
            {
                MessageBox.Show("合同已经审批,不能删除数据...", "注意");
                return;

            }

       
            ///////////////////////////////////////////////////////////////

            string message1 = "删除当前选择合同物料，继续吗？";
            string caption1 = "提示...";
            MessageBoxButtons buttons1 = MessageBoxButtons.YesNo;

            DialogResult result1;



            result1 = MessageBox.Show(message1, caption1, buttons1,
            MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

            if (result1 == DialogResult.Yes)
            {
                this.ly_outmachine_contract_detail_selBindingSource.RemoveCurrent();



                Savedetail();
                  

            }
        }

        private void lY_Machine_OutsourceJZDataGridView_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            string isgood = "yes";

         

                /////////////////////////////////////////////////////////////////////////////////////////////////////////


            //Setback();
                //else
                //{ 


                //}



            
        }

        private void Setback()
        {
            if (null == lY_Machine_OutsourceJZDataGridView.CurrentRow) return;
            
            foreach (DataGridViewRow dgr in lY_Machine_OutsourceJZDataGridView.Rows)
            {


                if (!string.IsNullOrEmpty(dgr.Cells["结账单号"].Value.ToString()))
                {


                    foreach (DataGridViewCell dgc in dgr.Cells)
                    {

                        dgc.Style.BackColor = Color.Gray;
                        dgc.Style.ForeColor = Color.DarkBlue;
                    }

                }
            }
        }

        private void ly_outmachine_contract_detail_selDataGridView_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            DataGridView dgv = ly_outmachine_contract_detail_selDataGridView;






            if ("True" == lY_outmachine_contract_main_selDataGridView.CurrentRow.Cells["批准"].Value.ToString())
            {
                MessageBox.Show("合同已经批准,不能修改...", "注意");
                return;

            }



            if ("单价3" == dgv.CurrentCell.OwningColumn.Name)
            {

                return;

                //if (!string.IsNullOrEmpty(dgv.CurrentCell.Value.ToString()))
                //{
                //    MessageBox.Show("外协单价已经审批,不能修改...", "注意");
                //    return;
                //}
                ChangeValue queryForm = new ChangeValue();

                queryForm.OldValue = dgv.CurrentCell.Value.ToString();
                queryForm.NewValue = "";
                queryForm.ChangeMode = "value";
                queryForm.ShowDialog();




                if (queryForm.NewValue != "")
                {
                    dgv.CurrentRow.Cells["单价3"].Value = queryForm.NewValue;

                    Savedetail();

                }
                else
                {
                    dgv.CurrentRow.Cells["单价3"].Value = 0;
                    Savedetail();
                }
                return;

            }
            //////////////////////////////////

            /////////////////////////////
            if ("税率" == dgv.CurrentCell.OwningColumn.Name)
            {

                ChangeValue queryForm = new ChangeValue();

                queryForm.OldValue = dgv.CurrentCell.Value.ToString();
                queryForm.NewValue = "";
                queryForm.ChangeMode = "value";
                queryForm.ShowDialog();




                if (queryForm.NewValue != "")
                {
                    dgv.CurrentRow.Cells["税率"].Value = queryForm.NewValue;
                    //dgv.CurrentRow.Cells["discount_money"].Value = DBNull.Value;

                    //dgv.CurrentRow.Cells["approve_flag"].Value = "False";
                    Savedetail();
                    //CountPlanStru();

                }
                else
                {
                    dgv.CurrentRow.Cells["税率"].Value = 0;
                    //dgv.CurrentRow.Cells["discount_money"].Value = DBNull.Value;

                    //dgv.CurrentRow.Cells["approve_flag"].Value = "False";
                    Savedetail();
                }
                return;

            }

            if ("发票" == dgv.CurrentCell.OwningColumn.Name)
            {


                string sel = "SELECT  id as 编号, tax_type_name as 发票类型 FROM ly_tax_type  ";
                QueryForm queryForm = new QueryForm();


                queryForm.Sel = sel;
                queryForm.Constr = SQLDatabase.Connectstring;

                queryForm.ShowDialog();


                if (queryForm.Result != "")
                {

                    dgv.CurrentRow.Cells["发票"].Value = queryForm.Result1;


                }
                else
                {
                    dgv.CurrentRow.Cells["发票"].Value = DBNull.Value;

                }
                Savedetail();
                return;
            }

            /////////////////////////////


            if ("到货日期" == dgv.CurrentCell.OwningColumn.Name)
            {

                ChangeValue queryForm = new ChangeValue();

                queryForm.OldValue = dgv.CurrentCell.Value.ToString();
                queryForm.NewValue = "";
                queryForm.ChangeMode = "datetime";
                queryForm.ShowDialog();




                if (queryForm.NewValue != "")
                {
                    dgv.CurrentRow.Cells["到货日期"].Value = queryForm.NewValue;
                    //dgv.CurrentRow.Cells["票到录入人"].Value = SQLDatabase.nowUserName();
                    //dgv.CurrentRow.Cells["discount_money"].Value = DBNull.Value;

                    //dgv.CurrentRow.Cells["approve_flag"].Value = "False";
                    Savedetail();


                    //CountPlanStru();

                }
                else
                {
                    dgv.CurrentRow.Cells["到货日期"].Value = DBNull.Value;
                    //dgv.CurrentRow.Cells["票到录入人"].Value = DBNull.Value;
                    Savedetail();


                }
                return;

            }
            /////////////////////////////

            ///////////////////////////
            if ("备注3" == dgv.CurrentCell.OwningColumn.Name)
            {

                ChangeValue queryForm = new ChangeValue();

                queryForm.OldValue = dgv.CurrentCell.Value.ToString();
                queryForm.NewValue = "";
                queryForm.ChangeMode = "longstring";
                queryForm.ShowDialog();




                if (queryForm.NewValue != "")
                {
                    dgv.CurrentRow.Cells["备注3"].Value = queryForm.NewValue;
                    //dgv.CurrentRow.Cells["discount_money"].Value = DBNull.Value;

                    //dgv.CurrentRow.Cells["approve_flag"].Value = "False";
                    Savedetail();

                    //CountPlanStru();

                }
                else
                {
                    dgv.CurrentRow.Cells["备注3"].Value = DBNull.Value;
                    Savedetail();
                }
                return;
            }
        }

        private void Savedetail()
        {
            ly_outmachine_contract_detail_selDataGridView.EndEdit();
            ly_outmachine_contract_detail_selBindingSource.EndEdit();

            this.ly_outmachine_contract_detail_selTableAdapter.Update(this.lYProductMange.ly_outmachine_contract_detail_sel);

            /////////////////////

            if (null == ly_outmachine_contract_detail_selDataGridView.CurrentRow) return;

            int originid = int.Parse(ly_outmachine_contract_detail_selDataGridView.CurrentRow.Cells["origin_id"].Value.ToString());

            //if (0 <= this.ly_outmachine_contract_detail_selBindingSource.Find("origin_id", originid))
            //{
            //    MessageBox.Show("条目已经选择,不能重复...", "注意");
            //    this.ly_outmachine_contract_detail_selBindingSource.Position = this.ly_outmachine_contract_detail_selBindingSource.Find("origin_id", originid);
            //    return;

            //}



            string nowcontractcode;
            if (null != lY_outmachine_contract_main_selDataGridView.CurrentRow)
            {
                nowcontractcode = this.lY_outmachine_contract_main_selDataGridView.CurrentRow.Cells["合同编号"].Value.ToString();
            }
            else
            {
                nowcontractcode = "";
            }

            this.lY_outmachine_contract_main_selTableAdapter.Fill(this.lYProductMange.LY_outmachine_contract_main_sel, nowsuppilercode);


            this.lY_outmachine_contract_main_selBindingSource.Position = this.lY_outmachine_contract_main_selBindingSource.Find("合同编号", nowcontractcode);

            this.ly_outmachine_contract_detail_selBindingSource.Position = this.ly_outmachine_contract_detail_selBindingSource.Find("origin_id", originid);

            //if (this.radioButton1.Checked)
            //{
            //    this.lY_Machine_OutsourceJZTableAdapter.Fill(this.lYProductMange.LY_Machine_OutsourceJZ, this.nowsuppilercode, this.dateTimePicker1.Value, this.dateTimePicker2.Value, "arrange");
            //}
            //else
            //{

            //    this.lY_Machine_OutsourceJZTableAdapter.Fill(this.lYProductMange.LY_Machine_OutsourceJZ, this.nowsuppilercode, this.dateTimePicker1.Value, this.dateTimePicker2.Value, "inspec");
            //}
        }

        private void 打印PToolStripButton_Click(object sender, EventArgs e)
        {
            if (null == this.ly_outmachine_contract_detail_selDataGridView.CurrentRow) return;

            //if ("True" != ly_purchase_contract_mainDataGridView.CurrentRow.Cells["批准"].Value.ToString())
            //{
            //    MessageBox.Show("合同未经生产审批,不能打印...", "注意");
            //    return;

            //}
            //if ("True" != ly_purchase_contract_mainDataGridView.CurrentRow.Cells["审核"].Value.ToString())
            //{
            //    MessageBox.Show("合同未经质检审核,不能打印...", "注意");
            //    return;

            //}




           // NewFrm.Show(this);  
            //this.ly_plan_getmaterialTableAdapter.Fill(this.lYPlanMange.ly_plan_getmaterial, parentId);

            BaseReportView queryForm = new BaseReportView();

            queryForm.Text = "中原精密加工基地委托加工结账单";

            queryForm.Printdata = this.lYProductMange;

            //if ("WT00002" == this.nowsuppilercode)
            //{

            //    queryForm.PrintCrystalReport = new LY_weituojiagongdan1();
            //}
            //else
            //{
                queryForm.PrintCrystalReport = new LY_weituojiagongdan();
            //}


            //string selectFormula;

            //selectFormula = "{ly_store_planitemcount.状态}  =   '原料'  and {ly_store_planitemcount.欠料金额}>0 ";
            //queryForm.PrintCrystalReport.DataDefinition.RecordSelectionFormula = selectFormula;

            // write abou

           // NewFrm.Hide(this);

            queryForm.ShowDialog();
        }

        private void lY_outmachine_contract_main_selDataGridView_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (null == ly_outmachine_contract_detail_selDataGridView.CurrentRow)
            {
                return;
            }

            //foreach (DataGridViewRow dgr in ly_outmachine_contract_detail_selDataGridView.Rows)
            //{

            //    if (string.IsNullOrEmpty(dgr.Cells["合格金额3"].Value.ToString().Replace(" ", "")))
            //    {
            //        MessageBox.Show("合同明细中存在无单价条目,不能通过...", "注意");
            //        return;
            //    }


            //}

            DataGridView dgv = sender as DataGridView;

            //if ("True" == dgv.CurrentRow.Cells["审定"].Value.ToString())
            //{
            //    MessageBox.Show("合同已经审定,不能修改...", "注意");
            //    return;

            //}

            if ("True" == dgv.CurrentRow.Cells["批准"].Value.ToString())
            {
                MessageBox.Show("合同已经批准,不能修改...", "注意");
                return;

            }


            ///////////////////////////////////////////////////////////
            //if ("批准" == dgv.CurrentCell.OwningColumn.Name)
            //{

            //    if ("True" == dgv.CurrentRow.Cells["批准"].Value.ToString())
            //    {
            //        dgv.CurrentRow.Cells["批准"].Value = "False";
            //        dgv.CurrentRow.Cells["审批人"].Value = DBNull.Value;

            //    }
            //    else
            //    {

            //        dgv.CurrentRow.Cells["批准"].Value = "True";
            //        dgv.CurrentRow.Cells["审批人"].Value = SQLDatabase.nowUserName();
            //    }



            //    SaveChanged();



            //    return;

            //}
            /////////////////////////////////////////////////////////////

            /////////////////////////////////////////////////////////////
            //if ("审核" == dgv.CurrentCell.OwningColumn.Name)
            //{

            //    if ("True" == dgv.CurrentRow.Cells["审核"].Value.ToString())
            //    {
            //        dgv.CurrentRow.Cells["审核"].Value = "False";
            //        dgv.CurrentRow.Cells["审核人"].Value = DBNull.Value;

            //    }
            //    else
            //    {

            //        dgv.CurrentRow.Cells["审核"].Value = "True";
            //        dgv.CurrentRow.Cells["审核人"].Value = SQLDatabase.nowUserName();
            //    }



            //    SaveChanged();



            //    return;

            //}
            /////////////////////////////////////////////////////////////


            //int nowcontractId;
            //if (null != dgv.CurrentRow)
            //{
            //    nowcontractId = int.Parse(dgv.CurrentRow.Cells["id6"].Value.ToString());
            //}
            //else
            //{
            //    nowcontractId = 0;
            //}




            //if (null != this.ly_material_plan_mainDataGridView.CurrentRow)
            //{

            //    string planNum = this.ly_material_plan_mainDataGridView.CurrentRow.Cells["计划编号"].Value.ToString();


            //    string nowOwningColumnName = dgv.CurrentCell.OwningColumn.Name;
            //    this.ly_purchase_contract_mainTableAdapter.Fill(this.lYMaterielRequirements.ly_purchase_contract_main, planNum);

            //    this.ly_purchase_contract_mainBindingSource.Position = this.ly_purchase_contract_mainBindingSource.Find("id", nowcontractId);


            //    ly_purchase_contract_mainDataGridView.CurrentCell = ly_purchase_contract_mainDataGridView.CurrentRow.Cells[nowOwningColumnName];
            //}

            //dgv = ly_purchase_contract_mainDataGridView;

            /////////////////////////////////////////////////////////

            //if ("True" == dgv.CurrentRow.Cells["批准"].Value.ToString())
            //{
            //    MessageBox.Show("合同已经审批,不能修改数据...", "注意");
            //    return;

            //}

           ////////////////////////////////////////////
           
           //////////////////////////


            if ("开票日期" == dgv.CurrentCell.OwningColumn.Name)
            {

                ChangeValue queryForm = new ChangeValue();

                queryForm.OldValue = dgv.CurrentCell.Value.ToString();
                queryForm.NewValue = "";
                queryForm.ChangeMode = "datetime";
                queryForm.ShowDialog();




                if (queryForm.NewValue != "")
                {
                    dgv.CurrentRow.Cells["开票日期"].Value = queryForm.NewValue;
                    dgv.CurrentRow.Cells["开票人"].Value = SQLDatabase.nowUserName();

                    //dgv.CurrentRow.Cells["approve_flag"].Value = "False";
                    SaveChanged();


                    //CountPlanStru();

                }
                else
                {
                    dgv.CurrentRow.Cells["开票日期"].Value = DBNull.Value;
                    dgv.CurrentRow.Cells["开票人"].Value = DBNull.Value;

                    SaveChanged();

                }
                return;

            }
            ////////////////////////////////////////////
            if ("税率0" == dgv.CurrentCell.OwningColumn.Name)
            {

                ChangeValue queryForm = new ChangeValue();

                queryForm.OldValue = dgv.CurrentCell.Value.ToString();
                queryForm.NewValue = "";
                queryForm.ChangeMode = "value";
                queryForm.ShowDialog();




                if (queryForm.NewValue != "")
                {
                    dgv.CurrentRow.Cells["税率0"].Value = queryForm.NewValue;
                    //dgv.CurrentRow.Cells["discount_money"].Value = DBNull.Value;

                    //dgv.CurrentRow.Cells["approve_flag"].Value = "False";
                    SaveChanged();

                    //CountPlanStru();

                }
                else
                {
                    dgv.CurrentRow.Cells["税率0"].Value = 0;
                    //dgv.CurrentRow.Cells["discount_money"].Value = DBNull.Value;

                    //dgv.CurrentRow.Cells["approve_flag"].Value = "False";
                    SaveChanged();
                }
                return;

            }

            /////////////////////////////
            if ("发票类别" == dgv.CurrentCell.OwningColumn.Name)
            {


                string sel = "SELECT  id as 编号, tax_type_name as 发票类型 FROM ly_tax_type  ";
                QueryForm queryForm = new QueryForm();


                queryForm.Sel = sel;
                queryForm.Constr = SQLDatabase.Connectstring;

                queryForm.ShowDialog();


                if (queryForm.Result != "")
                {

                    dgv.CurrentRow.Cells["发票类别"].Value = queryForm.Result1;


                }
                //else
                //{
                //    dgv.CurrentRow.Cells["发票类别"].Value = DBNull.Value;

                //}
                SaveChanged();
                return;
            }
            /////////////////////////////
            if ("账期" == dgv.CurrentCell.OwningColumn.Name)
            {

                ChangeValue queryForm = new ChangeValue();

                queryForm.OldValue = dgv.CurrentCell.Value.ToString();
                queryForm.NewValue = "";
                queryForm.ChangeMode = "number";
                queryForm.ShowDialog();




                if (queryForm.NewValue != "")
                {
                    dgv.CurrentRow.Cells["账期"].Value = queryForm.NewValue;
                    //dgv.CurrentRow.Cells["discount_money"].Value = DBNull.Value;

                    //dgv.CurrentRow.Cells["approve_flag"].Value = "False";
                    SaveChanged();
                    //CountPlanStru();

                }
                else
                {
                    dgv.CurrentRow.Cells["账期"].Value = DBNull.Value;
                    SaveChanged();

                    ;
                }
                return;

            }

            /////////////////////////////
            if ("运费" == dgv.CurrentCell.OwningColumn.Name)
            {

                ChangeValue queryForm = new ChangeValue();

                queryForm.OldValue = dgv.CurrentCell.Value.ToString();
                queryForm.NewValue = "";
                queryForm.ChangeMode = "number";
                queryForm.ShowDialog();




                if (queryForm.NewValue != "")
                {
                    dgv.CurrentRow.Cells["运费"].Value = queryForm.NewValue;
                    //dgv.CurrentRow.Cells["discount_money"].Value = DBNull.Value;

                    //dgv.CurrentRow.Cells["approve_flag"].Value = "False";
                    SaveChanged();

                    //CountPlanStru();

                }
                else
                {
                    dgv.CurrentRow.Cells["运费"].Value = DBNull.Value;
                    SaveChanged();
                   
                }
                return;

            }

            ///////////////////////////////////////////////////////////

            ///////////////////////////////////////

            if ("结算日期" == dgv.CurrentCell.OwningColumn.Name)
            {

                ChangeValue queryForm = new ChangeValue();

                queryForm.OldValue = dgv.CurrentCell.Value.ToString();
                queryForm.NewValue = "";
                queryForm.ChangeMode = "datetime";
                queryForm.ShowDialog();




                if (queryForm.NewValue != "")
                {
                    dgv.CurrentRow.Cells["结算日期"].Value = queryForm.NewValue;
                    //dgv.CurrentRow.Cells["discount_money"].Value = DBNull.Value;

                    //dgv.CurrentRow.Cells["approve_flag"].Value = "False";
                    SaveChanged();


                    //CountPlanStru();

                }
                else
                {
                    dgv.CurrentRow.Cells["结算日期"].Value = DBNull.Value; ;
                   
                    SaveChanged();

                }
                return;

            }

            ///////////////////////////
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

                }
                return;

            }

            ///////////////////////////////////////////////////////////
            if ("对方合同号" == dgv.CurrentCell.OwningColumn.Name)
            {

                ChangeValue queryForm = new ChangeValue();

                queryForm.OldValue = dgv.CurrentCell.Value.ToString();
                queryForm.NewValue = "";
                queryForm.ChangeMode = "string";
                queryForm.ShowDialog();




                if (queryForm.NewValue != "")
                {
                    dgv.CurrentRow.Cells["对方合同号"].Value = queryForm.NewValue;
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

            /////////////////////////////////////////////////////////////
            //if ("提交" == dgv.CurrentCell.OwningColumn.Name)
            //{

            //    if ("True" == dgv.CurrentRow.Cells["提交"].Value.ToString())
            //    {
            //        dgv.CurrentRow.Cells["提交"].Value = "False";

            //    }
            //    else
            //    {

            //        dgv.CurrentRow.Cells["提交"].Value = "True";
            //    }



            //    SaveContract();





            //    return; 

            //}
            /////////////////////////////////////////////////////////////
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {

            if (null == this.ly_outmachine_contract_detail_selDataGridView.CurrentRow) return;

            string nowcontractcode = this.lY_outmachine_contract_main_selDataGridView.CurrentRow.Cells["合同编号"].Value.ToString();



            //NewFrm.Show(this); ;

            //this.ly_plan_getmaterialTableAdapter.Fill(this.lYPlanMange.ly_plan_getmaterial, parentId);

            BaseReportView queryForm = new BaseReportView();

            queryForm.Text = "中原精密外协合同";

            queryForm.Printdata = this.lYProductMange;
            queryForm.PrintCrystalReport = new LY_weituoHetongNew();

            queryForm.CrystalReportViewer1.ShowExportButton = false;

            queryForm.Outfilename = nowcontractcode;




            //string selectFormula;

            //selectFormula = "{ly_store_planitemcount.状态}  =   '原料'  and {ly_store_planitemcount.欠料金额}>0 ";
            //queryForm.PrintCrystalReport.DataDefinition.RecordSelectionFormula = selectFormula;

            //NewFrm.Hide(this);

            queryForm.ShowDialog();

           //////////////////////////////////////////////////////////////////////////////
            
            
            //if (null == this.ly_outmachine_contract_detail_selDataGridView.CurrentRow) return;



            //NewFrm.Show(this); ;

            ////this.ly_plan_getmaterialTableAdapter.Fill(this.lYPlanMange.ly_plan_getmaterial, parentId);

            //BaseReportView queryForm = new BaseReportView();

            //queryForm.Text = "中原精密委托加工合同";


            //queryForm.Printdata = this.lYProductMange;

            //queryForm.PrintCrystalReport = new LY_weituohetong();

            ////queryForm.Printdata = this.lYOutsourceData;

            ////queryForm.PrintCrystalReport = new LY_WaixieHetong();


            ////string selectFormula;

            ////selectFormula = "{ly_store_planitemcount.状态}  =   '原料'  and {ly_store_planitemcount.欠料金额}>0 ";
            ////queryForm.PrintCrystalReport.DataDefinition.RecordSelectionFormula = selectFormula;

            //NewFrm.Hide(this);

            //queryForm.ShowDialog();
        }

        private void lY_outmachine_contract_main_selDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void 导入合同标准条款ToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if ("True" == lY_outmachine_contract_main_selDataGridView.CurrentRow.Cells["批准"].Value.ToString())
            {
                MessageBox.Show("合同已经批准,不能修改...", "注意");
                return;

            }



            string message = "导入标准合同条款吗吗？";
            string caption = "提示...";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result;



            result = MessageBox.Show(message, caption, buttons,
            MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

            if (result == DialogResult.Yes)
            {

                string nowcontractcode = this.lY_outmachine_contract_main_selDataGridView.CurrentRow.Cells["合同编号"].Value.ToString();


                Get_ContractSet(nowcontractcode);


                this.ly_contract_terms_formachineTableAdapter.Fill(this.lYProductMange.ly_contract_terms_formachine, nowcontractcode);

                MessageBox.Show("导入合同标准条款成功!", "注意");
            }
        }

        private void Get_ContractSet(string nowcontractcode)
        {
            SqlConnection sqlConnection1 = new SqlConnection(SQLDatabase.Connectstring);
            SqlCommand cmd = new SqlCommand();

            //string nowcontractcode = this.ly_purchase_contract_mainDataGridView.CurrentRow.Cells["合同编号"].Value.ToString();

            cmd.Parameters.Add("@contract_code", SqlDbType.VarChar);
            cmd.Parameters["@contract_code"].Value = nowcontractcode;

            cmd.Parameters.Add("@contract_type", SqlDbType.VarChar);
            cmd.Parameters["@contract_type"].Value = "machine";




            cmd.CommandText = "LY_contract_termscopy";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = sqlConnection1;

            sqlConnection1.Open();
            cmd.ExecuteNonQuery();
            sqlConnection1.Close();



        }

        private void ly_sales_contract_terms_forcontractDataGridView_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            DataGridView dgv = sender as DataGridView;

            if (null == dgv.CurrentRow) return;

            if ("True" == lY_outmachine_contract_main_selDataGridView.CurrentRow.Cells["批准"].Value.ToString())
            {
                MessageBox.Show("合同已经批准,不能修改...", "注意");
                return;

            }




            ///////////////////////////////////////////////////////////////
            if ("编号" == dgv.CurrentCell.OwningColumn.Name)
            {

                ChangeValue queryForm = new ChangeValue();

                queryForm.OldValue = dgv.CurrentCell.Value.ToString();
                queryForm.NewValue = "";
                queryForm.ChangeMode = "string";
                queryForm.ShowDialog();




                if (queryForm.NewValue != "")
                {
                    dgv.CurrentRow.Cells["编号"].Value = queryForm.NewValue;
                    //dgv.CurrentRow.Cells["discount_money"].Value = DBNull.Value;

                    //dgv.CurrentRow.Cells["approve_flag"].Value = "False";
                    this.ly_contract_terms_formachineBindingSource.EndEdit();
                    this.ly_contract_terms_formachineTableAdapter.Update(this.lYProductMange.ly_contract_terms_formachine);

                    //CountPlanStru();

                }
                else
                {

                }
                return;

            }
            ///////////////////////////////////////////////////////////////
            if ("条款选项" == dgv.CurrentCell.OwningColumn.Name)
            {

                ChangeValue queryForm = new ChangeValue();

                queryForm.OldValue = dgv.CurrentCell.Value.ToString();
                queryForm.NewValue = "";
                queryForm.ChangeMode = "longstring";
                queryForm.ShowDialog();




                if (queryForm.NewValue != "")
                {
                    dgv.CurrentRow.Cells["条款选项"].Value = queryForm.NewValue;
                    //dgv.CurrentRow.Cells["discount_money"].Value = DBNull.Value;

                    //dgv.CurrentRow.Cells["approve_flag"].Value = "False";
                    this.ly_contract_terms_formachineBindingSource.EndEdit();
                    this.ly_contract_terms_formachineTableAdapter.Update(this.lYProductMange.ly_contract_terms_formachine);
                    //CountPlanStru();

                }
                else
                {

                }
                return;

            }

            ///////////////////////////////////////////////////////////////
            if ("条款描述" == dgv.CurrentCell.OwningColumn.Name)
            {

                ChangeValue queryForm = new ChangeValue();

                queryForm.OldValue = dgv.CurrentCell.Value.ToString();
                queryForm.NewValue = "";
                queryForm.ChangeMode = "longstring";
                queryForm.ShowDialog();




                if (queryForm.NewValue != "")
                {
                    dgv.CurrentRow.Cells["条款描述"].Value = queryForm.NewValue;
                    //dgv.CurrentRow.Cells["discount_money"].Value = DBNull.Value;

                    //dgv.CurrentRow.Cells["approve_flag"].Value = "False";
                    this.ly_contract_terms_formachineBindingSource.EndEdit();
                    this.ly_contract_terms_formachineTableAdapter.Update(this.lYProductMange.ly_contract_terms_formachine);
                    //CountPlanStru();

                }
                else
                {

                }
                return;

            }


            ///////////////////////////////////////////////////////
        }

       

        private void 导入公司信息ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if ("True" == lY_outmachine_contract_main_selDataGridView.CurrentRow.Cells["批准"].Value.ToString())
            {
                MessageBox.Show("合同已经批准,不能修改...", "注意");
                return;

            }


            string message = "导入公司基本信息吗？";
            string caption = "提示...";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result;



            result = MessageBox.Show(message, caption, buttons,
            MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

            if (result == DialogResult.Yes)
            {

                string nowcontractcode = this.lY_outmachine_contract_main_selDataGridView.CurrentRow.Cells["合同编号"].Value.ToString();

                
                Get_CompanySet(nowcontractcode);


                this.ly_company_information_machineTableAdapter.Fill(this.lYProductMange.ly_company_information_machine, nowcontractcode);

                MessageBox.Show("导入公司信息成功!", "注意");
            }


        }

        private void Get_CompanySet(string nowcontractcode)
        {
            SqlConnection sqlConnection1 = new SqlConnection(SQLDatabase.Connectstring);
            SqlCommand cmd = new SqlCommand();

            //string nowcontractcode = this.ly_purchase_contract_mainDataGridView.CurrentRow.Cells["合同编号"].Value.ToString();

            cmd.Parameters.Add("@contract_code", SqlDbType.VarChar);
            cmd.Parameters["@contract_code"].Value = nowcontractcode;

            cmd.Parameters.Add("@contract_type", SqlDbType.VarChar);
            cmd.Parameters["@contract_type"].Value = "machine";




            cmd.CommandText = "LY_contract_CompanyInfo";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = sqlConnection1;

            sqlConnection1.Open();
            cmd.ExecuteNonQuery();
            sqlConnection1.Close();

          

        }

        private void 统一指定税率ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (null == ly_outmachine_contract_detail_selDataGridView.CurrentRow) return;

            if ("True" == lY_outmachine_contract_main_selDataGridView.CurrentRow.Cells["批准"].Value.ToString())
            {
                MessageBox.Show("合同已经批准,不能修改...", "注意");
                return;

            }


            DataGridView dgv = ly_outmachine_contract_detail_selDataGridView;

            string message = "统一指定税率结账吗？";
            string caption = "提示...";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;

            DialogResult result;



            result = MessageBox.Show(message, caption, buttons,
            MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

            if (result == DialogResult.Yes)
            {


                ChangeValue queryForm = new ChangeValue();

                queryForm.OldValue = dgv.CurrentCell.Value.ToString();
                queryForm.NewValue = "";
                queryForm.ChangeMode = "value";
                queryForm.ShowDialog();




                if (queryForm.NewValue != "")
                {
                    foreach (DataGridViewRow dgr in dgv.Rows)
                    {



                        dgr.Cells["税率"].Value = queryForm.NewValue;



                    }
                    
                
                    
                    Savedetail();
                   

                }
                //else
                //{
                //    dgv.CurrentRow.Cells["税率"].Value = 0;
                //    //dgv.CurrentRow.Cells["discount_money"].Value = DBNull.Value;

                //    //dgv.CurrentRow.Cells["approve_flag"].Value = "False";
                //    Savedetail();
                //}
            }
              

        }

        private void 更新委托商信息ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if ("True" == lY_outmachine_contract_main_selDataGridView.CurrentRow.Cells["批准"].Value.ToString())
            {
                MessageBox.Show("合同已经批准,不能修改...", "注意");
                return;

            }


            string message = "更新委托商信息吗？";
            string caption = "提示...";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;

            DialogResult result;



            result = MessageBox.Show(message, caption, buttons,
            MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

            if (result == DialogResult.Yes)
            {

                string nowcontractcode = this.lY_outmachine_contract_main_selDataGridView.CurrentRow.Cells["合同编号"].Value.ToString();

                string updString = " update ly_outmachine_contract_main  "
                                 + " set ly_outmachine_contract_main.supplier_people=ly_supplier_list.supplier_contacts,  "
                                 + "  ly_outmachine_contract_main.supplier_phone=ly_supplier_list.supplier_phone,  "
                                 + "  ly_outmachine_contract_main.supplier_fax=ly_supplier_list.fax_num,  "
                                 + "  ly_outmachine_contract_main.supplier_address=ly_supplier_list.supplier_address  "
                                 + "  from ly_outmachine_contract_main left join   "
                                 + "  ly_supplier_list on ly_outmachine_contract_main.supplier_code=ly_supplier_list.supplier_code  "
                                 + "  where ly_outmachine_contract_main.contract_code=  '" + nowcontractcode + "'";


               

                SqlConnection myConn = new SqlConnection(SQLDatabase.Connectstring);

                SqlCommand myCom = new SqlCommand(updString, myConn);
                myCom.CommandType = CommandType.Text;

                try
                {
                    myCom.Connection.Open();
                    myCom.ExecuteNonQuery();
                    myConn.Close();


                }
                catch (SqlException eSql)
                {

                    throw new Exception(eSql.Message, eSql);
                }


                this.lY_outmachine_contract_main_selTableAdapter.Fill(this.lYProductMange.LY_outmachine_contract_main_sel, this .nowsuppilercode);


                this.lY_outmachine_contract_main_selBindingSource.Position = this.lY_outmachine_contract_main_selBindingSource.Find("合同编号", nowcontractcode);
              
            }

        }

        private void lY_Machine_OutsourceJZDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void 指定指定加工商ToolStripMenuItem_Click(object sender, EventArgs e)
        {

            // if ("True" == lY_outmachine_contract_main_selDataGridView.CurrentRow.Cells["审核"].Value.ToString())
            //{
            //    MessageBox.Show("合同已经审核,不能修改...", "注意");
            //    return;

            //}

            if (!("019" == this.nowsuppilercode || "WX" == this.nowsuppilercode.Substring(0,2)))
             {
                 return;
             }

             if (!string.IsNullOrEmpty(lY_Machine_OutsourceJZDataGridView.CurrentRow.Cells["结账单号"].Value.ToString()))
                {
                    MessageBox.Show("条目已经处理,不能重复结账...", "注意");
                    return;
                }


            string message = "指定加工商吗？";
            string caption = "提示...";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;

            DialogResult result;



            result = MessageBox.Show(message, caption, buttons,
            MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

            if (result == DialogResult.Yes)
            {

                string sel = "SELECT  supplier_code as 工号, supplier_name as 姓名 FROM ly_supplier_list where sort_code='2' ";


                QueryForm queryForm = new QueryForm();


                queryForm.Sel = sel;
                queryForm.Constr = SQLDatabase.Connectstring;



                queryForm.ShowDialog();



                if (string.IsNullOrEmpty(queryForm.Result))
                {
                    MessageBox.Show("必须选择委托加工商,才能继续...", "注意");
                    return;
                }

                this.lY_Machine_OutsourceJZDataGridView.CurrentRow.Cells["委托商编号3"].Value = queryForm.Result;
                this.lY_Machine_OutsourceJZDataGridView.CurrentRow.Cells["委托商名称3"].Value = queryForm.Result1;

                string updString = "";
                string now_detail_id = this.lY_Machine_OutsourceJZDataGridView.CurrentRow.Cells["detail_id3"].Value.ToString();
                string jbcode= this.lY_Machine_OutsourceJZDataGridView.CurrentRow.Cells["跟单编号1"].Value.ToString();
                if (jbcode.Substring(0, 2) == "JG")
                {
                    updString = " update ly_production_order_detail  "
                                 + " set outorder_flag=1,work_code='" + queryForm.Result + "',  work_name='" + queryForm.Result1 + "'"

                                 + "  where id=  " + now_detail_id;

                }
                if (jbcode.Substring(0, 2) == "WX")
                {
                    updString = " update ly_outsource_order_detail  "
                                 + " set outorder_flag=1,work_code='" + queryForm.Result + "',  work_name='" + queryForm.Result1 + "'"

                                 + "  where id=  " + now_detail_id;

                }

                SqlConnection myConn = new SqlConnection(SQLDatabase.Connectstring);

                SqlCommand myCom = new SqlCommand(updString, myConn);
                myCom.CommandType = CommandType.Text;

                try
                {
                    myCom.Connection.Open();
                    myCom.ExecuteNonQuery();
                    myConn.Close();


                }
                catch (SqlException eSql)
                {

                    throw new Exception(eSql.Message, eSql);
                }

              
            }

        }

        private void 提交更新ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (null == this.lY_Machine_OutsourceJZDataGridView.CurrentRow)
            {
                return;
            
            }

            string nowsupplier = this.lY_Machine_OutsourceJZDataGridView.CurrentRow.Cells["委托商编号3"].Value.ToString();

            SetTreeview();


            this.treeView1.SelectedNode = FindNode(this.treeView1.Nodes, nowsupplier);
        }

        private TreeNode FindNode(TreeNodeCollection tnParent, string strValue)
        {

            if (tnParent == null) return null;

            //if (tnParent.Text == strValue) return tnParent;



            TreeNode tnRet = null;

            foreach (TreeNode tn in tnParent)
            {



                if (tn.Tag.ToString() == strValue)
                {
                    tnRet = tn;
                }
                else
                {

                    tnRet = FindNode(tn.Nodes, strValue);
                }

                if (tnRet != null) break;

            }

            return tnRet;

        }

        private void toolStripButton27_Click(object sender, EventArgs e)
        {

            ly_invoiceMg queryForm = new ly_invoiceMg();
            queryForm.StartPosition = FormStartPosition.CenterParent;
            queryForm.ShowDialog();
        }

        private void toolStripButton30_Click(object sender, EventArgs e)
        {
            if (lY_outmachine_contract_main_selDataGridView.CurrentRow == null)
            {
                return;
            }
            string contrat_code_now = lY_outmachine_contract_main_selDataGridView.CurrentRow.Cells["合同编号"].Value.ToString();
            string salespeople = this.lY_outmachine_contract_main_selDataGridView.CurrentRow.Cells["责任人"].Value.ToString();
            decimal max_price = decimal.Parse(this.lY_outmachine_contract_main_selDataGridView.CurrentRow.Cells["应付金额"].Value.ToString());
            if (salespeople != SQLDatabase.nowUserName())
            {
                MessageBox.Show("请采购员:" + salespeople + "申请", "注意");
                return;
            }
            LY_pay_money queryForm = new LY_pay_money();
            queryForm.contract_code_add = contrat_code_now;
            queryForm.max_moeny = max_price;
            queryForm.StartPosition = FormStartPosition.CenterParent;
            queryForm.ShowDialog();
            string remark = "";
            decimal price = 0;
            if (queryForm.DialogResult != DialogResult.Cancel)
            {
                remark = queryForm.txtRemark;
                price = queryForm.moeny;
            }
            this.ly_PrepaymentBindingSource.AddNew();


            ly_cg_fk.CurrentRow.Cells["申请金额1"].Value = price;
            ly_cg_fk.CurrentRow.Cells["申请日期1"].Value = SQLDatabase.GetNowdate();

            ly_cg_fk.CurrentRow.Cells["合同编号1"].Value = contrat_code_now;
            ly_cg_fk.CurrentRow.Cells["备注1"].Value = remark;

            this.ly_cg_fk.EndEdit();
            this.ly_PrepaymentBindingSource.EndEdit();
            this.ly_PrepaymentTableAdapter.Update(this.lYMaterielRequirements.ly_Prepayment);
        }

        private void toolStripButton31_Click(object sender, EventArgs e)
        {
            if (lY_outmachine_contract_main_selDataGridView.CurrentRow == null)
            {
                return;
            }
            string contrat_code_now = lY_outmachine_contract_main_selDataGridView.CurrentRow.Cells["合同编号"].Value.ToString();
            string salespeople = this.lY_outmachine_contract_main_selDataGridView.CurrentRow.Cells["责任人"].Value.ToString();
            if (salespeople != SQLDatabase.nowUserName())
            {
                MessageBox.Show("请采购员:" + salespeople + "删除", "注意");
                return;
            }
            if (ly_cg_fk.CurrentRow == null)
            { return; }
            if (ly_cg_fk.CurrentRow.Cells["审批1"].Value.ToString() == "True")
            {
                MessageBox.Show("已经审批...", "注意");
                return;
            }
            string caption = "提示...";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result;
            string message = "确定要删除吗";

            result = MessageBox.Show(message, caption, buttons,
            MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

            if (result == DialogResult.Yes)
            {
                this.ly_PrepaymentBindingSource.RemoveCurrent();
                this.ly_cg_fk.EndEdit();
                this.ly_PrepaymentBindingSource.EndEdit();
                this.ly_PrepaymentTableAdapter.Update(this.lYMaterielRequirements.ly_Prepayment);
            }

        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            DataGridView dgv = lY_Machine_OutsourceJZDataGridView;

            if (null == dgv.CurrentRow) return;
            if (null == lY_outmachine_contract_main_selDataGridView.CurrentRow) return;

            if ("True" == lY_outmachine_contract_main_selDataGridView.CurrentRow.Cells["批准"].Value.ToString())
            {
                MessageBox.Show("合同已经批准,不能增加数据...", "注意");
                return;

            }
            foreach (DataGridViewRow dgr in dgv.Rows)
            {
                if (true == dgr.Selected)
                {

                    decimal hegeqty;
                    if (null != dgr)
                    {
                        hegeqty = decimal.Parse(dgr.Cells["合格数量a"].Value.ToString());
                    }
                    else
                    {
                        hegeqty = 0;
                    }

                    decimal huiyongqty;
                    if (null != dgr)
                    {
                        huiyongqty = decimal.Parse(dgr.Cells["回用数量a"].Value.ToString());
                    }
                    else
                    {
                        huiyongqty = 0;
                    }

                    decimal fanxiuqty;
                    if (null != dgr)
                    {
                        fanxiuqty = decimal.Parse(dgr.Cells["返修数量a"].Value.ToString());
                    }
                    else
                    {
                        fanxiuqty = 0;
                    }


                    decimal feipinjiezhangqty;
                    if (null != dgr)
                    {
                        feipinjiezhangqty = decimal.Parse(dgr.Cells["废品结账数量"].Value.ToString());
                    }
                    else
                    {
                        feipinjiezhangqty = 0;
                    }

                    if (0 >= hegeqty + huiyongqty + fanxiuqty + feipinjiezhangqty)
                    {
                        continue;


                    }
                    /////////////////

                    if (!string.IsNullOrEmpty(dgr.Cells["结账单号"].Value.ToString()))
                    {
                        continue;
                    }

                    int originid = int.Parse(dgr.Cells["oriid"].Value.ToString());

                    if (0 <= this.ly_outmachine_contract_detail_selBindingSource.Find("origin_id", originid))
                    {
                        this.ly_outmachine_contract_detail_selBindingSource.Position = this.ly_outmachine_contract_detail_selBindingSource.Find("origin_id", originid);

                        continue;

                    }

                    string nowcontractcode;
                    if (null != lY_outmachine_contract_main_selDataGridView.CurrentRow)
                    {
                        nowcontractcode = this.lY_outmachine_contract_main_selDataGridView.CurrentRow.Cells["合同编号"].Value.ToString();
                    }
                    else
                    {
                        nowcontractcode = "";
                    }



                    if ("True" != dgr.Cells["approve"].Value.ToString())
                    {
                        continue;

                    }
                    if (string.IsNullOrEmpty(dgr.Cells["外协审批"].Value.ToString()))
                    {
                        continue;
                    }

                    if ((decimal.Parse(dgr.Cells["外协审批"].Value.ToString()) - 0) <= 0)
                    {
                        continue;
                    }
                    dgr.Cells["结账单号"].Value = nowcontractcode;

                    this.ly_outmachine_contract_detail_selBindingSource.AddNew();
                    if (!string.IsNullOrEmpty(dgr.Cells["外协审批"].Value.ToString()))
                    {
                        this.ly_outmachine_contract_detail_selDataGridView.CurrentRow.Cells["单价3"].Value = decimal.Parse(dgr.Cells["外协审批"].Value.ToString());
                    }


                    this.ly_outmachine_contract_detail_selDataGridView.CurrentRow.Cells["合同编号3"].Value = nowcontractcode;
                    this.ly_outmachine_contract_detail_selDataGridView.CurrentRow.Cells["origin_id"].Value = originid;
                }
            }
            Savedetail();

        }
    }
}
