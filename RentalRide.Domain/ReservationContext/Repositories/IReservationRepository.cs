using RentalRide.Domain.ReservationContext.Commands.Entities;
using RentalRide.Domain.ReservationContext.Commands.Inputs;
using RentalRide.Domain.ReservationContext.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalRide.Domain.ReservationContext.Repositories
{
    public interface IReservationRepository
    {
        public void Create(CreateReservationCommand command);
        public GetReservationQueryResult GetReservation(int id);
        public void CreateReservationPlan(CreateReservationPlanCommand command);
    }
}
