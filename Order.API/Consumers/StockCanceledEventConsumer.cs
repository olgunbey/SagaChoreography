using MassTransit;
using Order.API.Data;
using SharedMicroservice.Events;

namespace Order.API.Consumers
{
    public class StockCanceledEventConsumer : IConsumer<StockCanceledEvent>
    {
        private readonly OrderDbContext _db;
        public StockCanceledEventConsumer(OrderDbContext orderDbContext)
        {
            _db = orderDbContext;
        }
        public async Task Consume(ConsumeContext<StockCanceledEvent> context)
        { 

            Entities.Order order = (await _db.Order.FindAsync(context.Message.OrderId))!;
            order.OrderStatus = Enums.OrderStatus.Fail;
            await _db.SaveChangesAsync();
        }
    }
}
