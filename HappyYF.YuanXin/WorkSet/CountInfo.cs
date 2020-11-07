using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HappyYF.YuanXin.WorkSet
{
    public static class CountInfo
    {
        static string client_code;

        public static string Client_code
        {
            get { return CountInfo.client_code; }
            set { CountInfo.client_code = value; }
        }


        static string vehicle_code;

        public static string Vehicle_code
        {
            get { return CountInfo.vehicle_code; }
            set { CountInfo.vehicle_code = value; }
        }



    }
}
