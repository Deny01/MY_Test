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
    public partial class LY_Salescontract_SubmitSet : Form
    {
        public string innercode;
        public string contractcode;
        public string clientcode;
        public string billcode;

        public string fromwhere;

        public string runmode;
        
        public LY_Salescontract_SubmitSet()
        {
            InitializeComponent();
        }

      

        private void LY_Salescontract_SubmitSet_Load(object sender, EventArgs e)
        {
            if ("contract" == this.fromwhere)
            {
                this.Text = "合同发货设定";
            }
            else
            {
              
                this.Text = "借用发货设定";

                this.tabPage2.Parent = null;
                this.splitContainer4.Panel2Collapsed=true ;

                this.ly_sales_contract_detail_sumDataGridView.Columns["充借数量qd"].Visible = false;
            }

            this.ly_sales_borrow_detail_clientTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            
            this.ly_sales_contract_detail_sumTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;

            this.ly_sales_outbindTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            this.ly_sales_outbindTableAdapter.Fill(this.lYSalseMange.ly_sales_outbind, this.innercode);
            
          
            this.ly_sales_contract_detail_sumTableAdapter.Fill(this.lYSalseMange.ly_sales_contract_detail_sum, this.innercode, this .fromwhere);

            this.f_PlanExtend_LSPT_sum1TableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            this.f_PlanExtend_LSPT_sum1TableAdapter.Fill(this.lYSalseMange.f_PlanExtend_LSPT_sum1, this.billcode, this.fromwhere);

            this.f_PlanExtend_LSPT_sumTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;

            this.ly_material_plan_mainTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            this.ly_material_plan_mainTableAdapter.Fill(this.lYSalseMange.ly_material_plan_main, "LSPT", this .contractcode );
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {

            if (CheckData())
            {

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            //else
            //{
            //    this.DialogResult = DialogResult.Cancel;
            //}
           
        }

        private bool  CheckData()
        {
            decimal nowbillqty;
            decimal nowplanqty;
            decimal nowjieyongqty;

            string nowitemno;
            string nowitemnoname;
            string nowappendflag;
            string nowpassflag;

            foreach (DataGridViewRow dgr in ly_sales_contract_detail_sumDataGridView.Rows)
            {

                nowitemno = dgr.Cells["产品编码qd"].Value.ToString();
                nowitemnoname = dgr.Cells["产品名称qd"].Value.ToString();
                nowappendflag = dgr.Cells["附加"].Value.ToString();
                nowpassflag = dgr.Cells["转赠批准"].Value.ToString();

                if ("" != dgr.Cells["数量qd"].Value.ToString())
                {
                    nowbillqty = decimal.Parse(dgr.Cells["数量qd"].Value.ToString());
                }
                else
                {
                    nowbillqty = 0;
                }

                if ("" != dgr.Cells["实发数量qd"].Value.ToString())
                {
                    nowplanqty = decimal.Parse(dgr.Cells["实发数量qd"].Value.ToString());
                }
                else
                {
                    nowplanqty = 0;
                }

                if ("" != dgr.Cells["充借数量qd"].Value.ToString())
                {
                    nowjieyongqty = decimal.Parse(dgr.Cells["充借数量qd"].Value.ToString());
                }
                else
                {
                    nowjieyongqty = 0;
                }


                if ("True" == nowappendflag &&  nowplanqty == 0) continue;

            
                if ("安装调试费" == nowitemnoname) continue;

                int hadarrenged1 = this.f_PlanExtend_LSPT_sum1BindingSource.Find("产品编码", nowitemno);
                int hadarrenged2 = this.ly_sales_outbindBindingSource.Find("成品编码", nowitemno);

                if (-1 > (hadarrenged1 + hadarrenged2))
                {
                    if ("Z0000001" != nowitemno)
                    {
                        MessageBox.Show("发货清单产品:" + nowitemno + " 无出货安排,请检查依赖书发货数据...", "注意");
                        return false;
                    }

                }

              



                //if ("Z0000001"!=nowitemno)

                if ((nowbillqty != (nowplanqty + nowjieyongqty)) )
                {

                    if ("True" == nowpassflag) continue;

                    if ("Z0000001" != nowitemno)
                    {
                        MessageBox.Show("发货清单产品:" + nowitemno + " 清单发货数量和依赖书出货数量不一致,请检查依赖书发货数据...", "注意");
                        return false;
                    }


                }

            }

            foreach (DataGridViewRow dgr in dataGridView1.Rows)
            {
                nowitemno = dgr.Cells["产品编码yl"].Value.ToString();

                int hadarrenged = this.ly_sales_contract_detail_sumBindingSource.Find("产品编码", nowitemno);


                if (0 > hadarrenged)
                {
                    MessageBox.Show("依赖书产品:" + nowitemno + " 无发货清单安排,请检查发货清单数据...", "注意");
                    return false;

                }

            }

            foreach (DataGridViewRow dgr in ly_sales_outbindDataGridView.Rows)
            {
                nowitemno = dgr.Cells["成品编码fh"].Value.ToString();

                int hadarrenged = this.ly_sales_contract_detail_sumBindingSource.Find("产品编码", nowitemno);


                if (0 > hadarrenged)
                {
                    MessageBox.Show("充借用产品:" + nowitemno + " 无发货清单安排,请检查发货清单数据...", "注意");
                    return false;

                }

            }

            return true;
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {

            if (null != ly_sales_outbindDataGridView.CurrentRow)
            {

                for (int i = 0; i <= this.ly_sales_outbindBindingSource.Count - 1; i++)
                {
                    this.ly_sales_outbindBindingSource.RemoveAt(i);
                }

                //this.ly_sales_outbindBindingSource.Clear();


                this.ly_sales_outbindDataGridView.EndEdit();
                this.ly_sales_outbindBindingSource.EndEdit();

                this.ly_sales_outbindTableAdapter.Update(this.lYSalseMange.ly_sales_outbind);


            }

         


            
            
            
            
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

       
        private void ly_material_plan_mainDataGridView_SelectionChanged(object sender, EventArgs e)
        {
            if (null == ly_material_plan_mainDataGridView.CurrentRow)
            {

                this.f_PlanExtend_LSPT_sumTableAdapter.Fill(this.lYSalseMange.f_PlanExtend_LSPT_sum, "");
              
                return;
            }

            string nowplannum = ly_material_plan_mainDataGridView.CurrentRow.Cells["计划编号0"].Value.ToString();
            // string nowcontractnum = ly_material_plan_mainDataGridView.CurrentRow.Cells["计划编号"].Value.ToString();


            this.f_PlanExtend_LSPT_sumTableAdapter.Fill(this.lYSalseMange.f_PlanExtend_LSPT_sum, nowplannum);
        }

        private void ly_material_plan_mainDataGridView_DoubleClick(object sender, EventArgs e)
        {
            if (null == ly_material_plan_mainDataGridView.CurrentRow)
            {


                return;
            }

            if ("True" != ly_material_plan_mainDataGridView.CurrentRow.Cells["配套完成0"].Value.ToString())
            {
                MessageBox.Show("依赖书尚未配套完成,不能绑定合同数据...", "注意");
                return;

            }
            string nowplannum = ly_material_plan_mainDataGridView.CurrentRow.Cells["计划编号0"].Value.ToString();
            string nowqingdan = ly_material_plan_mainDataGridView.CurrentRow.Cells["合同编号0"].Value.ToString();

            if (!string.IsNullOrEmpty(nowqingdan))
            {

                MessageBox.Show("依赖书:" + nowplannum + "已和" + nowqingdan + " 绑定...", "注意");
                return;
            }

            
            this.billcode = nowplannum;
            string updstr;

            if ("contract" == this.fromwhere)
            {
                updstr = " update ly_sales_contract_main  " +
                       "  set out_bill_code=  '" + nowplannum + "' where  contract_inner_code='" + this.innercode + "'";
            }
            else
            {

                updstr = " update ly_sales_borrow  " +
                       "  set out_bill_code=  '" + nowplannum + "' where  borrow_code='" + this.innercode + "'";
            }


            SqlConnection sqlConnection1 = new SqlConnection(SQLDatabase.Connectstring);
            SqlCommand cmd = new SqlCommand();

            cmd.CommandText = updstr;
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

                    



                }
                catch (SqlException sqle)
                {

                    temp = 1;
                    MessageBox.Show(sqle.Message.Split('*')[0]);
                }


                finally
                {
                    sqlConnection1.Close();


                }
               
            }
            if (0 == temp)
            {
                this.f_PlanExtend_LSPT_sum1TableAdapter.Fill(this.lYSalseMange.f_PlanExtend_LSPT_sum1, nowplannum, this.fromwhere);
                
                this.ly_sales_contract_detail_sumTableAdapter.Fill(this.lYSalseMange.ly_sales_contract_detail_sum, this.innercode, this.fromwhere);
            }
        }

        private void ly_sales_contract_detail_sumDataGridView_SelectionChanged(object sender, EventArgs e)
        {
            if (null == ly_sales_contract_detail_sumDataGridView.CurrentRow)
            {

             
                return;
            }


            string nowitemno = this.ly_sales_contract_detail_sumDataGridView.CurrentRow.Cells["产品编码qd"].Value.ToString();
            /////////////////////////////////////////////////////////////////////////////////

            dataGridView1.SelectionChanged -= dataGridView1_SelectionChanged;

            this.f_PlanExtend_LSPT_sum1BindingSource.Position = this.f_PlanExtend_LSPT_sum1BindingSource.Find("产品编码", nowitemno);

            dataGridView1.SelectionChanged += dataGridView1_SelectionChanged;
            ///////////////////////////////////////////////////////////////////////////////////////////////////////

            ly_sales_outbindDataGridView.SelectionChanged -= ly_sales_outbindDataGridView_SelectionChanged;

            this.ly_sales_outbindBindingSource.Position = this.ly_sales_outbindBindingSource.Find("成品编码", nowitemno);

            ly_sales_outbindDataGridView.SelectionChanged += ly_sales_outbindDataGridView_SelectionChanged;

            ///////////////////////////////////////////////////////////////////////////////////
            
            this.ly_sales_borrow_detail_clientTableAdapter.Fill(this.lYSalseMange.ly_sales_borrow_detail_client, clientcode, nowitemno);
        }

        private void ly_sales_borrow_detail_clientDataGridView_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            DataGridView dgv = sender as DataGridView;

            if (null == dgv.CurrentRow) return;

            //if ("True" == ly_material_plan_mainDataGridView.CurrentRow.Cells["配套完成0"].Value.ToString())
            //{
            //    MessageBox.Show("依赖书已经技术配套,不能修改数据...", "注意");
            //    return;

            //}

            string borrowitemno = this.ly_sales_borrow_detail_clientDataGridView.CurrentRow.Cells["产品编号jy"].Value.ToString();
            string borrowplannum = this.ly_sales_borrow_detail_clientDataGridView.CurrentRow.Cells["依赖书号jy"].Value.ToString();
            string outitemno = this.ly_sales_borrow_detail_clientDataGridView.CurrentRow.Cells["出库编号jy"].Value.ToString();

            decimal borrowqty;
            if ("" != this.ly_sales_borrow_detail_clientDataGridView.CurrentRow.Cells["未还数量jy"].Value.ToString())
            {
                borrowqty = decimal.Parse(this.ly_sales_borrow_detail_clientDataGridView.CurrentRow.Cells["未还数量jy"].Value.ToString());
            }
            else
            {
                borrowqty = 0;
            }

            if (borrowqty == 0)
            {

                MessageBox.Show("借用已经归还,操作取消...", "注意");
                return;
            }
            ///////////////////////////////////////////////////////////////

            decimal nowbillqty;
            decimal nowplanqty;
            decimal nowjieyongqty;

            decimal oweqty;

            if ("" != ly_sales_contract_detail_sumDataGridView.CurrentRow .Cells["数量qd"].Value.ToString())
            {
                nowbillqty = decimal.Parse(ly_sales_contract_detail_sumDataGridView.CurrentRow.Cells["数量qd"].Value.ToString());
            }
            else
            {
                nowbillqty = 0;
            }

            if ("" != ly_sales_contract_detail_sumDataGridView.CurrentRow.Cells["实发数量qd"].Value.ToString())
            {
                nowplanqty = decimal.Parse(ly_sales_contract_detail_sumDataGridView.CurrentRow.Cells["实发数量qd"].Value.ToString());
            }
            else
            {
                nowplanqty = 0;
            }

            if ("" != ly_sales_contract_detail_sumDataGridView.CurrentRow.Cells["充借数量qd"].Value.ToString())
            {
                nowjieyongqty = decimal.Parse(ly_sales_contract_detail_sumDataGridView.CurrentRow.Cells["充借数量qd"].Value.ToString());
            }
            else
            {
                nowjieyongqty = 0;
            }





            if (nowbillqty == (nowplanqty + nowjieyongqty))
            {
                MessageBox.Show("发货清单产品:" + borrowitemno + " 清单发货数量已全部安排出库,不能充借用出库 ...", "注意");
                return;

            }

            oweqty = nowbillqty - (nowplanqty + nowjieyongqty);

            //////////////////////////////////////////////////////////
            this.ly_sales_outbindDataGridView.SelectionChanged -= ly_sales_outbindDataGridView_SelectionChanged;
            this.ly_sales_outbindBindingSource.AddNew();
            this.ly_sales_outbindDataGridView.SelectionChanged += ly_sales_outbindDataGridView_SelectionChanged;
           

            this.ly_sales_outbindDataGridView.CurrentRow.Cells["发货清单fh"].Value = this .innercode ;
            this.ly_sales_outbindDataGridView.CurrentRow.Cells["依赖书号fh"].Value = borrowplannum;
            this.ly_sales_outbindDataGridView.CurrentRow.Cells["成品编码fh"].Value = borrowitemno;
            this.ly_sales_outbindDataGridView.CurrentRow.Cells["出库编码fh"].Value = outitemno;

            if (oweqty >= borrowqty)
            {
                this.ly_sales_outbindDataGridView.CurrentRow.Cells["数量fh"].Value = oweqty;
            }
            else
            {
                this.ly_sales_outbindDataGridView.CurrentRow.Cells["数量fh"].Value = borrowqty;
            }

            this.ly_sales_outbindDataGridView.EndEdit();
            this.ly_sales_outbindBindingSource.EndEdit();

            this.ly_sales_outbindTableAdapter.Update(this.lYSalseMange.ly_sales_outbind);

            ly_sales_contract_detail_sumDataGridView.SelectionChanged -= ly_sales_contract_detail_sumDataGridView_SelectionChanged;

           
            this.ly_sales_outbindTableAdapter.Fill(this.lYSalseMange.ly_sales_outbind, this.innercode);
            this.ly_sales_outbindBindingSource.Position = this.ly_sales_outbindBindingSource.Find("成品编码", borrowitemno);


            ly_sales_contract_detail_sumDataGridView.SelectionChanged += ly_sales_contract_detail_sumDataGridView_SelectionChanged;

           
            this.ly_sales_contract_detail_sumTableAdapter.Fill(this.lYSalseMange.ly_sales_contract_detail_sum, this.innercode, this.fromwhere);
            this.ly_sales_contract_detail_sumBindingSource.Position = this.ly_sales_contract_detail_sumBindingSource.Find("产品编码", borrowitemno);

            this.ly_sales_borrow_detail_clientTableAdapter.Fill(this.lYSalseMange.ly_sales_borrow_detail_client, clientcode, borrowitemno);
           

        }

        private void 删除当前借用条目ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (null == ly_sales_outbindDataGridView.CurrentRow)
            {

                return;
            }

            //if ("True" == ly_material_plan_mainDataGridView.CurrentRow.Cells["配套完成0"].Value.ToString())
            //{
            //    MessageBox.Show("依赖书已经技术配套,不能删除数据...", "注意");
            //    return;

            //}

            //string salespeople = this.ly_sales_groupDataGridView.CurrentRow.Cells["录入人5"].Value.ToString();

            //if (salespeople != SQLDatabase.nowUserName())
            //{
            //    MessageBox.Show("请录入人:" + salespeople + "删除", "注意");
            //    return;
            //}

            //if ("True" == ly_sales_contract_mainDataGridView.CurrentRow.Cells["提交"].Value.ToString())
            //{
            //    MessageBox.Show("合同已经提交,不能删除数据...", "注意");
            //    return;

            //}

            //if ("True" == ly_sales_contract_mainDataGridView.CurrentRow.Cells["批准"].Value.ToString())
            //{
            //    MessageBox.Show("合同已经执行,不能删除数据...", "注意");
            //    return;

            //}

            //if ("True" == ly_sales_contract_mainDataGridView.CurrentRow.Cells["审核"].Value.ToString())
            //{
            //    MessageBox.Show("合同已经审批,不能删除数据...", "注意");
            //    return;

            //}


            string message = "确定删除当前配套吗？";
            string caption = "提示...";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result;



            result = MessageBox.Show(message, caption, buttons,
            MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

            if (result == DialogResult.Yes)
            {




                this.ly_sales_outbindBindingSource.RemoveCurrent();


                this.ly_sales_outbindDataGridView.EndEdit();
                this.ly_sales_outbindBindingSource.EndEdit();

                this.ly_sales_outbindTableAdapter.Update(this.lYSalseMange.ly_sales_outbind);

                //this.ly_sales_outbindTableAdapter.Fill(this.lYSalseMange.ly_sales_outbind, this.innercode);

                string nowitemno = this.ly_sales_contract_detail_sumDataGridView.CurrentRow.Cells["产品编码qd"].Value.ToString();

                this.ly_sales_contract_detail_sumTableAdapter.Fill(this.lYSalseMange.ly_sales_contract_detail_sum, this.innercode, this.fromwhere);
                this.ly_sales_contract_detail_sumBindingSource.Position = this.ly_sales_contract_detail_sumBindingSource.Find("产品编码", nowitemno);

                this.ly_sales_borrow_detail_clientTableAdapter.Fill(this.lYSalseMange.ly_sales_borrow_detail_client, clientcode, nowitemno);


              
            }
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (null == this.dataGridView1.CurrentRow) return;
            
            string nowitemno = this.dataGridView1.CurrentRow.Cells["产品编码yl"].Value.ToString();
            /////////////////////////////////////////////////////////////////////////////////

            ly_sales_contract_detail_sumDataGridView.SelectionChanged -= ly_sales_contract_detail_sumDataGridView_SelectionChanged;

            this.ly_sales_contract_detail_sumBindingSource.Position = this.ly_sales_contract_detail_sumBindingSource.Find("产品编码", nowitemno);

            ly_sales_contract_detail_sumDataGridView.SelectionChanged += ly_sales_contract_detail_sumDataGridView_SelectionChanged;
            ///////////////////////////////////////////////////////////////////////////////////////////////////////

            ly_sales_outbindDataGridView.SelectionChanged -= ly_sales_outbindDataGridView_SelectionChanged;

            this.ly_sales_outbindBindingSource.Position = this.ly_sales_outbindBindingSource.Find("成品编码", nowitemno);

            ly_sales_outbindDataGridView.SelectionChanged += ly_sales_outbindDataGridView_SelectionChanged;


            this.ly_sales_borrow_detail_clientTableAdapter.Fill(this.lYSalseMange.ly_sales_borrow_detail_client, clientcode, nowitemno);
        }

        private void ly_sales_outbindDataGridView_SelectionChanged(object sender, EventArgs e)
        {

            if (null == this.ly_sales_outbindDataGridView.CurrentRow) return;
            string nowitemno = this.ly_sales_outbindDataGridView.CurrentRow.Cells["成品编码fh"].Value.ToString();
            /////////////////////////////////////////////////////////////////////////////////

            dataGridView1.SelectionChanged -= dataGridView1_SelectionChanged;

            this.f_PlanExtend_LSPT_sum1BindingSource.Position = this.f_PlanExtend_LSPT_sum1BindingSource.Find("产品编码", nowitemno);

            dataGridView1.SelectionChanged += dataGridView1_SelectionChanged;
            ///////////////////////////////////////////////////////////////////////////////////////////////////////

            ly_sales_contract_detail_sumDataGridView.SelectionChanged -= ly_sales_contract_detail_sumDataGridView_SelectionChanged;

            this.ly_sales_contract_detail_sumBindingSource.Position = this.ly_sales_contract_detail_sumBindingSource.Find("产品编码", nowitemno);

            ly_sales_contract_detail_sumDataGridView.SelectionChanged += ly_sales_contract_detail_sumDataGridView_SelectionChanged;

            this.ly_sales_borrow_detail_clientTableAdapter.Fill(this.lYSalseMange.ly_sales_borrow_detail_client, clientcode, nowitemno);


        }

        private void ly_sales_outbindDataGridView_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            DataGridView dgv = sender as DataGridView;

            if (null == dgv.CurrentRow) return;



            if ("数量fh" == dgv.CurrentCell.OwningColumn.Name)
            {

                ChangeValue queryForm = new ChangeValue();

                queryForm.OldValue = dgv.CurrentCell.Value.ToString();
                queryForm.NewValue = "";
                queryForm.ChangeMode = "value";
                queryForm.ShowDialog();

                string borrowitemno = dgv.CurrentRow.Cells["成品编码fh"].Value.ToString();

                decimal oldnum ;

                if ("" != dgv.CurrentCell.Value.ToString())
                {
                    oldnum = decimal.Parse(dgv.CurrentCell.Value.ToString());
                }
                else 
                {
                    oldnum = 0;
                }

                decimal borrowqty;
                if ("" != this.ly_sales_borrow_detail_clientDataGridView.CurrentRow.Cells["未还数量jy"].Value.ToString())
                {
                    borrowqty = decimal.Parse(this.ly_sales_borrow_detail_clientDataGridView.CurrentRow.Cells["未还数量jy"].Value.ToString());
                }
                else
                {
                    borrowqty = 0;
                }
               

                if (queryForm.NewValue != "")
                {
                    decimal newnum = decimal.Parse(queryForm.NewValue);


                    if (newnum > borrowqty +oldnum )
                    {
                        MessageBox.Show("数量超出未还数量,操作取消...");

                    }
                    else 
                    
                    {
                        dgv.CurrentRow.Cells["数量fh"].Value = queryForm.NewValue;
                        //dgv.CurrentRow.Cells["discount_money"].Value = DBNull.Value;

                        //dgv.CurrentRow.Cells["approve_flag"].Value = "False";
                        this.ly_sales_outbindDataGridView.EndEdit();
                        this.ly_sales_outbindBindingSource.EndEdit();

                        this.ly_sales_outbindTableAdapter.Update(this.lYSalseMange.ly_sales_outbind);

                        ly_sales_contract_detail_sumDataGridView.SelectionChanged -= ly_sales_contract_detail_sumDataGridView_SelectionChanged;


                        this.ly_sales_outbindTableAdapter.Fill(this.lYSalseMange.ly_sales_outbind, this.innercode);
                        this.ly_sales_outbindBindingSource.Position = this.ly_sales_outbindBindingSource.Find("成品编码", borrowitemno);


                        ly_sales_contract_detail_sumDataGridView.SelectionChanged += ly_sales_contract_detail_sumDataGridView_SelectionChanged;


                        this.ly_sales_contract_detail_sumTableAdapter.Fill(this.lYSalseMange.ly_sales_contract_detail_sum, this.innercode, this.fromwhere);
                        this.ly_sales_contract_detail_sumBindingSource.Position = this.ly_sales_contract_detail_sumBindingSource.Find("产品编码", borrowitemno);

                        this.ly_sales_borrow_detail_clientTableAdapter.Fill(this.lYSalseMange.ly_sales_borrow_detail_client, clientcode, borrowitemno);
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

          
          

            //////////////////////////////////////////////////////////
          

            
        }

       
      

        

      

       
    }
}
