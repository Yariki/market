using FluentValidation;

namespace ProductCatalog.Application.Unit.Commands.AddUnit;

public class AddUnitCommandValidator : AbstractValidator<AddUnitCommand>
{
    public AddUnitCommandValidator()
    {
        RuleFor(x => x.Abbriviation)
            .NotEmpty()
            .MaximumLength(25);
        RuleFor(x => x.Description)
            .Empty()
            .MaximumLength(255);
    }
}