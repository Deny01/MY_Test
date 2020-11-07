namespace KReport.Engine
{
    using System;

    public class BandGroupFooder : BandBase
    {
        public BandGroupFooder()
        {
            base.Name = "分组尾";
            base.SetBandType(BandType.BandGroupFooder);
        }
    }
}

