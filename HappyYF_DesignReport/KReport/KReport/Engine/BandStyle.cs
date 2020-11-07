namespace KReport.Engine
{
    using System;

    public class BandStyle : BandBase
    {
        public BandStyle()
        {
            base.Name = "Style";
            base.SetBandType(BandType.BandSummary);
        }
    }
}

