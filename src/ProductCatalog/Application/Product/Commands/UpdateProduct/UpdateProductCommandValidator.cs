using FluentValidation;

namespace Microsoft.Extensions.DependencyInjection.Product.Commands.UpdateProduct;

public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
{
    public UpdateProductCommandValidator()
    {
        RuleFor(p => p.ProductId)
            .NotEqual(Guid.Empty)
            .WithMessage("ProductId is required.");
        
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
            .WithMessage($"{nameof(ProductCatalog.Domain.Product.Product.Description)} is required");
        
        RuleFor(p => p.PictureFilename)
            .NotEmpty()
            .WithMessage($"{nameof(ProductCatalog.Domain.Product.Product.PictureFilename)} is required");
        RuleFor(p => p.PictureUri)
            .NotEmpty()
            .WithMessage($"{nameof(ProductCatalog.Domain.Product.Product.PictureUri)} is required");
    }    
}