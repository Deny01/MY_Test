namespace KReport.Engine
{
    using System;
    using System.Drawing.Printing;
    using System.Runtime.CompilerServices;

    public class ReportDevicePrintDocument : ReportDevice
    {
        private PrintDocument printDocument = new PrintDocument();

        //public event DeviceEndPrintEventHandler EndPrint;

        //public event DevicePageGenerateEventHandler PageGenerate;

        //public event DeviceStartPrintEventHandler StartPrint;


       


        public ReportDevicePrintDocument()
        {
            this.printDocument.BeginPrint += new PrintEventHandler(this.printDocumentBegin);
            this.printDocument.EndPrint += new PrintEventHandler(this.printDocumentEnd);
            this.printDocument.PrintPage += new PrintPageEventHandler(this.printDocumentPrintPage);
            base.DefaultPageSettings = this.printDocument.DefaultPageSettings;
        }

        public override int ConvertPixelToDevice(int value)
        {
            return (int) Utils.ConvertPixelToDisplay(value);
        }

        public override void DrawControl(CustomControl control)
        {
        }

        private void printDocumentBegin(object sender, PrintEventArgs e)
        {
            base.OnStartPrint();

            //if (this.StartPrint != null)
            //{
            //    this.StartPrint();
            //}
        }

        private void printDocumentEnd(object sender, PrintEventArgs e)
        {

            base.OnEndPrint();
            //if (this.EndPrint != null)
            //{
            //    this.EndPrint();
            //}
        }

        private void printDocumentPrintPage(object sender, PrintPageEventArgs e)
        {
            base.offset = 0;

            base.OnPageGenerate(e);
            //if (this.PageGenerate != null)
            //{
            //    this.PageGenerate(e.Graphics);
            //}
            e.HasMorePages = base.hasmorepage;
        }

        public override void Show(Report report)
        {
            FormPreview.Show(report);
        }

        public override void Start()
        {
        }

        public PrintDocument Document
        {
            get
            {
                return this.printDocument;
            }
            set
            {
                this.printDocument = value;
            }
        }
    }
}

