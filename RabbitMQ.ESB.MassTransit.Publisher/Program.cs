// PUBLISHER


//ESB :    Enterprise Service Bus
using MassTransit;
using RabbitMQ.ESB.MassTransit.Shared.Messages;

string rabbitMqUri = "amqps://xdeobiyn:crbb-OcTyRzAijqE0osMdy6TMmZAc00w@sparrow.rmq.cloudamqp.com/xdeobiyn";
string queueName = "example-queue";

IBusControl bus = Bus.Factory.CreateUsingRabbitMq(factory =>
{
    factory.Host(rabbitMqUri);
});

//send direkt kuyruğa özel mesja gönderir. tek bir kuyruğa göndereceksek kullanırız
ISendEndpoint sendEndPoint = await bus.GetSendEndpoint(new($"{rabbitMqUri}/{queueName}"));

Console.Write("Gönderilecek Mesaj : ");
var message = Console.ReadLine();
await sendEndPoint.Send<IMessage>(new ExampleMessage()
{
    Text = message
});

Console.Read();