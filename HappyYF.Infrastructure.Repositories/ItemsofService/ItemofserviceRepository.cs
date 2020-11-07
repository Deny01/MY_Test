using System;
using System.Collections.Generic;
using System.Text;
using HappyYF.Model.ItemsofService;
using HappyYF.Infrastructure.RepositoryFramework;
using HappyYF.Infrastructure.DomainBase;

using System.Data.Common;

using System.Data;
using System.Data.SqlClient ;

using HappyYF.Infrastructure.EntityFactoryFramework;

namespace HappyYF.Infrastructure.Repositories
{
    public class ItemofserviceRepository : SqlServerRepositoryBase<Itemofservice>, IItem_serviceRepository
    {

        private readonly string baseQuery;

         public ItemofserviceRepository()
            : this(null)
        {
        }

         public ItemofserviceRepository(IUnitOfWork unitOfWork) 
            : base(unitOfWork)
        {
            this.baseQuery = "SELECT * FROM Itemofservice ";
        }


         public Itemofservice FindBy(string ItemofserviceNumber)
         {
             StringBuilder builder = new StringBuilder(50);
             builder.Append(this.baseQuery);
             return this.BuildEntityFromSql(builder.Append(string.Format(" WHERE  ItemofserviceNumber = N'{0}'", ItemofserviceNumber)).ToString());
         }

         public IList<Itemofservice> FindAll()
         {
             StringBuilder builder = new StringBuilder(50);
             builder.Append(this.baseQuery);
             builder.Append(";");
             return this.BuildEntitiesFromSql(builder.ToString());
         }

         public override Itemofservice FindBy(object key)
         {
             return new Itemofservice();
         
         }

         protected override void BuildChildCallbacks()
         {
             //this.ChildCallbacks.Add(ItemofserviceFactory.FieldNames.OwnerCompanyId, this.AppendOwner);
             //this.ChildCallbacks.Add(ItemofserviceFactory.FieldNames.ConstructionAdministratorEmployeeId, this.AppendConstructionAdministrator);
             //this.ChildCallbacks.Add(ItemofserviceFactory.FieldNames.PrincipalEmployeeId, this.AppendPrincipal);

         }

         protected override void PersistNewItem(Itemofservice item)
         {
             StringBuilder builder = new StringBuilder(100);
             builder.Append(string.Format("INSERT INTO Itemofservice ({0},{1},{2},{3}) ",
                 ItemofserviceFactory.FieldNames.ItemofserviceNumber ,
                 ItemofserviceFactory.FieldNames.ItemofserviceName ,
                 ItemofserviceFactory.FieldNames.Unit_price ,
                 ItemofserviceFactory.FieldNames.Remarks ));
                 
             builder.Append(string.Format("VALUES ({0},{1},{2},{3});",
                 DataHelper.GetSqlValue(item.Number ),
                 DataHelper.GetSqlValue(item.Name ),
                item.Unit_price.ToString (),
                 DataHelper.GetSqlValue(item.Remarks)));
             this.Database.ExecuteNonQuery(this.Database.GetSqlStringCommand(builder.ToString()));
         }

         protected override void PersistUpdatedItem(Itemofservice item)
         {
             StringBuilder builder = new StringBuilder(100);
             builder.Append("UPDATE Itemofservice SET ");

             builder.Append(string.Format("{0} = {1}",
                 ItemofserviceFactory.FieldNames.ItemofserviceNumber,
                DataHelper.GetSqlValue(item.Number )));

             builder.Append(string.Format(",{0} = {1}",
                  ItemofserviceFactory.FieldNames.ItemofserviceName ,
                 DataHelper.GetSqlValue(item.Name ) ));

             builder.Append(string.Format(",{0} = {1}",
                  ItemofserviceFactory.FieldNames.Unit_price ,
                item.Unit_price .ToString ()));

             builder.Append(string.Format(",{0} = {1}",
                ItemofserviceFactory.FieldNames.Remarks,
                 DataHelper.GetSqlValue(item.Remarks)));

           
             builder.Append(string.Format(" WHERE  ItemofserviceNumber = N'{0}';",item.Number ));

             this.Database.ExecuteNonQuery(this.Database.GetSqlStringCommand(builder.ToString()));
         }

         protected override void PersistDeletedItem(Itemofservice item)
         {


            string  query = "DELETE FROM Itemofservice " +  string.Format(" WHERE  ItemofserviceNumber = N'{0}';",item.Number );
             this.Database.ExecuteNonQuery(this.Database.GetSqlStringCommand(query));
         }

    }
}
