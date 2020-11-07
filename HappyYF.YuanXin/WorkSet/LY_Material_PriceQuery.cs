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


using System.Transactions;
using DataGridFilter;


namespace HappyYF.YuanXin.WorkSet
{
    public partial class LY_Material_PriceQuery : Form
    {
        string itemNum;

        public string ItemNum
        {
            get { return itemNum; }
            set { itemNum = value; }
        }
        string itemname;

        public string Itemname
        {
            get { return itemname; }
            set { itemname = value; }
        }
        
        public LY_Material_PriceQuery()
        {
            InitializeComponent();
            this.ly_material_price_InfoTableAdapter.CommandTimeout = 0;
        }

        private void ly_machinepart_processBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.ly_machinepart_processBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.lYMaterielRequirements);

        }

      

        private void LY_Material_PriceQuery_Load(object sender, EventArgs e)
        {

            ly_machinepart_processTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring; 
            ly_material_price_InfoTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            lY_purchase_contract_ItemTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            ly_assembly_time_bycodeTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            ly_outmachine_contract_detail_sel_checkPriceTableAdapter1.Connection.ConnectionString = SQLDatabase.Connectstring;
            ly_purchase_contract_detailTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;

            this.ly_purchase_contract_detailBindingSource.Filter = "编码='" + itemNum + "'";

 
            this.ly_purchase_contract_detailTableAdapter.Fill(this.genQuey.ly_purchase_contract_detail);
            
            //this.ly_machinepart_processTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            //    this.ly_machinepart_processTableAdapter.Fill(this.lYMaterielRequirements.ly_machinepart_process, itemNum, 0);

       
                this.ly_material_price_InfoTableAdapter.Fill(this.lYTecSet.ly_material_price_Info, itemNum);

       
               this.lY_purchase_contract_ItemTableAdapter.Fill(this.lYTecSet.LY_purchase_contract_Item, itemNum);
               this.ly_assembly_time_bycodeTableAdapter.Fill(this.lYTecSet.ly_assembly_time_bycode,  itemNum);
               this.Text = itemname;




    






            //////////////////////////////////////
            //string selAllString = "SELECT     parentno, itemno, cast(itemno +':'+itemname as char(30))  as itemname, qty_set,itemprice ,itemmoney,itemoutsourceall,itemoutsourcemachineall,assemblymoneyall,itemprocessingmoneyall/2.3 as itemprocessingmoneyall ,isnull(itemmoney,0)+isnull(itemoutsourceall,0)+isnull(itemoutsourcemachineall,0)+isnull(itemprocessingmoneyall,0)/2.3+isnull(assemblymoneyall,0) as allmoney from f_BomExtend_price_novatLab('" + itemNum + "',1) ORDER BY  id_num ";
            string selAllString = "SELECT     parentno, itemno, cast(itemno +':'+itemname as char(30))  as itemname, qty_set,itemprice ,itemmoney,itemoutsourceall,itemoutsourcemachineall,assemblymoneyall, itemprocessingmoneyall ,isnull(itemmoney,0)+isnull(itemoutsourceall,0)+isnull(itemoutsourcemachineall,0)+isnull(itemprocessingmoneyall,0)+isnull(assemblymoneyall,0) as allmoney from f_BomExtend_price_novatLab('" + itemNum + "',1) ORDER BY  id_num ";



            //string selAllString = "SELECT     parentno, itemno, cast(itemno +':'+itemname as char(30))  as itemname,cast(qty_set as decimal(18,3)) as qty_set,itemprice ,itemmoney,itemoutsourceall,itemoutsourcemachineall,assemblymoneyall,itemprocessingmoneyall,isnull(itemmoney,0)+isnull(itemoutsourceall,0)+isnull(itemoutsourcemachineall,0)+isnull(itemprocessingmoneyall,0)+isnull(assemblymoneyall,0) as allmoney from f_BomExtend_price_novatLab('" + itemNum + "',1) ORDER BY  id_num ";


            string cString = SQLDatabase.Connectstring; ;
            SqlDataAdapter bomAdapter = new SqlDataAdapter(selAllString, cString);

            bomAdapter.SelectCommand.CommandTimeout = 0;

            DataSet bomData = new DataSet();
            bomAdapter.Fill(bomData);

            this.treeView1.Nodes.Clear();
            MakeTreeView(bomData.Tables[0], null, null);
            //this.treeView1.ExpandAll();
            this.treeView1.SelectedNode = this.treeView1.Nodes[0];
            this.treeView1.SelectedNode.Expand();

            this.groupBox1.Text = itemNum + "BOM结构图";
        }


        private void MakeTreeView(DataTable table, string ParentID, System.Windows.Forms.TreeNode PNode)
        {


            //DataRow[] dr;

            //if (null == ParentID)
            //    dr = table.Select("parentno is null");
            //else
            //{

            //    string expression;
            //    expression = "parentno='" + ParentID + "'";

            //    dr = table.Select(expression);
            //}
            //try
            //{
            //    if (dr.Length > 0)
            //    {
            //        foreach (DataRow d in dr)
            //        {

            //            System.Windows.Forms.TreeNode TNode = new System.Windows.Forms.TreeNode();//absqty,itemprice ,itemmoney  
            //            //TNode.Text = d["itemname"].ToString() + d["allmoney"].ToString() + "  " + d["qty_set"].ToString() + "  " + d["itemprice"].ToString() + "  (" + d["itemmoney"].ToString() + ")+(" + d["itemprocessingmoney"].ToString() + ")";

            //            TNode.Text = d["itemname"].ToString() + d["allmoney"].ToString() + "     (" + d["qty_set"].ToString() + "  " + d["itemprice"].ToString() + "  (" + d["itemmoney"].ToString() + ")+(" + d["itemprocessingmoney"].ToString() + "))";

            //            TNode.Tag = d["itemno"].ToString();
            //            if (PNode == null)
            //            {
            //                this.treeView1.Nodes.Add(TNode);
            //            }
            //            else
            //            {
            //                PNode.Nodes.Add(TNode);
            //            }

            //            MakeTreeView(table, d["itemno"].ToString(), TNode);
            //        }
            //    }
            //    else
            //    {
            //        return;
            //    }
            //}
            //catch (Exception exp)
            //{
            //    MessageBox.Show(exp.Message);
            //}


            DataRow[] dr;

            if (null == ParentID)
                dr = table.Select("parentno is null");
            else
            {

                string expression;
                expression = "parentno='" + ParentID + "'";

                dr = table.Select(expression);
            }

            try
            {
                if (dr.Length > 0)
                {
                    foreach (DataRow d in dr)
                    {

                        System.Windows.Forms.TreeNode TNode = new System.Windows.Forms.TreeNode();//absqty,itemprice ,itemmoney  
                                                                                                  //TNode.Text = d["itemname"].ToString() + d["allmoney"].ToString() + "  " + d["qty_set"].ToString() + "  " + d["itemprice"].ToString() + "  (" + d["itemmoney"].ToString() + ")+(" + d["itemprocessingmoney"].ToString() + ")";

                        // TNode.Text = d["itemname"].ToString() + d["allmoney"].ToString() + "     (" + d["qty_set"].ToString() + "  " + d["itemprice"].ToString() + "  (" + d["itemmoney"].ToString() + ")+(" + d["itemoutsourceall"].ToString() + ")+(" + d["itemoutsourcemachineall"].ToString() + ")+(" + d["itemprocessingmoneyall"].ToString() + ")+(" + d["assemblymoneyall"].ToString() + "))";
                        TNode.Text = d["itemname"].ToString() + d["qty_set"].ToString() + "     " + d["allmoney"].ToString() + "  (" + d["itemmoney"].ToString() + ")+(" + d["itemoutsourceall"].ToString() + ")+(" + d["itemoutsourcemachineall"].ToString() + ")+(" + d["itemprocessingmoneyall"].ToString() + ")+(" + d["assemblymoneyall"].ToString() + ")";

                        TNode.Tag = d["itemno"].ToString();
                        if (PNode == null)
                        {
                            this.treeView1.Nodes.Add(TNode);
                        }
                        else
                        {
                            PNode.Nodes.Add(TNode);
                        }

                        MakeTreeView(table, d["itemno"].ToString(), TNode);
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

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
     
            string wzbh= e.Node.Tag.ToString();

            this.ly_purchase_contract_detailBindingSource.Filter = "编码='" + wzbh + "'";
            this.ly_machinepart_processTableAdapter.Fill(this.lYMaterielRequirements.ly_machinepart_process, wzbh, 0);

          
            this.ly_material_price_InfoTableAdapter.Fill(this.lYTecSet.ly_material_price_Info, wzbh);

         
            this.lY_purchase_contract_ItemTableAdapter.Fill(this.lYTecSet.LY_purchase_contract_Item, wzbh);
            this.ly_assembly_time_bycodeTableAdapter.Fill(this.lYTecSet.ly_assembly_time_bycode, wzbh);
        }

        private void ly_machinepart_processDataGridView_SelectionChanged(object sender, EventArgs e)
        {
            if (ly_machinepart_processDataGridView.CurrentRow == null)
            {
                ly_outmachine_contract_detail_sel_checkPriceTableAdapter1.Fill(this.lYProductMange.ly_outmachine_contract_detail_sel_checkPrice, "", 0,"");
            }
            else
            {
                string wzbh = ly_machinepart_processDataGridView.CurrentRow.Cells["工件编码2"].Value.ToString();
                string xh = ly_machinepart_processDataGridView.CurrentRow.Cells["顺序2"].Value.ToString();
                string nowsequence = ly_machinepart_processDataGridView.CurrentRow.Cells["工序编号2"].Value.ToString();
                
                ly_outmachine_contract_detail_sel_checkPriceTableAdapter1.Fill(this.lYProductMange.ly_outmachine_contract_detail_sel_checkPrice, wzbh, int.Parse(xh), nowsequence);
            }
        }
        //private void fillToolStripButton_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        this.lY_purchase_contract_ItemTableAdapter.Fill(this.lYTecSet.LY_purchase_contract_Item, itemnoToolStripTextBox.Text);
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
        //        this.ly_material_price_InfoTableAdapter.Fill(this.lYTecSet.ly_material_price_Info, itemnoToolStripTextBox.Text);
        //    }
        //    catch (System.Exception ex)
        //    {
        //        System.Windows.Forms.MessageBox.Show(ex.Message);
        //    }

        //}
    }
}
