using System;
 
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Transactions;
using HappyYF.Infrastructure.Repositories;
 
using DataGridFilter;

namespace HappyYF.YuanXin.WorkSet
{
    public partial class LY_Restructuring_Benchwork_StoreOut : Form
    {

        int comboMainindex = 0;
        int comboStanderindex = 0;
        int comboSingleindex = 0;
        int comboRstandindex = 0;
        int comboRSingle = 0;
        public LY_Restructuring_Benchwork_StoreOut()
        {
            InitializeComponent();
        }

        private void LY_QualityInspection_Benchwork_Load(object sender, EventArgs e)
        {
            this.dateTimePicker1.Text = DateTime.Today.AddMonths(-6).Date.ToString();
            this.dateTimePicker2.Text = DateTime.Today.AddDays(0).Date.ToString();


            this.ly_restructuring_task_inspectionTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            this.ly_restructuring_task_inspection_selTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;

            this.lY_restructuring_periodTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;

            //-------------
       
            this.ly_Restructuring_requestMainStroeTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            this.ly_Restructuring_requestDetStroeTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            ly_store_out_restructuringMainTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            ly_store_in_restructuringMainTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            this.ly_Restructuring_inTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            this.ly_Restructuring_outTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring; 
            this.ly_restructuring_employWarehouseTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring; 
            ly_Restructuring_request_singleTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            ly_Restructuring_return_singleTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring; 
            ly_Restructuring_return_stdInspectionTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;

        
            //------------



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
            ReloadMain();
        }

        protected void ReloadMain()
        {
            if (null != this.ly_production_orderDataGridView.CurrentRow)
            {


                string taskMumber = this.ly_production_orderDataGridView.CurrentRow.Cells["跟单编号_Main"].Value.ToString();

                this.ly_restructuring_task_inspectionTableAdapter.Fill(this.lYProductMange.ly_restructuring_task_inspection, taskMumber);

                this.ly_restructuring_task_inspection_selTableAdapter.Fill(this.lYProductMange.ly_restructuring_task_inspection_sel, taskMumber);

                string taskParentStr = ly_production_orderDataGridView.CurrentRow.Cells["改制编号_Main"].Value.ToString();

                if (taskMumber == "" || taskParentStr == "")
                {
                    //以防不测
                    this.ly_Restructuring_requestMainStroeTableAdapter.Fill(this.lYProductMange.ly_Restructuring_requestMainStroe, -1, "a123"); //主领料
                    this.ly_Restructuring_requestDetStroeTableAdapter.Fill(this.lYProductMange.ly_Restructuring_requestDetStroe, -1, "a123"); //副领料 
                }
                else
                {
                    int taskParent = int.Parse(taskParentStr);
                    this.ly_Restructuring_requestMainStroeTableAdapter.Fill(this.lYProductMange.ly_Restructuring_requestMainStroe, taskParent, taskMumber); //主领料
                    this.ly_Restructuring_requestDetStroeTableAdapter.Fill(this.lYProductMange.ly_Restructuring_requestDetStroe, taskParent, taskMumber);//副领料

                }







                string workName = ly_production_orderDataGridView.CurrentRow.Cells["工人_Main"].Value.ToString();

                ly_store_out_restructuringMainTableAdapter.Fill(this.lYSalseRepair.ly_store_out_restructuringMain, workName, taskMumber, SQLDatabase.NowUserID);
                ly_store_in_restructuringMainTableAdapter.Fill(this.lYSalseRepair.ly_store_in_restructuringMain, workName, taskMumber, SQLDatabase.NowUserID);





                if (null != this.dataGridView2.CurrentRow)
                {

                    string outNum = this.dataGridView2.CurrentRow.Cells["out_number_new"].Value.ToString();


                    this.ly_Restructuring_inTableAdapter.Fill(this.lYStoreMange.ly_Restructuring_in, outNum, SQLDatabase.nowUserName());
                }
                else
                {
                    this.ly_Restructuring_inTableAdapter.Fill(this.lYStoreMange.ly_Restructuring_in, "", SQLDatabase.nowUserName());
                }
                if (null != this.ly_store_outnumDataGridView.CurrentRow)
                {

                    string outNum = this.ly_store_outnumDataGridView.CurrentRow.Cells["out_number"].Value.ToString();


                    this.ly_Restructuring_outTableAdapter.Fill(this.lYStoreMange.ly_Restructuring_out, outNum, SQLDatabase.nowUserName());
                }
                else
                {

                    this.ly_Restructuring_outTableAdapter.Fill(this.lYStoreMange.ly_Restructuring_out, "", SQLDatabase.nowUserName());


                }


                this.ly_restructuring_employWarehouseTableAdapter.Fill(this.lYSalseRepair.ly_restructuring_employWarehouse, SQLDatabase.NowUserID);






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



        protected void ReLoad()
        {
            if (product_inspectionDataGridView2.CurrentRow == null )
            {

                ly_Restructuring_request_singleTableAdapter.Fill(lYProductMange.ly_Restructuring_request_single, -1);
                ly_Restructuring_return_singleTableAdapter.Fill(lYProductMange.ly_Restructuring_return_single, -1);
                ly_Restructuring_return_stdInspectionTableAdapter.Fill(lYProductMange.ly_Restructuring_return_stdInspection,"", -1);//标准退料

            }
            else
            {
                string tjh = product_inspectionDataGridView2.CurrentRow.Cells["提交号_single"].Value.ToString();
                ly_Restructuring_request_singleTableAdapter.Fill(lYProductMange.ly_Restructuring_request_single, int.Parse(tjh));  //追加领料
                ly_Restructuring_return_singleTableAdapter.Fill(lYProductMange.ly_Restructuring_return_single, int.Parse(tjh));//追加退料


                if (null != this.ly_production_orderDataGridView.CurrentRow)
                {


                    string taskMumber = this.ly_production_orderDataGridView.CurrentRow.Cells["跟单编号_Main"].Value.ToString();

                    ly_Restructuring_return_stdInspectionTableAdapter.Fill(lYProductMange.ly_Restructuring_return_stdInspection, taskMumber,int.Parse(tjh));//标准退料
                }
            }
        }




        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            if (ly_production_orderDataGridView.CurrentRow == null)
            {
                return;
            }
            if (ly_Restructuring_requestMainStroeDataGridView.Rows.Count <= 0)
            {
                MessageBox.Show("没有出库主物料记录！", "注意");
                return;
            }

            decimal canOut = decimal.Parse(ly_Restructuring_requestMainStroeDataGridView.CurrentRow.Cells["库存"].Value.ToString());//库存
            decimal notOut = decimal.Parse(ly_Restructuring_requestMainStroeDataGridView.CurrentRow.Cells["未领"].Value.ToString());//未领

            //库存为0不让领
            if (canOut <= 0)
            {
                MessageBox.Show("当前主物料库存为0！", "注意");
                return;
            }

            if (ly_production_task_inspectionDataGridView.Rows.Count <= 0)
            {
                MessageBox.Show("没有出库主物料明细记录！", "注意");
                return;
            }


            int k = 0;
            for (int i = 0; i < ly_production_task_inspectionDataGridView.Rows.Count; i++)
            {
                if (ly_production_task_inspectionDataGridView.Rows[i].Cells["出库"].Value.ToString() == "False"
                 && ly_production_task_inspectionDataGridView.Rows[i].Cells["设备编号"].Value.ToString().Trim() != "")
                {
                    k++; // 未出库，已设置编号
                }
            }

            if (k == 0)
            {
                MessageBox.Show("至少设置1条出库数据的机器码！", "注意");
                return;
            }
            //if (canOut >= notOut) //库存量够可以直接出
            //{
            //    if (k <notOut)
            //    {
            //        MessageBox.Show("请设置:" + notOut+ " 条出库数据的机器码！", "注意");
            //        return;
            //    }
            //}
            //else //库存量不够，先出库存剩余的
            //{
            //    if (k < canOut)
            //    {
            //        MessageBox.Show("库存量不够，可先出："+ canOut + "条，请设置:" + canOut + " 条出库机器码！", "注意");
            //        return;
            //    }
            //}


            string message = "未设置机器码的将不出库，确定领料出库吗?";
            string caption = "提示...";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result;



            result = MessageBox.Show(message, caption, buttons,
            MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

            if (result == DialogResult.Yes)
            {
                try
                {
                    CountStoreOutAuto();
                }
                catch (System.Exception ex)
                {
                    System.Windows.Forms.MessageBox.Show(ex.Message);
                }
            }


        }
        private void CountStoreOutAuto()
        {
            //---------------------------------------------------------------

            string id = ly_production_orderDataGridView.CurrentRow.Cells["id_Main_New"].Value.ToString();
            this.lY_restructuring_periodTableAdapter.Fill(this.lYProductMange.LY_restructuring_period, this.dateTimePicker1.Value, this.dateTimePicker2.Value.AddDays(1), "GQ", SQLDatabase.NowUserID);
            this.lY_restructuring_periodBindingSource.Position = lY_restructuring_periodBindingSource.Find("id", id);

            //---------------------------------------------------------------
            string taskCode = ly_production_orderDataGridView.CurrentRow.Cells["跟单编号_Main"].Value.ToString();
            string workName = ly_production_orderDataGridView.CurrentRow.Cells["工人_Main"].Value.ToString();

            string ReId = ly_Restructuring_requestMainStroeDataGridView.CurrentRow.Cells["子计划编号"].Value.ToString();

            string workCode = ly_production_orderDataGridView.CurrentRow.Cells["工号_Main"].Value.ToString();
          
    

            if (string.IsNullOrEmpty(workName) || string.IsNullOrEmpty(workCode))
            {
                MessageBox.Show("该工单没有排工人，无法出库！", "注意");
                return;
            }


            SqlConnection sqlConnection1 = new SqlConnection(SQLDatabase.Connectstring);
            SqlCommand cmd = new SqlCommand();

            cmd.CommandTimeout = 0;

            cmd.Parameters.Add("@pruductionOrder_num", SqlDbType.VarChar);
            cmd.Parameters["@pruductionOrder_num"].Value = taskCode;


            //cmd.Parameters.Add("@storecount", SqlDbType.Int);
            //cmd.Parameters["@storecount"].Value = outQty;





            string outNum = GetMaxOutNum();
            cmd.Parameters.Add("@out_number", SqlDbType.VarChar);
            cmd.Parameters["@out_number"].Value = outNum;

            cmd.Parameters.Add("@faliaoren", SqlDbType.VarChar);
            cmd.Parameters["@faliaoren"].Value = SQLDatabase.nowUserName();

            cmd.Parameters.Add("@workname", SqlDbType.VarChar);
            cmd.Parameters["@workname"].Value = workName;


            cmd.Parameters.Add("@workCode", SqlDbType.VarChar);
            cmd.Parameters["@workCode"].Value = workCode;

            cmd.Parameters.Add("@userID", SqlDbType.VarChar);
            cmd.Parameters["@userID"].Value = SQLDatabase.NowUserID;


            cmd.CommandText = "LY_store_out_restructuringMain";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = sqlConnection1;

            sqlConnection1.Open();
            NewFrm.Show(this);
            cmd.ExecuteNonQuery();
            NewFrm.Hide(this);
            sqlConnection1.Close();

            ReloadMain();
            ly_store_out_restructuringMainTableAdapter.Fill(this.lYSalseRepair.ly_store_out_restructuringMain, workName, taskCode, SQLDatabase.NowUserID);

        }

        private string GetMaxOutNum()
        {
            SqlConnection sqlConnection1 = new SqlConnection(SQLDatabase.Connectstring);
            SqlCommand cmd = new SqlCommand();

            string MaxPlanCode = "";


            cmd.CommandText = "LY_Get_OutNumber";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = sqlConnection1;

            sqlConnection1.Open();
            MaxPlanCode = cmd.ExecuteScalar().ToString();
            sqlConnection1.Close();



            return MaxPlanCode;
        }
        private void ly_production_task_inspectionDataGridView_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (null == this.ly_production_orderDataGridView.CurrentRow)
            {
                return;
            }
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

            if ("True" == dgv.CurrentRow.Cells["出库"].Value.ToString())
            {
                MessageBox.Show("已经出库，不能修改设备编号", "注意");
                return;
            }



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

                    if (queryForm.NewValue.Length < 3)
                    {
                        MessageBox.Show("设备编号过短...", "注意");
                        return;
                    }
                    string taskMumber = this.ly_production_orderDataGridView.CurrentRow.Cells["跟单编号_Main"].Value.ToString();
                    string tjh = dgv.CurrentRow.Cells["提交号"].Value.ToString();
                    string sql = "select count(1) from ly_restructuring_task_inspection where machine_num='"+ queryForm.NewValue + "' and id<>"+tjh+ " and pruductionOrder_num='"+taskMumber+"'";
                    int k = 0;
                    using (SqlConnection con = new SqlConnection(SQLDatabase.Connectstring))
                    {

                        using (SqlCommand cmd = new SqlCommand(sql, con))
                        {

                            con.Open();
                            k=Convert.ToInt32( cmd.ExecuteScalar().ToString());
                        }
                    }

                    if (k > 0)
                    {
                        MessageBox.Show("改任务单已经存在此机器号...", "注意");
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
            if ("新旧" == dgv.CurrentCell.OwningColumn.Name)
            {


                if ("True" == dgv.CurrentRow.Cells["提交"].Value.ToString())
                {
                    MessageBox.Show("已经提交,不能修改...", "注意");
                    return;

                }
          


                string sel = "SELECT oldnew  as 新旧 FROM ly_finishproduct_newold";


                QueryForm queryForm = new QueryForm();


                queryForm.Sel = sel;
                queryForm.Constr = SQLDatabase.Connectstring;



                queryForm.ShowDialog();

                if (!string.IsNullOrEmpty(queryForm.Result))
                {

                    dgv.CurrentRow.Cells["新旧"].Value = queryForm.Result;
                }
                else
                {
                    return;
                }

                if (!SaveChangedMachine())
                {

                }
                return;

            }
        }

 

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if (ly_Restructuring_requestMainStroeDataGridView.CurrentRow != null)
            {
                decimal outqty = decimal.Parse(ly_Restructuring_requestMainStroeDataGridView.CurrentRow.Cells["已领"].Value.ToString());//已领数量
                if (outqty <= 0)
                {
                    MessageBox.Show("为了准确核算成本，必须先领主料！", "注意");
                    return;
                }
            }


            if (ly_production_orderDataGridView.CurrentRow == null)
            {
                return;
            }
            if (dataGridView1.Rows.Count <= 0)
            {
                MessageBox.Show("没有出库副物料记录！", "注意");
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
                try
                {
                    CountStoreOutAutoDet();
                }
                catch (System.Exception ex)
                {
                    System.Windows.Forms.MessageBox.Show(ex.Message);
                }
             
            }
        }


        private void CountStoreOutAutoDet()
        {

            //---------------------------------------------------------------

            string id = ly_production_orderDataGridView.CurrentRow.Cells["id_Main_New"].Value.ToString();
            this.lY_restructuring_periodTableAdapter.Fill(this.lYProductMange.LY_restructuring_period, this.dateTimePicker1.Value, this.dateTimePicker2.Value.AddDays(1), "GQ", SQLDatabase.NowUserID);
            this.lY_restructuring_periodBindingSource.Position = lY_restructuring_periodBindingSource.Find("id", id);

            //---------------------------------------------------------------

            string taskCode = ly_production_orderDataGridView.CurrentRow.Cells["跟单编号_Main"].Value.ToString();
            string workName = ly_production_orderDataGridView.CurrentRow.Cells["工人_Main"].Value.ToString();
            string RestructId = ly_production_orderDataGridView.CurrentRow.Cells["改制编号_Main"].Value.ToString();
            string workCode = ly_production_orderDataGridView.CurrentRow.Cells["工号_Main"].Value.ToString();
            if (string.IsNullOrEmpty(workName) || string.IsNullOrEmpty(workCode))
            {
                MessageBox.Show("该工单没有排工人，无法出库！", "注意");
                return;
            }
            SqlConnection sqlConnection1 = new SqlConnection(SQLDatabase.Connectstring);
            SqlCommand cmd = new SqlCommand();



            cmd.Parameters.Add("@pruductionOrder_num", SqlDbType.VarChar);
            cmd.Parameters["@pruductionOrder_num"].Value = taskCode;


            cmd.Parameters.Add("@restructuring_id", SqlDbType.Int);
            cmd.Parameters["@restructuring_id"].Value = RestructId;

            string outNum = GetMaxOutNum();
            cmd.Parameters.Add("@out_number", SqlDbType.VarChar);
            cmd.Parameters["@out_number"].Value = outNum;

            cmd.Parameters.Add("@faliaoren", SqlDbType.VarChar);
            cmd.Parameters["@faliaoren"].Value = SQLDatabase.nowUserName();
            cmd.Parameters.Add("@workname", SqlDbType.VarChar);
            cmd.Parameters["@workname"].Value = workName;

            cmd.Parameters.Add("@workCode", SqlDbType.VarChar);
            cmd.Parameters["@workCode"].Value = workCode;
            cmd.Parameters.Add("@userID", SqlDbType.VarChar);
            cmd.Parameters["@userID"].Value = SQLDatabase.NowUserID;

            cmd.CommandText = "LY_store_out_restructuringDet";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = sqlConnection1;

            cmd.CommandTimeout = 0;

            sqlConnection1.Open();
            cmd.ExecuteNonQuery();
            sqlConnection1.Close();

            ReloadMain();
            ly_store_out_restructuringMainTableAdapter.Fill(this.lYSalseRepair.ly_store_out_restructuringMain, workName, taskCode, SQLDatabase.NowUserID);

        }

        private void product_inspectionDataGridView2_SelectionChanged(object sender, EventArgs e)
        {
            ReLoad();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            if (ly_Restructuring_requestMainStroeDataGridView.CurrentRow != null)
            {
                decimal outqty = decimal.Parse(ly_Restructuring_requestMainStroeDataGridView.CurrentRow.Cells["已领"].Value.ToString());//已领数量
                if (outqty <= 0)
                {
                    MessageBox.Show("为了准确核算成本，必须先领主料！", "注意");
                    return;
                }
            }
            if (ly_Restructuring_request_singleDataGridView.CurrentRow == null)
            {
                return;
            }
            if (ly_Restructuring_request_singleDataGridView.Rows.Count <= 0)
            {
                MessageBox.Show("没有追加领料记录！", "注意");
                return;
            }


            if (product_inspectionDataGridView2.CurrentRow == null)
            {
                return;
            }
            else
            {

                if (string.IsNullOrEmpty(product_inspectionDataGridView2.CurrentRow.Cells["设备编号_sg"].Value.ToString()))
                {

                    MessageBox.Show("请在任务主料中，设置该条机器码！", "注意");
                    return;
                }
            }

            string message = "确定领料出库吗?";
            string caption = "提示...";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result;



            result = MessageBox.Show(message, caption, buttons,
            MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

            if (result == DialogResult.Yes)
            {
                try
                {
                    CountStoreOutAutoSingle();
                }
                catch (System.Exception ex)
                {
                    System.Windows.Forms.MessageBox.Show(ex.Message);
                }


            }
        }

        private void CountStoreOutAutoSingle()
        {

             

            string taskCode = ly_production_orderDataGridView.CurrentRow.Cells["跟单编号_Main"].Value.ToString();
            string workName = ly_production_orderDataGridView.CurrentRow.Cells["工人_Main"].Value.ToString();
            string tjh = product_inspectionDataGridView2.CurrentRow.Cells["提交号_single"].Value.ToString();
            string workCode = ly_production_orderDataGridView.CurrentRow.Cells["工号_Main"].Value.ToString();

            if (string.IsNullOrEmpty(workName) || string.IsNullOrEmpty(workCode))
            {
                MessageBox.Show("该工单没有排工人，无法出库！", "注意");
                return;
            }
            SqlConnection sqlConnection1 = new SqlConnection(SQLDatabase.Connectstring);
            SqlCommand cmd = new SqlCommand();

            cmd.CommandTimeout = 0;

            cmd.Parameters.Add("@task_inspection", SqlDbType.Int);
            cmd.Parameters["@task_inspection"].Value = tjh;


            string outNum = GetMaxOutNum();
            cmd.Parameters.Add("@out_number", SqlDbType.VarChar);
            cmd.Parameters["@out_number"].Value = outNum;

            cmd.Parameters.Add("@faliaoren", SqlDbType.VarChar);
            cmd.Parameters["@faliaoren"].Value = SQLDatabase.nowUserName();

            cmd.Parameters.Add("@workname", SqlDbType.VarChar);
            cmd.Parameters["@workname"].Value = workName;
            cmd.Parameters.Add("@workCode", SqlDbType.VarChar);
            cmd.Parameters["@workCode"].Value = workCode;
            cmd.Parameters.Add("@userID", SqlDbType.VarChar);
            cmd.Parameters["@userID"].Value = SQLDatabase.NowUserID;

            cmd.CommandText = "LY_store_out_restructuringSingle";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = sqlConnection1;

            sqlConnection1.Open();
            cmd.ExecuteNonQuery();
            sqlConnection1.Close();
           //aaa
            ReLoad();
            ly_store_out_restructuringMainTableAdapter.Fill(this.lYSalseRepair.ly_store_out_restructuringMain, workName, taskCode, SQLDatabase.NowUserID);

        }

    

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
          
        }

        private void ly_store_outnumDataGridView_SelectionChanged(object sender, EventArgs e)
        {

            if (null != this.ly_store_outnumDataGridView.CurrentRow)
            {

                string outNum = this.ly_store_outnumDataGridView.CurrentRow.Cells["out_number"].Value.ToString();


                this.ly_Restructuring_outTableAdapter.Fill(this.lYStoreMange.ly_Restructuring_out, outNum, SQLDatabase.nowUserName());
            }
        }

        private void tabControl3_SelectedIndexChanged(object sender, EventArgs e)
        {
            TabControl nowtb = sender as TabControl;

            if (0 == nowtb.SelectedIndex)
            {

                this.tabControl2.SelectedIndex = 0;

            }

            else
            {
                this.tabControl2.SelectedIndex = 1;
            }
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            if (ly_Restructuring_requestMainStroeDataGridView.CurrentRow != null)
            {
                decimal outqty = decimal.Parse(ly_Restructuring_requestMainStroeDataGridView.CurrentRow.Cells["已领"].Value.ToString());//已领数量
                if (outqty <= 0)
                {
                    MessageBox.Show("为了准确核算成本，必须先领主料！", "注意");
                    return;
                }
            }
            if (ly_Restructuring_return_stdInspectionDataGridView.CurrentRow == null)
            {
                return;
            }
            if (ly_Restructuring_return_stdInspectionDataGridView.Rows.Count <= 0)
            {
                MessageBox.Show("没有标准退料记录！", "注意");
                return;
            }
            if (product_inspectionDataGridView2.CurrentRow == null)
            {
                return;
            }
            else
            {

                if (string.IsNullOrEmpty(product_inspectionDataGridView2.CurrentRow.Cells["设备编号_sg"].Value.ToString()))
                {

                    MessageBox.Show("请在任务主料中，设置该条机器码！", "注意");
                    return;
                }
            }
            string message = "确定退料吗?";
            string caption = "提示...";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result;



            result = MessageBox.Show(message, caption, buttons,
            MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

            if (result == DialogResult.Yes)
            {

                CountStoreInAutoStand();
            }

        }

        private void CountStoreInAutoStand()
        {

            
            string taskCode = ly_production_orderDataGridView.CurrentRow.Cells["跟单编号_Main"].Value.ToString();
            string workName = ly_production_orderDataGridView.CurrentRow.Cells["工人_Main"].Value.ToString();
            string tjh = product_inspectionDataGridView2.CurrentRow.Cells["提交号_single"].Value.ToString();

            string workCode = ly_production_orderDataGridView.CurrentRow.Cells["工号_Main"].Value.ToString();
            if (string.IsNullOrEmpty(workName) || string.IsNullOrEmpty(workCode))
            {
                MessageBox.Show("该工单没有排工人，无法出库！", "注意");
                return;
            }
            SqlConnection sqlConnection1 = new SqlConnection(SQLDatabase.Connectstring);
            SqlCommand cmd = new SqlCommand();

            cmd.CommandTimeout = 0;

            cmd.Parameters.Add("@task_inspection", SqlDbType.Int);
            cmd.Parameters["@task_inspection"].Value = tjh;


            string outNum = GetMaxOutNum();
            cmd.Parameters.Add("@out_number", SqlDbType.VarChar);
            cmd.Parameters["@out_number"].Value = outNum;

            cmd.Parameters.Add("@faliaoren", SqlDbType.VarChar);
            cmd.Parameters["@faliaoren"].Value = SQLDatabase.nowUserName();
            cmd.Parameters.Add("@workname", SqlDbType.VarChar);
            cmd.Parameters["@workname"].Value = workName;

            cmd.Parameters.Add("@taskcode", SqlDbType.VarChar);
            cmd.Parameters["@taskcode"].Value = taskCode;

            cmd.Parameters.Add("@workCode", SqlDbType.VarChar);
            cmd.Parameters["@workCode"].Value = workCode;
            cmd.Parameters.Add("@userID", SqlDbType.VarChar);
            cmd.Parameters["@userID"].Value = SQLDatabase.NowUserID;
            cmd.CommandText = "LY_store_In_restructuringStand";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = sqlConnection1;

            sqlConnection1.Open();
            cmd.ExecuteNonQuery();
            sqlConnection1.Close();

            ReLoad();
            ly_store_in_restructuringMainTableAdapter.Fill(this.lYSalseRepair.ly_store_in_restructuringMain, workName, taskCode, SQLDatabase.NowUserID);

        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            if (ly_Restructuring_requestMainStroeDataGridView.CurrentRow != null)
            {
                decimal outqty = decimal.Parse(ly_Restructuring_requestMainStroeDataGridView.CurrentRow.Cells["已领"].Value.ToString());//已领数量
                if (outqty <= 0)
                {
                    MessageBox.Show("为了准确核算成本，必须先领主料！", "注意");
                    return;
                }
            }
            if (ly_Restructuring_return_singleDataGridView.CurrentRow == null)
            {
                return;
            }
            if (ly_Restructuring_return_singleDataGridView.Rows.Count <= 0)
            {
                MessageBox.Show("没有退料记录！", "注意");
                return;
            }
            if (product_inspectionDataGridView2.CurrentRow == null)
            {
                return;
            }
            else
            {

                if (string.IsNullOrEmpty(product_inspectionDataGridView2.CurrentRow.Cells["设备编号_sg"].Value.ToString()))
                {

                    MessageBox.Show("请在任务主料中，设置该条机器码！", "注意");
                    return;
                }
            }
            string message = "确定退料吗?";
            string caption = "提示...";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result;



            result = MessageBox.Show(message, caption, buttons,
            MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

            if (result == DialogResult.Yes)
            {

                CountStoreInAutoSingle();
            }
        }


        private void CountStoreInAutoSingle()
        {

          
            string taskCode = ly_production_orderDataGridView.CurrentRow.Cells["跟单编号_Main"].Value.ToString();
            string workName = ly_production_orderDataGridView.CurrentRow.Cells["工人_Main"].Value.ToString();
            string tjh = product_inspectionDataGridView2.CurrentRow.Cells["提交号_single"].Value.ToString();
            string workCode = ly_production_orderDataGridView.CurrentRow.Cells["工号_Main"].Value.ToString();
            if (string.IsNullOrEmpty(workName) || string.IsNullOrEmpty(workCode))
            {
                MessageBox.Show("该工单没有排工人，无法出库！", "注意");
                return;
            }
            SqlConnection sqlConnection1 = new SqlConnection(SQLDatabase.Connectstring);
            SqlCommand cmd = new SqlCommand();

            cmd.Parameters.Add("@workCode", SqlDbType.VarChar);
            cmd.Parameters["@workCode"].Value = workCode;

            cmd.Parameters.Add("@task_inspection", SqlDbType.Int);
            cmd.Parameters["@task_inspection"].Value = tjh;


            string outNum = GetMaxOutNum();
            cmd.Parameters.Add("@out_number", SqlDbType.VarChar);
            cmd.Parameters["@out_number"].Value = outNum;

            cmd.Parameters.Add("@faliaoren", SqlDbType.VarChar);
            cmd.Parameters["@faliaoren"].Value = SQLDatabase.nowUserName();
            cmd.Parameters.Add("@workname", SqlDbType.VarChar);
            cmd.Parameters["@workname"].Value = workName;

            cmd.Parameters.Add("@taskcode", SqlDbType.VarChar);
            cmd.Parameters["@taskcode"].Value = taskCode;

            cmd.Parameters.Add("@userID", SqlDbType.VarChar);
            cmd.Parameters["@userID"].Value = SQLDatabase.NowUserID;

            cmd.CommandText = "LY_store_In_restructuringSingle";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = sqlConnection1;

            sqlConnection1.Open();
            cmd.ExecuteNonQuery();
            sqlConnection1.Close();

            ReLoad();
            ly_store_in_restructuringMainTableAdapter.Fill(this.lYSalseRepair.ly_store_in_restructuringMain, workName, taskCode, SQLDatabase.NowUserID);

        }

        private void dataGridView2_SelectionChanged(object sender, EventArgs e)
        {
            if (null != this.dataGridView2.CurrentRow)
            {

                string outNum = this.dataGridView2.CurrentRow.Cells["out_number_new"].Value.ToString();


                this.ly_Restructuring_inTableAdapter.Fill(this.lYStoreMange.ly_Restructuring_in, outNum, SQLDatabase.nowUserName());
            }
        }

        private void toolStripButton17_Click(object sender, EventArgs e)
        {
            // 领料单
            if (null == this.ly_store_outDataGridView.CurrentRow) return;


            BaseReportView queryForm = new BaseReportView();

            queryForm.Text = "中原精密维修领料单";

            queryForm.Printdata = this.lYStoreMange;


            queryForm.PrintCrystalReport = new LY_lingliaodan_GQ();


            queryForm.ShowDialog();
        }

        private void toolStripButton18_Click(object sender, EventArgs e)
        {
            //欠料单
        }

        private void 打印PToolStripButton_Click(object sender, EventArgs e)
        {
            // 领料单
            if (null == this.dataGridView3.CurrentRow) return;


            BaseReportView queryForm = new BaseReportView();

            queryForm.Text = "中原精密维修退料单";

            queryForm.Printdata = this.lYStoreMange;


            queryForm.PrintCrystalReport = new LY_tuiliaodan_GQ();


            queryForm.ShowDialog();
        }


        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (null == this.ly_store_outnumDataGridView.CurrentRow) return;


            if (SQLDatabase.nowUserName() != ly_store_outnumDataGridView.CurrentRow.Cells["发料人"].Value.ToString())
            {

                MessageBox.Show("请发料人:" + ly_store_outnumDataGridView.CurrentRow.Cells["发料人"].Value.ToString() + " 删除");

                return;
            }

            string outnumber = this.ly_store_outnumDataGridView.CurrentRow.Cells["out_number"].Value.ToString();
            string nowwarehouse = this.ly_store_outnumDataGridView.CurrentRow.Cells["warehouse"].Value.ToString();

            if ("True" == ly_store_outnumDataGridView.CurrentRow.Cells["finished"].Value.ToString())
            {
                MessageBox.Show("已经签证,领料单不能删除...");
                return;

            }


            string message = "删除当前领料单:" + outnumber + "吗？";
            string caption = "提示...";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;

            DialogResult result;



            result = MessageBox.Show(message, caption, buttons,
            MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

            if (result == DialogResult.Yes)
            {
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

                    this.ly_store_out_restructuringMainBindingSource.RemoveCurrent();


                }
                ReloadMain();
                if (null == this.ly_store_outnumDataGridView.CurrentRow)
                {
                    this.ly_Restructuring_outTableAdapter.Fill(this.lYStoreMange.ly_Restructuring_out, "-1", "-1");
                }

            }

        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {

            if (null == this.ly_store_outnumDataGridView.CurrentRow) return;
            if (null == this.ly_store_outDataGridView.CurrentRow) return;

            if (SQLDatabase.nowUserName() != ly_store_outnumDataGridView.CurrentRow.Cells["发料人"].Value.ToString())
            {
                MessageBox.Show("请发料人:" + ly_store_outnumDataGridView.CurrentRow.Cells["发料人"].Value.ToString() + " 删除");
                return;
            }
            if (SQLDatabase.nowUserName() != ly_store_outDataGridView.CurrentRow.Cells["发料人22"].Value.ToString())
            {
                MessageBox.Show("请发料人:" + ly_store_outDataGridView.CurrentRow.Cells["发料人22"].Value.ToString() + " 删除");
                return;
            }
            //string outnumber = this.ly_store_outnumDataGridView.CurrentRow.Cells["out_number"].Value.ToString();
            //string nowwarehouse = this.ly_store_outnumDataGridView.CurrentRow.Cells["warehouse"].Value.ToString();

            if ("True" == ly_store_outnumDataGridView.CurrentRow.Cells["finished"].Value.ToString())
            {
                MessageBox.Show("已经签证,领料单不能删除...");
                return;

            }
            int nowId = int.Parse(this.ly_store_outDataGridView.CurrentRow.Cells["idout"].Value.ToString());

            string componentNum = this.ly_store_outDataGridView.CurrentRow.Cells["物料编号"].Value.ToString();


            string message1 = "当前(物料：" + componentNum + ")将被删除，继续吗？";
            string caption1 = "提示...";
            MessageBoxButtons buttons1 = MessageBoxButtons.YesNo;

            DialogResult result1;

            result1 = MessageBox.Show(message1, caption1, buttons1,
            MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
            if (result1 == DialogResult.Yes)
            {
                this.ly_Restructuring_outBindingSource.RemoveCurrent();
                this.ly_store_outDataGridView.EndEdit();
                this.ly_Restructuring_outBindingSource.EndEdit();
                this.ly_Restructuring_outTableAdapter.Update(this.lYStoreMange.ly_Restructuring_out);
                ReloadMain();
            }


        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            if (null == this.dataGridView2.CurrentRow) return;


            if (SQLDatabase.nowUserName() != dataGridView2.CurrentRow.Cells["收料人_T"].Value.ToString())
            {

                MessageBox.Show("请收料人:" + dataGridView2.CurrentRow.Cells["收料人_T"].Value.ToString() + " 删除");

                return;
            }

            string outnumber = this.dataGridView2.CurrentRow.Cells["out_number_new"].Value.ToString();
            string nowwarehouse = this.dataGridView2.CurrentRow.Cells["仓库_T"].Value.ToString();

            if ("True" == dataGridView2.CurrentRow.Cells["签证_T"].Value.ToString())
            {
                MessageBox.Show("已经签证,退料单不能删除...");
                return;

            }


            string message = "删除当前退料单:" + outnumber + "吗？";
            string caption = "提示...";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;

            DialogResult result;



            result = MessageBox.Show(message, caption, buttons,
            MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

            if (result == DialogResult.Yes)
            {
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

                    this.lystoreinrestructuringMainBindingSource.RemoveCurrent();

                }
                ReLoad();
                if (null == this.dataGridView2.CurrentRow)
                {
                    this.ly_Restructuring_inTableAdapter.Fill(this.lYStoreMange.ly_Restructuring_in, "-1", "-1");

                }

            }
        }

        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            if (null == this.dataGridView2.CurrentRow) return;
            if (null == this.dataGridView3.CurrentRow) return;

            if (SQLDatabase.nowUserName() != dataGridView2.CurrentRow.Cells["收料人_T"].Value.ToString())
            {

                MessageBox.Show("请收料人:" + dataGridView2.CurrentRow.Cells["收料人_T"].Value.ToString() + " 删除");

                return;
            }
            if (SQLDatabase.nowUserName() != dataGridView3.CurrentRow.Cells["收料人22"].Value.ToString())
            {

                MessageBox.Show("请收料人:" + dataGridView3.CurrentRow.Cells["收料人22"].Value.ToString() + " 删除");

                return;
            }


            if ("True" == dataGridView2.CurrentRow.Cells["签证_T"].Value.ToString())
            {
                MessageBox.Show("已经签证,退料单不能删除...");
                return;

            }



            int nowId = int.Parse(this.dataGridView3.CurrentRow.Cells["idout_new"].Value.ToString());

            string componentNum = this.dataGridView3.CurrentRow.Cells["物料编号_new"].Value.ToString();


            string message1 = "当前(物料：" + componentNum + ")将被删除，继续吗？";
            string caption1 = "提示...";
            MessageBoxButtons buttons1 = MessageBoxButtons.YesNo;

            DialogResult result1;

            result1 = MessageBox.Show(message1, caption1, buttons1,
            MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
            if (result1 == DialogResult.Yes)
            {
                this.lyRestructuringinBindingSource.RemoveCurrent();
                this.dataGridView3.EndEdit();
                this.lyRestructuringinBindingSource.EndEdit();
                this.ly_Restructuring_inTableAdapter.Update(this.lYStoreMange.ly_Restructuring_in);
                ReLoad();
                if (dataGridView3.Rows.Count == 0)
                {
                    if (null != this.ly_production_orderDataGridView.CurrentRow)
                    {
                        string taskMumber = this.ly_production_orderDataGridView.CurrentRow.Cells["跟单编号_Main"].Value.ToString();

                        string workName = ly_production_orderDataGridView.CurrentRow.Cells["工人_Main"].Value.ToString();

                        ly_store_in_restructuringMainTableAdapter.Fill(this.lYSalseRepair.ly_store_in_restructuringMain, workName, taskMumber, SQLDatabase.NowUserID);

                    }
                }
            }
        }

        private void ly_store_outDataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

            DataGridView dgv = sender as DataGridView;


            if (null == this.ly_store_outnumDataGridView.CurrentRow) return;
            if (null == this.ly_store_outDataGridView.CurrentRow) return;

            if (SQLDatabase.nowUserName() != ly_store_outnumDataGridView.CurrentRow.Cells["发料人"].Value.ToString())
            {
                MessageBox.Show("请发料人:" + ly_store_outnumDataGridView.CurrentRow.Cells["发料人"].Value.ToString() + " 修改");
                return;
            }

      
            if ("True" == ly_store_outnumDataGridView.CurrentRow.Cells["finished"].Value.ToString())
            {
                MessageBox.Show("已经签证,领料单不能修改...");
                return;

            }
            


            if ("领料数量" == dgv.CurrentCell.OwningColumn.Name)
            {
          
                if (dgv.CurrentRow.Cells["仓库类别"].Value.ToString()== "成品")
                {
                    MessageBox.Show("成品数量不可以修改...");
                    return;
                }

                ChangeValue queryForm = new ChangeValue();

                queryForm.OldValue = dgv.CurrentCell.Value.ToString();
                queryForm.NewValue = "";
                queryForm.ChangeMode = "value";
                queryForm.ShowDialog();


                if (queryForm.NewValue != "")
                {
                    if (decimal.Parse(queryForm.NewValue) <= 0)
                    {
                        MessageBox.Show("领料数量必须大于0...");return;
                    }

                    dgv.CurrentRow.Cells["领料数量"].Value = queryForm.NewValue;

                }
                else
                { 

                }

           
                this.ly_store_outDataGridView.EndEdit();
                this.Validate();
                this.ly_Restructuring_outBindingSource.EndEdit();
                this.ly_Restructuring_outTableAdapter.Update(this.lYStoreMange.ly_Restructuring_out);
                ReloadMain(); 
                return;

            }
        }

        private void dataGridView3_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            DataGridView dgv = sender as DataGridView;

            if (null == this.dataGridView2.CurrentRow) return;
            if (null == this.dataGridView3.CurrentRow) return;

            if (SQLDatabase.nowUserName() != dataGridView2.CurrentRow.Cells["收料人_T"].Value.ToString())
            {

                MessageBox.Show("请收料人:" + dataGridView2.CurrentRow.Cells["收料人_T"].Value.ToString() + " 修改");

                return;
            }


            if ("True" == dataGridView2.CurrentRow.Cells["签证_T"].Value.ToString())
            {
                MessageBox.Show("已经签证,退料单不能修改...");
                return;

            }




            if ("领料数量_T" == dgv.CurrentCell.OwningColumn.Name)
            {
          
                ChangeValue queryForm = new ChangeValue();

                queryForm.OldValue = dgv.CurrentCell.Value.ToString();
                queryForm.NewValue = "";
                queryForm.ChangeMode = "value";
                queryForm.ShowDialog();


                if (queryForm.NewValue != "")
                {
                    if (decimal.Parse(queryForm.NewValue) >= 0)
                    {
                        MessageBox.Show("领料数量为负数..."); return;
                    }

                    dgv.CurrentRow.Cells["领料数量_T"].Value = queryForm.NewValue;

                }
                else
                {

                }

                this.dataGridView3.EndEdit();
                this.Validate();
                this.lyRestructuringinBindingSource.EndEdit();
                this.ly_Restructuring_inTableAdapter.Update(this.lYStoreMange.ly_Restructuring_in);
                ReLoad();
                if (dataGridView3.Rows.Count == 0)
                {
                    if (null != this.ly_production_orderDataGridView.CurrentRow)
                    {
                        string taskMumber = this.ly_production_orderDataGridView.CurrentRow.Cells["跟单编号_Main"].Value.ToString();

                        string workName = ly_production_orderDataGridView.CurrentRow.Cells["工人_Main"].Value.ToString();

                        ly_store_in_restructuringMainTableAdapter.Fill(this.lYSalseRepair.ly_store_in_restructuringMain, workName, taskMumber, SQLDatabase.NowUserID);

                    }
                }
           
                return;

            }
        }

        private void 批量进行退料ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ly_Restructuring_requestMainStroeDataGridView.CurrentRow != null)
            {
                decimal outqty = decimal.Parse(ly_Restructuring_requestMainStroeDataGridView.CurrentRow.Cells["已领"].Value.ToString());//已领数量
                if (outqty <= 0)
                {
                    MessageBox.Show("为了准确核算成本，必须先领主料！", "注意");
                    return;
                }
            }
            if (product_inspectionDataGridView2.Rows.Count <= 0)
            {
                MessageBox.Show("没有记录！", "注意");
                return;
            }
            NewFrm.Show(this);
            string outNum = GetMaxOutNum();
            string taskCode = ly_production_orderDataGridView.CurrentRow.Cells["跟单编号_Main"].Value.ToString();
            string workName = ly_production_orderDataGridView.CurrentRow.Cells["工人_Main"].Value.ToString();
            string workCode = ly_production_orderDataGridView.CurrentRow.Cells["工号_Main"].Value.ToString();
            if (string.IsNullOrEmpty(workName) || string.IsNullOrEmpty(workCode))
            {
                MessageBox.Show("该工单没有排工人，无法出库！", "注意");
                return;
            }
            for (int i = 0; i < product_inspectionDataGridView2.Rows.Count; i++)
            {
                ly_restructuring_task_inspection_selBindingSource.Position = i;

                if (string.IsNullOrEmpty(product_inspectionDataGridView2.Rows[i].Cells["设备编号_sg"].Value.ToString()))
                {

                    continue;
                }
                if (ly_Restructuring_return_stdInspectionDataGridView.Rows.Count <= 0)
                {

                    continue;
                }
              
                string tjh = product_inspectionDataGridView2.Rows[i].Cells["提交号_single"].Value.ToString();
               
                SqlConnection sqlConnection1 = new SqlConnection(SQLDatabase.Connectstring);
                SqlCommand cmd = new SqlCommand();



                cmd.Parameters.Add("@task_inspection", SqlDbType.Int);
                cmd.Parameters["@task_inspection"].Value = tjh;
                cmd.Parameters.Add("@out_number", SqlDbType.VarChar);
                cmd.Parameters["@out_number"].Value = outNum;

                cmd.Parameters.Add("@faliaoren", SqlDbType.VarChar);
                cmd.Parameters["@faliaoren"].Value = SQLDatabase.nowUserName();
                cmd.Parameters.Add("@workname", SqlDbType.VarChar);
                cmd.Parameters["@workname"].Value = workName;

                cmd.Parameters.Add("@taskcode", SqlDbType.VarChar);
                cmd.Parameters["@taskcode"].Value = taskCode;

                cmd.Parameters.Add("@workCode", SqlDbType.VarChar);
                cmd.Parameters["@workCode"].Value = workCode;
                cmd.Parameters.Add("@userID", SqlDbType.VarChar);
                cmd.Parameters["@userID"].Value = SQLDatabase.NowUserID;
                cmd.CommandText = "LY_store_In_restructuringStand";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = sqlConnection1;

                sqlConnection1.Open();
                cmd.ExecuteNonQuery();
                sqlConnection1.Close();

            }


            ReLoad();
            ly_store_in_restructuringMainTableAdapter.Fill(this.lYSalseRepair.ly_store_in_restructuringMain, workName, taskCode, SQLDatabase.NowUserID);
            NewFrm.Hide(this);
        }
    }
}