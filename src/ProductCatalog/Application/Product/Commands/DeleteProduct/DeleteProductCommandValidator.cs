using FluentValidation;

namespace Microsoft.Extensions.DependencyInjection.Product.Commands.DeleteProduct;

public class DeleteProductCommandValidator : AbstractValidator<DeleteProductCommand>
{
    public DeleteProductCommandValidator()
    {
        RuleFor(c => c.ProductId)
            .NotEmpty().WithMessage("Product id is required.");
    }
}