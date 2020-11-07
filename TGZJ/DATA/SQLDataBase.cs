using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TGZJ.DATA
{
    public  class SQLDataBase :IN_Database 
    {


        private string serverName;
        private string dataBaseName;
        private string userName;
        private string passWord;

        public SQLDataBase()
        {
            dataBaseName = null;
            userName = null;
            passWord = null;


        }

        public SQLDataBase(string _dataBaseName, string _userName, string _passWord )
        {
            dataBaseName = _dataBaseName;
            userName = _userName;
            passWord = _passWord;


        }

        public string ServerName
        {
            get
            {
                return serverName;
            }

            set
            {
                serverName = value;
            }
        }
  

       

        public string DataBaseName
        {
            get
            {
                return dataBaseName;
            }

            set
            {
                dataBaseName = value;
            }
        }  

        public string PassWord
        {
            get
            {
                return passWord;
            }

            set
            {
                passWord = value;
            }
        }


        public string UserName
        {
            get
            {
                return userName;
            }

            set
            {
                userName = value;
            }
        }






        #region IN_Database 成员


        public string MakeConnectString()
        {
            string myconn = "Data Source=" + this .serverName +
                          ";Initial Catalog=" + this .dataBaseName +";Persist Security Info=True;User ID=" + this.userName  +
                          ";Password=" + this.passWord  + ";Max Pool Size=150;Connection Timeout=0";

            return myconn;
        
        }

        #endregion
    }
}
