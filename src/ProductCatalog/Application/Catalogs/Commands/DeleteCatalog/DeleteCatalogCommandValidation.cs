using FluentValidation;
using ProductCatalog.Application.Catalogs.Commands.DeleteCatalog;

namespace ProductCatalog.Application;

public class DeleteCatalogCommandValidation : AbstractValidator<DeleteCatalogCommand>
{
    public DeleteCatalogCommandValidation()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Id is required");
        RuleFor(x => x.Id)
            .NotEqual(Guid.Empty)
            .WithMessage("Id is required");
    }
}
