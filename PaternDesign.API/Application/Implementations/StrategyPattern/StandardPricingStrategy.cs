using PaternDesign.API.Application.Abstractions;
using PaternDesign.API.Domain.DTOs;
using PaternDesign.API.Domain.Entities;

namespace PaternDesign.API.Application.Implementations.StrategyPattern
{
    public class StandardPricingStrategy : IPricingStrategyFactory
    {
        public decimal CalculateFinalPrice(Products product) => product.ProductPrice;
    }
}
