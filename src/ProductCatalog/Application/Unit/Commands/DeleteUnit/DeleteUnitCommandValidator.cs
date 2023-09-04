using FluentValidation;

namespace Microsoft.Extensions.DependencyInjection.Unit.Commands.DeleteUnit;

public class DeleteUnitCommandValidator : AbstractValidator<DeleteUnitCommand>
{
    public DeleteUnitCommandValidator()
    {
        RuleFor(x => x.UnitId)
            .NotEqual(Guid.Empty);
    }
}