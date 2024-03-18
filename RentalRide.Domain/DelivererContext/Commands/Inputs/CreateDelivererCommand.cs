using FluentValidator;
using FluentValidator.Validation;
using Microsoft.AspNetCore.Http;
using RentalRide.Domain.UserBaseContext.Enum;
using RentalRide.Shared.Commands;
using System.ComponentModel.DataAnnotations;


namespace RentalRide.Domain.DelivererContext.Commands.Inputs
{
    public class CreateDelivererCommand : Notifiable, ICommand
    {
        public string username { get; set; }
        public string password { get; set; }
        public string cnpj { get; set; }
        public string email { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public DateTime date_of_birth { get; set; }
        public string drivers_license { get; set; }
        public string license_photo_url { get; set; }
        
        //[MinFileSize(125), MaxFileSize(5 * 1024 * 1024)]
        //[AllowedExtensions(new[] { ".jpg", ".png", ".gif", ".jpeg", ".tiff" })]
        public IFormFile imageFile { get; set; }
        public ELicense license_type { get; set; }

        public bool IsValidCommand() 
        {
            AddNotifications(new ValidationContract()
                .Requires()
                .HasMinLen(username, 3, "User", "Username must contain at least 3 characters")
                .HasMaxLen(username, 20, "User", "Username must contain at maximum 20 characters")
                .HasMaxLen(password, 20, "Password", "Password must contain at maximum 20 characters")
                .HasMinLen(password, 5, "Password", "Password must contain at least 3 characters")
                .IsEmail(email,"Email","Inform a valid email")
            );
            return Valid;
        }
    }
}
