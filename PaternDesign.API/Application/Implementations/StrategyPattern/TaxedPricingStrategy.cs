using PaternDesign.API.Application.Abstractions;
using PaternDesign.API.Domain.DTOs;
using PaternDesign.API.Domain.Entities;

namespace PaternDesign.API.Application.Implementations.StrategyPattern
{
    public class TaxedPricingStrategy : IPricingStrategyFactory
    {
        public decimal CalculateFinalPrice(Products product) => product.ProductPrice * 1.21m;
    }
}
