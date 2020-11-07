 using System;
//using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using TGZJ.Manger;
using HappyYF.Infrastructure.Repositories;
using HappyYF.YuanXin.WorkSet;
using System.IO;

using RemoteBackupTool;
using Silver.UI;

using Project_Manager.AppServices;


namespace TGZJ
{
    public partial class MainForm : Form
    {
        
        int imageIndex = 0;
       
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            //SQLDatabase.Connectstring = Program.dataBase.MakeConnectString();
            //SQLDatabase.NowUserID = Program.nowUser.Name;

            this.notifyIcon2.Visible = false;
            //this.notifyIcon1.BalloonTipTitle = "        中原精密";
            //this.notifyIcon1.



            if (CheckRegister())
            {
                //

                DialogResult dr;
                dr = new LoginForm().ShowDialog(this);

                if (dr == DialogResult.Cancel)
                {
                    this.Close();
                }
                else
                {
                    //notifyIcon1.Text = "中原精密\n成铭  13633986937    方新  13839870291";
                    notifyIcon1.Text = "电话  2751827        小号  6827\n成铭  13633986937    方新  13839870291";

                    SQLDatabase.Connectstring = Program.dataBase.MakeConnectString();
                    SQLDatabase.NowUserID = Program.nowUser.Name;


                    if (CheckProbationDate())
                        this.Close();
                    MakeNavigationView(this.toolBox1, this.menuStrip1.Items, null);

                    SetNavigationView(this.toolBox1);

                    this.Text = SQLDatabase.nowUserName() + this.Text;
                    //定时函数

                    if ( SQLDatabase .CheckNeedInform())
                    {
                        this.timer1.Interval = 10000;
                        this.timer1.Start();
                    }

                }

            }
            else
            {
                //if (!CheckProbationDate())
                this.Close();

            }
 
            

        }

       
        private void SetNavigationView(ToolBox tb)
        {
            tb.LargeImageList.ImageSize = new Size(39, 39);

            tb.SelectedTabIndex = 1;
            tb.ItemSpacing = 6;
            //foreach (ToolBoxTab tbt in tb.ToolBoxTabs)
            //{
            //    if (null != tbt[0])
            //        tbt[0].Selected = true;

            //}
        }
        private void MakeNavigationView(ToolBox tb, ToolStripItemCollection ms, ToolBoxItem tbi)
        {

            if (ms.Count == 0)
                return;

            foreach (ToolStripItem tm in ms)
            {
                if ("N" == (tm.Tag as string)) continue;
                if (!HaveRight(Program.nowUser.Name, tm.Text)) continue;
                if (tm is ToolStripMenuItem)
                {
                    //ToolBoxTab nowTbt;
                    ToolBoxItem nowTbi;
                    if (null == tbi)
                    {
                        nowTbi = new ToolBoxTab();
                        nowTbi.Caption = tm.Text;
                        ((ToolBoxTab)nowTbi).View = ViewMode.LargeIcons;
                        ((ToolBoxTab)nowTbi).ShowOnlyOneItemPerRow = true;

                        tb.AddTab(nowTbi as ToolBoxTab);
                    }
                    else
                    {

                        nowTbi = new ToolBoxItem();
                        nowTbi.Caption = tm.Text;
                        nowTbi.Selected = false;
                        nowTbi.Enabled = true;
                        if (null != tm.Image)
                        {
                            tb.LargeImageList.Images.Add(tm.Image);
                            nowTbi.LargeImageIndex = imageIndex;


                            imageIndex = imageIndex + 1;
                        }

                        (tbi as ToolBoxTab).AddItem(nowTbi);
                        nowTbi.Selected = false;

                    }





                    MakeNavigationView(tb, ((ToolStripMenuItem)tm).DropDown.Items, nowTbi);
                }


            }
        }

        private bool CheckProbationDate()
        {


            SqlConnection Con = new SqlConnection( Program.dataBase.MakeConnectString());
            Con.Open();

          
            SqlCommand Com = new SqlCommand(" select DATEDIFF(day, getdate(),'2070-06-01') ", Con);

           
            string databaseDate = Com.ExecuteScalar().ToString();


            int leftdays = int.Parse(databaseDate);

            if (leftdays > 0 && leftdays < 10)
            {
                MessageBox.Show("离支付本软件余款还有" + leftdays.ToString() + "天");
                return false ;
            }

            if (leftdays <= 0)
            {
                MessageBox.Show("请支付本软件余款后才能使用");
                return true ;
            }

            return false;


           
           
        
        }
        private bool CheckRegister()
        {

            ////Program.connectstring = "Hello Deny...";
            //TGZJ.Base.Mydataconn mc = Mydataconn.Load();

            //if (null == mc) mc = new Mydataconn();



            //    Simple3Des jm = new Simple3Des("deny01.12345678901234567890zb");

            //    System.Management.ManagementClass mcpu = new ManagementClass("win32_processor");
            //    ManagementObjectCollection moc = mcpu.GetInstances();

            //    string scpuCode = "";

            //    foreach (ManagementObject mo in moc)
            //    {
            //        //MessageBox.Show(mo["processorid"].ToString());
            //        scpuCode = scpuCode + mo["processorid"].ToString();

            //    }

            //    if ((null == mc.RegisterCode) || (mc.RegisterCode != jm.EncryptData(scpuCode)))
            //    {
            //        DialogResult dr;
            //        dr = new RegisterCode().ShowDialog(this);

            //        if (dr == DialogResult.Cancel)
            //        {
            //        //this.Close();

            //        //return false;
            //        return true;
            //        }
            //        else
            //        {
            //            return true;
            //        }
            //    }
            //    else
            //    {

            //        return true;
            //    }
            return true;

        }

         private bool  HaveRight(string yh,string nowMenu)
         {
            string cString = Program.dataBase.MakeConnectString();

            string selAllString = "SELECT    *  from  T_gongnengshouquan  where yonghu_ID = '" + yh + "' and control_name = '" + nowMenu +"'";
            SqlDataAdapter nowAdapter = new SqlDataAdapter(selAllString, cString);

            DataSet nowData = new DataSet();
            nowAdapter.Fill(nowData);

             if ( nowData.Tables [0].Rows .Count > 0)
                 return true ;
             else 
                 return false ;

         
         
         
         }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void 退出ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void 用户管理ToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (!HaveRight(Program.nowUser.Name, sender.ToString()))
            {
                MessageBox.Show(" 没有" + sender.ToString() + "权限...");

                return;
            }
            

            foreach (Form frm in this.MdiChildren)
            {
                if (frm is Yonghu)
                {
                    frm.WindowState = FormWindowState.Normal;
                    frm.Activate();
                    return;
                }
            }

            Yonghu user = new Yonghu();
            user.MdiParent = this;
            user.WindowState = FormWindowState.Normal;
            user.Show();
        }

        private void 用户密码修改ToolStripMenuItem_Click(object sender, EventArgs e)
        {

            foreach (Form frm in this.MdiChildren)
            {
                if (frm is ChangePasswordForm)
                {
                    frm.WindowState = FormWindowState.Normal;
                    frm.Activate();
                    return;
                }
            }

            ChangePasswordForm password = new ChangePasswordForm();
            password.MdiParent = this;
            //user.WindowState = FormWindowState.Normal ;
            password.Show();

        }
     


        private void 软件开发商ToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (!HaveRight(Program.nowUser.Name, sender.ToString()))
            {
                MessageBox.Show(" 没有" + sender.ToString() + "权限...");

                return;
            }
            
        }

     
        private void 部门设置ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!HaveRight(Program.nowUser.Name, sender.ToString()))
            {
                MessageBox.Show(" 没有" + sender.ToString() + "权限...");

                return;
            }
            
            foreach (Form frm in this.MdiChildren)
            {
                if (frm is Bumen)
                {
                    frm.WindowState = FormWindowState.Maximized;
                    frm.Activate();
                    return;
                }
            }

            Bumen bm = new Bumen();

            bm.Parent_model = ((System.Windows.Forms.ToolStripItem)(sender)).OwnerItem.Text;
            bm.MdiParent = this;
            bm.WindowState = FormWindowState.Maximized;
            bm.Show();
        }

        private void 用户权限设置ToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (!HaveRight(Program.nowUser.Name, sender.ToString()))
            {
                MessageBox.Show(" 没有" + sender .ToString ()+ "权限...");

               //return;
            }
            
            foreach (Form frm in this.MdiChildren)
            {
                if (frm is QuanxianFenpei)
                {
                    frm.WindowState = FormWindowState.Maximized;
                    frm.Activate();
                    return;
                }
            }

            QuanxianFenpei qx = new QuanxianFenpei();

            qx.MainMenu = this.menuStrip1;
            qx.Parent_model = ((System.Windows.Forms.ToolStripItem)(sender)).OwnerItem.Text;
            qx.MdiParent = this;
            qx.WindowState = FormWindowState.Maximized;
            qx.Show();

        }

        private void 生产日报ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!HaveRight(Program.nowUser.Name, sender.ToString()))
            {
                MessageBox.Show(" 没有" + sender.ToString() + "权限...");

                return;
            }
            
         

        }

       

        private void 会员卡业务输入ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!HaveRight(Program.nowUser.Name, sender.ToString()))
            {
                MessageBox.Show(" 没有" + sender.ToString() + "权限...");

                return;
            }
         
        }

        private void 服务大类设置ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!HaveRight(Program.nowUser.Name, sender.ToString()))
            {
                MessageBox.Show(" 没有" + sender.ToString() + "权限...");

                return;
            }
            foreach (Form frm in this.MdiChildren)
            {
                if (frm is GroupSet)
                {
                    frm.WindowState = FormWindowState.Maximized;
                    frm.Activate();
                    return;
                }
            }

            GroupSet dwp = new GroupSet();

            //dwp.MainMenu = this.menuStrip1;
            //dwp.Parent_model = ((System.Windows.Forms.ToolStripItem)(sender)).OwnerItem.Text;
            dwp.MdiParent = this;
            dwp.WindowState = FormWindowState.Maximized ;
            //dwp.StartPosition = FormStartPosition.CenterScreen;
            dwp.Show();

        }

        private void 分类汇总ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!HaveRight(Program.nowUser.Name, sender.ToString()))
            {
                MessageBox.Show(" 没有" + sender.ToString() + "权限...");

                return;
            }
            foreach (Form frm in this.MdiChildren)
            {
                if (frm is SumAnalysis)
                {
                    frm.WindowState = FormWindowState.Maximized;
                    frm.Activate();
                    return;
                }
            }

            SumAnalysis dwp = new SumAnalysis();

            //dwp.MainMenu = this.menuStrip1;
            //dwp.Parent_model = ((System.Windows.Forms.ToolStripItem)(sender)).OwnerItem.Text;
            dwp.MdiParent = this;
            dwp.WindowState = FormWindowState.Maximized;
            //dwp.StartPosition = FormStartPosition.CenterScreen;
            dwp.Show();
        }

        private void 日常营业明细ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!HaveRight(Program.nowUser.Name, sender.ToString()))
            {
                MessageBox.Show(" 没有" + sender.ToString() + "权限...");

                return;
            }
            
            foreach (Form frm in this.MdiChildren)
            {
                if (frm is SumCard)
                {
                    frm.WindowState = FormWindowState.Maximized;
                    frm.Activate();
                    return;
                }
            }

            SumCard dwp = new SumCard();

            //dwp.MainMenu = this.menuStrip1;
            //dwp.Parent_model = ((System.Windows.Forms.ToolStripItem)(sender)).OwnerItem.Text;
            dwp.MdiParent = this;
            dwp.WindowState = FormWindowState.Maximized;
            //dwp.StartPosition = FormStartPosition.CenterScreen;
            dwp.Show();
        }

        private void 数据备份ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!HaveRight(Program.nowUser.Name, sender.ToString()))
            {
                MessageBox.Show(" 没有" + sender.ToString() + "权限...");

                return;
            }



            foreach (Form frm in this.MdiChildren)
            {
                if (frm is SQLBackup)
                {
                    frm.WindowState = FormWindowState.Normal;
                    frm.Activate();
                    return;
                }
            }

            SQLBackup user = new SQLBackup();

            user.ServerName = Program.dataBase.ServerName;
            user.DataBaseName = Program.dataBase.DataBaseName;
            user.UserName = Program.dataBase.UserName;
            user.PassWord = Program.dataBase.PassWord;

            user.MdiParent = this;
            user.WindowState = FormWindowState.Normal;
            user.StartPosition = FormStartPosition.CenterScreen;
            user.Show();
        }

        private void notifyIcon1_MouseClick(object sender, MouseEventArgs e)
        {

        }

        public void ShowHideForm()
        {
            if (this.Visible)
            {
                this.Visible = false;;
            }
            else
            {
                this.Visible = true ; 
              
            }
        }

        private void notifyIcon1_MouseMove(object sender, MouseEventArgs e)
        {
            notifyIcon1.ShowBalloonTip(3);
        }

       

        private void 机器授权ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!HaveRight(Program.nowUser.Name, sender.ToString()))
            {
                MessageBox.Show(" 没有" + sender.ToString() + "权限...");

                return;
            }
            foreach (Form frm in this.MdiChildren)
            {
                if (frm is CPUAuthorization)
                {
                    frm.WindowState = FormWindowState.Normal;
                    frm.Activate();
                    return;
                }
            }
            CPUAuthorization fm1 = new CPUAuthorization();
            fm1.MdiParent = this;
            fm1.WindowState = FormWindowState.Normal;
            fm1.StartPosition = FormStartPosition.CenterScreen;
            fm1.Show();
        }

     

        private void toolBox1_ItemMouseUp(ToolBoxItem sender, MouseEventArgs e)
        {
            ToolBoxItem tbi = sender as ToolBoxItem;
            foreach (Form frm in this.splitContainer1.Panel2.Controls)
            {

                frm.WindowState = FormWindowState.Minimized;
                //frm.Activate();

            }
            

            switch (tbi.Caption)
            {
                case "维修物料调拨":
                    foreach (Form frm in this.splitContainer1.Panel2.Controls)
                    {
                        if (frm is LY_DayGet_Plan_ForMaintenance)
                        {
                            frm.WindowState = FormWindowState.Maximized;
                            frm.Activate();
                            return;
                        }
                    }
                    LY_DayGet_Plan_ForMaintenance wxplll = new LY_DayGet_Plan_ForMaintenance();
                    wxplll.MdiParent = this;
                    wxplll.Parent = this.splitContainer1.Panel2;
                    wxplll.WindowState = FormWindowState.Maximized;
                    wxplll.Show();
                    break;


                case "库存调整":
                    foreach (Form frm in this.splitContainer1.Panel2.Controls)
                    {
                        if (frm is LY_DayGet_Plan_NewApprove)
                        {
                            frm.WindowState = FormWindowState.Maximized;
                            frm.Activate();
                            return;
                        }
                    } 
                    LY_DayGet_Plan_NewApprove kctz = new LY_DayGet_Plan_NewApprove(); 
                    kctz.MdiParent = this; 
                    kctz.Parent = this.splitContainer1.Panel2;
                    kctz.WindowState = FormWindowState.Maximized;
                    kctz.Show();
                    break;

               

                case "营业收件明细":
                    foreach (Form frm in this.splitContainer1.Panel2.Controls)
                    {
                        if (frm is LY_repair_query)
                        {
                            frm.WindowState = FormWindowState.Maximized;
                            frm.Activate();
                            return;
                        }
                    }

                    LY_repair_query dwp222 = new LY_repair_query();

                    dwp222.MdiParent = this;

                    dwp222.Parent = this.splitContainer1.Panel2;
                    dwp222.WindowState = FormWindowState.Maximized;
                    dwp222.Show(); 
                    break;


                case "报废库存查询":
                    foreach (Form frm in this.splitContainer1.Panel2.Controls)
                    {
                        if (frm is LY_Waste_InOutQuery)
                        {
                            frm.WindowState = FormWindowState.Maximized;
                            frm.Activate();
                            return;
                        }
                    }

                    LY_Waste_InOutQuery wasteQu = new LY_Waste_InOutQuery();
                     
                    wasteQu.MdiParent = this;

                    wasteQu.Parent = this.splitContainer1.Panel2;
                    wasteQu.WindowState = FormWindowState.Maximized;
                    wasteQu.Show();
 

                    break;

                case "维修网点库存":
                    foreach (Form frm in this.splitContainer1.Panel2.Controls)
                    {
                        if (frm is LY_Maintenance_InOutQuery)
                        {
                            frm.WindowState = FormWindowState.Maximized;
                            frm.Activate();
                            return;
                        }
                    }

                    LY_Maintenance_InOutQuery wxwdkc = new LY_Maintenance_InOutQuery();

                    wxwdkc.MdiParent = this;

                    wxwdkc.Parent = this.splitContainer1.Panel2;
                    wxwdkc.WindowState = FormWindowState.Maximized;
                    wxwdkc.Show();


                    break;



                case "机加序间在线统计":
                    foreach (Form frm in this.splitContainer1.Panel2.Controls)
                    {
                        if (frm is Get_ONline_CountRep)
                        {
                            frm.WindowState = FormWindowState.Maximized;
                            frm.Activate();
                            return;
                        }
                    }

                    Get_ONline_CountRep Get_ONline_Count = new Get_ONline_CountRep();

                    Get_ONline_Count.MdiParent = this;

                    Get_ONline_Count.Parent = this.splitContainer1.Panel2;
                    Get_ONline_Count.WindowState = FormWindowState.Maximized;
                    Get_ONline_Count.Show();


                    break;
                case "物料计划":
                    foreach (Form frm in this.splitContainer1.Panel2.Controls)
                    {
                        if (frm is LY_Material_Plan)
                        {
                            frm.WindowState = FormWindowState.Maximized;
                            frm.Activate();
                            return;
                        }
                    }

                    LY_Material_Plan dwp = new LY_Material_Plan();

                    //dwp.MainMenu = this.menuStrip1;
                    //dwp.Parent_model = ((System.Windows.Forms.ToolStripItem)(sender)).OwnerItem.Text;
                    dwp.MdiParent = this;

                    dwp.Parent = this.splitContainer1.Panel2;
                    dwp.WindowState = FormWindowState.Maximized;
                    dwp.Show();

                    //dwp.StartPosition = FormStartPosition.CenterScreen;

                    break;
                case "生产计划":
                    foreach (Form frm in this.splitContainer1.Panel2.Controls)
                    {
                        if (frm is LY_Product_Plan)
                        {
                            frm.WindowState = FormWindowState.Maximized;
                            frm.Activate();
                            return;
                        }
                    }

                    LY_Product_Plan sumanaly = new LY_Product_Plan();

                    //dwp.MainMenu = this.menuStrip1;
                    //dwp.Parent_model = ((System.Windows.Forms.ToolStripItem)(sender)).OwnerItem.Text;
                    sumanaly.MdiParent = this;

                    sumanaly.Parent = this.splitContainer1.Panel2;
                    sumanaly.WindowState = FormWindowState.Maximized;
                    sumanaly.Show();

                    //dwp.StartPosition = FormStartPosition.CenterScreen;

                    break;

                case "生产计划产品":
                    foreach (Form frm in this.splitContainer1.Panel2.Controls)
                    {
                        if (frm is LY_Product_Plan_EndProdut)
                        {
                            frm.WindowState = FormWindowState.Maximized;
                            frm.Activate();
                            return;
                        }
                    }

                    LY_Product_Plan_EndProdut ppp = new LY_Product_Plan_EndProdut();

                    //dwp.MainMenu = this.menuStrip1;
                    //dwp.Parent_model = ((System.Windows.Forms.ToolStripItem)(sender)).OwnerItem.Text;
                    ppp.MdiParent = this;

                    ppp.Parent = this.splitContainer1.Panel2;
                    ppp.WindowState = FormWindowState.Maximized;
                    ppp.Show();

                    //dwp.StartPosition = FormStartPosition.CenterScreen;

                    break;



                case "钳装领退料":
                    foreach (Form frm in this.splitContainer1.Panel2.Controls)
                    {
                        if (frm is LY_Pliers_Assembly_Out)
                        {
                            frm.WindowState = FormWindowState.Maximized;
                            frm.Activate();
                            return;
                        }
                    }

                    LY_Pliers_Assembly_Out aa1 = new LY_Pliers_Assembly_Out();


                    aa1.MdiParent = this;

                    aa1.Parent = this.splitContainer1.Panel2;
                    aa1.WindowState = FormWindowState.Maximized;
                    aa1.Show();


                    break;



                case "电装领退料":
                    foreach (Form frm in this.splitContainer1.Panel2.Controls)
                    {
                        if (frm is LY_Pliers_Assembly_Out_DZ)
                        {
                            frm.WindowState = FormWindowState.Maximized;
                            frm.Activate();
                            return;
                        }
                    }

                    LY_Pliers_Assembly_Out_DZ aa21 = new LY_Pliers_Assembly_Out_DZ();


                    aa21.MdiParent = this;

                    aa21.Parent = this.splitContainer1.Panel2;
                    aa21.WindowState = FormWindowState.Maximized;
                    aa21.Show();


                    break;


                case "物料信息":
                    foreach (Form frm in this.splitContainer1.Panel2.Controls)
                    {
                        if (frm is LY_MaterialMange)
                        {
                            frm.WindowState = FormWindowState.Maximized;
                            frm.Activate();
                            return;
                        }
                    }

                    LY_MaterialMange SellBalance = new LY_MaterialMange();

                    //dwp.MainMenu = this.menuStrip1;
                    //dwp.Parent_model = ((System.Windows.Forms.ToolStripItem)(sender)).OwnerItem.Text;
                    SellBalance.MdiParent = this;

                    SellBalance.Parent = this.splitContainer1.Panel2;
                    SellBalance.WindowState = FormWindowState.Maximized;
                    SellBalance.Show();

                    //dwp.StartPosition = FormStartPosition.CenterScreen;

                    break;
                /////////////////////////////////////////////////装配工时设置
                case "装配工时设置":
                    foreach (Form frm in this.splitContainer1.Panel2.Controls)
                    {
                        if (frm is LY_AssemblyTime_Mange)
                        {
                            frm.WindowState = FormWindowState.Maximized;
                            frm.Activate();
                            return;
                        }
                    }

                    LY_AssemblyTime_Mange AssemblyTime = new LY_AssemblyTime_Mange();

                    //dwp.MainMenu = this.menuStrip1;
                    //dwp.Parent_model = ((System.Windows.Forms.ToolStripItem)(sender)).OwnerItem.Text;

                    AssemblyTime.MdiParent = this;

                    AssemblyTime.Parent = this.splitContainer1.Panel2;
                    AssemblyTime.WindowState = FormWindowState.Maximized;
                    AssemblyTime.Show();

                    //dwp.StartPosition = FormStartPosition.CenterScreen;

                    break;

                /////////////////////////////////////////////////////////////////////////////

                case "图纸管理":
                    foreach (Form frm in this.splitContainer1.Panel2.Controls)
                    {
                        if (frm is LY_Drawing_Manage)
                        {
                            frm.WindowState = FormWindowState.Maximized;
                            frm.Activate();
                            return;
                        }
                    }

                    LY_Drawing_Manage drawingmamage = new LY_Drawing_Manage();

                    //dwp.MainMenu = this.menuStrip1;
                    //dwp.Parent_model = ((System.Windows.Forms.ToolStripItem)(sender)).OwnerItem.Text;
                    drawingmamage.MdiParent = this;

                    drawingmamage.Parent = this.splitContainer1.Panel2;
                    drawingmamage.WindowState = FormWindowState.Maximized;
                    drawingmamage.Show();

                    //dwp.StartPosition = FormStartPosition.Center发票Screen;

                    break;
                /////////////////////////////////////////////////////////////////////////////

                case "应付业务":
                    foreach (Form frm in this.splitContainer1.Panel2.Controls)
                    {
                        if (frm is LY_Payable_Manage)
                        {
                            frm.WindowState = FormWindowState.Maximized;
                            frm.Activate();
                            return;
                        }
                    }

                    LY_Payable_Manage payable = new LY_Payable_Manage();

                    //dwp.MainMenu = this.menuStrip1;
                    //dwp.Parent_model = ((System.Windows.Forms.ToolStripItem)(sender)).OwnerItem.Text;
                    payable.MdiParent = this;

                    payable.Parent = this.splitContainer1.Panel2;
                    payable.WindowState = FormWindowState.Maximized;
                    payable.Show();

                    //dwp.StartPosition = FormStartPosition.CenterScreen;

                    break;
                /////////////////////////////////////////////////////////////////////////////

                case "客户往来账业务":
                    foreach (Form frm in this.splitContainer1.Panel2.Controls)
                    {
                        if (frm is LY_Receivable_Manage)
                        {
                            frm.WindowState = FormWindowState.Maximized;
                            frm.Activate();
                            return;
                        }
                    }

                    LY_Receivable_Manage receivable = new LY_Receivable_Manage(); 
                    receivable.MdiParent = this; 
                    receivable.Parent = this.splitContainer1.Panel2;
                    receivable.WindowState = FormWindowState.Maximized;
                    receivable.Show(); 
                    break;
                case "客户往来账查询":
                    foreach (Form frm in this.splitContainer1.Panel2.Controls)
                    {
                        if (frm is LY_Receivable_Manage)
                        {
                            frm.WindowState = FormWindowState.Maximized;
                            frm.Activate();
                            return;
                        }
                    }

                    LY_Receivable_Manage receivable2 = new LY_Receivable_Manage(); 
                    receivable2.MdiParent = this;
                               
                    receivable2.Parent = this.splitContainer1.Panel2;
                    receivable2.WindowState = FormWindowState.Maximized;
                    receivable2.Show(); 
                    break;
                /////////////////////////////////////////////////////////////////////
                case "图纸管理记录":
                    foreach (Form frm in this.splitContainer1.Panel2.Controls)
                    {
                        if (frm is LY_Drawing_ManageRec)
                        {
                            frm.WindowState = FormWindowState.Maximized;
                            frm.Activate();
                            return;
                        }
                    }

                    LY_Drawing_ManageRec drawingmamageRec = new LY_Drawing_ManageRec();

                    //dwp.MainMenu = this.menuStrip1;
                    //dwp.Parent_model = ((System.Windows.Forms.ToolStripItem)(sender)).OwnerItem.Text;
                    // test  
                    drawingmamageRec.MdiParent = this;

                    drawingmamageRec.Parent = this.splitContainer1.Panel2;
                    drawingmamageRec.WindowState = FormWindowState.Maximized;
                    drawingmamageRec.Show();

                    //dwp.StartPosition = FormStartPosition.CenterScreen;

                    break;
                    

                case "财务大类设置":
                    foreach (Form frm in this.splitContainer1.Panel2.Controls)
                    {
                        if (frm is t_financial_type)
                        {
                            frm.WindowState = FormWindowState.Maximized;
                            frm.Activate();
                            return;
                        }
                    }

                    t_financial_type cwdlsz = new t_financial_type();

                    cwdlsz.MdiParent = this;

                    cwdlsz.Parent = this.splitContainer1.Panel2;
                    cwdlsz.WindowState = FormWindowState.Maximized;
                    cwdlsz.Show();


                    break;
                case "客户名称校对":
                    foreach (Form frm in this.splitContainer1.Panel2.Controls)
                    {
                        if (frm is t_cilent_name)
                        {
                            frm.WindowState = FormWindowState.Maximized;
                            frm.Activate();
                            return;
                        }
                    }

                    t_cilent_name t_cilent_name = new t_cilent_name();

                    t_cilent_name.MdiParent = this;

                    t_cilent_name.Parent = this.splitContainer1.Panel2;
                    t_cilent_name.WindowState = FormWindowState.Maximized;
                    t_cilent_name.Show();


                    break;

                case "产品系列设置":
                    foreach (Form frm in this.splitContainer1.Panel2.Controls)
                    {
                        if (frm is ly_inma0010_series)
                        {
                            frm.WindowState = FormWindowState.Maximized;
                            frm.Activate();
                            return;
                        }
                    }

                    ly_inma0010_series cpxl = new ly_inma0010_series();

                    cpxl.MdiParent = this;

                    cpxl.Parent = this.splitContainer1.Panel2;
                    cpxl.WindowState = FormWindowState.Maximized;
                    cpxl.Show();


                    break;




                ///////////////////////////////////////////////////////////////////////
                case "WIP":
                    foreach (Form frm in this.splitContainer1.Panel2.Controls)
                    {
                        if (frm is LY_WIP)
                        {
                            frm.WindowState = FormWindowState.Maximized;
                            frm.Activate();
                            return;
                        }
                    }

                    LY_WIP wip = new LY_WIP();

                    wip.MdiParent = this;

                    wip.Parent = this.splitContainer1.Panel2;
                    wip.WindowState = FormWindowState.Maximized;
                    wip.Show();
 

                    break;

                ///////////////////////////////////////////////////////////////////////
                case "图纸级别设置":
                    foreach (Form frm in this.splitContainer1.Panel2.Controls)
                    {
                        if (frm is LY_Drawing_levelSet)
                        {
                            frm.WindowState = FormWindowState.Maximized;
                            frm.Activate();
                            return;
                        }
                    }

                    LY_Drawing_levelSet drawinglevelSet = new LY_Drawing_levelSet();

                    //dwp.MainMenu = this.menuStrip1;
                    //dwp.Parent_model = ((System.Windows.Forms.ToolStripItem)(sender)).OwnerItem.Text;
                    drawinglevelSet.MdiParent = this;

                    drawinglevelSet.Parent = this.splitContainer1.Panel2;
                    drawinglevelSet.WindowState = FormWindowState.Maximized;
                    drawinglevelSet.Show();

                    //dwp.StartPosition = FormStartPosition.CenterScreen;

                    break;

                ///////////////////////////////////////////////////////////////////////
                case "物料结构BOM":
                    foreach (Form frm in this.splitContainer1.Panel2.Controls)
                    {
                        if (frm is LY_MaterialBom)
                        {
                            frm.WindowState = FormWindowState.Maximized;
                            frm.Activate();
                            return;
                        }
                    }

                    LY_MaterialBom clientPayment_Auditing = new LY_MaterialBom();

                    //dwp.MainMenu = this.menuStrip1;
                    //dwp.Parent_model = ((System.Windows.Forms.ToolStripItem)(sender)).OwnerItem.Text;
                    clientPayment_Auditing.MdiParent = this;

                    clientPayment_Auditing.Parent = this.splitContainer1.Panel2;
                    clientPayment_Auditing.WindowState = FormWindowState.Maximized;
                    clientPayment_Auditing.Show();

                    //dwp.StartPosition = FormStartPosition.CenterScreen;

                    break;

                ///////////////////////////////////////////////////////////////////////
                case "物料结构查询BOM":
                    foreach (Form frm in this.splitContainer1.Panel2.Controls)
                    {
                        if (frm is LY_MaterialBom_query)
                        {
                            frm.WindowState = FormWindowState.Maximized;
                            frm.Activate();
                            return;
                        }
                    }

                    LY_MaterialBom_query bom_query = new LY_MaterialBom_query();

                    //dwp.MainMenu = this.menuStrip1;
                    //dwp.Parent_model = ((System.Windows.Forms.ToolStripItem)(sender)).OwnerItem.Text;
                    bom_query.MdiParent = this;

                    bom_query.Parent = this.splitContainer1.Panel2;
                    bom_query.WindowState = FormWindowState.Maximized;
                    bom_query.Show();

                    //dwp.StartPosition = FormStartPosition.CenterScreen;

                    break;

                ///////////////////////////////////////////////////////////////////////

                case "改制BOM":
                    foreach (Form frm in this.splitContainer1.Panel2.Controls)
                    {
                        if (frm is LY_Restructuring_BOM_Mange)
                        {
                            frm.WindowState = FormWindowState.Maximized;
                            frm.Activate();
                            return;
                        }
                    }

                    LY_Restructuring_BOM_Mange RestructuringBom = new LY_Restructuring_BOM_Mange();

                    //dwp.MainMenu = this.menuStrip1;
                    //dwp.Parent_model = ((System.Windows.Forms.ToolStripItem)(sender)).OwnerItem.Text;
                    RestructuringBom.MdiParent = this;

                    RestructuringBom.Parent = this.splitContainer1.Panel2;
                    RestructuringBom.WindowState = FormWindowState.Maximized;
                    RestructuringBom.Show();

                    //dwp.StartPosition = FormStartPosition.CenterScreen;

                    break;

                ///////////////////////////////////////////////////////////////////////

                case "BOM反查":
                    foreach (Form frm in this.splitContainer1.Panel2.Controls)
                    {
                        if (frm is LY_MaterialBom_REV)
                        {
                            frm.WindowState = FormWindowState.Maximized;
                            frm.Activate();
                            return;
                        }
                    }

                    LY_MaterialBom_REV BomRev = new LY_MaterialBom_REV();

                    //dwp.MainMenu = this.menuStrip1;
                    //dwp.Parent_model = ((System.Windows.Forms.ToolStripItem)(sender)).OwnerItem.Text;
                    BomRev.MdiParent = this;

                    BomRev.Parent = this.splitContainer1.Panel2;
                    BomRev.WindowState = FormWindowState.Maximized;
                    BomRev.Show();

                    //dwp.StartPosition = FormStartPosition.CenterScreen;

                    break;

                ///////////////////////////////////////////////////////////////////////
                case "物料成本BOM":
                    foreach (Form frm in this.splitContainer1.Panel2.Controls)
                    {
                        if (frm is LY_MaterialBom_Price)
                        {
                            frm.WindowState = FormWindowState.Maximized;
                            frm.Activate();
                            return;
                        }
                    }

                    LY_MaterialBom_Price lmbmcb = new LY_MaterialBom_Price();

                    //dwp.MainMenu = this.menuStrip1;
                    //dwp.Parent_model = ((System.Windows.Forms.ToolStripItem)(sender)).OwnerItem.Text;
                    lmbmcb.MdiParent = this;

                    lmbmcb.Parent = this.splitContainer1.Panel2;
                    lmbmcb.WindowState = FormWindowState.Maximized;
                    lmbmcb.Show();

                    //dwp.StartPosition = FormStartPosition.CenterScreen;

                    break;
                ///////////////////////////////////////////////////////////////////////成品添加

                /////物料成本BOM_NoVat
                ///////////////////////////////////////////////////////////////////////
                case "物料成本BOM_NoVat":
                    foreach (Form frm in this.splitContainer1.Panel2.Controls)
                    {
                        if (frm is LY_MaterialBom_Price_noVat)
                        {
                            frm.WindowState = FormWindowState.Maximized;
                            frm.Activate();
                            return;
                        }
                    }

                    LY_MaterialBom_Price_noVat novat = new LY_MaterialBom_Price_noVat();

                    //dwp.MainMenu = this.menuStrip1;
                    //dwp.Parent_model = ((System.Windows.Forms.ToolStripItem)(sender)).OwnerItem.Text;
                    novat.MdiParent = this;

                    novat.Parent = this.splitContainer1.Panel2;
                    novat.WindowState = FormWindowState.Maximized;
                    novat.Show();

                    //dwp.StartPosition = FormStartPosition.CenterScreen;

                    break;

                //------------------------------------



                case "物料成本BOM_NoVatTrans":
                    foreach (Form frm in this.splitContainer1.Panel2.Controls)
                    {
                        if (frm is LY_MaterialBom_Price_noVat_Trans)
                        {
                            frm.WindowState = FormWindowState.Maximized;
                            frm.Activate();
                            return;
                        }
                    }

                    LY_MaterialBom_Price_noVat_Trans novatTrans = new LY_MaterialBom_Price_noVat_Trans();


                    novatTrans.MdiParent = this;

                    novatTrans.Parent = this.splitContainer1.Panel2;
                    novatTrans.WindowState = FormWindowState.Maximized;
                    novatTrans.Show();

                    //dwp.StartPosition = FormStartPosition.CenterScreen;

                    break;


                //------------------------------------

                case "物料成本BOM_L":
                    foreach (Form frm in this.splitContainer1.Panel2.Controls)
                    {
                        if (frm is LY_MaterialBom_Price_noVat_L)
                        {
                            frm.WindowState = FormWindowState.Maximized;
                            frm.Activate();
                            return;
                        }
                    }

                    LY_MaterialBom_Price_noVat_L novatLab = new LY_MaterialBom_Price_noVat_L();


                    novatLab.MdiParent = this;

                    novatLab.Parent = this.splitContainer1.Panel2;
                    novatLab.WindowState = FormWindowState.Maximized;
                    novatLab.Show();

                    //dwp.StartPosition = FormStartPosition.CenterScreen;

                    break;


                //------------------------------------









                case "物料成本BOM_NoVat2":
                    foreach (Form frm in this.splitContainer1.Panel2.Controls)
                    {
                        if (frm is LY_MaterialBom_Price_noVat2)
                        {
                            frm.WindowState = FormWindowState.Maximized;
                            frm.Activate();
                            return;
                        }
                    }

                    LY_MaterialBom_Price_noVat2 novat2 = new LY_MaterialBom_Price_noVat2();

                    //dwp.MainMenu = this.menuStrip1;
                    //dwp.Parent_model = ((System.Windows.Forms.ToolStripItem)(sender)).OwnerItem.Text;
                    novat2.MdiParent = this;

                    novat2.Parent = this.splitContainer1.Panel2;
                    novat2.WindowState = FormWindowState.Maximized;
                    novat2.Show();

                    //dwp.StartPosition = FormStartPosition.CenterScreen;

                    break;
                case "成品添加":
                    foreach (Form frm in this.splitContainer1.Panel2.Controls)
                    {
                        if (frm is LY_Material_salesprice)
                        {
                            frm.WindowState = FormWindowState.Maximized;
                            frm.Activate();
                            return;
                        }
                    }

                    LY_Material_salesprice Material_chengpin = new LY_Material_salesprice();

                    //dwp.MainMenu = this.menuStrip1;
                    //dwp.Parent_model = ((System.Windows.Forms.ToolStripItem)(sender)).OwnerItem.Text;
                    Material_chengpin.MdiParent = this;

                    Material_chengpin.Parent = this.splitContainer1.Panel2;
                    Material_chengpin.WindowState = FormWindowState.Maximized;
                    Material_chengpin.Show();

                    //dwp.StartPosition = FormStartPosition.CenterScreen;

                    break;
                ///////////////////////////////////////////////////////////////////////

                ///////////////////////////////////////////////////////////////////////成品添加
                case "附件管理":
                    foreach (Form frm in this.splitContainer1.Panel2.Controls)
                    {
                        if (frm is LY_Material_apprend)
                        {
                            frm.WindowState = FormWindowState.Maximized;
                            frm.Activate();
                            return;
                        }
                    }

                    LY_Material_apprend Material_fujian = new LY_Material_apprend();

                    //dwp.MainMenu = this.menuStrip1;
                    //dwp.Parent_model = ((System.Windows.Forms.ToolStripItem)(sender)).OwnerItem.Text;
                    Material_fujian.MdiParent = this;

                    Material_fujian.Parent = this.splitContainer1.Panel2;
                    Material_fujian.WindowState = FormWindowState.Maximized;
                    Material_fujian.Show();

                    //dwp.StartPosition = FormStartPosition.CenterScreen;

                    break;
                ///////////////////////////////////////////////////////////////////////
                case "机加结构BOM":
                    foreach (Form frm in this.splitContainer1.Panel2.Controls)
                    {
                        if (frm is LY_MaterialBom_Machine)
                        {
                            frm.WindowState = FormWindowState.Maximized;
                            frm.Activate();
                            return;
                        }
                    }

                    LY_MaterialBom_Machine lmbm = new LY_MaterialBom_Machine();

                    //dwp.MainMenu = this.menuStrip1;
                    //dwp.Parent_model = ((System.Windows.Forms.ToolStripItem)(sender)).OwnerItem.Text;
                    lmbm.MdiParent = this;

                    lmbm.Parent = this.splitContainer1.Panel2;
                    lmbm.WindowState = FormWindowState.Maximized;
                    lmbm.Show();

                    //dwp.StartPosition = FormStartPosition.CenterScreen;

                    break;
              
                   //////////////////////////////////////////////////////////////////////////////
                case "代料管理":
                    foreach (Form frm in this.splitContainer1.Panel2.Controls)
                    {
                        if (frm is LY_Material_Replacemange)
                        {
                            frm.WindowState = FormWindowState.Maximized;
                            frm.Activate();
                            return;
                        }
                    }

                    LY_Material_Replacemange lmrm = new LY_Material_Replacemange();

                    //dwp.MainMenu = this.menuStrip1;
                    //dwp.Parent_model = ((System.Windows.Forms.ToolStripItem)(sender)).OwnerItem.Text;
                    lmrm.MdiParent = this;

                    lmrm.Parent = this.splitContainer1.Panel2;
                    lmrm.WindowState = FormWindowState.Maximized;
                    lmrm.Show();

                    //dwp.StartPosition = FormStartPosition.CenterScreen;

                    break;
                //////////////////////////////////////////////////////////////////////////////
                case "询价管理":
                    foreach (Form frm in this.splitContainer1.Panel2.Controls)
                    {
                        if (frm is LY_Material_Queryprice)
                        {
                            frm.WindowState = FormWindowState.Maximized;
                            frm.Activate();
                            return;
                        }
                    }

                    LY_Material_Queryprice lmqp = new LY_Material_Queryprice();

                    //dwp.MainMenu = this.menuStrip1;
                    //dwp.Parent_model = ((System.Windows.Forms.ToolStripItem)(sender)).OwnerItem.Text;
                    lmqp.MdiParent = this;

                    lmqp.Parent = this.splitContainer1.Panel2;
                    lmqp.WindowState = FormWindowState.Maximized;
                    lmqp.Show();

                    //dwp.StartPosition = FormStartPosition.CenterScreen;

                    break;

                case "借用物料管理":
                    foreach (Form frm in this.splitContainer1.Panel2.Controls)
                    {
                        if (frm is LY_borrow_store)
                        {
                            frm.WindowState = FormWindowState.Maximized;
                            frm.Activate();
                            return;
                        }
                    }

                    LY_borrow_store bow = new LY_borrow_store(); 
                    bow.MdiParent = this; 
                    bow.Parent = this.splitContainer1.Panel2;
                    bow.WindowState = FormWindowState.Maximized;
                    bow.Show(); 

                    break;
                case "盘点管理":
                    foreach (Form frm in this.splitContainer1.Panel2.Controls)
                    {
                        if (frm is LY_StockTake)
                        {
                            frm.WindowState = FormWindowState.Maximized;
                            frm.Activate();
                            return;
                        }
                    }

                    LY_StockTake lst = new LY_StockTake();

                    //dwp.MainMenu = this.menuStrip1;
                    //dwp.Parent_model = ((System.Windows.Forms.ToolStripItem)(sender)).OwnerItem.Text;
                    lst.MdiParent = this;

                    lst.Parent = this.splitContainer1.Panel2;
                    lst.WindowState = FormWindowState.Maximized;
                    lst.Show();

                    //dwp.StartPosition = FormStartPosition.CenterScreen;

                    break;
                case "领料计划":
                    foreach (Form frm in this.splitContainer1.Panel2.Controls)
                    {
                        if (frm is LY_DayGet_Plan_New)
                        {
                            frm.WindowState = FormWindowState.Maximized;
                            frm.Activate();
                            return;
                        }
                    }

                    LY_DayGet_Plan_New clientPayment = new LY_DayGet_Plan_New();

                    //dwp.MainMenu = this.menuStrip1;
                    //dwp.Parent_model = ((System.Windows.Forms.ToolStripItem)(sender)).OwnerItem.Text;
                    clientPayment.MdiParent = this;

                    clientPayment.Parent = this.splitContainer1.Panel2;
                    clientPayment.WindowState = FormWindowState.Maximized;
                    clientPayment.Show();

                    //dwp.StartPosition = FormStartPosition.CenterScreen;

                    break;
                ////////////////////////////////////
                ///

                case "改型号计划":


                    foreach (Form frm in this.splitContainer1.Panel2.Controls)
                    {
                        if (frm is LY_Gxh_Plan)
                        {
                            frm.WindowState = FormWindowState.Maximized;
                            frm.Activate();
                            return;
                        }
                    }

                    LY_Gxh_Plan LY_Gxh_Plan = new LY_Gxh_Plan();
                    LY_Gxh_Plan.MdiParent = this; 
                    LY_Gxh_Plan.Parent = this.splitContainer1.Panel2;
                    LY_Gxh_Plan.WindowState = FormWindowState.Maximized;
                    LY_Gxh_Plan.Show(); 
                    break;


                case "改型号出入库":


                    foreach (Form frm in this.splitContainer1.Panel2.Controls)
                    {
                        if (frm is LY_Combination_INOut)
                        {
                            frm.WindowState = FormWindowState.Maximized;
                            frm.Activate();
                            return;
                        }
                    }

                    LY_Combination_INOut LY_Gxh_PlanOut = new LY_Combination_INOut();
                    LY_Gxh_PlanOut.MdiParent = this;
                    LY_Gxh_PlanOut.Parent = this.splitContainer1.Panel2;
                    LY_Gxh_PlanOut.WindowState = FormWindowState.Maximized;
                    LY_Gxh_PlanOut.Show();
                    break;
                ////////////////////////////////////

                case "改制计划":
                    foreach (Form frm in this.splitContainer1.Panel2.Controls)
                    {
                        if (frm is LY_Restructuring_Plan)
                        {
                            frm.WindowState = FormWindowState.Maximized;
                            frm.Activate();
                            return;
                        }
                    }

                    LY_Restructuring_Plan restructuringPlan = new LY_Restructuring_Plan();

                    //dwp.MainMenu = this.menuStrip1;
                    //dwp.Parent_model = ((System.Windows.Forms.ToolStripItem)(sender)).OwnerItem.Text;
                    restructuringPlan.MdiParent = this;

                    restructuringPlan.Parent = this.splitContainer1.Panel2;
                    restructuringPlan.WindowState = FormWindowState.Maximized;
                    restructuringPlan.Show();

                    //dwp.StartPosition = FormStartPosition.CenterScreen;

                    break;

                case "改制计划查看":
                    foreach (Form frm in this.splitContainer1.Panel2.Controls)
                    {
                        if (frm is LY_Restructuring_Plan_view)
                        {
                            frm.WindowState = FormWindowState.Maximized;
                            frm.Activate();
                            return;
                        }
                    }

                    LY_Restructuring_Plan_view restructuringPlan2 = new LY_Restructuring_Plan_view();
 
                    restructuringPlan2.MdiParent = this;

                    restructuringPlan2.Parent = this.splitContainer1.Panel2;
                    restructuringPlan2.WindowState = FormWindowState.Maximized;
                    restructuringPlan2.Show();

                    //dwp.StartPosition = FormStartPosition.CenterScreen;

                    break;
                case "临时配套":
                    foreach (Form frm in this.splitContainer1.Panel2.Controls)
                    {
                        if (frm is LY_Product_PlanTemp)
                        {
                            frm.WindowState = FormWindowState.Maximized;
                            frm.Activate();
                            return;
                        }
                    }

                    LY_Product_PlanTemp tempplan = new LY_Product_PlanTemp();

                    //dwp.MainMenu = this.menuStrip1;
                    //dwp.Parent_model = ((System.Windows.Forms.ToolStripItem)(sender)).OwnerItem.Text;
                    tempplan.MdiParent = this;

                    tempplan.Parent = this.splitContainer1.Panel2;
                    tempplan.WindowState = FormWindowState.Maximized;
                    tempplan.Show();

                    //dwp.StartPosition = FormStartPosition.CenterScreen;

                    break;

                case "生产配套":
                    foreach (Form frm in this.splitContainer1.Panel2.Controls)
                    {
                        if (frm is LY_Product_PlanTemp_pro)
                        {
                            frm.WindowState = FormWindowState.Maximized;
                            frm.Activate();
                            return;
                        }
                    }

                    LY_Product_PlanTemp_pro tempplanpro = new LY_Product_PlanTemp_pro();

                    //dwp.MainMenu = this.menuStrip1;
                    //dwp.Parent_model = ((System.Windows.Forms.ToolStripItem)(sender)).OwnerItem.Text;
                    tempplanpro.MdiParent = this;

                    tempplanpro.Parent = this.splitContainer1.Panel2;
                    tempplanpro.WindowState = FormWindowState.Maximized;
                    tempplanpro.Show();

                    //dwp.StartPosition = FormStartPosition.CenterScreen;

                    break;
               
                //销售综合查询
                case "领料出库":
                    foreach (Form frm in this.splitContainer1.Panel2.Controls)
                    {
                        if (frm is LY_Store_Out)
                        {
                            frm.WindowState = FormWindowState.Maximized;
                            frm.Activate();
                            return;
                        }
                    }

                    LY_Store_Out loan_Audiying = new LY_Store_Out();

                    //dwp.MainMenu = this.menuStrip1;
                    //dwp.Parent_model = ((System.Windows.Forms.ToolStripItem)(sender)).OwnerItem.Text;
                    loan_Audiying.MdiParent = this;

                    loan_Audiying.Parent = this.splitContainer1.Panel2;
                    loan_Audiying.WindowState = FormWindowState.Maximized;
                    loan_Audiying.Show();

                    //dwp.StartPosition = FormStartPosition.CenterScreen;

                    break;

                case "领料出库计划":
                    foreach (Form frm in this.splitContainer1.Panel2.Controls)
                    {
                        if (frm is LY_Store_Out_Department)
                        {
                            frm.WindowState = FormWindowState.Maximized;
                            frm.Activate();
                            return;
                        }
                    }

                    LY_Store_Out_Department out_dep = new LY_Store_Out_Department();

                    //dwp.MainMenu = this.menuStrip1;
                    //dwp.Parent_model = ((System.Windows.Forms.ToolStripItem)(sender)).OwnerItem.Text;
                    out_dep.MdiParent = this;

                    out_dep.Parent = this.splitContainer1.Panel2;
                    out_dep.WindowState = FormWindowState.Maximized;
                    out_dep.Show();

                    //dwp.StartPosition = FormStartPosition.CenterScreen;

                    break;

                case "物料调拨":
                    foreach (Form frm in this.splitContainer1.Panel2.Controls)
                    {
                        if (frm is LY_Store_Out_Transfer)
                        {
                            frm.WindowState = FormWindowState.Maximized;
                            frm.Activate();
                            return;
                        }
                    }

                    LY_Store_Out_Transfer out_transfer = new LY_Store_Out_Transfer();

                    //dwp.MainMenu = this.menuStrip1;
                    //dwp.Parent_model = ((System.Windows.Forms.ToolStripItem)(sender)).OwnerItem.Text;
                    out_transfer.MdiParent = this;

                    out_transfer.Parent = this.splitContainer1.Panel2;
                    out_transfer.WindowState = FormWindowState.Maximized;
                    out_transfer.Show();

                    //dwp.StartPosition = FormStartPosition.CenterScreen;

                    break;
                case "领料出库日常":
                    foreach (Form frm in this.splitContainer1.Panel2.Controls)
                    {
                        if (frm is LY_Store_OutDaily)
                        {
                            frm.WindowState = FormWindowState.Maximized;
                            frm.Activate();
                            return;
                        }
                    }

                    LY_Store_OutDaily out_daily = new LY_Store_OutDaily();

                    //dwp.MainMenu = this.menuStrip1;
                    //dwp.Parent_model = ((System.Windows.Forms.ToolStripItem)(sender)).OwnerItem.Text;
                    out_daily.MdiParent = this;

                    out_daily.Parent = this.splitContainer1.Panel2;
                    out_daily.WindowState = FormWindowState.Maximized;
                    out_daily.Show();

                    //dwp.StartPosition = FormStartPosition.CenterScreen;

                    break;
                case "报废出库日常":
                    foreach (Form frm in this.splitContainer1.Panel2.Controls)
                    {
                        if (frm is LY_Store_OutWaste)
                        {
                            frm.WindowState = FormWindowState.Maximized;
                            frm.Activate();
                            return;
                        }
                    }
                    LY_Store_OutWaste out_waste = new LY_Store_OutWaste();
                    out_waste.MdiParent = this;
                    out_waste.Parent = this.splitContainer1.Panel2;
                    out_waste.WindowState = FormWindowState.Maximized;
                    out_waste.Show();

                    break;

                case "物料入库":
                    foreach (Form frm in this.splitContainer1.Panel2.Controls)
                    {
                        if (frm is LY_Store_In)
                        {
                            frm.WindowState = FormWindowState.Maximized;
                            frm.Activate();
                            return;
                        }
                    }

                    LY_Store_In sas = new LY_Store_In();

                   
                    sas.MdiParent = this;

                    sas.Parent = this.splitContainer1.Panel2;
                    sas.WindowState = FormWindowState.Maximized;
                    sas.Show();

                    //dwp.StartPosition = FormStartPosition.CenterScreen;

                    break;

                case "机加生产入库":
                    foreach (Form frm in this.splitContainer1.Panel2.Controls)
                    {
                        if (frm is LY_Store_In_JG)
                        {
                            frm.WindowState = FormWindowState.Maximized;
                            frm.Activate();
                            return;
                        }
                    }

                    LY_Store_In_JG sasjg = new LY_Store_In_JG();


                    sasjg.MdiParent = this;

                    sasjg.Parent = this.splitContainer1.Panel2;
                    sasjg.WindowState = FormWindowState.Maximized;
                    sasjg.Show();

                    //dwp.StartPosition = FormStartPosition.CenterScreen;

                    break;

                case "机加生产领料":
                    foreach (Form frm in this.splitContainer1.Panel2.Controls)
                    {
                        if (frm is LY_Store_Out_JG)
                        {
                            frm.WindowState = FormWindowState.Maximized;
                            frm.Activate();
                            return;
                        }
                    }


                    LY_Store_Out_JG sasjgout = new LY_Store_Out_JG();


                    sasjgout.MdiParent = this;

                    sasjgout.Parent = this.splitContainer1.Panel2;
                    sasjgout.WindowState = FormWindowState.Maximized;
                    sasjgout.Show();

                    //dwp.StartPosition = FormStartPosition.CenterScreen;

                    break;
                case "外协领料":
                    foreach (Form frm in this.splitContainer1.Panel2.Controls)
                    {
                        if (frm is LY_Store_Out_Outsource)
                        {
                            frm.WindowState = FormWindowState.Maximized;
                            frm.Activate();
                            return;
                        }
                    }

                    LY_Store_Out_Outsource saswxout = new LY_Store_Out_Outsource();


                    saswxout.MdiParent = this;

                    saswxout.Parent = this.splitContainer1.Panel2;
                    saswxout.WindowState = FormWindowState.Maximized;
                    saswxout.Show();

                    //dwp.StartPosition = FormStartPosition.CenterScreen;

                    break;
                case "外协领料新":
                    foreach (Form frm in this.splitContainer1.Panel2.Controls)
                    {
                        if (frm is LY_Store_Out_Outsource_new)
                        {
                            frm.WindowState = FormWindowState.Maximized;
                            frm.Activate();
                            return;
                        }
                    }

                    LY_Store_Out_Outsource_new saswxoutnew = new LY_Store_Out_Outsource_new();


                    saswxoutnew.MdiParent = this;

                    saswxoutnew.Parent = this.splitContainer1.Panel2;
                    saswxoutnew.WindowState = FormWindowState.Maximized;
                    saswxoutnew.Show();

                    //dwp.StartPosition = FormStartPosition.CenterScreen;

                    break;
                case "成品半品入库":
                    foreach (Form frm in this.splitContainer1.Panel2.Controls)
                    {
                        if (frm is LY_Store_ProductIn)
                        {
                            frm.WindowState = FormWindowState.Maximized;
                            frm.Activate();
                            return;
                        }
                    }

                    LY_Store_ProductIn spi  = new LY_Store_ProductIn();


                    spi.MdiParent = this;

                    spi.Parent = this.splitContainer1.Panel2;
                    spi.WindowState = FormWindowState.Maximized;
                    spi.Show();

                    //dwp.StartPosition = FormStartPosition.CenterScreen;

                    break;

                case "计划产品入库":
                    foreach (Form frm in this.splitContainer1.Panel2.Controls)
                    {
                        if (frm is LY_Store_EndProductIn)
                        {
                            frm.WindowState = FormWindowState.Maximized;
                            frm.Activate();
                            return;
                        }
                    }

                    LY_Store_EndProductIn espi = new LY_Store_EndProductIn();


                    espi.MdiParent = this;

                    espi.Parent = this.splitContainer1.Panel2;
                    espi.WindowState = FormWindowState.Maximized;
                    espi.Show();

                    //dwp.StartPosition = FormStartPosition.CenterScreen;
                    
                    break;
                case "库存查询":
                    foreach (Form frm in this.splitContainer1.Panel2.Controls)
                    {
                        if (frm is LY_Store_InOutQuery)
                        {
                            frm.WindowState = FormWindowState.Maximized;
                            frm.Activate();
                            return;
                        }
                    }

                    LY_Store_InOutQuery sioq = new LY_Store_InOutQuery();


                    sioq.MdiParent = this;

                    sioq.Parent = this.splitContainer1.Panel2;
                    sioq.WindowState = FormWindowState.Maximized;
                    sioq.Show();

                    //dwp.StartPosition = FormStartPosition.CenterScreen;

                    break;

                    ///////////////////////aaaaaaaaa

                case "库存时段查询":
                    foreach (Form frm in this.splitContainer1.Panel2.Controls)
                    {
                        if (frm is LY_Finance_Store_InOutQuery)
                        {
                            frm.WindowState = FormWindowState.Maximized;
                            frm.Activate();
                            return;
                        }
                    }

                    LY_Finance_Store_InOutQuery finansioq = new LY_Finance_Store_InOutQuery();


                    finansioq.MdiParent = this;

                    finansioq.Parent = this.splitContainer1.Panel2;
                    finansioq.WindowState = FormWindowState.Maximized;
                    finansioq.Show();

                    //dwp.StartPosition = FormStartPosition.CenterScreen;

                    break;
                case "营业合同明细":
                    foreach (Form frm in this.splitContainer1.Panel2.Controls)
                    {
                        if (frm is LY_Finance_salsecontract)
                        {
                            frm.WindowState = FormWindowState.Maximized;
                            frm.Activate();
                            return;
                        }
                    }

                    LY_Finance_salsecontract salesFinan = new LY_Finance_salsecontract();


                    salesFinan.MdiParent = this;

                    salesFinan.Parent = this.splitContainer1.Panel2;
                    salesFinan.WindowState = FormWindowState.Maximized;
                    salesFinan.Show();

                    //dwp.StartPosition = FormStartPosition.CenterScreen;

                    break;
                case "营业维修明细":
                    foreach (Form frm in this.splitContainer1.Panel2.Controls)
                    {
                        if (frm is LY_Finance_salserepairQuery)
                        {
                            frm.WindowState = FormWindowState.Maximized;
                            frm.Activate();
                            return;
                        }
                    }

                    LY_Finance_salserepairQuery repairFinan = new LY_Finance_salserepairQuery();


                    repairFinan.MdiParent = this;

                    repairFinan.Parent = this.splitContainer1.Panel2;
                    repairFinan.WindowState = FormWindowState.Maximized;
                    repairFinan.Show();

                    //dwp.StartPosition = FormStartPosition.CenterScreen;

                    break;
                    //////////////////bbbbbbbbbbbbbbbbbbbbb
                case "外协库存":
                    foreach (Form frm in this.splitContainer1.Panel2.Controls)
                    {
                        if (frm is LY_Store_ClassQuery)
                        {
                            frm.WindowState = FormWindowState.Maximized;
                            frm.Activate();
                            return;
                        }
                    }

                    LY_Store_ClassQuery wxsioq = new LY_Store_ClassQuery();

                    wxsioq.Nowsort = "外协";
                    wxsioq.Nowsortcode = "2";

                    wxsioq.MdiParent = this;

                    wxsioq.Parent = this.splitContainer1.Panel2;
                    wxsioq.WindowState = FormWindowState.Maximized;
                    wxsioq.Show();

                    //dwp.StartPosition = FormStartPosition.CenterScreen;

                    break;
                case "采购库存":
                    foreach (Form frm in this.splitContainer1.Panel2.Controls)
                    {
                        if (frm is LY_Store_ClassQuery)
                        {
                            frm.WindowState = FormWindowState.Maximized;
                            frm.Activate();
                            return;
                        }
                    }

                    LY_Store_ClassQuery cgsioq = new LY_Store_ClassQuery();

                    cgsioq.Nowsort = "采购";
                    cgsioq.Nowsortcode = "3";

                    cgsioq.MdiParent = this;

                    cgsioq.Parent = this.splitContainer1.Panel2;
                    cgsioq.WindowState = FormWindowState.Maximized;
                    cgsioq.Show();

                    //dwp.StartPosition = FormStartPosition.CenterScreen;

                    break;
                case "机加库存":
                    foreach (Form frm in this.splitContainer1.Panel2.Controls)
                    {
                        if (frm is LY_Store_ClassQuery)
                        {
                            frm.WindowState = FormWindowState.Maximized;
                            frm.Activate();
                            return;
                        }
                    }

                    LY_Store_ClassQuery jjsioq = new LY_Store_ClassQuery();

                    jjsioq.Nowsort = "机加";
                    jjsioq.Nowsortcode = "4";


                    jjsioq.MdiParent = this;

                    jjsioq.Parent = this.splitContainer1.Panel2;
                    jjsioq.WindowState = FormWindowState.Maximized;
                    jjsioq.Show();

                    //dwp.StartPosition = FormStartPosition.CenterScreen;

                    break;
                case "机加物料跟单":
                   

                    foreach (Form frm in this.splitContainer1.Panel2.Controls)
                    {
                        if (frm is LY_Materiel_Machine_Order)
                        {

                          
                            frm.WindowState = FormWindowState.Maximized;

                            frm.Activate();
                            return;
                        }
                    }
                    LY_Materiel_Machine_Order jjorder = new LY_Materiel_Machine_Order();
                    //lysupplier_wx.Sortcode = "4";
                    jjorder.Sortmode = "WT";

                    jjorder.MdiParent = this;

                    jjorder.Parent = this.splitContainer1.Panel2;
                    jjorder.WindowState = FormWindowState.Maximized;
                    jjorder.Show();

                    break;

                   ////////////////////////////////
                case "跟单查询":
                    foreach (Form frm in this.splitContainer1.Panel2.Controls)
                    {
                        if (frm is LY_Production_order_Mange_A)
                        {
                            frm.WindowState = FormWindowState.Maximized;
                            frm.Activate();
                            return;
                        }
                    }

                    LY_Production_order_Mange_A jjquery = new LY_Production_order_Mange_A();

                    //jjsioq.Nowsort = "机加";
                    //jjsioq.Nowsortcode = "4";


                    jjquery.MdiParent = this;

                    jjquery.Parent = this.splitContainer1.Panel2;
                    jjquery.WindowState = FormWindowState.Maximized;
                    jjquery.Show();

                    //dwp.StartPosition = FormStartPosition.CenterScreen;

                    break;
                case "入库综合查询":
                    foreach (Form frm in this.splitContainer1.Panel2.Controls)
                    {
                        if (frm is LY_StoreInAnalysis)
                        {
                            frm.WindowState = FormWindowState.Maximized;
                            frm.Activate();
                            return;
                        }
                    }

                    LY_StoreInAnalysis lsi = new LY_StoreInAnalysis();


                    lsi.MdiParent = this;

                    lsi.Parent = this.splitContainer1.Panel2;
                    lsi.WindowState = FormWindowState.Maximized;
                    lsi.Show();

                    //dwp.StartPosition = FormStartPosition.CenterScreen;

                    break;

                case "入库综合查询财务":
                    foreach (Form frm in this.splitContainer1.Panel2.Controls)
                    {
                        if (frm is LY_StoreInAnalysis_Fin)
                        {
                            frm.WindowState = FormWindowState.Maximized;
                            frm.Activate();
                            return;
                        }
                    }

                    LY_StoreInAnalysis_Fin lsifin = new LY_StoreInAnalysis_Fin();


                    lsifin.MdiParent = this;

                    lsifin.Parent = this.splitContainer1.Panel2;
                    lsifin.WindowState = FormWindowState.Maximized;
                    lsifin.Show();

                    //dwp.StartPosition = FormStartPosition.CenterScreen;

                    break;
                case "入库费用平摊":
                    foreach (Form frm in this.splitContainer1.Panel2.Controls)
                    {
                        if (frm is LY_StoreInAnalysis_FinNew)
                        {
                            frm.WindowState = FormWindowState.Maximized;
                            frm.Activate();
                            return;
                        }
                    } 
                    LY_StoreInAnalysis_FinNew rkpt = new LY_StoreInAnalysis_FinNew(); 
                    rkpt.MdiParent = this; 
                    rkpt.Parent = this.splitContainer1.Panel2;
                    rkpt.WindowState = FormWindowState.Maximized;
                    rkpt.Show();  
                    break;
                case "出库综合查询":
                    foreach (Form frm in this.splitContainer1.Panel2.Controls)
                    {
                        if (frm is LY_StoreOutAnalysis)
                        {
                            frm.WindowState = FormWindowState.Maximized;
                            frm.Activate();
                            return;
                        }
                    }

                    LY_StoreOutAnalysis lso = new LY_StoreOutAnalysis();


                    lso.MdiParent = this;

                    lso.Parent = this.splitContainer1.Panel2;
                    lso.WindowState = FormWindowState.Maximized;
                    lso.Show();

                    //dwp.StartPosition = FormStartPosition.CenterScreen;

                    break;
                case "出库综合查询财务": 
                    foreach (Form frm in this.splitContainer1.Panel2.Controls)
                    {
                        if (frm is LY_StoreOutAnalysis1)
                        {
                            frm.WindowState = FormWindowState.Maximized;
                            frm.Activate();
                            return;
                        }
                    }

                    LY_StoreOutAnalysis1 lsofin = new LY_StoreOutAnalysis1();


                    lsofin.MdiParent = this;

                    lsofin.Parent = this.splitContainer1.Panel2;
                    lsofin.WindowState = FormWindowState.Maximized;
                    lsofin.Show();

                    //dwp.StartPosition = FormStartPosition.CenterScreen;

                    break;
                case "出库签证":
                    foreach (Form frm in this.splitContainer1.Panel2.Controls)
                    {
                        if (frm is LY_StoreOut_Verify)
                        {
                            frm.WindowState = FormWindowState.Maximized;
                            frm.Activate();
                            return;
                        }
                    }

                    LY_StoreOut_Verify lsov = new LY_StoreOut_Verify();


                    lsov.MdiParent = this;

                    lsov.Parent = this.splitContainer1.Panel2;
                    lsov.WindowState = FormWindowState.Maximized;
                    lsov.Show();

                    //dwp.StartPosition = FormStartPosition.CenterScreen;

                    break;
                case "入库签证":
                    foreach (Form frm in this.splitContainer1.Panel2.Controls)
                    {
                        if (frm is LY_StoreIn_Verify)
                        {
                            frm.WindowState = FormWindowState.Maximized;
                            frm.Activate();
                            return;
                        }
                    }

                    LY_StoreIn_Verify lsiv = new LY_StoreIn_Verify();


                    lsiv.MdiParent = this;

                    lsiv.Parent = this.splitContainer1.Panel2;
                    lsiv.WindowState = FormWindowState.Maximized;
                    lsiv.Show();

                    //dwp.StartPosition = FormStartPosition.CenterScreen;

                    break;
                
                case "部门设置":
                    foreach (Form frm in this.splitContainer1.Panel2.Controls)
                    {
                        if (frm is Bumen)
                        {
                            frm.WindowState = FormWindowState.Maximized;
                            frm.Activate();
                            return;
                        }
                    }

                    Bumen bm = new Bumen();

                    //  bm.Parent_model = ((System.Windows.Forms.ToolStripItem)(sender)).OwnerItem.Text;
                    bm.MdiParent = this;
                    bm.Parent = this.splitContainer1.Panel2;
                    bm.WindowState = FormWindowState.Maximized;
                    bm.Show();
                    break;

             

                case "员工管理":
                    foreach (Form frm in this.splitContainer1.Panel2.Controls)
                    {
                        if (frm is Yonghu)
                        {
                            frm.WindowState = FormWindowState.Normal;
                            frm.Activate();
                            return;
                        }
                    }

                    Yonghu user = new Yonghu();

                    //  bm.Parent_model = ((System.Windows.Forms.ToolStripItem)(sender)).OwnerItem.Text;
                    user.MdiParent = this;
                    user.Parent = this.splitContainer1.Panel2;
                    user.WindowState = FormWindowState.Maximized;
                    user.Show();
                    break;

                case "员工密码修改":
                    foreach (Form frm in this.splitContainer1.Panel2.Controls)
                    {
                        if (frm is ChangePasswordForm)
                        {
                            frm.WindowState = FormWindowState.Normal;
                            frm.Activate();
                            return;
                        }
                    }

                    ChangePasswordForm password = new ChangePasswordForm();

                    //  bm.Parent_model = ((System.Windows.Forms.ToolStripItem)(sender)).OwnerItem.Text;
                    password.MdiParent = this;
                    password.Parent = this.splitContainer1.Panel2;
                    password.WindowState = FormWindowState.Normal;
                    password.StartPosition = FormStartPosition.Manual;
                    password.Location = new Point(100, 100);
                    password.Show();
                    break;

                case "员工权限设置":
                    foreach (Form frm in this.splitContainer1.Panel2.Controls)
                    {
                        if (frm is QuanxianFenpei)
                        {
                            frm.WindowState = FormWindowState.Maximized;
                            frm.Activate();
                            return;
                        }
                    }

                    QuanxianFenpei qx = new QuanxianFenpei();

                    qx.MainMenu = this.menuStrip1;
                    //qx.Parent_model = ((System.Windows.Forms.ToolStripItem)(sender)).OwnerItem.Text;
                    qx.MdiParent = this;
                    qx.WindowState = FormWindowState.Maximized;
                    //  bm.Parent_model = ((System.Windows.Forms.ToolStripItem)(sender)).OwnerItem.Text;

                    qx.Parent = this.splitContainer1.Panel2;

                    qx.Show();
                    break;

                case "数据备份":
                    foreach (Form frm in this.splitContainer1.Panel2.Controls)
                    {
                        if (frm is SQLBackup)
                        {
                            frm.WindowState = FormWindowState.Normal;
                            frm.Activate();
                            return;
                        }
                    }

                    SQLBackup backup = new SQLBackup();

                    backup.ServerName = Program.dataBase.ServerName;
                    backup.DataBaseName = Program.dataBase.DataBaseName;
                    backup.UserName = Program.dataBase.UserName;
                    backup.PassWord = Program.dataBase.PassWord;

                    //  bm.Parent_model = ((System.Windows.Forms.ToolStripItem)(sender)).OwnerItem.Text;
                    backup.MdiParent = this;
                    backup.Parent = this.splitContainer1.Panel2;
                    backup.StartPosition = FormStartPosition.Manual;
                    backup.Location = new Point(100, 100);
                    backup.WindowState = FormWindowState.Normal;
                    backup.Show();
                    break;

                case "机器授权":
                    foreach (Form frm in this.splitContainer1.Panel2.Controls)
                    {
                        if (frm is CPUAuthorization)
                        {
                            frm.WindowState = FormWindowState.Normal;
                            frm.Activate();
                            return;
                        }
                    }
                    CPUAuthorization fm1 = new CPUAuthorization();

                    //  bm.Parent_model = ((System.Windows.Forms.ToolStripItem)(sender)).OwnerItem.Text;
                    fm1.MdiParent = this;
                    fm1.Parent = this.splitContainer1.Panel2;
                    fm1.WindowState = FormWindowState.Normal;
                    fm1.StartPosition = FormStartPosition.Manual;
                    fm1.Location = new Point(100, 100);
                    fm1.Show();
                    break;

                case "系统参数":
                    foreach (Form frm in this.splitContainer1.Panel2.Controls)
                    {
                        if (frm is OptionsForm)
                        {
                            frm.WindowState = FormWindowState.Normal;
                            frm.Activate();
                            return;
                        }
                    }
                    OptionsForm syspar = new OptionsForm();

                    //  bm.Parent_model = ((System.Windows.Forms.ToolStripItem)(sender)).OwnerItem.Text;
                    syspar.MdiParent = this;
                    syspar.Parent = this.splitContainer1.Panel2;
                    syspar.WindowState = FormWindowState.Normal;
                    syspar.StartPosition = FormStartPosition.Manual;
                    syspar.Location = new Point(100, 100);
                    syspar.Show();
                    break;
                 /////////////////////////////////////////////////////////
                case "加工业务":
                    foreach (Form frm in this.splitContainer1.Panel2.Controls)
                    {
                        if (frm is LY_Machine)
                        {
                            frm.WindowState = FormWindowState.Maximized;
                            frm.Activate();
                            return;
                        }
                    }
                    LY_Machine lymachine = new LY_Machine();

                    lymachine.MdiParent = this;

                    lymachine.Parent = this.splitContainer1.Panel2;
                    lymachine.WindowState = FormWindowState.Maximized;
                    lymachine.Show();

                    break;
                /////////////////////////////////////////////////////////
               
                ///////////////////////////////////加工基地外协合同
                case "加工基地外协合同":
                    foreach (Form frm in this.splitContainer1.Panel2.Controls)
                    {
                        if (frm is LY_Machine_Outsource)
                        {
                            frm.WindowState = FormWindowState.Maximized;
                            frm.Activate();
                            return;
                        }
                    }
                    LY_Machine_Outsource jgjdwxht = new LY_Machine_Outsource();

                    jgjdwxht.MdiParent = this;

                    jgjdwxht.Parent = this.splitContainer1.Panel2;
                    jgjdwxht.WindowState = FormWindowState.Maximized;
                    jgjdwxht.Show();

                    break;
                    //////////////////////////////
                case "机加委托审核":
                    foreach (Form frm in this.splitContainer1.Panel2.Controls)
                    {
                        if (frm is LY_Machine_Outsource_approve)
                        {
                            frm.WindowState = FormWindowState.Maximized;
                            frm.Activate();
                            return;
                        }
                    }
                    LY_Machine_Outsource_approve jgjdwxht0 = new LY_Machine_Outsource_approve();

                    jgjdwxht0.MdiParent = this;

                    jgjdwxht0.Parent = this.splitContainer1.Panel2;
                    jgjdwxht0.WindowState = FormWindowState.Maximized;
                    jgjdwxht0.Show();

                    break;
                //////////////////////////////
                case "机加委托批准":
                    foreach (Form frm in this.splitContainer1.Panel2.Controls)
                    {
                        if (frm is LY_Machine_Outsource_approve1)
                        {
                            frm.WindowState = FormWindowState.Maximized;
                            frm.Activate();
                            return;
                        }
                    }
                    LY_Machine_Outsource_approve1 jgjdwxht1 = new LY_Machine_Outsource_approve1();

                    jgjdwxht1.MdiParent = this;

                    jgjdwxht1.Parent = this.splitContainer1.Panel2;
                    jgjdwxht1.WindowState = FormWindowState.Maximized;
                    jgjdwxht1.Show();

                    break;
                
                //////////////////////////////
                case "机加委托审定":
                    foreach (Form frm in this.splitContainer1.Panel2.Controls)
                    {
                        if (frm is LY_Machine_Outsource_approve2)
                        {
                            frm.WindowState = FormWindowState.Maximized;
                            frm.Activate();
                            return;
                        }
                    }
                    LY_Machine_Outsource_approve2 jgjdwxht2 = new LY_Machine_Outsource_approve2();

                    jgjdwxht2.MdiParent = this;

                    jgjdwxht2.Parent = this.splitContainer1.Panel2;
                    jgjdwxht2.WindowState = FormWindowState.Maximized;
                    jgjdwxht2.Show();

                    break;

                //////////////////////////////
                case "机加委托开票":
                    foreach (Form frm in this.splitContainer1.Panel2.Controls)
                    {
                        if (frm is LY_Machine_Outsource_approve3)
                        {
                            frm.WindowState = FormWindowState.Maximized;
                            frm.Activate();
                            return;
                        }
                    }
                    LY_Machine_Outsource_approve3 jgjdwxht3 = new LY_Machine_Outsource_approve3();

                    jgjdwxht3.MdiParent = this;

                    jgjdwxht3.Parent = this.splitContainer1.Panel2;
                    jgjdwxht3.WindowState = FormWindowState.Maximized;
                    jgjdwxht3.Show();

                    break;

                //////////////////////////////


                case "外协后续加工":
                    foreach (Form frm in this.splitContainer1.Panel2.Controls)
                    {
                        if (frm is LY_Machine_Foroutsource)
                        {
                            frm.WindowState = FormWindowState.Maximized;
                            frm.Activate();
                            return;
                            ////aaa
                        }
                    }
                    LY_Machine_Foroutsource lymachineout = new LY_Machine_Foroutsource();

                    lymachineout.MdiParent = this;

                    lymachineout.Parent = this.splitContainer1.Panel2;
                    lymachineout.WindowState = FormWindowState.Maximized;
                    lymachineout.Show();

                    break;
                case "返修业务":
                    foreach (Form frm in this.splitContainer1.Panel2.Controls)
                    {
                        if (frm is LY_Machine_Remake)
                        {
                            frm.WindowState = FormWindowState.Maximized;
                            frm.Activate();
                            return;
                        }
                    }
                    LY_Machine_Remake lymachineremake = new LY_Machine_Remake();

                    lymachineremake.MdiParent = this;

                    lymachineremake.Parent = this.splitContainer1.Panel2;
                    lymachineremake.WindowState = FormWindowState.Maximized;
                    lymachineremake.Show();
                    
                    break;
                    

                    case "非生产采购":
                    foreach (Form frm in this.splitContainer1.Panel2.Controls)
                    {
                        if (frm is LY_Purchase_NP)
                        {
                            frm.WindowState = FormWindowState.Maximized;
                            frm.Activate();
                            return;
                        }
                    }
                    LY_Purchase_NP nppurchase = new LY_Purchase_NP();

                    nppurchase.MdiParent = this;

                    nppurchase.Parent = this.splitContainer1.Panel2;
                    nppurchase.WindowState = FormWindowState.Maximized;
                    nppurchase.Show();

                    break;

                case "采购业务":
                    foreach (Form frm in this.splitContainer1.Panel2.Controls)
                    {
                        if (frm is LY_Purchase)
                        {
                            frm.WindowState = FormWindowState.Maximized;
                            frm.Activate();
                            return;
                        }
                    }
                    LY_Purchase lypurchase = new LY_Purchase();

                    lypurchase.MdiParent = this;

                    lypurchase.Parent = this.splitContainer1.Panel2;
                    lypurchase.WindowState = FormWindowState.Maximized;
                    lypurchase.Show();

                    break;

                case "采购合同审批":
                    foreach (Form frm in this.splitContainer1.Panel2.Controls)
                    {
                        if (frm is LY_Purchase_Approve)
                        {
                            frm.WindowState = FormWindowState.Maximized;
                            frm.Activate();
                            return;
                        }
                    }
                    LY_Purchase_Approve lypurchaseapprove = new LY_Purchase_Approve();

                    lypurchaseapprove.MdiParent = this;

                    lypurchaseapprove.Parent = this.splitContainer1.Panel2;
                    lypurchaseapprove.WindowState = FormWindowState.Maximized;
                    lypurchaseapprove.Show();

                    break;

                case "采购合同评审":
                    foreach (Form frm in this.splitContainer1.Panel2.Controls)
                    {
                        if (frm is LY_Purchase_ApproveNewQC)
                        {
                            frm.WindowState = FormWindowState.Maximized;
                            frm.Activate();
                            return;
                        }
                    }
                    LY_Purchase_ApproveNewQC lypurchaseapproveQC = new LY_Purchase_ApproveNewQC();

                    lypurchaseapproveQC.MdiParent = this;

                    lypurchaseapproveQC.Parent = this.splitContainer1.Panel2;
                    lypurchaseapproveQC.WindowState = FormWindowState.Maximized;
                    lypurchaseapproveQC.Show();

                    break;




                case "采购合同查询":
                    foreach (Form frm in this.splitContainer1.Panel2.Controls)
                    {
                        if (frm is LY_Purchase_Approve_cw)
                        {
                            frm.WindowState = FormWindowState.Maximized;
                            frm.Activate();
                            return;
                        }
                    }
                    LY_Purchase_Approve_cw cgcx = new LY_Purchase_Approve_cw();

                    cgcx.MdiParent = this;

                    cgcx.Parent = this.splitContainer1.Panel2;
                    cgcx.WindowState = FormWindowState.Maximized;
                    cgcx.Show();

                    break;
                case "合同付款管理":
                    foreach (Form frm in this.splitContainer1.Panel2.Controls)
                    {
                        if (frm is LY_invoice_manage)
                        {
                            frm.WindowState = FormWindowState.Maximized;
                            frm.Activate();
                            return;
                        }
                    }
                    LY_invoice_manage fpmsg = new LY_invoice_manage();

                    fpmsg.MdiParent = this;

                    fpmsg.Parent = this.splitContainer1.Panel2;
                    fpmsg.WindowState = FormWindowState.Maximized;
                    fpmsg.Show();

                    break;
                    

                case "采购合同审批(新)":
                    foreach (Form frm in this.splitContainer1.Panel2.Controls)
                    {
                        if (frm is LY_Purchase_ApproveNew)
                        {
                            frm.WindowState = FormWindowState.Maximized;
                            frm.Activate();
                            return;
                        }
                    }
                    LY_Purchase_ApproveNew lypurchaseapproveN = new LY_Purchase_ApproveNew();

                    lypurchaseapproveN.MdiParent = this;

                    lypurchaseapproveN.Parent = this.splitContainer1.Panel2;
                    lypurchaseapproveN.WindowState = FormWindowState.Maximized;
                    lypurchaseapproveN.Show();

                    break;

                case "采购合同审定":
                    foreach (Form frm in this.splitContainer1.Panel2.Controls)
                    {
                        if (frm is LY_Purchase_ApproveFinal)
                        {
                            frm.WindowState = FormWindowState.Maximized;
                            frm.Activate();
                            return;
                        }
                    }
                    LY_Purchase_ApproveFinal lypurchaseapprovef = new LY_Purchase_ApproveFinal();

                    lypurchaseapprovef.MdiParent = this;

                    lypurchaseapprovef.Parent = this.splitContainer1.Panel2;
                    lypurchaseapprovef.WindowState = FormWindowState.Maximized;
                    lypurchaseapprovef.Show();

                    break;
                //
                /////////////////////////////////////////////
                case "采购检验":
                    foreach (Form frm in this.splitContainer1.Panel2.Controls)
                    {
                        if (frm is LY_Quality_Control_Purchase)
                        {



                            frm.WindowState = FormWindowState.Maximized;

                            frm.Activate();
                            return;
                        }
                    }
                    LY_Quality_Control_Purchase caigoujy = new LY_Quality_Control_Purchase();

                    caigoujy.MdiParent = this;

                    caigoujy.Parent = this.splitContainer1.Panel2;
                    caigoujy.WindowState = FormWindowState.Maximized;
                    caigoujy.Show();

                    break;
                /////////////////////////////////////////////
                case "采购质检报告":
                    foreach (Form frm in this.splitContainer1.Panel2.Controls)
                    {
                        if (frm is LY_Quality_Control_PurchaseRep)
                        {



                            frm.WindowState = FormWindowState.Maximized;

                            frm.Activate();
                            return;
                        }
                    }
                    LY_Quality_Control_PurchaseRep caigoujyrep = new LY_Quality_Control_PurchaseRep();

                    caigoujyrep.MdiParent = this;

                    caigoujyrep.Parent = this.splitContainer1.Panel2;
                    caigoujyrep.WindowState = FormWindowState.Maximized;
                    caigoujyrep.Show();

                    break;

                case "暂估入库":
                    foreach (Form frm in this.splitContainer1.Panel2.Controls)
                    {
                        if (frm is LY_Zg)
                        {



                            frm.WindowState = FormWindowState.Maximized;

                            frm.Activate();
                            return;
                        }
                    }
                    LY_Zg zgrk = new LY_Zg();

                    zgrk.MdiParent = this;
                    
                    zgrk.Parent = this.splitContainer1.Panel2;
                    zgrk.WindowState = FormWindowState.Maximized;
                    zgrk.Show();

                    break;


                case "制造编码查询":
                    foreach (Form frm in this.splitContainer1.Panel2.Controls)
                    {
                        if (frm is machine_query)
                        {
                            frm.WindowState = FormWindowState.Maximized;

                            frm.Activate();
                            return;
                        }
                    }
                    machine_query fmset22 = new machine_query();

                    fmset22.MdiParent = this;

                    fmset22.Parent = this.splitContainer1.Panel2;
                    fmset22.WindowState = FormWindowState.Maximized;
                    fmset22.Show();

                    break;
                    

                /////////////////////////////////////////////asd
                case "月结业务":
                    foreach (Form frm in this.splitContainer1.Panel2.Controls)
                    {
                        if (frm is LY_Financial_monthly_settlement)
                        {

                            
                          
                            frm.WindowState = FormWindowState.Maximized;

                            frm.Activate();
                            return;
                        }
                    }
                    LY_Financial_monthly_settlement fmset = new LY_Financial_monthly_settlement();

                    fmset.MdiParent = this;

                    fmset.Parent = this.splitContainer1.Panel2;
                    fmset.WindowState = FormWindowState.Maximized;
                    fmset.Show();

                    break;
               
                /////////////////////////////////////////////
                case "采购入库":
                    foreach (Form frm in this.splitContainer1.Panel2.Controls)
                    {
                        if (frm is LY_Store_In_Purchase)
                        {
                            frm.WindowState = FormWindowState.Maximized;
                            frm.Activate();
                            return;
                        }
                    }

                    LY_Store_In_Purchase caigouruku = new LY_Store_In_Purchase();


                    caigouruku.MdiParent = this;

                    caigouruku.Parent = this.splitContainer1.Panel2;
                    caigouruku.WindowState = FormWindowState.Maximized;
                    caigouruku.Show();

                    //dwp.StartPosition = FormStartPosition.CenterScreen;

                    break;
                /////////////////////////////////////////////
                case "采购质检审查":
                    foreach (Form frm in this.splitContainer1.Panel2.Controls)
                    {
                        if (frm is LY_Quality_Control_Purchase_Tec)
                        {



                            frm.WindowState = FormWindowState.Maximized;

                            frm.Activate();
                            return;
                        }
                    }
                    LY_Quality_Control_Purchase_Tec caigoujyt = new LY_Quality_Control_Purchase_Tec();

                    caigoujyt.MdiParent = this;

                    caigoujyt.Parent = this.splitContainer1.Panel2;
                    caigoujyt.WindowState = FormWindowState.Maximized;
                    caigoujyt.Show();

                    break;
                /////////////////////////////////////////////外协统计财务

                case "外协统计财务":
                    foreach (Form frm in this.splitContainer1.Panel2.Controls)
                    {
                        if (frm is LY_Outsource_Sum_Fin)
                        {
                            frm.WindowState = FormWindowState.Maximized;
                            frm.Activate();
                            return;
                        }
                    }
                    LY_Outsource_Sum_Fin lymoutsourcesumfin = new LY_Outsource_Sum_Fin();

                    lymoutsourcesumfin.MdiParent = this;

                    lymoutsourcesumfin.Parent = this.splitContainer1.Panel2;
                    lymoutsourcesumfin.WindowState = FormWindowState.Maximized;
                    lymoutsourcesumfin.Show();

                    break;
                /////////////////////////////////////////////
                case "发票管理":
                    foreach (Form frm in this.splitContainer1.Panel2.Controls)
                    {
                        if (frm is ly_invoiceMg)
                        {
                            frm.WindowState = FormWindowState.Maximized;
                            frm.Activate();
                            return;
                        }
                    }
                    ly_invoiceMg mgin = new ly_invoiceMg();

                    mgin.MdiParent = this;

                    mgin.Parent = this.splitContainer1.Panel2;
                    mgin.WindowState = FormWindowState.Maximized;
                    mgin.Show();

                    break;
                    
                case "外协业务":
                    foreach (Form frm in this.splitContainer1.Panel2.Controls)
                    {
                        if (frm is LY_Outsource)
                        {
                            frm.WindowState = FormWindowState.Maximized;
                            frm.Activate();
                            return;
                        }
                    }
                    LY_Outsource lymoutsource = new LY_Outsource();

                    lymoutsource.MdiParent = this;

                    lymoutsource.Parent = this.splitContainer1.Panel2;
                    lymoutsource.WindowState = FormWindowState.Maximized;
                    lymoutsource.Show();

                    break;
                /////////////////////////////////////////////

                case "外协合同评审":
                    foreach (Form frm in this.splitContainer1.Panel2.Controls)
                    {
                        if (frm is LY_Outsource_ApproveNewQC)
                        {
                            frm.WindowState = FormWindowState.Maximized;
                            frm.Activate();
                            return;
                        }
                    }
                    LY_Outsource_ApproveNewQC lypurchaseapproveQCnew = new LY_Outsource_ApproveNewQC();

                    lypurchaseapproveQCnew.MdiParent = this;

                    lypurchaseapproveQCnew.Parent = this.splitContainer1.Panel2;
                    lypurchaseapproveQCnew.WindowState = FormWindowState.Maximized;
                    lypurchaseapproveQCnew.Show();

                    break;




                case "外协合同查询":
                    foreach (Form frm in this.splitContainer1.Panel2.Controls)
                    {
                        if (frm is LY_Outsource_Approve_cw)
                        {
                            frm.WindowState = FormWindowState.Maximized;
                            frm.Activate();
                            return;
                        }
                    }
                    LY_Outsource_Approve_cw cww = new LY_Outsource_Approve_cw();

                    cww.MdiParent = this;

                    cww.Parent = this.splitContainer1.Panel2;
                    cww.WindowState = FormWindowState.Maximized;
                    cww.Show();

                    break;



                case "外协合同审批":
                    foreach (Form frm in this.splitContainer1.Panel2.Controls)
                    {
                        if (frm is LY_Outsource_Approve)
                        {
                            frm.WindowState = FormWindowState.Maximized;
                            frm.Activate();
                            return;
                        }
                    }
                    LY_Outsource_Approve lymoutsourceAp = new LY_Outsource_Approve();

                    lymoutsourceAp.MdiParent = this;

                    lymoutsourceAp.Parent = this.splitContainer1.Panel2;
                    lymoutsourceAp.WindowState = FormWindowState.Maximized;
                    lymoutsourceAp.Show();

                    break;
                /////////////////////////////////////////////
                case "外协合同审批(新)":
                    foreach (Form frm in this.splitContainer1.Panel2.Controls)
                    {
                        if (frm is LY_Outsource_ApproveNew)
                        {
                            frm.WindowState = FormWindowState.Maximized;
                            frm.Activate();
                            return;
                        }
                    }
                    LY_Outsource_ApproveNew lymoutsourceApnew = new LY_Outsource_ApproveNew();

                    lymoutsourceApnew.MdiParent = this;

                    lymoutsourceApnew.Parent = this.splitContainer1.Panel2;
                    lymoutsourceApnew.WindowState = FormWindowState.Maximized;
                    lymoutsourceApnew.Show();

                    break;
                /////////////////////////////////////////////

                case "外协合同审定":
                    foreach (Form frm in this.splitContainer1.Panel2.Controls)
                    {
                        if (frm is LY_Outsource_ApproveFinal)
                        {
                            frm.WindowState = FormWindowState.Maximized;
                            frm.Activate();
                            return;
                        }
                    }
                    LY_Outsource_ApproveFinal lymoutsourceApf = new LY_Outsource_ApproveFinal();

                    lymoutsourceApf.MdiParent = this;

                    lymoutsourceApf.Parent = this.splitContainer1.Panel2;
                    lymoutsourceApf.WindowState = FormWindowState.Maximized;
                    lymoutsourceApf.Show();

                    break;
                /////////////////////////////////////////////
                case "外协检验":
                    foreach (Form frm in this.splitContainer1.Panel2.Controls)
                    {
                        if (frm is LY_Quality_Control_Outsource)
                        {



                            frm.WindowState = FormWindowState.Maximized;

                            frm.Activate();
                            return;
                        }
                    }
                    LY_Quality_Control_Outsource waixiejy = new LY_Quality_Control_Outsource();

                    waixiejy.MdiParent = this;

                    waixiejy.Parent = this.splitContainer1.Panel2;
                    waixiejy.WindowState = FormWindowState.Maximized;
                    waixiejy.Show();

                    break;
                /////////////////////////////////////////////
                case "外协质检报告":
                    foreach (Form frm in this.splitContainer1.Panel2.Controls)
                    {
                        if (frm is LY_Quality_Control_OutsourceRep)
                        {



                            frm.WindowState = FormWindowState.Maximized;

                            frm.Activate();
                            return;
                        }
                    }
                    LY_Quality_Control_OutsourceRep waixiejyrep = new LY_Quality_Control_OutsourceRep();

                    waixiejyrep.MdiParent = this;

                    waixiejyrep.Parent = this.splitContainer1.Panel2;
                    waixiejyrep.WindowState = FormWindowState.Maximized;
                    waixiejyrep.Show();

                    break;
                /////////////////////////////////////////////
                case "钳装检验":
                    foreach (Form frm in this.splitContainer1.Panel2.Controls)
                    {
                        if (frm is LY_QualityInspection_Benchwork)
                        {



                            frm.WindowState = FormWindowState.Maximized;

                            frm.Activate();
                            return;
                        }
                    }
                    LY_QualityInspection_Benchwork benchwork = new LY_QualityInspection_Benchwork();

                    benchwork.MdiParent = this;

                    benchwork.Parent = this.splitContainer1.Panel2;
                    benchwork.WindowState = FormWindowState.Maximized;
                    benchwork.Show();

                    break;




                case "钳装质检报告":
                    foreach (Form frm in this.splitContainer1.Panel2.Controls)
                    {
                        if (frm is LY_Quality_qzRep)
                        {



                            frm.WindowState = FormWindowState.Maximized;

                            frm.Activate();
                            return;
                        }
                    }
                    LY_Quality_qzRep qzjy = new LY_Quality_qzRep();

                    qzjy.MdiParent = this;

                    qzjy.Parent = this.splitContainer1.Panel2;
                    qzjy.WindowState = FormWindowState.Maximized;
                    qzjy.Show();

                    break;

                case "客户到款及往来账信息汇总":
                    foreach (Form frm in this.splitContainer1.Panel2.Controls)
                    {
                        if (frm is LY_ReceivableRep)
                        {



                            frm.WindowState = FormWindowState.Maximized;

                            frm.Activate();
                            return;
                        }
                    }
                    LY_ReceivableRep dkhz = new LY_ReceivableRep();

                    dkhz.MdiParent = this;

                    dkhz.Parent = this.splitContainer1.Panel2;
                    dkhz.WindowState = FormWindowState.Maximized;
                    dkhz.Show();

                    break;
                    
                case "维修收件":
                    foreach (Form frm in this.splitContainer1.Panel2.Controls)
                    {
                        if (frm is LY_Receive_repairRep)
                        {



                            frm.WindowState = FormWindowState.Maximized;

                            frm.Activate();
                            return;
                        }
                    }
                    LY_Receive_repairRep repairRep = new LY_Receive_repairRep();

                    repairRep.MdiParent = this;

                    repairRep.Parent = this.splitContainer1.Panel2;
                    repairRep.WindowState = FormWindowState.Maximized;
                    repairRep.Show();

                    break;


                    
                case "电装质检报告":
                    foreach (Form frm in this.splitContainer1.Panel2.Controls)
                    {
                        if (frm is LY_Quality_dzRep)
                        {



                            frm.WindowState = FormWindowState.Maximized;

                            frm.Activate();
                            return;
                        }
                    }
                    LY_Quality_dzRep dzjy = new LY_Quality_dzRep();

                    dzjy.MdiParent = this;

                    dzjy.Parent = this.splitContainer1.Panel2;
                    dzjy.WindowState = FormWindowState.Maximized;
                    dzjy.Show();

                    break;




                case "钳装电装统计报告":
                    foreach (Form frm in this.splitContainer1.Panel2.Controls)
                    {
                        if (frm is LY_Quality_qzdz_Rep)
                        {



                            frm.WindowState = FormWindowState.Maximized;

                            frm.Activate();
                            return;
                        }
                    }
                    LY_Quality_qzdz_Rep qzdzjy = new LY_Quality_qzdz_Rep();

                    qzdzjy.MdiParent = this;

                    qzdzjy.Parent = this.splitContainer1.Panel2;
                    qzdzjy.WindowState = FormWindowState.Maximized;
                    qzdzjy.Show();

                    break;









                case "钳装改制检验":
                    foreach (Form frm in this.splitContainer1.Panel2.Controls)
                    {
                        if (frm is LY_Restructuring_QualityInspection_Benchwork)
                        { 

                            frm.WindowState = FormWindowState.Maximized; 
                            frm.Activate();
                            return;
                        }
                    }
                    LY_Restructuring_QualityInspection_Benchwork benchwork_gz = new LY_Restructuring_QualityInspection_Benchwork();

                    benchwork_gz.MdiParent = this;

                    benchwork_gz.Parent = this.splitContainer1.Panel2;
                    benchwork_gz.WindowState = FormWindowState.Maximized;
                    benchwork_gz.Show();

                    break;
     

                case "电装检验":
                    foreach (Form frm in this.splitContainer1.Panel2.Controls)
                    {
                        if (frm is LY_QualityInspection_Elecwork)
                        {



                            frm.WindowState = FormWindowState.Maximized;

                            frm.Activate();
                            return;
                        }
                    }
                    LY_QualityInspection_Elecwork elecwork = new LY_QualityInspection_Elecwork();

                    elecwork.MdiParent = this;

                    elecwork.Parent = this.splitContainer1.Panel2;
                    elecwork.WindowState = FormWindowState.Maximized;
                    elecwork.Show();

                    break;
    
                case "电装改制检验":
                    foreach (Form frm in this.splitContainer1.Panel2.Controls)
                    {
                        if (frm is LY_Restructuring_QualityInspection_Elecwork)
                        {



                            frm.WindowState = FormWindowState.Maximized;

                            frm.Activate();
                            return;
                        }
                    }
                    LY_Restructuring_QualityInspection_Elecwork elecwork_gz = new LY_Restructuring_QualityInspection_Elecwork();

                    elecwork_gz.MdiParent = this;

                    elecwork_gz.Parent = this.splitContainer1.Panel2;
                    elecwork_gz.WindowState = FormWindowState.Maximized;
                    elecwork_gz.Show();

                    break;
       
                case "外协质检审查":
                    foreach (Form frm in this.splitContainer1.Panel2.Controls)
                    {
                        if (frm is LY_Quality_Control_Outsource_Tec)
                        {



                            frm.WindowState = FormWindowState.Maximized;

                            frm.Activate();
                            return;
                        }
                    }
                    LY_Quality_Control_Outsource_Tec waixiejyt = new LY_Quality_Control_Outsource_Tec();

                    waixiejyt.MdiParent = this;

                    waixiejyt.Parent = this.splitContainer1.Panel2;
                    waixiejyt.WindowState = FormWindowState.Maximized;
                    waixiejyt.Show();

                    break;
 
                case "外协入库":
                    foreach (Form frm in this.splitContainer1.Panel2.Controls)
                    {
                        if (frm is LY_Store_In_Outsource)
                        {
                            frm.WindowState = FormWindowState.Maximized;
                            frm.Activate();
                            return;
                        }
                    }
                  ////////

                    LY_Store_In_Outsource waixieruku = new LY_Store_In_Outsource();


                    waixieruku.MdiParent = this;

                    waixieruku.Parent = this.splitContainer1.Panel2;
                    waixieruku.WindowState = FormWindowState.Maximized;
                    waixieruku.Show();

                    //dwp.StartPosition = FormStartPosition.CenterScreen;

                    break;
 
                case "供应商":
                    foreach (Form frm in this.splitContainer1.Panel2.Controls)
                    {
                        if (frm is LY_SupplierMange)
                        {
                            LY_SupplierMange lsm = frm as LY_SupplierMange;
                            lsm.Sortmode = "CG";
                            lsm.LoadData();
                            frm.WindowState = FormWindowState.Maximized;
                            frm.Activate();
                            return;
                        }
                    }
                    LY_SupplierMange lysupplier= new LY_SupplierMange();
                    //lysupplier.Sortcode = "3";
                    lysupplier.Sortmode = "CG";

                    lysupplier.MdiParent = this;

                    lysupplier.Parent = this.splitContainer1.Panel2;
                    lysupplier.WindowState = FormWindowState.Maximized;
                    lysupplier.Show();

                    break;

                case "加工商":
                    foreach (Form frm in this.splitContainer1.Panel2.Controls)
                    {
                        if (frm is LY_SupplierMange)
                        {

                            LY_SupplierMange lsm = frm as LY_SupplierMange;
                            lsm.Sortmode = "WX";
                            lsm.LoadData();
                            frm.WindowState = FormWindowState.Maximized;
                            frm.Activate();

                            return;
                        }
                    }
                    LY_SupplierMange lysupplier_wx = new LY_SupplierMange();
                    //lysupplier_wx.Sortcode = "2";
                    lysupplier_wx.Sortmode = "WX";

                    lysupplier_wx.MdiParent = this;

                    lysupplier_wx.Parent = this.splitContainer1.Panel2;
                    lysupplier_wx.WindowState = FormWindowState.Maximized;
                    lysupplier_wx.Show();

                    break;
                case "委托商":
                    foreach (Form frm in this.splitContainer1.Panel2.Controls)
                    {
                        if (frm is LY_SupplierMange)
                        {

                            LY_SupplierMange lsm = frm as LY_SupplierMange;
                            lsm.Sortmode = "WT";
                            lsm.LoadData();
                            frm.WindowState = FormWindowState.Maximized;
                            
                            frm.Activate();
                            return;
                        }
                    }
                    LY_SupplierMange lysupplier_wt = new LY_SupplierMange();
                    //lysupplier_wx.Sortcode = "4";
                    lysupplier_wt.Sortmode = "WT";

                    lysupplier_wt.MdiParent = this;

                    lysupplier_wt.Parent = this.splitContainer1.Panel2;
                    lysupplier_wt.WindowState = FormWindowState.Maximized;
                    lysupplier_wt.Show();

                    break;

                case "采购物料供应商":
                    foreach (Form frm in this.splitContainer1.Panel2.Controls)
                    {
                        if (frm is LY_Materiel_Supplier_Set)
                        {

                            LY_Materiel_Supplier_Set lsm = frm as LY_Materiel_Supplier_Set;
                            lsm.Sortmode = "CG";
                            lsm.LoadData();
                            frm.WindowState = FormWindowState.Maximized;

                            frm.Activate();
                            return;
                        }
                    }
                    LY_Materiel_Supplier_Set lymasup_cg = new LY_Materiel_Supplier_Set();
                    //lysupplier_wx.Sortcode = "4";
                    lymasup_cg.Sortmode = "CG";

                    lymasup_cg.MdiParent = this;

                    lymasup_cg.Parent = this.splitContainer1.Panel2;
                    lymasup_cg.WindowState = FormWindowState.Maximized;
                    lymasup_cg.Show();

                    break;

                case "协同采购设置":
                    foreach (Form frm in this.splitContainer1.Panel2.Controls)
                    {
                        if (frm is LY_Purchase_Part_Set)
                        {

                            LY_Purchase_Part_Set lpps = frm as LY_Purchase_Part_Set;
                            lpps.Sortmode = "CG";
                            lpps.LoadData();
                            frm.WindowState = FormWindowState.Maximized;

                            frm.Activate();
                            return;
                        }
                    }
                    LY_Purchase_Part_Set lpps_cg = new LY_Purchase_Part_Set();
                    //lysupplier_wx.Sortcode = "4";
                    lpps_cg.Sortmode = "CG";

                    lpps_cg.MdiParent = this;

                    lpps_cg.Parent = this.splitContainer1.Panel2;
                    lpps_cg.WindowState = FormWindowState.Maximized;
                    lpps_cg.Show();

                    break;
                case "协同外协设置":
                    foreach (Form frm in this.splitContainer1.Panel2.Controls)
                    {
                        if (frm is LY_Purchase_Part_Set)
                        {

                            LY_Purchase_Part_Set lppswx = frm as LY_Purchase_Part_Set;
                            lppswx.Sortmode = "WX";
                            lppswx.LoadData();
                            frm.WindowState = FormWindowState.Maximized;

                            frm.Activate();
                            return;
                        }
                    }
                    LY_Purchase_Part_Set lpps_wx = new LY_Purchase_Part_Set();
                    //lysupplier_wx.Sortcode = "4";
                    lpps_wx.Sortmode = "WX";

                    lpps_wx.MdiParent = this;

                    lpps_wx.Parent = this.splitContainer1.Panel2;
                    lpps_wx.WindowState = FormWindowState.Maximized;
                    lpps_wx.Show();

                    break;
                case "外协物料加工商":
                    foreach (Form frm in this.splitContainer1.Panel2.Controls)
                    {
                        if (frm is LY_Materiel_Supplier_Set)
                        {

                            LY_Materiel_Supplier_Set lsm = frm as LY_Materiel_Supplier_Set;
                            lsm.Sortmode = "WX";
                            lsm.LoadData();
                            frm.WindowState = FormWindowState.Maximized;

                            frm.Activate();
                            return;
                        }
                    }
                    LY_Materiel_Supplier_Set lymasup_wx = new LY_Materiel_Supplier_Set();
                    //lysupplier_wx.Sortcode = "4";
                    lymasup_wx.Sortmode = "WX";

                    lymasup_wx.MdiParent = this;

                    lymasup_wx.Parent = this.splitContainer1.Panel2;
                    lymasup_wx.WindowState = FormWindowState.Maximized;
                    lymasup_wx.Show();

                    break;
                case "机加工艺委托商":
                    foreach (Form frm in this.splitContainer1.Panel2.Controls)
                    {
                        if (frm is LY_Materiel_Supplier_Set)
                        {

                            LY_Materiel_Supplier_Set lsm = frm as LY_Materiel_Supplier_Set;
                            lsm.Sortmode = "WT";
                            lsm.LoadData();
                            frm.WindowState = FormWindowState.Maximized;

                            frm.Activate();
                            return;
                        }
                    }
                    LY_Materiel_Supplier_Set lymasup_wt = new LY_Materiel_Supplier_Set();
                    //lysupplier_wx.Sortcode = "4";
                    lymasup_wt.Sortmode = "WT";

                    lymasup_wt.MdiParent = this;

                    lymasup_wt.Parent = this.splitContainer1.Panel2;
                    lymasup_wt.WindowState = FormWindowState.Maximized;
                    lymasup_wt.Show();

                    break;

                case "机加工艺":
                    foreach (Form frm in this.splitContainer1.Panel2.Controls)
                    {
                        if (frm is LY_Manufacturing_procedure_Manage)
                        {

                            //LY_Manufacturing_procedure_Manage lmpm= frm as LY_Manufacturing_procedure_Manage;
                          
                            frm.WindowState = FormWindowState.Maximized;

                            frm.Activate();
                            return;
                        }
                    }
                    LY_Manufacturing_procedure_Manage lmpm = new LY_Manufacturing_procedure_Manage();
                    //lysupplier_wx.Sortcode = "4";
                    lmpm.MdiParent = this;
                    lmpm.Parent = this.splitContainer1.Panel2;
                    lmpm.WindowState = FormWindowState.Normal;
                    lmpm.StartPosition = FormStartPosition.Manual;
                    lmpm.Location = new Point(100, 100);
                    lmpm.Show();

                    break;

                case "机加工件工序设置":
                    foreach (Form frm in this.splitContainer1.Panel2.Controls)
                    {
                        if (frm is LY_Machine_Process)
                        {
                            frm.WindowState = FormWindowState.Maximized;
                            frm.Activate();
                            return;
                        }
                    }
                    LY_Machine_Process lymp = new LY_Machine_Process();

                    lymp.MdiParent = this;

                    lymp.Parent = this.splitContainer1.Panel2;
                    lymp.WindowState = FormWindowState.Maximized;
                    lymp.Show();

                    break;


                case "机加工件工序查看":
                    foreach (Form frm in this.splitContainer1.Panel2.Controls)
                    {
                        if (frm is LY_Machine_Process_view)
                        {
                            frm.WindowState = FormWindowState.Maximized;
                            frm.Activate();
                            return;
                        }
                    }
                    LY_Machine_Process_view lymp2 = new LY_Machine_Process_view();

                    lymp2.MdiParent = this;

                    lymp2.Parent = this.splitContainer1.Panel2;
                    lymp2.WindowState = FormWindowState.Maximized;
                    lymp2.Show();

                    break;

                case "外协工件工序设置":
                    foreach (Form frm in this.splitContainer1.Panel2.Controls)
                    {
                        if (frm is LY_Outsource_Process)
                        {
                            frm.WindowState = FormWindowState.Maximized;
                            frm.Activate();
                            return;
                        }
                    }
                    LY_Outsource_Process lyop = new LY_Outsource_Process();

                    lyop.MdiParent = this;

                    lyop.Parent = this.splitContainer1.Panel2;
                    lyop.WindowState = FormWindowState.Maximized;
                    lyop.Show();

                    break;

                case "外协件自加工工序":
                    foreach (Form frm in this.splitContainer1.Panel2.Controls)
                    {
                        if (frm is LY_Outsource_Process_Selfmake)
                        {
                            frm.WindowState = FormWindowState.Maximized;
                            frm.Activate();
                            return;
                        }
                    }
                    LY_Outsource_Process_Selfmake lyopself = new LY_Outsource_Process_Selfmake();

                    lyopself.MdiParent = this;

                    lyopself.Parent = this.splitContainer1.Panel2;
                    lyopself.WindowState = FormWindowState.Maximized;
                    lyopself.Show();

                    break;

                case "物料采购单价查询":
                    foreach (Form frm in this.splitContainer1.Panel2.Controls)
                    {
                        if (frm is LY_MatiarailPriceQuery)
                        {
                            frm.WindowState = FormWindowState.Maximized;
                            frm.Activate();
                            return;
                        }
                    }
                    LY_MatiarailPriceQuery mpquery = new LY_MatiarailPriceQuery();

                    mpquery.MdiParent = this;

                    mpquery.Parent = this.splitContainer1.Panel2;
                    mpquery.WindowState = FormWindowState.Maximized;
                    mpquery.Show();

                    break;

                case "机加人员管理":
                    foreach (Form frm in this.splitContainer1.Panel2.Controls)
                    {
                        if (frm is LY_Worker_Manage)
                        {

                            LY_Worker_Manage lwm = frm as LY_Worker_Manage;
                            lwm.Prod_code = "04";
                           
                            frm.WindowState = FormWindowState.Maximized;

                            frm.Activate();
                            return;
                        }
                    }
                    LY_Worker_Manage lwmjj = new LY_Worker_Manage();
                    //lysupplier_wx.Sortcode = "4";
                    lwmjj.Prod_code = "04";

                    lwmjj.MdiParent = this;

                    lwmjj.Parent = this.splitContainer1.Panel2;
                    lwmjj.WindowState = FormWindowState.Maximized;
                    lwmjj.Show();

                    break;
                //
                case "钳装人员管理":
                    foreach (Form frm in this.splitContainer1.Panel2.Controls)
                    {
                        if (frm is LY_Worker_Manage)
                        {

                            LY_Worker_Manage lwm = frm as LY_Worker_Manage;
                            lwm.Prod_code = "01";

                            frm.WindowState = FormWindowState.Maximized;

                            frm.Activate();
                            return;
                        }
                    }
                    LY_Worker_Manage lwmjjq = new LY_Worker_Manage();
                    //lysupplier_wx.Sortcode = "4";
                    lwmjjq.Prod_code = "01";

                    lwmjjq.MdiParent = this;

                    lwmjjq.Parent = this.splitContainer1.Panel2;
                    lwmjjq.WindowState = FormWindowState.Maximized;
                    lwmjjq.Show();

                    break;
                //
                case "电装人员管理":
                    foreach (Form frm in this.splitContainer1.Panel2.Controls)
                    {
                        if (frm is LY_Worker_Manage)
                        {

                            LY_Worker_Manage lwm = frm as LY_Worker_Manage;
                            lwm.Prod_code = "02";

                            frm.WindowState = FormWindowState.Maximized;

                            frm.Activate();
                            return;
                        }
                    }
                    LY_Worker_Manage lwmjjd = new LY_Worker_Manage();
                    //lysupplier_wx.Sortcode = "4";
                    lwmjjd.Prod_code = "02";

                    lwmjjd.MdiParent = this;

                    lwmjjd.Parent = this.splitContainer1.Panel2;
                    lwmjjd.WindowState = FormWindowState.Maximized;
                    lwmjjd.Show();

                    break;
                //

                case "机加序检":
                    foreach (Form frm in this.splitContainer1.Panel2.Controls)
                    {
                        if (frm is LY_Quality_Control_JG)
                        {

                          

                            frm.WindowState = FormWindowState.Maximized;

                            frm.Activate();
                            return;
                        }
                    }
                    LY_Quality_Control_JG qcjj = new LY_Quality_Control_JG();

                    qcjj.MdiParent = this;

                    qcjj.Parent = this.splitContainer1.Panel2;
                    qcjj.WindowState = FormWindowState.Maximized;
                    qcjj.Show();

                    break;
                /////////////////////////////////////////////



                case "机加序检报告":
                    foreach (Form frm in this.splitContainer1.Panel2.Controls)
                    {
                        if (frm is LY_Quality_Control_JGRep)
                        {



                            frm.WindowState = FormWindowState.Maximized;

                            frm.Activate();
                            return;
                        }
                    }
                    LY_Quality_Control_JGRep xx = new LY_Quality_Control_JGRep();

                    xx.MdiParent = this;

                    xx.Parent = this.splitContainer1.Panel2;
                    xx.WindowState = FormWindowState.Maximized;
                    xx.Show();

                    break;

                case "机加序检总检统计":
                    foreach (Form frm in this.splitContainer1.Panel2.Controls)
                    {
                        if (frm is LY_Quality_ZJ_XJ_Rep)
                        {



                            frm.WindowState = FormWindowState.Maximized;

                            frm.Activate();
                            return;
                        }
                    }
                    LY_Quality_ZJ_XJ_Rep xj_zj = new LY_Quality_ZJ_XJ_Rep();

                    xj_zj.MdiParent = this;
                    
                    xj_zj.Parent = this.splitContainer1.Panel2;
                    xj_zj.WindowState = FormWindowState.Maximized;
                    xj_zj.Show();

                    break;

                    
                case "机加总检报告":
                    foreach (Form frm in this.splitContainer1.Panel2.Controls)
                    {
                        if (frm is LY_Quality_Control_JG_MainRep)
                        {



                            frm.WindowState = FormWindowState.Maximized;

                            frm.Activate();
                            return;
                        }
                    }
                    LY_Quality_Control_JG_MainRep zz = new LY_Quality_Control_JG_MainRep();

                    zz.MdiParent = this;

                    zz.Parent = this.splitContainer1.Panel2;
                    zz.WindowState = FormWindowState.Maximized;
                    zz.Show();

                    break;



                //


                case "外协采购统计报告":
                    foreach (Form frm in this.splitContainer1.Panel2.Controls)
                    {
                        if (frm is LY_WX_CG_Report)
                        {



                            frm.WindowState = FormWindowState.Maximized;

                            frm.Activate();
                            return;
                        }
                    }
                    LY_WX_CG_Report wxcg = new LY_WX_CG_Report();

                    wxcg.MdiParent = this;

                    wxcg.Parent = this.splitContainer1.Panel2;
                    wxcg.WindowState = FormWindowState.Maximized;
                    wxcg.Show();

                    break;



                //
                case "机加序检审查":
                    foreach (Form frm in this.splitContainer1.Panel2.Controls)
                    {
                        if (frm is LY_Quality_Control_TEC)
                        {



                            frm.WindowState = FormWindowState.Maximized;

                            frm.Activate();
                            return;
                        }
                    }
                    LY_Quality_Control_TEC qcjjtec = new LY_Quality_Control_TEC();

                    qcjjtec.MdiParent = this;

                    qcjjtec.Parent = this.splitContainer1.Panel2;
                    qcjjtec.WindowState = FormWindowState.Maximized;
                    qcjjtec.Show();

                    break;


                case "付款申请审批":
                    foreach (Form frm in this.splitContainer1.Panel2.Controls)
                    {
                        if (frm is LY_pay_manage)
                        {



                            frm.WindowState = FormWindowState.Maximized;

                            frm.Activate();
                            return;
                        }
                    }
                    LY_pay_manage LY_pay_manage = new LY_pay_manage();

                    LY_pay_manage.MdiParent = this;

                    LY_pay_manage.Parent = this.splitContainer1.Panel2;
                    LY_pay_manage.WindowState = FormWindowState.Maximized;
                    LY_pay_manage.Show();

                    break;

                /////////////////////////////////////////////
                case "机加总检审查":
                    foreach (Form frm in this.splitContainer1.Panel2.Controls)
                    {
                        if (frm is LY_Quality_Control_All_TEC)
                        {



                            frm.WindowState = FormWindowState.Maximized;

                            frm.Activate();
                            return;
                        }
                    }
                    LY_Quality_Control_All_TEC qcjjteca = new LY_Quality_Control_All_TEC();

                    qcjjteca.MdiParent = this;

                    qcjjteca.Parent = this.splitContainer1.Panel2;
                    qcjjteca.WindowState = FormWindowState.Maximized;
                    qcjjteca.Show();

                    break;
                /////////////////////////////////////////////
                case "机加总检":
                    foreach (Form frm in this.splitContainer1.Panel2.Controls)
                    {
                        if (frm is LY_Quality_Control_JG_All)
                        {



                            frm.WindowState = FormWindowState.Maximized;

                            frm.Activate();
                            return;
                        }
                    }
                    LY_Quality_Control_JG_All qcjja = new LY_Quality_Control_JG_All();

                    qcjja.MdiParent = this;

                    qcjja.Parent = this.splitContainer1.Panel2;
                    qcjja.WindowState = FormWindowState.Maximized;
                    qcjja.Show();

                    break;
                /////////////////////////////////////////////
                /////////////////////////////////////////////
                case "返修质检审查":
                    foreach (Form frm in this.splitContainer1.Panel2.Controls)
                    {
                        if (frm is LY_Machine_Remake_Tec)
                        {



                            frm.WindowState = FormWindowState.Maximized;

                            frm.Activate();
                            return;
                        }
                    }
                    LY_Machine_Remake_Tec remakeTec = new LY_Machine_Remake_Tec();

                    remakeTec.MdiParent = this;

                    remakeTec.Parent = this.splitContainer1.Panel2;
                    remakeTec.WindowState = FormWindowState.Maximized;
                    remakeTec.Show();

                    break;
                /////////////////////////////////////////////采购质检审查
                /////////////////////////////////////////////
                case "返修检验":
                    foreach (Form frm in this.splitContainer1.Panel2.Controls)
                    {
                        if (frm is LY_Machine_Remake_QC)
                        {



                            frm.WindowState = FormWindowState.Maximized;

                            frm.Activate();
                            return;
                        }
                    }
                    LY_Machine_Remake_QC remakeQC = new LY_Machine_Remake_QC();

                    remakeQC.MdiParent = this;

                    remakeQC.Parent = this.splitContainer1.Panel2;
                    remakeQC.WindowState = FormWindowState.Maximized;
                    remakeQC.Show();

                    break;
                /////////////////////////////////////////////统计查询(质检)
                /////////////////////////////////////////////
                case "统计查询(质检)":
                    foreach (Form frm in this.splitContainer1.Panel2.Controls)
                    {
                        if (frm is LY_Quality_SumAnalysis)
                        {



                            frm.WindowState = FormWindowState.Maximized;

                            frm.Activate();
                            return;
                        }
                    }
                    LY_Quality_SumAnalysis machineAnalysis = new LY_Quality_SumAnalysis();

                    machineAnalysis.MdiParent = this;

                    machineAnalysis.Parent = this.splitContainer1.Panel2;
                    machineAnalysis.WindowState = FormWindowState.Maximized;
                    machineAnalysis.Show();

                    break;
                /////////////////////////////////////////////统计查询(质检)
                /////////////////////////////////////////////
                case "区域信息":
                    foreach (Form frm in this.splitContainer1.Panel2.Controls)
                    {
                        if (frm is LY_Salesregion_Mange)
                        {
                            frm.WindowState = FormWindowState.Normal;
                            frm.Activate();
                            return;
                        }
                    }
                    LY_Salesregion_Mange salesregion = new LY_Salesregion_Mange();

                    //  bm.Parent_model = ((System.Windows.Forms.ToolStripItem)(sender)).OwnerItem.Text;
                    salesregion.MdiParent = this;
                    salesregion.Parent = this.splitContainer1.Panel2;
                    salesregion.WindowState = FormWindowState.Normal;
                    salesregion.StartPosition = FormStartPosition.Manual;
                    salesregion.Location = new Point(100, 100);
                    salesregion.Show();
                    break;
                /////////////////////////////////////////////
                case "赠送类别设置":
                    foreach (Form frm in this.splitContainer1.Panel2.Controls)
                    {
                        if (frm is LY_SalesGiftstyle_Set)
                        {
                            frm.WindowState = FormWindowState.Normal;
                            frm.Activate();
                            return;
                        }
                    }
                    LY_SalesGiftstyle_Set salesGiftstyle = new LY_SalesGiftstyle_Set();

                    //  bm.Parent_model = ((System.Windows.Forms.ToolStripItem)(sender)).OwnerItem.Text;
                    salesGiftstyle.MdiParent = this;
                    salesGiftstyle.Parent = this.splitContainer1.Panel2;
                    salesGiftstyle.WindowState = FormWindowState.Normal;
                    salesGiftstyle.StartPosition = FormStartPosition.Manual;
                    salesGiftstyle.Location = new Point(100, 100);
                    salesGiftstyle.Show();
                    break;

                /////////////////////////////////////////////
                case "公司基本信息":
                    foreach (Form frm in this.splitContainer1.Panel2.Controls)
                    {
                        if (frm is LY_Company_Info)
                        {
                            frm.WindowState = FormWindowState.Normal;
                            frm.Activate();
                            return;
                        }
                    }
                    LY_Company_Info companyinfo = new LY_Company_Info();

                    //  bm.Parent_model = ((System.Windows.Forms.ToolStripItem)(sender)).OwnerItem.Text;
                    companyinfo.MdiParent = this;
                    companyinfo.Parent = this.splitContainer1.Panel2;
                    companyinfo.WindowState = FormWindowState.Normal;
                    companyinfo.StartPosition = FormStartPosition.Manual;
                    companyinfo.Location = new Point(100, 100);
                    companyinfo.Show();
                    break;
                /////////////////////////////////////////////
               
                case "营业人员":
                    foreach (Form frm in this.splitContainer1.Panel2.Controls)
                    {
                        if (frm is LY_Salesperson_Mange)
                        {
                            frm.WindowState = FormWindowState.Normal;
                            frm.Activate();
                            return;
                        }
                    }
                    LY_Salesperson_Mange salesperson = new LY_Salesperson_Mange();

                    //  bm.Parent_model = ((System.Windows.Forms.ToolStripItem)(sender)).OwnerItem.Text;
                    salesperson.MdiParent = this;
                    salesperson.Parent = this.splitContainer1.Panel2;
                    salesperson.WindowState = FormWindowState.Normal;
                    salesperson.StartPosition = FormStartPosition.Manual;
                    salesperson.Location = new Point(100, 100);
                    salesperson.Show();
                    break;
                /////////////////////////////////////////////、、、


                /////////////////////////////////////////////////////////////////
                case "客户信息":
                    foreach (Form frm in this.splitContainer1.Panel2.Controls)
                    {
                        if (frm is LY_Salesclient_Mange)
                        {



                            frm.WindowState = FormWindowState.Maximized;

                            frm.Activate();
                            return;
                        }
                    }
                    LY_Salesclient_Mange salesclient = new LY_Salesclient_Mange();

                    salesclient.MdiParent = this;

                    salesclient.Parent = this.splitContainer1.Panel2;
                    salesclient.WindowState = FormWindowState.Maximized;
                    salesclient.Show();

                    break;
                /////////////////////////////////////////////
                /////////////////////////////////////////////
                case "营业应收":
                    //foreach (Form frm in this.splitContainer1.Panel2.Controls)
                    //{
                    //    if (frm is LY_Salesclient_Receivables_Mange)
                    //    {



                    //        frm.WindowState = FormWindowState.Maximized;

                    //        frm.Activate();
                    //        return;
                    //    }
                    //}
                    //LY_Salesclient_Receivables_Mange salesclientReceivables = new LY_Salesclient_Receivables_Mange();

                    //salesclientReceivables.MdiParent = this;

                    //salesclientReceivables.Parent = this.splitContainer1.Panel2;
                    //salesclientReceivables.WindowState = FormWindowState.Maximized;
                    //salesclientReceivables.Show();

                    break;
                /////////////////////////////////////////////
                case "合同条款":
                    foreach (Form frm in this.splitContainer1.Panel2.Controls)
                    {
                        if (frm is LY_ContractTerms_Mange)
                        {
                            frm.WindowState = FormWindowState.Normal;
                            frm.Activate();
                            return;
                        }
                    }
                    LY_ContractTerms_Mange contractterm = new LY_ContractTerms_Mange();

                    //  bm.Parent_model = ((System.Windows.Forms.ToolStripItem)(sender)).OwnerItem.Text;
                    contractterm.MdiParent = this;
                    contractterm.Parent = this.splitContainer1.Panel2;
                    contractterm.WindowState = FormWindowState.Normal;
                    contractterm.StartPosition = FormStartPosition.Manual;
                    contractterm.Location = new Point(100, 100);
                    contractterm.Show();
                    break;
                /////////////////////////////////////////////
                case "采购合同条款":
                    foreach (Form frm in this.splitContainer1.Panel2.Controls)
                    {
                        if (frm is LY_PurchaseTerms_Mange)
                        {
                            frm.WindowState = FormWindowState.Normal;
                            frm.Activate();
                            return;
                        }
                    }
                    LY_PurchaseTerms_Mange contractterm_purchase = new LY_PurchaseTerms_Mange();

                    //  bm.Parent_model = ((System.Windows.Forms.ToolStripItem)(sender)).OwnerItem.Text;
                    contractterm_purchase.MdiParent = this;
                    contractterm_purchase.Parent = this.splitContainer1.Panel2;
                    contractterm_purchase.WindowState = FormWindowState.Normal;
                    contractterm_purchase.StartPosition = FormStartPosition.Manual;
                    contractterm_purchase.Location = new Point(100, 100);
                    contractterm_purchase.Show();
                    break;
                /////////////////////////////////////////////
                case "外协合同条款":
                    foreach (Form frm in this.splitContainer1.Panel2.Controls)
                    {
                        if (frm is LY_OutsourceTerms_Mange)
                        {
                            frm.WindowState = FormWindowState.Normal;
                            frm.Activate();
                            return;
                        }
                    }
                    LY_OutsourceTerms_Mange contractterm_outsource = new LY_OutsourceTerms_Mange();

                    //  bm.Parent_model = ((System.Windows.Forms.ToolStripItem)(sender)).OwnerItem.Text;
                    contractterm_outsource.MdiParent = this;
                    contractterm_outsource.Parent = this.splitContainer1.Panel2;
                    contractterm_outsource.WindowState = FormWindowState.Normal;
                    contractterm_outsource.StartPosition = FormStartPosition.Manual;
                    contractterm_outsource.Location = new Point(100, 100);
                    contractterm_outsource.Show();
                    break;
              
                /////////////////////////////////////////////
                case "委托合同条款":
                    foreach (Form frm in this.splitContainer1.Panel2.Controls)
                    {
                        if (frm is LY_MachineTerms_Mange)
                        {
                            frm.WindowState = FormWindowState.Normal;
                            frm.Activate();
                            return;
                        }
                    }
                    LY_MachineTerms_Mange contractterm_machine = new LY_MachineTerms_Mange();

                    //  bm.Parent_model = ((System.Windows.Forms.ToolStripItem)(sender)).OwnerItem.Text;
                    contractterm_machine.MdiParent = this;
                    contractterm_machine.Parent = this.splitContainer1.Panel2;
                    contractterm_machine.WindowState = FormWindowState.Normal;
                    contractterm_machine.StartPosition = FormStartPosition.Manual;
                    contractterm_machine.Location = new Point(100, 100);
                    contractterm_machine.Show();
                    break;
               


                /////////////////////////////////////////////
                case "追加领料统计":
                    foreach (Form frm in this.splitContainer1.Panel2.Controls)
                    {
                        if (frm is LY_Picking_Statistical)
                        {
                            frm.WindowState = FormWindowState.Maximized;
                            frm.Activate();
                            return;
                        }
                    }
                    LY_Picking_Statistical zjll = new LY_Picking_Statistical();

                    //  bm.Parent_model = ((System.Windows.Forms.ToolStripItem)(sender)).OwnerItem.Text;
                    zjll.MdiParent = this;
                    zjll.Parent = this.splitContainer1.Panel2;
                    zjll.WindowState = FormWindowState.Maximized;
       
                    zjll.Show();



                    break;
                /////////////////////////////////////////////

                                             


                case "营业日常业务":
                    foreach (Form frm in this.splitContainer1.Panel2.Controls)
                    {
                        if (frm is LY_Salescontract_Group)
                        {



                            frm.WindowState = FormWindowState.Maximized;

                            frm.Activate();
                            return;
                        }
                    }
                    LY_Salescontract_Group salescontract = new LY_Salescontract_Group();

                    salescontract.MdiParent = this;

                    salescontract.Parent = this.splitContainer1.Panel2;
                    salescontract.WindowState = FormWindowState.Maximized;
                    salescontract.Show();

                    break;
                /////////////////////////////////////////////
                case "产品销售订价":
                    foreach (Form frm in this.splitContainer1.Panel2.Controls)
                    {
                        if (frm is LY_Material_chengpin)
                        {



                            frm.WindowState = FormWindowState.Maximized;

                            frm.Activate();
                            return;
                        }
                    }
                    LY_Material_chengpin salesprice = new LY_Material_chengpin();

                    salesprice.MdiParent = this;

                    salesprice.Parent = this.splitContainer1.Panel2;
                    salesprice.WindowState = FormWindowState.Maximized;
                    salesprice.Show();

                    break;
                /////////////////////////////////////////////

                /////////////////////////////////////////////
                
                case "维修配件定价":
                    foreach (Form frm in this.splitContainer1.Panel2.Controls)
                    {
                        if (frm is LY_Material_repair)
                        {

                            frm.WindowState = FormWindowState.Maximized;

                            frm.Activate();
                            return;
                        }
                    }
                    LY_Material_repair repairprice = new LY_Material_repair();

                    repairprice.MdiParent = this;

                    repairprice.Parent = this.splitContainer1.Panel2;
                    repairprice.WindowState = FormWindowState.Maximized;
                    repairprice.Show();

                    break;
                /////////////////////////////////////////////

                case "折扣权限":
                    foreach (Form frm in this.splitContainer1.Panel2.Controls)
                    {
                        if (frm is LY_SalesDiscount_Mange)
                        {
                            frm.WindowState = FormWindowState.Normal;
                            frm.Activate();
                            return;
                        }
                    }
                    LY_SalesDiscount_Mange salesdiscount = new LY_SalesDiscount_Mange();

                    //  bm.Parent_model = ((System.Windows.Forms.ToolStripItem)(sender)).OwnerItem.Text;
                    salesdiscount.MdiParent = this;
                    salesdiscount.Parent = this.splitContainer1.Panel2;
                    salesdiscount.WindowState = FormWindowState.Normal;
                    salesdiscount.StartPosition = FormStartPosition.Manual;
                    salesdiscount.Location = new Point(100, 100);
                    salesdiscount.Show();
                    break;

                case "借用管理":
                    foreach (Form frm in this.splitContainer1.Panel2.Controls)
                    {
                        if (frm is LY_SalseBorrow_Mange)
                        {


                            frm.WindowState = FormWindowState.Maximized;

                            frm.Activate();
                            return;
                        }
                    }
                    LY_SalseBorrow_Mange salesborrow = new LY_SalseBorrow_Mange();

                    salesborrow.MdiParent = this;

                    salesborrow.Parent = this.splitContainer1.Panel2;
                    salesborrow.WindowState = FormWindowState.Maximized;
                    salesborrow.Show();

                    break;
                /////////////////////////////////////////////
                case "借用查询":
                    foreach (Form frm in this.splitContainer1.Panel2.Controls)
                    {
                        if (frm is LY_SalseBorrow_Query)
                        {



                            frm.WindowState = FormWindowState.Maximized;

                            frm.Activate();
                            return;
                        }
                    }
                    LY_SalseBorrow_Query salesborrowquery = new LY_SalseBorrow_Query();

                    salesborrowquery.MdiParent = this;

                    salesborrowquery.Parent = this.splitContainer1.Panel2;
                    salesborrowquery.WindowState = FormWindowState.Maximized;
                    salesborrowquery.Show();

                    break;
                /////////////////////////////////////////////
                case "借用部门审批":
                    foreach (Form frm in this.splitContainer1.Panel2.Controls)
                    {
                        if (frm is LY_SalseBorrow_Approve)
                        {



                            frm.WindowState = FormWindowState.Maximized;

                            frm.Activate();
                            return;
                        }
                    }
                    LY_SalseBorrow_Approve salesborrowapp = new LY_SalseBorrow_Approve();

                    salesborrowapp.MdiParent = this;

                    salesborrowapp.Parent = this.splitContainer1.Panel2;
                    salesborrowapp.WindowState = FormWindowState.Maximized;
                    salesborrowapp.Show();

                    break;

                ///////////////////////////////
                case "借用公司批准":

                    foreach (Form frm in this.splitContainer1.Panel2.Controls)
                    {
                        if (frm is LY_SalseBorrow_Com)
                        {



                            frm.WindowState = FormWindowState.Maximized;

                            frm.Activate();
                            return;
                        }
                    }
                    LY_SalseBorrow_Com salesborrowscom = new LY_SalseBorrow_Com();

                    salesborrowscom.MdiParent = this;

                    salesborrowscom.Parent = this.splitContainer1.Panel2;
                    salesborrowscom.WindowState = FormWindowState.Maximized;
                    salesborrowscom.Show();

                    break;

                /////////////////////////////////////////////
                case "借用生产审批":
                  
                    foreach (Form frm in this.splitContainer1.Panel2.Controls)
                    {
                        if (frm is LY_SalseBorrow_Pro)
                        {



                            frm.WindowState = FormWindowState.Maximized;

                            frm.Activate();
                            return;
                        }
                    }
                    LY_SalseBorrow_Pro salesborrowspro = new LY_SalseBorrow_Pro();

                    salesborrowspro.MdiParent = this;

                    salesborrowspro.Parent = this.splitContainer1.Panel2;
                    salesborrowspro.WindowState = FormWindowState.Maximized;
                    salesborrowspro.Show();

                    break;
                /////////////////////////////////////////////
                case "借用事务管理":
                    foreach (Form frm in this.splitContainer1.Panel2.Controls)
                    {
                        if (frm is LY_SalseBorrow_Daily)
                        {



                            frm.WindowState = FormWindowState.Maximized;

                            frm.Activate();
                            return;
                        }
                    }
                    LY_SalseBorrow_Daily salesborrowsaily = new LY_SalseBorrow_Daily();

                    salesborrowsaily.MdiParent = this;

                    salesborrowsaily.Parent = this.splitContainer1.Panel2;
                    salesborrowsaily.WindowState = FormWindowState.Maximized;
                    salesborrowsaily.Show();

                    break;
                /////////////////////////////////////////////
                case "营业审批业务":
                    foreach (Form frm in this.splitContainer1.Panel2.Controls)
                    {
                        if (frm is LY_Salescontract_Approve)
                        {



                            frm.WindowState = FormWindowState.Maximized;

                            frm.Activate();
                            return;
                        }
                    }
                    LY_Salescontract_Approve salescontract_approve = new LY_Salescontract_Approve();

                    salescontract_approve.MdiParent = this;

                    salescontract_approve.Parent = this.splitContainer1.Panel2;
                    salescontract_approve.WindowState = FormWindowState.Maximized;
                    salescontract_approve.Show();

                    break;
                /////////////////////////////////////////////
                /////////////////////////////////////////////
                case "营业事务管理":
                    foreach (Form frm in this.splitContainer1.Panel2.Controls)
                    {
                        if (frm is LY_Salescontract_Daily)
                        {



                            frm.WindowState = FormWindowState.Maximized;

                            frm.Activate();
                            return;
                        }
                    }
                    LY_Salescontract_Daily salescontractdaily = new LY_Salescontract_Daily();

                    salescontractdaily.MdiParent = this;

                    salescontractdaily.Parent = this.splitContainer1.Panel2;
                    salescontractdaily.WindowState = FormWindowState.Maximized;
                    salescontractdaily.Show();

                    break;
                /////////////////////////////////////////////
                case "中成合同":
                    foreach (Form frm in this.splitContainer1.Panel2.Controls)
                    {
                        if (frm is LY_Salescontract_Daily_ZC)
                        {



                            frm.WindowState = FormWindowState.Maximized;

                            frm.Activate();
                            return;
                        }
                    }
                    LY_Salescontract_Daily_ZC salescontractdaily_zc = new LY_Salescontract_Daily_ZC();

                    salescontractdaily_zc.MdiParent = this;

                    salescontractdaily_zc.Parent = this.splitContainer1.Panel2;
                    salescontractdaily_zc.WindowState = FormWindowState.Maximized;
                    salescontractdaily_zc.Show();

                    break;
                /////////////////////////////////////////////
                case "营业合同配套":
                    foreach (Form frm in this.splitContainer1.Panel2.Controls)
                    {
                        if (frm is LY_Salescontract_Group)
                        {



                            frm.WindowState = FormWindowState.Maximized;

                            frm.Activate();
                            return;
                        }
                    }
                    LY_Salescontract_Group salescontractgroup = new LY_Salescontract_Group();

                    salescontractgroup.MdiParent = this;

                    salescontractgroup.Parent = this.splitContainer1.Panel2;
                    salescontractgroup.WindowState = FormWindowState.Maximized;
                    salescontractgroup.Show();

                    break;
                /////////////////////////////////////////////
                case "依赖书配套":
                    foreach (Form frm in this.splitContainer1.Panel2.Controls)
                    {
                        if (frm is LY_Salescontract_GroupTec)
                        {



                            frm.WindowState = FormWindowState.Maximized;

                            frm.Activate();
                            return;
                        }
                    }
                    LY_Salescontract_GroupTec salescontractgrouptec = new LY_Salescontract_GroupTec();

                    salescontractgrouptec.MdiParent = this;

                    salescontractgrouptec.Parent = this.splitContainer1.Panel2;
                    salescontractgrouptec.WindowState = FormWindowState.Maximized;
                    salescontractgrouptec.Show();

                    break;
                /////////////////////////////////////////////营业配套审核
                /////////////////////////////////////////////
                case "营业配套审核":
                    foreach (Form frm in this.splitContainer1.Panel2.Controls)
                    {
                        if (frm is LY_Salescontract_GroupPro)
                        {



                            frm.WindowState = FormWindowState.Maximized;

                            frm.Activate();
                            return;
                        }
                    }
                    LY_Salescontract_GroupPro salescontractgrouppro = new LY_Salescontract_GroupPro();

                    salescontractgrouppro.MdiParent = this;

                    salescontractgrouppro.Parent = this.splitContainer1.Panel2;
                    salescontractgrouppro.WindowState = FormWindowState.Maximized;
                    salescontractgrouppro.Show();

                    break;
                /////////////////////////////////////////////

                case "配调检验":
                    foreach (Form frm in this.splitContainer1.Panel2.Controls)
                    {
                        if (frm is LY_Salescontract_GroupDebug)
                        {



                            frm.WindowState = FormWindowState.Maximized;

                            frm.Activate();
                            return;
                        }
                    }
                    LY_Salescontract_GroupDebug salescontractgroupdbg = new LY_Salescontract_GroupDebug();

                    salescontractgroupdbg.MdiParent = this;

                    salescontractgroupdbg.Parent = this.splitContainer1.Panel2;
                    salescontractgroupdbg.WindowState = FormWindowState.Maximized;
                    salescontractgroupdbg.Show();

                    break;
                /////////////////////////////////////////////
                case "配调查询":
                    foreach (Form frm in this.splitContainer1.Panel2.Controls)
                    {
                        if (frm is LY_Salescontract_GroupDebug_Query)
                        {



                            frm.WindowState = FormWindowState.Maximized;

                            frm.Activate();
                            return;
                        }
                    }
                    LY_Salescontract_GroupDebug_Query salescontractDebug_Query = new LY_Salescontract_GroupDebug_Query();

                    salescontractDebug_Query.MdiParent = this;

                    salescontractDebug_Query.Parent = this.splitContainer1.Panel2;
                    salescontractDebug_Query.WindowState = FormWindowState.Maximized;
                    salescontractDebug_Query.Show();

                    break;
                /////////////////////////////////////////////
                case "配调历史信息":
                    foreach (Form frm in this.splitContainer1.Panel2.Controls)
                    {
                        if (frm is LY_Salescontract_Debug_Query_his)
                        {



                            frm.WindowState = FormWindowState.Maximized;

                            frm.Activate();
                            return;
                        }
                    }
                    LY_Salescontract_Debug_Query_his salescontractDebug_His = new LY_Salescontract_Debug_Query_his();

                    salescontractDebug_His.MdiParent = this;

                    salescontractDebug_His.Parent = this.splitContainer1.Panel2;
                    salescontractDebug_His.WindowState = FormWindowState.Maximized;
                    salescontractDebug_His.Show();

                    break;
                /////////////////////////////////////////////
                case "配调汇总":
                    foreach (Form frm in this.splitContainer1.Panel2.Controls)
                    {
                        if (frm is LY_QualityDebug_Sumreport)
                        {

                            frm.WindowState = FormWindowState.Maximized;

                            frm.Activate();
                            return;
                        }
                    }
                    LY_QualityDebug_Sumreport salescontractDebug_Sum = new LY_QualityDebug_Sumreport();

                    salescontractDebug_Sum.MdiParent = this;

                    salescontractDebug_Sum.Parent = this.splitContainer1.Panel2;
                    salescontractDebug_Sum.WindowState = FormWindowState.Maximized;
                    salescontractDebug_Sum.Show();

                    break;
                /////////////////////////////////////////////
                case "营业发货管理":
                    foreach (Form frm in this.splitContainer1.Panel2.Controls)
                    {
                        if (frm is LY_Salescontract_Deliver)
                        {

                            frm.WindowState = FormWindowState.Maximized;

                            frm.Activate();
                            return;
                        }
                    }
                    LY_Salescontract_Deliver salescontractdeliver = new LY_Salescontract_Deliver();

                    salescontractdeliver.MdiParent = this;

                    salescontractdeliver.Parent = this.splitContainer1.Panel2;
                    salescontractdeliver.WindowState = FormWindowState.Maximized;
                    salescontractdeliver.Show();

                    break;
                /////////////////////////////////////////////
                case "维修发货":
                    foreach (Form frm in this.splitContainer1.Panel2.Controls)
                    {
                        if (frm is LY_Salesrepair_Deliver)
                        {



                            frm.WindowState = FormWindowState.Maximized;

                            frm.Activate();
                            return;
                        }
                    }
                    LY_Salesrepair_Deliver salesrepairdeliver = new LY_Salesrepair_Deliver();

                    salesrepairdeliver.MdiParent = this;

                    salesrepairdeliver.Parent = this.splitContainer1.Panel2;
                    salesrepairdeliver.WindowState = FormWindowState.Maximized;
                    salesrepairdeliver.Show();

                    break;
                /////////////////////////////////////////////
                case "营业出库审核":
                    foreach (Form frm in this.splitContainer1.Panel2.Controls)
                    {
                        if (frm is LY_Salescontract_GroupOut)
                        {



                            frm.WindowState = FormWindowState.Maximized;

                            frm.Activate();
                            return;
                        }
                    }
                    LY_Salescontract_GroupOut salescontractout = new LY_Salescontract_GroupOut();

                    salescontractout.MdiParent = this;

                    salescontractout.Parent = this.splitContainer1.Panel2;
                    salescontractout.WindowState = FormWindowState.Maximized;
                    salescontractout.Show();

                    break;
                /////////////////////////////////////////////
                case "营业发货":
                    foreach (Form frm in this.splitContainer1.Panel2.Controls)
                    {
                        if (frm is LY_Store_Out_Sales)
                        {



                            frm.WindowState = FormWindowState.Maximized;

                            frm.Activate();
                            return;
                        }
                    }
                    LY_Store_Out_Sales storeoutsales = new LY_Store_Out_Sales();

                    storeoutsales.MdiParent = this;

                    storeoutsales.Parent = this.splitContainer1.Panel2;
                    storeoutsales.WindowState = FormWindowState.Maximized;
                    storeoutsales.Show();

                    break;
                /////////////////////////////////////////////
                case "营业借用返库":
                    foreach (Form frm in this.splitContainer1.Panel2.Controls)
                    {
                        if (frm is LY_Store_In_Salseborrow)
                        {



                            frm.WindowState = FormWindowState.Maximized;

                            frm.Activate();
                            return;
                        }
                    }
                    LY_Store_In_Salseborrow storeinsales = new LY_Store_In_Salseborrow();

                    storeinsales.MdiParent = this;

                    storeinsales.Parent = this.splitContainer1.Panel2;
                    storeinsales.WindowState = FormWindowState.Maximized;
                    storeinsales.Show();

                    break;
                /////////////////////////////////////////////营业借用返库
                case "营业收件管理":
                    foreach (Form frm in this.splitContainer1.Panel2.Controls)
                    {
                        if (frm is LY_Receive_Record)
                        {



                            frm.WindowState = FormWindowState.Maximized;

                            frm.Activate();
                            return;
                        }
                    }
                    LY_Receive_Record receives = new LY_Receive_Record();

                    receives.MdiParent = this;

                    receives.Parent = this.splitContainer1.Panel2;
                    receives.WindowState = FormWindowState.Maximized;
                    receives.Show();

                    break;
                /////////////////////////////////////////////
                case "维修业务":
                    foreach (Form frm in this.splitContainer1.Panel2.Controls)
                    {
                        if (frm is LY_Receive_Repair)
                        {



                            frm.WindowState = FormWindowState.Maximized;

                            frm.Activate();
                            return;
                        }
                    }
                    LY_Receive_Repair repairs = new LY_Receive_Repair();

                    repairs.MdiParent = this;

                    repairs.Parent = this.splitContainer1.Panel2;
                    repairs.WindowState = FormWindowState.Maximized;
                    repairs.Show();

                    break;
                /////////////////////////////////////////////
                case "维修审批":
                    foreach (Form frm in this.splitContainer1.Panel2.Controls)
                    {
                        if (frm is LY_Receive_Repair_approve)
                        {



                            frm.WindowState = FormWindowState.Maximized;

                            frm.Activate();
                            return;
                        }
                    }
                    LY_Receive_Repair_approve repairsapprove = new LY_Receive_Repair_approve();

                    repairsapprove.MdiParent = this;

                    repairsapprove.Parent = this.splitContainer1.Panel2;
                    repairsapprove.WindowState = FormWindowState.Maximized;
                    repairsapprove.Show();

                    break;
                /////////////////////////////////////////////
                case "维修事务":
                    foreach (Form frm in this.splitContainer1.Panel2.Controls)
                    {
                        if (frm is LY_Receive_Repair_daily)
                        {



                            frm.WindowState = FormWindowState.Maximized;

                            frm.Activate();
                            return;
                        }
                    }
                    LY_Receive_Repair_daily repairdaily = new LY_Receive_Repair_daily();

                    repairdaily.MdiParent = this;

                    repairdaily.Parent = this.splitContainer1.Panel2;
                    repairdaily.WindowState = FormWindowState.Maximized;
                    repairdaily.Show();

                    break;
                /////////////////////////////////////////////维修检验统计
                case "维修统计查询":
                    foreach (Form frm in this.splitContainer1.Panel2.Controls)
                    {
                        if (frm is LY_SalseRepair_StandardReport)
                        {



                            frm.WindowState = FormWindowState.Maximized;

                            frm.Activate();
                            return;
                        }
                    }
                    LY_SalseRepair_StandardReport repairreport = new LY_SalseRepair_StandardReport();

                    repairreport.MdiParent = this;

                    repairreport.Parent = this.splitContainer1.Panel2;
                    repairreport.WindowState = FormWindowState.Maximized;
                    repairreport.Show();

                    break;
                /////////////////////////////////////////////维修检验统计
                case "维修积压数":
                    foreach (Form frm in this.splitContainer1.Panel2.Controls)
                    {
                        if (frm is LY_SalseRepair_Accum)
                        {



                            frm.WindowState = FormWindowState.Maximized;

                            frm.Activate();
                            return;
                        }
                    }
                    LY_SalseRepair_Accum LY_SalseRepair_Accum = new LY_SalseRepair_Accum();

                    LY_SalseRepair_Accum.MdiParent = this;

                    LY_SalseRepair_Accum.Parent = this.splitContainer1.Panel2;
                    LY_SalseRepair_Accum.WindowState = FormWindowState.Maximized;
                    LY_SalseRepair_Accum.Show();

                    break;

                /////////////////////////////////////////////维修检验统计
                case "收件质检统计":
                    foreach (Form frm in this.splitContainer1.Panel2.Controls)
                    {
                        if (frm is LY_SalseRepair_QCReport)
                        {



                            frm.WindowState = FormWindowState.Maximized;

                            frm.Activate();
                            return;
                        }
                    }
                    LY_SalseRepair_QCReport repairreportQC = new LY_SalseRepair_QCReport();

                    repairreportQC.MdiParent = this;

                    repairreportQC.Parent = this.splitContainer1.Panel2;
                    repairreportQC.WindowState = FormWindowState.Maximized;
                    repairreportQC.Show();

                    break;

                /////////////////////////////////////////////
                case "客户金额查询":
                    foreach (Form frm in this.splitContainer1.Panel2.Controls)
                    {
                        if (frm is LY_sales_ClientSUM_Marposs)
                        {



                            frm.WindowState = FormWindowState.Maximized;

                            frm.Activate();
                            return;
                        }
                    }
                    LY_sales_ClientSUM_Marposs clientSum = new LY_sales_ClientSUM_Marposs();

                    clientSum.MdiParent = this;

                    clientSum.Parent = this.splitContainer1.Panel2;
                    clientSum.WindowState = FormWindowState.Maximized;
                    clientSum.Show();

                    break;

                /////////////////////////////////////////////
                case "维修换件查询":
                    foreach (Form frm in this.splitContainer1.Panel2.Controls)
                    {
                        if (frm is LY_SalseRepair_Replacement)
                        {



                            frm.WindowState = FormWindowState.Maximized;

                            frm.Activate();
                            return;
                        }
                    }
                    LY_SalseRepair_Replacement repairReplace = new LY_SalseRepair_Replacement();

                    repairReplace.MdiParent = this;

                    repairReplace.Parent = this.splitContainer1.Panel2;
                    repairReplace.WindowState = FormWindowState.Maximized;
                    repairReplace.Show();

                    break;
                /////////////////////////////////////////////
                case "维修业务统计查询":
                    foreach (Form frm in this.splitContainer1.Panel2.Controls)
                    {
                        if (frm is LY_SalseRepair_SumQuery)
                        {



                            frm.WindowState = FormWindowState.Maximized;

                            frm.Activate();
                            return;
                        }
                    }
                    LY_SalseRepair_SumQuery repairSumquery = new LY_SalseRepair_SumQuery();

                    repairSumquery.MdiParent = this;

                    repairSumquery.Parent = this.splitContainer1.Panel2;
                    repairSumquery.WindowState = FormWindowState.Maximized;
                    repairSumquery.Show();

                    break;
                /////////////////////////////////////////////
                case "营业返库检验":
                    foreach (Form frm in this.splitContainer1.Panel2.Controls)
                    {
                        if (frm is LY_Receive_Repair_QualityIN)
                        {



                            frm.WindowState = FormWindowState.Maximized;

                            frm.Activate();
                            return;
                        }
                    }
                    LY_Receive_Repair_QualityIN repairsin = new LY_Receive_Repair_QualityIN();

                    repairsin.MdiParent = this;

                    repairsin.Parent = this.splitContainer1.Panel2;
                    repairsin.WindowState = FormWindowState.Maximized;
                    repairsin.Show();

                    break;
                /////////////////////////////////////////////
                case "营业维修检验":
                    foreach (Form frm in this.splitContainer1.Panel2.Controls)
                    {
                        if (frm is LY_Receive_Repair_QualityOut)
                        {



                            frm.WindowState = FormWindowState.Maximized;

                            frm.Activate();
                            return;
                        }
                    }
                    LY_Receive_Repair_QualityOut repairsout = new LY_Receive_Repair_QualityOut();

                    repairsout.MdiParent = this;

                    repairsout.Parent = this.splitContainer1.Panel2;
                    repairsout.WindowState = FormWindowState.Maximized;
                    repairsout.Show();

                    break;
                /////////////////////////////////////////////
                case "营业维修废料类别":
                    foreach (Form frm in this.splitContainer1.Panel2.Controls)
                    {
                        if (frm is LY_Receive_Repair_Wastestyle)
                        {



                            frm.WindowState = FormWindowState.Normal;

                            frm.Activate();
                            return;
                        }
                    }
                    LY_Receive_Repair_Wastestyle repairswaste = new LY_Receive_Repair_Wastestyle();

                    repairswaste.MdiParent = this;

                    repairswaste.Parent = this.splitContainer1.Panel2;
                    repairswaste.WindowState = FormWindowState.Normal;
                  
                    repairswaste.Show();

                    break;
                ///////////////////////////////////////////
                case "营业维修领退料":
                    foreach (Form frm in this.splitContainer1.Panel2.Controls)
                    {
                        if (frm is LY_Store_Out_Reapair)
                        {



                            frm.WindowState = FormWindowState.Maximized;

                            frm.Activate();
                            return;
                        }
                    }
                    LY_Store_Out_Reapair yywxll = new LY_Store_Out_Reapair();

                    yywxll.MdiParent = this;

                    yywxll.Parent = this.splitContainer1.Panel2;
                    yywxll.WindowState = FormWindowState.Maximized;
                    yywxll.Show();

                    break;
                ///////////////////////////////////////////
                case "钳装改制领料":
                    foreach (Form frm in this.splitContainer1.Panel2.Controls)
                    {
                        if (frm is LY_Restructuring_Benchwork_StoreOut)
                        {



                            frm.WindowState = FormWindowState.Maximized;

                            frm.Activate();
                            return;
                        }
                    }
                    LY_Restructuring_Benchwork_StoreOut qzgzll = new LY_Restructuring_Benchwork_StoreOut();

                    qzgzll.MdiParent = this;

                    qzgzll.Parent = this.splitContainer1.Panel2;
                    qzgzll.WindowState = FormWindowState.Maximized;
                    qzgzll.Show();

                    break;
                ///////////////////////////////////////////
                case "电装改制领料":
                    foreach (Form frm in this.splitContainer1.Panel2.Controls)
                    {
                        if (frm is LY_Restructuring_Elechwork_StoreOut)
                        {



                            frm.WindowState = FormWindowState.Maximized;

                            frm.Activate();
                            return;
                        }
                    }
                    LY_Restructuring_Elechwork_StoreOut dzgzll = new LY_Restructuring_Elechwork_StoreOut();

                    dzgzll.MdiParent = this;

                    dzgzll.Parent = this.splitContainer1.Panel2;
                    dzgzll.WindowState = FormWindowState.Maximized;
                    dzgzll.Show();

                    break;
                /////////////////////////////////////////////
                case "统计查询(营业)":
                    foreach (Form frm in this.splitContainer1.Panel2.Controls)
                    {
                        if (frm is LY_Salescontract_SumAnalysis)
                        {



                            frm.WindowState = FormWindowState.Maximized;

                            frm.Activate();
                            return;
                        }
                    }
                    LY_Salescontract_SumAnalysis salescontractAnalysis = new LY_Salescontract_SumAnalysis();

                    salescontractAnalysis.MdiParent = this;

                    salescontractAnalysis.Parent = this.splitContainer1.Panel2;
                    salescontractAnalysis.WindowState = FormWindowState.Maximized;
                    salescontractAnalysis.Show();

                    break;
                /////////////////////////////////////////////
                case "电装任务分配":
                    foreach (Form frm in this.splitContainer1.Panel2.Controls)
                    {
                        if (frm is LY_Plan_AssignTask)
                        {



                            frm.WindowState = FormWindowState.Maximized;

                            frm.Activate();
                            return;
                        }
                    }
                    LY_Plan_AssignTask planassign = new LY_Plan_AssignTask();

                    planassign.MdiParent = this;

                    planassign.Parent = this.splitContainer1.Panel2;
                    planassign.WindowState = FormWindowState.Maximized;
                    planassign.Show();

                    break;

                /////////////////////////////////////////////









                /////////////////////////////////////////////
                case "电装改制任务分配":
                    foreach (Form frm in this.splitContainer1.Panel2.Controls)
                    {
                        if (frm is LY_RestructuringPlan_AssignTask)
                        {



                            frm.WindowState = FormWindowState.Maximized;

                            frm.Activate();
                            return;
                        }
                    }
                    LY_RestructuringPlan_AssignTask planassign_gz = new LY_RestructuringPlan_AssignTask();

                    planassign_gz.MdiParent = this;

                    planassign_gz.Parent = this.splitContainer1.Panel2;
                    planassign_gz.WindowState = FormWindowState.Maximized;
                    planassign_gz.Show();

                    break;

                /////////////////////////////////////////////




                






                case "钳装任务分配":
                    foreach (Form frm in this.splitContainer1.Panel2.Controls)
                    {
                        if (frm is LY_Plan_AssignTask_Machine)
                        {



                            frm.WindowState = FormWindowState.Maximized;

                            frm.Activate();
                            return;
                        }
                    }
                    LY_Plan_AssignTask_Machine planassignMachine = new LY_Plan_AssignTask_Machine();

                    planassignMachine.MdiParent = this;

                    planassignMachine.Parent = this.splitContainer1.Panel2;
                    planassignMachine.WindowState = FormWindowState.Maximized;
                    planassignMachine.Show();

                    break;
          
                case "钳装改制任务分配":
                    foreach (Form frm in this.splitContainer1.Panel2.Controls)
                    {
                        if (frm is LY_RestructuringPlan_AssignTask_Machine)
                        {



                            frm.WindowState = FormWindowState.Maximized;

                            frm.Activate();
                            return;
                        }
                    }
                    LY_RestructuringPlan_AssignTask_Machine planassignMachine_gz = new LY_RestructuringPlan_AssignTask_Machine();

                    planassignMachine_gz.MdiParent = this;
                                    
                    planassignMachine_gz.Parent = this.splitContainer1.Panel2;
                    planassignMachine_gz.WindowState = FormWindowState.Maximized;
                    planassignMachine_gz.Show();

                    break;
                /////////////////////////////////////////////

                case "DashBoard":
                    foreach (Form frm in this.splitContainer1.Panel2.Controls)
                    {
                        if (frm is LY_Runing_DashBoard)
                        {



                            frm.WindowState = FormWindowState.Maximized;

                            frm.Activate();
                            return;
                        }
                    }
                    LY_Runing_DashBoard dashBoard = new LY_Runing_DashBoard();

                    dashBoard.MdiParent = this;

                    dashBoard.Parent = this.splitContainer1.Panel2;
                    dashBoard.WindowState = FormWindowState.Maximized;
                    dashBoard.Show();

                    break;
                ////////




                case "发货数量统计":
                    foreach (Form frm in this.splitContainer1.Panel2.Controls)
                    {
                        if (frm is LY_SalseContract_StandardReport)
                        {



                            frm.WindowState = FormWindowState.Maximized;

                            frm.Activate();
                            return;
                        }
                    }
                    LY_SalseContract_StandardReport salescount = new LY_SalseContract_StandardReport();

                    salescount.MdiParent = this;

                    salescount.Parent = this.splitContainer1.Panel2;
                    salescount.WindowState = FormWindowState.Maximized;
                    salescount.Show();

                    break;



                case "营业销售成本统计":
                case "销售成本统计":
                    foreach (Form frm in this.splitContainer1.Panel2.Controls)
                    {
                        if (frm is LY_cost_goods)
                        {



                            frm.WindowState = FormWindowState.Maximized;

                            frm.Activate();
                            return;
                        }
                    }
                    LY_cost_goods cgs = new LY_cost_goods();

                    cgs.MdiParent = this;

                    cgs.Parent = this.splitContainer1.Panel2;
                    cgs.WindowState = FormWindowState.Maximized;
                    cgs.Show();

                    break;
                case "LLL新增物料":
                    foreach (Form frm in this.splitContainer1.Panel2.Controls)
                    {
                        if (frm is LY_Newitems_LLL)
                        {



                            frm.WindowState = FormWindowState.Maximized;

                            frm.Activate();
                            return;
                        }
                    }
                    LY_Newitems_LLL newLLL = new LY_Newitems_LLL();

                    newLLL.MdiParent = this;

                    newLLL.Parent = this.splitContainer1.Panel2;
                    newLLL.WindowState = FormWindowState.Maximized;
                    newLLL.Show();

                    break;


                case "DNI报表":
                    foreach (Form frm in this.splitContainer1.Panel2.Controls)
                    {
                        if (frm is Ly_DNINEW)
                        { 
                            frm.WindowState = FormWindowState.Maximized;

                            frm.Activate();
                            return;
                        }
                    }
                    //LY_DNI DNI = new LY_DNI();
                    Ly_DNINEW DNI = new Ly_DNINEW();
                    DNI.MdiParent = this;

                    DNI.Parent = this.splitContainer1.Panel2;
                    DNI.WindowState = FormWindowState.Maximized;
                    DNI.Show();

                    break;

                case "DNI月份查询":
                    foreach (Form frm in this.splitContainer1.Panel2.Controls)
                    {
                        if (frm is Ly_DNINEW_SD)
                        {
                            frm.WindowState = FormWindowState.Maximized;

                            frm.Activate();
                            return;
                        }
                    }
                  
                    Ly_DNINEW_SD DNISD = new Ly_DNINEW_SD();
                    DNISD.MdiParent = this;
                  
                    DNISD.Parent = this.splitContainer1.Panel2;
                    DNISD.WindowState = FormWindowState.Maximized;
                    DNISD.Show();

                    break;


                case "营业客户销售统计":
                    foreach (Form frm in this.splitContainer1.Panel2.Controls)
                    {
                        if (frm is LY_client_Rep)
                        {
                            
                            frm.WindowState = FormWindowState.Maximized;
                            frm.Activate();
                            return;
                        }
                    }
                    LY_client_Rep repClient= new LY_client_Rep();

                    repClient.MdiParent = this;

                    repClient.Parent = this.splitContainer1.Panel2;
                    repClient.WindowState = FormWindowState.Maximized;
                    repClient.Show();

                    break;





                ////////
                case "研发项目分配":
                    foreach (Form frm in this.splitContainer1.Panel2.Controls)
                    {
                        if (frm is LY_SalseContract_StandardReport_Tec)
                        {



                            frm.WindowState = FormWindowState.Maximized;

                            frm.Activate();
                            return;
                        }
                    }
                    LY_SalseContract_StandardReport_Tec salescounttec = new LY_SalseContract_StandardReport_Tec();

                    salescounttec.MdiParent = this;

                    salescounttec.Parent = this.splitContainer1.Panel2;
                    salescounttec.WindowState = FormWindowState.Maximized;
                    salescounttec.Show();

                    break;
                ////////研发项目分配

                case "营业合同财务":
                    foreach (Form frm in this.splitContainer1.Panel2.Controls)
                    {
                        if (frm is LY_SalseContract_StandardReport_financial)
                        {



                            frm.WindowState = FormWindowState.Maximized;

                            frm.Activate();
                            return;
                        }
                    }
                    LY_SalseContract_StandardReport_financial salescount_finan = new LY_SalseContract_StandardReport_financial();

                    salescount_finan.MdiParent = this;

                    salescount_finan.Parent = this.splitContainer1.Panel2;
                    salescount_finan.WindowState = FormWindowState.Maximized;
                    salescount_finan.Show();

                    break;
                /////////////////////////////////////////////F
                /////////////////////////////////////////////
                case "销售金额统计":
                    foreach (Form frm in this.splitContainer1.Panel2.Controls)
                    {
                        if (frm is LY_SalseContract_StandardSum)
                        {

                            //aaa

                            frm.WindowState = FormWindowState.Maximized;

                            frm.Activate();
                            return;
                        }
                    }
                    LY_SalseContract_StandardSum salesmoney = new LY_SalseContract_StandardSum();

                    salesmoney.MdiParent = this;

                    salesmoney.Parent = this.splitContainer1.Panel2;
                    salesmoney.WindowState = FormWindowState.Maximized;
                    salesmoney.Show();

                    break;
                /////////////////////////////////////////////统计查询(质检)

                /////////////////////////////////////////////
                case "利润统计":
                    foreach (Form frm in this.splitContainer1.Panel2.Controls)
                    {
                        if (frm is LY_SalseContract_profitSum)
                        {



                            frm.WindowState = FormWindowState.Maximized;

                            frm.Activate();
                            return;
                        }
                    }
                    LY_SalseContract_profitSum salesprofit = new LY_SalseContract_profitSum();

                    salesprofit.MdiParent = this;

                    salesprofit.Parent = this.splitContainer1.Panel2;
                    salesprofit.WindowState = FormWindowState.Maximized;
                    salesprofit.Show();

                    break;
                /////////////////////////////////////////////
                case "利润统计客户":
                    foreach (Form frm in this.splitContainer1.Panel2.Controls)
                    {
                        if (frm is LY_SalseContract_profitSumClient)
                        {



                            frm.WindowState = FormWindowState.Maximized;

                            frm.Activate();
                            return;
                        }
                    }
                    LY_SalseContract_profitSumClient salesprofitC = new LY_SalseContract_profitSumClient();

                    salesprofitC.MdiParent = this;

                    salesprofitC.Parent = this.splitContainer1.Panel2;
                    salesprofitC.WindowState = FormWindowState.Maximized;
                    salesprofitC.Show();

                    break;
                /////////////////////////////////////////////
                case "配套备件设置":
                    foreach (Form frm in this.splitContainer1.Panel2.Controls)
                    {
                        if (frm is LY_SalesMating_Mange)
                        {
                            frm.WindowState = FormWindowState.Normal;
                            frm.Activate();
                            return;
                        }
                    }
                    LY_SalesMating_Mange lsmm = new LY_SalesMating_Mange();

                    //  bm.Parent_model = ((System.Windows.Forms.ToolStripItem)(sender)).OwnerItem.Text;
                    lsmm.MdiParent = this;
                    lsmm.Parent = this.splitContainer1.Panel2;
                    lsmm.WindowState = FormWindowState.Normal;
                    lsmm.StartPosition = FormStartPosition.Manual;
                    lsmm.Location = new Point(100, 100);
                    lsmm.Show();
                    break;
                /////////////////////////////////////////////外修业务
                case "外修业务":
                    foreach (Form frm in this.splitContainer1.Panel2.Controls)
                    {
                        if (frm is LY_Receive_Repair_OUT)
                        {
                            frm.WindowState = FormWindowState.Normal;
                            frm.Activate();
                            return;
                        }
                    }
                    LY_Receive_Repair_OUT repairOut = new LY_Receive_Repair_OUT();

                    //  bm.Parent_model = ((System.Windows.Forms.ToolStripItem)(sender)).OwnerItem.Text;
                    repairOut.MdiParent = this;
                    repairOut.Parent = this.splitContainer1.Panel2;
                    repairOut.WindowState = FormWindowState.Maximized;
                    //repairOut.StartPosition = FormStartPosition.Manual;
                    //repairOut.Location = new Point(100, 100);
                    repairOut.Show();
                    break;
                    ////////////////////////////////////////////////////////////
                case "钳装入库":
                    foreach (Form frm in this.splitContainer1.Panel2.Controls)
                    {
                        if (frm is LY_Store_In_Qianzhuang)
                        {
                            //


                            frm.WindowState = FormWindowState.Maximized;

                            frm.Activate();
                            return;
                        }
                    }
                    LY_Store_In_Qianzhuang QZinstore = new LY_Store_In_Qianzhuang();

                    QZinstore.MdiParent = this;

                    QZinstore.Parent = this.splitContainer1.Panel2;
                    QZinstore.WindowState = FormWindowState.Maximized;
                    QZinstore.Show();

                    break;




                ////////////////////////////////////////////////////////////
                case "钳装改制入库":
                    foreach (Form frm in this.splitContainer1.Panel2.Controls)
                    {
                        if (frm is LY_Restructuring_Store_In_Qianzhuang)
                        {



                            frm.WindowState = FormWindowState.Maximized;

                            frm.Activate();
                            return;
                        }
                    }
                    LY_Restructuring_Store_In_Qianzhuang QZinstore_gz = new LY_Restructuring_Store_In_Qianzhuang();

                    QZinstore_gz.MdiParent = this;

                    QZinstore_gz.Parent = this.splitContainer1.Panel2;
                    QZinstore_gz.WindowState = FormWindowState.Maximized;
                    QZinstore_gz.Show();

                    break;










                /////////////////////////////////////////////营业借用返库
                case "电装入库":
                    foreach (Form frm in this.splitContainer1.Panel2.Controls)
                    {
                        if (frm is LY_Store_In_Dianzhuang)
                        {



                            frm.WindowState = FormWindowState.Maximized;

                            frm.Activate();
                            return;
                        }
                    }
                    LY_Store_In_Dianzhuang DZinstore = new LY_Store_In_Dianzhuang();

                    DZinstore.MdiParent = this;

                    DZinstore.Parent = this.splitContainer1.Panel2;
                    DZinstore.WindowState = FormWindowState.Maximized;
                    DZinstore.Show();

                    break;





                /////////////////////////////////////////////营业借用返库
                case "电装改制入库":
                    foreach (Form frm in this.splitContainer1.Panel2.Controls)
                    {
                        if (frm is LY_Restructuring_Store_In_Dianzhuang)
                        {



                            frm.WindowState = FormWindowState.Maximized;

                            frm.Activate();
                            return;
                        }
                    }
                    LY_Restructuring_Store_In_Dianzhuang DZinstore_gz = new LY_Restructuring_Store_In_Dianzhuang();

                    DZinstore_gz.MdiParent = this;
               
                    DZinstore_gz.Parent = this.splitContainer1.Panel2;
                    DZinstore_gz.WindowState = FormWindowState.Maximized;
                    DZinstore_gz.Show();

                    break;








                /////////////////////////////////////////////营业借用返库
                case "钳装任务提交":
                    foreach (Form frm in this.splitContainer1.Panel2.Controls)
                    {
                        if (frm is LY_ProductTask_SubmitBenchwork)
                        {



                            frm.WindowState = FormWindowState.Maximized;

                            frm.Activate();
                            return;
                        }
                    }
                    LY_ProductTask_SubmitBenchwork QZsubmit = new LY_ProductTask_SubmitBenchwork();

                    QZsubmit.MdiParent = this;

                    QZsubmit.Parent = this.splitContainer1.Panel2;
                    QZsubmit.WindowState = FormWindowState.Maximized;
                    QZsubmit.Show();

                    break;
           



                case "钳装改制任务提交":
                    foreach (Form frm in this.splitContainer1.Panel2.Controls)
                    {
                        if (frm is LY_Restructuring_ProductTask_SubmitBenchwork)
                        {



                            frm.WindowState = FormWindowState.Maximized;

                            frm.Activate();
                            return;
                        }
                    }
                    LY_Restructuring_ProductTask_SubmitBenchwork QZsubmit_gz = new LY_Restructuring_ProductTask_SubmitBenchwork();

                    QZsubmit_gz.MdiParent = this;

                    QZsubmit_gz.Parent = this.splitContainer1.Panel2;
                    QZsubmit_gz.WindowState = FormWindowState.Maximized;
                    QZsubmit_gz.Show();

                    break;
                /////////////////////////////////////////////









                case "电装任务提交":
                    foreach (Form frm in this.splitContainer1.Panel2.Controls)
                    {
                        if (frm is LY_ProductTask_SubmitElecwork)
                        {



                            frm.WindowState = FormWindowState.Maximized;

                            frm.Activate();
                            return;
                        }
                    }
                    LY_ProductTask_SubmitElecwork DZsubmit = new LY_ProductTask_SubmitElecwork();

                    DZsubmit.MdiParent = this;

                    DZsubmit.Parent = this.splitContainer1.Panel2;
                    DZsubmit.WindowState = FormWindowState.Maximized;
                    DZsubmit.Show();

                    break;
            
                case "电装改制任务提交":
                    foreach (Form frm in this.splitContainer1.Panel2.Controls)
                    {
                        if (frm is LY_Restructuring_ProductTask_SubmitElecwork)
                        {
                            


                            frm.WindowState = FormWindowState.Maximized;

                            frm.Activate();
                            return;
                        }
                    }
                    LY_Restructuring_ProductTask_SubmitElecwork DZsubmit_gz = new LY_Restructuring_ProductTask_SubmitElecwork();

                    DZsubmit_gz.MdiParent = this;
                  
                    DZsubmit_gz.Parent = this.splitContainer1.Panel2;
                    DZsubmit_gz.WindowState = FormWindowState.Maximized;
                    DZsubmit_gz.Show();

                    break;

                case "外协退料过检":
                    foreach (Form frm in this.splitContainer1.Panel2.Controls)
                    {
                        if (frm is out_source_approve)
                        {



                            frm.WindowState = FormWindowState.Maximized;

                            frm.Activate();
                            return;
                        }
                    }
                    out_source_approve out_source_approve = new out_source_approve();

                    out_source_approve.MdiParent = this;

                    out_source_approve.Parent = this.splitContainer1.Panel2;
                    out_source_approve.WindowState = FormWindowState.Maximized;
                    out_source_approve.Show();

                    break;


                case "综合查询(钳装)":
                case "综合查询(电装)":
                case "生产计划查询":
                    foreach (Form frm in this.splitContainer1.Panel2.Controls)
                    {
                        if (frm is LY_ProductTask_SumAnalysis)
                        {
                            frm.WindowState = FormWindowState.Normal;
                            frm.Activate();
                            return;
                        }
                    }
                    LY_ProductTask_SumAnalysis protask = new LY_ProductTask_SumAnalysis();

                    //  bm.Parent_model = ((System.Windows.Forms.ToolStripItem)(sender)).OwnerItem.Text;
                    protask.MdiParent = this;
                    protask.Parent = this.splitContainer1.Panel2;
                    protask.WindowState = FormWindowState.Maximized;
                    //repairOut.StartPosition = FormStartPosition.Manual;
                    //repairOut.Location = new Point(100, 100);
                    protask.Show();
                    break;

                /////////////////////////////////////////////外修业务
                case "物料计划查询":
                    foreach (Form frm in this.splitContainer1.Panel2.Controls)
                    {
                        if (frm is LY_MaterialTask_SumAnalysis)
                        {
                            frm.WindowState = FormWindowState.Normal;
                            frm.Activate();
                            return;
                        }
                    }
                    LY_MaterialTask_SumAnalysis materialtask = new LY_MaterialTask_SumAnalysis();

                    //  bm.Parent_model = ((System.Windows.Forms.ToolStripItem)(sender)).OwnerItem.Text;
                    materialtask.MdiParent = this;
                    materialtask.Parent = this.splitContainer1.Panel2;
                    materialtask.WindowState = FormWindowState.Maximized;
                    //repairOut.StartPosition = FormStartPosition.Manual;
                    //repairOut.Location = new Point(100, 100);
                    materialtask.Show();
                    break;

                case "仓库工具管理":
                    foreach (Form frm in this.splitContainer1.Panel2.Controls)
                    {
                        if (frm is store_out_tool)
                        {
                            frm.WindowState = FormWindowState.Normal;
                            frm.Activate();
                            return;
                        }
                    }
                    store_out_tool store_out_tool = new store_out_tool(); 
                    store_out_tool.MdiParent = this;
                    store_out_tool.Parent = this.splitContainer1.Panel2;
                    store_out_tool.WindowState = FormWindowState.Maximized;
                    store_out_tool.Show();
                    break;


                case "工具出库":
                    foreach (Form frm in this.splitContainer1.Panel2.Controls)
                    {
                        if (frm is store_out_tool_ck)
                        {
                            frm.WindowState = FormWindowState.Normal;
                            frm.Activate();
                            return;
                        }
                    }
                    store_out_tool_ck store_out_tool_ck = new store_out_tool_ck();
                    store_out_tool_ck.MdiParent = this;
                    store_out_tool_ck.Parent = this.splitContainer1.Panel2;
                    store_out_tool_ck.WindowState = FormWindowState.Maximized;
                    store_out_tool_ck.Show();
                    break;

                /////////////////////////////////////////////外修业务
                case "计量设备管理":
                    foreach (Form frm in this.splitContainer1.Panel2.Controls)
                    {
                        if (frm is LY_MeteringEquipment_Manage)
                        {
                            frm.WindowState = FormWindowState.Normal;
                            frm.Activate();
                            return;
                        }
                    }
                    LY_MeteringEquipment_Manage lmem = new LY_MeteringEquipment_Manage();

                    //  bm.Parent_model = ((System.Windows.Forms.ToolStripItem)(sender)).OwnerItem.Text;
                    lmem.MdiParent = this;
                    lmem.Parent = this.splitContainer1.Panel2;
                    lmem.WindowState = FormWindowState.Maximized;
                    //repairOut.StartPosition = FormStartPosition.Manual;
                    //repairOut.Location = new Point(100, 100);
                    lmem.Show();
                    break;
                /////////////////////////////////////////////
                case "退出系统":
                    Application.Exit();
                    break;
                default:
                    //Console.WriteLine("Default case");
                    break;



            }
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {

            return;
            SqlConnection Con = new SqlConnection(Program.dataBase.MakeConnectString());
            Con.Open();


            SqlCommand Com = new SqlCommand(" UPDATE T_Users SET  online ='N'  where yhbm = '" + SQLDatabase.NowUserID + "'", Con);


          
            Com.ExecuteNonQuery();

            Con.Close();
            
        }
        clsMCI cm = new clsMCI();
        private void timer1_Tick(object sender, EventArgs e)
        {
            if ("000303" == SQLDatabase.nowUserDepartment())
            {

                if (SQLDatabase.CheckNoaccceptInform())
                {

                    System.Media.SystemSounds.Beep.Play();
                    System.Media.SystemSounds.Exclamation.Play();
                    System.Media.SystemSounds.Asterisk.Play();
                    System.Media.SystemSounds.Hand.Play();
                    System.Media.SystemSounds.Question.Play();


                    this.notifyIcon2.Visible = true;
                    this.notifyIcon1.Visible = false;
                    this.notifyIcon2.ShowBalloonTip(5000, "提示", "有通知发货未收到条目,请查看...", ToolTipIcon.Info);

                     //  clsMCI cm = new clsMCI ();

                       if (File.Exists("国际歌.wma"))
                       {
                           cm.FileName = "国际歌.wma";
                           cm.play();
                       }
                }
            }
            else
            {

                if (SQLDatabase.CheckHaveInform())
                {
                    System.Media.SystemSounds.Beep.Play(); 
                    this.notifyIcon2.Visible = true;
                    this.notifyIcon1.Visible = false;
                    this.notifyIcon2.ShowBalloonTip(5000, "提示", "有新的营业发货业务,请查看...", ToolTipIcon.Info);

                    OpenTask();
                }
            }
        }

        private void notifyIcon2_Click(object sender, EventArgs e)
        {
            this.notifyIcon2.Visible = false;
            this.notifyIcon1.Visible = true;
        }

        private void notifyIcon2_BalloonTipClicked(object sender, EventArgs e)
        {
            this.notifyIcon2.Visible = false;
            this.notifyIcon1.Visible = true;

            OpenTask();

        }

        private void OpenTask()
        {
            if ("000303" == SQLDatabase.nowUserDepartment())
            {

                LY_sales_outNoaccceptInform queryForm = new LY_sales_outNoaccceptInform();

                queryForm.OwnerForm = this;


                queryForm.StartPosition = FormStartPosition.CenterParent;

                //cm.StopT();
                cm.Puase();

                this.timer1.Stop();
                this.notifyIcon2.Visible = false;
                this.notifyIcon1.Visible = true;
                queryForm.ShowDialog(this);
                this.timer1.Start();
            }
            else
            {

                LY_sales_outInform queryForm = new LY_sales_outInform();

                queryForm.OwnerForm = this;


                queryForm.StartPosition = FormStartPosition.CenterParent;

                this.timer1.Stop();
                this.notifyIcon2.Visible = false;
                this.notifyIcon1.Visible = true;
                queryForm.ShowDialog(this);
                this.timer1.Start();
            }
        }

        private void notifyIcon2_BalloonTipClosed(object sender, EventArgs e)
        {
            this.notifyIcon2.Visible = false;
            this.notifyIcon1.Visible = true;
        }

        public void goto_Salescontract_Deliver(string nowplannum, string nowinnercode)
        {

            foreach (Form frm in this.splitContainer1.Panel2.Controls)
            {
                if (frm is LY_Store_Out_Sales)
                {
              

                    frm.WindowState = FormWindowState.Maximized;

                    frm.Activate();

                    ((LY_Store_Out_Sales)frm).Find_planlocation(nowplannum, nowinnercode);
                    return;
                }
            }
            LY_Store_Out_Sales salescontractdeliver = new LY_Store_Out_Sales();

            salescontractdeliver.MdiParent = this;

            salescontractdeliver.Parent = this.splitContainer1.Panel2;
            salescontractdeliver.WindowState = FormWindowState.Maximized;
            salescontractdeliver.Show();

            salescontractdeliver.Find_planlocation(nowplannum, nowinnercode);


        }

        public void goto_Salescontract_Bill(string nowbillnum, string nowcontractcode)
        {

            foreach (Form frm in this.splitContainer1.Panel2.Controls)
            {
                if (frm is LY_Salescontract_Daily)
                {



                    frm.WindowState = FormWindowState.Maximized;

                    frm.Activate();

                    ((LY_Salescontract_Daily)frm).Find_billlocation(nowbillnum, nowcontractcode);

                    return;
                }
            }
            LY_Salescontract_Daily salescontractdaily = new LY_Salescontract_Daily();

            salescontractdaily.MdiParent = this;

            salescontractdaily.Parent = this.splitContainer1.Panel2;
            salescontractdaily.WindowState = FormWindowState.Maximized;
            salescontractdaily.Show();

            
            ///////////////////////////////////////////////////////////////////////
            salescontractdaily.Find_billlocation(nowbillnum, nowcontractcode);


        }

        private void splitContainer1_Panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        //private void button1_Click(object sender, EventArgs e)
        //{
        //    for (int i = 1; i <= 10; i++)
        //    {


        //        System.Media.SystemSounds.Beep.Play();
        //        System.Media.SystemSounds.Exclamation.Play();
        //        System.Media.SystemSounds.Asterisk.Play();
        //        System.Media.SystemSounds.Hand.Play();
        //        System.Media.SystemSounds.Question.Play();
        //    }

        //}


    }
    

}
