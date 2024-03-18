using RentalRide.Domain.DeliveryContext.Entities;

namespace RentalRide.Domain.DeliveryContext.Repositories
{
    public interface IMessageService
    {
        public void MessageClients(Delivery delivery);
    }
}
