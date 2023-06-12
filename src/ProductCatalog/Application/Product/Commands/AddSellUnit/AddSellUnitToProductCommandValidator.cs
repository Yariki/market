using FluentValidation;

namespace Microsoft.Extensions.DependencyInjection.Product.Commands.AddSellUnit;

public class AddSellUnitToProductCommandValidator : AbstractValidator<AddSellUnitToProductCommand>
{
    public AddSellUnitToProductCommandValidator()
    {
        RuleFor(c => c.ProductId)
            .NotEmpty().WithMessage("Product id is required.");
        RuleFor(c => c.SellUnit)
            .NotEmpty().WithMessage("Sell unit is required.");
    }
}