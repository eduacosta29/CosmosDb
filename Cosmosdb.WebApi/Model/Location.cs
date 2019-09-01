using Newtonsoft.Json;

namespace Cosmosdb.WebApi.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class Location : EntityBase
    {
        [JsonProperty("state")]
        public string  State { get; set; }

        [JsonProperty("country")]
        public string Country { get; set; }

        [JsonProperty("city")]
        public string City { get; set; }

      

    }
}
