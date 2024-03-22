using FluentValidator;
using FluentValidator.Validation;
using RentalRide.Shared.Commands;

namespace RentalRide.Domain.ReservationContext.Commands.Inputs
{
    public class CreateReservationCommand : Notifiable, ICommand
    {
        public DateTime StartDate { get; set; }
        public DateTime EstimatedEndDate { get; set; }
        public int MotorcycleId { get; set; }
        public int ReservationPlanId { get; set; }
        public int DelivererId { get; set; }


        public bool IsValidCommand()
        {
            AddNotifications(new ValidationContract()
                .Requires()
                .IsGreaterThan(EstimatedEndDate, StartDate, "estimated date", "Date of estimated end of reservation must be higher than the start date")
                );

            return Valid;
        }
    }
}
