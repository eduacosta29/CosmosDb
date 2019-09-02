using AutoMapper;
using Cosmosdb.WebApi.Model;

namespace Cosmosdb.WebApi.Features.AddFamily
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Cosmosdb.WebApi.Infrastructure.Repository;
    using MediatR;
    using Microsoft.AspNetCore.Mvc;

    public class ComandQuery : IRequestHandler<FamilyModelCommand, IActionResult>
    {
        private IRepository _respository;
        private readonly IMapper _mapper;
        public ComandQuery(IRepository repository, IMapper mapper)
        {

            this._respository = repository;
            this._mapper = mapper;

        }

        public async Task<IActionResult> Handle(FamilyModelCommand request, CancellationToken cancellationToken)
        {

            var _family = this._mapper.Map<Family>(request);

            var _datos = await this._respository.Add(_family);

            return  new OkObjectResult(_datos);
        }
    }
}
