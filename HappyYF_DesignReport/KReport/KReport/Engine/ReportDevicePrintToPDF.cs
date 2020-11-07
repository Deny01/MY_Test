namespace KReport.Engine
{
    using System;

    public class ReportDevicePrintToPDF : IReportDevice
    {
        protected void DrawBarCode(CustomControl control)
        {
        }

        public void DrawControl(CustomControl control)
        {
            switch (control.Type)
            {
                case ControlType.ControlText:
                    this.DrawText(control);
                    break;

                case ControlType.ControlLine:
                    this.DrawLine(control);
                    break;

                case ControlType.ControlShape:
                    this.DrawShape(control);
                    break;

                case ControlType.ControlImage:
                    this.DrawImage(control);
                    break;

                case ControlType.ControlBarCode:
                    this.DrawBarCode(control);
                    break;

                case ControlType.ControlDBText:
                    this.DrawText(control);
                    break;
            }
        }

        protected void DrawImage(CustomControl control)
        {
        }

        protected void DrawLine(CustomControl control)
        {
        }

        protected void DrawShape(CustomControl control)
        {
        }

        protected void DrawText(CustomControl control)
        {
        }
    }
}

