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
using System.IO;


namespace HappyYF.YuanXin.WorkSet
{
    public partial class LY_Drawing_Manage : Form

    {
        string nowItemNum = "noSet";

        public LY_Drawing_Manage()
        {
            InitializeComponent();
        }

        private void ly_inma0010cpBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.ly_inma0010cpBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.lYMaterialMange);

        }

        private void LY_Drawing_Manage_Load(object sender, EventArgs e)
        {
            this.ly_inma0010cpTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;

           
            this.ly_inma0010cpTableAdapter.Fill(this.lYMaterialMange.ly_inma0010cp);

        }

        private void toolStripTextBox1_KeyUp(object sender, KeyEventArgs e)
        {
            string dFilter = "";

            dFilter = GetDataGridviewMultiFilterString.DGVMultiFilterString(this.ly_inma0010DataGridView, this.toolStripTextBox1.Text);

            if (null == dFilter)
                dFilter = "";

            this.ly_inma0010cpBindingSource.Filter = dFilter;
        }

        private void toolStripTextBox1_Enter(object sender, EventArgs e)
        {
            toolStripTextBox1.Text = "";

            this.ly_inma0010cpBindingSource.Filter = "";
        }

        private void MakeTreeView(DataTable table, string ParentID, System.Windows.Forms.TreeNode PNode, int nowlevel)
        {


            DataRow[] dr;

            if (null == ParentID)
                dr = table.Select("parentno is null");
            else
            {

                string expression;
                expression = "parentno='" + ParentID + "' and level=" + (nowlevel + 1).ToString();

                dr = table.Select(expression);
            }
            try
            {
                if (dr.Length > 0)
                {
                    foreach (DataRow d in dr)
                    {

                        System.Windows.Forms.TreeNode TNode = new System.Windows.Forms.TreeNode();
                        TNode.Text = d["itemname"].ToString();
                        TNode.Tag = d["itemno"].ToString();



                        if (string.IsNullOrEmpty(d["parentno"].ToString()))
                        {
                            TNode.ToolTipText = "********" + d["absqty"].ToString();
                        }
                        else
                        {
                            TNode.ToolTipText = d["parentno"].ToString() + d["absqty"].ToString();
                        }
                        if (PNode == null)
                        {
                            this.treeView1.Nodes.Add(TNode);
                        }
                        else
                        {
                            PNode.Nodes.Add(TNode);
                        }

                        MakeTreeView(table, d["itemno"].ToString(), TNode, int.Parse(d["level"].ToString()));
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

        private void ly_inma0010DataGridView_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (null == ly_inma0010DataGridView.CurrentRow) return;
            //string s = this.ly_inma0010DataGridView.CurrentRow.Cells["物资编号"].Value.ToString();
            string s = this.ly_inma0010DataGridView.Rows[e.RowIndex].Cells["物资编号"].Value.ToString();

           // this.bomquery_revTableAdapter.Fill(this.lYMaterialMange.bomquery_rev, s, 0, 1);


            string selAllString = "SELECT   level,  parentno, itemno,  itemno +':' +itemname as itemname,absqty  from f_BomExtend('" + s + "',1) ORDER BY  id_num ";
            string cString = SQLDatabase.Connectstring; ;
            SqlDataAdapter bomAdapter = new SqlDataAdapter(selAllString, cString);

            bomAdapter.SelectCommand.CommandTimeout = 0;

            DataSet bomData = new DataSet();
            bomAdapter.Fill(bomData);

            this.treeView1.Nodes.Clear();
            MakeTreeView(bomData.Tables[0], null, null, 0);
            //this.treeView1.ExpandAll();
            this.treeView1.SelectedNode = this.treeView1.Nodes[0];
            this.treeView1.SelectedNode.Expand();

            this.groupBox1.Text = s + "BOM结构图";
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            nowItemNum = e.Node.Tag.ToString();


            this.groupBox2.Text = nowItemNum;
        }



        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if ( "noSet"==nowItemNum)
            {
                return;
            }

            string sourcename="noSet";
            string targetName = "\\\\192.168.1.237\\Drawing\\"+nowItemNum+".pdf";

            //aaaaaaa
            OpenFileDialog openFile = new OpenFileDialog();

            openFile.Filter = "PDF文件|*.pdf";
            //openFile.ShowDialog();


            if (openFile.ShowDialog() == DialogResult.OK)
            {
                sourcename = openFile.FileName;
            }
            if ("noSet" == sourcename)
            {
                return;
            }

            if (Netfunction.Ping("192.168.1.237"))
            {
                if (Netfunction.Connect("192.168.1.237\\Drawing", "administrator", "jmfuq001."))
                {
                    File.Copy(sourcename, targetName, true);

                   Netfunction.DisConnect("192.168.1.237\\Drawing", "administrator", "jmfuq001.");
                }
                else
                {
                    MessageBox.Show("连接主机失败！", "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
            }
            else
            {
                MessageBox.Show("Ping主机失败！", "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Information);


            }
        }

        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            if ("noSet" == nowItemNum)
            {
                return;
            }

           
            string sourcename = "\\\\192.168.1.237\\Drawing\\" + nowItemNum + ".pdf";
            string targetName =  nowItemNum + ".pdf";

            ////////////////////////////////////////

            if (Netfunction.Ping("192.168.1.237"))
            {

                Netfunction.DisConnect("192.168.1.237\\Drawing", "administrator", "jmfuq001.");
                if (Netfunction.Connect("192.168.1.237\\Drawing", "administrator", "jmfuq001."))
                {
                    File.Copy(sourcename, targetName, true);

                    System.Diagnostics.Process.Start(targetName);
                    //System.Diagnostics.Process.Start("F:\\2.pdf");

                   // axAcroPDF1.src = sourcename;

                    Netfunction.DisConnect("192.168.1.237\\Drawing", "administrator", "jmfuq001.");
                }
                else
                {
                    MessageBox.Show("连接主机失败！", "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
            }
            else
            {
                MessageBox.Show("Ping主机失败！", "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Information);


            }
            //axAcroPDF1.src =System.Environment.CurrentDirectory+"\\"+ targetName;
        }
    }
}
