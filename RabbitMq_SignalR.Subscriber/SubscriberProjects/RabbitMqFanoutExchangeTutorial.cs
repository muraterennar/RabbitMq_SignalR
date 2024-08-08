using RabbitMq_SignalR.Core.Constants;
using RabbitMq_SignalR.Core.Entites;
using RabbitMq_SignalR.Core.Helper;
using RabbitMQ.Client;

namespace RabbitMq_SignalR.Subscriber.SubscriberProjects;

public class RabbitMqFanoutExchangeTutorial
{
    RabbitMqOperations _operations = new RabbitMqOperations();

    RabbitMqExchangeModal _exchangeModal = new RabbitMqExchangeModal
    {
        ExchangeName = RabbitMqExchangeName.RabbitMqFanaoutExchange,
        ExchangeType = ExchangeType.Fanout,
        Durable = true,
        AutoDelete = false
    };

    RabbitMqBasicQosModal _basicQosModal = new RabbitMqBasicQosModal
    {
        PrefetchSize = 0,
        PrefetchCount = 1,
        Global = false
    };

    public void SubscriberMessages()
    {
        RabbitMqConnectionModal connection = _operations.ConnectionToRabbitMq(RabbitMqUris.RabbitMqUri);

        string randomQueueName = connection.Channel.QueueDeclare().QueueName;
        _operations.RabbitMqConsumeMessages(connection.Channel, _exchangeModal, randomQueueName, _basicQosModal);

        Console.ReadLine();
    }
}