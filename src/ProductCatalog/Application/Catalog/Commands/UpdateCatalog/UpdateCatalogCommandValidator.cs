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
        RuleFor(c => c.Name)
            .MaximumLength(256)
            .NotEmpty();
    }
}
