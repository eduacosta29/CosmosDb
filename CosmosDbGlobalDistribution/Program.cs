using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using System.Threading.Tasks;

namespace CosmosDbGlobalDistribution
{
    class Program
    {
        static async Task Main(string[] args)
        {


            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            DatabaseConfiguration _databaseConfiguration = new DatabaseConfiguration();

            IConfiguration configuration = builder.Build();
            configuration.GetSection(nameof(DatabaseConfiguration)).Bind(_databaseConfiguration);


            

            var collection = new ServiceCollection();
            collection.AddSingleton(_databaseConfiguration);



            await GlobalDistributionDemo.Run(_databaseConfiguration);
            //await  DatabasesDemo.Run(_databaseConfiguration);
            //await  CollectionsDemo.Run(_databaseConfiguration);
            //await DocumentsDemo.Run(_databaseConfiguration);
            //await StoredProceduresDemo.Run(_databaseConfiguration);
            //await TriggersDemo.Run(_databaseConfiguration);

        }
    }
}
