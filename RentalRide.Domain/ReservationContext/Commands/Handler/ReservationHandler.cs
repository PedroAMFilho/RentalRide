using RentalRide.Domain.ReservationContext.Commands.Inputs;
using RentalRide.Domain.ReservationContext.Repositories;
using RentalRide.Domain.UserBaseContext.Commands.Outputs;
using RentalRide.Shared.Commands;

namespace RentalRide.Domain.ReservationContext.Commands.Handler
{
    public class ReservationHandler
    {
        private readonly IReservationRepository _repository;

        public ReservationHandler(IReservationRepository repository)
        {
            _repository = repository;
        }

        public ICommandResult Handle(CreateReservationCommand command)
        {

            //check if mototcycle_id is available
            //check if deliverer has A or AB license

            _repository.Create(command);

            return new CommandResult(true, "Reservation created with success", new
            {
            });
        }

        public ICommandResult Handle(ConsultReservationCommand command)
        {

            //check if mototcycle_id is available
            //check if deliverer has A or AB license

            var result = _repository.GetReservation(command.reservation_id);

            return new CommandResult(true, "Consultation created with success", new
            {
                reservationFinalPrice = 0,
                reservationBasePrice = 0,
                reservationFine = 0
            });
        }

        public ICommandResult Handle(CreateReservationPlanCommand command)
        {

            //check if mototcycle_id is available
            //check if deliverer has A or AB license

            _repository.CreateReservationPlan(command);

            return new CommandResult(true, "Reservation plan created with success", new
            {
            });
        }


    }
}
