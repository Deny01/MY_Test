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
using System.Text.RegularExpressions;
namespace HappyYF.YuanXin.WorkSet
{
    public partial class LY_Restructuring_ProductTask_SubmitBenchwork : Form
    {
        public LY_Restructuring_ProductTask_SubmitBenchwork()
        {
            InitializeComponent();
        }

        private void LY_QualityInspection_Benchwork_Load(object sender, EventArgs e)
        {
            this.dateTimePicker1.Text = DateTime.Today.AddMonths(-1).Date.ToString();
            this.dateTimePicker2.Text = DateTime.Today.AddDays(0).Date.ToString();


            this.ly_restructuring_task_inspectionTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;

            this.lY_restructuring_periodTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;



            this.ly_Restructuring_requestTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            this.ly_Restructuring_returnTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;

            ly_Restructuring_request_singleTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            ly_Restructuring_return_singleTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            ly_Restructuring_return_stdInspectionTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;

  
            ly_Restructuring_waste_newTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring; 
            this.ly_inma0010TableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring; 
            this.ly_inma0010_returnTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
 


            this.lY_restructuring_periodTableAdapter.Fill(this.lYProductMange.LY_restructuring_period, this.dateTimePicker1.Value, this.dateTimePicker2.Value.AddDays(1), "GQ", SQLDatabase.NowUserID);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.lY_restructuring_periodTableAdapter.Fill(this.lYProductMange.LY_restructuring_period, this.dateTimePicker1.Value, this.dateTimePicker2.Value.AddDays(1), "GQ", SQLDatabase.NowUserID);

        }

        private void toolStripTextBox2_KeyUp(object sender, KeyEventArgs e)
        {
            string dFilter = "";

            dFilter = GetDataGridviewMultiFilterString.DGVMultiFilterString(this.ly_production_orderDataGridView, this.toolStripTextBox2.Text);

            if (null == dFilter)
                dFilter = "";

            this.lY_restructuring_periodBindingSource.Filter = dFilter;
        }

        private void toolStripTextBox2_Enter(object sender, EventArgs e)
        {
            toolStripTextBox2.Text = "";

            this.lY_restructuring_periodBindingSource.Filter = "";
        }

        private void ly_production_orderDataGridView_SelectionChanged(object sender, EventArgs e)
        {

            if (null != this.ly_production_orderDataGridView.CurrentRow)
            {


                string taskMumber = this.ly_production_orderDataGridView.CurrentRow.Cells["任务单号"].Value.ToString();
                this.ly_restructuring_task_inspectionTableAdapter.Fill(this.lYProductMange.ly_restructuring_task_inspection, taskMumber);

                string taskParentStr = ly_production_orderDataGridView.CurrentRow.Cells["改制编号"].Value.ToString();

                if (taskMumber == "" || taskParentStr == "")
                {
                    //以防不测
                    this.ly_Restructuring_requestTableAdapter.Fill(this.lYProductMange.ly_Restructuring_request, -1, "a123");
                    this.ly_Restructuring_returnTableAdapter.Fill(this.lYProductMange.ly_Restructuring_return, -1, "a123");
                }
                else
                {
                    int taskParent = int.Parse(taskParentStr);
                    this.ly_Restructuring_requestTableAdapter.Fill(this.lYProductMange.ly_Restructuring_request, taskParent, taskMumber);
                    this.ly_Restructuring_returnTableAdapter.Fill(this.lYProductMange.ly_Restructuring_return, taskParent, taskMumber);
                }


                if (ly_production_task_inspectionDataGridView.CurrentRow == null)
                {
                    ly_Restructuring_request_singleTableAdapter.Fill(lYProductMange.ly_Restructuring_request_single, -1);
                    ly_Restructuring_return_singleTableAdapter.Fill(lYProductMange.ly_Restructuring_return_single, -1);
                    ly_Restructuring_return_stdInspectionTableAdapter.Fill(lYProductMange.ly_Restructuring_return_stdInspection,"", -1);//标准退料

                }
                else
                {
                    string tjh = ly_production_task_inspectionDataGridView.CurrentRow.Cells["提交号"].Value.ToString();
                    ly_Restructuring_request_singleTableAdapter.Fill(lYProductMange.ly_Restructuring_request_single, int.Parse(tjh));
                    ly_Restructuring_return_singleTableAdapter.Fill(lYProductMange.ly_Restructuring_return_single, int.Parse(tjh));
                    ly_Restructuring_return_stdInspectionTableAdapter.Fill(lYProductMange.ly_Restructuring_return_stdInspection, taskMumber,int.Parse(tjh));//标准退料

                }
            }
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

                    this.ly_restructuring_task_inspectionBindingSource.AddNew();

                    string nowproductionTaskNum = "";

                    nowproductionTaskNum = this.ly_production_orderDataGridView.CurrentRow.Cells["任务单号"].Value.ToString();

                    this.ly_production_task_inspectionDataGridView.CurrentRow.Cells["任务单号1"].Value = nowproductionTaskNum;

                    this.ly_production_task_inspectionDataGridView.CurrentRow.Cells["提交日期"].Value = SQLDatabase.GetNowdate().ToString(); ;

                    this.ly_production_task_inspectionDataGridView.CurrentRow.Cells["提交人"].Value = SQLDatabase.nowUserName();

                }

                SaveChanged();
            }
        }

        private bool SaveChangedMachine()
        {

            this.ly_production_task_inspectionDataGridView.EndEdit();

            this.Validate();
            this.ly_restructuring_task_inspectionBindingSource.EndEdit();

            try
            {

                this.ly_restructuring_task_inspectionTableAdapter.Update(this.lYProductMange.ly_restructuring_task_inspection);

                return true;
            }
            catch (SqlException sqle)
            {
                MessageBox.Show(sqle.Message.Split('*')[0]);
                return false;
            }
        }

        private void SaveChanged()
        {

            try
            {
                this.ly_production_task_inspectionDataGridView.EndEdit();

                this.Validate();
                this.ly_restructuring_task_inspectionBindingSource.EndEdit();

                this.ly_restructuring_task_inspectionTableAdapter.Update(this.lYProductMange.ly_restructuring_task_inspection);
                if (null != this.ly_production_orderDataGridView.CurrentRow)
                {

                    string nowproductionorderNum = this.ly_production_orderDataGridView.CurrentRow.Cells["任务单号"].Value.ToString();

                    this.ly_restructuring_task_inspectionTableAdapter.Fill(this.lYProductMange.ly_restructuring_task_inspection, nowproductionorderNum);

                }

                string nowproductionTaskNum = this.ly_production_orderDataGridView.CurrentRow.Cells["任务单号"].Value.ToString();

                this.ly_production_orderDataGridView.SelectionChanged -= ly_production_orderDataGridView_SelectionChanged;
                this.lY_restructuring_periodTableAdapter.Fill(this.lYProductMange.LY_restructuring_period, this.dateTimePicker1.Value, this.dateTimePicker2.Value.AddDays(1), "GQ", SQLDatabase.NowUserID);

                this.lY_restructuring_periodBindingSource.Position = this.lY_restructuring_periodBindingSource.Find("跟单编号", nowproductionTaskNum);
                this.ly_production_orderDataGridView.SelectionChanged += ly_production_orderDataGridView_SelectionChanged;
            }
            catch (Exception ex)
            {
                return;//偶有几率弹报错 
            }



        }



        private void bindingNavigatorDeleteItem_Click(object sender, EventArgs e)
        {
            if (null == this.ly_production_task_inspectionDataGridView.CurrentRow) return;


            if ("True" == ly_production_task_inspectionDataGridView.CurrentRow.Cells["合格"].Value.ToString())
            {
                MessageBox.Show("已经质检合格，不能删除(实需删除，请先清除质检合格标记)", "注意");
                return;

            }

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

                this.ly_restructuring_task_inspectionBindingSource.RemoveCurrent();

                SaveChanged();
            }
        }

        private void ly_production_task_inspectionDataGridView_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {


            DataGridView dgv = sender as DataGridView;

            if ("True" == dgv.CurrentRow.Cells["合格"].Value.ToString())
            {
                MessageBox.Show("已经质检合格，不能修改(实需修改，请先清除质检合格标记)", "注意");
                return;

            }

            if ("True" == dgv.CurrentRow.Cells["入库"].Value.ToString())
            {
                MessageBox.Show("已经入库，不能修改(实需修改，请先删除该质检单号的入库记录)", "注意");
                return;

            }

            string inspector = this.ly_production_task_inspectionDataGridView.CurrentRow.Cells["提交人"].Value.ToString();
            if (!string.IsNullOrEmpty(inspector))
            {
                if (inspector != SQLDatabase.nowUserName())
                {
                    MessageBox.Show("请提交人:" + inspector + "修改", "注意");
                    return;
                }
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
                    dgv.CurrentRow.Cells["提交人"].Value = DBNull.Value;
                    dgv.CurrentRow.Cells["提交日期"].Value = DBNull.Value;
                }
                else
                {

                    dgv.CurrentRow.Cells["提交"].Value = "True";
                    dgv.CurrentRow.Cells["提交人"].Value = SQLDatabase.nowUserName();
                    dgv.CurrentRow.Cells["提交日期"].Value = SQLDatabase.GetNowdate();
                }



                SaveChanged();

                return;

            }

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

            if ("设备编号" == dgv.CurrentCell.OwningColumn.Name)
            {

                return;
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

                    if (queryForm.NewValue.Length < 3)
                    {
                        MessageBox.Show("设备编号过短...", "注意");
                        return;
                    }
                    dgv.CurrentRow.Cells["设备编号"].Value = queryForm.NewValue;
                }
                else
                {
                    dgv.CurrentRow.Cells["设备编号"].Value = DBNull.Value;
                }

                this.ly_production_task_inspectionDataGridView.EndEdit();

                this.Validate();
                this.ly_restructuring_task_inspectionBindingSource.EndEdit();

                if (!SaveChangedMachine())
                {
                    dgv.CurrentRow.Cells["设备编号"].Value = DBNull.Value;

                }
                return;

            }

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

                        this.ly_restructuring_task_inspectionBindingSource.AddNew();
                        string nowproductionTaskNum = "";

                        nowproductionTaskNum = this.ly_production_orderDataGridView.CurrentRow.Cells["任务单号"].Value.ToString();

                        this.ly_production_task_inspectionDataGridView.CurrentRow.Cells["任务单号1"].Value = nowproductionTaskNum;

                        this.ly_production_task_inspectionDataGridView.CurrentRow.Cells["提交日期"].Value = SQLDatabase.GetNowdate().ToString(); ;

                        this.ly_production_task_inspectionDataGridView.CurrentRow.Cells["提交人"].Value = SQLDatabase.nowUserName();


                        SaveChanged();
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
                    }


                    j = j + 1;
                }
                else
                {

                    j = j + 1;
                }

            }
            SaveChanged();
        }

        private void ly_inma0010DataGridView_MouseDoubleClick(object sender, MouseEventArgs e)
        {

            if (ly_production_task_inspectionDataGridView.CurrentRow == null
                || ly_production_orderDataGridView.CurrentRow == null)
            {
                MessageBox.Show("请选择一项提交记录！", "注意");
                return;
            }
            if (ly_inma0010DataGridView.CurrentRow == null)
            {
                MessageBox.Show("请选择一项领料！", "注意");
                return;
            }
            string wzbh = ly_inma0010DataGridView.CurrentRow.Cells["物资编号"].Value.ToString();
            string parent_id = ly_production_orderDataGridView.CurrentRow.Cells["改制编号"].Value.ToString();
            string task_id = ly_production_orderDataGridView.CurrentRow.Cells["任务单号"].Value.ToString();
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
                string sql = "select material_flag,request_qty from ly_Restructuring_request_single where task_inspection=" + task_inspection + " and itemno='" + wzbh + "'";

                SqlDataAdapter adapter = new SqlDataAdapter(sql, connection);
                DataSet ds = new DataSet();
                adapter.Fill(ds);
                dt = ds.Tables[0];
            }
            DataTable dt2 = null;
            using (SqlConnection connection = new SqlConnection(SQLDatabase.Connectstring))
            {
                string sql = @"(SELECT pruductionTaskInspection_num, wzbh, out_level, ISNULL(SUM(qty), 0) AS out_qty  FROM ly_store_out where pruductionTaskInspection_num = '"+
                                task_id + "' and wzbh = '" + wzbh + "' and out_level = 'single'   GROUP BY pruductionTaskInspection_num, wzbh, out_level)";

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
                this.ly_Restructuring_request_singleBindingSource.AddNew();


                this.ly_Restructuring_request_singleDataGridView.CurrentRow.Cells["改制编号_f"].Value = parent_id;
                this.ly_Restructuring_request_singleDataGridView.CurrentRow.Cells["任务单号_f"].Value = task_id;
                this.ly_Restructuring_request_singleDataGridView.CurrentRow.Cells["物资编号_f"].Value = wzbh;
                this.ly_Restructuring_request_singleDataGridView.CurrentRow.Cells["机器码_f"].Value = machine_num;
                this.ly_Restructuring_request_singleDataGridView.CurrentRow.Cells["数量_f"].Value = decimal.Parse(queryForm.NewValue);
                this.ly_Restructuring_request_singleDataGridView.CurrentRow.Cells["机器码_f"].Value = machine_num;
                this.ly_Restructuring_request_singleDataGridView.CurrentRow.Cells["追领时间_f"].Value = SQLDatabase.GetNowdate().ToString(); ;
                this.ly_Restructuring_request_singleDataGridView.CurrentRow.Cells["提交号_f"].Value = task_inspection;

                this.ly_Restructuring_request_singleDataGridView.EndEdit();

                this.ly_Restructuring_request_singleBindingSource.EndEdit();
                this.Validate();
                this.ly_Restructuring_request_singleTableAdapter.Update(this.lYProductMange.ly_Restructuring_request_single);


            }
            else
            {

                if (dt.Rows[0]["material_flag"].ToString() == "True")
                {
                    MessageBox.Show("请联系质检取消审核！", "注意"); return;
                }

                using (SqlConnection connection = new SqlConnection(SQLDatabase.Connectstring))
                {

                    string sql = "update ly_Restructuring_request_single set request_qty=" + decimal.Parse(queryForm.NewValue) + " where task_inspection=" + task_inspection + " and itemno='" + wzbh + "'";
                    using (SqlCommand cmd = new SqlCommand(sql, connection))
                    {

                        connection.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            if (ly_production_task_inspectionDataGridView.CurrentRow == null)
            {
                return;
            }
            string tjh = ly_production_task_inspectionDataGridView.CurrentRow.Cells["提交号"].Value.ToString();
            ly_Restructuring_request_singleTableAdapter.Fill(lYProductMange.ly_Restructuring_request_single, int.Parse(tjh));


            string taskMumber = this.ly_production_orderDataGridView.CurrentRow.Cells["改制编号"].Value.ToString();
            this.ly_inma0010_returnTableAdapter.Fill(this.lYProductMange.ly_inma0010_return, task_id);
        }

        private void ly_production_task_inspectionDataGridView_SelectionChanged(object sender, EventArgs e)
        {

            ReLoad();
        }

        protected void ReLoad()
        {
            if (ly_production_task_inspectionDataGridView.CurrentRow == null || ly_production_orderDataGridView.CurrentRow==null)
            {
                return;
            }

            string task_id = ly_production_orderDataGridView.CurrentRow.Cells["任务单号"].Value.ToString();

            string tjh = ly_production_task_inspectionDataGridView.CurrentRow.Cells["提交号"].Value.ToString();
            ly_Restructuring_request_singleTableAdapter.Fill(lYProductMange.ly_Restructuring_request_single, int.Parse(tjh));  //追加领料
            ly_Restructuring_return_singleTableAdapter.Fill(lYProductMange.ly_Restructuring_return_single, int.Parse(tjh));//追加退料

            ly_Restructuring_return_stdInspectionTableAdapter.Fill(lYProductMange.ly_Restructuring_return_stdInspection, task_id, int.Parse(tjh));//标准退料




            ly_Restructuring_waste_newTableAdapter.Fill(lYProductMange.ly_Restructuring_waste_new, int.Parse(tjh));//物料打废

            if (null == this.ly_production_orderDataGridView.CurrentRow)
            {
                return;
                //以防不测
            }


            string itemno_1 = this.ly_production_orderDataGridView.CurrentRow.Cells["物料编码"].Value.ToString();
            string itemno_2 = this.ly_production_orderDataGridView.CurrentRow.Cells["改前编码"].Value.ToString();
            this.ly_inma0010TableAdapter.Fill(this.lYProductMange.ly_inma0010, itemno_1, itemno_2);  //可追加领料  选择列表



            string taskMumber = this.ly_production_orderDataGridView.CurrentRow.Cells["改制编号"].Value.ToString();
            this.ly_inma0010_returnTableAdapter.Fill(this.lYProductMange.ly_inma0010_return, task_id);//可追加退料  选择列表

        }

        private void toolStripButton25_Click(object sender, EventArgs e)
        {
            if (null == this.ly_Restructuring_request_singleDataGridView.CurrentRow) return;


            if ("True" == ly_Restructuring_request_singleDataGridView.CurrentRow.Cells["审批"].Value.ToString())
            {
                MessageBox.Show("已经审批，不能删除(实需删除，请先清除质检审批标记)", "注意");
                return;

            }
            string id = ly_Restructuring_request_singleDataGridView.CurrentRow.Cells["id"].Value.ToString();



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

                    string sql = "delete from  ly_Restructuring_request_single  where id=" + id;
                    using (SqlCommand cmd = new SqlCommand(sql, connection))
                    {

                        connection.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
                if (ly_production_task_inspectionDataGridView.CurrentRow == null)
                {
                    return;
                }
                string tjh = ly_production_task_inspectionDataGridView.CurrentRow.Cells["提交号"].Value.ToString();
                ly_Restructuring_request_singleTableAdapter.Fill(lYProductMange.ly_Restructuring_request_single, int.Parse(tjh));
            }

        }

        private void toolStripTextBox4_KeyUp(object sender, KeyEventArgs e)
        {
            string filterString = GetDataGridviewMultiFilterString.DGVMultiFilterString(this.ly_Restructuring_request_singleDataGridView, this.toolStripTextBox4.Text);



            if (null == filterString)
                filterString = "";

            this.ly_Restructuring_request_singleBindingSource.Filter = filterString;
        }

        private void toolStripTextBox6_KeyUp(object sender, KeyEventArgs e)
        {
            string filterString = GetDataGridviewMultiFilterString.DGVMultiFilterString(this.ly_inma0010DataGridView, this.toolStripTextBox6.Text);



            if (null == filterString)
                filterString = "";

            this.ly_inma0010BindingSource.Filter = filterString;
        }

        private void ly_inma0010_returnDataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (ly_production_task_inspectionDataGridView.CurrentRow == null
              || ly_production_orderDataGridView.CurrentRow == null)
            {
                MessageBox.Show("请选择一项提交记录！", "注意");
                return;
            }
            if (ly_inma0010DataGridView.CurrentRow == null)
            {
                MessageBox.Show("请选择一项退料！", "注意");
                return;
            }
            string wzbh = ly_inma0010_returnDataGridView.CurrentRow.Cells["物资编号_f5"].Value.ToString();

            string wzbh_qty = ly_inma0010_returnDataGridView.CurrentRow.Cells["数量_f5"].Value.ToString();


            string parent_id = ly_production_orderDataGridView.CurrentRow.Cells["改制编号"].Value.ToString();
            string task_id = ly_production_orderDataGridView.CurrentRow.Cells["任务单号"].Value.ToString();
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
                string sql = "select material_flag, request_qty from ly_Restructuring_return_single where task_inspection=" + task_inspection + " and itemno='" + wzbh + "'";

                SqlDataAdapter adapter = new SqlDataAdapter(sql, connection);
                DataSet ds = new DataSet();
                adapter.Fill(ds);
                dt = ds.Tables[0];
            }


            DataTable dt2 = null;
            using (SqlConnection connection = new SqlConnection(SQLDatabase.Connectstring))
            {
                string sql = @"(SELECT pruductionTaskInspection_num, wzbh, out_level, (0-ISNULL(SUM(qty), 0)) AS in_qty  FROM ly_store_out where pruductionTaskInspection_num = '" +
                                task_id + "' and wzbh = '" + wzbh + "' and out_level = 'returnsingle'   GROUP BY pruductionTaskInspection_num, wzbh, out_level)";

                SqlDataAdapter adapter = new SqlDataAdapter(sql, connection);
                DataSet ds = new DataSet();
                adapter.Fill(ds);
                dt2 = ds.Tables[0];
            }

            ChangeValue queryForm = new ChangeValue();
            queryForm.Text = "退料数量";
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
                    decimal a = decimal.Parse(dt2.Rows[0]["in_qty"].ToString());
                    decimal b = decimal.Parse(queryForm.NewValue);
                    if (a >= b)
                    {
                        MessageBox.Show("重新退料数超过已经领料数量！", "注意"); return;
                    }
                }
            }
            else
            {
                return;
            }

            if (queryForm.OldValue == "0")
            {
 

                this.ly_Restructuring_return_singleBindingSource.AddNew();


                this.ly_Restructuring_return_singleDataGridView.CurrentRow.Cells["改制编号_f3"].Value = parent_id;
                this.ly_Restructuring_return_singleDataGridView.CurrentRow.Cells["任务单号_f3"].Value = task_id;
                this.ly_Restructuring_return_singleDataGridView.CurrentRow.Cells["物资编号_f3"].Value = wzbh;
                this.ly_Restructuring_return_singleDataGridView.CurrentRow.Cells["机器码_f3"].Value = machine_num;
                this.ly_Restructuring_return_singleDataGridView.CurrentRow.Cells["数量_f3"].Value = decimal.Parse(queryForm.NewValue);
                this.ly_Restructuring_return_singleDataGridView.CurrentRow.Cells["机器码_f3"].Value = machine_num;
                this.ly_Restructuring_return_singleDataGridView.CurrentRow.Cells["追领时间_f3"].Value = SQLDatabase.GetNowdate().ToString(); ;
                this.ly_Restructuring_return_singleDataGridView.CurrentRow.Cells["提交号_f3"].Value = task_inspection;

                this.ly_Restructuring_return_singleDataGridView.EndEdit();

                this.ly_Restructuring_return_singleBindingSource.EndEdit();
                this.Validate();
                this.ly_Restructuring_return_singleTableAdapter.Update(this.lYProductMange.ly_Restructuring_return_single);


            }
            else
            {
              
                if (dt.Rows[0]["material_flag"].ToString() == "True")
                {
                    MessageBox.Show("请联系质检取消审核！", "注意"); return;
                }

                using (SqlConnection connection = new SqlConnection(SQLDatabase.Connectstring))
                {

                    string sql = "update ly_Restructuring_return_single set request_qty=" + decimal.Parse(queryForm.NewValue) + " where task_inspection=" + task_inspection + " and itemno='" + wzbh + "'";
                    using (SqlCommand cmd = new SqlCommand(sql, connection))
                    {

                        connection.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            if (ly_production_task_inspectionDataGridView.CurrentRow == null)
            {
                return;
            }
            string tjh = ly_production_task_inspectionDataGridView.CurrentRow.Cells["提交号"].Value.ToString();
            ly_Restructuring_return_singleTableAdapter.Fill(lYProductMange.ly_Restructuring_return_single, int.Parse(tjh));



        }

        private void toolStripButton26_Click(object sender, EventArgs e)
        {
            ReLoad();
        }

        private void toolStripButton33_Click(object sender, EventArgs e)
        {
            if (null == this.ly_Restructuring_return_singleDataGridView.CurrentRow) return;


            if ("True" == ly_Restructuring_return_singleDataGridView.CurrentRow.Cells["审批_f3"].Value.ToString())
            {
                MessageBox.Show("已经审批，不能删除(实需删除，请先清除质检审批标记)", "注意");
                return;

            }
            string id = ly_Restructuring_return_singleDataGridView.CurrentRow.Cells["id_f3"].Value.ToString();



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

                    string sql = "delete from  ly_Restructuring_return_single  where id=" + id;
                    using (SqlCommand cmd = new SqlCommand(sql, connection))
                    {

                        connection.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
                if (ly_production_task_inspectionDataGridView.CurrentRow == null)
                {
                    return;
                }
                string tjh = ly_production_task_inspectionDataGridView.CurrentRow.Cells["提交号"].Value.ToString();
                ly_Restructuring_return_singleTableAdapter.Fill(lYProductMange.ly_Restructuring_return_single, int.Parse(tjh));
            }
        }

        private void toolStripTextBox8_KeyUp(object sender, KeyEventArgs e)
        {
            string filterString = GetDataGridviewMultiFilterString.DGVMultiFilterString(this.ly_Restructuring_return_singleDataGridView, this.toolStripTextBox8.Text);



            if (null == filterString)
                filterString = "";

            this.ly_Restructuring_return_singleBindingSource.Filter = filterString;
        }

        private void toolStripTextBox10_KeyUp(object sender, KeyEventArgs e)
        {
            string filterString = GetDataGridviewMultiFilterString.DGVMultiFilterString(this.ly_inma0010_returnDataGridView, this.toolStripTextBox10.Text);

            if (null == filterString)
                filterString = "";

            this.ly_inma0010_returnBindingSource.Filter = filterString;
        }

        private void toolStripButton18_Click(object sender, EventArgs e)
        {
            ReLoad();
        }

        private void toolStripButton34_Click(object sender, EventArgs e)
        {
            ReLoad();
        }

        private void toolStripButton42_Click(object sender, EventArgs e)
        {
            ReLoad();
        }

        private void ly_Restructuring_return_stdInspectionDataGridView_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            DataGridView dgv = sender as DataGridView;

            if ("True" == dgv.CurrentRow.Cells["审核_f66"].Value.ToString())
            {
                MessageBox.Show("已经质检合格，不能修改(实需修改，请先清除质检合格标记)", "注意");
                return;

            }

            //if ("True" == dgv.CurrentRow.Cells["入库"].Value.ToString())
            //{
            //    MessageBox.Show("已经入库，不能修改(实需修改，请先删除该质检单号的入库记录)", "注意");
            //    return;

            //}

            string inspector = this.ly_production_task_inspectionDataGridView.CurrentRow.Cells["提交人"].Value.ToString();

            if (inspector != SQLDatabase.nowUserName())
            {
                MessageBox.Show("请提交人:" + inspector + "修改", "注意");
                return;
            }

            if ("免退" == dgv.CurrentCell.OwningColumn.Name)
            {

                ChangeValue queryForm = new ChangeValue();

                queryForm.OldValue = dgv.CurrentCell.Value.ToString();
                queryForm.NewValue = "";
                queryForm.ChangeMode = "value";
                queryForm.ShowDialog();


                if (queryForm.NewValue != "")
                {
                    if ((decimal.Parse(queryForm.NewValue) <= decimal.Parse(dgv.CurrentRow.Cells["需退"].Value.ToString())))
                    {
                        dgv.CurrentRow.Cells["免退"].Value = queryForm.NewValue;
                    }
                    else
                    {
                        MessageBox.Show("免退数必须小于退料总数！", "注意");
                        return;
                    }
                }
                else
                {

                    dgv.CurrentRow.Cells["免退"].Value = "0.00";
                }

                this.ly_Restructuring_return_stdInspectionDataGridView.EndEdit();

                this.Validate();
                this.ly_Restructuring_return_stdInspectionBindingSource.EndEdit();

                this.ly_Restructuring_return_stdInspectionTableAdapter.Update(this.lYProductMange.ly_Restructuring_return_stdInspection);
                return;
            }

            if ("免退说明" == dgv.CurrentCell.OwningColumn.Name)
            {

                ChangeValue queryForm = new ChangeValue();

                queryForm.OldValue = dgv.CurrentCell.Value.ToString();
                queryForm.NewValue = "";
                queryForm.ChangeMode = "string";
                queryForm.ShowDialog();


                if (queryForm.NewValue != "")
                {
                    dgv.CurrentRow.Cells["免退说明"].Value = queryForm.NewValue;
                }
                else
                {

                    dgv.CurrentRow.Cells["免退说明"].Value = "未说明";
                }

                this.ly_Restructuring_return_stdInspectionDataGridView.EndEdit();

                this.Validate();
                this.ly_Restructuring_return_stdInspectionBindingSource.EndEdit();

                this.ly_Restructuring_return_stdInspectionTableAdapter.Update(this.lYProductMange.ly_Restructuring_return_stdInspection);
                return;
            }

            if ("免退提交" == dgv.CurrentCell.OwningColumn.Name)
            {

                if (dgv.CurrentRow.Cells["免退"].Value.ToString() == "")
                {
                    MessageBox.Show("免退数必须大于退料总数！", "注意");
                    return;
                }

                if ("True" == dgv.CurrentRow.Cells["免退提交"].Value.ToString())
                {
                    dgv.CurrentRow.Cells["免退提交"].Value = "False";

                }
                else
                {

                    dgv.CurrentRow.Cells["免退提交"].Value = "True";

                }


                this.ly_Restructuring_return_stdInspectionDataGridView.EndEdit();

                this.Validate();
                this.ly_Restructuring_return_stdInspectionBindingSource.EndEdit();

                this.ly_Restructuring_return_stdInspectionTableAdapter.Update(this.lYProductMange.ly_Restructuring_return_stdInspection);
                return;
            }

        }

        private void dataGridView3_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (ly_production_task_inspectionDataGridView.CurrentRow == null
                     || ly_production_orderDataGridView.CurrentRow == null)
            {
                MessageBox.Show("请选择一项提交记录！", "注意");
                return;
            }
            if (dataGridView3.CurrentRow == null)
            {
                MessageBox.Show("请选择一项打废领料！", "注意");
                return;
            }
            string wzbh = dataGridView3.CurrentRow.Cells["物资编号_废"].Value.ToString();

            string wzbh_qty = dataGridView3.CurrentRow.Cells["数量_废"].Value.ToString();


            string parent_id = ly_production_orderDataGridView.CurrentRow.Cells["改制编号"].Value.ToString();
            string task_id = ly_production_orderDataGridView.CurrentRow.Cells["任务单号"].Value.ToString();
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
                string sql = "select * from ly_Restructuring_waste  where inspection_id=" + task_inspection + " and itemno='" + wzbh + "'";

                SqlDataAdapter adapter = new SqlDataAdapter(sql, connection);
                DataSet ds = new DataSet();
                adapter.Fill(ds);
                dt = ds.Tables[0];
            }

            ChangeValue queryForm = new ChangeValue();
            queryForm.Text = "废料数量";
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

                decimal total = 0;
                for (int i = 0; i < dataGridView3.Rows.Count; i++)
                {
                    if (dataGridView3.Rows[i].Cells["物资编号_废"].Value.ToString() == wzbh)
                    {

                        total += decimal.Parse(dataGridView3.Rows[i].Cells["数量_废"].Value.ToString());
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
 
 
                using (SqlConnection connection = new SqlConnection(SQLDatabase.Connectstring))
                {

                    string sql = @"insert into ly_Restructuring_waste (inspection_id,parent_id,pruductionOrder_num,itemno,request_qty,approve_flag,submit)values(" +
                        task_inspection + "," + parent_id + ",'" + task_id + "','" + wzbh + "'," + decimal.Parse(queryForm.NewValue) + ",0,1)";
                    using (SqlCommand cmd = new SqlCommand(sql, connection))
                    {

                        connection.Open();
                        cmd.ExecuteNonQuery();
                    }
                }





            }
            else
            {
                 


                if (dt.Rows[0]["approve_flag"].ToString() == "True")
                {
                    MessageBox.Show("请联系质检取消审核！", "注意"); return;
                }

                using (SqlConnection connection = new SqlConnection(SQLDatabase.Connectstring))
                {

                    string sql = "update ly_Restructuring_waste  set request_qty=" + decimal.Parse(queryForm.NewValue) + " where inspection_id=" + task_inspection + " and itemno='" + wzbh + "'";
                    using (SqlCommand cmd = new SqlCommand(sql, connection))
                    {

                        connection.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            if (ly_production_task_inspectionDataGridView.CurrentRow == null)
            {
                return;
            }
            string tjh = ly_production_task_inspectionDataGridView.CurrentRow.Cells["提交号"].Value.ToString();


            ly_Restructuring_waste_newTableAdapter.Fill(lYProductMange.ly_Restructuring_waste_new, int.Parse(tjh));//物料打废


        }

        private void toolStripButton70_Click(object sender, EventArgs e)
        {
            
        }

        private void toolStripButton69_Click(object sender, EventArgs e)
        {
            if (null == this.ly_Restructuring_waste_newDataGridView.CurrentRow) return;


            if ("True" == ly_Restructuring_waste_newDataGridView.CurrentRow.Cells["审核_打废"].Value.ToString())
            {
                MessageBox.Show("已经审批，不能删除(实需删除，请先清除质检审批标记)", "注意");
                return;

            }
            string id = ly_Restructuring_waste_newDataGridView.CurrentRow.Cells["编号_打废"].Value.ToString();



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

                    string sql = "delete from  ly_Restructuring_waste  where id=" + id;
                    using (SqlCommand cmd = new SqlCommand(sql, connection))
                    {

                        connection.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
                if (ly_production_task_inspectionDataGridView.CurrentRow == null)
                {
                    return;
                }
                string tjh = ly_production_task_inspectionDataGridView.CurrentRow.Cells["提交号"].Value.ToString();
                ly_Restructuring_waste_newTableAdapter.Fill(lYProductMange.ly_Restructuring_waste_new, int.Parse(tjh));
            }
        }

        private void ly_Restructuring_waste_newDataGridView_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (null == this.ly_Restructuring_waste_newDataGridView.CurrentRow) return;

            DataGridView dgv = sender as DataGridView;
            if ("True" == ly_Restructuring_waste_newDataGridView.CurrentRow.Cells["审核_打废"].Value.ToString())
            {
                MessageBox.Show("已经审批，不能修改(实需修改，请先清除质检审批标记)", "注意");
                return;
            }
            if ("提交打废" == dgv.CurrentCell.OwningColumn.Name)
            {

                if (string.IsNullOrEmpty(dgv.CurrentRow.Cells["打废说明"].Value.ToString()))
                {
                    MessageBox.Show("请先填写打废说明", "注意");
                    return;
                }

                if ("True" == dgv.CurrentRow.Cells["提交打废"].Value.ToString())
                {
                    dgv.CurrentRow.Cells["提交打废"].Value = "False"; 
                }
                else
                {
                    dgv.CurrentRow.Cells["提交打废"].Value = "True";                
                }

                this.ly_Restructuring_waste_newDataGridView.EndEdit();

                this.Validate();
                this.ly_Restructuring_waste_newBindingSource.EndEdit();

                this.ly_Restructuring_waste_newTableAdapter.Update(this.lYProductMange.ly_Restructuring_waste_new);


          

                return;
            }

            if ("打废说明" == dgv.CurrentCell.OwningColumn.Name)
            {

                ChangeValue queryForm = new ChangeValue();

                queryForm.OldValue = dgv.CurrentRow.Cells["打废说明"].Value.ToString();
                queryForm.NewValue = "";
                queryForm.ChangeMode = "string";
                queryForm.ShowDialog(); 

                if (queryForm.NewValue != "")
                {
                    dgv.CurrentRow.Cells["打废说明"].Value = queryForm.NewValue;

                }
                else
                {

                   dgv.CurrentRow.Cells["打废说明"].Value = DBNull.Value;
                }
                this.ly_Restructuring_waste_newDataGridView.EndEdit();

                this.Validate();
                this.ly_Restructuring_waste_newBindingSource.EndEdit();

                this.ly_Restructuring_waste_newTableAdapter.Update(this.lYProductMange.ly_Restructuring_waste_new);



                return;
            }

        }
    }
}
