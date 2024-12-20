namespace SharedMicroservice.Events
{
    public class PaymentReceivedEvent
    {
        public Guid OrderId { get; set; }
        public Guid BuyerId { get; set; }
    }
}
