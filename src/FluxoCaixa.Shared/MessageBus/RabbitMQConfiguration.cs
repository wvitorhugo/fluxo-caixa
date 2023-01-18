 
namespace FluxoCaixa.Shared.MessageBus
{
    public class RabbitMQConfiguration
    {
        public string Host { get; set; }
        public string Queue { get; set; }
        public int ConsumerDelay { get; set; }
    }
}
