using Domain.Dtos;
using FluentValidation;

namespace Domain.Validators;

public class AuthorValidator : AbstractValidator<AuthorModel>
{
    public AuthorValidator()
    {
        RuleFor(x => x.Name).NotEmpty();
    }
}