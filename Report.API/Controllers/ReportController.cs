using Microsoft.AspNetCore.Mvc;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

namespace Report.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ReportController : ControllerBase
    {
        private readonly RabbitMqPublisher _rabbitMqPublisher;
        private readonly RabbitMqClientService _rabbitMqClientService;
        private readonly IModel _channel;

        public ReportController(RabbitMqPublisher rabbitMqPublisher, RabbitMqClientService rabbitMqClientService)
        {
            _rabbitMqPublisher = rabbitMqPublisher;
            _rabbitMqClientService = rabbitMqClientService;
            _channel = _rabbitMqClientService.Connect2();
            _channel.BasicQos(0, 1, false);
        }
        [HttpGet(Name = "Report")]
        public  Task<List<ReportObject>> Get()
        {
            var result = new TaskCompletionSource<List<ReportObject>>();
            _rabbitMqPublisher.Publish();
            List<ReportObject> list = new();
            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += async (object? sender, BasicDeliverEventArgs e) =>
            {
                try
                {
                    var jsonString = Encoding.UTF8.GetString(e.Body.ToArray());
                    var options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true,
                    };
                    var data = JsonSerializer.Deserialize<List<ReportObject>>(jsonString, options);
                    list.AddRange(data);
                    result.SetResult(data);
                    _channel.BasicAck(e.DeliveryTag, false);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Hata: {ex.Message}");
                    _channel.BasicAck(e.DeliveryTag, false);
                }
            };
            _channel.BasicConsume(RabbitMqClientService.QueueName, false, consumer);
            return result.Task;
        }
        public class ReportObject
        {
            public string Location { get; set; }
            public int PersonCount { get; set; }
            public int PhoneCount { get; set; }
        }
    }
}