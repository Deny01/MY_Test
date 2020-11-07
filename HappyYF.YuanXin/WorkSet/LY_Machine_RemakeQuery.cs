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
    public partial class LY_Machine_RemakeQuery : Form
    {

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


        public LY_Machine_RemakeQuery()
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

            SetFormState(this .runmode );

            
        
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
                this.splitContainer2.Panel2Collapsed = true;

                this.groupBox1.Text ="跟单:" +this.Nowproductorder+" 工序:"+this .Nowordername+"  " +this .nowworker +" 返修安排";

                this.ly_production_order_remakeMenageTableAdapter.Fill(this.lYQualityInspector.ly_production_order_remakeMenage, this .nowinspectcode);


            }
            else
            {

                this.tabPage1.Parent = null;
                this.tabPage2.Parent =this.tabControl1;
                this.tabControl1.SelectedTab = this.tabPage2;

                //this.splitContainer2.Panel1Collapsed = false;
                this.splitContainer2.Panel2Collapsed = false ;

                this.ly_production_order_remakeTableAdapter.Fill(this.lYQualityInspector.ly_production_order_remake, DateTime.Parse(this.dateTimePicker1.Text).Date, DateTime.Parse(this.dateTimePicker2.Text).Date);



            }


        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void ly_production_order_remakeDataGridView_SelectionChanged(object sender, EventArgs e)
        {
            if (null == this.ly_production_order_remakeDataGridView.CurrentRow)
            {

                this.ly_machinepart_process_sum_forremakeOrderTableAdapter.Fill(this.lYQualityInspector.ly_machinepart_process_sum_forremakeOrder, 0, "");
                return;
            }

            int nowremakeid = int.Parse(this.ly_production_order_remakeDataGridView.CurrentRow.Cells["id"].Value.ToString());

            string nowpremakeorderNum = this.ly_production_order_remakeDataGridView.CurrentRow.Cells["返修单号"].Value.ToString();
       

          

            this.ly_machinepart_process_sum_forremakeOrderTableAdapter.Fill(this.lYQualityInspector.ly_machinepart_process_sum_forremakeOrder, nowremakeid, nowpremakeorderNum);
        }

        private void ly_machinepart_process_sum_forremakeOrderDataGridView_SelectionChanged(object sender, EventArgs e)
        {

            if (null == this.ly_production_order_remakeDataGridView.CurrentRow) return;

            string nowpremakeorderNum = this.ly_production_order_remakeDataGridView.CurrentRow.Cells["返修单号"].Value.ToString();

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
            if (null == this.ly_production_order_remakeDataGridView.CurrentRow)
            {
                return;
            }
            
            string nowpremakeorderNum = this.ly_production_order_remakeDataGridView.CurrentRow.Cells["返修单号"].Value.ToString();
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

                    if (!string.IsNullOrEmpty(this.ly_machinepart_process_sum_forremakeOrderDataGridView.Rows[nowrowIndex - 1].Cells["本序可用数量"].Value.ToString()))
                    {


                        up_canuse = decimal.Parse(this.ly_machinepart_process_sum_forremakeOrderDataGridView.Rows[nowrowIndex - 1].Cells["本序可用数量"].Value.ToString());
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
                    if (!string.IsNullOrEmpty(this.ly_production_order_remakeDataGridView.CurrentRow.Cells["返修数量"].Value.ToString()))
                    {
                        up_order_count = decimal.Parse(this.ly_production_order_remakeDataGridView.CurrentRow.Cells["返修数量"].Value.ToString());
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

                    this.ly_production_order_remake_detailDataGridView.CurrentRow.Cells["sequence_number1"].Value = this.ly_machinepart_process_sum_forremakeOrderDataGridView.CurrentRow.Cells["sequence_number"].Value;




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

            if (null != this.ly_production_order_remakeDataGridView.CurrentRow && null != this.ly_machinepart_process_sum_forremakeOrderDataGridView.CurrentRow)
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


                int nowremakeid = int.Parse(this.ly_production_order_remakeDataGridView.CurrentRow.Cells["id"].Value.ToString());

                string nowpremakeorderNum = this.ly_production_order_remakeDataGridView.CurrentRow.Cells["返修单号"].Value.ToString();


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

                this.ly_production_order_remakeMenageDataGridView.CurrentRow.Cells["返修类别2"].Value = "序检";


                this.ly_production_order_remakeMenageDataGridView.CurrentRow.Cells["返修数量2"].Value = this.allcan_remake_count-this .have_remake_count;


                this.ly_production_order_remakeMenageDataGridView.CurrentRow.Cells["Inspection_num"].Value = this .Nowinspectcode;


                this.ly_production_order_remakeMenageDataGridView.CurrentRow.Cells["inspection_id"].Value = this .now_inspectid;


                

                SaveChanged();

                this.ly_production_order_remakeMenageTableAdapter.Fill(this.lYQualityInspector.ly_production_order_remakeMenage, this.nowinspectcode);

              


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

            
        }

        private void 返修工艺设置ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (null == this.ly_production_order_remakeMenageDataGridView.CurrentRow) return;





            LY_Machine_Process_forremake queryForm = new LY_Machine_Process_forremake();


            string nowremakeOrder_num = this.ly_production_order_remakeMenageDataGridView.CurrentRow.Cells["返修单号2"].Value.ToString();
            int nowremakeid = int.Parse(this.ly_production_order_remakeMenageDataGridView.CurrentRow.Cells["id2"].Value.ToString());

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

            int nowremakeid = int.Parse(this.ly_production_order_remakeMenageDataGridView.CurrentRow.Cells["id2"].Value.ToString());

            string nowpremakeorderNum = this.ly_production_order_remakeMenageDataGridView.CurrentRow.Cells["返修单号2"].Value.ToString();




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

       


      
    }
}
