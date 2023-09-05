using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace ProductCatalog.Application.Catalogs.Commands.AddCatalog;

public  class AddCatalogCommandValidator : AbstractValidator<AddCatalogCommand>
{
    public AddCatalogCommandValidator()
    {
        RuleFor(a => a.Name)
            .MaximumLength(256)
            .NotEmpty();
    }
}
