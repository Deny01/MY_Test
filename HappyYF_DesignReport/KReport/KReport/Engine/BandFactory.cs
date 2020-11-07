namespace KReport.Engine
{
    using System;

    public class BandFactory
    {
        private static BandBase bandfactory;

        public static BandBase CreateInstance(BandType bandtype)
        {
            if (bandtype == BandType.BandDetail)
            {
                bandfactory = new BandDetalhe();
            }
            else if (bandtype == BandType.BandFooder)
            {
                bandfactory = new BandFooder();
            }
            else if (bandtype == BandType.BandHeader)
            {
                bandfactory = new BandHeader();
            }
            else if (bandtype == BandType.BandGroupHeader)
            {
                bandfactory = new BandGroupHeader();
            }
            else if (bandtype == BandType.BandGroupFooder)
            {
                bandfactory = new BandGroupFooder();
            }
            else if (bandtype == BandType.BandTitle)
            {
                bandfactory = new BandTitle();
            }
            else
            {
                if (bandtype != BandType.BandSummary)
                {
                    throw new Exception("Tipo de Band n\x00e3o definido");
                }
                bandfactory = new BandSummary();
            }
            return bandfactory;
        }
    }
}

