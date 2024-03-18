﻿using Dapper;
using RentalRide.Domain.DelivererContext.Commands.Outputs;
using RentalRide.Domain.DeliveryContext.Commands.Inputs;
using RentalRide.Domain.DeliveryContext.Entities;
using RentalRide.Domain.DeliveryContext.Repositories;
using RentalRide.Infra.Data.DataContext;
using RentalRide.Shared.Commands;
using System.Data;
using System.Text;

namespace RentalRide.Infra.Data.DeliveryContext.Repositories
{
    public class DeliveryRepository : IDeliveryRepository
    {
        private readonly RentalRideDataContext _context;

        public DeliveryRepository(RentalRideDataContext context)
        {
            _context = context;
        }

        public void Create(CreateDeliveryCommand command) 
        {
            var query = new StringBuilder();
            query.Append("INSERT INTO delivery(current_status, delivery_cost) ");
            query.Append("VALUES(:current_status, :delivery_cost) ");

            var param = new DynamicParameters();
            param.Add(name: "current_status", value: (int)command.current_status, direction: ParameterDirection.Input);
            param.Add(name: "delivery_cost", value: (int)command.delivery_cost, direction: ParameterDirection.Input);

            _context.Connection.Execute(query.ToString(), param);

        }

        public void AcceptDelivery(AcceptDeliveryComand command)
        {
            var query = new StringBuilder();
            query.Append("UPDATE delivery SET deliverer_id = :deliverer_id WHERE id = delivery_id");
            var param = new DynamicParameters();
            param.Add(name: "deliverer_id", value: command.deliverer_id, direction: ParameterDirection.Input);
            param.Add(name: "delivery_id", value: command.delivery_id, direction: ParameterDirection.Input);

            _context.Connection.Execute(query.ToString(), param);
        }

        public ICommandResult GetDelivery(int delivery_id, int deliverer_id)
        {
            var query = new StringBuilder();
            query.Append("SELECT id , create_at, deliverer_id, current_status, delivery_cost FROM delivery where id = :delivery_id and deliverer_id  = :delivery_id");

            var param = new DynamicParameters();
            param.Add(name: "delivery_id", value: delivery_id, direction: ParameterDirection.Input);
            param.Add(name: "deliverer_id", value: deliverer_id, direction: ParameterDirection.Input);
            var deliverer = _context.Connection.Query<Delivery>(query.ToString(), param).FirstOrDefault();

            return new CommandResult(true, "Delivery sucessfuly located:", new
            {
               deliverer
            });
        }

        public bool DelivererHasDelivery(int deliverer_id)
        {
            var query = new StringBuilder();
            query.Append(@"SELECT id , create_at, deliverer_id, current_status, delivery_cost FROM delivery where deliverer_id  = :delivery_id and current_status = 0");

            var param = new DynamicParameters();
            param.Add(name: "deliverer_id", value: deliverer_id, direction: ParameterDirection.Input);

            var result = _context.Connection.Query<bool>(query.ToString(), param);

            return result.Any();
        }

        public ICommandResult EndDelivery(int delivery_id) 
        {
            var query = new StringBuilder();
            query.Append("UPDATE delivery SET current_status = 1 WHERE id = delivery_id");
            var param = new DynamicParameters();
            param.Add(name: "delivery_id", value: delivery_id, direction: ParameterDirection.Input);

            _context.Connection.Execute(query.ToString(), param);

            return new CommandResult(true, "Delivery completed:", new
            {
                delivery_id
            });

        }
    }
}
