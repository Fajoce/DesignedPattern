using MediatR;
using PaternDesign.API.Domain.Entities;

namespace PaternDesign.API.Application.Handlers
{
    public class ProductCreatedEvent : INotification
    {
        public Products Product { get; }
        public DateTime OccurredOn { get; }

        public ProductCreatedEvent(Products product)
        {
            Product = product;
            OccurredOn = DateTime.UtcNow;
        }
    }
}
