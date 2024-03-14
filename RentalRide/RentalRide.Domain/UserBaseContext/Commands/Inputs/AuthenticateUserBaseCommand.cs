using FluentValidator;
using FluentValidator.Validation;
using RentalRide.Shared.Commands;

namespace RentalRide.Domain.UserBaseContext.Commands.Inputs
{
    public class AuthenticateUserBaseCommand : Notifiable, ICommand
    {
        public string User { get; set; }
        public string Password { get; set; }

        public bool IsValidCommand()
        {
            AddNotifications(new ValidationContract()
                .Requires()
                .HasMinLen(User, 3, "User", "Username must contain at least 3 characters")
                .HasMaxLen(User, 20, "User", "Username must contain at maximum 20 characters")
                .HasMaxLen(Password, 20, "Password", "Password must contain at maximum 20 characters")
                .HasMinLen(Password, 5, "Password", "Password must contain at least 3 characters")
            );
            return Valid;
        }
    }
}
