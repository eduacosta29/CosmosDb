using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CosmosDbGlobalDistribution
{
	public static class TriggersDemo
	{
        public static Uri MyStoreCollectionUri =>
                UriFactory.CreateDocumentCollectionUri(DatabaseConfiguration.MyDataBase, DatabaseConfiguration.MyCollection1);

        public async static Task Run(DatabaseConfiguration databaseConfiguration)
		{
			Debugger.Break();

			

			using (var client = new DocumentClient(new Uri(databaseConfiguration.CosmosDb.URI), databaseConfiguration.CosmosDb.Key))
			{
				
				await CreateDocumentWithValidation(client);
                await ChangeDocument(client);


            }
		}

		

		
		private async static Task<string> CreateDocumentWithValidation(DocumentClient client)
		{
			dynamic documentDefinition = new
			{
				name = "Eduardo",
				address = new { postalCode = "2800" },
				
			};

			var options = new RequestOptions { PreTriggerInclude = new[] { "tgrValidateCategoryField" } };

			try
			{
				var result = await client.CreateDocumentAsync(MyStoreCollectionUri, documentDefinition, options);
				var document = result.Resource;

				Console.WriteLine(" Result:");
				Console.WriteLine($"  Id = {document.id}");
				Console.WriteLine($"  name = {document.name}");
				Console.WriteLine($"  address = {document.address}");
				Console.WriteLine();

				return document._self;
			}
			catch (DocumentClientException ex)
			{
				Console.WriteLine($"Error: {ex.Message}");
				Console.WriteLine();

				return null;
			}
		}

		private async static Task ChangeDocument(DocumentClient client)
		{
            dynamic documentDefinition = new
            {
                name = "Eduardo",
                address = new { postalCode = "2800" },
                partitionKey = "2800",
            };
            var options = new RequestOptions { PostTriggerInclude = new[] { "tgrPost" } };

            var result = await client.CreateDocumentAsync(MyStoreCollectionUri, documentDefinition, options);

		
			var resource = result.Resource;

			Console.WriteLine(" Result:");
            Console.WriteLine($"  Id = {resource.id}");
            Console.WriteLine();
		}

	

		

	
	}
}
