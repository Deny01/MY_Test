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
    public partial class LY_MaterielRequirementsRecord : Form
    {
        int nowparentId;

        public int NowparentId
        {
            get { return nowparentId; }
            set { nowparentId = value; }
        }
        string nowsortname;

        public string Nowsortname
        {
            get { return nowsortname; }
            set { nowsortname = value; }
        }


        List<string> itemlist = new List<string>();

        public List<string> Itemlist
        {
            get { return itemlist; }
            set { itemlist = value; }
        }


        
        public LY_MaterielRequirementsRecord()
        {
            InitializeComponent();
        }

       

      

        private void LY_MaterielRequirementsRecord_Load(object sender, EventArgs e)
        {

            this.lY_MaterielRequirements_recordTableAdapter.Connection.ConnectionString = SQLDatabase.Connectstring;
            this.lY_MaterielRequirements_recordTableAdapter.Fill(this.lYPlanMange.LY_MaterielRequirements_record, nowparentId, nowsortname);
        }

        private void RecoverPlanStruSingle(string planNum ,string itemnum)
        {
           
          
            NewFrm.Show(this);

            SqlConnection sqlConnection1 = new SqlConnection(SQLDatabase.Connectstring);
            SqlCommand cmd = new SqlCommand();



            cmd.Parameters.Add("@planNum", SqlDbType.VarChar );
            cmd.Parameters["@planNum"].Value = planNum;

            cmd.Parameters.Add("@itemNum", SqlDbType.VarChar);
            cmd.Parameters["@itemNum"].Value = itemnum;


            cmd.CommandText = "LY_Record_Plan_single";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = sqlConnection1;
            cmd.CommandTimeout = 0;

            sqlConnection1.Open();
            cmd.ExecuteNonQuery();
            sqlConnection1.Close();



            //ly_material_plan_explodeTableAdapter.Fill(this.lYPlanMange.ly_material_plan_explode, parentId);
            //this.ly_store_planitemcountTableAdapter.Fill(this.lYPlanMange.ly_store_planitemcount, parentId);
            //this.ly_plan_getmaterial_departmentTableAdapter.Fill(this.lYPlanMange.ly_plan_getmaterial_department, planNum);

            NewFrm.Hide(this);
        }

        private void 恢复ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (null == this.lY_MaterielRequirementsDataGridView.CurrentRow) return;

            string planNum;
            string itemnum;

             string message = "恢复选中记录吗？";
            string caption = "提示...";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;

            DialogResult result;



            result = MessageBox.Show(message, caption, buttons,
            MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

            if (result == DialogResult.Yes)
            {
                this.itemlist.Clear();
                foreach (DataGridViewRow dgr in lY_MaterielRequirementsDataGridView.Rows)
                {
                   

                   if (true == dgr.Selected)
                    {
                       planNum=dgr.Cells["计划编号"].Value .ToString ();
                       itemnum = dgr.Cells["物料编码"].Value .ToString ();

                       this.itemlist.Add(itemnum);

                        RecoverPlanStruSingle(planNum, itemnum);
                    }


                }

                //this.lY_MaterielRequirements_recordTableAdapter.Fill(this.lYPlanMange.LY_MaterielRequirements_record, nowparentId, nowsortname);
            }
            this.Close();
        }
    }
}
