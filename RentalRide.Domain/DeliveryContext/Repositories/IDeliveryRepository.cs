using RentalRide.Domain.DeliveryContext.Commands.Inputs;
using RentalRide.Shared.Commands;

namespace RentalRide.Domain.DeliveryContext.Repositories
{
    public interface IDeliveryRepository
    {
        public void Create(CreateDeliveryCommand command);
        public void AcceptDelivery(AcceptDeliveryComand command);
        public ICommandResult GetDelivery(int delivery_id,int deliverer_id);
        public bool DelivererHasDelivery(int deliverer_id);
        public ICommandResult EndDelivery(int delivery_id);
    }
}
