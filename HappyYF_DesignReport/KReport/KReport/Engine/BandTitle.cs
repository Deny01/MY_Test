namespace KReport.Engine
{
    using System;

    public class BandTitle : BandBase
    {
        public BandTitle()
        {
            base.Name = "表头";
            base.SetBandType(BandType.BandTitle);
        }
    }
}

