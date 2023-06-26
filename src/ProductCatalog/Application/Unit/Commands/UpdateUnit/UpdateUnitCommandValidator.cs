using FluentValidation;

namespace Microsoft.Extensions.DependencyInjection.Unit.Commands.UpdateUnit;

public class UpdateUnitCommandValidator : AbstractValidator<UpdateUnitCommand>
{
    public UpdateUnitCommandValidator()
    {
        RuleFor(x => x.Abbriviation)
            .NotEmpty()
            .MaximumLength(25);

        RuleFor(x => x.Description)
            .Empty()
            .MaximumLength(255);
    }
}