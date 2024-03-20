using RentalRide.Domain.MotorcycleContext.Repositories;
using RentalRide.Infra.Data.DataContext;
using Dapper;
using RentalRide.Domain.MotorcycleContext.Entities;
using RentalRide.Domain.MotorcycleContext.Commands.Inputs;
using System.Text;
using System.Data;
using System.ComponentModel;
using RentalRide.Domain.ReservationContext.Commands.Entities;

namespace RentalRide.Infra.Data.MotorcycleContext.Repositories
{
    public class MotorcycleRepository :IMotorcycleRepository
    {
        private readonly RentalRideDataContext _context;

        public MotorcycleRepository(RentalRideDataContext context)
        {
            _context = context;
        }

        public IEnumerable<Motorcycle> GetByLicense(string license)
        {
            var query = @"SELECT * FROM motorcycle where license_plate LIKE :license";
            var motorcycles = _context.Connection.Query<Motorcycle>(query, new { license = string.Concat(license, "%") });

            return motorcycles;
        }

        public void Create(CreateMotorcycleCommand command) 
        {
            var query = new StringBuilder();
            query.Append(@"INSERT INTO motorcycle (model, year, license_plate) ");
            query.Append(@"VALUES(:model,:year,:license_plate) ");

            var param = new DynamicParameters();
            param.Add(name: "model", value: command.model, direction: ParameterDirection.Input);
            param.Add(name: "year", value: command.year, direction: ParameterDirection.Input);
            param.Add(name: "license_plate", value: command.license_plate, direction: ParameterDirection.Input);

            _context.Connection.Execute(query.ToString(), param);
        }

        public bool MotorcycleIsAvailable(int motorcycle_id) 
        {
            var query = @"SELECT * FROM reservation where motorcycle = :motorcycle_id and status = 0";
            var param = new DynamicParameters();
            param.Add(name: "motorcycle_id", value: motorcycle_id, direction: ParameterDirection.Input);

            var motorcycles = _context.Connection.Query<Reservation>(query, param);

            return !motorcycles.Any();
        }

        public void Update(UpdateMotorcycleCommand command) 
        {
            var query = new StringBuilder();
            query.Append(@"UPDATE motorcycle SET ");
            if (command.model != null)
                query.Append(@"model = :model ");

            if (command.year != 0)
                query.Append(@", year = :year");

            if(command.license_plate != null)
                query.Append(@", license_plate =:license_plate ");

            query.Append(@"WHERE id = :id");

            var param = new DynamicParameters();

            _context.Connection.Query(query.ToString(), param);
        }

        public void Delete(int id)
        {
            var query = @"DELETE FROM motorcycle where id = :id";
            var param = new DynamicParameters();
            param.Add(name: "id", value: id, direction: ParameterDirection.Input);
            _context.Connection.Query(query, param);
        }
    }
}
