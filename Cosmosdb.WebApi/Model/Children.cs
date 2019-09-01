using Newtonsoft.Json;

namespace Cosmosdb.WebApi.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class Children : EntityBase
    {

        public Children()
        {
            this.Pets = new List<string>();
        }

        [JsonProperty("firstname")]
        public string FirstName { get; set; }

        [JsonProperty("gender")]
        public string Gender { get; set; }

        [JsonProperty("grade")]
        public int Grade { get; set; }

        [JsonProperty("pets")]
        public List<string> Pets { get; set; }
    }
}
