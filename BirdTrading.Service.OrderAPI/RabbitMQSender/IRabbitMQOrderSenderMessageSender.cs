namespace BirdTrading.Services.OrderAPI.RabbitMQSender
{
    public interface IRabbitMQOrderSenderMessageSender
    {
        void SendMessage(Object message, string exchangeName);
    }
}
