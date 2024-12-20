using MassTransit;
using SharedMicroservice.Constants;
using Stock.API;
using Stock.API.Consumers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton(y => new List<ProductStockInfo>()
{
    new()
    {
        ProductId=Guid.Parse("866f56c7-45d7-400f-b459-6adc9802bc99"),
        Stock=15
    },
    new()
    {
        ProductId=Guid.Parse("8cfe24f8-baff-423e-9208-0281a857bf7e"),
        Stock=25
    },
    new()
    {
        ProductId=Guid.Parse("28084859-cd8e-4a6d-9d67-2c6a46860612"),
        Stock=45
    },
});
builder.Services.AddMassTransit(y =>
{
    y.AddConsumer<OrderCreatedEventConsumer>();
    y.AddConsumer<PaymentCanceledEventConsumer>();
    y.UsingRabbitMq((context, configurator) =>
    {
        configurator.Host(builder.Configuration.GetSection("MassTransitConfiguraton")["Host"], configure =>
        {
            configure.Username(builder.Configuration.GetSection("MassTransitConfiguraton")["Username"]!);
            configure.Password(builder.Configuration.GetSection("MassTransitConfiguraton")["Password"]!);
        });

        configurator.ReceiveEndpoint(QueueNames.Stock_Payment_Canceled_Event_Queue_Name, conf => conf.ConfigureConsumer<PaymentCanceledEventConsumer>(context));
        configurator.ReceiveEndpoint(QueueNames.Stock_Order_Created_Event_Queue_Name, conf => conf.ConfigureConsumer<OrderCreatedEventConsumer>(context));
    });

});




var app = builder.Build();


app.UseSwagger();
app.UseSwaggerUI();


app.Run();
