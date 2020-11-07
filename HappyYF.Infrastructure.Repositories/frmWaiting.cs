using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Threading;


namespace HappyYF.Infrastructure.Repositories
{
    public partial class frmWaiting : Form
    {
        public static frmWaiting _Instance = null;

        private delegate void IncreaseHandle(int nValue);
       // private  IncreaseHandle myIncrease =  new IncreaseHandle(Increase);

       //public static clsMCI cm = null ;
       private static int usetime = 0;
       Thread thdSub= new Thread(new ThreadStart(ThreadFun));
        public frmWaiting()
        {
            InitializeComponent();
            //if ( null ==cm)
            //    cm = new clsMCI();
            //myIncrease = new IncreaseHandle(Increase);
            
        }

        public frmWaiting( string showmess)
        {
            InitializeComponent();
            //if ( null ==cm)
            //    cm = new clsMCI();
            label1.Text = showmess;

        }

        public static void Show(Form owner)
        {
            owner.UseWaitCursor = true;

            owner.Enabled = false;

         
            //if (null == cm)
            //    cm = new clsMCI();

            if (_Instance == null) _Instance = new frmWaiting();

           // _Instance.thdSub = new Thread(new ThreadStart(ThreadFun));
           
           
           


           // _Instance.timer1.Interval = 1000;
          
           
            _Instance.Owner = owner;
            _Instance.Show();
           // _Instance.timer1.Start();
            //if (File.Exists("国际歌.wma"))
            //{
            //    cm.FileName = "国际歌.wma";
            //    cm.play();
            //}


            Application.DoEvents();
        }

        public static void Show(Form owner, string showmess)
        {
            owner.UseWaitCursor = true;

            owner.Enabled = false;
            //if (null == cm)
            //    cm = new clsMCI();

            if (_Instance == null) _Instance = new frmWaiting(showmess);


            _Instance.Owner = owner;
            _Instance.Show();

            //if (File.Exists("国际歌.wma"))
            //{
            //    cm.FileName = "国际歌.wma";
            //    cm.play();
            //}


            Application.DoEvents();
        }

        public static void Show(Form owner, bool disableOwner)
        {            
            owner.UseWaitCursor = true;
            owner.Enabled = !disableOwner;

            if (_Instance == null) _Instance = new frmWaiting();
            _Instance.Owner = owner;
            _Instance.Show(); 

            Application.DoEvents();
        }
       
        public static void Hide(Form owner)
        {
            //cm.StopT();
            
            owner.UseWaitCursor = false;
            owner.Enabled = true;
            
            //_Instance.timer1.Stop();
            if (_Instance != null)
            {
                
                _Instance.Hide();
                _Instance.Dispose(true);
            }
            _Instance =null ;
            
            Application.DoEvents();
        }

        public static void Increase(int nValue)
        {
            //if(nValue > 0)
            //{
            //    if(prcBar.Value + nValue < prcBar.Maximum)
            //    {
            //        prcBar.Value += nValue;
            //        return true;
            //    }
            //    else
            //    {
            //        prcBar.Value = prcBar.Maximum;
            //        this.Close();
            //        return false;
            //    }
            //}
            //return false;

            _Instance.label1.Text = "用时:" + nValue.ToString();
            _Instance.label1.Invalidate();
           
        }



        private static void ThreadFun()
        {
            //usetime = usetime + 1;



            //Thread.Sleep(10);//Sleep　a　while　to　show　window　　

            //_Instance.Invoke(_Instance.myIncrease, new object[] { usetime });

        }
     

        private void timer1_Tick(object sender, EventArgs e)
        {
           
            //_Instance.thdSub.Start();
            //usetime=usetime + 1;
            //label1.Text = "用时:" + usetime.ToString();
            //label1.Invalidate();
        }
    }
}
