using RabbitMQ.Client;
using System.Text;

ConnectionFactory factory = new();
factory.Uri = new("amqps://xdeobiyn:crbb-OcTyRzAijqE0osMdy6TMmZAc00w@sparrow.rmq.cloudamqp.com/xdeobiyn");


// Bağlantıyı Aktifleştirme ve Kanal Açma

using IConnection connection = factory.CreateConnection();
using IModel channel = connection.CreateModel();

// Exchange Type türlerini burada örneklendireceğiz


// BURASI PUBLISHER

#region Direct Exchange
/*
channel.ExchangeDeclare(exchange: "direct-exchange-example", type: ExchangeType.Direct);
while (true)
{
    Console.Write("Mesaj : ");
    string message = Console.ReadLine();
    byte[] byteMessage = Encoding.UTF8.GetBytes(message);
    channel.BasicPublish(exchange: "direct-exchange-example",
        routingKey: "direct-routingkey-example",
        body: byteMessage);

}
*/
#endregion


#region Fanouut Exchange
/*
channel.ExchangeDeclare(exchange: "fanout-exchange-example", type: ExchangeType.Fanout);
for (int i = 0; i < 100; i++)
{
    byte[] byteMessage = Encoding.UTF8.GetBytes($"Merhaba {i}");
    channel.BasicPublish(exchange: "fanout-exchange-example",
        routingKey: String.Empty,
        body: byteMessage);

}
*/
#endregion


#region Topic Exchange
/*
channel.ExchangeDeclare(exchange: "topic-exchange-example", type: ExchangeType.Topic);
for (int i = 0; i < 100; i++)
{
    byte[] message = Encoding.UTF8.GetBytes($"Merhaba {i}");
    Console.Write("Topic Giriniz : ");
    string topic = Console.ReadLine();
    channel.BasicPublish(exchange: "topic-exchange-example", routingKey: topic, body: message);
}

*/
#endregion


#region Headers Exchange

channel.ExchangeDeclare(exchange: "headers-exchange-example", type: ExchangeType.Headers);
for (int i = 0; i < 100; i++)
{

    byte[] message = Encoding.UTF8.GetBytes($"Merhaba {i}");
    Console.Write("Lütfen header giriniz: ");
    string header = Console.ReadLine();

    IBasicProperties basicProperties = channel.CreateBasicProperties();
    basicProperties.Headers = new Dictionary<string, object>()
    {
        ["key1"] = header
    };
    channel.BasicPublish(exchange: "headers-exchange-example", routingKey: string.Empty, body: message, basicProperties: basicProperties);

}

#endregion

Console.Read();