using MassTransit;
using Order.API.Data;
using SharedMicroservice.Events;

namespace Order.API.Consumers
{
    public class PaymentCanceledEventConsumer : IConsumer<PaymentCanceledEvent>
    {
        private readonly OrderDbContext _dbContext;
        public PaymentCanceledEventConsumer(OrderDbContext orderDb)
        {
            _dbContext = orderDb;
        }
        public async Task Consume(ConsumeContext<PaymentCanceledEvent> context)
        {
            Entities.Order order = (await _dbContext.Order.FindAsync(context.Message.OrderId))!;
            order.OrderStatus = Enums.OrderStatus.Fail;
            await _dbContext.SaveChangesAsync();
        }
    }
}
