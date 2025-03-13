using FluentValidation;

namespace MovieManagement.Web.Features.Movies.Queries.List;

public class ListMoviesQueryValidator : AbstractValidator<ListMoviesQuery>
{
    public ListMoviesQueryValidator()
    {
        RuleFor(x => x.Title)
            .MaximumLength(100);

        RuleFor(x => x.Genre)
            .MaximumLength(100);

        RuleFor(x => x.MinRating)
            .InclusiveBetween(0, 10);

        RuleFor(x => x.MaxRating)
            .InclusiveBetween(0, 10);

        When(x => x.ReleaseDateTo.HasValue, () => {
            RuleFor(x => x.ReleaseDateFrom)
            .LessThan( x => x.ReleaseDateTo);
        }).Otherwise(() => {
            RuleFor(x => x.ReleaseDateFrom)
             .LessThanOrEqualTo(DateTimeOffset.UtcNow);
        });

        RuleFor(x => x.ReleaseDateTo)
            .LessThanOrEqualTo(DateTimeOffset.UtcNow); 

        RuleFor(x => x.Page).GreaterThanOrEqualTo(1);
        RuleFor(x => x.PageSize).GreaterThanOrEqualTo(1);
    }
}
