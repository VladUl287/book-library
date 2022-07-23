using Domain.Dtos;
using FluentValidation;

namespace Domain.Validators;

public class CollectionCreateValidator : AbstractValidator<CollectionCreate>
{
    public CollectionCreateValidator()
    {
        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.Description).NotEmpty().MinimumLength(10);
        RuleFor(x => x.Books).Must(x => x is not null && x.Length > 0);
    }
}