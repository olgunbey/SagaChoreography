using MassTransit;
using Microsoft.EntityFrameworkCore;
using Order.API.Consumers;
using Order.API.Data;
using Order.API.Enums;
using Order.API.ViewModels;
using SharedMicroservice.Constants;
using SharedMicroservice.Events;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMassTransit<IBus>(y =>
{
    y.AddConsumer<PaymentReceivedEventConsumer>();
    y.AddConsumer<PaymentCanceledEventConsumer>();
    y.AddConsumer<StockCanceledEventConsumer>();
    y.UsingRabbitMq((context, configurator) =>
    {
        configurator.Host(builder.Configuration.GetSection("MassTransitConfiguraton")["Host"], configure =>
        {
            configure.Username(builder.Configuration.GetSection("MassTransitConfiguraton")["Username"]!);
            configure.Password(builder.Configuration.GetSection("MassTransitConfiguraton")["Password"]!);
        });
        configurator.ReceiveEndpoint(QueueNames.Order_Payment_Received_Event_Queue_Name, conf => conf.Consumer<PaymentReceivedEventConsumer>(context));
        configurator.ReceiveEndpoint(QueueNames.Order_Payment_Canceled_Event_Queue_Name, conf => conf.ConfigureConsumer<PaymentCanceledEventConsumer>(context));
        configurator.ReceiveEndpoint(QueueNames.Order_Stock_Canceled_Event_Queue_Name, conf => conf.ConfigureConsumer<StockCanceledEventConsumer>(context));
    });

});
builder.Services.AddDbContext<OrderDbContext>(y => y.UseSqlServer("Server=OLGUNBEY\\SQLEXPRESS; Database=ChoregraphyOrderDb; Trusted_Connection=True; TrustServerCertificate=True;"));


var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();

app.MapPost("create-order", async (CreateOrderVM createOrderMv, OrderDbContext orderDbContext, IBus bus) =>
{

    var order = new Order.API.Entities.Order()
    {
        BuyerId = Guid.NewGuid(),
        OrderItems = createOrderMv.OrderItem.ConvertAll(y => new Order.API.Entities.OrderItem()
        {
            Id = y.Id,
            Amount = y.Amount,
            Count = y.Count,
        }),
        OrderStatus = OrderStatus.Pending,
        OrderAmount = createOrderMv.OrderItem.Sum(y => y.Amount * y.Count)
    };

    orderDbContext.Order.Add(order);
    await orderDbContext.SaveChangesAsync();

    OrderCreatedEvent orderCreatedEvent = new()
    {
        OrderId = order.Id,
        Amount = order.OrderAmount,
        BuyerId = order.BuyerId,
        OrderItemMessages = order.OrderItems.ConvertAll(y => new SharedMicroservice.MessageContent.OrderItemMessage()
        {
            Count = y.Count,
            ProductId = y.Id
        })
    };
    await bus.Send(orderCreatedEvent);
    return Results.Ok("xd");

});
app.Run();
