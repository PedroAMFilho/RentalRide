using RentalRide.Domain.ReservationContext.Commands.Inputs;
using RentalRide.Domain.ReservationContext.Repositories;
using RentalRide.Infra.Data.DataContext;
using Dapper;
using System.Text;
using System.Data;
using RentalRide.Domain.ReservationContext.Queries;

namespace RentalRide.Infra.Data.ReservationContext.Repositories
{
    public class ReservationRepository : IReservationRepository
    {
        private readonly RentalRideDataContext _context;

        public ReservationRepository(RentalRideDataContext context)
        {
            _context = context;
        }

        public GetReservationQueryResult GetReservation(int id)
        {
            var query = new StringBuilder();
            query.Append("SELECT start_date as StartDate, end_date as EndDate, model as Model, year as Year, license_plate as LicensePlate, ");
            query.Append("name as Name, rental_days as RentalDays, daily_cost as DailyCost, status as Status, percentage_fine as PercentageFine ");
            query.Append("from reservation r INNER JOIN reservation_plan p ON r.reservation_plan_id = p.id ");
            query.Append("INNER JOIN motorcycle m ON r.motorcycle = m.id ");
            query.Append("WHERE r.id = :id");
            var param = new DynamicParameters();
            param.Add(name: "id", value: id, direction: ParameterDirection.Input);

            var reservation = _context.Connection.Query<GetReservationQueryResult>(query.ToString(), param);
            
            return reservation?.FirstOrDefault() ?? new GetReservationQueryResult();
        }

        public int Create(CreateReservationCommand command) 
        {
            var query = new StringBuilder();
            query.Append(@"INSERT INTO reservation (start_date, estimated_end_date, motorcycle, reservation_plan_id, deliverer_id) ");
            query.Append(@"VALUES(:start_date,:estimated_end_date,:motorcycle_id, :reservation_plan_id,:deliverer_id) ");
            query.Append(@"RETURNING id");

            var param = new DynamicParameters();
            param.Add(name: "start_date", value: command.StartDate, direction: ParameterDirection.Input);
            param.Add(name: "estimated_end_date", value: command.EstimatedEndDate, direction: ParameterDirection.Input);
            param.Add(name: "motorcycle_id", value: command.MotorcycleId, direction: ParameterDirection.Input);
            param.Add(name: "reservation_plan_id", value: command.ReservationPlanId, direction: ParameterDirection.Input);
            param.Add(name: "deliverer_id", value: command.DelivererId, direction: ParameterDirection.Input);

            var id = _context.Connection.Execute(query.ToString(), param);

            return id;
        }

        public int CreateReservationPlan(CreateReservationPlanCommand command)
        {
            var query = new StringBuilder();
            query.Append(@"INSERT INTO reservation_plan (name, rental_days, daily_cost, percentage_fine) ");
            query.Append(@"VALUES(:name,:rental_days,:daily_cost, :percentage_fine) ");
            query.Append(@"RETURNING id");

            var param = new DynamicParameters();
            param.Add(name: "name", value: command.Name, direction: ParameterDirection.Input);
            param.Add(name: "rental_days", value: command.RentalDays, direction: ParameterDirection.Input);
            param.Add(name: "daily_cost", value: command.DailyCost, direction: ParameterDirection.Input);
            param.Add(name: "percentage_fine", value: command.PercentageFine, direction: ParameterDirection.Input);

            var id = _context.Connection.Execute(query.ToString(), param);

            return id;
        }
    }
}
