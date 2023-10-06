// Consumer


using MassTransit;
using Microsoft.Extensions.Hosting;
using RabbitMQ.ESB.MassTransit.WorkerService.Consumer.Consumers;

IHost host = Host.CreateDefaultBuilder(args).ConfigureServices(services =>
{
    services.AddMassTransit(config =>
    {
        // Consumer ekledik
        config.AddConsumer<ExampleMessageConsumer>();
        config.UsingRabbitMq((context, _config) =>
        {
            _config.Host("amqps://xdeobiyn:crbb-OcTyRzAijqE0osMdy6TMmZAc00w@sparrow.rmq.cloudamqp.com/xdeobiyn");

            // Ve eklediğimiz consumerın hangi kuyruğu tüketeceğini yapılandırdık
            _config.ReceiveEndpoint("example-message-queue", e => e.ConfigureConsumer<ExampleMessageConsumer>(context));
        });
    });

}).Build();

await host.RunAsync();