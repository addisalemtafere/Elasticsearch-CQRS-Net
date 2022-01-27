using FluentValidation;
using MediatR;

namespace Application.Features.Property.Queries.GetAllPropertyQuery
{
    public class GetAllPropertyQuery : IRequest<GetAllPropertyQueryResponse>
    {
    }

    public class GetAllPropertyQueryValidator : AbstractValidator<GetAllPropertyQuery>
    {
    }
}