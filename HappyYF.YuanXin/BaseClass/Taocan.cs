using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace HappyYF.YuanXin.BaseClass
{
    class Taocan
    {
        private  string number;
        private  string name;
        private decimal price;
        private decimal discount;
        private string serviceNumber;
        private string remark;


        public decimal Price
        {
            get { return GetPrice(); }
           
        }

              
        
        public Taocan(): this(null)
        {
           
        }

        public Taocan( string number)
           
        {
            this.number = number;
        }

        public DataTable GetDetailList( string tcNumber)
        {
            throw new System.NotImplementedException();
        }

        public decimal GetPrice()
        {
            return 0;
        }
    }
}
