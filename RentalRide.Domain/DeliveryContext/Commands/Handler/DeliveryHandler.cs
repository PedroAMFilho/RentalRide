using FluentValidator;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RentalRide.Domain.DeliveryContext.Commands.Inputs;
using RentalRide.Domain.DeliveryContext.Commands.Outputs;
using RentalRide.Domain.DeliveryContext.Entities;
using RentalRide.Domain.DeliveryContext.Repositories;
using RentalRide.Shared.Commands;

namespace RentalRide.Domain.DeliveryContext.Commands.Handler
{
    public class DeliveryHandler : Notifiable, ICommandHandler<CreateDeliveryCommand>, ICommandHandler<AcceptDeliveryComand>
    {
        private readonly IDeliveryRepository _repository;
        private readonly IMessageService _service;

        public DeliveryHandler(IDeliveryRepository repository, IMessageService service)
        { 
            _repository = repository;
            _service = service;
        }

        public ICommandResult Handle(AcceptDeliveryComand command)
        {
            if (_repository.DelivererHasDelivery(command.DelivererId))
            {
                _repository.AcceptDelivery(command);

                return new CommandResult(true, "Delivery successfully accepted .", new{});
            }
            else { 
                return new CommandResult(false, "Deliverer already has an active delivery.", new { });
            }
        }

        public ICommandResult Handle(CreateDeliveryCommand command)
        {
            _repository.Create(command);

            _service.MessageClients(new Delivery() { DeliveryCost = command.DeliveryCost, CurrentStatus = command.CurrentStatus }); ;

            return new CommandResult(true, "Delivery created with success.", new
            {
            });
        }
    }
}
