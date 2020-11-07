using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HappyYF.Infrastructure.Repositories;
using DataGridFilter;
using System.Transactions;
using System.Data.SqlClient;


namespace HappyYF.YuanXin.WorkSet
{
    public partial class LY_Store_OutDaily : Form
    {
        private string formState;
        string  nowOutNum;
        string  nowdate;
        string nowOutstyle;
        string nowSubOutstyle;

        public LY_Store_OutDaily()
        {
            InitializeComponent();
        }

       

        private void LY_MaterialMange_Load(object sender, EventArgs e)
        {
            this.dateTimePicker1.Text = DateTime.Today.AddMonths(-1).Date.ToString();
            this.dateTimePicker2.Text = DateTime.Today.AddDays(1).Date.ToString();


            this.ly_store_outTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            this.ly_store_outnumDailyTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
         
           
            this.ly_inma0010_inoutTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;

            this.SetFormState("View");


            this.ly_inma0010_inoutTableAdapter.Fill(this.lYMaterialMange.ly_inma0010_inout ,SQLDatabase .NowUserID );

            
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {


            //if (keyData == Keys.Enter)
            //{
            //    if (ly_store_in_ylDataGridView.CurrentCell.OwningColumn.Name  == "采购员")
            //        ly_store_in_ylDataGridView.CurrentCell = ly_store_in_ylDataGridView.CurrentRow.Cells["单据编号"];
            //    else
            //        System.Windows.Forms.SendKeys.Send("{tab}");
            //}
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void toolStripTextBox1_Enter(object sender, EventArgs e)
        {
            toolStripTextBox1.Text = "";

            this.ly_inma0010_inoutBindingSource.Filter = "";
        }

        private void toolStripTextBox1_KeyUp(object sender, KeyEventArgs e)
        {
            string dFilter = "";

            //for (int i = 0; i < this.hT_Vehicle_collectDataGridView.ColumnCount; i++)
            for (int i = 1; i < 11; i++)
            {
                string tempColumnName = this.ly_inma0010_inoutDataGridView.Columns[i].DataPropertyName;
              
                if (i != 10)
                    dFilter = dFilter + tempColumnName + " like  '*" + this.toolStripTextBox1.Text + "*' or ";
                else
                    dFilter = dFilter + tempColumnName + " like  '*" + this.toolStripTextBox1.Text + "*' ";

            }

            if (this.toolStripTextBox1.Text.Replace(" ", "").Length > 0)

                this.ly_inma0010_inoutBindingSource.Filter = dFilter;
            else
                this.ly_inma0010_inoutBindingSource.Filter = " ";
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            ExportDataGridviewTOExcell.ExportDataGridview(this.ly_inma0010_inoutDataGridView, true);
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            FilterForm filterForm = new FilterForm();

         

            List<string> ls = new List<string>();
            ls.Add("id");


            filterForm.SetSourceColumns(ly_inma0010_inoutDataGridView.Columns , ls);

            filterForm.ShowDialog();

            this.ly_inma0010_inoutBindingSource.Filter = filterForm.GetFilterString();
        }

        private void bindingNavigatorAddNewItem_Click(object sender, EventArgs e)
        {
            //////////////////////////////////////

           

            //LY_MaterialAdd queryForm = new LY_MaterialAdd();

            //queryForm.material_code = "";
            //queryForm.runmode = "增加";

            //queryForm.StartPosition = FormStartPosition.CenterParent;
            //queryForm.ShowDialog();

            //if (queryForm.DialogResult != DialogResult.Cancel)
            //{
            //    this.ly_inma0010_inoutTableAdapter.Fill(this.lYMaterialMange.ly_inma0010_inout );
            //    this.ly_inma0010_inoutBindingSource.Position = this.ly_inma0010_inoutBindingSource.Find("物资编号", queryForm.material_code);
            //}

            ///////////////////////////////////////////

           
        }

      

        private void bindingNavigatorDeleteItem_Click(object sender, EventArgs e)
        {
            if (this.formState == "View") return;

            string nowOUTstyle = this.ly_store_outnumDailyDataGridView.CurrentRow.Cells["出库类别"].Value.ToString();

            if (nowOUTstyle == "借旧还新" || nowOUTstyle == "借新还旧")
            {
                MessageBox.Show("出库类别:" + nowOUTstyle + "不能在这里删除", "注意");
                return;
            }

            if ("True" == ly_store_outDataGridView.CurrentRow.Cells["finished1"].Value.ToString())
            {
                MessageBox.Show("已经签证,不能删除   ...");

                return;

            }

            if (!string.IsNullOrEmpty(ly_store_outnumDailyDataGridView.CurrentRow.Cells["计划编号"].Value.ToString()))
            {
                MessageBox.Show("计划发料只能在这里浏览,不能删除", "注意");
                return;
            }

            string nowoperptar = this.ly_store_outnumDailyDataGridView.CurrentRow.Cells["发料人"].Value.ToString();

            if (nowoperptar != SQLDatabase.nowUserName())
            {
                MessageBox.Show("请发料人:" + nowoperptar + "删除", "注意");
                return;
            }


            
            string message = "确定删除当前物料记录吗？";
            string caption = "提示...";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result;



            result = MessageBox.Show(message, caption, buttons,
            MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

            if (result == DialogResult.Yes)
            {

                this.ly_store_outBindingSource.RemoveCurrent();


                SaveStoreInDetail();

                //string s = this.xD_Sel_SellBalanceDataGridView.CurrentRow.Cells["编号"].Value.ToString();

                //this.hS_ClientPaymentTableAdapter.Fill(this.xD_SellBalance.HS_ClientPayment, s);


            }
        }

        private void SaveStoreInDetail()
        {
            ly_store_outDataGridView.EndEdit();
            ly_store_outBindingSource.EndEdit();

           DataGridView  dgv = this.ly_store_outDataGridView;

           decimal  now_lls = 0;
           decimal now_kcs = 0;
           string now_itemno;

            foreach (DataGridViewRow dgr in dgv.Rows)
            {

              

                if ( !string .IsNullOrEmpty(dgr.Cells["领料数量"].Value.ToString()))
                {
                    now_lls = decimal.Parse(dgr.Cells["领料数量"].Value.ToString());
                }
                else 
                {
                    now_lls = 0;
                }

                if (!string.IsNullOrEmpty(dgr.Cells["storecount"].Value.ToString()))
                {
                    now_kcs = decimal.Parse(dgr.Cells["storecount"].Value.ToString());
                }
                else
                {
                    now_kcs = 0;
                }

                now_itemno = dgr.Cells["物料编号"].Value.ToString();

                if (now_lls > now_kcs)
                {
                    MessageBox.Show("物料:" + now_itemno + " 领料数大于库存,无法出库", "注意");
                    return;
                }


            }

            this.ly_store_outTableAdapter.Update(this.lYStoreMange.ly_store_out);
            this.ly_store_outTableAdapter.Fill(this.lYStoreMange.ly_store_out, nowOutNum, SQLDatabase.nowUserName());
        }

      

        

        private void bindingNavigatorDeleteItem_Click_1(object sender, EventArgs e)
        {

            if (null == this.ly_store_outnumDailyDataGridView.CurrentRow) return;

            string nowOUTstyle = this.ly_store_outnumDailyDataGridView.CurrentRow.Cells["出库类别"].Value.ToString();

            if (nowOUTstyle == "借旧还新" || nowOUTstyle == "借新还旧")
            {
                MessageBox.Show("出库类别:" + nowOUTstyle + "不能在这里删除", "注意");
                return;
            }


            if (!string.IsNullOrEmpty(ly_store_outnumDailyDataGridView.CurrentRow.Cells["计划编号"].Value.ToString()))
            {
                MessageBox.Show("计划发料只能在这里浏览,不能删除", "注意");
                return;
            }

            string nowoperptar = this.ly_store_outnumDailyDataGridView.CurrentRow.Cells["发料人"].Value.ToString();

            if (nowoperptar != SQLDatabase.nowUserName())
            {
                MessageBox.Show("请发料人:" + nowoperptar + "删除", "注意");
                return;
            }

            if ("True" == ly_store_outnumDailyDataGridView.CurrentRow.Cells["签证"].Value.ToString())
            {
                MessageBox.Show("已经签证,不能删除出库单...");

                return;

            }

            
            //////////////////

            string outnumber = ly_store_outnumDailyDataGridView.CurrentRow.Cells["出库单号"].Value.ToString();
            string storename = ly_store_outnumDailyDataGridView.CurrentRow.Cells["仓库"].Value.ToString();

            string message = "删除当前领料单:" + outnumber + "吗？";
            string caption = "提示...";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;

            DialogResult result;



            result = MessageBox.Show(message, caption, buttons,
            MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

            if (result == DialogResult.Yes)
            {

                string delstr = " delete ly_store_out  from ly_store_out left join ly_inma0010 on ly_store_out.wzbh=ly_inma0010.wzbh  " +
                    " where ly_store_out.out_number = '" + outnumber + "' and ly_inma0010.warehouse='" + storename + "'";


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

                this.ly_store_outnumDailyBindingSource.RemoveCurrent();





            }


        }

        private void 保存SToolStripButton_Click(object sender, EventArgs e)
        {
            SaveStoreInDetail();

            this.nowOutstyle = null;
            this.nowSubOutstyle = null;

            this.SetFormState("View");
        }

        private void toolStripButton1_Click_1(object sender, EventArgs e)
        {

            
            /////////////////////////////

            if (null == this.ly_store_outnumDailyDataGridView.CurrentRow) return;

            if (!string.IsNullOrEmpty(ly_store_outnumDailyDataGridView.CurrentRow.Cells["计划编号"].Value.ToString()))
            {
                MessageBox.Show("计划发料只能在这里浏览,不能修改", "注意");
                return;
            }

            string nowoperptar = this.ly_store_outnumDailyDataGridView.CurrentRow.Cells["发料人"].Value.ToString();

            if (nowoperptar != SQLDatabase.nowUserName())
            {
                MessageBox.Show("请发料人:" + nowoperptar + "修改", "注意");
                return;
            }

            if ("True" == ly_store_outnumDailyDataGridView.CurrentRow.Cells["签证"].Value.ToString())
            {
                MessageBox.Show("已经签证,不能修改入库数据...");

                return;

            }


            //string ifcheck_Id = this.ly_store_innumDataGridView.CurrentRow.Cells["签证"].Value.ToString();

            //if (!string.IsNullOrEmpty(ifcheck_Id))
            //{
            //    MessageBox.Show("盘点数据不能在这里修改", "注意");
            //    return;
            //}
            this.nowOutstyle = ly_store_outnumDailyDataGridView.CurrentRow.Cells["出库类别"].Value.ToString();
            this.nowOutNum = ly_store_outnumDailyDataGridView.CurrentRow.Cells["出库单号"].Value.ToString();
            this.nowdate = ly_store_outnumDailyDataGridView.CurrentRow.Cells["出库日期"].Value.ToString();
            

            this.SetFormState("Edit");
        }
        private void SetFormState(string state)
        {
            // view edit add save cancle

            if ("View" == state)
            {
                this.formState = "View";
                this.ly_inma0010_inoutDataGridView.Enabled = false ;
                this.ly_store_outnumDailyDataGridView.Enabled = true;

                this.bindingNavigatorAddNewItem.Enabled = true;
                this.toolStripButton1.Enabled = true;
                this.toolStripButton3.Enabled = false;
                this.保存SToolStripButton.Enabled = false;
                this.bindingNavigatorDeleteItem.Enabled = true;
                //this.bindingNavigatorAddNewItem.Enabled = true;




                //this.bindingNavigatorMoveFirstItem1.Enabled = true;
                //this.bindingNavigatorMoveLastItem1.Enabled = true;
                //this.bindingNavigatorMoveNextItem1.Enabled = true;
                //this.bindingNavigatorMovePreviousItem1.Enabled = true;
                this.bindingNavigatorPositionItem2.Enabled = true;



                this.ly_store_outDataGridView.ReadOnly = true;

                foreach (DataGridViewColumn dvc in ly_store_outDataGridView.Columns)
                {
                    //if ("单据编号" == dvc.Name
                    //     || "入库日期" == dvc.Name
                    //     || "入库数量" == dvc.Name
                    //     || "入库单价" == dvc.Name
                    //     || "入库说明" == dvc.Name
                    //     || "采购员" == dvc.Name)
                    if ( "领料数量" == dvc.Name
                         || "备注" == dvc.Name
                         )
                        
                    {
                        //dvc.ReadOnly = false;
                        dvc.DefaultCellStyle.BackColor = Color.Teal;
                        dvc.DefaultCellStyle.ForeColor = Color.White;
                    }
                    else
                    {
                        //dvc.ReadOnly = true;
                        //dvc.DefaultCellStyle.BackColor = Color.Gray;
                        //dvc.DefaultCellStyle.ForeColor = Color.White;
                    }

                }






            }
            else
            {
                this.formState = "Edit";
                this.ly_inma0010_inoutDataGridView.Enabled = true ;
                this.ly_store_outnumDailyDataGridView.Enabled = false;
                //this.nowRow = ly_store_in_ylDataGridView.CurrentRow.Index;

                this.ly_store_outDataGridView.ReadOnly = false;

               // if (null != ly_stocktake_mainDataGridView.CurrentRow)



                this.bindingNavigatorAddNewItem.Enabled = false ;
                this.toolStripButton1.Enabled = false ;
                this.toolStripButton3.Enabled = true ;
                this.保存SToolStripButton.Enabled = true ;
                this.bindingNavigatorDeleteItem.Enabled = false ;
                //this.bindingNavigatorAddNewItem.Enabled = true;




                //this.bindingNavigatorMoveFirstItem1.Enabled = false;
                //this.bindingNavigatorMoveLastItem1.Enabled = false;
                //this.bindingNavigatorMoveNextItem1.Enabled = false;
                //this.bindingNavigatorMovePreviousItem1.Enabled = false;
                this.bindingNavigatorPositionItem2.Enabled = false;



                this.ly_store_outDataGridView.ReadOnly = false;

                foreach (DataGridViewColumn dvc in ly_store_outDataGridView.Columns)
                {
                   
                    if ("领料数量" == dvc.Name
                        || "备注" == dvc.Name
                        )
                    {
                        dvc.ReadOnly = false ;
                        dvc.DefaultCellStyle.BackColor = Color.Teal;
                        dvc.DefaultCellStyle.ForeColor  = Color.White ;
                    }
                    else
                    {
                        dvc.ReadOnly = true ;
                        //dvc.DefaultCellStyle.BackColor = Color.Gray;
                        //dvc.DefaultCellStyle.ForeColor = Color.White;
                    }

                }



            }


        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            this.SetFormState("View");
            if (null == this.ly_inma0010_inoutDataGridView.CurrentRow) return;

            string componentNum = this.ly_inma0010_inoutDataGridView.CurrentRow.Cells["物资编号"].Value.ToString();

            this.ly_store_outTableAdapter.Fill(this.lYStoreMange.ly_store_out, componentNum, SQLDatabase.nowUserName());
        }

        private void ly_store_in_ylDataGridView_SelectionChanged(object sender, EventArgs e)
        {
            if (null == ly_store_outDataGridView.CurrentRow) return;

            if (this.formState == "View")
            {
                
            }
            else
            {
               // this.ly_store_in_innumBindingSource.Position = this.nowRow;

                //this.hT_Insurance_ItemDataGridView.CurrentCell = this.hT_Insurance_ItemDataGridView.CurrentRow.Cells[this.hT_Insurance_ItemDataGridView.CurrentCell.ColumnIndex];
            }
        }

        private void ly_store_in_ylDataGridView_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            DataGridView dgv = sender as DataGridView;

            if (true == dgv.ReadOnly) return;

            DataGridViewCell dgc = dgv.Rows[e.RowIndex].Cells[e.ColumnIndex];



            if ("领料数量" == dgc.OwningColumn.Name || "入库单价" == dgc.OwningColumn.Name)
            {

                decimal newCheckCount;

                if ("" == e.FormattedValue.ToString().Replace(" ", ""))
                {
                    dgc.Value = DBNull.Value;
                   
                    //e.Cancel = false ;
                    return;

                }

                if (!decimal.TryParse(e.FormattedValue.ToString(), out newCheckCount))
                {
                    MessageBox.Show("数据格式错误...", "注意");
                    e.Cancel = true;

                }
               
            }
            else
            {

                return;

            }
        }

        private void ly_store_in_ylDataGridView_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.Cancel = false;
        }

        private void ly_store_in_ylDataGridView_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (true == ly_store_outDataGridView.ReadOnly) return;

            if ("领料数量" == ly_store_outDataGridView.CurrentCell.OwningColumn.Name
                         || "备注" == ly_store_outDataGridView.CurrentCell.OwningColumn.Name
                         )
            {
                //SetlastColumn();
                ly_store_outDataGridView.CurrentCell.Style.BackColor = Color.White;
                ly_store_outDataGridView.CurrentCell.Style.ForeColor = Color.Teal;

              
            }
        }

        private void ly_store_in_ylDataGridView_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
            if (true == ly_store_outDataGridView.ReadOnly) return;

            if ("领料数量" == ly_store_outDataGridView.CurrentCell.OwningColumn.Name
                        || "备注" == ly_store_outDataGridView.CurrentCell.OwningColumn.Name
                        )
            {
                //SetlastColumn();
                ly_store_outDataGridView.CurrentCell.Style.BackColor = Color.Teal;
                ly_store_outDataGridView.CurrentCell.Style.ForeColor = Color.White;


            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.ly_store_outnumDailyTableAdapter.Fill(this.lYStoreMange.ly_store_outnumDaily, DateTime.Parse(this.dateTimePicker1.Text).Date, DateTime.Parse(this.dateTimePicker2.Text).Date, SQLDatabase.NowUserID);

        }

        private void bindingNavigatorAddNewItem_Click_1(object sender, EventArgs e)
        {
            string sel = "SELECT a.stylecode as 编码,a.stylename as 名称 FROM ly_store_out_styleset a ";


            QueryForm queryForm = new QueryForm();


            queryForm.Sel = sel;
            queryForm.Constr = SQLDatabase.Connectstring;



            queryForm.ShowDialog();



            if (string.IsNullOrEmpty(queryForm.Result1))
            {
                MessageBox.Show("必须选择出库类别,才能出库...", "注意");
                return;
            }
            else
            {
                this.nowOutstyle = queryForm.Result1;

                if ("物料消耗" == this.nowOutstyle)
                {
                    string subsel = "SELECT a.substylecode as 子项编码,a.substylename as 子项名称 FROM ly_store_out_substyleset a ";


                    QueryForm subqueryForm = new QueryForm();


                    subqueryForm.Sel = subsel;
                    subqueryForm.Constr = SQLDatabase.Connectstring;



                    subqueryForm.ShowDialog();

                    if (string.IsNullOrEmpty(subqueryForm.Result1))
                    {
                        MessageBox.Show("必须选择物料消耗子项,才能出库...", "注意");
                        return;
                    }
                    else
                    {
                        this.nowSubOutstyle = subqueryForm.Result1;

                    }


                }

            }
            /////////////////////////////////////////////////////
            
            this.SetFormState("Edit");
            //this.nowInNum = GetMaxStoreInnum();
            this.nowOutNum = "NewOutnum";
            //this.nowdate = DateTime.Now.Date.ToString();
            this.nowdate = SQLDatabase.GetNowdate().ToString();
            this.ly_store_outTableAdapter.Fill(this.lYStoreMange.ly_store_out, "asd", SQLDatabase.nowUserName());

        }
        private DateTime GetNowdate()
        {
            SqlConnection sqlConnection1 = new SqlConnection(SQLDatabase.Connectstring);
            SqlCommand cmd = new SqlCommand();

            DateTime Nowdate = DateTime.Now.Date;

            //cmd.Parameters.Add("@Plan_mode", SqlDbType.VarChar);
            //cmd.Parameters["@Plan_mode"].Value = "LLJH";


            cmd.CommandText = "select getdate() ";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = sqlConnection1;

            sqlConnection1.Open();
            Nowdate = DateTime.Parse(cmd.ExecuteScalar().ToString()).Date;
            sqlConnection1.Close();



            return Nowdate;

        }
        private string GetMaxStoreOutnum()
        {

            SqlConnection sqlConnection1 = new SqlConnection(SQLDatabase.Connectstring);
            SqlCommand cmd = new SqlCommand();

            string MaxPlanCode = "";

            //cmd.Parameters.Add("@Plan_mode", SqlDbType.VarChar);
            //cmd.Parameters["@Plan_mode"].Value = "LLJH";


            cmd.CommandText = "LY_Get_OutNumber";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = sqlConnection1;

            sqlConnection1.Open();
            MaxPlanCode = cmd.ExecuteScalar().ToString();
            sqlConnection1.Close();



            return MaxPlanCode;
        
        }

        private void ly_inma0010_inoutDataGridView_DoubleClick(object sender, EventArgs e)
        {
            if (this.formState == "View") return;
            if (null == ly_inma0010_inoutDataGridView.CurrentRow) return;

            string componentNum = this.ly_inma0010_inoutDataGridView.CurrentRow.Cells["物资编号"].Value.ToString();

            string newFlag = "N";

            if (this.nowOutNum == "NewOutnum")
            {

                this.nowOutNum = GetMaxStoreOutnum();
                newFlag = "Y";
            }
            else 
            {

                newFlag = "N";
            }





            string insStr = " INSERT INTO ly_store_out  " +
           "( wzbh,out_style,sub_out_style,out_number,out_date,operoter) " +
           " values ('" + componentNum + "','" + nowOutstyle+ "','" + nowSubOutstyle + "','" + nowOutNum + "','" + nowdate + "','" + SQLDatabase.nowUserName() + "' )";


            using (TransactionScope scope = new TransactionScope())
            {

                SqlConnection sqlConnection1 = new SqlConnection(SQLDatabase.Connectstring);
                SqlCommand cmd = new SqlCommand();

                cmd.CommandText = insStr;
                cmd.CommandType = CommandType.Text;
                cmd.Connection = sqlConnection1;



                sqlConnection1.Open();
                cmd.ExecuteNonQuery();

                sqlConnection1.Close();

                scope.Complete();
            }


            SaveStoreInDetail();

            if (newFlag == "Y")
            {
                this.ly_store_outnumDailyDataGridView.SelectionChanged -= ly_store_innumDataGridView_SelectionChanged;
                this.ly_store_outnumDailyTableAdapter.Fill(this.lYStoreMange.ly_store_outnumDaily, DateTime.Parse(this.dateTimePicker1.Text).Date, DateTime.Now.AddDays(1).Date, SQLDatabase.NowUserID);
                this.ly_store_outnumDailyBindingSource.Position = this.ly_store_outnumDailyBindingSource.Find("出库单号", nowOutNum);
                this.ly_store_outnumDailyDataGridView.SelectionChanged += ly_store_innumDataGridView_SelectionChanged;

            }
            else
            {
                this.ly_store_outnumDailyDataGridView.SelectionChanged -= ly_store_innumDataGridView_SelectionChanged;
                this.ly_store_outnumDailyBindingSource.Position = this.ly_store_outnumDailyBindingSource.Find("出库单号", nowOutNum);
                this.ly_store_outnumDailyDataGridView.SelectionChanged += ly_store_innumDataGridView_SelectionChanged;
            }

            this.ly_store_outTableAdapter.Fill(this.lYStoreMange.ly_store_out, nowOutNum, SQLDatabase.nowUserName());
            this.ly_store_outBindingSource.Position = this.ly_store_outBindingSource.Find("物料编号", componentNum);
        }

        private void ly_store_innumDataGridView_SelectionChanged(object sender, EventArgs e)
        {

            if (this.formState != "View")
            {

                return;
            }
            if (null == this.ly_store_outnumDailyDataGridView.CurrentRow) return;

            //int parentId = int.Parse(this.ly_material_plan_mainDataGridView.CurrentRow.Cells["id"].Value.ToString());
            string nowOutNum = this.ly_store_outnumDailyDataGridView.CurrentRow.Cells["出库单号"].Value.ToString();



            this.ly_store_outTableAdapter.Fill(this.lYStoreMange.ly_store_out, nowOutNum, SQLDatabase.nowUserName());
        }

        private void SaveInStyle(string instyle)
        {
            

            string outnumber = ly_store_outnumDailyDataGridView.CurrentRow.Cells["出库单号"].Value.ToString();
            string storename = ly_store_outnumDailyDataGridView.CurrentRow.Cells["仓库"].Value.ToString();

            string delstr = " update ly_store_out  set out_style='" + instyle + "'" +
                " from ly_store_out left join ly_inma0010 on ly_store_out.wzbh=ly_inma0010.wzbh " +
               " where ly_store_out.out_number = '" + outnumber + "' and ly_inma0010.warehouse='" + storename + "'";


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
        private void SaveEmploye(string inemploye)
        {
            
            string outnumber = ly_store_outnumDailyDataGridView.CurrentRow.Cells["出库单号"].Value.ToString();
            string storename = ly_store_outnumDailyDataGridView.CurrentRow.Cells["仓库"].Value.ToString();

            string delstr = " update ly_store_out  set employe='" + inemploye + "'" +
                " from ly_store_out left join ly_inma0010 on ly_store_out.wzbh=ly_inma0010.wzbh " +
               " where ly_store_out.out_number = '" + outnumber + "' and ly_inma0010.warehouse='" + storename + "'";



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

        private void SaveOriginNum(string originNum)
        {

            string outnumber = ly_store_outnumDailyDataGridView.CurrentRow.Cells["出库单号"].Value.ToString();
            string storename = ly_store_outnumDailyDataGridView.CurrentRow.Cells["仓库"].Value.ToString();

            string delstr = " update ly_store_out  set origin_num='" + originNum + "'" +
                " from ly_store_out left join ly_inma0010 on ly_store_out.wzbh=ly_inma0010.wzbh " +
               " where ly_store_out.out_number = '" + outnumber + "' and ly_inma0010.warehouse='" + storename + "'";



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

        private void SaveDepartment(string inbillCode)
        {
            string outnumber = ly_store_outnumDailyDataGridView.CurrentRow.Cells["出库单号"].Value.ToString();
            string storename = ly_store_outnumDailyDataGridView.CurrentRow.Cells["仓库"].Value.ToString();

            string delstr = " update ly_store_out  set out_deptcode='" + inbillCode + "'" +
                " from ly_store_out left join ly_inma0010 on ly_store_out.wzbh=ly_inma0010.wzbh " +
               " where ly_store_out.out_number = '" + outnumber + "' and ly_inma0010.warehouse='" + storename + "'";


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

        private void SaveIndate(string indate)
        {
            

            string outnumber = ly_store_outnumDailyDataGridView.CurrentRow.Cells["出库单号"].Value.ToString();
            string storename = ly_store_outnumDailyDataGridView.CurrentRow.Cells["仓库"].Value.ToString();

            string delstr = " update ly_store_out  set out_date='" + indate + "'" +
                " from ly_store_out left join ly_inma0010 on ly_store_out.wzbh=ly_inma0010.wzbh " +
               " where ly_store_out.out_number = '" + outnumber + "' and ly_inma0010.warehouse='" + storename + "'";


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

        private void ly_store_innumDataGridView_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            DataGridView dgv = sender as DataGridView;

            if (!string.IsNullOrEmpty(ly_store_outnumDailyDataGridView.CurrentRow.Cells["计划编号"].Value.ToString()))
            {
                MessageBox.Show("计划发料只能在这里浏览,不能修改", "注意");
                return;
            }


            if (SQLDatabase.nowUserName() != dgv.CurrentRow.Cells["发料人"].Value.ToString())
            {

                MessageBox.Show("请发料人:" + dgv.CurrentRow.Cells["发料人"].Value.ToString() + " 修改");

                return;
            }

            /////////////////////////////////

            if ("出库类别" == dgv.CurrentCell.OwningColumn.Name)
            {
                if ("借旧还新" == dgv.CurrentCell.Value.ToString() || "借新还旧" == dgv.CurrentCell.Value.ToString())
                {
                    return;
                }
                
                
                
                if ("True" == ly_store_outDataGridView.CurrentRow.Cells["finished1"].Value.ToString())
                {
                    MessageBox.Show("已经签证,不能修改出库类别...");

                    return;

                }





                string sel = "SELECT a.stylecode as 编码,a.stylename as 名称 FROM ly_store_out_styleset a ";


                QueryForm queryForm = new QueryForm();


                queryForm.Sel = sel;
                queryForm.Constr = SQLDatabase.Connectstring;



                queryForm.ShowDialog();




                //dgv.CurrentRow.Cells["out_style"].Value = queryForm.Result;
                dgv.CurrentCell.Value = queryForm.Result1;
                SaveInStyle(queryForm.Result1);

                string outnumber = ly_store_outnumDailyDataGridView.CurrentRow.Cells["出库单号"].Value.ToString();
                this.ly_store_outTableAdapter.Fill(this.lYStoreMange.ly_store_out, outnumber, SQLDatabase.nowUserName());

                return;

            }


            /////////////////////////////////////////////////////

            if ("领料人" == dgv.CurrentCell.OwningColumn.Name)
            {
                if ("True" == ly_store_outDataGridView.CurrentRow.Cells["finished1"].Value.ToString())
                {
                    MessageBox.Show("已经签证,不能修改领料人...");

                    return;

                }



                ChangeValue queryForm = new ChangeValue();

                queryForm.OldValue = dgv.CurrentCell.Value.ToString();
                queryForm.NewValue = "";
                queryForm.ChangeMode = "string";
                queryForm.ShowDialog();



                if (queryForm.NewValue != "")
                {

                    dgv.CurrentCell.Value = queryForm.NewValue;

                    SaveEmploye(queryForm.NewValue);

                }
                else
                {
                    //hT_Manage_ItemDataGridView.CurrentRow.Cells["apply_money"].Value = queryForm.NewValue;
                    //dgv.CurrentRow.Cells["discount_money"].Value = DBNull.Value;
                    //dgv.CurrentRow.Cells["apply_money"].Value = DBNull.Value;
                    //dgv.CurrentRow.Cells["approve_flag"].Value = "False";
                    //SaveChanged();

                }
                string outnumber = ly_store_outnumDailyDataGridView.CurrentRow.Cells["出库单号"].Value.ToString();
                this.ly_store_outTableAdapter.Fill(this.lYStoreMange.ly_store_out, outnumber, SQLDatabase.nowUserName());
                return;

            }


            /////////////////////////////////////////////////////

            /////////////////////////////////////////////////////

            if ("原始单据" == dgv.CurrentCell.OwningColumn.Name)
            {
                if ("True" == ly_store_outDataGridView.CurrentRow.Cells["finished1"].Value.ToString())
                {
                    MessageBox.Show("已经签证,不能修改原始单据...");

                    return;

                }



                ChangeValue queryForm = new ChangeValue();

                queryForm.OldValue = dgv.CurrentCell.Value.ToString();
                queryForm.NewValue = "";
                queryForm.ChangeMode = "string";
                queryForm.ShowDialog();



                if (queryForm.NewValue != "")
                {

                    dgv.CurrentCell.Value = queryForm.NewValue;

                    SaveOriginNum(queryForm.NewValue);

                }
                else
                {
                    //hT_Manage_ItemDataGridView.CurrentRow.Cells["apply_money"].Value = queryForm.NewValue;
                    //dgv.CurrentRow.Cells["discount_money"].Value = DBNull.Value;
                    //dgv.CurrentRow.Cells["apply_money"].Value = DBNull.Value;
                    //dgv.CurrentRow.Cells["approve_flag"].Value = "False";
                    //SaveChanged();

                }
                string outnumber = ly_store_outnumDailyDataGridView.CurrentRow.Cells["出库单号"].Value.ToString();
                this.ly_store_outTableAdapter.Fill(this.lYStoreMange.ly_store_out, outnumber, SQLDatabase.nowUserName());
                return;

            }


            /////////////////////////////////////////////////////
            /////////////////////////////////////////////////////

            if ("部门名称" == dgv.CurrentCell.OwningColumn.Name)
            {
                if ("True" == ly_store_outDataGridView.CurrentRow.Cells["finished1"].Value.ToString())
                {
                    MessageBox.Show("已经签证,不能修改部门名称...");

                    return;

                }



                string sel = "SELECT a.prodcode as 编码,a.prodname as 名称 FROM ly_prod_dept a ";


                QueryForm queryForm = new QueryForm();


                queryForm.Sel = sel;
                queryForm.Constr = SQLDatabase.Connectstring;



                queryForm.ShowDialog();




                dgv.CurrentRow.Cells["部门代码"].Value = queryForm.Result;
                dgv.CurrentRow.Cells["部门名称"].Value = queryForm.Result1;
                SaveDepartment(queryForm.Result);



                return;

                string outnumber = ly_store_outnumDailyDataGridView.CurrentRow.Cells["出库单号"].Value.ToString();
                this.ly_store_outTableAdapter.Fill(this.lYStoreMange.ly_store_out, outnumber, SQLDatabase.nowUserName());
                return;

            }
          
            /////////////////////////////////////////////////////
            if ("出库日期" == dgv.CurrentCell.OwningColumn.Name)
            {
                if ("True" == ly_store_outDataGridView.CurrentRow.Cells["finished1"].Value.ToString())
                {
                    MessageBox.Show("已经签证,不能修改出库日期...");

                    return;

                }



                DatePicker queryForm = new DatePicker();
                //queryForm.Pt = pt;

                if (null != (dgv.CurrentCell.Value))
                    queryForm.NowDate = dgv.CurrentCell.Value.ToString();

                queryForm.ShowDialog();



                if (null != queryForm.NowDate)
                {

                    dgv.CurrentCell.Value = queryForm.NowDate;
                    SaveIndate(queryForm.NowDate);

                }


                string outnumber = ly_store_outnumDailyDataGridView.CurrentRow.Cells["出库单号"].Value.ToString();
                this.ly_store_outTableAdapter.Fill(this.lYStoreMange.ly_store_out, outnumber, SQLDatabase.nowUserName());
                return;

            }

            /////////////////////////////////////////////////////


        }

        private void 打印PToolStripButton_Click(object sender, EventArgs e)
        {
            if (null == this.ly_store_outDataGridView.CurrentRow) return;

            //int parentId = int.Parse(this.ly_material_plan_mainDataGridView.CurrentRow.Cells["id"].Value.ToString());
            if (!string.IsNullOrEmpty(ly_store_outnumDailyDataGridView.CurrentRow.Cells["计划编号"].Value.ToString()))
            {
                MessageBox.Show("非手工领料单只能在这里浏览,不能打印", "注意");
                return;
            }




            frmWaiting.Show(this);

            //this.ly_plan_getmaterialTableAdapter.Fill(this.lYPlanMange.ly_plan_getmaterial, parentId);

            BaseReportView queryForm = new BaseReportView();

            queryForm.Text = "中原精密出库单";

            queryForm.Printdata = this.lYStoreMange;

            queryForm.PrintCrystalReport = new LY_Lingliaodan();


            //string selectFormula;

            //selectFormula = "{ly_store_planitemcount.状态}  =   '原料'  and {ly_store_planitemcount.欠料金额}>0 ";
            //queryForm.PrintCrystalReport.DataDefinition.RecordSelectionFormula = selectFormula;

            frmWaiting.Hide(this);

            queryForm.ShowDialog();
        }

        //private void fillToolStripButton_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        this.ly_store_outTableAdapter.Fill(this.lYStoreMange.ly_store_out, out_numberToolStripTextBox.Text);
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
        //        this.ly_store_innumTableAdapter.Fill(this.lYStoreMange.ly_store_innum, new System.Nullable<System.DateTime>(((System.DateTime)(System.Convert.ChangeType(begindateToolStripTextBox.Text, typeof(System.DateTime))))), new System.Nullable<System.DateTime>(((System.DateTime)(System.Convert.ChangeType(enddateToolStripTextBox.Text, typeof(System.DateTime))))), yonghu_codeToolStripTextBox.Text);
        //    }
        //    catch (System.Exception ex)
        //    {
        //        System.Windows.Forms.MessageBox.Show(ex.Message);
        //    }

        //}
        
    }
}
