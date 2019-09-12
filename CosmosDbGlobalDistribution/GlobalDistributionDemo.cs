using Microsoft.Azure.Documents.Client;
using Microsoft.Azure.Documents.Linq;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Azure.Documents;

namespace CosmosDbGlobalDistribution
{
	public static class GlobalDistributionDemo
	{
		public static async Task Run(DatabaseConfiguration databaseConfiguration)
		{


            var collUri = UriFactory.CreateDocumentCollectionUri(databaseConfiguration.CosmosDb.DatabaseName, databaseConfiguration.CosmosDb.ContainerName);
			var sql = "SELECT * FROM c WHERE c.address.zipCode = '60603'";

			var connectionPolicy = new ConnectionPolicy();
			connectionPolicy.PreferredLocations.Add(databaseConfiguration.SouthBrazilEndpoint);
			connectionPolicy.PreferredLocations.Add(databaseConfiguration.SouthAsiaEndPoint);

			using (var client = new DocumentClient(new Uri(databaseConfiguration.CosmosDb.URI), databaseConfiguration.CosmosDb.Key, connectionPolicy))
			{
				for (var i = 0; i < 100; i++)
				{
					var startedAt = DateTime.Now;
					var query = client.CreateDocumentQuery(collUri, sql, new FeedOptions(){EnableCrossPartitionQuery = true}).AsDocumentQuery();
					var result = await query.ExecuteNextAsync();
					Console.WriteLine($"{i + 1}. Elapsed: {DateTime.Now.Subtract(startedAt).TotalMilliseconds} ms; Cost: {result.RequestCharge} RUs");
				}
			}

			Console.ReadKey();
		}

	}
}
