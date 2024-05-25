using RabbitMQ.Client;

namespace RabbitMq_SignalR.Core.Entites;

public class RabbitMqConnectionModal: IDisposable

{
    public ConnectionFactory Factory { get; set; }
    public IConnection Connection { get; set; }
    public IModel Channel { get; set; }
    
    public void Dispose()
    {
        Channel?.Dispose();
        Connection?.Dispose();
    }
}