using System.Text.Json.Serialization;

namespace PaternDesign.API.Domain.DTOs
{
    public class CreateProductDTO
    {
        [JsonIgnore]
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public decimal ProductPrice { get; set; } = 0;
    }
}
