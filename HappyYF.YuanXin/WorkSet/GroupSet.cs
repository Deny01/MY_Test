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

namespace HappyYF.YuanXin.WorkSet
{
    public partial class GroupSet : Form
    {
        public GroupSet()
        {
            InitializeComponent();
        }


        private void GroupSet_Load(object sender, EventArgs e)
        {
            this.yX_group_SETTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            this.itemofserviceTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;

            tableAdapterManager.Connection = this.itemofserviceTableAdapter.Connection;

            Repaint_Data();

           

        }

        private void Repaint_Data()
        {
            

            this.treeView1.Nodes.Clear();
            this.treeView2.Nodes.Clear();
            
            this.yX_group_SETTableAdapter.Fill(this.groupDataSet.YX_group_SET);
            this.itemofserviceTableAdapter.Fill(this.groupDataSet.Itemofservice);

            

            MakeTreeView(this.groupDataSet.YX_group_SET, "-1", null);

            
            MakeTreeView2(this.groupDataSet.Itemofservice);

            this.treeView1.ExpandAll();
        }

        private void MakeTreeView2(DataTable table)
        {
            this.treeView2.BeginUpdate();
            
            System.Windows.Forms.TreeNode TNode; 

            foreach (DataRow dr in table.Rows)
            {
                TNode = new System.Windows.Forms.TreeNode();

                TNode.Text = "[" + dr["ItemofserviceNumber"].ToString() + "]" + dr["ItemofserviceName"].ToString() + ":(" + dr["Unit_price"].ToString()+")" + dr["Remarks"].ToString();
                TNode.Tag = dr["ItemofserviceNumber"].ToString();
                TNode.ImageIndex = 1;
                TNode.SelectedImageIndex = 1;
                       
                            this.treeView2.Nodes.Add(TNode);
            
            
            }
            this.treeView2.EndUpdate();
        }

        private void MakeTreeView(DataTable table, string   ParentID, System.Windows.Forms.TreeNode PNode)
        {

            List <string > nowList ;
            DataRow[] dr;

            this.treeView1.BeginUpdate();

            if ( "-1" == ParentID)
                dr = table.Select("parentId = '-1' ");
            else
            {
                //if (ParentID == "0001010102")
                //{
                //    int aaa = 0;


                //}
                string expression;
                expression = "parentID='" + ParentID +"'";

                dr = table.Select(expression);
            }

            try
            {
                if (dr.Length > 0)
                {
                    foreach (DataRow d in dr)
                    {

                        nowList = new List<string>();
                        nowList.Add(d["parentId"].ToString());
                        nowList.Add(d["nowID"].ToString());

                        System.Windows.Forms.TreeNode TNode = new System.Windows.Forms.TreeNode();
                        TNode.Text = d["nowName"].ToString();
                        TNode.Tag = nowList;
                        if ("-1" == nowList[0])
                        {
                            TNode.ImageIndex = 0;
                            TNode.SelectedImageIndex = 0;
                        }
                        else
                        {

                            TNode.ImageIndex = 1;
                            TNode.SelectedImageIndex = 1;

                        }
                        if (PNode == null)
                        {
                            this.treeView1.Nodes.Add(TNode);
                        }
                        else
                        {
                            PNode.Nodes.Add(TNode);
                        }

                        MakeTreeView(table, d["nowID"].ToString(), TNode);
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
            finally
            {
                this.treeView1.EndUpdate();
            
            }
        }

        private void bindingNavigatorAddNewItem_Click(object sender, EventArgs e)
        {
            if (toolStripTextBox1.Text.Replace(" ", "").Length < 1)
            {
                MessageBox.Show("请输入大类名称...");
                this.toolStripTextBox1.Focus();
                return;
            }

            //////////////////////

              DataRow[] dr;


              dr = this.groupDataSet.YX_group_SET.Select("nowName='" + toolStripTextBox1.Text.Replace(" ", "") + "'");

                if (dr.Length > 0)
                {

                    MessageBox.Show("大类名称重复...");
                    this.toolStripTextBox1.Focus();
                    return;
                
                }


            //////////////////////

            string insdetail = " INSERT INTO YX_group_main " +
             " (GroupName) " +
             "VALUES ( '" + toolStripTextBox1.Text.Replace(" ", "") + "')";


            SqlConnection sqlConnection1 = new SqlConnection(SQLDatabase.Connectstring);
            SqlCommand cmd = new SqlCommand();

            cmd.CommandText = insdetail;
            cmd.CommandType = CommandType.Text;
            cmd.Connection = sqlConnection1;

            using (TransactionScope scope = new TransactionScope())
            {
                sqlConnection1.Open();
                cmd.ExecuteNonQuery();
                sqlConnection1.Close();

                scope.Complete();
            }

            Repaint_Data();


        }

        private void bindingNavigatorDeleteItem_Click(object sender, EventArgs e)
        {

            if ( null == this.treeView1.SelectedNode) return;
            
            List<string> nowList = this.treeView1.SelectedNode.Tag as List<string>;
            
            
            if ("-1" != nowList [0])
            {

                string message = "确定删除当前项目吗？";
                string caption = "提示...";
                MessageBoxButtons buttons = MessageBoxButtons.YesNo;
                DialogResult result;



                result = MessageBox.Show(message, caption, buttons,
                MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

                if (result == DialogResult.Yes)
                {
                    string deldetail = " delete  YX_group_detail   " +
                 " where GroupName = '" + nowList[1] + "' or ItemofserviceNumber= '" + nowList[1] + "'";



                    SqlConnection sqlConnection1 = new SqlConnection(SQLDatabase.Connectstring);
                    SqlCommand cmd = new SqlCommand();

                    cmd.CommandText = deldetail;
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = sqlConnection1;

                    using (TransactionScope scope = new TransactionScope())
                    {
                        sqlConnection1.Open();
                        
                        cmd.ExecuteNonQuery();
                        sqlConnection1.Close();

                        scope.Complete();
                    }
                }

            }
            else
            {

                string message = "确定删除当前大类及其包含的项目吗？";
                string caption = "提示...";
                MessageBoxButtons buttons = MessageBoxButtons.YesNo;
                DialogResult result;



                result = MessageBox.Show(message, caption, buttons,
                MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

                if (result == DialogResult.Yes)
                {
                    
                    
                    string delmain = " delete  YX_group_main " +
                     " where GroupName = '" + nowList[1]+"'";

                    string deldetail = " delete  YX_group_detail   " +
                   " where GroupName = '" + nowList[1] + "' or ItemofserviceNumber= '" + nowList[1]+"'";



                    SqlConnection sqlConnection1 = new SqlConnection(SQLDatabase.Connectstring);
                    SqlCommand cmd = new SqlCommand();

                    cmd.CommandText = delmain;
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = sqlConnection1;

                    using (TransactionScope scope = new TransactionScope())
                    {
                        sqlConnection1.Open();
                        cmd.ExecuteNonQuery();
                        cmd.CommandText = deldetail;
                        cmd.ExecuteNonQuery();
                        sqlConnection1.Close();

                        scope.Complete();
                    }
                }
            }

            Repaint_Data();
        }

        private void itemofserviceDataGridView_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right  ) return;
            Cursor.Current = Cursors.Hand;
           
        }

        private void itemofserviceDataGridView_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left )
            {
                
                Cursor.Current = Cursors.Hand ;

            }
        }

        private void treeView2_ItemDrag(object sender, ItemDragEventArgs e)
        {
            string strItem = ((TreeNode)e.Item).Tag .ToString ();

            //DoDragDrop(strItem, DragDropEffects.Copy | DragDropEffects.Move);Move

            DoDragDrop(strItem, DragDropEffects.All );

        }

        private void treeView1_DragDrop(object sender, DragEventArgs e)
        {
          string dummy = "temp" ;
                string s = ( string ) e.Data.GetData ( dummy.GetType ( ) ) ;
                //s = s.Substring ( s.IndexOf ( ":" ) + 1 ).Trim ( ) ;
                
                Point pt = new Point();
                pt.X = e.X;
                pt.Y = e.Y;
                pt  = treeView1.PointToClient ( pt ) ;
                TreeNode DropNode = this.treeView1.GetNodeAt ( pt ) ;

                string parentid;
                List<string> ls = DropNode.Tag as List<string>;

                if (null == DropNode) return;

                if ("-1" != ls[0]) parentid = ls[0];
                else parentid = DropNode.Text;

                string insdetail = " INSERT INTO YX_group_detail " +
                " (GroupName,ItemofserviceNumber) " +
                "VALUES ( '" + parentid + "','" + s + "')";


                SqlConnection sqlConnection1 = new SqlConnection(SQLDatabase.Connectstring);
                SqlCommand cmd = new SqlCommand();

                cmd.CommandText = insdetail;
                cmd.CommandType = CommandType.Text;
                cmd.Connection = sqlConnection1;

                using (TransactionScope scope = new TransactionScope())
                {
                    sqlConnection1.Open();
                    cmd.ExecuteNonQuery();
                    sqlConnection1.Close();

                    scope.Complete();
                }

                Repaint_Data();

               

          
        }

        private void treeView1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.Text))
                e.Effect = DragDropEffects.Move;
            else
                e.Effect = DragDropEffects.None;

        }

        private void treeView2_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {

                Cursor.Current = Cursors.Hand;
            }
        }

        private void treeView2_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {

                Cursor.Current = Cursors.Hand;
            }
        }
    }
}
