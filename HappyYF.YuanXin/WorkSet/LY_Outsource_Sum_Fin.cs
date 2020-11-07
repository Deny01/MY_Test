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
    public partial class LY_Outsource_Sum_Fin : Form
    {
        public LY_Outsource_Sum_Fin()
        {
            InitializeComponent();
        }

        private void LY_Outsource_Sum_Fin_Load(object sender, EventArgs e)
        {
            this.dateTimePicker1.Text = DateTime.Today.AddMonths(-1).Date.ToString();
            this.dateTimePicker2.Text = DateTime.Today.AddDays(0).Date.ToString();

            this.ly_outsource_contract_detailTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;  
            this.ly_store_in_OutsourceTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring; 
             this.ly_store_out_OutsourceTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
             this.ly_outsource_contract_mainTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;



             this.ly_outsource_contract_mainTableAdapter.Fill(this.lYFinancialMange.ly_outsource_contract_main, this.dateTimePicker1.Value, this.dateTimePicker2.Value.AddDays(1));

        }

        //private void fillToolStripButton_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        this.ly_outsource_contract_mainTableAdapter.Fill(this.lYFinancialMange.ly_outsource_contract_main, new System.Nullable<System.DateTime>(((System.DateTime)(System.Convert.ChangeType(begindateToolStripTextBox.Text, typeof(System.DateTime))))), new System.Nullable<System.DateTime>(((System.DateTime)(System.Convert.ChangeType(enddateToolStripTextBox.Text, typeof(System.DateTime))))));
        //    }
        //    catch (System.Exception ex)
        //    {
        //        System.Windows.Forms.MessageBox.Show(ex.Message);
        //    }

        //}

        //private void fillToolStripButton_Click_1(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        this.ly_store_out_OutsourceTableAdapter.Fill(this.lYFinancialMange.ly_store_out_Outsource, new System.Nullable<System.DateTime>(((System.DateTime)(System.Convert.ChangeType(begindateToolStripTextBox.Text, typeof(System.DateTime))))), new System.Nullable<System.DateTime>(((System.DateTime)(System.Convert.ChangeType(enddateToolStripTextBox.Text, typeof(System.DateTime))))), bill_codeToolStripTextBox.Text);
        //    }
        //    catch (System.Exception ex)
        //    {
        //        System.Windows.Forms.MessageBox.Show(ex.Message);
        //    }

        //}

        private void toolStripButton21_Click(object sender, EventArgs e)
        {
            this.ly_outsource_contract_mainTableAdapter.Fill(this.lYFinancialMange.ly_outsource_contract_main, this.dateTimePicker1.Value, this.dateTimePicker2.Value.AddDays(1));

        }

        private void ly_outsource_contract_mainDataGridView_SelectionChanged(object sender, EventArgs e)
        {
            if (null != this.ly_outsource_contract_mainDataGridView.CurrentRow)
            {


                string nowcontractNum = this.ly_outsource_contract_mainDataGridView.CurrentRow.Cells["合同编号"].Value.ToString();

                this.ly_store_in_OutsourceTableAdapter.Fill(this.lYFinancialMange.ly_store_in_Outsource, this.dateTimePicker1.Value, this.dateTimePicker2.Value.AddDays(1), nowcontractNum);
                this.ly_store_out_OutsourceTableAdapter.Fill(this.lYFinancialMange.ly_store_out_Outsource, this.dateTimePicker1.Value, this.dateTimePicker2.Value.AddDays(1), nowcontractNum);
                this.ly_outsource_contract_detailTableAdapter.Fill(this.lYFinancialMange.ly_outsource_contract_detail, nowcontractNum);

                
                //set_processOrder_Num();


            }
            else
            {
                this.ly_store_in_OutsourceTableAdapter.Fill(this.lYFinancialMange.ly_store_in_Outsource, this.dateTimePicker1.Value, this.dateTimePicker2.Value.AddDays(1), "");
                this.ly_store_out_OutsourceTableAdapter.Fill(this.lYFinancialMange.ly_store_out_Outsource, this.dateTimePicker1.Value, this.dateTimePicker2.Value.AddDays(1), "");
                this.ly_outsource_contract_detailTableAdapter.Fill(this.lYFinancialMange.ly_outsource_contract_detail, "");

                
            }
        }

        //private void fillToolStripButton_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        this.ly_outsource_contract_detailTableAdapter.Fill(this.lYFinancialMange.ly_outsource_contract_detail, contract_codeToolStripTextBox.Text);
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
        //        this.ly_store_in_OutsourceTableAdapter.Fill(this.lYFinancialMange.ly_store_in_Outsource, new System.Nullable<System.DateTime>(((System.DateTime)(System.Convert.ChangeType(begindateToolStripTextBox.Text, typeof(System.DateTime))))), new System.Nullable<System.DateTime>(((System.DateTime)(System.Convert.ChangeType(enddateToolStripTextBox.Text, typeof(System.DateTime))))));
        //    }
        //    catch (System.Exception ex)
        //    {
        //        System.Windows.Forms.MessageBox.Show(ex.Message);
        //    }

        //}
    }
}
