using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

ConnectionFactory factory = new();
factory.Uri = new("amqps://xdeobiyn:crbb-OcTyRzAijqE0osMdy6TMmZAc00w@sparrow.rmq.cloudamqp.com/xdeobiyn");


// Bağlantıyı Aktifleştirme ve Kanal Açma

using IConnection connection = factory.CreateConnection();
using IModel channel = connection.CreateModel();

// Exchange Type türlerini burada örneklendireceğiz


///  BURASI CUNSOMER

#region Direct Exchange
/*
// 1. Adım : Publisher'da ki exchange ile birebir aynı isim ve type'a sahip exchange tanımlanmalıdır
channel.ExchangeDeclare(exchange: "direct-exchange-example", type: ExchangeType.Direct);

// 2. Adım : Publisher tarafında routing key'de bulunan değerde ki kuyruğa gönderilen mesajları kendi oluşturduğumuz kuyruğa yönlendirerek işlememiz gerekir. Bunun için öncelikle kuyruk oluştuurlmalıdır
string queueName = channel.QueueDeclare().QueueName;

// 3. Adım : Queue Bind'a ilgili routing key değerini veriyoruz ve consume edip işliyoruz.
channel.QueueBind(queue: queueName,
    exchange: "direct-exchange-example",
    routingKey: "direct-routingkey-example");

EventingBasicConsumer consumer = new(channel);
channel.BasicConsume(queue: queueName,
    autoAck: true,
    consumer: consumer);
consumer.Received += (sender, e) =>
{
    string message = Encoding.UTF8.GetString(e.Body.Span);
    Console.WriteLine(message);
};
*/

#endregion


#region Fanout Exchange
/*
// Not : Kuyruk adı önemli değildir. 10 tane farklı kuyruk isminde cunsomer'imiz olabilir ve exchange ismi publisher ile aynı olanların hepsi publisherdan gelen mesajı alıp işler

channel.ExchangeDeclare(exchange: "fanout-exchange-example", type: ExchangeType.Fanout);
Console.Write("Kuyruk adını giriniz : ");
string queueName = Console.ReadLine();

channel.QueueDeclare(queue: queueName, exclusive: false);

channel.QueueBind(queue: queueName, exchange: "fanout-exchange-example", routingKey: String.Empty);

EventingBasicConsumer consumer = new(channel);

channel.BasicConsume(queue: queueName, autoAck: true, consumer: consumer);

consumer.Received += (sender, e) =>
{
    string message = Encoding.UTF8.GetString(e.Body.Span);
    Console.WriteLine(message);
};


*/
#endregion


#region Topic Exchange
/*

channel.ExchangeDeclare(exchange: "topic-exchange-example", type: ExchangeType.Topic);
Console.Write("Dinlenecek Topic Formatını Belirtiniz : ");
string topic = Console.ReadLine();
string queueName = channel.QueueDeclare().QueueName;
channel.QueueBind(queue: queueName, exchange: "topic-exchange-example", routingKey: topic);
EventingBasicConsumer consumer = new(channel);
channel.BasicConsume(queue: queueName, autoAck: true, consumer: consumer);

consumer.Received += (sender, e) =>
{
    string message = Encoding.UTF8.GetString(e.Body.Span);
    Console.WriteLine(message);
};
*/
#endregion


#region Header Exchange

channel.ExchangeDeclare(exchange: "headers-exchange-example", type: ExchangeType.Headers);

Console.Write("Alınacak Header Value Giriniz : ");
string value = Console.ReadLine();

string queueName = channel.QueueDeclare().QueueName;
channel.QueueBind(queue: queueName, exchange: "headers-exchange-example", routingKey: string.Empty,
    arguments: new Dictionary<string, object>
    {
        ["key1"] = value
    });

EventingBasicConsumer consumer = new(channel);
channel.BasicConsume(queue: queueName, autoAck: true, consumer: consumer);
consumer.Received += (sender, e) =>
{
    string message = Encoding.UTF8.GetString(e.Body.Span);
    Console.WriteLine(message);
};

#endregion

Console.Read();