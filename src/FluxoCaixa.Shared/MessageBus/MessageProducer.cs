using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace FluxoCaixa.Shared.MessageBus
{
    public class MessageProducer : IMessageProducer
    {
        private readonly ConnectionFactory _factory;
        private readonly RabbitMQConfiguration _config;

        public MessageProducer(IOptions<RabbitMQConfiguration> options)
        {
            _config = options.Value;
            _factory = new ConnectionFactory()
            {
                HostName = _config.Host
            };
        }
        public async Task EnviaMensagem<T>(T message)
        { 
            using (var connection = _factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare(queue: _config.Queue,
                                         durable: true,
                                         exclusive: false,
                                         autoDelete: false,
                                         arguments: null);

                    string messageString = JsonSerializer.Serialize(message);
                    var body = Encoding.UTF8.GetBytes(messageString);

                    var properties = channel.CreateBasicProperties();
                    properties.Persistent = true;

                    channel.BasicPublish(exchange: "",
                                         routingKey: _config.Queue,
                                         basicProperties: properties,
                                         body: body);
                }
            }
        }
    }
}
