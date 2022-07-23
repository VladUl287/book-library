using Domain.Dtos;
using FluentValidation;

namespace Domain.Validators;

public class BookCreateValidatior : AbstractValidator<BookCreate>
{
    public BookCreateValidatior()
    {
        RuleFor(x => x.Name)
            .NotEmpty();

        RuleFor(x => x.Description)
            .NotEmpty()
            .MinimumLength(10);

        RuleFor(x => x.Year)
            .GreaterThan(0);

        RuleFor(x => x.PagesCount)
            .GreaterThan(0);

        RuleFor(x => x.ImageFile)
            .NotNull();

        RuleFor(x => x.Genres)
            .Must(x => x is not null && x.Any());

        RuleFor(x => x.Authors)
            .Must(x => x is not null && x.Any());
    }
}