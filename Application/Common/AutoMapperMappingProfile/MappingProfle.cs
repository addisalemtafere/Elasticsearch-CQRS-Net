using Application.Features.Property.Queries.GetAllPropertyQuery;
using Application.Features.Property.Queries.SearchPropertyQuery;
using AutoMapper;
using Domain.Entity;

namespace Application.Common.AutoMapperMappingProfile
{
    public class MappingProfle : Profile
    {
        public MappingProfle()
        {
            CreateMap<Property, PropertyDto>();
            CreateMap<Property, SearchProperyDto>();
        }
    }
}