using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Cosmosdb.WebApi.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class Family : EntityBase
    {
        public Family()
        {
            this.Parents = new List<Parents>();
            this.Childrens = new List<Children>();
        }

        [JsonProperty("id")]
        public string  ID { get; set; }

        [JsonProperty("lastname")]
        public string LastName { get; set; }

        [JsonProperty("parents")]
        public List<Parents> Parents { get; set; }

        [JsonProperty("location")]
        public Location Location { get; set; }


        [JsonProperty("children")]
        public List<Children> Childrens { get; set; }
    }

}
