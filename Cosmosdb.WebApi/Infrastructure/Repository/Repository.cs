namespace Cosmosdb.WebApi.Infrastructure.Repository
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Runtime.InteropServices.ComTypes;
    using System.Threading.Tasks;
    using Cosmosdb.WebApi.Model;
    using Microsoft.Azure.Documents;
    using Microsoft.Azure.Documents.Client;

    public class Repository : IRepository
    {

        private readonly IDocumentClient _iDocumentClient;
        private readonly Configuration.Configuration _configuration;
        private Uri _databaseUri;
        private Uri _databaseCollectionUri;
        private Uri _documentUri;


        public Repository(IDocumentClient docoDocumentClient, Configuration.Configuration configuration)
        {

            
            this._iDocumentClient = docoDocumentClient;
            this._configuration = configuration;
            this._databaseUri = UriFactory.CreateDatabaseUri(this._configuration.CosmosDb.DatabaseName);
            this._databaseCollectionUri =
                UriFactory.CreateDocumentCollectionUri(this._configuration.CosmosDb.DatabaseName,
                    this._configuration.CosmosDb.ContainerName);

        }

        public async Task<T> Add<T>(T entity) where T : EntityBase
        {
           var _datos =  await  this._iDocumentClient.CreateDocumentAsync(this._databaseCollectionUri, entity);
           return (T)_datos.Resource;
        }

        public async Task<T> Update<T>(object id, T entity) where T : EntityBase
        {

            var _accesscondition = new AccessCondition(){Type = AccessConditionType.IfMatch , Condition = entity.ETag};

            var document = this._iDocumentClient.CreateDocumentQuery(this._databaseCollectionUri)
                .Where(c => c.Id == id).FirstOrDefault();

            var resource = await this._iDocumentClient.ReplaceDocumentAsync(document.SelfLink, entity, new RequestOptions(){AccessCondition = _accesscondition});

            return (T)resource.Resource;
        }

        public async Task Detele<T>(T entity) where T : EntityBase
        {
            var _accesscondition = new AccessCondition() { Type = AccessConditionType.IfMatch, Condition = entity.ETag };
            var document = this._iDocumentClient.CreateDocumentQuery(this._databaseCollectionUri)
                .Where(c => c.Id == entity.Id).FirstOrDefault();

             await this._iDocumentClient.DeleteDocumentAsync(document.SelfLink, new RequestOptions(){AccessCondition = _accesscondition});

            
        }

        public IEnumerable<string> CollectionQuery()
        {
            var collections = this._iDocumentClient.CreateDocumentCollectionQuery(this._databaseUri);

            foreach (var database in collections)
            {
                yield return database.ResourceId;
                yield return database.Timestamp.ToString();
                yield return database.ETag;
                yield return database.SelfLink;
                yield return database.Id;
            }
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
                yield return database.ResourceId;
                yield return database.Timestamp.ToString();
                yield return database.ETag;
                yield return database.SelfLink;
                yield return database.Id;
            }
        }

        public IList<T> GetList<T>() where T : EntityBase
        {
            
            return this._iDocumentClient.CreateDocumentQuery(this._databaseCollectionUri, new FeedOptions(){EnableCrossPartitionQuery = true}).Select(c => (T)c).ToList();

        }
    }
}
