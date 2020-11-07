using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Threading;
using Project_Manager.AppServices;

namespace Project_Manager
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        /// 

       //public static  AppSet appSet = AppSet.Load();

        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            if (HaveOtherInstance())   { return; }

            Application.Run(new MainForm());

            


          

           
        }

        private static bool HaveOtherInstance()
        {
            try
            {
                EventWaitHandle globalHandler = EventWaitHandle.OpenExisting("HaveOne");
                globalHandler.Set();
                return true;
            }
            catch (WaitHandleCannotBeOpenedException)
            {
                EventWaitHandle global = new EventWaitHandle(true, EventResetMode.AutoReset, "HaveOne");

                Thread monitor = new Thread(new ThreadStart(delegate()
                {
                    while (true)
                    {
                        global.WaitOne();
                        //MessageBox.Show("I'm here  bbb...");
                        Form form = Core.CoreData[CoreDataType.ApplicationForm] as Form;
                        if (form != null)
                        {
                          //  form.BeginInvoke(new ThreadStart(form.Show));
                            form.WindowState = FormWindowState.Maximized; 

                        }
                    }

                }));
                monitor.IsBackground = true;
                monitor.Start();
                GC.KeepAlive(monitor);

                return false ;
            }
        }
    }
}
