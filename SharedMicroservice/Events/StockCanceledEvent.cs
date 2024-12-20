namespace SharedMicroservice.Events
{
    public class StockCanceledEvent
    {
        public Guid OrderId { get; set; }
    }
}
