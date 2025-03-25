namespace WebShopShared.Models
{
    public class OrderProductDetailsDto
    {
        public required ProductDto Product { get; set; }
        public int Quantity { get; set; }
        public int OrderId { get; set; }
    }
}
