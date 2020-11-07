namespace KReport.Engine
{
    using System;

    public class BandDetalhe : BandBase
    {
        public BandDetalhe()
        {
            base.Name = "细节区";
            base.SetBandType(BandType.BandDetail);
        }
    }
}

