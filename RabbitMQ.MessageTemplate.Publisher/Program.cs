// MESSAGE TEMPLATE PUBLİSHER


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
byte[] message = Encoding.UTF8.GetBytes("merhaba");
channel.BasicPublish(exchange: "", routingKey: queueName, body: message);
*/
#endregion


#region Publish/Subscribe (Pub/Sub) Template
/*
string exchangeName = "pub-sub-example-exchange";
channel.ExchangeDeclare(exchange: exchangeName, ExchangeType.Fanout);
for (int i = 0; i < 100; i++)
{

    byte[] message = Encoding.UTF8.GetBytes($"merhaba {i}");

    channel.BasicPublish(exchange: exchangeName, routingKey: String.Empty, body: message);
}
*/
#endregion

#region Work Queue Template
/*
string queueName = "work-queue-example-queue";
channel.QueueDeclare(queue: queueName, durable: false, exclusive: false, autoDelete: false);
for (int i = 0; i < 100; i++)
{
    byte[] message = Encoding.UTF8.GetBytes($"merhaba {i}");
    channel.BasicPublish(exchange: String.Empty, routingKey: queueName, body: message);

}
*/
#endregion

#region Request-Response Template
// Request-Respponse Templatinde hem consumera mesaj gönderiyoruz hemde consumerdan dönen response değerini alıyoruz

string requestQueueName = "request-response-example-queue";
channel.QueueDeclare(queue: requestQueueName, durable: false, exclusive: false, autoDelete: false);

// Consumerdan dönecek olan sonucuelde edeceğimiz kuyruğun adı
string replyQueueName = channel.QueueDeclare().QueueName;
// Gönderilen mesajı ifade eden kolerasyon değeri olusturduk. Bu değeri ilgili consumera gönderiyoruz
string correlationId = Guid.NewGuid().ToString();

#region Request Mesajını Oluşturma ve Gönderme

IBasicProperties properties = channel.CreateBasicProperties();
properties.CorrelationId = correlationId;
properties.ReplyTo = replyQueueName;

for (int i = 0; i < 10; i++)
{
    byte[] message = Encoding.UTF8.GetBytes($"Merhaba {i}");
    channel.BasicPublish(exchange: String.Empty, routingKey: requestQueueName, body: message, basicProperties: properties);

}

#endregion


#region Response Kuyruğunu Dinleme

EventingBasicConsumer consumer = new(channel);
channel.BasicConsume(queue: replyQueueName, autoAck: true, consumer: consumer);
consumer.Received += (sender, e) =>
{
    string message = Encoding.UTF8.GetString(e.Body.Span);

    if (e.BasicProperties.CorrelationId == correlationId)
    {
        Console.WriteLine($"Response : {message}");
    }
};




#endregion

#endregion


Console.Read();