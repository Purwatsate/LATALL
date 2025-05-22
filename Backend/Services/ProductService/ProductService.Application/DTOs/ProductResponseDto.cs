namespace ProductService.Application.DTOs
{
    public class ProductResponseDto
    {
        public string? Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public decimal? DiscountPrice { get; set; }
        public string? Sku { get; set; }
        public int Quantity { get; set; }
        public string? WarehouseId { get; set; }
    }
}
