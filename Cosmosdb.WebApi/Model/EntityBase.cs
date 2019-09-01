using Microsoft.Azure.Documents;

namespace Cosmosdb.WebApi.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;

    public class EntityBase : Document
    {

        [JsonProperty("id")]
        public string ID { get; set; }
    }
}
