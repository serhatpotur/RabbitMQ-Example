// MESSAGE TEMPLATE CUNSOMER

using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

ConnectionFactory factory = new();
factory.Uri = new("amqps://xdeobiyn:crbb-OcTyRzAijqE0osMdy6TMmZAc00w@sparrow.rmq.cloudamqp.com/xdeobiyn");



using IConnection connection = factory.CreateConnection();
using IModel channel = connection.CreateModel();

#region Point To Point(P2P) Template
/*
string queueName = "p2p-example-queue";

channel.QueueDeclare(queue: queueName, durable: false, exclusive: false, autoDelete: false);

EventingBasicConsumer consumer = new(channel);
channel.BasicConsume(queue: queueName, autoAck: false, consumer: consumer);

consumer.Received += (sender, e) =>
{
    string message = Encoding.UTF8.GetString(e.Body.Span);
    Console.WriteLine(message);
};
*/
#endregion


#region Publish/Subscribe (Pub/Sub) Template
/*
string exchangeName = "pub-sub-example-exchange";
channel.ExchangeDeclare(exchange: exchangeName, ExchangeType.Fanout);

string queueName = channel.QueueDeclare().QueueName;
channel.QueueBind(queue: queueName, exchange: exchangeName, routingKey: string.Empty);

// BasicQos methodu ile bir ölçeklendirme yapılabilir. Bu template de genelde kullanmılır. Uygulamak bize kalmış
channel.BasicQos(prefetchSize: 1, prefetchCount: 0, global: false);
EventingBasicConsumer consumer = new(channel);
channel.BasicConsume(queue:queueName,autoAck:false,consumer:consumer);

consumer.Received += (sender, e) =>
{
    string message = Encoding.UTF8.GetString(e.Body.Span);
    Console.WriteLine(message);
};
*/
#endregion

#region Work Queue Template
/*
string queueName = "work-queue-example-queue";
channel.QueueDeclare(queue: queueName, durable: false, exclusive: false, autoDelete: false);
EventingBasicConsumer consumer = new EventingBasicConsumer(channel);

channel.BasicConsume(queue: queueName, autoAck: true, consumer: consumer);

channel.BasicQos(1, 0, false);

consumer.Received += (sender, e) =>
{
    string message = Encoding.UTF8.GetString(e.Body.Span);
    Console.WriteLine(message);
};
*/

#endregion

#region Request-Response Template

string requestQueueName = "request-response-example-queue";
channel.QueueDeclare(queue: requestQueueName, durable: false, exclusive: false, autoDelete: false);

EventingBasicConsumer consumer = new(channel);
channel.BasicConsume(queue: requestQueueName, autoAck: true, consumer: consumer);
consumer.Received += (sender, e) =>
{
    string message = Encoding.UTF8.GetString(e.Body.Span);
    Console.WriteLine(message);
    byte[] responseMessage = Encoding.UTF8.GetBytes($"İşlem Tamamlandı : {message}");
    IBasicProperties properties = channel.CreateBasicProperties();
    properties.CorrelationId = e.BasicProperties.CorrelationId;
    string responseQueueName = e.BasicProperties.ReplyTo;

    channel.BasicPublish(exchange: String.Empty, routingKey: responseQueueName, basicProperties: properties, body: responseMessage);
};

#endregion


Console.Read();