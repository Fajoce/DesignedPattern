using MediatR;
using PaternDesign.API.Domain.Entities;

namespace PaternDesign.API.Application.Abstractions
{
    public class ProductCreatedEventEvent : IDomainEvent
    {
        public Products Product { get; }
        public DateTime OccurredOn { get; }

        public ProductCreatedEventEvent(Products product)
        {
            Product = product;
            OccurredOn = DateTime.UtcNow;
        }
    }
}
