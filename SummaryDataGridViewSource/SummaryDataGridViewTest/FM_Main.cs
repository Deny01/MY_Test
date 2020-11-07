using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SummaryDataGridViewTest
{
    public partial class FM_Main : Form
    {
        public FM_Main()
        {
            InitializeComponent();
            InitializeApp();
            this.btnShow.Click += new EventHandler(btnShow_Click);
        }

        public void InitializeApp()
        {
            //Connect Grid with DataSource
            this.dataGridView.AutoGenerateColumns = true;
            this.dataGridView.DataSource = DataAccess.SampleData.Tables["Orders"];
            this.dataGridView.SummaryColumns = new string[] { "Freight" };
            
            //Show the SummaryBox
            this.dataGridView.SummaryRowVisible = true;            
        }

        private void btnShow_Click(object sender, EventArgs e)
        {
            this.dataGridView.SummaryRowVisible = !dataGridView.SummaryRowVisible;            
        }
    }
}
