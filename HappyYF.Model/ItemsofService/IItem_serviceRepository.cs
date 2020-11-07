using System;
using System.Collections.Generic;
using HappyYF.Infrastructure.RepositoryFramework;


namespace HappyYF.Model.ItemsofService
{
    public interface IItem_serviceRepository : IRepository<Itemofservice>
    {
        //IList<Item_service> FindBy(IList<MarketSegment> segments, bool completed);
        IList<Itemofservice> FindAll();
        Itemofservice FindBy(string itemNumber);
       

    }
}
