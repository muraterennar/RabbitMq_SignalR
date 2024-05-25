using RabbitMq_SignalR.Core.Entites;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace RabbitMq_SignalR.Core.Helper;

public class RabbitMqOperations
{
    // Declare Exchange
    public void RabbitMqPublishMessages<T>(IModel channel, RabbitMqExchangeModal exchangeModal, IList<T> messages)
    {
        RabbitMqCreateExchange(channel, exchangeModal);

        foreach (var message in messages)
        {
            string newMessage = message.ToString();
            byte[] messageBody = new CovertMessageHelper().ConvertMessageToByte(newMessage);

            channel.BasicPublish(exchangeModal?.ExchangeName, string.Empty, null, messageBody);

            Console.WriteLine(" [] Sent {0}", newMessage);
        }
    }

    // Declare Queue
    public void RabbitMqPublishMessages<T>(IModel channel, RabbitMqQueueModal queueModal, IList<T> messages)
    {
        RabbitMqCreateQueue(channel, queueModal);

        foreach (var message in messages)
        {
            string newMessage = message.ToString();
            byte[] messageBody = new CovertMessageHelper().ConvertMessageToByte(newMessage);

            channel.BasicPublish(string.Empty, queueModal.QueueName, null, messageBody);

            Console.WriteLine(" [] Sent {0}", newMessage);
        }
    }

    public void RabbitMqConsumeMessages(IModel channel, RabbitMqQueueModal queueModal,
        RabbitMqBasicQosModal basicQosModal)
    {
        //channel.QueueDeclare(queueModal.QueueName, queueModal.Durable, queueModal.Exclusive, queueModal.AutoDelete);
        channel.BasicQos(basicQosModal.PrefetchSize, basicQosModal.PrefetchCount, basicQosModal.Global);

        EventingBasicConsumer consumer = new EventingBasicConsumer(channel);

        channel.BasicConsume(queueModal.QueueName, false, consumer);

        Console.WriteLine("Loglar Dinlenşyor...");
        
        consumer.Received += (model, ea) =>
        {
            byte[] body = ea.Body.ToArray();
            string message = new CovertMessageHelper().ConvertByteToMessage(body);
            Console.WriteLine(" [x] Received {0}", message);

            channel.BasicAck(ea.DeliveryTag, false);
        };
    }

    public void RabbitMqConsumeMessages(IModel channel, RabbitMqExchangeModal exchangeModal,
        RabbitMqBasicQosModal basicQosModal)
    {
        string randomQueueName = channel.QueueDeclare().QueueName;

        channel.QueueBind(randomQueueName, exchangeModal.ExchangeName, string.Empty);

        channel.BasicQos(basicQosModal.PrefetchSize, basicQosModal.PrefetchCount, basicQosModal.Global);

        EventingBasicConsumer consumer = new EventingBasicConsumer(channel);

        channel.BasicConsume(randomQueueName, false, consumer);

        Console.WriteLine("Loglar Dinlenşyor...");
        
        consumer.Received += (model, ea) =>
        {
            byte[] body = ea.Body.ToArray();
            string message = new CovertMessageHelper().ConvertByteToMessage(body);
            Console.WriteLine(" [x] Received {0}", message);

            channel.BasicAck(ea.DeliveryTag, false);
        };
    }


    public RabbitMqConnectionModal ConnectionToRabbitMq(string uri)
    {
        ConnectionFactory factory = new ConnectionFactory();
        factory.Uri = new Uri(uri);

        IConnection connection = factory.CreateConnection();
        IModel channel = connection.CreateModel();

        return new RabbitMqConnectionModal()
        {
            Factory = factory,
            Connection = connection,
            Channel = channel
        };
    }

    public void RabbitMqCreateQueue(IModel channel, RabbitMqQueueModal queueModal)
    {
        channel.QueueDeclare(queueModal.QueueName, queueModal.Durable, queueModal.Exclusive, queueModal.AutoDelete);
    }

    public void RabbitMqCreateExchange(IModel channel, RabbitMqExchangeModal exchangeModal)
    {
        channel.ExchangeDeclare(exchangeModal.ExchangeName, exchangeModal.ExchangeType, exchangeModal.Durable,
            exchangeModal.AutoDelete);
    }
}