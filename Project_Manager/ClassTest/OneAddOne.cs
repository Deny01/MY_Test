using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Project_Manager.ClassTest
{
    class OneAddOne
    {
      public   delegate void ShowResult(int i);
        
        int one;

        public int One
        {
            get { return one; }
            set { one = value;
            ShowResultEvent(one);
            }
        }
        public OneAddOne()
        {
            this.one = 0;
          
        }
        public event ShowResult ShowResultEvent;



    }
}
