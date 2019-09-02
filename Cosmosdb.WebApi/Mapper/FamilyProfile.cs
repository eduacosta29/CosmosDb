using Cosmosdb.WebApi.Features.AddFamily;
using Cosmosdb.WebApi.Model;

namespace Cosmosdb.WebApi.Mapper
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using AutoMapper;

    public class FamilyProfile : Profile
    {

        public FamilyProfile()
        {

            CreateMap<Family, FamilyModelCommand>().ReverseMap();

        }

    }
}
