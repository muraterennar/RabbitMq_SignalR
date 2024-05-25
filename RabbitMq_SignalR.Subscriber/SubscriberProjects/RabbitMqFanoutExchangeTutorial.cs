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
        ExchangeName = RabbitMqExchangeName.RabbitMqExchange,
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

        _operations.RabbitMqConsumeMessages(connection.Channel, _exchangeModal, _basicQosModal);

        Console.ReadLine();
    }
}