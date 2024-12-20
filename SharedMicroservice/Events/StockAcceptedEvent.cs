namespace SharedMicroservice.Events
{
    public class StockAcceptedEvent
    {
        public Guid BuyerId { get; set; }
        public Guid OrderId { get; set; }
        public Dictionary<Guid, int> ProductStokTakeBack { get; set; }
    }
}
