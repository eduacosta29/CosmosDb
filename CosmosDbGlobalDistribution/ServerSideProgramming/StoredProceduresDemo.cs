using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CosmosDbGlobalDistribution
{
    public static class StoredProceduresDemo
    {
        public static Uri MyStoreCollectionUri =>
            UriFactory.CreateDocumentCollectionUri(DatabaseConfiguration.MyDataBase, DatabaseConfiguration.MyCollection1);

        public async static Task Run(DatabaseConfiguration databaseConfiguration)
        {
            Debugger.Break();

           

            using (var client = new DocumentClient(new Uri(databaseConfiguration.CosmosDb.URI), databaseConfiguration.CosmosDb.Key))
            {

                await ExecuteStoredProcedures(client);

            }
        }



        private static void ViewStoredProcedures(DocumentClient client)
        {
            Console.WriteLine();
            Console.WriteLine(">>> View Stored Procedures <<<");
            Console.WriteLine();

            var sprocs = client
                .CreateStoredProcedureQuery(MyStoreCollectionUri)
                .ToList();

            foreach (var sproc in sprocs)
            {
                Console.WriteLine($"Stored procedure {sproc.Id}; RID: {sproc.ResourceId}");
            }
        }



        private async static Task ExecuteStoredProcedures(DocumentClient client)
        {

            await Execute_spBulkInsert(client);
        }


        private async static Task Execute_spBulkInsert(DocumentClient client)
        {
            Console.WriteLine();
            Console.WriteLine("Execute spBulkInsert");

            var docs = new List<dynamic>();
            var total = 5000;
            for (var i = 1; i <= total; i++)
            {
                dynamic doc = new
                {
                    name = $"Bulk inserted doc {i}",
                    partitionKey = "12345",
                    address = new
                    {
                        postalCode = "12345"
                    }
                };
                docs.Add(doc);
            }


            var uri = UriFactory.CreateStoredProcedureUri(DatabaseConfiguration.MyDataBase, DatabaseConfiguration.MyCollection1, "spBulkInsert");
            var options = new RequestOptions { PartitionKey = new PartitionKey("12345") };

            var totalInserted = 0;
            while (totalInserted < total)
            {
                var result = await client.ExecuteStoredProcedureAsync<int>(uri, options, docs);
                var inserted = result.Response;
                totalInserted += inserted;
                var remaining = total - totalInserted;
                Console.WriteLine($"Inserted {inserted} documents ({totalInserted} total, {remaining} remaining)");
                docs = docs.GetRange(inserted, docs.Count - inserted);
            }


        }

    }
}
