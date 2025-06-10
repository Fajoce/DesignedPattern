using PaternDesign.API.Domain.DTOs;
using PaternDesign.API.Domain.Entities;

namespace PaternDesign.API.Application.Abstractions
{
    public interface IPricingStrategyFactory
    {
        decimal CalculateFinalPrice(Products product);
    }
}
