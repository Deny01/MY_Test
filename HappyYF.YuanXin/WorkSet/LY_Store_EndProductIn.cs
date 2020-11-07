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
using System.Data.SqlClient;

namespace HappyYF.YuanXin.WorkSet
{
    public partial class LY_Store_EndProductIn : Form
    {
        public LY_Store_EndProductIn()
        {
            InitializeComponent();
        }

        private void ly_material_plan_mainBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.ly_material_plan_mainBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.lYPlanMange);

        }

        private void LY_Store_ProductIn_Load(object sender, EventArgs e)
        {
            this.ly_material_plan_detail_endProductTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            this.ly_store_inTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            this.ly_material_plan_mainTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            this.ly_material_plan_mainTableAdapter.Fill(this.lYPlanMange.ly_material_plan_main,"SCJH");

            this.ly_material_plan_mainBindingSource.Filter = "启用='true'";
        }

        private void ly_material_plan_mainDataGridView_SelectionChanged(object sender, EventArgs e)
        {
            if (null == this.ly_material_plan_mainDataGridView.CurrentRow) return ;

                     int parentId = int.Parse(this.ly_material_plan_mainDataGridView.CurrentRow.Cells["id"].Value.ToString());
                    string planNum = this.ly_material_plan_mainDataGridView.CurrentRow.Cells["计划编号"].Value.ToString();

                    this.ly_material_plan_detail_endProductTableAdapter.Fill(this.lYMaterialMange.ly_material_plan_detail_endProduct , parentId);

                    this.ly_store_inTableAdapter.Fill(this.lYStoreMange.ly_store_in, planNum);


                 

        }

        private void ly_plan_productlistDataGridView_DoubleClick(object sender, EventArgs e)
        {
            if (null == this.ly_material_plan_mainDataGridView.CurrentRow) return;
            if (null == ly_material_plan_detail_endProductDataGridView.CurrentRow) return;

            string componentNum = this.ly_material_plan_detail_endProductDataGridView.CurrentRow.Cells["产品编号"].Value.ToString();
            string planNum = this.ly_material_plan_mainDataGridView.CurrentRow.Cells["计划编号"].Value.ToString();







            string insStr = " INSERT INTO ly_store_in  " +
           "( wzbh,warehouse,bill_code,operoter) " +
           " values ('" + componentNum + "','" + "生产入库" + "','" + planNum + "','" + SQLDatabase.nowUserName() + "' )";


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




            this.ly_store_inTableAdapter.Fill(this.lYStoreMange.ly_store_in, planNum);

           
        }

        private void bindingNavigatorDeleteItem_Click(object sender, EventArgs e)
        {
            if (null == this.ly_store_inDataGridView.CurrentRow) return;


            string nowPlanNumber = this.ly_store_inDataGridView.CurrentRow.Cells["物料编号1"].Value.ToString();


            string message1 = "当前(物料：" + nowPlanNumber + ")将被删除，继续吗？";
            string caption1 = "提示...";
            MessageBoxButtons buttons1 = MessageBoxButtons.YesNo;

            DialogResult result1;



            result1 = MessageBox.Show(message1, caption1, buttons1,
            MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

            if (result1 == DialogResult.Yes)
            {

                ly_store_inBindingSource.RemoveCurrent();

                this.ly_store_inTableAdapter.Update(this.lYStoreMange.ly_store_in);


            }
        }

        private void 保存SToolStripButton_Click(object sender, EventArgs e)
        {
            ly_store_inDataGridView.EndEdit();
            ly_store_inBindingSource.EndEdit();

            this.ly_store_inTableAdapter.Update(this.lYStoreMange.ly_store_in);


        }

       

        //private void fillToolStripButton_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        this.ly_plan_productlistTableAdapter.Fill(this.lYStoreMange.ly_plan_productlist, plan_numToolStripTextBox.Text);
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
        //        this.ly_store_inTableAdapter.Fill(this.lYStoreMange.ly_store_in, plan_numToolStripTextBox.Text);
        //    }
        //    catch (System.Exception ex)
        //    {
        //        System.Windows.Forms.MessageBox.Show(ex.Message);
        //    }

        //}

       
    }
}
