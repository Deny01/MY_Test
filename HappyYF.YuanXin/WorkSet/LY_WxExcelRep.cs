using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HappyYF.Infrastructure.Repositories;

namespace HappyYF.YuanXin.WorkSet
{
    public partial class LY_WxExcelRep : Form
    {
        public string rece_code = "";
        public LY_WxExcelRep()
        {
            InitializeComponent();
        }



        private void button1_Click(object sender, EventArgs e)
        {
            //if (people != SQLDatabase.nowUserName())
            //{
            //    MessageBox.Show("请负责人：" + people + " 操作", "注意");
            //    return;
            //}
            if (rece_code == "")
            {
                MessageBox.Show("无收件单号", "注意");
                return;
            }

            string targetName = "\\\\192.168.1.9\\Drawing\\ExcelSales\\";

            OpenFileDialog openFile = new OpenFileDialog();

            openFile.Filter = "所有文件|*.*";

            string sourcename = "";
            string filetype = "";
            string filepath = "";
            if (openFile.ShowDialog() == DialogResult.OK)
            {
                filetype = System.IO.Path.GetExtension(openFile.FileName);
                filepath = (rece_code+"_" + DateTime.Now.ToString("yyyyMMddHHmmss") + filetype);
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

                        string sql = @"insert into sales_excel (file_name ,file_path,rece_code,sava_time,save_peo,down_click)
                                   values   ('" + filepath + "'  ,'" + (filepath) + "','" + rece_code + "','" + DateTime.Now + "','" + SQLDatabase.nowUserName() + "',0)";
                        using (SqlCommand cmd = new SqlCommand(sql, con))
                        {
                            con.Open();
                            k = cmd.ExecuteNonQuery();
                        }

                    }
                    if (k > 0)
                    {
                        NewFrm.Hide(this);
                        this.sales_excelTableAdapter.Fill(this.lYSalseMange2.sales_excel, rece_code);
                        MessageBox.Show("导入成功！", "信息提示");
                     
                    }
                }
                else
                {
                    NewFrm.Hide(this);
                    MessageBox.Show("连接主机失败,请再次尝试！", "信息提示");

                }
            }
            else
            {
                NewFrm.Hide(this);
                MessageBox.Show("Ping主机失败,请再次尝试！", "信息提示");
            }
        }

        private void LY_Quality_Control_PurchaseRep_Load(object sender, EventArgs e)
        {
            this.sales_excelTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            if (rece_code != "")
            {
                this.sales_excelTableAdapter.Fill(this.lYSalseMange2.sales_excel, rece_code);
            }
        }

        private void 删除该文件ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.ly_purchase_contract_inspectionRepDataGridView.CurrentRow == null) return;
            
                string people = this.ly_purchase_contract_inspectionRepDataGridView.CurrentRow.Cells["savepeo"].Value.ToString();
                if (people != SQLDatabase.nowUserName())
                {
                    MessageBox.Show("请上传人：" + people + " 操作", "注意");
                    return;
                } 

                string id = this.ly_purchase_contract_inspectionRepDataGridView.CurrentRow.Cells["id"].Value.ToString();
          
                int k = 0;
                using (SqlConnection con = new SqlConnection(SQLDatabase.Connectstring))
                {
                    string sql = "delete from sales_excel where id=" + id;
                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {

                        con.Open();
                        k = cmd.ExecuteNonQuery();
                    }
                }
            if (k > 0)
            {

                MessageBox.Show("删除成功！", "信息提示");
                this.sales_excelTableAdapter.Fill(this.lYSalseMange2.sales_excel, rece_code);
            }

        }

        private void 下载文件ToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (null == this.ly_purchase_contract_inspectionRepDataGridView.CurrentRow) return;

            string filename = this.ly_purchase_contract_inspectionRepDataGridView.CurrentRow.Cells["filepath"].Value.ToString();
            string sourcename = "\\\\192.168.1.9\\Drawing\\ExcelSales\\" + filename;
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
               
                    MessageBox.Show("连接主机失败！", "信息提示");

                }
            }
            else
            {
                
                MessageBox.Show("Ping主机失败！", "信息提示");

            }
        }
    }
}
