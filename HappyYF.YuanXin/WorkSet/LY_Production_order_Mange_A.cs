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
    public partial class LY_Production_order_Mange_A : Form
    {
        private LY_Machine ownerForm;


        int nowplanid= 0;

        string nowitemno = "";

        string nowproduction_order = "";

        int nowprocess_order = 0;

        //int detail_Id=0;

        public LY_Machine OwnerForm
        {
            get { return ownerForm; }
            set { ownerForm = value; }
        }

        public LY_Production_order_Mange_A()
        {
            InitializeComponent();
        }

        private void LY_Production_order_Mange_Load(object sender, EventArgs e)
        {

            //this.Location = this.ownerForm.PointToScreen(new Point(this.ownerForm.Location .X+10 ,this .ownerForm .Location .Y+5 ));
            //this .Width =this .ownerForm.Width-20 ;
            //this .Height =360;

            this.WindowState = FormWindowState.Maximized;
            
            this.dateTimePicker1.Text = DateTime.Today.AddMonths(-2).Date.ToString();
            this.dateTimePicker2.Text = DateTime.Today.AddDays(1).Date.ToString();

            this.dateTimePicker3.Text = DateTime.Today.AddMonths(-2).Date.ToString();
            this.dateTimePicker4.Text = DateTime.Today.AddDays(1).Date.ToString();

            this.ly_machinepart_process_workTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;

            this.ly_production_order_detail1TableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;

            this.ly_production_order_materialrequisitionTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;

            this.ly_production_recordsTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            this.lY_productionorder_listTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;

            this.lY_productionorder_listTableAdapter.Fill(this.lYQualityInspector.LY_productionorder_list, DateTime.Parse(this.dateTimePicker1.Text).Date, DateTime.Parse(this.dateTimePicker2.Text).Date);


           

        }

       


        private void toolStripTextBox2_Enter(object sender, EventArgs e)
        {
            toolStripTextBox2.Text = "";

            this.lY_productionorder_listBindingSource.Filter = "";
        }

        private void toolStripTextBox2_KeyUp(object sender, KeyEventArgs e)
        {
            string dFilter = "";

            dFilter = GetDataGridviewMultiFilterString.DGVMultiFilterString(this.lY_productionorder_listDataGridView, this.toolStripTextBox2.Text);

            if (null == dFilter)
                dFilter = "";

            this.lY_productionorder_listBindingSource.Filter = dFilter;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.lY_productionorder_listTableAdapter.Fill(this.lYQualityInspector.LY_productionorder_list, DateTime.Parse(this.dateTimePicker1.Text).Date, DateTime.Parse(this.dateTimePicker2.Text).Date);
        }

        private void lY_productionorder_listDataGridView_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (null == this.lY_productionorder_listDataGridView.CurrentRow)
            {
               

                    this.ly_machinepart_process_workTableAdapter.Fill(this.lYMaterielRequirements.ly_machinepart_process_work, "", "");

                    this.ly_production_order_detail1TableAdapter.Fill(this.lYMaterielRequirements.ly_production_order_detail1, "", 0);

                    this.ly_production_order_materialrequisitionTableAdapter.Fill(this.lYMaterielRequirements.ly_production_order_materialrequisition, "");
                    
                return;
               
            
            }
            
             nowplanid = int.Parse(this.lY_productionorder_listDataGridView.Rows[e.RowIndex].Cells["plan_id"].Value.ToString());

             nowitemno = this.lY_productionorder_listDataGridView.Rows[e.RowIndex].Cells["物料编码"].Value.ToString();

             nowproduction_order = this.lY_productionorder_listDataGridView.Rows[e.RowIndex].Cells["跟单编号"].Value.ToString();

            //this.OwnerForm.find_NowProduc(nowplanid, nowitemno, nowproduction_order);

            ////////////////////////////////////////////





           this.ly_machinepart_process_workTableAdapter.Fill(this.lYMaterielRequirements.ly_machinepart_process_work, nowitemno, nowproduction_order);

           this.ly_production_order_detail1TableAdapter.Fill(this.lYMaterielRequirements.ly_production_order_detail1, nowproduction_order, 0);

           this.ly_production_order_materialrequisitionTableAdapter.Fill(this.lYMaterielRequirements.ly_production_order_materialrequisition, nowproduction_order);
               //set_processOrder_Num();


          
       


        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.ly_production_recordsTableAdapter.Fill(this.lYQualityInspector.ly_production_records, DateTime.Parse(this.dateTimePicker1.Text).Date, DateTime.Parse(this.dateTimePicker2.Text).Date,"xxx");

            

            if ( this .checkBox1.Checked   )
            {

                this.ly_production_recordsBindingSource.Filter = "";
               
            }
            else
            {

                this.ly_production_recordsBindingSource.Filter = "处理=0";
            }
        }

        private void toolStripTextBox4_KeyUp(object sender, KeyEventArgs e)
        {
            string dFilter = "";

            dFilter = GetDataGridviewMultiFilterString.DGVMultiFilterString(this.ly_production_recordsDataGridView, this.toolStripTextBox4.Text);

            if (null == dFilter)
                dFilter = "";

            this.ly_production_recordsBindingSource.Filter = dFilter;
        }

        private void toolStripTextBox4_Enter(object sender, EventArgs e)
        {
            toolStripTextBox4.Text = "";

            this.ly_production_recordsBindingSource.Filter = "";
        }

        private void ly_production_recordsDataGridView_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            
            
            if (null == this.ly_production_recordsDataGridView.CurrentRow)
            {

                this.ly_machinepart_process_workTableAdapter.Fill(this.lYMaterielRequirements.ly_machinepart_process_work, "", "");

                this.ly_production_order_detail1TableAdapter.Fill(this.lYMaterielRequirements.ly_production_order_detail1, "", 0);

                this.ly_production_order_materialrequisitionTableAdapter.Fill(this.lYMaterielRequirements.ly_production_order_materialrequisition, "");
                return;
            }

             nowplanid = int.Parse(this.ly_production_recordsDataGridView.Rows[e.RowIndex].Cells["plan_id2"].Value.ToString());

             nowitemno = this.ly_production_recordsDataGridView.Rows[e.RowIndex].Cells["物料编号2"].Value.ToString();

             nowproduction_order = this.ly_production_recordsDataGridView.Rows[e.RowIndex].Cells["跟单编号2"].Value.ToString();

             nowprocess_order = 0;

            if (!string .IsNullOrEmpty ( this.ly_production_recordsDataGridView.Rows[e.RowIndex].Cells["工序号"].Value.ToString ()))
            {
                 nowprocess_order = int.Parse(this.ly_production_recordsDataGridView.Rows[e.RowIndex].Cells["工序号"].Value.ToString());
            }

            this.OwnerForm.find_NowProduc(nowplanid, nowitemno, nowproduction_order, nowprocess_order);

            ///////////////////////////////////////////////

            this.ly_machinepart_process_workTableAdapter.Fill(this.lYMaterielRequirements.ly_machinepart_process_work, nowitemno, nowproduction_order);
            this.ly_machinepart_process_workBindingSource.Position = this.ly_machinepart_process_workBindingSource.Find("工序编号", nowprocess_order);

            this.ly_production_order_detail1TableAdapter.Fill(this.lYMaterielRequirements.ly_production_order_detail1, nowproduction_order, 0);

            this.ly_production_order_materialrequisitionTableAdapter.Fill(this.lYMaterielRequirements.ly_production_order_materialrequisition, nowproduction_order);
        }

        private void ly_production_recordsDataGridView_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            DataGridView dgv = sender as DataGridView;

            /////////////////////////////

            if ("处理2" == dgv.CurrentCell.OwningColumn.Name)
            {
                //if (!SQLDatabase.CheckHaveRight(SQLDatabase.NowUserID, "跟单完成设置"))
                //{
                //    MessageBox.Show("无跟单完成设置权限,操作取消...", "注意");
                //    return;

                //}

                if ("True" == dgv.CurrentRow.Cells["处理2"].Value.ToString())
                {
                    dgv.CurrentRow.Cells["处理2"].Value = "False";

                }
                else
                {

                    dgv.CurrentRow.Cells["处理2"].Value = "True";
                }


                this.ly_production_recordsDataGridView.EndEdit();
                this.ly_production_recordsBindingSource.EndEdit();

                this.ly_production_recordsTableAdapter.Update(this.lYQualityInspector.ly_production_records);

               
                if (this.checkBox1.Checked)
                {

                    this.ly_production_recordsBindingSource.Filter = "";

                }
                else
                {
                    this.ly_production_recordsBindingSource.Filter = "";
                    this.ly_production_recordsBindingSource.Filter = "处理=0";
                    this.ly_production_recordsBindingSource.Position = 1;
                    this.ly_production_recordsBindingSource.Position = 0;
                }
                return;
            }

 
        }

       



        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            TabControl tc = sender as TabControl; 

            if (this.tabPage1 == tc.SelectedTab)
            {


                if (null == this.lY_productionorder_listDataGridView.CurrentRow)
                {


                    this.ly_machinepart_process_workTableAdapter.Fill(this.lYMaterielRequirements.ly_machinepart_process_work, "", "");

                    this.ly_production_order_detail1TableAdapter.Fill(this.lYMaterielRequirements.ly_production_order_detail1, "", 0);

                    this.ly_production_order_materialrequisitionTableAdapter.Fill(this.lYMaterielRequirements.ly_production_order_materialrequisition, "");

                    return;


                }

                 nowplanid = int.Parse(this.lY_productionorder_listDataGridView.CurrentRow.Cells["plan_id"].Value.ToString());

                 nowitemno = this.lY_productionorder_listDataGridView.CurrentRow .Cells["物料编码"].Value.ToString();

                 nowproduction_order = this.lY_productionorder_listDataGridView.CurrentRow.Cells["跟单编号"].Value.ToString();

                //this.OwnerForm.find_NowProduc(nowplanid, nowitemno, nowproduction_order); 

                this.ly_machinepart_process_workTableAdapter.Fill(this.lYMaterielRequirements.ly_machinepart_process_work, nowitemno, nowproduction_order);

                this.ly_production_order_detail1TableAdapter.Fill(this.lYMaterielRequirements.ly_production_order_detail1, nowproduction_order, 0);

                this.ly_production_order_materialrequisitionTableAdapter.Fill(this.lYMaterielRequirements.ly_production_order_materialrequisition, nowproduction_order);
        
            }

            if (this.tabPage2 == tc.SelectedTab)
            {
                if (null == this.ly_production_recordsDataGridView.CurrentRow)
                {

                    this.ly_machinepart_process_workTableAdapter.Fill(this.lYMaterielRequirements.ly_machinepart_process_work, "", "");

                    this.ly_production_order_detail1TableAdapter.Fill(this.lYMaterielRequirements.ly_production_order_detail1, "", 0);

                    this.ly_production_order_materialrequisitionTableAdapter.Fill(this.lYMaterielRequirements.ly_production_order_materialrequisition, "");
                    return;
                }

                 nowplanid = int.Parse(this.ly_production_recordsDataGridView.CurrentRow.Cells["plan_id2"].Value.ToString());

                 nowitemno = this.ly_production_recordsDataGridView.CurrentRow.Cells["物料编号2"].Value.ToString();

                 nowproduction_order = this.ly_production_recordsDataGridView.CurrentRow.Cells["跟单编号2"].Value.ToString();

                 nowprocess_order = 0;

                if (!string.IsNullOrEmpty(this.ly_production_recordsDataGridView.CurrentRow.Cells["工序号"].Value.ToString()))
                {
                    nowprocess_order = int.Parse(this.ly_production_recordsDataGridView.CurrentRow.Cells["工序号"].Value.ToString());
                }

               // this.OwnerForm.find_NowProduc(nowplanid, nowitemno, nowproduction_order, nowprocess_order);
                

                this.ly_machinepart_process_workTableAdapter.Fill(this.lYMaterielRequirements.ly_machinepart_process_work, nowitemno, nowproduction_order);
                this.ly_machinepart_process_workBindingSource.Position = this.ly_machinepart_process_workBindingSource.Find("工序编号", nowprocess_order);

                this.ly_production_order_detail1TableAdapter.Fill(this.lYMaterielRequirements.ly_production_order_detail1, nowproduction_order, 0);

                this.ly_production_order_materialrequisitionTableAdapter.Fill(this.lYMaterielRequirements.ly_production_order_materialrequisition, nowproduction_order);

               
            }
        }
    
        private void ly_machinepart_process_workDataGridView_SelectionChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(nowproduction_order) && null != this.ly_machinepart_process_workDataGridView.CurrentRow)
            {

                int nowOrder;
            

                if ("" != this.ly_machinepart_process_workDataGridView.CurrentRow.Cells["工序编号"].Value.ToString())
                {
                    nowOrder = int.Parse(this.ly_machinepart_process_workDataGridView.CurrentRow.Cells["工序编号"].Value.ToString());
                }
                else
                {
                    nowOrder = 0;
                }



                this.ly_production_order_detailTableAdapter.Fill(this.lYMaterielRequirements.ly_production_order_detail, nowproduction_order, nowOrder);


            }
            else
            {
                this.ly_production_order_detailTableAdapter.Fill(this.lYMaterielRequirements.ly_production_order_detail, "", 0);
            }
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            //string nowproductionorder = this.ly_production_orderDataGridView.CurrentRow.Cells["跟单编号"].Value.ToString();
            string noworder = this.ly_machinepart_process_workDataGridView.CurrentRow.Cells["工序名称"].Value.ToString();




            string message1 = "确认分配(跟单：" + nowproduction_order + " 工序:" + noworder + ")任务吗?";
            string caption1 = "提示...";
            MessageBoxButtons buttons1 = MessageBoxButtons.YesNo;

            DialogResult result1;



            result1 = MessageBox.Show(message1, caption1, buttons1,
            MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

            if (result1 == DialogResult.Yes)
            {

                ///////////////////////////////////////////////////////////////// 

                decimal up_order_count;
                decimal order_count;

                decimal up_quality;
                decimal up_canuse;
                decimal up_remake;

                int nowrowIndex = this.ly_machinepart_process_workDataGridView.CurrentRow.Index;
                string nowordername = this.ly_machinepart_process_workDataGridView.CurrentRow.Cells["工序名称"].Value.ToString();

                int nowordernum = int.Parse(this.ly_machinepart_process_workDataGridView.CurrentRow.Cells["工序编号"].Value.ToString());


                if (1 != nowordernum)

                //if ("下料" != nowordername)
                {
                    if (!string.IsNullOrEmpty(this.ly_machinepart_process_workDataGridView.Rows[nowrowIndex - 1].Cells["本序合格"].Value.ToString()))
                    {


                        up_quality = decimal.Parse(this.ly_machinepart_process_workDataGridView.Rows[nowrowIndex - 1].Cells["本序合格"].Value.ToString());
                    }
                    else
                    {

                        up_quality = 0;
                    }

                    if (!string.IsNullOrEmpty(this.ly_machinepart_process_workDataGridView.Rows[nowrowIndex - 1].Cells["本序可用数量"].Value.ToString()))
                    {


                        up_canuse = decimal.Parse(this.ly_machinepart_process_workDataGridView.Rows[nowrowIndex - 1].Cells["本序可用数量"].Value.ToString());
                    }
                    else
                    {

                        up_canuse = 0;
                    }
                    ////////////////////////

                    if (!string.IsNullOrEmpty(this.ly_machinepart_process_workDataGridView.Rows[nowrowIndex - 1].Cells["本序返修合格"].Value.ToString()))
                    {


                        up_remake = decimal.Parse(this.ly_machinepart_process_workDataGridView.Rows[nowrowIndex - 1].Cells["本序返修合格"].Value.ToString());
                    }
                    else
                    {

                        up_remake = 0;
                    }


                    up_order_count = up_quality + up_canuse + up_remake;


                    //if (!string.IsNullOrEmpty(this.ly_machinepart_process_workDataGridView.Rows[nowrowIndex - 1].Cells["本序数量"].Value.ToString()))
                    //{


                    //    up_order_count = decimal.Parse(this.ly_machinepart_process_workDataGridView.Rows[nowrowIndex - 1].Cells["本序数量"].Value.ToString());
                    //}
                    //else
                    //{

                    //    up_order_count = 0;
                    //}
                }
                else
                {
                    if (!string.IsNullOrEmpty(this.ly_machinepart_process_workDataGridView.CurrentRow.Cells["跟单数量"].Value.ToString()))
                    {
                        up_order_count = decimal.Parse(this.ly_machinepart_process_workDataGridView.CurrentRow.Cells["跟单数量"].Value.ToString());
                    }
                    else
                    {
                        up_order_count = 0;
                    }

                }


                if (!string.IsNullOrEmpty(this.ly_machinepart_process_workDataGridView.CurrentRow.Cells["本序数量"].Value.ToString()))
                {
                    order_count = decimal.Parse(this.ly_machinepart_process_workDataGridView.CurrentRow.Cells["本序数量"].Value.ToString());
                }
                else
                {
                    order_count = 0;
                }



                decimal arrange_count = 0;

                if (!string .IsNullOrEmpty (nowproduction_order))
                {

                    foreach (DataGridViewRow dgr in ly_production_order_detailDataGridView.Rows)
                    {

                        if (string.IsNullOrEmpty(dgr.Cells["数量1"].Value.ToString())) continue;
                        arrange_count = arrange_count + decimal.Parse(dgr.Cells["数量1"].Value.ToString());



                    }
                }



                if (arrange_count >= up_order_count)
                {
                    MessageBox.Show("跟单数量已经全部安排,不能增加跟单任务", "注意");

                    return;
                }
                else
                {



                    ///////////////////////////////////////////////////////////////////








                    this.ly_production_order_detailBindingSource.AddNew();

                    this.ly_production_order_detailDataGridView.CurrentRow.Cells["跟单编号1"].Value = nowproduction_order;

                    this.ly_production_order_detailDataGridView.CurrentRow.Cells["工序1"].Value = this.ly_machinepart_process_workDataGridView.CurrentRow.Cells["工序编号"].Value;

                    this.ly_production_order_detailDataGridView.CurrentRow.Cells["sequence_number1"].Value = this.ly_machinepart_process_workDataGridView.CurrentRow.Cells["sequence_number"].Value;


                    this.ly_production_order_detailDataGridView.CurrentRow.Cells["pruductionOrder_count1"].Value = this.ly_machinepart_process_workDataGridView.CurrentRow.Cells["跟单数量"].Value.ToString();

                    this.ly_production_order_detailDataGridView.CurrentRow.Cells["准终1"].Value = this.ly_machinepart_process_workDataGridView.CurrentRow.Cells["准终"].Value;

                    this.ly_production_order_detailDataGridView.CurrentRow.Cells["实际准终1"].Value = this.ly_machinepart_process_workDataGridView.CurrentRow.Cells["准终"].Value;

                    this.ly_production_order_detailDataGridView.CurrentRow.Cells["单件工时1"].Value = this.ly_machinepart_process_workDataGridView.CurrentRow.Cells["单件工时"].Value;
                    this.ly_production_order_detailDataGridView.CurrentRow.Cells["实际单件工时1"].Value = this.ly_machinepart_process_workDataGridView.CurrentRow.Cells["单件工时"].Value;

                    this.ly_production_order_detailDataGridView.CurrentRow.Cells["准终累加1"].Value = this.ly_machinepart_process_workDataGridView.CurrentRow.Cells["准终累加"].Value;
                    this.ly_production_order_detailDataGridView.CurrentRow.Cells["单件工时累加1"].Value = this.ly_machinepart_process_workDataGridView.CurrentRow.Cells["工时累加"].Value;

                    this.ly_production_order_detailDataGridView.CurrentRow.Cells["工时单价1"].Value = this.ly_machinepart_process_workDataGridView.CurrentRow.Cells["工时单价"].Value;

                    //this.ly_production_order_detailDataGridView.CurrentRow.Cells["材料单价1"].Value = this.ly_production_orderDataGridView.CurrentRow.Cells["物料单价"].Value;


                    this.ly_production_order_detailDataGridView.CurrentRow.Cells["数量1"].Value = up_order_count - arrange_count;

                    this.ly_production_order_detailDataGridView.CurrentRow.Cells["调度1"].Value = SQLDatabase.nowUserName();

                    this.ly_production_order_detailDataGridView.CurrentRow.Cells["排序日期"].Value = SQLDatabase.GetNowdate();
                    this.ly_production_order_detailDataGridView.CurrentRow.Cells["外协"].Value = "False";


                    SaveTask();
                }

            }
        }

        private void SaveTask()
        {

            this.ly_production_order_detailDataGridView.EndEdit();

            this.Validate();
            this.ly_production_order_detailBindingSource.EndEdit();

            this.ly_production_order_detailTableAdapter.Update(this.lYMaterielRequirements.ly_production_order_detail);
            ////////////////////////////////////////////////

            if (!string.IsNullOrEmpty(nowproduction_order) && null != this.ly_machinepart_process_workDataGridView.CurrentRow)
            {
                int detail_Id;

                if (null != this.ly_production_order_detailDataGridView.CurrentRow)
                {
                    detail_Id = int.Parse(this.ly_production_order_detailDataGridView.CurrentRow.Cells["detailId"].Value.ToString());
                }
                else
                {
                    detail_Id = 0;
                }


                int nowOrder;
                //string nowproductionorderNum = this.ly_production_orderDataGridView.CurrentRow.Cells["跟单编号"].Value.ToString();

                if ("" != this.ly_machinepart_process_workDataGridView.CurrentRow.Cells["工序编号"].Value.ToString())
                {
                    nowOrder = int.Parse(this.ly_machinepart_process_workDataGridView.CurrentRow.Cells["工序编号"].Value.ToString());
                }
                else
                {
                    nowOrder = 0;
                }
                ////////////////////////////////////////////////////////////


                //string nowitem = this.ly_production_orderDataGridView.CurrentRow.Cells["物料编码"].Value.ToString();


                this.ly_machinepart_process_workTableAdapter.Fill(this.lYMaterielRequirements.ly_machinepart_process_work, nowitemno, nowproduction_order);

                this.ly_machinepart_process_workBindingSource.Position = this.ly_machinepart_process_workBindingSource.Find("工序编号", nowOrder);
                this.ly_production_order_detailBindingSource.Position = this.ly_production_order_detailBindingSource.Find("id", detail_Id);



                //this.ly_production_order_detailTableAdapter.Fill(this.lYMaterielRequirements.ly_production_order_detail, nowproductionorderNum, nowOrder);
                /////////////////////////////////////////////////////

            }
            else
            {
                this.ly_production_order_detailTableAdapter.Fill(this.lYMaterielRequirements.ly_production_order_detail, "", 0);
            }

        }

        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            if (null == this.ly_production_order_detailDataGridView.CurrentRow) return;

            //if (this.ly_material_plan_detailDataGridView.RowCount > 0)
            //{
            //    MessageBox.Show("计划已有物料记录，不能删除(实需删除，请先删除该计划的物料记录)", "注意");
            //    return;

            //}

            if (!checkqualityRec())
            {

                MessageBox.Show("任务已有检验记录,不能修改, 操作取消", "注意");

                return;

            }

            string diaodu = this.ly_production_order_detailDataGridView.CurrentRow.Cells["调度1"].Value.ToString();

            if (diaodu != SQLDatabase.nowUserName())
            {
                MessageBox.Show("请调度:" + diaodu + "删除", "注意");
                return;
            }


            string noworder = this.ly_machinepart_process_workDataGridView.CurrentRow.Cells["工序名称"].Value.ToString();
            string nowName = this.ly_production_order_detailDataGridView.CurrentRow.Cells["姓名1"].Value.ToString();



            string message1 = "删除" + nowName + " (工序：" + noworder + " )任务安排，继续吗？";
            string caption1 = "提示...";
            MessageBoxButtons buttons1 = MessageBoxButtons.YesNo;

            DialogResult result1;



            result1 = MessageBox.Show(message1, caption1, buttons1,
            MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

            if (result1 == DialogResult.Yes)
            {

                this.ly_production_order_detailBindingSource.RemoveCurrent();


                SaveTask();




            }
        }

        private bool checkqualityRec()
        {
            decimal qualitied_count = 0;
            decimal waste_count = 0;



            if (!string.IsNullOrEmpty(this.ly_production_order_detailDataGridView.CurrentRow.Cells["合格1"].Value.ToString()))
            {
                qualitied_count = decimal.Parse(this.ly_production_order_detailDataGridView.CurrentRow.Cells["合格1"].Value.ToString());
            }
            else
            {
                qualitied_count = 0;
            }

            if (!string.IsNullOrEmpty(this.ly_production_order_detailDataGridView.CurrentRow.Cells["废品1"].Value.ToString()))
            {
                waste_count = decimal.Parse(this.ly_production_order_detailDataGridView.CurrentRow.Cells["废品1"].Value.ToString());
            }
            else
            {
                waste_count = 0;
            }


            if ((qualitied_count + waste_count) > 0)
            {
                string nowordername = this.ly_machinepart_process_workDataGridView.CurrentRow.Cells["工序名称"].Value.ToString();

                if ("下料" == nowordername)
                {
                    return false;
                }
                else
                {
                    return false;
                }

            }
            else
            {

                return true;
            }

        }

        private void toolStripButton7_Click(object sender, EventArgs e)
        {
            //string nowproductionorder = this.ly_production_orderDataGridView.CurrentRow.Cells["跟单编号"].Value.ToString();
            string noworder = this.ly_machinepart_process_workDataGridView.CurrentRow.Cells["工序名称"].Value.ToString();




            string message1 = "确认分配(跟单：" + nowproduction_order + " 工序:" + noworder + ")外协任务吗?";
            string caption1 = "提示...";
            MessageBoxButtons buttons1 = MessageBoxButtons.YesNo;

            DialogResult result1;



            result1 = MessageBox.Show(message1, caption1, buttons1,
            MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

            if (result1 == DialogResult.Yes)
            {

                /////////////////////////////////////////////////////////////////

                ///////////////////////////////////////////////////////////////// 

                decimal up_order_count;
                decimal order_count;

                decimal up_quality;
                decimal up_canuse;
                decimal up_remake;

                int nowrowIndex = this.ly_machinepart_process_workDataGridView.CurrentRow.Index;
                string nowordername = this.ly_machinepart_process_workDataGridView.CurrentRow.Cells["工序名称"].Value.ToString();

                int nowordernum = int.Parse(this.ly_machinepart_process_workDataGridView.CurrentRow.Cells["工序编号"].Value.ToString());


                if (1 != nowordernum)


                //if ("下料" != nowordername)
                {
                    if (!string.IsNullOrEmpty(this.ly_machinepart_process_workDataGridView.Rows[nowrowIndex - 1].Cells["本序合格"].Value.ToString()))
                    {


                        up_quality = decimal.Parse(this.ly_machinepart_process_workDataGridView.Rows[nowrowIndex - 1].Cells["本序合格"].Value.ToString());
                    }
                    else
                    {

                        up_quality = 0;
                    }

                    if (!string.IsNullOrEmpty(this.ly_machinepart_process_workDataGridView.Rows[nowrowIndex - 1].Cells["本序可用数量"].Value.ToString()))
                    {


                        up_canuse = decimal.Parse(this.ly_machinepart_process_workDataGridView.Rows[nowrowIndex - 1].Cells["本序可用数量"].Value.ToString());
                    }
                    else
                    {

                        up_canuse = 0;
                    }
                    ////////////////////////

                    if (!string.IsNullOrEmpty(this.ly_machinepart_process_workDataGridView.Rows[nowrowIndex - 1].Cells["本序返修合格"].Value.ToString()))
                    {


                        up_remake = decimal.Parse(this.ly_machinepart_process_workDataGridView.Rows[nowrowIndex - 1].Cells["本序返修合格"].Value.ToString());
                    }
                    else
                    {

                        up_remake = 0;
                    }


                    up_order_count = up_quality + up_canuse + up_remake;




                    //if (!string.IsNullOrEmpty(this.ly_machinepart_process_workDataGridView.Rows[nowrowIndex - 1].Cells["本序数量"].Value.ToString()))
                    //{


                    //    up_order_count = decimal.Parse(this.ly_machinepart_process_workDataGridView.Rows[nowrowIndex - 1].Cells["本序数量"].Value.ToString());
                    //}
                    //else
                    //{

                    //    up_order_count = 0;
                    //}
                }
                else
                {
                    if (!string.IsNullOrEmpty(this.ly_machinepart_process_workDataGridView.CurrentRow.Cells["跟单数量"].Value.ToString()))
                    {
                        up_order_count = decimal.Parse(this.ly_machinepart_process_workDataGridView.CurrentRow.Cells["跟单数量"].Value.ToString());
                    }
                    else
                    {
                        up_order_count = 0;
                    }

                }


                if (!string.IsNullOrEmpty(this.ly_machinepart_process_workDataGridView.CurrentRow.Cells["本序数量"].Value.ToString()))
                {
                    order_count = decimal.Parse(this.ly_machinepart_process_workDataGridView.CurrentRow.Cells["本序数量"].Value.ToString());
                }
                else
                {
                    order_count = 0;
                }



                decimal arrange_count = 0;

                if (!string.IsNullOrEmpty(nowproduction_order))
                {

                    foreach (DataGridViewRow dgr in ly_production_order_detailDataGridView.Rows)
                    {

                        if (string.IsNullOrEmpty(dgr.Cells["数量1"].Value.ToString())) continue;
                        arrange_count = arrange_count + decimal.Parse(dgr.Cells["数量1"].Value.ToString());



                    }
                }



                if (arrange_count >= up_order_count)
                {
                    MessageBox.Show("跟单数量已经全部安排,不能增加跟单任务", "注意");

                    return;
                }
                else
                {



                    ///////////////////////////////////////////////////////////////////








                    this.ly_production_order_detailBindingSource.AddNew();

                    this.ly_production_order_detailDataGridView.CurrentRow.Cells["跟单编号1"].Value = nowproduction_order;

                    this.ly_production_order_detailDataGridView.CurrentRow.Cells["工序1"].Value = this.ly_machinepart_process_workDataGridView.CurrentRow.Cells["工序编号"].Value;

                    this.ly_production_order_detailDataGridView.CurrentRow.Cells["sequence_number1"].Value = this.ly_machinepart_process_workDataGridView.CurrentRow.Cells["sequence_number"].Value;


                    this.ly_production_order_detailDataGridView.CurrentRow.Cells["pruductionOrder_count1"].Value = this.ly_machinepart_process_workDataGridView.CurrentRow.Cells["跟单数量"].Value.ToString();

                    this.ly_production_order_detailDataGridView.CurrentRow.Cells["准终1"].Value = this.ly_machinepart_process_workDataGridView.CurrentRow.Cells["准终"].Value;

                    this.ly_production_order_detailDataGridView.CurrentRow.Cells["实际准终1"].Value = this.ly_machinepart_process_workDataGridView.CurrentRow.Cells["准终"].Value;

                    this.ly_production_order_detailDataGridView.CurrentRow.Cells["单件工时1"].Value = this.ly_machinepart_process_workDataGridView.CurrentRow.Cells["单件工时"].Value;
                    this.ly_production_order_detailDataGridView.CurrentRow.Cells["实际单件工时1"].Value = this.ly_machinepart_process_workDataGridView.CurrentRow.Cells["单件工时"].Value;

                    this.ly_production_order_detailDataGridView.CurrentRow.Cells["准终累加1"].Value = this.ly_machinepart_process_workDataGridView.CurrentRow.Cells["准终累加"].Value;
                    this.ly_production_order_detailDataGridView.CurrentRow.Cells["单件工时累加1"].Value = this.ly_machinepart_process_workDataGridView.CurrentRow.Cells["工时累加"].Value;

                    this.ly_production_order_detailDataGridView.CurrentRow.Cells["工时单价1"].Value = this.ly_machinepart_process_workDataGridView.CurrentRow.Cells["工时单价"].Value;
                    //this.ly_production_order_detailDataGridView.CurrentRow.Cells["材料单价1"].Value = this.ly_production_orderDataGridView.CurrentRow.Cells["物料单价"].Value;


                    this.ly_production_order_detailDataGridView.CurrentRow.Cells["数量1"].Value = up_order_count - arrange_count;

                    this.ly_production_order_detailDataGridView.CurrentRow.Cells["调度1"].Value = SQLDatabase.nowUserName();

                    this.ly_production_order_detailDataGridView.CurrentRow.Cells["排序日期"].Value = SQLDatabase.GetNowdate();
                    this.ly_production_order_detailDataGridView.CurrentRow.Cells["外协"].Value = "True";

                    SaveTask();
                }

            }
        }

        private void toolStripButton9_Click(object sender, EventArgs e)
        {

            if (null == this.ly_production_order_materialrequisitionDataGridView.CurrentRow) return;

            string nowproductionorder = this.ly_production_order_materialrequisitionDataGridView.CurrentRow.Cells["跟单编号5"].Value.ToString();
            string nowworker = this.ly_production_order_materialrequisitionDataGridView.CurrentRow.Cells["姓名5"].Value.ToString();
            string nowmaterialcode = this.ly_production_order_materialrequisitionDataGridView.CurrentRow.Cells["物料编号5"].Value.ToString();
            string nowmaterialname = this.ly_production_order_materialrequisitionDataGridView.CurrentRow.Cells["物料名称5"].Value.ToString();

            string message1 = "确认追加(跟单：" + nowproductionorder + "  " + nowmaterialname + "(" + nowmaterialcode + ") " + nowworker + " )领料吗?";
            string caption1 = "提示...";
            MessageBoxButtons buttons1 = MessageBoxButtons.YesNo;

            DialogResult result1;



            result1 = MessageBox.Show(message1, caption1, buttons1,
            MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

            if (result1 == DialogResult.Yes)
            {
                //string nowrequisition_style = "WXLL";


                //string nowmaterialrequisition_num = GetMaxMaterialrequisitioncode(nowcontractcode, nowparentitemno);


                string nowitemcode;

                ///////////////////////////////////////////////////////





                string sel = " SELECT   a.wzbh as 编号,a.mch as 名称,a.jph as 库位,a.xhc as 中方型号,a.xhc as 日方型号,a.gg as 规格,a.mch_jp as 简拼,a.mch_py as 拼音,b.sortname as类别 FROM ly_inma0010 a left join ly_materrial_sort b on a.sort1=b.sortcode  where a.status='原料' or a.status='基料' ";



                QueryForm queryForm = new QueryForm();


                queryForm.Sel = sel;
                queryForm.Constr = SQLDatabase.Connectstring;

                //Set the Column Collection to the filter Table
                //queryForm.SetSourceColumns(this.billMainDataSet.BalanceBill.Columns);

                queryForm.ShowDialog();


                if (!string.IsNullOrEmpty(queryForm.Result))
                {
                    nowitemcode = queryForm.Result;
                    //dgv.CurrentRow.Cells["discount_money"].Value = DBNull.Value;



                }
                else
                {
                    return;

                }



                //    ///////////////////////////////////////////////////////


                string nowdetailId = this.ly_production_order_materialrequisitionDataGridView.CurrentRow.Cells["detail_id5"].Value.ToString();
                string nowrequisition_style = this.ly_production_order_materialrequisitionDataGridView.CurrentRow.Cells["领料类别5"].Value.ToString();
                string nowproductionorderNum = this.ly_production_order_materialrequisitionDataGridView.CurrentRow.Cells["跟单编号5"].Value.ToString();
                // string nowitemcode = this.ly_production_order_materialrequisitionDataGridView.CurrentRow.Cells["物料编号5"].Value.ToString();

                string replacedmaterialrequisition_num = this.ly_production_order_materialrequisitionDataGridView.CurrentRow.Cells["领料单号5"].Value.ToString();
                //string replacedoriginalqty = this.ly_production_order_materialrequisitionDataGridView.CurrentRow.Cells["原单数量5"].Value.ToString();


                this.ly_production_order_materialrequisitionBindingSource.AddNew();





                this.ly_production_order_materialrequisitionDataGridView.CurrentRow.Cells["detail_id5"].Value = nowdetailId;

                this.ly_production_order_materialrequisitionDataGridView.CurrentRow.Cells["领料类别5"].Value = nowrequisition_style;

                this.ly_production_order_materialrequisitionDataGridView.CurrentRow.Cells["跟单编号5"].Value = nowproductionorderNum;


                //this.ly_production_order_materialrequisitionDataGridView.CurrentRow.Cells["领料单号"].Value = this.ly_production_order_materialrequisitionDataGridView.CurrentRow.Cells["跟单数量"].Value.ToString();

                this.ly_production_order_materialrequisitionDataGridView.CurrentRow.Cells["物料编号5"].Value = nowitemcode;

                this.ly_production_order_materialrequisitionDataGridView.CurrentRow.Cells["追加5"].Value = "True";
                this.ly_production_order_materialrequisitionDataGridView.CurrentRow.Cells["追加类别5"].Value = "普通";

                this.ly_production_order_materialrequisitionDataGridView.CurrentRow.Cells["原单编号5"].Value = replacedmaterialrequisition_num;
                //this.ly_production_order_materialrequisitionDataGridView.CurrentRow.Cells["原单数量5"].Value = replacedoriginalqty;

                this.ly_production_order_materialrequisitionDataGridView.EndEdit();
                this.ly_production_order_materialrequisitionBindingSource.EndEdit();

                this.ly_production_order_materialrequisitionTableAdapter.Update(this.lYMaterielRequirements.ly_production_order_materialrequisition);
                this.ly_production_order_materialrequisitionTableAdapter.Fill(this.lYMaterielRequirements.ly_production_order_materialrequisition, nowproductionorderNum);

                this.ly_production_order_materialrequisitionBindingSource.Position = this.ly_production_order_materialrequisitionBindingSource.Count - 1;
            }
            //if (null == ly_production_order_materialrequisitionDataGridView.CurrentRow) return;

            //if ("True" == this.ly_production_order_materialrequisitionDataGridView.CurrentRow.Cells["追加5"].Value.ToString())
            //{

            //    MessageBox.Show("追加领料单不能继续追加领料,可以在原领料单上继续追加领料的操作...", "注意");
            //    return;
            //}

            //string nowproductionorder = this.ly_production_order_materialrequisitionDataGridView.CurrentRow.Cells["跟单编号5"].Value.ToString();
            //string nowworker = this.ly_production_order_materialrequisitionDataGridView.CurrentRow.Cells["姓名5"].Value.ToString();
            //string nowmaterialcode = this.ly_production_order_materialrequisitionDataGridView.CurrentRow.Cells["物料编号5"].Value.ToString();
            //string nowmaterialname = this.ly_production_order_materialrequisitionDataGridView.CurrentRow.Cells["物料名称5"].Value.ToString();

            //string message1 = "确认追加(跟单：" + nowproductionorder + "  " + nowmaterialname + "(" + nowmaterialcode + ") " + nowworker + " )领料吗?";
            //string caption1 = "提示...";
            //MessageBoxButtons buttons1 = MessageBoxButtons.YesNo;

            //DialogResult result1;



            //result1 = MessageBox.Show(message1, caption1, buttons1,
            //MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

            //if (result1 == DialogResult.Yes)
            //{
            //    string nowdetailId = this.ly_production_order_materialrequisitionDataGridView.CurrentRow.Cells["detail_id5"].Value.ToString();
            //    string nowrequisition_style = this.ly_production_order_materialrequisitionDataGridView.CurrentRow.Cells["领料类别5"].Value.ToString();
            //    string nowproductionorderNum = this.ly_production_order_materialrequisitionDataGridView.CurrentRow.Cells["跟单编号5"].Value.ToString();
            //    string nowitemcode = this.ly_production_order_materialrequisitionDataGridView.CurrentRow.Cells["物料编号5"].Value.ToString();

            //    string replacedmaterialrequisition_num = this.ly_production_order_materialrequisitionDataGridView.CurrentRow.Cells["领料单号5"].Value.ToString();
            //    //string replacedoriginalqty = this.ly_production_order_materialrequisitionDataGridView.CurrentRow.Cells["原单数量5"].Value.ToString();


            //    this.ly_production_order_materialrequisitionBindingSource.AddNew();



            //    this.ly_production_order_materialrequisitionDataGridView.CurrentRow.Cells["detail_id5"].Value = nowdetailId;

            //    this.ly_production_order_materialrequisitionDataGridView.CurrentRow.Cells["领料类别5"].Value = nowrequisition_style;

            //    this.ly_production_order_materialrequisitionDataGridView.CurrentRow.Cells["跟单编号5"].Value = nowproductionorderNum;


            //    //this.ly_production_order_materialrequisitionDataGridView.CurrentRow.Cells["领料单号"].Value = this.ly_production_order_materialrequisitionDataGridView.CurrentRow.Cells["跟单数量"].Value.ToString();

            //    this.ly_production_order_materialrequisitionDataGridView.CurrentRow.Cells["物料编号5"].Value = nowitemcode;

            //    this.ly_production_order_materialrequisitionDataGridView.CurrentRow.Cells["追加5"].Value = "True";
            //    this.ly_production_order_materialrequisitionDataGridView.CurrentRow.Cells["追加类别5"].Value = "普通";

            //    this.ly_production_order_materialrequisitionDataGridView.CurrentRow.Cells["原单编号5"].Value = replacedmaterialrequisition_num;
            //    //this.ly_production_order_materialrequisitionDataGridView.CurrentRow.Cells["原单数量5"].Value = replacedoriginalqty;

            //    this.ly_production_order_materialrequisitionDataGridView.EndEdit();
            //    this.ly_production_order_materialrequisitionBindingSource.EndEdit();

            //    this.ly_production_order_materialrequisitionTableAdapter.Update(this.lYMaterielRequirements.ly_production_order_materialrequisition);
            //    this.ly_production_order_materialrequisitionTableAdapter.Fill(this.lYMaterielRequirements.ly_production_order_materialrequisition, nowproductionorderNum);

            //    this.ly_production_order_materialrequisitionBindingSource.Position = this.ly_production_order_materialrequisitionBindingSource.Count - 1;
            //}
        }

        private void toolStripButton11_Click(object sender, EventArgs e)
        {
            if (null == ly_production_order_materialrequisitionDataGridView.CurrentRow) return;

            if ("True" == this.ly_production_order_materialrequisitionDataGridView.CurrentRow.Cells["追加5"].Value.ToString())
            {

                MessageBox.Show("追加领料单不能继续追加领料,可以在原领料单上继续追加领料的操作...", "注意");
                return;
            }

            string nowproductionorder = this.ly_production_order_materialrequisitionDataGridView.CurrentRow.Cells["跟单编号5"].Value.ToString();
            string nowworker = this.ly_production_order_materialrequisitionDataGridView.CurrentRow.Cells["姓名5"].Value.ToString();
            string nowmaterialcode = this.ly_production_order_materialrequisitionDataGridView.CurrentRow.Cells["物料编号5"].Value.ToString();
            string nowmaterialname = this.ly_production_order_materialrequisitionDataGridView.CurrentRow.Cells["物料名称5"].Value.ToString();

            //string replacedmaterialrequisition_num = this.ly_production_order_materialrequisitionDataGridView.CurrentRow.Cells["原单编号5"].Value.ToString();
            //string replacedoriginalqty = this.ly_production_order_materialrequisitionDataGridView.CurrentRow.Cells["原单数量5"].Value.ToString();

            string message1 = "确认追加(跟单：" + nowproductionorder + "  " + nowmaterialname + "(" + nowmaterialcode + ") " + nowworker + " )代料领料吗?";
            string caption1 = "提示...";
            MessageBoxButtons buttons1 = MessageBoxButtons.YesNo;

            DialogResult result1;



            result1 = MessageBox.Show(message1, caption1, buttons1,
            MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

            if (result1 == DialogResult.Yes)
            {
                string nowdetailId = this.ly_production_order_materialrequisitionDataGridView.CurrentRow.Cells["detail_id5"].Value.ToString();
                string nowrequisition_style = this.ly_production_order_materialrequisitionDataGridView.CurrentRow.Cells["领料类别5"].Value.ToString();
                string nowproductionorderNum = this.ly_production_order_materialrequisitionDataGridView.CurrentRow.Cells["跟单编号5"].Value.ToString();
                string nowitemcode = this.ly_production_order_materialrequisitionDataGridView.CurrentRow.Cells["物料编号5"].Value.ToString();
                //string nowrequisition_style = this.ly_production_order_materialrequisitionDataGridView.CurrentRow.Cells["追加"].Value.ToString();

                string replacedmaterialrequisition_num = this.ly_production_order_materialrequisitionDataGridView.CurrentRow.Cells["领料单号5"].Value.ToString();
                //this.ly_production_order_materialrequisitionDataGridView.CurrentRow.Cells["原单数量5"].Value = replacedoriginalqty;


                string replaceitemno;

                ///////////////////////////////////////////////////////




                string sel;



                sel = "SELECT   a.replace_item as 编号, c.mch as 名称,c.dw AS 单位, a.replace_ratio as 代换比例, c.jph AS 库位, c.xhc AS 型号, c.gg AS 规格,  c.geometry as 形状, c.specific_weight as 比重, c.diameter as 直径, c.length as 长度, c.width as 宽度 ,c.length as 高度 FROM ly_material_replacelist a left join ly_inma0010 c on a.replace_item=c.wzbh where a.itemno='" + nowitemcode + "'";


                QueryForm queryForm = new QueryForm();


                queryForm.Sel = sel;
                queryForm.Constr = SQLDatabase.Connectstring;

                //Set the Column Collection to the filter Table
                //queryForm.SetSourceColumns(this.billMainDataSet.BalanceBill.Columns);

                queryForm.ShowDialog();


                if (!string.IsNullOrEmpty(queryForm.Result))
                {
                    replaceitemno = queryForm.Result;
                    //dgv.CurrentRow.Cells["discount_money"].Value = DBNull.Value;



                }
                else
                {
                    return;

                }



                ///////////////////////////////////////////////////////


                this.ly_production_order_materialrequisitionBindingSource.AddNew();



                this.ly_production_order_materialrequisitionDataGridView.CurrentRow.Cells["detail_id5"].Value = nowdetailId;

                this.ly_production_order_materialrequisitionDataGridView.CurrentRow.Cells["领料类别5"].Value = nowrequisition_style;

                this.ly_production_order_materialrequisitionDataGridView.CurrentRow.Cells["跟单编号5"].Value = nowproductionorderNum;


                //this.ly_production_order_materialrequisitionDataGridView.CurrentRow.Cells["领料单号"].Value = this.ly_production_order_materialrequisitionDataGridView.CurrentRow.Cells["跟单数量"].Value.ToString();

                this.ly_production_order_materialrequisitionDataGridView.CurrentRow.Cells["物料编号5"].Value = replaceitemno;

                this.ly_production_order_materialrequisitionDataGridView.CurrentRow.Cells["追加5"].Value = "True";
                this.ly_production_order_materialrequisitionDataGridView.CurrentRow.Cells["追加类别5"].Value = "代料";

                this.ly_production_order_materialrequisitionDataGridView.CurrentRow.Cells["原单编号5"].Value = replacedmaterialrequisition_num;
                //this.ly_production_order_materialrequisitionDataGridView.CurrentRow.Cells["原单数量5"].Value = replacedoriginalqty;


                this.ly_production_order_materialrequisitionDataGridView.EndEdit();
                this.ly_production_order_materialrequisitionBindingSource.EndEdit();

                this.ly_production_order_materialrequisitionTableAdapter.Update(this.lYMaterielRequirements.ly_production_order_materialrequisition);
                this.ly_production_order_materialrequisitionTableAdapter.Fill(this.lYMaterielRequirements.ly_production_order_materialrequisition, nowproductionorderNum);

                this.ly_production_order_materialrequisitionBindingSource.Position = this.ly_production_order_materialrequisitionBindingSource.Count - 1;
            }
        }

        private void toolStripButton10_Click(object sender, EventArgs e)
        {
            if (null == ly_production_order_materialrequisitionDataGridView.CurrentRow) return;

            string diaodu = this.ly_production_order_materialrequisitionDataGridView.CurrentRow.Cells["调度5"].Value.ToString();

            if (diaodu != SQLDatabase.nowUserName())
            {
                MessageBox.Show("请调度:" + diaodu + "删除", "注意");
                return;
            }



            if ("True" != this.ly_production_order_materialrequisitionDataGridView.CurrentRow.Cells["追加5"].Value.ToString())
            {

                MessageBox.Show("只有追加领料单才能删除...", "注意");
                return;
            }

            decimal nowgetqty;



            if ("" != this.ly_production_order_materialrequisitionDataGridView.CurrentRow.Cells["已领5"].Value.ToString())
            {
                nowgetqty = decimal.Parse(this.ly_production_order_materialrequisitionDataGridView.CurrentRow.Cells["已领5"].Value.ToString());
            }
            else
            {
                nowgetqty = 0;
            }

            if (nowgetqty > 0)
            {
                MessageBox.Show("已有领料记录,删除领料记录后才能删除领料单...", "注意");
                return;

            }

            string nowmaterialrequisitionnum = this.ly_production_order_materialrequisitionDataGridView.CurrentRow.Cells["领料单号5"].Value.ToString();

            string message1 = "当前(领料单：" + nowmaterialrequisitionnum + ")将被删除，继续吗？";
            string caption1 = "提示...";
            MessageBoxButtons buttons1 = MessageBoxButtons.YesNo;

            DialogResult result1;



            result1 = MessageBox.Show(message1, caption1, buttons1,
            MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

            if (result1 == DialogResult.Yes)
            {

                this.ly_production_order_materialrequisitionBindingSource.RemoveCurrent();


                this.ly_production_order_materialrequisitionDataGridView.EndEdit();
                this.ly_production_order_materialrequisitionBindingSource.EndEdit();

                this.ly_production_order_materialrequisitionTableAdapter.Update(this.lYMaterielRequirements.ly_production_order_materialrequisition);




            }
        }

        private void ly_production_order_detailDataGridView_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            DataGridView dgv = sender as DataGridView;

            if (null == dgv.CurrentCell) return;


            if ("数量1" == dgv.CurrentCell.OwningColumn.Name)
            {


                if (!checkqualityRec())
                {

                    MessageBox.Show("任务已有检验记录,不能修改, 操作取消", "注意");

                    return;

                }


                ChangeValue queryForm = new ChangeValue();

                queryForm.OldValue = dgv.CurrentCell.Value.ToString();
                queryForm.NewValue = "";
                queryForm.ChangeMode = "value";
                queryForm.ShowDialog();




                if (queryForm.NewValue != "")
                {
                    dgv.CurrentRow.Cells["数量1"].Value = queryForm.NewValue;
                    ////////////////////////////////////////////////////////////////////////

                    ///////////////////////////////////////////////////////////////// 

                    decimal up_order_count;


                    decimal up_quality;
                    decimal up_canuse;
                    decimal up_remake;

                    int nowrowIndex = this.ly_machinepart_process_workDataGridView.CurrentRow.Index;
                    string nowordername = this.ly_machinepart_process_workDataGridView.CurrentRow.Cells["工序名称"].Value.ToString();

                    int nowordernum = int.Parse(this.ly_machinepart_process_workDataGridView.CurrentRow.Cells["工序编号"].Value.ToString());


                    if (1 != nowordernum)

                    //if ("下料" != nowordername)
                    {
                        if (!string.IsNullOrEmpty(this.ly_machinepart_process_workDataGridView.Rows[nowrowIndex - 1].Cells["本序合格"].Value.ToString()))
                        {


                            up_quality = decimal.Parse(this.ly_machinepart_process_workDataGridView.Rows[nowrowIndex - 1].Cells["本序合格"].Value.ToString());
                        }
                        else
                        {

                            up_quality = 0;
                        }

                        if (!string.IsNullOrEmpty(this.ly_machinepart_process_workDataGridView.Rows[nowrowIndex - 1].Cells["本序可用数量"].Value.ToString()))
                        {


                            up_canuse = decimal.Parse(this.ly_machinepart_process_workDataGridView.Rows[nowrowIndex - 1].Cells["本序可用数量"].Value.ToString());
                        }
                        else
                        {

                            up_canuse = 0;
                        }

                        ////////////////////////

                        if (!string.IsNullOrEmpty(this.ly_machinepart_process_workDataGridView.Rows[nowrowIndex - 1].Cells["本序返修合格"].Value.ToString()))
                        {


                            up_remake = decimal.Parse(this.ly_machinepart_process_workDataGridView.Rows[nowrowIndex - 1].Cells["本序返修合格"].Value.ToString());
                        }
                        else
                        {

                            up_remake = 0;
                        }


                        up_order_count = up_quality + up_canuse + up_remake;

                      

                        //if (!string.IsNullOrEmpty(this.ly_machinepart_process_workDataGridView.Rows[nowrowIndex - 1].Cells["本序数量"].Value.ToString()))
                        //{


                        //    up_order_count = decimal.Parse(this.ly_machinepart_process_workDataGridView.Rows[nowrowIndex - 1].Cells["本序数量"].Value.ToString());
                        //}
                        //else
                        //{

                        //    up_order_count = 0;
                        //}
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(this.ly_machinepart_process_workDataGridView.CurrentRow.Cells["跟单数量"].Value.ToString()))
                        {
                            up_order_count = decimal.Parse(this.ly_machinepart_process_workDataGridView.CurrentRow.Cells["跟单数量"].Value.ToString());
                        }
                        else
                        {
                            up_order_count = 0;
                        }

                    }


                    //if (!string.IsNullOrEmpty(this.ly_machinepart_process_workDataGridView.CurrentRow.Cells["本序数量"].Value.ToString()))
                    //{
                    //    order_count = decimal.Parse(this.ly_machinepart_process_workDataGridView.CurrentRow.Cells["本序数量"].Value.ToString());
                    //}
                    //else
                    //{
                    //    order_count = 0;
                    //}

                    //////////////////////////////////////////////////////////

                    decimal order_count;


                    if (!string.IsNullOrEmpty(this.ly_machinepart_process_workDataGridView.CurrentRow.Cells["跟单数量"].Value.ToString()))
                    {
                        order_count = decimal.Parse(this.ly_machinepart_process_workDataGridView.CurrentRow.Cells["跟单数量"].Value.ToString());
                    }
                    else
                    {
                        order_count = 0;
                    }



                    decimal arrange_count = 0;

                    if (!string.IsNullOrEmpty(nowproduction_order))
                    {

                        foreach (DataGridViewRow dgr in ly_production_order_detailDataGridView.Rows)
                        {

                            if (string.IsNullOrEmpty(dgr.Cells["数量1"].Value.ToString())) continue;
                            arrange_count = arrange_count + decimal.Parse(dgr.Cells["数量1"].Value.ToString());



                        }
                    }


                    if (arrange_count > up_order_count)
                    {

                        dgv.CurrentRow.Cells["数量1"].Value = queryForm.OldValue;

                        MessageBox.Show("任务安排不能超过上序可用数量, 操作取消", "注意");

                        return;

                    }

                    //////////////////////////////////////////////////////////////////////////
                    SaveTask();


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

            if ("合格1" == dgv.CurrentCell.OwningColumn.Name)
            {


                return;
                string nowordername = this.ly_machinepart_process_workDataGridView.CurrentRow.Cells["工序名称"].Value.ToString();

                if ("下料" != nowordername) return;


                ChangeValue queryForm = new ChangeValue();

                queryForm.OldValue = dgv.CurrentCell.Value.ToString();
                queryForm.NewValue = "";
                queryForm.ChangeMode = "value";
                queryForm.ShowDialog();




                if (queryForm.NewValue != "")
                {
                    dgv.CurrentRow.Cells["合格1"].Value = queryForm.NewValue;

                    //////////////////////////////////////////////////////////////////////////////

                    int detail_Id = int.Parse(dgv.CurrentRow.Cells["detailId"].Value.ToString());



                    //string insstr = " delete ly_material_plan_main  where material_plan_num = '" + nowPlanNumber + "'";

                    string updstr = " update ly_production_order_inspection  " +
                            "  set qualified_count=  " + queryForm.NewValue + " , waste_count=inspect_count-" + queryForm.NewValue
                            + " where  detail_id=" + detail_Id.ToString();


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

                        SaveTask();

                    }

                    //////////////////////////////////////////////////////////////////////////////////////








                }
                else
                {


                }
                return;

            }


            ///////////////////////////////////////////////////////

            if ("工号" == dgv.CurrentCell.OwningColumn.Name || "姓名1" == dgv.CurrentCell.OwningColumn.Name)
            {



                if (!checkqualityRec())
                {

                    MessageBox.Show("任务已有检验记录,不能修改, 操作取消", "注意");

                    return;

                }

                string outflag = this.ly_production_order_detailDataGridView.CurrentRow.Cells["外协"].Value.ToString();
                string sel;

                if ("True" == outflag)
                {

                    sel = "SELECT  supplier_code as 工号, supplier_name as 姓名 FROM ly_supplier_list where sort_code='4'";
                }
                else
                {

                    sel = "SELECT  work_code as 工号, worker_name as 姓名 FROM ly_worker_list";
                }


                QueryForm queryForm = new QueryForm();


                queryForm.Sel = sel;
                queryForm.Constr = SQLDatabase.Connectstring;

                //Set the Column Collection to the filter Table
                //queryForm.SetSourceColumns(this.billMainDataSet.BalanceBill.Columns);

                queryForm.ShowDialog();


                if (queryForm.Result != "")
                {
                    dgv.CurrentRow.Cells["工号"].Value = queryForm.Result;
                    //dgv.CurrentRow.Cells["discount_money"].Value = DBNull.Value;

                    //dgv.CurrentRow.Cells["approve_flag"].Value = "False";
                    SaveTask();


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

            if ("实际准终1" == dgv.CurrentCell.OwningColumn.Name)
            {

                ChangeValue queryForm = new ChangeValue();

                queryForm.OldValue = dgv.CurrentCell.Value.ToString();
                queryForm.NewValue = "";
                queryForm.ChangeMode = "value";
                queryForm.ShowDialog();




                if (queryForm.NewValue != "")
                {
                    dgv.CurrentRow.Cells["实际准终1"].Value = queryForm.NewValue;
                    //dgv.CurrentRow.Cells["discount_money"].Value = DBNull.Value;

                    //dgv.CurrentRow.Cells["approve_flag"].Value = "False";
                    SaveTask();


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

            if ("实际加工工时1" == dgv.CurrentCell.OwningColumn.Name)
            {

                ChangeValue queryForm = new ChangeValue();

                queryForm.OldValue = dgv.CurrentCell.Value.ToString();
                queryForm.NewValue = "";
                queryForm.ChangeMode = "value";
                queryForm.ShowDialog();




                if (queryForm.NewValue != "")
                {
                    dgv.CurrentRow.Cells["实际加工工时1"].Value = queryForm.NewValue;
                    //dgv.CurrentRow.Cells["discount_money"].Value = DBNull.Value;

                    //dgv.CurrentRow.Cells["approve_flag"].Value = "False";
                    SaveTask();


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
            ///////////////////////////////////////////////////////

            if ("实际可用工时1" == dgv.CurrentCell.OwningColumn.Name)
            {

                ChangeValue queryForm = new ChangeValue();

                queryForm.OldValue = dgv.CurrentCell.Value.ToString();
                queryForm.NewValue = "";
                queryForm.ChangeMode = "value";
                queryForm.ShowDialog();




                if (queryForm.NewValue != "")
                {
                    dgv.CurrentRow.Cells["实际可用工时1"].Value = queryForm.NewValue;
                    //dgv.CurrentRow.Cells["discount_money"].Value = DBNull.Value;

                    //dgv.CurrentRow.Cells["approve_flag"].Value = "False";
                    SaveTask();


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

            if ("实际废品工时1" == dgv.CurrentCell.OwningColumn.Name)
            {

                ChangeValue queryForm = new ChangeValue();

                queryForm.OldValue = dgv.CurrentCell.Value.ToString();
                queryForm.NewValue = "";
                queryForm.ChangeMode = "value";
                queryForm.ShowDialog();




                if (queryForm.NewValue != "")
                {
                    dgv.CurrentRow.Cells["实际废品工时1"].Value = queryForm.NewValue;
                    //dgv.CurrentRow.Cells["discount_money"].Value = DBNull.Value;

                    //dgv.CurrentRow.Cells["approve_flag"].Value = "False";
                    SaveTask();


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

            if ("备注1" == dgv.CurrentCell.OwningColumn.Name)
            {

                ChangeValue queryForm = new ChangeValue();

                queryForm.OldValue = dgv.CurrentCell.Value.ToString();
                queryForm.NewValue = "";
                queryForm.ChangeMode = "longstring";
                queryForm.ShowDialog();




                if (queryForm.NewValue != "")
                {
                    dgv.CurrentRow.Cells["备注1"].Value = queryForm.NewValue;
                    //dgv.CurrentRow.Cells["discount_money"].Value = DBNull.Value;

                    //dgv.CurrentRow.Cells["approve_flag"].Value = "False";
                    SaveTask();


                    //CountPlanStru();

                }
                else
                {

                }
                return;

            }

            /////////////////////////////

            ///////////////////////////////////////////////////////

            if ("排序日期" == dgv.CurrentCell.OwningColumn.Name)
            {

                ChangeValue queryForm = new ChangeValue();

                queryForm.OldValue = dgv.CurrentCell.Value.ToString();
                queryForm.NewValue = "";
                queryForm.ChangeMode = "datetime";
                queryForm.ShowDialog();




                if (queryForm.NewValue != "")
                {
                    dgv.CurrentRow.Cells["排序日期"].Value = queryForm.NewValue;
                    //dgv.CurrentRow.Cells["discount_money"].Value = DBNull.Value;

                    //dgv.CurrentRow.Cells["approve_flag"].Value = "False";
                    SaveTask();


                    //CountPlanStru();

                }
                else
                {

                }
                return;

            }

        }

        private void ly_production_order_materialrequisitionDataGridView_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (null == this.ly_production_order_materialrequisitionDataGridView.CurrentRow) return;

            DataGridView dgv = sender as DataGridView;

            string diaodu = this.ly_production_order_materialrequisitionDataGridView.CurrentRow.Cells["调度5"].Value.ToString();

            //if ( !SQLDatabase.CheckHaveRight(SQLDatabase.NowUserID,"追加领料批准" ))
            //{
            //    MessageBox.Show("无追加领料批准权限", "注意");
            //    return;
            //}

            //string nowgeometry;



            //if (!string.IsNullOrEmpty(dgv.CurrentRow.Cells["geometry"].Value.ToString()))
            //{
            //    nowgeometry = dgv.CurrentRow.Cells["geometry"].Value.ToString();
            //}
            //else
            //{
            //    nowgeometry = "ELSE";
            //}


            //if ("True" != this.ly_production_order_materialrequisitionDataGridView.CurrentRow.Cells["追加5"].Value.ToString() && "ELSE" == nowgeometry)
            //{

            //    MessageBox.Show("只有追加领料单才能修改...", "注意");
            //    return;
            //}

            decimal nowgetqty;

            if ("" != this.ly_production_order_materialrequisitionDataGridView.CurrentRow.Cells["已领5"].Value.ToString())
            {
                nowgetqty = decimal.Parse(this.ly_production_order_materialrequisitionDataGridView.CurrentRow.Cells["已领5"].Value.ToString());
            }
            else
            {
                nowgetqty = 0;
            }

            if (nowgetqty > 0)
            {
                MessageBox.Show("已有领料记录,删除领料记录后才能修改领料单...", "注意");
                return;

            }

            //DataGridView dgv = sender as DataGridView;

            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            // if ("数量5" == dgv.CurrentCell.OwningColumn.Name && dgv.CurrentRow.Cells["追加类别5"].Value.ToString() == "普通")
            if ("数量5" == dgv.CurrentCell.OwningColumn.Name && dgv.CurrentRow.Cells["追加类别5"].Value.ToString() != "代料")

            {
                return;
                ChangeValue queryForm = new ChangeValue();

                queryForm.OldValue = dgv.CurrentCell.Value.ToString();
                queryForm.NewValue = "";
                queryForm.ChangeMode = "value";
                queryForm.ShowDialog();

                if (queryForm.NewValue != "")
                {
                    dgv.CurrentRow.Cells["数量5"].Value = queryForm.NewValue;
                    //dgv.CurrentRow.Cells["discount_money"].Value = DBNull.Value;

                    //dgv.CurrentRow.Cells["approve_flag"].Value = "False";
                    this.ly_production_order_materialrequisitionDataGridView.EndEdit();
                    this.ly_production_order_materialrequisitionBindingSource.EndEdit();

                    this.ly_production_order_materialrequisitionTableAdapter.Update(this.lYMaterielRequirements.ly_production_order_materialrequisition);


                    //CountPlanStru();

                }
                else
                {
                    MessageBox.Show("代料领料请在原单数量里输入被代料数量,系统会自动按比例计算代料数量...", "注意");

                    //hT_Manage_ItemDataGridView.CurrentRow.Cells["apply_money"].Value = queryForm.NewValue;
                    //dgv.CurrentRow.Cells["discount_money"].Value = DBNull.Value;
                    //dgv.CurrentRow.Cells["apply_money"].Value = DBNull.Value;
                    //dgv.CurrentRow.Cells["approve_flag"].Value = "False";
                    //SaveChanged();

                }
                return;

            }

            if ("数量5" == dgv.CurrentCell.OwningColumn.Name && dgv.CurrentRow.Cells["追加类别5"].Value.ToString() == "代料")
            {

                MessageBox.Show("代料领料请在原单数量里输入被代料数量,系统会自动按比例计算代料数量...", "注意");
                return;
            }

            ////////////////////////////////////////////

            if ("原单数量5" == dgv.CurrentCell.OwningColumn.Name && dgv.CurrentRow.Cells["追加类别5"].Value.ToString() == "代料")
            {
                return;
                ChangeValue queryForm = new ChangeValue();

                queryForm.OldValue = dgv.CurrentCell.Value.ToString();
                queryForm.NewValue = "";
                queryForm.ChangeMode = "value";
                queryForm.ShowDialog();

                if (queryForm.NewValue != "")
                {
                    dgv.CurrentRow.Cells["原单数量5"].Value = queryForm.NewValue;
                    //dgv.CurrentRow.Cells["discount_money"].Value = DBNull.Value;

                    //dgv.CurrentRow.Cells["approve_flag"].Value = "False";
                    this.ly_production_order_materialrequisitionDataGridView.EndEdit();
                    this.ly_production_order_materialrequisitionBindingSource.EndEdit();

                    this.ly_production_order_materialrequisitionTableAdapter.Update(this.lYMaterielRequirements.ly_production_order_materialrequisition);


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


            ///////////////////////////////////////////////////////

            if ("安排日期5" == dgv.CurrentCell.OwningColumn.Name)
            {

                return;
                ChangeValue queryForm = new ChangeValue();

                queryForm.OldValue = dgv.CurrentCell.Value.ToString();
                queryForm.NewValue = "";
                queryForm.ChangeMode = "datetime";
                queryForm.ShowDialog();




                if (queryForm.NewValue != "")
                {
                    dgv.CurrentRow.Cells["安排日期5"].Value = queryForm.NewValue;
                    //dgv.CurrentRow.Cells["discount_money"].Value = DBNull.Value;

                    //dgv.CurrentRow.Cells["approve_flag"].Value = "False";
                    this.ly_production_order_materialrequisitionDataGridView.EndEdit();
                    this.ly_production_order_materialrequisitionBindingSource.EndEdit();

                    this.ly_production_order_materialrequisitionTableAdapter.Update(this.lYMaterielRequirements.ly_production_order_materialrequisition);




                    //CountPlanStru();

                }
                else
                {


                }
                return;
            }



            ///////////////////////////////////////////////////////

            if ("备注5" == dgv.CurrentCell.OwningColumn.Name)
            {
                return;
                ChangeValue queryForm = new ChangeValue();

                queryForm.OldValue = dgv.CurrentCell.Value.ToString();
                queryForm.NewValue = "";
                queryForm.ChangeMode = "longstring";
                queryForm.ShowDialog();




                if (queryForm.NewValue != "")
                {
                    dgv.CurrentRow.Cells["备注5"].Value = queryForm.NewValue;
                    //dgv.CurrentRow.Cells["discount_money"].Value = DBNull.Value;

                    //dgv.CurrentRow.Cells["approve_flag"].Value = "False";
                    this.ly_production_order_materialrequisitionDataGridView.EndEdit();
                    this.ly_production_order_materialrequisitionBindingSource.EndEdit();

                    this.ly_production_order_materialrequisitionTableAdapter.Update(this.lYMaterielRequirements.ly_production_order_materialrequisition);




                    //CountPlanStru();

                }
                else
                {

                }
                return;

            }

            ///////////////////////////////////////////////////////

            if ("批准意见5" == dgv.CurrentCell.OwningColumn.Name)
            {

                ChangeValue queryForm = new ChangeValue();

                queryForm.OldValue = dgv.CurrentCell.Value.ToString();
                queryForm.NewValue = "";
                queryForm.ChangeMode = "longstring";
                queryForm.ShowDialog();




                if (queryForm.NewValue != "")
                {



                    if (!SQLDatabase.CheckHaveRight(SQLDatabase.NowUserID, "追加领料批准"))
                    {
                        MessageBox.Show("无追加领料批准权限,操作取消...", "注意");
                        return;

                    }
                    dgv.CurrentRow.Cells["批准意见5"].Value = queryForm.NewValue;
                    //dgv.CurrentRow.Cells["discount_money"].Value = DBNull.Value;

                    //dgv.CurrentRow.Cells["approve_flag"].Value = "False";
                    this.ly_production_order_materialrequisitionDataGridView.EndEdit();
                    this.ly_production_order_materialrequisitionBindingSource.EndEdit();

                    this.ly_production_order_materialrequisitionTableAdapter.Update(this.lYMaterielRequirements.ly_production_order_materialrequisition);



                }
                else
                {

                }
                return;

            }


            if ("批准5" == dgv.CurrentCell.OwningColumn.Name)
            {
                if (!SQLDatabase.CheckHaveRight(SQLDatabase.NowUserID, "追加领料批准"))
                {
                    MessageBox.Show("无追加领料批准权限,操作取消...", "注意");
                    return;

                }

                if ("True" == dgv.CurrentRow.Cells["批准5"].Value.ToString())
                {
                    dgv.CurrentRow.Cells["批准5"].Value = "False";
                    dgv.CurrentRow.Cells["批准人5"].Value = DBNull.Value;

                }
                else
                {

                    dgv.CurrentRow.Cells["批准5"].Value = "True";
                    dgv.CurrentRow.Cells["批准人5"].Value = SQLDatabase.nowUserName();
                }



                this.ly_production_order_materialrequisitionDataGridView.EndEdit();
                this.ly_production_order_materialrequisitionBindingSource.EndEdit();

                this.ly_production_order_materialrequisitionTableAdapter.Update(this.lYMaterielRequirements.ly_production_order_materialrequisition);



                return;
            }



            //if ("ELSE" != nowgeometry)
            //{


            //    if ("length" == dgv.CurrentCell.OwningColumn.Name)
            //    {
            //        return;
            //        if ("ELSE" == nowgeometry) return;
            //        //if ("圆柱体" != nowgeometry) return;

            //        ChangeValue queryForm = new ChangeValue();

            //        queryForm.OldValue = dgv.CurrentCell.Value.ToString();
            //        queryForm.NewValue = "";
            //        queryForm.ChangeMode = "value";
            //        queryForm.ShowDialog();




            //        if (queryForm.NewValue != "")
            //        {
            //            dgv.CurrentRow.Cells["length"].Value = queryForm.NewValue;
            //            //dgv.CurrentRow.Cells["discount_money"].Value = DBNull.Value;
            //            SetUseNum(nowgeometry);
            //            //dgv.CurrentRow.Cells["approve_flag"].Value = "False";
            //            this.ly_production_order_materialrequisitionDataGridView.EndEdit();
            //            this.ly_production_order_materialrequisitionBindingSource.EndEdit();

            //            this.ly_production_order_materialrequisitionTableAdapter.Update(this.lYMaterielRequirements.ly_production_order_materialrequisition);


            //        }
            //        else
            //        {


            //        }
            //        return;
            //    }


            //    if ("width" == dgv.CurrentCell.OwningColumn.Name)
            //    {
            //        return;
            //        if ("ELSE" == nowgeometry) return;
            //        if ("板料" != nowgeometry) return;

            //        ChangeValue queryForm = new ChangeValue();

            //        queryForm.OldValue = dgv.CurrentCell.Value.ToString();
            //        queryForm.NewValue = "";
            //        queryForm.ChangeMode = "value";
            //        queryForm.ShowDialog();




            //        if (queryForm.NewValue != "")
            //        {
            //            dgv.CurrentRow.Cells["width"].Value = queryForm.NewValue;
            //            //dgv.CurrentRow.Cells["discount_money"].Value = DBNull.Value;
            //            SetUseNum(nowgeometry);
            //            //dgv.CurrentRow.Cells["approve_flag"].Value = "False";
            //            this.ly_production_order_materialrequisitionDataGridView.EndEdit();
            //            this.ly_production_order_materialrequisitionBindingSource.EndEdit();

            //            this.ly_production_order_materialrequisitionTableAdapter.Update(this.lYMaterielRequirements.ly_production_order_materialrequisition);


            //        }
            //        else
            //        {


            //        }
            //        return;
            //    }



            //    if ("height" == dgv.CurrentCell.OwningColumn.Name)
            //    {
            //        return;
            //        if ("ELSE" == nowgeometry) return;
            //        if ("板料" != nowgeometry) return;
            //        if ("True" != this.ly_production_order_materialrequisitionDataGridView.CurrentRow.Cells["追加5"].Value.ToString()) return;

            //        ChangeValue queryForm = new ChangeValue();

            //        queryForm.OldValue = dgv.CurrentCell.Value.ToString();
            //        queryForm.NewValue = "";
            //        queryForm.ChangeMode = "value";
            //        queryForm.ShowDialog();


            //        if (queryForm.NewValue != "")
            //        {
            //            dgv.CurrentRow.Cells["height"].Value = queryForm.NewValue;
            //            //dgv.CurrentRow.Cells["discount_money"].Value = DBNull.Value;
            //            SetUseNum(nowgeometry);
            //            //dgv.CurrentRow.Cells["approve_flag"].Value = "False";
            //            this.ly_production_order_materialrequisitionDataGridView.EndEdit();
            //            this.ly_production_order_materialrequisitionBindingSource.EndEdit();

            //            this.ly_production_order_materialrequisitionTableAdapter.Update(this.lYMaterielRequirements.ly_production_order_materialrequisition);


            //        }
            //        else
            //        {


            //        }
            //        return;
            //    }

            //    /////////////////////////////////////////////////

            //    if ("diameter" == dgv.CurrentCell.OwningColumn.Name)
            //    {
            //        return;
            //        if ("ELSE" == nowgeometry) return;
            //        if ("棒料" != nowgeometry) return;
            //        if ("True" != this.ly_production_order_materialrequisitionDataGridView.CurrentRow.Cells["追加5"].Value.ToString()) return;

            //        ChangeValue queryForm = new ChangeValue();

            //        queryForm.OldValue = dgv.CurrentCell.Value.ToString();
            //        queryForm.NewValue = "";
            //        queryForm.ChangeMode = "value";
            //        queryForm.ShowDialog();




            //        if (queryForm.NewValue != "")
            //        {
            //            dgv.CurrentRow.Cells["diameter"].Value = queryForm.NewValue;
            //            //dgv.CurrentRow.Cells["discount_money"].Value = DBNull.Value;
            //            SetUseNum(nowgeometry);
            //            //dgv.CurrentRow.Cells["approve_flag"].Value = "False";
            //            this.ly_production_order_materialrequisitionDataGridView.EndEdit();
            //            this.ly_production_order_materialrequisitionBindingSource.EndEdit();

            //            this.ly_production_order_materialrequisitionTableAdapter.Update(this.lYMaterielRequirements.ly_production_order_materialrequisition);



            //        }
            //        else
            //        {
            //        }
            //        return;
            //    }


            //    if ("out_count" == dgv.CurrentCell.OwningColumn.Name)
            //    {
            //        return;
            //        if ("ELSE" == nowgeometry) return;


            //        ChangeValue queryForm = new ChangeValue();

            //        queryForm.OldValue = dgv.CurrentCell.Value.ToString();
            //        queryForm.NewValue = "";
            //        queryForm.ChangeMode = "value";
            //        queryForm.ShowDialog();




            //        if (queryForm.NewValue != "")
            //        {
            //            dgv.CurrentRow.Cells["out_count"].Value = queryForm.NewValue;
            //            //dgv.CurrentRow.Cells["discount_money"].Value = DBNull.Value;
            //            SetUseNum(nowgeometry);
            //            //dgv.CurrentRow.Cells["approve_flag"].Value = "False";
            //            this.ly_production_order_materialrequisitionDataGridView.EndEdit();
            //            this.ly_production_order_materialrequisitionBindingSource.EndEdit();

            //            this.ly_production_order_materialrequisitionTableAdapter.Update(this.lYMaterielRequirements.ly_production_order_materialrequisition);



            //        }
            //        else
            //        {

            //        }
            //        return;
            //    }
            //}
        }
        private void SetUseNum(string nowgeometry)
        {

            decimal nowspecific_weight;
            decimal nowdiameter;
            decimal nowlength;
            decimal nowwidth;
            decimal nowheight;
            decimal nowoutcount;
            decimal qtysetwaste;



            if (!string.IsNullOrEmpty(this.ly_production_order_materialrequisitionDataGridView.CurrentRow.Cells["diameter"].Value.ToString()))
            {
                nowdiameter = decimal.Parse(this.ly_production_order_materialrequisitionDataGridView.CurrentRow.Cells["diameter"].Value.ToString());
            }
            else
            {
                nowdiameter = 0;
            }

            if (!string.IsNullOrEmpty(this.ly_production_order_materialrequisitionDataGridView.CurrentRow.Cells["specific_weight"].Value.ToString()))
            {
                nowspecific_weight = decimal.Parse(this.ly_production_order_materialrequisitionDataGridView.CurrentRow.Cells["specific_weight"].Value.ToString());
            }
            else
            {
                nowspecific_weight = 0;
            }

            if (!string.IsNullOrEmpty(this.ly_production_order_materialrequisitionDataGridView.CurrentRow.Cells["length"].Value.ToString()))
            {
                nowlength = decimal.Parse(this.ly_production_order_materialrequisitionDataGridView.CurrentRow.Cells["length"].Value.ToString());
            }
            else
            {
                nowlength = 0;
            }

            if (!string.IsNullOrEmpty(this.ly_production_order_materialrequisitionDataGridView.CurrentRow.Cells["width"].Value.ToString()))
            {
                nowwidth = decimal.Parse(this.ly_production_order_materialrequisitionDataGridView.CurrentRow.Cells["width"].Value.ToString());
            }
            else
            {
                nowwidth = 0;
            }

            if (!string.IsNullOrEmpty(this.ly_production_order_materialrequisitionDataGridView.CurrentRow.Cells["height"].Value.ToString()))
            {
                nowheight = decimal.Parse(this.ly_production_order_materialrequisitionDataGridView.CurrentRow.Cells["height"].Value.ToString());
            }
            else
            {
                nowheight = 0;
            }

            if (!string.IsNullOrEmpty(this.ly_production_order_materialrequisitionDataGridView.CurrentRow.Cells["out_count"].Value.ToString()))
            {
                nowoutcount = decimal.Parse(this.ly_production_order_materialrequisitionDataGridView.CurrentRow.Cells["out_count"].Value.ToString());
            }
            else
            {
                nowoutcount = -1;
            }

            if (!string.IsNullOrEmpty(this.ly_production_order_materialrequisitionDataGridView.CurrentRow.Cells["qty_set_waste"].Value.ToString()))
            {
                qtysetwaste = 100 + decimal.Parse(this.ly_production_order_materialrequisitionDataGridView.CurrentRow.Cells["qty_set_waste"].Value.ToString());
            }
            else
            {
                qtysetwaste = 100;
            }


            if ("棒料" == nowgeometry)
            {
                this.ly_production_order_materialrequisitionDataGridView.CurrentRow.Cells["数量5"].Value = decimal.Parse("3.1415927") * nowdiameter * nowdiameter * nowlength / 4 * nowspecific_weight / 1000 / 1000 * nowoutcount * qtysetwaste / 100;

            }

            if ("板料" == nowgeometry)
            {
                this.ly_production_order_materialrequisitionDataGridView.CurrentRow.Cells["数量5"].Value = nowlength * nowwidth * nowheight * nowspecific_weight / 1000 / 1000 * nowoutcount * qtysetwaste / 100;

            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (this.checkBox1.Checked)
            {

                this.ly_production_recordsBindingSource.Filter = "";

            }
            else
            {

                this.ly_production_recordsBindingSource.Filter = "处理=0";
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            this.lY_productionorder_listTableAdapter.Fill(this.lYQualityInspector.LY_productionorder_list, DateTime.Parse(this.dateTimePicker1.Text).Date, DateTime.Parse(this.dateTimePicker2.Text).Date);
        }
        private void 统一指定提前期ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!SQLDatabase.CheckHaveRight(SQLDatabase.NowUserID, "关闭机加工单"))
            {
                return;
            }
            string product_order = ""; //关闭的工单ID

            DataGridView dgv = lY_productionorder_listDataGridView;
            NewFrm.Show(this);
            foreach (DataGridViewRow dgr in dgv.Rows)
            {
                if (true == dgr.Selected)
                {
                    if (dgr.Cells["closeBit"].Value.ToString() == "False" || dgr.Cells["closeBit"].Value.ToString() == "")  // 已经关闭就跳过它
                    {
                        product_order = dgr.Cells["product_id"].Value.ToString();


                        NewFrm.Notify(this, "正在更新:  (" + dgr.Cells["跟单编号"].Value.ToString() + ")" + "   工单");
                        UpdateFinishCount(0, product_order, 1);
                    }

                }
            }


            this.lY_productionorder_listTableAdapter.Fill(this.lYQualityInspector.LY_productionorder_list, DateTime.Parse(this.dateTimePicker1.Text).Date, DateTime.Parse(this.dateTimePicker2.Text).Date);

            NewFrm.Hide(this);
        }




        private void UpdateFinishCount(decimal closeEditCount, string orderId, int isFin)
        {
            if (string.IsNullOrEmpty(orderId)) return;

            SqlConnection sqlConnection1 = new SqlConnection(SQLDatabase.Connectstring);
            SqlCommand cmd = new SqlCommand();

            cmd.Parameters.Add("@closeEditCount", SqlDbType.Decimal);
            cmd.Parameters["@closeEditCount"].Value = closeEditCount;


            cmd.Parameters.Add("@isFin", SqlDbType.Int);
            cmd.Parameters["@isFin"].Value = isFin;

            cmd.Parameters.Add("@product_orderId", SqlDbType.Int);
            cmd.Parameters["@product_orderId"].Value = int.Parse(orderId);



            cmd.Parameters.Add("@closeTime", SqlDbType.DateTime);
            cmd.Parameters["@closeTime"].Value = DateTime.Now;

            cmd.Parameters.Add("@closePeople", SqlDbType.VarChar);
            cmd.Parameters["@closePeople"].Value = SQLDatabase.nowUserName();


            cmd.CommandText = "LY_Update_PruductOrder";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = sqlConnection1;

            sqlConnection1.Open();
            int aaa = cmd.ExecuteNonQuery();
            sqlConnection1.Close();




        }

        private void 取消该关闭ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!SQLDatabase.CheckHaveRight(SQLDatabase.NowUserID, "关闭机加工单"))
            {
                return;
            }
            string product_order = ""; //关闭的工单ID

            DataGridView dgv = lY_productionorder_listDataGridView;
            NewFrm.Show(this);
            foreach (DataGridViewRow dgr in dgv.Rows)
            {
                if (true == dgr.Selected)
                {
                    if (dgr.Cells["closeBit"].Value.ToString() == "True")  //没有关闭就跳过它
                    {
                        product_order = dgr.Cells["product_id"].Value.ToString();


                        NewFrm.Notify(this, "正在更新:  (" + dgr.Cells["跟单编号"].Value.ToString() + ")" + "   工单");
                        UpdateFinishCount(0, product_order, -1);
                    }

                }
            }


            this.lY_productionorder_listTableAdapter.Fill(this.lYQualityInspector.LY_productionorder_list, DateTime.Parse(this.dateTimePicker1.Text).Date, DateTime.Parse(this.dateTimePicker2.Text).Date);

            NewFrm.Hide(this);

        }

        private void 关闭单条工单单ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!SQLDatabase.CheckHaveRight(SQLDatabase.NowUserID, "关闭机加工单"))
            {
                return;
            }


            DataGridView dgv = lY_productionorder_listDataGridView;

            int k = 0;
            string product_order = "";
            string isfinish = "";
            decimal closeCount = 0;
            foreach (DataGridViewRow dgr in dgv.Rows)
            {
                if (true == dgr.Selected)
                {
                    k++;
                    product_order = dgr.Cells["product_id"].Value.ToString();
                    isfinish = dgr.Cells["closeBit"].Value.ToString();
                    closeCount = decimal.Parse(dgr.Cells["not_instore_count"].Value.ToString()); //未入数量
                }
            }
            if (k > 1)
            {

                MessageBox.Show("只能选择一条"); return;
            }
            if (isfinish == "False" || isfinish == "")
            {

                MessageBox.Show("请先选择关闭该条工单"); return;
            }



            ChangeValue queryForm = new ChangeValue();
            queryForm.Text = "修改关闭数量";

            queryForm.NewValue = "";
            queryForm.ChangeMode = "value";
            queryForm.ShowDialog();
            queryForm.OldValue = closeCount.ToString();

            decimal b = 0;
            if (queryForm.NewValue != "")
            {
                if (queryForm.NewValue == "0")
                {

                    MessageBox.Show("数量为0！", "注意"); return;
                }
                b = decimal.Parse(queryForm.NewValue);
                if (closeCount <= b)
                {

                    MessageBox.Show("不能大于或者等于未入数量！", "注意"); return;
                }
            }
            else
            {

                return;
            }

            NewFrm.Show(this);
            UpdateFinishCount(b, product_order, 0);

            this.lY_productionorder_listTableAdapter.Fill(this.lYQualityInspector.LY_productionorder_list, DateTime.Parse(this.dateTimePicker1.Text).Date, DateTime.Parse(this.dateTimePicker2.Text).Date);

            NewFrm.Hide(this);

        }

        private void lY_productionorder_listDataGridView_SelectionChanged(object sender, EventArgs e)
        {
            if (null == this.lY_productionorder_listDataGridView.CurrentRow)
            {


                this.ly_machinepart_process_workTableAdapter.Fill(this.lYMaterielRequirements.ly_machinepart_process_work, "", "");

                this.ly_production_order_detail1TableAdapter.Fill(this.lYMaterielRequirements.ly_production_order_detail1, "", 0);

                this.ly_production_order_materialrequisitionTableAdapter.Fill(this.lYMaterielRequirements.ly_production_order_materialrequisition, "");

                return;


            }

            nowplanid = int.Parse(this.lY_productionorder_listDataGridView.CurrentRow.Cells["plan_id"].Value.ToString());

            nowitemno = this.lY_productionorder_listDataGridView.CurrentRow.Cells["物料编码"].Value.ToString();

            nowproduction_order = this.lY_productionorder_listDataGridView.CurrentRow.Cells["跟单编号"].Value.ToString();
 


            this.ly_machinepart_process_workTableAdapter.Fill(this.lYMaterielRequirements.ly_machinepart_process_work, nowitemno, nowproduction_order);

            this.ly_production_order_detail1TableAdapter.Fill(this.lYMaterielRequirements.ly_production_order_detail1, nowproduction_order, 0);

            this.ly_production_order_materialrequisitionTableAdapter.Fill(this.lYMaterielRequirements.ly_production_order_materialrequisition, nowproduction_order);
        }
    }
}
