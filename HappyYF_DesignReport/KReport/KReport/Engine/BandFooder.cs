namespace KReport.Engine
{
    using System;

    public class BandFooder : BandBase
    {
        public BandFooder()
        {
            base.Name = "页尾";
            base.SetBandType(BandType.BandFooder);
        }
    }
}

