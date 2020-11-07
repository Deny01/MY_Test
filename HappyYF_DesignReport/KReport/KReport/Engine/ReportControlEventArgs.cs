namespace KReport.Engine
{
    using System;

    public class ReportControlEventArgs : EventArgs
    {
        public string ControlName;

        public ReportControlEventArgs(string controlname)
        {
            this.ControlName = controlname;
        }
    }
}

