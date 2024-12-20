using Order.API.Enums;

namespace Order.API.Entities
{
    public class Order
    {
        public Guid Id { get; set; }
        public Guid BuyerId { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public List<OrderItem> OrderItems { get; set; }
        public decimal OrderAmount {  get; set; }
    }
}
