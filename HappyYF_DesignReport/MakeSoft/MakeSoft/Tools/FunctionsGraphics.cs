namespace MakeSoft.Tools
{
    using System;

    public abstract class FunctionsGraphics
    {
        protected FunctionsGraphics()
        {
        }

        public static float ConvertDisplayToInch(int display)
        {
            return (((float) display) / 100f);
        }

        public static int ConvertDisplayToMilimetro(int display)
        {
            return Convert.ToInt32((double) ((display * 25.4) / 100.0));
        }

        public static int ConvertDisplayToPixel(int display)
        {
            return Convert.ToInt32((double) (0.96 * display));
        }

        public static double ConvertDisplayToPoint(int display)
        {
            return (0.72 * display);
        }

        public static int ConvertInchToDisplay(float inch)
        {
            return (int) ConvertPointToDisplay(ConvertInchToPoint(inch));
        }

        public static int ConvertInchToMilimetro(float inch)
        {
            return ConvertDisplayToMilimetro(ConvertInchToDisplay(inch));
        }

        public static int ConvertInchToPixel(float inch)
        {
            return ConvertDisplayToPixel(ConvertInchToDisplay(inch));
        }

        public static int ConvertInchToPoint(float inch)
        {
            return (int) (72f * inch);
        }

        public static int ConvertMilimetroToDisplay(int milimetre)
        {
            return Convert.ToInt32((double) (((double) (milimetre * 10)) / 2.54));
        }

        public static int ConvertMilimetroToPixel(int milimetre)
        {
            return ConvertDisplayToPixel(ConvertMilimetroToDisplay(milimetre));
        }

        public static double ConvertPixelToDisplay(int pixel)
        {
            return (((double) pixel) / 0.96);
        }

        public static double ConvertPixelToInch(int pixel)
        {
            return (double) ConvertDisplayToInch((int) ConvertPixelToDisplay(pixel));
        }

        public static double ConvertPixelToMilimetro(int pixel)
        {
            return (double) ConvertDisplayToMilimetro((int) ConvertPixelToDisplay(pixel));
        }

        public static double ConvertPixelToPoint(int pixel)
        {
            return ConvertDisplayToPoint((int) ConvertPixelToDisplay(pixel));
        }

        public static double ConvertPointToDisplay(int point)
        {
            return (((double) point) / 0.72);
        }

        public static double ConvertPointToInch(float point)
        {
            return (double) (point / 72f);
        }

        private static void InitializeDevive()
        {
        }
    }
}

