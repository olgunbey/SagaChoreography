namespace SharedMicroservice.MessageContent
{
    public class OrderItemMessage
    {
        public Guid ProductId { get; set; }
        public int Count { get; set; }
    }
}
