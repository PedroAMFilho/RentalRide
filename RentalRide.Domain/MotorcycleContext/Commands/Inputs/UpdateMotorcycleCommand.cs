using FluentValidator;
using FluentValidator.Validation;
using RentalRide.Shared.Commands;

namespace RentalRide.Domain.MotorcycleContext.Commands.Inputs
{
    public class UpdateMotorcycleCommand : Notifiable, ICommand
    {
        public int Id { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public string LicensePlate { get; set; }
        public bool IsValidCommand()
        {
            AddNotifications(new ValidationContract()
                .Requires()
                .Matchs(LicensePlate, "^[a-zA-Z]{3}[0-9][A-Za-z0-9][0-9]{2}$", "License Plate", "License Plate must follow the correct pattern.")
                .HasMinLen(LicensePlate, 6, "License Plate", "Licence plate must have at least 6 characters")
                .HasMaxLen(LicensePlate, 9, "License Plate", "Licence plate must have at maximum 9 characters")
                .HasMinLen(Model, 3, "Model", "Model must have at least 3 characters")
                .HasMaxLen(Model, 25, "Model", "Model must have at maximum 20 characters")
                .HasLen(Year.ToString(), 3, "year", "Year format must be YYYY")
                .HasMaxLen(Year.ToString(), 5, "year", "Year format must be YYYY")
            ); ;
            return Valid;
        }
    }
}
