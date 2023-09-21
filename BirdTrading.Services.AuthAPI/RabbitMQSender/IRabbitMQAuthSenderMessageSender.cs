namespace BirdTrading.Services.AuthAPI.RabbitMQSender
{
    public interface IRabbitMQAuthSenderMessageSender
    {
        void SendMessage(Object message, string queueName);
    }
}
