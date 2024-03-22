using FluentValidator;
using FluentValidator.Validation;
using RentalRide.Shared.Commands;

namespace RentalRide.Domain.DeliveryContext.Commands.Inputs
{
    public class AcceptDeliveryComand : Notifiable, ICommand
    {
        public int DelivererId { get; set; }
        public int DeliveryId { get; set; }

        public bool IsValidCommand()
        {
            AddNotifications(new ValidationContract()
                .Requires()
            );
            return Valid;
        }
    }
}
