namespace BirdTrading.Services.ShoppingCartAPI.RabbitMQSender
{
    public interface IRabbitMQCartSenderMessageSender
    {
        void SendMessage(Object message, string queueName);
    }
}
