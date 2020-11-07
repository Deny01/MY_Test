namespace KReport.Engine
{
    using KReport.Controls;
    using System;
    using System.Text;

    public class ReportDeviceFile : ReportDevice
    {
        private int column;
        private StringBuilder filetext = new StringBuilder();

        public override void DrawControl(CustomControl control)
        {
            if (control is RLabel)
            {
                this.column = this.GetPosition(control.Left);
                this.WriteString(control.Text, this.column);
            }
        }

        public int GetPosition(int poscontrol)
        {
            return Convert.ToInt16(Math.Truncate((double) ((((double) poscontrol) / 2.54) * 10.0)));
        }

        private void WriteLine()
        {
        }

        private void WriteString(string value, int position)
        {
        }
    }
}

