

using RabbitMQ.Client;
using System.Text;


// Bağlantı Oluşturma

ConnectionFactory factory = new();
factory.Uri = new("amqps://xdeobiyn:crbb-OcTyRzAijqE0osMdy6TMmZAc00w@sparrow.rmq.cloudamqp.com/xdeobiyn");


// Bağlantıyı Aktifleştirme ve Kanal Açma

using IConnection connection = factory.CreateConnection();
using IModel channel = connection.CreateModel();


// Queue Oluşturma

//durable : Kuyrukta ki mesajların kalıcılığıyla alakalıdır
//exlusive : Kuyruktaki mesaj özel mi. Bir bağlantıya özel olarak üretilir ve daha sonra silinir. False olursa birden fazla channel tarafından bağlanılabilir
//autodelete: Kuyurukta ki tüm mesajlar tüketildikten sonra silinsin mi
// durable:true : kuyruk kalıcı hale gelir
channel.QueueDeclare(queue: "example-queue", exclusive: false, durable: true);


// Mesajlar kalıcı hale gelir
IBasicProperties properties = channel.CreateBasicProperties();
properties.Persistent = true;

// Queue mesaj gönderme

//RabbitMQ kuyruğa atacağı mesajları byte türünde kabul eder.

byte[] message = Encoding.UTF8.GetBytes("Hello");
channel.BasicPublish(exchange: "", routingKey: "example-queue", body: message, basicProperties: properties);

Console.Read();