using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace HappyYF.YuanXin.WorkSet
{
    
    
    public partial class DatePicker : Form
    {
        string nowDate;
        Point pt;

        public Point Pt
        {
            get { return pt; }
            set { pt = value; }
        }

        public string NowDate
        {
            get { return nowDate; }
            set { nowDate = value; }
        }
        
        public DatePicker()
        {
            InitializeComponent();
           
        }

        private void monthCalendar1_MouseUp(object sender, MouseEventArgs e)
        {
           //nowDate = monthCalendar1.SelectionStart.Date.ToString().Substring (0,10);
            nowDate = monthCalendar1.SelectionStart.Date.ToString("yyyy-MM-dd");

           //this.Close();
        }

        private void DatePicker_Load(object sender, EventArgs e)
        {
          // this.Location = pt;
            if ("" == nowDate)
                this.monthCalendar1.TodayDate = DateTime.Today;
            else
                this.monthCalendar1.TodayDate = DateTime.Parse(nowDate);
            this.monthCalendar1.SelectionStart = this.monthCalendar1.TodayDate;
            this.monthCalendar1.SelectionEnd = this.monthCalendar1.TodayDate;
        }

      
    }
}
