using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.IO;


namespace HappyYF.Infrastructure.Repositories
{
    public class NewFrm:Control  
    {
        public static NewFrm _Instance = null;

         Thread thdSub = null;
        //
         //public static clsMCI cm = null;

         private delegate void IncreaseHandle(string mess);
         private IncreaseHandle myIncrease = null;
        //NewfrmWaiting myProcessBar = null;
        //static  NewfrmWaiting myProcessBar = new NewfrmWaiting();
          NewfrmWaiting myProcessBar;
        public NewFrm()
        {
            myProcessBar = new NewfrmWaiting();
            myIncrease = new IncreaseHandle( myProcessBar.setNotify );
        }

        ~NewFrm()
        {
           // myProcessBar.Dispose();
        }

        public static void Notify(Form owner,string mess)
        {

            //_Instance.myIncrease(mess);
            owner.Invoke(_Instance.myIncrease, new Object[] { mess });
        
        }
    
        public static void Show(Form owner)
        {
            //owner.ShowInTaskbar = false;

            //if (null == cm)
            //    cm = new clsMCI();

            //if (File.Exists("国际歌.wma"))
            //{
            //    cm.FileName = "国际歌.wma";
            //    cm.play();
            //}


            owner.UseWaitCursor = true;

            owner.Enabled = false;

            if (_Instance == null) _Instance = new NewFrm();

            _Instance.thdSub = new Thread(new ThreadStart(_Instance.ThreadFun));

            //Thread.Sleep(1000);

           // _Instance.myProcessBar.Show(owner);
            
            //_Instance.thdSub.IsBackground = true;
           
            _Instance.thdSub.Start();　

        }

        public static void Hide(Form owner)
        {

            //owner.ShowInTaskbar = true ;

            //cm.StopT();

            //if (null != cm)
            //    cm.Puase();


            if (_Instance != null)
                {

                    _Instance.thdSub.Abort();
                    _Instance.thdSub.Join();
                    _Instance.myProcessBar.Dispose();
                    _Instance.Dispose();

                   


                //_Instance.myProcessBar.Hide();
                //_Instance.myProcessBar.Close();

            }

                _Instance = null;

                owner.UseWaitCursor = false;

                owner.Enabled = true;

              //  Application.DoEvents();
           

        }
        private void ThreadFunction()
        {

            //Thread.Sleep(100);
            myProcessBar.ShowDialog(this.Parent);
           



        }
        private void ThreadFun()
        {

            //thread.sleep(500);

            //Thread.Sleep(100);
            _ = myProcessBar.ShowDialog(Parent);

            ////ThreadFunction();

            //myIncrease = new IncreaseHandle(ThreadFunction);

            //_Instance.Invoke(myIncrease);
            //
         

        }
    }
}
