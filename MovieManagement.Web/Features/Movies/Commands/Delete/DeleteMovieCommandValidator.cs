using FluentValidation;

namespace MovieManagement.Web.Features.Movies.Commands.Delete;

public sealed class DeleteMovieCommandValidator : AbstractValidator<DeleteMovieCommand>
{
    public DeleteMovieCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("{PropertyName} is required.")
            .NotEqual(Guid.Empty)
            .WithMessage("{PropertyName} is required.");
    }
}
