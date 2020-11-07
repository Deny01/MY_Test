namespace KReport.Engine
{
    using System;

    public class BandSummary : BandBase
    {
        public BandSummary()
        {
            base.Name = "汇总";
            base.SetBandType(BandType.BandSummary);
        }
    }
}

