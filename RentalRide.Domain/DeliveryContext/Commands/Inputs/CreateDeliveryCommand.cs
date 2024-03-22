using FluentValidator;
using FluentValidator.Validation;
using RentalRide.Domain.DeliveryContext.Enum;
using RentalRide.Shared.Commands;
using System.Xml.Schema;

namespace RentalRide.Domain.DeliveryContext.Commands.Inputs
{
    public class CreateDeliveryCommand : Notifiable, ICommand
    {
        public ECurrentStatus CurrentStatus { get; set; }
        public decimal DeliveryCost { get; set; }

        public bool IsValidCommand()
        {
            AddNotifications(new ValidationContract()
                .Requires()
            );
            return Valid;
        }
    }
}
