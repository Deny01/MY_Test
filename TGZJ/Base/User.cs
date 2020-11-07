using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace TGZJ.Base
{
    public  interface IN_User
    {
        string Name
        {
            get;
            set;
        }
        string Password
        {
            get;
            set;

        }

        string Bumen
        {
            get;
            set;

        }

        string State
        {
            get;
            set;

        }



        bool create_user();

        bool update_user();

        bool isvalid_user();

        string  Load_Bumen();

    }

   public  class User : IN_User
    {

        private string name;
        private string password;
        private string bumen;
        private string state;


        public string Name
        {
            get
            {
                return name;
            }

            set
            {
                name = value;
            }
        }



        public string Password
        {
            get
            {
                return password;
            }

            set
            {
                password = value;
            }
        }


        public string Bumen
        {
            get
            {
                return bumen;
            }

            set
            {
                bumen  = value;
            }
        }


        public string State
        {
            get
            {
                return state ;
            }

            set
            {
                state  = value;
            }
        }


        public bool create_user()
        {
            return true;
        
        
        }


        public bool update_user()
        {
            return true;


        }


        public bool isvalid_user()
        {
            return true;


        }

        public string  Load_Bumen( )
        {

            return "";
        
        }
    
         
    
    }



    public class Now_Client
    {

        private string yhbm;

        public string Yhbm
        {
            get { return yhbm; }
            set { yhbm = value; }
        }
        private string yhmc;

        public string Yhmc
        {
            get { return yhmc; }
            set { yhmc = value; }
        }
        private string bumen;

        public string Bumen
        {
            get { return bumen; }
            set { bumen = value; }
        }
        private string pwd;

        public string Pwd
        {
            get { return pwd; }
            set { pwd = value; }
        }

       

        public Now_Client(string ls_cUid)
        {
            this.yhbm  = ls_cUid;

            string cString = Program.dataBase .MakeConnectString ();
            string selAllString = "SELECT yhbm,yhmc, bumen,pwd FROM T_Users  where yhbm = '" + this .yhbm  + "'";


            SqlDataAdapter yonghuAdapter = new SqlDataAdapter(selAllString, cString);

            DataSet YhData = new DataSet();
            yonghuAdapter.Fill(YhData);

            yhbm  = YhData.Tables[0].Rows[0][0].ToString();
            yhmc = YhData.Tables[0].Rows[0][1].ToString();
            bumen  = YhData.Tables[0].Rows[0][2].ToString();

            pwd  = YhData.Tables[0].Rows[0][3].ToString();



        }

        public bool Update_client()
        {

            string cString = Program.dataBase.MakeConnectString();
            string updString = " UPDATE T_Users SET  yhmc = '" + this.yhmc + "',pwd = '" + this.pwd + "' where yhbm = '" + this.yhbm + "'";

            SqlConnection myConn = new SqlConnection(cString);

            SqlCommand myCom = new SqlCommand(updString, myConn);
            myCom.CommandType = CommandType.Text;

            try
            {
                myCom.Connection.Open();
                myCom.ExecuteNonQuery();
                myConn.Close();


            }
            catch (SqlException e)
            {

                return false;
            }

            return true;


        }
    }
}
