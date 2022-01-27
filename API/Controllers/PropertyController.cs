using Application.Features.Property.Queries.GetAllPropertyQuery;
using Application.Features.Property.Queries.SearchPropertyQuery;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PropertyController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PropertyController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("GetAllProperty", Name = "GetAllProperty")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<GetAllPropertyQueryResponse>> GetAllProperty()
        {
            var getAllProperty = new GetAllPropertyQuery();
            var getAllPropertyQueryResponse = await _mediator.Send(getAllProperty);
            return Ok(getAllPropertyQueryResponse);
        }

        [HttpPost("SearchProperty", Name = "SearchProperty")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<GetAllPropertyQueryResponse>> SearchProperty([FromBody] SearchPropertyQuery searchPropertyQuery)
        {
            var searchQueryResponse = await _mediator.Send(searchPropertyQuery);
            return Ok(searchQueryResponse);
        }
    }
}