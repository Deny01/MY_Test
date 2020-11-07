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
    public partial class LY_QualityInspection_Elecwork : Form
    {
        public LY_QualityInspection_Elecwork()
        {
            InitializeComponent();
        }

        private void LY_QualityInspection_Benchwork_Load(object sender, EventArgs e)
        {
            this.dateTimePicker1.Text = DateTime.Today.AddMonths(-6).Date.ToString();
            this.dateTimePicker2.Text = DateTime.Today.AddDays(0).Date.ToString();

            this.ly_production_task_inspectionBindingSource.Filter = "提交=1";
           
            this.ly_production_task_inspectionTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;

 

            this.ly_production_task_inspectionTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;

            ly_production_task_returnSingleTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            ly_production_task_requestSingleTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            ly_production_task_waste_TableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;


            this.lY_productiontask_periodTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            this.lY_productiontask_periodTableAdapter.Fill(this.lYProductMange.LY_productiontask_period, this.dateTimePicker1.Value, this.dateTimePicker2.Value.AddDays(1), "DZ", SQLDatabase.NowUserID);
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.lY_productiontask_periodTableAdapter.Fill(this.lYProductMange.LY_productiontask_period, this.dateTimePicker1.Value, this.dateTimePicker2.Value.AddDays(1), "DZ", SQLDatabase.NowUserID);
           
        }

        private void toolStripTextBox2_KeyUp(object sender, KeyEventArgs e)
        {
            string dFilter = "";

            dFilter = GetDataGridviewMultiFilterString.DGVMultiFilterString(this.ly_production_orderDataGridView, this.toolStripTextBox2.Text);

            if (null == dFilter)
                dFilter = "";

            this.lY_productiontask_periodBindingSource.Filter = dFilter;
        }

        private void toolStripTextBox2_Enter(object sender, EventArgs e)
        {
            toolStripTextBox2.Text = "";

            this.lY_productiontask_periodBindingSource.Filter = "";
        }

        private void ly_production_orderDataGridView_SelectionChanged(object sender, EventArgs e)
        {
            reLoad();


        }
        protected void reLoad()
        {
            if (null != this.ly_production_orderDataGridView.CurrentRow)
            {

                string nowproductionorderNum = this.ly_production_orderDataGridView.CurrentRow.Cells["任务单号"].Value.ToString();

                this.ly_production_task_inspectionTableAdapter.Fill(this.lYProductMange.ly_production_task_inspection, nowproductionorderNum);

                ly_production_task_returnSingleTableAdapter.Fill(this.lYProductMange.ly_production_task_returnSingle, nowproductionorderNum);
                ly_production_task_requestSingleTableAdapter.Fill(this.lYProductMange.ly_production_task_requestSingle, nowproductionorderNum);
                ly_production_task_waste_TableAdapter.Fill(this.lYProductMange.ly_production_task_waste_, nowproductionorderNum);

            }
            else
            {

                ly_production_task_returnSingleTableAdapter.Fill(this.lYProductMange.ly_production_task_returnSingle, "");
                ly_production_task_requestSingleTableAdapter.Fill(this.lYProductMange.ly_production_task_requestSingle, "");
                ly_production_task_waste_TableAdapter.Fill(this.lYProductMange.ly_production_task_waste_, "");
                this.ly_production_task_inspectionTableAdapter.Fill(this.lYProductMange.ly_production_task_inspection, "");
            }

        }

        private void bindingNavigatorAddNewItem_Click(object sender, EventArgs e)
        {


            if (null == this.ly_production_orderDataGridView.CurrentRow) return;

            string message = "增加质检记录吗？";
            string caption = "提示...";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;

            DialogResult result;



            result = MessageBox.Show(message, caption, buttons,
            MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

            if (result == DialogResult.Yes)
            {


                decimal need_inspect_count;
                decimal send_inspect_count;




                if (!string.IsNullOrEmpty(this.ly_production_orderDataGridView.CurrentRow.Cells["加工数量"].Value.ToString()))
                {
                    need_inspect_count = decimal.Parse(this.ly_production_orderDataGridView.CurrentRow.Cells["加工数量"].Value.ToString());
                }
                else
                {
                    need_inspect_count = 0;
                }


                send_inspect_count = this.ly_production_task_inspectionDataGridView.RowCount;



                if (send_inspect_count >= need_inspect_count)
                {
                    MessageBox.Show("任务单装配数量已经全部送交,不能增加质检记录", "注意");

                    return;
                }
                else
                {

                    this.ly_production_task_inspectionBindingSource.AddNew();

                    this.ly_production_task_inspectionDataGridView.CurrentRow.Cells["质检单号"].Value = GetMaxTaskInspection();

                    string nowproductionTaskNum = "";

                    nowproductionTaskNum = this.ly_production_orderDataGridView.CurrentRow.Cells["任务单号"].Value.ToString();

                    this.ly_production_task_inspectionDataGridView.CurrentRow.Cells["任务单号1"].Value = nowproductionTaskNum;

                    this.ly_production_task_inspectionDataGridView.CurrentRow.Cells["质检日期"].Value = SQLDatabase.GetNowdate().ToString(); ;

                    this.ly_production_task_inspectionDataGridView.CurrentRow.Cells["质检人"].Value = SQLDatabase.nowUserName();

                    this.ly_production_task_inspectionDataGridView.CurrentRow.Cells["质检次数"].Value = 1;

                    this.ly_production_task_inspectionDataGridView.CurrentRow.Cells["初检"].Value = "True";


                 
                }






                SaveChanged();




            }
        }
        private void SaveChanged()
        {

            this.ly_production_task_inspectionDataGridView.EndEdit();

            this.Validate();
            this.ly_production_task_inspectionBindingSource.EndEdit();

            this.ly_production_task_inspectionTableAdapter.Update(this.lYProductMange.ly_production_task_inspection);

            //string nowinspectionTaskNum = this.ly_production_task_inspectionDataGridView.CurrentRow.Cells["质检单号"].Value.ToString();

            string nowproductionTaskNum = this.ly_production_orderDataGridView.CurrentRow.Cells["任务单号"].Value.ToString();

            this.ly_production_orderDataGridView.SelectionChanged -= ly_production_orderDataGridView_SelectionChanged;
            this.lY_productiontask_periodTableAdapter.Fill(this.lYProductMange.LY_productiontask_period, this.dateTimePicker1.Value, this.dateTimePicker2.Value.AddDays(1), "DZ", SQLDatabase.NowUserID);

            this.lY_productiontask_periodBindingSource.Position = this.lY_productiontask_periodBindingSource.Find("跟单编号", nowproductionTaskNum);
            this.ly_production_orderDataGridView.SelectionChanged += ly_production_orderDataGridView_SelectionChanged;

            
            
            //this.ly_production_task_inspectionBindingSource.Position = this.ly_production_task_inspectionBindingSource.Find("质检单号", nowinspectionTaskNum);




        }

        private string GetMaxTaskInspection()
        {


            SqlConnection sqlConnection1 = new SqlConnection(SQLDatabase.Connectstring);
            SqlCommand cmd = new SqlCommand();

            string MaxInspectionNum = "";

            cmd.Parameters.Add("@Production_mode", SqlDbType.VarChar);
            cmd.Parameters["@Production_mode"].Value = "QCDZ";


            cmd.CommandText = "LY_GetMax_TaskInspectionNum";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = sqlConnection1;

            sqlConnection1.Open();
            MaxInspectionNum = cmd.ExecuteScalar().ToString();
            sqlConnection1.Close();



            return MaxInspectionNum;
        }

        private void bindingNavigatorDeleteItem_Click(object sender, EventArgs e)
        {
            if (null == this.ly_production_task_inspectionDataGridView.CurrentRow) return;

            if ("True" == ly_production_task_inspectionDataGridView.CurrentRow.Cells["入库"].Value.ToString())
            {
                MessageBox.Show("已经入库，不能删除(实需删除，请先删除该质检单号的入库记录)", "注意");
                return;

            }

          

            string inspector = this.ly_production_task_inspectionDataGridView.CurrentRow.Cells["质检人"].Value.ToString();

            if (inspector != SQLDatabase.nowUserName())
            {
                MessageBox.Show("请质检员:" + inspector + "删除", "注意");
                return;
            }



            string message1 = "当前质检记录将被删除，继续吗？";
            string caption1 = "提示...";
            MessageBoxButtons buttons1 = MessageBoxButtons.YesNo;

            DialogResult result1;



            result1 = MessageBox.Show(message1, caption1, buttons1,
            MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

            if (result1 == DialogResult.Yes)
            {

                this.ly_production_task_inspectionBindingSource.RemoveCurrent();



                SaveChanged();




            }
        }

        private void ly_production_task_inspectionDataGridView_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {


            DataGridView dgv = sender as DataGridView;

            if ("True" == dgv.CurrentRow.Cells["入库"].Value.ToString())
            {
                MessageBox.Show("已经入库，不能修改(实需修改，请先删除该质检单号的入库记录)", "注意");
                return;

            }

            //string inspector = this.ly_production_task_inspectionDataGridView.CurrentRow.Cells["质检人"].Value.ToString();

            //if (inspector != SQLDatabase.nowUserName())
            //{
            //    MessageBox.Show("请质检员:" + inspector + "修改", "注意");
            //    return;
            //}


            if ("合格" == dgv.CurrentCell.OwningColumn.Name)
            {

                if (string.IsNullOrEmpty(dgv.CurrentRow.Cells["设备编号"].Value.ToString()))
                {
                    MessageBox.Show("无设备编号,不能设置合格标记...", "注意");
                    return;
                
                }
                
                
                if ("True" == dgv.CurrentRow.Cells["合格"].Value.ToString())
                {
                    dgv.CurrentRow.Cells["合格"].Value = "False";
                    dgv.CurrentRow.Cells["质检人"].Value = DBNull.Value;
                    dgv.CurrentRow.Cells["质检日期"].Value = DBNull.Value;
                    dgv.CurrentRow.Cells["质检单号"].Value = DBNull.Value;
                }
                else
                {


                    dgv.CurrentRow.Cells["合格"].Value = "True";
                    dgv.CurrentRow.Cells["质检人"].Value = SQLDatabase.nowUserName();
                    dgv.CurrentRow.Cells["质检日期"].Value = SQLDatabase.GetNowdate();
                    dgv.CurrentRow.Cells["质检单号"].Value = GetMaxTaskInspection();

                    if (!string.IsNullOrEmpty(dgv.CurrentRow.Cells["质检次数"].Value.ToString()))
                    {

                        dgv.CurrentRow.Cells["质检次数"].Value = int.Parse(dgv.CurrentRow.Cells["质检次数"].Value.ToString()) + 1;
                    }
                    else
                    {
                        dgv.CurrentRow.Cells["质检次数"].Value = 1;
                    }
                }



                SaveChanged();




                return;

            }
            ////////////////////////////////////////////////////////////////////////





            if ("质检日期" == dgv.CurrentCell.OwningColumn.Name)
            {

                ChangeValue queryForm = new ChangeValue();

                queryForm.OldValue = dgv.CurrentCell.Value.ToString();
                queryForm.NewValue = "";
                queryForm.ChangeMode = "datetime";
                queryForm.ShowDialog();




                if (queryForm.NewValue != "")
                {
                    dgv.CurrentRow.Cells["质检日期"].Value = queryForm.NewValue;

                }
                else
                {

                    dgv.CurrentRow.Cells["质检日期"].Value = DBNull.Value;
                }

                SaveChanged();
                return;



            }
            ///////////////////////////////////////////////////////
            if ("质检次数" == dgv.CurrentCell.OwningColumn.Name)
            {

                ChangeValue queryForm = new ChangeValue();

                queryForm.OldValue = dgv.CurrentCell.Value.ToString();
                queryForm.NewValue = "";
                queryForm.ChangeMode = "value";
                queryForm.ShowDialog();




                if (queryForm.NewValue != "")
                {
                    dgv.CurrentRow.Cells["质检次数"].Value = queryForm.NewValue;


                    if (int.Parse(queryForm.NewValue) == 1)
                    {
                        dgv.CurrentRow.Cells["初检"].Value = "True";
                    }
                    else
                    {
                        dgv.CurrentRow.Cells["初检"].Value = "False";
                    }



                }
                else
                {
                    dgv.CurrentRow.Cells["质检次数"].Value = DBNull.Value;
                    dgv.CurrentRow.Cells["初检"].Value = "False";

                }

                SaveChanged();
                return;

            }

            ///////////////////////////////////////////////////////


            ///////////////////////////////////////////////////////

            if ("设备编号" == dgv.CurrentCell.OwningColumn.Name)
            {

                ChangeValue queryForm = new ChangeValue();

                queryForm.OldValue = dgv.CurrentCell.Value.ToString();
                queryForm.NewValue = "";
                queryForm.ChangeMode = "string";
                queryForm.ShowDialog();




                if (queryForm.NewValue != "")
                {
                    dgv.CurrentRow.Cells["设备编号"].Value = queryForm.NewValue;


                }
                else
                {
                    dgv.CurrentRow.Cells["设备编号"].Value = DBNull.Value;
                }

                SaveChanged();
                return;

            }






            ///////////////////////////////////////////////////////

            if ("质检意见" == dgv.CurrentCell.OwningColumn.Name)
            {

                ChangeValue queryForm = new ChangeValue();

                queryForm.OldValue = dgv.CurrentCell.Value.ToString();
                queryForm.NewValue = "";
                queryForm.ChangeMode = "longstring";
                queryForm.ShowDialog();




                if (queryForm.NewValue != "")
                {
                    dgv.CurrentRow.Cells["质检意见"].Value = queryForm.NewValue;


                }
                else
                {
                    dgv.CurrentRow.Cells["质检意见"].Value = DBNull.Value;
                }

                SaveChanged();
                return;

            }
        }

        private void 全部合格设置ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (null == ly_production_task_inspectionDataGridView.CurrentRow) return;




            int j = 1;

            for (int i = 0; i < ly_production_task_inspectionDataGridView.RowCount; i++)
            {
                if (!string.IsNullOrEmpty(ly_production_task_inspectionDataGridView.Rows[i].Cells["设备编号"].Value.ToString()))
                {



                    if ("True" == ly_production_task_inspectionDataGridView.Rows[i].Cells["合格"].Value.ToString())
                    {
                        j = j + 1;
                    }
                    else
                    {

                        ly_production_task_inspectionDataGridView.Rows[i].Cells["合格"].Value = "True";
                        ly_production_task_inspectionDataGridView.Rows[i].Cells["质检人"].Value = SQLDatabase.nowUserName();
                        ly_production_task_inspectionDataGridView.Rows[i].Cells["质检日期"].Value = SQLDatabase.GetNowdate();
                        ly_production_task_inspectionDataGridView.Rows[i].Cells["质检单号"].Value = GetMaxTaskInspection();

                        if (!string.IsNullOrEmpty(ly_production_task_inspectionDataGridView.Rows[i].Cells["质检次数"].Value.ToString()))
                        {

                            ly_production_task_inspectionDataGridView.Rows[i].Cells["质检次数"].Value = int.Parse(ly_production_task_inspectionDataGridView.Rows[i].Cells["质检次数"].Value.ToString()) + 1;
                        }
                        else
                        {
                            ly_production_task_inspectionDataGridView.Rows[i].Cells["质检次数"].Value = 1;
                        }

                        this.ly_production_task_inspectionDataGridView.EndEdit();

                        this.Validate();
                        this.ly_production_task_inspectionBindingSource.EndEdit();

                        this.ly_production_task_inspectionTableAdapter.Update(this.lYProductMange.ly_production_task_inspection);
                    }



                    j = j + 1;
                }
                else
                {

                    j = j + 1;
                }

                // SaveChanged();


            }
            SaveChanged();
        }



        private void ly_production_task_requestSingleDataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView dgv = sender as DataGridView;

            if (this.ly_production_task_requestSingleDataGridView.CurrentRow.Cells["领料审批人"].Value != null)
            {
                string inspector = this.ly_production_task_requestSingleDataGridView.CurrentRow.Cells["领料审批人"].Value.ToString();

                if (inspector.Trim() != "")
                {
                    if (inspector != SQLDatabase.nowUserName())
                    {
                        MessageBox.Show("请质检员:" + inspector + "修改", "注意");
                        return;
                    }
                }
            }



            if ("领料审批" == dgv.CurrentCell.OwningColumn.Name)
            {
                ChangeValue queryForm = new ChangeValue();
                queryForm.Text = "审批意见";
                queryForm.ChangeMode = "longstring";
                queryForm.OldValue = dgv.CurrentRow.Cells["审批意见"].Value.ToString();
                queryForm.NewValue = "";
                queryForm.ShowDialog();




                string id = this.ly_production_task_requestSingleDataGridView.CurrentRow.Cells["领料id"].Value.ToString();
                string sql = "";
                if ("True" == dgv.CurrentRow.Cells["领料审批"].Value.ToString())
                {

                    sql = @"update  ly_production_task_requestSingle  set   material_flag  = 0 , approve_people  =null  , approve_date  =null  ,approve_remark=null where  id=" + id;
                }
                else
                {
                    sql = @"update  ly_production_task_requestSingle  set   material_flag  =1 , approve_people  ='" + SQLDatabase.nowUserName() + "'  , approve_date  =GETDATE() ,approve_remark='" + queryForm.NewValue + "'  where  id=" + id;

                }

                using (SqlConnection con = new SqlConnection(SQLDatabase.Connectstring))
                {

                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {

                        con.Open();
                        cmd.ExecuteNonQuery();
                    }
                }

                reLoad();
                return;

            }
        }

        private void ly_production_task_returnSingleDataGridView_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            DataGridView dgv = sender as DataGridView;

            if (this.ly_production_task_returnSingleDataGridView.CurrentRow.Cells["退料审批人"].Value != null)
            {
                string inspector = this.ly_production_task_returnSingleDataGridView.CurrentRow.Cells["退料审批人"].Value.ToString();

                if (inspector.Trim() != "")
                {
                    if (inspector != SQLDatabase.nowUserName())
                    {
                        MessageBox.Show("请质检员:" + inspector + "修改", "注意");
                        return;
                    }
                }
            }



            if ("退料审批" == dgv.CurrentCell.OwningColumn.Name)
            {
                ChangeValue queryForm = new ChangeValue();
                queryForm.Text = "审批意见";
                queryForm.ChangeMode = "longstring";
                queryForm.OldValue = dgv.CurrentRow.Cells["审批意见2"].Value.ToString();
                queryForm.NewValue = "";
                queryForm.ShowDialog();


                string id = this.ly_production_task_returnSingleDataGridView.CurrentRow.Cells["退料编号"].Value.ToString();
                string sql = "";
                if ("True" == dgv.CurrentRow.Cells["退料审批"].Value.ToString())
                {

                    sql = @"update  ly_production_task_returnSingle  set   material_flag  = 0 , approve_people  =null  , approve_date  =null,approve_remark=null where  id=" + id;
                }
                else
                {
                    sql = @"update  ly_production_task_returnSingle  set   material_flag  =1 , approve_people  ='" + SQLDatabase.nowUserName() + "'  , approve_date  =GETDATE()  ,approve_remark='" + queryForm.NewValue + "'  where  id=" + id;

                }

                using (SqlConnection con = new SqlConnection(SQLDatabase.Connectstring))
                {

                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {

                        con.Open();
                        cmd.ExecuteNonQuery();
                    }
                }

                reLoad();
                return;
            }
        }

        private void ly_production_task_waste_DataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView dgv = sender as DataGridView;

            if (this.ly_production_task_waste_DataGridView.CurrentRow.Cells["打废审批人"].Value != null)
            {
                string inspector = this.ly_production_task_waste_DataGridView.CurrentRow.Cells["打废审批人"].Value.ToString();

                if (inspector.Trim() != "")
                {
                    if (inspector != SQLDatabase.nowUserName())
                    {
                        MessageBox.Show("请质检员:" + inspector + "修改", "注意");
                        return;
                    }
                }
            }



            if ("打废审批" == dgv.CurrentCell.OwningColumn.Name)
            {
                string sel;



                sel = "SELECT  stylecode as 编码, stylename as 原因 FROM ly_receive_repair_wastestyle ";



                QueryForm queryForm = new QueryForm();


                queryForm.Sel = sel;
                queryForm.Constr = SQLDatabase.Connectstring;

         
                queryForm.ShowDialog();


                if (queryForm.Result != "")
                {
                    string id = this.ly_production_task_waste_DataGridView.CurrentRow.Cells["打废编号"].Value.ToString();
                    string sql = "";
                    if ("True" == dgv.CurrentRow.Cells["打废审批"].Value.ToString())
                    {

                        sql = @"update  ly_production_task_waste   set   approve_flag  = 0 , approve_people  =null  , approve_date  =null,approve_remark=null where  id=" + id;
                    }
                    else
                    {
                        sql = @"update  ly_production_task_waste   set   approve_flag  =1 , approve_people  ='" + SQLDatabase.nowUserName() + "'  , approve_date  =GETDATE(),approve_remark='"+queryForm.Result1+"' where  id=" + id;

                    }

                    using (SqlConnection con = new SqlConnection(SQLDatabase.Connectstring))
                    {

                        using (SqlCommand cmd = new SqlCommand(sql, con))
                        {

                            con.Open();
                            cmd.ExecuteNonQuery();
                        }
                    }

                    reLoad();
                    return;

                }
            }
        }








    }
}
