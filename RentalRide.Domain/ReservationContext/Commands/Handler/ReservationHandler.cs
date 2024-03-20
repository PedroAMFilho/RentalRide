using RentalRide.Domain.DelivererContext.Repositories;
using RentalRide.Domain.MotorcycleContext.Repositories;
using RentalRide.Domain.ReservationContext.Commands.Inputs;
using RentalRide.Domain.ReservationContext.Repositories;
using RentalRide.Domain.UserBaseContext.Commands.Outputs;
using RentalRide.Domain.UserBaseContext.Enum;
using RentalRide.Shared.Commands;

namespace RentalRide.Domain.ReservationContext.Commands.Handler
{
    public class ReservationHandler
    {
        private readonly IReservationRepository _repository;
        private readonly IMotorcycleRepository _motorcycleRepository;
        private readonly IDelivererRepository _delivererRepository;

        public ReservationHandler(IReservationRepository repository, IMotorcycleRepository motorcycleRepository, IDelivererRepository delivererRepository)
        {
            _repository = repository;
            _motorcycleRepository = motorcycleRepository;
            _delivererRepository = delivererRepository;
        }

        public ICommandResult Handle(CreateReservationCommand command)
        {
            if (!command.IsValidCommand())
                return new CommandResult(false, "Invalid request, please verify the input fields.", new { command.Notifications });

            if (!_motorcycleRepository.MotorcycleIsAvailable(command.motorcycle_id))
                return new CommandResult(false, "This motorcycle id is already rented.", new { command.motorcycle_id });

            var deliverer = _delivererRepository.GetDelivererById(command.deliverer_id);
            if (!IsATypeDriverLicense(deliverer.license_type))
                return new CommandResult(false, "Driver's license dosen't allow this reservation.", new { deliverer });

            var returnedId = _repository.Create(command);
            if(returnedId == 0)
                return new CommandResult(false, "Failed to create the reservation.", new { returnedId });

            return new CommandResult(true, "Reservation created with success", new
            {
                returnedId
            });
        }

        private bool IsATypeDriverLicense(ELicense licenseType) 
        { 
            return licenseType == ELicense.A || licenseType == ELicense.AB;
        
        }

        public ICommandResult Handle(ConsultReservationCommand command)
        {
            var reservation = _repository.GetReservation(command.reservation_id);

            decimal fine = 0;
            int reservationDays = (command.estimated_end_date - reservation.start_date).Days;
            int planReservationDays = reservation.rental_days;
            decimal standardDailyCost = reservation.daily_cost;
            decimal reservationBasePrice = Convert.ToDecimal(planReservationDays) * standardDailyCost;

            if (reservationDays > planReservationDays)
            {
                int exceededDays = (reservationDays - planReservationDays);
                fine = Convert.ToDecimal(exceededDays) * 50;

            }
            else if (planReservationDays > reservationDays)
            {
                int unusedDays = planReservationDays - reservationDays;
                reservationBasePrice = (planReservationDays - unusedDays) * standardDailyCost;
                decimal unusedDaysFullCost = Convert.ToDecimal(unusedDays) * standardDailyCost;
                fine = (unusedDaysFullCost * reservation.percentage_fine) / 100;
            }

            return new CommandResult(true, "Consultation created with success", new
            {
                reservationFinalPrice = Math.Round(reservationBasePrice + fine, 2),
                reservationBasePrice  = Math.Round(reservationBasePrice,2),
                fine  = Math.Round(fine,2)
            }); ;
        }

        public ICommandResult Handle(CreateReservationPlanCommand command)
        {
            var id = _repository.CreateReservationPlan(command);

            return new CommandResult(true, "Reservation plan created with success", new
            {
                id
            });
        }
    }
}
