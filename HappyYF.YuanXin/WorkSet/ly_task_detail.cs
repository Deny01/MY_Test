using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HappyYF.Infrastructure.Repositories;

namespace HappyYF.YuanXin.WorkSet
{
    public partial class ly_task_detail : Form
    {
        public string runmode;
        public string nowplannum;
        public string material_code;

        public ly_task_detail()
        {
            InitializeComponent();
        }

        private void LY_MaterialAdd_Load(object sender, EventArgs e)
        {
            this.ly_material_task_detailBindingSource.Filter = "计划编码='" + nowplannum + "'";
            this.ly_material_task_detailTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            this.ly_material_task_detailTableAdapter.Fill(this.lYPlanMange.ly_material_task_detail, material_code);




          

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton nowrdb = sender as RadioButton;

            if ("全部计划" == nowrdb.Text)
            {
                this.ly_material_task_detailBindingSource.Filter = "";
            }
            else
            {
                this.ly_material_task_detailBindingSource.Filter = "计划编码='" + nowplannum + "'";
            }
        }

       
       
     

       
    }
}
