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

namespace HappyYF.YuanXin.WorkSet
{
    public partial class LY_Product_Plan_EndProdut : Form
    {
        public LY_Product_Plan_EndProdut()
        {
            InitializeComponent();
        }

       

        private void LY_Material_Plan__EndProdut_Load(object sender, EventArgs e)
        {
            this.ly_inma0010_planselEndProductTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            this.ly_material_plan_detail_endProductTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            
            this.ly_material_plan_mainTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            this.ly_material_plan_mainTableAdapter.Fill(this.lYPlanMange.ly_material_plan_main, "SCJH");

           // this.ly_material_plan_mainBindingSource.Filter = "启用='true'";
        }

        private void ly_material_plan_mainDataGridView_SelectionChanged(object sender, EventArgs e)
        {
            if (null != this.ly_material_plan_mainDataGridView.CurrentRow)
            {
                int parentId = int.Parse(this.ly_material_plan_mainDataGridView.CurrentRow.Cells["id"].Value.ToString());
                string planNum = this.ly_material_plan_mainDataGridView.CurrentRow.Cells["计划编号"].Value.ToString();

                this.ly_material_plan_detail_endProductTableAdapter.Fill(this.lYMaterialMange.ly_material_plan_detail_endProduct, parentId);
                this.ly_inma0010_planselEndProductTableAdapter.Fill(this.lYPlanMange.ly_inma0010_planselEndProduct , parentId);
               


                this.groupBox2.Text = planNum + ":物料列表";


            }
        }

        private void ly_inma0010_planselDataGridView_DoubleClick(object sender, EventArgs e)
        {
            if (null == this.ly_material_plan_mainDataGridView.CurrentRow) return;
            if (null == ly_inma0010_planselDataGridView.CurrentRow) return;

            string componentNum = this.ly_inma0010_planselDataGridView.CurrentRow.Cells["物资编号"].Value.ToString();
            int parentId = int.Parse(this.ly_material_plan_mainDataGridView.CurrentRow.Cells["id"].Value.ToString());








            string insStr = " INSERT INTO ly_material_plan_detail_endProduct  " +
           "( plan_id,itemno) " +
           " values ('" + parentId + "','" + componentNum + "' )";


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



          
            this.ly_material_plan_detail_endProductTableAdapter.Fill(this.lYMaterialMange.ly_material_plan_detail_endProduct, parentId);
            this.ly_inma0010_planselEndProductTableAdapter.Fill(this.lYPlanMange.ly_inma0010_planselEndProduct, parentId);


            this.ly_material_plan_detail_endProductBindingSource.Position = this.ly_material_plan_detail_endProductBindingSource.Find("产品编号", componentNum);
        }

        private void ly_material_plan_detail_endProductDataGridView_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            DataGridView dgv = sender as DataGridView;




            if ("数量" == dgv.CurrentCell.OwningColumn.Name)
            {

                ChangeValue queryForm = new ChangeValue();

                queryForm.OldValue = dgv.CurrentCell.Value.ToString();
                queryForm.NewValue = "";
                queryForm.ChangeMode = "value";
                queryForm.ShowDialog();




                if (queryForm.NewValue != "")
                {
                    dgv.CurrentRow.Cells["数量"].Value = queryForm.NewValue;
                    //dgv.CurrentRow.Cells["discount_money"].Value = DBNull.Value;

                    //dgv.CurrentRow.Cells["approve_flag"].Value = "False";
                    SaveChanged();


                    

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


            /////////////////////////////////////////////////////




            if ("备注" == dgv.CurrentCell.OwningColumn.Name)
            {
                ChangeValue queryForm = new ChangeValue();

                queryForm.OldValue = dgv.CurrentCell.Value.ToString();
                queryForm.NewValue = "";
                queryForm.ShowDialog();




                if (queryForm.NewValue != "")
                {
                    dgv.CurrentRow.Cells["备注"].Value = queryForm.NewValue;
                    //dgv.CurrentRow.Cells["discount_money"].Value = DBNull.Value;

                    //dgv.CurrentRow.Cells["approve_flag"].Value = "False";
                    SaveChanged();

                }
                else
                {


                }
                return;
            }
        }

        private void SaveChanged()
        {
            ///////////////////////////

            this.ly_material_plan_detail_endProductDataGridView.EndEdit();


            this.ly_material_plan_detail_endProductBindingSource.EndEdit();



            this.ly_material_plan_detail_endProductTableAdapter.Update(this.lYMaterialMange.ly_material_plan_detail_endProduct );
          


        }

        private void 删除子件ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (null == this.ly_material_plan_detail_endProductDataGridView.CurrentRow) return;


            int nowId = int.Parse(this.ly_material_plan_detail_endProductDataGridView.CurrentRow.Cells["id1"].Value.ToString());
            int parentId = int.Parse(this.ly_material_plan_mainDataGridView.CurrentRow.Cells["id"].Value.ToString());
            string componentNum = this.ly_material_plan_detail_endProductDataGridView.CurrentRow.Cells["产品编号"].Value.ToString();


            string message1 = "当前(物料：" + componentNum + ")将被删除，继续吗？";
            string caption1 = "提示...";
            MessageBoxButtons buttons1 = MessageBoxButtons.YesNo;

            DialogResult result1;



            result1 = MessageBox.Show(message1, caption1, buttons1,
            MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

            if (result1 == DialogResult.Yes)
            {

                string delstr = " delete ly_material_plan_detail_endProduct  where id = " + nowId + "";





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
                    

                    

                    this.ly_material_plan_detail_endProductTableAdapter.Fill(this.lYMaterialMange.ly_material_plan_detail_endProduct, parentId);
                    this.ly_inma0010_planselEndProductTableAdapter.Fill(this.lYPlanMange.ly_inma0010_planselEndProduct, parentId);


                    this.ly_inma0010_planselEndProductBindingSource.Position = this.ly_inma0010_planselEndProductBindingSource.Find("物资编号", componentNum);

                  
                }


            }
        }

        //private void fillToolStripButton_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        this.ly_inma0010_planselEndProductTableAdapter.Fill(this.lYPlanMange.ly_inma0010_planselEndProduct, new System.Nullable<int>(((int)(System.Convert.ChangeType(plan_idToolStripTextBox.Text, typeof(int))))));
        //    }
        //    catch (System.Exception ex)
        //    {
        //        System.Windows.Forms.MessageBox.Show(ex.Message);
        //    }

        //}

       

       

       

        

       
    }
}
