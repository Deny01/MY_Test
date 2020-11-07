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
    public partial class LY_Restructuring_Store_In_Qianzhuang : Form
    {
        private string formState;
        string nowInNum;
        string nowdate;

        public LY_Restructuring_Store_In_Qianzhuang()
        {
            InitializeComponent();
        }

        private void LY_Machine_Load(object sender, EventArgs e)
        {
            this.dateTimePicker1.Text = DateTime.Today.AddMonths(-12).Date.ToString();
            this.dateTimePicker2.Text = DateTime.Today.AddDays(1).Date.ToString();


            this.ly_store_in_innumTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            this.ly_store_innum_productTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
                       
            
            this.ly_restructuring_task_inspectionTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;



            //----------------

     
            ly_Restructuring_return_singleTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring; 
            ly_Restructuring_return_stdInspectionTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;

            this.ly_store_in_innumTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;


            //----------------


            this.lY_restructuring_periodTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            this.lY_restructuring_periodTableAdapter.Fill(this.lYProductMange.LY_restructuring_period, this.dateTimePicker1.Value, this.dateTimePicker2.Value.AddDays(1), "GQ", SQLDatabase.NowUserID);
       

            this.SetFormState("View");

        }


        private void SetFormState(string state)
        {
 

            if ("View" == state)
            {
                this.formState = "View";
         
            }
            else
            {
                this.formState = "Edit"; 
            }


        }

      
        private string GetMaxStoreInnum()
        {

            SqlConnection sqlConnection1 = new SqlConnection(SQLDatabase.Connectstring);
            SqlCommand cmd = new SqlCommand();

            string MaxStoreInnum = "";
 
            cmd.CommandText = "LY_Get_InNumber";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = sqlConnection1;

            sqlConnection1.Open();
            MaxStoreInnum = cmd.ExecuteScalar().ToString();
            sqlConnection1.Close(); 

            return MaxStoreInnum;

        }



        private void button1_Click(object sender, EventArgs e)
        {
            this.lY_restructuring_periodTableAdapter.Fill(this.lYProductMange.LY_restructuring_period, this.dateTimePicker1.Value, this.dateTimePicker2.Value.AddDays(1), "GQ", SQLDatabase.NowUserID);
       
        }
        
      
        private void CountStoreInAuto()
        {

            if (null == this.ly_production_orderDataGridView.CurrentRow) return;
            if (null == this.ly_production_task_inspectionDataGridView.CurrentRow) return;

            string nowtaskCode = ly_production_orderDataGridView.CurrentRow.Cells["任务单号"].Value.ToString();

       

            SqlConnection sqlConnection1 = new SqlConnection(SQLDatabase.Connectstring);
            SqlCommand cmd = new SqlCommand();



            cmd.Parameters.Add("@task_code", SqlDbType.VarChar);
            cmd.Parameters["@task_code"].Value = nowtaskCode;

            cmd.Parameters.Add("@yonghu_code", SqlDbType.VarChar);
            cmd.Parameters["@yonghu_code"].Value = SQLDatabase.NowUserID;


            string inNum = GetMaxStoreInnum();
            cmd.Parameters.Add("@in_number", SqlDbType.VarChar);
            cmd.Parameters["@in_number"].Value = inNum;

            cmd.Parameters.Add("@shouliaoren", SqlDbType.VarChar);
            cmd.Parameters["@shouliaoren"].Value = SQLDatabase.nowUserName();

            cmd.Parameters.Add("@in_style", SqlDbType.VarChar);
            cmd.Parameters["@in_style"].Value = "钳装改制入库"; 

            cmd.CommandText = "LY_store_in_restructuring";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = sqlConnection1;

            sqlConnection1.Open();
            cmd.ExecuteNonQuery();
            sqlConnection1.Close();


            this.lY_restructuring_periodTableAdapter.Fill(this.lYProductMange.LY_restructuring_period, this.dateTimePicker1.Value, this.dateTimePicker2.Value.AddDays(1), "GQ", SQLDatabase.NowUserID);
            this.lY_restructuring_periodBindingSource.Position = this.lY_restructuring_periodBindingSource.Find("跟单编号", nowtaskCode);

            this.ly_restructuring_task_inspectionTableAdapter.Fill(this.lYProductMange.ly_restructuring_task_inspection, nowtaskCode);

            this.ly_store_innum_productTableAdapter.Fill(this.lYStoreMange.ly_store_innum_product, nowtaskCode, SQLDatabase.NowUserID);
        
 
        }

      

       

        private void ly_store_innum_purchaseDataGridView_SelectionChanged(object sender, EventArgs e)
        {
            if (null == this.ly_store_innum_purchaseDataGridView.CurrentRow)
            {
                
                this.ly_store_in_innumTableAdapter.Fill(this.lYStoreMange.ly_store_in_innum, "", SQLDatabase.NowUserID);
        
                return;
            }

          
            string nowInNum = this.ly_store_innum_purchaseDataGridView.CurrentRow.Cells["入库单号"].Value.ToString();
            this.ly_store_in_innumTableAdapter.Fill(this.lYStoreMange.ly_store_in_innum, nowInNum, SQLDatabase.NowUserID);


            
        }

 
       
        private void ly_store_in_ylDataGridView_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            return;
            
            DataGridView dgv = sender as DataGridView;

            if ("True" == ly_store_innum_purchaseDataGridView.CurrentRow.Cells["签证"].Value.ToString())
            {
                MessageBox.Show("已经签证,入库信息不能修改...");

                return;

            }

            if (SQLDatabase.nowUserName() != (ly_store_innum_purchaseDataGridView.CurrentRow.Cells["收料人"].Value.ToString()))
            {

                MessageBox.Show("请收料人:" + ly_store_innum_purchaseDataGridView.CurrentRow.Cells["收料人"].Value.ToString() + " 修改");

                return;
            }


            if ("入库数量" == dgv.CurrentCell.OwningColumn.Name)
            {

                ChangeValue queryForm = new ChangeValue();

                queryForm.OldValue = dgv.CurrentCell.Value.ToString();
                queryForm.NewValue = "";
                queryForm.ChangeMode = "value";
                queryForm.ShowDialog();

                //decimal oldnum = decimal.Parse(dgv.CurrentCell.Value.ToString());
                //decimal notinnum = decimal.Parse(dgv.CurrentRow.Cells["storecount"].Value.ToString());
                //decimal stanterdnum = 0;

                //if (null != this.ly_plan_getmaterialDataGridView.CurrentRow)
                //{
                //    stanterdnum = decimal.Parse(this.ly_plan_getmaterialDataGridView.CurrentRow.Cells["未领数量"].Value.ToString());
                //}

                if (queryForm.NewValue != "")
                {
                    //decimal newnum = decimal.Parse(queryForm.NewValue);


                    //if ((newnum - oldnum) > storenum)
                    //{
                    //    MessageBox.Show("库存不足,操作取消...");

                    //}
                    //else if (newnum - oldnum > stanterdnum)
                    //{
                    //    MessageBox.Show("领料超计划,操作取消...");
                    //}
                    //else
                    //{
                    dgv.CurrentRow.Cells["入库数量"].Value = queryForm.NewValue;
                        //dgv.CurrentRow.Cells["discount_money"].Value = DBNull.Value;

                        //dgv.CurrentRow.Cells["approve_flag"].Value = "False";
                       SaveChanged();
                    //}


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

 
           
        }
        private void SaveChanged()
        {
          
            ly_store_in_ylDataGridView.EndEdit();
            ly_store_in_innumBindingSource.EndEdit();

            this.ly_store_in_innumTableAdapter.Update(this.lYStoreMange.ly_store_in_innum);

            RefreshInstore();


        }
        private void bindingNavigatorAddNewItem1_Click(object sender, EventArgs e)
        {
            if (null == this.ly_production_orderDataGridView.CurrentRow) return;
            if (null == ly_production_task_inspectionDataGridView.CurrentRow) return;

            string message = "确定钳装改制入库吗?";
            string caption = "提示...";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result; 

            result = MessageBox.Show(message, caption, buttons,
            MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

            string taskCode = ly_production_orderDataGridView.CurrentRow.Cells["任务单号"].Value.ToString();

            if (result == DialogResult.Yes)
            { 
                //进行标准退料  核查 
                string sql = @"SELECT a.id, a.inspection_id, a.itemno, a.return_qty, a.remark, a.sys_date, 
      a.restructuring_id, a.cancel_qty, a.cancel_remark, a.approve_flag, a.approve_people, 
      a.approve_time, a.submit, b.warehouse, ISNULL(h.in_qty, 0) AS in_qty, 
      g.ifQualified
FROM ly_Restructuring_return_stdInspection AS a LEFT OUTER JOIN 
dbo.ly_restructuring_task_inspection  g on g.id=a.inspection_id
left join    ly_inma0010 AS b ON a.itemno = b.wzbh LEFT OUTER JOIN
          (SELECT pruductionTaskInspection_num, wzbh, out_level, machine_num, 
               ISNULL(SUM(qty), 0) AS in_qty   FROM ly_store_out   GROUP BY pruductionTaskInspection_num, wzbh, out_level, machine_num) 
      AS h ON h.pruductionTaskInspection_num = '"+taskCode
      +"' AND h.wzbh = a.itemno AND  h.out_level = 'returnstandard' AND h.machine_num =(SELECT machine_num  FROM ly_restructuring_task_inspection where (id = a.inspection_id)) " +
      "WHERE(g.ifQualified = 1) and (g.pruductionOrder_num='" + taskCode + "')";

                DataTable dt = null;
                using (SqlConnection connection = new SqlConnection(SQLDatabase.Connectstring))
                {
                 
                    SqlDataAdapter adapter = new SqlDataAdapter(sql, connection);
                    DataSet ds = new DataSet();
                    adapter.Fill(ds);
                    dt = ds.Tables[0];
                }
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string tjh = dt.Rows[i]["inspection_id"].ToString();
                    decimal retqty = decimal.Parse(dt.Rows[i]["return_qty"].ToString()); //标准退料
                    string approve = dt.Rows[i]["approve_flag"].ToString();
                    decimal canqty = decimal.Parse(dt.Rows[i]["cancel_qty"].ToString());  //免退数量
                    decimal inqty = decimal.Parse(dt.Rows[i]["in_qty"].ToString());  //实退数量

                    if (approve == "True")
                    {
                        if (retqty - canqty != ( 0- inqty))
                        {

                            MessageBox.Show("提交号：" + tjh + "，标准退料没有入库！");
                            return;
                        }
                    }
                    else
                    {
                        MessageBox.Show("提交号：" + tjh + "，标准退料没有入库！");
                        return;
                    }
                }


                //进行 追加退料验证
                string sqlSin = @"select b.*  from dbo.ly_restructuring_task_inspection a left join  
( SELECT a.id, a.parent_id, a.pruductionOrder_num, a.itemno, a.machine_num, 
      ISNULL(a.material_flag, 0) AS material_flag, a.request_qty, a.approve_people, 
      a.approve_date, a.request_artribute, a.sys_date, a.task_inspection, b.warehouse, 
      ISNULL(h.in_qty, 0) AS in_qty, dbo.f_LY_Storecount(a.itemno) AS kcl
FROM ly_Restructuring_return_single AS a LEFT OUTER JOIN
      ly_inma0010 AS b ON a.itemno = b.wzbh LEFT OUTER JOIN
      ly_restructuring_task AS c ON c.restructuring_id = a.parent_id and c.pruductionOrder_num='" + taskCode 
      + "' LEFT OUTER JOIN  (SELECT pruductionTaskInspection_num, wzbh, out_level, ISNULL(SUM(qty), 0)   AS in_qty  FROM ly_store_out  GROUP BY pruductionTaskInspection_num, wzbh, out_level) AS h ON  h.pruductionTaskInspection_num = c.pruductionOrder_num AND   h.wzbh = a.itemno AND h.out_level = 'returnsingle') as b on a.id=b.task_inspection where  a.pruductionOrder_num='" + taskCode + "' and a.ifQualified=1";

                DataTable dtSin = null;
                using (SqlConnection connection = new SqlConnection(SQLDatabase.Connectstring))
                {

                    SqlDataAdapter adapter = new SqlDataAdapter(sqlSin, connection);
                    DataSet ds = new DataSet();
                    adapter.Fill(ds);
                    dtSin = ds.Tables[0];
                }
                for (int i = 0; i < dtSin.Rows.Count; i++)
                {
                    if (string.IsNullOrEmpty(dtSin.Rows[i]["task_inspection"].ToString())   ||  string.IsNullOrEmpty(dtSin.Rows[i]["request_qty"].ToString()) || string.IsNullOrEmpty(dtSin.Rows[i]["material_flag"].ToString()) )
                    {
                        continue;
                    }
                    string tjh = dtSin.Rows[i]["task_inspection"].ToString();
                    decimal retqty = decimal.Parse(dtSin.Rows[i]["request_qty"].ToString()); //标准退料
                    string approve = dtSin.Rows[i]["material_flag"].ToString();
 
                    decimal inqty = decimal.Parse(dtSin.Rows[i]["in_qty"].ToString());  //实退数量

                    if (approve == "True")
                    {
                        if (retqty  !=(0- inqty))
                        {

                            MessageBox.Show("提交号：" + tjh + "，追加退料没有入库！");
                            return;
                        }
                    }
                    else
                    {
                        MessageBox.Show("提交号：" + tjh + "，追加退料没有入库！");
                        return;
                    }
                }










                CountStoreInAuto();
            }
        }

        private void bindingNavigatorDeleteItem1_Click(object sender, EventArgs e)
        {
            if (null == this.ly_store_innum_purchaseDataGridView.CurrentRow) return;

            //if (this.formState != "View") return;

            if (SQLDatabase.nowUserName() != ly_store_innum_purchaseDataGridView.CurrentRow.Cells["收料人"].Value.ToString())
            {

                MessageBox.Show("请收料人:" + ly_store_innum_purchaseDataGridView.CurrentRow.Cells["收料人"].Value.ToString() + " 删除");

                return;
            }
 
            string innumber = this.ly_store_innum_purchaseDataGridView.CurrentRow.Cells["入库单号"].Value.ToString(); 

            if ("True" == ly_store_innum_purchaseDataGridView.CurrentRow.Cells["签证"].Value.ToString())
            {
                MessageBox.Show("已经签证,入库单不能删除...");

                return;

            }
              

            string message = "删除当前入库单:" + innumber + "吗？";
            string caption = "提示...";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;

            DialogResult result;



            result = MessageBox.Show(message, caption, buttons,
            MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

            if (result == DialogResult.Yes)
            {

       
                string delstr = " delete ly_store_in  from ly_store_in   " +" where ly_store_in.in_number = '" + innumber + "'";

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
                    //this.ly_material_plan_mainTableAdapter.Fill(this.lYPlanMange.ly_material_plan_main, "SCJH");
                }

                RefreshInstore(); 

            }
        }

        private void RefreshInstore()
        {
            if (null != this.ly_production_orderDataGridView.CurrentRow)
            {

                string nowproductionorderNum = this.ly_production_orderDataGridView.CurrentRow.Cells["任务单号"].Value.ToString();


                this.lY_restructuring_periodTableAdapter.Fill(this.lYProductMange.LY_restructuring_period, this.dateTimePicker1.Value, this.dateTimePicker2.Value.AddDays(1), "GQ", SQLDatabase.NowUserID);
       
                this.lY_restructuring_periodBindingSource.Position = this.lY_restructuring_periodBindingSource.Find("跟单编号", nowproductionorderNum);

                this.ly_restructuring_task_inspectionTableAdapter.Fill(this.lYProductMange.ly_restructuring_task_inspection, nowproductionorderNum);
                this.ly_store_innum_productTableAdapter.Fill(this.lYStoreMange.ly_store_innum_product, nowproductionorderNum, SQLDatabase.NowUserID);


            }
            else
            {

                this.ly_restructuring_task_inspectionTableAdapter.Fill(this.lYProductMange.ly_restructuring_task_inspection, "");
                this.ly_store_innum_productTableAdapter.Fill(this.lYStoreMange.ly_store_innum_product, "", SQLDatabase.NowUserID);

            }
        }

        private void 打印PToolStripButton_Click(object sender, EventArgs e)
        {
            if (null == this.ly_store_innum_purchaseDataGridView.CurrentRow) return;
            
            BaseReportView queryForm = new BaseReportView();
            queryForm.Text = "中原精密钳装改制入库单";
            queryForm.Printdata = this.lYStoreMange;
            queryForm.PrintCrystalReport = new LY_ProductRukudan_GZ();
            queryForm.ShowDialog();
        }

        private void 删除子件ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (null == this.ly_store_in_ylDataGridView.CurrentRow) return;

            if ("True" == ly_store_innum_purchaseDataGridView.CurrentRow.Cells["签证"].Value.ToString())
            {
                MessageBox.Show("已经签证,入库信息不能修改...");

                return;

            }
 

            string componentNum = this.ly_store_in_ylDataGridView.CurrentRow.Cells["物料编号1"].Value.ToString();


            string message1 = "当前(物料：" + componentNum + ")将被删除，继续吗？";
            string caption1 = "提示...";
            MessageBoxButtons buttons1 = MessageBoxButtons.YesNo;

            DialogResult result1;



            result1 = MessageBox.Show(message1, caption1, buttons1,
            MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

            if (result1 == DialogResult.Yes)
            {
  
                this.ly_store_in_innumBindingSource.RemoveCurrent();
                SaveChanged();

               

            }
        }

        private void SaveIndate(string indate)
        {
            string innumber = this.ly_store_innum_purchaseDataGridView.CurrentRow.Cells["入库单号"].Value.ToString();
          
            string delstr = " update ly_store_in  set input_date='" + indate + "'" +  " from ly_store_in " + " where ly_store_in.in_number = '" + innumber + "'";


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
                //this.ly_material_plan_mainTableAdapter.Fill(this.lYPlanMange.ly_material_plan_main, "SCJH");
            }

        }

        private void ly_store_innum_purchaseDataGridView_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            DataGridView dgv = sender as DataGridView;

            if (SQLDatabase.nowUserName() != dgv.CurrentRow.Cells["收料人"].Value.ToString())
            {

                MessageBox.Show("请收料人:" + dgv.CurrentRow.Cells["收料人"].Value.ToString() + " 修改");

                return;
            } 
            if ("入库日期1" == dgv.CurrentCell.OwningColumn.Name)
            {
                if ("True" == dgv.CurrentRow.Cells["签证"].Value.ToString())
                {
                    MessageBox.Show("已经签证,不能修改入库日期...");

                    return;

                } 
                ChangeValue queryForm = new ChangeValue(); 
                queryForm.OldValue = dgv.CurrentCell.Value.ToString();
                queryForm.NewValue = "";
                queryForm.ChangeMode = "datetime";
                queryForm.ShowDialog(); 
                if (queryForm.NewValue != "")
                {
                   
                    dgv.CurrentCell.Value = queryForm.NewValue;
                    SaveIndate(queryForm.NewValue);

                } 
                return;

            }
 
        }

    

        
        private void ly_production_orderDataGridView_SelectionChanged(object sender, EventArgs e)
        {
            if (null != this.ly_production_orderDataGridView.CurrentRow)
            {

                string nowproductionorderNum = this.ly_production_orderDataGridView.CurrentRow.Cells["任务单号"].Value.ToString();

                this.ly_restructuring_task_inspectionTableAdapter.Fill(this.lYProductMange.ly_restructuring_task_inspection, nowproductionorderNum);
                this.ly_store_innum_productTableAdapter.Fill(this.lYStoreMange.ly_store_innum_product , nowproductionorderNum, SQLDatabase.NowUserID);


            }
            else
            {

                this.ly_restructuring_task_inspectionTableAdapter.Fill(this.lYProductMange.ly_restructuring_task_inspection, "");
                this.ly_store_innum_productTableAdapter.Fill(this.lYStoreMange.ly_store_innum_product , "", SQLDatabase.NowUserID);

            }
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

        private void ly_production_task_inspectionDataGridView_SelectionChanged(object sender, EventArgs e)
        {
            if (ly_production_task_inspectionDataGridView.CurrentRow == null)
            {

           
                ly_Restructuring_return_singleTableAdapter.Fill(lYProductMange.ly_Restructuring_return_single, -1);
                ly_Restructuring_return_stdInspectionTableAdapter.Fill(lYProductMange.ly_Restructuring_return_stdInspection,"", -1);//标准退料

            }
            else
            {



                string tjh = ly_production_task_inspectionDataGridView.CurrentRow.Cells["id_in"].Value.ToString();
              
                ly_Restructuring_return_singleTableAdapter.Fill(lYProductMange.ly_Restructuring_return_single, int.Parse(tjh));//追加退料


                if (null != this.ly_production_orderDataGridView.CurrentRow)
                {

                    string nowproductionorderNum = this.ly_production_orderDataGridView.CurrentRow.Cells["任务单号"].Value.ToString();
                    ly_Restructuring_return_stdInspectionTableAdapter.Fill(lYProductMange.ly_Restructuring_return_stdInspection, nowproductionorderNum, int.Parse(tjh));//标准退料

                }

            }
        }
    }
}
