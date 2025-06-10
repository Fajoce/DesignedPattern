namespace PaternDesign.API.Domain.DTOs
{
    public class UpdateProductDTO
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public decimal ProductPrice { get; set; } = 0;
    }
}
