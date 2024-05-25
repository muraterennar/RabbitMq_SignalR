using System.Text;

namespace RabbitMq_SignalR.Core.Helper;

public class CovertMessageHelper
{
    public byte[] ConvertMessageToByte(string message)
    {
        byte[] body = Encoding.UTF8.GetBytes(message);
        return body;
    }
    
    public string ConvertByteToMessage(byte[] body)
    {
        string message = Encoding.UTF8.GetString(body);
        return message;
    }
}