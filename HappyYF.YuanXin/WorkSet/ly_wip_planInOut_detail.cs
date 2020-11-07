using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HappyYF.Infrastructure.Repositories;

namespace HappyYF.YuanXin.WorkSet
{
    
    public partial class ly_wip_planInOut_detail : Form
    {
        private string nowplannum;
        public string Nowplannum
         {
             get => nowplannum;
             set => nowplannum = value;
        }
        public ly_wip_planInOut_detail()
        {
            InitializeComponent();
        }

        

        

        private void ly_wip_planInOut_detail_Load(object sender, EventArgs e)
        {
            this.lY_WIP_PlanOutTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            this.lY_WIP_PlanInTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;

            this.lY_WIP_PlanOutTableAdapter.Fill(this.lYStoreMange.LY_WIP_PlanOut, nowplannum);
            this.lY_WIP_PlanInTableAdapter.Fill(this.lYStoreMange.LY_WIP_PlanIn, nowplannum);

        }

     

    }
}
