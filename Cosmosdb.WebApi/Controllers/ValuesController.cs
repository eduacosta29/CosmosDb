using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cosmosdb.WebApi.Infrastructure.Repository;
using Cosmosdb.WebApi.Model;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Cosmosdb.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {

        private readonly IRepository _repository;
        private IMediator _mediator;
        public ValuesController(IRepository repository, IMediator mediator)
        {
            this._repository = repository;
            this._mediator = mediator;
        }

        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return this._repository.DataBaseQuery().ToList();
        }

        [HttpGet]
        [Route("families")]
        [ProducesResponseType(typeof(IEnumerable<Family>), 200)]
        public ActionResult<IEnumerable<Family>> Families()
        {
            return this._repository.GetList<Family>().ToList();
        }

        [HttpPost]
        [ProducesResponseType( 200)]
        public async Task<IActionResult> AddFamily([FromBody]Features.AddFamily.FamilyModelCommand familyModel) =>  await this._mediator.Send(familyModel).ConfigureAwait(false);
        

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        //// POST api/values
        //[HttpPost]
        //public void Post([FromBody] string value)
        //{
        //}

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
