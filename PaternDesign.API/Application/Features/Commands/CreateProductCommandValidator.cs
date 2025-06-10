using FluentValidation;
using PaternDesign.API.Domain.DTOs;

namespace PaternDesign.API.Application.Features.Commands
{
    public class CreateProductCommandValidator: AbstractValidator<CreateProductCommand>
    {
        public CreateProductCommandValidator()
        {
            RuleFor(x => x.Product.ProductName)
                .NotEmpty().WithMessage("This field is required");
            RuleFor(x => x.Product.ProductDescription)
                .Length(5, 500).WithMessage("Product description must be between 5 and 500 characters.");
            RuleFor(x => x.Product.ProductPrice)
                .GreaterThan(0).WithMessage("Product price must be greater than 0."); ;
        }
    }
}
