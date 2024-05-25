using RabbitMq_SignalR.Core.Constants;
using RabbitMq_SignalR.Core.Entites;
using RabbitMq_SignalR.Core.Helper;
using RabbitMQ.Client;

namespace RabbitMq_SignalR.Publisher.PublisherProjects;

public class RabbitMqBasicTutorial
{
    public void PublishMessages()
    {
        var operations = new RabbitMqOperations();
        var rabbitMqQueueModal = new RabbitMqQueueModal()
        {
            QueueName = RabbitMqQueuesName.RabbitMqStarterQueue,
            Durable = true,
            Exclusive = false,
            AutoDelete = false
        };

        RabbitMqConnectionModal rabbitMqConnectionModal = operations.ConnectionToRabbitMq(RabbitMqUris.RabbitMqUri);

        var list = new List<string>()
        {
            $"{RabbitMqMessages.RabbitMqHelloWorldMessage}- {DateTime.Now}",
            $"{RabbitMqMessages.RabbitMqHelloWorldMessage}- {DateTime.Now.AddHours(1)}",
            $"{RabbitMqMessages.RabbitMqHelloWorldMessage}- {DateTime.Now.AddHours(2)}",
            $"{RabbitMqMessages.RabbitMqHelloWorldMessage}- {DateTime.Now.AddHours(3)}",
        };

        operations.RabbitMqPublishMessages<string>(rabbitMqConnectionModal.Channel, rabbitMqQueueModal, list);

        Console.ReadLine();
    }
}