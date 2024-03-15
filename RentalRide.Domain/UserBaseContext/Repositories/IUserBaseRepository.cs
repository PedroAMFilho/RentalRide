using RentalRide.Domain.UserBaseContext.Commands.Inputs;
using RentalRide.Domain.UserBaseContext.Entities;

namespace RentalRide.Domain.UserBaseContext.Repositories
{
    public interface IUserBaseRepository
    {
        User UserBase(string user, string password);
        void Create(CreateUserBaseCommand command);
    }
}
