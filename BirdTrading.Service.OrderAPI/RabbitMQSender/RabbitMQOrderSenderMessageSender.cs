using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;

namespace BirdTrading.Services.OrderAPI.RabbitMQSender
{
    public class RabbitMQOrderSenderMessageSender : IRabbitMQOrderSenderMessageSender
    {

        private readonly string _hostName;
        private readonly string _username;
        private readonly string _password;
        private IConnection _connection;
        private const string OrderCreated_RewardsUpdateQueue = "RewardsUpdateQueue";
        private const string OrderCreated_EmailUpdateQueue = "EmailUpdateQueue";

        public RabbitMQOrderSenderMessageSender()
        {
            _hostName = "localhost";
            _password = "guest";
            _username = "guest";
        }

        public void SendMessage(object message, string exchangeName)
        {
            if (ConnectionExists())
            {

                using var channel = _connection.CreateModel();
                channel.ExchangeDeclare(exchangeName, ExchangeType.Direct, durable: false);
                channel.QueueDeclare(OrderCreated_EmailUpdateQueue,false,false,false,null);
                channel.QueueDeclare(OrderCreated_RewardsUpdateQueue, false, false, false, null);

                channel.QueueBind(OrderCreated_EmailUpdateQueue, exchangeName, "EmailUpdateQueue");
                channel.QueueBind(OrderCreated_RewardsUpdateQueue, exchangeName, "RewardsUpdateQueue");

                var json = JsonConvert.SerializeObject(message);
                var body = Encoding.UTF8.GetBytes(json);
                channel.BasicPublish(exchange: exchangeName, routingKey: "EmailUpdateQueue", null, body: body);
                channel.BasicPublish(exchange: exchangeName, routingKey: "RewardsUpdateQueue", null, body: body);
            }

        }

        private void CreateConnection()
        {
            try
            {
                var factory = new ConnectionFactory
                {
                    HostName = _hostName,
                    Password = _password,
                    UserName = _username
                };

                _connection = factory.CreateConnection();
            }
            catch (Exception ex)
            {

            }
        }

        private bool ConnectionExists()
        {
            if (_connection != null)
            {
                return true;
            }
            CreateConnection();
            return true;
        }
    }
}

