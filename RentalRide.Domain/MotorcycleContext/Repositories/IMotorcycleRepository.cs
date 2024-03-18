using RentalRide.Domain.MotorcycleContext.Commands.Inputs;
using RentalRide.Domain.MotorcycleContext.Entities;
using RentalRide.Domain.MotorcycleContext.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalRide.Domain.MotorcycleContext.Repositories
{
    public interface IMotorcycleRepository
    {
        IEnumerable<Motorcycle> GetByLicense(string license);
        public void Create(CreateMotorcycleCommand command);
        public void Update(UpdateMotorcycleCommand command);
        public bool MotorcycleIsAvailable(int motorcycle_id);
        public void Delete(int id);
    }
}
