using System.Collections.Generic;
using Microsoft.Azure.Documents;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace AzureFunction
{
    public static class Function1
    {
        [FunctionName("Function1")]
        public static void Run([CosmosDBTrigger(
            databaseName: "MyDataBase",
            collectionName: "MyCollection1",
            ConnectionStringSetting = "AccountEndpoint=https://comosdb.documents.azure.com:443/;AccountKey=9fsQyVpqZ29B5w0bJGujBdmmNanOovZmUUmmNHrYhOmNoTXkFw8GdSBGNjjrSBTDIxmnqQyXa0NWmVawd0ByhA==;",
            LeaseCollectionName = "leases")]IReadOnlyList<Document> input, ILogger log)
        {
            if (input != null && input.Count > 0)
            {
                log.LogInformation("Documents modified " + input.Count);
                log.LogInformation("First document Id " + input[0].Id);
            }
        }
    }
}
