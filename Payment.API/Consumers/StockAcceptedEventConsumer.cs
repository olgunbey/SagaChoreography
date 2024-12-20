using MassTransit;
using SharedMicroservice.Constants;
using SharedMicroservice.Events;

namespace Payment.API.Consumers
{
    public class StockAcceptedEventConsumer : IConsumer<StockAcceptedEvent>
    {
        public async Task Consume(ConsumeContext<StockAcceptedEvent> context)
        {
            if (true)
            {
                await context.Send(new Uri($"queue:{QueueNames.Order_Payment_Received_Event_Queue_Name}"), new PaymentReceivedEvent()
                {
                    OrderId = context.Message.OrderId,
                });
            }
            else
            {
                await context.Publish(new PaymentCanceledEvent()
                {
                    OrderId = context.Message.OrderId,
                    ProductStockTakeBack = context.Message.ProductStokTakeBack
                });

            }
        }
    }
}
