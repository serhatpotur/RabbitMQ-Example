// Consumer

using MassTransit;
using RabbitMQ.ESB.MassTransit.RequestResponsePattern.Consumer.Cunsomers;

Console.WriteLine("Consumer");
string rabbitMqUri = "amqps://xdeobiyn:crbb-OcTyRzAijqE0osMdy6TMmZAc00w@sparrow.rmq.cloudamqp.com/xdeobiyn";
string queueName = "request-queue";

IBusControl bus = Bus.Factory.CreateUsingRabbitMq(factory =>
{
    factory.Host(rabbitMqUri);
    factory.ReceiveEndpoint(queueName, endpoint =>
    {
        endpoint.Consumer<RequestMessageConsumer>();
    });
});

await bus.StartAsync();
Console.Read();