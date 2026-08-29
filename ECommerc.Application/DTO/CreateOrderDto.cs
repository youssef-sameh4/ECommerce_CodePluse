namespace ECommerce.Application.DTO.Orders
{
  

    public class CreateOrderDto
    {
        public int CustomerId { get; set; }
        public List<OrderItemRequestDto> Items { get; set; } = new();
        public string? CouponCode { get; set; }
    }
}
