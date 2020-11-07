namespace KReport.Engine
{
    using System;
    using System.Drawing.Printing;
    using System.Runtime.CompilerServices;

    public abstract class ReportDevice : IReportDevice
    {
        protected ReportCache cache;
        public PageSettings DefaultPageSettings;
        public bool hasmorepage = true;
        protected int offset;

        public event DeviceEndPrintEventHandler EndPrint;

        public event DevicePageGenerateEventHandler PageGenerate;

        public event DeviceStartPrintEventHandler StartPrint;

        protected virtual void OnEndPrint()
        {
            if (this.StartPrint != null)
            {
                this.StartPrint();
            }
        }

        protected virtual void OnPageGenerate(PrintPageEventArgs e)
        {
       
           
            if (this.PageGenerate != null)
            {
                this.PageGenerate(e.Graphics);
            }
           
        }


        protected virtual void OnStartPrint()
        {
            if (this.StartPrint != null)
            {
                this.StartPrint();
            }
        }

        protected ReportDevice()
        {
        }

        public virtual int ConvertPixelToDevice(int value)
        {
            return 0;
        }

        public virtual void DrawControl(CustomControl control)
        {
        }

        public virtual void GeneretePage()
        {
        }

        public virtual void Show(Report report)
        {
        }

        public virtual void Start()
        {
        }
    }
}

