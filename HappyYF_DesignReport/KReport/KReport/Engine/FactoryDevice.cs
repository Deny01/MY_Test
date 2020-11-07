namespace KReport.Engine
{
    using System;

    public abstract class FactoryDevice
    {
        protected FactoryDevice()
        {
        }

        public static ReportDevice Instance(ReportTypeDevice typeDevice)
        {
            switch (typeDevice)
            {
                case ReportTypeDevice.Screen:
                    return new ReportDeviceScreen();

                case ReportTypeDevice.Printer:
                    return new ReportDevicePrinter();

                case ReportTypeDevice.File:
                    return new ReportDeviceFile();

                case ReportTypeDevice.DocumentPrint:
                    return new ReportDevicePrintDocument();
            }
            throw new Exception("Dispositivo n\x00e3o identificado");
        }
    }
}

