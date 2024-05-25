using RabbitMq_SignalR.Core.Constants;
using RabbitMq_SignalR.Core.Entites;
using RabbitMq_SignalR.Core.Helper;
using RabbitMQ.Client;

namespace RabbitMq_SignalR.Publisher.PublisherProjects;

public class RabbitMqFanoutExchangeTutorial
{
    RabbitMqOperations operations = new RabbitMqOperations();
    RabbitMqExchangeModal rabbitMqExchangeModal = new RabbitMqExchangeModal()
    {
        ExchangeName = "RabbitMqFanoutExchange",
        ExchangeType = ExchangeType.Fanout,
        Durable = true,
        AutoDelete = false
    };
    
    public void PublishMessages()
    {
        RabbitMqConnectionModal rabbitMqConnectionModal = operations.ConnectionToRabbitMq(RabbitMqUris.RabbitMqUri);
       
        var list = Enumerable.Range(1, 50)
            .Select(x => $"Message {x}")
            .ToList();
        
        operations.RabbitMqPublishMessages<string>(rabbitMqConnectionModal.Channel, rabbitMqExchangeModal, list);
        
        Console.ReadLine();
    }
}