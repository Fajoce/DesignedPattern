using System.ComponentModel.DataAnnotations;

namespace PaternDesign.API.Domain.DTOs
{
    public class UpdateProductDTO
    {
        public int ProductId { get; set; }
        [Required]
        public string ProductName { get; set; } = string.Empty;
        public string ProductDescription { get; set; } = string.Empty;
        public decimal ProductPrice { get; set; } = 0;
    }
}
