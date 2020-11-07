﻿using System;
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
    public partial class LY_Material_Costdetail : Form
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

        public LY_Material_Costdetail()
        {
            InitializeComponent();
        }

        private void ly_machinepart_processBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            //this.ly_machinepart_processBindingSource.EndEdit();
            //this.tableAdapterManager.UpdateAll(this.lYMaterielRequirements);

        }

      

        private void LY_Material_PriceQuery_Load(object sender, EventArgs e)
        {
            

           
                this.f_BomExtend_priceTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;

                NewFrm.Show(this);
                this.f_BomExtend_priceTableAdapter.Fill(this.lYTecSet.f_BomExtend_price, itemNum);
                NewFrm.Hide(this);


                //this.ly_machinepart_processTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
                //this.ly_machinepart_processTableAdapter.Fill(this.lYMaterielRequirements.ly_machinepart_process, itemNum, 0);

                //this.ly_material_price_InfoTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
                //this.ly_material_price_InfoTableAdapter.Fill(this.lYTecSet.ly_material_price_Info, itemNum);

                this.Text = itemname;

            ////////////////////////////////////////

            //    string selAllString = "SELECT     parentno, itemno, cast(itemno +':'+itemname as char(30))  as itemname,cast(qty_set as decimal(18,3)) as qty_set,itemprice ,itemmoney,itemprocessingmoney,isnull(itemmoney,0)+isnull(itemprocessingmoney,0) as allmoney from f_BomExtend_price('" + itemNum + "',1) ORDER BY  id_num ";
            //    string cString = SQLDatabase.Connectstring; ;
            //    SqlDataAdapter bomAdapter = new SqlDataAdapter(selAllString, cString);

            //    bomAdapter.SelectCommand.CommandTimeout = 0;

            //    DataSet bomData = new DataSet();
            //    bomAdapter.Fill(bomData);

            //    this.treeView1.Nodes.Clear();
            //    MakeTreeView(bomData.Tables[0], null, null);
            //    //this.treeView1.ExpandAll();
            //    this.treeView1.SelectedNode = this.treeView1.Nodes[0];
            //    this.treeView1.SelectedNode.Expand();

            //    this.groupBox1.Text = itemNum + Itemname + ":BOM结构图";
        }

        private void toolStripButton16_Click(object sender, EventArgs e)
        {
            ExportDataGridviewTOExcellA.ExportDataGridview(this.f_BomExtend_priceDataGridView, true);
        }


        //private void MakeTreeView(DataTable table, string ParentID, System.Windows.Forms.TreeNode PNode)
        //{


        //    DataRow[] dr;

        //    if (null == ParentID)
        //        dr = table.Select("parentno is null");
        //    else
        //    {

        //        string expression;
        //        expression = "parentno='" + ParentID + "'";

        //        dr = table.Select(expression);
        //    }
        //    try
        //    {
        //        if (dr.Length > 0)
        //        {
        //            foreach (DataRow d in dr)
        //            {

        //                System.Windows.Forms.TreeNode TNode = new System.Windows.Forms.TreeNode();//absqty,itemprice ,itemmoney  
        //                //TNode.Text = d["itemname"].ToString() + d["allmoney"].ToString() + "  " + d["qty_set"].ToString() + "  " + d["itemprice"].ToString() + "  (" + d["itemmoney"].ToString() + ")+(" + d["itemprocessingmoney"].ToString() + ")";

        //                TNode.Text = d["itemname"].ToString() + d["allmoney"].ToString() + "     (" + d["qty_set"].ToString() + "  " + d["itemprice"].ToString() + "  (" + d["itemmoney"].ToString() + ")+(" + d["itemprocessingmoney"].ToString() + "))";

        //                TNode.Tag = d["itemno"].ToString();
        //                if (PNode == null)
        //                {
        //                    this.treeView1.Nodes.Add(TNode);
        //                }
        //                else
        //                {
        //                    PNode.Nodes.Add(TNode);
        //                }

        //                MakeTreeView(table, d["itemno"].ToString(), TNode);
        //            }
        //        }
        //        else
        //        {
        //            return;
        //        }
        //    }
        //    catch (Exception exp)
        //    {
        //        MessageBox.Show(exp.Message);
        //    }
        //}

        //private void fillToolStripButton_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        this.f_BomExtend_priceTableAdapter.Fill(this.lYTecSet.f_BomExtend_price, itemnoToolStripTextBox.Text);
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
