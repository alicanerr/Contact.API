using RabbitMQ.Client;
using System.Text.Json;

namespace Report.API
{
    public class RabbitMqPublisher
    {
        private readonly RabbitMqClientService _rabbitMqClientService;

        public RabbitMqPublisher(RabbitMqClientService rabbitMqClientService)
        {
            _rabbitMqClientService = rabbitMqClientService;
        }
        public void Publish()
        {
            var channel = _rabbitMqClientService.Connect();
            var properties = channel.CreateBasicProperties();
            properties.Persistent = true;
            channel.BasicPublish(RabbitMqClientService.ExchangeName, RabbitMqClientService.ExchangeName,basicProperties:properties);
        }
    }
}
