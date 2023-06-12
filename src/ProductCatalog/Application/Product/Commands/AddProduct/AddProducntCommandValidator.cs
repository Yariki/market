using FluentValidation;

namespace ProductCatalog.Application.Product.Commands.AddProduct;

public class AddProductCommandValidator : AbstractValidator<AddProductCommand>
{
    public AddProductCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required.")
            .MaximumLength(255);

        RuleFor(x => x.UnitId)
            .NotEmpty().WithMessage("Unit is required.");
        
        RuleFor(x => x.PricePerUnit)
            .GreaterThan(0).WithMessage("Price per unit must be greater than 0.");
        
        RuleFor(x => x.AvailableStock)
            .GreaterThan(0).WithMessage("Available stock must be greater than 0.");
    }
}