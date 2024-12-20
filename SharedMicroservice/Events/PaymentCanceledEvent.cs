namespace SharedMicroservice.Events
{
    public class PaymentCanceledEvent
    {
        public Guid OrderId { get; set; }
        public Dictionary<Guid, int> ProductStockTakeBack { get; set; }
    }
}
