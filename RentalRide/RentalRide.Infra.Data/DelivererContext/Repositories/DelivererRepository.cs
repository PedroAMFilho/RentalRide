using Dapper;
using RentalRide.Domain.DelivererContext.Commands.Inputs;
using RentalRide.Domain.DelivererContext.Entities;
using RentalRide.Domain.DelivererContext.Queries;
using RentalRide.Domain.DelivererContext.Repositories;
using RentalRide.Infra.Data.DataContext;
using RentalRide.Shared.Commands;
using System.Data;
using System.Text;

namespace RentalRide.Infra.Data.DelivererContext.Repositories
{
    public class DelivererRepository : IDelivererRepository
    {

        private readonly RentalRideDataContext _context;

        public DelivererRepository(RentalRideDataContext context)
        {
            _context = context;
        }

        public IEnumerable<Deliverer> GetAllDeliverers()
        {
            var query = @"SELECT id_del, first_name, last_name, date_of_birth, drivers_license, license_photo_url, license_type FROM deliverer";
            var deliverer = _context.Connection.Query<Deliverer>(query);

            return deliverer;
        }

        public IEnumerable<Deliverer> GetDelivererByName(string name)
        {
            var query = new StringBuilder();
            query.Append(@"SELECT id_del, first_name, last_name, date_of_birth, drivers_license, license_photo_url, license_type FROM deliverer where concat(first_name , ' ' , last_name) LIKE :name");
            var deliverer = _context.Connection.Query<Deliverer>(query.ToString(), new { name = string.Concat(name, "%") });

            return deliverer;
        }

        public int Create(CreateDelivererCommand command) 
        {
            var query = new StringBuilder(); 
            query.Append("INSERT INTO deliverer(first_name, last_name, drivers_license, license_type, license_photo_url, date_of_birth) ");
            query.Append("VALUES(:first_name, :last_name, :drivers_license, :license_type,:license_photo_url,:date_of_birth) ");
            query.Append("RETURNING id_del");

            var param = new DynamicParameters();
            param.Add(name: "ide_del", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);
            param.Add(name: "first_name", value: command.first_name, direction: ParameterDirection.Input);
            param.Add(name: "last_name", value: command.last_name, direction: ParameterDirection.Input);
            param.Add(name: "date_of_birth", value: Convert.ToDateTime(command.date_of_birth), direction: ParameterDirection.Input);
            param.Add(name: "drivers_license", value: command.drivers_license, direction: ParameterDirection.Input);
            param.Add(name: "license_photo_url", value: command.license_photo_url, direction: ParameterDirection.Input);
            param.Add(name: "license_type", value: (int)command.license_type, direction: ParameterDirection.Input);
           
            var Id = _context.Connection.Execute(query.ToString(), param);

            return Id;
        }
    }
}
