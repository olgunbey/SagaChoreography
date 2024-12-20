using Order.API.Entities;

namespace Order.API.ViewModels
{
    public class CreateOrderVM
    {
        public List<OrderItem> OrderItem { get; set; }
    }
}
