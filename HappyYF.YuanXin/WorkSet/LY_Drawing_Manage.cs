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

            this.ly_inma0010_drawingTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            this.ly_inma0010cpTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            // this.ly_inma0010cpBindingSource.Filter = "图纸级别<=" + SQLDatabase.nowUserDrawinglevel();

            this.ly_inma0010cpTableAdapter.Fill(this.lYMaterialMange.ly_inma0010cp);
            this.ly_inma0010_drawing_electricalTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            this.ly_inma0010_drawing_pcbTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            this.ly_inma0010_drawing_processTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            this.ly_inma0010_drawing_InspectionTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            this.ly_inma0010_drawing_exeTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            this.ly_inma0010_drawing_codeTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            this.ly_inma0010_drawing_bookTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            this.ly_inma0010_drawing_mechanicalTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            this.ly_inma0010_drawing_bomTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;

            //判断总权限

            string sql = "select * from  ly_employe_pic where yonghu_code='" + SQLDatabase.NowUserID + "'";
            DataTable dt = null;
            using (SqlConnection connection = new SqlConnection(SQLDatabase.Connectstring))
            {

                SqlDataAdapter adapter = new SqlDataAdapter(sql, connection);
                DataSet ds = new DataSet();
                adapter.Fill(ds);
                dt = ds.Tables[0];
            }
            foreach (TabPage t in tabControl1.TabPages)
            {
                int m = 0;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (t.Text == dt.Rows[i]["pictype_name"].ToString())
                    {
                        m++;
                    }
                }
                if (m == 0)
                {
                    t.Parent = null;
                }
            }
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
            string s = this.ly_inma0010DataGridView.Rows[e.RowIndex].Cells["物资编号"].Value.ToString();

            string selAllString = "SELECT   a.level,  a.parentno, a.itemno,  a.itemno +':' + isnull(a.itemname,'')+' '+isnull(b.xhc,'')+' '+isnull(b.xhj,'')+' '+isnull(b.gg,'')+'  ' + isnull(b.work_people,'') as itemname,absqty  from f_BomExtend('" + s + "',1) a left join ly_inma0010 b on a.itemno=b.wzbh ORDER BY  id_num ";
            string cString = SQLDatabase.Connectstring; ;
            SqlDataAdapter bomAdapter = new SqlDataAdapter(selAllString, cString);

            bomAdapter.SelectCommand.CommandTimeout = 0;

            DataSet bomData = new DataSet();
            bomAdapter.Fill(bomData);

            this.treeView1.Nodes.Clear();
            MakeTreeView(bomData.Tables[0], null, null, 0);
            this.treeView1.SelectedNode = this.treeView1.Nodes[0];
            this.treeView1.SelectedNode.Expand();

            this.groupBox1.Text = s + "BOM结构图";

        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            nowItemNum = e.Node.Tag.ToString();

            this.ly_inma0010_drawingTableAdapter.Fill(this.lYMaterialMange.ly_inma0010_drawing, nowItemNum);


            this.lyinma0010drawingelectricalBindingSource.Filter = "level<=" + SQLDatabase.nowUserDrawinglevel();
            this.ly_inma0010_drawing_electricalTableAdapter.Fill(this.lYMaterialMange.ly_inma0010_drawing_electrical, nowItemNum);


            this.lyinma0010drawingpcbBindingSource.Filter = "level<=" + SQLDatabase.nowUserDrawinglevel();
            this.ly_inma0010_drawing_pcbTableAdapter.Fill(this.lYMaterialMange.ly_inma0010_drawing_pcb, nowItemNum);

            this.lyinma0010drawingprocessBindingSource.Filter = "level<=" + SQLDatabase.nowUserDrawinglevel();
            this.ly_inma0010_drawing_processTableAdapter.Fill(this.lYMaterialMange.ly_inma0010_drawing_process, nowItemNum);

            this.lyinma0010drawingInspectionBindingSource.Filter = "level<=" + SQLDatabase.nowUserDrawinglevel();
            this.ly_inma0010_drawing_InspectionTableAdapter.Fill(this.lYMaterialMange.ly_inma0010_drawing_Inspection, nowItemNum);

            this.lyinma0010drawingexeBindingSource.Filter = "level<=" + SQLDatabase.nowUserDrawinglevel();
            this.ly_inma0010_drawing_exeTableAdapter.Fill(this.lYMaterialMange.ly_inma0010_drawing_exe, nowItemNum);

            this.lyinma0010drawingcodeBindingSource.Filter = "level<=" + SQLDatabase.nowUserDrawinglevel();
            this.ly_inma0010_drawing_codeTableAdapter.Fill(this.lYMaterialMange.ly_inma0010_drawing_code, nowItemNum);


            this.lyinma0010drawingbookBindingSource.Filter = "level<=" + SQLDatabase.nowUserDrawinglevel();
            this.ly_inma0010_drawing_bookTableAdapter.Fill(this.lYMaterialMange.ly_inma0010_drawing_book, nowItemNum);

            this.lyinma0010drawingmechanicalBindingSource.Filter = "level<=" + SQLDatabase.nowUserDrawinglevel();
            this.ly_inma0010_drawing_mechanicalTableAdapter.Fill(this.lYMaterialMange.ly_inma0010_drawing_mechanical, nowItemNum);

            this.lyinma0010drawingbomBindingSource.Filter = "level<=" + SQLDatabase.nowUserDrawinglevel();
            this.ly_inma0010_drawing_bomTableAdapter.Fill(this.lYMaterialMange.ly_inma0010_drawing_bom, nowItemNum);

            this.groupBox2.Text = nowItemNum;
            this.groupBox3.Text = nowItemNum;
        }



        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if ("noSet" == nowItemNum)
            {
                return;
            }

            if (null != this.ly_inma0010DataGridView.CurrentRow)
            {
                string people = this.ly_inma0010DataGridView.CurrentRow.Cells["work_people"].Value.ToString();
                if (people != SQLDatabase.nowUserName())
                {
                    MessageBox.Show("请负责人：" + people + " 操作", "注意");
                    return;
                }

            }


            if (SQLDatabase.CheckHaveRight(SQLDatabase.NowUserID, "图纸上传"))
            {

            }
            else
            {
                MessageBox.Show("无图纸上传权限", "注意");
                return;
            }


            if (!string.IsNullOrEmpty(this.物料编码TextBox.Text))
            {

                MessageBox.Show("请先删除，再上传", "注意");
                return;
            }

            string sourcename = "noSet";
            string targetName = "\\\\192.168.1.9\\Drawing\\" + nowItemNum + ".pdf";

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


            if (Netfunction.Ping("192.168.1.9"))
            {
                Netfunction.DisConnect("192.168.1.9\\Drawing", "administrator", "jmfuq001.");
                if (Netfunction.Connect("192.168.1.9\\Drawing", "administrator", "jmfuq001."))
                {
                    File.Copy(sourcename, targetName, true);

                    Netfunction.DisConnect("192.168.1.9\\Drawing", "administrator", "jmfuq001.");

                    UpdateDrawingREC(nowItemNum);
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

        private void UpdateDrawingREC(string nowItemNum)
        {

            this.ly_inma0010_drawingBindingSource.AddNew();

            this.物料编码TextBox.Text = nowItemNum;
            this.图纸上传人TextBox.Text = SQLDatabase.nowUserName();
            this.上传日期DateTimePicker.Value = SQLDatabase.GetNowdate();

            this.ly_inma0010_drawingBindingSource.EndEdit();

            try
            {
                this.ly_inma0010_drawingTableAdapter.Update(this.lYMaterialMange.ly_inma0010_drawing);
                SQLDatabase.drawingChangeREC(nowItemNum, 0, "UPLOAD", SQLDatabase.Nowmemory_rec);
            }
            catch (SqlException sqle)
            {

                //MessageBox.Show("物资编号重复...", "注意");
                MessageBox.Show(sqle.Message, "注意");
            }

            this.ly_inma0010_drawingTableAdapter.Fill(this.lYMaterialMange.ly_inma0010_drawing, nowItemNum);

            MessageBox.Show("图纸上传完成...", "注意");


        }

        private void UpdateDrawingDel(string nowItemNum)
        {

 


            this.ly_inma0010_drawingBindingSource.RemoveCurrent();

            //this.物料编码TextBox.Text = nowItemNum;
            //this.图纸上传人TextBox.Text = SQLDatabase.nowUserName();
            //this.上传日期DateTimePicker.Value = SQLDatabase.GetNowdate();

            this.ly_inma0010_drawingBindingSource.EndEdit();

            try
            {
                this.ly_inma0010_drawingTableAdapter.Update(this.lYMaterialMange.ly_inma0010_drawing);
                SQLDatabase.drawingChangeREC(nowItemNum, 0, "DELETE", SQLDatabase.Nowmemory_rec);

            }
            catch (SqlException sqle)
            {

                //MessageBox.Show("物资编号重复...", "注意");
                MessageBox.Show(sqle.Message, "注意");
            }

            this.ly_inma0010_drawingTableAdapter.Fill(this.lYMaterialMange.ly_inma0010_drawing, nowItemNum);

            MessageBox.Show("图纸删除完成...", "注意");


        }

        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            string nowitemno;

            if ("noSet" == nowItemNum)
            {
                return;
            }

            if (string.IsNullOrEmpty(this.物料编码TextBox.Text))
            {

                MessageBox.Show("无图纸存储", "注意");
                return;
            }
            else
            {
                nowitemno = this.物料编码TextBox.Text;
            }

            //if (int.Parse(ly_inma0010DataGridView.CurrentRow.Cells["图纸级别"].Value.ToString()) > SQLDatabase.nowUserDrawinglevel())
            //{
            //    MessageBox.Show("图纸权限不够", "注意");
            //    return;
            //}

            if (1 !=  SQLDatabase.nowUserMachineDrawinglevel(nowitemno))
            {
                MessageBox.Show("图纸权限不够", "注意");
                return;
            }




            string sourcename = "\\\\192.168.1.9\\Drawing\\" + nowItemNum + ".pdf";
            //string targetName =  nowItemNum + ".pdf";

            string targetName = "nowdrawing" + ".pdf";

            ////////////////////////////////////////

            if (Netfunction.Ping("192.168.1.9"))
            {
                //Netfunction.DisConnect("192.168.1.9\\D$", "administrator", "jmfuq001.");
                Netfunction.DisConnect("192.168.1.9\\Drawing", "administrator", "jmfuq001.");
                if (Netfunction.Connect("192.168.1.9\\Drawing", "administrator", "jmfuq001."))
                {
                    if (File.Exists(sourcename))
                    {
                        if (File.Exists(targetName))
                        {
                            FileInfo fileInfo = new FileInfo(targetName);

                            //去掉隐藏属性 
                            //fileInfo.Attributes &= ~FileAttributes.Hidden; 
                            //去掉只读属性 
                            fileInfo.Attributes &= ~FileAttributes.ReadOnly; ;
                            //搜索
                            //相反的操作： 
                            ////增加只读属性 
                            //fileInfo.Attributes |= FileAttributes.ReadOnly; 
                            ////增加隐藏属性 
                            //fileInfo.Attributes |= FileAttributes.Hidden;


                            File.Delete(targetName);
                        }
                        File.Copy(sourcename, targetName, true);
                        SQLDatabase.drawingChangeREC(nowItemNum, 0, "DOWNLOAD", SQLDatabase.Nowmemory_rec);

                        System.Diagnostics.Process.Start(targetName);
                    }
                    //System.Diagnostics.Process.Start("F:\\2.pdf");

                    // axAcroPDF1.src = sourcename;

                    //Netfunction.DisConnect("192.168.1.9\\Drawing", "administrator", "jmfuq001.");
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

        private void toolStripButton7_Click(object sender, EventArgs e)
        {
            if (SQLDatabase.CheckHaveRight(SQLDatabase.NowUserID, "图纸删除"))
            {
                string sql = "select isnull(drawing_level,0) from T_users  where yhbm='" + SQLDatabase.NowUserID + "'";
                DataTable dt = null;
                using (SqlConnection connection = new SqlConnection(SQLDatabase.Connectstring))
                {

                    SqlDataAdapter adapter = new SqlDataAdapter(sql, connection);
                    DataSet ds = new DataSet();
                    adapter.Fill(ds);
                    dt = ds.Tables[0];
                }
                if (dt.Rows.Count > 0)
                {
                    if (int.Parse(dt.Rows[0][0].ToString()) < 4)
                    {
                        MessageBox.Show("权限低于4级无法删除", "注意");
                    }
                }
            }
            else
            {
                MessageBox.Show("无图纸删除权限", "注意");
                return;
            }

            //if (null != this.ly_inma0010DataGridView.CurrentRow)
            //{
            //    string people = this.ly_inma0010DataGridView.CurrentRow.Cells["work_people"].Value.ToString();
            //    if (people != SQLDatabase.nowUserName())
            //    {
            //        MessageBox.Show("请负责人：" + people + " 操作", "注意");
            //        return;
            //    }

            //}

            UpdateDrawingDel(nowItemNum);
        }

        private void ly_inma0010DataGridView_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            DataGridView dgv = sender as DataGridView;

            if (null == dgv.CurrentRow) return;



            //if ("True" == ly_material_plan_mainDataGridView.CurrentRow.Cells["配套完成0"].Value.ToString())
            //{
            //    MessageBox.Show("依赖书已经配套完成,不能修改数据...", "注意");
            //    return;

            //}

            ///////////////////////////////////////////////////////////配套完成0
            if ("图纸级别" == dgv.CurrentCell.OwningColumn.Name)
            {

                if (SQLDatabase.CheckHaveRight(SQLDatabase.NowUserID, "图纸级别设置"))
                {

                }
                else
                {
                    MessageBox.Show("无图纸级别设置权限", "注意");
                    return;
                }


                // string nowplannum = ly_material_plan_mainDataGridView.CurrentRow.Cells["图纸级别"].Value.ToString();

                ChangeValue queryForm = new ChangeValue();

                queryForm.OldValue = dgv.CurrentCell.Value.ToString();
                queryForm.NewValue = "";
                queryForm.ChangeMode = "value";
                queryForm.ShowDialog();


                if (queryForm.NewValue != "")
                {
                    //dgv.CurrentRow.Cells["发货日期0"].Value = queryForm.NewValue;
                    dgv.CurrentRow.Cells["图纸级别"].Value = queryForm.NewValue;
                }


                this.ly_inma0010cpBindingSource.EndEdit();
                this.ly_inma0010cpTableAdapter.Update(this.lYMaterialMange.ly_inma0010cp);


                SQLDatabase.drawingChangeREC(nowItemNum, 0, "CHANGE_LEV", SQLDatabase.Nowmemory_rec);




                return;

            }

        }

        private void toolStripButton8_Click(object sender, EventArgs e)
        {

            if ("noSet" == nowItemNum)
            {
                return;
            }


            if (SQLDatabase.CheckHaveRight(SQLDatabase.NowUserID, "图纸上传"))
            {

            }
            else
            {
                MessageBox.Show("无图纸上传权限", "注意");//111
                return;
            }
            if (null != this.ly_inma0010DataGridView.CurrentRow)
            {
                string people = this.ly_inma0010DataGridView.CurrentRow.Cells["work_people"].Value.ToString();
                if (people != SQLDatabase.nowUserName())
                {
                    MessageBox.Show("请负责人：" + people + " 操作", "注意");
                    return;
                }

            }

            string targetName = "\\\\192.168.1.9\\Drawing\\Electrical\\电气图_" + nowItemNum;

            OpenFileDialog openFile = new OpenFileDialog();

            openFile.Filter = "所有文件|*.*";

            string sourcename = "";
            string filetype = "";
            string filepath = "";
            if (openFile.ShowDialog() == DialogResult.OK)
            {
                filetype = System.IO.Path.GetExtension(openFile.FileName);
                filepath = ("_" + DateTime.Now.ToString("yyyyMMddHHmmss") + filetype);
                sourcename = openFile.FileName;
                targetName = targetName + (filepath);
            }
            else
            {
                return;
            }

            NewFrm.Show(this);

            if (Netfunction.Ping("192.168.1.9"))
            {
                Netfunction.DisConnect("192.168.1.9\\Drawing", "administrator", "jmfuq001.");
                if (Netfunction.Connect("192.168.1.9\\Drawing", "administrator", "jmfuq001."))
                {
                    File.Copy(sourcename, targetName, true);


                    Netfunction.DisConnect("192.168.1.9\\Drawing", "administrator", "jmfuq001.");
                    int k = 0;
                    using (SqlConnection con = new SqlConnection(SQLDatabase.Connectstring))
                    {

                        string sql = @"INSERT INTO [ly_inma0010_drawing_electrical] ([itemno] ,[file_path],[input_people] )
                                   VALUES   ('" + nowItemNum + "'  ,'电气图_" + (nowItemNum + filepath) + "','" + SQLDatabase.nowUserName() + "')";
                        using (SqlCommand cmd = new SqlCommand(sql, con))
                        {
                            con.Open();
                            k = cmd.ExecuteNonQuery();
                        }
                        SQLDatabase.drawingChangeREC(nowItemNum, 0, "UPLOAD-Electrical-" + (nowItemNum + filepath), SQLDatabase.Nowmemory_rec);
                    }
                    if (k > 0)
                    {
                        NewFrm.Hide(this);
                        MessageBox.Show("导入成功！", "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.ly_inma0010_drawing_electricalTableAdapter.Fill(this.lYMaterialMange.ly_inma0010_drawing_electrical, nowItemNum);
                    }
                }
                else
                {
                    NewFrm.Hide(this);
                    MessageBox.Show("连接主机失败,请再次尝试！", "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
            }
            else
            {
                NewFrm.Hide(this);
                MessageBox.Show("Ping主机失败,请再次尝试！", "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }


        }

        private void toolStripButton10_Click(object sender, EventArgs e)
        {
            if (this.dataGridView1.CurrentRow == null) return;
            if (SQLDatabase.CheckHaveRight(SQLDatabase.NowUserID, "图纸删除"))
            {
                string sql = "select isnull(drawing_level,0) from T_users  where yhbm='" + SQLDatabase.NowUserID + "'";
                DataTable dt = null;
                using (SqlConnection connection = new SqlConnection(SQLDatabase.Connectstring))
                {

                    SqlDataAdapter adapter = new SqlDataAdapter(sql, connection);
                    DataSet ds = new DataSet();
                    adapter.Fill(ds);
                    dt = ds.Tables[0];
                }
                if (dt.Rows.Count > 0)
                {
                    if (int.Parse(dt.Rows[0][0].ToString()) < 4)
                    {
                        MessageBox.Show("权限低于4级无法删除", "注意");
                    }
                }
            }
            else
            {
                MessageBox.Show("无图纸删除权限", "注意");
                return;
            }

            //if (null != this.dataGridView1.CurrentRow)
            //{
            //    string people = this.dataGridView1.CurrentRow.Cells["work_people"].Value.ToString();
            //    if (people != SQLDatabase.nowUserName())
            //    {
            //        MessageBox.Show("请负责人：" + people + " 操作", "注意");
            //        return;
            //    }

            //}
            if (null != this.dataGridView1.CurrentRow)
            {
                

                string id = this.dataGridView1.CurrentRow.Cells["id_dq"].Value.ToString();
                string filename = this.dataGridView1.CurrentRow.Cells["file_path"].Value.ToString();
                string wzbh = this.dataGridView1.CurrentRow.Cells["itemno"].Value.ToString();
                int k = 0;
                using (SqlConnection con = new SqlConnection(SQLDatabase.Connectstring))
                {
                    string sql = "delete from ly_inma0010_drawing_electrical where id=" + id;
                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {

                        con.Open();
                        k = cmd.ExecuteNonQuery();
                    }


                }
                SQLDatabase.drawingChangeREC(nowItemNum, 0, "DELETE_-Electrical-" + filename, SQLDatabase.Nowmemory_rec);
                if (k > 0)
                {

                    MessageBox.Show("删除成功！", "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.ly_inma0010_drawing_electricalTableAdapter.Fill(this.lYMaterialMange.ly_inma0010_drawing_electrical, wzbh);
                }
            }


        }

        private void toolStripButton9_Click(object sender, EventArgs e)
        {
            if (null == this.dataGridView1.CurrentRow) return;

            string filename = this.dataGridView1.CurrentRow.Cells["file_path"].Value.ToString();
            string sourcename = "\\\\192.168.1.9\\Drawing\\Electrical\\" + filename;
            string selePath = "";
            FolderBrowserDialog frmBrowser = new FolderBrowserDialog();
            if (frmBrowser.ShowDialog() == DialogResult.OK)
            {
                selePath = frmBrowser.SelectedPath;
            }
            else
            {
                return;
            }
            string targetName = selePath + "\\" + filename;

            ////////////////////////////////////////
            NewFrm.Show(this);
            if (Netfunction.Ping("192.168.1.9"))
            {
                //Netfunction.DisConnect("192.168.1.9\\D$", "administrator", "jmfuq001.");
                Netfunction.DisConnect("192.168.1.9\\Drawing", "administrator", "jmfuq001.");
                if (Netfunction.Connect("192.168.1.9\\Drawing", "administrator", "jmfuq001."))
                {
                    try
                    {
                        if (File.Exists(sourcename))
                        {
                            if (File.Exists(targetName))
                            {
                                FileInfo fileInfo = new FileInfo(targetName);

                                //去掉隐藏属性 
                                //fileInfo.Attributes &= ~FileAttributes.Hidden; 
                                //去掉只读属性 
                                fileInfo.Attributes &= ~FileAttributes.ReadOnly; ;
                                //搜索
                                //相反的操作： 
                                ////增加只读属性 
                                //fileInfo.Attributes |= FileAttributes.ReadOnly; 
                                ////增加隐藏属性 
                                //fileInfo.Attributes |= FileAttributes.Hidden;


                                File.Delete(targetName);
                            }
                            File.Copy(sourcename, targetName, true);
                            NewFrm.Hide(this);
                            MessageBox.Show("下载成功！");
                            SQLDatabase.drawingChangeREC(nowItemNum, 0, "DOWNLOAD-Electrical-" + filename, SQLDatabase.Nowmemory_rec);

                            // System.Diagnostics.Process.Start(targetName);
                        }


                        Netfunction.DisConnect("192.168.1.9\\Drawing", "administrator", "jmfuq001.");


                    }
                    catch (Exception ex)
                    {
                        NewFrm.Hide(this);
                        MessageBox.Show(ex.Message.ToString());

                    }
                }
                else
                {
                    NewFrm.Hide(this);
                    MessageBox.Show("连接主机失败！", "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
            }
            else
            {
                NewFrm.Hide(this);
                MessageBox.Show("Ping主机失败！", "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }

        }


        private void toolStripButton11_Click(object sender, EventArgs e)
        {

            if (this.dataGridView1.CurrentRow == null) return;
            if (SQLDatabase.CheckHaveRight(SQLDatabase.NowUserID, "图纸级别设置"))
            {

            }
            else
            {
                string people = this.dataGridView1.CurrentRow.Cells["input_people"].Value.ToString();
                if (people != SQLDatabase.nowUserName())
                {
                    MessageBox.Show("请上传人：" + people + " 操作", "注意");
                    return;
                }
            }


            string filename = this.dataGridView1.CurrentRow.Cells["file_path"].Value.ToString();

            ChangeValue queryForm = new ChangeValue();

            queryForm.OldValue = this.dataGridView1.CurrentRow.Cells["level"].Value.ToString();
            queryForm.NewValue = "";
            queryForm.ChangeMode = "value";
            queryForm.ShowDialog();


            if (queryForm.NewValue != "")
            {
                string wzbh = this.dataGridView1.CurrentRow.Cells["itemno"].Value.ToString();
                string id = this.dataGridView1.CurrentRow.Cells["id_dq"].Value.ToString();
                int k = 0;
                using (SqlConnection con = new SqlConnection(SQLDatabase.Connectstring))
                {

                    string sql = "update  ly_inma0010_drawing_electrical set level=" + queryForm.NewValue + "  where id=" + id;
                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {

                        con.Open();
                        k = cmd.ExecuteNonQuery();
                    }

                }
                SQLDatabase.drawingChangeREC(nowItemNum, 0, "CHANGE_LEV-Electrical-" + filename, SQLDatabase.Nowmemory_rec);
                if (k > 0)
                {
                    MessageBox.Show("修改成功！", "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.ly_inma0010_drawing_electricalTableAdapter.Fill(this.lYMaterialMange.ly_inma0010_drawing_electrical, wzbh);
                    lyinma0010drawingelectricalBindingSource.Position = lyinma0010drawingelectricalBindingSource.Find("id", id);
                }
            }
        }

        private void toolStripButton13_Click(object sender, EventArgs e)
        {


            if (this.dataGridView1.CurrentRow == null) return;
            if (SQLDatabase.CheckHaveRight(SQLDatabase.NowUserID, "图纸级别设置"))
            {

            }
            else
            {
                string people = this.dataGridView1.CurrentRow.Cells["input_people"].Value.ToString();
                if (people != SQLDatabase.nowUserName())
                {
                    MessageBox.Show("请上传人：" + people + " 操作", "注意");
                    return;
                }
            }
            string filename = this.dataGridView1.CurrentRow.Cells["file_path"].Value.ToString();

            ChangeValue queryForm = new ChangeValue();

            queryForm.OldValue = this.dataGridView1.CurrentRow.Cells["remark"].Value.ToString();
            queryForm.NewValue = "";
            queryForm.ChangeMode = "longstring";
            queryForm.ShowDialog();


            if (queryForm.NewValue != "")
            {
                string wzbh = this.dataGridView1.CurrentRow.Cells["itemno"].Value.ToString();
                string id = this.dataGridView1.CurrentRow.Cells["id_dq"].Value.ToString();
                int k = 0;
                using (SqlConnection con = new SqlConnection(SQLDatabase.Connectstring))
                {

                    string sql = "update  ly_inma0010_drawing_electrical set remark='" + queryForm.NewValue + "'  where id=" + id;
                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {

                        con.Open();
                        k = cmd.ExecuteNonQuery();
                    }

                }
                SQLDatabase.drawingChangeREC(nowItemNum, 0, "CHANGE_Remark-Electrical-" + filename, SQLDatabase.Nowmemory_rec);
                if (k > 0)
                {
                    MessageBox.Show("修改成功！", "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.ly_inma0010_drawing_electricalTableAdapter.Fill(this.lYMaterialMange.ly_inma0010_drawing_electrical, wzbh);
                    lyinma0010drawingelectricalBindingSource.Position = lyinma0010drawingelectricalBindingSource.Find("id", id);
                }
            }
        }

        private void toolStripButton12_Click(object sender, EventArgs e)
        {
            if ("noSet" == nowItemNum)
            {
                return;
            }


            if (SQLDatabase.CheckHaveRight(SQLDatabase.NowUserID, "图纸上传"))
            {

            }
            else
            {
                MessageBox.Show("无图纸上传权限", "注意");
                return;
            }
            if (null != this.ly_inma0010DataGridView.CurrentRow)
            {
                string people = this.ly_inma0010DataGridView.CurrentRow.Cells["work_people"].Value.ToString();
                if (people != SQLDatabase.nowUserName())
                {
                    MessageBox.Show("请负责人：" + people + " 操作", "注意");
                    return;
                }

            }

            string targetName = "\\\\192.168.1.9\\Drawing\\PCB\\PCB图_" + nowItemNum;

            OpenFileDialog openFile = new OpenFileDialog();

            openFile.Filter = "所有文件|*.*";

            string sourcename = "";
            string filetype = "";
            string filepath = "";
            if (openFile.ShowDialog() == DialogResult.OK)
            {
                filetype = System.IO.Path.GetExtension(openFile.FileName);
                filepath = ("_" + DateTime.Now.ToString("yyyyMMddHHmmss") + filetype);
                sourcename = openFile.FileName;
                targetName = targetName + (filepath);
            }
            else
            {
                return;
            }
            NewFrm.Show(this);

            if (Netfunction.Ping("192.168.1.9"))
            {
                Netfunction.DisConnect("192.168.1.9\\Drawing", "administrator", "jmfuq001.");
                if (Netfunction.Connect("192.168.1.9\\Drawing", "administrator", "jmfuq001."))
                {
                    File.Copy(sourcename, targetName, true);


                    Netfunction.DisConnect("192.168.1.9\\Drawing", "administrator", "jmfuq001.");
                    int k = 0;
                    using (SqlConnection con = new SqlConnection(SQLDatabase.Connectstring))
                    {

                        string sql = @"INSERT INTO [ly_inma0010_drawing_pcb] ([itemno] ,[file_path],[input_people] )
                                   VALUES   ('" + nowItemNum + "'  ,'PCB图_" + (nowItemNum + filepath) + "','" + SQLDatabase.nowUserName() + "')";
                        using (SqlCommand cmd = new SqlCommand(sql, con))
                        {
                            con.Open();
                            k = cmd.ExecuteNonQuery();
                        }
                        SQLDatabase.drawingChangeREC(nowItemNum, 0, "UPLOAD-PCB-" + (nowItemNum + filepath), SQLDatabase.Nowmemory_rec);
                    }
                    if (k > 0)
                    {
                        NewFrm.Hide(this);
                        MessageBox.Show("导入成功！", "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.ly_inma0010_drawing_pcbTableAdapter.Fill(this.lYMaterialMange.ly_inma0010_drawing_pcb, nowItemNum);
                    }
                }
                else
                {
                    NewFrm.Hide(this);
                    MessageBox.Show("连接主机失败,请再次尝试！", "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
            }
            else
            {
                NewFrm.Hide(this);
                MessageBox.Show("Ping主机失败,请再次尝试！", "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }


        }

        private void toolStripButton14_Click(object sender, EventArgs e)
        {
            if (null == this.dataGridView2.CurrentRow) return;

            string filename = this.dataGridView2.CurrentRow.Cells["file_path_2"].Value.ToString();
            string sourcename = "\\\\192.168.1.9\\Drawing\\PCB\\" + filename;
            string selePath = "";
            FolderBrowserDialog frmBrowser = new FolderBrowserDialog();
            if (frmBrowser.ShowDialog() == DialogResult.OK)
            {
                selePath = frmBrowser.SelectedPath;
            }
            else
            {
                return;
            }
            string targetName = selePath + "\\" + filename;

            ////////////////////////////////////////
            NewFrm.Show(this);
            if (Netfunction.Ping("192.168.1.9"))
            {
                //Netfunction.DisConnect("192.168.1.9\\D$", "administrator", "jmfuq001.");
                Netfunction.DisConnect("192.168.1.9\\Drawing", "administrator", "jmfuq001.");
                if (Netfunction.Connect("192.168.1.9\\Drawing", "administrator", "jmfuq001."))
                {
                    try
                    {
                        if (File.Exists(sourcename))
                        {
                            if (File.Exists(targetName))
                            {
                                FileInfo fileInfo = new FileInfo(targetName);

                                //去掉隐藏属性 
                                //fileInfo.Attributes &= ~FileAttributes.Hidden; 
                                //去掉只读属性 
                                fileInfo.Attributes &= ~FileAttributes.ReadOnly; ;
                                //搜索
                                //相反的操作： 
                                ////增加只读属性 
                                //fileInfo.Attributes |= FileAttributes.ReadOnly; 
                                ////增加隐藏属性 
                                //fileInfo.Attributes |= FileAttributes.Hidden;


                                File.Delete(targetName);
                            }
                            File.Copy(sourcename, targetName, true);
                            NewFrm.Hide(this);
                            MessageBox.Show("下载成功！");
                            SQLDatabase.drawingChangeREC(nowItemNum, 0, "DOWNLOAD-PCB-" + filename, SQLDatabase.Nowmemory_rec);

                            // System.Diagnostics.Process.Start(targetName);
                        }


                        Netfunction.DisConnect("192.168.1.9\\Drawing", "administrator", "jmfuq001.");


                    }
                    catch (Exception ex)
                    {
                        NewFrm.Hide(this);
                        MessageBox.Show(ex.Message.ToString());

                    }
                }
                else
                {
                    NewFrm.Hide(this);
                    MessageBox.Show("连接主机失败！", "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
            }
            else
            {
                NewFrm.Hide(this);
                MessageBox.Show("Ping主机失败！", "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }

        }

        private void toolStripButton15_Click(object sender, EventArgs e)
        {
            if (this.dataGridView2.CurrentRow == null) return;
            if (SQLDatabase.CheckHaveRight(SQLDatabase.NowUserID, "图纸删除"))
            {
                string sql = "select isnull(drawing_level,0) from T_users  where yhbm='" + SQLDatabase.NowUserID + "'";
                DataTable dt = null;
                using (SqlConnection connection = new SqlConnection(SQLDatabase.Connectstring))
                {

                    SqlDataAdapter adapter = new SqlDataAdapter(sql, connection);
                    DataSet ds = new DataSet();
                    adapter.Fill(ds);
                    dt = ds.Tables[0];
                }
                if (dt.Rows.Count > 0)
                {
                    if (int.Parse(dt.Rows[0][0].ToString()) < 4)
                    {
                        MessageBox.Show("权限低于4级无法删除", "注意");
                    }
                }
            }
            else
            {
                MessageBox.Show("无图纸删除权限", "注意");
                return;
            }

            //if (null != this.dataGridView2.CurrentRow)
            //{
            //    string people = this.dataGridView2.CurrentRow.Cells["work_people"].Value.ToString();
            //    if (people != SQLDatabase.nowUserName())
            //    {
            //        MessageBox.Show("请负责人：" + people + " 操作", "注意");
            //        return;
            //    }

            //}

            if (null != this.dataGridView2.CurrentRow)
            {


                string id = this.dataGridView2.CurrentRow.Cells["id_pcb"].Value.ToString();
                string filename = this.dataGridView2.CurrentRow.Cells["file_path_2"].Value.ToString();
                string wzbh = this.dataGridView2.CurrentRow.Cells["itemno_2"].Value.ToString();
                int k = 0;
                using (SqlConnection con = new SqlConnection(SQLDatabase.Connectstring))
                {
                    string sql = "delete from ly_inma0010_drawing_pcb where id=" + id;
                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {

                        con.Open();
                        k = cmd.ExecuteNonQuery();
                    }


                }
                SQLDatabase.drawingChangeREC(nowItemNum, 0, "DELETE_-PCB-" + filename, SQLDatabase.Nowmemory_rec);
                if (k > 0)
                {

                    MessageBox.Show("删除成功！", "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.ly_inma0010_drawing_pcbTableAdapter.Fill(this.lYMaterialMange.ly_inma0010_drawing_pcb, wzbh);
                }
            }
        }

        private void toolStripButton16_Click(object sender, EventArgs e)
        {
            if (this.dataGridView2.CurrentRow == null) return;
            if (SQLDatabase.CheckHaveRight(SQLDatabase.NowUserID, "图纸级别设置"))
            {

            }
            else
            {
                string people = this.dataGridView2.CurrentRow.Cells["input_people_2"].Value.ToString();
                if (people != SQLDatabase.nowUserName())
                {
                    MessageBox.Show("请上传人：" + people + " 操作", "注意");
                    return;
                }
            }


            string filename = this.dataGridView2.CurrentRow.Cells["file_path_2"].Value.ToString();

            ChangeValue queryForm = new ChangeValue();

            queryForm.OldValue = this.dataGridView2.CurrentRow.Cells["level_2"].Value.ToString();
            queryForm.NewValue = "";
            queryForm.ChangeMode = "value";
            queryForm.ShowDialog();


            if (queryForm.NewValue != "")
            {
                string wzbh = this.dataGridView2.CurrentRow.Cells["itemno_2"].Value.ToString();
                string id = this.dataGridView2.CurrentRow.Cells["id_pcb"].Value.ToString();
                int k = 0;
                using (SqlConnection con = new SqlConnection(SQLDatabase.Connectstring))
                {

                    string sql = "update  ly_inma0010_drawing_pcb set level=" + queryForm.NewValue + "  where id=" + id;
                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {

                        con.Open();
                        k = cmd.ExecuteNonQuery();
                    }

                }
                SQLDatabase.drawingChangeREC(nowItemNum, 0, "CHANGE_LEV-PCB-" + filename, SQLDatabase.Nowmemory_rec);
                if (k > 0)
                {
                    MessageBox.Show("修改成功！", "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.ly_inma0010_drawing_pcbTableAdapter.Fill(this.lYMaterialMange.ly_inma0010_drawing_pcb, wzbh);
                    lyinma0010drawingpcbBindingSource.Position = lyinma0010drawingpcbBindingSource.Find("id", id);
                }
            }
        }

        private void toolStripButton17_Click(object sender, EventArgs e)
        {

            if (this.dataGridView2.CurrentRow == null) return;
            if (SQLDatabase.CheckHaveRight(SQLDatabase.NowUserID, "图纸级别设置"))
            {

            }
            else
            {
                string people = this.dataGridView2.CurrentRow.Cells["input_people_2"].Value.ToString();
                if (people != SQLDatabase.nowUserName())
                {
                    MessageBox.Show("请上传人：" + people + " 操作", "注意");
                    return;
                }
            }
            string filename = this.dataGridView2.CurrentRow.Cells["file_path_2"].Value.ToString();

            ChangeValue queryForm = new ChangeValue();

            queryForm.OldValue = this.dataGridView2.CurrentRow.Cells["remark_2"].Value.ToString();
            queryForm.NewValue = "";
            queryForm.ChangeMode = "longstring";
            queryForm.ShowDialog();


            if (queryForm.NewValue != "")
            {
                string wzbh = this.dataGridView2.CurrentRow.Cells["itemno_2"].Value.ToString();
                string id = this.dataGridView2.CurrentRow.Cells["id_pcb"].Value.ToString();
                int k = 0;
                using (SqlConnection con = new SqlConnection(SQLDatabase.Connectstring))
                {

                    string sql = "update  ly_inma0010_drawing_pcb set remark='" + queryForm.NewValue + "'  where id=" + id;
                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {

                        con.Open();
                        k = cmd.ExecuteNonQuery();
                    }

                }
                SQLDatabase.drawingChangeREC(nowItemNum, 0, "CHANGE_Remark-PCB-" + filename, SQLDatabase.Nowmemory_rec);
                if (k > 0)
                {
                    MessageBox.Show("修改成功！", "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.ly_inma0010_drawing_pcbTableAdapter.Fill(this.lYMaterialMange.ly_inma0010_drawing_pcb, wzbh);
                    lyinma0010drawingpcbBindingSource.Position = lyinma0010drawingpcbBindingSource.Find("id", id);
                }
            }
        }

        private void toolStripButton18_Click(object sender, EventArgs e)
        {
            if ("noSet" == nowItemNum)
            {
                return;
            }

            if (SQLDatabase.CheckHaveRight(SQLDatabase.NowUserID, "图纸上传"))
            {

            }
            else
            {
                MessageBox.Show("无图纸上传权限", "注意");
                return;
            }
            if (null != this.ly_inma0010DataGridView.CurrentRow)
            {
                string people = this.ly_inma0010DataGridView.CurrentRow.Cells["work_people"].Value.ToString();
                if (people != SQLDatabase.nowUserName())
                {
                    MessageBox.Show("请负责人：" + people + " 操作", "注意");
                    return;
                }

            }

            string targetName = "\\\\192.168.1.9\\Drawing\\Process\\工艺图_" + nowItemNum;

            OpenFileDialog openFile = new OpenFileDialog();

            openFile.Filter = "所有文件|*.*";

            string sourcename = "";
            string filetype = "";
            string filepath = "";
            if (openFile.ShowDialog() == DialogResult.OK)
            {
                filetype = System.IO.Path.GetExtension(openFile.FileName);
                filepath = ("_" + DateTime.Now.ToString("yyyyMMddHHmmss") + filetype);
                sourcename = openFile.FileName;
                targetName = targetName + (filepath);
            }
            else
            {
                return;
            }

            NewFrm.Show(this);
            if (Netfunction.Ping("192.168.1.9"))
            {
                Netfunction.DisConnect("192.168.1.9\\Drawing", "administrator", "jmfuq001.");
                if (Netfunction.Connect("192.168.1.9\\Drawing", "administrator", "jmfuq001."))
                {
                    File.Copy(sourcename, targetName, true);


                    Netfunction.DisConnect("192.168.1.9\\Drawing", "administrator", "jmfuq001.");
                    int k = 0;
                    using (SqlConnection con = new SqlConnection(SQLDatabase.Connectstring))
                    {

                        string sql = @"INSERT INTO [ly_inma0010_drawing_process] ([itemno] ,[file_path],[input_people] )
                                   VALUES   ('" + nowItemNum + "'  ,'工艺图_" + (nowItemNum + filepath) + "','" + SQLDatabase.nowUserName() + "')";
                        using (SqlCommand cmd = new SqlCommand(sql, con))
                        {
                            con.Open();
                            k = cmd.ExecuteNonQuery();
                        }
                        SQLDatabase.drawingChangeREC(nowItemNum, 0, "UPLOAD-Process-" + (nowItemNum + filepath), SQLDatabase.Nowmemory_rec);
                    }
                    if (k > 0)
                    {
                        NewFrm.Hide(this);
                        MessageBox.Show("导入成功！", "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.ly_inma0010_drawing_processTableAdapter.Fill(this.lYMaterialMange.ly_inma0010_drawing_process, nowItemNum);
                    }
                }
                else
                {
                    NewFrm.Hide(this);
                    MessageBox.Show("连接主机失败,请再次尝试！", "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
            }
            else
            {
                NewFrm.Hide(this);
                MessageBox.Show("Ping主机失败,请再次尝试！", "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void toolStripButton19_Click(object sender, EventArgs e)
        {
            if (null == this.dataGridView3.CurrentRow) return;

            string filename = this.dataGridView3.CurrentRow.Cells["file_path_3"].Value.ToString();
            string sourcename = "\\\\192.168.1.9\\Drawing\\Process\\" + filename;
            string selePath = "";
            FolderBrowserDialog frmBrowser = new FolderBrowserDialog();
            if (frmBrowser.ShowDialog() == DialogResult.OK)
            {
                selePath = frmBrowser.SelectedPath;
            }
            else
            {
                return;
            }
            string targetName = selePath + "\\" + filename;

            NewFrm.Show(this);

            if (Netfunction.Ping("192.168.1.9"))
            {
                //Netfunction.DisConnect("192.168.1.9\\D$", "administrator", "jmfuq001.");
                Netfunction.DisConnect("192.168.1.9\\Drawing", "administrator", "jmfuq001.");
                if (Netfunction.Connect("192.168.1.9\\Drawing", "administrator", "jmfuq001."))
                {
                    try
                    {
                        if (File.Exists(sourcename))
                        {
                            if (File.Exists(targetName))
                            {
                                FileInfo fileInfo = new FileInfo(targetName);

                                //去掉隐藏属性 
                                //fileInfo.Attributes &= ~FileAttributes.Hidden; 
                                //去掉只读属性 
                                fileInfo.Attributes &= ~FileAttributes.ReadOnly; ;
                                //搜索
                                //相反的操作： 
                                ////增加只读属性 
                                //fileInfo.Attributes |= FileAttributes.ReadOnly; 
                                ////增加隐藏属性 
                                //fileInfo.Attributes |= FileAttributes.Hidden;


                                File.Delete(targetName);
                            }
                            File.Copy(sourcename, targetName, true);
                            NewFrm.Hide(this);
                            MessageBox.Show("下载成功！");
                            SQLDatabase.drawingChangeREC(nowItemNum, 0, "DOWNLOAD-Process-" + filename, SQLDatabase.Nowmemory_rec);

                            // System.Diagnostics.Process.Start(targetName);
                        }


                        Netfunction.DisConnect("192.168.1.9\\Drawing", "administrator", "jmfuq001.");


                    }
                    catch (Exception ex)
                    {
                        NewFrm.Hide(this);
                        MessageBox.Show(ex.Message.ToString());

                    }
                }
                else
                {
                    NewFrm.Hide(this);
                    MessageBox.Show("连接主机失败！", "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
            }
            else
            {
                NewFrm.Hide(this);
                MessageBox.Show("Ping主机失败！", "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
        }

        private void toolStripButton20_Click(object sender, EventArgs e)
        {
            if (this.dataGridView3.CurrentRow == null) return;
            if (SQLDatabase.CheckHaveRight(SQLDatabase.NowUserID, "图纸删除"))
            {
                string sql = "select isnull(drawing_level,0) from T_users  where yhbm='" + SQLDatabase.NowUserID + "'";
                DataTable dt = null;
                using (SqlConnection connection = new SqlConnection(SQLDatabase.Connectstring))
                {

                    SqlDataAdapter adapter = new SqlDataAdapter(sql, connection);
                    DataSet ds = new DataSet();
                    adapter.Fill(ds);
                    dt = ds.Tables[0];
                }
                if (dt.Rows.Count > 0)
                {
                    if (int.Parse(dt.Rows[0][0].ToString()) < 4)
                    {
                        MessageBox.Show("权限低于4级无法删除", "注意");
                    }
                }
            }
            else
            {
                MessageBox.Show("无图纸删除权限", "注意");
                return;
            }

            //if (null != this.dataGridView3.CurrentRow)
            //{
            //    string people = this.dataGridView3.CurrentRow.Cells["work_people"].Value.ToString();
            //    if (people != SQLDatabase.nowUserName())
            //    {
            //        MessageBox.Show("请负责人：" + people + " 操作", "注意");
            //        return;
            //    }

            //}
            if (null != this.dataGridView3.CurrentRow)
            {


                string id = this.dataGridView3.CurrentRow.Cells["id_process"].Value.ToString();
                string filename = this.dataGridView3.CurrentRow.Cells["file_path_3"].Value.ToString();
                string wzbh = this.dataGridView3.CurrentRow.Cells["itemno_3"].Value.ToString();
                int k = 0;
                using (SqlConnection con = new SqlConnection(SQLDatabase.Connectstring))
                {
                    string sql = "delete from ly_inma0010_drawing_process where id=" + id;
                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {

                        con.Open();
                        k = cmd.ExecuteNonQuery();
                    }


                }
                SQLDatabase.drawingChangeREC(nowItemNum, 0, "DELETE_-Process-" + filename, SQLDatabase.Nowmemory_rec);
                if (k > 0)
                {

                    MessageBox.Show("删除成功！", "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.ly_inma0010_drawing_processTableAdapter.Fill(this.lYMaterialMange.ly_inma0010_drawing_process, wzbh);
                }
            }
        }

        private void toolStripButton21_Click(object sender, EventArgs e)
        {

            if (this.dataGridView3.CurrentRow == null) return;
            if (SQLDatabase.CheckHaveRight(SQLDatabase.NowUserID, "图纸级别设置"))
            {

            }
            else
            {
                string people = this.dataGridView3.CurrentRow.Cells["input_people_3"].Value.ToString();
                if (people != SQLDatabase.nowUserName())
                {
                    MessageBox.Show("请上传人：" + people + " 操作", "注意");
                    return;
                }
            }


            string filename = this.dataGridView3.CurrentRow.Cells["file_path_3"].Value.ToString();

            ChangeValue queryForm = new ChangeValue();

            queryForm.OldValue = this.dataGridView3.CurrentRow.Cells["level_3"].Value.ToString();
            queryForm.NewValue = "";
            queryForm.ChangeMode = "value";
            queryForm.ShowDialog();


            if (queryForm.NewValue != "")
            {
                string wzbh = this.dataGridView3.CurrentRow.Cells["itemno_3"].Value.ToString();
                string id = this.dataGridView3.CurrentRow.Cells["id_process"].Value.ToString();
                int k = 0;
                using (SqlConnection con = new SqlConnection(SQLDatabase.Connectstring))
                {

                    string sql = "update  ly_inma0010_drawing_process set level=" + queryForm.NewValue + "  where id=" + id;
                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {

                        con.Open();
                        k = cmd.ExecuteNonQuery();
                    }

                }
                SQLDatabase.drawingChangeREC(nowItemNum, 0, "CHANGE_LEV-Process-" + filename, SQLDatabase.Nowmemory_rec);
                if (k > 0)
                {
                    MessageBox.Show("修改成功！", "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.ly_inma0010_drawing_processTableAdapter.Fill(this.lYMaterialMange.ly_inma0010_drawing_process, wzbh);
                    lyinma0010drawingprocessBindingSource.Position = lyinma0010drawingprocessBindingSource.Find("id", id);
                }
            }
        }
        private void toolStripButton22_Click(object sender, EventArgs e)
        {

            if (this.dataGridView3.CurrentRow == null) return;
            if (SQLDatabase.CheckHaveRight(SQLDatabase.NowUserID, "图纸级别设置"))
            {

            }
            else
            {
                string people = this.dataGridView3.CurrentRow.Cells["input_people_3"].Value.ToString();
                if (people != SQLDatabase.nowUserName())
                {
                    MessageBox.Show("请上传人：" + people + " 操作", "注意");
                    return;
                }
            }
            string filename = this.dataGridView3.CurrentRow.Cells["file_path_3"].Value.ToString();

            ChangeValue queryForm = new ChangeValue();

            queryForm.OldValue = this.dataGridView3.CurrentRow.Cells["remark_3"].Value.ToString();
            queryForm.NewValue = "";
            queryForm.ChangeMode = "longstring";
            queryForm.ShowDialog();


            if (queryForm.NewValue != "")
            {
                string wzbh = this.dataGridView3.CurrentRow.Cells["itemno_3"].Value.ToString();
                string id = this.dataGridView3.CurrentRow.Cells["id_process"].Value.ToString();
                int k = 0;
                using (SqlConnection con = new SqlConnection(SQLDatabase.Connectstring))
                {

                    string sql = "update  ly_inma0010_drawing_process set remark='" + queryForm.NewValue + "'  where id=" + id;
                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {

                        con.Open();
                        k = cmd.ExecuteNonQuery();
                    }

                }
                SQLDatabase.drawingChangeREC(nowItemNum, 0, "CHANGE_Remark-Process-" + filename, SQLDatabase.Nowmemory_rec);
                if (k > 0)
                {
                    MessageBox.Show("修改成功！", "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.ly_inma0010_drawing_processTableAdapter.Fill(this.lYMaterialMange.ly_inma0010_drawing_process, wzbh);
                    lyinma0010drawingprocessBindingSource.Position = lyinma0010drawingprocessBindingSource.Find("id", id);
                }
            }
        }

        private void toolStripButton23_Click(object sender, EventArgs e)
        {

            if ("noSet" == nowItemNum)
            {
                return;
            }

            if (SQLDatabase.CheckHaveRight(SQLDatabase.NowUserID, "图纸上传"))
            {

            }
            else
            {
                MessageBox.Show("无图纸上传权限", "注意");
                return;
            }

            if (null != this.ly_inma0010DataGridView.CurrentRow)
            {
                string people = this.ly_inma0010DataGridView.CurrentRow.Cells["work_people"].Value.ToString();
                if (people != SQLDatabase.nowUserName())
                {
                    MessageBox.Show("请负责人：" + people + " 操作", "注意");
                    return;
                }

            }
            string targetName = "\\\\192.168.1.9\\Drawing\\Inspection\\检验图_" + nowItemNum;

            OpenFileDialog openFile = new OpenFileDialog();

            openFile.Filter = "所有文件|*.*";

            string sourcename = "";
            string filetype = "";
            string filepath = "";
            if (openFile.ShowDialog() == DialogResult.OK)
            {
                filetype = System.IO.Path.GetExtension(openFile.FileName);
                filepath = ("_" + DateTime.Now.ToString("yyyyMMddHHmmss") + filetype);
                sourcename = openFile.FileName;
                targetName = targetName + (filepath);
            }
            else
            {
                return;
            }

            NewFrm.Show(this);

            if (Netfunction.Ping("192.168.1.9"))
            {
                Netfunction.DisConnect("192.168.1.9\\Drawing", "administrator", "jmfuq001.");
                if (Netfunction.Connect("192.168.1.9\\Drawing", "administrator", "jmfuq001."))
                {
                    File.Copy(sourcename, targetName, true);


                    Netfunction.DisConnect("192.168.1.9\\Drawing", "administrator", "jmfuq001.");
                    int k = 0;
                    using (SqlConnection con = new SqlConnection(SQLDatabase.Connectstring))
                    {

                        string sql = @"INSERT INTO [ly_inma0010_drawing_Inspection] ([itemno] ,[file_path],[input_people])
                                   VALUES   ('" + nowItemNum + "'  ,'检验图_" + (nowItemNum + filepath) + "','" + SQLDatabase.nowUserName() + "')";
                        using (SqlCommand cmd = new SqlCommand(sql, con))
                        {
                            con.Open();
                            k = cmd.ExecuteNonQuery();
                        }
                        SQLDatabase.drawingChangeREC(nowItemNum, 0, "UPLOAD-Inspection-" + (nowItemNum + filepath), SQLDatabase.Nowmemory_rec);
                    }
                    if (k > 0)
                    {
                        NewFrm.Hide(this);
                        MessageBox.Show("导入成功！", "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.ly_inma0010_drawing_InspectionTableAdapter.Fill(this.lYMaterialMange.ly_inma0010_drawing_Inspection, nowItemNum);
                    }
                }
                else
                {
                    NewFrm.Hide(this);
                    MessageBox.Show("连接主机失败,请再次尝试！", "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
            }
            else
            {
                NewFrm.Hide(this);
                MessageBox.Show("Ping主机失败,请再次尝试！", "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void toolStripButton24_Click(object sender, EventArgs e)
        {
            if (null == this.dataGridView4.CurrentRow) return;

            string filename = this.dataGridView4.CurrentRow.Cells["file_path_4"].Value.ToString();
            string sourcename = "\\\\192.168.1.9\\Drawing\\Inspection\\" + filename;
            string selePath = "";
            FolderBrowserDialog frmBrowser = new FolderBrowserDialog();
            if (frmBrowser.ShowDialog() == DialogResult.OK)
            {
                selePath = frmBrowser.SelectedPath;
            }
            else
            {
                return;
            }
            string targetName = selePath + "\\" + filename;
            NewFrm.Show(this);

            if (Netfunction.Ping("192.168.1.9"))
            {
                //Netfunction.DisConnect("192.168.1.9\\D$", "administrator", "jmfuq001.");
                Netfunction.DisConnect("192.168.1.9\\Drawing", "administrator", "jmfuq001.");
                if (Netfunction.Connect("192.168.1.9\\Drawing", "administrator", "jmfuq001."))
                {
                    try
                    {
                        if (File.Exists(sourcename))
                        {
                            if (File.Exists(targetName))
                            {
                                FileInfo fileInfo = new FileInfo(targetName);

                                //去掉隐藏属性 
                                //fileInfo.Attributes &= ~FileAttributes.Hidden; 
                                //去掉只读属性 
                                fileInfo.Attributes &= ~FileAttributes.ReadOnly; ;
                                //搜索
                                //相反的操作： 
                                ////增加只读属性 
                                //fileInfo.Attributes |= FileAttributes.ReadOnly; 
                                ////增加隐藏属性 
                                //fileInfo.Attributes |= FileAttributes.Hidden;


                                File.Delete(targetName);
                            }
                            File.Copy(sourcename, targetName, true);
                            NewFrm.Hide(this);
                            MessageBox.Show("下载成功！");
                            SQLDatabase.drawingChangeREC(nowItemNum, 0, "DOWNLOAD-Inspection-" + filename, SQLDatabase.Nowmemory_rec);

                            // System.Diagnostics.Process.Start(targetName);
                        }


                        Netfunction.DisConnect("192.168.1.9\\Drawing", "administrator", "jmfuq001.");


                    }
                    catch (Exception ex)
                    {
                        NewFrm.Hide(this);
                        MessageBox.Show(ex.Message.ToString());

                    }
                }
                else
                {
                    NewFrm.Hide(this);
                    MessageBox.Show("连接主机失败！", "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
            }
            else
            {
                NewFrm.Hide(this);
                MessageBox.Show("Ping主机失败！", "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
        }

        private void toolStripButton25_Click(object sender, EventArgs e)
        {
            if (this.dataGridView4.CurrentRow == null) return;
            if (SQLDatabase.CheckHaveRight(SQLDatabase.NowUserID, "图纸删除"))
            {
                string sql = "select isnull(drawing_level,0) from T_users  where yhbm='" + SQLDatabase.NowUserID + "'";
                DataTable dt = null;
                using (SqlConnection connection = new SqlConnection(SQLDatabase.Connectstring))
                {

                    SqlDataAdapter adapter = new SqlDataAdapter(sql, connection);
                    DataSet ds = new DataSet();
                    adapter.Fill(ds);
                    dt = ds.Tables[0];
                }
                if (dt.Rows.Count > 0)
                {
                    if (int.Parse(dt.Rows[0][0].ToString()) < 4)
                    {
                        MessageBox.Show("权限低于4级无法删除", "注意");
                    }
                }
            }
            else
            {
                MessageBox.Show("无图纸删除权限", "注意");
                return;
            }

            //if (null != this.dataGridView4.CurrentRow)
            //{
            //    string people = this.dataGridView4.CurrentRow.Cells["work_people"].Value.ToString();
            //    if (people != SQLDatabase.nowUserName())
            //    {
            //        MessageBox.Show("请负责人：" + people + " 操作", "注意");
            //        return;
            //    }

            //}

            if (null != this.dataGridView4.CurrentRow)
            {


                string id = this.dataGridView4.CurrentRow.Cells["id_inspection"].Value.ToString();
                string filename = this.dataGridView4.CurrentRow.Cells["file_path_4"].Value.ToString();
                string wzbh = this.dataGridView4.CurrentRow.Cells["itemno_4"].Value.ToString();
                int k = 0;
                using (SqlConnection con = new SqlConnection(SQLDatabase.Connectstring))
                {
                    string sql = "delete from ly_inma0010_drawing_Inspection where id=" + id;
                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {

                        con.Open();
                        k = cmd.ExecuteNonQuery();
                    }


                }
                SQLDatabase.drawingChangeREC(nowItemNum, 0, "DELETE_-Inspection-" + filename, SQLDatabase.Nowmemory_rec);
                if (k > 0)
                {

                    MessageBox.Show("删除成功！", "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.ly_inma0010_drawing_InspectionTableAdapter.Fill(this.lYMaterialMange.ly_inma0010_drawing_Inspection, wzbh);
                }
            }
        }

        private void toolStripButton26_Click(object sender, EventArgs e)
        {
            if (this.dataGridView4.CurrentRow == null) return;
            if (SQLDatabase.CheckHaveRight(SQLDatabase.NowUserID, "图纸级别设置"))
            {

            }
            else
            {
                string people = this.dataGridView4.CurrentRow.Cells["input_people_4"].Value.ToString();
                if (people != SQLDatabase.nowUserName())
                {
                    MessageBox.Show("请上传人：" + people + " 操作", "注意");
                    return;
                }
            }


            string filename = this.dataGridView4.CurrentRow.Cells["file_path_4"].Value.ToString();

            ChangeValue queryForm = new ChangeValue();

            queryForm.OldValue = this.dataGridView4.CurrentRow.Cells["level_4"].Value.ToString();
            queryForm.NewValue = "";
            queryForm.ChangeMode = "value";
            queryForm.ShowDialog();


            if (queryForm.NewValue != "")
            {
                string wzbh = this.dataGridView4.CurrentRow.Cells["itemno_4"].Value.ToString();
                string id = this.dataGridView4.CurrentRow.Cells["id_inspection"].Value.ToString();
                int k = 0;
                using (SqlConnection con = new SqlConnection(SQLDatabase.Connectstring))
                {

                    string sql = "update  ly_inma0010_drawing_Inspection set level=" + queryForm.NewValue + "  where id=" + id;
                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {

                        con.Open();
                        k = cmd.ExecuteNonQuery();
                    }

                }
                SQLDatabase.drawingChangeREC(nowItemNum, 0, "CHANGE_LEV-Inspection-" + filename, SQLDatabase.Nowmemory_rec);
                if (k > 0)
                {
                    MessageBox.Show("修改成功！", "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.ly_inma0010_drawing_InspectionTableAdapter.Fill(this.lYMaterialMange.ly_inma0010_drawing_Inspection, wzbh);
                    lyinma0010drawingInspectionBindingSource.Position = lyinma0010drawingInspectionBindingSource.Find("id", id);
                }
            }
        }

        private void toolStripButton27_Click(object sender, EventArgs e)
        {

            if (this.dataGridView4.CurrentRow == null) return;
            if (SQLDatabase.CheckHaveRight(SQLDatabase.NowUserID, "图纸级别设置"))
            {

            }
            else
            {
                string people = this.dataGridView4.CurrentRow.Cells["input_people_4"].Value.ToString();
                if (people != SQLDatabase.nowUserName())
                {
                    MessageBox.Show("请上传人：" + people + " 操作", "注意");
                    return;
                }
            }
            string filename = this.dataGridView4.CurrentRow.Cells["file_path_4"].Value.ToString();

            ChangeValue queryForm = new ChangeValue();

            queryForm.OldValue = this.dataGridView4.CurrentRow.Cells["remark_4"].Value.ToString();
            queryForm.NewValue = "";
            queryForm.ChangeMode = "longstring";
            queryForm.ShowDialog();


            if (queryForm.NewValue != "")
            {
                string wzbh = this.dataGridView4.CurrentRow.Cells["itemno_4"].Value.ToString();
                string id = this.dataGridView4.CurrentRow.Cells["id_inspection"].Value.ToString();
                int k = 0;
                using (SqlConnection con = new SqlConnection(SQLDatabase.Connectstring))
                {

                    string sql = "update  ly_inma0010_drawing_Inspection set remark='" + queryForm.NewValue + "'  where id=" + id;
                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {

                        con.Open();
                        k = cmd.ExecuteNonQuery();
                    }

                }
                SQLDatabase.drawingChangeREC(nowItemNum, 0, "CHANGE_Remark-Inspection-" + filename, SQLDatabase.Nowmemory_rec);
                if (k > 0)
                {
                    MessageBox.Show("修改成功！", "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.ly_inma0010_drawing_InspectionTableAdapter.Fill(this.lYMaterialMange.ly_inma0010_drawing_Inspection, wzbh);
                    lyinma0010drawingInspectionBindingSource.Position = lyinma0010drawingInspectionBindingSource.Find("id", id);
                }
            }
        }

        private void toolStripButton28_Click(object sender, EventArgs e)
        {

            if ("noSet" == nowItemNum)
            {
                return;
            }

            if (SQLDatabase.CheckHaveRight(SQLDatabase.NowUserID, "图纸上传"))
            {

            }
            else
            {
                MessageBox.Show("无图纸上传权限", "注意");
                return;
            }
            if (null != this.ly_inma0010DataGridView.CurrentRow)
            {
                string people = this.ly_inma0010DataGridView.CurrentRow.Cells["work_people"].Value.ToString();
                if (people != SQLDatabase.nowUserName())
                {
                    MessageBox.Show("请负责人：" + people + " 操作", "注意");
                    return;
                }

            }

            string targetName = "\\\\192.168.1.9\\Drawing\\Exe\\程序_" + nowItemNum;

            OpenFileDialog openFile = new OpenFileDialog();

            openFile.Filter = "所有文件|*.*";

            string sourcename = "";
            string filetype = "";
            string filepath = "";
            if (openFile.ShowDialog() == DialogResult.OK)
            {
                filetype = System.IO.Path.GetExtension(openFile.FileName);
                filepath = ("_" + DateTime.Now.ToString("yyyyMMddHHmmss") + filetype);
                sourcename = openFile.FileName;
                targetName = targetName + (filepath);
            }
            else
            {
                return;
            }

            NewFrm.Show(this);
            if (Netfunction.Ping("192.168.1.9"))
            {
                Netfunction.DisConnect("192.168.1.9\\Drawing", "administrator", "jmfuq001.");
                if (Netfunction.Connect("192.168.1.9\\Drawing", "administrator", "jmfuq001."))
                {
                    File.Copy(sourcename, targetName, true);


                    Netfunction.DisConnect("192.168.1.9\\Drawing", "administrator", "jmfuq001.");
                    int k = 0;
                    using (SqlConnection con = new SqlConnection(SQLDatabase.Connectstring))
                    {

                        string sql = @"INSERT INTO [ly_inma0010_drawing_exe] ([itemno] ,[file_path],[input_people])
                                   VALUES   ('" + nowItemNum + "'  ,'程序_" + (nowItemNum + filepath) + "','" + SQLDatabase.nowUserName() + "')";
                        using (SqlCommand cmd = new SqlCommand(sql, con))
                        {
                            con.Open();
                            k = cmd.ExecuteNonQuery();
                        }
                        SQLDatabase.drawingChangeREC(nowItemNum, 0, "UPLOAD-Exe-" + (nowItemNum + filepath), SQLDatabase.Nowmemory_rec);
                    }
                    if (k > 0)
                    {
                        NewFrm.Hide(this);
                        MessageBox.Show("导入成功！", "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.ly_inma0010_drawing_exeTableAdapter.Fill(this.lYMaterialMange.ly_inma0010_drawing_exe, nowItemNum);
                    }
                }
                else
                {
                    NewFrm.Hide(this);
                    MessageBox.Show("连接主机失败,请再次尝试！", "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
            }
            else
            {
                NewFrm.Hide(this);
                MessageBox.Show("Ping主机失败,请再次尝试！", "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void toolStripButton29_Click(object sender, EventArgs e)
        {
            if (null == this.dataGridView5.CurrentRow) return;

            string filename = this.dataGridView5.CurrentRow.Cells["file_path_5"].Value.ToString();
            string sourcename = "\\\\192.168.1.9\\Drawing\\Exe\\" + filename;
            string selePath = "";
            FolderBrowserDialog frmBrowser = new FolderBrowserDialog();
            if (frmBrowser.ShowDialog() == DialogResult.OK)
            {
                selePath = frmBrowser.SelectedPath;
            }
            else
            {
                return;
            }
            string targetName = selePath + "\\" + filename;

            NewFrm.Show(this);

            if (Netfunction.Ping("192.168.1.9"))
            {
                //Netfunction.DisConnect("192.168.1.9\\D$", "administrator", "jmfuq001.");
                Netfunction.DisConnect("192.168.1.9\\Drawing", "administrator", "jmfuq001.");
                if (Netfunction.Connect("192.168.1.9\\Drawing", "administrator", "jmfuq001."))
                {
                    try
                    {
                        if (File.Exists(sourcename))
                        {
                            if (File.Exists(targetName))
                            {
                                FileInfo fileInfo = new FileInfo(targetName);

                                //去掉隐藏属性 
                                //fileInfo.Attributes &= ~FileAttributes.Hidden; 
                                //去掉只读属性 
                                fileInfo.Attributes &= ~FileAttributes.ReadOnly; ;
                                //搜索
                                //相反的操作： 
                                ////增加只读属性 
                                //fileInfo.Attributes |= FileAttributes.ReadOnly; 
                                ////增加隐藏属性 
                                //fileInfo.Attributes |= FileAttributes.Hidden;


                                File.Delete(targetName);
                            }
                            File.Copy(sourcename, targetName, true);
                            NewFrm.Hide(this);
                            MessageBox.Show("下载成功！");
                            SQLDatabase.drawingChangeREC(nowItemNum, 0, "DOWNLOAD-Exe-" + filename, SQLDatabase.Nowmemory_rec);

                            // System.Diagnostics.Process.Start(targetName);
                        }


                        Netfunction.DisConnect("192.168.1.9\\Drawing", "administrator", "jmfuq001.");


                    }
                    catch (Exception ex)
                    {
                        NewFrm.Hide(this);
                        MessageBox.Show(ex.Message.ToString());

                    }
                }
                else
                {
                    NewFrm.Hide(this);
                    MessageBox.Show("连接主机失败！", "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
            }
            else
            {
                NewFrm.Hide(this);
                MessageBox.Show("Ping主机失败！", "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
        }

        private void toolStripButton30_Click(object sender, EventArgs e)
        {
            if (this.dataGridView5.CurrentRow == null) return;

            if (SQLDatabase.CheckHaveRight(SQLDatabase.NowUserID, "图纸删除"))
            {
                string sql = "select isnull(drawing_level,0) from T_users  where yhbm='" + SQLDatabase.NowUserID + "'";
                DataTable dt = null;
                using (SqlConnection connection = new SqlConnection(SQLDatabase.Connectstring))
                {

                    SqlDataAdapter adapter = new SqlDataAdapter(sql, connection);
                    DataSet ds = new DataSet();
                    adapter.Fill(ds);
                    dt = ds.Tables[0];
                }
                if (dt.Rows.Count > 0)
                {
                    if (int.Parse(dt.Rows[0][0].ToString()) < 4)
                    {
                        MessageBox.Show("权限低于4级无法删除", "注意");
                    }
                }
            }
            else
            {
                MessageBox.Show("无图纸删除权限", "注意");
                return;
            }

            //if (null != this.dataGridView5.CurrentRow)
            //{
            //    string people = this.dataGridView5.CurrentRow.Cells["work_people"].Value.ToString();
            //    if (people != SQLDatabase.nowUserName())
            //    {
            //        MessageBox.Show("请负责人：" + people + " 操作", "注意");
            //        return;
            //    }

            //}
            if (null != this.dataGridView5.CurrentRow)
            {


                string id = this.dataGridView5.CurrentRow.Cells["id_exe"].Value.ToString();
                string filename = this.dataGridView5.CurrentRow.Cells["file_path_5"].Value.ToString();
                string wzbh = this.dataGridView5.CurrentRow.Cells["itemno_5"].Value.ToString();
                int k = 0;
                using (SqlConnection con = new SqlConnection(SQLDatabase.Connectstring))
                {
                    string sql = "delete from ly_inma0010_drawing_exe where id=" + id;
                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {

                        con.Open();
                        k = cmd.ExecuteNonQuery();
                    }


                }
                SQLDatabase.drawingChangeREC(nowItemNum, 0, "DELETE_-Exe-" + filename, SQLDatabase.Nowmemory_rec);
                if (k > 0)
                {

                    MessageBox.Show("删除成功！", "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.ly_inma0010_drawing_exeTableAdapter.Fill(this.lYMaterialMange.ly_inma0010_drawing_exe, wzbh);
                }
            }
        }

        private void toolStripButton31_Click(object sender, EventArgs e)
        {
            if (this.dataGridView5.CurrentRow == null) return;

            if (SQLDatabase.CheckHaveRight(SQLDatabase.NowUserID, "图纸级别设置"))
            {

            }
            else
            {
                string people = this.dataGridView5.CurrentRow.Cells["input_people_5"].Value.ToString();
                if (people != SQLDatabase.nowUserName())
                {
                    MessageBox.Show("请上传人：" + people + " 操作", "注意");
                    return;
                }
            }

            string filename = this.dataGridView5.CurrentRow.Cells["file_path_5"].Value.ToString();

            ChangeValue queryForm = new ChangeValue();

            queryForm.OldValue = this.dataGridView5.CurrentRow.Cells["level_5"].Value.ToString();
            queryForm.NewValue = "";
            queryForm.ChangeMode = "value";
            queryForm.ShowDialog();


            if (queryForm.NewValue != "")
            {
                string wzbh = this.dataGridView5.CurrentRow.Cells["itemno_5"].Value.ToString();
                string id = this.dataGridView5.CurrentRow.Cells["id_exe"].Value.ToString();
                int k = 0;
                using (SqlConnection con = new SqlConnection(SQLDatabase.Connectstring))
                {

                    string sql = "update  ly_inma0010_drawing_exe set level=" + queryForm.NewValue + "  where id=" + id;
                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {

                        con.Open();
                        k = cmd.ExecuteNonQuery();
                    }

                }
                SQLDatabase.drawingChangeREC(nowItemNum, 0, "CHANGE_LEV-Exe-" + filename, SQLDatabase.Nowmemory_rec);
                if (k > 0)
                {
                    MessageBox.Show("修改成功！", "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.ly_inma0010_drawing_exeTableAdapter.Fill(this.lYMaterialMange.ly_inma0010_drawing_exe, wzbh);
                    lyinma0010drawingexeBindingSource.Position = lyinma0010drawingexeBindingSource.Find("id", id);
                }
            }
        }

        private void toolStripButton32_Click(object sender, EventArgs e)
        {
            if (this.dataGridView5.CurrentRow == null) return;
            if (SQLDatabase.CheckHaveRight(SQLDatabase.NowUserID, "图纸级别设置"))
            {

            }
            else
            {
                string people = this.dataGridView5.CurrentRow.Cells["input_people_5"].Value.ToString();
                if (people != SQLDatabase.nowUserName())
                {
                    MessageBox.Show("请上传人：" + people + " 操作", "注意");
                    return;
                }
            }
            string filename = this.dataGridView5.CurrentRow.Cells["file_path_5"].Value.ToString();

            ChangeValue queryForm = new ChangeValue();

            queryForm.OldValue = this.dataGridView5.CurrentRow.Cells["remark_5"].Value.ToString();
            queryForm.NewValue = "";
            queryForm.ChangeMode = "longstring";
            queryForm.ShowDialog();


            if (queryForm.NewValue != "")
            {
                string wzbh = this.dataGridView5.CurrentRow.Cells["itemno_5"].Value.ToString();
                string id = this.dataGridView5.CurrentRow.Cells["id_exe"].Value.ToString();
                int k = 0;
                using (SqlConnection con = new SqlConnection(SQLDatabase.Connectstring))
                {

                    string sql = "update  ly_inma0010_drawing_exe set remark='" + queryForm.NewValue + "'  where id=" + id;
                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {

                        con.Open();
                        k = cmd.ExecuteNonQuery();
                    }

                }
                SQLDatabase.drawingChangeREC(nowItemNum, 0, "CHANGE_Remark-Exe-" + filename, SQLDatabase.Nowmemory_rec);
                if (k > 0)
                {
                    MessageBox.Show("修改成功！", "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.ly_inma0010_drawing_exeTableAdapter.Fill(this.lYMaterialMange.ly_inma0010_drawing_exe, wzbh);
                    lyinma0010drawingexeBindingSource.Position = lyinma0010drawingexeBindingSource.Find("id", id);
                }
            }
        }

        private void toolStripButton33_Click(object sender, EventArgs e)
        {

            if ("noSet" == nowItemNum)
            {
                return;
            }

            if (SQLDatabase.CheckHaveRight(SQLDatabase.NowUserID, "图纸上传"))
            {

            }
            else
            {
                MessageBox.Show("无图纸上传权限", "注意");
                return;
            }
            if (null != this.ly_inma0010DataGridView.CurrentRow)
            {
                string people = this.ly_inma0010DataGridView.CurrentRow.Cells["work_people"].Value.ToString();
                if (people != SQLDatabase.nowUserName())
                {
                    MessageBox.Show("请负责人：" + people + " 操作", "注意");
                    return;
                }

            }

            string targetName = "\\\\192.168.1.9\\Drawing\\Code\\代码_" + nowItemNum;

            OpenFileDialog openFile = new OpenFileDialog();

            openFile.Filter = "所有文件|*.*";

            string sourcename = "";
            string filetype = "";
            string filepath = "";
            if (openFile.ShowDialog() == DialogResult.OK)
            {
                filetype = System.IO.Path.GetExtension(openFile.FileName);
                filepath = ("_" + DateTime.Now.ToString("yyyyMMddHHmmss") + filetype);
                sourcename = openFile.FileName;
                targetName = targetName + (filepath);
            }
            else
            {
                return;
            }

            NewFrm.Show(this);
            if (Netfunction.Ping("192.168.1.9"))
            {
                Netfunction.DisConnect("192.168.1.9\\Drawing", "administrator", "jmfuq001.");
                if (Netfunction.Connect("192.168.1.9\\Drawing", "administrator", "jmfuq001."))
                {
                    File.Copy(sourcename, targetName, true);


                    Netfunction.DisConnect("192.168.1.9\\Drawing", "administrator", "jmfuq001.");
                    int k = 0;
                    using (SqlConnection con = new SqlConnection(SQLDatabase.Connectstring))
                    {

                        string sql = @"INSERT INTO [ly_inma0010_drawing_code] ([itemno] ,[file_path],[input_people] )
                                   VALUES   ('" + nowItemNum + "'  ,'代码_" + (nowItemNum + filepath) + "','" + SQLDatabase.nowUserName() + "')";
                        using (SqlCommand cmd = new SqlCommand(sql, con))
                        {
                            con.Open();
                            k = cmd.ExecuteNonQuery();
                        }
                        SQLDatabase.drawingChangeREC(nowItemNum, 0, "UPLOAD-code-" + (nowItemNum + filepath), SQLDatabase.Nowmemory_rec);
                    }
                    if (k > 0)
                    {
                        NewFrm.Hide(this);
                        MessageBox.Show("导入成功！", "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.ly_inma0010_drawing_codeTableAdapter.Fill(this.lYMaterialMange.ly_inma0010_drawing_code, nowItemNum);
                    }
                }
                else
                {
                    NewFrm.Hide(this);
                    MessageBox.Show("连接主机失败,请再次尝试！", "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
            }
            else
            {
                NewFrm.Hide(this);
                MessageBox.Show("Ping主机失败,请再次尝试！", "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void toolStripButton34_Click(object sender, EventArgs e)
        {
            if (null == this.dataGridView6.CurrentRow) return;

            string filename = this.dataGridView6.CurrentRow.Cells["file_path_6"].Value.ToString();
            string sourcename = "\\\\192.168.1.9\\Drawing\\Code\\" + filename;
            string selePath = "";
            FolderBrowserDialog frmBrowser = new FolderBrowserDialog();
            if (frmBrowser.ShowDialog() == DialogResult.OK)
            {
                selePath = frmBrowser.SelectedPath;
            }
            else
            {
                return;
            }
            string targetName = selePath + "\\" + filename;

            NewFrm.Show(this);

            if (Netfunction.Ping("192.168.1.9"))
            {
                //Netfunction.DisConnect("192.168.1.9\\D$", "administrator", "jmfuq001.");
                Netfunction.DisConnect("192.168.1.9\\Drawing", "administrator", "jmfuq001.");
                if (Netfunction.Connect("192.168.1.9\\Drawing", "administrator", "jmfuq001."))
                {
                    try
                    {
                        if (File.Exists(sourcename))
                        {
                            if (File.Exists(targetName))
                            {
                                FileInfo fileInfo = new FileInfo(targetName);

                                //去掉隐藏属性 
                                //fileInfo.Attributes &= ~FileAttributes.Hidden; 
                                //去掉只读属性 
                                fileInfo.Attributes &= ~FileAttributes.ReadOnly; ;
                                //搜索
                                //相反的操作： 
                                ////增加只读属性 
                                //fileInfo.Attributes |= FileAttributes.ReadOnly; 
                                ////增加隐藏属性 
                                //fileInfo.Attributes |= FileAttributes.Hidden;


                                File.Delete(targetName);
                            }
                            File.Copy(sourcename, targetName, true);
                            NewFrm.Hide(this);
                            MessageBox.Show("下载成功！");
                            SQLDatabase.drawingChangeREC(nowItemNum, 0, "DOWNLOAD-code-" + filename, SQLDatabase.Nowmemory_rec);

                            // System.Diagnostics.Process.Start(targetName);
                        }


                        Netfunction.DisConnect("192.168.1.9\\Drawing", "administrator", "jmfuq001.");


                    }
                    catch (Exception ex)
                    {
                        NewFrm.Hide(this);
                        MessageBox.Show(ex.Message.ToString());

                    }
                }
                else
                {
                    NewFrm.Hide(this);
                    MessageBox.Show("连接主机失败！", "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
            }
            else
            {
                NewFrm.Hide(this);
                MessageBox.Show("Ping主机失败！", "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
        }

        private void toolStripButton35_Click(object sender, EventArgs e)
        {
            if (this.dataGridView6.CurrentRow == null) return;
            if (SQLDatabase.CheckHaveRight(SQLDatabase.NowUserID, "图纸删除"))
            {
                string sql = "select isnull(drawing_level,0) from T_users  where yhbm='" + SQLDatabase.NowUserID + "'";
                DataTable dt = null;
                using (SqlConnection connection = new SqlConnection(SQLDatabase.Connectstring))
                {

                    SqlDataAdapter adapter = new SqlDataAdapter(sql, connection);
                    DataSet ds = new DataSet();
                    adapter.Fill(ds);
                    dt = ds.Tables[0];
                }
                if (dt.Rows.Count > 0)
                {
                    if (int.Parse(dt.Rows[0][0].ToString()) < 4)
                    {
                        MessageBox.Show("权限低于4级无法删除", "注意");
                    }
                }
            }
            else
            {
                MessageBox.Show("无图纸删除权限", "注意");
                return;
            }

            //if (null != this.dataGridView6.CurrentRow)
            //{
            //    string people = this.dataGridView6.CurrentRow.Cells["work_people"].Value.ToString();
            //    if (people != SQLDatabase.nowUserName())
            //    {
            //        MessageBox.Show("请负责人：" + people + " 操作", "注意");
            //        return;
            //    }

            //}
            if (null != this.dataGridView6.CurrentRow)
            {


                string id = this.dataGridView6.CurrentRow.Cells["id_code"].Value.ToString();
                string filename = this.dataGridView6.CurrentRow.Cells["file_path_6"].Value.ToString();
                string wzbh = this.dataGridView6.CurrentRow.Cells["itemno_6"].Value.ToString();
                int k = 0;
                using (SqlConnection con = new SqlConnection(SQLDatabase.Connectstring))
                {
                    string sql = "delete from ly_inma0010_drawing_code where id=" + id;
                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {

                        con.Open();
                        k = cmd.ExecuteNonQuery();
                    }


                }
                SQLDatabase.drawingChangeREC(nowItemNum, 0, "DELETE_-code-" + filename, SQLDatabase.Nowmemory_rec);
                if (k > 0)
                {

                    MessageBox.Show("删除成功！", "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.ly_inma0010_drawing_codeTableAdapter.Fill(this.lYMaterialMange.ly_inma0010_drawing_code, wzbh);
                }
            }
        }

        private void toolStripButton36_Click(object sender, EventArgs e)
        {
            if (this.dataGridView6.CurrentRow == null) return;
            if (SQLDatabase.CheckHaveRight(SQLDatabase.NowUserID, "图纸级别设置"))
            {

            }
            else
            {
                string people = this.dataGridView6.CurrentRow.Cells["input_people_6"].Value.ToString();
                if (people != SQLDatabase.nowUserName())
                {
                    MessageBox.Show("请上传人：" + people + " 操作", "注意");
                    return;
                }
            }


            string filename = this.dataGridView6.CurrentRow.Cells["file_path_6"].Value.ToString();

            ChangeValue queryForm = new ChangeValue();

            queryForm.OldValue = this.dataGridView6.CurrentRow.Cells["level_6"].Value.ToString();
            queryForm.NewValue = "";
            queryForm.ChangeMode = "value";
            queryForm.ShowDialog();


            if (queryForm.NewValue != "")
            {
                string wzbh = this.dataGridView6.CurrentRow.Cells["itemno_6"].Value.ToString();
                string id = this.dataGridView6.CurrentRow.Cells["id_code"].Value.ToString();
                int k = 0;
                using (SqlConnection con = new SqlConnection(SQLDatabase.Connectstring))
                {

                    string sql = "update  ly_inma0010_drawing_code set level=" + queryForm.NewValue + "  where id=" + id;
                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {

                        con.Open();
                        k = cmd.ExecuteNonQuery();
                    }

                }
                SQLDatabase.drawingChangeREC(nowItemNum, 0, "CHANGE_LEV-code-" + filename, SQLDatabase.Nowmemory_rec);
                if (k > 0)
                {
                    MessageBox.Show("修改成功！", "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.ly_inma0010_drawing_codeTableAdapter.Fill(this.lYMaterialMange.ly_inma0010_drawing_code, wzbh);
                    lyinma0010drawingcodeBindingSource.Position = lyinma0010drawingcodeBindingSource.Find("id", id);
                }
            }
        }

        private void toolStripButton37_Click(object sender, EventArgs e)
        {
            if (this.dataGridView6.CurrentRow == null) return;
            if (SQLDatabase.CheckHaveRight(SQLDatabase.NowUserID, "图纸级别设置"))
            {

            }
            else
            {
                string people = this.dataGridView6.CurrentRow.Cells["input_people_6"].Value.ToString();
                if (people != SQLDatabase.nowUserName())
                {
                    MessageBox.Show("请上传人：" + people + " 操作", "注意");
                    return;
                }
            }
            string filename = this.dataGridView6.CurrentRow.Cells["file_path_6"].Value.ToString();

            ChangeValue queryForm = new ChangeValue();

            queryForm.OldValue = this.dataGridView6.CurrentRow.Cells["remark_6"].Value.ToString();
            queryForm.NewValue = "";
            queryForm.ChangeMode = "longstring";
            queryForm.ShowDialog();


            if (queryForm.NewValue != "")
            {
                string wzbh = this.dataGridView6.CurrentRow.Cells["itemno_6"].Value.ToString();
                string id = this.dataGridView6.CurrentRow.Cells["id_code"].Value.ToString();
                int k = 0;
                using (SqlConnection con = new SqlConnection(SQLDatabase.Connectstring))
                {

                    string sql = "update  ly_inma0010_drawing_code set remark='" + queryForm.NewValue + "'  where id=" + id;
                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {

                        con.Open();
                        k = cmd.ExecuteNonQuery();
                    }

                }
                SQLDatabase.drawingChangeREC(nowItemNum, 0, "CHANGE_Remark-code-" + filename, SQLDatabase.Nowmemory_rec);
                if (k > 0)
                {
                    MessageBox.Show("修改成功！", "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.ly_inma0010_drawing_codeTableAdapter.Fill(this.lYMaterialMange.ly_inma0010_drawing_code, wzbh);
                    lyinma0010drawingcodeBindingSource.Position = lyinma0010drawingcodeBindingSource.Find("id", id);
                }
            }
        }

        private void toolStripButton38_Click(object sender, EventArgs e)
        {

            if ("noSet" == nowItemNum)
            {
                return;
            }

            if (SQLDatabase.CheckHaveRight(SQLDatabase.NowUserID, "图纸上传"))
            {

            }
            else
            {
                MessageBox.Show("无图纸上传权限", "注意");
                return;
            }
            if (null != this.ly_inma0010DataGridView.CurrentRow)
            {
                string people = this.ly_inma0010DataGridView.CurrentRow.Cells["work_people"].Value.ToString();
                if (people != SQLDatabase.nowUserName())
                {
                    MessageBox.Show("请负责人：" + people + " 操作", "注意");
                    return;
                }

            }

            string targetName = "\\\\192.168.1.9\\Drawing\\Book\\说明书_" + nowItemNum;

            OpenFileDialog openFile = new OpenFileDialog();

            openFile.Filter = "所有文件|*.*";

            string sourcename = "";
            string filetype = "";
            string filepath = "";
            if (openFile.ShowDialog() == DialogResult.OK)
            {
                filetype = System.IO.Path.GetExtension(openFile.FileName);
                filepath = ("_" + DateTime.Now.ToString("yyyyMMddHHmmss") + filetype);
                sourcename = openFile.FileName;
                targetName = targetName + (filepath);
            }
            else
            {
                return;
            }


            if (Netfunction.Ping("192.168.1.9"))
            {
                Netfunction.DisConnect("192.168.1.9\\Drawing", "administrator", "jmfuq001.");
                if (Netfunction.Connect("192.168.1.9\\Drawing", "administrator", "jmfuq001."))
                {
                    File.Copy(sourcename, targetName, true);


                    Netfunction.DisConnect("192.168.1.9\\Drawing", "administrator", "jmfuq001.");
                    int k = 0;
                    using (SqlConnection con = new SqlConnection(SQLDatabase.Connectstring))
                    {

                        string sql = @"INSERT INTO [ly_inma0010_drawing_book] ([itemno] ,[file_path],[input_people] )
                                   VALUES   ('" + nowItemNum + "'  ,'说明书_" + (nowItemNum + filepath) + "','" + SQLDatabase.nowUserName() + "')";
                        using (SqlCommand cmd = new SqlCommand(sql, con))
                        {
                            con.Open();
                            k = cmd.ExecuteNonQuery();
                        }
                        SQLDatabase.drawingChangeREC(nowItemNum, 0, "UPLOAD-book-" + (nowItemNum + filepath), SQLDatabase.Nowmemory_rec);
                    }
                    if (k > 0)
                    {

                        MessageBox.Show("导入成功！", "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.ly_inma0010_drawing_bookTableAdapter.Fill(this.lYMaterialMange.ly_inma0010_drawing_book, nowItemNum);
                    }
                }
                else
                {

                    MessageBox.Show("连接主机失败,请再次尝试！", "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
            }
            else
            {
                MessageBox.Show("Ping主机失败,请再次尝试！", "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void toolStripButton39_Click(object sender, EventArgs e)
        {
            if (null == this.dataGridView7.CurrentRow) return;

            string filename = this.dataGridView7.CurrentRow.Cells["file_path_7"].Value.ToString();
            string sourcename = "\\\\192.168.1.9\\Drawing\\Book\\" + filename;
            string selePath = "";
            FolderBrowserDialog frmBrowser = new FolderBrowserDialog();
            if (frmBrowser.ShowDialog() == DialogResult.OK)
            {
                selePath = frmBrowser.SelectedPath;
            }
            else
            {
                return;
            }
            string targetName = selePath + "\\" + filename;

            ////////////////////////////////////////

            if (Netfunction.Ping("192.168.1.9"))
            {
                //Netfunction.DisConnect("192.168.1.9\\D$", "administrator", "jmfuq001.");
                Netfunction.DisConnect("192.168.1.9\\Drawing", "administrator", "jmfuq001.");
                if (Netfunction.Connect("192.168.1.9\\Drawing", "administrator", "jmfuq001."))
                {
                    try
                    {
                        if (File.Exists(sourcename))
                        {
                            if (File.Exists(targetName))
                            {
                                FileInfo fileInfo = new FileInfo(targetName);

                                //去掉隐藏属性 
                                //fileInfo.Attributes &= ~FileAttributes.Hidden; 
                                //去掉只读属性 
                                fileInfo.Attributes &= ~FileAttributes.ReadOnly; ;
                                //搜索
                                //相反的操作： 
                                ////增加只读属性 
                                //fileInfo.Attributes |= FileAttributes.ReadOnly; 
                                ////增加隐藏属性 
                                //fileInfo.Attributes |= FileAttributes.Hidden;


                                File.Delete(targetName);
                            }
                            File.Copy(sourcename, targetName, true);
                            MessageBox.Show("下载成功！");
                            SQLDatabase.drawingChangeREC(nowItemNum, 0, "DOWNLOAD-book-" + filename, SQLDatabase.Nowmemory_rec);

                            // System.Diagnostics.Process.Start(targetName);
                        }


                        Netfunction.DisConnect("192.168.1.9\\Drawing", "administrator", "jmfuq001.");


                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message.ToString());

                    }
                }
                else
                {
                    MessageBox.Show("连接主机失败！", "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
            }
        }

        private void toolStripButton40_Click(object sender, EventArgs e)
        {
            if (this.dataGridView7.CurrentRow == null) return;
            if (SQLDatabase.CheckHaveRight(SQLDatabase.NowUserID, "图纸删除"))
            {
                string sql = "select isnull(drawing_level,0) from T_users  where yhbm='" + SQLDatabase.NowUserID + "'";
                DataTable dt = null;
                using (SqlConnection connection = new SqlConnection(SQLDatabase.Connectstring))
                {

                    SqlDataAdapter adapter = new SqlDataAdapter(sql, connection);
                    DataSet ds = new DataSet();
                    adapter.Fill(ds);
                    dt = ds.Tables[0];
                }
                if (dt.Rows.Count > 0)
                {
                    if (int.Parse(dt.Rows[0][0].ToString()) < 4)
                    {
                        MessageBox.Show("权限低于4级无法删除", "注意");
                    }
                }
            }
            else
            {
                MessageBox.Show("无图纸删除权限", "注意");
                return;
            }

            //if (null != this.dataGridView7.CurrentRow)
            //{
            //    string people = this.dataGridView7.CurrentRow.Cells["work_people"].Value.ToString();
            //    if (people != SQLDatabase.nowUserName())
            //    {
            //        MessageBox.Show("请负责人：" + people + " 操作", "注意");
            //        return;
            //    }

            //}
            if (null != this.dataGridView7.CurrentRow)
            {


                string id = this.dataGridView7.CurrentRow.Cells["id_book"].Value.ToString();
                string filename = this.dataGridView7.CurrentRow.Cells["file_path_7"].Value.ToString();
                string wzbh = this.dataGridView7.CurrentRow.Cells["itemno_7"].Value.ToString();
                int k = 0;
                using (SqlConnection con = new SqlConnection(SQLDatabase.Connectstring))
                {
                    string sql = "delete from ly_inma0010_drawing_book where id=" + id;
                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {

                        con.Open();
                        k = cmd.ExecuteNonQuery();
                    }


                }
                SQLDatabase.drawingChangeREC(nowItemNum, 0, "DELETE_-book-" + filename, SQLDatabase.Nowmemory_rec);
                if (k > 0)
                {

                    MessageBox.Show("删除成功！", "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.ly_inma0010_drawing_bookTableAdapter.Fill(this.lYMaterialMange.ly_inma0010_drawing_book, wzbh);
                }
            }
        }

        private void toolStripButton41_Click(object sender, EventArgs e)
        {
            if (this.dataGridView7.CurrentRow == null) return;
            if (SQLDatabase.CheckHaveRight(SQLDatabase.NowUserID, "图纸级别设置"))
            {

            }
            else
            {
                string people = this.dataGridView7.CurrentRow.Cells["input_people_7"].Value.ToString();
                if (people != SQLDatabase.nowUserName())
                {
                    MessageBox.Show("请上传人：" + people + " 操作", "注意");
                    return;
                }
            }


            string filename = this.dataGridView7.CurrentRow.Cells["file_path_7"].Value.ToString();

            ChangeValue queryForm = new ChangeValue();

            queryForm.OldValue = this.dataGridView7.CurrentRow.Cells["level_7"].Value.ToString();
            queryForm.NewValue = "";
            queryForm.ChangeMode = "value";
            queryForm.ShowDialog();


            if (queryForm.NewValue != "")
            {
                string wzbh = this.dataGridView7.CurrentRow.Cells["itemno_7"].Value.ToString();
                string id = this.dataGridView7.CurrentRow.Cells["id_book"].Value.ToString();
                int k = 0;
                using (SqlConnection con = new SqlConnection(SQLDatabase.Connectstring))
                {

                    string sql = "update  ly_inma0010_drawing_book set level=" + queryForm.NewValue + "  where id=" + id;
                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {

                        con.Open();
                        k = cmd.ExecuteNonQuery();
                    }

                }
                SQLDatabase.drawingChangeREC(nowItemNum, 0, "CHANGE_LEV-book-" + filename, SQLDatabase.Nowmemory_rec);
                if (k > 0)
                {
                    MessageBox.Show("修改成功！", "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.ly_inma0010_drawing_bookTableAdapter.Fill(this.lYMaterialMange.ly_inma0010_drawing_book, wzbh);
                    lyinma0010drawingbookBindingSource.Position = lyinma0010drawingbookBindingSource.Find("id", id);
                }
            }
        }

        private void toolStripButton42_Click(object sender, EventArgs e)
        {
            if (this.dataGridView7.CurrentRow == null) return;
            if (SQLDatabase.CheckHaveRight(SQLDatabase.NowUserID, "图纸级别设置"))
            {

            }
            else
            {
                string people = this.dataGridView7.CurrentRow.Cells["input_people_7"].Value.ToString();
                if (people != SQLDatabase.nowUserName())
                {
                    MessageBox.Show("请上传人：" + people + " 操作", "注意");
                    return;
                }
            }
            string filename = this.dataGridView7.CurrentRow.Cells["file_path_7"].Value.ToString();

            ChangeValue queryForm = new ChangeValue();

            queryForm.OldValue = this.dataGridView7.CurrentRow.Cells["remark_7"].Value.ToString();
            queryForm.NewValue = "";
            queryForm.ChangeMode = "longstring";
            queryForm.ShowDialog();


            if (queryForm.NewValue != "")
            {
                string wzbh = this.dataGridView7.CurrentRow.Cells["itemno_7"].Value.ToString();
                string id = this.dataGridView7.CurrentRow.Cells["id_book"].Value.ToString();
                int k = 0;
                using (SqlConnection con = new SqlConnection(SQLDatabase.Connectstring))
                {

                    string sql = "update  ly_inma0010_drawing_book set remark='" + queryForm.NewValue + "'  where id=" + id;
                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {

                        con.Open();
                        k = cmd.ExecuteNonQuery();
                    }

                }
                SQLDatabase.drawingChangeREC(nowItemNum, 0, "CHANGE_Remark-book-" + filename, SQLDatabase.Nowmemory_rec);
                if (k > 0)
                {
                    MessageBox.Show("修改成功！", "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.ly_inma0010_drawing_bookTableAdapter.Fill(this.lYMaterialMange.ly_inma0010_drawing_book, wzbh);
                    lyinma0010drawingbookBindingSource.Position = lyinma0010drawingbookBindingSource.Find("id", id);
                }
            }
        }

        private void toolStripButton43_Click(object sender, EventArgs e)
        {
            if ("noSet" == nowItemNum)
            {
                return;
            }


            if (SQLDatabase.CheckHaveRight(SQLDatabase.NowUserID, "图纸上传"))
            {

            }
            else
            {
                MessageBox.Show("无图纸上传权限", "注意");
                return;
            }
            if (null != this.ly_inma0010DataGridView.CurrentRow)
            {
                string people = this.ly_inma0010DataGridView.CurrentRow.Cells["work_people"].Value.ToString();
                if (people != SQLDatabase.nowUserName())
                {
                    MessageBox.Show("请负责人：" + people + " 操作", "注意");
                    return;
                }

            }

            string targetName = "\\\\192.168.1.9\\Drawing\\Mechanical\\机械图_" + nowItemNum;

            OpenFileDialog openFile = new OpenFileDialog();

            openFile.Filter = "所有文件|*.*";

            string sourcename = "";
            string filetype = "";
            string filepath = "";
            if (openFile.ShowDialog() == DialogResult.OK)
            {
                filetype = System.IO.Path.GetExtension(openFile.FileName);
                filepath = ("_" + DateTime.Now.ToString("yyyyMMddHHmmss") + filetype);
                sourcename = openFile.FileName;
                targetName = targetName + (filepath);
            }
            else
            {
                return;
            }

            NewFrm.Show(this);

            if (Netfunction.Ping("192.168.1.9"))
            {
                Netfunction.DisConnect("192.168.1.9\\Drawing", "administrator", "jmfuq001.");
                if (Netfunction.Connect("192.168.1.9\\Drawing", "administrator", "jmfuq001."))
                {
                    File.Copy(sourcename, targetName, true);


                    Netfunction.DisConnect("192.168.1.9\\Drawing", "administrator", "jmfuq001.");
                    int k = 0;
                    using (SqlConnection con = new SqlConnection(SQLDatabase.Connectstring))
                    {

                        string sql = @"INSERT INTO [ly_inma0010_drawing_mechanical] ([itemno] ,[file_path],[input_people] )
                                   VALUES   ('" + nowItemNum + "'  ,'机械图_" + (nowItemNum + filepath) + "','" + SQLDatabase.nowUserName() + "')";
                        using (SqlCommand cmd = new SqlCommand(sql, con))
                        {
                            con.Open();
                            k = cmd.ExecuteNonQuery();
                        }
                        SQLDatabase.drawingChangeREC(nowItemNum, 0, "UPLOAD-Mechanical-" + (nowItemNum + filepath), SQLDatabase.Nowmemory_rec);
                    }
                    if (k > 0)
                    {
                        NewFrm.Hide(this);
                        MessageBox.Show("导入成功！", "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.ly_inma0010_drawing_mechanicalTableAdapter.Fill(this.lYMaterialMange.ly_inma0010_drawing_mechanical, nowItemNum);
                    }
                }
                else
                {
                    NewFrm.Hide(this);
                    MessageBox.Show("连接主机失败,请再次尝试！", "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
            }
            else
            {
                NewFrm.Hide(this);
                MessageBox.Show("Ping主机失败,请再次尝试！", "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void toolStripButton44_Click(object sender, EventArgs e)
        {
            if (null == this.dataGridView8.CurrentRow) return;

            string filename = this.dataGridView8.CurrentRow.Cells["file_path8"].Value.ToString();
            string sourcename = "\\\\192.168.1.9\\Drawing\\Mechanical\\" + filename;
            string selePath = "";
            FolderBrowserDialog frmBrowser = new FolderBrowserDialog();
            if (frmBrowser.ShowDialog() == DialogResult.OK)
            {
                selePath = frmBrowser.SelectedPath;
            }
            else
            {
                return;
            }
            string targetName = selePath + "\\" + filename;

            ////////////////////////////////////////
            NewFrm.Show(this);
            if (Netfunction.Ping("192.168.1.9"))
            {
                //Netfunction.DisConnect("192.168.1.9\\D$", "administrator", "jmfuq001.");
                Netfunction.DisConnect("192.168.1.9\\Drawing", "administrator", "jmfuq001.");
                if (Netfunction.Connect("192.168.1.9\\Drawing", "administrator", "jmfuq001."))
                {
                    try
                    {
                        if (File.Exists(sourcename))
                        {
                            if (File.Exists(targetName))
                            {
                                FileInfo fileInfo = new FileInfo(targetName);

                                //去掉隐藏属性 
                                //fileInfo.Attributes &= ~FileAttributes.Hidden; 
                                //去掉只读属性 
                                fileInfo.Attributes &= ~FileAttributes.ReadOnly; ;
                                //搜索
                                //相反的操作： 
                                ////增加只读属性 
                                //fileInfo.Attributes |= FileAttributes.ReadOnly; 
                                ////增加隐藏属性 
                                //fileInfo.Attributes |= FileAttributes.Hidden;


                                File.Delete(targetName);
                            }
                            File.Copy(sourcename, targetName, true);
                            NewFrm.Hide(this);
                            MessageBox.Show("下载成功！");
                            SQLDatabase.drawingChangeREC(nowItemNum, 0, "DOWNLOAD-Mechanical-" + filename, SQLDatabase.Nowmemory_rec);

                            // System.Diagnostics.Process.Start(targetName);
                        }


                        Netfunction.DisConnect("192.168.1.9\\Drawing", "administrator", "jmfuq001.");


                    }
                    catch (Exception ex)
                    {
                        NewFrm.Hide(this);
                        MessageBox.Show(ex.Message.ToString());

                    }
                }
                else
                {
                    NewFrm.Hide(this);
                    MessageBox.Show("连接主机失败！", "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
            }
            else
            {
                NewFrm.Hide(this);
                MessageBox.Show("Ping主机失败！", "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }

        }

        private void toolStripButton45_Click(object sender, EventArgs e)
        {

            if (this.dataGridView8.CurrentRow == null) return;
            if (SQLDatabase.CheckHaveRight(SQLDatabase.NowUserID, "图纸删除"))
            {
                string sql = "select isnull(drawing_level,0) from T_users  where yhbm='" + SQLDatabase.NowUserID + "'";
                DataTable dt = null;
                using (SqlConnection connection = new SqlConnection(SQLDatabase.Connectstring))
                {

                    SqlDataAdapter adapter = new SqlDataAdapter(sql, connection);
                    DataSet ds = new DataSet();
                    adapter.Fill(ds);
                    dt = ds.Tables[0];
                }
                if (dt.Rows.Count > 0)
                {
                    if (int.Parse(dt.Rows[0][0].ToString()) < 4)
                    {
                        MessageBox.Show("权限低于4级无法删除", "注意");
                    }
                }
            }
            else
            {
                MessageBox.Show("无图纸删除权限", "注意");
                return;
            }

            //if (null != this.dataGridView8.CurrentRow)
            //{
            //    string people = this.dataGridView8.CurrentRow.Cells["work_people"].Value.ToString();
            //    if (people != SQLDatabase.nowUserName())
            //    {
            //        MessageBox.Show("请负责人：" + people + " 操作", "注意");
            //        return;
            //    }

            //}
            if (null != this.dataGridView8.CurrentRow)
            {
                 

                string id = this.dataGridView8.CurrentRow.Cells["id_jx"].Value.ToString();
                string filename = this.dataGridView8.CurrentRow.Cells["file_path8"].Value.ToString();
                string wzbh = this.dataGridView8.CurrentRow.Cells["itemno8"].Value.ToString();
                int k = 0;
                using (SqlConnection con = new SqlConnection(SQLDatabase.Connectstring))
                {
                    string sql = "delete from ly_inma0010_drawing_mechanical where id=" + id;
                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {

                        con.Open();
                        k = cmd.ExecuteNonQuery();
                    }


                }
                SQLDatabase.drawingChangeREC(nowItemNum, 0, "DELETE_-Mechanical-" + filename, SQLDatabase.Nowmemory_rec);
                if (k > 0)
                {

                    MessageBox.Show("删除成功！", "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.ly_inma0010_drawing_mechanicalTableAdapter.Fill(this.lYMaterialMange.ly_inma0010_drawing_mechanical, wzbh);
                }
            }
        }

        private void toolStripButton46_Click(object sender, EventArgs e)
        {

            if (this.dataGridView8.CurrentRow == null) return;
            if (SQLDatabase.CheckHaveRight(SQLDatabase.NowUserID, "图纸级别设置"))
            {

            }
            else
            {
                string people = this.dataGridView8.CurrentRow.Cells["input_people8"].Value.ToString();
                if (people != SQLDatabase.nowUserName())
                {
                    MessageBox.Show("请上传人：" + people + " 操作", "注意");
                    return;
                }
            }


            string filename = this.dataGridView8.CurrentRow.Cells["file_path8"].Value.ToString();

            ChangeValue queryForm = new ChangeValue();

            queryForm.OldValue = this.dataGridView8.CurrentRow.Cells["level8"].Value.ToString();
            queryForm.NewValue = "";
            queryForm.ChangeMode = "value";
            queryForm.ShowDialog();


            if (queryForm.NewValue != "")
            {
                string wzbh = this.dataGridView8.CurrentRow.Cells["itemno8"].Value.ToString();
                string id = this.dataGridView8.CurrentRow.Cells["id_jx"].Value.ToString();
                int k = 0;
                using (SqlConnection con = new SqlConnection(SQLDatabase.Connectstring))
                {

                    string sql = "update  ly_inma0010_drawing_mechanical set level=" + queryForm.NewValue + "  where id=" + id;
                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {

                        con.Open();
                        k = cmd.ExecuteNonQuery();
                    }

                }
                SQLDatabase.drawingChangeREC(nowItemNum, 0, "CHANGE_LEV-Mechanical-" + filename, SQLDatabase.Nowmemory_rec);
                if (k > 0)
                {
                    MessageBox.Show("修改成功！", "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.ly_inma0010_drawing_mechanicalTableAdapter.Fill(this.lYMaterialMange.ly_inma0010_drawing_mechanical, wzbh);
                    lyinma0010drawingmechanicalBindingSource.Position = lyinma0010drawingmechanicalBindingSource.Find("id", id);
                }
            }
        }

        private void toolStripButton47_Click(object sender, EventArgs e)
        {

            if (this.dataGridView8.CurrentRow == null) return;
            if (SQLDatabase.CheckHaveRight(SQLDatabase.NowUserID, "图纸级别设置"))
            {

            }
            else
            {
                string people = this.dataGridView8.CurrentRow.Cells["input_people8"].Value.ToString();
                if (people != SQLDatabase.nowUserName())
                {
                    MessageBox.Show("请上传人：" + people + " 操作", "注意");
                    return;
                }
            }
            string filename = this.dataGridView8.CurrentRow.Cells["file_path8"].Value.ToString();

            ChangeValue queryForm = new ChangeValue();

            queryForm.OldValue = this.dataGridView8.CurrentRow.Cells["remark8"].Value.ToString();
            queryForm.NewValue = "";
            queryForm.ChangeMode = "longstring";
            queryForm.ShowDialog();


            if (queryForm.NewValue != "")
            {
                string wzbh = this.dataGridView8.CurrentRow.Cells["itemno8"].Value.ToString();
                string id = this.dataGridView8.CurrentRow.Cells["id_jx"].Value.ToString();
                int k = 0;
                using (SqlConnection con = new SqlConnection(SQLDatabase.Connectstring))
                {

                    string sql = "update  ly_inma0010_drawing_mechanical set remark='" + queryForm.NewValue + "'  where id=" + id;
                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {

                        con.Open();
                        k = cmd.ExecuteNonQuery();
                    }

                }
                SQLDatabase.drawingChangeREC(nowItemNum, 0, "CHANGE_Remark-Mechanical-" + filename, SQLDatabase.Nowmemory_rec);
                if (k > 0)
                {
                    MessageBox.Show("修改成功！", "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.ly_inma0010_drawing_mechanicalTableAdapter.Fill(this.lYMaterialMange.ly_inma0010_drawing_mechanical, wzbh);
                    lyinma0010drawingmechanicalBindingSource.Position = lyinma0010drawingmechanicalBindingSource.Find("id", id);
                }
            }
        }

        private void toolStripButton48_Click(object sender, EventArgs e)
        {
            if ("noSet" == nowItemNum)
            {
                return;
            }

            if (SQLDatabase.CheckHaveRight(SQLDatabase.NowUserID, "图纸上传"))
            {

            }
            else
            {
                MessageBox.Show("无图纸上传权限", "注意");
                return;
            }
            if (null != this.ly_inma0010DataGridView.CurrentRow)
            {
                string people = this.ly_inma0010DataGridView.CurrentRow.Cells["work_people"].Value.ToString();
                if (people != SQLDatabase.nowUserName())
                {
                    MessageBox.Show("请负责人：" + people + " 操作", "注意");
                    return;
                }

            }

            string targetName = "\\\\192.168.1.9\\Drawing\\BOM\\外协BOM_" + nowItemNum;

            OpenFileDialog openFile = new OpenFileDialog();

            openFile.Filter = "所有文件|*.*";

            string sourcename = "";
            string filetype = "";
            string filepath = "";
            if (openFile.ShowDialog() == DialogResult.OK)
            {
                filetype = System.IO.Path.GetExtension(openFile.FileName);
                filepath = ("_" + DateTime.Now.ToString("yyyyMMddHHmmss") + filetype);
                sourcename = openFile.FileName;
                targetName = targetName + (filepath);
            }
            else
            {
                return;
            }


            if (Netfunction.Ping("192.168.1.9"))
            {
                Netfunction.DisConnect("192.168.1.9\\Drawing", "administrator", "jmfuq001.");
                if (Netfunction.Connect("192.168.1.9\\Drawing", "administrator", "jmfuq001."))
                {
                    File.Copy(sourcename, targetName, true);


                    Netfunction.DisConnect("192.168.1.9\\Drawing", "administrator", "jmfuq001.");
                    int k = 0;
                    using (SqlConnection con = new SqlConnection(SQLDatabase.Connectstring))
                    {

                        string sql = @"INSERT INTO [ly_inma0010_drawing_bom] ([itemno] ,[file_path],[input_people] )
                                   VALUES   ('" + nowItemNum + "'  ,'外协BOM_" + (nowItemNum + filepath) + "','" + SQLDatabase.nowUserName() + "')";
                        using (SqlCommand cmd = new SqlCommand(sql, con))
                        {
                            con.Open();
                            k = cmd.ExecuteNonQuery();
                        }
                        SQLDatabase.drawingChangeREC(nowItemNum, 0, "UPLOAD-bom-" + (nowItemNum + filepath), SQLDatabase.Nowmemory_rec);
                    }
                    if (k > 0)
                    {

                        MessageBox.Show("导入成功！", "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.ly_inma0010_drawing_bomTableAdapter.Fill(this.lYMaterialMange.ly_inma0010_drawing_bom, nowItemNum);
                    }
                }
                else
                {

                    MessageBox.Show("连接主机失败,请再次尝试！", "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
            }
            else
            {
                MessageBox.Show("Ping主机失败,请再次尝试！", "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void toolStripButton49_Click(object sender, EventArgs e)
        {
            if (null == this.dataGridView9.CurrentRow) return;

            string filename = this.dataGridView9.CurrentRow.Cells["file_path_9"].Value.ToString();
            string sourcename = "\\\\192.168.1.9\\Drawing\\BOM\\" + filename;
            string selePath = "";
            FolderBrowserDialog frmBrowser = new FolderBrowserDialog();
            if (frmBrowser.ShowDialog() == DialogResult.OK)
            {
                selePath = frmBrowser.SelectedPath;
            }
            else
            {
                return;
            }
            string targetName = selePath + "\\" + filename;

            ////////////////////////////////////////

            if (Netfunction.Ping("192.168.1.9"))
            {
                //Netfunction.DisConnect("192.168.1.9\\D$", "administrator", "jmfuq001.");
                Netfunction.DisConnect("192.168.1.9\\Drawing", "administrator", "jmfuq001.");
                if (Netfunction.Connect("192.168.1.9\\Drawing", "administrator", "jmfuq001."))
                {
                    try
                    {
                        if (File.Exists(sourcename))
                        {
                            if (File.Exists(targetName))
                            {
                                FileInfo fileInfo = new FileInfo(targetName);

                                //去掉隐藏属性 
                                //fileInfo.Attributes &= ~FileAttributes.Hidden; 
                                //去掉只读属性 
                                fileInfo.Attributes &= ~FileAttributes.ReadOnly; ;
                                //搜索
                                //相反的操作： 
                                ////增加只读属性 
                                //fileInfo.Attributes |= FileAttributes.ReadOnly; 
                                ////增加隐藏属性 
                                //fileInfo.Attributes |= FileAttributes.Hidden;


                                File.Delete(targetName);
                            }
                            File.Copy(sourcename, targetName, true);
                            MessageBox.Show("下载成功！");
                            SQLDatabase.drawingChangeREC(nowItemNum, 0, "DOWNLOAD-bom-" + filename, SQLDatabase.Nowmemory_rec);

                            // System.Diagnostics.Process.Start(targetName);
                        }


                        Netfunction.DisConnect("192.168.1.9\\Drawing", "administrator", "jmfuq001.");


                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message.ToString());

                    }
                }
                else
                {
                    MessageBox.Show("连接主机失败！", "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
            }
        }

        private void toolStripButton50_Click(object sender, EventArgs e)
        {

            if (this.dataGridView9.CurrentRow == null) return;
            if (SQLDatabase.CheckHaveRight(SQLDatabase.NowUserID, "图纸删除"))
            {
                string sql = "select isnull(drawing_level,0) from T_users  where yhbm='" + SQLDatabase.NowUserID + "'";
                DataTable dt = null;
                using (SqlConnection connection = new SqlConnection(SQLDatabase.Connectstring))
                {

                    SqlDataAdapter adapter = new SqlDataAdapter(sql, connection);
                    DataSet ds = new DataSet();
                    adapter.Fill(ds);
                    dt = ds.Tables[0];
                }
                if (dt.Rows.Count > 0)
                {
                    if (int.Parse(dt.Rows[0][0].ToString()) < 4)
                    {
                        MessageBox.Show("权限低于4级无法删除", "注意");
                    }
                }
            }
            else
            {
                MessageBox.Show("无图纸删除权限", "注意");
                return;
            }

            //if (null != this.dataGridView9.CurrentRow)
            //{
            //    string people = this.dataGridView9.CurrentRow.Cells["work_people"].Value.ToString();
            //    if (people != SQLDatabase.nowUserName())
            //    {
            //        MessageBox.Show("请负责人：" + people + " 操作", "注意");
            //        return;
            //    }

            //}
            if (null != this.dataGridView9.CurrentRow)
            {


                string id = this.dataGridView9.CurrentRow.Cells["id_bom"].Value.ToString();
                string filename = this.dataGridView9.CurrentRow.Cells["file_path_9"].Value.ToString();
                string wzbh = this.dataGridView9.CurrentRow.Cells["itemno_9"].Value.ToString();
                int k = 0;
                using (SqlConnection con = new SqlConnection(SQLDatabase.Connectstring))
                {
                    string sql = "delete from ly_inma0010_drawing_bom where id=" + id;
                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {

                        con.Open();
                        k = cmd.ExecuteNonQuery();
                    }


                }
                SQLDatabase.drawingChangeREC(nowItemNum, 0, "DELETE_-bom-" + filename, SQLDatabase.Nowmemory_rec);
                if (k > 0)
                {

                    MessageBox.Show("删除成功！", "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.ly_inma0010_drawing_bomTableAdapter.Fill(this.lYMaterialMange.ly_inma0010_drawing_bom, wzbh);
                }
            }
        }

        private void toolStripButton51_Click(object sender, EventArgs e)
        {
            if (this.dataGridView9.CurrentRow == null) return;
            if (SQLDatabase.CheckHaveRight(SQLDatabase.NowUserID, "图纸级别设置"))
            {

            }
            else
            {
                string people = this.dataGridView9.CurrentRow.Cells["input_people_9"].Value.ToString();
                if (people != SQLDatabase.nowUserName())
                {
                    MessageBox.Show("请上传人：" + people + " 操作", "注意");
                    return;
                }
            }


            string filename = this.dataGridView9.CurrentRow.Cells["file_path_9"].Value.ToString();

            ChangeValue queryForm = new ChangeValue();

            queryForm.OldValue = this.dataGridView9.CurrentRow.Cells["level_9"].Value.ToString();
            queryForm.NewValue = "";
            queryForm.ChangeMode = "value";
            queryForm.ShowDialog();


            if (queryForm.NewValue != "")
            {
                string wzbh = this.dataGridView9.CurrentRow.Cells["itemno_9"].Value.ToString();
                string id = this.dataGridView9.CurrentRow.Cells["id_bom"].Value.ToString();
                int k = 0;
                using (SqlConnection con = new SqlConnection(SQLDatabase.Connectstring))
                {

                    string sql = "update  ly_inma0010_drawing_bom set level=" + queryForm.NewValue + "  where id=" + id;
                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {

                        con.Open();
                        k = cmd.ExecuteNonQuery();
                    }

                }
                SQLDatabase.drawingChangeREC(nowItemNum, 0, "CHANGE_LEV-bom-" + filename, SQLDatabase.Nowmemory_rec);
                if (k > 0)
                {
                    MessageBox.Show("修改成功！", "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.ly_inma0010_drawing_bomTableAdapter.Fill(this.lYMaterialMange.ly_inma0010_drawing_bom, wzbh);
                    lyinma0010drawingbomBindingSource.Position = lyinma0010drawingbomBindingSource.Find("id", id);
                }
            }
        }

        private void toolStripButton52_Click(object sender, EventArgs e)
        {

            if (this.dataGridView9.CurrentRow == null) return;
            if (SQLDatabase.CheckHaveRight(SQLDatabase.NowUserID, "图纸级别设置"))
            {

            }
            else
            {
                string people = this.dataGridView9.CurrentRow.Cells["input_people_9"].Value.ToString();
                if (people != SQLDatabase.nowUserName())
                {
                    MessageBox.Show("请上传人：" + people + " 操作", "注意");
                    return;
                }
            }
            string filename = this.dataGridView9.CurrentRow.Cells["file_path_9"].Value.ToString();

            ChangeValue queryForm = new ChangeValue();

            queryForm.OldValue = this.dataGridView9.CurrentRow.Cells["remark_9"].Value.ToString();
            queryForm.NewValue = "";
            queryForm.ChangeMode = "longstring";
            queryForm.ShowDialog();


            if (queryForm.NewValue != "")
            {
                string wzbh = this.dataGridView9.CurrentRow.Cells["itemno_9"].Value.ToString();
                string id = this.dataGridView9.CurrentRow.Cells["id_bom"].Value.ToString();
                int k = 0;
                using (SqlConnection con = new SqlConnection(SQLDatabase.Connectstring))
                {

                    string sql = "update  ly_inma0010_drawing_bom set remark='" + queryForm.NewValue + "'  where id=" + id;
                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {

                        con.Open();
                        k = cmd.ExecuteNonQuery();
                    }

                }
                SQLDatabase.drawingChangeREC(nowItemNum, 0, "CHANGE_Remark-bom-" + filename, SQLDatabase.Nowmemory_rec);
                if (k > 0)
                {
                    MessageBox.Show("修改成功！", "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.ly_inma0010_drawing_bomTableAdapter.Fill(this.lYMaterialMange.ly_inma0010_drawing_bom, wzbh);
                    lyinma0010drawingbomBindingSource.Position = lyinma0010drawingbomBindingSource.Find("id", id);
                }
            }
        }

        private void dataGridView3_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

            if ("remark_3" == this.dataGridView3.CurrentCell.OwningColumn.Name)
            {

                if (this.dataGridView3.CurrentRow == null) return;
                if (SQLDatabase.CheckHaveRight(SQLDatabase.NowUserID, "图纸级别设置"))
                {

                }
                else
                {
                    string people = this.dataGridView3.CurrentRow.Cells["input_people_3"].Value.ToString();
                    if (people != SQLDatabase.nowUserName())
                    {
                        MessageBox.Show("请上传人：" + people + " 操作", "注意");
                        return;
                    }
                }
                string filename = this.dataGridView3.CurrentRow.Cells["file_path_3"].Value.ToString();

                ChangeValue queryForm = new ChangeValue();

                queryForm.OldValue = this.dataGridView3.CurrentRow.Cells["remark_3"].Value.ToString();
                queryForm.NewValue = "";
                queryForm.ChangeMode = "longstring";
                queryForm.ShowDialog();


                if (queryForm.NewValue != "")
                {
                    string wzbh = this.dataGridView3.CurrentRow.Cells["itemno_3"].Value.ToString();
                    string id = this.dataGridView3.CurrentRow.Cells["id_process"].Value.ToString();
                    int k = 0;
                    using (SqlConnection con = new SqlConnection(SQLDatabase.Connectstring))
                    {

                        string sql = "update  ly_inma0010_drawing_process set remark='" + queryForm.NewValue + "'  where id=" + id;
                        using (SqlCommand cmd = new SqlCommand(sql, con))
                        {

                            con.Open();
                            k = cmd.ExecuteNonQuery();
                        }

                    }
                    SQLDatabase.drawingChangeREC(nowItemNum, 0, "CHANGE_Remark-Process-" + filename, SQLDatabase.Nowmemory_rec);
                    if (k > 0)
                    {
                        MessageBox.Show("修改成功！", "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.ly_inma0010_drawing_processTableAdapter.Fill(this.lYMaterialMange.ly_inma0010_drawing_process, wzbh);
                        lyinma0010drawingprocessBindingSource.Position = lyinma0010drawingprocessBindingSource.Find("id", id);
                    }
                }
                return;
            }
            if ("level_3" == this.dataGridView3.CurrentCell.OwningColumn.Name)
            {
                if (this.dataGridView3.CurrentRow == null) return;
                if (SQLDatabase.CheckHaveRight(SQLDatabase.NowUserID, "图纸级别设置"))
                {

                }
                else
                {
                    string people = this.dataGridView3.CurrentRow.Cells["input_people_3"].Value.ToString();
                    if (people != SQLDatabase.nowUserName())
                    {
                        MessageBox.Show("请上传人：" + people + " 操作", "注意");
                        return;
                    }
                }


                string filename = this.dataGridView3.CurrentRow.Cells["file_path_3"].Value.ToString();

                ChangeValue queryForm = new ChangeValue();

                queryForm.OldValue = this.dataGridView3.CurrentRow.Cells["level_3"].Value.ToString();
                queryForm.NewValue = "";
                queryForm.ChangeMode = "value";
                queryForm.ShowDialog();


                if (queryForm.NewValue != "")
                {
                    string wzbh = this.dataGridView3.CurrentRow.Cells["itemno_3"].Value.ToString();
                    string id = this.dataGridView3.CurrentRow.Cells["id_process"].Value.ToString();
                    int k = 0;
                    using (SqlConnection con = new SqlConnection(SQLDatabase.Connectstring))
                    {

                        string sql = "update  ly_inma0010_drawing_process set level=" + queryForm.NewValue + "  where id=" + id;
                        using (SqlCommand cmd = new SqlCommand(sql, con))
                        {

                            con.Open();
                            k = cmd.ExecuteNonQuery();
                        }

                    }
                    SQLDatabase.drawingChangeREC(nowItemNum, 0, "CHANGE_LEV-Process-" + filename, SQLDatabase.Nowmemory_rec);
                    if (k > 0)
                    {
                        MessageBox.Show("修改成功！", "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.ly_inma0010_drawing_processTableAdapter.Fill(this.lYMaterialMange.ly_inma0010_drawing_process, wzbh);
                        lyinma0010drawingprocessBindingSource.Position = lyinma0010drawingprocessBindingSource.Find("id", id);
                    }
                }
                return;
            }
        }

        private void dataGridView8_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if ("remark8" == this.dataGridView8.CurrentCell.OwningColumn.Name)
            {
                if (this.dataGridView8.CurrentRow == null) return;
                if (SQLDatabase.CheckHaveRight(SQLDatabase.NowUserID, "图纸级别设置"))
                {

                }
                else
                {
                    string people = this.dataGridView8.CurrentRow.Cells["input_people8"].Value.ToString();
                    if (people != SQLDatabase.nowUserName())
                    {
                        MessageBox.Show("请上传人：" + people + " 操作", "注意");
                        return;
                    }
                }
                string filename = this.dataGridView8.CurrentRow.Cells["file_path8"].Value.ToString();

                ChangeValue queryForm = new ChangeValue();

                queryForm.OldValue = this.dataGridView8.CurrentRow.Cells["remark8"].Value.ToString();
                queryForm.NewValue = "";
                queryForm.ChangeMode = "longstring";
                queryForm.ShowDialog();


                if (queryForm.NewValue != "")
                {
                    string wzbh = this.dataGridView8.CurrentRow.Cells["itemno8"].Value.ToString();
                    string id = this.dataGridView8.CurrentRow.Cells["id_jx"].Value.ToString();
                    int k = 0;
                    using (SqlConnection con = new SqlConnection(SQLDatabase.Connectstring))
                    {

                        string sql = "update  ly_inma0010_drawing_mechanical set remark='" + queryForm.NewValue + "'  where id=" + id;
                        using (SqlCommand cmd = new SqlCommand(sql, con))
                        {

                            con.Open();
                            k = cmd.ExecuteNonQuery();
                        }

                    }
                    SQLDatabase.drawingChangeREC(nowItemNum, 0, "CHANGE_Remark-Mechanical-" + filename, SQLDatabase.Nowmemory_rec);
                    if (k > 0)
                    {
                        MessageBox.Show("修改成功！", "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.ly_inma0010_drawing_mechanicalTableAdapter.Fill(this.lYMaterialMange.ly_inma0010_drawing_mechanical, wzbh);
                        lyinma0010drawingmechanicalBindingSource.Position = lyinma0010drawingmechanicalBindingSource.Find("id", id);
                    }
                }
                return;
            }
            if ("level8" == this.dataGridView8.CurrentCell.OwningColumn.Name)
            {

                if (this.dataGridView8.CurrentRow == null) return;
                if (SQLDatabase.CheckHaveRight(SQLDatabase.NowUserID, "图纸级别设置"))
                {

                }
                else
                {
                    string people = this.dataGridView8.CurrentRow.Cells["input_people8"].Value.ToString();
                    if (people != SQLDatabase.nowUserName())
                    {
                        MessageBox.Show("请上传人：" + people + " 操作", "注意");
                        return;
                    }
                }


                string filename = this.dataGridView8.CurrentRow.Cells["file_path8"].Value.ToString();

                ChangeValue queryForm = new ChangeValue();

                queryForm.OldValue = this.dataGridView8.CurrentRow.Cells["level8"].Value.ToString();
                queryForm.NewValue = "";
                queryForm.ChangeMode = "value";
                queryForm.ShowDialog();


                if (queryForm.NewValue != "")
                {
                    string wzbh = this.dataGridView8.CurrentRow.Cells["itemno8"].Value.ToString();
                    string id = this.dataGridView8.CurrentRow.Cells["id_jx"].Value.ToString();
                    int k = 0;
                    using (SqlConnection con = new SqlConnection(SQLDatabase.Connectstring))
                    {

                        string sql = "update  ly_inma0010_drawing_mechanical set level=" + queryForm.NewValue + "  where id=" + id;
                        using (SqlCommand cmd = new SqlCommand(sql, con))
                        {

                            con.Open();
                            k = cmd.ExecuteNonQuery();
                        }

                    }
                    SQLDatabase.drawingChangeREC(nowItemNum, 0, "CHANGE_LEV-Mechanical-" + filename, SQLDatabase.Nowmemory_rec);
                    if (k > 0)
                    {
                        MessageBox.Show("修改成功！", "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.ly_inma0010_drawing_mechanicalTableAdapter.Fill(this.lYMaterialMange.ly_inma0010_drawing_mechanical, wzbh);
                        lyinma0010drawingmechanicalBindingSource.Position = lyinma0010drawingmechanicalBindingSource.Find("id", id);
                    }
                }
                return;
            }
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if ("remark" == this.dataGridView1.CurrentCell.OwningColumn.Name)
            {
                if (this.dataGridView1.CurrentRow == null) return;
                if (SQLDatabase.CheckHaveRight(SQLDatabase.NowUserID, "图纸级别设置"))
                {

                }
                else
                {
                    string people = this.dataGridView1.CurrentRow.Cells["input_people"].Value.ToString();
                    if (people != SQLDatabase.nowUserName())
                    {
                        MessageBox.Show("请上传人：" + people + " 操作", "注意");
                        return;
                    }
                }
                string filename = this.dataGridView1.CurrentRow.Cells["file_path"].Value.ToString();

                ChangeValue queryForm = new ChangeValue();

                queryForm.OldValue = this.dataGridView1.CurrentRow.Cells["remark"].Value.ToString();
                queryForm.NewValue = "";
                queryForm.ChangeMode = "longstring";
                queryForm.ShowDialog();


                if (queryForm.NewValue != "")
                {
                    string wzbh = this.dataGridView1.CurrentRow.Cells["itemno"].Value.ToString();
                    string id = this.dataGridView1.CurrentRow.Cells["id_dq"].Value.ToString();
                    int k = 0;
                    using (SqlConnection con = new SqlConnection(SQLDatabase.Connectstring))
                    {

                        string sql = "update  ly_inma0010_drawing_electrical set remark='" + queryForm.NewValue + "'  where id=" + id;
                        using (SqlCommand cmd = new SqlCommand(sql, con))
                        {

                            con.Open();
                            k = cmd.ExecuteNonQuery();
                        }

                    }
                    SQLDatabase.drawingChangeREC(nowItemNum, 0, "CHANGE_Remark-Electrical-" + filename, SQLDatabase.Nowmemory_rec);
                    if (k > 0)
                    {
                        MessageBox.Show("修改成功！", "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.ly_inma0010_drawing_electricalTableAdapter.Fill(this.lYMaterialMange.ly_inma0010_drawing_electrical, wzbh);
                        lyinma0010drawingelectricalBindingSource.Position = lyinma0010drawingelectricalBindingSource.Find("id", id);
                    }
                }
                return;

            }
            if ("level" == this.dataGridView1.CurrentCell.OwningColumn.Name)
            {
                if (this.dataGridView1.CurrentRow == null) return;
                if (SQLDatabase.CheckHaveRight(SQLDatabase.NowUserID, "图纸级别设置"))
                {

                }
                else
                {
                    string people = this.dataGridView1.CurrentRow.Cells["input_people"].Value.ToString();
                    if (people != SQLDatabase.nowUserName())
                    {
                        MessageBox.Show("请上传人：" + people + " 操作", "注意");
                        return;
                    }
                }


                string filename = this.dataGridView1.CurrentRow.Cells["file_path"].Value.ToString();

                ChangeValue queryForm = new ChangeValue();

                queryForm.OldValue = this.dataGridView1.CurrentRow.Cells["level"].Value.ToString();
                queryForm.NewValue = "";
                queryForm.ChangeMode = "value";
                queryForm.ShowDialog();


                if (queryForm.NewValue != "")
                {
                    string wzbh = this.dataGridView1.CurrentRow.Cells["itemno"].Value.ToString();
                    string id = this.dataGridView1.CurrentRow.Cells["id_dq"].Value.ToString();
                    int k = 0;
                    using (SqlConnection con = new SqlConnection(SQLDatabase.Connectstring))
                    {

                        string sql = "update  ly_inma0010_drawing_electrical set level=" + queryForm.NewValue + "  where id=" + id;
                        using (SqlCommand cmd = new SqlCommand(sql, con))
                        {

                            con.Open();
                            k = cmd.ExecuteNonQuery();
                        }

                    }
                    SQLDatabase.drawingChangeREC(nowItemNum, 0, "CHANGE_LEV-Electrical-" + filename, SQLDatabase.Nowmemory_rec);
                    if (k > 0)
                    {
                        MessageBox.Show("修改成功！", "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.ly_inma0010_drawing_electricalTableAdapter.Fill(this.lYMaterialMange.ly_inma0010_drawing_electrical, wzbh);
                        lyinma0010drawingelectricalBindingSource.Position = lyinma0010drawingelectricalBindingSource.Find("id", id);
                    }
                }
                return;
            }
        }

        private void dataGridView2_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if ("remark_2" == this.dataGridView2.CurrentCell.OwningColumn.Name)
            {
                if (this.dataGridView2.CurrentRow == null) return;
                if (SQLDatabase.CheckHaveRight(SQLDatabase.NowUserID, "图纸级别设置"))
                {

                }
                else
                {
                    string people = this.dataGridView2.CurrentRow.Cells["input_people_2"].Value.ToString();
                    if (people != SQLDatabase.nowUserName())
                    {
                        MessageBox.Show("请上传人：" + people + " 操作", "注意");
                        return;
                    }
                }
                string filename = this.dataGridView2.CurrentRow.Cells["file_path_2"].Value.ToString();

                ChangeValue queryForm = new ChangeValue();

                queryForm.OldValue = this.dataGridView2.CurrentRow.Cells["remark_2"].Value.ToString();
                queryForm.NewValue = "";
                queryForm.ChangeMode = "longstring";
                queryForm.ShowDialog();


                if (queryForm.NewValue != "")
                {
                    string wzbh = this.dataGridView2.CurrentRow.Cells["itemno_2"].Value.ToString();
                    string id = this.dataGridView2.CurrentRow.Cells["id_pcb"].Value.ToString();
                    int k = 0;
                    using (SqlConnection con = new SqlConnection(SQLDatabase.Connectstring))
                    {

                        string sql = "update  ly_inma0010_drawing_pcb set remark='" + queryForm.NewValue + "'  where id=" + id;
                        using (SqlCommand cmd = new SqlCommand(sql, con))
                        {

                            con.Open();
                            k = cmd.ExecuteNonQuery();
                        }

                    }
                    SQLDatabase.drawingChangeREC(nowItemNum, 0, "CHANGE_Remark-PCB-" + filename, SQLDatabase.Nowmemory_rec);
                    if (k > 0)
                    {
                        MessageBox.Show("修改成功！", "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.ly_inma0010_drawing_pcbTableAdapter.Fill(this.lYMaterialMange.ly_inma0010_drawing_pcb, wzbh);
                        lyinma0010drawingpcbBindingSource.Position = lyinma0010drawingpcbBindingSource.Find("id", id);
                    }
                }
                return;
            }
            if ("level_2" == this.dataGridView2.CurrentCell.OwningColumn.Name)
            {
                if (this.dataGridView2.CurrentRow == null) return;
                if (SQLDatabase.CheckHaveRight(SQLDatabase.NowUserID, "图纸级别设置"))
                {

                }
                else
                {
                    string people = this.dataGridView2.CurrentRow.Cells["input_people_2"].Value.ToString();
                    if (people != SQLDatabase.nowUserName())
                    {
                        MessageBox.Show("请上传人：" + people + " 操作", "注意");
                        return;
                    }
                }


                string filename = this.dataGridView2.CurrentRow.Cells["file_path_2"].Value.ToString();

                ChangeValue queryForm = new ChangeValue();

                queryForm.OldValue = this.dataGridView2.CurrentRow.Cells["level_2"].Value.ToString();
                queryForm.NewValue = "";
                queryForm.ChangeMode = "value";
                queryForm.ShowDialog();


                if (queryForm.NewValue != "")
                {
                    string wzbh = this.dataGridView2.CurrentRow.Cells["itemno_2"].Value.ToString();
                    string id = this.dataGridView2.CurrentRow.Cells["id_pcb"].Value.ToString();
                    int k = 0;
                    using (SqlConnection con = new SqlConnection(SQLDatabase.Connectstring))
                    {

                        string sql = "update  ly_inma0010_drawing_pcb set level=" + queryForm.NewValue + "  where id=" + id;
                        using (SqlCommand cmd = new SqlCommand(sql, con))
                        {

                            con.Open();
                            k = cmd.ExecuteNonQuery();
                        }

                    }
                    SQLDatabase.drawingChangeREC(nowItemNum, 0, "CHANGE_LEV-PCB-" + filename, SQLDatabase.Nowmemory_rec);
                    if (k > 0)
                    {
                        MessageBox.Show("修改成功！", "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.ly_inma0010_drawing_pcbTableAdapter.Fill(this.lYMaterialMange.ly_inma0010_drawing_pcb, wzbh);
                        lyinma0010drawingpcbBindingSource.Position = lyinma0010drawingpcbBindingSource.Find("id", id);
                    }
                }
                return;
            }
        }

        private void dataGridView4_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if ("remark_4" == this.dataGridView4.CurrentCell.OwningColumn.Name)
            {
                if (this.dataGridView4.CurrentRow == null) return;
                if (SQLDatabase.CheckHaveRight(SQLDatabase.NowUserID, "图纸级别设置"))
                {

                }
                else
                {
                    string people = this.dataGridView4.CurrentRow.Cells["input_people_4"].Value.ToString();
                    if (people != SQLDatabase.nowUserName())
                    {
                        MessageBox.Show("请上传人：" + people + " 操作", "注意");
                        return;
                    }
                }
                string filename = this.dataGridView4.CurrentRow.Cells["file_path_4"].Value.ToString();

                ChangeValue queryForm = new ChangeValue();

                queryForm.OldValue = this.dataGridView4.CurrentRow.Cells["remark_4"].Value.ToString();
                queryForm.NewValue = "";
                queryForm.ChangeMode = "longstring";
                queryForm.ShowDialog();


                if (queryForm.NewValue != "")
                {
                    string wzbh = this.dataGridView4.CurrentRow.Cells["itemno_4"].Value.ToString();
                    string id = this.dataGridView4.CurrentRow.Cells["id_inspection"].Value.ToString();
                    int k = 0;
                    using (SqlConnection con = new SqlConnection(SQLDatabase.Connectstring))
                    {

                        string sql = "update  ly_inma0010_drawing_Inspection set remark='" + queryForm.NewValue + "'  where id=" + id;
                        using (SqlCommand cmd = new SqlCommand(sql, con))
                        {

                            con.Open();
                            k = cmd.ExecuteNonQuery();
                        }

                    }
                    SQLDatabase.drawingChangeREC(nowItemNum, 0, "CHANGE_Remark-Inspection-" + filename, SQLDatabase.Nowmemory_rec);
                    if (k > 0)
                    {
                        MessageBox.Show("修改成功！", "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.ly_inma0010_drawing_InspectionTableAdapter.Fill(this.lYMaterialMange.ly_inma0010_drawing_Inspection, wzbh);
                        lyinma0010drawingInspectionBindingSource.Position = lyinma0010drawingInspectionBindingSource.Find("id", id);
                    }
                }
                return;

            }
            if ("level_4" == this.dataGridView4.CurrentCell.OwningColumn.Name)
            {
                if (this.dataGridView4.CurrentRow == null) return;
                if (SQLDatabase.CheckHaveRight(SQLDatabase.NowUserID, "图纸级别设置"))
                {

                }
                else
                {
                    string people = this.dataGridView4.CurrentRow.Cells["input_people_4"].Value.ToString();
                    if (people != SQLDatabase.nowUserName())
                    {
                        MessageBox.Show("请上传人：" + people + " 操作", "注意");
                        return;
                    }
                }


                string filename = this.dataGridView4.CurrentRow.Cells["file_path_4"].Value.ToString();

                ChangeValue queryForm = new ChangeValue();

                queryForm.OldValue = this.dataGridView4.CurrentRow.Cells["level_4"].Value.ToString();
                queryForm.NewValue = "";
                queryForm.ChangeMode = "value";
                queryForm.ShowDialog();


                if (queryForm.NewValue != "")
                {
                    string wzbh = this.dataGridView4.CurrentRow.Cells["itemno_4"].Value.ToString();
                    string id = this.dataGridView4.CurrentRow.Cells["id_inspection"].Value.ToString();
                    int k = 0;
                    using (SqlConnection con = new SqlConnection(SQLDatabase.Connectstring))
                    {

                        string sql = "update  ly_inma0010_drawing_Inspection set level=" + queryForm.NewValue + "  where id=" + id;
                        using (SqlCommand cmd = new SqlCommand(sql, con))
                        {

                            con.Open();
                            k = cmd.ExecuteNonQuery();
                        }

                    }
                    SQLDatabase.drawingChangeREC(nowItemNum, 0, "CHANGE_LEV-Inspection-" + filename, SQLDatabase.Nowmemory_rec);
                    if (k > 0)
                    {
                        MessageBox.Show("修改成功！", "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.ly_inma0010_drawing_InspectionTableAdapter.Fill(this.lYMaterialMange.ly_inma0010_drawing_Inspection, wzbh);
                        lyinma0010drawingInspectionBindingSource.Position = lyinma0010drawingInspectionBindingSource.Find("id", id);
                    }
                }
                return;
            }
        }

        private void dataGridView5_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if ("remark_5" == this.dataGridView5.CurrentCell.OwningColumn.Name)
            {

                if (this.dataGridView5.CurrentRow == null) return;
                if (SQLDatabase.CheckHaveRight(SQLDatabase.NowUserID, "图纸级别设置"))
                {

                }
                else
                {
                    string people = this.dataGridView5.CurrentRow.Cells["input_people_5"].Value.ToString();
                    if (people != SQLDatabase.nowUserName())
                    {
                        MessageBox.Show("请上传人：" + people + " 操作", "注意");
                        return;
                    }
                }
                string filename = this.dataGridView5.CurrentRow.Cells["file_path_5"].Value.ToString();

                ChangeValue queryForm = new ChangeValue();

                queryForm.OldValue = this.dataGridView5.CurrentRow.Cells["remark_5"].Value.ToString();
                queryForm.NewValue = "";
                queryForm.ChangeMode = "longstring";
                queryForm.ShowDialog();


                if (queryForm.NewValue != "")
                {
                    string wzbh = this.dataGridView5.CurrentRow.Cells["itemno_5"].Value.ToString();
                    string id = this.dataGridView5.CurrentRow.Cells["id_exe"].Value.ToString();
                    int k = 0;
                    using (SqlConnection con = new SqlConnection(SQLDatabase.Connectstring))
                    {

                        string sql = "update  ly_inma0010_drawing_exe set remark='" + queryForm.NewValue + "'  where id=" + id;
                        using (SqlCommand cmd = new SqlCommand(sql, con))
                        {

                            con.Open();
                            k = cmd.ExecuteNonQuery();
                        }

                    }
                    SQLDatabase.drawingChangeREC(nowItemNum, 0, "CHANGE_Remark-Exe-" + filename, SQLDatabase.Nowmemory_rec);
                    if (k > 0)
                    {
                        MessageBox.Show("修改成功！", "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.ly_inma0010_drawing_exeTableAdapter.Fill(this.lYMaterialMange.ly_inma0010_drawing_exe, wzbh);
                        lyinma0010drawingexeBindingSource.Position = lyinma0010drawingexeBindingSource.Find("id", id);
                    }
                }
                return;
            }
            if ("level_5" == this.dataGridView5.CurrentCell.OwningColumn.Name)
            {
                if (this.dataGridView5.CurrentRow == null) return;

                if (SQLDatabase.CheckHaveRight(SQLDatabase.NowUserID, "图纸级别设置"))
                {

                }
                else
                {
                    string people = this.dataGridView5.CurrentRow.Cells["input_people_5"].Value.ToString();
                    if (people != SQLDatabase.nowUserName())
                    {
                        MessageBox.Show("请上传人：" + people + " 操作", "注意");
                        return;
                    }
                }

                string filename = this.dataGridView5.CurrentRow.Cells["file_path_5"].Value.ToString();

                ChangeValue queryForm = new ChangeValue();

                queryForm.OldValue = this.dataGridView5.CurrentRow.Cells["level_5"].Value.ToString();
                queryForm.NewValue = "";
                queryForm.ChangeMode = "value";
                queryForm.ShowDialog();


                if (queryForm.NewValue != "")
                {
                    string wzbh = this.dataGridView5.CurrentRow.Cells["itemno_5"].Value.ToString();
                    string id = this.dataGridView5.CurrentRow.Cells["id_exe"].Value.ToString();
                    int k = 0;
                    using (SqlConnection con = new SqlConnection(SQLDatabase.Connectstring))
                    {

                        string sql = "update  ly_inma0010_drawing_exe set level=" + queryForm.NewValue + "  where id=" + id;
                        using (SqlCommand cmd = new SqlCommand(sql, con))
                        {

                            con.Open();
                            k = cmd.ExecuteNonQuery();
                        }

                    }
                    SQLDatabase.drawingChangeREC(nowItemNum, 0, "CHANGE_LEV-Exe-" + filename, SQLDatabase.Nowmemory_rec);
                    if (k > 0)
                    {
                        MessageBox.Show("修改成功！", "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.ly_inma0010_drawing_exeTableAdapter.Fill(this.lYMaterialMange.ly_inma0010_drawing_exe, wzbh);
                        lyinma0010drawingexeBindingSource.Position = lyinma0010drawingexeBindingSource.Find("id", id);
                    }
                }
                return;
            }
        }

        private void dataGridView6_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if ("remark_6" == this.dataGridView6.CurrentCell.OwningColumn.Name)
            {
                if (this.dataGridView6.CurrentRow == null) return;
                if (SQLDatabase.CheckHaveRight(SQLDatabase.NowUserID, "图纸级别设置"))
                {

                }
                else
                {
                    string people = this.dataGridView6.CurrentRow.Cells["input_people_6"].Value.ToString();
                    if (people != SQLDatabase.nowUserName())
                    {
                        MessageBox.Show("请上传人：" + people + " 操作", "注意");
                        return;
                    }
                }
                string filename = this.dataGridView6.CurrentRow.Cells["file_path_6"].Value.ToString();

                ChangeValue queryForm = new ChangeValue();

                queryForm.OldValue = this.dataGridView6.CurrentRow.Cells["remark_6"].Value.ToString();
                queryForm.NewValue = "";
                queryForm.ChangeMode = "longstring";
                queryForm.ShowDialog();


                if (queryForm.NewValue != "")
                {
                    string wzbh = this.dataGridView6.CurrentRow.Cells["itemno_6"].Value.ToString();
                    string id = this.dataGridView6.CurrentRow.Cells["id_code"].Value.ToString();
                    int k = 0;
                    using (SqlConnection con = new SqlConnection(SQLDatabase.Connectstring))
                    {

                        string sql = "update  ly_inma0010_drawing_code set remark='" + queryForm.NewValue + "'  where id=" + id;
                        using (SqlCommand cmd = new SqlCommand(sql, con))
                        {

                            con.Open();
                            k = cmd.ExecuteNonQuery();
                        }

                    }
                    SQLDatabase.drawingChangeREC(nowItemNum, 0, "CHANGE_Remark-code-" + filename, SQLDatabase.Nowmemory_rec);
                    if (k > 0)
                    {
                        MessageBox.Show("修改成功！", "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.ly_inma0010_drawing_codeTableAdapter.Fill(this.lYMaterialMange.ly_inma0010_drawing_code, wzbh);
                        lyinma0010drawingcodeBindingSource.Position = lyinma0010drawingcodeBindingSource.Find("id", id);
                    }
                }
                return;

            }
                if ("level_6" == this.dataGridView6.CurrentCell.OwningColumn.Name)
            {
                if (this.dataGridView6.CurrentRow == null) return;
                if (SQLDatabase.CheckHaveRight(SQLDatabase.NowUserID, "图纸级别设置"))
                {

                }
                else
                {
                    string people = this.dataGridView6.CurrentRow.Cells["input_people_6"].Value.ToString();
                    if (people != SQLDatabase.nowUserName())
                    {
                        MessageBox.Show("请上传人：" + people + " 操作", "注意");
                        return;
                    }
                }


                string filename = this.dataGridView6.CurrentRow.Cells["file_path_6"].Value.ToString();

                ChangeValue queryForm = new ChangeValue();

                queryForm.OldValue = this.dataGridView6.CurrentRow.Cells["level_6"].Value.ToString();
                queryForm.NewValue = "";
                queryForm.ChangeMode = "value";
                queryForm.ShowDialog();


                if (queryForm.NewValue != "")
                {
                    string wzbh = this.dataGridView6.CurrentRow.Cells["itemno_6"].Value.ToString();
                    string id = this.dataGridView6.CurrentRow.Cells["id_code"].Value.ToString();
                    int k = 0;
                    using (SqlConnection con = new SqlConnection(SQLDatabase.Connectstring))
                    {

                        string sql = "update  ly_inma0010_drawing_code set level=" + queryForm.NewValue + "  where id=" + id;
                        using (SqlCommand cmd = new SqlCommand(sql, con))
                        {

                            con.Open();
                            k = cmd.ExecuteNonQuery();
                        }

                    }
                    SQLDatabase.drawingChangeREC(nowItemNum, 0, "CHANGE_LEV-code-" + filename, SQLDatabase.Nowmemory_rec);
                    if (k > 0)
                    {
                        MessageBox.Show("修改成功！", "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.ly_inma0010_drawing_codeTableAdapter.Fill(this.lYMaterialMange.ly_inma0010_drawing_code, wzbh);
                        lyinma0010drawingcodeBindingSource.Position = lyinma0010drawingcodeBindingSource.Find("id", id);
                    }
                }
                return;
            }
        }

        private void dataGridView7_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if ("remark_7" == this.dataGridView7.CurrentCell.OwningColumn.Name)
            {
                if (this.dataGridView7.CurrentRow == null) return;
                if (SQLDatabase.CheckHaveRight(SQLDatabase.NowUserID, "图纸级别设置"))
                {

                }
                else
                {
                    string people = this.dataGridView7.CurrentRow.Cells["input_people_7"].Value.ToString();
                    if (people != SQLDatabase.nowUserName())
                    {
                        MessageBox.Show("请上传人：" + people + " 操作", "注意");
                        return;
                    }
                }
                string filename = this.dataGridView7.CurrentRow.Cells["file_path_7"].Value.ToString();

                ChangeValue queryForm = new ChangeValue();

                queryForm.OldValue = this.dataGridView7.CurrentRow.Cells["remark_7"].Value.ToString();
                queryForm.NewValue = "";
                queryForm.ChangeMode = "longstring";
                queryForm.ShowDialog();


                if (queryForm.NewValue != "")
                {
                    string wzbh = this.dataGridView7.CurrentRow.Cells["itemno_7"].Value.ToString();
                    string id = this.dataGridView7.CurrentRow.Cells["id_book"].Value.ToString();
                    int k = 0;
                    using (SqlConnection con = new SqlConnection(SQLDatabase.Connectstring))
                    {

                        string sql = "update  ly_inma0010_drawing_book set remark='" + queryForm.NewValue + "'  where id=" + id;
                        using (SqlCommand cmd = new SqlCommand(sql, con))
                        {

                            con.Open();
                            k = cmd.ExecuteNonQuery();
                        }

                    }
                    SQLDatabase.drawingChangeREC(nowItemNum, 0, "CHANGE_Remark-book-" + filename, SQLDatabase.Nowmemory_rec);
                    if (k > 0)
                    {
                        MessageBox.Show("修改成功！", "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.ly_inma0010_drawing_bookTableAdapter.Fill(this.lYMaterialMange.ly_inma0010_drawing_book, wzbh);
                        lyinma0010drawingbookBindingSource.Position = lyinma0010drawingbookBindingSource.Find("id", id);
                    }
                }
                return;
            }
                if ("level_7" == this.dataGridView7.CurrentCell.OwningColumn.Name)
            {
                if (this.dataGridView7.CurrentRow == null) return;
                if (SQLDatabase.CheckHaveRight(SQLDatabase.NowUserID, "图纸级别设置"))
                {

                }
                else
                {
                    string people = this.dataGridView7.CurrentRow.Cells["input_people_7"].Value.ToString();
                    if (people != SQLDatabase.nowUserName())
                    {
                        MessageBox.Show("请上传人：" + people + " 操作", "注意");
                        return;
                    }
                }


                string filename = this.dataGridView7.CurrentRow.Cells["file_path_7"].Value.ToString();

                ChangeValue queryForm = new ChangeValue();

                queryForm.OldValue = this.dataGridView7.CurrentRow.Cells["level_7"].Value.ToString();
                queryForm.NewValue = "";
                queryForm.ChangeMode = "value";
                queryForm.ShowDialog();


                if (queryForm.NewValue != "")
                {
                    string wzbh = this.dataGridView7.CurrentRow.Cells["itemno_7"].Value.ToString();
                    string id = this.dataGridView7.CurrentRow.Cells["id_book"].Value.ToString();
                    int k = 0;
                    using (SqlConnection con = new SqlConnection(SQLDatabase.Connectstring))
                    {

                        string sql = "update  ly_inma0010_drawing_book set level=" + queryForm.NewValue + "  where id=" + id;
                        using (SqlCommand cmd = new SqlCommand(sql, con))
                        {

                            con.Open();
                            k = cmd.ExecuteNonQuery();
                        }

                    }
                    SQLDatabase.drawingChangeREC(nowItemNum, 0, "CHANGE_LEV-book-" + filename, SQLDatabase.Nowmemory_rec);
                    if (k > 0)
                    {
                        MessageBox.Show("修改成功！", "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.ly_inma0010_drawing_bookTableAdapter.Fill(this.lYMaterialMange.ly_inma0010_drawing_book, wzbh);
                        lyinma0010drawingbookBindingSource.Position = lyinma0010drawingbookBindingSource.Find("id", id);
                    }
                }
                return;
            }
        }

        private void dataGridView9_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if ("remark_9" == this.dataGridView9.CurrentCell.OwningColumn.Name)
            {
                if (this.dataGridView9.CurrentRow == null) return;
                if (SQLDatabase.CheckHaveRight(SQLDatabase.NowUserID, "图纸级别设置"))
                {

                }
                else
                {
                    string people = this.dataGridView9.CurrentRow.Cells["input_people_9"].Value.ToString();
                    if (people != SQLDatabase.nowUserName())
                    {
                        MessageBox.Show("请上传人：" + people + " 操作", "注意");
                        return;
                    }
                }
                string filename = this.dataGridView9.CurrentRow.Cells["file_path_9"].Value.ToString();

                ChangeValue queryForm = new ChangeValue();

                queryForm.OldValue = this.dataGridView9.CurrentRow.Cells["remark_9"].Value.ToString();
                queryForm.NewValue = "";
                queryForm.ChangeMode = "longstring";
                queryForm.ShowDialog();


                if (queryForm.NewValue != "")
                {
                    string wzbh = this.dataGridView9.CurrentRow.Cells["itemno_9"].Value.ToString();
                    string id = this.dataGridView9.CurrentRow.Cells["id_bom"].Value.ToString();
                    int k = 0;
                    using (SqlConnection con = new SqlConnection(SQLDatabase.Connectstring))
                    {

                        string sql = "update  ly_inma0010_drawing_bom set remark='" + queryForm.NewValue + "'  where id=" + id;
                        using (SqlCommand cmd = new SqlCommand(sql, con))
                        {

                            con.Open();
                            k = cmd.ExecuteNonQuery();
                        }

                    }
                    SQLDatabase.drawingChangeREC(nowItemNum, 0, "CHANGE_Remark-bom-" + filename, SQLDatabase.Nowmemory_rec);
                    if (k > 0)
                    {
                        MessageBox.Show("修改成功！", "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.ly_inma0010_drawing_bomTableAdapter.Fill(this.lYMaterialMange.ly_inma0010_drawing_bom, wzbh);
                        lyinma0010drawingbomBindingSource.Position = lyinma0010drawingbomBindingSource.Find("id", id);
                    }
                }
                return;
            }
                if ("level_9" == this.dataGridView9.CurrentCell.OwningColumn.Name)
            {
                if (this.dataGridView9.CurrentRow == null) return;
                if (SQLDatabase.CheckHaveRight(SQLDatabase.NowUserID, "图纸级别设置"))
                {

                }
                else
                {
                    string people = this.dataGridView9.CurrentRow.Cells["input_people_9"].Value.ToString();
                    if (people != SQLDatabase.nowUserName())
                    {
                        MessageBox.Show("请上传人：" + people + " 操作", "注意");
                        return;
                    }
                }


                string filename = this.dataGridView9.CurrentRow.Cells["file_path_9"].Value.ToString();

                ChangeValue queryForm = new ChangeValue();

                queryForm.OldValue = this.dataGridView9.CurrentRow.Cells["level_9"].Value.ToString();
                queryForm.NewValue = "";
                queryForm.ChangeMode = "value";
                queryForm.ShowDialog();


                if (queryForm.NewValue != "")
                {
                    string wzbh = this.dataGridView9.CurrentRow.Cells["itemno_9"].Value.ToString();
                    string id = this.dataGridView9.CurrentRow.Cells["id_bom"].Value.ToString();
                    int k = 0;
                    using (SqlConnection con = new SqlConnection(SQLDatabase.Connectstring))
                    {

                        string sql = "update  ly_inma0010_drawing_bom set level=" + queryForm.NewValue + "  where id=" + id;
                        using (SqlCommand cmd = new SqlCommand(sql, con))
                        {

                            con.Open();
                            k = cmd.ExecuteNonQuery();
                        }

                    }
                    SQLDatabase.drawingChangeREC(nowItemNum, 0, "CHANGE_LEV-bom-" + filename, SQLDatabase.Nowmemory_rec);
                    if (k > 0)
                    {
                        MessageBox.Show("修改成功！", "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.ly_inma0010_drawing_bomTableAdapter.Fill(this.lYMaterialMange.ly_inma0010_drawing_bom, wzbh);
                        lyinma0010drawingbomBindingSource.Position = lyinma0010drawingbomBindingSource.Find("id", id);
                    }
                }
                return;
            }
        }
    }
}