using RentalRide.Domain.DelivererContext.Commands.Inputs;
using RentalRide.Domain.DelivererContext.Entities;

namespace RentalRide.Domain.DelivererContext.Repositories
{
    public interface IDelivererRepository
    {
        public IEnumerable<Deliverer> GetAllDeliverers();
        public IEnumerable<Deliverer> GetDelivererByName(string name);
        public int Create(CreateDelivererCommand command);
        public Deliverer GetDelivererById(int id);
    }
}
