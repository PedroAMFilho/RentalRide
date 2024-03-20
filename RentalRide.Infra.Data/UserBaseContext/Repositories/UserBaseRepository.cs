using Npgsql;
using RentalRide.Domain.UserBaseContext.Entities;
using RentalRide.Domain.UserBaseContext.Repositories;
using RentalRide.Infra.Data.DataContext;
using System.Text;
using Dapper;
using RentalRide.Domain.UserBaseContext.Commands.Inputs;
using System.Data;

namespace RentalRide.Infra.Data.UserBaseContext.Repositories
{
    public class UserBaseRepository : IUserBaseRepository
    {
        private readonly RentalRideDataContext _context;

        public UserBaseRepository(RentalRideDataContext context)
        {
            _context = context;
        }

        public User UserBase(string user, string pass)
        {
            var query = @"SELECT * FROM userbase where username = :user AND password = :pass";
            var userBase = _context.Connection.Query<User>(query, new { user, pass });
            return userBase?.FirstOrDefault() ?? new User();
        }

        public void Create(CreateUserBaseCommand command)
        {
            var query = new StringBuilder();
            query.Append("INSERT INTO userbase(username, password, access_level, deliverer_id, email) ");
            query.Append("VALUES(:username, :password, :access_level, :deliverer_id,:email) ");

            var param = new DynamicParameters();
            param.Add(name: "username", value: command.username, direction: ParameterDirection.Input);
            param.Add(name: "password", value: command.password, direction: ParameterDirection.Input);
            param.Add(name: "deliverer_id", value: command.deliverer_id, direction: ParameterDirection.Input);
            param.Add(name: "email", value: command.email, direction: ParameterDirection.Input);
            param.Add(name: "access_level", value: (int)command.access_level, direction: ParameterDirection.Input);

            _context.Connection.Execute(query.ToString(), param);
        }

    }
}
