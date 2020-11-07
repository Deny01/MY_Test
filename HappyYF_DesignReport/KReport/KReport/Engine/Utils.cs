namespace KReport.Engine
{
    using MakeSoft.Tools;
    using System;

    public abstract class Utils
    {
        protected Utils()
        {
        }

        public static double ConvertPixelToDisplay(int pixel)
        {
            return FunctionsGraphics.ConvertPixelToDisplay(pixel);
        }
    }
}

