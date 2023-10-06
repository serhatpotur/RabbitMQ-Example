
// Bağlantı Oluşturma

using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

ConnectionFactory factory = new();
factory.Uri = new("amqps://xdeobiyn:crbb-OcTyRzAijqE0osMdy6TMmZAc00w@sparrow.rmq.cloudamqp.com/xdeobiyn");


// Bağlantıyı Aktifleştirme ve Kanal Açma

using IConnection connection = factory.CreateConnection();
using IModel channel = connection.CreateModel();


// Queue Oluşturma
//Queue Publisher ile aynı olmalı

channel.QueueDeclare(queue: "example-queue", exclusive: false, durable: true);


// Queue Mesaj Okuma

//autoAck : Kuyruktan mesaj aldıktan sonra o mesajın kuyruktan fiziksel olrak silinip silinmeyeceğini belriten davranıştır.
// autoAck: false diyerek silme işlemini cunsomerden onay bekle onay gelirse sil dedik. True ise Onay beklemeden sil demektir

EventingBasicConsumer consumer = new(channel);
channel.BasicConsume(queue: "example-queue", autoAck: false, consumer);
channel.BasicQos(prefetchSize: 0, prefetchCount: 1, global: false);
consumer.Received += (sender, e) =>
{
    // Kuyruğa gelen mesajın işlendiği alan
    // e.Body : Kuyrukta ki mesajın verisini bütünsel olarak getirecektir
    // e.Body.Span veya e.Body.ToArray() kuyrukta ki mesajın byte verisini getirecektir.

    Console.WriteLine(Encoding.UTF8.GetString(e.Body.Span));

    //DeliveryTag değerine saship olan ilgili mesajın başarıyla işlendiğini onaylar
    channel.BasicAck(deliveryTag: e.DeliveryTag, multiple: false);
};

Console.Read();