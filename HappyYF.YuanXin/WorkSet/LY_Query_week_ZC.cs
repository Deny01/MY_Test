
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
    public partial class LY_Query_week_ZC : Form
    {
        private LY_Salescontract_Daily_ZC ownerForm;

        public LY_Salescontract_Daily_ZC OwnerForm
        {
            get { return ownerForm; }
            set { ownerForm = value; }
        }

        public LY_Query_week_ZC()
        {
            InitializeComponent();
        }

        private void LY_Query_week_Load(object sender, EventArgs e)
        {
            DateTime nowdate = SQLDatabase.GetNowdate();

            string nowyearweek = NowYearWeek(nowdate);

            this.toolStripTextBox1.Text = nowdate.Year.ToString();
            this.toolStripTextBox2.Text = nowdate.Year.ToString();

            int beginyear, endyear;

            beginyear = int.Parse(this.toolStripTextBox1.Text);
            endyear = int.Parse(this.toolStripTextBox2.Text);
            
            this.f_Week_infoTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            this.f_Week_infoTableAdapter.Fill(this.lYSalseRepair.f_Week_info, beginyear, endyear);

            this.f_Week_infoBindingSource.Position = this.f_Week_infoBindingSource.Find("年周", nowyearweek);

        }
        private string  NowYearWeek( DateTime nowdate )
        {

            string nowyearcode;

            SqlConnection conn = new SqlConnection(SQLDatabase .Connectstring);

            string strSql = "f_LY_Getyearweek"; //自定SQL函数

            SqlCommand cmd = new SqlCommand(strSql, conn);

            cmd.CommandType = CommandType.StoredProcedure;

            //cmd.Parameters.Add("@headStr", SqlDbType.NVarChar).Value = "ZQ3"; //输入参数

            cmd.Parameters.Add("@nowdate", SqlDbType.DateTime).Value = nowdate; //输入参数

            cmd.Parameters.Add("@returnString", SqlDbType.VarChar);

            cmd.Parameters["@returnString"].Direction = ParameterDirection.ReturnValue;  //返回参数

            try
            {

                conn.Open();

                object o = cmd.ExecuteScalar();



                nowyearcode = cmd.Parameters["@returnString"].Value.ToString();



                //Response.Write("");



            }

            catch (Exception ex)
            {

                nowyearcode = "";

                MessageBox .Show( ex.Message);



            }

            finally
            {



                if (!(conn.State == ConnectionState.Closed))
                {



                    conn.Close();





                }
              


            }

            return nowyearcode;



        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            int beginyear, endyear;

            beginyear = int.Parse(this.toolStripTextBox1.Text);
            endyear = int.Parse(this.toolStripTextBox2.Text);

           
            this.f_Week_infoTableAdapter.Fill(this.lYSalseRepair.f_Week_info, beginyear, endyear);
        }

        private void f_Week_infoDataGridView_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (null == this.f_Week_infoDataGridView.CurrentRow)
            {

                return;

            }

            string begindate = f_Week_infoDataGridView.CurrentRow.Cells["起始日期"].Value.ToString();
            string enddate = f_Week_infoDataGridView.CurrentRow.Cells["结束日期"].Value.ToString();





            this.OwnerForm.refresh_weekdata(begindate, enddate);
        }

        //private void fillToolStripButton_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        this.f_Week_infoTableAdapter.Fill(this.lYSalseRepair.f_Week_info, ((int)(System.Convert.ChangeType(beginyearToolStripTextBox.Text, typeof(int)))), ((int)(System.Convert.ChangeType(endyearToolStripTextBox.Text, typeof(int)))));
        //    }
        //    catch (System.Exception ex)
        //    {
        //        System.Windows.Forms.MessageBox.Show(ex.Message);
        //    }

        //}
    }
}
