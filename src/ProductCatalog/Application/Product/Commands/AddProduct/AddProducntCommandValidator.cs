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
            .NotEqual(Guid.Empty).WithMessage("Unit is required.");
        
        RuleFor(x => x.PricePerUnit)
            .GreaterThan(0).WithMessage("Price per unit must be greater than 0.");
        
        RuleFor(x => x.AvailableStock)
            .GreaterThan(0).WithMessage("Available stock must be greater than 0.");

        RuleFor(p => p.Description)
            .NotEmpty()
            .WithMessage($"{nameof(Domain.Product.Product.Description)} is required");
        
        RuleFor(p => p.PictureFilename)
            .NotEmpty()
            .WithMessage($"{nameof(Domain.Product.Product.PictureFilename)} is required");
        RuleFor(p => p.PictureUri)
            .NotEmpty()
            .WithMessage($"{nameof(Domain.Product.Product.PictureUri)} is required");
    }
}