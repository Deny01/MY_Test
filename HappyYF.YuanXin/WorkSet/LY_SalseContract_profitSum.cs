using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HappyYF.Infrastructure.Repositories;
using System.Transactions;
using DataGridFilter;
using System.Data.SqlClient;

namespace HappyYF.YuanXin.WorkSet
{
    public partial class LY_SalseContract_profitSum : Form
    {
        public LY_SalseContract_profitSum()
        {
            InitializeComponent();
        }

        List<string> itemlist = new List<string>();

        private void LY_sales_ClientSUM_Marposs_Load(object sender, EventArgs e)
        {
            this.dateTimePicker9.Text = SQLDatabase.GetNowdate().Date.Year.ToString() + "-01" + "-01";
            this.dateTimePicker10.Text = SQLDatabase.GetNowdate().AddDays(0).Date.ToString();

            this.ly_sales_profit_sumTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
        }

        private void toolStripButton38_Click(object sender, EventArgs e)
        {
            this.ly_sales_profit_sumTableAdapter.Fill(this.lYSalseMange2.ly_sales_profit_sum, this.dateTimePicker9.Value, this.dateTimePicker10.Value.AddDays(1));
            
        }

        private void toolStripButton37_Click(object sender, EventArgs e)
        {
            ExportDataGridviewTOExcell.ExportDataGridview(this.ly_sales_profit_sumDataGridView, true);
        }

        private void ly_sales_profit_sumDataGridView_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {

            DataGridView dgv = sender as DataGridView;

            if (null == dgv.CurrentRow) return;

            ////////////////////////////////////////

            if ("业务费用因子" == dgv.CurrentCell.OwningColumn.Name || "利润率基值" == dgv.CurrentCell.OwningColumn.Name)
            {



               
                ChangeValue queryForm = new ChangeValue();

                queryForm.OldValue = dgv.CurrentCell.Value.ToString();
                queryForm.NewValue = "";
                queryForm.ChangeMode = "value";
                queryForm.setInFocus();
                queryForm.ShowDialog(this);





                if (queryForm.NewValue != "")
                {
                    ////////////////////////////////////////////////

                    string updstr ;

                    if ("业务费用因子" == dgv.CurrentCell.OwningColumn.Name)
                    {
                        updstr = " update ly_sales_profit_arrt  set buseness_arrt = " + queryForm.NewValue;
                    }
                    else
                    {
                        updstr = " update ly_sales_profit_arrt  set profit_arrt = " + queryForm.NewValue;
                    
                    }






                    SqlConnection sqlConnection1 = new SqlConnection(SQLDatabase.Connectstring);
                    SqlCommand cmd = new SqlCommand();

                    cmd.CommandText = updstr;
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = sqlConnection1;
                    cmd.CommandTimeout = 0;

                    using (TransactionScope scope = new TransactionScope())
                    {

                        sqlConnection1.Open();
                        try
                        {

                            cmd.ExecuteNonQuery();



                            scope.Complete();



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

                }
                else
                {
                    

                }

                this.ly_sales_profit_sumTableAdapter.Fill(this.lYSalseMange2.ly_sales_profit_sum, this.dateTimePicker9.Value, this.dateTimePicker10.Value.AddDays(1));

                return;

            }


            /////////////////////////////////////////////////////


           
        }

       

      
      

       

       
    }
}
