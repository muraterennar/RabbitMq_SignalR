using System.Text;
using RabbitMq_SignalR.Core.Constants;
using RabbitMq_SignalR.Core.Entites;
using RabbitMq_SignalR.Core.Helper;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace RabbitMq_SignalR.Subscriber.SubscriberProjects;

public class RabbitMqDirectExchangeTutorial
{
    RabbitMqOperations _rabbitMqOperations = new RabbitMqOperations();
    
    
    public void SubscribeMessages()
    {
        try
        {
            RabbitMqConnectionModal rabbitMqConnectionModal =
                _rabbitMqOperations.ConnectionToRabbitMq(RabbitMqUris.RabbitMqUri);

            rabbitMqConnectionModal.Channel.BasicQos(0, 1, false);
            var consumer = new EventingBasicConsumer(rabbitMqConnectionModal.Channel);

            rabbitMqConnectionModal.Channel.BasicConsume(RabbitMqLogQueues.Info, false, consumer);

            Console.WriteLine("Logları Dinlemeye Başla...");

            consumer.Received += (sender, e) =>
            {
                try
                {
                    var message = Encoding.UTF8.GetString(e.Body.ToArray());
                
                    // Eğer mesajları asenkron olarak işlemek isterseniz:
                    Task.Delay(1000).Wait(); // Asenkron bekleme (Thread.Sleep yerine)

                    Console.WriteLine("Gelen Mesaj: " + message);
                
                    rabbitMqConnectionModal.Channel.BasicAck(e.DeliveryTag, false);
                }
                catch (Exception ex)
                {
                    // Burada hata durumlarını loglayabilirsiniz
                    Console.WriteLine($"Mesaj işlenirken bir hata oluştu: {ex.Message}");
                
                    // Geri alma (Nack) işlemi ile mesajı kuyruğa geri koyabilirsiniz
                    rabbitMqConnectionModal.Channel.BasicNack(e.DeliveryTag, false, true);
                }
            };

            // Uygulamanın sonsuz döngüde kalmasını sağlar
            Console.ReadLine();

            // Consumer ve kanal bağlantısını kapatma işlemi
            rabbitMqConnectionModal.Channel.Close();
            rabbitMqConnectionModal.Connection.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Bağlantı kurulurken bir hata oluştu: {ex.Message}");
        }
    }
}

public static class RabbitMqLogQueues
{
    public const string Info = "direct_queue_Info";
    public const string Error = "direct_queue_Error";
    public const string Warning = "direct_queue_Warning";
    public const string Critical = "direct_queue_Critical";
}