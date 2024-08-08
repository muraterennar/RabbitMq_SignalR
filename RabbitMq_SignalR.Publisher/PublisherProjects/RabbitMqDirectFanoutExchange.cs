using RabbitMq_SignalR.Core.Constants;
using RabbitMq_SignalR.Core.Entites;
using RabbitMq_SignalR.Core.Helper;
using RabbitMQ.Client;

namespace RabbitMq_SignalR.Publisher.PublisherProjects;

public class RabbitMqDirectFanoutExchange
{
    readonly RabbitMqOperations _rabbitMqOperations = new RabbitMqOperations();

    RabbitMqExchangeModal _rabbitMqExchangeModal = new RabbitMqExchangeModal()
    {
        ExchangeName = RabbitMqExchangeName.RabbitMqDirectExchange,
        ExchangeType = ExchangeType.Direct,
        Durable = true,
        AutoDelete = false
    };

    public void PublishMessages()
    {
        RabbitMqConnectionModal rabbitMqConnectionModal =
            _rabbitMqOperations.ConnectionToRabbitMq(RabbitMqUris.RabbitMqUri);

        rabbitMqConnectionModal.Channel.ExchangeDeclare("logs_direct", ExchangeType.Direct, true, false);

        Enum.GetNames(typeof(RabbitLogType)).ToList().ForEach(x =>
        {
            var queueName = $"direct_queue_{x}";
            var routeKey = $"route-{x}";
            rabbitMqConnectionModal.Channel.QueueDeclare(queueName, true, false, false);
            rabbitMqConnectionModal.Channel.QueueBind(queueName, "logs_direct", routeKey);
        });

        Enumerable.Range(1, 50).ToList().ForEach(x =>
        {
            RabbitLogType log = (RabbitLogType)new Random().Next(1, 4);
            var message = $"Log Type: {log}";
            byte[] messageBody = new CovertMessageHelper().ConvertMessageToByte(message);

            var routeKey = $"route-{log}";
            rabbitMqConnectionModal.Channel.BasicPublish("logs_direct", routeKey, null, messageBody);
            Console.WriteLine($" [] Log Sent {message}");
        });
    }

    private enum RabbitLogType
    {
        Info = 1,
        Error = 2,
        Warning = 3,
        Critical = 4
    }
}