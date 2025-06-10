namespace PaternDesign.API.Domain.DTOs
{
    public class ProductDTO
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }  // Fixed typo here
        public decimal ProductPrice { get; set; } = 0;

        // Calculated property for IVA (19% of ProductPrice)
        public decimal ProductIVA
        {
            get
            {
                return ProductPrice * 19 / 100;  // Calculates 19% of ProductPrice
            }
        }

        // Calculated property for Total (ProductPrice + 19% VAT)
        public decimal ProductTotal
        {
            get
            {
                return ProductPrice * 1.19m;  // ProductPrice + 19% of ProductPrice
            }
        }

    }
}
