


using System;
using System.Collections.Generic;
using HappyYF.Infrastructure.RepositoryFramework;

namespace HappyYF.Model.Dayworks
{
    public interface IDayworkRepository : IRepository<Daywork>
    {
        //IList<Daywork> FindBy(IList<MarketSegment> segments, bool completed);
        IList<Daywork> FindAll();
        Daywork FindBy(DateTime  dayworkDateNumber);
        //IList<MarketSegment> FindAllMarketSegments();
    }
}
