using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace ProductCatalog.Application.Catalog.Commands.UpdateCatalog;
public  class UpdateCatalogCommandValidator : AbstractValidator<UpdateCatalogCommand>
{
    public UpdateCatalogCommandValidator()
    {
        RuleFor(c => c.Id)
            .NotEmpty();
        RuleFor(c => c.Id)
            .Custom((id, context) =>
            {
                if (id == Guid.Empty)
                {
                    context.AddFailure("Id is empty");
                }
            });    
        RuleFor(c => c.Name)
            .MaximumLength(256)
            .NotEmpty();
    }
}
