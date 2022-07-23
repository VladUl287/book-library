using Domain.Dtos;
using FluentValidation;

namespace Domain.Validators;

public class AuthValidator : AbstractValidator<AuthModel>
{
    public AuthValidator()
    {
        RuleFor(p => p.Email).NotEmpty().EmailAddress();
        RuleFor(p => p.Password).NotEmpty().MinimumLength(6);
    }
}