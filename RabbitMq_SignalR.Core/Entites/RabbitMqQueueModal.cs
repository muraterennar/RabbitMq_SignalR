namespace RabbitMq_SignalR.Core.Entites;

public class RabbitMqQueueModal
{
    public string QueueName { get; set; }
    public bool Durable { get; set; }
    public bool Exclusive { get; set; }
    public bool AutoDelete { get; set; }
}