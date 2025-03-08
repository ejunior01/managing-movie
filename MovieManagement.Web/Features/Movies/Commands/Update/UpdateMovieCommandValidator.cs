using FluentValidation;

namespace MovieManagement.Web.Features.Movies.Commands.Update;

public sealed class UpdateMovieCommandValidator : AbstractValidator<UpdateMovieCommand>
{
    public UpdateMovieCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("{PropertyName} is required.")
            .NotEqual(Guid.Empty)
            .WithMessage("{PropertyName} is required.");

        RuleFor(x => x.Title)
            .NotEmpty()
            .WithMessage("{PropertyName} is required.");

        RuleFor(x => x.Genre)
            .NotEmpty()
            .WithMessage("{PropertyName} is required.");

        RuleFor(x => x.ReleaseDate)
            .NotEmpty()
            .WithName("Release Date")
            .WithMessage("{PropertyName} is required.")
            .LessThan(DateTimeOffset.UtcNow)
            .WithName("Release Date")
            .WithMessage("{PropertyName} cannot be in the future.");

        RuleFor(x => x.Rating)
            .NotEmpty()
            .WithMessage("{PropertyName} is required.")
            .InclusiveBetween(0, 10)
            .WithMessage("{PropertyName} must be between 0 and 10.");

    }
}
