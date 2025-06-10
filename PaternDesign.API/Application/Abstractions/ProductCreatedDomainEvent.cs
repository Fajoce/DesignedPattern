using PaternDesign.API.Domain.Entities;

namespace PaternDesign.API.Application.Abstractions
{
    public class ProductCreatedDomainEvent : IDomainEvent
    {
        public Products Product { get; }
        public DateTime OccurredOn { get; }

        public ProductCreatedDomainEvent(Products product)
        {
            Product = product;
            OccurredOn = DateTime.UtcNow;
        }
    }
}
