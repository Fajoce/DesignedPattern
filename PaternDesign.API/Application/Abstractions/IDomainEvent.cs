using System.ComponentModel.DataAnnotations;

namespace PaternDesign.API.Application.Abstractions
{
    public class IDomainEvent
    {
        [Key]
        public int Id { get; set; }
        DateTime OccurredOn { get; }
    }
}
