namespace RabbitMq_SignalR.Core.Helper;

public class RabbitMqBasicQosModal
{
    public uint PrefetchSize { get; set; }
    public ushort PrefetchCount { get; set; }
    public bool Global { get; set; }
}