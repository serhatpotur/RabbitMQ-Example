// Publisher

using MassTransit;
using RabbitMQ.ESB.MassTransit.Shared.RequesetResponseMessages;

Console.WriteLine("Publisher");
string rabbitMqUri = "amqps://xdeobiyn:crbb-OcTyRzAijqE0osMdy6TMmZAc00w@sparrow.rmq.cloudamqp.com/xdeobiyn";
string queueName = "request-queue";

IBusControl bus = Bus.Factory.CreateUsingRabbitMq(factory =>
{
    factory.Host(rabbitMqUri);
});

await bus.StartAsync();

var request = bus.CreateRequestClient<RequestMessage>(new Uri($"{rabbitMqUri}/{queueName}"));

int i = 1;
while (true)
{
    var response = await request.GetResponse<ResponseMessage>(new()
    {
        MessageNo = i,
        Text = $"{i++}"
    });
    Console.WriteLine($"Response Received : {response.Message.Text}");
}

Console.Read();