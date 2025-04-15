namespace WebApplication1;
using FluentValidation;
using WebApplication1.Data;

public class ProductValidator : AbstractValidator<Product>
{
    public ProductValidator()
    {
        RuleFor(p => p.Name).NotEmpty().WithMessage("Name is required")
            .Length(2, 100).WithMessage("Name must between 2 and 100 chars");
        RuleFor(p =>p.Sku).NotEmpty().WithMessage("Sku is required").Length(3,50)
            .WithMessage("Sku must be between 3 and 50 chars")
            .Matches(@"^[A-Za-z0-9-]+$").WithMessage("SKU can only contain letters, numbers, and hyphens");
    }
}
