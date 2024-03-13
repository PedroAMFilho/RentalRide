using FluentValidator;
using FluentValidator.Validation;
using System.Text;

namespace RentalRide.Domain.UserBaseContext.ValueObjects
{
    public class LoginVO : Notifiable
    {
        public LoginVO() { }

        public LoginVO(string user, string password)
        {
            User = user;
            Password = EncryptPassword(password);

            AddNotifications(new ValidationContract()
                .Requires()
                .HasMaxLen(User, 20, "User", "Username must contain at maximum 20 characters")
                .HasMinLen(User, 3, "User", "Username must contain at least 3 characters")
            );
        }

        public LoginVO(string user)
        {
            User = user;

            AddNotifications(new ValidationContract()
                .Requires()
                .HasMaxLen(User, 20, "User", "Username must contain at maximum 20 characters")
                .HasMinLen(User, 3, "User", "Username must contain at least 3 characters")
            );
        }

        public string User { get; private set; }
        public string Password { get; private set; }

        public string EncryptPassword(string pass)
        {
            if (string.IsNullOrEmpty(pass)) return "";
         
            var password = pass;
            var md5 = System.Security.Cryptography.MD5.Create();
            var data = md5.ComputeHash(Encoding.Default.GetBytes(password));
            var sbString = new StringBuilder();
            foreach (var t in data)
                sbString.Append(t.ToString("x2"));

            return sbString.ToString();
        }

        public void AtributePassword(string password) => this.Password = password;

        public string GeneratePassword() => Guid.NewGuid().ToString().Replace("-", "").Substring(0, 8).ToUpper();

        public override string ToString() => $"[ {GetType().Name} - User: {User}, Password: {Password} ]";
    }
}
