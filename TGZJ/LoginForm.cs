using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using TGZJ.Base;
using System.Management;
using System.Transactions;
using HappyYF.Infrastructure.Repositories;


namespace TGZJ
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();

            this.Height =270;

            this.splitContainer1.Panel1Collapsed = false;
            this.splitContainer1.Panel2Collapsed = true;
            
        }

        private void button1_Click(object sender, EventArgs e)
        {

           
            
            string myconn = "Data Source=" + this.textBoxSQLSERVER.Text +
                          ";Initial Catalog=" +this .textDatabaseName.Text +";Persist Security Info=True;User ID=" + this.textBoxSQLuser.Text +
                          ";Password=" + this.textBoxSQLPW.Text + ";Max Pool Size=150;Connection Timeout=300";
            SqlConnection connection = new SqlConnection(myconn);

            if (ConnectSQL(connection))
            {

               


                if (CheckClient(myconn, connection))
                {
                    this.DialogResult = DialogResult.OK;
                }

            }

  
        }

        private bool  MakeYYPara( string yyZT)
        {
            //if (Program.haveYY)
            //{


            //    //string yyDatabaseName = "ufdata_" + yyZT.Substring(1, 3) + "_" + this.dateTimePicker1.Text.Substring(0, 4);
            //    string yyDatabaseName = "UFDATA_002_2008";
            //    string yyconn = "Data Source=" + this.textBoxSQLSERVER.Text +
            //          ";Initial Catalog= " + yyDatabaseName + ";Persist Security Info=True;User ID=" + this.textBoxSQLuser.Text +
            //          ";Password=" + this.textBoxSQLPW.Text + ";Connection Timeout=30";

            //    SqlConnection connection = new SqlConnection(yyconn);

            //    try
            //    {
            //        connection.Open();

            //        Program.yyconnectstring = yyconn;
            //        Program.YYDataBaseName = yyDatabaseName;

            //        return true;

            //    }
            //    catch (SqlException ee)
            //    {
            //        MessageBox.Show(" 连接用友帐套失败，请选择存在的帐套");

            //        return false;

            //    }
            //    finally
            //    {

            //        connection.Close();
            //        connection.Dispose();
            //    }

                
            //}

            return true;
        }

        private string  CheckYY( )
        {
            //string yyconn = "Data Source=" + this.textBoxSQLSERVER.Text +
            //          ";Initial Catalog=UFSystem;Persist Security Info=True;User ID=" + this.textBoxSQLuser.Text +
            //          ";Password=" + this.textBoxSQLPW.Text + ";Connection Timeout=30";

            //SqlConnection connection = new SqlConnection(yyconn);

            //SqlCommand Com = new SqlCommand("select cacc_id,cacc_name from ua_account ", connection);


            //try
            //{
            //    connection.Open();

            //    SqlDataReader dr = Com.ExecuteReader();

            //    //dr.Read();
            //    if (dr.HasRows)
            //    {

            //        while (dr.Read())
            //        {
                       
            //            this.comboBoxYYZT.Items.Add( "[" + dr [0].ToString () +"]" + dr [1].ToString () );
            //        }

                    
            //        dr.Close();
            //        return "OK";


            //    }
            //    else
            //    {

            //        dr.Close();
            //        return "无用友帐套";
            //    }

            //}
            //catch (SqlException ee)
            //{

            //    return "无用友软件" ;
            //}
            //finally
            //{

            //    connection.Close();
               
            
            //}

            return "aaa";
            
            
            
        }

        private bool  CheckClient(string myconn, SqlConnection connection)
        {
            SqlCommand Com = new SqlCommand("select * from T_users where yhbm='" +
                             this.textBoxuserID.Text + "' and Pwd='" +
                             this.textBoxuserPW.Text + "' ", connection);

            connection.Open();
            SqlDataReader dr = Com.ExecuteReader();


            dr.Read();
            if (dr.HasRows)
            {
                Program .dataBase .ServerName = this.textBoxSQLSERVER.Text;
                Program .dataBase .DataBaseName  = this.textDatabaseName.Text;
                Program.dataBase.UserName = this.textBoxSQLuser.Text;
                Program.dataBase.PassWord = this.textBoxSQLPW.Text;

                Program.nowUser.Name=DataHelper .ToDBC ( this.textBoxuserID.Text);

                SQLDatabase.Connectstring = Program.dataBase.MakeConnectString();
                SQLDatabase.NowUserID = Program.nowUser.Name;
                
                
                SetConfig();

                connection.Close();
                dr.Close();

                //return true;

                //if (CheckAuthorization(connection))
                //{
                //    if (CheckOnline(connection, Program.nowUser.Name))
                //        return true;
                //    else
                //        return false;
                //}
                //else
                //    return false;


                return true;
               
                

            }
            else
            {
                MessageBox.Show(" 用户名或密码不正确！");

                connection.Close();
                dr.Close();
                return false;
            }

            
        }

        private void SetConfig()
        {
            Simple3Des jm = new Simple3Des("deny01.12345678901234567890zb");

            Mydataconn mc = Mydataconn.Load();

            if (null == mc)
            {
                mc = new Mydataconn();
            }
           
            mc.Sqlser = this.textBoxSQLSERVER.Text;
            mc.Databs = this.textDatabaseName.Text;
            mc.SqlserUser = this.textBoxSQLuser.Text;
            mc.ServerKey = jm.EncryptData(this.textBoxSQLPW.Text);

            mc.UserID =DataHelper .ToDBC ( this.textBoxuserID.Text);
            mc.Passwd = jm.EncryptData(this.textBoxuserPW.Text);

            
           

            mc.Save();
        }

        private bool  ConnectSQL(SqlConnection connection)
        {
            


            try
            {
                connection.Open();

                return true;

            }
            catch (SqlException ee)
            {
                MessageBox.Show(" 连接服务器失败，请设置服务器连接参数" + ee.Message.ToString());

                this.splitContainer1.Panel1Collapsed = true;
                this.splitContainer1.Panel2Collapsed = false;

                return false;

            }
            finally
            {

                connection.Close();
                //connection.Dispose();
            }
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
            //Program.connectstring = "Hello Deny...";
            Mydataconn mc = Mydataconn.Load();

            if (null != mc)
            {

                Simple3Des jm = new Simple3Des("deny01.12345678901234567890zb");

                this.textBoxSQLSERVER.Text = mc.Sqlser;
                this .textDatabaseName .Text = mc.Databs ;
                this.textBoxSQLuser.Text = mc.SqlserUser;
                this.textBoxSQLPW.Text = jm.DecryptData(mc.ServerKey);

                this.textBoxuserID.Text = mc.UserID;

                //this.textBoxuserID.Select(0, 0);

                this.textBoxuserPW.Focus();
               //this.textBoxuserPW.Text = jm.DecryptData(mc.Passwd);
            }

          
            
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (e.KeyChar.ToString() == "\r")
            {
                this.textBoxuserPW.Focus();
            }
        }

        private void textBox2_ImeModeChanged(object sender, EventArgs e)
        {
            //MessageBox.Show(this.textBox2.ImeMode.ToString());
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.splitContainer1.Panel1Collapsed = true;
            this.splitContainer1.Panel2Collapsed = false;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.splitContainer1.Panel1Collapsed = false ;
            this.splitContainer1.Panel2Collapsed = true ;

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            string myconn = "Data Source=" + this.textBoxSQLSERVER.Text +
                          ";Initial Catalog=" + this.textDatabaseName.Text  + ";Persist Security Info=True;User ID=" + this.textBoxSQLuser.Text +
                          ";Password=" + this.textBoxSQLPW.Text + ";Connection Timeout=30";
            SqlConnection connection = new SqlConnection(myconn);

            
            if (ConnectSQL(connection))
            {
                MessageBox.Show(" 连接服务器成功，现在可以登录系统 ");
                this.splitContainer1.Panel1Collapsed = false;
                this.splitContainer1.Panel2Collapsed = true;
            }

            connection.Close();
        }

        public static string GetCPU_ID()
        {



            System.Management.ManagementClass mcpu = new ManagementClass("win32_processor");
            ManagementObjectCollection moc = mcpu.GetInstances();

            string nowCPU_ID = "";

            foreach (ManagementObject mo in moc)
            {
                //MessageBox.Show(mo["processorid"].ToString());
                nowCPU_ID = nowCPU_ID + mo["processorid"].ToString();

            }

            return nowCPU_ID;

        }

        public bool CheckOnline(SqlConnection connection, string userId)
        {

            return true;
            SqlCommand Com = new SqlCommand("SELECT isnull(online,'N') as online FROM T_Users  where yhbm = '" + userId + "'", connection);

            connection.Open();
            SqlDataReader dr = Com.ExecuteReader();


            dr.Read();
            if (dr.HasRows)
            {
                if ("N" == dr[0].ToString())
                {
                    connection.Close();
                    dr.Close();

                    Com = new SqlCommand(" UPDATE T_Users SET  online ='Y'  where yhbm = '" + userId + "'", connection);
                    connection.Open();
                   Com.ExecuteNonQuery ();

                   connection.Close();

                    return true;
                }
                else
                {


                  MessageBox.Show(" 账户已在别处登录,不能同时使用... ");
                    connection.Close();
                    dr.Close();
                    return true;
                }
            }
            return false;
        }

        public  bool CheckAuthorization(SqlConnection connection)
        {
            SqlCommand Com = new SqlCommand("select * from T_Authorization where cpu_id = '" + GetCPU_ID() + "'", connection);

            connection.Open();
            SqlDataReader dr = Com.ExecuteReader();

           
            dr.Read();
            if (dr.HasRows)
            {
                if ("True" == dr[3].ToString())
                {
                    connection.Close();
                    dr.Close();
                    return true;
                }
                else
                {


                    MessageBox.Show(" 本机尚未获得授权，请联系管理人员，授权后可以登录系统 ");
                    connection.Close();
                    dr.Close();
                    if (this.textBoxuserID.Text  == "Admin")
                       
                        return true;
                    else
                        return false;
                }
            }
            else
            {
                connection.Close();
                dr.Close();
                
                string insdetail = " INSERT INTO T_Authorization " +
               " ( cpu_id, user_name) " +
               "VALUES ( '" + GetCPU_ID() + "','" + this.textBoxuserID.Text + "')";

                //string myconn = "Data Source=" + this.textBoxSQLSERVER.Text +
                //          ";Initial Catalog=" + this.textDatabaseName.Text + ";Persist Security Info=True;User ID=" + this.textBoxSQLuser.Text +
                //          ";Password=" + this.textBoxSQLPW.Text + ";Connection Timeout=30";
               // SqlConnection connection = new SqlConnection(myconn);




                //SqlConnection sqlConnection1 = new SqlConnection(myconn);
                SqlCommand cmd = new SqlCommand();

                cmd.CommandText = insdetail;
                cmd.CommandType = CommandType.Text;
                //cmd.Connection = sqlConnection1;

                cmd.Connection =connection ;

                using (TransactionScope scope = new TransactionScope())
                {
                    
                    connection.Open();
                    cmd.ExecuteNonQuery();
                    connection.Close();

                    scope.Complete();
                }

                MessageBox.Show(" 本机尚未获得授权，请联系管理人员，授权后可以登录系统 ");
                connection.Close();
                dr.Close();
                if (this.textBoxuserID.Text == "000")
                    return true;
                else
                    return false;
            }



        }

        private void textBoxSQLSERVER_TextChanged(object sender, EventArgs e)
        {

        }
    }
}