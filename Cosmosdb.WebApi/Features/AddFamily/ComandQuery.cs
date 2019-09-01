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

    public class ComandQuery : IRequestHandler<FamilyModel, IActionResult>
    {
        private IRepository _respository;
        public ComandQuery(IRepository repository)
        {

            this._respository = repository;

        }

        public Task<IActionResult> Handle(FamilyModel request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
