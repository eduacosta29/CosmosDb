namespace Cosmosdb.WebApi.Features.AddFamily
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using MediatR;
    using Microsoft.AspNetCore.Mvc;

    public class FamilyModel : IRequest<IActionResult>
    {

        
        public string Family { get; set; }

        
        public string LastName { get; set; }
    }
}
