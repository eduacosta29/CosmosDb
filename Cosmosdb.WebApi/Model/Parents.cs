namespace Cosmosdb.WebApi.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Newtonsoft.Json;

    public class Parents : EntityBase
    {

        [JsonProperty("firstname")]
        public string FirstName { get; set; }

        [JsonProperty("relationship")]
        public string RelantionShip { get; set; }

    }
}
