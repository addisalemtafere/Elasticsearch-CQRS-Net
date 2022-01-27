using Application.Common.BaseResponse;

namespace Application.Features.Property.Queries.SearchPropertyQuery
{
    public class SearchPropertyQueryResponse : BaseResponse
    {
        public List<SearchProperyDto> SearchProperyDtos { get; set; }

        public SearchPropertyQueryResponse()
        {
            SearchProperyDtos = new List<SearchProperyDto>();
        }
    }
}