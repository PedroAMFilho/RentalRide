using FluentValidator;
using FluentValidator.Validation;
using Microsoft.AspNetCore.Http;
using RentalRide.Domain.UserBaseContext.Enum;
using RentalRide.Shared.Commands;

namespace RentalRide.Domain.DelivererContext.Commands.Inputs
{
    public class CreateDelivererCommand : Notifiable, ICommand
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Cnpj { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string DriversLicense { get; set; }
        public string LicensePhotoUrl { get; set; }
        
        //[MinFileSize(125), MaxFileSize(5 * 1024 * 1024)]
        //[AllowedExtensions(new[] { ".jpg", ".png", ".gif", ".jpeg", ".tiff" })]
        public IFormFile ImageFile { get; set; }
        public ELicense LicenseType { get; set; }

        public bool IsValidCommand() 
        {
            AddNotifications(new ValidationContract()
                .Requires()
                .HasMinLen(Username, 2, "User", "Username must contain at least 3 characters")
                .HasMaxLen(Username, 20, "User", "Username must contain at maximum 20 characters")
                .HasMaxLen(Password, 20, "Password", "Password must contain at maximum 20 characters")
                .HasMinLen(Password, 5, "Password", "Password must contain at least 3 characters")
                //.Matchs( cnpj, @"\\d{2}\\.?\\d{3}\\.?\\d{3}\\/?\\d{4}\\-?\\d{2}\\", "cnpj", "CNPJ format is invalid" )
                //Matchs(drivers_license, @"((cnh.*[0-9]{11})|(CNH.*[0-9]{11})|(habilitação.*[0-9]{11})|(carteira.*[0-9]{11}))", "Driver's license", "Driver's license format is invalid")
                .IsEmail(Email,"Email","Inform a valid email")
            );
            return Valid;
        }
    }
}
