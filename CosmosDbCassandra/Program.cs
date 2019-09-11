﻿using System;
using System.Net.Security;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;
using Cassandra;
using Cassandra.Mapping;

namespace CosmosDbCassandra
{
    class Program
    {
        private const string UserName = "cassandracosmosdb";
        private const string Password = "W8bOw3pTzPKAL3B2x7VwU6dFEFtOp35B3K2M0v6TmNdyyV84OeiD98VZ7HIQqV2nUbQacP0PXt6ey5AWOslrgg==";
        private const string CassandraContactPoint = "cassandracosmosdb.cassandra.cosmos.azure.com";  // DnsName  
        private static int CassandraPort = 10350;

        public static void Main(string[] args)
        {
            // Connect to cassandra cluster  (Cassandra API on Azure Cosmos DB supports only TLSv1.2)
            var options = new Cassandra.SSLOptions(SslProtocols.Tls12, true, ValidateServerCertificate);
            options.SetHostNameResolver((ipAddress) => CassandraContactPoint);
            Cluster cluster = Cluster.Builder().WithCredentials(UserName, Password).WithPort(CassandraPort).AddContactPoint(CassandraContactPoint).WithSSL(options).Build();


            //Cluster cluster = Cluster.Builder().WithConnectionString("AccountEndpoint=https://mydatabasegraph.documents.azure.com:443/;AccountKey=sQfe98WUdRPkN4PSYNDArSjPO4sGpBTFEy9S44CSkdjtO1vD3yLgvIRqyAOHqDRXbTrVMDATTmyV7ir5VZ9WTg==;ApiKind=Gremlin;").WithPort(CassandraPort).AddContactPoint(CassandraContactPoint).WithSSL(options).Build();
            ISession session = cluster.Connect();

            // Creating KeySpace and table
            session.Execute("DROP KEYSPACE IF EXISTS uprofile");
            session.Execute("CREATE KEYSPACE uprofile WITH REPLICATION = { 'class' : 'NetworkTopologyStrategy', 'datacenter1' : 1 };");
            Console.WriteLine(String.Format("created keyspace uprofile"));
            session.Execute("CREATE TABLE IF NOT EXISTS uprofile.user (user_id int PRIMARY KEY, user_name text, user_bcity text)");
            Console.WriteLine(String.Format("created table user"));

            session = cluster.Connect("uprofile");
            IMapper mapper = new Mapper(session);

            // Inserting Data into user table
            mapper.Insert<User>(new User(1, "LyubovK", "Dubai"));
            mapper.Insert<User>(new User(2, "JiriK", "Toronto"));
            mapper.Insert<User>(new User(3, "IvanH", "Mumbai"));
            mapper.Insert<User>(new User(4, "LiliyaB", "Seattle"));
            mapper.Insert<User>(new User(5, "JindrichH", "Buenos Aires", "Edu"));
            Console.WriteLine("Inserted data into user table");

            Console.WriteLine("Select ALL");
            Console.WriteLine("-------------------------------");
            foreach (User user in mapper.Fetch<User>("Select * from user"))
            {
                Console.WriteLine(user);
            }

            Console.WriteLine("Getting by id 3");
            Console.WriteLine("-------------------------------");
            User userId3 = mapper.FirstOrDefault<User>("Select * from user where user_id = ?", 3);
            Console.WriteLine(userId3);

            // Clean up of Table and KeySpace
            session.Execute("DROP table user");
            session.Execute("DROP KEYSPACE uprofile");

            // Wait for enter key before exiting  
            Console.ReadLine();
        }

        public static bool ValidateServerCertificate(
            object sender,
            X509Certificate certificate,
            X509Chain chain,
            SslPolicyErrors sslPolicyErrors)
        {
            if (sslPolicyErrors == SslPolicyErrors.None)
                return true;

            Console.WriteLine("Certificate error: {0}", sslPolicyErrors);
            // Do not allow this client to communicate with unauthenticated servers.
            return false;
        }
    }
}