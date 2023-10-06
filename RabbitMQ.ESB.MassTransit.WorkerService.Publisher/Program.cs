// Publisher


using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RabbitMQ.ESB.MassTransit.WorkerService.Publisher.Services;


IHost host = Host.CreateDefaultBuilder(args).ConfigureServices(services =>
{
    services.AddMassTransit(config =>
    {
        config.UsingRabbitMq((context, _config) =>
        {
            _config.Host("amqps://xdeobiyn:crbb-OcTyRzAijqE0osMdy6TMmZAc00w@sparrow.rmq.cloudamqp.com/xdeobiyn");
        });
    });

    // worker service'e bağlı olan tüm kuyruklara mesaj gönderir
    services.AddHostedService<PublishMessageService>(provider =>
    {
        using IServiceScope scope = provider.CreateScope();
        IPublishEndpoint publishEndpoint = scope.ServiceProvider.GetService<IPublishEndpoint>();
        return new(publishEndpoint);
    });
}).Build();

await host.RunAsync();