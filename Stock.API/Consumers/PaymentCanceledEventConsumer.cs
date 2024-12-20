using MassTransit;
using SharedMicroservice.Events;

namespace Stock.API.Consumers
{
    public class PaymentCanceledEventConsumer : IConsumer<PaymentCanceledEvent>
    {
        private readonly List<ProductStockInfo> _productStockInfos;
        public PaymentCanceledEventConsumer(List<ProductStockInfo> productStockInfos)
        {
            _productStockInfos = productStockInfos;
        }
        public async Task Consume(ConsumeContext<PaymentCanceledEvent> context)
        {
            foreach (var item in context.Message.ProductStockTakeBack)
            {
                var x = _productStockInfos.FirstOrDefault(y => y.ProductId == item.Key);
                x!.Stock += item.Value;

                Console.WriteLine($"Stok Güncellendi{x.Stock}");
            }

        }
    }
}
