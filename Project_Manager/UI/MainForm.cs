using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Project_Manager.AppServices;
using Project_Manager.UI;

namespace Project_Manager
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            notifyIcon.Icon = this.Icon;
            notifyIcon.Text = this.Text;
            notifyIcon.Visible = true;

            Core.CoreData[CoreDataType.ApplicationForm] = this;

            AppSet appSet = AppSet.Load();

            this.Text = appSet.CompanyName;
           
        }

        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void inputToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OptionsForm BI = new OptionsForm();
            BI.ShowDialog ();

        }

        private void testToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OracleTest OT = new OracleTest();
            OT.ShowDialog();
        }

       

       
    }
}
