using MassTransit;
using SharedMicroservice.Constants;
using SharedMicroservice.Events;

namespace Stock.API.Consumers
{
    public class OrderCreatedEventConsumer : IConsumer<OrderCreatedEvent>
    {
        private readonly List<ProductStockInfo> _productStockInfos;
        public OrderCreatedEventConsumer(List<ProductStockInfo> productStockInfos)
        {
            _productStockInfos = productStockInfos;
        }
        public async Task Consume(ConsumeContext<OrderCreatedEvent> context)
        {

            List<bool> resuls = new();
            foreach (var item in context.Message.OrderItemMessages)
            {
                resuls.Add(_productStockInfos.Any(y => y.ProductId == item.ProductId && y.Stock >= item.Count));
                _productStockInfos.Single(y => y.ProductId == item.ProductId && y.Stock >= item.Count).Stock -= item.Count;
            }
            foreach (var item in _productStockInfos)
            {
                Console.WriteLine("Stoklar: "+ item.ProductId + " " + item.Stock);
            }

            if (resuls.TrueForAll(y => y.Equals(true)))
            {

                var sendEndPoint = await context.GetSendEndpoint(new Uri($"queue:{QueueNames.Payment_Stock_Accepted_Event_Queue_Name}"));


                await sendEndPoint.Send(new StockAcceptedEvent()
                {
                    OrderId = context.Message.OrderId,
                    BuyerId = context.Message.BuyerId,
                    ProductStokTakeBack = context.Message.OrderItemMessages.ToDictionary(y => y.ProductId, x => x.Count)
                });
            }
            else
            {
                var sendEndPoint = await context.GetSendEndpoint(new Uri($"queue:{QueueNames.Order_Stock_Canceled_Event_Queue_Name}"));
                await sendEndPoint.Send(new StockCanceledEvent()
                {
                    OrderId = context.Message.OrderId,
                });
            }
        }
    }
}
