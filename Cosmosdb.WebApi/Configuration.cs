namespace Cosmosdb.WebApi
{
    public class Configuration
    {

        public CosmosDb CosmosDb { get; set; }

    }

    public class CosmosDb
    {

        public string URI { get; set; }
        public string Key { get; set; }
        public string DatabaseName { get; set; }
        public string ContainerName { get; set; }
        

    }
}
