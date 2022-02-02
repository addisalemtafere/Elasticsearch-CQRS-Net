using AutoMapper;
using Domain.Interfaces.Repository;
using MediatR;

namespace Application.Features.Property.Queries.SearchPropertyQuery
{
    public class SearchPropertyQueryHandler : IRequestHandler<SearchPropertyQuery, SearchPropertyQueryResponse>
    {
        private readonly IMapper _mapper;
        private readonly IPropertyRepository _propertyRepository;

        public SearchPropertyQueryHandler(IPropertyRepository propertyRepository,
            IMapper mapper)
        {
            _mapper = mapper;
            _propertyRepository = propertyRepository;
        }

        public async Task<SearchPropertyQueryResponse> Handle(SearchPropertyQuery request,
            CancellationToken cancellationToken)
        {
            var propertyQueryResponse = new SearchPropertyQueryResponse();

            var validator = new SearchPropertyQueryValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (validationResult.Errors.Count > 0)
            {
                propertyQueryResponse.Success = false;
                propertyQueryResponse.ValidationErrors = new List<string>();
                foreach (var error in validationResult.Errors)
                    propertyQueryResponse.ValidationErrors.Add(error.ErrorMessage);
            }

            if (!propertyQueryResponse.Success) return propertyQueryResponse;
            var propertyResponse = await _propertyRepository.GetByTextAsync(request.SearchKey,request.MarketType, request.Limit);

            var locationDto = _mapper.Map<List<SearchProperyDto>>(propertyResponse);

            propertyQueryResponse.SearchProperyDtos = locationDto;

            return propertyQueryResponse;
        }
    }
}