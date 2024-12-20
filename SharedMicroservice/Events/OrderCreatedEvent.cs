using SharedMicroservice.MessageContent;

namespace SharedMicroservice.Events
{
    public class OrderCreatedEvent
    {
        public Guid OrderId { get; set; }
        public Guid BuyerId { get; set; }
        public List<OrderItemMessage> OrderItemMessages { get; set; }
        public decimal Amount { get; set; }
    }
}
