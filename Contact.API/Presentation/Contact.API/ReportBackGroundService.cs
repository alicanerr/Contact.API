using Contact.Application.Abstraction;
using Contact.Application.Dtos;
using Contact.Application.Repositories;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

namespace Contact.API
{
    public class ReportBackGroundService : BackgroundService
    {
        private readonly RabbitMqClientService _rabbitMqClientService;
        private readonly IModel _channel;
        private readonly IPersonService _personService;
        private readonly IContactInformationService _contactInformationService;

        public ReportBackGroundService(RabbitMqClientService rabbitMqClientService, IPersonService personService, IContactInformationService contactInformationService)
        {
            _rabbitMqClientService = rabbitMqClientService;
            _channel = _rabbitMqClientService.Connect();
            _channel.BasicQos(0, 1, false);
            _personService = personService;
            _contactInformationService = contactInformationService;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var consumer = new EventingBasicConsumer(_channel);
            
            consumer.Received += async (object? sender, BasicDeliverEventArgs e) =>
            {
                var lsit = await _personService.GetAllAsync();
                var lsi2 = await _contactInformationService.GetAllAsync();

                var groupedQuery = from prsn in lsit
                                   join contact in lsi2 on prsn.Id equals contact.PersonId
                                   group contact by contact.Location into locationGroup
                                   select new ReportObject
                                   {
                                       Location = locationGroup.Key,
                                       PersonCount = locationGroup.Select(x => x.PersonId).Distinct().Count(),
                                       PhoneCount = locationGroup.Select(x => x.Phone).Distinct().Count(),
                                   };

                var result = groupedQuery.ToList();
                Publish(result);

                _channel.BasicAck(e.DeliveryTag, false);
            };
            _channel.BasicConsume(RabbitMqClientService.QueueName, false, consumer);

        
        }
        public void Publish(List<ReportObject> datas)
        {
            var channel = _rabbitMqClientService.Connect2();

            var body = JsonSerializer.Serialize(datas);
            var bodyByte = Encoding.UTF8.GetBytes(body);

            var properties = channel.CreateBasicProperties();
            properties.Persistent = true;
            channel.BasicPublish(RabbitMqClientService.ExchangeName, RabbitMqClientService.ExchangeName, basicProperties: properties,body:bodyByte);
        }

    }

    public class ReportObject
    {
        public string Location { get; set; }
        public int PersonCount { get; set; }
        public int PhoneCount { get; set; }

    }
}
