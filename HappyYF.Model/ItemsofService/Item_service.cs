using System;
using System.Collections.Generic;
using HappyYF.Model.ItemsofService;
using HappyYF.Infrastructure.RepositoryFramework;


namespace HappyYF.Model.ItemsofService
{
    class Item_service
    {

        private static IItem_serviceRepository repository;

         static Item_service()
        {
            Item_service.repository = RepositoryFactory.GetRepository<IItem_serviceRepository, Itemofservice>();
        }

         public static IList<Itemofservice> GetAllItemsofService()
        {
            return Item_service.repository.FindAll();
        }

       
    }
}
