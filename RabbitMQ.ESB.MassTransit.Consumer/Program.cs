//CONSUMER

//ESB :    Enterprise Service Bus

using MassTransit;
using RabbitMQ.ESB.MassTransit.Consumer.Consumers;

string rabbitMqUri = "amqps://xdeobiyn:crbb-OcTyRzAijqE0osMdy6TMmZAc00w@sparrow.rmq.cloudamqp.com/xdeobiyn";
string queueName = "example-queue";
IBusControl bus = Bus.Factory.CreateUsingRabbitMq(factory =>
{
    factory.Host(rabbitMqUri);
    factory.ReceiveEndpoint(queueName, endpoint =>
    {
        endpoint.Consumer<ExampleMessageConsumer>();
    });
});
await bus.StartAsync();
Console.Read();