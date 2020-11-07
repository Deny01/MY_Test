using System;
using HappyYF.Infrastructure.DomainBase;
using System.Data;

namespace HappyYF.Infrastructure.EntityFactoryFramework
{
    public interface IEntityFactory<T> where T : EntityBase
    {
        T BuildEntity(IDataReader reader);
    }
}
