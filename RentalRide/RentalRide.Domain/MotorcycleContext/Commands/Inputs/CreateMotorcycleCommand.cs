using FluentValidator;
using FluentValidator.Validation;
using RentalRide.Shared.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalRide.Domain.MotorcycleContext.Commands.Inputs
{
    public class CreateMotorcycleCommand : Notifiable, ICommand
    {
        public string model { get; set; }
        public int year { get; set; }
        public string license_plate { get; set; }
        public bool IsValidCommand()
        {
            AddNotifications(new ValidationContract()
                .Requires()
                .HasMinLen(license_plate, 6, "License Plate", "Licence plate must have at least 6 characters")
                .HasMaxLen(license_plate, 9, "License Plate", "Licence plate must have at maximum 9 characters")
                .HasMinLen(model, 3, "Model", "Model must have at least 3 characters")
                .HasMaxLen(model, 25, "Model", "Model must have at maximum 20 characters")
            );
            return Valid;
        }
    }
}
