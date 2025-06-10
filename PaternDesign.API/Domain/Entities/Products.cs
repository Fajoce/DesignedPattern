using PaternDesign.API.Application.Abstractions;
using System.ComponentModel.DataAnnotations;

namespace PaternDesign.API.Domain.Entities
{
    public class Products
    {
        [Key]
        public int ProductId { get; set; }
        [Required(ErrorMessage = "This field is required")]
        [MinLength(5)]        
        public string ProductName { get; set; } = string.Empty;
        public string? ProductDescription { get; set; } = string.Empty;
        [Required]
        public decimal ProductPrice { get; set; }

        private readonly List<IDomainEvent> _domainEvents = new();
        public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents;

        public void AddDomainEvent(IDomainEvent domainEvent)
        {
            _domainEvents.Add(domainEvent);
        }

        public void ClearDomainEvents()
        {
            _domainEvents.Clear();
        }

    }
}
