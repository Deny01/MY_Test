using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using TGZJ.Base;


namespace TGZJ.Manger
{
    public partial class Bumen : BaseForm
    {
        bool IsAdd = false ;
        
        public Bumen()
        {
            InitializeComponent();
            this.splitContainer1.SplitterDistance = 25;
        }

        private void Bumen_Load(object sender, EventArgs e)
        {

            this.t_bumenTableAdapter.Connection.ConnectionString = Program.dataBase .MakeConnectString ();
            this.t_bumenTableAdapter.Fill(this.bumenDataSet.T_bumen);
            //MakeTreeView();

            MakeTreeView(this.bumenDataSet.T_bumen, null, null);
            this.treeView1.ExpandAll();

            this.treeView1.SelectedNode = this.treeView1.Nodes[0];

            SetViewState("View");
        }

        private void SetViewState(string state)
        { 
            if ("View"==state )
            {
                this.bumenMCTextBox.ReadOnly = true;

                this.bindingNavigatorAddNewItem.Enabled = true;
                this.bindingNavigatorDeleteItem.Enabled = true;
                this.toolStripButton1.Enabled = true;
                this.t_bumenBindingNavigatorSaveItem.Enabled = false;
                
                this.bindingNavigatorMoveFirstItem.Enabled = true;
                this.bindingNavigatorMoveLastItem.Enabled = true;
                this.bindingNavigatorMovePreviousItem.Enabled = true;
                this.bindingNavigatorMoveNextItem.Enabled = true;
                this.bindingNavigatorPositionItem.Enabled = true;
                this.treeView1.Focus();


            }
            else
            {
                this.bumenMCTextBox.ReadOnly = false ;

                this.bindingNavigatorAddNewItem.Enabled = false;
                this.bindingNavigatorDeleteItem.Enabled = false;
                this.toolStripButton1.Enabled = false;
                this.t_bumenBindingNavigatorSaveItem.Enabled = true ;

                this.bindingNavigatorMoveFirstItem.Enabled = false;
                this.bindingNavigatorMoveLastItem.Enabled = false;
                this.bindingNavigatorMovePreviousItem.Enabled = false;
                this.bindingNavigatorMoveNextItem.Enabled = false;
                this.bindingNavigatorPositionItem.Enabled = false;
            
            }
        
        }

        //private void MakeTreeView()
        //{


        //    //string cString = Program.dataBase .MakeConnectString();
        //    //string selAllString = "SELECT     parentID, bumenID, bumenBM, bumenMC FROM  T_bumen ORDER BY parentID, bumenID ";


        //    SqlDataAdapter bumenAdapter = new SqlDataAdapter(selAllString, cString);

        //    DataSet bumenData = new DataSet();
        //    bumenAdapter.Fill(bumenData);

        //    MyTreeView(bumenData.Tables[0],null,null );
        //}

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
                  expression = "ParentID='" + ParentID +"'";

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
                            TNode.Tag  = d["bumenID"].ToString(); 
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

        private void 新增部门ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int treeNode = this.treeView1.Nodes.Count;

            BumenMingxi bumenMingxi = new BumenMingxi();

            if (0 == treeNode)
            {
              


                //queryForm.Sel = sel;
                //queryForm.Constr = Program.yyconnectstring;

                //Set the Column Collection to the filter Table
                //queryForm.SetSourceColumns(this.billMainDataSet.BalanceBill.Columns);

               
            
            }
            else
            { }

            //bumenMingxi.ShowDialog();

        }

       

        private void t_bumenBindingNavigatorSaveItem_Click_1(object sender, EventArgs e)
        {
            if (IsAdd )  AddNode();

            this.Validate();
            this.t_bumenBindingSource.EndEdit();

            this.tableAdapterManager.UpdateAll(this.bumenDataSet);

            IsAdd = false;

            string bumenID = this.bumenIDTextBox.Text;


            this.t_bumenBindingSource.Sort = "bumenID ASC";

            this.t_bumenBindingSource.Position =this.t_bumenBindingSource.Find("bumenID", bumenID);


            this.treeView1.SelectedNode=FindNowNode(bumenID);
            this.treeView1.SelectedNode.Text = this.bumenMCTextBox.Text; 

            SetViewState("View");
            

        }

        private void AddNode()
        {
            int treeNode = this.treeView1.Nodes.Count;

            if (0 == treeNode)
            {

                this.bumenIDTextBox.Text = "00";

                System.Windows.Forms.TreeNode TNode0 = new System.Windows.Forms.TreeNode();
                TNode0.Text = this.bumenMCTextBox.Text;
                TNode0.Tag = this.bumenIDTextBox.Text;

                this.treeView1.Nodes.Add(TNode0);

            }
            else
            {
                System.Windows.Forms.TreeNode nowNode = this.treeView1.SelectedNode;

                string nowParentId = nowNode.Tag.ToString();

                DataRowView dr0 = (DataRowView)this.t_bumenBindingSource.Current;

                dr0["parentID"] = nowParentId;
                this.bumenIDTextBox.Text = MakeBumenID(nowParentId);

                System.Windows.Forms.TreeNode TNode = new System.Windows.Forms.TreeNode();
                TNode.Text = this.bumenMCTextBox.Text;
                TNode.Tag = this.bumenIDTextBox.Text;

                nowNode.Nodes.Add(TNode);





            }
        }

        private void bindingNavigatorAddNewItem_Click(object sender, EventArgs e)
        {
            IsAdd = true;
            this.t_bumenBindingSource.AddNew();
            SetViewState("Edit");

           

            
        }

        private string MakeBumenID(string nowParentId)
        {

            string expression;
            expression = "ParentID='" + nowParentId + "'";

            DataRow[] dr = this.bumenDataSet.T_bumen.Select(expression,"bumenID DESC");

            if (0 == dr.Length)
                return nowParentId + "01";
            else
            {
                string temp = dr[0]["bumenID"].ToString();
                temp = temp.Substring(temp.Length - 2, 2);


                return nowParentId + (int.Parse(temp) + 1).ToString ("00");
            }
        
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            SetViewState("Edit");
            
            IsAdd = false;
        }

        private void bindingNavigatorDeleteItem_Click(object sender, EventArgs e)
        {
            IsAdd = false;

             string message = "确定删除该部门及其子项？";
            string caption = "提示...";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result;



            result = MessageBox.Show(message, caption, buttons,
            MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

            if (result == DialogResult.Yes)
            {
                IsAdd = true;
                
                DataRow[] dr;


                dr = this.bumenDataSet.T_bumen.Select("bumenID like '" +this .treeView1 .SelectedNode .Tag .ToString () +"%'" );

                foreach (DataRow d in dr)
                {
                    this.t_bumenBindingSource.RemoveAt(this.t_bumenBindingSource.Find("bumenID", d["bumenID"]));
                }

                this.treeView1.SelectedNode.Remove();

                this.tableAdapterManager.UpdateAll(this.bumenDataSet);

                IsAdd = false ;
                this.treeView1.Nodes.Clear();
                MakeTreeView(this.bumenDataSet.T_bumen, null, null);
                this.treeView1.ExpandAll();

                this.treeView1.SelectedNode = this.treeView1.Nodes[0];
            }
        }

        private void t_bumenBindingSource_CurrentChanged(object sender, EventArgs e)
        {

            if (!IsAdd)
            {

                DataRowView dr0 = (DataRowView)this.t_bumenBindingSource.Current;

                string nowBumenID = dr0["bumenID"].ToString();

                this.treeView1.SelectedNode = FindNowNode(nowBumenID);
            }

        }

        public TreeNode FindNowNode(string nowBumenID)
        {

            TreeNode node =null ;


            foreach (TreeNode tn in this.treeView1.Nodes)
            {
                node = FindNode(tn, nowBumenID);
                if (node != null) break;
            }


            return node;
        
        
        }

        private TreeNode FindNode(TreeNode tnParent, string strValue)
        {

            if (tnParent == null) return null;

            if (tnParent.Tag.ToString () == strValue) return tnParent;



            TreeNode tnRet = null;

            foreach (TreeNode tn in tnParent.Nodes)
            {

                tnRet = FindNode(tn, strValue);

                if (tnRet != null) break;

            }

            return tnRet;

        }

        private void treeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            

            //this.t_bumenBindingSource.Position = this.t_bumenBindingSource.Find("bumenID",e.Node.Tag .ToString ());

        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            this.t_bumenBindingSource.Position = this.t_bumenBindingSource.Find("bumenID", e.Node.Tag.ToString());

        }


 
    }
}
