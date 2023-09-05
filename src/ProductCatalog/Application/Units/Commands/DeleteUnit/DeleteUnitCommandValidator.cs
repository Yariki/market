using FluentValidation;

namespace ProductCatalog.Application.Units.Commands.DeleteUnit;

public class DeleteUnitCommandValidator : AbstractValidator<DeleteUnitCommand>
{
    public DeleteUnitCommandValidator()
    {
        RuleFor(x => x.UnitId)
            .NotEqual(Guid.Empty);
    }
}