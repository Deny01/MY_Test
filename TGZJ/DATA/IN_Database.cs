using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TGZJ.DATA
{
    public   interface IN_Database
    {

        string ServerName
        {
            get;
            set;
        }
        
        
        string  DataBaseName
        {
            get;
            set;
        }

        string  PassWord
        {
            get;
            set;
        }

        string  UserName
        {
            get;
            set;
        }
       

       

        string MakeConnectString();

        
    }
}
