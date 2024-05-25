using RabbitMQ.Client;

namespace RabbitMq_SignalR.Core.Entites;
public class RabbitMqExchangeModal
{
    public string ExchangeName { get; set; }
    public string ExchangeType { get; set; }
    public bool Durable { get; set; }
    public bool AutoDelete { get; set; } 
}