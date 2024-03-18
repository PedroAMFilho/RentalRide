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
    public class UpdateMotorcycleCommand : Notifiable, ICommand
    {
        public int id { get; set; }
        public string model { get; set; }
        public int year { get; set; }
        public string license_plate { get; set; }
        public bool IsValidCommand()
        {
            AddNotifications(new ValidationContract()
                .Requires()
                .Matchs(license_plate, "^[a-zA-Z]{3}[0-9][A-Za-z0-9][0-9]{2}$", "License Plate", "License Plate must follow the correct pattern.")
                .HasMinLen(license_plate, 6, "License Plate", "Licence plate must have at least 6 characters")
                .HasMaxLen(license_plate, 9, "License Plate", "Licence plate must have at maximum 9 characters")
                .HasMinLen(model, 3, "Model", "Model must have at least 3 characters")
                .HasMaxLen(model, 25, "Model", "Model must have at maximum 20 characters")
                .HasLen(year.ToString(), 3, "year", "Year format must be YYYY")
                .HasMaxLen(year.ToString(), 5, "year", "Year format must be YYYY")
            ); ;
            return Valid;
        }
    }


}
