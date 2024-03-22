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
        public string Model { get; set; }
        public int Year { get; set; }
        public string LicensePlate { get; set; }
        public bool IsValidCommand()
        {
            AddNotifications(new ValidationContract()
                .Requires()
                .HasMinLen(LicensePlate, 6, "License Plate", "Licence plate must have at least 6 characters")
                .HasMaxLen(LicensePlate, 9, "License Plate", "Licence plate must have at maximum 9 characters")
                .HasMinLen(Model, 3, "Model", "Model must have at least 3 characters")
                .HasMaxLen(Model, 25, "Model", "Model must have at maximum 20 characters")
            );
            return Valid;
        }
    }
}
