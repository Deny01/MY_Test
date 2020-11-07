namespace KReport.Engine
{
    using System;

    public class BandGroupHeader : BandBase
    {
        public BandGroupHeader()
        {
            base.Name = "分组头";
            base.SetBandType(BandType.BandGroupHeader);
        }
    }
}

