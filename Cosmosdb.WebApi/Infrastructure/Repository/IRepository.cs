﻿namespace Cosmosdb.WebApi.Infrastructure.Repository
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Cosmosdb.WebApi.Model;

    public interface IRepository
    {

        IList<T> GetList<T>()
            where T : EntityBase;

        Task<T> Add<T>(T entity) where T : EntityBase;

        Task<T> Update<T>(object id, T entity) where T : EntityBase;

        Task Detele<T>(T entity) where T : EntityBase;

        Task CreateDataBase(string name);

        IEnumerable<string> DataBaseQuery();

        IEnumerable<string> CollectionQuery();

    }
}
