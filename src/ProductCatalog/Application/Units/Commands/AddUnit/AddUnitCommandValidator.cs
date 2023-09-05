using FluentValidation;

namespace ProductCatalog.Application.Units.Commands.AddUnit;

public class AddUnitCommandValidator : AbstractValidator<AddUnitCommand>
{
    public AddUnitCommandValidator()
    {
        RuleFor(x => x.Abbriviation)
            .NotEmpty()
            .MaximumLength(25);
        RuleFor(x => x.Description)
            .MaximumLength(255);
    }
}