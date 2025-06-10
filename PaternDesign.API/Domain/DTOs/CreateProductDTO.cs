using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace PaternDesign.API.Domain.DTOs
{
    public class CreateProductDTO
    {
        [JsonIgnore]
        public int ProductID { get; set; }
        [Required]
        public string ProductName { get; set; } = string.Empty;
        public string? ProductDescription { get; set; }
        public decimal ProductPrice { get; set; } = 0;
    }
}
