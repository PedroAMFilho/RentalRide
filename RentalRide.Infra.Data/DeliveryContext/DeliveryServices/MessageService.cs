using Newtonsoft.Json;
using RabbitMQ.Client;
using RentalRide.Domain.DelivererContext.Entities;
using RentalRide.Domain.DelivererContext.Repositories;
using RentalRide.Domain.DeliveryContext.Entities;
using RentalRide.Domain.DeliveryContext.Repositories;
using RentalRide.Infra.Data.MessageContext;
using System.Text;


namespace RentalRide.Infra.Data.DeliveryContext.DeliveryServices
{
    public class MessageService : IMessageService
    {
        private readonly RentalRideMessageContext _context;
        private readonly IDelivererRepository _delivererRepository;

        public MessageService(RentalRideMessageContext context, IDelivererRepository delivererRepository)
        {
            _context = context;
            _delivererRepository = delivererRepository;
        }

        public void MessageClients(Delivery delivery)
        {
            ConnectionFactory connectionFactory = new ConnectionFactory
            {
                HostName = "rabbitmq_rental"
            };
            using var connection = connectionFactory.CreateConnection();
            var deliverers = _delivererRepository.GetAllDeliverers();

            using var channel = connection.CreateModel();

            channel.QueueDeclare("delivery_notification", exclusive: false);

            foreach (Deliverer deliverer in deliverers)
            {
                var textMessage = string.Concat("There is a new delivery available for you ", deliverer.FirstName, " the full delivery price is :", delivery.DeliveryCost);

                var objectMessage = new
                {
                    textMessage,
                    delivery,
                    deliverer
                };

                var json = JsonConvert.SerializeObject(objectMessage);
                var body = Encoding.UTF8.GetBytes(json);

                channel.BasicPublish(exchange: "", routingKey: "delivery_notification", body: body);
            }
        }
    }
}
