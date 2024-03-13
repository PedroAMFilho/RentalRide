using FluentValidator;
using FluentValidator.Validation;
using RentalRide.Domain.UserBaseContext.Enum;
using RentalRide.Shared.Commands;

namespace RentalRide.Domain.UserBaseContext.Commands.Inputs
{
    public class CreateUserBaseCommand : Notifiable, ICommand
    {
        public string username { get; set; }
        public string password { get; set; }
        public string email { get; set; }
        public EAccessLevel access_level { get; set; }
        public int deliverer_id { get; set; }

        public bool IsValidCommand()
        {
            AddNotifications(new ValidationContract()
                .Requires()
                .HasMinLen(username, 3, "User", "Username must contain at least 3 characters")
                .HasMaxLen(username, 20, "User", "Username must contain at maximum 20 characters")
                .HasMaxLen(password, 20, "Password", "Password must contain at maximum 20 characters")
                .HasMinLen(password, 5, "Password", "Password must contain at least 3 characters")
            );
            return Valid;
        }
    }
}
