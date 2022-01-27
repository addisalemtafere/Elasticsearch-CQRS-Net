using AutoMapper;
using Domain.Interfaces.Repository;
using MediatR;

namespace Application.Features.Property.Queries.GetAllPropertyQuery
{
    public class GetAllPropertyQueryHandler : IRequestHandler<GetAllPropertyQuery, GetAllPropertyQueryResponse>
    {
        private readonly IMapper _mapper;
        private readonly IPropertyRepository _propertyRepository;

        public GetAllPropertyQueryHandler(IPropertyRepository propertyRepository,
            IMapper mapper)
        {
            _mapper = mapper;
            _propertyRepository = propertyRepository;
        }

        public async Task<GetAllPropertyQueryResponse> Handle(GetAllPropertyQuery request,
            CancellationToken cancellationToken)
        {
            var propertyQueryResponse = new GetAllPropertyQueryResponse();

            var validator = new GetAllPropertyQueryValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (validationResult.Errors.Count > 0)
            {
                propertyQueryResponse.Success = false;
                propertyQueryResponse.ValidationErrors = new List<string>();
                foreach (var error in validationResult.Errors)
                    propertyQueryResponse.ValidationErrors.Add(error.ErrorMessage);
            }

            if (!propertyQueryResponse.Success) return propertyQueryResponse;
            var propertyResponse = await _propertyRepository.GetAllAsync();

            var locationDto = _mapper.Map<List<PropertyDto>>(propertyResponse);

            propertyQueryResponse.PropertyDtos = locationDto;

            return propertyQueryResponse;
        }
    }
}