using MassTransit;
using Order.API.Data;
using SharedMicroservice.Events;

namespace Order.API.Consumers
{
    public class PaymentReceivedEventConsumer : IConsumer<PaymentReceivedEvent>
    {
        private readonly OrderDbContext _dbContext;
        public PaymentReceivedEventConsumer(OrderDbContext orderDbContext)
        {
            _dbContext = orderDbContext;
        }
        public async Task Consume(ConsumeContext<PaymentReceivedEvent> context)
        {
            Entities.Order order = (await _dbContext.Order.FindAsync(context.Message.OrderId))!;
            order.OrderStatus = Enums.OrderStatus.Completed;
            await _dbContext.SaveChangesAsync();
        }
    }
}
