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
		public static async Task Run()
		{

            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            IConfigurationRoot configuration = builder.Build();



            var endpoint = configuration.GetSection("CosmosDb:URI").Value;
			var masterKey = configuration.GetSection("CosmosDb:Key").Value;
            var databasename = configuration.GetSection("CosmosDb:DatabaseName").Value;
            var collectionname = configuration.GetSection("CosmosDb:ContainerName").Value;


            var collUri = UriFactory.CreateDocumentCollectionUri(databasename, collectionname);
			var sql = "SELECT * FROM c WHERE c.address.zipCode = '60603'";

			var connectionPolicy = new ConnectionPolicy();
			connectionPolicy.PreferredLocations.Add(configuration.GetSection("SouthBrazilEndpoint").Value);
			connectionPolicy.PreferredLocations.Add(configuration.GetSection("SouthAsiaEndPoint").Value);

			using (var client = new DocumentClient(new Uri(endpoint), masterKey, connectionPolicy))
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
