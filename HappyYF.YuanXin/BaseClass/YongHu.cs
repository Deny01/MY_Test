using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HappyYF.YuanXin.BaseClass
{
    class YongHu
    {
        private string number;

        public string Number
        {
            get { return number; }
            set { number = value; }
        }
        private string name;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        private string identityCard;

        public string IdentityCard
        {
            get { return identityCard; }
            set { identityCard = value; }
        }
        private string address;

        public string Address
        {
            get { return address; }
            set { address = value; }
        }
        private string phone;

        public string Phone
        {
            get { return phone; }
            set { phone = value; }
        }

        List<Taocan> myTaocan;

        internal List<Taocan> MyTaocan
        {
            get { return myTaocan; }
            set { myTaocan = value; }
        }

        public YongHu():this(null)
        {
           
        }

        public YongHu(string number)
            
        {
            this .number = number ;
        }


    }
}
