using Dapper;
using RentalRide.Domain.DelivererContext.Commands.Inputs;
using RentalRide.Domain.DelivererContext.Entities;
using RentalRide.Domain.DelivererContext.Repositories;
using RentalRide.Infra.Data.DataContext;
using System.Data;
using System.Text;

namespace RentalRide.Infra.Data.DelivererContext.Repositories
{
    public class DelivererRepository : IDelivererRepository
    {
        private readonly RentalRideDataContext _context;
        private readonly string SelectDeliverer = @"SELECT id_del as Id, first_name as FirstName, last_name as LastName, date_of_birth as DateOfBirth, drivers_license as DriversLicense, cnpj as Cnpj, license_photo_url as LicensePhotoUrl, license_type as LicenseType FROM deliverer ";

        public DelivererRepository(RentalRideDataContext context)
        {
            _context = context;
        }


        public IEnumerable<Deliverer> GetAllDeliverers()
        {
            var query = SelectDeliverer;
            var deliverer = _context.Connection.Query<Deliverer>(query);

            return deliverer;
        }

        public IEnumerable<Deliverer> GetDelivererByName(string name)
        {
            var query = new StringBuilder();
            query.Append(SelectDeliverer);
            query.Append(@"where concat(first_name , ' ' , last_name) LIKE :name");
            var deliverer = _context.Connection.Query<Deliverer>(query.ToString(), new { name = string.Concat(name, "%") });

            return deliverer;
        }

        public Deliverer GetDelivererById(int deliverer_id)
        {
            var query = new StringBuilder();
            query.Append(SelectDeliverer);
            query.Append(@"where id_del = :deliverer_id");
            var deliverer = _context.Connection.Query<Deliverer>(query.ToString(), new { deliverer_id });

            return deliverer.FirstOrDefault() ?? new Deliverer();
        }

        public IEnumerable<Deliverer> GetAllAvailableDeliverers() 
        {
            var query = new StringBuilder();
            query.Append(SelectDeliverer);
            query.Append(@"LEFT JOIN delivery where delivery.deliverer_id IS NULL");
            var deliverer = _context.Connection.Query<Deliverer>(query.ToString());

            return deliverer;
        }

        public int Create(CreateDelivererCommand command) 
        {
            var query = new StringBuilder(); 
            query.Append("INSERT INTO deliverer(first_name, last_name, cnpj, drivers_license, license_type, license_photo_url, date_of_birth) ");
            query.Append("VALUES(:first_name, :last_name, :cnpj, :drivers_license, :license_type,:license_photo_url,:date_of_birth) ");
            query.Append("RETURNING id_del");

            var param = new DynamicParameters();
            param.Add(name: "ide_del", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);
            param.Add(name: "first_name", value: command.FirstName, direction: ParameterDirection.Input);
            param.Add(name: "last_name", value: command.LastName, direction: ParameterDirection.Input);
            param.Add(name: "cnpj", value: command.Cnpj, direction: ParameterDirection.Input);
            param.Add(name: "date_of_birth", value: Convert.ToDateTime(command.DateOfBirth), direction: ParameterDirection.Input);
            param.Add(name: "drivers_license", value: command.DriversLicense, direction: ParameterDirection.Input);
            param.Add(name: "license_photo_url", value: command.LicensePhotoUrl, direction: ParameterDirection.Input);
            param.Add(name: "license_type", value: (int)command.LicenseType, direction: ParameterDirection.Input);
           
            var id = _context.Connection.Execute(query.ToString(), param);

            return id;
        }
    }
}
