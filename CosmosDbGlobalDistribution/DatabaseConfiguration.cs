using System;
using System.Collections.Generic;
using System.Text;

namespace CosmosDbGlobalDistribution
{
    public class DatabaseConfiguration
    {

        public CosmosDb CosmosDb { get; set; }

        public string SouthBrazilEndpoint { get; set; }

        public string SouthAsiaEndPoint { get; set; }

        public static string MyDataBase = "MyDataBase";

        public static string MyCollection1 = "MyCollection1";

        public static string MyCollection2 = "MyCollection2";

    }

    public class CosmosDb
    {

        public string URI { get; set; }
        public string Key { get; set; }

        public string DatabaseName { get; set; }
        public string ContainerName { get; set; }

    }


}
