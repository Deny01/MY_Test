using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace HappyYF.Infrastructure.Repositories
{
    public partial class NewfrmWaiting : Form
    {
        int usetime=0;
        
        public NewfrmWaiting()
        {
            InitializeComponent();
        }

     

        private void timer1_Tick(object sender, EventArgs e)
        {

            usetime = usetime + 1;
            this.label1.Text = "用时:" + usetime.ToString();
        }
        public  void setNotify(string mess)
        {
            this.label2.Text = mess;

        }

        private void NewfrmWaiting_Load(object sender, EventArgs e)
        {
            //this.Parent.Enabled = false;
            //this.ShowInTaskbar = false;
            this.timer1.Interval = 1000;
            this.timer1.Start();
        }

        private void NewfrmWaiting_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.timer1.Stop();
        }
        //////////////////////////////////////////////

        //private frmProgress myProcessBar =null;
        //private delegate bool IncreaseHandle(int nValue);
        //private IncreaseHandle myIncrease =null;

        /////　<　summary>　　///　Open　process　bar　window　　///　<　/summary>　　
        //private void ShowProcessBar()
        //{
        //    myProcessBar = new frmProgress();//　Init　increase　event　　
        //    myIncrease =new IncreaseHandle(myProcessBar.Increase);
        //    myProcessBar.ShowDialog();
        //    myProcessBar =null;
        //}

        ////////////////////////////////////////

        ///　<　summary>　　///　Sub　thread　function　　///　<　/summary>　　
        //private void ThreadFun()
        //{
        //    MethodInvoker mi =new MethodInvoker(ShowProcessBar);
        //    this.BeginInvoke(mi);
        //    Thread.Sleep(1000);//Sleep　a　while　to　show　window　　
        //    bool blnIncreased =false;
        //    object objReturn =null;
        //    do
        //    {
        //        Thread.Sleep(5);
        //        objReturn = this.Invoke(this.myIncrease,new object[] { 2 });
        //        blnIncreased = (bool)objReturn;
        //    }while(blnIncreased);
        //}

        ////

        //打开进度条窗体和增加进度条进度的时候，
        //一个用的是BeginInvoke，一个是Invoke，
        //这里的区别是BeginInvoke不需要等待方法运行完毕，
        //而Invoke是要等待方法运行完毕。还有一点，此处用返回值来判断进度条是否到头了，
        //如果需要有其他的控制，可以类似前面的方法来进行扩展。　　
        //启动线程，
        //可以如下：Thread thdSub = new Thread(new ThreadStart(ThreadFun));thdSub.Start();　

　
        ////////////////////////////////////
    }
}
