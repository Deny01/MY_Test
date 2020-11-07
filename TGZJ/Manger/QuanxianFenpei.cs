using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient ;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TGZJ.Base;

namespace TGZJ.Manger
{
    public partial class QuanxianFenpei : BaseForm
    {
        private MenuStrip mainMenu;
        bool isclearAll = true ;
       
        public MenuStrip MainMenu
        {
            get { return mainMenu; }
            set { mainMenu = value; }
        }
        
        public QuanxianFenpei()
        {
            InitializeComponent();
        }

        private void QuanxianFenpei_Load(object sender, EventArgs e)
        {

            this.t_gongnengshouquanTableAdapter.Connection.ConnectionString = Program.dataBase.MakeConnectString();
            //this.t_gongnengshouquanTableAdapter.Fill(this.shouquanDataSet.T_gongnengshouquan,"");
            string cString = Program.dataBase.MakeConnectString();
            


            string selAllString = "SELECT     parentID, bumenID, bumenBM, bumenMC FROM  T_bumen ORDER BY  bumenID ";


            SqlDataAdapter bumenAdapter = new SqlDataAdapter(selAllString, cString);

            DataSet bumenData = new DataSet();
            bumenAdapter.Fill(bumenData);

            MakeTreeView(bumenData.Tables[0], null, null);
            FillYonghu(treeView1);
            
            this.treeView1.ExpandAll();
            
            MakeMenuTreeView(this.treeView2, mainMenu.Items, null);

            this.treeView2.ExpandAll();

            //this .treeView2 .

        }

        private void FillYonghu(TreeView tv)
        {
            string cString = Program.dataBase.MakeConnectString();



            string selAllString = "SELECT     yhbm, yhmc, bumen  from  T_users ORDER BY  yhbm ";


            SqlDataAdapter nowAdapter = new SqlDataAdapter(selAllString, cString);

            DataSet nowData = new DataSet();
            nowAdapter.Fill(nowData);

            DataRow[] dr;
            dr = nowData.Tables[0].Select();

            foreach (DataRow d in dr)
            {

                TreeNode TNode = new TreeNode();
                TNode.Text = d["yhmc"].ToString();
                TNode.Tag = d["yhbm"].ToString();
                TNode.ImageIndex = 1;
                TNode.SelectedImageIndex = 1;

                TreeNode PNode = FindNowNode(  treeView1 , d["bumen"].ToString());
                PNode.Nodes.Add(TNode);
                

              
            }
        
        }


        public TreeNode FindNowNode(TreeView  tv,string nowBumenID)
        {

            TreeNode node = null;


            foreach (TreeNode tn in tv.Nodes)
            {
                node = FindNode(tn, nowBumenID);
                if (node != null) break;
            }


            return node;


        }

        private TreeNode FindNode(TreeNode tnParent, string strValue)
        {

            if (tnParent == null) return null;

            if (tnParent.Tag.ToString() == strValue) return tnParent;



            TreeNode tnRet = null;

            foreach (TreeNode tn in tnParent.Nodes)
            {

                tnRet = FindNode(tn, strValue);

                if (tnRet != null) break;

            }

            return tnRet;

        }

        //private void MakeMenuTreeView(TreeView tv,MenuStrip ms )
        private void MakeMenuTreeView(TreeView tv, ToolStripItemCollection ms, TreeNode PNode)
        {

            if (ms.Count == 0)
                return;
            
            foreach ( ToolStripItem tm in ms )
           {

               if (tm is ToolStripMenuItem)
               {
                   TreeNode TNode = new TreeNode();
                   TNode.Text = tm.Text;
                   TNode.Tag  = tm.Text;

                   if (null == PNode)
                       tv.Nodes.Add(TNode);
                   else
                       PNode.Nodes.Add(TNode);

                   MakeMenuTreeView(tv, ((ToolStripMenuItem)tm).DropDown.Items, TNode);
               }
           
           
           }
        }

        //private void MakeFullMenuTreeView(TreeView tv, ToolStripco ms)
        //{
        //    foreach (ToolStripMenuItem tm in ms)
        //    {
        //        TreeNode TNode = new TreeNode();
        //        TNode.Text = tm.Text;
        //        tv.Nodes.Add(TNode);


        //    }
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

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            isclearAll = true;
            ClearTreeViewCheck(treeView2);
            isclearAll = false;
            
            if (e.Node.SelectedImageIndex == 1)
            {
                SetYonghuCheck(treeView2, e.Node.Tag.ToString());
                
            
            }
        }

        private void SetYonghuCheck( TreeView tv, string yh)
        {
           
            
            this.t_gongnengshouquanTableAdapter.Fill(this.shouquanDataSet.T_gongnengshouquan, yh );

            DataRow[] dr = this.shouquanDataSet .T_gongnengshouquan.Select();

           

            if (0 == dr.Length)
                return ;
            else
            {
                TreeNode tr;
               foreach ( DataRow d in dr)
               {
                   tr = FindNowNode(treeView2, d["control_name"].ToString());

                   if (null != tr)
                   { 
                       tr.Checked = true;
                       if (1 == tr.Level)
                       {
                           tr.ForeColor = Color.White;
                           tr.BackColor = Color.Teal;
                       }
                   }

               
               }
            }

        
        }

        private void ClearTreeViewCheck(TreeView tv)
        {
            foreach (TreeNode tn in tv.Nodes)
            {
                tn.Checked = false;
                tn.ForeColor = Color.Gray;
                tn.BackColor = Color.White;
                
                ClearTreeNodeCheck(tn);
               
            }
        
        }

        private void ClearTreeNodeCheck(TreeNode td)
        {
            foreach (TreeNode tn in td.Nodes)
            {
                tn.Checked = false;
                tn.ForeColor = Color.Gray;
                tn.BackColor = Color.White;
                ClearTreeNodeCheck(tn);
            }
        
        }


        private void t_gongnengshouquanBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.t_gongnengshouquanBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.shouquanDataSet);

        }

        private void treeView2_AfterCheck(object sender, TreeViewEventArgs e)
        {
            if (isclearAll) return;

            if (treeView1.SelectedNode.SelectedImageIndex == 1)
            {

                DataRow[] dr = this.shouquanDataSet.T_gongnengshouquan.Select("control_name ='" + e.Node.Tag.ToString() + "'");


                if (e.Node.Checked == true)
                {


                    SetParentNode(e.Node);

                    if (dr.Length == 0)
                    {
                        if (treeView1.SelectedNode.SelectedImageIndex == 1)
                        {
                            DataRow newRow = this.shouquanDataSet.T_gongnengshouquan.NewRow();
                            newRow["control_name"] = e.Node.Tag.ToString();
                            newRow["yonghu_ID"] = treeView1.SelectedNode.Tag.ToString();
                            this.shouquanDataSet.T_gongnengshouquan.Rows.Add(newRow);
                        }
                    }
                }
                else
                {
                    ClearChildNode(e.Node);
                    if (dr.Length > 0)
                    {
                        //DataRow tempdr;
                        int pos;
                        foreach (DataRow d in dr)
                        {
                            //tempdr = this.shouquanDataSet.T_gongnengshouquan.Rows.Find(d["id"]);
                            //this.shouquanDataSet.T_gongnengshouquan.Rows.Remove(tempdr);
                            pos = this.t_gongnengshouquanBindingSource.Find("id", d["id"]);
                            this.t_gongnengshouquanBindingSource.RemoveAt(pos);
                        }
                    }
                }

                treeView2.SelectedNode = e.Node;
                this.t_gongnengshouquanBindingSource.EndEdit();
                this.tableAdapterManager.UpdateAll(this.shouquanDataSet);

            }
        }

        private void SetParentNode(TreeNode td)
        {
            if (null != td.Parent)
            {
                td.Parent.Checked = true;
                SetParentNode(td.Parent);

            }
            else
            {
                return;
            
            }
        }

        private void ClearChildNode(TreeNode td)
        {
            if (td.Nodes.Count > 0)
                foreach (TreeNode t in td.Nodes)
                {
                    t.Checked = false;
                    ClearChildNode(t);

                }
            else
            {
                return;
            }
        
        }
    }
}
