namespace KReport.Engine
{
    using System;

    public class BandHeader : BandBase
    {
        public BandHeader()
        {
            base.Name = "页眉";
            base.SetBandType(BandType.BandHeader);
        }
    }
}

