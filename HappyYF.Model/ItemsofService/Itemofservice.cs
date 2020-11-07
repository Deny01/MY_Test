using System;
using System.Collections.Generic;
using System.Text;
using HappyYF.Infrastructure.DomainBase;


namespace HappyYF.Model.ItemsofService
{
    public class Itemofservice : EntityBase
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
        private decimal unit_price;

        public decimal Unit_price
        {
            get { return unit_price; }
            set { unit_price = value; }
        }
        private string remarks;

        public string Remarks
        {
            get { return remarks; }
            set { remarks = value; }
        }

         public Itemofservice()
            : this(null)
        {
        }

         public Itemofservice(string number) 
        {
            this.number = number;
            this.name = null;
            this.unit_price = 0;
            this.remarks = null;

         }
    }
}
