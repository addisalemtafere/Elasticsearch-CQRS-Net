using Application.Common.BaseResponse;

namespace Application.Features.Property.Queries.GetAllPropertyQuery
{
    public class GetAllPropertyQueryResponse : BaseResponse
    {
        public GetAllPropertyQueryResponse() : base()
        {
        }

        public List<PropertyDto> PropertyDtos { get; set; }
    }
}