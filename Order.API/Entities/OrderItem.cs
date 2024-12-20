namespace Order.API.Entities
{
    public class OrderItem
    {
        public Guid Id { get; set; }
        public decimal Amount { get; set; }
        public int Count { get; set; }
    }
}
