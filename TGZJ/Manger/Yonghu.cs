using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Transactions;
using System.Windows.Forms;
using TGZJ.Base;

namespace TGZJ.Manger
{
    public partial class Yonghu : BaseForm
    {
        public Yonghu()
        {
            InitializeComponent();
        }

        private void t_usersBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.t_usersBindingSource.EndEdit();
            //this.tableAdapterManager.UpdateAll(this.yonghuDataSet);
            this.t_usersTableAdapter.Update( this.yonghuDataSet.T_users);

            SetViewState("View");

        }

        private void Yonghu_Load(object sender, EventArgs e)
        {
            
           
            

            string cString = Program.dataBase.MakeConnectString();

            this.ly_drawing_levelTableAdapter.Connection.ConnectionString = cString;

            this.ly_pic_typeTableAdapter.Connection.ConnectionString = cString;
            this.ly_drawing_levelTableAdapter.Fill(this.yonghuDataSet.ly_drawing_level);

            this.ly_salesdiscountTableAdapter.Connection.ConnectionString = cString;
            this.ly_salesdiscountTableAdapter.Fill(this.yonghuDataSet.ly_salesdiscount);
            
            //this.yX_teacher_groupTableAdapter.Connection.ConnectionString = cString;
            //this.yX_teacher_groupTableAdapter.Fill(this.happyYF_ZBDataSet.YX_teacher_group);

            this.itemofserviceTableAdapter.Connection.ConnectionString = cString;
            //this.itemofserviceTableAdapter.Fill(this.yonghuDataSet.Itemofservice);

            //this.itemofserviceBindingSource.Filter = "ItemofserviceNumber = '" + "" + "'";
             this.ly_employe_warehouseTableAdapter.Connection.ConnectionString = cString;

             this.t_usersTableAdapter.Connection.ConnectionString = cString;
             this.t_usersTableAdapter.Fill(this.yonghuDataSet.T_users);


            
            string selAllString = "SELECT     parentID, bumenID, bumenBM, bumenMC FROM  T_bumen ORDER BY  bumenID ";


            SqlDataAdapter bumenAdapter = new SqlDataAdapter(selAllString, cString);

            DataSet bumenData = new DataSet();
            bumenAdapter.Fill(bumenData);

            MakeTreeView(bumenData.Tables[0], null, null);
            this.treeView1.ExpandAll();

            SetViewState("View");

        }

        private void SetViewState(string state)
        {
            if ("View" == state)
            {

                this.yhbmTextBox.ReadOnly = true ;
                this.yhmcTextBox.ReadOnly = true ;
                this.pwdTextBox.ReadOnly = true ;
                this.group_IdComboBox.Enabled = false;
                this.month_salaryTextBox.ReadOnly = true;

                this.bindingNavigatorAddNewItem.Enabled = true;
                this.bindingNavigatorDeleteItem.Enabled = true;
                this.toolStripButton1.Enabled = true;
                this.t_usersBindingNavigatorSaveItem.Enabled = false;

                this.bindingNavigatorMoveFirstItem.Enabled = true;
                this.bindingNavigatorMoveLastItem.Enabled = true;
                this.bindingNavigatorMovePreviousItem.Enabled = true;
                this.bindingNavigatorMoveNextItem.Enabled = true;
                this.bindingNavigatorPositionItem.Enabled = true;

                this.genderComboBox.Enabled = false;
                this.identityCardTextBox.ReadOnly = true;
                this.phoneTextBox.ReadOnly = true;
                this.addressTextBox.ReadOnly = true;
                this.inwork_dayDateTimePicker.Enabled = false;
                this.in_active_serviceCheckBox.Enabled = false;
                //this.duty_classComboBox.Enabled = false;
                this.onlineTextBox.ReadOnly = true;

                this.phone2TextBox.ReadOnly = true;
                this.diacount_codeComboBox.Enabled = false;
               
                this.treeView1.Focus();


            }
            else
            {
                this.yhbmTextBox.ReadOnly = false  ;
                this.yhmcTextBox.ReadOnly = false  ;
                this.pwdTextBox.ReadOnly = false  ;
                this.group_IdComboBox.Enabled = true ;
                this.month_salaryTextBox.ReadOnly = false ;

                this.bindingNavigatorAddNewItem.Enabled = false;
                this.bindingNavigatorDeleteItem.Enabled = false;
                this.toolStripButton1.Enabled = false;
                this.t_usersBindingNavigatorSaveItem.Enabled = true;

                this.bindingNavigatorMoveFirstItem.Enabled = false;
                this.bindingNavigatorMoveLastItem.Enabled = false;
                this.bindingNavigatorMovePreviousItem.Enabled = false;
                this.bindingNavigatorMoveNextItem.Enabled = false;
                this.bindingNavigatorPositionItem.Enabled = false;

                this.genderComboBox.Enabled = true ;
                this.identityCardTextBox.ReadOnly = false ;
                this.phoneTextBox.ReadOnly = false ;
                this.addressTextBox.ReadOnly = false ;
                this.inwork_dayDateTimePicker.Enabled = true ;
                this.in_active_serviceCheckBox.Enabled = true ;
                //this.duty_classComboBox.Enabled = true;
                this.onlineTextBox.ReadOnly = false ;

                this.phone2TextBox.ReadOnly = false ;
                this.diacount_codeComboBox.Enabled = true ;

            }

        }



        private void MakeTreeView(DataTable table, string ParentID, System.Windows.Forms.TreeNode PNode)
        {


            DataRow[] dr;

            if (null == ParentID)
                dr = table.Select("ParentID is null");
            else
            {
                //if (ParentID == "0001010102")
                //{
                //    int aaa = 0;


                //}
                string expression;
                expression = "ParentID='" + ParentID + "'";

                dr = table.Select(expression);
            }
            try
            {
                if (dr.Length > 0)
                {
                    foreach (DataRow d in dr)
                    {

                        System.Windows.Forms.TreeNode TNode = new System.Windows.Forms.TreeNode();
                        TNode.Text = d["bumenMC"].ToString();
                        TNode.Tag = d["bumenID"].ToString();
                        if (PNode == null)
                        {
                            this.treeView1.Nodes.Add(TNode);
                        }
                        else
                        {
                            PNode.Nodes.Add(TNode);
                        }

                        MakeTreeView(table, d["bumenID"].ToString(), TNode);
                    }
                }
                else
                {
                    return;
                }
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
            }
        }

        private void bindingNavigatorAddNewItem_Click(object sender, EventArgs e)
        {
            SetViewState("Edit");
            
            TreeNode td = this.treeView1.SelectedNode;

            if (null == td)
            {
                MessageBox.Show("请首先选择部门...");
            }
            else
            {
                this.t_usersBindingSource.AddNew();
                this.bumenTextBox.Text = td.Tag.ToString();
                this.bumenMCTextBox.Text = td.Text;
            }

        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            this.t_usersBindingSource.Filter = "bumen like '" + e.Node.Tag.ToString() +  "%'";
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            SetViewState("Edit");
        }

        private void t_usersBindingSource_CurrentChanged(object sender, EventArgs e)
        {
            System.Windows.Forms.BindingSource bs = sender as BindingSource;

            //this.itemofserviceBindingSource.Filter = "Teacher = '" + ((DataRowView)bs.Current)["yhbm"] + "'";
            //this.duty_classComboBox.DataSource = null;
            //this.duty_classComboBox.DataSource = this.itemofserviceBindingSource;
            //this.duty_classComboBox.DisplayMember = "ItemofserviceName";
            //this.duty_classComboBox.ValueMember = "ItemofserviceNumber";
            if (null != (DataRowView)bs.Current)
            {
                this.ly_employe_warehouseTableAdapter.Fill(this.yonghuDataSet.ly_employe_warehouse, ((DataRowView)bs.Current)["yhbm"].ToString());

                this.ly_pic_typeTableAdapter.Fill(this.yonghuDataSet.ly_pic_type, ((DataRowView)bs.Current)["yhbm"].ToString());

                this.itemofserviceTableAdapter.Fill(this.yonghuDataSet.Itemofservice, ((DataRowView)bs.Current)["yhbm"].ToString());
            }
            //this.duty_classComboBox.Refresh();

        }

        private void t_usersDataGridView_SelectionChanged(object sender, EventArgs e)
        {
            //this.itemofserviceTableAdapter.Fill(this.yonghuDataSet.Itemofservice, this.t_usersDataGridView.CurrentRow.Cells["yhbm"].Value.ToString());
            //this.itemofserviceBindingSource .Filter = "ItemofserviceNumber = '" + this.t_usersDataGridView.CurrentRow.Cells["yhbm"].Value.ToString() + "'";
            if (null != this.t_usersDataGridView.CurrentRow)
               
            this.itemofserviceBindingSource.Position = this.itemofserviceBindingSource.Find("ItemofserviceNumber", this.t_usersDataGridView.CurrentRow.Cells["duty_class"].Value.ToString());
        }

        private void duty_classComboBox_Click(object sender, EventArgs e)
        {
            //this.itemofserviceTableAdapter.Fill(this.yonghuDataSet.Itemofservice, this.t_usersDataGridView.CurrentRow.Cells["yhbm"].Value.ToString());
        }

        private void ly_employe_warehouseDataGridView_DoubleClick(object sender, EventArgs e)
        {
            if (null == ly_employe_warehouseDataGridView.CurrentRow) return;
            if (this.yhbmTextBox.ReadOnly != true) return;

            string warehouseName = this.ly_employe_warehouseDataGridView.CurrentRow.Cells["warehousename"].Value.ToString();
            string haveRight = this.ly_employe_warehouseDataGridView.CurrentRow.Cells["haveright"].Value.ToString();



            if (haveRight == "0")
            {




                string insStr = " INSERT INTO ly_employe_warehouse  " +
               "( yonghu_code,warehouse_name) " +
               " values ('" + yhbmTextBox.Text + "','" + warehouseName + "' )";


                using (TransactionScope scope = new TransactionScope())
                {

                    SqlConnection sqlConnection1 = new SqlConnection(Program.dataBase.MakeConnectString());
                    SqlCommand cmd = new SqlCommand();

                    cmd.CommandText = insStr;
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = sqlConnection1;



                    sqlConnection1.Open();
                    cmd.ExecuteNonQuery();

                    sqlConnection1.Close();

                    scope.Complete();
                }
            }
            else
            {

                string delStr = " delete ly_employe_warehouse  " +
             "where  yonghu_code='"+ yhbmTextBox.Text + "'" + " and warehouse_name ='" + warehouseName + "'";
           

                using (TransactionScope scope = new TransactionScope())
                {

                    SqlConnection sqlConnection1 = new SqlConnection(Program.dataBase.MakeConnectString());
                    SqlCommand cmd = new SqlCommand();

                    cmd.CommandText = delStr;
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = sqlConnection1;



                    sqlConnection1.Open();
                    cmd.ExecuteNonQuery();

                    sqlConnection1.Close();

                    scope.Complete();
                }
            
            }



            this.ly_employe_warehouseTableAdapter.Fill(this.yonghuDataSet.ly_employe_warehouse, yhbmTextBox.Text);

            this.ly_employe_warehouseBindingSource.Position = this.ly_employe_warehouseBindingSource.Find("warehousename", warehouseName);
        }

        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
            if (null == dataGridView1.CurrentRow) return;
            if (this.yhbmTextBox.ReadOnly != true) return;

            string warehouseName = this.dataGridView1.CurrentRow.Cells["pic_type"].Value.ToString();
            string haveRight = this.dataGridView1.CurrentRow.Cells["haveright2"].Value.ToString();



            if (haveRight == "0")
            {
                 

                string insStr = " INSERT INTO ly_employe_pic  " +  "( yonghu_code,pictype_name) " +  " values ('" + yhbmTextBox.Text + "','" + warehouseName + "' )";


                using (TransactionScope scope = new TransactionScope())
                {

                    SqlConnection sqlConnection1 = new SqlConnection(Program.dataBase.MakeConnectString());
                    SqlCommand cmd = new SqlCommand();

                    cmd.CommandText = insStr;
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = sqlConnection1;



                    sqlConnection1.Open();
                    cmd.ExecuteNonQuery();

                    sqlConnection1.Close();

                    scope.Complete();
                }
            }
            else
            {

                string delStr = " delete ly_employe_pic  " + "where  yonghu_code='" + yhbmTextBox.Text + "'" + " and pictype_name ='" + warehouseName + "'";


                using (TransactionScope scope = new TransactionScope())
                {

                    SqlConnection sqlConnection1 = new SqlConnection(Program.dataBase.MakeConnectString());
                    SqlCommand cmd = new SqlCommand();

                    cmd.CommandText = delStr;
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = sqlConnection1;



                    sqlConnection1.Open();
                    cmd.ExecuteNonQuery();

                    sqlConnection1.Close();

                    scope.Complete();
                }

            }



            this.ly_pic_typeTableAdapter.Fill(this.yonghuDataSet.ly_pic_type, yhbmTextBox.Text);

            this.lypictypeBindingSource.Position = this.lypictypeBindingSource.Find("pic_type", warehouseName);

        }
    }
}
