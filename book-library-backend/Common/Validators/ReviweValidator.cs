using Domain.Dtos;
using FluentValidation;

namespace Domain.Validators;

public class ReviweValidator : AbstractValidator<ReviewModel>
{
    public ReviweValidator()
    {
        RuleFor(x => x.Text)
            .NotEmpty()
            .MaximumLength(50);

        RuleFor(x => x.Rating)
            .GreaterThan(0)
            .LessThan(11);
    }
}
