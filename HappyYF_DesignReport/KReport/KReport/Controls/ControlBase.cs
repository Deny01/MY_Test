namespace KReport.Controls
{
    using System.Windows.Forms;

    public class ControlBase : Control
    {
        ControlBox_Select cs;

        public ControlBox_Select Cs
        {
            get { return cs; }
            set { cs = value; }
        }
        public ControlBase()
         {
             cs = new ControlBox_Select(this);
         
         }
    }
}

