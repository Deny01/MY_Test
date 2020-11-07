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
    public partial class LY_Store_In : Form
    {
        private string formState;
        string  nowInNum;
        string  nowdate;

        string nowInstyle;

        string nowmemory_rec;
        string changeflag = "no";
        
        public LY_Store_In()
        {
            InitializeComponent();
        }

       

        private void LY_MaterialMange_Load(object sender, EventArgs e)
        {
            this.dateTimePicker1.Text = DateTime.Today.AddMonths(-1).Date.ToString();
            this.dateTimePicker2.Text = DateTime.Today.AddDays(1).Date.ToString();

            nowmemory_rec = SQLDatabase.NowUserID + "_" + SQLDatabase.nowMachineName() + "_" + SQLDatabase.nowMachinecode();
            this.ly_store_in_innumTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            this.ly_store_innumTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
         
           
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

            dFilter = GetDataGridviewMultiFilterString.DGVMultiFilterString(this.ly_inma0010_inoutDataGridView, this.toolStripTextBox1.Text);

            if (null == dFilter)
                dFilter = "";

            this.ly_inma0010_inoutBindingSource.Filter = dFilter;

            //for (int i = 0; i < this.hT_Vehicle_collectDataGridView.ColumnCount; i++)
            //for (int i = 1; i < 11; i++)
            //{
            //    string tempColumnName = this.ly_inma0010_inoutDataGridView.Columns[i].DataPropertyName;
              
            //    if (i != 10)
            //        dFilter = dFilter + tempColumnName + " like  '*" + this.toolStripTextBox1.Text + "*' or ";
            //    else
            //        dFilter = dFilter + tempColumnName + " like  '*" + this.toolStripTextBox1.Text + "*' ";

            //}

            //if (this.toolStripTextBox1.Text.Replace(" ", "").Length > 0)

            //    this.ly_inma0010_inoutBindingSource.Filter = dFilter;
            //else
            //    this.ly_inma0010_inoutBindingSource.Filter = " ";
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

            if (null == ly_store_in_ylDataGridView.CurrentRow) return;

            string nowINstyle = this.ly_store_innumDataGridView.CurrentRow.Cells["入库类别1"].Value.ToString();

            if (nowINstyle == "借旧还新" || nowINstyle == "借新还旧")
            {
                MessageBox.Show("入库类别:" + nowINstyle + "不能在这里删除", "注意");
                return;
            }


            string nowoperptar = this.ly_store_in_ylDataGridView.CurrentRow.Cells["录入人"].Value.ToString();
            if (nowoperptar != SQLDatabase.nowUserName())
            {
                MessageBox.Show("请收料人:" + nowoperptar + "删除", "注意");
                return;
            }

            if ("True" == ly_store_innumDataGridView.CurrentRow.Cells["签证"].Value.ToString())
            {
                MessageBox.Show("已经签证,不能删除...");

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

                changeflag = "no";


                if (this.nowmemory_rec != ly_store_in_ylDataGridView.CurrentRow.Cells["memory_rec"].Value.ToString())
                    {
                        ly_store_in_ylDataGridView.CurrentRow.Cells["memory_rec"].Value = this.nowmemory_rec;
                        changeflag = "yes";
                    }


                
                if ("yes" == changeflag)
                {
                    ly_store_in_ylDataGridView.EndEdit();
                    ly_store_in_innumBindingSource.EndEdit();

                    this.ly_store_in_innumTableAdapter.Update(this.lYStoreMange.ly_store_in_innum);
                }
                
                
                
                this.ly_store_in_innumBindingSource.RemoveCurrent();


                SaveStoreInDetail();

                //string s = this.xD_Sel_SellBalanceDataGridView.CurrentRow.Cells["编号"].Value.ToString();

                //this.hS_ClientPaymentTableAdapter.Fill(this.xD_SellBalance.HS_ClientPayment, s);


            }
        }

        private void SaveStoreInDetail()
        {
            ly_store_in_ylDataGridView.EndEdit();
            ly_store_in_innumBindingSource.EndEdit();

            this.ly_store_in_innumTableAdapter.Update(this.lYStoreMange.ly_store_in_innum);

            string nowInNum = this.ly_store_innumDataGridView.CurrentRow.Cells["入库单号"].Value.ToString();



            this.ly_store_in_innumTableAdapter.Fill(this.lYStoreMange.ly_store_in_innum, nowInNum,SQLDatabase .NowUserID);
        }

      

        

        private void bindingNavigatorDeleteItem_Click_1(object sender, EventArgs e)
        {

            if (null == this.ly_store_innumDataGridView.CurrentRow) return;

            string nowINstyle = this.ly_store_innumDataGridView.CurrentRow.Cells["入库类别1"].Value.ToString();

            if (nowINstyle == "借旧还新" || nowINstyle=="借新还旧")
            {
                MessageBox.Show("入库类别:" + nowINstyle + "不能在这里删除删除", "注意");
                return;
            }


            string nowoperptar = this.ly_store_innumDataGridView.CurrentRow.Cells["收料人"].Value.ToString();

            if (nowoperptar != SQLDatabase.nowUserName())
            {
                MessageBox.Show("请收料人:" + nowoperptar + "删除", "注意");
                return;
            }

            if ("True" == ly_store_innumDataGridView.CurrentRow.Cells["签证"].Value.ToString())
            {
                MessageBox.Show("已经签证,不能删除入库单...");

                return;

            }

            
            //////////////////

            string innumber = ly_store_innumDataGridView.CurrentRow.Cells["入库单号"].Value.ToString();
            string storename = ly_store_innumDataGridView.CurrentRow.Cells["仓库"].Value.ToString();

            string message = "删除当前入库单:" + innumber + "吗？";
            string caption = "提示...";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;

            DialogResult result;



            result = MessageBox.Show(message, caption, buttons,
            MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

            if (result == DialogResult.Yes)
            {


                int changeflagint = 0;

                foreach (DataGridViewRow dgr in ly_store_in_ylDataGridView.Rows)
                {
                    if (this.nowmemory_rec != dgr.Cells["memory_rec"].Value.ToString())
                    {
                        dgr.Cells["memory_rec"].Value = this.nowmemory_rec;
                        changeflagint = changeflagint + 1;
                    }


                }
                if (0 < changeflagint)
                {
                    ly_store_in_ylDataGridView.EndEdit();
                    ly_store_in_innumBindingSource.EndEdit();

                    this.ly_store_in_innumTableAdapter.Update(this.lYStoreMange.ly_store_in_innum);
                }
                
                
                
                
                
                string delstr = " delete ly_store_in  from ly_store_in left join ly_inma0010 on ly_store_in.wzbh=ly_inma0010.wzbh  " +
                    " where ly_store_in.in_number = '" + innumber + "' and ly_inma0010.warehouse='" + storename + "'";


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

                this.ly_store_innumBindingSource.RemoveCurrent();





            }


        }

        private void 保存SToolStripButton_Click(object sender, EventArgs e)
        {
            SaveStoreInDetail();

            this.SetFormState("View");
        }

        private void toolStripButton1_Click_1(object sender, EventArgs e)
        {

            
            /////////////////////////////

            if (null == this.ly_store_innumDataGridView.CurrentRow) return;

            string nowoperptar = this.ly_store_innumDataGridView.CurrentRow.Cells["收料人"].Value.ToString();

            if (nowoperptar != SQLDatabase.nowUserName())
            {
                MessageBox.Show("请收料人:" + nowoperptar + "修改", "注意");
                return;
            }

            if ("True" == ly_store_innumDataGridView.CurrentRow.Cells["签证"].Value.ToString())
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

            this.nowInNum = ly_store_innumDataGridView.CurrentRow.Cells["入库单号"].Value.ToString();
            this.nowdate = ly_store_innumDataGridView.CurrentRow.Cells["入库日期1"].Value.ToString();
            

            this.SetFormState("Edit");
        }
        private void SetFormState(string state)
        {
            // view edit add save cancle

            if ("View" == state)
            {
                this.formState = "View";
                this.ly_inma0010_inoutDataGridView.Enabled = false ;
                this.ly_store_innumDataGridView.Enabled = true;

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



                this.ly_store_in_ylDataGridView.ReadOnly = true;

                foreach (DataGridViewColumn dvc in ly_store_in_ylDataGridView.Columns)
                {
                    //if ("单据编号" == dvc.Name
                    //     || "入库日期" == dvc.Name
                    //     || "入库数量" == dvc.Name
                    //     || "入库单价" == dvc.Name
                    //     || "入库说明" == dvc.Name
                    //     || "采购员" == dvc.Name)
                    if ( "入库数量" == dvc.Name
                         || "入库单价" == dvc.Name
                         || "入库说明" == dvc.Name)
                        
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
                this.ly_store_innumDataGridView.Enabled = false ;
                //this.nowRow = ly_store_in_ylDataGridView.CurrentRow.Index;

                this.ly_store_in_ylDataGridView.ReadOnly = false;

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



                this.ly_store_in_ylDataGridView.ReadOnly = false;

                foreach (DataGridViewColumn dvc in ly_store_in_ylDataGridView.Columns)
                {
                    //if (    "单据编号" == dvc.Name
                    //     || "入库日期" == dvc.Name
                    //     || "入库数量" == dvc.Name
                    //     || "入库单价" == dvc.Name
                    //     || "入库说明" == dvc.Name
                    //     || "采购员" == dvc.Name)
                     if (  "入库数量" == dvc.Name
                         || "入库单价" == dvc.Name
                         || "入库说明" == dvc.Name
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

            this.ly_store_in_innumTableAdapter.Fill(this.lYStoreMange.ly_store_in_innum , componentNum,SQLDatabase .NowUserID);
        }

        private void ly_store_in_ylDataGridView_SelectionChanged(object sender, EventArgs e)
        {
            if (null == ly_store_in_ylDataGridView.CurrentRow) return;

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



            if ("入库数量" == dgc.OwningColumn.Name || "入库单价" == dgc.OwningColumn.Name)
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
            if (true == ly_store_in_ylDataGridView.ReadOnly) return;
          
                //if ("单据编号" == ly_store_in_ylDataGridView.CurrentCell.OwningColumn.Name
                //         || "入库日期" == ly_store_in_ylDataGridView.CurrentCell.OwningColumn.Name
                //         || "入库数量" == ly_store_in_ylDataGridView.CurrentCell.OwningColumn.Name
                //         || "入库单价" == ly_store_in_ylDataGridView.CurrentCell.OwningColumn.Name
                //         || "入库说明" == ly_store_in_ylDataGridView.CurrentCell.OwningColumn.Name
                //         || "采购员" == ly_store_in_ylDataGridView.CurrentCell.OwningColumn.Name)
            if ("入库数量" == ly_store_in_ylDataGridView.CurrentCell.OwningColumn.Name
                        || "入库单价" == ly_store_in_ylDataGridView.CurrentCell.OwningColumn.Name
                        || "入库说明" == ly_store_in_ylDataGridView.CurrentCell.OwningColumn.Name)
            {
                //SetlastColumn();
                ly_store_in_ylDataGridView.CurrentCell.Style.BackColor = Color.White;
                ly_store_in_ylDataGridView.CurrentCell.Style.ForeColor = Color.Teal;

              
            }
        }

        private void ly_store_in_ylDataGridView_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
            if (true == ly_store_in_ylDataGridView.ReadOnly) return;

            //if ("单据编号" == ly_store_in_ylDataGridView.CurrentCell.OwningColumn.Name
            //         || "入库日期" == ly_store_in_ylDataGridView.CurrentCell.OwningColumn.Name
            //         || "入库数量" == ly_store_in_ylDataGridView.CurrentCell.OwningColumn.Name
            //         || "入库单价" == ly_store_in_ylDataGridView.CurrentCell.OwningColumn.Name
            //         || "入库说明" == ly_store_in_ylDataGridView.CurrentCell.OwningColumn.Name
            //         || "采购员" == ly_store_in_ylDataGridView.CurrentCell.OwningColumn.Name)

            if ("入库数量" == ly_store_in_ylDataGridView.CurrentCell.OwningColumn.Name
                        || "入库单价" == ly_store_in_ylDataGridView.CurrentCell.OwningColumn.Name
                        || "入库说明" == ly_store_in_ylDataGridView.CurrentCell.OwningColumn.Name)
            {
                //SetlastColumn();
                ly_store_in_ylDataGridView.CurrentCell.Style.BackColor = Color.Teal ;
                ly_store_in_ylDataGridView.CurrentCell.Style.ForeColor = Color.White ;


            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.ly_store_innumTableAdapter.Fill(this.lYStoreMange.ly_store_innum , DateTime.Parse(this.dateTimePicker1.Text).Date, DateTime.Parse(this.dateTimePicker2.Text).Date,SQLDatabase .NowUserID );

        }

        private void bindingNavigatorAddNewItem_Click_1(object sender, EventArgs e)
        {
            string sel = "SELECT a.stylecode as 编码,a.stylename as 名称 FROM ly_store_in_styleset a ";


            QueryForm queryForm = new QueryForm();


            queryForm.Sel = sel;
            queryForm.Constr = SQLDatabase.Connectstring;



            queryForm.ShowDialog();



            if (string.IsNullOrEmpty(queryForm.Result1))
            {
                MessageBox.Show("必须选择入库类别,才能入库...", "注意");
                return;
            }
            else
            {
                this.nowInstyle = queryForm.Result1;

               

            }
            /////////////////////////////////////////////////////
            
            
            
            
            this.SetFormState("Edit");
            //this.nowInNum = GetMaxStoreInnum();
            this.nowInNum = "NewInnum";
            //this.nowdate = DateTime.Now.Date.ToString();
            this.nowdate =SQLDatabase .GetNowdate() .ToString ();
            this.ly_store_in_innumTableAdapter.Fill(this.lYStoreMange.ly_store_in_innum, "asd",SQLDatabase .NowUserID);

        }

        private DateTime GetNowdate()
        {
            SqlConnection sqlConnection1 = new SqlConnection(SQLDatabase.Connectstring);
            SqlCommand cmd = new SqlCommand();

            DateTime Nowdate = DateTime.Now.Date;

            //cmd.Parameters.Add("@Plan_mode", SqlDbType.VarChar);
            //cmd.Parameters["@Plan_mode"].Value = "LLJH";


            cmd.CommandText = "select getdate() ";
            cmd.CommandType = CommandType.Text ;
            cmd.Connection = sqlConnection1;

            sqlConnection1.Open();
            Nowdate = DateTime .Parse ( cmd.ExecuteScalar().ToString()).Date ;
            sqlConnection1.Close();



            return Nowdate;

        }
        private string GetMaxStoreInnum()
        {

            SqlConnection sqlConnection1 = new SqlConnection(SQLDatabase.Connectstring);
            SqlCommand cmd = new SqlCommand();

            string MaxPlanCode = "";

            //cmd.Parameters.Add("@Plan_mode", SqlDbType.VarChar);
            //cmd.Parameters["@Plan_mode"].Value = "LLJH";


            cmd.CommandText = "LY_Get_InNumber";
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

            if (this.nowInNum == "NewInnum")
            {

                this.nowInNum = GetMaxStoreInnum();
                newFlag = "Y";
            }
            else 
            {

                newFlag = "N";
            }





            string insStr = " INSERT INTO ly_store_in  " +
           "( wzbh,in_style,in_number,input_date,operoter) " +
           " values ('" + componentNum + "','" + nowInstyle +"','"+ nowInNum + "','" + nowdate + "','" + SQLDatabase.nowUserName() + "' )";

            //nowOutstyle
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
                this.ly_store_innumDataGridView.SelectionChanged -= ly_store_innumDataGridView_SelectionChanged;
                this.ly_store_innumTableAdapter.Fill(this.lYStoreMange.ly_store_innum, DateTime.Parse(this.dateTimePicker1.Text).Date, DateTime.Now .AddDays (1).Date, SQLDatabase.NowUserID);
                this.ly_store_innumBindingSource.Position = this.ly_store_innumBindingSource.Find("入库单号", nowInNum);
                this.ly_store_innumDataGridView.SelectionChanged += ly_store_innumDataGridView_SelectionChanged;

            }

            this.ly_store_in_innumTableAdapter.Fill(this.lYStoreMange.ly_store_in_innum, nowInNum,SQLDatabase .NowUserID);
            this.ly_store_in_innumBindingSource.Position = this.ly_store_in_innumBindingSource.Find("物料编号", componentNum);
        }

        private void ly_store_innumDataGridView_SelectionChanged(object sender, EventArgs e)
        {

            if (this.formState != "View")
            {

                return;
            }
            if (null == this.ly_store_innumDataGridView.CurrentRow) return;

            //int parentId = int.Parse(this.ly_material_plan_mainDataGridView.CurrentRow.Cells["id"].Value.ToString());
            string nowInNum = this.ly_store_innumDataGridView.CurrentRow.Cells["入库单号"].Value.ToString();



            this.ly_store_in_innumTableAdapter.Fill(this.lYStoreMange.ly_store_in_innum, nowInNum,SQLDatabase .NowUserID);

            changeflag = "no";

            foreach (DataGridViewRow dgr in ly_store_in_ylDataGridView.Rows)
            {
                if (this.nowmemory_rec != dgr.Cells["memory_rec"].Value .ToString () )
                {
                   // dgr.Cells["memory_rec"].Value = SQLDatabase.NowUserID + SQLDatabase.nowMachinecode();
                    changeflag = "yes";
                }
                
               
            }
            //if ("yes" == changeflag)
            //{
            //    ly_store_in_ylDataGridView.EndEdit();
            //    ly_store_in_innumBindingSource.EndEdit();

            //    this.ly_store_in_innumTableAdapter.Update(this.lYStoreMange.ly_store_in_innum);
            //}


        }

        private void SaveInStyle(string instyle)
        {
            string innumber = ly_store_innumDataGridView.CurrentRow.Cells["入库单号"].Value.ToString();
            string storename = ly_store_innumDataGridView.CurrentRow.Cells["仓库"].Value.ToString();

            string delstr = " update ly_store_in  set in_style='" + instyle + "'"+
                " from ly_store_in left join ly_inma0010 on ly_store_in.wzbh=ly_inma0010.wzbh "+
               " where ly_store_in.in_number = '" + innumber + "' and ly_inma0010.warehouse='" + storename + "'";


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
            string innumber = ly_store_innumDataGridView.CurrentRow.Cells["入库单号"].Value.ToString();
            string storename = ly_store_innumDataGridView.CurrentRow.Cells["仓库"].Value.ToString();

            string delstr = " update ly_store_in  set employe='" + inemploye + "'" +
                " from ly_store_in left join ly_inma0010 on ly_store_in.wzbh=ly_inma0010.wzbh " +
               " where ly_store_in.in_number = '" + innumber + "' and ly_inma0010.warehouse='" + storename + "'";


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

        private void SaveBllcode(string inbillCode)
        {
            string innumber = ly_store_innumDataGridView.CurrentRow.Cells["入库单号"].Value.ToString();
            string storename = ly_store_innumDataGridView.CurrentRow.Cells["仓库"].Value.ToString();

            string delstr = " update ly_store_in  set bill_code='" + inbillCode + "'" +
                " from ly_store_in left join ly_inma0010 on ly_store_in.wzbh=ly_inma0010.wzbh " +
               " where ly_store_in.in_number = '" + innumber + "' and ly_inma0010.warehouse='" + storename + "'";


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
            string innumber = ly_store_innumDataGridView.CurrentRow.Cells["入库单号"].Value.ToString();
            string storename = ly_store_innumDataGridView.CurrentRow.Cells["仓库"].Value.ToString();

            string delstr = " update ly_store_in  set input_date='" + indate + "'" +
                " from ly_store_in left join ly_inma0010 on ly_store_in.wzbh=ly_inma0010.wzbh " +
                " where ly_store_in.in_number = '" + innumber + "' and ly_inma0010.warehouse='" + storename + "'";


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
                //
            }

        }

        private void ly_store_innumDataGridView_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            DataGridView dgv = sender as DataGridView;

            if (SQLDatabase.nowUserName() != dgv.CurrentRow.Cells["收料人"].Value.ToString() && "000" != SQLDatabase.NowUserID)
            {

                MessageBox.Show("请收料人:" + dgv.CurrentRow.Cells["收料人"].Value.ToString() + " 修改");

                return;
            }

            /////////////////////////////////

            if ("入库类别1" == dgv.CurrentCell.OwningColumn.Name)
            {
                if ("借旧还新" == dgv.CurrentCell.Value.ToString() || "借新还旧" == dgv.CurrentCell.Value.ToString())
                {
                    return;
                }
                
                if ("True" == ly_store_in_ylDataGridView.CurrentRow.Cells["签证1"].Value.ToString() && "000"!=SQLDatabase .NowUserID )
                {
                    MessageBox.Show("已经签证,不能修改入库类别...");

                    return;

                }
                

              
                string sel = "SELECT a.stylecode as 编码,a.stylename as 名称 FROM ly_store_in_styleset a ";


                QueryForm queryForm = new QueryForm();


                queryForm.Sel = sel;
                queryForm.Constr = SQLDatabase.Connectstring;



                queryForm.ShowDialog();




                //dgv.CurrentRow.Cells["out_style"].Value = queryForm.Result;
                dgv.CurrentCell.Value = queryForm.Result1;
                SaveInStyle(queryForm.Result1);

                string innumber = ly_store_innumDataGridView.CurrentRow.Cells["入库单号"].Value.ToString();
                this.ly_store_in_innumTableAdapter.Fill(this.lYStoreMange.ly_store_in_innum, innumber,SQLDatabase .NowUserID);

                return;

            }


            /////////////////////////////////////////////////////

            if ("采购员1" == dgv.CurrentCell.OwningColumn.Name)
            {
                if ("True" == ly_store_in_ylDataGridView.CurrentRow.Cells["签证1"].Value.ToString())
                {
                    MessageBox.Show("已经签证,不能修改采购员...");

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
                string innumber = ly_store_innumDataGridView.CurrentRow.Cells["入库单号"].Value.ToString();
                this.ly_store_in_innumTableAdapter.Fill(this.lYStoreMange.ly_store_in_innum, innumber, SQLDatabase.NowUserID);
                return;

            }


            /////////////////////////////////////////////////////
            /////////////////////////////////////////////////////

            if ("单据编号1" == dgv.CurrentCell.OwningColumn.Name)
            {
                if ("True" == ly_store_in_ylDataGridView.CurrentRow.Cells["签证1"].Value.ToString())
                {
                    MessageBox.Show("已经签证,不能修改单据编号...");

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

                    SaveBllcode(queryForm.NewValue);

                }
                else
                {
                    //hT_Manage_ItemDataGridView.CurrentRow.Cells["apply_money"].Value = queryForm.NewValue;
                    //dgv.CurrentRow.Cells["discount_money"].Value = DBNull.Value;
                    //dgv.CurrentRow.Cells["apply_money"].Value = DBNull.Value;
                    //dgv.CurrentRow.Cells["approve_flag"].Value = "False";
                    //SaveChanged();

                }

                string innumber = ly_store_innumDataGridView.CurrentRow.Cells["入库单号"].Value.ToString();
                this.ly_store_in_innumTableAdapter.Fill(this.lYStoreMange.ly_store_in_innum, innumber, SQLDatabase.NowUserID);
                return;

            }
          
            /////////////////////////////////////////////////////
            if ("入库日期1" == dgv.CurrentCell.OwningColumn.Name)
            {
                if ("True" == ly_store_in_ylDataGridView.CurrentRow.Cells["签证1"].Value.ToString())
                {
                    MessageBox.Show("已经签证,不能修改入库日期...");

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
               

                string innumber = ly_store_innumDataGridView.CurrentRow.Cells["入库单号"].Value.ToString();
                this.ly_store_in_innumTableAdapter.Fill(this.lYStoreMange.ly_store_in_innum, innumber, SQLDatabase.NowUserID);
                return;

            }

            /////////////////////////////////////////////////////


        }

        private void 打印PToolStripButton_Click(object sender, EventArgs e)
        {
            if (null == this.ly_store_in_ylDataGridView.CurrentRow) return;

            //int parentId = int.Parse(this.ly_material_plan_mainDataGridView.CurrentRow.Cells["id"].Value.ToString());





            frmWaiting.Show(this);

            //this.ly_plan_getmaterialTableAdapter.Fill(this.lYPlanMange.ly_plan_getmaterial, parentId);

            BaseReportView queryForm = new BaseReportView();

            queryForm.Text = "中原精密入库单";

            queryForm.Printdata = this.lYStoreMange;

            queryForm.PrintCrystalReport = new LY_Rukudan();


            //string selectFormula;

            //selectFormula = "{ly_store_planitemcount.状态}  =   '原料'  and {ly_store_planitemcount.欠料金额}>0 ";
            //queryForm.PrintCrystalReport.DataDefinition.RecordSelectionFormula = selectFormula;

            frmWaiting.Hide(this);

            queryForm.ShowDialog();
        }

        private void 修改物料基本信息ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (null == ly_inma0010_inoutDataGridView.CurrentRow) return;


            string s = this.ly_inma0010_inoutDataGridView.CurrentRow.Cells["物资编号"].Value.ToString();
            string nowxh = this.ly_inma0010_inoutDataGridView.CurrentRow.Cells["id"].Value.ToString();
            //int loanid = int.Parse(this.xD_Client_LoanDataGridView.CurrentRow.Cells["Id_loan"].Value.ToString());

            

            LY_MaterialAdd queryForm = new LY_MaterialAdd();

            queryForm.statemode = "原料";
            queryForm.runmode = "修改";
            queryForm.material_code = s;

            queryForm.StartPosition = FormStartPosition.CenterParent;
            queryForm.ShowDialog();

            if (queryForm.DialogResult != DialogResult.Cancel)
            {
               // this.ly_inma0010ylTableAdapter.Fill(this.lYMaterialMange.ly_inma0010yl);

                this.ly_inma0010_inoutTableAdapter.Fill(this.lYMaterialMange.ly_inma0010_inout, SQLDatabase.NowUserID);

                int nowpos = this.ly_inma0010_inoutBindingSource.Find("id", nowxh);

                //if (nowpos < 1)
                //{
                //    nowpos = this.ly_inma0010ylBindingSource.Find("物资编号", nowxh);
                //}

                this.ly_inma0010_inoutBindingSource.Position = nowpos; // this.ly_inma0010ylBindingSource.Find("物资编号", s);
            }
        }

       


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
