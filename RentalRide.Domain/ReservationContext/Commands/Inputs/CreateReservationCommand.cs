using FluentValidator;
using FluentValidator.Validation;
using RentalRide.Shared.Commands;

namespace RentalRide.Domain.ReservationContext.Commands.Inputs
{
    public class CreateReservationCommand : Notifiable, ICommand
    {
        public DateTime start_date { get; set; }
        public DateTime estimated_end_date { get; set; }
        public int motorcycle_id { get; set; }
        public int reservation_plan_id { get; set; }
        public int deliverer_id { get; set; }


        public bool IsValidCommand()
        {
            AddNotifications(new ValidationContract()
                .Requires()
                .IsGreaterThan(estimated_end_date, start_date, "estimated date", "Date of estimated end of reservation must be higher than the start date")
                );

            return Valid;
        }
    }
}
