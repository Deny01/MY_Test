using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HappyYF.Model.ItemsofService;

using System.Data;
using System.Data.SqlClient;
using HappyYF.Model;
using HappyYF.Infrastructure.EntityFactoryFramework;

namespace HappyYF.Infrastructure.Repositories
{
    internal class ItemofserviceFactory : IEntityFactory<Itemofservice>
    {

        internal static class FieldNames
        {


            public const string ItemofserviceNumber = "ItemofserviceNumber";
            public const string ItemofserviceName = "ItemofserviceName";
            public const string Unit_price = "Unit_price";
            public const string Remarks = "Remarks";
        }

        public Itemofservice BuildEntity(IDataReader reader)
        {
            Itemofservice itemofservice = new Itemofservice(reader[FieldNames.ItemofserviceNumber].ToString ());

            itemofservice.Name = reader[FieldNames.ItemofserviceName].ToString();
            itemofservice.Unit_price = (decimal)reader[FieldNames.Unit_price];
            itemofservice.Remarks = reader[FieldNames.Remarks].ToString ();

            return itemofservice;
        
        }

    }
}
