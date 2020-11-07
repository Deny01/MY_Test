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
    public partial class CheckKeywordsell : Form
    {
        public CheckKeywordsell()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {



            string myconn = SQLDatabase.Connectstring;
            SqlConnection connection = new SqlConnection(myconn);

       



                if (CheckClient(myconn, connection))
                {
                    this.DialogResult = DialogResult.OK;
                }

            
        }

        private bool CheckClient(string myconn, SqlConnection connection)
        {
            SqlCommand Com = new SqlCommand("select * from T_users where yhbm='" +
                             SQLDatabase.NowUserID + "' and Pwd='" +
                             this.textBox1.Text + "' ", connection);

            connection.Open();
            SqlDataReader dr = Com.ExecuteReader();


            dr.Read();
            if (dr.HasRows)
            {
              

           

                connection.Close();
                dr.Close();

                return true;




            }
            else
            {
                MessageBox.Show(" 密码不正确！");

                connection.Close();
                dr.Close();
                return false;
            }


        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar.ToString() == "\r")
            {
                e.Handled = true;


                string myconn = SQLDatabase.Connectstring;
                SqlConnection connection = new SqlConnection(myconn);





                if (CheckClient(myconn, connection))
                {
                    this.DialogResult = DialogResult.OK;
                }


            }

        }


    }
}
