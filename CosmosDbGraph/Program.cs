﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Gremlin.Net.Driver;
using Gremlin.Net.Driver.Exceptions;
using Gremlin.Net.Structure.IO.GraphSON;
using Newtonsoft.Json;

namespace CosmosDbGraph
{
    class Program
    {
        // Azure Cosmos DB Configuration variables
        // Replace the values in these variables to your own.
        private static string hostname = "mydatabasegraph.gremlin.cosmosdb.azure.com";
        private static int port = 443;
        private static string authKey = "sQfe98WUdRPkN4PSYNDArSjPO4sGpBTFEy9S44CSkdjtO1vD3yLgvIRqyAOHqDRXbTrVMDATTmyV7ir5VZ9WTg==";
        private static string database = "graphdb";
        private static string collection = "Persons";

        // Gremlin queries that will be executed.
        private static Dictionary<string, string> gremlinQueries = new Dictionary<string, string>
        {
            //{ "Cleanup",        "g.V().drop()" },
            { "AddVertex 1",    "g.addV('Employee').property('id', 'Alan').property('location', 'NY').property('age', 44)" },
            { "AddVertex 2",    "g.addV('Employee').property('id', 'Pepe').property('location', 'NY').property('age', 39).property('Like', 'pizza')" },
            { "AddVertex 3",    "g.addV('Company').property('id', 'Microsoft').property('location', 'NY').property('founded', '2009')" },
           
            { "AddEdge 1",      "g.V('Alan').addE('Works At').property('weekends', true).to(g.V('Microsoft'))" },
            { "AddEdge 2",      "g.V('Pepe').addE('Works At').to(g.V('Microsoft'))" },
            { "AddEdge 3",      "g.V('Alan').addE('Manage').to(g.V('Pepe'))" },
            { "AddEdge 4",      "g.V('Pepe').addE('Reports To').to(g.V('Alan'))" },

     
        };

        // Starts a console application that executes every Gremlin query in the gremlinQueries dictionary. 
        static void Main(string[] args)
        {
            var gremlinServer = new GremlinServer(hostname, port, enableSsl: true,
                                                    username: "/dbs/" + database + "/colls/" + collection,
                                                    password: authKey);

            using (var gremlinClient = new GremlinClient(gremlinServer, new GraphSON2Reader(), new GraphSON2Writer(), GremlinClient.GraphSON2MimeType))
            {
                foreach (var query in gremlinQueries)
                {
                    Console.WriteLine(String.Format("Running this query: {0}: {1}", query.Key, query.Value));

                    // Create async task to execute the Gremlin query.
                    var resultSet = SubmitRequest(gremlinClient, query).Result;
                    if (resultSet.Count > 0)
                    {
                        Console.WriteLine("\tResult:");
                        foreach (var result in resultSet)
                        {
                            // The vertex results are formed as Dictionaries with a nested dictionary for their properties
                            string output = JsonConvert.SerializeObject(result);
                            Console.WriteLine($"\t{output}");
                        }
                        Console.WriteLine();
                    }

                    
                    PrintStatusAttributes(resultSet.StatusAttributes);
                    Console.WriteLine();
                }
            }

            // Exit program
            Console.WriteLine("Done. Press any key to exit...");
            Console.ReadLine();
        }

        private static Task<ResultSet<dynamic>> SubmitRequest(GremlinClient gremlinClient, KeyValuePair<string, string> query)
        {
            try
            {
                return gremlinClient.SubmitAsync<dynamic>(query.Value);
            }
            catch (ResponseException e)
            {
                Console.WriteLine("\tRequest Error!");

                // Print the Gremlin status code.
                Console.WriteLine($"\tStatusCode: {e.StatusCode}");

               
                PrintStatusAttributes(e.StatusAttributes);
                Console.WriteLine($"\t[\"x-ms-retry-after-ms\"] : { GetValueAsString(e.StatusAttributes, "x-ms-retry-after-ms")}");
                Console.WriteLine($"\t[\"x-ms-activity-id\"] : { GetValueAsString(e.StatusAttributes, "x-ms-activity-id")}");

                throw;
            }
        }

        private static void PrintStatusAttributes(IReadOnlyDictionary<string, object> attributes)
        {
            Console.WriteLine($"\tStatusAttributes:");
            Console.WriteLine($"\t[\"x-ms-status-code\"] : { GetValueAsString(attributes, "x-ms-status-code")}");
            Console.WriteLine($"\t[\"x-ms-total-request-charge\"] : { GetValueAsString(attributes, "x-ms-total-request-charge")}");
        }

        public static string GetValueAsString(IReadOnlyDictionary<string, object> dictionary, string key)
        {
            return JsonConvert.SerializeObject(GetValueOrDefault(dictionary, key));
        }

        public static object GetValueOrDefault(IReadOnlyDictionary<string, object> dictionary, string key)
        {
            if (dictionary.ContainsKey(key))
            {
                return dictionary[key];
            }

            return null;
        }
    }
}
