using Domain.Dtos;
using FluentValidation;

namespace Domain.Validators;

public class GenreValidator : AbstractValidator<GenreModel>
{
    public GenreValidator()
    {
        RuleFor(e => e.Name).NotEmpty();
    }
}