using RabbitMQ.Client;

namespace Contact.API
{
    public class RabbitMqClientService : IDisposable
    {
        private readonly ConnectionFactory _connectionFactory;
        private IConnection _connection;
        private IModel _channel;
        public static string ExchangeName = "Contact";
        public static string QueueName = "ContactReport";
        public static string ResultQueue = "ContactResult";
        private readonly ILogger<RabbitMqClientService> _logger;

        public RabbitMqClientService(ConnectionFactory connectionFactory, ILogger<RabbitMqClientService> logger)
        {
            _connectionFactory = connectionFactory;
            _logger = logger;
        }
        public IModel Connect()
        {
            _connection = _connectionFactory.CreateConnection();

            if (_channel is { IsOpen: true })
            {
                return _channel;
            }
            _channel = _connection.CreateModel();
            _channel.ExchangeDeclare(ExchangeName, type: "direct", true, false);
            _channel.QueueDeclare(QueueName, true, false, false, null);
            _channel.QueueBind(exchange: ExchangeName, queue: QueueName, routingKey: ExchangeName);

            _logger.LogInformation("Connect to RabbitMq");
            return _channel;
        }
        public IModel Connect2()
        {
            _connection = _connectionFactory.CreateConnection();

            if (_channel is { IsOpen: true })
            {
                return _channel;
            }
            _channel = _connection.CreateModel();
            _channel.ExchangeDeclare(ExchangeName, type: "direct", true, false);
            _channel.QueueDeclare(ResultQueue, true, false, false, null);
            _channel.QueueBind(exchange: ExchangeName, queue: ResultQueue, routingKey: ExchangeName);

            _logger.LogInformation("Connect to RabbitMq");
            return _channel;
        }


        public void Dispose()
        {
            _channel?.Close();
            _channel?.Dispose();
            _connection?.Close();
            _connection?.Dispose();

            _logger.LogInformation("Connection lost with RabbitMq");
        }
    }
}
