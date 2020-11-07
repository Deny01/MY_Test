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

namespace HappyYF.YuanXin.WorkSet
{
    public partial class LY_Machine_Outsource_approve3 : Form
    {
        private string nowsuppilercode = "All";
        private string nowsuppilername = "Allalllaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";

        public LY_Machine_Outsource_approve3()
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



            this.ly_outmachine_contract_detail_selTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            this.lY_outmachine_contract_main_selTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
           

            this.ly_inma0010_costTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            this.ly_machinepart_process_fororderTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            this.lY_Machine_OutsourceJZTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;



            this.dateTimePicker1.Text = DateTime.Today.AddMonths(-3).Date.ToString();
            this.dateTimePicker2.Text = DateTime.Today.AddDays(1).Date.ToString();

            this.lY_outmachine_contract_main_selBindingSource.Filter = "审定  = 'True'";
            
            string selAllString;

           

                //selAllString = "SELECT  a.salesregion_code, a.salesregion_code+':'+a.salesregion_name as salesregion_name,b.yhbm,b.yhbm+':'+b.yhmc as yhmc FROM  ly_salesregion a left join T_users b on a.salesregion_code=b.salesregion_code ORDER BY  salesregion_code ";
                selAllString = " select distinct work_code,work_code+':'+ work_name as work_name from ly_production_order_detail where isnull(outorder_flag,0)=1 and work_code is not null ";
               
                SqlDataAdapter supplierAdapter = new SqlDataAdapter(selAllString, SQLDatabase.Connectstring);

            DataSet supplierData = new DataSet();
            supplierAdapter.Fill(supplierData);


            System.Windows.Forms.TreeNode TNode = new System.Windows.Forms.TreeNode();
            TNode.Text = "中原精密外协加工商";


            TNode.Tag = "All";
          

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

            if (null != this.lY_outmachine_contract_main_selDataGridView.CurrentRow)
            {
                 nowcontract = this.lY_outmachine_contract_main_selDataGridView.CurrentRow.Cells["合同编号"].Value.ToString();
            }

            this.ly_outmachine_contract_detail_selTableAdapter.Fill(this.lYProductMange.ly_outmachine_contract_detail_sel, nowcontract);
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
            if ("All" == nowsuppilercode)
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

            if ("True" == dgv.CurrentRow.Cells["审核"].Value.ToString())
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

            return;

             if (null == dgv.CurrentRow) return;

             if ("True" == lY_outmachine_contract_main_selDataGridView.CurrentRow.Cells["审核"].Value.ToString())
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


                if (0 >= hegeqty + huiyongqty + fanxiuqty)
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

                    dgv.CurrentRow.Cells["结账单号"].Value = nowcontractcode;

                    this.ly_outmachine_contract_detail_selBindingSource.AddNew();

                    this.ly_outmachine_contract_detail_selDataGridView.CurrentRow.Cells["合同编号3"].Value = nowcontractcode;
                    this.ly_outmachine_contract_detail_selDataGridView.CurrentRow.Cells["origin_id"].Value = originid;

                    Savedetail();
                  

                    //SaveContractdetail();

               


            //}
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            
            
            DataGridView dgv = ly_outmachine_contract_detail_selDataGridView;

            if (null == dgv.CurrentRow) return;

          

            if ("True" == lY_outmachine_contract_main_selDataGridView.CurrentRow.Cells["审核"].Value.ToString())
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




         


            /////////////////////////////////////////////////////////

            //if ("True" == ly_purchase_contract_mainDataGridView.CurrentRow.Cells["批准"].Value.ToString())
            //{
            //    MessageBox.Show("合同已经审批,不能修改数据...", "注意");
            //    return;

            //}

            ///////////////////////////////////////////////////////////////

         
            /////////////////////////////
            if ("合格金额3" == dgv.CurrentCell.OwningColumn.Name)
            {

                ChangeValue queryForm = new ChangeValue();

                queryForm.OldValue = dgv.CurrentCell.Value.ToString();
                queryForm.NewValue = "";
                queryForm.ChangeMode = "value";
                queryForm.ShowDialog();




                if (queryForm.NewValue != "")
                {
                    dgv.CurrentRow.Cells["合格金额3"].Value = queryForm.NewValue;
                    //dgv.CurrentRow.Cells["discount_money"].Value = DBNull.Value;

                    //dgv.CurrentRow.Cells["approve_flag"].Value = "False";
                    Savedetail();
                    //CountPlanStru();

                }
                else
                {
                    dgv.CurrentRow.Cells["合格金额3"].Value = 0;
                    //dgv.CurrentRow.Cells["discount_money"].Value = DBNull.Value;

                    //dgv.CurrentRow.Cells["approve_flag"].Value = "False";
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




            //NewFrm.Show(this); 2018-09-12

            //this.ly_plan_getmaterialTableAdapter.Fill(this.lYPlanMange.ly_plan_getmaterial, parentId);

            BaseReportView queryForm = new BaseReportView();

            queryForm.Text = "中原精密加工基地委托加工结账单";

            queryForm.Printdata = this.lYProductMange;

            queryForm.PrintCrystalReport = new LY_weituojiagongdan();


            //string selectFormula;

            //selectFormula = "{ly_store_planitemcount.状态}  =   '原料'  and {ly_store_planitemcount.欠料金额}>0 ";
            //queryForm.PrintCrystalReport.DataDefinition.RecordSelectionFormula = selectFormula;

            //NewFrm.Hide(this)2018-09-12

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

            //if ( !string .IsNullOrEmpty( dgv.CurrentRow.Cells["开票人"].Value.ToString()))
            //{
            //    MessageBox.Show("合同已经开票,不能修改...", "注意");
            //    return;

            //}

            //if ("True" == dgv.CurrentRow.Cells["批准"].Value.ToString())
            //{
            //    MessageBox.Show("合同已经批准,不能修改...", "注意");
            //    return;

            //}


            ///////////////////////////////////////////////////////////
            //if ("审定" == dgv.CurrentCell.OwningColumn.Name)
            //{

            //    if ("True" == dgv.CurrentRow.Cells["审定"].Value.ToString())
            //    {
            //        dgv.CurrentRow.Cells["审定"].Value = "False";
            //        dgv.CurrentRow.Cells["审定人"].Value = DBNull.Value;

            //    }
            //    else
            //    {

            //        dgv.CurrentRow.Cells["审定"].Value = "True";
            //        dgv.CurrentRow.Cells["审定人"].Value = SQLDatabase.nowUserName();
            //    }



            //    SaveChanged();



            //    return;

            //}
            ///////////////////////////////////////////////////////////

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
                    //dgv.CurrentRow.Cells["discount_money"].Value = DBNull.Value;

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

            ///////////////////////////
            //    if ("备注" == dgv.CurrentCell.OwningColumn.Name)
            //    {

            //        ChangeValue queryForm = new ChangeValue();

            //        queryForm.OldValue = dgv.CurrentCell.Value.ToString();
            //        queryForm.NewValue = "";
            //        queryForm.ChangeMode = "longstring";
            //        queryForm.ShowDialog();




            //        if (queryForm.NewValue != "")
            //        {
            //            dgv.CurrentRow.Cells["备注"].Value = queryForm.NewValue;
            //            //dgv.CurrentRow.Cells["discount_money"].Value = DBNull.Value;

            //            //dgv.CurrentRow.Cells["approve_flag"].Value = "False";
            //            SaveChanged();

            //            //CountPlanStru();

            //        }
            //        else
            //        {

            //        }
            //        return;
            //    }
            //}
        }


        //private void fillToolStripButton_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        this.lY_outmachine_contract_main_selTableAdapter.Fill(this.lYProductMange.LY_outmachine_contract_main_sel, supplier_codeToolStripTextBox.Text);
            
        //    }
        //    catch (System.Exception ex)
        //    {
        //        System.Windows.Forms.MessageBox.Show(ex.Message);
        //    }

        //}

        //private void fillToolStripButton_Click_1(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        this.ly_outmachine_contract_detail_selTableAdapter.Fill(this.lYProductMange.ly_outmachine_contract_detail_sel, contract_codeToolStripTextBox.Text);
                
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
        //        this.ly_inma0010_costTableAdapter.Fill(this.lYMaterielRequirements.ly_inma0010_cost, wzbhToolStripTextBox.Text);
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
        //        this.lY_Machine_OutsourceJZTableAdapter.Fill(this.lYProductMange.LY_Machine_OutsourceJZ, workcodeToolStripTextBox.Text, new System.Nullable<System.DateTime>(((System.DateTime)(System.Convert.ChangeType(begindateToolStripTextBox.Text, typeof(System.DateTime))))), new System.Nullable<System.DateTime>(((System.DateTime)(System.Convert.ChangeType(enddateToolStripTextBox.Text, typeof(System.DateTime))))));
        //    }
        //    catch (System.Exception ex)
        //    {
        //        System.Windows.Forms.MessageBox.Show(ex.Message);
        //    }

        //}
    }
}
