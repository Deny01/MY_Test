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
    public partial class LY_ProductTask_SubmitBenchwork : Form
    {
        public LY_ProductTask_SubmitBenchwork()
        {
            InitializeComponent();
        }

        private void LY_QualityInspection_Benchwork_Load(object sender, EventArgs e)
        {
            this.dateTimePicker1.Text = DateTime.Today.AddMonths(-1).Date.ToString();
            this.dateTimePicker2.Text = DateTime.Today.AddDays(0).Date.ToString();


            this.ly_production_task_inspectionTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;

            this.lY_productiontask_periodTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            ly_product_task_requestSingleTableAdapter.Connection.ConnectionString= SQLDatabase.Connectstring;

            ly_product_task_returnSingleTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;

            ly_product_wasteSelectTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            ly_Bom_expendTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;

            ly_product_returnTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            ly_product_wasteSelectTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;

            ly_product_task_wasteTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            ly_material_replacelistTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            ly_product_task_returnSingleTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;


            this.lY_productiontask_periodTableAdapter.Fill(this.lYProductMange.LY_productiontask_period, this.dateTimePicker1.Value, this.dateTimePicker2.Value.AddDays(1), "QZ", SQLDatabase.NowUserID);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.lY_productiontask_periodTableAdapter.Fill(this.lYProductMange.LY_productiontask_period, this.dateTimePicker1.Value, this.dateTimePicker2.Value.AddDays(1), "QZ", SQLDatabase.NowUserID);

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
            if (null != this.ly_production_orderDataGridView.CurrentRow)
            {

                string nowproductionorderNum = this.ly_production_orderDataGridView.CurrentRow.Cells["任务单号"].Value.ToString();

                this.ly_production_task_inspectionTableAdapter.Fill(this.lYProductMange.ly_production_task_inspection, nowproductionorderNum);

            }
            else
            {


                 this.ly_production_task_inspectionTableAdapter.Fill(this.lYProductMange.ly_production_task_inspection, "");
   
            }

            this.ly_material_replacelistTableAdapter.Fill(this.lYMaterielRequirements.ly_material_replacelist, "asd");

        }

        private void bindingNavigatorAddNewItem_Click(object sender, EventArgs e)
        {


            if (null == this.ly_production_orderDataGridView.CurrentRow) return;

            string message = "增加提交记录吗？";
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
                    MessageBox.Show("任务单装配数量已经全部送交,不能增加提交记录", "注意");

                    return;
                }
                else
                {

                    this.ly_production_task_inspectionBindingSource.AddNew();

                    // this.ly_production_task_inspectionDataGridView.CurrentRow.Cells["质检单号"].Value = GetMaxTaskInspection();

                    string nowproductionTaskNum = "";

                    nowproductionTaskNum = this.ly_production_orderDataGridView.CurrentRow.Cells["任务单号"].Value.ToString();

                    this.ly_production_task_inspectionDataGridView.CurrentRow.Cells["任务单号1"].Value = nowproductionTaskNum;

                    this.ly_production_task_inspectionDataGridView.CurrentRow.Cells["提交日期"].Value = SQLDatabase.GetNowdate().ToString(); ;

                    this.ly_production_task_inspectionDataGridView.CurrentRow.Cells["提交人"].Value = SQLDatabase.nowUserName();

                    //this.ly_production_task_inspectionDataGridView.CurrentRow.Cells["质检次数"].Value = 1;

                    //this.ly_production_task_inspectionDataGridView.CurrentRow.Cells["初检"].Value = "True";

                    /////////////////////////
                    if ("成品" == this.ly_production_orderDataGridView.CurrentRow.Cells["组别"].Value.ToString())
                    {
                        ly_production_task_inspectionDataGridView.CurrentRow.Cells["合格"].Value = "True";
                        ly_production_task_inspectionDataGridView.CurrentRow.Cells["质检人"].Value = "系统";
                        ly_production_task_inspectionDataGridView.CurrentRow.Cells["质检日期"].Value = SQLDatabase.GetNowdate();
                        ly_production_task_inspectionDataGridView.CurrentRow.Cells["质检单号"].Value = GetMaxTaskInspection();

                        if (!string.IsNullOrEmpty(ly_production_task_inspectionDataGridView.CurrentRow.Cells["质检次数"].Value.ToString()))
                        {

                            //ly_production_task_inspectionDataGridView.Rows[i].Cells["质检次数"].Value = int.Parse(ly_production_task_inspectionDataGridView.Rows[i].Cells["质检次数"].Value.ToString()) + 1;
                            ly_production_task_inspectionDataGridView.CurrentRow.Cells["质检次数"].Value = 1;
                        }
                        else
                        {
                            ly_production_task_inspectionDataGridView.CurrentRow.Cells["质检次数"].Value = 1;
                        }
                    }

                    ///////////



                }






                SaveChanged();




            }
        }

        private bool SaveChangedMachine()
        {

            this.ly_production_task_inspectionDataGridView.EndEdit();

            this.Validate();
            this.ly_production_task_inspectionBindingSource.EndEdit();

            try
            {

                this.ly_production_task_inspectionTableAdapter.Update(this.lYProductMange.ly_production_task_inspection);
                return true;
            }
            catch (SqlException sqle)
            {
                MessageBox.Show(sqle.Message.Split('*')[0]);
                return false;
            }





        }

        //private void SaveChangedMachine()
        //{

        //    this.ly_production_task_inspectionDataGridView.EndEdit();

        //    this.Validate();
        //    this.ly_production_task_inspectionBindingSource.EndEdit();

        //    this.ly_production_task_inspectionTableAdapter.Update(this.lYProductMange.ly_production_task_inspection);

        //    //string nowinspectionTaskNum = this.ly_production_task_inspectionDataGridView.CurrentRow.Cells["质检单号"].Value.ToString();

        //    string nowproductionTaskNum = this.ly_production_orderDataGridView.CurrentRow.Cells["任务单号"].Value.ToString();

        //    this.ly_production_orderDataGridView.SelectionChanged -= ly_production_orderDataGridView_SelectionChanged;
        //    this.lY_productiontask_periodTableAdapter.Fill(this.lYProductMange.LY_productiontask_period, this.dateTimePicker1.Value, this.dateTimePicker2.Value.AddDays(1), "QZ");

        //    this.lY_productiontask_periodBindingSource.Position = this.lY_productiontask_periodBindingSource.Find("跟单编号", nowproductionTaskNum);
        //    this.ly_production_orderDataGridView.SelectionChanged += ly_production_orderDataGridView_SelectionChanged;



        //    //this.ly_production_task_inspectionBindingSource.Position = this.ly_production_task_inspectionBindingSource.Find("质检单号", nowinspectionTaskNum);




        //}
        private void SaveChanged()
        {

            this.ly_production_task_inspectionDataGridView.EndEdit();

            this.Validate();
            this.ly_production_task_inspectionBindingSource.EndEdit();

            this.ly_production_task_inspectionTableAdapter.Update(this.lYProductMange.ly_production_task_inspection);

            //string nowinspectionTaskNum = this.ly_production_task_inspectionDataGridView.CurrentRow.Cells["质检单号"].Value.ToString();

            string nowproductionTaskNum = this.ly_production_orderDataGridView.CurrentRow.Cells["任务单号"].Value.ToString();

            this.ly_production_orderDataGridView.SelectionChanged -= ly_production_orderDataGridView_SelectionChanged;
            this.lY_productiontask_periodTableAdapter.Fill(this.lYProductMange.LY_productiontask_period, this.dateTimePicker1.Value, this.dateTimePicker2.Value.AddDays(1), "QZ", SQLDatabase.NowUserID);

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
            cmd.Parameters["@Production_mode"].Value = "QCQZ";


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


            //if ("True" == ly_production_task_inspectionDataGridView.CurrentRow.Cells["合格"].Value.ToString())
            //{
            //    MessageBox.Show("已经质检合格，不能删除(实需删除，请先清除质检合格标记)", "注意");
            //    return;

            //}

            if ("True" == ly_production_task_inspectionDataGridView.CurrentRow.Cells["入库"].Value.ToString())
            {
                MessageBox.Show("已经入库，不能删除(实需删除，请先删除该质检单号的入库记录)", "注意");
                return;

            }



            string inspector = this.ly_production_task_inspectionDataGridView.CurrentRow.Cells["提交人"].Value.ToString();

            if (inspector != SQLDatabase.nowUserName())
            {
                MessageBox.Show("请提交人:" + inspector + "删除", "注意");
                return;
            }



            string message1 = "当前提交记录将被删除，继续吗？";
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

            //if ("True" == dgv.CurrentRow.Cells["合格"].Value.ToString())
            //{
            //    MessageBox.Show("已经质检合格，不能修改(实需修改，请先清除质检合格标记)", "注意");
            //    return;

            //}

            if ("True" == dgv.CurrentRow.Cells["入库"].Value.ToString())
            {
                MessageBox.Show("已经入库，不能修改(实需修改，请先删除该质检单号的入库记录)", "注意");
                return;

            }

            string inspector = this.ly_production_task_inspectionDataGridView.CurrentRow.Cells["提交人"].Value.ToString();

            if (inspector != SQLDatabase.nowUserName())
            {
                MessageBox.Show("请提交人:" + inspector + "修改", "注意");
                return;
            }


            if ("提交" == dgv.CurrentCell.OwningColumn.Name)
            {

                if (string.IsNullOrEmpty(dgv.CurrentRow.Cells["设备编号"].Value.ToString()))
                {
                    MessageBox.Show("无设备编号,不能提交...", "注意");
                    return;

                }


                if ("True" == dgv.CurrentRow.Cells["提交"].Value.ToString())
                {
                    dgv.CurrentRow.Cells["提交"].Value = "False";
                    //dgv.CurrentRow.Cells["提交人"].Value = DBNull.Value;
                    dgv.CurrentRow.Cells["提交日期"].Value = DBNull.Value;

                    /////////////////////////
                    if ("成品" == this.ly_production_orderDataGridView.CurrentRow.Cells["组别"].Value.ToString())
                    {
                        ly_production_task_inspectionDataGridView.CurrentRow.Cells["合格"].Value = "False";
                        ly_production_task_inspectionDataGridView.CurrentRow.Cells["质检人"].Value = DBNull.Value;
                        ly_production_task_inspectionDataGridView.CurrentRow.Cells["质检日期"].Value = DBNull.Value;
                        ly_production_task_inspectionDataGridView.CurrentRow.Cells["质检单号"].Value = DBNull.Value;

                        if (!string.IsNullOrEmpty(ly_production_task_inspectionDataGridView.CurrentRow.Cells["质检次数"].Value.ToString()))
                        {

                            //ly_production_task_inspectionDataGridView.Rows[i].Cells["质检次数"].Value = int.Parse(ly_production_task_inspectionDataGridView.Rows[i].Cells["质检次数"].Value.ToString()) + 1;
                            ly_production_task_inspectionDataGridView.CurrentRow.Cells["质检次数"].Value = DBNull.Value; ;
                        }
                        else
                        {
                            ly_production_task_inspectionDataGridView.CurrentRow.Cells["质检次数"].Value = DBNull.Value; ;
                        }
                    }

                    ///////////
                }
                else
                {

                    dgv.CurrentRow.Cells["提交"].Value = "True";
                    //dgv.CurrentRow.Cells["提交人"].Value = SQLDatabase.nowUserName();
                    dgv.CurrentRow.Cells["提交日期"].Value = SQLDatabase.GetNowdate();

                    /////////////////////////
                    if ("成品" == this.ly_production_orderDataGridView.CurrentRow.Cells["组别"].Value.ToString())
                    {
                        ly_production_task_inspectionDataGridView.CurrentRow.Cells["合格"].Value = "True";
                        ly_production_task_inspectionDataGridView.CurrentRow.Cells["质检人"].Value = "系统";
                        ly_production_task_inspectionDataGridView.CurrentRow.Cells["质检日期"].Value = SQLDatabase.GetNowdate();
                        ly_production_task_inspectionDataGridView.CurrentRow.Cells["质检单号"].Value = GetMaxTaskInspection();

                        if (!string.IsNullOrEmpty(ly_production_task_inspectionDataGridView.CurrentRow.Cells["质检次数"].Value.ToString()))
                        {

                            //ly_production_task_inspectionDataGridView.Rows[i].Cells["质检次数"].Value = int.Parse(ly_production_task_inspectionDataGridView.Rows[i].Cells["质检次数"].Value.ToString()) + 1;
                            ly_production_task_inspectionDataGridView.CurrentRow.Cells["质检次数"].Value = 1;
                        }
                        else
                        {
                            ly_production_task_inspectionDataGridView.CurrentRow.Cells["质检次数"].Value = 1;
                        }
                    }

                    ///////////
                }





                SaveChanged();




                return;

            }
            ////////////////////////////////////////////////////////////////////////





            if ("提交日期" == dgv.CurrentCell.OwningColumn.Name)
            {

                ChangeValue queryForm = new ChangeValue();

                queryForm.OldValue = dgv.CurrentCell.Value.ToString();
                queryForm.NewValue = "";
                queryForm.ChangeMode = "datetime";
                queryForm.ShowDialog();




                if (queryForm.NewValue != "")
                {
                    dgv.CurrentRow.Cells["提交日期"].Value = queryForm.NewValue;

                }
                else
                {

                    dgv.CurrentRow.Cells["提交日期"].Value = DBNull.Value;
                }

                SaveChanged();
                return;



            }
            /////////////////////////////////////////////////////////
            //if ("质检次数" == dgv.CurrentCell.OwningColumn.Name)
            //{

            //    ChangeValue queryForm = new ChangeValue();

            //    queryForm.OldValue = dgv.CurrentCell.Value.ToString();
            //    queryForm.NewValue = "";
            //    queryForm.ChangeMode = "value";
            //    queryForm.ShowDialog();




            //    if (queryForm.NewValue != "")
            //    {
            //        dgv.CurrentRow.Cells["质检次数"].Value = queryForm.NewValue;


            //        if (int.Parse(queryForm.NewValue) == 1)
            //        {
            //            dgv.CurrentRow.Cells["初检"].Value = "True";
            //        }
            //        else
            //        {
            //            dgv.CurrentRow.Cells["初检"].Value = "False";
            //        }



            //    }
            //    else
            //    {
            //        dgv.CurrentRow.Cells["质检次数"].Value = DBNull.Value;
            //        dgv.CurrentRow.Cells["初检"].Value = "False";

            //    }

            //    SaveChanged();
            //    return;

            //}

            /////////////////////////////////////////////////////////


            ///////////////////////////////////////////////////////

            if ("设备编号" == dgv.CurrentCell.OwningColumn.Name)
            {


                if ("True" == dgv.CurrentRow.Cells["提交"].Value.ToString())
                {
                    MessageBox.Show("已经提交,不能修改...", "注意");
                    return;

                }


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

                this.ly_production_task_inspectionDataGridView.EndEdit();

                this.Validate();
                this.ly_production_task_inspectionBindingSource.EndEdit();

                if (!SaveChangedMachine())
                {
                    dgv.CurrentRow.Cells["设备编号"].Value = DBNull.Value;

                }


                //SaveChanged();
                return;

            }






            //    ///////////////////////////////////////////////////////

            //    if ("质检意见" == dgv.CurrentCell.OwningColumn.Name)
            //    {

            //        ChangeValue queryForm = new ChangeValue();

            //        queryForm.OldValue = dgv.CurrentCell.Value.ToString();
            //        queryForm.NewValue = "";
            //        queryForm.ChangeMode = "longstring";
            //        queryForm.ShowDialog();




            //        if (queryForm.NewValue != "")
            //        {
            //            dgv.CurrentRow.Cells["质检意见"].Value = queryForm.NewValue;


            //        }
            //        else
            //        {
            //            dgv.CurrentRow.Cells["质检意见"].Value = DBNull.Value;
            //        }

            //        SaveChanged();
            //        return;

            //    }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if (null == this.ly_production_orderDataGridView.CurrentRow) return;

            string message = "增加全部提交记录吗？";
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
                    MessageBox.Show("任务单装配数量已经全部送交,不能增加提交记录", "注意");

                    return;
                }
                else
                {

                    for (int i = 0; i < (need_inspect_count - send_inspect_count); i++)
                    {

                        this.ly_production_task_inspectionBindingSource.AddNew();

                        // this.ly_production_task_inspectionDataGridView.CurrentRow.Cells["质检单号"].Value = GetMaxTaskInspection();

                        string nowproductionTaskNum = "";

                        nowproductionTaskNum = this.ly_production_orderDataGridView.CurrentRow.Cells["任务单号"].Value.ToString();

                        this.ly_production_task_inspectionDataGridView.CurrentRow.Cells["任务单号1"].Value = nowproductionTaskNum;

                        this.ly_production_task_inspectionDataGridView.CurrentRow.Cells["提交日期"].Value = SQLDatabase.GetNowdate().ToString(); ;

                        this.ly_production_task_inspectionDataGridView.CurrentRow.Cells["提交人"].Value = SQLDatabase.nowUserName();

                        ///////////////////////////
                        //if ("成品" == this.ly_production_orderDataGridView.CurrentRow.Cells["组别"].Value.ToString())
                        //{
                        //    ly_production_task_inspectionDataGridView.CurrentRow.Cells["合格"].Value = "True";
                        //    ly_production_task_inspectionDataGridView.CurrentRow.Cells["质检人"].Value = "系统";
                        //    ly_production_task_inspectionDataGridView.CurrentRow.Cells["质检日期"].Value = SQLDatabase.GetNowdate();
                        //    ly_production_task_inspectionDataGridView.CurrentRow.Cells["质检单号"].Value = GetMaxTaskInspection();

                        //    if (!string.IsNullOrEmpty(ly_production_task_inspectionDataGridView.CurrentRow.Cells["质检次数"].Value.ToString()))
                        //    {

                        //        //ly_production_task_inspectionDataGridView.Rows[i].Cells["质检次数"].Value = int.Parse(ly_production_task_inspectionDataGridView.Rows[i].Cells["质检次数"].Value.ToString()) + 1;
                        //        ly_production_task_inspectionDataGridView.CurrentRow.Cells["质检次数"].Value = 1;
                        //    }
                        //    else
                        //    {
                        //        ly_production_task_inspectionDataGridView.CurrentRow.Cells["质检次数"].Value = 1;
                        //    }
                        //}

                        /////////////



                        SaveChanged();
                    }

                    //this.ly_production_task_inspectionDataGridView.CurrentRow.Cells["质检次数"].Value = 1;

                    //this.ly_production_task_inspectionDataGridView.CurrentRow.Cells["初检"].Value = "True";



                }











            }
        }

        private void 自动增加设备编号ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (null == ly_production_task_inspectionDataGridView.CurrentRow) return;

            string nowmachineNum = ly_production_task_inspectionDataGridView.CurrentRow.Cells["设备编号"].Value.ToString();

            if (string.IsNullOrEmpty(nowmachineNum))
            {
                return;
            }

            else
            {
                int nowNum = int.Parse(nowmachineNum.Substring(nowmachineNum.Length - 3, 3));

                int j = 1;

                for (int i = 0; i < ly_production_task_inspectionDataGridView.RowCount; i++)
                {
                    if (string.IsNullOrEmpty(ly_production_task_inspectionDataGridView.Rows[i].Cells["设备编号"].Value.ToString()))
                    {

                        ly_production_task_inspectionDataGridView.Rows[i].Cells["设备编号"].Value = nowmachineNum.Substring(0, nowmachineNum.Length - 3) + (1000 + j + nowNum).ToString().Substring(1, 3);
                        if (!SaveChangedMachine())
                        {
                            ly_production_task_inspectionDataGridView.Rows[i].Cells["设备编号"].Value = DBNull.Value;
                            return;
                        }
                        j = j + 1;
                    }
                }

            }
        }

        private void 提交全部ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (null == ly_production_task_inspectionDataGridView.CurrentRow) return;


            

            int j = 1;

            for (int i = 0; i < ly_production_task_inspectionDataGridView.RowCount; i++)
            {
                if (!string.IsNullOrEmpty(ly_production_task_inspectionDataGridView.Rows[i].Cells["设备编号"].Value.ToString()))
                {


                    if ("True" != ly_production_task_inspectionDataGridView.Rows[i].Cells["提交"].Value.ToString())
                    {

                        ly_production_task_inspectionDataGridView.Rows[i].Cells["提交"].Value = "True";
                        //dgv.CurrentRow.Cells["提交人"].Value = SQLDatabase.nowUserName();
                        ly_production_task_inspectionDataGridView.Rows[i].Cells["提交日期"].Value = SQLDatabase.GetNowdate();

                        /////////////////////////
                        if ("成品" == this.ly_production_orderDataGridView.CurrentRow.Cells["组别"].Value.ToString())
                        {
                            ly_production_task_inspectionDataGridView.Rows[i].Cells["合格"].Value = "True";
                            ly_production_task_inspectionDataGridView.Rows[i].Cells["质检人"].Value = "系统";
                            ly_production_task_inspectionDataGridView.Rows[i].Cells["质检日期"].Value = SQLDatabase.GetNowdate();
                            ly_production_task_inspectionDataGridView.Rows[i].Cells["质检单号"].Value = GetMaxTaskInspection();

                            if (!string.IsNullOrEmpty(ly_production_task_inspectionDataGridView.CurrentRow.Cells["质检次数"].Value.ToString()))
                            {

                                //ly_production_task_inspectionDataGridView.Rows[i].Cells["质检次数"].Value = int.Parse(ly_production_task_inspectionDataGridView.Rows[i].Cells["质检次数"].Value.ToString()) + 1;
                                ly_production_task_inspectionDataGridView.CurrentRow.Cells["质检次数"].Value = 1;
                            }
                            else
                            {
                                ly_production_task_inspectionDataGridView.CurrentRow.Cells["质检次数"].Value = 1;
                            }

                        }

                        this.ly_production_task_inspectionDataGridView.EndEdit();

                        this.Validate();
                        this.ly_production_task_inspectionBindingSource.EndEdit();

                        this.ly_production_task_inspectionTableAdapter.Update(this.lYProductMange.ly_production_task_inspection);

                       
                    }
                   

                    ///////////
                   

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

        private void 提交选中ToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (null == ly_production_task_inspectionDataGridView.CurrentRow) return;




            int j = 1;

            for (int i = 0; i < ly_production_task_inspectionDataGridView.RowCount; i++)
            {
                if (!string.IsNullOrEmpty(ly_production_task_inspectionDataGridView.Rows[i].Cells["设备编号"].Value.ToString()))
                {
                    if (true == ly_production_task_inspectionDataGridView.Rows[i].Selected)
                    {

                        if ("True" != ly_production_task_inspectionDataGridView.Rows[i].Cells["提交"].Value.ToString())
                        {

                            ly_production_task_inspectionDataGridView.Rows[i].Cells["提交"].Value = "True";
                            //dgv.CurrentRow.Cells["提交人"].Value = SQLDatabase.nowUserName();
                            ly_production_task_inspectionDataGridView.Rows[i].Cells["提交日期"].Value = SQLDatabase.GetNowdate();
                        }
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

        private void ly_Bom_expendDataGridView_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {

            if (ly_production_task_inspectionDataGridView.CurrentRow == null
                || ly_production_orderDataGridView.CurrentRow == null)
            {
                MessageBox.Show("请选择一项提交记录！", "注意");
                return;
            }
            if (ly_Bom_expendDataGridView.CurrentRow == null)
            {
                MessageBox.Show("请选择一项领料！", "注意");
                return;
            }
            string wzbh = ly_Bom_expendDataGridView.CurrentRow.Cells["领料物资编号"].Value.ToString();
            string plan_id = ly_production_orderDataGridView.CurrentRow.Cells["plan_id_order"].Value.ToString();
            string taskcode = ly_production_orderDataGridView.CurrentRow.Cells["任务单号"].Value.ToString();
            if (string.IsNullOrEmpty(ly_production_task_inspectionDataGridView.CurrentRow.Cells["设备编号"].Value.ToString()))
            {
                MessageBox.Show("请先设置提交的设备编号！", "注意");
                return;
            }
            string machine_num = ly_production_task_inspectionDataGridView.CurrentRow.Cells["设备编号"].Value.ToString();
            string task_inspection = ly_production_task_inspectionDataGridView.CurrentRow.Cells["提交号"].Value.ToString();


            DataTable dt = null;
            using (SqlConnection connection = new SqlConnection(SQLDatabase.Connectstring))
            {
                string sql = "select material_flag,request_qty from ly_production_task_requestSingle where task_inspection=" + task_inspection + " and itemno='" + wzbh + "'";

                SqlDataAdapter adapter = new SqlDataAdapter(sql, connection);
                DataSet ds = new DataSet();
                adapter.Fill(ds);
                dt = ds.Tables[0];
            }
            DataTable dt2 = null;
            using (SqlConnection connection = new SqlConnection(SQLDatabase.Connectstring))
            {
                string sql = @"(SELECT pruductionTaskInspection_num, wzbh, out_level, ISNULL(SUM(qty), 0) AS out_qty  FROM ly_store_out where pruductionTaskInspection_num = '" +
                                taskcode + "' and wzbh = '" + wzbh + "' and out_level = 'single' and machine_num='"+machine_num+"'   GROUP BY pruductionTaskInspection_num, wzbh, out_level)";

                SqlDataAdapter adapter = new SqlDataAdapter(sql, connection);
                DataSet ds = new DataSet();
                adapter.Fill(ds);
                dt2 = ds.Tables[0];
            }


            ChangeValue queryForm = new ChangeValue();
            queryForm.Text = "领料数量";
            if (dt.Rows.Count > 0)
            {
                queryForm.OldValue = dt.Rows[0]["request_qty"].ToString();
            }
            else
            {
                queryForm.OldValue = "0";
            }
            queryForm.NewValue = "";
            queryForm.ChangeMode = "value";
            queryForm.ShowDialog();

            if (queryForm.NewValue != "")
            {
                if (queryForm.NewValue == "0")
                {
                    MessageBox.Show("数量为0！", "注意"); return;
                }

                if (dt2.Rows.Count > 0)
                {
                    decimal a = decimal.Parse(dt2.Rows[0]["out_qty"].ToString());
                    decimal b = decimal.Parse(queryForm.NewValue);
                    if (a >= b)
                    {
                        MessageBox.Show("重新领料数不能小于或者等于已经领料数量！", "注意"); return;
                    }
                }

            }
            else
            {
                return;
            }
             

            if (queryForm.OldValue == "0")
            { 

                string sql = @"INSERT INTO  [dbo].[ly_production_task_requestSingle] ([plan_id] ,[pruductionOrder_num]  ,[itemno] , [machine_num]  ,[material_flag]  ,[request_qty] ,[sys_date]   ,[task_inspection])
                             VALUES  (" + plan_id + " ,'" + taskcode + "','" + wzbh + "','" + machine_num + "',0," + decimal.Parse(queryForm.NewValue) + ",'" + SQLDatabase.GetNowdate().ToString() + "'," + task_inspection + ")";

                sqlHelper(sql); 
            }
            else
            {

                if (dt.Rows[0]["material_flag"].ToString() == "True")
                {
                    MessageBox.Show("请联系质检取消审核！", "注意"); return;
                }

                using (SqlConnection connection = new SqlConnection(SQLDatabase.Connectstring))
                {

                    string sql = "update ly_production_task_requestSingle set request_qty=" + decimal.Parse(queryForm.NewValue) + " where task_inspection=" + task_inspection + " and itemno='" + wzbh + "'";
                    using (SqlCommand cmd = new SqlCommand(sql, connection))
                    {

                        connection.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
            }

            reLoad();


        } 


        protected void sqlHelper(string sql)
        {

            SqlConnection myConn = new SqlConnection(SQLDatabase.Connectstring);
            myConn.Open();
            SqlCommand myComm = new SqlCommand();
            SqlTransaction myTran;
            myTran = myConn.BeginTransaction();
            try
            {
                myComm.Connection = myConn;
                myComm.Transaction = myTran;
                myComm.CommandText = sql;
                myComm.ExecuteNonQuery();
                myTran.Commit();
            }
            catch (SqlException err)
            {
                MessageBox.Show(err.Message);
                myTran.Rollback();
            }
            finally
            {
                myConn.Close();
            }
        }

        private void ly_production_task_inspectionDataGridView_SelectionChanged(object sender, EventArgs e)
        {

            reLoad();
        }


        protected void reLoad()
        {
            if (ly_production_task_inspectionDataGridView.CurrentRow == null || this.ly_production_orderDataGridView.CurrentRow == null)
            {
                return;
            }
            string plan_id = ly_production_orderDataGridView.CurrentRow.Cells["plan_id_order"].Value.ToString();
            string tjh = ly_production_task_inspectionDataGridView.CurrentRow.Cells["提交号"].Value.ToString();
            ly_product_task_requestSingleTableAdapter.Fill(lYProductMange.ly_product_task_requestSingle, int.Parse(tjh));

            ly_product_task_returnSingleTableAdapter.Fill(lYProductMange.ly_product_task_returnSingle, int.Parse(tjh));
            this.ly_product_returnTableAdapter.Fill(this.lYProductMange.ly_product_return, int.Parse(plan_id));//可追加退料  选择列表
            this.ly_product_wasteSelectTableAdapter.Fill(this.lYProductMange.ly_product_wasteSelect, int.Parse(plan_id));
            this.ly_product_task_wasteTableAdapter.Fill(this.lYProductMange.ly_product_task_waste, int.Parse(tjh));
            string nowitemno = this.ly_production_orderDataGridView.CurrentRow.Cells["物料编码"].Value.ToString();
            this.ly_Bom_expendTableAdapter.Fill(this.lYMaterialMange.ly_Bom_expend, nowitemno);

        }
        private void toolStripButton25_Click(object sender, EventArgs e)
        {
            if (null == this.ly_Restructuring_request_singleDataGridView.CurrentRow) return;


            if ("True" == ly_Restructuring_request_singleDataGridView.CurrentRow.Cells["审核"].Value.ToString())
            {
                MessageBox.Show("已经审批，不能删除(实需删除，请先清除质检审批标记)", "注意");
                return;

            }
            string id = ly_Restructuring_request_singleDataGridView.CurrentRow.Cells["编号"].Value.ToString();



            string message1 = "当前记录将被删除，继续吗？";
            string caption1 = "提示...";
            MessageBoxButtons buttons1 = MessageBoxButtons.YesNo;

            DialogResult result1;



            result1 = MessageBox.Show(message1, caption1, buttons1,
            MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

            if (result1 == DialogResult.Yes)
            {
                using (SqlConnection connection = new SqlConnection(SQLDatabase.Connectstring))
                {

                    string sql = "delete from  ly_production_task_requestSingle  where id=" + id;
                    using (SqlCommand cmd = new SqlCommand(sql, connection))
                    {

                        connection.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
                reLoad();
            }

        }

        private void toolStripButton26_Click(object sender, EventArgs e)
        {
            reLoad();
        }

        private void ly_inma0010_returnDataGridView_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {

            if (ly_production_task_inspectionDataGridView.CurrentRow == null
                || ly_production_orderDataGridView.CurrentRow == null)
            {
                MessageBox.Show("请选择一项提交记录！", "注意");
                return;
            }
            if (ly_inma0010_returnDataGridView.CurrentRow == null)
            {
                MessageBox.Show("请选择一项退料！", "注意");
                return;
            }
            string wzbh = ly_inma0010_returnDataGridView.CurrentRow.Cells["退料编码"].Value.ToString();
            string plan_id = ly_production_orderDataGridView.CurrentRow.Cells["plan_id_order"].Value.ToString();
            string taskcode = ly_production_orderDataGridView.CurrentRow.Cells["任务单号"].Value.ToString();
            if (string.IsNullOrEmpty(ly_production_task_inspectionDataGridView.CurrentRow.Cells["设备编号"].Value.ToString()))
            {
                MessageBox.Show("请先设置提交的设备编号！", "注意");
                return;
            }
            string machine_num = ly_production_task_inspectionDataGridView.CurrentRow.Cells["设备编号"].Value.ToString();
            string task_inspection = ly_production_task_inspectionDataGridView.CurrentRow.Cells["提交号"].Value.ToString();


            DataTable dt = null;
            using (SqlConnection connection = new SqlConnection(SQLDatabase.Connectstring))
            {
                string sql = "select material_flag,return_qty from ly_production_task_returnSingle where task_inspection=" + task_inspection + " and itemno='" + wzbh + "'";

                SqlDataAdapter adapter = new SqlDataAdapter(sql, connection);
                DataSet ds = new DataSet();
                adapter.Fill(ds);
                dt = ds.Tables[0];
            }
            DataTable dt2 = null;
            using (SqlConnection connection = new SqlConnection(SQLDatabase.Connectstring))
            {
                string sql = @"(SELECT pruductionTaskInspection_num, wzbh, out_level,  (0-ISNULL(SUM(qty), 0)) AS out_qty  FROM ly_store_out where pruductionTaskInspection_num = '" +
                                taskcode + "' and wzbh = '" + wzbh + "' and out_level = 'returnsingle' and machine_num='" + machine_num + "'    GROUP BY pruductionTaskInspection_num, wzbh, out_level)";

                SqlDataAdapter adapter = new SqlDataAdapter(sql, connection);
                DataSet ds = new DataSet();
                adapter.Fill(ds);
                dt2 = ds.Tables[0];
            }


            ChangeValue queryForm = new ChangeValue();
            queryForm.Text = "退料数量";
            if (dt.Rows.Count > 0)
            {
                queryForm.OldValue = dt.Rows[0]["return_qty"].ToString();
            }
            else
            {
                queryForm.OldValue = "0";
            }
            queryForm.NewValue = "";
            queryForm.ChangeMode = "value";
            queryForm.ShowDialog();

            if (queryForm.NewValue != "")
            {
                if (queryForm.NewValue == "0")
                {
                    MessageBox.Show("数量为0！", "注意"); return;
                }
                if (dt2.Rows.Count > 0)
                {
                    decimal a = decimal.Parse(dt2.Rows[0]["out_qty"].ToString());
                    decimal b = decimal.Parse(queryForm.NewValue);
                    if (a >= b)
                    {
                        MessageBox.Show("重新退料数不能小于或者等于已经退料数量！", "注意"); return;
                    }
                }
            }
            else
            {
                return;
            }


            if (queryForm.OldValue == "0")
            {

                string sql = @"INSERT INTO  [dbo].[ly_production_task_returnSingle] ([plan_id] ,[pruductionOrder_num]  ,[itemno] , [machine_num]  ,[material_flag]  ,[return_qty] ,[sys_date]   ,[task_inspection])
                             VALUES  (" + plan_id + " ,'" + taskcode + "','" + wzbh + "','" + machine_num + "',0," + decimal.Parse(queryForm.NewValue) + ",'" + SQLDatabase.GetNowdate().ToString() + "'," + task_inspection + ")";

                sqlHelper(sql);
            }
            else
            {

                if (dt.Rows[0]["material_flag"].ToString() == "True")
                {
                    MessageBox.Show("请联系质检取消审核！", "注意"); return;
                }

                using (SqlConnection connection = new SqlConnection(SQLDatabase.Connectstring))
                {

                    string sql = "update ly_production_task_returnSingle set return_qty=" + decimal.Parse(queryForm.NewValue) + " where task_inspection=" + task_inspection + " and itemno='" + wzbh + "'";
                    using (SqlCommand cmd = new SqlCommand(sql, connection))
                    {

                        connection.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
            }

            reLoad();

        }

        private void toolStripButton33_Click(object sender, EventArgs e)
        {
            if (null == this.ly_Restructuring_return_singleDataGridView.CurrentRow) return;


            if ("True" == ly_Restructuring_return_singleDataGridView.CurrentRow.Cells["退料审批"].Value.ToString())
            {
                MessageBox.Show("已经审批，不能删除(实需删除，请先清除质检审批标记)", "注意");
                return;

            }
            string id = ly_Restructuring_return_singleDataGridView.CurrentRow.Cells["退料编号"].Value.ToString();



            string message1 = "当前记录将被删除，继续吗？";
            string caption1 = "提示...";
            MessageBoxButtons buttons1 = MessageBoxButtons.YesNo;

            DialogResult result1;



            result1 = MessageBox.Show(message1, caption1, buttons1,
            MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

            if (result1 == DialogResult.Yes)
            {
                using (SqlConnection connection = new SqlConnection(SQLDatabase.Connectstring))
                {

                    string sql = "delete from  ly_production_task_returnSingle  where id=" + id;
                    using (SqlCommand cmd = new SqlCommand(sql, connection))
                    {

                        connection.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
                reLoad();
            }
        }

 

        private void dataGridView3_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (ly_production_task_inspectionDataGridView.CurrentRow == null
                || ly_production_orderDataGridView.CurrentRow == null)
            {
                MessageBox.Show("请选择一项提交记录！", "注意");
                return;
            }
            if (dataGridView3.CurrentRow == null)
            {
                MessageBox.Show("请选择一项废料！", "注意");
                return;
            }
            string wzbh = dataGridView3.CurrentRow.Cells["废料编码"].Value.ToString();
            string plan_id = ly_production_orderDataGridView.CurrentRow.Cells["plan_id_order"].Value.ToString();
            string taskcode = ly_production_orderDataGridView.CurrentRow.Cells["任务单号"].Value.ToString();
            if (string.IsNullOrEmpty(ly_production_task_inspectionDataGridView.CurrentRow.Cells["设备编号"].Value.ToString()))
            {
                MessageBox.Show("请先设置提交的设备编号！", "注意");
                return;
            }
            string machine_num = ly_production_task_inspectionDataGridView.CurrentRow.Cells["设备编号"].Value.ToString();
            string task_inspection = ly_production_task_inspectionDataGridView.CurrentRow.Cells["提交号"].Value.ToString();


            DataTable dt = null;
            using (SqlConnection connection = new SqlConnection(SQLDatabase.Connectstring))
            {
                string sql = "select approve_flag,waste_qty from ly_production_task_waste  where inspection_id=" + task_inspection + " and itemno='" + wzbh + "'";

                SqlDataAdapter adapter = new SqlDataAdapter(sql, connection);
                DataSet ds = new DataSet();
                adapter.Fill(ds);
                dt = ds.Tables[0];
            }
            


            ChangeValue queryForm = new ChangeValue();
            queryForm.Text = "废料数量";
            if (dt.Rows.Count > 0)
            {
                queryForm.OldValue = dt.Rows[0]["waste_qty"].ToString();
            }
            else
            {
                queryForm.OldValue = "0";
            }
            queryForm.NewValue = "";
            queryForm.ChangeMode = "value";
            queryForm.ShowDialog();

            if (queryForm.NewValue != "")
            {
                if (queryForm.NewValue == "0")
                {
                    MessageBox.Show("数量为0！", "注意"); return;
                }

                decimal total = 0;
                for (int i = 0; i < dataGridView3.Rows.Count; i++)
                {
                    if (dataGridView3.Rows[i].Cells["废料编码"].Value.ToString() == wzbh)
                    {

                        total += decimal.Parse(dataGridView3.Rows[i].Cells["废料数量"].Value.ToString());
                    }
                }

                decimal b = decimal.Parse(queryForm.NewValue);
                if (total < b)
                {
                    MessageBox.Show("重新废料数不能大于领料数量！", "注意"); return;
                }



            }
            else
            {
                return;
            }


            if (queryForm.OldValue == "0")
            {

                string sql = @"INSERT INTO  [dbo].[ly_production_task_waste] ([plan_id] ,[pruductionOrder_num]  ,[itemno] , [approve_flag]  ,[waste_qty] ,[sys_date]   ,[inspection_id])
                             VALUES  (" + plan_id + " ,'" + taskcode + "','" + wzbh + "' ,0," + decimal.Parse(queryForm.NewValue) + ",'" + SQLDatabase.GetNowdate().ToString() + "'," + task_inspection + ")";

                sqlHelper(sql);
            }
            else
            {

                if (dt.Rows[0]["approve_flag"].ToString() == "True")
                {
                    MessageBox.Show("请联系质检取消审核！", "注意"); return;
                }

                using (SqlConnection connection = new SqlConnection(SQLDatabase.Connectstring))
                {

                    string sql = "update ly_production_task_waste set waste_qty=" + decimal.Parse(queryForm.NewValue) + " where inspection_id=" + task_inspection + " and itemno='" + wzbh + "'";
                    using (SqlCommand cmd = new SqlCommand(sql, connection))
                    {

                        connection.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
            }

            reLoad();
        }

        private void toolStripButton69_Click(object sender, EventArgs e)
        {
            if (null == this.ly_product_task_wasteDataGridView.CurrentRow) return;


            if ("True" == ly_product_task_wasteDataGridView.CurrentRow.Cells["废料审核"].Value.ToString())
            {
                MessageBox.Show("已经审批，不能删除(实需删除，请先清除质检审批标记)", "注意");
                return;

            }
            string id = ly_product_task_wasteDataGridView.CurrentRow.Cells["废料编号"].Value.ToString();



            string message1 = "当前记录将被删除，继续吗？";
            string caption1 = "提示...";
            MessageBoxButtons buttons1 = MessageBoxButtons.YesNo;

            DialogResult result1;



            result1 = MessageBox.Show(message1, caption1, buttons1,
            MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

            if (result1 == DialogResult.Yes)
            {
                using (SqlConnection connection = new SqlConnection(SQLDatabase.Connectstring))
                {

                    string sql = "delete from  ly_production_task_waste   where id=" + id;
                    using (SqlCommand cmd = new SqlCommand(sql, connection))
                    {

                        connection.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
                reLoad();
            }
        }

       

        private void ly_Bom_expendDataGridView_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView dgv = sender as DataGridView;

            if (null == dgv.CurrentRow) return;


            string itemno = dgv.Rows[e.RowIndex].Cells["领料物资编号"].Value.ToString();
            this.ly_material_replacelistTableAdapter.Fill(this.lYMaterielRequirements.ly_material_replacelist, itemno);
          
        }

        private void ly_material_replacelistDataGridView_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {

            if (ly_production_task_inspectionDataGridView.CurrentRow == null
                || ly_production_orderDataGridView.CurrentRow == null || ly_Bom_expendDataGridView.CurrentRow==null)
            {
                MessageBox.Show("请选择一项提交记录！", "注意");
                return;
            }
            if (ly_material_replacelistDataGridView.CurrentRow == null)
            {
                MessageBox.Show("请选择一项领料！", "注意");
                return;
            }
            string old_wzbh = ly_Bom_expendDataGridView.CurrentRow.Cells["领料物资编号"].Value.ToString();
            string wzbh = ly_material_replacelistDataGridView.CurrentRow.Cells["代料编码"].Value.ToString();
            string plan_id = ly_production_orderDataGridView.CurrentRow.Cells["plan_id_order"].Value.ToString();
            string taskcode = ly_production_orderDataGridView.CurrentRow.Cells["任务单号"].Value.ToString();
            if (string.IsNullOrEmpty(ly_production_task_inspectionDataGridView.CurrentRow.Cells["设备编号"].Value.ToString()))
            {
                MessageBox.Show("请先设置提交的设备编号！", "注意");
                return;
            }
            string machine_num = ly_production_task_inspectionDataGridView.CurrentRow.Cells["设备编号"].Value.ToString();
            string task_inspection = ly_production_task_inspectionDataGridView.CurrentRow.Cells["提交号"].Value.ToString();


            DataTable dt = null;
            using (SqlConnection connection = new SqlConnection(SQLDatabase.Connectstring))
            {
                string sql = "select material_flag,request_qty from ly_production_task_requestSingle where task_inspection="
                    + task_inspection + " and itemno='" + wzbh + "' and origin_itemno='"+old_wzbh+"'";

                SqlDataAdapter adapter = new SqlDataAdapter(sql, connection);
                DataSet ds = new DataSet();
                adapter.Fill(ds);
                dt = ds.Tables[0];
            }
            DataTable dt2 = null;
            using (SqlConnection connection = new SqlConnection(SQLDatabase.Connectstring))
            {
                string sql = @"(SELECT pruductionTaskInspection_num, wzbh, out_level, ISNULL(SUM(qty), 0) AS out_qty  FROM ly_store_out where pruductionTaskInspection_num = '" +
                                taskcode + "' and wzbh = '" + wzbh + "' and out_level = 'single' and machine_num='" + machine_num + "'    GROUP BY pruductionTaskInspection_num, wzbh, out_level)";

                SqlDataAdapter adapter = new SqlDataAdapter(sql, connection);
                DataSet ds = new DataSet();
                adapter.Fill(ds);
                dt2 = ds.Tables[0];
            }


            ChangeValue queryForm = new ChangeValue();
            queryForm.Text = "领料数量";
            if (dt.Rows.Count > 0)
            {
                queryForm.OldValue = dt.Rows[0]["request_qty"].ToString();
            }
            else
            {
                queryForm.OldValue = "0";
            }
            queryForm.NewValue = "";
            queryForm.ChangeMode = "value";
            queryForm.ShowDialog();

            if (queryForm.NewValue != "")
            {
                if (queryForm.NewValue == "0")
                {
                    MessageBox.Show("数量为0！", "注意"); return;
                }

                if (dt2.Rows.Count > 0)
                {
                    decimal a = decimal.Parse(dt2.Rows[0]["out_qty"].ToString());
                    decimal b = decimal.Parse(queryForm.NewValue);
                    if (a >= b)
                    {
                        MessageBox.Show("重新领料数不能小于或者等于已经领料数量！", "注意"); return;
                    }
                }

            }
            else
            {
                return;
            }


            if (queryForm.OldValue == "0")
            {

                string sql = @"INSERT INTO  [dbo].[ly_production_task_requestSingle] ([plan_id] ,[pruductionOrder_num]  ,[itemno] , [machine_num]  ,[material_flag]  ,[request_qty] ,[sys_date]   ,[task_inspection],[origin_itemno])
                             VALUES  (" + plan_id + " ,'" + taskcode + "','" + wzbh + "','" + machine_num + "',1," + decimal.Parse(queryForm.NewValue) + ",'" + SQLDatabase.GetNowdate().ToString() + "'," + task_inspection + ",'"+old_wzbh+"')";

                sqlHelper(sql);
            }
            else
            {

                if (dt.Rows[0]["material_flag"].ToString() == "True")
                {
                    MessageBox.Show("请联系质检取消审核！", "注意"); return;
                }

                using (SqlConnection connection = new SqlConnection(SQLDatabase.Connectstring))
                {

                    string sql = "update ly_production_task_requestSingle set request_qty=" + decimal.Parse(queryForm.NewValue) + " where task_inspection=" + task_inspection + "  and itemno='" + wzbh + "' and origin_itemno='" + old_wzbh + "'";
                    using (SqlCommand cmd = new SqlCommand(sql, connection))
                    {

                        connection.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
            }

            reLoad();


        }

        private void toolStripButton34_Click(object sender, EventArgs e)
        {
            reLoad();
        }

        private void toolStripButton70_Click(object sender, EventArgs e)
        {
            reLoad();
        }

        private void toolStripButton54_Click(object sender, EventArgs e)
        {
            reLoad();
        }

        private void toolStripButton42_Click(object sender, EventArgs e)
        {
            reLoad();
        }

   

     

        private void toolStripTextBox1_Enter_1(object sender, EventArgs e)
        {
            toolStripTextBox1.Text = "";

            this.ly_Bom_expendBindingSource.Filter = "";
        }

        private void toolStripTextBox1_KeyUp(object sender, KeyEventArgs e)
        {
            string dFilter = "";

            dFilter = GetDataGridviewMultiFilterString.DGVMultiFilterString(this.ly_Bom_expendDataGridView, this.toolStripTextBox1.Text);

            if (null == dFilter)
                dFilter = "";

            this.ly_Bom_expendBindingSource.Filter = dFilter;
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (ly_production_task_inspectionDataGridView.CurrentRow == null
                          || ly_production_orderDataGridView.CurrentRow == null || ly_Bom_expendDataGridView.CurrentRow == null)
            {
                MessageBox.Show("请选择一项提交记录！", "注意");
                return;
            }
            if (ly_material_replacelistDataGridView.CurrentRow == null)
            {
                MessageBox.Show("请选择一项代料！", "注意");
                return;
            }
            string old_wzbh = ly_Bom_expendDataGridView.CurrentRow.Cells["领料物资编号"].Value.ToString();
            string wzbh = ly_material_replacelistDataGridView.CurrentRow.Cells["代料编码"].Value.ToString();
            string plan_id = ly_production_orderDataGridView.CurrentRow.Cells["plan_id_order"].Value.ToString();
            string taskcode = ly_production_orderDataGridView.CurrentRow.Cells["任务单号"].Value.ToString();

            string message1 = "确定批量代料，继续吗？";
            string caption1 = "提示...";
            MessageBoxButtons buttons1 = MessageBoxButtons.YesNo;

            DialogResult result1;



            result1 = MessageBox.Show(message1, caption1, buttons1,
            MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

            if (result1 == DialogResult.Yes)
            {

                if (null == ly_production_task_inspectionDataGridView.CurrentRow) return;

                ChangeValue queryForm = new ChangeValue();
                queryForm.Text = "代料数量";
                queryForm.NewValue = "";
                queryForm.ChangeMode = "value";
                queryForm.ShowDialog();

                decimal count = 0;
                if (queryForm.NewValue != "" && queryForm.NewValue.Trim() != "0")
                {
                    count = Convert.ToDecimal(queryForm.NewValue);
                }
                else
                {
                    return;
                }
                if (count == 0) return;

                for (int i = 0; i < ly_production_task_inspectionDataGridView.RowCount; i++)
                {
                    if (!string.IsNullOrEmpty(ly_production_task_inspectionDataGridView.Rows[i].Cells["设备编号"].Value.ToString()))
                    {
                        string task_inspection = ly_production_task_inspectionDataGridView.Rows[i].Cells["提交号"].Value.ToString();
                        string machine_num = ly_production_task_inspectionDataGridView.Rows[i].Cells["设备编号"].Value.ToString();
                        string sql = @"select count(1) from ly_production_task_requestSingle where task_inspection="
                                   + task_inspection + " and itemno='" + wzbh + "' and origin_itemno='" + old_wzbh + "'";

                        int k = 0;
                        using (SqlConnection con = new SqlConnection(SQLDatabase.Connectstring))
                        {

                            using (SqlCommand cmd = new SqlCommand(sql, con))
                            {

                                con.Open();
                                k = Convert.ToInt32(cmd.ExecuteScalar().ToString());
                            }
                        }
                        if (k > 0)
                        {
                            continue;
                        }
                        else
                        {
                            string sqlInto = @"INSERT INTO  [dbo].[ly_production_task_requestSingle] ([plan_id] ,[pruductionOrder_num]  ,[itemno] , [machine_num]  ,[material_flag]  ,[request_qty] ,[sys_date]   ,[task_inspection],[origin_itemno])
                             VALUES  (" + plan_id + " ,'" + taskcode + "','" + wzbh + "','" + machine_num + "',1," + decimal.Parse(queryForm.NewValue) + ",'" + SQLDatabase.GetNowdate().ToString() + "'," + task_inspection + ",'" + old_wzbh + "')";

                            sqlHelper(sqlInto);
                        }

                    }
                    else
                    {
                        continue;
                    }
                }

                reLoad();
            }









        }

        private void ly_Restructuring_request_singleDataGridView_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {

            if (null == this.ly_Restructuring_request_singleDataGridView.CurrentRow) return;

            DataGridView dgv = sender as DataGridView;


            if ("True" == ly_Restructuring_request_singleDataGridView.CurrentRow.Cells["审核"].Value.ToString())
            {
                MessageBox.Show("已经审批，不能修改(实需修改，请先清除质检审批标记)", "注意");
                return;

            }
            string id = ly_Restructuring_request_singleDataGridView.CurrentRow.Cells["编号"].Value.ToString();



            if ("申请说明" == dgv.CurrentCell.OwningColumn.Name)
            {

                ChangeValue queryForm = new ChangeValue();

                queryForm.OldValue = dgv.CurrentCell.Value.ToString();
                queryForm.NewValue = "";
                queryForm.ShowDialog();




                if (queryForm.NewValue != "")
                {
                    using (SqlConnection connection = new SqlConnection(SQLDatabase.Connectstring))
                    {

                        string sql = "update  ly_production_task_requestSingle set request_remark='"+ queryForm.NewValue + "'  where id=" + id;
                        using (SqlCommand cmd = new SqlCommand(sql, connection))
                        {

                            connection.Open();
                            cmd.ExecuteNonQuery();
                        }
                    }
                    reLoad();



                }

                return;

            }
        }

        private void ly_Restructuring_return_singleDataGridView_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (null == this.ly_Restructuring_return_singleDataGridView.CurrentRow) return;

            DataGridView dgv = sender as DataGridView;
            if ("True" == ly_Restructuring_return_singleDataGridView.CurrentRow.Cells["退料审批"].Value.ToString())
            {
                MessageBox.Show("已经审批，不能修改(实需修改，请先清除质检审批标记)", "注意");
                return;

            }
            string id = ly_Restructuring_return_singleDataGridView.CurrentRow.Cells["退料编号"].Value.ToString();


            if ("申请说明2" == dgv.CurrentCell.OwningColumn.Name)
            {

                ChangeValue queryForm = new ChangeValue();

                queryForm.OldValue = dgv.CurrentCell.Value.ToString();
                queryForm.NewValue = "";
                queryForm.ShowDialog();




                if (queryForm.NewValue != "")
                {
                    using (SqlConnection connection = new SqlConnection(SQLDatabase.Connectstring))
                    {

                        string sql = "update  ly_production_task_returnSingle set request_remark='" + queryForm.NewValue + "'  where id=" + id;
                        using (SqlCommand cmd = new SqlCommand(sql, connection))
                        {

                            connection.Open();
                            cmd.ExecuteNonQuery();
                        }
                    }
                    reLoad();



                }

                return;

            }
        }
    }
}
