using MassTransit;
using Payment.API.Consumers;
using SharedMicroservice.Constants;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<StockAcceptedEventConsumer>();
    x.UsingRabbitMq((context, configurator) =>
    {
        configurator.Host(builder.Configuration.GetSection("MassTransitConfiguraton")["Host"], configure =>
        {
            configure.Username(builder.Configuration.GetSection("MassTransitConfiguraton")["Username"]!);
            configure.Password(builder.Configuration.GetSection("MassTransitConfiguraton")["Password"]!);
        });

        configurator.ReceiveEndpoint(QueueNames.Payment_Stock_Accepted_Event_Queue_Name, conf =>
        {
            conf.ConfigureConsumer<StockAcceptedEventConsumer>(context);
        });
    });
});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();


app.Run();
