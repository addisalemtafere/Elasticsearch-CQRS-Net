using FluentValidation;
using MediatR;

namespace Application.Features.Property.Queries.SearchPropertyQuery
{
    public class SearchPropertyQuery : IRequest<SearchPropertyQueryResponse>
    {
        public string SearchKey { get; set; }
        public int Limit { get; set; } = 25;
        public List<string> MarketType { get; set; } = new List<string>() { };
    }

    public class SearchPropertyQueryValidator : AbstractValidator<SearchPropertyQuery>
    {
        public SearchPropertyQueryValidator()
        {
            RuleFor(p => p.SearchKey)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull();
        }
    }
}