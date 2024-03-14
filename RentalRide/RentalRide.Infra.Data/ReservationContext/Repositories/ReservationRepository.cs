using RentalRide.Domain.ReservationContext.Commands.Inputs;
using RentalRide.Domain.ReservationContext.Repositories;
using RentalRide.Infra.Data.DataContext;
using Dapper;
using System.Text;
using System.Data;
using RentalRide.Domain.ReservationContext.Commands.Entities;

namespace RentalRide.Infra.Data.ReservationContext.Repositories
{
    public class ReservationRepository : IReservationRepository
    {
        private readonly RentalRideDataContext _context;

        public ReservationRepository(RentalRideDataContext context)
        {
            _context = context;
        }

        public Reservation GetReservation(int id)
        {
            var query = new StringBuilder();
            query.Append("SELECT start_date, end_date, model, year, license_plate, name, rental_days, daily_cost, status ");
            query.Append("from reservation r INNER JOIN reservation_plan p ON r.reservation_plan_id = p.id ");
            query.Append("INNER JOIN motorcycle m ON r.motorcycle = m.id ");
            query.Append("WHERE r.id = :id");
            var param = new DynamicParameters();
            param.Add(name: "id", value: id, direction: ParameterDirection.Input);

            var reservation = _context.Connection.Query<Reservation>(query.ToString(), param);
            
            return reservation.FirstOrDefault();
        }

        public void Create(CreateReservationCommand command) 
        {
            var query = new StringBuilder();
            query.Append(@"INSERT INTO reservation (start_date, estimated_end_date, motorcycle, reservation_plan_id, deliverer_id) ");
            query.Append(@"VALUES(:start_date,:estimated_end_date,:motorcycle_id, :reservation_plan_id,:deliverer_id) ");

            var param = new DynamicParameters();
            param.Add(name: "start_date", value: command.start_date, direction: ParameterDirection.Input);
            param.Add(name: "estimated_end_date", value: command.estimated_end_date, direction: ParameterDirection.Input);
            param.Add(name: "motorcycle_id", value: command.motorcycle_id, direction: ParameterDirection.Input);
            param.Add(name: "reservation_plan_id", value: command.reservation_plan_id, direction: ParameterDirection.Input);
            param.Add(name: "deliverer_id", value: command.deliverer_id, direction: ParameterDirection.Input);

            _context.Connection.Execute(query.ToString(), param);
        }

        public void CreateReservationPlan(CreateReservationPlanCommand command)
        {
            var query = new StringBuilder();
            query.Append(@"INSERT INTO reservation_plan (name, rental_days, daily_cost) ");
            query.Append(@"VALUES(:name,:rental_days,:daily_cost) ");

            var param = new DynamicParameters();
            param.Add(name: "name", value: command.name, direction: ParameterDirection.Input);
            param.Add(name: "rental_days", value: command.rental_days, direction: ParameterDirection.Input);
            param.Add(name: "daily_cost", value: command.daily_cost, direction: ParameterDirection.Input);

            _context.Connection.Execute(query.ToString(), param);
        }
    }
}
