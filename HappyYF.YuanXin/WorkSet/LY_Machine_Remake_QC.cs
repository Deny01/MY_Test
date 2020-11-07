using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Transactions;
using System.Windows.Forms;
using DataGridFilter;
using HappyYF.Infrastructure.Repositories;

namespace HappyYF.YuanXin.WorkSet
{
    public partial class LY_Machine_Remake_QC : Form
    {

        string remake_style;

        public string Remake_style
        {
            get { return remake_style; }
            set { remake_style = value; }
        }

        string materialplannum;

        public string Materialplannum
        {
            get { return materialplannum; }
            set { materialplannum = value; }
        }

        string originpruductionOrdernum;

        public string OriginpruductionOrdernum
        {
            get { return originpruductionOrdernum; }
            set { originpruductionOrdernum = value; }
        }

        string originitemno;

        public string Originitemno
        {
            get { return originitemno; }
            set { originitemno = value; }
        }

        int parent_process_order;

        public int Parent_process_order
        {
            get { return parent_process_order; }
            set { parent_process_order = value; }
        }
        
        string runmode;


        public string Runmode
        {
            get { return runmode; }
            set { runmode = value; }
        }

        decimal allcan_remake_count;

        public decimal Allcan_remake_count
        {
            get { return allcan_remake_count; }
            set { allcan_remake_count = value; }
        }

        decimal have_remake_count;

        public decimal Have_remake_count
        {
            get { return have_remake_count; }
            set { have_remake_count = value; }
        }
        int now_inspectid;

        public int Now_inspectid
        {
            get { return now_inspectid; }
            set { now_inspectid = value; }
        }
        string nowinspectcode;

        public string Nowinspectcode
        {
            get { return nowinspectcode; }
            set { nowinspectcode = value; }
        }

        string nowproductorder;

        public string Nowproductorder
        {
            get { return nowproductorder; }
            set { nowproductorder = value; }
        }
        string nowordername;

        public string Nowordername
        {
            get { return nowordername; }
            set { nowordername = value; }
        }
        string nowworker;

        public string Nowworker
        {
            get { return nowworker; }
            set { nowworker = value; }
        }
        
        //////////////////////////

        string nowpremakeorderNum;
        int nowremakeid;

        public LY_Machine_Remake_QC()
        {
            InitializeComponent();
        }

        //private void ly_production_order_remakeBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        //{
        //    this.Validate();
        //    this.ly_production_order_remakeBindingSource.EndEdit();
        //    this.tableAdapterManager.UpdateAll(this.lYQualityInspector);

        //}

        private void LY_Machine_RemakeMange_Load(object sender, EventArgs e)
        {
            this.dateTimePicker1.Text = DateTime.Today.AddMonths(-3).Date.ToString();
            this.dateTimePicker2.Text = DateTime.Today.AddDays(1).Date.ToString();
            
            this.ly_production_order_remakeTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            this.ly_production_order_remakeMenageTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            this.ly_machinepart_process_sum_forremakeOrderTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            this.ly_production_order_remake_detailTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;

            SetFormState("加工" );

            
        
        }

        private void SetFormState(string state)
        {
            // view edit add save cancle

            //return;

            if ("管理" == state)
            {

                this.tabPage1.Parent = this.tabControl1;
                this.tabPage2.Parent = null;
                this.tabControl1.SelectedTab = this.tabPage1;

                //this.splitContainer2.Panel1Collapsed = false ;
                //this.splitContainer2.Panel2Collapsed = true;

                this.groupBox1.Text ="跟单:" +this.Nowproductorder+" 工序:"+this .Nowordername+"  " +this .nowworker +" 返修安排";

                this.ly_production_order_remakeMenageTableAdapter.Fill(this.lYQualityInspector.ly_production_order_remakeMenage, this .nowinspectcode);


            }
            else
            {

                this.tabPage1.Parent = null;
                this.tabPage2.Parent = this.tabControl1;
                this.tabControl1.SelectedTab = this.tabPage2;

                //this.splitContainer2.Panel1Collapsed = false;
                //this.splitContainer2.Panel2Collapsed = false ;

                this.ly_production_order_remakeTableAdapter.Fill(this.lYQualityInspector.ly_production_order_remake, DateTime.Parse(this.dateTimePicker1.Text).Date, DateTime.Parse(this.dateTimePicker2.Text).Date);



            }


        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.ly_production_order_remakeTableAdapter.Fill(this.lYQualityInspector.ly_production_order_remake, DateTime.Parse(this.dateTimePicker1.Text).Date, DateTime.Parse(this.dateTimePicker2.Text).Date);
        }

        private void ly_production_order_remakeDataGridView_SelectionChanged(object sender, EventArgs e)
        {
            if (null == this.ly_production_order_remakeDataGridView.CurrentRow)
            {

                this.ly_machinepart_process_sum_forremakeOrderTableAdapter.Fill(this.lYQualityInspector.ly_machinepart_process_sum_forremakeOrder, 0, "");
                return;
            }

            this.nowremakeid = int.Parse(this.ly_production_order_remakeDataGridView.CurrentRow.Cells["id"].Value.ToString());

            this.nowpremakeorderNum = this.ly_production_order_remakeDataGridView.CurrentRow.Cells["返修单号"].Value.ToString();
       

          

            this.ly_machinepart_process_sum_forremakeOrderTableAdapter.Fill(this.lYQualityInspector.ly_machinepart_process_sum_forremakeOrder, nowremakeid, nowpremakeorderNum);
            if (null == this.ly_machinepart_process_sum_forremakeOrderDataGridView.CurrentRow)
            {
                this.ly_production_order_remake_detailTableAdapter.Fill(this.lYQualityInspector.ly_production_order_remake_detail, "", 0);
            }
        }

        private void ly_machinepart_process_sum_forremakeOrderDataGridView_SelectionChanged(object sender, EventArgs e)
        {

            if (null == this.ly_machinepart_process_sum_forremakeOrderDataGridView.CurrentRow) return;

            //string nowpremakeorderNum = this.ly_production_order_remakeDataGridView.CurrentRow.Cells["返修单号"].Value.ToString();

            if (!string.IsNullOrEmpty(nowpremakeorderNum) && null != this.ly_machinepart_process_sum_forremakeOrderDataGridView.CurrentRow)
            {

                int nowOrder;

                if ("" != this.ly_machinepart_process_sum_forremakeOrderDataGridView.CurrentRow.Cells["顺序"].Value.ToString())
                {
                    nowOrder = int.Parse(this.ly_machinepart_process_sum_forremakeOrderDataGridView.CurrentRow.Cells["顺序"].Value.ToString());
                }
                else
                {
                    nowOrder = 0;
                }


                this.ly_production_order_remake_detailTableAdapter.Fill(this.lYQualityInspector.ly_production_order_remake_detail, nowpremakeorderNum, nowOrder);


            }
            else
            {
                this.ly_production_order_remake_detailTableAdapter.Fill(this.lYQualityInspector.ly_production_order_remake_detail, "", 0);
            }
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            DataGridView dgv;

            if (this.runmode == "管理")
            {
                dgv = this.ly_production_order_remakeMenageDataGridView;
            }
            else
            {
                dgv = this.ly_production_order_remakeDataGridView;
            
            }
            
            if (null == dgv.CurrentRow)
            {
                return;
            }

            if (null == this.ly_machinepart_process_sum_forremakeOrderDataGridView.CurrentRow) return;
            
            //string nowpremakeorderNum = this.ly_production_order_remakeDataGridView.CurrentRow.Cells["返修单号"].Value.ToString();
            string noworder = this.ly_machinepart_process_sum_forremakeOrderDataGridView.CurrentRow.Cells["顺序"].Value.ToString();




            string message1 = "确认分配(返修单：" + nowpremakeorderNum + " 工序:" + noworder + ")任务吗?";
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

                int nowrowIndex = this.ly_machinepart_process_sum_forremakeOrderDataGridView.CurrentRow.Index;
                string nowordername = this.ly_machinepart_process_sum_forremakeOrderDataGridView.CurrentRow.Cells["工序名称"].Value.ToString();

                int nowordernum = int.Parse(this.ly_machinepart_process_sum_forremakeOrderDataGridView.CurrentRow.Cells["顺序"].Value.ToString());


                if (1 != nowordernum)
                //if ("下料" != nowordername)
                {
                    if (!string.IsNullOrEmpty(this.ly_machinepart_process_sum_forremakeOrderDataGridView.Rows[nowrowIndex - 1].Cells["本序合格数量"].Value.ToString()))
                    {

                        up_quality = decimal.Parse(this.ly_machinepart_process_sum_forremakeOrderDataGridView.Rows[nowrowIndex - 1].Cells["本序合格数量"].Value.ToString());
                    }
                    else
                    {

                        up_quality = 0;
                    }

                    if (!string.IsNullOrEmpty(this.ly_machinepart_process_sum_forremakeOrderDataGridView.Rows[nowrowIndex - 1].Cells["本序回用数量"].Value.ToString()))
                    {


                        up_canuse = decimal.Parse(this.ly_machinepart_process_sum_forremakeOrderDataGridView.Rows[nowrowIndex - 1].Cells["本序回用数量"].Value.ToString());
                    }
                    else
                    {

                        up_canuse = 0;
                    }

                    if (!string.IsNullOrEmpty(this.ly_machinepart_process_sum_forremakeOrderDataGridView.Rows[nowrowIndex - 1].Cells["本序返修数量"].Value.ToString()))
                    {


                        up_remake = decimal.Parse(this.ly_machinepart_process_sum_forremakeOrderDataGridView.Rows[nowrowIndex - 1].Cells["本序返修数量"].Value.ToString());
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
                    if (!string.IsNullOrEmpty(this.ly_machinepart_process_sum_forremakeOrderDataGridView.CurrentRow.Cells["返修单数量"].Value.ToString()))
                    {
                        up_order_count = decimal.Parse(this.ly_machinepart_process_sum_forremakeOrderDataGridView.CurrentRow.Cells["返修单数量"].Value.ToString());
                    }
                    else
                    {
                        up_order_count = 0;
                    }

                }


                if (!string.IsNullOrEmpty(this.ly_machinepart_process_sum_forremakeOrderDataGridView.CurrentRow.Cells["本序加工数量"].Value.ToString()))
                {
                    order_count = decimal.Parse(this.ly_machinepart_process_sum_forremakeOrderDataGridView.CurrentRow.Cells["本序加工数量"].Value.ToString());
                }
                else
                {
                    order_count = 0;
                }



                decimal arrange_count = 0;

                if (null != this.ly_production_order_remake_detailDataGridView.CurrentRow)
                {

                    foreach (DataGridViewRow dgr in ly_production_order_remake_detailDataGridView.Rows)
                    {

                        if (string.IsNullOrEmpty(dgr.Cells["数量"].Value.ToString())) continue;
                        arrange_count = arrange_count + decimal.Parse(dgr.Cells["数量"].Value.ToString());



                    }
                }



                if (arrange_count >= up_order_count)
                {
                    MessageBox.Show("返修单数量已经全部安排,不能增加返修单任务", "注意");

                    return;
                }
                else
                {



                    ///////////////////////////////////////////////////////////////////








                    this.ly_production_order_remake_detailBindingSource.AddNew();

                    this.ly_production_order_remake_detailDataGridView.CurrentRow.Cells["外协"].Value = "False";

                    this.ly_production_order_remake_detailDataGridView.CurrentRow.Cells["安排日期"].Value = SQLDatabase.GetNowdate();

                    this.ly_production_order_remake_detailDataGridView.CurrentRow.Cells["返修单号1"].Value = this.ly_machinepart_process_sum_forremakeOrderDataGridView.CurrentRow.Cells["返修单号0"].Value.ToString();

                    this.ly_production_order_remake_detailDataGridView.CurrentRow.Cells["工序"].Value = this.ly_machinepart_process_sum_forremakeOrderDataGridView.CurrentRow.Cells["顺序"].Value;

                    this.ly_production_order_remake_detailDataGridView.CurrentRow.Cells["sequence_number1"].Value = this.ly_machinepart_process_sum_forremakeOrderDataGridView.CurrentRow.Cells["工序编号"].Value;




                    this.ly_production_order_remake_detailDataGridView.CurrentRow.Cells["数量"].Value = up_order_count - arrange_count;

                    this.ly_production_order_remake_detailDataGridView.CurrentRow.Cells["安排人"].Value = SQLDatabase.nowUserName();

                    
                   


                    SaveTask();
                }

            }
        }

        private void SaveTask()
        {

            this.ly_production_order_remake_detailDataGridView.EndEdit();

            this.Validate();
            this.ly_production_order_remake_detailBindingSource.EndEdit();

            this.ly_production_order_remake_detailTableAdapter.Update(this.lYQualityInspector.ly_production_order_remake_detail);
            ////////////////////////////////////////////////

            if (null != this.ly_production_order_remake_detailDataGridView.CurrentRow && null != this.ly_machinepart_process_sum_forremakeOrderDataGridView.CurrentRow)
            {
                int detail_Id;

                if (null != this.ly_production_order_remake_detailDataGridView.CurrentRow)
                {
                    detail_Id = int.Parse(this.ly_production_order_remake_detailDataGridView.CurrentRow.Cells["detailId"].Value.ToString());
                }
                else
                {
                    detail_Id = 0;
                }


                //int nowremakeid = int.Parse(this.ly_production_order_remakeDataGridView.CurrentRow.Cells["id"].Value.ToString());

                //string nowpremakeorderNum = this.ly_production_order_remakeDataGridView.CurrentRow.Cells["返修单号"].Value.ToString();


                int noworder;

                if ("" != this.ly_machinepart_process_sum_forremakeOrderDataGridView.CurrentRow.Cells["顺序"].Value.ToString())
                {
                    noworder =int.Parse ( this.ly_machinepart_process_sum_forremakeOrderDataGridView.CurrentRow.Cells["顺序"].Value.ToString());
                }
                else
                {
                    noworder = 0;
                }

                this.ly_machinepart_process_sum_forremakeOrderTableAdapter.Fill(this.lYQualityInspector.ly_machinepart_process_sum_forremakeOrder, nowremakeid, nowpremakeorderNum);


                this.ly_machinepart_process_sum_forremakeOrderBindingSource.Position = this.ly_machinepart_process_sum_forremakeOrderBindingSource.Find("顺序", noworder);
                this.ly_production_order_remake_detailBindingSource.Position = this.ly_production_order_remake_detailBindingSource.Find("id", detail_Id);



                //this.ly_production_order_detailTableAdapter.Fill(this.lYMaterielRequirements.ly_production_order_detail, nowproductionorderNum, nowOrder);
                /////////////////////////////////////////////////////

            }
            else
            {
               this.ly_production_order_remake_detailTableAdapter.Fill(this.lYQualityInspector.ly_production_order_remake_detail, "", 0);
            }

        }

        private void bindingNavigatorAddNewItem1_Click(object sender, EventArgs e)
        {

           

            this.have_remake_count = 0;
            
            foreach (DataGridViewRow dgr in this.ly_production_order_remakeMenageDataGridView.Rows)
            {

                if (string.IsNullOrEmpty(dgr.Cells["返修数量2"].Value.ToString())) continue;
                this.have_remake_count = this.have_remake_count + decimal.Parse(dgr.Cells["返修数量2"].Value.ToString());



            }


            if (0 >= this.allcan_remake_count - this.have_remake_count)
            {
                MessageBox.Show("可返修数量已经全部安排,不能增加返修单", "注意");
                return;
            }

            string message = "增加返修单吗？";
            string caption = "提示...";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;

            DialogResult result;



            result = MessageBox.Show(message, caption, buttons,
            MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

            if (result == DialogResult.Yes)
            {
                this.ly_production_order_remakeMenageBindingSource.AddNew();

                this.ly_production_order_remakeMenageDataGridView.CurrentRow.Cells["返修单号2"].Value = GetMaxRemakeOrder();

                this.ly_production_order_remakeMenageDataGridView.CurrentRow.Cells["安排日期2"].Value = SQLDatabase.GetNowdate().ToString(); ;

                this.ly_production_order_remakeMenageDataGridView.CurrentRow.Cells["返修类别2"].Value = this .remake_style;


                this.ly_production_order_remakeMenageDataGridView.CurrentRow.Cells["返修数量2"].Value = this.allcan_remake_count-this .have_remake_count;


                this.ly_production_order_remakeMenageDataGridView.CurrentRow.Cells["Inspection_num"].Value = this .Nowinspectcode;


                this.ly_production_order_remakeMenageDataGridView.CurrentRow.Cells["inspection_id"].Value = this .now_inspectid;

                if (this.remake_style == "返修")
                {
                    this.ly_production_order_remakeMenageDataGridView.CurrentRow.Cells["计划编号"].Value = this.materialplannum;
                    this.ly_production_order_remakeMenageDataGridView.CurrentRow.Cells["origin_pruductionOrder_num"].Value = this.originpruductionOrdernum ;
                    this.ly_production_order_remakeMenageDataGridView.CurrentRow.Cells["origin_itemno"].Value = this.originitemno;
                
                }
                

                SaveChanged();

                //this.ly_production_order_remakeMenageTableAdapter.Fill(this.lYQualityInspector.ly_production_order_remakeMenage, this.nowinspectcode);

              


            }
        }

        private string GetMaxRemakeOrder()
        {
            SqlConnection sqlConnection1 = new SqlConnection(SQLDatabase.Connectstring);
            SqlCommand cmd = new SqlCommand();

            string MaxRemakeOrder = "";

            cmd.Parameters.Add("@Production_mode", SqlDbType.VarChar);
            cmd.Parameters["@Production_mode"].Value = "FX";


            cmd.CommandText = "LY_GetMax_RemakeOrder";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = sqlConnection1;

            sqlConnection1.Open();
            MaxRemakeOrder = cmd.ExecuteScalar().ToString();
            sqlConnection1.Close();



            return MaxRemakeOrder;
        }
        private void SaveChanged()
        {

            this.ly_production_order_remakeMenageDataGridView.EndEdit();

            this.Validate();
            this.ly_production_order_remakeMenageBindingSource.EndEdit();

            

            this.ly_production_order_remakeMenageTableAdapter.Update(this.lYQualityInspector.ly_production_order_remakeMenage);

            this.ly_production_order_remakeMenageTableAdapter.Fill(this.lYQualityInspector.ly_production_order_remakeMenage, this.nowinspectcode);

            
        }

        private void 返修工艺设置ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (null == this.ly_production_order_remakeDataGridView.CurrentRow) return;





            LY_Machine_Process_forremake queryForm = new LY_Machine_Process_forremake();


            string nowremakeOrder_num = this.ly_production_order_remakeDataGridView.CurrentRow.Cells["返修单号"].Value.ToString();
            int nowremakeid = int.Parse(this.ly_production_order_remakeDataGridView.CurrentRow.Cells["id"].Value.ToString());

            queryForm.NowremakeOrder_num = nowremakeOrder_num;
            queryForm.Nowremakeid = nowremakeid;

            queryForm.StartPosition = FormStartPosition.CenterParent;
            queryForm.ShowDialog(this);




            this.ly_machinepart_process_sum_forremakeOrderTableAdapter.Fill(this.lYQualityInspector.ly_machinepart_process_sum_forremakeOrder, nowremakeid, nowremakeOrder_num);
        }

        private void ly_production_order_remakeMenageDataGridView_SelectionChanged(object sender, EventArgs e)
        {
            if (null == this.ly_production_order_remakeMenageDataGridView.CurrentRow)
            {

                this.ly_machinepart_process_sum_forremakeOrderTableAdapter.Fill(this.lYQualityInspector.ly_machinepart_process_sum_forremakeOrder, 0, "");
                return;
            }

             this.nowremakeid = int.Parse(this.ly_production_order_remakeMenageDataGridView.CurrentRow.Cells["id2"].Value.ToString());
                  
           this.nowpremakeorderNum = this.ly_production_order_remakeMenageDataGridView.CurrentRow.Cells["返修单号2"].Value.ToString();




            this.ly_machinepart_process_sum_forremakeOrderTableAdapter.Fill(this.lYQualityInspector.ly_machinepart_process_sum_forremakeOrder, nowremakeid, nowpremakeorderNum);
        }

        private void bindingNavigatorAddNewItem_Click(object sender, EventArgs e)
        {

        }

        private void bindingNavigatorDeleteItem1_Click(object sender, EventArgs e)
        {
               string message = "删除选中返修单吗？";
            string caption = "提示...";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;

            DialogResult result;



            result = MessageBox.Show(message, caption, buttons,
            MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

            if (result == DialogResult.Yes)
            {
                this.ly_production_order_remakeMenageBindingSource.RemoveCurrent();

               



                SaveChanged();


            }
        }

        private void ly_production_order_remakeMenageDataGridView_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
             if (null == this.ly_production_order_remakeMenageDataGridView.CurrentRow) return;
            
            DataGridView dgv = sender as DataGridView;




            if ("返修数量2" == dgv.CurrentCell.OwningColumn.Name)
            {

                ChangeValue queryForm = new ChangeValue();

                queryForm.OldValue = dgv.CurrentCell.Value.ToString();
                queryForm.NewValue = "";
                queryForm.ChangeMode = "value";
                queryForm.ShowDialog();




                if (queryForm.NewValue != "")
                {


                    dgv.CurrentRow.Cells["返修数量2"].Value = queryForm.NewValue;

                    this.have_remake_count = 0;

                    //int nowplanValue = int.Parse(this.lY_MaterielRequirementsDataGridView.CurrentRow.Cells["需求数量"].Value.ToString(), System.Globalization.NumberStyles.Number);


                    foreach (DataGridViewRow dgr in dgv.Rows)
                    {

                        if (string.IsNullOrEmpty(dgr.Cells["返修数量2"].Value.ToString())) continue;
                        this.have_remake_count = this.have_remake_count + decimal.Parse(dgr.Cells["返修数量2"].Value.ToString());




                    }






                    if (0 > this.allcan_remake_count - this.have_remake_count)
                    {
                        MessageBox.Show("返修数量超出可返修数量,操作取消", "注意");
                        dgv.CurrentRow.Cells["返修数量2"].Value = queryForm.OldValue; 
                        return;
                    }

                    SaveChanged();

                   
                }
            }

                    

             
        }

        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            if (null == this.ly_production_order_remake_detailDataGridView.CurrentRow) return;

            //if (this.ly_material_plan_detailDataGridView.RowCount > 0)
            //{
            //    MessageBox.Show("计划已有物料记录，不能删除(实需删除，请先删除该计划的物料记录)", "注意");
            //    return;

            //}

            decimal quality_count;

            if (!string.IsNullOrEmpty(this.ly_production_order_remake_detailDataGridView.CurrentRow.Cells["合格"].Value.ToString()))
            {

                quality_count = decimal.Parse(this.ly_production_order_remake_detailDataGridView.CurrentRow.Cells["合格"].Value.ToString());
            }
            else
            {

                quality_count = 0;
            }


            if (0 < quality_count)
            {

                MessageBox.Show("任务已有检验记录,不能修改, 操作取消", "注意");

                return;

            }

            string diaodu = this.ly_production_order_remake_detailDataGridView.CurrentRow.Cells["安排人"].Value.ToString();

            if (diaodu != SQLDatabase.nowUserName())
            {
                MessageBox.Show("请安排人:" + diaodu + "删除", "注意");
                return;
            }

            if (null == this.ly_machinepart_process_sum_forremakeOrderDataGridView.CurrentRow)
            {
                return;
            }

            string noworder = this.ly_machinepart_process_sum_forremakeOrderDataGridView.CurrentRow.Cells["工序名称"].Value.ToString();
            string nowName = this.ly_production_order_remake_detailDataGridView.CurrentRow.Cells["姓名"].Value.ToString();



            string message1 = "删除" + nowName + " (工序：" + noworder + " )任务安排，继续吗？";
            string caption1 = "提示...";
            MessageBoxButtons buttons1 = MessageBoxButtons.YesNo;

            DialogResult result1;



            result1 = MessageBox.Show(message1, caption1, buttons1,
            MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

            if (result1 == DialogResult.Yes)
            {

                this.ly_production_order_remake_detailBindingSource.RemoveCurrent();


                SaveTask();




            }
        }

        private void ly_production_order_remake_detailDataGridView_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            DataGridView dgv = sender as DataGridView;

            if (null == dgv.CurrentCell) return;
            if (null == this.ly_machinepart_process_sum_forremakeOrderDataGridView.CurrentRow) return;

            if (!checkqualityRec() && "系统管理员" != SQLDatabase.nowUserName())
            {

                MessageBox.Show("任务已有检验记录,不能修改, 操作取消", "注意");

                return;

            }
            if (!string.IsNullOrEmpty(dgv.CurrentRow.Cells["结账单号"].Value.ToString()))
            {
                MessageBox.Show("已经有结账单,不能修改, 操作取消", "注意");

                return;

            }
            if ("True" != dgv.CurrentRow.Cells["审批"].Value.ToString())
            {
                MessageBox.Show("外协单价未审批,不能增加质检记录...", "注意");
                return;

            }

            //if ("数量" == dgv.CurrentCell.OwningColumn.Name)
            //{


            //    if (!checkqualityRec() && "系统管理员" != SQLDatabase.nowUserName())
            //    {

            //        MessageBox.Show("任务已有检验记录,不能修改, 操作取消", "注意");

            //        return;

            //    }


            //    ChangeValue queryForm = new ChangeValue();

            //    queryForm.OldValue = dgv.CurrentCell.Value.ToString();
            //    queryForm.NewValue = "";
            //    queryForm.ChangeMode = "value";
            //    queryForm.ShowDialog();




            //    if (queryForm.NewValue != "")
            //    {
            //        dgv.CurrentRow.Cells["数量"].Value = queryForm.NewValue;
            //        ////////////////////////////////////////////////////////////////////////



            //        ///////////////////////////////////////////////////////////////// 

            //        decimal up_order_count;


            //        decimal up_quality;
            //        decimal up_canuse;
            //        decimal up_remake;

            //        int nowrowIndex = this.ly_machinepart_process_sum_forremakeOrderDataGridView.CurrentRow.Index;
            //        string nowordername = this.ly_machinepart_process_sum_forremakeOrderDataGridView.CurrentRow.Cells["工序名称"].Value.ToString();

            //        int nowordernum = int.Parse(this.ly_machinepart_process_sum_forremakeOrderDataGridView.CurrentRow.Cells["顺序"].Value.ToString());


            //        if (1 != nowordernum)

            //        //if ("下料" != nowordername)
            //        {
            //            if (!string.IsNullOrEmpty(this.ly_machinepart_process_sum_forremakeOrderDataGridView.Rows[nowrowIndex - 1].Cells["本序合格数量"].Value.ToString()))
            //            {


            //                up_quality = decimal.Parse(this.ly_machinepart_process_sum_forremakeOrderDataGridView.Rows[nowrowIndex - 1].Cells["本序合格数量"].Value.ToString());
            //            }
            //            else
            //            {

            //                up_quality = 0;
            //            }

            //            if (!string.IsNullOrEmpty(this.ly_machinepart_process_sum_forremakeOrderDataGridView.Rows[nowrowIndex - 1].Cells["本序回用数量"].Value.ToString()))
            //            {


            //                up_canuse = decimal.Parse(this.ly_machinepart_process_sum_forremakeOrderDataGridView.Rows[nowrowIndex - 1].Cells["本序回用数量"].Value.ToString());
            //            }
            //            else
            //            {

            //                up_canuse = 0;
            //            }

            //            ////////////////////////

            //            if (!string.IsNullOrEmpty(this.ly_machinepart_process_sum_forremakeOrderDataGridView.Rows[nowrowIndex - 1].Cells["本序返修数量"].Value.ToString()))
            //            {


            //                up_remake = decimal.Parse(this.ly_machinepart_process_sum_forremakeOrderDataGridView.Rows[nowrowIndex - 1].Cells["本序返修数量"].Value.ToString());
            //            }
            //            else
            //            {

            //                up_remake = 0;
            //            }


            //            up_order_count = up_quality + up_canuse + up_remake;





            //            //if (!string.IsNullOrEmpty(this.ly_machinepart_process_workDataGridView.Rows[nowrowIndex - 1].Cells["本序数量"].Value.ToString()))
            //            //{


            //            //    up_order_count = decimal.Parse(this.ly_machinepart_process_workDataGridView.Rows[nowrowIndex - 1].Cells["本序数量"].Value.ToString());
            //            //}
            //            //else
            //            //{

            //            //    up_order_count = 0;
            //            //}
            //        }
            //        else
            //        {
            //            if (!string.IsNullOrEmpty(this.ly_machinepart_process_sum_forremakeOrderDataGridView.CurrentRow.Cells["返修单数量"].Value.ToString()))
            //            {
            //                up_order_count = decimal.Parse(this.ly_machinepart_process_sum_forremakeOrderDataGridView.CurrentRow.Cells["返修单数量"].Value.ToString());
            //            }
            //            else
            //            {
            //                up_order_count = 0;
            //            }

            //        }


            //        //if (!string.IsNullOrEmpty(this.ly_machinepart_process_workDataGridView.CurrentRow.Cells["本序数量"].Value.ToString()))
            //        //{
            //        //    order_count = decimal.Parse(this.ly_machinepart_process_workDataGridView.CurrentRow.Cells["本序数量"].Value.ToString());
            //        //}
            //        //else
            //        //{
            //        //    order_count = 0;
            //        //}

            //        //////////////////////////////////////////////////////////

            //        decimal order_count;


            //        if (!string.IsNullOrEmpty(this.ly_machinepart_process_sum_forremakeOrderDataGridView.CurrentRow.Cells["返修单数量"].Value.ToString()))
            //        {
            //            order_count = decimal.Parse(this.ly_machinepart_process_sum_forremakeOrderDataGridView.CurrentRow.Cells["返修单数量"].Value.ToString());
            //        }
            //        else
            //        {
            //            order_count = 0;
            //        }



            //        decimal arrange_count = 0;

            //        if (null != this.ly_production_order_remakeMenageDataGridView.CurrentRow)
            //        {

            //            foreach (DataGridViewRow dgr in ly_production_order_remake_detailDataGridView.Rows)
            //            {

            //                if (string.IsNullOrEmpty(dgr.Cells["数量"].Value.ToString())) continue;
            //                arrange_count = arrange_count + decimal.Parse(dgr.Cells["数量"].Value.ToString());



            //            }
            //        }


            //        if (arrange_count > up_order_count)
            //        {

            //            dgv.CurrentRow.Cells["数量"].Value = queryForm.OldValue;

            //            MessageBox.Show("任务安排不能超过上序可用数量, 操作取消", "注意");

            //            return;

            //        }

            //        //////////////////////////////////////////////////////////////////////////
            //        SaveTask();


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


            /////////////////////////////////////////////////////////

            if ("合格" == dgv.CurrentCell.OwningColumn.Name)
            {


                ChangeValue queryForm = new ChangeValue();

                queryForm.OldValue = dgv.CurrentCell.Value.ToString();
                queryForm.NewValue = "";
                queryForm.ChangeMode = "value";
                queryForm.ShowDialog();




                if (queryForm.NewValue != "")
                {
                    dgv.CurrentRow.Cells["合格"].Value = queryForm.NewValue;


                    if (!CheckInput(dgv))
                    {
                        dgv.CurrentRow.Cells["合格"].Value = queryForm.OldValue;
                        return;
                    }



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
            if ("返修" == dgv.CurrentCell.OwningColumn.Name)
            {

                ChangeValue queryForm = new ChangeValue();

                queryForm.OldValue = dgv.CurrentCell.Value.ToString();
                queryForm.NewValue = "";
                queryForm.ChangeMode = "value";
                queryForm.ShowDialog();




                if (queryForm.NewValue != "")
                {


                    if (string.IsNullOrEmpty(queryForm.NewValue))
                    {
                        dgv.CurrentRow.Cells["返修"].Value = DBNull.Value;
                    }
                    else
                    {
                        if (decimal.Parse(queryForm.NewValue) >= 0)
                        {

                            dgv.CurrentRow.Cells["返修"].Value = queryForm.NewValue;
                        }
                    }




                    if (!Checkremake(dgv))
                    {
                        if (string.IsNullOrEmpty(queryForm.OldValue))
                        {
                            dgv.CurrentRow.Cells["返修"].Value = DBNull.Value;
                        }
                        else
                        {
                            dgv.CurrentRow.Cells["返修"].Value = queryForm.OldValue;
                        }

                        return;
                    }



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

            /////////////////////////////////////////////////////////////
            //if ("回用" == dgv.CurrentCell.OwningColumn.Name)
            //{

            //    ChangeValue queryForm = new ChangeValue();

            //    queryForm.OldValue = dgv.CurrentCell.Value.ToString();
            //    queryForm.NewValue = "";
            //    queryForm.ChangeMode = "value";
            //    queryForm.ShowDialog();




            //    if (queryForm.NewValue != "")
            //    {


            //        if (string.IsNullOrEmpty(queryForm.NewValue))
            //        {
            //            dgv.CurrentRow.Cells["回用"].Value = DBNull.Value;
            //        }
            //        else
            //        {
            //            if (decimal.Parse(queryForm.NewValue) >= 0)
            //            {

            //                dgv.CurrentRow.Cells["回用"].Value = queryForm.NewValue;
            //            }
            //        }




            //        if (!CheckCanuse(dgv))
            //        {
            //            if (string.IsNullOrEmpty(queryForm.OldValue))
            //            {
            //                dgv.CurrentRow.Cells["回用"].Value = DBNull.Value;
            //            }
            //            else
            //            {
            //                dgv.CurrentRow.Cells["回用"].Value = queryForm.OldValue;
            //            }

            //            return;
            //        }



            //        SaveTask();


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


            /////////////////////////////////////////////////////////


            /////////////////////////////////////////////////////////

            //if ("工号" == dgv.CurrentCell.OwningColumn.Name || "姓名" == dgv.CurrentCell.OwningColumn.Name)
            //{



            //    if (!checkqualityRec() && "系统管理员" != SQLDatabase.nowUserName())
            //    {

            //        MessageBox.Show("任务已有检验记录,不能修改, 操作取消", "注意");

            //        return;

            //    }

            //    string outflag = this.ly_production_order_remake_detailDataGridView.CurrentRow.Cells["外协"].Value.ToString();
            //    string sel;

            //    if ("True" == outflag)
            //    {

            //        sel = "SELECT  supplier_code as 工号, supplier_name as 姓名 FROM ly_supplier_list where sort_code='4'";
            //    }
            //    else
            //    {

            //        sel = "SELECT  work_code as 工号, worker_name as 姓名 FROM ly_worker_list";
            //    }


            //    QueryForm queryForm = new QueryForm();


            //    queryForm.Sel = sel;
            //    queryForm.Constr = SQLDatabase.Connectstring;

            //    //Set the Column Collection to the filter Table
            //    //queryForm.SetSourceColumns(this.billMainDataSet.BalanceBill.Columns);

            //    queryForm.ShowDialog();


            //    if (queryForm.Result != "")
            //    {
            //        dgv.CurrentRow.Cells["工号"].Value = queryForm.Result;
            //        //dgv.CurrentRow.Cells["discount_money"].Value = DBNull.Value;

            //        //dgv.CurrentRow.Cells["approve_flag"].Value = "False";
            //        SaveTask();


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


            ///////////////////////////////////////////////////////////

            //if ("实际准终1" == dgv.CurrentCell.OwningColumn.Name)
            //{

            //    ChangeValue queryForm = new ChangeValue();

            //    queryForm.OldValue = dgv.CurrentCell.Value.ToString();
            //    queryForm.NewValue = "";
            //    queryForm.ChangeMode = "value";
            //    queryForm.ShowDialog();




            //    if (queryForm.NewValue != "")
            //    {
            //        dgv.CurrentRow.Cells["实际准终1"].Value = queryForm.NewValue;
            //        //dgv.CurrentRow.Cells["discount_money"].Value = DBNull.Value;

            //        //dgv.CurrentRow.Cells["approve_flag"].Value = "False";
            //        SaveTask();


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


            /////////////////////////////////////////////////////////

            //if ("实际加工工时1" == dgv.CurrentCell.OwningColumn.Name)
            //{

            //    ChangeValue queryForm = new ChangeValue();

            //    queryForm.OldValue = dgv.CurrentCell.Value.ToString();
            //    queryForm.NewValue = "";
            //    queryForm.ChangeMode = "value";
            //    queryForm.ShowDialog();




            //    if (queryForm.NewValue != "")
            //    {
            //        dgv.CurrentRow.Cells["实际加工工时1"].Value = queryForm.NewValue;
            //        //dgv.CurrentRow.Cells["discount_money"].Value = DBNull.Value;

            //        //dgv.CurrentRow.Cells["approve_flag"].Value = "False";
            //        SaveTask();


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


            /////////////////////////////////////////////////////////
            /////////////////////////////////////////////////////////

            //if ("实际可用工时1" == dgv.CurrentCell.OwningColumn.Name)
            //{

            //    ChangeValue queryForm = new ChangeValue();

            //    queryForm.OldValue = dgv.CurrentCell.Value.ToString();
            //    queryForm.NewValue = "";
            //    queryForm.ChangeMode = "value";
            //    queryForm.ShowDialog();




            //    if (queryForm.NewValue != "")
            //    {
            //        dgv.CurrentRow.Cells["实际可用工时1"].Value = queryForm.NewValue;
            //        //dgv.CurrentRow.Cells["discount_money"].Value = DBNull.Value;

            //        //dgv.CurrentRow.Cells["approve_flag"].Value = "False";
            //        SaveTask();


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


            /////////////////////////////////////////////////////////

            //if ("实际废品工时1" == dgv.CurrentCell.OwningColumn.Name)
            //{

            //    ChangeValue queryForm = new ChangeValue();

            //    queryForm.OldValue = dgv.CurrentCell.Value.ToString();
            //    queryForm.NewValue = "";
            //    queryForm.ChangeMode = "value";
            //    queryForm.ShowDialog();




            //    if (queryForm.NewValue != "")
            //    {
            //        dgv.CurrentRow.Cells["实际废品工时1"].Value = queryForm.NewValue;
            //        //dgv.CurrentRow.Cells["discount_money"].Value = DBNull.Value;

            //        //dgv.CurrentRow.Cells["approve_flag"].Value = "False";
            //        SaveTask();


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


            /////////////////////////////////////////////////////////

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
                    SaveTask();


                    //CountPlanStru();

                }
                else
                {

                }
                return;

            }

            /////////////////////////////

            /////////////////////////////////////////////////////////

            //if ("安排日期" == dgv.CurrentCell.OwningColumn.Name)
            //{

            //    ChangeValue queryForm = new ChangeValue();

            //    queryForm.OldValue = dgv.CurrentCell.Value.ToString();
            //    queryForm.NewValue = "";
            //    queryForm.ChangeMode = "datetime";
            //    queryForm.ShowDialog();




            //    if (queryForm.NewValue != "")
            //    {
            //        dgv.CurrentRow.Cells["安排日期"].Value = queryForm.NewValue;
            //        //dgv.CurrentRow.Cells["discount_money"].Value = DBNull.Value;

            //        //dgv.CurrentRow.Cells["approve_flag"].Value = "False";
            //        SaveTask();


            //        //CountPlanStru();

            //    }
            //    else
            //    {

            //    }
            //    return;

            //}

            ///////////////////////////////
        }
        private bool Checkremake(DataGridView dgv)
        {
            decimal qualified_count;
            decimal inspect_count;



            if (!string.IsNullOrEmpty(dgv.CurrentRow.Cells["数量"].Value.ToString()))
            {
                inspect_count = decimal.Parse(dgv.CurrentRow.Cells["数量"].Value.ToString());
            }
            else
            {
                inspect_count = 0;
            }

            if (!string.IsNullOrEmpty(dgv.CurrentRow.Cells["合格"].Value.ToString()))
            {
                qualified_count = decimal.Parse(dgv.CurrentRow.Cells["合格"].Value.ToString());
            }
            else
            {
                qualified_count = 0;
            }



            decimal waste_count;
            decimal canuse_count;
            decimal remake_count;
            decimal have_remake_count;


            if (!string.IsNullOrEmpty(dgv.CurrentRow.Cells["废品"].Value.ToString()))
            {
                waste_count = decimal.Parse(dgv.CurrentRow.Cells["废品"].Value.ToString());
            }
            else
            {
                waste_count = 0;
            }

            if (!string.IsNullOrEmpty(dgv.CurrentRow.Cells["回用"].Value.ToString()))
            {
                canuse_count = decimal.Parse(dgv.CurrentRow.Cells["回用"].Value.ToString());
            }
            else
            {
                canuse_count = 0;
            }

            if (!string.IsNullOrEmpty(dgv.CurrentRow.Cells["返修"].Value.ToString()))
            {
                remake_count = decimal.Parse(dgv.CurrentRow.Cells["返修"].Value.ToString());
            }
            else
            {
                remake_count = 0;
            }

            if (!string.IsNullOrEmpty(dgv.CurrentRow.Cells["返修已排"].Value.ToString()))
            {
                have_remake_count = decimal.Parse(dgv.CurrentRow.Cells["返修已排"].Value.ToString());
            }
            else
            {
                have_remake_count = 0;
            }



            if (remake_count > have_remake_count)
            {
                MessageBox.Show("返修合格数量不能大于返修已排数量", "注意");

                return false;
            }
            else
            {
                dgv.CurrentRow.Cells["废品"].Value = inspect_count - (qualified_count + canuse_count + remake_count);
                dgv.CurrentRow.Cells["技术员"].Value = SQLDatabase.nowUserName();
                dgv.CurrentRow.Cells["审核日期"].Value = SQLDatabase.GetNowdate().ToString();


                return true;
            }
        }
        private bool CheckInput(DataGridView dgv)
        {
            decimal qualified_count;
            decimal inspect_count;
            decimal canuse_count;
            decimal remake_count;

            if (!string.IsNullOrEmpty(dgv.CurrentRow.Cells["返修"].Value.ToString()))
            {
                remake_count = decimal.Parse(dgv.CurrentRow.Cells["返修"].Value.ToString());
            }
            else
            {
                remake_count = 0;
            }


            if (!string.IsNullOrEmpty(dgv.CurrentRow.Cells["数量"].Value.ToString()))
            {
                inspect_count = decimal.Parse(dgv.CurrentRow.Cells["数量"].Value.ToString());
            }
            else
            {
                inspect_count = 0;
            }

            if (!string.IsNullOrEmpty(dgv.CurrentRow.Cells["合格"].Value.ToString()))
            {
                qualified_count = decimal.Parse(dgv.CurrentRow.Cells["合格"].Value.ToString());
            }
            else
            {
                qualified_count = 0;
            }

            if (!string.IsNullOrEmpty(dgv.CurrentRow.Cells["回用"].Value.ToString()))
            {
                canuse_count = decimal.Parse(dgv.CurrentRow.Cells["回用"].Value.ToString());
            }
            else
            {
                canuse_count = 0;
            }

            if ((qualified_count + canuse_count + remake_count) > inspect_count)
            {
                MessageBox.Show("合格数量不能大于送检数量", "注意");

                return false;
            }
            else
            {
                dgv.CurrentRow.Cells["废品"].Value = inspect_count - (qualified_count + canuse_count + remake_count);
                dgv.CurrentRow.Cells["质检员"].Value = SQLDatabase.nowUserName();
                dgv.CurrentRow.Cells["质检日期"].Value = SQLDatabase.GetNowdate().ToString();
                return true;
            }
        }

        private bool CheckCanuse(DataGridView dgv)
        {
            decimal qualified_count;
            decimal inspect_count;

            decimal remake_count;

            if (!string.IsNullOrEmpty(dgv.CurrentRow.Cells["返修"].Value.ToString()))
            {
                remake_count = decimal.Parse(dgv.CurrentRow.Cells["返修"].Value.ToString());
            }
            else
            {
                remake_count = 0;
            }

            if (!string.IsNullOrEmpty(dgv.CurrentRow.Cells["数量"].Value.ToString()))
            {
                inspect_count = decimal.Parse(dgv.CurrentRow.Cells["数量"].Value.ToString());
            }
            else
            {
                inspect_count = 0;
            }

            if (!string.IsNullOrEmpty(dgv.CurrentRow.Cells["合格"].Value.ToString()))
            {
                qualified_count = decimal.Parse(dgv.CurrentRow.Cells["合格"].Value.ToString());
            }
            else
            {
                qualified_count = 0;
            }



            decimal waste_count;
            decimal canuse_count;


            if (!string.IsNullOrEmpty(dgv.CurrentRow.Cells["废品"].Value.ToString()))
            {
                waste_count = decimal.Parse(dgv.CurrentRow.Cells["废品"].Value.ToString());
            }
            else
            {
                waste_count = 0;
            }

            if (!string.IsNullOrEmpty(dgv.CurrentRow.Cells["回用"].Value.ToString()))
            {
                canuse_count = decimal.Parse(dgv.CurrentRow.Cells["回用"].Value.ToString());
            }
            else
            {
                canuse_count = 0;
            }

            if (canuse_count > (inspect_count - qualified_count))
            {
                MessageBox.Show("回用数量不能大于废品数量", "注意");

                return false;
            }
            else
            {
                dgv.CurrentRow.Cells["废品"].Value = inspect_count - (qualified_count + canuse_count + remake_count);
                dgv.CurrentRow.Cells["技术员"].Value = SQLDatabase.nowUserName();
                dgv.CurrentRow.Cells["审核日期"].Value = SQLDatabase.GetNowdate().ToString();


                return true;
            }
        }

        private bool checkqualityRec()
        {
            decimal qualitied_count = 0;
            decimal waste_count = 0;

            if (null == this.ly_machinepart_process_sum_forremakeOrderDataGridView.CurrentRow)
            {

                return false;
            }


            if (!string.IsNullOrEmpty(this.ly_production_order_remake_detailDataGridView.CurrentRow.Cells["合格"].Value.ToString()))
            {
                qualitied_count = decimal.Parse(this.ly_production_order_remake_detailDataGridView.CurrentRow.Cells["合格"].Value.ToString());
            }
            else
            {
                qualitied_count = 0;
            }

            if (!string.IsNullOrEmpty(this.ly_production_order_remake_detailDataGridView.CurrentRow.Cells["废品"].Value.ToString()))
            {
                waste_count = decimal.Parse(this.ly_production_order_remake_detailDataGridView.CurrentRow.Cells["废品"].Value.ToString());
            }
            else
            {
                waste_count = 0;
            }


            if ((qualitied_count + waste_count) > 0)
            {
                string nowordername = this.ly_machinepart_process_sum_forremakeOrderDataGridView.CurrentRow.Cells["工序名称"].Value.ToString();

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
            DataGridView dgv;

            if (this.runmode == "管理")
            {
                dgv = this.ly_production_order_remakeMenageDataGridView;
            }
            else
            {
                dgv = this.ly_production_order_remakeDataGridView;

            }

            if (null == dgv.CurrentRow)
            {
                return;
            }

            if (null == this.ly_machinepart_process_sum_forremakeOrderDataGridView.CurrentRow) return;
            //string nowpremakeorderNum = this.ly_production_order_remakeDataGridView.CurrentRow.Cells["返修单号"].Value.ToString();
            string noworder = this.ly_machinepart_process_sum_forremakeOrderDataGridView.CurrentRow.Cells["顺序"].Value.ToString();




            string message1 = "确认分配(返修单：" + nowpremakeorderNum + " 工序:" + noworder + ")任务吗?";
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

                int nowrowIndex = this.ly_machinepart_process_sum_forremakeOrderDataGridView.CurrentRow.Index;
                string nowordername = this.ly_machinepart_process_sum_forremakeOrderDataGridView.CurrentRow.Cells["工序名称"].Value.ToString();

                int nowordernum = int.Parse(this.ly_machinepart_process_sum_forremakeOrderDataGridView.CurrentRow.Cells["顺序"].Value.ToString());


                if (1 != nowordernum)
                //if ("下料" != nowordername)
                {
                    if (!string.IsNullOrEmpty(this.ly_machinepart_process_sum_forremakeOrderDataGridView.Rows[nowrowIndex - 1].Cells["本序合格数量"].Value.ToString()))
                    {

                        up_quality = decimal.Parse(this.ly_machinepart_process_sum_forremakeOrderDataGridView.Rows[nowrowIndex - 1].Cells["本序合格数量"].Value.ToString());
                    }
                    else
                    {

                        up_quality = 0;
                    }

                    if (!string.IsNullOrEmpty(this.ly_machinepart_process_sum_forremakeOrderDataGridView.Rows[nowrowIndex - 1].Cells["本序回用数量"].Value.ToString()))
                    {


                        up_canuse = decimal.Parse(this.ly_machinepart_process_sum_forremakeOrderDataGridView.Rows[nowrowIndex - 1].Cells["本序回用数量"].Value.ToString());
                    }
                    else
                    {

                        up_canuse = 0;
                    }

                    if (!string.IsNullOrEmpty(this.ly_machinepart_process_sum_forremakeOrderDataGridView.Rows[nowrowIndex - 1].Cells["本序返修数量"].Value.ToString()))
                    {


                        up_remake = decimal.Parse(this.ly_machinepart_process_sum_forremakeOrderDataGridView.Rows[nowrowIndex - 1].Cells["本序返修数量"].Value.ToString());
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
                    if (!string.IsNullOrEmpty(this.ly_machinepart_process_sum_forremakeOrderDataGridView.CurrentRow.Cells["返修单数量"].Value.ToString()))
                    {
                        up_order_count = decimal.Parse(this.ly_machinepart_process_sum_forremakeOrderDataGridView.CurrentRow.Cells["返修单数量"].Value.ToString());
                    }
                    else
                    {
                        up_order_count = 0;
                    }

                }


                if (!string.IsNullOrEmpty(this.ly_machinepart_process_sum_forremakeOrderDataGridView.CurrentRow.Cells["本序加工数量"].Value.ToString()))
                {
                    order_count = decimal.Parse(this.ly_machinepart_process_sum_forremakeOrderDataGridView.CurrentRow.Cells["本序加工数量"].Value.ToString());
                }
                else
                {
                    order_count = 0;
                }



                decimal arrange_count = 0;

                if (null != this.ly_production_order_remake_detailDataGridView.CurrentRow)
                {

                    foreach (DataGridViewRow dgr in ly_production_order_remake_detailDataGridView.Rows)
                    {

                        if (string.IsNullOrEmpty(dgr.Cells["数量"].Value.ToString())) continue;
                        arrange_count = arrange_count + decimal.Parse(dgr.Cells["数量"].Value.ToString());



                    }
                }



                if (arrange_count >= up_order_count)
                {
                    MessageBox.Show("返修单数量已经全部安排,不能增加返修单任务", "注意");

                    return;
                }
                else
                {



                    ///////////////////////////////////////////////////////////////////








                    this.ly_production_order_remake_detailBindingSource.AddNew();

                    this.ly_production_order_remake_detailDataGridView.CurrentRow.Cells["外协"].Value = "True";

                    this.ly_production_order_remake_detailDataGridView.CurrentRow.Cells["安排日期"].Value = SQLDatabase.GetNowdate();

                    this.ly_production_order_remake_detailDataGridView.CurrentRow.Cells["返修单号1"].Value = this.ly_machinepart_process_sum_forremakeOrderDataGridView.CurrentRow.Cells["返修单号0"].Value.ToString();

                    this.ly_production_order_remake_detailDataGridView.CurrentRow.Cells["工序"].Value = this.ly_machinepart_process_sum_forremakeOrderDataGridView.CurrentRow.Cells["顺序"].Value;

                    this.ly_production_order_remake_detailDataGridView.CurrentRow.Cells["sequence_number1"].Value = this.ly_machinepart_process_sum_forremakeOrderDataGridView.CurrentRow.Cells["工序编号"].Value;




                    this.ly_production_order_remake_detailDataGridView.CurrentRow.Cells["数量"].Value = up_order_count - arrange_count;

                    this.ly_production_order_remake_detailDataGridView.CurrentRow.Cells["安排人"].Value = SQLDatabase.nowUserName();





                    SaveTask();
                }

            }
        }

        private void 返修业务管理ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (null == this.ly_production_order_remake_detailDataGridView.CurrentRow) return;

            LY_Machine_RemakeMange queryForm = new LY_Machine_RemakeMange();



            decimal qualified_count;
            decimal inspect_count;



            if (!string.IsNullOrEmpty(ly_production_order_remake_detailDataGridView.CurrentRow.Cells["数量"].Value.ToString()))
            {
                inspect_count = decimal.Parse(ly_production_order_remake_detailDataGridView.CurrentRow.Cells["数量"].Value.ToString());
            }
            else
            {
                inspect_count = 0;
            }

            if (!string.IsNullOrEmpty(ly_production_order_remake_detailDataGridView.CurrentRow.Cells["合格"].Value.ToString()))
            {
                qualified_count = decimal.Parse(ly_production_order_remake_detailDataGridView.CurrentRow.Cells["合格"].Value.ToString());
            }
            else
            {
                qualified_count = 0;
            }



            decimal waste_count;
            decimal canuse_count;


            if (!string.IsNullOrEmpty(ly_production_order_remake_detailDataGridView.CurrentRow.Cells["废品"].Value.ToString()))
            {
                waste_count = decimal.Parse(ly_production_order_remake_detailDataGridView.CurrentRow.Cells["废品"].Value.ToString());
            }
            else
            {
                waste_count = 0;
            }

            if (!string.IsNullOrEmpty(ly_production_order_remake_detailDataGridView.CurrentRow.Cells["回用"].Value.ToString()))
            {
                canuse_count = decimal.Parse(ly_production_order_remake_detailDataGridView.CurrentRow.Cells["回用"].Value.ToString());
            }
            else
            {
                canuse_count = 0;
            }

            decimal have_remake_count;
            if (!string.IsNullOrEmpty(ly_production_order_remake_detailDataGridView.CurrentRow.Cells["返修已排"].Value.ToString()))
            {
                have_remake_count = decimal.Parse(ly_production_order_remake_detailDataGridView.CurrentRow.Cells["返修已排"].Value.ToString());
            }
            else
            {
                have_remake_count = 0;
            }

            decimal remake_count;
            if (!string.IsNullOrEmpty(ly_production_order_remake_detailDataGridView.CurrentRow.Cells["返修"].Value.ToString()))
            {
                remake_count = decimal.Parse(ly_production_order_remake_detailDataGridView.CurrentRow.Cells["返修"].Value.ToString());
            }
            else
            {
                remake_count = 0;
            }

            int nowinspectionId;

            if (!string.IsNullOrEmpty(ly_production_order_remake_detailDataGridView.CurrentRow.Cells["detailId"].Value.ToString()))
            {
                nowinspectionId = int.Parse(ly_production_order_remake_detailDataGridView.CurrentRow.Cells["detailId"].Value.ToString());
            }
            else
            {
                nowinspectionId = 0;
            }

            string nowinspectionNum;

            if (!string.IsNullOrEmpty(ly_production_order_remake_detailDataGridView.CurrentRow.Cells["返修单号1"].Value.ToString()))
            {
                nowinspectionNum = ly_production_order_remake_detailDataGridView.CurrentRow.Cells["返修单号1"].Value.ToString();
            }
            else
            {
                nowinspectionNum = "";
            }

            //string originpruductionOrdernum;

            //if (!string.IsNullOrEmpty(lY_productionorder_listDataGridView.CurrentRow.Cells["跟单编号2"].Value.ToString()))
            //{
            //    originpruductionOrdernum = lY_productionorder_listDataGridView.CurrentRow.Cells["跟单编号2"].Value.ToString();
            //}
            //else
            //{
            //    originpruductionOrdernum = "";
            //}

            //if (0 >= waste_count)
            //{
            //    MessageBox.Show("本序无废品,无须增加返修单...", "注意");
            //    return;


            //}
            string nowplannum = this.ly_production_order_remakeDataGridView.CurrentRow.Cells["计划编号0"].Value.ToString();
            string nowproductionorderNum = this.ly_production_order_remakeDataGridView.CurrentRow.Cells["跟单编号0"].Value.ToString();
            string nowitemno = this.ly_production_order_remakeDataGridView.CurrentRow.Cells["物料编号0"].Value.ToString();

            string now_order_name = this.ly_machinepart_process_sum_forremakeOrderDataGridView.CurrentRow.Cells["工序名称"].Value.ToString();
            string now_worker = this.ly_production_order_remake_detailDataGridView.CurrentRow.Cells["姓名"].Value.ToString();

            queryForm.Runmode = "管理";
            queryForm.Remake_style = "返修";
            queryForm.Now_inspectid = nowinspectionId;
            queryForm.Nowinspectcode = nowinspectionNum;


            //queryForm.Parent_process_order = now_parent_process_order;
            queryForm.Materialplannum = nowplannum;
            queryForm.OriginpruductionOrdernum = nowproductionorderNum;
            queryForm.Originitemno = nowitemno;

            queryForm.Nowproductorder = nowproductionorderNum;
            queryForm.Nowordername = now_order_name;
            queryForm.Nowworker = now_worker;

            queryForm.Allcan_remake_count = inspect_count - qualified_count - canuse_count;
            queryForm.Have_remake_count = have_remake_count;

            queryForm.StartPosition = FormStartPosition.CenterParent;
            queryForm.ShowDialog();



            this.nowremakeid = int.Parse(this.ly_production_order_remakeDataGridView.CurrentRow.Cells["id"].Value.ToString());

            this.nowpremakeorderNum = this.ly_production_order_remakeDataGridView.CurrentRow.Cells["返修单号"].Value.ToString();
        }

        private void toolStripButton12_Click(object sender, EventArgs e)
        {
            if (null == this.ly_machinepart_process_sum_forremakeOrderDataGridView.CurrentRow) return;



            NewFrm.Show(this); ;

            //this.ly_plan_getmaterialTableAdapter.Fill(this.lYPlanMange.ly_plan_getmaterial, parentId);

            BaseReportView queryForm = new BaseReportView();

            queryForm.Text = "中原精密返修单";

            queryForm.Printdata = this.lYQualityInspector; 

            queryForm.PrintCrystalReport = new LY_Fanxiudan();


            //string selectFormula;

            //selectFormula = "{ly_store_planitemcount.状态}  =   '原料'  and {ly_store_planitemcount.欠料金额}>0 ";
            //queryForm.PrintCrystalReport.DataDefinition.RecordSelectionFormula = selectFormula;

            NewFrm.Hide(this);

            queryForm.ShowDialog();
        }

     
       

        private void toolStripTextBox2_KeyUp(object sender, KeyEventArgs e)
        {
            string filterString = GetDataGridviewMultiFilterString.DGVMultiFilterString(this.ly_production_order_remakeDataGridView, this.toolStripTextBox2.Text);



            if (null == filterString)
                filterString = "";

            this.ly_production_order_remakeBindingSource.Filter = filterString;
        }

        private void toolStripTextBox2_Enter(object sender, EventArgs e)
        {
            toolStripTextBox2.Text = "";

            this.ly_production_order_remakeBindingSource.Filter = "";
        }

       //////////////////////////////////////////////////////////////////

      
    }
}
