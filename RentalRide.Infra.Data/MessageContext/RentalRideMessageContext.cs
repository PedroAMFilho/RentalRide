using RabbitMQ.Client;

namespace RentalRide.Infra.Data.MessageContext
{
    public class RentalRideMessageContext
    {
        public IModel Channel { get; set; }

        public RentalRideMessageContext()
        {
            ConnectionFactory connectionFactory = new ConnectionFactory
            {
                HostName = "rabbitmq_rental"
            };
            using var connection = connectionFactory.CreateConnection();

            Channel = connection.CreateModel();
        }
    }
}
