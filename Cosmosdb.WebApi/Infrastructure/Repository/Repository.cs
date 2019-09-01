namespace Cosmosdb.WebApi.Infrastructure.Repository
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Cosmosdb.WebApi.Model;
    using Microsoft.Azure.Documents;

    public class Repository : IRepository
    {

        private readonly IDocumentClient _iDocumentClient;

        public Repository(IDocumentClient docoDocumentClient)
        {
            this._iDocumentClient = docoDocumentClient;
        }

        public Task Add<T>(T entity) where T : EntityBase
        {
            throw new NotImplementedException();
        }

        public async Task CreateDataBase(string name)
        {
            var result = await this._iDocumentClient.CreateDatabaseAsync(new Database() { Id = name });
            var p =  result.Resource;
        }

        public IEnumerable<string> DataBaseQuery()
        {

            var databases = this._iDocumentClient.CreateDatabaseQuery().ToList();

            foreach (var database in databases)
            {
                yield  return  database.ResourceId;
            }
        }

        public Task<IList<T>> GetList<T>() where T : EntityBase
        {
            throw new NotImplementedException();
        }
    }
}
