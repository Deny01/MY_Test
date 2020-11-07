using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Project_Manager;
using System.Threading;

using TGZJ.DATA;
using TGZJ.Base;

namespace TGZJ
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        /// 

        public static IN_Database  dataBase = new SQLDataBase();
        public static IN_User  nowUser = new User ();
        public static string user;

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            if (HaveOtherInstance()) { return; }

            Application.Run(new  MainForm());
        
        
        
        }

        private static bool HaveOtherInstance()
        {
            try
            {
                EventWaitHandle globalHandler = EventWaitHandle.OpenExisting("HaveTwo");
                globalHandler.Set();
                return true;
            }
            catch (WaitHandleCannotBeOpenedException)
            {
                EventWaitHandle global = new EventWaitHandle(true, EventResetMode.AutoReset, "HaveTwo");

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

                return false;
            }
        }
    }
}
