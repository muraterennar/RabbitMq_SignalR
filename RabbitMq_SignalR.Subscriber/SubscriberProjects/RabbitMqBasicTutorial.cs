using RabbitMq_SignalR.Core.Constants;
using RabbitMq_SignalR.Core.Entites;
using RabbitMq_SignalR.Core.Helper;

namespace RabbitMq_SignalR.Subscriber.SubscriberProjects;

public class RabbitMqBasicTutorial
{
    public void SubscriberMessages()
    {
        var operations = new RabbitMqOperations();
        RabbitMqConnectionModal connectionModal = operations.ConnectionToRabbitMq(RabbitMqUris.RabbitMqUri);

        var mqQueueModal = new RabbitMqQueueModal()
        {
            QueueName = RabbitMqQueuesName.RabbitMqStarterQueue,
            Durable = true,
            Exclusive = false,
            AutoDelete = false
        };

        var mqBasicQosModal = new RabbitMqBasicQosModal()
        {
            PrefetchSize = 0,
            PrefetchCount = 1,
            Global = false
        };

        operations.RabbitMqConsumeMessages(connectionModal.Channel, mqQueueModal, mqBasicQosModal);

        Console.ReadLine();
    }
}